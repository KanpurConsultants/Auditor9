Module MdlVar
    Public StrDocID As String       'Holds DocId Or Key Field On Save And Is Free After Save Is Executed    
    Public StrPath As String = My.Application.Info.DirectoryPath & "\"
    Public IniName As String = "AgStructure.ini"
    'Public StrDBPasswordSQL As String = ""
    'Public StrDBPasswordAccess As String = "jai"
    Public AgL As AgLibrary.ClsMain
    Public AgCL As New AgControls.AgLib()
    Public AgpL As AgLibrary.ClsPrinting
    Public AgIniVar As AgLibrary.ClsIniVariables
End Module
