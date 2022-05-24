Imports AgLibrary.ClsMain.agConstants
Public Class ClsFunction
    Dim WithEvents ObjRepFormGlobal As AgLibrary.RepFormGlobal
    'Dim WithEvents ReportFrm As Aglibrary.FrmReportLayout
    Dim WithEvents ReportFrm As AgLibrary.FrmReportLayout
    Dim WithEvents GridReportFrm As AgLibrary.FrmRepDisplay
    Dim CRepProc As ClsReportProcedures
    Dim CRep As ClsReports
    Public Function FOpen(ByVal StrSender As String, ByVal StrSenderText As String, Optional ByVal mTargetEntryType As TargetEntryType = TargetEntryType.EntryPoint, Optional ByVal StrSenderModule As String = "")
        Dim FrmObj As Form
        Dim StrUserPermission As String
        Dim DTUP As New DataTable
        Dim ADMain As OleDb.OleDbDataAdapter = Nothing

        Dim MDI As New MDIMain
        Dim MdiCheque As New MDICheque
        Dim MdiSchool As New MDISchool
        Dim MdiSpare As New MdiSpare
        Dim MdiKirana As New MdiKirana




        Dim Cls_Accounts As New AgAccounts.ClsMain(AgL)

        PubStopwatchStartValue = AgL.PubStopWatch.ElapsedMilliseconds
        'For User Permission Open
        If StrSenderModule <> "" Then
            StrUserPermission = AgIniVar.FunGetUserPermission(StrSenderModule, StrSender, StrSenderText, DTUP)
        Else
            StrUserPermission = AgIniVar.FunGetUserPermission(ClsMain.ModuleName, StrSender, StrSenderText, DTUP)
        End If
        Debug.Print("Fetching Settings For Entry. " & (AgL.PubStopWatch.ElapsedMilliseconds - PubStopwatchStartValue).ToString)
        ''For User Permission End 

        'If AgL.StrCmp(StrReferenceModule, "Accounts") Then

        '    Dim objAccountsClsFunction As New AgAccounts.ClsFunction
        '    FrmObj = objAccountsClsFunction.FOpen(StrSender, StrSenderText, mTargetEntryType)
        '    Return FrmObj
        'End If


        If mTargetEntryType = TargetEntryType.EntryPoint Then
            Select Case StrSender


'#Region "Carpet Industry Menus"
'                Case MDI.MnuWeavingOrder.Name
'                    FrmObj = New FrmPurchInvoiceDirect_WithDimension(StrUserPermission, DTUP, ClsCarpet.NCat_WeavingOrder)
'                Case MDI.MnuWeavingReceive.Name
'                    FrmObj = New FrmWeavingReceive(StrUserPermission, DTUP, ClsCarpet.NCat_WeavingReceive)
'                Case MDI.MnuWeavingInvoice.Name
'                    FrmObj = New FrmPurchInvoiceDirect_WithDimension(StrUserPermission, DTUP, ClsCarpet.NCat_WeavingInvoice)

'                Case MDI.MnuDyeingOrder.Name
'                    FrmObj = New FrmPurchInvoiceDirect_WithDimension(StrUserPermission, DTUP, ClsCarpet.NCat_DyeingOrder)
'                Case MDI.MnuDyeingReceive.Name
'                    FrmObj = New FrmPurchInvoiceDirect_WithDimension(StrUserPermission, DTUP, ClsCarpet.NCat_DyeingReceive)
'                Case MDI.MnuDyeingInvoice.Name
'                    FrmObj = New FrmPurchInvoiceDirect_WithDimension(StrUserPermission, DTUP, ClsCarpet.NCat_DyeingInvoice)

