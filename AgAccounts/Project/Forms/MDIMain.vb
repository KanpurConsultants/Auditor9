Public Class MDIMain
    Private Sub MDIMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If AgL Is Nothing Then
            AgL = New AgLibrary.ClsMain()


            If FOpenIni(StrPath, AgLibrary.ClsConstant.PubSuperUserName, AgLibrary.ClsConstant.PubSuperUserPassword) Then
                AgIniVar.FOpenConnection("6", "1")
            End If

            AgIniVar.FOpenConnection("6", "1")
            AgL.PubDivCode = "D"
        End If
    End Sub

    Private Sub MnuLedger_Click(ByVal sender As Object, ByVal e As System.EventArgs) _
    Handles MnuLedger.Click, MnuTrialGroup.Click, MnuBalanceSheet_Disp.Click, _
    MnuTrialBalance_Disp.Click, MnuProfitAndLoss_Disp.Click, MnuEnvironmentSetting.Click, _
    MnuCashBook.Click, MnuBankBook.Click, MnuAnnexure.Click, MnuCurrencyMaster.Click, _
    MnuDayBook.Click, MnuJournalBook.Click, MnuAgeingAnalysisFIFO.Click, _
    MnuBillWiseOutstandingCreditors.Click, MnuBillWiseOutstandingDebtors.Click, _
    MnuAccountGroup.Click, MnuAccountMaster.Click, MnuTDSCategoryDescriptionMaster.Click, _
    MnuTDSCategoryMaster.Click, MnuVoucherEntry.Click, MnuBankReconsilationEntry.Click, _
    MnuVoucherEntryPost.Click, MnuDefineCostCenter.Click, MnuBudgetSchedule.Click, _
    MnuBudgetAnalysis.Click, MnuCityMaster.Click, MnuCountryMaster.Click, _
    MnuCurrencyRateEntry.Click, MnuAssetGroup.Click, MnuAssetMaster.Click, _
    MnuAssetTransaction.Click, MnuAssetRegister.Click, MnuCashFlowStatement.Click, _
    MnuFundFlowStatement.Click, MnuMonthlyExpenseChart.Click, MnuTrialDetail.Click, _
    MnuDetailTrialBalance_Disp.Click, MnuFBTReport.Click, MnuTDSReport.Click, _
    MnuPartyWiseTDS.Click, MnuInterestLedger.Click, MnuMonthyLedgerSummary.Click, _
    MnuTrialDetailDrCr.Click, MnuMonthlyLedgerSummaryFull.Click, MnuDailyTransactionSummary.Click, _
    MnuLedgerGroup.Click, MnuNarrationMaster.Click, MnuZoneMaster.Click, MnuOutstandinDebtorsFIFO.Click, _
    MnuOutstandingCreditorsFIFO.Click, MnuAgeingAnalysisBillWise.Click, _
    MnuDailyCollectionRegister.Click, MnuLedgerGroupMergeLedger.Click, _
    MnuDailyExpenseRegister.Click, MnuSmartFinder.Click, MnuAccountGroupMergeLedger.Click, _
    MnuChequeMaster.Click, MnuServiceTaxEntry.Click, MnuGTANonGTARegister.Click, _
    MnuBillWiseAdjustmentRegister.Click, MnuTDSCertificate.Click, _
    MnuAccountGroupWiseAgeingAnalysis.Click, MnuStockValuation.Click, MnuStockRateUpdation.Click, _
    MnuInterestCalculationForDebtors.Click, MnuStockReport.Click, MnuChequePrinting.Click, MnuChequePrintSetup.Click

        Dim FrmObj As Form = Nothing
        Dim CFOpen As New ClsFunction

        FrmObj = CFOpen.FOpen(sender.name, sender.Text)
        If FrmObj IsNot Nothing Then
            FrmObj.MdiParent = Me
            FrmObj.Show()
            FrmObj = Nothing
        End If
    End Sub

End Class
