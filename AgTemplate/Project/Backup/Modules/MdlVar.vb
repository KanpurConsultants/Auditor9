Module MdlVar
    Public StrDocID As String       'Holds DocId Or Key Field On Save And Is Free After Save Is Executed    
    Public StrPath As String = "D:\Active Projects\CarpetSoft\Release\"
    Public IniName As String = "Academic.ini"
    Public StrDBPasswordSQL As String = ""
    Public StrDBPasswordAccess As String = "jai"
    Public AgL As AgLibrary.ClsMain
    Public AgCL As New AgControls.AgLib()
    Public AgPL As AgLibrary.ClsPrinting
    Public AgIniVar As AgLibrary.ClsIniVariables    

    Public ReportPath As String = "D:\Dataman\Release\"

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
