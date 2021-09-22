
Public Class ClsReportProcedures

#Region "Danger Zone"
    Dim StrArr1() As String = Nothing, StrArr2() As String = Nothing, StrArr3() As String = Nothing, StrArr4() As String = Nothing, StrArr5() As String = Nothing

    Dim mGRepFormName As String = ""

    Dim WithEvents ReportFrm As ReportLayout.FrmReportLayout

    Public Property GRepFormName() As String
        Get
            GRepFormName = mGRepFormName
        End Get
        Set(ByVal value As String)
            mGRepFormName = value
        End Set
    End Property

#End Region

#Region "Common Reports Constant"
    Private Const CityList As String = "CityList"
    Private Const UserWiseEntryReport As String = "UserWiseEntryReport"
    Private Const UserWiseEntryTargetReport As String = "UserWiseEntryTargetReport"
#End Region

#Region "Reports Constant"
    Private Const SaleChallanReport As String = "SaleChallanReport"
    Private Const SaleInvoiceReport As String = "SaleInvoiceReport"
    Private Const PurchaseChallanReport As String = "PurchaseChallanReport"
    Private Const PurchaseInvoiceReport As String = "PurchaseInvoiceReport"
    Private Const PurchaseAdviseReport As String = "PurchaseAdviseReport"
    Private Const ItemExpiryReport As String = "ItemExpiryReport"
    Private Const PurchaseIndentReport As String = "PurchaseIndentReport"
    Private Const VATReports As String = "VATReports"
    Private Const PartyOutstandingReport As String = "PartyOutstandingReport"
    Private Const BillWiseProfitability As String = "BillWiseProfitability"
    Private Const DebtorsOutstandingOverDue As String = "DebtorsOutstandingOverDue"
    Private Const WeavingOrderRatio As String = "WeavingOrderRatio"
#End Region

#Region "Queries Definition"
    Dim mHelpCityQry$ = "Select 'o' As Tick, CityCode, CityName From City "
    Dim mHelpStateQry$ = "Select 'o' As Tick, State_Code, State_Desc From State "
    Dim mHelpUserQry$ = "Select 'o' As Tick, User_Name As Code, User_Name As [User] From UserMast "
    Dim mHelpSiteQry$ = "Select 'o' As Tick, Code, Name As [Site] From SiteMast Where " & AgL.PubSiteCondition("Code", AgL.PubSiteCode) & " "
    Dim mHelpItemQry$ = "Select 'o' As Tick, Code, Description As [Item] From Item "
    Dim mHelpItemGroupQry$ = "Select 'o' As Tick, Code, Description As [Item Group] From ItemGroup "
    Dim mHelpVendorQry$ = " Select 'o' As Tick,  H.SubCode As Code, Sg.DispName AS Vendor FROM Vendor H LEFT JOIN SubGroup Sg ON H.SubCode = Sg.SubCode "
    Dim mHelpTableQry$ = "Select 'o' As Tick, H.Code, H.Description AS [Table] FROM HT_Table H "
    Dim mHelpPaymentModeQry$ = "Select 'o' As Tick, '" & ClsMain.PaymentMode.Cash & "' As Code, '" & ClsMain.PaymentMode.Cash & "' As Description " & _
                                " UNION ALL " & _
                                " Select 'o' As Tick, '" & ClsMain.PaymentMode.Credit & "' As Code, '" & ClsMain.PaymentMode.Credit & "' As Description "
    Dim mHelpOutletQry$ = "Select 'o' As Tick, H.Code, H.Description AS [Table] FROM Outlet H "
    Dim mHelpStewardQry$ = "Select 'o' As Tick,  Sg.SubCode AS Code, Sg.DispName AS Steward FROM SubGroup Sg  "
    Dim mHelpPartyQry$ = " Select 'o' As Tick,  Sg.SubCode As Code, Sg.DispName AS Party FROM SubGroup Sg Where Sg.Nature In ('Customer','Supplier','Cash') "
    Dim mHelpAgentQry$ = " Select Sg.SubCode As Code, Sg.DispName AS Agent FROM SubGroup Sg Where Sg.MasterType = '" & ClsMain.MasterType.Agent & "' "
    Dim mHelpSaleOrderQry$ = " Select 'o' As Tick,  H.DocID AS Code, H.V_Type + '-' + H.ReferenceNo  FROM SaleOrder H "
    Dim mHelpSaleBillQry$ = " SELECT 'o' As Tick,DocId, ReferenceNo AS BillNo, V_Date AS Date FROM SaleChallan "
    Dim mHelpItemReportingGroupQry$ = "Select 'o' As Tick,I.Code,I.Description  AS ItemReportingGroup FROM ItemReportingGroup I "
#End Region

    Dim DsRep As DataSet = Nothing, DsRep1 As DataSet = Nothing, DsRep2 As DataSet = Nothing
    Dim mQry$ = "", RepName$ = "", RepTitle$ = "", OrderByStr$ = ""

    Dim StrMonth$ = ""
    Dim StrQuarter$ = ""
    Dim StrFinancialYear$ = ""
    Dim StrTaxPeriod$ = ""

#Region "Initializing Grid"
    Public Sub Ini_Grid()
        Try
            Dim I As Integer = 0
            Select Case GRepFormName
                Case SaleInvoiceReport, SaleChallanReport
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubEndDate)
                    ReportFrm.CreateHelpGrid("Report Type", "Report Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, "Select 'Summary' as Code, 'Summary' as Name Union All Select 'Month Wise Summary' as Code, 'Month Wise Summary' as Name Union All Select 'Party Wise Summary' as Code, 'Party Wise Summary' as Name Union All Select 'Agent Wise Summary' as Code, 'Agent Wise Summary' as Name Union All Select 'Agent-Item Wise Summary' as Code, 'Agent-Item Wise Summary' as Name Union All Select 'Item Wise Detail' as Code, 'Item Wise Detail' as Name", "Summary", , , , , False)
                    ReportFrm.CreateHelpGrid("Party", "Party", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpPartyQry)
                    ReportFrm.CreateHelpGrid("Item", "Item", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemQry)
                    ReportFrm.CreateHelpGrid("Site", "Site", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpSiteQry)
                    ReportFrm.CreateHelpGrid("VoucherType", "Voucher Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, FGetVoucher_TypeQry("SaleInvoice"))
                    ReportFrm.CreateHelpGrid("CashCredit", "Cash/Credit", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, "Select 'Cash' as Code, 'Cash' as Name Union All Select 'Credit' as Code, 'Credit' as Name Union All Select 'Both' as Code, 'Both' as Name", "Both", , , , , False)
                    ReportFrm.CreateHelpGrid("Agent", "Agent", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, mHelpAgentQry)
                    ReportFrm.CreateHelpGrid("Team/Individual", "Team/Individual", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, "Select 'Individual' as Code, 'Individual' as Name Union All Select 'Team' as Code, 'Team' as Name", "Team", , , , , False)

                Case PurchaseInvoiceReport, PurchaseChallanReport
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubEndDate)
                    ReportFrm.CreateHelpGrid("Report Type", "Report Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, "Select 'Summary' as Code, 'Summary' as Name Union All Select 'Month Wise Summary' as Code, 'Party Wise Summary' as Name Union All Select 'Party Wise Summary' as Code, 'Month Wise Summary' as Name Union All Select 'Item Wise Detail' as Code, 'Item Wise Detail' as Name", "Summary", , , , , False)
                    ReportFrm.CreateHelpGrid("Item", "Item", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemQry)
                    ReportFrm.CreateHelpGrid("Party", "Party", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpPartyQry)
                    ReportFrm.CreateHelpGrid("Site", "Site", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpSiteQry)
                    ReportFrm.CreateHelpGrid("VoucherType", "Voucher Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, FGetVoucher_TypeQry("PurchInvoice"))

                Case PurchaseAdviseReport
                    ReportFrm.CreateHelpGrid("AsOnDate", "As On Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubLoginDate)
                    ReportFrm.CreateHelpGrid("Item", "Item", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemQry)
                    ReportFrm.CreateHelpGrid("ItemGroup", "Item Group", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemGroupQry)
                    ReportFrm.CreateHelpGrid("Item ActiveInActive", "Item Active/InActive", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, "Select 'Active' As Code, 'Active' as Name Union All Select 'InActive' as Code, 'InActive' as Name Union All Select 'Both' as Code, 'Both' as Name", "Active")

                Case ItemExpiryReport
                    ReportFrm.CreateHelpGrid("AsOnDate", "Before Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubLoginDate)
                    ReportFrm.CreateHelpGrid("Item", "Item", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemQry)
                    ReportFrm.CreateHelpGrid("ItemGroup", "Item Group", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemGroupQry)
                    ReportFrm.CreateHelpGrid("Item ActiveInActive", "Item Active/InActive", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, "Select 'Active' As Code, 'Active' as Name Union All Select 'InActive' as Code, 'InActive' as Name Union All Select 'Both' as Code, 'Both' as Name", "Active")

                Case PurchaseIndentReport
                    ReportFrm.CreateHelpGrid("FromDate", "Order From Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "Order Upto Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubEndDate)
                    ReportFrm.CreateHelpGrid("Item", "Item", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemQry)

                Case VATReports
                    mQry = "Select 'Annexure A' AS Code, 'Annexure A' AS Name "
                    mQry += "Union All Select 'Annexure A1' as Code, 'Annexure A1' AS Name "
                    mQry += "Union All Select 'Annexure B' as Code, 'Annexure B' as Name "
                    mQry += "Union All Select 'Annexure B1' as Code, 'Annexure B1' as Name "
                    mQry += "Union All Select 'Annexure C' as Code, 'Annexure C' as Name "
                    mQry += "Union All Select 'Return Of Tax' as Code, 'Return Of Tax' as Name "
                    mQry += "Union All Select 'Return Form 24' as Code, 'Return Form 24' as Name "
                    ReportFrm.CreateHelpGrid("FromDate", "Order From Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "Order Upto Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubEndDate)
                    ReportFrm.CreateHelpGrid("Report Type", "Report Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, mQry, "", , , , , False)

                Case PartyOutstandingReport
                    ReportFrm.CreateHelpGrid("As On Date", "As On Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubLoginDate)
                    ReportFrm.CreateHelpGrid("Party", "Party", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpPartyQry)

                Case BillWiseProfitability
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubEndDate)
                    ReportFrm.CreateHelpGrid("Party", "Party", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpPartyQry)
                    ReportFrm.CreateHelpGrid("Item", "Item", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemQry)
                    ReportFrm.CreateHelpGrid("Bill No", "Bill No", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpSaleBillQry)
                    ReportFrm.CreateHelpGrid("Item Reporting Group", "Item Reporting Group", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemReportingGroupQry)

                Case DebtorsOutstandingOverDue
                    ReportFrm.CreateHelpGrid("As On Date", "As On Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubLoginDate)
                    ReportFrm.CreateHelpGrid("NoofDays", "No of Days", ReportLayout.FrmReportLayout.FieldFilterDataType.NumericType, ReportLayout.FrmReportLayout.FieldDataType.NumericType, "", "")
                    ReportFrm.CreateHelpGrid("Party", "Party", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpPartyQry)


                Case WeavingOrderRatio
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubEndDate)
                    ReportFrm.CreateHelpGrid("Party", "Party", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpPartyQry)


            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
#End Region

    Private Function FGetVoucher_TypeQry(ByVal TableName As String) As String
        FGetVoucher_TypeQry = " SELECT Distinct 'o' As Tick, H.V_Type AS Code, Vt.Description AS [Voucher Type] " & _
                                " FROM " & TableName & " H  " & _
                                " LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type "
    End Function

    Private Sub ObjRepFormGlobal_ProcessReport() Handles ReportFrm.ProcessReport
        Select Case mGRepFormName
            Case SaleChallanReport
                ProcSaleReport("SaleChallan", "SaleChallanDetail")

            Case SaleInvoiceReport
                ProcSaleReport("SaleInvoice", "SaleInvoiceDetail")

            Case PurchaseChallanReport
                ProcPurchaseInvoiceReport("PurchChallan", "PurchChallanDetail")

            Case PurchaseInvoiceReport
                ProcPurchaseInvoiceReport("PurchInvoice", "PurchInvoiceDetail")

            Case PurchaseAdviseReport
                ProcPurchaseAdviseReport()

            Case ItemExpiryReport
                ProcItemExpiryReport()

            Case PurchaseIndentReport
                ProcPurchaseIndentReport()

            Case VATReports
                ProcVatAnexureReports()

            Case PartyOutstandingReport
                ProcPartyOutstandingReport()

            Case BillWiseProfitability
                ProcBillWiseProfitabilty()

            Case DebtorsOutstandingOverDue
                ProcDebtorsOutstandingOverDue()

        End Select
    End Sub

    Public Sub New(ByVal mReportFrm As ReportLayout.FrmReportLayout)
        ReportFrm = mReportFrm
    End Sub

#Region "Sale Report"
    Private Sub ProcSaleReport(ByVal HeaderTable As String, ByVal LineTable As String)
        Try
            RepName = "Trade_SaleReport" : RepTitle = "Sale Report"


            Dim mCondStr$ = ""
            Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"

            If ReportFrm.FGetText(2) = "Summary" Then
                RepName = "Trade_SaleReport" : RepTitle = "Sale Invoice Report"
            ElseIf ReportFrm.FGetText(2) = "Party Wise Summary" Then
                RepName = "Trade_SaleReportSummary" : RepTitle = "Sale Invoice Report (" & ReportFrm.FGetText(2) & ")"
                strGrpFld = "Party.Name"
                strGrpFldDesc = "Party.Name + ',' + IsNull(City.CityName,'')"
                strGrpFldHead = "'Party Name'"
            ElseIf ReportFrm.FGetText(2) = "Agent Wise Summary" Then
                RepName = "Trade_SaleReportSummary" : RepTitle = "Sale Invoice Report (" & ReportFrm.FGetText(2) & ")"
                strGrpFld = "Agent.Name"
                strGrpFldDesc = "Agent.Name + ',' + IsNull(City.CityName,'')"
                strGrpFldHead = "'AgentName'"
            ElseIf ReportFrm.FGetText(2) = "Agent-Item Wise Summary" Then
                RepName = "Trade_SaleReportAgentItemWiseSummary" : RepTitle = "Sale Invoice Report (" & ReportFrm.FGetText(2) & ")"
            ElseIf ReportFrm.FGetText(2) = "Month Wise Summary" Then
                RepName = "Trade_SaleReportSummary" : RepTitle = "Sale Invoice Report (" & ReportFrm.FGetText(2) & ")"
                strGrpFld = "Substring(convert(nvarchar,H.V_Date,11),0,6)"
                strGrpFldDesc = "Replace(SubString(Convert(VARCHAR,H.v_Date,6),4,6),' ','-')"
                strGrpFldHead = "'Month'"
            ElseIf ReportFrm.FGetText(2) = "Item Wise Detail" Then
                RepName = "Trade_ItemWiseSaleReport" : RepTitle = "Item Wise Sale Report"
            End If


            mCondStr = " Where 1 = 1 "

            If ReportFrm.FGetText(7) = "Cash" Then
                mCondStr = mCondStr & " AND Sg.Nature = 'Cash'"
            ElseIf ReportFrm.FGetText(7) = "Credit" Then
                mCondStr = mCondStr & " AND Sg.Nature <> 'Cash'"
            End If


            mCondStr = mCondStr & " AND H.V_Date Between '" & ReportFrm.FGetText(0) & "' And '" & ReportFrm.FGetText(1) & "' "
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.BillToParty", 3)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Item", 4)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.Site_Code", 5)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.V_Type", 6)

            If ReportFrm.FGetText(8) <> "All" Then
                If ReportFrm.FGetText(9) = "Team" Then
                    mCondStr += " And CharIndex('" & AgL.XNull(ReportFrm.FGetCode(8)) & "',H.Upline) > 0 "
                Else
                    mCondStr += " And H.Agent = '" & ReportFrm.FGetCode(8) & "'"
                End If
            End If

            mQry = " SELECT " & strGrpFld & " as GrpField, " & strGrpFldDesc & " as GrpFieldDesc, " & strGrpFldHead & " as GrpFieldHead, " & _
                    " H.DocID, H.V_Date, " & _
                    " Party.Name + ',' + IsNull(City.CityName,'') As SaleToPartyName , " & _
                    " Agent.Name + ',' + IsNull(City.CityName,'') As AgentName , " & _
                    " H.ReferenceNo,  " & _
                    " I.Description As ItemDesc, " & _
                    " L.Net_Amount, L.Qty, L.Unit " & _
                    " FROM " & HeaderTable & " H " & _
                    " Left Join " & LineTable & " L On H.DocID = L.DocID " & _
                    " Left Join Item I On L.Item = I.Code " & _
                    " Left Join Subgroup Party On H.SaleToParty = Party.SubCode " & _
                    " Left Join SubGroup Agent On H.Agent = Agent.SubCode " & _
                    " LEFT JOIN City On Party.CityCode = City.CityCode " & _
                    " LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type " & mCondStr & OrderByStr
            DsRep = AgL.FillData(mQry, AgL.GCn)

            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records to Print!")

            ReportFrm.PrintReport(DsRep, RepName, RepTitle)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
#End Region

#Region "Purchase Invoice Report"
    Private Sub ProcPurchaseInvoiceReport(ByVal HeaderTable As String, ByVal LineTable As String)
        Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"
        Try
            If ReportFrm.FGetText(2) = "Summary" Then
                RepName = "Trade_PurchaseReport" : RepTitle = "Purchase Invoice Report"
            ElseIf ReportFrm.FGetText(2) = "Party Wise Summary" Then
                RepName = "Trade_PurchaseReportSummary" : RepTitle = "Purchase Invoice Report (" & ReportFrm.FGetText(2) & ")"
                strGrpFld = "Sg.Name"
                strGrpFldDesc = "Sg.Name + ',' + IsNull(C.CityName,'')"
                strGrpFldHead = "'Party Name'"
            ElseIf ReportFrm.FGetText(2) = "Month Wise Summary" Then
                RepName = "Trade_PurchaseReportSummary" : RepTitle = "Purchase Invoice Report (" & ReportFrm.FGetText(2) & ")"
                strGrpFld = "Substring(convert(nvarchar,H.V_Date,11),0,6)"
                strGrpFldDesc = "Replace(SubString(Convert(VARCHAR,H.v_Date,6),4,6),' ','-')"
                strGrpFldHead = "'Month'"
            ElseIf ReportFrm.FGetText(2) = "Item Wise Detail" Then
                RepName = "Trade_ItemWisePurchaseReport" : RepTitle = "Item Wise Purchase Invoice Report"
            End If

            Dim mCondStr$ = ""
            mCondStr = " Where Vt.NCat = '" & AgTemplate.ClsMain.Temp_NCat.PurchaseInvoice & "' "

            mCondStr = mCondStr & " AND H.V_Date Between '" & ReportFrm.FGetText(0) & "' And '" & ReportFrm.FGetText(1) & "' "
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Item", 3)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.Vendor", 4)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.Site_Code", 5)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.V_Type", 6)

            mQry = " SELECT " & strGrpFld & " as GrpField, " & strGrpFldDesc & " as GrpFieldDesc, " & strGrpFldHead & " as GrpFieldHead, " & _
                        " L.DocId, L.Qty, L.Unit, L.Net_Amount, " & _
                        " I.Description AS ItemDesc, H.V_Date, H.ReferenceNo, Sg.DispName + ',' + IsNull(C.CityName,'') As VendorName, L.Remark " & _
                        " FROM " & LineTable & " L " & _
                        " LEFT JOIN " & HeaderTable & " H ON L.DocId = H.DocId " & _
                        " LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type " & _
                        " LEFT JOIN Item I ON L.Item = I.Code " & _
                        " LEFT JOIN SubGroup Sg On H.Vendor = Sg.SubCode " & _
                        " LEFT JOIN City C On Sg.CityCode = C.CityCode " & mCondStr

            DsRep = AgL.FillData(mQry, AgL.GCn)

            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records to Print!")

            ReportFrm.PrintReport(DsRep, RepName, RepTitle)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
#End Region

#Region "Purchase Advise Report"
    Private Sub ProcPurchaseAdviseReport()
        Try
            Dim mCondStr$ = ""
            Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"

            RepName = "Med_PurchaseAdviseReport" : RepTitle = "Purchase Advise Report"


            mCondStr = " Where 1 = 1 "
            mCondStr = mCondStr & " AND L.V_Date <= '" & ReportFrm.FGetText(0) & "' "
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Item", 1)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemGroup", 2)

            If ReportFrm.FGetText(3) <> "Both" Then
                mCondStr = mCondStr & " And I.Status = '" & ReportFrm.FGetText(3) & "'"
            End If

            mQry = " SELECT L.Item, Max(I.Description) AS ItemDesc, Max(I.ReorderLevel) AS ReorderLevel, " & _
                    " IsNull(Sum(L.Qty_Rec),0) - IsNull(Sum(L.Qty_Iss),0) AS StockQty " & _
                    " FROM Stock L " & _
                    " LEFT JOIN Item  I ON L.Item = I.Code " & mCondStr & _
                    " GROUP BY L.Item " & _
                    " HAVING(IsNull(Sum(L.Qty_Rec), 0) - IsNull(Sum(L.Qty_Iss), 0) <= Max(I.ReorderLevel)) "
            DsRep = AgL.FillData(mQry, AgL.GCn)

            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records to Print!")

            ReportFrm.PrintReport(DsRep, RepName, RepTitle)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
#End Region

#Region "Item Expiry Report"
    Private Sub ProcItemExpiryReport()
        Try
            Dim mCondStr$ = ""
            Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"

            RepName = "Med_ItemExpiryReport" : RepTitle = "Item Expiry Report"

            mCondStr = " Where 1 = 1 "
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Item", 1)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemGroup", 2)

            If ReportFrm.FGetText(3) <> "Both" Then
                mCondStr = mCondStr & " And I.Status = '" & ReportFrm.FGetText(3) & "'"
            End If

            mQry = " SELECT L.ReferenceDocID, L.ReferenceDocIDSr, Max(I.Description) As ItemDesc, " & _
                    " IsNull(Sum(L.Qty_Rec),0) - IsNull(Sum(L.Qty_Iss),0) AS StockQty, " & _
                    " Max(L.ExpiryDate) As ExpiryDate " & _
                    " FROM Stock L  " & _
                    " LEFT JOIN Item I ON L.Item = I.Code " & mCondStr & _
                    " GROUP BY L.ReferenceDocID, L.ReferenceDocIDSr " & _
                    " HAVING IsNull(Sum(L.Qty_Rec), 0) - IsNull(Sum(L.Qty_Iss), 0) > 0 " & _
                    " And Max(L.ExpiryDate) <= '" & ReportFrm.FGetText(0) & "' "
            DsRep = AgL.FillData(mQry, AgL.GCn)

            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records to Print!")

            ReportFrm.PrintReport(DsRep, RepName, RepTitle)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
