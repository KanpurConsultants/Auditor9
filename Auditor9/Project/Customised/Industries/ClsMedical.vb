Imports AgLibrary.ClsMain.agConstants
Imports Customised.ClsMain

Public Class ClsMedical
    Private mQry As String = ""
    Private mItemTypeFieldQry As String = ""
    Public Sub FSeedData_Medical()
        Dim ClsObj As New Customised.ClsMain(AgL)
        Try
            If ClsMain.IsScopeOfWorkContains(IndustryType.MedicalIndustry) Then
                FInitVariables()
                FConfigure_PurchaseInvoice(ClsObj)
                FConfigure_SaleInvoice(ClsObj)
                FConfigure_Item(ClsObj)
                FConfigure_SaleInvoiceOverlay(ClsObj)
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " In FSeedData_MedicalIndustry")
        End Try
    End Sub

    Private Sub FInitVariables()
        mItemTypeFieldQry = "SELECT Code, Name FROM ItemType Order By Name"
    End Sub


    Private Sub FConfigure_PurchaseInvoice(ClsObj As ClsMain)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Deal, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ExpiryDate, True)

        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchInvoiceDirect", "", Ncat.PurchaseInvoice, "", "", "", "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCategory, 0, 0, 0, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchInvoiceDirect", "", Ncat.PurchaseInvoice, "", "", "", "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemGroup, 0, 0, 0, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchInvoiceDirect", "", Ncat.PurchaseInvoice, "", "", "", "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1MRP, 1, 0, 0, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchInvoiceDirect", "", Ncat.PurchaseInvoice, "", "", "", "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1SaleRate, 1, 0, 0, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchInvoiceDirect", "", Ncat.PurchaseInvoice, "", "", "", "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1LotNo, 1, 0, 0, "Batch No")

        mQry = "UPDATE StructureDetail SET VisibleInTransactionLine = 0 WHERE Code = 'GstSale'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
    End Sub
    Private Sub FConfigure_SaleInvoice(ClsObj As ClsMain)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Deal, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1ExpiryDate, True, 0,, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1MRP, True, 0,, False)

        ClsObj.FUpdateSeed_EntryHeaderUISetting("FrmSaleInvoiceDirect", "", Ncat.SaleInvoice, "", "", "", "Dgl2", FrmSaleInvoiceDirect_WithDimension.hcRateType, 0, 0, 1, 0, "")

        ClsObj.FUpdateSeed_EntryLineUISetting("FrmSaleInvoiceDirect", "", Ncat.SaleInvoice, "", "", "", "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1ItemCategory, 0, 0, 0, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmSaleInvoiceDirect", "", Ncat.SaleInvoice, "", "", "", "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1ItemGroup, 0, 0, 0, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmSaleInvoiceDirect", "", Ncat.SaleInvoice, "", "", "", "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1LotNo, 1, 0, 0, "Batch No")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmSaleInvoiceDirect", "", Ncat.SaleInvoice, "", "", "", "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1StockInDiv_Code, 1, 0, 1, "")


        ClsObj.FUpdateSeed_Setting(SettingType.General, VoucherCategory.Sales, "", SettingFields.DocumentPrintShowPartyBalance, DocumentPrintFieldsVisibilityOptions.Show, AgDataType.Text, "50", "DocumentPrintFieldsVisibilityOptions", AgHelpQueryType.ClassName, AgHelpSelectionType.SingleSelect,,,,, , "+SUPPORT")


        mQry = "UPDATE StructureDetail SET VisibleInTransactionLine = 0 WHERE Code = 'GstPur'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
    End Sub
    Private Sub FConfigure_Item(ClsObj As ClsMain)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmItemMaster", ItemTypeCode.TradingProduct, "Dgl1", FrmItemMaster.hcRemark1, 1,,, "Pack Size")

        mQry = "UPDATE ItemTypeSetting SET IsItemGroupLinkedWithItemCategory = 0 
                WHERE Code = '" & ItemTypeCode.TradingProduct & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)

        mQry = "UPDATE Setting SET Value = '<SPECIFICATION>          [<ItemCategory>]' 
                WHERE SettingType = 'General' AND VoucherType IS NULL
                And FieldName = 'Item Name Pattern'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)

        ClsObj.FUpdateSeed_EntryHeaderUISetting("FrmItemMaster", "", ItemTypeCode.TradingProduct, "", "", "", "Dgl1", FrmItemMaster.hcItemGroup, 0, 0, 0, 0, "")


        ClsObj.FUpdateSeed_EntryLineUISetting("FrmItemMaster", "", ItemTypeCode.TradingProduct, "", "", "", "Dgl1", ConfigurableFields.FrmItemGroupLineRateType.RateType, 0, 0, 0, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmItemMaster", "", ItemTypeCode.TradingProduct, "", "", "", "Dgl1", ConfigurableFields.FrmItemGroupLineRateType.MarginPer, 0, 0, 0, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmItemMaster", "", ItemTypeCode.TradingProduct, "", "", "", "Dgl1", ConfigurableFields.FrmItemGroupLineRateType.DiscountPer, 0, 0, 0, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmItemMaster", "", ItemTypeCode.TradingProduct, "", "", "", "Dgl1", ConfigurableFields.FrmItemGroupLineRateType.AdditionalDiscountPer, 0, 0, 0, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmItemMaster", "", ItemTypeCode.TradingProduct, "", "", "", "Dgl1", ConfigurableFields.FrmItemGroupLineRateType.AdditionPer, 0, 0, 0, "")
    End Sub


    Private Sub FConfigure_SaleInvoiceOverlay(ClsObj As ClsMain)
        Dim MdiObj As New MDIMain

        ClsObj.FSeedSingleIfNotExists_Voucher_Type(Ncat.SaleInvoiceOverlay, "Sale Invoice Overlay", Ncat.SaleInvoiceOverlay, VoucherCategory.Sales, "", "Customised", MdiObj.MnuSaleEntryOverlay.Name, MdiObj.MnuSaleEntryOverlay.Text)




        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoiceOverlay, "DglMain", AgTemplate.TempTransaction1.hcSite_Code, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoiceOverlay, "DglMain", AgTemplate.TempTransaction1.hcV_Type, 1, 1, 1, "Invoice Type")
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoiceOverlay, "DglMain", AgTemplate.TempTransaction1.hcV_Date, 1, 1, 1, "Invoice Date")
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoiceOverlay, "DglMain", AgTemplate.TempTransaction1.hcV_No, 0, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoiceOverlay, "DglMain", AgTemplate.TempTransaction1.hcReferenceNo, 1, 1, 1, "Invoice No")
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoiceOverlay, "DglMain", AgTemplate.TempTransaction1.hcSettingGroup, 0, 0, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoiceOverlay, "DglMain", FrmSaleInvoiceDirect_WithDimension.hcSaleToParty, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoiceOverlay, "DglMain", FrmSaleInvoiceDirect_WithDimension.hcBillToParty, 1, 1)


        If ClsMain.IsScopeOfWorkContains(IndustryType.CommonModules.RateTypeModule) Then
            ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoiceOverlay, "Dgl2", FrmSaleInvoiceDirect_WithDimension.hcRateType, 1)
        End If
        If ClsMain.IsScopeOfWorkContains(IndustryType.CommonModules.GodownModule) Then
            ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoiceOverlay, "Dgl2", FrmSaleInvoiceDirect_WithDimension.hcGodown, 1)
        End If
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoiceOverlay, "Dgl2", FrmSaleInvoiceDirect_WithDimension.hcShipToParty)

        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoiceOverlay, "Dgl2", FrmSaleInvoiceDirect_WithDimension.HcSalesTaxNo)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoiceOverlay, "Dgl2", FrmSaleInvoiceDirect_WithDimension.HcAadharNo)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoiceOverlay, "Dgl2", FrmSaleInvoiceDirect_WithDimension.HcBtnAttachments)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoiceOverlay, "Dgl2", FrmSaleInvoiceDirect_WithDimension.HcBtnMoneyReceived)

        If ClsMain.IsScopeOfWorkContains(IndustryType.CommonModules.SalesExecutiveModule) Then
            ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoiceOverlay, "Dgl2", FrmSaleInvoiceDirect_WithDimension.HcSalesRepresentative, 1)
        End If

        If ClsMain.IsScopeOfWorkContains(IndustryType.CommonModules.SalesInterestModule) Then
            ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoiceOverlay, "Dgl3", FrmSaleInvoiceDirect_WithDimension.hcCreditDays, 1)
        End If
        If ClsMain.IsScopeOfWorkContains(IndustryType.CommonModules.SalesAgentModule) Then
            ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoiceOverlay, "Dgl3", FrmSaleInvoiceDirect_WithDimension.hcAgent, 1)
        End If
        If ClsMain.IsScopeOfWorkContains(IndustryType.CommonModules.SalesTransportModule) Then
            ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoiceOverlay, "Dgl3", FrmSaleInvoiceDirect_WithDimension.hcTransporter, 1)
        End If
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoiceOverlay, "Dgl3", FrmSaleInvoiceDirect_WithDimension.hcResponsiblePerson, 0)
        If ClsMain.IsScopeOfWorkContains(IndustryType.CommonModules.SalesExecutiveModule) Then
            ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoiceOverlay, "Dgl3", FrmSaleInvoiceDirect_WithDimension.HcSalesRepresentative, 1)
        End If
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoiceOverlay, "Dgl3", FrmSaleInvoiceDirect_WithDimension.hcRemarks1, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoiceOverlay, "Dgl3", FrmSaleInvoiceDirect_WithDimension.hcRemarks2, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoiceOverlay, "Dgl3", FrmSaleInvoiceDirect_WithDimension.hcRemarks, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoiceOverlay, "Dgl3", FrmSaleInvoiceDirect_WithDimension.hcTermsAndConditions, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoiceOverlay, "Dgl3", FrmSaleInvoiceDirect_WithDimension.hcTags)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoiceOverlay, "Dgl3", FrmSaleInvoiceDirect_WithDimension.hcBtnTransportDetail, 1)











        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoiceOverlay, "Dgl1", FrmSaleInvoiceDirect_WithDimension.ColSNo, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoiceOverlay, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Barcode, False)
        If ClsMain.IsScopeOfWorkContains("+CLOTH TRADING WHOLESALE") Then
            ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoiceOverlay, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1ItemCategory, True)
        Else
            ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoiceOverlay, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1ItemCategory, False)
        End If
        If ClsMain.IsScopeOfWorkContains("+CLOTH TRADING WHOLESALE") Then
            ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoiceOverlay, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1ItemGroup, True)
        Else
            ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoiceOverlay, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1ItemGroup, False)
        End If
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoiceOverlay, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1ItemCode, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoiceOverlay, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Item, True)
        If ClsMain.IsScopeOfWorkContains(IndustryType.CommonModules.Dimension1) Then
            ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoiceOverlay, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Dimension1, False)
        End If
        If ClsMain.IsScopeOfWorkContains(IndustryType.CommonModules.Dimension2) Then
            ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoiceOverlay, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Dimension2, False)
        End If
        If ClsMain.IsScopeOfWorkContains(IndustryType.CommonModules.Dimension3) Then
            ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoiceOverlay, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Dimension3, False)
        End If
        If ClsMain.IsScopeOfWorkContains(IndustryType.CommonModules.Dimension4) Then
            ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoiceOverlay, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Dimension4, False)
        End If
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoiceOverlay, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Size, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoiceOverlay, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Specification, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoiceOverlay, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1ItemState, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoiceOverlay, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1SalesTaxGroup, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoiceOverlay, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1BaleNo, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoiceOverlay, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1LotNo, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoiceOverlay, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1DocQty, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoiceOverlay, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Unit, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoiceOverlay, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Pcs, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoiceOverlay, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1MasterSaleRate, False,,, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoiceOverlay, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1RateDiscountPer, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoiceOverlay, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Rate, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoiceOverlay, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1DiscountPer, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoiceOverlay, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1DiscountAmount, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoiceOverlay, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1AdditionalDiscountPer, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoiceOverlay, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1AdditionalDiscountAmount, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoiceOverlay, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1AdditionPer, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoiceOverlay, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1AdditionAmount, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoiceOverlay, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Amount, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoiceOverlay, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Remark, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoiceOverlay, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1StockInDiv_Code, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoiceOverlay, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Godown, False)
        If ClsMain.IsScopeOfWorkContains(IndustryType.CommonModules.SalesExecutiveModule) Then
            ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoiceOverlay, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1SalesRepresentative, False)
        End If
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoiceOverlay, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1PurchaseRate, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoiceOverlay, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1SaleInvoice, False, False, "Sale Order")



        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceParty", Ncat.SaleInvoiceOverlay, "DGL1CASH", FrmSaleInvoiceParty_WithDimension.HcPartyName, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceParty", Ncat.SaleInvoiceOverlay, "DGL1CASH", FrmSaleInvoiceParty_WithDimension.HcAddress, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceParty", Ncat.SaleInvoiceOverlay, "DGL1CASH", FrmSaleInvoiceParty_WithDimension.HcCity, 1, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceParty", Ncat.SaleInvoiceOverlay, "DGL1CASH", FrmSaleInvoiceParty_WithDimension.HcPincode, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceParty", Ncat.SaleInvoiceOverlay, "DGL1CASH", FrmSaleInvoiceParty_WithDimension.HcMobile, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceParty", Ncat.SaleInvoiceOverlay, "DGL1CASH", FrmSaleInvoiceParty_WithDimension.HcStateCode, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceParty", Ncat.SaleInvoiceOverlay, "DGL1CASH", FrmSaleInvoiceParty_WithDimension.HcPlaceOfSupply, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceParty", Ncat.SaleInvoiceOverlay, "DGL1CASH", FrmSaleInvoiceParty_WithDimension.HcSalesTaxGroup, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceParty", Ncat.SaleInvoiceOverlay, "DGL1CASH", FrmSaleInvoiceParty_WithDimension.HcSalesTaxGroupRegType, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceParty", Ncat.SaleInvoiceOverlay, "DGL1CASH", FrmSaleInvoiceParty_WithDimension.HcSalesTaxNo, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceParty", Ncat.SaleInvoiceOverlay, "DGL1CASH", FrmSaleInvoiceParty_WithDimension.HcAadharNo, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceParty", Ncat.SaleInvoiceOverlay, "DGL1CASH", FrmSaleInvoiceParty_WithDimension.HcPanNo, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceParty", Ncat.SaleInvoiceOverlay, "DGL1CASH", FrmSaleInvoiceParty_WithDimension.HcShipToAddress, 0)


        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceParty", Ncat.SaleInvoiceOverlay, "DGL1", FrmSaleInvoiceParty_WithDimension.HcPartyName, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceParty", Ncat.SaleInvoiceOverlay, "DGL1", FrmSaleInvoiceParty_WithDimension.HcAddress, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceParty", Ncat.SaleInvoiceOverlay, "DGL1", FrmSaleInvoiceParty_WithDimension.HcCity, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceParty", Ncat.SaleInvoiceOverlay, "DGL1", FrmSaleInvoiceParty_WithDimension.HcPincode, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceParty", Ncat.SaleInvoiceOverlay, "DGL1", FrmSaleInvoiceParty_WithDimension.HcMobile, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceParty", Ncat.SaleInvoiceOverlay, "DGL1", FrmSaleInvoiceParty_WithDimension.HcStateCode, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceParty", Ncat.SaleInvoiceOverlay, "DGL1", FrmSaleInvoiceParty_WithDimension.HcPlaceOfSupply, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceParty", Ncat.SaleInvoiceOverlay, "DGL1", FrmSaleInvoiceParty_WithDimension.HcSalesTaxGroup, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceParty", Ncat.SaleInvoiceOverlay, "DGL1", FrmSaleInvoiceParty_WithDimension.HcSalesTaxNo, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceParty", Ncat.SaleInvoiceOverlay, "DGL1", FrmSaleInvoiceParty_WithDimension.HcAadharNo, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceParty", Ncat.SaleInvoiceOverlay, "DGL1", FrmSaleInvoiceParty_WithDimension.HcPanNo, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceParty", Ncat.SaleInvoiceOverlay, "DGL1", FrmSaleInvoiceParty_WithDimension.HcShipToAddress, 1)



        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.SaleInvoiceOverlay, SettingFields.TermsAndConditions, "(1) Payment within <CreditDays> days from date of Invoice. <BR>(2) Overdue Interest @ <Default_DebtorsInterestRate> P.A. after due date. <BR>(3) Goods dispatched by transport at the owner risk. <BR>(4) All disputes are subject to <CompanyCity> jurisdiction only.", AgDataType.Text, "500",,,,,,,,, "+SUPPORT")
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.SaleInvoiceOverlay, SettingFields.DocumentPrintReportFileName, "", AgDataType.Text, "50",,,,,,,,, "+SUPPORT")
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.SaleInvoiceOverlay, SettingFields.DocumentPrintReportFileNameUnregisteredParty, "", AgDataType.Text, "50",,,,,,,,, "+SUPPORT")
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.SaleInvoiceOverlay, SettingFields.DocumentPrintEntryNoPrefix, "", AgDataType.Text, "8",,,,,,,,, "+SUPPORT")
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.SaleInvoiceOverlay, SettingFields.DocumentPrintEntryNoPrefix, "", AgDataType.Text, "8",,,,,,,,, "+SUPPORT")
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.SaleInvoiceOverlay, SettingFields.DocumentPrintReportFileName, "", AgDataType.Text, "50",,,,,,,,,, "+SUPPORT")
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.SaleInvoiceOverlay, SettingFields.DocumentPrintEntryNoPrefix, "", AgDataType.Text, "8",,,,,,,,,, "+SUPPORT")
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.SaleInvoiceOverlay, SettingFields.DocumentPrintEntryNoPrefix, "", AgDataType.Text, "8",,,,,,,,, , "+SUPPORT")
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.SaleInvoiceOverlay, SettingFields.MaximumItemLimit, "", AgDataType.Number, "50",,,,,,,,,, "+SUPPORT")
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.SaleInvoiceOverlay, SettingFields.ActionToPrintOnAdd, ActionToPrint.AskAndPrintOnScreen, AgDataType.Text, "20", "ActionToPrint", AgHelpQueryType.ClassName, AgHelpSelectionType.SingleSelect,,,,, ,, "+SUPPORT")
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.SaleInvoiceOverlay, SettingFields.PrintingCopyCaptions, "", AgDataType.Text, "250",,,,,,,, ,, "+SUPPORT")
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.SaleInvoiceOverlay, SettingFields.PrintingBulkCopyCaptions, "", AgDataType.Text, "250",,,,,,,, ,, "+SUPPORT")
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.SaleInvoiceOverlay, SettingFields.PickRateFrom, PickRateFrom.Master, AgDataType.Text, "50", "PickRateFrom", AgHelpQueryType.ClassName, AgHelpSelectionType.SingleSelect,,,,, , "+SUPPORT")
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.SaleInvoiceOverlay, SettingFields.FilterInclude_ItemType, "", AgDataType.Text, "255", mItemTypeFieldQry, AgHelpQueryType.SqlQuery, AgHelpSelectionType.MultiSelect,,,,, ,, "+SUPPORT")
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.SaleInvoiceOverlay, SettingFields.MailTo, "", AgDataType.Text, "50",,,,,,,,, IndustryType.CommonModules.EmailModule)
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.SaleInvoiceOverlay, SettingFields.MailCc, "", AgDataType.Text, "50",,,,,,,,, IndustryType.CommonModules.EmailModule)
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.SaleInvoiceOverlay, SettingFields.MailSubject, "Invoice No. <EntryNo> from <DivisionName>", AgDataType.Text, "100",,,,,,,, , IndustryType.CommonModules.EmailModule)
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.SaleInvoiceOverlay, SettingFields.MailMessage, "Dear <PartyName>

