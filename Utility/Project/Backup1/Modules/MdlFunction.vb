Imports System.Data.SqlClient
Module MdlFunction
    Public FrmNew As Form
    Dim gCmd As New SqlClient.SqlCommand
    Public Const ConExpense As Byte = 0
    Public Const ConExpCode As Byte = 1
    Public Const ConPer As Byte = 2
    Public Const ConAccount As Byte = 3
    Public Const ConExpType As Byte = 4
    Public Const ConCalcOn As Byte = 5
    Public Const ConOnAmt As Byte = 6
    Public Const ConFormulaString As Byte = 7
    Public Const ConDrCr As Byte = 8
    Public Const ConFillAmt As Byte = 9

    Public Function FOpenIni(ByVal StrIniPath As String, ByVal StrUserName As String, ByVal StrPassword As String) As Boolean
        Dim OLECmd As New OleDb.OleDbCommand
        Dim BlnRtn As Boolean = False
        Dim ECmd As SqlClient.SqlCommand

        Try
            AgL = New AgLibrary.ClsMain : AgL.AglObj = AgL
            ClsMain_Structure = New AgStructure.ClsMain(AgL)
            ClsMain_CustomFields = New AgCustomFields.ClsMain(AgL)
            AgL.PubDBUserSQL = "sa"
            AgL.PubDBPasswordSQL = ""
            AgL.PubServerName = AgL.INIRead(StrIniPath, "Server", "Name", "")
            AgL.PubReportPath = AgL.INIRead(StrIniPath, "Reports", "Path", "")
            AgL.PubCompanyDBName = AgL.INIRead(StrIniPath, "CompanyInfo", "Path", "")
            AgL.PubChkPasswordSQL = AgL.INIRead(StrIniPath, "Security", "PasswordSQL", "")
            AgL.PubChkPasswordAccess = AgL.INIRead(StrIniPath, "Security", "PasswordAccess", "")

            AgL.PubReportPath_CommonData = AgL.INIRead(StrIniPath, "Reports", "CommonData", AgL.PubReportPath)
            AgL.PubReportPath_Utility = AgL.INIRead(StrIniPath, "Reports", "Utility", AgL.PubReportPath)

            AgIniVar = New AgLibrary.ClsIniVariables(AgL)

            BlnRtn = AgIniVar.FOpenIni(StrUserName, StrPassword)

            OLECmd = Nothing
        Catch Ex As Exception
            BlnRtn = False
            MsgBox(Ex.Message, MsgBoxStyle.Information, AgLibrary.ClsMain.PubMsgTitleInfo)
        Finally
            ECmd = Nothing
            AgPL = New AgLibrary.ClsPrinting(AgL)

            FOpenIni = BlnRtn
        End Try
    End Function


    Public Sub FormulaSet(ByVal Rpt As Object, ByVal frm As Form)
        Dim i As Int16

        For i = 0 To Rpt.DataDefinition.FormulaFields.Count - 1
            Select Case CStr(UCase(Rpt.DataDefinition.FormulaFields.Item(i).Name))
                Case "COMPANYNAME"
                    Rpt.DataDefinition.FormulaFields.Item(i).Text = "'" & AgL.PubCompName & "'"
                Case "COMPANYADDRESS"
                    Rpt.DataDefinition.FormulaFields.Item(i).Text = "'" & AgL.PubCompAdd1 & ", " & AgL.PubCompAdd2 & "'"
                Case "COMPANYCITY"
                    Rpt.DataDefinition.FormulaFields.Item(i).Text = "'" & AgL.PubCompCity & " - " & AgL.PubCompPinCode & " '"
                Case "TITLE"
                    Rpt.DataDefinition.FormulaFields.Item(i).Text = "'" & AgL.PubReportTitle & "'"
            End Select
        Next
    End Sub
 
    
End Module