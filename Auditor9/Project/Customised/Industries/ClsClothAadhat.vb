Imports AgLibrary.ClsMain.agConstants
Imports Customised.ClsMain

Public Class ClsClothAadhat
    Private mQry As String = ""
    Public Sub FSeedData_ClothAadhatIndustry()
        Dim ClsObj As New Customised.ClsMain(AgL)
        Try
            If ClsMain.IsScopeOfWorkContains(IndustryType.SubIndustryType.AadhatModule) Then
                FConfigure_SaleInvoice(ClsObj)
                FConfigure_SaleReturn(ClsObj)
                FConfigure_OpeningEntry(ClsObj)
                FConfigure_CommonSetting(ClsObj)
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " In FSeedData_TextileIndustry")
        End Try
    End Sub
    Private Sub FConfigure_SaleInvoice(ClsObj As ClsMain)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoice, "Dgl2", FrmSaleInvoiceDirect_WithDimension.HcAmsDocNo, 1,,, "Ams Inv.No.")
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoice, "Dgl2", FrmSaleInvoiceDirect_WithDimension.HcAmsDocDate, 1,,, "Ams Inv.Date")
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoice, "Dgl2", FrmSaleInvoiceDirect_WithDimension.HcAmsDocNetAmount, 1,,, "Ams Inv.Amt")

        ClsObj.FUpdateSeed_EntryLineUISetting("FrmSaleInvoiceDirect", "", Ncat.SaleInvoice, "", "", "", "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1DiscountAmount, 0, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmSaleInvoiceDirect", "", Ncat.SaleInvoice, "", "", "", "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1AdditionalDiscountAmount, 0, 0, 1, "")

        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoice, "DglPurchase", FrmSaleInvoiceDirect_WithDimension.Col5ItemGroup, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoice, "DglPurchase", FrmSaleInvoiceDirect_WithDimension.Col5ParentSupplier, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoice, "DglPurchase", FrmSaleInvoiceDirect_WithDimension.Col5Supplier, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoice, "DglPurchase", FrmSaleInvoiceDirect_WithDimension.Col5PlaceOfSupply, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoice, "DglPurchase", FrmSaleInvoiceDirect_WithDimension.Col5PurchInvoiceNo, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoice, "DglPurchase", FrmSaleInvoiceDirect_WithDimension.Col5PurchInvoiceDate, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoice, "DglPurchase", FrmSaleInvoiceDirect_WithDimension.Col5GrossAmount, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoice, "DglPurchase", FrmSaleInvoiceDirect_WithDimension.Col5TotalTax, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoice, "DglPurchase", FrmSaleInvoiceDirect_WithDimension.Col5OtherCharge, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoice, "DglPurchase", FrmSaleInvoiceDirect_WithDimension.Col5OtherCharge1, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoice, "DglPurchase", FrmSaleInvoiceDirect_WithDimension.Col5Deduction, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoice, "DglPurchase", FrmSaleInvoiceDirect_WithDimension.Col5NetAmount, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoice, "DglPurchase", FrmSaleInvoiceDirect_WithDimension.Col5CommissionAmount, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoice, "DglPurchase", FrmSaleInvoiceDirect_WithDimension.Col5AdditionalCommissionAmount, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoice, "DglPurchase", FrmSaleInvoiceDirect_WithDimension.Col5AmsDocNo, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoice, "DglPurchase", FrmSaleInvoiceDirect_WithDimension.Col5AmsDocDate, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleInvoice, "DglPurchase", FrmSaleInvoiceDirect_WithDimension.Col5AmsDocAmount, False)

        mQry = "UPDATE Setting SET Value = '" & Ncat.PurchaseInvoice & "'
                WHERE NCat = '" & Ncat.SaleInvoice & "'
                AND FieldName = '" & ClsMain.SettingFields.GeneratedEntryV_TypeForAadhat & "' "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
    End Sub
    Private Sub FConfigure_SaleReturn(ClsObj As ClsMain)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "Dgl2", FrmSaleInvoiceDirect_WithDimension.HcAmsDocNo, 1,,, "Ams Ret.No.")
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "Dgl2", FrmSaleInvoiceDirect_WithDimension.HcAmsDocDate, 1,,, "Ams Ret.Date")
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "Dgl2", FrmSaleInvoiceDirect_WithDimension.HcAmsDocNetAmount, 1,,, "Ams Ret.Amt")

        ClsObj.FUpdateSeed_EntryLineUISetting("FrmSaleInvoiceDirect", "", Ncat.SaleReturn, "", "", "", "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1DiscountAmount, 0, 0, 1, "")
        ClsObj.FUpdateSeed_EntryLineUISetting("FrmSaleInvoiceDirect", "", Ncat.SaleReturn, "", "", "", "Dgl1", FrmSaleInvoiceDirect_WithDimension.Col1AdditionalDiscountAmount, 0, 0, 1, "")


        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "DglPurchase", FrmSaleInvoiceDirect_WithDimension.Col5ItemGroup, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "DglPurchase", FrmSaleInvoiceDirect_WithDimension.Col5ParentSupplier, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "DglPurchase", FrmSaleInvoiceDirect_WithDimension.Col5Supplier, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "DglPurchase", FrmSaleInvoiceDirect_WithDimension.Col5PlaceOfSupply, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "DglPurchase", FrmSaleInvoiceDirect_WithDimension.Col5PurchInvoiceNo, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "DglPurchase", FrmSaleInvoiceDirect_WithDimension.Col5PurchInvoiceDate, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "DglPurchase", FrmSaleInvoiceDirect_WithDimension.Col5GrossAmount, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "DglPurchase", FrmSaleInvoiceDirect_WithDimension.Col5TotalTax, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "DglPurchase", FrmSaleInvoiceDirect_WithDimension.Col5OtherCharge, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "DglPurchase", FrmSaleInvoiceDirect_WithDimension.Col5OtherCharge1, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "DglPurchase", FrmSaleInvoiceDirect_WithDimension.Col5Deduction, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "DglPurchase", FrmSaleInvoiceDirect_WithDimension.Col5NetAmount, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "DglPurchase", FrmSaleInvoiceDirect_WithDimension.Col5AmsDocNo, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "DglPurchase", FrmSaleInvoiceDirect_WithDimension.Col5AmsDocDate, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "DglPurchase", FrmSaleInvoiceDirect_WithDimension.Col5AmsDocAmount, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "DglPurchase", FrmSaleInvoiceDirect_WithDimension.Col5CommissionAmount, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmSaleInvoiceDirect", Ncat.SaleReturn, "DglPurchase", FrmSaleInvoiceDirect_WithDimension.Col5AdditionalCommissionAmount, True)


        mQry = "UPDATE Setting SET Value = '" & Ncat.PurchaseReturn & "'
                WHERE NCat = '" & Ncat.SaleReturn & "'
                AND FieldName = '" & ClsMain.SettingFields.GeneratedEntryV_TypeForAadhat & "' "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
    End Sub
    Private Sub FConfigure_OpeningEntry(ClsObj As ClsMain)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmJournalEntry", Ncat.OpeningBalance, "Dgl1", FrmJournalEntry.Col1AmsReferenceNo, True,, "Ams Bill No")
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmJournalEntry", Ncat.OpeningBalance, "Dgl1", FrmJournalEntry.Col1AmsReferenceDate, True,, "Ams Bill Date")
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmJournalEntry", Ncat.OpeningBalance, "Dgl1", FrmJournalEntry.Col1AmsReferenceAmount, True,, "Ams Bill Amount")
    End Sub

    Private Sub FConfigure_CommonSetting(ClsObj As ClsMain)
        ClsObj.FUpdateSeed_Setting(SettingType.General, "", "", SettingFields.LineDiscountCaption, "Pcs Less", AgDataType.Text, "50",,,,,,, ,, , "+SUPPORT")
    End Sub
End Class
