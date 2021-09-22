Imports AgLibrary.ClsMain.agConstants
Imports Customised.ClsMain
Public Class ClsFallPico
    Private mQry As String = ""
    Private mItemTypeFieldQry As String = ""

    Public Const Process_Alter As String = "PAlter"
    Dim mProcessFieldQry As String = ""
    Public Sub FSeedData_FallPico()
        Dim ClsObj As New Customised.ClsMain(AgL)
        Try
            If ClsMain.IsScopeOfWorkContains(IndustryType.SubIndustryType.FallPico) Then
                FInitVariables()

                FConfigure_Structure(ClsObj)
                FConfigure_Process(ClsObj)

                FSeedTable_Voucher_Type(ClsObj)
                FConfigure_WorkOrder(ClsObj)
                FConfigure_WorkInvoice(ClsObj)

                FConfigure_JobOrder(ClsObj)
                FConfigure_JobInvoice(ClsObj)

                FConfigure_JobWorker(ClsObj)
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " In FSeedData_FallPicoIndustry")
        End Try
    End Sub
    Private Sub FInitVariables()
        mProcessFieldQry = "SELECT Code, Name From viewHelpSubgroup Sg  With (NoLock) Where SubgroupType ='" & SubgroupType.Process & "' Order By Name"
    End Sub
    Private Sub FSeedTable_Voucher_Type(ClsObj As ClsMain)
        Try
            Dim MdiObj As New MDIMain

            ClsObj.FSeedSingleIfNotExists_Voucher_Type(Ncat.WorkOrder, "Work Order", Ncat.WorkOrder, VoucherCategory.Work, "", "Customised", MdiObj.MnuWorkOrder_FallPico.Name, MdiObj.MnuWorkOrder_FallPico.Text)
            ClsObj.FSeedSingleIfNotExists_Voucher_Type(Ncat.WorkInvoice, "Work Invoice", Ncat.WorkInvoice, VoucherCategory.Work, "", "Customised", MdiObj.MnuWorkInvoice_FallPico.Name, MdiObj.MnuWorkInvoice_FallPico.Text)

            ClsObj.FSeedSingleIfNotExists_Voucher_Type(Ncat.JobOrder, "Job Order", Ncat.JobOrder, VoucherCategory.Production, "", "Customised", MdiObj.MnuJobOrder.Name, MdiObj.MnuJobOrder.Text)
            ClsObj.FSeedSingleIfNotExists_Voucher_Type(Ncat.JobInvoice, "Job Invoice", Ncat.JobInvoice, VoucherCategory.Production, "", "Customised", MdiObj.MnuJobInvoice.Name, MdiObj.MnuJobInvoice.Text)

            mQry = "UPDATE Voucher_Type SET Nature = '" & NCatNature.Order & "' WHERE NCat IN ('" & Ncat.WorkOrder & "', '" & Ncat.JobOrder & "');"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

            mQry = "UPDATE Voucher_Type SET Nature = '" & NCatNature.Invoice & "' WHERE NCat IN ('" & Ncat.WorkInvoice & "', '" & Ncat.JobInvoice & "');"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        Catch ex As Exception
            MsgBox(ex.Message & " [FSeedTable_Voucher_Type] ")
        End Try
    End Sub
    Private Sub FConfigure_WorkOrder(ClsObj As ClsMain)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceDirect", Ncat.WorkOrder, "DglMain", AgTemplate.TempTransaction1.hcSite_Code, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceDirect", Ncat.WorkOrder, "DglMain", AgTemplate.TempTransaction1.hcV_Type, 1, 1, 1, "Order Type")
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceDirect", Ncat.WorkOrder, "DglMain", AgTemplate.TempTransaction1.hcV_Date, 1, 1, 1, "Order Date")
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceDirect", Ncat.WorkOrder, "DglMain", AgTemplate.TempTransaction1.hcV_No, 0, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceDirect", Ncat.WorkOrder, "DglMain", AgTemplate.TempTransaction1.hcReferenceNo, 1, 1, 1, "Order No")
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceDirect", Ncat.WorkOrder, "DglMain", AgTemplate.TempTransaction1.hcSettingGroup, 0, 0, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceDirect", Ncat.WorkOrder, "DglMain", FrmSaleInvoiceDirect_WithDimension.hcSaleToParty, 0, 0, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceDirect", Ncat.WorkOrder, "DglMain", FrmSaleInvoiceDirect_WithDimension.hcSaleToPartyName, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceDirect", Ncat.WorkOrder, "DglMain", FrmSaleInvoiceDirect_WithDimension.hcSaleToPartyMobile, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceDirect", Ncat.WorkOrder, "DglMain", FrmSaleInvoiceDirect_WithDimension.hcBillToParty, 0, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceDirect", Ncat.WorkOrder, "DglMain", FrmSaleInvoiceDirect_WithDimension.hcProcess, 1, 1)

        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceDirect", Ncat.WorkOrder, "Dgl2", FrmSaleInvoiceDirect_WithDimension.hcReferenceSaleInvoiceNo, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceDirect", Ncat.WorkOrder, "Dgl2", FrmSaleInvoiceDirect_WithDimension.HcBtnAttachments)

        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceDirect", Ncat.WorkOrder, "Dgl3", FrmSaleInvoiceDirect_WithDimension.hcRemarks, 1)


        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.WorkOrder, "Dgl1", FrmSaleInvoiceDirect_WithDimension.ColSNo, True, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.WorkOrder, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1ItemCategory, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.WorkOrder, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1ItemGroup, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.WorkOrder, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1ItemCode, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.WorkOrder, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Item, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.WorkOrder, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1DocQty, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.WorkOrder, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Unit, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.WorkOrder, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Rate, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.WorkOrder, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1DiscountPer, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.WorkOrder, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1DiscountAmount, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.WorkOrder, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1AdditionalDiscountPer, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.WorkOrder, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1AdditionalDiscountAmount, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.WorkOrder, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1AdditionPer, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.WorkOrder, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1AdditionAmount, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.WorkOrder, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Amount, True,,, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.WorkOrder, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Remark, False)

        If AgL.VNull(AgL.Dman_Execute(" Select Count(*) From SaleInvoiceSetting Where Code = '" & Ncat.WorkOrder & "'", AgL.GCn).ExecuteScalar()) = 0 Then
            mQry = "INSERT INTO SaleInvoiceSetting (Code, V_Type, IsAllowedZeroRate)
                    VALUES('" & Ncat.WorkOrder & "', '" & Ncat.WorkOrder & "', 1);"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.WorkOrder, SettingFields.ShowReferenceNoHeaderHelpYn, "0", AgDataType.YesNo, "50")
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.WorkOrder, SettingFields.ShowReferenceNoHeaderButtonYn, "1", AgDataType.YesNo, "50")

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.WorkOrder, SettingFields.FilterInclude_Process, "+PMain", AgDataType.Text, "255", mProcessFieldQry, AgHelpQueryType.SqlQuery, AgHelpSelectionType.MultiSelect,,,, , ,, "+SUPPORT")
    End Sub
    Private Sub FConfigure_WorkInvoice(ClsObj As ClsMain)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceDirect", Ncat.WorkInvoice, "DglMain", AgTemplate.TempTransaction1.hcSite_Code, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceDirect", Ncat.WorkInvoice, "DglMain", AgTemplate.TempTransaction1.hcV_Type, 1, 1, 1, "Invoice Type")
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceDirect", Ncat.WorkInvoice, "DglMain", AgTemplate.TempTransaction1.hcV_Date, 1, 1, 1, "Invoice Date")
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceDirect", Ncat.WorkInvoice, "DglMain", AgTemplate.TempTransaction1.hcV_No, 0, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceDirect", Ncat.WorkInvoice, "DglMain", AgTemplate.TempTransaction1.hcReferenceNo, 1, 1, 1, "Invoice No")
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceDirect", Ncat.WorkInvoice, "DglMain", AgTemplate.TempTransaction1.hcSettingGroup, 0, 0, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceDirect", Ncat.WorkInvoice, "DglMain", FrmSaleInvoiceDirect_WithDimension.hcSaleToParty, 0, 0, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceDirect", Ncat.WorkInvoice, "DglMain", FrmSaleInvoiceDirect_WithDimension.hcSaleToPartyName, 1, 0, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceDirect", Ncat.WorkInvoice, "DglMain", FrmSaleInvoiceDirect_WithDimension.hcSaleToPartyMobile, 1, 0, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceDirect", Ncat.WorkInvoice, "DglMain", FrmSaleInvoiceDirect_WithDimension.hcBillToParty, 0, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceDirect", Ncat.WorkInvoice, "DglMain", FrmSaleInvoiceDirect_WithDimension.hcProcess, 1, 1)

        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceDirect", Ncat.WorkInvoice, "Dgl2", FrmSaleInvoiceDirect_WithDimension.hcBtnPendingSaleOrder, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceDirect", Ncat.WorkInvoice, "Dgl2", FrmSaleInvoiceDirect_WithDimension.HcBtnAttachments)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceDirect", Ncat.WorkInvoice, "Dgl3", FrmSaleInvoiceDirect_WithDimension.hcRemarks, 1)

        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.WorkInvoice, "Dgl1", FrmSaleInvoiceDirect_WithDimension.ColSNo, True, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.WorkInvoice, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1ItemCategory, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.WorkInvoice, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1ItemGroup, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.WorkInvoice, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1ItemCode, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.WorkInvoice, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Item, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.WorkInvoice, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1DocQty, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.WorkInvoice, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Unit, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.WorkInvoice, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Rate, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.WorkInvoice, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1DiscountPer, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.WorkInvoice, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1DiscountAmount, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.WorkInvoice, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1AdditionalDiscountPer, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.WorkInvoice, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1AdditionalDiscountAmount, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.WorkInvoice, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1AdditionPer, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.WorkInvoice, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1AdditionAmount, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.WorkInvoice, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Amount, True,,, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.WorkInvoice, "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1Remark, False)

        If AgL.VNull(AgL.Dman_Execute(" Select Count(*) From SaleInvoiceSetting Where Code = '" & Ncat.WorkInvoice & "'", AgL.GCn).ExecuteScalar()) = 0 Then
            mQry = "INSERT INTO SaleInvoiceSetting (Code, V_Type, IsAllowedZeroRate)
                    VALUES('" & Ncat.WorkInvoice & "', '" & Ncat.WorkInvoice & "', 1);"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.WorkInvoice, SettingFields.FilterInclude_Process, "+PMain", AgDataType.Text, "255", mProcessFieldQry, AgHelpQueryType.SqlQuery, AgHelpSelectionType.MultiSelect,,,, , ,, "+SUPPORT")
    End Sub
    Private Sub FConfigure_JobOrder(ClsObj As ClsMain)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "DglMain", AgTemplate.TempTransaction1.hcSite_Code, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "DglMain", AgTemplate.TempTransaction1.hcV_Type, 1, 1, 1, "Order Type")
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "DglMain", AgTemplate.TempTransaction1.hcV_Date, 1, 1, 1, "Order Date")
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "DglMain", AgTemplate.TempTransaction1.hcV_No, 0, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "DglMain", AgTemplate.TempTransaction1.hcReferenceNo, 1, 1, 1, "Order No")
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "DglMain", AgTemplate.TempTransaction1.hcSettingGroup, 0, 0, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "DglMain", FrmPurchInvoiceDirect_WithDimension.hcProcess, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "DglMain", FrmPurchInvoiceDirect_WithDimension.hcVendor, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "DglMain", FrmPurchInvoiceDirect_WithDimension.hcBillToParty, 0, 0)

        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcBtnPendingSaleInvoiceForPurchInvoice, 1, 0,, "Work Order")

        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.ColSNo, True, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCategory, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemGroup, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCode, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Item, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DocQty, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Unit, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Rate, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DiscountPer, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DiscountAmount, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionalDiscountPer, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionalDiscountAmount, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionPer, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionAmount, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Amount, True,,, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobOrder, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Remark, False)

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.JobOrder, SettingFields.FilterInclude_Process, "+PMain", AgDataType.Text, "255", mProcessFieldQry, AgHelpQueryType.SqlQuery, AgHelpSelectionType.MultiSelect,,,, , ,, "+SUPPORT")
    End Sub
    Private Sub FConfigure_JobInvoice(ClsObj As ClsMain)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.JobInvoice, "DglMain", AgTemplate.TempTransaction1.hcSite_Code, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.JobInvoice, "DglMain", AgTemplate.TempTransaction1.hcV_Type, 1, 1, 1, "Invoice Type")
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.JobInvoice, "DglMain", AgTemplate.TempTransaction1.hcV_Date, 1, 1, 1, "Invoice Date")
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.JobInvoice, "DglMain", AgTemplate.TempTransaction1.hcV_No, 0, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.JobInvoice, "DglMain", AgTemplate.TempTransaction1.hcReferenceNo, 1, 1, 1, "Invoice No")
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.JobInvoice, "DglMain", AgTemplate.TempTransaction1.hcSettingGroup, 0, 0, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.JobInvoice, "DglMain", FrmPurchInvoiceDirect_WithDimension.hcProcess, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.JobInvoice, "DglMain", FrmPurchInvoiceDirect_WithDimension.hcVendor, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.JobInvoice, "DglMain", FrmPurchInvoiceDirect_WithDimension.hcBillToParty, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.JobInvoice, "DglMain", FrmPurchInvoiceDirect_WithDimension.hcProcess, 0, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.JobInvoice, "DglMain", FrmPurchInvoiceDirect_WithDimension.hcVendor, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.JobInvoice, "DglMain", FrmPurchInvoiceDirect_WithDimension.hcBillToParty, 1, 1)

        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPurchInvoiceDirect", Ncat.JobInvoice, "Dgl2", FrmPurchInvoiceDirect_WithDimension.hcBtnPendingPurchOrder, 1)


        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.ColSNo, True, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCategory, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemGroup, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1ItemCode, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Item, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DocQty, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Unit, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Rate, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DiscountPer, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1DiscountAmount, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionalDiscountPer, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionalDiscountAmount, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionPer, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1AdditionAmount, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Amount, True,,, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1PurchaseInvoice, True,, "Order No", False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmPurchInvoiceDirect", Ncat.JobInvoice, "Dgl1", FrmPurchInvoiceDirect_WithDimension.Col1Remark, False)

        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", Ncat.JobInvoice, SettingFields.FilterInclude_Process, "+PMain", AgDataType.Text, "255", mProcessFieldQry, AgHelpQueryType.SqlQuery, AgHelpSelectionType.MultiSelect,,,, , ,, "+SUPPORT")
    End Sub
    Private Sub FConfigure_Structure(ClsObj As ClsMain)
        If AgL.FillData("Select Subcode from SubGroup Where SubCode='JOBWORK'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
            mQry = " 
                        INSERT INTO SubGroup
                        (SubCode, Site_Code, Div_Code, NamePrefix, Name, DispName, GroupCode, GroupNature, ManualCode, Nature, CityCode, PIN, Phone, Mobile, EMail, Status, SalesTaxPostingGroup, Parent, SubgroupType, Address)
                        VALUES('JOBWORK', '1', 'D', NULL, 'Jobwork A/C', 'Jobwork A/C', '0024', '', 'JOBWORK', 'Purchase', NULL, '', '', '', NULL, NULL, NULL, NULL, NULL, NULL);
                    "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If


        If AgL.FillData("Select * from Structure Where Code='GstJob'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
            mQry = " 
                        INSERT INTO Structure (Code, Description, HeaderTable, LineTable, Div_Code, Site_Code, PreparedBy, U_EntDt, U_AE, ModifiedBy, Edit_Date, UpLoadDate)
                        VALUES ('GstJob', 'GST JOB', NULL, NULL, 'D', '1', 'sa', '2012-05-08', 'E', 'sa', '2017-07-23', NULL);


                        INSERT INTO StructureDetail (Code, Sr, Charges, Charge_Type, Value_Type, Value, Calculation, BaseColumn, PostAc, PostAcFromColumn, DrCr, LineItem, AffectCost, Active, Percentage, Amount, VisibleInMaster, VisibleInMasterLine, VisibleInTransactionLine, VisibleInTransactionFooter, HeaderPerField, HeaderAmtField, LineAmtField, LinePerField, GridDisplayIndex, UpLoadDate, WEF, InactiveDate)
                        VALUES ('GstJob', 10, 'GAMT', 'Charges', 'FixedValue', NULL, '|AMOUNT|', NULL, Null, NULL, Null, 0, 1, NULL, 0, 0, 0, 0, 0, 1, NULL, 'Gross_Amount', 'Gross_Amount', NULL, 0, NULL, '2017-07-01', NULL);

                        INSERT INTO StructureDetail (Code, Sr, Charges, Charge_Type, Value_Type, Value, Calculation, BaseColumn, PostAc, PostAcFromColumn, ContraAc, ContraAcFromColumn, DrCr, LineItem, AffectCost, InactiveDate, Percentage, Amount, VisibleInMaster, VisibleInMasterLine, VisibleInTransactionLine, VisibleInTransactionFooter, HeaderPerField, HeaderAmtField, LinePerField, LineAmtField, GridDisplayIndex, UploadDate, Active)
                        VALUES ('GstJob', 15, 'SD', 'Charges', 'Percentage Or Amount', NULL, '{GAMT}*{SD}/100', 'AMOUNT', NULL, NULL, NULL, NULL, NULL, 0, 0, NULL, 0, 0, 0, 0, 0, 1, 'SpecialDiscount_Per', 'SpecialDiscount', 'SpecialDiscount_Per', 'SpecialDiscount', 0, NULL, NULL);

                        INSERT INTO StructureDetail (Code, Sr, Charges, Charge_Type, Value_Type, Value, Calculation, BaseColumn, PostAc, PostAcFromColumn, DrCr, LineItem, AffectCost, Active, Percentage, Amount, VisibleInMaster, VisibleInMasterLine, VisibleInTransactionLine, VisibleInTransactionFooter, HeaderPerField, HeaderAmtField, LineAmtField, LinePerField, GridDisplayIndex, UpLoadDate, WEF, InactiveDate)
                        VALUES ('GstJob', 20, 'STTA', 'Charges', 'FixedValue', NULL, '{GAMT}-{SD}', NULL, 'JOBWORK', NULL, 'Dr', 1, NULL, NULL, 0, 0, 1, 1, 1, 1, NULL, 'Taxable_Amount', 'Taxable_Amount', NULL, 0, NULL, '2017-07-01', NULL);

                        INSERT INTO StructureDetail (Code, Sr, Charges, Charge_Type, Value_Type, Value, Calculation, BaseColumn, PostAc, PostAcFromColumn, DrCr, LineItem, AffectCost, Active, Percentage, Amount, VisibleInMaster, VisibleInMasterLine, VisibleInTransactionLine, VisibleInTransactionFooter, HeaderPerField, HeaderAmtField, LineAmtField, LinePerField, GridDisplayIndex, UpLoadDate, WEF, InactiveDate)
                        VALUES ('GstJob', 25, 'IGST', 'Tax1', 'Percentage Changeable', NULL, '{STTA}*{IGST}/100', NULL, NULL, NULL, 'Dr', 1, 1, NULL, 0, 0, 0, 0, 1, 1, 'Tax1_Per', 'Tax1', 'Tax1', 'Tax1_Per', 0, NULL, '2017-07-01', NULL);

                        INSERT INTO StructureDetail (Code, Sr, Charges, Charge_Type, Value_Type, Value, Calculation, BaseColumn, PostAc, PostAcFromColumn, DrCr, LineItem, AffectCost, Active, Percentage, Amount, VisibleInMaster, VisibleInMasterLine, VisibleInTransactionLine, VisibleInTransactionFooter, HeaderPerField, HeaderAmtField, LineAmtField, LinePerField, GridDisplayIndex, UpLoadDate, WEF, InactiveDate)
                        VALUES ('GstJob', 30, 'CGST', 'Tax2', 'Percentage Changeable', NULL, '{STTA}*{CGST}/100', NULL, NULL, NULL, 'Dr', 1, 1, NULL, 0, 0, 0, 0, 1, 1, 'Tax2_Per', 'Tax2', 'Tax2', 'Tax2_Per', 0, NULL, '2017-07-01', NULL);

                        INSERT INTO StructureDetail (Code, Sr, Charges, Charge_Type, Value_Type, Value, Calculation, BaseColumn, PostAc, PostAcFromColumn, DrCr, LineItem, AffectCost, Active, Percentage, Amount, VisibleInMaster, VisibleInMasterLine, VisibleInTransactionLine, VisibleInTransactionFooter, HeaderPerField, HeaderAmtField, LineAmtField, LinePerField, GridDisplayIndex, UpLoadDate, WEF, InactiveDate)
                        VALUES ('GstJob', 35, 'SGST', 'Tax3', 'Percentage Changeable', NULL, '{STTA}*{SGST}/100', NULL, NULL, NULL, 'Dr', 1, 1, NULL, 0, 0, 0, 0, 1, 1, 'Tax3_Per', 'Tax3', 'Tax3', 'Tax3_Per', 0, NULL, '2017-07-01', NULL);

                        INSERT INTO StructureDetail (Code, Sr, Charges, Charge_Type, Value_Type, Value, Calculation, BaseColumn, PostAc, PostAcFromColumn, DrCr, LineItem, AffectCost, Active, Percentage, Amount, VisibleInMaster, VisibleInMasterLine, VisibleInTransactionLine, VisibleInTransactionFooter, HeaderPerField, HeaderAmtField, LineAmtField, LinePerField, GridDisplayIndex, UpLoadDate, WEF, InactiveDate)
                        VALUES ('GstJob', 40, 'STOT1', 'Charges', 'FixedValue', NULL, '{STTA}+{IGST}+{CGST}+{SGST}', NULL, NULL, NULL, NULL, 1, 1, NULL, 0, 0, 1, 0, 1, 1, NULL, 'SubTotal1', 'SubTotal1', NULL, 0, NULL, '2017-07-01', NULL);

                        INSERT INTO StructureDetail (Code, Sr, Charges, Charge_Type, Value_Type, Value, Calculation, BaseColumn, PostAc, PostAcFromColumn, DrCr, LineItem, AffectCost, Active, Percentage, Amount, VisibleInMaster, VisibleInMasterLine, VisibleInTransactionLine, VisibleInTransactionFooter, HeaderPerField, HeaderAmtField, LineAmtField, LinePerField, GridDisplayIndex, UpLoadDate, WEF, InactiveDate)
                        VALUES ('GstJob', 45, 'OC', 'Charges', 'Percentage Or Amount', NULL, '{STOT1}*{OC}/100', 'AMOUNT', 'OCHARGE', NULL, 'Dr', 0, 1, NULL, 0, 0, 0, 0, 0, 1, 'Other_Charge_Per', 'Other_Charge', 'Other_Charge', 'Other_Charge_Per', 0, NULL, '2017-07-01', NULL);

                        INSERT INTO StructureDetail (Code, Sr, Charges, Charge_Type, Value_Type, Value, Calculation, BaseColumn, PostAc, PostAcFromColumn, DrCr, LineItem, AffectCost, Active, Percentage, Amount, VisibleInMaster, VisibleInMasterLine, VisibleInTransactionLine, VisibleInTransactionFooter, HeaderPerField, HeaderAmtField, LineAmtField, LinePerField, GridDisplayIndex, UpLoadDate, WEF, InactiveDate)
                        VALUES ('GstJob', 50, 'DED', 'Charges', 'Percentage Or Amount', NULL, '{STOT1}*{DED}/100', 'AMOUNT', 'DEDUCTION', NULL, 'Cr', 0, 0, NULL, 0, 0, 0, 0, 0, 1, 'Deduction_Per', 'Deduction', 'Deduction', 'Deduction_Per', 0, NULL, '2017-07-01', NULL);

                        INSERT INTO StructureDetail (Code, Sr, Charges, Charge_Type, Value_Type, Value, Calculation, BaseColumn, PostAc, PostAcFromColumn, DrCr, LineItem, AffectCost, Active, Percentage, Amount, VisibleInMaster, VisibleInMasterLine, VisibleInTransactionLine, VisibleInTransactionFooter, HeaderPerField, HeaderAmtField, LineAmtField, LinePerField, GridDisplayIndex, UpLoadDate, WEF, InactiveDate)
                        VALUES ('GstJob', 55, 'RO', 'Charges', 'Round_Off', NULL, 'ROUND(({STOT1}+{OC}-{DED}),0)-({STOT1}+{OC}-{DED})', 'NET AMOUNT', 'ROFF', NULL, 'Dr', 0, 1, NULL, 0, 0, 0, 0, 0, 1, NULL, 'Round_Off', 'Round_Off', NULL, 0, NULL, '2017-07-01', NULL);

                        INSERT INTO StructureDetail (Code, Sr, Charges, Charge_Type, Value_Type, Value, Calculation, BaseColumn, PostAc, PostAcFromColumn, DrCr, LineItem, AffectCost, Active, Percentage, Amount, VisibleInMaster, VisibleInMasterLine, VisibleInTransactionLine, VisibleInTransactionFooter, HeaderPerField, HeaderAmtField, LineAmtField, LinePerField, GridDisplayIndex, UpLoadDate, WEF, InactiveDate)
                        VALUES ('GstJob', 60, 'NAMT', 'Charges', 'FixedValue', NULL, '{STOT1}+{OC}-{DED}+{RO}', NULL, '|PARTY|', NULL, 'Cr', 0, 1, NULL, 0, 0, 0, 0, 0, 1, NULL, 'Net_Amount', 'Net_Amount', NULL, 0, NULL, '2017-07-01', NULL);
                "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)

            mQry = "UPDATE StructureDetail SET VisibleInTransactionLine = 0 WHERE Code = 'GstJob'"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If
    End Sub
    Private Sub FConfigure_Process(ClsObj As ClsMain)
        If AgL.Dman_Execute("Select Count(*) from SubGroupTypeSetting Where SubgroupType = '" & SubgroupType.Process & "'", AgL.GcnMain).ExecuteScalar = 0 Then
            mQry = "INSERT INTO SubGroupTypeSetting
                    (SubgroupType, Div_Code, Site_Code)
                    VALUES('" & SubgroupType.Process & "', Null, Null); "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If

        ClsObj.FSeedSingleIfNotExist_Subgroup("PMain", "Main Process", SubgroupType.Process, "0028", "R", "Expense", "", "", AgL.PubDivCode, AgL.PubSiteCode, "", "System Defined")
        ClsObj.FSeedSingleIfNotExist_Subgroup(Process_Alter, "Alteration", SubgroupType.Process, "0028", "R", "Expense", "", "+PMain", AgL.PubDivCode, AgL.PubSiteCode, "", "")

        mQry = " UPDATE SubGroup Set Status = 'InActive' Where SubCode = 'PMain'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
    End Sub
    Private Sub FConfigure_JobWorker(ClsObj As ClsMain)
        If AgL.FillData("Select * from SubGroupType Where SubgroupType='" & SubgroupType.Jobworker & "'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
            mQry = " 
                        Insert Into SubGroupType (SubgroupType, IsCustomUI, IsActive, Parent)
                        Values ('" & SubgroupType.Jobworker & "', 0,1, '" & SubgroupType.Supplier & "');                    "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If


        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubgroupType.Jobworker, "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.SubgroupType, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubgroupType.Jobworker, "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.Code, 0, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubgroupType.Jobworker, "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.Name, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubgroupType.Jobworker, "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.PrintingDescription, 1, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubgroupType.Jobworker, "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.Address, 1, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubgroupType.Jobworker, "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.City, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubgroupType.Jobworker, "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.Pincode, 1, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubgroupType.Jobworker, "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.ContactNo, 1, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubgroupType.Jobworker, "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.Mobile, 1, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubgroupType.Jobworker, "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.Email, 1, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubgroupType.Jobworker, "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.AcGroup, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubgroupType.Jobworker, "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.SalesTaxGroup, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubgroupType.Jobworker, "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.ContactPerson, 0, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubgroupType.Jobworker, "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.SalesTaxNo, 1, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubgroupType.Jobworker, "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.PanNo, 1, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubgroupType.Jobworker, "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.AadharNo, 1, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubgroupType.Jobworker, "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.Parent, 0, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubgroupType.Jobworker, "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.Area, 0, 0)
        If ClsMain.IsScopeOfWorkContains(IndustryType.CommonModules.PurchaseAgentModule) Then
            ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubgroupType.Jobworker, "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.Agent, 0, 0)
        End If
        'If ClsMain.IsScopeOfWorkContains(IndustryType.CommonModules.PurchaseTransportModule) Then
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubgroupType.Jobworker, "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.Transporter, 0, 0)
        'End If
        If ClsMain.IsScopeOfWorkContains(IndustryType.CommonModules.PurchaseInterestModule) Then
            ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubgroupType.Jobworker, "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.InterestSlab, 0, 0)
        End If

        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubgroupType.Jobworker, "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.Distance, 0, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubgroupType.Jobworker, "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.Discount, 0, 0)
        If ClsMain.IsScopeOfWorkContains(IndustryType.CommonModules.PurchaseInterestModule) Then
            ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubgroupType.Jobworker, "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.CreditDays, 0, 0)
        End If
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubgroupType.Jobworker, "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.RateType, 1, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubgroupType.Jobworker, "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.CreditLimit, 0, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubgroupType.Jobworker, "Dgl1", FrmPerson.hcBankName, 0, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubgroupType.Jobworker, "Dgl1", FrmPerson.hcBankAccount, 0, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubgroupType.Jobworker, "Dgl1", FrmPerson.hcBankIFSC, 0, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubgroupType.Jobworker, "Dgl1", FrmPerson.hcShowAccountInOtherDivisions, 0, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubgroupType.Jobworker, "Dgl1", FrmPerson.hcShowAccountInOtherSites, 0, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubgroupType.Jobworker, "Dgl1", FrmPerson.hcWeekOffDays, 0, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubgroupType.Jobworker, "Dgl1", FrmPerson.hcRelationshipExecutive, 0, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubgroupType.Jobworker, "Dgl1", FrmPerson.hcReligion)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubgroupType.Jobworker, "Dgl1", FrmPerson.hcCaste)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubgroupType.Jobworker, "Dgl1", FrmPerson.hcProcesses, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubgroupType.Jobworker, "Dgl1", FrmPerson.hcReconciliationUpToDate, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubgroupType.Jobworker, "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.Remarks, 1, 0)

        If ClsMain.IsScopeOfWorkContains(IndustryType.CommonModules.TdsModule) Then
            ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubgroupType.Jobworker, "Dgl1", FrmPerson.hcTdsGroup, 1, 1)
            ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubgroupType.Jobworker, "Dgl1", FrmPerson.hcTdsCategory, 1, 1)
        End If
    End Sub
End Class