'                Case MDI.MnuFinishingOrder.Name
'                    FrmObj = New FrmPurchInvoiceDirect_WithDimension(StrUserPermission, DTUP, ClsCarpet.NCat_FinishingOrder)
'                Case MDI.MnuFinishingReceive.Name
'                    FrmObj = New FrmPurchInvoiceDirect_WithDimension(StrUserPermission, DTUP, ClsCarpet.NCat_FinishingReceive)
'                Case MDI.MnuFinishingInvoice.Name
'                    FrmObj = New FrmPurchInvoiceDirect_WithDimension(StrUserPermission, DTUP, ClsCarpet.NCat_FinishingInvoice)
'#End Region

                Case MdiKirana.MnuReceiptEntry_Kirana.Name
                    FrmObj = New FrmPaymentReceiptSettlement_Kirana(StrUserPermission, DTUP, Ncat.Receipt)
                Case MdiKirana.MnuPaymentEntry_Kirana.Name
                    FrmObj = New FrmPaymentReceiptSettlement_Kirana(StrUserPermission, DTUP, Ncat.Payment)


                Case MDI.MnuRecheckBills.Name
                    FrmObj = New FrmRecelculateSales()

                Case MDI.MnuMakeDataBlank.Name
                    FrmObj = New FrmBlankData()


                Case MDI.MnuChequePrinting.Name, MdiCheque.MnuChequePrintCheque.Name
                    FrmObj = New FrmChequeData(StrUserPermission, DTUP)
                Case MDI.MnuBank.Name, MdiCheque.MnuChequeBankMaster.Name
                    FrmObj = New FrmChequeUI(StrUserPermission, DTUP)
                Case MDI.MnuPrintCheque.Name
                    FrmObj = New frmPrintCheque()
                Case MDI.MnuBankInfo.Name
                    FrmObj = New frmBankInfo()
                Case MDI.MnuBarcodeHistory.Name
                    FrmObj = New FrmBarcodeHistory()
                Case MDI.MnuBulkEmail.Name
                    FrmObj = New FrmMailBulk()
                Case MDI.MnuSplitLedgerOpening.Name
                    FrmObj = New FrmSplitLedgerOpening()
                Case MDI.MnuOpeningTransfer.Name
                    FrmObj = New FrmOpeningTransfer()
                Case MDI.MnuJobOrder.Name
                    FrmObj = New FrmPurchInvoiceDirect_WithDimension(StrUserPermission, DTUP, Ncat.JobOrder)
                Case MDI.MnuJobReceive.Name
                    FrmObj = New FrmPurchInvoiceDirect_WithDimension(StrUserPermission, DTUP, Ncat.JobReceive)
                Case MDI.MnuJobInvoice.Name
                    FrmObj = New FrmPurchInvoiceDirect_WithDimension(StrUserPermission, DTUP, Ncat.JobInvoice)
                Case MDI.MnuPurchaseGoodsReceipt.Name
                    FrmObj = New FrmPurchInvoiceDirect_WithDimension(StrUserPermission, DTUP, Ncat.PurchaseGoodsReceipt)
                Case MDI.MnuVoucherType.Name, MdiSpare.MnuVoucherType.Name, MdiKirana.MnuVoucherType.Name
                    FrmObj = New FrmVoucher_Type(StrUserPermission, DTUP)
                Case MDI.MnuItemTypeMaster.Name, MdiSpare.MnuItemTypeMaster.Name, MdiKirana.MnuItemTypeMaster.Name
                    FrmObj = New FrmItemType(StrUserPermission, DTUP)
                Case MDI.MnuItemInvoiceGroup.Name
                    FrmObj = New FrmItemMaster(StrUserPermission, DTUP, ItemV_Type.ItemInvoiceGroup)
                Case MDI.MnuLotMaster.Name
                    FrmObj = New FrmItemMaster(StrUserPermission, DTUP, ItemV_Type.Lot)
                Case MDI.MnuUnitMaster.Name, MdiSpare.MnuUnitMaster.Name, MdiKirana.MnuUnitMaster.Name
                    FrmObj = New FrmUnit(StrUserPermission, DTUP)
                Case MDI.MnuUnitConversion.Name
                    FrmObj = New FrmUnitConversion(StrUserPermission, DTUP)
                Case MDI.MnuCatalogMaster.Name
                    FrmObj = New FrmCatalog(StrUserPermission, DTUP)
                'Case MDI.MnuSaleInvoiceWMapping.Name
                '    FrmObj = New FrmSaleInvoiceMapping()
                Case MDI.MnuReportSettings.Name
                    FrmObj = New FrmSettingsReports_Visibility(StrUserPermission, DTUP)
                Case MDI.MnuReverseChargeEntry.Name
                    FrmObj = New FrmReverseChargeEntry(StrUserPermission, DTUP, Ncat.ReverseCharge)
                Case MDI.MnuRestoreDatabase.Name
                    FrmObj = New FrmRestoreDatabase(AgL)
                Case MDI.MnuYearEnd.Name, MdiSpare.MnuYearEnd.Name, MdiKirana.MnuYearEnd.Name
                    FrmObj = New FrmYearClosing(AgL)
                Case MDI.ChequeFormatToolStripMenuItem.Name
                    FrmObj = New FrmChequeFormat(StrUserPermission, DTUP)
                Case MDI.MnuCuttingConsumption.Name
                    FrmObj = New FrmCuttingConsumption(StrUserPermission, DTUP)
                'Case MDI.MnuCuttingConsumptionException.Name
                '    FrmObj = New FrmCuttingConsumptionException(StrUserPermission, DTUP)
                Case MDI.MnuProcessMaster.Name
                    FrmObj = New FrmPerson(StrUserPermission, DTUP, SubgroupType.Process)
                Case MDI.MnuRateList.Name
                    FrmObj = New FrmRateList(StrUserPermission, DTUP)
                Case MDI.MnuRateListException.Name
                    FrmObj = New FrmRateListException(StrUserPermission, DTUP)
                Case MDI.MnuJournalEntry.Name, MdiSpare.MnuJournalEntry.Name, MdiSchool.MnuJournalEntry.Name
                    FrmObj = New FrmJournalEntry(StrUserPermission, DTUP, "OB,JV")
                Case MDI.MnuJournalAdjustmentEntry.Name
                    FrmObj = New FrmJournalEntry(StrUserPermission, DTUP, "JV", "Shyama Shyam")
                Case MDI.MnuUpdateLinkedAccount.Name
                    FrmObj = New FrmUpdateLinkAccount
                Case MDI.MnuSerializeEntryNo.Name
                    FrmObj = New FrmSerializeEntryNo
                Case MDI.MnuDebtorsOpeningEntry.Name
                    FrmObj = New FrmPurchInvoiceDirect_WithDimension(StrUserPermission, DTUP, Ncat.OpeningBalance, mCustomUI_OpeningBalanceDebtors)
                Case MDI.MnuCreditorsOpeningEntry.Name
                    FrmObj = New FrmPurchInvoiceDirect_WithDimension(StrUserPermission, DTUP, Ncat.OpeningBalance, mCustomUI_OpeningBalanceCreditors)


                'Case MDI.MnuFinishedMaterialPlan.Name
                '    FrmObj = New FrmPurchPlan(StrUserPermission, DTUP, Ncat.FinishedMaterialPlan)
                'Case MDI.MnuRawMaterialPlan.Name
                '    FrmObj = New FrmPurchPlan(StrUserPermission, DTUP, Ncat.RawMaterialPlan)
                'Case MDI.MnuDyeingPlan.Name
                '    FrmObj = New FrmPurchPlan(StrUserPermission, DTUP, "DPL")
                Case MDI.MnuSizeMaster.Name
                    FrmObj = New FrmItemMaster(StrUserPermission, DTUP, ItemV_Type.SIZE)
                Case MDI.MnuChangePassword.Name, MdiSchool.MnuChangePassword.Name
                    FrmObj = New FrmChangePassword
                Case MDI.MnuUserPermission.Name, MdiSchool.MnuUserPermission.Name
                    FrmObj = New FrmUserPermission(StrUserPermission, DTUP, AgL)
                Case MDI.MnuUserMaster.Name, MdiSchool.MnuUserMaster.Name
                    FrmObj = New FrmUser(StrUserPermission, DTUP, AgL)
                Case MDI.MnuCompanyMaster.Name, MdiSchool.MnuCompanyMaster.Name, MdiSpare.MnuCompanyMaster.Name, MdiKirana.MnuCompanyMaster.Name
                    FrmObj = New FrmCompanyInput
                Case MDI.MnuCustomerMaster.Name, MdiSpare.MnuCustomerMaster.Name, MdiSchool.MnuCustomerMaster.Name, MdiKirana.MnuCustomerMaster.Name
                    If ClsMain.FDivisionNameForCustomization(20) = "SHYAMA SHYAM FABRICS" Or ClsMain.FDivisionNameForCustomization(22) = "W SHYAMA SHYAM FABRICS" Then
                        FrmObj = New FrmPerson_ShyamaShyam(StrUserPermission, DTUP)
                    Else
                        FrmObj = New FrmPerson(StrUserPermission, DTUP)
                    End If

                Case MDI.MnuGodownMaster.Name
                    'FrmObj = New FrmGodown(StrUserPermission, DTUP)
                    FrmObj = New FrmPerson(StrUserPermission, DTUP, SubgroupType.Godown)
                'Case MDI.MnuDebtorOutstandingReportFormatted.Name
                '    Dim cRepProc As ClsDebtorOutstandingReport
                '    ReportFrm = New Aglibrary.FrmReportLayout("", "", StrSenderText, "")
                '    CRepProc = New ClsDebtorOutstandingReport(ReportFrm)
                '    cRepProc.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                '    cRepProc.Ini_Grid()
                '    FrmObj = ReportFrm

                'Case MDI.MnuPartyLedger.Name
                '    Dim cRepProc As ClsPartyLedger
                '    ReportFrm = New Aglibrary.FrmReportLayout("", "", StrSenderText, "")
                '    cRepProc = New ClsPartyLedger(ReportFrm)
                '    cRepProc.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                '    cRepProc.Ini_Grid()
                '    FrmObj = ReportFrm

                Case MDI.MnuExportDataForBranch.Name
                    Dim cRepProc As ClsExportDataForBranch
                    ReportFrm = New AgLibrary.FrmReportLayout(AgL, "", "", StrSenderText, "")
                    cRepProc = New ClsExportDataForBranch(ReportFrm)
                    cRepProc.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    cRepProc.Ini_Grid()
                    FrmObj = ReportFrm

                Case MDI.MnuDeleteData.Name
                    Dim cRepProc As ClsDeleteData_New
                    ReportFrm = New AgLibrary.FrmReportLayout(AgL, "", "", StrSenderText, "")
                    cRepProc = New ClsDeleteData_New(ReportFrm)
                    cRepProc.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    cRepProc.Ini_Grid()
                    FrmObj = ReportFrm

                Case MDI.MnuDeleteAttachments.Name
                    Dim cRepProc As ClsDeleteAttachments
                    ReportFrm = New AgLibrary.FrmReportLayout(AgL, "", "", StrSenderText, "")
                    cRepProc = New ClsDeleteAttachments(ReportFrm)
                    cRepProc.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    cRepProc.Ini_Grid()
                    FrmObj = ReportFrm

                Case MDI.MnuAdjustStockFIFO.Name
                    FrmObj = New FrmStockAdjustmentFIFO()

                Case MDI.MnuImportDataFromBranch.Name
                    FrmObj = New FrmImportDataFromBranch()

                Case MDI.MnuImportDataCustom.Name
                    FrmObj = New FrmSyncDataFromOtherDatabase()

                Case MDI.MnuDivisionMaster.Name, MdiCheque.MnuChequeCompanyMaster.Name, MdiSchool.MnuDivisionMaster.Name, MdiSpare.MnuDivisionMaster.Name, MdiKirana.MnuDivisionMaster.Name
                    If AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Then
                        FrmObj = New FrmPerson("AE**", DTUP, SubgroupType.Division)
                    Else
                        FrmObj = New FrmPerson("*E**", DTUP, SubgroupType.Division)
                    End If
                Case MDI.MnuSiteMaster.Name, MdiSchool.MnuSiteMaster.Name, MdiSpare.MnuSiteMaster.Name, MdiKirana.MnuSiteMaster.Name
                    If AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Then
                        FrmObj = New FrmPerson("AE**", DTUP, SubgroupType.Site)
                    Else
                        FrmObj = New FrmPerson("*E**", DTUP, SubgroupType.Site)
                    End If

                Case MDI.MnuCustomerReceiptEntry.Name
                    FrmObj = New FrmVoucherEntry(StrUserPermission, DTUP, "VR")
                Case MDI.MnuPaymentEntry.Name, MdiSpare.MnuPaymentEntry.Name, MdiSchool.MnuPaymentEntry.Name
                    FrmObj = New FrmVoucherEntry(StrUserPermission, DTUP, Ncat.Payment)
                Case MDI.MnuReceiptEntry.Name, MdiSpare.MnuReceiptEntry.Name, MdiSchool.MnuReceiptEntry.Name
                    FrmObj = New FrmVoucherEntry(StrUserPermission, DTUP, Ncat.Receipt)








                Case MdiKirana.MnuSalesOrderSettlement.Name
                    FrmObj = New FrmOrderSettlement_Kirana(StrUserPermission, DTUP, Ncat.SaleOrderCancel)
                Case MdiKirana.MnuPurchaseOrderSettlement.Name
                    FrmObj = New FrmOrderSettlement_Kirana(StrUserPermission, DTUP, Ncat.PurchaseOrderCancel)




                Case MDI.MnuPaymentEntryJobWorker.Name
                    FrmObj = New FrmPaymentEntryJobWorker(StrUserPermission, DTUP, ClsGarmentProduction.NCat_PaymentJobWorker)
                Case MDI.MnuFabricConsumption.Name
                    FrmObj = New FrmConsumption(StrUserPermission, DTUP)
                Case MDI.MnuLocalFreightEntry.Name
                    FrmObj = New FrmFreightEntry(StrUserPermission, DTUP, "LF")
                Case MDI.MnuExpenseIncomeVoucher.Name
                    FrmObj = New FrmExpenseEntry(StrUserPermission, DTUP, Ncat.ExpenseVoucher + "," + Ncat.IncomeVoucher)

                Case MDI.MnuDebitNote.Name
                    FrmObj = New FrmDebitCreditNote(StrUserPermission, DTUP, Ncat.DebitNoteSupplier + "," + Ncat.DebitNoteCustomer)
                Case MDI.MnuCreditNote.Name
                    FrmObj = New FrmDebitCreditNote(StrUserPermission, DTUP, Ncat.CreditNoteCustomer + "," + Ncat.CreditNoteSupplier)
                Case MDI.MnuPaymentSettlementEntry.Name
                    FrmObj = New FrmPartyAcSettlement(StrUserPermission, DTUP, Ncat.PaymentSettlement)
                Case MDI.MnuCustomerAccountSettlement.Name
                    'If ClsMain.FDivisionNameForCustomization(22) = "W SHYAMA SHYAM FABRICS" Then
                    '    FrmObj = New FrmCustomerAcSettlementAadhat(StrUserPermission, DTUP, Ncat.ReceiptSettlement)
                    'Else
                    FrmObj = New FrmPartyAcSettlement(StrUserPermission, DTUP, Ncat.ReceiptSettlement)
                    'End If
                Case MDI.MnuPurchaseOrder.Name, MdiKirana.MnuPurchaseOrder.Name
                    If FGetNewVersionFlag() = True Then
                        FrmObj = New FrmPurchInvoiceDirect_WithDimension(StrUserPermission, DTUP, Ncat.PurchaseOrder)
                    Else
                        FrmObj = New FrmPurchInvoiceDirect(StrUserPermission, DTUP, Ncat.PurchaseOrder)
                    End If
                Case MDI.MnuPurchaseEntry.Name, MdiSchool.MnuPurchaseEntry.Name, MdiKirana.MnuPurchaseEntry.Name
                    If FGetNewVersionFlag() = True Then
                        FrmObj = New FrmPurchInvoiceDirect_WithDimension(StrUserPermission, DTUP, Ncat.PurchaseInvoice)
                    Else
                        FrmObj = New FrmPurchInvoiceDirect(StrUserPermission, DTUP, Ncat.PurchaseInvoice)
                    End If
                Case MDI.MnuPurchaseReturnEntry.Name
                    If FGetNewVersionFlag() = True Then
                        FrmObj = New FrmPurchInvoiceDirect_WithDimension(StrUserPermission, DTUP, Ncat.PurchaseReturn)
                    Else
                        FrmObj = New FrmPurchInvoiceDirect(StrUserPermission, DTUP, Ncat.PurchaseReturn)
                    End If
                Case MDI.MnuSalesOrder.Name, MdiKirana.MnuSalesOrder.Name
                    If ClsMain.FDivisionNameForCustomization(20) = "SHYAMA SHYAM FABRICS" Or ClsMain.FDivisionNameForCustomization(22) = "W SHYAMA SHYAM FABRICS" Then
                        FrmObj = New FrmSaleInvoiceDirect(StrUserPermission, DTUP, Ncat.SaleOrder)
                    Else
                        If FGetNewVersionFlag() = True Then
                            FrmObj = New FrmSaleInvoiceDirect_WithDimension(StrUserPermission, DTUP, Ncat.SaleOrder)
                        Else
                            FrmObj = New FrmSaleInvoiceDirect(StrUserPermission, DTUP, Ncat.SaleOrder)
                        End If
                    End If
                Case MDI.MnuSalesEntry.Name, MdiSchool.MnuSalesEntry.Name, MdiKirana.MnuSalesEntry.Name
                    If ClsMain.FDivisionNameForCustomization(20) = "SHYAMA SHYAM FABRICS" Or ClsMain.FDivisionNameForCustomization(22) = "W SHYAMA SHYAM FABRICS" Then
                        FrmObj = New FrmSaleInvoiceDirect(StrUserPermission, DTUP, Ncat.SaleInvoice)
                    Else
                        If FGetNewVersionFlag() = True Then
                            FrmObj = New FrmSaleInvoiceDirect_WithDimension(StrUserPermission, DTUP, Ncat.SaleInvoice)
                        Else
                            FrmObj = New FrmSaleInvoiceDirect(StrUserPermission, DTUP, Ncat.SaleInvoice)
                        End If
                    End If

                Case MDI.MnuSalesEntryRetail.Name
                    FrmObj = New FrmSaleInvoiceDirect_WithDimension(StrUserPermission, DTUP, Ncat.SaleInvoice, mCustomUI_Retail)

                Case MDI.MnuQuotation.Name
                    FrmObj = New FrmSaleInvoiceDirect_WithDimension(StrUserPermission, DTUP, Ncat.SaleInvoice, mCustomUI_Quotation)

                Case MDI.MnuSaleEntryOverlay.Name
                    FrmObj = New FrmSaleInvoiceDirect_WithDimension(StrUserPermission, DTUP, Ncat.SaleInvoiceOverlay)

                Case MDI.MnuSaleEntryOverlayRetail.Name
                    FrmObj = New FrmSaleInvoiceDirect_WithDimension(StrUserPermission, DTUP, Ncat.SaleInvoiceOverlay, mCustomUI_Retail)

                Case MDI.MnuSalesReturnEntry.Name
                    If ClsMain.FDivisionNameForCustomization(22) = "W SHYAMA SHYAM FABRICS" Then
                        FrmObj = New FrmSaleInvoiceDirect_WithDimension_ShyamaShyam(StrUserPermission, DTUP, Ncat.SaleReturn)
                    Else
                        If FGetNewVersionFlag() = True Then
                            FrmObj = New FrmSaleInvoiceDirect_WithDimension(StrUserPermission, DTUP, Ncat.SaleReturn)
                        Else
                            FrmObj = New FrmSaleInvoiceDirect(StrUserPermission, DTUP, Ncat.SaleReturn)
                        End If
                    End If
                Case MDI.MnuSalesReturnEntryRetail.Name
                    FrmObj = New FrmSaleInvoiceDirect_WithDimension(StrUserPermission, DTUP, Ncat.SaleReturn, mCustomUI_Retail)
                Case MDI.MnuSalesEntryAadhat.Name
                    FrmObj = New FrmSaleInvoiceDirect_Aadhat(StrUserPermission, DTUP, Ncat.SaleInvoice)
                Case MDI.MnuSalesDelivery.Name
                    FrmObj = New FrmDelivery(StrUserPermission, DTUP, Ncat.SaleDelivery + "," + Ncat.PurchaseDelivery)
                Case MDI.MnuSalesEnquiry.Name
                    FrmObj = New FrmSaleEnquiry(StrUserPermission, DTUP, Ncat.SaleEnquiry)
                Case MDI.MnuPacking.Name
                    FrmObj = New FrmPacking(StrUserPermission, DTUP, Ncat.Packing)
                Case MDI.MnuOpeningStock.Name, MdiKirana.MnuOpeningStock.Name
                    FrmObj = New FrmPurchInvoiceDirect_WithDimension(StrUserPermission, DTUP, Ncat.OpeningStock)
                Case MDI.MnuOpeningStockProcess.Name
                    FrmObj = New FrmPurchInvoiceDirect_WithDimension(StrUserPermission, DTUP, Ncat.OpeningStockProcess)
                Case MDI.MnuStockIssue.Name, MdiKirana.MnuStockIssue.Name
                    If ClsMain.FDivisionNameForCustomization(6) = "SADHVI" And AgL.StrCmp(AgL.PubDBName, "Sadhvi") Then
                        FrmObj = New FrmStockEntry(StrUserPermission, DTUP, Ncat.StockIssue)
                    Else
                        FrmObj = New FrmPurchInvoiceDirect_WithDimension(StrUserPermission, DTUP, Ncat.StockIssue)
                    End If
                Case MDI.MnuStockReceive.Name, MdiKirana.MnuStockReceive.Name
                    If ClsMain.FDivisionNameForCustomization(6) = "SADHVI" And AgL.StrCmp(AgL.PubDBName, "Sadhvi") Then
                        FrmObj = New FrmStockEntry(StrUserPermission, DTUP, Ncat.StockReceive)
                    Else
                        FrmObj = New FrmPurchInvoiceDirect_WithDimension(StrUserPermission, DTUP, Ncat.StockReceive)
                    End If
                Case MDI.MnuStockTransfer.Name, MdiKirana.MnuStockTransfer.Name
                    FrmObj = New FrmPurchInvoiceDirect_WithDimension(StrUserPermission, DTUP, Ncat.StockTransfer)
                Case MDI.MnuPhysicalStock.Name
                    FrmObj = New FrmPurchInvoiceDirect_WithDimension(StrUserPermission, DTUP, Ncat.PhysicalStock)
                Case MDI.MnuPhysicalStockAdjustment.Name
                    FrmObj = New FrmPhysicalStockAdjustment(StrUserPermission, DTUP, Ncat.PhysicalStockAdjustment)
                Case MDI.MnuPaymentSettlementHeads.Name
                    FrmObj = New FrmPartyAcSettlementHead(StrUserPermission, DTUP)
                Case MDI.MnuTemporaryLimit.Name
                    FrmObj = New FrmPersonTemporaryCreditLimit(StrUserPermission, DTUP)
                Case MDI.MnuItemGroupMaster.Name, MdiSchool.MnuItemGroupMaster.Name, MdiKirana.MnuItemGroupMaster.Name
                    FrmObj = New FrmItemGroup(StrUserPermission, DTUP)
                Case MDI.MnuItemCategoryMaster.Name, MdiSpare.MnuItemCategoryMaster.Name, MdiSchool.MnuItemCategoryMaster.Name, MdiKirana.MnuItemCategoryMaster.Name
                    If FGetNewVersionFlag() = True Then
                        FrmObj = New FrmItemCategory_Grid(StrUserPermission, DTUP)
                    Else
                        FrmObj = New FrmItemCategory(StrUserPermission, DTUP)
                    End If
                Case MDI.MnuInterestSlabMaster.Name
                    FrmObj = New FrmInterestSlab(StrUserPermission, DTUP)
                Case MDI.MnuItemMaster.Name, MdiSpare.MnuItemMaster.Name, MdiSchool.MnuItemMaster.Name
                    FrmObj = New FrmItemMaster(StrUserPermission, DTUP, ItemV_Type.Item)
                Case MDI.MnuDimension1Master.Name
                    FrmObj = New FrmItemMaster(StrUserPermission, DTUP, ItemV_Type.Dimension1)
                Case MDI.MnuDimension2Master.Name
                    FrmObj = New FrmItemMaster(StrUserPermission, DTUP, ItemV_Type.Dimension2)
                Case MDI.MnuDimension3Master.Name
                    FrmObj = New FrmItemMaster(StrUserPermission, DTUP, ItemV_Type.Dimension3)
                Case MDI.MnuDimension4Master.Name
                    FrmObj = New FrmItemMaster(StrUserPermission, DTUP, ItemV_Type.Dimension4)
                Case MDI.MnuPaymentModeMaster.Name
                    FrmObj = New FrmPaymentMode(StrUserPermission, DTUP)
                Case MDI.MnuCityMaster.Name, MdiSpare.MnuCityMaster.Name, MdiKirana.MnuCityMaster.Name
                    FrmObj = New FrmCity(StrUserPermission, DTUP)
                Case MDI.MnuStateMaster.Name
                    FrmObj = New FrmState(StrUserPermission, DTUP)
                Case MDI.MnuAreaMaster.Name, MdiSpare.MnuAreaMaster.Name, MdiKirana.MnuAreaMaster.Name
                    FrmObj = New FrmArea(StrUserPermission, DTUP)
                Case MDI.MnuZoneMaster.Name
                    FrmObj = New FrmZone(StrUserPermission, DTUP)

                Case MDI.MnuItemGroupPersonMaster.Name
                    FrmObj = New FrmItemGroupPersonMaster()
                Case MDI.MnuDepartmentMaster.Name
                    FrmObj = New FrmDepartment(StrUserPermission, DTUP)
                Case MDI.MnuDesignationMaster.Name
                    FrmObj = New FrmDesignation(StrUserPermission, DTUP)
                Case MDI.MnuRateTypeMaster.Name
                    FrmObj = New FrmRateType(StrUserPermission, DTUP)
                Case MDI.MnuSchemeMaster.Name
                    FrmObj = New FrmScheme(StrUserPermission, DTUP)
                Case MDI.MnuBarcodeRateRevision.Name
                    FrmObj = New FrmBarcodeRateRevision(StrUserPermission, DTUP)
                Case MDI.MnuGenerateBarcode.Name
                    FrmObj = New FrmPrintBarcode(StrUserPermission, DTUP)
                Case MDI.MnuShape.Name
                    FrmObj = New FrmShape(StrUserPermission, DTUP)
                Case MDI.MnuPermissionApproval.Name
                    FrmObj = New FrmPermissionApproval(StrUserPermission, DTUP)
                Case MDI.MnuLREntry.Name
                    FrmObj = New FrmLrEntry(StrUserPermission, DTUP, Ncat.LrEntry)
                Case MDI.FrmVoucherAdjustment.Name
                    FrmObj = New FrmVoucherAdjBulk()
                Case MDI.MnuUpdateTableStructure.Name, MdiSchool.MnuUpdateTableStructure.Name, MdiSpare.MnuUpdateTableStructure.Name, MdiKirana.MnuUpdateTableStructure.Name
                    Dim clsM As New ClsMain(AgL)
                    clsM.UpdateTableStructure()
                Case MDI.MnuSyncWithActualDatabase.Name
                    FrmObj = New FrmSyncData()
                Case MDI.MnuDataCorrection.Name
                    FrmObj = New FrmChequeDateCorrection()

                Case MDI.MnuWorkOrder_FallPico.Name
                    FrmObj = New FrmSaleInvoiceDirect_WithDimension(StrUserPermission, DTUP, Ncat.WorkOrder)
                Case MDI.MnuWorkInvoice_FallPico.Name
                    FrmObj = New FrmSaleInvoiceDirect_WithDimension(StrUserPermission, DTUP, Ncat.WorkInvoice)

                Case MDI.MnuJobOrder_FallPico.Name
                    FrmObj = New FrmPurchInvoiceDirect_WithDimension(StrUserPermission, DTUP, Ncat.JobOrder)
                Case MDI.MnuJobReceive_FallPico.Name
                    FrmObj = New FrmPurchInvoiceDirect_WithDimension(StrUserPermission, DTUP, Ncat.JobInvoice)


                Case MDI.MnuDivisionCompanySetting.Name
                    FrmObj = New FrmDivisionCompanySetting(StrUserPermission, DTUP)

                Case MDI.MnuTagMaster.Name
                    FrmObj = New FrmTag(StrUserPermission, DTUP)


                Case MDI.MnuBackupDatabase.Name, MdiCheque.MnuChequeBackupData.Name, MdiSchool.MnuBackupDatabase.Name, MdiSpare.MnuBackupDatabase.Name, MdiKirana.MnuBackupDatabase.Name
                    If AgL.PubServerName = "" Then
                        FrmObj = New AgLibrary.FrmAgZip(AgL)
                    Else
                        FrmObj = New AgLibrary.FrmBackupDatase(AgL)
                    End If
                'Case MDI.MnuUpdateDefaultSettings.Name
                '    Dim clsObj As New ClsMain(AgL)

                '    clsObj.FSeedTable_SaleInvoiceSetting(True)
                '    clsObj.FSeedTable_PurchaseInvoiceSetting(True)
                '    clsObj.FSeedTable_StockHeadSetting(True)
                Case MDI.MnuExecuteQuery.Name, MdiSchool.MnuExecuteQuery.Name, MdiSpare.MnuExecuteQuery.Name, MdiKirana.MnuExecuteQuery.Name
                    FrmObj = New FrmQuery()

                Case MDI.MnuSettings.Name, MdiSchool.MnuSettings.Name, MdiSpare.MnuSettings.Name, MdiKirana.MnuSettings.Name
                    FrmObj = New FrmSettings_New(StrUserPermission, DTUP)

                Case MDI.MnuSettingsVisibility.Name, MdiSchool.MnuSettingsVisibility.Name, MdiSpare.MnuSettingsVisibility.Name, MdiKirana.MnuSettingsVisibility.Name
                    FrmObj = New FrmSettings_Visibility(StrUserPermission, DTUP)

                Case MDI.MnuSettingsCommon.Name, MdiSchool.MnuSettingsCommon.Name, MdiSpare.MnuSettingsCommon.Name, MdiKirana.MnuSettingsCommon.Name
                    FrmObj = New FrmSettings_Common(StrUserPermission, DTUP)

                Case MDI.MnuSettingsEInvoice.Name
                    FrmObj = New FrmSettings_Common(StrUserPermission, DTUP, "E Invoice")

                Case MDI.MnuVoucherTypeTimePlan.Name, MdiSchool.MnuVoucherTypeTimePlan.Name
                    FrmObj = New FrmVoucherTypeTimePlan(StrUserPermission, DTUP)

                Case MDI.MnuSchemeQualification.Name
                    FrmObj = New FrmSchemeQualification()

                Case MDI.MnuSettingsMenus.Name, MdiSpare.MnuSettingsMenus.Name, MdiKirana.MnuSettingsMenus.Name
                    FrmObj = New FrmSettings_Menus(StrUserPermission, DTUP)

                'Case MDI.MnuSendSms.Name
                '    FrmObj = New FrmSendSms(AgL)

                Case MDI.MnuExportSqlServerData.Name
                    FrmObj = New FrmExportDataFromSqlServer(AgL)

                Case MDI.MnuExportSqliteDataToSqlServer.Name
                    FrmObj = New FrmExportDataToSqlServer(AgL)
                Case MDI.MnuImportData.Name
                    FrmObj = New FrmImportDataSingleUI()

                Case MDI.MnuLeadMaster.Name
                    FrmObj = New FrmLead(StrUserPermission, DTUP)


                Case MDI.MnuItemMerging.Name
                    FrmObj = New FrmItemMerging()

                Case MDI.MnuSaleInvoiceW.Name
                    'FrmObj = New FrmSaleInvoiceW()
                    'FrmObj = New FrmSaleInvoiceW_New()
                    FrmObj = New FrmSaleInvoiceW_OnlyW()
                Case MDI.MnuSaleReturnW.Name
                    FrmObj = New FrmSaleReturnW()
                'Case MDI.MnuQuickView.Name
                '    FrmObj = New FrmQuickView()
                Case MDI.MnuLRTransfer.Name
                    FrmObj = New FrmLrTransfer(StrUserPermission, DTUP, Ncat.LrTransfer)

                'Case MDI.MnuExportSqlServerData.Name
                '    FrmObj = New FrmExportDataFromSqlServer(AgL)
                'Case MDI.MnuSalesEntryAadhat.Name
                '    FrmObj = New FrmSaleInvoiceDirect_Aadhat(StrUserPermission, DTUP, Ncat.SaleInvoice)
                Case MDI.MnuHolidayMaster.Name
                    FrmObj = New FrmHrm_Holiday(StrUserPermission, DTUP)
                Case MDI.MnuDefineCostCenter.Name
                    FrmObj = New AgAccounts.FrmSingleFieldMaster(MDI, AgL, StrSenderText, "CostCenterMast", "Cost Center", "Code", "Name", 30, StrUserPermission, DTUP, False, False, AgL.PubReportPath)
                    AgL.PubReportTitle = "Define Cost Center"
                Case MDI.MnuBillWiseOutstandingCreditors.Name
                    FrmObj = New AgAccounts.FrmReportLayout("BillWsOS_Cr", MDI.MnuBillWiseOutstandingCreditors.Text, 6)
                Case MDI.MnuBillWiseOutstandingDebtors.Name
                    FrmObj = New AgAccounts.FrmReportLayout("BillWsOS_Dr", MDI.MnuBillWiseOutstandingDebtors.Text, 6)
                'Case MDI.MnuAgeingAnalysisFIFO.Name
                '    FrmObj = New AgAccounts.FrmReportLayout("Ageing", MDI.MnuAgeingAnalysisFIFO.Text, 15)
                Case MDI.MnuBankReconsilationEntry.Name
                    FrmObj = New AgAccounts.FrmBankReconciliation(StrUserPermission, DTUP)
                'Case MDI.MnuTrialBalance_Disp.Name
                '    FrmObj = New AgAccounts.FrmDisplayHierarchy
                '    CType(FrmObj, AgAccounts.FrmDisplayHierarchy).FForward(0, 0, AgAccounts.ClsStructure.DisplayType.TrailBalance)
                'Case MDI.MnuDetailTrialBalance_Disp.Name
                '    FrmObj = New AgAccounts.FrmDisplayHierarchy
                '    CType(FrmObj, AgAccounts.FrmDisplayHierarchy).FForward(0, 0, AgAccounts.ClsStructure.DisplayType.DTrailBalance)
                Case MDI.MnuStockReport.Name, MdiKirana.MnuStockReport.Name
                    FrmObj = New AgAccounts.FrmDisplayHierarchy_Stock
                    CType(FrmObj, AgAccounts.FrmDisplayHierarchy_Stock).FForward(0, 0)
                Case MDI.MnuLedger.Name
                    FrmObj = New AgAccounts.FrmReportLayout("Ledger", MDI.MnuLedger.Text, 10)
                Case MDI.MnuTrialGroup.Name
                    FrmObj = New AgAccounts.FrmReportLayout("TrialGroup", MDI.MnuTrialGroup.Text, 3)
                Case MDI.MnuTrialDetail.Name
                    FrmObj = New AgAccounts.FrmReportLayout("TrialDetail", MDI.MnuTrialDetail.Text, 5)
                'Case MDI.MnuProfitAndLoss_Disp.Name
                '    FrmObj = New AgAccounts.FrmDisplayHierarchy
                '    CType(FrmObj, AgAccounts.FrmDisplayHierarchy).FForward(0, 0, AgAccounts.ClsStructure.DisplayType.ProfitAndLoss)
                'Case MDI.MnuBalanceSheet_Disp.Name
                '    FrmObj = New AgAccounts.FrmDisplayHierarchy
                '    CType(FrmObj, AgAccounts.FrmDisplayHierarchy).FForward(0, 0, AgAccounts.ClsStructure.DisplayType.BalanceSheet)
                Case MDI.MnuVoucherEntry.Name
                    FrmObj = New AgAccounts.FrmVoucherEntry(StrUserPermission, DTUP, AgAccounts.ClsStructure.EntryType.ForEntry)
                Case MDI.MnuAccountGroup.Name
                    'FrmObj = New AgAccounts.FrmAcGroupMaster(StrUserPermission, DTUP)
                    FrmObj = New FrmAccountGroup(StrUserPermission, DTUP)
                Case MDI.MnuNarrationMaster.Name
                    FrmObj = New AgAccounts.FrmSingleFieldMaster(MDI, AgL, StrSenderText, "NarrationMast", "Narration", "Code", "Name", 100, StrUserPermission, DTUP, False, False, AgL.PubReportPath)
                    AgL.PubReportTitle = "Narration Master"
                Case MDI.MnuAnnexure.Name
                    FrmObj = New AgAccounts.FrmReportLayout("Annexure", MDI.MnuAnnexure.Text, 4)
                Case MDI.MnuCashBook.Name
                    FrmObj = New AgAccounts.FrmReportLayout("CashBook", MDI.MnuCashBook.Text, 10)
                Case MDI.MnuBankBook.Name
                    FrmObj = New AgAccounts.FrmReportLayout("BankBook", MDI.MnuBankBook.Text, 10)
                Case MDI.MnuJournalBook.Name
                    FrmObj = New AgAccounts.FrmReportLayout("Journal", MDI.MnuJournalBook.Text, 5)
                Case MDI.MnuDayBook.Name
                    FrmObj = New AgAccounts.FrmReportLayout("DayBook", MDI.MnuDayBook.Text, 5)
                Case MDI.MnuCashFlowStatement.Name
                    FrmObj = New AgAccounts.FrmReportLayout("CashFlow", MDI.MnuCashFlowStatement.Text, 3)
                Case MDI.MnuFundFlowStatement.Name
                    FrmObj = New AgAccounts.FrmReportLayout("FundFlow", MDI.MnuFundFlowStatement.Text, 3)
                Case MDI.MnuMonthlyExpenseChart.Name
                    FrmObj = New AgAccounts.FrmReportLayout("MonthlyExpenses", MDI.MnuMonthlyExpenseChart.Text, 4)
                Case MDI.MnuInterestLedger.Name
                    FrmObj = New AgAccounts.FrmReportLayout("InterestLedger", MDI.MnuInterestLedger.Text, 8)
                Case MDI.MnuMonthyLedgerSummary.Name
                    FrmObj = New AgAccounts.FrmReportLayout("MonthlyLedgerSummary", MDI.MnuMonthyLedgerSummary.Text, 3)
                Case MDI.MnuTrialDetailDrCr.Name
                    FrmObj = New AgAccounts.FrmReportLayout("TrialDetailDrCr", MDI.MnuTrialDetailDrCr.Text, 7)
                Case MDI.MnuMonthlyLedgerSummaryFull.Name
                    FrmObj = New AgAccounts.FrmReportLayout("MonthlyLedgerSummaryFull", MDI.MnuMonthlyLedgerSummaryFull.Text, 4)
                Case MDI.MnuDailyTransactionSummary.Name
                    FrmObj = New AgAccounts.FrmReportLayout("DailyTransBook", MDI.MnuDailyTransactionSummary.Text, 6)
                Case MDI.MnuOutstandinDebtorsFIFO.Name
                    FrmObj = New AgAccounts.FrmReportLayout("FIFOWsOS_Dr", MDI.MnuOutstandinDebtorsFIFO.Text, 9)
                Case MDI.MnuOutstandingCreditorsFIFO.Name
                    FrmObj = New AgAccounts.FrmReportLayout("FIFOWsOS_Cr", MDI.MnuOutstandingCreditorsFIFO.Text, 5)
                'Case MDI.MnuStockValuation.Name
                '    FrmObj = New AgAccounts.FrmReportLayout("Stock_Valuation", MDI.MnuStockValuation.Text, 8)
                Case MDI.MnuDailyCollectionRegister.Name
                    FrmObj = New AgAccounts.FrmReportLayout("DailyCollectionRegister", MDI.MnuDailyCollectionRegister.Text, 4)
                Case MDI.MnuDailyExpenseRegister.Name
                    FrmObj = New AgAccounts.FrmReportLayout("DailyExpenseRegister", MDI.MnuDailyExpenseRegister.Text, 4)
                Case MDI.MnuAccountGroupMergeLedger.Name
                    FrmObj = New AgAccounts.FrmReportLayout("AccountGrMergeLedger", MDI.MnuAccountGroupMergeLedger.Text, 6)
                Case MDI.MnuAgeingAnalysisBillWise.Name
                    FrmObj = New AgAccounts.FrmReportLayout("BillWsOSAgeing", MDI.MnuAgeingAnalysisBillWise.Text, 5)
                Case MDI.MnuBillWiseAdjustmentRegister.Name
                    FrmObj = New AgAccounts.FrmReportLayout("BillWiseAdj", MDI.MnuBillWiseAdjustmentRegister.Text, 5)
                Case MDI.MnuAccountGroupWiseAgeingAnalysis.Name
                    FrmObj = New AgAccounts.FrmReportLayout("AccountGrpWsOSAgeing", MDI.MnuAccountGroupWiseAgeingAnalysis.Text, 9)
                Case MDI.MnuInterestCalculationForDebtors.Name
                    FrmObj = New AgAccounts.FrmReportLayout("IntCalForDebtors", MDI.MnuInterestCalculationForDebtors.Text, 5)


                    'AgAccount End


                    'School
                Case MdiSchool.MnuFeeMaster.Name
                    FrmObj = New FrmPerson(StrUserPermission, DTUP, ClsSchool.SubGroupType_Fee)
                Case MdiSchool.MnuClassMaster.Name
                    FrmObj = New FrmPerson(StrUserPermission, DTUP, ClsSchool.SubGroupType_Class)
                Case MdiSchool.MnuSectionMaster.Name
                    FrmObj = New FrmPerson(StrUserPermission, DTUP, ClsSchool.SubGroupType_Section)
                Case MdiSchool.MnuHouseMaster.Name
                    FrmObj = New FrmPerson(StrUserPermission, DTUP, ClsSchool.SubGroupType_House)
                Case MdiSchool.MnuCasteMaster.Name
                    FrmObj = New FrmPerson(StrUserPermission, DTUP, SubgroupType.Caste)
                Case MdiSchool.MnuReligionMaster.Name
                    FrmObj = New FrmPerson(StrUserPermission, DTUP, SubgroupType.Religion)
                Case MdiSchool.MnuFacilityMaster.Name
                    FrmObj = New FrmPerson(StrUserPermission, DTUP, ClsSchool.SubGroupType_Facility)
                Case MdiSchool.MnuClassFeeSchedule.Name
                    FrmObj = New FrmClassFee(StrUserPermission, DTUP)
                Case MdiSchool.MnuFacilityFeeSchedule.Name
                    FrmObj = New FrmFacilityFee(StrUserPermission, DTUP)
                Case MdiSchool.MnuStudentMaster.Name
                    FrmObj = New FrmStudent(StrUserPermission, DTUP)
                Case MdiSchool.MnuStudentPromotionEntry.Name
                    FrmObj = New FrmStudentPromotion()
                Case MdiSchool.MnuFeeDueEntry.Name
                    FrmObj = New FrmFeeDueEntry(StrUserPermission, DTUP, ClsSchool.NCat_FeeDue)
                Case MdiSchool.MnuFeeReceiptEntry.Name
                    FrmObj = New FrmFeeReceiptEntry(StrUserPermission, DTUP, ClsSchool.NCat_FeeReceipt)
                Case MdiSchool.MnuFeeDueReport.Name
                    Dim cRepProc As ClsFeeDueReport
                    ReportFrm = New AgLibrary.FrmReportLayout(AgL, "", "", StrSenderText, "")
                    cRepProc = New ClsFeeDueReport(ReportFrm)
                    cRepProc.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    cRepProc.Ini_Grid()
                    FrmObj = ReportFrm
                Case MdiSchool.MnuFeeReceiveReport.Name
                    Dim cRepProc As ClsFeeReceiveReport
                    ReportFrm = New AgLibrary.FrmReportLayout(AgL, "", "", StrSenderText, "")
                    cRepProc = New ClsFeeReceiveReport(ReportFrm)
                    cRepProc.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    cRepProc.Ini_Grid()
                    FrmObj = ReportFrm
                Case MdiSchool.MnuStudentLedger.Name
                    Dim cRepProc As ClsStudentLedger
                    ReportFrm = New AgLibrary.FrmReportLayout(AgL, "", "", StrSenderText, "")
                    cRepProc = New ClsStudentLedger(ReportFrm)
                    cRepProc.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    cRepProc.Ini_Grid()
                    FrmObj = ReportFrm
                Case MdiSchool.MnuStudentList.Name
                    Dim cRepProc As ClsStudentList
                    ReportFrm = New AgLibrary.FrmReportLayout(AgL, "", "", StrSenderText, "")
                    cRepProc = New ClsStudentList(ReportFrm)
                    cRepProc.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    cRepProc.Ini_Grid()
                    FrmObj = ReportFrm
                    'School End


                    'Spare
                Case MdiSpare.MnuOrder.Name
                    FrmObj = New FrmSaleInvoiceDirect_WithDimension(StrUserPermission, DTUP, Ncat.SaleInvoice, mCustomUI_Order)
                Case MdiSpare.MnuQuotation.Name
                    FrmObj = New FrmSaleInvoiceDirect_WithDimension(StrUserPermission, DTUP, Ncat.SaleInvoice, mCustomUI_Quotation)
                Case MdiSpare.MnuEstimate.Name
                    FrmObj = New FrmSaleInvoiceDirect_WithDimension(StrUserPermission, DTUP, Ncat.SaleInvoice, mCustomUI_Estimate)
                    'Spare End


                Case MDI.MnuChuktiLedger.Name
                    ReportFrm = New AgLibrary.FrmReportLayout(AgL, "", "", StrSenderText, "")
                    Dim CRepProc As ClsConcurLedger
                    CRepProc = New ClsConcurLedger(ReportFrm)
                    CRepProc.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    CRepProc.Ini_Grid()
                    FrmObj = ReportFrm

                Case MDI.MnuPartyWiseItemWiseOutstandingReport.Name
                    ReportFrm = New AgLibrary.FrmReportLayout(AgL, "", "", StrSenderText, "")
                    Dim CRepProc As ClsPartyWiseItemWiseOutstandingReport
                    CRepProc = New ClsPartyWiseItemWiseOutstandingReport(ReportFrm)
                    CRepProc.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    CRepProc.Ini_Grid()
                    FrmObj = ReportFrm


                Case Else
                    FrmObj = Nothing
            End Select
        ElseIf mTargetEntryType = TargetEntryType.GridReport Then
            GridReportFrm = New AgLibrary.FrmRepDisplay(StrSenderText, AgL)
            GridReportFrm.Filter_IniGrid()
            Select Case StrSender
                Case MDI.MnuBalanceSheet_Aadhat.Name
                    Dim CRep As ClsFinancialDisplay_New = New ClsFinancialDisplay_New(GridReportFrm)
                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    CRep.ShowReportType = ClsFinancialDisplay_New.ReportType.BalanceSheet
                    CRep.Ini_Grid()
                    FrmObj = GridReportFrm
                Case MDI.MnuProfitAndLoss_Aadhat.Name
                    Dim CRep As ClsFinancialDisplay_New = New ClsFinancialDisplay_New(GridReportFrm)
                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    CRep.ShowReportType = ClsFinancialDisplay_New.ReportType.ProfitAndLoss
                    CRep.Ini_Grid()
                    FrmObj = GridReportFrm
                Case MDI.MnuTrialBalance_Aadhat.Name, MdiSpare.MnuTrialBalance_Aadhat.Name
                    Dim CRep As ClsFinancialDisplay_New = New ClsFinancialDisplay_New(GridReportFrm)
                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    CRep.ShowReportType = ClsFinancialDisplay_New.ReportType.TrialBalance
                    CRep.Ini_Grid()
                    FrmObj = GridReportFrm
                Case MDI.MnuDetailTrialBalance_Aadhat.Name, MdiSpare.MnuDetailTrialBalance_Aadhat.Name
                    Dim CRep As ClsFinancialDisplay_New = New ClsFinancialDisplay_New(GridReportFrm)
                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    CRep.ShowReportType = ClsFinancialDisplay_New.ReportType.DetailTrialBalance
                    CRep.Ini_Grid()
                    FrmObj = GridReportFrm
                Case MDI.MnuLedger_Aadhat.Name, MdiSpare.MnuLedger_Aadhat.Name
                    Dim CRep As ClsFinancialDisplay_New = New ClsFinancialDisplay_New(GridReportFrm)
                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    CRep.ShowReportType = ClsFinancialDisplay_New.ReportType.Ledger
                    CRep.Ini_Grid()
                    FrmObj = GridReportFrm
                Case MDI.MnuBankBookGrid.Name, MdiSpare.MnuBankBookGrid.Name
                    Dim CRep As ClsFinancialDisplay_New = New ClsFinancialDisplay_New(GridReportFrm)
                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    CRep.ShowReportType = ClsFinancialDisplay_New.ReportType.BankBook
                    CRep.Ini_Grid()
                    FrmObj = GridReportFrm
                Case MDI.MnuCashBookGrid.Name, MdiSpare.MnuCashBookGrid.Name
                    Dim CRep As ClsFinancialDisplay_New = New ClsFinancialDisplay_New(GridReportFrm)
                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    CRep.ShowReportType = ClsFinancialDisplay_New.ReportType.CashBook
                    CRep.Ini_Grid()
                    FrmObj = GridReportFrm
                Case MDI.MnuCustomerBook.Name
                    Dim CRep As ClsFinancialDisplay_New = New ClsFinancialDisplay_New(GridReportFrm)
                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    CRep.ShowReportType = ClsFinancialDisplay_New.ReportType.CustomerBook
                    CRep.Ini_Grid()
                    FrmObj = GridReportFrm
                Case MDI.MnuSupplierBook.Name
                    Dim CRep As ClsFinancialDisplay_New = New ClsFinancialDisplay_New(GridReportFrm)
                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    CRep.ShowReportType = ClsFinancialDisplay_New.ReportType.SupplierBook
                    CRep.Ini_Grid()
                    FrmObj = GridReportFrm
                Case MDI.MnuGSTReports.Name
                    Dim CRep As ClsSalesTaxReports_OneDotSeven = New ClsSalesTaxReports_OneDotSeven(GridReportFrm)
                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    CRep.Ini_Grid()
                    FrmObj = GridReportFrm
                Case MDI.MnuEBillGeneration.Name
                    Dim CRep As ClsGenerateEInvoice_URL = New ClsGenerateEInvoice_URL(GridReportFrm)
                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    CRep.Ini_Grid()
                    FrmObj = GridReportFrm
                Case MDI.MnuPartyList.Name
                    Dim CRep As ClsPartyList = New ClsPartyList(GridReportFrm)
                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    CRep.Ini_Grid()
                    FrmObj = GridReportFrm
                Case MDI.MnuItemMasterBulk.Name
                    Dim CRep As ClsItemMasterBulk = New ClsItemMasterBulk(GridReportFrm)
                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    CRep.Ini_Grid()
                    FrmObj = GridReportFrm
                Case MDI.MnuSchemeQualification.Name
                    Dim CRep As ClsSchemeQualification = New ClsSchemeQualification(GridReportFrm)
                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    CRep.Ini_Grid()
                    FrmObj = GridReportFrm

                Case MdiKirana.MnuSaleRegister.Name
                    Dim CRep As ClsSalesReport_Kirana = New ClsSalesReport_Kirana(GridReportFrm)
                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    CRep.Ini_Grid()
                    FrmObj = GridReportFrm
                Case MdiKirana.MnuPurchaseRegister.Name
                    Dim CRep As ClsPurchaseReport_Kirana = New ClsPurchaseReport_Kirana(GridReportFrm)
                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    CRep.Ini_Grid()
                    FrmObj = GridReportFrm
                Case MdiKirana.MnuPaymentRegister.Name
                    Dim CRep As ClsPaymentReceiptReport_Kirana = New ClsPaymentReceiptReport_Kirana(GridReportFrm)
                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    CRep.ShowReportType = ClsPaymentReceiptReport_Kirana.ReportType.PaymentRegister
                    CRep.Ini_Grid()
                    FrmObj = GridReportFrm
                Case MdiKirana.MnuReceiptRegister.Name
                    Dim CRep As ClsPaymentReceiptReport_Kirana = New ClsPaymentReceiptReport_Kirana(GridReportFrm)
                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    CRep.ShowReportType = ClsPaymentReceiptReport_Kirana.ReportType.ReceiptRegister
                    CRep.Ini_Grid()
                    FrmObj = GridReportFrm
                Case MdiKirana.MnuCompleteBalancePosition.Name
                    Dim CRep As ClsCompleteBalancePosition_Kirana = New ClsCompleteBalancePosition_Kirana(GridReportFrm)
                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    CRep.Ini_Grid()
                    FrmObj = GridReportFrm

                Case MDI.MnuSmsCustomerLedgerBalance.Name
                    Dim CRep As ClsSmsCustomerLedgerBalance = New ClsSmsCustomerLedgerBalance(GridReportFrm)
                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    CRep.Ini_Grid()
                    FrmObj = GridReportFrm




                Case MDI.MnuSupplierOutstandingWithBankAc.Name
                    Dim CRep As ClsSupplierOutstanding_ShyamaShyam = New ClsSupplierOutstanding_ShyamaShyam(GridReportFrm)
                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    CRep.Ini_Grid()
                    FrmObj = GridReportFrm
                Case MDI.MnuCustomerPaymentFollowup.Name
                    Dim CRep As ClsCustomerPaymentFollowup = New ClsCustomerPaymentFollowup(GridReportFrm)
                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    CRep.Ini_Grid()
                    FrmObj = GridReportFrm

                Case MDI.MnuLeadFollowup.Name
                    Dim CRep As ClsLeadFollowup = New ClsLeadFollowup(GridReportFrm)
                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    CRep.Ini_Grid()
                    FrmObj = GridReportFrm


                Case MDI.MnuCustomerLedger.Name
                    If ClsMain.IsScopeOfWorkContains(IndustryType.KiranaIndustry) Then
                        ReportFrm = New AgLibrary.FrmReportLayout(AgL, "", "", StrSenderText, "")
                        Dim CRepProc As ClsKiranaCustomerLedger
                        CRepProc = New ClsKiranaCustomerLedger(ReportFrm)
                        CRepProc.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                        CRepProc.Ini_Grid()
                        FrmObj = ReportFrm
                    Else
                        Dim CRep As ClsPartyLedgerGrid = New ClsPartyLedgerGrid(GridReportFrm)
                        CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                        CRep.Ini_Grid()
                        FrmObj = GridReportFrm
                    End If

                Case MDI.MnuSupplierLedger.Name
                    Dim CRep As ClsPartyLedgerGrid = New ClsPartyLedgerGrid(GridReportFrm)
                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    CRep.Ini_Grid()
                    FrmObj = GridReportFrm
                Case MDI.MnuTransactionSummary.Name
                    Dim CRep As ClsTransactionSummary = New ClsTransactionSummary(GridReportFrm)
                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    CRep.Ini_Grid()
                    FrmObj = GridReportFrm
                Case MDI.MnuSaleOrderStatus.Name
                    Dim CRep As ClsSaleOrderStatusAadhat = New ClsSaleOrderStatusAadhat(GridReportFrm)
                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    CRep.Ini_Grid()
                    FrmObj = GridReportFrm
                Case MDI.MnuSaleInvoiceReportAadhat.Name
                    Dim CRep As ClsSaleInvoiceReportAadhat = New ClsSaleInvoiceReportAadhat(GridReportFrm)
                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    CRep.Ini_Grid()
                    FrmObj = GridReportFrm
                Case MDI.MnuSaleInvoicePendingInW.Name
                    Dim CRep As ClsSaleInvoicePendingInW = New ClsSaleInvoicePendingInW(GridReportFrm)
                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    CRep.Ini_Grid()
                    FrmObj = GridReportFrm
                'Case MDI.MnuMasterPartyLedger.Name
                '    Dim CRep As ClsMasterPartyLedgerAadhat = New ClsMasterPartyLedgerAadhat(GridReportFrm)
                '    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                '    CRep.Ini_Grid()

                Case MDI.MnuPurchaseGoodsReceiptReport.Name
                    Dim CRep As ClsPurchaseReport = New ClsPurchaseReport(GridReportFrm, Ncat.PurchaseGoodsReceipt, "")
                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    CRep.Ini_Grid()
                    FrmObj = GridReportFrm

                Case MDI.MnuPurchaseInvoiceReport.Name
                    Dim CRep As ClsPurchaseReport = New ClsPurchaseReport(GridReportFrm, Ncat.PurchaseInvoice & "," & Ncat.PurchaseReturn, "")
                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    CRep.Ini_Grid()
                    FrmObj = GridReportFrm
                    'Case MDI.MnuSaleInvoiceReport.Name
                    '    Dim CRep As ClsPurchaseReport = New ClsPurchaseReport(GridReportFrm, Ncat.PurchaseInvoice & "," & Ncat.PurchaseReturn)
                    '    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    '    CRep.Ini_Grid()

                Case MDI.MnuLinkedPartyMismatchReport.Name
                    Dim CRep As ClsLinkedPartyMismatchReport = New ClsLinkedPartyMismatchReport(GridReportFrm)
                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    CRep.Ini_Grid()
                    FrmObj = GridReportFrm
                Case MDI.MnuPartyNotTransactedReport.Name
                    Dim CRep As ClsPartyNotTransactedReport = New ClsPartyNotTransactedReport(GridReportFrm)
                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    CRep.Ini_Grid()
                    FrmObj = GridReportFrm
                Case MDI.MnuCashCustomerReport.Name
                    Dim CRep As ClsCashCustomerReport = New ClsCashCustomerReport(GridReportFrm)
                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    CRep.Ini_Grid()
                    FrmObj = GridReportFrm
                Case MDI.MnuCashCustomerOutstandingReport.Name
                    Dim CRep As ClsCashCustomerOutstandingReport = New ClsCashCustomerOutstandingReport(GridReportFrm)
                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    CRep.Ini_Grid()
                    FrmObj = GridReportFrm

                Case MDI.MnuMissingVoucherReport.Name
                    Dim CRep As ClsMissingVoucherReport = New ClsMissingVoucherReport(GridReportFrm)
                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    CRep.Ini_Grid()
                    FrmObj = GridReportFrm



                Case MDI.MnuSaleSummary.Name
                    Dim CRep As ClsSaleSummary = New ClsSaleSummary(GridReportFrm)
                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    CRep.Ini_Grid()
                    FrmObj = GridReportFrm
                Case MDI.MnuGSTOutputTaxReport.Name
                    Dim CRep As ClsGstOutputTaxReport = New ClsGstOutputTaxReport(GridReportFrm)
                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    CRep.Ini_Grid()
                    FrmObj = GridReportFrm
                Case MDI.MnuTCSOutputReport.Name
                    Dim CRep As ClsTCSReport = New ClsTCSReport(GridReportFrm)
                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    CRep.Ini_Grid()
                    FrmObj = GridReportFrm
                Case MDI.MnuTCSInputReport.Name
                    Dim CRep As ClsTCSReport = New ClsTCSReport(GridReportFrm)
                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    CRep.Ini_Grid()
                    FrmObj = GridReportFrm
                Case MDI.MnuGSTInputTaxReport.Name
                    Dim CRep As ClsGstInputTaxReport = New ClsGstInputTaxReport(GridReportFrm)
                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    CRep.Ini_Grid()
                    FrmObj = GridReportFrm
                Case MDI.MnuInputTaxRegister.Name
                    Dim CRep As ClsInputTaxRegister = New ClsInputTaxRegister(GridReportFrm)
                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    CRep.Ini_Grid()
                    FrmObj = GridReportFrm
                Case MDI.MnuSalesRepCommissionReport.Name
                    Dim CRep As ClsSalesManCommissionReport = New ClsSalesManCommissionReport(GridReportFrm)
                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    CRep.Ini_Grid()
                    FrmObj = GridReportFrm
                Case MDI.MnuBranchStatusReport.Name
                    Dim CRep As ClsSadhviBranchStatus = New ClsSadhviBranchStatus(GridReportFrm)
                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    CRep.Ini_Grid()
                    FrmObj = GridReportFrm
                Case MDI.MnuCustomerPaymentFollowupHistoryReport.Name
                    Dim CRep As ClsCustomerPaymentFollowupHistory = New ClsCustomerPaymentFollowupHistory(GridReportFrm)
                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    CRep.Ini_Grid()
                    FrmObj = GridReportFrm

                Case MDI.MnuBranchPaymentStatusReport.Name
                    Dim CRep As ClsSadhviBranchPaymentStatus = New ClsSadhviBranchPaymentStatus(GridReportFrm)
                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    CRep.Ini_Grid()
                    FrmObj = GridReportFrm
                Case MDI.MnuInconsistencyReport.Name
                    Dim CRep As ClsInconsistencyReport = New ClsInconsistencyReport(GridReportFrm)
                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    CRep.Ini_Grid()
                    FrmObj = GridReportFrm
                Case MdiCheque.MnuChequeReport.Name
                    Dim CRep As ClsChequeReport = New ClsChequeReport(GridReportFrm)
                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    CRep.Ini_Grid()
                    FrmObj = GridReportFrm
                Case MDI.MnuPurchaseAgentCommissionOnPayment.Name
                    Dim CRep As ClsPurchaseAgentCommissionOnPayment = New ClsPurchaseAgentCommissionOnPayment(GridReportFrm)
                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    CRep.Ini_Grid()
                    FrmObj = GridReportFrm
                Case MDI.MnuTDSParameters.Name
                    Dim CRep As ClsTdsParameters = New ClsTdsParameters(GridReportFrm)
                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    CRep.Ini_Grid()
                    FrmObj = GridReportFrm
                Case MDI.MnuCustomerDiscountReport.Name
                    Dim CRep As ClsCustomerDiscountReport = New ClsCustomerDiscountReport(GridReportFrm)
                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    CRep.Ini_Grid()
                    FrmObj = GridReportFrm


                Case MDI.MnuSupplierDiscoutMaster.Name
                    Dim CRep As ClsSupplierDiscountReport = New ClsSupplierDiscountReport(GridReportFrm)
                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    CRep.Ini_Grid()
                    FrmObj = GridReportFrm
                Case MDI.MnuFairDebitNoteReport.Name
                    Dim CRep As ClsFairDebitNoteReport = New ClsFairDebitNoteReport(GridReportFrm)
                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    CRep.Ini_Grid()
                    FrmObj = GridReportFrm
                Case MDI.MnuPurchaseSaleComparisonReport.Name
                    Dim CRep As ClsPurchaseSaleComparisonRegister = New ClsPurchaseSaleComparisonRegister(GridReportFrm)
                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    CRep.Ini_Grid()
                    FrmObj = GridReportFrm
                Case MDI.MnuCustomerFairReport.Name
                    Dim CRep As ClsCustomerFairReport = New ClsCustomerFairReport(GridReportFrm)
                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    CRep.Ini_Grid()
                    FrmObj = GridReportFrm
                Case MDI.MnuSupplierFairReport.Name
                    Dim CRep As ClsSupplierFairReport = New ClsSupplierFairReport(GridReportFrm)
                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    CRep.Ini_Grid()
                    FrmObj = GridReportFrm
                Case MDI.MnuBillWiseProfitability.Name
                    Dim CRep As ClsBillWiseProfitability = New ClsBillWiseProfitability(GridReportFrm)
                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    CRep.Ini_Grid()
                    FrmObj = GridReportFrm
                Case MDI.MnuBatchWiseStockBalance.Name
                    Dim CRep As ClsBatchWiseStockBalance = New ClsBatchWiseStockBalance(GridReportFrm)
                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    CRep.Ini_Grid()
                    FrmObj = GridReportFrm
                'Case MDI.MnuDispatchRegister.Name
                '    Dim CRep As ClsDispatchRegister = New ClsDispatchRegister(GridReportFrm)
                '    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                '    CRep.Ini_Grid()

                Case MDI.MnuTransportRegisterFormat1.Name
                    Dim CRep As ClsTransporterRegister = New ClsTransporterRegister(GridReportFrm, "Format-1")
                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    CRep.Ini_Grid()
                    FrmObj = GridReportFrm
                Case MDI.MnuTransportRegisterFormat2.Name
                    Dim CRep As ClsTransporterRegister = New ClsTransporterRegister(GridReportFrm, "Format-2")
                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    CRep.Ini_Grid()
                    FrmObj = GridReportFrm

                Case MDI.MnuLocalFreightReport.Name
                    Dim CRep As ClsLedgerHeadSummary = New ClsLedgerHeadSummary(GridReportFrm, "LF")
                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    CRep.Ini_Grid()
                    FrmObj = GridReportFrm


                Case MDI.MnuMoneyReceiptReport.Name
                    Dim CRep As ClsPaymentAndReceiptReport = New ClsPaymentAndReceiptReport(GridReportFrm, Ncat.Receipt + "," + Ncat.VisitReceipt + "," + Ncat.ReceiptSettlement)
                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    CRep.Ini_Grid()
                    FrmObj = GridReportFrm
                Case MDI.MnuPaymentReport.Name
                    Dim CRep As ClsPaymentAndReceiptReport = New ClsPaymentAndReceiptReport(GridReportFrm, Ncat.Payment + "," + Ncat.PaymentSettlement)
                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    CRep.Ini_Grid()
                    FrmObj = GridReportFrm

                Case MDI.MnuLRUpdation.Name
                    Dim CRep As ClsLRUpdation = New ClsLRUpdation(GridReportFrm)
                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    CRep.Ini_Grid()
                    FrmObj = GridReportFrm

