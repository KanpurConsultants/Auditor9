Public Class ClsFunction
    Dim WithEvents ObjRepFormGlobal As AgLibrary.RepFormGlobal
    Dim WithEvents ReportFrm As ReportLayout.FrmReportLayout
    Dim CRepProc As ClsReportProcedures

    Public Function FOpen(ByVal StrSender As String, ByVal StrSenderText As String, Optional ByVal IsEntryPoint As Boolean = True, Optional ByVal StrSenderModule As String = "")
        Dim FrmObj As Form
        Dim StrUserPermission As String
        Dim DTUP As New DataTable
        Dim ADMain As OleDb.OleDbDataAdapter = Nothing
        Dim MDI As New MDIMain

        'For User Permission Open
        If StrSenderModule <> "" Then
            StrUserPermission = AgIniVar.FunGetUserPermission(StrSenderModule, StrSender, StrSenderText, DTUP)
        Else
            StrUserPermission = AgIniVar.FunGetUserPermission(ClsMain.ModuleName, StrSender, StrSenderText, DTUP)
        End If
        ''For User Permission End 

        If IsEntryPoint Then
            Select Case StrSender
                Case MDI.MnuAdjustmentIssueEntry.Name
                    FrmObj = New FrmAdjustmentIssue(StrUserPermission, DTUP, "ADISS")

                Case MDI.MnuAdjustSaleInvoices.Name
                    FrmObj = New FrmSaleInvoiceAdj

                Case MDI.MnuGodownMaster.Name
                    FrmObj = New Store.FrmGodown(StrUserPermission, DTUP)


                Case MDI.MnuSaleInvoiceDetailEntry.Name
                    FrmObj = New FrmSaleInvoiceDetail(StrUserPermission, DTUP)

                Case MDI.MnuSaleInvoice.Name
                    FrmObj = New FrmSaleInvoice(StrUserPermission, DTUP, AgTemplate.ClsMain.Temp_NCat.SaleInvoice)

                Case MDI.MnuOpeningStockEntry.Name
                    FrmObj = New Purchase.FrmPurchChallan(StrUserPermission, DTUP, AgTemplate.ClsMain.Temp_NCat.StockOpening)
                    CType(FrmObj, Purchase.FrmPurchChallan).RestrictFinancialYearRecord = False

                Case MDI.MnuVatCommodityCodeMaster.Name
                    FrmObj = New FrmVatCommodityCode(StrUserPermission, DTUP)

                Case MDI.MnuManufacturerMaster.Name
                    FrmObj = New FrmManufacturer(StrUserPermission, DTUP)

                Case MDI.MnuRateList.Name
                    FrmObj = New FrmRateList(StrUserPermission, DTUP)

                Case MDI.MnuItemGroupMaster.Name
                    FrmObj = New FrmItemGroup(StrUserPermission, DTUP)

                Case MDI.MnuItemCategoryMaster.Name
                    FrmObj = New FrmItemCategory(StrUserPermission, DTUP)

                Case MDI.MnuItemMaster.Name
                    FrmObj = New FrmItem(StrUserPermission, DTUP)

                Case MDI.MnuRateTypeMaster.Name
                    FrmObj = New FrmRateType(StrUserPermission, DTUP)

                Case MDI.MnuAgentMaster.Name
                    FrmObj = New FrmParty(StrUserPermission, DTUP)
                    CType(FrmObj, FrmParty).MasterType = ClsMain.MasterType.Agent
                    CType(FrmObj, FrmParty).SubGroupNature = FrmParty.ESubgroupNature.Supplier

                Case MDI.MnuCustomerMaster.Name
                    FrmObj = New FrmParty(StrUserPermission, DTUP)
                    CType(FrmObj, FrmParty).MasterType = ClsMain.MasterType.Customer
                    CType(FrmObj, FrmParty).SubGroupNature = FrmParty.ESubgroupNature.Customer

                Case MDI.MnuSupplierMaster.Name
                    FrmObj = New FrmParty(StrUserPermission, DTUP)
                    CType(FrmObj, FrmParty).MasterType = ClsMain.MasterType.Supplier
                    CType(FrmObj, FrmParty).SubGroupNature = FrmParty.ESubgroupNature.Supplier

                Case Else
                    FrmObj = Nothing
            End Select
        Else
            ReportFrm = New ReportLayout.FrmReportLayout("", "", StrSenderText, "")
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
End Class

