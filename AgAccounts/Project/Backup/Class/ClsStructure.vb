Public Class ClsStructure
    Public Enum EntryType
        ForEntry = 0
        ForPosting = 1
    End Enum
    Public Enum DisplayType
        BalanceSheet = 0
        ProfitAndLoss = 1
        TrailBalance = 2
        DTrailBalance = 3
        GroupBalance = 4
        Ledger = 5
    End Enum
    Public Enum DisplayType_Stock
        Category = 0
        Item = 1
        Transaction = 2
    End Enum
    Public Structure DHSettings
        Dim StrFromDate As String
        Dim StrToDate As String
        Dim StrSiteCode As String
        Dim StrSiteName As String
        Dim StrZeroBalace As String
        Dim StrShowContra As String
        Dim DblClosingStock As String
        Dim StrAcGroup As String
        Dim StrCostCenter As String
    End Structure
    Public Structure DHSettings_Stock
        Dim StrFromDate As String
        Dim StrToDate As String
        Dim StrSiteCode As String
        Dim StrSiteName As String
        Dim StrGodownCode As String
        Dim StrGodownName As String
        Dim StrZeroBalace As String
        Dim StrReportType As String
        Dim StrItemCategory As String
        Dim StrItemCategoryCode As String
        Dim StrItemGroup As String
        Dim StrItemGroupCode As String
        Dim StrItemName As String
        Dim StrItemNameCode As String
    End Structure
    Public Structure TDS
        Dim StrDesc As String
        Dim StrDescCode As String
        Dim StrPostingAc As String
        Dim StrPostingAcCode As String
        Dim DblPercentage As Double
        Dim DblAmount As Double
        Dim StrFormula As String
    End Structure
    Public Structure LedgerAdj
        Dim StrAdj_Type As String
        Dim StrDocId As String
        Dim StrV_No As String
        Dim StrV_SNo As String
        Dim StrV_Type As String
        Dim StrV_Date As String
        Dim StrNarration As String
        Dim DblBillAmt As Double
        Dim DblAdjusted As Double
        Dim DblBalanceAmt As Double
        Dim StrBalanceAmtDrCr As String
        Dim DblAdjustment As Double
        Dim StrAdjustmentDrCr As String
    End Structure
    Public Structure LedgerItemAdj
        Dim StrItemCode As String
        Dim StrItemName As String
        Dim DblQuantity As Double
        Dim StrUnit As String
        Dim DblAmount As Double
        Dim StrRemark As String
    End Structure
    Public Structure VoucherType
        Dim TDSVar() As TDS
        Dim LAdjVar() As LedgerAdj
        Dim LIAdjVar() As LedgerItemAdj
    End Structure
    Public Structure StuctAcDetails
        Dim Address1 As String
        Dim Address2 As String
        Dim CityCode As String
        Dim CityName As String
        Dim PIN As String
        Dim CountryCode As String
        Dim CountryName As String
        Dim ContactPerson As String
        Dim PhoneNo As String
        Dim Fax As String
        Dim Mobile As String
        Dim Remark As String
        Dim DuplicateTIN As String
        Dim TIN As String
        Dim CST As String
        Dim ST As String
        Dim PAN As String
        Dim EMail As String
        Dim Location As String
        Dim PartyCategory As String
        Dim PartyType As String
        Dim CreditLimit As Double
        Dim DueDays As Double
        Dim ECCCode As String
        Dim ExciseNo As String
        Dim Range As String
        Dim Division As String
        Dim IECCode As String
        Dim FBTOnPer As Double
        Dim FBTPer As Double
        Dim TDSCode As String
        Dim TDSName As String
        Dim LedgerGroupCode As String
        Dim LedgerGroupName As String
        Dim DistributorCode As String
        Dim DistributorName As String
        Dim ZoneCode As String
        Dim ZoneName As String
        Dim PolicyNo As String
    End Structure
End Class
