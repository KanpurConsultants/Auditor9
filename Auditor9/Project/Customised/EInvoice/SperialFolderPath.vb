Module SperialFolderPath
    Private Const CSIDL_DESKTOP = &H0 '// The Desktop - virtual folder
    Private Const CSIDL_PROGRAMS = 2 '// Program Files
    Private Const CSIDL_CONTROLS = 3 '// Control Panel - virtual folder
    Private Const CSIDL_PRINTERS = 4 '// Printers - virtual folder
    Private Const CSIDL_DOCUMENTS = 5 '// My Documents
    Private Const CSIDL_FAVORITES = 6 '// Favourites
    Private Const CSIDL_STARTUP = 7 '// Startup Folder
    Private Const CSIDL_RECENT = 8 '// Recent Documents
    Private Const CSIDL_SENDTO = 9 '// Send To Folder
    Private Const CSIDL_BITBUCKET = 10 '// Recycle Bin - virtual folder
    Private Const CSIDL_STARTMENU = 11 '// Start Menu
    Private Const CSIDL_DESKTOPFOLDER = 16 '// Desktop folder
    Private Const CSIDL_DRIVES = 17 '// My Computer - virtual folder
    Private Const CSIDL_NETWORK = 18 '// Network Neighbourhood - virtual folder
    Private Const CSIDL_NETHOOD = 19 '// NetHood Folder
    Private Const CSIDL_FONTS = 20 '// Fonts folder
    Private Const CSIDL_SHELLNEW = 21 '// ShellNew folder
    Dim Timer1 As Integer

    'Module Code

    Declare Function SHGetSpecialFolderLocation Lib "Shell32.dll" _
(ByVal hwndOwner As Long, ByVal nFolder As Long, pidl As ITEMIDLIST) As Long
    Declare Function SHGetPathFromIDList Lib "Shell32.dll" Alias "SHGetPathFromIDListA" _
(ByVal pidl As Long, ByVal pszPath As String) As Long

    Public Type SHITEMID
    cb As Long
    abID As Byte
End Type

Public Type ITEMIDLIST
    mkid As SHITEMID
End Type
Public Const MAX_PATH As Integer = 260

    Public Function fGetSpecialFolder(CSIDL As Long) As String
        Dim sPath As String
        Dim IDL As ITEMIDLIST
        '
        ' Retrieve info about system folders such as the "Recent Documents" folder.
        ' Info is stored in the IDL structure.
        '
        fGetSpecialFolder = ""
        If SHGetSpecialFolderLocation(frmeInvoice.hWnd, CSIDL, IDL) = 0 Then
            '
            ' Get the path from the ID list, and return the folder.
            '
            sPath = Space$(MAX_PATH)
            If SHGetPathFromIDList(ByVal IDL.mkid.cb, ByVal sPath) Then
                fGetSpecialFolder = Left$(sPath, InStr(sPath, vbNullChar) - 1) & ""
            End If
        End If
    End Function

End Module
