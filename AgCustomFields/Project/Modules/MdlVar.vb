Module MdlVar
    Public StrDocID As String       'Holds DocId Or Key Field On Save And Is Free After Save Is Executed    
    Public StrPath As String = "D:\Active Project\Vehicle Service\Release\"
    Public IniName As String = "KC.ini"
    Public StrDBPasswordSQL As String = ""
    Public StrDBPasswordAccess As String = "jai"
    Public AgL As AgLibrary.ClsMain
    Public AgCL As New AgControls.AgLib()
    Public AgPL As AgLibrary.ClsPrinting
    Public AgIniVar As AgLibrary.ClsIniVariables
End Module
