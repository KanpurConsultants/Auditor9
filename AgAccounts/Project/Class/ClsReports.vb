
Imports AgLibrary

Public Class ClsReports

#Region "Danger Zone"
    Dim StrArr1() As String = Nothing, StrArr2() As String = Nothing, StrArr3() As String = Nothing, StrArr4() As String = Nothing, StrArr5() As String = Nothing

    Dim mGRepFormName As String = ""

    Dim WithEvents ReportFrm As FrmRepDisplay
    Public Const GField As Byte = 0
    Public Const GFilter As Byte = 2
    Public Const GFilterCode As Byte = 4
    Dim StrSQLQuery As String = ""

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
    Private Const DailyTransactionSummary As String = "DailyTransactionSummary"
    Private Const MonthlyLedgerSummaryFull As String = "MonthlyLedgerSummaryFull"
    Private Const TrialDetailDrCr As String = "TrialDetailDrCr"
    Private Const MonthlyLedgerSummary As String = "MonthlyLedgerSummary"
    Private Const InterestLedger As String = "InterestLedger"
    Private Const FBTReport As String = "FBTReport"
    Private Const PartyWiseTDSReport As String = "PartyWiseTDSReport"
    Private Const TDSCategoryWiseReport As String = "TDSCategoryWiseReport"
    Private Const FixedAssetRegister As String = "FixedAssetRegister"
    Private Const Ledger As String = "Ledger"
    Private Const TrialGroup As String = "TrialGroup"
    Private Const TrialDetail As String = "TrialDetail"
    Private Const CashBook As String = "CashBook"
    Private Const BankBook As String = "BankBook"
    Private Const Annexure As String = "Annexure"
    Private Const Journal As String = "Journal"
    Private Const DayBook As String = "DayBook"
    Private Const Ageing As String = "Ageing"
    Private Const BillWisesOutStandingAgeing As String = "BillWisesOutStandingAgeing"
    Private Const BillWiseOutStanding_Debtors As String = "BillWiseOutStanding_Debtors"
    Private Const BillWiseOutStanding_Creditors As String = "BillWiseOutStanding_Creditors"
    Private Const CashFlow As String = "CashFlow"
    Private Const FundFlow As String = "FundFlow"
    Private Const MonthlyExpenses As String = "MonthlyExpenses"
    Private Const FIFOOutStanding_Debtors As String = "FIFOOutstandingDebtors"
    Private Const FIFOOutStanding_Creditors As String = "FIFOOutStandingCreditors"
    Private Const Stock_Valuation As String = "Stock_Valuation"
    Private Const DailyExpenseRegister As String = "DailyExpenseRegister"
    Private Const DailyCollectionRegister As String = "DailyCollectionRegister"
    Private Const LedgerGrMergeLedger As String = "LedgerGrMergeLedger"
    Private Const AccountGrMergeLedger As String = "AccountGrMergeLedger"
    Private Const GTAReg As String = "GTAReg"
    Private Const BillWiseAdj As String = "BillWiseAdj"
    Private Const TDSTaxChallan As String = "TDSTaxChallan"
    Private Const AccountGrpWsOSAgeing As String = "AccountGrpWsOSAgeing"
    Private Const IntCalForDebtors As String = "IntCalForDebtors"
#End Region

