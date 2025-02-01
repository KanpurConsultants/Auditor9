Imports System.IO
Imports AgLibrary
Imports AgLibrary.ClsMain
Imports AgLibrary.ClsMain.agConstants
Imports Microsoft.Reporting.WinForms

Public Class ClsReports

#Region "Danger Zone"
    Dim StrArr1() As String = Nothing, StrArr2() As String = Nothing, StrArr3() As String = Nothing, StrArr4() As String = Nothing, StrArr5() As String = Nothing

    Dim mGRepFormName As String = ""

    Dim WithEvents ReportFrm As FrmRepDisplay
    Public Const GFilter As Byte = 2
    Public Const GFilterCode As Byte = 4


    Public Const Col1SearchCode As String = "Search Code"

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
    'Private Const StockReport As String = "StockReport"
    Private Const SaleInvoiceReport As String = "SaleInvoiceReport"
    Private Const SaleOrderReport As String = "SaleOrderReport"
    Private Const SaleInvoiceReportAadhat As String = "SaleInvoiceReportAadhat"
    Private Const SaleOrderStatus As String = "SaleOrderStatus"
    Private Const SaleChallanStatus As String = "SaleChallanStatus"
    Private Const PurchaseInvoiceReport As String = "PurchaseInvoiceReport"
    Private Const PurchaseOrderReport As String = "PurchaseOrderReport"
    Private Const DebitCreditNoteReport As String = "DebitCreditNoteReport"
    Private Const ExpenseIncomeReport As String = "ExpenseIncomeReport"
    Private Const RateListReport As String = "RateListReport"
    Private Const SalesAgentCommissionOnPayment As String = "SalesAgentCommissionOnPayment"
    Private Const SalesRepresentativeCommissionOnPayment As String = "SalesRepresentativeCommissionOnPayment"
    Private Const MoneyReceiptReport As String = "MoneyReceiptReport"
    Private Const PackedBalesLocationReport As String = "PackedBaleLocationReport"
    Private Const BaleMovementReport As String = "BaleMovementReport"
    Private Const FsnAnalysis As String = "FSNAnalysis"
    Private Const EWayBillGeneration As String = "EWayBillGeneration"
    Private Const DebtorsOutstandingReport As String = "DebtorsOutstandingReport"
    Private Const CreditorsOutstandingReport As String = "CreditorsOutstandingReport"
    Private Const LRStatusChange As String = "LRStatusChange"
    Private Const LedgerPostingDifference As String = "LedgerPostingDifference"
    Private Const ChequeSearching As String = "ChequeSearching"
    Private Const LogReport As String = "LogReport"
#End Region

#Region "Queries Definition"
    Public Shared mHelpCityQry$ = "Select 'o' As Tick, CityCode, CityName From City "
    Public Shared mHelpStateQry$ = "Select 'o' As Tick, Code, Description From State "
    Public Shared mHelpAreaQry$ = "Select 'o' As Tick, Code, Description From Area "
    Public Shared mHelpAcGroupCustomerQry$ = "Select 'o' As Tick, GroupCode, GroupName From AcGroup Where Nature='Customer' "
    Public Shared mHelpAcGroupSupplierQry$ = "Select 'o' As Tick, GroupCode, GroupName From AcGroup Where Nature='Supplier' "
    Public Shared mHelpUserQry$ = "Select 'o' As Tick, User_Name As Code, User_Name As [User] From UserMast "
    'Dim mHelpSiteQry$ = "Select 'o' As Tick, Code, Name As [Site] From SiteMast Where " & AgL.PubSiteCondition("Code", AgL.PubSiteCode) & " "
    Public Shared mHelpSiteQry$ = "Select 'o' As Tick, Code, Name As [Site] From SiteMast Where Code In (" & AgL.PubSiteList & ") "
    'Dim mHelpDivisionQry$ = "Select 'o' As Tick, Div_Code as Code, Div_Name As [Division] From Division Where Div_Code In ('" & Replace(AgL.PubDivisionList, ",", "','") & "') "
    Public Shared mHelpDivisionQry$ = "Select 'o' As Tick, Div_Code as Code, Div_Name As [Division] From Division Where Div_Code In (" & AgL.PubDivisionList & ") "
    Public Shared mHelpItemQry$ = "Select 'o' As Tick, Code, Description As [Item] From Item Where V_Type = '" & ItemV_Type.Item & "'"
    Public Shared mHelpItemGroupQry$ = "Select 'o' As Tick, Code, Description As [Item Group] From ItemGroup "


    Public Shared mHelpVendorQry$ = " Select 'o' As Tick,  H.SubCode As Code, Sg.DispName AS Vendor FROM Vendor H LEFT JOIN SubGroup Sg ON H.SubCode = Sg.SubCode "
    Public Shared mHelpTableQry$ = "Select 'o' As Tick, H.Code, H.Description AS [Table] FROM HT_Table H "
    Public Shared mHelpPaymentModeQry$ = "Select 'o' As Tick, 'Cash' As Code, 'Cash' As Description " &
                                " UNION ALL " &
                                " Select 'o' As Tick, 'Credit' As Code, 'Credit' As Description "

    Public Shared mHelpPartyTradeTypeQry$ = "Select 'o' As Tick, 'Manufacturers' As Code, 'Manufacturers' As Description " &
                                " UNION ALL " &
                                " Select 'o' As Tick, 'Traders' As Code, 'Traders' As Description "

    Public Shared mHelpOutletQry$ = "Select 'o' As Tick, H.Code, H.Description AS [Table] FROM Outlet H "
    Public Shared mHelpStewardQry$ = "Select 'o' As Tick,  Sg.SubCode AS Code, Sg.DispName AS Steward FROM SubGroup Sg  "
    Public Shared mHelpPartyQry$ = " Select 'o' As Tick,  Sg.SubCode As Code, Sg.DispName || ',' ||  City.CityName AS Party, Sg.Address FROM SubGroup Sg Left Join City On Sg.CityCode = City.CityCode Where Sg.Nature In ('Customer','Supplier','Cash') "
    Public Shared mHelpLocationQry$ = " Select 'o' As Tick,  Sg.Code, Sg.Name AS Party FROM viewHelpSubGroup Sg Where Sg.Nature In ('Supplier','Stock') "
    Public Shared mHelpPartySingleQry$ = " Select Sg.Code, Sg.Name AS Party FROM viewHelpSubGroup Sg Where Sg.Nature In ('Customer','Supplier','Cash') "
    Public Shared mHelpTransporterQry$ = " Select 'o' As Tick, Sg.Code, Sg.Name AS Agent FROM viewHelpSubgroup Sg Where Sg.SubgroupType = '" & SubgroupType.Transporter & "' "
    Public Shared mHelpSalesRepresentativeQry$ = " Select 'o' As Tick, Sg.Code, Sg.Name AS [Sales Representative] FROM viewHelpSubgroup Sg Left Join HRM_Employee E On Sg.Code = E.Subcode LEFT JOIN HRM_Designation D ON E.designation = D.Code Where Sg.SubgroupType = '" & SubgroupType.Employee & "' AND D.Code ='SREP' "
    Public Shared mHelpResponsiblePersonQry$ = " Select 'o' As Tick, Sg.Code, Sg.Name AS Agent FROM viewHelpSubgroup Sg Left Join HRM_Employee E On Sg.Code = E.Subcode LEFT JOIN HRM_Designation D ON E.designation = D.Code Where Sg.SubgroupType = '" & SubgroupType.Employee & "' AND D.Code <>'SREP' "
    Public Shared mHelpSalesAgentQry$ = " Select 'o' As Tick, Sg.Code, Sg.Name AS [Responsible Person] FROM viewHelpSubgroup Sg Where Sg.SubgroupType = '" & SubgroupType.SalesAgent & "' "
    Public Shared mHelpPurchaseAgentQry$ = " Select 'o' As Tick, Sg.Code, Sg.Name AS Agent FROM viewHelpSubgroup Sg Where Sg.SubgroupType = '" & SubgroupType.PurchaseAgent & "' "
    Public Shared mHelpYesNo$ = " Select 'Yes' As Code, 'Yes' AS [Value] Union All Select 'No' As Code, 'No' AS [Value] "
    Public Shared mHelpSaleOrderQry$ = " Select 'o' As Tick,  H.DocID AS Code, H.V_Type || '-' || H.ReferenceNo  FROM SaleOrder H "
    Public Shared mHelpSaleBillQry$ = " SELECT 'o' As Tick,DocId, ReferenceNo AS BillNo, V_Date AS Date FROM SaleChallan "
    Public Shared mHelpItemReportingGroupQry$ = "Select 'o' As Tick,I.Code,I.Description  AS ItemReportingGroup FROM ItemReportingGroup I "
    Public Shared mHelpItemCategoryQry$ = "Select 'o' As Tick, Code, Description As [Item Category] From ItemCategory "
    Public Shared mHelpItemTypeQry$ = "Select 'o' As Tick, Code, Name FROM ItemType "
    Public Shared mHelpItemStateQry$ = "Select 'o' As Tick, Code, Description As [Item Category] From Item Where V_Type = '" & ItemV_Type.ItemState & "' And IfNull(Status,'Active') = 'Active' "
    Public Shared mHelpTagQry$ = "Select 'o' As Tick, H.Code, H.Description   FROM Tag H "
    Public Shared mHelpVoucherTypeQry$ = "SELECT 'o' As Tick, H.V_Type AS Code, H.Description FROM Voucher_Type H  "
    Public Shared mHelpPartyTaxGroup$ = "SELECT 'o' As Tick, H.Description AS Code, H.Description FROM PostingGroupSalesTaxParty H  "
    Public Shared mHelpItemTaxGroup$ = "SELECT 'o' As Tick, H.Description AS Code, H.Description FROM PostingGroupSalesTaxItem H  "
    Public Shared mHelpCatalog$ = "SELECT 'o' As Tick, H.Code, H.Description FROM Catalog H Order By Description "
    Public Shared mHelpDepartment$ = "SELECT 'o' As Tick, H.Code, H.Description FROM Department H Order By Description "
    Public Shared mHelpAccountType$ = "SELECT 'o' As Tick, subgroupType As Code, SubgroupType As Name from subgroup where SubgroupType Is Not Null group by SubgroupType "
    Public Shared mHelpAccountNature$ = "SELECT 'o' As Tick, Nature As Code, Nature As Name from subgroup where Nature Is Not Null group by Nature "


#End Region

    Dim DsHeader As DataSet = Nothing, DsRep1 As DataSet = Nothing, DsRep2 As DataSet = Nothing
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
                Case SaleInvoiceReport, SaleOrderReport
                    mQry = "Select 'Doc.Header Wise Detail' as Code, 'Doc.Header Wise Detail' as Name 
                            Union All Select 'Item Wise Detail' as Code, 'Item Wise Detail' as Name 
                            Union All Select 'Month Wise Summary' as Code, 'Month Wise Summary' as Name 
                            Union All Select 'Party Wise Summary' as Code, 'Party Wise Summary' as Name 
                            Union All Select 'Agent Wise Summary' as Code, 'Agent Wise Summary' as Name 
                            Union All Select 'Voucher Type Wise Summary' as Code, 'Voucher Type Wise Summary' as Name 
                            Union All Select 'HSN Wise Summary' as Code, 'HSN Wise Summary' as Name 
                            Union All Select 'Item Wise Summary' as Code, 'Item Wise Summary' as Name 
                            Union All Select 'Item Group Wise Summary' as Code, 'Item Group Wise Summary' as Name 
                            Union All Select 'Item Category Wise Summary' as Code, 'Item Category Wise Summary' as Name 
                            Union All Select 'City Wise Summary' as Code, 'City Wise Summary' as Name 
                            Union All Select 'Area Wise Summary' as Code, 'Area Wise Summary' as Name 
                            Union All Select 'State Wise Summary' as Code, 'State Wise Summary' as Name
                            Union All Select 'Sales Representative Wise Summary' as Code, 'Sales Representative Wise Summary' as Name
                            Union All Select 'Responsible Person Wise Summary' as Code, 'Responsible Person Wise Summary' as Name
                            Union All Select 'User Wise Summary' as Code, 'User Wise Summary' as Name
                            Union All Select 'Party Tax Group Wise Summary' as Code, 'Party Tax Group Wise Summary' as Name
                            Union All Select 'Item Tax Group Wise Summary' as Code, 'Item Tax Group Wise Summary' as Name
                            Union All Select 'Division Wise Summary' as Code, 'Division Wise Summary' as Name
                            Union All Select 'Site Wise Summary' as Code, 'Site Wise Summary' as Name
                            Union All Select 'Account Wise Summary' as Code, 'Account Wise Summary' as Name
                            Union All Select 'Account Type Wise Summary' as Code, 'Account Type Wise Summary' as Name
                            Union All Select 'Account Nature Wise Summary' as Code, 'Account Nature Wise Summary' as Name
                            Union All Select 'Department Wise Summary' as Code, 'Department Wise Summary' as Name
                            "
                    If ClsMain.FDivisionNameForCustomization(13) = "JAIN BROTHERS" Or ClsMain.FDivisionNameForCustomization(11) = "BOOK SHOPEE" Then
                        mQry = mQry & " Union All Select 'Catalog Wise Summary' as Code, 'Catalog Wise Summary' as Name "
                    End If

                    If AgL.StrCmp(ClsMain.FDivisionNameForCustomization(6), "SADHVI") And AgL.StrCmp(AgL.PubDBName, "SADHVI") Then
                        mQry = mQry & " Union All Select 'All Addition' as Code, 'All Addition' as Name "
                        mQry = mQry & " Union All Select 'Un-Adjusted Addition' as Code, 'Un-Adjusted Addition' as Name "
                    End If

                    ReportFrm.CreateHelpGrid("Report Type", "Report Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mQry, "Month Wise Summary",,, 300)
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubEndDate)
                    ReportFrm.CreateHelpGrid("Party", "Party", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpPartyQry)
                    ReportFrm.CreateHelpGrid("Item", "Item", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpItemQry)
                    ReportFrm.CreateHelpGrid("Site", "Site", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSiteQry, "[SITECODE]")
                    If GRepFormName = SaleOrderReport Then
                        ReportFrm.CreateHelpGrid("VoucherType", "Voucher Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, FGetVoucher_TypeQry("SaleInvoice", Ncat.SaleOrder + "," + Ncat.SaleOrderCancel))
                    Else
                        ReportFrm.CreateHelpGrid("VoucherType", "Voucher Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, FGetVoucher_TypeQry("SaleInvoice", Ncat.SaleInvoice + "," + Ncat.SaleReturn))
                    End If
                    ReportFrm.CreateHelpGrid("CashCredit", "Cash/Credit", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, "Select 'Cash' as Code, 'Cash' as Name Union All Select 'Credit' as Code, 'Credit' as Name Union All Select 'Both' as Code, 'Both' as Name", "Both")
                    ReportFrm.CreateHelpGrid("Agent", "Agent", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSalesAgentQry)
                    ReportFrm.CreateHelpGrid("Item Group", "Item Group", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpItemGroupQry)
                    ReportFrm.CreateHelpGrid("Item Category", "Item Category", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpItemCategoryQry)
                    ReportFrm.CreateHelpGrid("Item Type", "Item Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpItemTypeQry)
                    ReportFrm.CreateHelpGrid("City", "City", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpCityQry)
                    ReportFrm.CreateHelpGrid("State", "State", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpStateQry)
                    ReportFrm.CreateHelpGrid("SalesRepresentative", "Sales Representative", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSalesRepresentativeQry)
                    ReportFrm.CreateHelpGrid("ResponsiblePerson", "ResponsiblePerson", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpResponsiblePersonQry)
                    ReportFrm.CreateHelpGrid("Tag", "Tag", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpTagQry)
                    ReportFrm.CreateHelpGrid("Division", "Division", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpDivisionQry, "[DIVISIONCODE]")
                    ReportFrm.CreateHelpGrid("User", "User", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpUserQry)
                    ReportFrm.CreateHelpGrid("HSN", "HSN", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.StringType, "")
                    ReportFrm.CreateHelpGrid("Party Tax Group", "Party Tax Group", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpPartyTaxGroup)
                    ReportFrm.CreateHelpGrid("Item Tax Group", "Item Tax Group", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpItemTaxGroup)
                    ReportFrm.CreateHelpGrid("Catalog", "Catalog", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpCatalog)
                    ReportFrm.CreateHelpGrid("Supplier City", "Supplier City", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpCityQry)
                    ReportFrm.CreateHelpGrid("Item State", "Item State", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpItemStateQry)
                    ReportFrm.CreateHelpGrid("Account Type", "Account Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpAccountType)
                    ReportFrm.CreateHelpGrid("Account Nature", "Account Nature", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpAccountNature)
                    ReportFrm.CreateHelpGrid("Department", "Department", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpDepartment)
                    ReportFrm.FilterGrid.Rows(19).Visible = False 'Hide HSN Row


                Case SaleInvoiceReportAadhat
                    mQry = "Select 'Doc.Header Wise Detail' as Code, 'Doc.Header Wise Detail' as Name 
                            "
                    ReportFrm.CreateHelpGrid("Report Type", "Report Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mQry, "Month Wise Summary",,, 300)
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubEndDate)
                    ReportFrm.CreateHelpGrid("Party", "Party", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpPartyQry)
                    ReportFrm.CreateHelpGrid("Item", "Item", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpItemQry)
                    ReportFrm.CreateHelpGrid("Site", "Site", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSiteQry, "[SITECODE]")
                    If GRepFormName = SaleOrderReport Then
                        ReportFrm.CreateHelpGrid("VoucherType", "Voucher Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, FGetVoucher_TypeQry("SaleInvoice", Ncat.SaleOrder + "," + Ncat.SaleOrderCancel))
                    Else
                        ReportFrm.CreateHelpGrid("VoucherType", "Voucher Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, FGetVoucher_TypeQry("SaleInvoice", Ncat.SaleInvoice + "," + Ncat.SaleReturn))
                    End If
                    ReportFrm.CreateHelpGrid("CashCredit", "Cash/Credit", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, "Select 'Cash' as Code, 'Cash' as Name Union All Select 'Credit' as Code, 'Credit' as Name Union All Select 'Both' as Code, 'Both' as Name", "Both")
                    ReportFrm.CreateHelpGrid("Agent", "Agent", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSalesAgentQry)
                    ReportFrm.CreateHelpGrid("Item Group", "Item Group", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpItemGroupQry)
                    ReportFrm.CreateHelpGrid("Item Category", "Item Category", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpItemCategoryQry)
                    ReportFrm.CreateHelpGrid("City", "City", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpCityQry)
                    ReportFrm.CreateHelpGrid("State", "State", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpStateQry)
                    ReportFrm.CreateHelpGrid("Tag", "Tag", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpTagQry)
                    ReportFrm.CreateHelpGrid("Division", "Division", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpDivisionQry, "[DIVISIONCODE]")



                Case SaleOrderStatus
                    mQry = "Select 'Item Wise Balance' as Code, 'Item Wise Balance' as Name 
                            Union All Select 'Item Wise Status' as Code, 'Item Wise Status' as Name 
                            "

                    ReportFrm.CreateHelpGrid("Report Type", "Report Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mQry, "Item Wise Balance",,, 300)
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubEndDate)
                    ReportFrm.CreateHelpGrid("Party", "Party", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpPartyQry)
                    ReportFrm.CreateHelpGrid("Item", "Item", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpItemQry)
                    ReportFrm.CreateHelpGrid("Site", "Site", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSiteQry, "[SITECODE]")
                    If GRepFormName = SaleOrderReport Then
                        ReportFrm.CreateHelpGrid("VoucherType", "Voucher Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, FGetVoucher_TypeQry("SaleInvoice", Ncat.SaleOrder + "," + Ncat.SaleOrderCancel))
                    Else
                        ReportFrm.CreateHelpGrid("VoucherType", "Voucher Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, FGetVoucher_TypeQry("SaleInvoice", Ncat.SaleInvoice + "," + Ncat.SaleReturn))
                    End If
                    ReportFrm.CreateHelpGrid("CashCredit", "Cash/Credit", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, "Select 'Cash' as Code, 'Cash' as Name Union All Select 'Credit' as Code, 'Credit' as Name Union All Select 'Both' as Code, 'Both' as Name", "Both")
                    ReportFrm.CreateHelpGrid("Agent", "Agent", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSalesAgentQry)
                    ReportFrm.CreateHelpGrid("Item Group", "Item Group", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpItemGroupQry)
                    ReportFrm.CreateHelpGrid("Item Category", "Item Category", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpItemCategoryQry)
                    ReportFrm.CreateHelpGrid("City", "City", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpCityQry)
                    ReportFrm.CreateHelpGrid("State", "State", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpStateQry)
                    ReportFrm.CreateHelpGrid("SalesRepresentative", "Sales Representative", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSalesRepresentativeQry)
                    ReportFrm.CreateHelpGrid("ResponsiblePerson", "ResponsiblePerson", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpResponsiblePersonQry)
                    ReportFrm.CreateHelpGrid("Tag", "Tag", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpTagQry)
                    ReportFrm.CreateHelpGrid("Division", "Division", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpDivisionQry, "[DIVISIONCODE]")
                    mQry = "Select 'Amount Balance' as Code, 'Amount Balance' as Name 
                            Union All 
                            Select 'Qty Balance' as Code, 'Qty Balance' as Name 
                            Union All 
                            Select 'Bale Balance' as Code, 'Bale Balance' as Name 
                            "
                    ReportFrm.CreateHelpGrid("Balance Type", "Balance Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mQry, "Qty Balance",,, 300)

                Case SaleChallanStatus
                    mQry = "Select 'Item Wise Balance' as Code, 'Item Wise Balance' as Name 
                            Union All Select 'Item Wise Status' as Code, 'Item Wise Status' as Name 
                            "

                    ReportFrm.CreateHelpGrid("Report Type", "Report Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mQry, "Item Wise Balance",,, 300)
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubEndDate)
                    ReportFrm.CreateHelpGrid("Party", "Party", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpPartyQry)
                    ReportFrm.CreateHelpGrid("Item", "Item", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpItemQry)
                    ReportFrm.CreateHelpGrid("Site", "Site", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSiteQry, "[SITECODE]")
                    ReportFrm.CreateHelpGrid("VoucherType", "Voucher Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, FGetVoucher_TypeQry("SaleInvoice", Ncat.SaleChallan))
                    ReportFrm.CreateHelpGrid("CashCredit", "Cash/Credit", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, "Select 'Cash' as Code, 'Cash' as Name Union All Select 'Credit' as Code, 'Credit' as Name Union All Select 'Both' as Code, 'Both' as Name", "Both")
                    ReportFrm.CreateHelpGrid("Agent", "Agent", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSalesAgentQry)
                    ReportFrm.CreateHelpGrid("Item Group", "Item Group", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpItemGroupQry)
                    ReportFrm.CreateHelpGrid("Item Category", "Item Category", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpItemCategoryQry)
                    ReportFrm.CreateHelpGrid("City", "City", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpCityQry)
                    ReportFrm.CreateHelpGrid("State", "State", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpStateQry)
                    ReportFrm.CreateHelpGrid("SalesRepresentative", "Sales Representative", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSalesRepresentativeQry)
                    ReportFrm.CreateHelpGrid("ResponsiblePerson", "ResponsiblePerson", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpResponsiblePersonQry)
                'ReportFrm.CreateHelpGrid("Tag", "Tag", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpTagQry)
                'ReportFrm.CreateHelpGrid("Division", "Division", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpDivisionQry, "[DIVISIONCODE]")
                'mQry = "Select 'Amount Balance' as Code, 'Amount Balance' as Name 
                '        Union All 
                '        Select 'Qty Balance' as Code, 'Qty Balance' as Name 
                '        Union All 
                '        Select 'Bale Balance' as Code, 'Bale Balance' as Name 
                '        "
                'ReportFrm.CreateHelpGrid("Balance Type", "Balance Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mQry, "Qty Balance",,, 300)


                Case DebitCreditNoteReport
                    mQry = "Select 'Entry Head Wise Detail' as Code, 'Entry Head Wise Detail' as Name 
                            Union All Select 'Entry Line Wise Detail' as Code, 'Entry Line Wise Detail' as Name 
                            Union All Select 'Month Wise Summary' as Code, 'Month Wise Summary' as Name 
                            Union All Select 'Party Wise Summary' as Code, 'Party Wise Summary' as Name 
                            Union All Select 'Agent Wise Summary' as Code, 'Agent Wise Summary' as Name 
                            Union All Select 'Voucher Type Wise Summary' as Code, 'Voucher Type Wise Summary' as Name 
                            Union All Select 'City Wise Summary' as Code, 'City Wise Summary' as Name 
                            Union All Select 'State Wise Summary' as Code, 'State Wise Summary' as Name                             
                            "
                    ReportFrm.CreateHelpGrid("Report Type", "Report Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mQry, "Voucher Type Wise Summary")
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubEndDate)
                    ReportFrm.CreateHelpGrid("Party", "Party", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpPartyQry)
                    ReportFrm.CreateHelpGrid("Site", "Site", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSiteQry)
                    ReportFrm.CreateHelpGrid("VoucherType", "Voucher Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, FGetVoucher_TypeQry("LedgerHead", Ncat.DebitNoteSupplier + "," + Ncat.DebitNoteCustomer + "," + Ncat.CreditNoteCustomer + "," + Ncat.CreditNoteSupplier))
                    ReportFrm.CreateHelpGrid("Agent", "Agent", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSalesAgentQry)
                    ReportFrm.CreateHelpGrid("City", "City", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpCityQry)
                    ReportFrm.CreateHelpGrid("State", "State", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpStateQry)


                Case ExpenseIncomeReport
                    mQry = "Select 'Entry Head Wise Detail' as Code, 'Entry Head Wise Detail' as Name 
                            Union All Select 'Entry Line Wise Detail' as Code, 'Entry Line Wise Detail' as Name 
                            Union All Select 'Month Wise Summary' as Code, 'Month Wise Summary' as Name 
                            Union All Select 'Party Wise Summary' as Code, 'Party Wise Summary' as Name 
                            Union All Select 'Agent Wise Summary' as Code, 'Agent Wise Summary' as Name 
                            Union All Select 'Voucher Type Wise Summary' as Code, 'Voucher Type Wise Summary' as Name 
                            Union All Select 'City Wise Summary' as Code, 'City Wise Summary' as Name 
                            Union All Select 'State Wise Summary' as Code, 'State Wise Summary' as Name                             
                            "
                    ReportFrm.CreateHelpGrid("Report Type", "Report Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mQry, "Voucher Type Wise Summary")
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubEndDate)
                    ReportFrm.CreateHelpGrid("Party", "Party", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpPartyQry)
                    ReportFrm.CreateHelpGrid("Site", "Site", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSiteQry)
                    ReportFrm.CreateHelpGrid("VoucherType", "Voucher Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, FGetVoucher_TypeQry("LedgerHead", Ncat.DebitNoteSupplier + "," + Ncat.DebitNoteCustomer + "," + Ncat.CreditNoteCustomer + "," + Ncat.CreditNoteSupplier))
                    ReportFrm.CreateHelpGrid("Agent", "Agent", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSalesAgentQry)
                    ReportFrm.CreateHelpGrid("City", "City", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpCityQry)
                    ReportFrm.CreateHelpGrid("State", "State", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpStateQry)


                Case PurchaseInvoiceReport, PurchaseOrderReport
                    mQry = "Select 'Invoice Wise Detail' as Code, 'Invoice Wise Detail' as Name 
                            Union All Select 'Item Wise Detail' as Code, 'Item Wise Detail' as Name 
                            Union All Select 'Month Wise Summary' as Code, 'Month Wise Summary' as Name 
                            Union All Select 'Party Wise Summary' as Code, 'Party Wise Summary' as Name 
                            Union All Select 'Agent Wise Summary' as Code, 'Agent Wise Summary' as Name 
                            Union All Select 'HSN Wise Summary' as Code, 'HSN Wise Summary' as Name 
                            Union All Select 'Item Wise Summary' as Code, 'Item Wise Summary' as Name 
                            Union All Select 'Item Group Wise Summary' as Code, 'Item Group Wise Summary' as Name 
                            Union All Select 'Item Category Wise Summary' as Code, 'Item Category Wise Summary' as Name 
                            Union All Select 'City Wise Summary' as Code, 'City Wise Summary' as Name 
                            Union All Select 'State Wise Summary' as Code, 'State Wise Summary' as Name                             
                            "
                    ReportFrm.CreateHelpGrid("Report Type", "Report Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mQry, "Month Wise Summary")
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubEndDate)
                    ReportFrm.CreateHelpGrid("Party", "Party", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpPartyQry)
                    ReportFrm.CreateHelpGrid("Item", "Item", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpItemQry)
                    ReportFrm.CreateHelpGrid("Site", "Site", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSiteQry)
                    ReportFrm.CreateHelpGrid("VoucherType", "Voucher Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, FGetVoucher_TypeQry("PurchInvoice"))
                    ReportFrm.CreateHelpGrid("CashCredit", "Cash/Credit", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, "Select 'Cash' as Code, 'Cash' as Name Union All Select 'Credit' as Code, 'Credit' as Name Union All Select 'Both' as Code, 'Both' as Name", "Both")
                    ReportFrm.CreateHelpGrid("Agent", "Agent", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpPurchaseAgentQry)
                    ReportFrm.CreateHelpGrid("Item Group", "Item Group", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpItemGroupQry)
                    ReportFrm.CreateHelpGrid("Item Category", "Item Category", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpItemCategoryQry)
                    ReportFrm.CreateHelpGrid("City", "City", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpCityQry)
                    ReportFrm.CreateHelpGrid("State", "State", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpStateQry)
                    ReportFrm.CreateHelpGrid("HSN", "HSN", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.StringType, "")
                    ReportFrm.CreateHelpGrid("Party Tax Group", "Party Tax Group", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpPartyTaxGroup)
                    ReportFrm.CreateHelpGrid("Party Trade Type", "Party Trade Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpPartyTradeTypeQry)
                    ReportFrm.CreateHelpGrid("Item Tax Group", "Item Tax Group", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpItemTaxGroup)
                    ReportFrm.FilterGrid.Rows(13).Visible = False 'Hide HSN Row

                Case RateListReport
                    ReportFrm.CreateHelpGrid("ItemCategory", "ItemCategory", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpItemCategoryQry)
                    ReportFrm.CreateHelpGrid("ItemGroup", "ItemGroup", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpItemGroupQry)
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubEndDate)
                    mQry = "Select 'Not Applicable' as Code, 'Not Applicable' as Name 
                            Union All Select 'Item Master Date' as Code, 'Item Master Date' as Name 
                            Union All Select 'Item Transaction Date' as Code, 'Item Transaction Date' as Name 
                            Union All Select 'Stock' as Code, 'Stock' as Name 
                            "
                    ReportFrm.CreateHelpGrid("Date Filter On", "Date Filter On", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mQry, "Item Master Date")


                Case SalesAgentCommissionOnPayment, SalesRepresentativeCommissionOnPayment
                    If GRepFormName = SalesRepresentativeCommissionOnPayment Then
                        mQry = "Select 'Payment Wise Detail' as Code, 'Payment Wise Detail' as Name 
                            Union All Select 'Party Wise Summary' as Code, 'Party Wise Summary' as Name 
                            Union All Select 'SalesRepresentative Wise Summary' as Code, 'SalesRepresentative Wise Summary' as Name 
                            Union All Select 'SalesRepresentative Wise Periodic Summary' as Code, 'SalesRepresentative Wise Periodic Summary' as Name 
                            Union All Select 'Party Wise Periodic Summary' as Code, 'Party Wise Periodic Summary' as Name 
                            "
                        ReportFrm.CreateHelpGrid("Report Type", "Report Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mQry, "SalesRepresentative Wise Summary",, 600, 500)
                    Else
                        mQry = "Select 'Payment Wise Detail' as Code, 'Payment Wise Detail' as Name 
                            Union All Select 'Party Wise Summary' as Code, 'Party Wise Summary' as Name 
                            Union All Select 'Agent Wise Summary' as Code, 'Agent Wise Summary' as Name 
                            Union All Select 'Agent Wise Periodic Summary' as Code, 'Agent Wise Periodic Summary' as Name 
                            Union All Select 'Party Wise Periodic Summary' as Code, 'Party Wise Periodic Summary' as Name 
                            "
                        ReportFrm.CreateHelpGrid("Report Type", "Report Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mQry, "Agent Wise Summary")
                    End If
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubEndDate)
                    ReportFrm.CreateHelpGrid("Commission %", "Commission %", FrmRepDisplay.FieldFilterDataType.NumericType, FrmRepDisplay.FieldDataType.FloatType, "", "1")
                    ReportFrm.CreateHelpGrid("Party", "Party", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpPartyQry)
                    mQry = "SELECT 'o' As Tick, Vt.V_Type AS Code, Vt.Description
                            FROM Voucher_Type Vt 
                            WHERE Vt.Category IN ('RCT','JV') "
                    ReportFrm.CreateHelpGrid("VoucherType", "Voucher Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mQry)
                    ReportFrm.CreateHelpGrid("Agent", "Agent", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSalesAgentQry)
                    ReportFrm.CreateHelpGrid("City", "City", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpCityQry)
                    ReportFrm.CreateHelpGrid("Area", "Area", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpAreaQry)
                    ReportFrm.CreateHelpGrid("State", "State", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpStateQry)
                    ReportFrm.CreateHelpGrid("Site", "Site", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSiteQry, "[SITECODE]")
                    ReportFrm.CreateHelpGrid("Division", "Division", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpDivisionQry, "[DIVISIONCODE]")
                    If GRepFormName = SalesRepresentativeCommissionOnPayment Then
                        ReportFrm.CreateHelpGrid("Report On", "Report On", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.StringType, "", "Sales Representative")
                    Else
                        ReportFrm.CreateHelpGrid("Report On", "Report On", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.StringType, "", "Agent")
                    End If
                    ReportFrm.FilterGrid.Rows(12).Visible = False 'Hide HSN Row


                'Case StockReport
                '    mQry = "Select 'Stock Balance' as Code, 'Stock Balance' as Name 
                '            Union All Select 'Stock Summary' as Code, 'Stock Summary' as Name 
                '    Union All Select 'Stock Ledger' as Code, 'Stock Ledger' as Name 
                '            "
                '    ReportFrm.CreateHelpGrid("Report Type", "Report Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mQry, "Stock Balance")
                '    ReportFrm.CreateHelpGrid("FromDate", "From Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubStartDate)
                '    ReportFrm.CreateHelpGrid("ToDate", "To Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubEndDate)
                '    mQry = "Select 'o' As Tick, 'In Hand' as Code, 'In Hand' as Name
                '            Union All Select 'o' As Tick, 'At Person' as Code, 'At Person' as Name                            
                '           "
                '    ReportFrm.CreateHelpGrid("LocationType", "LocationType", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mQry)
                '    ReportFrm.CreateHelpGrid("Location", "Location", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpLocationQry)
                '    ReportFrm.CreateHelpGrid("ItemCategory", "ItemCategory", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpItemCategoryQry)
                '    ReportFrm.CreateHelpGrid("ItemGroup", "ItemGroup", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpItemGroupQry)
                '    ReportFrm.CreateHelpGrid("Item", "Item", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpItemQry)
                '    ReportFrm.CreateHelpGrid("Site", "Site", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSiteQry, "[SITECODE]")
                '    ReportFrm.CreateHelpGrid("Division", "Division", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpDivisionQry, "[DIVISIONCODE]")


                Case DebtorsOutstandingReport
                    mQry = "Select 'Party Wise Summary' as Code, 'Party Wise Summary' as Name 
                            Union All Select 'Party Wise Ageing' as Code, 'Party Wise Ageing' as Name 
                            Union All Select 'Invoice Wise Detail' as Code, 'Invoice Wise Detail' as Name 
                            "
                    ReportFrm.CreateHelpGrid("Report Type", "Report Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mQry, "Party Wise Summary")
                    mQry = "Select 'FIFO' as Code, 'FIFO' as Name 
                            Union All Select 'Adjustment' as Code, 'Adjustment' as Name 
                           "
                    ReportFrm.CreateHelpGrid("Calculation", "Calculation", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mQry, "FIFO")

                    ReportFrm.CreateHelpGrid("AsOnDate", "As On Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubLoginDate)
                    ReportFrm.CreateHelpGrid("LeaverageDays", "Leaverage Days", FrmRepDisplay.FieldFilterDataType.NumericType, FrmRepDisplay.FieldDataType.FloatType, "", "90")
                    ReportFrm.CreateHelpGrid("Party", "Party", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpPartyQry)
                    ReportFrm.CreateHelpGrid("AccountGroup", "Account Group", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpAcGroupCustomerQry)
                    ReportFrm.CreateHelpGrid("Agent", "Agent", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSalesAgentQry)
                    ReportFrm.CreateHelpGrid("City", "City", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpCityQry)
                    ReportFrm.CreateHelpGrid("State", "State", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpStateQry)
                    ReportFrm.CreateHelpGrid("Area", "Area", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpAreaQry)
                    ReportFrm.CreateHelpGrid("Site", "Site", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSiteQry, "[SITECODE]")
                    ReportFrm.CreateHelpGrid("Division", "Division", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpDivisionQry, "[DIVISIONCODE]")


                Case CreditorsOutstandingReport
                    mQry = "Select 'Party Wise Summary' as Code, 'Party Wise Summary' as Name 
                            Union All Select 'Party Wise Ageing' as Code, 'Party Wise Ageing' as Name 
                            Union All Select 'Invoice Wise Detail' as Code, 'Invoice Wise Detail' as Name 
                            "
                    ReportFrm.CreateHelpGrid("Report Type", "Report Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mQry, "Party Wise Summary")
                    mQry = "Select 'FIFO' as Code, 'FIFO' as Name 
                            Union All Select 'Adjustment' as Code, 'Adjustment' as Name 
                           "
                    ReportFrm.CreateHelpGrid("Calculation", "Calculation", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mQry, "FIFO")

                    ReportFrm.CreateHelpGrid("AsOnDate", "As On Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubLoginDate)
                    ReportFrm.CreateHelpGrid("LeaverageDays", "Leaverage Days", FrmRepDisplay.FieldFilterDataType.NumericType, FrmRepDisplay.FieldDataType.FloatType, "", "90")
                    ReportFrm.CreateHelpGrid("Party", "Party", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpPartyQry)
                    ReportFrm.CreateHelpGrid("AccountGroup", "Account Group", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpAcGroupSupplierQry)
                    ReportFrm.CreateHelpGrid("Agent", "Agent", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpPurchaseAgentQry)
                    ReportFrm.CreateHelpGrid("City", "City", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpCityQry)
                    ReportFrm.CreateHelpGrid("State", "State", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpStateQry)
                    ReportFrm.CreateHelpGrid("Area", "Area", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpAreaQry)
                    ReportFrm.CreateHelpGrid("Site", "Site", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSiteQry, "[SITECODE]")
                    ReportFrm.CreateHelpGrid("Division", "Division", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpDivisionQry, "[DIVISIONCODE]")


                Case MoneyReceiptReport
                    mQry = "Select 'Voucher Wise Detail' as Code, 'Voucher Wise Detail' as Name 
                            Union All Select 'Agent Wise Summary' as Code, 'Agent Wise Summary' as Name 
                            Union All Select 'Party Wise Summary' as Code, 'Party Wise Summary' as Name 
                            Union All Select 'Voucher Wise Detail (Agent)' as Code, 'Voucher Wise Detail (Agent)' as Name 
                           "
                    mQry = "Select 'Voucher Wise Detail' as Code, 'Voucher Wise Detail' as Name                             
                            Union All Select 'Party Wise Summary' as Code, 'Party Wise Summary' as Name                             
                           "

                    ReportFrm.CreateHelpGrid("Report Type", "Report Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mQry, "Party Wise Summary")
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubLoginDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubEndDate)
                    ReportFrm.CreateHelpGrid("Agent Commission %", "Agent Commission %", FrmRepDisplay.FieldFilterDataType.NumericType, FrmRepDisplay.FieldDataType.FloatType, "", "1")
                    ReportFrm.CreateHelpGrid("Party", "Party", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpPartyQry)
                    ReportFrm.CreateHelpGrid("VoucherType", "Voucher Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, FGetVoucher_TypeQry("SaleInvoice"))
                    ReportFrm.CreateHelpGrid("Agent", "Agent", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSalesAgentQry)
                    ReportFrm.CreateHelpGrid("City", "City", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpCityQry)
                    ReportFrm.CreateHelpGrid("State", "State", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpStateQry)
                    ReportFrm.CreateHelpGrid("Site", "Site", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSiteQry, "[SITECODE]")
                    ReportFrm.CreateHelpGrid("Division", "Division", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpDivisionQry, "[DIVISIONCODE]")


                Case PackedBalesLocationReport
                    mQry = "Select 'LR Wise Detail' as Code, 'LR Wise Detail' as Name                             
                            Union All Select 'Location Type Wise Summary' as Code, 'Location Type Wise Summary' as Name
                            Union All Select 'Location Wise Summary' as Code, 'Location Wise Summary' as Name
                            Union All Select 'Party Wise Summary' as Code, 'Party Wise Summary' as Name                 
                            Union All Select 'Agent Wise Summary' as Code, 'Agent Wise Summary' as Name                            
                           "
                    ReportFrm.CreateHelpGrid("Report Type", "Report Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mQry, "Location Type Wise Summary")
                    mQry = "Select H.SubgroupType as Code, H.SubgroupType as Name From SubgroupType H Where IsNull(H.Parent,H.SubgroupType) = '" & SubgroupType.Transporter & "'
                            Union All Select H.SubgroupType as Code, H.SubgroupType as Name From SubgroupType H Where IsNull(H.Parent,H.SubgroupType) = '" & SubgroupType.Employee & "'
                            Union All Select H.SubgroupType as Code, H.SubgroupType as Name From SubgroupType H Where IsNull(H.Parent,H.SubgroupType) = '" & SubgroupType.Godown & "'                            
                           "
                    ReportFrm.CreateHelpGrid("Location Type", "Location Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mQry)
                    ReportFrm.CreateHelpGrid("Location", "Location", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpLocationQry)
                    ReportFrm.CreateHelpGrid("Party", "Party", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpPartyQry)
                    ReportFrm.CreateHelpGrid("Agent", "Agent", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSalesAgentQry)
                    ReportFrm.CreateHelpGrid("City", "City", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpCityQry)
                    ReportFrm.CreateHelpGrid("State", "State", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpStateQry)
                    ReportFrm.CreateHelpGrid("Site", "Site", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSiteQry, "[SITECODE]")
                    ReportFrm.CreateHelpGrid("Division", "Division", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpDivisionQry, "[DIVISIONCODE]")


                Case BaleMovementReport
                    mQry = "Select 'LR Wise Detail' as Code, 'LR Wise Detail' as Name                             
                            Union All Select 'Location Type Wise Summary' as Code, 'Location Type Wise Summary' as Name
                            Union All Select 'Location Wise Summary' as Code, 'Location Wise Summary' as Name
                            Union All Select 'Party Wise Summary' as Code, 'Party Wise Summary' as Name                 
                            Union All Select 'Agent Wise Summary' as Code, 'Agent Wise Summary' as Name                            
                           "
                    ReportFrm.CreateHelpGrid("Report Type", "Report Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mQry, "Location Type Wise Summary")
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubLoginDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubLoginDate)
                    mQry = "Select H.SubgroupType as Code, H.SubgroupType as Name From SubgroupType H Where IsNull(H.Parent,H.SubgroupType) = '" & SubgroupType.Transporter & "'
                            Union All Select H.SubgroupType as Code, H.SubgroupType as Name From SubgroupType H Where IsNull(H.Parent,H.SubgroupType) = '" & SubgroupType.Employee & "'
                            Union All Select H.SubgroupType as Code, H.SubgroupType as Name From SubgroupType H Where IsNull(H.Parent,H.SubgroupType) = '" & SubgroupType.Godown & "'                            
                           "
                    ReportFrm.CreateHelpGrid("Location Type", "Location Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mQry)
                    ReportFrm.CreateHelpGrid("Location", "Location", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpLocationQry)
                    ReportFrm.CreateHelpGrid("Party", "Party", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpPartyQry)
                    ReportFrm.CreateHelpGrid("Agent", "Agent", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSalesAgentQry)
                    ReportFrm.CreateHelpGrid("City", "City", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpCityQry)
                    ReportFrm.CreateHelpGrid("State", "State", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpStateQry)
                    ReportFrm.CreateHelpGrid("Site", "Site", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSiteQry, "[SITECODE]")
                    ReportFrm.CreateHelpGrid("Division", "Division", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpDivisionQry, "[DIVISIONCODE]")


                Case FsnAnalysis
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubEndDate)
                    ReportFrm.CreateHelpGrid("Fast %", "Fast %", FrmRepDisplay.FieldFilterDataType.NumericType, FrmRepDisplay.FieldDataType.FloatType, "", "50")
                    ReportFrm.CreateHelpGrid("Slow %", "Slow %", FrmRepDisplay.FieldFilterDataType.NumericType, FrmRepDisplay.FieldDataType.FloatType, "", "70")
                    ReportFrm.CreateHelpGrid("Item Group", "Item Group", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpItemGroupQry)
                    ReportFrm.CreateHelpGrid("Item Category", "Item Category", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpItemCategoryQry)
                    ReportFrm.CreateHelpGrid("Division", "Division", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpDivisionQry, "[DIVISIONCODE]")
                    ReportFrm.CreateHelpGrid("Site", "Site", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSiteQry, "[SITECODE]")

                Case EWayBillGeneration
                    ReportFrm.BtnCustomMenu.Visible = True
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubLoginDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubLoginDate)
                    ReportFrm.CreateHelpGrid("InvoiceValueGreaterThen", "Invoice Value Greater Then", FrmRepDisplay.FieldFilterDataType.NumericType, FrmRepDisplay.FieldDataType.FloatType, "", "50000")

                Case LRStatusChange
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubLoginDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubLoginDate)

                Case LedgerPostingDifference
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubLoginDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubLoginDate)

                Case ChequeSearching
                    ReportFrm.CreateHelpGrid("ChequeNo", "Cheque No.", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.StringType, "", "")



                Case LogReport
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.RetMonthStartDate(AgL.PubLoginDate))
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubLoginDate)
                    mQry = "Select 'Action Date' as Code, 'Action Date' as Name                             
                            Union All Select 'Entry Date' as Code, 'Entry Date' as Name "
                    ReportFrm.CreateHelpGrid("Filter On Date", "Filter On Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mQry, "Action Date")
                    ReportFrm.CreateHelpGrid("VoucherType", "Voucher Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpVoucherTypeQry)
                    ReportFrm.CreateHelpGrid("Division", "Division", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpDivisionQry)
                    ReportFrm.CreateHelpGrid("Site", "Site", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSiteQry)
                    mQry = "Select 'All' as Code, 'All' as Name                             
                            Union All Select 'A' as Code, 'Only Add' as Name 
                            Union All Select 'E' as Code, 'Only Edit' as Name 
                            Union All Select 'D' as Code, 'Only Delete' as Name 
                            Union All Select 'P' as Code, 'Only Print' as Name "
                    ReportFrm.CreateHelpGrid("Action", "Action", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mQry, "All")
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
#End Region

    Private Function FGetVoucher_TypeQry(ByVal TableName As String, Optional NCat As String = "") As String
        Dim mQry As String
        mQry = " SELECT Distinct 'o' As Tick, H.V_Type AS Code, Vt.Description AS [Voucher Type] " &
                                " FROM " & TableName & " H  " &
                                " LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type "
        If NCat <> "" Then
            NCat = Replace(NCat, ",", "','")
            mQry = mQry & " Where Vt.NCat In ('" & NCat & "') "
        End If
        FGetVoucher_TypeQry = mQry
    End Function

    Private Sub ObjRepFormGlobal_ProcessReport() Handles ReportFrm.ProcessReport
        Select Case mGRepFormName
            Case SaleInvoiceReport, SaleOrderReport
                ProcSaleReport()

            Case SaleInvoiceReportAadhat
                ProcSaleReportAadhat()

            Case SaleOrderStatus
                ProcSaleOrderStatus()

            Case SaleChallanStatus
                ProcSaleChallanStatus()

            Case DebitCreditNoteReport
                ProcDebitCreditNoteReport()

            Case ExpenseIncomeReport
                ProcExpenseIncomeReport()


            Case PurchaseInvoiceReport
                ProcPurchaseReport()

            Case RateListReport
                ProcRateListReport()

            Case SalesAgentCommissionOnPayment, SalesRepresentativeCommissionOnPayment
                ProcSalesAgentAndSalesRepresentativeCommissionReport()

            'Case StockReport
            '    ProcStockReport()

            Case MoneyReceiptReport
                ProcMoneyReceiptReport()

            Case PackedBalesLocationReport
                ProcPackedBalesReport()

            Case BaleMovementReport
                ProcBaleMovementReport()

            Case FsnAnalysis
                ProcFsnAnalysis()

            Case EWayBillGeneration
                ProcEWayBillGeneration()

            Case DebtorsOutstandingReport
                ProcDebtorsOutstaningReport()

            Case CreditorsOutstandingReport
                ProcCreditorsOutstaningReport()

            Case LRStatusChange
                ProcLRStatusChange()

            Case LedgerPostingDifference
                ProcLedgerPostingDifference()

            Case ChequeSearching
                ProcChequeSearching()

            Case LogReport
                ProcLogReport()
        End Select
    End Sub
    Public Sub New(ByVal mReportFrm As FrmRepDisplay)
        ReportFrm = mReportFrm
    End Sub

    Public Sub ProcSaleOrderStatus(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try
            Dim mCondStr$ = ""
            Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"
            Dim mTags As String() = Nothing
            Dim J As Integer



            RepTitle = "Sale Order Status"

            If mFilterGrid IsNot Nothing And mGridRow IsNot Nothing Then
                If mGridRow.DataGridView.Columns.Contains("Search Code") = True Then
                    If mFilterGrid.Item(GFilter, 0).Value = "Item Wise Balance" Or
                            mFilterGrid.Item(GFilter, 0).Value = "Item Wise Status" Then
                        ClsMain.FOpenForm(mGridRow.Cells("Search Code").Value, ReportFrm)
                        ReportFrm.FiterGridCopy_Arr.RemoveAt(ReportFrm.FiterGridCopy_Arr.Count - 1)
                        Exit Sub
                    End If
                Else
                    Exit Sub
                End If
            End If

            mCondStr = " Where VT.NCat In ('" & Ncat.SaleOrder & "', '" & Ncat.SaleOrderCancel & "') "
            mCondStr = mCondStr & " AND H.Div_Code = '" & AgL.PubDivCode & "' "
            mCondStr = mCondStr & " AND Date(H.V_Date) Between " & AgL.Chk_Date(CDate(ReportFrm.FGetText(1)).ToString("s")) & " And " & AgL.Chk_Date(CDate(ReportFrm.FGetText(2)).ToString("s")) & " "
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.SaleToParty", 3)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Item", 4)
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("H.Site_Code", 5), "''", "'")
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

            If ReportFrm.FGetText(15) <> "All" Then
                mTags = ReportFrm.FGetText(15).ToString.Split(",")
                For J = 0 To mTags.Length - 1
                    mCondStr += " And CharIndex('+' || '" & mTags(J) & "',H.Tags) > 0 "
                Next
            End If
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("H.Div_Code", 16), "''", "'")

            mQry = " SELECT L.DocID, L.Sr, H.V_Type, Vt.Description as VoucherType, Site.Name as Site, Div.Div_Name as Division,                    
                    strftime('%d/%m/%Y', H.V_Date) As V_Date, H.V_Date As V_Date_ActualFormat,                    
                    (Case When H.SaleToParty=H.BillToParty Then Party.Name Else BillToParty.Name || ' - ' || Party.Name End) As SaleToPartyName ,                                         
                    H.V_Type || '-' || H.ManualRefNo as OrderNo, H.ManualRefNo, 
                    I.Specification as ItemSpecification, I.Description As ItemDesc,IG.Description as ItemGroupDescription, 
                    IC.Description as ItemCategoryDescription, L.Qty as NoOfBales, L.Qty, L.Amount, IfNull(SI.BillNoOfBales,0) as BillBales, IfNull(SI.BillQty,0) as BillQty, IfNull(SI.BillAmount,0) as BillAmount, 
                    (Case When L.Qty - IfNull(SI.BillNoOfBales,0) > 0  Then L.Qty - IfNull(SI.BillNoOfBales,0) Else 0 End) as BalanceBales,                                                            
                    (Case When L.Qty - IfNull(SI.BillQty,0) > 0  Then L.Qty - IfNull(SI.BillQty,0) Else 0 End) as BalanceQty,                                                            
                    (Case When L.Amount - IfNull(SI.BillAmount,0) > 0  Then L.Amount - IfNull(SI.BillAmount,0) Else 0 End) as BalanceAmount                                                            
                    FROM SaleOrder H 
                    Left Join SaleOrderDetail L On H.DocID = L.DocID 
                    Left Join (
                                select BL.SaleOrder, BL.SaleOrderSr, Sum(Case When BL.Sr=1 Then IfNull(BT.NoOfBales,1) Else 0 End) BillNoOfBales, Sum(BL.Qty) as BillQty, Sum(BL.Amount) as BillAmount
                                From SaleBill BH With (NoLock)
                                Left Join SaleBillDetail BL With (NoLock) On BH.DocId = BL.DocID                                
                                Left Join SaleInvoiceTransport BT With (NoLock) On BH.DocId = BT.DocId
                                Group By BL.SaleOrder, BL.SaleOrdersr
                              ) SI On L.DocID = SI.SaleOrder And L.Sr = SI.SaleOrderSr
                    Left Join Item I On L.Item = I.Code 
                    Left Join Item IG On I.ItemGroup = IG.Code
                    Left Join Item IC On I.ItemCategory = IC.Code
                    Left Join viewHelpSubgroup Party On H.SaleToParty = Party.Code 
                    Left Join viewHelpSubgroup BillToParty On H.BillToParty = BillToParty.Code 
                    Left Join (Select SILTV.Subcode, Max(SILTV.Agent) as Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode) as LTV On Party.code = LTV.Subcode                                                           
                    LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type     
                    Left Join SiteMast Site On H.Site_Code = Site.Code
                    Left Join Division Div On H.Div_Code = Div.Div_Code
                    " & mCondStr


            If ReportFrm.FGetText(0) = "Item Wise Balance" Then
                mQry = " Select VMain.DocId As SearchCode, Max(VMain.Division) as Division, Max(Vmain.Site) as Site, Max(VMain.V_Date) As OrderDate, Max(VMain.OrderNo) As OrderNo,
                    Max(VMain.SaleToPartyName) As Party, Max(Vmain.ItemDesc) as ItemDescription, Max(VMain.NoOfBales) as OrderBales, Max(VMain.Qty) as OrderQty, Max(VMain.Amount) as OrderAmount, Max(Vmain.BalanceBales) as BalanceBales, Max(VMain.BalanceQty) as BalanceQty, Max(VMain.BalanceAmount) as BalanceAmount
                    From (" & mQry & ") As VMain
                    GROUP By VMain.DocId, VMain.Sr  "

                If ReportFrm.FGetText(17) = "Amount Balance" Then
                    mQry += "Having Max(VMain.BalanceAmount) > 0 "
                ElseIf ReportFrm.FGetText(17) = "Bale Balance" Then
                    mQry += "Having Max(VMain.BalanceBales) > 0 "
                Else
                    mQry += "Having Max(VMain.BalanceQty) > 0 "
                End If

                mQry += "Order By Max(VMain.V_Date_ActualFormat), Cast(Max(Replace(Vmain.ManualRefNo,'-','')) as Integer) "

            ElseIf ReportFrm.FGetText(0) = "Item Wise Status" Then
                    mQry = " Select VMain.DocId As SearchCode, Max(VMain.Division) as Division, Max(Vmain.Site) as Site, Max(VMain.V_Date) As OrderDate, Max(VMain.OrderNo) As OrderNo,
                    Max(VMain.SaleToPartyName) As Party, Max(Vmain.ItemDesc) as ItemDescription, Max(VMain.NoOfBales) as OrderBales, Max(VMain.BillBales) as InvoiceBales, Max(Vmain.BalanceBales) as BalanceBales, Max(VMain.Qty) as OrderQty, Max(Vmain.BillQty) as InvoiceQty, Max(VMain.BalanceQty) as BalanceQty, Max(VMain.Amount) as OrderAmount, Max(VMain.BillAmount) as InvoiceAmount, Max(VMain.BalanceAmount) as BalanceAmount
                    From (" & mQry & ") As VMain
                    GROUP By VMain.DocId, VMain.Sr  
                    Order By Max(VMain.V_Date_ActualFormat), Cast(Max(Replace(Vmain.ManualRefNo,'-','')) as Integer) "
            End If



            DsHeader = AgL.FillData(mQry, AgL.GCn)

            If DsHeader.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            ReportFrm.Text = "Sale Order Status - " + ReportFrm.FGetText(0)
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = "ProcSaleOrderStatus"

            ReportFrm.ProcFillGrid(DsHeader)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsHeader = Nothing
        End Try
    End Sub

    Public Sub ProcSaleChallanStatus(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try
            Dim mCondStr$ = ""
            Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"
            Dim mTags As String() = Nothing
            Dim J As Integer



            RepTitle = "Sale Challan Status"

            If mFilterGrid IsNot Nothing And mGridRow IsNot Nothing Then
                If mGridRow.DataGridView.Columns.Contains("Search Code") = True Then
                    If mFilterGrid.Item(GFilter, 0).Value = "Item Wise Balance" Or
                            mFilterGrid.Item(GFilter, 0).Value = "Item Wise Status" Then
                        ClsMain.FOpenForm(mGridRow.Cells("Search Code").Value, ReportFrm)
                        ReportFrm.FiterGridCopy_Arr.RemoveAt(ReportFrm.FiterGridCopy_Arr.Count - 1)
                        Exit Sub
                    End If
                Else
                    Exit Sub
                End If
            End If

            mCondStr = " Where VT.NCat In ('" & Ncat.SaleChallan & "') "
            mCondStr = mCondStr & " AND H.Div_Code = '" & AgL.PubDivCode & "' "
            mCondStr = mCondStr & " AND Date(H.V_Date) Between " & AgL.Chk_Date(CDate(ReportFrm.FGetText(1)).ToString("s")) & " And " & AgL.Chk_Date(CDate(ReportFrm.FGetText(2)).ToString("s")) & " "
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.SaleToParty", 3)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Item", 4)
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("H.Site_Code", 5), "''", "'")
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

            'If ReportFrm.FGetText(15) <> "All" Then
            '    mTags = ReportFrm.FGetText(15).ToString.Split(",")
            '    For J = 0 To mTags.Length - 1
            '        mCondStr += " And CharIndex('+' || '" & mTags(J) & "',H.Tags) > 0 "
            '    Next
            'End If
            'mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("H.Div_Code", 16), "''", "'")

            mQry = " SELECT L.DocID, L.Sr, H.V_Type, Vt.Description as VoucherType, Site.Name as Site, Div.Div_Name as Division,                    
                    strftime('%d/%m/%Y', H.V_Date) As V_Date, H.V_Date As V_Date_ActualFormat,                    
                    (Case When H.SaleToParty=H.BillToParty Then Party.Name Else BillToParty.Name || ' - ' || Party.Name End) As SaleToPartyName ,                                         
                    H.V_Type || '-' || H.ManualRefNo as ChallanNo, H.ManualRefNo, Godown.Name as GodownName,
                    I.Specification as ItemSpecification, I.Description As ItemDesc,IG.Description as ItemGroupDescription, 
                    IC.Description as ItemCategoryDescription, L.Qty as NoOfBales, L.Qty, L.Amount,  IfNull(SI.BillQty,0) as BillQty, IfNull(SI.BillAmount,0) as BillAmount, SI.BillNo,                                                           
                    (Case When L.Qty - IfNull(SI.BillQty,0) > 0  Then L.Qty - IfNull(SI.BillQty,0) Else 0 End) as BalanceQty,                                                            
                    (Case When L.Amount - IfNull(SI.BillAmount,0) > 0  Then L.Amount - IfNull(SI.BillAmount,0) Else 0 End) as BalanceAmount                                                            
                    FROM SaleInvoice H 
                    Left Join SaleInvoiceDetail L On H.DocID = L.DocID 
                    Left Join (
                                select BL.SaleInvoice, BL.SaleInvoiceSr, Sum(BL.Qty) as BillQty, Sum(BL.Amount) as BillAmount, Max(BH.V_Type) || '-' || Max(BH.ManualRefNo) As BillNo
                                From SaleInvoice BH With (NoLock)
                                Left Join SaleInvoiceDetail BL With (NoLock) On BH.DocId = BL.DocID   
                                LEFT JOIN Voucher_Type Vt On BH.V_Type = Vt.V_Type    
                                Where VT.NCat In ('" & Ncat.SaleInvoice & "')                            
                                Group By BL.SaleInvoice, BL.SaleInvoicesr
                              ) SI On L.DocID = SI.SaleInvoice And L.Sr = SI.SaleInvoiceSr
                    Left Join Item I On L.Item = I.Code 
                    Left Join Item IG On I.ItemGroup = IG.Code
                    Left Join Item IC On I.ItemCategory = IC.Code
                    Left Join viewHelpSubgroup Party On H.SaleToParty = Party.Code 
                    Left Join viewHelpSubgroup BillToParty On H.BillToParty = BillToParty.Code 
                    Left Join Subgroup Godown On L.Godown = Godown.Subcode
                    Left Join (Select SILTV.Subcode, Max(SILTV.Agent) as Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode) as LTV On Party.code = LTV.Subcode                                                           
                    LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type     
                    Left Join SiteMast Site On H.Site_Code = Site.Code
                    Left Join Division Div On H.Div_Code = Div.Div_Code
                    " & mCondStr


            If ReportFrm.FGetText(0) = "Item Wise Balance" Then
                mQry = " Select VMain.DocId As SearchCode, Max(VMain.Division) as Division, Max(Vmain.Site) as Site, Max(VMain.V_Date) As ChallanDate, Max(VMain.ChallanNo) As ChallanNo,
                    Max(VMain.SaleToPartyName) As Party, Max(VMain.GodownName) As Godown, Max(Vmain.ItemDesc) as ItemDescription,  Max(VMain.Qty) as ChallanQty
                    From (" & mQry & ") As VMain
                    GROUP By VMain.DocId, VMain.Sr  "

                mQry += "Having Max(VMain.BalanceQty) > 0 "

                mQry += "Order By Max(VMain.V_Date_ActualFormat), Cast(Max(Replace(Vmain.ManualRefNo,'-','')) as Integer) "

            ElseIf ReportFrm.FGetText(0) = "Item Wise Status" Then
                mQry = " Select VMain.DocId As SearchCode, Max(VMain.Division) as Division, Max(Vmain.Site) as Site, Max(VMain.V_Date) As ChallanDate, Max(VMain.ChallanNo) As ChallanNo,
                    Max(VMain.SaleToPartyName) As Party, Max(VMain.GodownName) As Godown, Max(Vmain.ItemDesc) as ItemDescription,  Max(VMain.Qty) as ChallanQty, Max(Vmain.BillQty) as InvoiceQty, Max(VMain.BalanceQty) as BalanceQty, Max(VMain.BillNo) as BillNo
                    From (" & mQry & ") As VMain
                    GROUP By VMain.DocId, VMain.Sr  
                    Order By Max(VMain.V_Date_ActualFormat), Cast(Max(Replace(Vmain.ManualRefNo,'-','')) as Integer) "
            End If



            DsHeader = AgL.FillData(mQry, AgL.GCn)

            If DsHeader.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            ReportFrm.Text = "Sale Challan Status - " + ReportFrm.FGetText(0)
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = "ProcSaleChallanStatus"

            ReportFrm.ProcFillGrid(DsHeader)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsHeader = Nothing
        End Try
    End Sub


    Public Sub ProcSaleReport(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try
            Dim mCondStr$ = ""
            Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"
            Dim mTags As String() = Nothing
            Dim J As Integer



            RepTitle = "Sale Invoice Report"

            If mFilterGrid IsNot Nothing And mGridRow IsNot Nothing Then
                If mGridRow.DataGridView.Columns.Contains("Search Code") = True Then
                    If mFilterGrid.Item(GFilter, 0).Value = "Month Wise Summary" Then
                        mFilterGrid.Item(GFilter, 0).Value = "Doc.Header Wise Detail"
                        mFilterGrid.Item(GFilter, 1).Value = AgL.RetMonthStartDate(CDate(mGridRow.Cells("Month").Value))
                        mFilterGrid.Item(GFilter, 2).Value = AgL.RetMonthEndDate(CDate(mGridRow.Cells("Month").Value))
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = "Party Wise Summary" Then
                        mFilterGrid.Item(GFilter, 0).Value = "Doc.Header Wise Detail"
                        mFilterGrid.Item(GFilter, 3).Value = mGridRow.Cells("Party").Value
                        mFilterGrid.Item(GFilterCode, 3).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = "Account Type Wise Summary" Then
                        mFilterGrid.Item(GFilter, 0).Value = "Doc.Header Wise Detail"
                        mFilterGrid.Item(GFilter, 25).Value = mGridRow.Cells("Account Type").Value
                        mFilterGrid.Item(GFilterCode, 25).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = "Account Nature Wise Summary" Then
                        mFilterGrid.Item(GFilter, 0).Value = "Doc.Header Wise Detail"
                        mFilterGrid.Item(GFilter, 26).Value = mGridRow.Cells("Account Nature").Value
                        mFilterGrid.Item(GFilterCode, 26).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = "Item Wise Summary" Then
                        mFilterGrid.Item(GFilter, 0).Value = "Item Wise Detail"
                        mFilterGrid.Item(GFilter, 4).Value = mGridRow.Cells("Item").Value
                        mFilterGrid.Item(GFilterCode, 4).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = "Voucher Type Wise Summary" Then
                        mFilterGrid.Item(GFilter, 0).Value = "Doc.Header Wise Detail"
                        mFilterGrid.Item(GFilter, 6).Value = mGridRow.Cells("Voucher Type").Value
                        mFilterGrid.Item(GFilterCode, 6).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = "Agent Wise Summary" Then
                        mFilterGrid.Item(GFilter, 0).Value = "Doc.Header Wise Detail"
                        mFilterGrid.Item(GFilter, 8).Value = mGridRow.Cells("Agent").Value
                        mFilterGrid.Item(GFilterCode, 8).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = "Item Group Wise Summary" Then
                        mFilterGrid.Item(GFilter, 0).Value = "Item Wise Detail"
                        mFilterGrid.Item(GFilter, 9).Value = mGridRow.Cells("Item Group").Value
                        mFilterGrid.Item(GFilterCode, 9).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = "Department Wise Summary" Then
                        mFilterGrid.Item(GFilter, 0).Value = "Item Wise Detail"
                        mFilterGrid.Item(GFilter, 27).Value = mGridRow.Cells("Department").Value
                        mFilterGrid.Item(GFilterCode, 27).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = "Item Category Wise Summary" Then
                        mFilterGrid.Item(GFilter, 0).Value = "Item Wise Detail"
                        mFilterGrid.Item(GFilter, 10).Value = mGridRow.Cells("Item Category").Value
                        mFilterGrid.Item(GFilterCode, 10).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = "City Wise Summary" Then
                        mFilterGrid.Item(GFilter, 0).Value = "Doc.Header Wise Detail"
                        mFilterGrid.Item(GFilter, 12).Value = mGridRow.Cells("City").Value
                        mFilterGrid.Item(GFilterCode, 12).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = "Area Wise Summary" Then
                        mFilterGrid.Item(GFilter, 0).Value = "Doc.Header Wise Detail"
                        mFilterGrid.Item(GFilter, 12).Value = mGridRow.Cells("Area").Value
                        mFilterGrid.Item(GFilterCode, 12).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = "State Wise Summary" Then
                        mFilterGrid.Item(GFilter, 0).Value = "Doc.Header Wise Detail"
                        mFilterGrid.Item(GFilter, 13).Value = mGridRow.Cells("State").Value
                        mFilterGrid.Item(GFilterCode, 13).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = "Sales Representative Wise Summary" Then
                        mFilterGrid.Item(GFilter, 0).Value = "Doc.Header Wise Detail"
                        mFilterGrid.Item(GFilter, 14).Value = mGridRow.Cells("Sales Representative").Value
                        mFilterGrid.Item(GFilterCode, 14).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = "User Wise Summary" Then
                        mFilterGrid.Item(GFilter, 0).Value = "Doc.Header Wise Detail"
                        mFilterGrid.Item(GFilter, 18).Value = mGridRow.Cells("User Name").Value
                        mFilterGrid.Item(GFilterCode, 18).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = "Responsible Person Wise Summary" Then
                        mFilterGrid.Item(GFilter, 0).Value = "Doc.Header Wise Detail"
                        mFilterGrid.Item(GFilter, 15).Value = mGridRow.Cells("Responsible Person").Value
                        mFilterGrid.Item(GFilterCode, 15).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = "HSN Wise Summary" Then
                        mFilterGrid.Item(GFilter, 0).Value = "Item Wise Detail"
                        mFilterGrid.Item(GFilter, 19).Value = mGridRow.Cells("HSN").Value
                        mFilterGrid.Item(GFilterCode, 19).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = "Party Tax Group Wise Summary" Then
                        mFilterGrid.Item(GFilter, 0).Value = "Doc.Header Wise Detail"
                        mFilterGrid.Item(GFilter, 20).Value = mGridRow.Cells("Party Tax Group").Value
                        mFilterGrid.Item(GFilterCode, 20).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = "Item Tax Group Wise Summary" Then
                        mFilterGrid.Item(GFilter, 0).Value = "Doc.Header Wise Detail"
                        mFilterGrid.Item(GFilter, 21).Value = mGridRow.Cells("Item Tax Group").Value
                        mFilterGrid.Item(GFilterCode, 21).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = "Catalog Wise Summary" Then
                        mFilterGrid.Item(GFilter, 0).Value = "Doc.Header Wise Detail"
                        mFilterGrid.Item(GFilter, 22).Value = mGridRow.Cells("Catalog").Value
                        mFilterGrid.Item(GFilterCode, 22).Value = "'" + mGridRow.Cells("Search Code").Value + "'"

                    ElseIf mFilterGrid.Item(GFilter, 0).Value = "Site Wise Summary" Then
                        mFilterGrid.Item(GFilter, 0).Value = "Doc.Header Wise Detail"
                        mFilterGrid.Item(GFilter, 5).Value = mGridRow.Cells("Site").Value
                        mFilterGrid.Item(GFilterCode, 5).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = "Division Wise Summary" Then
                        mFilterGrid.Item(GFilter, 0).Value = "Doc.Header Wise Detail"
                        mFilterGrid.Item(GFilter, 17).Value = mGridRow.Cells("Division").Value
                        mFilterGrid.Item(GFilterCode, 17).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = "Doc.Header Wise Detail" Or
                            mFilterGrid.Item(GFilter, 0).Value = "Item Wise Detail" Then

                        ClsMain.FOpenForm(mGridRow.Cells("Search Code").Value, ReportFrm)
                        ReportFrm.FiterGridCopy_Arr.RemoveAt(ReportFrm.FiterGridCopy_Arr.Count - 1)

                        Exit Sub
                    End If
                Else
                    Exit Sub
                End If
            End If

            If GRepFormName = SaleOrderReport Then
                mCondStr = " Where VT.NCat In ('" & Ncat.SaleOrder & "', '" & Ncat.SaleOrderCancel & "') "
            Else
                mCondStr = " Where VT.NCat In ('" & Ncat.SaleInvoice & "', '" & Ncat.SaleReturn & "') "
            End If
            'mCondStr = mCondStr & " AND H.Div_Code = '" & AgL.PubDivCode & "' "
            mCondStr = mCondStr & " AND Date(H.V_Date) Between " & AgL.Chk_Date(CDate(ReportFrm.FGetText(1)).ToString("s")) & " And " & AgL.Chk_Date(CDate(ReportFrm.FGetText(2)).ToString("s")) & " "
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.SaleToParty", 3)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Item", 4)
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("H.Site_Code", 5), "''", "'")
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.V_Type", 6)
            If ReportFrm.FGetText(7) = "Cash" Then
                mCondStr = mCondStr & " AND BillToParty.Nature = 'Cash'"
            ElseIf ReportFrm.FGetText(7) = "Credit" Then
                mCondStr = mCondStr & " AND BillToParty.Nature <> 'Cash'"
            End If
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("LTV.Agent", 8)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemGroup", 9)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemCategory", 10)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemType", 11)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("City.CityCode", 12)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("City.State", 13)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.SalesRepresentative", 14)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.ResponsiblePerson", 15)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.EntryBy", 18)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Catalog", 22)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("DS.CityCode", 23)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.ItemState", 24)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("Party.SubgroupType", 25)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("Party.Nature", 26)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("IG.Department", 27)

            'If ReportFrm.FGetText(8) <> "All" Then
            '    mCondStr += " And H.Agent = '" & ReportFrm.FGetCode(8) & "' "
            'End If

            'mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.Agent", 8)

            If ReportFrm.FGetText(16) <> "All" Then
                mTags = ReportFrm.FGetText(16).ToString.Split(",")
                For J = 0 To mTags.Length - 1
                    mCondStr += " And CharIndex('+' || '" & mTags(J) & "',H.Tags) > 0 "
                Next
            End If
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("H.Div_Code", 17), "''", "'")
            If AgL.XNull(ReportFrm.FGetText(19)) <> "All" Then
                mCondStr = mCondStr & " And IfNull(IfNull(IfNull(I.HSN,IC.HSN),Bi.HSN),'') = '" & AgL.XNull(ReportFrm.FGetText(19)) & "' "
            End If
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.SalesTaxGroupParty", 20)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.SalesTaxGroupItem", 21)


            mQry = " SELECT H.DocID, H.V_Type, Vt.Description as VoucherType, H.Site_Code, H.Div_Code, Site.Name as Site, Div.Div_Name as Division,
                    (Select Case When Vt1.NCat = 'SO' Then S1.ManualRefNo Else Null End From SaleInvoice S1 Left Join Voucher_Type Vt1 On S1.V_Type = Vt1.V_Type Where S1.DocID = L.SaleInvoice) as OrderNo, 
                    strftime('%d/%m/%Y', H.V_Date) As V_Date, H.V_Date As V_Date_ActualFormat,
                    H.SaleToParty, Party.SubgroupType, Party.Nature as AccountNature, I.ItemGroup, I.ItemCategory,
                    (Case When H.SaleToParty=H.BillToParty And (Party.Nature='Cash' Or Party.SubgroupType='" & SubgroupType.RevenuePoint & "') Then Party.Name || ' - ' || IfNull(H.SaleToPartyName,'') When H.SaleToParty=H.BillToParty Then Party.Name When BillToParty.Nature='Cash' And H.SaleToParty<>H.BillToParty Then  BillToParty.Name || ' - ' || Party.Name  Else Party.Name || ' - ' || BillToParty.Name End) As SaleToPartyName ,                     
                    Party.Mobile, SIT.NoOfBales,
                    LTV.Agent As AgentCode, Agent.Name As AgentName, H.ResponsiblePerson, ResponsiblePerson.Name as ResponsiblePersonName,G.Name as GodownName,
                    L.SalesRepresentative, SalesRep.Name as SalesRepresentativeName, H.SalesTaxGroupParty, L.SalesTaxGroupItem,
                    City.CityCode, City.CityName, Area.Code As AreaCode, Area.Description As AreaName, State.Code As StateCode, State.Description As StateName,
                    Cast(Replace(H.ManualRefNo,'-','') as Integer) as InvoiceNo, H.ManualRefNo, L.Item,
                    I.Specification as ItemSpecification, I.Description As ItemDesc, IfNull(IfNull(I.HSN,IC.HSN),Bi.HSN) as HSN,IG.Description as ItemGroupDescription, IC.Description as ItemCategoryDescription,  
                    I.PurchaseRate, L.Catalog, Catalog.Description as CatalogDesc, IG.Department, Department.Description as DepartmentDesc,
                    (Case When L.DiscountPer = 0 Then '' else Cast(L.DiscountPer as nVarchar) End)  || (Case When L.AdditionalDiscountPer>0 Then '+' else '' End) || (Case When L.AdditionalDiscountPer=0 Then '' else Cast(L.AdditionalDiscountPer as nVarchar) End)  as DiscountPer, 
                    L.DiscountAmount as Discount, L.AdditionalDiscountAmount as AdditionalDiscount, L.AdditionAmount as Addition, 
                    L.SpecialDiscount_Per, L.SpecialDiscount, L.SpecialAddition_Per, L.SpecialAddition, 
                    L.Taxable_Amount, (Case When L.Net_Amount=0 Then L.Amount Else L.Net_Amount End) as Net_Amount, L.Qty, L.Unit, L.DealQty, L.DealUnit, L.Rate, L.Amount +(L.DiscountAmount + L.AdditionalDiscountAmount - L.AdditionAmount) as AmountExDiscount, L.Amount,
                    L.Tax1, L.Tax2, L.Tax3, L.Tax4, L.Tax5, L.Tax1+L.Tax2+L.Tax3+L.Tax4+L.Tax5 as TotalTax, H.EntryBy as EntryByUser,
                    H.Tags,
                    (select Max(Tags) From SaleInvoice Where DocId In (Select SaleInvoice From SaleInvoiceDetail Where DocId=H.DocID)) as OrderTags,
                    (Select Max(I1.Description) from SaleInvoiceDetailSKU DSKU Left Join Item I1 On IfNull(DSKU.ItemGroup, DSKU.Item) = I1.Code Where DSKU.DocID = H.DocID And I1.V_Type='IG') as Brand 
                    FROM SaleInvoice H 
                    Left Join SaleInvoiceTransport SIT On H.DocID = SIT.DocID
                    Left Join SaleInvoiceDetail L On H.DocID = L.DocID 
                    Left Join SaleInvoiceDetailSku LS On L.DocID = LS.DocID And LS.Sr = L.Sr
                    Left Join Item I On L.Item = I.Code 
                    Left Join Item IG On LS.ItemGroup = IG.Code
                    Left Join Item IC On I.ItemCategory = IC.Code
                    LEFT JOIN Item Bi On I.BaseItem = Bi.Code
                    Left Join viewHelpSubgroup Party On H.SaleToParty = Party.Code 
                    Left Join viewHelpSubgroup BillToParty On H.BillToParty = BillToParty.Code 
                    Left Join (Select SILTV.Subcode, SILTV.Div_Code, SILTV.Site_Code, Max(SILTV.Agent) as Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode, SILTV.Div_Code, SILTV.Site_Code) as LTV On Party.code = LTV.Subcode And H.Site_Code = LTV.Site_Code And H.Div_Code = LTV.Div_Code
                    Left Join viewHelpSubGroup Agent On LTV.Agent = Agent.Code 
                    Left Join viewHelpSubGroup SalesRep On L.SalesRepresentative = SalesRep.Code 
                    Left Join viewHelpSubGroup ResponsiblePerson On H.ResponsiblePerson = ResponsiblePerson.Code 
                    Left Join SubGroup G With (NoLock) on L.Godown  = G.Subcode 
                    Left Join City On H.SaleToPartyCity = City.CityCode 
                    Left Join Area On Party.Area = Area.Code 
                    Left Join State On City.State = State.Code                    
                    LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type     
                    Left Join SiteMast Site On H.Site_Code = Site.Code
                    Left Join Division Div On H.Div_Code = Div.Div_Code
                    Left Join Catalog On L.Catalog = Catalog.Code
                    Left Join Subgroup DS On IG.DefaultSupplier = Ds.Subcode 
                    Left join Department On IG.Department = Department.Code                   
                    " & mCondStr


            If ReportFrm.FGetText(0) = "Doc.Header Wise Detail" Then
                If GRepFormName = SaleOrderReport Then
                    mQry = " Select VMain.DocId As SearchCode, Max(VMain.Division) as Division, Max(Vmain.Site) as Site, Max(VMain.V_Date) As OrderDate, Max(VMain.V_Type) as DocType, Max(VMain.InvoiceNo) As OrderNo,
                    Max(VMain.SaleToPartyName) As Party, Max(Vmain.Brand) as Brand, IfNull(Sum(VMain.AmountExDiscount),0) As AmountExDiscount, IfNull(Sum(VMain.Discount + VMain.AdditionalDiscount),0) As Discount, IfNull(Sum(VMain.Addition),0) as Addition, IfNull(Sum(VMain.SpecialDiscount),0) As SpecialDiscount, IfNull(Sum(VMain.SpecialAddition),0) As SpecialAddition,
                    IfNull(Sum(VMain.Amount),0) As Amount,IfNull(Sum(VMain.Taxable_Amount),0) As TaxableAmount, IfNull(Sum(VMain.TotalTax),0) As TaxAmount, IfNull(Sum(VMain.Net_Amount),0) As NetAmount
                    From (" & mQry & ") As VMain
                    GROUP By VMain.DocId 
                    Order By Max(VMain.V_Date_ActualFormat), Cast(Max(Replace(Vmain.ManualRefNo,'-','')) as Integer) "
                Else
                    If (AgL.PubServerName <> "") Then
                        mQry = " Select VMain.DocId As SearchCode, Max(VMain.Division) as Division, Max(Vmain.Site) as Site, Row_Number() OVER (ORDER BY Max(VMain.V_Date_ActualFormat),Cast(Max(Replace(Vmain.ManualRefNo,'-','')) as Integer),VMain.DocId) AS Sr,
                                Max(VMain.V_Date) As InvoiceDate, Max(VMain.V_Type) as DocType, Max(VMain.InvoiceNo) As InvoiceNo, Max(VMain.NoOfBales) AS NoOfBales, Max(VMain.GodownName) AS GodownName,
                                Max(VMain.SaleToPartyName) As Party, Max(Vmain.Brand) as Brand, Max(VMain.SalesTaxGroupParty) As SalesTaxGroupParty, IfNull(Sum(VMain.AmountExDiscount),0) As AmountExDiscount, 
                                IfNull(Sum(VMain.Discount+VMain.AdditionalDiscount),0) As Discount, IfNull(Sum(VMain.Addition),0) as Addition, IfNull(Sum(VMain.SpecialDiscount),0) As SpecialDiscount, IfNull(Sum(VMain.SpecialAddition),0) As SpecialAddition,
                                IfNull(Sum(VMain.Amount),0) As Amount,IfNull(Sum(VMain.Taxable_Amount),0) As TaxableAmount, IfNull(Sum(VMain.TotalTax),0) As TaxAmount, IfNull(Sum(VMain.Net_Amount),0) As NetAmount, Max(VMain.Tags) as Tags, Max(VMain.OrderTags) as OrderTags
                                From (" & mQry & ") As VMain
                                GROUP By VMain.DocId 
                                Order By Max(VMain.V_Date_ActualFormat), Cast(Max(Replace(Vmain.ManualRefNo,'-','')) as Integer),VMain.DocId "
                    Else
                        mQry = " Select VMain.DocId As SearchCode, Max(VMain.Division) as Division, Max(Vmain.Site) as Site, 
                                Max(VMain.V_Date) As InvoiceDate, Max(VMain.V_Type) as DocType, Max(VMain.InvoiceNo) As InvoiceNo, IfNull(Cast(Max(VMain.NoOfBales) as Integer),0) AS NoOfBales, Max(VMain.GodownName) AS GodownName,
                                Max(VMain.SaleToPartyName) As Party, Max(Vmain.Brand) as Brand, Max(VMain.SalesTaxGroupParty) As SalesTaxGroupParty, IfNull(Sum(VMain.AmountExDiscount),0) As AmountExDiscount, 
                                IfNull(Sum(VMain.Discount+VMain.AdditionalDiscount),0) As Discount, IfNull(Sum(VMain.Addition),0) as Addition, IfNull(Sum(VMain.SpecialDiscount),0) As SpecialDiscount, IfNull(Sum(VMain.SpecialAddition),0) As SpecialAddition,
                                IfNull(Sum(VMain.Amount),0) As Amount,IfNull(Sum(VMain.Taxable_Amount),0) As TaxableAmount, IfNull(Sum(VMain.TotalTax),0) As TaxAmount, IfNull(Sum(VMain.Net_Amount),0) As NetAmount, Max(VMain.Tags) as Tags, Max(VMain.OrderTags) as OrderTags
                                From (" & mQry & ") As VMain
                                GROUP By VMain.DocId 
                                Order By Max(VMain.V_Date_ActualFormat), Cast(Max(Replace(Vmain.ManualRefNo,'-','')) as Integer) "
                    End If
                End If
            ElseIf ReportFrm.FGetText(0) = "All Addition" Or ReportFrm.FGetText(0) = "Un-Adjusted Addition" Then
                If GRepFormName = SaleOrderReport Then
                    mQry = " Select VMain.DocId As SearchCode, Max(VMain.Division) as Division, Max(Vmain.Site) as Site, Max(VMain.V_Date) As OrderDate, Max(VMain.V_Type) as DocType, Max(VMain.InvoiceNo) As OrderNo,
                                Max(VMain.SaleToPartyName) As Party, Max(Vmain.Brand) as Brand, IfNull(Sum(VMain.AmountExDiscount),0) As AmountExDiscount, IfNull(Sum(VMain.Discount + VMain.AdditionalDiscount),0) As Discount, IfNull(Sum(VMain.Addition),0) as Addition, IfNull(Sum(VMain.SpecialDiscount),0) As SpecialDiscount, IfNull(Sum(VMain.SpecialAddition),0) As SpecialAddition,
                                IfNull(Sum(VMain.Amount),0) As Amount,IfNull(Sum(VMain.Taxable_Amount),0) As TaxableAmount, IfNull(Sum(VMain.TotalTax),0) As TaxAmount, IfNull(Sum(VMain.Net_Amount),0) As NetAmount
                                From (" & mQry & ") As VMain
                                GROUP By VMain.DocId 
                                Order By Max(VMain.V_Date_ActualFormat), Cast(Max(Replace(Vmain.ManualRefNo,'-','')) as Integer) "
                Else
                    If (AgL.PubServerName <> "") Then
                        mQry = " Select VMain.DocId As SearchCode, Max(VMain.Division) as Division, Max(Vmain.Site) as Site, Row_Number() OVER (ORDER BY Max(VMain.V_Date_ActualFormat),Cast(Max(Replace(Vmain.ManualRefNo,'-','')) as Integer),VMain.DocId) AS Sr,
                                Max(VMain.V_Date) As InvoiceDate, Max(VMain.V_Type) as DocType, Max(VMain.InvoiceNo) As InvoiceNo, Max(VMain.NoOfBales) AS NoOfBales,
                                Max(VMain.SaleToPartyName) As Party, Max(Vmain.Brand) as Brand, Max(VMain.SalesTaxGroupParty) As SalesTaxGroupParty, IfNull(Sum(VMain.AmountExDiscount),0) As AmountExDiscount, 
                                IfNull(Sum(VMain.Discount+VMain.AdditionalDiscount),0) As Discount, IfNull(Sum(VMain.Addition),0) as Addition, -isnull(Max(CNAmount),0) CreditNoteAmt, IsNull(Sum(VMain.Addition),0)+isnull(Max(CNAmount),0) BalAddForCreditNote,
                                IfNull(Sum(VMain.SpecialDiscount),0) As SpecialDiscount, IfNull(Sum(VMain.SpecialAddition),0) As SpecialAddition,
                                IfNull(Sum(VMain.Amount),0) As Amount,IfNull(Sum(VMain.Taxable_Amount),0) As TaxableAmount, IfNull(Sum(VMain.TotalTax),0) As TaxAmount, IfNull(Sum(VMain.Net_Amount),0) As NetAmount, Max(VMain.Tags) as Tags, Max(VMain.OrderTags) as OrderTags
                                From (" & mQry & ") As VMain
                                LEFT JOIN 
                                (
                                SELECT L.SpecificationDocID, Sum(L.Amount) AS CNAmount 
					            FROM LedgerHead H 
					            LEFT JOIN LedgerHeadDetail L ON L.DocID = H.DocID
					            WHERE H.V_Type ='CNC' AND L.SpecificationDocID IS NOT NULL 
					            GROUP BY L.SpecificationDocID 
                                ) AS CNC ON CNC.SpecificationDocID = VMain.DocID  
                                GROUP By VMain.DocId "
                        mQry = mQry + " HAVING IfNull(Sum(VMain.Addition),0) > 0 "

                        If ReportFrm.FGetText(0) = "Un-Adjusted Addition" Then
                            mQry = mQry + " And IsNull(Sum(VMain.Addition),0)+isnull(Max(CNAmount),0) > 0 "
                        End If

                        mQry = mQry + "Order By Max(VMain.V_Date_ActualFormat), Cast(Max(Replace(Vmain.ManualRefNo,'-','')) as Integer),VMain.DocId "
                    Else
                        mQry = " Select VMain.DocId As SearchCode, Max(VMain.Division) as Division, Max(Vmain.Site) as Site, 
                                Max(VMain.V_Date) As InvoiceDate, Max(VMain.V_Type) as DocType, Max(VMain.InvoiceNo) As InvoiceNo, IfNull(Cast(Max(VMain.NoOfBales) as Integer),0) AS NoOfBales,
                                Max(VMain.SaleToPartyName) As Party, Max(Vmain.Brand) as Brand, Max(VMain.SalesTaxGroupParty) As SalesTaxGroupParty, IfNull(Sum(VMain.AmountExDiscount),0) As AmountExDiscount, 
                                IfNull(Sum(VMain.Discount+VMain.AdditionalDiscount),0) As Discount, IfNull(Sum(VMain.Addition),0) as Addition, IfNull(Sum(VMain.SpecialDiscount),0) As SpecialDiscount, IfNull(Sum(VMain.SpecialAddition),0) As SpecialAddition,
                                IfNull(Sum(VMain.Amount),0) As Amount,IfNull(Sum(VMain.Taxable_Amount),0) As TaxableAmount, IfNull(Sum(VMain.TotalTax),0) As TaxAmount, IfNull(Sum(VMain.Net_Amount),0) As NetAmount, Max(VMain.Tags) as Tags, Max(VMain.OrderTags) as OrderTags
                                From (" & mQry & ") As VMain
                                GROUP By VMain.DocId 
                                Order By Max(VMain.V_Date_ActualFormat), Cast(Max(Replace(Vmain.ManualRefNo,'-','')) as Integer) "
                    End If
                End If
            ElseIf ReportFrm.FGetText(0) = "Item Wise Detail" Then
                If GRepFormName = SaleOrderReport Then
                    mQry = " Select VMain.DocId As SearchCode, Max(Vmain.Site) as Site, Max(VMain.Division) as Division, Max(VMain.V_Date) As [Order Date], Max(VMain.V_Type) as DocType, Max(VMain.InvoiceNo) As [Order No],
                    Max(VMain.SaleToPartyName) As Party, Max(VMain.ItemDesc) As Item, Sum(VMain.Qty) As Qty, Max(VMain.Unit) As Unit, Max(VMain.HSN) As HSN, 
                    Max(VMain.DealQty)  as DealQty, Max(VMain.DealUnit) as DealUnit,
                    Max(VMain.Rate) As Rate,
                    Sum(VMain.AmountExDiscount) As AmountExDiscount, Max(VMain.DiscountPer) As [Discount Per], 
                    Sum(VMain.Discount) As Discount,
                    Sum(VMain.AdditionalDiscount) As AdditionalDiscount,
                    Sum(VMain.Addition) As Addition,
                    Max(VMain.SpecialDiscount_Per) As [Sp Disc Per], 
                    Sum(VMain.SpecialDiscount) As [Sp Disc],        
                    Max(VMain.SpecialAddition_Per) As [Sp Addition Per], 
                    Sum(VMain.SpecialAddition) As [Sp Addition],        
                    Sum(VMain.Amount) As [Amount],
                    Sum(VMain.Taxable_Amount) As [Taxable Amount],
                    Sum(VMain.TotalTax) As [Tax Amount],
                    Sum(VMain.Net_Amount) As [Net Amount]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.DocId, VMain.Item 
                    Order By  Max(VMain.V_Date_ActualFormat), Cast(Max(Replace(Vmain.ManualRefNo,'-','')) as Integer) "
                Else
                    mQry = " Select VMain.DocId As SearchCode, Max(Vmain.Site) as Site, Max(VMain.Division) as Division, Max(VMain.V_Date) As [Invoice Date], Max(VMain.V_Type) as DocType, Max(VMain.InvoiceNo) As [Invoice No],
                    Max(VMain.SaleToPartyName) As Party, Max(VMain.ItemDesc) As Item, Max(VMain.ItemGroupDescription) as ItemGroup, Max(VMain.OrderNo) as [Order No], Sum(VMain.Qty) As Qty, Max(VMain.Unit) As Unit, Max(VMain.HSN) As HSN, 
                    Max(VMain.DealQty)  as DealQty, Max(VMain.DealUnit) as DealUnit,
                    Max(VMain.Rate) As Rate,
                    Sum(VMain.AmountExDiscount) As Amount, Max(VMain.DiscountPer) As [Discount Per], 
                    Sum(VMain.Discount) As Discount,
                    Sum(VMain.AdditionalDiscount) As AdditionalDiscount,
                    Sum(VMain.Addition) As Addition,
                    Max(VMain.SpecialDiscount_Per) As [Sp Disc Per], 
                    Sum(VMain.SpecialDiscount) As [Sp Disc],        
                    Max(VMain.SpecialAddition_Per) As [Sp Addition Per], 
                    Sum(VMain.SpecialAddition) As [Sp Addition],        
                    Sum(VMain.Amount) As Amount, 
                    Sum(VMain.Taxable_Amount) As [Taxable Amount],
                    Sum(VMain.TotalTax) As [Tax Amount],
                    Sum(VMain.Net_Amount) As [Net Amount]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.DocId, VMain.Item 
                    Order By  Max(VMain.V_Date_ActualFormat), Cast(Max(Replace(Vmain.ManualRefNo,'-','')) as Integer) "
                End If
            ElseIf ReportFrm.FGetText(0) = "Voucher Type Wise Summary" Then
                mQry = " Select VMain.V_Type as SearchCode, Max(VMain.VoucherType) As VoucherType, 
                    Count(Distinct Vmain.DocID) as [Doc.Count], Sum(VMain.Qty) as Qty,
                    Sum(VMain.AmountExDiscount) as GoodsValue, Sum(VMain.Discount) as Discount, Sum(VMain.Addition) as Addition, Sum(VMain.SpecialDiscount) as SpecialDiscount, Sum(VMain.SpecialAddition) as SpecialAddition,
                    Sum(VMain.Amount) As Amount, Sum(VMain.Taxable_Amount) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount, Sum(VMain.Net_Amount) As [Net Amount]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.V_Type
                    Order By Max(VMain.VoucherType)"
            ElseIf ReportFrm.FGetText(0) = "Party Wise Summary" Then
                mQry = " Select VMain.SaleToParty as SearchCode, Max(VMain.SaleToPartyName) As Party, Max(Vmain.Mobile) as Mobile,
                    Count(Distinct Vmain.DocID) as [Doc.Count],  Sum(VMain.Qty) as Qty,
                    Sum(VMain.AmountExDiscount) as GoodsValue, Sum(VMain.Discount) as Discount, Sum(VMain.Addition) as Addition, Sum(VMain.SpecialDiscount) as SpecialDiscount, Sum(VMain.SpecialAddition) as SpecialAddition,
                    Sum(VMain.Amount) As Amount, Sum(VMain.Taxable_Amount) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount, Sum(VMain.Net_Amount) As [Net Amount]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.SaleToParty 
                    Order By Max(VMain.SaleToPartyName)"
            ElseIf ReportFrm.FGetText(0) = "Account Wise Summary" Then
                If GRepFormName = SaleOrderReport Then
                    mCondStr = " Where VT.NCat In ('" & Ncat.SaleOrder & "', '" & Ncat.SaleOrderCancel & "') "
                Else
                    mCondStr = " Where VT.NCat In ('" & Ncat.SaleInvoice & "', '" & Ncat.SaleReturn & "') "
                End If
                mCondStr = mCondStr & " AND Date(H.V_Date) Between " & AgL.Chk_Date(CDate(ReportFrm.FGetText(1)).ToString("s")) & " And " & AgL.Chk_Date(CDate(ReportFrm.FGetText(2)).ToString("s")) & " "
                mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("H.Site_Code", 5), "''", "'")
                mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.V_Type", 6)

                mQry = " SELECT Max(SG.Name) AS Account, Sum(LG.Amount) AS Amount                      
                        FROM SaleInvoice H  
                        LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                        LEFT JOIN SaleInvoicePayment LG ON LG.DocId = H.DocID
                        LEFT JOIN Subgroup SG ON SG.Subcode = LG.PostToAc 
                        " & mCondStr & " GROUP BY LG.PostToAc "
            ElseIf ReportFrm.FGetText(0) = "Account Type Wise Summary" Then
                mQry = " Select VMain.SubgroupType as SearchCode, Max(VMain.SubgroupType) As AccountType, 
                    Count(Distinct Vmain.DocID) as [Doc.Count],  Sum(VMain.Qty) as Qty,
                    Sum(VMain.AmountExDiscount) as GoodsValue, Sum(VMain.Discount) as Discount, Sum(VMain.Addition) as Addition, Sum(VMain.SpecialDiscount) as SpecialDiscount, Sum(VMain.SpecialAddition) as SpecialAddition,
                    Sum(VMain.Amount) As Amount, Sum(VMain.Taxable_Amount) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount, Sum(VMain.Net_Amount) As [Net Amount]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.SubgroupType 
                    Order By Max(VMain.SubgroupType)"
            ElseIf ReportFrm.FGetText(0) = "Account Nature Wise Summary" Then
                mQry = " Select VMain.AccountNature as SearchCode, Max(VMain.AccountNature) As AccountNature, 
                    Count(Distinct Vmain.DocID) as [Doc.Count],  Sum(VMain.Qty) as Qty,
                    Sum(VMain.AmountExDiscount) as GoodsValue, Sum(VMain.Discount) as Discount, Sum(VMain.Addition) as Addition, Sum(VMain.SpecialDiscount) as SpecialDiscount, Sum(VMain.SpecialAddition) as SpecialAddition,
                    Sum(VMain.Amount) As Amount, Sum(VMain.Taxable_Amount) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount, Sum(VMain.Net_Amount) As [Net Amount]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.AccountNature 
                    Order By Max(VMain.AccountNature)"

            ElseIf ReportFrm.FGetText(0) = "Sales Representative Wise Summary" Then
                mQry = " Select VMain.SalesRepresentative as SearchCode, Max(VMain.SalesRepresentativeName) As SalesRepresentative, 
                    Count(Distinct Vmain.DocID) as [Doc.Count],  Count(Distinct VMain.V_Date) as DaysCount,  Sum(VMain.Qty) as Qty,
                    Sum(VMain.AmountExDiscount) as GoodsValue, Sum(VMain.Discount) as Discount, Sum(VMain.Addition) as Addition, Sum(VMain.SpecialDiscount) as SpecialDiscount, Sum(VMain.SpecialAddition) as SpecialAddition,
                    Sum(VMain.Amount) As Amount, Sum(VMain.Taxable_Amount) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount, Sum(VMain.Net_Amount) As [Net Amount]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.SalesRepresentative 
                    Order By Max(VMain.SalesRepresentativeName)"
            ElseIf ReportFrm.FGetText(0) = "Responsible Person Wise Summary" Then
                mQry = " Select VMain.ResponsiblePerson as SearchCode, Max(VMain.ResponsiblePersonName) As ResponsiblePerson,
                    Count(Distinct Vmain.DocID) as [Doc.Count],  Count(Distinct VMain.V_Date) as DaysCount,  Sum(VMain.Qty) as Qty,
                    Sum(VMain.AmountExDiscount) as GoodsValue, Sum(VMain.Discount) as Discount, Sum(VMain.Addition) as Addition, Sum(VMain.SpecialDiscount) as SpecialDiscount, Sum(VMain.SpecialAddition) as SpecialAddition,
                    Sum(VMain.Amount) As Amount, Sum(VMain.Taxable_Amount) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount, Sum(VMain.Net_Amount) As [Net Amount]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.ResponsiblePerson 
                    Order By Max(VMain.ResponsiblePersonName)"
            ElseIf ReportFrm.FGetText(0) = "User Wise Summary" Then
                mQry = " Select VMain.EntryByUser as SearchCode, Max(VMain.EntryByUser) As UserName,
                    Count(Distinct Vmain.DocID) as [Doc.Count],  Count(Distinct VMain.V_Date) as DaysCount,  Sum(VMain.Qty) as Qty,
                    Sum(VMain.AmountExDiscount) as GoodsValue, Sum(VMain.Discount) as Discount, Sum(VMain.Addition) as Addition, Sum(VMain.SpecialDiscount) as SpecialDiscount, Sum(VMain.SpecialAddition) as SpecialAddition,
                    Sum(VMain.Amount) As Amount, Sum(VMain.Taxable_Amount) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount, Sum(VMain.Net_Amount) As [Net Amount]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.EntryByUser
                    Order By Max(VMain.EntryByUser)"
            ElseIf ReportFrm.FGetText(0) = "Catalog Wise Summary" Then
                mQry = " Select VMain.Catalog as SearchCode, Max(VMain.CatalogDesc) As Catalog, 
                    Count(Distinct Vmain.DocID) as [Doc.Count],  Sum(VMain.Qty) as Qty,
                    Sum(VMain.AmountExDiscount) as GoodsValue, Sum(VMain.Discount) as Discount, Sum(VMain.Addition) as Addition, Sum(VMain.SpecialDiscount) as SpecialDiscount, Sum(VMain.SpecialAddition) as SpecialAddition,
                    Sum(VMain.Amount) As Amount, Sum(VMain.Taxable_Amount) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount, Sum(VMain.Net_Amount) As [Net Amount]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.Catalog
                    Order By Max(VMain.CatalogDesc)"
            ElseIf ReportFrm.FGetText(0) = "Department Wise Summary" Then
                mQry = " Select VMain.Department as SearchCode, Max(VMain.DepartmentDesc) As Department, 
                    Count(Distinct Vmain.DocID) as [Doc.Count],  Sum(VMain.Qty) as Qty,
                    Sum(VMain.AmountExDiscount) as GoodsValue, Sum(VMain.Discount) as Discount, Sum(VMain.Addition) as Addition, Sum(VMain.SpecialDiscount) as SpecialDiscount, Sum(VMain.SpecialAddition) as SpecialAddition,
                    Sum(VMain.Amount) As Amount, Sum(VMain.Taxable_Amount) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount, Sum(VMain.Net_Amount) As [Net Amount]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.Department
                    Order By Max(VMain.DepartmentDesc)"

            ElseIf ReportFrm.FGetText(0) = "Item Wise Summary" Then
                mQry = " Select VMain.Item As SearchCode, Max(VMain.ItemDesc) As [Item],  
                    Count(Distinct Vmain.DocID) as [Doc.Count], Round(Sum(VMain.Qty),3) as Qty,
                    Sum(VMain.AmountExDiscount) as GoodsValue, Sum(VMain.Discount) as Discount, Sum(VMain.Addition) as Addition, Sum(VMain.SpecialDiscount) as SpecialDiscount, Sum(VMain.SpecialAddition) as SpecialAddition,
                    Sum(VMain.Amount) As Amount, IfNull(Sum(VMain.Taxable_Amount),0) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount, IfNull(Sum(VMain.Net_Amount),0) As [Net Amount],
                    Max(VMain.PurchaseRate) AS ItemPurchaseRate, IsNull(Sum(VMain.Taxable_Amount),0)-Max(VMain.PurchaseRate)*Sum(VMain.Qty) AS Diif
                    From (" & mQry & ") As VMain
                    GROUP By VMain.Item 
                    Order By Max(VMain.ItemDesc)"
            ElseIf ReportFrm.FGetText(0) = "HSN Wise Summary" Then
                mQry = " Select VMain.HSN As SearchCode, VMain.HSN, Max(VMain.ItemCategoryDescription) As [Description],  
                    Count(Distinct Vmain.DocID) as [Doc.Count], Round(Sum(VMain.Qty),3) as Qty,
                    Sum(VMain.AmountExDiscount) as GoodsValue, Sum(VMain.Discount) as Discount, Sum(VMain.Addition) as Addition, Sum(VMain.SpecialDiscount) as SpecialDiscount, Sum(VMain.SpecialAddition) as SpecialAddition,
                    Sum(VMain.Amount) As Amount, IfNull(Sum(VMain.Taxable_Amount),0) As [Taxable Amount], IfNull(Sum(VMain.Tax1),0) As IGST, IfNull(Sum(VMain.Tax2),0) As CGST, 
                    IfNull(Sum(VMain.Tax3),0) As SGST, IfNull(Sum(VMain.Tax4),0) As Cess, IfNull(Sum(VMain.Net_Amount),0) As [Net Amount]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.HSN, VMain.ItemCategoryDescription 
                    Order By VMain.HSN, VMain.ItemCategoryDescription"
            ElseIf ReportFrm.FGetText(0) = "Item Group Wise Summary" Then
                mQry = " Select VMain.ItemGroup as SearchCode, Max(VMain.ItemGroupDescription) As [Item Group],  
                    Count(Distinct Vmain.DocID) as [Doc.Count], Round(Sum(VMain.Qty),3) as Qty,
                    Sum(VMain.AmountExDiscount) as GoodsValue, Sum(VMain.Discount) as Discount, Sum(VMain.Addition) as Addition, Sum(VMain.SpecialDiscount) as SpecialDiscount, Sum(VMain.SpecialAddition) as SpecialAddition,
                    Sum(VMain.Amount) As Amount, IfNull(Sum(VMain.Taxable_Amount),0) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount, IfNull(Sum(VMain.Net_Amount),0) As [Net Amount]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.ItemGroup 
                    Order By Max(VMain.ItemGroupDescription)"
            ElseIf ReportFrm.FGetText(0) = "Item Category Wise Summary" Then
                mQry = " Select VMain.ItemCategory as SearchCode, Max(VMain.ItemCategoryDescription) As [Item Category],  
                    Count(Distinct Vmain.DocID) as [Doc.Count], Round(Sum(VMain.Qty),3) as Qty,
                    Sum(VMain.AmountExDiscount) as GoodsValue, Sum(VMain.Discount) as Discount, Sum(VMain.Addition) as Addition, Sum(VMain.SpecialDiscount) as SpecialDiscount, Sum(VMain.SpecialAddition) as SpecialAddition,
                    Sum(VMain.Amount) As Amount, IfNull(Sum(VMain.Taxable_Amount),0) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount, IfNull(Sum(VMain.Net_Amount),0) As [Net Amount]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.ItemCategory 
                    Order By Max(VMain.ItemCategoryDescription)"
            ElseIf ReportFrm.FGetText(0) = "City Wise Summary" Then
                mQry = " Select VMain.CityCode as SearchCode, Max(VMain.CityName) As [City], 
                    Count(Distinct Vmain.DocID) as [Doc.Count],  Round(Sum(VMain.Qty),3) as Qty,
                    Sum(VMain.AmountExDiscount) as GoodsValue, Sum(VMain.Discount) as Discount, Sum(VMain.Addition) as Addition, Sum(VMain.SpecialDiscount) as SpecialDiscount, Sum(VMain.SpecialAddition) as SpecialAddition,
                    Sum(VMain.Amount) As Amount, IfNull(Sum(VMain.Taxable_Amount),0) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount, IfNull(Sum(VMain.Net_Amount),0) As [Net Amount]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.CityCode 
                    Order By Max(VMain.CityName)"
            ElseIf ReportFrm.FGetText(0) = "Area Wise Summary" Then
                mQry = " Select VMain.AreaCode as SearchCode, Max(VMain.AreaName) As [Area], 
                    Count(Distinct Vmain.DocID) as [Doc.Count],  Round(Sum(VMain.Qty),3) as Qty,
                    Sum(VMain.AmountExDiscount) as GoodsValue, Sum(VMain.Discount) as Discount, Sum(VMain.Addition) as Addition, Sum(VMain.SpecialDiscount) as SpecialDiscount, Sum(VMain.SpecialAddition) as SpecialAddition,
                    Sum(VMain.Amount) As Amount, IfNull(Sum(VMain.Taxable_Amount),0) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount, IfNull(Sum(VMain.Net_Amount),0) As [Net Amount]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.AreaCode 
                    Order By Max(VMain.AreaName)"
            ElseIf ReportFrm.FGetText(0) = "State Wise Summary" Then
                mQry = " Select VMain.StateCode as SearchCode, Max(VMain.StateName) As [State], 
                    Count(Distinct Vmain.DocID) as [Doc.Count],  Round(Sum(VMain.Qty),3) as Qty,
                    Sum(VMain.AmountExDiscount) as GoodsValue, Sum(VMain.Discount) as Discount, Sum(VMain.Addition) as Addition, Sum(VMain.SpecialDiscount) as SpecialDiscount, Sum(VMain.SpecialAddition) as SpecialAddition,
                    Sum(VMain.Amount) As Amount, Sum(VMain.Taxable_Amount) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount, Sum(VMain.Net_Amount) As [Net Amount]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.StateCode 
                    Order By Max(VMain.StateName)"
            ElseIf ReportFrm.FGetText(0) = "Party Tax Group Wise Summary" Then
                mQry = " Select VMain.SalesTaxGroupParty as SearchCode, Max(VMain.SalesTaxGroupParty) As [Party Tax Group], 
                    Count(Distinct Vmain.DocID) as [Doc.Count],  Round(Sum(VMain.Qty),3) as Qty,
                    Sum(VMain.AmountExDiscount) as GoodsValue, Sum(VMain.Discount) as Discount, Sum(VMain.Addition) as Addition, Sum(VMain.SpecialDiscount) as SpecialDiscount, Sum(VMain.SpecialAddition) as SpecialAddition,
                    Sum(VMain.Amount) As Amount, Sum(VMain.Taxable_Amount) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount, Sum(VMain.Net_Amount) As [Net Amount]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.SalesTaxGroupParty 
                    Order By Max(VMain.SalesTaxGroupParty)"
            ElseIf ReportFrm.FGetText(0) = "Item Tax Group Wise Summary" Then
                mQry = " Select VMain.SalesTaxGroupItem as SearchCode, Max(VMain.SalesTaxGroupItem) As [Item Tax Group], 
                    Count(Distinct Vmain.DocID) as [Doc.Count],  Round(Sum(VMain.Qty),3) as Qty,
                    Sum(VMain.AmountExDiscount) as GoodsValue, Sum(VMain.Discount) as Discount, Sum(VMain.Addition) as Addition, Sum(VMain.SpecialDiscount) as SpecialDiscount, Sum(VMain.SpecialAddition) as SpecialAddition,
                    Sum(VMain.Amount) As Amount, Sum(VMain.Taxable_Amount) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount, Sum(VMain.Net_Amount) As [Net Amount]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.SalesTaxGroupItem
                    Order By Max(VMain.SalesTaxGroupItem)"
            ElseIf ReportFrm.FGetText(0) = "Agent Wise Summary" Then
                mQry = " Select VMain.AgentCode As SearchCode, Max(VMain.AgentName) As [Agent], 
                    Count(Distinct Vmain.DocID) as [Doc.Count],  Round(Sum(VMain.Qty),3) as Qty,
                    Sum(VMain.AmountExDiscount) as GoodsValue, Sum(VMain.Discount) as Discount, Sum(VMain.Addition) as Addition, Sum(VMain.SpecialDiscount) as SpecialDiscount, Sum(VMain.SpecialAddition) as SpecialAddition,
                    Sum(VMain.Amount) As Amount, Sum(VMain.Taxable_Amount) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount, Sum(VMain.Net_Amount) As [Net Amount]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.AgentCode 
                    Order By Max(VMain.AgentName)"
            ElseIf ReportFrm.FGetText(0) = "Site Wise Summary" Then
                mQry = " Select VMain.Site_Code As SearchCode, Max(VMain.Site) As [Site], 
                    Count(Distinct Vmain.DocID) as [Doc.Count],  Round(Sum(VMain.Qty),3) as Qty,
                    Sum(VMain.AmountExDiscount) as GoodsValue, Sum(VMain.Discount) as Discount, Sum(VMain.Addition) as Addition, Sum(VMain.SpecialDiscount) as SpecialDiscount, Sum(VMain.SpecialAddition) as SpecialAddition,
                    Sum(VMain.Amount) As Amount, Sum(VMain.Taxable_Amount) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount, Sum(VMain.Net_Amount) As [Net Amount]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.Site_Code 
                    Order By Max(VMain.Site)"
            ElseIf ReportFrm.FGetText(0) = "Division Wise Summary" Then
                mQry = " Select VMain.Div_Code As SearchCode, Max(VMain.Division) As [Division], 
                    Count(Distinct Vmain.DocID) as [Doc.Count], Round(Sum(VMain.Qty),3) as Qty,
                    Sum(VMain.AmountExDiscount) as GoodsValue, Sum(VMain.Discount) as Discount, Sum(VMain.Addition) as Addition, Sum(VMain.SpecialDiscount) as SpecialDiscount, Sum(VMain.SpecialAddition) as SpecialAddition,
                    Sum(VMain.Amount) As Amount, Sum(VMain.Taxable_Amount) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount, Sum(VMain.Net_Amount) As [Net Amount]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.Div_Code 
                    Order By Max(VMain.Division)"
            ElseIf ReportFrm.FGetText(0) = "Month Wise Summary" Then
                If AgL.PubServerName = "" Then
                    mQry = " Select strftime('%m-%Y',VMain.V_Date_ActualFormat) As SearchCode, strftime('%m-%Y',VMain.V_Date_ActualFormat) As [Month], Max(VMain.GodownName) AS GodownName,
                    Count(Distinct Vmain.DocID) as [Doc.Count],  Round(Sum(VMain.Qty),3) as Qty,
                    Sum(VMain.AmountExDiscount) as GoodsValue, Sum(VMain.Discount) as Discount, Sum(VMain.Addition) as Addition, Sum(VMain.SpecialDiscount) as SpecialDiscount, Sum(VMain.SpecialAddition) as SpecialAddition,
                    Sum(VMain.Amount) As Amount, Sum(VMain.Taxable_Amount) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount, Sum(VMain.Net_Amount) As [Net Amount]
                    From (" & mQry & ") As VMain
                    GROUP By strftime('%m-%Y',VMain.V_Date_ActualFormat),VMain.GodownName  
                    Order By strftime('%Y',VMain.V_Date_ActualFormat), strftime('%m',VMain.V_Date_ActualFormat), Max(VMain.GodownName)"
                Else
                    mQry = " Select Substring(Convert(NVARCHAR, VMain.V_Date_ActualFormat,103),4,7) As SearchCode, Substring(Convert(NVARCHAR, VMain.V_Date_ActualFormat,103),4,7) As [Month], 
                    Count(Distinct Vmain.DocID) as [Doc.Count], 
                    Sum(VMain.AmountExDiscount) as GoodsValue, Sum(VMain.Discount) as Discount, Sum(VMain.Addition) as Addition, Sum(VMain.SpecialDiscount) as SpecialDiscount, Sum(VMain.SpecialAddition) as SpecialAddition,
                    Sum(VMain.Amount) As Amount, Sum(VMain.Taxable_Amount) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount, Sum(VMain.Net_Amount) As [Net Amount]
                    From (" & mQry & ") As VMain
                    GROUP By Substring(Convert(NVARCHAR, VMain.V_Date_ActualFormat,103),4,7), Year(VMain.V_Date_ActualFormat), Month(VMain.V_Date_ActualFormat)  
                    Order By Year(VMain.V_Date_ActualFormat), Month(VMain.V_Date_ActualFormat) "
                End If
            End If




            DsHeader = AgL.FillData(mQry, AgL.GCn)



            If DsHeader.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            ReportFrm.Text = "Sale Invoice Report - " + ReportFrm.FGetText(0)
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = "ProcSaleReport"
            ReportFrm.InputColumnsStr = "Tags"

            ReportFrm.ProcFillGrid(DsHeader)

            If AgL.VNull(ReportFrm.DGL2.Item("Taxable Amount", 0).Value) = AgL.VNull(ReportFrm.DGL2.Item("Amount", 0).Value) Then
                ReportFrm.DGL1.Columns("Taxable Amount").Visible = False
                ReportFrm.DGL2.Columns("Taxable Amount").Visible = False
            End If

            If AgL.VNull(ReportFrm.DGL2.Item("Amount", 0).Value) = AgL.VNull(ReportFrm.DGL2.Item("Net Amount", 0).Value) Then
                ReportFrm.DGL1.Columns("Amount").Visible = False
                ReportFrm.DGL2.Columns("Amount").Visible = False
            End If


        Catch ex As Exception
            MsgBox(ex.Message)
            DsHeader = Nothing
        End Try
    End Sub


    Private Sub ObjRepFormGlobal_Dgl1KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ReportFrm.Dgl1KeyDown
        Dim bRowIndex As Integer = 0, bColumnIndex As Integer = 0
        Dim bItemCode As String = ""
        Dim DrTemp As DataRow() = Nothing
        Dim dsTemp As DataSet
        Try

            If ReportFrm.DGL1.CurrentCell Is Nothing Then Exit Sub

            bRowIndex = ReportFrm.DGL1.CurrentCell.RowIndex
            bColumnIndex = ReportFrm.DGL1.CurrentCell.ColumnIndex

            If ClsMain.IsSpecialKeyPressed(e) = True Then Exit Sub

            Select Case GRepFormName
                Case SaleInvoiceReport
                    Select Case ReportFrm.DGL1.Columns(bColumnIndex).Name
                        Case "Tags"
                            mQry = " Select Code, '+' || Description As Description From Tag
                                Union All
                                Select '' as Code, '' as Description "
                            dsTemp = AgL.FillData(mQry, AgL.GCn)
                            FSingleSelectForm("Tags", bRowIndex, dsTemp)

                            mQry = "Update SaleInvoice Set Tags = " & AgL.Chk_Text(ReportFrm.DGL1.Item(bColumnIndex, bRowIndex).Value) & " Where DocID = '" & ReportFrm.DGL1.Item(Col1SearchCode, bRowIndex).Value & "'"
                            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
                    End Select
            End Select

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FSingleSelectForm(bColumnName As String, bRowIndex As Integer, bDataSet As DataSet)
        Dim FRH_Single As DMHelpGrid.FrmHelpGrid
        FRH_Single = New DMHelpGrid.FrmHelpGrid(New DataView(CType(bDataSet, DataSet).Tables(0)), "", 500, 500, 150, 520, False)
        FRH_Single.FFormatColumn(0, , 0, , False)
        FRH_Single.FFormatColumn(1, "Description", 400, DataGridViewContentAlignment.MiddleLeft)
        FRH_Single.StartPosition = FormStartPosition.Manual
        FRH_Single.ShowDialog()

        Dim bCode As String = ""
        If FRH_Single.BytBtnValue = 0 Then
            ReportFrm.DGL1.Item(bColumnName, bRowIndex).Tag = FRH_Single.DRReturn("Code")
            ReportFrm.DGL1.Item(bColumnName, bRowIndex).Value = FRH_Single.DRReturn("Description")
        End If
    End Sub

    Public Sub ProcSaleReportAadhat(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try
            Dim mCondStr$ = ""
            Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"
            Dim mTags As String() = Nothing
            Dim J As Integer



            RepTitle = "Sale Invoice Report"

            If mFilterGrid IsNot Nothing And mGridRow IsNot Nothing Then
                If mGridRow.DataGridView.Columns.Contains("Search Code") = True Then
                    If mFilterGrid.Item(GFilter, 0).Value = "Doc.Header Wise Detail" Or
                            mFilterGrid.Item(GFilter, 0).Value = "Item Wise Detail" Then

                        ClsMain.FOpenForm(mGridRow.Cells("Search Code").Value, ReportFrm)
                        ReportFrm.FiterGridCopy_Arr.RemoveAt(ReportFrm.FiterGridCopy_Arr.Count - 1)

                        Exit Sub
                    End If
                Else
                    Exit Sub
                End If
            End If

            mCondStr = " Where VT.NCat In ('" & Ncat.SaleInvoice & "', '" & Ncat.SaleReturn & "') "
            mCondStr = mCondStr & " AND H.Div_Code = '" & AgL.PubDivCode & "' "
            mCondStr = mCondStr & " AND Date(H.V_Date) Between " & AgL.Chk_Date(CDate(ReportFrm.FGetText(1)).ToString("s")) & " And " & AgL.Chk_Date(CDate(ReportFrm.FGetText(2)).ToString("s")) & " "
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.SaleToParty", 3)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Item", 4)
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("H.Site_Code", 5), "''", "'")
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
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.EntryBy", 17)
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
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("H.Div_Code", 16), "''", "'")
            If ReportFrm.FGetText(18) <> "All" Then
                mCondStr = mCondStr & " And I.HSN = " & AgL.Chk_Text(ReportFrm.FGetText(18)) & " "
            End If
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.SalesTaxGroupParty", 19)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.SalesTaxGroupItem", 20)


            mQry = " SELECT H.DocID, H.V_Type, Vt.Description as VoucherType, Site.Name as Site, Div.Div_Name as Division,
                    (Select Case When Vt1.NCat = 'SO' Then S1.ManualRefNo Else Null End From SaleInvoice S1 Left Join Voucher_Type Vt1 On S1.V_Type = Vt1.V_Type Where S1.DocID = L.SaleInvoice) as OrderNo, 
                    strftime('%d/%m/%Y', H.V_Date) As V_Date, H.V_Date As V_Date_ActualFormat,
                    H.SaleToParty, I.ItemGroup, I.ItemCategory,
                    (Case When H.SaleToParty=H.BillToParty Then Party.Name Else BillToParty.Name || ' - ' || Party.Name End) As SaleToPartyName , 
                    LTV.Agent As AgentCode, Agent.Name As AgentName, H.ResponsiblePerson, ResponsiblePerson.Name as ResponsiblePersonName,
                    L.SalesRepresentative, SalesRep.Name as SalesRepresentativeName, H.SalesTaxGroupParty,
                    City.CityCode, City.CityName, State.Code As StateCode, State.Description As StateName,
                    Cast(Replace(H.ManualRefNo,'-','') as Integer) as InvoiceNo, H.ManualRefNo, L.Item,
                    I.Specification as ItemSpecification, I.Description As ItemDesc, IfNull(I.HSN,IC.HSN) as HSN,IG.Description as ItemGroupDescription, IC.Description as ItemCategoryDescription,  
                    (Case When L.DiscountPer = 0 Then '' else Cast(L.DiscountPer as nVarchar) End)  || (Case When L.AdditionalDiscountPer>0 Then '+' else '' End) || (Case When L.AdditionalDiscountPer=0 Then '' else Cast(L.AdditionalDiscountPer as nVarchar) End)  as DiscountPer, L.DiscountAmount + L.AdditionalDiscountAmount as Discount, L.Taxable_Amount, (Case When L.Net_Amount=0 Then L.Amount Else L.Net_Amount End) as Net_Amount, L.Qty, L.Unit, L.Rate, L.Amount -(L.DiscountAmount + L.AdditionalDiscountAmount) as AmountExDiscount, L.Amount,
                    L.Tax1, L.Tax2, L.Tax3, L.Tax4, L.Tax5, L.Tax1+L.Tax2+L.Tax3+L.Tax4+L.Tax5 as TotalTax, H.EntryBy as EntryByUser
                    FROM SaleInvoice H 
                    Left Join SaleInvoiceDetail L On H.DocID = L.DocID 
                    Left Join Item I On L.Item = I.Code 
                    Left Join Item IG On I.ItemGroup = IG.Code
                    Left Join Item IC On I.ItemCategory = IC.Code
                    Left Join viewHelpSubgroup Party On H.SaleToParty = Party.Code 
                    Left Join viewHelpSubgroup BillToParty On H.BillToParty = BillToParty.Code 
                    Left Join (Select SILTV.Subcode, SILTV.Div_Code, SILTV.Site_Code, Max(SILTV.Agent) as Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode, SILTV.Div_Code, SILTV.Site_Code) as LTV On Party.code = LTV.Subcode And H.Site_Code = LTV.Site_Code And H.Div_Code = LTV.Div_Code
                    Left Join viewHelpSubGroup Agent On LTV.Agent = Agent.Code 
                    Left Join viewHelpSubGroup SalesRep On L.SalesRepresentative = SalesRep.Code 
                    Left Join viewHelpSubGroup ResponsiblePerson On H.ResponsiblePerson = ResponsiblePerson.Code 
                    Left Join City On H.SaleToPartyCity = City.CityCode 
                    Left Join State On City.State = State.Code                    
                    LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type     
                    Left Join SiteMast Site On H.Site_Code = Site.Code
                    Left Join Division Div On H.Div_Code = Div.Div_Code
                    " & mCondStr


            If ReportFrm.FGetText(0) = "Doc.Header Wise Detail" Then
                If GRepFormName = SaleOrderReport Then
                    mQry = " Select VMain.DocId As SearchCode, Max(VMain.Division) as Division, Max(Vmain.Site) as Site, Max(VMain.V_Date) As OrderDate, Max(VMain.V_Type) as DocType, Max(VMain.InvoiceNo) As OrderNo,
                    Max(VMain.SaleToPartyName) As Party, IfNull(Sum(VMain.AmountExDiscount),0) As Amount, IfNull(Sum(VMain.Discount),0) As Discount,
                    IfNull(Sum(VMain.Taxable_Amount),0) As TaxableAmount, IfNull(Sum(VMain.TotalTax),0) As TaxAmount, IfNull(Sum(VMain.Net_Amount),0) As NetAmount
                    From (" & mQry & ") As VMain
                    GROUP By VMain.DocId 
                    Order By Max(VMain.V_Date_ActualFormat), Cast(Max(Replace(Vmain.ManualRefNo,'-','')) as Integer) "
                Else
                    mQry = " Select VMain.DocId As SearchCode, Max(VMain.Division) as Division, Max(Vmain.Site) as Site, Max(VMain.V_Date) As InvoiceDate, Max(VMain.V_Type) as DocType, Max(VMain.InvoiceNo) As InvoiceNo,
                    Max(VMain.SaleToPartyName) As Party, Max(VMain.SalesTaxGroupParty) As SalesTaxGroupParty, IfNull(Sum(VMain.AmountExDiscount),0) As Amount, IfNull(Sum(VMain.Discount),0) As Discount,
                    IfNull(Sum(VMain.Taxable_Amount),0) As TaxableAmount, IfNull(Sum(VMain.TotalTax),0) As TaxAmount, IfNull(Sum(VMain.Net_Amount),0) As NetAmount
                    From (" & mQry & ") As VMain
                    GROUP By VMain.DocId 
                    Order By Max(VMain.V_Date_ActualFormat), Cast(Max(Replace(Vmain.ManualRefNo,'-','')) as Integer) "
                End If
            ElseIf ReportFrm.FGetText(0) = "Item Wise Detail" Then
                If GRepFormName = SaleOrderReport Then
                    mQry = " Select VMain.DocId As SearchCode, Max(Vmain.Site) as Site, Max(VMain.Division) as Division, Max(VMain.V_Date) As [Order Date], Max(VMain.V_Type) as DocType, Max(VMain.InvoiceNo) As [Order No],
                    Max(VMain.SaleToPartyName) As Party, Max(VMain.ItemDesc) As Item, Sum(VMain.Qty) As Qty, Max(VMain.Unit) As Unit, 
                    Max(VMain.Rate) As Rate,
                    Sum(VMain.AmountExDiscount) As AmountExDiscount, Max(VMain.DiscountPer) As [Discount Per], 
                    Sum(VMain.Discount) As Discount,
                    Sum(VMain.Amount) As [Amount],
                    Sum(VMain.Taxable_Amount) As [Taxable Amount],
                    Sum(VMain.TotalTax) As [Tax Amount],
                    Sum(VMain.Net_Amount) As [Net Amount]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.DocId, VMain.Item 
                    Order By  Max(VMain.V_Date), Cast(Max(Replace(Vmain.ManualRefNo,'-','')) as Integer) "
                Else
                    mQry = " Select VMain.DocId As SearchCode, Max(Vmain.Site) as Site, Max(VMain.Division) as Division, Max(VMain.V_Date) As [Invoice Date], Max(VMain.V_Type) as DocType, Max(VMain.InvoiceNo) As [Invoice No],
                    Max(VMain.SaleToPartyName) As Party, Max(VMain.ItemDesc) As Item, Max(VMain.OrderNo) as [Order No], Sum(VMain.Qty) As Qty, Max(VMain.Unit) As Unit, 
                    Max(VMain.Rate) As Rate,
                    Sum(VMain.AmountExDiscount) As Amount, Max(VMain.DiscountPer) As [Discount Per], 
                    Sum(VMain.Discount) As Discount,
                    Sum(VMain.Taxable_Amount) As [Taxable Amount],
                    Sum(VMain.TotalTax) As [Tax Amount],
                    Sum(VMain.Net_Amount) As [Net Amount]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.DocId, VMain.Item 
                    Order By  Max(VMain.V_Date), Cast(Max(Replace(Vmain.ManualRefNo,'-','')) as Integer) "
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
                    Count(Distinct Vmain.DocID) as [Doc.Count], 
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
            ElseIf ReportFrm.FGetText(0) = "User Wise Summary" Then
                mQry = " Select VMain.EntryByUser as SearchCode, Max(VMain.EntryByUser) As UserName,
                    Count(Distinct Vmain.DocID) as InvoicesCount, Count(Distinct VMain.V_Date) as DaysCount, 
                    Sum(VMain.Taxable_Amount) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount, Sum(VMain.Net_Amount) As [Net Amount]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.EntryByUser
                    Order By Max(VMain.EntryByUser)"
            ElseIf ReportFrm.FGetText(0) = "Item Wise Summary" Then
                mQry = " Select VMain.Item As SearchCode, Max(VMain.ItemDesc) As [Item], 
                    IfNull(Sum(VMain.Taxable_Amount),0) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount, IfNull(Sum(VMain.Net_Amount),0) As [Net Amount]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.Item 
                    Order By Max(VMain.ItemDesc)"
            ElseIf ReportFrm.FGetText(0) = "HSN Wise Summary" Then
                mQry = " Select VMain.HSN As SearchCode, VMain.HSN, Max(VMain.ItemCategoryDescription) As [Description], 
                    IfNull(Sum(VMain.Taxable_Amount),0) As [Taxable Amount], IfNull(Sum(VMain.Tax1),0) As IGST, IfNull(Sum(VMain.Tax2),0) As CGST, 
                    IfNull(Sum(VMain.Tax3),0) As SGST, IfNull(Sum(VMain.Tax4),0) As Cess, IfNull(Sum(VMain.Net_Amount),0) As [Net Amount]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.HSN, VMain.ItemCategoryDescription 
                    Order By VMain.HSN, VMain.ItemCategoryDescription"
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
                    Count(Distinct Vmain.DocID) as [Doc.Count], 
                    IfNull(Sum(VMain.Taxable_Amount),0) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount, IfNull(Sum(VMain.Net_Amount),0) As [Net Amount]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.CityCode 
                    Order By Max(VMain.CityName)"
            ElseIf ReportFrm.FGetText(0) = "State Wise Summary" Then
                mQry = " Select VMain.StateCode as SearchCode, Max(VMain.StateName) As [State], 
                    Count(Distinct Vmain.DocID) as [Doc.Count], 
                    Sum(VMain.Taxable_Amount) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount, Sum(VMain.Net_Amount) As [Net Amount]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.StateCode 
                    Order By Max(VMain.StateName)"
            ElseIf ReportFrm.FGetText(0) = "Agent Wise Summary" Then
                mQry = " Select VMain.AgentCode As SearchCode, Max(VMain.AgentName) As [Agent], 
                    Count(Distinct Vmain.DocID) as [Doc.Count], 
                    Sum(VMain.Taxable_Amount) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount, Sum(VMain.Net_Amount) As [Net Amount]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.AgentCode 
                    Order By Max(VMain.AgentName)"
            ElseIf ReportFrm.FGetText(0) = "Month Wise Summary" Then
                If AgL.PubServerName = "" Then
                    mQry = " Select strftime('%m-%Y',VMain.V_Date_ActualFormat) As SearchCode, strftime('%m-%Y',VMain.V_Date_ActualFormat) As [Month], 
                    Count(Distinct Vmain.DocID) as [Doc.Count], 
                    Sum(VMain.Taxable_Amount) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount, Sum(VMain.Net_Amount) As [Net Amount]
                    From (" & mQry & ") As VMain
                    GROUP By strftime('%m-%Y',VMain.V_Date_ActualFormat)  
                    Order By strftime('%Y',VMain.V_Date_ActualFormat), strftime('%m',VMain.V_Date_ActualFormat)"
                Else
                    mQry = " Select Substring(Convert(NVARCHAR, VMain.V_Date_ActualFormat,103),4,7) As SearchCode, Substring(Convert(NVARCHAR, VMain.V_Date_ActualFormat,103),4,7) As [Month], 
                    Count(Distinct Vmain.DocID) as [Doc.Count], 
                    Sum(VMain.Taxable_Amount) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount, Sum(VMain.Net_Amount) As [Net Amount]
                    From (" & mQry & ") As VMain
                    GROUP By Substring(Convert(NVARCHAR, VMain.V_Date_ActualFormat,103),4,7), Year(VMain.V_Date_ActualFormat), Month(VMain.V_Date_ActualFormat)  
                    Order By Year(VMain.V_Date_ActualFormat), Month(VMain.V_Date_ActualFormat) "
                End If
            End If




            DsHeader = AgL.FillData(mQry, AgL.GCn)

            If DsHeader.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            ReportFrm.Text = "Sale Invoice Report - " + ReportFrm.FGetText(0)
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = "ProcSaleReport"

            ReportFrm.ProcFillGrid(DsHeader)

        Catch ex As Exception
            MsgBox(ex.Message)
            DsHeader = Nothing
        End Try
    End Sub

    Public Sub ProcDebitCreditNoteReport(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try
            Dim mCondStr$ = ""
            Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"

            RepTitle = "Debit / Credit Note Report"

            If mFilterGrid IsNot Nothing And mGridRow IsNot Nothing Then
                If mGridRow.DataGridView.Columns.Contains("Search Code") = True Then
                    If mFilterGrid.Item(GFilter, 0).Value = "Month Wise Summary" Then
                        mFilterGrid.Item(GFilter, 0).Value = "Entry Head Wise Detail"
                        mFilterGrid.Item(GFilter, 1).Value = AgL.RetMonthStartDate(CDate(mGridRow.Cells("Month").Value))
                        mFilterGrid.Item(GFilter, 2).Value = AgL.RetMonthEndDate(CDate(mGridRow.Cells("Month").Value))
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = "Party Wise Summary" Then
                        mFilterGrid.Item(GFilter, 0).Value = "Entry Line Wise Detail"
                        mFilterGrid.Item(GFilter, 3).Value = mGridRow.Cells("Party").Value
                        mFilterGrid.Item(GFilterCode, 3).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = "Agent Wise Summary" Then
                        mFilterGrid.Item(GFilter, 0).Value = "Entry Line Wise Detail"
                        mFilterGrid.Item(GFilter, 6).Value = mGridRow.Cells("Agent").Value
                        mFilterGrid.Item(GFilterCode, 6).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = "Voucher Type Wise Summary" Then
                        mFilterGrid.Item(GFilter, 0).Value = "Entry Line Wise Detail"
                        mFilterGrid.Item(GFilter, 5).Value = mGridRow.Cells("Voucher Type").Value
                        mFilterGrid.Item(GFilterCode, 5).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = "City Wise Summary" Then
                        mFilterGrid.Item(GFilter, 0).Value = "Entry Line Wise Detail"
                        mFilterGrid.Item(GFilter, 7).Value = mGridRow.Cells("City").Value
                        mFilterGrid.Item(GFilterCode, 7).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = "State Wise Summary" Then
                        mFilterGrid.Item(GFilter, 0).Value = "Entry Line Wise Detail"
                        mFilterGrid.Item(GFilter, 8).Value = mGridRow.Cells("State").Value
                        mFilterGrid.Item(GFilterCode, 8).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = "Entry Head Wise Detail" Or
                            mFilterGrid.Item(GFilter, 0).Value = "Entry Line Wise Detail" Then

                        ClsMain.FOpenForm(mGridRow.Cells("Search Code").Value, ReportFrm)
                        ReportFrm.FiterGridCopy_Arr.RemoveAt(ReportFrm.FiterGridCopy_Arr.Count - 1)

                        Exit Sub
                    End If
                Else
                    Exit Sub
                End If
            End If

            mCondStr = " Where VT.NCat In ('" & Ncat.DebitNoteSupplier & "','" & Ncat.DebitNoteCustomer & "','" & Ncat.CreditNoteCustomer & "','" & Ncat.CreditNoteSupplier & "') "
            mCondStr = mCondStr & " AND H.Div_Code = '" & AgL.PubDivCode & "' "
            mCondStr = mCondStr & " AND Date(H.V_Date) Between " & AgL.Chk_Date(CDate(ReportFrm.FGetText(1)).ToString("s")) & " And " & AgL.Chk_Date(CDate(ReportFrm.FGetText(2)).ToString("s")) & " "
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.Subcode", 3)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.Site_Code", 4)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.V_Type", 5)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("LTV.Agent", 6)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("City.CityCode", 7)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("City.State", 8)
            'If ReportFrm.FGetText(8) <> "All" Then
            '    mCondStr += " And H.Agent = '" & ReportFrm.FGetCode(8) & "' "
            'End If

            'mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.Agent", 8)

            mQry = " SELECT H.DocID, L.Sr, H.V_Type, Vt.Description as VoucherType, strftime('%d/%m/%Y', H.V_Date) As V_Date, H.V_Date As V_Date_ActualFormat,
                    H.Subcode,
                    Party.Name As PartyName , LinkedParty.Name As LinkedPartyName ,
                    LTV.Agent As AgentCode, Agent.Name As AgentName , 
                    City.CityCode, City.CityName, State.Code As StateCode, State.Description As StateName,
                    H.Div_Code || H.Site_Code || '-' || H.V_Type || '-' || H.ManualRefNo as InvoiceNo, H.ManualRefNo, 
                    L.SalesTaxGroupItem, LC.Taxable_Amount, LC.Net_Amount, L.Qty, L.Unit, L.Rate, L.Amount,
                    LC.Tax1+LC.Tax2+LC.Tax3+LC.Tax4+LC.Tax5 as TotalTax, H.Remarks as HeadRemarks, L.Remarks as LineRemarks
                    FROM LedgerHead H 
                    Left Join LedgerHeadDetail L On H.DocID = L.DocID 
                    Left Join LedgerHeadDetailCharges LC On L.DocID = LC.DocID And L.Sr = LC.Sr
                    Left Join viewHelpSubgroup Party On H.Subcode = Party.Code
                    Left Join viewHelpSubgroup LinkedParty On H.LinkedSubcode = LinkedParty.Code  
                    Left Join (Select SILTV.Subcode, SILTV.Div_Code, SILTV.Site_Code, Max(SILTV.Agent) as Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode, SILTV.Div_Code, SILTV.Site_Code) as LTV On Party.code = LTV.Subcode And H.Div_Code = LTV.Div_Code and H.Site_Code = LTV.Site_Code
                    Left Join viewHelpSubGroup Agent On LTV.Agent = Agent.Code 
                    Left Join City On H.PartyCity = City.CityCode 
                    Left Join State On City.State = State.Code
                    LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type " & mCondStr


            If ReportFrm.FGetText(0) = "Entry Head Wise Detail" Then
                mQry = " Select VMain.DocId As SearchCode, Max(VMain.V_Date) As InvoiceDate, Max(VMain.InvoiceNo) As InvoiceNo,
                    Max(VMain.PartyName) As Party, IfNull(Sum(VMain.AmountExDiscount),0) As Amount, IfNull(Sum(VMain.Discount),0) As Discount,
                    IfNull(Sum(VMain.Taxable_Amount),0) As TaxableAmount, IfNull(Sum(VMain.TotalTax),0) As TaxAmount, IfNull(Sum(VMain.Net_Amount),0) As NetAmount, Max(VMain.HeadRemarks) as HeadRemarks
                    From (" & mQry & ") As VMain
                    GROUP By VMain.DocId 
                    Order By Max(VMain.V_Date_ActualFormat) "
            ElseIf ReportFrm.FGetText(0) = "Entry Line Wise Detail" Then
                mQry = " Select VMain.DocId As SearchCode, Max(VMain.V_Date) As [Entry Date], Max(VMain.InvoiceNo) As [Entry No],
                    Max(VMain.PartyName) As Party, Max(VMain.LinkedPartyName) As LinkedParty, Max(VMain.SalesTaxGroupItem)  as SalesTaxGroupItem,  Sum(VMain.Qty) As Qty, Max(VMain.Unit) As Unit, 
                    Max(VMain.Rate) As Rate,                
                    Sum(VMain.Amount) As Amount,                     
                    Sum(VMain.Taxable_Amount) As [Taxable Amount],
                    Sum(VMain.TotalTax) As [Tax Amount],
                    Sum(VMain.Net_Amount) As [Net Amount], Max(VMain.HeadRemarks) as HeadRemarks, Max(VMain.LineRemarks) as LineRemarks
                    From (" & mQry & ") As VMain
                    GROUP By VMain.DocId, VMain.Sr 
                    Order By  Max(VMain.V_Date_ActualFormat) "
            ElseIf ReportFrm.FGetText(0) = "Party Wise Summary" Then
                mQry = " Select VMain.Subcode as SearchCode, Max(VMain.PartyName) As Party, 
                    Sum(VMain.Taxable_Amount) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount, Sum(VMain.Net_Amount) As [Net Amount]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.Subcode 
                    Order By Max(VMain.PartyName)"
            ElseIf ReportFrm.FGetText(0) = "Voucher Type Wise Summary" Then
                mQry = " Select VMain.V_Type As SearchCode, Max(VMain.VoucherType) As [Voucher Type], 
                    IfNull(Sum(VMain.Taxable_Amount),0) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount, IfNull(Sum(VMain.Net_Amount),0) As [Net Amount]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.V_Type 
                    Order By Max(VMain.VoucherType)"
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




            DsHeader = AgL.FillData(mQry, AgL.GCn)

            If DsHeader.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            ReportFrm.Text = "Debit / Credit Note Report - " + ReportFrm.FGetText(0)
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = "ProcDebitCreditNoteReport"

            ReportFrm.ProcFillGrid(DsHeader)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsHeader = Nothing
        End Try
    End Sub

    Public Sub ProcExpenseIncomeReport(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try
            Dim mCondStr$ = ""
            Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"

            RepTitle = "Expense Income Report"

            If mFilterGrid IsNot Nothing And mGridRow IsNot Nothing Then
                If mGridRow.DataGridView.Columns.Contains("Search Code") = True Then
                    If mFilterGrid.Item(GFilter, 0).Value = "Month Wise Summary" Then
                        mFilterGrid.Item(GFilter, 0).Value = "Entry Head Wise Detail"
                        mFilterGrid.Item(GFilter, 1).Value = AgL.RetMonthStartDate(CDate(mGridRow.Cells("Month").Value))
                        mFilterGrid.Item(GFilter, 2).Value = AgL.RetMonthEndDate(CDate(mGridRow.Cells("Month").Value))
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = "Party Wise Summary" Then
                        mFilterGrid.Item(GFilter, 0).Value = "Entry Line Wise Detail"
                        mFilterGrid.Item(GFilter, 3).Value = mGridRow.Cells("Party").Value
                        mFilterGrid.Item(GFilterCode, 3).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = "Agent Wise Summary" Then
                        mFilterGrid.Item(GFilter, 0).Value = "Entry Line Wise Detail"
                        mFilterGrid.Item(GFilter, 6).Value = mGridRow.Cells("Agent").Value
                        mFilterGrid.Item(GFilterCode, 6).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = "Voucher Type Wise Summary" Then
                        mFilterGrid.Item(GFilter, 0).Value = "Entry Line Wise Detail"
                        mFilterGrid.Item(GFilter, 5).Value = mGridRow.Cells("Voucher Type").Value
                        mFilterGrid.Item(GFilterCode, 5).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = "City Wise Summary" Then
                        mFilterGrid.Item(GFilter, 0).Value = "Entry Line Wise Detail"
                        mFilterGrid.Item(GFilter, 7).Value = mGridRow.Cells("City").Value
                        mFilterGrid.Item(GFilterCode, 7).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = "State Wise Summary" Then
                        mFilterGrid.Item(GFilter, 0).Value = "Entry Line Wise Detail"
                        mFilterGrid.Item(GFilter, 8).Value = mGridRow.Cells("State").Value
                        mFilterGrid.Item(GFilterCode, 8).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = "Entry Head Wise Detail" Or
                            mFilterGrid.Item(GFilter, 0).Value = "Entry Line Wise Detail" Then

                        ClsMain.FOpenForm(mGridRow.Cells("Search Code").Value, ReportFrm)
                        ReportFrm.FiterGridCopy_Arr.RemoveAt(ReportFrm.FiterGridCopy_Arr.Count - 1)

                        Exit Sub
                    End If
                Else
                    Exit Sub
                End If
            End If

            mCondStr = " Where VT.NCat In ('" & Ncat.ExpenseVoucher & "','" & Ncat.IncomeVoucher & "') "
            mCondStr = mCondStr & " AND H.Div_Code = '" & AgL.PubDivCode & "' "
            mCondStr = mCondStr & " AND Date(H.V_Date) Between " & AgL.Chk_Date(CDate(ReportFrm.FGetText(1)).ToString("s")) & " And " & AgL.Chk_Date(CDate(ReportFrm.FGetText(2)).ToString("s")) & " "
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.Subcode", 3)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.Site_Code", 4)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.V_Type", 5)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("LTV.Agent", 6)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("City.CityCode", 7)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("City.State", 8)
            'If ReportFrm.FGetText(8) <> "All" Then
            '    mCondStr += " And H.Agent = '" & ReportFrm.FGetCode(8) & "' "
            'End If

            'mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.Agent", 8)

            mQry = " SELECT H.DocID, L.Sr, H.V_Type, Vt.Description as VoucherType, strftime('%d/%m/%Y', H.V_Date) As V_Date, H.V_Date As V_Date_ActualFormat,
                    H.Subcode,
                    Party.Name As PartyName, vReg.SalesTaxNo,
                    LTV.Agent As AgentCode, Agent.Name As AgentName , 
                    City.CityCode, City.CityName, State.Code As StateCode, State.Description As StateName,
                    H.Div_Code || H.Site_Code || '-' || H.V_Type || '-' || H.ManualRefNo as InvoiceNo, H.ManualRefNo, 
                    H.PartyDocNo, H.PartyDocDate, Exp.Name as LedgerAc,
                    L.SalesTaxGroupItem, LC.Taxable_Amount, LC.Net_Amount, L.Qty, L.Unit, L.Rate, L.Amount,
                    LC.Tax1+LC.Tax2+LC.Tax3+LC.Tax4+LC.Tax5 as TotalTax, H.Remarks as HeadRemarks, L.Remarks as LineRemarks
                    FROM LedgerHead H 
                    Left Join LedgerHeadDetail L On H.DocID = L.DocID 
                    Left Join LedgerHeadDetailCharges LC On L.DocID = LC.DocID And L.Sr = LC.Sr
                    Left Join viewHelpSubgroup Party On H.Subcode = Party.Code 
                    Left Join Subgroup Exp On L.Subcode = Exp.Subcode 
                    LEFT JOIN (Select Subcode, RegistrationNo As SalesTaxNo
                               From SubgroupRegistration 
                               Where RegistrationType = 'Sales Tax No') As VReg On H.Subcode = VReg.SubCode
                    Left Join (Select SILTV.Subcode, SILTV.Div_Code, SILTV.Site_Code, Max(SILTV.Agent) as Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode, SILTV.Div_Code, SILTV.Site_Code) as LTV On Party.code = LTV.Subcode And H.Div_Code = LTV.Div_Code and H.Site_Code = LTV.Site_Code
                    Left Join viewHelpSubGroup Agent On LTV.Agent = Agent.Code 
                    Left Join City On H.PartyCity = City.CityCode 
                    Left Join State On City.State = State.Code
                    LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type " & mCondStr


            If ReportFrm.FGetText(0) = "Entry Head Wise Detail" Then
                mQry = " Select VMain.DocId As SearchCode, Max(VMain.V_Date) As InvoiceDate, Max(VMain.InvoiceNo) As InvoiceNo, 
                    Max(VMain.PartyDocNo) as PartyDocNo, Max(VMain.PartyDocDate) as PartyDocDate,
                    Max(VMain.PartyName) As Party, Max(Vmain.SalesTaxNo) as GstNo, IfNull(Sum(VMain.Amount),0) As Amount,
                    IfNull(Sum(VMain.Taxable_Amount),0) As TaxableAmount, IfNull(Sum(VMain.TotalTax),0) As TaxAmount, IfNull(Sum(VMain.Net_Amount),0) As NetAmount, Max(VMain.HeadRemarks) as HeadRemarks
                    From (" & mQry & ") As VMain
                    GROUP By VMain.DocId 
                    Order By Max(VMain.V_Date_ActualFormat) "
            ElseIf ReportFrm.FGetText(0) = "Entry Line Wise Detail" Then
                mQry = " Select VMain.DocId As SearchCode, Max(VMain.V_Date) As [Entry Date], Max(VMain.InvoiceNo) As [Entry No],
                    Max(VMain.PartyName) As Party, Max(Vmain.SalesTaxNo) as GstNo, Max(VMain.SalesTaxGroupItem)  as SalesTaxGroupItem,  Sum(VMain.Qty) As Qty, Max(VMain.Unit) As Unit, 
                    Max(VMain.PartyDocNo) as PartyDocNo, Max(VMain.PartyDocDate) as PartyDocDate, Max(VMain.LedgerAc) as LedgerAc,
                    Max(VMain.Rate) As Rate,                
                    Sum(VMain.Amount) As Amount,                     
                    Sum(VMain.Taxable_Amount) As [Taxable Amount],
                    Sum(VMain.TotalTax) As [Tax Amount],
                    Sum(VMain.Net_Amount) As [Net Amount], Max(VMain.HeadRemarks) as HeadRemarks, Max(VMain.LineRemarks) as LineRemarks
                    From (" & mQry & ") As VMain
                    GROUP By VMain.DocId, VMain.Sr 
                    Order By  Max(VMain.V_Date_ActualFormat) "
            ElseIf ReportFrm.FGetText(0) = "Party Wise Summary" Then
                mQry = " Select VMain.Subcode as SearchCode, Max(VMain.PartyName) As Party, 
                    Sum(VMain.Taxable_Amount) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount, Sum(VMain.Net_Amount) As [Net Amount]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.Subcode 
                    Order By Max(VMain.PartyName)"
            ElseIf ReportFrm.FGetText(0) = "Voucher Type Wise Summary" Then
                mQry = " Select VMain.V_Type As SearchCode, Max(VMain.VoucherType) As [Voucher Type], 
                    IfNull(Sum(VMain.Taxable_Amount),0) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount, IfNull(Sum(VMain.Net_Amount),0) As [Net Amount]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.V_Type 
                    Order By Max(VMain.VoucherType)"
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




            DsHeader = AgL.FillData(mQry, AgL.GCn)

            If DsHeader.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            ReportFrm.Text = "Expense Income Report - " + ReportFrm.FGetText(0)
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = "ProcExpenseIncomeReport"

            ReportFrm.ProcFillGrid(DsHeader)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsHeader = Nothing
        End Try
    End Sub

    Public Sub ProcPurchaseReport(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try
            Dim mCondStr$ = ""
            Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"

            RepTitle = "Purchase Invoice Report"

            If mFilterGrid IsNot Nothing And mGridRow IsNot Nothing Then
                If mGridRow.DataGridView.Columns.Contains("Search Code") = True Then
                    If mFilterGrid.Item(GFilter, 0).Value = "Month Wise Summary" Then
                        mFilterGrid.Item(GFilter, 0).Value = "Invoice Wise Detail"
                        mFilterGrid.Item(GFilter, 1).Value = AgL.RetMonthStartDate(CDate(mGridRow.Cells("Month").Value))
                        mFilterGrid.Item(GFilter, 2).Value = AgL.RetMonthEndDate(CDate(mGridRow.Cells("Month").Value))
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = "Item Wise Summary" Then
                        mFilterGrid.Item(GFilter, 0).Value = "Item Wise Detail"
                        mFilterGrid.Item(GFilter, 4).Value = mGridRow.Cells("Item").Value
                        mFilterGrid.Item(GFilterCode, 4).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = "Party Wise Summary" Then
                        mFilterGrid.Item(GFilter, 0).Value = "Item Wise Detail"
                        mFilterGrid.Item(GFilter, 3).Value = mGridRow.Cells("Party").Value
                        mFilterGrid.Item(GFilterCode, 3).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = "Agent Wise Summary" Then
                        mFilterGrid.Item(GFilter, 0).Value = "Item Wise Detail"
                        mFilterGrid.Item(GFilter, 8).Value = mGridRow.Cells("Agent").Value
                        mFilterGrid.Item(GFilterCode, 8).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = "Item Group Wise Summary" Then
                        mFilterGrid.Item(GFilter, 0).Value = "Item Wise Detail"
                        mFilterGrid.Item(GFilter, 9).Value = mGridRow.Cells("Item Group").Value
                        mFilterGrid.Item(GFilterCode, 9).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = "Item Category Wise Summary" Then
                        mFilterGrid.Item(GFilter, 0).Value = "Item Wise Detail"
                        mFilterGrid.Item(GFilter, 10).Value = mGridRow.Cells("Item Category").Value
                        mFilterGrid.Item(GFilterCode, 10).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = "City Wise Summary" Then
                        mFilterGrid.Item(GFilter, 0).Value = "Item Wise Detail"
                        mFilterGrid.Item(GFilter, 11).Value = mGridRow.Cells("City").Value
                        mFilterGrid.Item(GFilterCode, 11).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = "State Wise Summary" Then
                        mFilterGrid.Item(GFilter, 0).Value = "Item Wise Detail"
                        mFilterGrid.Item(GFilter, 12).Value = mGridRow.Cells("State").Value
                        mFilterGrid.Item(GFilterCode, 12).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = "HSN Wise Summary" Then
                        mFilterGrid.Item(GFilter, 0).Value = "Item Wise Detail"
                        mFilterGrid.Item(GFilter, 13).Value = mGridRow.Cells("HSN").Value
                        mFilterGrid.Item(GFilterCode, 13).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = "Invoice Wise Detail" Or
                        mFilterGrid.Item(GFilter, 0).Value = "Item Wise Detail" Then
                        ClsMain.FOpenForm(mGridRow.Cells("Search Code").Value, ReportFrm)
                        ReportFrm.FiterGridCopy_Arr.RemoveAt(ReportFrm.FiterGridCopy_Arr.Count - 1)

                        Exit Sub
                    End If
                Else
                    Exit Sub
                End If
            End If


            If GRepFormName = PurchaseOrderReport Then
                mCondStr = " Where VT.NCat In ('" & Ncat.PurchaseOrder & "', '" & Ncat.PurchaseOrderCancel & "') "
            Else
                mCondStr = " Where VT.NCat In ('" & Ncat.PurchaseInvoice & "', '" & Ncat.PurchaseReturn & "') "
            End If
            mCondStr = mCondStr & " AND H.Div_Code = '" & AgL.PubDivCode & "' "
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
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("LTV.Agent", 8)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemGroup", 9)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemCategory", 10)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("City.CityCode", 11)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("City.State", 12)
            If ReportFrm.FGetText(13) <> "All" Then
                mCondStr = mCondStr & " And I.HSN = " & AgL.Chk_Text(ReportFrm.FGetText(13)) & " "
            End If
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.SalesTaxGroupParty", 14)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("Party.TradeType", 15)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.SalesTaxGroupItem", 16)

            mQry = " SELECT H.DocID, L.Sr, strftime('%d/%m/%Y', H.V_Date) As V_Date, H.V_Date As V_Date_ActualFormat,
                    H.Vendor, I.ItemGroup, I.ItemCategory,
                    Party.Name As VendorName, H.VendorSalesTaxNo as PartyGstNo,
                    Agent.Code As AgentCode, Agent.Name As AgentName , 
                    City.CityCode, City.CityName, State.Code As StateCode, State.Description As StateName,
                    H.VendorDocNo as InvoiceNo, H.VendorDocDate as PartyInvoiceDate, H.Div_Code || H.Site_Code || '-' || H.V_Type || '-' || H.ManualRefNo as ManualRefNo, L.Item,
                    I.Specification as ItemSpecification, IfNull(I.HSN,IC.HSN) as HSN, I.Description As ItemDesc,IG.Description as ItemGroupDescription, IC.Description as ItemCategoryDescription,  
                    (Case When L.DiscountPer = 0 Then '' else Cast(L.DiscountPer as nVarchar) End)  || (Case When L.AdditionalDiscountPer>0 Then '+' else '' End) || (Case When L.AdditionalDiscountPer=0 Then '' else Cast(L.AdditionalDiscountPer  as nVarchar) End) as DiscountPer, 
                    L.DiscountAmount + L.AdditionalDiscountAmount as Discount, 
                    L.Taxable_Amount, L.Net_Amount, L.Qty, L.Unit, L.Rate, L.Amount + (L.DiscountAmount + L.AdditionalDiscountAmount - L.AdditionAmount) as AmountExDiscount, L.Amount,
                    L.Tax1, L.Tax2, L.Tax3, L.Tax4, L.Tax5, L.Tax1+L.Tax2+L.Tax3+L.Tax4+L.Tax5 as TotalTax, L.Commission, L.AdditionalCommission, 
                    (L.Commission + L.AdditionalCommission) as TotalCommission
                    FROM PurchInvoice H 
                    Left Join PurchInvoiceDetail L On H.DocID = L.DocID 
                    Left Join Item I On L.Item = I.Code 
                    Left Join ItemGroup IG On I.ItemGroup = IG.Code
                    Left Join ItemCategory IC On I.ItemCategory = IC.Code
                    Left Join viewHelpSubgroup Party On H.Vendor = Party.Code                     
                    Left Join viewHelpSubgroup Sg On H.BillToParty = Sg.Code                     
                    Left Join (Select SILTV.Subcode,SILTV.Div_Code, SILTV.Site_Code, Max(SILTV.Agent) as Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode, SILTV.Div_Code, SILTV.Site_Code ) as LTV On Party.code = LTV.Subcode And H.Site_Code = LTV.Site_Code And H.Div_Code = LTV.Div_Code
                    Left Join viewHelpSubGroup Agent On LTV.Agent = Agent.Code 
                    Left Join City On H.VendorCity = City.CityCode 
                    Left Join State On City.State = State.Code
                    LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type " & mCondStr


            If ReportFrm.FGetText(0) = "Invoice Wise Detail" Then
                mQry = " Select VMain.DocId As SearchCode, Max(VMain.V_Date) As InvoiceDate, Max(VMain.InvoiceNo) As InvoiceNo, Max(Vmain.PartyInvoiceDate) as PartyInvoiceDate,
                    Max(VMain.VendorName) As Party, Max(Vmain.PartyGstNo) as PartyGstNo, IfNull(Sum(VMain.AmountExDiscount),0) As AmountExDiscount, IfNull(Sum(VMain.Discount),0) As Discount,
                    Sum(VMain.Amount) as Amount,IfNull(Sum(VMain.Taxable_Amount),0) As TaxableAmount, IfNull(Sum(VMain.TotalTax),0) As TaxAmount,IfNull(Sum(VMain.Net_Amount),0) As NetAmount,
                    IfNull(Sum(VMain.TotalCommission),0) As TotalCommission
                    From (" & mQry & ") As VMain
                    GROUP By VMain.DocId 
                    Order By Max(VMain.V_Date_ActualFormat) "
            ElseIf ReportFrm.FGetText(0) = "Item Wise Detail" Then
                mQry = " Select VMain.DocId As SearchCode, Max(VMain.V_Date) As [Invoice Date], Max(VMain.InvoiceNo) As [Invoice No], Max(Vmain.PartyInvoiceDate) as PartyInvoiceDate,
                    Max(VMain.VendorName) As Party, Max(Vmain.PartyGstNo) as PartyGstNo, Max(VMain.HSN) as HSN, Max(VMain.ItemDesc) As Item, Sum(VMain.Qty) As Qty, Max(VMain.Unit) As Unit, 
                    Max(VMain.Rate) As Rate,
                    Sum(VMain.AmountExDiscount) As AmountExDiscount, Max(VMain.DiscountPer) As [Discount Per], 
                    Sum(VMain.Discount) As Discount,
                    Sum(VMain.Amount) as Amount,
                    Sum(VMain.Taxable_Amount) As [Taxable Amount],
                    Sum(VMain.TotalTax) As [Tax Amount],
                    Sum(VMain.Net_Amount) As [Net Amount],
                    Sum(VMain.TotalCommission) as [Total Commission]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.DocId, VMain.Sr 
                    Order By Max(VMain.V_Date_ActualFormat), Max(VMain.InvoiceNo), Vmain.Sr "
            ElseIf ReportFrm.FGetText(0) = "Party Wise Summary" Then
                mQry = " Select VMain.Vendor as SearchCode, Max(VMain.VendorName) As Party, 
                    Sum(VMain.Taxable_Amount) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount, Sum(VMain.Net_Amount) As [Net Amount], Sum(VMain.TotalCommission) As [Total Commission]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.Vendor 
                    Order By Max(VMain.VendorName)"
            ElseIf ReportFrm.FGetText(0) = "HSN Wise Summary" Then
                mQry = " Select VMain.HSN As SearchCode, VMain.HSN, Max(VMain.ItemCategoryDescription) As [Description], 
                    IfNull(Sum(VMain.Taxable_Amount),0) As [Taxable Amount], IfNull(Sum(VMain.Tax1),0) As IGST, IfNull(Sum(VMain.Tax2),0) As CGST, IfNull(Sum(VMain.Tax3),0) As SGST, IfNull(Sum(VMain.Tax4),0) As Cess, IfNull(Sum(VMain.Net_Amount),0) As [Net Amount], Sum(VMain.TotalCommission) As [Total Commission]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.HSN, VMain.ItemCategoryDescription 
                    Order By VMain.HSN, VMain.ItemCategoryDescription "
            ElseIf ReportFrm.FGetText(0) = "Item Wise Summary" Then
                mQry = " Select VMain.Item As SearchCode, Max(VMain.ItemDesc) As [Item], 
                    IfNull(Sum(VMain.Taxable_Amount),0) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount,IfNull(Sum(VMain.Net_Amount),0) As [Net Amount], Sum(VMain.TotalCommission) As [Total Commission]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.Item 
                    Order By Max(VMain.ItemDesc)"
            ElseIf ReportFrm.FGetText(0) = "Item Group Wise Summary" Then
                mQry = " Select VMain.ItemGroup as SearchCode, Max(VMain.ItemGroupDescription) As [Item Group], 
                    IfNull(Sum(VMain.Taxable_Amount),0) As [Taxable Amount],IfNull(Sum(VMain.TotalTax),0) As TaxAmount, IfNull(Sum(VMain.Net_Amount),0) As [Net Amount], Sum(VMain.TotalCommission) As [Total Commission]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.ItemGroup 
                    Order By Max(VMain.ItemGroupDescription)"
            ElseIf ReportFrm.FGetText(0) = "Item Category Wise Summary" Then
                mQry = " Select VMain.ItemCategory as SearchCode, Max(VMain.ItemCategoryDescription) As [Item Category], 
                    IfNull(Sum(VMain.Taxable_Amount),0) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount,IfNull(Sum(VMain.Net_Amount),0) As [Net Amount], Sum(VMain.TotalCommission) As [Total Commission]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.ItemCategory 
                    Order By Max(VMain.ItemCategoryDescription)"
            ElseIf ReportFrm.FGetText(0) = "City Wise Summary" Then
                mQry = " Select VMain.CityCode as SearchCode, Max(VMain.CityName) As [City], 
                    IfNull(Sum(VMain.Taxable_Amount),0) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount,IfNull(Sum(VMain.Net_Amount),0) As [Net Amount], Sum(VMain.TotalCommission) As [Total Commission]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.CityCode 
                    Order By Max(VMain.CityName)"
            ElseIf ReportFrm.FGetText(0) = "State Wise Summary" Then
                mQry = " Select VMain.StateCode as SearchCode, Max(VMain.StateName) As [State], 
                    Sum(VMain.Taxable_Amount) As [Taxable Amount],IfNull(Sum(VMain.TotalTax),0) As TaxAmount, Sum(VMain.Net_Amount) As [Net Amount], Sum(VMain.TotalCommission) As [Total Commission]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.StateCode 
                    Order By Max(VMain.StateName)"
            ElseIf ReportFrm.FGetText(0) = "Agent Wise Summary" Then
                mQry = " Select VMain.AgentCode as SearchCode, Max(VMain.AgentName) As [Agent], 
                    Sum(VMain.Taxable_Amount) As [Taxable Amount],IfNull(Sum(VMain.TotalTax),0) As TaxAmount, Sum(VMain.Net_Amount) As [Net Amount], Sum(VMain.TotalCommission) As [Total Commission]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.AgentCode 
                    Order By Max(VMain.AgentName)"
            ElseIf ReportFrm.FGetText(0) = "Month Wise Summary" Then
                If AgL.PubServerName = "" Then
                    mQry = " Select strftime('%m-%Y',VMain.V_Date_ActualFormat) As SearchCode, strftime('%m-%Y',VMain.V_Date_ActualFormat) As [Month], 
                    Sum(VMain.Taxable_Amount) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount,Sum(VMain.Net_Amount) As [Net Amount], Sum(VMain.TotalCommission) As [Total Commission]
                    From (" & mQry & ") As VMain
                    GROUP By strftime('%m-%Y',VMain.V_Date_ActualFormat)  
                    Order By strftime('%Y',VMain.V_Date_ActualFormat), strftime('%m',VMain.V_Date_ActualFormat)"
                Else
                    mQry = " Select Substring(Convert(NVARCHAR, VMain.V_Date_ActualFormat,103),4,7) As SearchCode, Substring(Convert(NVARCHAR, VMain.V_Date_ActualFormat,103),4,7) As [Month], 
                    Sum(VMain.Taxable_Amount) As [Taxable Amount], IfNull(Sum(VMain.TotalTax),0) As TaxAmount, Sum(VMain.Net_Amount) As [Net Amount], Sum(VMain.TotalCommission) As [Total Commission]
                    From (" & mQry & ") As VMain
                    GROUP By Substring(Convert(NVARCHAR, VMain.V_Date_ActualFormat,103),4,7)
                    Order By Max(Year(VMain.V_Date_ActualFormat)), Max(Month(VMain.V_Date_ActualFormat)) "
                End If
            End If




            DsHeader = AgL.FillData(mQry, AgL.GCn)

            If DsHeader.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            ReportFrm.Text = "Purchase Invoice Report - " + ReportFrm.FGetText(0)
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = "ProcPurchaseReport"

            ReportFrm.ProcFillGrid(DsHeader)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsHeader = Nothing
        End Try
    End Sub
    Public Sub ProcRateListReport(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try
            Dim mCondStr$ = ""
            Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"

            RepTitle = "Rate List Report"

            mCondStr = " Where I.ItemCategory Is Not Null 
                        And I.V_Type = '" & ItemV_Type.Item & "' 
                        And IfNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' "
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemCategory", 0)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemGroup", 1)
            If ReportFrm.FGetText(4) = "Item Master Date" Then
                mCondStr = mCondStr & " AND ( Date(I.EntryDate) Between " & AgL.Chk_Date(CDate(ReportFrm.FGetText(2)).ToString("s")) & " And " & AgL.Chk_Date(CDate(ReportFrm.FGetText(3)).ToString("s")) & " Or Date(I.MoveToLogDate) Between " & AgL.Chk_Date(CDate(ReportFrm.FGetText(2)).ToString("s")) & " And " & AgL.Chk_Date(CDate(ReportFrm.FGetText(3)).ToString("s")) & ") "
            ElseIf ReportFrm.FGetText(4) = "Item Transaction Date" Then
                mCondStr = mCondStr & " AND ( Date(S.LastTrnDate) Between " & AgL.Chk_Date(CDate(ReportFrm.FGetText(2)).ToString("s")) & " And " & AgL.Chk_Date(CDate(ReportFrm.FGetText(3)).ToString("s")) & " Or Date(I.MoveToLogDate) Between " & AgL.Chk_Date(CDate(ReportFrm.FGetText(2)).ToString("s")) & " And " & AgL.Chk_Date(CDate(ReportFrm.FGetText(3)).ToString("s")) & ") "
            ElseIf ReportFrm.FGetText(4) = "Stock" Then
                mCondStr = mCondStr & " And I.Code In (Select Item From Stock Group By Item Having Sum(Qty_Rec-Qty_iss)>0) "
            End If

            mQry = "Select Distinct IfNull(Rt.Description,'Sale Rate') As RateTypeDesc
                    From RateListDetail L 
                    LEFT JOIN RateType Rt On L.RateType = Rt.Code "
            Dim DtRateTypes As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)


            mQry = "Select Ic.Description As ItemCategory, Ig.Description As ItemGroup, I.Specification As Item, Max(I.ProfitMarginPer) as ProfitMarginPer, "

            For I As Integer = 0 To DtRateTypes.Rows.Count - 1
                mQry += " IfNull(Max(Case When IfNull(Rt.Description,'Sale Rate') = '" & DtRateTypes.Rows(I)("RateTypeDesc") & "' Then L.Rate * 1.0 Else 0.00 End),0.00) As [" & DtRateTypes.Rows(I)("RateTypeDesc") & "]  "
                If I <> DtRateTypes.Rows.Count - 1 Then mQry += ", "
            Next

            mQry += " From Item I 
                    LEFT JOIN ItemGroup Ig On I.ItemGroup = Ig.Code
                    LEFT JOIN ItemCategory Ic On I.ItemCategory = Ic.Code
                    LEFT JOIN RateListDetail L On I.Code = L.Item 
                    LEFT JOIN RateType Rt On L.RateType = Rt.Code 
                    "
            If ReportFrm.FGetText(4) = "Item Transaction Date" Then
                mQry += " Left Join (Select Item, Max(V_Date) as LastTrnDate From Stock Group By Item) S On I.Code = S.Item "
            End If
            mQry += mCondStr
            mQry += "GROUP By Ic.Description, Ig.Description, I.Description, I.Specification "

            DsHeader = AgL.FillData(mQry, AgL.GCn)

            If DsHeader.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            ReportFrm.Text = "Rate List Report"
            ReportFrm.ClsRep = Me
            ReportFrm.IsHideZeroColumns = False

            ReportFrm.ProcFillGrid(DsHeader)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsHeader = Nothing
        End Try
    End Sub
    Public Sub ProcSalesAgentAndSalesRepresentativeCommissionReport(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try
            Dim mCondStr$ = ""
            Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"
            Dim mCommissionPer As Double


            RepTitle = "Sales Agent Commission Report"
            Dim mFieldName As String = "Agent"

            If ReportFrm.FGetText(12) = "Sales Representative" Then
                RepTitle = "Sales Representative Commission Report"
                mFieldName = "Salesrepresentative"
            Else
                RepTitle = "Sales Agent Commission Report"
                mFieldName = "Agent"
            End If

            If mFilterGrid IsNot Nothing And mGridRow IsNot Nothing Then
                If mGridRow.DataGridView.Columns.Contains("Search Code") = True Then
                    If mFilterGrid.Item(GFilter, 0).Value = mFieldName & " Wise Summary" Then
                        mFilterGrid.Item(GFilter, 0).Value = "Party Wise Summary"
                        mFilterGrid.Item(GFilter, 6).Value = mGridRow.Cells(mFieldName).Value
                        mFilterGrid.Item(GFilterCode, 6).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = "Party Wise Summary" Then
                        mFilterGrid.Item(GFilter, 0).Value = "Payment Wise Detail"
                        mFilterGrid.Item(GFilter, 4).Value = mGridRow.Cells("Party").Value
                        mFilterGrid.Item(GFilterCode, 4).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = mFieldName & " Wise Periodic Summary" Then
                        mFilterGrid.Item(GFilter, 0).Value = "Party Wise Periodic Summary"
                        mFilterGrid.Item(GFilter, 6).Value = mGridRow.Cells(mFieldName).Value
                        mFilterGrid.Item(GFilterCode, 6).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = "Payment Wise Detail" Then
                        ClsMain.FOpenForm(mGridRow.Cells("Search Code").Value, ReportFrm)
                        ReportFrm.FiterGridCopy_Arr.RemoveAt(ReportFrm.FiterGridCopy_Arr.Count - 1)
                        Exit Sub
                    Else
                        Exit Sub
                    End If
                Else
                    Exit Sub
                End If
            End If

            mCondStr = "  "
            mCondStr = mCondStr & " And Party.Nature='Customer' "
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Subcode", 4)

            If AgL.StrCmp(ClsMain.FDivisionNameForCustomization(6), "SADHVI") And AgL.StrCmp(AgL.PubDBName, "SADHVI") Then
                mCondStr = mCondStr & " And IfNull(ReferenceLh.V_Type,L.V_Type) Not In ('IMR') "
            Else
                mCondStr = mCondStr & ReportFrm.GetWhereCondition("IfNull(ReferenceLh.V_Type,L.V_Type)", 5)
            End If

            'mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.V_Type", 5)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("LTV." & mFieldName, 6)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("City.CityCode", 7)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("Party.Area", 8)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("City.State", 9)
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("L.Site_Code", 10), "''", "'")
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("L.DivCode", 11), "''", "'")

            mCommissionPer = Val(ReportFrm.FGetText(3))

            'If ReportFrm.FGetText(8) <> "All" Then
            '    mCondStr += " And H." & mFieldName = '" & ReportFrm.FGetCode(8) & "' "
            'End If

            'mCondStr = mCondStr & ReportFrm.GetWhereCondition("H." & mFieldName, 8)
            If ReportFrm.FGetText(0) = "Party Wise Periodic Summary" Or ReportFrm.FGetText(0) = mFieldName & " Wise Periodic Summary" Then
                mQry = "
                        SELECT X.PartyCode, X.PartyName, X." & mFieldName & "Code, X." & mFieldName & "Name, X.Opening, X.CurrentDr, X.DebitNote, X.Receipts,
                        (CASE WHEN X.Receipts > 0 AND X.Opening > 0 THEN CASE WHEN X.Opening - X.DebitNote > 0 And X.Receipts - X.Opening + X.DebitNote  >= 0 Then X.Opening - X.DebitNote ELSE X.Receipts - X.DebitNote END ELSE 0 End) AS OldReceipts,
                        (CASE WHEN X.Receipts > 0 THEN 
                                                CASE When X.Receipts - (X.Opening - X.DebitNote + X.CurrentDr) <= 0 THEN X.Receipts - (CASE WHEN X.Receipts > 0 AND X.Opening > 0 THEN CASE WHEN X.Opening - X.DebitNote > 0 And X.Receipts - X.Opening + X.DebitNote  >= 0 Then X.Opening - X.DebitNote ELSE X.Receipts - X.DebitNote END ELSE 0 End)							   
                                                ELSE (X.Opening - X.DebitNote + X.CurrentDr) END ELSE 0 End) AS CurrentReceipts,
                        (CASE WHEN X.Receipts >0 And X.Receipts - (X.Opening - X.DebitNote + X.CurrentDr) > 0  THEN X.Receipts - (X.Opening - X.DebitNote + X.CurrentDr) ELSE 0 END) AS AdvanceReceipts,
                        X.Closing
                        FROM
                        (
                        SELECT L.SubCode AS PartyCode, Max(Party.Name) AS PartyName, " & mFieldName & ".Code AS " & mFieldName & "Code, Max(" & mFieldName & ".Name) AS " & mFieldName & "Name,
                        Sum(CASE WHEN Date(L.V_Date) < " & AgL.Chk_Date(ReportFrm.FGetText(1)) & " Then L.AmtDr - L.AmtCr ELSE 0 End) AS Opening, 
                        Sum(CASE WHEN VT.Category <> 'JV' And Date(L.V_Date) >= " & AgL.Chk_Date(ReportFrm.FGetText(1)) & " AND Date(L.V_Date) <= " & AgL.Chk_Date(ReportFrm.FGetText(2)) & "  Then L.AmtDr  ELSE 0 End) AS CurrentDr, 
                        Sum(CASE WHEN Date(L.V_Date) >= " & AgL.Chk_Date(ReportFrm.FGetText(1)) & " AND Date(L.V_Date) <= " & AgL.Chk_Date(ReportFrm.FGetText(2)) & "  Then L.AmtCr  ELSE 0 End) AS CurrentCr, 
                        Sum(CASE WHEN VT.Category <> 'RCT' AND Date(L.V_Date) >= " & AgL.Chk_Date(ReportFrm.FGetText(1)) & " AND Date(L.V_Date) <= " & AgL.Chk_Date(ReportFrm.FGetText(2)) & "  Then L.AmtCr  ELSE 0 End) AS DebitNote, 
                        IfNull(Sum(CASE WHEN VT.Category ='RCT' AND Date(L.V_Date) >= " & AgL.Chk_Date(ReportFrm.FGetText(1)) & " AND Date(L.V_Date) <= " & AgL.Chk_Date(ReportFrm.FGetText(2)) & "  Then L.AmtCr  ELSE 0 End),0) - IfNull(Sum(CASE WHEN VT.Category ='JV' AND Date(L.V_Date) >= " & AgL.Chk_Date(ReportFrm.FGetText(1)) & " AND Date(L.V_Date) <= " & AgL.Chk_Date(ReportFrm.FGetText(2)) & "  Then L.AmtDr  ELSE 0 End),0)   AS Receipts, 
                        Sum(CASE WHEN Date(L.V_Date) <= " & AgL.Chk_Date(ReportFrm.FGetText(2)) & " Then L.AmtDr - L.AmtCr ELSE 0 End) AS Closing 
                        FROM Ledger L
                        LEFT JOIN viewHelpSubgroup Party ON L.SubCode = Party.Code
                        LEFT JOIN Voucher_type vt ON L.V_Type = vt.V_Type 
                        LEFT JOIN SubGroupType Partyt ON Party.SubgroupType = Partyt.SubgroupType 
                        Left Join (Select SILTV.Subcode, Max(SILTV." & mFieldName & ") as " & mFieldName & " From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode) as LTV On Party.code = LTV.Subcode
                        Left Join viewHelpSubGroup " & mFieldName & " On LTV." & mFieldName & " = " & mFieldName & ".Code 
                        Left Join City On Party.CityCode = City.CityCode 
                        Left Join State On City.State = State.Code
                        LEFT JOIN LedgerHead ReferenceLh On IfNull(L.ReferenceDocId,'') = ReferenceLh.DocId
                        Left Join TransactionReferences Trd With (NoLock) On L.DocID= Trd.DocId And L.V_Sno = IfNull(Trd.DocIDSr, L.V_Sno)
                        Left Join TransactionReferences Trr With (NoLock) On L.DocID= Trr.ReferenceDocId And L.V_Sno = IfNull(Trr.ReferenceSr, L.V_Sno)
                        WHERE Isnull(Partyt.Parent, Partyt.SubgroupType) ='Customer' And IfNull(Trd.Type,'')<>'Cancelled' And IfNull(Trr.Type,'')<>'Cancelled'  And Date(L.V_Date) <= " & AgL.Chk_Date(ReportFrm.FGetText(2)) & " " & mCondStr & "
                        GROUP BY L.SubCode , " & mFieldName & ".Code
                        ) AS X
                        "
            Else
                mQry = " SELECT L.DocID, L.V_Sno, strftime('%d/%m/%Y', L.V_Date) As V_Date, L.V_Date As V_Date_ActualFormat,
                    L.Subcode as Party, Party.Name As PartyName, LTV." & mFieldName & " As " & mFieldName & "Code, " & mFieldName & ".Name As " & mFieldName & "Name , 
                    City.CityCode, City.CityName, State.Code As StateCode, State.Description As StateName,
                    L.DivCode || L.Site_Code || '-' || L.V_Type || '-' || L.RecId as VoucherNo, L.RecId, 
                    L.AmtCr as Amount, 0.00 As Internal, 0.00 AS JvAmount, " & mCommissionPer & " as CommissionPer, L.AmtCr*" & mCommissionPer & "/100 as Commission
                    FROM Ledger L                     
                    Left Join viewHelpSubgroup Party On L.Subcode = Party.Code 
                    Left Join (Select SILTV.Subcode, Max(SILTV." & mFieldName & ") as " & mFieldName & " From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode) as LTV On Party.code = LTV.Subcode
                    Left Join viewHelpSubGroup " & mFieldName & " On LTV." & mFieldName & " = " & mFieldName & ".Code 
                    Left Join City On Party.CityCode = City.CityCode 
                    Left Join State On City.State = State.Code
                    LEFT JOIN Voucher_Type Vt On L.V_Type = Vt.V_Type 
                    LEFT JOIN LedgerHead ReferenceLh On IfNull(L.ReferenceDocId,'') = ReferenceLh.DocId
                    Where (VT.Category='RCT' Or VT.NCAT='OB') And L.AmtCr > 0 
                    AND Date(L.V_Date) Between " & AgL.Chk_Date(CDate(ReportFrm.FGetText(1)).ToString("s")) & " And " & AgL.Chk_Date(CDate(ReportFrm.FGetText(2)).ToString("s")) & "
                    " & mCondStr
                mQry = mQry & " Union All "
                mQry = mQry & " SELECT L.DocID, L.V_Sno, strftime('%d/%m/%Y', L.V_Date) As V_Date, L.V_Date As V_Date_ActualFormat,
                    L.Subcode as Party, Party.Name As PartyName, LTV." & mFieldName & " As " & mFieldName & "Code, " & mFieldName & ".Name As " & mFieldName & "Name , 
                    City.CityCode, City.CityName, State.Code As StateCode, State.Description As StateName,
                    L.DivCode || L.Site_Code || '-' || L.V_Type || '-' || L.RecId as VoucherNo, L.RecId, 
                    0 as Amount, 0.00 As Internal, L.AmtDr as JVAmount, " & mCommissionPer & " as CommissionPer, -1.0*(L.AmtDr*" & mCommissionPer & "/100) as Commission
                    FROM Ledger L                     
                    Left Join viewHelpSubgroup Party On L.Subcode = Party.Code 
                    Left Join (Select SILTV.Subcode, Max(SILTV." & mFieldName & ") as " & mFieldName & " From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode) as LTV On Party.code = LTV.Subcode
                    Left Join viewHelpSubGroup " & mFieldName & " On LTV." & mFieldName & " = " & mFieldName & ".Code 
                    Left Join City On Party.CityCode = City.CityCode 
                    Left Join State On City.State = State.Code
                    LEFT JOIN Voucher_Type Vt On L.V_Type = Vt.V_Type 
                    LEFT JOIN LedgerHead ReferenceLh On IfNull(L.ReferenceDocId,'') = ReferenceLh.DocId
                    Where VT.Category='JV' And VT.NCAT<>'OB' And L.Amtdr > 0 
                    AND Date(L.V_Date) Between " & AgL.Chk_Date(CDate(ReportFrm.FGetText(1)).ToString("s")) & " And " & AgL.Chk_Date(CDate(ReportFrm.FGetText(2)).ToString("s")) & "
                    " & mCondStr
            End If



            If ReportFrm.FGetText(0) = "Payment Wise Detail" Then
                mQry = " Select VMain.DocId As SearchCode, Max(VMain.V_Date) As VoucherDate, Max(VMain.VoucherNo) as VoucherNo,
                    Max(VMain.PartyName) As Party, Max(VMain." & mFieldName & "Name) As [" & mFieldName & "], Sum(VMain.Amount) as Amount, Sum(Vmain.JvAmount) as JvAmount, Max(VMain.CommissionPer) as CommissionPer, Sum(VMain.Commission) as CommissionAmt
                    From (" & mQry & ") As VMain
                    GROUP By VMain.DocId, VMain.V_Sno 
                    Order By VoucherDate, VoucherNo  "
            ElseIf ReportFrm.FGetText(0) = "" & mFieldName & " Wise Summary" Then
                mQry = " Select VMain." & mFieldName & "Code As SearchCode, Max(VMain." & mFieldName & "Name) As [" & mFieldName & "], 
                    Sum(VMain.Amount) As [Amount], Sum(VMain.Internal) As [Internal], Sum(Vmain.JvAmount) as JvAmount, Max(VMain.CommissionPer) as [CommissionPer], Sum(VMain.Commission) As [CommissionAmt]
                    From (" & mQry & ") As VMain
                    GROUP By VMain." & mFieldName & "Code 
                    Order By [" & mFieldName & "]"
            ElseIf ReportFrm.FGetText(0) = "Party Wise Summary" Then
                mQry = " Select VMain.Party As SearchCode, Max(VMain.PartyName) as [Party], Max(VMain." & mFieldName & "Name) As [" & mFieldName & "], 
                    Sum(VMain.Amount) As [Amount], Sum(VMain.Internal) As [Internal], Sum(Vmain.JvAmount) as JvAmount, Max(VMain.CommissionPer) as [CommissionPer], Sum(VMain.Commission) As [CommissionAmt]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.Party
                    Order By [Party]"
            ElseIf ReportFrm.FGetText(0) = "Party Wise Periodic Summary" Then
                mQry = " Select VMain.PartyCode As SearchCode, Max(VMain.PartyName) as [Party], Max(VMain." & mFieldName & "Name) As [" & mFieldName & "], 
                    Sum(VMain.Opening) As [Opening], Sum(VMain.CurrentDr) as [CurrentDr], Sum(VMain.DebitNote) as [DebitNotes],
                    Sum(VMain.Receipts) as [Receipts],Sum(VMain.OldReceipts) as [OldReceipts], Sum(VMain.CurrentReceipts) as [CurrentReceipts],
                    Sum(VMain.AdvanceReceipts) as [AdvanceReceipts],Sum(VMain.Closing) as [Closing],
                    " & mCommissionPer & " as CommissionPer, Sum(VMain.CurrentReceipts)*" & mCommissionPer & "/100 as Commission
                    From (" & mQry & ") As VMain
                    GROUP By VMain.PartyCode, VMain." & mFieldName & "Code
                    Order By [Party],[" & mFieldName & "]"
            ElseIf ReportFrm.FGetText(0) = "" & mFieldName & " Wise Periodic Summary" Then
                mQry = " Select VMain." & mFieldName & "Code As SearchCode, Max(VMain." & mFieldName & "Name) As [" & mFieldName & "], 
                    Sum(VMain.Opening) As [Opening], Sum(VMain.CurrentDr) as [CurrentDr], Sum(VMain.DebitNote) as [DebitNotes],
                    Sum(VMain.Receipts) as [Receipts],Sum(VMain.OldReceipts) as [OldReceipts], Sum(VMain.CurrentReceipts) as [CurrentReceipts],
                    Sum(VMain.AdvanceReceipts) as [AdvanceReceipts],Sum(VMain.Closing) as [Closing],
                    " & mCommissionPer & " as CommissionPer, Sum(VMain.CurrentReceipts)*" & mCommissionPer & "/100 as Commission
                    From (" & mQry & ") As VMain
                    GROUP By VMain." & mFieldName & "Code
                    Order By [" & mFieldName & "]"
            End If




            DsHeader = AgL.FillData(mQry, AgL.GCn)

            If DsHeader.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            ReportFrm.Text = mFieldName & " Commission On Payment - " + ReportFrm.FGetText(0)
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = "ProcSalesAgentAndSalesRepresentativeCommissionReport"

            ReportFrm.ProcFillGrid(DsHeader)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsHeader = Nothing
        End Try
    End Sub

    'Public Sub ProcStockReport(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
    '                            Optional mGridRow As DataGridViewRow = Nothing)
    '    Try
    '        Dim mCondStr$ = ""
    '        Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"



    '        RepTitle = "Stock Report"

    '        If mFilterGrid IsNot Nothing And mGridRow IsNot Nothing Then
    '            If mGridRow.DataGridView.Columns.Contains("Search Code") = True Then
    '                If mFilterGrid.Item(GFilter, 0).Value = "Stock Balance" Then
    '                    mFilterGrid.Item(GFilter, 0).Value = "Stock Ledger"
    '                    mFilterGrid.Item(GFilter, 7).Value = mGridRow.Cells("Item").Value
    '                    mFilterGrid.Item(GFilterCode, 7).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
    '                ElseIf mFilterGrid.Item(GFilter, 0).Value = "Stock Summary" Then
    '                    mFilterGrid.Item(GFilter, 0).Value = "Stock Ledger"
    '                    mFilterGrid.Item(GFilter, 7).Value = mGridRow.Cells("Item").Value
    '                    mFilterGrid.Item(GFilterCode, 7).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
    '                ElseIf mFilterGrid.Item(GFilter, 0).Value = "Stock Ledger" Then
    '                    ClsMain.FOpenForm(mGridRow.Cells("Search Code").Value, ReportFrm)
    '                    ReportFrm.FiterGridCopy_Arr.RemoveAt(ReportFrm.FiterGridCopy_Arr.Count - 1)
    '                    Exit Sub
    '                Else
    '                    Exit Sub
    '                End If
    '            Else
    '                Exit Sub
    '            End If
    '        End If

    '        mCondStr = "  "
    '        mCondStr = mCondStr & "  "

    '        mCondStr = mCondStr & "And Sku.ItemType Not In ('" & ItemTypeCode.ServiceProduct & "','" & ItemTypeCode.InternalProduct & "')"
    '        mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Godown", 4)
    '        mCondStr = mCondStr & ReportFrm.GetWhereCondition("Sku.ItemCategory", 5)
    '        mCondStr = mCondStr & ReportFrm.GetWhereCondition("Sku.ItemGroup", 6)
    '        mCondStr = mCondStr & ReportFrm.GetWhereCondition("Sku.Code", 7)
    '        mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("L.Site_Code", 8), "''", "'")
    '        mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("L.Div_Code", 9), "''", "'")


    '        If ReportFrm.FGetText(0) = "Stock Ledger" Then
    '            If ReportFrm.GetWhereCondition("Sku.Code", 7) = "" Then
    '                MsgBox("Stock Ledger can be filled for single item only.")
    '                Exit Sub
    '            ElseIf InStr(ReportFrm.GetWhereCondition("Sku.Code", 7), "',") > 0 Then
    '                MsgBox("Stock Ledger can be filled for single item only.")
    '                Exit Sub
    '            End If
    '        End If



    '        mQry = "
    '                SELECT ' Opening' as DocID, ' Opening' V_Type, ' 0' as RecId, strftime('%d/%m/%Y', " & AgL.Chk_Date(ReportFrm.FGetText(1)) & ")  V_Date, " & AgL.Chk_Date(ReportFrm.FGetText(1)) & "  V_Date_ActualFormat
    '                , Null as PartyName, Max(Location.Name) as LocationName, Sku.Code AS ItemCode, Max(Sku.Description) AS ItemName, Max(Sku.Specification) as ItemSpecification
    '                , Max(IG.Code) as ItemGroupCode, Max(IG.Description) as ItemGroupName
    '                , Max(IC.Code) as ItemCategoryCode, Max(IC.Description) as ItemCategoryName 
    '                , Max(I.Code) as BaseItemCode, Max(I.Specification) as BaseItemName 
    '                , Max(D1.Code) as Dimension1Code, Max(D1.Specification) as Dimension1Name 
    '                , Max(D2.Code) as Dimension2Code, Max(D2.Specification) as Dimension2Name 
    '                , Max(D3.Code) as Dimension3Code, Max(D3.Specification) as Dimension3Name 
    '                , Max(D4.Code) as Dimension4Code, Max(D4.Specification) as Dimension4Name 
    '                , Max(Size.Code) as SizeCode, Max(Size.Description) as SizeName, 
    '                Max(Sku.Unit) as Unit, Max(U.DecimalPlaces) as DecimalPlaces
    '                ,Sum(L.Qty_Rec - L.Qty_Iss) AS Opening, 
    '                0 AS Qty_Rec, 
    '                0 AS Qty_Iss, 
    '                Sum(L.Qty_Rec - L.Qty_Iss) AS Closing, 0 as TransactionRate, Max(Sku.PurchaseRate) as ValuationRate, Sum(L.Qty_Rec - L.Qty_Iss)*Max(Sku.PurchaseRate) as Amount
    '                FROM Stock L
    '                LEFT JOIN Item Sku ON L.Item = Sku.Code
    '                Left Join Item IG On Sku.ItemGroup = IG.Code
    '                Left Join Item IC On Sku.ItemCategory = IC.Code
    '                LEFT JOIN Item I ON Sku.BaseItem = I.Code
    '                LEFT JOIN Item D1 ON Sku.Dimension1 = D1.Code
    '                LEFT JOIN Item D2 ON Sku.Dimension2 = D2.Code
    '                LEFT JOIN Item D3 ON Sku.Dimension3 = D3.Code
    '                LEFT JOIN Item D4 ON Sku.Dimension4 = D4.Code
    '                LEFT JOIN Item Size ON Sku.Size = Size.Code
    '                Left Join Unit U On Sku.Unit = U.Code
    '                Left Join viewHelpSubgroup Sg On L.Subcode = Sg.Code
    '                LEFT JOIN Voucher_type vt ON L.V_Type = vt.V_Type 
    '                Left Join Subgroup Location On L.Godown = Location.Subcode
    '                WHERE L.V_Date < " & AgL.Chk_Date(ReportFrm.FGetText(1)) & " " & mCondStr & "
    '                GROUP BY Sku.Code , L.Godown
    '                Union All
    '                SELECT L.DocID, L.V_Type, L.RecId, 
    '                strftime('%d/%m/%Y', L.V_Date) As V_Date, L.V_Date As V_Date_ActualFormat
    '                , Sg.Name as PartyName, Location.Name as LocationName,Sku.Code AS ItemCode, Sku.Description AS ItemName, Sku.Specification as ItemSpecification
    '                , IG.Code as ItemGroupCode, IG.Description as ItemGroupName
    '                , IC.Code as ItemCategoryCode, IC.Description as ItemCategoryName 
    '                , I.Code as BaseItemCode, I.Specification as BaseItemName 
    '                , D1.Code as Dimension1Code, D1.Specification as Dimension1Name 
    '                , D2.Code as Dimension2Code, D2.Specification as Dimension2Name 
    '                , D3.Code as Dimension3Code, D3.Specification as Dimension3Name 
    '                , D4.Code as Dimension4Code, D4.Specification as Dimension4Name 
    '                , Size.Code as SizeCode, Size.Description as SizeName, 
    '                Sku.Unit, U.DecimalPlaces
    '                ,0 AS Opening 
    '                ,L.Qty_Rec AS Qty_Rec, 
    '                L.Qty_Iss As Qty_Iss, 
    '                L.Qty_Rec - L.Qty_Iss AS Closing, L.Rate as TransactionRate, Sku.PurchaseRate as ValuationRate,  (L.Qty_Rec - L.Qty_Iss)*Sku.PurchaseRate as Amount
    '                FROM Stock L
    '                LEFT JOIN Item Sku ON L.Item = Sku.Code
    '                Left Join Item IG On Sku.ItemGroup = IG.Code
    '                Left Join Item IC On Sku.ItemCategory = IC.Code
    '                LEFT JOIN Item I ON Sku.BaseItem = I.Code
    '                LEFT JOIN Item D1 ON Sku.Dimension1 = D1.Code
    '                LEFT JOIN Item D2 ON Sku.Dimension2 = D2.Code
    '                LEFT JOIN Item D3 ON Sku.Dimension3 = D3.Code
    '                LEFT JOIN Item D4 ON Sku.Dimension4 = D4.Code
    '                LEFT JOIN Item Size ON Sku.Size = Size.Code
    '                Left Join Unit U On Sku.Unit = U.Code
    '                Left Join viewHelpSubgroup Sg on L.Subcode = Sg.Code
    '                LEFT JOIN Voucher_type vt ON L.V_Type = vt.V_Type 
    '                Left Join Subgroup Location On L.Godown = Location.Subcode
    '                WHERE Date(L.V_Date) >= " & AgL.Chk_Date(ReportFrm.FGetText(1)) & " And Date(L.V_Date) <= " & AgL.Chk_Date(ReportFrm.FGetText(2)) & " " & mCondStr & "                    
    '            "


    '        If ReportFrm.FGetText(0) = "Stock Summary" Then
    '            mQry = " Select VMain.ItemCode As SearchCode 
    '                , Max(VMain.ItemCategoryName) as ItemCategory
    '                , Max(VMain.ItemGroupName) ItemGroup
    '                , Max(VMain.ItemSpecification) as Item
    '                , IfNull(Max(VMain.Dimension1Name),'') as Dimension1
    '                , IfNull(Max(VMain.Dimension2Name),'') as Dimension2
    '                , IfNull(Max(VMain.Dimension3Name),'') as Dimension3
    '                , IfNull(Max(VMain.Dimension4Name),'') as Dimension4
    '                , IfNull(Max(VMain.SizeName),'') as Size
    '                , Max(VMain.Unit) as Unit,
    '                Round(Sum(VMain.Opening),Max(VMain.DecimalPlaces)) As [Opening], Round(Sum(VMain.Qty_Rec),Max(VMain.DecimalPlaces)) as [ReceiveQty], Round(Sum(VMain.Qty_Iss),Max(VMain.DecimalPlaces)) as [IssueQty],                    
    '                Round(Sum(VMain.Closing),Max(VMain.DecimalPlaces)) as [Closing], Sum(VMain.Amount) as Amount
    '                From (" & mQry & ") As VMain
    '                GROUP By VMain.ItemCode
    '                Order By Max(ItemName),Max(ItemGroupName),Max(ItemCategoryName) "
    '        ElseIf ReportFrm.FGetText(0) = "Stock Balance" Then
    '            mQry = " Select VMain.ItemCode As SearchCode 
    '                , Max(VMain.ItemCategoryName) as ItemCategory
    '                , Max(VMain.ItemGroupName) ItemGroup
    '                , Max(VMain.ItemSpecification) as Item
    '                , IfNull(Max(VMain.Dimension1Name),'')  as Dimension1
    '                , IfNull(Max(VMain.Dimension2Name),'')  as Dimension2
    '                , IfNull(Max(VMain.Dimension3Name),'')  as Dimension3
    '                , IfNull(Max(VMain.Dimension4Name),'')  as Dimension4
    '                , IfNull(Max(VMain.SizeName),'') as Size, 
    '                Max(VMain.Unit) as Unit,                   
    '                Sum(VMain.Closing) as [Balance], Sum(VMain.Amount) as Amount                    
    '                From (" & mQry & ") As VMain
    '                GROUP By VMain.ItemCode
    '                Having Sum(VMain.Closing) <> 0 
    '                Order By [Item],[ItemGroup],[ItemCategory] "
    '        Else
    '            mQry = " Select VMain.DocID As SearchCode 
    '                , Max(VMain.V_Date) As [Doc Date], Max(VMain.V_Type) as DocType, Max(VMain.RecId) As [Doc No]
    '                , Max(Vmain.PartyName) as PartyName, Max(VMain.LocationName) As [Location Name]
    '                , Sum(VMain.Qty_Rec) as [Receive Qty]
    '                , Sum(VMain.Qty_Iss) as [Issue Qty]
    '                , Sum(VMain.Closing) as [Balance] 
    '                , Max(VMain.Unit) as Unit
    '                , Max(VMain.TransactionRate) as TransactionRate                    
    '                , Max(VMain.ValuationRate) as ValuationRate                    
    '                , Sum(VMain.Amount) as Amount                    
    '                From (" & mQry & ") As VMain
    '                GROUP By VMain.DocID, VMain.ItemCode,VMain.TransactionRate, VMain.ValuationRate                     
    '                Order By Max(VMain.ItemName), Max(VMain.V_Date_ActualFormat), Max(Cast(Replace(VMain.RecID,'-','') as Integer)), Max(VMain.Qty_Rec), Max(VMain.Qty_Iss)"
    '        End If


    '        DsHeader = AgL.FillData(mQry, AgL.GCn)


    '        If DsHeader.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

    '        ReportFrm.Text = "Stock Report - " + ReportFrm.FGetText(0)
    '        ReportFrm.ClsRep = Me
    '        ReportFrm.ReportProcName = "ProcStockReport"

    '        ReportFrm.ProcFillGrid(DsHeader)

    '        If ReportFrm.FGetText(0) = "Stock Ledger" Then
    '            Dim I As Integer
    '            Dim mRunningBal As Double
    '            mRunningBal = 0
    '            For I = 0 To ReportFrm.DGL1.RowCount - 1
    '                mRunningBal += Val(ReportFrm.DGL1.Item("Balance", I).Value)
    '                ReportFrm.DGL1.Item("Balance", I).Value = mRunningBal
    '            Next
    '        End If

    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '        DsHeader = Nothing
    '    End Try
    'End Sub

    Public Sub ProcCreditorsOutstaningReport(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try
            Dim mCondStr$ = ""
            Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"
            Dim mLeavergeDays As Double
            Dim strSql As String
            Dim strDate As String

            Dim mPendingBillCount As Integer

            RepTitle = "Creditors Outstanding Report"

            If mFilterGrid IsNot Nothing And mGridRow IsNot Nothing Then
                If mGridRow.DataGridView.Columns.Contains("Search Code") = True Then
                    If mFilterGrid.Item(GFilter, 0).Value = "Party Wise Summary" Then
                        Dim mSearchCodes As String()
                        mSearchCodes = mGridRow.Cells("Search Code").Value.ToString.Split("^")

                        mFilterGrid.Item(GFilter, 4).Value = mGridRow.Cells("Party").Value
                        mFilterGrid.Item(GFilterCode, 4).Value = "'" + mSearchCodes(0) + "'" '"'" + mGridRow.Cells("Search Code").Value + "'"

                        mFilterGrid.Item(GFilter, 11).Value = mGridRow.Cells("Division").Value
                        mFilterGrid.Item(GFilterCode, 11).Value = "'" + mSearchCodes(1) + "'" '"'" + mGridRow.Cells("Search Code").Value + "'"

                        mFilterGrid.Item(GFilter, 0).Value = "Invoice Wise Detail"
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = "Invoice Wise Detail" Then
                        ClsMain.FOpenForm(mGridRow.Cells("Search Code").Value, ReportFrm)
                        ReportFrm.FiterGridCopy_Arr.RemoveAt(ReportFrm.FiterGridCopy_Arr.Count - 1)
                        Exit Sub
                    End If
                Else
                    Exit Sub
                End If
            End If


            strDate = AgL.Chk_Text(CDate(ReportFrm.FGetText(2)).ToString("s"))

            mCondStr = "  "
            mCondStr = mCondStr & " AND Date(LG.V_Date) <= " & AgL.Chk_Date(CDate(ReportFrm.FGetText(2)).ToString("s")) & " "
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("LG.Subcode", 4)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("SG.GroupCode", 5)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("LTV.Agent", 6)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("CT.CityCode", 7)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("Ct.State", 8)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("Party.Area", 9)
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("LG.Site_Code", 10), "''", "'")
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("LG.DivCode", 11), "''", "'")

            mLeavergeDays = Val(ReportFrm.FGetText(3))



            If ReportFrm.FGetText(1) = "FIFO" Then

                Try
                    mQry = "Drop Table #TempRecord"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
                Catch ex As Exception
                End Try

                mQry = " CREATE Temporary TABLE #TempRecord (DocId  nvarchar(21),RecId  nvarchar(50),V_Date  DateTime,subcode nvarchar(30),"
                mQry += " PartyName nvarchar(500),BillAmt FLOAT,PendingAmt FLOAT, cummAmt Float,Status  nvarchar(20), Site_Code  nvarchar(2), Div_Code nVarchar(1),
                          PartyCity  nvarchar(200),Narration  varchar(2000),V_type  nvarchar(20) );	"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

                Dim SubCode As String = "", Party As String = "", PCity As String = "", SiteCode As String = "", DivCode As String = ""
                Dim Dr As Double = 0, Adv As Double = 0
                Dim runningDr As Double = 0

                Dim CurrTempPayment As DataTable = Nothing

                mQry = " SELECT LG.SubCode,max(Sg.name) as PartyName,max(CT.CityName) as PCity,IfNull(sum(AmtDr),0) AS AmtDr,
                    Case When IfNull(sum(AmtDr),0) > IfNull(sum(AmtCr),0) Then (IfNull(sum(AmtDr),0) - IfNull(sum(AmtCr),0)) Else  0   End As Advance ,
                    Max(LG.Site_Code) As SiteCode, LG.DivCode  
                    FROM Ledger LG 
                    LEFT JOIN SubGroup SG On SG.SubCode =LG.SubCode  
                    Left Join (Select SILTV.Subcode, Max(SILTV.Agent) as Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode) as LTV On Sg.SubCode = LTV.Subcode
                    LEFT JOIN City CT On SG.CityCode  =CT.CityCode Where 1=1 " + mCondStr + " And SG.Nature ='Supplier'
                    GROUP BY LG.SubCode, LG.DivCode 
                    Having IfNull(sum(AmtDr),0) - IfNull(sum(AmtCr),0) < 0 "
                CurrTempPayment = AgL.FillData(mQry, AgL.GCn).Tables(0)

                For I As Integer = 0 To CurrTempPayment.Rows.Count - 1
                    SubCode = AgL.XNull(CurrTempPayment.Rows(I)("SubCode"))
                    Party = AgL.XNull(CurrTempPayment.Rows(I)("PartyName"))
                    PCity = AgL.XNull(CurrTempPayment.Rows(I)("PCity"))
                    Dr = AgL.XNull(CurrTempPayment.Rows(I)("AmtDr"))
                    Adv = AgL.XNull(CurrTempPayment.Rows(I)("Advance"))
                    SiteCode = AgL.XNull(CurrTempPayment.Rows(I)("SiteCode"))
                    DivCode = AgL.XNull(CurrTempPayment.Rows(I)("DivCode"))

                    Dim DrAmt As Double = 0, tempval As Double = 0, CrAmt As Double = 0
                    Dim DocId As String = "", RecId As String = "", Supplier As String = "", PartyName As String = "", Site As String = "", Division As String = "", City As String = "", Narr As String = "", VType As String = ""
                    Dim V_Date As String = ""

                    tempval = 0

                    Dim curr_TempAdjust As DataTable = Nothing

                    mQry = " SELECT  IfNull(LG.DocId,'') AS DocId, LG.V_Type,'" & IIf(AgL.PubPrintDivisionShortNameOnDocumentsYn, AgL.PubDivShortName, "") & IIf(AgL.PubPrintSiteShortNameOnDocumentsYn, AgL.PubSiteShortName, "") & "' || (Case When VT.Short_Name Is Not Null Then VT.Short_Name Else '' End) ||  IfNull(PI.VendorDocNo,LG.RecId) As RecId,LG.V_date AS V_date,IfNull(LG.SubCode,'') AS Subcode,
                IfNull(SG.Name,'') AS PartyName, IfNull(Lg.AmtCr,0) AS AmtCr,IfNull(Lg.Site_Code,0) AS Site_Code, LG.DivCode ,IfNull(Ct.CityName,'') as City,IfNull(Lg.Narration,'') as Narr,IfNull(Lg.V_type,'') as V_type  
                FROM Ledger LG LEFT JOIN SubGroup SG On  SG.SubCode=LG.SubCode 
                Left Join (Select SILTV.Subcode, Max(SILTV.Agent) as Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode) as LTV On Sg.SubCode = LTV.Subcode
                LEFT JOIN City CT On Ct.CityCode =Sg.CityCode  
                Left Join Voucher_Type Vt On Lg.V_Type = Vt.V_Type
                Left Join PurchInvoice PI On LG.DocID = PI.DocID
                Where Date(LG.V_Date) <= " & AgL.Chk_Date(CDate(ReportFrm.FGetText(2)).ToString("s")) & " 
                And IfNull(Lg.AmtCr, 0) <> 0 And LG.SubCode = '" & SubCode & "' And LG.DivCode='" & DivCode & "'  "
                    If AgL.PubServerName = "" Then
                        mQry = mQry & " Order By Lg.V_Date, Try_Parse(Replace(LG.RecId,'-','') as Integer) "
                    Else
                        mQry = mQry & " Order By Lg.V_Date, Cast((Case When IsNumeric(Replace(LG.RecId,'-',''))=1 Then Replace(LG.RecId,'-','') Else Null End) as Integer) "
                    End If


                    curr_TempAdjust = AgL.FillData(mQry, AgL.GCn).Tables(0)

                    DrAmt = Dr
                    mPendingBillCount = 0

                    For J As Integer = 0 To curr_TempAdjust.Rows.Count - 1
                        DocId = AgL.XNull(curr_TempAdjust.Rows(J)("DocId"))
                        RecId = AgL.XNull(curr_TempAdjust.Rows(J)("RecId"))
                        V_Date = curr_TempAdjust.Rows(J)("V_Date")
                        Supplier = AgL.XNull(curr_TempAdjust.Rows(J)("Subcode"))
                        PartyName = AgL.XNull(curr_TempAdjust.Rows(J)("PartyName"))
                        CrAmt = AgL.XNull(curr_TempAdjust.Rows(J)("AmtCr"))
                        Site = AgL.XNull(curr_TempAdjust.Rows(J)("Site_Code"))
                        Division = AgL.XNull(curr_TempAdjust.Rows(J)("DivCode"))
                        City = AgL.XNull(curr_TempAdjust.Rows(J)("City"))
                        Narr = AgL.XNull(curr_TempAdjust.Rows(J)("Narr"))
                        VType = AgL.XNull(curr_TempAdjust.Rows(J)("V_type"))

                        If Math.Round(CrAmt, 2) < Math.Round(DrAmt, 2) Then
                            DrAmt = Math.Round(DrAmt, 2) - Math.Round(CrAmt, 2)
                        Else
                            Dim Status As String = ""
                            If Math.Round(CrAmt, 2) <> Math.Round(CrAmt, 2) - Math.Round(DrAmt, 2) Then Status = "A"
                            runningDr = runningDr + Math.Round(CrAmt, 2) - Math.Round(DrAmt, 2)
                            mQry = " INSERT INTO  #TempRecord 
                                VALUES ('" & DocId & "','" & RecId & "'," & AgL.Chk_Date(V_Date) & ",'" & Supplier & "','" & Replace(PartyName, "'", "`") & "',
                                " & Math.Round(CrAmt, 2) & ", " & Math.Round(CrAmt, 2) - Math.Round(DrAmt, 2) & ", " & runningDr & ", '" & Status & "', '" & Site & "', '" & Division & "' , '" & City & "', 
                                '" & Narr & "', '" & VType & "')  "
                            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
                            mPendingBillCount += 1

                            DrAmt = 0
                            Status = ""
                        End If
                    Next



                    Dim NextYearDate As String
                    NextYearDate = DateAdd(DateInterval.Day, 1, CDate(AgL.PubLoginDate))

                    If mPendingBillCount > 0 Then
                        mQry = " INSERT INTO  #TempRecord (DocId, RecId, V_Date, subcode,
                                     PartyName,BillAmt,PendingAmt, cummAmt, 
                                     Status, Site_Code, Div_Code, PartyCity,
                                     Narration ,V_type)
                            VALUES ('','Total'," & AgL.Chk_Date(NextYearDate) & ", '" & SubCode & "', 
                            '" & Replace(PartyName, "'", "`") & "', 0, 0, 0,
                            '', '" & SiteCode & "', '" & DivCode & "', '" & PCity & "',
                            '','') "
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
                    End If
                    'If Adv <> 0 Then
                    '    mQry = " INSERT INTO  #TempRecord 
                    '        VALUES ('','','01/feb/1980', '" & SubCode & "', '" & Replace(Party, "'", "`") & "', 0, " & -Adv & ",'Adv',
                    '        '" & SiteCode & "', '" & DivCode & "', '" & PCity & "','Advance Payment ','') "
                    '    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
                    'End If
                Next

                Dim mDays1 As Double
                Dim mDays2 As Double
                Dim mDays3 As Double
                Dim mDays4 As Double
                Dim mDays5 As Double
                Dim mDays6 As Double

                mDays1 = mLeavergeDays
                mDays2 = mDays1 + mLeavergeDays
                mDays3 = mDays2 + mLeavergeDays
                mDays4 = mDays3 + mLeavergeDays
                mDays5 = mDays4 + mLeavergeDays
                mDays6 = mDays5 + mLeavergeDays


                strSql = " SELECT *, "
                strSql += " (CASE WHEN DaysDiff>= 0 AND  DaysDiff<=" & mLeavergeDays & " THEN  PendingAmt Else 0 end ) AS AmtDay1, "
                strSql += " (CASE WHEN DaysDiff>" & mLeavergeDays & " THEN  PendingAmt ELSE 0 end) AS AmtDay2, "
                strSql += " (CASE WHEN DaysDiff<=" & mDays1 & " THEN  PendingAmt ELSE 0 end) AS AmtDay0, "
                strSql += " (CASE WHEN DaysDiff>" & mDays1 & " And DaysDiff<=" & mDays2 & " THEN  PendingAmt ELSE 0 end) AS AmtDay30, "
                strSql += " (CASE WHEN DaysDiff>" & mDays2 & " And DaysDiff<=" & mDays3 & " THEN  PendingAmt ELSE 0 end) AS AmtDay60, "
                strSql += " (CASE WHEN DaysDiff>" & mDays3 & " And DaysDiff<=" & mDays4 & " THEN  PendingAmt ELSE 0 end) AS AmtDay90, "
                strSql += " (CASE WHEN DaysDiff>" & mDays4 & " And DaysDiff<=" & mDays5 & " THEN  PendingAmt ELSE 0 end) AS AmtDay120, "
                strSql += " (CASE WHEN DaysDiff>" & mDays5 & " And DaysDiff<=" & mDays6 & " THEN  PendingAmt ELSE 0 end) AS AmtDay150, "
                strSql += " (CASE WHEN DaysDiff>" & mDays6 & " THEN  PendingAmt ELSE 0 end) AS AmtDay180 "
                strSql += " FROM ( "
                strSql += " SELECT DocId, RecId, V_Date As V_Date,subcode, PartyName,BillAmt,PendingAmt,Status,Site_Code, Div_Code,PartyCity,Narration,V_type,"
                If AgL.PubServerName = "" Then
                    strSql += "  julianday(" & strDate & ")  - julianday(V_Date)  As DaysDiff, "
                Else
                    strSql += " DateDiff(Day,V_Date, " & strDate & ") As DaysDiff, "
                End If

                strSql += " " & mLeavergeDays & " As Days "
                strSql += " FROM #TempRecord where (IfNull(Round(PendingAmt,2),0)<>0  Or RecId='Total') "
                strSql += " ) As VMain "




                mQry = strSql

                Dim dtTemp As DataTable
                dtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)


                If ReportFrm.FGetText(0) = "Invoice Wise Detail" Then
                    mQry = " Select VMain.DocId As SearchCode, VMain.Subcode as Subcode, strftime('%d/%m/%Y',VMain.V_Date) As VoucherDate, VMain.RecID as VoucherNo,
                        VMain.PartyName As Party, VMain.PartyCity as City, Cast(VMain.DaysDiff as Int) as [Age], VMain.BillAmt, VMain.AmtDay2 as  [Amount], 1 as Balance, '.' as DrCr
                        From (" & mQry & ") As VMain                                            
                        Where (VMain.AmtDay2<>0 Or VMain.RecId='Total')
                        Order By VMain.PartyName, VMain.V_Date, VMain.RecID  "
                ElseIf ReportFrm.FGetText(0) = "Party Wise Summary" Then
                    mQry = " Select VMain.Subcode || '^' || VMain.Div_Code  As SearchCode, Max(VMain.PartyName) As [Party], Max(VMain.PartyCity) as City, 
                        IfNull(Max(Party.Mobile),'') || (Case  When IfNull(Max(Party.Phone),'')='' Then '' Else ', ' || IfNull(Max(Party.Phone),'')  End)  as ContactNo, 
                        Max(VPartyGST.SalesTaxNo) as GstNo, Max(Division.ManualCode) as Division, Max(Agent.Name) as AgentName,
                        sum(VMain.PendingAmt) as [Amount], Sum(VMain.AmtDay2) As [Amount GE " & mLeavergeDays.ToString & " Days],
                        Max(Cast(VMain.DaysDiff as Int)) As FirstBillAge 
                        From (" & mQry & ") As VMain
                        Left Join Subgroup Division On VMain.Div_Code  COLLATE DATABASE_DEFAULT = Division.Subcode  COLLATE DATABASE_DEFAULT
                        Left Join Subgroup Party On VMain.Subcode  COLLATE DATABASE_DEFAULT = Party.SubCode  COLLATE DATABASE_DEFAULT
                        Left Join (Select SILTV.Subcode, SILTV.Div_Code, Max(SILTV.Agent) as Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode, SILTV.Div_Code) as LTV On Party.Subcode  COLLATE DATABASE_DEFAULT = LTV.Subcode  COLLATE DATABASE_DEFAULT And VMain.Div_Code COLLATE DATABASE_DEFAULT = LTV.Div_Code  COLLATE DATABASE_DEFAULT                    
                        Left Join viewHelpSubgroup Agent On LTV.Agent  COLLATE DATABASE_DEFAULT = Agent.Code  COLLATE DATABASE_DEFAULT
                        LEFT JOIN (Select Subcode, RegistrationNo As SalesTaxNo
                            From SubgroupRegistration 
                            Where RegistrationType = 'Sales Tax No') As VPartyGST On VMain.Subcode COLLATE DATABASE_DEFAULT = VPartyGST.SubCode COLLATE DATABASE_DEFAULT
                        GROUP By VMain.Subcode, VMain.Div_Code
                        Having Sum(VMain.AmtDay2)<>0
                        Order By [Party]"
                ElseIf ReportFrm.FGetText(0) = "Party Wise Ageing" Then
                    Dim StrDays0 As String
                    Dim StrDays1 As String
                    Dim StrDays2 As String
                    Dim StrDays3 As String
                    Dim StrDays4 As String
                    Dim StrDays5 As String
                    Dim StrDays6 As String
                    StrDays0 = "[F 0 T " & mDays1.ToString() & "]"
                    StrDays1 = "[F " & mDays1.ToString() & " T " & mDays2.ToString() & "]"
                    StrDays2 = "[F " & mDays2.ToString() & " T " & mDays3.ToString() & "]"
                    StrDays3 = "[F " & mDays3.ToString() & " T " & mDays4.ToString() & "]"
                    StrDays4 = "[F " & mDays4.ToString() & " T " & mDays5.ToString() & "]"
                    StrDays5 = "[F " & mDays5.ToString() & " T " & mDays6.ToString() & "]"
                    StrDays6 = "[GE " & mDays6.ToString() & "]"

                    mQry = " Select VMain.Subcode || '^' || VMain.Div_Code  As SearchCode, Max(VMain.PartyName) As [Party], Max(VMain.PartyCity) as City, 
                        IfNull(Max(Party.Mobile),'') || (Case  When IfNull(Max(Party.Phone),'')='' Then '' Else ', ' || IfNull(Max(Party.Phone),'')  End)  as ContactNo, 
                        Max(Agent.Name) as AgentName,
                        sum(VMain.PendingAmt) as [Amount], 
                        Sum(VMain.AmtDay0) As " & StrDays0 & ",
                        Sum(VMain.AmtDay30) As " & StrDays1 & ",
                        Sum(VMain.AmtDay60) As " & StrDays2 & ",
                        Sum(VMain.AmtDay90) As " & StrDays3 & ",
                        Sum(VMain.AmtDay120) As " & StrDays4 & ",
                        Sum(VMain.AmtDay150) As " & StrDays5 & ",
                        Sum(VMain.AmtDay180) As " & StrDays6 & "
                        From (" & mQry & ") As VMain
                        Left Join Subgroup Division On VMain.Div_Code  COLLATE DATABASE_DEFAULT = Division.Subcode  COLLATE DATABASE_DEFAULT
                        Left Join Subgroup Party On VMain.Subcode  COLLATE DATABASE_DEFAULT = Party.SubCode  COLLATE DATABASE_DEFAULT
                        Left Join (Select SILTV.Subcode, SILTV.Div_Code, Max(SILTV.Agent) as Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode, SILTV.Div_Code) as LTV On Party.Subcode  COLLATE DATABASE_DEFAULT = LTV.Subcode  COLLATE DATABASE_DEFAULT And VMain.Div_Code COLLATE DATABASE_DEFAULT = LTV.Div_Code  COLLATE DATABASE_DEFAULT                    
                        Left Join viewHelpSubgroup Agent On LTV.Agent  COLLATE DATABASE_DEFAULT = Agent.Code  COLLATE DATABASE_DEFAULT
                        GROUP By VMain.Subcode, VMain.Div_Code
                        Having Sum(VMain.AmtDay2)<>0
                        Order By [Party]"
                End If



                DsHeader = AgL.FillData(mQry, AgL.GCn)

                If DsHeader.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

                ReportFrm.Text = "Creditors Outstanding Report - " + ReportFrm.FGetText(0)
                ReportFrm.ClsRep = Me
                ReportFrm.ReportProcName = "ProcCreditorsOutstaningReport"

                ReportFrm.ProcFillGrid(DsHeader)


                If ReportFrm.FGetText(0) = "Invoice Wise Detail" Then
                    ReportFrm.DGL1.Columns("Subcode").Visible = False
                    Dim I As Integer
                    Dim mRunningBal As Double
                    Dim mSubcodeCount As Integer
                    mRunningBal = 0 : mSubcodeCount = 1
                    For I = 0 To ReportFrm.DGL1.RowCount - 1
                        If I > 0 Then
                            If ReportFrm.DGL1.Item("Subcode", I).Value <> ReportFrm.DGL1.Item("Subcode", I - 1).Value Then
                                mRunningBal = 0
                                mSubcodeCount += 1
                            End If
                        End If

                        mRunningBal += Val(ReportFrm.DGL1.Item("Amount", I).Value)
                        ReportFrm.DGL1.Item("Balance", I).Value = mRunningBal
                        ReportFrm.DGL1.Item("Dr Cr", I).Value = IIf(mRunningBal < 0, "Cr", "Dr")
                        If AgL.XNull(ReportFrm.DGL1.Item("Voucher No", I).Value) = "Total" Then
                            ReportFrm.DGL1.Item("Age", I).Value = "0"
                            ReportFrm.DGL1.Item("Voucher Date", I).Value = ""
                        End If

                    Next
                    If mSubcodeCount = 1 Then
                        ReportFrm.DGL2.Item("Balance", 0).Value = ReportFrm.DGL1.Item("Balance", I - 1).Value
                        ReportFrm.DGL2.Item("Dr Cr", 0).Value = ReportFrm.DGL1.Item("Dr Cr", I - 1).Value
                    Else
                        ReportFrm.DGL2.Item("Balance", 0).Value = ""
                        ReportFrm.DGL2.Item("Dr Cr", 0).Value = ""
                    End If
                End If


            ElseIf ReportFrm.FGetText(1) = "Adjustment" Then
                mQry = "
                        Select LG.DocID, Lg.Site_Code, LG.DivCode as Div_Code, D.ManualCode as Division, LG.Subcode, LG.V_Date, LG.RecID, PI.VendorDocDate, PI.VendorDocNo, 
                        Sg.Name as PartyName, Ct.CityName, Sg.Mobile, Sg.Phone, Agent.Name as Agent, LG.AmtDr+LG.AmtCr as TransAmt, IfNull(Adj.AdjAmt,0) as AdjAmt, 
                        (Case When Lg.AmtDr > 0 Then LG.AmtDr-IfNull(Adj.AdjAmt,0) Else 0 End) AmtDr, 
                        (Case When Lg.AmtCr>0 Then LG.AmtCr-IfNull(Adj.AdjAmt,0) Else 0 End) as AmtCr "
                If AgL.PubServerName = "" Then
                    mQry += ",  julianday(" & strDate & ")  - julianday(Lg.V_Date)  As DaysDiff "
                Else
                    mQry += ", DateDiff(Day,LG.V_Date, " & strDate & ") As DaysDiff "
                End If
                mQry = mQry + "From ledger LG 
                        LEFT JOIN purchinvoice PI ON PI.DocID = LG.DocId
                        Left Join (Select Adj_DocID as DocID, Adj_V_Sno as V_SNo, 
                                    abs(Sum(Amount)) as AdjAmt 
                                    From LedgerAdj LA  
                                    Left Join Ledger L1   On L1.DocId = LA.Vr_DocID And L1.V_SNo = LA.Vr_V_Sno
                                    Group By Adj_DocID, Adj_V_Sno
                                    Union All 
                                    Select Vr_DocID as DocID, Vr_V_Sno as V_SNo, 
                                    abs(Sum(Amount)) as AdjAmt 
                                    From LedgerAdj LA  
                                    Left Join Ledger L1   On L1.DocId = LA.Vr_DocID And L1.V_SNo = LA.Vr_V_Sno
                                    Group By Vr_DocID, Vr_V_Sno                    
                                    ) as Adj On LG.DocID = Adj.DocID And LG.V_Sno = Adj.V_Sno                
                        LEFT JOIN SubGroup SG On SG.SubCode =LG.SubCode  
                        Left Join (Select SILTV.Subcode, Max(SILTV.Agent) as Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode) as LTV On Sg.SubCode = LTV.Subcode
                        Left Join viewHelpSubgroup Agent On LTV.Agent = Agent.Code
                        LEFT JOIN City CT On SG.CityCode  =CT.CityCode 
                        Left Join SiteMast Site On LG.Site_Code = Site.Code
                        Left Join Subgroup D On LG.DivCode = D.SubCode
                        Where Round((LG.AmtDr+LG.AmtCr)  - IsNull(Adj.AdjAmt,0),3) >0                         
                        And SG.Nature ='Supplier'                            
                    " & mCondStr



                If ReportFrm.FGetText(0) = "Invoice Wise Detail" Then

                    mQry = " Select VMain.DocId As SearchCode, Vmain.Subcode, Vmain.Division, strftime('%d/%m/%Y',VMain.V_Date) As VoucherDate, VMain.RecID as VoucherNo, strftime('%d/%m/%Y',VMain.VendorDocDate) As VendorDocDate, VMain.VendorDocNo as VendorDocNo,
                        VMain.PartyName As Party, VMain.CityName as City, VMain.DaysDiff as [Age], Vmain.TransAmt, Vmain.AdjAmt, Vmain.AmtDr, Vmain.AmtCr, 1 as Balance, '.' as DrCr
                        From (" & mQry & ") As VMain       
                        Where (Vmain.DaysDiff > " & mLeavergeDays & " Or Vmain.AmtDr>0)            
                        Order By VMain.PartyName, VMain.V_Date, VMain.RecID "


                    DsHeader = AgL.FillData(mQry, AgL.GCn)


                ElseIf ReportFrm.FGetText(0) = "Party Wise Summary" Then
                    mQry = " Select VMain.Subcode || '^' || VMain.Div_Code  As SearchCode, Max(VMain.PartyName) As [Party], Max(VMain.CityName) as City, 
                        IfNull(Max(VMain.Mobile),'') || (Case  When IfNull(Max(VMain.Phone),'')='' Then '' Else ', ' || IfNull(Max(VMain.Phone),'')  End)  as ContactNo, 
                        Max(VMain.Division) as Division, Max(Vmain.Agent) as AgentName,
                        abs(sum(VMain.AmtDr - VMain.AmtCr)) as [Amount], abs(Sum(Case When VMain.DaysDiff > " & mLeavergeDays & " Or VMain.AmtDr > 0 Then VMain.AmtDr - VMain.AmtCr  Else 0 End)) As [Amount GE " & mLeavergeDays.ToString & " Days]
                        From (" & mQry & ") As VMain
                        GROUP By VMain.Subcode, VMain.Div_Code
                        Having Sum(Case When VMain.DaysDiff > " & mLeavergeDays & " Or VMain.AmtDr > 0 Then VMain.AmtCr - VMain.AmtDr Else 0 End) > 0
                        Order By [Party]"

                    DsHeader = AgL.FillData(mQry, AgL.GCn)
                End If








                If DsHeader.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

                ReportFrm.Text = "Creditors Outstanding Report - " + ReportFrm.FGetText(0)
                ReportFrm.ClsRep = Me
                ReportFrm.ReportProcName = "ProcCreditorsOutstaningReport"
                ReportFrm.AllowAutoResizeRows = False
                ReportFrm.ProcFillGrid(DsHeader)



                If ReportFrm.FGetText(0) = "Invoice Wise Detail" Then
                    ReportFrm.DGL1.Columns("Subcode").Visible = False
                    Dim I As Integer
                    Dim mRunningBal As Double
                    Dim mSubcodeCount As Integer
                    mRunningBal = 0 : mSubcodeCount = 1
                    For I = 0 To ReportFrm.DGL1.RowCount - 1
                        If I > 0 Then
                            If ReportFrm.DGL1.Item("Subcode", I).Value <> ReportFrm.DGL1.Item("Subcode", I - 1).Value Then
                                mRunningBal = 0
                                mSubcodeCount += 1
                            End If
                        End If

                        mRunningBal += Val(ReportFrm.DGL1.Item("Amt Dr", I).Value) - Val(ReportFrm.DGL1.Item("Amt Cr", I).Value)
                        ReportFrm.DGL1.Item("Balance", I).Value = Math.Abs(mRunningBal)
                        ReportFrm.DGL1.Item("Dr Cr", I).Value = IIf(mRunningBal < 0, "Cr", "Dr")
                        If AgL.XNull(ReportFrm.DGL1.Item("Voucher No", I).Value) = "Total" Then
                            'ReportFrm.DGL1.Item("Age", I).Value = ""
                            ReportFrm.DGL1.Item("Voucher Date", I).Value = ""
                        End If
                    Next
                    If mSubcodeCount = 1 Then
                        ReportFrm.DGL2.Item("Balance", 0).Value = ReportFrm.DGL1.Item("Balance", I - 1).Value
                        ReportFrm.DGL2.Item("Dr Cr", 0).Value = ReportFrm.DGL1.Item("Dr Cr", I - 1).Value
                    Else
                        ReportFrm.DGL2.Item("Balance", 0).Value = ""
                        ReportFrm.DGL2.Item("Dr Cr", 0).Value = ""
                    End If
                End If

            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            DsHeader = Nothing
        End Try
    End Sub



    Public Sub ProcDebtorsOutstaningReport(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try
            Dim mCondStr$ = ""
            Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"
            Dim mLeavergeDays As Double
            Dim strSql As String
            Dim strDate As String

            Dim mPendingBillCount As Integer

            RepTitle = "Debtors Outstanding Report"

            If mFilterGrid IsNot Nothing And mGridRow IsNot Nothing Then
                If mGridRow.DataGridView.Columns.Contains("Search Code") = True Then
                    If mFilterGrid.Item(GFilter, 0).Value = "Party Wise Summary" Then
                        Dim mSearchCodes As String()
                        mSearchCodes = mGridRow.Cells("Search Code").Value.ToString.Split("^")

                        mFilterGrid.Item(GFilter, 4).Value = mGridRow.Cells("Party").Value
                        mFilterGrid.Item(GFilterCode, 4).Value = "'" + mSearchCodes(0) + "'" '"'" + mGridRow.Cells("Search Code").Value + "'"

                        mFilterGrid.Item(GFilter, 11).Value = mGridRow.Cells("Division").Value
                        mFilterGrid.Item(GFilterCode, 11).Value = "'" + mSearchCodes(1) + "'" '"'" + mGridRow.Cells("Search Code").Value + "'"

                        mFilterGrid.Item(GFilter, 0).Value = "Invoice Wise Detail"
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = "Invoice Wise Detail" Then
                        ClsMain.FOpenForm(mGridRow.Cells("Search Code").Value, ReportFrm)
                        ReportFrm.FiterGridCopy_Arr.RemoveAt(ReportFrm.FiterGridCopy_Arr.Count - 1)
                        Exit Sub
                    End If
                Else
                    Exit Sub
                End If
            End If


            strDate = AgL.Chk_Text(CDate(ReportFrm.FGetText(2)).ToString("s"))

            mCondStr = "  "
            mCondStr = mCondStr & " AND Date(LG.V_Date) <= " & AgL.Chk_Date(CDate(ReportFrm.FGetText(2)).ToString("s")) & " "
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("LG.Subcode", 4)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("SG.GroupCode", 5)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("LTV.Agent", 6)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("CT.CityCode", 7)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("Ct.State", 8)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("SG.Area", 9)
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("LG.Site_Code", 10), "''", "'")
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("LG.DivCode", 11), "''", "'")

            mLeavergeDays = Val(ReportFrm.FGetText(3))



            If ReportFrm.FGetText(1) = "FIFO" Then

                Try
                    mQry = "Drop Table #TempRecord"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
                Catch ex As Exception
                End Try

                mQry = " CREATE Temporary TABLE #TempRecord (DocId  nvarchar(21),RecId  nvarchar(50),V_Date  DateTime,subcode nvarchar(30),"
                mQry += " PartyName nvarchar(500),BillAmt FLOAT,PendingAmt FLOAT, cummAmt Float,Status  nvarchar(20), Site_Code  nvarchar(2), Div_Code nVarchar(1),
                          PartyCity  nvarchar(200),Narration  varchar(2000),V_type  nvarchar(20) );	"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

                Dim SubCode As String = "", Party As String = "", PCity As String = "", SiteCode As String = "", DivCode As String = ""
                Dim Cr As Double = 0, Adv As Double = 0
                Dim runningDr As Double = 0

                Dim CurrTempPayment As DataTable = Nothing

                mQry = " SELECT LG.SubCode,max(Sg.name) as PartyName,max(CT.CityName) as PCity,IfNull(sum(AmtCr),0) AS AmtCr,
                    Case When IfNull(sum(AmtCr),0)> IfNull(sum(AmtDr),0) Then (IfNull(sum(AmtCr),0) - IfNull(sum(AmtDr),0)) Else  0   End As Advance ,
                    Max(LG.Site_Code) As SiteCode, LG.DivCode  
                    FROM Ledger LG 
                    LEFT JOIN SubGroup SG On SG.SubCode =LG.SubCode  
                    Left Join (Select SILTV.Subcode, Max(SILTV.Agent) as Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode) as LTV On Sg.SubCode = LTV.Subcode
                    LEFT JOIN City CT On SG.CityCode  =CT.CityCode Where 1=1 " + mCondStr + " And SG.Nature ='Customer' And IfNull(SG.IsSisterConcern,0) =0
                    GROUP BY LG.SubCode, LG.DivCode 
                    Having IfNull(sum(AmtCr),0) - IfNull(sum(AmtDr),0) < 0 "
                CurrTempPayment = AgL.FillData(mQry, AgL.GCn).Tables(0)

                For I As Integer = 0 To CurrTempPayment.Rows.Count - 1
                    SubCode = AgL.XNull(CurrTempPayment.Rows(I)("SubCode"))
                    Party = AgL.XNull(CurrTempPayment.Rows(I)("PartyName"))
                    PCity = AgL.XNull(CurrTempPayment.Rows(I)("PCity"))
                    Cr = AgL.XNull(CurrTempPayment.Rows(I)("AmtCr"))
                    Adv = AgL.XNull(CurrTempPayment.Rows(I)("Advance"))
                    SiteCode = AgL.XNull(CurrTempPayment.Rows(I)("SiteCode"))
                    DivCode = AgL.XNull(CurrTempPayment.Rows(I)("DivCode"))

                    Dim CrAmt As Double = 0, tempval As Double = 0, DrAmt As Double = 0
                    Dim DocId As String = "", RecId As String = "", Supplier As String = "", PartyName As String = "", Site As String = "", Division As String = "", City As String = "", Narr As String = "", VType As String = ""
                    Dim V_Date As String = ""

                    tempval = 0

                    Dim curr_TempAdjust As DataTable = Nothing

                    mQry = " SELECT  IfNull(LG.DocId,'') AS DocId, LG.V_Type,'" & IIf(AgL.PubPrintDivisionShortNameOnDocumentsYn, AgL.PubDivShortName, "") & IIf(AgL.PubPrintSiteShortNameOnDocumentsYn, AgL.PubSiteShortName, "") & "' || (Case When VT.Short_Name Is Not Null Then VT.Short_Name Else '' End) ||  LG.RecId As RecId,LG.V_date AS V_date,IfNull(LG.SubCode,'') AS Subcode,
                IfNull(SG.Name,'') AS PartyName, IfNull(Lg.AmtDr,0) AS AmtDr,IfNull(Lg.Site_Code,0) AS Site_Code, LG.DivCode ,IfNull(Ct.CityName,'') as City,IfNull(Lg.Narration,'') as Narr,IfNull(Lg.V_type,'') as V_type  
                FROM Ledger LG LEFT JOIN SubGroup SG On  SG.SubCode=LG.SubCode 
                Left Join (Select SILTV.Subcode, Max(SILTV.Agent) as Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode) as LTV On Sg.SubCode = LTV.Subcode
                LEFT JOIN City CT On Ct.CityCode =Sg.CityCode  
                Left Join Voucher_Type Vt On Lg.V_Type = Vt.V_Type
                Where Date(LG.V_Date) <= " & AgL.Chk_Date(CDate(ReportFrm.FGetText(2)).ToString("s")) & " 
                And IfNull(Lg.AmtDr, 0) <> 0 And LG.SubCode = '" & SubCode & "' And LG.DivCode='" & DivCode & "'  "
                    If AgL.PubServerName = "" Then
                        mQry = mQry & " Order By Lg.V_Date, Try_Parse(Replace(LG.RecId,'-','') as Integer) "
                    Else
                        mQry = mQry & " Order By Lg.V_Date, Cast((Case When IsNumeric(Replace(LG.RecId,'-',''))=1 Then Replace(LG.RecId,'-','') Else Null End) as BigInt) "
                    End If


                    curr_TempAdjust = AgL.FillData(mQry, AgL.GCn).Tables(0)

                    CrAmt = Cr
                    mPendingBillCount = 0
                    For J As Integer = 0 To curr_TempAdjust.Rows.Count - 1
                        DocId = AgL.XNull(curr_TempAdjust.Rows(J)("DocId"))
                        RecId = AgL.XNull(curr_TempAdjust.Rows(J)("RecId"))
                        V_Date = curr_TempAdjust.Rows(J)("V_Date")
                        Supplier = AgL.XNull(curr_TempAdjust.Rows(J)("Subcode"))
                        PartyName = AgL.XNull(curr_TempAdjust.Rows(J)("PartyName"))
                        DrAmt = AgL.XNull(curr_TempAdjust.Rows(J)("AmtDr"))
                        Site = AgL.XNull(curr_TempAdjust.Rows(J)("Site_Code"))
                        Division = AgL.XNull(curr_TempAdjust.Rows(J)("DivCode"))
                        City = AgL.XNull(curr_TempAdjust.Rows(J)("City"))
                        Narr = AgL.XNull(curr_TempAdjust.Rows(J)("Narr"))
                        VType = AgL.XNull(curr_TempAdjust.Rows(J)("V_type"))

                        If Math.Round(DrAmt, 2) < Math.Round(CrAmt, 2) Then
                            CrAmt = Math.Round(CrAmt, 2) - Math.Round(DrAmt, 2)
                        Else
                            Dim Status As String = ""
                            If Math.Round(DrAmt, 2) <> Math.Round(DrAmt, 2) - Math.Round(CrAmt, 2) Then Status = "A"
                            runningDr = runningDr + Math.Round(DrAmt, 2) - Math.Round(CrAmt, 2)
                            mQry = " INSERT INTO  #TempRecord (DocId, RecId, V_Date, subcode,
                                     PartyName,BillAmt,PendingAmt, cummAmt, 
                                     Status, Site_Code, Div_Code, PartyCity,
                                     Narration ,V_type)
                                    VALUES ('" & DocId & "','" & RecId & "'," & AgL.Chk_Date(V_Date) & ",'" & Supplier & "',
                                    '" & Replace(PartyName, "'", "`") & "', " & Math.Round(DrAmt, 2) & ", " & Math.Round(DrAmt, 2) - Math.Round(CrAmt, 2) & ", " & runningDr & ", 
                                    '" & Status & "', '" & Site & "', '" & Division & "' , '" & City & "', 
                                    '" & Narr & "', '" & VType & "')  "
                            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
                            mPendingBillCount += 1

                            CrAmt = 0
                            Status = ""
                        End If
                    Next

                    Dim NextYearDate As String
                    NextYearDate = DateAdd(DateInterval.Day, 1, CDate(AgL.PubLoginDate))

                    If mPendingBillCount > 0 Then
                        mQry = " INSERT INTO  #TempRecord (DocId, RecId, V_Date, subcode,
                                     PartyName,BillAmt,PendingAmt, cummAmt, 
                                     Status, Site_Code, Div_Code, PartyCity,
                                     Narration ,V_type)
                            VALUES ('','Total'," & AgL.Chk_Date(NextYearDate) & ", '" & SubCode & "', 
                            '" & Replace(PartyName, "'", "`") & "', 0, 0, 0,
                            '', '" & SiteCode & "', '" & DivCode & "', '" & PCity & "',
                            '','') "
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
                    End If


                    'If Adv <> 0 Then
                    '    mQry = " INSERT INTO  #TempRecord 
                    '        VALUES ('','','01/feb/1980', '" & SubCode & "', '" & Replace(Party, "'", "`") & "', 0, " & -Adv & ",'Adv',
                    '        '" & SiteCode & "', '" & DivCode & "', '" & PCity & "','Advance Payment ','') "
                    '    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
                    'End If
                Next



                Dim mDays1 As Double
                Dim mDays2 As Double
                Dim mDays3 As Double
                Dim mDays4 As Double
                Dim mDays5 As Double
                Dim mDays6 As Double

                mDays1 = mLeavergeDays
                mDays2 = mDays1 + mLeavergeDays
                mDays3 = mDays2 + mLeavergeDays
                mDays4 = mDays3 + mLeavergeDays
                mDays5 = mDays4 + mLeavergeDays
                mDays6 = mDays5 + mLeavergeDays

                strSql = " SELECT *, "
                strSql += " (CASE WHEN DaysDiff>= 0 AND  DaysDiff<=" & mLeavergeDays & " THEN  PendingAmt Else 0 end ) AS AmtDay1, "
                strSql += " (CASE WHEN DaysDiff>" & mLeavergeDays & " THEN  PendingAmt ELSE 0 end) AS AmtDay2, "
                strSql += " (CASE WHEN DaysDiff<=" & mDays1 & " THEN  PendingAmt ELSE 0 end) AS AmtDay0, "
                strSql += " (CASE WHEN DaysDiff>" & mDays1 & " And DaysDiff<=" & mDays2 & " THEN  PendingAmt ELSE 0 end) AS AmtDay30, "
                strSql += " (CASE WHEN DaysDiff>" & mDays2 & " And DaysDiff<=" & mDays3 & " THEN  PendingAmt ELSE 0 end) AS AmtDay60, "
                strSql += " (CASE WHEN DaysDiff>" & mDays3 & " And DaysDiff<=" & mDays4 & " THEN  PendingAmt ELSE 0 end) AS AmtDay90, "
                strSql += " (CASE WHEN DaysDiff>" & mDays4 & " And DaysDiff<=" & mDays5 & " THEN  PendingAmt ELSE 0 end) AS AmtDay120, "
                strSql += " (CASE WHEN DaysDiff>" & mDays5 & " And DaysDiff<=" & mDays6 & " THEN  PendingAmt ELSE 0 end) AS AmtDay150, "
                strSql += " (CASE WHEN DaysDiff>" & mDays6 & " THEN  PendingAmt ELSE 0 end) AS AmtDay180 "
                strSql += " FROM ( "
                strSql += " SELECT DocId, RecId, V_Date As V_Date,subcode, PartyName,BillAmt,PendingAmt,Status,Site_Code, Div_Code,PartyCity,Narration,V_type,"
                If AgL.PubServerName = "" Then
                    strSql += "  julianday(" & strDate & ")  - julianday(V_Date)  As DaysDiff, "
                Else
                    strSql += " DateDiff(Day,V_Date, " & strDate & ") As DaysDiff, "
                End If

                strSql += " " & mLeavergeDays & " As Days "
                strSql += " FROM #TempRecord where (IfNull(Round(PendingAmt,2),0)<>0  Or RecId='Total')"
                strSql += " ) As VMain "

                'mQry = "Select * INTO TempRecord FROM #TempRecord "
                'AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

                mQry = strSql

                Dim dtTemp As DataTable
                dtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)


                If ReportFrm.FGetText(0) = "Invoice Wise Detail" Then
                    mQry = " Select VMain.DocId As SearchCode, VMain.Subcode as Subcode, strftime('%d/%m/%Y',VMain.V_Date) As VoucherDate, VMain.RecID as VoucherNo,
                        VMain.PartyName As Party, VMain.PartyCity as City, Cast(VMain.DaysDiff as Int) as [Age], VMain.BillAmt, VMain.AmtDay2 as  [Amount], 1 as Balance, '.' as DrCr
                        From (" & mQry & ") As VMain                                            
                        Where (VMain.AmtDay2<>0 Or VMain.RecId='Total')
                        Order By VMain.PartyName, VMain.Subcode, VMain.V_Date, VMain.RecID  "
                ElseIf ReportFrm.FGetText(0) = "Party Wise Summary" Then
                    mQry = " Select VMain.Subcode || '^' || VMain.Div_Code  As SearchCode, Max(VMain.PartyName) As [Party], Max(VMain.PartyCity) as City, 
                        IfNull(Max(Party.Mobile),'') || (Case  When IfNull(Max(Party.Phone),'')='' Then '' Else ', ' || IfNull(Max(Party.Phone),'')  End)  as ContactNo, 
                        Max(VPartyGST.SalesTaxNo) as GstNo, Max(Division.ManualCode) as Division, Max(Agent.Name) as AgentName,
                        sum(VMain.PendingAmt) as [Amount], Sum(VMain.AmtDay2) As [Amount GE " & mLeavergeDays.ToString & " Days],
                        Max(Cast(VMain.DaysDiff as Int)) As FirstBillAge 
                        From (" & mQry & ") As VMain
                        Left Join Subgroup Division On VMain.Div_Code  COLLATE DATABASE_DEFAULT = Division.Subcode  COLLATE DATABASE_DEFAULT
                        Left Join Subgroup Party On VMain.Subcode  COLLATE DATABASE_DEFAULT = Party.SubCode  COLLATE DATABASE_DEFAULT
                        Left Join (Select SILTV.Subcode, SILTV.Div_Code, Max(SILTV.Agent) as Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode, SILTV.Div_Code) as LTV On Party.Subcode  COLLATE DATABASE_DEFAULT = LTV.Subcode  COLLATE DATABASE_DEFAULT And VMain.Div_Code COLLATE DATABASE_DEFAULT = LTV.Div_Code  COLLATE DATABASE_DEFAULT                    
                        Left Join viewHelpSubgroup Agent On LTV.Agent  COLLATE DATABASE_DEFAULT = Agent.Code  COLLATE DATABASE_DEFAULT
                        LEFT JOIN (Select Subcode, RegistrationNo As SalesTaxNo
                            From SubgroupRegistration 
                            Where RegistrationType = 'Sales Tax No') As VPartyGST On VMain.Subcode COLLATE DATABASE_DEFAULT = VPartyGST.SubCode COLLATE DATABASE_DEFAULT
                        GROUP By VMain.Subcode, VMain.Div_Code
                        Having Sum(VMain.AmtDay2)<>0
                        Order By [Party]"
                ElseIf ReportFrm.FGetText(0) = "Party Wise Ageing" Then
                    Dim StrDays0 As String
                    Dim StrDays1 As String
                    Dim StrDays2 As String
                    Dim StrDays3 As String
                    Dim StrDays4 As String
                    Dim StrDays5 As String
                    Dim StrDays6 As String
                    StrDays0 = "[F 0 T " & mDays1.ToString() & "]"
                    StrDays1 = "[F " & mDays1.ToString() & " T " & mDays2.ToString() & "]"
                    StrDays2 = "[F " & mDays2.ToString() & " T " & mDays3.ToString() & "]"
                    StrDays3 = "[F " & mDays3.ToString() & " T " & mDays4.ToString() & "]"
                    StrDays4 = "[F " & mDays4.ToString() & " T " & mDays5.ToString() & "]"
                    StrDays5 = "[F " & mDays5.ToString() & " T " & mDays6.ToString() & "]"
                    StrDays6 = "[GE " & mDays6.ToString() & "]"

                    mQry = " Select VMain.Subcode || '^' || VMain.Div_Code  As SearchCode, Max(VMain.PartyName) As [Party], Max(VMain.PartyCity) as City, 
                        IfNull(Max(Party.Mobile),'') || (Case  When IfNull(Max(Party.Phone),'')='' Then '' Else ', ' || IfNull(Max(Party.Phone),'')  End)  as ContactNo, 
                        Max(Agent.Name) as AgentName,
                        sum(VMain.PendingAmt) as [Amount], 
                        Sum(VMain.AmtDay0) As " & StrDays0 & ",
                        Sum(VMain.AmtDay30) As " & StrDays1 & ",
                        Sum(VMain.AmtDay60) As " & StrDays2 & ",
                        Sum(VMain.AmtDay90) As " & StrDays3 & ",
                        Sum(VMain.AmtDay120) As " & StrDays4 & ",
                        Sum(VMain.AmtDay150) As " & StrDays5 & ",
                        Sum(VMain.AmtDay180) As " & StrDays6 & "
                        From (" & mQry & ") As VMain
                        Left Join Subgroup Division On VMain.Div_Code  COLLATE DATABASE_DEFAULT = Division.Subcode  COLLATE DATABASE_DEFAULT
                        Left Join Subgroup Party On VMain.Subcode  COLLATE DATABASE_DEFAULT = Party.SubCode  COLLATE DATABASE_DEFAULT
                        Left Join (Select SILTV.Subcode, SILTV.Div_Code, Max(SILTV.Agent) as Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode, SILTV.Div_Code) as LTV On Party.Subcode  COLLATE DATABASE_DEFAULT = LTV.Subcode  COLLATE DATABASE_DEFAULT And VMain.Div_Code COLLATE DATABASE_DEFAULT = LTV.Div_Code  COLLATE DATABASE_DEFAULT                    
                        Left Join viewHelpSubgroup Agent On LTV.Agent  COLLATE DATABASE_DEFAULT = Agent.Code  COLLATE DATABASE_DEFAULT
                        GROUP By VMain.Subcode, VMain.Div_Code
                        Having Sum(VMain.AmtDay2)<>0
                        Order By [Party]"
                End If



                DsHeader = AgL.FillData(mQry, AgL.GCn)

                If DsHeader.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

                ReportFrm.Text = "Debtors Outstanding Report - " + ReportFrm.FGetText(0)
                ReportFrm.ClsRep = Me
                ReportFrm.ReportProcName = "ProcDebtorsOutstaningReport"

                ReportFrm.ProcFillGrid(DsHeader)


                If ReportFrm.FGetText(0) = "Invoice Wise Detail" Then
                    ReportFrm.DGL1.Columns("Subcode").Visible = False
                    Dim I As Integer
                    Dim mRunningBal As Double
                    Dim mSubcodeCount As Integer
                    mRunningBal = 0 : mSubcodeCount = 1
                    For I = 0 To ReportFrm.DGL1.RowCount - 1
                        If I > 0 Then
                            If ReportFrm.DGL1.Item("Subcode", I).Value <> ReportFrm.DGL1.Item("Subcode", I - 1).Value Then
                                mRunningBal = 0
                                mSubcodeCount += 1
                            End If
                        End If

                        mRunningBal += Val(ReportFrm.DGL1.Item("Amount", I).Value)
                        ReportFrm.DGL1.Item("Balance", I).Value = mRunningBal
                        ReportFrm.DGL1.Item("Dr Cr", I).Value = IIf(mRunningBal < 0, "Cr", "Dr")
                        If AgL.XNull(ReportFrm.DGL1.Item("Voucher No", I).Value) = "Total" Then
                            ReportFrm.DGL1.Item("Age", I).Value = "0"
                            ReportFrm.DGL1.Item("Voucher Date", I).Value = ""
                        End If

                    Next
                    If mSubcodeCount = 1 Then
                        ReportFrm.DGL2.Item("Balance", 0).Value = ReportFrm.DGL1.Item("Balance", I - 1).Value
                        ReportFrm.DGL2.Item("Dr Cr", 0).Value = ReportFrm.DGL1.Item("Dr Cr", I - 1).Value
                    Else
                        ReportFrm.DGL2.Item("Balance", 0).Value = ""
                        ReportFrm.DGL2.Item("Dr Cr", 0).Value = ""
                    End If

                End If


            ElseIf ReportFrm.FGetText(1) = "Adjustment" Then
                mQry = "
                        Select LG.DocID, Lg.Site_Code, LG.DivCode as Div_Code, D.ManualCode as Division, LG.Subcode, LG.V_Date, LG.RecID, 
                        Sg.Name as PartyName, Ct.CityName, Sg.Mobile, Sg.Phone, Agent.Name as Agent, LG.AmtDr+LG.AmtCr as TransAmt, IfNull(Adj.AdjAmt,0) as AdjAmt, 
                        (Case When Lg.AmtDr > 0 Then LG.AmtDr-IfNull(Adj.AdjAmt,0) Else 0 End) AmtDr, 
                        (Case When Lg.AmtCr>0 Then LG.AmtCr-IfNull(Adj.AdjAmt,0) Else 0 End) as AmtCr "
                If AgL.PubServerName = "" Then
                    mQry += ",  julianday(" & strDate & ")  - julianday(Lg.V_Date)  As DaysDiff "
                Else
                    mQry += ", DateDiff(Day,LG.V_Date, " & strDate & ") As DaysDiff "
                End If
                mQry = mQry + "From ledger LG 
                        Left Join (Select Adj_DocID as DocID, Adj_V_Sno as V_SNo, 
                                    abs(Sum(Amount)) as AdjAmt 
                                    From LedgerAdj LA  
                                    Left Join Ledger L1   On L1.DocId = LA.Vr_DocID And L1.V_SNo = LA.Vr_V_Sno
                                    Group By Adj_DocID, Adj_V_Sno
                                    Union All 
                                    Select Vr_DocID as DocID, Vr_V_Sno as V_SNo, 
                                    abs(Sum(Amount)) as AdjAmt 
                                    From LedgerAdj LA  
                                    Left Join Ledger L1   On L1.DocId = LA.Vr_DocID And L1.V_SNo = LA.Vr_V_Sno
                                    Group By Vr_DocID, Vr_V_Sno                    
                                    ) as Adj On LG.DocID = Adj.DocID And LG.V_Sno = Adj.V_Sno                
                        LEFT JOIN SubGroup SG On SG.SubCode =LG.SubCode  
                        Left Join (Select SILTV.Subcode, Max(SILTV.Agent) as Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode) as LTV On Sg.SubCode = LTV.Subcode
                        Left Join viewHelpSubgroup Agent On LTV.Agent = Agent.Code
                        LEFT JOIN City CT On SG.CityCode  =CT.CityCode 
                        Left Join SiteMast Site On LG.Site_Code = Site.Code
                        Left Join Subgroup D On LG.DivCode = D.SubCode
                        Where (LG.AmtDr+LG.AmtCr)  - IfNull(Adj.AdjAmt,0) >0                         
                        And SG.Nature ='Customer'                            
                    " & mCondStr



                If ReportFrm.FGetText(0) = "Invoice Wise Detail" Then

                    mQry = " Select VMain.DocId As SearchCode, Vmain.Subcode, Vmain.Division, strftime('%d/%m/%Y',VMain.V_Date) As VoucherDate, VMain.RecID as VoucherNo,
                        VMain.PartyName As Party, VMain.CityName as City, VMain.DaysDiff as [Age], Vmain.TransAmt, Vmain.AdjAmt, Vmain.AmtDr, Vmain.AmtCr, 1 as Balance, '.' as DrCr
                        From (" & mQry & ") As VMain       
                        Where (Vmain.DaysDiff > " & mLeavergeDays & " Or Vmain.AmtCr>0)            
                        Order By VMain.PartyName, VMain.V_Date, VMain.RecID "


                    DsHeader = AgL.FillData(mQry, AgL.GCn)


                ElseIf ReportFrm.FGetText(0) = "Party Wise Summary" Then
                    mQry = " Select VMain.Subcode || '^' || VMain.Div_Code  As SearchCode, Max(VMain.PartyName) As [Party], Max(VMain.CityName) as City, 
                        IfNull(Max(VMain.Mobile),'') || (Case  When IfNull(Max(VMain.Phone),'')='' Then '' Else ', ' || IfNull(Max(VMain.Phone),'')  End)  as ContactNo, 
                        Max(VMain.Division) as Division, Max(Vmain.Agent) as AgentName,
                        sum(VMain.AmtDr - VMain.AmtCr) as [Amount], Sum(Case When VMain.DaysDiff > " & mLeavergeDays & " Or VMain.AmtCr > 0 Then VMain.AmtDr - VMain.AmtCr  Else 0 End) As [Amount GE " & mLeavergeDays.ToString & " Days]
                        From (" & mQry & ") As VMain
                        GROUP By VMain.Subcode, VMain.Div_Code
                        Having Sum(Case When VMain.DaysDiff > " & mLeavergeDays & " Or VMain.AmtCr > 0 Then VMain.AmtDr - VMain.AmtCr Else 0 End) > 0
                        Order By [Party]"

                    DsHeader = AgL.FillData(mQry, AgL.GCn)
                End If








                If DsHeader.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

                ReportFrm.Text = "Debtors Outstanding Report - " + ReportFrm.FGetText(0)
                ReportFrm.ClsRep = Me
                ReportFrm.ReportProcName = "ProcDebtorsOutstaningReport"
                ReportFrm.AllowAutoResizeRows = False
                ReportFrm.ProcFillGrid(DsHeader)



                If ReportFrm.FGetText(0) = "Invoice Wise Detail" Then
                    ReportFrm.DGL1.Columns("Subcode").Visible = False
                    Dim I As Integer
                    Dim mRunningBal As Double
                    Dim mSubcodeCount As Integer
                    mRunningBal = 0 : mSubcodeCount = 1
                    For I = 0 To ReportFrm.DGL1.RowCount - 1
                        If I > 0 Then
                            If ReportFrm.DGL1.Item("Subcode", I).Value <> ReportFrm.DGL1.Item("Subcode", I - 1).Value Then
                                mRunningBal = 0
                                mSubcodeCount += 1
                            End If
                        End If

                        mRunningBal += Val(ReportFrm.DGL1.Item("Amt Dr", I).Value) - Val(ReportFrm.DGL1.Item("Amt Cr", I).Value)
                        ReportFrm.DGL1.Item("Balance", I).Value = mRunningBal
                        ReportFrm.DGL1.Item("Dr Cr", I).Value = IIf(mRunningBal < 0, "Cr", "Dr")
                        If AgL.XNull(ReportFrm.DGL1.Item("Voucher No", I).Value) = "Total" Then
                            'ReportFrm.DGL1.Item("Age", I).Value = ""
                            ReportFrm.DGL1.Item("Voucher Date", I).Value = ""
                        End If
                    Next
                    If mSubcodeCount = 1 Then
                        ReportFrm.DGL2.Item("Balance", 0).Value = ReportFrm.DGL1.Item("Balance", I - 1).Value
                        ReportFrm.DGL2.Item("Dr Cr", 0).Value = ReportFrm.DGL1.Item("Dr Cr", I - 1).Value
                    Else
                        ReportFrm.DGL2.Item("Balance", 0).Value = ""
                        ReportFrm.DGL2.Item("Dr Cr", 0).Value = ""
                    End If
                End If

            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            DsHeader = Nothing
        End Try
    End Sub
    Private Function FGetDateQry(FieldName As String) As String
        Return " DATETIME(substr(" & FieldName & ",8,4) || '-' ||
                Case When substr(" & FieldName & ",4,3) = 'Jan' Then '01'
                     When substr(" & FieldName & ",4,3) = 'Feb' Then '02'
                     When substr(" & FieldName & ",4,3) = 'Mar' Then '03'
                     When substr(" & FieldName & ",4,3) = 'Apr' Then '04'
                     When substr(" & FieldName & ",4,3) = 'May' Then '05'
                     When substr(" & FieldName & ",4,3) = 'Jun' Then '06'
                     When substr(" & FieldName & ",4,3) = 'Jul' Then '07'
                     When substr(" & FieldName & ",4,3) = 'Aug' Then '08'
                     When substr(" & FieldName & ",4,3) = 'Sep' Then '09'
                     When substr(" & FieldName & ",4,3) = 'Oct' Then '10'
                     When substr(" & FieldName & ",4,3) = 'Nov' Then '11'
                     When substr(" & FieldName & ",4,3) = 'Dec' Then '12'
                Else Null End || '-' || substr(" & FieldName & ",1,2)) "
    End Function

    Public Sub ProcMoneyReceiptReport(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try
            Dim mCondStr$ = ""
            Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"
            Dim mCommissionPer As Double


            RepTitle = "Money Receipt Report"

            If mFilterGrid IsNot Nothing And mGridRow IsNot Nothing Then
                If mGridRow.DataGridView.Columns.Contains("Search Code") = True Then
                    If mFilterGrid.Item(GFilter, 0).Value = "Party Wise Summary" Then
                        mFilterGrid.Item(GFilter, 0).Value = "Voucher Wise Detail"
                        mFilterGrid.Item(GFilter, 4).Value = mGridRow.Cells("Party").Value
                        mFilterGrid.Item(GFilterCode, 4).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = "Agent Wise Summary" Then
                        mFilterGrid.Item(GFilter, 0).Value = "Voucher Wise Detail (Agent)"
                        mFilterGrid.Item(GFilter, 6).Value = mGridRow.Cells("Agent").Value
                        mFilterGrid.Item(GFilterCode, 6).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = "Voucher Wise Detail" Or mFilterGrid.Item(GFilter, 0).Value = "Voucher Wise Detail (Agent)" Then
                        ClsMain.FOpenForm(mGridRow.Cells("Search Code").Value, ReportFrm)
                        ReportFrm.FiterGridCopy_Arr.RemoveAt(ReportFrm.FiterGridCopy_Arr.Count - 1)
                        Exit Sub
                    End If
                Else
                    Exit Sub
                End If
            End If

            mCondStr = " Where VT.Category='RCT' And L.ReferenceDocID is Null And Party.Nature Not In ('Cash','Bank') "
            mCondStr = mCondStr & " AND Date(H.V_Date) Between " & AgL.Chk_Date(CDate(ReportFrm.FGetText(1)).ToString("s")) & " And " & AgL.Chk_Date(CDate(ReportFrm.FGetText(2)).ToString("s")) & " "
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Subcode", 4)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.V_Type", 5)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("LTV.Agent", 6)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("City.CityCode", 7)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("City.State", 8)
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("H.Site_Code", 9), "''", "'")
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("H.Div_Code", 10), "''", "'")

            mCommissionPer = Val(ReportFrm.FGetText(3))

            'If ReportFrm.FGetText(8) <> "All" Then
            '    mCondStr += " And H.Agent = '" & ReportFrm.FGetCode(8) & "' "
            'End If

            'mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.Agent", 8)

            mQry = " SELECT H.DocID, strftime('%d/%m/%Y', H.V_Date) As V_Date, H.V_Date As V_Date_ActualFormat,
                    L.Subcode as Party, Party.Name As PartyName, LinkedParty.Name as LinkedPartyName, LTV.Agent As AgentCode, Agent.Name As AgentName , 
                    City.CityCode, City.CityName, State.Code As StateCode, State.Description As StateName,
                    H.Div_Code || H.Site_Code || '-' || H.V_Type || '-' || H.ManualRefNo as VoucherNo, H.ManualRefNo RecId, 
                    L.Amount as Amount, L.Remarks Narration, H.Remarks, " & mCommissionPer & " as CommissionPer, L.Amount*" & mCommissionPer & "/100 as Commission
                    FROM LedgerHeadDetail L                     
                    Left Join LedgerHead H On L.DocID = H.DocID
                    Left Join viewHelpSubgroup Party On L.Subcode = Party.Code 
                    Left Join viewHelpSubgroup LinkedParty On L.LinkedSubcode = LinkedParty.Code 
                    Left Join (Select SILTV.Subcode, Max(SILTV.Agent) as Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode) as LTV On Party.code = LTV.Subcode
                    Left Join viewHelpSubGroup Agent On LTV.Agent = Agent.Code 
                    Left Join City On Party.CityCode = City.CityCode 
                    Left Join State On City.State = State.Code
                    LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type " & mCondStr


            If ReportFrm.FGetText(0) = "Voucher Wise Detail" Then
                mQry = " Select VMain.DocId As SearchCode, Max(VMain.V_Date) As VoucherDate, Max(VMain.VoucherNo) as VoucherNo,
                    Max(VMain.PartyName) As Party, Max(VMain.LinkedPartyName) as LinkedParty, Max(VMain.Amount) as Amount, Max(VMain.Narration) as Narration, Max(VMain.Remarks) as Remarks
                    From (" & mQry & ") As VMain
                    GROUP By VMain.DocId 
                    Order By VoucherDate, VoucherNo  "
            ElseIf ReportFrm.FGetText(0) = "Voucher Wise Detail (Agent)" Then
                mQry = " Select VMain.DocId As SearchCode, Max(VMain.V_Date) As VoucherDate, Max(VMain.VoucherNo) as VoucherNo,
                    Max(VMain.PartyName) As Party, Max(VMain.Narration) as Narration, Max(VMain.Amount) as Amount, Max(VMain.CommissionPer) as CommissionPer, Max(VMain.Commission) as CommissionAmt
                    From (" & mQry & ") As VMain
                    GROUP By VMain.DocId 
                    Order By VoucherDate, VoucherNo  "
            ElseIf ReportFrm.FGetText(0) = "Agent Wise Summary" Then
                mQry = " Select VMain.AgentCode As SearchCode, Max(VMain.AgentName) As [Agent], 
                    Sum(VMain.Amount) As [Amount], Max(VMain.CommissionPer) as [Commission Per], Sum(VMain.Commission) As [Commission]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.AgentCode 
                    Order By [Agent]"
            ElseIf ReportFrm.FGetText(0) = "Party Wise Summary" Then
                mQry = " Select VMain.Party As SearchCode, Max(VMain.PartyName) As [Party], Max(VMain.LinkedPartyName) as LinkedParty,
                    Sum(VMain.Amount) As [Amount]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.Party 
                    Order By [Party]"
            End If




            DsHeader = AgL.FillData(mQry, AgL.GCn)

            If DsHeader.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            ReportFrm.Text = "Money Receipt Report - " + ReportFrm.FGetText(0)
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = "ProcMoneyReceiptReport"

            ReportFrm.ProcFillGrid(DsHeader)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsHeader = Nothing
        End Try
    End Sub


    Public Sub ProcPackedBalesReport(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try
            Dim mCondStr$ = ""
            Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"


            RepTitle = "LR Status Report"

            If mFilterGrid IsNot Nothing And mGridRow IsNot Nothing Then
                If mGridRow.DataGridView.Columns.Contains("Search Code") = True Then
                    If mFilterGrid.Item(GFilter, 0).Value = "Location Type Wise Summary" Then
                        mFilterGrid.Item(GFilter, 1).Value = mGridRow.Cells("Location Type").Value
                        mFilterGrid.Item(GFilterCode, 1).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                        'If mGridRow.Cells("Location Type").Value = SubgroupType.Transporter Then
                        mFilterGrid.Item(GFilter, 0).Value = "Location Wise Summary"
                        'Else
                        'mFilterGrid.Item(GFilter, 0).Value = "Party Wise Summary"
                        'End If
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = "Location Wise Summary" Then
                        mFilterGrid.Item(GFilter, 2).Value = mGridRow.Cells("Location").Value
                        mFilterGrid.Item(GFilterCode, 2).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                        mFilterGrid.Item(GFilter, 0).Value = "LR Wise Detail"
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = "Party Wise Summary" Then
                        mFilterGrid.Item(GFilter, 3).Value = mGridRow.Cells("Party").Value
                        mFilterGrid.Item(GFilterCode, 3).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                        mFilterGrid.Item(GFilter, 0).Value = "LR Wise Detail"
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = "Agent Wise Summary" Then
                        mFilterGrid.Item(GFilter, 4).Value = mGridRow.Cells("Agent").Value
                        mFilterGrid.Item(GFilterCode, 4).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                        mFilterGrid.Item(GFilter, 0).Value = "LR Wise Detail"
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = "LR Wise Detail" Then
                        ClsMain.FOpenForm(mGridRow.Cells("Search Code").Value, ReportFrm)
                        ReportFrm.FiterGridCopy_Arr.RemoveAt(ReportFrm.FiterGridCopy_Arr.Count - 1)
                        Exit Sub
                    End If
                Else
                    Exit Sub
                End If
            End If

            mCondStr = " Where 1=1 "
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("SubgroupType.SubgroupType", 1)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.CurrentGodown", 2)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("SH.Subcode", 3)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("Sgd.Agent", 4)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("Party.CityCode", 5)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("City.State", 6)
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("SH.Site_Code", 7), "''", "'")
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("SH.Div_Code", 8), "''", "'")



            mQry = "select SH.DocID, SH.V_Type, H.Code as Barcode, H.LrBaleNo, strftime('%d/%m/%Y', H.LrDate) As LrDate, H.LrDate as LrDateActualFormat, H.Weight, H.PrivateMark, H.LrNo, H.Transporter, Transporter.Name as TransporterName, 
                    SH.Subcode as Party, Party.Name as PartyName, L.CurrentGodown, CurrentGodown.Name as CurrentGodownName, SubgroupType.SubgroupType as LocationType, Sgd.Agent, Agent.Name as AgentName
                    From LrBale H
                    Left Join LrBaleSiteDetail L On H.Code = L.Code
                    Left Join viewHelpSubgroup Transporter On H.Transporter = Transporter.Code
                    Left Join StockHead SH On H.GenDocID = SH.DocID
                    Left Join viewHelpSubgroup Party On SH.Subcode = Party.Code
                    Left Join City On Party.CityCode = City.CityCode 
                    Left Join viewHelpSubgroup CurrentGodown On L.CurrentGodown = CurrentGodown.Code
                    Left Join SubgroupType On CurrentGodown.SubgroupType = SubGroupType.SubGroupType
                    Left Join (Select SSDD.Subcode, SSDD.Site_Code, SSDD.Div_Code, Max(SSDD.Agent) as Agent 
                               From SubgroupSiteDivisionDetail SSDD  
                               Group By SSDD.Subcode, SSDD.Site_Code, SSDD.Div_Code) as Sgd 
                               On Party.code = Sgd.Subcode And SH.Site_Code = Sgd.Site_Code And SH.Div_Code = Sgd.Div_Code
                    Left Join viewHelpSubGroup Agent On Sgd.Agent = Agent.Code 
                    " & mCondStr




            If ReportFrm.FGetText(0) = "LR Wise Detail" Then
                mQry = " Select Max(VMain.DocId) As SearchCode, Max(VMain.LrNo) as LrNo, Max(VMain.LrDate) As LrDate, 
                    Max(VMain.LrBaleNo) as BaleNo, Max(VMain.PrivateMark) as PrivateMark, Max(VMain.Weight) as Weight, 
                    Max(VMain.TransporterName) As Transporter, Max(VMain.PartyName) As Party, 
                    Max(VMain.CurrentGodownName) as CurrLocation, Max(VMain.LocationType) as LocationType
                    From (" & mQry & ") As VMain
                    GROUP By VMain.Barcode 
                    Order By Max(LrDate), Max(LrBaleNo)  "
            ElseIf ReportFrm.FGetText(0) = "Agent Wise Summary" Then
                mQry = " Select VMain.Agent As SearchCode, Max(VMain.AgentName) As [Agent],
                    Sum(Case When VMain.LocationType = '" & SubgroupType.Transporter & "' Then  1 Else 0 End) As [Transporter],
                    Sum(Case When VMain.LocationType = '" & SubgroupType.Employee & "' Then  1 Else 0 End) As [Carrier],
                    Sum(Case When VMain.LocationType = '" & SubgroupType.Godown & "' Then  1 Else 0 End) As [Godown],
                    Sum(Case When VMain.LocationType Not In ('" & SubgroupType.Transporter & "', '" & SubgroupType.Employee & "', '" & SubgroupType.Godown & "', '" & SubgroupType.Shop & "') Then  1 Else 0 End) As [Yard]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.Agent
                    Order By [Agent] "
            ElseIf ReportFrm.FGetText(0) = "Party Wise Summary" Then
                mQry = " Select VMain.Party As SearchCode, Max(VMain.PartyName) As [Party],
                    Sum(Case When VMain.LocationType = '" & SubgroupType.Transporter & "' Then  1 Else 0 End) As [Transporter],
                    Sum(Case When VMain.LocationType = '" & SubgroupType.Employee & "' Then  1 Else 0 End) As [Carrier],
                    Sum(Case When VMain.LocationType = '" & SubgroupType.Godown & "' Then  1 Else 0 End) As [Godown],
                    Sum(Case When VMain.LocationType Not In ('" & SubgroupType.Transporter & "', '" & SubgroupType.Employee & "', '" & SubgroupType.Godown & "', '" & SubgroupType.Shop & "') Then  1 Else 0 End) As [Yard]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.Party
                    Order By [Party] "
            ElseIf ReportFrm.FGetText(0) = "Location Type Wise Summary" Then
                mQry = " Select VMain.LocationType As SearchCode, Max(VMain.LocationType) As [LocationType],
                    Count(VMain.Barcode) As [NoOfBales]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.LocationType
                    Order By [LocationType] "
            ElseIf ReportFrm.FGetText(0) = "Location Wise Summary" Then
                mQry = " Select VMain.CurrentGodown As SearchCode, Max(VMain.CurrentGodownName) as [Location], Max(VMain.LocationType) As [LocationType],
                    Count(VMain.Barcode) As [NoOfBales]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.CurrentGodown
                    Order By [Location] "
            End If




            DsHeader = AgL.FillData(mQry, AgL.GCn)

            If DsHeader.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            ReportFrm.Text = "Packed Bales Report - " + ReportFrm.FGetText(0)
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = "ProcPackedBalesReport"

            ReportFrm.ProcFillGrid(DsHeader)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsHeader = Nothing
        End Try
    End Sub


    Public Sub ProcBaleMovementReport(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try
            Dim mCondStr$ = ""
            Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"


            RepTitle = "LR Status Report"

            If mFilterGrid IsNot Nothing And mGridRow IsNot Nothing Then
                If mGridRow.DataGridView.Columns.Contains("Search Code") = True Then
                    If mFilterGrid.Item(GFilter, 0).Value = "Location Type Wise Summary" Then
                        mFilterGrid.Item(GFilter, 3).Value = mGridRow.Cells("Location Type").Value
                        mFilterGrid.Item(GFilterCode, 3).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                        'If mGridRow.Cells("Location Type").Value = SubgroupType.Transporter Then
                        mFilterGrid.Item(GFilter, 0).Value = "Location Wise Summary"
                        'Else
                        'mFilterGrid.Item(GFilter, 0).Value = "Party Wise Summary"
                        'End If
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = "Location Wise Summary" Then
                        mFilterGrid.Item(GFilter, 4).Value = mGridRow.Cells("Location").Value
                        mFilterGrid.Item(GFilterCode, 4).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                        mFilterGrid.Item(GFilter, 0).Value = "LR Wise Detail"
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = "Party Wise Summary" Then
                        mFilterGrid.Item(GFilter, 5).Value = mGridRow.Cells("Party").Value
                        mFilterGrid.Item(GFilterCode, 5).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                        mFilterGrid.Item(GFilter, 0).Value = "LR Wise Detail"
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = "Agent Wise Summary" Then
                        mFilterGrid.Item(GFilter, 6).Value = mGridRow.Cells("Agent").Value
                        mFilterGrid.Item(GFilterCode, 6).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                        mFilterGrid.Item(GFilter, 0).Value = "LR Wise Detail"
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = "LR Wise Detail" Then
                        ClsMain.FOpenForm(mGridRow.Cells("Search Code").Value, ReportFrm)
                        ReportFrm.FiterGridCopy_Arr.RemoveAt(ReportFrm.FiterGridCopy_Arr.Count - 1)
                        Exit Sub
                    End If
                Else
                    Exit Sub
                End If
            End If

            mCondStr = " Where I.Code='LrBale' And S.Qty_Rec > 0 "
            mCondStr = mCondStr & " AND Date(S.V_Date) Between " & AgL.Chk_Date(CDate(ReportFrm.FGetText(1)).ToString("s")) & " And " & AgL.Chk_Date(CDate(ReportFrm.FGetText(2)).ToString("s")) & " "
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("SubgroupType.SubgroupType", 3)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("S.Godown", 4)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("SH.Subcode", 5)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("Sgd.Agent", 6)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("Party.CityCode", 7)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("City.State", 8)
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("SH.Site_Code", 9), "''", "'")
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("SH.Div_Code", 10), "''", "'")




            mQry = "
                    select S.DocID, VT.Description as EntryType, strftime('%d/%m/%Y', S.V_Date) As EntryDate,  
                   " & IIf(AgL.PubServerName = "", " julianday(S.V_Date) - julianday(SH.V_Date) ", " DateDiff(Day, SH.V_Date, S.V_Date) ") & "  as AgeDays, 
                    H.Code as Barcode, H.LrBaleNo, strftime('%d/%m/%Y', H.LrDate) As LrDate, H.LrDate as LrDateActualFormat, 
                    H.Weight, H.PrivateMark, H.LrNo, H.Transporter, Transporter.Name as TransporterName, SH.Subcode as Party, 
                    Party.Name as PartyName, Godown.Code as Godown, Godown.Name as GodownName, SubgroupType.SubgroupType as LocationType, 
                    Sgd.Agent, Agent.Name as AgentName, S.Qty_Iss, S.Qty_Rec,
                    (Case When L.CurrentGodown = S.Godown Then 1 Else 0 End) as CurrentBalance
                    From Stock S
                    Left Join LrBale H On S.Barcode = H.Code
                    Left Join LrBaleSiteDetail L On H.Code = L.Code
                    Left Join Voucher_Type Vt On S.V_Type = Vt.V_Type
                    Left Join viewHelpSubgroup Transporter On H.Transporter = Transporter.Code
                    Left Join StockHead SH On H.GenDocID = SH.DocID
                    Left Join viewHelpSubgroup Party On SH.Subcode = Party.Code
                    Left Join City On Party.CityCode = City.CityCode 
                    Left Join viewHelpSubgroup Godown On S.Godown = Godown.Code
                    Left Join SubgroupType On Godown.SubgroupType = SubGroupType.SubGroupType
                    Left Join (Select SSDD.Subcode, SSDD.Site_Code, SSDD.Div_Code, Max(SSDD.Agent) as Agent 
                               From SubgroupSiteDivisionDetail SSDD  
                               Group By SSDD.Subcode, SSDD.Site_Code, SSDD.Div_Code) as Sgd 
                               On Party.code = Sgd.Subcode And SH.Site_Code = Sgd.Site_Code And SH.Div_Code = Sgd.Div_Code
                    Left Join viewHelpSubGroup Agent On Sgd.Agent = Agent.Code 
                    Left Join Item I On S.Item = I.Code                    
                    " & mCondStr


            If ReportFrm.FGetText(0) = "LR Wise Detail" Then
                mQry = " Select Max(VMain.DocId) As SearchCode, Max(VMain.EntryType) as EntryType, Max(VMain.EntryDate) as EntryDate, Max(VMain.AgeDays) as AgeDays, Max(VMain.LrNo) as LrNo, Max(VMain.LrDate) As LrDate, 
                    Max(VMain.LrBaleNo) as BaleNo, Max(VMain.PrivateMark) as PrivateMark, Max(VMain.Weight) as Weight, 
                    Max(VMain.TransporterName) As Transporter, Max(VMain.PartyName) As Party, 
                    Max(VMain.GodownName) as Location, Max(VMain.LocationType) as LocationType
                    From (" & mQry & ") As VMain
                    GROUP By VMain.Barcode, VMain.LrDate, VMain.LrBaleNo 
                    Order By VMain.LrDate, VMain.LrBaleNo  "
            ElseIf ReportFrm.FGetText(0) = "Agent Wise Summary" Then
                mQry = " Select VMain.Agent As SearchCode, Max(VMain.AgentName) As [Agent],
                    Sum(Case When VMain.LocationType = '" & SubgroupType.Transporter & "' Then  Vmain.Qty_Rec Else 0 End) As [Transporter],
                    Sum(Case When VMain.LocationType = '" & SubgroupType.Employee & "' Then  Vmain.Qty_Rec Else 0 End) As [Carrier],
                    Sum(Case When VMain.LocationType = '" & SubgroupType.Godown & "' Then  Vmain.Qty_Rec Else 0 End) As [Godown],
                    Sum(Case When VMain.LocationType = '" & SubgroupType.Shop & "' Then  Vmain.Qty_Rec Else 0 End) As [Shop],
                    Sum(Case When VMain.LocationType Not In ('" & SubgroupType.Transporter & "', '" & SubgroupType.Employee & "', '" & SubgroupType.Godown & "', '" & SubgroupType.Shop & "') Then  VMain.Barcode Else 0 End) As [Yard]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.Agent
                    Order By [Agent] "
            ElseIf ReportFrm.FGetText(0) = "Party Wise Summary" Then
                mQry = " Select VMain.Party As SearchCode, Max(VMain.PartyName) As [Party],
                    Count(Case When VMain.LocationType = '" & SubgroupType.Transporter & "' Then  VMain.Barcode Else 0 End) As [Transporter],
                    Count(Case When VMain.LocationType = '" & SubgroupType.Employee & "' Then  VMain.Barcode Else 0 End) As [Carrier],
                    Count(Case When VMain.LocationType = '" & SubgroupType.Godown & "' Then  VMain.Barcode Else 0 End) As [Godown],
                    Count(Case When VMain.LocationType = '" & SubgroupType.Shop & "' Then  VMain.Barcode Else 0 End) As [Shop],
                    Count(Case When VMain.LocationType Not In ('" & SubgroupType.Transporter & "', '" & SubgroupType.Employee & "', '" & SubgroupType.Godown & "', '" & SubgroupType.Shop & "') Then  VMain.Barcode Else 0 End) As [Yard]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.Party
                    Order By [Party] "
            ElseIf ReportFrm.FGetText(0) = "Location Type Wise Summary" Then
                mQry = " Select VMain.LocationType As SearchCode, Max(VMain.LocationType) As [LocationType],
                    Sum(VMain.Qty_Rec) As [BalesReceived], Sum(VMain.CurrentBalance) as CurrBaleBalance
                    From (" & mQry & ") As VMain
                    GROUP By VMain.LocationType
                    Order By [LocationType] "
            ElseIf ReportFrm.FGetText(0) = "Location Wise Summary" Then
                mQry = " Select VMain.Godown As SearchCode, Max(VMain.GodownName) as [Location], Max(VMain.LocationType) As [LocationType],
                    Sum(VMain.Qty_Rec) As [BalesReceived], Sum(VMain.CurrentBalance) as CurrBaleBalance
                    From (" & mQry & ") As VMain
                    GROUP By VMain.Godown
                    Order By [Location] "
            End If




            DsHeader = AgL.FillData(mQry, AgL.GCn)

            If DsHeader.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            ReportFrm.Text = "Bale Movement Report - " + ReportFrm.FGetText(0)
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = "ProcBaleMovementReport"

            ReportFrm.ProcFillGrid(DsHeader)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsHeader = Nothing
        End Try
    End Sub


    Public Sub ProcFsnAnalysis(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try
            Dim mCondStr$ = ""
            Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"
            Dim mFastPer As Double
            Dim mSlowPer As Double
            Dim mCondStrItem As String

            RepTitle = "FSN Analysis"



            mCondStr = " Where 1=1 "
            mCondStrItem = " Where 1=1 "
            mCondStr = mCondStr & " AND Date(H.V_Date) Between " & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & " And " & AgL.Chk_Date(CDate(ReportFrm.FGetText(1)).ToString("s")) & " "
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemGroup", 4)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemCategory", 5)
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("I.Div_Code", 6), "''", "'")
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("H.Site_Code", 7), "''", "'")



            mCondStrItem = mCondStrItem & ReportFrm.GetWhereCondition("I.ItemGroup", 4)
            mCondStrItem = mCondStrItem & ReportFrm.GetWhereCondition("I.ItemCategory", 5)
            mCondStrItem = mCondStrItem & Replace(ReportFrm.GetWhereCondition("I.Div_Code", 6), "''", "'")
            mCondStrItem = mCondStrItem & Replace(ReportFrm.GetWhereCondition("S.Site_Code", 7), "''", "'")


            mFastPer = Val(ReportFrm.FGetText(2))
            mSlowPer = Val(ReportFrm.FGetText(3))


            If AgL.IsTableExist("#TempTbl", AgL.GCn) Then
                mQry = "Drop Table TempTbl;"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
            End If

            mQry = "

                    CREATE Temporary TABLE TempTbl( 
                    id INTEGER PRIMARY KEY AUTOINCREMENT,                          
                     ItemName NVARCHAR(255),
                     Stock Float,
                     Qty_Rec Float,
                     Qty_Iss Float,
                     Sales BigInt
                    );



                    insert into TempTbl (ItemName, Stock,  Qty_Rec, Qty_Iss, Sales) 
                    SELECT I.Description as ItemName, Stk.CurrentStock, Stk.Qty_Rec, Stk.Qty_Iss, Count(CASE WHEN l.Gross_Amount >0 THEN 1 ELSE 0 END ) Sales    
                    FROM (
                            select S.Item, Sum(Qty_Rec) as Qty_Rec, Sum(Qty_Iss) as Qty_Iss, Sum(s.Qty_Rec-S.Qty_Iss) as CurrentStock
                            from stock s 
                            Left Join Item I On S.Item = I.Code
                            " & mCondStrItem & "
                            Group By S.Item
                            Having Sum(s.Qty_Rec-S.Qty_Iss)>0    
                         ) as Stk     
                    LEFT JOIN SaleInvoiceDetail L ON L.Item = Stk.Item
                    Left Join SaleInvoice H On L.DocId = H.DocID
                    LEFT JOIN Item I ON I.Code = L.Item
                    LEFT JOIN ItemGroup IG ON I.ItemGroup = IG.Code
                    LEFT JOIN ItemCategory IC ON IC.Code = I.ItemCategory                   
                    " & mCondStr & "
                    GROUP BY I.Description
                    Order By Sales Desc;



                    Drop Table If Exists TempTbl2;

                    CREATE Temp TABLE TempTbl2(
                             ItemName NVARCHAR(255),
                             Sales BigInt,
                             Stock Float,
                             Qty_Rec Float,
                             Qty_Iss Float,
                             CumulativeTrans BigInt,
                             TotalTrans BigInt
                             );



                    Insert Into TempTbl2
                    SELECT
                        ps.[ItemName], 
                        ps.[Sales] AS Trans,
                        Ps.Stock,
                        Round(Ps.Qty_Rec,2),
                        Round(Ps.Qty_Iss,2),
                        (Select Sum(Sales) From TempTbl Where Id<=Ps.Id) as CumulativeTrans,
                        --SUM(ps.[Sales]) OVER (ORDER BY ps.[Sales] DESC) AS CumulativeTrans,
                        (Select SUM(Sales) From TempTbl) AS TotalTrans
                    FROM  TempTbl ps
                    GROUP BY
                        ps.ItemName
                        order by ps.id;    





                    SELECT
                        ps.[ItemName],
                        Ps.Stock, 
                        Ps.Qty_Rec,
                        PS.Qty_Iss,
                        ps.[Sales] AS SaleInvoices,
                         CASE
                            WHEN PS.CumulativeTrans*100 / PS.TotalTrans <= " & mFastPer & " 
                                THEN 'Fast'
                            WHEN PS.CumulativeTrans*100 / PS.TotalTrans <= " & mSlowPer & "
                                THEN 'Slow'
                            ELSE 'Non Moving'
                        END AS Class

                    FROM    TempTbl2 ps
                    order by ps.Sales desc

                    "


            DsHeader = AgL.FillData(mQry, AgL.GCn)

            If DsHeader.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            ReportFrm.Text = "FSN Analysis"
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = "ProcFsnAnalysis"

            ReportFrm.ProcFillGrid(DsHeader)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsHeader = Nothing
        End Try
    End Sub



    'Private Sub FOpenForm(DocId As String)
    '    Dim FrmObjMDI As Object
    '    Dim FrmObj As Object
    '    Dim DtVType As DataTable
    '    Dim StrModuleName As String = ""
    '    Dim StrMnuName As String = ""
    '    Dim StrMnuText As String = ""

    '    Try
    '        DtVType = AgL.FillData("Select V_Type,MnuName,MnuText,MnuAttachedInModule From Voucher_Type Where IfNull(MnuName,'')<>'' And V_Type = '" & AgL.DeCodeDocID(DocId, AgLibrary.ClsMain.DocIdPart.VoucherType) & "' Order By V_Type", AgL.GCn).tables(0)
    '        If DtVType.Rows.Count > 0 Then
    '            StrModuleName = AgL.XNull(DtVType.Rows(0)("MnuAttachedInModule"))
    '            StrMnuName = AgL.XNull(DtVType.Rows(0)("MnuName"))
    '            StrMnuText = AgL.XNull(DtVType.Rows(0)("MnuText"))

    '            FrmObjMDI = ReportFrm.MdiParent
    '            FrmObj = FrmObjMDI.FOpenForm(StrModuleName, StrMnuName, StrMnuText)
    '            FrmObj.MdiParent = ReportFrm.MdiParent
    '            FrmObj.OpenDocId = DocId
    '            FrmObj.Show()
    '            FrmObj.FindMove(DocId)
    '            FrmObj = Nothing
    '        Else
    '            MsgBox("Define Details For This Voucher Type.")
    '        End If
    '        ReportFrm.FiterGridCopy_Arr.RemoveAt(ReportFrm.FiterGridCopy_Arr.Count - 1)
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub

#Region "EWay Bill Generation"
    Public Sub ProcEWayBillGeneration(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing, Optional bDocId As String = "")
        Try
            Dim mSaleCondStr$ = ""
            Dim mPurchaseReturnCondStr$ = ""
            Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"

            RepTitle = "Create JSON File"


            If mFilterGrid IsNot Nothing And mGridRow IsNot Nothing Then
                If mGridRow.DataGridView.Columns.Contains("Search Code") = True Then
                    ClsMain.FOpenForm(mGridRow.Cells("Search Code").Value, ReportFrm)
                    ReportFrm.FiterGridCopy_Arr.RemoveAt(ReportFrm.FiterGridCopy_Arr.Count - 1)
                Else
                    Exit Sub
                End If
            End If


            If bDocId <> "" Then
                mQry = " Select V_Date From SaleInvoice H Where H.DocId = '" & bDocId & "'"
                Dim DtInvoiceDetail As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
                If DtInvoiceDetail.Rows.Count > 0 Then
                    ReportFrm.FilterGrid.Item(GFilter, 0).Value = ClsMain.FormatDate(AgL.XNull(DtInvoiceDetail.Rows(0)("V_Date")))
                    ReportFrm.FilterGrid.Item(GFilter, 1).Value = ClsMain.FormatDate(AgL.XNull(DtInvoiceDetail.Rows(0)("V_Date")))
                    ReportFrm.FilterGrid.Item(GFilter, 2).Value = 0
                End If


                mSaleCondStr = " Where H.DocId = '" & bDocId & "' "
            Else
                mSaleCondStr = " Where Date(H.V_Date) Between " & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & " And " & AgL.Chk_Date(CDate(ReportFrm.FGetText(1)).ToString("s")) & " "
            End If
            mSaleCondStr += " And H.Div_Code = '" & AgL.PubDivCode & "' "
            mSaleCondStr += " And H.Site_Code = '" & AgL.PubSiteCode & "' "
            mSaleCondStr += " And Vt.NCat = 'SI' "
            mSaleCondStr += " And IfNull(H.Net_Amount,0) > " & ReportFrm.FGetText(2) & " "


            If bDocId <> "" Then
                mPurchaseReturnCondStr = " Where H.DocId = '" & bDocId & "' "
            Else
                mPurchaseReturnCondStr = " Where Date(H.V_Date) Between " & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & " And " & AgL.Chk_Date(CDate(ReportFrm.FGetText(1)).ToString("s")) & " "
            End If
            mPurchaseReturnCondStr += " And H.Div_Code = '" & AgL.PubDivCode & "' "
            mPurchaseReturnCondStr += " And H.Site_Code = '" & AgL.PubSiteCode & "' "
            mPurchaseReturnCondStr += " And Vt.NCat = 'PR' "
            mPurchaseReturnCondStr += " And IfNull(Abs(H.Net_Amount),0) > " & ReportFrm.FGetText(2) & " "


            'Sale Invoice Qry
            mQry = "Select " & IIf(bDocId <> "", "'þ'", "'o'") & " As Tick, '' As Exception, H.DocId  As SearchCode, Vt.Description As VoucherType, 
                '" & IIf(AgL.PubPrintDivisionShortNameOnDocumentsYn, AgL.PubDivShortName, "") & IIf(AgL.PubPrintSiteShortNameOnDocumentsYn, AgL.PubSiteShortName, "") & "' || (Case When VT.Short_Name Is Not Null Then VT.Short_Name Else '' End) || H.ManualRefNo as InvoiceNo, 
                strftime('%d/%m/%Y', H.V_Date) As InvoiceDate, IfNull(H.Net_Amount,0) As InvoiceValue,
                Sg.DispName As Party, 
                Case When H.ShipToParty Is Not Null Then ShipTo.Pin Else H.SaleToPartyPinCode End As PinCode, 
                Case When H.ShipToParty Is Not Null Then ShipToState.Description Else S.Description End As State, 
                TSg.DispName As Transporter,
                VDist.Distance As Distance
                From SaleInvoice H 
                LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                LEFT JOIN City C On H.SaleToPartyCity = C.CityCode
                LEFT JOIN State S On C.State = S.Code
                LEFT JOIN SubGroup Sg On H.SaleToParty = Sg.SubCode
                LEFT JOIN SubGroup ShipTo On H.ShipToParty = ShipTo.SubCode
                LEFT JOIN City ShipToCity On ShipTo.CityCode = ShipToCity.CityCode
                LEFT JOIN State ShipToState On ShipToCity.State = ShipToState.Code
                LEFT JOIN SaleInvoiceTransport Sit On H.DocId = Sit.DocId
                LEFT JOIN (Select SubCode, Max(Transporter) Transporter 
                            From SubgroupSiteDivisionDetail
                            Group By SubCode) As Hlt On H.SaleToParty = Hlt.SubCode
                LEFT JOIN SubGroup TSg ON IfNull(Sit.Transporter,Hlt.Transporter) = TSg.SubCode
                LEFT JOIN (Select Subcode, RegistrationNo As SalesTaxNo
                            From SubgroupRegistration 
                            Where RegistrationType = 'Sales Tax No') As VReg On H.SaleToParty = VReg.SubCode
                LEFT JOIN (Select Subcode, RegistrationNo As SalesTaxNo
                            From SubgroupRegistration 
                            Where RegistrationType = 'Sales Tax No') As VTranReg On TSg.SubCode = VTranReg.SubCode
                LEFT JOIN (Select SubCode, Distance
                            From SubgroupSiteDivisionDetail 
                            Where Site_Code = '" & AgL.PubSiteCode & "' 
                            And Div_Code = '" & AgL.PubDivCode & "') As VDist On IfNull(H.ShipToParty,H.SaleToParty) = VDist.SubCode " & mSaleCondStr

            mQry = mQry + " UNION ALL "

            'Purchase Invoice Return Qry
            mQry = mQry + "Select " & IIf(bDocId <> "", "'þ'", "'o'") & " As Tick, '' As Exception, H.DocId  As SearchCode, Vt.Description As VoucherType, 
                '" & IIf(AgL.PubPrintDivisionShortNameOnDocumentsYn, AgL.PubDivShortName, "") & IIf(AgL.PubPrintSiteShortNameOnDocumentsYn, AgL.PubSiteShortName, "") & "' || (Case When VT.Short_Name Is Not Null Then VT.Short_Name Else '' End) || H.ManualRefNo as InvoiceNo, 
                strftime('%d/%m/%Y', H.V_Date) As InvoiceDate, IfNull(Abs(H.Net_Amount),0) As InvoiceValue,
                Sg.DispName As Party, 
                Case When H.ShipToParty Is Not Null Then ShipTo.Pin Else H.VendorPinCode End As PinCode, 
                Case When H.ShipToParty Is Not Null Then ShipToState.Description Else S.Description End As State, 
                TSg.DispName As Transporter,
                VDist.Distance As Distance
                From PurchInvoice H 
                LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                LEFT JOIN City C On H.VendorCity = C.CityCode
                LEFT JOIN State S On C.State = S.Code
                LEFT JOIN SubGroup Sg On H.Vendor = Sg.SubCode
                LEFT JOIN SubGroup ShipTo On H.ShipToParty = ShipTo.SubCode
                LEFT JOIN City ShipToCity On ShipTo.SubCode = ShipToCity.CityCode
                LEFT JOIN State ShipToState On ShipToCity.CityCode = ShipToState.Code
                LEFT JOIN PurchInvoiceTransport Sit On H.DocId = Sit.DocId
                LEFT JOIN (Select SubCode, Max(Transporter) Transporter 
                            From SubgroupSiteDivisionDetail
                            Group By SubCode) As Hlt On H.Vendor = Hlt.SubCode
                LEFT JOIN SubGroup TSg ON IfNull(Sit.Transporter,Hlt.Transporter) = TSg.SubCode
                LEFT JOIN (Select Subcode, RegistrationNo As SalesTaxNo
                            From SubgroupRegistration 
                            Where RegistrationType = 'Sales Tax No') As VReg On H.Vendor = VReg.SubCode
                LEFT JOIN (Select Subcode, RegistrationNo As SalesTaxNo
                            From SubgroupRegistration 
                            Where RegistrationType = 'Sales Tax No') As VTranReg On TSg.SubCode = VTranReg.SubCode
                LEFT JOIN (Select SubCode, Distance
                            From SubgroupSiteDivisionDetail
                            Where Site_Code = '" & AgL.PubSiteCode & "'
                            And Div_Code = '" & AgL.PubDivCode & "') As VDist On IfNull(H.ShipToParty,H.Vendor) = VDist.SubCode " & mPurchaseReturnCondStr
            DsHeader = AgL.FillData(mQry, AgL.GCn)


            mQry = " SELECT H.DocID, IfNull(I.HSN,Ic.HSN) As HSN
                    FROM SaleInvoice H 
                    LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                    LEFT JOIN SaleInvoiceDetail L ON H.DocID = L.DocID
                    LEFT JOIN Item I ON L.Item = I.Code 
                    LEFT JOIN ItemCategory Ic On I.ItemCategory = Ic.Code
                    " & mSaleCondStr &
                    " And IfNull(I.HSN,Ic.HSN) Is Null "

            mQry = mQry + " UNION ALL "

            mQry = mQry + " SELECT H.DocID, IfNull(I.HSN,Ic.HSN) As HSN
                    FROM PurchInvoice H 
                    LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                    LEFT JOIN PurchInvoiceDetail L ON H.DocID = L.DocID
                    LEFT JOIN Item I ON L.Item = I.Code 
                    LEFT JOIN ItemCategory Ic On I.ItemCategory = Ic.Code
                    " & mSaleCondStr &
                    " And IfNull(I.HSN,Ic.HSN) Is Null "
            Dim DtLine As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

            For I As Integer = 0 To DsHeader.Tables(0).Rows.Count - 1
                If AgL.XNull(DsHeader.Tables(0).Rows(I)("Pincode")) = "" Then
                    If AgL.XNull(DsHeader.Tables(0).Rows(I)("Exception")) <> "" Then DsHeader.Tables(0).Rows(I)("Exception") += vbCrLf
                    DsHeader.Tables(0).Rows(I)("Exception") += "Party Pin Code is blank."
                End If
                If AgL.XNull(DsHeader.Tables(0).Rows(I)("Distance")) = "" Or AgL.VNull(DsHeader.Tables(0).Rows(I)("Distance")) = 0 Then
                    If AgL.XNull(DsHeader.Tables(0).Rows(I)("Exception")) <> "" Then DsHeader.Tables(0).Rows(I)("Exception") += vbCrLf
                    DsHeader.Tables(0).Rows(I)("Exception") += "Party Distance is blank."
                End If
                If AgL.XNull(DsHeader.Tables(0).Rows(I)("Pincode")) <> "" Then
                    If Not System.Text.RegularExpressions.Regex.IsMatch(AgL.XNull(DsHeader.Tables(0).Rows(I)("Pincode")), "^[0-9 ]+$") Then
                        If AgL.XNull(DsHeader.Tables(0).Rows(I)("Exception")) <> "" Then DsHeader.Tables(0).Rows(I)("Exception") += vbCrLf
                        DsHeader.Tables(0).Rows(I)("Exception") += "Party Pin Code is not valid."
                    End If
                End If

                Dim DtRowLineDetail_ForHeader As DataRow() = DtLine.Select(" DocId = " + AgL.Chk_Text(DsHeader.Tables(0).Rows(I)("SearchCode")))
                If DtRowLineDetail_ForHeader.Length > 0 Then
                    For M As Integer = 0 To DtRowLineDetail_ForHeader.Length - 1
                        If AgL.XNull(DtRowLineDetail_ForHeader(M)("HSN")) = "" Then
                            If AgL.XNull(DsHeader.Tables(0).Rows(I)("Exception")) <> "" Then DsHeader.Tables(0).Rows(I)("Exception") += vbCrLf
                            DsHeader.Tables(0).Rows(I)("Exception") += "Some items have blank HSN Codes."
                        End If
                    Next
                End If


                'If AgL.XNull(DsRep.Tables(0).Rows(I)("Exception")) <> "" Then
                '    DsRep.Tables(0).Rows(I)("Tick") = "o"
                'End If
            Next

            If DsHeader.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            mQry = "Select 'Create JSON File' As MenuText, 'FCreateJSONFile' As FunctionName"
            Dim DtMenuList As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)


            ReportFrm.Text = "EWay Bill Report"
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = "ProcEWayBillGeneration"
            ReportFrm.DTCustomMenus = DtMenuList

            ReportFrm.ProcFillGrid(DsHeader)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsHeader = Nothing
        End Try
    End Sub
    Sub FCreateJSONFile(DGL As AgControls.AgDataGrid)
        Dim I As Integer = 0
        Dim mSearchCodeStr As String = ""
        Dim mSaleInvoiceDocPrefix As String
        Dim mPurchInvoiceDocPrefix As String

        For I = 0 To DGL.Rows.Count - 1
            If DGL.Item("Search Code", I).Value IsNot Nothing And DGL.Item("Search Code", I).Value <> "" Then
                If mSearchCodeStr = "" Then
                    mSearchCodeStr = AgL.Chk_Text(DGL.Item("Search Code", I).Value)
                Else
                    mSearchCodeStr = mSearchCodeStr + "," + AgL.Chk_Text(DGL.Item("Search Code", I).Value)
                End If
            End If

            If DGL.Item("Exception", I).Value <> "" Then
                MsgBox("There are some errors found in selected bills, please resolve them.", MsgBoxStyle.Information)
                Exit Sub
            End If
        Next

        mSaleInvoiceDocPrefix = ClsMain.FGetSettings(ClsMain.SettingFields.DocumentPrintEntryNoPrefix, SettingType.General, AgL.PubDivCode, AgL.PubSiteCode, VoucherCategory.Sales, Ncat.SaleInvoice, "", "", "")

        If mSearchCodeStr = "" Then MsgBox("No Records Selected...!", MsgBoxStyle.Information) : Exit Sub

        mQry = "Select H.DocId, 
                '" & IIf(AgL.PubPrintDivisionShortNameOnDocumentsYn, AgL.PubDivShortName, "") & IIf(AgL.PubPrintSiteShortNameOnDocumentsYn, AgL.PubSiteShortName, "") & "' || (Case When VT.Short_Name Is Not Null Then VT.Short_Name Else '' End) || H.ManualRefNo as ManualRefNo, 
                H.V_Date, I.Description As ItemDesc, I.Specification As ItemSpecification, 
                Sg.DispName As SaleToPartyName, 
                S.ManualCode As SaleToPartyStateCode, 
                Case When H.ShipToParty Is Not Null Then ShipTo.Address Else H.SaleToPartyAddress End As SaleToPartyAddress, 
                Case When H.ShipToParty Is Not Null Then ShipTo.Pin Else H.SaleToPartyPinCode End As SaleToPartyPinCode,
                Case When H.ShipToParty Is Not Null Then ShipToState.ManualCode Else S.ManualCode End As ActualStateCode,
                IfNull(VReg.SalesTaxNo,'URP') As SaleToPartySalesTaxNo,  H.Div_Code, H.Site_Code, IfNull(VDist.Distance,0) As transDistance,
                TSg.DispName As TransporterName, VTranReg.SalesTaxNo As TransporterSalesTaxNo,
                Sit.LRNo As TransDocNo, IfNull(Sit.LRDate,H.V_Date) As TransDocDate, VMainHSN.MainHSN,
                Ic.Description As ItemCategoryDesc, I.ManualCode As ItemCode, L.Qty, L.Sr,
                L.Tax1_Per As LineTax1_Per, L.Tax1 As LineTax1, 
                L.Tax2_Per As LineTax2_Per, L.Tax2 As LineTax2, 
                L.Tax3_Per As LineTax3_Per, L.Tax3 As LineTax3, 
                L.Tax4_Per As LineTax4_Per, L.Tax4 As LineTax4, 
                L.Tax5_Per As LineTax5_Per, L.Tax5 As LineTax5, L.Taxable_Amount As LineTaxable_Amount,
                IfNull(I.HSN,Ic.HSN) As HSN, (Case When L.Unit='Meter' Then 'MTR' Else L.Unit End) as Unit, H.Net_Amount As TotalInvoiceValue,
                H.Tax1_Per As HeaderTax1_Per, H.Tax1 As HeaderTax1, 
                H.Tax2_Per As HeaderTax2_Per, H.Tax2 As HeaderTax2, 
                H.Tax3_Per As HeaderTax3_Per, H.Tax3 As HeaderTax3, 
                H.Tax4_Per As HeaderTax4_Per, H.Tax4 As HeaderTax4, 
                H.Tax5_Per As HeaderTax5_Per, H.Tax5 As HeaderTax5, H.Taxable_Amount As HeaderTaxable_Amount, 
                0 As TotNonAdvolVal, 0 As OthValue, 0 As cessNonAdvol, 
                Case When H.ShipToParty Is Not Null Then 2 Else 1 End As TransType
                From SaleInvoice H 
                Left Join Voucher_Type Vt  With (NoLock) On H.V_Type = Vt.V_Type
                LEFT JOIN City C On H.SaleToPartyCity = C.CityCode
                LEFT JOIN State S On C.State = S.Code
                LEFT JOIN SubGroup Sg On H.SaleToParty = Sg.SubCode
                LEFT JOIN SubGroup ShipTo On H.ShipToParty = ShipTo.SubCode
                LEFT JOIN City ShipToCity On ShipTo.CityCode = ShipToCity.CityCode
                LEFT JOIN State ShipToState On ShipToCity.State = ShipToState.Code
                LEFT JOIN SaleInvoiceDetail L On H.DocId = L.DocID
                LEFT JOIN Item I ON L.Item = I.Code
                LEFT JOIN ItemCategory Ic On I.ItemCategory = Ic.Code
                LEFT JOIN SaleInvoiceTransport Sit On H.DocId = Sit.DocId
                LEFT JOIN (Select SubCode, Max(Transporter) Transporter 
                            From SubgroupSiteDivisionDetail
                            Group By SubCode) As Hlt On H.SaleToParty = Hlt.SubCode
                LEFT JOIN SubGroup TSg ON IfNull(Sit.Transporter,Hlt.Transporter) = TSg.SubCode
                LEFT JOIN (Select Subcode, RegistrationNo As SalesTaxNo
                            From SubgroupRegistration 
                            Where RegistrationType = 'Sales Tax No') As VReg On H.SaleToParty = VReg.SubCode
                LEFT JOIN (Select Subcode, RegistrationNo As SalesTaxNo
                            From SubgroupRegistration 
                            Where RegistrationType = 'Sales Tax No') As VTranReg On TSg.SubCode = VTranReg.SubCode
                LEFT JOIN (Select SubCode, Distance
                            From SubgroupSiteDivisionDetail
                            Where Site_Code = '" & AgL.PubSiteCode & "'
                            And Div_Code = '" & AgL.PubDivCode & "' ) As VDist On IfNull(H.ShipToParty,H.SaleToParty) = VDist.SubCode "

        mQry = mQry + " LEFT JOIN (SELECT V1.DocId, Max(V2.HSN) AS MainHSN
                        FROM (SELECT VHSN.DocId, Max(VHSN.CntHSN) AS CntHSN FROM (
		                        Select L.DocID, IfNull(I.HSN,Ic.HSN) As HSN, Count(*) As CntHSN
		                        From SaleInvoiceDetail L
		                        LEFT JOIN Item I On L.Item = I.Code
                                LEFT JOIN ItemCategory Ic On I.ItemCategory = Ic.Code
		                        Where L.DocId In (" & mSearchCodeStr & ")
		                        GROUP By L.DocID, IfNull(I.HSN,Ic.HSN)
	                        ) AS VHSN GROUP BY VHSN.DocId ) AS V1
                        LEFT JOIN (
		                        Select L.DocID, IfNull(I.HSN,Ic.HSN) As HSN, Count(*) As CntHSN
		                        From SaleInvoiceDetail L
		                        LEFT JOIN Item I On L.Item = I.Code
                                LEFT JOIN ItemCategory Ic On I.ItemCategory = Ic.Code
		                        Where L.DocId In (" & mSearchCodeStr & ")
		                        GROUP By L.DocID, IfNull(I.HSN,Ic.HSN)
                        ) AS V2 ON V1.DocId = V2.DocId AND V1.CntHSN = V2.CntHSN
                        GROUP BY V1.DocId ) As VMainHSN On H.DocId = VMainHSN.DocId "

        mQry = mQry + " Where H.DocId In (" & mSearchCodeStr & ")"

        mQry = mQry + " UNION ALL "

        mQry = mQry + " Select H.DocId, 
                '" & IIf(AgL.PubPrintDivisionShortNameOnDocumentsYn, AgL.PubDivShortName, "") & IIf(AgL.PubPrintSiteShortNameOnDocumentsYn, AgL.PubSiteShortName, "") & "' || (Case When VT.Short_Name Is Not Null Then VT.Short_Name Else '' End) || H.ManualRefNo as ManualRefNo, 
                H.V_Date, I.Description As ItemDesc, I.Specification As ItemSpecification, 
                Sg.DispName As SaleToPartyName, 
                S.ManualCode As SaleToPartyStateCode, 
                Case When H.ShipToParty Is Not Null Then ShipTo.Address Else H.VendorAddress End As SaleToPartyAddress, 
                Case When H.ShipToParty Is Not Null Then ShipTo.Pin Else H.VendorPinCode End As SaleToPartyPinCode,
                Case When H.ShipToParty Is Not Null Then ShipToState.ManualCode Else S.ManualCode End As ActualStateCode,
                IfNull(VReg.SalesTaxNo,'URP') As SaleToPartySalesTaxNo,  H.Div_Code, H.Site_Code, IfNull(VDist.Distance,0) As transDistance,
                TSg.DispName As TransporterName, VTranReg.SalesTaxNo As TransporterSalesTaxNo,
                Sit.LRNo As TransDocNo, IfNull(Sit.LRDate,H.V_Date) As TransDocDate, VMainHSN.MainHSN,
                Ic.Description As ItemCategoryDesc, I.ManualCode As ItemCode, L.Qty, L.Sr,
                L.Tax1_Per As LineTax1_Per, L.Tax1 As LineTax1, 
                L.Tax2_Per As LineTax2_Per, L.Tax2 As LineTax2, 
                L.Tax3_Per As LineTax3_Per, L.Tax3 As LineTax3, 
                L.Tax4_Per As LineTax4_Per, L.Tax4 As LineTax4, 
                L.Tax5_Per As LineTax5_Per, L.Tax5 As LineTax5, L.Taxable_Amount As LineTaxable_Amount,
                IfNull(I.HSN,Ic.HSN) As HSN, (Case When L.Unit='Meter' Then 'MTR' Else L.Unit End) as Unit, H.Net_Amount As TotalInvoiceValue,
                H.Tax1_Per As HeaderTax1_Per, H.Tax1 As HeaderTax1, 
                H.Tax2_Per As HeaderTax2_Per, H.Tax2 As HeaderTax2, 
                H.Tax3_Per As HeaderTax3_Per, H.Tax3 As HeaderTax3, 
                H.Tax4_Per As HeaderTax4_Per, H.Tax4 As HeaderTax4, 
                H.Tax5_Per As HeaderTax5_Per, H.Tax5 As HeaderTax5, H.Taxable_Amount As HeaderTaxable_Amount, 
                0 As TotNonAdvolVal, 0 As OthValue, 0 As cessNonAdvol, 
                Case When H.ShipToParty Is Not Null Then 2 Else 1 End As TransType
                From PurchInvoice H 
                Left Join Voucher_Type Vt  With (NoLock) On H.V_Type = Vt.V_Type
                LEFT JOIN City C On H.VendorCity = C.CityCode
                LEFT JOIN State S On C.State = S.Code
                LEFT JOIN SubGroup Sg On H.Vendor = Sg.SubCode
                LEFT JOIN SubGroup ShipTo On H.ShipToParty = ShipTo.SubCode
                LEFT JOIN City ShipToCity On ShipTo.SubCode = ShipToCity.CityCode
                LEFT JOIN State ShipToState On ShipToCity.CityCode = ShipToState.Code
                LEFT JOIN PurchInvoiceDetail L On H.DocId = L.DocID
                LEFT JOIN Item I ON L.Item = I.Code
                LEFT JOIN ItemCategory Ic On I.ItemCategory = Ic.Code
                LEFT JOIN PurchInvoiceTransport Sit On H.DocId = Sit.DocId
                LEFT JOIN (Select SubCode, Max(Transporter) Transporter 
                            From SubgroupSiteDivisionDetail
                            Group By SubCode) As Hlt On H.Vendor = Hlt.SubCode
                LEFT JOIN SubGroup TSg ON IfNull(Sit.Transporter,Hlt.Transporter) = TSg.SubCode
                LEFT JOIN (Select Subcode, RegistrationNo As SalesTaxNo
                            From SubgroupRegistration 
                            Where RegistrationType = 'Sales Tax No') As VReg On H.Vendor = VReg.SubCode
                LEFT JOIN (Select Subcode, RegistrationNo As SalesTaxNo
                            From SubgroupRegistration 
                            Where RegistrationType = 'Sales Tax No') As VTranReg On TSg.SubCode = VTranReg.SubCode
                LEFT JOIN (Select SubCode, Distance
                            From SubgroupSiteDivisionDetail 
                            Where Site_Code = '" & AgL.PubSiteCode & "'
                            And Div_Code = '" & AgL.PubDivCode & "') As VDist On IfNull(H.ShipToParty,H.Vendor) = VDist.SubCode "

        mQry = mQry + " LEFT JOIN (SELECT V1.DocId, Max(V2.HSN) AS MainHSN
                        FROM (SELECT VHSN.DocId, Max(VHSN.CntHSN) AS CntHSN FROM (
		                        Select L.DocID, IfNull(I.HSN,Ic.HSN) As HSN, Count(*) As CntHSN
		                        From PurchInvoiceDetail L
		                        LEFT JOIN Item I On L.Item = I.Code
                                LEFT JOIN ItemCategory Ic On I.ItemCategory = Ic.Code
		                        Where L.DocId In (" & mSearchCodeStr & ")
		                        GROUP By L.DocID, IfNull(I.HSN,Ic.HSN)
	                        ) AS VHSN GROUP BY VHSN.DocId ) AS V1
                        LEFT JOIN (
		                        Select L.DocID, IfNull(I.HSN,Ic.HSN) As HSN, Count(*) As CntHSN
		                        From PurchInvoiceDetail L
		                        LEFT JOIN Item I On L.Item = I.Code
                                LEFT JOIN ItemCategory Ic On I.ItemCategory = Ic.Code
		                        Where L.DocId In (" & mSearchCodeStr & ")
		                        GROUP By L.DocID, IfNull(I.HSN,Ic.HSN)
                        ) AS V2 ON V1.DocId = V2.DocId AND V1.CntHSN = V2.CntHSN
                        GROUP BY V1.DocId ) As VMainHSN On H.DocId = VMainHSN.DocId "

        mQry = mQry + " Where H.DocId In (" & mSearchCodeStr & ")"


        Dim DTInvoiceDetail As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)


        FCreateJSONFile(DTInvoiceDetail)
    End Sub

    Public Shared Sub FCreateJSONFile(DTInvoiceDetail As DataTable)
        Dim mQry As String = ""
        Dim I As Integer = 0
        Dim J As Integer = 0
        Dim K As Integer = 0
        Dim M As Integer = 0


        'mQry = " Select VReg.SalesTaxNo As DivisionSalesTaxNo, Sg.DispName As DivisionName, Sg.Address As DivisionAddress,
        '        Sg.PIN As DivisionPinCode, S.ManualCode As DivisionStateCode
        '        From Division D
        '        LEFT JOIN SubGroup Sg On D.SubCode = Sg.SubCode
        '        LEFT JOIN City C On Sg.CityCode = C.CityCode
        '        LEFT JOIN State S On C.State = S.Code
        '        LEFT JOIN (Select Subcode, RegistrationNo As SalesTaxNo
        '                    From SubgroupRegistration 
        '                    Where RegistrationType = 'Sales Tax No') As VReg On D.SubCode = VReg.SubCode
        '        Where D.Div_Code = '" & DTInvoiceDetail.Rows(0)("Div_Code") & "'"


        mQry = " Select VReg.SalesTaxNo As DivisionSalesTaxNo, Sg.DispName As DivisionName, 
                Case When IsNull(Sm.Add1,'') || IsNull(Sm.Add2,'') || IsNull(Sm.Add3,'') <> ''
                     Then IsNull(Sm.Add1,'') || IsNull(Sm.Add2,'') || IsNull(Sm.Add3,'')
                     Else Sg.Address END As DivisionAddress,
                Case When IsNull(Sm.Add1,'') || IsNull(Sm.Add2,'') || IsNull(Sm.Add3,'') <> ''
                     Then Sm.PinNo Else Sg.PIN END As DivisionPinCode, 
                Case When IsNull(Sm.Add1,'') || IsNull(Sm.Add2,'') || IsNull(Sm.Add3,'') <> ''
                     Then SS.ManualCode Else S.ManualCode END As DivisionStateCode
                From Division D
                LEFT JOIN SubGroup Sg On D.SubCode = Sg.SubCode
                LEFT JOIN City C On Sg.CityCode = C.CityCode
                LEFT JOIN State S On C.State = S.Code
                LEFT JOIN (Select Subcode, RegistrationNo As SalesTaxNo
                            From SubgroupRegistration 
                            Where RegistrationType = 'Sales Tax No') As VReg On D.SubCode = VReg.SubCode
                LEFT JOIN SiteMast Sm ON 1=1
                LEFT JOIN City SC On Sm.City_Code = SC.CityCode
                LEFT JOIN State SS On SC.State = SS.Code
                Where D.Div_Code = '" & AgL.XNull(DTInvoiceDetail.Rows(0)("Div_Code")) & "'
                And Sm.Code = '" & AgL.XNull(DTInvoiceDetail.Rows(0)("Site_Code")) & "'"
        Dim DTDivisionDetail As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

        If AgL.XNull(DTDivisionDetail.Rows(0)("DivisionSalesTaxNo")) = "" Then
            Dim mDivisionSiteSalesTaxNo As String = ClsMain.FGetSettings(ClsMain.SettingFields.DivisionSiteSalesTaxNo,
                                                SettingType.General, AgL.XNull(DTInvoiceDetail.Rows(0)("Div_Code")),
                                                DTInvoiceDetail.Rows(0)("Site_Code"), "", "", "", "", "")
            If mDivisionSiteSalesTaxNo = "" Then
                MsgBox("Company GST No. is blank.", MsgBoxStyle.Information)
                Exit Sub
            Else
                DTDivisionDetail.Rows(0)("DivisionSalesTaxNo") = mDivisionSiteSalesTaxNo
            End If
        End If

        Dim DtInvoice_DocId As DataTable = DTInvoiceDetail.DefaultView.ToTable(True, "DocId")


        Dim FilePath As String = ""
        Dim SaveFileDialogBox As SaveFileDialog
        Dim sFilePath As String = ""
        SaveFileDialogBox = New SaveFileDialog
        SaveFileDialogBox.Title = "File Name"
        FilePath = My.Computer.FileSystem.SpecialDirectories.Desktop
        SaveFileDialogBox.InitialDirectory = FilePath
        SaveFileDialogBox.FilterIndex = 1
        SaveFileDialogBox.FileName = "Ewaybill_" + DTInvoiceDetail.Rows(0)("ManualRefNo") + "_" + CDate(DTInvoiceDetail.Rows(0)("V_Date")).ToString("ddMMyyyy") + ".json"
        If SaveFileDialogBox.ShowDialog = Windows.Forms.DialogResult.Cancel Then Exit Sub
        sFilePath = SaveFileDialogBox.FileName



        Dim fileExists As Boolean = File.Exists(sFilePath)
        If fileExists Then File.Delete(sFilePath)
        Dim StringTabPresses As String = ""
        Using sw As New StreamWriter(File.Open(sFilePath, FileMode.OpenOrCreate))
            sw.WriteLine("{")
            'sw.WriteLine(ControlChars.Tab + """version"": ""1.0.1118"",")
            sw.WriteLine(ControlChars.Tab + """version"": ""1.0.0621"",")
            sw.WriteLine(ControlChars.Tab + """billLists"": [")

            For I = 0 To DtInvoice_DocId.Rows.Count - 1
                Dim DtInvoiceDetail_Filtered As New DataTable
                DtInvoiceDetail_Filtered = DTInvoiceDetail.Clone
                Dim DtInvoiceDetailRows_Filtered As DataRow() = DTInvoiceDetail.Select("DocId = '" & DtInvoice_DocId.Rows(I)("DocId") & "'")
                For M = 0 To DtInvoiceDetailRows_Filtered.Length - 1
                    DtInvoiceDetail_Filtered.ImportRow(DtInvoiceDetailRows_Filtered(M))
                Next


                sw.WriteLine(ControlChars.Tab + ControlChars.Tab + "{")
                sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """userGstin"": """ & AgL.XNull(DTDivisionDetail.Rows(0)("DivisionSalesTaxNo")) & """, ")
                sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """supplyType"": ""O"", ")
                sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """subSupplyType"": 1, ")
                sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """subSupplyDesc"": """", ")
                sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """docType"": ""INV"", ")
                sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """docNo"": """ & FRemoveSpecialCharactersDocNo(AgL.XNull(DtInvoiceDetail_Filtered.Rows(0)("ManualRefNo"))) & """, ")
                sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """docDate"": """ & CDate(DtInvoiceDetail_Filtered.Rows(0)("V_Date")).ToString("dd'/'MM'/'yyyy") & """, ")

                sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """transtype"": """ & FRemoveSpecialCharacters(AgL.XNull(DtInvoiceDetail_Filtered.Rows(0)("transtype"))) & """, ")

                sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """fromGstin"": """ & FRemoveSpecialCharacters(AgL.XNull(DTDivisionDetail.Rows(0)("DivisionSalesTaxNo"))) & """, ")
                sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """fromTrdName"": """ & FRemoveSpecialCharacters(AgL.XNull(DTDivisionDetail.Rows(0)("DivisionName"))) & """, ")
                sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """fromAddr1"": """ & FRemoveSpecialCharacters(AgL.XNull(DTDivisionDetail.Rows(0)("DivisionAddress"))) & """, ")
                sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """fromAddr2"": """", ")
                sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """fromPlace"": """", ")
                sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """fromPincode"": " & FRemoveSpecialCharacters(AgL.XNull(DTDivisionDetail.Rows(0)("DivisionPinCode"))) & ", ")
                sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """fromStateCode"": " & Val(DTDivisionDetail.Rows(0)("DivisionStateCode")) & ", ")
                sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """actualFromStateCode"": " & Val(DTDivisionDetail.Rows(0)("DivisionStateCode")) & ", ")
                sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """toGstin"": """ & FRemoveSpecialCharacters(AgL.XNull(DtInvoiceDetail_Filtered.Rows(0)("SaleToPartySalesTaxNo"))) & """, ")
                sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """toTrdName"": """ & FRemoveSpecialCharacters(AgL.XNull(DtInvoiceDetail_Filtered.Rows(0)("SaleToPartyName"))) & """, ")
                sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """toAddr1"": """ & FRemoveSpecialCharacters(AgL.XNull(DtInvoiceDetail_Filtered.Rows(0)("SaleToPartyAddress"))) & """, ")
                sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """toAddr2"": """", ")
                sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """toPlace"": """", ")
                sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """toPincode"": " & AgL.XNull(DtInvoiceDetail_Filtered.Rows(0)("SaleToPartyPinCode")) & ", ")
                sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """toStateCode"": " & Val(AgL.VNull(DtInvoiceDetail_Filtered.Rows(0)("SaleToPartyStateCode"))) & ", ")
                sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """actualToStateCode"": " & Val(AgL.VNull(DtInvoiceDetail_Filtered.Rows(0)("ActualStateCode"))) & ", ")
                sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """totalValue"": " & Math.Abs(AgL.VNull(DtInvoiceDetail_Filtered.Rows(0)("HeaderTaxable_Amount"))) & ", ")
                sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """cgstValue"": " & Math.Abs(AgL.VNull(DtInvoiceDetail_Filtered.Rows(0)("HeaderTax2"))) & ", ")
                sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """sgstValue"": " & Math.Abs(AgL.VNull(DtInvoiceDetail_Filtered.Rows(0)("HeaderTax3"))) & ", ")
                sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """igstValue"": " & Math.Abs(AgL.VNull(DtInvoiceDetail_Filtered.Rows(0)("HeaderTax1"))) & ", ")
                sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """cessValue"": " & Math.Abs(AgL.VNull(DtInvoiceDetail_Filtered.Rows(0)("HeaderTax4"))) & ", ")

                sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """TotNonAdvolVal"": " & Math.Abs(AgL.VNull(DtInvoiceDetail_Filtered.Rows(0)("TotNonAdvolVal"))) & ", ")
                sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """OthValue"": " & Math.Abs(AgL.VNull(DtInvoiceDetail_Filtered.Rows(0)("OthValue"))) & ", ")

                sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """totInvValue"": " & Math.Abs(AgL.VNull(DtInvoiceDetail_Filtered.Rows(0)("TotalInvoiceValue"))) & ", ")
                sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """transMode"": 1, ")
                sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """transDistance"": " & DtInvoiceDetail_Filtered.Rows(0)("transDistance") & ", ")
                sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """transporterName"": """ & FRemoveSpecialCharacters(AgL.XNull(DtInvoiceDetail_Filtered.Rows(0)("TransporterName"))) & """, ")
                sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """transporterId"": """ & FRemoveSpecialCharacters(AgL.XNull(DtInvoiceDetail_Filtered.Rows(0)("TransporterSalesTaxNo"))) & """, ")
                sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """transDocNo"": """ & FRemoveSpecialCharacters(AgL.XNull(DtInvoiceDetail_Filtered.Rows(0)("TransDocNo"))) & """, ")
                If AgL.XNull(DtInvoiceDetail_Filtered.Rows(0)("TransDocDate")) <> "" Then
                    sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """transDocDate"": """ & CDate(AgL.XNull(DtInvoiceDetail_Filtered.Rows(0)("TransDocDate"))).ToString("dd'/'MM'/'yyyy") & """, ")
                Else
                    sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """transDocDate"": """", ")
                End If
                sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """vehicleNo"": """", ")
                sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """vehicleType"": ""R"",")

                sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """mainHsnCode"": " & AgL.XNull(DtInvoiceDetail_Filtered.Rows(0)("MainHSN")) & ", ")
                sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """itemList"": [")

                For K = 0 To DtInvoiceDetail_Filtered.Rows.Count - 1
                    sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + "{")
                    sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """itemNo"": " & AgL.VNull(DtInvoiceDetail_Filtered.Rows(K)("Sr")) & ", ")
                    sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """productName"": """ & FRemoveSpecialCharacters(AgL.XNull(DtInvoiceDetail_Filtered.Rows(K)("ItemSpecification"))) & """, ")
                    sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """productDesc"": """ & FRemoveSpecialCharacters(AgL.XNull(DtInvoiceDetail_Filtered.Rows(K)("ItemCategoryDesc"))) & """, ")
                    sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """hsnCode"": " & FRemoveSpecialCharacters(AgL.XNull(DtInvoiceDetail_Filtered.Rows(K)("HSN"))) & ", ")
                    sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """quantity"": " & Math.Abs(AgL.VNull(DtInvoiceDetail_Filtered.Rows(K)("Qty"))) & ", ")
                    sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """qtyUnit"": """ & FRemoveSpecialCharacters(AgL.XNull(DtInvoiceDetail_Filtered.Rows(K)("Unit"))) & """, ")
                    sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """taxableAmount"": " & Math.Abs(AgL.VNull(DtInvoiceDetail_Filtered.Rows(K)("LineTaxable_Amount"))) & ", ")
                    sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """sgstRate"": " & AgL.VNull(DtInvoiceDetail_Filtered.Rows(K)("LineTax3_Per")) & ", ")
                    sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """cgstRate"": " & AgL.VNull(DtInvoiceDetail_Filtered.Rows(K)("LineTax2_Per")) & ", ")
                    sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """igstRate"": " & AgL.VNull(DtInvoiceDetail_Filtered.Rows(K)("LineTax1_Per")) & ", ")
                    sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """cessRate"": " & AgL.VNull(DtInvoiceDetail_Filtered.Rows(K)("LineTax4_Per")) & ", ")

                    sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """cessNonAdvol"": " & AgL.VNull(DtInvoiceDetail_Filtered.Rows(K)("cessNonAdvol")) & "")

                    sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + "}" + IIf(K < DtInvoiceDetail_Filtered.Rows.Count - 1, ",", ""))
                Next
                sw.WriteLine(ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + "]")
                sw.WriteLine(ControlChars.Tab + ControlChars.Tab + "}" + IIf(I < DtInvoice_DocId.Rows.Count - 1, ",", ""))
            Next

            sw.WriteLine(ControlChars.Tab + "]")
            sw.WriteLine("}")
        End Using

        MsgBox("File Generated Successfully.", MsgBoxStyle.Information)
    End Sub
    Private Shared Function FRemoveSpecialCharacters(StrValue As String)
        FRemoveSpecialCharacters = StrValue.Replace("~", "").Replace("`", "").Replace("!", "").
            Replace("@", "").Replace("#", "").Replace("$", "").Replace("%", "").Replace("^", "").
            Replace("&", "").Replace("*", "").Replace("(", "").Replace(")", "").Replace("{", "").
            Replace("}", "").Replace("[", "").Replace("]", "").Replace("\", "").Replace(":", "").
            Replace(";", "").Replace("'", "").Replace(",", "").Replace("?", "").Replace("<", "").
            Replace(">", "").Replace("""", "").Replace("_", "").Replace("-", "").Replace("+", "").Replace("=", "").
            Replace(vbCrLf, " ")
    End Function

    Private Shared Function FRemoveSpecialCharactersDocNo(StrValue As String)
        FRemoveSpecialCharactersDocNo = StrValue.Replace("~", "").Replace("`", "").Replace("!", "").
            Replace("@", "").Replace("#", "").Replace("$", "").Replace("%", "").Replace("^", "").
            Replace("&", "").Replace("*", "").Replace("(", "").Replace(")", "").Replace("{", "").
            Replace("}", "").Replace("[", "").Replace("]", "").Replace("\", "").Replace(":", "").
            Replace(";", "").Replace("'", "").Replace(",", "").Replace("?", "").Replace("<", "").
            Replace(">", "").Replace("""", "").Replace("_", "").Replace("+", "").Replace("=", "").
            Replace(vbCrLf, " ")
    End Function
#End Region






#Region "LR Status Change"
    Public Sub ProcLRStatusChange(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try
            Dim mCondStr$ = ""
            Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"

            RepTitle = "Create JSON File"


            If mFilterGrid IsNot Nothing And mGridRow IsNot Nothing Then
                If mGridRow.DataGridView.Columns.Contains("Search Code") = True Then
                    ClsMain.FOpenForm(mGridRow.Cells("Search Code").Value, ReportFrm)
                    ReportFrm.FiterGridCopy_Arr.RemoveAt(ReportFrm.FiterGridCopy_Arr.Count - 1)
                Else
                    Exit Sub
                End If
            End If

            mCondStr = " Where Date(H.V_Date) Between " & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & " And " & AgL.Chk_Date(CDate(ReportFrm.FGetText(1)).ToString("s")) & " "
            mCondStr += " And H.Div_Code = '" & AgL.PubDivCode & "' "
            mCondStr += " And H.Site_Code = '" & AgL.PubSiteCode & "' "


            mQry = " Select 'o' As Tick, H.Code As SearchCode, H.LrNo, H.LrDate, 
                Sg.Name As Transporter
                From LR H
                LEFT JOIN (Select * From LRSiteDetail Where Site_Code = '" & AgL.PubSiteCode & "' 
                                        And Div_Code = '" & AgL.PubDivCode & "') As L On H.Code = L.Code
                LEFT JOIN SubGroup Sg On H.Transporter = Sg.SubCode
                Where L.CurrentGodown = 'Transport' "
            DsHeader = AgL.FillData(mQry, AgL.GCn)

            If DsHeader.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            mQry = "Select 'Save' As MenuText, 'FSaveLRStatus' As FunctionName"
            Dim DtMenuList As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)


            ReportFrm.Text = "LR Status Change"
            ReportFrm.ClsRep = Me
            ReportFrm.IsHideZeroColumns = False
            ReportFrm.IsAllowFind = False
            ReportFrm.ReportProcName = "ProcLRStatusChange"
            ReportFrm.DTCustomMenus = DtMenuList

            If ReportFrm.DGL1.Columns.Contains("Mukhadim") Then ReportFrm.DGL1.Columns.Remove("Mukhadim")
            If ReportFrm.DGL1.Columns.Contains("Remark") Then ReportFrm.DGL1.Columns.Remove("Remark")

            ReportFrm.ProcFillGrid(DsHeader)

            ReportFrm.DGL1.ReadOnly = False

            For I As Integer = 0 To ReportFrm.DGL1.Columns.Count - 1
                ReportFrm.DGL1.Columns(I).ReadOnly = True
            Next


            AgCL.AddAgTextColumn(ReportFrm.DGL1, "Mukhadim", 250, 255, "Mukhadim", True, False)
            ReportFrm.DGL1.AgHelpDataSet("Mukhadim") = AgL.FillData("Select Code, Name From ViewHelpSubgroup", AgL.GCn)
            AgCL.AddAgTextColumn(ReportFrm.DGL1, "Remark", 350, 255, "Remark", True, False)


            AgCL.GridSetiingShowXml(ReportFrm.Text & "-Visible", ReportFrm.DGL1)
            AgCL.GridSetiingShowXml(ReportFrm.Text & "-Visible", ReportFrm.DGL2)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsHeader = Nothing
        End Try
    End Sub
    Sub FSaveLRStatus(DGL As AgControls.AgDataGrid)
        Dim I As Integer = 0
        Dim mSr As Integer = 0
        Dim mSearchCodeStr As String = ""

        For I = 0 To DGL.Rows.Count - 1
            Dim V_Type As String = "LRT"
            Dim V_No As String = ""
            Dim V_Prefix As String = ""
            Dim V_Date As String = ""
            Dim ManualRefNo As String = ""
            'Dim DocID As String = AgL.GetDocId(V_Type, CStr(V_No), CDate(V_Date), AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode)
            Dim DocID As String = AgL.CreateDocId(AgL, "StockHead", V_Type, CStr(V_No), CDate(V_Date), AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode)

            mQry = " INSERT INTO StockHead (DocID, V_Type, V_Prefix, V_Date, V_No, Div_Code, Site_Code, ManualRefNo, 
                    SubCode, Remarks, EntryBy, EntryDate)
                    Select " & AgL.Chk_Text(DocID) & " As DocID, " & AgL.Chk_Text(V_Type) & " As V_Type, " & AgL.Chk_Text(V_Prefix) & " As V_Prefix, 
                    " & AgL.Chk_Text(V_Date) & " As V_Date, " & AgL.Chk_Text(V_No) & " As V_No, " & AgL.Chk_Text(AgL.PubDivCode) & " As Div_Code, 
                    " & AgL.Chk_Text(AgL.PubSiteCode) & " As Site_Code, " & AgL.Chk_Text(ManualRefNo) & " As ManualRefNo, 
                    " & AgL.Chk_Text(DGL.Item("SubCode", I).Value) & " As SubCode, 
                    " & AgL.Chk_Text(DGL.Item("Remark", I).Value) & " As Remarks, 
                    " & AgL.Chk_Text(AgL.PubUserName) & " As EntryBy, 
                    " & AgL.Chk_Text(AgL.PubLoginDate) & " As EntryDate "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

            mSr += 1

            mQry = "Insert Into StockHeadDetail(DocId, Sr, Barcode, Item, Godown, Qty, Unit, Remarks) 
                Select " & AgL.Chk_Text(DocID) & ", " & mSr & ", " &
                " " & AgL.Chk_Text(DGL.Item("Code", I).Tag) & ", " &
                " " & AgL.Chk_Text(ItemCode.Lr) & ", " &
                " " & AgL.Chk_Text(DGL.Item("Remark", I).Value) & ", " &
                " 1 As Qty, " &
                " 'Nos' As Unit, " &
                " " & AgL.Chk_Text(DGL.Item("Remark", I).Value) & " " &
                " ) "
        Next
    End Sub
#End Region



    Public Sub ProcLogReport(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing, Optional bDocId As String = "")
        Try
            Dim mCondStr$ = ""
            Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"
            Dim mTags As String() = Nothing

            RepTitle = "Log Report"

            'To Update LogTable V_Type For ItemMaster
            mQry = "UPDATE LogTable SET V_Type = 'ITEM' WHERE EntryPoint ='Item Master'  AND V_Type IS NULL "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

            If mFilterGrid IsNot Nothing And mGridRow IsNot Nothing Then
                If mGridRow.DataGridView.Columns.Contains("Search Code") = True Then
                    If ReportFrm.DGL1.Columns(ReportFrm.DGL1.CurrentCell.ColumnIndex).Name = "Action" And
                            (mGridRow.Cells("Action").Value = "Edit" Or mGridRow.Cells("Action").Value = "Delete") And
                            AgL.PubServerName <> "" Then
                        Dim FrmObj As New FrmTransactionHistoryTreeView
                        FrmObj.PopulateTreeView(mGridRow.Cells("Search Code").Value, CDate(mGridRow.Cells("Action Date Time").Value))
                        FrmObj.StartPosition = FormStartPosition.CenterParent
                        FrmObj.ShowDialog()
                    Else
                        ClsMain.FOpenForm(mGridRow.Cells("Search Code").Value, ReportFrm)
                        ReportFrm.FiterGridCopy_Arr.RemoveAt(ReportFrm.FiterGridCopy_Arr.Count - 1)
                    End If
                    Exit Sub
                Else
                    Exit Sub
                End If
            End If

            mCondStr = " Where 1=1"

            If bDocId <> "" Then
                mQry = " Select V_Date From LogTable H Where H.DocId = '" & bDocId & "'"
                Dim DtInvoiceDetail As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
                If DtInvoiceDetail.Rows.Count > 0 Then
                    If AgL.XNull(DtInvoiceDetail.Rows(0)("V_Date")) <> "" Then
                        ReportFrm.FilterGrid.Item(GFilter, 0).Value = AgL.RetDate(AgL.XNull(DtInvoiceDetail.Rows(0)("V_Date")))
                        ReportFrm.FilterGrid.Item(GFilter, 1).Value = AgL.RetDate(AgL.XNull(DtInvoiceDetail.Rows(0)("V_Date")))
                        ReportFrm.FilterGrid.Item(GFilter, 2).Value = "Entry Date"
                    End If
                End If
                mCondStr = mCondStr & " AND H.DocId = '" & bDocId & "' "
            Else
                If ReportFrm.FilterGrid.Item(GFilter, 2).Value = "Entry Date" Then
                    mCondStr = mCondStr & " AND Date(H.V_Date) Between " & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & " And " & AgL.Chk_Date(CDate(ReportFrm.FGetText(1)).ToString("s")) & " "
                Else
                    mCondStr = mCondStr & " AND Cast(H.U_EntDt AS DATE) Between " & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & " And " & AgL.Chk_Date(CDate(ReportFrm.FGetText(1)).ToString("s")) & " "
                End If
            End If

            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.V_Type", 3)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.Div_Code", 4).Replace("''", "'")
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.Site_Code", 5).Replace("''", "'")

            If ReportFrm.FilterGrid.Item(GFilter, 6).Value = "Only Add" Then
                mCondStr = mCondStr & " AND H.U_AE ='A' "
            ElseIf ReportFrm.FilterGrid.Item(GFilter, 6).Value = "Only Edit" Then
                mCondStr = mCondStr & " AND H.U_AE ='E' "
            ElseIf ReportFrm.FilterGrid.Item(GFilter, 6).Value = "Only Delete" Then
                mCondStr = mCondStr & " AND H.U_AE ='D' "
            ElseIf ReportFrm.FilterGrid.Item(GFilter, 6).Value = "Only Print" Then
                mCondStr = mCondStr & " AND H.U_AE ='P' "
            End If

            mQry = " SELECT H.DocId As SearchCode, Sm.Name AS SiteName, D.Div_Name AS DivisionName,   
                    IfNull(Vt.Description,H.EntryPoint) AS EntryType, CASE WHEN H.EntryPoint ='Item Master' THEN I.Description WHEN H.EntryPoint ='Sales Entry' THEN SI.ManualRefNo ELSE H.ManualRefNo END AS ManualRefNo, H.V_Date AS EntryDate, Sg.Name AS PartyName, 
                    H.MachineName, H.U_Name As UserName, H.U_EntDt As ActionDateTime,
                    CASE WHEN H.U_AE = 'A' THEN 'Add'
	                     WHEN H.U_AE = 'E' THEN 'Edit'
	                     WHEN H.U_AE = 'D' THEN 'Delete'
	                     WHEN H.U_AE = 'P' THEN 'Print' END AS Action, H.Modifications
                    FROM LogTable H
                    LEFT JOIN Item I ON I.Code = H.DocId 
                    LEFT JOIN SaleInvoice  SI ON SI.DocId = H.DocId 
                    LEFT JOIN ViewHelpSubgroup Sg ON H.SubCode = Sg.Code  
                    LEFT JOIN SiteMast Sm ON H.Site_Code = Sm.Code
                    LEFT JOIN Division D ON H.Div_Code = D.Div_Code
                    LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type " & mCondStr &
                    " Order By H.U_EntDt Desc "
            DsHeader = AgL.FillData(mQry, AgL.GCn)

            If DsHeader.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            mQry = "Select 'Clear Log' As MenuText, 'FClearLog' As FunctionName "
            Dim DtMenuList As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

            ReportFrm.Text = "Log Report"
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = "ProcLogReport"
            ReportFrm.DTCustomMenus = DtMenuList

            ReportFrm.ProcFillGrid(DsHeader)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsHeader = Nothing
        End Try
    End Sub
    Sub FClearLog(DGL As AgControls.AgDataGrid)
        Dim I As Integer = 0
        Dim bDocIdStr As String = ""

        If Not AgL.StrCmp(AgL.PubUserName, "sa") And Not AgL.StrCmp(AgL.PubUserName, "Super") Then
            MsgBox("Only System Administrator can perform this task...!", MsgBoxStyle.Information)
            Exit Sub
        End If

        If MsgBox("Are you sure you want to clear log ?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            For I = 0 To DGL.Rows.Count - 1
                If bDocIdStr <> "" Then bDocIdStr = bDocIdStr + ","
                bDocIdStr = bDocIdStr + AgL.Chk_Text(DGL.Item("Search Code", I).Value)
            Next

            mQry = "Delete From LogTable Where DocId In (" & bDocIdStr & ")"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

            MsgBox("Log cleared successfully...!", MsgBoxStyle.Information)
            ReportFrm.DGL1.DataSource = Nothing
        End If
    End Sub
#Region "Ledger Posting Difference"
    Public Sub ProcLedgerPostingDifference(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try
            Dim mCondStr$ = ""
            Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"


            If mFilterGrid IsNot Nothing And mGridRow IsNot Nothing Then
                If mGridRow.DataGridView.Columns.Contains("Search Code") = True Then
                    ClsMain.FOpenForm(mGridRow.Cells("Search Code").Value, ReportFrm)
                    ReportFrm.FiterGridCopy_Arr.RemoveAt(ReportFrm.FiterGridCopy_Arr.Count - 1)
                Else
                    Exit Sub
                End If
            End If

            mCondStr = " Where Date(H.V_Date) Between " & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & " And " & AgL.Chk_Date(CDate(ReportFrm.FGetText(1)).ToString("s")) & " "
            mCondStr += " And H.Div_Code = '" & AgL.PubDivCode & "' "
            mCondStr += " And H.Site_Code = '" & AgL.PubSiteCode & "' "


            mQry = " SELECT H.DocId As SearchCode, Sm.Name AS Site, D.Div_Name AS Division, Vt.Description AS EntryType, H.ManualRefNo AS EntryNo, H.V_Date AS EntryDate, Sg.Name AS PartyName, H.Net_Amount AS TransactionAmount, L.AmtDr AS LedgerAmount 
                        FROM SaleInvoice H 
                        LEFT JOIN Ledger L ON H.DocID = L.DocId AND H.BillToParty = L.SubCode
                        LEFT JOIN Subgroup Sg ON H.BillToParty = Sg.Subcode
                        LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type
                        LEFT JOIN SiteMast Sm ON H.Site_Code = Sm.Code
                        LEFT JOIN Division D ON H.Div_Code = D.Div_Code
                        WHERE Vt.NCat = '" & Ncat.SaleInvoice & "'
                        AND IsNull(H.Net_Amount,0) <> IsNull(L.AmtDr,0)

                        UNION ALL 

                        SELECT H.DocId As SearchCode, Sm.Name AS Site, D.Div_Name AS Division, Vt.Description AS EntryType, H.ManualRefNo AS EntryNo, H.V_Date AS EntryDate, Sg.Name AS PartyName, H.Net_Amount AS TransactionAmount, Abs(L.AmtCr) AS LedgerAmount 
                        FROM SaleInvoice H 
                        LEFT JOIN Ledger L ON H.DocID = L.DocId AND H.BillToParty = L.SubCode
                        LEFT JOIN Subgroup Sg ON H.BillToParty = Sg.Subcode
                        LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type
                        LEFT JOIN SiteMast Sm ON H.Site_Code = Sm.Code
                        LEFT JOIN Division D ON H.Div_Code = D.Div_Code
                        WHERE Vt.NCat = '" & Ncat.SaleReturn & "'
                        AND IsNull(Abs(H.Net_Amount),0) <> IsNull(L.AmtCr,0)

                        UNION ALL 

                        SELECT H.DocId As SearchCode, Sm.Name AS Site, D.Div_Name AS Division, Vt.Description AS EntryType, H.ManualRefNo AS EntryNo, H.V_Date AS EntryDate, Sg.Name AS PartyName, H.Net_Amount AS TransactionAmount, L.AmtCr AS LedgerAmount 
                        FROM PurchInvoice H 
                        LEFT JOIN Ledger L ON H.DocID = L.DocId AND H.BillToParty = L.SubCode
                        LEFT JOIN Subgroup Sg ON H.BillToParty = Sg.Subcode
                        LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type
                        LEFT JOIN SiteMast Sm ON H.Site_Code = Sm.Code
                        LEFT JOIN Division D ON H.Div_Code = D.Div_Code
                        WHERE Vt.NCat = '" & Ncat.PurchaseInvoice & "'
                        AND IsNull(Abs(H.Net_Amount),0) <> IsNull(L.AmtCr,0)

                        UNION ALL 

                        SELECT H.DocId As SearchCode, Sm.Name AS Site, D.Div_Name AS Division, Vt.Description AS EntryType, H.ManualRefNo AS EntryNo, H.V_Date AS EntryDate, Sg.Name AS PartyName, H.Net_Amount AS TransactionAmount, Abs(L.AmtDr) AS LedgerAmount 
                        FROM PurchInvoice H 
                        LEFT JOIN Ledger L ON H.DocID = L.DocId AND H.BillToParty = L.SubCode
                        LEFT JOIN Subgroup Sg ON H.BillToParty = Sg.Subcode
                        LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type
                        LEFT JOIN SiteMast Sm ON H.Site_Code = Sm.Code
                        LEFT JOIN Division D ON H.Div_Code = D.Div_Code
                        WHERE Vt.NCat = '" & Ncat.PurchaseReturn & "'
                        AND IsNull(Abs(H.Net_Amount),0) <> IsNull(L.AmtDr,0)

                        UNION ALL 

                        SELECT H.DocId As SearchCode, Sm.Name AS Site, D.Div_Name AS Division, Vt.Description AS EntryType, H.ManualRefNo AS EntryNo, H.V_Date AS EntryDate, Sg.Name AS PartyName, Hc.Net_Amount AS TransactionAmount, Abs(L.AmtDr) AS LedgerAmount 
                        FROM LedgerHead H 
                        LEFT JOIN LedgerHeadCharges Hc ON H.DocID = Hc.DocID
                        LEFT JOIN Ledger L ON H.DocID = L.DocId AND H.Subcode = L.SubCode
                        LEFT JOIN Subgroup Sg ON H.Subcode = Sg.Subcode
                        LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type
                        LEFT JOIN SiteMast Sm ON H.Site_Code = Sm.Code
                        LEFT JOIN Division D ON H.Div_Code = D.Div_Code
                        WHERE Vt.NCat = '" & Ncat.VisitReceipt & "'
                        AND IsNull(Abs(HC.Net_Amount),0) <> IsNull(L.AmtDr,0) "
            DsHeader = AgL.FillData(mQry, AgL.GCn)

            If DsHeader.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            ReportFrm.Text = "Ledger Posting Difference"
            ReportFrm.ClsRep = Me
            ReportFrm.IsHideZeroColumns = False
            ReportFrm.IsAllowFind = False
            ReportFrm.ReportProcName = "ProcLedgerPostingDifference"

            ReportFrm.ProcFillGrid(DsHeader)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsHeader = Nothing
        End Try
    End Sub

#End Region



#Region "Cheque Searching"
    Public Sub ProcChequeSearching(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try
            Dim mCondStr$ = ""
            Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"

            If ReportFrm.FGetText(0) = "" Then
                MsgBox("Cheque No. is required.", MsgBoxStyle.Information)
                Exit Sub
            End If

            If mFilterGrid IsNot Nothing And mGridRow IsNot Nothing Then
                If mGridRow.DataGridView.Columns.Contains("Search Code") = True Then
                    ClsMain.FOpenForm(mGridRow.Cells("Search Code").Value, ReportFrm)
                    ReportFrm.FiterGridCopy_Arr.RemoveAt(ReportFrm.FiterGridCopy_Arr.Count - 1)
                Else
                    Exit Sub
                End If
            End If

            mCondStr = " And L.Chq_No Like  '%" & ReportFrm.FGetText(0) & "%' "

            mQry = "SELECT L.DocId As SearchCode, Vt.Description AS EntryType, L.V_Date AS EntryDate,  SG.Name, L.AmtDr AS Amount, 
                    L.Chq_Date AS ChequeDate, L.Narration
                    FROM Ledger L 
                    LEFT JOIN Subgroup SG ON L.SubCode = Sg.Subcode
                    LEFT JOIN Voucher_Type Vt ON L.V_Type = Vt.V_Type
                    WHERE 1=1 " & mCondStr
            DsHeader = AgL.FillData(mQry, AgL.GCn)

            If DsHeader.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            ReportFrm.Text = "Cheque Searching"
            ReportFrm.ClsRep = Me
            ReportFrm.IsHideZeroColumns = False
            ReportFrm.IsAllowFind = False
            ReportFrm.ReportProcName = "ProcChequeSearching"

            ReportFrm.ProcFillGrid(DsHeader)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsHeader = Nothing
        End Try
    End Sub

#End Region
End Class
