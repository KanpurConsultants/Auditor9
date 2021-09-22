<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmChequeIssueReport
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmChequeIssueReport))
        Me.pnlToolBar = New System.Windows.Forms.Panel()
        Me.txtChequeNumber = New System.Windows.Forms.TextBox()
        Me.lblChequeNumber = New System.Windows.Forms.Label()
        Me.cmbChequeStatus = New System.Windows.Forms.ComboBox()
        Me.lblChequeStatus = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.rbnInfixBank = New System.Windows.Forms.RadioButton()
        Me.rbnSuffixBank = New System.Windows.Forms.RadioButton()
        Me.rbnPrefixBank = New System.Windows.Forms.RadioButton()
        Me.txtBankName = New System.Windows.Forms.TextBox()
        Me.lblBankName = New System.Windows.Forms.Label()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.rbnInfixPayee = New System.Windows.Forms.RadioButton()
        Me.rbnSuffixPayee = New System.Windows.Forms.RadioButton()
        Me.rbnPrefixPayee = New System.Windows.Forms.RadioButton()
        Me.txtPayName = New System.Windows.Forms.TextBox()
        Me.lblPayName = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.rbnChequeDate = New System.Windows.Forms.RadioButton()
        Me.rbnIssueDate = New System.Windows.Forms.RadioButton()
        Me.txtBillAmountTo = New System.Windows.Forms.TextBox()
        Me.txtBillAmountFrom = New System.Windows.Forms.TextBox()
        Me.lblBillAmount = New System.Windows.Forms.Label()
        Me.dtpChequeDateTo = New System.Windows.Forms.DateTimePicker()
        Me.lblBillDateTo = New System.Windows.Forms.Label()
        Me.dtpChequeDateFrom = New System.Windows.Forms.DateTimePicker()
        Me.lblBillDateFrom = New System.Windows.Forms.Label()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.pnlone = New System.Windows.Forms.Panel()
        Me.rbnInfixCompany = New System.Windows.Forms.RadioButton()
        Me.rbnSuffixCompany = New System.Windows.Forms.RadioButton()
        Me.rbnPrefixCompany = New System.Windows.Forms.RadioButton()
        Me.txtCompanyName = New System.Windows.Forms.TextBox()
        Me.lblCompanyName = New System.Windows.Forms.Label()
        Me.pnlContent = New System.Windows.Forms.Panel()
        Me.CrystalReportViewer1 = New CrystalDecisions.Windows.Forms.CrystalReportViewer()
        Me.lblHeading = New System.Windows.Forms.Label()
        Me.pnlFooter = New System.Windows.Forms.Panel()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.pnlToolBar.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.pnlone.SuspendLayout()
        Me.pnlContent.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlToolBar
        '
        Me.pnlToolBar.Controls.Add(Me.txtChequeNumber)
        Me.pnlToolBar.Controls.Add(Me.lblChequeNumber)
        Me.pnlToolBar.Controls.Add(Me.cmbChequeStatus)
        Me.pnlToolBar.Controls.Add(Me.lblChequeStatus)
        Me.pnlToolBar.Controls.Add(Me.Label1)
        Me.pnlToolBar.Controls.Add(Me.btnExit)
        Me.pnlToolBar.Controls.Add(Me.Panel7)
        Me.pnlToolBar.Controls.Add(Me.Panel5)
        Me.pnlToolBar.Controls.Add(Me.Panel2)
        Me.pnlToolBar.Controls.Add(Me.txtBillAmountTo)
        Me.pnlToolBar.Controls.Add(Me.txtBillAmountFrom)
        Me.pnlToolBar.Controls.Add(Me.lblBillAmount)
        Me.pnlToolBar.Controls.Add(Me.dtpChequeDateTo)
        Me.pnlToolBar.Controls.Add(Me.lblBillDateTo)
        Me.pnlToolBar.Controls.Add(Me.dtpChequeDateFrom)
        Me.pnlToolBar.Controls.Add(Me.lblBillDateFrom)
        Me.pnlToolBar.Controls.Add(Me.btnSearch)
        Me.pnlToolBar.Location = New System.Drawing.Point(0, 42)
        Me.pnlToolBar.Name = "pnlToolBar"
        Me.pnlToolBar.Size = New System.Drawing.Size(1019, 133)
        Me.pnlToolBar.TabIndex = 21
        '
        'txtChequeNumber
        '
        Me.txtChequeNumber.Location = New System.Drawing.Point(796, 13)
        Me.txtChequeNumber.Name = "txtChequeNumber"
        Me.txtChequeNumber.Size = New System.Drawing.Size(186, 20)
        Me.txtChequeNumber.TabIndex = 97
        '
        'lblChequeNumber
        '
        Me.lblChequeNumber.AutoSize = True
        Me.lblChequeNumber.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChequeNumber.Location = New System.Drawing.Point(671, 14)
        Me.lblChequeNumber.Name = "lblChequeNumber"
        Me.lblChequeNumber.Size = New System.Drawing.Size(116, 17)
        Me.lblChequeNumber.TabIndex = 96
        Me.lblChequeNumber.Text = "Cheque Number"
        '
        'cmbChequeStatus
        '
        Me.cmbChequeStatus.FormattingEnabled = True
        Me.cmbChequeStatus.Items.AddRange(New Object() {"Draft Cheque", "Issued Cheque", "Cancel Cheque"})
        Me.cmbChequeStatus.Location = New System.Drawing.Point(796, 47)
        Me.cmbChequeStatus.Name = "cmbChequeStatus"
        Me.cmbChequeStatus.Size = New System.Drawing.Size(186, 21)
        Me.cmbChequeStatus.TabIndex = 95
        '
        'lblChequeStatus
        '
        Me.lblChequeStatus.AutoSize = True
        Me.lblChequeStatus.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChequeStatus.Location = New System.Drawing.Point(672, 48)
        Me.lblChequeStatus.Name = "lblChequeStatus"
        Me.lblChequeStatus.Size = New System.Drawing.Size(103, 17)
        Me.lblChequeStatus.TabIndex = 94
        Me.lblChequeStatus.Text = "Cheque Status"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(176, 85)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(14, 19)
        Me.Label1.TabIndex = 82
        Me.Label1.Text = "-"
        '
        'btnExit
        '
        Me.btnExit.BackColor = System.Drawing.Color.White
        Me.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnExit.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.Image = Global.DenariuChequeWriterSoft.My.Resources.Resources.exit_32
        Me.btnExit.Location = New System.Drawing.Point(888, 75)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(94, 39)
        Me.btnExit.TabIndex = 81
        Me.btnExit.Text = " E&XIT"
        Me.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'Panel7
        '
        Me.Panel7.Controls.Add(Me.Panel4)
        Me.Panel7.Controls.Add(Me.txtBankName)
        Me.Panel7.Controls.Add(Me.lblBankName)
        Me.Panel7.Location = New System.Drawing.Point(341, 15)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(315, 43)
        Me.Panel7.TabIndex = 80
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.rbnInfixBank)
        Me.Panel4.Controls.Add(Me.rbnSuffixBank)
        Me.Panel4.Controls.Add(Me.rbnPrefixBank)
        Me.Panel4.Location = New System.Drawing.Point(130, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(182, 20)
        Me.Panel4.TabIndex = 65
        '
        'rbnInfixBank
        '
        Me.rbnInfixBank.AutoSize = True
        Me.rbnInfixBank.Checked = True
        Me.rbnInfixBank.Location = New System.Drawing.Point(72, 0)
        Me.rbnInfixBank.Name = "rbnInfixBank"
        Me.rbnInfixBank.Size = New System.Drawing.Size(44, 17)
        Me.rbnInfixBank.TabIndex = 13
        Me.rbnInfixBank.TabStop = True
        Me.rbnInfixBank.Text = "Infix"
        Me.rbnInfixBank.UseVisualStyleBackColor = True
        '
        'rbnSuffixBank
        '
        Me.rbnSuffixBank.AutoSize = True
        Me.rbnSuffixBank.Location = New System.Drawing.Point(124, 0)
        Me.rbnSuffixBank.Name = "rbnSuffixBank"
        Me.rbnSuffixBank.Size = New System.Drawing.Size(51, 17)
        Me.rbnSuffixBank.TabIndex = 14
        Me.rbnSuffixBank.Text = "Suffix"
        Me.rbnSuffixBank.UseVisualStyleBackColor = True
        '
        'rbnPrefixBank
        '
        Me.rbnPrefixBank.AutoSize = True
        Me.rbnPrefixBank.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold)
        Me.rbnPrefixBank.Location = New System.Drawing.Point(0, 0)
        Me.rbnPrefixBank.Name = "rbnPrefixBank"
        Me.rbnPrefixBank.Size = New System.Drawing.Size(64, 20)
        Me.rbnPrefixBank.TabIndex = 12
        Me.rbnPrefixBank.Text = "Prefix"
        Me.rbnPrefixBank.UseVisualStyleBackColor = True
        '
        'txtBankName
        '
        Me.txtBankName.Location = New System.Drawing.Point(2, 20)
        Me.txtBankName.Name = "txtBankName"
        Me.txtBankName.Size = New System.Drawing.Size(310, 20)
        Me.txtBankName.TabIndex = 64
        '
        'lblBankName
        '
        Me.lblBankName.AutoSize = True
        Me.lblBankName.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBankName.Location = New System.Drawing.Point(2, 4)
        Me.lblBankName.Name = "lblBankName"
        Me.lblBankName.Size = New System.Drawing.Size(81, 16)
        Me.lblBankName.TabIndex = 63
        Me.lblBankName.Text = "Bank Name"
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.Panel1)
        Me.Panel5.Controls.Add(Me.txtPayName)
        Me.Panel5.Controls.Add(Me.lblPayName)
        Me.Panel5.Location = New System.Drawing.Point(18, 15)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(315, 43)
        Me.Panel5.TabIndex = 78
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.rbnInfixPayee)
        Me.Panel1.Controls.Add(Me.rbnSuffixPayee)
        Me.Panel1.Controls.Add(Me.rbnPrefixPayee)
        Me.Panel1.Location = New System.Drawing.Point(131, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(182, 20)
        Me.Panel1.TabIndex = 47
        '
        'rbnInfixPayee
        '
        Me.rbnInfixPayee.AutoSize = True
        Me.rbnInfixPayee.Checked = True
        Me.rbnInfixPayee.Location = New System.Drawing.Point(72, 0)
        Me.rbnInfixPayee.Name = "rbnInfixPayee"
        Me.rbnInfixPayee.Size = New System.Drawing.Size(44, 17)
        Me.rbnInfixPayee.TabIndex = 13
        Me.rbnInfixPayee.TabStop = True
        Me.rbnInfixPayee.Text = "Infix"
        Me.rbnInfixPayee.UseVisualStyleBackColor = True
        '
        'rbnSuffixPayee
        '
        Me.rbnSuffixPayee.AutoSize = True
        Me.rbnSuffixPayee.Location = New System.Drawing.Point(124, 0)
        Me.rbnSuffixPayee.Name = "rbnSuffixPayee"
        Me.rbnSuffixPayee.Size = New System.Drawing.Size(51, 17)
        Me.rbnSuffixPayee.TabIndex = 14
        Me.rbnSuffixPayee.Text = "Suffix"
        Me.rbnSuffixPayee.UseVisualStyleBackColor = True
        '
        'rbnPrefixPayee
        '
        Me.rbnPrefixPayee.AutoSize = True
        Me.rbnPrefixPayee.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold)
        Me.rbnPrefixPayee.Location = New System.Drawing.Point(0, 0)
        Me.rbnPrefixPayee.Name = "rbnPrefixPayee"
        Me.rbnPrefixPayee.Size = New System.Drawing.Size(64, 20)
        Me.rbnPrefixPayee.TabIndex = 12
        Me.rbnPrefixPayee.Text = "Prefix"
        Me.rbnPrefixPayee.UseVisualStyleBackColor = True
        '
        'txtPayName
        '
        Me.txtPayName.Location = New System.Drawing.Point(3, 20)
        Me.txtPayName.Name = "txtPayName"
        Me.txtPayName.Size = New System.Drawing.Size(310, 20)
        Me.txtPayName.TabIndex = 46
        '
        'lblPayName
        '
        Me.lblPayName.AutoSize = True
        Me.lblPayName.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPayName.Location = New System.Drawing.Point(3, 4)
        Me.lblPayName.Name = "lblPayName"
        Me.lblPayName.Size = New System.Drawing.Size(73, 16)
        Me.lblPayName.TabIndex = 45
        Me.lblPayName.Text = "Pay Name"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.rbnChequeDate)
        Me.Panel2.Controls.Add(Me.rbnIssueDate)
        Me.Panel2.Location = New System.Drawing.Point(602, 79)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(180, 29)
        Me.Panel2.TabIndex = 77
        '
        'rbnChequeDate
        '
        Me.rbnChequeDate.AutoSize = True
        Me.rbnChequeDate.Location = New System.Drawing.Point(90, 6)
        Me.rbnChequeDate.Name = "rbnChequeDate"
        Me.rbnChequeDate.Size = New System.Drawing.Size(88, 17)
        Me.rbnChequeDate.TabIndex = 1
        Me.rbnChequeDate.Text = "Cheque Date"
        Me.rbnChequeDate.UseVisualStyleBackColor = True
        '
        'rbnIssueDate
        '
        Me.rbnIssueDate.AutoSize = True
        Me.rbnIssueDate.Checked = True
        Me.rbnIssueDate.Location = New System.Drawing.Point(6, 6)
        Me.rbnIssueDate.Name = "rbnIssueDate"
        Me.rbnIssueDate.Size = New System.Drawing.Size(76, 17)
        Me.rbnIssueDate.TabIndex = 0
        Me.rbnIssueDate.TabStop = True
        Me.rbnIssueDate.Text = "Issue Date"
        Me.rbnIssueDate.UseVisualStyleBackColor = True
        '
        'txtBillAmountTo
        '
        Me.txtBillAmountTo.Location = New System.Drawing.Point(190, 84)
        Me.txtBillAmountTo.Name = "txtBillAmountTo"
        Me.txtBillAmountTo.Size = New System.Drawing.Size(96, 20)
        Me.txtBillAmountTo.TabIndex = 76
        '
        'txtBillAmountFrom
        '
        Me.txtBillAmountFrom.Location = New System.Drawing.Point(79, 84)
        Me.txtBillAmountFrom.Name = "txtBillAmountFrom"
        Me.txtBillAmountFrom.Size = New System.Drawing.Size(96, 20)
        Me.txtBillAmountFrom.TabIndex = 75
        '
        'lblBillAmount
        '
        Me.lblBillAmount.AutoSize = True
        Me.lblBillAmount.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBillAmount.Location = New System.Drawing.Point(17, 86)
        Me.lblBillAmount.Name = "lblBillAmount"
        Me.lblBillAmount.Size = New System.Drawing.Size(57, 16)
        Me.lblBillAmount.TabIndex = 74
        Me.lblBillAmount.Text = "Amount"
        '
        'dtpChequeDateTo
        '
        Me.dtpChequeDateTo.CustomFormat = "dd/MM/yyyy"
        Me.dtpChequeDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpChequeDateTo.Location = New System.Drawing.Point(476, 84)
        Me.dtpChequeDateTo.Name = "dtpChequeDateTo"
        Me.dtpChequeDateTo.Size = New System.Drawing.Size(124, 20)
        Me.dtpChequeDateTo.TabIndex = 73
        '
        'lblBillDateTo
        '
        Me.lblBillDateTo.AutoSize = True
        Me.lblBillDateTo.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBillDateTo.Location = New System.Drawing.Point(463, 86)
        Me.lblBillDateTo.Name = "lblBillDateTo"
        Me.lblBillDateTo.Size = New System.Drawing.Size(14, 19)
        Me.lblBillDateTo.TabIndex = 72
        Me.lblBillDateTo.Text = "-"
        '
        'dtpChequeDateFrom
        '
        Me.dtpChequeDateFrom.CustomFormat = "dd/MM/yyyy"
        Me.dtpChequeDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpChequeDateFrom.Location = New System.Drawing.Point(336, 84)
        Me.dtpChequeDateFrom.Name = "dtpChequeDateFrom"
        Me.dtpChequeDateFrom.Size = New System.Drawing.Size(124, 20)
        Me.dtpChequeDateFrom.TabIndex = 71
        Me.dtpChequeDateFrom.Value = New Date(2016, 9, 1, 0, 0, 0, 0)
        '
        'lblBillDateFrom
        '
        Me.lblBillDateFrom.AutoSize = True
        Me.lblBillDateFrom.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBillDateFrom.Location = New System.Drawing.Point(290, 86)
        Me.lblBillDateFrom.Name = "lblBillDateFrom"
        Me.lblBillDateFrom.Size = New System.Drawing.Size(37, 16)
        Me.lblBillDateFrom.TabIndex = 70
        Me.lblBillDateFrom.Text = "Date"
        '
        'btnSearch
        '
        Me.btnSearch.BackColor = System.Drawing.Color.White
        Me.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnSearch.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSearch.Image = Global.DenariuChequeWriterSoft.My.Resources.Resources.search_32
        Me.btnSearch.Location = New System.Drawing.Point(786, 75)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(94, 39)
        Me.btnSearch.TabIndex = 69
        Me.btnSearch.Text = "SEARC&H"
        Me.btnSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        Me.btnSearch.UseVisualStyleBackColor = False
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.pnlone)
        Me.Panel6.Controls.Add(Me.txtCompanyName)
        Me.Panel6.Controls.Add(Me.lblCompanyName)
        Me.Panel6.Location = New System.Drawing.Point(342, 42)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(348, 43)
        Me.Panel6.TabIndex = 79
        Me.Panel6.Visible = False
        '
        'pnlone
        '
        Me.pnlone.Controls.Add(Me.rbnInfixCompany)
        Me.pnlone.Controls.Add(Me.rbnSuffixCompany)
        Me.pnlone.Controls.Add(Me.rbnPrefixCompany)
        Me.pnlone.Location = New System.Drawing.Point(131, -1)
        Me.pnlone.Name = "pnlone"
        Me.pnlone.Size = New System.Drawing.Size(182, 20)
        Me.pnlone.TabIndex = 25
        '
        'rbnInfixCompany
        '
        Me.rbnInfixCompany.AutoSize = True
        Me.rbnInfixCompany.Checked = True
        Me.rbnInfixCompany.Location = New System.Drawing.Point(72, 0)
        Me.rbnInfixCompany.Name = "rbnInfixCompany"
        Me.rbnInfixCompany.Size = New System.Drawing.Size(44, 17)
        Me.rbnInfixCompany.TabIndex = 13
        Me.rbnInfixCompany.TabStop = True
        Me.rbnInfixCompany.Text = "Infix"
        Me.rbnInfixCompany.UseVisualStyleBackColor = True
        '
        'rbnSuffixCompany
        '
        Me.rbnSuffixCompany.AutoSize = True
        Me.rbnSuffixCompany.Location = New System.Drawing.Point(124, 0)
        Me.rbnSuffixCompany.Name = "rbnSuffixCompany"
        Me.rbnSuffixCompany.Size = New System.Drawing.Size(51, 17)
        Me.rbnSuffixCompany.TabIndex = 14
        Me.rbnSuffixCompany.Text = "Suffix"
        Me.rbnSuffixCompany.UseVisualStyleBackColor = True
        '
        'rbnPrefixCompany
        '
        Me.rbnPrefixCompany.AutoSize = True
        Me.rbnPrefixCompany.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold)
        Me.rbnPrefixCompany.Location = New System.Drawing.Point(0, 0)
        Me.rbnPrefixCompany.Name = "rbnPrefixCompany"
        Me.rbnPrefixCompany.Size = New System.Drawing.Size(64, 20)
        Me.rbnPrefixCompany.TabIndex = 12
        Me.rbnPrefixCompany.Text = "Prefix"
        Me.rbnPrefixCompany.UseVisualStyleBackColor = True
        '
        'txtCompanyName
        '
        Me.txtCompanyName.Location = New System.Drawing.Point(3, 19)
        Me.txtCompanyName.Name = "txtCompanyName"
        Me.txtCompanyName.Size = New System.Drawing.Size(333, 20)
        Me.txtCompanyName.TabIndex = 11
        '
        'lblCompanyName
        '
        Me.lblCompanyName.AutoSize = True
        Me.lblCompanyName.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCompanyName.Location = New System.Drawing.Point(3, 3)
        Me.lblCompanyName.Name = "lblCompanyName"
        Me.lblCompanyName.Size = New System.Drawing.Size(109, 16)
        Me.lblCompanyName.TabIndex = 10
        Me.lblCompanyName.Text = "Company Name"
        '
        'pnlContent
        '
        Me.pnlContent.AutoScroll = True
        Me.pnlContent.Controls.Add(Me.CrystalReportViewer1)
        Me.pnlContent.Controls.Add(Me.Panel6)
        Me.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlContent.Location = New System.Drawing.Point(0, 175)
        Me.pnlContent.Name = "pnlContent"
        Me.pnlContent.Size = New System.Drawing.Size(1022, 331)
        Me.pnlContent.TabIndex = 0
        '
        'CrystalReportViewer1
        '
        Me.CrystalReportViewer1.ActiveViewIndex = -1
        Me.CrystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CrystalReportViewer1.DisplayGroupTree = False
        Me.CrystalReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CrystalReportViewer1.Location = New System.Drawing.Point(0, 0)
        Me.CrystalReportViewer1.Name = "CrystalReportViewer1"
        Me.CrystalReportViewer1.SelectionFormula = ""
        Me.CrystalReportViewer1.Size = New System.Drawing.Size(1022, 331)
        Me.CrystalReportViewer1.TabIndex = 0
        Me.CrystalReportViewer1.ViewTimeSelectionFormula = ""
        '
        'lblHeading
        '
        Me.lblHeading.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblHeading.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeading.Location = New System.Drawing.Point(0, 0)
        Me.lblHeading.Name = "lblHeading"
        Me.lblHeading.Size = New System.Drawing.Size(1022, 42)
        Me.lblHeading.TabIndex = 0
        Me.lblHeading.Text = "CHEQUE ISSUE REPORT"
        Me.lblHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlFooter
        '
        Me.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlFooter.Location = New System.Drawing.Point(0, 506)
        Me.pnlFooter.Name = "pnlFooter"
        Me.pnlFooter.Size = New System.Drawing.Size(1022, 10)
        Me.pnlFooter.TabIndex = 1
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.pnlContent)
        Me.pnlMain.Controls.Add(Me.pnlFooter)
        Me.pnlMain.Controls.Add(Me.pnlHeader)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 0)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(1022, 516)
        Me.pnlMain.TabIndex = 16
        '
        'pnlHeader
        '
        Me.pnlHeader.AutoScroll = True
        Me.pnlHeader.Controls.Add(Me.pnlToolBar)
        Me.pnlHeader.Controls.Add(Me.lblHeading)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1022, 175)
        Me.pnlHeader.TabIndex = 0
        '
        'ErrorProvider1
        '
        Me.ErrorProvider1.ContainerControl = Me
        '
        'frmChequeIssueReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1022, 516)
        Me.Controls.Add(Me.pnlMain)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmChequeIssueReport"
        Me.pnlToolBar.ResumeLayout(False)
        Me.pnlToolBar.PerformLayout()
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        Me.pnlone.ResumeLayout(False)
        Me.pnlone.PerformLayout()
        Me.pnlContent.ResumeLayout(False)
        Me.pnlMain.ResumeLayout(False)
        Me.pnlHeader.ResumeLayout(False)
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlToolBar As System.Windows.Forms.Panel
    Friend WithEvents pnlContent As System.Windows.Forms.Panel
    Friend WithEvents CrystalReportViewer1 As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend WithEvents lblHeading As System.Windows.Forms.Label
    Friend WithEvents pnlFooter As System.Windows.Forms.Panel
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents ErrorProvider1 As System.Windows.Forms.ErrorProvider
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents rbnInfixBank As System.Windows.Forms.RadioButton
    Friend WithEvents rbnSuffixBank As System.Windows.Forms.RadioButton
    Friend WithEvents rbnPrefixBank As System.Windows.Forms.RadioButton
    Friend WithEvents txtBankName As System.Windows.Forms.TextBox
    Friend WithEvents lblBankName As System.Windows.Forms.Label
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents pnlone As System.Windows.Forms.Panel
    Friend WithEvents rbnInfixCompany As System.Windows.Forms.RadioButton
    Friend WithEvents rbnSuffixCompany As System.Windows.Forms.RadioButton
    Friend WithEvents rbnPrefixCompany As System.Windows.Forms.RadioButton
    Friend WithEvents txtCompanyName As System.Windows.Forms.TextBox
    Friend WithEvents lblCompanyName As System.Windows.Forms.Label
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents rbnInfixPayee As System.Windows.Forms.RadioButton
    Friend WithEvents rbnSuffixPayee As System.Windows.Forms.RadioButton
    Friend WithEvents rbnPrefixPayee As System.Windows.Forms.RadioButton
    Friend WithEvents txtPayName As System.Windows.Forms.TextBox
    Friend WithEvents lblPayName As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents rbnChequeDate As System.Windows.Forms.RadioButton
    Friend WithEvents rbnIssueDate As System.Windows.Forms.RadioButton
    Friend WithEvents txtBillAmountTo As System.Windows.Forms.TextBox
    Friend WithEvents txtBillAmountFrom As System.Windows.Forms.TextBox
    Friend WithEvents lblBillAmount As System.Windows.Forms.Label
    Friend WithEvents dtpChequeDateTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblBillDateTo As System.Windows.Forms.Label
    Friend WithEvents dtpChequeDateFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblBillDateFrom As System.Windows.Forms.Label
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbChequeStatus As System.Windows.Forms.ComboBox
    Friend WithEvents lblChequeStatus As System.Windows.Forms.Label
    Friend WithEvents lblChequeNumber As System.Windows.Forms.Label
    Friend WithEvents txtChequeNumber As System.Windows.Forms.TextBox
End Class