'#Region "Carpet Reports"
'                Case MDI.MnuDyeingOrderReport.Name
'                    Dim CRep As ClsPurchaseReport = New ClsPurchaseReport(GridReportFrm, ClsCarpet.NCat_DyeingOrder, "")
'                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
'                    CRep.Ini_Grid()

'                Case MDI.MnuDyeingReceiveReport.Name
'                    Dim CRep As ClsStockHeadReport = New ClsStockHeadReport(GridReportFrm, ClsCarpet.NCat_DyeingReceive)
'                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
'                    CRep.Ini_Grid()

'                Case MDI.MnuDyeingInvoiceReport.Name
'                    Dim CRep As ClsPurchaseReport = New ClsPurchaseReport(GridReportFrm, ClsCarpet.NCat_DyeingInvoice, "")
'                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
'                    CRep.Ini_Grid()

'                Case MDI.MnuDyeingOrderStatusReport.Name
'                    Dim CRep As ClsPurchOrderStatusReport = New ClsPurchOrderStatusReport(GridReportFrm, ClsCarpet.NCat_DyeingOrder)
'                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
'                    CRep.Ini_Grid()

'                Case MDI.MnuWeavingOrderReport.Name
'                    Dim CRep As ClsPurchaseReport = New ClsPurchaseReport(GridReportFrm, ClsCarpet.NCat_WeavingOrder, "")
'                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
'                    CRep.Ini_Grid()