#Region "Queries Definition"
    Dim mHelpCityQry$ = "Select 'o' As Tick, CityCode, CityName From City "
    Dim mHelpStateQry$ = "Select 'o' As Tick, State_Code, State_Desc From State "
    Dim mHelpUserQry$ = "Select 'o' As Tick, User_Name As Code, User_Name As [User] From UserMast "
    Dim mHelpSiteQry$ = "Select 'o' As Tick, Code, Name As [Site] From SiteMast Where " & AgL.PubSiteCondition("Code", AgL.PubSiteCode) & " "
    Dim mHelpDivisionQry$ = "Select 'o' As Tick, Div_Code as Code, Div_Name As [Division] From Division Where Div_Code In (" & AgL.PubDivisionList & ") "
    Dim mHelpItemQry$ = "Select 'o' As Tick, Code, Description As [Item] From Item "
    Dim mHelpItemGroupQry$ = "Select 'o' As Tick, Code, Description As [Item Group] From ItemGroup "
    Dim mHelpItemCategoryQry$ = "Select 'o' As Tick, Code, Description As [Item Category] From ItemCategory "
    Dim mHelpItemTypeQry$ = "Select 'o' As Tick, Code, Description As [Item Type] From ItemType "
    Dim mHelpVendorQry$ = " Select 'o' As Tick,  H.SubCode As Code, Sg.DispName AS Vendor FROM Vendor H LEFT JOIN SubGroup Sg ON H.SubCode = Sg.SubCode "
    Dim mHelpTableQry$ = "Select 'o' As Tick, H.Code, H.Description AS [Table] FROM HT_Table H "
    Dim mHelpOutletQry$ = "Select 'o' As Tick, H.Code, H.Description AS [Table] FROM Outlet H "
    Dim mHelpStewardQry$ = "Select 'o' As Tick,  Sg.SubCode AS Code, Sg.DispName AS Steward FROM SubGroup Sg  "
    Dim mHelpPartyQry$ = " Select 'o' As Tick,  Sg.SubCode As Code, Sg.DispName AS Party FROM SubGroup Sg Where Sg.Nature In ('Customer','Supplier','Cash') "
    Dim mHelpPartySingleQry$ = " Select Sg.Code, Sg.Name AS Party FROM viewHelpSubGroup Sg Where Sg.Nature In ('Customer','Supplier','Cash') "
    Dim mHelpSaleOrderQry$ = " Select 'o' As Tick,  H.DocID AS Code, H.V_Type || '-' || H.ReferenceNo  FROM SaleOrder H "
    Dim mHelpSaleBillQry$ = " SELECT 'o' As Tick,DocId, ReferenceNo AS BillNo, V_Date AS Date FROM SaleChallan "
    Dim mHelpItemReportingGroupQry$ = "Select 'o' As Tick,I.Code,I.Description  AS ItemReportingGroup FROM ItemReportingGroup I "
    Dim mHelpVoucherTypeQry$ = "Select 'o' As Tick,VT.V_Type,VT.Description From Voucher_Type VT  Where VT.V_Type in ( Select DISTINCT V_Type from Ledger) Order by VT.Description"
    Dim mHelpAccountQry$ = "Select 'o' As Tick,SG.SubCode,SG.Name From SubGroup SG  Where (SG.SiteList Like '%|" & AgL.PubSiteCode & "|%' Or SG.SiteList Is Null) Order by SG.Name"
    Dim mHelpAccountGroupQry$ = "Select 'o' As Tick,AG.GroupCode,AG.GroupName From AcGroup  AG Order By AG.GroupName"
    Dim mHelpAccountTypeQry$ = "Select ag.nature as code,AG.nature as Name From acgroup AG group by ag.nature having ag.nature in('Customer','Supplier') Order By AG.Nature"
    Dim mHelpGroupNatureQry$ = "Select 'o' As Tick,AG.groupcode,AG.GroupName as Name, " &
                          "(Case When GroupNature='L' Then 'Liabilities' " &
                          "When GroupNature='A' Then 'Assets' " &
                          "When GroupNature='E' Then 'Revenue' " &
                          "When GroupNature='R' Then 'Expenses' End) MainGroup " &
                          "From acgroup AG Order By AG.GroupName"
    Dim mHelpAreaQry$ = "Select 'o' As Tick,Zm.Code,Zm.Description as Name From Area Zm  Order By Zm.Description"
    Dim mHelpYesNoQry$ = "Select 'Y' as Code, 'Yes' as Name Union All Select 'N' as Code, 'No' as Name"
    Dim mHelpTdsCategoryQry$ = "Select 'o' As Tick,Code,Name From TdsCat Order By Name"
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
            Select Case GRepFormName
                Case DailyTransactionSummary
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubEndDate)
                    ReportFrm.CreateHelpGrid("Site", "Site Name", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSiteQry)
                    ReportFrm.CreateHelpGrid("Division", "Division Name", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpDivisionQry)
                    ReportFrm.CreateHelpGrid("Account", "Account Name", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpAccountQry)
                    ReportFrm.CreateHelpGrid("V_Type", "Voucher Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpVoucherTypeQry)

                Case MonthlyLedgerSummaryFull
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubEndDate)
                    ReportFrm.CreateHelpGrid("Account", "Account Name", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpAccountQry)
                    ReportFrm.CreateHelpGrid("Site", "Site Name", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSiteQry)

                Case TrialDetailDrCr
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubEndDate)
                    ReportFrm.CreateHelpGrid("Account Gorup", "Account Name", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpAccountGroupQry)
                    ReportFrm.CreateHelpGrid("Account", "Account Name", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpAccountQry)
                    ReportFrm.CreateHelpGrid("Site", "Site Name", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSiteQry)
                    ReportFrm.CreateHelpGrid("Division", "Division Name", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpDivisionQry)

                Case MonthlyLedgerSummary
                    mQry = "Select 'F' as Code, 'First Six Month' as Name 
                            Union All Select 'L' as Code, 'Last Six Month' as Name "
                    ReportFrm.CreateHelpGrid("Month", "Month", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mQry, "Last Six Month")
                    ReportFrm.CreateHelpGrid("Account Gorup", "Account Name", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpAccountGroupQry)
                    ReportFrm.CreateHelpGrid("Site", "Site Name", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSiteQry)

                Case InterestLedger
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubEndDate)
                    ReportFrm.CreateHelpGrid("InterestRateDr", "Interest Rate (Dr.)", FrmRepDisplay.FieldFilterDataType.NumericType, FrmRepDisplay.FieldDataType.NumericType, Nothing, "1")
                    ReportFrm.CreateHelpGrid("InterestRateCr", "Interest Rate (Cr)", FrmRepDisplay.FieldFilterDataType.NumericType, FrmRepDisplay.FieldDataType.NumericType, Nothing, "1")
                    ReportFrm.CreateHelpGrid("Days", "Days", FrmRepDisplay.FieldFilterDataType.NumericType, FrmRepDisplay.FieldDataType.NumericType, Nothing, "365")
                    ReportFrm.CreateHelpGrid("Account", "Account Name", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpAccountQry)
                    ReportFrm.CreateHelpGrid("Site", "Site Name", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSiteQry)

                Case FBTReport
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubEndDate)
                    ReportFrm.CreateHelpGrid("Account", "Account Name", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpAccountQry)
                    ReportFrm.CreateHelpGrid("WithOpening", "With Opening", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mHelpYesNoQry, "Yes")
                    ReportFrm.CreateHelpGrid("Site", "Site Name", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSiteQry)

                Case PartyWiseTDSReport

                Case TDSCategoryWiseReport

                Case FixedAssetRegister

                Case Ledger
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubEndDate)
                    ReportFrm.CreateHelpGrid("Account Gorup", "Account Name", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpAccountGroupQry)
                    ReportFrm.CreateHelpGrid("Account", "Account Name", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpAccountQry)
                    ReportFrm.CreateHelpGrid("V_Type", "Voucher Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpVoucherTypeQry)
                    ReportFrm.CreateHelpGrid("Site", "Site Name", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSiteQry)
                    ReportFrm.CreateHelpGrid("Division", "Division Name", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpDivisionQry)
                    ReportFrm.CreateHelpGrid("IndexNeeded", "Index Needed", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mHelpYesNoQry, "No")
                    ReportFrm.CreateHelpGrid("ContraACNeeded", "Contra A/C Needed", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mHelpYesNoQry, "No")

                Case TrialGroup
                    ReportFrm.CreateHelpGrid("AsOnDate", "As On Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubLoginDate)
                    ReportFrm.CreateHelpGrid("Site", "Site Name", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSiteQry)
                    ReportFrm.CreateHelpGrid("Division", "Division Name", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpDivisionQry)

                Case TrialDetail
                    ReportFrm.CreateHelpGrid("AsOnDate", "As On Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubLoginDate)
                    ReportFrm.CreateHelpGrid("Site", "Site Name", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSiteQry)
                    ReportFrm.CreateHelpGrid("Division", "Division Name", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpDivisionQry)
                    mQry = "Select 'A' as Code, 'Alphabatical' as Name Union All Select 'M' as Code, 'Manual' as Name "
                    ReportFrm.CreateHelpGrid("Positioning", "Positioning", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mQry, "Alphabatical")
                    ReportFrm.CreateHelpGrid("ShowZeroValue", "Show Zero Value", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mHelpYesNoQry, "No")

                Case CashBook
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubEndDate)
                    ReportFrm.CreateHelpGrid("Site", "Site Name", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSiteQry)
                    ReportFrm.CreateHelpGrid("Division", "Division Name", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpDivisionQry)
                    ReportFrm.CreateHelpGrid("Account", "Account Name", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpAccountQry)
                    ReportFrm.CreateHelpGrid("PageWise", "Page Wise", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mHelpYesNoQry, "No")
                    ReportFrm.CreateHelpGrid("WithNarration", "With Narration", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mHelpYesNoQry, "No")
                    mQry = "Select 'S' as Code, 'Single' as Name Union All Select 'D' as Code, 'Double' as Name Union All Select 'J' as Code, 'Journal' as Name "
                    ReportFrm.CreateHelpGrid("ReportType", "Report Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mQry, "S")

                Case BankBook
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubEndDate)
                    ReportFrm.CreateHelpGrid("Site", "Site Name", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSiteQry)
                    ReportFrm.CreateHelpGrid("Division", "Division Name", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpDivisionQry)
                    ReportFrm.CreateHelpGrid("Account", "Account Name", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpAccountQry)
                    ReportFrm.CreateHelpGrid("PageWise", "Page Wise", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mHelpYesNoQry, "No")
                    ReportFrm.CreateHelpGrid("WithNarration", "With Narration", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mHelpYesNoQry, "No")
                    mQry = "Select 'S' as Code, 'Single' as Name Union All Select 'D' as Code, 'Double' as Name Union All Select 'J' as Code, 'Journal' as Name "
                    ReportFrm.CreateHelpGrid("ReportType", "Report Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mQry, "Single")


                Case Annexure
                    ReportFrm.CreateHelpGrid("UpToDate", "Up To Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubLoginDate)
                    ReportFrm.CreateHelpGrid("GroupNature", "Group Nature", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpGroupNatureQry)
                    ReportFrm.CreateHelpGrid("Site", "Site Name", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSiteQry)
                    ReportFrm.CreateHelpGrid("Division", "Division Name", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpDivisionQry)

                Case Journal
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubEndDate)
                    ReportFrm.CreateHelpGrid("VoucherType", "Voucher Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpVoucherTypeQry)
                    ReportFrm.CreateHelpGrid("Site", "Site Name", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSiteQry)
                    ReportFrm.CreateHelpGrid("Division", "Division Name", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpDivisionQry)


                Case DayBook
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubEndDate)
                    ReportFrm.CreateHelpGrid("VoucherType", "Voucher Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpVoucherTypeQry)
                    ReportFrm.CreateHelpGrid("Site", "Site Name", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSiteQry)
                    ReportFrm.CreateHelpGrid("Division", "Division Name", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpDivisionQry)


                Case Ageing
                    ReportFrm.CreateHelpGrid("UpToDate", "Up To Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubLoginDate)
                    ReportFrm.CreateHelpGrid("AccountType", "Account Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpAccountTypeQry)
                    ReportFrm.CreateHelpGrid("Site", "Site Name", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSiteQry)
                    ReportFrm.CreateHelpGrid("IInterval", "I Interval", FrmRepDisplay.FieldFilterDataType.NumericType, FrmRepDisplay.FieldDataType.NumericType, Nothing, "5")
                    ReportFrm.CreateHelpGrid("IIInterval", "II Interval", FrmRepDisplay.FieldFilterDataType.NumericType, FrmRepDisplay.FieldDataType.NumericType, Nothing, "10")
                    ReportFrm.CreateHelpGrid("IIIInterval", "III Interval", FrmRepDisplay.FieldFilterDataType.NumericType, FrmRepDisplay.FieldDataType.NumericType, Nothing, "15")
                    ReportFrm.CreateHelpGrid("IVInterval", "IV Interval", FrmRepDisplay.FieldFilterDataType.NumericType, FrmRepDisplay.FieldDataType.NumericType, Nothing, "20")
                    ReportFrm.CreateHelpGrid("VInterval", "V Interval", FrmRepDisplay.FieldFilterDataType.NumericType, FrmRepDisplay.FieldDataType.NumericType, Nothing, "25")
                    ReportFrm.CreateHelpGrid("VIInterval", "VI Interval", FrmRepDisplay.FieldFilterDataType.NumericType, FrmRepDisplay.FieldDataType.NumericType, Nothing, "30")

                    mQry = "Select 'A' as Code, 'All' as Name Union All Select 'HB' as Code, 'Having Balance' as Name "
                    ReportFrm.CreateHelpGrid("ShowRecords", "Show Records", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mQry, "All")
                    mQry = "Select 'AG' as Code, 'Account Group Wise' as Name Union All Select 'AC' as Code, 'Account Name Wise' as Name "
                    ReportFrm.CreateHelpGrid("ReportOnChoice", "Report On Choice", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mQry, "Account Group Wise")

                Case BillWisesOutStandingAgeing
                    ReportFrm.CreateHelpGrid("AsOnDate", "As On Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubLoginDate)
                    ReportFrm.CreateHelpGrid("AccountGroup", "Account Group", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpAccountGroupQry)
                    ReportFrm.CreateHelpGrid("Account", "Account Name", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpAccountQry)
                    ReportFrm.CreateHelpGrid("Interval", "Interval", FrmRepDisplay.FieldFilterDataType.NumericType, FrmRepDisplay.FieldDataType.NumericType, Nothing, "180")
                    ReportFrm.CreateHelpGrid("Site", "Site Name", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSiteQry)

                Case BillWiseOutStanding_Debtors
                    ReportFrm.CreateHelpGrid("AsOnDate", "As On Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubLoginDate)
                    ReportFrm.CreateHelpGrid("AccountGroup", "Account Group", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpAccountGroupQry)
                    ReportFrm.CreateHelpGrid("Account", "Account Name", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpAccountQry)
                    mQry = "Select 'D' as Code, 'Detail' as Name Union All Select 'S' as Code, 'Summary' as Name "
                    ReportFrm.CreateHelpGrid("ReportOnChoice", "Report On Choice", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mQry, "Details")
                    ReportFrm.CreateHelpGrid("Site", "Site Name", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSiteQry)

                Case BillWiseOutStanding_Creditors
                    ReportFrm.CreateHelpGrid("AsOnDate", "As On Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubLoginDate)
                    ReportFrm.CreateHelpGrid("AccountGroup", "Account Group", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpAccountGroupQry)
                    ReportFrm.CreateHelpGrid("Account", "Account Name", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpAccountQry)
                    mQry = "Select 'D' as Code, 'Detail' as Name Union All Select 'S' as Code, 'Summary' as Name "
                    ReportFrm.CreateHelpGrid("ReportOnChoice", "Report On Choice", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mQry, "Details")
                    ReportFrm.CreateHelpGrid("Site", "Site Name", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSiteQry)

                Case CashFlow, FundFlow
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubEndDate)
                    ReportFrm.CreateHelpGrid("Site", "Site Name", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSiteQry)

                Case MonthlyExpenses
                    ReportFrm.CreateHelpGrid("Month", "Month", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpAccountGroupQry)
                    ReportFrm.CreateHelpGrid("Expense", "Expense", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpAccountQry)
                    ReportFrm.CreateHelpGrid("Account", "Account Name", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpAccountQry)
                    ReportFrm.CreateHelpGrid("Site", "Site Name", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSiteQry)
                    ReportFrm.CreateHelpGrid("Division", "DivisionName", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpDivisionQry)

                Case FIFOOutStanding_Debtors
                    ReportFrm.CreateHelpGrid("As On Date", "AsOnDate", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubLoginDate)
                    ReportFrm.CreateHelpGrid("Account Gorup", "Account Name", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpAccountGroupQry)
                    ReportFrm.CreateHelpGrid("Account", "Account Name", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpAccountQry)
                    ReportFrm.CreateHelpGrid("Interval", "Interval", FrmRepDisplay.FieldFilterDataType.NumericType, FrmRepDisplay.FieldDataType.NumericType, Nothing)
                    ReportFrm.CreateHelpGrid("Site", "Site Name", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSiteQry)

                Case FIFOOutStanding_Creditors
                    ReportFrm.CreateHelpGrid("As On Date", "AsOnDate", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubLoginDate)
                    ReportFrm.CreateHelpGrid("Account Gorup", "Account Name", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpAccountGroupQry)
                    ReportFrm.CreateHelpGrid("Account", "Account Name", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpAccountQry)
                    ReportFrm.CreateHelpGrid("Interval", "Interval", FrmRepDisplay.FieldFilterDataType.NumericType, FrmRepDisplay.FieldDataType.NumericType, Nothing)
                    ReportFrm.CreateHelpGrid("Site", "Site Name", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSiteQry)

                Case Stock_Valuation
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubEndDate)
                    ReportFrm.CreateHelpGrid("ItemType", "Item Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpItemTypeQry)
                    ReportFrm.CreateHelpGrid("ItemCategory", "Item Category", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpItemCategoryQry)
                    ReportFrm.CreateHelpGrid("ItemGroup", "Item Group", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpItemGroupQry)
                    ReportFrm.CreateHelpGrid("Item", "Item", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpItemQry)
                    mQry = "Select 'D' as Code, 'Detail' as Name Union All Select 'S' as Code, 'Summary' as Name "
                    ReportFrm.CreateHelpGrid("ReportOnChoice", "Detail / Summary", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mQry, "Detail")
                    mQry = "Select 'WA' as Code, 'Weightage Average' as Name Union All Select 'FF' as Code, 'FIFO' as Name "
                    ReportFrm.CreateHelpGrid("Method", "Method", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mQry, "FIFO")

                Case DailyExpenseRegister
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubEndDate)
                    ReportFrm.CreateHelpGrid("Account Gorup", "Account Name", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpAccountGroupQry)
                    ReportFrm.CreateHelpGrid("Site", "Site Name", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSiteQry)

                Case DailyCollectionRegister
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubEndDate)
                    ReportFrm.CreateHelpGrid("Account Gorup", "Account Name", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpAccountGroupQry)
                    ReportFrm.CreateHelpGrid("Site", "Site Name", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSiteQry)

                Case LedgerGrMergeLedger
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubEndDate)
                    ReportFrm.CreateHelpGrid("LedgerGroup", "Ledger Group", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpAccountGroupQry)
                    ReportFrm.CreateHelpGrid("Site", "Site Name", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSiteQry)
                    ReportFrm.CreateHelpGrid("IndexNeeded", "Index Needed", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mHelpYesNoQry, "No")
                    ReportFrm.CreateHelpGrid("ContraACNeeded", "Contra A/C Needed", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mHelpYesNoQry, "No")


                Case AccountGrMergeLedger
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubEndDate)
                    ReportFrm.CreateHelpGrid("AccountGroup", "Account Group", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpAccountGroupQry)
                    ReportFrm.CreateHelpGrid("Site", "Site Name", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSiteQry)
                    ReportFrm.CreateHelpGrid("IndexNeeded", "Index Needed", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mHelpYesNoQry, "No")
                    ReportFrm.CreateHelpGrid("ContraACNeeded", "Contra A/C Needed", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mHelpYesNoQry, "No")
                    ReportFrm.CreateHelpGrid("VoucherType", "Voucher Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpVoucherTypeQry)


                Case GTAReg
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubEndDate)
                    ReportFrm.CreateHelpGrid("ConsignorName", "Consignor Name", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpAccountQry)
                    ReportFrm.CreateHelpGrid("ConsigneeName", "Consignee Name", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpAccountQry)
                    mQry = "Select 'G' as Code, 'G.T.A.' as Name Union All Select 'N' as Code, 'NON G.T.A.' as Name "
                    ReportFrm.CreateHelpGrid("ReportOnChoice", "Report On Choice", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mQry, "G.T.A.")
                    ReportFrm.CreateHelpGrid("Site", "Site Name", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSiteQry)


                Case BillWiseAdj
                    ReportFrm.CreateHelpGrid("AsOnDate", "As On Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubLoginDate)
                    ReportFrm.CreateHelpGrid("AccountGroup", "Account Group", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpAccountGroupQry)
                    ReportFrm.CreateHelpGrid("Account", "Account Name", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpAccountQry)
                    mQry = "Select 'C' as Code, 'Credit' as Name Union All Select 'D' as Code, 'Debit' as Name "
                    ReportFrm.CreateHelpGrid("Report For", "Report For", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mQry, "Debit")
                    ReportFrm.CreateHelpGrid("Site", "Site Name", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSiteQry)

                Case TDSTaxChallan
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubEndDate)
                    ReportFrm.CreateHelpGrid("TdsCategory", "Tds Category", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpTdsCategoryQry)
                    ReportFrm.CreateHelpGrid("Site", "Site Name", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSiteQry)


                Case AccountGrpWsOSAgeing
                    ReportFrm.CreateHelpGrid("AsOnDate", "As On Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubLoginDate)
                    ReportFrm.CreateHelpGrid("AccountGroup", "Account Group", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpAccountGroupQry)
                    ReportFrm.CreateHelpGrid("Account", "Account Name", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpAccountQry)
                    ReportFrm.CreateHelpGrid("IstSlabe", "Ist Slabe", FrmRepDisplay.FieldFilterDataType.NumericType, FrmRepDisplay.FieldDataType.NumericType, Nothing, "30")
                    ReportFrm.CreateHelpGrid("IIndSlabe", "IInd Slabe", FrmRepDisplay.FieldFilterDataType.NumericType, FrmRepDisplay.FieldDataType.NumericType, Nothing, "60")
                    ReportFrm.CreateHelpGrid("IIIrdSlabe", "IIIrd Slabe", FrmRepDisplay.FieldFilterDataType.NumericType, FrmRepDisplay.FieldDataType.NumericType, Nothing, "90")

                Case IntCalForDebtors
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubEndDate)
                    ReportFrm.CreateHelpGrid("PartyName", "Party Name", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpAccountQry)
                    ReportFrm.CreateHelpGrid("Site", "Site Name", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSiteQry)
                    ReportFrm.CreateHelpGrid("InterestRate", "Interest Rate", FrmRepDisplay.FieldFilterDataType.NumericType, FrmRepDisplay.FieldDataType.NumericType, Nothing, "0")
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
#End Region

    Private Function FGetVoucher_TypeQry(ByVal TableName As String) As String
        FGetVoucher_TypeQry = " Select Distinct 'o' As Tick, H.V_Type AS Code, Vt.Description AS [Voucher Type] " &
                                " FROM " & TableName & " H  " &
                                " LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type "
    End Function
    Private Function FIsValid(ByVal IntRow As Integer, Optional ByVal StrMsg As String = "Invalid Data") As Boolean
        Dim BlnRtn As Boolean = True

        If ReportFrm.FilterGrid(GFilter, IntRow).Value = "" Then
            MsgBox(ReportFrm.FilterGrid(GField, IntRow).Value + " : " + vbCrLf + StrMsg)
            ReportFrm.FilterGrid(GFilter, IntRow).Selected = True
            ReportFrm.FilterGrid.Focus()
            BlnRtn = False
        End If
        Return BlnRtn
    End Function

    Private Sub ObjRepFormGlobal_ProcessReport() Handles ReportFrm.ProcessReport
        Select Case mGRepFormName
            Case DailyTransactionSummary
                ProcDailyTransactionSummary()
            Case MonthlyLedgerSummaryFull
                ProcMonthlyLedgerSummaryFull()
            Case TrialDetailDrCr
                ProcTrialDetailDrCr()
            Case MonthlyLedgerSummary
                ProcMonthlyLedgerSummary()
            Case InterestLedger
                ProcInterestLedger()
            Case FBTReport
                ProcFBTReport()
            Case PartyWiseTDSReport

            Case TDSCategoryWiseReport

            Case FixedAssetRegister
                ProcFixedAssetRegister()
            Case Ledger
                ProcLedger()
            Case TrialGroup
                ProcTrialGroup()
            Case TrialDetail

            Case CashBook
                If Trim(ReportFrm.FilterGrid(GFilterCode, 6).Value) = "D" Then
                    ProcCashBook()
                ElseIf Trim(ReportFrm.FilterGrid(GFilterCode, 6).Value) = "J" Then
                    'ProcCashBank_JournalBook()
                Else
                    'ProcBank_CashBookSingle()
                End If
            Case BankBook
                If Trim(ReportFrm.FilterGrid(GFilterCode, 6).Value) = "D" Then
                    'ProcBankBook()
                ElseIf Trim(ReportFrm.FilterGrid(GFilterCode, 6).Value) = "J" Then
                    'ProcCashBank_JournalBook()
                Else
                    'ProcBank_CashBookSingle()
                End If
            Case Annexure
                ProcAnnexure()
            Case DayBook
                ProcDayBook()
            Case Journal
                ProcJournal()
            Case Ageing
                'ProcAgeing()
            Case BillWisesOutStandingAgeing
                ProcBillWsOSAgeing("AmtDr", "AmtCr", "Sundry Debtors")
            Case BillWiseOutStanding_Debtors
                ProcBillWsOS("AmtDr", "AmtCr", "Sundry Debtors")
            Case BillWiseOutStanding_Creditors
                ProcBillWsOS("AmtCr", "AmtDr", "Sundry Creditors")
            Case CashFlow
                ProcCash_Fund_Flow(1)
            Case FundFlow
                ProcCash_Fund_Flow(2)
            Case MonthlyExpenses
                ProcMonthlyExpenses()
            Case FIFOOutStanding_Debtors
                ProcFIFOOutStanding_Debtors()
            Case FIFOOutStanding_Creditors
                ProcFIFOOutStanding_Creditors()
            Case Stock_Valuation
                ProcStockValuation()
            Case DailyExpenseRegister
                ProcDailyExpenseReg()
            Case DailyCollectionRegister
                ProcDailyCollectionReg()
            Case LedgerGrMergeLedger
                ProcLedgerGrMergeLedger()
            Case AccountGrMergeLedger
                ProcAccountGrMergeLedger()
            Case GTAReg
                ProcGTAReg()
            Case BillWiseAdj
                ProcBillWiseAdj()
            Case TDSTaxChallan
                ProcTDSTaxChallan()
            Case AccountGrpWsOSAgeing
                ProcAccountGrpWsOSAgeing()
            Case IntCalForDebtors
                ProcIntCalForDebtors()
        End Select
    End Sub
    Public Sub New(ByVal mReportFrm As FrmRepDisplay)
        ReportFrm = mReportFrm
    End Sub
    Public Sub ProcDailyTransactionSummary(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try
            Dim StrCondition1 As String, StrConditionOP As String, StrConditionSite As String
            Dim StrConditionMain As String

            If Not FIsValid(0) Then Exit Sub
            If Not FIsValid(1) Then Exit Sub
            If Not FIsValid(2) Then Exit Sub
            If Not FIsValid(3) Then Exit Sub
            If Not FIsValid(4) Then Exit Sub

            StrConditionMain = " Where (V_Date Between " & AgL.Chk_Text(CDate(ReportFrm.FilterGrid(GFilter, 0).Value.ToString).ToString("s")) & " And " & AgL.Chk_Text(CDate(ReportFrm.FilterGrid(GFilter, 1).Value.ToString).ToString("s")) & ") "
            StrCondition1 = " Where (LG.V_Date Between " & AgL.Chk_Text(CDate(ReportFrm.FilterGrid(GFilter, 0).Value.ToString).ToString("s")) & " And " & AgL.Chk_Text(CDate(ReportFrm.FilterGrid(GFilter, 1).Value.ToString).ToString("s")) & ") "
            StrConditionOP = " Where LG.V_Date < " & AgL.Chk_Text(CDate(ReportFrm.FilterGrid(GFilter, 0).Value.ToString).ToString("s")) & " "
            StrConditionSite = ""
            If Trim(ReportFrm.FilterGrid(GFilterCode, 2).Value) <> "" Then
                StrConditionSite = " And LG.Site_Code In (" & ReportFrm.FilterGrid(GFilterCode, 2).Value & ") "
            Else
                StrConditionSite = " And LG.Site_Code In  (" & AgL.PubSiteList & ") "
            End If

            If Trim(ReportFrm.FilterGrid(GFilterCode, 3).Value) <> "" Then
                StrConditionSite = " And LG.DivCode In (" & ReportFrm.FilterGrid(GFilterCode, 3).Value & ") "
            Else
                StrConditionSite = " And LG.DivCode In  (" & AgL.PubDivisionList & ") "
            End If


            If Trim(ReportFrm.FilterGrid(GFilterCode, 4).Value) <> "" Then StrCondition1 = StrCondition1 & " And LG.SubCode In (" & ReportFrm.FilterGrid(GFilterCode, 4).Value & ")"
            If Trim(ReportFrm.FilterGrid(GFilterCode, 4).Value) <> "" Then StrConditionOP = StrConditionOP & " And LG.SubCode In (" & ReportFrm.FilterGrid(GFilterCode, 4).Value & ")"


            If Trim(ReportFrm.FilterGrid(GFilterCode, 5).Value) <> "" Then StrCondition1 = StrCondition1 & " And LG.V_Type In (" & ReportFrm.FilterGrid(GFilterCode, 5).Value & ")"
            If Trim(ReportFrm.FilterGrid(GFilterCode, 5).Value) <> "" Then StrConditionOP = StrConditionOP & " And LG.V_Type In (" & ReportFrm.FilterGrid(GFilterCode, 5).Value & ")"


            '========== Head Query Date Wise Grouping ====================================
            StrSQLQuery = "Select V_Date,IfNull(Sum(AmtDr),0) As AmtDr,IfNull(Sum(AmtCr),0) As AmtCr, "
            StrSQLQuery += "IfNull(Sum(OPBal),0) As OPBal "
            StrSQLQuery += "From ( "
            '========== For Detail Section =======
            StrSQLQuery += "Select	LG.V_Date, "
            StrSQLQuery += "IfNull(LG.AmtDr,0) As AmtDr ,IfNull(LG.AmtCr,0) As AmtCr,0 As OPBal  "
            StrSQLQuery += "From Ledger LG "
            StrSQLQuery += StrCondition1 + StrConditionSite
            StrSQLQuery += "Union All "

            '======= For Opening Balance =========
            StrSQLQuery += "Select " & AgL.Chk_Text(CDate(ReportFrm.FilterGrid(GFilter, 0).Value.ToString).ToString("s")) & " As V_Date, "
            StrSQLQuery += "0 As AmtDr,0 As AmtCr, "
            StrSQLQuery += "IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LG.AmtCr),0) As OPBal "
            StrSQLQuery += "From Ledger LG "
            StrSQLQuery += StrConditionOP + StrConditionSite
            StrSQLQuery += "Group By LG.V_Date "
            StrSQLQuery += " ) As Tmp "
            StrSQLQuery += StrConditionMain
            StrSQLQuery += "Group By V_Date "
            StrSQLQuery += "Order By V_Date "

            DsRep = AgL.FillData(StrSQLQuery, AgL.GCn)

            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            ReportFrm.Text = "Daily Transaction Summary"
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = System.Reflection.MethodBase.GetCurrentMethod().Name

            ReportFrm.ProcFillGrid(DsRep)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub

    Public Sub ProcMonthlyLedgerSummaryFull(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try
            Dim StrCondition1 As String, StrConditionOP As String, StrConditionsite As String

            Dim DblFirstYear As Double, DblSecondYear As Double

            If Not FIsValid(0) Then Exit Sub
            If Not FIsValid(1) Then Exit Sub

            DblFirstYear = Year(AgL.PubStartDate)
            DblSecondYear = Year(AgL.PubEndDate)

            StrCondition1 = " Where (LG.V_Date Between " & AgL.Chk_Text(CDate(ReportFrm.FilterGrid(GFilter, 0).Value.ToString).ToString("s")) & " And " & AgL.Chk_Text(CDate(ReportFrm.FilterGrid(GFilter, 1).Value.ToString).ToString("s")) & ") "
            StrConditionOP = " Where LG.V_Date < " & AgL.Chk_Text(CDate(ReportFrm.FilterGrid(GFilter, 0).Value.ToString).ToString("s")) & " "
            StrConditionsite = ""
            If Trim(ReportFrm.FilterGrid(GFilterCode, 2).Value) <> "" Then StrCondition1 = StrCondition1 & " And LG.SubCode In (" & ReportFrm.FilterGrid(GFilterCode, 2).Value & ")"
            If Trim(ReportFrm.FilterGrid(GFilterCode, 2).Value) <> "" Then StrConditionOP = StrConditionOP & " And LG.SubCode In (" & ReportFrm.FilterGrid(GFilterCode, 2).Value & ")"

            If Trim(ReportFrm.FilterGrid(GFilterCode, 3).Value) <> "" Then
                StrConditionsite = " And LG.site_Code In (" & ReportFrm.FilterGrid(GFilterCode, 3).Value & ") "
            Else
                StrConditionsite = " And LG.site_Code In  (" & AgL.PubSiteList & ") "
            End If
            StrSQLQuery = "Select Max(SName) As SName,IfNull(Sum(AmtDr),0) As AmtDr, IfNull(Sum(AmtCr),0) As AmtCr, "
            StrSQLQuery += "Max(Month) As Month,Max(Narration) As Narration,ID  "
            StrSQLQuery += "From "
            '======= For Opening Balance =========
            StrSQLQuery += "( Select IfNull(SG.SubCode,'') As SubCode, Max(IfNull(SG.Name,'')) As SName, "
            StrSQLQuery += "(Case When (IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LG.AmtCr),0))>0 Then  "
            StrSQLQuery += "(IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LG.AmtCr),0)) Else 0 End) As AmtDr, "
            StrSQLQuery += "(Case When (IfNull(Sum(LG.AmtCr),0)-IfNull(Sum(LG.AmtDr),0))>0 Then "
            StrSQLQuery += "(IfNull(Sum(LG.AmtCr),0)-IfNull(Sum(LG.AmtDr),0)) Else 0 End) As AmtCr, '' AS Month,'OPENING BALANCE' As Narration,0 AS ID,0 as MON,0 as yr  "
            StrSQLQuery += "From Ledger LG  "
            StrSQLQuery += "Left Join SubGroup SG On LG.SubCode=SG.SubCode "
            StrSQLQuery += StrConditionOP + StrConditionsite
            StrSQLQuery += "Group By IfNull(SG.SubCode,'') "
            '======= For Detail =========
            StrSQLQuery += "Union All "
            StrSQLQuery += "Select	IfNull(SG.SubCode,'') As SubCode, Max(IfNull(SG.Name,'')) As SName, "
            StrSQLQuery += "IfNull(Sum(LG.AmtDr),0) As AmtDr, "
            StrSQLQuery += "IfNull(Sum(LG.AmtCr),0) As AmtCr, "
            StrSQLQuery += "Max(strftime('%m', LG.V_date) +' ' || (strftime('%y', LG.V_date))) AS Month,'' As Narration,1 AS ID,  "
            StrSQLQuery += "max(strftime('%m', LG.V_date)) AS MON,max(strftime('%y', LG.V_date)) AS yr "
            StrSQLQuery += "From Ledger  "
            StrSQLQuery += "LG Left Join SubGroup SG On LG.SubCode=SG.SubCode "
            StrSQLQuery += StrCondition1 + StrConditionsite
            StrSQLQuery += "Group By IfNull(SG.SubCode,''),(strftime('%m', LG.V_date) +' ' || (strftime('%y', LG.V_date))) "
            StrSQLQuery += " ) As Tmp "
            StrSQLQuery += "Group By SubCode,ID,MON having IfNull(SubCode,'')<>''  "
            StrSQLQuery += "Order By Max(SName),ID,MAX(Yr),MON "

            DsRep = AgL.FillData(StrSQLQuery, AgL.GCn)

            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            ReportFrm.Text = "Daily Transaction Summary"
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = System.Reflection.MethodBase.GetCurrentMethod().Name

            ReportFrm.ProcFillGrid(DsRep)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub

    Public Sub ProcTrialDetailDrCr(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try
            Dim StrCondition1 As String, StrConditionOP As String, StrConditionsite As String


            If Not FIsValid(0) Then Exit Sub
            If Not FIsValid(1) Then Exit Sub

            StrCondition1 = " Where (LG.V_Date Between " & AgL.Chk_Text(CDate(ReportFrm.FilterGrid(GFilter, 0).Value.ToString).ToString("s")) & " And " & AgL.Chk_Text(CDate(ReportFrm.FilterGrid(GFilter, 1).Value.ToString).ToString("s")) & ") "
            StrConditionOP = " Where LG.V_Date < " & AgL.Chk_Text(CDate(ReportFrm.FilterGrid(GFilter, 0).Value.ToString).ToString("s")) & " "
            StrConditionsite = ""

            If Trim(ReportFrm.FilterGrid(GFilterCode, 2).Value) <> "" Then StrCondition1 = StrCondition1 & " And (IfNull(SG.GroupCode,'') In (Select IfNull(AGP.GroupCode,'') From AcGroupPath AGP Where AGP.GroupUnder In (" & ReportFrm.FilterGrid(GFilterCode, 2).Value & ")) Or SG.GroupCode In (" & ReportFrm.FilterGrid(GFilterCode, 2).Value & ")) "
            If Trim(ReportFrm.FilterGrid(GFilterCode, 2).Value) <> "" Then StrConditionOP = StrConditionOP & " And (IfNull(SG.GroupCode,'') In (Select IfNull(AGP.GroupCode,'') From AcGroupPath AGP Where AGP.GroupUnder In (" & ReportFrm.FilterGrid(GFilterCode, 2).Value & ")) Or SG.GroupCode In (" & ReportFrm.FilterGrid(GFilterCode, 2).Value & ")) "

            If Trim(ReportFrm.FilterGrid(GFilterCode, 3).Value) <> "" Then StrCondition1 = StrCondition1 & " And LG.SubCode In (" & ReportFrm.FilterGrid(GFilterCode, 3).Value & ")"
            If Trim(ReportFrm.FilterGrid(GFilterCode, 3).Value) <> "" Then StrConditionOP = StrConditionOP & " And LG.SubCode In (" & ReportFrm.FilterGrid(GFilterCode, 3).Value & ")"

            If Trim(ReportFrm.FilterGrid(GFilterCode, 4).Value) <> "" Then
                StrConditionsite = " and LG.site_Code In (" & ReportFrm.FilterGrid(GFilterCode, 4).Value & ") "
            Else
                StrConditionsite = " and LG.site_Code In  (" & AgL.PubSiteList & ") "
            End If

            If Trim(ReportFrm.FilterGrid(GFilterCode, 5).Value) <> "" Then
                StrConditionsite = " and LG.DivCode In (" & ReportFrm.FilterGrid(GFilterCode, 5).Value & ") "
            Else
                StrConditionsite = " and LG.DivCode In  (" & AgL.PubDivisionList & ") "
            End If

            StrSQLQuery = "Select Max(GroupName) AS GroupName,Max(SName) As SName, IfNull(Sum(OPBalDr),0) As OPBalDr, "
            StrSQLQuery += "IfNull(Sum(OPBalCr),0) As OPBalCr,IfNull(Sum(AmtDr),0) As AmtDr, IfNull(Sum(AmtCr),0) As AmtCr "
            StrSQLQuery += "From "
            StrSQLQuery += "( Select Max(IfNull(AG.GroupName,'')) AS GroupName,IfNull(AG.GroupCode,'') As GroupCode, "
            StrSQLQuery += "IfNull(SG.SubCode,'') As SubCode, Max(IfNull(SG.Name,'')) As SName, "
            StrSQLQuery += "(Case When (IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LG.AmtCr),0))>0 Then  "
            StrSQLQuery += "(IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LG.AmtCr),0)) Else 0 End) As OPBalDr, "
            StrSQLQuery += "(Case When (IfNull(Sum(LG.AmtCr),0)-IfNull(Sum(LG.AmtDr),0))>0 Then "
            StrSQLQuery += "(IfNull(Sum(LG.AmtCr),0)-IfNull(Sum(LG.AmtDr),0)) Else 0 End) As OPBalCr,0 As AmtDr,0 As AmtCr "
            StrSQLQuery += "From Ledger LG Left Join SubGroup SG On LG.SubCode=SG.SubCode "
            StrSQLQuery += "LEFT JOIN AcGroup AG ON AG.GroupCode=SG.GroupCode "
            StrSQLQuery += StrConditionOP + StrConditionsite
            StrSQLQuery += "Group By IfNull(AG.GroupCode,''),IfNull(SG.SubCode,'') "
            StrSQLQuery += "Having(IfNull(Sum(LG.AmtDr), 0) - IfNull(Sum(LG.AmtCr), 0)) <> 0 "
            StrSQLQuery += "Union All "
            StrSQLQuery += "Select	Max(IfNull(AG.GroupName,'')) AS GroupName,IfNull(AG.GroupCode,'') As GroupCode, "
            StrSQLQuery += "IfNull(SG.SubCode,'') As SubCode, "
            StrSQLQuery += "Max(IfNull(SG.Name,'')) As SName, 0 As OPBalDr,0 As OPBalCr, "
            StrSQLQuery += "IfNull(Sum(LG.AmtDr),0) As AmtDr,  "
            StrSQLQuery += "IfNull(Sum(LG.AmtCr),0) As AmtCr "
            StrSQLQuery += "From Ledger LG Left Join SubGroup SG On LG.SubCode=SG.SubCode "
            StrSQLQuery += "LEFT JOIN AcGroup AG ON AG.GroupCode=SG.GroupCode "
            StrSQLQuery += StrCondition1 + StrConditionsite
            StrSQLQuery += "Group By IfNull(AG.GroupCode,''),IfNull(SG.SubCode,'') "
            StrSQLQuery += "Having (IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LG.AmtCr),0)) <> 0 ) As Tmp "
            StrSQLQuery += "Group By GroupCode,SubCode "
            StrSQLQuery += "Order By Max(GroupName),Max(SName) "

            DsRep = AgL.FillData(StrSQLQuery, AgL.GCn)

            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            ReportFrm.Text = "Daily Transaction Summary"
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = System.Reflection.MethodBase.GetCurrentMethod().Name

            ReportFrm.ProcFillGrid(DsRep)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub

    Public Sub ProcMonthlyLedgerSummary(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try
            Dim StrCondition1 As String = ""
            Dim TempField As String

            Dim DblFirstYear As Double, DblSecondYear As Double

            If Not FIsValid(0) Then Exit Sub

            If Trim(ReportFrm.FilterGrid(GFilterCode, 0).Value) = "F" Then
                TempField = "0 As Sel "
                DblFirstYear = Year(AgL.PubStartDate)
                DblSecondYear = Year(AgL.PubStartDate)
                StrCondition1 += "Where Month(LG.V_Date) In (4,5,6,7,8,9) And Year(LG.V_Date) In (" & DblFirstYear & ") "
            Else
                TempField = "1 As Sel "
                DblFirstYear = Year(AgL.PubStartDate)
                DblSecondYear = Year(AgL.PubEndDate)
                StrCondition1 += "Where Month(LG.V_Date) In (10,11,12,1,2,3) And Year(LG.V_Date) In (" & DblFirstYear & "," & DblSecondYear & ") "
            End If
            If Trim(ReportFrm.FilterGrid(GFilterCode, 2).Value) <> "" Then
                StrCondition1 = StrCondition1 & " And LG.Site_Code IN (" & ReportFrm.FilterGrid(GFilterCode, 2).Value & ") "
            Else
                StrCondition1 = StrCondition1 & " And LG.Site_Code IN (" & AgL.PubSiteList & ") "
            End If

            If Trim(ReportFrm.FilterGrid(GFilterCode, 1).Value) <> "" Then StrCondition1 = StrCondition1 & " And (IfNull(SG.GroupCode,'') In (Select IfNull(AGP.GroupCode,'') From AcGroupPath AGP Where AGP.GroupUnder In (" & ReportFrm.FilterGrid(GFilterCode, 1).Value & ")) Or SG.GroupCode In (" & ReportFrm.FilterGrid(GFilterCode, 1).Value & ")) "

            StrSQLQuery = "Select GroupName,PName,SubCode,"
            StrSQLQuery += "(Case When (IfNull(DR_1,0)-IfNull(CR_1,0))>0 Then (IfNull(DR_1,0)-IfNull(CR_1,0)) Else 0 End) As DR_1, "
            StrSQLQuery += "(Case When (IfNull(CR_1,0)-IfNull(DR_1,0))>0 Then (IfNull(CR_1,0)-IfNull(DR_1,0)) Else 0 End) As CR_1, "
            StrSQLQuery += "(Case When (IfNull(DR_2,0)-IfNull(CR_2,0))>0 Then (IfNull(DR_2,0)-IfNull(CR_2,0)) Else 0 End) As DR_2, "
            StrSQLQuery += "(Case When (IfNull(CR_2,0)-IfNull(DR_2,0))>0 Then (IfNull(CR_2,0)-IfNull(DR_2,0)) Else 0 End) As CR_2, "
            StrSQLQuery += "(Case When (IfNull(DR_3,0)-IfNull(CR_3,0))>0 Then (IfNull(DR_3,0)-IfNull(CR_3,0)) Else 0 End) As DR_3, "
            StrSQLQuery += "(Case When (IfNull(CR_3,0)-IfNull(DR_3,0))>0 Then (IfNull(CR_3,0)-IfNull(DR_3,0)) Else 0 End) As CR_3, "
            StrSQLQuery += "(Case When (IfNull(DR_4,0)-IfNull(CR_4,0))>0 Then (IfNull(DR_4,0)-IfNull(CR_4,0)) Else 0 End) As DR_4, "
            StrSQLQuery += "(Case When (IfNull(CR_4,0)-IfNull(DR_4,0))>0 Then (IfNull(CR_4,0)-IfNull(DR_4,0)) Else 0 End) As CR_4, "
            StrSQLQuery += "(Case When (IfNull(DR_5,0)-IfNull(CR_5,0))>0 Then (IfNull(DR_5,0)-IfNull(CR_5,0)) Else 0 End) As DR_5, "
            StrSQLQuery += "(Case When (IfNull(CR_5,0)-IfNull(DR_5,0))>0 Then (IfNull(CR_5,0)-IfNull(DR_5,0)) Else 0 End) As CR_5, "
            StrSQLQuery += "(Case When (IfNull(DR_6,0)-IfNull(CR_6,0))>0 Then (IfNull(DR_6,0)-IfNull(CR_6,0)) Else 0 End) As DR_6, "
            StrSQLQuery += "(Case When (IfNull(CR_6,0)-IfNull(DR_6,0))>0 Then (IfNull(CR_6,0)-IfNull(DR_6,0)) Else 0 End) As CR_6, "
            StrSQLQuery += "Sel "
            StrSQLQuery += "From ( "
            StrSQLQuery += "Select	AG.GroupName,Max(SG.Name) As PName,LG.SubCode, "
            StrSQLQuery += "Sum(Case When (Month(LG.V_Date)=4 Or Month(LG.V_Date)=10)And Year(LG.V_Date)=" & DblFirstYear & " Then LG.AmtDr Else 0 End) As DR_1, "
            StrSQLQuery += "Sum(Case When (Month(LG.V_Date)=4 Or Month(LG.V_Date)=10)And Year(LG.V_Date)=" & DblFirstYear & " Then LG.AmtCr Else 0 End) As CR_1, "
            StrSQLQuery += "Sum(Case When (Month(LG.V_Date)=5 Or Month(LG.V_Date)=11)And Year(LG.V_Date)=" & DblFirstYear & " Then LG.AmtDr Else 0 End) As DR_2, "
            StrSQLQuery += "Sum(Case When (Month(LG.V_Date)=5 Or Month(LG.V_Date)=11)And Year(LG.V_Date)=" & DblFirstYear & " Then LG.AmtCr Else 0 End) As CR_2, "
            StrSQLQuery += "Sum(Case When (Month(LG.V_Date)=6 Or Month(LG.V_Date)=12)And Year(LG.V_Date)=" & DblFirstYear & " Then LG.AmtDr Else 0 End) As DR_3, "
            StrSQLQuery += "Sum(Case When (Month(LG.V_Date)=6 Or Month(LG.V_Date)=12)And Year(LG.V_Date)=" & DblFirstYear & " Then LG.AmtCr Else 0 End) As CR_3, "
            StrSQLQuery += "Sum(Case When (Month(LG.V_Date)=7 Or Month(LG.V_Date)=1)And Year(LG.V_Date)=" & DblSecondYear & " Then LG.AmtDr Else 0 End) As DR_4, "
            StrSQLQuery += "Sum(Case When (Month(LG.V_Date)=7 Or Month(LG.V_Date)=1)And Year(LG.V_Date)=" & DblSecondYear & " Then LG.AmtCr Else 0 End) As CR_4, "
            StrSQLQuery += "Sum(Case When (Month(LG.V_Date)=8 Or Month(LG.V_Date)=2)And Year(LG.V_Date)=" & DblSecondYear & " Then LG.AmtDr Else 0 End) As DR_5, "
            StrSQLQuery += "Sum(Case When (Month(LG.V_Date)=8 Or Month(LG.V_Date)=2)And Year(LG.V_Date)=" & DblSecondYear & " Then LG.AmtCr Else 0 End) As CR_5, "
            StrSQLQuery += "Sum(Case When (Month(LG.V_Date)=9 Or Month(LG.V_Date)=3)And Year(LG.V_Date)=" & DblSecondYear & " Then LG.AmtDr Else 0 End) As DR_6, "
            StrSQLQuery += "Sum(Case When (Month(LG.V_Date)=9 Or Month(LG.V_Date)=3)And Year(LG.V_Date)=" & DblSecondYear & " Then LG.AmtCr Else 0 End) As CR_6, "
            StrSQLQuery += TempField
            StrSQLQuery += "From Ledger LG Left Join "
            StrSQLQuery += "SubGroup SG ON LG.SubCode=SG.SubCode Left Join "
            StrSQLQuery += "AcGroup AG ON AG.GroupCode=SG.GroupCode "
            StrSQLQuery += StrCondition1
            StrSQLQuery += "Group By AG.GroupName,LG.SubCode "
            StrSQLQuery += ") As Tmp "
            StrSQLQuery += "Order By GroupName,PName "
            DsRep = AgL.FillData(StrSQLQuery, AgL.GCn)

            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            ReportFrm.Text = "Daily Transaction Summary"
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = System.Reflection.MethodBase.GetCurrentMethod().Name

            ReportFrm.ProcFillGrid(DsRep)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub

    Public Sub ProcInterestLedger(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try
            Dim StrCondition As String, StrField As String


            If Not FIsValid(0) Then Exit Sub
            If Not FIsValid(1) Then Exit Sub
            If Not FIsValid(2) Then Exit Sub
            If Not FIsValid(3) Then Exit Sub
            If Not FIsValid(3) Then Exit Sub
            StrField = ""
            StrCondition = " Where (L.V_Date Between " & AgL.Chk_Text(CDate(ReportFrm.FilterGrid(GFilter, 0).Value.ToString).ToString("s")) & " And " & AgL.Chk_Text(CDate(ReportFrm.FilterGrid(GFilter, 1).Value.ToString).ToString("s")) & ") "

            StrField = "," & Val(ReportFrm.FilterGrid(GFilter, 2).Value) & " as IntrateDr"
            StrField += "," & Val(ReportFrm.FilterGrid(GFilter, 3).Value) & " as IntrateCr"
            StrField += "," & Val(ReportFrm.FilterGrid(GFilter, 4).Value) & " as Days"
            StrField += ",'" & Trim(ReportFrm.FilterGrid(GFilter, 1).Value) & "' as ToDate"
            If Trim(ReportFrm.FilterGrid(GFilterCode, 5).Value) <> "" Then StrCondition += " And SG.Subcode In (" & ReportFrm.FilterGrid(GFilterCode, 5).Value & ")"

            If Trim(ReportFrm.FilterGrid(GFilterCode, 6).Value) <> "" Then
                StrCondition += " and L.site_Code In (" & ReportFrm.FilterGrid(GFilterCode, 6).Value & ") "
            Else
                StrCondition += " and L.site_Code In  (" & AgL.PubSiteList & ") "
            End If

            StrSQLQuery = "SELECT Max(SG.Name) AS Party,Max(L.V_Date) AS VDate,max(L.V_type) as V_type, sum(amtdr) AS DRAmt,"
            StrSQLQuery += "sum(amtcr) AS CRAmt, sum(amtdr)-sum(amtcr) AS Bal" + StrField
            StrSQLQuery += " FROM Ledger L"
            StrSQLQuery += " LEFT JOIN SubGroup SG ON SG.SubCode=L.SubCode "
            StrSQLQuery += StrCondition
            StrSQLQuery += " GROUP BY L.SubCode,L.V_Date,L.V_No ORDER BY L.SubCode"


            DsRep = AgL.FillData(StrSQLQuery, AgL.GCn)

            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            ReportFrm.Text = "Daily Transaction Summary"
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = System.Reflection.MethodBase.GetCurrentMethod().Name

            ReportFrm.ProcFillGrid(DsRep)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub

    Public Sub ProcFBTReport(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try
            Dim StrCondition As String

            Dim StrCnd As String = ""

            If Not FIsValid(0) Then Exit Sub
            If Not FIsValid(1) Then Exit Sub

            If Trim(ReportFrm.FilterGrid(GFilter, 3).Value) = "Yes" Then
                StrCondition = " And (L.V_Date <=" & AgL.Chk_Text(CDate(ReportFrm.FilterGrid(GFilter, 1).Value.ToString).ToString("s")) & " ) "
            Else
                StrCondition = " And (L.V_Date Between " & AgL.Chk_Text(CDate(ReportFrm.FilterGrid(GFilter, 0).Value.ToString).ToString("s")) & " And " & AgL.Chk_Text(CDate(ReportFrm.FilterGrid(GFilter, 1).Value.ToString).ToString("s")) & " ) "
            End If

            If Trim(ReportFrm.FilterGrid(GFilterCode, 2).Value) <> "" Then StrCondition = StrCondition & " And L.SubCode In (" & ReportFrm.FilterGrid(GFilterCode, 2).Value & ")"

            If Trim(ReportFrm.FilterGrid(GFilterCode, 4).Value) <> "" Then
                StrCondition += " And  L.Site_Code IN (" & ReportFrm.FilterGrid(GFilterCode, 4).Value & ") "
            Else
                StrCondition += " And  L.Site_Code IN (" & AgL.PubSiteList & ") "
            End If


            StrSQLQuery = "SELECT max(SG.SubCode) AS SubCode,SG.Name,"
            StrSQLQuery += "sum(L.AmtDr)-sum (L.AmtCr) AS DrBal,"
            StrSQLQuery += "Max(IfNull(SG.FBTOnPer,0)) AS FBTOnPer,"
            StrSQLQuery += "Max(IfNull(SG.FBTOnPer,0))*(sum(L.AmtDr)-sum (L.AmtCr))/100 AS FBTOn,"
            StrSQLQuery += "Max(IfNull(SG.FBTPer,0)) AS FBTPer,"
            StrSQLQuery += "(Max(IfNull(SG.FBTOnPer,0))*(sum(L.AmtDr)-sum (L.AmtCr))/100)*Max(IfNull(SG.FBTPer,0))/100 AS FBT "
            StrSQLQuery += "FROM Ledger L "
            StrSQLQuery += "LEFT JOIN SubGroup SG ON SG.SubCode=L.SubCode "
            StrSQLQuery += "WHERE SG.Nature='Expenses' "
            StrSQLQuery += StrCondition
            StrSQLQuery += "AND IfNull(SG.FBTOnPer,0)>0 "
            StrSQLQuery += "AND IfNull(SG.FBTPer,0)>0 "
            StrSQLQuery += "GROUP BY SG.Name "
            StrSQLQuery += "HAVING(sum(L.AmtDr) - sum(L.AmtCr) > 0)"


            DsRep = AgL.FillData(StrSQLQuery, AgL.GCn)

            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            ReportFrm.Text = "Daily Transaction Summary"
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = System.Reflection.MethodBase.GetCurrentMethod().Name

            ReportFrm.ProcFillGrid(DsRep)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub


    Public Sub ProcFixedAssetRegister(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try
            Dim StrIST6Month As String = ""
            Dim StrLast6Month As String = ""
            Dim StrCondition2 As String = ""
            Dim StrCondition3 As String = ""


            If Not FIsValid(0) Then Exit Sub
            If Not FIsValid(1) Then Exit Sub
            If Not FIsValid(2) Then Exit Sub

            If DateValue(ReportFrm.FilterGrid(GFilter, 0).Value) < DateValue(AgL.PubStartDate) Or DateValue(ReportFrm.FilterGrid(GFilter, 0).Value) > DateValue(AgL.PubEndDate) Then
                MsgBox("As On Date Is Not In Financial Date")
                Exit Sub
            End If

            'Date Setting For Ist 6 Month        
            StrIST6Month = "'" & AgL.PubStartDate & "'"

            If ReportFrm.FilterGrid(GFilter, 0).Value >= AgL.PubStartDate And ReportFrm.FilterGrid(GFilter, 0).Value <= Microsoft.VisualBasic.DateAdd(DateInterval.Day, +182, CDate(AgL.PubStartDate)) Then
                StrIST6Month = StrIST6Month & " And " & "'" & ReportFrm.FilterGrid(GFilter, 0).Value & "'"
            Else
                StrIST6Month = StrIST6Month & " And " & "'" & Microsoft.VisualBasic.DateAdd(DateInterval.Day, +182, CDate(AgL.PubStartDate)) & "'"
            End If

            'Date Setting For Last 6 Month    
            If ReportFrm.FilterGrid(GFilter, 0).Value >= Microsoft.VisualBasic.DateAdd(DateInterval.Day, +183, CDate(AgL.PubStartDate)) And ReportFrm.FilterGrid(GFilter, 0).Value <= AgL.PubEndDate Then
                StrLast6Month = "'" & Microsoft.VisualBasic.DateAdd(DateInterval.Day, +183, CDate(AgL.PubStartDate)) & "'"
                StrLast6Month = StrLast6Month & " And " & "'" & ReportFrm.FilterGrid(GFilter, 0).Value & "'"
            End If

            StrCondition2 = "WHERE AR.V_Date Between  '" & CDate(AgL.PubStartDate).ToString("s") & "' And  '" & CDate(AgL.PubEndDate).ToString("s") & "' "
            StrCondition3 = "And AT.V_Date Between  '" & CDate(AgL.PubStartDate).ToString("s") & "' And  '" & CDate(AgL.PubEndDate).ToString("s") & "' "

            If Trim(ReportFrm.FilterGrid(GFilterCode, 1).Value) <> "" Then
                StrCondition2 = StrCondition2 & " And AGM.Code IN (" & ReportFrm.FilterGrid(GFilterCode, 1).Value & ") "
            End If

            StrSQLQuery = "SELECT DISTINCT AGM.Name AS Group_Name,AM.Name AS Asset_Description,AGM.Depreciation AS Depreciation,"
            StrSQLQuery = StrSQLQuery + "(SELECT Distinct AMOUNT FROM AssetTransaction AT WHERE AM.Docid=AT.Asset AND AT.V_TYPE IN ('ASTOP') " & StrCondition3 & ") AS OPEING,"
            If StrLast6Month <> "" Then
                StrSQLQuery = StrSQLQuery + "(SELECT SUM(AMOUNT) FROM AssetTransaction AT WHERE AM.Docid=AT.Asset AND AT.V_TYPE IN ('ASTPR','ASTAP') And AT.V_Date Between " & StrLast6Month & ") AS Last6Month,"
                StrSQLQuery = StrSQLQuery + "(SELECT Distinct AMOUNT FROM AssetTransaction AT WHERE AM.Docid=AT.Asset AND AT.V_TYPE IN ('ASTPR') And AT.V_Date Between " & StrLast6Month & ") AS PurchaseVal,"
                StrSQLQuery = StrSQLQuery + "(SELECT Distinct DATEDIFF(DD,V_DATE,'" & ReportFrm.FilterGrid(GFilter, 0).Value & "') FROM AssetTransaction AT WHERE AM.Docid=AT.Asset AND AT.V_TYPE IN ('ASTPR') And AT.V_Date Between " & StrLast6Month & ") AS DepLast6Days,"
            Else
                StrSQLQuery = StrSQLQuery + "0  AS Last6Month,"
                StrSQLQuery = StrSQLQuery + "0  AS PurchaseVal,"
                StrSQLQuery = StrSQLQuery + "0 AS DepLast6Days,"
            End If
            StrSQLQuery = StrSQLQuery + "(SELECT SUM(AMOUNT) FROM AssetTransaction AT WHERE AM.Docid=AT.Asset AND AT.V_TYPE IN ('ASTPR','ASTAP') And AT.V_Date Between " & StrIST6Month & ") AS Ist6Month,"
            StrSQLQuery = StrSQLQuery + "(SELECT Distinct DATEDIFF(DD,'" & AgL.PubStartDate & "','" & ReportFrm.FilterGrid(GFilter, 0).Value & "') FROM AssetTransaction AT) AS DepIst6Days,"
            StrSQLQuery = StrSQLQuery + "(SELECT Distinct AMOUNT FROM AssetTransaction AT WHERE AM.Docid=AT.Asset AND AT.V_TYPE IN ('ASTSL') " & StrCondition3 & ") AS SALEVal "
            StrSQLQuery = StrSQLQuery + "FROM AssetMast AM "
            StrSQLQuery = StrSQLQuery + "INNER JOIN AssetTransaction AR ON AM.Docid=AR.Asset "
            StrSQLQuery = StrSQLQuery + "INNER JOIN Voucher_Type VT ON VT.V_Type=AR.V_Type "
            StrSQLQuery = StrSQLQuery + "INNER JOIN AssetGroupMast AGM ON AGM.Code=AM.AssetGroup " + StrCondition2



            DsRep = AgL.FillData(StrSQLQuery, AgL.GCn)

            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            ReportFrm.Text = "Daily Transaction Summary"
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = System.Reflection.MethodBase.GetCurrentMethod().Name

            ReportFrm.ProcFillGrid(DsRep)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub

    Public Sub ProcLedger(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try
            Dim StrCondition1 As String, StrConditionOP As String, StrConditionsite As String

            Dim I As Integer

            If Not FIsValid(0) Then Exit Sub
            If Not FIsValid(1) Then Exit Sub

            StrCondition1 = " Where (LG.V_Date Between " & AgL.Chk_Text(CDate(ReportFrm.FilterGrid(GFilter, 0).Value.ToString).ToString("s")) & " And " & AgL.Chk_Text(CDate(ReportFrm.FilterGrid(GFilter, 1).Value.ToString).ToString("s")) & ") "
            StrConditionOP = " Where LG.V_Date < " & AgL.Chk_Text(CDate(ReportFrm.FilterGrid(GFilter, 0).Value.ToString).ToString("s")) & " "

            If Trim(ReportFrm.FilterGrid(GFilterCode, 2).Value) <> "" Then StrCondition1 = StrCondition1 & " And (IfNull(SG.GroupCode,'') In (Select IfNull(AGP.GroupCode,'') From AcGroupPath AGP Where AGP.GroupUnder In (" & ReportFrm.FilterGrid(GFilterCode, 2).Value & ")) Or SG.GroupCode In (" & ReportFrm.FilterGrid(GFilterCode, 2).Value & ")) "
            If Trim(ReportFrm.FilterGrid(GFilterCode, 2).Value) <> "" Then StrConditionOP = StrConditionOP & " And (IfNull(SG.GroupCode,'') In (Select IfNull(AGP.GroupCode,'') From AcGroupPath AGP Where AGP.GroupUnder In (" & ReportFrm.FilterGrid(GFilterCode, 2).Value & ")) Or SG.GroupCode In (" & ReportFrm.FilterGrid(GFilterCode, 2).Value & ")) "
            If Trim(ReportFrm.FilterGrid(GFilterCode, 3).Value) <> "" Then StrCondition1 = StrCondition1 & " And LG.SubCode In (" & ReportFrm.FilterGrid(GFilterCode, 3).Value & ")"
            If Trim(ReportFrm.FilterGrid(GFilterCode, 3).Value) <> "" Then StrConditionOP = StrConditionOP & " And LG.SubCode In (" & ReportFrm.FilterGrid(GFilterCode, 3).Value & ")"
            If Trim(ReportFrm.FilterGrid(GFilterCode, 4).Value) <> "" Then StrCondition1 = StrCondition1 & " And LG.V_Type In (" & ReportFrm.FilterGrid(GFilterCode, 4).Value & ")"
            If Trim(ReportFrm.FilterGrid(GFilterCode, 4).Value) <> "" Then StrConditionOP = StrConditionOP & " And LG.V_Type In (" & ReportFrm.FilterGrid(GFilterCode, 4).Value & ")"

            StrConditionsite = ""
            If Trim(ReportFrm.FilterGrid(GFilterCode, 5).Value) <> "" Then
                StrConditionsite = " and LG.site_Code In (" & ReportFrm.FilterGrid(GFilterCode, 5).Value & ") "
            Else
                StrConditionsite = " and LG.site_Code In  (" & AgL.PubSiteList & ") "
            End If

            If Trim(ReportFrm.FilterGrid(GFilterCode, 6).Value) <> "" Then
                StrConditionsite = " and LG.DivCode In (" & ReportFrm.FilterGrid(GFilterCode, 6).Value & ") "
            Else
                StrConditionsite = " and LG.DivCode In  (" & AgL.PubSiteList & ") "
            End If


            '========== For Detail Section =======
            StrSQLQuery = "Select	LG.V_Type,Cast(LG.V_No as Varchar) As V_No,LG.V_Date,LG.V_Prefix,SG.Name  As PName,LG.SubCode,LG.Narration, "
            StrSQLQuery = StrSQLQuery + "LG.AmtDr,LG.AmtCr,1 As SNo,SM.Name as Division,LG.ContraText As ContraName,LG.Chq_No,LG.Chq_Date,"
            StrSQLQuery = StrSQLQuery + "IfNull(C.CityName,'') as PCity,IfNull(LG.Site_Code,'') As Site_Code "
            StrSQLQuery = StrSQLQuery + "From Ledger LG Left Join SubGroup SG On LG.Subcode=SG.SubCode "
            StrSQLQuery = StrSQLQuery + "Left Join Sitemast SM On LG.Site_Code=SM.Code "
            StrSQLQuery = StrSQLQuery + "Left Join City C On C.CityCode=SG.CityCode "

            StrSQLQuery = StrSQLQuery + StrCondition1 + StrConditionsite
            StrSQLQuery = StrSQLQuery + "Union All "

            '======= For Opening Balance =========
            StrSQLQuery = StrSQLQuery + "Select	Null As V_Type,Null As V_No,Null As V_Date,Null As V_Prefix, "
            StrSQLQuery = StrSQLQuery + "max(SG.Name)   As PName,LG.SubCode,'OPENING BALANCE' As Narration, "
            StrSQLQuery = StrSQLQuery + "(Case When (IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LG.AmtCr),0))>0 Then  "
            StrSQLQuery = StrSQLQuery + "(IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LG.AmtCr),0)) Else 0 End) As AmtDr, "
            StrSQLQuery = StrSQLQuery + "(Case When (IfNull(Sum(LG.AmtCr),0)-IfNull(Sum(LG.AmtDr),0))>0 Then "
            StrSQLQuery = StrSQLQuery + "(IfNull(Sum(LG.AmtCr),0)-IfNull(Sum(LG.AmtDr),0)) Else 0 End) As AmtCr,"
            StrSQLQuery = StrSQLQuery + "0 As SNo,max(SM.name) as Division,Null As ContraName,Null As Chq_No,Null As Chq_Date,"
            StrSQLQuery = StrSQLQuery + "IfNull(max(C.CityName),'') as PCity,Null As Site_Code "
            StrSQLQuery = StrSQLQuery + "From Ledger LG Left Join SubGroup SG On LG.Subcode=SG.SubCode "
            StrSQLQuery = StrSQLQuery + "Left Join Sitemast SM On LG.Site_Code=SM.Code "
            StrSQLQuery = StrSQLQuery + "Left Join City C On C.CityCode=SG.CityCode "
            StrSQLQuery = StrSQLQuery + StrConditionOP + StrConditionsite

            StrSQLQuery = StrSQLQuery + "Group By LG.SubCode "
            StrSQLQuery = StrSQLQuery + "Order By PName,V_Date,V_Type,V_No,SNo "


            DsRep = AgL.FillData(StrSQLQuery, AgL.GCn)

            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            ReportFrm.Text = "Daily Transaction Summary"
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = System.Reflection.MethodBase.GetCurrentMethod().Name

            ReportFrm.ProcFillGrid(DsRep)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
    Public Sub ProcTrialGroup(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try
            Dim StrCondition1 As String


            If Not FIsValid(0) Then Exit Sub

            StrCondition1 = " Where LG.V_Date <= " & AgL.Chk_Text(CDate(ReportFrm.FilterGrid(GFilter, 0).Value.ToString).ToString("s")) & " "
            If Trim(ReportFrm.FilterGrid(GFilterCode, 1).Value) <> "" Then
                StrCondition1 += " And LG.Site_Code In (" & ReportFrm.FilterGrid(GFilterCode, 1).Value & ") "
            Else
                StrCondition1 += " And LG.Site_Code In  (" & AgL.PubSiteList & ") "
            End If


            If Trim(ReportFrm.FilterGrid(GFilterCode, 2).Value) <> "" Then
                StrCondition1 += " And LG.DivCode In (" & ReportFrm.FilterGrid(GFilterCode, 2).Value & ") "
            Else
                StrCondition1 += " And LG.DivCode In  (" & AgL.PubSiteList & ") "
            End If

            '========== For Detail Section =======
            StrSQLQuery = "Select	(Case IfNull(AG1.GroupCode,'') When '' Then IfNull(AG.GroupCode,'') "
            StrSQLQuery = StrSQLQuery + "Else IfNull(AG1.GroupCode,'') End)  As GroupCode, "
            StrSQLQuery = StrSQLQuery + "Max(Case IfNull(AG1.GroupName,'') When '' Then IfNull(AG.GroupName,'') "
            StrSQLQuery = StrSQLQuery + "Else IfNull(AG1.GroupName,'') End)  As GName, "
            StrSQLQuery = StrSQLQuery + "(Case When (IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LG.AmtCr),0))>0 Then  "
            StrSQLQuery = StrSQLQuery + "(IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LG.AmtCr),0)) Else 0 End) As AmtDr, "
            StrSQLQuery = StrSQLQuery + "(Case When (IfNull(Sum(LG.AmtCr),0)-IfNull(Sum(LG.AmtDr),0))>0 Then "
            StrSQLQuery = StrSQLQuery + "(IfNull(Sum(LG.AmtCr),0)-IfNull(Sum(LG.AmtDr),0)) Else 0 End) As AmtCr "
            StrSQLQuery = StrSQLQuery + "From Ledger LG Left Join SubGroup SG On LG.SubCode=SG.SubCode  Left Join "
            StrSQLQuery = StrSQLQuery + "AcGroup AG On AG.GroupCode=SG.GroupCode Left Join "
            StrSQLQuery = StrSQLQuery + "AcGroupPath AGP On AGP.GroupCode=AG.GroupCode And AGP.SNo=1 Left Join "
            StrSQLQuery = StrSQLQuery + "AcGroup AG1 On AG1.GroupCode=AGP.GroupUnder "
            StrSQLQuery = StrSQLQuery + StrCondition1

            StrSQLQuery = StrSQLQuery + "Group By (Case IfNull(AG1.GroupCode,'') When '' Then IfNull(AG.GroupCode,'') "
            StrSQLQuery = StrSQLQuery + "Else IfNull(AG1.GroupCode,'') End) "
            StrSQLQuery = StrSQLQuery + "Having (IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LG.AmtCr),0)) <> 0 "
            StrSQLQuery = StrSQLQuery + "Order By Max(Case IfNull(AG1.GroupName,'') When '' Then IfNull(AG.GroupName,'') "
            StrSQLQuery = StrSQLQuery + "Else IfNull(AG1.GroupName,'') End) "


            DsRep = AgL.FillData(StrSQLQuery, AgL.GCn)

            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            ReportFrm.Text = "Daily Transaction Summary"
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = System.Reflection.MethodBase.GetCurrentMethod().Name

            ReportFrm.ProcFillGrid(DsRep)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
    Public Sub ProcCashBook(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try
            Dim StrCondition1 As String
            Dim StrConditionOP As String
            Dim DTTemp As DataTable
            Dim DblOpening As Double = 0
            Dim SQL As String
            Dim Pagewise As String
            Dim Withnarration As String

            If Not FIsValid(0) Then Exit Sub
            If Not FIsValid(1) Then Exit Sub
            If Not FIsValid(3) Then Exit Sub
            Pagewise = "N"
            Withnarration = "N"
            StrCondition1 = " Where (L.V_Date Between " & AgL.Chk_Text(CDate(ReportFrm.FilterGrid(GFilter, 0).Value.ToString).ToString("s")) & " And " & AgL.Chk_Text(CDate(ReportFrm.FilterGrid(GFilter, 1).Value.ToString).ToString("s")) & ") "
            If Trim(ReportFrm.FilterGrid(GFilterCode, 2).Value) <> "" Then
                StrCondition1 = StrCondition1 & " And  L.Site_Code IN (" & ReportFrm.FilterGrid(GFilterCode, 2).Value & ") "
                StrConditionOP = " And  L.Site_Code IN (" & ReportFrm.FilterGrid(GFilterCode, 2).Value & ") "
            Else
                StrCondition1 = StrCondition1 & " And  L.Site_Code IN (" & AgL.PubSiteList & ") "
                StrConditionOP = " And  L.Site_Code IN (" & AgL.PubSiteList & ") "
            End If

            If Trim(ReportFrm.FilterGrid(GFilterCode, 3).Value) <> "" Then
                StrCondition1 = StrCondition1 & " And  L.DivCode IN (" & ReportFrm.FilterGrid(GFilterCode, 3).Value & ") "
                StrConditionOP = " And  L.DivCode IN (" & ReportFrm.FilterGrid(GFilterCode, 3).Value & ") "
            Else
                StrCondition1 = StrCondition1 & " And  L.DivCode IN (" & AgL.PubDivisionList & ") "
                StrConditionOP = " And  L.DivCode IN (" & AgL.PubDivisionList & ") "
            End If

            If Trim(ReportFrm.FilterGrid(GFilterCode, 5).Value) <> "" Then
                Pagewise = Trim(ReportFrm.FilterGrid(GFilterCode, 5).Value)
            End If
            If Trim(ReportFrm.FilterGrid(GFilterCode, 6).Value) <> "" Then
                Withnarration = Trim(ReportFrm.FilterGrid(GFilterCode, 6).Value)
            End If
            SQL = "Select (IfNull(Sum(AmtCr),0)-IfNull(Sum(AmtDr),0)) As OP From Ledger L "
            SQL = SQL + "Left Join SubGroup SG On L.SubCode=SG.SubCode Where SG.Nature='Cash' "
            SQL = SQL + "And V_Date<" & AgL.Chk_Text(CDate(ReportFrm.FilterGrid(GFilter, 0).Value.ToString).ToString("s")) & " "
            SQL = SQL + "And " & " L.subcode IN ('" & ReportFrm.FilterGrid(GFilterCode, 4).Value & "') " & StrConditionOP

            DTTemp = CMain.FGetDatTable(SQL, AgL.GCn)
            If DTTemp.Rows.Count > 0 Then DblOpening = AgL.VNull(DTTemp.Rows(0).Item("OP"))
            SQL = "DECLARE @tmptb TABLE(code datetime) "
            SQL += "DECLARE @tempfromdt AS DATETIME "
            SQL += "DECLARE @temptodt AS DATETIME "
            SQL += " SET @tempfromdt=" & AgL.Chk_Text(CDate(ReportFrm.FilterGrid(GFilter, 0).Value.ToString).ToString("s"))
            SQL += " SET @temptodt=" & AgL.Chk_Text(CDate(ReportFrm.FilterGrid(GFilter, 1).Value.ToString).ToString("s"))
            SQL += " WHILE @tempfromdt<=@temptodt "
            SQL += " BEGIN "
            SQL += " INSERT INTO @tmptb VALUES (@tempfromdt) "
            SQL += " SET @tempfromdt=@tempfromdt+1 End "
            SQL += "Select IfNull(DocID,'') As DocId,Cast(IfNull(V_No,'') as Varchar) As V_no,IfNull(T.Code,'') As V_date,IfNull(Particular,'') As Particular,IfNull(AmtDr,0) As AmtDr,IfNull(AmtCr,0) As AmtCr,IfNull(V_Type,'') As V_Type,IfNull(NCat,'') As NCat,IfNull(Nature,'') As nature,IfNull(Narration,'') as Narration "
            SQL = SQL + " From @tmptb T left join "
            SQL = SQL + " (Select L.DocID,Cast(L.RecID as Varchar) As V_No,L.V_Date ,SG.[Name] As Particular,L.AmtDr , L.AmtCr,L.V_Type ,VT.NCat,SG.Nature,IfNull(L.Narration,'') as Narration "
            SQL = SQL + " From Ledger L "
            SQL = SQL + " Left Join SubGroup SG On L.SubCode=SG.SubCode "
            SQL = SQL + " Left Join Voucher_Type VT On VT.V_Type=L.V_Type "
            SQL = SQL + " Where L.subcode<>'" & ReportFrm.FilterGrid(GFilterCode, 4).Value & "' "
            SQL = SQL + " And (IfNull(L.TDSCategory,'')='' Or (IfNull(L.TDSCategory,'')<>'' And IfNull(L.System_Generated,'N')='N'))"
            SQL = SQL + " And L.DocID In ( "
            SQL = SQL + " Select L.DocID From Ledger L "
            SQL = SQL + " Left Join SubGroup SG On L.SubCode=SG.SubCode "
            SQL = SQL + " Left Join Voucher_Type VT On VT.V_Type=L.V_Type "
            SQL = SQL + StrCondition1 & " And VT.Category IN('RCT','PMT') And SG.Nature='Cash'"
            SQL = SQL + " And L.subcode IN ('" & ReportFrm.FilterGrid(GFilterCode, 4).Value & "'))"
            SQL = SQL + " Union All "
            SQL = SQL + "Select L.DocID,Cast(L.RecID as Varchar) As V_No,L.V_Date ,SG.[Name] As Particular,L.AmtCr As AmtCr,L.AmtDr As AmtDr,L.V_Type ,VT.NCat,SG.Nature,IfNull(L.Narration,'') as Narration "
            SQL = SQL + "From Ledger L "
            SQL = SQL + "Left Join SubGroup SG On L.ContraSub=SG.SubCode "
            SQL = SQL + "Left Join Voucher_Type VT On VT.V_Type=L.V_Type "
            SQL = SQL + StrCondition1 & " And VT.Category NOT IN('RCT','PMT') "
            SQL = SQL + " And L.subcode IN ('" & ReportFrm.FilterGrid(GFilterCode, 4).Value & "')"
            SQL = SQL + ") Tab on tab.v_date=t.code Order By t.code,DocId"


            DsRep = AgL.FillData(SQL, AgL.GCn)

            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            ReportFrm.Text = "Daily Transaction Summary"
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = System.Reflection.MethodBase.GetCurrentMethod().Name

            ReportFrm.ProcFillGrid(DsRep)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
    Public Sub ProcAnnexure(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try
            Dim StrCondition1 As String
            Dim DTTemp As DataTable
            Dim I As Int16
            Dim StrFieldName As String = "GroupName", StrSpace As String = "   ", StrFieldPrefix As String = ""
            Dim IntMaxHirarchy As Int16 = 10

            If Not FIsValid(0) Then Exit Sub
            If Not FIsValid(1) Then Exit Sub

            StrCondition1 = "Where LG.V_Date<=" & AgL.Chk_Text(CDate(ReportFrm.FilterGrid(GFilter, 0).Value.ToString).ToString("s")) & " "

            If Trim(ReportFrm.FilterGrid(GFilterCode, 1).Value) <> "" Then
                StrCondition1 += "And (SG.GroupCode In (" & ReportFrm.FilterGrid(GFilterCode, 1).Value & ")  "
                StrCondition1 += "Or SG.GroupCode In (Select AGP2.GroupCode From AcGroupPath AGP2 "
                StrCondition1 += "Where AGP2.GroupUnder In (" & ReportFrm.FilterGrid(GFilterCode, 1).Value & ")))  "
            End If

            If Trim(ReportFrm.FilterGrid(GFilterCode, 2).Value) <> "" Then
                StrCondition1 += " And LG.Site_Code In (" & ReportFrm.FilterGrid(GFilterCode, 2).Value & ") "
            Else
                StrCondition1 += " And LG.Site_Code In  (" & AgL.PubSiteList & ") "
            End If

            If Trim(ReportFrm.FilterGrid(GFilterCode, 3).Value) <> "" Then
                StrCondition1 += " And LG.DivCode In (" & ReportFrm.FilterGrid(GFilterCode, 3).Value & ") "
            Else
                StrCondition1 += " And LG.DivCode In  (" & AgL.PubDivisionList & ") "
            End If

            StrSQLQuery = "Select  IfNull((Select Max(AG1.GroupName) "
            StrSQLQuery += "From AcGroupPath AGP Left Join "
            StrSQLQuery += "AcGroup AG1 On AG1.GroupCode=AGP.GroupUnder And AGP.SNo=" & 1 & " "
            StrSQLQuery += "Where AGP.GroupCode=Max(SG.GroupCode)), "
            StrSQLQuery += "(Case When (Select IfNull(Max(SNo),0) From AcGroupPath AGP1 "
            StrSQLQuery += "Where AGP1.GroupCode=Max(SG.GroupCode))= " & 0 & " "
            StrSQLQuery += "Then Max(AG.GroupName) Else '' End)) As " & StrFieldName + Trim(1) & " , "

            DTTemp = CMain.FGetDatTable("Select IfNull(Max(SNo),0) From AcGroupPath", AgL.GCn)
            If DTTemp.Rows(0).Item(0) > (IntMaxHirarchy - 1) Then MsgBox("There Can Be Only " & IntMaxHirarchy - 1 & " A/c Group Levels. Levels Are Exceding.") : Exit Sub
            For I = 2 To DTTemp.Rows(0).Item(0) + 1
                StrFieldPrefix += StrSpace
                StrSQLQuery += "IfNull((Select '" & StrFieldPrefix & "' || Max(AG1.GroupName) "
                StrSQLQuery += "From AcGroupPath AGP Left Join "
                StrSQLQuery += "AcGroup AG1 On AG1.GroupCode=AGP.GroupUnder And AGP.SNo=" & I & " "
                StrSQLQuery += "Where AGP.GroupCode=Max(SG.GroupCode)), "
                StrSQLQuery += "(Case When (Select IfNull(Max(SNo),0) From AcGroupPath AGP1 "
                StrSQLQuery += "Where AGP1.GroupCode=Max(SG.GroupCode))= " & I - 1 & " "
                StrSQLQuery += "Then '" & StrFieldPrefix & "' || Max(AG.GroupName) Else '' End)) As "
                StrSQLQuery += StrFieldName + Trim(I) & " , "
            Next

            For I = DTTemp.Rows(0).Item(0) + 2 To IntMaxHirarchy
                StrSQLQuery += "' ' As " & StrFieldName + Trim(I) & " , "
            Next

            StrSQLQuery += "SG.Name, "
            StrSQLQuery += "(Case When IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LG.AmtCr),0)>0 Then "
            StrSQLQuery += "IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LG.AmtCr),0) Else 0 End) As AmtDr, "
            StrSQLQuery += "(Case When IfNull(Sum(LG.AmtCr),0)-IfNull(Sum(LG.AmtDr),0)>0 Then "
            StrSQLQuery += "IfNull(Sum(LG.AmtCr),0)-IfNull(Sum(LG.AmtDr),0) Else 0 End) As AmtCr "
            StrSQLQuery += "From "
            StrSQLQuery += "Ledger LG Left Join "
            StrSQLQuery += "SubGroup SG On LG.SubCode=SG.SubCode Left Join "
            StrSQLQuery += "AcGroup AG On AG.GroupCode=SG.GroupCode "
            StrSQLQuery += StrCondition1
            StrSQLQuery += "Group By SG.Name "
            StrSQLQuery += "Having (IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LG.AmtCr),0)) <> 0 "


            DsRep = AgL.FillData(StrSQLQuery, AgL.GCn)

            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            ReportFrm.Text = "Daily Transaction Summary"
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = System.Reflection.MethodBase.GetCurrentMethod().Name

            ReportFrm.ProcFillGrid(DsRep)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
    Public Sub ProcDayBook(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try
            Dim StrCondition1 As String


            If Not FIsValid(0) Then Exit Sub

            StrCondition1 = " Where LG.V_Date Between " & AgL.Chk_Text(CDate(ReportFrm.FilterGrid(GFilter, 0).Value.ToString).ToString("s")) & " and  " & AgL.Chk_Text(CDate(ReportFrm.FilterGrid(GFilter, 1).Value.ToString).ToString("s")) & " "

            If Trim(ReportFrm.FilterGrid(GFilterCode, 2).Value) <> "" Then StrCondition1 = StrCondition1 & " And LG.V_type In (" & ReportFrm.FilterGrid(GFilterCode, 2).Value & ")"

            If Trim(ReportFrm.FilterGrid(GFilterCode, 3).Value) <> "" Then
                StrCondition1 = StrCondition1 & " And  LG.Site_Code  IN (" & ReportFrm.FilterGrid(GFilterCode, 3).Value & ") "
            Else
                StrCondition1 = StrCondition1 & " And  LG.Site_Code  IN (" & AgL.PubSiteList & ") "
            End If

            If Trim(ReportFrm.FilterGrid(GFilterCode, 4).Value) <> "" Then
                StrCondition1 = StrCondition1 & " And  LG.DivCode  IN (" & ReportFrm.FilterGrid(GFilterCode, 4).Value & ") "
            Else
                StrCondition1 = StrCondition1 & " And  LG.DivCode  IN (" & AgL.PubDivisionList & ") "
            End If

            StrSQLQuery = "Select LG.V_date,LG.Amtcr,LG.AmtDr,LG.V_type,Cast(LG.V_No as Varchar) As V_no,LG.V_prefix as V_add,LG.Chq_No, "
            StrSQLQuery = StrSQLQuery + "LG.Chq_Date,LG.Narration As narr,LG.V_Sno,LedgerM.Narration As mnarration,LG.Docid,SG.Name As Name,St.name As SiteName,LG.Site_Code "
            StrSQLQuery = StrSQLQuery + "FROM Ledger LG LEFT  JOIN  LedgerM ON LG.DocId = LedgerM.DocId "
            StrSQLQuery = StrSQLQuery + " Left Join Subgroup SG On SG.Subcode=LG.Subcode "
            StrSQLQuery = StrSQLQuery + "Left join Voucher_type VType on Vtype.V_Type=LG.V_Type "
            StrSQLQuery = StrSQLQuery + "LEFT JOIN SiteMast ST ON LG.Site_Code =St.code"
            StrSQLQuery = StrSQLQuery + StrCondition1
            StrSQLQuery = StrSQLQuery + "Order By LG.V_DATE,LG.V_TYPE,LG.V_No,LG.V_SNO"

            DsRep = AgL.FillData(StrSQLQuery, AgL.GCn)

            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            ReportFrm.Text = "Daily Transaction Summary"
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = System.Reflection.MethodBase.GetCurrentMethod().Name

            ReportFrm.ProcFillGrid(DsRep)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
    Public Sub ProcJournal(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try
            Dim StrCondition1 As String


            If Not FIsValid(0) Then Exit Sub

            StrCondition1 = " Where LG.V_Date Between " & AgL.Chk_Text(CDate(ReportFrm.FilterGrid(GFilter, 0).Value.ToString).ToString("s")) & " and  " & AgL.Chk_Text(CDate(ReportFrm.FilterGrid(GFilter, 1).Value.ToString).ToString("s")) & " And VType.Category='JV' "
            If Trim(ReportFrm.FilterGrid(GFilterCode, 2).Value) <> "" Then StrCondition1 = StrCondition1 & " And LG.V_type In (" & ReportFrm.FilterGrid(GFilterCode, 2).Value & ")"

            If Trim(ReportFrm.FilterGrid(GFilterCode, 3).Value) <> "" Then
                StrCondition1 = StrCondition1 & "  And LG.Site_Code IN (" & ReportFrm.FilterGrid(GFilterCode, 3).Value & ") "
            Else
                StrCondition1 = StrCondition1 & " And LG.Site_Code IN (" & AgL.PubSiteList & ") "
            End If

            If Trim(ReportFrm.FilterGrid(GFilterCode, 4).Value) <> "" Then
                StrCondition1 = StrCondition1 & "  And LG.DivCode IN (" & ReportFrm.FilterGrid(GFilterCode, 4).Value & ") "
            Else
                StrCondition1 = StrCondition1 & " And LG.DivCode IN (" & AgL.PubSiteList & ") "
            End If


            StrSQLQuery = "Select LG.V_date,LG.Amtcr,LG.AmtDr,LG.V_type,Cast(LG.V_No as Varchar) As V_no,LG.V_prefix as V_add,LG.Chq_No, "
            StrSQLQuery = StrSQLQuery + "LG.Chq_Date,LG.Narration As narr,LG.V_Sno,LedgerM.Narration As mnarration,LG.Docid,SG.Name As Name,St.name As SiteName ,LG.Site_Code "
            StrSQLQuery = StrSQLQuery + "FROM Ledger LG LEFT  JOIN  LedgerM ON LG.DocId = LedgerM.DocId "
            StrSQLQuery = StrSQLQuery + "Left Join Subgroup SG On SG.Subcode=LG.Subcode "
            StrSQLQuery = StrSQLQuery + "Left join Voucher_type VType on Vtype.V_Type=LG.V_Type "
            StrSQLQuery = StrSQLQuery + "LEFT JOIN SiteMast ST ON LG.Site_Code =St.code "
            StrSQLQuery = StrSQLQuery + StrCondition1
            StrSQLQuery = StrSQLQuery + "Order By LG.V_DATE,LG.V_TYPE,LG.V_No,LG.V_SNO"

            DsRep = AgL.FillData(StrSQLQuery, AgL.GCn)

            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            ReportFrm.Text = "Daily Transaction Summary"
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = System.Reflection.MethodBase.GetCurrentMethod().Name

            ReportFrm.ProcFillGrid(DsRep)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
    Public Sub ProcBillWsOSAgeing(ByVal StrAmt1 As String, ByVal StrAmt2 As String, ByVal StrReportFor As String,
                                 Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try
            Dim StrCondition1 As String, StrCondition2, STRDATE As String
            Dim DTTemp As DataTable
            Dim StrCnd As String = ""
            Dim D1 As Integer

            If Not FIsValid(0) Then Exit Sub
            DTTemp = CMain.FGetDatTable("SELECT GroupCode FROM AcGroup WHERE GroupName='" & StrReportFor & "'", AgL.GCn)

            If DTTemp.Rows.Count > 0 Then StrCnd = AgL.XNull(DTTemp.Rows(0).Item("GroupCode")) : DTTemp.Rows.Clear()
            STRDATE = AgL.Chk_Text(CDate(ReportFrm.FilterGrid(GFilter, 0).Value.ToString).ToString("s"))
            StrCondition1 = " Where (LG.V_Date <= " & AgL.Chk_Text(CDate(ReportFrm.FilterGrid(GFilter, 0).Value.ToString).ToString("s")) & " And IfNull(LG." & StrAmt1 & ",0)>0) And (SG.GroupCode In (SELECT AGP.GroupCode FROM AcGroupPath AGP WHERE AGP.GroupUnder='" & StrCnd & "') Or SG.GroupCode='" & StrCnd & "') "
            StrCondition2 = " Where (LG.V_Date <= " & AgL.Chk_Text(CDate(ReportFrm.FilterGrid(GFilter, 0).Value.ToString).ToString("s")) & ") And IfNull(LG." & StrAmt2 & ",0)>0 And IfNull(LG." & StrAmt2 & ",0)-IfNull(T.AMOUNT,0)<>0 And (SG.GroupCode In (SELECT AGP.GroupCode FROM AcGroupPath AGP WHERE AGP.GroupUnder='" & StrCnd & "') Or SG.GroupCode='" & StrCnd & "') "

            If Trim(ReportFrm.FilterGrid(GFilterCode, 1).Value) <> "" Then StrCondition1 = StrCondition1 & " And (IfNull(SG.GroupCode,'') In (Select IfNull(AGP.GroupCode,'') From AcGroupPath AGP Where AGP.GroupUnder In (" & ReportFrm.FilterGrid(GFilterCode, 1).Value & ")) Or SG.GroupCode In (" & ReportFrm.FilterGrid(GFilterCode, 1).Value & ")) "
            If Trim(ReportFrm.FilterGrid(GFilterCode, 1).Value) <> "" Then StrCondition2 = StrCondition2 & " And (IfNull(SG.GroupCode,'') In (Select IfNull(AGP.GroupCode,'') From AcGroupPath AGP Where AGP.GroupUnder In (" & ReportFrm.FilterGrid(GFilterCode, 1).Value & ")) Or SG.GroupCode In (" & ReportFrm.FilterGrid(GFilterCode, 1).Value & ")) "

            If Trim(ReportFrm.FilterGrid(GFilterCode, 2).Value) <> "" Then StrCondition1 = StrCondition1 & " And LG.SubCode In (" & ReportFrm.FilterGrid(GFilterCode, 2).Value & ")"
            If Trim(ReportFrm.FilterGrid(GFilterCode, 2).Value) <> "" Then StrCondition2 = StrCondition2 & " And LG.SubCode In (" & ReportFrm.FilterGrid(GFilterCode, 2).Value & ")"

            If Trim(ReportFrm.FilterGrid(GFilterCode, 4).Value) <> "" Then
                StrCondition1 = StrCondition1 & " And  LG.Site_Code IN (" & ReportFrm.FilterGrid(GFilterCode, 4).Value & ") "
                StrCondition2 = StrCondition2 & " And  LG.Site_Code IN (" & ReportFrm.FilterGrid(GFilterCode, 4).Value & ") "
            Else
                StrCondition1 = StrCondition1 & " And  LG.Site_Code IN (" & AgL.PubSiteList & ") "
                StrCondition2 = StrCondition2 & " And  LG.Site_Code IN (" & AgL.PubSiteList & ") "
            End If
            D1 = Val((ReportFrm.FilterGrid(GFilter, 3).Value.ToString))

            StrSQLQuery = "Select LG.DocId,LG.V_SNo,Cast(Max(LG.V_No) as Varchar) as VNo,Max(LG.V_Type) as VType,Max(LG.V_Date) as VDate,Max(SG.Name) As PName,"
            StrSQLQuery = StrSQLQuery + "Max(LG.SubCode) as SubCode,Max(LG.Narration) as Narration,Max(LG." & StrAmt1 & ") as Amt1,0 As Amt2,IfNull(Sum(LA.Amount),0) as Amt, "
            StrSQLQuery = StrSQLQuery + "Max(SG.Add1)As Add1,Max(SG.Add2)As Add2,Max(C.CityName)As CityName,Max(CT.Name) as Country,MAx(St.name) As SiteName,max(Ag.GroupName) as AcGroupName, "
            StrSQLQuery = StrSQLQuery + "(CASE WHEN DateDiff(Day,Max(LG.V_Date), " & STRDATE & "  )>= 0 AND  DateDiff(Day,Max(LG.V_Date)," & STRDATE & " )<=" & D1 & " THEN  Max(LG.AmtDr)-IfNull(Sum(LA.Amount),0) ELSE 0 end) AS AmtDay1, "
            StrSQLQuery = StrSQLQuery + "(CASE WHEN DateDiff(Day,Max(LG.V_Date)," & STRDATE & " )>" & D1 & " THEN  Max(LG.AmtDr)-IfNull(Sum(LA.Amount),0) ELSE 0 end) AS AmtDay2," & D1 & " As Days  "
            StrSQLQuery = StrSQLQuery + "From Ledger LG Left Join SubGroup SG On LG.Subcode=SG.SubCode Left Join "
            StrSQLQuery = StrSQLQuery + "City C on SG.CityCode=C.CityCode Left Join Country CT on SG.CountryCode=CT.Code LEFT JOIN AcGroup AG ON SG.GroupCode =AG.GroupCode  "
            StrSQLQuery = StrSQLQuery + "Left Join LedgerAdj LA On LG.DocId=LA.Adj_DocID  And LG.V_SNo=LA.Adj_V_SNo "
            StrSQLQuery = StrSQLQuery + "LEFT JOIN SiteMast ST ON LG.Site_Code =St.code  "
            StrSQLQuery = StrSQLQuery + StrCondition1
            StrSQLQuery = StrSQLQuery + "Group By LG.DocId,LG.V_SNo "
            StrSQLQuery = StrSQLQuery + "HAVING(IfNull(Sum(LA.Amount), 0) <> Max(LG." & StrAmt1 & "))"
            StrSQLQuery = StrSQLQuery + "Union All "
            StrSQLQuery = StrSQLQuery + "Select	LG.DocId,LG.V_SNo,Cast(LG.V_No as Varchar) As V_No,LG.V_Type,LG.V_Date,SG.Name As PName,LG.SubCode, "
            StrSQLQuery = StrSQLQuery + "LG.Narration,0 As Amt1,IfNull(LG." & StrAmt2 & ",0)-IfNull(T.AMOUNT,0) as Amt2,0 As Amount,Null As Add1,Null As Add2,"
            StrSQLQuery = StrSQLQuery + "Null As CityName,Null As Country,ST.name As sitename,IfNull(Ag.GroupName,'') as AcGroupName,0 AS AmtDay1,0 AS AmtDay2,0 As Days "
            StrSQLQuery = StrSQLQuery + "From Ledger LG Left Join SubGroup SG On SG.SubCode=LG.SubCode LEFT JOIN AcGroup AG ON SG.GroupCode =AG.GroupCode  LEFT JOIN SiteMast ST ON LG.Site_Code =St.code   "
            StrSQLQuery = StrSQLQuery + "LEFT JOIN (SELECT LA.Vr_Docid AS Docid,LA.Vr_V_SNo AS S_No,SUM(AMOUNT) AS AMOUNT FROM LedgerAdj LA GROUP BY LA.Vr_DocId,LA.Vr_V_SNo) T ON T.DOCID=LG.DOCID AND T.S_NO=LG.V_SNO  "
            StrSQLQuery = StrSQLQuery + StrCondition2

            DsRep = AgL.FillData(StrSQLQuery, AgL.GCn)

            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            ReportFrm.Text = "Daily Transaction Summary"
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = System.Reflection.MethodBase.GetCurrentMethod().Name

            ReportFrm.ProcFillGrid(DsRep)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
    Public Sub ProcBillWsOS(ByVal StrAmt1 As String, ByVal StrAmt2 As String, ByVal StrReportFor As String,
                           Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try
            Dim StrCondition1 As String, StrCondition2 As String
            Dim DTTemp As DataTable
            Dim StrCnd As String = ""

            If Not FIsValid(0) Then Exit Sub
            DTTemp = CMain.FGetDatTable("SELECT GroupCode FROM AcGroup WHERE GroupName='" & StrReportFor & "'", AgL.GCn)

            If DTTemp.Rows.Count > 0 Then StrCnd = AgL.XNull(DTTemp.Rows(0).Item("GroupCode")) : DTTemp.Rows.Clear()
            StrCondition1 = " Where (LG.V_Date <= " & AgL.Chk_Text(CDate(ReportFrm.FilterGrid(GFilter, 0).Value.ToString).ToString("s")) & " And IfNull(LG." & StrAmt1 & ",0)>0) And (SG.GroupCode In (SELECT AGP.GroupCode FROM AcGroupPath AGP WHERE AGP.GroupUnder='" & StrCnd & "') Or SG.GroupCode='" & StrCnd & "') "
            StrCondition2 = " Where (LG.V_Date <= " & AgL.Chk_Text(CDate(ReportFrm.FilterGrid(GFilter, 0).Value.ToString).ToString("s")) & ") And IfNull(LG." & StrAmt2 & ",0)>0 And IfNull(LG." & StrAmt2 & ",0)-IfNull(T.AMOUNT,0)<>0 And (SG.GroupCode In (SELECT AGP.GroupCode FROM AcGroupPath AGP WHERE AGP.GroupUnder='" & StrCnd & "') Or SG.GroupCode='" & StrCnd & "') "

            If Trim(ReportFrm.FilterGrid(GFilterCode, 1).Value) <> "" Then StrCondition1 = StrCondition1 & " And (IfNull(SG.GroupCode,'') In (Select IfNull(AGP.GroupCode,'') From AcGroupPath AGP Where AGP.GroupUnder In (" & ReportFrm.FilterGrid(GFilterCode, 1).Value & ")) Or SG.GroupCode In (" & ReportFrm.FilterGrid(GFilterCode, 1).Value & ")) "
            If Trim(ReportFrm.FilterGrid(GFilterCode, 1).Value) <> "" Then StrCondition2 = StrCondition2 & " And (IfNull(SG.GroupCode,'') In (Select IfNull(AGP.GroupCode,'') From AcGroupPath AGP Where AGP.GroupUnder In (" & ReportFrm.FilterGrid(GFilterCode, 1).Value & ")) Or SG.GroupCode In (" & ReportFrm.FilterGrid(GFilterCode, 1).Value & ")) "

            If Trim(ReportFrm.FilterGrid(GFilterCode, 2).Value) <> "" Then StrCondition1 = StrCondition1 & " And LG.SubCode In (" & ReportFrm.FilterGrid(GFilterCode, 2).Value & ")"
            If Trim(ReportFrm.FilterGrid(GFilterCode, 2).Value) <> "" Then StrCondition2 = StrCondition2 & " And LG.SubCode In (" & ReportFrm.FilterGrid(GFilterCode, 2).Value & ")"

            If Trim(ReportFrm.FilterGrid(GFilterCode, 3).Value) <> "" Then StrCondition1 = StrCondition1 & "  AND ZM.Code In (" & ReportFrm.FilterGrid(GFilterCode, 3).Value & ")"
            If Trim(ReportFrm.FilterGrid(GFilterCode, 3).Value) <> "" Then StrCondition2 = StrCondition2 & " AND ZM.Code In (" & ReportFrm.FilterGrid(GFilterCode, 3).Value & ")"

            If Trim(ReportFrm.FilterGrid(GFilterCode, 5).Value) <> "" Then
                StrCondition1 = StrCondition1 & " And  LG.Site_Code IN (" & ReportFrm.FilterGrid(GFilterCode, 5).Value & ") "
                StrCondition2 = StrCondition2 & " And  LG.Site_Code IN (" & ReportFrm.FilterGrid(GFilterCode, 5).Value & ") "
            Else
                StrCondition1 = StrCondition1 & " And  LG.Site_Code IN (" & AgL.PubSiteList & ") "
                StrCondition2 = StrCondition2 & " And  LG.Site_Code IN (" & AgL.PubSiteList & ") "
            End If


            StrSQLQuery = "Select LG.DocId,LG.V_SNo,Cast(Max(LG.V_No)  as Varchar) as VNo,Max(LG.V_Type) as VType,Max(LG.V_Date) as VDate,Max(SG.Name) As PName,"
            StrSQLQuery = StrSQLQuery + "Max(LG.SubCode) as SubCode,Max(LG.Narration) as Narration,Max(LG." & StrAmt1 & ") as Amt1,0 As Amt2,IfNull(Sum(LA.Amount),0) as Amt, "
            StrSQLQuery = StrSQLQuery + "Max(SG.Add1)As Add1,Max(SG.Add2)As Add2,Max(C.CityName)As CityName,Max(CT.Name) as Country,MAx(St.name) As SiteName,max(Ag.GroupName) as AcGroupName,'" + Trim(ReportFrm.FilterGrid(GFilterCode, 4).Value) + "' as RepChoice  "
            StrSQLQuery = StrSQLQuery + "From Ledger LG Left Join SubGroup SG On LG.Subcode=SG.SubCode Left Join "
            StrSQLQuery = StrSQLQuery + "City C on SG.CityCode=C.CityCode Left Join Country CT on SG.CountryCode=CT.Code LEFT JOIN AcGroup AG ON SG.GroupCode =AG.GroupCode  "
            StrSQLQuery = StrSQLQuery + "Left Join LedgerAdj LA On LG.DocId=LA.Adj_DocID  And LG.V_SNo=LA.Adj_V_SNo "
            StrSQLQuery = StrSQLQuery + "LEFT JOIN SiteMast ST ON LG.Site_Code =St.code  "
            StrSQLQuery = StrSQLQuery + "LEFT JOIN Area ZM ON ZM.Code =SG.Area "
            StrSQLQuery = StrSQLQuery + StrCondition1
            StrSQLQuery = StrSQLQuery + "Group By LG.DocId,LG.V_SNo "
            StrSQLQuery = StrSQLQuery + "HAVING(IfNull(Sum(LA.Amount), 0) <> Max(LG." & StrAmt1 & "))"
            StrSQLQuery = StrSQLQuery + "Union All "
            StrSQLQuery = StrSQLQuery + "Select	LG.DocId,LG.V_SNo,Cast(LG.V_No  as Varchar) As V_No,LG.V_Type,LG.V_Date,SG.Name As PName,LG.SubCode, "
            StrSQLQuery = StrSQLQuery + "LG.Narration,0 As Amt1,IfNull(LG." & StrAmt2 & ",0)-IfNull(T.AMOUNT,0) as Amt2,0 As Amount,Null As Add1,Null As Add2,"
            StrSQLQuery = StrSQLQuery + "Null As CityName,Null As Country,ST.name As sitename,IfNull(Ag.GroupName,'') as AcGroupName,'" + Trim(ReportFrm.FilterGrid(GFilterCode, 4).Value) + "' as RepChoice  "
            StrSQLQuery = StrSQLQuery + "From Ledger LG Left Join SubGroup SG On SG.SubCode=LG.SubCode LEFT JOIN AcGroup AG ON SG.GroupCode =AG.GroupCode LEFT JOIN Area ZM ON ZM.Code =SG.Area  LEFT JOIN SiteMast ST ON LG.Site_Code =St.code   "
            StrSQLQuery = StrSQLQuery + "LEFT JOIN (SELECT LA.Vr_Docid AS Docid,LA.Vr_V_SNo AS S_No,SUM(AMOUNT) AS AMOUNT FROM LedgerAdj LA GROUP BY LA.Vr_DocId,LA.Vr_V_SNo) T ON T.DOCID=LG.DOCID AND T.S_NO=LG.V_SNO  "
            StrSQLQuery = StrSQLQuery + StrCondition2

            DsRep = AgL.FillData(StrSQLQuery, AgL.GCn)

            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            ReportFrm.Text = "Daily Transaction Summary"
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = System.Reflection.MethodBase.GetCurrentMethod().Name

            ReportFrm.ProcFillGrid(DsRep)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
    Public Sub ProcCash_Fund_Flow(ByVal IntType As Integer, Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try
            Dim StrCondition1 As String, StrConditionsite As String, reptype As String


            If Not FIsValid(0) Then Exit Sub
            If Not FIsValid(1) Then Exit Sub

            StrCondition1 = " And (Ledger.V_Date Between " & AgL.Chk_Text(CDate(ReportFrm.FilterGrid(GFilter, 0).Value.ToString).ToString("s")) & " And " & AgL.Chk_Text(CDate(ReportFrm.FilterGrid(GFilter, 1).Value.ToString).ToString("s")) & ") "

            StrConditionsite = ""
            'If Trim(ReportFrm.FilterGrid(GFilterCode, 2).Value) <> "" Then StrConditionsite = " And Ledger.site_Code In (" & ReportFrm.FilterGrid(GFilterCode, 2).Value & ") "
            If Trim(ReportFrm.FilterGrid(GFilterCode, 2).Value) <> "" Then
                StrConditionsite = StrConditionsite & " And  Ledger.Site_Code IN (" & ReportFrm.FilterGrid(GFilterCode, 2).Value & ") "
            Else
                StrConditionsite = StrConditionsite & " And  Ledger.Site_Code IN (" & AgL.PubSiteList & ") "
            End If

            If IntType = 1 Then reptype = "Cash" Else reptype = "Bank"

            StrSQLQuery = "SELECT s.*,a.* from( "
            ''1 sources of funds part (s Table
            StrSQLQuery = StrSQLQuery + "SELECT row_number() OVER (ORDER BY id) AS sno,id,groupname,sourceamt FROM ("
            ''1.1 cash bal selection (temp table
            StrSQLQuery = StrSQLQuery + "SELECT 1 AS id, '' AS type,'Cash In hand' AS groupname, (IfNull(sum(amtcr),0)-IfNull(Sum(amtdr),0)) AS sourceamt FROM Ledger "
            StrSQLQuery = StrSQLQuery + "WHERE SubCode IN (SELECT subcode FROM SubGroup WHERE Nature IN ('" & reptype & "')) "
            StrSQLQuery = StrSQLQuery + StrConditionsite
            StrSQLQuery = StrSQLQuery + StrCondition1
            StrSQLQuery = StrSQLQuery + "UNION ALL "
            ''1.2 groups for sources of funds
            StrSQLQuery = StrSQLQuery + "SELECT 2 AS id,'Sourcesoffunds' AS type,max(acgroup.GroupName ) AS groupname,"
            StrSQLQuery = StrSQLQuery + "IfNull(sum(amtcr),0) AS sourceamt FROM Ledger LEFT JOIN SubGroup ON Ledger.SubCode =subgroup.SubCode "
            StrSQLQuery = StrSQLQuery + "LEFT JOIN AcGroup ON AcGroup.GroupCode =SubGroup.GroupCode "
            StrSQLQuery = StrSQLQuery + "WHERE DocId IN "
            StrSQLQuery = StrSQLQuery + "(SELECT DISTINCT docid FROM Ledger WHERE SubCode IN ("
            StrSQLQuery = StrSQLQuery + "SELECT subcode FROM SubGroup WHERE Nature IN ('" & reptype & "'))) "
            StrSQLQuery = StrSQLQuery + "AND ledger.SubCode NOT IN (SELECT subcode FROM SubGroup WHERE Nature IN ('" & reptype & "')) "
            StrSQLQuery = StrSQLQuery + StrConditionsite
            StrSQLQuery = StrSQLQuery + StrCondition1
            StrSQLQuery = StrSQLQuery + "AND IfNull(ledger.Amtcr,0)>0 "
            StrSQLQuery = StrSQLQuery + "GROUP BY acgroup.GroupCode "
            ''1.3 just to getmax no of rows here to support left join
            StrSQLQuery = StrSQLQuery + "UNION ALL "
            StrSQLQuery = StrSQLQuery + "SELECT 2 AS id,'NA'AS type,'',0 FROM acgroup GROUP BY groupcode ) AS temp "
            StrSQLQuery = StrSQLQuery + ") s "
            StrSQLQuery = StrSQLQuery + " Left Join"
            ''2 application of funds (a Table
            StrSQLQuery = StrSQLQuery + "(SELECT row_number() OVER (ORDER BY id2) AS sno2,groupname2,appamt FROM( "
            ''2.1 selecting cash balance( Temp4 table
            StrSQLQuery = StrSQLQuery + "SELECT 1 AS id2, '' AS type,'Cash In hand' AS groupname2, (IfNull(sum(amtdr),0)-IfNull(Sum(amtcr),0)) AS appamt FROM Ledger "
            StrSQLQuery = StrSQLQuery + "WHERE SubCode IN (SELECT subcode FROM SubGroup WHERE Nature IN ('" & reptype & "')) "
            StrSQLQuery = StrSQLQuery + StrConditionsite
            StrSQLQuery = StrSQLQuery + StrCondition1
            StrSQLQuery = StrSQLQuery + "UNION all "
            ''2.2 groups for application of funds
            StrSQLQuery = StrSQLQuery + "SELECT 2 AS id2,'Applicationoffunds' AS type,max(AcGroup.GroupName) AS groupname2,"
            StrSQLQuery = StrSQLQuery + "IfNull(sum(amtdr),0) AS appamt "
            StrSQLQuery = StrSQLQuery + "FROM Ledger LEFT JOIN SubGroup ON Ledger.SubCode =subgroup.SubCode "
            StrSQLQuery = StrSQLQuery + "LEFT JOIN AcGroup ON AcGroup.GroupCode =SubGroup.GroupCode   "
            StrSQLQuery = StrSQLQuery + "WHERE DocId IN "
            StrSQLQuery = StrSQLQuery + "(SELECT DISTINCT docid FROM Ledger WHERE SubCode IN ("
            StrSQLQuery = StrSQLQuery + "SELECT subcode FROM SubGroup WHERE Nature IN ('" & reptype & "'))) "
            StrSQLQuery = StrSQLQuery + "AND ledger.SubCode NOT IN (SELECT subcode FROM SubGroup WHERE Nature IN ('" & reptype & "')) "
            StrSQLQuery = StrSQLQuery + StrConditionsite
            StrSQLQuery = StrSQLQuery + StrCondition1
            StrSQLQuery = StrSQLQuery + "AND IfNull(ledger.Amtdr,0)>0 "
            StrSQLQuery = StrSQLQuery + "GROUP BY AcGroup.GroupCode) AS temp4 "
            StrSQLQuery = StrSQLQuery + ") a ON s.sno=a.sno2 "


            DsRep = AgL.FillData(StrSQLQuery, AgL.GCn)

            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            ReportFrm.Text = "Daily Transaction Summary"
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = System.Reflection.MethodBase.GetCurrentMethod().Name

            ReportFrm.ProcFillGrid(DsRep)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
    Public Sub ProcMonthlyExpenses(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try
            Dim StrCondition1 As String
            Dim StrCondition2 As String


            If Not FIsValid(0) Then Exit Sub
            StrCondition2 = ""
            StrCondition1 = " Where SG.GroupNature ='E' "
            If Trim(ReportFrm.FilterGrid(GFilterCode, 0).Value) <> "" Then StrCondition2 = StrCondition2 & " HAVING  LEFT(convert(CHAR,max(lg.V_Date),7),3) In (" & ReportFrm.FilterGrid(GFilterCode, 0).Value & ")"

            If Trim(ReportFrm.FilterGrid(GFilterCode, 1).Value) <> "" Then StrCondition1 = StrCondition1 & " And SG.subcode In (" & ReportFrm.FilterGrid(GFilterCode, 1).Value & ")"

            If Trim(ReportFrm.FilterGrid(GFilterCode, 2).Value) <> "" Then
                StrCondition1 = StrCondition1 & " And  LG.Site_Code IN (" & ReportFrm.FilterGrid(GFilterCode, 2).Value & ") "
            Else
                StrCondition1 = StrCondition1 & " And  LG.Site_Code IN (" & AgL.PubSiteList & ") "
            End If

            If Trim(ReportFrm.FilterGrid(GFilterCode, 3).Value) <> "" Then
                StrCondition1 = StrCondition1 & " And  LG.DivCode IN (" & ReportFrm.FilterGrid(GFilterCode, 3).Value & ") "
            Else
                StrCondition1 = StrCondition1 & " And  LG.DivCode IN (" & AgL.PubDivisionList & ") "
            End If


            StrSQLQuery = "SELECT CASE WHEN (Sum(Amtdr)-Sum(Amtcr))> 0 THEN Sum(Amtdr)-Sum(Amtcr) ELSE 0 end  AS bal ,Max(SG.name) AS Party, "
            StrSQLQuery = StrSQLQuery + "LEFT(convert(CHAR,max(lg.V_Date),7),3) AS month "
            StrSQLQuery = StrSQLQuery + "FROM Ledger lg LEFT JOIN subgroup sg ON lg.SubCode =sg.SubCode  "
            StrSQLQuery = StrSQLQuery + StrCondition1
            StrSQLQuery = StrSQLQuery + "GROUP BY sg.SubCode, LEFT(convert(CHAR,(lg.V_Date),7),3)" + StrCondition2 + "Order By LEFT(convert(CHAR,max(lg.V_Date),7),3) "

            DsRep = AgL.FillData(StrSQLQuery, AgL.GCn)

            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            ReportFrm.Text = "Daily Transaction Summary"
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = System.Reflection.MethodBase.GetCurrentMethod().Name

            ReportFrm.ProcFillGrid(DsRep)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
    Public Sub ProcFIFOOutStanding_Debtors(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try
            Dim StrCondition1 As String, StrCondDt As String
            Dim StrSql As String, STRDATE As String
            Dim D1 As Integer

            If Not FIsValid(0) Then Exit Sub

            STRDATE = AgL.Chk_Text(CDate(ReportFrm.FilterGrid(GFilter, 0).Value.ToString).ToString("s"))

            StrCondition1 = " Where LG.V_Date < = " & AgL.Chk_Text(CDate(ReportFrm.FilterGrid(GFilter, 0).Value.ToString).ToString("s")) & "  "
            StrCondDt = " Where LG.V_Date < = " & AgL.Chk_Text(CDate(ReportFrm.FilterGrid(GFilter, 0).Value.ToString).ToString("s")) & "  "

            If Trim(ReportFrm.FilterGrid(GFilterCode, 1).Value) <> "" Then StrCondition1 = StrCondition1 & " And (IfNull(SG.GroupCode,'') In (Select IfNull(AGP.GroupCode,'') From AcGroupPath AGP Where AGP.GroupUnder In (" & ReportFrm.FilterGrid(GFilterCode, 1).Value & ")) Or SG.GroupCode In (" & ReportFrm.FilterGrid(GFilterCode, 1).Value & ")) "
            If Trim(ReportFrm.FilterGrid(GFilterCode, 2).Value) <> "" Then StrCondition1 = StrCondition1 & " And LG.SubCode In (" & ReportFrm.FilterGrid(GFilterCode, 2).Value & ")"

            D1 = Val((ReportFrm.FilterGrid(GFilter, 3).Value.ToString))

            If Trim(ReportFrm.FilterGrid(GFilterCode, 4).Value) <> "" Then
                StrCondition1 = StrCondition1 & " And  LG.Site_Code IN (" & ReportFrm.FilterGrid(GFilterCode, 4).Value & ") "
            Else
                StrCondition1 = StrCondition1 & " And  LG.Site_Code IN (" & AgL.PubSiteList & ") "
            End If

            StrSql = "  CREATE TEMP TABLE @TempRecord (DocId  nvarchar(20),RecId  nvarchar(20),V_Date  nvarchar(30),subcode nvarchar(30),"
            StrSql += " PartyName nvarchar(500),BillAmt FLOAT,PendingAmt FLOAT,Status  nvarchar(20),Site_Code  nvarchar(20),PartyCity  nvarchar(200),Narration  varchar(max),V_type  nvarchar(20) );	"

            mQry += " SELECT LG.SubCode,max(Sg.name) as PartyName,max(CT.CityName) as PCity,IfNull(sum(AmtCr),0) AS AmtCr,"
            mQry += " CASE WHEN IfNull(sum(AmtCr),0)> IfNull(sum(AmtDr),0) THEN (IfNull(sum(AmtCr),0) - IfNull(sum(AmtDr),0)) ELSE  0   END AS Advance ,"
            mQry += "  Max(LG.Site_Code) as SiteCode "
            mQry += " FROM Ledger LG LEFT JOIN SubGroup SG ON SG.SubCode =LG.SubCode  "
            mQry += " LEFT JOIN City CT ON SG.CityCode  =CT.CityCode "
            mQry += StrCondition1 + " and SG.Nature='Customer'"
            mQry += " GROUP BY LG.SubCode "

            Dim CurrTempPayment As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
            For I As Integer = 0 To CurrTempPayment.Rows.Count - 1
                mQry = " SELECT  IfNull(LG.DocId,'') AS DocId,Cast(IfNull(LG.V_No,'') as Varchar) AS RecId,IfNull(LG.V_date,'') AS V_date,IfNull(LG.SubCode,'') AS Subcode,"
                mQry += " IfNull(SG.Name,'') AS PartyName, IfNull(Lg.AmtDr,0) AS AmtDr,IfNull(Lg.Site_Code,0) AS Site_Code ,IfNull(Ct.CityName,'') as City,IfNull(Lg.Narration,'') as Narr,IfNull(Lg.V_type,'') as V_type  "
                mQry += " FROM Ledger LG LEFT JOIN SubGroup SG ON  SG.SubCode=LG.SubCode LEFT JOIN City CT ON Ct.CityCode =Sg.CityCode  "
                mQry += StrCondDt + " and IfNull(Lg.AmtDr,0) <>0  AND LG.SubCode = @SubCode   order by Lg.V_Date ; "

            Next



            StrSql += " SET @CrAmt=@Cr  OPEN curr_TempAdjust; "
            StrSql += " FETCH next FROM curr_TempAdjust INTO @DocId,@RecId,@V_date,@Supplier,@PartyName,@DrAmt,@Site,@City,@Narr,@VType;"
            StrSql += " WHILE @@FETCH_STATUS =0 BEGIN if   @DrAmt< @CrAmt Begin "
            StrSql += " SET @CrAmt=@CrAmt-@DrAmt End Else BEGIN  DECLARE @Status nvarchar(20);"
            StrSql += " IF  @DrAmt<> @DrAmt -@CrAmt SET  @Status='A'"
            StrSql += " INSERT INTO  @TempRecord VALUES (@DocId,@RecId,@V_date,@Supplier,@PartyName,@DrAmt,@DrAmt -@CrAmt,@Status,@Site,@City,@Narr,@VType);  "
            StrSql += " Set  @CrAmt = 0 SET @Status='' End"
            StrSql += " FETCH next FROM curr_TempAdjust INTO @DocId,@RecId,@V_date,@Supplier,@PartyName,@DrAmt,@Site,@City,@Narr,@VType;  End"
            StrSql += " CLOSE curr_TempAdjust; DEALLOCATE curr_TempAdjust;"
            StrSql += " IF   @Adv<>0  INSERT INTO  @TempRecord VALUES ('','','01/feb/1980', @SubCode,@Party,0,@Adv,'Adv',@SiteCode,@PCity,'Advance Payment ','');  "
            StrSql += " FETCH next FROM CurrTempPayment INTO @SubCode,@Party,@PCity,@Cr,@Adv,@SiteCode ; End"
            StrSql += " CLOSE CurrTempPayment;DEALLOCATE CurrTempPayment;	"
            StrSql += " SELECT *, "
            StrSql += " (CASE WHEN DateDiff(Day,V_Date," & STRDATE & "  )>= 0 AND  DateDiff(Day,V_Date," & STRDATE & " )<=" & D1 & " THEN  PendingAmt Else 0 end ) AS AmtDay1, "
            StrSql += " (CASE WHEN DateDiff(Day,V_Date," & STRDATE & " )>" & D1 & " THEN  PendingAmt ELSE 0 end) AS AmtDay2, " & D1 & " As Days "
            StrSql += " FROM @TempRecord where IfNull(PendingAmt,0)<>0  "



            'StrSql = "  CREATE TEMP TABLE @TempRecord (DocId  nvarchar(20),RecId  nvarchar(20),V_Date  nvarchar(30),subcode nvarchar(30),"
            'StrSql += " PartyName nvarchar(500),BillAmt FLOAT,PendingAmt FLOAT,Status  nvarchar(20),Site_Code  nvarchar(20),PartyCity  nvarchar(200),Narration  varchar(max),V_type  nvarchar(20) );	"
            'StrSql += " DECLARE @SubCode VARCHAR(100);DECLARE @Party VARCHAR(200);DECLARE @PCity VARCHAR(200);"
            'StrSql += " DECLARE @Cr float;DECLARE @Adv float;DECLARE @SiteCode VARCHAR(100)"
            'StrSql += " DECLARE CurrTempPayment CURSOR FOR  SELECT LG.SubCode,max(Sg.name) as PartyName,max(CT.CityName) as PCity,IfNull(sum(AmtCr),0) AS AmtCr,"
            'StrSql += " CASE WHEN IfNull(sum(AmtCr),0)> IfNull(sum(AmtDr),0) THEN (IfNull(sum(AmtCr),0) - IfNull(sum(AmtDr),0)) ELSE  0   END AS Advance ,"
            'StrSql += "  Max(LG.Site_Code) as SiteCode "
            'StrSql += " FROM Ledger LG LEFT JOIN SubGroup SG ON SG.SubCode =LG.SubCode  "
            'StrSql += " LEFT JOIN City CT ON SG.CityCode  =CT.CityCode "
            'StrSql += StrCondition1 + " and SG.Nature='Customer'"
            'StrSql += " GROUP BY LG.SubCode "
            'StrSql += " OPEN CurrTempPayment; "
            'StrSql += " FETCH next FROM CurrTempPayment INTO @SubCode,@Party,@PCity,@Cr,@Adv,@SiteCode ;"
            'StrSql += " WHILE @@FETCH_STATUS =0 "
            'StrSql += " BEGIN  DECLARE @CrAmt float; DECLARE @tempval float; "
            'StrSql += " DECLARE @DocId nvarchar(20);DECLARE @RecId nvarchar(20);"
            'StrSql += " DECLARE @V_date nvarchar(20);DECLARE @Supplier nvarchar(20);DECLARE @PartyName nvarchar(300);DECLARE @DrAmt float;"
            'StrSql += " DECLARE @Site nvarchar(30);DECLARE @City nvarchar(100);DECLARE @Narr varchar(max);DECLARE @VType nvarchar(1000);"
            'StrSql += " SET @tempval=0;  "
            'StrSql += " DECLARE curr_TempAdjust CURSOR FOR SELECT  IfNull(LG.DocId,'') AS DocId,Cast(IfNull(LG.V_No,'') as Varchar) AS RecId,IfNull(LG.V_date,'') AS V_date,IfNull(LG.SubCode,'') AS Subcode,"
            'StrSql += " IfNull(SG.Name,'') AS PartyName, IfNull(Lg.AmtDr,0) AS AmtDr,IfNull(Lg.Site_Code,0) AS Site_Code ,IfNull(Ct.CityName,'') as City,IfNull(Lg.Narration,'') as Narr,IfNull(Lg.V_type,'') as V_type  "
            'StrSql += " FROM Ledger LG LEFT JOIN SubGroup SG ON  SG.SubCode=LG.SubCode LEFT JOIN City CT ON Ct.CityCode =Sg.CityCode  "
            'StrSql += StrCondDt + " and IfNull(Lg.AmtDr,0) <>0  AND LG.SubCode = @SubCode   order by Lg.V_Date ; "
            'StrSql += " SET @CrAmt=@Cr  OPEN curr_TempAdjust; "
            'StrSql += " FETCH next FROM curr_TempAdjust INTO @DocId,@RecId,@V_date,@Supplier,@PartyName,@DrAmt,@Site,@City,@Narr,@VType;"
            'StrSql += " WHILE @@FETCH_STATUS =0 BEGIN if   @DrAmt< @CrAmt Begin "
            'StrSql += " SET @CrAmt=@CrAmt-@DrAmt End Else BEGIN  DECLARE @Status nvarchar(20);"
            'StrSql += " IF  @DrAmt<> @DrAmt -@CrAmt SET  @Status='A'"
            'StrSql += " INSERT INTO  @TempRecord VALUES (@DocId,@RecId,@V_date,@Supplier,@PartyName,@DrAmt,@DrAmt -@CrAmt,@Status,@Site,@City,@Narr,@VType);  "
            'StrSql += " Set  @CrAmt = 0 SET @Status='' End"
            'StrSql += " FETCH next FROM curr_TempAdjust INTO @DocId,@RecId,@V_date,@Supplier,@PartyName,@DrAmt,@Site,@City,@Narr,@VType;  End"
            'StrSql += " CLOSE curr_TempAdjust; DEALLOCATE curr_TempAdjust;"
            'StrSql += " IF   @Adv<>0  INSERT INTO  @TempRecord VALUES ('','','01/feb/1980', @SubCode,@Party,0,@Adv,'Adv',@SiteCode,@PCity,'Advance Payment ','');  "
            'StrSql += " FETCH next FROM CurrTempPayment INTO @SubCode,@Party,@PCity,@Cr,@Adv,@SiteCode ; End"
            'StrSql += " CLOSE CurrTempPayment;DEALLOCATE CurrTempPayment;	"
            'StrSql += " SELECT *, "
            'StrSql += " (CASE WHEN DateDiff(Day,V_Date," & STRDATE & "  )>= 0 AND  DateDiff(Day,V_Date," & STRDATE & " )<=" & D1 & " THEN  PendingAmt Else 0 end ) AS AmtDay1, "
            'StrSql += " (CASE WHEN DateDiff(Day,V_Date," & STRDATE & " )>" & D1 & " THEN  PendingAmt ELSE 0 end) AS AmtDay2, " & D1 & " As Days "
            'StrSql += " FROM @TempRecord where IfNull(PendingAmt,0)<>0  "


            DsRep = AgL.FillData(StrSql, AgL.GCn)

            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            ReportFrm.Text = "Outstanding Debtors FIFO"
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = System.Reflection.MethodBase.GetCurrentMethod().Name

            ReportFrm.ProcFillGrid(DsRep)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
    Public Sub ProcFIFOOutStanding_Creditors(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try
            Dim StrCondition1 As String, StrCondDt As String
            Dim StrSql As String, STRDATE As String

            Dim StrCnd As String = ""
            Dim D1 As Integer
            If Not FIsValid(0) Then Exit Sub
            If Not FIsValid(1) Then Exit Sub

            STRDATE = AgL.Chk_Text(CDate(ReportFrm.FilterGrid(GFilter, 0).Value.ToString).ToString("s"))

            StrCondition1 = " Where LG.V_Date < = " & AgL.Chk_Text(CDate(ReportFrm.FilterGrid(GFilter, 0).Value.ToString).ToString("s")) & "  "
            StrCondDt = " Where LG.V_Date < = " & AgL.Chk_Text(CDate(ReportFrm.FilterGrid(GFilter, 0).Value.ToString).ToString("s")) & "  "

            If Trim(ReportFrm.FilterGrid(GFilterCode, 1).Value) <> "" Then StrCondition1 = StrCondition1 & " And (IfNull(SG.GroupCode,'') In (Select IfNull(AGP.GroupCode,'') From AcGroupPath AGP Where AGP.GroupUnder In (" & ReportFrm.FilterGrid(GFilterCode, 1).Value & ")) Or SG.GroupCode In (" & ReportFrm.FilterGrid(GFilterCode, 1).Value & ")) "
            If Trim(ReportFrm.FilterGrid(GFilterCode, 2).Value) <> "" Then StrCondition1 = StrCondition1 & " And LG.SubCode In (" & ReportFrm.FilterGrid(GFilterCode, 2).Value & ")"

            D1 = Val((ReportFrm.FilterGrid(GFilter, 3).Value.ToString))

            If Trim(ReportFrm.FilterGrid(GFilterCode, 4).Value) <> "" Then
                StrCondition1 = StrCondition1 & " And  LG.Site_Code IN (" & ReportFrm.FilterGrid(GFilterCode, 4).Value & ") "
            Else
                StrCondition1 = StrCondition1 & " And  LG.Site_Code IN (" & AgL.PubSiteList & ") "
            End If

            StrSql = " DECLARE @TempRecord TABLE (DocId  nvarchar(20),RecId  nvarchar(20),V_Date  nvarchar(30),subcode nvarchar(30),"
            StrSql += " PartyName nvarchar(500),BillAmt FLOAT,PendingAmt FLOAT,Status  nvarchar(20),Site_Code  nvarchar(20),PartyCity  nvarchar(200),Narration  varchar(max),V_type  nvarchar(20));	"
            StrSql += " DECLARE @SubCode VARCHAR(100);DECLARE @Party VARCHAR(200);DECLARE @PCity VARCHAR(200);"
            StrSql += " DECLARE @Dr float;DECLARE @Adv float;DECLARE @SiteCode VARCHAR(100)"
            StrSql += " DECLARE CurrTempPayment CURSOR FOR  SELECT LG.SubCode,max(Sg.name) as PartyName,max(CT.CityName) as PCity,IfNull(sum(AmtDr),0) AS AmtDr,"
            StrSql += " CASE WHEN IfNull(sum(AmtDr),0)> IfNull(sum(AmtCr),0) THEN (IfNull(sum(AmtDr),0) - IfNull(sum(AmtCr),0)) ELSE  0   END AS Advance ,Max(LG.Site_Code) as SiteCode "
            StrSql += " FROM Ledger LG LEFT JOIN SubGroup SG ON SG.SubCode =LG.SubCode  "
            StrSql += " LEFT JOIN City CT ON SG.CityCode  =CT.CityCode "
            StrSql += StrCondition1 + " and SG.Nature='Supplier'"
            StrSql += " GROUP BY LG.SubCode "
            StrSql += " OPEN CurrTempPayment; "
            StrSql += " FETCH next FROM CurrTempPayment INTO @SubCode,@Party,@PCity,@Dr,@Adv,@SiteCode ;"
            StrSql += " WHILE @@FETCH_STATUS =0 "
            StrSql += " BEGIN  DECLARE @DrAmt float; DECLARE @tempval float; "
            StrSql += " DECLARE @DocId nvarchar(20);DECLARE @RecId nvarchar(20);"
            StrSql += " DECLARE @V_date nvarchar(20);DECLARE @Supplier nvarchar(20);DECLARE @PartyName nvarchar(300);DECLARE @CrAmt float;"
            StrSql += " DECLARE @Site nvarchar(30);DECLARE @City nvarchar(100);DECLARE @Narr varchar(max);DECLARE @VType nvarchar(1000);"
            StrSql += " SET @tempval=0;  "
            StrSql += " DECLARE curr_TempAdjust CURSOR FOR SELECT  IfNull(LG.DocId,'') AS DocId,Cast(IfNull(LG.V_No,'') as Varchar) AS RecId,IfNull(LG.V_date,'') AS V_date,IfNull(LG.SubCode,'') AS Subcode,"
            StrSql += " IfNull(SG.Name,'') AS PartyName, IfNull(Lg.AmtCr,0) AS AmtCr,IfNull(Lg.Site_Code,0) AS Site_Code ,IfNull(Ct.CityName,'') as City,IfNull(Lg.Narration,'') as Narr,IfNull(Lg.V_type,'') as V_type  "
            StrSql += " FROM Ledger LG LEFT JOIN SubGroup SG ON  SG.SubCode=LG.SubCode LEFT JOIN City CT ON Ct.CityCode =Sg.CityCode  "
            StrSql += StrCondDt + " and IfNull(Lg.AmtCr,0) <>0  AND LG.SubCode = @SubCode   order by Lg.V_Date ; "
            StrSql += " SET @DrAmt=@Dr  OPEN curr_TempAdjust; "
            StrSql += " FETCH next FROM curr_TempAdjust INTO @DocId,@RecId,@V_date,@Supplier,@PartyName,@CrAmt,@Site,@City,@Narr,@VType;"
            StrSql += " WHILE @@FETCH_STATUS =0 BEGIN if   @CrAmt< @DrAmt Begin "
            StrSql += " SET @DrAmt=@DrAmt-@CrAmt End Else BEGIN  DECLARE @Status nvarchar(20);"
            StrSql += " IF  @CrAmt<> @CrAmt -@DrAmt SET  @Status='A'"
            StrSql += " INSERT INTO  @TempRecord VALUES (@DocId,@RecId,@V_date,@Supplier,@PartyName,@CrAmt,@CrAmt -@DrAmt,@Status,@Site,@City,@Narr,@VType);  "
            StrSql += " Set  @DrAmt = 0 SET @Status='' End"
            StrSql += " FETCH next FROM curr_TempAdjust INTO @DocId,@RecId,@V_date,@Supplier,@PartyName,@CrAmt,@Site,@City,@Narr,@VType;  End"
            StrSql += " CLOSE curr_TempAdjust; DEALLOCATE curr_TempAdjust;"
            StrSql += " IF   @Adv<>0  INSERT INTO  @TempRecord VALUES ('','','01/feb/1980', @SubCode,@Party,0,@Adv,'Adv',@SiteCode,@PCity,'Advance Payment ','');  "
            StrSql += " FETCH next FROM CurrTempPayment INTO @SubCode,@Party,@PCity,@Dr,@Adv,@SiteCode ; End "
            StrSql += " CLOSE CurrTempPayment;DEALLOCATE CurrTempPayment;	"
            StrSql += " SELECT *,"
            StrSql += "(CASE WHEN DateDiff(Day,V_Date," & STRDATE & "  )>= 0 AND  DateDiff(Day,V_Date," & STRDATE & " )<=" & D1 & " THEN  PendingAmt Else 0 end ) AS AmtDay1, "
            StrSql += "(CASE WHEN DateDiff(Day,V_Date," & STRDATE & " )>" & D1 & " THEN  PendingAmt ELSE 0 end) AS AmtDay2, " & D1 & " As Days "
            StrSql += "FROM @TempRecord where IfNull(PendingAmt,0)<>0  "


            DsRep = AgL.FillData(StrSql, AgL.GCn)

            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            ReportFrm.Text = "Outstanding Debtors FIFO"
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = System.Reflection.MethodBase.GetCurrentMethod().Name

            ReportFrm.ProcFillGrid(DsRep)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub

    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Public Sub ProcStockValuation(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try
            Dim StrCondition As String
            Dim StrConditionOP As String

            Dim StrSQL As String
            Dim StrValueField As String

            If Not FIsValid(0) Then Exit Sub
            If Not FIsValid(1) Then Exit Sub
            If Not FIsValid(2) Then Exit Sub
            If Not FIsValid(3) Then Exit Sub
            If Not FIsValid(4) Then Exit Sub
            If Not FIsValid(5) Then Exit Sub
            If Not FIsValid(6) Then Exit Sub
            If Not FIsValid(7) Then Exit Sub

            StrCondition = " Where (ST.V_Date Between " & AgL.Chk_Text(CDate(ReportFrm.FilterGrid(GFilter, 0).Value.ToString).ToString("s")) & " And " & AgL.Chk_Text(CDate(ReportFrm.FilterGrid(GFilter, 1).Value.ToString).ToString("s")) & ") "
            StrConditionOP = " Where ST.V_Date < " & AgL.Chk_Text(CDate(ReportFrm.FilterGrid(GFilter, 0).Value.ToString).ToString("s")) & " "

            If Trim(ReportFrm.FilterGrid(GFilterCode, 2).Value) <> "" Then StrCondition += " And IM.ItemType In (" & ReportFrm.FilterGrid(GFilterCode, 2).Value & ") "
            If Trim(ReportFrm.FilterGrid(GFilterCode, 2).Value) <> "" Then StrConditionOP += " And IM.ItemType In (" & ReportFrm.FilterGrid(GFilterCode, 2).Value & ") "

            If Trim(ReportFrm.FilterGrid(GFilterCode, 3).Value) <> "" Then StrCondition += " And IG.CatCode In (" & ReportFrm.FilterGrid(GFilterCode, 3).Value & ") "
            If Trim(ReportFrm.FilterGrid(GFilterCode, 3).Value) <> "" Then StrConditionOP += " And IG.CatCode In (" & ReportFrm.FilterGrid(GFilterCode, 3).Value & ") "

            If Trim(ReportFrm.FilterGrid(GFilterCode, 4).Value) <> "" Then StrCondition += " And IG.Code In (" & ReportFrm.FilterGrid(GFilterCode, 4).Value & ") "
            If Trim(ReportFrm.FilterGrid(GFilterCode, 4).Value) <> "" Then StrConditionOP += " And IG.Code In (" & ReportFrm.FilterGrid(GFilterCode, 4).Value & ") "

            If Trim(ReportFrm.FilterGrid(GFilterCode, 5).Value) <> "" Then StrCondition += " And ST.Item In (" & ReportFrm.FilterGrid(GFilterCode, 5).Value & ") "
            If Trim(ReportFrm.FilterGrid(GFilterCode, 5).Value) <> "" Then StrConditionOP += " And ST.Item In (" & ReportFrm.FilterGrid(GFilterCode, 5).Value & ") "

            If UCase(Trim(ReportFrm.FilterGrid(GFilterCode, 7).Value)) = "WA" Then
                StrValueField = "ST.AverageValue"
            Else
                StrValueField = "ST.FifoValue"
            End If

            StrSQL = "Select	'OPENING' As RecId,Null As DocId,Null As V_Type,Null As V_Date,ST.Item,"
            StrSQL += "Max(IM.Description) As ItemName,Max(IM.Unit) As Unit, "
            StrSQL += "(IfNull(Sum(ST.Qty_Rec),0) - IfNull(Sum(ST.Qty_Iss),0)) As OPQty, "
            StrSQL += "(IfNull(Sum((Case When IfNull(ST.Qty_Rec,0)<> 0 Then " & StrValueField & " Else 0 End)),0) -  "
            StrSQL += "IfNull(Sum((Case	When IfNull(ST.Qty_Iss,0) <> 0 Then " & StrValueField & " Else 0 End)),0)) As OPValue, "
            StrSQL += "0 As RQty,0 As RValue,0 As IQty,0 As IValue, "
            StrSQL += "0 As SNo,0 As SerialNo "
            StrSQL += "From Stock ST "
            StrSQL += "Left Join Item IM On ST.Item=IM.Code "
            StrSQL += StrConditionOP
            StrSQL += "Group By Item "
            '=========================================================
            '================= For Transaction Stock =================
            '=========================================================
            StrSQL += "Union All "
            StrSQL += "Select Cast(ST.V_No as Varchar) as V_No,ST.DocId,ST.V_Type As V_Type,ST.V_Date,ST.Item, "
            StrSQL += "IfNull(IM.Description,'') As ItemName,IfNull(IM.Unit,'') As Unit, "
            StrSQL += "0 As OpQty,0 As OPValue, "
            StrSQL += "IfNull(ST.Qty_Rec,0) As RQty, "
            StrSQL += "(Case When IfNull(ST.Qty_Rec,0)<> 0 Then IfNull(" & StrValueField & ",0) Else 0 End) As RVal, "
            StrSQL += "IfNull(ST.Qty_Iss,0) As IQty, "
            StrSQL += "(Case When  IfNull(ST.Qty_Iss,0) <> 0 Then IfNull(" & StrValueField & ",0) Else 0 End) As IVal, "
            StrSQL += "1 As SNo,IfNull(VT.SerialNo,0) As SerialNo "
            StrSQL += "From Stock ST "
            StrSQL += "Left Join Item IM On ST.Item=IM.Code "
            StrSQL += "Left Join Voucher_Type VT On VT.V_Type=ST.V_Type "
            StrSQL += StrCondition
            StrSQL += "Order By Item,V_Date,SNo,SerialNo,RecId "


            DsRep = AgL.FillData(StrSQL, AgL.GCn)

            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            ReportFrm.Text = "Outstanding Debtors FIFO"
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = System.Reflection.MethodBase.GetCurrentMethod().Name

            ReportFrm.ProcFillGrid(DsRep)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
    Public Sub ProcDailyExpenseReg(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try
            Dim StrCondition1 As String, StrConditionsite As String


            If Not FIsValid(0) Then Exit Sub
            If Not FIsValid(1) Then Exit Sub

            StrCondition1 = " Where (LG.V_Date Between " & AgL.Chk_Text(CDate(ReportFrm.FilterGrid(GFilter, 0).Value.ToString).ToString("s")) & " And " & AgL.Chk_Text(CDate(ReportFrm.FilterGrid(GFilter, 1).Value.ToString).ToString("s")) & ") "
            StrConditionsite = ""

            If Trim(ReportFrm.FilterGrid(GFilterCode, 2).Value) <> "" Then StrCondition1 = StrCondition1 & " And (IfNull(SG.GroupCode,'') In (Select IfNull(AGP.GroupCode,'') From AcGroupPath AGP Where AGP.GroupUnder In (" & ReportFrm.FilterGrid(GFilterCode, 2).Value & ")) Or SG.GroupCode In (" & ReportFrm.FilterGrid(GFilterCode, 2).Value & ")) "
            If Trim(ReportFrm.FilterGrid(GFilterCode, 3).Value) <> "" Then
                StrConditionsite = " and LG.site_Code In (" & ReportFrm.FilterGrid(GFilterCode, 3).Value & ") "
            Else
                StrConditionsite = " and LG.site_Code In  (" & AgL.PubSiteList & ") "
            End If

            StrSQLQuery = "Select	LG.V_Type,Cast(LG.V_No as Varchar) As V_No,LG.V_Date,LG.V_Prefix,SG.Name  As PName,LG.SubCode,LG.Narration, "
            StrSQLQuery = StrSQLQuery + "LG.AmtDr,1 As SNo,LG.Chq_No,LG.Chq_Date,"
            StrSQLQuery = StrSQLQuery + "IfNull(C.CityName,'') as PCity,IfNull(LG.Site_Code,'') As Site_Code,AG.GroupName "
            StrSQLQuery = StrSQLQuery + "From Ledger LG Left Join SubGroup SG On LG.Subcode=SG.SubCode "
            StrSQLQuery = StrSQLQuery + "LEFT JOIN AcGroup AG ON AG.GroupCode=SG.GroupCode "
            StrSQLQuery = StrSQLQuery + "Left Join Sitemast SM On LG.Site_Code=SM.Code "
            StrSQLQuery = StrSQLQuery + "Left Join City C On C.CityCode=SG.CityCode "

            StrSQLQuery = StrSQLQuery + StrCondition1 + StrConditionsite + " And LG.V_Type IN ('PMT','CPV') AND LG.AmtDr>0 "
            StrSQLQuery = StrSQLQuery + "Order By V_Date,V_No,PName,SNo "



            DsRep = AgL.FillData(StrSQLQuery, AgL.GCn)

            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            ReportFrm.Text = "Outstanding Debtors FIFO"
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = System.Reflection.MethodBase.GetCurrentMethod().Name

            ReportFrm.ProcFillGrid(DsRep)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
    Public Sub ProcDailyCollectionReg(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try
            Dim StrCondition1 As String, StrConditionsite As String


            If Not FIsValid(0) Then Exit Sub
            If Not FIsValid(1) Then Exit Sub

            StrCondition1 = " Where (LG.V_Date Between " & AgL.Chk_Text(CDate(ReportFrm.FilterGrid(GFilter, 0).Value.ToString).ToString("s")) & " And " & AgL.Chk_Text(CDate(ReportFrm.FilterGrid(GFilter, 1).Value.ToString).ToString("s")) & ") "
            StrConditionsite = ""

            If Trim(ReportFrm.FilterGrid(GFilterCode, 2).Value) <> "" Then StrCondition1 = StrCondition1 & " And (IfNull(SG.GroupCode,'') In (Select IfNull(AGP.GroupCode,'') From AcGroupPath AGP Where AGP.GroupUnder In (" & ReportFrm.FilterGrid(GFilterCode, 2).Value & ")) Or SG.GroupCode In (" & ReportFrm.FilterGrid(GFilterCode, 2).Value & ")) "

            If Trim(ReportFrm.FilterGrid(GFilterCode, 3).Value) <> "" Then
                StrConditionsite = " and LG.site_Code In (" & ReportFrm.FilterGrid(GFilterCode, 3).Value & ") "
            Else
                StrConditionsite = " and LG.site_Code In  (" & AgL.PubSiteList & ") "
            End If

            StrSQLQuery = "Select	LG.V_Type,Cast(LG.V_No as Varchar) As V_No,LG.V_Date,LG.V_Prefix,SG.Name  As PName,LG.SubCode,LG.Narration, "
            StrSQLQuery = StrSQLQuery + "LG.AmtCr,1 As SNo,LG.Chq_No,LG.Chq_Date,"
            StrSQLQuery = StrSQLQuery + "IfNull(C.CityName,'') as PCity,IfNull(LG.Site_Code,'') As Site_Code,AG.GroupName "
            StrSQLQuery = StrSQLQuery + "From Ledger LG Left Join SubGroup SG On LG.Subcode=SG.SubCode "
            StrSQLQuery = StrSQLQuery + "LEFT JOIN AcGroup AG ON AG.GroupCode=SG.GroupCode "
            StrSQLQuery = StrSQLQuery + "Left Join Sitemast SM On LG.Site_Code=SM.Code "
            StrSQLQuery = StrSQLQuery + "Left Join City C On C.CityCode=SG.CityCode "

            StrSQLQuery = StrSQLQuery + StrCondition1 + StrConditionsite + " And LG.V_Type IN ('RCT','CRV') AND LG.AmtCr>0 "
            StrSQLQuery = StrSQLQuery + "Order By V_Date,V_No,PName,SNo "


            DsRep = AgL.FillData(StrSQLQuery, AgL.GCn)

            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            ReportFrm.Text = "Outstanding Debtors FIFO"
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = System.Reflection.MethodBase.GetCurrentMethod().Name

            ReportFrm.ProcFillGrid(DsRep)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
    Public Sub ProcLedgerGrMergeLedger(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try
            Dim StrCondition1 As String, StrConditionOP As String, StrConditionsite As String

            Dim I As Integer

            If Not FIsValid(0) Then Exit Sub
            If Not FIsValid(1) Then Exit Sub

            StrCondition1 = " Where (LG.V_Date Between " & AgL.Chk_Text(CDate(ReportFrm.FilterGrid(GFilter, 0).Value.ToString)).ToString("s") & " And " & AgL.Chk_Text(CDate(ReportFrm.FilterGrid(GFilter, 1).Value.ToString).ToString("s")) & ") "
            StrConditionOP = " Where LG.V_Date < " & AgL.Chk_Text(CDate(ReportFrm.FilterGrid(GFilter, 0).Value.ToString).ToString("s")) & " "

            StrConditionsite = ""
            If Trim(ReportFrm.FilterGrid(GFilterCode, 2).Value) <> "" Then StrCondition1 = StrCondition1 & " And LGG.Code In (" & ReportFrm.FilterGrid(GFilterCode, 2).Value & ")"
            If Trim(ReportFrm.FilterGrid(GFilterCode, 2).Value) <> "" Then StrConditionOP = StrConditionOP & " And LGG.Code In (" & ReportFrm.FilterGrid(GFilterCode, 2).Value & ")"

            If Trim(ReportFrm.FilterGrid(GFilterCode, 3).Value) <> "" Then
                StrConditionsite = " And LG.Site_Code In (" & ReportFrm.FilterGrid(GFilterCode, 3).Value & ") "
            Else
                StrConditionsite = " And LG.Site_Code In  (" & AgL.PubSiteList & ") "
            End If

            '========== For Detail Section =======
            StrSQLQuery = "Select LG.V_Type,Cast(LG.V_No as Varchar) As V_No,LG.V_Date,LG.V_Prefix,SG.Name As PName,LG.SubCode,LG.Narration, "
            StrSQLQuery = StrSQLQuery + "LG.AmtDr,LG.AmtCr,1 As SNo,SM.Name As Division,LG.ContraText As ContraName,LG.Chq_No,LG.Chq_Date,"
            StrSQLQuery = StrSQLQuery + "IfNull(LG.Site_Code,'') As Site_Code,IfNull(LGG.Name,'') As LedgerGr,LGG.Code As Code "
            StrSQLQuery = StrSQLQuery + "From LedgerGroup LGG Left Join SubGroup SG On LGG.Code = SG.LedgerGroup "
            StrSQLQuery = StrSQLQuery + "Left Join Ledger LG ON LG.SubCode = SG.SubCode "
            StrSQLQuery = StrSQLQuery + "Left Join Sitemast SM On LG.Site_Code = SM.Code "

            StrSQLQuery = StrSQLQuery + StrCondition1 + StrConditionsite + "AND IfNull(LGG.Code,'')<>'' "
            StrSQLQuery = StrSQLQuery + "Union All "

            '======= For Opening Balance =========
            StrSQLQuery = StrSQLQuery + "Select	Null As V_Type,Null As V_No,Null As V_Date,Null As V_Prefix, "
            StrSQLQuery = StrSQLQuery + "Max(SG.Name) As PName,Max(LG.SubCode) As SubCode,'OPENING BALANCE' As Narration, "
            StrSQLQuery = StrSQLQuery + "(Case When (IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LG.AmtCr),0))>0 Then  "
            StrSQLQuery = StrSQLQuery + "(IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LG.AmtCr),0)) Else 0 End) As AmtDr, "
            StrSQLQuery = StrSQLQuery + "(Case When (IfNull(Sum(LG.AmtCr),0)-IfNull(Sum(LG.AmtDr),0))>0 Then "
            StrSQLQuery = StrSQLQuery + "(IfNull(Sum(LG.AmtCr),0)-IfNull(Sum(LG.AmtDr),0)) Else 0 End) As AmtCr,"
            StrSQLQuery = StrSQLQuery + "0 As SNo,max(SM.name) As Division,Null As ContraName,Null As Chq_No,Null As Chq_Date,"
            StrSQLQuery = StrSQLQuery + "Null As Site_Code,Max(IfNull(LGG.Name,'')) As LedgerGr,Max(LGG.Code) As Code "
            StrSQLQuery = StrSQLQuery + "From LedgerGroup LGG Left Join SubGroup SG On LGG.Code = SG.LedgerGroup "
            StrSQLQuery = StrSQLQuery + "Left Join Ledger LG ON LG.SubCode = SG.SubCode "
            StrSQLQuery = StrSQLQuery + "Left Join Sitemast SM On LG.Site_Code = SM.Code "

            StrSQLQuery = StrSQLQuery + StrConditionOP + StrConditionsite + "AND IfNull(LGG.Code,'')<>'' "

            StrSQLQuery = StrSQLQuery + "Group By LGG.Code "
            StrSQLQuery = StrSQLQuery + "Order By LedgerGr,V_Date,V_Type,V_No,SNo "

            DsRep = AgL.FillData(StrSQLQuery, AgL.GCn)

            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            ReportFrm.Text = "Outstanding Debtors FIFO"
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = System.Reflection.MethodBase.GetCurrentMethod().Name

            ReportFrm.ProcFillGrid(DsRep)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
    Public Sub ProcAccountGrMergeLedger(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try
            Dim StrCondition1 As String, StrConditionOP As String, StrConditionsite As String

            Dim I As Integer

            If Not FIsValid(0) Then Exit Sub
            If Not FIsValid(1) Then Exit Sub

            StrCondition1 = " Where (LG.V_Date Between " & AgL.Chk_Text(CDate(ReportFrm.FilterGrid(GFilter, 0).Value.ToString).ToString("s")) & " And " & AgL.Chk_Text(CDate(ReportFrm.FilterGrid(GFilter, 1).Value.ToString).ToString("s")) & ") "
            StrConditionOP = " Where LG.V_Date < " & AgL.Chk_Text(CDate(ReportFrm.FilterGrid(GFilter, 0).Value.ToString).ToString("s")) & " "

            StrConditionsite = ""
            If Trim(ReportFrm.FilterGrid(GFilterCode, 2).Value) <> "" Then StrCondition1 = StrCondition1 & " And (IfNull(SG.GroupCode,'') In (Select IfNull(AGP.GroupCode,'') From AcGroupPath AGP Where AGP.GroupUnder In (" & ReportFrm.FilterGrid(GFilterCode, 2).Value & ")) Or SG.GroupCode In (" & ReportFrm.FilterGrid(GFilterCode, 2).Value & ")) "
            If Trim(ReportFrm.FilterGrid(GFilterCode, 2).Value) <> "" Then StrConditionOP = StrConditionOP & " And (IfNull(SG.GroupCode,'') In (Select IfNull(AGP.GroupCode,'') From AcGroupPath AGP Where AGP.GroupUnder In (" & ReportFrm.FilterGrid(GFilterCode, 2).Value & ")) Or SG.GroupCode In (" & ReportFrm.FilterGrid(GFilterCode, 2).Value & ")) "
            If Trim(ReportFrm.FilterGrid(GFilterCode, 5).Value) <> "" Then StrCondition1 = StrCondition1 & " And LG.V_Type In (" & ReportFrm.FilterGrid(GFilterCode, 5).Value & ")"

            If Trim(ReportFrm.FilterGrid(GFilterCode, 3).Value) <> "" Then
                StrConditionsite = " and LG.site_Code In (" & ReportFrm.FilterGrid(GFilterCode, 3).Value & ") "
            Else
                StrConditionsite = " and LG.site_Code In  (" & AgL.PubSiteList & ") "
            End If


            '========== For Detail Section =======
            StrSQLQuery = "Select LG.V_Type,Cast(LG.V_No as Varchar) As V_No,LG.V_Date,LG.V_Prefix,SG.Name  As PName,LG.SubCode,LG.Narration, "
            StrSQLQuery = StrSQLQuery + "LG.AmtDr,LG.AmtCr,1 As SNo,SM.Name As Division,LG.ContraText As ContraName,LG.Chq_No,LG.Chq_Date,"
            StrSQLQuery = StrSQLQuery + "IfNull(LG.Site_Code,'') As Site_Code,AG.GroupName As AccGrName,AG.GroupCode AS GroupCode "
            StrSQLQuery = StrSQLQuery + "From Ledger LG Left Join SubGroup SG On LG.Subcode = SG.SubCode "
            StrSQLQuery = StrSQLQuery + "Left Join AcGroup AG ON AG.GroupCode = SG.GroupCode "
            StrSQLQuery = StrSQLQuery + "Left Join Sitemast SM On LG.Site_Code = SM.Code "

            StrSQLQuery = StrSQLQuery + StrCondition1 + StrConditionsite
            StrSQLQuery = StrSQLQuery + "Union All "

            '======= For Opening Balance =========
            StrSQLQuery = StrSQLQuery + "Select	Null As V_Type,Null As V_No,Null As V_Date,Null As V_Prefix, "
            StrSQLQuery = StrSQLQuery + "Max(SG.Name)As PName,Max(LG.SubCode) As SubCode,'OPENING BALANCE' As Narration, "
            StrSQLQuery = StrSQLQuery + "(Case When (IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LG.AmtCr),0))>0 Then  "
            StrSQLQuery = StrSQLQuery + "(IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LG.AmtCr),0)) Else 0 End) As AmtDr, "
            StrSQLQuery = StrSQLQuery + "(Case When (IfNull(Sum(LG.AmtCr),0)-IfNull(Sum(LG.AmtDr),0))>0 Then "
            StrSQLQuery = StrSQLQuery + "(IfNull(Sum(LG.AmtCr),0)-IfNull(Sum(LG.AmtDr),0)) Else 0 End) As AmtCr,"
            StrSQLQuery = StrSQLQuery + "0 As SNo,Max(SM.name) As Division,Null As ContraName,Null As Chq_No,Null As Chq_Date,"
            StrSQLQuery = StrSQLQuery + "Null As Site_Code,Max(AG.GroupName) As AccGrName,Max(AG.GroupCode) AS GroupCode  "
            StrSQLQuery = StrSQLQuery + "From Ledger LG Left Join SubGroup SG On LG.Subcode = SG.SubCode "
            StrSQLQuery = StrSQLQuery + "Left Join AcGroup AG ON AG.GroupCode = SG.GroupCode "
            StrSQLQuery = StrSQLQuery + "Left Join Sitemast SM On LG.Site_Code = SM.Code "

            StrSQLQuery = StrSQLQuery + StrConditionOP + StrConditionsite

            StrSQLQuery = StrSQLQuery + "Group By AG.GroupCode "
            StrSQLQuery = StrSQLQuery + "Order By AccGrName,V_Date,V_Type,V_No,SNo "

            DsRep = AgL.FillData(StrSQLQuery, AgL.GCn)

            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            ReportFrm.Text = "Outstanding Debtors FIFO"
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = System.Reflection.MethodBase.GetCurrentMethod().Name

            ReportFrm.ProcFillGrid(DsRep)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
    Public Sub ProcGTAReg(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try
            Dim StrCondition As String
            Dim StrConditionOp As String

            If Not FIsValid(0) Then Exit Sub
            If Not FIsValid(1) Then Exit Sub
            If Not FIsValid(2) Then Exit Sub
            If Not FIsValid(3) Then Exit Sub
            If Not FIsValid(4) Then Exit Sub
            If Not FIsValid(5) Then Exit Sub

            StrCondition = " Where (St.V_Date Between " & AgL.Chk_Text(CDate(ReportFrm.FilterGrid(GFilter, 0).Value.ToString).ToString("s")) & " And " & AgL.Chk_Text(CDate(ReportFrm.FilterGrid(GFilter, 1).Value.ToString).ToString("s")) & ") "
            StrConditionOp = " Where St.V_Date <  " & AgL.Chk_Text(CDate(ReportFrm.FilterGrid(GFilter, 0).Value.ToString).ToString("s")) & " "
            If Trim(ReportFrm.FilterGrid(GFilterCode, 2).Value) <> "" Then StrCondition = StrCondition & " And ST.Consignor In (" & ReportFrm.FilterGrid(GFilterCode, 2).Value & ")"
            If Trim(ReportFrm.FilterGrid(GFilterCode, 2).Value) <> "" Then StrConditionOp = StrConditionOp & " And ST.Consignor In (" & ReportFrm.FilterGrid(GFilterCode, 2).Value & ")"

            If Trim(ReportFrm.FilterGrid(GFilterCode, 3).Value) <> "" Then StrCondition = StrCondition & " And ST.Consignee In (" & ReportFrm.FilterGrid(GFilterCode, 3).Value & ")"
            If Trim(ReportFrm.FilterGrid(GFilterCode, 3).Value) <> "" Then StrConditionOp = StrConditionOp & " And ST.Consignee In (" & ReportFrm.FilterGrid(GFilterCode, 3).Value & ")"

            If Trim(ReportFrm.FilterGrid(GFilterCode, 5).Value) <> "" Then
                StrCondition = StrCondition & " And  St.Site_Code IN (" & ReportFrm.FilterGrid(GFilterCode, 5).Value & ") "
                StrConditionOp = StrConditionOp & " And  St.Site_Code IN (" & ReportFrm.FilterGrid(GFilterCode, 5).Value & ") "
            Else
                StrCondition = StrCondition & " And  St.Site_Code IN (" & AgL.PubSiteList & ") "
                StrConditionOp = StrConditionOp & " And  St.Site_Code IN (" & AgL.PubSiteList & ") "
            End If

            If UCase(Trim(ReportFrm.FilterGrid(GFilterCode, 4).Value)) <> "N" Then

                StrSQLQuery = "SELECT  " & AgL.Chk_Text(CDate(ReportFrm.FilterGrid(GFilter, 0).Value.ToString).ToString("s")) & " as V_Date,'' AS Consignor,'' AS Consignee,'' as VehicleNo, "
                StrSQLQuery += "'Opening' as  Description,''  AS FrPlace,'' AS ToPlace,'' as ConsignorBill,'' as ConsigneeBill,  "
                StrSQLQuery += "max(ST.EntryType) as EntryType,'' as Remark,datename(MM," & AgL.Chk_Text(CDate(ReportFrm.FilterGrid(GFilter, 1).Value.ToString).ToString("s")) & ")  As Month, "
                StrSQLQuery += "IfNull(sum(case when ST.V_Type<>'STAXR' then ST.GAmount else (0 - ST.GAmount) end),0) as Gamount, "
                StrSQLQuery += "IfNull(sum(case when ST.V_Type<>'STAXR' then ST.Exempted else (0 - ST.Exempted) end),0 ) as Exempted, "
                StrSQLQuery += "IfNull(sum(case when ST.V_Type<>'STAXR' then ST.TaxableAmt else (0 - ST.TaxableAmt) end),0) TaxableAmt, "
                StrSQLQuery += "IfNull(sum(case when ST.V_Type<>'STAXR' then ST.ServiceTaxAmt else (0 - ST.ServiceTaxAmt) end),0) ServiceTaxAmt, "
                StrSQLQuery += "IfNull(sum(case when ST.V_Type<>'STAXR' then ST.ECessAmt else (0 - ST.ECessAmt) end ),0) ECessAmt, "
                StrSQLQuery += "IfNull(sum(case when ST.V_Type<>'STAXR' then ST.SHCessAmt else (0 - ST.SHCessAmt) end),0) as SHCessAmt,max(ST.V_Type) As  V_Type, "
                StrSQLQuery += "Null As PtyBillNo,Null As PtyBillDt "
                StrSQLQuery += "FROM STaxTrn ST " + StrConditionOp
                StrSQLQuery += "and ST.EntryType='G'     "
                StrSQLQuery += "Union All "
                StrSQLQuery += "SELECT ST.V_Date,S.Name AS Consignor,S1.Name AS Consignee,ST.VehicleNo, "
                StrSQLQuery += "ST.Description,C.CityName AS FrPlace,C1.CityName AS ToPlace,ST.ConsignorBill,ST.ConsigneeBill,  "
                StrSQLQuery += "ST.EntryType,ST.Remark,datename(MM," & AgL.Chk_Text(CDate(ReportFrm.FilterGrid(GFilter, 1).Value.ToString).ToString("s")) & ") As Month, "
                StrSQLQuery += "case when ST.V_Type<>'STAXR' then ST.GAmount else (0 - ST.GAmount) end as Gamount, "
                StrSQLQuery += "case when ST.V_Type<>'STAXR' then ST.Exempted else (0 - ST.Exempted) end as Exempted, "
                StrSQLQuery += "case when ST.V_Type<>'STAXR' then ST.TaxableAmt else (0 - ST.TaxableAmt) end TaxableAmt, "
                StrSQLQuery += "case when ST.V_Type<>'STAXR' then ST.ServiceTaxAmt else (0 - ST.ServiceTaxAmt) end ServiceTaxAmt, "
                StrSQLQuery += "case when ST.V_Type<>'STAXR' then ST.ECessAmt else (0 - ST.ECessAmt) end ECessAmt, "
                StrSQLQuery += "case when ST.V_Type<>'STAXR' then ST.SHCessAmt else (0 - ST.SHCessAmt) end as SHCessAmt,ST.V_Type, "
                StrSQLQuery += "ST.PtyBillNo,ST.PtyBillDt "
                StrSQLQuery += "FROM STaxTrn ST "
                StrSQLQuery += "LEFT JOIN SubGroup S ON S.SubCode=ST.Consignor "
                StrSQLQuery += "LEFT JOIN SubGroup S1 ON S1.SubCode=ST.Consignee "
                StrSQLQuery += "LEFT JOIN City C ON C.CityCode=ST.FromPlace "
                StrSQLQuery += "LEFT JOIN City C1 ON C1.CityCode=ST.ToPlace " + StrCondition
                StrSQLQuery += "and ST.EntryType='G'  "
            Else

                StrSQLQuery = "SELECT " & AgL.Chk_Text(CDate(ReportFrm.FilterGrid(GFilter, 0).Value.ToString).ToString("s")) & " as V_Date,'' AS Consignor,'' as STNo,'Opening' as Description,'' as ConsignorBill,'' as Remark,"
                StrSQLQuery += "IfNull(sum(case when ST.V_Type<>'STXNR' then ST.ServiceTaxAmt else (0 - ST.ServiceTaxAmt) end),0) as ServiceTaxAmt, "
                StrSQLQuery += "IfNull(sum(case when ST.V_Type<>'STXNR' then ST.ECessAmt else (0 - ST.ECessAmt) end ),0) as ECessAmt, "
                StrSQLQuery += "IfNull(sum(case when ST.V_Type<>'STXNR' then ST.SHCessAmt else (0 - ST.SHCessAmt) end),0) as SHCessAmt, "
                StrSQLQuery += "IfNull(sum((Case When IfNull(ST.VrRefDocId,'')<>'' Then 0 Else (case when ST.V_Type<>'STXNR' then ST.NetAmount else (0 - ST.NetAmount) end) End)),0) as NetAmount, "
                StrSQLQuery += "max(ST.EntryType)as EntryType ,'' As V_Type, "
                StrSQLQuery += "Null As PtyBillNo,Null As PtyBillDt,Null As Chq_No,Null As Chq_Date,Null As Narration,Null As PmtDate "
                StrSQLQuery += " FROM STaxTrn ST " + StrConditionOp
                StrSQLQuery += " and ST.EntryType='N' "
                StrSQLQuery += " Union All "
                StrSQLQuery += " SELECT ST.V_Date,S.Name AS Consignor,S.STNo,ST.Description,ST.ConsignorBill,ST.Remark,"
                StrSQLQuery += " case when ST.V_Type<>'STXNR' then ST.ServiceTaxAmt else (0 - ST.ServiceTaxAmt) end as ServiceTaxAmt, "
                StrSQLQuery += " case when ST.V_Type<>'STXNR' then ST.ECessAmt else (0 - ST.ECessAmt) end as ECessAmt, "
                StrSQLQuery += " case when ST.V_Type<>'STXNR' then ST.SHCessAmt else (0 - ST.SHCessAmt) end as SHCessAmt, "
                StrSQLQuery += " (Case When IfNull(ST.VrRefDocId,'')<>'' Then 0 Else (case when ST.V_Type<>'STXNR' then ST.NetAmount else (0 - ST.NetAmount) end) End) as NetAmount, "
                StrSQLQuery += " ST.EntryType,ST.V_Type, "
                StrSQLQuery += " ST.PtyBillNo,ST.PtyBillDt,L.Chq_No,L.Chq_Date,L.Narration,L.V_Date As PmtDate  "
                StrSQLQuery += " FROM STaxTrn ST"
                StrSQLQuery += " LEFT JOIN SubGroup S ON S.SubCode=ST.Consignor"
                StrSQLQuery += " LEFT JOIN SubGroup S1 ON S1.SubCode=ST.Consignee  "
                StrSQLQuery += " LEFT JOIN Ledger L ON L.DocId=ST.VrRefDocId AND L.V_SNo=ST.VrRef_Sno " + StrCondition
                StrSQLQuery += " and ST.EntryType='N' "
            End If

            DsRep = AgL.FillData(StrSQLQuery, AgL.GCn)

            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            ReportFrm.Text = "Outstanding Debtors FIFO"
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = System.Reflection.MethodBase.GetCurrentMethod().Name

            ReportFrm.ProcFillGrid(DsRep)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
    Public Sub ProcBillWiseAdj(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try
            Dim StrCondition1 As String

            Dim DrCr As String = ""
            Dim StrAmt1 As String = ""
            Dim StrAmt2 As String = ""

            If Not FIsValid(0) Then Exit Sub

            StrCondition1 = " Where LG.V_Date < = " & AgL.Chk_Text(CDate(ReportFrm.FilterGrid(GFilter, 0).Value.ToString).ToString("s")) & "  "
            If Trim(ReportFrm.FilterGrid(GFilterCode, 1).Value) <> "" Then StrCondition1 = StrCondition1 & " And (IfNull(SG.GroupCode,'') In (Select IfNull(AGP.GroupCode,'') From AcGroupPath AGP Where AGP.GroupUnder In (" & ReportFrm.FilterGrid(GFilterCode, 1).Value & ")) Or SG.GroupCode In (" & ReportFrm.FilterGrid(GFilterCode, 1).Value & ")) "
            If Trim(ReportFrm.FilterGrid(GFilterCode, 2).Value) <> "" Then StrCondition1 = StrCondition1 & " And SG.SubCode In (" & ReportFrm.FilterGrid(GFilterCode, 2).Value & ")"

            If UCase(Trim(ReportFrm.FilterGrid(GFilterCode, 3).Value)) = "C" Then
                DrCr = UCase(Trim(ReportFrm.FilterGrid(GFilterCode, 3).Value))
            Else
                DrCr = "D"
            End If

            If DrCr = "D" Then StrAmt1 = "IfNull(LG.AmtDr,0)"
            If DrCr = "C" Then StrAmt1 = "IfNull(LG.AmtCr,0)"

            If DrCr = "D" Then StrAmt2 = "IfNull(LG.AmtCr,0)"
            If DrCr = "C" Then StrAmt2 = "IfNull(LG.AmtDr,0)"

            If Trim(ReportFrm.FilterGrid(GFilterCode, 4).Value) <> "" Then
                StrCondition1 = StrCondition1 & " And  LG.Site_Code IN (" & ReportFrm.FilterGrid(GFilterCode, 4).Value & ") "
            Else
                StrCondition1 = StrCondition1 & " And  LG.Site_Code IN (" & AgL.PubSiteList & ") "
            End If

            StrSQLQuery = "SELECT DocId,Vr_DocId,VSno,VNo,AdjVNo,VDate AS VDate,AdjDate AS AdjDate, "
            StrSQLQuery += "VType,AdjVType,PName As PName,Narration AS Narr,AdjNarr AS AdjNarr,'" & DrCr & "' As DRCR, "
            StrSQLQuery += "Amt1 AS Amt1,Amt2 AS Amt2,AdjAmt AS AdjAmt,CityName AS CityName,SiteName AS SiteName "
            StrSQLQuery += "FROM ( "
            StrSQLQuery += "Select LG.DocId,LG.V_SNo AS VSno,Cast(LG.V_No as Varchar) as VNo,Cast(LG1.V_No as Varchar) As AdjVNo, "
            StrSQLQuery += "LG.V_Date as VDate,LG1.V_Date  AS AdjDate, SG.Name As PName,"
            StrSQLQuery += "LG.Narration as Narration,LG1.Narration  AS AdjNarr," & StrAmt1 & " As Amt1,0 As Amt2, "
            StrSQLQuery += "IfNull(LA1.Amount,0) AS AdjAmt,C.CityName As CityName,(St.name) As SiteName, "
            StrSQLQuery += "LA1.Vr_DocId,LG.V_Type AS VType ,LG1.V_Type AS AdjVType "
            StrSQLQuery += "From  Ledger_Temp LG Left Join SubGroup SG On LG.Subcode=SG.SubCode Left Join "
            StrSQLQuery += "City C on SG.CityCode=C.CityCode Left Join LedgerAdj_Temp LA1 On LG.DocId=LA1.Adj_DocId And LG.V_SNo=LA1.Adj_V_SNo "
            StrSQLQuery += "LEFT JOIN Ledger LG1 ON LG1.DocId =LA1.Vr_DocId And LG1.V_SNo=LA1.Vr_V_SNo "
            StrSQLQuery += "LEFT JOIN AcGroup AG ON SG.GroupCode =AG.GroupCode  "
            StrSQLQuery += "Left Join SiteMast ST ON LG.Site_Code=St.code "
            StrSQLQuery += StrCondition1 & " And " & StrAmt1 & " > 0 And IfNull(LA1.Amount, 0) <> " & StrAmt1 & " "
            StrSQLQuery += "Union All "
            StrSQLQuery += "Select LG.DocId,LG.V_SNo AS VSno,NULL as VNo,Cast(LG.V_No as Varchar) As AdjVNo,LG.V_Date as VDate, "
            StrSQLQuery += "LG.V_Date AS AdjDate,SG.Name As PName,LG.Narration as Narration,LG.Narration AS AdjNarr, "
            StrSQLQuery += "0 As Amt1," & StrAmt2 & "-IfNull(T.AMOUNT,0) as Amt2,0 AS AdjAmt, "
            StrSQLQuery += "C.CityName As CityName,ST.name As sitename, "
            StrSQLQuery += "LG.DocId AS Vr_DocId,LG.V_Type AS VType,LG.V_Type AS AdjVType  "
            StrSQLQuery += "From Ledger_Temp LG Left Join SubGroup SG On SG.SubCode=LG.SubCode "
            StrSQLQuery += "Left Join City C on SG.CityCode=C.CityCode "
            StrSQLQuery += "LEFT JOIN (SELECT LA.Vr_Docid AS Docid,LA.Vr_V_SNo AS S_No,SUM(AMOUNT) AS AMOUNT FROM LedgerAdj LA GROUP BY LA.Vr_DocId,LA.Vr_V_SNo) T ON T.DOCID=LG.DOCID AND T.S_NO=LG.V_SNO  "
            StrSQLQuery += "LEFT JOIN AcGroup AG ON SG.GroupCode =AG.GroupCode  "
            StrSQLQuery += "Left Join SiteMast ST ON LG.Site_Code=St.code "
            StrSQLQuery += StrCondition1 & " And " & StrAmt2 & " > 0 And " & StrAmt2 & "-IfNull(T.AMOUNT,0)<>0 "
            StrSQLQuery += ") As Tmp "
            StrSQLQuery += "Order By VDate,AdjDate,DocId,Vr_DocId "

            DsRep = AgL.FillData(StrSQLQuery, AgL.GCn)

            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            ReportFrm.Text = "Outstanding Debtors FIFO"
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = System.Reflection.MethodBase.GetCurrentMethod().Name

            ReportFrm.ProcFillGrid(DsRep)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
    Public Sub ProcTDSTaxChallan(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try
            Dim StrCondition As String

            Dim StrCnd As String = ""
            If Not FIsValid(0) Then Exit Sub
            If Not FIsValid(1) Then Exit Sub


            StrCondition = " And (L.V_Date Between " & AgL.Chk_Text(CDate(ReportFrm.FilterGrid(GFilter, 0).Value.ToString).ToString("s")) & " And " & AgL.Chk_Text(CDate(ReportFrm.FilterGrid(GFilter, 1).Value.ToString).ToString("s")) & " ) "


            If Trim(ReportFrm.FilterGrid(GFilterCode, 2).Value) <> "" Then StrCondition = StrCondition & " And TC.Code In (" & ReportFrm.FilterGrid(GFilterCode, 2).Value & ")"

            If Trim(ReportFrm.FilterGrid(GFilterCode, 3).Value) <> "" Then
                StrCondition += " And  L.Site_Code IN (" & ReportFrm.FilterGrid(GFilterCode, 3).Value & ") "
            Else
                StrCondition += " And  L.Site_Code IN (" & AgL.PubSiteList & ") "
            End If

            StrSQLQuery = "SELECT TC.Name AS TSDCat,Sum(L.TdsOnAmt) AS TdsOnAmt,Sum(L.AmtCr) AS TdsAmt "
            StrSQLQuery += "FROM Ledger L "
            StrSQLQuery += "LEFT JOIN SubGroup SG ON SG.SubCode =L.ContraSub "
            StrSQLQuery += "LEFT JOIN TDSCat TC ON TC.Code=L.TDSCategory "
            StrSQLQuery += "WHERE IfNull(L.TDSCategory,'')<>'' AND IfNull(L.tdsdesc,'')<>'' "
            StrSQLQuery += "AND L.System_Generated ='Y' "
            StrSQLQuery += StrCondition & " GROUP BY TC.Name "

            DsRep = AgL.FillData(StrSQLQuery, AgL.GCn)

            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            ReportFrm.Text = "Outstanding Debtors FIFO"
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = System.Reflection.MethodBase.GetCurrentMethod().Name

            ReportFrm.ProcFillGrid(DsRep)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub

    Public Sub ProcAccountGrpWsOSAgeing(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try
            Dim StrCondition1 As String
            Dim StrCondition2 As String

            Dim STRDATE As String = ""
            Dim STROpt As String = ""
            Dim Ist As Integer
            Dim IInd As Integer
            Dim IIIrd As Integer
            Dim StrCnd As String = ""


            StrCondition1 = " Where (LG.V_Date <= " & AgL.Chk_Text(CDate(ReportFrm.FilterGrid(GFilter, 0).Value.ToString).ToString("s")) & " And IfNull(LG.AmtDr,0)>0) And AG.Nature='Customer'  "
            StrCondition2 = " Where (LG.V_Date <= " & AgL.Chk_Text(CDate(ReportFrm.FilterGrid(GFilter, 0).Value.ToString).ToString("s")) & ") And IfNull(LG.AmtCr,0)>0 And IfNull(LG.AmtCr,0)-IfNull(T.AMOUNT,0)<>0 And AG.Nature='Customer'  "

            If Trim(ReportFrm.FilterGrid(GFilterCode, 1).Value) <> "" Then StrCondition1 = StrCondition1 & " And (IfNull(SG.GroupCode,'') In (Select IfNull(AGP.GroupCode,'') From AcGroupPath AGP Where AGP.GroupUnder In (" & ReportFrm.FilterGrid(GFilterCode, 1).Value & ")) Or SG.GroupCode In (" & ReportFrm.FilterGrid(GFilterCode, 1).Value & ")) "
            If Trim(ReportFrm.FilterGrid(GFilterCode, 1).Value) <> "" Then StrCondition2 = StrCondition2 & " And (IfNull(SG.GroupCode,'') In (Select IfNull(AGP.GroupCode,'') From AcGroupPath AGP Where AGP.GroupUnder In (" & ReportFrm.FilterGrid(GFilterCode, 1).Value & ")) Or SG.GroupCode In (" & ReportFrm.FilterGrid(GFilterCode, 1).Value & ")) "

            If Trim(ReportFrm.FilterGrid(GFilterCode, 2).Value) <> "" Then StrCondition1 = StrCondition1 & " And LG.SubCode In (" & ReportFrm.FilterGrid(GFilterCode, 2).Value & ")"
            If Trim(ReportFrm.FilterGrid(GFilterCode, 2).Value) <> "" Then StrCondition2 = StrCondition2 & " And LG.SubCode In (" & ReportFrm.FilterGrid(GFilterCode, 2).Value & ")"

            Ist = Val((ReportFrm.FilterGrid(GFilter, 3).Value.ToString))
            IInd = Val((ReportFrm.FilterGrid(GFilter, 4).Value.ToString))
            IIIrd = Val((ReportFrm.FilterGrid(GFilter, 5).Value.ToString))

            If Trim(ReportFrm.FilterGrid(GFilterCode, 6).Value) = "S" Then
                STROpt = "S"
            Else
                STROpt = "D"
            End If

            If Trim(ReportFrm.FilterGrid(GFilterCode, 7).Value) <> "" Then
                StrCondition1 = StrCondition1 & " And  LG.Site_Code IN (" & ReportFrm.FilterGrid(GFilterCode, 7).Value & ") "
                StrCondition2 = StrCondition2 & " And  LG.Site_Code IN (" & ReportFrm.FilterGrid(GFilterCode, 7).Value & ") "
            Else
                StrCondition1 = StrCondition1 & " And  LG.Site_Code IN (" & AgL.PubSiteList & ") "
                StrCondition2 = StrCondition2 & " And  LG.Site_Code IN (" & ReportFrm.FilterGrid(GFilterCode, 7).Value & ") "
            End If

            STRDATE = AgL.Chk_Text(CDate(ReportFrm.FilterGrid(GFilter, 0).Value.ToString).ToString("s"))


            StrSQLQuery = "Select LG.Docid,Max(LG.V_Date) AS V_Date,Max(LG.V_Type) AS V_Type,Cast(Max(LG.V_No) as Varchar) AS Recid,Max(SG.Name) As Party,Max(SG.SubCode) As PartySCode,IfNull(Max(C.CityName),'')  As CityName,Max(AG.GroupName) As AGGroup,Max(AG.GroupCode) As AGCode,Max(SG.DueDays) AS CrDays,"
            StrSQLQuery = StrSQLQuery + "Sum(LG.AmtDr) As TotAmtDr,"
            StrSQLQuery = StrSQLQuery + "IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LA.Amount),0) as Balance,"
            StrSQLQuery = StrSQLQuery + "CASE WHEN DATEdiff(day,Max(LG.V_date)," & STRDATE & ")<=Max(SG.DueDays) THEN IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LA.Amount),0) END AS UnDueAmt,"
            StrSQLQuery = StrSQLQuery + "CASE WHEN DATEdiff(day,Max(LG.V_date)," & STRDATE & ")>Max(SG.DueDays) THEN IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LA.Amount),0) END AS DueAmt,"
            StrSQLQuery = StrSQLQuery + "MAx(St.name) As SiteName,"
            StrSQLQuery = StrSQLQuery + "CASE WHEN DATEdiff(day,Max(LG.V_date)," & STRDATE & ")>=0 AND DATEdiff(day,Max(LG.V_date)," & STRDATE & ")<=" & Ist & " THEN IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LA.Amount),0) END AS Ist,"
            StrSQLQuery = StrSQLQuery + "CASE WHEN DATEdiff(day,Max(LG.V_date)," & STRDATE & ")>" & Ist & " AND DATEdiff(day,Max(LG.V_date)," & STRDATE & ")<=" & IInd & " THEN IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LA.Amount),0) END AS IInd,"
            StrSQLQuery = StrSQLQuery + "CASE WHEN DATEdiff(day,Max(LG.V_date)," & STRDATE & ")>" & IInd & " AND DATEdiff(day,Max(LG.V_date)," & STRDATE & ")<=" & IIIrd & " THEN IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LA.Amount),0) END AS IIIrd,"
            StrSQLQuery = StrSQLQuery + "CASE WHEN DATEdiff(day,Max(LG.V_date)," & STRDATE & ")>" & IIIrd & "  THEN IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LA.Amount),0) END AS IV,0 As UnAdjust," & Ist & " AS IstSlabe,  "
            StrSQLQuery = StrSQLQuery + "" & IInd & " IIndSlab," & IIIrd & " IIIrdSlab,'" & STROpt & "' AS Opt  "
            StrSQLQuery = StrSQLQuery + "From Ledger LG "
            StrSQLQuery = StrSQLQuery + "Left Join SubGroup SG On LG.Subcode=SG.SubCode Left Join "
            StrSQLQuery = StrSQLQuery + "City C on SG.CityCode=C.CityCode Left Join "
            StrSQLQuery = StrSQLQuery + "Country CT on SG.CountryCode=CT.Code LEFT JOIN "
            StrSQLQuery = StrSQLQuery + "AcGroup AG ON SG.GroupCode =AG.GroupCode  "
            StrSQLQuery = StrSQLQuery + "Left Join LedgerAdj LA On LG.DocId=LA.Adj_DocID And LG.V_SNo=LA.Adj_V_SNo "
            StrSQLQuery = StrSQLQuery + "LEFT JOIN SiteMast ST ON LG.Site_Code =St.code  "
            StrSQLQuery = StrSQLQuery + "LEFT JOIN Area ZM ON ZM.Code =SG.Area "
            StrSQLQuery = StrSQLQuery + StrCondition1
            StrSQLQuery = StrSQLQuery + "Group By LG.DocId,LG.V_SNo "
            StrSQLQuery = StrSQLQuery + "HAVING(IfNull(Sum(LA.Amount), 0) <> Max(LG.AmtDr))"

            StrSQLQuery = StrSQLQuery + " Union All "

            StrSQLQuery = StrSQLQuery + " SELECT LG.Docid,LG.V_Date AS V_Date,LG.V_Type,Cast(LG.V_No as Varchar) AS Recid,SG.Name As Party,"
            StrSQLQuery = StrSQLQuery + " SG.SubCode As PartySCode,IfNull(C.CityName,'') As CityName,AG.GroupName As AGGroup,AG.GroupCode As AGCode,0 AS CrDays,  "
            StrSQLQuery = StrSQLQuery + " 0 As TotAmtDr,0 As Balance,0 AS UnDueAmt,0 AS  DueAmt,St.name As SiteName, 0 AS Ist,0 AS IInd,"
            StrSQLQuery = StrSQLQuery + " 0 AS IIIrd,0 AS IV,IfNull(LG.AmtCr,0)-IfNull(T.AMOUNT,0) As UnAdjust," & Ist & " AS IstSlabe, " & IInd & " IIndSlab," & IIIrd & " IIIrdSlab,"
            StrSQLQuery = StrSQLQuery + " '" & STROpt & "'  AS Opt   "
            StrSQLQuery = StrSQLQuery + "From Ledger LG "
            StrSQLQuery = StrSQLQuery + "LEFT JOIN SubGroup SG On SG.SubCode=LG.SubCode "
            StrSQLQuery = StrSQLQuery + "LEFT JOIN City C on SG.CityCode=C.CityCode "
            StrSQLQuery = StrSQLQuery + "LEFT JOIN AcGroup AG ON SG.GroupCode =AG.GroupCode "
            StrSQLQuery = StrSQLQuery + "LEFT JOIN Area ZM ON ZM.Code =SG.Area  "
            StrSQLQuery = StrSQLQuery + "LEFT JOIN SiteMast ST ON LG.Site_Code =St.code   "
            StrSQLQuery = StrSQLQuery + "LEFT JOIN (SELECT LA.Vr_Docid AS Docid,LA.Vr_V_SNo AS S_No,SUM(AMOUNT) AS AMOUNT FROM LedgerAdj LA GROUP BY LA.Vr_DocId,LA.Vr_V_SNo) T ON T.DOCID=LG.DOCID AND T.S_NO=LG.V_SNO  "
            StrSQLQuery = StrSQLQuery + StrCondition2
            StrSQLQuery = StrSQLQuery + "ORDER BY AGGroup,Party,V_Date,Recid  "



            DsRep = AgL.FillData(StrSQLQuery, AgL.GCn)

            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            ReportFrm.Text = "Outstanding Debtors FIFO"
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = System.Reflection.MethodBase.GetCurrentMethod().Name

            ReportFrm.ProcFillGrid(DsRep)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
    Public Sub ProcIntCalForDebtors(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try
            Dim StrCndBill As String, StrCndPmt As String
            Dim StrCndParty As String, StrCndPmt1 As String


            If Not FIsValid(0) Then Exit Sub
            If Not FIsValid(1) Then Exit Sub
            If Not FIsValid(4) Then Exit Sub

            StrCndBill = " And LG.V_Date <= " & AgL.Chk_Text(CDate(ReportFrm.FilterGrid(GFilter, 1).Value.ToString).ToString("s")) & " "
            StrCndPmt = " And LG.V_Date < " & AgL.Chk_Text(CDate(ReportFrm.FilterGrid(GFilter, 0).Value.ToString).ToString("s")) & " "
            StrCndPmt1 = " And (LG.V_Date Between " & AgL.Chk_Text(CDate(ReportFrm.FilterGrid(GFilter, 0).Value.ToString).ToString("s")) & " And " & AgL.Chk_Text(CDate(ReportFrm.FilterGrid(GFilter, 1).Value.ToString).ToString("s")) & ") "

            StrCndParty = ""
            If Trim(ReportFrm.FilterGrid(GFilterCode, 2).Value) <> "" Then StrCndParty = " And Max(Tmp.SubCode) In (" & ReportFrm.FilterGrid(GFilterCode, 2).Value & ") "

            If Trim(ReportFrm.FilterGrid(GFilterCode, 3).Value) <> "" Then
                StrCndBill += " And  LG.Site_Code IN (" & ReportFrm.FilterGrid(GFilterCode, 3).Value & ") "
                StrCndPmt += " And  LG.Site_Code IN (" & ReportFrm.FilterGrid(GFilterCode, 3).Value & ") "
                StrCndPmt1 += " And  LG.Site_Code IN (" & ReportFrm.FilterGrid(GFilterCode, 3).Value & ") "
            Else
                StrCndBill += " And  LG.Site_Code IN (" & AgL.PubSiteList & ") "
                StrCndPmt += " And  LG.Site_Code IN (" & AgL.PubSiteList & ") "
                StrCndPmt1 += " And  LG.Site_Code IN (" & AgL.PubSiteList & ") "
            End If

            StrSQLQuery = "Select	MT.PName,MT.SubCode,"
            StrSQLQuery += "IfNull(RTrim(LTrim(MT.Adj_DocId)),'') +'|'+ IfNull(RTrim(LTrim(MT.Adj_V_SNo)),'') As AdjDocId, "
            StrSQLQuery += "MT.V_Type, MT.RecId as V_No, "
            StrSQLQuery += "MT.V_Date,MT.AmtDr,MT.DueDays,LGAT.Vr_DocId, LGAT.Vr_Type, LGAT.Vr_RecId, "
            StrSQLQuery += "LGAT.Vr_V_Date, IfNull(LGAT.Amount,0) As Amount, "
            StrSQLQuery += "" & AgL.Chk_Text(CDate(ReportFrm.FilterGrid(GFilter, 0).Value.ToString).ToString("s")) & " As FromDate , "
            StrSQLQuery += "" & AgL.Chk_Text(CDate(ReportFrm.FilterGrid(GFilter, 1).Value.ToString).ToString("s")) & " As UpToDate , "
            StrSQLQuery += "" & Val(ReportFrm.FilterGrid(GFilter, 4).Value.ToString) & " As InterestRate "
            StrSQLQuery += "From ( "
            StrSQLQuery += "Select	Max(Adj_DocId) As Adj_DocId,Max(Adj_V_SNo) As Adj_V_SNo, "
            StrSQLQuery += "Max(V_Type) As V_Type,Max(RecId) As RecId,Max(V_Date) As V_Date, "
            StrSQLQuery += "(IfNull(Sum(AmtDr),0)-IfNull(Sum(AmtCr),0)) As AmtDr,Max(DueDays) As DueDays, "
            StrSQLQuery += "Max(PName) As PName,Max(SubCode) As SubCode "
            StrSQLQuery += "From "
            StrSQLQuery += "( "
            StrSQLQuery += "Select	LG.DocId As Adj_DocId,LG.V_SNo As Adj_V_SNo,LG.V_Type,LG.RecId,LG.V_Date, "
            StrSQLQuery += "LG.AmtDr,Null As AmtCr,SG.CreditDays DueDays,SG.Name As PName,SG.SubCode  "
            StrSQLQuery += "From Ledger LG  "
            StrSQLQuery += "Left Join SubGroup SG On LG.SubCode=SG.SubCode "
            StrSQLQuery += "Where SG.Nature In ('Customer') And IfNull(LG.AmtDr,0)<>0 "
            StrSQLQuery += StrCndBill
            StrSQLQuery += "Union All "
            StrSQLQuery += "Select	LGA.Adj_DocId,LGA.Adj_V_SNo,Null As V_Type,Null As RecId,Null As V_Date,Null As AmtDr, "
            StrSQLQuery += "LGA.Amount As AmtCr,0 As DueDays,Null As PName,Null As SubCode "
            StrSQLQuery += "From LedgerAdj LGA "
            StrSQLQuery += "Left Join Ledger LG On LGA.Vr_DocId=LG.DocId "
            StrSQLQuery += "Left Join SubGroup SG On LG.SubCode=SG.SubCode "
            StrSQLQuery += "Where SG.Nature In ('Customer') And IfNull(LG.AmtCr,0)<>0 "
            StrSQLQuery += StrCndPmt
            StrSQLQuery += ") As Tmp "
            StrSQLQuery += "Group By Adj_DocId,Adj_V_SNo "
            StrSQLQuery += "Having (IfNull(Sum(AmtDr),0)-IfNull(Sum(AmtCr),0))>0 "
            StrSQLQuery += StrCndParty
            StrSQLQuery += ") As MT "
            StrSQLQuery += "Left Join "
            StrSQLQuery += "( "
            StrSQLQuery += "Select	LGA.Adj_DocId,LGA.Adj_V_SNo,LGA.Vr_DocId,LG.V_Type As Vr_Type, "
            StrSQLQuery += "LG.V_No As Vr_RecId,LG.V_Date As Vr_V_Date,LGA.Amount "
            StrSQLQuery += "From LedgerAdj LGA "
            StrSQLQuery += "Left Join Ledger LG On LGA.Vr_DocId=LG.DocId "
            StrSQLQuery += "Left Join SubGroup SG On LG.SubCode=SG.SubCode "
            StrSQLQuery += "Where SG.Nature In ('Customer') And IfNull(LG.AmtCr,0)<>0 "
            StrSQLQuery += StrCndPmt1
            StrSQLQuery += ") As LGAT On LGAT.Adj_DocId=MT.Adj_DocId And LGAT.Adj_V_SNo=MT.Adj_V_SNo "
            StrSQLQuery += "Order By	MT.V_Date,MT.V_Type,MT.RecId,"
            StrSQLQuery += "IfNull(RTrim(LTrim(MT.Adj_DocId)),'') +'|'+ IfNull(RTrim(LTrim(MT.Adj_V_SNo)),''), "
            StrSQLQuery += "LGAT.Vr_V_Date,LGAT.Vr_RecId "


            DsRep = AgL.FillData(StrSQLQuery, AgL.GCn)

            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            ReportFrm.Text = "Outstanding Debtors FIFO"
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = System.Reflection.MethodBase.GetCurrentMethod().Name

            ReportFrm.ProcFillGrid(DsRep)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
End Class
