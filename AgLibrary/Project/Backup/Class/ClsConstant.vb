Public Class ClsConstant

    Public Shared SiteCode_Reserve As String = "*"
    Public Shared PubKillerFilePrefix As String = "sys"

    Public Shared IsOldFaVoucherEntryActive As Boolean = True
    Public Shared IsNewFaVoucherEntryActive As Boolean = True
    Public Shared BlnIsAuditFaVoucher As Boolean = False
    Public Shared BlnManageUserControl As Boolean = False

    Public Shared IsSmsActive As Boolean = False
    Public Shared IsEMailActive As Boolean = False

    Public Const StrCheckedValue As String = "þ"
    Public Const StrUnCheckedValue As String = "¨"

    Public Const StrIniMainStreamCode As String = "010"

#Region "Entry Mode Constants"
#Region "Plz. Don't Use This Pattern"
    Public Shared AddMode As String = "A"
    Public Shared EditMode As String = "E"
    Public Shared DeleteMode As String = "D"
    Public Shared PrintMode As String = "P"
    Public Shared VarifiedMode As String = "V"
    Public Shared SynchroniseMode As String = "S"
    Public Shared BackupDatabaseMode As String = "B"
    Public Shared RestoreDatabaseMode As String = "R"
#End Region

    Public Shared EntryMode_Add As String = "A"
    Public Shared EntryMode_Edit As String = "E"
    Public Shared EntryMode_Delete As String = "D"
    Public Shared EntryMode_Print As String = "P"
    Public Shared EntryMode_Varified As String = "V"
    Public Shared EntryMode_Audited As String = "T"
    Public Shared EntryMode_Synchronise As String = "S"
    Public Shared EntryMode_BackupDatabase As String = "B"
    Public Shared EntryMode_RestoreDatabase As String = "R"

#End Region

#Region "Super User Constants"
    Public Shared PubSuperUserName As String = "SUPER"
    Public Shared PubSuperUserPassword As String = "dtman"
#End Region

#Region "SubGroup Type Constants"
    Public Shared AgentType As String = "1"
    Public Shared AgentGroupCode As String = "1"
    Public Shared AgentGroupNature As String = "A"
    Public Shared AgentNature As String = "Customer"

    Public Shared EmployeeType As String = "2"
    Public Shared EmployeeGroupCode As String = "0020"
    Public Shared EmployeeGroupNature As String = "A"
    Public Shared EmployeeNature As String = "Customer"

    Public Shared DriverType As String = "3"
    Public Shared DriverGroupCode As String = "0020"
    Public Shared DriverGroupNature As String = "A"
    Public Shared DriverNature As String = "Customer"

    Public Shared ConsignerType As String = "4"
    Public Shared ConsignerGroupCode As String = "0020"
    Public Shared ConsignerGroupNature As String = "A"
    Public Shared ConsignerNature As String = "Customer"

    Public Shared ConsigneeType As String = "5"
    Public Shared ConsigneeGroupCode As String = "0020"
    Public Shared ConsigneeGroupNature As String = "A"
    Public Shared ConsigneeNature As String = "Customer"

    Public Shared CustomerType As String = "6"
    Public Shared CustomerGroupCode As String = "0020"
    Public Shared CustomerGroupNature As String = "A"
    Public Shared CustomerNature As String = "Customer"


    Public Shared SalesManType As String = "7"
    Public Shared SalesManGroupCode As String = "0020"
    Public Shared SalesManGroupNature As String = "A"
    Public Shared SalesManNature As String = "Customer"

    Public Shared AstrologerType As String = "8"
    Public Shared AstrologerGroupCode As String = "0020"
    Public Shared AstrologerGroupNature As String = "A"
    Public Shared AstrologerNature As String = "Customer"

    Public Shared ArtisionType As String = "9"
    Public Shared ArtisionGroupCode As String = "0016"
    Public Shared ArtisionGroupNature As String = "L"
    Public Shared ArtisionNature As String = "Supplier"

    Public Shared WorkshopType As String = "10"
    Public Shared WorkshopGroupCode As String = "0016"
    Public Shared WorkshopGroupNature As String = "L"
    Public Shared WorkshopNature As String = "Supplier"

    Public Shared SubGroupType_InsuranceCompany As String = "11"
    Public Shared GroupCode_InsuranceCompany As String = "0016"
    Public Shared GroupNature_InsuranceCompany As String = "L"
    Public Shared Nature_InsuranceCompany As String = "Supplier"

    Public Shared SubGroupType_Vendor As String = "12"
    Public Shared GroupCode_Vendor As String = "0016"
    Public Shared GroupNature_Vendor As String = "L"
    Public Shared Nature_Vendor As String = "Supplier"

    Public Shared SubGroupType_PetrolPump As String = "13"
    Public Shared GroupCode_PetrolPump As String = "0016"
    Public Shared GroupNature_PetrolPump As String = "L"
    Public Shared Nature_PetrolPump As String = "Supplier"

    Public Shared SubGroupType_Other As String = "14"

#End Region

