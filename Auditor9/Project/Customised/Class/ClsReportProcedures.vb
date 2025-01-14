Imports System.Data.SqlClient
Imports System.Data.SQLite
Imports System.IO
Imports AgLibrary.ClsMain
Imports AgLibrary.ClsMain.agConstants

Public Class ClsReportProcedures

#Region "Danger Zone"
    Dim StrArr1() As String = Nothing, StrArr2() As String = Nothing, StrArr3() As String = Nothing, StrArr4() As String = Nothing, StrArr5() As String = Nothing

    Dim mGRepFormName As String = ""
    Dim ErrorLog As String = ""

    'Dim WithEvents ReportFrm As Aglibrary.FrmReportLayout
    Dim WithEvents ReportFrm As AgLibrary.FrmReportLayout

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
    Private Const SaleCertificates As String = "SaleCertificates"
    Private Const SaleOrderRegister As String = "SaleOrderRegister"
    Private Const SaleChallanReport As String = "SaleChallanReport"
    Private Const SaleInvoiceReport As String = "SaleInvoiceReport"
    Private Const SizeWiseSaleInvoiceReport As String = "SizeWiseSaleInvoiceReport"
    Private Const SaleAndCollectionSummary As String = "SaleCollectionSummary"
    Private Const PendingToDeliverReport As String = "PendingToDeliverReport"
    Private Const SizeWiseStockReport As String = "SizeWiseStockReport"
    Private Const SizeWiseRateList As String = "SizeWiseRateList"
    Private Const SizeWiseConsumptionList As String = "SizeWiseConsumptionList"
    Private Const SizeWiseJobOrderReport As String = "SizeWiseJobOrderReport"
    Private Const SizeWiseJobReceiveReport As String = "SizeWiseJobReceiveReport"
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
    Private Const CurrentStockReport As String = "CurrentStockReport"
    Private Const StockValuationReportForBank As String = "StockValuationReportForBank"
    Private Const GSTReports As String = "GSTReports"
    'Private Const ConcurLedger As String = "ChuktiLedger"
    Private Const AadhatLedger As String = "AadhatLedger"
    Private Const AadhatLedgerDebtors As String = "AadhatLedgerDebtors"
    Private Const AadhatLedgerCreditors As String = "AadhatLedgerCreditors"
    Private Const PartyLabelPrint As String = "PartyLabelPrint"
    Private Const BrandList As String = "BrandList"
    Private Const ExportDataToSqlite As String = "ExportDataToSqlite"
    Private Const ImportDataFromSqlite As String = "ImportDataFromSqlite"
    Private Const RestoreDatabase As String = "RestoreDatabase"
    Private Const DeleteData As String = "DeleteData"
    Private Const ExportStockIssueData As String = "ExportStockIssueData"
    Private Const ImportStockReceiveData As String = "ImportStockReceiveData"
#End Region

#Region "Queries Definition"
    Dim mHelpAcGroupQry$ = "Select 'o' As Tick, GroupCode as Code, GroupName as Name From AcGroup Order By GroupName "
    Dim mHelpAreaQry$ = "Select 'o' As Tick, Code, Description From Area "
    Dim mHelpCityQry$ = "Select 'o' As Tick, CityCode, CityName From City "
    Dim mHelpStateQry$ = "Select 'o' As Tick, State_Code, State_Desc From State "
    Dim mHelpUserQry$ = "Select 'o' As Tick, User_Name As Code, User_Name As [User] From UserMast "
    Dim mHelpSiteQry$ = "Select 'o' As Tick, Code, Name As [Site] From SiteMast Where  Code In (" & AgL.PubSiteList & ")  "
    'Dim mHelpDivisionQry$ = "Select 'o' As Tick, Div_Code as Code, Div_Name As [Division] From Division Where Div_Code In (" & AgL.PubDivisionList & ") "
    Dim mHelpDivisionQry$ = "Select 'o' As Tick, Div_Code as Code, Div_Name As [Division] From Division "
    Dim mHelpItemQry$ = "Select 'o' As Tick, Code, Description As [Item] From Item "
    Dim mHelpItemTypeQry$ = "Select 'o' As Tick, Code, Name From ItemType "
    Dim mHelpItemGroupQry$ = "Select 'o' As Tick, Code, Description As [Item Group] From ItemGroup "
    Dim mHelpVendorQry$ = " Select 'o' As Tick,  H.SubCode As Code, Sg.DispName AS Vendor FROM Vendor H LEFT JOIN SubGroup Sg ON H.SubCode = Sg.SubCode "
    Dim mHelpTableQry$ = "Select 'o' As Tick, H.Code, H.Description AS [Table] FROM HT_Table H "
    Dim mHelpPaymentModeQry$ = "Select 'o' As Tick, 'Cash' As Code, 'Cash' As Description " &
                                " UNION ALL " &
                                " Select 'o' As Tick, 'Credit' As Code, 'Credit' As Description "
    Dim mHelpOutletQry$ = "Select 'o' As Tick, H.Code, H.Description AS [Table] FROM Outlet H "
    Dim mHelpStewardQry$ = "Select 'o' As Tick,  Sg.SubCode AS Code, Sg.DispName AS Steward FROM SubGroup Sg  "
    Dim mHelpPartyQry$ = " Select 'o' As Tick,  Sg.Code, Sg.Name AS Party, Sg.Address, Ag.GroupName FROM viewHelpSubGroup Sg  Left Join AcGroup Ag On Sg.groupCode= ag.GroupCode Where Sg.Nature In ('Customer','Supplier','Cash') Order By Name "
    Dim mHelpSubgroupQry$ = " Select 'o' As Tick,  Sg.Code, Sg.Name AS Party, Sg.Address, Ag.GroupName FROM viewHelpSubGroup Sg  Left Join AcGroup Ag On Sg.groupCode= ag.GroupCode Order By Name "
    Dim mHelpPartySingleQry$ = " Select Sg.Code, Sg.Name AS Party FROM viewHelpSubGroup Sg Where Sg.Nature In ('Customer','Supplier','Cash') "
    Dim mHelpAgentQry$ = " Select 'o' As Tick,  Sg.Code, Sg.Name AS Agent FROM viewHelpSubgroup Sg Where Sg.SubgroupType = '" & SubgroupType.SalesAgent & "' "
    Dim mHelpYesNo$ = " Select 'Yes' As Code, 'Yes' AS [Value] Union All Select 'No' As Code, 'No' AS [Value] "
    Dim mHelpSaleOrderQry$ = " Select 'o' As Tick,  H.DocID AS Code, H.V_Type || '-' || H.ManualRefNo  FROM SaleOrder H "
    Dim mHelpSaleBillQry$ = " SELECT 'o' As Tick,DocId, ReferenceNo AS BillNo, V_Date AS Date FROM SaleChallan "
    Dim mHelpItemReportingGroupQry$ = "Select 'o' As Tick,I.Code,I.Description  AS ItemReportingGroup FROM ItemReportingGroup I "
    Dim mHelpSalesRepresentativeQry$ = " Select 'o' As Tick, Sg.Code, Sg.Name AS [Sales Representative] FROM viewHelpSubgroup Sg Left Join HRM_Employee E On Sg.Code = E.Subcode LEFT JOIN HRM_Designation D ON E.designation = D.Code Where Sg.SubgroupType = '" & SubgroupType.Employee & "' AND D.Code ='SREP' "
    Dim mHelpResponsiblePersonQry$ = " Select 'o' As Tick, Sg.Code, Sg.Name AS Agent FROM viewHelpSubgroup Sg Left Join HRM_Employee E On Sg.Code = E.Subcode LEFT JOIN HRM_Designation D ON E.designation = D.Code Where Sg.SubgroupType = '" & SubgroupType.Employee & "' AND D.Code <>'SREP' "
    Dim mHelpSalesAgentQry$ = " Select 'o' As Tick, Sg.Code, Sg.Name AS [Responsible Person] FROM viewHelpSubgroup Sg Where Sg.SubgroupType = '" & SubgroupType.SalesAgent & "' "
    Dim mHelpItemCategoryQry$ = "Select 'o' As Tick, Code, Description As [Item Category] From ItemCategory "
    Dim mHelpDimension1Qry$ = "Select 'o' As Tick, Code, Specification As Name From Item Where V_Type = '" & ItemV_Type.Dimension1 & "' Order By Specification "
    Dim mHelpDimension2Qry$ = "Select 'o' As Tick, Code, Specification As Name From Item Where V_Type = '" & ItemV_Type.Dimension2 & "' Order By Specification "
    Dim mHelpDimension3Qry$ = "Select 'o' As Tick, Code, Specification As Name From Item Where V_Type = '" & ItemV_Type.Dimension3 & "' Order By Specification "
    Dim mHelpDimension4Qry$ = "Select 'o' As Tick, Code, Specification As Name From Item Where V_Type = '" & ItemV_Type.Dimension4 & "' Order By Specification "
    Dim mHelpSingleDimension1Qry$ = "Select Code, Specification As Name From Item Where V_Type = '" & ItemV_Type.Dimension1 & "' Order By Specification "
    Dim mHelpSingleDimension2Qry$ = "Select Code, Specification As Name From Item Where V_Type = '" & ItemV_Type.Dimension2 & "' Order By Specification "
    Dim mHelpSingleDimension3Qry$ = "Select Code, Specification As Name From Item Where V_Type = '" & ItemV_Type.Dimension3 & "' Order By Specification "
    Dim mHelpSingleDimension4Qry$ = "Select Code, Specification As Name From Item Where V_Type = '" & ItemV_Type.Dimension4 & "' Order By Specification "
    Dim mHelpSingleProcessQry$ = "Select Subcode as Code, Name From Subgroup Where SubgroupType = '" & SubgroupType.Process & "' Order By Name "
    Dim mHelpSingleJobProcessQry$ = "Select Subcode as Code, Name From Subgroup Where SubgroupType = '" & SubgroupType.Process & "' And Subcode Not In ('" & Process.Sales & "', '" & Process.Purchase & "', '" & Process.Stock & "')  Order By Name "
    Dim mHelpSizeQry$ = "Select 'o' As Tick, Code, Description As Name From Item Where V_Type = '" & ItemV_Type.SIZE & "' Order By Specification "
    Dim mHelpTagQry$ = "Select 'o' As Tick, H.Code, H.Description   FROM Tag H "
    Dim mHelpCatalogQry$ = "Select 'o' As Tick, H.Code, H.Description AS [Name] FROM Catalog H  Order by H.Description"
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
            Dim mQry As String
            Dim I As Integer = 0
            Select Case GRepFormName
                Case SaleInvoiceReport, SaleChallanReport
                    mQry = "Select 'Invoice Wise Detail' as Code, 'Invoice Wise Detail' as Name 
                            Union All Select 'Item Wise Detail' as Code, 'Item Wise Detail' as Name 
                            Union All Select 'Month Wise Summary' as Code, 'Month Wise Summary' as Name 
                            Union All Select 'Party Wise Summary' as Code, 'Party Wise Summary' as Name 
                            Union All Select 'Agent Wise Summary' as Code, 'Agent Wise Summary' as Name 
                            Union All Select 'Item Wise Summary' as Code, 'Item Wise Summary' as Name 
                            Union All Select 'Item Group Wise Summary' as Code, 'Item Group Wise Summary' as Name 
                            Union All Select 'Item Category Wise Summary' as Code, 'Item Category Wise Summary' as Name 
                            Union All Select 'City Wise Summary' as Code, 'City Wise Summary' as Name 
                            Union All Select 'State Wise Summary' as Code, 'State Wise Summary' as Name                             
                            "
                    ReportFrm.CreateHelpGrid("Report Type", "Report Type", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.SingleSelection, mQry, "Invoice Wise Detail")
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.PubEndDate)
                    ReportFrm.CreateHelpGrid("Party", "Party", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpPartyQry)
                    ReportFrm.CreateHelpGrid("Item", "Item", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemQry)
                    ReportFrm.CreateHelpGrid("Site", "Site", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpSiteQry)
                    ReportFrm.CreateHelpGrid("VoucherType", "Voucher Type", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, FGetVoucher_TypeQry("SaleInvoice"))
                    ReportFrm.CreateHelpGrid("CashCredit", "Cash/Credit", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.SingleSelection, "Select 'Cash' as Code, 'Cash' as Name Union All Select 'Credit' as Code, 'Credit' as Name Union All Select 'Both' as Code, 'Both' as Name", "Both")
                    ReportFrm.CreateHelpGrid("Agent", "Agent", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.SingleSelection, mHelpAgentQry)

                Case SaleCertificates
                    mQry = "Select 'Form 21' as Code, 'Form 21' as Name                          
                            "
                    ReportFrm.CreateHelpGrid("Report Type", "Report Type", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.SingleSelection, mQry, "Form 21")
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.PubEndDate)
                    ReportFrm.CreateHelpGrid("Site", "Site", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpSiteQry)
                    ReportFrm.CreateHelpGrid("VoucherType", "Voucher Type", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, FGetVoucher_TypeQry("SaleInvoice"))
                    ReportFrm.CreateHelpGrid("Party", "Party", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpPartyQry)
                    ReportFrm.CreateHelpGrid("SaleInvoice", "Invoice No", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, FGetSaleInvoiceQry("SI"))





                Case SizeWiseSaleInvoiceReport
                    mQry = "Select 'Summary' as Code, 'Summary' as Name 
                            Union All Select 'Summary With Design' as Code,  'Summary With Design' as Name  "
                    ReportFrm.CreateHelpGrid("Report Type", "Report Type", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.SingleSelection, mQry, "Summary")
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.PubEndDate)
                    ReportFrm.CreateHelpGrid("Party", "Party", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpPartyQry)
                    ReportFrm.CreateHelpGrid(AgL.PubCaptionItemType, AgL.PubCaptionItemType, AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemTypeQry)
                    ReportFrm.CreateHelpGrid("Site", "Site", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpSiteQry)
                    ReportFrm.CreateHelpGrid("VoucherType", "Voucher Type", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, FGetVoucher_TypeQry("SaleInvoice"))
                    ReportFrm.CreateHelpGrid(AgL.PubCaptionItemCategory, AgL.PubCaptionItemCategory, AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemCategoryQry)
                    ReportFrm.CreateHelpGrid(AgL.PubCaptionDimension1, AgL.PubCaptionDimension1, AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpDimension1Qry)
                    ReportFrm.CreateHelpGrid(AgL.PubCaptionDimension2, AgL.PubCaptionDimension2, AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpDimension2Qry)
                    ReportFrm.CreateHelpGrid(AgL.PubCaptionDimension3, AgL.PubCaptionDimension3, AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpDimension3Qry)
                    ReportFrm.CreateHelpGrid(AgL.PubCaptionDimension4, AgL.PubCaptionDimension4, AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpDimension4Qry)
                    ReportFrm.CreateHelpGrid("Size", "Size", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpSizeQry)

                Case SaleAndCollectionSummary
                    mQry = "Select 'Date Wise Summary' as Code, 'Date Wise Summary' as Name 
                           Union All Select 'Document Wise Detail' as Code, 'Document Wise Detail' as Name  "
                    ReportFrm.CreateHelpGrid("Report Type", "Report Type", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.SingleSelection, mQry, "Date Wise Summary")
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.PubEndDate)
                    ReportFrm.CreateHelpGrid("Party", "Party", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpPartyQry)
                    ReportFrm.CreateHelpGrid("VoucherType", "Voucher Type", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, FGetVoucher_TypeQry("SaleInvoice"))
                    ReportFrm.CreateHelpGrid("Division", "Division", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpDivisionQry, "[DIVISIONCODE]")
                    ReportFrm.CreateHelpGrid("Site", "Site", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpSiteQry, "[SITECODE]")

                Case PendingToDeliverReport
                    mQry = "Select 'Detail' as Code, 'Detail' as Name  "
                    ReportFrm.CreateHelpGrid("Report Type", "Report Type", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.SingleSelection, mQry, "Detail")
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.PubEndDate)
                    ReportFrm.CreateHelpGrid("Catalog", "Catalog", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpCatalogQry)
                    ReportFrm.CreateHelpGrid("ItemCategory", "Item Category", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemCategoryQry)
                    ReportFrm.CreateHelpGrid("Division", "Division", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpDivisionQry, "[DIVISIONCODE]")
                    ReportFrm.CreateHelpGrid("Site", "Site", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpSiteQry, "")


                Case SizeWiseJobReceiveReport, SizeWiseJobOrderReport
                    mQry = "Select 'Summary' as Code, 'Summary' as Name 
                            Union All Select 'Summary With Design' as Code,  'Summary With Design' as Name  "
                    ReportFrm.CreateHelpGrid("Report Type", "Report Type", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.SingleSelection, mQry, "Summary")
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.PubEndDate)
                    ReportFrm.CreateHelpGrid("Process", "Process", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.SingleSelection, mHelpSingleJobProcessQry, "")
                    ReportFrm.CreateHelpGrid("Party", "Party", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpPartyQry)
                    ReportFrm.CreateHelpGrid(AgL.PubCaptionItemType, AgL.PubCaptionItemType, AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemTypeQry)
                    ReportFrm.CreateHelpGrid("Item Category", "Item Category", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemCategoryQry)
                    ReportFrm.CreateHelpGrid(AgL.PubCaptionDimension1, AgL.PubCaptionDimension1, AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpDimension1Qry)
                    ReportFrm.CreateHelpGrid(AgL.PubCaptionDimension2, AgL.PubCaptionDimension2, AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpDimension2Qry)
                    ReportFrm.CreateHelpGrid(AgL.PubCaptionDimension3, AgL.PubCaptionDimension3, AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpDimension3Qry)
                    ReportFrm.CreateHelpGrid(AgL.PubCaptionDimension4, AgL.PubCaptionDimension4, AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpDimension4Qry)
                    ReportFrm.CreateHelpGrid("Size", "Size", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpSizeQry)
                    ReportFrm.CreateHelpGrid("Site", "Site", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpSiteQry)


                Case SizeWiseStockReport
                    mQry = "Select 'Stock In Hand' as Code, 'Stock In Hand' as Name 
                            Union All 
                            Select 'Stock In Process' as Code, 'Stock In Process' as Name
                            Union All 
                            Select 'Stock In Hand & Process' as Code, 'Stock In Hand & Process' as Name
                           "
                    ReportFrm.CreateHelpGrid("Report Type", "Report Type", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.SingleSelection, mQry, "Stock In Hand")
                    mQry = "Select 'Category & Colour' as Code, 'Category & Colour' as Name 
                            Union All 
                            Select 'Category, Colour & Process' as Code, 'Category, Colour & Process' as Name
                            Union All 
                            Select 'Category, Colour & Location' as Code, 'Category, Colour & Location' as Name
                           "
                    ReportFrm.CreateHelpGrid("Group On", "Group On", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.SingleSelection, mQry, "Category & Colour")
                    ReportFrm.CreateHelpGrid("As On Date", "As On Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.PubLoginDate)
                    ReportFrm.CreateHelpGrid(AgL.PubCaptionItemType, AgL.PubCaptionItemType, AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemTypeQry)
                    ReportFrm.CreateHelpGrid("Item Category", "Item Category", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemCategoryQry)
                    ReportFrm.CreateHelpGrid(AgL.PubCaptionDimension1, AgL.PubCaptionDimension1, AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpDimension1Qry)
                    ReportFrm.CreateHelpGrid(AgL.PubCaptionDimension2, AgL.PubCaptionDimension2, AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpDimension2Qry)
                    ReportFrm.CreateHelpGrid(AgL.PubCaptionDimension3, AgL.PubCaptionDimension3, AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpDimension3Qry)
                    ReportFrm.CreateHelpGrid(AgL.PubCaptionDimension4, AgL.PubCaptionDimension4, AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpDimension4Qry)
                    ReportFrm.CreateHelpGrid("Size", "Size", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpSizeQry)
                    ReportFrm.CreateHelpGrid("Site", "Site", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpSiteQry)


                Case SizeWiseRateList
                    mQry = "Select 'Summary' as Code, 'Summary' as Name "
                    ReportFrm.CreateHelpGrid("Report Type", "Report Type", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.SingleSelection, mQry, "Summary")
                    ReportFrm.CreateHelpGrid("Process", "Process", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.SingleSelection, mHelpSingleProcessQry, "SALE|PSales")
                    ReportFrm.CreateHelpGrid(AgL.PubCaptionItemType, AgL.PubCaptionItemType, AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemTypeQry)
                    ReportFrm.CreateHelpGrid("Item Category", "Item Category", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemCategoryQry)
                    ReportFrm.CreateHelpGrid(AgL.PubCaptionDimension1, AgL.PubCaptionDimension1, AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpDimension1Qry)
                    ReportFrm.CreateHelpGrid(AgL.PubCaptionDimension2, AgL.PubCaptionDimension2, AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpDimension2Qry)
                    ReportFrm.CreateHelpGrid(AgL.PubCaptionDimension3, AgL.PubCaptionDimension3, AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.SingleSelection, mHelpDimension3Qry)
                    ReportFrm.CreateHelpGrid(AgL.PubCaptionDimension4, AgL.PubCaptionDimension4, AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpDimension4Qry)
                    ReportFrm.CreateHelpGrid("Size", "Size", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpSizeQry)
                    ReportFrm.CreateHelpGrid("Site", "Site", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpSiteQry)


                Case SizeWiseConsumptionList
                    mQry = "Select 'Summary' as Code, 'Summary' as Name "
                    ReportFrm.CreateHelpGrid("Report Type", "Report Type", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.SingleSelection, mQry, "Summary")
                    ReportFrm.CreateHelpGrid(AgL.PubCaptionItemType, AgL.PubCaptionItemType, AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemTypeQry)
                    ReportFrm.CreateHelpGrid("Item Category", "Item Category", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemCategoryQry)
                    ReportFrm.CreateHelpGrid(AgL.PubCaptionDimension3, AgL.PubCaptionDimension3, AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.SingleSelection, mHelpDimension3Qry)
                    ReportFrm.CreateHelpGrid(AgL.PubCaptionDimension4, AgL.PubCaptionDimension4, AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpDimension4Qry)
                    ReportFrm.CreateHelpGrid("Size", "Size", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpSizeQry)



                Case SaleOrderRegister
                    mQry = "Select 'Customer / Item Wise Detail' as Code, 'Customer / Item Wise Detail' as Name "
                    mQry += "Union All Select 'Item / Customer Wise Detail' as Code, 'Item / Customer Wise Detail' as Name "

                    ReportFrm.CreateHelpGrid("Report Type", "Report Type", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.SingleSelection, mQry, "Customer / Item Wise Detail",,, 300)
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.PubEndDate)
                    ReportFrm.CreateHelpGrid("Party", "Party", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpPartyQry)
                    ReportFrm.CreateHelpGrid("Item", "Item", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemQry)
                    ReportFrm.CreateHelpGrid("Site", "Site", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpSiteQry)
                    If GRepFormName = SaleOrderRegister Then
                        ReportFrm.CreateHelpGrid("VoucherType", "Voucher Type", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, FGetVoucher_TypeQry("SaleInvoice"))
                    Else
                        ReportFrm.CreateHelpGrid("VoucherType", "Voucher Type", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, FGetVoucher_TypeQry("SaleInvoice"))
                    End If
                    ReportFrm.CreateHelpGrid("CashCredit", "Cash/Credit", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.SingleSelection, "Select 'Cash' as Code, 'Cash' as Name Union All Select 'Credit' as Code, 'Credit' as Name Union All Select 'Both' as Code, 'Both' as Name", "Both")
                    ReportFrm.CreateHelpGrid("Agent", "Agent", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpSalesAgentQry)
                    ReportFrm.CreateHelpGrid("Item Group", "Item Group", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemGroupQry)
                    ReportFrm.CreateHelpGrid("Item Category", "Item Category", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemCategoryQry)
                    ReportFrm.CreateHelpGrid("City", "City", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpCityQry)
                    ReportFrm.CreateHelpGrid("State", "State", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpStateQry)
                    ReportFrm.CreateHelpGrid("SalesRepresentative", "Sales Representative", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpSalesRepresentativeQry)
                    ReportFrm.CreateHelpGrid("ResponsiblePerson", "ResponsiblePerson", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpResponsiblePersonQry)
                    ReportFrm.CreateHelpGrid("Tag", "Tag", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpTagQry)


                Case PurchaseInvoiceReport, PurchaseChallanReport
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.PubEndDate)
                    ReportFrm.CreateHelpGrid("Report Type", "Report Type", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.SingleSelection, "Select 'Summary' as Code, 'Summary' as Name Union All Select 'Month Wise Summary' as Code, 'Party Wise Summary' as Name Union All Select 'Party Wise Summary' as Code, 'Month Wise Summary' as Name Union All Select 'Item Wise Detail' as Code, 'Item Wise Detail' as Name", "Summary", , , , , False)
                    ReportFrm.CreateHelpGrid("Item", "Item", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemQry)
                    ReportFrm.CreateHelpGrid("Party", "Party", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpPartyQry)
                    ReportFrm.CreateHelpGrid("Site", "Site", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpSiteQry)
                    ReportFrm.CreateHelpGrid("VoucherType", "Voucher Type", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, FGetVoucher_TypeQry("PurchInvoice"))

                Case PurchaseAdviseReport
                    ReportFrm.CreateHelpGrid("AsOnDate", "As On Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.PubLoginDate)
                    ReportFrm.CreateHelpGrid("Item", "Item", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemQry)
                    ReportFrm.CreateHelpGrid("ItemGroup", "Item Group", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemGroupQry)
                    ReportFrm.CreateHelpGrid("Item ActiveInActive", "Item Active/InActive", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.SingleSelection, "Select 'Active' As Code, 'Active' as Name Union All Select 'InActive' as Code, 'InActive' as Name Union All Select 'Both' as Code, 'Both' as Name", "Active")

                Case ItemExpiryReport
                    ReportFrm.CreateHelpGrid("AsOnDate", "Before Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.PubLoginDate)
                    ReportFrm.CreateHelpGrid("Item", "Item", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemQry)
                    ReportFrm.CreateHelpGrid("ItemGroup", "Item Group", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemGroupQry)
                    ReportFrm.CreateHelpGrid("Item ActiveInActive", "Item Active/InActive", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.SingleSelection, "Select 'Active' As Code, 'Active' as Name Union All Select 'InActive' as Code, 'InActive' as Name Union All Select 'Both' as Code, 'Both' as Name", "Active")

                Case PurchaseIndentReport
                    ReportFrm.CreateHelpGrid("FromDate", "Order From Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "Order Upto Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.PubEndDate)
                    ReportFrm.CreateHelpGrid("Item", "Item", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemQry)

                Case CurrentStockReport
                    ReportFrm.CreateHelpGrid("AsOnDate", "As On Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("Item", "Item", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemQry)
                    ReportFrm.CreateHelpGrid("Item Reporting Group", "Item Reporting Group", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemReportingGroupQry)

                Case VATReports
                    mQry = "Select 'Annexure A' AS Code, 'Annexure A' AS Name "
                    mQry += "Union All Select 'Annexure A1' as Code, 'Annexure A1' AS Name "
                    mQry += "Union All Select 'Annexure B' as Code, 'Annexure B' as Name "
                    mQry += "Union All Select 'Annexure B1' as Code, 'Annexure B1' as Name "
                    mQry += "Union All Select 'Annexure C' as Code, 'Annexure C' as Name "
                    mQry += "Union All Select 'Return Of Tax' as Code, 'Return Of Tax' as Name "
                    mQry += "Union All Select 'Return Form 24' as Code, 'Return Form 24' as Name "
                    ReportFrm.CreateHelpGrid("FromDate", "Order From Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "Order Upto Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.PubEndDate)
                    ReportFrm.CreateHelpGrid("Report Type", "Report Type", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.SingleSelection, mQry, "", , , , , False)

                Case PartyOutstandingReport
                    ReportFrm.CreateHelpGrid("As On Date", "As On Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.PubLoginDate)
                    ReportFrm.CreateHelpGrid("Party", "Party", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpPartyQry)

                Case BillWiseProfitability
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.PubEndDate)
                    ReportFrm.CreateHelpGrid("Party", "Party", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpPartyQry)
                    ReportFrm.CreateHelpGrid("Item", "Item", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemQry)
                    ReportFrm.CreateHelpGrid("Bill No", "Bill No", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpSaleBillQry)
                    ReportFrm.CreateHelpGrid("Item Reporting Group", "Item Reporting Group", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemReportingGroupQry)

                Case DebtorsOutstandingOverDue
                    ReportFrm.CreateHelpGrid("As On Date", "As On Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.PubLoginDate)
                    ReportFrm.CreateHelpGrid("NoofDays", "No of Days", AgLibrary.FrmReportLayout.FieldFilterDataType.NumericType, AgLibrary.FrmReportLayout.FieldDataType.NumericType, "", "")
                    ReportFrm.CreateHelpGrid("Party", "Party", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpPartyQry)

                'Case ConcurLedger
                '    mQry = "Select 'Format 1' as Code, 'Format 1' as Name 
                '            Union All Select 'Format 2' as Code, 'Format 2' as Name 
                '            Union All Select 'Without Interest Portrait' as Code, 'Without Interest Portrait' as Name "
                '    ReportFrm.CreateHelpGrid("Report Format", "Report Format", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.SingleSelection, mQry, "Format 1")
                '    ReportFrm.CreateHelpGrid("As On Date", "As On Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.PubLoginDate)
                '    ReportFrm.CreateHelpGrid("Grace Days", "Grace Days", AgLibrary.FrmReportLayout.FieldFilterDataType.NumericType, AgLibrary.FrmReportLayout.FieldDataType.NumericType, "", AgL.VNull(AgL.PubDtEnviro.Rows(0)("Default_DebtorsCreditDays")))
                '    ReportFrm.CreateHelpGrid("Party", "Party", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpSubgroupQry, , 450, 825, 300)
                '    mQry = "Select 'After Chukti' as Code, 'After Chukti' as Name 
                '            Union All Select 'Financial Year' as Code, 'Financial Year' as Name 
                '            Union All Select 'Financial Year Opening' as Code, 'Financial Year Opening' as Name 
                '            Union All Select 'Complete' as Code, 'Complete' as Name"
                '    ReportFrm.CreateHelpGrid("Records Type", "Records Type", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.SingleSelection, mQry, "After Chukti")
                '    ReportFrm.CreateHelpGrid("Agent", "Agent", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpAgentQry)
                '    ReportFrm.CreateHelpGrid("City", "City", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpCityQry)
                '    ReportFrm.CreateHelpGrid("Area", "Area", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpAreaQry)
                '    ReportFrm.CreateHelpGrid("Division", "Division", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpDivisionQry, "[DIVISIONCODE]")
                '    ReportFrm.CreateHelpGrid("Site", "Site", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpSiteQry, "[SITECODE]")
                '    ReportFrm.CreateHelpGrid("Interest Rate", "Interest Rate", AgLibrary.FrmReportLayout.FieldFilterDataType.NumericType, AgLibrary.FrmReportLayout.FieldDataType.NumericType, "", AgL.VNull(AgL.PubDtEnviro.Rows(0)("Default_DebtorsInterestRate")))
                '    ReportFrm.CreateHelpGrid("Account Group", "Account Group", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpAcGroupQry)

                Case AadhatLedger
                    ReportFrm.CreateHelpGrid("From Date", "From Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("Upto Date", "Upto Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.PubLoginDate)
                    Dim mHelpMasterPartyQry$ = " Select 'o' As Tick,  Sg.Code, Sg.Name AS Party, Sg.Address, Ag.GroupName FROM viewHelpSubGroup Sg  Left Join AcGroup Ag On Sg.groupCode= ag.GroupCode Where Sg.SubgroupType In ('Master Customer','Master Supplier') "
                    ReportFrm.CreateHelpGrid("Master Party", "Master Party", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpMasterPartyQry, , 450, 825, 300)
                    Dim mHelpSubPartyQry$ = " Select 'o' As Tick,  Sg.Code, Sg.Name AS Party, Sg.Address, Ag.GroupName FROM viewHelpSubGroup Sg  Left Join AcGroup Ag On Sg.groupCode= ag.GroupCode Where Sg.SubgroupType In ('Customer','Supplier') "
                    ReportFrm.CreateHelpGrid("Sub Party", "Sub Party", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpSubPartyQry, , 450, 825, 300)
                    ReportFrm.CreateHelpGrid("Agent", "Agent", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpAgentQry)
                    ReportFrm.CreateHelpGrid("Division", "Division", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpDivisionQry, "[DIVISIONCODE]")
                    ReportFrm.CreateHelpGrid("Site", "Site", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpSiteQry, "[SITECODE]")
                    mQry = "Select 'After Settlement' as Code, 'After Settlement' as Name 
                            Union All Select 'Complete' as Code, 'Complete' as Name"
                    ReportFrm.CreateHelpGrid("Records Type", "Records Type", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.SingleSelection, mQry, "After Settlement")


                Case AadhatLedgerDebtors
                    ReportFrm.CreateHelpGrid("From Date", "From Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("Upto Date", "Upto Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.PubLoginDate)
                    Dim mHelpMasterPartyQry$ = " Select 'o' As Tick,  Sg.Code, Sg.Name AS Party, Sg.Address, Ag.GroupName FROM viewHelpSubGroup Sg  Left Join AcGroup Ag On Sg.groupCode= ag.GroupCode Where Sg.SubgroupType In ('Master Customer','Master Supplier') "
                    ReportFrm.CreateHelpGrid("Master Party", "Master Party", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpMasterPartyQry, , 450, 825, 300)
                    Dim mHelpSubPartyQry$ = " Select 'o' As Tick,  Sg.Code, Sg.Name AS Party, Sg.Address, Ag.GroupName FROM viewHelpSubGroup Sg  Left Join AcGroup Ag On Sg.groupCode= ag.GroupCode Where Sg.SubgroupType In ('Customer','Supplier') "
                    ReportFrm.CreateHelpGrid("Sub Party", "Sub Party", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpSubPartyQry, , 450, 825, 300)
                    ReportFrm.CreateHelpGrid("Agent", "Agent", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpAgentQry)
                    ReportFrm.CreateHelpGrid("Division", "Division", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpDivisionQry, "[DIVISIONCODE]")
                    ReportFrm.CreateHelpGrid("Site", "Site", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpSiteQry, "[SITECODE]")
                    mQry = "Select 'After Settlement' as Code, 'After Settlement' as Name 
                            Union All Select 'Complete' as Code, 'Complete' as Name"
                    ReportFrm.CreateHelpGrid("Records Type", "Records Type", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.SingleSelection, mQry, "After Settlement")


                Case AadhatLedgerCreditors
                    ReportFrm.CreateHelpGrid("From Date", "From Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("Upto Date", "Upto Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.PubLoginDate)
                    Dim mHelpMasterPartyQry$ = " Select 'o' As Tick,  Sg.Code, Sg.Name AS Party, Sg.Address, Ag.GroupName FROM viewHelpSubGroup Sg  Left Join AcGroup Ag On Sg.groupCode= ag.GroupCode Where Sg.SubgroupType In ('Master Customer','Master Supplier') "
                    ReportFrm.CreateHelpGrid("Master Party", "Master Party", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpMasterPartyQry, , 450, 825, 300)
                    Dim mHelpSubPartyQry$ = " Select 'o' As Tick,  Sg.Code, Sg.Name AS Party, Sg.Address, Ag.GroupName FROM viewHelpSubGroup Sg  Left Join AcGroup Ag On Sg.groupCode= ag.GroupCode Where Sg.SubgroupType In ('Customer','Supplier') "
                    ReportFrm.CreateHelpGrid("Sub Party", "Sub Party", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpSubPartyQry, , 450, 825, 300)
                    ReportFrm.CreateHelpGrid("Agent", "Agent", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpAgentQry)
                    ReportFrm.CreateHelpGrid("Division", "Division", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpDivisionQry, "[DIVISIONCODE]")
                    ReportFrm.CreateHelpGrid("Site", "Site", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpSiteQry, "[SITECODE]")
                    mQry = "Select 'After Settlement' as Code, 'After Settlement' as Name 
                            Union All Select 'Complete' as Code, 'Complete' as Name"
                    ReportFrm.CreateHelpGrid("Records Type", "Records Type", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.SingleSelection, mQry, "After Settlement")


                Case PartyLabelPrint
                    mQry = " Select 'o' As Tick,  Sg.Code, Sg.Name AS Party, Sg.SubgroupType, Sg.Address FROM viewHelpSubGroup Sg Where Sg.SubgroupType Not In ('" & SubgroupType.LedgerAccount & "', '" & SubgroupType.Process & "','" & SubgroupType.Godown & "', '" & SubgroupType.Shop & "','" & SubgroupType.Division & "', '" & SubgroupType.Site & "', '" & SubgroupType.RevenuePoint & "') Order By Sg.Name  "
                    ReportFrm.CreateHelpGrid("Party", "Party", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mQry, , 450, 820, 300)
                    ReportFrm.CreateHelpGrid("From Inv.Date", "From Inv.Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", "")
                    ReportFrm.CreateHelpGrid("Upto Inv.Date", "Upto Inv.Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", "")

                Case BrandList
                    mQry = " Select Distinct 'o' As Tick, C.CityCode, C.CityName  
                            From SiteMast S
                            Left Join city C ON s.City_Code = C.CityCode Order By C.CityName "
                    ReportFrm.CreateHelpGrid("City", "City", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mQry, , 450, 820, 300)
                    ReportFrm.CreateHelpGrid("Area", "Area", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpAreaQry, , 450, 820, 300)

                Case WeavingOrderRatio
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.PubEndDate)
                    ReportFrm.CreateHelpGrid("Party", "Party", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpPartyQry)

                Case GSTReports
                    mQry = "Select 'GST 3B' as Code, 'GST 3B' as Name 
                            Union All Select 'GSTR1' as Code, 'GSTR1' as Name "
                    ReportFrm.CreateHelpGrid("Report Type", "Report Type", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.SingleSelection, mQry, "GST 3B")

                    Dim mLastMonthDate As String = DateAdd(DateInterval.Month, -1, CDate(AgL.Dman_Execute("SELECT date('now')", AgL.GCn).ExecuteScalar()))
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.RetMonthStartDate(mLastMonthDate))
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.RetMonthEndDate(mLastMonthDate))

                Case ExportDataToSqlite
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.PubLoginDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.PubLoginDate)

                Case ExportStockIssueData
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.PubLoginDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.PubLoginDate)

                Case DeleteData
                    ReportFrm.CreateHelpGrid("AsOnDate", "As On Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.PubLoginDate)
                    ReportFrm.CreateHelpGrid("Party", "Party", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpPartyQry, "", 600, 800)

                Case StockValuationReportForBank
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.PubEndDate)
                    mQry = "Select 'In Hand' as Code, 'In Hand' as Name
                            Union All 
                            Select 'At Person' as Code, 'At Person' as Name 
                            Union All 
                            Select 'Both' as Code, 'Both' as Name "
                    ReportFrm.CreateHelpGrid("LocationType", "Location Type", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.SingleSelection, mQry, "In Hand")
                    mQry = "SELECT 'Master Purchase Rate' As Code, 'Master Purchase Rate' As Name 
                            UNION ALL 
                            SELECT 'Last Purchase Rate' As Code, 'Last Purchase Rate' As Name "
                    ReportFrm.CreateHelpGrid("Valuation", "Valuation", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.SingleSelection, mQry, "Last Purchase Rate")
                    ReportFrm.CreateHelpGrid("ValuationPercentage", "Valuation Percentage", AgLibrary.FrmReportLayout.FieldFilterDataType.NumericType, AgLibrary.FrmReportLayout.FieldDataType.NumericType, "", "",,,,, False)
                    ReportFrm.CreateHelpGrid("Site", "Site", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpSiteQry, "[SITECODE]")
                    ReportFrm.CreateHelpGrid("Division", "Division", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpDivisionQry, "[DIVISIONCODE]")
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
#End Region

    Private Function FGetVoucher_TypeQry(ByVal TableName As String) As String
        FGetVoucher_TypeQry = " SELECT Distinct 'o' As Tick, H.V_Type AS Code, Vt.Description AS [Voucher Type] " &
                                " FROM " & TableName & " H  " &
                                " LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type "
    End Function

    Private Function FGetSaleInvoiceQry(ByVal NCat As String) As String
        FGetSaleInvoiceQry = " SELECT Distinct 'o' As Tick, H.DocID AS Code, VT.Short_Name || '-'|| H.V_Prefix||'-'||H.ManualRefNo AS [Invoice No] " &
                             " FROM SaleInvoice H 
                               LEFT JOIN Voucher_Type VT ON VT.V_Type = H.V_Type
                               LEFT JOIN SiteMast SI ON SI.Code = H.Site_Code 
                               WHERE VT.NCat ='" & NCat & "' AND H.Site_Code = '" & AgL.PubSiteCode & "' AND H.Div_Code = '" & AgL.PubDivCode & "' "
    End Function

    Private Sub ObjRepFormGlobal_ProcessReport() Handles ReportFrm.ProcessReport
        Select Case mGRepFormName
            Case SaleChallanReport
                ProcSaleReportOld("SaleChallan", "SaleChallanDetail")

            Case SaleOrderRegister
                ProcSaleOrderRegister()

            Case SaleCertificates
                ProcSaleCertificate()

            Case SaleInvoiceReport
                ProcSaleReportOld("SaleInvoice", "SaleInvoiceDetail")

            Case SizeWiseSaleInvoiceReport
                ProcSizeWiseSaleReport()

            Case SaleAndCollectionSummary
                ProcSaleAndCollectionSummary()

            Case PendingToDeliverReport
                ProcPendingToDeliverReport()

            Case SizeWiseJobReceiveReport
                ProcSizeWiseJobReport(Ncat.JobReceive)
            Case SizeWiseJobOrderReport
                ProcSizeWiseJobReport(Ncat.JobOrder)
            Case SizeWiseStockReport
                ProcSizeWiseStockReport()

            Case SizeWiseRateList
                ProcSizeWiseRateList()

            Case SizeWiseConsumptionList
                ProcSizeWiseConsumptionList()

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

            Case PartyLabelPrint
                ProcPartyLabelPrint()

            Case BrandList
                ProcBrandList()

            Case PartyOutstandingReport
                ProcPartyOutstandingReport()

            Case BillWiseProfitability
                ProcBillWiseProfitabilty()

            Case DebtorsOutstandingOverDue
                ProcDebtorsOutstandingOverDue()

            'Case ConcurLedger
            '    ProcConcurLedger()

            Case AadhatLedgerDebtors
                ProcAadhatLedger()

            Case AadhatLedgerCreditors
                ProcAadhatLedgerCreditors()


            Case CurrentStockReport
                ProcCurrentStockReport()

            Case GSTReports
                ProcGSTReports()

            Case ExportDataToSqlite
                ProcExportDataToSqlite()

            Case ImportDataFromSqlite
                ProcImportDataFromSqlite()

            Case RestoreDatabase
                ProcRestoreDatabase()

            Case DeleteData
                ProcDeleteData()

            Case ExportStockIssueData
                ProcExportStockIssueDataToSqlite()

            Case ImportStockReceiveData
                ProcImportStockIssueDataFromSqlite()

            Case StockValuationReportForBank
                ProcStockValuationReportForBank()
        End Select
    End Sub

    'Public Sub New(ByVal mReportFrm As Aglibrary.FrmReportLayout)
    '    ReportFrm = mReportFrm
    'End Sub

    Public Sub New(ByVal mReportFrm As AgLibrary.FrmReportLayout)
        ReportFrm = mReportFrm
    End Sub

    'Public Sub ProcSaleReport(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
    '                            Optional mGridRow As DataGridViewRow = Nothing)
    '    Try
    '        Dim mCondStr$ = ""
    '        Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"
    '        Dim mTags As String() = Nothing
    '        Dim J As Integer



    '        RepTitle = "Sale Invoice Report"

    '        If mFilterGrid IsNot Nothing And mGridRow IsNot Nothing Then
    '            If mGridRow.DataGridView.Columns.Contains("Search Code") = True Then
    '                If mFilterGrid.Item(GFilter, 0).Value = "Month Wise Summary" Then
    '                    mFilterGrid.Item(GFilter, 0).Value = "Item Wise Detail"
    '                    mFilterGrid.Item(GFilter, 1).Value = AgL.RetMonthStartDate(CDate(mGridRow.Cells("Month").Value))
    '                    mFilterGrid.Item(GFilter, 2).Value = AgL.RetMonthEndDate(CDate(mGridRow.Cells("Month").Value))
    '                ElseIf mFilterGrid.Item(GFilter, 0).Value = "Party Wise Summary" Then
    '                    mFilterGrid.Item(GFilter, 0).Value = "Item Wise Detail"
    '                    mFilterGrid.Item(GFilter, 3).Value = mGridRow.Cells("Party").Value
    '                    mFilterGrid.Item(GFilterCode, 3).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
    '                ElseIf mFilterGrid.Item(GFilter, 0).Value = "Item Wise Summary" Then
    '                    mFilterGrid.Item(GFilter, 0).Value = "Item Wise Detail"
    '                    mFilterGrid.Item(GFilter, 4).Value = mGridRow.Cells("Item").Value
    '                    mFilterGrid.Item(GFilterCode, 4).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
    '                ElseIf mFilterGrid.Item(GFilter, 0).Value = "Voucher Type Wise Summary" Then
    '                    mFilterGrid.Item(GFilter, 0).Value = "Item Wise Detail"
    '                    mFilterGrid.Item(GFilter, 6).Value = mGridRow.Cells("Voucher Type").Value
    '                    mFilterGrid.Item(GFilterCode, 6).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
    '                ElseIf mFilterGrid.Item(GFilter, 0).Value = "Agent Wise Summary" Then
    '                    mFilterGrid.Item(GFilter, 0).Value = "Item Wise Detail"
    '                    mFilterGrid.Item(GFilter, 8).Value = mGridRow.Cells("Agent").Value
    '                    mFilterGrid.Item(GFilterCode, 8).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
    '                ElseIf mFilterGrid.Item(GFilter, 0).Value = "Item Group Wise Summary" Then
    '                    mFilterGrid.Item(GFilter, 0).Value = "Item Wise Detail"
    '                    mFilterGrid.Item(GFilter, 9).Value = mGridRow.Cells("Item Group").Value
    '                    mFilterGrid.Item(GFilterCode, 9).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
    '                ElseIf mFilterGrid.Item(GFilter, 0).Value = "Item Category Wise Summary" Then
    '                    mFilterGrid.Item(GFilter, 0).Value = "Item Wise Detail"
    '                    mFilterGrid.Item(GFilter, 10).Value = mGridRow.Cells("Item Category").Value
    '                    mFilterGrid.Item(GFilterCode, 10).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
    '                ElseIf mFilterGrid.Item(GFilter, 0).Value = "City Wise Summary" Then
    '                    mFilterGrid.Item(GFilter, 0).Value = "Item Wise Detail"
    '                    mFilterGrid.Item(GFilter, 11).Value = mGridRow.Cells("City").Value
    '                    mFilterGrid.Item(GFilterCode, 11).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
    '                ElseIf mFilterGrid.Item(GFilter, 0).Value = "State Wise Summary" Then
    '                    mFilterGrid.Item(GFilter, 0).Value = "Item Wise Detail"
    '                    mFilterGrid.Item(GFilter, 12).Value = mGridRow.Cells("State").Value
    '                    mFilterGrid.Item(GFilterCode, 12).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
    '                ElseIf mFilterGrid.Item(GFilter, 0).Value = "Sales Representative Wise Summary" Then
    '                    mFilterGrid.Item(GFilter, 0).Value = "Item Wise Detail"
    '                    mFilterGrid.Item(GFilter, 13).Value = mGridRow.Cells("Sales Representative").Value
    '                    mFilterGrid.Item(GFilterCode, 13).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
    '                ElseIf mFilterGrid.Item(GFilter, 0).Value = "Responsible Person Wise Summary" Then
    '                    mFilterGrid.Item(GFilter, 0).Value = "Item Wise Detail"
    '                    mFilterGrid.Item(GFilter, 14).Value = mGridRow.Cells("Responsible Person").Value
    '                    mFilterGrid.Item(GFilterCode, 14).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
    '                ElseIf mFilterGrid.Item(GFilter, 0).Value = "Doc.Header Wise Detail" Or
    '                        mFilterGrid.Item(GFilter, 0).Value = "Item Wise Detail" Then

    '                    FOpenForm(mGridRow.Cells("Search Code").Value)

    '                    Exit Sub
    '                End If
    '            Else
    '                Exit Sub
    '            End If
    '        End If

    '        If GRepFormName = SaleOrderReport Then
    '            mCondStr = " Where VT.NCat In ('" & Ncat.SaleOrder & "', '" & Ncat.SaleOrderCancel & "') "
    '        Else
    '            mCondStr = " Where VT.NCat In ('" & Ncat.SaleInvoice & "', '" & Ncat.SaleReturn & "') "
    '        End If
    '        mCondStr = mCondStr & " AND H.Div_Code = '" & AgL.PubDivCode & "' "
    '        mCondStr = mCondStr & " AND H.V_Date Between '" & CDate(ReportFrm.FGetText(1)).ToString("s") & "' And '" & CDate(ReportFrm.FGetText(2)).ToString("s") & "' "
    '        mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.SaleToParty", 3)
    '        mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Item", 4)
    '        mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.Site_Code", 5)
    '        mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.V_Type", 6)
    '        If ReportFrm.FGetText(7) = "Cash" Then
    '            mCondStr = mCondStr & " AND BillToParty.Nature = 'Cash'"
    '        ElseIf ReportFrm.FGetText(7) = "Credit" Then
    '            mCondStr = mCondStr & " AND BillToParty.Nature <> 'Cash'"
    '        End If
    '        mCondStr = mCondStr & ReportFrm.GetWhereCondition("LTV.Agent", 8)
    '        mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemGroup", 9)
    '        mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemCategory", 10)
    '        mCondStr = mCondStr & ReportFrm.GetWhereCondition("City.CityCode", 11)
    '        mCondStr = mCondStr & ReportFrm.GetWhereCondition("City.State", 12)
    '        mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.SalesRepresentative", 13)
    '        mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.ResponsiblePerson", 14)
    '        'If ReportFrm.FGetText(8) <> "All" Then
    '        '    mCondStr += " And H.Agent = '" & ReportFrm.FGetCode(8) & "' "
    '        'End If

    '        'mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.Agent", 8)

    '        If ReportFrm.FGetText(15) <> "All" Then
    '            mTags = ReportFrm.FGetText(15).ToString.Split(",")
    '            For J = 0 To mTags.Length - 1
    '                mCondStr += " And CharIndex('+' || '" & mTags(J) & "',H.Tags) > 0 "
    '            Next
    '        End If


    '        mQry = " SELECT H.DocID, H.V_Type, Vt.Description as VoucherType, strftime('%d/%m/%Y', H.V_Date) As V_Date, H.V_Date As V_Date_ActualFormat,
    '                H.SaleToParty, I.ItemGroup, I.ItemCategory,
    '                (Case When H.SaleToParty=H.BillToParty Then Party.Name Else BillToParty.Name || ' - ' || Party.Name End) As SaleToPartyName , 
    '                LTV.Agent As AgentCode, Agent.Name As AgentName, H.ResponsiblePerson, ResponsiblePerson.Name as ResponsiblePersonName,
    '                L.SalesRepresentative, SalesRep.Name as SalesRepresentativeName,
    '                City.CityCode, City.CityName, State.Code As StateCode, State.Description As StateName,
    '                H.Div_Code || H.Site_Code || '-' || H.V_Type || '-' || H.ManualRefNo as InvoiceNo, H.ManualRefNo, L.Item,
    '                I.Specification as ItemSpecification, I.Description As ItemDesc,IG.Description as ItemGroupDescription, IC.Description as ItemCategoryDescription,  
    '                Cast((Case When L.DiscountPer = 0 Then '' else L.DiscountPer End) as nVarchar) || (Case When L.AdditionalDiscountPer>0 Then '+' else '' End) || Cast((Case When L.AdditionalDiscountPer=0 Then '' else L.AdditionalDiscountPer End) as nVarchar) as DiscountPer, L.DiscountAmount + L.AdditionalDiscountAmount as Discount, L.Taxable_Amount, (Case When L.Net_Amount=0 Then L.Amount Else L.Net_Amount End) as Net_Amount, L.Qty, L.Unit, L.Rate, L.Amount -(L.DiscountAmount + L.AdditionalDiscountAmount) as AmountExDiscount,
    '                L.Tax1+L.Tax2+L.Tax3+L.Tax4+L.Tax5 as TotalTax
    '                FROM SaleInvoice H 
    '                Left Join SaleInvoiceDetail L On H.DocID = L.DocID 
    '                Left Join Item I On L.Item = I.Code 
    '                Left Join Item IG On I.ItemGroup = IG.Code
    '                Left Join Item IC On I.ItemCategory = IC.Code
    '                Left Join viewHelpSubgroup Party On H.SaleToParty = Party.Code 
    '                Left Join viewHelpSubgroup BillToParty On H.BillToParty = BillToParty.Code 
    '                Left Join (Select SILTV.Subcode, Max(SILTV.Agent) as Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode) as LTV On Party.code = LTV.Subcode
    '                Left Join viewHelpSubGroup Agent On LTV.Agent = Agent.Code 
    '                Left Join viewHelpSubGroup SalesRep On L.SalesRepresentative = SalesRep.Code 
    '                Left Join viewHelpSubGroup ResponsiblePerson On H.ResponsiblePerson = ResponsiblePerson.Code 
    '                Left Join City On H.SaleToPartyCity = City.CityCode 
    '                Left Join State On City.State = State.Code
    '                LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type                     
    '                " & mCondStr


    '        If ReportFrm.FGetText(0) = "Doc.Header Wise Detail" Then
    '            If GRepFormName = SaleOrderReport Then
    '                mQry = " Select VMain.DocId As SearchCode, Max(VMain.V_Date) As OrderDate, Max(VMain.InvoiceNo) As OrderNo,
    '                Max(VMain.SaleToPartyName) As Party, IfNull(Sum(VMain.AmountExDiscount),0) As Amount, IfNull(Sum(VMain.Discount),0) As Discount,
    '                IfNull(Sum(VMain.Taxable_Amount),0) As TaxableAmount, IfNull(Sum(VMain.TotalTax),0) As TaxAmount, IfNull(Sum(VMain.Net_Amount),0) As NetAmount
    '                From (" & mQry & ") As VMain
    '                GROUP By VMain.DocId 
    '                Order By Max(VMain.V_Date_ActualFormat) "
    '            Else
    '                mQry = " Select VMain.DocId As SearchCode, Max(VMain.V_Date) As InvoiceDate, Max(VMain.InvoiceNo) As InvoiceNo,
    '                Max(VMain.SaleToPartyName) As Party, IfNull(Sum(VMain.AmountExDiscount),0) As Amount, IfNull(Sum(VMain.Discount),0) As Discount,
    '                IfNull(Sum(VMain.Taxable_Amount),0) As TaxableAmount, IfNull(Sum(VMain.TotalTax),0) As TaxAmount, IfNull(Sum(VMain.Net_Amount),0) As NetAmount
    '                From (" & mQry & ") As VMain
    '                GROUP By VMain.DocId 
    '                Order By Max(VMain.V_Date_ActualFormat) "
    '            End If
    '        ElseIf ReportFrm.FGetText(0) = "Item Wise Detail" Then
    '            If GRepFormName = SaleOrderReport Then
    '                mQry = " Select VMain.DocId As SearchCode, Max(VMain.V_Date) As [Order Date], Max(VMain.InvoiceNo) As [Order No],
    '                Max(VMain.SaleToPartyName) As Party, Max(VMain.ItemDesc) As Item, Sum(VMain.Qty) As Qty, Max(VMain.Unit) As Unit, 
    '                Max(VMain.Rate) As Rate,
    '                Sum(VMain.AmountExDiscount) As Amount, Max(VMain.DiscountPer) As [Discount Per], 
    '                Sum(VMain.Discount) As Discount,
    '                Sum(VMain.Taxable_Amount) As [Taxable Amount],
    '                Sum(VMain.TotalTax) As [Tax Amount],
    '                Sum(VMain.Net_Amount) As [Net Amount]
    '                From (" & mQry & ") As VMain
    '                GROUP By VMain.DocId, VMain.Item 
    '                Order By  Max(VMain.V_Date) "
    '            Else
    '                mQry = " Select VMain.DocId As SearchCode, Max(VMain.V_Date) As [Invoice Date], Max(VMain.InvoiceNo) As [Invoice No],
    '                Max(VMain.SaleToPartyName) As Party, Max(VMain.ItemDesc) As Item, Sum(VMain.Qty) As Qty, Max(VMain.Unit) As Unit, 
    '                Max(VMain.Rate) As Rate,
    '                Sum(VMain.AmountExDiscount) As Amount, Max(VMain.DiscountPer) As [Discount Per], 
    '                Sum(VMain.Discount) As Discount,
    '                Sum(VMain.Taxable_Amount) As [Taxable Amount],
    '                Sum(VMain.TotalTax) As [Tax Amount],
    '                Sum(VMain.Net_Amount) As [Net Amount]
    '                From (" & mQry & ") As VMain
    '                GROUP By VMain.DocId, VMain.Item 
    '                Order By  Max(VMain.V_Date) "
    '            End If
    '        ElseIf ReportFrm.FGetText(0) = "Voucher Type Wise Summary" Then
    '            mQry = " Select VMain.V_Type as SearchCode, Max(VMain.VoucherType) As VoucherType, 
    '                Count(Distinct Vmain.DocID) as [Doc.Count], 
    '                Sum(VMain.Taxable_Amount) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount, Sum(VMain.Net_Amount) As [Net Amount]
    '                From (" & mQry & ") As VMain
    '                GROUP By VMain.V_Type
    '                Order By Max(VMain.VoucherType)"
    '        ElseIf ReportFrm.FGetText(0) = "Party Wise Summary" Then
    '            mQry = " Select VMain.SaleToParty as SearchCode, Max(VMain.SaleToPartyName) As Party, 
    '                Sum(VMain.Taxable_Amount) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount, Sum(VMain.Net_Amount) As [Net Amount]
    '                From (" & mQry & ") As VMain
    '                GROUP By VMain.SaleToParty 
    '                Order By Max(VMain.SaleToPartyName)"
    '        ElseIf ReportFrm.FGetText(0) = "Sales Representative Wise Summary" Then
    '            mQry = " Select VMain.SalesRepresentative as SearchCode, Max(VMain.SalesRepresentativeName) As SalesRepresentative, 
    '                Count(Distinct Vmain.DocID) as InvoicesCount, Count(Distinct VMain.V_Date) as DaysCount, 
    '                Sum(VMain.Taxable_Amount) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount, Sum(VMain.Net_Amount) As [Net Amount]
    '                From (" & mQry & ") As VMain
    '                GROUP By VMain.SalesRepresentative 
    '                Order By Max(VMain.SalesRepresentativeName)"
    '        ElseIf ReportFrm.FGetText(0) = "Responsible Person Wise Summary" Then
    '            mQry = " Select VMain.ResponsiblePerson as SearchCode, Max(VMain.ResponsiblePersonName) As ResponsiblePerson,
    '                Count(Distinct Vmain.DocID) as InvoicesCount, Count(Distinct VMain.V_Date) as DaysCount, 
    '                Sum(VMain.Taxable_Amount) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount, Sum(VMain.Net_Amount) As [Net Amount]
    '                From (" & mQry & ") As VMain
    '                GROUP By VMain.ResponsiblePerson 
    '                Order By Max(VMain.ResponsiblePersonName)"
    '        ElseIf ReportFrm.FGetText(0) = "Item Wise Summary" Then
    '            mQry = " Select VMain.Item As SearchCode, Max(VMain.ItemDesc) As [Item], 
    '                IfNull(Sum(VMain.Taxable_Amount),0) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount, IfNull(Sum(VMain.Net_Amount),0) As [Net Amount]
    '                From (" & mQry & ") As VMain
    '                GROUP By VMain.Item 
    '                Order By Max(VMain.ItemDesc)"
    '        ElseIf ReportFrm.FGetText(0) = "Item Group Wise Summary" Then
    '            mQry = " Select VMain.ItemGroup as SearchCode, Max(VMain.ItemGroupDescription) As [Item Group], 
    '                IfNull(Sum(VMain.Taxable_Amount),0) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount, IfNull(Sum(VMain.Net_Amount),0) As [Net Amount]
    '                From (" & mQry & ") As VMain
    '                GROUP By VMain.ItemGroup 
    '                Order By Max(VMain.ItemGroupDescription)"
    '        ElseIf ReportFrm.FGetText(0) = "Item Category Wise Summary" Then
    '            mQry = " Select VMain.ItemCategory as SearchCode, Max(VMain.ItemCategoryDescription) As [Item Category], 
    '                IfNull(Sum(VMain.Taxable_Amount),0) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount, IfNull(Sum(VMain.Net_Amount),0) As [Net Amount]
    '                From (" & mQry & ") As VMain
    '                GROUP By VMain.ItemCategory 
    '                Order By Max(VMain.ItemCategoryDescription)"
    '        ElseIf ReportFrm.FGetText(0) = "City Wise Summary" Then
    '            mQry = " Select VMain.CityCode as SearchCode, Max(VMain.CityName) As [City], 
    '                IfNull(Sum(VMain.Taxable_Amount),0) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount, IfNull(Sum(VMain.Net_Amount),0) As [Net Amount]
    '                From (" & mQry & ") As VMain
    '                GROUP By VMain.CityCode 
    '                Order By Max(VMain.CityName)"
    '        ElseIf ReportFrm.FGetText(0) = "State Wise Summary" Then
    '            mQry = " Select VMain.StateCode as SearchCode, Max(VMain.StateName) As [State], 
    '                Sum(VMain.Taxable_Amount) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount, Sum(VMain.Net_Amount) As [Net Amount]
    '                From (" & mQry & ") As VMain
    '                GROUP By VMain.StateCode 
    '                Order By Max(VMain.StateName)"
    '        ElseIf ReportFrm.FGetText(0) = "Agent Wise Summary" Then
    '            mQry = " Select VMain.AgentCode As SearchCode, Max(VMain.AgentName) As [Agent], 
    '                Sum(VMain.Taxable_Amount) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount, Sum(VMain.Net_Amount) As [Net Amount]
    '                From (" & mQry & ") As VMain
    '                GROUP By VMain.AgentCode 
    '                Order By Max(VMain.AgentName)"
    '        ElseIf ReportFrm.FGetText(0) = "Month Wise Summary" Then
    '            If AgL.PubServerName = "" Then
    '                mQry = " Select strftime('%m-%Y',VMain.V_Date_ActualFormat) As SearchCode, strftime('%m-%Y',VMain.V_Date_ActualFormat) As [Month], 
    '                Sum(VMain.Taxable_Amount) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount, Sum(VMain.Net_Amount) As [Net Amount]
    '                From (" & mQry & ") As VMain
    '                GROUP By strftime('%m-%Y',VMain.V_Date_ActualFormat)  
    '                Order By strftime('%Y',VMain.V_Date_ActualFormat), strftime('%m',VMain.V_Date_ActualFormat)"
    '            Else
    '                mQry = " Select Substring(Convert(NVARCHAR, VMain.V_Date_ActualFormat,103),4,7) As SearchCode, Substring(Convert(NVARCHAR, VMain.V_Date_ActualFormat,103),4,7) As [Month], 
    '                Sum(VMain.Taxable_Amount) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount, Sum(VMain.Net_Amount) As [Net Amount]
    '                From (" & mQry & ") As VMain
    '                GROUP By Substring(Convert(NVARCHAR, VMain.V_Date_ActualFormat,103),4,7), Year(VMain.V_Date_ActualFormat), Month(VMain.V_Date_ActualFormat)  
    '                Order By Year(VMain.V_Date_ActualFormat), Month(VMain.V_Date_ActualFormat) "
    '            End If
    '        End If




    '        DsHeader = AgL.FillData(mQry, AgL.GCn)

    '        If DsHeader.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

    '        ReportFrm.Text = "Sale Invoice Report - " + ReportFrm.FGetText(0)
    '        ReportFrm.ClsRep = Me
    '        ReportFrm.ReportProcName = "ProcSaleReport"

    '        ReportFrm.ProcFillGrid(DsHeader)
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '        DsHeader = Nothing
    '    End Try
    'End Sub


    Private Sub ProcSaleOrderRegister()
        Try
            RepName = "SaleOrderRegister" : RepTitle = "Sale Order Register"

            If ReportFrm.FGetText(0) = "Item / Customer Wise Detail" Then
                RepName = "SaleOrderRegisterCustomer"
            Else
                RepName = "SaleOrderRegister"
            End If



            Dim mCondStr$ = ""
            Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"
            Dim mTags As String() = Nothing
            Dim J As Integer



            If GRepFormName = SaleOrderRegister Then
                mCondStr = " Where VT.NCat In ('" & Ncat.SaleOrder & "', '" & Ncat.SaleOrderCancel & "') "
            Else
                mCondStr = " Where VT.NCat In ('" & Ncat.SaleInvoice & "', '" & Ncat.SaleReturn & "') "
            End If
            mCondStr = mCondStr & " AND H.Div_Code = '" & AgL.PubDivCode & "' "
            mCondStr = mCondStr & " AND Date(H.V_Date) Between " & AgL.Chk_Date(CDate(ReportFrm.FGetText(1)).ToString("s")) & " And " & AgL.Chk_Date(CDate(ReportFrm.FGetText(2)).ToString("s")) & " "
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.SaleToParty", 3)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Item", 4)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.Site_Code", 5)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.V_Type", 6)
            If ReportFrm.FGetText(7) = "Cash" Then
                mCondStr = mCondStr & " AND BillToParty.Nature = 'Cash'"
            ElseIf ReportFrm.FGetText(7) = "Credit" Then
                mCondStr = mCondStr & " AND BillToParty.Nature <> 'Cash'"
            End If
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("LTV.Agent", 8)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemGroup", 9)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemCategory", 10)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("City.CityCode", 11)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("City.State", 12)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.SalesRepresentative", 13)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.ResponsiblePerson", 14)
            'If ReportFrm.FGetText(8) <> "All" Then
            '    mCondStr += " And H.Agent = '" & ReportFrm.FGetCode(8) & "' "
            'End If

            'mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.Agent", 8)

            If ReportFrm.FGetText(15) <> "All" Then
                mTags = ReportFrm.FGetText(15).ToString.Split(",")
                For J = 0 To mTags.Length - 1
                    mCondStr += " And CharIndex('+' || '" & mTags(J) & "',H.Tags) > 0 "
                Next
            End If




            mQry = " SELECT H.DocID, H.V_Type, Vt.Description as VoucherType, Site.Name as SiteName, strftime('%d/%m/%Y', H.V_Date) As V_Date, strftime('%d/%m/%Y', H.DeliveryDate) As DeliveryDate, H.V_Date As V_Date_ActualFormat,
                    H.SaleToParty, I.ItemGroup, I.ItemCategory,
                    (Case When H.SaleToParty=H.BillToParty Then Party.Name Else BillToParty.Name || '    /   ' || Party.Name End) As SaleToPartyName , 
                    LTV.Agent As AgentCode, Agent.Name As AgentName, H.ResponsiblePerson, ResponsiblePerson.Name as ResponsiblePersonName,
                    L.SalesRepresentative, SalesRep.Name as SalesRepresentativeName,
                    City.CityCode, City.CityName, State.Code As StateCode, State.Description As StateName,
                    H.ManualRefNo as InvoiceNo, H.ManualRefNo, L.Item,
                    I.Specification as ItemSpecification, I.Description As ItemDesc,IG.Description as ItemGroupDescription, IC.Description as ItemCategoryDescription,  
                    Cast((Case When L.DiscountPer = 0 Then '' else L.DiscountPer End) as nVarchar) || (Case When L.AdditionalDiscountPer>0 Then '+' else '' End) || Cast((Case When L.AdditionalDiscountPer=0 Then '' else L.AdditionalDiscountPer End) as nVarchar) as DiscountPer, L.DiscountAmount + L.AdditionalDiscountAmount as Discount, L.Taxable_Amount, (Case When L.Net_Amount=0 Then L.Amount Else L.Net_Amount End) as Net_Amount, L.Qty, L.Unit, L.Rate, L.Amount -(L.DiscountAmount + L.AdditionalDiscountAmount) as AmountExDiscount,
                    L.Tax1+L.Tax2+L.Tax3+L.Tax4+L.Tax5 as TotalTax
                    FROM SaleInvoice H 
                    Left Join SaleInvoiceDetail L On H.DocID = L.DocID 
                    Left Join Item I On L.Item = I.Code 
                    Left Join Item IG On I.ItemGroup = IG.Code
                    Left Join Item IC On I.ItemCategory = IC.Code
                    Left Join viewHelpSubgroup Party On H.SaleToParty = Party.Code 
                    Left Join viewHelpSubgroup BillToParty On H.BillToParty = BillToParty.Code 
                    Left Join (Select SILTV.Subcode, Max(SILTV.Agent) as Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode) as LTV On Party.code = LTV.Subcode
                    Left Join viewHelpSubGroup Agent On LTV.Agent = Agent.Code 
                    Left Join viewHelpSubGroup SalesRep On L.SalesRepresentative = SalesRep.Code 
                    Left Join viewHelpSubGroup ResponsiblePerson On H.ResponsiblePerson = ResponsiblePerson.Code 
                    Left Join City On H.SaleToPartyCity = City.CityCode 
                    Left Join State On City.State = State.Code
                    LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type                     
                    Left Join SiteMast Site On H.Site_Code = Site.Code
                    " & mCondStr


            If ReportFrm.FGetText(0) = "Doc.Header Wise Detail" Then
                If GRepFormName = SaleOrderRegister Then
                    mQry = " Select VMain.DocId As SearchCode, Max(VMain.V_Date) As OrderDate, Max(VMain.InvoiceNo) As OrderNo,
                    Max(VMain.SaleToPartyName) As Party, IfNull(Sum(VMain.AmountExDiscount),0) As Amount, IfNull(Sum(VMain.Discount),0) As Discount,
                    IfNull(Sum(VMain.Taxable_Amount),0) As TaxableAmount, IfNull(Sum(VMain.TotalTax),0) As TaxAmount, IfNull(Sum(VMain.Net_Amount),0) As NetAmount
                    From (" & mQry & ") As VMain
                    GROUP By VMain.DocId 
                    Order By Max(VMain.V_Date_ActualFormat) "
                Else
                    mQry = " Select VMain.DocId As SearchCode, Max(VMain.V_Date) As InvoiceDate, Max(VMain.InvoiceNo) As InvoiceNo,
                    Max(VMain.SaleToPartyName) As Party, IfNull(Sum(VMain.AmountExDiscount),0) As Amount, IfNull(Sum(VMain.Discount),0) As Discount,
                    IfNull(Sum(VMain.Taxable_Amount),0) As TaxableAmount, IfNull(Sum(VMain.TotalTax),0) As TaxAmount, IfNull(Sum(VMain.Net_Amount),0) As NetAmount
                    From (" & mQry & ") As VMain
                    GROUP By VMain.DocId 
                    Order By Max(VMain.V_Date_ActualFormat) "
                End If
            ElseIf ReportFrm.FGetText(0) = "Customer / Item Wise Detail" Or ReportFrm.FGetText(0) = "Item / Customer Wise Detail" Then
                If GRepFormName = SaleOrderRegister Then
                    mQry = " Select VMain.DocId As SearchCode, Max(VMain.SiteName) as SiteName, Max(VMain.V_Date) As [Order Date], Max(Vmain.DeliveryDate) as DeliveryDate, Max(VMain.InvoiceNo) As [Order No],
                    Max(VMain.SaleToPartyName) As Party, Max(VMain.ItemDesc) As Item, Sum(VMain.Qty) As Qty, Max(VMain.Unit) As Unit, 
                    Max(VMain.Rate) As Rate,
                    Sum(VMain.AmountExDiscount) As Amount, Max(VMain.DiscountPer) As [Discount Per], 
                    Sum(VMain.Discount) As Discount,
                    Sum(VMain.Taxable_Amount) As [Taxable Amount],
                    Sum(VMain.TotalTax) As [Tax Amount],
                    Sum(VMain.Net_Amount) As [Net Amount]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.DocId, VMain.Item 
                    Order By  Max(VMain.V_Date), Max(VMain.SiteName), Cast(Max(VMain.InvoiceNo) as Integer)  "
                Else
                    mQry = " Select VMain.DocId As SearchCode, Max(VMain.V_Date) As [Invoice Date], Max(VMain.InvoiceNo) As [Invoice No],
                    Max(VMain.SaleToPartyName) As Party, Max(VMain.ItemDesc) As Item, Sum(VMain.Qty) As Qty, Max(VMain.Unit) As Unit, 
                    Max(VMain.Rate) As Rate,
                    Sum(VMain.AmountExDiscount) As Amount, Max(VMain.DiscountPer) As [Discount Per], 
                    Sum(VMain.Discount) As Discount,
                    Sum(VMain.Taxable_Amount) As [Taxable Amount],
                    Sum(VMain.TotalTax) As [Tax Amount],
                    Sum(VMain.Net_Amount) As [Net Amount]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.DocId, VMain.Item 
                    Order By  Max(VMain.V_Date) "
                End If
            ElseIf ReportFrm.FGetText(0) = "Voucher Type Wise Summary" Then
                mQry = " Select VMain.V_Type as SearchCode, Max(VMain.VoucherType) As VoucherType, 
                    Count(Distinct Vmain.DocID) as [Doc.Count], 
                    Sum(VMain.Taxable_Amount) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount, Sum(VMain.Net_Amount) As [Net Amount]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.V_Type
                    Order By Max(VMain.VoucherType)"
            ElseIf ReportFrm.FGetText(0) = "Party Wise Summary" Then
                mQry = " Select VMain.SaleToParty as SearchCode, Max(VMain.SaleToPartyName) As Party, 
                    Sum(VMain.Taxable_Amount) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount, Sum(VMain.Net_Amount) As [Net Amount]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.SaleToParty 
                    Order By Max(VMain.SaleToPartyName)"
            ElseIf ReportFrm.FGetText(0) = "Sales Representative Wise Summary" Then
                mQry = " Select VMain.SalesRepresentative as SearchCode, Max(VMain.SalesRepresentativeName) As SalesRepresentative, 
                    Count(Distinct Vmain.DocID) as InvoicesCount, Count(Distinct VMain.V_Date) as DaysCount, 
                    Sum(VMain.Taxable_Amount) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount, Sum(VMain.Net_Amount) As [Net Amount]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.SalesRepresentative 
                    Order By Max(VMain.SalesRepresentativeName)"
            ElseIf ReportFrm.FGetText(0) = "Responsible Person Wise Summary" Then
                mQry = " Select VMain.ResponsiblePerson as SearchCode, Max(VMain.ResponsiblePersonName) As ResponsiblePerson,
                    Count(Distinct Vmain.DocID) as InvoicesCount, Count(Distinct VMain.V_Date) as DaysCount, 
                    Sum(VMain.Taxable_Amount) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount, Sum(VMain.Net_Amount) As [Net Amount]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.ResponsiblePerson 
                    Order By Max(VMain.ResponsiblePersonName)"
            ElseIf ReportFrm.FGetText(0) = "Item Wise Summary" Then
                mQry = " Select VMain.Item As SearchCode, Max(VMain.ItemDesc) As [Item], 
                    IfNull(Sum(VMain.Taxable_Amount),0) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount, IfNull(Sum(VMain.Net_Amount),0) As [Net Amount]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.Item 
                    Order By Max(VMain.ItemDesc)"
            ElseIf ReportFrm.FGetText(0) = "Item Group Wise Summary" Then
                mQry = " Select VMain.ItemGroup as SearchCode, Max(VMain.ItemGroupDescription) As [Item Group], 
                    IfNull(Sum(VMain.Taxable_Amount),0) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount, IfNull(Sum(VMain.Net_Amount),0) As [Net Amount]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.ItemGroup 
                    Order By Max(VMain.ItemGroupDescription)"
            ElseIf ReportFrm.FGetText(0) = "Item Category Wise Summary" Then
                mQry = " Select VMain.ItemCategory as SearchCode, Max(VMain.ItemCategoryDescription) As [Item Category], 
                    IfNull(Sum(VMain.Taxable_Amount),0) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount, IfNull(Sum(VMain.Net_Amount),0) As [Net Amount]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.ItemCategory 
                    Order By Max(VMain.ItemCategoryDescription)"
            ElseIf ReportFrm.FGetText(0) = "City Wise Summary" Then
                mQry = " Select VMain.CityCode as SearchCode, Max(VMain.CityName) As [City], 
                    IfNull(Sum(VMain.Taxable_Amount),0) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount, IfNull(Sum(VMain.Net_Amount),0) As [Net Amount]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.CityCode 
                    Order By Max(VMain.CityName)"
            ElseIf ReportFrm.FGetText(0) = "State Wise Summary" Then
                mQry = " Select VMain.StateCode as SearchCode, Max(VMain.StateName) As [State], 
                    Sum(VMain.Taxable_Amount) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount, Sum(VMain.Net_Amount) As [Net Amount]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.StateCode 
                    Order By Max(VMain.StateName)"
            ElseIf ReportFrm.FGetText(0) = "Agent Wise Summary" Then
                mQry = " Select VMain.AgentCode As SearchCode, Max(VMain.AgentName) As [Agent], 
                    Sum(VMain.Taxable_Amount) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount, Sum(VMain.Net_Amount) As [Net Amount]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.AgentCode 
                    Order By Max(VMain.AgentName)"
            ElseIf ReportFrm.FGetText(0) = "Month Wise Summary" Then
                If AgL.PubServerName = "" Then
                    mQry = " Select strftime('%m-%Y',VMain.V_Date_ActualFormat) As SearchCode, strftime('%m-%Y',VMain.V_Date_ActualFormat) As [Month], 
                    Sum(VMain.Taxable_Amount) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount, Sum(VMain.Net_Amount) As [Net Amount]
                    From (" & mQry & ") As VMain
                    GROUP By strftime('%m-%Y',VMain.V_Date_ActualFormat)  
                    Order By strftime('%Y',VMain.V_Date_ActualFormat), strftime('%m',VMain.V_Date_ActualFormat)"
                Else
                    mQry = " Select Substring(Convert(NVARCHAR, VMain.V_Date_ActualFormat,103),4,7) As SearchCode, Substring(Convert(NVARCHAR, VMain.V_Date_ActualFormat,103),4,7) As [Month], 
                    Sum(VMain.Taxable_Amount) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount, Sum(VMain.Net_Amount) As [Net Amount]
                    From (" & mQry & ") As VMain
                    GROUP By Substring(Convert(NVARCHAR, VMain.V_Date_ActualFormat,103),4,7), Year(VMain.V_Date_ActualFormat), Month(VMain.V_Date_ActualFormat)  
                    Order By Year(VMain.V_Date_ActualFormat), Month(VMain.V_Date_ActualFormat) "
                End If
            End If
            DsRep = AgL.FillData(mQry, AgL.GCn)

            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            ReportFrm.PrintReport(DsRep, RepName, RepTitle)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub



#Region "Sale Report"
    Private Sub ProcSaleReportOld(ByVal HeaderTable As String, ByVal LineTable As String)
        Try
            RepName = "Trade_SaleReport" : RepTitle = "Sale Report"


            Dim mCondStr$ = ""
            Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"

            RepTitle = "Sale Invoice Report"
            If ReportFrm.FGetText(0) = "Invoice Wise Detail" Then
                RepName = "Trade_SaleReport"
            ElseIf ReportFrm.FGetText(0) = "Item Wise Detail" Then
                RepName = "Trade_ItemWiseSaleReport"
            ElseIf ReportFrm.FGetText(0) = "Party Wise Summary" Then
                RepName = "Trade_SaleReportSummary"
                strGrpFld = "H.SaleToParty"
                strGrpFldDesc = "Party.Name"
                strGrpFldHead = "'Party Name'"
            ElseIf ReportFrm.FGetText(0) = "Item Group Wise Summary" Then
                RepName = "Trade_SaleReportSummary"
                strGrpFld = "I.ItemGroup"
                strGrpFldDesc = "IG.Description"
                strGrpFldHead = "'Item Group'"
            ElseIf ReportFrm.FGetText(0) = "Item Category Wise Summary" Then
                RepName = "Trade_SaleReportSummary"
                strGrpFld = "I.ItemCategory"
                strGrpFldDesc = "IC.Description"
                strGrpFldHead = "'Item Category'"
            ElseIf ReportFrm.FGetText(0) = "City Wise Summary" Then
                RepName = "Trade_SaleReportSummary"
                strGrpFld = "H.SaleToPartyCity"
                strGrpFldDesc = "City.CityName"
                strGrpFldHead = "'City'"
            ElseIf ReportFrm.FGetText(0) = "State Wise Summary" Then
                RepName = "Trade_SaleReportSummary"
                strGrpFld = "City.State"
                strGrpFldDesc = "State.Description"
                strGrpFldHead = "'State'"

            ElseIf ReportFrm.FGetText(0) = "Agent Wise Summary" Then
                RepName = "Trade_SaleReportSummary"
                strGrpFld = "Agent.Name"
                strGrpFldDesc = "Agent.Name"
                strGrpFldHead = "'AgentName'"
            ElseIf ReportFrm.FGetText(0) = "Month Wise Summary" Then
                RepName = "Trade_SaleReportSummary_Month"
                strGrpFld = "strftime('%m-%Y',H.V_Date) "
                strGrpFldDesc = "H.V_Date"
                strGrpFldHead = "'Month'"
            End If


            mCondStr = " Where 1 = 1 "



            mCondStr = mCondStr & " AND Date(H.V_Date) Between " & AgL.Chk_Date(CDate(ReportFrm.FGetText(1)).ToString("s")) & " And " & AgL.Chk_Date(CDate(ReportFrm.FGetText(2)).ToString("s")) & " "
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.BillToParty", 3)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Item", 4)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.Site_Code", 5)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.V_Type", 6)
            If ReportFrm.FGetText(7) = "Cash" Then
                mCondStr = mCondStr & " AND Sg.Nature = 'Cash'"
            ElseIf ReportFrm.FGetText(7) = "Credit" Then
                mCondStr = mCondStr & " AND Sg.Nature <> 'Cash'"
            End If
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.Agent", 8)

            'If ReportFrm.FGetText(8) <> "All" Then mCondStr += " And H.Agent = '" & ReportFrm.FGetCode(8) & "'"



            mQry = " SELECT " & strGrpFld & " as GrpField, " & strGrpFldDesc & " as GrpFieldDesc, " & strGrpFldHead & " as GrpFieldHead, 
                    H.DocID, H.V_Date, 
                    Party.Name As SaleToPartyName , 
                    Agent.Name As AgentName , 
                    H.Div_Code || H.Site_Code || '-' || H.V_Type || '-' || H.ManualRefNo as InvoiceNo, H.ManualRefNo, 
                    I.Specification as ItemSpecification, I.Description As ItemDesc,IG.Description as ItemGroupName, IC.Description as ItemCategoryDescription,  
                    Cast((Case When L.DiscountPer = 0 Then '' else L.DiscountPer End) as nVarchar) || (Case When L.AdditionalDiscountPer>0 Then '+' else '' End) || Cast((Case When L.AdditionalDiscountPer=0 Then '' else L.AdditionalDiscountPer End) as nVarchar) as DiscountPer, L.DiscountAmount + L.AdditionalDiscountAmount as Discount, L.Taxable_Amount, L.Net_Amount, L.Qty, L.Unit, L.Rate, L.Amount -(L.DiscountAmount + L.AdditionalDiscountAmount) as AmountExDiscount,
                    L.Tax1+L.Tax2+L.Tax3+L.Tax4+L.Tax5 as TotalTax
                    FROM SaleInvoice H 
                    Left Join SaleInvoiceDetail L On H.DocID = L.DocID 
                    Left Join Item I On L.Item = I.Code 
                    Left Join ItemGroup IG On I.ItemGroup = IG.Code
                    Left Join ItemCategory IC On I.ItemCategory = IC.Code
                    Left Join viewHelpSubgroup Party On H.SaleToParty = Party.Code 
                    Left Join viewHelpSubGroup Agent On H.Agent = Agent.Code 
                    Left Join City On H.SaleToPartyCity = City.CityCode 
                    Left Join State On City.State = State.Code
                    LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type " & mCondStr & OrderByStr
            DsRep = AgL.FillData(mQry, AgL.GCn)

            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            ReportFrm.PrintReport(DsRep, RepName, RepTitle)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
#End Region

#Region "Sale Certificate"
    Private Sub ProcSaleCertificate()
        Try


            If ReportFrm.FGetText(0) = "Form 21" Then
                RepName = "SalesCertificate_Form21" : RepTitle = "Form 21"
                If AgL.StrCmp(AgL.PubDBName, "RVN") Then
                    RepName = "SalesCertificate_RVN_Form21"
                End If
            End If
            Dim mCondStr$ = ""


            If AgL.StrCmp(AgL.PubDBName, "RVN") Then
                mCondStr = " Where 1 = 1 AND IC.Description IN ('EV','CNG') "
            Else
                mCondStr = " Where 1 = 1 "
            End If





            mCondStr = mCondStr & " AND Date(H.V_Date) Between " & AgL.Chk_Date(CDate(ReportFrm.FGetText(1)).ToString("s")) & " And " & AgL.Chk_Date(CDate(ReportFrm.FGetText(2)).ToString("s")) & " "
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.Site_Code", 3)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.V_Type", 4)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.BillToParty", 5)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.DocId", 6)




            mQry = "Select  H.DocID, H.V_Date, Agent.Name As AgentName , 
                    Party.Name As SaleToPartyName , Party.Address As SaleToPartyAddress , Party.Mobile As SaleToPartyMobile ,                     
                    H.Div_Code || H.Site_Code || '-' || H.V_Type || '-' || H.ManualRefNo as InvoiceNo, H.ManualRefNo, 
                    I.Specification as ItemSpecification, I.Description As ItemDesc,IG.Description as ItemGroupName, IC.Description as ItemCategoryDescription,  
                    Cast((Case When L.DiscountPer = 0 Then '' else L.DiscountPer End) as nVarchar) || (Case When L.AdditionalDiscountPer>0 Then '+' else '' End) || Cast((Case When L.AdditionalDiscountPer=0 Then '' else L.AdditionalDiscountPer End) as nVarchar) as DiscountPer, L.DiscountAmount + L.AdditionalDiscountAmount as Discount, L.Taxable_Amount, L.Net_Amount, L.Qty, L.Unit, L.Rate, L.Amount -(L.DiscountAmount + L.AdditionalDiscountAmount) as AmountExDiscount,
                    Case When IC.Description ='CNG' Then '225.8' Else '15.01' End AS HP,
                    Case When IC.Description ='CNG' Then 'CNG' Else 'Electric' End AS FuelUsed,
                    Case When IC.Description ='CNG' Then 'One' Else 'NA' End AS NoOfCylender,
                    Case When IC.Description ='CNG' Then '411' Else '418' End AS UnladenWeight,
                    Case When IC.Description ='CNG' Then '741' Else '748' End AS GrossWeight,
                    Case When IC.Description ='CNG' Then 'Three-Wheeler (Passenger)' Else '3-wheeled motor vehicle / motor tricycle' End AS TypeofBody,
                    Case When IC.Description ='CNG' Then '1990' Else '1991' End AS WheelBase,
                    L.Remark,L.Remarks1,L.Remarks2,L.Remarks3,L.Remarks4, L.Tax1+L.Tax2+L.Tax3+L.Tax4+L.Tax5 as TotalTax
                    FROM SaleInvoice H 
                    Left Join SaleInvoiceDetail L On H.DocID = L.DocID 
                    Left Join Item I On L.Item = I.Code 
                    Left Join ItemGroup IG On I.ItemGroup = IG.Code
                    Left Join ItemCategory IC On I.ItemCategory = IC.Code
                    Left Join viewHelpSubgroup Party On H.SaleToParty = Party.Code 
                    Left Join viewHelpSubGroup Agent On H.Agent = Agent.Code 
                    Left Join City On H.SaleToPartyCity = City.CityCode 
                    Left Join State On City.State = State.Code
                    LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type " & mCondStr & OrderByStr
            DsRep = AgL.FillData(mQry, AgL.GCn)

            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            ReportFrm.PrintReport(DsRep, RepName, RepTitle)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
#End Region

#Region "Size Wise Sale Report"
    Private Sub ProcSizeWiseSaleReport()
        Try

            If ReportFrm.FGetText(0) = "Summary With Design" Then
                RepName = "Apparel_SizeDesignWiseSaleReport"
            Else
                RepName = "Apparel_SizeWiseSaleReport"
            End If

            RepTitle = "Size Wise Sale Report"


            Dim mCondStr$ = ""


            mCondStr = " Where 1 = 1 And IT.Parent = '" & ItemTypeCode.TradingProduct & "' "


            mCondStr = mCondStr & " AND Date(H.V_Date) Between " & AgL.Chk_Date(CDate(ReportFrm.FGetText(1)).ToString("s")) & " And " & AgL.Chk_Date(CDate(ReportFrm.FGetText(2)).ToString("s")) & " "
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.BillToParty", 3)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemType", 4)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.Site_Code", 5)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.V_Type", 6)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemCategory", 7)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.Dimension1", 8)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.Dimension2", 9)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.Dimension3", 10)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.Dimension4", 11)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.Size", 12)



            mQry = " SELECT H.DocID, H.V_Date, 
                    I.Specification as ItemSpecification, I.Description As ItemDesc,IG.Description as ItemGroupName, IC.Description as ItemCategoryDescription,  
                    D1.Specification as D1Specification, D2.Specification as D2Specification, D3.Specification as D3Specification, 
                    Size.Specification as SizeSpecification, L.Qty                                       
                    FROM SaleInvoice H 
                    Left Join SaleInvoiceDetail L On H.DocID = L.DocID 
                    LEFT JOIN SaleInvoiceDetailSku LS ON l.DocID = LS.DocID AND l.Sr = LS.Sr 
                    LEFT JOIN Item D1 ON LS.Dimension1 = D1.Code 
                    LEFT JOIN Item D2 ON LS.Dimension2 = D2.Code 
                    LEFT JOIN Item D3 ON LS.Dimension3 = D3.Code 
                    LEFT JOIN Item Size ON LS.Size = Size.Code 
                    Left Join Item I On L.Item = I.Code 
                    Left Join ItemGroup IG On I.ItemGroup = IG.Code
                    Left Join ItemCategory IC On I.ItemCategory = IC.Code
                    Left Join ItemType IT on I.ItemType = IT.Code    
                    Left Join viewHelpSubgroup Party On H.SaleToParty = Party.Code 
                    Left Join viewHelpSubGroup Agent On H.Agent = Agent.Code 
                    Left Join City On H.SaleToPartyCity = City.CityCode 
                    Left Join State On City.State = State.Code
                    LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type " & mCondStr & OrderByStr
            DsRep = AgL.FillData(mQry, AgL.GCn)

            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            ReportFrm.PrintReport(DsRep, RepName, RepTitle)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
#End Region

#Region "Sale And Collection Summary"
    Private Sub ProcSaleAndCollectionSummary()
        Try



            Dim mCondStr$ = ""



            mCondStr = mCondStr & " AND Date(H.V_Date) Between " & AgL.Chk_Date(CDate(ReportFrm.FGetText(1)).ToString("s")) & " And " & AgL.Chk_Date(CDate(ReportFrm.FGetText(2)).ToString("s")) & " "
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.SaleToParty", 3)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.V_Type", 4)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.Div_Code", 5).Replace("''", "'")
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.Site_Code", 6).Replace("''", "'")










            Dim mMainQry As String = ""
            If ReportFrm.FGetText(0) = "Document Wise Detail" Then
                mMainQry = "SELECT '2' AS Srl, H.DocID, H.ManualRefNo as DocNo, H.V_Date, Vt.Description AS VType, Party.Name AS PartyName, Null As PaymentMode, H.Net_Amount AS Amount
                    FROM SaleInvoice H
                    LEFT JOIN ViewHelpSubgroup Party ON H.SaleToParty = Party.Code 
                    LEFT JOIN voucher_type vt ON H.V_Type = Vt.V_Type 
                    WHERE vt.NCat ='SR' " & mCondStr & "
                    
                    UNION All

                    SELECT '3' AS Srl, H.DocID, H.ManualRefNo as DocNo, H.V_Date, 'Collection' AS VType, Party.Name AS PartyName, PM.Description AS PaymentMode, HP.Amount AS Amount 
                    FROM SaleInvoice H
                    LEFT JOIN ViewHelpSubgroup Party ON H.SaleToParty = Party.Code 
                    LEFT JOIN SaleInvoicePayment HP ON H.DocID = HP.DocID 
                    LEFT JOIN PaymentMode PM ON HP.PaymentMode = PM.Code 
                    LEFT JOIN voucher_type vt ON H.V_Type = Vt.V_Type 
                    WHERE HP.DocID Is Not Null And vt.NCat ='SI' And IfNull(HP.Amount,0) > 0 " & mCondStr & "

                    UNION All

                    SELECT '3' AS Srl, H.DocID, H.ManualRefNo as DocNo, H.V_Date, 'Collection' AS VType, Party.Name AS PartyName, PM.Description || ' Refund' AS PaymentMode, HP.Amount AS Amount 
                    FROM SaleInvoice H
                    LEFT JOIN ViewHelpSubgroup Party ON H.SaleToParty = Party.Code 
                    LEFT JOIN SaleInvoicePayment HP ON H.DocID = HP.DocID 
                    LEFT JOIN PaymentMode PM ON HP.PaymentMode = PM.Code 
                    LEFT JOIN voucher_type vt ON H.V_Type = Vt.V_Type 
                    WHERE HP.DocID Is Not Null And vt.NCat ='SI' And IfNull(HP.Amount,0) < 0 " & mCondStr & " "

                mQry = "Select V.Srl, V.DocNo , V.V_Date, V.VType, V.PartyName, V.PaymentMode, V.Amount As Amount
                        From (" & mMainQry & ") as V     
                        Where V.Srl In ('2','3')
                        Order By V_Date, Srl"

                RepName = "SaleAndCollectionDetail_Thermal"
            Else
                mMainQry = " SELECT '1' AS Srl, H.DocID, H.ManualRefNo as DocNo, H.V_Date, Vt.Description AS VType, Party.Name AS PartyName, Null PaymentMode, H.Net_Amount AS Amount
                    FROM SaleInvoice H
                    LEFT JOIN ViewHelpSubgroup Party ON H.SaleToParty = Party.Code 
                    LEFT JOIN voucher_type vt ON H.V_Type = Vt.V_Type 
                    WHERE vt.NCat ='SI' " & mCondStr & "
                    
                    UNION All

                    Select '1' AS Srl, H.DocID, H.ManualRefNo as DocNo, H.V_Date, Vt.Description AS VType, Party.Name AS PartyName, Null As PaymentMode, H.Net_Amount AS Amount
                    From SaleInvoice H
                    Left Join ViewHelpSubgroup Party ON H.SaleToParty = Party.Code 
                    Left Join voucher_type vt ON H.V_Type = Vt.V_Type 
                    Left Join SubGroup Sg On H.BillToParty = Sg.SubCode
                    WHERE vt.NCat ='SR' " & mCondStr & "

                    UNION All

                    SELECT '2' AS Srl, H.DocID, Null as DocNo, H.V_Date, 'Net Sale' AS VType, Party.Name AS PartyName, Null PaymentMode, H.Net_Amount AS Amount
                    FROM SaleInvoice H
                    LEFT JOIN ViewHelpSubgroup Party ON H.SaleToParty = Party.Code 
                    LEFT JOIN voucher_type vt ON H.V_Type = Vt.V_Type 
                    WHERE vt.NCat In ('SI','SR') " & mCondStr & "
                    
                    UNION All

                    SELECT '3' AS Srl, H.DocID, H.ManualRefNo as DocNo, H.V_Date, 'Collection' AS VType, Party.Name AS PartyName, PM.Description AS PaymentMode, HP.Amount AS Amount 
                    FROM SaleInvoice H
                    LEFT JOIN ViewHelpSubgroup Party ON H.SaleToParty = Party.Code 
                    LEFT JOIN SaleInvoicePayment HP ON H.DocID = HP.DocID 
                    LEFT JOIN PaymentMode PM ON HP.PaymentMode = PM.Code 
                    LEFT JOIN voucher_type vt ON H.V_Type = Vt.V_Type 
                    WHERE HP.DocID Is Not Null And vt.NCat ='SI' And IfNull(HP.Amount,0) > 0 " & mCondStr & "

                    UNION All

                    SELECT '3' AS Srl, H.DocID, H.ManualRefNo as DocNo, H.V_Date, 'Collection' AS VType, Party.Name AS PartyName, PM.Description || ' Refund' AS PaymentMode, HP.Amount AS Amount 
                    FROM SaleInvoice H
                    LEFT JOIN ViewHelpSubgroup Party ON H.SaleToParty = Party.Code 
                    LEFT JOIN SaleInvoicePayment HP ON H.DocID = HP.DocID 
                    LEFT JOIN PaymentMode PM ON HP.PaymentMode = PM.Code 
                    LEFT JOIN voucher_type vt ON H.V_Type = Vt.V_Type 
                    WHERE HP.DocID Is Not Null And vt.NCat ='SI' And IfNull(HP.Amount,0) < 0 " & mCondStr & "

                    UNION All

                    SELECT '4' AS Srl, H.DocID, Null as DocNo, H.V_Date, 'Net Cash' AS VType, Party.Name AS PartyName, Null AS PaymentMode, HP.Amount AS Amount 
                    FROM SaleInvoice H
                    LEFT JOIN ViewHelpSubgroup Party ON H.SaleToParty = Party.Code 
                    LEFT JOIN SaleInvoicePayment HP ON H.DocID = HP.DocID 
                    LEFT JOIN PaymentMode PM ON HP.PaymentMode = PM.Code 
                    LEFT JOIN voucher_type vt ON H.V_Type = Vt.V_Type 
                    WHERE HP.DocID Is Not Null And vt.NCat ='SI' 
                    And Pm.Code = 'Cash' " & mCondStr & " 
                    
                    UNION ALL 

                    Select '4' AS Srl, H.DocID, Null as DocNo, H.V_Date, 'Net Cash' AS VType, Party.Name AS PartyName, Null As PaymentMode, H.Net_Amount AS Amount
                    From SaleInvoice H
                    Left Join ViewHelpSubgroup Party ON H.SaleToParty = Party.Code 
                    Left Join voucher_type vt ON H.V_Type = Vt.V_Type 
                    Left Join SubGroup Sg On H.BillToParty = Sg.SubCode
                    WHERE vt.NCat ='SR' And Sg.Nature = 'Cash'  " & mCondStr & ""


                mQry = "Select V.Srl , V.V_Date, V.VType, V.PaymentMode, Sum(V.Amount) As Amount, Cast(Min(Cast(V.DocNo as Integer)) as Char) as MinDocNo, Cast(Max(Cast(V.DocNo as Integer)) as Char) as MaxDocNo
                        From (" & mMainQry & ") as V 
                        Group by V.V_Date, V.Srl, V.VType, V.PaymentMode 
                        Order By V.V_Date, V.Srl"

                RepName = "SaleAndCollectionSummary_Thermal"
            End If

            RepTitle = "Sale & Collection Summary"












            mQry = AgL.GetBackendBasedQuery(mQry)
            DsRep = AgL.FillData(mQry, AgL.GCn)

            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            ReportFrm.PrintReport(DsRep, RepName, RepTitle)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
#End Region

#Region "Pending to Deliver Report"
    Private Sub ProcPendingToDeliverReport()
        Try

            Dim mCondStr$ = ""

            mCondStr = mCondStr & " AND Date(H.V_Date) Between " & AgL.Chk_Date(CDate(ReportFrm.FGetText(1)).ToString("s")) & " And " & AgL.Chk_Date(CDate(ReportFrm.FGetText(2)).ToString("s")) & " "
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L1.Catalog", 3)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.ItemCategory", 4)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.Div_Code", 5).Replace("''", "'")
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("C.Site_Code", 6).Replace("''", "'")


            mQry = "                   
                    SELECT H.DocID, H.ManualRefNo, H.V_Date, H.SaleToParty, IfNull(H.SaleToPartyName,'') SaleToPartyName, IfNull(H.SaleToPartyMobile,'') SaleToPartyMobile, 
                    I.Specification AS ItemSpecification, IG.Description AS ItemGroupDescription, 
                    IC.Description AS ItemCategoryDescription, L.Qty, S.Description AS ItemStateDescription, C.Description, CS.Name AS SiteName
                    FROM SaleInvoice H
                    LEFT JOIN SaleInvoiceDetail L ON H.DocID = L.DocID 
                    LEFT JOIN (SELECT DocID, Max(Catalog) AS Catalog FROM SaleInvoiceDetail GROUP BY DocID) AS L1 ON H.DocID = L1.DocID
                    LEFT JOIN Item I ON L.Item = I.Code 
                    LEFT JOIN Item IG ON I.ItemGroup = IG.Code 
                    LEFT JOIN Item IC ON I.ItemCategory = IC.Code 
                    LEFT JOIN catalog C ON L1.Catalog = C.Code
                    LEFT JOIN SiteMast CS ON C.Site_Code = Cs.Code 
                    LEFT JOIN Item S ON L.ItemState = S.Code 
                    WHERE L.ItemState ='OOStock'
                    
                     " & mCondStr


            If ReportFrm.FGetText(0) = "Detail" Then
                mQry = "Select V.DocID, V.ManualRefNo, V.V_Date, V.SaleToParty, V.SaleToPartyName, 
                        V.SaleToPartyMobile, V.ItemSpecification, V.ItemGroupDescription, V.ItemCategoryDescription, 
                        V.Qty, V.ItemStateDescription, V.Description, V.SiteName
                        From (" & mQry & ") as V                             
                        ORDER BY V.V_Date, CAST(V.ManualRefNo AS INTEGER ), V.ItemSpecification, V.ItemGroupDescription, V.ItemCategoryDescription "

                RepName = "PendingToDeliver"
            End If

            RepTitle = "Pending To Deliver Report"



            mQry = AgL.GetBackendBasedQuery(mQry)
            DsRep = AgL.FillData(mQry, AgL.GCn)

            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            ReportFrm.PrintReport(DsRep, RepName, RepTitle)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
#End Region

#Region "Size Wise Job Receive Report"
    Private Sub ProcSizeWiseJobReport(NCatStr As String)
        Try
            If ReportFrm.FGetText(0) = "Summary With Design" Then
                RepName = "Apparel_SizeDesignWiseJobReceiveReport" : RepTitle = "Size Wise Job Receive Report"
            Else
                RepName = "Apparel_SizeWiseJobReceiveReport" : RepTitle = "Size Wise Job Receive Report"
            End If

            Dim mCondStr$ = ""

            mCondStr = " Where 1 = 1 And IT.Parent = '" & ItemTypeCode.TradingProduct & "' "

            If AgL.XNull(ReportFrm.FGetCode(3)) = "" Then
                MsgBox("Process is mandatory")
                Exit Sub
            End If

            mCondStr = mCondStr & " And L.SubRecordType Is Null And Vt.NCat In ('" & NCatStr.Replace(",", "','") & "') "
            mCondStr = mCondStr & " AND Date(H.V_Date) Between " & AgL.Chk_Date(CDate(ReportFrm.FGetText(1)).ToString("s")) & " And " & AgL.Chk_Date(CDate(ReportFrm.FGetText(2)).ToString("s")) & " "
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.Process", 3)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.Vendor", 4)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemType", 5)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemCategory", 6)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.Dimension1", 7)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.Dimension2", 8)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.Dimension3", 9)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.Dimension4", 10)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.Size", 11)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.Site_Code", 12)


            mQry = " SELECT H.DocID, H.V_Date, 
                    I.Specification as ItemSpecification, I.Description As ItemDesc,IG.Description as ItemGroupName, IC.Description as ItemCategoryDescription,  
                    D1.Specification as D1Specification, D2.Specification as D2Specification, D3.Specification as D3Specification, 
                    Size.Specification as SizeSpecification, L.Qty                                       
                    FROM PurchInvoice H 
                    Left Join PurchInvoiceDetail L On H.DocID = L.DocID 
                    LEFT JOIN PurchInvoiceDetailSku LS ON l.DocID = LS.DocID AND l.Sr = LS.Sr 
                    LEFT JOIN Item D1 ON LS.Dimension1 = D1.Code 
                    LEFT JOIN Item D2 ON LS.Dimension2 = D2.Code 
                    LEFT JOIN Item D3 ON LS.Dimension3 = D3.Code 
                    LEFT JOIN Item Size ON LS.Size = Size.Code 
                    Left Join Item I On L.Item = I.Code 
                    Left Join ItemGroup IG On I.ItemGroup = IG.Code
                    Left Join ItemCategory IC On I.ItemCategory = IC.Code
                    Left Join ItemType IT on I.ItemType = IT.Code    
                    Left Join viewHelpSubgroup Party On H.Vendor = Party.Code                                        
                    LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type " & mCondStr & OrderByStr
            DsRep = AgL.FillData(mQry, AgL.GCn)

            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            ReportFrm.PrintReport(DsRep, RepName, RepTitle)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
#End Region

#Region "Size Wise Stock Report"
    Private Sub ProcSizeWiseStockReport()
        Try

            Dim mCondStr$ = ""


            mCondStr = " Where 1 = 1 And IT.Parent='" & ItemTypeCode.TradingProduct & "'   "


            mCondStr = mCondStr & " AND Date(l.V_Date) <= " & AgL.Chk_Date(CDate(ReportFrm.FGetText(2)).ToString("s")) & " "
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemType", 3)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemCategory", 4)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.Dimension1", 5)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.Dimension2", 6)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.Dimension3", 7)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.Dimension4", 8)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.Size", 9)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Site_Code", 10)

            If ReportFrm.FGetText(0) = "Stock In Hand & Process" Or ReportFrm.FGetText(0) = "Stock In Hand" Then

                mQry = " SELECT 'H'as LocationType, L.Item, Max(I.Description) AS ItemDesc, Max(IC.Description) as ItemCategory, 
                    Max(D1.Specification) as Dimension1, Max(D2.Specification) as Dimension2, Max(Size.Specification) as Size,  
                    IfNull(Sum(L.Qty_Rec),0) - IfNull(Sum(L.Qty_Iss),0) As StockQty, Max(Process.Name) as ProcessName, Max(Godown.Name) as GodownName
                    FROM Stock L 
                    LEFT JOIN Item  I On L.Item = I.Code
                    Left Join Item IC On I.ItemCategory = IC.Code 
                    Left Join ItemType IT On I.ItemType = IT.Code 
                    Left Join Item D1 On I.Dimension1 = D1.Code 
                    Left Join Item D2 On I.Dimension2 = D2.Code 
                    Left Join Item D3 On I.Dimension3 = D3.Code 
                    Left Join Item D4 On I.Dimension4 = D4.Code 
                    Left Join Item Size On I.Size = size.Code 
                    Left Join Subgroup Process On L.Process = Process.subcode
                    Left Join Subgroup Godown On  L.Godown = Godown.subcode"
                mQry = mQry & mCondStr
                mQry = mQry & " GROUP BY L.Item "
                If ReportFrm.FGetText(1) = "Category, Colour & Process" Then
                    mQry = mQry & ", L.Process "
                End If
                If ReportFrm.FGetText(1) = "Category, Colour & Location" Then
                    mQry = mQry & ", L.Process, L.Godown "
                End If
                mQry = mQry & " HAVING IfNull(Sum(L.Qty_Rec), 0) - IfNull(Sum(L.Qty_Iss), 0) <> 0 "
            End If

            If ReportFrm.FGetText(0) = "Stock In Hand & Process" Then
                mQry = mQry & " Union All "
            End If

            If ReportFrm.FGetText(0) = "Stock In Hand & Process" Or ReportFrm.FGetText(0) = "Stock In Process" Then

                mQry = mQry & " SELECT 'P' as LocationType, L.Item, Max(I.Description) AS ItemDesc, Max(IC.Description) as ItemCategory, 
                    Max(D1.Specification) as Dimension1, Max(D2.Specification) as Dimension2, Max(Size.Specification) as Size,  
                    IfNull(Sum(L.Qty_Rec),0) - IfNull(Sum(L.Qty_Iss),0) As StockQty, Max(Process.Name) as ProcessName, Max(Person.Name) as GodownName
                    FROM StockProcess L 
                    LEFT JOIN Item  I On L.Item = I.Code
                    Left Join Item IC On I.ItemCategory = IC.Code 
                    Left Join ItemType IT On I.ItemType = IT.Code 
                    Left Join Item D1 On I.Dimension1 = D1.Code 
                    Left Join Item D2 On I.Dimension2 = D2.Code 
                    Left Join Item D3 On I.Dimension3 = D3.Code 
                    Left Join Item D4 On I.Dimension4 = D4.Code 
                    Left Join Item Size On I.Size = size.Code 
                    Left Join Subgroup Process On L.Process = Process.subcode
                    Left Join viewHelpSubgroup Person On  L.Subcode = Person.Code"
                mQry = mQry & mCondStr
                mQry = mQry & " GROUP BY L.Item "
                If ReportFrm.FGetText(1) = "Category, Colour & Process" Then
                    mQry = mQry & ", L.Process "
                End If
                If ReportFrm.FGetText(1) = "Category, Colour & Location" Then
                    mQry = mQry & ", L.Process, L.Godown "
                End If
                mQry = mQry & " HAVING IfNull(Sum(L.Qty_Rec), 0) - IfNull(Sum(L.Qty_Iss), 0) <> 0 "

            End If

            DsRep = AgL.FillData(mQry, AgL.GCn)

            If ReportFrm.FGetText(1) = "Category, Colour & Process" Then
                RepName = "Apparel_SizeWiseStockWithProcessReport" : RepTitle = "Size Wise Stock Report"
            ElseIf ReportFrm.FGetText(1) = "Category, Colour & Location" Then
                RepName = "Apparel_SizeWiseStockWithLocationReport" : RepTitle = "Size Wise Stock Report"
            Else
                RepName = "Apparel_SizeWiseStockReport" : RepTitle = "Size Wise Stock Report"
            End If



            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            ReportFrm.PrintReport(DsRep, RepName, RepTitle)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
#End Region


#Region "Size Wise Rate List"
    Private Sub ProcSizeWiseRateList()
        Try
            RepName = "Apparel_SizeWiseRateList" : RepTitle = "Size Wise Rate List"


            Dim mCondStr$ = ""


            mCondStr = " Where 1 = 1 And IT.Parent='" & ItemTypeCode.TradingProduct & "'   "


            If AgL.XNull(ReportFrm.FGetCode(6)) = "" Then
                mCondStr = mCondStr & " And IfNull(H.V_type,'RTL')='RTL' "
            End If

            'mCondStr = mCondStr & " AND Date(l.V_Date) <= " & AgL.Chk_Date(CDate(ReportFrm.FGetText(1)).ToString("s")) & " "
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Process", 1)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("IC.ItemType", 2)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.ItemCategory", 3)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Dimension1", 4)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Dimension2", 5)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Dimension3", 6)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Dimension4", 7)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Size", 8)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Site_Code", 9)






            mQry = " SELECT L.Item, I.Description AS ItemDesc, IC.Description as ItemCategory, 
                    D1.Specification as Dimension1, Size.Specification as Size,  " &
                    " L.Rate AS StockQty " &
                    " FROM RateListDetail L " &
                    " Left Join RateList H on L.Code = H.Code " &
                    " LEFT JOIN Item  I ON L.Item = I.Code " &
                    " Left Join Item IC On L.ItemCategory = IC.Code " &
                    " Left Join ItemType IT On IC.ItemType = IT.Code " &
                    " Left Join Item D1 On L.Dimension1 = D1.Code " &
                    " Left Join Item D2 On L.Dimension2 = D2.Code " &
                    " Left Join Item D3 On L.Dimension3 = D3.Code " &
                    " Left Join Item D4 On L.Dimension4 = D4.Code " &
                    " Left Join Item Size On L.Size = size.Code " & mCondStr



            DsRep = AgL.FillData(mQry, AgL.GCn)

            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            ReportFrm.PrintReport(DsRep, RepName, RepTitle)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
#End Region

#Region "Size Wise Consumption List"
    Private Sub ProcSizeWiseConsumptionList()
        Try
            RepName = "Apparel_SizeWiseConsumptionList" : RepTitle = "Size Wise Consumption List"

            Dim mCondStr$ = ""

            mCondStr = " Where 1 = 1 And IT.Parent='" & ItemTypeCode.TradingProduct & "' And H.RawMaterial Is Not Null   "


            If AgL.XNull(ReportFrm.FGetCode(3)) = "" Then
                mCondStr = mCondStr & " And H.Dimension3 Is Null "
            End If

            'mCondStr = mCondStr & " AND Date(l.V_Date) <= " & AgL.Chk_Date(CDate(ReportFrm.FGetText(1)).ToString("s")) & " "

            mCondStr = mCondStr & ReportFrm.GetWhereCondition("IC.ItemType", 1)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.ItemCategory", 2)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.RawMaterial", 4)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Size", 5)





            mQry = " SELECT L.Item,  IC.Description as ItemCategory, 
                    D4.Specification as Dimension4, Size.Specification as Size,  " &
                    " L.Qty AS StockQty " &
                    " FROM BomDetail L " &
                    " Left Join Item H on L.Code = H.Code " &
                    " Left Join Item IC On H.ItemCategory = IC.Code " &
                    " Left Join ItemType IT On IC.ItemType = IT.Code " &
                    " Left Join Item D4 On H.Rawmaterial = D4.Code " &
                    " Left Join Item Size On H.Size = size.Code " & mCondStr



            DsRep = AgL.FillData(mQry, AgL.GCn)

            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

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
                strGrpFldDesc = "Sg.Name || ',' || IfNull(C.CityName,'')"
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

            mCondStr = mCondStr & " AND Date(H.V_Date) Between " & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & " And " & AgL.Chk_Date(CDate(ReportFrm.FGetText(1)).ToString("s")) & " "
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Item", 3)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.Vendor", 4)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.Site_Code", 5)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.V_Type", 6)

            mQry = " SELECT " & strGrpFld & " as GrpField, " & strGrpFldDesc & " as GrpFieldDesc, " & strGrpFldHead & " as GrpFieldHead, " &
                        " L.DocId, L.Qty, L.Unit, L.Net_Amount, " &
                        " I.Description AS ItemDesc, H.V_Date, H.ManualRefNo, Sg.DispName || ',' || IfNull(C.CityName,'') As VendorName, L.Remark " &
                        " FROM " & LineTable & " L " &
                        " LEFT JOIN " & HeaderTable & " H ON L.DocId = H.DocId " &
                        " LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type " &
                        " LEFT JOIN Item I ON L.Item = I.Code " &
                        " LEFT JOIN SubGroup Sg On H.Vendor = Sg.SubCode " &
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
            mCondStr = mCondStr & " AND Date(L.V_Date) <= '" & ReportFrm.FGetText(0) & "' "
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Item", 1)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemGroup", 2)

            If ReportFrm.FGetText(3) <> "Both" Then
                mCondStr = mCondStr & " And I.Status = '" & ReportFrm.FGetText(3) & "'"
            End If

            mQry = " SELECT L.Item, Max(I.Description) AS ItemDesc, Max(I.ReorderLevel) AS ReorderLevel, " &
                    " IfNull(Sum(L.Qty_Rec),0) - IfNull(Sum(L.Qty_Iss),0) AS StockQty " &
                    " FROM Stock L " &
                    " LEFT JOIN Item  I ON L.Item = I.Code " & mCondStr &
                    " GROUP BY L.Item " &
                    " HAVING(IfNull(Sum(L.Qty_Rec), 0) - IfNull(Sum(L.Qty_Iss), 0) <= Max(I.ReorderLevel)) "
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

            mQry = " SELECT L.ReferenceDocID, L.ReferenceDocIDSr, Max(I.Description) As ItemDesc, " &
                    " IfNull(Sum(L.Qty_Rec),0) - IfNull(Sum(L.Qty_Iss),0) AS StockQty, " &
                    " Max(L.ExpiryDate) As ExpiryDate " &
                    " FROM Stock L  " &
                    " LEFT JOIN Item I ON L.Item = I.Code " & mCondStr &
                    " GROUP BY L.ReferenceDocID, L.ReferenceDocIDSr " &
                    " HAVING IfNull(Sum(L.Qty_Rec), 0) - IfNull(Sum(L.Qty_Iss), 0) > 0 " &
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
            mCondStr = mCondStr & " AND Date(H.V_Date) Between " & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & " And " & AgL.Chk_Date(CDate(ReportFrm.FGetText(1)).ToString("s")) & " "
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Item", 2)

            mQry = " SELECT H.V_Date, I.Description As ItemDesc, IfNull(VStock.CurrentStock,0) As StockQty " &
                    " FROM PurchIndent H  " &
                    " LEFT JOIN PurchIndentDetail L ON H.DocID = L.DocId " &
                    " LEFT JOIN ( " &
                    "   Select S.Item, IfNull(Sum(S.Qty_Rec),0) - IfNull(Sum(S.Qty_Iss),0) As CurrentStock " &
                    "   From Stock S Group By S.Item " &
                    " ) As VStock On L.Item = VStock.Item " &
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

#Region "Current Stock Report"
    Private Sub ProcCurrentStockReport()
        Try
            RepName = "Med_CurrentStockReport" : RepTitle = "Current Stock Report"

            Dim mCondStr$ = "", mCondstr1$ = ""

            mCondStr = "  And H.Site_Code ='" & AgL.PubSiteCode & "' "
            mCondStr = mCondStr & " AND Date(H.V_Date) <= '" & ReportFrm.FGetText(0) & "'  "
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.Item", 1)

            mCondstr1 = " And Stock.Site_Code ='" & AgL.PubSiteCode & "' "
            mCondstr1 = mCondstr1 & " AND Date(Stock.V_Date) <= '" & ReportFrm.FGetText(0) & "'  "
            mCondstr1 = mCondstr1 & ReportFrm.GetWhereCondition("Stock.Item", 1)


            If ReportFrm.GetWhereCondition("ItemRrportingGroup", 2) <> "" Then
                mQry = " Select '''' ||  replace(ItemList,',',''',''')  || ''''  From ItemReportingGroup Where 1=1 " & ReportFrm.GetWhereCondition("Code", 2)
                mCondStr = mCondStr & " And H.Item In (" & AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar & ")"
                mCondstr1 = mCondstr1 & " And Stock.Item In (" & AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar & ")"
            End If




            mQry = "SELECT I.Code, I.Description, H.LotNo AS LotNo, " &
                    "IfNull(H.Qty_Rec, 0) - IfNull(SAdj.AdjQty, 0) AS [Bal.Qty], I.Unit,  " &
                    "H.V_Type || '-' || H.RecId As StockInNo, H.V_Date AS Purchase_Date,   " &
                    "H.Sale_Rate, H.MRP, H.ExpiryDate,   " &
                    "   I.ManualCode,   " &
                    "I.SalesTaxPostingGroup, H.MeasureUnit, " &
                    "         H.MeasurePerPcs,  Sg.Name AS Vendor,   " &
                    "        U.DecimalPlaces As QtyDecimalPlaces, U1.DecimalPlaces As MeasureDecimalPlaces,   " &
                    "       I.BillingOn as BillingType,H.DocId As StockInDocID, H.Sr As StockInDocIDSr,    " &
                    "      (H.Landed_Value/H.Qty_Rec)  as PurchaseRate, (H.Landed_Value/H.Qty_Rec)*(IfNull(H.Qty_Rec, 0) - IfNull(SAdj.AdjQty, 0)) as StockAmount  " &
                    "     FROM Stock H    " &
                    "    LEFT JOIN (  " &
                    "    			SELECT StockInDocID, StockInSr, Sum(AdjQty) AS AdjQty    " &
                    "    			FROM StockAdj   " &
                    "    			LEFT JOIN Stock ON StockAdj.StockOutDocID = Stock.DocID AND StockAdj.StockOutSr = Stock.Sr    " &
                    "    			WHERE 1=1 " & mCondstr1 &
                    "    			GROUP BY StockInDocID, StockInSr    " &
                    "             	) AS SAdj ON H.DocID = SAdj.StockInDocID AND H.Sr = Sadj.StockInSr   " &
                    "    LEFT JOIN Item I ON H.Item = I.Code   " &
                    "    LEFT JOIN SubGroup Sg ON H.SubCode = Sg.SubCode    " &
                    "    LEFT JOIN Unit U On I.Unit = U.Code   " &
                    "    LEFT JOIN Unit U1 On I.MeasureUnit = U1.Code   " &
                    "    Where IfNull(H.Qty_Rec, 0) - IfNull(SAdj.AdjQty, 0) > 0  And IfNull(H.Qty_Rec, 0)>0   " & mCondStr &
                    "  And H.Site_Code ='" & AgL.PubSiteCode & "' "


            DsRep = AgL.FillData(mQry, AgL.GCn)

            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records to Print!")

            ReportFrm.PrintReport(DsRep, RepName, RepTitle)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
#End Region

#Region "Bill Wise Profitabilty"
    Private Sub ProcBillWiseProfitabilty()
        Try
            RepName = "Trade_BillWiseProfitabilty" : RepTitle = "Bill Wise Profitabilty"


            Dim mCondStr$ = ""
            Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"

            mCondStr = " Where 1 = 1 "
            mCondStr = mCondStr & " AND Date(H.V_Date) Between " & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & " And " & CDate(ReportFrm.FGetText(1)).ToString("s") & " "
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.SaleToParty", 2)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Item", 3)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.DocID", 4)

            If ReportFrm.GetWhereCondition("ItemRrportingGroup", 5) <> "" Then
                mQry = " Select '''' ||  replace(ItemList,',',''',''')  || ''''  From ItemReportingGroup Where 1=1 " & ReportFrm.GetWhereCondition("Code", 5)
                mCondStr = mCondStr & " And L.Item In (" & AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar & ")"
            End If

            mQry = " SELECT H.DocID , H.V_Type, H.V_Date, H.ManualRefNo, Sg.Name As SaleToPartyName, H.SaleToParty , " &
                    " L.Sr, L.Item , L.Qty, L.Unit, L.Rate, L.Amount, L.Landed_Value AS SaleValue, " &
                    " I.Description AS ItemDesc , PCD.Landed_Value/PCD.Qty * L.Qty AS PurchaseValue ,  " &
                    " L.Landed_Value - PCD.Landed_Value/PCD.Qty * L.Qty AS Profit,  " &
                    " CASE WHEN IfNull(PCD.Landed_Value/PCD.Qty * L.Qty,0) > 0 THEN ( L.Landed_Value - PCD.Landed_Value/PCD.Qty * L.Qty) * 100 / ( PCD.Landed_Value/PCD.Qty * L.Qty) ELSE 0 END AS ProfitPer " &
                    " FROM SaleInvoice  H " &
                    " LEFT JOIN SubGroup Sg On H.SaleToParty = Sg.SubCode " &
                    " LEFT JOIN SaleInvoiceDetail L ON L.DocId = H.DocID  " &
                    " LEFT JOIN Item I ON I.Code = L.Item  " &
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

            mCondStr = " Where Date(H.V_Date) <= '" & ReportFrm.FGetText(0) & "' "
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.SubCode", 2)
            mCondStr = mCondStr & " And  H.Site_Code = " & AgL.Chk_Text(AgL.PubSiteCode) & " "
            mCondStr = mCondStr & " And  Sg.Nature In ('" & ClsMain.SubGroupNature.Customer & "') "


            mQry = "SELECT VRep.SubCode, Max(VRep.PartyName) AS PartyName, Max(VRep.TotalBal) AS TotalBal, Sum(VRep.NetBalAmount) AS BalAboveDays, '" & ReportFrm.FGetText(1) & "' AS OnOfDays " &
                    " FROM " &
                    " ( " &
                    " SELECT VMain.*,    SUM(NetAmount) OVER( PARTITION BY SubCode ORDER BY V_Date DESC , DocId ) sum_stock1,  " &
                    " CASE WHEN VMain.NetAmount = 0 THEN  VMain.TotalBal - SUM(NetAmount) OVER( PARTITION BY SubCode ORDER BY V_Date DESC , DocId )  ELSE VMain.NetAmount END AS NetBalAmount " &
                    " FROM  ( " &
                    " SELECT P.PartyName, P.TotalBal, SD.*,  CASE WHEN P.TotalBal > SD.Sum_Dr THEN SD.AmtDr ELSE 0 END AS NetAmount " &
                    " FROM " &
                    " ( " &
                    " SELECT SG.SubCode AS SubCode, max(SG.Name) AS PartyName, IfNull(sum(H.AmtDr),0)- IfNull(sum(H.AmtCR),0) AS TotalBal " &
                    " FROM Ledger H   " &
                    " LEFT JOIN SubGroup SG  ON SG.SubCode = H.SubCode " & mCondStr &
                    " GROUP BY SG.SubCode  " &
                    " Having IfNull(sum(H.AmtDr),0)- IfNull(sum(H.AmtCR),0)  > 0 " &
                    " ) As P " &
                    " LEFT JOIN " &
                    " ( " &
                    " SELECT s.*, SUM(AmtDr) OVER( PARTITION BY SubCode ORDER BY V_Date DESC, DocId  ) Sum_Dr " &
                    " FROM " &
                    " ( " &
                    " SELECT H.DocID, H.RecId, H.V_TYpe, H.V_Date, H.AmtDr , H.SubCode " &
                    " FROM Ledger H  " &
                    " LEFT JOIN SubGroup SG  ON SG.SubCode = H.SubCode " & mCondStr &
                    " AND IfNull(H.AmtDr,0) <> 0 " &
                    " )  s  " &
                    " ) SD ON SD.SubCode = p.SubCode  AND IfNull(P.TotalBal,0)  > IfNull(SD.Sum_Dr,0)  - IfNull(SD.AmtDr,0) " &
                    " ) VMain " &
                    " ) VRep " &
                    " WHERE DateDiff(Day,VRep.V_Date," & AgL.Chk_Text(ReportFrm.FGetText(0)) & " ) >=  " & NoofDays & " " &
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

    '#Region "Concur Ledger"

    '    Public Function FunConcurLedger(Conn As Object) As DataSet
    '        Dim mCondStr$ = ""
    '        Dim mCondStrOp$ = ""
    '        Dim NoofDays As Integer = 0
    '        Dim DtSubcode As DataTable
    '        Dim iSubcode As Integer
    '        Dim DtDivision As DataTable
    '        Dim iDivision As Integer
    '        Dim DtDr As DataTable
    '        Dim DtCr As DataTable
    '        Dim DtTemp As DataTable
    '        Dim DrRecordCount As Integer
    '        Dim CrRecordCount As Integer
    '        Dim LoopLimit As Integer
    '        Dim I As Integer, J As Integer
    '        Dim iDr As Integer
    '        Dim iCr As Integer
    '        Dim DrSr As Integer
    '        Dim CrSr As Integer
    '        Dim ConcurSr As Integer = -1
    '        Dim FirstConcurSr As Integer = -1
    '        Dim mSubcode As String
    '        Dim mDivision As String
    '        Dim mLastChuktiAmount As Double
    '        Dim mTotalDr As Double
    '        Dim mTotalCr As Double
    '        Dim DtSubcodeBalances As DataTable


    '        Try

    '            If AgL.XNull(ReportFrm.FGetText(0)).ToString.ToUpper = "Format 2".ToUpper Then
    '                If ClsMain.FDivisionNameForCustomization(4) = "X DEVI" Then
    '                    RepName = "ConcurLedgerLandscape_Devi" : RepTitle = "Chukti Ledger"
    '                ElseIf ClsMain.FDivisionNameForCustomization(6) = "SADHVI" Then
    '                    RepName = "ConcurLedgerLandscape_Sadhvi" : RepTitle = "Chukti Ledger"
    '                Else
    '                    RepName = "ConcurLedgerLandscape" : RepTitle = "Chukti Ledger"
    '                End If
    '            ElseIf AgL.XNull(ReportFrm.FGetText(0)).ToString.ToUpper = "Without Interest Portrait".ToUpper Then
    '                RepName = "ConcurLedgerWithoutInterest" : RepTitle = "Chukti Ledger"
    '            Else
    '                RepName = "ConcurLedger" : RepTitle = "Chukti Ledger"

    '            End If

    '            If Val(ReportFrm.FGetText(2)) <> 0 Then
    '                NoofDays = Val(ReportFrm.FGetText(2))
    '            Else
    '                MsgBox("Please Enter Valid No. Of Days.") : FunConcurLedger = Nothing : Exit Function
    '            End If




    '            Try
    '                mQry = "Drop Table #TempTblDr "
    '                AgL.Dman_ExecuteNonQry(mQry, Conn)
    '            Catch ex As Exception
    '            End Try



    '            mQry = "Create Temporary Table #TempTblDr 
    '                    (
    '                        DrDocID nVarchar(21),
    '                        DrDivision nVarchar(1),
    '                        DrSubcode nVarchar(10),
    '                        DrSr Integer,
    '                        DrDate DateTime,
    '                        DrDocNo nVarchar(21)  Collate NoCase,
    '                        DrAmount Float Default 0,
    '                        DrTaxableAmount Float Default 0,
    '                        DrTaxAmount Float Default 0,
    '                        DrDays Integer,
    '                        DrInterest Float,
    '                        DrCumAmount Float,
    '                        DrTotal Float,
    '                        DrNarration nVarchar(100)
    '                    )
    '                    "

    '            AgL.Dman_ExecuteNonQry(mQry, Conn)


    '            Try
    '                mQry = "Drop Table #TempTblCr "
    '                AgL.Dman_ExecuteNonQry(mQry, Conn)
    '            Catch ex As Exception
    '            End Try


    '            mQry = "Create Temporary Table #TempTblCr 
    '                    (
    '                        CrDocID nVarchar(21),
    '                        CrDivision nVarchar(1),
    '                        CrSubcode nVarchar(10),
    '                        CrSr Integer,
    '                        CrDate DateTime,
    '                        CrDocNo nVarchar(21)  Collate NoCase,
    '                        CrAmount Float Default 0,
    '                        CrTaxableAmount Float Default 0,
    '                        CrTaxAmount Float Default 0,
    '                        CrDays Integer,
    '                        CrInterest Float,
    '                        CrCumAmount Float,
    '                        CrTotal Float,
    '                        CrNarration nVarchar(100)                        
    '                    )
    '                    "

    '            AgL.Dman_ExecuteNonQry(mQry, Conn)


    '            Try
    '                mQry = "Drop Table #TempTblDrCr "
    '                AgL.Dman_ExecuteNonQry(mQry, Conn)
    '            Catch ex As Exception
    '            End Try


    '            mQry = "Create Temporary Table #TempTblDrCr 
    '                    (
    '                        DrDocID nVarchar(21),
    '                        DrDivision nVarchar(1),
    '                        DrSubcode nVarchar(10),
    '                        DrSr Integer,
    '                        DrDate DateTime,
    '                        DrDocNo nVarchar(21)  Collate NoCase,
    '                        DrAmount Float Default 0,
    '                        DrTaxableAmount Float Default 0,
    '                        DrTaxAmount Float Default 0,
    '                        DrDays Integer,
    '                        DrInterest Float,
    '                        DrCumAmount Float,
    '                        DrBalAmount Float,
    '                        DrTotal Float,
    '                        DrNarration nVarchar(100),
    '                        CrDocID nVarchar(21),
    '                        CrDivision nVarchar(1),
    '                        CrSubcode nVarchar(10),
    '                        CrSr Integer,
    '                        CrDate DateTime,
    '                        CrDocNo nVarchar(21)  Collate NoCase,
    '                        CrAmount Float Default 0,
    '                        CrTaxableAmount Float Default 0,
    '                        CrTaxAmount Float Default 0,
    '                        CrDays Integer,
    '                        CrInterest Float,
    '                        CrCumAmount Float,
    '                        CrBalAmount Float,
    '                        CrTotal Float,
    '                        CrNarration nVarchar(100)
    '                    )
    '                    "
    '            AgL.Dman_ExecuteNonQry(mQry, Conn)



    '            If AgL.XNull(ReportFrm.FGetText(8)) = "All" Then
    '                mQry = "Select D.Div_Code as Code, D.Div_Name As [Division] From Division D With (Nolock) Where Div_Code In (" & AgL.PubDivisionList & ") "
    '                DtDivision = AgL.FillData(mQry, Conn).Tables(0)
    '            Else
    '                mQry = "Select D.Div_Code as Code, D.Div_Name As [Division] From Division D Where 1=1 "
    '                mQry = mQry & Replace(ReportFrm.GetWhereCondition("D.Div_Code", 8), "''", "'")
    '                DtDivision = AgL.FillData(mQry, Conn).Tables(0)
    '            End If




    '            mQry = "Select Sg.Subcode, Max(Sg.Nature) as Nature 
    '                    From subgroup sg 
    '                    Left Join Area A On Sg.Area = A.Code
    '                    Left Join City C On Sg.CityCode = C.CityCode
    '                    Left Join SubgroupSiteDivisionDetail L On L.Subcode = Sg.Subcode
    '                    Where 1=1 And Sg.Subcode Is Not Null "

    '            mCondStr = mCondStr & ReportFrm.GetWhereCondition("Sg.SubCode", 3)
    '            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Agent", 5)
    '            mCondStr = mCondStr & ReportFrm.GetWhereCondition("Sg.CityCode", 6)
    '            mCondStr = mCondStr & ReportFrm.GetWhereCondition("Sg.Area", 7)
    '            mCondStr = mCondStr & ReportFrm.GetWhereCondition("Sg.GroupCode", 11)
    '            mQry = mQry + mCondStr + " Group By Sg.Subcode "

    '            DtSubcode = AgL.FillData(mQry, Conn).Tables(0)

    '            For iSubcode = 0 To DtSubcode.Rows.Count - 1

    '                ClsMain.GetAveragePaymentDays(AgL.XNull(DtSubcode.Rows(iSubcode)("Subcode")), True)
    '                Debug.Print(iSubcode.ToString + " / " + DtSubcode.Rows.Count.ToString)
    '                For iDivision = 0 To DtDivision.Rows.Count - 1

    '                    mDivision = AgL.XNull(DtDivision.Rows(iDivision)("Code"))
    '                    mSubcode = AgL.XNull(DtSubcode.Rows(iSubcode)("Subcode"))
    '                    iDr = 0 : iCr = 0 : DrSr = 0 : CrSr = 0 : mTotalDr = 0 : mTotalCr = 0

    '                    mCondStr = "" : mCondStrOp = ""
    '                    If ReportFrm.FGetText(4) = "Financial Year Opening" Then
    '                        mCondStr = " And Date(L.V_Date) >= " & AgL.Chk_Date(CDate(AgL.PubStartDate).ToString("s")) & " "
    '                    End If
    '                    mCondStr = mCondStr & " And Date(L.V_Date) <= " & AgL.Chk_Date(CDate(ReportFrm.FGetText(1)).ToString("s")) & " "
    '                    mCondStrOp = mCondStrOp & " And Date(L.V_Date) < " & AgL.Chk_Date(CDate(AgL.PubStartDate).ToString("s")) & " "
    '                    mCondStr = mCondStr & " And L.Subcode = " & AgL.Chk_Text(mSubcode) & " "
    '                    mCondStrOp = mCondStrOp & " And L.Subcode = " & AgL.Chk_Text(mSubcode) & " "
    '                    mCondStr = mCondStr & " And L.DivCode = " & AgL.Chk_Text(mDivision) & " "
    '                    mCondStrOp = mCondStrOp & " And L.DivCode = " & AgL.Chk_Text(mDivision) & " "
    '                    mCondStr = mCondStr & " And L.Site_Code = " & AgL.Chk_Text(AgL.PubSiteCode) & " "
    '                    mCondStrOp = mCondStrOp & " And L.Site_Code = " & AgL.Chk_Text(AgL.PubSiteCode) & " "


    '                    If ReportFrm.FGetText(4) = "After Chukti" Then
    '                        mCondStr = mCondStr & " And L.DocId || Cast(L.V_SNo As NVARCHAR) Not In (
    '                                Select PaymentDocId ||  Cast(L.PaymentDocIdSr As NVARCHAR) 
    '                                From Cloth_SupplierSettlementPayments) "

    '                        mCondStr = mCondStr & " And L.DocId || Cast(L.V_SNo As NVARCHAR) Not In (
    '                                Select PurchaseInvoiceDocId ||  Cast(L.PurchaseInvoiceDocIdSr As NVARCHAR) 
    '                                From Cloth_SupplierSettlementInvoices) "

    '                        mCondStr = mCondStr & " And L.DocId Not In (
    '                                Select PurchaseInvoiceDocId +  Cast(L.PurchaseInvoiceDocIdSr As NVARCHAR) 
    '                                From LedgerHead H 
    '                                LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
    '                                Where Vt.NCat In (" & Ncat.PaymentSettlement & ")) "


    '                    End If




    '                    '//For Cheque Cancellation Working But not okay for old data
    '                    'mQry = "select L.DocId, L.V_Date, L.DivCode || L.site_Code || '-' || L.V_Type || '-' || L.RecId as DocNo, (Case When IfNull(Trd.Type,'')='Cancelled' OR IfNull(Trr.Type,'')='Cancelled' Then 0 Else L.AmtDr End) as AmtDr, 
    '                    '(Case When IfNull(L.Chq_No,'') <>'' Then 'Chq : ' || IfNull(L.Chq_No,'') Else '' End) || 
    '                    '(Case When IfNull(LH.PartyDocNo,'') <>'' Then 'Inv : ' || IfNull(LH.PartyDocNo,'') Else '' End) ||
    '                    '(Case When IfNull(Trd.Type,'')='Cancelled' OR IfNull(Trr.Type,'')='Cancelled' Then ' Cancelled Amt.' || Cast(L.AmtDr as NVarchar) Else '' End)
    '                    'as DrNarration,
    '                    'INV.Taxable_Amount, INV.Tax1+INV.Tax2+INV.Tax3+INV.Tax4+INV.Tax5 as Tax_Amount
    '                    'from ledger L With (NoLock)
    '                    'Left Join LedgerHead LH With (NoLock) on L.DocID = LH.DocID
    '                    'Left Join SaleInvoice INV With (NoLock) On L.DocID = INV.DocID
    '                    'Left Join TransactionReferences Trd With (NoLock) On L.DocID = Trd.DocId And L.TSr = IfNull(Trd.DocIDSr, L.TSr) 
    '                    'Left Join TransactionReferences Trr With (NoLock) On L.DocID = Trr.ReferenceDocId And L.TSr = IfNull(Trr.ReferenceSr, L.TSr)
    '                    'where L.AmtDr>0  " & mCondStr & " Order By L.V_Date, Cast(Replace(L.RecId,'-','') as Integer) "

    '                    mQry = "Select Sum(AmtDr) as AmtDr, Sum(AmtCr) as AmtCr From Ledger L With (NoLock) Where 1=1 " & mCondStr
    '                    DtSubcodeBalances = AgL.FillData(mQry, Conn).Tables(0)
    '                    If DtSubcodeBalances.Rows.Count > 0 Then
    '                        mTotalDr = AgL.VNull(DtSubcodeBalances.Rows(0)("AmtDr"))
    '                        mTotalCr = AgL.VNull(DtSubcodeBalances.Rows(0)("AmtCr"))
    '                    Else
    '                        mTotalDr = 0
    '                        mTotalCr = 0
    '                    End If



    '                    mQry = ""
    '                    If ReportFrm.FGetText(4) = "Financial Year Opening" Then
    '                        mQry = "select 'Opening' DocId, " & AgL.Chk_Date(DateAdd(DateInterval.Day, -1, CDate(AgL.PubStartDate))) & " as V_Date, 'Opening' as DocNo, Sum(L.AmtDr-L.AmtCr) as AmtDr, 
    '                        Null as DrNarration, 0 as Taxable_Amount, 0 as Tax_Amount
    '                        from Ledger L With (NoLock)
    '                        Left Join LedgerHead LH With (NoLock) on L.DocID = LH.DocID
    '                        Left Join SaleInvoice INV With (NoLock) On L.DocID = INV.DocID
    '                        Left Join RateType Rt On Inv.RateType = Rt.Code
    '                        where 1=1 " & mCondStrOp & " Group By L.Subcode Having Sum(L.AmtDr-L.AmtCr) > 0 "

    '                        mQry = mQry & " Union All "
    '                    End If

    '                    mQry = mQry & "select L.DocId, IfNull(L.EffectiveDate,L.V_Date) as V_Date, L.V_Type || '-' || L.RecId as DocNo, 
    '                    (Case When IfNull(Trd.Type,'')='Cancelled' OR IfNull(Trr.Type,'')='Cancelled' Then 0 Else L.AmtDr End) as AmtDr, 
    '                    (Case When IfNull(L.Chq_No,'') <>'' Then 'Chq : ' || IfNull(L.Chq_No,'') Else '' End) || 
    '                    (Case When IfNull(LH.PartyDocNo,'') <>'' Then 'Inv : ' || IfNull(LH.PartyDocNo,'') Else '' End) ||
    '                    (Case When IfNull(Inv.RateType,'') <>'' Then 'RT : ' || IfNull(RT.Description,'') Else '' End) ||
    '                    (Case When IfNull(Trd.Type,'')='Cancelled' OR IfNull(Trr.Type,'')='Cancelled' Then ' Cancelled Amt.' || Cast(L.AmtDr as NVarchar) Else '' End) || IfNull(L.Narration,'')
    '                    as DrNarration,
    '                    INV.Taxable_Amount, INV.Tax1+INV.Tax2+INV.Tax3+INV.Tax4+INV.Tax5 as Tax_Amount
    '                    from ledger L With (NoLock)
    '                    Left Join LedgerHead LH With (NoLock) on L.DocID = LH.DocID
    '                    Left Join SaleInvoice INV With (NoLock) On L.DocID = INV.DocID
    '                    Left Join RateType Rt On Inv.RateType = Rt.Code
    '                    Left Join TransactionReferences Trd With (NoLock) On L.DocID = Trd.DocId And L.V_SNo = Trd.DocIDSr And L.V_Date >= '2019-07-01'
    '                    Left Join TransactionReferences Trr With (NoLock) On L.DocID = Trr.ReferenceDocId And L.TSr = Trr.ReferenceSr And L.V_Date >= '2019-07-01'
    '                    where L.AmtDr>0  " & mCondStr & "  "

    '                    If ReportFrm.FGetText(4) = "Financial Year Opening" Then
    '                        mQry = mQry & " Order By V_Date,  DocNo "
    '                    Else
    '                        If AgL.PubServerName = "" Then
    '                            mQry = mQry & " Order By IfNull(L.EffectiveDate,L.V_Date), Try_Parse(Replace(L.RecId,'-','') as Integer) "
    '                        Else
    '                            mQry = mQry & " Order By IfNull(L.EffectiveDate,L.V_Date), Cast((Case When IsNumeric(Replace(L.RecId,'-',''))=1 Then Replace(L.RecId,'-','') Else Null End) as Integer) "
    '                        End If
    '                    End If



    '                    DtDr = AgL.FillData(mQry, Conn).Tables(0)
    '                    DrRecordCount = DtDr.Rows.Count

    '                    'mQry = "
    '                    'select L.DocId, L.V_Date, L.DivCode || L.site_Code || '-' || L.V_Type || '-' || L.RecId as DocNo, (Case When IfNull(Trd.Type,'')='Cancelled' OR IfNull(Trr.Type,'')='Cancelled' Then 0 Else L.AmtCr End) as AmtCr,
    '                    '(Case When IfNull(L.Chq_No,'') <>'' Then 'Chq : ' || IfNull(L.Chq_No,'') Else '' End) || 
    '                    '(Case When IfNull(LH.PartyDocNo,'') <>'' Then 'Inv : ' || IfNull(LH.PartyDocNo,'') Else '' End) ||
    '                    '(Case When IfNull(Trd.Type,'')='Cancelled' OR IfNull(Trr.Type,'')='Cancelled' Then ' Cancelled Amt.' || Cast(L.AmtCr as NVarchar) Else '' End)
    '                    'as CrNarration,
    '                    'INV.Taxable_Amount, INV.Tax1+INV.Tax2+INV.Tax3+INV.Tax4+INV.Tax5 as Tax_Amount
    '                    'from ledger L  With (NoLock)
    '                    'Left Join LedgerHead LH  With (NoLock) On L.DocID = LH.DocID
    '                    'Left Join PurchInvoice INV With (NoLock) On L.DocID = INV.DocID
    '                    'Left Join TransactionReferences Trd With (NoLock) On L.DocID = Trd.DocId And L.TSr = IfNull(Trd.DocIDSr, L.TSr)
    '                    'Left Join TransactionReferences Trr With (NoLock) On L.DocID = Trr.ReferenceDocId And L.TSr = IfNull(Trr.ReferenceSr, L.TSr)
    '                    'where L.AmtCr>0  " & mCondStr & " Order By L.V_Date,  Cast(Replace(L.RecId,'-','') as Integer) "



    '                    mQry = ""
    '                    If ReportFrm.FGetText(4) = "Financial Year Opening" Then
    '                        mQry = "select 'Opening' DocId, " & AgL.Chk_Date(DateAdd(DateInterval.Day, -1, CDate(AgL.PubStartDate))) & " as V_Date, 'Opening' as DocNo, Sum(L.AmtCr-L.AmtDr) as AmtCr, 
    '                        Null as CrNarration, 0 as Taxable_Amount, 0 as Tax_Amount
    '                        from Ledger L With (NoLock)
    '                        Left Join LedgerHead LH With (NoLock) on L.DocID = LH.DocID
    '                        where 1=1 " & mCondStrOp & " Group By L.Subcode Having Sum(L.AmtCr-L.AmtDr) > 0 "

    '                        mQry = mQry & " Union All "
    '                    End If


    '                    mQry = mQry & "
    '                    select L.DocId, IfNull(L.EffectiveDate,L.V_Date) as V_Date, L.V_Type || '-' || L.RecId as DocNo, 
    '                    (Case When IfNull(Trd.Type,'')='Cancelled' OR IfNull(Trr.Type,'')='Cancelled' Then 0 Else L.AmtCr End) as AmtCr,
    '                    (Case When IfNull(L.Chq_No,'') <>'' Then 'Chq : ' || IfNull(L.Chq_No,'') Else '' End) || 
    '                    (Case When IfNull(LH.PartyDocNo,'') <>'' Then 'Inv : ' || IfNull(LH.PartyDocNo,'') Else '' End) ||
    '                    (Case When IfNull(Trd.Type,'')='Cancelled' OR IfNull(Trr.Type,'')='Cancelled' Then ' Cancelled Amt.' || Cast(L.AmtCr as NVarchar)  Else '' End) || Left(IfNull(L.Narration,''),30) 
    '                    as CrNarration,
    '                    INV.Taxable_Amount, INV.Tax1+INV.Tax2+INV.Tax3+INV.Tax4+INV.Tax5 as Tax_Amount
    '                    from ledger L  With (NoLock)
    '                    Left Join LedgerHead LH  With (NoLock) On L.DocID = LH.DocID
    '                    Left Join PurchInvoice INV With (NoLock) On L.DocID = INV.DocID
    '                    Left Join TransactionReferences Trd With (NoLock) On L.DocID = Trd.DocId And L.V_SNo = Trd.DocIDSr And Trd.Type = 'Cancelled' And L.V_Date >= '2019-07-01'
    '                    Left Join TransactionReferences Trr With (NoLock) On L.DocID = Trr.ReferenceDocId And L.TSr = Trr.ReferenceSr And Trr.Type = 'Cancelled' And L.V_Date >= '2019-07-01'
    '                    where L.AmtCr>0  " & mCondStr & "  "

    '                    If ReportFrm.FGetText(4) = "Financial Year Opening" Then
    '                        mQry = mQry & " Order By V_Date,  DocNo "
    '                    Else
    '                        If AgL.PubServerName = "" Then
    '                            mQry = mQry & " Order By IfNull(L.EffectiveDate,L.V_Date),  Try_Parse(Replace(L.RecId,'-','') as Integer) "
    '                        Else
    '                            'mQry = mQry & " Order By IfNull(L.EffectiveDate,L.V_Date),  Try_Parse(Replace(L.RecId,'-','') as Integer) "
    '                            mQry = mQry & " Order By IfNull(L.EffectiveDate,L.V_Date), Cast((Case When IsNumeric(Replace(L.RecId,'-',''))=1 Then Replace(L.RecId,'-','') Else Null End) as BigInt) "
    '                        End If
    '                    End If

    '                    DtCr = AgL.FillData(mQry, Conn).Tables(0)
    '                    CrRecordCount = DtCr.Rows.Count


    '                    Dim mRunningTotalDr As Double
    '                    Dim mRunningTotalCr As Double
    '                    Dim mDays As Integer
    '                    Dim mInterest As Double
    '                    mRunningTotalDr = 0 : mRunningTotalCr = 0 : mLastChuktiAmount = 0 : mDays = 0 : mInterest = 0 : ConcurSr = -1 : FirstConcurSr = -1

    '                    LoopLimit = DrRecordCount + CrRecordCount ' IIf(DrRecordCount >= CrRecordCount, DrRecordCount, CrRecordCount)
    '                    For I = 0 To LoopLimit

    '                        If DrRecordCount > iDr Then
    '                            If iDr = 0 Or iCr >= CrRecordCount Or mRunningTotalDr <= mRunningTotalCr Then
    '                                If AgL.XNull(DtSubcode.Rows(iSubcode)("Nature")).ToString.ToUpper = "CUSTOMER" Then
    '                                    mDays = DateDiff(DateInterval.Day, DateAdd(DateInterval.Day, Val(ReportFrm.FGetText(2)), CDate(DtDr.Rows(iDr)("V_Date"))), CDate(ReportFrm.FGetText(1)))
    '                                Else
    '                                    mDays = DateDiff(DateInterval.Day, AgL.XNull(DtDr.Rows(iDr)("V_Date")), CDate(ReportFrm.FGetText(1)))
    '                                End If
    '                                If mDays < 0 Then mDays = 0
    '                                'mInterest = Math.Round(Val(DtDr.Rows(iDr)("AmtDr")) * AgL.VNull(AgL.PubDtEnviro.Rows(0)("Default_DebtorsInterestRate")) * (mDays / 36500), 2)
    '                                mInterest = Math.Round(Val(DtDr.Rows(iDr)("AmtDr")) * Val(ReportFrm.FGetText(10)) * (mDays / 36500), 2)
    '                                mRunningTotalDr += AgL.VNull(DtDr.Rows(iDr)("AmtDr"))
    '                                mQry = "Insert Into #TempTblDr (DrDocID,DrDivision,DrSubcode,DrSr,DrDate,DrDocNo,DrAmount,DrTaxableAmount,DrTaxAmount,DrDays,DrInterest,DrNarration, DrCumAmount, DrTotal)
    '                                        Values(" & AgL.Chk_Text(AgL.XNull(DtDr.Rows(iDr)("DocID"))) & "," & AgL.Chk_Text(mDivision) & ", " & AgL.Chk_Text(mSubcode) & ", " & DrSr & ", " & AgL.Chk_Date(AgL.XNull(DtDr.Rows(iDr)("V_Date"))) & ", " & AgL.Chk_Text(AgL.XNull(DtDr.Rows(iDr)("DocNo"))) & "," & AgL.VNull(DtDr.Rows(iDr)("AmtDr")) & "," & AgL.VNull(DtDr.Rows(iDr)("Taxable_Amount")) & "," & AgL.VNull(DtDr.Rows(iDr)("Tax_Amount")) & ", " & mDays & ", " & mInterest & "," & AgL.Chk_Text(AgL.XNull(DtDr.Rows(iDr)("DrNarration"))) & ", " & Val(mRunningTotalDr) & ", " & Val(mTotalDr) & ")
    '                                       "
    '                                AgL.Dman_ExecuteNonQry(mQry, Conn)
    '                                iDr += 1
    '                                DrSr += 1
    '                            End If
    '                        End If


    '                        If Math.Round(mRunningTotalDr, 2) = Math.Round(mRunningTotalCr, 2) And mLastChuktiAmount <> mRunningTotalDr And mRunningTotalDr > 0 Then
    '                            J = 0
    '                            mLastChuktiAmount = mRunningTotalDr
    '                            If DrSr > CrSr Then
    '                                For J = CrSr To DrSr - 1
    '                                    mQry = "Insert Into #TempTblCr(CrDivision,CrSubcode, CrSr, CrCumAmount, CrTotal) Values(" & AgL.Chk_Text(mDivision) & ", " & AgL.Chk_Text(mSubcode) & ", " & J & ", " & Val(mRunningTotalCr) & ", " & Val(mTotalCr) & ")"
    '                                    AgL.Dman_ExecuteNonQry(mQry, Conn)
    '                                Next
    '                            ElseIf CrSr > DrSr Then
    '                                For J = DrSr To CrSr - 1
    '                                    mQry = "Insert Into #TempTblDr(DrDivision,DrSubcode,DrSr, DrCumAmount, DrTotal) Values(" & AgL.Chk_Text(mDivision) & ", " & AgL.Chk_Text(mSubcode) & "," & J & ", " & Val(mRunningTotalDr) & ", " & Val(mTotalDr) & ")"
    '                                    AgL.Dman_ExecuteNonQry(mQry, Conn)
    '                                Next
    '                            End If



    '                            If iDr <= DrRecordCount Or iCr <= CrRecordCount Then
    '                                If J = 0 Then J = DrSr + 1
    '                                mQry = "Insert Into #TempTblDr(DrDivision,DrSubcode,DrSr, DrCumAmount, DrTotal) Values(" & AgL.Chk_Text(mDivision) & ", " & AgL.Chk_Text(mSubcode) & ", " & J & ", " & Val(mRunningTotalDr) & ", " & Val(mTotalDr) & ")"
    '                                AgL.Dman_ExecuteNonQry(mQry, Conn)


    '                                mQry = "Insert Into #TempTblCr(CrDivision,CrSubcode, CrSr, CrCumAmount, CrTotal) Values(" & AgL.Chk_Text(mDivision) & ", " & AgL.Chk_Text(mSubcode) & ", " & J & ", " & Val(mRunningTotalCr) & ", " & Val(mTotalCr) & ")"
    '                                AgL.Dman_ExecuteNonQry(mQry, Conn)

    '                                ConcurSr = J
    '                                'If CDate(AgL.XNull(DtDr.Rows(iDr)("V_Date"))) < CDate(DateAdd(DateInterval.Year, -1, CDate(AgL.PubStartDate))) Then
    '                                If CDate(AgL.XNull(DtDr.Rows(iDr - 1)("V_Date"))) < CDate(AgL.PubStartDate) Then
    '                                    FirstConcurSr = ConcurSr
    '                                End If

    '                                DrSr = J + 1
    '                                CrSr = J + 1

    '                                LoopLimit += 1
    '                            End If
    '                        End If



    '                        If CrRecordCount > iCr Then
    '                            If iCr = 0 Or iDr >= DrRecordCount Or mRunningTotalDr > mRunningTotalCr Then
    '                                'mDays = DateDiff(DateInterval.Day, DateAdd(DateInterval.Day, Val(ReportFrm.FGetText(1)), AgL.XNull(DtCr.Rows(iCr)("V_Date"))), CDate(ReportFrm.FGetText(0)))
    '                                If AgL.XNull(DtSubcode.Rows(iSubcode)("Nature")).ToString.ToUpper = "CUSTOMER" Then
    '                                    mDays = DateDiff(DateInterval.Day, AgL.XNull(DtCr.Rows(iCr)("V_Date")), CDate(ReportFrm.FGetText(1)))
    '                                Else
    '                                    mDays = DateDiff(DateInterval.Day, DateAdd(DateInterval.Day, Val(ReportFrm.FGetText(2)), CDate(DtCr.Rows(iCr)("V_Date"))), CDate(ReportFrm.FGetText(1)))
    '                                End If
    '                                'mInterest = Math.Round(Val(DtCr.Rows(iCr)("AmtCr")) * AgL.VNull(AgL.PubDtEnviro.Rows(0)("Default_DebtorsInterestRate")) * (mDays / 36500), 2)
    '                                mInterest = Math.Round(Val(DtCr.Rows(iCr)("AmtCr")) * Val(ReportFrm.FGetText(10)) * (mDays / 36500), 2)
    '                                mRunningTotalCr += AgL.VNull(DtCr.Rows(iCr)("AmtCr"))
    '                                mQry = "Insert Into #TempTblCr (CrDocId,CrDivision, CrSubcode, CrSr,CrDate,CrDocNo,CrAmount,CrTaxableAmount,CrTaxAmount,CrDays,CrInterest,CrNarration,CrCumAmount,CrTotal)
    '                                        Values(" & AgL.Chk_Text(AgL.XNull(DtCr.Rows(iCr)("DocID"))) & "," & AgL.Chk_Text(mDivision) & ", " & AgL.Chk_Text(mSubcode) & ", " & CrSr & ", " & AgL.Chk_Date(AgL.XNull(DtCr.Rows(iCr)("V_Date"))) & ", " & AgL.Chk_Text(AgL.XNull(DtCr.Rows(iCr)("DocNo"))) & "," & AgL.VNull(DtCr.Rows(iCr)("AmtCr")) & "," & AgL.VNull(DtCr.Rows(iCr)("Taxable_Amount")) & "," & AgL.VNull(DtCr.Rows(iCr)("Tax_Amount")) & ", " & mDays & ", " & mInterest & "," & AgL.Chk_Text(AgL.XNull(DtCr.Rows(iCr)("CrNarration"))) & ", " & Val(mRunningTotalCr) & ", " & Val(mTotalCr) & ")
    '                                       "
    '                                AgL.Dman_ExecuteNonQry(mQry, Conn)

    '                                iCr += 1
    '                                CrSr += 1
    '                            End If
    '                        End If

    '                        'Try
    '                        '    Debug.Print("Dr : " + AgL.XNull(DtDr.Rows(iDr - 1)("V_Date")) + "   Amt : " + Val(DtDr.Rows(iDr - 1)("AmtDr")).ToString() + "   RunningTotal : " + mRunningTotalDr.ToString)
    '                        'Catch ex As Exception
    '                        'End Try
    '                        'Try
    '                        '    Debug.Print("Cr : " + AgL.XNull(DtCr.Rows(iCr - 1)("V_Date")) + "   Amt : " + Val(DtCr.Rows(iCr - 1)("AmtCr")).ToString() + "   RunningTotal : " + mRunningTotalCr.ToString)
    '                        'Catch ex As Exception
    '                        'End Try


    '                        If Math.Round(mRunningTotalDr, 2) = Math.Round(mRunningTotalCr, 2) And mLastChuktiAmount <> mRunningTotalDr And mRunningTotalDr > 0 Then
    '                            J = 0
    '                            mLastChuktiAmount = mRunningTotalDr
    '                            If DrSr > CrSr Then
    '                                For J = CrSr To DrSr - 1
    '                                    mQry = "Insert Into #TempTblCr(CrDivision,CrSubcode, CrSr, CrCumAmount, CrTotal) Values(" & AgL.Chk_Text(mDivision) & ", " & AgL.Chk_Text(mSubcode) & ", " & J & ", " & Val(mRunningTotalCr) & ", " & Val(mTotalCr) & ")"
    '                                    AgL.Dman_ExecuteNonQry(mQry, Conn)
    '                                Next
    '                            ElseIf CrSr > DrSr Then
    '                                For J = DrSr To CrSr - 1
    '                                    mQry = "Insert Into #TempTblDr(DrDivision,DrSubcode,DrSr, DrCumAmount, DrTotal) Values(" & AgL.Chk_Text(mDivision) & ", " & AgL.Chk_Text(mSubcode) & "," & J & ", " & Val(mRunningTotalDr) & ", " & Val(mTotalDr) & ")"
    '                                    AgL.Dman_ExecuteNonQry(mQry, Conn)
    '                                Next
    '                            End If



    '                            If iDr <= DrRecordCount Or iCr <= CrRecordCount Then
    '                                If J = 0 Then J = DrSr + 1
    '                                mQry = "Insert Into #TempTblDr(DrDivision,DrSubcode,DrSr, DrCumAmount, DrTotal) Values(" & AgL.Chk_Text(mDivision) & ", " & AgL.Chk_Text(mSubcode) & ", " & J & ", " & Val(mRunningTotalDr) & ", " & Val(mTotalDr) & ")"
    '                                AgL.Dman_ExecuteNonQry(mQry, Conn)


    '                                mQry = "Insert Into #TempTblCr(CrDivision,CrSubcode, CrSr, CrCumAmount, CrTotal) Values(" & AgL.Chk_Text(mDivision) & ", " & AgL.Chk_Text(mSubcode) & ", " & J & ", " & Val(mRunningTotalCr) & ", " & Val(mTotalCr) & ")"
    '                                AgL.Dman_ExecuteNonQry(mQry, Conn)

    '                                ConcurSr = J
    '                                'If CDate(AgL.XNull(DtCr.Rows(iCr)("V_Date"))) < CDate(DateAdd(DateInterval.Year, -1, CDate(AgL.PubStartDate))) Then
    '                                If CDate(AgL.XNull(DtCr.Rows(iCr - 1)("V_Date"))) < CDate(AgL.PubStartDate) Then
    '                                    FirstConcurSr = ConcurSr
    '                                End If


    '                                DrSr = J + 1
    '                                CrSr = J + 1

    '                                LoopLimit += 1
    '                            End If
    '                        End If
    '                    Next


    '                    If DrSr > CrSr Then
    '                        For J = CrSr To DrSr - 1
    '                            mQry = "Insert Into #TempTblCr(CrDivision,CrSubcode, CrSr, CrCumAmount, CrTotal) Values(" & AgL.Chk_Text(mDivision) & ", " & AgL.Chk_Text(mSubcode) & ", " & J & ", " & Val(mRunningTotalCr) & ", " & Val(mTotalCr) & ")"
    '                            AgL.Dman_ExecuteNonQry(mQry, Conn)
    '                        Next
    '                    ElseIf CrSr > DrSr Then
    '                        For J = DrSr To CrSr - 1
    '                            mQry = "Insert Into #TempTblDr(DrDivision,DrSubcode, DrSr, DrCumAmount, DrTotal) Values(" & AgL.Chk_Text(mDivision) & ", " & AgL.Chk_Text(mSubcode) & ", " & J & ", " & Val(mRunningTotalDr) & ", " & Val(mTotalDr) & ")"
    '                            AgL.Dman_ExecuteNonQry(mQry, Conn)
    '                        Next
    '                    End If


    '                    mQry = "Insert Into #TempTblDrCr (DrDocID,DrDivision,DrSubcode, DrSr, DrDate, DrDocNo, DrAmount, DrTaxableAmount, DrTaxAmount, DrDays, DrInterest, DrNarration,DrCumAmount,DrTotal,
    '                        CrDocId,CrDivision,CrSubcode, CrSr, CrDate, CrDocNo, CrAmount, CrTaxableAmount, CrTaxAmount, CrDays, CrInterest, CrNarration,CrCumAmount, CrTotal, DrBalAmount, CrBalAmount) 
    '                    Select Dr.DrDocID, Dr.DrDivision, Dr.DrSubcode, Dr.DrSr, Dr.DrDate, Dr.DrDocNo, Dr.DrAmount, Dr.DrTaxableAmount, Dr.DrTaxAmount, Dr.DrDays, Dr.DrInterest, Dr.DrNarration, Dr.DrCumAmount, Dr.DrTotal, 
    '                        Cr.CrDocID, Cr.CrDivision,Cr.CrSubcode, Cr.CrSr, Cr.CrDate, Cr.CrDocNo, Cr.CrAmount, Cr.CrTaxableAmount, Cr.CrTaxAmount, Cr.CrDays, Cr.CrInterest, Cr.CrNarration, Cr.CrCumAmount, Cr.CrTotal,                    
    '                    (Case When Dr.DrAmount-((Case When (Cr.CrTotal-(Dr.DrCumAmount-Dr.DrAmount))<0 Then 0 Else Cr.CrTotal-(Dr.DrCumAmount-Dr.DrAmount) End))<0 Then 0 Else Dr.DrAmount-((Case When (Cr.CrTotal-(Dr.DrCumAmount-Dr.DrAmount))<0 Then 0 Else Cr.CrTotal-(Dr.DrCumAmount-Dr.DrAmount) End)) End) as DrBalAmount,
    '                    (Case When Cr.CrAmount-((Case When (Dr.DrTotal-(Cr.CrCumAmount-Cr.CrAmount))<0 Then 0 Else Dr.DrTotal-(Cr.CrCumAmount-Cr.CrAmount) End))<0 Then 0 Else Cr.CrAmount-((Case When (Dr.DrTotal-(Cr.CrCumAmount-Cr.CrAmount))<0 Then 0 Else Dr.DrTotal-(Cr.CrCumAmount-Cr.CrAmount) End)) End) as CrBalAmount
    '                    From #TempTblDr Dr, #TempTblCr Cr Where Dr.DrDivision = Cr.CrDivision And  Dr.DrSubcode = Cr.CrSubcode And Dr.DrSr = Cr.CrSr "

    '                    If ReportFrm.FGetText(4) = "After Chukti" Then
    '                        mQry = mQry & " And Dr.DrSr > " & ConcurSr & ""
    '                    ElseIf ReportFrm.FGetText(4) = "Financial Year" Then
    '                        mQry = mQry & " And Dr.DrSr > " & FirstConcurSr & ""
    '                    End If

    '                    mQry = mQry & " Order By Dr.DrSr "

    '                    AgL.Dman_ExecuteNonQry(mQry, Conn)

    '                    mQry = "Delete From #TempTblDr"
    '                    AgL.Dman_ExecuteNonQry(mQry, Conn)

    '                    mQry = "Delete From #TempTblCr"
    '                    AgL.Dman_ExecuteNonQry(mQry, Conn)

    '                Next iDivision
    '            Next iSubcode






    '            mQry = "Select D.Name as DivisionName, Sg.Name as PartyName, Sg.Address, Sg.Mobile, Agent.Name as AgentName, 
    '                    SRep.Name as SalesRepresentativeName, Area.Description as AreaName, H.*, SL.AdditionPer, SL.AdditionAmount, Gr.GrReturnAmt, Gr.GrSaleAmt, Gr.ReturnPer, Sg1.AveragePaymentDays, "
    '            If AgL.PubServerName <> "" Then
    '                mQry = mQry & "Substring(Convert(NVARCHAR, H.DrDate,103),4,7) As [DrMonth], Substring(Convert(NVARCHAR, H.CrDate,103),4,7) As [CrMonth]  "
    '            Else
    '                mQry = mQry & "strftime('%m-%Y',H.DrDate) As [DrMonth], strftime('%m-%Y',H.CrDate) As [CrMonth]  "
    '            End If

    '            mQry = mQry & "from #TempTblDrCr H 
    '                    Left Join viewHelpSubgroup Sg on H.DrSubcode COLLATE DATABASE_DEFAULT = Sg.Code COLLATE DATABASE_DEFAULT
    '                    Left Join subgroup sg1 on sg.code= Sg1.Subcode
    '                    Left Join viewHelpSubgroup D On D.Code COLLATE DATABASE_DEFAULT = H.DrDivision COLLATE DATABASE_DEFAULT
    '                    Left Join (
    '                                select subcode, Max(Agent) as Agent, Max(SalesRepresentative) as SalesRepresentative
    '                                From SubgroupSiteDivisionDetail
    '                                Group By Subcode
    '                              ) as LTV On LTV.Subcode = Sg.Code
    '                    Left Join viewHelpSubgroup Agent  On LTV.Agent COLLATE DATABASE_DEFAULT = Agent.Code COLLATE DATABASE_DEFAULT
    '                    Left Join viewHelpSubgroup SRep  On  LTV.SalesRepresentative COLLATE DATABASE_DEFAULT = SRep.Code COLLATE DATABASE_DEFAULT
    '                    Left Join Area On Sg1.Area = Area.Code
    '                    Left Join (
    '                                SELECT L.SubCode, L.DivCode, Sum(L.AmtCr) AS GrReturnAmt, (CASE WHEN Sum(L.AmtDr) = 0 THEN Sum(L.AmtCr) ELSE Sum(L.AmtDr) END) AS GrSaleAmt,  Round((Sum(L.AmtCr) /  (CASE WHEN Sum(L.AmtDr) = 0 THEN Sum(L.AmtCr) ELSE Sum(L.AmtDr) END))*100,2) 	AS ReturnPer
    '                                FROM Ledger L With (NoLock)
    '                                LEFT JOIN Voucher_Type VT With (NoLock) ON L.V_Type = vt.V_type
    '                                LEFT JOIN subgroup Sg  With (NoLock) ON L.SubCode = Sg.Subcode 
    '                                WHERE VT.NCat IN ('SI','SR') OR VT.V_Type ='OB' AND Sg.Nature ='Customer'
    '                                GROUP BY L.SubCode, L.DivCode  
    '                                HAVING Sum(L.AmtCr) > 0
    '                              ) as Gr On Gr.Subcode  COLLATE DATABASE_DEFAULT = H.DrSubcode  COLLATE DATABASE_DEFAULT And Gr.DivCode  COLLATE DATABASE_DEFAULT = H.DrDivision  COLLATE DATABASE_DEFAULT
    '                    Left Join (
    '                                SELECT DocID, Max(AdditionPer) AS AdditionPer, Sum(AdditionAmount) AS AdditionAmount  
    '                                FROM SaleInvoiceDetail GROUP BY DocID 
    '                              ) as SL On H.DrDocID  COLLATE DATABASE_DEFAULT = SL.DocId  COLLATE DATABASE_DEFAULT 
    '                    Order By H.DrSubcode, H.DrSr"
    '            DsRep = AgL.FillData(mQry, Conn)

    '            FunConcurLedger = DsRep
    '        Catch ex As Exception
    '            FunConcurLedger = Nothing
    '            MsgBox(ex.Message)
    '            DsRep = Nothing
    '        End Try
    '    End Function

    '    Private Sub ProcConcurLedger()
    '        Dim DsRep As DataSet = FunConcurLedger(AgL.GCn)

    '        If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records to Print!")
    '        ReportFrm.PrintReport(DsRep, RepName, RepTitle)
    '    End Sub

    '    Private Sub ProcConcurLedgerBackup()
    '        Dim mCondStr$ = ""
    '        Dim mCondStrOp$ = ""
    '        Dim NoofDays As Integer = 0
    '        Dim DtSubcode As DataTable
    '        Dim iSubcode As Integer
    '        Dim DtDivision As DataTable
    '        Dim iDivision As Integer
    '        Dim DtDr As DataTable
    '        Dim DtCr As DataTable
    '        Dim DtTemp As DataTable
    '        Dim DrRecordCount As Integer
    '        Dim CrRecordCount As Integer
    '        Dim LoopLimit As Integer
    '        Dim I As Integer, J As Integer
    '        Dim iDr As Integer
    '        Dim iCr As Integer
    '        Dim DrSr As Integer
    '        Dim CrSr As Integer
    '        Dim ConcurSr As Integer = -1
    '        Dim FirstConcurSr As Integer = -1
    '        Dim mSubcode As String
    '        Dim mDivision As String
    '        Dim mLastChuktiAmount As Double

    '        Try

    '            If AgL.XNull(ReportFrm.FGetText(0)).ToString.ToUpper = "Format 2".ToUpper Then
    '                RepName = "ConcurLedgerLandscape" : RepTitle = "Chukti Ledger"
    '            Else
    '                RepName = "ConcurLedger" : RepTitle = "Chukti Ledger"
    '            End If

    '            If Val(ReportFrm.FGetText(2)) <> 0 Then
    '                NoofDays = Val(ReportFrm.FGetText(2))
    '            Else
    '                MsgBox("Please Enter Valid No. Of Days.") : Exit Sub
    '            End If




    '            Try
    '                mQry = "Drop Table #TempTblDr "
    '                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
    '            Catch ex As Exception
    '            End Try



    '            mQry = "Create Temporary Table #TempTblDr 
    '                    (
    '                        DrDivision nVarchar(1),
    '                        DrSubcode nVarchar(10),
    '                        DrSr Integer,
    '                        DrDate DateTime,
    '                        DrDocNo nVarchar(21)  Collate NoCase,
    '                        DrAmount Float Default 0,
    '                        DrTaxableAmount Float Default 0,
    '                        DrTaxAmount Float Default 0,
    '                        DrDays Integer,
    '                        DrInterest Float,
    '                        DrNarration nVarchar(100)
    '                    )
    '                    "

    '            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)


    '            Try
    '                mQry = "Drop Table #TempTblCr "
    '                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
    '            Catch ex As Exception
    '            End Try


    '            mQry = "Create Temporary Table #TempTblCr 
    '                    (
    '                        CrDivision nVarchar(1),
    '                        CrSubcode nVarchar(10),
    '                        CrSr Integer,
    '                        CrDate DateTime,
    '                        CrDocNo nVarchar(21)  Collate NoCase,
    '                        CrAmount Float Default 0,
    '                        CrTaxableAmount Float Default 0,
    '                        CrTaxAmount Float Default 0,
    '                        CrDays Integer,
    '                        CrInterest Float,
    '                        CrNarration nVarchar(100)                        
    '                    )
    '                    "

    '            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)


    '            Try
    '                mQry = "Drop Table #TempTblDrCr "
    '                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
    '            Catch ex As Exception
    '            End Try


    '            mQry = "Create Temporary Table #TempTblDrCr 
    '                    (
    '                        DrDivision nVarchar(1),
    '                        DrSubcode nVarchar(10),
    '                        DrSr Integer,
    '                        DrDate DateTime,
    '                        DrDocNo nVarchar(21)  Collate NoCase,
    '                        DrAmount Float Default 0,
    '                        DrTaxableAmount Float Default 0,
    '                        DrTaxAmount Float Default 0,
    '                        DrDays Integer,
    '                        DrInterest Float,
    '                        DrNarration nVarchar(100),
    '                        CrDivision nVarchar(1),
    '                        CrSubcode nVarchar(10),
    '                        CrSr Integer,
    '                        CrDate DateTime,
    '                        CrDocNo nVarchar(21)  Collate NoCase,
    '                        CrAmount Float Default 0,
    '                        CrTaxableAmount Float Default 0,
    '                        CrTaxAmount Float Default 0,
    '                        CrDays Integer,
    '                        CrInterest Float,
    '                        CrNarration nVarchar(100)
    '                    )
    '                    "
    '            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)



    '            If AgL.XNull(ReportFrm.FGetText(8)) = "All" Then
    '                mQry = "Select D.Div_Code as Code, D.Div_Name As [Division] From Division D With (Nolock) Where Div_Code In (" & AgL.PubDivisionList & ") "
    '                DtDivision = AgL.FillData(mQry, AgL.GCn).Tables(0)
    '            Else
    '                mQry = "Select D.Div_Code as Code, D.Div_Name As [Division] From Division D Where 1=1 "
    '                mQry = mQry & Replace(ReportFrm.GetWhereCondition("D.Div_Code", 8), "''", "'")
    '                DtDivision = AgL.FillData(mQry, AgL.GCn).Tables(0)
    '            End If




    '            mQry = "Select Sg.Subcode 
    '                    From subgroup sg 
    '                    Left Join Area A On Sg.Area = A.Code
    '                    Left Join City C On Sg.CityCode = C.CityCode
    '                    Left Join SubgroupSiteDivisionDetail L On L.Subcode = Sg.Subcode
    '                    Where 1=1 And Sg.Subcode Is Not Null "

    '            mCondStr = mCondStr & ReportFrm.GetWhereCondition("Sg.SubCode", 3)
    '            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Agent", 5)
    '            mCondStr = mCondStr & ReportFrm.GetWhereCondition("Sg.CityCode", 6)
    '            mCondStr = mCondStr & ReportFrm.GetWhereCondition("Sg.Area", 7)
    '            mQry = mQry + mCondStr + " Group By Sg.Subcode "

    '            DtSubcode = AgL.FillData(mQry, AgL.GCn).Tables(0)

    '            For iSubcode = 0 To DtSubcode.Rows.Count - 1
    '                Debug.Print(iSubcode.ToString + " / " + DtSubcode.Rows.Count.ToString)
    '                For iDivision = 0 To DtDivision.Rows.Count - 1

    '                    mDivision = AgL.XNull(DtDivision.Rows(iDivision)("Code"))
    '                    mSubcode = AgL.XNull(DtSubcode.Rows(iSubcode)("Subcode"))
    '                    iDr = 0 : iCr = 0 : DrSr = 0 : CrSr = 0

    '                    mCondStr = "" : mCondStrOp = ""
    '                    If ReportFrm.FGetText(4) = "Financial Year Opening" Then
    '                        mCondStr = " And Date(L.V_Date) >= " & AgL.Chk_Date(CDate(AgL.PubStartDate).ToString("s")) & " "
    '                    End If
    '                    mCondStr = mCondStr & " And Date(L.V_Date) <= " & AgL.Chk_Date(CDate(ReportFrm.FGetText(1)).ToString("s")) & " "
    '                    mCondStrOp = mCondStrOp & " And Date(L.V_Date) < " & AgL.Chk_Date(CDate(AgL.PubStartDate).ToString("s")) & " "
    '                    mCondStr = mCondStr & " And L.Subcode = " & AgL.Chk_Text(mSubcode) & " "
    '                    mCondStrOp = mCondStrOp & " And L.Subcode = " & AgL.Chk_Text(mSubcode) & " "
    '                    mCondStr = mCondStr & " And L.DivCode = " & AgL.Chk_Text(mDivision) & " "
    '                    mCondStrOp = mCondStrOp & " And L.DivCode = " & AgL.Chk_Text(mDivision) & " "



    '                    '//For Cheque Cancellation Working But not okay for old data
    '                    'mQry = "select L.DocId, L.V_Date, L.DivCode || L.site_Code || '-' || L.V_Type || '-' || L.RecId as DocNo, (Case When IfNull(Trd.Type,'')='Cancelled' OR IfNull(Trr.Type,'')='Cancelled' Then 0 Else L.AmtDr End) as AmtDr, 
    '                    '(Case When IfNull(L.Chq_No,'') <>'' Then 'Chq : ' || IfNull(L.Chq_No,'') Else '' End) || 
    '                    '(Case When IfNull(LH.PartyDocNo,'') <>'' Then 'Inv : ' || IfNull(LH.PartyDocNo,'') Else '' End) ||
    '                    '(Case When IfNull(Trd.Type,'')='Cancelled' OR IfNull(Trr.Type,'')='Cancelled' Then ' Cancelled Amt.' || Cast(L.AmtDr as NVarchar) Else '' End)
    '                    'as DrNarration,
    '                    'INV.Taxable_Amount, INV.Tax1+INV.Tax2+INV.Tax3+INV.Tax4+INV.Tax5 as Tax_Amount
    '                    'from ledger L With (NoLock)
    '                    'Left Join LedgerHead LH With (NoLock) on L.DocID = LH.DocID
    '                    'Left Join SaleInvoice INV With (NoLock) On L.DocID = INV.DocID
    '                    'Left Join TransactionReferences Trd With (NoLock) On L.DocID = Trd.DocId And L.TSr = IfNull(Trd.DocIDSr, L.TSr) 
    '                    'Left Join TransactionReferences Trr With (NoLock) On L.DocID = Trr.ReferenceDocId And L.TSr = IfNull(Trr.ReferenceSr, L.TSr)
    '                    'where L.AmtDr>0  " & mCondStr & " Order By L.V_Date, Cast(Replace(L.RecId,'-','') as Integer) "


    '                    mQry = ""
    '                    If ReportFrm.FGetText(4) = "Financial Year Opening" Then
    '                        mQry = "select 'Opening' DocId, " & AgL.Chk_Date(DateAdd(DateInterval.Day, -1, CDate(AgL.PubStartDate))) & " as V_Date, 'Opening' as DocNo, Sum(L.AmtDr-L.AmtCr) as AmtDr, 
    '                        Null as DrNarration, 0 as Taxable_Amount, 0 as Tax_Amount
    '                        from Ledger L With (NoLock)
    '                        Left Join LedgerHead LH With (NoLock) on L.DocID = LH.DocID
    '                        Left Join SaleInvoice INV With (NoLock) On L.DocID = INV.DocID
    '                        Left Join RateType Rt On Inv.RateType = Rt.Code
    '                        where 1=1 " & mCondStrOp & " Having Sum(L.AmtDr-L.AmtCr) > 0 "

    '                        mQry = mQry & " Union All "
    '                    End If

    '                    mQry = mQry & "select L.DocId, IfNull(L.EffectiveDate,L.V_Date) as V_Date, L.V_Type || '-' || L.RecId as DocNo, L.AmtDr, 
    '                    (Case When IfNull(L.Chq_No,'') <>'' Then 'Chq : ' || IfNull(L.Chq_No,'') Else '' End) || 
    '                    (Case When IfNull(LH.PartyDocNo,'') <>'' Then 'Inv : ' || IfNull(LH.PartyDocNo,'') Else '' End) ||
    '                    (Case When IfNull(Inv.RateType,'') <>'' Then 'RT : ' || IfNull(RT.Description,'') Else '' End)
    '                    as DrNarration,
    '                    INV.Taxable_Amount, INV.Tax1+INV.Tax2+INV.Tax3+INV.Tax4+INV.Tax5 as Tax_Amount
    '                    from ledger L With (NoLock)
    '                    Left Join LedgerHead LH With (NoLock) on L.DocID = LH.DocID
    '                    Left Join SaleInvoice INV With (NoLock) On L.DocID = INV.DocID
    '                    Left Join RateType Rt On Inv.RateType = Rt.Code
    '                    where L.AmtDr>0  " & mCondStr & "  "

    '                    If ReportFrm.FGetText(4) = "Financial Year Opening" Then
    '                        mQry = mQry & " Order By V_Date,  DocNo "
    '                    Else
    '                        If AgL.PubServerName = "" Then
    '                            mQry = mQry & " Order By IfNull(L.EffectiveDate,L.V_Date), Try_Parse(Replace(L.RecId,'-','') as Integer) "
    '                        Else
    '                            mQry = mQry & " Order By IfNull(L.EffectiveDate,L.V_Date), Cast((Case When IsNumeric(Replace(L.RecId,'-',''))=1 Then Replace(L.RecId,'-','') Else Null End) as Integer) "
    '                        End If
    '                    End If



    '                    DtDr = AgL.FillData(mQry, AgL.GCn).Tables(0)
    '                    DrRecordCount = DtDr.Rows.Count

    '                    'mQry = "
    '                    'select L.DocId, L.V_Date, L.DivCode || L.site_Code || '-' || L.V_Type || '-' || L.RecId as DocNo, (Case When IfNull(Trd.Type,'')='Cancelled' OR IfNull(Trr.Type,'')='Cancelled' Then 0 Else L.AmtCr End) as AmtCr,
    '                    '(Case When IfNull(L.Chq_No,'') <>'' Then 'Chq : ' || IfNull(L.Chq_No,'') Else '' End) || 
    '                    '(Case When IfNull(LH.PartyDocNo,'') <>'' Then 'Inv : ' || IfNull(LH.PartyDocNo,'') Else '' End) ||
    '                    '(Case When IfNull(Trd.Type,'')='Cancelled' OR IfNull(Trr.Type,'')='Cancelled' Then ' Cancelled Amt.' || Cast(L.AmtCr as NVarchar) Else '' End)
    '                    'as CrNarration,
    '                    'INV.Taxable_Amount, INV.Tax1+INV.Tax2+INV.Tax3+INV.Tax4+INV.Tax5 as Tax_Amount
    '                    'from ledger L  With (NoLock)
    '                    'Left Join LedgerHead LH  With (NoLock) On L.DocID = LH.DocID
    '                    'Left Join PurchInvoice INV With (NoLock) On L.DocID = INV.DocID
    '                    'Left Join TransactionReferences Trd With (NoLock) On L.DocID = Trd.DocId And L.TSr = IfNull(Trd.DocIDSr, L.TSr)
    '                    'Left Join TransactionReferences Trr With (NoLock) On L.DocID = Trr.ReferenceDocId And L.TSr = IfNull(Trr.ReferenceSr, L.TSr)
    '                    'where L.AmtCr>0  " & mCondStr & " Order By L.V_Date,  Cast(Replace(L.RecId,'-','') as Integer) "



    '                    mQry = ""
    '                    If ReportFrm.FGetText(4) = "Financial Year Opening" Then
    '                        mQry = "select 'Opening' DocId, " & AgL.Chk_Date(DateAdd(DateInterval.Day, -1, CDate(AgL.PubStartDate))) & " as V_Date, 'Opening' as DocNo, Sum(L.AmtCr-L.AmtDr) as AmtCr, 
    '                        Null as CrNarration, 0 as Taxable_Amount, 0 as Tax_Amount
    '                        from Ledger L With (NoLock)
    '                        Left Join LedgerHead LH With (NoLock) on L.DocID = LH.DocID
    '                        where 1=1 " & mCondStrOp & " Having Sum(L.AmtCr-L.AmtDr) > 0 "

    '                        mQry = mQry & " Union All "
    '                    End If


    '                    mQry = mQry & "
    '                    select L.DocId, IfNull(L.EffectiveDate,L.V_Date) as V_Date, L.V_Type || '-' || L.RecId as DocNo, L.AmtCr,
    '                    (Case When IfNull(L.Chq_No,'') <>'' Then 'Chq : ' || IfNull(L.Chq_No,'') Else '' End) || 
    '                    (Case When IfNull(LH.PartyDocNo,'') <>'' Then 'Inv : ' || IfNull(LH.PartyDocNo,'') Else '' End) 
    '                    as CrNarration,
    '                    INV.Taxable_Amount, INV.Tax1+INV.Tax2+INV.Tax3+INV.Tax4+INV.Tax5 as Tax_Amount
    '                    from ledger L  With (NoLock)
    '                    Left Join LedgerHead LH  With (NoLock) On L.DocID = LH.DocID
    '                    Left Join PurchInvoice INV With (NoLock) On L.DocID = INV.DocID
    '                    where L.AmtCr>0  " & mCondStr & "  "

    '                    If ReportFrm.FGetText(4) = "Financial Year Opening" Then
    '                        mQry = mQry & " Order By V_Date,  DocNo "
    '                    Else
    '                        If AgL.PubServerName = "" Then
    '                            mQry = mQry & " Order By IfNull(L.EffectiveDate,L.V_Date),  Try_Parse(Replace(L.RecId,'-','') as Integer) "
    '                        Else
    '                            'mQry = mQry & " Order By IfNull(L.EffectiveDate,L.V_Date),  Try_Parse(Replace(L.RecId,'-','') as Integer) "
    '                            mQry = mQry & " Order By IfNull(L.EffectiveDate,L.V_Date), Cast((Case When IsNumeric(Replace(L.RecId,'-',''))=1 Then Replace(L.RecId,'-','') Else Null End) as Integer) "
    '                        End If
    '                    End If

    '                    DtCr = AgL.FillData(mQry, AgL.GCn).Tables(0)
    '                    CrRecordCount = DtCr.Rows.Count


    '                    Dim mRunningTotalDr As Double
    '                    Dim mRunningTotalCr As Double
    '                    Dim mDays As Integer
    '                    Dim mInterest As Integer
    '                    mRunningTotalDr = 0 : mRunningTotalCr = 0 : mLastChuktiAmount = 0 : mDays = 0 : mInterest = 0 : ConcurSr = -1 : FirstConcurSr = -1

    '                    LoopLimit = DrRecordCount + CrRecordCount ' IIf(DrRecordCount >= CrRecordCount, DrRecordCount, CrRecordCount)
    '                    For I = 0 To LoopLimit


    '                        If DrRecordCount > iDr Then
    '                            If iDr = 0 Or iCr >= CrRecordCount Or mRunningTotalDr <= mRunningTotalCr Then
    '                                mDays = DateDiff(DateInterval.Day, DateAdd(DateInterval.Day, Val(ReportFrm.FGetText(2)), DtDr.Rows(iDr)("V_Date")), CDate(ReportFrm.FGetText(1)))
    '                                If mDays < 0 Then mDays = 0
    '                                mInterest = Math.Round(Val(DtDr.Rows(iDr)("AmtDr")) * AgL.VNull(AgL.PubDtEnviro.Rows(0)("Default_DebtorsInterestRate")) * (mDays / 36500), 2)
    '                                mQry = "Insert Into #TempTblDr (DrDivision,DrSubcode,DrSr,DrDate,DrDocNo,DrAmount,DrTaxableAmount,DrTaxAmount,DrDays,DrInterest,DrNarration)
    '                                        Values(" & AgL.Chk_Text(mDivision) & ", " & AgL.Chk_Text(mSubcode) & ", " & DrSr & ", " & AgL.Chk_Date(AgL.XNull(DtDr.Rows(iDr)("V_Date"))) & ", " & AgL.Chk_Text(AgL.XNull(DtDr.Rows(iDr)("DocNo"))) & "," & AgL.VNull(DtDr.Rows(iDr)("AmtDr")) & "," & AgL.VNull(DtDr.Rows(iDr)("Taxable_Amount")) & "," & AgL.VNull(DtDr.Rows(iDr)("Tax_Amount")) & ", " & mDays & ", " & mInterest & "," & AgL.Chk_Text(AgL.XNull(DtDr.Rows(iDr)("DrNarration"))) & ")
    '                                       "
    '                                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
    '                                mRunningTotalDr += AgL.VNull(DtDr.Rows(iDr)("AmtDr"))
    '                                iDr += 1
    '                                DrSr += 1
    '                            End If
    '                        End If


    '                        If Math.Round(mRunningTotalDr, 2) = Math.Round(mRunningTotalCr, 2) And mLastChuktiAmount <> mRunningTotalDr And mRunningTotalDr > 0 Then
    '                            J = 0
    '                            mLastChuktiAmount = mRunningTotalDr
    '                            If DrSr > CrSr Then
    '                                For J = CrSr To DrSr - 1
    '                                    mQry = "Insert Into #TempTblCr(CrDivision,CrSubcode, CrSr) Values(" & AgL.Chk_Text(mDivision) & ", " & AgL.Chk_Text(mSubcode) & ", " & J & ")"
    '                                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
    '                                Next
    '                            ElseIf CrSr > DrSr Then
    '                                For J = DrSr To CrSr - 1
    '                                    mQry = "Insert Into #TempTblDr(DrDivision,DrSubcode,DrSr) Values(" & AgL.Chk_Text(mDivision) & ", " & AgL.Chk_Text(mSubcode) & "," & J & ")"
    '                                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
    '                                Next
    '                            End If



    '                            If iDr <= DrRecordCount Or iCr <= CrRecordCount Then
    '                                If J = 0 Then J = DrSr + 1
    '                                mQry = "Insert Into #TempTblDr(DrDivision,DrSubcode,DrSr) Values(" & AgL.Chk_Text(mDivision) & ", " & AgL.Chk_Text(mSubcode) & ", " & J & ")"
    '                                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)


    '                                mQry = "Insert Into #TempTblCr(CrDivision,CrSubcode, CrSr) Values(" & AgL.Chk_Text(mDivision) & ", " & AgL.Chk_Text(mSubcode) & ", " & J & ")"
    '                                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

    '                                ConcurSr = J
    '                                'If CDate(AgL.XNull(DtDr.Rows(iDr)("V_Date"))) < CDate(DateAdd(DateInterval.Year, -1, CDate(AgL.PubStartDate))) Then
    '                                If CDate(AgL.XNull(DtDr.Rows(iDr - 1)("V_Date"))) < CDate(AgL.PubStartDate) Then
    '                                    FirstConcurSr = ConcurSr
    '                                End If

    '                                DrSr = J + 1
    '                                CrSr = J + 1

    '                                LoopLimit += 1
    '                            End If
    '                        End If



    '                        If CrRecordCount > iCr Then
    '                            If iCr = 0 Or iDr >= DrRecordCount Or mRunningTotalDr > mRunningTotalCr Then
    '                                'mDays = DateDiff(DateInterval.Day, DateAdd(DateInterval.Day, Val(ReportFrm.FGetText(1)), AgL.XNull(DtCr.Rows(iCr)("V_Date"))), CDate(ReportFrm.FGetText(0)))
    '                                mDays = DateDiff(DateInterval.Day, AgL.XNull(DtCr.Rows(iCr)("V_Date")), CDate(ReportFrm.FGetText(1)))
    '                                mInterest = Math.Round(Val(DtCr.Rows(iCr)("AmtCr")) * AgL.VNull(AgL.PubDtEnviro.Rows(0)("Default_DebtorsInterestRate")) * (mDays / 36500), 2)
    '                                mQry = "Insert Into #TempTblCr (CrDivision, CrSubcode, CrSr,CrDate,CrDocNo,CrAmount,CrTaxableAmount,CrTaxAmount,CrDays,CrInterest,CrNarration)
    '                                        Values(" & AgL.Chk_Text(mDivision) & ", " & AgL.Chk_Text(mSubcode) & ", " & CrSr & ", " & AgL.Chk_Date(AgL.XNull(DtCr.Rows(iCr)("V_Date"))) & ", " & AgL.Chk_Text(AgL.XNull(DtCr.Rows(iCr)("DocNo"))) & "," & AgL.VNull(DtCr.Rows(iCr)("AmtCr")) & "," & AgL.VNull(DtCr.Rows(iCr)("Taxable_Amount")) & "," & AgL.VNull(DtCr.Rows(iCr)("Tax_Amount")) & ", " & mDays & ", " & mInterest & "," & AgL.Chk_Text(AgL.XNull(DtCr.Rows(iCr)("CrNarration"))) & ")
    '                                       "
    '                                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
    '                                mRunningTotalCr += AgL.VNull(DtCr.Rows(iCr)("AmtCr"))
    '                                iCr += 1
    '                                CrSr += 1
    '                            End If
    '                        End If

    '                        'Try
    '                        '    Debug.Print("Dr : " + AgL.XNull(DtDr.Rows(iDr - 1)("V_Date")) + "   Amt : " + Val(DtDr.Rows(iDr - 1)("AmtDr")).ToString() + "   RunningTotal : " + mRunningTotalDr.ToString)
    '                        'Catch ex As Exception
    '                        'End Try
    '                        'Try
    '                        '    Debug.Print("Cr : " + AgL.XNull(DtCr.Rows(iCr - 1)("V_Date")) + "   Amt : " + Val(DtCr.Rows(iCr - 1)("AmtCr")).ToString() + "   RunningTotal : " + mRunningTotalCr.ToString)
    '                        'Catch ex As Exception
    '                        'End Try


    '                        If Math.Round(mRunningTotalDr, 2) = Math.Round(mRunningTotalCr, 2) And mLastChuktiAmount <> mRunningTotalDr And mRunningTotalDr > 0 Then
    '                            J = 0
    '                            mLastChuktiAmount = mRunningTotalDr
    '                            If DrSr > CrSr Then
    '                                For J = CrSr To DrSr - 1
    '                                    mQry = "Insert Into #TempTblCr(CrDivision,CrSubcode, CrSr) Values(" & AgL.Chk_Text(mDivision) & ", " & AgL.Chk_Text(mSubcode) & ", " & J & ")"
    '                                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
    '                                Next
    '                            ElseIf CrSr > DrSr Then
    '                                For J = DrSr To CrSr - 1
    '                                    mQry = "Insert Into #TempTblDr(DrDivision,DrSubcode,DrSr) Values(" & AgL.Chk_Text(mDivision) & ", " & AgL.Chk_Text(mSubcode) & "," & J & ")"
    '                                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
    '                                Next
    '                            End If



    '                            If iDr <= DrRecordCount Or iCr <= CrRecordCount Then
    '                                If J = 0 Then J = DrSr + 1
    '                                mQry = "Insert Into #TempTblDr(DrDivision,DrSubcode,DrSr) Values(" & AgL.Chk_Text(mDivision) & ", " & AgL.Chk_Text(mSubcode) & ", " & J & ")"
    '                                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)


    '                                mQry = "Insert Into #TempTblCr(CrDivision,CrSubcode, CrSr) Values(" & AgL.Chk_Text(mDivision) & ", " & AgL.Chk_Text(mSubcode) & ", " & J & ")"
    '                                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

    '                                ConcurSr = J
    '                                'If CDate(AgL.XNull(DtCr.Rows(iCr)("V_Date"))) < CDate(DateAdd(DateInterval.Year, -1, CDate(AgL.PubStartDate))) Then
    '                                If CDate(AgL.XNull(DtCr.Rows(iCr - 1)("V_Date"))) < CDate(AgL.PubStartDate) Then
    '                                    FirstConcurSr = ConcurSr
    '                                End If


    '                                DrSr = J + 1
    '                                CrSr = J + 1

    '                                LoopLimit += 1
    '                            End If
    '                        End If
    '                    Next

    '                    If DrSr > CrSr Then
    '                        For J = CrSr To DrSr - 1
    '                            mQry = "Insert Into #TempTblCr(CrDivision,CrSubcode, CrSr) Values(" & AgL.Chk_Text(mDivision) & ", " & AgL.Chk_Text(mSubcode) & ", " & J & ")"
    '                            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
    '                        Next
    '                    ElseIf CrSr > DrSr Then
    '                        For J = DrSr To CrSr - 1
    '                            mQry = "Insert Into #TempTblDr(DrDivision,DrSubcode, DrSr) Values(" & AgL.Chk_Text(mDivision) & ", " & AgL.Chk_Text(mSubcode) & ", " & J & ")"
    '                            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
    '                        Next
    '                    End If


    '                    mQry = "Insert Into #TempTblDrCr (DrDivision,DrSubcode, DrSr, DrDate, DrDocNo, DrAmount, DrTaxableAmount, DrTaxAmount, DrDays, DrInterest, DrNarration,
    '                        CrDivision,CrSubcode, CrSr, CrDate, CrDocNo, CrAmount, CrTaxableAmount, CrTaxAmount, CrDays, CrInterest, CrNarration) 
    '                    Select Dr.DrDivision, Dr.DrSubcode, Dr.DrSr, Dr.DrDate, Dr.DrDocNo, Dr.DrAmount, Dr.DrTaxableAmount, Dr.DrTaxAmount, Dr.DrDays, Dr.DrInterest, Dr.DrNarration, 
    '                        Cr.CrDivision,Cr.CrSubcode, Cr.CrSr, Cr.CrDate, Cr.CrDocNo, Cr.CrAmount, Cr.CrTaxableAmount, Cr.CrTaxAmount, Cr.CrDays, Cr.CrInterest, Cr.CrNarration
    '                    From #TempTblDr Dr, #TempTblCr Cr Where Dr.DrDivision = Cr.CrDivision And  Dr.DrSubcode = Cr.CrSubcode And Dr.DrSr = Cr.CrSr "
    '                    If ReportFrm.FGetText(4) = "After Chukti" Then
    '                        mQry = mQry & " And Dr.DrSr > " & ConcurSr & ""
    '                    ElseIf ReportFrm.FGetText(4) = "Financial Year" Then
    '                        mQry = mQry & " And Dr.DrSr > " & FirstConcurSr & ""
    '                    End If
    '                    mQry = mQry & " Order By Dr.DrSr "

    '                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

    '                    mQry = "Delete From #TempTblDr"
    '                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

    '                    mQry = "Delete From #TempTblCr"
    '                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
    '                Next iDivision
    '            Next iSubcode






    '            mQry = "Select D.Name as DivisionName, Sg.Name as PartyName, Sg.Address, Sg.Mobile, Agent.Name as AgentName, H.*, 
    '                    Gr.GrReturnAmt, Gr.GrSaleAmt, Gr.ReturnPer 
    '                    from #TempTblDrCr H 
    '                    Left Join viewHelpSubgroup Sg on H.DrSubcode COLLATE DATABASE_DEFAULT = Sg.Code COLLATE DATABASE_DEFAULT
    '                    Left Join viewHelpSubgroup D On D.Code COLLATE DATABASE_DEFAULT = H.DrDivision COLLATE DATABASE_DEFAULT
    '                    Left Join (
    '                                select subcode, Max(Agent) as Agent
    '                                From SubgroupSiteDivisionDetail
    '                                Group By Subcode
    '                              ) as LTV On LTV.Subcode = Sg.Code
    '                    Left Join viewHelpSubgroup Agent  On LTV.Agent COLLATE DATABASE_DEFAULT = Agent.Code COLLATE DATABASE_DEFAULT
    '                    Left Join (
    '                                SELECT L.SubCode, L.DivCode, Sum(L.AmtCr) AS GrReturnAmt, (CASE WHEN Sum(L.AmtDr) = 0 THEN Sum(L.AmtCr) ELSE Sum(L.AmtDr) END) AS GrSaleAmt,  Round((Sum(L.AmtCr) /  (CASE WHEN Sum(L.AmtDr) = 0 THEN Sum(L.AmtCr) ELSE Sum(L.AmtDr) END))*100,2) 	AS ReturnPer
    '                                FROM Ledger L With (NoLock)
    '                                LEFT JOIN Voucher_Type VT With (NoLock) ON L.V_Type = vt.V_type
    '                                LEFT JOIN subgroup Sg  With (NoLock) ON L.SubCode = Sg.Subcode 
    '                                WHERE VT.NCat IN ('SI','SR') OR VT.V_Type ='OB' AND Sg.Nature ='Customer'
    '                                GROUP BY L.SubCode, L.DivCode  
    '                                HAVING Sum(L.AmtCr) > 0
    '                              ) as Gr On Gr.Subcode  COLLATE DATABASE_DEFAULT = H.DrSubcode  COLLATE DATABASE_DEFAULT And Gr.DivCode  COLLATE DATABASE_DEFAULT = H.DrDivision  COLLATE DATABASE_DEFAULT
    '                    Order By H.DrSubcode, H.DrSr"
    '            DsRep = AgL.FillData(mQry, AgL.GCn)

    '            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records to Print!")

    '            ReportFrm.PrintReport(DsRep, RepName, RepTitle)
    '        Catch ex As Exception
    '            MsgBox(ex.Message)
    '            DsRep = Nothing
    '        End Try
    '    End Sub

    '#End Region

#Region "Aadhat Ledger"
    Private Sub ProcAadhatLedger()
        Dim mCondStr$ = ""


        Try
            RepName = "AadhatLedger" : RepTitle = "Aadhat Ledger"


            mCondStr = " "
            mCondStr = mCondStr & " AND Date(L.V_Date) <= " & AgL.Chk_Date(ReportFrm.FGetText(1)) & " "
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("L.LinkedSubcode", 2), "''", "'")
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("L.Subcode", 3), "''", "'")
            'mCondStr = mCondStr & " And  L.Subcode In (" & ReportFrm.FGetCode(3) & ")"
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("L.DivCode", 5), "''", "'")
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("L.Site_Code", 6), "''", "'")
            If ReportFrm.FGetText(7) = "After Settlement" Then
                mCondStr = mCondStr & " And L.DocID || L.V_SNo Not In (SELECT H.PurchaseInvoiceDocId || H.PurchaseInvoiceDocIdSr   FROM Cloth_SupplierSettlementInvoices H
                                                                       UNION ALL 
                                                                       SELECT H.PaymentDocId || H.PaymentDocIdSr   FROM Cloth_SupplierSettlementPayments H
                                                                       ) "
                mCondStr = mCondStr & " And L.DocID Not In ( SELECT H.DocID FROM LedgerHead H LEFT JOIN LedgerHeadDetail L ON H.DocID = L.DocID WHERE H.V_Type ='WRS' ) "
            End If



            mQry = "
                    SELECT SubCode, Max(Party.Name) as PartyName, Max(VMain.Customer) as Customer, Max(Cust.Name)  as CustomerName,
                    Max(CASE WHEN VMain.DocDateActualFormat<" & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & " THEN Null ELSE VMain.SearchCode END) SearchCode,                    
                    Max(CASE WHEN VMain.DocDateActualFormat<" & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & " THEN Null ELSE VMain.Site END) Site, 
                    (CASE WHEN VMain.DocDateActualFormat<" & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & " THEN strftime('%d/%m/%Y'," & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & ") ELSE VMain.DocDate END) DocDate, 
                    Max(CASE WHEN VMain.DocDateActualFormat<" & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & " THEN Null ELSE VMain.DocType END) DocType, 
                    VMain.DocNo as DocNo,                                        
                    Max(CASE WHEN VMain.DocDateActualFormat<" & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & " THEN Null ELSE VMain.Brand END) Brand, 
                    Max(CASE WHEN VMain.DocDateActualFormat<" & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & " THEN Null ELSE VMain.LrNo END) LrNo, 
                    Sum(CASE WHEN VMain.DocDateActualFormat<" & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & " THEN 0 ELSE VMain.TaxableAmount End) TaxableAmount, 
                    Sum(CASE WHEN VMain.DocDateActualFormat<" & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & " THEN 0 ELSE VMain.TaxAmount End) TaxAmount, 
                    Sum(CASE WHEN VMain.DocDateActualFormat<" & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & " THEN 0 ELSE VMain.InvoiceAmount End) InvoiceAmount, 
                    Sum(CASE WHEN VMain.DocDateActualFormat<" & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & " THEN 0 ELSE VMain.GrAmount End) GrAmount, 
                    (CASE WHEN VMain.DocDateActualFormat<" & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & " THEN (Case When Sum(VMain.AmtDr)-Sum(VMain.AmtCr) > 0 Then Sum(VMain.AmtDr)-Sum(VMain.AmtCr) Else 0 End) ELSE Sum(VMain.AmtDr) END) AmtDr, 
                    (CASE WHEN VMain.DocDateActualFormat<" & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & " THEN (Case When Sum(VMain.AmtCr)-Sum(VMain.AmtDr) > 0 Then Sum(VMain.AmtCr)-Sum(VMain.AmtDr) Else 0 End) ELSE Sum(VMain.AmtCr) END) AmtCr, 
                    0 as Balance,                                            
                    Max(CASE WHEN VMain.DocDateActualFormat<" & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & " THEN Null ELSE VMain.WStatus END) WaStatus, 
                    Max(CASE WHEN VMain.DocDateActualFormat<" & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & " THEN Null ELSE VMain.WDocType END) WaDocType, 
                    VMain.WDocNo as WaDocNo,                                        
                    Sum(CASE WHEN VMain.DocDateActualFormat<" & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & " THEN 0 ELSE VMain.WGrossAmount End) WGrossAmount, 
                    Sum(CASE WHEN VMain.DocDateActualFormat<" & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & " THEN 0 ELSE VMain.WAdditionalAmount End) WAdditionalAmount, 
                    Sum(CASE WHEN VMain.DocDateActualFormat<" & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & " THEN 0 ELSE VMain.WInvoiceAmount End) [Wa Invoice Amount], 
                    (CASE WHEN VMain.DocDateActualFormat<" & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & " THEN (Case When Sum(VMain.WAmtDr)-Sum(VMain.WAmtCr) > 0 Then Sum(VMain.WAmtDr)-Sum(VMain.WAmtCr) Else 0 End) ELSE Sum(VMain.WAmtDr) END) WaAmtDr, 
                    (CASE WHEN VMain.DocDateActualFormat<" & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & " THEN (Case When Sum(VMain.WAmtCr)-Sum(VMain.WAmtDr) > 0 Then Sum(VMain.WAmtCr)-Sum(VMain.WAmtDr) Else 0 End) ELSE Sum(VMain.WAmtCr) END) WaAmtCr, 
                    Sum(CASE WHEN VMain.DocDateActualFormat<" & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & " THEN 0 ELSE VMain.WGrAmount End) WGrAmount, 
                    Sum(CASE WHEN VMain.DocDateActualFormat<" & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & " THEN 0 ELSE VMain.WPayment End) WPayment,                     
                    Sum(CASE WHEN VMain.DocDateActualFormat<" & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & " THEN 0 ELSE VMain.WDCNote End) WDebitCreditNote,
                    0 as WaBalance,
                    (CASE WHEN VMain.DocDateActualFormat<" & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & " THEN (Case When Sum(VMain.TotalDr)-Sum(VMain.TotalCr) > 0 Then Sum(VMain.TotalDr)-Sum(VMain.TotalCr) Else 0 End) ELSE Sum(VMain.TotalDr) END)  TotalDr, 
                    (CASE WHEN VMain.DocDateActualFormat<" & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & " THEN (Case When Sum(VMain.TotalCr)-Sum(VMain.TotalDr) > 0 Then Sum(VMain.TotalCr)-Sum(VMain.TotalDr) Else 0 End) ELSE Sum(VMain.TotalCr) END)  TotalCr                     
                    FROM
                    (

                        SELECT VTemp.Subcode, Max(VTemp.Customer) as Customer, Max(VTemp.SearchCode) AS SearchCode, Max(VTemp.DocID) AS DocID, 
					                        Max(VTemp.Site) AS Site, 
					                        Max(VTemp.RecID) AS DocNo, 
					                        strftime('%d/%m/%Y',IfNull(Max(VTemp.V_Date),Max(VTemp.WV_Date))) AS DocDate, 
                                            IfNull(Max(VTemp.V_Date),Max(VTemp.WV_Date)) AS DocDateActualFormat, 
					                        Max(VTemp.V_Type) AS DocType,
					                        IfNull(Sum(VTemp.AmtDr),0)+IfNull(Sum(VTemp.WAmtDr),0) AS TotalDr, IfNull(Sum(VTemp.AmtCr),0)+IfNull(Sum(VTemp.WAmtCr),0) AS TotalCr, 
                                            Max(VTemp.Brand) AS Brand, Max(VTemp.LRNo) AS LRNo, Sum(VTemp.TaxableAmount) AS TaxableAmount, 
                                            Sum(VTemp.TaxAmount) AS TaxAmount, Sum(VTemp.InvoiceAmount) AS InvoiceAmount, 
                                            Sum(CASE WHEN VTemp.NCat ='SR' THEN VTemp.AmtCr ELSE 0 End) AS GrAmount, 
                                            Sum(VTemp.AmtDr) AS AmtDr, 
                                            Sum(VTemp.AmtCr) AS AmtCr,
                                            Max(VTemp.WStatus) AS WStatus, 
                                            Max(VTemp.WDocID) AS WDocId, 
					                        (Case When IfNull(Max(VTemp.WStatus),'') = 'Pending' Then 'Pending' Else Max(VTemp.WRecID) End)  AS WDocNo, 					                        
					                        Max(VTemp.WV_Type) AS WDocType,
                                            Sum(VTemp.WGrossAmount) AS WGrossAmount, 
                                            Sum(VTemp.WAdditionAmount) AS WAdditionalAmount, Sum(VTemp.WInvoiceAmount) AS WInvoiceAmount, 
                                            Sum(VTemp.WAmtDr) AS WAmtDr, Sum(VTemp.WAmtCr) AS WAmtCr, 
                                            Sum(CASE WHEN VTemp.NCat ='RCT' THEN VTemp.WAmtCr ELSE 0 End) AS WPayment, 
                                            Sum(CASE WHEN VTemp.NCat ='SR' THEN VTemp.AmtCr ELSE 0 End) AS WGrAmount, 
                                            Sum(CASE WHEN VTemp.NCat <> 'RCT' And VTemp.NCat <> 'SR' THEN VTemp.WAmtCr ELSE 0 End) AS WDCNote
                                            
					
                        FROM
                        (					
                            Select (CASE WHEN DATE(L.V_Date) < '2019-04-01' THEN strftime('%m-%Y',L.V_Date) ELSE CR.Code End) as Code, L.LinkedSubcode AS SubCode, L.Subcode as Customer, (CASE WHEN DATE(L.V_Date) < '2019-04-01' THEN strftime('%m-%Y',L.V_Date) ELSE L.DocID End)  AS SearchCode, L.DocID || L.V_SNo as DocID, L.V_SNo, Site.ShortName AS Site, (CASE WHEN DATE(L.V_Date) < '2019-04-01' THEN strftime('%m-%Y',L.V_Date) ELSE L.RecId End) as RecID , L.V_Date, L.V_Type, Vt.Ncat, 
                                                (Case When Vt.Ncat = '" & Ncat.Receipt & "' Then L.Chq_No Else S.Brand End) Brand, IfNull(S.LRNo,'') || (Case When L.Chq_No Is Not Null Then 'Chq:' || L.Chq_No Else '' End) as LrNo, S.TaxableAmount, S.TaxAmount, S.InvoiceAmount, L.AmtDr, L.AmtCr,
					                            (Case When S.DocID Is Not Null And S.LEDocID Is Null Then 'Pending' Else Null End) as WStatus, NULL AS WDocID, NULL AS WRecID, NULL AS WV_Date, NULL AS WV_Type, 0 WGrossAmount, 0 WAdditionAmount, 0 WInvoiceAmount, 0 WAmtDr, 0 WAmtCr                    
                                                From Ledger L
                                                Left Join (
                    			                            Select SI.DociD, Max(SI.V_Type) AS V_Type, Max(CASE When I.ItemType = '" & ItemTypeCode.ServiceProduct & "' Then Null WHEN I.V_Type ='Item' THEN IG.Description ELSE I.Description End) AS Brand, Max(SI.Taxable_Amount) AS TaxableAmount,
                    			                            Max(SI.Tax1) + Max(SI.Tax2) + Max(SI.Tax3) + Max(SI.Tax4) + Max(SI.Tax5) AS TaxAmount,
                    			                            Max(SI.Net_Amount) AS InvoiceAmount, SIT.LrNo || (Case When SIT.NoOfBales Is Not Null Then ' @ ' ||  Cast(SIT.NoOfBales as nVarchar) Else '' End) as LrNo, LE.DocID as LeDocID
                                                            From SaleInvoice SI
                                                            Left Join SaleInvoiceDetail SIL On SI.DocID = SIL.DocId
                                                            LEFT JOIN SaleInvoiceTransport SIT ON SI.DocID = SIT.DocID                                 
                                                            Left Join Item I On SIL.Item = I.Code
                                                            Left JOIN Item IG ON I.ItemGroup = IG.Code 
                                                            LEFT JOIN Voucher_Type Vt ON SI.V_Type = VT.V_Type 
                                                            Left Join SaleInvoiceGeneratedEntries LE On SI.DocID = LE.DocID
                                                            WHERE Vt.NCat = 'SI' And SI.V_Type='SI' --And I.ItemType='TP'
                                                            GROUP BY SI.DocID                                
                                                          ) AS S ON L.DocID = S.DocID 
                                                LEFT JOIN SaleInvoiceGeneratedEntries CR ON L.DocId = CR.DocId
                                                LEFT JOIN SiteMast Site ON L.Site_Code = Site.Code                     
                                                LEFT JOIN voucher_type Vt ON L.V_Type = Vt.V_Type 
                            WHERE Substr(L.V_Type,1,1)<> 'W' And 1=1 " & mCondStr & "

                            UNION All

                            SELECT (CASE WHEN DATE(L.V_Date) < '2019-04-01' THEN strftime('%m-%Y',L.V_Date) ELSE Cr.Code End) as Code, L.LinkedSubCode AS SubCode, L.Subcode as Customer, (CASE WHEN DATE(L.V_Date) < '2019-04-01' THEN strftime('%m-%Y',L.V_Date) ELSE L.DocID End)  AS SearchCode, Null  DocId, L.V_SNo, Site.ShortName AS Site, NULL RecId, NULL V_Date, NULL V_Type, Vt.nCat, 
                                                S.Brand, S.LRNo, 0 TaxableAmount, 0 TaxAmount, 0 InvoiceAmount, 0 AmtDr, 0 AmtCr,
                                                Null as WStatus, (CASE WHEN DATE(L.V_Date) < '2019-04-01' THEN strftime('%m-%Y',L.V_Date) ELSE L.DocId End) AS WDocID, (CASE WHEN DATE(L.V_Date) < '2019-04-01' THEN strftime('%m-%Y',L.V_Date) ELSE L.RecId End) AS WRecID, L.V_Date AS WV_Date, L.V_Type AS WV_Type, IfNull(S.GrossAmount,0) AS WGrossAmount, IfNull(S.AdditionalAmount,0) AS WAdditionAmount, IfNull(S.InvoiceAmount,0) AS WInvoiceAmount, L.AmtDr AS WAmtDr, L.AmtCr WAmtCr
                                                From Ledger L
                                                Left Join (
                    			                            Select SI.DociD, Max(SI.V_Type) AS V_Type, Max(CASE WHEN I.V_Type ='Item' THEN IG.Description ELSE I.Description End) AS Brand, Sum(CASE WHEN I.ItemType = 'TP' THEN SIL.Amount ELSE 0 End) AS GrossAmount,
                    			                            Sum(CASE WHEN I.ItemType <> 'TP' THEN SIL.Amount ELSE 0 End) AS AdditionalAmount,
                    			                            Max(SI.Net_Amount) AS InvoiceAmount, SIT.LrNo || (Case When SIT.NoOfBales Is Not Null Then ' # ' ||  SIT.NoOfBales Else '' End) as LrNo
                                                            From SaleInvoice SI
                                                            Left Join SaleInvoiceDetail SIL On SI.DocID = SIL.DocId
                                                            LEFT JOIN SaleInvoiceTransport SIT ON SI.DocID = SIT.DocID                                 
                                                            Left Join Item I On SIL.Item = I.Code
                                                            Left JOIN Item IG ON I.ItemGroup = IG.Code 
                                                            LEFT JOIN Voucher_Type Vt ON SI.V_Type = VT.V_Type 
                                                            WHERE Vt.NCat = 'SI' And SI.V_Type='WSI'  --And I.ItemType='TP'
                                                            GROUP BY SI.DocID                                
                                                          ) AS S ON L.DocID = S.DocID 
                                                LEFT JOIN SaleInvoiceGeneratedEntries CR ON L.DocId = CR.DocId                               
                                                LEFT JOIN SiteMast Site ON L.Site_Code = Site.Code
                                                LEFT JOIN voucher_type Vt ON L.V_Type = Vt.V_Type                      
                            WHERE Substr(L.V_Type,1,1) = 'W' And 1=1 " & mCondStr & " 
                            ) AS VTemp
                            GROUP BY VTemp.Subcode, VTemp.Customer, IfNull(VTemp.Code, IfNull(VTemp.DocID,VTemp.WDocID))
                    ) AS VMain
                    Left Join viewHelpSubgroup Party On VMain.Subcode = Party.Code
                    Left Join viewHelpSubgroup Cust On VMain.Customer = Cust.Code
                    GROUP BY VMain.Subcode, VMain.Customer, CASE WHEN VMain.DocDateActualFormat<" & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & " THEN Null ELSE VMain.SearchCode END, (CASE WHEN VMain.DocDateActualFormat<'2019-01-01' THEN Null ELSE VMain.LrNo END)
                    ORDER BY VMain.Subcode,
		            VMain.DocDateActualFormat, VMain.DocType, VMain.DocNo, VMain.LrNo
                   "

            DsRep = AgL.FillData(mQry, AgL.GCn)

            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records to Print!")

            ReportFrm.PrintReport(DsRep, RepName, RepTitle)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub


#End Region

#Region "Aadhat Ledger Creditors"
    Private Sub ProcAadhatLedgerCreditors()
        Dim mCondStr$ = ""


        Try
            RepName = "AadhatLedger" : RepTitle = "Aadhat Ledger"


            mCondStr = " "
            mCondStr = mCondStr & " AND Date(L.V_Date) <= " & AgL.Chk_Date(ReportFrm.FGetText(1)) & " "
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("L.LinkedSubcode", 2), "''", "'")
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("L.Subcode", 3), "''", "'")
            'mCondStr = mCondStr & " And  L.Subcode In (" & ReportFrm.FGetCode(3) & ")"
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("L.DivCode", 5), "''", "'")
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("L.Site_Code", 6), "''", "'")
            If ReportFrm.FGetText(7) = "After Settlement" Then
                mCondStr = mCondStr & " And L.DocID || L.V_SNo Not In (SELECT H.PurchaseInvoiceDocId || H.PurchaseInvoiceDocIdSr   FROM Cloth_SupplierSettlementInvoices H
                                                                       UNION ALL 
                                                                       SELECT H.PaymentDocId || H.PaymentDocIdSr   FROM Cloth_SupplierSettlementPayments H
                                                                       ) "
                mCondStr = mCondStr & " And L.DocID Not In ( SELECT H.DocID FROM LedgerHead H LEFT JOIN LedgerHeadDetail L ON H.DocID = L.DocID WHERE H.V_Type ='WRS' ) "
            End If



            mQry = "
                    SELECT SubCode, Max(Party.Name) as PartyName, Max(VMain.Customer) as Customer, Max(Cust.Name)  as CustomerName,
                    Max(CASE WHEN VMain.DocDateActualFormat<" & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & " THEN Null ELSE VMain.SearchCode END) SearchCode,                    
                    Max(CASE WHEN VMain.DocDateActualFormat<" & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & " THEN Null ELSE VMain.Site END) Site, 
                    (CASE WHEN VMain.DocDateActualFormat<" & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & " THEN strftime('%d/%m/%Y'," & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & ") ELSE VMain.DocDate END) DocDate, 
                    Max(CASE WHEN VMain.DocDateActualFormat<" & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & " THEN Null ELSE VMain.DocType END) DocType, 
                    VMain.DocNo as DocNo,                                        
                    Max(CASE WHEN VMain.DocDateActualFormat<" & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & " THEN Null ELSE VMain.Brand END) Brand, 
                    Max(CASE WHEN VMain.DocDateActualFormat<" & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & " THEN Null ELSE VMain.LrNo END) LrNo, 
                    Sum(CASE WHEN VMain.DocDateActualFormat<" & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & " THEN 0 ELSE VMain.TaxableAmount End) TaxableAmount, 
                    Sum(CASE WHEN VMain.DocDateActualFormat<" & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & " THEN 0 ELSE VMain.TaxAmount End) TaxAmount, 
                    Sum(CASE WHEN VMain.DocDateActualFormat<" & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & " THEN 0 ELSE VMain.InvoiceAmount End) InvoiceAmount, 
                    Sum(CASE WHEN VMain.DocDateActualFormat<" & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & " THEN 0 ELSE VMain.GrAmount End) GrAmount, 
                    (CASE WHEN VMain.DocDateActualFormat<" & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & " THEN (Case When Sum(VMain.AmtDr)-Sum(VMain.AmtCr) > 0 Then Sum(VMain.AmtDr)-Sum(VMain.AmtCr) Else 0 End) ELSE Sum(VMain.AmtDr) END) AmtDr, 
                    (CASE WHEN VMain.DocDateActualFormat<" & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & " THEN (Case When Sum(VMain.AmtCr)-Sum(VMain.AmtDr) > 0 Then Sum(VMain.AmtCr)-Sum(VMain.AmtDr) Else 0 End) ELSE Sum(VMain.AmtCr) END) AmtCr, 
                    0 as Balance,                                            
                    Max(CASE WHEN VMain.DocDateActualFormat<" & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & " THEN Null ELSE VMain.WStatus END) WaStatus, 
                    Max(CASE WHEN VMain.DocDateActualFormat<" & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & " THEN Null ELSE VMain.WDocType END) WaDocType, 
                    VMain.WDocNo as WaDocNo,                                        
                    Sum(CASE WHEN VMain.DocDateActualFormat<" & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & " THEN 0 ELSE VMain.WGrossAmount End) WGrossAmount, 
                    Sum(CASE WHEN VMain.DocDateActualFormat<" & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & " THEN 0 ELSE VMain.WAdditionalAmount End) WAdditionalAmount, 
                    Sum(CASE WHEN VMain.DocDateActualFormat<" & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & " THEN 0 ELSE VMain.WInvoiceAmount End) [Wa Invoice Amount], 
                    (CASE WHEN VMain.DocDateActualFormat<" & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & " THEN (Case When Sum(VMain.WAmtDr)-Sum(VMain.WAmtCr) > 0 Then Sum(VMain.WAmtDr)-Sum(VMain.WAmtCr) Else 0 End) ELSE Sum(VMain.WAmtDr) END) WaAmtDr, 
                    (CASE WHEN VMain.DocDateActualFormat<" & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & " THEN (Case When Sum(VMain.WAmtCr)-Sum(VMain.WAmtDr) > 0 Then Sum(VMain.WAmtCr)-Sum(VMain.WAmtDr) Else 0 End) ELSE Sum(VMain.WAmtCr) END) WaAmtCr, 
                    Sum(CASE WHEN VMain.DocDateActualFormat<" & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & " THEN 0 ELSE VMain.WGrAmount End) WGrAmount, 
                    Sum(CASE WHEN VMain.DocDateActualFormat<" & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & " THEN 0 ELSE VMain.WPayment End) WPayment,                     
                    Sum(CASE WHEN VMain.DocDateActualFormat<" & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & " THEN 0 ELSE VMain.WDCNote End) WDebitCreditNote,
                    0 as WaBalance,
                    (CASE WHEN VMain.DocDateActualFormat<" & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & " THEN (Case When Sum(VMain.TotalDr)-Sum(VMain.TotalCr) > 0 Then Sum(VMain.TotalDr)-Sum(VMain.TotalCr) Else 0 End) ELSE Sum(VMain.TotalDr) END)  TotalDr, 
                    (CASE WHEN VMain.DocDateActualFormat<" & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & " THEN (Case When Sum(VMain.TotalCr)-Sum(VMain.TotalDr) > 0 Then Sum(VMain.TotalCr)-Sum(VMain.TotalDr) Else 0 End) ELSE Sum(VMain.TotalCr) END)  TotalCr                     
                    FROM
                    (

                        SELECT VTemp.Subcode, Max(VTemp.Customer) as Customer, Max(VTemp.SearchCode) AS SearchCode, Max(VTemp.DocID) AS DocID, 
					                        Max(VTemp.Site) AS Site, 
					                        Max(VTemp.RecID) AS DocNo, 
					                        strftime('%d/%m/%Y',IfNull(Max(VTemp.V_Date),Max(VTemp.WV_Date))) AS DocDate, 
                                            IfNull(Max(VTemp.V_Date),Max(VTemp.WV_Date)) AS DocDateActualFormat, 
					                        Max(VTemp.V_Type) AS DocType,
					                        IfNull(Sum(VTemp.AmtDr),0)+IfNull(Sum(VTemp.WAmtDr),0) AS TotalDr, IfNull(Sum(VTemp.AmtCr),0)+IfNull(Sum(VTemp.WAmtCr),0) AS TotalCr, 
                                            Max(VTemp.Brand) AS Brand, Max(VTemp.LRNo) AS LRNo, Sum(VTemp.TaxableAmount) AS TaxableAmount, 
                                            Sum(VTemp.TaxAmount) AS TaxAmount, Sum(VTemp.InvoiceAmount) AS InvoiceAmount, 
                                            Sum(CASE WHEN VTemp.NCat ='SR' THEN VTemp.AmtCr ELSE 0 End) AS GrAmount, 
                                            Sum(VTemp.AmtDr) AS AmtDr, 
                                            Sum(VTemp.AmtCr) AS AmtCr,
                                            Max(VTemp.WStatus) AS WStatus, 
                                            Max(VTemp.WDocID) AS WDocId, 
					                        (Case When IfNull(Max(VTemp.WStatus),'') = 'Pending' Then 'Pending' Else Max(VTemp.WRecID) End)  AS WDocNo, 					                        
					                        Max(VTemp.WV_Type) AS WDocType,
                                            Sum(VTemp.WGrossAmount) AS WGrossAmount, 
                                            Sum(VTemp.WAdditionAmount) AS WAdditionalAmount, Sum(VTemp.WInvoiceAmount) AS WInvoiceAmount, 
                                            Sum(VTemp.WAmtDr) AS WAmtDr, Sum(VTemp.WAmtCr) AS WAmtCr, 
                                            Sum(CASE WHEN VTemp.NCat ='RCT' THEN VTemp.WAmtCr ELSE 0 End) AS WPayment, 
                                            Sum(CASE WHEN VTemp.NCat ='SR' THEN VTemp.AmtCr ELSE 0 End) AS WGrAmount, 
                                            Sum(CASE WHEN VTemp.NCat <> 'RCT' And VTemp.NCat <> 'SR' THEN VTemp.WAmtCr ELSE 0 End) AS WDCNote
                                            
					
                        FROM
                        (					
                            Select (CASE WHEN DATE(L.V_Date) < '2019-04-01' THEN strftime('%m-%Y',L.V_Date) ELSE CR.Code End) as Code, L.LinkedSubcode AS SubCode, L.Subcode as Customer, (CASE WHEN DATE(L.V_Date) < '2019-04-01' THEN strftime('%m-%Y',L.V_Date) ELSE L.DocID End)  AS SearchCode, L.DocID || L.V_SNo as DocID, L.V_SNo, Site.ShortName AS Site, (CASE WHEN DATE(L.V_Date) < '2019-04-01' THEN strftime('%m-%Y',L.V_Date) ELSE L.RecId End) as RecID , L.V_Date, L.V_Type, Vt.Ncat, 
                                                (Case When Vt.Ncat = '" & Ncat.Receipt & "' Then L.Chq_No Else S.Brand End) Brand, IfNull(S.LRNo,'') || (Case When L.Chq_No Is Not Null Then 'Chq:' || L.Chq_No Else '' End) as LrNo, S.TaxableAmount, S.TaxAmount, S.InvoiceAmount, L.AmtDr, L.AmtCr,
					                            (Case When S.DocID Is Not Null And S.LEDocID Is Null Then 'Pending' Else Null End) as WStatus, NULL AS WDocID, NULL AS WRecID, NULL AS WV_Date, NULL AS WV_Type, 0 WGrossAmount, 0 WAdditionAmount, 0 WInvoiceAmount, 0 WAmtDr, 0 WAmtCr                    
                                                From Ledger L
                                                Left Join (
                    			                            Select PI.DociD, Max(PI.V_Type) AS V_Type, Max(CASE When I.ItemType = '" & ItemTypeCode.ServiceProduct & "' Then Null WHEN I.V_Type ='Item' THEN IG.Description ELSE I.Description End) AS Brand, Max(PI.Taxable_Amount) AS TaxableAmount,
                    			                            Max(PI.Tax1) + Max(PI.Tax2) + Max(PI.Tax3) + Max(PI.Tax4) + Max(PI.Tax5) AS TaxAmount,
                    			                            Max(PI.Net_Amount) AS InvoiceAmount, PIT.LrNo || (Case When PIT.NoOfBales Is Not Null Then ' @ ' ||  Cast(PIT.NoOfBales as nVarchar) Else '' End) as LrNo, LE.DocID as LeDocID
                                                            From PurchInvoice PI
                                                            Left Join PurchInvoiceDetail PIL On PI.DocID = PIL.DocId
                                                            LEFT JOIN PurchInvoiceTransport PIT ON PI.DocID = PIT.DocID                                 
                                                            Left Join Item I On PIL.Item = I.Code
                                                            Left JOIN Item IG ON I.ItemGroup = IG.Code 
                                                            LEFT JOIN Voucher_Type Vt ON PI.V_Type = VT.V_Type 
                                                            Left Join SaleInvoiceGeneratedEntries LE On PI.DocID = LE.DocID
                                                            WHERE Vt.NCat = 'PI' And PI.V_Type='PI' --And I.ItemType='TP'
                                                            GROUP BY PI.DocID                                
                                                          ) AS S ON L.DocID = S.DocID 
                                                LEFT JOIN SaleInvoiceGeneratedEntries CR ON L.DocId = CR.DocId
                                                LEFT JOIN SiteMast Site ON L.Site_Code = Site.Code                     
                                                LEFT JOIN voucher_type Vt ON L.V_Type = Vt.V_Type 
                            WHERE Substr(L.V_Type,1,1)<> 'W' And 1=1 " & mCondStr & "

                            UNION All

                            SELECT (CASE WHEN DATE(L.V_Date) < '2019-04-01' THEN strftime('%m-%Y',L.V_Date) ELSE Cr.Code End) as Code, L.LinkedSubCode AS SubCode, L.Subcode as Customer, (CASE WHEN DATE(L.V_Date) < '2019-04-01' THEN strftime('%m-%Y',L.V_Date) ELSE L.DocID End)  AS SearchCode, Null  DocId, L.V_SNo, Site.ShortName AS Site, NULL RecId, NULL V_Date, NULL V_Type, Vt.nCat, 
                                                S.Brand, S.LRNo, 0 TaxableAmount, 0 TaxAmount, 0 InvoiceAmount, 0 AmtDr, 0 AmtCr,
                                                Null as WStatus, (CASE WHEN DATE(L.V_Date) < '2019-04-01' THEN strftime('%m-%Y',L.V_Date) ELSE L.DocId End) AS WDocID, (CASE WHEN DATE(L.V_Date) < '2019-04-01' THEN strftime('%m-%Y',L.V_Date) ELSE L.RecId End) AS WRecID, L.V_Date AS WV_Date, L.V_Type AS WV_Type, IfNull(S.GrossAmount,0) AS WGrossAmount, IfNull(S.AdditionalAmount,0) AS WAdditionAmount, IfNull(S.InvoiceAmount,0) AS WInvoiceAmount, L.AmtDr AS WAmtDr, L.AmtCr WAmtCr
                                                From Ledger L
                                                Left Join (
                    			                            Select PI.DociD, Max(PI.V_Type) AS V_Type, Max(CASE WHEN I.V_Type ='Item' THEN IG.Description ELSE I.Description End) AS Brand, Sum(CASE WHEN I.ItemType = 'TP' THEN PIL.Amount ELSE 0 End) AS GrossAmount,
                    			                            Sum(CASE WHEN I.ItemType <> 'TP' THEN PIL.Amount ELSE 0 End) AS AdditionalAmount,
                    			                            Max(PI.Net_Amount) AS InvoiceAmount, PIT.LrNo || (Case When PIT.NoOfBales Is Not Null Then ' # ' ||  PIT.NoOfBales Else '' End) as LrNo
                                                            From PurchInvoice PI
                                                            Left Join PurchInvoiceDetail PIL On PI.DocID = PIL.DocId
                                                            LEFT JOIN PurchInvoiceTransport PIT ON PI.DocID = PIT.DocID                                 
                                                            Left Join Item I On PIL.Item = I.Code
                                                            Left JOIN Item IG ON I.ItemGroup = IG.Code 
                                                            LEFT JOIN Voucher_Type Vt ON PI.V_Type = VT.V_Type 
                                                            WHERE Vt.NCat = 'PI' And PI.V_Type='WPI'  --And I.ItemType='TP'
                                                            GROUP BY PI.DocID                                
                                                          ) AS S ON L.DocID = S.DocID 
                                                LEFT JOIN SaleInvoiceGeneratedEntries CR ON L.DocId = CR.DocId                               
                                                LEFT JOIN SiteMast Site ON L.Site_Code = Site.Code
                                                LEFT JOIN voucher_type Vt ON L.V_Type = Vt.V_Type                      
                            WHERE Substr(L.V_Type,1,1) = 'W' And 1=1 " & mCondStr & " 
                            ) AS VTemp
                            GROUP BY VTemp.Subcode, VTemp.Customer, IfNull(VTemp.Code, IfNull(VTemp.DocID,VTemp.WDocID))
                    ) AS VMain
                    Left Join viewHelpSubgroup Party On VMain.Subcode = Party.Code
                    Left Join viewHelpSubgroup Cust On VMain.Customer = Cust.Code
                    GROUP BY VMain.Subcode, VMain.Customer, CASE WHEN VMain.DocDateActualFormat<" & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & " THEN Null ELSE VMain.SearchCode END, (CASE WHEN VMain.DocDateActualFormat<'2019-01-01' THEN Null ELSE VMain.LrNo END)
                    ORDER BY VMain.Subcode,
		            VMain.DocDateActualFormat, VMain.DocType, VMain.DocNo, VMain.LrNo
                   "

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

            mCondStr1 = " Where Date(LG.V_Date) <= '" & ReportFrm.FGetText(0) & "' "
            mCondStr2 = " Where Date(LG.V_Date) <= '" & ReportFrm.FGetText(0) & "' "

            mCondStr1 = mCondStr1 & ReportFrm.GetWhereCondition("LG.SubCode", 1)
            mCondStr2 = mCondStr2 & ReportFrm.GetWhereCondition("LG.SubCode", 1)

            mCondStr1 = mCondStr1 & " And  LG.Site_Code IN (" & AgL.PubSiteCode & ") "
            mCondStr2 = mCondStr2 & " And  LG.Site_Code IN (" & AgL.PubSiteCode & ") "

            mCondStr1 = mCondStr1 & " And  Sg.Nature In ('" & ClsMain.SubGroupNature.Customer & "','" & ClsMain.SubGroupNature.Supplier & "') "
            mCondStr2 = mCondStr2 & " And  Sg.Nature In ('" & ClsMain.SubGroupNature.Customer & "','" & ClsMain.SubGroupNature.Supplier & "') "

            mCondStr1 = mCondStr1 & " And  IfNull(Lg.AmtDr,0) > 0 "

            mQry = "Select DocId, V_SNo, Max(RecId) As RecId, Max(V_Type) As V_Type, " &
                            " Max(V_Date) As V_Date, Max(Narration) As Narration, " &
                            " Max(Sg.Name) As PartyName, Max(C.CityName) As CityName, " &
                            " Max(BillAmt) As BillAmt, Abs(Sum(Adjusted)) As 
                            , " &
                            " Max(BillAmt) - Abs(Sum(Adjusted)) As Balance," &
                            " Max(DateAdd(Day,IfNull(Sg.CreditDays,0),Tmp.V_Date)) As DueDate, " &
                            " Max(Datediff(Day, Dateadd(Day,Sg.CreditDays,Tmp.V_Date),getdate())) AS OverDue  " &
                            " From ( " &
                            "       Select  LG.DocId,LG.V_SNo, LG.RecId As RecId, " &
                            "       LG.V_Type, LG.V_Date, LG.Narration, Lg.SubCode, " &
                            "       IfNull(Lg.AmtDr,0) As BillAmt,0 As Adjusted " &
                            "       From Ledger LG " &
                            "       LEFT JOIN SubGroup Sg On Lg.SubCode = Sg.SubCode " & mCondStr1 &
                            "       And IfNull(Lg.AmtDr,0) > 0 " &
                            " Union All " &
                            "       Select	LA.Adj_DocId As DocId,LA.Adj_V_SNo As V_SNo,Null As RecId,Null As V_Type,Null As V_Date, " &
                            "       Null As Narration, Lg.SubCode,0 As BillAmt,LA.Amount As Adjusted	 " &
                            "       From LedgerAdj LA " &
                            "       Left Join Ledger LG On LA.Adj_DocId = LG.DocId And LA.Adj_V_SNo = LG.V_SNo " &
                            "       Left Join SubGroup Sg On Lg.SubCode = Sg.SubCode " & mCondStr2 &
                            " ) As Tmp " &
                            " LEFT JOIN SubGroup Sg On Tmp.SubCode = Sg.SubCode " &
                            " LEFT JOIN City C On SG.CityCode = C.CityCode " &
                            " Group By DocId, V_SNo " &
                            " Having (IfNull(Max(BillAmt),0)-IfNull(Sum(Adjusted),0))>0" &
                            " Order By Max(V_Date),Max(RecId) "


            'mQry = "Select LG.DocId,LG.V_SNo,Convert(Varchar,Max(LG.V_No)) as VNo,Max(LG.V_Type) as VType,Max(LG.V_Date) as VDate,Max(SG.Name) As PName,"
            'mQry = mQry + "Max(LG.SubCode) as SubCode,Max(LG.Narration) as Narration,Max(LG.AmtDr) as Amt1,0 As Amt2,IfNull(Sum(LA.Amount),0) as Amt, "
            'mQry = mQry + "Max(SG.Add1)As Add1,Max(SG.Add2)As Add2,Max(C.CityName)As CityName,Max(CT.Name) as Country,MAx(St.name) As SiteName,max(Ag.GroupName) as AcGroupName, Max(Lg.RecId) As RecId, "
            'mQry = mQry + "Max(DateAdd(Day,IfNull(Sg.CreditDays,0),Lg.V_Date)) As DueDate, "
            'mQry = mQry + "Max(Datediff(Day, Dateadd(Day,Sg.CreditDays,Lg.V_Date),getdate())) AS OverDue "
            'mQry = mQry + "From Ledger LG Left Join SubGroup SG On LG.Subcode=SG.SubCode Left Join "
            'mQry = mQry + "City C on SG.CityCode=C.CityCode Left Join Country CT on SG.CountryCode=CT.Code LEFT JOIN AcGroup AG ON SG.GroupCode =AG.GroupCode  "
            'mQry = mQry + "Left Join LedgerAdj LA On LG.DocId=LA.Adj_DocID  And LG.V_SNo=LA.Adj_V_SNo "
            'mQry = mQry + "LEFT JOIN SiteMast ST ON LG.Site_Code =St.code  "
            'mQry = mQry + "LEFT JOIN ZoneMast ZM ON ZM.Code =SG.Zone "
            'mQry = mQry + mCondStr1
            'mQry = mQry + "Group By LG.DocId,LG.V_SNo "
            'mQry = mQry + "HAVING(IfNull(Sum(LA.Amount), 0) <> Max(LG.AmtDr))"
            'mQry = mQry + "Union All "
            'mQry = mQry + "Select	LG.DocId,LG.V_SNo,Convert(Varchar,LG.V_No) As V_No,LG.V_Type,LG.V_Date,SG.Name As PName,LG.SubCode, "
            'mQry = mQry + "LG.Narration,0 As Amt1,IfNull(LG.AmtCr,0)-IfNull(T.AMOUNT,0) as Amt2,0 As Amount,Null As Add1,Null As Add2,"
            'mQry = mQry + "Null As CityName,Null As Country,ST.name As sitename,IfNull(Ag.GroupName,'') as AcGroupName, Lg.RecId As RecId,  "
            'mQry = mQry + "DateAdd(Day,IfNull(Sg.CreditDays,0),Lg.V_Date) As DueDate, "
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

#Region "Party Label Print"
    Private Sub ProcPartyLabelPrint()
        Dim mCondStr$ = ""

        Try
            RepName = "PartyLabelPrint" : RepTitle = "Party Label Print"

            mCondStr = " Where H.Nature in ('Customer', 'Supplier') And H.Parent Is Null "

            If AgL.XNull(ReportFrm.FGetText(1)) <> "" And AgL.XNull(ReportFrm.FGetText(2)) = "" Then
                If ClsMain.FDivisionNameForCustomization(22) = "W SHYAMA SHYAM FABRICS" Or ClsMain.FDivisionNameForCustomization(20) = "SHYAMA SHYAM FABRICS" Or ClsMain.FDivisionNameForCustomization(25) = "SHYAMA SHYAM VENTURES LLP" Or ClsMain.FDivisionNameForCustomization(27) = "W SHYAMA SHYAM VENTURES LLP" Then
                    mCondStr = mCondStr & " And H.Subcode In (Select BillToParty From SaleInvoice Where V_Type='WSI' And Date(EntryDate) = " & AgL.Chk_Date(ReportFrm.FGetText(1)) & ") "
                Else
                    mCondStr = mCondStr & " And H.Subcode In (Select BillToParty From SaleInvoice Where V_Type='SI' And Date(EntryDate) = " & AgL.Chk_Date(ReportFrm.FGetText(1)) & ") "
                End If

            End If

            If AgL.XNull(ReportFrm.FGetText(1)) = "" And AgL.XNull(ReportFrm.FGetText(2)) <> "" Then
                If ClsMain.FDivisionNameForCustomization(22) = "W SHYAMA SHYAM FABRICS" Or ClsMain.FDivisionNameForCustomization(20) = "SHYAMA SHYAM FABRICS" Then
                    mCondStr = mCondStr & " And H.Subcode In (Select BillToParty From SaleInvoice Where V_Type='WSI' And Date(EntryDate) = " & AgL.Chk_Date(ReportFrm.FGetText(2)) & ") "
                Else
                    mCondStr = mCondStr & " And H.Subcode In (Select BillToParty From SaleInvoice Where V_Type='SI' And Date(EntryDate) = " & AgL.Chk_Date(ReportFrm.FGetText(2)) & ") "
                End If
            End If

            If AgL.XNull(ReportFrm.FGetText(1)) <> "" And AgL.XNull(ReportFrm.FGetText(2)) <> "" Then
                If ClsMain.FDivisionNameForCustomization(22) = "W SHYAMA SHYAM FABRICS" Or ClsMain.FDivisionNameForCustomization(20) = "SHYAMA SHYAM FABRICS" Or ClsMain.FDivisionNameForCustomization(25) = "SHYAMA SHYAM VENTURES LLP" Or ClsMain.FDivisionNameForCustomization(27) = "W SHYAMA SHYAM VENTURES LLP" Then
                    mCondStr = mCondStr & " And H.Subcode In (Select BillToParty From SaleInvoice Where V_Type='WSI' And Date(EntryDate) Between " & AgL.Chk_Date(ReportFrm.FGetText(1)) & " And " & AgL.Chk_Date(ReportFrm.FGetText(2)) & ") "
                Else
                    mCondStr = mCondStr & " And H.Subcode In (Select BillToParty From SaleInvoice Where V_Type='SI' And Date(EntryDate) Between " & AgL.Chk_Date(ReportFrm.FGetText(1)) & " And " & AgL.Chk_Date(ReportFrm.FGetText(2)) & ") "
                End If
            End If


            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.SubCode", 0)


            mQry = "select H.DispName, H.Address, C.CityName, IfNull(H.Pin,'') as Pin, IfNull(H.Phone,'') as Phone, IfNull(H.Mobile,'') as Mobile, substr(H.Address,0, charindex(CHAR(13), H.Address)) AS Address1, substr(H.address,charindex(CHAR(13), H.Address), length(H.address)) AS Address2, H.ManualCode 
                    from subgroup H
                    Left Join City C On H.cityCode = C.CityCode " & mCondStr & " Order By H.DispName "

            DsRep = AgL.FillData(mQry, AgL.GCn)

            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records to Print!")

            ReportFrm.PrintReport(DsRep, RepName, RepTitle)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
#End Region

#Region "Brand List"
    Private Sub ProcBrandList()
        Dim mCondStr$ = ""

        Try
            RepName = "BrandList" : RepTitle = "BrandList"

            mCondStr = mCondStr & " And Ig.DefaultSupplier Is Not Null "
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("C.CityCode", 0)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("Sg.Area", 1)

            mQry = "SELECT IC.Description AS Category, IG.Description AS Brand, c.CityName 
                    FROM Item IG
                    LEFT JOIN ItemCategory IC ON IG.ItemCategory = IC.Code 
                    LEFT JOIN subgroup sg ON Sg.Subcode = ig.DefaultSupplier 
                    LEFT JOIN city c ON sg.CityCode = c.CityCode 
                    WHERE V_Type ='IG' " & mCondStr & " Order By IG.Description "

            DsRep = AgL.FillData(mQry, AgL.GCn)

            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records to Print!")

            ReportFrm.PrintReport(DsRep, RepName, RepTitle)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
#End Region

#Region "Weaving Order Ratio"
    Private Sub ProcWeavingOrderRatio()
        Try
            RepName = "Trade_WeavingOrderRatio" : RepTitle = "Weaving Order Ratio"
            Dim bTempTable$ = ""
            Dim bTempItem$ = ""

            bTempItem = AgL.GetGUID(AgL.GCn).ToString
            bTempTable = AgL.GetGUID(AgL.GCn).ToString
            mQry = "CREATE TABLE [#" & bTempTable & "] " &
                    " (Party NVARCHAR(10), ClothQty Float, ClothWeight Float, " &
                    " WeavingOrderQty Float, WeavingOrderMeasure Float )  "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

            Dim mSaleCondStr$ = ""
            Dim mWeavingCondStr$ = ""


            mSaleCondStr = mSaleCondStr & " AND Date(H.V_Date) Between " & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & " And " & AgL.Chk_Date(CDate(ReportFrm.FGetText(1)).ToString("s")) & " "
            mSaleCondStr = mSaleCondStr & ReportFrm.GetWhereCondition("H.SaleToParty", 2)

            Dim mSaleQry$ = "SELECT H.SaleToParty AS Party, sum(L.Qty) AS Qty, sum(L.TotalMeasure) AS AS ClothWeaight " &
                            " FROM SaleInvoice H " &
                            " LEFT JOIN SaleInvoiceDetail L ON L.DocId = H.DocID  " &
                            " WHERE 1=1 " & mSaleCondStr &
                            " AND H.Site_Code = " & AgL.Chk_Text(AgL.PubSiteCode) & " AND H.Div_Code = " & AgL.Chk_Text(AgL.PubDivCode) & " " &
                            " GROUP BY H.SaleToParty "

            mWeavingCondStr = " AND Date(H.V_Date) Between " & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & " And " & AgL.Chk_Date(CDate(ReportFrm.FGetText(1)).ToString("s")) & " "
            mWeavingCondStr = mWeavingCondStr & ReportFrm.GetWhereCondition("H.JobWorker", 2)

            'For Inserting Carpet Consumption
            mQry = "INSERT INTO [#" & bTempTable & "](Party, ClothQty, ClothWeight ) " &
                      mSaleQry
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

            Dim mWeavingOrderQry$ = "SELECT H.JobWorker, sum(L.Qty) AS Qty, sum(L.TotalMeasure) AS TotalMeasure, max(L.MeasureUnit) AS OrdMeasure " &
                    " FROM JobOrderDetail L  " &
                    " LEFT JOIN JobOrder H  ON H.DocID = L.JobOrder  " &
                    " LEFT JOIN Voucher_Type Vt  ON Vt.V_Type = H.V_Type  " &
                    " WHERE Vt.NCat IN ('WVORD', 'WVCNL') " &
                    " AND H.Site_Code = " & AgL.Chk_Text(AgL.PubSiteCode) & " AND H.Div_Code = " & AgL.Chk_Text(AgL.PubDivCode) & " " &
                    " GROUP BY H.JobWorker "




            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records to Print!")

            ReportFrm.PrintReport(DsRep, RepName, RepTitle)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
#End Region

#Region "GST Reports"
    Private Sub ProcGSTReports()
        If CDate(ReportFrm.FGetText(1)).Day <> 1 Then
            MsgBox("From Date should be start date of month...!", MsgBoxStyle.Information)
            Exit Sub
        End If

        If AgL.RetMonthEndDate(CDate(ReportFrm.FGetText(2))) <> CDate(ReportFrm.FGetText(2)) Then
            MsgBox("To Date should be end date of month...!", MsgBoxStyle.Information)
            Exit Sub
        End If

        If ReportFrm.FGetText(0) = "GST 3B" Then
            ProcGSTR3BReports()
        ElseIf ReportFrm.FGetText(0) = "GSTR1" Then
            ProcGSTR1Reports()
        Else
            MsgBox("Please select Report Type...!")
        End If
    End Sub
    Private Sub ProcGSTR3BReports()
        Dim DtTable As DataTable = Nothing
        Dim SubTitle$ = ""
        Dim GroupHeaderTitle1$ = "", GroupHeaderTitle2$ = ""
        Dim IsReturn As Integer = 0
        Dim AssessmentYear$ = ""
        'Dim OutputFile As String = My.Application.Info.DirectoryPath + "\TaxReturns\GSTR3B.xlsm"
        Dim OutputFile As String = ""
        Dim mCondStr$ = ""

        Dim ToDate As DateTime = ReportFrm.FGetText(2)
        Dim newdate = String.Format("{0:yyyy-MM-dd}", ToDate)
        Dim MonthName As String = AgL.XNull(AgL.Dman_Execute(" select case strftime('%m', '" & newdate & "') when '01' then 'January' when '02' then 'Febuary' when '03' then 'March' 
                    when '04' then 'April' when '05' then 'May' when '06' then 'June' when '07' then 'July' 
                    when '08' then 'August' when '09' then 'September' when '10' then 'October' when '11' then 'November' 
                    when '12' then 'December' else '' end as month ", AgL.GCn).ExecuteScalar)


        Dim FilePath As String = ""
        Dim SaveFileDialogBox As SaveFileDialog
        Dim sFilePath As String = ""
        SaveFileDialogBox = New SaveFileDialog

        SaveFileDialogBox.Title = "File Name"
        FilePath = My.Computer.FileSystem.SpecialDirectories.Desktop
        SaveFileDialogBox.InitialDirectory = FilePath
        SaveFileDialogBox.FilterIndex = 1
        SaveFileDialogBox.FileName = "GSTR3B_" + MonthName + ".xlsm"
        If SaveFileDialogBox.ShowDialog = Windows.Forms.DialogResult.Cancel Then Exit Sub
        OutputFile = SaveFileDialogBox.FileName




        Dim xlApp As Excel.Application
        Dim TemplateWorkBook As Excel.Workbook
        Dim OutputWorkBook As Excel.Workbook

        xlApp = New Excel.Application
        xlApp.AlertBeforeOverwriting = False
        xlApp.DisplayAlerts = False

        TemplateWorkBook = xlApp.Workbooks.Open(My.Application.Info.DirectoryPath + "\Templates\" + "GSTR3B_Excel_Utility_V3.0.xlsm")
        TemplateWorkBook.SaveAs(OutputFile)
        xlApp.Workbooks.Close()
        OutputWorkBook = xlApp.Workbooks.Open(OutputFile)

        Try
            Dim xlWorkSheet As Excel.Worksheet
            xlWorkSheet = OutputWorkBook.Worksheets("GSTR-3B")

            mCondStr = " Where 1=1"
            mCondStr = mCondStr & " AND H.Div_Code = '" & AgL.PubDivCode & "' "
            mCondStr = mCondStr & " AND Date(H.V_Date) Between " & AgL.Chk_Date(CDate(ReportFrm.FGetText(1)).ToString("s")) & " And " & AgL.Chk_Date(CDate(ReportFrm.FGetText(2)).ToString("s")) & " "


            'For GSTIN, Legal Name of the registered person
            mQry = " Select VReg.SalesTaxNo As RegistrationNo, Sg.DispName As Name
                From Division D
                LEFT JOIN SubGroup Sg On D.SubCode = Sg.SubCode
                LEFT JOIN City C On Sg.CityCode = C.CityCode
                LEFT JOIN State S On C.State = S.Code
                LEFT JOIN (Select Subcode, RegistrationNo As SalesTaxNo
                            From SubgroupRegistration 
                            Where RegistrationType = 'Sales Tax No') As VReg On D.SubCode = VReg.SubCode
                Where D.Div_Code = '" & AgL.PubDivCode & "'"
            DtTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtTable.Rows.Count > 0 Then
                xlWorkSheet.Cells.Item(5, 3).Value = DtTable.Rows(0)("RegistrationNo")
                xlWorkSheet.Cells.Item(6, 3).Value = DtTable.Rows(0)("Name")
            End If

            'For Year
            xlWorkSheet.Cells.Item(5, 7).Value = AgL.XNull(AgL.Dman_Execute(" Select cyear From Company Where Comp_Code = '" & AgL.PubCompCode & "' ", AgL.GCn).ExecuteScalar)

            'Month	

            xlWorkSheet.Cells.Item(6, 7).Value = MonthName


            '3.1 (a) Outward Taxable  supplies  (other than zero rated, nil rated and exempted)
            'Sales Amount And Tax On It (Both Local And Central Combined)
            'mQry = " SELECT Sum(L.Taxable_Amount) as TotalTaxablevalue, Sum(L.Tax1) As IntegratedTax, Sum(L.Tax2) as CentralTax, Sum(L.Tax3) as StateTax, 0 As Cess
            '        from SaleInvoice H 
            '        left join SaleInvoiceDetail L On H.DocID = L.DocID " & mCondStr &
            '        " And ifnull(L.Tax1,0) + ifnull(L.Tax2,0) + ifnull(L.Tax3,0) <> 0 "

            mQry = " Select Sum(VMain.TotalTaxablevalue) as TotalTaxablevalue, Sum(VMain.IntegratedTax) As IntegratedTax, 
                    Sum(VMain.CentralTax) as CentralTax, Sum(VMain.StateTax) as StateTax, Sum(VMain.Cess)  As Cess
                    From
                    (
                        SELECT L.Taxable_Amount as TotalTaxablevalue, L.Tax1 As IntegratedTax, L.Tax2 as CentralTax, L.Tax3 as StateTax, 0 As Cess
                        from SaleInvoice H 
                        left join SaleInvoiceDetail L On H.DocID = L.DocID " & mCondStr &
                        " And ifnull(L.Tax1,0) + ifnull(L.Tax2,0) + ifnull(L.Tax3,0) <> 0
                        UNION ALL 
                        Select -L.Taxable_Amount As TotalTaxablevalue, -L.Tax1 As IntegratedTax, -L.Tax2 As CentralTax, -L.Tax3 As StateTax, 0 As Cess
                        From LedgerHead H 
                        LEFT JOIN LedgerHeadDetailCharges L On H.DocId = L.DocId 
                        LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type " & mCondStr &
                        " And Vt.NCat In ('" & agConstants.Ncat.CreditNoteCustomer & "', '" & agConstants.Ncat.CreditNoteSupplier & "') 
                    ) As VMain "
            DtTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtTable.Rows.Count > 0 Then
                xlWorkSheet.Cells.Item(11, 3).Value = DtTable.Rows(0)("TotalTaxablevalue")
                xlWorkSheet.Cells.Item(11, 4).Value = DtTable.Rows(0)("IntegratedTax")
                xlWorkSheet.Cells.Item(11, 5).Value = DtTable.Rows(0)("CentralTax")
                xlWorkSheet.Cells.Item(11, 7).Value = DtTable.Rows(0)("Cess")
            End If

            '3.1 (b) Outward Taxable  supplies  (zero rated )
            'Export Sales (Both on Bond Without Bond)
            mQry = " SELECT 0 as TotalTaxablevalue, 0 As IntegratedTax, 0 As Cess "
            DtTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtTable.Rows.Count > 0 Then
                xlWorkSheet.Cells.Item(12, 3).Value = DtTable.Rows(0)("TotalTaxablevalue")
                xlWorkSheet.Cells.Item(12, 4).Value = DtTable.Rows(0)("IntegratedTax")
                xlWorkSheet.Cells.Item(12, 7).Value = DtTable.Rows(0)("Cess")
            End If

            '3.1 (c) Other Outward Taxable  supplies (Nil rated, exempted)
            'Goods Covered in Excemtion Notification & Goods Having rate 0%
            mQry = " SELECT ifnull(Sum(L.Taxable_Amount),0) as TotalTaxablevalue
                    from SaleInvoice H 
                    left join SaleInvoiceDetail L On H.DocID = L.DocID " & mCondStr &
                    " And ifnull(L.Tax1,0) + ifnull(L.Tax2,0) + ifnull(L.Tax3,0) = 0 "
            DtTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtTable.Rows.Count > 0 Then
                xlWorkSheet.Cells.Item(13, 3).Value = DtTable.Rows(0)("TotalTaxablevalue")
            End If

            '3.1 (d) Inward supplies (liable to reverse charge) 
            'Tax to be Paid on reverse charge.
            'mQry = " SELECT Sum(L.Taxable_Amount) as TotalTaxablevalue, Sum(L.Tax1) As IntegratedTax, Sum(L.Tax2) as CentralTax, Sum(L.Tax3) as StateTax, 0 As Cess
            '        from PurchInvoice H 
            '        left join PurchInvoiceDetail L On H.DocID = L.DocID  " & mCondStr &
            '        " And H.SalesTaxGroupParty = '" & PostingGroupSalesTaxParty.Unregistered & "' "


            mQry = " SELECT 
                    Round(Sum(Case When Ps.ChargeType = 'TAXABLE AMOUNT' Then L.Taxable_Amount Else 0 End),2) as TotalTaxablevalue, 
                    Round(Sum(Case When Ps.ChargeType = 'TAX1' Then L.Taxable_Amount * Ps.Percentage / 100  Else 0 End),2) As IntegratedTax,
                    Round(Sum(Case When Ps.ChargeType = 'TAX2' Then L.Taxable_Amount * Ps.Percentage / 100  Else 0 End),2) As CentralTax,
                    Round(Sum(Case When Ps.ChargeType = 'TAX3' Then L.Taxable_Amount * Ps.Percentage / 100  Else 0 End),2) As StateTax,
                    0 As Cess
                    from PurchInvoice H 
                    left join PurchInvoiceDetail L On H.DocID = L.DocID   
                    LEFT JOIN Item I ON L.Item = I.Code
                    LEFT JOIN ItemCategory Ic On I.ItemCategory = Ic.Code
                    LEFT JOIN PostingGroupSalesTax Ps On 'Registered' = Ps.PostingGroupSalesTaxParty
                            And IfNull(I.SalesTaxPostingGroup,Ic.SalesTaxGroup)  =  Ps.PostingGroupSalesTaxItem
                            And H.PlaceOfSupply = Ps.PlaceOfSupply
                            And Ps.Process = 'PURCH' " & mCondStr &
                    " And H.SalesTaxGroupParty = '" & PostingGroupSalesTaxParty.Unregistered & "' "
            DtTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtTable.Rows.Count > 0 Then
                xlWorkSheet.Cells.Item(14, 3).Value = DtTable.Rows(0)("TotalTaxablevalue")
                xlWorkSheet.Cells.Item(14, 4).Value = DtTable.Rows(0)("IntegratedTax")
                xlWorkSheet.Cells.Item(14, 5).Value = DtTable.Rows(0)("CentralTax")
                xlWorkSheet.Cells.Item(14, 7).Value = DtTable.Rows(0)("Cess")
            End If

            '3.1 (e) Non-GST Outward supplies
            'Goods not covered in GST, Like Diesel
            mQry = " SELECT 0 as TotalTaxablevalue "
            DtTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtTable.Rows.Count > 0 Then
                xlWorkSheet.Cells.Item(15, 3).Value = DtTable.Rows(0)("TotalTaxablevalue")
            End If


            '3.2  Of the supplies shown in 3.1 (a), details of inter-state supplies made to unregistered persons, composition taxable person and UIN holders						
            'Suppliers Made to UnRegistered Person : Only InterState Sales to Unregistered
            'Suppliers Made to Composition Taxable Person : Only InterState Sales to Composition Dealer
            'Suppliers Made to UiN Holders : Only InterState Sales to UIN Holders like Embassy
            mQry = " SELECT S.Description As PlaceOfSupply,
                    Sum(CASE when H.SalesTaxGroupParty =  '" & PostingGroupSalesTaxParty.Unregistered & "' THEN L.Taxable_Amount Else 0 END) As TotalTaxablevalue_Unregistered,
                    0 As AmountOfIntegratedTax_Unregistered,
                    Sum(CASE when H.SalesTaxGroupParty = 'Composition' THEN L.Taxable_Amount Else 0 END) As TotalTaxablevalue_Composition,
                    0 As AmountOfIntegratedTax_Composition,
                    0 As TotalTaxablevalue_UINholders,
                    0 As AmountOfIntegratedTax_UINholders
                    From SaleInvoice H 
                    left join SaleInvoiceDetail L on H.DocID = L.DocID
                    Left join City C On H.SaleToPartyCity = C.CityCode
                    left join State S on C.State = S.Code " & mCondStr &
                    " And H.PlaceOfSupply = '" & PlaceOfSupplay.OutsideState & "' 
                    Group By S.Description 
                    Having Sum(CASE when H.SalesTaxGroupParty =  'Unregistered' THEN L.Taxable_Amount Else 0 END) > 0
                    And Sum(CASE when H.SalesTaxGroupParty = 'Composition' THEN L.Taxable_Amount Else 0 END) > 0 "
            DtTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtTable.Rows.Count > 0 Then
                xlWorkSheet.Cells.Item(79, 2).Value = DtTable.Rows(0)("PlaceOfSupply")

                xlWorkSheet.Cells.Item(79, 3).Value = DtTable.Rows(0)("TotalTaxablevalue_Unregistered")
                xlWorkSheet.Cells.Item(79, 4).Value = DtTable.Rows(0)("AmountOfIntegratedTax_Unregistered")

                xlWorkSheet.Cells.Item(79, 5).Value = DtTable.Rows(0)("TotalTaxablevalue_Composition")
                xlWorkSheet.Cells.Item(79, 6).Value = DtTable.Rows(0)("AmountOfIntegratedTax_Composition")

                xlWorkSheet.Cells.Item(79, 7).Value = DtTable.Rows(0)("TotalTaxablevalue_UINholders")
                xlWorkSheet.Cells.Item(79, 8).Value = DtTable.Rows(0)("AmountOfIntegratedTax_UINholders")
            End If


            '4. Eligible ITC	(1)   Import of goods 
            'Tax Charged on Import of Goods liKe IGST
            mQry = " SELECT 0 as IntegratedTax, 0 As Cess "
            DtTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtTable.Rows.Count > 0 Then
                xlWorkSheet.Cells.Item(22, 3).Value = DtTable.Rows(0)("IntegratedTax")
                xlWorkSheet.Cells.Item(22, 6).Value = DtTable.Rows(0)("Cess")
            End If

            '4. Eligible ITC	(2)   Import of services
            'Tax paid on Import of service (Covered under Reverse Charge) 
            mQry = " SELECT 0 as IntegratedTax, 0 As Cess "
            DtTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtTable.Rows.Count > 0 Then
                xlWorkSheet.Cells.Item(23, 3).Value = DtTable.Rows(0)("IntegratedTax")
                xlWorkSheet.Cells.Item(23, 6).Value = DtTable.Rows(0)("Cess")
            End If

            '4. Eligible ITC	(3)   Inward supplies liable to reverse charge        (other than 1 &2 above)
            'All Other purchase from unregistered Dealer (Local Purchase)
            mQry = " SELECT 0 as IntegratedTax, 0 As CentralTax, 0 As Cess "
            DtTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtTable.Rows.Count > 0 Then
                xlWorkSheet.Cells.Item(24, 3).Value = DtTable.Rows(0)("IntegratedTax")
                xlWorkSheet.Cells.Item(24, 4).Value = DtTable.Rows(0)("CentralTax")
                xlWorkSheet.Cells.Item(24, 6).Value = DtTable.Rows(0)("Cess")
            End If

            '4. Eligible ITC	(4)   Inward supplies from ISD
            'Input from other Branches (Input Service Distributors)
            mQry = " SELECT 0 as IntegratedTax, 0 As CentralTax, 0 As Cess "
            DtTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtTable.Rows.Count > 0 Then
                xlWorkSheet.Cells.Item(25, 3).Value = DtTable.Rows(0)("IntegratedTax")
                xlWorkSheet.Cells.Item(25, 4).Value = DtTable.Rows(0)("CentralTax")
                xlWorkSheet.Cells.Item(25, 6).Value = DtTable.Rows(0)("Cess")
            End If

            '4. Eligible ITC	(5)   All other ITC
            'Normal Purchase from Registered Dealer
            mQry = " Select Sum(VMain.IntegratedTax) As IntegratedTax, 
                    Sum(VMain.CentralTax) as CentralTax, Sum(VMain.StateTax) as StateTax, Sum(VMain.Cess)  As Cess
                    From
                    (
                        SELECT L.Tax1 As IntegratedTax, L.Tax2 as CentralTax, L.Tax3 as StateTax, 0 As Cess
                        from PurchInvoice H 
                        left join PurchInvoiceDetail L on H.DocID = L.DocID " & mCondStr &
                        " And H.SalesTaxGroupParty = '" & PostingGroupSalesTaxParty.Registered & "' 
                        And ifnull(L.Tax1,0) + ifnull(L.Tax2,0) + ifnull(L.Tax3,0) <> 0 
                        UNION ALL 
                        Select L.Tax1 As IntegratedTax, L.Tax2 as CentralTax, L.Tax3 as StateTax, 0 As Cess
                        From LedgerHead H 
                        LEFT JOIN LedgerHeadDetailCharges L On H.DocId = L.DocId 
                        LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type " & mCondStr &
                        " And Vt.NCat In ('" & agConstants.Ncat.DebitNoteSupplier & "', '" & agConstants.Ncat.DebitNoteCustomer & "' )
                    ) As VMain "
            DtTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtTable.Rows.Count > 0 Then
                xlWorkSheet.Cells.Item(26, 3).Value = DtTable.Rows(0)("IntegratedTax")
                xlWorkSheet.Cells.Item(26, 4).Value = DtTable.Rows(0)("CentralTax")
                xlWorkSheet.Cells.Item(26, 6).Value = DtTable.Rows(0)("Cess")
            End If


            '4. (D)  Ineligible ITC	(1)   As per section 17(5) of CGST//SGST Act
            mQry = " SELECT 
                    Round(Sum(Case When Ps.ChargeType = 'TAX1' Then L.Taxable_Amount * Ps.Percentage / 100  Else 0 End),2) As IntegratedTax,
                    Round(Sum(Case When Ps.ChargeType = 'TAX2' Then L.Taxable_Amount * Ps.Percentage / 100  Else 0 End),2) As CentralTax,
                    Round(Sum(Case When Ps.ChargeType = 'TAX3' Then L.Taxable_Amount * Ps.Percentage / 100  Else 0 End),2) As StateTax,
                    0 As Cess
                    from PurchInvoice H 
                    left join PurchInvoiceDetail L On H.DocID = L.DocID   
                    LEFT JOIN Item I ON L.Item = I.Code
                    LEFT JOIN ItemCategory Ic On I.ItemCategory = Ic.Code
                    LEFT JOIN PostingGroupSalesTax Ps On 'Registered' = Ps.PostingGroupSalesTaxParty
                            And IfNull(I.SalesTaxPostingGroup,Ic.SalesTaxGroup)  =  Ps.PostingGroupSalesTaxItem
                            And H.PlaceOfSupply = Ps.PlaceOfSupply
                            And Ps.Process = 'PURCH' " & mCondStr &
                    " And H.SalesTaxGroupParty = '" & PostingGroupSalesTaxParty.Unregistered & "' "
            DtTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtTable.Rows.Count > 0 Then
                xlWorkSheet.Cells.Item(32, 3).Value = DtTable.Rows(0)("IntegratedTax")
                xlWorkSheet.Cells.Item(32, 4).Value = DtTable.Rows(0)("CentralTax")
                xlWorkSheet.Cells.Item(32, 5).Value = DtTable.Rows(0)("StateTax")
                xlWorkSheet.Cells.Item(32, 6).Value = DtTable.Rows(0)("Cess")
            End If


            '5. Values of exempt, From a supplier under composition scheme, Exempt  and Nil rated supply	
            'Purchase of Goods 0%, Exempted etc
            mQry = " Select Case When H.PlaceOfSupply = '" & PlaceOfSupplay.OutsideState & "' Then Sum(L.Taxable_Amount) Else 0 End As InterStatesupplies,
                    Case When H.PlaceOfSupply <> '" & PlaceOfSupplay.OutsideState & "' Then   Sum(L.Taxable_Amount) Else 0 End As Intrastatesupplies
                    from PurchInvoice H 
                    left join PurchInvoiceDetail L on H.DocID = L.DocID " & mCondStr &
                    " And L.SalesTaxGroupItem = 'GST 0%' Group By H.PlaceOfSupply"
            DtTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtTable.Rows.Count > 0 Then
                xlWorkSheet.Cells.Item(39, 4).Value = DtTable.Rows(0)("InterStatesupplies")
                xlWorkSheet.Cells.Item(39, 5).Value = DtTable.Rows(0)("Intrastatesupplies")
            End If

            '5. Values of exempt, Non GST supply	
            'Purchase of Goods not Covered on GST
            mQry = " SELECT 0 as InterStatesupplies, 0 As Intrastatesupplies "
            DtTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtTable.Rows.Count > 0 Then
                xlWorkSheet.Cells.Item(40, 4).Value = DtTable.Rows(0)("InterStatesupplies")
                xlWorkSheet.Cells.Item(40, 5).Value = DtTable.Rows(0)("Intrastatesupplies")
            End If


            '5.1 Interest & late fee payable	
            'Intrest @18% on late payment of tax
            mQry = " SELECT 0 as IntegratedTax, 0 As CentralTax, 0 As StateTax, 0 As Cess "
            DtTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtTable.Rows.Count > 0 Then
                xlWorkSheet.Cells.Item(56, 3).Value = DtTable.Rows(0)("IntegratedTax")
                xlWorkSheet.Cells.Item(56, 4).Value = DtTable.Rows(0)("CentralTax")
                xlWorkSheet.Cells.Item(56, 5).Value = DtTable.Rows(0)("StateTax")
                xlWorkSheet.Cells.Item(56, 6).Value = DtTable.Rows(0)("Cess")
            End If


            ClsMain.FReleaseObjects(xlWorkSheet)

            OutputWorkBook.Save()
            OutputWorkBook.Close()
            xlApp.Quit()

            ClsMain.FReleaseObjects(xlApp)
            ClsMain.FReleaseObjects(TemplateWorkBook)

            System.Diagnostics.Process.Start(OutputFile)

        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
            OutputWorkBook.Close()
            xlApp.Quit()
            ClsMain.FReleaseObjects(xlApp)
            ClsMain.FReleaseObjects(TemplateWorkBook)
        End Try
    End Sub
    Private Sub ProcGSTR1Reports()
        Dim SubTitle$ = ""
        Dim GroupHeaderTitle1$ = "", GroupHeaderTitle2$ = ""
        Dim IsReturn As Integer = 0
        Dim AssessmentYear$ = ""
        'Dim OutputFile As String = My.Application.Info.DirectoryPath + "\TaxReturns\GSTR1.xlsx"
        Dim OutputFile As String = ""
        Dim I As Integer
        Dim mCondStr$ = ""




        Dim ToDate As DateTime = ReportFrm.FGetText(2)
        Dim newdate = String.Format("{0:yyyy-MM-dd}", ToDate)
        Dim MonthName As String = AgL.XNull(AgL.Dman_Execute(" select case strftime('%m', '" & newdate & "') when '01' then 'January' when '02' then 'Febuary' when '03' then 'March' 
                    when '04' then 'April' when '05' then 'May' when '06' then 'June' when '07' then 'July' 
                    when '08' then 'August' when '09' then 'September' when '10' then 'October' when '11' then 'November' 
                    when '12' then 'December' else '' end as month ", AgL.GCn).ExecuteScalar)

        Dim FilePath As String = ""
        Dim SaveFileDialogBox As SaveFileDialog
        Dim sFilePath As String = ""
        SaveFileDialogBox = New SaveFileDialog

        SaveFileDialogBox.Title = "File Name"
        FilePath = My.Computer.FileSystem.SpecialDirectories.Desktop
        SaveFileDialogBox.InitialDirectory = FilePath
        SaveFileDialogBox.FilterIndex = 1
        SaveFileDialogBox.FileName = "GSTR1_" + MonthName + ".xlsx"
        If SaveFileDialogBox.ShowDialog = Windows.Forms.DialogResult.Cancel Then Exit Sub
        OutputFile = SaveFileDialogBox.FileName

        Dim xlApp As Excel.Application
        Dim TemplateWorkBook As Excel.Workbook
        Dim OutputWorkBook As Excel.Workbook

        xlApp = New Excel.Application
        xlApp.AlertBeforeOverwriting = False
        xlApp.DisplayAlerts = False



        TemplateWorkBook = xlApp.Workbooks.Open(My.Application.Info.DirectoryPath + "\Templates\" + "GSTR1_Excel_Workbook_Template_V1.5.xlsx")
        TemplateWorkBook.SaveAs(OutputFile)
        xlApp.Workbooks.Close()
        OutputWorkBook = xlApp.Workbooks.Open(OutputFile)


        Try
            mCondStr = " Where 1=1"
            mCondStr = mCondStr & " AND H.Div_Code = '" & AgL.PubDivCode & "' "
            mCondStr = mCondStr & " AND Date(H.V_Date) Between " & AgL.Chk_Date(CDate(ReportFrm.FGetText(1)).ToString("s")) & " And " & AgL.Chk_Date(CDate(ReportFrm.FGetText(2)).ToString("s")) & " "


            FWriteGSTR1B2B(OutputWorkBook, mCondStr)
            FWriteGSTR1B2CL(OutputWorkBook, mCondStr)
            FWriteGSTR1B2CS(OutputWorkBook, mCondStr)
            FWriteGSTR1CDNR(OutputWorkBook, mCondStr)
            FWriteGSTR1CDNUR(OutputWorkBook, mCondStr)
            FWriteGSTR1EXEMP(OutputWorkBook, mCondStr)
            FWriteGSTR1HSN(OutputWorkBook, mCondStr)
            FWriteGSTR1DOC(OutputWorkBook, mCondStr)

            OutputWorkBook.Save()
            OutputWorkBook.Close()
            xlApp.Quit()

            ClsMain.FReleaseObjects(xlApp)
            ClsMain.FReleaseObjects(TemplateWorkBook)
            ClsMain.FReleaseObjects(OutputWorkBook)

            If ErrorLog <> "" Then
                If File.Exists(My.Application.Info.DirectoryPath + " \ " + "ErrorLog.txt") Then
                    My.Computer.FileSystem.WriteAllText(My.Application.Info.DirectoryPath + "\" + "ErrorLog.txt", ErrorLog, False)
                Else
                    File.Create(My.Application.Info.DirectoryPath + " \ " + "ErrorLog.txt")
                    My.Computer.FileSystem.WriteAllText(My.Application.Info.DirectoryPath + " \ " + "ErrorLog.txt", ErrorLog, False)
                End If
                System.Diagnostics.Process.Start("notepad.exe", My.Application.Info.DirectoryPath + "\" + "ErrorLog.txt")
                Exit Sub
            End If

            System.Diagnostics.Process.Start(OutputFile)

        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
            OutputWorkBook.Close()
            xlApp.Quit()
            ClsMain.FReleaseObjects(xlApp)
            ClsMain.FReleaseObjects(TemplateWorkBook)
        End Try
    End Sub

    Private Sub FWriteGSTR1B2B(ByVal xlWorkBook As Excel.Workbook, mCondStr As String)
        Dim DtTable As DataTable = Nothing
        Dim xlWorkSheet As Excel.Worksheet
        Dim I As Integer = 0
        xlWorkSheet = xlWorkBook.Worksheets("b2b")

        mQry = " SELECT Max(Sgr.RegistrationNo) As GSTINofRecipient, Max(Sg.Name) As ReceiverName, Max(H.ManualRefNo) As InvoiceNumber,
                    Max(strftime('%d/%m/%Y', H.V_Date)) As InvoiceDate, Max(H.Net_Amount) As InvoiceValue, Max(S.ManualCode || '-' || S.Description) As PlaceOfSupply, 'N' As ReverseCharge,
                    '' As ApplicableTaxRate, 'Regular' As InvoiceType,	Null As ECommerceGSTIN,	 
                    Max(IfNull(L.Tax1_Per,0) + IfNull(L.Tax2_Per,0) + IfNull(L.Tax3_Per,0)) As Rate,	
                    Sum(L.Taxable_Amount) As TaxableValue, 0 As CessAmount
                    From SaleInvoice H 
                    left join SaleInvoiceDetail L On H.DocID = L.DocID
                    left join SubGroup Sg On H.SaleToParty = Sg.SubCode
                    LEFT JOIN SubGroupRegistration Sgr On Sg.SubCode = Sgr.SubCode And Sgr.RegistrationType = 'Sales Tax No'
                    LEFT JOIN City C On H.SaleToPartyCity = C.CityCode
                    LEFT JOIN State S on C.State = S.Code " & mCondStr &
                    " And H.SalesTaxGroupParty In ('" & PostingGroupSalesTaxParty.Registered & "','" & PostingGroupSalesTaxParty.Composition & "') 
                    Group BY L.DocID, SalesTaxGroupItem "
        DtTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

        For I = 0 To DtTable.Rows.Count - 1
            If AgL.XNull(DtTable.Rows(I)("GSTINofRecipient")) = "" Then
                ErrorLog += "GST No. is blank for " & AgL.XNull(DtTable.Rows(I)("ReceiverName")) & vbCrLf
            End If
        Next

        FillGSTR1ExcelFiles(DtTable, xlWorkSheet)
    End Sub

    Private Sub FWriteGSTR1B2CL(ByVal xlWorkBook As Excel.Workbook, mCondStr As String)
        Dim DtTable As DataTable = Nothing
        Dim xlWorkSheet As Excel.Worksheet
        Dim I As Integer = 0
        xlWorkSheet = xlWorkBook.Worksheets("b2cl")

        mQry = " SELECT Max(Sgr.RegistrationNo) As GSTINofRecipient, Max(Sg.Name) As ReceiverName, Max(H.ManualRefNo) As InvoiceNumber,
                    Max(H.V_Date) As InvoiceDate, Max(H.Net_Amount) As InvoiceValue, Max(S.Code + '-' + S.Description) As PlaceOfSupply, 'N' As ReverseCharge,
                    0 As ApplicableTaxRate, 'Regular' As InvoiceType,	Null As ECommerceGSTIN,	 
                    Max(IfNull(L.Tax1_Per,0) + IfNull(L.Tax2_Per,0) + IfNull(L.Tax3_Per,0)) As Rate,	
                    Sum(L.Taxable_Amount) As TaxableValue, 0 As CessAmount
                    From SaleInvoice H 
                    left join SaleInvoiceDetail L On H.DocID = L.DocID
                    left join SubGroup Sg On H.SaleToParty = Sg.SubCode
                    LEFT JOIN SubGroupRegistration Sgr On Sg.SubCode = Sgr.SubCode And Sgr.RegistrationType = 'GSTIN'
                    LEFT JOIN City C On H.SaleToPartyCity = C.CityCode
                    LEFT JOIN State S on C.State = S.Code " & mCondStr &
                    " And H.SalesTaxGroupParty = '" & PostingGroupSalesTaxParty.Registered & "'  
                    And H.PlaceOfSupply = '" & PlaceOfSupplay.OutsideState & "'
                    And H.Net_Amount > 250000
                    Group BY L.DocID, SalesTaxGroupItem "
        DtTable = AgL.FillData(mQry, AgL.GCn).Tables(0)



        FillGSTR1ExcelFiles(DtTable, xlWorkSheet)
    End Sub
    Private Sub FWriteGSTR1B2CS(ByVal xlWorkBook As Excel.Workbook, mCondStr As String)
        Dim DtTable As DataTable = Nothing
        Dim xlWorkSheet As Excel.Worksheet
        Dim I As Integer = 0
        xlWorkSheet = xlWorkBook.Worksheets("b2cs")



        mQry = " SELECT 'OE' As Type, S.ManualCode || '-' || S.Description As PlaceOfSupply,  Null As ApplicablePercentOfTaxRate,
                    IfNull(L.Tax1_Per,0) + IfNull(L.Tax2_Per,0) + IfNull(L.Tax3_Per,0) As Rate,
                    Sum(L.Taxable_Amount) As TaxableValue, 0 As CessAmount, Null As ECommerceGSTIN
                    From SaleInvoice H 
                    left join SaleInvoiceDetail L On H.DocID = L.DocID
                    left join SubGroup Sg On H.SaleToParty = Sg.SubCode
                    LEFT JOIN SubGroupRegistration Sgr On Sg.SubCode = Sgr.SubCode And Sgr.RegistrationType = 'GSTIN'
                    LEFT JOIN City C On H.SaleToPartyCity = C.CityCode
                    LEFT JOIN State S on C.State = S.Code " & mCondStr &
                    " And H.SalesTaxGroupParty = '" & PostingGroupSalesTaxParty.Unregistered & "'
                    And H.PlaceOfSupply = '" & PlaceOfSupplay.WithinState & "'
                    Group BY S.ManualCode || '-' || S.Description, SalesTaxGroupItem, 
                    IfNull(L.Tax1_Per,0) + IfNull(L.Tax2_Per,0) + IfNull(L.Tax3_Per,0) "
        DtTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

        FillGSTR1ExcelFiles(DtTable, xlWorkSheet)
    End Sub
    Private Sub FWriteGSTR1CDNR(ByVal xlWorkBook As Excel.Workbook, mCondStr As String)
        Dim DtTable As DataTable = Nothing
        Dim xlWorkSheet As Excel.Worksheet
        Dim I As Integer = 0
        xlWorkSheet = xlWorkBook.Worksheets("cdnr")

        mQry = " SELECT Sgr.RegistrationNo As GSTINofRecipient, Sg.Name As ReceiverName, Si.ManualRefNo As InvoiceNumber, Si.V_Date As InvoiceDate,
                H.ManualRefNo As LedgerHeadNo, H.V_Date As LedgerHeadDate, substr(Vt.Description,1,1) As DocumentType,
                S.ManualCode || '-' || S.Description As PlaceOfSupply, Lc.Net_Amount As LedgerHeadValue, 
                Null As ApplicableTaxRate, L.SalesTaxGroupItem As Rate,
                0 As TaxableValue, 0 As CessAmount, NUll As PreGST
                From LedgerHead H 
                Left join LedgerHeadDetail L on H.DocID = L.DocID
                LEft join LedgerHeadDetailCharges Lc ON L.DocID = Lc.DocID and L.Sr = Lc.Sr
                left join Voucher_Type Vt On H.V_Type = Vt.V_Type
                left join SubGroup Sg On H.Subcode = Sg.SubCode
                LEFT JOIN SubGroupRegistration Sgr On Sg.SubCode = Sgr.SubCode And Sgr.RegistrationType = 'Sales Tax No'
                Left join SaleInvoiceDetail Sid On L.SpecificationDocID = Sid.DocID And L.SpecificationDocIDSr = Sid.Sr
                Left join SaleInvoice Si On Sid.DocID = Si.DocID
                LEFT JOIN City C On Si.SaleToPartyCity = C.CityCode
                LEFT JOIN State S on C.State = S.Code " & mCondStr &
                " And 1=2 "
        DtTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

        FillGSTR1ExcelFiles(DtTable, xlWorkSheet)
    End Sub
    Private Sub FWriteGSTR1CDNUR(ByVal xlWorkBook As Excel.Workbook, mCondStr As String)
        Dim DtTable As DataTable = Nothing
        Dim xlWorkSheet As Excel.Worksheet
        Dim I As Integer = 0
        xlWorkSheet = xlWorkBook.Worksheets("cdnur")

        mQry = " SELECT Si.V_Date As InvoiceDate, S.ManualCode || '-' || S.Description As PlaceOfSupply, 
                Lc.Net_Amount As LedgerLineValue, Null As ApplicableTaxRate, L.SalesTaxGroupItem As Rate,
                0 As TaxableValue, 0 As CessAmount, NUll As PreGST
                From LedgerHead H 
                Left join LedgerHeadDetail L on H.DocID = L.DocID
                LEft join LedgerHeadDetailCharges Lc ON L.DocID = Lc.DocID and L.Sr = Lc.Sr
                left join Voucher_Type Vt On H.V_Type = Vt.V_Type
                left join SubGroup Sg On H.Subcode = Sg.SubCode
                LEFT JOIN SubGroupRegistration Sgr On Sg.SubCode = Sgr.SubCode And Sgr.RegistrationType = 'GSTIN'
                Left join SaleInvoiceDetail Sid On L.SpecificationDocID = Sid.DocID And L.SpecificationDocIDSr = Sid.Sr
                Left join SaleInvoice Si On Sid.DocID = Si.DocID
                LEFT JOIN City C On Si.SaleToPartyCity = C.CityCode
                LEFT JOIN State S on C.State = S.Code " & mCondStr &
                " And Si.SalesTaxGroupParty = '" & PostingGroupSalesTaxParty.Unregistered & "'
                And Si.PlaceOfSupply = '" & PlaceOfSupplay.OutsideState & "'
                And Si.Net_Amount > 250000 "
        DtTable = AgL.FillData(mQry, AgL.GCn).Tables(0)



        FillGSTR1ExcelFiles(DtTable, xlWorkSheet)
    End Sub
    Private Sub FWriteGSTR1EXEMP(ByVal xlWorkBook As Excel.Workbook, mCondStr As String)
        Dim DtTable As DataTable = Nothing
        Dim xlWorkSheet As Excel.Worksheet
        Dim I As Integer = 0
        xlWorkSheet = xlWorkBook.Worksheets("exemp")

        mQry = " SELECT Ic.Description As Description, 
                Sum(Case When L.SalesTaxGroupItem = 'GST 0%' Then L.Amount Else 0 End) As NilRatedSupplies,
                Sum(Case When L.SalesTaxGroupItem = 'GST Excempt' Then L.Amount Else 0 End) As ExemptedSupplies,
                0 As NonGSTSupplies
                From SaleInvoice H 
                Left join SaleInvoiceDetail L on H.DocId = L.DocID 
                Left join Item I on L.Item = I.Code
                Left Join ItemCategory Ic On I.ItemCategory = Ic.Code " & mCondStr &
                " And L.SalesTaxGroupItem In ('GST 0%','GST Excempt')
                Group By Ic.Description "
        DtTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

        FillGSTR1ExcelFiles(DtTable, xlWorkSheet)
    End Sub
    Private Sub FWriteGSTR1HSN(ByVal xlWorkBook As Excel.Workbook, mCondStr As String)
        Dim DtTable As DataTable = Nothing
        Dim xlWorkSheet As Excel.Worksheet
        Dim I As Integer = 0
        xlWorkSheet = xlWorkBook.Worksheets("hsn")

        mQry = " SELECT I.HSN, Max(Ic.Description) As Description, 'PCS-PIECES' As UQC,
                Sum(L.Qty) As TotalQuantity, Sum(L.Amount) As TotalValue, Sum(L.Taxable_Amount) As TaxableValue,
                Sum(L.Tax1) As IntegratedTaxAmount,
                Sum(L.Tax2) As CentralTaxAmount,
                Sum(L.Tax3) As StateTaxAmount,
                0 As CessAmount
                From SaleInvoice H 
                Left join SaleInvoiceDetail L on H.DocId = L.DocID 
                Left join Item I on L.Item = I.Code
                Left Join Unit U on I.Unit = U.Code
                Left Join ItemCategory Ic On I.ItemCategory = Ic.Code " & mCondStr &
                " Group By I.HSN "
        DtTable = AgL.FillData(mQry, AgL.GCn).Tables(0)


        For I = 0 To DtTable.Rows.Count - 1
            If AgL.XNull(DtTable.Rows(I)("HSN")) = "" Then
                ErrorLog += "Some product has blank HSN Code under Category " & AgL.XNull(DtTable.Rows(I)("Description")) & vbCrLf
            End If
        Next

        FillGSTR1ExcelFiles(DtTable, xlWorkSheet)
    End Sub
    Private Sub FWriteGSTR1DOC(ByVal xlWorkBook As Excel.Workbook, mCondStr As String)
        Dim DtTable As DataTable = Nothing
        Dim xlWorkSheet As Excel.Worksheet
        Dim I As Integer = 0
        xlWorkSheet = xlWorkBook.Worksheets("docs")

        mQry = " SELECT 'Invoices for outward supply' As NatureOfDocument, Min(H.ManualRefNo) As SrNoFrom, Max(H.ManualRefNo) As SrNoTo, Count(*) As TotalNumber, 0 As Cancelled
                    From SaleInvoice H 
                    Left JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type " & mCondStr
        DtTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

        FillGSTR1ExcelFiles(DtTable, xlWorkSheet)
    End Sub
    Private Sub FillGSTR1ExcelFiles(DtTable As DataTable, xlWorkSheet As Excel.Worksheet)
        If DtTable.Rows.Count > 0 Then
            For ColIndex As Integer = 0 To DtTable.Columns.Count - 1
                Dim ColumnValues(0, 0) As Object
                ReDim ColumnValues(0 To DtTable.Rows.Count - 1, 0)
                For I As Integer = 0 To DtTable.Rows.Count - 1
                    If DtTable.Columns(ColIndex).ColumnName.Contains("Date") Then
                        If AgL.PubServerName = "" Then
                            ColumnValues(I, 0) = AgL.XNull(AgL.RetDate(DtTable.Rows(I)(ColIndex)))
                        Else
                            ColumnValues(I, 0) = CDate(AgL.XNull(DtTable.Rows(I)(ColIndex))).ToString("dd/MMM/yyyy")
                        End If
                    Else
                        ColumnValues(I, 0) = AgL.XNull(DtTable.Rows(I)(ColIndex)).ToString().Replace("(", "").Replace(")", "")
                    End If
                Next
                xlWorkSheet.Range(GetExcelColumnName(ColIndex + 1) + (5).ToString + ":" + GetExcelColumnName(ColIndex + 1) + (5 + DtTable.Rows.Count - 1).ToString).Value = ColumnValues
            Next
        End If
        ClsMain.FReleaseObjects(xlWorkSheet)
    End Sub
#End Region

    Private Function GetExcelColumnName(columnNumber As Integer) As String
        Dim dividend As Integer = columnNumber
        Dim columnName As String = String.Empty
        Dim modulo As Integer

        While dividend > 0
            modulo = (dividend - 1) Mod 26
            columnName = Convert.ToChar(65 + modulo).ToString() & columnName
            dividend = CInt((dividend - modulo) / 26)
        End While

        Return columnName
    End Function
#Region "Export Data To Sqlite"
    Private Sub ProcExportDataToSqlite()
        Dim DtSaleInvoice As DataTable
        Dim DtSaleInvoiceTrnSetting As DataTable
        Dim DtSaleInvoiceTransport As DataTable
        Dim DtSaleInvoicePayment As DataTable
        Dim DtSaleInvoiceDetail As DataTable
        Dim DtSaleInvoiceDimensionDetail As DataTable
        Dim DtSaleInvoiceDetailHelpValues As DataTable
        Dim DtLedger As DataTable
        Dim DtStock As DataTable
        Dim mStrMainQry As String = ""

        mStrMainQry = "Select H.DocId From SaleInvoice H
                    Where Date(H.V_Date) Between " & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & " 
                    And " & AgL.Chk_Date(CDate(ReportFrm.FGetText(1)).ToString("s")) & " 
                    And H.V_Type = 'SID' "

        mQry = " Select H.* From (" & mStrMainQry & ") As VMain 
                    LEFT JOIN SaleInvoice H ON VMain.DocId = H.DocId 
                    Where H.DocId Is Not Null "
        DtSaleInvoice = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = " Select H.* From (" & mStrMainQry & ") As VMain LEFT JOIN SaleInvoiceTrnSetting H ON VMain.DocId = H.DocId 
                    Where H.DocId Is Not Null "
        DtSaleInvoiceTrnSetting = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = " Select H.* From (" & mStrMainQry & ") As VMain LEFT JOIN SaleInvoiceTransport H ON VMain.DocId = H.DocId 
                    Where H.DocId Is Not Null "
        DtSaleInvoiceTransport = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = " Select H.* From (" & mStrMainQry & ") As VMain 
                    LEFT JOIN SaleInvoicePayment H ON VMain.DocId = H.DocId 
                    Where H.DocId Is Not Null "
        DtSaleInvoicePayment = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = " Select H.* From (" & mStrMainQry & ") As VMain LEFT JOIN SaleInvoiceDetail H ON VMain.DocId = H.DocId 
                    Where H.DocId Is Not Null "
        DtSaleInvoiceDetail = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = " Select H.* From (" & mStrMainQry & ") As VMain LEFT JOIN SaleInvoiceDimensionDetail H ON VMain.DocId = H.DocId 
                    Where H.DocId Is Not Null "
        DtSaleInvoiceDimensionDetail = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = " Select H.* From (" & mStrMainQry & ") As VMain LEFT JOIN SaleInvoiceDetailHelpValues H ON VMain.DocId = H.DocId 
                    Where H.DocId Is Not Null "
        DtSaleInvoiceDetailHelpValues = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = " Select H.* From (" & mStrMainQry & ") As VMain LEFT JOIN Ledger H ON VMain.DocId = H.DocId 
                    Where H.DocId Is Not Null "
        DtLedger = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = " Select H.* From (" & mStrMainQry & ") As VMain LEFT JOIN Stock H ON VMain.DocId = H.DocId 
                    Where H.DocId Is Not Null "
        DtStock = AgL.FillData(mQry, AgL.GCn).Tables(0)


        Dim FilePath As String = My.Computer.FileSystem.SpecialDirectories.Desktop
        Dim SaveFileDialogBox As SaveFileDialog = New SaveFileDialog
        SaveFileDialogBox.Title = "File Name"
        SaveFileDialogBox.InitialDirectory = FilePath
        If ReportFrm.FGetText(0) <> ReportFrm.FGetText(1) Then
            SaveFileDialogBox.FileName = AgL.PubDBName + "_" + ReportFrm.FGetText(0).ToString.Replace("/", "") + "_To_" + ReportFrm.FGetText(1).ToString.Replace("/", "")
        Else
            SaveFileDialogBox.FileName = AgL.PubDBName + "_" + ReportFrm.FGetText(0).ToString.Replace("/", "")
        End If
        If SaveFileDialogBox.ShowDialog = Windows.Forms.DialogResult.Cancel Then Exit Sub
        Dim mDbPath As String = SaveFileDialogBox.FileName

        Dim Connection As New SQLite.SQLiteConnection
        SQLite.SQLiteConnection.CreateFile(mDbPath)

        Dim Command As New SQLite.SQLiteCommand()
        Connection.ConnectionString = "DataSource=" & mDbPath & ";Version=3;New=False;Compress=True;"
        Connection.Open()
        Command.Connection = Connection
        Dim bTransaction As SQLite.SQLiteTransaction = Connection.BeginTransaction()
        Command.Transaction = bTransaction

        Try
            FExportToSqliteTable("SaleInvoice", DtSaleInvoice, Connection, Command)
            FExportToSqliteTable("SaleInvoiceTrnSetting", DtSaleInvoiceTrnSetting, Connection, Command)
            FExportToSqliteTable("SaleInvoiceTransport", DtSaleInvoiceTransport, Connection, Command)
            FExportToSqliteTable("SaleInvoicePayment", DtSaleInvoicePayment, Connection, Command)
            FExportToSqliteTable("SaleInvoiceDetail", DtSaleInvoiceDetail, Connection, Command)
            FExportToSqliteTable("SaleInvoiceDetailHelpValues", DtSaleInvoiceDetailHelpValues, Connection, Command)
            FExportToSqliteTable("SaleInvoiceDimensionDetail", DtSaleInvoiceDimensionDetail, Connection, Command)
            FExportToSqliteTable("Ledger", DtLedger, Connection, Command)
            FExportToSqliteTable("Stock", DtStock, Connection, Command)

            bTransaction.Commit()
            Connection.Close()
            MsgBox("Process Completed Succesfully.", MsgBoxStyle.Information)
        Catch ex As Exception
            bTransaction.Rollback()
            Connection.Close()
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
    Private Sub FExportToSqliteTable(bTableName As String, DtTable As DataTable,
                            Conn As SQLite.SQLiteConnection, Cmd As SQLite.SQLiteCommand)
        Dim DtFields As DataTable
        Dim I As Integer = 0
        Dim J As Integer = 0
        Dim K As Integer = 0
        Dim StrQry As String = ""
        Dim StrInsertionQry As String = ""
        Dim StrValuesQry As String = ""

        mQry = " SELECT ORDINAL_POSITION, COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH, IS_NULLABLE
                    FROM INFORMATION_SCHEMA.COLUMNS
                    WHERE TABLE_NAME = '" & bTableName & "' "
        DtFields = AgL.FillData(mQry, AgL.GCn).Tables(0)

        If Not AgL.IsTableExist(bTableName, Conn) Then
            For I = 0 To DtFields.Rows.Count - 1
                If I = 0 Then
                    StrQry = "CREATE TABLE [" & bTableName & "] ("
                    StrQry += "[" & AgL.XNull(DtFields.Rows(I)("COLUMN_NAME")) & "] 
                    " & AgL.XNull(DtFields.Rows(I)("DATA_TYPE")) &
                    " (" & AgL.VNull(DtFields.Rows(I)("CHARACTER_MAXIMUM_LENGTH")).ToString & ") 
                    " & IIf(AgL.XNull(DtFields.Rows(I)("IS_NULLABLE")) = "No", " Not Null", "Null") & ")"
                    AgL.Dman_ExecuteNonQry(StrQry, Conn, Cmd)
                Else
                    AgL.AddFieldSqlite(Conn, bTableName, AgL.XNull(DtFields.Rows(I)("COLUMN_NAME")),
                                   AgL.XNull(DtFields.Rows(I)("DATA_TYPE")) + "(" + AgL.VNull(DtFields.Rows(I)("CHARACTER_MAXIMUM_LENGTH")).ToString + ")", "",
                                   True)
                End If
            Next
        End If


        For J = 0 To DtTable.Columns.Count - 1
            If J = 0 Then
                StrInsertionQry = " INSERT INTO " & bTableName & "(" & DtTable.Columns(J).ColumnName
            ElseIf J = DtTable.Columns.Count - 1 Then
                StrInsertionQry += ", " & DtTable.Columns(J).ColumnName + ")"
            Else
                StrInsertionQry += ", " & DtTable.Columns(J).ColumnName
            End If
        Next

        For K = 0 To DtTable.Rows.Count - 1
            StrValuesQry = ""
            For J = 0 To DtTable.Columns.Count - 1
                If StrValuesQry = "" Then

                    StrValuesQry = " Values( " & AgL.Chk_Text(DtTable.Rows(K)(DtTable.Columns(J).ColumnName))
                Else
                    If DtTable.Columns(J).ColumnName.ToString.EndsWith("Date") Then
                        StrValuesQry += ", " & AgL.Chk_Date(AgL.XNull(DtTable.Rows(K)(DtTable.Columns(J).ColumnName)))
                    Else
                        StrValuesQry += ", " & AgL.Chk_Text(AgL.XNull(DtTable.Rows(K)(DtTable.Columns(J).ColumnName)))
                    End If
                End If
            Next
            StrValuesQry += ")"
            AgL.Dman_ExecuteNonQry(StrInsertionQry + StrValuesQry, Conn, Cmd)
        Next
    End Sub
#End Region

#Region "Import Data From Sqlite"
    Private Sub ProcImportDataFromSqlite()
        Dim mStrMainQry As String = ""
        Dim I As Integer = 0
        Dim J As Integer = 0
        Dim bSelectionQry As String = ""
        Dim mSqliteDataQry As String = ""


        Dim FilePath As String = My.Computer.FileSystem.SpecialDirectories.Desktop
        Dim OpenFileDialogBox As OpenFileDialog = New OpenFileDialog
        OpenFileDialogBox.Title = "File Name"
        OpenFileDialogBox.InitialDirectory = FilePath
        If OpenFileDialogBox.ShowDialog = Windows.Forms.DialogResult.Cancel Then Exit Sub
        Dim mDbPath As String = OpenFileDialogBox.FileName

        Dim Connection As New SQLite.SQLiteConnection
        Connection.ConnectionString = "DataSource=" & mDbPath & ";Version=3;New=False;Compress=True;"
        Connection.Open()

        Dim mSqlConn As New SqlClient.SqlConnection
        Dim mSqlCmd As New SqlClient.SqlCommand
        Dim mSqlTrans As SqlClient.SqlTransaction

        mSqlConn.ConnectionString = AgL.GCn.ConnectionString
        mSqlConn.Open()
        mSqlCmd.Connection = mSqlConn
        mSqlTrans = mSqlConn.BeginTransaction()
        mSqlCmd.Transaction = mSqlTrans

        Try
            FImportDataFromSqliteTable("SaleInvoice", "H.DocId = H_Temp.DocId", "DocId", Connection, mSqlConn, mSqlCmd, mDbPath)
            FImportDataFromSqliteTable("SaleInvoiceTrnSetting", "H.DocId = H_Temp.DocId", "DocId", Connection, mSqlConn, mSqlCmd, mDbPath)
            FImportDataFromSqliteTable("SaleInvoiceTransport", "H.DocId = H_Temp.DocId", "DocId", Connection, mSqlConn, mSqlCmd, mDbPath)
            FImportDataFromSqliteTable("SaleInvoicePayment", "H.DocId = H_Temp.DocId And H.Sr = H_Temp.Sr", "DocId", Connection, mSqlConn, mSqlCmd, mDbPath)
            FImportDataFromSqliteTable("SaleInvoiceDetail", "H.DocId = H_Temp.DocId And H.Sr = H_Temp.Sr", "DocId", Connection, mSqlConn, mSqlCmd, mDbPath)
            FImportDataFromSqliteTable("SaleInvoiceDetailHelpValues", "H.DocId = H_Temp.DocId And H.Sr = H_Temp.Sr", "DocId", Connection, mSqlConn, mSqlCmd, mDbPath)
            FImportDataFromSqliteTable("SaleInvoiceDimensionDetail", "H.DocId = H_Temp.DocId And H.TSr = H_Temp.TSr And H.Sr = H_Temp.Sr", "DocId", Connection, mSqlConn, mSqlCmd, mDbPath)
            FImportDataFromSqliteTable("Ledger", "H.DocId = H_Temp.DocId And H.V_SNo = H_Temp.V_SNo", "DocId", Connection, mSqlConn, mSqlCmd, mDbPath)
            FImportDataFromSqliteTable("Stock", "H.DocId = H_Temp.DocId And H.TSr = H_Temp.TSr And H.Sr = H_Temp.Sr", "DocId", Connection, mSqlConn, mSqlCmd, mDbPath)

            mQry = "UPDATE Voucher_Prefix
                    SET Voucher_Prefix.Start_Srl_No = V1.V_No_Max
                    FROM (
	                    SELECT H.V_Type, H.V_Prefix, IfNull(Max(H.V_No),0) AS V_No_Max
	                    FROM SaleInvoice H
	                    WHERE H.V_Type = 'SID'
	                    GROUP BY H.V_Type, H.V_Prefix
                    ) AS V1 WHERE V1.V_Type = Voucher_Prefix.V_Type AND V1.V_Prefix = Voucher_Prefix.Prefix"
            AgL.Dman_ExecuteNonQry(mQry, mSqlConn, mSqlCmd)

            mSqlTrans.Commit()
            mSqlConn.Close()
            Connection.Close()
            MsgBox("Process Completed Succesfully.", MsgBoxStyle.Information)
        Catch ex As Exception
            mSqlTrans.Rollback()
            mSqlConn.Close()
            Connection.Close()
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
    Private Sub FImportDataFromSqliteTable(bTableName As String, bJoinCondStr As String, bPrimaryField As String,
                Connection As Object, mSqlConn As Object, mSqlCmd As Object, mDbPath As String)
        Dim mTrans As String = ""
        Dim DtFields As DataTable = Nothing
        Dim DtSqliteTableData As DataTable = Nothing
        Dim I As Integer = 0
        Dim J As Integer = 0
        Dim K As Integer = 0
        Dim StrColumnList As String = ""
        Dim bTempTableName As String = "[#Temp_" + bTableName + "]"

        If AgL.PubServerName = "" Then
            mQry = "PRAGMA table_info(Item)"
        Else
            mQry = "SELECT COLUMN_NAME As Name FROM INFORMATION_SCHEMA.Columns WHERE TABLE_NAME = '" & bTableName & "'  
                ORDER BY ORDINAL_POSITION "
        End If
        DtFields = AgL.FillData(mQry, AgL.GCn).Tables(0)
        StrColumnList = ""
        For J = 0 To DtFields.Rows.Count - 1
            If StrColumnList = "" Then
                StrColumnList = DtFields.Rows(J)("Name")
            Else
                StrColumnList += ", " & DtFields.Rows(J)("Name")
            End If
        Next

        If AgL.PubServerName = "" Then
            mQry = "DROP TABLE IF EXISTS " & bTempTableName & " ;
                    CREATE TABLE " & bTempTableName & " AS SELECT * FROM " & bTableName & ""
            AgL.Dman_ExecuteNonQry(mQry, mSqlConn, mSqlCmd)
        Else
            mQry = "SELECT * INTO " & bTempTableName & " FROM " & bTableName & " WHERE 1 = 2 "
            AgL.Dman_ExecuteNonQry(mQry, mSqlConn, mSqlCmd)
        End If

        If AgL.PubServerName = "" Then
            Try
                mQry = "Attach '" & mDbPath & "' AS Source "
                AgL.Dman_ExecuteNonQry(mQry, mSqlConn, mSqlCmd)
            Catch ex As Exception
            End Try

            mQry = " INSERT INTO " & bTempTableName & "(" & StrColumnList & ")"
            mQry += " Select " & StrColumnList & " From Source." & bTableName & ""
            AgL.Dman_ExecuteNonQry(mQry, mSqlConn, mSqlCmd)
        Else
            Dim commandSourceData As SQLiteCommand = New SQLiteCommand("Select " & StrColumnList & " From " & bTableName & " ", Connection)
            Dim reader As SQLiteDataReader = commandSourceData.ExecuteReader

            Using bulkCopy As SqlBulkCopy = New SqlBulkCopy(mSqlConn, SqlBulkCopyOptions.Default, mSqlCmd.Transaction)
                bulkCopy.DestinationTableName = bTempTableName
                bulkCopy.BulkCopyTimeout = 500
                bulkCopy.WriteToServer(reader)
                reader.Close()
            End Using
        End If


        StrColumnList = StrColumnList.Replace("00", "DateTime")

        mQry = "INSERT INTO " & bTableName & "(" & StrColumnList & ")
                Select H_Temp." & Replace(StrColumnList, ",", ",H_Temp.") & "
                From " & bTempTableName & " H_Temp 
                LEFT JOIN " & bTableName & " H On " & bJoinCondStr &
                " Where H." & bPrimaryField & " Is Null "
        AgL.Dman_ExecuteNonQry(mQry, mSqlConn, mSqlCmd)
    End Sub
#End Region

#Region "Restore Database"
    Private Sub ProcRestoreDatabase()
        Try
            If MsgBox("Are you sure you want to proceed with the data file' restore?" & vbNewLine & "This will overwrite your data files in the Back-Up file.", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "") = MsgBoxResult.Yes Then
                Dim bCurrentSite_Code As String = AgL.PubSiteCode
                Dim bCurrentDiv_Code As String = AgL.PubDivCode
                Dim bCurrentComp_Code As String = AgL.PubCompCode

                Dim FilePath As String = My.Computer.FileSystem.SpecialDirectories.Desktop
                Dim OpenFileDialogBox As OpenFileDialog = New OpenFileDialog
                OpenFileDialogBox.Title = "File Name"
                OpenFileDialogBox.InitialDirectory = FilePath
                If OpenFileDialogBox.ShowDialog = Windows.Forms.DialogResult.Cancel Then Exit Sub
                Dim mDbPath As String = OpenFileDialogBox.FileName



                Dim Conn As New SqlConnection
                Dim Cmd As New SqlCommand
                Conn.ConnectionString = AgL.GCn.ConnectionString
                Conn.Open()
                Cmd.Connection = Conn
                Cmd.CommandTimeout = 1000

                Cmd.CommandText = "Alter Database " & AgL.PubDBName & " Set Single_user With Rollback Immediate"
                Cmd.ExecuteNonQuery()

                Cmd.CommandText = "Use Master"
                Cmd.ExecuteNonQuery()

                Cmd.CommandText = " Restore Database " & AgL.PubDBName & " FROM DISK='" & mDbPath & "' with replace,
                        MOVE '" & AgL.PubDBName & "' TO 'D:\Database Files\" & AgL.PubDBName & ".mdf',
                        MOVE '" & AgL.PubDBName + "_log" & "' TO 'D:\Database Files\" & AgL.PubDBName & ".ldf'"
                Cmd.ExecuteNonQuery()

                Cmd.CommandText = "Alter Database " & AgL.PubDBName & " Set MULTI_USER"
                Cmd.ExecuteNonQuery()

                If Not FOpenIni(StrPath + "\" + IniName, AgL.PubUserName, AgL.PubUserPassword) Then
                    MsgBox("Can't Connect to Database")
                Else
                    AgL.PubSiteCode = bCurrentSite_Code
                    AgL.PubDivCode = bCurrentDiv_Code
                    AgL.PubCompCode = bCurrentComp_Code
                    AgL.PubLoginDate = DateTime.Now()
                    AgL.PubLastTransactionDate = Now()
                    AgIniVar.FOpenConnection(AgL.PubCompCode, AgL.PubSiteCode)
                    AgIniVar.ProcSwapSiteCompanyDetail()
                End If

                MsgBox("Process Complete.")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
#End Region

#Region "Delete W Data"
    Private Sub ProcDeleteData()
        Dim mTrans As String
        Dim bConStr$ = ""
        Dim bOMSIdConStr$ = ""
        Dim Connection_Pakka As New SQLite.SQLiteConnection
        Dim mDbPath As String = ""
        Dim mDbEncryption As String = ""

        mDbPath = AgL.INIRead(StrPath + "\" + IniName, "CompanyInfo", "ActualDBPath", "")
        mDbEncryption = AgL.INIRead(StrPath + "\" + IniName, "CompanyInfo", "Encryption", "")
        If mDbEncryption = "N" Then
            Connection_Pakka.ConnectionString = "DataSource=" & mDbPath & ";Version=3;"
        Else
            Connection_Pakka.ConnectionString = "DataSource=" & mDbPath & ";Version=3;Password=" & AgLibrary.ClsConstant.PubDbPassword & ";"
        End If
        Connection_Pakka.Open()


        If ReportFrm.FGetText(0) = "" Then MsgBox("As On Date is required.", MsgBoxStyle.Information) : Exit Sub
        If ReportFrm.FGetText(1) = "" Then MsgBox("Party is required.", MsgBoxStyle.Information) : Exit Sub

        If MsgBox("Are you sure you want to proceed delete data ?" & vbNewLine & "This will wash selected data.", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "") = MsgBoxResult.Yes Then
            Try
                AgL.ECmd = AgL.GCn.CreateCommand
                AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
                AgL.ECmd.Transaction = AgL.ETrans
                mTrans = "Begin"

                '''''''''''''For Updating Updaload in Pakka Databsae''''''''''''

                bOMSIdConStr = " Where DocId In (SELECT H.OMSId
                            FROM SaleInvoice H 
                            WHERE Date(H.V_Date) <= " & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & ReportFrm.GetWhereCondition("H.SaleToParty", 1) & ")"

                mQry = " UPDATE SaleInvoice Set IsUploadedAlready = 1 " & bOMSIdConStr
                AgL.Dman_ExecuteNonQry(mQry, Connection_Pakka)

                bOMSIdConStr = " Where DocId In (SELECT H.OMSId
                            FROM PurchInvoice H 
                            WHERE Date(H.V_Date) <= " & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & ReportFrm.GetWhereCondition("H.Vendor", 1) & ")"

                mQry = " UPDATE PurchInvoice Set IsUploadedAlready = 1 " & bOMSIdConStr
                AgL.Dman_ExecuteNonQry(mQry, Connection_Pakka)

                bOMSIdConStr = " Where DocId In (SELECT H.OMSId
                            FROM LedgerHead H 
                            WHERE Date(H.V_Date) <= " & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & ReportFrm.GetWhereCondition("H.SubCode", 1) & ")"

                mQry = " UPDATE LedgerHead Set IsUploadedAlready = 1 " & bOMSIdConStr
                AgL.Dman_ExecuteNonQry(mQry, Connection_Pakka)

                bOMSIdConStr = " Where DocId In (SELECT H.OMSId
                            FROM StockHead H 
                            WHERE Date(H.V_Date) <= " & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & ReportFrm.GetWhereCondition("H.SubCode", 1) & ")"

                mQry = " UPDATE StockHead Set IsUploadedAlready = 1 " & bOMSIdConStr
                AgL.Dman_ExecuteNonQry(mQry, Connection_Pakka)

                '''''''''''''End For Updating Updaload in Pakka Databsae''''''''''''


                bConStr = " Where DocId In (SELECT H.DocID
                            FROM SaleInvoice H 
                            WHERE Date(H.V_Date) <= " & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & ReportFrm.GetWhereCondition("H.SaleToParty", 1) & ")"

                mQry = "DELETE FROM SaleInvoiceBarcodeLastTransactionValues " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM SaleInvoiceDetail " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM SaleInvoiceDetailBarCodeValues " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM SaleInvoiceDetailHelpValues " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM SaleInvoiceDetailSku " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM SaleInvoiceDimensionDetail " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM SaleInvoiceDimensionDetailSku " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM SaleInvoiceGeneratedEntries " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM SaleInvoicePayment " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM SaleInvoiceReferences " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM SaleInvoice " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)


                bConStr = " Where DocId In (SELECT H.DocID
                            FROM PurchInvoice H 
                            WHERE Date(H.V_Date) <= " & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & ReportFrm.GetWhereCondition("H.Vendor", 1) & ")"


                mQry = "DELETE FROM PurchInvoiceBarcodeLastTransactionValues " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM PurchInvoiceDetail " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM PurchInvoiceDetailBarCodeValues " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM PurchInvoiceDetailBom " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM PurchInvoiceDetailBomSku " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM PurchInvoiceDetailHelpValues " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM PurchInvoiceDetailSku " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM PurchInvoiceDimensionDetail " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM PurchInvoiceDimensionDetailSku " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM PurchInvoiceTransport " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM PurchInvoice " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                bConStr = " Where DocId In (SELECT H.DocID
                            FROM LedgerHead H 
                            WHERE Date(H.V_Date) <= " & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & ReportFrm.GetWhereCondition("H.SubCode", 1) & ")"

                mQry = "DELETE FROM Ledger " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM LedgerAdj " & " Where Vr_DocId In (SELECT H.DocID
                            FROM LedgerHead H 
                            WHERE Date(H.V_Date) <= " & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & ReportFrm.GetWhereCondition("H.SubCode", 1) & ")"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM LedgerHeadCharges " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM LedgerHeadDetail " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM LedgerHeadDetailCharges " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM LedgerHeadDetailChequePrinting " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM LedgerItemAdj " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM LedgerM " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                mQry = "DELETE FROM LedgerHead " & bConStr
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)


                'For Deleting All Data
                'mQry = "Delete  From  SaleInvoiceDimensionDetailSku ;
                '        Delete  From  SaleInvoiceDimensionDetail ;
                '        Delete  From  SaleInvoiceDetailSku;
                '        Delete  From  SaleInvoiceDetail ;
                '        Delete  From  SaleInvoiceGeneratedEntries ;
                '        Delete  From  SaleInvoiceLastTransactionValues ;
                '        Delete  From  SaleInvoicePayment ;
                '        Delete  From  SaleInvoiceReferences;
                '        Delete  From  SaleInvoiceTransport ;
                '        Delete  From  SaleInvoiceTrnSetting ;
                '        Delete  From  SaleInvoiceBarcodeLastTransactionValues ;
                '        Delete  From  SaleInvoiceDetailBarCodeValues ;
                '        Delete  From  SaleInvoiceDetailHelpValues ;
                '        Delete  From  SaleInvoice ;


                '        Delete  From  PurchInvoiceDimensionDetailSku;
                '        Delete  From  PurchInvoiceDimensionDetail ;
                '        Delete  From  PurchInvoiceDetailSku ;
                '        Delete  From  PurchInvoiceDetail ;
                '        Delete  From  PurchInvoiceTransport ;
                '        Delete  From  PurchInvoice ;


                '        Delete  From  Cloth_SupplierSettlementInvoices ;
                '        Delete  From  Cloth_SupplierSettlementInvoicesAdjustment ;
                '        Delete  From  Cloth_SupplierSettlementInvoicesLine ;
                '        Delete  From  Cloth_SupplierSettlementPayments ;


                '        Delete  From  ItemGroupPerson ;
                '        Delete  From  LogTable ;

                '        Delete  From  LedgerHeadDetailCharges ;
                '        Delete  From  LedgerHeadDetail ;
                '        Delete  From  Ledger ;
                '        Delete  From  LedgerAdj ;
                '        Delete  From  LedgerHeadCharges ;
                '        Delete  From  LedgerM ;
                '        Delete  From  LedgerHead ;


                '        Delete  From  StockHeadDimensionDetailSku ;
                '        Delete  From  StockHeadDimensionDetail ;
                '        Delete  From  StockHeadDetailBomSku;
                '        Delete  From  StockHeadDetailBom ;
                '        Delete  From  StockHeadDetailTransfer ;
                '        Delete  From  StockHeadDetailSku;
                '        Delete  From  StockHeadDetail ;
                '        Delete  From  StockHeadDetailBarCodeValues ;
                '        Delete  From  StockHeadDetailBase ;
                '        Delete  From  Stock;
                '        Delete  From  StockProcess;
                '        Delete  From  StockAdj ;
                '        Delete  From  StockHeadTransfer ;
                '        Delete  From  StockHeadTransport ;
                '        Delete  From  StockHead ;


                '        Delete  From  TransactionReferences ;
                '        Delete  From  WLedgerHeadDetail ;
                '        Delete  From  WPurchInvoiceDetail; 
                '        Delete  From  WSaleInvoiceDetail;
                '        Delete from item where code  like 'D1%' or code  like 'E1%' ;
                '        Delete From Subgroup Where Subcode Like 'D1%' or Subcode  like 'E1%' "

                AgL.ETrans.Commit()
                mTrans = "Commit"
                MsgBox("Process Complete.", MsgBoxStyle.Information)
            Catch ex As Exception
                AgL.ETrans.Rollback()
                MsgBox(ex.Message)
            End Try
        End If
    End Sub
#End Region



    Private Sub ProcExportStockIssueDataToSqlite()
        Dim DtStockHead As DataTable
        Dim DtStockHeadDetail As DataTable
        Dim DtStockHeadDimensionDetail As DataTable
        Dim DtStock As DataTable
        Dim DtItemCategory As DataTable
        Dim DtItemGroup As DataTable
        Dim DtItem As DataTable
        Dim mStrMainQry As String = ""

        mStrMainQry = "Select H.DocId From StockHead H
                    Where Date(H.V_Date) Between " & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & " 
                    And " & AgL.Chk_Date(CDate(ReportFrm.FGetText(1)).ToString("s")) & " 
                    And H.V_Type = 'ISS' "

        mQry = " Select H.* From (" & mStrMainQry & ") As VMain 
                    LEFT JOIN StockHead H ON VMain.DocId = H.DocId 
                    Where H.DocId Is Not Null "
        DtStockHead = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = " Select H.* From (" & mStrMainQry & ") As VMain LEFT JOIN StockHeadDetail H ON VMain.DocId = H.DocId 
                    Where H.DocId Is Not Null "
        DtStockHeadDetail = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = " Select H.* From (" & mStrMainQry & ") As VMain LEFT JOIN StockHeadDimensionDetail H ON VMain.DocId = H.DocId 
                    Where H.DocId Is Not Null "
        DtStockHeadDimensionDetail = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = " Select H.* From (" & mStrMainQry & ") As VMain LEFT JOIN Stock H ON VMain.DocId = H.DocId 
                    Where H.DocId Is Not Null "
        DtStock = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = " Select Distinct Ic.Code, Ic.ManualCode, Ic.Description As Description, Ic.DisplayName, 
                Ic.Unit, Ic.DealQty, Ic.DealUnit, Ic.ItemGroup, Ic.ItemCategory, Ic.ItemType, 
                Ic.Godown, Ic.GodownSection, Ic.QcGroup, Ic.CurrentStock, 
                Ic.CurrentIssued, Ic.CurrentRequisition, Ic.IsDeleted, Ic.UpcCode, 
                Ic.Bom, Ic.PurchaseRate, Ic.Rate, Ic.ItemImportExportGroup, Ic.EntryBy, 
                Ic.EntryDate, Ic.EntryType, Ic.EntryStatus, Ic.ApproveBy, Ic.ApproveDate, 
                Ic.MoveToLog, Ic.MoveToLogDate, Ic.Status, Ic.Div_Code, Ic.UID, 
                Ic.SalesTaxPostingGroup, Ic.ExcisePostingGroup, Ic.EntryTaxPostingGroup, 
                Ic.LastPurchaseRate, Ic.LastPurchaseDate, Ic.LastPurchaseInvoice, 
                Ic.Specification, Ic.ProcessSequence, Ic.ItemInvoiceGroup, Ic.StockYN, 
                Ic.StockOn, Ic.UnitMultiplier, Ic.BillingOn, Ic.Manufacturer, Ic.VatCommodityCode, 
                Ic.ReorderLevel, Ic.Design, Ic.Size, Ic.Deal, Ic.ProfitMarginPer, Ic.ProdBatchQty, 
                Ic.ProdBatchUnit, Ic.SubCode, Ic.CostCenter, Ic.CustomFields, Ic.GenTable, 
                Ic.GenCode, Ic.Gross_Weight, Ic.IsSystemDefine, Ic.IsRestricted_InTransaction, 
                Ic.IsMandatory_UnitConversion, Ic.HSN, Ic.Barcode, Ic.ShowItemInOtherDivisions, 
                Ic.MRP, Ic.DiscountCalculationPatternPurchase, Ic.DiscountPerPurchase, 
                Ic.DiscountCalculationPatternSale, Ic.DiscountPerSale, Ic.AdditionPerSale, 
                Ic.MaintainStockYn, Ic.DefaultSupplier, Ic.V_Type, Ic.Default_DiscountPerSale, 
                Ic.Default_AdditionalDiscountPerSale, Ic.Default_AdditionPerSale, Ic.Default_DiscountPerPurchase, 
                Ic.Default_AdditionalDiscountPerPurchase, Ic.Default_MarginPer, Ic.BarcodeType, 
                Ic.BarcodePattern, Ic.PrintingDescription, Ic.Department, Ic.Dimension1, 
                Ic.Dimension2, Ic.Dimension3, Ic.Dimension4, Ic.Default_AdditionPerPurchase, 
                Ic.ShowItemInOtherSites, Ic.Site_Code, Ic.LockText, Ic.Parent, Ic.TopParent, 
                Ic.RawMaterial, Ic.BaseItem, Ic.WastagePer, Ic.WeightForPer, Ic.Code As OmsId, 
                Ic.UploadDate, Ic.Tags, Ic.SalesRepresentativeCommissionPer, Ic.GenDocId, 
                Ic.IsNewItemAllowedPurch, Ic.IsNewDimension1AllowedPurch, Ic.IsNewDimension2AllowedPurch, 
                Ic.IsNewDimension3AllowedPurch, Ic.IsNewDimension4AllowedPurch, Ic.SalesAc, Ic.PurchaseAc  
                From (" & mStrMainQry & ") As VMain 
                LEFT JOIN StockHeadDetail L ON VMain.DocId = L.DocId 
                LEFT JOIN Item I On L.Item = I.Code
                LEFT JOIN Item Ic On I.ItemCategory = Ic.Code
                Where L.DocId Is Not Null "
        DtItemCategory = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = " Select Distinct Ig.Code, Ig.ManualCode, IfNull(Ig.PrintingDescription,Ig.Description) As Description, Ig.DisplayName, 
                Ig.Unit, Ig.DealQty, Ig.DealUnit, Ig.ItemGroup, Ig.ItemCategory, Ig.ItemType, 
                Ig.Godown, Ig.GodownSection, Ig.QcGroup, Ig.CurrentStock, 
                Ig.CurrentIssued, Ig.CurrentRequisition, Ig.IsDeleted, Ig.UpcCode, 
                Ig.Bom, Ig.PurchaseRate, Ig.Rate, Ig.ItemImportExportGroup, Ig.EntryBy, 
                Ig.EntryDate, Ig.EntryType, Ig.EntryStatus, Ig.ApproveBy, Ig.ApproveDate, 
                Ig.MoveToLog, Ig.MoveToLogDate, Ig.Status, Ig.Div_Code, Ig.UID, 
                Ig.SalesTaxPostingGroup, Ig.ExcisePostingGroup, Ig.EntryTaxPostingGroup, 
                Ig.LastPurchaseRate, Ig.LastPurchaseDate, Ig.LastPurchaseInvoice, 
                Ig.Specification, Ig.ProcessSequence, Ig.ItemInvoiceGroup, Ig.StockYN, 
                Ig.StockOn, Ig.UnitMultiplier, Ig.BillingOn, Ig.Manufacturer, Ig.VatCommodityCode, 
                Ig.ReorderLevel, Ig.Design, Ig.Size, Ig.Deal, Ig.ProfitMarginPer, Ig.ProdBatchQty, 
                Ig.ProdBatchUnit, Ig.SubCode, Ig.CostCenter, Ig.CustomFields, Ig.GenTable, 
                Ig.GenCode, Ig.Gross_Weight, Ig.IsSystemDefine, Ig.IsRestricted_InTransaction, 
                Ig.IsMandatory_UnitConversion, Ig.HSN, Ig.Barcode, Ig.ShowItemInOtherDivisions, 
                Ig.MRP, Ig.DiscountCalculationPatternPurchase, Ig.DiscountPerPurchase, 
                Ig.DiscountCalculationPatternSale, Ig.DiscountPerSale, Ig.AdditionPerSale, 
                Ig.MaintainStockYn, Ig.DefaultSupplier, Ig.V_Type, Ig.Default_DiscountPerSale, 
                Ig.Default_AdditionalDiscountPerSale, Ig.Default_AdditionPerSale, Ig.Default_DiscountPerPurchase, 
                Ig.Default_AdditionalDiscountPerPurchase, Ig.Default_MarginPer, Ig.BarcodeType, 
                Ig.BarcodePattern, Ig.PrintingDescription, Ig.Department, Ig.Dimension1, 
                Ig.Dimension2, Ig.Dimension3, Ig.Dimension4, Ig.Default_AdditionPerPurchase, 
                Ig.ShowItemInOtherSites, Ig.Site_Code, Ig.LockText, Ig.Parent, Ig.TopParent, 
                Ig.RawMaterial, Ig.BaseItem, Ig.WastagePer, Ig.WeightForPer, Ig.Code As OmsId, 
                Ig.UploadDate, Ig.Tags, Ig.SalesRepresentativeCommissionPer, Ig.GenDocId, 
                Ig.IsNewItemAllowedPurch, Ig.IsNewDimension1AllowedPurch, Ig.IsNewDimension2AllowedPurch, 
                Ig.IsNewDimension3AllowedPurch, Ig.IsNewDimension4AllowedPurch, Ig.SalesAc, Ig.PurchaseAc 
                From (" & mStrMainQry & ") As VMain 
                LEFT JOIN StockHeadDetail L ON VMain.DocId = L.DocId 
                LEFT JOIN Item I On L.Item = I.Code
                LEFT JOIN Item Ig On I.ItemGroup = Ig.Code
                Where L.DocId Is Not Null "
        DtItemGroup = AgL.FillData(mQry, AgL.GCn).Tables(0)


        mQry = " Select Distinct I.Code, I.ManualCode, 
                I.Specification || '-' || IfNull(Ig.PrintingDescription,Ig.Description) || '-' || Ic.Description As Description, 
                Null As DisplayName, I.Unit, I.DealQty, I.DealUnit, I.ItemGroup, I.ItemCategory, I.ItemType, 
                I.Godown, I.GodownSection, I.QcGroup, I.CurrentStock, 
                I.CurrentIssued, I.CurrentRequisition, I.IsDeleted, I.UpcCode, 
                I.Bom, I.Rate As PurchaseRate, I.Rate, I.ItemImportExportGroup, I.EntryBy, 
                I.EntryDate, I.EntryType, I.EntryStatus, I.ApproveBy, I.ApproveDate, 
                I.MoveToLog, I.MoveToLogDate, I.Status, I.Div_Code, I.UID, 
                I.SalesTaxPostingGroup, I.ExcisePostingGroup, I.EntryTaxPostingGroup, 
                I.LastPurchaseRate, I.LastPurchaseDate, I.LastPurchaseInvoice, 
                I.Specification, I.ProcessSequence, I.ItemInvoiceGroup, I.StockYN, 
                I.StockOn, I.UnitMultiplier, I.BillingOn, I.Manufacturer, I.VatCommodityCode, 
                I.ReorderLevel, I.Design, I.Size, I.Deal, I.ProfitMarginPer, I.ProdBatchQty, 
                I.ProdBatchUnit, I.SubCode, I.CostCenter, I.CustomFields, I.GenTable, 
                I.GenCode, I.Gross_Weight, I.IsSystemDefine, I.IsRestricted_InTransaction, 
                I.IsMandatory_UnitConversion, I.HSN, I.Barcode, I.ShowItemInOtherDivisions, 
                I.MRP, I.DiscountCalculationPatternPurchase, I.DiscountPerPurchase, 
                I.DiscountCalculationPatternSale, I.DiscountPerSale, I.AdditionPerSale, 
                I.MaintainStockYn, I.DefaultSupplier, I.V_Type, I.Default_DiscountPerSale, 
                I.Default_AdditionalDiscountPerSale, I.Default_AdditionPerSale, I.Default_DiscountPerPurchase, 
                I.Default_AdditionalDiscountPerPurchase, I.Default_MarginPer, I.BarcodeType, 
                I.BarcodePattern, I.PrintingDescription, I.Department, I.Dimension1, 
                I.Dimension2, I.Dimension3, I.Dimension4, I.Default_AdditionPerPurchase, 
                I.ShowItemInOtherSites, I.Site_Code, I.LockText, I.Parent, I.TopParent, 
                I.RawMaterial, I.BaseItem, I.WastagePer, I.WeightForPer, I.Code As OmsId, 
                I.UploadDate, I.Tags, I.SalesRepresentativeCommissionPer, I.GenDocId, 
                I.IsNewItemAllowedPurch, I.IsNewDimension1AllowedPurch, I.IsNewDimension2AllowedPurch, 
                I.IsNewDimension3AllowedPurch, I.IsNewDimension4AllowedPurch, I.SalesAc, I.PurchaseAc 
                From (" & mStrMainQry & ") As VMain 
                LEFT JOIN StockHeadDetail L ON VMain.DocId = L.DocId 
                LEFT JOIN Item I On L.Item = I.Code
                LEFT JOIN Item Ig On I.ItemGroup = Ig.Code
                LEFT JOIN Item Ic On I.ItemCategory = Ic.Code
                Where L.DocId Is Not Null "
        DtItem = AgL.FillData(mQry, AgL.GCn).Tables(0)


        Dim FilePath As String = My.Computer.FileSystem.SpecialDirectories.Desktop
        Dim SaveFileDialogBox As SaveFileDialog = New SaveFileDialog
        SaveFileDialogBox.Title = "File Name"
        SaveFileDialogBox.InitialDirectory = FilePath
        If ReportFrm.FGetText(0) <> ReportFrm.FGetText(1) Then
            SaveFileDialogBox.FileName = AgL.PubDBName + "_" + ReportFrm.FGetText(0).ToString.Replace("/", "") + "_To_" + ReportFrm.FGetText(1).ToString.Replace("/", "")
        Else
            SaveFileDialogBox.FileName = AgL.PubDBName + "_" + ReportFrm.FGetText(0).ToString.Replace("/", "")
        End If
        If SaveFileDialogBox.ShowDialog = Windows.Forms.DialogResult.Cancel Then Exit Sub
        Dim mDbPath As String = SaveFileDialogBox.FileName

        Dim Connection As New SQLite.SQLiteConnection
        SQLite.SQLiteConnection.CreateFile(mDbPath)

        Dim Command As New SQLite.SQLiteCommand()
        If AgL.PubIsDatabaseEncrypted = "N" Then
            Connection.ConnectionString = "DataSource=" & mDbPath & ";Version=3;New=False;Compress=True;"
        Else
            Connection.ConnectionString = "Data Source=" & mDbPath & ";Version=3;Password=" & AgLibrary.ClsConstant.PubDbPassword & ";"
        End If
        Connection.Open()
        Command.Connection = Connection
        Dim bTransaction As SQLite.SQLiteTransaction = Connection.BeginTransaction()
        Command.Transaction = bTransaction

        Try
            FExportToSqliteTable("Item", DtItemCategory, Connection, Command)
            FExportToSqliteTable("Item", DtItemGroup, Connection, Command)
            FExportToSqliteTable("Item", DtItem, Connection, Command)
            FExportToSqliteTable("StockHead", DtStockHead, Connection, Command)
            FExportToSqliteTable("StockHeadDetail", DtStockHeadDetail, Connection, Command)
            FExportToSqliteTable("StockHeadDimensionDetail", DtStockHeadDimensionDetail, Connection, Command)
            FExportToSqliteTable("Stock", DtStock, Connection, Command)

            bTransaction.Commit()
            Connection.Close()
            MsgBox("Process Completed Succesfully.", MsgBoxStyle.Information)
        Catch ex As Exception
            bTransaction.Rollback()
            Connection.Close()
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
    Private Sub ProcImportStockIssueDataFromSqlite()
        Dim mStrMainQry As String = ""
        Dim mTrans As String = ""
        Dim I As Integer = 0
        Dim J As Integer = 0
        Dim bSelectionQry As String = ""
        Dim mSqliteDataQry As String = ""


        Dim FilePath As String = My.Computer.FileSystem.SpecialDirectories.Desktop
        Dim OpenFileDialogBox As OpenFileDialog = New OpenFileDialog
        OpenFileDialogBox.Title = "File Name"
        OpenFileDialogBox.InitialDirectory = FilePath
        If OpenFileDialogBox.ShowDialog = Windows.Forms.DialogResult.Cancel Then Exit Sub
        Dim mDbPath As String = OpenFileDialogBox.FileName

        Dim Connection As New SQLite.SQLiteConnection
        If AgL.PubIsDatabaseEncrypted = "N" Then
            Connection.ConnectionString = "DataSource=" & mDbPath & ";Version=3;New=False;Compress=True;"
        Else
            Connection.ConnectionString = "Data Source=" & mDbPath & ";Version=3;Password=" & AgLibrary.ClsConstant.PubDbPassword & ";"
        End If
        Connection.Open()


        AgL.ECmd = AgL.GCn.CreateCommand
        AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
        AgL.ECmd.Transaction = AgL.ETrans
        mTrans = "Begin"


        'Dim mSqlConn As New Object
        'Dim mSqlCmd As New Object
        'Dim mSqlTrans As Object

        'If AgL.PubServerName = "" Then
        '    mSqlConn = New SQLiteConnection
        '    mSqlCmd = New SQLiteCommand
        'Else
        '    mSqlConn = New SqlClient.SqlConnection
        '    mSqlCmd = New SqlClient.SqlCommand
        'End If
        'mSqlConn.ConnectionString = AgL.GCn.ConnectionString
        'mSqlConn.Open()
        'mSqlCmd.Connection = mSqlConn
        'mSqlTrans = mSqlConn.BeginTransaction()
        'mSqlCmd.Transaction = mSqlTrans

        Try
            FImportDataFromSqliteTable("Item", "H.Code = H_Temp.Code", "Code", Connection, AgL.GCn, AgL.ECmd, mDbPath)

            mQry = " Select H.*
                    From StockHead H "
            Dim DtHeaderSource As DataTable = AgL.FillData(mQry, Connection).Tables(0)

            mQry = " SELECT H.V_Type, H.ManualRefNo, I.Description As ItemDesc, 
                L.*
                FROM StockHead H 
                LEFT JOIN StockHeadDetail L ON H.DocID = L.DocID
                LEFT JOIN Item I ON L.Item = I.Code "
            Dim DtLineDetailSource As DataTable = AgL.FillData(mQry, Connection).Tables(0)

            mQry = " Select * From PurchInvoice "
            Dim DtPurchInvoice As DataTable = AgL.FillData(mQry, IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).Tables(0)


            For I = 0 To DtHeaderSource.Rows.Count - 1
                If DtPurchInvoice.Select("OMSId = '" & AgL.XNull(DtHeaderSource.Rows(I)("DocId")) & "'").Length = 0 Then
                    Dim Tot_Gross_Amount As Double = 0
                    Dim Tot_Taxable_Amount As Double = 0
                    Dim Tot_Tax1 As Double = 0
                    Dim Tot_Tax2 As Double = 0
                    Dim Tot_Tax3 As Double = 0
                    Dim Tot_Tax4 As Double = 0
                    Dim Tot_Tax5 As Double = 0
                    Dim Tot_SubTotal1 As Double = 0


                    Dim PurchInvoiceTableList(0) As FrmPurchInvoiceDirect.StructPurchInvoice
                    Dim PurchInvoiceTable As New FrmPurchInvoiceDirect.StructPurchInvoice

                    PurchInvoiceTable.DocID = ""
                    PurchInvoiceTable.V_Type = "PI"
                    PurchInvoiceTable.V_Prefix = AgL.XNull(DtHeaderSource.Rows(I)("V_Prefix"))
                    PurchInvoiceTable.Site_Code = AgL.XNull(DtHeaderSource.Rows(I)("Site_Code"))
                    PurchInvoiceTable.Div_Code = AgL.XNull(DtHeaderSource.Rows(I)("Div_Code"))
                    PurchInvoiceTable.V_No = 0
                    PurchInvoiceTable.V_Date = AgL.XNull(DtHeaderSource.Rows(I)("V_Date"))
                    PurchInvoiceTable.ManualRefNo = AgL.XNull(DtHeaderSource.Rows(I)("ManualRefNo"))
                    PurchInvoiceTable.Vendor = "D"
                    PurchInvoiceTable.VendorName = ""
                    PurchInvoiceTable.AgentCode = ""
                    PurchInvoiceTable.AgentName = ""
                    PurchInvoiceTable.BillToPartyCode = "D"
                    PurchInvoiceTable.BillToPartyName = ""
                    PurchInvoiceTable.VendorAddress = ""
                    PurchInvoiceTable.VendorCity = ""
                    PurchInvoiceTable.VendorMobile = ""
                    PurchInvoiceTable.VendorSalesTaxNo = ""
                    PurchInvoiceTable.SalesTaxGroupParty =
                    PurchInvoiceTable.PlaceOfSupply = ""
                    PurchInvoiceTable.StructureCode = ""
                    PurchInvoiceTable.CustomFields = ""
                    PurchInvoiceTable.VendorDocNo = AgL.XNull(DtHeaderSource.Rows(I)("ManualRefNo"))
                    PurchInvoiceTable.VendorDocDate = AgL.XNull(DtHeaderSource.Rows(I)("V_Date"))
                    PurchInvoiceTable.ReferenceDocId = ""
                    PurchInvoiceTable.Tags = ""
                    PurchInvoiceTable.Remarks = AgL.XNull(DtHeaderSource.Rows(I)("Remarks"))
                    PurchInvoiceTable.Status = "Active"
                    PurchInvoiceTable.EntryBy = AgL.PubUserName
                    PurchInvoiceTable.EntryDate = AgL.GetDateTime(AgL.GcnRead)
                    PurchInvoiceTable.ApproveBy = ""
                    PurchInvoiceTable.ApproveDate = ""
                    PurchInvoiceTable.MoveToLog = ""
                    PurchInvoiceTable.MoveToLogDate = ""
                    PurchInvoiceTable.UploadDate = ""
                    PurchInvoiceTable.OmsId = AgL.XNull(DtHeaderSource.Rows(I)("DocId"))
                    PurchInvoiceTable.LockText = "Synced From Other Database."

                    PurchInvoiceTable.Gross_Amount = AgL.VNull(DtHeaderSource.Rows(I)("Gross_Amount"))
                    PurchInvoiceTable.Taxable_Amount = AgL.VNull(DtHeaderSource.Rows(I)("Taxable_Amount"))
                    PurchInvoiceTable.Tax1 = AgL.VNull(DtHeaderSource.Rows(I)("Tax1"))
                    PurchInvoiceTable.Tax2 = AgL.VNull(DtHeaderSource.Rows(I)("Tax2"))
                    PurchInvoiceTable.Tax3 = AgL.VNull(DtHeaderSource.Rows(I)("Tax3"))
                    PurchInvoiceTable.Tax4 = AgL.VNull(DtHeaderSource.Rows(I)("Tax4"))
                    PurchInvoiceTable.Tax5 = AgL.VNull(DtHeaderSource.Rows(I)("Tax5"))
                    PurchInvoiceTable.SubTotal1 = AgL.VNull(DtHeaderSource.Rows(I)("SubTotal1"))
                    PurchInvoiceTable.Other_Charge = AgL.VNull(DtHeaderSource.Rows(I)("Other_Charge"))
                    PurchInvoiceTable.Deduction = AgL.VNull(DtHeaderSource.Rows(I)("Deduction"))
                    PurchInvoiceTable.Round_Off = AgL.VNull(DtHeaderSource.Rows(I)("Round_Off"))
                    PurchInvoiceTable.Net_Amount = AgL.VNull(DtHeaderSource.Rows(I)("Net_Amount"))

                    Dim DtPurchInvoiceDetail_ForHeader As New DataTable
                    For M As Integer = 0 To DtLineDetailSource.Columns.Count - 1
                        Dim DColumn As New DataColumn
                        DColumn.ColumnName = DtLineDetailSource.Columns(M).ColumnName
                        DtPurchInvoiceDetail_ForHeader.Columns.Add(DColumn)
                    Next

                    Dim DtRowPurchInvoiceDetail_ForHeader As DataRow() = DtLineDetailSource.Select("DocId = " + AgL.Chk_Text(AgL.XNull(DtHeaderSource.Rows(I)("DocId"))))
                    If DtRowPurchInvoiceDetail_ForHeader.Length > 0 Then
                        For M As Integer = 0 To DtRowPurchInvoiceDetail_ForHeader.Length - 1
                            DtPurchInvoiceDetail_ForHeader.Rows.Add()
                            For N As Integer = 0 To DtPurchInvoiceDetail_ForHeader.Columns.Count - 1
                                DtPurchInvoiceDetail_ForHeader.Rows(M)(N) = DtRowPurchInvoiceDetail_ForHeader(M)(N)
                            Next
                        Next
                    End If


                    For J = 0 To DtPurchInvoiceDetail_ForHeader.Rows.Count - 1
                        PurchInvoiceTable.Line_Sr = AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Sr"))
                        PurchInvoiceTable.Line_ItemCode = AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Item"))
                        PurchInvoiceTable.Line_ItemName = AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("ItemDesc"))
                        PurchInvoiceTable.Line_Specification = AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Specification"))
                        PurchInvoiceTable.Line_SalesTaxGroupItem = AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("SalesTaxGroupItem"))
                        PurchInvoiceTable.Line_ReferenceNo = AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("ReferenceNo"))
                        PurchInvoiceTable.Line_DocQty = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("DocQty"))
                        PurchInvoiceTable.Line_FreeQty = 0
                        PurchInvoiceTable.Line_Qty = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Qty"))
                        PurchInvoiceTable.Line_Unit = AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Unit"))
                        PurchInvoiceTable.Line_Pcs = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Pcs"))
                        PurchInvoiceTable.Line_UnitMultiplier = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("UnitMultiplier"))
                        PurchInvoiceTable.Line_DealUnit = AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("DealUnit"))
                        PurchInvoiceTable.Line_DocDealQty = AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("DealQty"))

                        PurchInvoiceTable.Line_OmsId = AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("DocId")) + AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Sr"))
                        PurchInvoiceTable.Line_Rate = AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Rate"))
                        PurchInvoiceTable.Line_DiscountPer = 0
                        PurchInvoiceTable.Line_DiscountAmount = 0
                        PurchInvoiceTable.Line_AdditionalDiscountPer = 0
                        PurchInvoiceTable.Line_AdditionalDiscountAmount = 0
                        PurchInvoiceTable.Line_Amount = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Amount"))
                        PurchInvoiceTable.Line_Remark = AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Remark"))
                        PurchInvoiceTable.Line_BaleNo = AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("BaleNo"))
                        PurchInvoiceTable.Line_LotNo = AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("LotNo"))
                        PurchInvoiceTable.Line_ReferenceDocId = ""
                        PurchInvoiceTable.Line_GrossWeight = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("GrossWeight"))
                        PurchInvoiceTable.Line_NetWeight = 0
                        PurchInvoiceTable.Line_Gross_Amount = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Amount"))
                        PurchInvoiceTable.Line_Taxable_Amount = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Amount"))
                        PurchInvoiceTable.Line_Tax1_Per = 0
                        PurchInvoiceTable.Line_Tax1 = 0
                        PurchInvoiceTable.Line_Tax2_Per = 0
                        PurchInvoiceTable.Line_Tax2 = 0
                        PurchInvoiceTable.Line_Tax3_Per = 0
                        PurchInvoiceTable.Line_Tax3 = 0
                        PurchInvoiceTable.Line_Tax4_Per = 0
                        PurchInvoiceTable.Line_Tax4 = 0
                        PurchInvoiceTable.Line_Tax5_Per = 0
                        PurchInvoiceTable.Line_Tax5 = 0
                        PurchInvoiceTable.Line_SubTotal1 = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Amount"))
                        PurchInvoiceTable.Line_Other_Charge = 0
                        PurchInvoiceTable.Line_Deduction = 0
                        PurchInvoiceTable.Line_Round_Off = 0
                        PurchInvoiceTable.Line_Net_Amount = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Amount"))

                        'For Header Values
                        Tot_Gross_Amount += PurchInvoiceTable.Line_Gross_Amount
                        Tot_Taxable_Amount += PurchInvoiceTable.Line_Taxable_Amount
                        Tot_Tax1 += PurchInvoiceTable.Line_Tax1
                        Tot_Tax2 += PurchInvoiceTable.Line_Tax2
                        Tot_Tax3 += PurchInvoiceTable.Line_Tax3
                        Tot_Tax4 += PurchInvoiceTable.Line_Tax4
                        Tot_Tax5 += PurchInvoiceTable.Line_Tax5
                        Tot_SubTotal1 += PurchInvoiceTable.Line_SubTotal1


                        PurchInvoiceTableList(UBound(PurchInvoiceTableList)) = PurchInvoiceTable
                        ReDim Preserve PurchInvoiceTableList(UBound(PurchInvoiceTableList) + 1)
                    Next

                    PurchInvoiceTableList(0).Gross_Amount = Tot_Gross_Amount
                    PurchInvoiceTableList(0).Taxable_Amount = Tot_Taxable_Amount
                    PurchInvoiceTableList(0).Tax1 = Tot_Tax1
                    PurchInvoiceTableList(0).Tax2 = Tot_Tax2
                    PurchInvoiceTableList(0).Tax3 = Tot_Tax3
                    PurchInvoiceTableList(0).Tax4 = Tot_Tax4
                    PurchInvoiceTableList(0).Tax5 = Tot_Tax5
                    PurchInvoiceTableList(0).SubTotal1 = Tot_SubTotal1
                    PurchInvoiceTableList(0).Other_Charge = 0
                    PurchInvoiceTableList(0).Deduction = 0
                    PurchInvoiceTableList(0).Round_Off = Math.Round(Math.Round(PurchInvoiceTableList(0).SubTotal1) - PurchInvoiceTableList(0).SubTotal1, 2)
                    PurchInvoiceTableList(0).Net_Amount = Math.Round(PurchInvoiceTableList(0).SubTotal1)


                    FrmPurchInvoiceDirect.InsertPurchInvoice(PurchInvoiceTableList)
                End If
            Next


            AgL.ETrans.Commit()
            mTrans = "Commit"
            Connection.Close()
            MsgBox("Process Completed Succesfully.", MsgBoxStyle.Information)
        Catch ex As Exception
            AgL.ETrans.Rollback()
            Connection.Close()
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub








    Public Sub ProcStockValuationReportForBank()
        Try
            Dim bTableName$ = ""
            Dim mCondStr$ = ""
            Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"
            Dim IsLastPurchaseRateUpdated As Boolean = False

            Dim rowFromDate As Integer = 0
            Dim rowToDate As Integer = 1
            Dim rowLocationType As Integer = 2
            Dim rowValuation As Integer = 3
            Dim rowValuationPercentage As Integer = 4
            Dim rowSite As Integer = 5
            Dim rowDivision As Integer = 6

            RepTitle = "Stock Valuation Report For Bank"


            If ReportFrm.FGetText(rowValuation) = "Last Purchase Rate" And IsLastPurchaseRateUpdated = False Then
                mQry = "UPDATE Item SET LastPurchaseRate = 0 "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

                mQry = "UPDATE Item SET LastPurchaseRate = (SELECT " & IIf(AgL.PubServerName = "", "", "Top 1") & " L.Taxable_Amount / (CASE WHEN L.Qty > 0 THEN L.Qty ELSE 1 End)  FROM PurchInvoiceDetail L LEFT JOIN PurchInvoice H ON L.DocID = H.DocID 
                        LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                        WHERE L.Item = Item.Code And L.Qty > 0 And L.Rate > 0
                        And Vt.NCat In ('" & Ncat.PurchaseInvoice & "','" & Ncat.OpeningStock & "')
                        ORDER BY H.V_Date DESC " & IIf(AgL.PubServerName = "", "Limit 1", "") & ") Where LastPurchaseRate = 0 "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

                mQry = "UPDATE Item SET LastPurchaseRate = (
	                            SELECT " & IIf(AgL.PubServerName = "", "", "Top 1") & " Case When IfNull(L.Taxable_Amount,0) = 0 Then L.Amount Else L.Taxable_Amount End / (CASE WHEN L.Qty > 0 THEN L.Qty ELSE 1 End)  
	                            FROM PurchInvoiceDimensionDetail Pdl
	                            LEFT JOIN PurchInvoiceDetail L ON Pdl.DocID = L.DocID AND Pdl.TSr = L.Sr
	                            LEFT JOIN PurchInvoice H ON L.DocID = H.DocID
                                LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type  
	                            WHERE Pdl.Item = Item.Code 
                                And Vt.NCat In ('" & Ncat.PurchaseInvoice & "','" & Ncat.OpeningStock & "')
	                            And L.Qty > 0 
	                            AND Pdl.Item IS NOT NULL
	                            ORDER BY H.V_Date DESC  " & IIf(AgL.PubServerName = "", "Limit 1", "") & "
                            ) Where IsNull(LastPurchaseRate,0) = 0 "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

                mQry = "UPDATE Item SET LastPurchaseRate = (SELECT " & IIf(AgL.PubServerName = "", "", "Top 1") & " Round(L.Taxable_Amount / (CASE WHEN L.Qty > 0 THEN L.Qty * Uc.Multiplier ELSE 1 End),2)  
                        FROM Item I 
                        LEFT JOIN PurchInvoiceDetail L ON I.Code = L.Item
                        LEFT JOIN PurchInvoice H ON L.DocID = H.DocID
                        LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type
                        LEFT JOIN UnitConversion Uc ON L.Item = Uc.Item And L.Unit = Uc.FromUnit AND I.StockUnit = Uc.ToUnit
                        WHERE L.Item = Item.Code 
                        And Vt.NCat In ('" & Ncat.PurchaseInvoice & "','" & Ncat.OpeningStock & "')
                        AND I.StockUnit IS NOT NULL 
                        AND I.Unit <> I.StockUnit
                        ORDER BY H.V_Date DESC  " & IIf(AgL.PubServerName = "", "Limit 1", "") & ")
                        WHERE Code IN (SELECT I.Code FROM Item I WHERE I.StockUnit IS NOT NULL AND I.Unit <> I.StockUnit)"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)


                If ClsMain.FDivisionNameForCustomization(14) = "PRATHAM APPARE" Or
                        ClsMain.FDivisionNameForCustomization(15) = "AGARWAL UNIFORM" Then
                    mQry = "UPDATE Item
                                SET Item.LastPurchaseRate = V1.LastPurchaseRate_New
                                FROM (
	                                SELECT I.Code, I1.LastPurchaseRate AS LastPurchaseRate_New
	                                FROM Item I 
	                                LEFT JOIN Item I1 ON IsNull(I.ItemCategory,'') = IsNull(I1.ItemCategory,'') 
			                                AND IsNull(I.ItemGroup,'') = IsNull(I1.ItemGroup,'') 
			                                AND IsNull(I.BaseItem,'') = IsNull(I1.BaseItem,'') 
			                                AND IsNull(I.Dimension1,'') = IsNull(I1.Dimension1,'') 
			                                AND IsNull(I.Dimension2,'') = IsNull(I1.Dimension2,'') 
			                                AND IsNull(I.Dimension4,'') = IsNull(I1.Dimension4,'') 
			                                AND IsNull(I.Size,'') = IsNull(I1.Size,'') 
			                                AND IsNull(I.Code,'') <> IsNull(I1.Code,'') 
	                                WHERE IsNull(I.LastPurchaseRate,0) = 0
	                                AND I.Dimension3 IS NOT NULL
	                                AND I1.Code IS NOT NULL
	                                AND IsNull(I1.LastPurchaseRate,0) <> 0
                                ) AS V1 WHERE Item.Code = V1.Code "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
                End If
                IsLastPurchaseRateUpdated = True
            End If



            Dim bStockTable As String = " Select DocId, TSr, Sr, V_Date, V_Type, Site_Code, Div_Code, RecId, SubCode, Godown, Process, LotNo, Item, Qty_Iss, Qty_Rec, Unit, Rate From Stock "
            Dim bStockProcessTable As String = " Select DocId, TSr, Sr, V_Date, V_Type, Site_Code, Div_Code, RecId, SubCode, SubCode As Godown, Process, LotNo, Item, Qty_Iss, Qty_Rec, Unit, Rate From StockProcess "
            Dim bCombinedTable As String = "(" & bStockTable & " UNION ALL " & bStockProcessTable & ")"
            bStockTable = " (" + bStockTable + ") "
            bStockProcessTable = " (" + bStockProcessTable + ") "


            If ReportFrm.FGetText(rowLocationType) = "At Person" Then
                bTableName = bStockProcessTable
            ElseIf ReportFrm.FGetText(rowLocationType) = "Both" Then
                bTableName = bCombinedTable
            Else
                bTableName = bStockTable
            End If

            mCondStr = "  "
            mCondStr = mCondStr & "  "
            mCondStr = mCondStr & "And Sku.ItemType Not In ('" & ItemTypeCode.ServiceProduct & "','" & ItemTypeCode.InternalProduct & "')"



            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("L.Site_Code", rowSite), "''", "'")
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("L.Div_Code", rowDivision), "''", "'")


            Dim bTempTableName As String = "[" + Guid.NewGuid().ToString() + "]"

            If AgL.VNull(AgL.Dman_Execute("SELECT Count(Bd.Code) As Cnt
                            FROM BOMDetail Bd
                            LEFT JOIN Item I ON Bd.Code = I.Code
                            LEFT JOIN Item Bi ON I.BaseItem = Bi.Code
                            LEFT JOIN Item Ci ON Bd.Item = Ci.Code
                            WHERE Bi.ItemType = Ci.ItemType", AgL.GCn).ExecuteScalar()) > 0 Then

                If AgL.IsTableExist(bTempTableName.Replace("[", "").Replace("]", ""), AgL.GCn) Then
                    mQry = "Drop Table " + bTempTableName
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
                End If

                mQry = " CREATE TABLE " & bTempTableName & "(DocID NVARCHAR (21), TSr INT
                        , Sr INT, V_Type NVARCHAR (5), V_Prefix NVARCHAR (5), V_Date DATETIME
                        , V_No BIGINT, Div_Code NVARCHAR (1), Site_Code NVARCHAR (2), SubCode NVARCHAR (10)
                        , LotNo NVARCHAR (20), Godown NVARCHAR (10), Item WVARCHAR (255), Qty_Iss DOUBLE
                        , Qty_Rec DOUBLE, Unit NVARCHAR (10), UnitMultiplier FLOAT, Rate FLOAT, RecId VARCHAR (20)) "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

                mQry = "INSERT INTO " & bTempTableName & "(DocID, TSr, Sr, V_Type, V_Prefix, V_Date, V_No, Div_Code, 
                        Site_Code, SubCode, LotNo, Godown, Item, Qty_Iss, Qty_Rec, Unit, UnitMultiplier, Rate, RecId)
                        SELECT L.DocID, L.TSr, L.Sr, L.V_Type, L.V_Prefix, L.V_Date, L.V_No, L.Div_Code, L.Site_Code, L.SubCode, 
                        L.LotNo, L.Godown, 
                        CASE WHEN V1.Item IS NOT NULL THEN V1.Item ELSE L.Item END AS Item, 
                        CASE WHEN V1.Item IS NOT NULL THEN V1.Qty * L.Qty_Iss ELSE L.Qty_Iss END AS Qty_Iss, 
                        CASE WHEN V1.Item IS NOT NULL THEN V1.Qty * L.Qty_Rec ELSE L.Qty_Rec END AS Qty_Rec, 
                        L.Unit, L.UnitMultiplier, L.Rate, L.RecId
                        FROM " & bTableName & " L 
                        LEFT JOIN Item I ON L.Item = I.Code
                        LEFT JOIN (
	                        SELECT I.BaseItem AS Code, Bd.Item, Bd.Qty
	                        FROM BOMDetail Bd
	                        LEFT JOIN Item I ON Bd.Code = I.Code
                        ) AS V1 ON I.Code = V1.Code "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

                bTableName = bTempTableName
            End If

            Dim mMainQry As String = ""
            mMainQry = " SELECT ' Opening' as DocID, ' Opening' V_Type, ' 0' as RecId, strftime('%d/%m/%Y', " & AgL.Chk_Date(ReportFrm.FGetText(rowFromDate)) & ")  V_Date, " & AgL.Chk_Date(ReportFrm.FGetText(rowFromDate)) & "  V_Date_ActualFormat
                    , Null as PartyName, Max(Location.Name) as LocationName
                    , Sku.Code AS SkuCode, Max(Sku.Description) AS SkuName 
                    , Max(Sku.Specification) as SkuSpecification
                    , Max(IG.Code) as ItemGroupCode, Max(IG.Description) as ItemGroupName
                    , Max(IC.Code) as ItemCategoryCode, Max(IC.Description) as ItemCategoryName 
                    , Max(IT.Code) as ItemTypeCode, Max(IT.Name) as ItemType
                    , Max(Case When Sku.V_Type = '" & ItemV_Type.SKU & "' Then I.Code Else Sku.Code End) as ItemCode
                    , Max(Case When Sku.V_Type = '" & ItemV_Type.SKU & "' Then I.Specification Else IfNull(Sku.Specification, Sku.Description) End) as ItemName 
                    , Max(D1.Code) as Dimension1Code, Max(D1.Specification) as Dimension1Name 
                    , Max(D2.Code) as Dimension2Code, Max(D2.Specification) as Dimension2Name 
                    , Max(D3.Code) as Dimension3Code, Max(D3.Specification) as Dimension3Name 
                    , Max(D4.Code) as Dimension4Code, Max(D4.Specification) as Dimension4Name 
                    , Max(Size.Code) as SizeCode, Max(Size.Description) as SizeName
                    , Max(IfNull(Sku.HSN, IC.HSN)) as HSN, Max(L.LotNo) as LotNo
                    , Max(Prc.SubCode) as ProcessCode, Max(Prc.Name) as ProcessName, 
                    Max(IfNull(Sku.StockUnit, L.Unit)) as Unit, Max(U.DecimalPlaces) as DecimalPlaces, 
                    Sum(Case When Sku.StockUnit Is Not Null And Sku.StockUnit <> L.Unit Then L.Qty_Rec * Uc.Multiplier Else L.Qty_Rec End - 
                        Case When Sku.StockUnit Is Not Null And Sku.StockUnit <> L.Unit Then L.Qty_Iss * Uc.Multiplier Else L.Qty_Iss End) AS Opening, 
                    0 AS Qty_Rec, 
                    0 AS Qty_Iss, 
                    Sum(Case When Sku.StockUnit Is Not Null And Sku.StockUnit <> L.Unit Then L.Qty_Rec * Uc.Multiplier Else L.Qty_Rec End - 
                        Case When Sku.StockUnit Is Not Null And Sku.StockUnit <> L.Unit Then L.Qty_Iss * Uc.Multiplier Else L.Qty_Iss End) AS Closing, 
                    0 as TransactionRate, "


            If ReportFrm.FGetText(rowValuation) = "Master Purchase Rate" Then
                mMainQry = mMainQry & " Max(Sku.PurchaseRate) " & IIf(AgL.VNull(ReportFrm.FGetText(rowValuationPercentage)) <> 0, " * " & AgL.VNull(ReportFrm.FGetText(rowValuationPercentage)) & "/100", "") & " as ValuationRate, 
                    IfNull(Sum((Case When Sku.StockUnit Is Not Null And Sku.StockUnit <> L.Unit Then L.Qty_Rec * Uc.Multiplier Else L.Qty_Rec End - 
                    Case When Sku.StockUnit Is Not Null And Sku.StockUnit <> L.Unit Then L.Qty_Iss * Uc.Multiplier Else L.Qty_Iss End)*(Sku.PurchaseRate)),0) 
                    " & IIf(AgL.VNull(ReportFrm.FGetText(rowValuationPercentage)) <> 0, " * " & AgL.VNull(ReportFrm.FGetText(rowValuationPercentage)) & "/100", "") & " as Amount "
            ElseIf ReportFrm.FGetText(rowValuation) = "Last Purchase Rate" Then
                mMainQry = mMainQry & " (Case When Max(RList.Code) Is Not Null Then Max(RList.Cost) Else 
                    Max(Sku.LastPurchaseRate) End) " & IIf(AgL.VNull(ReportFrm.FGetText(rowValuationPercentage)) <> 0, " * " & AgL.VNull(ReportFrm.FGetText(rowValuationPercentage)) & "/100", "") & " as ValuationRate, 
                    IfNull(Sum((Case When Sku.StockUnit Is Not Null And Sku.StockUnit <> L.Unit Then L.Qty_Rec * Uc.Multiplier Else L.Qty_Rec End - 
                    Case When Sku.StockUnit Is Not Null And Sku.StockUnit <> L.Unit Then L.Qty_Iss * Uc.Multiplier Else L.Qty_Iss End)*(Case When RList.Code Is Not Null Then RList.Cost Else Sku.LastPurchaseRate End)),0) 
                    " & IIf(AgL.VNull(ReportFrm.FGetText(rowValuationPercentage)) <> 0, " * " & AgL.VNull(ReportFrm.FGetText(rowValuationPercentage)) & "/100", "") & " as Amount "
            Else
                mMainQry = mMainQry & " 0 as ValuationRate, 0 as Amount "
            End If
            mMainQry = mMainQry & " FROM " & bTableName & " L
                    LEFT JOIN Item Sku ON L.Item = Sku.Code
                    Left Join Item IG On Sku.ItemGroup = IG.Code
                    Left Join Item IC On IfNull(Sku.ItemCategory,Sku.code) = IC.Code
                    LEFT JOIN Item I ON IfNull(Sku.BaseItem, Sku.Code) = I.Code
                    LEFT JOIN Item D1 ON Sku.Dimension1 = D1.Code
                    LEFT JOIN Item D2 ON Sku.Dimension2 = D2.Code
                    LEFT JOIN Item D3 ON Sku.Dimension3 = D3.Code
                    LEFT JOIN Item D4 ON Sku.Dimension4 = D4.Code
                    LEFT JOIN Item Size ON Sku.Size = Size.Code
                    Left Join ItemType It On Sku.ItemType = It.Code
                    LEFT JOIN SubGroup Prc On L.Process = Prc.SubCode
                    Left Join Unit U On L.Unit = U.Code
                    LEFT JOIN Unit Su On Sku.StockUnit = Su.Code 
                    LEFT JOIN UnitConversion Uc On L.Item = Uc.Item And L.Unit = Uc.FromUnit And Sku.StockUnit = Uc.ToUnit
                    Left Join viewHelpSubgroup Sg On L.Subcode = Sg.Code
                    LEFT JOIN Voucher_type vt ON L.V_Type = vt.V_Type 
                    Left Join Subgroup Location On L.Godown = Location.Subcode "
            mMainQry += " Left Join (
                                     Select Max(RLD.Code) as Code, RLD.ItemCategory, RLD.Dimension1, RLD.Size, Max(RLD.Cost) as Cost 
                                     From RateListDetail RLD 
                                     Left Join RateList RL On RLD.Code = RL.Code 
                                     Where RLD.Process='PSales' 
                                     And RL.RateCategory is Null
                                     Group By RLD.ItemCategory, RLD.Dimension1, RLD.Size
                                     ) as RList On IC.Code = RList.ItemCategory and D1.Code = RList.Dimension1 and size.Code = RList.Size "
            mMainQry += " WHERE L.V_Date < " & AgL.Chk_Date(ReportFrm.FGetText(rowFromDate)) & " " & mCondStr & "
                    GROUP BY Sku.Code , L.Godown
                    Union All
                    SELECT L.DocID, L.V_Type, L.RecId, 
                    strftime('%d/%m/%Y', L.V_Date) As V_Date, L.V_Date As V_Date_ActualFormat
                    , Sg.Name as PartyName, Location.Name as LocationName
                    , Sku.Code AS SkuCode, Sku.Description AS SkuName
                    , Sku.Specification as SkuSpecification
                    , IG.Code as ItemGroupCode, IG.Description as ItemGroupName
                    , IC.Code as ItemCategoryCode, IC.Description as ItemCategoryName 
                    , IT.Code as ItemTypeCode, IT.Name as ItemType
                    , Case When Sku.V_Type = '" & ItemV_Type.SKU & "' Then I.Code Else Sku.Code End as ItemCode
                    , Case When Sku.V_Type = '" & ItemV_Type.SKU & "' Then I.Specification Else IfNull(Sku.Specification,Sku.Description) End as ItemName 
                    , D1.Code as Dimension1Code, D1.Specification as Dimension1Name 
                    , D2.Code as Dimension2Code, D2.Specification as Dimension2Name 
                    , D3.Code as Dimension3Code, D3.Specification as Dimension3Name 
                    , D4.Code as Dimension4Code, D4.Specification as Dimension4Name 
                    , Size.Code as SizeCode, Size.Description as SizeName
                    , IfNull(Sku.HSN, IC.HSN) as HSN, L.LotNo as LotNo
                    , Prc.SubCode as ProcessCode, Prc.Name as ProcessName, 
                    IfNull(Sku.StockUnit, L.Unit) As Unit, U.DecimalPlaces, 
                    0 AS Opening,
                    Case When Sku.StockUnit Is Not Null And Sku.StockUnit <> L.Unit Then L.Qty_Rec * Uc.Multiplier Else L.Qty_Rec End AS Qty_Rec, 
                    Case When Sku.StockUnit Is Not Null And Sku.StockUnit <> L.Unit Then L.Qty_Iss * Uc.Multiplier Else L.Qty_Iss End As Qty_Iss, 
                    Case When Sku.StockUnit Is Not Null And Sku.StockUnit <> L.Unit Then L.Qty_Rec * Uc.Multiplier Else L.Qty_Rec End - 
                    Case When Sku.StockUnit Is Not Null And Sku.StockUnit <> L.Unit Then L.Qty_Iss * Uc.Multiplier Else L.Qty_Iss End AS Closing, 
                    L.Rate as TransactionRate, "

            If ReportFrm.FGetText(rowValuation) = "Master Purchase Rate" Then
                mMainQry = mMainQry & " Sku.PurchaseRate " & IIf(AgL.VNull(ReportFrm.FGetText(rowValuationPercentage)) <> 0, " * " & AgL.VNull(ReportFrm.FGetText(rowValuationPercentage)) & "/100", "") & " as ValuationRate, 
                    IfNull((Case When Sku.StockUnit Is Not Null And Sku.StockUnit <> L.Unit Then L.Qty_Rec * Uc.Multiplier Else L.Qty_Rec End - 
                    Case When Sku.StockUnit Is Not Null And Sku.StockUnit <> L.Unit Then L.Qty_Iss * Uc.Multiplier Else L.Qty_Iss End)*(Sku.PurchaseRate),0) " & IIf(AgL.VNull(ReportFrm.FGetText(rowValuationPercentage)) <> 0, " * " & AgL.VNull(ReportFrm.FGetText(rowValuationPercentage)) & "/100", "") & " as Amount "
            ElseIf ReportFrm.FGetText(rowValuation) = "Last Purchase Rate" Then
                mMainQry = mMainQry & " (Case When RList.Code Is Not Null Then RList.Cost Else 
                    Sku.LastPurchaseRate End) " & IIf(AgL.VNull(ReportFrm.FGetText(rowValuationPercentage)) <> 0, " * " & AgL.VNull(ReportFrm.FGetText(rowValuationPercentage)) & "/100", "") & " as ValuationRate, 
                    IfNull((Case When Sku.StockUnit Is Not Null And Sku.StockUnit <> L.Unit Then L.Qty_Rec * Uc.Multiplier Else L.Qty_Rec End - 
                    Case When Sku.StockUnit Is Not Null And Sku.StockUnit <> L.Unit Then L.Qty_Iss * Uc.Multiplier Else L.Qty_Iss End)*(Case When RList.Code Is Not Null Then RList.Cost Else Sku.LastPurchaseRate End),0) 
                    " & IIf(AgL.VNull(ReportFrm.FGetText(rowValuationPercentage)) <> 0, " * " & AgL.VNull(ReportFrm.FGetText(rowValuationPercentage)) & "/100", "") & " as Amount "
            Else
                mMainQry = mMainQry & " 0 as ValuationRate, 0 as Amount "
            End If

            mMainQry = mMainQry & " FROM " & bTableName & " L
                    LEFT JOIN Item Sku ON L.Item = Sku.Code
                    Left Join Item IG On Sku.ItemGroup = IG.Code
                    Left Join Item IC On IfNull(Sku.ItemCategory,Sku.code) = IC.Code
                    LEFT JOIN Item I ON IfNull(Sku.BaseItem, Sku.Code) = I.Code
                    LEFT JOIN Item D1 ON Sku.Dimension1 = D1.Code
                    LEFT JOIN Item D2 ON Sku.Dimension2 = D2.Code
                    LEFT JOIN Item D3 ON Sku.Dimension3 = D3.Code
                    LEFT JOIN Item D4 ON Sku.Dimension4 = D4.Code
                    LEFT JOIN Item Size ON Sku.Size = Size.Code
                    Left Join ItemType It On Sku.ItemType = It.Code
                    LEFT JOIN SubGroup Prc On L.Process = Prc.SubCode
                    Left Join Unit U On L.Unit = U.Code
                    LEFT JOIN Unit Su On Sku.StockUnit = Su.Code 
                    LEFT JOIN UnitConversion Uc On L.Item = Uc.Item And L.Unit = Uc.FromUnit And Sku.StockUnit = Uc.ToUnit
                    Left Join viewHelpSubgroup Sg on L.Subcode = Sg.Code
                    LEFT JOIN Voucher_type vt ON L.V_Type = vt.V_Type 
                    Left Join Subgroup Location On L.Godown = Location.Subcode
                    "
            mMainQry += " Left Join (
                                     Select Max(RLD.Code) as Code, RLD.ItemCategory, RLD.Dimension1, RLD.Size, Max(RLD.Cost) as Cost 
                                     From RateListDetail RLD 
                                     Left Join RateList RL On RLD.Code = RL.Code 
                                     Where RLD.Process='PSales' 
                                     And RL.RateCategory is Null
                                     Group By RLD.ItemCategory, RLD.Dimension1, RLD.Size
                                     ) as RList On IC.Code = RList.ItemCategory and D1.Code = RList.Dimension1 and size.Code = RList.Size "
            mMainQry = mMainQry & "WHERE Date(L.V_Date) >= " & AgL.Chk_Date(ReportFrm.FGetText(rowFromDate)) & " And Date(L.V_Date) <= " & AgL.Chk_Date(ReportFrm.FGetText(rowToDate)) & " " & mCondStr & "  "

            Dim bGroupOn As String = ""
            bGroupOn = "ItemType"

            mQry = " Select Max(VMain.SkuCode) As SearchCode, VMain.ItemType,"
            mQry += " Round(Sum(VMain.Opening * IfNull(ValuationRate,0)),2) As [Opening], 
                    Round(Sum(VMain.Qty_Rec * IfNull(ValuationRate,0)),2) as [Receive], 
                    Round(Sum(VMain.Qty_Iss * IfNull(ValuationRate,0)),2) as [Issue], 
                    Round(Sum(VMain.Closing * IfNull(ValuationRate,0)), 2) as [Closing] 
                    From (" & mMainQry & ") As VMain
                    GROUP By VMain.ItemType "
            mQry += " Order By VMain.ItemType "
            DsRep = AgL.FillData(mQry, AgL.GCn)


            RepName = "StockValuationReportForBank" : RepTitle = "Stock Valuation Report"


            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            ReportFrm.PrintReport(DsRep, RepName, RepTitle)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
End Class