'                Case MDI.MnuWeavingReceiveReport.Name
'                    Dim CRep As ClsStockHeadReport = New ClsStockHeadReport(GridReportFrm, ClsCarpet.NCat_WeavingReceive)
'                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
'                    CRep.Ini_Grid()

'                Case MDI.MnuWeavingInvoiceReport.Name
'                    Dim CRep As ClsPurchaseReport = New ClsPurchaseReport(GridReportFrm, ClsCarpet.NCat_WeavingInvoice, "")
'                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
'                    CRep.Ini_Grid()

'                Case MDI.MnuWeavingOrderStatusReport.Name
'                    Dim CRep As ClsPurchOrderStatusReport = New ClsPurchOrderStatusReport(GridReportFrm, ClsCarpet.NCat_WeavingOrder)
'                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
'                    CRep.Ini_Grid()

'                Case MDI.MnuFinishingOrderReport.Name
'                    Dim CRep As ClsPurchaseReport = New ClsPurchaseReport(GridReportFrm, ClsCarpet.NCat_FinishingOrder, "")
'                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
'                    CRep.Ini_Grid()

'                Case MDI.MnuFinishingReceiveReport.Name
'                    Dim CRep As ClsStockHeadReport = New ClsStockHeadReport(GridReportFrm, ClsCarpet.NCat_WeavingReceive)
'                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
'                    CRep.Ini_Grid()

