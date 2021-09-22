Public Class ClsFunction
    Public Function FOpen(ByVal StrSender As String, ByVal StrSenderText As String)
        Dim FrmObj As Form = Nothing
        Dim StrUserPermission As String
        Dim DTUP As New DataTable
        Dim ADMain As SqlClient.SqlDataAdapter
        Dim MDI As New MDIMain


        'For User Permission Open
        'StrUserPermission = "****"
        StrUserPermission = "AEDP"
        'ADMain = New OleDb.OleDbDataAdapter("Select Permission From User_Permission Where UserName='" & AgL.PubUserName & "' And MnuModule='" & StrModuleName & "' And MnuName='" & StrSender & "'", AgL.GCn.ConnectionString)

        ADMain = New SqlClient.SqlDataAdapter("Select Permission From User_Permission Where UserName='" & AgL.PubUserName & "' And MnuModule='" & StrModuleName & "' And MnuName='" & StrSender & "'", AgL.GCn)
        ADMain.Fill(DTUP)
        If DTUP.Rows.Count > 0 Then
            StrUserPermission = AgL.XNull(DTUP.Rows(0).Item("Permission"))
        End If
        DTUP.Clear()
        DTUP = Nothing

        DTUP = New DataTable
        'ADMain = New OleDb.OleDbDataAdapter("Select GroupText As UP From User_Control_Permission Where UserName='" & AgL.PubUserName & "' And MnuModule='" & StrModuleName & "' And MnuName='" & StrSender & "'", AgL.GCn.ConnectionString)
        ADMain = New SqlClient.SqlDataAdapter("Select GroupText As UP From User_Control_Permission Where UserName='" & AgL.PubUserName & "' And MnuModule='" & StrModuleName & "' And MnuName='" & StrSender & "'", AgL.GCn)
        ADMain.Fill(DTUP)
        'For User Permission End 

        Select Case StrSender
            Case MDI.MnuOpeningBalanceEntry.Name
                FrmObj = New FrmFaOpening(StrUserPermission, DTUP)
            Case MDI.MnuCityMaster.Name
                FrmObj = New FrmCityMast(StrUserPermission, DTUP)
            Case MDI.MnuCountryMaster.Name
                FrmObj = New frmCountryMast(StrUserPermission, DTUP)
            Case MDI.MnuAssetTransaction.Name
                FrmObj = New FrmAssetTransaction(StrUserPermission, DTUP)
            Case MDI.MnuAssetMaster.Name
                FrmObj = New FrmAssetMaster(StrUserPermission, DTUP)
            Case MDI.MnuAssetGroup.Name
                FrmObj = New FrmAssetGroupMaster(StrUserPermission, DTUP)
            Case MDI.MnuCurrencyMaster.Name
                FrmObj = New frmCurrencyMast(StrUserPermission, DTUP)
            Case MDI.MnuCurrencyRateEntry.Name
                FrmObj = New FrmCurrencyRate()
            Case MDI.MnuSmartFinder.Name
                FrmObj = New FrmSmartFinder()
            Case MDI.MnuStockRateUpdation.Name
                FrmObj = New FrmAdjustingRates()
            Case MDI.MnuBudgetAnalysis.Name
                FrmObj = New FrmBudgetAnalysis()
            Case MDI.MnuBudgetSchedule.Name
                FrmObj = New FrmBudget(StrUserPermission, DTUP)
            Case MDI.MnuZoneMaster.Name
                FrmObj = New FrmSingleFieldMaster(MDI, AgL, StrSenderText, "ZoneMast", "Zone Master", "Code", "Name", 50, StrUserPermission, DTUP, False, False, AgL.PubReportPath)
                AgL.PubReportTitle = "Zone Master"
            Case MDI.MnuChequeMaster.Name
                FrmObj = New FrmChequeMast(StrUserPermission, DTUP)
            Case MDI.MnuLedgerGroup.Name
                FrmObj = New FrmSingleFieldMaster(MDI, AgL, StrSenderText, "LedgerGroup", "Ledger Group", "Code", "Name", 100, StrUserPermission, DTUP, False, False, AgL.PubReportPath)
                AgL.PubReportTitle = "Ledger Group"
            Case MDI.MnuDefineCostCenter.Name
                FrmObj = New FrmSingleFieldMaster(MDI, AgL, StrSenderText, "CostCenterMast", "Cost Center", "Code", "Name", 30, StrUserPermission, DTUP, False, False, AgL.PubReportPath)
                AgL.PubReportTitle = "Define Cost Center"
            Case MDI.MnuEnvironmentSetting.Name
                FrmObj = New FrmEnviro()
            Case MDI.MnuChequePrintSetup.Name
                FrmObj = New FrmChequePrintSetup(StrUserPermission, DTUP)
            Case MDI.MnuChequePrinting.Name
                FrmObj = New FrmChequePrinting()
            Case MDI.MnuBillWiseOutstandingCreditors.Name
                FrmObj = New FrmReportLayout("BillWsOS_Cr", MDI.MnuBillWiseOutstandingCreditors.Text, 6)
            Case MDI.MnuBillWiseOutstandingDebtors.Name
                FrmObj = New FrmReportLayout("BillWsOS_Dr", MDI.MnuBillWiseOutstandingDebtors.Text, 6)
            Case MDI.MnuAgeingAnalysisFIFO.Name
                FrmObj = New FrmReportLayout("Ageing", MDI.MnuAgeingAnalysisFIFO.Text, 11)
            Case MDI.MnuBankReconsilationEntry.Name
                FrmObj = New FrmBankReconciliation(StrUserPermission, DTUP)
            Case MDI.MnuAccountMaster.Name
                FrmObj = New FrmAccountMaster(StrUserPermission, DTUP)
            Case MDI.MnuTDSCategoryMaster.Name
                FrmObj = New frmTdsCatMaster(StrUserPermission, DTUP)
            Case MDI.MnuTrialBalance_Disp.Name
                FrmObj = New FrmDisplayHierarchy
                CType(FrmObj, FrmDisplayHierarchy).FForward(0, 0, ClsStructure.DisplayType.TrailBalance)
            Case MDI.MnuDetailTrialBalance_Disp.Name
                FrmObj = New FrmDisplayHierarchy
                CType(FrmObj, FrmDisplayHierarchy).FForward(0, 0, ClsStructure.DisplayType.DTrailBalance)
            Case MDI.MnuStockReport.Name
                FrmObj = New FrmDisplayHierarchy_Stock
                CType(FrmObj, FrmDisplayHierarchy_Stock).FForward(0, 0)
            Case MDI.MnuLedger.Name
                FrmObj = New FrmReportLayout("Ledger", MDI.MnuLedger.Text, 8)
            Case MDI.MnuTrialGroup.Name
                FrmObj = New FrmReportLayout("TrialGroup", MDI.MnuTrialGroup.Text, 2)
            Case MDI.MnuTrialDetail.Name
                FrmObj = New FrmReportLayout("TrialDetail", MDI.MnuTrialDetail.Text, 4)
            Case MDI.MnuProfitAndLoss_Disp.Name
                FrmObj = New FrmDisplayHierarchy
                CType(FrmObj, FrmDisplayHierarchy).FForward(0, 0, ClsStructure.DisplayType.ProfitAndLoss)
            Case MDI.MnuBalanceSheet_Disp.Name
                FrmObj = New FrmDisplayHierarchy
                CType(FrmObj, FrmDisplayHierarchy).FForward(0, 0, ClsStructure.DisplayType.BalanceSheet)
            Case MDI.MnuVoucherEntry.Name
                FrmObj = New FrmVoucherEntry(StrUserPermission, DTUP, ClsStructure.EntryType.ForEntry)
                'Case MDI.MnuVoucherEntryPost.Name
                '    FrmObj = New FrmVoucherEntry(DTUP, ClsStructure.EntryType.ForPosting)
            Case MDI.MnuServiceTaxEntry.Name
                'FrmObj = New FrmServiceTax(StrUserPermission, DTUP)
            Case MDI.MnuAccountGroup.Name
                FrmObj = New FrmAcGroupMaster(StrUserPermission, DTUP)
            Case MDI.MnuTDSCategoryDescriptionMaster.Name
                FrmObj = New FrmSingleFieldMaster(MDI, AgL, StrSenderText, "TDSCat_Description", "TDS Description", "Code", "Name", 25, StrUserPermission, DTUP, False, False, AgL.PubReportPath)
                AgL.PubReportTitle = "TDS Category Description Master"
            Case MDI.MnuNarrationMaster.Name
                FrmObj = New FrmSingleFieldMaster(MDI, AgL, StrSenderText, "NarrationMast", "Narration", "Code", "Name", 100, StrUserPermission, DTUP, False, False, AgL.PubReportPath)
                AgL.PubReportTitle = "Narration Master"
            Case MDI.MnuAnnexure.Name
                FrmObj = New FrmReportLayout("Annexure", MDI.MnuAnnexure.Text, 3)
            Case MDI.MnuCashBook.Name
                FrmObj = New FrmReportLayout("CashBook", MDI.MnuCashBook.Text, 7)
            Case MDI.MnuBankBook.Name
                FrmObj = New FrmReportLayout("BankBook", MDI.MnuBankBook.Text, 7)
            Case MDI.MnuJournalBook.Name
                FrmObj = New FrmReportLayout("Journal", MDI.MnuJournalBook.Text, 4)
            Case MDI.MnuDayBook.Name
                FrmObj = New FrmReportLayout("DayBook", MDI.MnuDayBook.Text, 4)
            Case MDI.MnuCashFlowStatement.Name
                FrmObj = New FrmReportLayout("CashFlow", MDI.MnuCashFlowStatement.Text, 3)
            Case MDI.MnuFundFlowStatement.Name
                FrmObj = New FrmReportLayout("FundFlow", MDI.MnuFundFlowStatement.Text, 3)
            Case MDI.MnuMonthlyExpenseChart.Name
                FrmObj = New FrmReportLayout("MonthlyExpenses", MDI.MnuMonthlyExpenseChart.Text, 3)
            Case MDI.MnuAssetRegister.Name
                FrmObj = New FrmReportLayout("FixedAssetRegister", MDI.MnuAssetRegister.Text, 3)
            Case MDI.MnuFBTReport.Name
                FrmObj = New FrmReportLayout("FBTReport", MDI.MnuFBTReport.Text, 5)
            Case MDI.MnuTDSReport.Name
                FrmObj = New FrmReportLayout("TDSCategoryWiseReport", MDI.MnuTDSReport.Text, 7)
            Case MDI.MnuPartyWiseTDS.Name
                FrmObj = New FrmReportLayout("PartyWiseTDSReport", MDI.MnuPartyWiseTDS.Text, 8)
            Case MDI.MnuInterestLedger.Name
                FrmObj = New FrmReportLayout("InterestLedger", MDI.MnuInterestLedger.Text, 7)
            Case MDI.MnuMonthyLedgerSummary.Name
                FrmObj = New FrmReportLayout("MonthlyLedgerSummary", MDI.MnuMonthyLedgerSummary.Text, 3)
            Case MDI.MnuTrialDetailDrCr.Name
                FrmObj = New FrmReportLayout("TrialDetailDrCr", MDI.MnuTrialDetailDrCr.Text, 5)
            Case MDI.MnuMonthlyLedgerSummaryFull.Name
                FrmObj = New FrmReportLayout("MonthlyLedgerSummaryFull", MDI.MnuMonthlyLedgerSummaryFull.Text, 4)
            Case MDI.MnuDailyTransactionSummary.Name
                FrmObj = New FrmReportLayout("DailyTransBook", MDI.MnuDailyTransactionSummary.Text, 5)
            Case MDI.MnuOutstandinDebtorsFIFO.Name
                FrmObj = New FrmReportLayout("FIFOWsOS_Dr", MDI.MnuOutstandinDebtorsFIFO.Text, 5)
            Case MDI.MnuOutstandingCreditorsFIFO.Name
                FrmObj = New FrmReportLayout("FIFOWsOS_Cr", MDI.MnuOutstandingCreditorsFIFO.Text, 5)
            Case MDI.MnuStockValuation.Name
                FrmObj = New FrmReportLayout("Stock_Valuation", MDI.MnuStockValuation.Text, 8)
            Case MDI.MnuDailyCollectionRegister.Name
                FrmObj = New FrmReportLayout("DailyCollectionRegister", MDI.MnuDailyCollectionRegister.Text, 4)
            Case MDI.MnuDailyExpenseRegister.Name
                FrmObj = New FrmReportLayout("DailyExpenseRegister", MDI.MnuDailyExpenseRegister.Text, 4)
            Case MDI.MnuLedgerGroupMergeLedger.Name
                FrmObj = New FrmReportLayout("LedgerGrMergeLedger", MDI.MnuLedgerGroupMergeLedger.Text, 6)
            Case MDI.MnuAccountGroupMergeLedger.Name
                FrmObj = New FrmReportLayout("AccountGrMergeLedger", MDI.MnuAccountGroupMergeLedger.Text, 6)
            Case MDI.MnuGTANonGTARegister.Name
                FrmObj = New FrmReportLayout("GTAReg", MDI.MnuGTANonGTARegister.Text, 6)
            Case MDI.MnuAgeingAnalysisBillWise.Name
                FrmObj = New FrmReportLayout("BillWsOSAgeing", MDI.MnuAgeingAnalysisBillWise.Text, 5)
            Case MDI.MnuBillWiseAdjustmentRegister.Name
                FrmObj = New FrmReportLayout("BillWiseAdj", MDI.MnuBillWiseAdjustmentRegister.Text, 5)
            Case MDI.MnuTDSCertificate.Name
                FrmObj = New FrmReportLayout("TDSTaxChallan", MDI.MnuTDSCertificate.Text, 4)
            Case MDI.MnuAccountGroupWiseAgeingAnalysis.Name
                FrmObj = New FrmReportLayout("AccountGrpWsOSAgeing", MDI.MnuAccountGroupWiseAgeingAnalysis.Text, 8)
            Case MDI.MnuInterestCalculationForDebtors.Name
                FrmObj = New FrmReportLayout("IntCalForDebtors", MDI.MnuInterestCalculationForDebtors.Text, 5)
            Case MDI.MnuSalesTaxClubbing.Name
                FrmObj = New FrmReportLayout("SalesTaxClubbing", MDI.MnuSalesTaxClubbing.Text, 5)

            Case Else
                FrmObj = Nothing
        End Select
        Return FrmObj
    End Function
End Class