Please find your attached invoice.We appreciate your prompt payment.

Sincerely
<DivisionName>", AgDataType.Text, "1000",,,,,,,,, IndustryType.CommonModules.EmailModule)
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.SaleInvoiceOverlay, SettingFields.SmsMessage, "Dear <PartyName>,

Your Inv.No. <EntryNo> Dated <EntryDate> of Rs.<NetAmount> has been dispatched.

Sincerely
<DivisionName>", AgDataType.Text, "1000",,,,,,,, , IndustryType.CommonModules.SmsModule)
        mQry = "  Select '[Item Group PD]' as Code, '[Item Group PD]' as Name "
        mQry += " Union All "
        mQry += " Select '[Margin]' as Code, '[Margin]' as Name "
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.SaleInvoiceOverlay, SettingFields.ItemHelpAdditionalColumns, "[Item Group PD]", AgDataType.Text, "255", mQry, AgHelpQueryType.SqlQuery, AgHelpSelectionType.MultiSelect,,,,,, "+SUPPORT")
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.SaleInvoiceOverlay, SettingFields.ShowLastRatesYn, "1", AgDataType.YesNo, "50")



        ''''''''''''''''''''''''''''For Making it Medical Sale''''''''''''''''

        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoiceOverlay, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Deal, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoiceOverlay, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1ExpiryDate, True, 0,, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoiceOverlay, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1MRP, True, 0,, False)

        ClsObj.FUpdateSeed_EntryHeaderUISetting("FrmSaleInvoiceDirect", "", Ncat.SaleInvoiceOverlay, "", "", "", "Dgl2", FrmSaleInvoiceDirect_WithDimension.hcRateType, 0, 0, 1, 0, "")

        ClsObj.FUpdateSeed_EntryLineUISetting("FrmSaleInvoiceDirect", "", Ncat.SaleInvoiceOverlay, "", "", "", "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1ItemCategory, 0, 0, 0, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmSaleInvoiceDirect", "", Ncat.SaleInvoiceOverlay, "", "", "", "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1ItemGroup, 0, 0, 0, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmSaleInvoiceDirect", "", Ncat.SaleInvoiceOverlay, "", "", "", "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1LotNo, 1, 0, 0, "Batch No")

        'mQry = "UPDATE User_Permission SET Permission = '*EDP' WHERE MnuName  ='MnuSalesEntry' "
        'AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
    End Sub
End Class