'                Case MDI.MnuFinishingInvoiceReport.Name
'                    Dim CRep As ClsPurchaseReport = New ClsPurchaseReport(GridReportFrm, ClsCarpet.NCat_FinishingInvoice, "")
'                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
'                    CRep.Ini_Grid()

'                Case MDI.MnuFinishingOrderStatusReport.Name
'                    Dim CRep As ClsPurchOrderStatusReport = New ClsPurchOrderStatusReport(GridReportFrm, ClsCarpet.NCat_FinishingOrder)
'                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
'                    CRep.Ini_Grid()

'#End Region


                Case MDI.MnuOpeningStockReport.Name
                    Dim CRep As ClsPurchaseReport = New ClsPurchaseReport(GridReportFrm, Ncat.OpeningStock, "")
                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    CRep.Ini_Grid()
                    FrmObj = GridReportFrm
                Case MDI.MnuStockIssueReport.Name
                    If ClsMain.FDivisionNameForCustomization(6) = "SADHVI" And AgL.StrCmp(AgL.PubDBName, "Sadhvi") Then
                        Dim CRep As ClsStockHeadReport = New ClsStockHeadReport(GridReportFrm, Ncat.StockIssue)
                        CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                        CRep.Ini_Grid()
                    Else
                        Dim CRep As ClsPurchaseReport = New ClsPurchaseReport(GridReportFrm, Ncat.StockIssue, mSubRecordType_StockIssue)
                        CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                        CRep.Ini_Grid()
                    End If

                    FrmObj = GridReportFrm
                Case MDI.MnuStockReceiveReport.Name
                    Dim CRep As ClsPurchaseReport = New ClsPurchaseReport(GridReportFrm, Ncat.StockReceive, "")
                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    CRep.Ini_Grid()
                    FrmObj = GridReportFrm

                Case MDI.MnuJobOrderReport.Name
                    Dim CRep As ClsPurchaseReport = New ClsPurchaseReport(GridReportFrm, Ncat.JobOrder, "")
                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    CRep.Ini_Grid()
                    FrmObj = GridReportFrm
                Case MDI.MnuJobReceiveReport.Name
                    Dim CRep As ClsPurchaseReport = New ClsPurchaseReport(GridReportFrm, Ncat.JobReceive, "")
                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    CRep.Ini_Grid()
                    FrmObj = GridReportFrm
                Case MDI.MnuJobInvoiceReport.Name
                    Dim CRep As ClsPurchaseReport = New ClsPurchaseReport(GridReportFrm, Ncat.JobInvoice, "")
                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    CRep.Ini_Grid()
                    FrmObj = GridReportFrm
                Case MDI.MnuJobOrderStatusReport.Name, MDI.MnuJobOrderStatusReport_FallPico.Name
                    Dim CRep As ClsPurchOrderStatusReport = New ClsPurchOrderStatusReport(GridReportFrm, Ncat.JobOrder)
                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    CRep.Ini_Grid()
                    FrmObj = GridReportFrm

                Case MDI.MnuWorkOrderJobStatusReport.Name
                    Dim CRep As ClsWorkOrderJobStatusReport = New ClsWorkOrderJobStatusReport(GridReportFrm)
                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    CRep.Ini_Grid()
                    FrmObj = GridReportFrm



                Case MDI.MnuPurchaseOrderStatusReport.Name
                    Dim CRep As ClsPurchOrderStatusReport = New ClsPurchOrderStatusReport(GridReportFrm, Ncat.PurchaseOrder)
                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    CRep.Ini_Grid()
                    FrmObj = GridReportFrm





                Case MDI.MnuStockReport.Name
                    Dim CRep As ClsStockReport = New ClsStockReport(GridReportFrm)
                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    CRep.Ini_Grid()
                    FrmObj = GridReportFrm



                Case MDI.MnuFinishedStockReport.Name
                    Dim CRep As ClsStockReport = New ClsStockReport(GridReportFrm)
                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    CRep.Ini_Grid()
                    CRep.ReportFrm.FilterGrid.Item(ClsStockReport.GFilterCode, CRep.rowGroupOn).Value = "ItemCategoryCode,Dimension1Code,Dimension2Code,SizeCode"
                    CRep.ReportFrm.FilterGrid.Item(ClsStockReport.GFilter, CRep.rowGroupOn).Value = "Item Category,Quality,Colour,Size"
                    CRep.ReportFrm.FilterGrid.Rows(CRep.rowGroupOn).Visible = False
                    CRep.ReportFrm.FilterGrid.Item(ClsStockReport.GFilterCode, CRep.rowItemType).Value = "'" + ItemTypeCode.ManufacturingProduct + "'"
                    CRep.ReportFrm.FilterGrid.Item(ClsStockReport.GFilter, CRep.rowItemType).Value = AgL.XNull(AgL.Dman_Execute("SELECT Name FROM ItemType WHERE Code = '" & ItemTypeCode.ManufacturingProduct & "'", AgL.GCn).ExecuteScalar())
                    CRep.ReportFrm.FilterGrid.Rows(CRep.rowItemType).Visible = False
                    FrmObj = GridReportFrm

                Case MDI.MnuTradingStockReport.Name
                    Dim CRep As ClsStockReport = New ClsStockReport(GridReportFrm)
                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    CRep.Ini_Grid()
                    CRep.ReportFrm.FilterGrid.Item(ClsStockReport.GFilterCode, CRep.rowGroupOn).Value = "ItemCategoryCode,Dimension1Code,Dimension2Code,SizeCode"
                    CRep.ReportFrm.FilterGrid.Item(ClsStockReport.GFilter, CRep.rowGroupOn).Value = "Item Category,Quality,Colour,Size"
                    CRep.ReportFrm.FilterGrid.Rows(CRep.rowGroupOn).Visible = False
                    CRep.ReportFrm.FilterGrid.Item(ClsStockReport.GFilterCode, CRep.rowItemType).Value = "'" + ItemTypeCode.TradingProduct + "'"
                    CRep.ReportFrm.FilterGrid.Item(ClsStockReport.GFilter, CRep.rowItemType).Value = AgL.XNull(AgL.Dman_Execute("SELECT Name FROM ItemType WHERE Code = '" & ItemTypeCode.TradingProduct & "'", AgL.GCn).ExecuteScalar())
                    CRep.ReportFrm.FilterGrid.Rows(CRep.rowItemType).Visible = False
                    FrmObj = GridReportFrm

                Case MDI.MnuRawMaterialStockReport.Name
                    Dim CRep As ClsStockReport = New ClsStockReport(GridReportFrm)
                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    CRep.Ini_Grid()
                    CRep.ReportFrm.FilterGrid.Item(ClsStockReport.GFilterCode, CRep.rowGroupOn).Value = "ItemCategoryCode,Dimension1Code,Dimension2Code,Dimension4Code"
                    CRep.ReportFrm.FilterGrid.Item(ClsStockReport.GFilter, CRep.rowGroupOn).Value = "Item Category,Quality,Colour,Width"
                    CRep.ReportFrm.FilterGrid.Rows(CRep.rowGroupOn).Visible = False
                    CRep.ReportFrm.FilterGrid.Item(ClsStockReport.GFilterCode, CRep.rowItemType).Value = "'" + ItemTypeCode.RawProduct + "'"
                    CRep.ReportFrm.FilterGrid.Item(ClsStockReport.GFilter, CRep.rowItemType).Value = AgL.XNull(AgL.Dman_Execute("SELECT Name FROM ItemType WHERE Code = '" & ItemTypeCode.RawProduct & "'", AgL.GCn).ExecuteScalar())
                    CRep.ReportFrm.FilterGrid.Rows(CRep.rowItemType).Visible = False
                    FrmObj = GridReportFrm

                Case MDI.MnuOtherRawMaterialStockReport.Name
                    Dim CRep As ClsStockReport = New ClsStockReport(GridReportFrm)
                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    CRep.Ini_Grid()
                    CRep.ReportFrm.FilterGrid.Item(ClsStockReport.GFilterCode, CRep.rowGroupOn).Value = "ItemCategoryCode,ItemCode"
                    CRep.ReportFrm.FilterGrid.Item(ClsStockReport.GFilter, CRep.rowGroupOn).Value = "Item Category,Item"
                    CRep.ReportFrm.FilterGrid.Rows(CRep.rowGroupOn).Visible = False
                    CRep.ReportFrm.FilterGrid.Item(ClsStockReport.GFilterCode, CRep.rowItemType).Value = "'" + ItemTypeCode.OtherRawProduct + "'"
                    CRep.ReportFrm.FilterGrid.Item(ClsStockReport.GFilter, CRep.rowItemType).Value = AgL.XNull(AgL.Dman_Execute("SELECT Name FROM ItemType WHERE Code = '" & ItemTypeCode.OtherRawProduct & "'", AgL.GCn).ExecuteScalar())
                    CRep.ReportFrm.FilterGrid.Rows(CRep.rowItemType).Visible = False
                    FrmObj = GridReportFrm

                Case Else
                    CRep = New ClsReports(GridReportFrm)
                    CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
                    CRep.Ini_Grid()
                    FrmObj = GridReportFrm
            End Select


            'GridReportFrm = New AgLibrary.FrmRepDisplay(StrSenderText, AgL)
            'GridReportFrm.Filter_IniGrid()
            'CRep = New ClsReports(GridReportFrm)
            'CRep.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
            'CRep.Ini_Grid()
            'FrmObj = GridReportFrm
        Else
            ReportFrm = New AgLibrary.FrmReportLayout(AgL, "", "", StrSenderText, "")
            CRepProc = New ClsReportProcedures(ReportFrm)
            CRepProc.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
            CRepProc.Ini_Grid()
            FrmObj = ReportFrm
        End If
        If FrmObj IsNot Nothing Then
            FrmObj.Text = StrSenderText
        End If
        Return FrmObj
    End Function

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Public Shared Function GetPlaceOfSupply(CityCode As String, SalesTaxGroupRegType As String) As String
        Dim mStateCode As String

        If CityCode = "" Then GetPlaceOfSupply = "Within State" : Exit Function
        If SalesTaxGroupRegType Is Nothing Then SalesTaxGroupRegType = ""
        If SalesTaxGroupRegType.ToUpper = "SEZ" Then GetPlaceOfSupply = "Outside State" : Exit Function

        mStateCode = AgL.Dman_Execute("Select IfNull(Max(State),'') From City Where CityCode = '" & CityCode & "'", AgL.GCn).ExecuteScalar()
        If mStateCode = AgL.PubSiteStateCode Or mStateCode = "" Then
            GetPlaceOfSupply = "Within State"
        Else
            GetPlaceOfSupply = "Outside State"
        End If
    End Function
    Public Shared Function ValidateGstNo(GstNo As String, RegistrationType As String, StateCode As String, AllIndiaParty As Boolean) As Boolean
        Dim mReason As String = ""

        'If RegistrationType.ToUpper = "REGISTERED" Or RegistrationType.ToUpper = "COMPOSITION" Then
        If GstNo = "" Then
            If RegistrationType.ToUpper <> "UNREGISTERED" And RegistrationType <> "" Then
                mReason = "Gst No. Can not be blank"
            End If
        ElseIf Len(GstNo) <> 15 Then
            mReason = "Gst No. should be of 15 characters. Currently It is " & Len(GstNo).ToString
        ElseIf GstNo.ToString.Substring(0, 2) <> StateCode And Not AllIndiaParty Then
            mReason = "First two characteres of gst no are not matching with state code"
        Else
            If Not System.Text.RegularExpressions.Regex.IsMatch(GstNo, "[0-9]{2}[a-zA-Z]{5}[0-9]{4}[a-zA-Z]{1}[1-9A-Za-z]{1}[Z]{1}[0-9a-zA-Z]{1}") Then
                mReason = "Some thing wrong in the given GST No."
            End If
        End If
        If mReason <> "" Then
            MsgBox(mReason)
            ValidateGstNo = False
        Else
            ValidateGstNo = True
        End If
    End Function

    Public Shared Sub PostStructureLineToAccounts(ByVal FGMain As AgStructure.AgCalcGrid, ByVal mNarrParty As String, ByVal mNarr As String, ByVal mDocID As String, ByVal mDiv_Code As String,
                                               ByVal mSite_Code As String, ByVal Div_Code As String, ByVal mV_Type As String, ByVal mV_Prefix As String, ByVal mV_No As Integer,
                                               ByVal mRecID As String, ByVal PostingPartyAc As String, ByVal mV_Date As String,
                                               ByVal Conn As Object, ByVal Cmd As Object, Optional ByVal mCostCenter As String = "", Optional MultiplyWithMinus As Boolean = False, Optional LinkedPartyAc As String = "")
        Dim StrContraTextJV As String = ""
        Dim mPostSubCode = ""
        Dim mLinkedSubCode = ""
        Dim I As Integer, J As Integer
        Dim mQry$ = "", bSelectionQry$ = ""
        Dim mTestingQry As String = ""
        Dim DtTemp As DataTable = Nothing

        Dim bTableName As String = "[" + Guid.NewGuid().ToString() + "]"

        If AgL.IsTableExist(bTableName.Replace("[", "").Replace("]", ""), IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)) Then
            mQry = "Drop Table " + bTableName
            AgL.Dman_ExecuteNonQry(mQry, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead))
        End If

        mQry = " CREATE TABLE " & bTableName & "(TmpCol INTEGER, PostAc NVARCHAR(255), Amount Float) "
        AgL.Dman_ExecuteNonQry(mQry, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead))


        bSelectionQry = ""
        For I = 0 To FGMain.Rows.Count - 1
            For J = 0 To FGMain.AgLineGrid.Rows.Count - 1
                If FGMain.AgLineGrid.Rows(J).Visible Then
                    If AgL.XNull(FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.PostAc)) <> "" Then
                        'If bSelectionQry <> "" Then bSelectionQry += " UNION ALL "

                        bSelectionQry = " INSERT INTO " & bTableName & "(TmpCol, PostAc, Amount)"
                        bSelectionQry += " Select 1 As TmpCol, '" & FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.PostAc) & "' As PostAc, " &
                        " Case When " & AgL.Chk_Text(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_DrCr, I).Value) & " = 'Dr' Then " & Val(FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.Amount)) * 1.0 & "  " &
                        "      When " & AgL.Chk_Text(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_DrCr, I).Value) & " = 'Cr' Then " & -Val(FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.Amount)) * 1.0 & " End As Amount "
                        AgL.Dman_ExecuteNonQry(bSelectionQry, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead))

                        mTestingQry += " Select 1 As TmpCol, '" & FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.PostAc) & "' As PostAc, " &
                        " Case When " & AgL.Chk_Text(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_DrCr, I).Value) & " = 'Dr' Then " & Val(FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.Amount)) * 1.0 & "  " &
                        "      When " & AgL.Chk_Text(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_DrCr, I).Value) & " = 'Cr' Then " & -Val(FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.Amount)) * 1.0 & " End As Amount "



                    ElseIf Trim(AgL.XNull(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_PostAc, I).Value)) <> "" Then
                        'If bSelectionQry <> "" Then bSelectionQry += " UNION ALL "

                        bSelectionQry = " INSERT INTO " & bTableName & "(TmpCol, PostAc, Amount)"
                        bSelectionQry += " Select 1 as TmpCol,'" & FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_PostAc, I).Value & "' As PostAc, " &
                        " Case When " & AgL.Chk_Text(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_DrCr, I).Value) & " = 'Dr' Then " & Val(FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.Amount)) * 1.0 & "  " &
                        "      When " & AgL.Chk_Text(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_DrCr, I).Value) & " = 'Cr' Then " & -Val(FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.Amount)) * 1.0 & " End As Amount "
                        AgL.Dman_ExecuteNonQry(bSelectionQry, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead))

                        mTestingQry += " Select 1 as TmpCol,'" & FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_PostAc, I).Value & "' As PostAc, " &
                        " Case When " & AgL.Chk_Text(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_DrCr, I).Value) & " = 'Dr' Then " & Val(FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.Amount)) * 1.0 & "  " &
                        "      When " & AgL.Chk_Text(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_DrCr, I).Value) & " = 'Cr' Then " & -Val(FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.Amount)) * 1.0 & " End As Amount "

                    End If

                    If Val(FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.Amount)) <> 0 Then
                        If AgL.Chk_Text(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_DrCr, I).Value) Is Nothing Then
                            Err.Raise(1, , "Error In Ledger Posting. Dr/Cr not defined for any value.")
                        End If
                    End If
                End If
            Next
        Next

        bSelectionQry = " Select * From " & bTableName & " With (NoLock) "
        Dim DtDebug As DataTable = AgL.FillData(bSelectionQry, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).Tables(0)

        mQry = " Select Count(*)  " &
                " From (" & bSelectionQry & ") As V1 Group by tmpCol " &
                " Having Round(Sum(Case When IfNull(V1.Amount*1.0,0) > 0 Then IfNull(V1.Amount*1.0,0) Else 0 End),3) <> Round(abs(Sum(Case When IfNull(V1.Amount*1.0,0) < 0 Then IfNull(V1.Amount*1.0,0) Else 0 End)),3)  "
        DtTemp = AgL.FillData(mQry, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).Tables(0)
        If DtTemp.Rows.Count > 0 Then
            If AgL.VNull(DtTemp.Rows(0)(0)) > 0 Then
                Console.Write(mQry)
                Err.Raise(1, , "Error In Ledger Posting. Debit and Credit balances are not equal.")
            End If
        End If


        If MultiplyWithMinus Then
            mQry = " Select V1.PostAc, IfNull(Sum(Cast(V1.Amount as Float)),0) As Amount, " &
                " Case When IfNull(Sum(V1.Amount),0) > 0 Then 'Cr' " &
                "      When IfNull(Sum(V1.Amount),0) < 0 Then 'Dr' End As DrCr " &
                " From (" & bSelectionQry & ") As V1 " &
                " Group BY V1.PostAc "
        Else
            mQry = " Select V1.PostAc, IfNull(Sum(Cast(V1.Amount as Float)),0) As Amount, " &
                " Case When IfNull(Sum(V1.Amount),0) > 0 Then 'Dr' " &
                "      When IfNull(Sum(V1.Amount),0) < 0 Then 'Cr' End As DrCr " &
                " From (" & bSelectionQry & ") As V1 " &
                " Group BY V1.PostAc "
        End If

        DtTemp = AgL.FillData(mQry, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).Tables(0)

        With DtTemp
            For I = 0 To .Rows.Count - 1
                If Trim(AgL.XNull(.Rows(I)("PostAc"))) <> "" Then
                    If AgL.StrCmp(AgL.XNull(.Rows(I)("PostAc")), "|PARTY|") Then
                        If AgL.VNull(.Rows(I)("Amount")) <> 0 And AgL.XNull(.Rows(I)("DrCr")) <> "" Then
                            If StrContraTextJV <> "" Then StrContraTextJV += vbCrLf
                            FPrepareContraText(False, StrContraTextJV, PostingPartyAc, Math.Abs(AgL.VNull(.Rows(I)("Amount"))), AgL.XNull(.Rows(I)("DrCr")))
                        End If
                    Else
                        If AgL.VNull(.Rows(I)("Amount")) <> 0 And AgL.XNull(.Rows(I)("DrCr")) <> "" Then
                            If StrContraTextJV <> "" Then StrContraTextJV += vbCrLf
                            FPrepareContraText(False, StrContraTextJV, AgL.XNull(.Rows(I)("PostAc")), Math.Abs(Val(AgL.VNull(.Rows(I)("Amount")))), AgL.XNull(.Rows(I)("DrCr")))
                        End If
                    End If
                End If
            Next
        End With

        Dim mSrl As Integer = 0, mDebit As Double, mCredit As Double

        With DtTemp
            For I = 0 To .Rows.Count - 1
                If Trim(AgL.XNull(.Rows(I)("PostAc"))) <> "" And Val(AgL.VNull(.Rows(I)("Amount"))) <> 0 Then
                    mSrl += 1

                    mDebit = 0 : mCredit = 0
                    If AgL.StrCmp(AgL.XNull(.Rows(I)("PostAc")), "|PARTY|") Then
                        mPostSubCode = PostingPartyAc
                        mLinkedSubCode = LinkedPartyAc
                    Else
                        mPostSubCode = AgL.XNull(.Rows(I)("PostAc"))
                    End If

                    If AgL.StrCmp(AgL.XNull(.Rows(I)("DrCr")), "Dr") Then
                        mDebit = Math.Abs(AgL.VNull(.Rows(I)("Amount")))
                    ElseIf AgL.StrCmp(AgL.XNull(.Rows(I)("DrCr")), "Cr") Then
                        mCredit = Math.Abs(AgL.VNull(.Rows(I)("Amount")))
                    End If

                    mQry = "Insert Into Ledger(DocId,RecId,V_SNo,V_Date,SubCode,ContraSub,AmtDr,AmtCr," &
                         " Narration,V_Type,V_No,V_Prefix,Site_Code,DivCode,Chq_No,Chq_Date,TDSCategory,TDSOnAmt,TDSDesc," &
                         " TDSPer,TDS_Of_V_SNo,System_Generated,FormulaString,ContraText, CostCenter, LinkedSubCode) Values " &
                         " ('" & mDocID & "','" & mRecID & "'," & mSrl & "," & AgL.Chk_Date(CDate(mV_Date).ToString("s")) & "," & AgL.Chk_Text(mPostSubCode) & "," & AgL.Chk_Text("") & ", " &
                         " " & mDebit & "," & mCredit & ", " &
                         " " & AgL.Chk_Text(IIf(AgL.StrCmp(AgL.XNull(.Rows(I)("PostAc")), "|PARTY|"), mNarrParty, mNarr)) & ",'" & mV_Type & "','" & mV_No & "','" & mV_Prefix & "'," &
                         " '" & mSite_Code & "','" & mDiv_Code & "'," & AgL.Chk_Text("") & "," &
                         " " & AgL.Chk_Text("") & "," & AgL.Chk_Text("") & "," &
                         " " & Val("") & "," & AgL.Chk_Text("") & "," & Val("") & "," & 0 & ",'Y','" & "" & "', " & AgL.Chk_Text(StrContraTextJV) & ", " & AgL.Chk_Text(mCostCenter) & ", 
                         " & AgL.Chk_Text(mLinkedSubCode) & ")"
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                End If
            Next I
        End With

        If AgL.IsTableExist(bTableName.Replace("[", "").Replace("]", ""), IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)) Then
            mQry = "Drop Table " + bTableName
            AgL.Dman_ExecuteNonQry(mQry, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead))
        End If
    End Sub
    Public Shared Sub FPrepareContraText(ByVal BlnOverWrite As Boolean, ByRef StrContraTextVar As String,
                                       ByVal StrContraName As String, ByVal DblAmount As Double, ByVal StrDrCr As String)
        Dim IntNameMaxLen As Integer = 35, IntAmtMaxLen As Integer = 18, IntSpaceNeeded As Integer = 2
        StrContraName = AgL.XNull(AgL.Dman_Execute("Select Name from Subgroup  Where SubCode = '" & StrContraName & "'  ", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar)

        If BlnOverWrite Then
            StrContraTextVar = Mid(Trim(StrContraName), 1, IntNameMaxLen) & Space((IntNameMaxLen + IntSpaceNeeded) - Len(Mid(Trim(StrContraName), 1, IntNameMaxLen))) & Space(IntAmtMaxLen - Len(Format(Val(DblAmount), "##,##,##,##,##0.00"))) & Format(Val(DblAmount), "##,##,##,##,##0.00") & " " & Trim(StrDrCr)
        Else
            StrContraTextVar += Mid(Trim(StrContraName), 1, IntNameMaxLen) & Space((IntNameMaxLen + IntSpaceNeeded) - Len(Mid(Trim(StrContraName), 1, IntNameMaxLen))) & Space(IntAmtMaxLen - Len(Format(Val(DblAmount), "##,##,##,##,##0.00"))) & Format(Val(DblAmount), "##,##,##,##,##0.00") & " " & Trim(StrDrCr)
        End If
    End Sub
    Private Function FGetNewVersionFlag() As Boolean
        If ClsMain.FDivisionNameForCustomization().Contains("SADHVI") Or
               ClsMain.FDivisionNameForCustomization().Contains("SHYAMA") Or
               ClsMain.FDivisionNameForCustomization().Contains("VAISHNO") Or
               ClsMain.FDivisionNameForCustomization().Contains("PARWATI") Or
               ClsMain.FDivisionNameForCustomization().Contains("MANOJ") Or
               ClsMain.FDivisionNameForCustomization().Contains("ANJANI") Or
               ClsMain.FDivisionNameForCustomization().Contains("SECOND DIVISION") Or
               ClsMain.FDivisionNameForCustomization().Contains("RAM PRAKASH SHYAM SWAROOP") Or
               ClsMain.FDivisionNameForCustomization().Contains("K.K. SONS") Or
               ClsMain.FDivisionNameForCustomization().Contains("R.H. TRADERS") Or
               ClsMain.FDivisionNameForCustomization().Contains("SHREE SAMRIDHI FAB") Or
               ClsMain.FDivisionNameForCustomization().Contains("SUMAN") Or
               ClsMain.FDivisionNameForCustomization().Contains("AARNAV") Or
               ClsMain.FDivisionNameForCustomization().Contains("RADHA") Or
               ClsMain.FDivisionNameForCustomization().Contains("SHREE") Or
               ClsMain.FDivisionNameForCustomization().Contains("SUBHASHINI FAB") Or
               ClsMain.FDivisionNameForCustomization().Contains("SITARAM HARISH") Or
               ClsMain.FDivisionNameForCustomization().Contains("SHREE RAM") Then
            FGetNewVersionFlag = False
        Else
            FGetNewVersionFlag = True
        End If
    End Function
    Private Function FGetOldVoucherEntryFlag() As Boolean
        If ClsMain.FDivisionNameForCustomization().Contains("SADHVI") Or
               ClsMain.FDivisionNameForCustomization().Contains("SHIVA") Or
               ClsMain.FDivisionNameForCustomization().Contains("SHREE AMIT") Or
               ClsMain.FDivisionNameForCustomization().Contains("BHAGWANT") Or
               ClsMain.FDivisionNameForCustomization().Contains("SHYAMA") Or
               ClsMain.FDivisionNameForCustomization().Contains("HARSHEEN") Or
               ClsMain.FDivisionNameForCustomization().Contains("NEW SUPER") Or
               ClsMain.FDivisionNameForCustomization().Contains("PRATHAM") Or
               ClsMain.FDivisionNameForCustomization().Contains("VAISHNO") Or
               ClsMain.FDivisionNameForCustomization().Contains("MANOJ") Or
               ClsMain.FDivisionNameForCustomization().Contains("PAWAN") Or
               ClsMain.FDivisionNameForCustomization().Contains("ANJANI") Or
               ClsMain.FDivisionNameForCustomization().Contains("SUMAN") Or
               ClsMain.FDivisionNameForCustomization().Contains("AARNAV") Or
               ClsMain.FDivisionNameForCustomization().Contains("SECOND DIVISION") Or
               ClsMain.FDivisionNameForCustomization().Contains("RAM PRAKASH SHYAM SWAROOP") Or
               ClsMain.FDivisionNameForCustomization().Contains("K.K. SONS") Or
               ClsMain.FDivisionNameForCustomization().Contains("R.H. TRADERS") Or
               ClsMain.FDivisionNameForCustomization().Contains("SHREE SAMRIDHI FAB") Or
               ClsMain.FDivisionNameForCustomization().Contains("RADHA") Or
               ClsMain.FDivisionNameForCustomization().Contains("SHREE RAM") Then
            FGetOldVoucherEntryFlag = True
        Else
            FGetOldVoucherEntryFlag = False
        End If

        If CDate(AgL.PubLoginDate) >= CDate("01/Apr/2020") Then
            FGetOldVoucherEntryFlag = False
        End If
    End Function
End Class

