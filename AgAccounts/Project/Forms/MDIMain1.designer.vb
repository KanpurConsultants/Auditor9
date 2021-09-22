<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MDIMain1
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
        Me.MnuMain = New System.Windows.Forms.MenuStrip()
        Me.AccountsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuMaster = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuNarrationMaster = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuDefineCostCenter = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuAccountGroup = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuAccountMaster = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuLedgerGroup = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuTransactions = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuVoucherEntry = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuBankReconsilationEntry = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuDisplay = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuTrialBalance_Disp = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuDetailTrialBalance_Disp = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuProfitAndLoss_Disp = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuBalanceSheet_Disp = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuStockReport = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuReports = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuDailyTransactionSummary = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuDayBook = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuBankBook = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuCashBook = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuJournalBook = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuLedger = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuAccountGroupMergeLedger = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuTrialGroup = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuTrialDetail = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuTrialDetailDrCr = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuAnnexure = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuMonthlyLedgerSummaryFull = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuReportsII = New System.Windows.Forms.ToolStripMenuItem()
        Me.SSrpMain = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel2 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.TbcMain = New System.Windows.Forms.TabControl()
        Me.Tbp1 = New System.Windows.Forms.TabPage()
        Me.MnuAgeingAnalysisBillWise = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuBillWiseAdjustmentRegister = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuAccountGroupWiseAgeingAnalysis = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuStockValuation = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuCashFlowStatement = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuFundFlowStatement = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuAgeingAnalysisFIFO = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuBillWiseOutstandingCreditors = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuBillWiseOutstandingDebtors = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuOutstandinDebtorsFIFO = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuOutstandingCreditorsFIFO = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuDailyCollectionRegister = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuInterestLedger = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuMonthyLedgerSummary = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuInterestCalculationForDebtors = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuMonthlyExpenseChart = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuDailyExpenseRegister = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuMain.SuspendLayout()
        Me.SSrpMain.SuspendLayout()
        Me.TbcMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'MnuMain
        '
        Me.MnuMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AccountsToolStripMenuItem})
        Me.MnuMain.Location = New System.Drawing.Point(0, 0)
        Me.MnuMain.Name = "MnuMain"
        Me.MnuMain.Size = New System.Drawing.Size(868, 24)
        Me.MnuMain.TabIndex = 1
        Me.MnuMain.Text = "MenuStrip1"
        '
        'AccountsToolStripMenuItem
        '
        Me.AccountsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuMaster, Me.MnuTransactions, Me.MnuDisplay, Me.MnuReports, Me.MnuReportsII})
        Me.AccountsToolStripMenuItem.Name = "AccountsToolStripMenuItem"
        Me.AccountsToolStripMenuItem.Size = New System.Drawing.Size(69, 20)
        Me.AccountsToolStripMenuItem.Text = "Accounts"
        '
        'MnuMaster
        '
        Me.MnuMaster.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuNarrationMaster, Me.MnuDefineCostCenter, Me.MnuAccountGroup, Me.MnuAccountMaster, Me.MnuLedgerGroup})
        Me.MnuMaster.Name = "MnuMaster"
        Me.MnuMaster.Size = New System.Drawing.Size(152, 22)
        Me.MnuMaster.Text = "Master"
        '
        'MnuNarrationMaster
        '
        Me.MnuNarrationMaster.Name = "MnuNarrationMaster"
        Me.MnuNarrationMaster.Size = New System.Drawing.Size(173, 22)
        Me.MnuNarrationMaster.Text = "Narration Master"
        '
        'MnuDefineCostCenter
        '
        Me.MnuDefineCostCenter.Name = "MnuDefineCostCenter"
        Me.MnuDefineCostCenter.Size = New System.Drawing.Size(173, 22)
        Me.MnuDefineCostCenter.Text = "Define Cost Center"
        '
        'MnuAccountGroup
        '
        Me.MnuAccountGroup.Name = "MnuAccountGroup"
        Me.MnuAccountGroup.Size = New System.Drawing.Size(173, 22)
        Me.MnuAccountGroup.Text = "Account Group"
        '
        'MnuAccountMaster
        '
        Me.MnuAccountMaster.Name = "MnuAccountMaster"
        Me.MnuAccountMaster.Size = New System.Drawing.Size(173, 22)
        Me.MnuAccountMaster.Text = "Account Master"
        '
        'MnuLedgerGroup
        '
        Me.MnuLedgerGroup.Name = "MnuLedgerGroup"
        Me.MnuLedgerGroup.Size = New System.Drawing.Size(173, 22)
        Me.MnuLedgerGroup.Text = "Ledger Group"
        Me.MnuLedgerGroup.Visible = False
        '
        'MnuTransactions
        '
        Me.MnuTransactions.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuVoucherEntry, Me.MnuBankReconsilationEntry})
        Me.MnuTransactions.Name = "MnuTransactions"
        Me.MnuTransactions.Size = New System.Drawing.Size(152, 22)
        Me.MnuTransactions.Text = "Transactions"
        '
        'MnuVoucherEntry
        '
        Me.MnuVoucherEntry.Name = "MnuVoucherEntry"
        Me.MnuVoucherEntry.Size = New System.Drawing.Size(208, 22)
        Me.MnuVoucherEntry.Text = "Voucher Entry"
        '
        'MnuBankReconsilationEntry
        '
        Me.MnuBankReconsilationEntry.Name = "MnuBankReconsilationEntry"
        Me.MnuBankReconsilationEntry.Size = New System.Drawing.Size(208, 22)
        Me.MnuBankReconsilationEntry.Text = "Bank Reconciliation Entry"
        '
        'MnuDisplay
        '
        Me.MnuDisplay.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuTrialBalance_Disp, Me.MnuDetailTrialBalance_Disp, Me.MnuProfitAndLoss_Disp, Me.MnuBalanceSheet_Disp, Me.MnuStockReport})
        Me.MnuDisplay.Name = "MnuDisplay"
        Me.MnuDisplay.Size = New System.Drawing.Size(152, 22)
        Me.MnuDisplay.Text = "Display"
        '
        'MnuTrialBalance_Disp
        '
        Me.MnuTrialBalance_Disp.Name = "MnuTrialBalance_Disp"
        Me.MnuTrialBalance_Disp.Size = New System.Drawing.Size(173, 22)
        Me.MnuTrialBalance_Disp.Text = "Trial Balance"
        '
        'MnuDetailTrialBalance_Disp
        '
        Me.MnuDetailTrialBalance_Disp.Name = "MnuDetailTrialBalance_Disp"
        Me.MnuDetailTrialBalance_Disp.Size = New System.Drawing.Size(173, 22)
        Me.MnuDetailTrialBalance_Disp.Text = "Detail Trial Balance"
        '
        'MnuProfitAndLoss_Disp
        '
        Me.MnuProfitAndLoss_Disp.Name = "MnuProfitAndLoss_Disp"
        Me.MnuProfitAndLoss_Disp.Size = New System.Drawing.Size(173, 22)
        Me.MnuProfitAndLoss_Disp.Text = "Profit And Loss"
        '
        'MnuBalanceSheet_Disp
        '
        Me.MnuBalanceSheet_Disp.Name = "MnuBalanceSheet_Disp"
        Me.MnuBalanceSheet_Disp.Size = New System.Drawing.Size(173, 22)
        Me.MnuBalanceSheet_Disp.Text = "Balance Sheet"
        '
        'MnuStockReport
        '
        Me.MnuStockReport.Name = "MnuStockReport"
        Me.MnuStockReport.Size = New System.Drawing.Size(173, 22)
        Me.MnuStockReport.Text = "Stock Report"
        '
        'MnuReports
        '
        Me.MnuReports.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuDailyTransactionSummary, Me.MnuDayBook, Me.MnuBankBook, Me.MnuCashBook, Me.MnuJournalBook, Me.MnuLedger, Me.MnuAccountGroupMergeLedger, Me.MnuTrialGroup, Me.MnuTrialDetail, Me.MnuTrialDetailDrCr, Me.MnuAnnexure, Me.MnuFundFlowStatement, Me.MnuCashFlowStatement, Me.MnuMonthlyExpenseChart, Me.MnuInterestCalculationForDebtors, Me.MnuInterestLedger, Me.MnuMonthyLedgerSummary, Me.MnuMonthlyLedgerSummaryFull})
        Me.MnuReports.Name = "MnuReports"
        Me.MnuReports.Size = New System.Drawing.Size(152, 22)
        Me.MnuReports.Text = "Reports I"
        '
        'MnuDailyTransactionSummary
        '
        Me.MnuDailyTransactionSummary.Name = "MnuDailyTransactionSummary"
        Me.MnuDailyTransactionSummary.Size = New System.Drawing.Size(238, 22)
        Me.MnuDailyTransactionSummary.Tag = ""
        Me.MnuDailyTransactionSummary.Text = "Daily Transaction Summary"
        '
        'MnuDayBook
        '
        Me.MnuDayBook.Name = "MnuDayBook"
        Me.MnuDayBook.Size = New System.Drawing.Size(238, 22)
        Me.MnuDayBook.Tag = ""
        Me.MnuDayBook.Text = "DayBook"
        '
        'MnuBankBook
        '
        Me.MnuBankBook.Name = "MnuBankBook"
        Me.MnuBankBook.Size = New System.Drawing.Size(238, 22)
        Me.MnuBankBook.Tag = ""
        Me.MnuBankBook.Text = "Bank Book"
        '
        'MnuCashBook
        '
        Me.MnuCashBook.Name = "MnuCashBook"
        Me.MnuCashBook.Size = New System.Drawing.Size(238, 22)
        Me.MnuCashBook.Tag = ""
        Me.MnuCashBook.Text = "Cash Book"
        '
        'MnuJournalBook
        '
        Me.MnuJournalBook.Name = "MnuJournalBook"
        Me.MnuJournalBook.Size = New System.Drawing.Size(238, 22)
        Me.MnuJournalBook.Tag = ""
        Me.MnuJournalBook.Text = "Journal Book"
        '
        'MnuLedger
        '
        Me.MnuLedger.Name = "MnuLedger"
        Me.MnuLedger.Size = New System.Drawing.Size(238, 22)
        Me.MnuLedger.Tag = ""
        Me.MnuLedger.Text = "Ledger"
        '
        'MnuAccountGroupMergeLedger
        '
        Me.MnuAccountGroupMergeLedger.Name = "MnuAccountGroupMergeLedger"
        Me.MnuAccountGroupMergeLedger.Size = New System.Drawing.Size(238, 22)
        Me.MnuAccountGroupMergeLedger.Tag = ""
        Me.MnuAccountGroupMergeLedger.Text = "Account Group Merge Ledger"
        '
        'MnuTrialGroup
        '
        Me.MnuTrialGroup.Name = "MnuTrialGroup"
        Me.MnuTrialGroup.Size = New System.Drawing.Size(238, 22)
        Me.MnuTrialGroup.Tag = ""
        Me.MnuTrialGroup.Text = "Trial Group"
        '
        'MnuTrialDetail
        '
        Me.MnuTrialDetail.Name = "MnuTrialDetail"
        Me.MnuTrialDetail.Size = New System.Drawing.Size(238, 22)
        Me.MnuTrialDetail.Text = "Trial Detail"
        '
        'MnuTrialDetailDrCr
        '
        Me.MnuTrialDetailDrCr.Name = "MnuTrialDetailDrCr"
        Me.MnuTrialDetailDrCr.Size = New System.Drawing.Size(238, 22)
        Me.MnuTrialDetailDrCr.Text = "Trial Detail (Dr/Cr)"
        '
        'MnuAnnexure
        '
        Me.MnuAnnexure.Name = "MnuAnnexure"
        Me.MnuAnnexure.Size = New System.Drawing.Size(238, 22)
        Me.MnuAnnexure.Tag = ""
        Me.MnuAnnexure.Text = "Annexure"
        '
        'MnuMonthlyLedgerSummaryFull
        '
        Me.MnuMonthlyLedgerSummaryFull.Name = "MnuMonthlyLedgerSummaryFull"
        Me.MnuMonthlyLedgerSummaryFull.Size = New System.Drawing.Size(238, 22)
        Me.MnuMonthlyLedgerSummaryFull.Tag = ""
        Me.MnuMonthlyLedgerSummaryFull.Text = "Monthly Ledger Summary Full"
        '
        'MnuReportsII
        '
        Me.MnuReportsII.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuAgeingAnalysisFIFO, Me.MnuAgeingAnalysisBillWise, Me.MnuAccountGroupWiseAgeingAnalysis, Me.MnuBillWiseOutstandingDebtors, Me.MnuBillWiseOutstandingCreditors, Me.MnuBillWiseAdjustmentRegister, Me.MnuOutstandingCreditorsFIFO, Me.MnuOutstandinDebtorsFIFO, Me.MnuStockValuation, Me.MnuDailyCollectionRegister, Me.MnuDailyExpenseRegister})
        Me.MnuReportsII.Name = "MnuReportsII"
        Me.MnuReportsII.Size = New System.Drawing.Size(152, 22)
        Me.MnuReportsII.Text = "Reports II"
        '
        'SSrpMain
        '
        Me.SSrpMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel1, Me.ToolStripStatusLabel2})
        Me.SSrpMain.Location = New System.Drawing.Point(23, 386)
        Me.SSrpMain.Name = "SSrpMain"
        Me.SSrpMain.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional
        Me.SSrpMain.Size = New System.Drawing.Size(845, 22)
        Me.SSrpMain.TabIndex = 7
        Me.SSrpMain.Text = "StatusStrip1"
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(94, 17)
        Me.ToolStripStatusLabel1.Text = "Company Name"
        '
        'ToolStripStatusLabel2
        '
        Me.ToolStripStatusLabel2.Name = "ToolStripStatusLabel2"
        Me.ToolStripStatusLabel2.Size = New System.Drawing.Size(65, 17)
        Me.ToolStripStatusLabel2.Text = "User Name"
        '
        'TbcMain
        '
        Me.TbcMain.Alignment = System.Windows.Forms.TabAlignment.Left
        Me.TbcMain.Controls.Add(Me.Tbp1)
        Me.TbcMain.Dock = System.Windows.Forms.DockStyle.Left
        Me.TbcMain.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TbcMain.ItemSize = New System.Drawing.Size(100, 20)
        Me.TbcMain.Location = New System.Drawing.Point(0, 24)
        Me.TbcMain.Multiline = True
        Me.TbcMain.Name = "TbcMain"
        Me.TbcMain.SelectedIndex = 0
        Me.TbcMain.Size = New System.Drawing.Size(23, 384)
        Me.TbcMain.SizeMode = System.Windows.Forms.TabSizeMode.Fixed
        Me.TbcMain.TabIndex = 6
        '
        'Tbp1
        '
        Me.Tbp1.AutoScroll = True
        Me.Tbp1.Font = New System.Drawing.Font("Arial", 27.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Tbp1.ForeColor = System.Drawing.Color.Black
        Me.Tbp1.Location = New System.Drawing.Point(24, 4)
        Me.Tbp1.Name = "Tbp1"
        Me.Tbp1.Padding = New System.Windows.Forms.Padding(3)
        Me.Tbp1.Size = New System.Drawing.Size(0, 376)
        Me.Tbp1.TabIndex = 1
        Me.Tbp1.Text = "Menu"
        Me.Tbp1.UseVisualStyleBackColor = True
        '
        'MnuAgeingAnalysisBillWise
        '
        Me.MnuAgeingAnalysisBillWise.Name = "MnuAgeingAnalysisBillWise"
        Me.MnuAgeingAnalysisBillWise.Size = New System.Drawing.Size(270, 22)
        Me.MnuAgeingAnalysisBillWise.Text = "Ageing Analysis Bill Wise"
        '
        'MnuBillWiseAdjustmentRegister
        '
        Me.MnuBillWiseAdjustmentRegister.Name = "MnuBillWiseAdjustmentRegister"
        Me.MnuBillWiseAdjustmentRegister.Size = New System.Drawing.Size(270, 22)
        Me.MnuBillWiseAdjustmentRegister.Text = "Bill Wise Adjustment Register"
        '
        'MnuAccountGroupWiseAgeingAnalysis
        '
        Me.MnuAccountGroupWiseAgeingAnalysis.Name = "MnuAccountGroupWiseAgeingAnalysis"
        Me.MnuAccountGroupWiseAgeingAnalysis.Size = New System.Drawing.Size(270, 22)
        Me.MnuAccountGroupWiseAgeingAnalysis.Text = "Account Group Wise Ageing Analysis"
        '
        'MnuStockValuation
        '
        Me.MnuStockValuation.Name = "MnuStockValuation"
        Me.MnuStockValuation.Size = New System.Drawing.Size(270, 22)
        Me.MnuStockValuation.Text = "Stock Valuation"
        '
        'MnuCashFlowStatement
        '
        Me.MnuCashFlowStatement.Name = "MnuCashFlowStatement"
        Me.MnuCashFlowStatement.Size = New System.Drawing.Size(240, 22)
        Me.MnuCashFlowStatement.Text = "Cash Flow Statement"
        '
        'MnuFundFlowStatement
        '
        Me.MnuFundFlowStatement.Name = "MnuFundFlowStatement"
        Me.MnuFundFlowStatement.Size = New System.Drawing.Size(240, 22)
        Me.MnuFundFlowStatement.Text = "Fund Flow Statement"
        '
        'MnuAgeingAnalysisFIFO
        '
        Me.MnuAgeingAnalysisFIFO.Name = "MnuAgeingAnalysisFIFO"
        Me.MnuAgeingAnalysisFIFO.Size = New System.Drawing.Size(270, 22)
        Me.MnuAgeingAnalysisFIFO.Text = "Ageing Analysis FIFO"
        '
        'MnuBillWiseOutstandingCreditors
        '
        Me.MnuBillWiseOutstandingCreditors.Name = "MnuBillWiseOutstandingCreditors"
        Me.MnuBillWiseOutstandingCreditors.Size = New System.Drawing.Size(270, 22)
        Me.MnuBillWiseOutstandingCreditors.Text = "Bill Wise Outstanding Creditors"
        '
        'MnuBillWiseOutstandingDebtors
        '
        Me.MnuBillWiseOutstandingDebtors.Name = "MnuBillWiseOutstandingDebtors"
        Me.MnuBillWiseOutstandingDebtors.Size = New System.Drawing.Size(270, 22)
        Me.MnuBillWiseOutstandingDebtors.Text = "Bill Wise Outstanding Debtors"
        '
        'MnuOutstandinDebtorsFIFO
        '
        Me.MnuOutstandinDebtorsFIFO.Name = "MnuOutstandinDebtorsFIFO"
        Me.MnuOutstandinDebtorsFIFO.Size = New System.Drawing.Size(270, 22)
        Me.MnuOutstandinDebtorsFIFO.Tag = ""
        Me.MnuOutstandinDebtorsFIFO.Text = "Outstandin Debtors FIFO"
        '
        'MnuOutstandingCreditorsFIFO
        '
        Me.MnuOutstandingCreditorsFIFO.Name = "MnuOutstandingCreditorsFIFO"
        Me.MnuOutstandingCreditorsFIFO.Size = New System.Drawing.Size(270, 22)
        Me.MnuOutstandingCreditorsFIFO.Text = "Outstanding Creditors FIFO"
        '
        'MnuDailyCollectionRegister
        '
        Me.MnuDailyCollectionRegister.Name = "MnuDailyCollectionRegister"
        Me.MnuDailyCollectionRegister.Size = New System.Drawing.Size(270, 22)
        Me.MnuDailyCollectionRegister.Text = "Daily Collection Register"
        '
        'MnuInterestLedger
        '
        Me.MnuInterestLedger.Name = "MnuInterestLedger"
        Me.MnuInterestLedger.Size = New System.Drawing.Size(240, 22)
        Me.MnuInterestLedger.Text = "Interest Ledger"
        '
        'MnuMonthyLedgerSummary
        '
        Me.MnuMonthyLedgerSummary.Name = "MnuMonthyLedgerSummary"
        Me.MnuMonthyLedgerSummary.Size = New System.Drawing.Size(240, 22)
        Me.MnuMonthyLedgerSummary.Text = "Monthy Ledger Summary"
        '
        'MnuInterestCalculationForDebtors
        '
        Me.MnuInterestCalculationForDebtors.Name = "MnuInterestCalculationForDebtors"
        Me.MnuInterestCalculationForDebtors.Size = New System.Drawing.Size(240, 22)
        Me.MnuInterestCalculationForDebtors.Text = "Interest Calculation For Debtors"
        '
        'MnuMonthlyExpenseChart
        '
        Me.MnuMonthlyExpenseChart.Name = "MnuMonthlyExpenseChart"
        Me.MnuMonthlyExpenseChart.Size = New System.Drawing.Size(240, 22)
        Me.MnuMonthlyExpenseChart.Text = "Monthly Expense Chart"
        '
        'MnuDailyExpenseRegister
        '
        Me.MnuDailyExpenseRegister.Name = "MnuDailyExpenseRegister"
        Me.MnuDailyExpenseRegister.Size = New System.Drawing.Size(270, 22)
        Me.MnuDailyExpenseRegister.Text = "Daily Expense Register"
        '
        'MDIMain1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.ClientSize = New System.Drawing.Size(868, 408)
        Me.Controls.Add(Me.SSrpMain)
        Me.Controls.Add(Me.TbcMain)
        Me.Controls.Add(Me.MnuMain)
        Me.IsMdiContainer = True
        Me.MainMenuStrip = Me.MnuMain
        Me.Name = "MDIMain1"
        Me.Text = "Accounts"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.MnuMain.ResumeLayout(False)
        Me.MnuMain.PerformLayout()
        Me.SSrpMain.ResumeLayout(False)
        Me.SSrpMain.PerformLayout()
        Me.TbcMain.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MnuMain As System.Windows.Forms.MenuStrip
    Friend WithEvents SSrpMain As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel2 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents TbcMain As System.Windows.Forms.TabControl
    Friend WithEvents Tbp1 As System.Windows.Forms.TabPage
    Friend WithEvents AccountsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuMaster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuNarrationMaster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuDefineCostCenter As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuAccountGroup As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuAccountMaster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuTransactions As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuVoucherEntry As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuBankReconsilationEntry As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuDisplay As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuTrialBalance_Disp As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuDetailTrialBalance_Disp As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuProfitAndLoss_Disp As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuBalanceSheet_Disp As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuStockReport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuReports As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuDailyTransactionSummary As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuDayBook As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuBankBook As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuCashBook As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuJournalBook As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuLedger As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuAccountGroupMergeLedger As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuTrialGroup As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuTrialDetail As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuTrialDetailDrCr As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuAnnexure As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuLedgerGroup As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuMonthlyLedgerSummaryFull As ToolStripMenuItem
    Friend WithEvents MnuReportsII As ToolStripMenuItem
    Friend WithEvents MnuAgeingAnalysisBillWise As ToolStripMenuItem
    Friend WithEvents MnuBillWiseAdjustmentRegister As ToolStripMenuItem
    Friend WithEvents MnuAccountGroupWiseAgeingAnalysis As ToolStripMenuItem
    Friend WithEvents MnuStockValuation As ToolStripMenuItem
    Friend WithEvents MnuCashFlowStatement As ToolStripMenuItem
    Friend WithEvents MnuFundFlowStatement As ToolStripMenuItem
    Friend WithEvents MnuInterestLedger As ToolStripMenuItem
    Friend WithEvents MnuMonthyLedgerSummary As ToolStripMenuItem
    Friend WithEvents MnuInterestCalculationForDebtors As ToolStripMenuItem
    Friend WithEvents MnuMonthlyExpenseChart As ToolStripMenuItem
    Friend WithEvents MnuAgeingAnalysisFIFO As ToolStripMenuItem
    Friend WithEvents MnuBillWiseOutstandingDebtors As ToolStripMenuItem
    Friend WithEvents MnuBillWiseOutstandingCreditors As ToolStripMenuItem
    Friend WithEvents MnuOutstandingCreditorsFIFO As ToolStripMenuItem
    Friend WithEvents MnuOutstandinDebtorsFIFO As ToolStripMenuItem
    Friend WithEvents MnuDailyCollectionRegister As ToolStripMenuItem
    Friend WithEvents MnuDailyExpenseRegister As ToolStripMenuItem
End Class
