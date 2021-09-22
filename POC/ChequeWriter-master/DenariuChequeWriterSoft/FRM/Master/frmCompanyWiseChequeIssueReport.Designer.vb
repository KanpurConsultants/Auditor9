<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCompanyWiseChequeIssueReport
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
        Me.components = New System.ComponentModel.Container
        Me.pnlContent = New System.Windows.Forms.Panel
        Me.CrystalReportViewer1 = New CrystalDecisions.Windows.Forms.CrystalReportViewer
        Me.pnlMain = New System.Windows.Forms.Panel
        Me.pnlFooter = New System.Windows.Forms.Panel
        Me.pnlHeader = New System.Windows.Forms.Panel
        Me.pnlTop = New System.Windows.Forms.Panel
        Me.lblHead = New System.Windows.Forms.Label
        Me.pnlToolBar = New System.Windows.Forms.Panel
        Me.Panel7 = New System.Windows.Forms.Panel
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.rbnInfixBank = New System.Windows.Forms.RadioButton
        Me.rbnSuffixBank = New System.Windows.Forms.RadioButton
        Me.rbnPrefixBank = New System.Windows.Forms.RadioButton
        Me.txtBankName = New System.Windows.Forms.TextBox
        Me.lblBankName = New System.Windows.Forms.Label
        Me.Panel6 = New System.Windows.Forms.Panel
        Me.pnlone = New System.Windows.Forms.Panel
        Me.rbnInfixCompany = New System.Windows.Forms.RadioButton
        Me.rbnSuffixCompany = New System.Windows.Forms.RadioButton
        Me.rbnPrefixCompany = New System.Windows.Forms.RadioButton
        Me.txtCompanyName = New System.Windows.Forms.TextBox
        Me.lblCompanyName = New System.Windows.Forms.Label
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.rbnInfixPayee = New System.Windows.Forms.RadioButton
        Me.rbnSuffixPayee = New System.Windows.Forms.RadioButton
        Me.rbnPrefixPayee = New System.Windows.Forms.RadioButton
        Me.txtPayName = New System.Windows.Forms.TextBox
        Me.lblPayName = New System.Windows.Forms.Label
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.rbnChequeDate = New System.Windows.Forms.RadioButton
        Me.rbnIssueDate = New System.Windows.Forms.RadioButton
        Me.txtBillAmountTo = New System.Windows.Forms.TextBox
        Me.txtBillAmountFrom = New System.Windows.Forms.TextBox
        Me.lblBillAmount = New System.Windows.Forms.Label
        Me.dtpChequeDateTo = New System.Windows.Forms.DateTimePicker
        Me.lblBillDateTo = New System.Windows.Forms.Label
        Me.dtpChequeDateFrom = New System.Windows.Forms.DateTimePicker
        Me.lblBillDateFrom = New System.Windows.Forms.Label
        Me.btnSearch = New System.Windows.Forms.Button
        Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.pboxClose = New System.Windows.Forms.PictureBox
        Me.pboxRestore = New System.Windows.Forms.PictureBox
        Me.pboxMinimize = New System.Windows.Forms.PictureBox
        Me.pboxMaximize = New System.Windows.Forms.PictureBox
        Me.pnlContent.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        Me.pnlTop.SuspendLayout()
        Me.pnlToolBar.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.pnlone.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        CType(Me.pboxClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pboxRestore, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pboxMinimize, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pboxMaximize, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlContent
        '
        Me.pnlContent.AutoScroll = True
        Me.pnlContent.Controls.Add(Me.CrystalReportViewer1)
        Me.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlContent.Location = New System.Drawing.Point(0, 166)
        Me.pnlContent.Name = "pnlContent"
        Me.pnlContent.Size = New System.Drawing.Size(1184, 485)
        Me.pnlContent.TabIndex = 0
        '
        'CrystalReportViewer1
        '
        Me.CrystalReportViewer1.ActiveViewIndex = -1
        Me.CrystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CrystalReportViewer1.DisplayGroupTree = False
        Me.CrystalReportViewer1.DisplayStatusBar = False
        Me.CrystalReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CrystalReportViewer1.Location = New System.Drawing.Point(0, 0)
        Me.CrystalReportViewer1.Name = "CrystalReportViewer1"
        Me.CrystalReportViewer1.SelectionFormula = ""
        Me.CrystalReportViewer1.Size = New System.Drawing.Size(1184, 485)
        Me.CrystalReportViewer1.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.CrystalReportViewer1, "Cheque Issue Report ")
        Me.CrystalReportViewer1.ViewTimeSelectionFormula = ""
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.pnlContent)
        Me.pnlMain.Controls.Add(Me.pnlFooter)
        Me.pnlMain.Controls.Add(Me.pnlHeader)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 0)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(1184, 661)
        Me.pnlMain.TabIndex = 16
        '
        'pnlFooter
        '
        Me.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlFooter.Location = New System.Drawing.Point(0, 651)
        Me.pnlFooter.Name = "pnlFooter"
        Me.pnlFooter.Size = New System.Drawing.Size(1184, 10)
        Me.pnlFooter.TabIndex = 1
        '
        'pnlHeader
        '
        Me.pnlHeader.AutoScroll = True
        Me.pnlHeader.Controls.Add(Me.pnlTop)
        Me.pnlHeader.Controls.Add(Me.pnlToolBar)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1184, 166)
        Me.pnlHeader.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.pnlHeader, "ChequeIssueReport")
        '
        'pnlTop
        '
        Me.pnlTop.BackColor = System.Drawing.Color.Transparent
        Me.pnlTop.Controls.Add(Me.Panel3)
        Me.pnlTop.Controls.Add(Me.lblHead)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(1184, 39)
        Me.pnlTop.TabIndex = 4
        '
        'lblHead
        '
        Me.lblHead.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblHead.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHead.ForeColor = System.Drawing.Color.DarkOrange
        Me.lblHead.Location = New System.Drawing.Point(0, 0)
        Me.lblHead.Name = "lblHead"
        Me.lblHead.Size = New System.Drawing.Size(1184, 39)
        Me.lblHead.TabIndex = 48
        Me.lblHead.Text = "Cheque Issue Report"
        Me.lblHead.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.ToolTip1.SetToolTip(Me.lblHead, "ChequeWritterPro")
        '
        'pnlToolBar
        '
        Me.pnlToolBar.Controls.Add(Me.Panel7)
        Me.pnlToolBar.Controls.Add(Me.Panel6)
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
        Me.pnlToolBar.Location = New System.Drawing.Point(24, 45)
        Me.pnlToolBar.Name = "pnlToolBar"
        Me.pnlToolBar.Size = New System.Drawing.Size(1148, 107)
        Me.pnlToolBar.TabIndex = 0
        '
        'Panel7
        '
        Me.Panel7.Controls.Add(Me.Panel4)
        Me.Panel7.Controls.Add(Me.txtBankName)
        Me.Panel7.Controls.Add(Me.lblBankName)
        Me.Panel7.Location = New System.Drawing.Point(329, 5)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(315, 43)
        Me.Panel7.TabIndex = 68
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
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.pnlone)
        Me.Panel6.Controls.Add(Me.txtCompanyName)
        Me.Panel6.Controls.Add(Me.lblCompanyName)
        Me.Panel6.Location = New System.Drawing.Point(652, 5)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(315, 43)
        Me.Panel6.TabIndex = 67
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
        Me.txtCompanyName.Size = New System.Drawing.Size(310, 20)
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
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.Panel1)
        Me.Panel5.Controls.Add(Me.txtPayName)
        Me.Panel5.Controls.Add(Me.lblPayName)
        Me.Panel5.Location = New System.Drawing.Point(6, 5)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(315, 43)
        Me.Panel5.TabIndex = 66
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
        Me.Panel2.Location = New System.Drawing.Point(652, 53)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(184, 29)
        Me.Panel2.TabIndex = 62
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
        Me.txtBillAmountTo.Location = New System.Drawing.Point(200, 57)
        Me.txtBillAmountTo.Name = "txtBillAmountTo"
        Me.txtBillAmountTo.Size = New System.Drawing.Size(119, 20)
        Me.txtBillAmountTo.TabIndex = 61
        '
        'txtBillAmountFrom
        '
        Me.txtBillAmountFrom.Location = New System.Drawing.Point(73, 57)
        Me.txtBillAmountFrom.Name = "txtBillAmountFrom"
        Me.txtBillAmountFrom.Size = New System.Drawing.Size(119, 20)
        Me.txtBillAmountFrom.TabIndex = 60
        '
        'lblBillAmount
        '
        Me.lblBillAmount.AutoSize = True
        Me.lblBillAmount.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBillAmount.Location = New System.Drawing.Point(9, 59)
        Me.lblBillAmount.Name = "lblBillAmount"
        Me.lblBillAmount.Size = New System.Drawing.Size(57, 16)
        Me.lblBillAmount.TabIndex = 59
        Me.lblBillAmount.Text = "Amount"
        '
        'dtpChequeDateTo
        '
        Me.dtpChequeDateTo.CustomFormat = "ddMMMMyyyy"
        Me.dtpChequeDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpChequeDateTo.Location = New System.Drawing.Point(517, 57)
        Me.dtpChequeDateTo.Name = "dtpChequeDateTo"
        Me.dtpChequeDateTo.Size = New System.Drawing.Size(124, 20)
        Me.dtpChequeDateTo.TabIndex = 44
        '
        'lblBillDateTo
        '
        Me.lblBillDateTo.AutoSize = True
        Me.lblBillDateTo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBillDateTo.Location = New System.Drawing.Point(504, 59)
        Me.lblBillDateTo.Name = "lblBillDateTo"
        Me.lblBillDateTo.Size = New System.Drawing.Size(12, 16)
        Me.lblBillDateTo.TabIndex = 43
        Me.lblBillDateTo.Text = "-"
        '
        'dtpChequeDateFrom
        '
        Me.dtpChequeDateFrom.CustomFormat = "ddMMMMyyyy"
        Me.dtpChequeDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpChequeDateFrom.Location = New System.Drawing.Point(377, 57)
        Me.dtpChequeDateFrom.Name = "dtpChequeDateFrom"
        Me.dtpChequeDateFrom.Size = New System.Drawing.Size(124, 20)
        Me.dtpChequeDateFrom.TabIndex = 42
        '
        'lblBillDateFrom
        '
        Me.lblBillDateFrom.AutoSize = True
        Me.lblBillDateFrom.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBillDateFrom.Location = New System.Drawing.Point(331, 59)
        Me.lblBillDateFrom.Name = "lblBillDateFrom"
        Me.lblBillDateFrom.Size = New System.Drawing.Size(37, 16)
        Me.lblBillDateFrom.TabIndex = 41
        Me.lblBillDateFrom.Text = "Date"
        '
        'btnSearch
        '
        Me.btnSearch.BackColor = System.Drawing.Color.White
        Me.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnSearch.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSearch.Image = Global.DenariuChequeWriterSoft.My.Resources.Resources.search_32
        Me.btnSearch.Location = New System.Drawing.Point(870, 48)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(94, 39)
        Me.btnSearch.TabIndex = 20
        Me.btnSearch.Text = "SEARC&H"
        Me.btnSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        Me.ToolTip1.SetToolTip(Me.btnSearch, "Perform Search")
        Me.btnSearch.UseVisualStyleBackColor = False
        '
        'ErrorProvider1
        '
        Me.ErrorProvider1.ContainerControl = Me
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.pboxClose)
        Me.Panel3.Controls.Add(Me.pboxRestore)
        Me.Panel3.Controls.Add(Me.pboxMinimize)
        Me.Panel3.Controls.Add(Me.pboxMaximize)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel3.Location = New System.Drawing.Point(1091, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(93, 39)
        Me.Panel3.TabIndex = 50
        '
        'pboxClose
        '
        Me.pboxClose.Image = Global.DenariuChequeWriterSoft.My.Resources.Resources.CLOSE1
        Me.pboxClose.Location = New System.Drawing.Point(60, 10)
        Me.pboxClose.Name = "pboxClose"
        Me.pboxClose.Size = New System.Drawing.Size(24, 24)
        Me.pboxClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pboxClose.TabIndex = 0
        Me.pboxClose.TabStop = False
        Me.ToolTip1.SetToolTip(Me.pboxClose, "Close")
        '
        'pboxRestore
        '
        Me.pboxRestore.Image = Global.DenariuChequeWriterSoft.My.Resources.Resources.restore1
        Me.pboxRestore.Location = New System.Drawing.Point(34, 10)
        Me.pboxRestore.Name = "pboxRestore"
        Me.pboxRestore.Size = New System.Drawing.Size(24, 24)
        Me.pboxRestore.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pboxRestore.TabIndex = 0
        Me.pboxRestore.TabStop = False
        Me.ToolTip1.SetToolTip(Me.pboxRestore, "Restore")
        '
        'pboxMinimize
        '
        Me.pboxMinimize.Image = Global.DenariuChequeWriterSoft.My.Resources.Resources.minimize1
        Me.pboxMinimize.Location = New System.Drawing.Point(8, 10)
        Me.pboxMinimize.Name = "pboxMinimize"
        Me.pboxMinimize.Size = New System.Drawing.Size(24, 24)
        Me.pboxMinimize.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pboxMinimize.TabIndex = 0
        Me.pboxMinimize.TabStop = False
        Me.ToolTip1.SetToolTip(Me.pboxMinimize, "Minimize")
        '
        'pboxMaximize
        '
        Me.pboxMaximize.Image = Global.DenariuChequeWriterSoft.My.Resources.Resources.maximize1
        Me.pboxMaximize.Location = New System.Drawing.Point(34, 10)
        Me.pboxMaximize.Name = "pboxMaximize"
        Me.pboxMaximize.Size = New System.Drawing.Size(24, 24)
        Me.pboxMaximize.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pboxMaximize.TabIndex = 0
        Me.pboxMaximize.TabStop = False
        Me.ToolTip1.SetToolTip(Me.pboxMaximize, "Maximize")
        '
        'frmCompanyWiseChequeIssueReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.DenariuChequeWriterSoft.My.Resources.Resources.plsn_bck__1_
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1184, 661)
        Me.Controls.Add(Me.pnlMain)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.KeyPreview = True
        Me.Name = "frmCompanyWiseChequeIssueReport"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Cheque Issue Report"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlContent.ResumeLayout(False)
        Me.pnlMain.ResumeLayout(False)
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlTop.ResumeLayout(False)
        Me.pnlToolBar.ResumeLayout(False)
        Me.pnlToolBar.PerformLayout()
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        Me.pnlone.ResumeLayout(False)
        Me.pnlone.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        CType(Me.pboxClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pboxRestore, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pboxMinimize, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pboxMaximize, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlContent As System.Windows.Forms.Panel
    Friend WithEvents CrystalReportViewer1 As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents pnlFooter As System.Windows.Forms.Panel
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents pnlToolBar As System.Windows.Forms.Panel
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents ErrorProvider1 As System.Windows.Forms.ErrorProvider
    Friend WithEvents dtpChequeDateTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblBillDateTo As System.Windows.Forms.Label
    Friend WithEvents dtpChequeDateFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblBillDateFrom As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents rbnInfixPayee As System.Windows.Forms.RadioButton
    Friend WithEvents rbnSuffixPayee As System.Windows.Forms.RadioButton
    Friend WithEvents rbnPrefixPayee As System.Windows.Forms.RadioButton
    Friend WithEvents txtPayName As System.Windows.Forms.TextBox
    Friend WithEvents lblPayName As System.Windows.Forms.Label
    Friend WithEvents txtBillAmountTo As System.Windows.Forms.TextBox
    Friend WithEvents txtBillAmountFrom As System.Windows.Forms.TextBox
    Friend WithEvents lblBillAmount As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents lblHead As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents rbnChequeDate As System.Windows.Forms.RadioButton
    Friend WithEvents rbnIssueDate As System.Windows.Forms.RadioButton
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents rbnInfixBank As System.Windows.Forms.RadioButton
    Friend WithEvents rbnSuffixBank As System.Windows.Forms.RadioButton
    Friend WithEvents rbnPrefixBank As System.Windows.Forms.RadioButton
    Friend WithEvents txtBankName As System.Windows.Forms.TextBox
    Friend WithEvents lblBankName As System.Windows.Forms.Label
    Friend WithEvents pnlone As System.Windows.Forms.Panel
    Friend WithEvents rbnInfixCompany As System.Windows.Forms.RadioButton
    Friend WithEvents rbnSuffixCompany As System.Windows.Forms.RadioButton
    Friend WithEvents rbnPrefixCompany As System.Windows.Forms.RadioButton
    Friend WithEvents txtCompanyName As System.Windows.Forms.TextBox
    Friend WithEvents lblCompanyName As System.Windows.Forms.Label
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents pboxClose As System.Windows.Forms.PictureBox
    Friend WithEvents pboxRestore As System.Windows.Forms.PictureBox
    Friend WithEvents pboxMinimize As System.Windows.Forms.PictureBox
    Friend WithEvents pboxMaximize As System.Windows.Forms.PictureBox
End Class