#End Region

#Region "Purchase Indent Report"
    Private Sub ProcPurchaseIndentReport()
        Try
            RepName = "Med_PurchaseIndentReport" : RepTitle = "Purhcase Indent Report"

            Dim mCondStr$ = ""

            mCondStr = " Where 1=1 "
            mCondStr = mCondStr & " AND H.V_Date Between '" & ReportFrm.FGetText(0) & "' And '" & ReportFrm.FGetText(1) & "' "
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Item", 2)

            mQry = " SELECT H.V_Date, I.Description As ItemDesc, IsNull(VStock.CurrentStock,0) As StockQty " & _
                    " FROM PurchIndent H  " & _
                    " LEFT JOIN PurchIndentDetail L ON H.DocID = L.DocId " & _
                    " LEFT JOIN ( " & _
                    "   Select S.Item, IsNull(Sum(S.Qty_Rec),0) - IsNull(Sum(S.Qty_Iss),0) As CurrentStock " & _
                    "   From Stock S Group By S.Item " & _
                    " ) As VStock On L.Item = VStock.Item " & _
                    " LEFT JOIN Item I ON L.Item = I.Code " & mCondStr

            DsRep = AgL.FillData(mQry, AgL.GCn)

            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records to Print!")

            ReportFrm.PrintReport(DsRep, RepName, RepTitle)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
