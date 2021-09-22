Imports System.Data.SqlClient
Module MdlFunction
    Dim mQry As String = ""

    Public Function FOpenIni(ByVal StrIniPath As String, ByVal StrUserName As String, ByVal StrPassword As String) As Boolean
        Dim OLECmd As New OleDb.OleDbCommand
        Dim BlnRtn As Boolean = False
        Dim ECmd As SqlClient.SqlCommand

        Try
            AgL = New AgLibrary.ClsMain : AgL.AglObj = AgL
            ClsMain_Structure = New AgStructure.ClsMain(AgL)
            ClsMain_Purchase = New Purchase.ClsMain(AgL)
            ClsMain_Sales = New Sales.ClsMain(AgL)
            ClsMain_CustomFields = New AgCustomFields.ClsMain(AgL)
            ClsMain_ReportLayout = New ReportLayout.ClsMain(AgL)
            ClsMain_EMail = New EMail.ClsMain(AgL)

            AgL.PubDBUserSQL = "sa"
            AgL.PubDBPasswordSQL = ""
            AgL.PubServerName = AgL.INIRead(StrIniPath, "Server", "Name", "")
            AgL.PubReportPath = AgL.INIRead(StrIniPath, "Reports", "Path", "")
            AgL.PubCompanyDBName = AgL.INIRead(StrIniPath, "CompanyInfo", "Path", "")
            AgL.PubChkPasswordSQL = AgL.INIRead(StrIniPath, "Security", "PasswordSQL", "")
            AgL.PubChkPasswordAccess = AgL.INIRead(StrIniPath, "Security", "PasswordAccess", "")

            AgL.PubReportPath_CommonData = AgL.INIRead(StrIniPath, "Reports", "CommonData", AgL.PubReportPath)
            AgL.PubReportPath_Utility = AgL.INIRead(StrIniPath, "Reports", "Utility", AgL.PubReportPath)

            AgL.PubReportPath = My.Application.Info.DirectoryPath & "\Reports"
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

    Public Sub IniDtEnviro()
        Call IniDtCommon_Enviro()


    End Sub

    Public Sub IniDtCommon_Enviro()

        AgL.PubDtEnviro = AgL.FillData("SELECT E.* FROM Enviro E With (NoLock) WHERE E.Site_Code ='" & AgL.PubSiteCode & "'", AgL.GCn).Tables(0)
    End Sub



End Module