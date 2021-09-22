Imports AgLibrary.ClsMain.agConstants
Imports Customised.ClsMain

Public Class ClsGarmentProduction
    Public Const SettingGroup_FinishedMaterial As String = "FM"
    Public Const SettingGroup_RawMaterial As String = "RM"
    Public Const SettingGroup_OtherMaterial As String = "OM"
    Public Const SettingGroup_RawAndOtherMaterial As String = "RAOM"


    Private Const Process_Main As String = "PMain"
    Private Const Process_Sub As String = "PSub"


    Public Const Voucher_Type_CuttingOrder As String = "CORD"
    Public Const Voucher_Type_CuttingAndStitchingOrder As String = "CSORD"
    Public Const Voucher_Type_CuttingAndStitchingAndPackingOrder As String = "CSPOD"
    Public Const Voucher_Type_StitchingOrder As String = "SORD"
    Public Const Voucher_Type_OtherProcessOrder As String = "OORD"
    Public Const Voucher_Type_PackingOrder As String = "PKORD"

    Public Const Voucher_Type_CuttingReceive As String = "CREC"
    Public Const Voucher_Type_CuttingAndStitchingReceive As String = "CSREC"
    Public Const Voucher_Type_CuttingAndStitchingAndPackingReceive As String = "CSPRC"
    Public Const Voucher_Type_StitchingReceive As String = "SREC"
    Public Const Voucher_Type_OtherProcessReceive As String = "OREC"
    Public Const Voucher_Type_Packing As String = "PK"

    Public Const Voucher_Type_CuttingInvoice As String = "CINV"
    Public Const Voucher_Type_CuttingAndStitchingInvoice As String = "CSINV"
    Public Const Voucher_Type_CuttingAndStitchingAndPackingInvoice As String = "CSPIV"
    Public Const Voucher_Type_StitchingInvoice As String = "SINV"
    Public Const Voucher_Type_OtherProcessInvoice As String = "OINV"
    Public Const Voucher_Type_PackingInvoice As String = "PINV"


    Public Const Process_Cutting As String = "PCutting"
    Public Const Process_CuttingAndStitching As String = "PCtSt"
    Public Const Process_CuttingAndStitchingAndPacking As String = "PCSP"
    Public Const Process_Stitching As String = "PStitching"
    Public Const Process_Other As String = "POther"
    Public Const Process_Packing As String = "PPack"

    Public Const NCat_PaymentJobWorker As String = "PMJ"

    Private mQry As String = ""

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
    Public Sub FSeedData_GarmentsProductionIndustry()
        Dim ClsObj As New Customised.ClsMain(AgL)
        Try
            If ClsMain.IsScopeOfWorkContains(IndustryType.GarmentIndustry) Then
                If ClsMain.IsScopeOfWorkContains(IndustryType.SubIndustryType.ProductionModule) Then
                    ClsObj.AppendScopeOfWork(IndustryType.CommonModules.BOM)
                    ClsObj.AppendScopeOfWork(IndustryType.CommonModules.Dimension1)
                    ClsObj.AppendScopeOfWork(IndustryType.CommonModules.Dimension2)
                    ClsObj.AppendScopeOfWork(IndustryType.CommonModules.Dimension3)
                    ClsObj.AppendScopeOfWork(IndustryType.CommonModules.Dimension4)
                    ClsObj.AppendScopeOfWork(IndustryType.CommonModules.Size)
                    ClsObj.AppendScopeOfWork(IndustryType.CommonModules.RateListModule)
                    ClsObj.AppendScopeOfWork(IndustryType.CommonModules.CuttingConsumptionModule)
                    ClsObj.AppendScopeOfWork(IndustryType.CommonModules.SalesAgentModule)
                    ClsObj.AppendScopeOfWork(IndustryType.CommonModules.PurchaseAgentModule)
                    ClsObj.AppendScopeOfWork(IndustryType.CommonModules.PurchaseTransportModule)
                    ClsObj.AppendScopeOfWork(IndustryType.CommonModules.SalesTransportModule)

                    FInitVariables()

                    FConfigure_SettingGroup(ClsObj)
                    FConfigure_Process(ClsObj)
                    FConfigure_Dimensions(ClsObj)
                    FConfigure_ItemTypes(ClsObj)
                    FConfigure_ItemCategory(ClsObj)
                    FConfigure_ItemRatePattern(ClsObj)
                    FConfigure_RateList_MainProcess(ClsObj)
                    FConfigure_RateList_SubProcess(ClsObj)
                    FConfigure_RateListException(ClsObj)

                    FConfigure_VoucherType(ClsObj)

                    FConfigure_FinishedMaterialSaleInvoice(ClsObj)
                    FConfigure_RawMaterialSaleInvoice(ClsObj)
                    FConfigure_OtherMaterialSaleInvoice(ClsObj)

                    FConfigure_FinishedMaterialSaleReturn(ClsObj)
                    FConfigure_RawMaterialSaleReturn(ClsObj)
                    FConfigure_OtherMaterialSaleReturn(ClsObj)

                    FConfigure_FinishedMaterialPurchaseInvoice(ClsObj)
                    FConfigure_RawMaterialPurchaseInvoice(ClsObj)
                    FConfigure_OtherMaterialPurchaseInvoice(ClsObj)

                    FConfigure_FinishedMaterialPurchaseReturn(ClsObj)
                    FConfigure_RawMaterialPurchaseReturn(ClsObj)
                    FConfigure_OtherMaterialPurchaseReturn(ClsObj)

                    FConfigure_FinishedMaterialOpeningStock(ClsObj)
                    FConfigure_RawMaterialOpeningStock(ClsObj)
                    FConfigure_OtherMaterialOpeningStock(ClsObj)

                    FConfigure_FinishedMaterialStockIssue(ClsObj)
                    FConfigure_RawMaterialStockIssue(ClsObj)
                    FConfigure_OtherMaterialStockIssue(ClsObj)
                    FConfigure_RawAndOtherMaterialStockIssue(ClsObj)

                    FConfigure_FinishedMaterialStockReceive(ClsObj)
                    FConfigure_RawMaterialStockReceive(ClsObj)
                    FConfigure_OtherMaterialStockReceive(ClsObj)
                    FConfigure_RawAndOtherMaterialStockReceive(ClsObj)

                    FConfigure_JobOrder(ClsObj)
                    FConfigure_JobReceive(ClsObj)
                    FConfigure_JobInvoice(ClsObj)

                    FConfigure_CuttingOrder(ClsObj)
                    FConfigure_CuttingReceive(ClsObj)
                    FConfigure_CuttingInvoice(ClsObj)

                    FConfigure_CuttingAndStitchingOrder(ClsObj)
                    FConfigure_CuttingAndStitchingReceive(ClsObj)
                    FConfigure_CuttingAndStitchingInvoice(ClsObj)

                    FConfigure_CuttingAndStitchingAndPackingOrder(ClsObj)
                    FConfigure_CuttingAndStitchingAndPackingReceive(ClsObj)
                    FConfigure_CuttingAndStitchingAndPackingInvoice(ClsObj)

                    FConfigure_StitchingOrder(ClsObj)
                    FConfigure_StitchingReceive(ClsObj)
                    FConfigure_StitchingInvoice(ClsObj)

                    FConfigure_OtherOrder(ClsObj)
                    FConfigure_OtherReceive(ClsObj)
                    FConfigure_OtherInvoice(ClsObj)

                    FConfigure_PackingOrder(ClsObj)
                    FConfigure_Packing(ClsObj)
                    FConfigure_PackingInvoice(ClsObj)

                    FConfigure_Bom(ClsObj)
                    FConfigure_PaymentJobWorker(ClsObj)
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
    Private Sub FConfigure_FinishedMaterialSaleInvoice(ClsObj As ClsMain)
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmSaleInvoiceDirect", "", Ncat.SaleInvoice, "", "", "", "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1ItemCategory, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmSaleInvoiceDirect", "", Ncat.SaleInvoice, "", "", "", "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Dimension1, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmSaleInvoiceDirect", "", Ncat.SaleInvoice, "", "", "", "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Dimension2, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmSaleInvoiceDirect", "", Ncat.SaleInvoice, "", "", "", "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Dimension3, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmSaleInvoiceDirect", "", Ncat.SaleInvoice, "", "", "", "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Size, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmSaleInvoiceDirect", "", Ncat.SaleInvoice, "", "", "", "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Item, 0, 0, 1, "")


        ClsObj.FUpdateSeed_EntryLineUISetting("FrmSaleInvoiceReconciliation", "", Ncat.SaleInvoice, "", "", "", "Dgl1", FrmSaleInvoiceReconciliation_WithDimension.Col1ItemCategory, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmSaleInvoiceReconciliation", "", Ncat.SaleInvoice, "", "", "", "Dgl1", FrmSaleInvoiceReconciliation_WithDimension.Col1Dimension1, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmSaleInvoiceReconciliation", "", Ncat.SaleInvoice, "", "", "", "Dgl1", FrmSaleInvoiceReconciliation_WithDimension.Col1Dimension2, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmSaleInvoiceReconciliation", "", Ncat.SaleInvoice, "", "", "", "Dgl1", FrmSaleInvoiceReconciliation_WithDimension.Col1Dimension3, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmSaleInvoiceReconciliation", "", Ncat.SaleInvoice, "", "", "", "Dgl1", FrmSaleInvoiceReconciliation_WithDimension.Col1Size, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmSaleInvoiceReconciliation", "", Ncat.SaleInvoice, "", "", "", "Dgl1", FrmSaleInvoiceReconciliation_WithDimension.Col1Item, 0, 0, 1, "")


        mQry = "Update Setting Set Value ='+" & ItemTypeCode.ManufacturingProduct & "+" & ItemTypeCode.TradingProduct & "+" & ItemTypeCode.ServiceProduct & "'
                Where SettingType = '" & SettingType.General & "'
                And NCat = '" & Ncat.SaleInvoice & "'
                And SettingGroup Is Null
                And FieldName = '" & ClsMain.SettingFields.FilterInclude_ItemType & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.SaleInvoice, SettingFields.DefaultSettingGroup, SettingGroup_FinishedMaterial, AgDataType.Text, "255", mSettingGroupFieldQry, AgHelpQueryType.SqlQuery, AgHelpSelectionType.SingleSelect,,,,, ,, "+SUPPORT")

        ClsObj.FUpdateSeed_EntryHeaderUISetting("FrmSaleInvoiceDirect", "", Ncat.SaleInvoice, "", "", "", "DglMain", FrmSaleInvoiceDirect_WithDimension.hcSettingGroup, 1, 0, 1, 0, "")
    End Sub
    Private Sub FConfigure_RawMaterialSaleInvoice(ClsObj As ClsMain)
        'For Line
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceDirect_WithDimension.ColSNo, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Barcode, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1ItemCategory, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1ItemGroup, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1ItemCode, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Item, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Dimension1, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Dimension2, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Dimension3, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Dimension4, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Size, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Specification, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1ItemState, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1SalesTaxGroup, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1BaleNo, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1LotNo, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1DocQty, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Unit, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Pcs, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Rate, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1DiscountPer, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1DiscountAmount, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1AdditionalDiscountPer, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1AdditionalDiscountAmount, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1AdditionPer, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1AdditionAmount, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Amount, True,,, False,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Remark, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Godown, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1PurchaseRate, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1SaleInvoice, False, False, "Sale Order",,,, SettingGroup_RawMaterial)



        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDimension", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceDimension_WithDimension.ColSNo, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDimension", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceDimension_WithDimension.Col1ItemCategory, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDimension", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceDimension_WithDimension.Col1ItemGroup, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDimension", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceDimension_WithDimension.Col1Item, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDimension", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceDimension_WithDimension.Col1Dimension1, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDimension", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceDimension_WithDimension.Col1Dimension2, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDimension", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceDimension_WithDimension.Col1Dimension3, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDimension", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceDimension_WithDimension.Col1Dimension4, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDimension", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceDimension_WithDimension.Col1Size, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDimension", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceDimension_WithDimension.Col1Specification, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDimension", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceDimension_WithDimension.Col1Pcs, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDimension", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceDimension_WithDimension.Col1Qty, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDimension", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceDimension_WithDimension.Col1TotalQty, True,,,,,, SettingGroup_RawMaterial)


        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceReconciliation", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceReconciliation_WithDimension.ColSNo, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceReconciliation", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceReconciliation_WithDimension.Col1ItemCategory, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceReconciliation", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceReconciliation_WithDimension.Col1ItemGroup, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceReconciliation", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceReconciliation_WithDimension.Col1Dimension1, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceReconciliation", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceReconciliation_WithDimension.Col1Dimension4, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceReconciliation", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceReconciliation_WithDimension.Col1Specification, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceReconciliation", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceReconciliation_WithDimension.Col1SalesTaxGroup, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceReconciliation", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceReconciliation_WithDimension.Col1BaleNo, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceReconciliation", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceReconciliation_WithDimension.Col1LotNo, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceReconciliation", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceReconciliation_WithDimension.Col1DocQty, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceReconciliation", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceReconciliation_WithDimension.Col1Unit, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceReconciliation", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceReconciliation_WithDimension.Col1Pcs, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceReconciliation", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceReconciliation_WithDimension.Col1Rate, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceReconciliation", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceReconciliation_WithDimension.Col1DiscountPer, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceReconciliation", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceReconciliation_WithDimension.Col1DiscountAmount, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceReconciliation", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceReconciliation_WithDimension.Col1AdditionalDiscountPer, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceReconciliation", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceReconciliation_WithDimension.Col1AdditionalDiscountAmount, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceReconciliation", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceReconciliation_WithDimension.Col1Amount, True,,, False,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceReconciliation", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceReconciliation_WithDimension.Col1DimensionDetail, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceReconciliation", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceReconciliation_WithDimension.Col1Remark, False,,,,,, SettingGroup_RawMaterial)


        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.SaleInvoice, SettingFields.FilterInclude_ItemType, "+" & ItemTypeCode.RawProduct & "+" & ItemTypeCode.ServiceProduct, AgDataType.Text, "255", mItemTypeFieldQry, AgHelpQueryType.SqlQuery, AgHelpSelectionType.MultiSelect,,,,, SettingGroup_RawMaterial,, "+SUPPORT")
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.SaleInvoice, SettingFields.DocumentPrintReportFileName, "", AgDataType.Text, "50",,,,,,,, SettingGroup_RawMaterial,, "+SUPPORT")
    End Sub
    Private Sub FConfigure_OtherMaterialSaleInvoice(ClsObj As ClsMain)
        'For Line
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceDirect_WithDimension.ColSNo, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Barcode, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1ItemCategory, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1ItemGroup, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1ItemCode, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Item, True,,,,,, SettingGroup_OtherMaterial)
        If ClsMain.IsScopeOfWorkContains(IndustryType.CommonModules.Dimension1) Then
            ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Dimension1, False,,,,,, SettingGroup_OtherMaterial)
        End If
        If ClsMain.IsScopeOfWorkContains(IndustryType.CommonModules.Dimension2) Then
            ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Dimension2, False,,,,,, SettingGroup_OtherMaterial)
        End If
        If ClsMain.IsScopeOfWorkContains(IndustryType.CommonModules.Dimension3) Then
            ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Dimension3, False,,,,,, SettingGroup_OtherMaterial)
        End If
        If ClsMain.IsScopeOfWorkContains(IndustryType.CommonModules.Dimension4) Then
            ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Dimension4, False,,,,,, SettingGroup_OtherMaterial)
        End If
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Size, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Specification, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1ItemState, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1SalesTaxGroup, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1BaleNo, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1LotNo, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1DocQty, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Unit, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Pcs, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Rate, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1DiscountPer, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1DiscountAmount, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1AdditionalDiscountPer, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1AdditionalDiscountAmount, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1AdditionPer, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1AdditionAmount, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Amount, True,,, False,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Remark, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Godown, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1PurchaseRate, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1SaleInvoice, False, False, "Sale Order",,,, SettingGroup_OtherMaterial)


        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceReconciliation", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceReconciliation_WithDimension.ColSNo, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceReconciliation", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceReconciliation_WithDimension.Col1Barcode, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceReconciliation", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceReconciliation_WithDimension.Col1ItemCategory, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceReconciliation", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceReconciliation_WithDimension.Col1ItemGroup, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceReconciliation", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceReconciliation_WithDimension.Col1ItemCode, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceReconciliation", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceReconciliation_WithDimension.Col1Item, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceReconciliation", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceReconciliation_WithDimension.Col1Specification, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceReconciliation", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceReconciliation_WithDimension.Col1SalesTaxGroup, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceReconciliation", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceReconciliation_WithDimension.Col1BaleNo, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceReconciliation", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceReconciliation_WithDimension.Col1LotNo, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceReconciliation", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceReconciliation_WithDimension.Col1DocQty, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceReconciliation", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceReconciliation_WithDimension.Col1Unit, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceReconciliation", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceReconciliation_WithDimension.Col1Pcs, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceReconciliation", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceReconciliation_WithDimension.Col1Rate, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceReconciliation", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceReconciliation_WithDimension.Col1DiscountPer, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceReconciliation", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceReconciliation_WithDimension.Col1DiscountAmount, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceReconciliation", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceReconciliation_WithDimension.Col1AdditionalDiscountPer, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceReconciliation", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceReconciliation_WithDimension.Col1AdditionalDiscountAmount, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceReconciliation", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceReconciliation_WithDimension.Col1Amount, True,,, False,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceReconciliation", Ncat.SaleInvoice, "Dgl1", FrmSaleInvoiceReconciliation_WithDimension.Col1Remark, True,,,,,, SettingGroup_OtherMaterial)

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.SaleInvoice, SettingFields.FilterInclude_ItemType, "+" & ItemTypeCode.OtherProduct & "+" & ItemTypeCode.OtherRawProduct & "+" & ItemTypeCode.ServiceProduct, AgDataType.Text, "255", mItemTypeFieldQry, AgHelpQueryType.SqlQuery, AgHelpSelectionType.MultiSelect,,,,, SettingGroup_OtherMaterial,, "+SUPPORT")
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.SaleInvoice, SettingFields.DocumentPrintReportFileName, "", AgDataType.Text, "50",,,,,,,, SettingGroup_OtherMaterial,, "+SUPPORT")
    End Sub
    Private Sub FConfigure_RateList_MainProcess(ClsObj As ClsMain)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmRateList", "", "DglMain", FrmRateList.hcWEF, True, True,,,, Process_Main)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmRateList", "", "DglMain", FrmRateList.hcProcess, True, True,,,, Process_Main)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmRateList", "", "DglMain", FrmRateList.hcRateCategory, False,,,,, Process_Main)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmRateList", "", "DglMain", FrmRateList.hcItemCategory, True,,,,, Process_Main)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmRateList", "", "DglMain", FrmRateList.hcItemGroup, False,,,,, Process_Main)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmRateList", "", "DglMain", FrmRateList.hcItem, False,,,,, Process_Main)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmRateList", "", "DglMain", FrmRateList.hcDimension1, False,,,,, Process_Main)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmRateList", "", "DglMain", FrmRateList.hcDimension2, False,,,,, Process_Main)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmRateList", "", "DglMain", FrmRateList.hcDimension3, False,,,,, Process_Main)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmRateList", "", "DglMain", FrmRateList.hcDimension4, False,,,,, Process_Main)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmRateList", "", "DglMain", FrmRateList.hcSize, False,,,,, Process_Main)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmRateList", "", "DglMain", FrmRateList.hcParty, False,,,,, Process_Main)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmRateList", "", "DglMain", FrmRateList.hcRateType, True,,,,, Process_Main)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmRateList", "", "DglMain", FrmRateList.hcBtnFill, False,,,,, Process_Main)

        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmRateList", "", "Dgl1", FrmRateList.ColSNo, True,,,,, Process_Main)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmRateList", "", "Dgl1", FrmRateList.Col1Party, False,,,,, Process_Main)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmRateList", "", "Dgl1", FrmRateList.Col1RateType, False,,,,, Process_Main)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmRateList", "", "Dgl1", FrmRateList.Col1ItemCategory, False,,,,, Process_Main)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmRateList", "", "Dgl1", FrmRateList.Col1ItemGroup, False,,,,, Process_Main)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmRateList", "", "Dgl1", FrmRateList.Col1Item, False,,,,, Process_Main)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmRateList", "", "Dgl1", FrmRateList.Col1Dimension1, False,,,,, Process_Main)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmRateList", "", "Dgl1", FrmRateList.Col1Dimension2, False,,,,, Process_Main)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmRateList", "", "Dgl1", FrmRateList.Col1Dimension3, False,,,,, Process_Main)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmRateList", "", "Dgl1", FrmRateList.Col1Dimension4, False,,,,, Process_Main)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmRateList", "", "Dgl1", FrmRateList.Col1Size, True,,,,, Process_Main)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmRateList", "", "Dgl1", FrmRateList.Col1Rate, True,,,,, Process_Main)

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.RateList, SettingFields.AskToCopyRateYn, "1", AgDataType.YesNo, "50",,,,,,, Process_Main, ,, "+SUPPORT")
    End Sub
    Private Sub FConfigure_RateList_SubProcess(ClsObj As ClsMain)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmRateList", "", "DglMain", FrmRateList.hcWEF, True, True,,,, Process_Sub)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmRateList", "", "DglMain", FrmRateList.hcProcess, True, True,,,, Process_Sub)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmRateList", "", "DglMain", FrmRateList.hcRateCategory, False,,,,, Process_Sub)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmRateList", "", "DglMain", FrmRateList.hcItemCategory, False,,,,, Process_Sub)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmRateList", "", "DglMain", FrmRateList.hcItemGroup, False,,,,, Process_Sub)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmRateList", "", "DglMain", FrmRateList.hcItem, False,,,,, Process_Sub)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmRateList", "", "DglMain", FrmRateList.hcDimension1, False,,,,, Process_Sub)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmRateList", "", "DglMain", FrmRateList.hcDimension2, False,,,,, Process_Sub)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmRateList", "", "DglMain", FrmRateList.hcDimension3, True,,,,, Process_Sub)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmRateList", "", "DglMain", FrmRateList.hcDimension4, False,,,,, Process_Sub)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmRateList", "", "DglMain", FrmRateList.hcSize, False,,,,, Process_Sub)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmRateList", "", "DglMain", FrmRateList.hcParty, False,,,,, Process_Sub)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmRateList", "", "DglMain", FrmRateList.hcRateType, False,,,,, Process_Sub)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmRateList", "", "DglMain", FrmRateList.hcBtnFill, False,,,,, Process_Sub)

        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmRateList", "", "Dgl1", FrmRateList.ColSNo, True,,,,, Process_Sub)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmRateList", "", "Dgl1", FrmRateList.Col1Party, False,,,,, Process_Sub)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmRateList", "", "Dgl1", FrmRateList.Col1RateType, False,,,,, Process_Sub)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmRateList", "", "Dgl1", FrmRateList.Col1ItemCategory, True,,,,, Process_Sub)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmRateList", "", "Dgl1", FrmRateList.Col1ItemGroup, False,,,,, Process_Sub)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmRateList", "", "Dgl1", FrmRateList.Col1Item, False,,,,, Process_Sub)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmRateList", "", "Dgl1", FrmRateList.Col1Dimension1, False,,,,, Process_Sub)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmRateList", "", "Dgl1", FrmRateList.Col1Dimension2, False,,,,, Process_Sub)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmRateList", "", "Dgl1", FrmRateList.Col1Dimension3, False,,,,, Process_Sub)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmRateList", "", "Dgl1", FrmRateList.Col1Dimension4, False,,,,, Process_Sub)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmRateList", "", "Dgl1", FrmRateList.Col1Size, False,,,,, Process_Sub)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmRateList", "", "Dgl1", FrmRateList.Col1Rate, True,,,,, Process_Sub)

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.RateList, SettingFields.AskToCopyRateYn, "1", AgDataType.YesNo, "50",,,,,,, Process_Sub, ,, "+SUPPORT")
    End Sub
    Private Sub FConfigure_ItemRatePattern(ClsObj As ClsMain)
        Dim mMaxId As String = ""
        If AgL.VNull(AgL.Dman_Execute("Select Count(*) From ItemRatePattern Where Item Is Null And RateCategory Is Null And Process = '" & Process.Sales & "'", AgL.GCn).ExecuteScalar()) = 0 Then
            mMaxId = AgL.GetMaxId("ItemRatePattern", "Code", AgL.GcnMain, AgL.PubDivCode, AgL.PubSiteCode, 8, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)
            mQry = "INSERT INTO ItemRatePattern (Code, Item, Sr, Process, Pattern, RateCategory)
                                VALUES ('" & mMaxId & "', NULL, 1, '" & Process.Sales & "', 'Item Category,Dimension1,Size', NULL)"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        End If

        If AgL.VNull(AgL.Dman_Execute("Select Count(*) From ItemRatePattern Where Item Is Null And RateCategory = 'Rate Addition' And Process = '" & Process.Sales & "'", AgL.GCn).ExecuteScalar()) = 0 Then
            mMaxId = AgL.GetMaxId("ItemRatePattern", "Code", AgL.GcnMain, AgL.PubDivCode, AgL.PubSiteCode, 8, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)
            mQry = "INSERT INTO ItemRatePattern (Code, Item, Sr, Process, Pattern, RateCategory)
                                VALUES ('" & mMaxId & "', NULL, 1, '" & Process.Sales & "', 'Item Category,Dimension1,Dimension2,Dimension3', 'Rate Addition')"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        End If

        If AgL.VNull(AgL.Dman_Execute("Select Count(*) From ItemRatePattern Where Item Is Null And RateCategory Is Null And Process = '" & Process_Cutting & "'", AgL.GCn).ExecuteScalar()) = 0 Then
            mMaxId = AgL.GetMaxId("ItemRatePattern", "Code", AgL.GcnMain, AgL.PubDivCode, AgL.PubSiteCode, 8, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)
            mQry = "INSERT INTO ItemRatePattern (Code, Item, Sr, Process, Pattern, RateCategory)
                                VALUES ('" & mMaxId & "', NULL, 1, '" & Process_Cutting & "', 'Item Category,Size', NULL)"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        End If

        If AgL.VNull(AgL.Dman_Execute("Select Count(*) From ItemRatePattern Where Item Is Null And RateCategory Is Null And Process = '" & Process_Cutting & "'", AgL.GCn).ExecuteScalar()) = 0 Then
            mMaxId = AgL.GetMaxId("ItemRatePattern", "Code", AgL.GcnMain, AgL.PubDivCode, AgL.PubSiteCode, 8, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)
            mQry = "INSERT INTO ItemRatePattern (Code, Item, Sr, Process, Pattern, RateCategory)
                                VALUES ('" & mMaxId & "', NULL, 1, '" & Process_Cutting & "', 'Item Category,Size', NULL)"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        End If

        If AgL.VNull(AgL.Dman_Execute("Select Count(*) From ItemRatePattern Where Item Is Null And RateCategory Is Null And Process Is Null", AgL.GCn).ExecuteScalar()) = 0 Then
            mMaxId = AgL.GetMaxId("ItemRatePattern", "Code", AgL.GcnMain, AgL.PubDivCode, AgL.PubSiteCode, 8, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)
            mQry = "INSERT INTO ItemRatePattern (Code, Item, Sr, Process, RateCategory, Pattern)
                    VALUES ('" & mMaxId & "', NULL, 1, Null, NULL, 'Item Category,Size')"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        End If

        If AgL.VNull(AgL.Dman_Execute("Select Count(*) From ItemRatePattern Where Item Is Null And RateCategory = 'Rate Addition' And Process Is Null", AgL.GCn).ExecuteScalar()) = 0 Then
            mMaxId = AgL.GetMaxId("ItemRatePattern", "Code", AgL.GcnMain, AgL.PubDivCode, AgL.PubSiteCode, 8, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)
            mQry = "INSERT INTO ItemRatePattern (Code, Item, Sr, Process, Pattern, RateCategory)
                    VALUES ('" & mMaxId & "', NULL, 1, Null, 'Item Category,Dimension1,Dimension2,Dimension3', 'Rate Addition')"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        End If
    End Sub
    Private Sub FConfigure_ItemTypes(ClsObj As ClsMain)
        mQry = "Update Setting Set Value ='Dimension2' 
                                Where SettingType = '" & SettingType.Item & "' 
                                And NCat = '" & ItemTypeCode.ManufacturingProduct & "'
                                And SettingGroup Is Null
                                And FieldName = '" & ClsMain.SettingFields.MultiLineUIWindowBaseField & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.Item, "", ItemTypeCode.RawProduct, SettingFields.DimensionWindowBaseField, "Dimension4", AgDataType.Text, "255", mItemBaseFieldQry, AgHelpQueryType.SqlQuery, AgHelpSelectionType.SingleSelect,,,,, ,, "+SUPPORT")

        mQry = "Update Setting Set Value ='Size' Where FieldName = '" & ClsMain.SettingFields.ContraWindowBaseField & "' 
                    And NCat = '" & ItemTypeCode.ManufacturingProduct & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)

        mQry = "Update Setting Set Value ='Dimension4' Where FieldName = '" & ClsMain.SettingFields.ContraWindowBaseField & "' 
                    And NCat = '" & ItemTypeCode.RawProduct & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)

        mQry = "Update Setting Set Value ='Item' Where FieldName = '" & ClsMain.SettingFields.ContraWindowBaseField & "' 
                    And NCat = '" & ItemTypeCode.OtherProduct & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
    End Sub
    Private Sub FConfigure_RateListException(ClsObj As ClsMain)
        ClsObj.FSeedSingleIfNotExists_Voucher_Type("RTLE", "Rate List Exception", Ncat.RateList, Ncat.RateList)
    End Sub
    Private Sub FConfigure_Dimensions(ClsObj As ClsMain)
        mQry = "Update Setting Set Value ='Quality' Where FieldName = '" & ClsMain.SettingFields.Dimension1Caption & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        mQry = "Update Setting Set Value ='Colour' Where FieldName = '" & ClsMain.SettingFields.Dimension2Caption & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        mQry = "Update Setting Set Value ='Design' Where FieldName = '" & ClsMain.SettingFields.Dimension3Caption & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        mQry = "Update Setting Set Value ='Width' Where FieldName = '" & ClsMain.SettingFields.Dimension4Caption & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
    End Sub
    Private Sub FConfigure_Process(ClsObj As ClsMain)
        ClsObj.FSeedSingleIfNotExist_Subgroup("PMain", "Main Process", SubgroupType.Process, "0028", "R", "Expense", "", "", AgL.PubDivCode, AgL.PubSiteCode, "", "System Defined")
        ClsObj.FSeedSingleIfNotExist_Subgroup("PSub", "Sub Process", SubgroupType.Process, "0028", "R", "Expense", "", "", AgL.PubDivCode, AgL.PubSiteCode, "", "System Defined")

        mQry = " UPDATE SubGroup Set Status = 'InActive' Where SubCode = 'PMain'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)

        mQry = " UPDATE SubGroup Set Status = 'InActive' Where SubCode = 'PSub'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)

        ClsObj.FSeedSingleIfNotExist_Subgroup(Process_Cutting, "Cutting", SubgroupType.Process, "0028", "R", "Expense", "", "PMain", AgL.PubDivCode, AgL.PubSiteCode, "", "")
        ClsObj.FSeedSingleIfNotExist_Subgroup(Process_Stitching, "Stitching", SubgroupType.Process, "0028", "R", "Expense", "", "PMain", AgL.PubDivCode, AgL.PubSiteCode, "", "")
        If AgL.VNull(AgL.Dman_Execute("Select Count(*) From ProcessDetail Where SubCode = '" & Process_Stitching & "'", AgL.GcnMain).ExecuteScalar()) = 0 Then
            mQry = "INSERT INTO ProcessDetail (Subcode, ScopeOfWork, PrevProcess, CombinationOfProcesses, FirstProcessOfCombination, LastProcessOfCombination)
                    VALUES ('" & Process_Stitching & "', NULL, '" & Process_Cutting & "', Null, Null, Null) "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If
        ClsObj.FSeedSingleIfNotExist_Subgroup(Process_CuttingAndStitching, "Cutting + Stitching", SubgroupType.Process, "0028", "R", "Expense", "", "PMain", AgL.PubDivCode, AgL.PubSiteCode, "", "")
        If AgL.VNull(AgL.Dman_Execute("Select Count(*) From ProcessDetail Where SubCode = '" & Process_CuttingAndStitching & "'", AgL.GcnMain).ExecuteScalar()) = 0 Then
            mQry = "INSERT INTO ProcessDetail (Subcode, ScopeOfWork, PrevProcess, CombinationOfProcesses, FirstProcessOfCombination, LastProcessOfCombination)
                    VALUES ('" & Process_CuttingAndStitching & "', NULL, NULL, '" & Process_Cutting & "," & Process_Stitching & "', '" & Process_Cutting & "', '" & Process_Stitching & "') "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If
        ClsObj.FSeedSingleIfNotExist_Subgroup(Process_CuttingAndStitchingAndPacking, "Cutting + Stitching + Packing", SubgroupType.Process, "0028", "R", "Expense", "", "PMain", AgL.PubDivCode, AgL.PubSiteCode, "", "")
        If AgL.VNull(AgL.Dman_Execute("Select Count(*) From ProcessDetail Where SubCode = '" & Process_CuttingAndStitchingAndPacking & "'", AgL.GcnMain).ExecuteScalar()) = 0 Then
            mQry = "INSERT INTO ProcessDetail (Subcode, ScopeOfWork, PrevProcess, CombinationOfProcesses, FirstProcessOfCombination, LastProcessOfCombination)
                    VALUES ('" & Process_CuttingAndStitchingAndPacking & "', NULL, NULL, '" & Process_Cutting & "," & Process_Stitching & "," & Process_Packing & "', '" & Process_Cutting & "', '" & Process_Packing & "') "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If
        ClsObj.FSeedSingleIfNotExist_Subgroup(Process_Other, "Other", SubgroupType.Process, "0028", "R", "Expense", "", "PMain", AgL.PubDivCode, AgL.PubSiteCode, "", "")
        ClsObj.FSeedSingleIfNotExist_Subgroup(Process_Packing, "Packing", SubgroupType.Process, "0028", "R", "Expense", "", "PMain", AgL.PubDivCode, AgL.PubSiteCode, "", "")
        If AgL.VNull(AgL.Dman_Execute("Select Count(*) From ProcessDetail Where SubCode = '" & Process_Packing & "'", AgL.GcnMain).ExecuteScalar()) = 0 Then
            mQry = "INSERT INTO ProcessDetail (Subcode, ScopeOfWork, PrevProcess, CombinationOfProcesses, FirstProcessOfCombination, LastProcessOfCombination)
                    VALUES ('" & Process_Packing & "', NULL, '" & Process_Stitching & "', NULL, NULL, NULL) "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If

        'ClsObj.FSeedSingleIfNotExist_Subgroup("PPress", "Pressing & Packing", SubgroupType.Process, "0028", "R", "Expense", "", "PMain", AgL.PubDivCode, AgL.PubSiteCode, "", "")
        'ClsObj.FSeedSingleIfNotExist_Subgroup("PPack", "Packing", SubgroupType.Process, "0028", "R", "Expense", "", "PMain", AgL.PubDivCode, AgL.PubSiteCode, "", "")


        'ClsObj.FSeedSingleIfNotExist_Subgroup("PCutting", "Cutting", SubgroupType.Process, "0028", "R", "Expense", "", "PSub", AgL.PubDivCode, AgL.PubSiteCode, "", "")
        'ClsObj.FSeedSingleIfNotExist_Subgroup("PPrinting", "Stitching", SubgroupType.Process, "0028", "R", "Expense", "", "PSub", AgL.PubDivCode, AgL.PubSiteCode, "", "")
        'ClsObj.FSeedSingleIfNotExist_Subgroup("PKazz", "Kazz", SubgroupType.Process, "0028", "R", "Expense", "", "PSub", AgL.PubDivCode, AgL.PubSiteCode, "", "")
        'ClsObj.FSeedSingleIfNotExist_Subgroup("PButton", "Embroidery", SubgroupType.Process, "0028", "R", "Expense", "", "PSub", AgL.PubDivCode, AgL.PubSiteCode, "", "")
        'ClsObj.FSeedSingleIfNotExist_Subgroup("PWash", "Washing", SubgroupType.Process, "0028", "R", "Expense", "", "PSub", AgL.PubDivCode, AgL.PubSiteCode, "", "")
    End Sub
    Private Sub FConfigure_FinishedMaterialPurchaseInvoice(ClsObj As ClsMain)
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchInvoiceDirect", "", Ncat.PurchaseInvoice, "", "", "", "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCategory, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchInvoiceDirect", "", Ncat.PurchaseInvoice, "", "", "", "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension1, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchInvoiceDirect", "", Ncat.PurchaseInvoice, "", "", "", "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension2, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchInvoiceDirect", "", Ncat.PurchaseInvoice, "", "", "", "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension3, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchInvoiceDirect", "", Ncat.PurchaseInvoice, "", "", "", "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Size, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchInvoiceDirect", "", Ncat.PurchaseInvoice, "", "", "", "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Item, 0, 0, 1, "")

        mQry = "Update Setting Set Value ='+" & ItemTypeCode.ManufacturingProduct & "+" & ItemTypeCode.TradingProduct & "+" & ItemTypeCode.ServiceProduct & "'
                WHERE NCat = '" & Ncat.PurchaseInvoice & "' 
                And SettingGroup Is Null
                AND FieldName = '" & ClsMain.SettingFields.FilterInclude_ItemType & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.PurchaseInvoice, SettingFields.DefaultSettingGroup, SettingGroup_FinishedMaterial, AgDataType.Text, "255", mSettingGroupFieldQry, AgHelpQueryType.SqlQuery, AgHelpSelectionType.SingleSelect,,,,, ,, "+SUPPORT")

        ClsObj.FUpdateSeed_EntryHeaderUISetting("FrmPurchInvoiceDirect", "", Ncat.PurchaseInvoice, "", "", "", "DglMain", FrmPurchInvoiceDirect_WithDimension.hcSettingGroup, 1, 0, 1, 1, "")
    End Sub
    Private Sub FConfigure_SettingGroup(ClsObj As ClsMain)
        If AgL.FillData("Select Count(*) from SettingGroup Where Code = '" & SettingGroup_FinishedMaterial & "' ", AgL.GcnMain).tables(0).Rows(0)(0) = 0 Then
            mQry = " Insert into SettingGroup (Code, Name)
                        Select '" & SettingGroup_FinishedMaterial & "','Finished Material'"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If

        If AgL.FillData("Select Count(*) from SettingGroup Where Code = '" & SettingGroup_RawMaterial & "' ", AgL.GcnMain).tables(0).Rows(0)(0) = 0 Then
            mQry = " Insert into SettingGroup (Code, Name)
                        Select '" & SettingGroup_RawMaterial & "','Raw Material'"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If

        If AgL.FillData("Select Count(*) from SettingGroup Where Code = '" & SettingGroup_OtherMaterial & "' ", AgL.GcnMain).tables(0).Rows(0)(0) = 0 Then
            mQry = " Insert into SettingGroup (Code, Name)
                        Select '" & SettingGroup_OtherMaterial & "','Other Material'"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If

        If AgL.FillData("Select Count(*) from SettingGroup Where Code = '" & SettingGroup_RawAndOtherMaterial & "' ", AgL.GcnMain).tables(0).Rows(0)(0) = 0 Then
            mQry = " Insert into SettingGroup (Code, Name)
                        Select '" & SettingGroup_RawAndOtherMaterial & "','Raw & Other Material'"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If
    End Sub
    Private Sub FConfigure_RawMaterialPurchaseInvoice(ClsObj As ClsMain)
        'For Line
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.ColSNo, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Barcode, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCategory, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemGroup, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCode, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Item, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension1, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension2, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension3, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension4, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Size, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Specification, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1SalesTaxGroup, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1BaleNo, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1LotNo, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DocQty, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Unit, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1StockUnitMultiplier, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1StockUnit, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1StockQty, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Rate, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DiscountPer, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DiscountAmount, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionalDiscountPer, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionalDiscountAmount, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionPer, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionAmount, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Amount, True,,, False,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Remark, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Godown, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1PurchaseInvoice, False, False, "Order No",,,, SettingGroup_RawMaterial)

        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.PurchaseInvoice, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.ColSNo, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.PurchaseInvoice, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.Col1ItemCategory, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.PurchaseInvoice, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.Col1ItemGroup, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.PurchaseInvoice, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.Col1Item, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.PurchaseInvoice, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.Col1Dimension1, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.PurchaseInvoice, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.Col1Dimension2, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.PurchaseInvoice, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.Col1Dimension3, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.PurchaseInvoice, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.Col1Dimension4, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.PurchaseInvoice, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.Col1Size, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.PurchaseInvoice, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.Col1Specification, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.PurchaseInvoice, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.Col1Pcs, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.PurchaseInvoice, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.Col1Qty, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.PurchaseInvoice, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.Col1TotalQty, True,,,,,, SettingGroup_RawMaterial)


        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.PurchaseInvoice, SettingFields.FilterInclude_ItemType, "+" & ItemTypeCode.RawProduct & "+" & ItemTypeCode.ServiceProduct, AgDataType.Text, "255", mItemTypeFieldQry, AgHelpQueryType.SqlQuery, AgHelpSelectionType.MultiSelect,,,,, SettingGroup_RawMaterial,, "+SUPPORT")
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.PurchaseInvoice, SettingFields.DocumentPrintReportFileName, "", AgDataType.Text, "50",,,,,,,, SettingGroup_RawMaterial,, "+SUPPORT")
    End Sub

    Private Sub FConfigure_OtherMaterialPurchaseInvoice(ClsObj As ClsMain)
        'For Line
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.ColSNo, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Barcode, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCategory, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemGroup, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCode, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Item, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension1, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension2, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension3, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension4, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Size, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Specification, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1SalesTaxGroup, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1BaleNo, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1LotNo, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DocQty, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Unit, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1StockUnitMultiplier, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1StockUnit, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1StockQty, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Rate, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DiscountPer, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DiscountAmount, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionalDiscountPer, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionalDiscountAmount, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionPer, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionAmount, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Amount, True,,, False,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Remark, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Godown, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1PurchaseInvoice, False, False, "Order No",,,, SettingGroup_OtherMaterial)

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.PurchaseInvoice, SettingFields.FilterInclude_ItemType, "+" & ItemTypeCode.OtherProduct & "+" & ItemTypeCode.OtherRawProduct & "+" & ItemTypeCode.ServiceProduct, AgDataType.Text, "255", mItemTypeFieldQry, AgHelpQueryType.SqlQuery, AgHelpSelectionType.MultiSelect,,,,, SettingGroup_OtherMaterial,, "+SUPPORT")
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.PurchaseInvoice, SettingFields.DocumentPrintReportFileName, "", AgDataType.Text, "50",,,,,,,, SettingGroup_OtherMaterial,, "+SUPPORT")
    End Sub
    Private Sub FConfigure_FinishedMaterialOpeningStock(ClsObj As ClsMain)
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchInvoiceDirect", "", Ncat.OpeningStock, "", "", "", "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCategory, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchInvoiceDirect", "", Ncat.OpeningStock, "", "", "", "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension1, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchInvoiceDirect", "", Ncat.OpeningStock, "", "", "", "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension2, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchInvoiceDirect", "", Ncat.OpeningStock, "", "", "", "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension3, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchInvoiceDirect", "", Ncat.OpeningStock, "", "", "", "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Size, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchInvoiceDirect", "", Ncat.OpeningStock, "", "", "", "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Item, 0, 0, 1, "")


        mQry = " UPDATE Setting SET Value ='+" & ItemTypeCode.ManufacturingProduct & "+" & ItemTypeCode.TradingProduct & "'
                    WHERE NCat = '" & Ncat.OpeningStock & "' 
                    AND SettingGroup Is Null 
                    AND FieldName = '" & ClsMain.SettingFields.FilterInclude_ItemType & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.OpeningStock, SettingFields.DefaultSettingGroup, SettingGroup_FinishedMaterial, AgDataType.Text, "255", mSettingGroupFieldQry, AgHelpQueryType.SqlQuery, AgHelpSelectionType.SingleSelect,,,,, ,, "+SUPPORT")

        ClsObj.FUpdateSeed_EntryHeaderUISetting("FrmPurchInvoiceDirect", "", Ncat.OpeningStock, "", "", "", "DglMain", FrmPurchInvoiceDirect_WithDimension.hcSettingGroup, 1, 0, 1, 1, "")
    End Sub
    Private Sub FConfigure_RawMaterialOpeningStock(ClsObj As ClsMain)
        'For Line
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.ColSNo, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Barcode, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCategory, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemGroup, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCode, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Item, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension1, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension2, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension3, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension4, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Size, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Specification, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1BaleNo, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1LotNo, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DocQty, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Unit, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Rate, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Amount, False,,, False,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Remark, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Godown, False,,,,,, SettingGroup_RawMaterial)

        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.OpeningStock, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.ColSNo, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.OpeningStock, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.Col1ItemCategory, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.OpeningStock, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.Col1ItemGroup, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.OpeningStock, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.Col1Item, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.OpeningStock, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.Col1Dimension1, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.OpeningStock, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.Col1Dimension2, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.OpeningStock, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.Col1Dimension3, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.OpeningStock, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.Col1Dimension4, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.OpeningStock, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.Col1Size, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.OpeningStock, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.Col1Specification, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.OpeningStock, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.Col1Pcs, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.OpeningStock, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.Col1Qty, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.OpeningStock, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.Col1TotalQty, True,,,,,, SettingGroup_RawMaterial)

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.OpeningStock, SettingFields.FilterInclude_ItemType, "+" & ItemTypeCode.RawProduct, AgDataType.Text, "255", mItemTypeFieldQry, AgHelpQueryType.SqlQuery, AgHelpSelectionType.MultiSelect,,,,, SettingGroup_RawMaterial,, "+SUPPORT")
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.OpeningStock, SettingFields.DocumentPrintReportFileName, "", AgDataType.Text, "50",,,,,,,, SettingGroup_RawMaterial,, "+SUPPORT")

        ClsObj.FUpdateSeed_EntryHeaderUISetting("FrmPurchInvoiceDirect", "", Ncat.OpeningStock, "", "", SettingGroup_RawMaterial, "DglMain", FrmPurchInvoiceDirect_WithDimension.hcSettingGroup, 1, 0, 1, 1, "")
    End Sub
    Private Sub FConfigure_OtherMaterialOpeningStock(ClsObj As ClsMain)
        'For Line
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.ColSNo, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Barcode, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCategory, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemGroup, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCode, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Item, True,,,,,, SettingGroup_OtherMaterial)
        If ClsMain.IsScopeOfWorkContains(IndustryType.CommonModules.Dimension1) Then
            ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension1, False,,,,,, SettingGroup_OtherMaterial)
        End If
        If ClsMain.IsScopeOfWorkContains(IndustryType.CommonModules.Dimension2) Then
            ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension2, False,,,,,, SettingGroup_OtherMaterial)
        End If
        If ClsMain.IsScopeOfWorkContains(IndustryType.CommonModules.Dimension3) Then
            ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension3, False,,,,,, SettingGroup_OtherMaterial)
        End If
        If ClsMain.IsScopeOfWorkContains(IndustryType.CommonModules.Dimension4) Then
            ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension4, False,,,,,, SettingGroup_OtherMaterial)
        End If
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Size, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Specification, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemState, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1BaleNo, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1LotNo, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DocQty, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Unit, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Pcs, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Rate, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Amount, False,,, False,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Remark, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Godown, False,,,,,, SettingGroup_OtherMaterial)

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.OpeningStock, SettingFields.FilterInclude_ItemType, "+" & ItemTypeCode.OtherProduct, AgDataType.Text, "255", mItemTypeFieldQry, AgHelpQueryType.SqlQuery, AgHelpSelectionType.MultiSelect,,,,, SettingGroup_OtherMaterial,, "+SUPPORT")
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.OpeningStock, SettingFields.DocumentPrintReportFileName, "", AgDataType.Text, "50",,,,,,,, SettingGroup_OtherMaterial,, "+SUPPORT")

        ClsObj.FUpdateSeed_EntryHeaderUISetting("FrmPurchInvoiceDirect", "", Ncat.OpeningStock, "", "", SettingGroup_OtherMaterial, "DglMain", FrmPurchInvoiceDirect_WithDimension.hcSettingGroup, 1, 0, 1, 1, "")
    End Sub
    Private Sub FConfigure_FinishedMaterialSaleReturn(ClsObj As ClsMain)
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmSaleInvoiceDirect", "", Ncat.SaleReturn, "", "", "", "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1ItemCategory, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmSaleInvoiceDirect", "", Ncat.SaleReturn, "", "", "", "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Dimension1, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmSaleInvoiceDirect", "", Ncat.SaleReturn, "", "", "", "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Dimension2, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmSaleInvoiceDirect", "", Ncat.SaleReturn, "", "", "", "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Dimension3, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmSaleInvoiceDirect", "", Ncat.SaleReturn, "", "", "", "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Size, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmSaleInvoiceDirect", "", Ncat.SaleReturn, "", "", "", "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Item, 0, 0, 1, "")

        mQry = "Update Setting Set Value ='+" & ItemTypeCode.ManufacturingProduct & "+" & ItemTypeCode.TradingProduct & "+" & ItemTypeCode.ServiceProduct & "'
                Where SettingType = '" & SettingType.General & "'
                And NCat = '" & Ncat.SaleReturn & "'
                And SettingGroup Is Null
                And FieldName = '" & ClsMain.SettingFields.FilterInclude_ItemType & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.SaleReturn, SettingFields.DefaultSettingGroup, SettingGroup_FinishedMaterial, AgDataType.Text, "255", mSettingGroupFieldQry, AgHelpQueryType.SqlQuery, AgHelpSelectionType.SingleSelect,,,,, ,, "+SUPPORT")

        ClsObj.FUpdateSeed_EntryHeaderUISetting("FrmSaleInvoiceDirect", "", Ncat.SaleReturn, "", "", "", "DglMain", FrmSaleInvoiceDirect_WithDimension.hcSettingGroup, 1, 0, 1, 0, "")
    End Sub
    Private Sub FConfigure_RawMaterialSaleReturn(ClsObj As ClsMain)
        'For Line
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "Dgl1", FrmSaleInvoiceDirect_WithDimension.ColSNo, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Barcode, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1ItemCategory, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1ItemGroup, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1ItemCode, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Item, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Dimension1, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Dimension2, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Dimension3, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Dimension4, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Size, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Specification, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1ItemState, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1SalesTaxGroup, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1BaleNo, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1LotNo, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1DocQty, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Unit, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Pcs, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Rate, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1DiscountPer, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1DiscountAmount, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1AdditionalDiscountPer, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1AdditionalDiscountAmount, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1AdditionPer, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1AdditionAmount, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Amount, True,,, False,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Remark, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Godown, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1PurchaseRate, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1SaleInvoice, False, False, "Sale Order",,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1ReferenceNo, True, False, "Against Inv.No",,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1ReferenceDate, True, False, "Against Inv.Date",,,, SettingGroup_RawMaterial)




        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDimension", Ncat.SaleReturn, "Dgl1", FrmSaleInvoiceDimension_WithDimension.ColSNo, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDimension", Ncat.SaleReturn, "Dgl1", FrmSaleInvoiceDimension_WithDimension.Col1ItemCategory, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDimension", Ncat.SaleReturn, "Dgl1", FrmSaleInvoiceDimension_WithDimension.Col1ItemGroup, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDimension", Ncat.SaleReturn, "Dgl1", FrmSaleInvoiceDimension_WithDimension.Col1Item, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDimension", Ncat.SaleReturn, "Dgl1", FrmSaleInvoiceDimension_WithDimension.Col1Dimension1, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDimension", Ncat.SaleReturn, "Dgl1", FrmSaleInvoiceDimension_WithDimension.Col1Dimension2, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDimension", Ncat.SaleReturn, "Dgl1", FrmSaleInvoiceDimension_WithDimension.Col1Dimension3, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDimension", Ncat.SaleReturn, "Dgl1", FrmSaleInvoiceDimension_WithDimension.Col1Dimension4, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDimension", Ncat.SaleReturn, "Dgl1", FrmSaleInvoiceDimension_WithDimension.Col1Size, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDimension", Ncat.SaleReturn, "Dgl1", FrmSaleInvoiceDimension_WithDimension.Col1Specification, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDimension", Ncat.SaleReturn, "Dgl1", FrmSaleInvoiceDimension_WithDimension.Col1Pcs, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDimension", Ncat.SaleReturn, "Dgl1", FrmSaleInvoiceDimension_WithDimension.Col1Qty, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDimension", Ncat.SaleReturn, "Dgl1", FrmSaleInvoiceDimension_WithDimension.Col1TotalQty, True,,,,,, SettingGroup_RawMaterial)

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.SaleReturn, SettingFields.FilterInclude_ItemType, "+" & ItemTypeCode.RawProduct & "+" & ItemTypeCode.ServiceProduct, AgDataType.Text, "255", mItemTypeFieldQry, AgHelpQueryType.SqlQuery, AgHelpSelectionType.MultiSelect,,,,, SettingGroup_RawMaterial,, "+SUPPORT")
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.SaleReturn, SettingFields.DocumentPrintReportFileName, "", AgDataType.Text, "50",,,,,,,, SettingGroup_RawMaterial,, "+SUPPORT")
    End Sub
    Private Sub FConfigure_OtherMaterialSaleReturn(ClsObj As ClsMain)
        'For Line
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "Dgl1", FrmSaleInvoiceDirect_WithDimension.ColSNo, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Barcode, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1ItemCategory, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1ItemGroup, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1ItemCode, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Item, True,,,,,, SettingGroup_OtherMaterial)
        If ClsMain.IsScopeOfWorkContains(IndustryType.CommonModules.Dimension1) Then
            ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Dimension1, False,,,,,, SettingGroup_OtherMaterial)
        End If
        If ClsMain.IsScopeOfWorkContains(IndustryType.CommonModules.Dimension2) Then
            ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Dimension2, False,,,,,, SettingGroup_OtherMaterial)
        End If
        If ClsMain.IsScopeOfWorkContains(IndustryType.CommonModules.Dimension3) Then
            ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Dimension3, False,,,,,, SettingGroup_OtherMaterial)
        End If
        If ClsMain.IsScopeOfWorkContains(IndustryType.CommonModules.Dimension4) Then
            ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Dimension4, False,,,,,, SettingGroup_OtherMaterial)
        End If
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Size, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Specification, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1ItemState, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1SalesTaxGroup, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1BaleNo, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1LotNo, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1DocQty, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Unit, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Pcs, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Rate, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1DiscountPer, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1DiscountAmount, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1AdditionalDiscountPer, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1AdditionalDiscountAmount, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1AdditionPer, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1AdditionAmount, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Amount, True,,, False,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Remark, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Godown, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1PurchaseRate, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1SaleInvoice, False, False, "Sale Order",,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1ReferenceNo, True, False, "Against Inv.No",,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1ReferenceDate, True, False, "Against Inv.Date",,,, SettingGroup_OtherMaterial)


        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.SaleReturn, SettingFields.FilterInclude_ItemType, "+" & ItemTypeCode.OtherProduct & "+" & ItemTypeCode.OtherRawProduct & "+" & ItemTypeCode.ServiceProduct, AgDataType.Text, "255", mItemTypeFieldQry, AgHelpQueryType.SqlQuery, AgHelpSelectionType.MultiSelect,,,,, SettingGroup_OtherMaterial,, "+SUPPORT")
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.SaleReturn, SettingFields.DocumentPrintReportFileName, "", AgDataType.Text, "50",,,,,,,, SettingGroup_OtherMaterial,, "+SUPPORT")
    End Sub
    Private Sub FConfigure_FinishedMaterialPurchaseReturn(ClsObj As ClsMain)
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchInvoiceDirect", "", Ncat.PurchaseReturn, "", "", "", "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCategory, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchInvoiceDirect", "", Ncat.PurchaseReturn, "", "", "", "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension1, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchInvoiceDirect", "", Ncat.PurchaseReturn, "", "", "", "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension2, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchInvoiceDirect", "", Ncat.PurchaseReturn, "", "", "", "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension3, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchInvoiceDirect", "", Ncat.PurchaseReturn, "", "", "", "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Size, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchInvoiceDirect", "", Ncat.PurchaseReturn, "", "", "", "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Item, 0, 0, 1, "")

        mQry = "Update Setting Set Value ='+" & ItemTypeCode.ManufacturingProduct & "+" & ItemTypeCode.TradingProduct & "+" & ItemTypeCode.ServiceProduct & "'
                WHERE NCat = '" & Ncat.PurchaseReturn & "' 
                AND SettingGroup Is Null 
                AND FieldName = '" & ClsMain.SettingFields.FilterInclude_ItemType & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.PurchaseReturn, SettingFields.DefaultSettingGroup, SettingGroup_FinishedMaterial, AgDataType.Text, "255", mSettingGroupFieldQry, AgHelpQueryType.SqlQuery, AgHelpSelectionType.SingleSelect,,,,, ,, "+SUPPORT")

        ClsObj.FUpdateSeed_EntryHeaderUISetting("FrmPurchInvoiceDirect", "", Ncat.PurchaseReturn, "", "", "", "DglMain", FrmPurchInvoiceDirect_WithDimension.hcSettingGroup, 1, 0, 1, 1, "")
    End Sub
    Private Sub FConfigure_RawMaterialPurchaseReturn(ClsObj As ClsMain)
        'For Line
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseReturn, "Dgl1", FrmPurchInvoiceDirect_WithDimension.ColSNo, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseReturn, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Barcode, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseReturn, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCategory, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseReturn, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemGroup, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseReturn, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCode, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseReturn, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Item, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseReturn, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension1, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseReturn, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension2, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseReturn, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension3, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseReturn, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension4, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseReturn, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Size, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseReturn, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Specification, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseReturn, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1SalesTaxGroup, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseReturn, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1BaleNo, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseReturn, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1LotNo, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseReturn, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DocQty, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseReturn, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Unit, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseReturn, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Rate, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseReturn, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DiscountPer, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseReturn, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DiscountAmount, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseReturn, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionalDiscountPer, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseReturn, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionalDiscountAmount, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseReturn, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionPer, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseReturn, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionAmount, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseReturn, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Amount, True,,, False,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseReturn, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Remark, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseReturn, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Godown, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseReturn, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1PurchaseInvoice, False, False, "Order No",,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseReturn, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ReferenceNo, True, False, "Against Inv.No",,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseReturn, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ReferenceDate, True, False, "Against Inv.Date",,,, SettingGroup_RawMaterial)


        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.PurchaseReturn, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.ColSNo, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.PurchaseReturn, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.Col1ItemCategory, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.PurchaseReturn, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.Col1ItemGroup, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.PurchaseReturn, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.Col1Item, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.PurchaseReturn, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.Col1Dimension1, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.PurchaseReturn, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.Col1Dimension2, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.PurchaseReturn, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.Col1Dimension3, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.PurchaseReturn, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.Col1Dimension4, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.PurchaseReturn, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.Col1Size, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.PurchaseReturn, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.Col1Specification, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.PurchaseReturn, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.Col1Pcs, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.PurchaseReturn, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.Col1Qty, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.PurchaseReturn, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.Col1TotalQty, True,,,,,, SettingGroup_RawMaterial)


        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.PurchaseReturn, SettingFields.FilterInclude_ItemType, "+" & ItemTypeCode.RawProduct & "+" & ItemTypeCode.ServiceProduct, AgDataType.Text, "255", mItemTypeFieldQry, AgHelpQueryType.SqlQuery, AgHelpSelectionType.MultiSelect,,,,, SettingGroup_RawMaterial,, "+SUPPORT")
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.PurchaseReturn, SettingFields.DocumentPrintReportFileName, "", AgDataType.Text, "50",,,,,,,, SettingGroup_RawMaterial,, "+SUPPORT")
    End Sub

    Private Sub FConfigure_OtherMaterialPurchaseReturn(ClsObj As ClsMain)
        'For Line
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseReturn, "Dgl1", FrmPurchInvoiceDirect_WithDimension.ColSNo, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseReturn, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Barcode, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseReturn, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCategory, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseReturn, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemGroup, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseReturn, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCode, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseReturn, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Item, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseReturn, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension1, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseReturn, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension2, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseReturn, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension3, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseReturn, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension4, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseReturn, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Size, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseReturn, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Specification, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseReturn, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1SalesTaxGroup, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseReturn, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1BaleNo, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseReturn, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1LotNo, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseReturn, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DocQty, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseReturn, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Unit, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseReturn, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Rate, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseReturn, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DiscountPer, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseReturn, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DiscountAmount, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseReturn, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionalDiscountPer, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseReturn, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionalDiscountAmount, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseReturn, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionPer, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseReturn, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionAmount, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseReturn, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Amount, True,,, False,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseReturn, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Remark, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseReturn, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Godown, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseReturn, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1PurchaseInvoice, False, False, "Order No",,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseReturn, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ReferenceNo, True, False, "Against Inv.No",,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseReturn, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ReferenceDate, True, False, "Against Inv.Date",,,, SettingGroup_OtherMaterial)


        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.PurchaseReturn, SettingFields.FilterInclude_ItemType, "+" & ItemTypeCode.OtherProduct & "+" & ItemTypeCode.OtherRawProduct & "+" & ItemTypeCode.ServiceProduct, AgDataType.Text, "255", mItemTypeFieldQry, AgHelpQueryType.SqlQuery, AgHelpSelectionType.MultiSelect,,,,, SettingGroup_OtherMaterial,, "+SUPPORT")
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.PurchaseReturn, SettingFields.DocumentPrintReportFileName, "", AgDataType.Text, "50",,,,,,,, SettingGroup_OtherMaterial,, "+SUPPORT")
    End Sub
    Private Sub FConfigure_FinishedMaterialStockIssue(ClsObj As ClsMain)
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchInvoiceDirect", "", Ncat.StockIssue, "", "", "", "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCategory, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchInvoiceDirect", "", Ncat.StockIssue, "", "", "", "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension1, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchInvoiceDirect", "", Ncat.StockIssue, "", "", "", "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension2, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchInvoiceDirect", "", Ncat.StockIssue, "", "", "", "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension3, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchInvoiceDirect", "", Ncat.StockIssue, "", "", "", "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Size, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchInvoiceDirect", "", Ncat.StockIssue, "", "", "", "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Item, 0, 0, 1, "")


        mQry = " UPDATE Setting Set Value ='+" & ItemTypeCode.ManufacturingProduct & "+" & ItemTypeCode.TradingProduct & "'
                    WHERE NCat = '" & Ncat.StockIssue & "' 
                    AND SettingGroup Is Null
                    AND FieldName = '" & ClsMain.SettingFields.FilterInclude_ItemType & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)

        mQry = " UPDATE Setting Set Value ='+" & SubgroupType.Jobworker & "'
                    WHERE NCat = '" & Ncat.StockIssue & "' 
                    AND SettingGroup Is Null
                    AND FieldName = '" & ClsMain.SettingFields.FilterInclude_SubgroupType & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.StockIssue, SettingFields.DefaultSettingGroup, SettingGroup_FinishedMaterial, AgDataType.Text, "255", mSettingGroupFieldQry, AgHelpQueryType.SqlQuery, AgHelpSelectionType.SingleSelect,,,,, ,, "+SUPPORT")

        ClsObj.FUpdateSeed_EntryHeaderUISetting("FrmPurchInvoiceDirect", "", Ncat.StockIssue, "", "", "", "DglMain", FrmPurchInvoiceDirect_WithDimension.hcSettingGroup, 1, 0, 1, 1, "")
        ClsObj.FUpdateSeed_EntryHeaderUISetting("FrmPurchInvoiceDirect", "", Ncat.StockIssue, "", "", "", "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcBtnStockBalance, 0, 0, 0, 0, "")
    End Sub
    Private Sub FConfigure_RawMaterialStockIssue(ClsObj As ClsMain)
        'For Line
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockIssue, "Dgl1", FrmPurchInvoiceDirect_WithDimension.ColSNo, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockIssue, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Barcode, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockIssue, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCategory, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockIssue, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemGroup, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockIssue, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCode, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockIssue, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Item, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockIssue, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension1, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockIssue, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension2, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockIssue, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension3, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockIssue, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension4, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockIssue, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Size, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockIssue, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Specification, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockIssue, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1BaleNo, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockIssue, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1LotNo, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockIssue, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DocQty, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockIssue, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Unit, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockIssue, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Rate, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockIssue, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Amount, False,,, False,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockIssue, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Remark, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockIssue, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Godown, False,,,,,, SettingGroup_RawMaterial)

        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.StockIssue, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.ColSNo, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.StockIssue, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.Col1ItemCategory, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.StockIssue, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.Col1ItemGroup, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.StockIssue, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.Col1Item, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.StockIssue, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.Col1Dimension1, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.StockIssue, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.Col1Dimension2, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.StockIssue, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.Col1Dimension3, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.StockIssue, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.Col1Dimension4, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.StockIssue, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.Col1Size, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.StockIssue, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.Col1Specification, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.StockIssue, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.Col1Pcs, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.StockIssue, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.Col1Qty, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.StockIssue, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.Col1TotalQty, True,,,,,, SettingGroup_RawMaterial)

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.StockIssue, SettingFields.FilterInclude_ItemType, "+" & ItemTypeCode.RawProduct, AgDataType.Text, "255", mItemTypeFieldQry, AgHelpQueryType.SqlQuery, AgHelpSelectionType.MultiSelect,,,,, SettingGroup_RawMaterial,, "+SUPPORT")
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.StockIssue, SettingFields.DocumentPrintReportFileName, "", AgDataType.Text, "50",,,,,,,, SettingGroup_RawMaterial,, "+SUPPORT")
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.StockIssue, SettingFields.FilterInclude_SubgroupType, "+" & SubgroupType.Jobworker, AgDataType.Text, "50",,,,,,,, SettingGroup_RawMaterial,, "+SUPPORT")

        ClsObj.FUpdateSeed_EntryHeaderUISetting("FrmPurchInvoiceDirect", "", Ncat.StockIssue, "", "", SettingGroup_RawMaterial, "DglMain", FrmPurchInvoiceDirect_WithDimension.hcSettingGroup, 1, 0, 1, 1, "")
    End Sub
    Private Sub FConfigure_OtherMaterialStockIssue(ClsObj As ClsMain)
        'For Line
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockIssue, "Dgl1", FrmPurchInvoiceDirect_WithDimension.ColSNo, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockIssue, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Barcode, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockIssue, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCategory, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockIssue, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemGroup, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockIssue, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCode, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockIssue, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Item, True,,,,,, SettingGroup_OtherMaterial)
        If ClsMain.IsScopeOfWorkContains(IndustryType.CommonModules.Dimension1) Then
            ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockIssue, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension1, False,,,,,, SettingGroup_OtherMaterial)
        End If
        If ClsMain.IsScopeOfWorkContains(IndustryType.CommonModules.Dimension2) Then
            ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockIssue, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension2, False,,,,,, SettingGroup_OtherMaterial)
        End If
        If ClsMain.IsScopeOfWorkContains(IndustryType.CommonModules.Dimension3) Then
            ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockIssue, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension3, False,,,,,, SettingGroup_OtherMaterial)
        End If
        If ClsMain.IsScopeOfWorkContains(IndustryType.CommonModules.Dimension4) Then
            ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockIssue, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension4, False,,,,,, SettingGroup_OtherMaterial)
        End If
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockIssue, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Size, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockIssue, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Specification, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockIssue, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemState, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockIssue, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1BaleNo, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockIssue, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1LotNo, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockIssue, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DocQty, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockIssue, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Unit, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockIssue, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Pcs, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockIssue, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Rate, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockIssue, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Amount, False,,, False,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockIssue, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Remark, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockIssue, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Godown, False,,,,,, SettingGroup_OtherMaterial)

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.StockIssue, SettingFields.FilterInclude_ItemType, "+" & ItemTypeCode.OtherProduct, AgDataType.Text, "255", mItemTypeFieldQry, AgHelpQueryType.SqlQuery, AgHelpSelectionType.MultiSelect,,,,, SettingGroup_OtherMaterial,, "+SUPPORT")
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.StockIssue, SettingFields.DocumentPrintReportFileName, "", AgDataType.Text, "50",,,,,,,, SettingGroup_OtherMaterial,, "+SUPPORT")
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.StockIssue, SettingFields.FilterInclude_SubgroupType, "+" & SubgroupType.Jobworker, AgDataType.Text, "50",,,,,,,, SettingGroup_OtherMaterial,, "+SUPPORT")

        ClsObj.FUpdateSeed_EntryHeaderUISetting("FrmPurchInvoiceDirect", "", Ncat.StockIssue, "", "", SettingGroup_OtherMaterial, "DglMain", FrmPurchInvoiceDirect_WithDimension.hcSettingGroup, 1, 0, 1, 1, "")
    End Sub
    Private Sub FConfigure_RawAndOtherMaterialStockIssue(ClsObj As ClsMain)
        'For Line
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockIssue, "Dgl1", FrmPurchInvoiceDirect_WithDimension.ColSNo, True,,,,,, SettingGroup_RawAndOtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockIssue, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Barcode, False,,,,,, SettingGroup_RawAndOtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockIssue, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCategory, True,,,,,, SettingGroup_RawAndOtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockIssue, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemGroup, False,,,,,, SettingGroup_RawAndOtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockIssue, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCode, False,,,,,, SettingGroup_RawAndOtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockIssue, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Item, True,,,,,, SettingGroup_RawAndOtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockIssue, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension1, True,,,,,, SettingGroup_RawAndOtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockIssue, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension2, False,,,,,, SettingGroup_RawAndOtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockIssue, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension3, True,,,,,, SettingGroup_RawAndOtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockIssue, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension4, True,,,,,, SettingGroup_RawAndOtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockIssue, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Size, False,,,,,, SettingGroup_RawAndOtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockIssue, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Specification, False,,,,,, SettingGroup_RawAndOtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockIssue, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1BaleNo, False,,,,,, SettingGroup_RawAndOtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockIssue, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1LotNo, False,,,,,, SettingGroup_RawAndOtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockIssue, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DocQty, True,,,,,, SettingGroup_RawAndOtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockIssue, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Unit, True,,,,,, SettingGroup_RawAndOtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockIssue, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Rate, True,,,,,, SettingGroup_RawAndOtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockIssue, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Amount, True,,, False,,, SettingGroup_RawAndOtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockIssue, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Remark, True,,,,,, SettingGroup_RawAndOtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockIssue, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Godown, False,,,,,, SettingGroup_RawAndOtherMaterial)

        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.StockIssue, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.ColSNo, True,,,,,, SettingGroup_RawAndOtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.StockIssue, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.Col1ItemCategory, False,,,,,, SettingGroup_RawAndOtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.StockIssue, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.Col1ItemGroup, False,,,,,, SettingGroup_RawAndOtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.StockIssue, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.Col1Item, False,,,,,, SettingGroup_RawAndOtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.StockIssue, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.Col1Dimension1, False,,,,,, SettingGroup_RawAndOtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.StockIssue, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.Col1Dimension2, True,,,,,, SettingGroup_RawAndOtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.StockIssue, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.Col1Dimension3, False,,,,,, SettingGroup_RawAndOtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.StockIssue, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.Col1Dimension4, False,,,,,, SettingGroup_RawAndOtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.StockIssue, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.Col1Size, False,,,,,, SettingGroup_RawAndOtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.StockIssue, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.Col1Specification, False,,,,,, SettingGroup_RawAndOtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.StockIssue, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.Col1Pcs, True,,,,,, SettingGroup_RawAndOtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.StockIssue, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.Col1Qty, True,,,,,, SettingGroup_RawAndOtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.StockIssue, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.Col1TotalQty, True,,,,,, SettingGroup_RawAndOtherMaterial)



        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.StockIssue, SettingFields.FilterInclude_ItemType, "+" & ItemTypeCode.RawProduct & "+" & ItemTypeCode.OtherRawProduct, AgDataType.Text, "255", mItemTypeFieldQry, AgHelpQueryType.SqlQuery, AgHelpSelectionType.MultiSelect,,,,, SettingGroup_RawAndOtherMaterial,, "+SUPPORT")
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.StockIssue, SettingFields.DocumentPrintReportFileName, "", AgDataType.Text, "50",,,,,,,, SettingGroup_RawAndOtherMaterial,, "+SUPPORT")
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.StockIssue, SettingFields.FilterInclude_SubgroupType, "+" & SubgroupType.Jobworker, AgDataType.Text, "50",,,,,,,, SettingGroup_RawAndOtherMaterial,, "+SUPPORT")

        ClsObj.FUpdateSeed_EntryHeaderUISetting("FrmPurchInvoiceDirect", "", Ncat.StockIssue, "", "", SettingGroup_RawAndOtherMaterial, "DglMain", FrmPurchInvoiceDirect_WithDimension.hcSettingGroup, 1, 0, 1, 1, "")

        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.StockIssue, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcGodown, 1, 1,,,,, SettingGroup_RawAndOtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.StockIssue, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcRemarks, 1,,,,,, SettingGroup_RawAndOtherMaterial)
    End Sub
    Private Sub FConfigure_FinishedMaterialStockReceive(ClsObj As ClsMain)
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchInvoiceDirect", "", Ncat.StockReceive, "", "", "", "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCategory, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchInvoiceDirect", "", Ncat.StockReceive, "", "", "", "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension1, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchInvoiceDirect", "", Ncat.StockReceive, "", "", "", "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension2, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchInvoiceDirect", "", Ncat.StockReceive, "", "", "", "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension3, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchInvoiceDirect", "", Ncat.StockReceive, "", "", "", "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Size, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchInvoiceDirect", "", Ncat.StockReceive, "", "", "", "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Item, 0, 0, 1, "")


        mQry = " UPDATE Setting SET Value ='+" & ItemTypeCode.ManufacturingProduct & "+" & ItemTypeCode.TradingProduct & "'
                    WHERE NCat = '" & Ncat.StockReceive & "' 
                    AND SettingGroup Is Null 
                    AND FieldName = '" & ClsMain.SettingFields.FilterInclude_ItemType & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)

        mQry = " UPDATE Setting Set Value ='+" & SubgroupType.Jobworker & "'
                    WHERE NCat = '" & Ncat.StockReceive & "' 
                    AND SettingGroup Is Null
                    AND FieldName = '" & ClsMain.SettingFields.FilterInclude_SubgroupType & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)


        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.StockReceive, SettingFields.DefaultSettingGroup, SettingGroup_FinishedMaterial, AgDataType.Text, "255", mSettingGroupFieldQry, AgHelpQueryType.SqlQuery, AgHelpSelectionType.SingleSelect,,,,, ,, "+SUPPORT")

        ClsObj.FUpdateSeed_EntryHeaderUISetting("FrmPurchInvoiceDirect", "", Ncat.StockReceive, "", "", "", "DglMain", FrmPurchInvoiceDirect_WithDimension.hcSettingGroup, 1, 0, 1, 1, "")
    End Sub
    Private Sub FConfigure_RawMaterialStockReceive(ClsObj As ClsMain)
        'For Line
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.ColSNo, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Barcode, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCategory, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemGroup, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCode, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Item, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension1, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension2, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension3, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension4, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Size, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Specification, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1BaleNo, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1LotNo, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DocQty, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Unit, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Rate, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Amount, False,,, False,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Remark, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Godown, False,,,,,, SettingGroup_RawMaterial)

        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.StockReceive, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.ColSNo, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.StockReceive, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.Col1ItemCategory, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.StockReceive, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.Col1ItemGroup, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.StockReceive, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.Col1Item, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.StockReceive, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.Col1Dimension1, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.StockReceive, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.Col1Dimension2, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.StockReceive, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.Col1Dimension3, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.StockReceive, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.Col1Dimension4, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.StockReceive, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.Col1Size, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.StockReceive, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.Col1Specification, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.StockReceive, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.Col1Pcs, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.StockReceive, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.Col1Qty, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.StockReceive, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.Col1TotalQty, True,,,,,, SettingGroup_RawMaterial)

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.StockReceive, SettingFields.FilterInclude_ItemType, "+" & ItemTypeCode.RawProduct, AgDataType.Text, "255", mItemTypeFieldQry, AgHelpQueryType.SqlQuery, AgHelpSelectionType.MultiSelect,,,,, SettingGroup_RawMaterial,, "+SUPPORT")
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.StockReceive, SettingFields.DocumentPrintReportFileName, "", AgDataType.Text, "50",,,,,,,, SettingGroup_RawMaterial,, "+SUPPORT")
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.StockReceive, SettingFields.FilterInclude_SubgroupType, "+" & SubgroupType.Jobworker, AgDataType.Text, "50",,,,,,,, SettingGroup_RawMaterial,, "+SUPPORT")

        ClsObj.FUpdateSeed_EntryHeaderUISetting("FrmPurchInvoiceDirect", "", Ncat.StockReceive, "", "", SettingGroup_RawMaterial, "DglMain", FrmPurchInvoiceDirect_WithDimension.hcSettingGroup, 1, 0, 1, 1, "")
    End Sub
    Private Sub FConfigure_OtherMaterialStockReceive(ClsObj As ClsMain)
        'For Line
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.ColSNo, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Barcode, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCategory, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemGroup, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCode, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Item, True,,,,,, SettingGroup_OtherMaterial)
        If ClsMain.IsScopeOfWorkContains(IndustryType.CommonModules.Dimension1) Then
            ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension1, False,,,,,, SettingGroup_OtherMaterial)
        End If
        If ClsMain.IsScopeOfWorkContains(IndustryType.CommonModules.Dimension2) Then
            ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension2, False,,,,,, SettingGroup_OtherMaterial)
        End If
        If ClsMain.IsScopeOfWorkContains(IndustryType.CommonModules.Dimension3) Then
            ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension3, False,,,,,, SettingGroup_OtherMaterial)
        End If
        If ClsMain.IsScopeOfWorkContains(IndustryType.CommonModules.Dimension4) Then
            ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension4, False,,,,,, SettingGroup_OtherMaterial)
        End If
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Size, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Specification, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemState, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1BaleNo, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1LotNo, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DocQty, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Unit, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Pcs, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Rate, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Amount, False,,, False,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Remark, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Godown, False,,,,,, SettingGroup_OtherMaterial)

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.StockReceive, SettingFields.FilterInclude_ItemType, "+" & ItemTypeCode.OtherProduct, AgDataType.Text, "255", mItemTypeFieldQry, AgHelpQueryType.SqlQuery, AgHelpSelectionType.MultiSelect,,,,, SettingGroup_OtherMaterial,, "+SUPPORT")
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.StockReceive, SettingFields.DocumentPrintReportFileName, "", AgDataType.Text, "50",,,,,,,, SettingGroup_OtherMaterial,, "+SUPPORT")
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.StockReceive, SettingFields.FilterInclude_SubgroupType, "+" & SubgroupType.Jobworker, AgDataType.Text, "50",,,,,,,, SettingGroup_OtherMaterial,, "+SUPPORT")

        ClsObj.FUpdateSeed_EntryHeaderUISetting("FrmPurchInvoiceDirect", "", Ncat.StockReceive, "", "", SettingGroup_OtherMaterial, "DglMain", FrmPurchInvoiceDirect_WithDimension.hcSettingGroup, 1, 0, 1, 1, "")
    End Sub
    Private Sub FConfigure_JobOrder(ClsObj As ClsMain)
        ClsObj.FUpdateSeed_EntryHeaderUISetting("FrmPurchInvoiceDirect", "", Ncat.JobOrder, "", "", "", "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcBtnStockBalance, 0, 0, 0, 0, "")

        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchaseInvoiceStockIssRec", "", Ncat.JobOrder, "", "", "", "Dgl1", FrmPurchaseInvoiceStockIssRec.ColItemCategory, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchaseInvoiceStockIssRec", "", Ncat.JobOrder, "", "", "", "Dgl1", FrmPurchaseInvoiceStockIssRec.ColDimension1, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchaseInvoiceStockIssRec", "", Ncat.JobOrder, "", "", "", "Dgl1", FrmPurchaseInvoiceStockIssRec.ColDimension4, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchaseInvoiceStockIssRec", "", Ncat.JobOrder, "", "", "", "Dgl1", FrmPurchaseInvoiceStockIssRec.ColItem, 0, 0, 0, "")

        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchaseInvoiceStockIssRec", "", Ncat.JobOrder, "", "", "", "Dgl2", FrmPurchaseInvoiceStockIssRec.ColItem, 1, 0, 1, "")

        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchaseInvoiceStockIssRecDimension", "", Ncat.JobOrder, "", "", "", "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Item, 0, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchaseInvoiceStockIssRecDimension", "", Ncat.JobOrder, "", "", "", "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Dimension2, 1, 0, 1, "")


        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchInvoiceDirect", "", Ncat.JobOrder, "", "", "", "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCategory, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchInvoiceDirect", "", Ncat.JobOrder, "", "", "", "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension1, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchInvoiceDirect", "", Ncat.JobOrder, "", "", "", "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension2, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchInvoiceDirect", "", Ncat.JobOrder, "", "", "", "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension3, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchInvoiceDirect", "", Ncat.JobOrder, "", "", "", "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Size, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchInvoiceDirect", "", Ncat.JobOrder, "", "", "", "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Item, 0, 0, 1, "")

        mQry = " UPDATE Setting SET Value ='+" & ItemTypeCode.ManufacturingProduct & "+" & ItemTypeCode.TradingProduct & "'
                WHERE NCat = '" & Ncat.JobOrder & "' 
                And Process Is Null
                And SettingGroup Is Null
                AND FieldName = '" & ClsMain.SettingFields.FilterInclude_ItemType & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)

        mQry = " UPDATE Setting SET Value ='1'
                WHERE NCat = '" & Ncat.JobOrder & "' 
                And Process Is Null
                And SettingGroup Is Null
                AND FieldName = '" & ClsMain.SettingFields.AskVoucherTypeBeforeOpeningEntry & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)

        ClsObj.FUpdateSeed_EntryHeaderUISetting("FrmPurchaseInvoiceStockSelection", "", Ncat.JobOrder, "", "", "", "DglMain", FrmPurchaseInvoiceStockSelection.HcGodown, 0, 0, 0, 0, "")
        ClsObj.FUpdateSeed_EntryHeaderUISetting("FrmPurchaseInvoiceStockSelection", "", Ncat.JobOrder, "", "", "", "DglMain", FrmPurchaseInvoiceStockSelection.HcFromProcess, 0, 0, 1, 0, "")
        ClsObj.FUpdateSeed_EntryHeaderUISetting("FrmPurchaseInvoiceStockSelection", "", Ncat.JobOrder, "", "", "", "DglMain", FrmPurchaseInvoiceStockSelection.HcItemCategory, 1, 0, 1, 0, "")
        ClsObj.FUpdateSeed_EntryHeaderUISetting("FrmPurchaseInvoiceStockSelection", "", Ncat.JobOrder, "", "", "", "DglMain", FrmPurchaseInvoiceStockSelection.HcItemGroup, 0, 0, 1, 0, "")
        ClsObj.FUpdateSeed_EntryHeaderUISetting("FrmPurchaseInvoiceStockSelection", "", Ncat.JobOrder, "", "", "", "DglMain", FrmPurchaseInvoiceStockSelection.HcItem, 0, 0, 1, 0, "")
        ClsObj.FUpdateSeed_EntryHeaderUISetting("FrmPurchaseInvoiceStockSelection", "", Ncat.JobOrder, "", "", "", "DglMain", FrmPurchaseInvoiceStockSelection.HcDimension1, 1, 0, 1, 0, "")
        ClsObj.FUpdateSeed_EntryHeaderUISetting("FrmPurchaseInvoiceStockSelection", "", Ncat.JobOrder, "", "", "", "DglMain", FrmPurchaseInvoiceStockSelection.HcDimension2, 1, 0, 1, 0, "")
        ClsObj.FUpdateSeed_EntryHeaderUISetting("FrmPurchaseInvoiceStockSelection", "", Ncat.JobOrder, "", "", "", "DglMain", FrmPurchaseInvoiceStockSelection.HcDimension3, 1, 0, 1, 0, "")
        ClsObj.FUpdateSeed_EntryHeaderUISetting("FrmPurchaseInvoiceStockSelection", "", Ncat.JobOrder, "", "", "", "DglMain", FrmPurchaseInvoiceStockSelection.HcDimension4, 0, 0, 1, 0, "")
        ClsObj.FUpdateSeed_EntryHeaderUISetting("FrmPurchaseInvoiceStockSelection", "", Ncat.JobOrder, "", "", "", "DglMain", FrmPurchaseInvoiceStockSelection.HcSize, 1, 0, 1, 0, "")
        ClsObj.FUpdateSeed_EntryHeaderUISetting("FrmPurchaseInvoiceStockSelection", "", Ncat.JobOrder, "", "", "", "DglMain", FrmPurchaseInvoiceStockSelection.HcBtnStockBalance, 1, 0, 1, 0, "")

        mQry = " UPDATE Voucher_Type Set Status = 'InActive' Where V_Type = '" & Ncat.JobOrder & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
    End Sub
    Private Sub FConfigure_JobReceive(ClsObj As ClsMain)
        ClsObj.FUpdateSeed_EntryHeaderUISetting("FrmPurchInvoiceDirect", "", Ncat.JobReceive, "", "", "", "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcBtnPendingPurchOrder, 1, 0, 0, 0, "")

        ClsObj.FUpdateSeed_EntryHeaderUISetting("FrmPurchaseInvoiceStockIssRec", "", Ncat.JobReceive, "", "", "", "DglMain", FrmPurchaseInvoiceStockIssRec.hcStockIssRecNos, 0, 0, 0, 0, "")
        'ClsObj.FUpdateSeed_EntryHeaderUISetting("FrmPurchaseInvoiceStockIssRec", "", Ncat.JobReceive, "", "", "", "DglMain", FrmPurchaseInvoiceStockIssRec.hcBtnStandardConsumption, 1, 0, 1, 0, "")

        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchInvoiceDirect", "", Ncat.JobReceive, "", "", "", "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCategory, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchInvoiceDirect", "", Ncat.JobReceive, "", "", "", "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension1, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchInvoiceDirect", "", Ncat.JobReceive, "", "", "", "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension2, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchInvoiceDirect", "", Ncat.JobReceive, "", "", "", "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension3, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchInvoiceDirect", "", Ncat.JobReceive, "", "", "", "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Size, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchInvoiceDirect", "", Ncat.JobReceive, "", "", "", "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Item, 0, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchInvoiceDirect", "", Ncat.JobReceive, "", "", "", "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ReferenceDocIdDate, 1, 0, 0, "Order Date")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchInvoiceDirect", "", Ncat.JobReceive, "", "", "", "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ReferenceDocIdBalanceQty, 1, 0, 0, "Balance Qty")

        ClsObj.FUpdateSeed_EntryHeaderUISetting("FrmPurchaseInvoiceStockSelection", "", Ncat.JobReceive, "", "", "", "DglMain", FrmPurchaseInvoiceStockSelection.HcGodown, 0, 0, 0, 0, "")
        ClsObj.FUpdateSeed_EntryHeaderUISetting("FrmPurchaseInvoiceStockSelection", "", Ncat.JobReceive, "", "", "", "DglMain", FrmPurchaseInvoiceStockSelection.HcFromProcess, 0, 0, 1, 0, "")
        ClsObj.FUpdateSeed_EntryHeaderUISetting("FrmPurchaseInvoiceStockSelection", "", Ncat.JobReceive, "", "", "", "DglMain", FrmPurchaseInvoiceStockSelection.HcItemCategory, 1, 0, 1, 0, "")
        ClsObj.FUpdateSeed_EntryHeaderUISetting("FrmPurchaseInvoiceStockSelection", "", Ncat.JobReceive, "", "", "", "DglMain", FrmPurchaseInvoiceStockSelection.HcItemGroup, 0, 0, 1, 0, "")
        ClsObj.FUpdateSeed_EntryHeaderUISetting("FrmPurchaseInvoiceStockSelection", "", Ncat.JobReceive, "", "", "", "DglMain", FrmPurchaseInvoiceStockSelection.HcItem, 0, 0, 1, 0, "")
        ClsObj.FUpdateSeed_EntryHeaderUISetting("FrmPurchaseInvoiceStockSelection", "", Ncat.JobReceive, "", "", "", "DglMain", FrmPurchaseInvoiceStockSelection.HcDimension1, 1, 0, 1, 0, "")
        ClsObj.FUpdateSeed_EntryHeaderUISetting("FrmPurchaseInvoiceStockSelection", "", Ncat.JobReceive, "", "", "", "DglMain", FrmPurchaseInvoiceStockSelection.HcDimension2, 1, 0, 1, 0, "")
        ClsObj.FUpdateSeed_EntryHeaderUISetting("FrmPurchaseInvoiceStockSelection", "", Ncat.JobReceive, "", "", "", "DglMain", FrmPurchaseInvoiceStockSelection.HcDimension3, 1, 0, 1, 0, "")
        ClsObj.FUpdateSeed_EntryHeaderUISetting("FrmPurchaseInvoiceStockSelection", "", Ncat.JobReceive, "", "", "", "DglMain", FrmPurchaseInvoiceStockSelection.HcDimension4, 0, 0, 1, 0, "")
        ClsObj.FUpdateSeed_EntryHeaderUISetting("FrmPurchaseInvoiceStockSelection", "", Ncat.JobReceive, "", "", "", "DglMain", FrmPurchaseInvoiceStockSelection.HcSize, 1, 0, 1, 0, "")
        ClsObj.FUpdateSeed_EntryHeaderUISetting("FrmPurchaseInvoiceStockSelection", "", Ncat.JobReceive, "", "", "", "DglMain", FrmPurchaseInvoiceStockSelection.HcBtnStockBalance, 1, 0, 1, 0, "")


        mQry = " UPDATE Setting SET Value ='+" & ItemTypeCode.ManufacturingProduct & "+" & ItemTypeCode.TradingProduct & "'
                WHERE NCat = '" & Ncat.JobReceive & "' 
                And Process Is Null
                And SettingGroup Is Null
                AND FieldName = '" & ClsMain.SettingFields.FilterInclude_ItemType & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)

        mQry = " UPDATE Setting SET Value ='1'
                WHERE NCat = '" & Ncat.JobReceive & "' 
                And Process Is Null
                And SettingGroup Is Null
                AND FieldName = '" & ClsMain.SettingFields.AskVoucherTypeBeforeOpeningEntry & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)

        mQry = " UPDATE Voucher_Type Set Status = 'InActive' Where V_Type = '" & Ncat.JobReceive & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
    End Sub
    Private Sub FConfigure_JobInvoice(ClsObj As ClsMain)
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchInvoiceDirect", "", Ncat.JobInvoice, "", "", "", "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCategory, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchInvoiceDirect", "", Ncat.JobInvoice, "", "", "", "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension1, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchInvoiceDirect", "", Ncat.JobInvoice, "", "", "", "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension2, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchInvoiceDirect", "", Ncat.JobInvoice, "", "", "", "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension3, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchInvoiceDirect", "", Ncat.JobInvoice, "", "", "", "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Size, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchInvoiceDirect", "", Ncat.JobInvoice, "", "", "", "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Item, 0, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchInvoiceDirect", "", Ncat.JobInvoice, "", "", "", "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ReferenceDocIdDate, 1, 0, 0, "Receive Date")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchInvoiceDirect", "", Ncat.JobInvoice, "", "", "", "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ReferenceDocIdBalanceQty, 1, 0, 0, "Balance Qty")

        ClsObj.FUpdateSeed_EntryHeaderUISetting("FrmPurchInvoiceDirect", "", Ncat.JobInvoice, "", "", "", "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcPaidAmount, 1, 0, 0, 0, "")

        mQry = " UPDATE Setting SET Value ='+" & ItemTypeCode.ManufacturingProduct & "+" & ItemTypeCode.TradingProduct & "'
                WHERE NCat = '" & Ncat.JobInvoice & "' 
                And Process Is Null
                And SettingGroup Is Null
                AND FieldName = '" & ClsMain.SettingFields.FilterInclude_ItemType & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)

        mQry = " UPDATE Setting SET Value ='1'
                WHERE NCat = '" & Ncat.JobInvoice & "' 
                And Process Is Null
                And SettingGroup Is Null
                AND FieldName = '" & ClsMain.SettingFields.AskVoucherTypeBeforeOpeningEntry & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)

        mQry = " UPDATE Voucher_Type Set Status = 'InActive' Where V_Type = '" & Ncat.JobInvoice & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
    End Sub
    Private Sub FConfigure_CuttingOrder(ClsObj As ClsMain)
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.JobOrder, ClsMain.SettingFields.PostConsumptionYn, "1", ClsMain.AgDataType.YesNo, "50",,,,,,, Process_Cutting, ,, "+SUPPORT")
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.JobOrder, SettingFields.PostInStockYn, "0", AgDataType.YesNo, "50",,,,,,, Process_Cutting)
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.JobOrder, SettingFields.PostInStockProcessYn, "1", AgDataType.YesNo, "50",,,,,,, Process_Cutting)


        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcVendorDocNo, 1,,,,, Process_Cutting, "")
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcVendorDocDate, 1,,,,, Process_Cutting, "")
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcDeliveryDate, 1,,,,, Process_Cutting, "")
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcRateType, 1,,,,, Process_Cutting, "")
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcBtnMaterialIssue, 1,,,,, Process_Cutting, "")

        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.ColSNo, True, True,,,, Process_Cutting)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCategory, True,,,,, Process_Cutting)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemGroup, False,,,,, Process_Cutting)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCode, False,,,,, Process_Cutting)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Item, False,,,,, Process_Cutting)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension1, True,,,,, Process_Cutting)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension2, True,,,,, Process_Cutting)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension3, True,,,,, Process_Cutting)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Size, True,,,,, Process_Cutting)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1RawMaterial, True,, "Fabric Width",,, Process_Cutting)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Specification, False,,,,, Process_Cutting)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1SalesTaxGroup, False,,,,, Process_Cutting)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1BaleNo, False,,,,, Process_Cutting)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1LotNo, False,,,,, Process_Cutting)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DocQty, True,,,,, Process_Cutting)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Unit, True,,,,, Process_Cutting)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Rate, True,,,,, Process_Cutting)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DiscountPer, False,,,,, Process_Cutting)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DiscountAmount, False,,,,, Process_Cutting)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionalDiscountPer, False,,,,, Process_Cutting)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionalDiscountAmount, False,,,,, Process_Cutting)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionPer, False,,,,, Process_Cutting)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionAmount, False,,,,, Process_Cutting)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Amount, True,,, False,, Process_Cutting)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Remark, True,,,,, Process_Cutting)

        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl4", FrmPurchInvoiceDirect_WithDimension.ColSNo, True,,,,, Process_Cutting)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl4", FrmPurchInvoiceDirect_WithDimension.Col4ItemCategory, True,,,,, Process_Cutting)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl4", FrmPurchInvoiceDirect_WithDimension.Col4Item, True,,,,, Process_Cutting)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl4", FrmPurchInvoiceDirect_WithDimension.Col4Dimension1, True,,,,, Process_Cutting)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl4", FrmPurchInvoiceDirect_WithDimension.Col4Dimension2, True,,,,, Process_Cutting)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl4", FrmPurchInvoiceDirect_WithDimension.Col4Dimension3, True,,,,, Process_Cutting)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl4", FrmPurchInvoiceDirect_WithDimension.Col4Dimension4, True,,,,, Process_Cutting)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl4", FrmPurchInvoiceDirect_WithDimension.Col4Qty, True,,,,, Process_Cutting)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl4", FrmPurchInvoiceDirect_WithDimension.Col4CurrentStock, True,,,,, Process_Cutting)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl4", FrmPurchInvoiceDirect_WithDimension.Col4ConsiderInIssueYN, True,,,,, Process_Cutting)


        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", "", SettingFields.FilterInclude_Process, "+" + Process_Cutting, AgDataType.Text, "255", mProcessFieldQry, AgHelpQueryType.SqlQuery, AgHelpSelectionType.MultiSelect,,, Voucher_Type_CuttingOrder,,,, "+SUPPORT")

        mQry = " UPDATE Voucher_Type Set Status = 'InActive' Where V_Type In ('" & Voucher_Type_CuttingOrder & "')"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
    End Sub
    Private Sub FConfigure_CuttingReceive(ClsObj As ClsMain)
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.JobReceive, SettingFields.PostConsumptionInStockYn, "1", AgDataType.YesNo, "50",,,,,,, Process_Cutting)
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.JobReceive, SettingFields.PostConsumptionInStockProcessYn, "0", AgDataType.YesNo, "50",,,,,,, Process_Cutting)

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.JobReceive, SettingFields.PostInStockProcessYn, "0", AgDataType.YesNo, "50",,,,,,, Process_Cutting)

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.JobReceive, ClsMain.SettingFields.PostConsumptionYn, "1", ClsMain.AgDataType.YesNo, "50",,,,,,, Process_Cutting, ,, "+SUPPORT")

        'ClsObj.FUpdateSeed_EntryHeaderUISetting("FrmPurchInvoiceDirect", "", Ncat.JobReceive, "", Process_Cutting, "", "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcBtnPendingPurchOrder, 0, 0, 0, 0, "")

        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcGodown, 1, 1,,,, Process_Cutting)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcRemarks, 1,,,,, Process_Cutting)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcBtnPendingPurchOrder, 0,,,,, Process_Cutting)



        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.ColSNo, True,,,,, Process_Cutting)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Barcode, False,,,,, Process_Cutting)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCategory, True,,,,, Process_Cutting)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemGroup, False,,,,, Process_Cutting)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCode, False,,,,, Process_Cutting)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Item, False,,,,, Process_Cutting)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension1, True,,,,, Process_Cutting)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension2, True,,,,, Process_Cutting)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension3, True,,,,, Process_Cutting)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension4, False,,,,, Process_Cutting)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Size, True,,,,, Process_Cutting)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1RawMaterial, True,, "Fabric Width",,, Process_Cutting)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1RawMaterialConsumptionQty, True,, "Fabric Consumption Qty",,, Process_Cutting)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Specification, False,,,,, Process_Cutting)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemState, False,,,,, Process_Cutting)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1BaleNo, False,,,,, Process_Cutting)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1LotNo, False,,,,, Process_Cutting)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Godown, False,,,,, Process_Cutting)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ReferenceNo, False,, "Order No.",,, Process_Cutting)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ReferenceDocIdDate, False,, "Order Date",,, Process_Cutting)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ReferenceDocIdBalanceQty, False,, "Balance Qty",,, Process_Cutting)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DocQty, True,,,,, Process_Cutting)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1LossQty, False,,,,, Process_Cutting)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Qty, False,,,,, Process_Cutting)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Unit, True, False,, False,, Process_Cutting)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Pcs, False,,,,, Process_Cutting)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DealQty, False,,,,, Process_Cutting)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DealUnit, False,,,,, Process_Cutting)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Rate, False,,,,, Process_Cutting)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Amount, False,,, False,, Process_Cutting)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Remark, True,,,,, Process_Cutting)

        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl4", FrmPurchInvoiceDirect_WithDimension.ColSNo, True,,,,, Process_Cutting)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl4", FrmPurchInvoiceDirect_WithDimension.Col4ItemCategory, True,,,,, Process_Cutting)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl4", FrmPurchInvoiceDirect_WithDimension.Col4Item, True,,,,, Process_Cutting)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl4", FrmPurchInvoiceDirect_WithDimension.Col4Dimension1, True,,,,, Process_Cutting)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl4", FrmPurchInvoiceDirect_WithDimension.Col4Dimension2, True,,,,, Process_Cutting)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl4", FrmPurchInvoiceDirect_WithDimension.Col4Dimension3, True,,,,, Process_Cutting)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl4", FrmPurchInvoiceDirect_WithDimension.Col4Dimension4, True,,,,, Process_Cutting)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl4", FrmPurchInvoiceDirect_WithDimension.Col4Qty, True,,,,, Process_Cutting)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl4", FrmPurchInvoiceDirect_WithDimension.Col4CurrentStock, False,,,,, Process_Cutting)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl4", FrmPurchInvoiceDirect_WithDimension.Col4CurrentStockProcess, True,,,,, Process_Cutting)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl4", FrmPurchInvoiceDirect_WithDimension.Col4Wastage, True,,,,, Process_Cutting)

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", "", SettingFields.FilterInclude_Process, "+" + Process_Cutting, AgDataType.Text, "255", mProcessFieldQry, AgHelpQueryType.SqlQuery, AgHelpSelectionType.MultiSelect,,, Voucher_Type_CuttingReceive,,,, "+SUPPORT")
    End Sub
    Private Sub FConfigure_CuttingInvoice(ClsObj As ClsMain)
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", "", SettingFields.FilterInclude_Process, "+" + Process_Cutting, AgDataType.Text, "255", mProcessFieldQry, AgHelpQueryType.SqlQuery, AgHelpSelectionType.MultiSelect,,, Voucher_Type_CuttingInvoice,,,, "+SUPPORT")
    End Sub
    Private Sub FConfigure_Bom(ClsObj As ClsMain)
        Dim mMaxId As String = ""
        If AgL.VNull(AgL.Dman_Execute("Select Count(*) From ItemBomPattern Where Item Is Null And Process Is Null", AgL.GCn).ExecuteScalar()) = 0 Then
            mMaxId = AgL.GetMaxId("ItemBomPattern", "Code", AgL.GcnMain, AgL.PubDivCode, AgL.PubSiteCode, 8, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)
            mQry = "INSERT INTO ItemBomPattern (Code, Item, Sr, Process, Pattern)
                    VALUES ('" & mMaxId & "', NULL, 1, NULL, 'Item Category,Size,Raw Material')"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        End If
    End Sub
    Private Sub FConfigure_CuttingAndStitchingOrder(ClsObj As ClsMain)
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.JobOrder, ClsMain.SettingFields.PostConsumptionYn, "1", ClsMain.AgDataType.YesNo, "50",,,,,,, Process_CuttingAndStitching, ,, "+SUPPORT")
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.JobOrder, SettingFields.PostInStockYn, "0", AgDataType.YesNo, "50",,,,,,, Process_CuttingAndStitching)
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.JobOrder, SettingFields.PostInStockProcessYn, "1", AgDataType.YesNo, "50",,,,,,, Process_CuttingAndStitching)


        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcVendorDocNo, 1,,,,, Process_CuttingAndStitching, "")
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcVendorDocDate, 1,,,,, Process_CuttingAndStitching, "")
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcDeliveryDate, 1,,,,, Process_CuttingAndStitching, "")
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcBtnMaterialIssue, 1,,,,, Process_CuttingAndStitching, "")

        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.ColSNo, True, True,,,, Process_CuttingAndStitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCategory, True,,,,, Process_CuttingAndStitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemGroup, False,,,,, Process_CuttingAndStitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCode, False,,,,, Process_CuttingAndStitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Item, False,,,,, Process_CuttingAndStitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension1, True,,,,, Process_CuttingAndStitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension2, True,,,,, Process_CuttingAndStitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension3, True,,,,, Process_CuttingAndStitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Size, True,,,,, Process_CuttingAndStitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1RawMaterial, True,, "Fabric Width",,, Process_CuttingAndStitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Specification, False,,,,, Process_CuttingAndStitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1SalesTaxGroup, False,,,,, Process_CuttingAndStitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1BaleNo, False,,,,, Process_CuttingAndStitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1LotNo, False,,,,, Process_CuttingAndStitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DocQty, True,,,,, Process_CuttingAndStitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Unit, True,,,,, Process_CuttingAndStitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Rate, True,,,,, Process_CuttingAndStitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DiscountPer, False,,,,, Process_CuttingAndStitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DiscountAmount, False,,,,, Process_CuttingAndStitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionalDiscountPer, False,,,,, Process_CuttingAndStitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionalDiscountAmount, False,,,,, Process_CuttingAndStitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionPer, False,,,,, Process_CuttingAndStitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionAmount, False,,,,, Process_CuttingAndStitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Amount, True,,, False,, Process_CuttingAndStitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Remark, True,,,,, Process_CuttingAndStitching)

        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl4", FrmPurchInvoiceDirect_WithDimension.ColSNo, True,,,,, Process_CuttingAndStitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl4", FrmPurchInvoiceDirect_WithDimension.Col4ItemCategory, True,,,,, Process_CuttingAndStitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl4", FrmPurchInvoiceDirect_WithDimension.Col4Item, True,,,,, Process_CuttingAndStitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl4", FrmPurchInvoiceDirect_WithDimension.Col4Dimension1, True,,,,, Process_CuttingAndStitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl4", FrmPurchInvoiceDirect_WithDimension.Col4Dimension2, True,,,,, Process_CuttingAndStitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl4", FrmPurchInvoiceDirect_WithDimension.Col4Dimension3, True,,,,, Process_CuttingAndStitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl4", FrmPurchInvoiceDirect_WithDimension.Col4Dimension4, True,,,,, Process_CuttingAndStitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl4", FrmPurchInvoiceDirect_WithDimension.Col4Qty, True,,,,, Process_CuttingAndStitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl4", FrmPurchInvoiceDirect_WithDimension.Col4CurrentStock, True,,,,, Process_CuttingAndStitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl4", FrmPurchInvoiceDirect_WithDimension.Col4ConsiderInIssueYN, True,,,,, Process_CuttingAndStitching)

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", "", SettingFields.FilterInclude_Process, "+" + Process_CuttingAndStitching, AgDataType.Text, "255", mProcessFieldQry, AgHelpQueryType.SqlQuery, AgHelpSelectionType.MultiSelect,,, Voucher_Type_CuttingAndStitchingOrder,,,, "+SUPPORT")
    End Sub
    Private Sub FConfigure_CuttingAndStitchingReceive(ClsObj As ClsMain)
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.JobReceive, SettingFields.PostConsumptionInStockYn, "0", AgDataType.YesNo, "50",,,,,,, Process_CuttingAndStitching)
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.JobReceive, SettingFields.PostConsumptionInStockProcessYn, "1", AgDataType.YesNo, "50",,,,,,, Process_CuttingAndStitching)

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.JobReceive, ClsMain.SettingFields.PostConsumptionYn, "1", ClsMain.AgDataType.YesNo, "50",,,,,,, Process_CuttingAndStitching, ,, "+SUPPORT")

        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.ColSNo, True,,,,, Process_CuttingAndStitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Barcode, False,,,,, Process_CuttingAndStitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCategory, True,,,,, Process_CuttingAndStitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemGroup, False,,,,, Process_CuttingAndStitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCode, False,,,,, Process_CuttingAndStitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Item, False,,,,, Process_CuttingAndStitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension1, True,,,,, Process_CuttingAndStitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension2, True,,,,, Process_CuttingAndStitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension3, True,,,,, Process_CuttingAndStitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension4, False,,,,, Process_CuttingAndStitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Size, True,,,,, Process_CuttingAndStitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1RawMaterial, True,, "Fabric Width",,, Process_CuttingAndStitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1RawMaterialConsumptionQty, True,, "Fabric Consumption Qty",,, Process_CuttingAndStitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Specification, False,,,,, Process_CuttingAndStitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemState, False,,,,, Process_CuttingAndStitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1BaleNo, False,,,,, Process_CuttingAndStitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1LotNo, False,,,,, Process_CuttingAndStitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Godown, False,,,,, Process_CuttingAndStitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ReferenceNo, True,, "Order No.",,, Process_CuttingAndStitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ReferenceDocIdDate, True,, "Order Date",,, Process_CuttingAndStitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ReferenceDocIdBalanceQty, True,, "Balance Qty",,, Process_CuttingAndStitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DocQty, True,,,,, Process_CuttingAndStitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1LossQty, False,,,,, Process_CuttingAndStitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Qty, False,,,,, Process_CuttingAndStitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Unit, True, False,, False,, Process_CuttingAndStitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Pcs, False,,,,, Process_CuttingAndStitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DealQty, False,,,,, Process_CuttingAndStitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DealUnit, False,,,,, Process_CuttingAndStitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Rate, False,,,,, Process_CuttingAndStitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Amount, False,,, False,, Process_CuttingAndStitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Remark, True,,,,, Process_CuttingAndStitching)



        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl4", FrmPurchInvoiceDirect_WithDimension.ColSNo, True,,,,, Process_CuttingAndStitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl4", FrmPurchInvoiceDirect_WithDimension.Col4ItemCategory, True,,,,, Process_CuttingAndStitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl4", FrmPurchInvoiceDirect_WithDimension.Col4Item, True,,,,, Process_CuttingAndStitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl4", FrmPurchInvoiceDirect_WithDimension.Col4Dimension1, True,,,,, Process_CuttingAndStitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl4", FrmPurchInvoiceDirect_WithDimension.Col4Dimension2, True,,,,, Process_CuttingAndStitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl4", FrmPurchInvoiceDirect_WithDimension.Col4Dimension3, True,,,,, Process_CuttingAndStitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl4", FrmPurchInvoiceDirect_WithDimension.Col4Dimension4, True,,,,, Process_CuttingAndStitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl4", FrmPurchInvoiceDirect_WithDimension.Col4Qty, True,,,,, Process_CuttingAndStitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl4", FrmPurchInvoiceDirect_WithDimension.Col4CurrentStockProcess, True,,,,, Process_CuttingAndStitching)

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", "", SettingFields.FilterInclude_Process, "+" + Process_CuttingAndStitching, AgDataType.Text, "255", mProcessFieldQry, AgHelpQueryType.SqlQuery, AgHelpSelectionType.MultiSelect,,, Voucher_Type_CuttingAndStitchingReceive,,,, "+SUPPORT")
    End Sub
    Private Sub FConfigure_CuttingAndStitchingInvoice(ClsObj As ClsMain)
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", "", SettingFields.FilterInclude_Process, "+" + Process_CuttingAndStitching, AgDataType.Text, "255", mProcessFieldQry, AgHelpQueryType.SqlQuery, AgHelpSelectionType.MultiSelect,,, Voucher_Type_CuttingAndStitchingInvoice,,,, "+SUPPORT")
    End Sub
    Private Sub FConfigure_CuttingAndStitchingAndPackingOrder(ClsObj As ClsMain)
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.JobOrder, ClsMain.SettingFields.PostConsumptionYn, "1", ClsMain.AgDataType.YesNo, "50",,,,,,, Process_CuttingAndStitchingAndPacking, ,, "+SUPPORT")
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.JobOrder, SettingFields.PostInStockYn, "0", AgDataType.YesNo, "50",,,,,,, Process_CuttingAndStitchingAndPacking)
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.JobOrder, SettingFields.PostInStockProcessYn, "1", AgDataType.YesNo, "50",,,,,,, Process_CuttingAndStitchingAndPacking)


        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcVendorDocNo, 1,,,,, Process_CuttingAndStitchingAndPacking, "")
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcVendorDocDate, 1,,,,, Process_CuttingAndStitchingAndPacking, "")
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcDeliveryDate, 1,,,,, Process_CuttingAndStitchingAndPacking, "")
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcBtnMaterialIssue, 1,,,,, Process_CuttingAndStitchingAndPacking, "")

        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.ColSNo, True, True,,,, Process_CuttingAndStitchingAndPacking)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCategory, True,,,,, Process_CuttingAndStitchingAndPacking)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemGroup, False,,,,, Process_CuttingAndStitchingAndPacking)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCode, False,,,,, Process_CuttingAndStitchingAndPacking)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Item, False,,,,, Process_CuttingAndStitchingAndPacking)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension1, True,,,,, Process_CuttingAndStitchingAndPacking)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension2, True,,,,, Process_CuttingAndStitchingAndPacking)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension3, True,,,,, Process_CuttingAndStitchingAndPacking)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Size, True,,,,, Process_CuttingAndStitchingAndPacking)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1RawMaterial, True,, "Fabric Width",,, Process_CuttingAndStitchingAndPacking)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Specification, False,,,,, Process_CuttingAndStitchingAndPacking)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1SalesTaxGroup, False,,,,, Process_CuttingAndStitchingAndPacking)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1BaleNo, False,,,,, Process_CuttingAndStitchingAndPacking)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1LotNo, False,,,,, Process_CuttingAndStitchingAndPacking)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DocQty, True,,,,, Process_CuttingAndStitchingAndPacking)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Unit, True,,,,, Process_CuttingAndStitchingAndPacking)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Rate, True,,,,, Process_CuttingAndStitchingAndPacking)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DiscountPer, False,,,,, Process_CuttingAndStitchingAndPacking)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DiscountAmount, False,,,,, Process_CuttingAndStitchingAndPacking)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionalDiscountPer, False,,,,, Process_CuttingAndStitchingAndPacking)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionalDiscountAmount, False,,,,, Process_CuttingAndStitchingAndPacking)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionPer, False,,,,, Process_CuttingAndStitchingAndPacking)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionAmount, False,,,,, Process_CuttingAndStitchingAndPacking)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Amount, True,,, False,, Process_CuttingAndStitchingAndPacking)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Remark, True,,,,, Process_CuttingAndStitchingAndPacking)

        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl4", FrmPurchInvoiceDirect_WithDimension.ColSNo, True,,,,, Process_CuttingAndStitchingAndPacking)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl4", FrmPurchInvoiceDirect_WithDimension.Col4ItemCategory, True,,,,, Process_CuttingAndStitchingAndPacking)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl4", FrmPurchInvoiceDirect_WithDimension.Col4Item, True,,,,, Process_CuttingAndStitchingAndPacking)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl4", FrmPurchInvoiceDirect_WithDimension.Col4Dimension1, True,,,,, Process_CuttingAndStitchingAndPacking)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl4", FrmPurchInvoiceDirect_WithDimension.Col4Dimension2, True,,,,, Process_CuttingAndStitchingAndPacking)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl4", FrmPurchInvoiceDirect_WithDimension.Col4Dimension3, True,,,,, Process_CuttingAndStitchingAndPacking)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl4", FrmPurchInvoiceDirect_WithDimension.Col4Dimension4, True,,,,, Process_CuttingAndStitchingAndPacking)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl4", FrmPurchInvoiceDirect_WithDimension.Col4Qty, True,,,,, Process_CuttingAndStitchingAndPacking)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl4", FrmPurchInvoiceDirect_WithDimension.Col4CurrentStock, True,,,,, Process_CuttingAndStitchingAndPacking)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl4", FrmPurchInvoiceDirect_WithDimension.Col4ConsiderInIssueYN, True,,,,, Process_CuttingAndStitchingAndPacking)

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", "", SettingFields.FilterInclude_Process, "+" + Process_CuttingAndStitchingAndPacking, AgDataType.Text, "255", mProcessFieldQry, AgHelpQueryType.SqlQuery, AgHelpSelectionType.MultiSelect,,, Voucher_Type_CuttingAndStitchingAndPackingOrder,,,, "+SUPPORT")
    End Sub
    Private Sub FConfigure_CuttingAndStitchingAndPackingReceive(ClsObj As ClsMain)
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.JobReceive, ClsMain.SettingFields.PostConsumptionYn, "1", ClsMain.AgDataType.YesNo, "50",,,,,,, Process_CuttingAndStitchingAndPacking, ,, "+SUPPORT")

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.JobReceive, SettingFields.PostConsumptionInStockYn, "0", AgDataType.YesNo, "50",,,,,,, Process_CuttingAndStitchingAndPacking)
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.JobReceive, SettingFields.PostConsumptionInStockProcessYn, "1", AgDataType.YesNo, "50",,,,,,, Process_CuttingAndStitchingAndPacking)

        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.ColSNo, True,,,,, Process_CuttingAndStitchingAndPacking)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Barcode, False,,,,, Process_CuttingAndStitchingAndPacking)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCategory, True,,,,, Process_CuttingAndStitchingAndPacking)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemGroup, False,,,,, Process_CuttingAndStitchingAndPacking)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCode, False,,,,, Process_CuttingAndStitchingAndPacking)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Item, False,,,,, Process_CuttingAndStitchingAndPacking)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension1, True,,,,, Process_CuttingAndStitchingAndPacking)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension2, True,,,,, Process_CuttingAndStitchingAndPacking)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension3, True,,,,, Process_CuttingAndStitchingAndPacking)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension4, False,,,,, Process_CuttingAndStitchingAndPacking)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Size, True,,,,, Process_CuttingAndStitchingAndPacking)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1RawMaterial, True,, "Fabric Width",,, Process_CuttingAndStitchingAndPacking)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1RawMaterialConsumptionQty, True,, "Fabric Consumption Qty",,, Process_CuttingAndStitchingAndPacking)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Specification, False,,,,, Process_CuttingAndStitchingAndPacking)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemState, False,,,,, Process_CuttingAndStitchingAndPacking)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1BaleNo, False,,,,, Process_CuttingAndStitchingAndPacking)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1LotNo, False,,,,, Process_CuttingAndStitchingAndPacking)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Godown, False,,,,, Process_CuttingAndStitchingAndPacking)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ReferenceNo, True,, "Order No.",,, Process_CuttingAndStitchingAndPacking)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ReferenceDocIdDate, True,, "Order Date",,, Process_CuttingAndStitchingAndPacking)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ReferenceDocIdBalanceQty, True,, "Balance Qty",,, Process_CuttingAndStitchingAndPacking)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DocQty, True,,,,, Process_CuttingAndStitchingAndPacking)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1LossQty, False,,,,, Process_CuttingAndStitchingAndPacking)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Qty, False,,,,, Process_CuttingAndStitchingAndPacking)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Unit, True, False,, False,, Process_CuttingAndStitchingAndPacking)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Pcs, False,,,,, Process_CuttingAndStitchingAndPacking)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DealQty, False,,,,, Process_CuttingAndStitchingAndPacking)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DealUnit, False,,,,, Process_CuttingAndStitchingAndPacking)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Rate, False,,,,, Process_CuttingAndStitchingAndPacking)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Amount, False,,, False,, Process_CuttingAndStitchingAndPacking)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Remark, True,,,,, Process_CuttingAndStitchingAndPacking)



        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl4", FrmPurchInvoiceDirect_WithDimension.ColSNo, True,,,,, Process_CuttingAndStitchingAndPacking)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl4", FrmPurchInvoiceDirect_WithDimension.Col4ItemCategory, True,,,,, Process_CuttingAndStitchingAndPacking)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl4", FrmPurchInvoiceDirect_WithDimension.Col4Item, True,,,,, Process_CuttingAndStitchingAndPacking)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl4", FrmPurchInvoiceDirect_WithDimension.Col4Dimension1, True,,,,, Process_CuttingAndStitchingAndPacking)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl4", FrmPurchInvoiceDirect_WithDimension.Col4Dimension2, True,,,,, Process_CuttingAndStitchingAndPacking)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl4", FrmPurchInvoiceDirect_WithDimension.Col4Dimension3, True,,,,, Process_CuttingAndStitchingAndPacking)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl4", FrmPurchInvoiceDirect_WithDimension.Col4Dimension4, True,,,,, Process_CuttingAndStitchingAndPacking)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl4", FrmPurchInvoiceDirect_WithDimension.Col4Qty, True,,,,, Process_CuttingAndStitchingAndPacking)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl4", FrmPurchInvoiceDirect_WithDimension.Col4CurrentStockProcess, True,,,,, Process_CuttingAndStitchingAndPacking)

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", "", SettingFields.FilterInclude_Process, "+" + Process_CuttingAndStitchingAndPacking, AgDataType.Text, "255", mProcessFieldQry, AgHelpQueryType.SqlQuery, AgHelpSelectionType.MultiSelect,,, Voucher_Type_CuttingAndStitchingAndPackingReceive,,,, "+SUPPORT")
    End Sub
    Private Sub FConfigure_CuttingAndStitchingAndPackingInvoice(ClsObj As ClsMain)
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", "", SettingFields.FilterInclude_Process, "+" + Process_CuttingAndStitchingAndPacking, AgDataType.Text, "255", mProcessFieldQry, AgHelpQueryType.SqlQuery, AgHelpSelectionType.MultiSelect,,, Voucher_Type_CuttingAndStitchingAndPackingInvoice,,,, "+SUPPORT")
    End Sub

    Private Sub FConfigure_RawAndOtherMaterialStockReceive(ClsObj As ClsMain)
        'For Line
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.ColSNo, True,,,,,, SettingGroup_RawAndOtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Barcode, False,,,,,, SettingGroup_RawAndOtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCategory, True,,,,,, SettingGroup_RawAndOtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemGroup, False,,,,,, SettingGroup_RawAndOtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCode, False,,,,,, SettingGroup_RawAndOtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Item, True,,,,,, SettingGroup_RawAndOtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension1, True,,,,,, SettingGroup_RawAndOtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension2, False,,,,,, SettingGroup_RawAndOtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension3, True,,,,,, SettingGroup_RawAndOtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension4, True,,,,,, SettingGroup_RawAndOtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Size, False,,,,,, SettingGroup_RawAndOtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Specification, False,,,,,, SettingGroup_RawAndOtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1BaleNo, False,,,,,, SettingGroup_RawAndOtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1LotNo, False,,,,,, SettingGroup_RawAndOtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DocQty, True,,,,,, SettingGroup_RawAndOtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Unit, True,,,,,, SettingGroup_RawAndOtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Rate, False,,,,,, SettingGroup_RawAndOtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Amount, False,,, False,,, SettingGroup_RawAndOtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Remark, True,,,,,, SettingGroup_RawAndOtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.StockReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Godown, False,,,,,, SettingGroup_RawAndOtherMaterial)

        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.StockReceive, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.ColSNo, True,,,,,, SettingGroup_RawAndOtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.StockReceive, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.Col1ItemCategory, False,,,,,, SettingGroup_RawAndOtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.StockReceive, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.Col1ItemGroup, False,,,,,, SettingGroup_RawAndOtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.StockReceive, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.Col1Item, False,,,,,, SettingGroup_RawAndOtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.StockReceive, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.Col1Dimension1, False,,,,,, SettingGroup_RawAndOtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.StockReceive, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.Col1Dimension2, True,,,,,, SettingGroup_RawAndOtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.StockReceive, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.Col1Dimension3, False,,,,,, SettingGroup_RawAndOtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.StockReceive, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.Col1Dimension4, False,,,,,, SettingGroup_RawAndOtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.StockReceive, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.Col1Size, False,,,,,, SettingGroup_RawAndOtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.StockReceive, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.Col1Specification, False,,,,,, SettingGroup_RawAndOtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.StockReceive, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.Col1Pcs, True,,,,,, SettingGroup_RawAndOtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.StockReceive, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.Col1Qty, True,,,,,, SettingGroup_RawAndOtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchaseInvoiceDimension", Ncat.StockReceive, "Dgl1", FrmPurchaseInvoiceDimension_WithDimension.Col1TotalQty, True,,,,,, SettingGroup_RawAndOtherMaterial)



        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.StockReceive, SettingFields.FilterInclude_ItemType, "+" & ItemTypeCode.RawProduct & "+" & ItemTypeCode.OtherRawProduct, AgDataType.Text, "255", mItemTypeFieldQry, AgHelpQueryType.SqlQuery, AgHelpSelectionType.MultiSelect,,,,, SettingGroup_RawAndOtherMaterial,, "+SUPPORT")
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.StockReceive, SettingFields.DocumentPrintReportFileName, "", AgDataType.Text, "50",,,,,,,, SettingGroup_RawAndOtherMaterial,, "+SUPPORT")
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.StockReceive, SettingFields.FilterInclude_SubgroupType, "+" & SubgroupType.Jobworker, AgDataType.Text, "50",,,,,,,, SettingGroup_RawAndOtherMaterial,, "+SUPPORT")

        ClsObj.FUpdateSeed_EntryHeaderUISetting("FrmPurchInvoiceDirect", "", Ncat.StockReceive, "", "", SettingGroup_RawAndOtherMaterial, "DglMain", FrmPurchInvoiceDirect_WithDimension.hcSettingGroup, 1, 0, 1, 1, "")


        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.StockReceive, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcGodown, 1, 1,,,,, SettingGroup_RawAndOtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.StockReceive, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcRemarks, 1,,,,,, SettingGroup_RawAndOtherMaterial)
    End Sub
    Private Sub FConfigure_PaymentJobWorker(ClsObj As ClsMain)
        Dim Mdi As New MDIMain
        ClsObj.FSeedSingleIfNotExists_Voucher_Type(NCat_PaymentJobWorker, "Payment (Job Worker)",
                        NCat_PaymentJobWorker, VoucherCategory.Payment, "", "Customised",
                        Mdi.MnuPaymentEntryJobWorker.Name, Mdi.MnuPaymentEntryJobWorker.Text)

        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPaymentEntryJobWorker", NCat_PaymentJobWorker, "Dgl1", FrmPaymentEntryJobWorker.Col1Subcode, True, True, "Party Name")
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPaymentEntryJobWorker", NCat_PaymentJobWorker, "Dgl1", FrmPaymentEntryJobWorker.Col1BalanceInvoiceAmount, True, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPaymentEntryJobWorker", NCat_PaymentJobWorker, "Dgl1", FrmPaymentEntryJobWorker.Col1BalanceAdvance, True, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPaymentEntryJobWorker", NCat_PaymentJobWorker, "Dgl1", FrmPaymentEntryJobWorker.Col1OtherBalanceAmount, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPaymentEntryJobWorker", NCat_PaymentJobWorker, "Dgl1", FrmPaymentEntryJobWorker.Col1Deduction, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPaymentEntryJobWorker", NCat_PaymentJobWorker, "Dgl1", FrmPaymentEntryJobWorker.Col1OtherCharges, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPaymentEntryJobWorker", NCat_PaymentJobWorker, "Dgl1", FrmPaymentEntryJobWorker.Col1NetPayableAmount, True, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPaymentEntryJobWorker", NCat_PaymentJobWorker, "Dgl1", FrmPaymentEntryJobWorker.Col1PaidAmount, True, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPaymentEntryJobWorker", NCat_PaymentJobWorker, "Dgl1", FrmPaymentEntryJobWorker.Col1Remark, True, True)

        If AgL.FillData("Select Count(*) from LedgerHeadSetting Where V_Type = '" & NCat_PaymentJobWorker & "'", AgL.GcnMain).tables(0).Rows(0)(0) = 0 Then
            mQry = " Insert into LedgerHeadSetting (Code, V_Type) Values('" & NCat_PaymentJobWorker & "','" & NCat_PaymentJobWorker & "') "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If
    End Sub
    Private Sub FConfigure_StitchingOrder(ClsObj As ClsMain)
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.JobOrder, ClsMain.SettingFields.PostConsumptionYn, "1", ClsMain.AgDataType.YesNo, "50",,,,,,, Process_Stitching, ,, "+SUPPORT")
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.JobOrder, SettingFields.PostInStockYn, "1", AgDataType.YesNo, "50",,,,,,, Process_Stitching)
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.JobOrder, SettingFields.PostInStockProcessYn, "1", AgDataType.YesNo, "50",,,,,,, Process_Stitching)
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.JobOrder, SettingFields.FilterInclude_ContraProcess, "+" + Process_Cutting, AgDataType.Text, "255", mProcessFieldQry, AgHelpQueryType.SqlQuery, AgHelpSelectionType.MultiSelect,,,, Process_Stitching, ,, "+SUPPORT")

        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcVendorDocNo, 1,,,,, Process_Stitching)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcVendorDocDate, 1,,,,, Process_Stitching)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcDeliveryDate, 1,,,,, Process_Stitching)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcRateType, 1,,,,, Process_Stitching)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcBtnStockBalance, 1,,,,, Process_Stitching)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcBtnMaterialIssue, 1,,,,, Process_Stitching)

        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl4", FrmPurchInvoiceDirect_WithDimension.ColSNo, True,,,,, Process_Stitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl4", FrmPurchInvoiceDirect_WithDimension.Col4ItemCategory, True,,,,, Process_Stitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl4", FrmPurchInvoiceDirect_WithDimension.Col4Item, True,,,,, Process_Stitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl4", FrmPurchInvoiceDirect_WithDimension.Col4Dimension1, True,,,,, Process_Stitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl4", FrmPurchInvoiceDirect_WithDimension.Col4Dimension2, True,,,,, Process_Stitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl4", FrmPurchInvoiceDirect_WithDimension.Col4Dimension3, True,,,,, Process_Stitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl4", FrmPurchInvoiceDirect_WithDimension.Col4Dimension4, True,,,,, Process_Stitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl4", FrmPurchInvoiceDirect_WithDimension.Col4Qty, True,,,,, Process_Stitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl4", FrmPurchInvoiceDirect_WithDimension.Col4CurrentStock, True,,,,, Process_Stitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl4", FrmPurchInvoiceDirect_WithDimension.Col4ConsiderInIssueYN, True,,,,, Process_Stitching)

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", "", SettingFields.FilterInclude_Process, "+" + Process_Stitching, AgDataType.Text, "255", mProcessFieldQry, AgHelpQueryType.SqlQuery, AgHelpSelectionType.MultiSelect,,, Voucher_Type_StitchingOrder,,,, "+SUPPORT")
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", "", SettingFields.ActionOnStockBalanceExceed, ActionOnDuplicateItem.DoNothing, AgDataType.Text, "255", "ActionOnBalanceExceed", AgHelpQueryType.ClassName, AgHelpSelectionType.SingleSelect,,, Voucher_Type_StitchingOrder,,,, "+SUPPORT")
    End Sub
    Private Sub FConfigure_StitchingReceive(ClsObj As ClsMain)
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.JobReceive, ClsMain.SettingFields.PostConsumptionYn, "1", ClsMain.AgDataType.YesNo, "50",,,,,,, Process_Stitching, ,, "+SUPPORT")

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.JobReceive, SettingFields.PostConsumptionInStockYn, "0", AgDataType.YesNo, "50",,,,,,, Process_Stitching)
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.JobReceive, SettingFields.PostConsumptionInStockProcessYn, "1", AgDataType.YesNo, "50",,,,,,, Process_Stitching)

        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl4", FrmPurchInvoiceDirect_WithDimension.ColSNo, True,,,,, Process_Stitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl4", FrmPurchInvoiceDirect_WithDimension.Col4ItemCategory, True,,,,, Process_Stitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl4", FrmPurchInvoiceDirect_WithDimension.Col4Item, True,,,,, Process_Stitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl4", FrmPurchInvoiceDirect_WithDimension.Col4Dimension1, True,,,,, Process_Stitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl4", FrmPurchInvoiceDirect_WithDimension.Col4Dimension2, True,,,,, Process_Stitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl4", FrmPurchInvoiceDirect_WithDimension.Col4Dimension3, True,,,,, Process_Stitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl4", FrmPurchInvoiceDirect_WithDimension.Col4Dimension4, True,,,,, Process_Stitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl4", FrmPurchInvoiceDirect_WithDimension.Col4Qty, True,,,,, Process_Stitching)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl4", FrmPurchInvoiceDirect_WithDimension.Col4CurrentStockProcess, True,,,,, Process_Stitching)

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", "", SettingFields.FilterInclude_Process, "+" + Process_Stitching, AgDataType.Text, "255", mProcessFieldQry, AgHelpQueryType.SqlQuery, AgHelpSelectionType.MultiSelect,,, Voucher_Type_StitchingReceive,,,, "+SUPPORT")
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", "", SettingFields.ActionOnOrderBalanceExceed, ActionOnDuplicateItem.DoNothing, AgDataType.Text, "255", "ActionOnBalanceExceed", AgHelpQueryType.ClassName, AgHelpSelectionType.SingleSelect,,, Voucher_Type_StitchingReceive,,,, "+SUPPORT")
    End Sub
    Private Sub FConfigure_StitchingInvoice(ClsObj As ClsMain)
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", "", SettingFields.FilterInclude_Process, "+" + Process_Stitching, AgDataType.Text, "255", mProcessFieldQry, AgHelpQueryType.SqlQuery, AgHelpSelectionType.MultiSelect,,, Voucher_Type_StitchingInvoice,,,, "+SUPPORT")
    End Sub
    Private Sub FConfigure_PackingOrder(ClsObj As ClsMain)
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.JobOrder, SettingFields.PostInStockYn, "0", AgDataType.YesNo, "50",,,,,,, Process_Packing)
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.JobOrder, SettingFields.PostInStockProcessYn, "0", AgDataType.YesNo, "50",,,,,,, Process_Packing)

        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcVendorDocNo, 1,,,,, Process_Packing, "")
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcVendorDocDate, 1,,,,, Process_Packing, "")
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcDeliveryDate, 1,,,,, Process_Packing, "")
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcRemarks, 1,,,,, Process_Packing, "")

        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.ColSNo, True, True,,,, Process_Packing)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCategory, True,,,,, Process_Packing)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemGroup, False,,,,, Process_Packing)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCode, False,,,,, Process_Packing)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Item, False,,,,, Process_Packing)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension1, True,,,,, Process_Packing)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension2, True,,,,, Process_Packing)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension3, True,,,,, Process_Packing)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension4, False,,,,, Process_Packing)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Size, False,,,,, Process_Packing)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1RawMaterial, False,, "Fabric Width",,, Process_Packing)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Specification, False,,,,, Process_Packing)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1SalesTaxGroup, False,,,,, Process_Packing)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1BaleNo, False,,,,, Process_Packing)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1LotNo, False,,,,, Process_Packing)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DocQty, True,,,,, Process_Packing)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Unit, True,,,,, Process_Packing)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Rate, True,,,,, Process_Packing)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DiscountPer, False,,,,, Process_Packing)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DiscountAmount, False,,,,, Process_Packing)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionalDiscountPer, False,,,,, Process_Packing)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionalDiscountAmount, False,,,,, Process_Packing)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionPer, False,,,,, Process_Packing)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionAmount, False,,,,, Process_Packing)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Amount, True,,, False,, Process_Packing)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Remark, True,,,,, Process_Packing)

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", "", SettingFields.FilterInclude_Process, "+" + Process_Packing, AgDataType.Text, "255", mProcessFieldQry, AgHelpQueryType.SqlQuery, AgHelpSelectionType.MultiSelect,,, Voucher_Type_PackingOrder,,,, "+SUPPORT")
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", "", SettingFields.FilterInclude_ItemType, "+" + ItemTypeCode.ManufacturingProduct, AgDataType.Text, "255", mItemTypeFieldQry, AgHelpQueryType.SqlQuery, AgHelpSelectionType.MultiSelect,,  , Voucher_Type_PackingOrder,,, "+SUPPORT")
    End Sub
    Private Sub FConfigure_Packing(ClsObj As ClsMain)
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", "", SettingFields.GenerateBarcodeYn, "1", AgDataType.YesNo, "50",,,,,, Voucher_Type_Packing)

        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcGodown, 1, 1,,,, Process_Packing)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcRemarks, 1,,,,, Process_Packing)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcBtnPendingPurchOrder, 0,,,,, Process_Packing)

        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcBtnStockBalance, 1,,,,, Process_Packing)

        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.ColSNo, True,,,,, Process_Packing)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Barcode, False,,,,, Process_Packing)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCategory, True,,,,, Process_Packing)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemGroup, False,,,,, Process_Packing)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCode, False,,,,, Process_Packing)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Item, False,,,,, Process_Packing)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension1, True,,,,, Process_Packing)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension2, True,,,,, Process_Packing)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension3, True,,,,, Process_Packing)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension4, False,,,,, Process_Packing)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Size, True,,,,, Process_Packing)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Specification, False,,,,, Process_Packing)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemState, False,,,,, Process_Packing)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1BaleNo, False,,,,, Process_Packing)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1LotNo, False,,,,, Process_Packing)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Godown, False,,,,, Process_Packing)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ReferenceNo, False,, "Order No.",,, Process_Packing)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ReferenceDocIdDate, False,, "Order Date",,, Process_Packing)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ReferenceDocIdBalanceQty, False,, "Balance Qty",,, Process_Packing)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DocQty, True,,,,, Process_Packing)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1LossQty, False,,,,, Process_Packing)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Qty, False,,,,, Process_Packing)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Unit, True, False,, False,, Process_Packing)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Pcs, False,,,,, Process_Packing)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DealQty, False,,,,, Process_Packing)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DealUnit, False,,,,, Process_Packing)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Rate, False,,,,, Process_Packing)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Amount, False,,, False,, Process_Packing)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Remark, True,,,,, Process_Packing)

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", "", SettingFields.FilterInclude_Process, "+" + Process_Packing, AgDataType.Text, "255", mProcessFieldQry, AgHelpQueryType.SqlQuery, AgHelpSelectionType.MultiSelect,,  , Voucher_Type_Packing,,, "+SUPPORT")
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", "", SettingFields.FilterInclude_ItemType, "+" + ItemTypeCode.ManufacturingProduct, AgDataType.Text, "255", mItemTypeFieldQry, AgHelpQueryType.SqlQuery, AgHelpSelectionType.MultiSelect,,  , Voucher_Type_Packing,,, "+SUPPORT")
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", "", SettingFields.PostStockReverseEntryAlsoYn, "1", AgDataType.YesNo, "50",,,,,, Voucher_Type_Packing, "",,, "+SUPPORT")
    End Sub
    Private Sub FConfigure_PackingInvoice(ClsObj As ClsMain)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.ColSNo, True, True,,,, Process_Packing)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Barcode, False,,,,, Process_Packing)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCategory, True,,,,, Process_Packing)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension1, True,,,,, Process_Packing)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension2, True,,,,, Process_Packing)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension3, True,,,,, Process_Packing)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Size, True,,,,, Process_Packing)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Specification, False,,,,, Process_Packing)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1SalesTaxGroup, False,,,,, Process_Packing)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1BaleNo, False,,,,, Process_Packing)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1LotNo, False,,,,, Process_Packing)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DocQty, True,,,,, Process_Packing)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Unit, True,,,,, Process_Packing)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Rate, True,,,,, Process_Packing)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DiscountPer, False,,,,, Process_Packing)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DiscountAmount, False,,,,, Process_Packing)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionalDiscountPer, False,,,,, Process_Packing)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionalDiscountAmount, False,,,,, Process_Packing)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionPer, False,,,,, Process_Packing)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionAmount, False,,,,, Process_Packing)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Amount, True,,, False,, Process_Packing)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Remark, True,,,,, Process_Packing)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ReferenceNo, True,, "Order No.", False,, Process_Packing)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ReferenceDocIdDate, True,, "Order Date", False,, Process_Packing)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ReferenceDocIdBalanceQty, True,, "Balance Qty", True,, Process_Packing)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Godown, False,,,,, Process_Packing)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1PurchaseInvoice, False, False, "Order No", False,, Process_Packing)

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", "", SettingFields.FilterInclude_Process, "+" + Process_Packing, AgDataType.Text, "255", mProcessFieldQry, AgHelpQueryType.SqlQuery, AgHelpSelectionType.MultiSelect,,, Voucher_Type_PackingInvoice,,,, "+SUPPORT")
    End Sub
    Private Sub FConfigure_OtherOrder(ClsObj As ClsMain)
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.JobOrder, SettingFields.PostInStockYn, "1", AgDataType.YesNo, "50",,,,,,, Process_Other)
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.JobOrder, SettingFields.PostInStockProcessYn, "1", AgDataType.YesNo, "50",,,,,,, Process_Other)

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.JobOrder, ClsMain.SettingFields.PostConsumptionYn, "1", ClsMain.AgDataType.YesNo, "50",,,,,,, Process_Other, ,, "+SUPPORT")
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.JobOrder, SettingFields.PostInStockYn, "0", AgDataType.YesNo, "50",,,,,,, Process_Other)
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.JobOrder, SettingFields.PostInStockProcessYn, "0", AgDataType.YesNo, "50",,,,,,, Process_Other)

        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcVendorDocNo, 1,,,,, Process_Other, "")
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcVendorDocDate, 1,,,,, Process_Other, "")
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcDeliveryDate, 1,,,,, Process_Other, "")
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcRateType, 1,,,,, Process_Other, "")
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcBtnMaterialIssue, 1,,,,, Process_Other, "")

        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.ColSNo, True, True,,,, Process_Other)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCategory, True,,,,, Process_Other)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemGroup, False,,,,, Process_Other)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCode, False,,,,, Process_Other)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Item, False,,,,, Process_Other)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension1, False,,,,, Process_Other)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension2, False,,,,, Process_Other)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension3, False,,,,, Process_Other)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Size, False,,,,, Process_Other)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1RawMaterial, False,, "Fabric Width",,, Process_Other)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Specification, False,,,,, Process_Other)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1SalesTaxGroup, False,,,,, Process_Other)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1BaleNo, False,,,,, Process_Other)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1LotNo, False,,,,, Process_Other)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DocQty, True,,,,, Process_Other)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Unit, True,,,,, Process_Other)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Rate, True,,,,, Process_Other)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DiscountPer, False,,,,, Process_Other)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DiscountAmount, False,,,,, Process_Other)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionalDiscountPer, False,,,,, Process_Other)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionalDiscountAmount, False,,,,, Process_Other)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionPer, False,,,,, Process_Other)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionAmount, False,,,,, Process_Other)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Amount, True,,, False,, Process_Other)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Remark, True,,,,, Process_Other)

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", "", SettingFields.FilterInclude_Process, "+" + Process_Other, AgDataType.Text, "255", mProcessFieldQry, AgHelpQueryType.SqlQuery, AgHelpSelectionType.MultiSelect,,, Voucher_Type_OtherProcessOrder,,,, "+SUPPORT")
    End Sub
    Private Sub FConfigure_OtherReceive(ClsObj As ClsMain)
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.JobReceive, ClsMain.SettingFields.PostConsumptionYn, "1", ClsMain.AgDataType.YesNo, "50",,,,,,, Process_Other, ,, "+SUPPORT")
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.JobReceive, ClsMain.SettingFields.PostConsumptionYn, "1", ClsMain.AgDataType.YesNo, "50",,,,,,, Process_Other, ,, "+SUPPORT")

        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.ColSNo, True,,,,, Process_Other)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Barcode, False,,,,, Process_Other)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCategory, False,,,,, Process_Other)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemGroup, False,,,,, Process_Other)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCode, False,,,,, Process_Other)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Item, True,,,,, Process_Other)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension1, False,,,,, Process_Other)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension2, False,,,,, Process_Other)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension3, False,,,,, Process_Other)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension4, False,,,,, Process_Other)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Size, False,,,,, Process_Other)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1RawMaterial, False,, "Fabric Width",,, Process_Other)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1RawMaterialConsumptionQty, False,, "Fabric Consumption Qty",,, Process_Other)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Specification, False,,,,, Process_Other)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemState, False,,,,, Process_Other)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1BaleNo, False,,,,, Process_Other)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1LotNo, False,,,,, Process_Other)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Godown, False,,,,, Process_Other)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ReferenceNo, True,, "Order No.",,, Process_Other)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ReferenceDocIdDate, True,, "Order Date",,, Process_Other)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ReferenceDocIdBalanceQty, True,, "Balance Qty",,, Process_Other)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DocQty, True,,,,, Process_Other)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1LossQty, False,,,,, Process_Other)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Qty, False,,,,, Process_Other)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Unit, True, False,, False,, Process_Other)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Pcs, False,,,,, Process_Other)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DealQty, False,,,,, Process_Other)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DealUnit, False,,,,, Process_Other)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Rate, False,,,,, Process_Other)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Amount, False,,, False,, Process_Other)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Remark, True,,,,, Process_Other)

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", "", SettingFields.FilterInclude_Process, "+" + Process_Other, AgDataType.Text, "255", mProcessFieldQry, AgHelpQueryType.SqlQuery, AgHelpSelectionType.MultiSelect,,, Voucher_Type_OtherProcessReceive,,,, "+SUPPORT")

        mQry = " UPDATE Voucher_Type Set Status = 'InActive' Where V_Type In ('" & Voucher_Type_OtherProcessReceive & "')"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
    End Sub
    Private Sub FConfigure_OtherInvoice(ClsObj As ClsMain)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.ColSNo, True, True,,,, Process_Other)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCategory, True,,,,, Process_Other)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemGroup, False,,,,, Process_Other)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCode, False,,,,, Process_Other)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Item, False,,,,, Process_Other)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension1, False,,,,, Process_Other)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension2, False,,,,, Process_Other)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension3, False,,,,, Process_Other)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Size, False,,,,, Process_Other)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1RawMaterial, False,, "Fabric Width",,, Process_Other)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Specification, False,,,,, Process_Other)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1SalesTaxGroup, False,,,,, Process_Other)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1BaleNo, False,,,,, Process_Other)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1LotNo, False,,,,, Process_Other)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DocQty, True,,,,, Process_Other)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Unit, True,,,,, Process_Other)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Rate, True,,,,, Process_Other)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DiscountPer, False,,,,, Process_Other)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DiscountAmount, False,,,,, Process_Other)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionalDiscountPer, False,,,,, Process_Other)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionalDiscountAmount, False,,,,, Process_Other)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionPer, False,,,,, Process_Other)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionAmount, False,,,,, Process_Other)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Amount, True,,, False,, Process_Other)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Remark, True,,,,, Process_Other)

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", "", SettingFields.FilterInclude_Process, "+" + Process_Other, AgDataType.Text, "255", mProcessFieldQry, AgHelpQueryType.SqlQuery, AgHelpSelectionType.MultiSelect,,, Voucher_Type_OtherProcessInvoice,,,, "+SUPPORT")
    End Sub
    Private Sub FConfigure_ItemCategory(ClsObj As ClsMain)
        ClsObj.FUpdateSeed_EntryHeaderUISetting("FrmItemCategory", "", ItemTypeCode.ManufacturingProduct, "", "", "", "Dgl1", FrmItemCategory_Grid.hcBarcodeType, 1, 0, 1, 0, "")
        ClsObj.FUpdateSeed_EntryHeaderUISetting("FrmItemCategory", "", ItemTypeCode.ManufacturingProduct, "", "", "", "Dgl1", FrmItemCategory_Grid.hcBarcodePattern, 1, 0, 1, 0, "")
    End Sub
    Private Sub FConfigure_VoucherType(ClsObj As ClsMain)
        Dim MdiObj As New MDIMain
        ClsObj.FSeedSingleIfNotExists_Voucher_Type(Voucher_Type_CuttingOrder, "Cutting Order", Ncat.JobOrder, VoucherCategory.Production, "", "Customised", MdiObj.MnuJobOrder.Name, MdiObj.MnuJobOrder.Text)
        ClsObj.FSeedSingleIfNotExists_Voucher_Type(Voucher_Type_CuttingAndStitchingOrder, "Cutting + Stitching Order", Ncat.JobOrder, VoucherCategory.Production, "", "Customised", MdiObj.MnuJobOrder.Name, MdiObj.MnuJobOrder.Text)
        ClsObj.FSeedSingleIfNotExists_Voucher_Type(Voucher_Type_CuttingAndStitchingAndPackingOrder, "Cutting + Stitching + Packing Order", Ncat.JobOrder, VoucherCategory.Production, "", "Customised", MdiObj.MnuJobOrder.Name, MdiObj.MnuJobOrder.Text)
        ClsObj.FSeedSingleIfNotExists_Voucher_Type(Voucher_Type_StitchingOrder, "Stitching Order", Ncat.JobOrder, VoucherCategory.Production, "", "Customised", MdiObj.MnuJobOrder.Name, MdiObj.MnuJobOrder.Text)
        ClsObj.FSeedSingleIfNotExists_Voucher_Type(Voucher_Type_OtherProcessOrder, "Other Process Order", Ncat.JobOrder, VoucherCategory.Production, "", "Customised", MdiObj.MnuJobOrder.Name, MdiObj.MnuJobOrder.Text)
        ClsObj.FSeedSingleIfNotExists_Voucher_Type(Voucher_Type_PackingOrder, "Packing Order", Ncat.JobOrder, VoucherCategory.Production, "", "Customised", MdiObj.MnuJobOrder.Name, MdiObj.MnuJobOrder.Text)

        ClsObj.FSeedSingleIfNotExists_Voucher_Type(Voucher_Type_CuttingReceive, "Cutting Receive", Ncat.JobReceive, VoucherCategory.Production, "", "Customised", MdiObj.MnuJobReceive.Name, MdiObj.MnuJobReceive.Text)
        ClsObj.FSeedSingleIfNotExists_Voucher_Type(Voucher_Type_CuttingAndStitchingReceive, "Cutting + Stitching Receive", Ncat.JobReceive, VoucherCategory.Production, "", "Customised", MdiObj.MnuJobReceive.Name, MdiObj.MnuJobReceive.Text)
        ClsObj.FSeedSingleIfNotExists_Voucher_Type(Voucher_Type_CuttingAndStitchingAndPackingReceive, "Cutting + Stitching + Packing Receive", Ncat.JobReceive, VoucherCategory.Production, "", "Customised", MdiObj.MnuJobReceive.Name, MdiObj.MnuJobReceive.Text)
        ClsObj.FSeedSingleIfNotExists_Voucher_Type(Voucher_Type_StitchingReceive, "Stitching Receive", Ncat.JobReceive, VoucherCategory.Production, "", "Customised", MdiObj.MnuJobReceive.Name, MdiObj.MnuJobReceive.Text)
        ClsObj.FSeedSingleIfNotExists_Voucher_Type(Voucher_Type_OtherProcessReceive, "Other Process Receive", Ncat.JobReceive, VoucherCategory.Production, "", "Customised", MdiObj.MnuJobReceive.Name, MdiObj.MnuJobReceive.Text)

        ClsObj.FSeedSingleIfNotExists_Voucher_Type(Voucher_Type_CuttingInvoice, "Cutting Invoice", Ncat.JobInvoice, VoucherCategory.Production, "", "Customised", MdiObj.MnuJobInvoice.Name, MdiObj.MnuJobInvoice.Text)
        ClsObj.FSeedSingleIfNotExists_Voucher_Type(Voucher_Type_CuttingAndStitchingInvoice, "Cutting + Stitching Invoice", Ncat.JobInvoice, VoucherCategory.Production, "", "Customised", MdiObj.MnuJobInvoice.Name, MdiObj.MnuJobInvoice.Text)
        ClsObj.FSeedSingleIfNotExists_Voucher_Type(Voucher_Type_CuttingAndStitchingAndPackingInvoice, "Cutting + Stitching + Packing Invoice", Ncat.JobInvoice, VoucherCategory.Production, "", "Customised", MdiObj.MnuJobInvoice.Name, MdiObj.MnuJobInvoice.Text)
        ClsObj.FSeedSingleIfNotExists_Voucher_Type(Voucher_Type_StitchingInvoice, "Stitching Invoice", Ncat.JobInvoice, VoucherCategory.Production, "", "Customised", MdiObj.MnuJobInvoice.Name, MdiObj.MnuJobInvoice.Text)
        ClsObj.FSeedSingleIfNotExists_Voucher_Type(Voucher_Type_OtherProcessInvoice, "Other Process Invoice", Ncat.JobInvoice, VoucherCategory.Production, "", "Customised", MdiObj.MnuJobInvoice.Name, MdiObj.MnuJobInvoice.Text)

        ClsObj.FSeedSingleIfNotExists_Voucher_Type(Voucher_Type_PackingInvoice, "Packing Invoice", Ncat.JobInvoice, VoucherCategory.Production, "", "Customised", MdiObj.MnuJobInvoice.Name, MdiObj.MnuJobInvoice.Text)

        mQry = " UPDATE Voucher_Type Set NCat = '" & Ncat.JobReceive & "', Category = 'Prod'
                Where V_Type = 'PK'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
    End Sub
    'Private Sub FConfigure_OpeningStock(ClsObj As ClsMain)
    '    ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.ColSNo, True,,,,, Process_Cutting)
    '    ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Barcode, False,,,,, Process_Cutting)
    '    ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCategory, True,,,,, Process_Cutting)
    '    ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemGroup, False,,,,, Process_Cutting)
    '    ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCode, False,,,,, Process_Cutting)
    '    ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Item, False,,,,, Process_Cutting)
    '    ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension1, True,,,,, Process_Cutting)
    '    ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension2, True,,,,, Process_Cutting)
    '    ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension3, True,,,,, Process_Cutting)
    '    ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension4, False,,,,, Process_Cutting)
    '    ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Size, True,,,,, Process_Cutting)
    '    ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Specification, False,,,,, Process_Cutting)
    '    ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemState, False,,,,, Process_Cutting)
    '    ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1BaleNo, False,,,,, Process_Cutting)
    '    ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1LotNo, False,,,,, Process_Cutting)
    '    ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Godown, False,,,,, Process_Cutting)
    '    ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ReferenceNo, False,, "Order No.",,, Process_Cutting)
    '    ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ReferenceDocIdDate, False,, "Order Date",,, Process_Cutting)
    '    ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ReferenceDocIdBalanceQty, False,, "Balance Qty",,, Process_Cutting)
    '    ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DocQty, True,,,,, Process_Cutting)
    '    ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1LossQty, False,,,,, Process_Cutting)
    '    ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Qty, False,,,,, Process_Cutting)
    '    ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Unit, True, False,, False,, Process_Cutting)
    '    ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Pcs, False,,,,, Process_Cutting)
    '    ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DealQty, False,,,,, Process_Cutting)
    '    ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DealUnit, False,,,,, Process_Cutting)
    '    ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Rate, False,,,,, Process_Cutting)
    '    ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Amount, False,,,False,, Process_Cutting)
    '    ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Remark, True,,,,, Process_Cutting)
    'End Sub
End Class
