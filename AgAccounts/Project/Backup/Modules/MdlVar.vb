Module MdlVar
    Public StrDocID As String       'Holds DocId Or Key Field On Save And Is Free After Save Is Executed    
    Public StrModuleName = "Accounts"
    Public StrPath As String = My.Application.Info.DirectoryPath & "\"
    Public IniName As String = "KC.ini"
    Public AgL As AgLibrary.ClsMain
    Public AgCl As New AgControls.AgLib
    Public AgIniVar As AgLibrary.ClsIniVariables
    Public CMain As New ClsMain(AgL)
End Module
