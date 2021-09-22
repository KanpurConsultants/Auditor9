Imports System.Data.SQLite
Module MdlFunction
    Public FrmNew As Form
    Dim gCmd As New Object
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
        Dim ECmd As Object
        Try

            AgL = New AgLibrary.ClsMain
            AgCL = New AgControls.AgLib
            AgL.PubDBUserSQL = "sa"
            AgL.PubDBPasswordSQL = ""
            AgL.PubServerName = AgL.INIRead(StrIniPath, "Server", "Name", "")
            AgL.PubReportPath = AgL.INIRead(StrIniPath, "Reports", "Path", "")
            AgL.PubCompanyDBName = AgL.INIRead(StrIniPath, "CompanyInfo", "Path", "")
            AgL.PubChkPasswordSQL = AgL.INIRead(StrIniPath, "Security", "PasswordSQL", "")
            AgL.PubChkPasswordAccess = AgL.INIRead(StrIniPath, "Security", "PasswordAccess", "")

            'AgL.GCnComp = New OleDb.OleDbConnection()
            'If UCase(Trim(AgL.PubChkPasswordAccess)) = "Y" Then
            '    AgL.GCnComp.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Jet OLEDB:Database Password=" & StrDBPasswordAccess & ";Data Source=" & AgL.PubCompanyDBName & ";Persist Security Info=False"
            'Else
            '    AgL.GCnComp.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & AgL.PubCompanyDBName & ";Persist Security Info=False"
            'End If
            'AgL.GCnComp.Open()

            'OLECmd.Connection = AgL.GCnComp
            'OLECmd.CommandText = "Select PassWD From UserMast Where User_Name='" & StrUserName & "'"
            'StrGetPassword = OLECmd.ExecuteScalar

            'AgIniVar = New AgLibrary.ClsIniVariables(AgL)
            'AgIniVar.FOpenIni(StrUserName, StrPassword)


            'AgL.GCnComp = New OleDb.OleDbConnection()
            'AgL.ECompConn = New Object()
            'AgL.GcnMain = New Object()
            'If UCase(Trim(AgL.PubChkPasswordSQL)) = "Y" Then
            '    AgL.GCnComp.ConnectionString = "Provider=SQLOLEDB;User ID='" & AgL.PubDBUserSQL & "';password=" & AgL.PubDBPasswordSQL & ";Data Source=" & AgL.PubServerName & ";Database=" & AgL.PubCompanyDBName & ""
            '    AgL.ECompConn.ConnectionString = "Persist Security Info=False;User ID='" & AgL.PubDBUserSQL & "';pwd=" & AgL.PubDBPasswordSQL & ";Initial Catalog=" & AgL.PubCompanyDBName & ";Data Source=" & AgL.PubServerName
            '    AgL.GcnMain.ConnectionString = "Persist Security Info=False;User ID='" & AgL.PubDBUserSQL & "';pwd=" & AgL.PubDBPasswordSQL & ";Initial Catalog=" & AgL.PubCompanyDBName & ";Data Source=" & AgL.PubServerName
            'Else
            '    AgL.GCnComp.ConnectionString = "Provider=SQLOLEDB;User ID='" & AgL.PubDBUserSQL & "';password=;Data Source=" & AgL.PubServerName & ";Database=" & AgL.PubCompanyDBName & ""
            '    AgL.ECompConn.ConnectionString = "Persist Security Info=False;User ID='" & AgL.PubDBUserSQL & "';pwd=;Initial Catalog=" & AgL.PubCompanyDBName & ";Data Source=" & AgL.PubServerName
            '    AgL.GcnMain.ConnectionString = "Persist Security Info=False;User ID='" & AgL.PubDBUserSQL & "';pwd=;Initial Catalog=" & AgL.PubCompanyDBName & ";Data Source=" & AgL.PubServerName
            'End If

            'AgL.GCnComp.Open()
            'AgL.ECompConn.Open()
            'AgL.GcnMain.Open()

            'ECmd = AgL.Dman_Execute("Select PassWD From UserMast Where User_Name='" & StrUserName & "'", AgL.ECompConn)
            'StrGetPassword = ECmd.ExecuteScalar

            'If UCase(StrPassword) = UCase(AgL.DCODIFY(StrGetPassword)) And (Not StrGetPassword Is Nothing) Then
            '    AgL.PubUserName = StrUserName
            '    BlnRtn = True
            'Else
            '    MsgBox("Access Denied.Please Check User Name/ Password.", MsgBoxStyle.Information, AgLibrary.ClsMain.PubMsgTitleInfo)
            '    BlnRtn = False
            'End If

            'AgIniVar = New AgLibrary.ClsIniVariables(AgL)
            'BlnRtn = AgIniVar.FOpenIni(StrUserName, StrPassword)

            Call FOpenCompanyConnection()

            BlnRtn = FIniUser(StrUserName, StrPassword)



            OLECmd = Nothing
        Catch Ex As Exception
            BlnRtn = False
            MsgBox(Ex.Message, MsgBoxStyle.Information, AgLibrary.ClsMain.PubMsgTitleInfo)
        Finally
            ECmd = Nothing
            AgpL = New AgLibrary.ClsPrinting(AgL)
        End Try
        FOpenIni = BlnRtn
    End Function


    Private Function FIniUser(ByVal StrUserName As String, ByVal StrPassword As String) As Boolean
        Dim StrGetPassword$
        Dim BlnRtn As Boolean = False
        Dim DtTemp As DataTable = Nothing

        Try
            StrGetPassword = ""
            If AgL.StrCmp(StrUserName, AgLibrary.ClsConstant.PubSuperUserName) Then
                StrGetPassword = AgLibrary.ClsConstant.PubSuperUserPassword
                If AgL.StrCmp(StrPassword, StrGetPassword) Then
                    AgL.PubUserName = StrUserName
                    AgL.PubUserPassword = StrPassword
                    AgL.PubIsUserAdmin = True
                    AgL.PubIsUserActive = True
                    BlnRtn = True
                Else
                    MsgBox("Access Denied.Please Check User Name/ Password.", MsgBoxStyle.Information, AgLibrary.ClsMain.PubMsgTitleInfo)
                    BlnRtn = False
                End If
            Else
                DtTemp = AgL.FillData("Select U.* From UserMast U  Where U.User_Name='" & StrUserName & "'", AgL.ECompConn).TABLES(0)
                If DtTemp.Rows.Count > 0 Then
                    StrGetPassword = AgL.XNull(DtTemp.Rows(0)("PASSWD"))
                End If


                If UCase(StrPassword) = UCase(AgL.DCODIFY(StrGetPassword)) And (Not StrGetPassword Is Nothing) Then
                    AgL.PubUserName = StrUserName
                    AgL.PubUserPassword = StrPassword
                    AgL.PubIsUserAdmin = AgL.StrCmp(AgL.XNull(DtTemp.Rows(0)("Admin")).ToString, "Y")

                    If AgL.IsFieldExist("Code", "UserMast", AgL.ECompConn) Then
                        AgL.PubUserCode = AgL.XNull(DtTemp.Rows(0)("Code"))
                    End If

                    If AgL.IsFieldExist("MainStreamCode", "UserMast", AgL.ECompConn) Then
                        AgL.PubUserMainStreamCode = AgL.XNull(DtTemp.Rows(0)("MainStreamCode"))
                    End If

                    AgL.PubIsUserActive = True

                    If AgL.IsFieldExist("IsActive", "UserMast", AgL.ECompConn) Then
                        AgL.PubIsUserActive = AgL.VNull(AgL.Dman_Execute("Select IfNull(U.IsActive,1) As IsUserActive From UserMast U   Where U.User_Name='" & StrUserName & "'", AgL.ECompConn).ExecuteScalar)
                    End If

                    BlnRtn = True
                Else
                    MsgBox("Access Denied.Please Check User Name/ Password.", MsgBoxStyle.Information, AgLibrary.ClsMain.PubMsgTitleInfo)
                    BlnRtn = False
                End If

                DtTemp = Nothing
            End If
        Catch ex As Exception
            BlnRtn = False
            MsgBox(ex.Message)
        Finally
            FIniUser = BlnRtn
        End Try
    End Function

    Public Sub FOpenCompanyConnection()
        'Agl.GCnComp = New OleDb.OleDbConnection()
        AgL.ECompConn = New Object()
        AgL.GcnMain = New Object()
        'Agl.GcnMainAdo = New ADODB.Connection
        'Agl.GcnMainAdo.CursorLocation = ADODB.CursorLocationEnum.adUseClient
        'Agl.GcnMainAdo.IsolationLevel = ADODB.IsolationLevelEnum.adXactChaos


        If UCase(Trim(AgL.PubChkPasswordSQL)) = "Y" Then
            'Agl.GcnComp_ConnectionString = "Provider=SQLOLEDB;User ID='" & Agl.PubDBUserSQL & "';password=" & Agl.PubDBPasswordSQL & ";Data Source=" & Agl.PubServerName & ";Database=" & Agl.PubCompanyDBName & "" '--OK
            ''Agl.GcnComp_ConnectionString = "Provider=SQLOLEDB;Data source=" & Agl.PubServerName & ";Initial catalog=" & Agl.PubCompanyDBName & ";User Id='" & Agl.PubDBUserSQL & "';Password=" & Agl.PubDBPasswordSQL & ";" '--Temp Connnection String for testin
            'Agl.GCnComp.ConnectionString = Agl.GcnComp_ConnectionString

            AgL.ECompConn_ConnectionString = "Persist Security Info=False;User ID='" & AgL.PubDBUserSQL & "';pwd=" & AgL.PubDBPasswordSQL & ";Initial Catalog=" & AgL.PubCompanyDBName & ";Data Source=" & AgL.PubServerName
            AgL.ECompConn.ConnectionString = AgL.ECompConn_ConnectionString

            AgL.GcnMain_ConnectionString = "Persist Security Info=False;User ID='" & AgL.PubDBUserSQL & "';pwd=" & AgL.PubDBPasswordSQL & ";Initial Catalog=" & AgL.PubCompanyDBName & ";Data Source=" & AgL.PubServerName
            AgL.GcnMain.ConnectionString = AgL.GcnMain_ConnectionString

            'Agl.GcnMainAdo_ConnectionString = "Provider=SQLOLEDB.1;Persist Security Info=False;User ID=" & Agl.PubDBUserSQL & "; Password=" & Agl.PubDBPasswordSQL & ";Initial Catalog=" & Agl.PubCompanyDBName & " ;Data Source=" & Agl.PubServerName
            'Agl.GcnMainAdo.ConnectionString = Agl.GcnMainAdo_ConnectionString
        Else
            'Agl.GcnComp_ConnectionString = "Provider=SQLOLEDB;User ID='" & Agl.PubDBUserSQL & "';password=;Data Source=" & Agl.PubServerName & ";Database=" & Agl.PubCompanyDBName & ""
            'Agl.GCnComp.ConnectionString = Agl.GcnComp_ConnectionString

            AgL.ECompConn_ConnectionString = "Persist Security Info=False;User ID='" & AgL.PubDBUserSQL & "';pwd=;Initial Catalog=" & AgL.PubCompanyDBName & ";Data Source=" & AgL.PubServerName
            AgL.ECompConn.ConnectionString = AgL.ECompConn_ConnectionString

            AgL.GcnMain_ConnectionString = "Persist Security Info=False;User ID='" & AgL.PubDBUserSQL & "';pwd=;Initial Catalog=" & AgL.PubCompanyDBName & ";Data Source=" & AgL.PubServerName
            AgL.GcnMain.ConnectionString = AgL.GcnMain_ConnectionString

            'Agl.GcnMainAdo_ConnectionString = "Provider=SQLOLEDB.1;Persist Security Info=False;User ID=" & Agl.PubDBUserSQL & "; Initial Catalog=" & Agl.PubCompanyDBName & " ;Data Source=" & Agl.PubServerName
            'Agl.GcnMainAdo.ConnectionString = Agl.GcnMainAdo_ConnectionString
        End If

        'Agl.GCnComp.Open()
        AgL.ECompConn.Open()
        AgL.GcnMain.Open()
        'Agl.GcnMainAdo.Open()
    End Sub


    Public Sub FOpenConnection(ByVal StrCompanyCode As String, Optional ByVal StrSiteCode As String = "")
        Dim ADTemp As OleDb.OleDbDataAdapter = Nothing

        Dim DTTemp As New DataTable
        Dim mQry As String
        Try
            mQry = "Select * From Company Where Comp_Code='" & StrCompanyCode & "'"
            'ADTemp = New OleDb.OleDbDataAdapter(mQry, AgL.GCnComp)
            'ADTemp.Fill(DTTemp)
            DTTemp = AgL.FillData(mQry, AgL.ECompConn).tables(0)
            If DTTemp.Rows.Count > 0 Then
                AgL.PubCompAdd1 = AgL.XNull(DTTemp.Rows(0).Item("address1"))
                AgL.PubCompAdd2 = AgL.XNull(DTTemp.Rows(0).Item("address2"))
                AgL.PubCompCST = AgL.XNull(DTTemp.Rows(0).Item("cstno"))
                AgL.PubCompEMail = AgL.XNull(DTTemp.Rows(0).Item("EMAil"))
                AgL.PubCompFax = AgL.XNull(DTTemp.Rows(0).Item("fax"))
                AgL.PubCompName = AgL.XNull(DTTemp.Rows(0).Item("Comp_Name"))
                AgL.PubCompShortName = AgL.XNull(DTTemp.Rows(0).Item("SName"))
                AgL.PubCompPhone = AgL.XNull(DTTemp.Rows(0).Item("phone"))
                AgL.PubCompTIN = AgL.XNull(DTTemp.Rows(0).Item("TinNo"))

                AgL.PubCompYear = AgL.XNull(DTTemp.Rows(0).Item("cyear"))
                AgL.PubDBName = AgL.XNull(DTTemp.Rows(0).Item("CentralData_Path"))
                AgL.PubEndDate = AgL.XNull(DTTemp.Rows(0).Item("End_Dt"))
                AgL.PubStartDate = AgL.XNull(DTTemp.Rows(0).Item("Start_Dt"))
                AgL.PubCompVPrefix = AgL.XNull(DTTemp.Rows(0).Item("V_Prefix"))

                AgL.PubCompCity = AgL.XNull(DTTemp.Rows(0).Item("City"))
                AgL.PubCompPinCode = AgL.XNull(DTTemp.Rows(0).Item("PIN"))
                AgL.PubCompCountry = AgL.XNull(DTTemp.Rows(0).Item("Country"))

                AgL.PubDivCode = AgL.XNull(DTTemp.Rows(0).Item("Div_Code"))
                If StrSiteCode = "" Then
                    AgL.PubSiteCode = AgL.XNull(DTTemp.Rows(0).Item("Site_Code"))
                Else
                    AgL.PubSiteCode = StrSiteCode
                End If





                AgL.GCn = New Object()
                If UCase(Trim(AgL.PubChkPasswordSQL)) = "Y" Then
                    AgL.Gcn_ConnectionString = "Persist Security Info=False;User ID='" & AgL.PubDBUserSQL & "';pwd=" & AgL.PubDBPasswordSQL & ";Initial Catalog=" & AgL.PubDBName & ";Data Source=" & AgL.PubServerName
                    AgL.GCn.ConnectionString = AgL.Gcn_ConnectionString
                Else
                    AgL.Gcn_ConnectionString = "Persist Security Info=False;User ID='" & AgL.PubDBUserSQL & "';pwd=;Initial Catalog=" & AgL.PubDBName & ";Data Source=" & AgL.PubServerName
                    AgL.GCn.ConnectionString = AgL.Gcn_ConnectionString
                End If
                AgL.GCn.Open()

                DTTemp.Clear()

                AgL.PubLoginDate = Format(AgL.XNull(AgL.GetDateTime(AgL.GCn)), "Short Date")

                AgL.PubMachineName = My.Computer.Name
            End If

            'Dim x As New ClsMain(AgL)
            'x.UpdateTableStructure(AgL.PubMdlTable)
            'AgL.FExecuteDBScript(AgL.PubMdlTable, AgL.GCn)
        Catch Ex As Exception
            MsgBox(Ex.Message, MsgBoxStyle.Information, AgLibrary.ClsMain.PubMsgTitleInfo)
        End Try
    End Sub

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