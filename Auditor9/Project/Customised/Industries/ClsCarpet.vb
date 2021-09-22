Imports AgLibrary.ClsMain.agConstants
Imports Customised.ClsMain

Public Class ClsCarpet
    Public Const NCat_DyeingPlan As String = "DPL"

    Public Const NCat_WeavingOrder As String = "WVO"
    Public Const NCat_WeavingReceive As String = "WVRC"
    Public Const NCat_WeavingInvoice As String = "WVI"

    Public Const NCat_DyeingOrder As String = "DO"
    Public Const NCat_DyeingReceive As String = "DRC"
    Public Const NCat_DyeingInvoice As String = "DI"

    Public Const NCat_FinishingOrder As String = "FO"
    Public Const NCat_FinishingReceive As String = "FRC"
    Public Const NCat_FinishingInvoice As String = "FI"

    Public Const Process_Dyeing As String = "PDyeing"
    Public Const Process_Weaving As String = "PWeaving"
    Public Const Process_Finishing As String = "PFinishing"

    Public Const Process_Washing As String = "PWashing"
    Public Const Process_Binding As String = "PBinding"
    Public Const Process_ThirdBacking As String = "PTBacking"

    Public Const SubGroupType_JobWorker As String = "Job Worker"

    Private Const SettingGroup_FinishedMaterial As String = "FM"
    Private Const SettingGroup_RawMaterial As String = "RM"
    Private Const SettingGroup_OtherMaterial As String = "OM"

    Private Const V_Type_SaleEnquirySample As String = "SES"
    Private Const V_Type_SaleOrderSample As String = "SOS"

    Dim MdiObj As New MDICarpet
    Private mQry As String = ""
    Public Sub FSeedData_Carpet()
        Dim ClsObj As New Customised.ClsMain(AgL)
        Try
            If ClsMain.IsScopeOfWorkContains(IndustryType.CarpetIndustry) Then
                If ClsMain.IsScopeOfWorkContains(IndustryType.SubIndustryType.ProductionModule) Then
                    ClsObj.AppendScopeOfWork(IndustryType.CommonModules.SalesEnquiry)
                    ClsObj.AppendScopeOfWork(IndustryType.CommonModules.SalesOrder)
                    ClsObj.AppendScopeOfWork(IndustryType.CommonModules.BOM)
                    ClsObj.AppendScopeOfWork(IndustryType.CommonModules.BOMOtherModule)
                    ClsObj.AppendScopeOfWork(IndustryType.CommonModules.PlanningModule)
                    ClsObj.AppendScopeOfWork(IndustryType.CommonModules.Dimension1)
                    ClsObj.AppendScopeOfWork(IndustryType.CommonModules.Dimension2)
                    ClsObj.AppendScopeOfWork(IndustryType.CommonModules.Dimension3)
                    ClsObj.AppendScopeOfWork(IndustryType.CommonModules.Dimension4)
                    ClsObj.AppendScopeOfWork(IndustryType.CommonModules.Size)
                    ClsObj.AppendScopeOfWork(IndustryType.CommonModules.PurchaseOrderModule)
                    ClsObj.AppendScopeOfWork(IndustryType.CommonModules.PurchaseGoodsReceiptModule)


                    FConfigure_ItemTypes(ClsObj)
                    FConfigure_ItemCategory(ClsObj)
                    FConfigure_ItemGroup(ClsObj)
                    FConfigure_Dimensions(ClsObj)
                    FConfigure_Shapes(ClsObj)
                    FConfigure_Size(ClsObj)
                    FConfigure_Process(ClsObj)
                    FConfigure_Unit(ClsObj)
                    FConfigure_Bom(ClsObj)

                    FConfigure_SettingGroup(ClsObj)

                    FConfigure_SaleEnquiry(ClsObj)
                    FConfigure_SaleOrder(ClsObj)

                    FConfigure_SaleEnquiry_Sample(ClsObj)
                    FConfigure_SaleOrder_Sample(ClsObj)

                    FConfigure_FinishedMaterialPlan(ClsObj)
                    FConfigure_DyeingPlan(ClsObj)
                    FConfigure_DyeingOrder(ClsObj)
                    FConfigure_DyeingReceive(ClsObj)
                    FConfigure_DyeingInvoice(ClsObj)
                    FConfigure_WeavingOrder(ClsObj)
                    FConfigure_WeavingReceive(ClsObj)
                    FConfigure_WeavingInvoice(ClsObj)
                    FConfigure_FinishingOrder(ClsObj)
                    FConfigure_FinishingReceive(ClsObj)
                    FConfigure_FinishingInvoice(ClsObj)

                    FConfigure_FinishedMaterialPurchaseOrder(ClsObj)
                    FConfigure_FinishedMaterialPurchaseInvoice(ClsObj)
                    FConfigure_RawMaterialPurchaseOrder(ClsObj)
                    FConfigure_RawMaterialPurchaseInvoice(ClsObj)
                    FConfigure_OtherMaterialPurchaseOrder(ClsObj)
                    FConfigure_OtherMaterialPurchaseInvoice(ClsObj)

                    FConfigure_FinishedMaterialOpeningStock(ClsObj)
                    FConfigure_RawMaterialOpeningStock(ClsObj)
                    FConfigure_OtherMaterialOpeningStock(ClsObj)

                    FConfigure_FinishedMaterialStockReceive(ClsObj)
                    FConfigure_RawMaterialStockReceive(ClsObj)
                    FConfigure_OtherMaterialStockReceive(ClsObj)

                    FConfigure_FinishedMaterialStockIssue(ClsObj)
                    FConfigure_RawMaterialStockIssue(ClsObj)
                    FConfigure_OtherMaterialStockIssue(ClsObj)

                    FConfigure_FinishedMaterialPurchaseGoodsReceipt(ClsObj)
                    FConfigure_RawMaterialPurchaseGoodsReceipt(ClsObj)
                    FConfigure_OtherMaterialPurchaseGoodsReceipt(ClsObj)

                    FConfigure_DyeingOrderStatusReport(ClsObj)
                    FConfigure_WeavingOrderStatusReport(ClsObj)
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " In FSeedData_Carpet")
        End Try
    End Sub
    Private Sub FConfigure_WeavingOrder(ClsObj As ClsMain)
        ClsObj.FSeedSingleIfNotExists_Voucher_Type(NCat_WeavingOrder, "Weaving Order", NCat_WeavingOrder, VoucherCategory.Purchase, "", "Customised", MdiObj.MnuWeavingOrder.Name, MdiObj.MnuWeavingOrder.Text)

        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_WeavingOrder, "DglMain", AgTemplate.TempTransaction1.hcSite_Code, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_WeavingOrder, "DglMain", AgTemplate.TempTransaction1.hcV_Type, 1, 1, 1, "Order Type")
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_WeavingOrder, "DglMain", AgTemplate.TempTransaction1.hcV_Date, 1, 1, 1, "Order Date")
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_WeavingOrder, "DglMain", AgTemplate.TempTransaction1.hcV_No, 0, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_WeavingOrder, "DglMain", AgTemplate.TempTransaction1.hcReferenceNo, 1, 1, 1, "Order No")
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_WeavingOrder, "DglMain", AgTemplate.TempTransaction1.hcSettingGroup, 0, 0, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_WeavingOrder, "DglMain", FrmPurchInvoiceDirect_WithDimension.hcProcess, 0, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_WeavingOrder, "DglMain", FrmPurchInvoiceDirect_WithDimension.hcVendor, 1, 1, 1, "Job Worker")
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_WeavingOrder, "DglMain", FrmPurchInvoiceDirect_WithDimension.hcBillToParty, 0, 0)

        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_WeavingOrder, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcVendorDocNo, 1, 0, 0, "Party Doc No")
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_WeavingOrder, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcVendorDocDate, 1, 0, 0, "Party Doc Date")
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_WeavingOrder, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcDeliveryDate, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_WeavingOrder, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcAgent, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_WeavingOrder, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcTags, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_WeavingOrder, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcRemarks, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_WeavingOrder, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcBtnTransportDetail, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_WeavingOrder, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcBtnPendingPurchPlan, 1)


        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_WeavingOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.ColSNo, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_WeavingOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCategory, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_WeavingOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension1, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_WeavingOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension2, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_WeavingOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension3, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_WeavingOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Size, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_WeavingOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Specification, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_WeavingOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1SalesTaxGroup, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_WeavingOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DocQty, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_WeavingOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Unit, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_WeavingOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1UnitMultiplier, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_WeavingOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DealQty, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_WeavingOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DealUnit, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_WeavingOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Rate, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_WeavingOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DiscountPer, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_WeavingOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DiscountAmount, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_WeavingOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionalDiscountPer, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_WeavingOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionalDiscountAmount, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_WeavingOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionPer, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_WeavingOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionAmount, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_WeavingOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Amount, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_WeavingOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Remark, True)

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", NCat_WeavingOrder, ClsMain.SettingFields.FilterInclude_Process,
            "+" + Process_Weaving, ClsMain.AgDataType.Text, "255",
            "SELECT Code, Name From viewHelpSubgroup Sg  With (NoLock) Where SubgroupType ='" & SubgroupType.Process & "' Order By Name",
            ClsMain.AgHelpQueryType.SqlQuery, ClsMain.AgHelpSelectionType.MultiSelect,,,,, ,, "+SUPPORT")

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", NCat_WeavingOrder, ClsMain.SettingFields.PostConsumptionYn,
            "1", ClsMain.AgDataType.YesNo, "50",,,,,,,, ,, "+SUPPORT")

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", NCat_WeavingOrder, ClsMain.SettingFields.FilterInclude_ItemType,
            "+" + ItemTypeCode.ManufacturingProduct, ClsMain.AgDataType.Text, "255",
            "SELECT Code, Name FROM ItemType Order By Name",
            ClsMain.AgHelpQueryType.SqlQuery, ClsMain.AgHelpSelectionType.MultiSelect,,,,, ,, "+SUPPORT")
    End Sub
    Private Sub FConfigure_WeavingInvoice(ClsObj As ClsMain)
        ClsObj.FSeedSingleIfNotExists_Voucher_Type(NCat_WeavingInvoice, "Weaving Invoice", NCat_WeavingInvoice, VoucherCategory.Purchase, "", "Customised", MdiObj.MnuWeavingInvoice.Name, MdiObj.MnuWeavingInvoice.Text)

        mQry = "Update Voucher_Type SET Structure = 'GstPur' Where V_Type = '" & NCat_WeavingInvoice & "' And Structure Is Null "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)

        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_WeavingInvoice, "DglMain", AgTemplate.TempTransaction1.hcSite_Code, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_WeavingInvoice, "DglMain", AgTemplate.TempTransaction1.hcV_Type, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_WeavingInvoice, "DglMain", AgTemplate.TempTransaction1.hcV_Date, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_WeavingInvoice, "DglMain", AgTemplate.TempTransaction1.hcV_No, 0, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_WeavingInvoice, "DglMain", AgTemplate.TempTransaction1.hcReferenceNo, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_WeavingInvoice, "DglMain", AgTemplate.TempTransaction1.hcSettingGroup, 0, 0, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_WeavingInvoice, "DglMain", FrmPurchInvoiceDirect_WithDimension.hcProcess, 0, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_WeavingInvoice, "DglMain", FrmPurchInvoiceDirect_WithDimension.hcVendor, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_WeavingInvoice, "DglMain", FrmPurchInvoiceDirect_WithDimension.hcBillToParty, 1, 1)

        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_WeavingInvoice, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcVendorDocNo, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_WeavingInvoice, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcVendorDocDate, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_WeavingInvoice, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcAgent, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_WeavingInvoice, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcTags, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_WeavingInvoice, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcRemarks, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_WeavingInvoice, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcBtnTransportDetail, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_WeavingInvoice, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcBtnPendingPurchPlan, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_WeavingInvoice, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcBtnPendingStockReceive, 1)

        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_WeavingInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.ColSNo, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_WeavingInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Barcode, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_WeavingInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCategory, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_WeavingInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension1, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_WeavingInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension2, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_WeavingInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension3, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_WeavingInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Size, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_WeavingInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Specification, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_WeavingInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1SalesTaxGroup, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_WeavingInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DocQty, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_WeavingInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Unit, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_WeavingInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DealQty, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_WeavingInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DealUnit, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_WeavingInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Rate, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_WeavingInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DiscountPer, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_WeavingInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DiscountAmount, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_WeavingInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionalDiscountPer, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_WeavingInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionalDiscountAmount, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_WeavingInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionPer, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_WeavingInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionAmount, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_WeavingInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Amount, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_WeavingInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Remark, True)

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", NCat_WeavingInvoice, ClsMain.SettingFields.FilterInclude_Process,
            "+" + Process_Weaving, ClsMain.AgDataType.Text, "255",
            "SELECT Code, Name From viewHelpSubgroup Sg  With (NoLock) Where SubgroupType ='" & SubgroupType.Process & "' Order By Name",
            ClsMain.AgHelpQueryType.SqlQuery, ClsMain.AgHelpSelectionType.MultiSelect,,,,, ,, "+SUPPORT")

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", NCat_WeavingInvoice, ClsMain.SettingFields.PostInStockProcessYn,
            "1", ClsMain.AgDataType.YesNo, "50",,,,,,,, ,, "+SUPPORT")

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", NCat_WeavingInvoice, ClsMain.SettingFields.FilterInclude_ItemType,
            "+" + ItemTypeCode.ManufacturingProduct, ClsMain.AgDataType.Text, "255",
            "SELECT Code, Name FROM ItemType Order By Name",
            ClsMain.AgHelpQueryType.SqlQuery, ClsMain.AgHelpSelectionType.MultiSelect,,,,, ,, "+SUPPORT")
    End Sub
    Private Sub FConfigure_DyeingOrder(ClsObj As ClsMain)
        ClsObj.FSeedSingleIfNotExists_Voucher_Type(NCat_DyeingOrder, "Dyeing Order", NCat_DyeingOrder, VoucherCategory.Purchase, "", "Customised", MdiObj.MnuDyeingOrder.Name, MdiObj.MnuDyeingOrder.Text)

        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_DyeingOrder, "DglMain", AgTemplate.TempTransaction1.hcSite_Code, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_DyeingOrder, "DglMain", AgTemplate.TempTransaction1.hcV_Type, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_DyeingOrder, "DglMain", AgTemplate.TempTransaction1.hcV_Date, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_DyeingOrder, "DglMain", AgTemplate.TempTransaction1.hcV_No, 0, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_DyeingOrder, "DglMain", AgTemplate.TempTransaction1.hcReferenceNo, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_DyeingOrder, "DglMain", AgTemplate.TempTransaction1.hcSettingGroup, 0, 0, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_DyeingOrder, "DglMain", FrmPurchInvoiceDirect_WithDimension.hcProcess, 0, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_DyeingOrder, "DglMain", FrmPurchInvoiceDirect_WithDimension.hcVendor, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_DyeingOrder, "DglMain", FrmPurchInvoiceDirect_WithDimension.hcBillToParty, 0, 1)

        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_DyeingOrder, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcVendorDocNo, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_DyeingOrder, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcVendorDocDate, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_DyeingOrder, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcDeliveryDate, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_DyeingOrder, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcAgent, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_DyeingOrder, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcTags, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_DyeingOrder, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcRemarks, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_DyeingOrder, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcBtnTransportDetail, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_DyeingOrder, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcBtnPendingPurchPlan, 1)


        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_DyeingOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.ColSNo, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_DyeingOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCategory, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_DyeingOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemGroup, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_DyeingOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Item, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_DyeingOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension4, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_DyeingOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Specification, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_DyeingOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1SalesTaxGroup, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_DyeingOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DocQty, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_DyeingOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Unit, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_DyeingOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Rate, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_DyeingOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DiscountPer, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_DyeingOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DiscountAmount, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_DyeingOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionalDiscountPer, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_DyeingOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionalDiscountAmount, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_DyeingOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionPer, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_DyeingOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionAmount, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_DyeingOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Amount, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_DyeingOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Remark, True)

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", NCat_DyeingOrder, ClsMain.SettingFields.FilterInclude_Process,
            "+" + Process_Dyeing, ClsMain.AgDataType.Text, "255",
            "SELECT Code, Name From viewHelpSubgroup Sg  With (NoLock) Where SubgroupType ='" & SubgroupType.Process & "' Order By Name",
            ClsMain.AgHelpQueryType.SqlQuery, ClsMain.AgHelpSelectionType.MultiSelect,,,,, ,, "+SUPPORT")

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", NCat_DyeingOrder, ClsMain.SettingFields.FilterInclude_SubgroupType,
            "+" + SubgroupType.Jobworker, ClsMain.AgDataType.Text, "255",
            "SELECT SubgroupType AS Code, SubgroupType AS Name  FROM SubGroupType ORDER BY SubgroupType",
            ClsMain.AgHelpQueryType.SqlQuery, ClsMain.AgHelpSelectionType.MultiSelect,,,,, ,, "+SUPPORT")

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", NCat_DyeingOrder, ClsMain.SettingFields.FilterInclude_ItemType,
            "+" + ItemTypeCode.RawProduct, ClsMain.AgDataType.Text, "255",
            "SELECT Code, Name FROM ItemType Order By Name",
            ClsMain.AgHelpQueryType.SqlQuery, ClsMain.AgHelpSelectionType.MultiSelect,,,,, ,, "+SUPPORT")
    End Sub
    Private Sub FConfigure_DyeingReceive(ClsObj As ClsMain)
        ClsObj.FSeedSingleIfNotExists_Voucher_Type(NCat_DyeingReceive, "Dyeing Receive", NCat_DyeingReceive, VoucherCategory.Purchase, "", "Customised", MdiObj.MnuDyeingReceive.Name, MdiObj.MnuDyeingReceive.Text)

        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmStockEntry", NCat_DyeingReceive, "DglMain", AgTemplate.TempTransaction1.hcSite_Code, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmStockEntry", NCat_DyeingReceive, "DglMain", AgTemplate.TempTransaction1.hcV_Type, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmStockEntry", NCat_DyeingReceive, "DglMain", AgTemplate.TempTransaction1.hcV_Date, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmStockEntry", NCat_DyeingReceive, "DglMain", AgTemplate.TempTransaction1.hcV_No, 0, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmStockEntry", NCat_DyeingReceive, "DglMain", AgTemplate.TempTransaction1.hcReferenceNo, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmStockEntry", NCat_DyeingReceive, "DglMain", AgTemplate.TempTransaction1.hcSettingGroup, 0, 0, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmStockEntry", NCat_DyeingReceive, "DglMain", FrmStockEntry.hcProcess, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmStockEntry", NCat_DyeingReceive, "DglMain", FrmStockEntry.hcParty, 1, 1, 1)


        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmStockEntry", NCat_DyeingReceive, "Dgl2", FrmStockEntry.hcPartyDocNo, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmStockEntry", NCat_DyeingReceive, "Dgl2", FrmStockEntry.hcPartyDocDate, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmStockEntry", NCat_DyeingReceive, "Dgl2", FrmStockEntry.hcTransporter, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmStockEntry", NCat_DyeingReceive, "Dgl2", FrmStockEntry.hcResponsiblePerson, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmStockEntry", NCat_DyeingReceive, "Dgl2", FrmStockEntry.hcGodown, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmStockEntry", NCat_DyeingReceive, "Dgl2", FrmStockEntry.hcReason, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmStockEntry", NCat_DyeingReceive, "Dgl2", FrmStockEntry.hcRemarks, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmStockEntry", NCat_DyeingReceive, "Dgl2", FrmStockEntry.hcRemarks1, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmStockEntry", NCat_DyeingReceive, "Dgl2", FrmStockEntry.hcRemarks2, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmStockEntry", NCat_DyeingReceive, "Dgl2", FrmStockEntry.hcBtnPendingPurchOrder, 1)


        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", NCat_DyeingReceive, "Dgl1", FrmStockEntry.ColSNo, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", NCat_DyeingReceive, "Dgl1", FrmStockEntry.Col1Barcode, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", NCat_DyeingReceive, "Dgl1", FrmStockEntry.Col1ItemCategory, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", NCat_DyeingReceive, "Dgl1", FrmStockEntry.Col1ItemGroup, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", NCat_DyeingReceive, "Dgl1", FrmStockEntry.Col1Item, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", NCat_DyeingReceive, "Dgl1", FrmStockEntry.Col1Dimension4, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", NCat_DyeingReceive, "Dgl1", FrmStockEntry.Col1Specification, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", NCat_DyeingReceive, "Dgl1", FrmStockEntry.Col1ItemState, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", NCat_DyeingReceive, "Dgl1", FrmStockEntry.Col1BaleNo, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", NCat_DyeingReceive, "Dgl1", FrmStockEntry.Col1LotNo, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", NCat_DyeingReceive, "Dgl1", FrmStockEntry.Col1ReferenceDocId, True,, "Order No")
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", NCat_DyeingReceive, "Dgl1", FrmStockEntry.Col1Godown, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", NCat_DyeingReceive, "Dgl1", FrmStockEntry.Col1Qty, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", NCat_DyeingReceive, "Dgl1", FrmStockEntry.Col1Unit, True, False,, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", NCat_DyeingReceive, "Dgl1", FrmStockEntry.Col1Pcs, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", NCat_DyeingReceive, "Dgl1", FrmStockEntry.Col1DealQty, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", NCat_DyeingReceive, "Dgl1", FrmStockEntry.Col1DealUnit, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", NCat_DyeingReceive, "Dgl1", FrmStockEntry.Col1Rate, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", NCat_DyeingReceive, "Dgl1", FrmStockEntry.Col1Amount, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", NCat_DyeingReceive, "Dgl1", FrmStockEntry.Col1Remark, True)


        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", NCat_DyeingReceive, ClsMain.SettingFields.FilterInclude_Process,
            "+" + Process_Dyeing, ClsMain.AgDataType.Text, "255",
            "SELECT Code, Name From viewHelpSubgroup Sg  With (NoLock) Where SubgroupType ='" & SubgroupType.Process & "' Order By Name",
            ClsMain.AgHelpQueryType.SqlQuery, ClsMain.AgHelpSelectionType.MultiSelect,,,,, ,, "+SUPPORT")

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", NCat_DyeingReceive, ClsMain.SettingFields.FilterInclude_SubgroupType,
            "+" + SubgroupType.Jobworker, ClsMain.AgDataType.Text, "255",
            "SELECT SubgroupType AS Code, SubgroupType AS Name  FROM SubGroupType ORDER BY SubgroupType",
            ClsMain.AgHelpQueryType.SqlQuery, ClsMain.AgHelpSelectionType.MultiSelect,,,,, ,, "+SUPPORT")

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", NCat_DyeingReceive, ClsMain.SettingFields.PostInStockYn,
            "1", ClsMain.AgDataType.YesNo, "50",,,,,,,, ,, "+SUPPORT")

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", NCat_DyeingReceive, ClsMain.SettingFields.PostInStockProcessYn,
            "1", ClsMain.AgDataType.YesNo, "50",,,,,,,, ,, "+SUPPORT")

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", NCat_DyeingReceive, ClsMain.SettingFields.FilterInclude_ItemType,
            "+" + ItemTypeCode.RawProduct, ClsMain.AgDataType.Text, "255",
            "SELECT Code, Name FROM ItemType Order By Name",
            ClsMain.AgHelpQueryType.SqlQuery, ClsMain.AgHelpSelectionType.MultiSelect,,,,, ,, "+SUPPORT")
    End Sub
    Private Sub FConfigure_DyeingInvoice(ClsObj As ClsMain)
        ClsObj.FSeedSingleIfNotExists_Voucher_Type(NCat_DyeingInvoice, "Dyeing Order", NCat_DyeingInvoice, VoucherCategory.Purchase, "", "Customised", MdiObj.MnuDyeingInvoice.Name, MdiObj.MnuDyeingInvoice.Text)

        mQry = "Update Voucher_Type SET Structure = 'GstPur' Where V_Type = '" & NCat_DyeingInvoice & "' And Structure Is Null "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)

        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_DyeingInvoice, "DglMain", AgTemplate.TempTransaction1.hcSite_Code, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_DyeingInvoice, "DglMain", AgTemplate.TempTransaction1.hcV_Type, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_DyeingInvoice, "DglMain", AgTemplate.TempTransaction1.hcV_Date, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_DyeingInvoice, "DglMain", AgTemplate.TempTransaction1.hcV_No, 0, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_DyeingInvoice, "DglMain", AgTemplate.TempTransaction1.hcReferenceNo, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_DyeingInvoice, "DglMain", AgTemplate.TempTransaction1.hcSettingGroup, 0, 0, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_DyeingInvoice, "DglMain", FrmPurchInvoiceDirect_WithDimension.hcProcess, 0, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_DyeingInvoice, "DglMain", FrmPurchInvoiceDirect_WithDimension.hcVendor, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_DyeingInvoice, "DglMain", FrmPurchInvoiceDirect_WithDimension.hcBillToParty, 1, 1)

        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_DyeingInvoice, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcVendorDocNo, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_DyeingInvoice, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcVendorDocDate, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_DyeingInvoice, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcAgent, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_DyeingInvoice, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcTags, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_DyeingInvoice, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcRemarks, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_DyeingInvoice, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcBtnTransportDetail, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_DyeingInvoice, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcBtnPendingPurchOrder, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_DyeingInvoice, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcBtnPendingStockReceive, 1)

        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_DyeingInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.ColSNo, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_DyeingInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCategory, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_DyeingInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemGroup, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_DyeingInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Item, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_DyeingInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension4, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_DyeingInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Specification, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_DyeingInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1SalesTaxGroup, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_DyeingInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DocQty, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_DyeingInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Unit, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_DyeingInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Rate, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_DyeingInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DiscountPer, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_DyeingInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DiscountAmount, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_DyeingInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionalDiscountPer, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_DyeingInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionalDiscountAmount, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_DyeingInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionPer, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_DyeingInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionAmount, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_DyeingInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Amount, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_DyeingInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Remark, True)

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", NCat_DyeingInvoice, ClsMain.SettingFields.FilterInclude_Process,
            "+" + Process_Dyeing, ClsMain.AgDataType.Text, "255",
            "SELECT Code, Name From viewHelpSubgroup Sg  With (NoLock) Where SubgroupType ='" & SubgroupType.Process & "' Order By Name",
            ClsMain.AgHelpQueryType.SqlQuery, ClsMain.AgHelpSelectionType.MultiSelect,,,,, ,, "+SUPPORT")

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", NCat_DyeingInvoice, ClsMain.SettingFields.FilterInclude_SubgroupType,
            "+" + SubgroupType.Jobworker, ClsMain.AgDataType.Text, "255",
            "SELECT SubgroupType AS Code, SubgroupType AS Name  FROM SubGroupType ORDER BY SubgroupType",
            ClsMain.AgHelpQueryType.SqlQuery, ClsMain.AgHelpSelectionType.MultiSelect,,,,, ,, "+SUPPORT")

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", NCat_DyeingInvoice, ClsMain.SettingFields.FilterInclude_ItemType,
            "+" + ItemTypeCode.RawProduct, ClsMain.AgDataType.Text, "255",
            "SELECT Code, Name FROM ItemType Order By Name",
            ClsMain.AgHelpQueryType.SqlQuery, ClsMain.AgHelpSelectionType.MultiSelect,,,,, ,, "+SUPPORT")

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", NCat_DyeingInvoice, ClsMain.SettingFields.PostInStockProcessYn,
            "1", ClsMain.AgDataType.YesNo, "50",,,,,,,, ,, "+SUPPORT")
    End Sub

    Private Sub FConfigure_DyeingPlan(ClsObj As ClsMain)
        ClsObj.FSeedSingleIfNotExists_Voucher_Type(NCat_DyeingPlan, "Dyeing Plan", NCat_DyeingPlan, VoucherCategory.Plan)

        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchPlan", NCat_DyeingPlan, "DglMain", AgTemplate.TempTransaction1.hcSite_Code, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchPlan", NCat_DyeingPlan, "DglMain", AgTemplate.TempTransaction1.hcV_Type, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchPlan", NCat_DyeingPlan, "DglMain", AgTemplate.TempTransaction1.hcV_Date, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchPlan", NCat_DyeingPlan, "DglMain", AgTemplate.TempTransaction1.hcV_No, 0, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchPlan", NCat_DyeingPlan, "DglMain", AgTemplate.TempTransaction1.hcReferenceNo, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchPlan", NCat_DyeingPlan, "DglMain", AgTemplate.TempTransaction1.hcSettingGroup, 0, 0, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchPlan", NCat_DyeingPlan, "DglMain", FrmPurchPlan.hcProcess, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchPlan", NCat_DyeingPlan, "DglMain", FrmPurchPlan.hcResponsiblePerson, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchPlan", NCat_DyeingPlan, "DglMain", FrmPurchPlan.hcRemarks, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchPlan", NCat_DyeingPlan, "DglMain", FrmPurchPlan.hcRemarks1, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchPlan", NCat_DyeingPlan, "DglMain", FrmPurchPlan.hcRemarks2, 0)


        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchPlan", NCat_DyeingPlan, "Dgl1", FrmPurchPlan.Col1Process, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchPlan", NCat_DyeingPlan, "Dgl1", FrmPurchPlan.Col1ItemCategory, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchPlan", NCat_DyeingPlan, "Dgl1", FrmPurchPlan.Col1ItemGroup, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchPlan", NCat_DyeingPlan, "Dgl1", FrmPurchPlan.Col1ItemCode, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchPlan", NCat_DyeingPlan, "Dgl1", FrmPurchPlan.Col1Item, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchPlan", NCat_DyeingPlan, "Dgl1", FrmPurchPlan.Col1Dimension4, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchPlan", NCat_DyeingPlan, "Dgl1", FrmPurchPlan.Col1Specification, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchPlan", NCat_DyeingPlan, "Dgl1", FrmPurchPlan.Col1Qty, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchPlan", NCat_DyeingPlan, "Dgl1", FrmPurchPlan.Col1Unit, True, False,, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchPlan", NCat_DyeingPlan, "Dgl1", FrmPurchPlan.Col1DealQty, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchPlan", NCat_DyeingPlan, "Dgl1", FrmPurchPlan.Col1DealUnit, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchPlan", NCat_DyeingPlan, "Dgl1", FrmPurchPlan.Col1Remark, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchPlan", NCat_DyeingPlan, "Dgl1", FrmPurchPlan.Col1BtnBasePlanDetail, True)


        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchPlanBase", NCat_DyeingPlan, "Dgl1", FrmPurchPlanBase.Col1BasePlanNo, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchPlanBase", NCat_DyeingPlan, "Dgl1", FrmPurchPlanBase.Col1BasePlanItemCategory, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchPlanBase", NCat_DyeingPlan, "Dgl1", FrmPurchPlanBase.Col1BasePlanItemGroup, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchPlanBase", NCat_DyeingPlan, "Dgl1", FrmPurchPlanBase.Col1BasePlanItem, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchPlanBase", NCat_DyeingPlan, "Dgl1", FrmPurchPlanBase.Col1BasePlanDimension1, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchPlanBase", NCat_DyeingPlan, "Dgl1", FrmPurchPlanBase.Col1BasePlanDimension2, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchPlanBase", NCat_DyeingPlan, "Dgl1", FrmPurchPlanBase.Col1BasePlanDimension3, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchPlanBase", NCat_DyeingPlan, "Dgl1", FrmPurchPlanBase.Col1BasePlanQty, True)

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", NCat_DyeingPlan, ClsMain.SettingFields.FilterInclude_ItemType,
            "+" + ItemTypeCode.RawProduct, ClsMain.AgDataType.Text, "255",
            "SELECT Code, Name FROM ItemType Order By Name",
            ClsMain.AgHelpQueryType.SqlQuery, ClsMain.AgHelpSelectionType.MultiSelect,,,,, ,, "+SUPPORT")
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", NCat_DyeingPlan, ClsMain.SettingFields.FilterInclude_ItemV_Type,
            "", ClsMain.AgDataType.Text, "255",
              "ItemV_Type", ClsMain.AgHelpQueryType.ClassName, ClsMain.AgHelpSelectionType.MultiSelect,,,,, ,, "+SUPPORT")
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", NCat_DyeingPlan, ClsMain.SettingFields.FilterInclude_ItemGroup,
            "", ClsMain.AgDataType.Text, "255",
            "SELECT Code, Description FROM ItemGroup Order By Description",
            ClsMain.AgHelpQueryType.SqlQuery, ClsMain.AgHelpSelectionType.MultiSelect,,,,, ,, "+SUPPORT")
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", NCat_DyeingPlan, ClsMain.SettingFields.FilterInclude_Item,
            "", ClsMain.AgDataType.Text, "255",
            "SELECT Code, Description FROM Item Order By Description",
            ClsMain.AgHelpQueryType.SqlQuery, ClsMain.AgHelpSelectionType.MultiSelect,,,,, ,, "+SUPPORT")
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", NCat_DyeingPlan, ClsMain.SettingFields.ActionOnDuplicateItem,
            ActionOnDuplicateItem.DoNothing, ClsMain.AgDataType.Text, "255",
            "ActionOnDuplicateItem", ClsMain.AgHelpQueryType.ClassName, ClsMain.AgHelpSelectionType.SingleSelect,,,,, ,, "+SUPPORT")
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", NCat_DyeingPlan, ClsMain.SettingFields.FilterInclude_Process,
            "+" + Process_Dyeing, ClsMain.AgDataType.Text, "255",
            "SELECT Code, Name From viewHelpSubgroup Sg  With (NoLock) Where SubgroupType ='" & SubgroupType.Process & "' Order By Name",
            ClsMain.AgHelpQueryType.SqlQuery, ClsMain.AgHelpSelectionType.MultiSelect,,,,, ,, "+SUPPORT")
    End Sub
    Private Sub FConfigure_SaleOrder(ClsObj As ClsMain)
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmSaleInvoiceDirect", "", Ncat.SaleOrder, "", "", "", "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1ItemCategory, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmSaleInvoiceDirect", "", Ncat.SaleOrder, "", "", "", "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Dimension1, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmSaleInvoiceDirect", "", Ncat.SaleOrder, "", "", "", "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Dimension2, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmSaleInvoiceDirect", "", Ncat.SaleOrder, "", "", "", "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Dimension3, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmSaleInvoiceDirect", "", Ncat.SaleOrder, "", "", "", "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Size, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmSaleInvoiceDirect", "", Ncat.SaleOrder, "", "", "", "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Item, 0, 0, 1, "")

        mQry = " UPDATE Setting SET Value = '" & "+" + ItemTypeCode.ManufacturingProduct & "' 
                            WHERE NCat = '" & Ncat.SaleOrder & "' AND FieldName = 'Filter Include Item Type' "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
    End Sub
    Private Sub FConfigure_Shapes(ClsObj As ClsMain)
        'If AgL.VNull(AgL.Dman_Execute("SELECT Count(*) AS Cnt FROM Item WHERE Code = 'RECT'", AgL.GCn).ExecuteScalar()) = 0 Then
        '    mQry = "INSERT INTO Shape (Code, Description, AreaFormula, PerimeterFormula, EntryBy, EntryDate, Status, Div_Code)
        '            VALUES ('RECT', 'Rectangle', '<LENGTH>*<WIDTH>', '2*(<LENGTH>+<WIDTH>)', 'SUPER', " & AgL.Chk_Date(AgL.PubLoginDate) & ", 'Active', 'D')"
        '    AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        'End If

        'If AgL.VNull(AgL.Dman_Execute("SELECT Count(*) AS Cnt FROM Item WHERE Code = 'Circle'", AgL.GCn).ExecuteScalar()) = 0 Then
        '    mQry = "INSERT INTO Shape (Code, Description, AreaFormula, PerimeterFormula, EntryBy, EntryDate, Status, Div_Code)
        '            VALUES ('Circle', 'Circle', NULL, NULL, 'SUPER', " & AgL.Chk_Date(AgL.PubLoginDate) & ", 'Active', 'D')"
        '    AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        'End If

        If AgL.VNull(AgL.Dman_Execute("SELECT Count(*) AS Cnt FROM Shape WHERE Code = 'Other'", AgL.GCn).ExecuteScalar()) = 0 Then
            mQry = "INSERT INTO Shape (Code, Description, AreaFormula, PerimeterFormula, EntryBy, EntryDate, Status, Div_Code)
                                VALUES ('Other', 'Other', NULL, NULL, 'SUPER', " & AgL.Chk_Date(AgL.PubLoginDate) & ", 'Active', 'D')"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If
    End Sub
    Private Sub FConfigure_Size(ClsObj As ClsMain)
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmItemMaster", "", "", ItemV_Type.SIZE, "", "", "Dgl1", FrmItemMaster.hcThickness, 0, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmItemMaster", "", "", ItemV_Type.SIZE, "", "", "Dgl1", FrmItemMaster.hcShape, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmItemMaster", "", "", ItemV_Type.SIZE, "", "", "Dgl1", FrmItemMaster.hcSizeUnit, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmItemMaster", "", "", ItemV_Type.SIZE, "", "", "Dgl1", FrmItemMaster.hcLength, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmItemMaster", "", "", ItemV_Type.SIZE, "", "", "Dgl1", FrmItemMaster.hcWidth, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmItemMaster", "", "", ItemV_Type.SIZE, "", "", "Dgl1", FrmItemMaster.hcArea, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmItemMaster", "", "", ItemV_Type.SIZE, "", "", "Dgl1", FrmItemMaster.hcPerimeter, 1, 0, 1, "")
    End Sub
    Private Sub FConfigure_SaleEnquiry(ClsObj As ClsMain)
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmSaleEnquiry", "", Ncat.SaleEnquiry, "", "", "", "Dgl1", FrmSaleEnquiry.Col1PartyItem, 1, 0, 1, "Party Design")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmSaleEnquiry", "", Ncat.SaleEnquiry, "", "", "", "Dgl1", FrmSaleEnquiry.Col1PartyItemSpecification1, 1, 0, 1, "Party Colour")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmSaleEnquiry", "", Ncat.SaleEnquiry, "", "", "", "Dgl1", FrmSaleEnquiry.Col1PartyItemSpecification2, 1, 0, 1, "Party Quality")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmSaleEnquiry", "", Ncat.SaleEnquiry, "", "", "", "Dgl1", FrmSaleEnquiry.Col1PartyItemSpecification3, 1, 0, 1, "Party Size")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmSaleEnquiry", "", Ncat.SaleEnquiry, "", "", "", "Dgl1", FrmSaleEnquiry.Col1ItemCategory, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmSaleEnquiry", "", Ncat.SaleEnquiry, "", "", "", "Dgl1", FrmSaleEnquiry.Col1Dimension1, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmSaleEnquiry", "", Ncat.SaleEnquiry, "", "", "", "Dgl1", FrmSaleEnquiry.Col1Dimension2, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmSaleEnquiry", "", Ncat.SaleEnquiry, "", "", "", "Dgl1", FrmSaleEnquiry.Col1Dimension3, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmSaleEnquiry", "", Ncat.SaleEnquiry, "", "", "", "Dgl1", FrmSaleEnquiry.Col1Size, 1, 0, 1, "")

        mQry = " UPDATE Setting SET Value = '" & "+" + ItemTypeCode.ManufacturingProduct & "' 
                            WHERE NCat = '" & Ncat.SaleEnquiry & "' AND FieldName = 'Filter Include Item Type' "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
    End Sub
    Private Sub FConfigure_FinishedMaterialPlan(ClsObj As ClsMain)
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchPlan", "", Ncat.FinishedMaterialPlan, "", "", "", "Dgl1", FrmPurchPlan.Col1Item, 0, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchPlan", "", Ncat.FinishedMaterialPlan, "", "", "", "Dgl1", FrmPurchPlan.Col1ItemCategory, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchPlan", "", Ncat.FinishedMaterialPlan, "", "", "", "Dgl1", FrmPurchPlan.Col1Dimension1, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchPlan", "", Ncat.FinishedMaterialPlan, "", "", "", "Dgl1", FrmPurchPlan.Col1Dimension2, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchPlan", "", Ncat.FinishedMaterialPlan, "", "", "", "Dgl1", FrmPurchPlan.Col1Dimension3, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchPlan", "", Ncat.FinishedMaterialPlan, "", "", "", "Dgl1", FrmPurchPlan.Col1Size, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchPlanBase", "", Ncat.FinishedMaterialPlan, "", "", "", "Dgl1", FrmPurchPlanBase.Col1BasePlanItem, 0, 0, 1, "")

        mQry = " UPDATE Setting SET Value = '" & "+" + Process_Weaving + "+" + Process.Purchase + "+" + Process.Stock & "' 
                            WHERE NCat = '" & Ncat.FinishedMaterialPlan & "' AND FieldName = 'Filter Include Process' "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
    End Sub

    Private Sub FConfigure_Process(ClsObj As ClsMain)
        ClsObj.FSeedSingleIfNotExist_Subgroup(Process_Weaving, "Weaving", SubgroupType.Process, "0028", "R", "Expense", "", "", AgL.PubDivCode, AgL.PubSiteCode, "", "System Defined")
        ClsObj.FSeedSingleIfNotExist_Subgroup(Process_Dyeing, "Dyeing", SubgroupType.Process, "0028", "R", "Expense", "", "", AgL.PubDivCode, AgL.PubSiteCode, "", "System Defined")
        ClsObj.FSeedSingleIfNotExist_Subgroup(Process_Finishing, "Finishing", SubgroupType.Process, "0028", "R", "Expense", "", "", AgL.PubDivCode, AgL.PubSiteCode, "", "System Defined")

        ClsObj.FSeedSingleIfNotExist_Subgroup(Process_Washing, "Washing", SubgroupType.Process, "0028", "R", "Expense", "", Process_Finishing, AgL.PubDivCode, AgL.PubSiteCode, "", "System Defined")
        ClsObj.FSeedSingleIfNotExist_Subgroup(Process_Binding, "Binding", SubgroupType.Process, "0028", "R", "Expense", "", Process_Finishing, AgL.PubDivCode, AgL.PubSiteCode, "", "System Defined")
        ClsObj.FSeedSingleIfNotExist_Subgroup(Process_ThirdBacking, "Third Backing", SubgroupType.Process, "0028", "R", "Expense", "", Process_Finishing, AgL.PubDivCode, AgL.PubSiteCode, "", "System Defined")

        mQry = " UPDATE SubGroup Set Status = 'InActive' Where SubCode = '" & Process.Production & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        mQry = " UPDATE SubGroup Set Status = 'InActive' Where SubCode = '" & Process_Finishing & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
    End Sub
    Private Sub FConfigure_ItemTypes(ClsObj As ClsMain)
        mQry = "Update ItemType Set Name ='CARPET' Where Code = '" & ItemTypeCode.ManufacturingProduct & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        mQry = "Update ItemType Set Name ='Yarn' Where Code = '" & ItemTypeCode.RawProduct & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)

        mQry = "Update Setting Set Value ='Item Category' Where FieldName = '" & ClsMain.SettingFields.ContraWindowBaseField & "' And NCat = '" & ItemTypeCode.ManufacturingProduct & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)

        mQry = "Update Setting Set Value ='Dimension4' 
                Where FieldName = '" & ClsMain.SettingFields.ContraWindowBaseField & "' 
                And NCat = '" & ItemTypeCode.RawProduct & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
    End Sub
    Private Sub FConfigure_Dimensions(ClsObj As ClsMain)
        mQry = "Update Setting Set Value ='Design' Where FieldName = '" & ClsMain.SettingFields.Dimension1Caption & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        mQry = "Update Setting Set Value ='Colour' Where FieldName = '" & ClsMain.SettingFields.Dimension2Caption & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        mQry = "Update Setting Set Value ='Quality' Where FieldName = '" & ClsMain.SettingFields.Dimension3Caption & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        mQry = "Update Setting Set Value ='Shade' Where FieldName = '" & ClsMain.SettingFields.Dimension4Caption & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
    End Sub
    Private Sub FConfigure_ItemCategory(ClsObj As ClsMain)
        ClsObj.FSeedSingleIfNotExist_Item("TUFTED", "TUFTED", "PCS", "", "", ItemTypeCode.ManufacturingProduct, ItemV_Type.ItemCategory, "5702", AgL.PubDivCode, "GST 5%", True, "", "")
    End Sub
    Private Sub FConfigure_ItemGroup(ClsObj As ClsMain)
        ClsObj.FSeedSingleIfNotExist_Item("2.0K", "2.0K", "", "", "TUFTED", ItemTypeCode.ManufacturingProduct, ItemV_Type.ItemGroup, "", AgL.PubDivCode, "", True, "", "")
    End Sub
    Private Sub FConfigure_FinishingOrder(ClsObj As ClsMain)
        ClsObj.FSeedSingleIfNotExists_Voucher_Type(NCat_FinishingOrder, "Finishing Order", NCat_FinishingOrder, VoucherCategory.Purchase, "", "Customised", MdiObj.MnuFinishingOrder.Name, MdiObj.MnuFinishingOrder.Text)

        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_FinishingOrder, "DglMain", AgTemplate.TempTransaction1.hcSite_Code, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_FinishingOrder, "DglMain", AgTemplate.TempTransaction1.hcV_Type, 1, 1, 1, "Order Type")
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_FinishingOrder, "DglMain", AgTemplate.TempTransaction1.hcV_Date, 1, 1, 1, "Order Date")
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_FinishingOrder, "DglMain", AgTemplate.TempTransaction1.hcV_No, 0, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_FinishingOrder, "DglMain", AgTemplate.TempTransaction1.hcReferenceNo, 1, 1, 1, "Order No")
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_FinishingOrder, "DglMain", AgTemplate.TempTransaction1.hcSettingGroup, 0, 0, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_FinishingOrder, "DglMain", FrmPurchInvoiceDirect_WithDimension.hcProcess, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_FinishingOrder, "DglMain", FrmPurchInvoiceDirect_WithDimension.hcVendor, 1, 1, 1, "Job Worker")
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_FinishingOrder, "DglMain", FrmPurchInvoiceDirect_WithDimension.hcBillToParty, 0, 0)

        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_FinishingOrder, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcVendorDocNo, 1, 0, 0, "Party Doc No")
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_FinishingOrder, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcVendorDocDate, 1, 0, 0, "Party Doc Date")
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_FinishingOrder, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcDeliveryDate, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_FinishingOrder, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcAgent, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_FinishingOrder, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcTags, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_FinishingOrder, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcRemarks, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_FinishingOrder, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcBtnTransportDetail, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_FinishingOrder, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcBtnPendingPurchPlan, 0)


        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_FinishingOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.ColSNo, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_FinishingOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Barcode, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_FinishingOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCategory, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_FinishingOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension1, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_FinishingOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension2, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_FinishingOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension3, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_FinishingOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Size, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_FinishingOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Specification, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_FinishingOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1SalesTaxGroup, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_FinishingOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DocQty, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_FinishingOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Unit, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_FinishingOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1UnitMultiplier, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_FinishingOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DealQty, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_FinishingOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DealUnit, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_FinishingOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Rate, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_FinishingOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DiscountPer, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_FinishingOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DiscountAmount, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_FinishingOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionalDiscountPer, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_FinishingOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionalDiscountAmount, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_FinishingOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionPer, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_FinishingOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionAmount, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_FinishingOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Amount, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_FinishingOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Remark, True)

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", NCat_FinishingOrder, ClsMain.SettingFields.FilterInclude_Process,
            "+" + Process_Finishing, ClsMain.AgDataType.Text, "255",
            "SELECT Code, Name From viewHelpSubgroup Sg  With (NoLock) Where SubgroupType ='" & SubgroupType.Process & "' Order By Name",
            ClsMain.AgHelpQueryType.SqlQuery, ClsMain.AgHelpSelectionType.MultiSelect,,,,, ,, "+SUPPORT")

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", NCat_FinishingOrder, ClsMain.SettingFields.FilterInclude_SubgroupType,
            "+" + SubgroupType.Jobworker, ClsMain.AgDataType.Text, "255",
            "SELECT SubgroupType AS Code, SubgroupType AS Name  FROM SubGroupType ORDER BY SubgroupType",
            ClsMain.AgHelpQueryType.SqlQuery, ClsMain.AgHelpSelectionType.MultiSelect,,,,, ,, "+SUPPORT")

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", NCat_FinishingOrder, ClsMain.SettingFields.BarcodeGunInputYn,
                        "1", ClsMain.AgDataType.YesNo, "50",,,,,,,, ,, "+SUPPORT")

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", NCat_FinishingOrder, ClsMain.SettingFields.PostInStockYn,
                "1", ClsMain.AgDataType.YesNo, "50",,,,,,,, ,, "+SUPPORT")
    End Sub
    Private Sub FConfigure_FinishingReceive(ClsObj As ClsMain)
        ClsObj.FSeedSingleIfNotExists_Voucher_Type(NCat_FinishingReceive, "Finishing Receive", NCat_FinishingReceive, VoucherCategory.Purchase, "", "Customised", MdiObj.MnuFinishingReceive.Name, MdiObj.MnuFinishingReceive.Text)

        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmStockEntry", NCat_FinishingReceive, "DglMain", AgTemplate.TempTransaction1.hcSite_Code, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmStockEntry", NCat_FinishingReceive, "DglMain", AgTemplate.TempTransaction1.hcV_Type, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmStockEntry", NCat_FinishingReceive, "DglMain", AgTemplate.TempTransaction1.hcV_Date, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmStockEntry", NCat_FinishingReceive, "DglMain", AgTemplate.TempTransaction1.hcV_No, 0, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmStockEntry", NCat_FinishingReceive, "DglMain", AgTemplate.TempTransaction1.hcReferenceNo, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmStockEntry", NCat_FinishingReceive, "DglMain", AgTemplate.TempTransaction1.hcSettingGroup, 0, 0, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmStockEntry", NCat_FinishingReceive, "DglMain", FrmStockEntry.hcProcess, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmStockEntry", NCat_FinishingReceive, "DglMain", FrmStockEntry.hcParty, 1, 1, 1)


        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmStockEntry", NCat_FinishingReceive, "Dgl2", FrmStockEntry.hcPartyDocNo, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmStockEntry", NCat_FinishingReceive, "Dgl2", FrmStockEntry.hcPartyDocDate, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmStockEntry", NCat_FinishingReceive, "Dgl2", FrmStockEntry.hcTransporter, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmStockEntry", NCat_FinishingReceive, "Dgl2", FrmStockEntry.hcResponsiblePerson, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmStockEntry", NCat_FinishingReceive, "Dgl2", FrmStockEntry.hcGodown, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmStockEntry", NCat_FinishingReceive, "Dgl2", FrmStockEntry.hcReason, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmStockEntry", NCat_FinishingReceive, "Dgl2", FrmStockEntry.hcRemarks, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmStockEntry", NCat_FinishingReceive, "Dgl2", FrmStockEntry.hcRemarks1, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmStockEntry", NCat_FinishingReceive, "Dgl2", FrmStockEntry.hcRemarks2, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmStockEntry", NCat_FinishingReceive, "Dgl2", FrmStockEntry.hcBtnPendingPurchOrder, 1)


        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", NCat_FinishingReceive, "Dgl1", FrmStockEntry.ColSNo, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", NCat_FinishingReceive, "Dgl1", FrmStockEntry.Col1Barcode, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", NCat_FinishingReceive, "Dgl1", FrmStockEntry.Col1ItemCategory, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", NCat_FinishingReceive, "Dgl1", FrmStockEntry.Col1ItemGroup, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", NCat_FinishingReceive, "Dgl1", FrmStockEntry.Col1ItemCode, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", NCat_FinishingReceive, "Dgl1", FrmStockEntry.Col1Dimension1, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", NCat_FinishingReceive, "Dgl1", FrmStockEntry.Col1Dimension2, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", NCat_FinishingReceive, "Dgl1", FrmStockEntry.Col1Dimension3, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", NCat_FinishingReceive, "Dgl1", FrmStockEntry.Col1Size, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", NCat_FinishingReceive, "Dgl1", FrmStockEntry.Col1Specification, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", NCat_FinishingReceive, "Dgl1", FrmStockEntry.Col1ItemState, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", NCat_FinishingReceive, "Dgl1", FrmStockEntry.Col1BaleNo, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", NCat_FinishingReceive, "Dgl1", FrmStockEntry.Col1LotNo, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", NCat_FinishingReceive, "Dgl1", FrmStockEntry.Col1Godown, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", NCat_FinishingReceive, "Dgl1", FrmStockEntry.Col1Qty, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", NCat_FinishingReceive, "Dgl1", FrmStockEntry.Col1Unit, True, False,, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", NCat_FinishingReceive, "Dgl1", FrmStockEntry.Col1Pcs, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", NCat_FinishingReceive, "Dgl1", FrmStockEntry.Col1UnitMultiplier, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", NCat_FinishingReceive, "Dgl1", FrmStockEntry.Col1DealQty, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", NCat_FinishingReceive, "Dgl1", FrmStockEntry.Col1DealUnit, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", NCat_FinishingReceive, "Dgl1", FrmStockEntry.Col1Rate, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", NCat_FinishingReceive, "Dgl1", FrmStockEntry.Col1Amount, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", NCat_FinishingReceive, "Dgl1", FrmStockEntry.Col1Remark, True)

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", NCat_FinishingReceive, ClsMain.SettingFields.FilterInclude_Process,
            "+" + Process_Finishing, ClsMain.AgDataType.Text, "255",
            "SELECT Code, Name From viewHelpSubgroup Sg  With (NoLock) Where SubgroupType ='" & SubgroupType.Process & "' Order By Name",
            ClsMain.AgHelpQueryType.SqlQuery, ClsMain.AgHelpSelectionType.MultiSelect,,,,, ,, "+SUPPORT")

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", NCat_FinishingReceive, ClsMain.SettingFields.FilterInclude_SubgroupType,
            "+" + SubgroupType.Jobworker, ClsMain.AgDataType.Text, "255",
            "SELECT SubgroupType AS Code, SubgroupType AS Name  FROM SubGroupType ORDER BY SubgroupType",
            ClsMain.AgHelpQueryType.SqlQuery, ClsMain.AgHelpSelectionType.MultiSelect,,,,, ,, "+SUPPORT")

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", NCat_FinishingReceive, ClsMain.SettingFields.BarcodeGunInputYn,
                "1", ClsMain.AgDataType.YesNo, "50",,,,,,,, ,, "+SUPPORT")

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", NCat_FinishingReceive, ClsMain.SettingFields.PostInStockYn,
                "1", ClsMain.AgDataType.YesNo, "50",,,,,,,, ,, "+SUPPORT")

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", NCat_FinishingReceive, ClsMain.SettingFields.PostInStockProcessYn,
                "1", ClsMain.AgDataType.YesNo, "50",,,,,,,, ,, "+SUPPORT")
    End Sub
    Private Sub FConfigure_FinishingInvoice(ClsObj As ClsMain)
        ClsObj.FSeedSingleIfNotExists_Voucher_Type(NCat_FinishingInvoice, "Finishing Invoice", NCat_FinishingInvoice, VoucherCategory.Purchase, "", "Customised", MdiObj.MnuFinishingInvoice.Name, MdiObj.MnuFinishingInvoice.Text)

        mQry = "Update Voucher_Type SET Structure = 'GstPur' Where V_Type = '" & NCat_FinishingInvoice & "' And Structure Is Null "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)

        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_FinishingInvoice, "DglMain", AgTemplate.TempTransaction1.hcSite_Code, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_FinishingInvoice, "DglMain", AgTemplate.TempTransaction1.hcV_Type, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_FinishingInvoice, "DglMain", AgTemplate.TempTransaction1.hcV_Date, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_FinishingInvoice, "DglMain", AgTemplate.TempTransaction1.hcV_No, 0, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_FinishingInvoice, "DglMain", AgTemplate.TempTransaction1.hcReferenceNo, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_FinishingInvoice, "DglMain", AgTemplate.TempTransaction1.hcSettingGroup, 0, 0, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_FinishingInvoice, "DglMain", FrmPurchInvoiceDirect_WithDimension.hcProcess, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_FinishingInvoice, "DglMain", FrmPurchInvoiceDirect_WithDimension.hcVendor, 1, 1, 1, "Job Worker")
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_FinishingInvoice, "DglMain", FrmPurchInvoiceDirect_WithDimension.hcBillToParty, 0, 1)

        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_FinishingInvoice, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcVendorDocNo, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_FinishingInvoice, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcVendorDocDate, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_FinishingInvoice, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcAgent, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_FinishingInvoice, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcTags, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_FinishingInvoice, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcRemarks, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_FinishingInvoice, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcBtnTransportDetail, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_FinishingInvoice, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcBtnPendingPurchPlan, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", NCat_FinishingInvoice, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcBtnPendingStockReceive, 1)

        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_FinishingInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.ColSNo, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_FinishingInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Barcode, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_FinishingInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCategory, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_FinishingInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension1, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_FinishingInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension2, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_FinishingInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension3, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_FinishingInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Size, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_FinishingInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Specification, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_FinishingInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1SalesTaxGroup, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_FinishingInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DocQty, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_FinishingInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Unit, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_FinishingInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1UnitMultiplier, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_FinishingInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DealQty, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_FinishingInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DealUnit, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_FinishingInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Rate, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_FinishingInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DiscountPer, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_FinishingInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DiscountAmount, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_FinishingInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionalDiscountPer, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_FinishingInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionalDiscountAmount, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_FinishingInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionPer, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_FinishingInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionAmount, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_FinishingInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Amount, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", NCat_FinishingInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Remark, True)

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", NCat_FinishingInvoice, ClsMain.SettingFields.FilterInclude_Process,
            "+" + Process_Finishing, ClsMain.AgDataType.Text, "255",
            "SELECT Code, Name From viewHelpSubgroup Sg  With (NoLock) Where SubgroupType ='" & SubgroupType.Process & "' Order By Name",
            ClsMain.AgHelpQueryType.SqlQuery, ClsMain.AgHelpSelectionType.MultiSelect,,,,, ,, "+SUPPORT")

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", NCat_FinishingInvoice, ClsMain.SettingFields.FilterInclude_SubgroupType,
            "+" + SubgroupType.Jobworker, ClsMain.AgDataType.Text, "255",
            "SELECT SubgroupType AS Code, SubgroupType AS Name  FROM SubGroupType ORDER BY SubgroupType",
            ClsMain.AgHelpQueryType.SqlQuery, ClsMain.AgHelpSelectionType.MultiSelect,,,,, ,, "+SUPPORT")

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", NCat_FinishingInvoice, ClsMain.SettingFields.BarcodeGunInputYn,
                "1", ClsMain.AgDataType.YesNo, "50",,,,,,,, ,, "+SUPPORT")

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", NCat_FinishingInvoice, ClsMain.SettingFields.PostInStockProcessYn,
            "1", ClsMain.AgDataType.YesNo, "50",,,,,,,, ,, "+SUPPORT")
    End Sub
    Private Sub FConfigure_Unit(ClsObj As ClsMain)
        If AgL.FillData("Select Count(*) from Unit Where Code = 'Sq.Meter' ", AgL.GcnMain).tables(0).Rows(0)(0) = 0 Then
            mQry = " INSERT INTO Unit
                    (Code, IsActive, DecimalPlaces,ShowDimensionDetailInPurchase, ShowDimensionDetailInSales,UQC)
                    VALUES('Sq.Meter', 1, 3,0,0,'SQM-SQUARE METERS');"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If
        If AgL.FillData("Select Count(*) from Unit Where Code = 'Sq.Yard' ", AgL.GcnMain).tables(0).Rows(0)(0) = 0 Then
            mQry = " INSERT INTO Unit
                    (Code, IsActive, DecimalPlaces,ShowDimensionDetailInPurchase, ShowDimensionDetailInSales,UQC)
                    VALUES('Sq.Yard', 1, 3,0,0,'SQY-SQUARE YARDS');"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If
        If AgL.FillData("Select Count(*) from Unit Where Code = 'Sq.Feet' ", AgL.GcnMain).tables(0).Rows(0)(0) = 0 Then
            mQry = " INSERT INTO Unit
                    (Code, IsActive, DecimalPlaces,ShowDimensionDetailInPurchase, ShowDimensionDetailInSales,UQC)
                    VALUES('Sq.Feet', 1, 3,0,0,'SQF-SQUARE FEET');"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If

        If AgL.FillData("Select * from StandardUnitConversion Where FromUnit = '" & ClsMain.UnitConstants.SqMeter & "' 
                        And ToUnit = '" & ClsMain.UnitConstants.SqFeet & "'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
            mQry = " INSERT INTO StandardUnitConversion (Code, FromUnit, ToUnit, Multiplier, Rounding, EntryBy, EntryDate)
                        Select '1' As Code, '" & ClsMain.UnitConstants.SqMeter & "' As FromUnit, 
                        '" & ClsMain.UnitConstants.SqFeet & "' As ToUnit, 10.764 As Multiplier, 
                        3 As Rounding, 'Super' As EntryBy, " & AgL.Chk_Date(AgL.PubLoginDate) & " EntryDate "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If

        If AgL.FillData("Select * from StandardUnitConversion Where FromUnit = '" & ClsMain.UnitConstants.SqFeet & "' 
                        And ToUnit = '" & ClsMain.UnitConstants.SqMeter & "'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
            mQry = " INSERT INTO StandardUnitConversion (Code, FromUnit, ToUnit, Multiplier, Rounding, EntryBy, EntryDate)
                        Select '2' As Code, '" & ClsMain.UnitConstants.SqFeet & "' As FromUnit, 
                        '" & ClsMain.UnitConstants.SqMeter & "' As ToUnit, 0.0929 As Multiplier, 
                        3 As Rounding, 'Super' As EntryBy, " & AgL.Chk_Date(AgL.PubLoginDate) & " EntryDate "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If
    End Sub
    Private Sub FConfigure_Bom(ClsObj As ClsMain)
        Dim mMaxId As String = ""
        If AgL.VNull(AgL.Dman_Execute("Select Count(*) From ItemBomPattern Where Item Is Null And Process Is Null", AgL.GCn).ExecuteScalar()) = 0 Then
            mMaxId = AgL.GetMaxId("ItemBomPattern", "Code", AgL.GcnMain, AgL.PubDivCode, AgL.PubSiteCode, 8, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)
            mQry = "INSERT INTO ItemBomPattern (Code, Item, Sr, Process, Pattern)
                    VALUES ('" & mMaxId & "', NULL, 1, Null, 'Item Category,Dimension1,Dimension2,Dimension3')"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        End If
    End Sub
    Private Sub FConfigure_WeavingReceive(ClsObj As ClsMain)
        ClsObj.FSeedSingleIfNotExists_Voucher_Type(NCat_WeavingReceive, "Weaving Receive", NCat_WeavingReceive, VoucherCategory.Purchase, "", "Customised", MdiObj.MnuWeavingReceive.Name, MdiObj.MnuWeavingReceive.Text)

        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmWeavingReceive", NCat_WeavingReceive, "DglMain", AgTemplate.TempTransaction1.hcSite_Code, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmWeavingReceive", NCat_WeavingReceive, "DglMain", AgTemplate.TempTransaction1.hcV_Type, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmWeavingReceive", NCat_WeavingReceive, "DglMain", AgTemplate.TempTransaction1.hcV_Date, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmWeavingReceive", NCat_WeavingReceive, "DglMain", AgTemplate.TempTransaction1.hcV_No, 0, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmWeavingReceive", NCat_WeavingReceive, "DglMain", AgTemplate.TempTransaction1.hcReferenceNo, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmWeavingReceive", NCat_WeavingReceive, "DglMain", AgTemplate.TempTransaction1.hcSettingGroup, 0, 0, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmWeavingReceive", NCat_WeavingReceive, "DglMain", FrmWeavingReceive.hcProcess, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmWeavingReceive", NCat_WeavingReceive, "DglMain", FrmWeavingReceive.hcSubCode, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmWeavingReceive", NCat_WeavingReceive, "DglMain", FrmWeavingReceive.hcGodown, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmWeavingReceive", NCat_WeavingReceive, "DglMain", FrmWeavingReceive.hcItemCategory, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmWeavingReceive", NCat_WeavingReceive, "DglMain", FrmWeavingReceive.hcDimension1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmWeavingReceive", NCat_WeavingReceive, "DglMain", FrmWeavingReceive.hcDimension2, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmWeavingReceive", NCat_WeavingReceive, "DglMain", FrmWeavingReceive.hcDimension3, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmWeavingReceive", NCat_WeavingReceive, "DglMain", FrmWeavingReceive.hcSize, 1, 1)

        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmWeavingReceive", NCat_WeavingReceive, "Dgl2", FrmWeavingReceive.hcBuyer, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmWeavingReceive", NCat_WeavingReceive, "Dgl2", FrmWeavingReceive.hcJobOrder, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmWeavingReceive", NCat_WeavingReceive, "Dgl2", FrmWeavingReceive.hcQty, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmWeavingReceive", NCat_WeavingReceive, "Dgl2", FrmWeavingReceive.hcUnit, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmWeavingReceive", NCat_WeavingReceive, "Dgl2", FrmWeavingReceive.hcUnitMultiplier, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmWeavingReceive", NCat_WeavingReceive, "Dgl2", FrmWeavingReceive.hcDealQty, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmWeavingReceive", NCat_WeavingReceive, "Dgl2", FrmWeavingReceive.hcDealUnit, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmWeavingReceive", NCat_WeavingReceive, "Dgl2", FrmWeavingReceive.hcBarcode, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmWeavingReceive", NCat_WeavingReceive, "Dgl2", FrmWeavingReceive.hcWeight, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmWeavingReceive", NCat_WeavingReceive, "Dgl2", FrmWeavingReceive.hcActualLength, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmWeavingReceive", NCat_WeavingReceive, "Dgl2", FrmWeavingReceive.hcActualWidth, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmWeavingReceive", NCat_WeavingReceive, "Dgl2", FrmWeavingReceive.hcRemarks, 1)

        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmWeavingReceive", NCat_WeavingReceive, "Dgl1", FrmWeavingReceive.ColSNo, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmWeavingReceive", NCat_WeavingReceive, "Dgl1", FrmWeavingReceive.Col1SubCode, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmWeavingReceive", NCat_WeavingReceive, "Dgl1", FrmWeavingReceive.Col1Amount, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmWeavingReceive", NCat_WeavingReceive, "Dgl1", FrmWeavingReceive.Col1Remark, True)

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", NCat_WeavingReceive, ClsMain.SettingFields.FilterInclude_Process,
            "+" + Process_Weaving, ClsMain.AgDataType.Text, "255",
            "SELECT Code, Name From viewHelpSubgroup Sg  With (NoLock) Where SubgroupType ='" & SubgroupType.Process & "' Order By Name",
            ClsMain.AgHelpQueryType.SqlQuery, ClsMain.AgHelpSelectionType.MultiSelect,,,,, ,, "+SUPPORT")

        If AgL.FillData("Select * from AcGroup Where GroupName='Penalty'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
            mQry = " 
                    Insert Into AcGroup (GroupCode, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE)
                    Values('0035', 'Penalty', 'Penalty', '0016','L', 'Supplier','N', 'SUPER','2018-03-01','A')
                    "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", NCat_WeavingReceive, ClsMain.SettingFields.FilterInclude_AcGroupLine,
                        "+0035", ClsMain.AgDataType.Text, "255",
                        "SELECT GroupCode AS Code, GroupName AS Name FROM AcGroup ORDER BY GroupName",
                        ClsMain.AgHelpQueryType.SqlQuery, ClsMain.AgHelpSelectionType.MultiSelect)

        AgL.AddFieldSqlite(AgL.GcnMain, "StockHeadDetail", "ActualLength", "Float", "0", True)
        AgL.AddFieldSqlite(AgL.GcnMain, "StockHeadDetail", "ActualWidth", "Float", "0", True)
    End Sub


    Private Sub FConfigure_DyeingOrderStatusReport(ClsObj As ClsMain)

        ClsMain.FSeedSingleIfNotExist_ReportHeaderUISetting(MdiObj.MnuDyeingOrderStatusReport.Text, "", "FilterGrid", ClsPurchOrderStatusReport.hcStatusType, 1, 0, 0, "")
        ClsMain.FSeedSingleIfNotExist_ReportHeaderUISetting(MdiObj.MnuDyeingOrderStatusReport.Text, "", "FilterGrid", ClsPurchOrderStatusReport.hcFromDate, 1, 0, 0, "")
        ClsMain.FSeedSingleIfNotExist_ReportHeaderUISetting(MdiObj.MnuDyeingOrderStatusReport.Text, "", "FilterGrid", ClsPurchOrderStatusReport.hcToDate, 1, 0, 0, "")
        ClsMain.FSeedSingleIfNotExist_ReportHeaderUISetting(MdiObj.MnuDyeingOrderStatusReport.Text, "", "FilterGrid", ClsPurchOrderStatusReport.hcParty, 1, 0, 0, "")
        ClsMain.FSeedSingleIfNotExist_ReportHeaderUISetting(MdiObj.MnuDyeingOrderStatusReport.Text, "", "FilterGrid", ClsPurchOrderStatusReport.hcSite, 1, 0, 0, "")
        ClsMain.FSeedSingleIfNotExist_ReportHeaderUISetting(MdiObj.MnuDyeingOrderStatusReport.Text, "", "FilterGrid", ClsPurchOrderStatusReport.hcVoucherType, 1, 0, 0, "")
        ClsMain.FSeedSingleIfNotExist_ReportHeaderUISetting(MdiObj.MnuDyeingOrderStatusReport.Text, "", "FilterGrid", ClsPurchOrderStatusReport.hcCashCredit, 1, 0, 0, "")
        ClsMain.FSeedSingleIfNotExist_ReportHeaderUISetting(MdiObj.MnuDyeingOrderStatusReport.Text, "", "FilterGrid", ClsPurchOrderStatusReport.hcItemCategory, 1, 0, 0, "")
        ClsMain.FSeedSingleIfNotExist_ReportHeaderUISetting(MdiObj.MnuDyeingOrderStatusReport.Text, "", "FilterGrid", ClsPurchOrderStatusReport.hcDimension4, 1, 0, 0, "")
        ClsMain.FSeedSingleIfNotExist_ReportHeaderUISetting(MdiObj.MnuDyeingOrderStatusReport.Text, "", "FilterGrid", ClsPurchOrderStatusReport.hcCity, 1, 0, 0, "")
        ClsMain.FSeedSingleIfNotExist_ReportHeaderUISetting(MdiObj.MnuDyeingOrderStatusReport.Text, "", "FilterGrid", ClsPurchOrderStatusReport.hcState, 1, 0, 0, "")
        ClsMain.FSeedSingleIfNotExist_ReportHeaderUISetting(MdiObj.MnuDyeingOrderStatusReport.Text, "", "FilterGrid", ClsPurchOrderStatusReport.hcSalesRepresentative, 1, 0, 0, "")
        ClsMain.FSeedSingleIfNotExist_ReportHeaderUISetting(MdiObj.MnuDyeingOrderStatusReport.Text, "", "FilterGrid", ClsPurchOrderStatusReport.hcResponsiblePerson, 1, 0, 0, "")
        ClsMain.FSeedSingleIfNotExist_ReportHeaderUISetting(MdiObj.MnuDyeingOrderStatusReport.Text, "", "FilterGrid", ClsPurchOrderStatusReport.hcTag, 1, 0, 0, "")
        ClsMain.FSeedSingleIfNotExist_ReportHeaderUISetting(MdiObj.MnuDyeingOrderStatusReport.Text, "", "FilterGrid", ClsPurchOrderStatusReport.hcDivision, 1, 0, 0, "")
        ClsMain.FSeedSingleIfNotExist_ReportHeaderUISetting(MdiObj.MnuDyeingOrderStatusReport.Text, "", "FilterGrid", ClsPurchOrderStatusReport.hcBalanceType, 1, 0, 0, "")


        ClsMain.FSeedSingleIfNotExist_ReportLineUISetting(MdiObj.MnuDyeingOrderStatusReport.Text, "", "Dgl1", ClsPurchOrderStatusReport.Col1Division, 1, 0, 0, "", 70)
        ClsMain.FSeedSingleIfNotExist_ReportLineUISetting(MdiObj.MnuDyeingOrderStatusReport.Text, "", "Dgl1", ClsPurchOrderStatusReport.Col1Site, 1, 0, 0, "", 60)
        ClsMain.FSeedSingleIfNotExist_ReportLineUISetting(MdiObj.MnuDyeingOrderStatusReport.Text, "", "Dgl1", ClsPurchOrderStatusReport.Col1OrderDate, 1, 0, 0, "", 100)
        ClsMain.FSeedSingleIfNotExist_ReportLineUISetting(MdiObj.MnuDyeingOrderStatusReport.Text, "", "Dgl1", ClsPurchOrderStatusReport.Col1OrderNo, 1, 0, 0, "", 90)
        ClsMain.FSeedSingleIfNotExist_ReportLineUISetting(MdiObj.MnuDyeingOrderStatusReport.Text, "", "Dgl1", ClsPurchOrderStatusReport.Col1Party, 1, 0, 0, "", 120)
        ClsMain.FSeedSingleIfNotExist_ReportLineUISetting(MdiObj.MnuDyeingOrderStatusReport.Text, "", "Dgl1", ClsPurchOrderStatusReport.Col1ItemCategory, 1, 0, 0, "", 105)
        ClsMain.FSeedSingleIfNotExist_ReportLineUISetting(MdiObj.MnuDyeingOrderStatusReport.Text, "", "Dgl1", ClsPurchOrderStatusReport.Col1Dimension4, 1, 0, 0, "", 100)
        ClsMain.FSeedSingleIfNotExist_ReportLineUISetting(MdiObj.MnuDyeingOrderStatusReport.Text, "", "Dgl1", ClsPurchOrderStatusReport.Col1OrderQty, 1, 0, 0, "", 80)
        ClsMain.FSeedSingleIfNotExist_ReportLineUISetting(MdiObj.MnuDyeingOrderStatusReport.Text, "", "Dgl1", ClsPurchOrderStatusReport.Col1OrderAmount, 1, 0, 0, "", 100)
        ClsMain.FSeedSingleIfNotExist_ReportLineUISetting(MdiObj.MnuDyeingOrderStatusReport.Text, "", "Dgl1", ClsPurchOrderStatusReport.Col1BalanceQty, 1, 0, 0, "", 80)
        ClsMain.FSeedSingleIfNotExist_ReportLineUISetting(MdiObj.MnuDyeingOrderStatusReport.Text, "", "Dgl1", ClsPurchOrderStatusReport.Col1BalanceAmount, 1, 0, 0, "", 100)
    End Sub

    Private Sub FConfigure_WeavingOrderStatusReport(ClsObj As ClsMain)

        ClsMain.FSeedSingleIfNotExist_ReportHeaderUISetting(MdiObj.MnuWeavingOrderStatusReport.Text, "", "FilterGrid", ClsPurchOrderStatusReport.hcStatusType, 1, 0, 0, "")
        ClsMain.FSeedSingleIfNotExist_ReportHeaderUISetting(MdiObj.MnuWeavingOrderStatusReport.Text, "", "FilterGrid", ClsPurchOrderStatusReport.hcFromDate, 1, 0, 0, "")
        ClsMain.FSeedSingleIfNotExist_ReportHeaderUISetting(MdiObj.MnuWeavingOrderStatusReport.Text, "", "FilterGrid", ClsPurchOrderStatusReport.hcToDate, 1, 0, 0, "")
        ClsMain.FSeedSingleIfNotExist_ReportHeaderUISetting(MdiObj.MnuWeavingOrderStatusReport.Text, "", "FilterGrid", ClsPurchOrderStatusReport.hcParty, 1, 0, 0, "")
        ClsMain.FSeedSingleIfNotExist_ReportHeaderUISetting(MdiObj.MnuWeavingOrderStatusReport.Text, "", "FilterGrid", ClsPurchOrderStatusReport.hcSite, 1, 0, 0, "")
        ClsMain.FSeedSingleIfNotExist_ReportHeaderUISetting(MdiObj.MnuWeavingOrderStatusReport.Text, "", "FilterGrid", ClsPurchOrderStatusReport.hcVoucherType, 1, 0, 0, "")
        ClsMain.FSeedSingleIfNotExist_ReportHeaderUISetting(MdiObj.MnuWeavingOrderStatusReport.Text, "", "FilterGrid", ClsPurchOrderStatusReport.hcCashCredit, 1, 0, 0, "")
        ClsMain.FSeedSingleIfNotExist_ReportHeaderUISetting(MdiObj.MnuWeavingOrderStatusReport.Text, "", "FilterGrid", ClsPurchOrderStatusReport.hcItemCategory, 1, 0, 0, "")
        ClsMain.FSeedSingleIfNotExist_ReportHeaderUISetting(MdiObj.MnuWeavingOrderStatusReport.Text, "", "FilterGrid", ClsPurchOrderStatusReport.hcDimension1, 1, 0, 0, "")
        ClsMain.FSeedSingleIfNotExist_ReportHeaderUISetting(MdiObj.MnuWeavingOrderStatusReport.Text, "", "FilterGrid", ClsPurchOrderStatusReport.hcDimension2, 1, 0, 0, "")
        ClsMain.FSeedSingleIfNotExist_ReportHeaderUISetting(MdiObj.MnuWeavingOrderStatusReport.Text, "", "FilterGrid", ClsPurchOrderStatusReport.hcDimension3, 1, 0, 0, "")
        ClsMain.FSeedSingleIfNotExist_ReportHeaderUISetting(MdiObj.MnuWeavingOrderStatusReport.Text, "", "FilterGrid", ClsPurchOrderStatusReport.hcSize, 1, 0, 0, "")
        ClsMain.FSeedSingleIfNotExist_ReportHeaderUISetting(MdiObj.MnuWeavingOrderStatusReport.Text, "", "FilterGrid", ClsPurchOrderStatusReport.hcCity, 1, 0, 0, "")
        ClsMain.FSeedSingleIfNotExist_ReportHeaderUISetting(MdiObj.MnuWeavingOrderStatusReport.Text, "", "FilterGrid", ClsPurchOrderStatusReport.hcState, 1, 0, 0, "")
        ClsMain.FSeedSingleIfNotExist_ReportHeaderUISetting(MdiObj.MnuWeavingOrderStatusReport.Text, "", "FilterGrid", ClsPurchOrderStatusReport.hcSalesRepresentative, 1, 0, 0, "")
        ClsMain.FSeedSingleIfNotExist_ReportHeaderUISetting(MdiObj.MnuWeavingOrderStatusReport.Text, "", "FilterGrid", ClsPurchOrderStatusReport.hcResponsiblePerson, 1, 0, 0, "")
        ClsMain.FSeedSingleIfNotExist_ReportHeaderUISetting(MdiObj.MnuWeavingOrderStatusReport.Text, "", "FilterGrid", ClsPurchOrderStatusReport.hcTag, 1, 0, 0, "")
        ClsMain.FSeedSingleIfNotExist_ReportHeaderUISetting(MdiObj.MnuWeavingOrderStatusReport.Text, "", "FilterGrid", ClsPurchOrderStatusReport.hcDivision, 1, 0, 0, "")
        ClsMain.FSeedSingleIfNotExist_ReportHeaderUISetting(MdiObj.MnuWeavingOrderStatusReport.Text, "", "FilterGrid", ClsPurchOrderStatusReport.hcBalanceType, 1, 0, 0, "")


        ClsMain.FSeedSingleIfNotExist_ReportLineUISetting(MdiObj.MnuWeavingOrderStatusReport.Text, "", "Dgl1", ClsPurchOrderStatusReport.Col1Division, 1, 0, 0, "", 70)
        ClsMain.FSeedSingleIfNotExist_ReportLineUISetting(MdiObj.MnuWeavingOrderStatusReport.Text, "", "Dgl1", ClsPurchOrderStatusReport.Col1Site, 1, 0, 0, "", 60)
        ClsMain.FSeedSingleIfNotExist_ReportLineUISetting(MdiObj.MnuWeavingOrderStatusReport.Text, "", "Dgl1", ClsPurchOrderStatusReport.Col1OrderDate, 1, 0, 0, "", 100)
        ClsMain.FSeedSingleIfNotExist_ReportLineUISetting(MdiObj.MnuWeavingOrderStatusReport.Text, "", "Dgl1", ClsPurchOrderStatusReport.Col1OrderNo, 1, 0, 0, "", 90)
        ClsMain.FSeedSingleIfNotExist_ReportLineUISetting(MdiObj.MnuWeavingOrderStatusReport.Text, "", "Dgl1", ClsPurchOrderStatusReport.Col1Party, 1, 0, 0, "", 120)
        ClsMain.FSeedSingleIfNotExist_ReportLineUISetting(MdiObj.MnuWeavingOrderStatusReport.Text, "", "Dgl1", ClsPurchOrderStatusReport.Col1ItemCategory, 1, 0, 0, "", 105)
        ClsMain.FSeedSingleIfNotExist_ReportLineUISetting(MdiObj.MnuWeavingOrderStatusReport.Text, "", "Dgl1", ClsPurchOrderStatusReport.Col1Dimension1, 1, 0, 0, "", 100)
        ClsMain.FSeedSingleIfNotExist_ReportLineUISetting(MdiObj.MnuWeavingOrderStatusReport.Text, "", "Dgl1", ClsPurchOrderStatusReport.Col1Dimension2, 1, 0, 0, "", 100)
        ClsMain.FSeedSingleIfNotExist_ReportLineUISetting(MdiObj.MnuWeavingOrderStatusReport.Text, "", "Dgl1", ClsPurchOrderStatusReport.Col1Dimension3, 1, 0, 0, "", 100)
        ClsMain.FSeedSingleIfNotExist_ReportLineUISetting(MdiObj.MnuWeavingOrderStatusReport.Text, "", "Dgl1", ClsPurchOrderStatusReport.Col1Size, 1, 0, 0, "", 80)
        ClsMain.FSeedSingleIfNotExist_ReportLineUISetting(MdiObj.MnuWeavingOrderStatusReport.Text, "", "Dgl1", ClsPurchOrderStatusReport.Col1OrderQty, 1, 0, 0, "", 80)
        ClsMain.FSeedSingleIfNotExist_ReportLineUISetting(MdiObj.MnuWeavingOrderStatusReport.Text, "", "Dgl1", ClsPurchOrderStatusReport.Col1OrderAmount, 1, 0, 0, "", 100)
        ClsMain.FSeedSingleIfNotExist_ReportLineUISetting(MdiObj.MnuWeavingOrderStatusReport.Text, "", "Dgl1", ClsPurchOrderStatusReport.Col1BalanceQty, 1, 0, 0, "", 80)
        ClsMain.FSeedSingleIfNotExist_ReportLineUISetting(MdiObj.MnuWeavingOrderStatusReport.Text, "", "Dgl1", ClsPurchOrderStatusReport.Col1BalanceAmount, 1, 0, 0, "", 100)
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
    End Sub
    Private Sub FConfigure_FinishedMaterialPurchaseOrder(ClsObj As ClsMain)
        ClsObj.FUpdateSeed_EntryHeaderUISetting("FrmPurchInvoiceDirect", "", Ncat.PurchaseOrder, "", "", "", "DglMain", FrmSaleInvoiceDirect_WithDimension.hcSettingGroup, 1, 0, 1, 1, "")

        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchInvoiceDirect", "", Ncat.PurchaseOrder, "", "", "", "Dgl1", FrmPurchPlan.Col1Item, 0, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchInvoiceDirect", "", Ncat.PurchaseOrder, "", "", "", "Dgl1", FrmPurchPlan.Col1ItemCategory, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchInvoiceDirect", "", Ncat.PurchaseOrder, "", "", "", "Dgl1", FrmPurchPlan.Col1Dimension1, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchInvoiceDirect", "", Ncat.PurchaseOrder, "", "", "", "Dgl1", FrmPurchPlan.Col1Dimension2, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchInvoiceDirect", "", Ncat.PurchaseOrder, "", "", "", "Dgl1", FrmPurchPlan.Col1Dimension3, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchInvoiceDirect", "", Ncat.PurchaseOrder, "", "", "", "Dgl1", FrmPurchPlan.Col1Size, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchInvoiceDirect", "", Ncat.PurchaseOrder, "", "", "", "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1PurchasePlan, 1, 0, 1, "")

        ClsObj.FUpdateSeed_EntryHeaderUISetting("FrmPurchInvoiceDirect", "", Ncat.PurchaseOrder, "", "", "", "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcBtnPendingPurchPlan, 1, 0, 0, 0, "")

        mQry = "Update Setting Set Value ='+" & ItemTypeCode.ManufacturingProduct & "+" & ItemTypeCode.TradingProduct & "'
                Where SettingType = '" & SettingType.General & "'
                And NCat = '" & Ncat.PurchaseOrder & "'
                And SettingGroup Is Null
                And FieldName = '" & ClsMain.SettingFields.FilterInclude_ItemType & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
    End Sub
    Private Sub FConfigure_FinishedMaterialPurchaseInvoice(ClsObj As ClsMain)
        ClsObj.FUpdateSeed_EntryHeaderUISetting("FrmPurchInvoiceDirect", "", Ncat.PurchaseInvoice, "", "", "", "DglMain", FrmSaleInvoiceDirect_WithDimension.hcSettingGroup, 1, 0, 1, 1, "")

        mQry = "Update Setting Set Value ='1' Where FieldName = '" & ClsMain.SettingFields.OrderApplicableYn & "' And NCat = '" & Ncat.PurchaseInvoice & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        mQry = "Update Setting Set Value ='1' Where FieldName = '" & ClsMain.SettingFields.ContraBalanceOnValueYN & "' And NCat = '" & Ncat.PurchaseInvoice & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        'mQry = "Update Setting Set Value ='1' Where FieldName = '" & ClsMain.SettingFields.ItemHelpFromOrderYN & "' And NCat = '" & Ncat.PurchaseInvoice & "'"
        'AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)

        mQry = "Update Setting Set Value ='+" & ItemTypeCode.ManufacturingProduct & "+" & ItemTypeCode.TradingProduct & "'
                Where SettingType = '" & SettingType.General & "'
                And NCat = '" & Ncat.PurchaseInvoice & "'
                And SettingGroup Is Null
                And FieldName = '" & ClsMain.SettingFields.FilterInclude_ItemType & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)

        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchInvoiceDirect", "", Ncat.PurchaseInvoice, "", "", "", "Dgl1", FrmPurchPlan.Col1Item, 0, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchInvoiceDirect", "", Ncat.PurchaseInvoice, "", "", "", "Dgl1", FrmPurchPlan.Col1ItemCategory, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchInvoiceDirect", "", Ncat.PurchaseInvoice, "", "", "", "Dgl1", FrmPurchPlan.Col1Dimension1, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchInvoiceDirect", "", Ncat.PurchaseInvoice, "", "", "", "Dgl1", FrmPurchPlan.Col1Dimension2, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchInvoiceDirect", "", Ncat.PurchaseInvoice, "", "", "", "Dgl1", FrmPurchPlan.Col1Dimension3, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmPurchInvoiceDirect", "", Ncat.PurchaseInvoice, "", "", "", "Dgl1", FrmPurchPlan.Col1Size, 1, 0, 1, "")
    End Sub
    Private Sub FConfigure_RawMaterialPurchaseOrder(ClsObj As ClsMain)
        ClsObj.FUpdateSeed_EntryHeaderUISetting("FrmPurchInvoiceDirect", "", Ncat.PurchaseOrder, "", "", "", "DglMain", FrmSaleInvoiceDirect_WithDimension.hcSettingGroup, 1, 0, 1, 1, "")

        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.ColSNo, True, True,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Barcode, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCategory, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemGroup, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCode, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Item, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension4, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Specification, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1SalesTaxGroup, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1BaleNo, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1LotNo, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DocQty, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Unit, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Rate, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DiscountPer, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DiscountAmount, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionalDiscountPer, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionalDiscountAmount, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionPer, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionAmount, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Amount, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Remark, True,,,,,, SettingGroup_RawMaterial)

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.PurchaseOrder, SettingFields.FilterInclude_ItemType,
                            "+" & ItemTypeCode.RawProduct, AgDataType.Text, "255",
                            "SELECT Code, Name FROM ItemType Order By Name",
                            AgHelpQueryType.SqlQuery, AgHelpSelectionType.MultiSelect,,,,, SettingGroup_RawMaterial,, "+SUPPORT")
    End Sub
    Private Sub FConfigure_RawMaterialPurchaseInvoice(ClsObj As ClsMain)
        ClsObj.FUpdateSeed_EntryHeaderUISetting("FrmPurchInvoiceDirect", "", Ncat.PurchaseInvoice, "", "", "", "DglMain", FrmSaleInvoiceDirect_WithDimension.hcSettingGroup, 1, 0, 1, 1, "")

        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.ColSNo, True, True,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Barcode, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCategory, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemGroup, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCode, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Item, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Dimension4, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Specification, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1SalesTaxGroup, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1BaleNo, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1LotNo, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DocQty, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Unit, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Rate, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DiscountPer, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DiscountAmount, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionalDiscountPer, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionalDiscountAmount, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionPer, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionAmount, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Amount, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Remark, True,,,,,, SettingGroup_RawMaterial)

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.PurchaseInvoice, SettingFields.FilterInclude_ItemType,
                            "+" & ItemTypeCode.RawProduct, AgDataType.Text, "255",
                            "SELECT Code, Name FROM ItemType Order By Name",
                            AgHelpQueryType.SqlQuery, AgHelpSelectionType.MultiSelect,,,,, SettingGroup_RawMaterial,, "+SUPPORT")

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.PurchaseInvoice, SettingFields.ItemHelpFromOrderYN, "0", AgDataType.YesNo, "50",,,,,,,, SettingGroup_RawMaterial,, "+SUPPORT")
    End Sub
    Private Sub FConfigure_OtherMaterialPurchaseOrder(ClsObj As ClsMain)
        ClsObj.FUpdateSeed_EntryHeaderUISetting("FrmPurchInvoiceDirect", "", Ncat.PurchaseOrder, "", "", "", "DglMain", FrmSaleInvoiceDirect_WithDimension.hcSettingGroup, 1, 0, 1, 1, "")

        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.ColSNo, True, True,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Barcode, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCategory, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemGroup, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCode, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Item, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Specification, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1SalesTaxGroup, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1BaleNo, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1LotNo, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DocQty, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Unit, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Rate, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DiscountPer, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DiscountAmount, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionalDiscountPer, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionalDiscountAmount, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionPer, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionAmount, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Amount, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Remark, True,,,,,, SettingGroup_OtherMaterial)

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.PurchaseOrder, SettingFields.FilterInclude_ItemType,
                            "+" & ItemTypeCode.OtherProduct, AgDataType.Text, "255",
                            "SELECT Code, Name FROM ItemType Order By Name",
                            AgHelpQueryType.SqlQuery, AgHelpSelectionType.MultiSelect,,,,, SettingGroup_OtherMaterial,, "+SUPPORT")
    End Sub
    Private Sub FConfigure_OtherMaterialPurchaseInvoice(ClsObj As ClsMain)
        ClsObj.FUpdateSeed_EntryHeaderUISetting("FrmPurchInvoiceDirect", "", Ncat.PurchaseInvoice, "", "", "", "DglMain", FrmSaleInvoiceDirect_WithDimension.hcSettingGroup, 1, 0, 1, 1, "")

        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.ColSNo, True, True,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Barcode, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCategory, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemGroup, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCode, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Item, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Specification, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1SalesTaxGroup, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1BaleNo, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1LotNo, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DocQty, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Unit, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Rate, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DiscountPer, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DiscountAmount, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionalDiscountPer, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionalDiscountAmount, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionPer, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionAmount, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Amount, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.PurchaseInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Remark, True,,,,,, SettingGroup_OtherMaterial)

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.PurchaseInvoice, SettingFields.FilterInclude_ItemType,
                            "+" & ItemTypeCode.OtherProduct, AgDataType.Text, "255",
                            "SELECT Code, Name FROM ItemType Order By Name",
                            AgHelpQueryType.SqlQuery, AgHelpSelectionType.MultiSelect,,,,, SettingGroup_OtherMaterial,, "+SUPPORT")

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.PurchaseInvoice, SettingFields.ItemHelpFromOrderYN, "0", AgDataType.YesNo, "50",,,,,,,, SettingGroup_OtherMaterial,, "+SUPPORT")
    End Sub
    Private Sub FConfigure_FinishedMaterialOpeningStock(ClsObj As ClsMain)
        ClsObj.FUpdateSeed_EntryHeaderUISetting("FrmStockEntry", "", Ncat.OpeningStock, "", "", "", "DglMain", FrmSaleInvoiceDirect_WithDimension.hcSettingGroup, 1, 0, 1, 1, "")

        ClsObj.FUpdateSeed_EntryLineUISetting("FrmStockEntry", "", Ncat.OpeningStock, "", "", "", "Dgl1", FrmPurchPlan.Col1Item, 0, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmStockEntry", "", Ncat.OpeningStock, "", "", "", "Dgl1", FrmPurchPlan.Col1ItemCategory, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmStockEntry", "", Ncat.OpeningStock, "", "", "", "Dgl1", FrmPurchPlan.Col1Dimension1, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmStockEntry", "", Ncat.OpeningStock, "", "", "", "Dgl1", FrmPurchPlan.Col1Dimension2, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmStockEntry", "", Ncat.OpeningStock, "", "", "", "Dgl1", FrmPurchPlan.Col1Dimension3, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmStockEntry", "", Ncat.OpeningStock, "", "", "", "Dgl1", FrmPurchPlan.Col1Size, 1, 0, 1, "")

        mQry = "Update Setting Set Value ='+" & ItemTypeCode.ManufacturingProduct & "+" & ItemTypeCode.TradingProduct & "'
                Where SettingType = '" & SettingType.General & "'
                And NCat = '" & Ncat.OpeningStock & "'
                And SettingGroup Is Null
                And FieldName = '" & ClsMain.SettingFields.FilterInclude_ItemType & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
    End Sub
    Private Sub FConfigure_RawMaterialOpeningStock(ClsObj As ClsMain)
        ClsObj.FUpdateSeed_EntryHeaderUISetting("FrmStockEntry", "", Ncat.OpeningStock, "", "", "", "DglMain", FrmSaleInvoiceDirect_WithDimension.hcSettingGroup, 1, 0, 1, 1, "")

        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.OpeningStock, "Dgl1", FrmStockEntry.ColSNo, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.OpeningStock, "Dgl1", FrmStockEntry.Col1Barcode, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.OpeningStock, "Dgl1", FrmStockEntry.Col1ItemCategory, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.OpeningStock, "Dgl1", FrmStockEntry.Col1ItemGroup, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.OpeningStock, "Dgl1", FrmStockEntry.Col1ItemCode, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.OpeningStock, "Dgl1", FrmStockEntry.Col1Item, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.OpeningStock, "Dgl1", FrmStockEntry.Col1Dimension4, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.OpeningStock, "Dgl1", FrmStockEntry.Col1Specification, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.OpeningStock, "Dgl1", FrmStockEntry.Col1ItemState, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.OpeningStock, "Dgl1", FrmStockEntry.Col1BaleNo, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.OpeningStock, "Dgl1", FrmStockEntry.Col1LotNo, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.OpeningStock, "Dgl1", FrmStockEntry.Col1Godown, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.OpeningStock, "Dgl1", FrmStockEntry.Col1Qty, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.OpeningStock, "Dgl1", FrmStockEntry.Col1Unit, True, False,, False,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.OpeningStock, "Dgl1", FrmStockEntry.Col1Pcs, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.OpeningStock, "Dgl1", FrmStockEntry.Col1DealQty, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.OpeningStock, "Dgl1", FrmStockEntry.Col1DealUnit, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.OpeningStock, "Dgl1", FrmStockEntry.Col1Rate, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.OpeningStock, "Dgl1", FrmStockEntry.Col1Amount, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.OpeningStock, "Dgl1", FrmStockEntry.Col1Remark, True,,,,,, SettingGroup_RawMaterial)

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.OpeningStock, SettingFields.FilterInclude_ItemType,
                            "+" & ItemTypeCode.RawProduct, AgDataType.Text, "255",
                            "SELECT Code, Name FROM ItemType Order By Name",
                            AgHelpQueryType.SqlQuery, AgHelpSelectionType.MultiSelect,,,,, SettingGroup_RawMaterial,, "+SUPPORT")
    End Sub
    Private Sub FConfigure_OtherMaterialOpeningStock(ClsObj As ClsMain)
        ClsObj.FUpdateSeed_EntryHeaderUISetting("FrmStockEntry", "", Ncat.OpeningStock, "", "", "", "DglMain", FrmSaleInvoiceDirect_WithDimension.hcSettingGroup, 1, 0, 1, 1, "")

        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.ColSNo, True, True,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Barcode, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCategory, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemGroup, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCode, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Item, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Specification, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1SalesTaxGroup, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1BaleNo, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1LotNo, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DocQty, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Unit, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Rate, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DiscountPer, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DiscountAmount, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionalDiscountPer, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionalDiscountAmount, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionPer, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionAmount, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Amount, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.OpeningStock, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Remark, True,,,,,, SettingGroup_OtherMaterial)

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.OpeningStock, SettingFields.FilterInclude_ItemType,
                            "+" & ItemTypeCode.OtherProduct, AgDataType.Text, "255",
                            "SELECT Code, Name FROM ItemType Order By Name",
                            AgHelpQueryType.SqlQuery, AgHelpSelectionType.MultiSelect,,,,, SettingGroup_OtherMaterial,, "+SUPPORT")
    End Sub







    Private Sub FConfigure_FinishedMaterialPurchaseGoodsReceipt(ClsObj As ClsMain)
        ClsObj.FUpdateSeed_EntryHeaderUISetting("FrmStockEntry", "", Ncat.PurchaseGoodsReceipt, "", "", "", "DglMain", FrmSaleInvoiceDirect_WithDimension.hcSettingGroup, 1, 0, 1, 1, "")

        ClsObj.FUpdateSeed_EntryLineUISetting("FrmStockEntry", "", Ncat.PurchaseGoodsReceipt, "", "", "", "Dgl1", FrmPurchPlan.Col1Item, 0, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmStockEntry", "", Ncat.PurchaseGoodsReceipt, "", "", "", "Dgl1", FrmPurchPlan.Col1ItemCategory, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmStockEntry", "", Ncat.PurchaseGoodsReceipt, "", "", "", "Dgl1", FrmPurchPlan.Col1Dimension1, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmStockEntry", "", Ncat.PurchaseGoodsReceipt, "", "", "", "Dgl1", FrmPurchPlan.Col1Dimension2, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmStockEntry", "", Ncat.PurchaseGoodsReceipt, "", "", "", "Dgl1", FrmPurchPlan.Col1Dimension3, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmStockEntry", "", Ncat.PurchaseGoodsReceipt, "", "", "", "Dgl1", FrmPurchPlan.Col1Size, 1, 0, 1, "")

        mQry = "Update Setting Set Value ='+" & ItemTypeCode.ManufacturingProduct & "+" & ItemTypeCode.TradingProduct & "'
                Where SettingType = '" & SettingType.General & "'
                And NCat = '" & Ncat.PurchaseGoodsReceipt & "'
                And SettingGroup Is Null
                And FieldName = '" & ClsMain.SettingFields.FilterInclude_ItemType & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
    End Sub
    Private Sub FConfigure_RawMaterialPurchaseGoodsReceipt(ClsObj As ClsMain)
        ClsObj.FUpdateSeed_EntryHeaderUISetting("FrmStockEntry", "", Ncat.PurchaseGoodsReceipt, "", "", "", "DglMain", FrmSaleInvoiceDirect_WithDimension.hcSettingGroup, 1, 0, 1, 1, "")

        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.PurchaseGoodsReceipt, "Dgl1", FrmStockEntry.ColSNo, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.PurchaseGoodsReceipt, "Dgl1", FrmStockEntry.Col1Barcode, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.PurchaseGoodsReceipt, "Dgl1", FrmStockEntry.Col1ItemCategory, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.PurchaseGoodsReceipt, "Dgl1", FrmStockEntry.Col1ItemGroup, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.PurchaseGoodsReceipt, "Dgl1", FrmStockEntry.Col1ItemCode, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.PurchaseGoodsReceipt, "Dgl1", FrmStockEntry.Col1Item, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.PurchaseGoodsReceipt, "Dgl1", FrmStockEntry.Col1Dimension4, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.PurchaseGoodsReceipt, "Dgl1", FrmStockEntry.Col1Specification, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.PurchaseGoodsReceipt, "Dgl1", FrmStockEntry.Col1ItemState, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.PurchaseGoodsReceipt, "Dgl1", FrmStockEntry.Col1BaleNo, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.PurchaseGoodsReceipt, "Dgl1", FrmStockEntry.Col1LotNo, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.PurchaseGoodsReceipt, "Dgl1", FrmStockEntry.Col1Godown, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.PurchaseGoodsReceipt, "Dgl1", FrmStockEntry.Col1Qty, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.PurchaseGoodsReceipt, "Dgl1", FrmStockEntry.Col1Unit, True, False,, False,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.PurchaseGoodsReceipt, "Dgl1", FrmStockEntry.Col1Pcs, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.PurchaseGoodsReceipt, "Dgl1", FrmStockEntry.Col1DealQty, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.PurchaseGoodsReceipt, "Dgl1", FrmStockEntry.Col1DealUnit, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.PurchaseGoodsReceipt, "Dgl1", FrmStockEntry.Col1Rate, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.PurchaseGoodsReceipt, "Dgl1", FrmStockEntry.Col1Amount, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.PurchaseGoodsReceipt, "Dgl1", FrmStockEntry.Col1Remark, True,,,,,, SettingGroup_RawMaterial)

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.PurchaseGoodsReceipt, SettingFields.FilterInclude_ItemType,
                            "+" & ItemTypeCode.RawProduct, AgDataType.Text, "255",
                            "SELECT Code, Name FROM ItemType Order By Name",
                            AgHelpQueryType.SqlQuery, AgHelpSelectionType.MultiSelect,,,,, SettingGroup_RawMaterial,, "+SUPPORT")
    End Sub
    Private Sub FConfigure_OtherMaterialPurchaseGoodsReceipt(ClsObj As ClsMain)
        ClsObj.FUpdateSeed_EntryHeaderUISetting("FrmStockEntry", "", Ncat.PurchaseGoodsReceipt, "", "", "", "DglMain", FrmSaleInvoiceDirect_WithDimension.hcSettingGroup, 1, 0, 1, 1, "")

        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.PurchaseGoodsReceipt, "Dgl1", FrmPurchInvoiceDirect_WithDimension.ColSNo, True, True,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.PurchaseGoodsReceipt, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Barcode, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.PurchaseGoodsReceipt, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCategory, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.PurchaseGoodsReceipt, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemGroup, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.PurchaseGoodsReceipt, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCode, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.PurchaseGoodsReceipt, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Item, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.PurchaseGoodsReceipt, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Specification, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.PurchaseGoodsReceipt, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1SalesTaxGroup, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.PurchaseGoodsReceipt, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1BaleNo, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.PurchaseGoodsReceipt, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1LotNo, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.PurchaseGoodsReceipt, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DocQty, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.PurchaseGoodsReceipt, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Unit, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.PurchaseGoodsReceipt, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Rate, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.PurchaseGoodsReceipt, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DiscountPer, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.PurchaseGoodsReceipt, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DiscountAmount, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.PurchaseGoodsReceipt, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionalDiscountPer, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.PurchaseGoodsReceipt, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionalDiscountAmount, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.PurchaseGoodsReceipt, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionPer, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.PurchaseGoodsReceipt, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionAmount, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.PurchaseGoodsReceipt, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Amount, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.PurchaseGoodsReceipt, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Remark, True,,,,,, SettingGroup_OtherMaterial)

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.PurchaseGoodsReceipt, SettingFields.FilterInclude_ItemType,
                            "+" & ItemTypeCode.OtherProduct, AgDataType.Text, "255",
                            "SELECT Code, Name FROM ItemType Order By Name",
                            AgHelpQueryType.SqlQuery, AgHelpSelectionType.MultiSelect,,,,, SettingGroup_OtherMaterial,, "+SUPPORT")
    End Sub

    Private Sub FConfigure_FinishedMaterialStockReceive(ClsObj As ClsMain)
        ClsObj.FUpdateSeed_EntryHeaderUISetting("FrmStockEntry", "", Ncat.StockReceive, "", "", "", "DglMain", FrmSaleInvoiceDirect_WithDimension.hcSettingGroup, 1, 0, 1, 1, "")

        ClsObj.FUpdateSeed_EntryLineUISetting("FrmStockEntry", "", Ncat.StockReceive, "", "", "", "Dgl1", FrmPurchPlan.Col1Item, 0, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmStockEntry", "", Ncat.StockReceive, "", "", "", "Dgl1", FrmPurchPlan.Col1ItemCategory, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmStockEntry", "", Ncat.StockReceive, "", "", "", "Dgl1", FrmPurchPlan.Col1Dimension1, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmStockEntry", "", Ncat.StockReceive, "", "", "", "Dgl1", FrmPurchPlan.Col1Dimension2, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmStockEntry", "", Ncat.StockReceive, "", "", "", "Dgl1", FrmPurchPlan.Col1Dimension3, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmStockEntry", "", Ncat.StockReceive, "", "", "", "Dgl1", FrmPurchPlan.Col1Size, 1, 0, 1, "")

        mQry = "Update Setting Set Value ='+" & ItemTypeCode.ManufacturingProduct & "+" & ItemTypeCode.TradingProduct & "'
                Where SettingType = '" & SettingType.General & "'
                And NCat = '" & Ncat.StockReceive & "'
                And SettingGroup Is Null
                And FieldName = '" & ClsMain.SettingFields.FilterInclude_ItemType & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
    End Sub
    Private Sub FConfigure_RawMaterialStockReceive(ClsObj As ClsMain)
        ClsObj.FUpdateSeed_EntryHeaderUISetting("FrmStockEntry", "", Ncat.StockReceive, "", "", "", "DglMain", FrmSaleInvoiceDirect_WithDimension.hcSettingGroup, 1, 0, 1, 1, "")

        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.StockReceive, "Dgl1", FrmStockEntry.ColSNo, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.StockReceive, "Dgl1", FrmStockEntry.Col1Barcode, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.StockReceive, "Dgl1", FrmStockEntry.Col1ItemCategory, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.StockReceive, "Dgl1", FrmStockEntry.Col1ItemGroup, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.StockReceive, "Dgl1", FrmStockEntry.Col1ItemCode, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.StockReceive, "Dgl1", FrmStockEntry.Col1Item, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.StockReceive, "Dgl1", FrmStockEntry.Col1Dimension4, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.StockReceive, "Dgl1", FrmStockEntry.Col1Specification, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.StockReceive, "Dgl1", FrmStockEntry.Col1ItemState, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.StockReceive, "Dgl1", FrmStockEntry.Col1BaleNo, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.StockReceive, "Dgl1", FrmStockEntry.Col1LotNo, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.StockReceive, "Dgl1", FrmStockEntry.Col1Godown, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.StockReceive, "Dgl1", FrmStockEntry.Col1Qty, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.StockReceive, "Dgl1", FrmStockEntry.Col1Unit, True, False,, False,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.StockReceive, "Dgl1", FrmStockEntry.Col1Pcs, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.StockReceive, "Dgl1", FrmStockEntry.Col1DealQty, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.StockReceive, "Dgl1", FrmStockEntry.Col1DealUnit, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.StockReceive, "Dgl1", FrmStockEntry.Col1Rate, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.StockReceive, "Dgl1", FrmStockEntry.Col1Amount, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.StockReceive, "Dgl1", FrmStockEntry.Col1Remark, True,,,,,, SettingGroup_RawMaterial)

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.StockReceive, SettingFields.FilterInclude_ItemType,
                            "+" & ItemTypeCode.RawProduct, AgDataType.Text, "255",
                            "SELECT Code, Name FROM ItemType Order By Name",
                            AgHelpQueryType.SqlQuery, AgHelpSelectionType.MultiSelect,,,,, SettingGroup_RawMaterial,, "+SUPPORT")
    End Sub
    Private Sub FConfigure_OtherMaterialStockReceive(ClsObj As ClsMain)
        ClsObj.FUpdateSeed_EntryHeaderUISetting("FrmStockEntry", "", Ncat.StockReceive, "", "", "", "DglMain", FrmSaleInvoiceDirect_WithDimension.hcSettingGroup, 1, 0, 1, 1, "")

        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.StockReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.ColSNo, True, True,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.StockReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Barcode, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.StockReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCategory, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.StockReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemGroup, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.StockReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCode, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.StockReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Item, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.StockReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Specification, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.StockReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1SalesTaxGroup, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.StockReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1BaleNo, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.StockReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1LotNo, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.StockReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DocQty, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.StockReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Unit, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.StockReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Rate, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.StockReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DiscountPer, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.StockReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DiscountAmount, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.StockReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionalDiscountPer, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.StockReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionalDiscountAmount, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.StockReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionPer, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.StockReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionAmount, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.StockReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Amount, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.StockReceive, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Remark, True,,,,,, SettingGroup_OtherMaterial)

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.StockReceive, SettingFields.FilterInclude_ItemType,
                            "+" & ItemTypeCode.OtherProduct, AgDataType.Text, "255",
                            "SELECT Code, Name FROM ItemType Order By Name",
                            AgHelpQueryType.SqlQuery, AgHelpSelectionType.MultiSelect,,,,, SettingGroup_OtherMaterial,, "+SUPPORT")
    End Sub





    Private Sub FConfigure_FinishedMaterialStockIssue(ClsObj As ClsMain)
        ClsObj.FUpdateSeed_EntryHeaderUISetting("FrmStockEntry", "", Ncat.StockIssue, "", "", "", "DglMain", FrmSaleInvoiceDirect_WithDimension.hcSettingGroup, 1, 0, 1, 1, "")

        ClsObj.FUpdateSeed_EntryLineUISetting("FrmStockEntry", "", Ncat.StockIssue, "", "", "", "Dgl1", FrmPurchPlan.Col1Item, 0, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmStockEntry", "", Ncat.StockIssue, "", "", "", "Dgl1", FrmPurchPlan.Col1ItemCategory, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmStockEntry", "", Ncat.StockIssue, "", "", "", "Dgl1", FrmPurchPlan.Col1Dimension1, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmStockEntry", "", Ncat.StockIssue, "", "", "", "Dgl1", FrmPurchPlan.Col1Dimension2, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmStockEntry", "", Ncat.StockIssue, "", "", "", "Dgl1", FrmPurchPlan.Col1Dimension3, 1, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmStockEntry", "", Ncat.StockIssue, "", "", "", "Dgl1", FrmPurchPlan.Col1Size, 1, 0, 1, "")

        mQry = "Update Setting Set Value ='+" & ItemTypeCode.ManufacturingProduct & "+" & ItemTypeCode.TradingProduct & "'
                Where SettingType = '" & SettingType.General & "'
                And NCat = '" & Ncat.StockIssue & "'
                And SettingGroup Is Null
                And FieldName = '" & ClsMain.SettingFields.FilterInclude_ItemType & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
    End Sub
    Private Sub FConfigure_RawMaterialStockIssue(ClsObj As ClsMain)
        ClsObj.FUpdateSeed_EntryHeaderUISetting("FrmStockEntry", "", Ncat.StockIssue, "", "", "", "DglMain", FrmSaleInvoiceDirect_WithDimension.hcSettingGroup, 1, 0, 1, 1, "")

        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.StockIssue, "Dgl1", FrmStockEntry.ColSNo, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.StockIssue, "Dgl1", FrmStockEntry.Col1Barcode, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.StockIssue, "Dgl1", FrmStockEntry.Col1ItemCategory, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.StockIssue, "Dgl1", FrmStockEntry.Col1ItemGroup, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.StockIssue, "Dgl1", FrmStockEntry.Col1ItemCode, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.StockIssue, "Dgl1", FrmStockEntry.Col1Item, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.StockIssue, "Dgl1", FrmStockEntry.Col1Dimension4, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.StockIssue, "Dgl1", FrmStockEntry.Col1Specification, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.StockIssue, "Dgl1", FrmStockEntry.Col1ItemState, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.StockIssue, "Dgl1", FrmStockEntry.Col1BaleNo, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.StockIssue, "Dgl1", FrmStockEntry.Col1LotNo, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.StockIssue, "Dgl1", FrmStockEntry.Col1Godown, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.StockIssue, "Dgl1", FrmStockEntry.Col1Qty, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.StockIssue, "Dgl1", FrmStockEntry.Col1Unit, True, False,, False,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.StockIssue, "Dgl1", FrmStockEntry.Col1Pcs, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.StockIssue, "Dgl1", FrmStockEntry.Col1DealQty, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.StockIssue, "Dgl1", FrmStockEntry.Col1DealUnit, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.StockIssue, "Dgl1", FrmStockEntry.Col1Rate, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.StockIssue, "Dgl1", FrmStockEntry.Col1Amount, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.StockIssue, "Dgl1", FrmStockEntry.Col1Remark, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.StockIssue, "Dgl1", FrmStockEntry.Col1BtnBaseDetail, True,,,,,, SettingGroup_RawMaterial)

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.StockIssue, SettingFields.FilterInclude_ItemType,
                            "+" & ItemTypeCode.RawProduct, AgDataType.Text, "255",
                            "SELECT Code, Name FROM ItemType Order By Name",
                            AgHelpQueryType.SqlQuery, AgHelpSelectionType.MultiSelect,,,,, SettingGroup_RawMaterial,, "+SUPPORT")

        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockHeadDetailBase", Ncat.StockIssue, "Dgl1", FrmStockHeadDetailBase.Col1BaseReferenceNo, True,, "Order No.",,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockHeadDetailBase", Ncat.StockIssue, "Dgl1", FrmStockHeadDetailBase.Col1BaseItemCategory, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockHeadDetailBase", Ncat.StockIssue, "Dgl1", FrmStockHeadDetailBase.Col1BaseItemGroup, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockHeadDetailBase", Ncat.StockIssue, "Dgl1", FrmStockHeadDetailBase.Col1BaseItem, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockHeadDetailBase", Ncat.StockIssue, "Dgl1", FrmStockHeadDetailBase.Col1BaseDimension1, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockHeadDetailBase", Ncat.StockIssue, "Dgl1", FrmStockHeadDetailBase.Col1BaseDimension2, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockHeadDetailBase", Ncat.StockIssue, "Dgl1", FrmStockHeadDetailBase.Col1BaseDimension3, True,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockHeadDetailBase", Ncat.StockIssue, "Dgl1", FrmStockHeadDetailBase.Col1BaseDimension4, False,,,,,, SettingGroup_RawMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockHeadDetailBase", Ncat.StockIssue, "Dgl1", FrmStockHeadDetailBase.Col1BaseQty, True,,,,,, SettingGroup_RawMaterial)
    End Sub
    Private Sub FConfigure_OtherMaterialStockIssue(ClsObj As ClsMain)
        ClsObj.FUpdateSeed_EntryHeaderUISetting("FrmStockEntry", "", Ncat.StockIssue, "", "", "", "DglMain", FrmSaleInvoiceDirect_WithDimension.hcSettingGroup, 1, 0, 1, 1, "")

        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.StockIssue, "Dgl1", FrmPurchInvoiceDirect_WithDimension.ColSNo, True, True,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.StockIssue, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Barcode, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.StockIssue, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCategory, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.StockIssue, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemGroup, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.StockIssue, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCode, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.StockIssue, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Item, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.StockIssue, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Specification, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.StockIssue, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1SalesTaxGroup, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.StockIssue, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1BaleNo, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.StockIssue, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1LotNo, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.StockIssue, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DocQty, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.StockIssue, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Unit, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.StockIssue, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Rate, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.StockIssue, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DiscountPer, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.StockIssue, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DiscountAmount, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.StockIssue, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionalDiscountPer, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.StockIssue, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionalDiscountAmount, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.StockIssue, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionPer, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.StockIssue, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionAmount, False,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.StockIssue, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Amount, True,,,,,, SettingGroup_OtherMaterial)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStockEntry", Ncat.StockIssue, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Remark, True,,,,,, SettingGroup_OtherMaterial)

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.StockIssue, SettingFields.FilterInclude_ItemType,
                            "+" & ItemTypeCode.OtherProduct, AgDataType.Text, "255",
                            "SELECT Code, Name FROM ItemType Order By Name",
                            AgHelpQueryType.SqlQuery, AgHelpSelectionType.MultiSelect,,,,, SettingGroup_OtherMaterial,, "+SUPPORT")
    End Sub
    Private Sub FConfigure_SaleEnquiry_Sample(ClsObj As ClsMain)
        ClsObj.FSeedSingleIfNotExists_Voucher_Type(V_Type_SaleEnquirySample, "Sale Enquiry Sample", Ncat.SaleEnquiry, VoucherCategory.Sales, "", "Customised", MdiObj.MnuSalesEnquiry.Name, MdiObj.MnuSalesEnquiry.Text)

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", "", SettingFields.GeneratedEntryV_TypeForAadhat, V_Type_SaleOrderSample, AgDataType.Text, "255",
                    "SELECT V_Type AS Code, Description AS Name FROM Voucher_Type WHERE NCat = '" & Ncat.SaleOrder & "' ORDER BY Description",
                    AgHelpQueryType.SqlQuery, AgHelpSelectionType.SingleSelect,,, V_Type_SaleEnquirySample,, ,, "+SUPPORT")
    End Sub
    Private Sub FConfigure_SaleOrder_Sample(ClsObj As ClsMain)
        ClsObj.FSeedSingleIfNotExists_Voucher_Type(V_Type_SaleOrderSample, "Sale Order Sample", Ncat.SaleOrder, VoucherCategory.Sales, "", "Customised", MdiObj.MnuSalesOrder.Name, MdiObj.MnuSalesOrder.Text)
    End Sub
End Class
