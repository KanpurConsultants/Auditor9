Imports AgLibrary.ClsMain.agConstants
Imports Customised.ClsMain
Public Class ClsKirana
    Private mQry As String = ""
    Private mItemTypeFieldQry As String = ""
    Public Const SubGroupType_SubParty As String = "Sub Party"
    Dim mSubGroupTypeFieldQry As String = "SELECT SubgroupType AS Code, SubgroupType AS Name  FROM SubGroupType ORDER BY SubgroupType"
    Public Sub FSeedData_Kirana()
        Dim ClsObj As New Customised.ClsMain(AgL)
        Try
            If ClsMain.IsScopeOfWorkContains(IndustryType.KiranaIndustry) Then
                FInitVariables()
                FConfigure_SubGroupType(ClsObj)
                FConfigure_Voucher_Type(ClsObj)
                FConfigure_DebtorsAndCreditorsOpening(ClsObj)
                FAlterTable_LedgerHead()
                FAlterTable_LedgerSettlement()
                FAlterTable_SaleInvoice()
                FAlterTable_SaleInvoiceDetail()
                FAlterTable_PurchInvoice()
                FAlterTable_PurchInvoiceDetail()
                FConfigure_Payment(ClsObj)
                FConfigure_Receipt(ClsObj)
                FConfigure_SaleOrderSettlement(ClsObj)
                FConfigure_PurchaseOrderSettlement(ClsObj)
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " In FSeedData_KiranaIndustry")
        End Try
    End Sub
    Private Sub FInitVariables()
        mItemTypeFieldQry = "SELECT Code, Name FROM ItemType Order By Name"
    End Sub
    Private Sub FConfigure_SubGroupType(ClsObj As ClsMain)
        If AgL.FillData("Select * from SubGroupType Where SubgroupType='" & SubGroupType_SubParty & "'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
            mQry = " Insert Into SubGroupType (SubgroupType,IsCustomUI,IsActive, Parent)
                        Values ('" & SubGroupType_SubParty & "',0, 1, '" & SubGroupType_SubParty & "');                    "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If

        If AgL.FillData("Select * from SubgroupTypeSetting Where SubgroupType='" & SubGroupType_SubParty & "'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
            mQry = "INSERT INTO SubgroupTypeSetting (SubgroupType, Div_Code, Site_Code, AcGroupCode, PersonCanHaveSiteWiseAgentYn, PersonCanHaveDivisionWiseAgentYn, PersonCanHaveSiteWiseTransporterYn, PersonCanHaveDivisionWiseTransporterYn, PersonCanHaveSiteWiseRateTypeYn, PersonCanHaveDivisionWiseRateTypeYn, PersonCanHaveItemGroupWiseInterestSlabYn, PersonCanHaveItemCategoryWiseInterestSlabYn, PersonCanHaveItemGroupWiseDiscountYn, PersonCanHaveItemCategoryWiseDiscountYn, PersonCanHaveOwnDistanceYn, Default_SalesTaxGroupPerson, FilterInclude_SubgroupTypeForMasterParty)
                    VALUES ('" & SubGroupType_SubParty & "', NULL, NULL, '0020', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 'Unregistered', NULL) "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If

        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubGroupType_SubParty, "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.SubgroupType, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubGroupType_SubParty, "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.Code, 0, 0, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubGroupType_SubParty, "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.Name, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubGroupType_SubParty, "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.PrintingDescription, 1, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubGroupType_SubParty, "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.Address, 1, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubGroupType_SubParty, "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.City, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubGroupType_SubParty, "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.Pincode, 1, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubGroupType_SubParty, "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.ContactNo, 0, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubGroupType_SubParty, "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.Mobile, 1, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubGroupType_SubParty, "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.Email, 0, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubGroupType_SubParty, "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.AcGroup, 1, 1, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubGroupType_SubParty, "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.SalesTaxGroup, 0, 0, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubGroupType_SubParty, "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.SalesTaxGroupRegType, 0, 0, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubGroupType_SubParty, "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.ContactPerson, 0, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubGroupType_SubParty, "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.SalesTaxNo, 0, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubGroupType_SubParty, "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.PanNo, 0, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubGroupType_SubParty, "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.AadharNo, 0, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubGroupType_SubParty, "Dgl1", FrmPerson.hcLicenseNo, 0, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubGroupType_SubParty, "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.Parent, 1, 0, 0, "Broker")
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubGroupType_SubParty, "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.Area, 0, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubGroupType_SubParty, "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.Agent, 0, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubGroupType_SubParty, "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.Transporter, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubGroupType_SubParty, "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.InterestSlab, 0, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubGroupType_SubParty, "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.RateType, 0, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubGroupType_SubParty, "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.CreditDays, 0, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubGroupType_SubParty, "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.CreditLimit, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubGroupType_SubParty, "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.Discount, 0, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubGroupType_SubParty, "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.Addition, 0, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubGroupType_SubParty, "Dgl1", FrmPerson.hcBankName, 0, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubGroupType_SubParty, "Dgl1", FrmPerson.hcBankAccount, 0, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubGroupType_SubParty, "Dgl1", FrmPerson.hcBankIFSC, 0, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubGroupType_SubParty, "Dgl1", FrmPerson.hcShowAccountInOtherDivisions, 0, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubGroupType_SubParty, "Dgl1", FrmPerson.hcShowAccountInOtherSites, 0, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubGroupType_SubParty, "Dgl1", FrmPerson.hcWeekOffDays, 0, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubGroupType_SubParty, "Dgl1", FrmPerson.hcRelationshipExecutive, 0, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubGroupType_SubParty, "Dgl1", FrmPerson.hcProcesses, 0, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubGroupType_SubParty, "Dgl1", FrmPerson.hcSalesRepresentative, 0, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubGroupType_SubParty, "Dgl1", FrmPerson.hcSalesRepresentativeCommissionPer, 0, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubGroupType_SubParty, "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.Remarks, 0, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubGroupType_SubParty, "Dgl1", FrmPerson.hcBlockedTransactions, 0, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubGroupType_SubParty, "Dgl1", FrmPerson.hcGrade, 0, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubGroupType_SubParty, "Dgl1", FrmPerson.hcReconciliationUpToDate, 0, 0)
    End Sub
    Private Sub FConfigure_Voucher_Type(ClsObj As ClsMain)
        Dim MdiObj As New MDIMain
        ClsObj.FSeedSingleIfNotExists_Voucher_Type("OBD", "Opening Balance Debtors", Ncat.OpeningBalance, VoucherCategory.Journal, "", "Customised", MdiObj.MnuDebtorsOpeningEntry.Name, MdiObj.MnuDebtorsOpeningEntry.Text)

        mQry = " UPDATE Voucher_Type Set CustomUI = '" & mCustomUI_OpeningBalanceDebtors & "' WHERE V_Type = 'OBD' "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)

        ClsObj.FSeedSingleIfNotExists_Voucher_Type("OBC", "Opening Balance Creditors", Ncat.OpeningBalance, VoucherCategory.Journal, "", "Customised", MdiObj.MnuCreditorsOpeningEntry.Name, MdiObj.MnuCreditorsOpeningEntry.Text)

        mQry = " UPDATE Voucher_Type Set CustomUI = '" & mCustomUI_OpeningBalanceCreditors & "' WHERE V_Type = 'OBC' "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
    End Sub
    Private Sub FConfigure_DebtorsAndCreditorsOpening(ClsObj As ClsMain)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.OpeningBalance, "DglMain", AgTemplate.TempTransaction1.hcSite_Code, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.OpeningBalance, "DglMain", AgTemplate.TempTransaction1.hcV_Type, 1, 1, 1, "Opening Type")
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.OpeningBalance, "DglMain", AgTemplate.TempTransaction1.hcV_Date, 1, 1, 1, "Opening Date")
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.OpeningBalance, "DglMain", AgTemplate.TempTransaction1.hcV_No, 0, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.OpeningBalance, "DglMain", AgTemplate.TempTransaction1.hcReferenceNo, 1, 1, 1, "Opening No")
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.OpeningBalance, "DglMain", AgTemplate.TempTransaction1.hcSettingGroup, 0, 0, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.OpeningBalance, "DglMain", FrmPurchInvoiceDirect_WithDimension.hcProcess, 0, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.OpeningBalance, "DglMain", FrmPurchInvoiceDirect_WithDimension.hcVendor, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.OpeningBalance, "DglMain", FrmPurchInvoiceDirect_WithDimension.hcBillToParty, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.OpeningBalance, "DglMain", FrmPurchInvoiceDirect_WithDimension.hcProcess, 0, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.OpeningBalance, "DglMain", FrmPurchInvoiceDirect_WithDimension.hcVendor, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.OpeningBalance, "DglMain", FrmPurchInvoiceDirect_WithDimension.hcBillToParty, 1, 1)

        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.OpeningBalance, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcStructure, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.OpeningBalance, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcVendorDocNo, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.OpeningBalance, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcVendorDocDate, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.OpeningBalance, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcDeliveryDate, 0, 0, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.OpeningBalance, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcAgent, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.OpeningBalance, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcTags, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.OpeningBalance, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcGodown, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.OpeningBalance, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcSalesTaxNo, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.OpeningBalance, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcCatalog, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.OpeningBalance, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcStockInNo, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.OpeningBalance, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcRemarks, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.OpeningBalance, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcBtnTransportDetail, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.OpeningBalance, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcBtnPendingPurchOrder, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.OpeningBalance, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcBtnPendingStockReceive, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.OpeningBalance, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcBtnAttachments)


        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningBalance, "Dgl1", FrmPurchInvoiceDirect_WithDimension.ColSNo, True, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningBalance, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Barcode, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningBalance, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCategory, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningBalance, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemGroup, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningBalance, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCode, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningBalance, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Item, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningBalance, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension1, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningBalance, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension2, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningBalance, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension3, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningBalance, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension4, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningBalance, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Size, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningBalance, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Specification, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningBalance, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1SalesTaxGroup, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningBalance, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1BaleNo, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningBalance, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1LotNo, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningBalance, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DocQty, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningBalance, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1LossQty)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningBalance, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Qty)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningBalance, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Unit, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningBalance, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1StockUnitMultiplier, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningBalance, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1StockUnit, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningBalance, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1StockQty, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningBalance, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Rate, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningBalance, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DiscountPer, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningBalance, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DiscountAmount, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningBalance, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionalDiscountPer, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningBalance, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionalDiscountAmount, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningBalance, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionPer, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningBalance, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionAmount, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningBalance, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Amount, True,,, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningBalance, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1MRP, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningBalance, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ProfitMarginPer, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningBalance, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1SaleRate, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningBalance, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1HSN, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningBalance, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Remark, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningBalance, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Godown, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningBalance, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1PurchaseInvoice, False, False)
    End Sub


    Private Sub FAlterTable_LedgerHead()
        Try
            AgL.AddFieldSqlite(AgL.GcnMain, "LedgerHead", "IsFinalPayment", "Bit", "0", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "LedgerHead", "PaidAmount", "Float", "0", True)
        Catch ex As Exception
            MsgBox(ex.Message & "  [FCreateTable_LedgerHead]")
        End Try
    End Sub
    Private Sub FAlterTable_LedgerSettlement()
        Dim mQry As String
        Try
            AgL.AddFieldSqlite(AgL.GcnMain, "LedgerSettlement", "SubTotal", "Float", "0", True)
        Catch ex As Exception
            MsgBox(ex.Message & "  [FCreateTable_LedgerSettlement]")
        End Try
    End Sub
    Private Sub FConfigure_Payment(ClsObj As ClsMain)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPaymentReceiptSettlement_Kirana", Ncat.Payment, "DglMain", AgTemplate.TempTransaction1.hcSite_Code, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPaymentReceiptSettlement_Kirana", Ncat.Payment, "DglMain", AgTemplate.TempTransaction1.hcV_Type, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPaymentReceiptSettlement_Kirana", Ncat.Payment, "DglMain", AgTemplate.TempTransaction1.hcV_Date, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPaymentReceiptSettlement_Kirana", Ncat.Payment, "DglMain", AgTemplate.TempTransaction1.hcV_No, 0, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPaymentReceiptSettlement_Kirana", Ncat.Payment, "DglMain", AgTemplate.TempTransaction1.hcReferenceNo, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPaymentReceiptSettlement_Kirana", Ncat.Payment, "DglMain", AgTemplate.TempTransaction1.hcSettingGroup, 0, 0, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPaymentReceiptSettlement_Kirana", Ncat.Payment, "DglMain", FrmPaymentReceiptSettlement_Kirana.hcSubCode, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPaymentReceiptSettlement_Kirana", Ncat.Payment, "DglMain", FrmPaymentReceiptSettlement_Kirana.hcLinkedSubCode, 1,,, "Sub Party")


        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPaymentReceiptSettlement_Kirana", Ncat.Payment, "Dgl2", FrmPaymentReceiptSettlement_Kirana.hcLineSubCode, 1, 1, 1, "Bank/Cash A/c")
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPaymentReceiptSettlement_Kirana", Ncat.Payment, "Dgl2", FrmPaymentReceiptSettlement_Kirana.hcIsFinalPayment, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPaymentReceiptSettlement_Kirana", Ncat.Payment, "Dgl2", FrmPaymentReceiptSettlement_Kirana.hcAmount, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPaymentReceiptSettlement_Kirana", Ncat.Payment, "Dgl2", FrmPaymentReceiptSettlement_Kirana.hcRemarks, 1)


        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPaymentReceiptSettlement_Kirana", Ncat.Payment, "Dgl1", FrmPaymentReceiptSettlement_Kirana.ColSNo, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPaymentReceiptSettlement_Kirana", Ncat.Payment, "Dgl1", FrmPaymentReceiptSettlement_Kirana.Col1Select, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPaymentReceiptSettlement_Kirana", Ncat.Payment, "Dgl1", FrmPaymentReceiptSettlement_Kirana.Col1TransactionDocID, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPaymentReceiptSettlement_Kirana", Ncat.Payment, "Dgl1", FrmPaymentReceiptSettlement_Kirana.Col1LinkedSubCode, True,, "Party")
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPaymentReceiptSettlement_Kirana", Ncat.Payment, "Dgl1", FrmPaymentReceiptSettlement_Kirana.Col1AmountDr, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPaymentReceiptSettlement_Kirana", Ncat.Payment, "Dgl1", FrmPaymentReceiptSettlement_Kirana.Col1AmountCr, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPaymentReceiptSettlement_Kirana", Ncat.Payment, "Dgl1", FrmPaymentReceiptSettlement_Kirana.Col1InterestPer, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPaymentReceiptSettlement_Kirana", Ncat.Payment, "Dgl1", FrmPaymentReceiptSettlement_Kirana.Col1InterestAmount, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPaymentReceiptSettlement_Kirana", Ncat.Payment, "Dgl1", FrmPaymentReceiptSettlement_Kirana.Col1DiscountPer, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPaymentReceiptSettlement_Kirana", Ncat.Payment, "Dgl1", FrmPaymentReceiptSettlement_Kirana.Col1DiscountAmount, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPaymentReceiptSettlement_Kirana", Ncat.Payment, "Dgl1", FrmPaymentReceiptSettlement_Kirana.Col1SubTotal, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPaymentReceiptSettlement_Kirana", Ncat.Payment, "Dgl1", FrmPaymentReceiptSettlement_Kirana.Col1BrokeragePer, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPaymentReceiptSettlement_Kirana", Ncat.Payment, "Dgl1", FrmPaymentReceiptSettlement_Kirana.Col1BrokerageAmount, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPaymentReceiptSettlement_Kirana", Ncat.Payment, "Dgl1", FrmPaymentReceiptSettlement_Kirana.Col1Remark, False)

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.Payment, SettingFields.FilterInclude_SubgroupType, "+" + SubgroupType.Supplier, AgDataType.Text, "255", mSubGroupTypeFieldQry, AgHelpQueryType.SqlQuery, AgHelpSelectionType.MultiSelect,,,,, ,, "+SUPPORT")
    End Sub
    Private Sub FConfigure_Receipt(ClsObj As ClsMain)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPaymentReceiptSettlement_Kirana", Ncat.Receipt, "DglMain", AgTemplate.TempTransaction1.hcSite_Code, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPaymentReceiptSettlement_Kirana", Ncat.Receipt, "DglMain", AgTemplate.TempTransaction1.hcV_Type, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPaymentReceiptSettlement_Kirana", Ncat.Receipt, "DglMain", AgTemplate.TempTransaction1.hcV_Date, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPaymentReceiptSettlement_Kirana", Ncat.Receipt, "DglMain", AgTemplate.TempTransaction1.hcV_No, 0, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPaymentReceiptSettlement_Kirana", Ncat.Receipt, "DglMain", AgTemplate.TempTransaction1.hcReferenceNo, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPaymentReceiptSettlement_Kirana", Ncat.Receipt, "DglMain", AgTemplate.TempTransaction1.hcSettingGroup, 0, 0, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPaymentReceiptSettlement_Kirana", Ncat.Receipt, "DglMain", FrmPaymentReceiptSettlement_Kirana.hcSubCode, 1,,, "Broker")
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPaymentReceiptSettlement_Kirana", Ncat.Receipt, "DglMain", FrmPaymentReceiptSettlement_Kirana.hcLinkedSubCode, 1,,, "Sub Party")


        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPaymentReceiptSettlement_Kirana", Ncat.Receipt, "Dgl2", FrmPaymentReceiptSettlement_Kirana.hcLineSubCode, 1, 1, 1, "Bank/Cash A/c")
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPaymentReceiptSettlement_Kirana", Ncat.Receipt, "Dgl2", FrmPaymentReceiptSettlement_Kirana.hcIsFinalPayment, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPaymentReceiptSettlement_Kirana", Ncat.Receipt, "Dgl2", FrmPaymentReceiptSettlement_Kirana.hcAmount, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPaymentReceiptSettlement_Kirana", Ncat.Receipt, "Dgl2", FrmPaymentReceiptSettlement_Kirana.hcRemarks, 1)


        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPaymentReceiptSettlement_Kirana", Ncat.Receipt, "Dgl1", FrmPaymentReceiptSettlement_Kirana.ColSNo, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPaymentReceiptSettlement_Kirana", Ncat.Receipt, "Dgl1", FrmPaymentReceiptSettlement_Kirana.Col1Select, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPaymentReceiptSettlement_Kirana", Ncat.Receipt, "Dgl1", FrmPaymentReceiptSettlement_Kirana.Col1TransactionDocID, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPaymentReceiptSettlement_Kirana", Ncat.Receipt, "Dgl1", FrmPaymentReceiptSettlement_Kirana.Col1LinkedSubCode, True,, "Party")
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPaymentReceiptSettlement_Kirana", Ncat.Receipt, "Dgl1", FrmPaymentReceiptSettlement_Kirana.Col1AmountDr, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPaymentReceiptSettlement_Kirana", Ncat.Receipt, "Dgl1", FrmPaymentReceiptSettlement_Kirana.Col1AmountCr, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPaymentReceiptSettlement_Kirana", Ncat.Receipt, "Dgl1", FrmPaymentReceiptSettlement_Kirana.Col1InterestPer, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPaymentReceiptSettlement_Kirana", Ncat.Receipt, "Dgl1", FrmPaymentReceiptSettlement_Kirana.Col1InterestAmount, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPaymentReceiptSettlement_Kirana", Ncat.Receipt, "Dgl1", FrmPaymentReceiptSettlement_Kirana.Col1DiscountPer, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPaymentReceiptSettlement_Kirana", Ncat.Receipt, "Dgl1", FrmPaymentReceiptSettlement_Kirana.Col1DiscountAmount, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPaymentReceiptSettlement_Kirana", Ncat.Receipt, "Dgl1", FrmPaymentReceiptSettlement_Kirana.Col1SubTotal, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPaymentReceiptSettlement_Kirana", Ncat.Receipt, "Dgl1", FrmPaymentReceiptSettlement_Kirana.Col1BrokeragePer, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPaymentReceiptSettlement_Kirana", Ncat.Receipt, "Dgl1", FrmPaymentReceiptSettlement_Kirana.Col1BrokerageAmount, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPaymentReceiptSettlement_Kirana", Ncat.Receipt, "Dgl1", FrmPaymentReceiptSettlement_Kirana.Col1Remark, False)

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.Receipt, SettingFields.FilterInclude_SubgroupType, "+" + SubgroupType.Customer, AgDataType.Text, "255", mSubGroupTypeFieldQry, AgHelpQueryType.SqlQuery, AgHelpSelectionType.MultiSelect,,,,, ,, "+SUPPORT")
    End Sub
    Private Sub FConfigure_SaleOrderSettlement(ClsObj As ClsMain)
        Dim MdiObj As New MdiKirana
        ClsObj.FSeedSingleIfNotExists_Voucher_Type("SOS", "Sale Order Settlement", Ncat.SaleOrderCancel, VoucherCategory.Sales, "", "Customised", MdiObj.MnuSalesOrderSettlement.Name, MdiObj.MnuSalesOrderSettlement.Text)

        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmOrderSettlement_Kirana", Ncat.SaleOrderCancel, "DglMain", AgTemplate.TempTransaction1.hcSite_Code, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmOrderSettlement_Kirana", Ncat.SaleOrderCancel, "DglMain", AgTemplate.TempTransaction1.hcV_Type, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmOrderSettlement_Kirana", Ncat.SaleOrderCancel, "DglMain", AgTemplate.TempTransaction1.hcV_Date, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmOrderSettlement_Kirana", Ncat.SaleOrderCancel, "DglMain", AgTemplate.TempTransaction1.hcV_No, 0, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmOrderSettlement_Kirana", Ncat.SaleOrderCancel, "DglMain", AgTemplate.TempTransaction1.hcReferenceNo, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmOrderSettlement_Kirana", Ncat.SaleOrderCancel, "DglMain", AgTemplate.TempTransaction1.hcSettingGroup, 0, 0, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmOrderSettlement_Kirana", Ncat.SaleOrderCancel, "DglMain", FrmOrderSettlement_Kirana.hcSettlementType, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmOrderSettlement_Kirana", Ncat.SaleOrderCancel, "DglMain", FrmOrderSettlement_Kirana.hcSubCode, 1,,, "Broker")

        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmOrderSettlement_Kirana", Ncat.SaleOrderCancel, "Dgl2", FrmOrderSettlement_Kirana.hcProduct, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmOrderSettlement_Kirana", Ncat.SaleOrderCancel, "Dgl2", FrmOrderSettlement_Kirana.hcOrderNo, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmOrderSettlement_Kirana", Ncat.SaleOrderCancel, "Dgl2", FrmOrderSettlement_Kirana.hcOrderBalance, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmOrderSettlement_Kirana", Ncat.SaleOrderCancel, "Dgl2", FrmOrderSettlement_Kirana.hcOrderRate, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmOrderSettlement_Kirana", Ncat.SaleOrderCancel, "Dgl2", FrmOrderSettlement_Kirana.hcSettlementQty, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmOrderSettlement_Kirana", Ncat.SaleOrderCancel, "Dgl2", FrmOrderSettlement_Kirana.hcSettlementRate, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmOrderSettlement_Kirana", Ncat.SaleOrderCancel, "Dgl2", FrmOrderSettlement_Kirana.hcDifferenceRate, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmOrderSettlement_Kirana", Ncat.SaleOrderCancel, "Dgl2", FrmOrderSettlement_Kirana.hcDifferenceAmount, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmOrderSettlement_Kirana", Ncat.SaleOrderCancel, "Dgl2", FrmOrderSettlement_Kirana.hcLessDiscountPer, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmOrderSettlement_Kirana", Ncat.SaleOrderCancel, "Dgl2", FrmOrderSettlement_Kirana.hcLessDiscountAmount, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmOrderSettlement_Kirana", Ncat.SaleOrderCancel, "Dgl2", FrmOrderSettlement_Kirana.hcLessBrokeragePer, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmOrderSettlement_Kirana", Ncat.SaleOrderCancel, "Dgl2", FrmOrderSettlement_Kirana.hcLessBrokerageAmount, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmOrderSettlement_Kirana", Ncat.SaleOrderCancel, "Dgl2", FrmOrderSettlement_Kirana.hcNetDifferenceAmount, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmOrderSettlement_Kirana", Ncat.SaleOrderCancel, "Dgl2", FrmOrderSettlement_Kirana.hcRemarks, 1)
    End Sub
    Private Sub FAlterTable_SaleInvoice()
        Try
            AgL.AddFieldSqlite(AgL.GcnMain, "SaleInvoice", "SettlementType", "nvarchar(10)", "", True)
        Catch ex As Exception
            MsgBox(ex.Message & "  [FAlterTable_SaleInvoice]")
        End Try
    End Sub
    Private Sub FAlterTable_SaleInvoiceDetail()
        Try
            AgL.AddFieldSqlite(AgL.GcnMain, "SaleInvoiceDetail", "OrderBalance", "Float", "0", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "SaleInvoiceDetail", "OrderRate", "Float", "0", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "SaleInvoiceDetail", "DifferenceRate", "Float", "0", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "SaleInvoiceDetail", "DifferenceAmount", "Float", "0", True)
        Catch ex As Exception
            MsgBox(ex.Message & "  [FCreateTable_SaleInvoiceDetail]")
        End Try
    End Sub
    Private Sub FAlterTable_PurchInvoice()
        Try
            AgL.AddFieldSqlite(AgL.GcnMain, "PurchInvoice", "SettlementType", "nvarchar(10)", "", True)
        Catch ex As Exception
            MsgBox(ex.Message & "  [FAlterTable_PurchInvoice]")
        End Try
    End Sub
    Private Sub FAlterTable_PurchInvoiceDetail()
        Try
            AgL.AddFieldSqlite(AgL.GcnMain, "PurchInvoiceDetail", "OrderBalance", "Float", "0", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "PurchInvoiceDetail", "OrderRate", "Float", "0", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "PurchInvoiceDetail", "DifferenceRate", "Float", "0", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "PurchInvoiceDetail", "DifferenceAmount", "Float", "0", True)
        Catch ex As Exception
            MsgBox(ex.Message & "  [FCreateTable_PurchInvoiceDetail]")
        End Try
    End Sub
    Private Sub FConfigure_PurchaseOrderSettlement(ClsObj As ClsMain)
        Dim MdiObj As New MdiKirana
        ClsObj.FSeedSingleIfNotExists_Voucher_Type("POS", "Purchase Order Settlement", Ncat.PurchaseOrderCancel, VoucherCategory.Purchase, "", "Customised", MdiObj.MnuPurchaseOrderSettlement.Name, MdiObj.MnuPurchaseOrderSettlement.Text)

        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmOrderSettlement_Kirana", Ncat.PurchaseOrderCancel, "DglMain", AgTemplate.TempTransaction1.hcSite_Code, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmOrderSettlement_Kirana", Ncat.PurchaseOrderCancel, "DglMain", AgTemplate.TempTransaction1.hcV_Type, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmOrderSettlement_Kirana", Ncat.PurchaseOrderCancel, "DglMain", AgTemplate.TempTransaction1.hcV_Date, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmOrderSettlement_Kirana", Ncat.PurchaseOrderCancel, "DglMain", AgTemplate.TempTransaction1.hcV_No, 0, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmOrderSettlement_Kirana", Ncat.PurchaseOrderCancel, "DglMain", AgTemplate.TempTransaction1.hcReferenceNo, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmOrderSettlement_Kirana", Ncat.PurchaseOrderCancel, "DglMain", AgTemplate.TempTransaction1.hcSettingGroup, 0, 0, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmOrderSettlement_Kirana", Ncat.PurchaseOrderCancel, "DglMain", FrmOrderSettlement_Kirana.hcSettlementType, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmOrderSettlement_Kirana", Ncat.PurchaseOrderCancel, "DglMain", FrmOrderSettlement_Kirana.hcSubCode, 1,,, "Supplier")

        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmOrderSettlement_Kirana", Ncat.PurchaseOrderCancel, "Dgl2", FrmOrderSettlement_Kirana.hcProduct, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmOrderSettlement_Kirana", Ncat.PurchaseOrderCancel, "Dgl2", FrmOrderSettlement_Kirana.hcOrderNo, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmOrderSettlement_Kirana", Ncat.PurchaseOrderCancel, "Dgl2", FrmOrderSettlement_Kirana.hcOrderBalance, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmOrderSettlement_Kirana", Ncat.PurchaseOrderCancel, "Dgl2", FrmOrderSettlement_Kirana.hcOrderRate, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmOrderSettlement_Kirana", Ncat.PurchaseOrderCancel, "Dgl2", FrmOrderSettlement_Kirana.hcSettlementQty, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmOrderSettlement_Kirana", Ncat.PurchaseOrderCancel, "Dgl2", FrmOrderSettlement_Kirana.hcSettlementRate, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmOrderSettlement_Kirana", Ncat.PurchaseOrderCancel, "Dgl2", FrmOrderSettlement_Kirana.hcDifferenceRate, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmOrderSettlement_Kirana", Ncat.PurchaseOrderCancel, "Dgl2", FrmOrderSettlement_Kirana.hcDifferenceAmount, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmOrderSettlement_Kirana", Ncat.PurchaseOrderCancel, "Dgl2", FrmOrderSettlement_Kirana.hcLessDiscountPer, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmOrderSettlement_Kirana", Ncat.PurchaseOrderCancel, "Dgl2", FrmOrderSettlement_Kirana.hcLessDiscountAmount, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmOrderSettlement_Kirana", Ncat.PurchaseOrderCancel, "Dgl2", FrmOrderSettlement_Kirana.hcLessBrokeragePer, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmOrderSettlement_Kirana", Ncat.PurchaseOrderCancel, "Dgl2", FrmOrderSettlement_Kirana.hcLessBrokerageAmount, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmOrderSettlement_Kirana", Ncat.PurchaseOrderCancel, "Dgl2", FrmOrderSettlement_Kirana.hcNetDifferenceAmount, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmOrderSettlement_Kirana", Ncat.PurchaseOrderCancel, "Dgl2", FrmOrderSettlement_Kirana.hcRemarks, 1)
    End Sub
End Class
