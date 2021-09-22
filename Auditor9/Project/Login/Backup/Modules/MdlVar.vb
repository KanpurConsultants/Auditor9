Module MdlVar
    Public StrDocID As String       'Holds DocId Or Key Field On Save And Is Free After Save Is Executed
    Public StrPath As String = Trade.My.Application.Info.DirectoryPath
    Public IniName As String = "KC.ini"
    Public StrDBPasswordSQL As String = ""
    Public StrDBPasswordAccess As String = ""
    Public AgL As AgLibrary.ClsMain
    Public StrModuleName As String = "Login"
    Public AgPL As AgLibrary.ClsPrinting    
    Public AgIniVar As AgLibrary.ClsIniVariables

    Public BaseTableList = "'Company', 'Division', 'UserMast', 'Login_Log', 'User_Permission', 'User_Control_Permission'"

End Module
