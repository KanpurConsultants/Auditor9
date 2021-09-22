Imports AgLibrary.ClsMain.agConstants
Imports Customised.ClsMain

Public Class ClsClothRetail
    Private mQry As String = ""

    Private SubCode_Customer As String = "Customer"

    Private mItemV_TypeFieldQry As String = ""
    Private mItemTypeFieldQry As String = ""
    Private mItemCategoryFieldQry As String = ""
    Private mItemGroupFieldQry As String = ""
    Private mItemFieldQry As String = ""
    Private mProcessFieldQry As String = ""
    Private mAcGroupFieldQry As String = ""
    Private mItemBaseFieldQry As String = ""
    Private mSubGroupTypeFieldQry As String = ""
    Private mSubGroupNatureFieldQry As String = ""
    Private mPostingGroupSalesTaxPartyFieldQry As String = ""
    Private mVoucher_TypeFieldQry As String = ""
    Private mSettingGroupFieldQry As String = ""
    Public Sub FSeedData_ClothRetail()
        Dim ClsObj As New Customised.ClsMain(AgL)
        Try
            If ClsMain.IsScopeOfWorkContains(IndustryType.TextileIndustry) Then
                If ClsMain.IsScopeOfWorkContains(IndustryType.SubIndustryType.RetailModule) Then
                    ClsObj.AppendScopeOfWork(IndustryType.CommonModules.PurchaseGoodsReceiptModule)
                    ClsObj.AppendScopeOfWork(IndustryType.CommonModules.BarcodeModule)
                    ClsObj.AppendScopeOfWork(IndustryType.CommonModules.Dimension1)
                    ClsObj.AppendScopeOfWork(IndustryType.CommonModules.Size)
                    ClsObj.AppendScopeOfWork(IndustryType.CommonModules.RevenuePointModule)
                    ClsObj.AppendScopeOfWork(IndustryType.CommonModules.SalesExecutiveModule)


                    'For Removing Any Scope Of Work
                    ClsObj.RemoveScopeOfWork(IndustryType.CommonModules.RateTypeModule)

                    FInitVariables()

                    FConfigure_SaleInvoiceSetting(ClsObj)

                    FConfigure_Setting(ClsObj)

                    FConfigure_PurchaseInvoice(ClsObj)
                    FConfigure_SaleInvoice(ClsObj)
                    FConfigure_Receipt(ClsObj)
                    FConfigure_StockOpening(ClsObj)
                    FConfigure_PurchaseGoodsReceipt(ClsObj)
                    FConfigure_Item(ClsObj)
                    FConfigure_ItemType(ClsObj)

                    FConfigure_ItemCategory(ClsObj)
                    FConfigure_ItemGroup(ClsObj)
                    FConfigure_Size(ClsObj)

                    FSeedTable_Subgroup(ClsObj)
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " In FSeedData_TextileIndustry")
        End Try
    End Sub
    Private Sub FInitVariables()
        mItemTypeFieldQry = "SELECT Code, Name FROM ItemType Order By Name"
        mItemGroupFieldQry = "SELECT Code, Description FROM ItemGroup Order By Description"
        mItemCategoryFieldQry = "SELECT Code, Description FROM ItemCategory Order By Description"
        mItemFieldQry = "SELECT Code, Description FROM Item Order By Description"
        mProcessFieldQry = "SELECT Code, Name From viewHelpSubgroup Sg  With (NoLock) Where SubgroupType ='" & SubgroupType.Process & "' Order By Name"
        mAcGroupFieldQry = "SELECT GroupCode AS Code, GroupName AS Name FROM AcGroup ORDER BY GroupName"
        mSubGroupTypeFieldQry = "SELECT SubgroupType AS Code, SubgroupType AS Name  FROM SubGroupType ORDER BY SubgroupType"
        mSubGroupNatureFieldQry = "SELECT DISTINCT Nature AS Code, Nature AS Name  FROM Subgroup WHERE Nature IS NOT NULL ORDER BY Nature"
        mPostingGroupSalesTaxPartyFieldQry = "SELECT Description As Code, Description FROM PostingGroupSalesTaxParty Order By Description"
        mVoucher_TypeFieldQry = "SELECT V_Type AS Code, Description AS Name FROM Voucher_Type WHERE NCat = '" & Ncat.SaleOrder & "' ORDER BY Description"
        mSettingGroupFieldQry = "Select Code, Name From SettingGroup Order By Name"

        mItemBaseFieldQry = "SELECT 'Item Category' As Code, 'Item Category' As Name 
                        UNION ALL 
                        SELECT 'Item Group' As Code, 'Item Group' As Name 
                        UNION ALL 
                        SELECT 'Item' As Code, 'Item' As Name "
        If ClsMain.IsScopeOfWorkContains(IndustryType.CommonModules.Dimension1) Then
            mItemBaseFieldQry += " UNION ALL SELECT 'Dimension1' As Code, '" & AgL.PubCaptionDimension1 & "' As Name "
        End If
        If ClsMain.IsScopeOfWorkContains(IndustryType.CommonModules.Dimension2) Then
            mItemBaseFieldQry += " UNION ALL SELECT 'Dimension2' As Code, '" & AgL.PubCaptionDimension2 & "' As Name "
        End If
        If ClsMain.IsScopeOfWorkContains(IndustryType.CommonModules.Dimension3) Then
            mItemBaseFieldQry += " UNION ALL SELECT 'Dimension3' As Code, '" & AgL.PubCaptionDimension3 & "' As Name "
        End If
        If ClsMain.IsScopeOfWorkContains(IndustryType.CommonModules.Dimension4) Then
            mItemBaseFieldQry += " UNION ALL SELECT 'Dimension4' As Code, '" & AgL.PubCaptionDimension4 & "' As Name "
        End If
        If ClsMain.IsScopeOfWorkContains(IndustryType.CommonModules.Size) Then
            mItemBaseFieldQry += " UNION ALL SELECT 'Size' As Code, 'Size' As Name "
        End If
        mItemBaseFieldQry += " UNION ALL SELECT 'None' As Code, 'None' As Name "
    End Sub
    Private Sub FConfigure_SaleInvoiceSetting(ClsObj As ClsMain)
        mQry = "Update SaleInvoiceSetting Set SaleInvoicePattern = '" & SaleInvoicePattern.PointOfSale & "' 
                Where Code='" & Ncat.SaleInvoice & "' 
                And V_Type is Null And Div_Code Is Null And Site_Code Is Null  
                And SaleInvoicePattern Is Null "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)

        mQry = "Update SaleInvoiceSetting Set IsVisible_BarcodeGunTextbox = '1' 
                Where Code='" & Ncat.SaleInvoice & "' 
                And V_Type is Null And Div_Code Is Null And Site_Code Is Null "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
    End Sub
    Private Sub FConfigure_PurchaseInvoice(ClsObj As ClsMain)
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.PurchaseInvoice, SettingFields.GenerateBarcodeYn, "1", AgDataType.YesNo, "50")

        ClsObj.FUpdateSeed_EntryHeaderUISetting("FrmPurchInvoiceDirect", "", Ncat.PurchaseInvoice, "", "", "", "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcStockInNo, 1, 0, 0, 0, "")

        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchInvoiceDirect", "", Ncat.PurchaseInvoice, "", "", "", "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension1, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchInvoiceDirect", "", Ncat.PurchaseInvoice, "", "", "", "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Size, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchInvoiceDirect", "", Ncat.PurchaseInvoice, "", "", "", "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1MRP, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchInvoiceDirect", "", Ncat.PurchaseInvoice, "", "", "", "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ProfitMarginPer, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchInvoiceDirect", "", Ncat.PurchaseInvoice, "", "", "", "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1SaleRate, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchInvoiceDirect", "", Ncat.PurchaseInvoice, "", "", "", "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1HSN, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchInvoiceDirect", "", Ncat.PurchaseInvoice, "", "", "", "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Barcode, 1, 0, 1, "")

        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPrintBarcode", "", "", "", "", "", "Dgl1", FrmPrintBarcode.Col1Dimension1, 1, 0, 1, "")
    End Sub
    Private Sub FConfigure_Setting(ClsObj As ClsMain)
        mQry = "Update Setting Set Value ='Design' Where FieldName = '" & ClsMain.SettingFields.Dimension1Caption & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        mQry = "Update Setting Set Value ='1' Where FieldName = '" & ClsMain.SettingFields.SkuManagementApplicableYN & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
    End Sub
    Private Sub FConfigure_SaleInvoice(ClsObj As ClsMain)
        mQry = "Update Setting Set Value = '0'
                Where SettingType = '" & SettingType.General & "'
                And NCat = '" & Ncat.SaleInvoice & "'
                And FieldName = '" & ClsMain.SettingFields.ShowLastRatesYn & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)

        mQry = " UPDATE Voucher_Type SET Structure = 'GstSaleMrp' WHERE V_Type = 'SI'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)

        ClsObj.FUpdateSeed_EntryHeaderUISetting("FrmSaleInvoiceDirect", "", Ncat.SaleInvoice, "", "", "", "Dgl2", FrmSaleInvoiceDirect_WithDimension.hcRateType, 0, 0, 1, 0, "")

        ClsObj.FUpdateSeed_EntryHeaderUISetting("FrmSaleInvoiceDirect", "", Ncat.SaleInvoice, "", "", "", "Dgl3", FrmSaleInvoiceDirect_WithDimension.hcCreditDays, 0, 0, 1, 0, "")
        ClsObj.FUpdateSeed_EntryHeaderUISetting("FrmSaleInvoiceDirect", "", Ncat.SaleInvoice, "", "", "", "Dgl3", FrmSaleInvoiceDirect_WithDimension.hcAgent, 0, 0, 1, 0, "")
        ClsObj.FUpdateSeed_EntryHeaderUISetting("FrmSaleInvoiceDirect", "", Ncat.SaleInvoice, "", "", "", "Dgl3", FrmSaleInvoiceDirect_WithDimension.hcTransporter, 0, 0, 1, 0, "")
        ClsObj.FUpdateSeed_EntryHeaderUISetting("FrmSaleInvoiceDirect", "", Ncat.SaleInvoice, "", "", "", "Dgl3", FrmSaleInvoiceDirect_WithDimension.hcBtnTransportDetail, 0, 0, 1, 0, "")

        ClsObj.FUpdateSeed_EntryLineUISetting("FrmSaleInvoiceDirect", "", Ncat.SaleInvoice, "", "", "", "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1ItemCategory, 0, 0, 0, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmSaleInvoiceDirect", "", Ncat.SaleInvoice, "", "", "", "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1ItemGroup, 0, 0, 0, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmSaleInvoiceDirect", "", Ncat.SaleInvoice, "", "", "", "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Dimension1, 0, 0, 0, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmSaleInvoiceDirect", "", Ncat.SaleInvoice, "", "", "", "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Size, 0, 0, 0, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmSaleInvoiceDirect", "", Ncat.SaleInvoice, "", "", "", "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Barcode, 1, 0, 1, "")
    End Sub
    Private Sub FConfigure_Item(ClsObj As ClsMain)
        ClsObj.FUpdateSeed_EntryHeaderUISetting("FrmItemMaster", "", ItemTypeCode.TradingProduct, "", "", "", "Dgl1", FrmItemMaster.hcBarcode, 1, 0, 1, 0, "")
    End Sub
    Private Sub FConfigure_ItemType(ClsObj As ClsMain)
        mQry = "UPDATE ItemTypeSetting SET IsApplicable_Barcode = 1 WHERE Code = '" & ItemTypeCode.TradingProduct & "' "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        mQry = "UPDATE ItemTypeSetting SET IsItemGroupLinkedWithItemCategory = 0 WHERE Code = '" & ItemTypeCode.TradingProduct & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
    End Sub
    Private Sub FConfigure_ItemCategory(ClsObj As ClsMain)
        ClsObj.FUpdateSeed_EntryHeaderUISetting("FrmItemCategory", "", ItemTypeCode.TradingProduct, "", "", "", "Dgl1", FrmItemCategory_Grid.hcIsNewItemAllowedPurch, 1, 0, 1, 0, "")
        ClsObj.FUpdateSeed_EntryHeaderUISetting("FrmItemCategory", "", ItemTypeCode.TradingProduct, "", "", "", "Dgl1", FrmItemCategory_Grid.hcIsNewDimension1AllowedPurch, 1, 0, 1, 0, "")

        ClsObj.FUpdateSeed_EntryHeaderUISetting("FrmItemCategory", "", ItemTypeCode.TradingProduct, "", "", "", "Dgl1", FrmItemCategory_Grid.hcSalesRepresentativeCommissionPer, 1, 0, 1, 0, "")
        ClsObj.FUpdateSeed_EntryHeaderUISetting("FrmItemCategory", "", ItemTypeCode.TradingProduct, "", "", "", "Dgl1", FrmItemCategory_Grid.hcBarcodeType, 1, 0, 1, 0, "")
        ClsObj.FUpdateSeed_EntryHeaderUISetting("FrmItemCategory", "", ItemTypeCode.TradingProduct, "", "", "", "Dgl1", FrmItemCategory_Grid.hcBarcodePattern, 1, 0, 1, 0, "")
    End Sub
    Private Sub FConfigure_ItemGroup(ClsObj As ClsMain)
        ClsObj.FUpdateSeed_EntryHeaderUISetting("FrmItemGroup", "", ItemTypeCode.TradingProduct, "", "", "", "Dgl1", ConfigurableFields.FrmItemGroupHeaderDgl1.SalesRepresentativeCommissionPer, 1, 0, 1, 0, "")
    End Sub
    Private Sub FConfigure_Size(ClsObj As ClsMain)
        ClsObj.FUpdateSeed_EntryHeaderUISetting("FrmItemMaster", "", "", ItemV_Type.SIZE, "", "", "Dgl1", FrmItemMaster.hcSizeUnit, 0, 0, 0, 0, "")
        ClsObj.FUpdateSeed_EntryHeaderUISetting("FrmItemMaster", "", "", ItemV_Type.SIZE, "", "", "Dgl1", FrmItemMaster.hcLength, 0, 0, 0, 0, "")
        ClsObj.FUpdateSeed_EntryHeaderUISetting("FrmItemMaster", "", "", ItemV_Type.SIZE, "", "", "Dgl1", FrmItemMaster.hcWidth, 0, 0, 0, 0, "")
        ClsObj.FUpdateSeed_EntryHeaderUISetting("FrmItemMaster", "", "", ItemV_Type.SIZE, "", "", "Dgl1", FrmItemMaster.hcThickness, 0, 0, 0, 0, "")
    End Sub
    Private Sub FConfigure_StockOpening(ClsObj As ClsMain)
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.OpeningStock, SettingFields.GenerateBarcodeYn, "1", AgDataType.YesNo, "50")

        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "DglMain", AgTemplate.TempTransaction1.hcSite_Code, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "DglMain", AgTemplate.TempTransaction1.hcV_Type, 1, 1, 1, "Doc Type")
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "DglMain", AgTemplate.TempTransaction1.hcV_Date, 1, 1, 1, "Doc Date")
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "DglMain", AgTemplate.TempTransaction1.hcV_No, 0, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "DglMain", AgTemplate.TempTransaction1.hcReferenceNo, 1, 1, 1, "Doc No")
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "DglMain", AgTemplate.TempTransaction1.hcSettingGroup, 0, 0, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "DglMain", FrmPurchInvoiceDirect_WithDimension.hcProcess, 0, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "DglMain", FrmPurchInvoiceDirect_WithDimension.hcVendor, 1, 0, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "DglMain", FrmPurchInvoiceDirect_WithDimension.hcBillToParty, 0, 0)

        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.ColSNo, True, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Barcode, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCategory, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemGroup, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCode, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Item, True)
        If ClsMain.IsScopeOfWorkContains(IndustryType.CommonModules.Dimension1) Then
            ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension1, True)
        End If
        If ClsMain.IsScopeOfWorkContains(IndustryType.CommonModules.Dimension2) Then
            ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension2, False)
        End If
        If ClsMain.IsScopeOfWorkContains(IndustryType.CommonModules.Dimension3) Then
            ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension3, False)
        End If
        If ClsMain.IsScopeOfWorkContains(IndustryType.CommonModules.Dimension4) Then
            ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension4, False)
        End If
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Size, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Specification, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1SalesTaxGroup, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1BaleNo, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1LotNo, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DocQty, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Unit, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Rate, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DiscountPer, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DiscountAmount, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionalDiscountPer, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionalDiscountAmount, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionPer, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionAmount, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Amount, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1MRP, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ProfitMarginPer, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1SaleRate, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1HSN, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Remark, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Godown, False)
    End Sub
    Private Sub FConfigure_Receipt(ClsObj As ClsMain)
        mQry = "Update Setting Set Value = '1'
                Where SettingType = '" & SettingType.General & "'
                And NCat = '" & Ncat.Receipt & "'
                And FieldName = '" & ClsMain.SettingFields.ShowContraWindowYn & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)

        ClsObj.FUpdateSeed_EntryLineUISetting("FrmVoucherEntry", "", Ncat.Receipt, "", "", "", "Dgl1", FrmVoucherEntry.Col1ReferenceNo, 1, 0, 1, "Against Bill No.")
    End Sub
    Private Sub FSeedTable_Subgroup(ClsObj As ClsMain)
        ClsObj.FSeedSingleIfNotExist_Subgroup(SubCode_Customer, "Customer", SubgroupType.Customer, "0020", "A", "Customer", "UNREGISTERED", "", AgL.PubDivCode, AgL.PubSiteCode, "", "System Defined")

        If AgL.VNull(AgL.Dman_Execute("SELECT Count(*) AS Cnt
                FROM PaymentModeAccount L 
                WHERE L.Code = '" & PaymentMode.Credit & "' ", AgL.GCn).ExecuteScalar()) = 0 Then
            mQry = "Insert Into PaymentModeAccount (Code, PaymentMode, PostToAc) 
                      Values ('" & PaymentMode.Credit & "', '" & PaymentMode.Credit & "', '" & SubCode_Customer & "')"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If
    End Sub
    Private Sub FConfigure_PurchaseGoodsReceipt(ClsObj As ClsMain)
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.PurchaseGoodsReceipt, SettingFields.GenerateBarcodeYn, "1", AgDataType.YesNo, "50")

        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseGoodsReceipt, "DglMain", AgTemplate.TempTransaction1.hcSite_Code, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseGoodsReceipt, "DglMain", AgTemplate.TempTransaction1.hcV_Type, 1, 1, 1, "Doc Type")
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseGoodsReceipt, "DglMain", AgTemplate.TempTransaction1.hcV_Date, 1, 1, 1, "Doc Date")
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseGoodsReceipt, "DglMain", AgTemplate.TempTransaction1.hcV_No, 0, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseGoodsReceipt, "DglMain", AgTemplate.TempTransaction1.hcReferenceNo, 1, 1, 1, "Doc No")
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseGoodsReceipt, "DglMain", AgTemplate.TempTransaction1.hcSettingGroup, 0, 0, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseGoodsReceipt, "DglMain", FrmPurchInvoiceDirect_WithDimension.hcProcess, 0, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseGoodsReceipt, "DglMain", FrmPurchInvoiceDirect_WithDimension.hcVendor, 1, 0, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseGoodsReceipt, "DglMain", FrmPurchInvoiceDirect_WithDimension.hcBillToParty, 0, 0)

        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseGoodsReceipt, "Dgl1", FrmPurchInvoiceDirect_WithDimension.ColSNo, True, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseGoodsReceipt, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Barcode, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseGoodsReceipt, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCategory, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseGoodsReceipt, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemGroup, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseGoodsReceipt, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCode, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseGoodsReceipt, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Item, True)
        If ClsMain.IsScopeOfWorkContains(IndustryType.CommonModules.Dimension1) Then
            ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseGoodsReceipt, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension1, True)
        End If
        If ClsMain.IsScopeOfWorkContains(IndustryType.CommonModules.Dimension2) Then
            ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseGoodsReceipt, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension2, False)
        End If
        If ClsMain.IsScopeOfWorkContains(IndustryType.CommonModules.Dimension3) Then
            ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseGoodsReceipt, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension3, False)
        End If
        If ClsMain.IsScopeOfWorkContains(IndustryType.CommonModules.Dimension4) Then
            ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseGoodsReceipt, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension4, False)
        End If
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseGoodsReceipt, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Size, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseGoodsReceipt, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Specification, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseGoodsReceipt, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1SalesTaxGroup, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseGoodsReceipt, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1BaleNo, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseGoodsReceipt, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1LotNo, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseGoodsReceipt, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DocQty, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseGoodsReceipt, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Unit, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseGoodsReceipt, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Rate, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseGoodsReceipt, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DiscountPer, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseGoodsReceipt, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DiscountAmount, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseGoodsReceipt, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionalDiscountPer, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseGoodsReceipt, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionalDiscountAmount, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseGoodsReceipt, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionPer, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseGoodsReceipt, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionAmount, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseGoodsReceipt, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Amount, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseGoodsReceipt, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1MRP, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseGoodsReceipt, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ProfitMarginPer, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseGoodsReceipt, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1SaleRate, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseGoodsReceipt, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1HSN, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseGoodsReceipt, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Remark, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseGoodsReceipt, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Godown, False)
    End Sub
End Class
