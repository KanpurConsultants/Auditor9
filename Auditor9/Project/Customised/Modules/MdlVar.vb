Module MdlVar
    Public StrDocID As String       'Holds DocId Or Key Field On Save And Is Free After Save Is Executed    
    Public StrPath As String = My.Application.Info.DirectoryPath & "\"
    Public IniName As String = "KC.ini"
    Public StrDBPasswordSQL As String = ""
    Public StrDBPasswordAccess As String = "jai"
    Public AgL As AgLibrary.ClsMain
    Public AgCL As New AgControls.AgLib()
    Public AgPL As AgLibrary.ClsPrinting
    Public AgIniVar As AgLibrary.ClsIniVariables

    Public DtCommon_Enviro As DataTable = Nothing
    Public ClsMain_Structure As AgStructure.ClsMain
    Public ClsMain_EMail As EMail.ClsMain
    Public ClsMain_CustomFields As AgCustomFields.ClsMain
    'Public ClsMain_ReportLayout As ReportLayout.ClsMain
    Public RowLockedColour As Color = Color.AliceBlue
    Public ReportPath As String = "D:\Satyam\Active Projects\RUG CARE\Reports\Reports_Carpet_Main\"
    Public PubAttachmentPath As String = "..\Data\Images\"

    Public AgReportQuery As String = "Select * from SaleInvoice"
    Public AgReport_Name As String = "Sale Report"
    Public PubReportDataPath As String = ""

    Public GcnTrans As New SQLite.SQLiteConnection
    Public PubStopwatchStartValue As Integer

    Public IsFeatureApplicable_Overlay As Boolean = True

    Public PubDtSaleInvoiceItemHelp As DataSet

    Public Const mSubRecordType_StockIssue As String = "Stock Issue"
    Public Const mSubRecordType_Consumption As String = "Consumption"
    Public Const mSubRecordType_ReversePosted As String = "Reverse Posted"
    Public Const mSubRecordType_StockTransfer As String = "Stock Transfer"

    Public Const mCustomUI_Retail As String = "Retail"
    Public Const mCustomUI_Order As String = "Order"
    Public Const mCustomUI_Quotation As String = "Quotation"
    Public Const mCustomUI_Estimate As String = "Estimate"

    Public Const mCustomUI_OpeningBalanceDebtors As String = "Opening Balance Debtors"
    Public Const mCustomUI_OpeningBalanceCreditors As String = "Opening Balance Creditors"
    Public Enum StockFormType
        Opening = 0
        Transfer_Issue = 1
        Transfer_Receive = 2
    End Enum

    Public Enum StockTransferType
        Transfer_Issue = 0
        Transfer_Receive = 1
    End Enum
End Module

