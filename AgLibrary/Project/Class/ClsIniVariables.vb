Imports System.Data.SQLite

Public Class ClsIniVariables
    Dim Agl As AgLibrary.ClsMain

    Public Sub New(ByVal AgLibVar As AgLibrary.ClsMain)
        Agl = AgLibVar
    End Sub

    Public Function FOpenIni(ByVal StrUserName As String, ByVal StrPassword As String, ByVal StrUserCode As String) As Boolean
        Dim BlnRtn As Boolean = False

        Try
            Call FOpenCompanyConnection()

            If StrUserName.Trim = "" And StrUserCode.Trim <> "" Then
                StrUserName = Agl.XNull(Agl.Dman_Execute("Select U.User_Name From UserMast U Where U.Code ='" & StrUserCode & "'", Agl.ECompConn).ExecuteScalar)
            End If

            If StrUserName.Trim <> "" Then
                BlnRtn = FIniUser(StrUserName, StrPassword)
            Else
                BlnRtn = False
            End If


        Catch ex As Exception
            BlnRtn = False
            MsgBox(ex.Message)
        Finally
            FOpenIni = BlnRtn
        End Try
    End Function

    Public Function FOpenIni(ByVal StrUserName As String, ByVal StrPassword As String) As Boolean
        Dim BlnRtn As Boolean = False

        Try
            Call FOpenCompanyConnection()

            BlnRtn = FIniUser(StrUserName, StrPassword)
        Catch ex As Exception
            BlnRtn = False
            MsgBox(ex.Message)
        Finally
            FOpenIni = BlnRtn
        End Try

    End Function

    Private Function FIniUser(ByVal StrUserName As String, ByVal StrPassword As String) As Boolean
        Dim StrGetPassword$
        Dim BlnRtn As Boolean = False
        Dim DtTemp As DataTable = Nothing

        Try
            StrGetPassword = ""
            If Agl.StrCmp(StrUserName, AgLibrary.ClsConstant.PubSuperUserName) Then
                StrGetPassword = AgLibrary.ClsConstant.PubSuperUserPassword
                If Agl.StrCmp(StrPassword, StrGetPassword) Then
                    Agl.PubUserName = StrUserName
                    Agl.PubUserPassword = StrPassword
                    Agl.PubIsUserAdmin = True
                    Agl.PubIsUserActive = True
                    BlnRtn = True
                Else
                    MsgBox("Access Denied.Please Check User Name/ Password.", MsgBoxStyle.Information, AgLibrary.ClsMain.PubMsgTitleInfo)
                    BlnRtn = False
                End If
            Else
                DtTemp = Agl.FillData("Select U.* From UserMast U  Where U.User_Name='" & StrUserName & "'", Agl.ECompConn).TABLES(0)
                If DtTemp.Rows.Count > 0 Then
                    StrGetPassword = Agl.XNull(DtTemp.Rows(0)("PASSWD"))
                End If


                If UCase(StrPassword) = UCase(Agl.DCODIFY(StrGetPassword)) And (Not StrGetPassword Is Nothing) Then
                    Agl.PubUserName = StrUserName
                    Agl.PubUserPassword = StrPassword
                    Agl.PubIsUserAdmin = Agl.StrCmp(Agl.XNull(DtTemp.Rows(0)("Admin")).ToString, "Y")

                    If Agl.IsFieldExist("Code", "UserMast", Agl.GcnMain) Then
                        Agl.PubUserCode = Agl.XNull(DtTemp.Rows(0)("Code"))
                    End If

                    If Agl.IsFieldExist("MainStreamCode", "UserMast", Agl.GcnMain) Then
                        Agl.PubUserMainStreamCode = Agl.XNull(DtTemp.Rows(0)("MainStreamCode"))
                    End If

                    Agl.PubIsUserActive = True

                    If Agl.IsFieldExist("IsActive", "UserMast", Agl.GcnMain) Then
                        Agl.PubIsUserActive = Agl.VNull(Agl.Dman_Execute("Select (Case When U.IsActive Is Null Then 1 Else U.IsActive End) As IsUserActive From UserMast U   Where U.User_Name='" & StrUserName & "'", Agl.ECompConn).ExecuteScalar)
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

        If Agl.PubServerName = "" Then
            Agl.ECompConn = New SQLiteConnection()
            Agl.GcnMain = New SQLiteConnection()

            If Agl.PubIsDatabaseEncrypted = "N" Then
                Agl.GcnMain_ConnectionString = "Data Source=" & Agl.PubCompanyDBPath & Agl.PubCompanyDBName & ";Version=3;"
                Agl.GcnMain.ConnectionString = "Data Source=" & Agl.PubCompanyDBPath & Agl.PubCompanyDBName & ";Version=3;"
                Agl.ECompConn_ConnectionString = "Data Source=" & Agl.PubCompanyDBPath & Agl.PubCompanyDBName & ";Version=3;"
                Agl.ECompConn.ConnectionString = "Data Source=" & Agl.PubCompanyDBPath & Agl.PubCompanyDBName & ";Version=3;"
            Else
                Agl.GcnMain_ConnectionString = "Data Source=" & Agl.PubCompanyDBPath & Agl.PubCompanyDBName & ";Version=3;Password=" & AgLibrary.ClsConstant.PubDbPassword & ";"
                Agl.GcnMain.ConnectionString = "Data Source=" & Agl.PubCompanyDBPath & Agl.PubCompanyDBName & ";Version=3;Password=" & AgLibrary.ClsConstant.PubDbPassword & ";"
                Agl.ECompConn_ConnectionString = "Data Source=" & Agl.PubCompanyDBPath & Agl.PubCompanyDBName & ";Version=3;Password=" & AgLibrary.ClsConstant.PubDbPassword & ";"
                Agl.ECompConn.ConnectionString = "Data Source=" & Agl.PubCompanyDBPath & Agl.PubCompanyDBName & ";Version=3;Password=" & AgLibrary.ClsConstant.PubDbPassword & ";"
            End If

        Else
            Agl.ECompConn = New SqlClient.SqlConnection
            Agl.GcnMain = New SqlClient.SqlConnection

            Agl.ECompConn_ConnectionString = "Persist Security Info=False;User ID='SA';pwd=;Initial Catalog=" & Agl.PubCompanyDBName & ";Data Source=" & Agl.PubServerName
            Agl.ECompConn.ConnectionString = Agl.ECompConn_ConnectionString

            Agl.GcnMain_ConnectionString = "Persist Security Info=False;User ID='SA';pwd=;Initial Catalog=" & Agl.PubCompanyDBName & ";Data Source=" & Agl.PubServerName
            Agl.GcnMain.ConnectionString = Agl.GcnMain_ConnectionString
        End If


        Agl.ECompConn.Open()
        Agl.GcnMain.Open()
    End Sub

    Public Sub FOpenConnection(ByVal StrCompanyCode As String, Optional ByVal StrSiteCode As String = "", Optional ByVal CheckKiller As Boolean = True)
        Dim ADTemp As OleDb.OleDbDataAdapter = Nothing
        Dim ADTempSQL As SQLiteDataAdapter
        Dim DTTemp As New DataTable

        Dim mQry As String = ""

        Try
            mQry = "Select * From Company Where Comp_Code='" & StrCompanyCode & "'"
            DTTemp = Agl.FillData(mQry, Agl.GcnMain).Tables(0)
            If DTTemp.Rows.Count > 0 Then

                If Agl.IsFieldExist("UseSiteNameAsCompanyName", "Company", Agl.GcnMain) Then Agl.PubUseSiteNameAsCompanyName = Agl.VNull(DTTemp.Rows(0).Item("UseSiteNameAsCompanyName"))

                Agl.PubCompAdd1 = Agl.XNull(DTTemp.Rows(0).Item("address1"))
                Agl.PubCompAdd2 = Agl.XNull(DTTemp.Rows(0).Item("address2"))
                Agl.PubCompCity = Agl.XNull(DTTemp.Rows(0).Item("City"))
                Agl.PubJurisdictionCity = Agl.XNull(DTTemp.Rows(0).Item("City"))
                Agl.PubCompPinCode = Agl.XNull(DTTemp.Rows(0).Item("Pin"))
                Agl.PubCompCST = Agl.XNull(DTTemp.Rows(0).Item("cstno"))
                Agl.PubCompEMail = Agl.XNull(DTTemp.Rows(0).Item("EMAil"))
                'Agl.PubCompFax = Agl.XNull(DTTemp.Rows(0).Item("fax"))
                Agl.PubMainCompName = Agl.XNull(DTTemp.Rows(0).Item("Comp_Name"))
                Agl.PubCompName = Agl.XNull(DTTemp.Rows(0).Item("Comp_Name"))
                'Agl.PubCompShortName = Agl.XNull(DTTemp.Rows(0).Item("SName"))
                Agl.PubCompPhone = Agl.XNull(DTTemp.Rows(0).Item("phone"))
                'Agl.PubCompTIN = Agl.XNull(DTTemp.Rows(0).Item("TinNo"))

                Agl.PubCompYear = Agl.XNull(DTTemp.Rows(0).Item("cyear"))
                Agl.PubDBPrefix = Agl.XNull(DTTemp.Rows(0).Item("DBPrefix"))
                Agl.PubDBName = Agl.XNull(DTTemp.Rows(0).Item("CentralData_Path"))
                If Agl.PubDBName = "" Then Agl.PubDBName = Agl.PubCompanyDBName
                Agl.PubPrevDBName = Agl.XNull(DTTemp.Rows(0).Item("PrevDBName"))
                Agl.PubAgReportPath = Agl.XNull(DTTemp.Rows(0).Item("Repo_Path"))
                Agl.PubEndDate = Agl.XNull(DTTemp.Rows(0).Item("End_Dt"))
                Agl.PubStartDate = Agl.XNull(DTTemp.Rows(0).Item("Start_Dt"))
                Agl.PubCompVPrefix = Agl.XNull(DTTemp.Rows(0).Item("V_Prefix"))

                Agl.PubCompCode = Agl.XNull(DTTemp.Rows(0).Item("Comp_Code"))
                'If StrSiteCode = "" Then
                '    Agl.PubSiteCode = Agl.XNull(DTTemp.Rows(0).Item("Site_Code"))
                'Else
                '    Agl.PubSiteCode = StrSiteCode
                'End If


                'Agl.PubSiteCodeDisplay = "('" & Agl.PubSiteCode & "')"
                Agl.PubCompSerialNo = Agl.XNull(DTTemp.Rows(0).Item("SerialKeyNo"))

                If Agl.IsFieldExist("SitewiseV_No", "Division", Agl.GcnMain) Then
                    mQry = "Select IfNull(SitewiseV_No,0) As SitewiseV_No From Division Where Div_Code=" & Agl.Chk_Text(Agl.XNull(DTTemp.Rows(0).Item("Div_Code"))) & " "
                    Agl.ECmd = Agl.Dman_Execute(mQry, Agl.GcnMain)
                    Agl.PubSitewiseV_No = Agl.ECmd.ExecuteScalar()
                End If


                If Agl.PubDivisionApplicable = False Then
                    Agl.PubDivCode = Agl.XNull(DTTemp.Rows(0).Item("Div_Code"))
                Else
                    '''''''AgL.PubDivCode Assigned Value During Login''''''''''''''''
                End If

                Agl.PubCompCountry = Agl.XNull(DTTemp.Rows(0).Item("Country"))

                If Agl.IsFieldExist("ImageDbName", "Company", Agl.GcnMain) Then
                    Agl.PubImageDBName = Agl.XNull(DTTemp.Rows(0).Item("ImageDbName"))
                End If

                DTTemp.Clear()
                'ADTempSQL = New SqliteDataAdapter("Select Date('now') As SrvDate ", Agl.ECompConn)
                'ADTempSQL.Fill(DTTemp)
                'If DTTemp.Rows.Count > 0 Then
                Agl.PubLoginDate = System.DateTime.Today()                'Format(Agl.XNull(DTTemp.Rows(0).Item("SrvDate")), "Short Date")
                Agl.PubLastTransactionDate = Today()
                'End If

                '===============================================================
                '============= For Activating Or DeActivating ==================
                '====================== Killer ==================================
                '===============================================================
                If CheckKiller Then Call Activate_Killer()
                '===============================================================
                '===============================================================


                'If UCase(Trim(Agl.PubChkPasswordSQL)) = "Y" Then
                '    Agl.Gcn_ConnectionString = "Persist Security Info=False;User ID='" & Agl.PubDBUserSQL & "';pwd=" & Agl.PubDBPasswordSQL & ";Initial Catalog=" & Agl.PubDBName & ";Data Source=" & Agl.PubServerName
                '    Agl.GCn.ConnectionString = Agl.Gcn_ConnectionString
                '    Agl.GcnRead.ConnectionString = Agl.Gcn_ConnectionString

                '    Agl.GCnRep_ConnectionString = "Persist Security Info=False;User ID='" & Agl.PubDBUserSQL & "';pwd=" & Agl.PubDBPasswordSQL & ";Initial Catalog=" & Agl.PubAgReportPath & ";Data Source=" & Agl.PubServerName
                '    Agl.GCnRep.ConnectionString = Agl.GCnRep_ConnectionString
                'Else
                '    Agl.Gcn_ConnectionString = "Persist Security Info=False;User ID='" & Agl.PubDBUserSQL & "';pwd=;Initial Catalog=" & Agl.PubDBName & ";Data Source=" & Agl.PubServerName
                '    Agl.GCn.ConnectionString = Agl.Gcn_ConnectionString
                '    Agl.GcnRead.ConnectionString = Agl.Gcn_ConnectionString

                '    Agl.GCnRep_ConnectionString = "Persist Security Info=False;User ID='" & Agl.PubDBUserSQL & "';pwd=;Initial Catalog=" & Agl.PubAgReportPath & ";Data Source=" & Agl.PubServerName
                '    Agl.GCnRep.ConnectionString = Agl.GCnRep_ConnectionString
                'End If

                'If Agl.PubOfflineApplicable Then
                '    Agl.GcnSite_ConnectionString = "Persist Security Info=False;User ID='" & Agl.PubDBUserSQL & "';pwd=" & Agl.PubDBPasswordSQL & ";Initial Catalog=" & Agl.PubDBName & ";Data Source=" & Agl.PubSqlServerSite
                '    Agl.GcnSite = New SqliteConnection
                '    Agl.GcnSite.ConnectionString = Agl.GcnSite_ConnectionString
                '    Agl.GcnSiteRead = New SqliteConnection
                '    Agl.GcnSiteRead.ConnectionString = Agl.GcnSite_ConnectionString

                '    Agl.GcnSiteComp_ConnectionString = "Persist Security Info=False;User ID='" & Agl.PubDBUserSQL & "';pwd=" & Agl.PubDBPasswordSQL & ";Initial Catalog=" & Agl.PubDBName & ";Data Source=" & Agl.PubSqlServerSite
                '    Agl.GcnSiteComp = New SqliteConnection
                '    Agl.GcnSiteComp.ConnectionString = Agl.GcnSiteComp_ConnectionString

                '    Agl.GcnSite.Open()
                '    Agl.GcnSiteComp.Open()
                '    Agl.GcnSiteRead.Open()
                'End If
                If Agl.PubServerName = "" Then

                    Agl.GCn = New SQLiteConnection()
                    Agl.GcnRead = New SQLiteConnection()
                    Agl.GCnRep = New SQLiteConnection()


                    If Agl.PubIsDatabaseEncrypted = "N" Then
                        Agl.Gcn_ConnectionString = "Data Source=" & Agl.PubCompanyDBPath & Agl.PubDBName & ";Version=3;"
                        Agl.GCn.ConnectionString = Agl.Gcn_ConnectionString
                        Agl.GcnRead.ConnectionString = "Data Source=" & Agl.PubCompanyDBPath & Agl.PubDBName & ";Version=3;"

                        Agl.GCnRep_ConnectionString = "Data Source=" & Agl.PubCompanyDBPath & Agl.PubDBName & ";Version=3;"
                        Agl.GCnRep.ConnectionString = Agl.GCnRep_ConnectionString
                    Else
                        Agl.Gcn_ConnectionString = "Data Source=" & Agl.PubCompanyDBPath & Agl.PubDBName & ";Version=3;Password=" & AgLibrary.ClsConstant.PubDbPassword & ";"
                        Agl.GCn.ConnectionString = Agl.Gcn_ConnectionString
                        Agl.GcnRead.ConnectionString = "Data Source=" & Agl.PubCompanyDBPath & Agl.PubDBName & ";Version=3;Password=" & AgLibrary.ClsConstant.PubDbPassword & ";"

                        Agl.GCnRep_ConnectionString = "Data Source=" & Agl.PubCompanyDBPath & Agl.PubDBName & ";Version=3;Password=" & AgLibrary.ClsConstant.PubDbPassword & ";"
                        Agl.GCnRep.ConnectionString = Agl.GCnRep_ConnectionString
                    End If



                Else
                    Agl.GCn = New SqlClient.SqlConnection()
                    Agl.GcnRead = New SqlClient.SqlConnection()
                    Agl.GCnRep = New SqlClient.SqlConnection()


                    Agl.Gcn_ConnectionString = "Persist Security Info=False;User ID=sa;pwd=;Initial Catalog=" & Agl.PubDBName & ";Data Source=" & Agl.PubServerName
                    Agl.GCn.ConnectionString = Agl.Gcn_ConnectionString
                    Agl.GcnRead.ConnectionString = Agl.Gcn_ConnectionString

                    Agl.GCnRep_ConnectionString = "Persist Security Info=False;User ID=sa;pwd=;Initial Catalog=" & Agl.PubAgReportPath & ";Data Source=" & Agl.PubServerName
                    Agl.GCnRep.ConnectionString = Agl.GCnRep_ConnectionString
                End If

                Agl.GCn.Open()
                Agl.GcnRead.Open()
                Agl.GCnRep.Open()

                If Agl.XNull(Agl.PubImageDBName).ToString.Trim <> "" Then
                    Agl.GcnImage = New SQLiteConnection()

                    If UCase(Trim(Agl.PubChkPasswordSQL)) = "Y" Then
                        Agl.GCnImage_ConnectionString = "Persist Security Info=False;User ID='" & Agl.PubDBUserSQL & "';pwd=" & Agl.PubDBPasswordSQL & ";Initial Catalog=" & Agl.PubImageDBName & ";Data Source=" & Agl.PubServerName
                        Agl.GcnImage.ConnectionString = Agl.GCnImage_ConnectionString
                    Else
                        Agl.GCnImage_ConnectionString = "Persist Security Info=False;User ID='" & Agl.PubDBUserSQL & "';pwd=;Initial Catalog=" & Agl.PubImageDBName & ";Data Source=" & Agl.PubServerName
                        Agl.GcnImage.ConnectionString = Agl.GCnImage_ConnectionString
                    End If

                    Agl.GcnImage.Open()
                End If


                If Agl.PubUserName.ToUpper = "SA" Or Agl.PubUserName.ToUpper = "SUPER" Then
                    If Agl.PubServerName = "" Then
                        Agl.PubDivisionList = Agl.Dman_Execute("Select  group_concat('|' || div_code || '|' ,',')   from division", Agl.GCn).ExecuteScalar
                    Else
                        Agl.PubDivisionList = Agl.Dman_Execute("Select  '|' + div_code + '|' + ','   from division for xml path('')", Agl.GCn).ExecuteScalar
                        Agl.PubDivisionList = Agl.PubDivisionList.Substring(0, Agl.PubDivisionList.Length - 1)
                    End If
                Else
                    Agl.PubDivisionList = Agl.Dman_Execute("Select IfNull(DivisionList,'') From UserSite Where User_Name = '" & Agl.PubUserName & "' And CompCode = '" & Agl.PubCompCode & "' ", Agl.GCn).ExecuteScalar
                End If


                If Agl.PubDivisionList = "" Then
                    Agl.PubDivisionList = "''"
                Else
                    Agl.PubDivisionList = "" & Replace(Agl.PubDivisionList, "|", "'") & ""
                End If


                If Agl.PubUserName.ToUpper = "SA" Or Agl.PubUserName.ToUpper = "SUPER" Then
                    If Agl.PubServerName = "" Then
                        Agl.PubSiteList = Agl.Dman_Execute("Select  group_concat('|' || code || '|' ,',')   from SITEMAST", Agl.GCn).ExecuteScalar
                    Else
                        Agl.PubSiteList = Agl.Dman_Execute("Select '|' + code + '|' + ','   from SITEMAST for xml path('')", Agl.GCn).ExecuteScalar
                        Agl.PubSiteList = Agl.PubSiteList.Substring(0, Agl.PubSiteList.Length - 1)
                    End If
                Else
                    Agl.PubSiteList = Agl.Dman_Execute("Select IfNull(SiteList,'') From UserSite Where User_Name = '" & Agl.PubUserName & "' And CompCode = '" & Agl.PubCompCode & "' ", Agl.GCn).ExecuteScalar
                End If

                If Agl.PubSiteList = "" Then
                    Agl.PubSiteList = "''"
                Else
                    Agl.PubSiteList = "" & Replace(Agl.PubSiteList, "|", "'") & ""
                End If


                Agl.ApplyFeature_Area()
                Agl.ApplyFeature_Godown()
                Agl.ApplyFeature_SalesAgent()
                Agl.ApplyFeature_PurchaseAgent()
                Agl.ApplyFeature_SalesRep()
                Agl.ApplyFeature_RateType()



                Call IniEnviro()
                Agl.PubMachineName = AgLibrary.My.Computer.Name

                ''**********************************************************************************************************************
                ''**Refresh View Procedure will be called when a Common Database********************************************************
                ''******************Will be used for Master Tables *********************************************************************
                ''**********************************************************************************************************************
                ''Call Refresh_View(AgL.GcnMain, "")
                ''Call Refresh_View(AgL.ECompConn)
                ''**********************************************************************************************************************
                ''**********************************************************************************************************************
                ''**********************************************************************************************************************
            End If

        Catch Ex As Exception
            MsgBox(Ex.Message, MsgBoxStyle.Information, AgLibrary.ClsMain.PubMsgTitleInfo)
        End Try
    End Sub


    Sub IniEnviro()
        Dim mQry As String

        Try
            If Agl.IsTableExist("Enviro", Agl.GCn) Then
                mQry = "Select * From Enviro Where Div_Code = '" & Agl.PubDivCode & "' And Site_Code = '" & Agl.PubSiteCode & "' "
                Agl.PubDtEnviro = Agl.FillData(mQry, Agl.GCn).Tables(0)
                If Agl.PubDtEnviro.Rows.Count = 0 Then
                    mQry = "Select * From Enviro Where Div_Code = '" & Agl.PubDivCode & "' And Site_Code Is Null "
                    Agl.PubDtEnviro = Agl.FillData(mQry, Agl.GCn).Tables(0)
                    If Agl.PubDtEnviro.Rows.Count = 0 Then
                        mQry = "Select * From Enviro Where Div_Code Is Null And Site_Code = '" & Agl.PubSiteCode & "' "
                        Agl.PubDtEnviro = Agl.FillData(mQry, Agl.GCn).Tables(0)
                        If Agl.PubDtEnviro.Rows.Count = 0 Then
                            mQry = "Select * From Enviro Where Div_Code Is Null And Site_Code Is Null "
                            Agl.PubDtEnviro = Agl.FillData(mQry, Agl.GCn).Tables(0)
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            AgLibrary.ClsErrHandler.HandleException(ex, "IniEnviro {Enviro} Procedure of AgLibrary.ClsIniVariables")
        End Try


        Try
            If Agl.IsTableExist("DivisionSiteSetting", Agl.GCn) Then
                mQry = "Select * From DivisionSiteSetting Where Div_Code = '" & Agl.PubDivCode & "' And Site_Code = '" & Agl.PubSiteCode & "' "
                Agl.PubDtDivisionSiteSetting = Agl.FillData(mQry, Agl.GCn).Tables(0)
                If Agl.PubDtDivisionSiteSetting.Rows.Count = 0 Then
                    mQry = "Select * From DivisionSiteSetting Where Div_Code = '" & Agl.PubDivCode & "' And Site_Code Is Null "
                    Agl.PubDtDivisionSiteSetting = Agl.FillData(mQry, Agl.GCn).Tables(0)
                    If Agl.PubDtDivisionSiteSetting.Rows.Count = 0 Then
                        mQry = "Select * From DivisionSiteSetting Where Div_Code Is Null And Site_Code = '" & Agl.PubSiteCode & "' "
                        Agl.PubDtDivisionSiteSetting = Agl.FillData(mQry, Agl.GCn).Tables(0)
                        If Agl.PubDtDivisionSiteSetting.Rows.Count = 0 Then
                            mQry = "Select * From DivisionSiteSetting Where Div_Code Is Null And Site_Code Is Null "
                            Agl.PubDtDivisionSiteSetting = Agl.FillData(mQry, Agl.GCn).Tables(0)
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            AgLibrary.ClsErrHandler.HandleException(ex, "IniEnviro {Enviro} Procedure of AgLibrary.ClsIniVariables")
        End Try


        Try
            If Agl.IsTableExist("Enviro_LedgerAccounts", Agl.GCn) Then
                mQry = "Select * From Enviro_LedgerAccounts Where Div_Code = '" & Agl.PubDivCode & "' And Site_Code = '" & Agl.PubSiteCode & "' "
                Agl.PubDtEnviro_LedgerAccounts = Agl.FillData(mQry, Agl.GCn).Tables(0)
            End If
        Catch ex As Exception
            AgLibrary.ClsErrHandler.HandleException(ex, "IniEnviro {Enviro_LedgerAccounts} Procedure of AgLibrary.ClsIniVariables")
        End Try

        Try
            If Agl.IsTableExist("SMS_Enviro", Agl.GCn) Then
                mQry = "Select * From SMS_Enviro Where Site_Code = '" & Agl.PubSiteCode & "' "
                Agl.PubDtEnviro_SMS = Agl.FillData(mQry, Agl.GCn).Tables(0)

                With Agl.PubDtEnviro_SMS
                    If .Rows.Count > 0 Then
                        Agl.PubSmsAPI = Agl.XNull(.Rows(0)("APICode"))
                    End If
                End With

            End If
        Catch ex As Exception
            AgLibrary.ClsErrHandler.HandleException(ex, "IniEnviro {SMS_Enviro} Procedure of AgLibrary.ClsIniVariables")
        End Try

        Try
            If Agl.IsTableExist("EMail_Enviro", Agl.GCn) Then
                mQry = "Select * From EMail_Enviro Where Site_Code = '" & Agl.PubSiteCode & "' "
                Agl.PubDtEnviro_EMail = Agl.FillData(mQry, Agl.GCn).Tables(0)

                With Agl.PubDtEnviro_EMail
                    If .Rows.Count > 0 Then
                        Agl.PubOutGoingMailId = Agl.XNull(.Rows(0)("OutGoingMailId"))
                        Agl.PubOutGoingMailIdPassword = Agl.XNull(.Rows(0)("OutGoingMailIdPassword"))
                    End If
                End With

            End If
        Catch ex As Exception
            AgLibrary.ClsErrHandler.HandleException(ex, "IniEnviro {EMail_Enviro} Procedure of AgLibrary.ClsIniVariables")
        End Try

    End Sub

    Public Function Activate_Killer(Optional ByVal DCodify_CentralData_Path As Boolean = False) As Boolean
        Dim DsTemp As DataSet = Nothing
        Dim mQry As String
        Dim I As Integer = 0
        Dim mFlag As Boolean = False
        Dim bStrMessage$ = ""
        If Agl.PubKillerDate = "" Then Exit Function
        '-----------Killer------------====================================================

        ''===================< ********************* >=======================================================================================================================
        ''===================< Activate Killer >=======================================================================================================================
        ''=================< For This We Create >=====================================================================================================================
        ''==============< A Killer File In System32 Folder >=================================================================================================================
        ''===================< ********************* >=======================================================================================================================
        Agl.PubKillerFile = AgLibrary.ClsConstant.PubKillerFilePrefix & CDate(Agl.PubKillerDate).ToString("ddMMyy") & ".dll"
        Agl.PubKillerFile = Environment.GetFolderPath(Environment.SpecialFolder.System) & "\" & Agl.PubKillerFile
        If System.IO.File.Exists(Agl.PubKillerFile) Then
            mFlag = True
        Else
            If CDate(Agl.PubLoginDate) >= CDate(Agl.PubKillerDate) Then
                System.IO.File.Create(Agl.PubKillerFile)

                mFlag = True
            Else
                ''===================< ************************************** >=======================================================================================================================
                ''========================< First Deactivate Killer >=======================================================================================================================
                ''=================================< Means >==========================================================================================================================================
                ''=================< Decodify CentralData_Path Field In Company Table >===============================================================================================================
                ''===================< ************************************** >=======================================================================================================================

                DsTemp = Agl.FillData("SELECT C.Comp_Code, C.CentralData_Path  FROM Company C Where C.DBPrefix=" & Agl.Chk_Text(Agl.PubDBPrefix) & "", Agl.ECompConn)
                With DsTemp.Tables(0)
                    For I = 0 To .Rows.Count - 1
                        If InStr(.Rows(I)("CentralData_Path"), Agl.PubDBPrefix) = 0 Then
                            mQry = "Update Company Set CentralData_Path='" & Agl.DCODIFY(.Rows(I)("CentralData_Path")) & "' Where Comp_Code='" & .Rows(I)("Comp_Code") & "'"
                            Agl.Dman_ExecuteNonQry(mQry, Agl.ECompConn)
                        End If
                    Next
                End With
                DsTemp = Nothing

            End If
        End If

        If mFlag = True Then
            DsTemp = Agl.FillData("SELECT C.Comp_Code, C.CentralData_Path  FROM Company C Where C.DBPrefix=" & Agl.Chk_Text(Agl.PubDBPrefix) & " ", Agl.ECompConn)
            With DsTemp.Tables(0)
                For I = 0 To .Rows.Count - 1
                    If InStr(.Rows(I)("CentralData_Path"), Agl.PubDBPrefix) > 0 Then
                        mQry = "Update Company Set CentralData_Path='" & Agl.CODIFY(.Rows(I)("CentralData_Path")) & "' Where Comp_Code='" & .Rows(I)("Comp_Code") & "'"
                        Agl.Dman_ExecuteNonQry(mQry, Agl.ECompConn)
                    End If
                Next
            End With
            DsTemp = Nothing

            'bStrMessage = "Run-time error '-21365445':" & vbCrLf & vbCrLf & "Allowed memory size of X bytes exhausted (tried to allocate Y bytes)"
            'bStrMessage = "A connection could not be established because the security token is larger than the maximum allowed by the network protocol."

            bStrMessage = "The timeout of the user instance after no connection is made on the server."
            MsgBox(bStrMessage, MsgBoxStyle.Information, "System Error")

        End If

        Activate_Killer = mFlag

    End Function

    Private Sub Refresh_View(ByVal mConn As SQLiteConnection, Optional ByVal mBaseTableOperator As String = "Not")
        Dim DsComp As DataSet
        Dim mQRY As String = "", mTable_Name As String = "", mTable_Catalog As String = "", mTable_Schema As String = ""
        Dim I As Integer
        Try
            mQRY = "Select * From INFORMATION_SCHEMA.Tables Where Table_Type='BASE TABLE' And Table_Name " & mBaseTableOperator & " In(" & Agl.BaseTableList & ")"
            DsComp = Agl.FillData(mQRY, mConn)

            With DsComp.Tables(0)
                If .Rows.Count > 0 Then
                    For I = 0 To .Rows.Count - 1
                        mTable_Catalog = .Rows(I)("Table_Catalog")
                        mTable_Schema = .Rows(I)("Table_Schema")
                        mTable_Name = .Rows(I)("Table_Name")

                        mQRY = "Select IfNull(Count(Table_Name),0) As Cnt From INFORMATION_SCHEMA.Tables Where Table_Name='" & mTable_Name & "' And Table_Type='VIEW'"
                        Agl.ECmd = Agl.Dman_Execute(mQRY, Agl.GCn)
                        If Agl.ECmd.ExecuteScalar > 0 Then
                            mQRY = "Drop View [" & mTable_Name & "]"
                            Agl.Dman_ExecuteNonQry(mQRY, Agl.GCn)
                        End If
                        mQRY = "  View [" & mTable_Name & "] As " &
                                " Select * From " & mTable_Catalog & "." & mTable_Schema & ".[" & mTable_Name & "]"
                        Agl.Dman_ExecuteNonQry(mQRY, Agl.GCn)
                    Next
                End If
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            DsComp = Nothing
        End Try
    End Sub


    Public Sub ProcIniSiteDetail(ByVal SiteCodeStr As String, ByVal AgIniVarObj As AgLibrary.ClsIniVariables)
        Dim DtTemp As DataTable
        Dim mQry As String
        Try
            If SiteCodeStr.Trim <> "" Then
                mQry = "Select S.*, C.CityName, 
                        Case IfNull(S.Ho_Yn,'N') When 'N' Then 0 When '' Then 0 Else 1 End As IsHO, 
                        S.ManualCode, S.Add1, S.Add2, S.Add3, S.Phone, S.Mobile, S.PinNo, C.State 
                        From SiteMast S 
                        Left Join City C On S.City_Code = C.CityCode                         
                        Where S.Code = '" & SiteCodeStr & "'"

                DtTemp = Agl.FillData(mQry, Agl.ECompConn).TABLES(0)
                If DtTemp.Rows.Count > 0 Then

                    Agl.PubSiteCode = DtTemp.Rows(0).Item("Code")                    
                    Agl.PubSiteName = DtTemp.Rows(0).Item("Name")
                    Agl.PubSiteManualCode = Agl.XNull(DtTemp.Rows(0).Item("ManualCode"))
                    Agl.PubSiteAdd1 = Agl.XNull(DtTemp.Rows(0).Item("Add1"))
                    Agl.PubSiteAdd2 = Agl.XNull(DtTemp.Rows(0).Item("Add2"))
                    Agl.PubSiteAdd3 = Agl.XNull(DtTemp.Rows(0).Item("Add3"))
                    Agl.PubSiteCity = Agl.XNull(DtTemp.Rows(0).Item("CityName"))
                    Agl.PubSiteCityCode = Agl.XNull(DtTemp.Rows(0).Item("City_Code"))
                    Agl.PubSitePinNo = Agl.XNull(DtTemp.Rows(0).Item("PinNo"))
                    Agl.PubSitePhone = Agl.XNull(DtTemp.Rows(0).Item("Phone"))
                    Agl.PubSiteMobile = Agl.XNull(DtTemp.Rows(0).Item("Mobile"))
                    Agl.PubSiteStateCode = Agl.XNull(DtTemp.Rows(0).Item("State"))

                    Agl.PubSiteCodeDisplay = "('" & DtTemp.Rows(0).Item("Code") & "')"
                    Agl.PubLogSiteName = Agl.PubSiteName

                    Agl.PubIsHo = Agl.VNull(DtTemp.Rows(0).Item("IsHO"))

                    AgIniVarObj.ProcSwapSiteCompanyDetail()
                    AgIniVarObj.IniEnviro()

                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            DtTemp.Dispose()
        End Try
    End Sub


    Public Sub ProcSwapSiteCompanyDetail()
        If Agl.PubSiteCode.Trim <> "" Then
            ''Assign Company Address Detail Into Registered Office Address Variables
            Agl.PubRegOfficeName = Agl.PubCompName
            Agl.PubRegOfficeAdd1 = Agl.PubCompAdd1
            Agl.PubRegOfficeAdd2 = Agl.PubCompAdd2
            Agl.PubRegOfficeAdd3 = Agl.PubCompAdd3
            Agl.PubRegOfficeCity = Agl.PubCompCity
            Agl.PubRegOfficePin = Agl.PubCompPinCode
            Agl.PubRegOfficePhone = Agl.PubCompPhone
            Agl.PubRegOfficeMobile = ""


            ''Assign Site Address Detail Into Company Address Detail Variables
            If Agl.PubUseSiteNameAsCompanyName Then Agl.PubCompName = Agl.PubSiteName Else Agl.PubCompName = Agl.PubDivName
            Agl.PubCompAdd1 = Agl.PubSiteAdd1
            Agl.PubCompAdd2 = Agl.PubSiteAdd2
            Agl.PubCompAdd3 = Agl.PubSiteAdd3
            Agl.PubCompCity = Agl.PubSiteCity
            Agl.PubCompPinCode = Agl.PubSitePinNo
            Agl.PubCompPhone = Agl.PubSitePhone
            ''=====================================================


            Dim mQry As String = "Select Sg.Address, C.CityName, Sg.Pin, Sg.Phone
                    From Division D 
                    LEFT JOIN SubGroup Sg On D.SubCode = Sg.SubCode
                    LEFT JOIN City C On Sg.CityCode = C.CityCode
                    Where D.Div_Code = '" & Agl.PubDivCode & "'"
            Dim DtTempDivision As DataTable = Agl.FillData(mQry, Agl.GcnMain).Tables(0)

            If DtTempDivision.Rows.Count > 0 Then
                If Agl.PubCompAdd1 = "" Or Agl.PubCompAdd1 Is Nothing Then
                    Agl.PubCompAdd1 = Agl.XNull(DtTempDivision.Rows(0)("Address"))
                    Agl.PubCompAdd2 = ""
                    Agl.PubCompAdd3 = ""
                    Agl.PubCompCity = Agl.XNull(DtTempDivision.Rows(0)("CityName"))
                    Agl.PubCompPinCode = Agl.XNull(DtTempDivision.Rows(0)("Pin"))
                    Agl.PubCompPhone = Agl.XNull(DtTempDivision.Rows(0)("Phone"))
                End If
            End If
        End If
    End Sub

    Public Function FunGetUserPermission(ByVal StrModule As String, ByVal StrSender As String, ByVal StrSenderText As String, _
                                            Optional ByRef DTUP As DataTable = Nothing)
        Dim StrUserPermission As String
        Dim mQry As String
        'For User Permission Open

        If Agl.StrCmp(Agl.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Then
            StrUserPermission = "AEDP"
        Else
            StrUserPermission = "****"
        End If

        If Agl.PubIsUserAdmin Then
            mQry = "Select Permission From User_Permission Where UserName='SA' And MnuModule='" & StrModule & "' And MnuName='" & StrSender & "'"
        Else
            mQry = "Select Permission From User_Permission Where UserName='" & Agl.PubUserName & "' And MnuModule='" & StrModule & "' And MnuName='" & StrSender & "'"
        End If
        DTUP = Agl.FillData(mQry, Agl.ECompConn).tables(0)
        If DTUP.Rows.Count > 0 Then
            StrUserPermission = Agl.XNull(DTUP.Rows(0).Item("Permission"))
        End If
        DTUP.Clear()
        DTUP = Nothing

        If Agl.PubOfflineApplicable And Agl.PubSiteCode <> Agl.PubSiteCodeActual Then
            StrUserPermission = Replace(Replace(Replace(StrUserPermission, "A", "*"), "E", "*"), "D", "*")
        End If

        If Agl.PubIsLogInProjectActive Then
            If Agl.IsTableExist("EntryPointPermission", Agl.GCn) Then
                mQry = "SELECT " & IIf(Agl.PubOfflineApplicable, "Ep.IsOnLineEntry", "Ep.IsOffLineEntry") & "  " & _
                        " FROM EntryPointPermission Ep " & _
                        " WHERE  MnuModule='" & StrModule & "' And MnuName='" & StrSender & "'"

                If Agl.Dman_Execute(mQry, Agl.GCn).ExecuteScalar = False Then
                    StrUserPermission = Replace(Replace(Replace(StrUserPermission, "A", "*"), "E", "*"), "D", "*")
                End If
            End If
        End If

        mQry = "Select GroupText As UP From User_Control_Permission " & _
                " Where UserName='" & IIf(Agl.StrCmp(Agl.PubUserName, AgLibrary.ClsConstant.PubSuperUserName), "SA", Agl.PubUserName) & "' " & _
                " And MnuModule='" & StrModule & "' " & _
                " And MnuName='" & StrSender & "' " & _
                " " & IIf(Agl.PubIsHo, " And  1=2 ", "") & " "
        DTUP = Agl.FillData(mQry, Agl.ECompConn).Tables(0)
        'For User Permission End 

        'If Agl.PubIsHo Then
        '    StrUserPermission = "****"
        'End If

        Return StrUserPermission
    End Function

End Class