#End Region



    Private Sub FWriteTaxReturn(ByVal DsTemp As DataSet)
        Dim I As Integer = 0

        Dim ErrorMessage As String = String.Empty
        Dim OExcelHandler As New ExcelHandler()
        Dim DtSalesTaxForPurchase As DataTable = Nothing
        Dim DtAddSalesTaxForPurchase As DataTable = Nothing
        Dim DtSalesTaxForSale As DataTable = Nothing
        Dim DtAddSalesTaxForSale As DataTable = Nothing

        Dim PurchaseInOwnAcAgainstTaxInvoice$ = " SELECT Str(IsNull(Sum(H.Sales_Tax_Taxable_Amt),0),15,2) FROM View_Vat H LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type LEFT JOIN SubGroup Sg ON H.SubCode = Sg.SubCode LEFT JOIN PostingGroupSalesTaxParty S On H.SalesTaxGroupParty = S.Description WHERE Vt.Category = '" & ClsMain.Voucher_Category.Purchase & "' AND H.V_Date BETWEEN '" & ReportFrm.FGetText(0) & "' And '" & ReportFrm.FGetText(1) & "' And IsNull(H.Qty,0) > 0 And IsNull(H.Sales_Tax,0) > 0 And IsNull(S.Nature,'" & ClsMain.SalesTaxGroupPartyNature.Local & "') = '" & ClsMain.SalesTaxGroupPartyNature.Local & "'  And H.TINNo IS NOT NULL "
        Dim PurchaseInOwnAcFromNonRegisteredDealer$ = " SELECT Str(IsNull(Sum(H.Sales_Tax_Taxable_Amt),0),15,2) FROM View_Vat H LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type LEFT JOIN SubGroup Sg ON H.SubCode = Sg.SubCode LEFT JOIN PostingGroupSalesTaxParty S On H.SalesTaxGroupParty = S.Description WHERE Vt.Category = '" & ClsMain.Voucher_Category.Purchase & "' AND H.V_Date BETWEEN '" & ReportFrm.FGetText(0) & "' And '" & ReportFrm.FGetText(1) & "' And IsNull(H.Qty,0) > 0 And IsNull(H.Sales_Tax,0) > 0 And IsNull(S.Nature,'" & ClsMain.SalesTaxGroupPartyNature.Local & "') = '" & ClsMain.SalesTaxGroupPartyNature.Local & "'  And H.TINNo IS NULL "
        Dim PurchaseOfExcemptGoods$ = " SELECT Str(IsNull(Sum(H.Sales_Tax_Taxable_Amt),0),15,2) FROM View_Vat H LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type LEFT JOIN SubGroup Sg ON H.SubCode = Sg.SubCode LEFT JOIN PostingGroupSalesTaxParty S On H.SalesTaxGroupParty = S.Description WHERE Vt.Category = '" & ClsMain.Voucher_Category.Purchase & "' AND H.V_Date BETWEEN '" & ReportFrm.FGetText(0) & "' And '" & ReportFrm.FGetText(1) & "' And IsNull(H.Qty,0) > 0 And IsNull(H.Sales_Tax,0) = 0 And IsNull(S.Nature,'" & ClsMain.SalesTaxGroupPartyNature.Local & "')  = '" & ClsMain.SalesTaxGroupPartyNature.Local & "'  "
        Dim PurchaseOfExUp$ = " SELECT Str(IsNull(Sum(H.Sales_Tax_Taxable_Amt),0),15,2) FROM View_Vat H LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type LEFT JOIN SubGroup Sg ON H.SubCode = Sg.SubCode LEFT JOIN PostingGroupSalesTaxParty S On H.SalesTaxGroupParty = S.Description WHERE Vt.Category = '" & ClsMain.Voucher_Category.Purchase & "' AND H.V_Date BETWEEN '" & ReportFrm.FGetText(0) & "' And '" & ReportFrm.FGetText(1) & "' And IsNull(H.Qty,0) > 0 And IsNull(S.Nature,'" & ClsMain.SalesTaxGroupPartyNature.Local & "')  = '" & ClsMain.SalesTaxGroupPartyNature.Central & "' "

        Dim TotalPurchase$ = " SELECT Str(IsNull(Sum(H.Sales_Tax_Taxable_Amt),0),15,2) FROM View_Vat H LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type LEFT JOIN SubGroup Sg ON H.SubCode = Sg.SubCode LEFT JOIN PostingGroupSalesTaxParty S On H.SalesTaxGroupParty = S.Description WHERE Vt.Category = '" & ClsMain.Voucher_Category.Purchase & "'  AND H.V_Date BETWEEN '" & ReportFrm.FGetText(0) & "' And '" & ReportFrm.FGetText(1) & "' And IsNull(H.Qty,0) > 0 "
        Dim TotalPurchaseReturn$ = " SELECT Str(IsNull(Sum(H.Sales_Tax_Taxable_Amt),0),15,2) FROM View_Vat H LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type LEFT JOIN SubGroup Sg ON H.SubCode = Sg.SubCode LEFT JOIN PostingGroupSalesTaxParty S On H.SalesTaxGroupParty = S.Description WHERE Vt.Category = '" & ClsMain.Voucher_Category.Purchase & "'  AND H.V_Date BETWEEN '" & ReportFrm.FGetText(0) & "' And '" & ReportFrm.FGetText(1) & "' And IsNull(H.Qty,0) < 0  "

        Dim NetAmtOfPurchase$ = " SELECT Str(IsNull(Sum(H.Sales_Tax_Taxable_Amt),0),15,2) FROM View_Vat H LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type LEFT JOIN SubGroup Sg ON H.SubCode = Sg.SubCode LEFT JOIN PostingGroupSalesTaxParty S On H.SalesTaxGroupParty = S.Description WHERE Vt.Category = '" & ClsMain.Voucher_Category.Purchase & "'  AND H.V_Date BETWEEN '" & ReportFrm.FGetText(0) & "' And '" & ReportFrm.FGetText(1) & "' "
        Dim GrandTotalOfPurchase$ = " SELECT Str(IsNull(Sum(H.Sales_Tax_Taxable_Amt),0),15,2) FROM View_Vat H LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type LEFT JOIN SubGroup Sg ON H.SubCode = Sg.SubCode LEFT JOIN PostingGroupSalesTaxParty S On H.SalesTaxGroupParty = S.Description WHERE Vt.Category = '" & ClsMain.Voucher_Category.Purchase & "'  AND H.V_Date BETWEEN '" & ReportFrm.FGetText(0) & "' And '" & ReportFrm.FGetText(1) & "' "

        Dim TurnOverOfPurchaseOnePer$ = "SELECT Str(IsNull(Sum(H.Sales_Tax_Taxable_Amt),0),15,2) FROM View_Vat H LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type LEFT JOIN SubGroup Sg ON H.SubCode = Sg.SubCode LEFT JOIN PostingGroupSalesTaxParty S On H.SalesTaxGroupParty = S.Description WHERE Vt.Category = '" & ClsMain.Voucher_Category.Purchase & "'  AND H.V_Date BETWEEN '" & ReportFrm.FGetText(0) & "' And '" & ReportFrm.FGetText(1) & "' And IsNull(H.Qty,0) > 0 And IsNull(H.Sales_Tax_Per,0) = 1 "
        Dim TurnOverOfPurchaseFivePer$ = "SELECT Str(IsNull(Sum(H.Sales_Tax_Taxable_Amt),0),15,2) FROM View_Vat H LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type LEFT JOIN SubGroup Sg ON H.SubCode = Sg.SubCode LEFT JOIN PostingGroupSalesTaxParty S On H.SalesTaxGroupParty = S.Description WHERE Vt.Category = '" & ClsMain.Voucher_Category.Purchase & "'  AND H.V_Date BETWEEN '" & ReportFrm.FGetText(0) & "' And '" & ReportFrm.FGetText(1) & "' And IsNull(H.Qty,0) > 0 And IsNull(H.Sales_Tax_Per,0) = 5 "
        Dim TurnOverOfPurchaseTwelvePer$ = "SELECT Str(IsNull(Sum(H.Sales_Tax_Taxable_Amt),0),15,2) FROM View_Vat H LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type LEFT JOIN SubGroup Sg ON H.SubCode = Sg.SubCode LEFT JOIN PostingGroupSalesTaxParty S On H.SalesTaxGroupParty = S.Description WHERE Vt.Category = '" & ClsMain.Voucher_Category.Purchase & "'  AND H.V_Date BETWEEN '" & ReportFrm.FGetText(0) & "' And '" & ReportFrm.FGetText(1) & "' And IsNull(H.Qty,0) > 0 And IsNull(H.Sales_Tax_Per,0) = 12.5 "

        Dim TotalSalesTaxVatOnePerOnPurchase$ = "SELECT Str(IsNull(Sum(H.Sales_Tax),0),15,2) FROM View_Vat H LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type LEFT JOIN SubGroup Sg ON H.SubCode = Sg.SubCode LEFT JOIN PostingGroupSalesTaxParty S On H.SalesTaxGroupParty = S.Description WHERE Vt.Category = '" & ClsMain.Voucher_Category.Purchase & "'  AND H.V_Date BETWEEN '" & ReportFrm.FGetText(0) & "' And '" & ReportFrm.FGetText(1) & "' And IsNull(H.Qty,0) > 0 And IsNull(H.Sales_Tax_Per,0) = 1 "
        Dim TotalSalesTaxVatFivePerOnPurchase$ = "SELECT Str(IsNull(Sum(H.Sales_Tax),0),15,2) FROM View_Vat H LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type LEFT JOIN SubGroup Sg ON H.SubCode = Sg.SubCode LEFT JOIN PostingGroupSalesTaxParty S On H.SalesTaxGroupParty = S.Description WHERE Vt.Category = '" & ClsMain.Voucher_Category.Purchase & "'  AND H.V_Date BETWEEN '" & ReportFrm.FGetText(0) & "' And '" & ReportFrm.FGetText(1) & "' And IsNull(H.Qty,0) > 0 And IsNull(H.Sales_Tax_Per,0) = 5 "
        Dim TotalSalesTaxVatTwelvePerOnPurchase$ = "SELECT Str(IsNull(Sum(H.Sales_Tax),0),15,2) FROM View_Vat H LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type LEFT JOIN SubGroup Sg ON H.SubCode = Sg.SubCode LEFT JOIN PostingGroupSalesTaxParty S On H.SalesTaxGroupParty = S.Description WHERE Vt.Category = '" & ClsMain.Voucher_Category.Purchase & "'  AND H.V_Date BETWEEN '" & ReportFrm.FGetText(0) & "' And '" & ReportFrm.FGetText(1) & "' And IsNull(H.Qty,0) > 0 And IsNull(H.Sales_Tax_Per,0) = 12.5 "

        Dim TotalAddSalesTaxPointFivePerOnPurchase$ = "SELECT Str(IsNull(Sum(H.Additional_Sales_Tax),0),15,2) FROM View_Vat H LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type LEFT JOIN SubGroup Sg ON H.SubCode = Sg.SubCode LEFT JOIN PostingGroupSalesTaxParty S On H.SalesTaxGroupParty = S.Description WHERE Vt.Category = '" & ClsMain.Voucher_Category.Purchase & "'  AND H.V_Date BETWEEN '" & ReportFrm.FGetText(0) & "' And '" & ReportFrm.FGetText(1) & "' And IsNull(H.Qty,0) > 0 And IsNull(H.Additional_Sales_Tax_Per,0) = 0.5 "
        Dim TotalAddSalesTaxOnePerOnPurchase$ = "SELECT Str(IsNull(Sum(H.Additional_Sales_Tax),0),15,2) FROM View_Vat H LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type LEFT JOIN SubGroup Sg ON H.SubCode = Sg.SubCode LEFT JOIN PostingGroupSalesTaxParty S On H.SalesTaxGroupParty = S.Description WHERE Vt.Category = '" & ClsMain.Voucher_Category.Purchase & "'  AND H.V_Date BETWEEN '" & ReportFrm.FGetText(0) & "' And '" & ReportFrm.FGetText(1) & "' And IsNull(H.Qty,0) > 0 And IsNull(H.Additional_Sales_Tax_Per,0) = 1 "

        Dim TotalSalesAndAddSalesTaxOnPurchase$ = "SELECT Str(IsNull(Sum(H.Sales_Tax),0) + IsNull(Sum(H.Additional_Sales_Tax),0),15,2) FROM View_Vat H LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type LEFT JOIN SubGroup Sg ON H.SubCode = Sg.SubCode LEFT JOIN PostingGroupSalesTaxParty S On H.SalesTaxGroupParty = S.Description WHERE Vt.Category = '" & ClsMain.Voucher_Category.Purchase & "'  AND H.V_Date BETWEEN '" & ReportFrm.FGetText(0) & "' And '" & ReportFrm.FGetText(1) & "' And IsNull(H.Qty,0) > 0 "

        Dim TotalTurnOverOfPurchase$ = "SELECT Str(IsNull(Sum(H.Sales_Tax_Taxable_Amt),0),15,2) FROM View_Vat H LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type LEFT JOIN SubGroup Sg ON H.SubCode = Sg.SubCode LEFT JOIN PostingGroupSalesTaxParty S On H.SalesTaxGroupParty = S.Description WHERE Vt.Category = '" & ClsMain.Voucher_Category.Purchase & "'  AND H.V_Date BETWEEN '" & ReportFrm.FGetText(0) & "' And '" & ReportFrm.FGetText(1) & "' And IsNull(H.Qty,0) > 0 And IsNull(H.Sales_Tax_Per,0) <> 0 "



        Dim SaleInOwnAcAgainstTaxInvoice$ = " SELECT Str(IsNull(Sum(H.Sales_Tax_Taxable_Amt),0),15,2) FROM View_Vat H LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type LEFT JOIN SubGroup Sg ON H.SubCode = Sg.SubCode LEFT JOIN PostingGroupSalesTaxParty S On H.SalesTaxGroupParty = S.Description WHERE Vt.Category = '" & ClsMain.Voucher_Category.Sale & "' AND H.V_Date BETWEEN '" & ReportFrm.FGetText(0) & "' And '" & ReportFrm.FGetText(1) & "' And IsNull(H.Qty,0) > 0 And IsNull(H.Sales_Tax,0) > 0 And IsNull(S.Nature,'" & ClsMain.SalesTaxGroupPartyNature.Local & "') = '" & ClsMain.SalesTaxGroupPartyNature.Local & "' And H.TINNo IS NOT NULL "
        Dim SaleInOwnAcFromNonRegisteredDealer$ = " SELECT Str(IsNull(Sum(H.Sales_Tax_Taxable_Amt),0),15,2) FROM View_Vat H LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type LEFT JOIN SubGroup Sg ON H.SubCode = Sg.SubCode LEFT JOIN PostingGroupSalesTaxParty S On H.SalesTaxGroupParty = S.Description WHERE Vt.Category = '" & ClsMain.Voucher_Category.Sale & "' AND H.V_Date BETWEEN '" & ReportFrm.FGetText(0) & "' And '" & ReportFrm.FGetText(1) & "' And IsNull(H.Qty,0) > 0 And IsNull(H.Sales_Tax,0) > 0 And IsNull(S.Nature,'" & ClsMain.SalesTaxGroupPartyNature.Local & "') = '" & ClsMain.SalesTaxGroupPartyNature.Local & "'   And H.TINNo IS NULL"
        Dim SaleOfExcemptGoods$ = " SELECT Str(IsNull(Sum(H.Sales_Tax_Taxable_Amt),0),15,2) FROM View_Vat H LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type LEFT JOIN SubGroup Sg ON H.SubCode = Sg.SubCode LEFT JOIN PostingGroupSalesTaxParty S On H.SalesTaxGroupParty = S.Description WHERE Vt.Category = '" & ClsMain.Voucher_Category.Sale & "' AND H.V_Date BETWEEN '" & ReportFrm.FGetText(0) & "' And '" & ReportFrm.FGetText(1) & "' And IsNull(H.Qty,0) > 0 And IsNull(H.Sales_Tax,0) = 0 And IsNull(S.Nature,'" & ClsMain.SalesTaxGroupPartyNature.Local & "')  = '" & ClsMain.SalesTaxGroupPartyNature.Local & "'  "
        Dim SaleOfExUp$ = " SELECT Str(IsNull(Sum(H.Sales_Tax_Taxable_Amt),0),15,2) FROM View_Vat H LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type LEFT JOIN SubGroup Sg ON H.SubCode = Sg.SubCode LEFT JOIN PostingGroupSalesTaxParty S On H.SalesTaxGroupParty = S.Description WHERE Vt.Category = '" & ClsMain.Voucher_Category.Sale & "' AND H.V_Date BETWEEN '" & ReportFrm.FGetText(0) & "' And '" & ReportFrm.FGetText(1) & "' And IsNull(H.Qty,0) > 0  And IsNull(S.Nature,'" & ClsMain.SalesTaxGroupPartyNature.Local & "')  = '" & ClsMain.SalesTaxGroupPartyNature.Central & "' "

        Dim TotalSale$ = " SELECT Str(IsNull(Sum(H.Sales_Tax_Taxable_Amt),0),15,2) FROM View_Vat H LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type LEFT JOIN SubGroup Sg ON H.SubCode = Sg.SubCode LEFT JOIN PostingGroupSalesTaxParty S On H.SalesTaxGroupParty = S.Description WHERE Vt.Category = '" & ClsMain.Voucher_Category.Sale & "'  AND H.V_Date BETWEEN '" & ReportFrm.FGetText(0) & "' And '" & ReportFrm.FGetText(1) & "' And IsNull(H.Qty,0) > 0 "
        Dim TotalSaleReturn$ = " SELECT Str(IsNull(Sum(H.Sales_Tax_Taxable_Amt),0),15,2) FROM View_Vat H LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type LEFT JOIN SubGroup Sg ON H.SubCode = Sg.SubCode LEFT JOIN PostingGroupSalesTaxParty S On H.SalesTaxGroupParty = S.Description WHERE Vt.Category = '" & ClsMain.Voucher_Category.Sale & "'  AND H.V_Date BETWEEN '" & ReportFrm.FGetText(0) & "' And '" & ReportFrm.FGetText(1) & "' And IsNull(H.Qty,0) < 0  "

        Dim NetAmtOfSale$ = " SELECT Str(IsNull(Sum(H.Sales_Tax_Taxable_Amt),0),15,2) FROM View_Vat H LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type LEFT JOIN SubGroup Sg ON H.SubCode = Sg.SubCode LEFT JOIN PostingGroupSalesTaxParty S On H.SalesTaxGroupParty = S.Description WHERE Vt.Category = '" & ClsMain.Voucher_Category.Sale & "'  AND H.V_Date BETWEEN '" & ReportFrm.FGetText(0) & "' And '" & ReportFrm.FGetText(1) & "' "
        Dim GrandTotalOfSale$ = " SELECT Str(IsNull(Sum(H.Sales_Tax_Taxable_Amt),0),15,2) FROM View_Vat H LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type LEFT JOIN SubGroup Sg ON H.SubCode = Sg.SubCode LEFT JOIN PostingGroupSalesTaxParty S On H.SalesTaxGroupParty = S.Description WHERE Vt.Category = '" & ClsMain.Voucher_Category.Sale & "'  AND H.V_Date BETWEEN '" & ReportFrm.FGetText(0) & "' And '" & ReportFrm.FGetText(1) & "' "

        Dim TurnOverOfSaleOnePer$ = "SELECT Str(IsNull(Sum(H.Sales_Tax_Taxable_Amt),0),15,2) FROM View_Vat H LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type LEFT JOIN SubGroup Sg ON H.SubCode = Sg.SubCode LEFT JOIN PostingGroupSalesTaxParty S On H.SalesTaxGroupParty = S.Description WHERE Vt.Category = '" & ClsMain.Voucher_Category.Sale & "'  AND H.V_Date BETWEEN '" & ReportFrm.FGetText(0) & "' And '" & ReportFrm.FGetText(1) & "' And IsNull(H.Qty,0) > 0 And IsNull(H.Sales_Tax_Per,0) = 1 "
        Dim TurnOverOfSaleFivePer$ = "SELECT Str(IsNull(Sum(H.Sales_Tax_Taxable_Amt),0),15,2) FROM View_Vat H LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type LEFT JOIN SubGroup Sg ON H.SubCode = Sg.SubCode LEFT JOIN PostingGroupSalesTaxParty S On H.SalesTaxGroupParty = S.Description WHERE Vt.Category = '" & ClsMain.Voucher_Category.Sale & "'  AND H.V_Date BETWEEN '" & ReportFrm.FGetText(0) & "' And '" & ReportFrm.FGetText(1) & "' And IsNull(H.Qty,0) > 0 And IsNull(H.Sales_Tax_Per,0) = 5 "
        Dim TurnOverOfSaleTwelvePer$ = "SELECT Str(IsNull(Sum(H.Sales_Tax_Taxable_Amt),0),15,2) FROM View_Vat H LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type LEFT JOIN SubGroup Sg ON H.SubCode = Sg.SubCode LEFT JOIN PostingGroupSalesTaxParty S On H.SalesTaxGroupParty = S.Description WHERE Vt.Category = '" & ClsMain.Voucher_Category.Sale & "'  AND H.V_Date BETWEEN '" & ReportFrm.FGetText(0) & "' And '" & ReportFrm.FGetText(1) & "' And IsNull(H.Qty,0) > 0 And IsNull(H.Sales_Tax_Per,0) = 12.5 "

        Dim TotalSalesTaxVatOnePerOnSale$ = "SELECT Str(IsNull(Sum(H.Sales_Tax),0),15,2) FROM View_Vat H LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type LEFT JOIN SubGroup Sg ON H.SubCode = Sg.SubCode LEFT JOIN PostingGroupSalesTaxParty S On H.SalesTaxGroupParty = S.Description WHERE Vt.Category = '" & ClsMain.Voucher_Category.Sale & "'  AND H.V_Date BETWEEN '" & ReportFrm.FGetText(0) & "' And '" & ReportFrm.FGetText(1) & "' And IsNull(H.Qty,0) > 0 And IsNull(H.Sales_Tax_Per,0) = 1 "
        Dim TotalSalesTaxVatFivePerOnSale$ = "SELECT Str(IsNull(Sum(H.Sales_Tax),0),15,2) FROM View_Vat H LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type LEFT JOIN SubGroup Sg ON H.SubCode = Sg.SubCode LEFT JOIN PostingGroupSalesTaxParty S On H.SalesTaxGroupParty = S.Description WHERE Vt.Category = '" & ClsMain.Voucher_Category.Sale & "'  AND H.V_Date BETWEEN '" & ReportFrm.FGetText(0) & "' And '" & ReportFrm.FGetText(1) & "' And IsNull(H.Qty,0) > 0 And IsNull(H.Sales_Tax_Per,0) = 5 "
        Dim TotalSalesTaxVatTwelvePerOnSale$ = "SELECT Str(IsNull(Sum(H.Sales_Tax),0),15,2) FROM View_Vat H LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type LEFT JOIN SubGroup Sg ON H.SubCode = Sg.SubCode LEFT JOIN PostingGroupSalesTaxParty S On H.SalesTaxGroupParty = S.Description WHERE Vt.Category = '" & ClsMain.Voucher_Category.Sale & "'  AND H.V_Date BETWEEN '" & ReportFrm.FGetText(0) & "' And '" & ReportFrm.FGetText(1) & "' And IsNull(H.Qty,0) > 0 And IsNull(H.Sales_Tax_Per,0) = 12.5 "

        Dim TotalAddSalesTaxPointFivePerOnSale$ = "SELECT Str(IsNull(Sum(H.Additional_Sales_Tax),0),15,2) FROM View_Vat H LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type LEFT JOIN SubGroup Sg ON H.SubCode = Sg.SubCode LEFT JOIN PostingGroupSalesTaxParty S On H.SalesTaxGroupParty = S.Description WHERE Vt.Category = '" & ClsMain.Voucher_Category.Sale & "'  AND H.V_Date BETWEEN '" & ReportFrm.FGetText(0) & "' And '" & ReportFrm.FGetText(1) & "' And IsNull(H.Qty,0) > 0 And IsNull(H.Additional_Sales_Tax_Per,0) = 0.5 "
        Dim TotalAddSalesTaxOnePerOnSale$ = "SELECT Str(IsNull(Sum(H.Additional_Sales_Tax),0),15,2) FROM View_Vat H LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type LEFT JOIN SubGroup Sg ON H.SubCode = Sg.SubCode LEFT JOIN PostingGroupSalesTaxParty S On H.SalesTaxGroupParty = S.Description WHERE Vt.Category = '" & ClsMain.Voucher_Category.Sale & "'  AND H.V_Date BETWEEN '" & ReportFrm.FGetText(0) & "' And '" & ReportFrm.FGetText(1) & "' And IsNull(H.Qty,0) > 0 And IsNull(H.Additional_Sales_Tax_Per,0) = 1 "

        Dim TotalSalesAndAddSalesTaxOnSale$ = "SELECT Str(IsNull(Sum(H.Sales_Tax),0) + IsNull(Sum(H.Additional_Sales_Tax),0),15,2) FROM View_Vat H LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type LEFT JOIN SubGroup Sg ON H.SubCode = Sg.SubCode LEFT JOIN PostingGroupSalesTaxParty S On H.SalesTaxGroupParty = S.Description WHERE Vt.Category = '" & ClsMain.Voucher_Category.Sale & "'  AND H.V_Date BETWEEN '" & ReportFrm.FGetText(0) & "' And '" & ReportFrm.FGetText(1) & "' And IsNull(H.Qty,0) > 0 "

        Dim TotalTurnOverOfSale$ = "SELECT Str(IsNull(Sum(H.Sales_Tax_Taxable_Amt),0),15,2) FROM View_Vat H LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type LEFT JOIN SubGroup Sg ON H.SubCode = Sg.SubCode LEFT JOIN PostingGroupSalesTaxParty S On H.SalesTaxGroupParty = S.Description WHERE Vt.Category = '" & ClsMain.Voucher_Category.Sale & "'  AND H.V_Date BETWEEN '" & ReportFrm.FGetText(0) & "' And '" & ReportFrm.FGetText(1) & "' And IsNull(H.Qty,0) > 0 And IsNull(H.Sales_Tax_Per,0) <> 0 "

        Dim TotalTax$ = " SELECT Str(IsNull(Sum(H.Sales_Tax),0) + IsNull(Sum(H.Additional_Sales_Tax),0),15,2) FROM View_Vat H LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type LEFT JOIN SubGroup Sg ON H.SubCode = Sg.SubCode LEFT JOIN PostingGroupSalesTaxParty S On H.SalesTaxGroupParty = S.Description WHERE Vt.Category In ('" & ClsMain.Voucher_Category.Sale & "','" & ClsMain.Voucher_Category.Purchase & "')  AND H.V_Date BETWEEN '" & ReportFrm.FGetText(0) & "' And '" & ReportFrm.FGetText(1) & "' And IsNull(H.Qty,0) > 0 "

        Dim NetTax$ = " Select " & _
                " Str((SELECT IsNull(Sum(H.Sales_Tax),0) + IsNull(Sum(H.Additional_Sales_Tax),0) FROM View_Vat H LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type LEFT JOIN SubGroup Sg ON H.SubCode = Sg.SubCode LEFT JOIN PostingGroupSalesTaxParty S On H.SalesTaxGroupParty = S.Description WHERE Vt.Category = '" & ClsMain.Voucher_Category.Sale & "'  AND H.V_Date BETWEEN '" & ReportFrm.FGetText(0) & "' And '" & ReportFrm.FGetText(1) & "' And IsNull(H.Qty,0) > 0) " & _
                " - (SELECT IsNull(Sum(H.Sales_Tax),0) + IsNull(Sum(H.Additional_Sales_Tax),0) FROM View_Vat H LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type LEFT JOIN SubGroup Sg ON H.SubCode = Sg.SubCode LEFT JOIN PostingGroupSalesTaxParty S On H.SalesTaxGroupParty = S.Description WHERE Vt.Category = '" & ClsMain.Voucher_Category.Purchase & "'  AND H.V_Date BETWEEN '" & ReportFrm.FGetText(0) & "' And '" & ReportFrm.FGetText(1) & "' And IsNull(H.Qty,0) > 0),15,2)   "


        mQry = " SELECT H.Sales_Tax_Per, Sum(H.Sales_Tax_Taxable_Amt) AS Sales_Tax_Taxable_Amt, " & _
                " Sum(H.Sales_Tax) AS Sales_Tax " & _
                " FROM View_Vat H  " & _
                " LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type " & _
                " WHERE Vt.Category = '" & ClsMain.Voucher_Category.Purchase & "' " & _
                " AND IsNull(H.Sales_Tax_Per,0) <> 0 " & _
                " GROUP BY H.Sales_Tax_Per "
        DtSalesTaxForPurchase = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = " SELECT H.Additional_Sales_Tax_Per, Sum(H.Additional_Sales_Tax) AS Additional_Sales_Tax " & _
                " FROM View_Vat H  " & _
                " LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type " & _
                " WHERE Vt.Category = '" & ClsMain.Voucher_Category.Purchase & "' " & _
                " AND IsNull(H.Additional_Sales_Tax,0) <> 0 " & _
                " GROUP BY H.Additional_Sales_Tax_Per "
        DtAddSalesTaxForPurchase = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = " SELECT H.Sales_Tax_Per, Sum(H.Sales_Tax_Taxable_Amt) AS Sales_Tax_Taxable_Amt, " & _
                " Sum(H.Sales_Tax) AS Sales_Tax " & _
                " FROM View_Vat H  " & _
                " LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type " & _
                " WHERE Vt.Category = '" & ClsMain.Voucher_Category.Sale & "' " & _
                " AND IsNull(H.Sales_Tax_Per,0) <> 0 " & _
                " GROUP BY H.Sales_Tax_Per "
        DtSalesTaxForSale = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = " SELECT H.Additional_Sales_Tax_Per, Sum(H.Additional_Sales_Tax) AS Additional_Sales_Tax " & _
                " FROM View_Vat H  " & _
                " LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type " & _
                " WHERE Vt.Category = '" & ClsMain.Voucher_Category.Sale & "' " & _
                " AND IsNull(H.Additional_Sales_Tax,0) <> 0 " & _
                " GROUP BY H.Additional_Sales_Tax_Per "
        DtAddSalesTaxForSale = AgL.FillData(mQry, AgL.GCn).Tables(0)

        Try
            mQry = " SELECT '                                  UPVAT - XXIV'"
            mQry += "UNION ALL" & " SELECT '           Department of Commercial Taxes,Government of Utter Pradesh'"
            mQry += "UNION ALL" & " SELECT '                    [See Rule-45(2) of the UPVAT Rules,2008]'"
            mQry += "UNION ALL" & " SELECT '                    Return of Tax Period - monthly/quarterly'"
            mQry += "UNION ALL" & " SELECT '1. Assessment Year                    : " & AgL.PubStartDate & " - " & AgL.PubEndDate & "'"
            mQry += "UNION ALL" & " SELECT '2. Tax Period                         : " & ReportFrm.FGetText(0) & " - " & ReportFrm.FGetText(1) & "'"
            mQry += "UNION ALL" & " SELECT '3. Designation of Assessing Authority :'"
            mQry += "UNION ALL" & " SELECT '4. Name of Circle / Sector            :'"
            mQry += "UNION ALL" & " SELECT '5. Name & Address of Dealer           : " & AgL.PubCompName & "'"
            mQry += "UNION ALL" & " SELECT '                                      : " & AgL.PubCompAdd1 & "'"
            mQry += "UNION ALL" & " SELECT '                                      : " & AgL.PubCompAdd2 & "'"
            mQry += "UNION ALL" & " SELECT '6. Taxpayer`s Indentification Number  : " & AgL.PubCompTIN & "'"
            mQry += "UNION ALL" & " SELECT '7.   Details of Purchase [in Rs.]'"
            mQry += "UNION ALL" & " SELECT '______________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________'"
            mQry += "UNION ALL" & " SELECT '  a. Vat Goods'"
            mQry += "UNION ALL" & " SELECT '     i.  Purchase in own a/c against tax invoice   :' + (" & PurchaseInOwnAcAgainstTaxInvoice & ")"
            mQry += "UNION ALL" & " SELECT '         (Annexure-A Part I)                       :'"
            mQry += "UNION ALL" & " SELECT '     ii. Purchase in own A/c from person other than:' + (" & PurchaseInOwnAcFromNonRegisteredDealer & ")"
            mQry += "UNION ALL" & " SELECT '         registered dealer                         :'"
            mQry += "UNION ALL" & " SELECT '     iii.Purchase of exempted goods                :' + (" & PurchaseOfExcemptGoods & ")"
            mQry += "UNION ALL" & " SELECT '     iv. Purchase from Ex.U.P.                     :' + (" & PurchaseOfExUp & ")"
            mQry += "UNION ALL" & " SELECT '     v.  Purchase in Principal A/c                 :'"
            mQry += "UNION ALL" & " SELECT '         (a) U.P.Principal                         :           0.00'"
            mQry += "UNION ALL" & " SELECT '         (a-i)  Principle against tax invoice      :'"
            mQry += "UNION ALL" & " SELECT '                (Annexure-A Part II)               :'"
            mQry += "UNION ALL" & " SELECT '         (a-ii) Other Purchase                     :'"
            mQry += "UNION ALL" & " SELECT '         (b) Ex.U.P.Principal                      :           0.00'"
            mQry += "UNION ALL" & " SELECT '     vi. Any other Purchase                        :           0.00'"
            mQry += "UNION ALL" & " SELECT '                                           Total   :' + (" & TotalPurchase & ")"
            mQry += "UNION ALL" & " SELECT '     vii.Less - Purchase Return (annexure-A1)      :' + (" & TotalPurchaseReturn & ")"
            mQry += "UNION ALL" & " SELECT '     viii. Net amount of purchase                  :' + (" & NetAmtOfPurchase & ")"
            mQry += "UNION ALL" & " SELECT '______________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________'"
            mQry += "UNION ALL" & " SELECT '  b. Non Vat Goods'"
            mQry += "UNION ALL" & " SELECT '     i.  Purchase from registered dealers          :'"
            mQry += "UNION ALL" & " SELECT '     ii. Purchase from unregistered                :'"
            mQry += "UNION ALL" & " SELECT '     iii.Purchase of exempted goods                :'"
            mQry += "UNION ALL" & " SELECT '     iv. Purchase from Ex.U.P.                     :'"
            mQry += "UNION ALL" & " SELECT '     v.  Purchase in Principal A/c                 :'"
            mQry += "UNION ALL" & " SELECT '         (a) U.P.Principal                         :'"
            mQry += "UNION ALL" & " SELECT '         (a-i)  Principle against tax invoice      :'"
            mQry += "UNION ALL" & " SELECT '                (Annexure-A Part II)               :'"
            mQry += "UNION ALL" & " SELECT '         (a-ii) Other Purchase                     :'"
            mQry += "UNION ALL" & " SELECT '         (b) Ex.U.P.Principal                      :'"
            mQry += "UNION ALL" & " SELECT '     vi. Any other Purchase                        :'"
            mQry += "UNION ALL" & " SELECT '                                           Total   :'"
            mQry += "UNION ALL" & " SELECT '     vii.Less - Purchase Return (annexure-A1)      :'"
            mQry += "UNION ALL" & " SELECT '     viii. Net amount of purchase                  :'"
            mQry += "UNION ALL" & " SELECT '______________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________'"
            mQry += "UNION ALL" & " SELECT '                                     Grand Total   :' + (" & GrandTotalOfPurchase & ")"
            mQry += "UNION ALL" & " SELECT '______________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________'"
            mQry += "UNION ALL" & " SELECT '  c. Capital Goods Purchased from with the state'"
            mQry += "UNION ALL" & " SELECT '     i.  Purchase against tax invoice Annexure A-2 :           0.00'"
            mQry += "UNION ALL" & " SELECT '     ii. Purchase from unregistered                :'"
            mQry += "UNION ALL" & " SELECT '______________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________'"
            mQry += "UNION ALL" & " SELECT '                                           Total   :           0.00'"
            mQry += "UNION ALL" & " SELECT '______________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________'"
            mQry += "UNION ALL" & " SELECT '  d. Purchase through commission agent for which'"
            mQry += "UNION ALL" & " SELECT '     certificate in form VI has been received'"
            mQry += "UNION ALL" & " SELECT 'S.No.  Certificate No.       Date       Value of goods   Amout of Tax'"
            mQry += "UNION ALL" & " SELECT '1.'"
            mQry += "UNION ALL" & " SELECT '2.'"
            mQry += "UNION ALL" & " SELECT '3.'"
            mQry += "UNION ALL" & " SELECT '                             Total   :'"
            mQry += "UNION ALL" & " SELECT '______________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________'"
            mQry += "UNION ALL" & " SELECT '8.   Computation of tax on purchase'"
            mQry += "UNION ALL" & " SELECT '______________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________'"
            mQry += "UNION ALL" & " SELECT '     Sl.No.   Rate of Tax      Commodity      Turnover of Purchase      Tax'"
            mQry += "UNION ALL" & " SELECT ''"
            mQry += "UNION ALL" & " SELECT '______________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________'"
            mQry += "UNION ALL" & " SELECT '              Vat Goods'"


            For I = 0 To DtSalesTaxForPurchase.Rows.Count - 1
                mQry += "UNION ALL" & " SELECT '     " & (I + 1).ToString & "         ' + Str(" & AgL.XNull(DtSalesTaxForPurchase.Rows(I)("Sales_Tax_Per")) & ",5,2)  + ' %                       :' + Str(" & AgL.XNull(DtSalesTaxForPurchase.Rows(I)("Sales_Tax_Taxable_Amt")) & ",15,2)  + ' :' +           Str(" & AgL.XNull(DtSalesTaxForPurchase.Rows(I)("Sales_Tax")).ToString & ",15,2)"
            Next

            mQry += "UNION ALL" & " SELECT '     Additional Tax'"

            For I = 0 To DtAddSalesTaxForPurchase.Rows.Count - 1
                mQry += "UNION ALL" & " SELECT '     " & (I + 1).ToString & "         ' + Str(" & AgL.XNull(DtAddSalesTaxForPurchase.Rows(I)("Additional_Sales_Tax_Per")) & ",5,2)  + ' %                       :' + Space(15)  + ' :' +           Str(" & AgL.XNull(DtAddSalesTaxForPurchase.Rows(I)("Additional_Sales_Tax")).ToString & ",15,2)"
            Next

            mQry += "UNION ALL" & " SELECT '______________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________'"
            mQry += "UNION ALL" & " SELECT '                                Total        :' + (" & TotalTurnOverOfPurchase & ") + ' :' +           (" & TotalSalesAndAddSalesTaxOnPurchase & ")"
            mQry += "UNION ALL" & " SELECT '______________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________'"
            mQry += "UNION ALL" & " SELECT '              Non Vat Goods'"
            mQry += "UNION ALL" & " SELECT '     vi.          20 %                       :           0.00 :           0.00'"
            mQry += "UNION ALL" & " SELECT '     vii.         32.5 %                     :           0.00 :           0.00'"
            mQry += "UNION ALL" & " SELECT '     viii.        ____ %                     :           0.00 :           0.00'"
            mQry += "UNION ALL" & " SELECT '______________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________'"
            mQry += "UNION ALL" & " SELECT '                                Total        :           0.00 :           0.00'"
            mQry += "UNION ALL" & " SELECT '______________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________'"
            mQry += "UNION ALL" & " SELECT '                          Grand Total        :' + (" & GrandTotalOfPurchase & ") + ' :' +        (" & TotalSalesAndAddSalesTaxOnPurchase & ")"
            mQry += "UNION ALL" & " SELECT '______________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________'"
            mQry += "UNION ALL" & " SELECT '9.   Details of Sale'"
            mQry += "UNION ALL" & " SELECT '  a. Vat Goods'"
            mQry += "UNION ALL" & " SELECT '______________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________'"
            mQry += "UNION ALL" & " SELECT '     i.   Turnover of sale in own a/c against tax  :' + (" & SaleInOwnAcAgainstTaxInvoice & ")"
            mQry += "UNION ALL" & " SELECT '          invoice (annexure-B Part-1)              :'"
            mQry += "UNION ALL" & " SELECT '     ii.  Turnover of sale in own a/c other than in:' + (" & SaleInOwnAcFromNonRegisteredDealer & ")"
            mQry += "UNION ALL" & " SELECT '          column-i                                 :'"
            mQry += "UNION ALL" & " SELECT '     iii. Turnover of sale of exempted goods       :' + (" & SaleOfExcemptGoods & ")"
            mQry += "UNION ALL" & " SELECT '     iv.  Sale in Principal`s A/c                  :'"
            mQry += "UNION ALL" & " SELECT '          (a)  U.P.Principal                       :           0.00'"
            mQry += "UNION ALL" & " SELECT '          (a-i) Sales ag.tax invoice Annx-B Part-II:'"
            mQry += "UNION ALL" & " SELECT '          (a-ii)Other Sales                        :'"
            mQry += "UNION ALL" & " SELECT '          (b)  Ex.U.P.Pricipal                     :' + (" & SaleOfExUp & ")"
            mQry += "UNION ALL" & " SELECT '     v.   Interstate sale against form `C`         :           0.00'"
            mQry += "UNION ALL" & " SELECT '     vi.  Interstate sale without form `C`         :           0.00'"
            mQry += "UNION ALL" & " SELECT '     vii. Sale in course of export out of India    :           0.00'"
            mQry += "UNION ALL" & " SELECT '     viii Sale in course of import                 :           0.00'"
            mQry += "UNION ALL" & " SELECT '     ix.  Sale outside state                       :           0.00'"
            mQry += "UNION ALL" & " SELECT '     x.   Consignment Sale                         :           0.00'"
            mQry += "UNION ALL" & " SELECT '     xi.  Any other sale                           :           0.00'"
            mQry += "UNION ALL" & " SELECT '                                           Total   :' + (" & TotalSale & ")"
            mQry += "UNION ALL" & " SELECT '     xii. Less - sales return (annexure B-1)       :' + (" & TotalSaleReturn & ")"
            mQry += "UNION ALL" & " SELECT '     xiii.Net amount of sales                      :' + (" & NetAmtOfSale & ")"
            mQry += "UNION ALL" & " SELECT '______________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________'"
            mQry += "UNION ALL" & " SELECT '  b. Non Vat Goods'"
            mQry += "UNION ALL" & " SELECT '______________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________'"
            mQry += "UNION ALL" & " SELECT '     i.   Taxable turnover of sale                 :'"
            mQry += "UNION ALL" & " SELECT '     ii.  Exempted turnover of sale                :'"
            mQry += "UNION ALL" & " SELECT '     iii. Tax paid turnover of goods               :'"
            mQry += "UNION ALL" & " SELECT '     iv.  Sale in Principal`s A/c                  :'"
            mQry += "UNION ALL" & " SELECT '          (a)  U.P.Principal                       :'"
            mQry += "UNION ALL" & " SELECT '          (a-i) Sales ag.tax invoice Annx-B Part-II:'"
            mQry += "UNION ALL" & " SELECT '          (a-ii)Other Sales                        :'"
            mQry += "UNION ALL" & " SELECT '          (b)  Ex.U.P.Pricipal                     :'"
            mQry += "UNION ALL" & " SELECT '     v.   Any other Sale                           :'"
            mQry += "UNION ALL" & " SELECT '                                           Total   :'"
            mQry += "UNION ALL" & " SELECT '     vi.  Less - sales return (annexure B-1)       :'"
            mQry += "UNION ALL" & " SELECT '     vii. Net amount of sales                      :'"
            mQry += "UNION ALL" & " SELECT '______________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________'"
            mQry += "UNION ALL" & " SELECT '                                     Grand Total   :' + (" & GrandTotalOfSale & ")"
            mQry += "UNION ALL" & " SELECT '______________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________'"
            mQry += "UNION ALL" & " SELECT '  c. Sales through commission agent for which in form V has'"
            mQry += "UNION ALL" & " SELECT '     been received'"
            mQry += "UNION ALL" & " SELECT 'S.No.  Certificate No.       Date       Value of goods   Amout of Tax'"
            mQry += "UNION ALL" & " SELECT '1.'"
            mQry += "UNION ALL" & " SELECT '2.'"
            mQry += "UNION ALL" & " SELECT '3.'"
            mQry += "UNION ALL" & " SELECT '                             Total   :'"
            mQry += "UNION ALL" & " SELECT '______________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________'"
            mQry += "UNION ALL" & " SELECT '10.  Computation of tax on sale'"
            mQry += "UNION ALL" & " SELECT '______________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________'"
            mQry += "UNION ALL" & " SELECT '     Sl.No.   Rate of Tax      Commodity             Sale Amount        Tax'"
            mQry += "UNION ALL" & " SELECT ''"
            mQry += "UNION ALL" & " SELECT '______________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________'"
            mQry += "UNION ALL" & " SELECT '              Vat Goods'"


            For I = 0 To DtSalesTaxForSale.Rows.Count - 1
                mQry += "UNION ALL" & " SELECT '     " & (I + 1).ToString & "         ' + Str(" & AgL.XNull(DtSalesTaxForSale.Rows(I)("Sales_Tax_Per")) & ",5,2)  + ' %                       :' + Str(" & AgL.XNull(DtSalesTaxForSale.Rows(I)("Sales_Tax_Taxable_Amt")) & ",15,2)  + ' :' +           Str(" & AgL.XNull(DtSalesTaxForSale.Rows(I)("Sales_Tax")).ToString & ",15,2)"
            Next

            mQry += "UNION ALL" & " SELECT '     Additional Tax'"

            For I = 0 To DtAddSalesTaxForSale.Rows.Count - 1
                mQry += "UNION ALL" & " SELECT '     " & (I + 1).ToString & "         ' + Str(" & AgL.XNull(DtAddSalesTaxForSale.Rows(I)("Additional_Sales_Tax_Per")) & ",5,2)  + ' %                       :' + Space(15)  + ' :' +           Str(" & AgL.XNull(DtAddSalesTaxForSale.Rows(I)("Additional_Sales_Tax")).ToString & ",15,2)"
            Next

            mQry += "UNION ALL" & " SELECT '______________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________'"
            mQry += "UNION ALL" & " SELECT '                                Total        :' + (" & TotalTurnOverOfSale & ") + ' :' +           (" & TotalSalesAndAddSalesTaxOnSale & ")"
            mQry += "UNION ALL" & " SELECT '______________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________'"
            mQry += "UNION ALL" & " SELECT '              Non Vat Goods'"
            mQry += "UNION ALL" & " SELECT '     i.           20 %                       :           0.00 :           0.00'"
            mQry += "UNION ALL" & " SELECT '     ii.          32.5 %                     :           0.00 :           0.00'"
            mQry += "UNION ALL" & " SELECT '     iii.         ____ %                     :           0.00 :           0.00'"
            mQry += "UNION ALL" & " SELECT '______________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________'"
            mQry += "UNION ALL" & " SELECT '                                Total        :           0.00 :           0.00'"
            mQry += "UNION ALL" & " SELECT '______________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________'"
            mQry += "UNION ALL" & " SELECT '                          Grand Total        :' + (" & GrandTotalOfSale & ") + ' :' +        (" & TotalSalesAndAddSalesTaxOnSale & ")"
            mQry += "UNION ALL" & " SELECT '______________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________'"
            mQry += "UNION ALL" & " SELECT '11. Installment of compounding scheme,if any :           0.00'"
            mQry += "UNION ALL" & " SELECT '12. Amount of T.D.S.                         :           0.00'"
            mQry += "UNION ALL" & " SELECT '13. Tax Payable'"
            mQry += "UNION ALL" & " SELECT '______________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________'"
            mQry += "UNION ALL" & " SELECT '    i.  Tax on Purchase                      :' + (" & TotalSalesAndAddSalesTaxOnPurchase & ")"
            mQry += "UNION ALL" & " SELECT '    ii. Tax on Sale                          :' + (" & TotalSalesAndAddSalesTaxOnSale & ")"
            mQry += "UNION ALL" & " SELECT '    iii.Installment of compounding scheme    :           0.00'"
            mQry += "UNION ALL" & " SELECT '    iv. T.D.S.Amount                         :           0.00'"
            mQry += "UNION ALL" & " SELECT '______________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________'"
            mQry += "UNION ALL" & " SELECT '                                       Total :' + (" & TotalTax & ")"
            mQry += "UNION ALL" & " SELECT '______________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________'"
            mQry += "UNION ALL" & " SELECT '14. Detail of ITC'"
            mQry += "UNION ALL" & " SELECT '______________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________'"
            mQry += "UNION ALL" & " SELECT '    i.  ITC brought forward from previous    :           0.00'"
            mQry += "UNION ALL" & " SELECT '    ii. ITC earned during the tax period     :'"
            mQry += "UNION ALL" & " SELECT '     a. On purchase made in own account      :'+        (" & TotalSalesAndAddSalesTaxOnPurchase & ")"
            mQry += "UNION ALL" & " SELECT '     b. On purc.made through purchasing      :'"
            mQry += "UNION ALL" & " SELECT '        commission agent against in form VI  :'"
            mQry += "UNION ALL" & " SELECT '     c. Installment of ITC on opening stock  :'"
            mQry += "UNION ALL" & " SELECT '        due in the tax period                :'"
            mQry += "UNION ALL" & " SELECT '     d. Installment of ITC on capital goods  :'"
            mQry += "UNION ALL" & " SELECT '        due in the tax period                :'"
            mQry += "UNION ALL" & " SELECT '     e. ITC reversed during the tax period   :'"
            mQry += "UNION ALL" & " SELECT '     f. Admissible ITC in the tax period     :'"
            mQry += "UNION ALL" & " SELECT '    iii. a. Adjustment against tax payable   :'+        (" & TotalSalesAndAddSalesTaxOnPurchase & ")"
            mQry += "UNION ALL" & " SELECT '         b. ITC adjustment [14(VII)]         :'"
            mQry += "UNION ALL" & " SELECT '    iv. ITC carried forward                  :           0.00'"
            mQry += "UNION ALL" & " SELECT '______________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________'"
            mQry += "UNION ALL" & " SELECT '                                      Total  :            NIL'"
            mQry += "UNION ALL" & " SELECT '______________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________'"
            mQry += "UNION ALL" & " SELECT '15. Net Tax'"
            mQry += "UNION ALL" & " SELECT '______________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________'"
            mQry += "UNION ALL" & " SELECT '    i.  Total tax payable (serial no.13)     :'+        (" & TotalSalesAndAddSalesTaxOnSale & ")"
            mQry += "UNION ALL" & " SELECT '    ii. ITC adjustment [14(iii)]             :'+        (" & TotalSalesAndAddSalesTaxOnPurchase & ")"
            mQry += "UNION ALL" & " SELECT '    iii.Net Tax                              :'+        (" & NetTax & ")"
            mQry += "UNION ALL" & " SELECT '______________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________'"
            mQry += "UNION ALL" & " SELECT '16. Details of Tax Deposited'"
            mQry += "UNION ALL" & " SELECT '______________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________'"
            mQry += "UNION ALL" & " SELECT 'A. Tax deposit in Bank / Treasury'"
            mQry += "UNION ALL" & " SELECT 'Name of the Bank / Branch           T.C.Number       Date          Amount'"
            mQry += "UNION ALL" & " SELECT '______________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________'"
            mQry += "UNION ALL" & " SELECT ''"
            mQry += "UNION ALL" & " SELECT ''"
            mQry += "UNION ALL" & " SELECT ''"
            mQry += "UNION ALL" & " SELECT ''"
            mQry += "UNION ALL" & " SELECT '______________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________'"
            mQry += "UNION ALL" & " SELECT 'B. By Adjustment against adjustment vouchers'"
            mQry += "UNION ALL" & " SELECT '______________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________'"
            mQry += "UNION ALL" & " SELECT 'Adjustment Voucher No.                               Date          Amount'"
            mQry += "UNION ALL" & " SELECT '______________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________'"
            mQry += "UNION ALL" & " SELECT ''"
            mQry += "UNION ALL" & " SELECT ''"
            mQry += "UNION ALL" & " SELECT ''"
            mQry += "UNION ALL" & " SELECT '______________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________'"
            mQry += "UNION ALL" & " SELECT 'C. Total tax deposited (A+B)                           Total         0.00'"
            mQry += "UNION ALL" & " SELECT 'Rs. Zero Only'"
            mQry += "UNION ALL" & " SELECT '______________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________'"
            mQry += "UNION ALL" & " SELECT 'Annexure 1 - Annexure A / A-1 / A-2 / B / B-1 whichever is applicable'"
            mQry += "UNION ALL" & " SELECT '         2 - Treasury Challan number.............../ date ..........'"
            mQry += "UNION ALL" & " SELECT '                       DECLARATION'"
            mQry += "UNION ALL" & " SELECT 'I ....................... S/o, D/o,W/o ..................... Status...........'"
            mQry += "UNION ALL" & " SELECT '[i.e.proprietor, director,partner etc.as provided in rule-32(6)], do hereby'"
            mQry += "UNION ALL" & " SELECT 'declare and verify that, to the best of my knowledge and belief all the'"
            mQry += "UNION ALL" & " SELECT 'statements and figures given in this return are ture and complete and nothing'"
            mQry += "UNION ALL" & " SELECT 'has been willfully omitted or wrongly stated.'"
            mQry += "UNION ALL" & " SELECT ''"
            mQry += "UNION ALL" & " SELECT 'Date  -                                  Signature -'"
            mQry += "UNION ALL" & " SELECT 'Place -                                  Status    -'"





            DsRep = AgL.FillData(mQry, AgL.GCn)



            If DsTemp IsNot Nothing Then
                'OExcelHandler.ExportToExcel("C:\Documents and Settings\Akash\Desktop\VatReturn", DsRep, "Write In Excel", ErrorMessage)
                OExcelHandler.ExportToExcel(My.Application.Info.DirectoryPath + "\" + "TaxReturn.xlsx", DsRep, "Write In Excel", ErrorMessage)
                System.Diagnostics.Process.Start(My.Application.Info.DirectoryPath + "\" + "TaxReturn.xlsx")


            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


#Region "Bill Wise Profitabilty"
    Private Sub ProcBillWiseProfitabilty()
        Try
            RepName = "Trade_BillWiseProfitabilty" : RepTitle = "Bill Wise Profitabilty"


            Dim mCondStr$ = ""
            Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"

            mCondStr = " Where 1 = 1 "
            mCondStr = mCondStr & " AND H.V_Date Between '" & ReportFrm.FGetText(0) & "' And '" & ReportFrm.FGetText(1) & "' "
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.SaleToParty", 2)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Item", 3)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.DocID", 4)

            If ReportFrm.GetWhereCondition("ItemRrportingGroup", 5) <> "" Then
                mQry = " Select '''' +  replace(ItemList,',',''',''')  + ''''  From ItemReportingGroup Where 1=1 " & ReportFrm.GetWhereCondition("Code", 5)
                mCondStr = mCondStr & " And L.Item In (" & AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar & ")"
            End If

            mQry = " SELECT H.DocID , H.V_Type, H.V_Date, H.ReferenceNo, Sg.Name As SaleToPartyName, H.SaleToParty , " & _
                    " L.Sr, L.Item , L.Qty, L.Unit, L.Rate, L.Amount, L.Landed_Value AS SaleValue, " & _
                    " I.Description AS ItemDesc , PCD.Landed_Value/PCD.Qty * L.Qty AS PurchaseValue ,  " & _
                    " L.Landed_Value - PCD.Landed_Value/PCD.Qty * L.Qty AS Profit,  " & _
                    " CASE WHEN isnull(PCD.Landed_Value/PCD.Qty * L.Qty,0) > 0 THEN ( L.Landed_Value - PCD.Landed_Value/PCD.Qty * L.Qty) * 100 / ( PCD.Landed_Value/PCD.Qty * L.Qty) ELSE 0 END AS ProfitPer " & _
                    " FROM SaleInvoice  H " & _
                    " LEFT JOIN SubGroup Sg On H.SaleToParty = Sg.SubCode " & _
                    " LEFT JOIN SaleInvoiceDetail L ON L.DocId = H.DocID  " & _
                    " LEFT JOIN Item I ON I.Code = L.Item  " & _
                    " LEFT JOIN PurchChallanDetail PCD ON PCD.DocId = L.ReferenceDocId AND PCD.Sr  = L.ReferenceDocIdSr " & mCondStr
            DsRep = AgL.FillData(mQry, AgL.GCn)



            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records to Print!")

            ReportFrm.PrintReport(DsRep, RepName, RepTitle)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
#End Region


#Region "Debtors Outstanding Over Due Days"
    Private Sub ProcDebtorsOutstandingOverDue()
        Dim mCondStr$ = ""
        Dim NoofDays As Integer = 0

        Try
            RepName = "Trade_DebtorsOutstandingOverDue" : RepTitle = "Debtors Outstanding Over Due"

            If Val(ReportFrm.FGetText(1)) <> 0 Then
                NoofDays = Val(ReportFrm.FGetText(1))
            Else
                MsgBox("Please Enter Valid No. Of Days.") : Exit Sub
            End If

            mCondStr = " Where H.V_Date <= '" & ReportFrm.FGetText(0) & "' "
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.SubCode", 2)
            mCondStr = mCondStr & " And  H.Site_Code = " & AgL.Chk_Text(AgL.PubSiteCode) & " "
            mCondStr = mCondStr & " And  Sg.Nature In ('" & ClsMain.SubGroupNature.Customer & "') "


            mQry = "SELECT VRep.SubCode, Max(VRep.PartyName) AS PartyName, Max(VRep.TotalBal) AS TotalBal, Sum(VRep.NetBalAmount) AS BalAboveDays, '" & ReportFrm.FGetText(1) & "' AS OnOfDays " & _
                    " FROM " & _
                    " ( " & _
                    " SELECT VMain.*,    SUM(NetAmount) OVER( PARTITION BY SubCode ORDER BY V_Date DESC , DocId ) sum_stock1,  " & _
                    " CASE WHEN VMain.NetAmount = 0 THEN  VMain.TotalBal - SUM(NetAmount) OVER( PARTITION BY SubCode ORDER BY V_Date DESC , DocId )  ELSE VMain.NetAmount END AS NetBalAmount " & _
                    " FROM  ( " & _
                    " SELECT P.PartyName, P.TotalBal, SD.*,  CASE WHEN P.TotalBal > SD.Sum_Dr THEN SD.AmtDr ELSE 0 END AS NetAmount " & _
                    " FROM " & _
                    " ( " & _
                    " SELECT SG.SubCode AS SubCode, max(SG.Name) AS PartyName, isnull(sum(H.AmtDr),0)- isnull(sum(H.AmtCR),0) AS TotalBal " & _
                    " FROM Ledger H  WITH (Nolock) " & _
                    " LEFT JOIN SubGroup SG WITH (Nolock) ON SG.SubCode = H.SubCode " & mCondStr & _
                    " GROUP BY SG.SubCode  " & _
                    " Having isnull(sum(H.AmtDr),0)- isnull(sum(H.AmtCR),0)  > 0 " & _
                    " ) As P " & _
                    " LEFT JOIN " & _
                    " ( " & _
                    " SELECT s.*, SUM(AmtDr) OVER( PARTITION BY SubCode ORDER BY V_Date DESC, DocId  ) Sum_Dr " & _
                    " FROM " & _
                    " ( " & _
                    " SELECT H.DocID, H.RecId, H.V_TYpe, H.V_Date, H.AmtDr , H.SubCode " & _
                    " FROM Ledger H WITH (Nolock) " & _
                    " LEFT JOIN SubGroup SG WITH (Nolock) ON SG.SubCode = H.SubCode " & mCondStr & _
                    " AND isnull(H.AmtDr,0) <> 0 " & _
                    " )  s  " & _
                    " ) SD ON SD.SubCode = p.SubCode  AND IsNull(P.TotalBal,0)  > IsNull(SD.Sum_Dr,0)  - IsNull(SD.AmtDr,0) " & _
                    " ) VMain " & _
                    " ) VRep " & _
                    " WHERE DateDiff(Day,VRep.V_Date," & AgL.Chk_Text(ReportFrm.FGetText(0)) & " ) >=  " & NoofDays & " " & _
                    " GROUP BY VRep.SubCode "


            DsRep = AgL.FillData(mQry, AgL.GCn)

            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records to Print!")

            ReportFrm.PrintReport(DsRep, RepName, RepTitle)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
#End Region

#Region "Party Outstanding Report"
    Private Sub ProcPartyOutstandingReport()
        Dim mCondStr1$ = ""
        Dim mCondStr2$ = ""
        Try
            RepName = "BillWiseOutstandingReport" : RepTitle = "Party Outstanding Report"

            mCondStr1 = " Where LG.V_Date <= '" & ReportFrm.FGetText(0) & "' "
            mCondStr2 = " Where LG.V_Date <= '" & ReportFrm.FGetText(0) & "' "

            mCondStr1 = mCondStr1 & ReportFrm.GetWhereCondition("LG.SubCode", 1)
            mCondStr2 = mCondStr2 & ReportFrm.GetWhereCondition("LG.SubCode", 1)

            mCondStr1 = mCondStr1 & " And  LG.Site_Code IN (" & AgL.PubSiteCode & ") "
            mCondStr2 = mCondStr2 & " And  LG.Site_Code IN (" & AgL.PubSiteCode & ") "

            mCondStr1 = mCondStr1 & " And  Sg.Nature In ('" & ClsMain.SubGroupNature.Customer & "','" & ClsMain.SubGroupNature.Supplier & "') "
            mCondStr2 = mCondStr2 & " And  Sg.Nature In ('" & ClsMain.SubGroupNature.Customer & "','" & ClsMain.SubGroupNature.Supplier & "') "

            mCondStr1 = mCondStr1 & " And  IsNull(Lg.AmtDr,0) > 0 "

            mQry = "Select DocId, V_SNo, Max(RecId) As RecId, Max(V_Type) As V_Type, " & _
                            " Max(V_Date) As V_Date, Max(Narration) As Narration, " & _
                            " Max(Sg.Name) As PartyName, Max(C.CityName) As CityName, " & _
                            " Max(BillAmt) As BillAmt, Abs(Sum(Adjusted)) As Receipt, " & _
                            " Max(BillAmt) - Abs(Sum(Adjusted)) As Balance," & _
                            " Max(DateAdd(Day,IsNull(Sg.CreditDays,0),Tmp.V_Date)) As DueDate, " & _
                            " Max(Datediff(Day, Dateadd(Day,Sg.CreditDays,Tmp.V_Date),getdate())) AS OverDue  " & _
                            " From ( " & _
                            "       Select  LG.DocId,LG.V_SNo, LG.RecId As RecId, " & _
                            "       LG.V_Type, LG.V_Date, LG.Narration, Lg.SubCode, " & _
                            "       IsNull(Lg.AmtDr,0) As BillAmt,0 As Adjusted " & _
                            "       From Ledger LG " & _
                            "       LEFT JOIN SubGroup Sg On Lg.SubCode = Sg.SubCode " & mCondStr1 & _
                            "       And IsNull(Lg.AmtDr,0) > 0 " & _
                            " Union All " & _
                            "       Select	LA.Adj_DocId As DocId,LA.Adj_V_SNo As V_SNo,Null As RecId,Null As V_Type,Null As V_Date, " & _
                            "       Null As Narration, Lg.SubCode,0 As BillAmt,LA.Amount As Adjusted	 " & _
                            "       From LedgerAdj LA " & _
                            "       Left Join Ledger LG On LA.Adj_DocId = LG.DocId And LA.Adj_V_SNo = LG.V_SNo " & _
                            "       Left Join SubGroup Sg On Lg.SubCode = Sg.SubCode " & mCondStr2 & _
                            " ) As Tmp " & _
                            " LEFT JOIN SubGroup Sg On Tmp.SubCode = Sg.SubCode " & _
                            " LEFT JOIN City C On SG.CityCode = C.CityCode " & _
                            " Group By DocId, V_SNo " & _
                            " Having (IsNull(Max(BillAmt),0)-IsNull(Sum(Adjusted),0))>0" & _
                            " Order By Max(V_Date),Max(RecId) "


            'mQry = "Select LG.DocId,LG.V_SNo,Convert(Varchar,Max(LG.V_No)) as VNo,Max(LG.V_Type) as VType,Max(LG.V_Date) as VDate,Max(SG.Name) As PName,"
            'mQry = mQry + "Max(LG.SubCode) as SubCode,Max(LG.Narration) as Narration,Max(LG.AmtDr) as Amt1,0 As Amt2,IsNull(Sum(LA.Amount),0) as Amt, "
            'mQry = mQry + "Max(SG.Add1)As Add1,Max(SG.Add2)As Add2,Max(C.CityName)As CityName,Max(CT.Name) as Country,MAx(St.name) As SiteName,max(Ag.GroupName) as AcGroupName, Max(Lg.RecId) As RecId, "
            'mQry = mQry + "Max(DateAdd(Day,IsNull(Sg.CreditDays,0),Lg.V_Date)) As DueDate, "
            'mQry = mQry + "Max(Datediff(Day, Dateadd(Day,Sg.CreditDays,Lg.V_Date),getdate())) AS OverDue "
            'mQry = mQry + "From Ledger LG Left Join SubGroup SG On LG.Subcode=SG.SubCode Left Join "
            'mQry = mQry + "City C on SG.CityCode=C.CityCode Left Join Country CT on SG.CountryCode=CT.Code LEFT JOIN AcGroup AG ON SG.GroupCode =AG.GroupCode  "
            'mQry = mQry + "Left Join LedgerAdj LA On LG.DocId=LA.Adj_DocID  And LG.V_SNo=LA.Adj_V_SNo "
            'mQry = mQry + "LEFT JOIN SiteMast ST ON LG.Site_Code =St.code  "
            'mQry = mQry + "LEFT JOIN ZoneMast ZM ON ZM.Code =SG.Zone "
            'mQry = mQry + mCondStr1
            'mQry = mQry + "Group By LG.DocId,LG.V_SNo "
            'mQry = mQry + "HAVING(IsNull(Sum(LA.Amount), 0) <> Max(LG.AmtDr))"
            'mQry = mQry + "Union All "
            'mQry = mQry + "Select	LG.DocId,LG.V_SNo,Convert(Varchar,LG.V_No) As V_No,LG.V_Type,LG.V_Date,SG.Name As PName,LG.SubCode, "
            'mQry = mQry + "LG.Narration,0 As Amt1,ISNULL(LG.AmtCr,0)-ISNULL(T.AMOUNT,0) as Amt2,0 As Amount,Null As Add1,Null As Add2,"
            'mQry = mQry + "Null As CityName,Null As Country,ST.name As sitename,isnull(Ag.GroupName,'') as AcGroupName, Lg.RecId As RecId,  "
            'mQry = mQry + "DateAdd(Day,IsNull(Sg.CreditDays,0),Lg.V_Date) As DueDate, "
            'mQry = mQry + "Datediff(Day, Dateadd(Day,Sg.CreditDays,Lg.V_Date),getdate()) AS OverDue "
            'mQry = mQry + "From Ledger LG Left Join SubGroup SG On SG.SubCode=LG.SubCode LEFT JOIN AcGroup AG ON SG.GroupCode =AG.GroupCode LEFT JOIN ZoneMast ZM ON ZM.Code =SG.Zone  LEFT JOIN SiteMast ST ON LG.Site_Code =St.code   "
            'mQry = mQry + "LEFT JOIN (SELECT LA.Vr_Docid AS Docid,LA.Vr_V_SNo AS S_No,SUM(AMOUNT) AS AMOUNT FROM LedgerAdj LA GROUP BY LA.Vr_DocId,LA.Vr_V_SNo) T ON T.DOCID=LG.DOCID AND T.S_NO=LG.V_SNO  "
            'mQry = mQry + mCondStr2

            DsRep = AgL.FillData(mQry, AgL.GCn)

            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records to Print!")

            ReportFrm.PrintReport(DsRep, RepName, RepTitle)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
#End Region

#Region "Vat Anexure Reports"
    Private Sub ProcVatAnexureReports()
        Dim SubTitle$ = ""
        Dim GroupHeaderTitle1$ = "", GroupHeaderTitle2$ = ""
        Dim IsReturn As Integer = 0
        Dim AssessmentYear$ = ""
        Try
            Dim mCondStr$ = ""

            If ReportFrm.FGetText(2) = "" Then
                MsgBox("Select Report Type First...!", MsgBoxStyle.Information)
                Exit Sub
            End If

            If AgL.StrCmp(ReportFrm.FGetText(2), "Annexure A") Then
                RepName = "Vat_Annexure_A" : RepTitle = "Annexure A" : SubTitle = "List of Purchases made against tax invoice"
                GroupHeaderTitle1 = "Purchase in Own Account" : GroupHeaderTitle2 = "Purchase in Commission Account"
                IsReturn = 0
                mCondStr = " Where Vt.Category = '" & ClsMain.Voucher_Category.Purchase & "'"
                mCondStr = mCondStr & " AND H.V_Date Between '" & ReportFrm.FGetText(0) & "' And '" & ReportFrm.FGetText(1) & "' "
                mCondStr = mCondStr & " AND IsNull(H.Qty,0) > 0  "
                mCondStr = mCondStr & " AND H.TinNo Is Not Null "
                mCondStr = mCondStr & " And S.Nature = '" & ClsMain.SalesTaxGroupPartyNature.Local & "'  "
            ElseIf AgL.StrCmp(ReportFrm.FGetText(2), "Annexure A1") Then
                RepName = "Vat_Annexure_A" : RepTitle = "Annexure A1" : SubTitle = "List of Purchases Return"
                GroupHeaderTitle1 = "Purchase in Own Account" : GroupHeaderTitle2 = "Purchase in Commission Account"
                IsReturn = 1
                mCondStr = " Where Vt.Category = '" & ClsMain.Voucher_Category.Purchase & "'"
                mCondStr = mCondStr & " AND H.V_Date Between '" & ReportFrm.FGetText(0) & "' And '" & ReportFrm.FGetText(1) & "' "
                mCondStr = mCondStr & " AND IsNull(H.Qty,0) < 0  "
                mCondStr = mCondStr & " AND H.TinNo Is Not Null "
                mCondStr = mCondStr & " And S.Nature = '" & ClsMain.SalesTaxGroupPartyNature.Local & "'  "
            ElseIf AgL.StrCmp(ReportFrm.FGetText(2), "Annexure B") Then
                RepName = "Vat_Annexure_B" : RepTitle = "Annexure B" : SubTitle = "List of Sale made against tax invoice"
                GroupHeaderTitle1 = "Sale in Own Account" : GroupHeaderTitle2 = "Sale in Commission Account"
                IsReturn = 0
                mCondStr = " Where Vt.Category = '" & ClsMain.Voucher_Category.Sale & "'"
                mCondStr = mCondStr & " AND H.V_Date Between '" & ReportFrm.FGetText(0) & "' And '" & ReportFrm.FGetText(1) & "' "
                mCondStr = mCondStr & " AND IsNull(H.Qty,0) > 0  "
                mCondStr = mCondStr & " AND H.TinNo Is Not Null "
                mCondStr = mCondStr & " And S.Nature = '" & ClsMain.SalesTaxGroupPartyNature.Local & "'  "
            ElseIf AgL.StrCmp(ReportFrm.FGetText(2), "Annexure B1") Then
                RepName = "Vat_Annexure_B" : RepTitle = "Annexure B1" : SubTitle = "List of Sales Return"
                GroupHeaderTitle1 = "Details of Sale Returned VAT Goods" : GroupHeaderTitle2 = "Details of Sale Returned NON-VAT Goods"
                IsReturn = 1
                mCondStr = " Where Vt.Category = '" & ClsMain.Voucher_Category.Sale & "'"
                mCondStr = mCondStr & " AND H.V_Date Between '" & ReportFrm.FGetText(0) & "' And '" & ReportFrm.FGetText(1) & "' "
                mCondStr = mCondStr & " AND IsNull(H.Qty,0) < 0  "
                mCondStr = mCondStr & " AND H.TinNo Is Not Null "
                mCondStr = mCondStr & " And S.Nature = '" & ClsMain.SalesTaxGroupPartyNature.Local & "'  "
            ElseIf AgL.StrCmp(ReportFrm.FGetText(2), "Annexure C") Then
                RepName = "Vat_Annexure_C" : RepTitle = "Annexure C" : SubTitle = ""
                GroupHeaderTitle1 = "Purchase in Own Account" : GroupHeaderTitle2 = ""
                IsReturn = 0
                mCondStr = " Where Vt.Category = '" & ClsMain.Voucher_Category.Sale & "'"
                mCondStr = mCondStr & " AND H.V_Date Between '" & ReportFrm.FGetText(0) & "' And '" & ReportFrm.FGetText(1) & "' "
                mCondStr = mCondStr & " AND IsNull(H.Qty,0) > 0  "
                mCondStr = mCondStr & " AND H.FormNo Is Not Null  "
            ElseIf AgL.StrCmp(ReportFrm.FGetText(2), "Return Of Tax") Then

            End If

            mQry = " Select cyear From Company Where Comp_Code = '" & AgL.PubCompCode & "' "
            AssessmentYear = AgL.XNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar)

            mQry = " SELECT 1 As Serial, 1 as GroupSr, '" & GroupHeaderTitle1 & "' As Title, H.DocId, Max(H.Sr) Sr, Max(H.V_Type) V_Type, Max(H.V_Date) V_Date, Max(H.SubCode) SubCode, Max(H.TINNo) TINNo, " & _
                    " Max(H.RecId) RecId, Max(H.Item) Item, Abs(Max(H.Qty)) As Qty, Max(H.Unit) Unit, " & _
                    " Abs(Sum(H.Sales_Tax_Taxable_Amt)) As Sales_Tax_Taxable_Amt, Abs(Max(H.Sales_Tax_Per)) As Sales_Tax_Per, Abs(Sum(H.Sales_Tax)) As Sales_Tax, Abs(Max(H.Additional_Sales_Tax_Per)) As Additional_Sales_Tax_Per, Abs(Sum(H.Additional_Sales_Tax)) As Additional_Sales_Tax, Abs(Sum(H.Net_Amount)) As Net_Amount,  " & _
                    " Max(Sg.DispName) AS PartyName, Max(I.VatCommodityCode) as VatCommodityCode, Max(V.ManualCode) AS VatCommodityManualCode, " & _
                    " Abs(Sum(H.Sales_Tax_Taxable_Amt)) + Abs(Sum(H.Sales_Tax)) + Abs(Sum(H.Additional_Sales_Tax)) As TotalAmount, " & _
                    " '" & SubTitle & "' As SubTitle, " & IsReturn & " As IsReturn, Max(H.FormNo) As FormNo, '" & AssessmentYear & "' As AssessmentYear, '" & ReportFrm.FGetText(1) & "' As TaxPeriodEndDate    " & _
                    " FROM View_Vat H With (NoLock) " & _
                    " LEFT JOIN PostingGroupSalesTaxParty S On H.SalesTaxGroupParty = S.Description " & _
                    " LEFT JOIN SubGroup Sg With (NoLock) ON H.SubCode = Sg.SubCode " & _
                    " LEFT JOIN Item I With (NoLock) ON H.Item = I.Code  " & _
                    " LEFT JOIN VatCommodityCode V With (NoLock) ON I.VatCommodityCode = V.Code " & _
                    " LEFT JOIN Voucher_Type Vt With (NoLock) On H.V_Type = Vt.V_Type " & mCondStr & _
                    " Group By H.DocID, H.Sales_Tax_Per, H.Additional_Sales_Tax_Per "
            mQry += " Union All "
            mQry += " SELECT 2 As Serial, 1 as GroupSr, '" & GroupHeaderTitle1 & "' As Title, 'ZZZZZ' DocId, Null Sr, Null V_Type, Null V_Date, Null SubCode, Null TINNo, " & _
                    " Null RecId, Null Item, Null As Qty, Null Unit, " & _
                    " Abs(Sum(H.Sales_Tax_Taxable_Amt)) As Sales_Tax_Taxable_Amt, Abs(Max(H.Sales_Tax_Per)) As Sales_Tax_Per, Abs(Sum(H.Sales_Tax)) As Sales_Tax, Abs(Max(H.Additional_Sales_Tax_Per)) As Additional_Sales_Tax_Per, Abs(Sum(H.Additional_Sales_Tax)) As Additional_Sales_Tax, Abs(Sum(H.Net_Amount)) As Net_Amount,  " & _
                    " 'Tax Wise Breakup' AS PartyName, Null VatCommodityCode, Null VatCommodityManualCode, " & _
                    " Abs(Sum(H.Sales_Tax_Taxable_Amt)) + Abs(Sum(H.Sales_Tax)) + Abs(Sum(H.Additional_Sales_Tax)) As TotalAmount, " & _
                    " '" & SubTitle & "' As SubTitle, " & IsReturn & " As IsReturn, Null FormNo, '" & AssessmentYear & "' As AssessmentYear, '" & ReportFrm.FGetText(1) & "' As TaxPeriodEndDate    " & _
                    " FROM View_Vat H With (NoLock) " & _
                    " LEFT JOIN PostingGroupSalesTaxParty S On H.SalesTaxGroupParty = S.Description " & _
                    " LEFT JOIN SubGroup Sg With (NoLock) ON H.SubCode = Sg.SubCode " & _
                    " LEFT JOIN Item I With (NoLock) ON H.Item = I.Code  " & _
                    " LEFT JOIN VatCommodityCode V With (NoLock) ON I.VatCommodityCode = V.Code " & _
                    " LEFT JOIN Voucher_Type Vt With (NoLock) On H.V_Type = Vt.V_Type " & mCondStr & _
                    " Group By H.Sales_Tax_Per, H.Additional_Sales_Tax_Per "

            If GroupHeaderTitle2 <> "" Then
                mQry += " UNION ALL "

                mQry += " SELECT 3 As Serial, 2 as GroupSr, '" & GroupHeaderTitle2 & "' As Title, Null As DocId, Null As Sr, Null As V_Type, Null As V_Date, Null As SubCode, Null As TINNo, Null As RecId, Null As Item, Null As Qty, Null As Unit, " & _
                        " Null As Sales_Tax_Taxable_Amt, Null As Sales_Tax_Per, Null As Sales_Tax, Null As Additional_Sales_Tax_Per, Null As Additional_Sales_Tax, Null As Net_Amount,  " & _
                        " Null AS PartyName, Null As VatCommodityCode, Null As  VatCommodityManualCode, " & _
                        " Null  As TotalAmount  ,'" & SubTitle & "' As SubTitle, " & IsReturn & " As IsReturn, Null As FormNo, '" & AssessmentYear & "' As AssessmentYear, '" & ReportFrm.FGetText(1) & "' As TaxPeriodEndDate         "

            End If
            mQry += " Order By Serial, V_Date, RecID, DocID "

            DsRep = AgL.FillData(mQry, AgL.GCn)

            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records to Print!")

            If AgL.StrCmp(ReportFrm.FGetText(2), "Return Of Tax") Then
                FWriteTaxReturn(DsRep)
            ElseIf AgL.StrCmp(ReportFrm.FGetText(2), "Return Form 24") Then
                StrMonth = CDate(ReportFrm.FGetText(0)).Month.ToString
                StrTaxPeriod = "2"
                StrFinancialYear = AgL.XNull(AgL.Dman_Execute(" SELECT cyear FROM Company WHERE Comp_Code =  '" & AgL.PubCompCode & "' ", AgL.GCn).ExecuteScalar)
                If CDate(ReportFrm.FGetText(0)).Month >= 4 And CDate(ReportFrm.FGetText(0)).Month <= 6 Then
                    StrQuarter = "1"
                ElseIf CDate(ReportFrm.FGetText(0)).Month >= 7 And CDate(ReportFrm.FGetText(0)).Month <= 9 Then
                    StrQuarter = "2"
                ElseIf CDate(ReportFrm.FGetText(0)).Month >= 10 And CDate(ReportFrm.FGetText(0)).Month <= 12 Then
                    StrQuarter = "3"
                ElseIf CDate(ReportFrm.FGetText(0)).Month >= 1 And CDate(ReportFrm.FGetText(0)).Month <= 3 Then
                    StrQuarter = "4"
                End If

                Dim xlApp As Excel.Application
                Dim xlWorkBook As Excel.Workbook

                xlApp = New Excel.Application
                xlApp.AlertBeforeOverwriting = False
                xlApp.DisplayAlerts = False

                'Gives an error on the below line
                xlWorkBook = xlApp.Workbooks.Open(My.Application.Info.DirectoryPath + "\" + "ReturnForm24.xls")


                FWriteMainForm(xlWorkBook)
                FWriteVatNonVat(xlWorkBook)
                FWriteTaxDetailSale(xlWorkBook)
                FWriteTaxDetailPurchase(xlWorkBook)
                FWriteVatBankDetail(xlWorkBook)
                FWriteForm24_Annexure_A(xlWorkBook)
                FWriteForm24_Annexure_A1(xlWorkBook)
                FWriteForm24_Annexure_A2(xlWorkBook)
                FWriteForm24_Annexure_B(xlWorkBook)
                FWriteForm24_Annexure_B1(xlWorkBook)
                FWriteCommAgent(xlWorkBook)
                FWriteForm24_Annexure_C(xlWorkBook)
                FWriteForm24_Annexure_D(xlWorkBook)
                FWriteForm24_Annexure_E(xlWorkBook)
                FWriteForm24_Annexure_F(xlWorkBook)

                xlWorkBook.Save()
                xlWorkBook.Close()
                xlApp.Quit()

                ClsMain.FReleaseObjects(xlApp)
                ClsMain.FReleaseObjects(xlWorkBook)

                System.Diagnostics.Process.Start(My.Application.Info.DirectoryPath + "\" + "ReturnForm24.xls")
            Else
                ReportFrm.PrintReport(DsRep, RepName, RepTitle)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
#End Region

    Private Sub FWriteMainForm(ByVal xlWorkBook As Excel.Workbook)
        Dim TotalSalesAndAddSalesTaxOnPurchaseForLocal As Double = AgL.VNull(AgL.Dman_Execute("SELECT Str(IsNull(Sum(H.Sales_Tax),0) + IsNull(Sum(H.Additional_Sales_Tax),0),15,2) FROM View_Vat H LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type LEFT JOIN SubGroup Sg ON H.SubCode = Sg.SubCode LEFT JOIN PostingGroupSalesTaxParty S On H.SalesTaxGroupParty = S.Description WHERE Vt.Category = '" & ClsMain.Voucher_Category.Purchase & "'  AND H.V_Date BETWEEN '" & ReportFrm.FGetText(0) & "' And '" & ReportFrm.FGetText(1) & "' And IsNull(S.Nature,'" & ClsMain.SalesTaxGroupPartyNature.Local & "') = '" & ClsMain.SalesTaxGroupPartyNature.Local & "' ", AgL.GCn).ExecuteScalar)

        mQry = "SELECT Str(IsNull(Sum(H.Sales_Tax),0) + IsNull(Sum(H.Additional_Sales_Tax),0),15,2) FROM View_Vat H LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type LEFT JOIN SubGroup Sg ON H.SubCode = Sg.SubCode LEFT JOIN PostingGroupSalesTaxParty S On H.SalesTaxGroupParty = S.Description WHERE Vt.Category = '" & ClsMain.Voucher_Category.Purchase & "'  AND H.V_Date BETWEEN '" & ReportFrm.FGetText(0) & "' And '" & ReportFrm.FGetText(1) & "' And IsNull(S.Nature,'" & ClsMain.SalesTaxGroupPartyNature.Local & "') = '" & ClsMain.SalesTaxGroupPartyNature.Local & "' "

        Dim TotalSalesAndAddSalesTaxOnSale$ = AgL.VNull(AgL.Dman_Execute("SELECT Str(IsNull(Sum(H.Sales_Tax),0) + IsNull(Sum(H.Additional_Sales_Tax),0),15,2) FROM View_Vat H LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type LEFT JOIN SubGroup Sg ON H.SubCode = Sg.SubCode LEFT JOIN PostingGroupSalesTaxParty S On H.SalesTaxGroupParty = S.Description WHERE Vt.Category = '" & ClsMain.Voucher_Category.Sale & "'  AND H.V_Date BETWEEN '" & ReportFrm.FGetText(0) & "' And '" & ReportFrm.FGetText(1) & "'  ", AgL.GCn).ExecuteScalar)

        Dim PurchaseInOwnAcAgainstTaxInvoice$ = AgL.VNull(AgL.Dman_Execute(" SELECT Str(IsNull(Sum(H.Sales_Tax_Taxable_Amt),0),15,2) FROM View_Vat H LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type LEFT JOIN SubGroup Sg ON H.SubCode = Sg.SubCode LEFT JOIN PostingGroupSalesTaxParty S On H.SalesTaxGroupParty = S.Description WHERE Vt.Category = '" & ClsMain.Voucher_Category.Purchase & "' AND H.V_Date BETWEEN '" & ReportFrm.FGetText(0) & "' And '" & ReportFrm.FGetText(1) & "' And IsNull(H.Sales_Tax,0) <> 0 And IsNull(S.Nature,'" & ClsMain.SalesTaxGroupPartyNature.Local & "') = '" & ClsMain.SalesTaxGroupPartyNature.Local & "'  And H.TINNo IS NOT NULL ", AgL.GCn).ExecuteScalar)
        Dim PurchaseInOwnAcFromNonRegisteredDealer$ = AgL.VNull(AgL.Dman_Execute(" SELECT Str(IsNull(Sum(H.Sales_Tax_Taxable_Amt),0),15,2) FROM View_Vat H LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type LEFT JOIN SubGroup Sg ON H.SubCode = Sg.SubCode LEFT JOIN PostingGroupSalesTaxParty S On H.SalesTaxGroupParty = S.Description WHERE Vt.Category = '" & ClsMain.Voucher_Category.Purchase & "' AND H.V_Date BETWEEN '" & ReportFrm.FGetText(0) & "' And '" & ReportFrm.FGetText(1) & "' And IsNull(H.Sales_Tax,0) <> 0 And IsNull(S.Nature,'" & ClsMain.SalesTaxGroupPartyNature.Local & "') = '" & ClsMain.SalesTaxGroupPartyNature.Local & "'  And H.TINNo IS NULL ", AgL.GCn).ExecuteScalar)
        Dim PurchaseOfExcemptGoods$ = AgL.VNull(AgL.Dman_Execute(" SELECT Str(IsNull(Sum(H.Sales_Tax_Taxable_Amt),0),15,2) FROM View_Vat H LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type LEFT JOIN SubGroup Sg ON H.SubCode = Sg.SubCode LEFT JOIN PostingGroupSalesTaxParty S On H.SalesTaxGroupParty = S.Description WHERE Vt.Category = '" & ClsMain.Voucher_Category.Purchase & "' AND H.V_Date BETWEEN '" & ReportFrm.FGetText(0) & "' And '" & ReportFrm.FGetText(1) & "' And IsNull(H.Sales_Tax,0) = 0 And IsNull(S.Nature,'" & ClsMain.SalesTaxGroupPartyNature.Local & "')  = '" & ClsMain.SalesTaxGroupPartyNature.Local & "'  ", AgL.GCn).ExecuteScalar)

        Dim TotalSalesAndAddSalesTaxOnPurchaseForAll$ = AgL.VNull(AgL.Dman_Execute("SELECT Str(IsNull(Sum(H.Sales_Tax),0) + IsNull(Sum(H.Additional_Sales_Tax),0),15,2) FROM View_Vat H LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type LEFT JOIN SubGroup Sg ON H.SubCode = Sg.SubCode LEFT JOIN PostingGroupSalesTaxParty S On H.SalesTaxGroupParty = S.Description WHERE Vt.Category = '" & ClsMain.Voucher_Category.Purchase & "'  AND H.V_Date BETWEEN '" & ReportFrm.FGetText(0) & "' And '" & ReportFrm.FGetText(1) & "' And IsNull(S.Nature,'" & ClsMain.SalesTaxGroupPartyNature.Local & "') = '" & ClsMain.SalesTaxGroupPartyNature.Local & "' ", AgL.GCn).ExecuteScalar)
        Dim SaleInOwnAcAgainstTaxInvoice$ = AgL.VNull(AgL.Dman_Execute(" SELECT Str(IsNull(Sum(H.Sales_Tax_Taxable_Amt),0),15,2) FROM View_Vat H LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type LEFT JOIN SubGroup Sg ON H.SubCode = Sg.SubCode LEFT JOIN PostingGroupSalesTaxParty S On H.SalesTaxGroupParty = S.Description WHERE Vt.Category = '" & ClsMain.Voucher_Category.Sale & "' AND H.V_Date BETWEEN '" & ReportFrm.FGetText(0) & "' And '" & ReportFrm.FGetText(1) & "' And IsNull(H.Sales_Tax,0) <> 0 And IsNull(S.Nature,'" & ClsMain.SalesTaxGroupPartyNature.Local & "') = '" & ClsMain.SalesTaxGroupPartyNature.Local & "' And H.TINNo IS NOT NULL And H.SalesTaxGroupParty = 'Local' ", AgL.GCn).ExecuteScalar)


        Dim NetTax$ = AgL.VNull(AgL.Dman_Execute(" Select " & _
                " Str((SELECT IsNull(Sum(H.Sales_Tax),0) + IsNull(Sum(H.Additional_Sales_Tax),0) FROM View_Vat H LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type LEFT JOIN SubGroup Sg ON H.SubCode = Sg.SubCode LEFT JOIN PostingGroupSalesTaxParty S On H.SalesTaxGroupParty = S.Description WHERE Vt.Category = '" & ClsMain.Voucher_Category.Sale & "'  AND H.V_Date BETWEEN '" & ReportFrm.FGetText(0) & "' And '" & ReportFrm.FGetText(1) & "'  And IsNull(S.Nature,'" & ClsMain.SalesTaxGroupPartyNature.Local & "') = '" & ClsMain.SalesTaxGroupPartyNature.Local & "' ) " & _
                " - (SELECT IsNull(Sum(H.Sales_Tax),0) + IsNull(Sum(H.Additional_Sales_Tax),0) FROM View_Vat H LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type LEFT JOIN SubGroup Sg ON H.SubCode = Sg.SubCode LEFT JOIN PostingGroupSalesTaxParty S On H.SalesTaxGroupParty = S.Description WHERE Vt.Category = '" & ClsMain.Voucher_Category.Purchase & "'  AND H.V_Date BETWEEN '" & ReportFrm.FGetText(0) & "' And '" & ReportFrm.FGetText(1) & "'  And IsNull(S.Nature,'" & ClsMain.SalesTaxGroupPartyNature.Local & "') = '" & ClsMain.SalesTaxGroupPartyNature.Local & "' ),15,2)   ", AgL.GCn).ExecuteScalar)

        Try
            Dim xlWorkSheet As Excel.Worksheet
            xlWorkSheet = xlWorkBook.Worksheets("Main_Form")
            xlWorkSheet.Range("B2:B8").ClearContents()
            xlWorkSheet.Range("B10:B13").ClearContents()
            xlWorkSheet.Range("B15:B15").ClearContents()
            xlWorkSheet.Range("B17:B21").ClearContents()
            xlWorkSheet.Range("B23:B24").ClearContents()

            'edit the cell with new value
            xlWorkSheet.Cells(2, 2) = AgL.PubCompTIN
            xlWorkSheet.Cells(3, 2) = StrFinancialYear
            xlWorkSheet.Cells(4, 2) = StrTaxPeriod
            xlWorkSheet.Cells(5, 2) = StrMonth
            xlWorkSheet.Cells(6, 2) = StrQuarter
            xlWorkSheet.Cells(7, 2) = 0.0
            xlWorkSheet.Cells(8, 2) = 0.0
            xlWorkSheet.Cells(10, 2) = 0.0
            xlWorkSheet.Cells(11, 2) = TotalSalesAndAddSalesTaxOnSale
            xlWorkSheet.Cells(12, 2) = 0.0
            xlWorkSheet.Cells(13, 2) = 0.0
            xlWorkSheet.Cells(15, 2) = 0.0
            xlWorkSheet.Cells(17, 2) = TotalSalesAndAddSalesTaxOnPurchaseForAll
            xlWorkSheet.Cells(18, 2) = 0.0
            xlWorkSheet.Cells(19, 2) = 0.0
            xlWorkSheet.Cells(20, 2) = 0.0
            xlWorkSheet.Cells(21, 2) = 0.0
            xlWorkSheet.Cells(23, 2) = TotalSalesAndAddSalesTaxOnPurchaseForLocal
            xlWorkSheet.Cells(24, 2) = 0.0
            xlWorkSheet.Cells(25, 2) = 0.0
            'xlWorkSheet.Cells(29, 2) = NetTax

            ClsMain.FReleaseObjects(xlWorkSheet)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FWriteVatNonVat(ByVal xlWorkBook As Excel.Workbook)
        Dim VPurchaseInOwnAcAgainstTaxInvoice$ = AgL.VNull(AgL.Dman_Execute(" SELECT Str(IsNull(Sum(H.Sales_Tax_Taxable_Amt),0),15,2) FROM View_Vat H LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type LEFT JOIN SubGroup Sg ON H.SubCode = Sg.SubCode LEFT JOIN PostingGroupSalesTaxParty S On H.SalesTaxGroupParty = S.Description WHERE Vt.Category = '" & ClsMain.Voucher_Category.Purchase & "' AND H.V_Date BETWEEN '" & ReportFrm.FGetText(0) & "' And '" & ReportFrm.FGetText(1) & "' And IsNull(H.Qty,0) > 0 And IsNull(H.Sales_Tax,0) > 0 And IsNull(S.Nature,'" & ClsMain.SalesTaxGroupPartyNature.Local & "') = '" & ClsMain.SalesTaxGroupPartyNature.Local & "'  And H.TINNo IS NOT NULL ", AgL.GCn).ExecuteScalar)
        Dim VPurchaseInOwnAcFromNonRegisteredDealer$ = AgL.VNull(AgL.Dman_Execute(" SELECT Str(IsNull(Sum(H.Sales_Tax_Taxable_Amt),0),15,2) FROM View_Vat H LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type LEFT JOIN SubGroup Sg ON H.SubCode = Sg.SubCode LEFT JOIN PostingGroupSalesTaxParty S On H.SalesTaxGroupParty = S.Description WHERE Vt.Category = '" & ClsMain.Voucher_Category.Purchase & "' AND H.V_Date BETWEEN '" & ReportFrm.FGetText(0) & "' And '" & ReportFrm.FGetText(1) & "' And IsNull(H.Qty,0) > 0 And IsNull(H.Sales_Tax,0) > 0 And IsNull(S.Nature,'" & ClsMain.SalesTaxGroupPartyNature.Local & "') = '" & ClsMain.SalesTaxGroupPartyNature.Local & "'  And H.TINNo IS NULL ", AgL.GCn).ExecuteScalar)
        Dim VPurchaseOfExcemptGoods$ = AgL.VNull(AgL.Dman_Execute(" SELECT Str(IsNull(Sum(H.Sales_Tax_Taxable_Amt),0),15,2) FROM View_Vat H LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type LEFT JOIN SubGroup Sg ON H.SubCode = Sg.SubCode LEFT JOIN PostingGroupSalesTaxParty S On H.SalesTaxGroupParty = S.Description WHERE Vt.Category = '" & ClsMain.Voucher_Category.Purchase & "' AND H.V_Date BETWEEN '" & ReportFrm.FGetText(0) & "' And '" & ReportFrm.FGetText(1) & "' And IsNull(H.Qty,0) > 0 And IsNull(H.Sales_Tax,0) = 0 And IsNull(S.Nature,'" & ClsMain.SalesTaxGroupPartyNature.Local & "')  = '" & ClsMain.SalesTaxGroupPartyNature.Local & "'  ", AgL.GCn).ExecuteScalar)
        Dim VPurchaseOfExUp$ = AgL.VNull(AgL.Dman_Execute(" SELECT Sum(H.Sales_Tax_Taxable_Amt) FROM View_Vat H LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type LEFT JOIN SubGroup Sg ON H.SubCode = Sg.SubCode LEFT JOIN PostingGroupSalesTaxParty S On H.SalesTaxGroupParty = S.Description WHERE Vt.Category = '" & ClsMain.Voucher_Category.Purchase & "' AND H.V_Date BETWEEN '" & ReportFrm.FGetText(0) & "' And '" & ReportFrm.FGetText(1) & "' And IsNull(H.Qty,0) > 0 And IsNull(S.Nature,'" & ClsMain.SalesTaxGroupPartyNature.Local & "')  = '" & ClsMain.SalesTaxGroupPartyNature.Central & "' ", AgL.GCn).ExecuteScalar)
        Dim VPurchaseInPrincipalAc As Double = 0
        Dim VPurchaseAgainstTaxInvoice As Double = 0
        Dim VOtherPurchase As Double = 0
        Dim VExUPPrinicipal As Double = 0
        Dim NVUPPrinicipal As Double = 0
        Dim NVExUPPrinicipal As Double = 0
        Dim VAnyOtherPurchase As Double = 0
        Dim NVPurchaseFromRegisteredDealers As Double = 0
        Dim NVPurchaseFromUnRegisteredDealers As Double = 0
        Dim NVPurchaseOfExcemptedGoods As Double = 0
        Dim NVPurchaseFromExUP As Double = 0
        Dim NVPurchaseInPrincipalAc As Double = 0
        Dim CGPurchaseAgainstTaxInvoice As Double = 0
        Dim CGPurchaseFromPersonOtherThanRegisteredDealer As Double = 0
        Dim NVAnyOtherPurchase As Double = 0
        Dim OSPurchaseAgainstFormCFormHFormI As Double = 0
        Dim OSValueOfGoodsFromOtherStateFormF As Double = 0
        Dim VTurnOverOfSaleInOwnAcAgainstTaxInvoice As Double = 0
        Dim VTurnOverOfSaleOtherThenColumn1 As Double = 0
        Dim VTurnOverOfSaleOfExcemptedGoods As Double = 0
        Dim VInterStateSaleAgainstFormC As Double = 0
        Dim VInterStateSaleWithoutFormC As Double = 0
        Dim VSaleInCourseOfExport As Double = 0
        Dim VSaleInCourseOfImport As Double = 0
        Dim VSaleOutsideState As Double = 0
        Dim VConsignmentSaleTransfer As Double = 0
        Dim VAnyOtherSale As Double = 0
        Dim NVTaxableTurnoverOfSale As Double = 0
        Dim NVExcemptedTurnoverOfSale As Double = 0
        Dim NVTaxPaidTurnOverOfGoods As Double = 0
        Dim NVSaleInPrincipalAc As Double = 0
        Dim VSaleAgainstTaxInvoice As Double = 0
        Dim VOtherSales As Double = 0
        Dim VExUPPrincipal As Double = 0
        Dim NVUPPrincipal As Double = 0
        Dim NVExUPPrincipal As Double = 0
        Dim NVAnyOtherSaleAmount As Double = 0

        Try
            Dim xlWorkSheet As Excel.Worksheet
            xlWorkSheet = xlWorkBook.Worksheets("Vat_Non_Vat")
            xlWorkSheet.Range("J2:J42").ClearContents()

            'edit the cell with new value
            Dim I As Integer = 0
            For I = 2 To 42
                xlWorkSheet.Cells(I, 1) = AgL.PubCompTIN
                xlWorkSheet.Cells(I, 2) = StrFinancialYear
                xlWorkSheet.Cells(I, 3) = StrTaxPeriod
                xlWorkSheet.Cells(I, 4) = StrMonth
                xlWorkSheet.Cells(I, 5) = StrQuarter
            Next

            xlWorkSheet.Cells(2, 10) = VPurchaseInOwnAcAgainstTaxInvoice
            xlWorkSheet.Cells(3, 10) = VPurchaseInOwnAcFromNonRegisteredDealer
            xlWorkSheet.Cells(4, 10) = VPurchaseOfExcemptGoods
            xlWorkSheet.Cells(5, 10) = VPurchaseOfExUp
            xlWorkSheet.Cells(6, 10) = VPurchaseInPrincipalAc
            xlWorkSheet.Cells(7, 10) = VPurchaseAgainstTaxInvoice
            xlWorkSheet.Cells(8, 10) = VOtherPurchase
            xlWorkSheet.Cells(9, 10) = VExUPPrincipal
            xlWorkSheet.Cells(10, 10) = NVUPPrincipal
            xlWorkSheet.Cells(11, 10) = NVExUPPrincipal
            xlWorkSheet.Cells(12, 10) = VAnyOtherPurchase
            xlWorkSheet.Cells(13, 10) = NVPurchaseFromRegisteredDealers
            xlWorkSheet.Cells(14, 10) = NVPurchaseFromUnRegisteredDealers
            xlWorkSheet.Cells(15, 10) = NVPurchaseOfExcemptedGoods
            xlWorkSheet.Cells(16, 10) = NVPurchaseFromExUP
            xlWorkSheet.Cells(17, 10) = NVPurchaseInPrincipalAc
            xlWorkSheet.Cells(18, 10) = CGPurchaseAgainstTaxInvoice
            xlWorkSheet.Cells(19, 10) = CGPurchaseFromPersonOtherThanRegisteredDealer
            xlWorkSheet.Cells(20, 10) = NVAnyOtherPurchase
            xlWorkSheet.Cells(21, 10) = OSPurchaseAgainstFormCFormHFormI
            xlWorkSheet.Cells(22, 10) = OSValueOfGoodsFromOtherStateFormF
            xlWorkSheet.Cells(23, 10) = VTurnOverOfSaleInOwnAcAgainstTaxInvoice
            xlWorkSheet.Cells(24, 10) = VTurnOverOfSaleOtherThenColumn1
            xlWorkSheet.Cells(25, 10) = VTurnOverOfSaleOfExcemptedGoods
            xlWorkSheet.Cells(26, 10) = VInterStateSaleAgainstFormC
            xlWorkSheet.Cells(27, 10) = VInterStateSaleWithoutFormC
            xlWorkSheet.Cells(28, 10) = VSaleInCourseOfExport
            xlWorkSheet.Cells(29, 10) = VSaleInCourseOfImport
            xlWorkSheet.Cells(30, 10) = VSaleOutsideState
            xlWorkSheet.Cells(31, 10) = VConsignmentSaleTransfer
            xlWorkSheet.Cells(32, 10) = VAnyOtherSale
            xlWorkSheet.Cells(33, 10) = NVTaxableTurnoverOfSale
            xlWorkSheet.Cells(34, 10) = NVExcemptedTurnoverOfSale
            xlWorkSheet.Cells(35, 10) = NVTaxPaidTurnOverOfGoods
            xlWorkSheet.Cells(36, 10) = NVSaleInPrincipalAc
            xlWorkSheet.Cells(37, 10) = VSaleAgainstTaxInvoice
            xlWorkSheet.Cells(38, 10) = VOtherSales
            xlWorkSheet.Cells(39, 10) = VExUPPrincipal
            xlWorkSheet.Cells(40, 10) = NVUPPrincipal
            xlWorkSheet.Cells(41, 10) = NVExUPPrincipal
            xlWorkSheet.Cells(42, 10) = NVAnyOtherSaleAmount


            ClsMain.FReleaseObjects(xlWorkSheet)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FWriteTaxDetailSale(ByVal xlWorkBook As Excel.Workbook)
        Dim DtSale As DataTable = Nothing
        Dim mSaleQry$ = ""
        Dim I As Integer = 0
        Dim J As Integer = 0

        mSaleQry = " SELECT H.Sales_Tax_Per As Sales_Tax_Per, ISNULL(VC.ManualCode,'') AS VatCommodityCode, " & _
                " Sum(H.Sales_Tax_Taxable_Amt) AS Sales_Tax_Taxable_Amt, " & _
                " Sum(H.Sales_Tax) AS Sales_Tax, 'V' As VatNonVat " & _
                " FROM View_Vat H  " & _
                " LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type " & _
                " LEFT JOIN Item I ON I.Code = H.Item " & _
                " LEFT JOIN VatCommodityCode VC ON VC.Code = I.VatCommodityCode " & _
                " WHERE Vt.Category = '" & ClsMain.Voucher_Category.Sale & "' " & _
                " And IsNull(H.Sales_Tax_Per,0) <> 0 " & _
                " And H.V_Date BETWEEN '" & ReportFrm.FGetText(0) & "' And '" & ReportFrm.FGetText(1) & "' " & _
                " GROUP BY H.Sales_Tax_Per , VC.ManualCode "

        mSaleQry += "UNION ALL "

        mSaleQry += " SELECT H.Additional_Sales_Tax_Per As Sales_Tax_Per, ISNULL(VC.ManualCode,'') AS VatCommodityCode, " & _
                " Sum(H.Sales_Tax_Taxable_Amt) AS Sales_Tax_Taxable_Amt, " & _
                " Sum(H.Additional_Sales_Tax) AS Sales_Tax, 'AT' As VatNonVat  " & _
                " FROM View_Vat H  " & _
                " LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type " & _
                " LEFT JOIN Item I ON I.Code = H.Item " & _
                " LEFT JOIN VatCommodityCode VC ON VC.Code = I.VatCommodityCode " & _
                " WHERE Vt.Category = '" & ClsMain.Voucher_Category.Sale & "' " & _
                " And IsNull(H.Additional_Sales_Tax,0) <> 0 " & _
                " And H.V_Date BETWEEN '" & ReportFrm.FGetText(0) & "' And '" & ReportFrm.FGetText(1) & "' " & _
                " GROUP BY H.Additional_Sales_Tax_Per, VC.ManualCode "


        mQry = " Select * From (" & mSaleQry & ") As VMain Order By VatCommodityCode, VatNonVat Desc "
        DtSale = AgL.FillData(mQry, AgL.GCn).Tables(0)

        Try
            Dim xlWorkSheet As Excel.Worksheet
            xlWorkSheet = xlWorkBook.Worksheets("Tax_Detail_Sale")
            xlWorkSheet.Range("A2:IV65536").ClearContents()

            'edit the cell with new value
            J = 2
            For I = 0 To DtSale.Rows.Count - 1
                xlWorkSheet.Cells(J, 1) = AgL.PubCompTIN
                xlWorkSheet.Cells(J, 2) = StrFinancialYear
                xlWorkSheet.Cells(J, 3) = StrTaxPeriod
                xlWorkSheet.Cells(J, 4) = StrMonth
                xlWorkSheet.Cells(J, 5) = StrQuarter
                xlWorkSheet.Cells(J, 6) = AgL.XNull(DtSale.Rows(I)("VatNonVat"))
                xlWorkSheet.Cells(J, 7) = AgL.XNull(DtSale.Rows(I)("VatCommodityCode"))
                xlWorkSheet.Cells(J, 8) = AgL.VNull(DtSale.Rows(I)("Sales_Tax_Per"))
                xlWorkSheet.Cells(J, 9) = AgL.VNull(DtSale.Rows(I)("Sales_Tax_Taxable_Amt"))
                xlWorkSheet.Cells(J, 10) = AgL.VNull(DtSale.Rows(I)("Sales_Tax"))
                J = J + 1
            Next

            ClsMain.FReleaseObjects(xlWorkSheet)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FWriteTaxDetailPurchase(ByVal xlWorkBook As Excel.Workbook)
        Dim DtPurchase As DataTable = Nothing
        Dim mPurchaseQry$ = ""
        Dim I As Integer = 0
        Dim J As Integer = 0

        mPurchaseQry = " SELECT H.Sales_Tax_Per As Sales_Tax_Per, ISNULL(VC.ManualCode,'') AS VatCommodityCode, " & _
                " Sum(H.Sales_Tax_Taxable_Amt) AS Sales_Tax_Taxable_Amt, " & _
                " Sum(H.Sales_Tax) AS Sales_Tax, 'V' As VatNonVat " & _
                " FROM View_Vat H  " & _
                " LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type " & _
                " LEFT JOIN Item I ON I.Code = H.Item " & _
                " LEFT JOIN VatCommodityCode VC ON VC.Code = I.VatCommodityCode " & _
                " WHERE Vt.Category = '" & ClsMain.Voucher_Category.Purchase & "' " & _
                " And IsNull(H.Sales_Tax_Per,0) <> 0 " & _
                " And H.SalesTaxGroupParty = 'Local' " & _
                " And H.V_Date BETWEEN '" & ReportFrm.FGetText(0) & "' And '" & ReportFrm.FGetText(1) & "' " & _
                " GROUP BY H.Sales_Tax_Per , VC.ManualCode "

        mPurchaseQry += "UNION ALL "

        mPurchaseQry += " SELECT H.Additional_Sales_Tax_Per As Sales_Tax_Per, ISNULL(VC.ManualCode,'') AS VatCommodityCode, " & _
                " Sum(H.Sales_Tax_Taxable_Amt) AS Sales_Tax_Taxable_Amt, " & _
                " Sum(H.Additional_Sales_Tax) AS Sales_Tax, 'AT' As VatNonVat  " & _
                " FROM View_Vat H  " & _
                " LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type " & _
                " LEFT JOIN Item I ON I.Code = H.Item " & _
                " LEFT JOIN VatCommodityCode VC ON VC.Code = I.VatCommodityCode " & _
                " WHERE Vt.Category = '" & ClsMain.Voucher_Category.Purchase & "' " & _
                " And H.SalesTaxGroupParty = 'Local' " & _
                " And IsNull(H.Additional_Sales_Tax,0) <> 0 " & _
                " And H.V_Date BETWEEN '" & ReportFrm.FGetText(0) & "' And '" & ReportFrm.FGetText(1) & "' " & _
                " GROUP BY H.Additional_Sales_Tax_Per, VC.ManualCode "


        mQry = " Select * From (" & mPurchaseQry & ") As VMain Order By VatCommodityCode, VatNonVat Desc "
        DtPurchase = AgL.FillData(mQry, AgL.GCn).Tables(0)

        Try
            Dim xlWorkSheet As Excel.Worksheet
            xlWorkSheet = xlWorkBook.Worksheets("Tax_Detail_Purchase")
            xlWorkSheet.Range("A2:IV65536").ClearContents()

            'edit the cell with new value
            J = 2
            For I = 0 To DtPurchase.Rows.Count - 1
                xlWorkSheet.Cells(J, 1) = AgL.PubCompTIN
                xlWorkSheet.Cells(J, 2) = StrFinancialYear
                xlWorkSheet.Cells(J, 3) = StrTaxPeriod
                xlWorkSheet.Cells(J, 4) = StrMonth
                xlWorkSheet.Cells(J, 5) = StrQuarter
                xlWorkSheet.Cells(J, 6) = AgL.XNull(DtPurchase.Rows(I)("VatNonVat"))
                xlWorkSheet.Cells(J, 7) = AgL.XNull(DtPurchase.Rows(I)("VatCommodityCode"))
                xlWorkSheet.Cells(J, 8) = AgL.VNull(DtPurchase.Rows(I)("Sales_Tax_Per"))
                xlWorkSheet.Cells(J, 9) = AgL.VNull(DtPurchase.Rows(I)("Sales_Tax_Taxable_Amt"))
                xlWorkSheet.Cells(J, 10) = AgL.VNull(DtPurchase.Rows(I)("Sales_Tax"))
                J = J + 1
            Next

            ClsMain.FReleaseObjects(xlWorkSheet)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FWriteVatBankDetail(ByVal xlWorkBook As Excel.Workbook)
        Dim I As Integer = 0
        Dim DtSalesTaxForPurchase As DataTable = Nothing
        Dim DtAddSalesTaxForPurchase As DataTable = Nothing

        Try
            Dim xlWorkSheet As Excel.Worksheet
            xlWorkSheet = xlWorkBook.Worksheets("Vat_Bank_Detail")
            xlWorkSheet.Range("A2:IV65536").ClearContents()

            'edit the cell with new value
            'For I = 2 To DtSalesTaxForPurchase.Rows.Count - 1
            '    xlWorkSheet.Cells(I, 1) = AgL.PubCompTIN
            '    xlWorkSheet.Cells(I, 2) = StrFinancialYear
            '    xlWorkSheet.Cells(I, 3) = StrTaxPeriod
            '    xlWorkSheet.Cells(I, 4) = StrMonth
            '    xlWorkSheet.Cells(I, 5) = StrQuarter
            '    xlWorkSheet.Cells(I, 6) = "V"
            'Next
            ClsMain.FReleaseObjects(xlWorkSheet)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FWriteForm24_Annexure_A(ByVal xlWorkBook As Excel.Workbook)
        Dim I As Integer = 0
        Dim J As Integer = 0
        Dim mCondStr As String = ""
        Dim DtSalesTaxForPurchase As DataTable = Nothing

        mCondStr = " Where Vt.Category = '" & ClsMain.Voucher_Category.Purchase & "'"
        mCondStr = mCondStr & " AND H.V_Date Between '" & ReportFrm.FGetText(0) & "' And '" & ReportFrm.FGetText(1) & "' "
        mCondStr = mCondStr & " AND IsNull(H.Qty,0) > 0  "
        mCondStr = mCondStr & " AND H.TinNo Is Not Null "
        mCondStr = mCondStr & " AND IsNull(H.Sales_Tax_Per,0) <> 0 "
        mCondStr = mCondStr & " And S.Nature = '" & ClsMain.SalesTaxGroupPartyNature.Local & "'  "

        mQry = " SELECT Max(H.RecId) AS InvoiceNo,  Max(H.V_Date) AS V_Date, Max(VC.ManualCode) AS CommodityCode,  Sum(H.Qty) AS Qty, " & _
                " Sum(H.Sales_Tax_Taxable_Amt) As Sales_Tax_Taxable_Amt, Sum(H.Sales_Tax) As Sales_Tax, " & _
                " Sum(H.Additional_Sales_Tax) As Additional_Sales_Tax, Sum(H.Net_Amount) As Net_Amount, " & _
                " Max(H.TINNo) As TINNo, Max(H.Unit) As Unit " & _
                " FROM View_Vat H " & _
                " LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type " & _
                " LEFT JOIN Item I ON I.Code = H.Item  " & _
                " LEFT JOIN PostingGroupSalesTaxParty S On H.SalesTaxGroupParty = S.Description " & _
                " LEFT JOIN VatCommodityCode VC ON VC.Code = I.VatCommodityCode  " & _
                " " & mCondStr & " " & _
                " Group By H.DocId " & _
                " Order By V_Date "
        DtSalesTaxForPurchase = AgL.FillData(mQry, AgL.GCn).Tables(0)

        Try
            Dim xlWorkSheet As Excel.Worksheet
            xlWorkSheet = xlWorkBook.Worksheets("Form24_Annexure_A")
            xlWorkSheet.Range("A2:IV65536").ClearContents()

            J = 2
            For I = 0 To DtSalesTaxForPurchase.Rows.Count - 1
                xlWorkSheet.Cells(J, 1) = AgL.PubCompTIN
                xlWorkSheet.Cells(J, 2) = StrFinancialYear
                xlWorkSheet.Cells(J, 3) = StrTaxPeriod
                xlWorkSheet.Cells(J, 4) = StrMonth
                xlWorkSheet.Cells(J, 5) = StrQuarter
                xlWorkSheet.Cells(J, 6) = AgL.XNull(DtSalesTaxForPurchase.Rows(I)("InvoiceNo"))
                xlWorkSheet.Cells(J, 7) = AgL.XNull(DtSalesTaxForPurchase.Rows(I)("V_Date"))
                xlWorkSheet.Cells(J, 8) = AgL.XNull(DtSalesTaxForPurchase.Rows(I)("CommodityCode"))
                xlWorkSheet.Cells(J, 9) = AgL.XNull(DtSalesTaxForPurchase.Rows(I)("Qty"))
                xlWorkSheet.Cells(J, 10) = AgL.VNull(DtSalesTaxForPurchase.Rows(I)("Sales_Tax_Taxable_Amt"))
                xlWorkSheet.Cells(J, 11) = AgL.VNull(DtSalesTaxForPurchase.Rows(I)("Sales_Tax"))
                xlWorkSheet.Cells(J, 12) = AgL.VNull(DtSalesTaxForPurchase.Rows(I)("Additional_Sales_Tax"))
                xlWorkSheet.Cells(J, 13) = AgL.VNull(DtSalesTaxForPurchase.Rows(I)("Net_Amount"))
                xlWorkSheet.Cells(J, 14) = AgL.XNull(DtSalesTaxForPurchase.Rows(I)("TINNo"))
                xlWorkSheet.Cells(J, 15) = AgL.XNull(DtSalesTaxForPurchase.Rows(I)("Unit"))
                xlWorkSheet.Cells(J, 16) = 1
                J = J + 1
            Next
            ClsMain.FReleaseObjects(xlWorkSheet)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FWriteForm24_Annexure_A1(ByVal xlWorkBook As Excel.Workbook)
        Dim I As Integer = 0
        Dim DtSalesTaxForPurchase As DataTable = Nothing
        Dim DtAddSalesTaxForPurchase As DataTable = Nothing

        Try
            Dim xlWorkSheet As Excel.Worksheet
            xlWorkSheet = xlWorkBook.Worksheets("Form24_Annexure_A1")
            xlWorkSheet.Range("A2:IV65536").ClearContents()

            'edit the cell with new value
            'For I = 2 To DtSalesTaxForPurchase.Rows.Count - 1
            '    xlWorkSheet.Cells(I, 1) = AgL.PubCompTIN
            '    xlWorkSheet.Cells(I, 2) = StrFinancialYear
            '    xlWorkSheet.Cells(I, 3) = StrTaxPeriod
            '    xlWorkSheet.Cells(I, 4) = StrMonth
            '    xlWorkSheet.Cells(I, 5) = StrQuarter

            '    xlWorkSheet.Cells(I, 6) = "V"
            'Next

            ClsMain.FReleaseObjects(xlWorkSheet)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FWriteForm24_Annexure_A2(ByVal xlWorkBook As Excel.Workbook)
        Dim I As Integer = 0
        Dim DtSalesTaxForPurchase As DataTable = Nothing
        Dim DtAddSalesTaxForPurchase As DataTable = Nothing

        Try
            Dim xlWorkSheet As Excel.Worksheet
            xlWorkSheet = xlWorkBook.Worksheets("Form24_Annexure_A2")
            xlWorkSheet.Range("A2:IV65536").ClearContents()

            'edit the cell with new value
            'For I = 2 To DtSalesTaxForPurchase.Rows.Count - 1
            '    xlWorkSheet.Cells(I, 1) = AgL.PubCompTIN
            '    xlWorkSheet.Cells(I, 2) = StrFinancialYear
            '    xlWorkSheet.Cells(I, 3) = StrTaxPeriod
            '    xlWorkSheet.Cells(I, 4) = StrMonth
            '    xlWorkSheet.Cells(I, 5) = StrQuarter

            '    xlWorkSheet.Cells(I, 6) = "V"
            'Next

            ClsMain.FReleaseObjects(xlWorkSheet)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FWriteForm24_Annexure_B(ByVal xlWorkBook As Excel.Workbook)
        Dim I As Integer = 0
        Dim mCondStr As String = ""
        Dim DtSalesTaxForSale As DataTable = Nothing
        Dim J As Integer = 0

        mCondStr = " Where Vt.Category = '" & ClsMain.Voucher_Category.Sale & "'"
        mCondStr = mCondStr & " AND H.V_Date Between '" & ReportFrm.FGetText(0) & "' And '" & ReportFrm.FGetText(1) & "' "
        mCondStr = mCondStr & " AND IsNull(H.Qty,0) > 0  "
        mCondStr = mCondStr & " AND H.TinNo Is Not Null "
        mCondStr = mCondStr & " And S.Nature = '" & ClsMain.SalesTaxGroupPartyNature.Local & "'  "

        mQry = " SELECT Max(H.RecId) AS InvoiceNo,  convert(NVARCHAR,Max(H.V_Date),103) AS V_Date, Max(VC.ManualCode) AS CommodityCode,  " & _
                " Sum(H.Qty) AS Qty, " & _
                " Sum(H.Sales_Tax_Taxable_Amt) As Sales_Tax_Taxable_Amt, Sum(H.Sales_Tax) As Sales_Tax, " & _
                " Sum(H.Additional_Sales_Tax) As Additional_Sales_Tax, Sum(H.Net_Amount) As Net_Amount, " & _
                " Max(H.TINNo) As TINNo, Max(H.Unit) As Unit " & _
                " FROM View_Vat H " & _
                " LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type " & _
                " LEFT JOIN Item I ON I.Code = H.Item  " & _
                " LEFT JOIN PostingGroupSalesTaxParty S On H.SalesTaxGroupParty = S.Description " & _
                " LEFT JOIN VatCommodityCode VC ON VC.Code = I.VatCommodityCode  " & _
                " " & mCondStr & " " & _
                " Group By H.DocId, VC.Code " & _
                " Order By V_Date, InvoiceNo, VC.Code "
        DtSalesTaxForSale = AgL.FillData(mQry, AgL.GCn).Tables(0)

        Try
            Dim xlWorkSheet As Excel.Worksheet
            xlWorkSheet = xlWorkBook.Worksheets("Form24_Annexure_B")
            xlWorkSheet.Range("A2:IV65536").ClearContents()

            J = 2
            For I = 0 To DtSalesTaxForSale.Rows.Count - 1
                xlWorkSheet.Cells(J, 1) = AgL.PubCompTIN
                xlWorkSheet.Cells(J, 2) = StrFinancialYear
                xlWorkSheet.Cells(J, 3) = StrTaxPeriod
                xlWorkSheet.Cells(J, 4) = StrMonth
                xlWorkSheet.Cells(J, 5) = StrQuarter
                xlWorkSheet.Cells(J, 6) = AgL.XNull(DtSalesTaxForSale.Rows(I)("InvoiceNo"))
                xlWorkSheet.Cells(J, 7) = AgL.XNull(DtSalesTaxForSale.Rows(I)("V_Date"))
                xlWorkSheet.Cells(J, 8) = AgL.XNull(DtSalesTaxForSale.Rows(I)("CommodityCode"))
                xlWorkSheet.Cells(J, 9) = AgL.XNull(DtSalesTaxForSale.Rows(I)("Qty"))
                xlWorkSheet.Cells(J, 10) = AgL.VNull(DtSalesTaxForSale.Rows(I)("Sales_Tax_Taxable_Amt"))
                xlWorkSheet.Cells(J, 11) = AgL.VNull(DtSalesTaxForSale.Rows(I)("Sales_Tax"))
                xlWorkSheet.Cells(J, 12) = AgL.VNull(DtSalesTaxForSale.Rows(I)("Additional_Sales_Tax"))
                xlWorkSheet.Cells(J, 13) = AgL.VNull(DtSalesTaxForSale.Rows(I)("Net_Amount"))
                xlWorkSheet.Cells(J, 14) = AgL.XNull(DtSalesTaxForSale.Rows(I)("TINNo"))
                xlWorkSheet.Cells(J, 15) = AgL.XNull(DtSalesTaxForSale.Rows(I)("Unit"))
                xlWorkSheet.Cells(J, 16) = 1
                J = J + 1
            Next
            ClsMain.FReleaseObjects(xlWorkSheet)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FWriteForm24_Annexure_B1(ByVal xlWorkBook As Excel.Workbook)
        Dim I As Integer = 0
        Dim DtSalesTaxForPurchase As DataTable = Nothing
        Dim DtAddSalesTaxForPurchase As DataTable = Nothing

        Try
            Dim xlWorkSheet As Excel.Worksheet
            xlWorkSheet = xlWorkBook.Worksheets("Form24_Annexure_B1")
            xlWorkSheet.Range("A2:IV65536").ClearContents()

            'edit the cell with new value
            'For I = 2 To DtSalesTaxForPurchase.Rows.Count - 1
            '    xlWorkSheet.Cells(I, 1) = AgL.PubCompTIN
            '    xlWorkSheet.Cells(I, 2) = StrFinancialYear
            '    xlWorkSheet.Cells(I, 3) = StrTaxPeriod
            '    xlWorkSheet.Cells(I, 4) = StrMonth
            '    xlWorkSheet.Cells(I, 5) = StrQuarter

            '    xlWorkSheet.Cells(I, 6) = "V"
            'Next

            ClsMain.FReleaseObjects(xlWorkSheet)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FWriteCommAgent(ByVal xlWorkBook As Excel.Workbook)
        Dim I As Integer = 0
        Dim DtSalesTaxForPurchase As DataTable = Nothing
        Dim DtAddSalesTaxForPurchase As DataTable = Nothing

        Try
            Dim xlWorkSheet As Excel.Worksheet
            xlWorkSheet = xlWorkBook.Worksheets("Form24_CommAgent")
            xlWorkSheet.Range("A2:IV65536").ClearContents()

            'edit the cell with new value
            'For I = 2 To DtSalesTaxForPurchase.Rows.Count - 1
            '    xlWorkSheet.Cells(I, 1) = AgL.PubCompTIN
            '    xlWorkSheet.Cells(I, 2) = StrFinancialYear
            '    xlWorkSheet.Cells(I, 3) = StrTaxPeriod
            '    xlWorkSheet.Cells(I, 4) = StrMonth
            '    xlWorkSheet.Cells(I, 5) = StrQuarter

            '    xlWorkSheet.Cells(I, 6) = "V"
            'Next

            ClsMain.FReleaseObjects(xlWorkSheet)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FWriteForm24_Annexure_C(ByVal xlWorkBook As Excel.Workbook)
        Dim I As Integer = 0
        Dim J As Integer = 0
        Dim mCondStr As String = ""
        Dim DtSalesTaxForPurchase As DataTable = Nothing

        mCondStr = " Where Vt.Category = '" & ClsMain.Voucher_Category.Purchase & "'"
        mCondStr = mCondStr & " AND H.V_Date Between '" & ReportFrm.FGetText(0) & "' And '" & ReportFrm.FGetText(1) & "' "
        mCondStr = mCondStr & " AND IsNull(H.Qty,0) > 0  "
        mCondStr = mCondStr & " AND IsNull(H.Sales_Tax_Per,0) > 0  "
        mCondStr = mCondStr & " And S.Nature = '" & ClsMain.SalesTaxGroupPartyNature.Central & "'  "

        mQry = " SELECT Max(H.RecId) AS InvoiceNo,  convert(NVARCHAR,Max(H.V_Date),103) AS V_Date, Max(VC.ManualCode) AS CommodityCode,  Sum(H.Qty) AS Qty, " & _
                " Sum(H.Sales_Tax_Taxable_Amt) As Sales_Tax_Taxable_Amt, Sum(H.Sales_Tax) As Sales_Tax, Sum(H.Additional_Sales_Tax) As Additional_Sales_Tax, " & _
                " Sum(H.Net_Amount) As Net_Amount, Max(H.TINNo) As TINNo, " & _
                " Max(H.Unit) As Unit, Max(SG.DispName) AS SellerName, iSNULL(Max(SG.Add1),'') + ' ' + isnull(Max(SG.Add2),'') + '-' + Isnull(Max(C.CityName),'') AS  SellerAddress, " & _
                " Max(C.State) AS SellerState, Max(H.Form) As Form, Max(H.FormNo) As FormNo " & _
                " FROM View_Vat H " & _
                " LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type " & _
                " LEFT JOIN SUBGROUP SG ON H.SubCode = SG.SubCode " & _
                " LEFT JOIN City C ON C.CityCode = SG.CityCode " & _
                " LEFT JOIN Item I ON I.Code = H.Item  " & _
                " LEFT JOIN PostingGroupSalesTaxParty S On H.SalesTaxGroupParty = S.Description " & _
                " LEFT JOIN VatCommodityCode VC ON VC.Code = I.VatCommodityCode  " & _
                " " & mCondStr & " " & _
                " Group By H.DocId, VC.Code " & _
                " Order By V_Date, InvoiceNo, VC.Code "
        DtSalesTaxForPurchase = AgL.FillData(mQry, AgL.GCn).Tables(0)

        Try
            Dim xlWorkSheet As Excel.Worksheet
            xlWorkSheet = xlWorkBook.Worksheets("Form24_Annexure_C")
            xlWorkSheet.Range("A2:IV65536").ClearContents()

            J = 2
            For I = 0 To DtSalesTaxForPurchase.Rows.Count - 1
                xlWorkSheet.Cells(J, 1) = AgL.PubCompTIN
                xlWorkSheet.Cells(J, 2) = StrFinancialYear
                xlWorkSheet.Cells(J, 3) = StrTaxPeriod
                xlWorkSheet.Cells(J, 4) = StrMonth
                xlWorkSheet.Cells(J, 5) = StrQuarter
                xlWorkSheet.Cells(J, 6) = AgL.XNull(DtSalesTaxForPurchase.Rows(I)("TINNo"))
                xlWorkSheet.Cells(J, 7) = AgL.XNull(DtSalesTaxForPurchase.Rows(I)("SellerName"))
                xlWorkSheet.Cells(J, 8) = AgL.XNull(DtSalesTaxForPurchase.Rows(I)("SellerAddress"))
                xlWorkSheet.Cells(J, 9) = AgL.XNull(DtSalesTaxForPurchase.Rows(I)("SellerState"))
                xlWorkSheet.Cells(J, 10) = AgL.XNull(DtSalesTaxForPurchase.Rows(I)("FormNo"))
                xlWorkSheet.Cells(J, 11) = AgL.XNull(DtSalesTaxForPurchase.Rows(I)("InvoiceNo"))
                xlWorkSheet.Cells(J, 12) = AgL.XNull(DtSalesTaxForPurchase.Rows(I)("V_Date"))


                xlWorkSheet.Cells(J, 15) = AgL.XNull(DtSalesTaxForPurchase.Rows(I)("CommodityCode"))
                xlWorkSheet.Cells(J, 16) = AgL.XNull(DtSalesTaxForPurchase.Rows(I)("Qty"))
                xlWorkSheet.Cells(J, 17) = AgL.XNull(DtSalesTaxForPurchase.Rows(I)("Unit"))
                xlWorkSheet.Cells(J, 18) = AgL.VNull(DtSalesTaxForPurchase.Rows(I)("Sales_Tax_Taxable_Amt"))
                xlWorkSheet.Cells(J, 19) = AgL.VNull(DtSalesTaxForPurchase.Rows(I)("Sales_Tax"))
                xlWorkSheet.Cells(J, 20) = AgL.XNull(DtSalesTaxForPurchase.Rows(I)("Net_Amount"))
                xlWorkSheet.Cells(J, 21) = 1
                J = J + 1
            Next

            ClsMain.FReleaseObjects(xlWorkSheet)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FWriteForm24_Annexure_D(ByVal xlWorkBook As Excel.Workbook)
        Dim I As Integer = 0
        Dim DtSalesTaxForPurchase As DataTable = Nothing
        Dim DtAddSalesTaxForPurchase As DataTable = Nothing

        Try
            Dim xlWorkSheet As Excel.Worksheet
            xlWorkSheet = xlWorkBook.Worksheets("Form24_Annexure_D")
            xlWorkSheet.Range("A2:IV65536").ClearContents()

            'edit the cell with new value
            'For I = 2 To DtSalesTaxForPurchase.Rows.Count - 1
            '    xlWorkSheet.Cells(I, 1) = AgL.PubCompTIN
            '    xlWorkSheet.Cells(I, 2) = StrFinancialYear
            '    xlWorkSheet.Cells(I, 3) = StrTaxPeriod
            '    xlWorkSheet.Cells(I, 4) = StrMonth
            '    xlWorkSheet.Cells(I, 5) = StrQuarter

            '    xlWorkSheet.Cells(I, 6) = "V"
            'Next

            ClsMain.FReleaseObjects(xlWorkSheet)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FWriteForm24_Annexure_E(ByVal xlWorkBook As Excel.Workbook)
        Dim I As Integer = 0
        Dim DtSalesTaxForPurchase As DataTable = Nothing
        Dim DtAddSalesTaxForPurchase As DataTable = Nothing

        Try
            Dim xlWorkSheet As Excel.Worksheet
            xlWorkSheet = xlWorkBook.Worksheets("Form24_Annexure_E")
            xlWorkSheet.Range("A2:IV65536").ClearContents()

            'edit the cell with new value
            'For I = 2 To DtSalesTaxForPurchase.Rows.Count - 1
            '    xlWorkSheet.Cells(I, 1) = AgL.PubCompTIN
            '    xlWorkSheet.Cells(I, 2) = StrFinancialYear
            '    xlWorkSheet.Cells(I, 3) = StrTaxPeriod
            '    xlWorkSheet.Cells(I, 4) = StrMonth
            '    xlWorkSheet.Cells(I, 5) = StrQuarter

            '    xlWorkSheet.Cells(I, 6) = "V"
            'Next

            ClsMain.FReleaseObjects(xlWorkSheet)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FWriteForm24_Annexure_F(ByVal xlWorkBook As Excel.Workbook)
        Dim I As Integer = 0
        Dim DtSalesTaxForPurchase As DataTable = Nothing
        Dim DtAddSalesTaxForPurchase As DataTable = Nothing

        Try
            Dim xlWorkSheet As Excel.Worksheet
            xlWorkSheet = xlWorkBook.Worksheets("Form24_Annexure_F")
            xlWorkSheet.Range("A2:IV65536").ClearContents()

            'edit the cell with new value
            'For I = 2 To DtSalesTaxForPurchase.Rows.Count - 1
            '    xlWorkSheet.Cells(I, 1) = AgL.PubCompTIN
            '    xlWorkSheet.Cells(I, 2) = StrFinancialYear
            '    xlWorkSheet.Cells(I, 3) = StrTaxPeriod   
            '    xlWorkSheet.Cells(I, 4) = StrMonth
            '    xlWorkSheet.Cells(I, 5) = StrQuarter

            '    xlWorkSheet.Cells(I, 6) = "V"
            'Next

            ClsMain.FReleaseObjects(xlWorkSheet)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

#Region "Weaving Order Ratio"
    Private Sub ProcWeavingOrderRatio()
        Try
            RepName = "Trade_WeavingOrderRatio" : RepTitle = "Weaving Order Ratio"
            Dim bTempTable$ = ""
            Dim bTempItem$ = ""

            bTempItem = AgL.GetGUID(AgL.GCn).ToString
            bTempTable = AgL.GetGUID(AgL.GCn).ToString
            mQry = "CREATE TABLE [#" & bTempTable & "] " & _
                    " (Party NVARCHAR(10), ClothQty Float, ClothWeight Float, " & _
                    " WeavingOrderQty Float, WeavingOrderMeasure Float )  "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

            Dim mSaleCondStr$ = ""
            Dim mWeavingCondStr$ = ""


            mSaleCondStr = mSaleCondStr & " AND H.V_Date Between '" & ReportFrm.FGetText(0) & "' And '" & ReportFrm.FGetText(1) & "' "
            mSaleCondStr = mSaleCondStr & ReportFrm.GetWhereCondition("H.SaleToParty", 2)

            Dim mSaleQry$ = "SELECT H.SaleToParty AS Party, sum(L.Qty) AS Qty, sum(L.TotalMeasure) AS AS ClothWeaight " & _
                            " FROM SaleInvoice H " & _
                            " LEFT JOIN SaleInvoiceDetail L ON L.DocId = H.DocID  " & _
                            " WHERE 1=1 " & mSaleCondStr & _
                            " AND H.Site_Code = " & AgL.Chk_Text(AgL.PubSiteCode) & " AND H.Div_Code = " & AgL.Chk_Text(AgL.PubDivCode) & " " & _
                            " GROUP BY H.SaleToParty "

            mWeavingCondStr = " AND H.V_Date Between '" & ReportFrm.FGetText(0) & "' And '" & ReportFrm.FGetText(1) & "' "
            mWeavingCondStr = mWeavingCondStr & ReportFrm.GetWhereCondition("H.JobWorker", 2)

            'For Inserting Carpet Consumption
            mQry = "INSERT INTO [#" & bTempTable & "](Party, ClothQty, ClothWeight ) " & _
                      mSaleQry
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

            Dim mWeavingOrderQry$ = "SELECT H.JobWorker, sum(L.Qty) AS Qty, sum(L.TotalMeasure) AS TotalMeasure, max(L.MeasureUnit) AS OrdMeasure " & _
                    " FROM JobOrderDetail L WITH (Nolock) " & _
                    " LEFT JOIN JobOrder H WITH (Nolock) ON H.DocID = L.JobOrder  " & _
                    " LEFT JOIN Voucher_Type Vt WITH (Nolock) ON Vt.V_Type = H.V_Type  " & _
                    " WHERE Vt.NCat IN ('WVORD', 'WVCNL') " & _
                    " AND H.Site_Code = " & AgL.Chk_Text(AgL.PubSiteCode) & " AND H.Div_Code = " & AgL.Chk_Text(AgL.PubDivCode) & " " & _
                    " GROUP BY H.JobWorker "




            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records to Print!")

            ReportFrm.PrintReport(DsRep, RepName, RepTitle)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
#End Region

End Class