#Region "AcGroup Constants"
    Public Shared PubBranchDivisionsMainGRCode As String = "070"
    Public Shared PubBranchDivisionsMainGRLen As Integer = 3

    Public Shared PubDutiesTaxesMainGRCode As String = "030001"
    Public Shared PubDutiesTaxesMainGRLen As Integer = 6

    Public Shared MainGRCodeCapital As String = "010"
    Public Shared MainGRLenCapital As Integer = 3

    Public Shared MainGRCodeLoanLiability As String = "020"
    Public Shared MainGRLenLoanLiability As Integer = 3

    Public Shared MainGRCodeCurrentLiabilities As String = "030"
    Public Shared MainGRLenCurrentLiabilities As Integer = 3

    Public Shared MainGRCodeFixedAssets As String = "040"
    Public Shared MainGRLenFixedAssets As Integer = 3

    Public Shared MainGRCodeInvestments As String = "050"
    Public Shared MainGRLenInvestments As Integer = 3

    Public Shared MainGRCodeCurrentAssets As String = "060"
    Public Shared MainGRLenCurrentAssets As Integer = 3

    Public Shared MainGRCodeBranchDivisions As String = "070"
    Public Shared MainGRLenBranchDivisions As Integer = 3

    Public Shared MainGRCodeMiscExpencesAsset As String = "080"
    Public Shared MainGRLenMiscExpencesAsset As Integer = 3

    Public Shared MainGRCodeSuspenseAc As String = "090"
    Public Shared MainGRLen As Integer = 3

    Public Shared MainGRCodeProfitAndLossAc As String = "999"
    Public Shared MainGRLenProfitAndLossAc As Integer = 3

    Public Shared MainGRCodeDutiesTaxes As String = "030001"
    Public Shared MainGRLenDutiesTaxes As Integer = 6

    Public Shared MainGRCodeIndirectExpences As String = "280"
    Public Shared MainGRLenIndirectExpences As Integer = 3

    Public Shared MainGRCodeIndirectIncome As String = "270"
    Public Shared MainGRLenIndirectIncome As Integer = 3

    Public Shared MainGRCodeDirectExpences As String = "260"
    Public Shared MainGRLenDirectExpences As Integer = 3

    Public Shared MainGRCodeDirectIncome As String = "250"
    Public Shared MainGRLenDirectIncome As Integer = 3

    Public Shared MainGRCodeSales As String = "230"
    Public Shared MainGRLenSales As Integer = 3

    Public Shared MainGRCodePurchase As String = "240"
    Public Shared MainGRLenPurchase As Integer = 3

    Public Shared MainGRCodeCashInHand As String = "060005"
    Public Shared MainGRLenCashInHand As Integer = 6

    Public Shared MainGRCodeBank As String = "060006"
    Public Shared MainGRLenBank As Integer = 6

    Public Shared MainGRCodeSundryCreditors As String = "030003"
    Public Shared MainGRLenSundryCreditors As Integer = 6

    Public Shared MainGRCodeSundryDebtors As String = "060004"
    Public Shared MainGRLenSundryDebtors As Integer = 6

#End Region

#Region "Voucher Cat / NCat Constants"
    '============= <Opening Voucher Type> Constants=================
    Public Shared NCat_FaOpening As String = "OPBAL"
    Public Shared Cat_FaOpening As String = "OPBAL"
    Public Shared NCatDesc_FaOpening As String = "A/c Opening"
    '============= <********************> ==========================
    '<Old Fa Voucher NCat> Constants==============================
    Public Shared NCat_OldFaVoucher As String = "FA"

    '<Voucher NCat> Constants==============================
    Public Shared NCat_Voucher As String = "VFA"

    '<Voucher Category> Constants==============================
    Public Shared Cat_BankReceiptVoucher As String = "VBRCT"
    Public Shared Cat_CashReceiptVoucher As String = "VCRCT"
    Public Shared Cat_BankPaymentVoucher As String = "VBPMT"
    Public Shared Cat_CashPaymentVoucher As String = "VCPMT"
    Public Shared Cat_JournalVoucher As String = "VJNL"
    Public Shared Cat_ContraVoucher As String = "VCNT"


    '<(W) Voucher Type> Constants==============================
    Public Shared WVType_BankReceiptVoucher As String = "WBRCT"
    Public Shared WVType_CashReceiptVoucher As String = "WCRCT"
    Public Shared WVType_BankPaymentVoucher As String = "WBPMT"
    Public Shared WVType_CashPaymentVoucher As String = "WCPMT"


    '<(W) Voucher Type Description> Constants==============================
    Public Shared WVTypeDesc_BankReceiptVoucher As String = "Bank Receipt Entry"
    Public Shared WVTypeDesc_CashReceiptVoucher As String = "Cash Receipt Entry"
    Public Shared WVTypeDesc_BankPaymentVoucher As String = "Bank Payment Entry"
    Public Shared WVTypeDesc_CashPaymentVoucher As String = "Cash Payment Entry"

    '<Voucher NCategory Description> Constants==============================
    Public Shared NCatDesc_BankReceiptVoucher As String = "Bank Receipt Voucher."
    Public Shared NCatDesc_CashReceiptVoucher As String = "Cash Receipt Voucher."
    Public Shared NCatDesc_BankPaymentVoucher As String = "Bank Payment Voucher."
    Public Shared NCatDesc_CashPaymentVoucher As String = "Cash Payment Voucher."
    Public Shared NCatDesc_JournalVoucher As String = "Journal Voucher."
    Public Shared NCatDesc_ContraVoucher As String = "Contra Voucher."
    '==========================================================

#End Region

#Region "Date Format Constants"
    Public Shared DateFormat_ShortDate = "Short Date"
    Public Shared DateFormat_LongDate = "Long Date"
#End Region

#Region "User Module Constants"
    ''======<Common User Modeles>=========================================
    Public Shared Module_Common_Master As String = "Common Master"
    Public Shared Module_Financial_Accounts As String = "FA"
    Public Shared Module_Utility As String = "Utility"
    Public Shared Module_SMS As String = "SMS"
    Public Shared Module_EMail As String = "EMail"
    ''=====================================================================
#End Region

End Class
