Public Class ClsMain
    Public CFOpen As New ClsFunction

    Sub New(ByVal AgLibVar As AgLibrary.ClsMain)
        AgL = AgLibVar
        AgIniVar = New AgLibrary.ClsIniVariables(AgL)
    End Sub


    Public Sub UpdateTableStructure()
        Dim mQry$ = "", mEnviroId$ = ""

        '==================================================================================================================================================================================
        '============================<UPDATES FOR SITE WISE>===============================================================================================================================
        '=====================================FEEDING======================================================================================================================================
        '==================================================================================================================================================================================
        Call AddNewTable()

        Call AddNewField()

        Call EditField()

        mQry = "Insert Into Table_SearchField (Table_Name) " & _
               "Select S.Table_Name From Information_Schema.Tables S  " & _
               "Left Join Table_SearchField T ON S.TABLE_NAME = T.Table_Name COLLATE DATABASE_DEFAULT   " & _
               "WHERE T.Table_Name IS Null "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

        If AgL.Dman_Execute("Select IsNull(Count(*),0) Cnt FROM Division D Where IsNull(D.SitewiseV_No,0)<>0 And D.Div_Code='" & AgL.PubDivCode & "' ", AgL.GcnMain).ExecuteScalar > 0 Then
            If AgL.Dman_Execute("Select IsNull(Count(*),0) Cnt FROM Company C Where IsNull(C.Site_Code,'')<>'' And C.Div_Code='" & AgL.PubDivCode & "' ", AgL.GcnMain).ExecuteScalar > 0 Then
                AgL.Dman_ExecuteNonQry("Update Company Set Site_Code=Null Where Div_Code='" & AgL.PubDivCode & "' ", AgL.GcnMain)
            End If

            AgL.Dman_ExecuteNonQry("Update Voucher_Type Set SiteWise=1 Where IsNull(SiteWise,0)=0 ", AgL.GCn)

            'Inserting Record in Voucher Prefix Table----------------------------------
            mQry = "Select IsNull(Count(*),0) Cnt " & _
                    " From Voucher_Prefix V " & _
                    " Where V.Site_Code='" & AgL.PubSiteCode & "' And V.Div_Code='" & AgL.PubDivCode & "' "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar = 0 Then
                mQry = "Insert Into Voucher_Prefix(Site_Code,V_Type,Date_From,Date_To,Prefix,Start_Srl_No,Div_Code)" & _
                        " (Select '" & AgL.PubSiteCode & "' As Site_Code,V_Type,Date_From,Date_To,Prefix,0 As Start_Srl_No,'" & AgL.PubDivCode & "' As Div_Code From Voucher_Prefix Where Site_Code='1' And Div_Code='" & AgL.PubDivCode & "')"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
            Else
                mQry = "Insert Into Voucher_Prefix(Site_Code,V_Type,Date_From,Date_To,Prefix,Start_Srl_No,Div_Code)" & _
                        " (Select '" & AgL.PubSiteCode & "' As Site_Code, V1.V_Type, V1.Date_From, V1.Date_To, V1.Prefix, 0 As Start_Srl_No,'" & AgL.PubDivCode & "' As Div_Code " & _
                        "   From Voucher_Prefix V1 " & _
                        "   Where V1.Site_Code='1' And V1.Div_Code='" & AgL.PubDivCode & "' And V1.V_Type Not In " & _
                        "   (Select V2.V_Type From Voucher_Prefix V2 Where V2.Site_Code='" & AgL.PubSiteCode & "' And V2.Div_Code='" & AgL.PubDivCode & "') )"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
            End If

            'Inserting Record in Enviro Table----------------------------------
            mEnviroId = AgL.GetMaxId("Enviro", "ID", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 2, True, True, , AgL.Gcn_ConnectionString)

            mQry = "Select IsNull(Count(*),0) Cnt " & _
                    " From Enviro E " & _
                    " Where E.Site_Code='" & AgL.PubSiteCode & "' And E.Div_Code='" & AgL.PubDivCode & "' "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar = 0 Then
                mQry = "Insert Into Enviro(Id,Site_Code,Div_Code, " & _
                        " U_EntDt, PreparedBy, U_AE) Values (" & _
                        " '" & mEnviroId & "', '" & AgL.PubSiteCode & "', '" & AgL.PubDivCode & "', " & _
                        " '" & Format(AgL.PubLoginDate, "Short Date") & "', '" & AgL.PubUserName & "', 'A') "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
            End If

            'Inserting Record in FaEnviro Table----------------------------------
            'mQry = "Select IsNull(Count(*),0) Cnt " & _
            '        " From FaEnviro E " & _
            '        " Where E.Site_Code='" & AgL.PubSiteCode & "' "

            'If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar = 0 Then
            '    mQry = "INSERT INTO FaEnviro ( " & _
            '            " Age1, Age2, Age3, Age4, Age5, Age6, Amt1, Amt2, Amt3, Amt4, Amt5, Amt6,  " & _
            '            " TagadaHeader1, TagadaHeader2, TagadaHeader3, TagadaHeader4, TagadaHeader5, " & _
            '            " TagadaFooter1, TagadaFooter2, TagadaFooter3, TagadaFooter4, TagadaFooter5, " & _
            '            " CreditLimit, DebitLimit, NegativeCashBalance, ShowGroup, DonotShowGroup, " & _
            '            " ShowCurrentBalance, VerticleBalanceSheet, ShowQty, ShowCityName, LedDivCode, " & _
            '            " LedSiteCode, LedPrefix, linefiller, daterfill, titlerfill, pagenofill, " & _
            '            " RunPIF, FilterAC, DateLock, AddressHelp, CashBookBalance, MonthTotal, OnLineAdjustment, " & _
            '            " OpStockQTY, OpStockValue, ClStockQTY, ClStockValue, CashBookPage, RepToBy, PreBal, " & _
            '            " PDCDt, ToBy, CityNameDisp, Site_Code) " & _
            '            " ( Select Age1, Age2, Age3, Age4, Age5, Age6, Amt1, Amt2, Amt3, Amt4, Amt5, Amt6,  " & _
            '            " TagadaHeader1, TagadaHeader2, TagadaHeader3, TagadaHeader4, TagadaHeader5, " & _
            '            " TagadaFooter1, TagadaFooter2, TagadaFooter3, TagadaFooter4, TagadaFooter5, " & _
            '            " CreditLimit, DebitLimit, NegativeCashBalance, ShowGroup, DonotShowGroup, " & _
            '            " ShowCurrentBalance, VerticleBalanceSheet, ShowQty, ShowCityName, LedDivCode, " & _
            '            " LedSiteCode, LedPrefix, linefiller, daterfill, titlerfill, pagenofill, " & _
            '            " RunPIF, FilterAC, DateLock, AddressHelp, CashBookBalance, MonthTotal, OnLineAdjustment, " & _
            '            " OpStockQTY, OpStockValue, ClStockQTY, ClStockValue, CashBookPage, RepToBy, PreBal, " & _
            '            " PDCDt, ToBy, CityNameDisp, '" & AgL.PubSiteCode & "' As Site_Code " & _
            '            " From FaEnviro Where Site_Code='1') "
            '    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
            'End If

        End If
        '==================================================================================================================================================================================
        '==================================================================================================================================================================================

        'AgL.CreateNCat(AgL.GCn, PLib.PubIncomeNCat, PLib.PubBinaryPerfectSlaveIncome_VType, "Binary Perfect Slave Income", AgL.PubSiteCode)
        'AgL.CreateVType(AgL.GCn, PLib.PubIncomeNCat, PLib.PubBinaryPerfectSlaveIncome_VType, PLib.PubBinaryPerfectSlaveIncome_VType, "Binary Perfect Slave Income", PLib.PubBinaryPerfectSlaveIncome_VType, AgL.PubUserName, AgL.PubLoginDate, AgL.PubStartDate, AgL.PubEndDate, AgL.PubSiteCode, AgL.PubDivCode, False, AgL.PubSitewiseV_No)
        If MsgBox("Want To Create ""SQL Procedures""!...", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Utility") = MsgBoxResult.Yes Then
            Call CreateSqlProcedures()
        End If

        If MsgBox("Want To Create ""SQL Triggers""!...", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Utility") = MsgBoxResult.Yes Then
            Call CreateSqlTriggers()
        End If

    End Sub

    Private Sub AddNewTable()
        Dim mQry$



        Try
            If Not AgL.IsTableExist("Synchronise_Error", AgL.GCn) Then
                mQry = "CREATE TABLE dbo.Synchronise_Error " & _
                        "	( " & _
                        "	RowId   BIGINT NOT NULL, " & _
                        "	Message NVARCHAR (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL, " & _
                        "	CONSTRAINT PK_Synchronise_Error PRIMARY KEY (RowId) " & _
                        "	) "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try



        Try
            If Not AgL.IsTableExist("Log_TableRecords", AgL.GCn) Then
                mQry = "CREATE TABLE [dbo].[Log_TableRecords]( " & _
                    "	[RowId] [bigint] IDENTITY(1,1) NOT NULL, " & _
                    "	[SearchKey] [nvarchar](21) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL, " & _
                    "	[AED] [nvarchar](1) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL, " & _
                    "	[UpdateDate] [datetime] NOT NULL, " & _
                    "	[TableName] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL, " & _
                    "	[Site] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL, " & _
                    "	[UploadDate] [smalldatetime] NULL, " & _
                    " CONSTRAINT [PK_Log_TableRecords] PRIMARY KEY CLUSTERED  " & _
                    "( " & _
                    "	[SearchKey] ASC, " & _
                    "	[AED] ASC, " & _
                    "	[UpdateDate] ASC " & _
                    ")WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY] " & _
                    ") ON [PRIMARY]"

                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
            End If
        Catch ex As Exception
            MsgBox(ex.Message & vbCrLf & " In Creating Log_Table_Records Table of Add New Table Procedure ")
        End Try


        Try
            If Not AgL.IsTableExist("Table_SearchField", AgL.GCn) Then
                mQry = "CREATE TABLE [dbo].[Table_SearchField]( " & _
                        "	[RowId] [bigint] IDENTITY(1,1)  NOT NULL, " & _
                        "	[TABLE_NAME] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL, " & _
                        "	[SEARCH_FIELD] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL " & _
                        ") ON [PRIMARY] "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


        Try
            If Not AgL.IsTableExist("Log_TablePermission", AgL.GCn) Then
                mQry = "CREATE TABLE [dbo].[Log_TablePermission]( " & _
                        "	[CreateLogYn] Bit NOT NULL Default 0) "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Try
            mQry = "CREATE TABLE dbo.EntryPointPermission " & _
                    " ( " & _
                    " MnuModule      NVARCHAR (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL, " & _
                    " MnuName        NVARCHAR (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL, " & _
                    " IsOnLineEntry  BIT CONSTRAINT DF_EntryPointPermission_IsOnLineEntry DEFAULT ((0)) NOT NULL, " & _
                    " IsOffLineEntry BIT CONSTRAINT DF_EntryPointPermission_IsOffLineEntry DEFAULT ((1)) NOT NULL, " & _
                    " CONSTRAINT PK_EntryPointPermission PRIMARY KEY (MnuModule,MnuName) " & _
                    " )"

            If Not AgL.IsTableExist("EntryPointPermission", AgL.GCn) Then AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

            If AgL.PubOfflineApplicable Then
                If Not AgL.IsTableExist("EntryPointPermission", AgL.GcnSite) Then AgL.Dman_ExecuteNonQry(mQry, AgL.GcnSite)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try



    End Sub

    Private Sub AddNewField()
        Dim DtTemp As DataTable = Nothing
        Dim I As Integer = 0
        Dim bQry$ = ""

        AgL.AddNewField(AgL.GCn, "User_Permission", "LogSystem", "Bit", 0)
        AgL.AddNewField(AgL.GCn, "User_Permission", "ControlPermissionGroups", "nVarchar(Max)")
        AgL.AddNewField(AgL.GCn, "UserSite", "DivisionList", "nVarchar(250)")

        '============================< UserMast >==================================================
        AgL.AddNewField(AgL.GCn, "UserMast", "ModuleList", "nVarchar(Max)")
        AgL.AddNewField(AgL.GCn, "UserMast", "SeniorName", "nVarchar(10)")
        If AgL.IsFieldExist("SeniorName", "UserMast", AgL.GCn) Then
            AgL.AddForeignKey(AgL.GCn, "FK_UserMast_SeniorName", "UserMast", "UserMast", "User_Name", "SeniorName")
        End If

        AgL.AddNewField(AgL.GCn, "UserMast", "MainStreamCode", "nVarChar(Max)")
        If AgL.IsFieldExist("MainStreamCode", "UserMast", AgL.GCn) Then
            bQry = "Update UserMast Set MainStreamCode = '" & AgLibrary.ClsConstant.StrIniMainStreamCode & "' Where User_Name = 'SA' And MainStreamCode Is Null "
            AgL.Dman_ExecuteNonQry(bQry, AgL.GCn)
        End If

        AgL.AddNewField(AgL.GCn, "UserMast", "EMail", "nVarchar(100)")
        AgL.AddNewField(AgL.GCn, "UserMast", "Mobile", "nVarchar(10)")
        AgL.AddNewField(AgL.GCn, "UserMast", "IsActive", "bit", 1)
        AgL.AddNewField(AgL.GCn, "UserMast", "InActiveDate", "SmallDateTime")
        '============================< ********** >==================================================

        'AgL.AddNewField(AgL.GCn, "LogTable", "V_Date", "SmallDateTime")
        'AgL.AddNewField(AgL.GCn, "LogTable", "V_Date", "SmallDateTime")
        'AgL.AddNewField(AgL.GCn, "LogTable", "SubCode", "nVarChar(10)")
        'AgL.AddNewField(AgL.GCn, "LogTable", "PartyDetail", "nVarChar(255)")
        'AgL.AddNewField(AgL.GCn, "LogTable", "Amount", "Float")
        'AgL.AddNewField(AgL.GCn, "LogTable", "Site_Code", "nVarChar(2)")
        'AgL.AddNewField(AgL.GCn, "LogTable", "Div_Code", "nVarChar(1)")

        'AgL.AddNewField(AgL.ECompConn, "City", "District", "nVarChar(50)")

        'AgL.AddNewField(AgL.ECompConn, "City", "Circle", "nVarChar(50)")
        'If AgL.PubOfflineApplicable Then
        '    AgL.AddNewField(AgL.GcnSite, "City", "Circle", "nVarChar(50)")
        'End If


        'AgL.AddNewField(AgL.GcnMain, "Division", "SitewiseV_No", "bit", "0")

        'AgL.AddNewField(AgL.GCn, "SiteMast", "ManualCode", "nVarChar(20)")
        'AgL.AddNewField(AgL.GCn, "SiteMast", "AcCode", "nVarChar(10)")        
        'AgL.AddNewField(AgL.GCn, "SiteMast", "SqlServer", "nVarChar(50)")
        'AgL.AddNewField(AgL.GCn, "SiteMast", "DataPath", "nVarChar(50)")
        'AgL.AddNewField(AgL.GCn, "SiteMast", "DataPathMain", "nVarChar(50)")
        'AgL.AddNewField(AgL.GCn, "SiteMast", "SqlUser", "nVarChar(50)")
        'AgL.AddNewField(AgL.GCn, "SiteMast", "SqlPassword", "nVarChar(50)")

        'AgL.AddNewField(AgL.GCn, "SubGroup", "CommonAc", "bit", "0")

        'AgL.AddNewField(AgL.GCn, "Log_TableRecords", "SearchField", "nVarChar(50)")

        'AgL.AddNewField(AgL.GCn, "Table_SearchField", "TransactionYn", "Bit", "0")
        'AgL.AddNewField(AgL.GCn, "Table_SearchField", "LineItemYn", "Bit", "0")

        'AgL.AddNewField(AgL.GCn, "Enviro", "ServiceTaxPer", "Float", "0")
        'AgL.AddNewField(AgL.GCn, "Enviro", "ECessPer", "Float", "0")
        'AgL.AddNewField(AgL.GCn, "Enviro", "HECessPer", "Float", "0")
        'AgL.AddNewField(AgL.GCn, "Table_SearchField", "UniqueField", "nVarchar(Max)")
        'AgL.AddNewField(AgL.GCn, "Log_TableRecords", "UniqueField", "nVarchar(Max)")
        'AgL.AddNewField(AgL.GCn, "Log_TableRecords", "UniqueKey", "nVarchar(Max)")
        'AgL.AddNewField(AgL.GcnMain, "User_Permission", "MainStreamCode", "nVarchar(Max)")
        'AgL.AddNewField(AgL.GcnMain, "User_Permission", "GroupLevel", "Numeric")
        'AgL.AddNewField(AgL.GCn, "EntryPointPermission", "ApprovedYn", "Bit", "0")

        'If Not AgL.IsFieldExist("AgentPayoutPer", "Commission_Post", AgL.GCn) Then
        '    AgL.AddNewField(AgL.GCn, "Commission_Post", "AgentPayoutPer", "Numeric(18,2)", "0")
        '    AgL.Dman_ExecuteNonQry("Update Commission_Post Set AgentPayoutPer=100", AgL.GCn)
        'End If



        'DtTemp = AgL.FillData("SELECT Table_Catalog + '.' + Table_Schema + '.' + Table_Name As Table_Name FROM INFORMATION_SCHEMA.Tables WHERE TABLE_TYPE ='base table'", AgL.GCn).Tables(0)
        'If DtTemp.Rows.Count > 0 Then
        '    For I = 0 To DtTemp.Rows.Count - 1
        '        AgL.AddNewField(AgL.GCn, AgL.XNull(DtTemp.Rows(I)("Table_Name")), "RowId", "BigInt Identity", , False)
        '        AgL.AddNewField(AgL.GCn, AgL.XNull(DtTemp.Rows(I)("Table_Name")), "UpLoadDate", "SmallDateTime")
        '        AgL.AddNewField(AgL.GCn, AgL.XNull(DtTemp.Rows(I)("Table_Name")), "ApprovedBy", "nVarchar(10)")
        '        AgL.AddNewField(AgL.GCn, AgL.XNull(DtTemp.Rows(I)("Table_Name")), "ApprovedDate", "SmallDateTime")
        '        AgL.AddNewField(AgL.GCn, AgL.XNull(DtTemp.Rows(I)("Table_Name")), "GPX1", "nVarchar(255)")
        '        AgL.AddNewField(AgL.GCn, AgL.XNull(DtTemp.Rows(I)("Table_Name")), "GPX2", "nVarchar(255)")
        '        AgL.AddNewField(AgL.GCn, AgL.XNull(DtTemp.Rows(I)("Table_Name")), "GPN1", "Float")
        '        AgL.AddNewField(AgL.GCn, AgL.XNull(DtTemp.Rows(I)("Table_Name")), "GPN2", "Float")
        '    Next
        'End If

        'DtTemp = AgL.FillData("SELECT Table_Catalog + '.' + Table_Schema + '.' + Table_Name As Table_Name FROM INFORMATION_SCHEMA.Tables WHERE TABLE_TYPE ='base table'", AgL.ECompConn).Tables(0)
        'If DtTemp.Rows.Count > 0 Then
        '    For I = 0 To DtTemp.Rows.Count - 1
        '        AgL.AddNewField(AgL.ECompConn, AgL.XNull(DtTemp.Rows(I)("Table_Name")), "RowId", "BigInt Identity", , False)
        '        AgL.AddNewField(AgL.ECompConn, AgL.XNull(DtTemp.Rows(I)("Table_Name")), "UpLoadDate", "SmallDateTime")
        '    Next
        'End If

    End Sub

    Sub EditField()
        Try
            AgL.EditField("LogTable", "DocId", "nVarChar(36)", AgL.GCn, False)
            If AgL.PubOfflineApplicable Then
                AgL.EditField("LogTable", "DocId", "nVarChar(36)", AgL.GcnSite, False)
            End If


            If AgL.IsTableExist("Sms_Trans", AgL.GCn) Then
                If AgL.IsFieldExist("Site_Code", "Sms_Trans", AgL.GCn) Then
                    AgL.AddForeignKey(AgL.GCn, "FK_Sms_Trans_SiteMast", "SiteMast", "Sms_Trans", "Code", "Site_Code")
                End If
            End If

            If AgL.IsTableExist("EMail_OutBox", AgL.GCn) Then
                If AgL.IsFieldExist("Site_Code", "EMail_OutBox", AgL.GCn) Then
                    AgL.AddForeignKey(AgL.GCn, "FK_EMail_OutBox_SiteMast", "SiteMast", "EMail_OutBox", "Code", "Site_Code")
                End If
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Sub CreateSqlProcedures()
        Dim mQry$

        Try
            AgL.IsProcedureExist("Create_LogTableRecords", AgL.GCn, True)

            mQry = "CREATE PROCEDURE dbo.Create_LogTableRecords 	@TableName NVARCHAR(128), 	@AED NVARCHAR(1), 	@SearchKey NVARCHAR(21), @UniqueKey NVARCHAR(MAX) AS  " & _
            "	BEGIN 	 " & _
            "		SET NOCOUNT ON; 	 " & _
            "		DECLARE @SearchField NVARCHAR(128); 	 " & _
            "		DECLARE @UniqueField NVARCHAR(Max); 	 " & _
            "		DECLARE @UpdateDate DATETIME;  			 " & _
            "		DECLARE @mCreateLogYn BIT ; " & _
            "		SET @mCreateLogYn=(SELECT TOP 1 CreateLogYn FROM Log_TablePermission ) " & _
            "		IF (@mCreateLogYn=1) " & _
            "		Begin " & _
            "			SET @SearchField=(SELECT Search_Field FROM Table_SearchField WHERE TABLE_NAME =@TableName) 	 " & _
            "			SET @UniqueField=(SELECT UniqueField FROM Table_SearchField WHERE TABLE_NAME =@TableName) 	 " & _
            "			SET @UpdateDate=(SELECT GetDate())	     	 	 " & _
            "			INSERT INTO Log_TableRecords 		    " & _
            "			(SearchKey,AED,UpdateDate,TableName,Site,UploadDate,SearchField, UniqueKey, UniqueField) 	VALUES (@searchkey,@aed,@updatedate,@tablename,'',Null,@searchfield, @UniqueKey, @UniqueField)	  " & _
            "		end " & _
            "	END  "

            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)




        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        ''#################### <<Next Procedure>> ###########################
        Try
            'AgL.IsProcedureExist("<Procedure Name>", AgL.GCn, True)

            'mQry = "CREATE PROCEDURE [dbo].[<Procedure Name>] " & _
            '        "	<@ParamName ParamType> " & _
            '        "AS " & _
            '        "BEGIN " & _
            '        "	SET NOCOUNT ON; " & _
            '        "	<Executable Code>; " & _
            '        "END "
            'AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Sub CreateSqlTriggers()
        Dim mQry$
        Dim DtTemp As DataTable
        Dim I As Integer

        mQry = "SELECT * FROM Table_SearchField WHERE IsNull(Search_Field,'')<>''"
        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
        With DtTemp
            For I = 0 To DtTemp.Rows.Count - 1
                mQry = "IF EXISTS (SELECT * FROM sys.Triggers WHERE name ='Tr_" & AgL.XNull(.Rows(I)("Table_Name")) & "_A') "
                mQry += " Drop Trigger Tr_" & AgL.XNull(.Rows(I)("Table_Name")) & "_A "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
                mQry = "IF EXISTS (SELECT * FROM sys.Triggers WHERE name ='Tr_" & AgL.XNull(.Rows(I)("Table_Name")) & "_E') "
                mQry += " Drop Trigger Tr_" & AgL.XNull(.Rows(I)("Table_Name")) & "_E"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
                mQry = "IF EXISTS (SELECT * FROM sys.Triggers WHERE name ='Tr_" & AgL.XNull(.Rows(I)("Table_Name")) & "_D') "
                mQry += " Drop Trigger Tr_" & AgL.XNull(.Rows(I)("Table_Name")) & "_D "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)



                mQry = "CREATE TRIGGER Tr_" & AgL.XNull(.Rows(I)("Table_Name")) & "_A ON " & AgL.XNull(.Rows(I)("Table_Name")) & "  " & _
                        "FOR INSERT " & _
                        "AS " & _
                        "DECLARE @Code NVARCHAR(21) " & _
                        "DECLARE @UniqueKey NVARCHAR(Max) " & _
                        "DECLARE @UDate DateTime " & _
                        "IF EXISTS (SELECT * FROM Inserted) 	 " & _
                        "	BEGIN  " & _
                        "		SET @Code=(SELECT Top 1 " & AgL.XNull(.Rows(I)("Search_Field")) & " FROM Inserted i) " & _
                        "		SET @UniqueKey=(SELECT Top 1 " & AgL.XNull(.Rows(I)("UniqueField")) & " FROM Inserted i) " & _
                        "       Set @UDate = (SELECT GetDate()) " & _
                        "       IF NOT EXISTS(SELECT * FROM Log_TableRecords WHERE SearchKey = @Code And UniqueKey=@UniqueKey AND TableName = '" & AgL.XNull(.Rows(I)("Table_Name")) & "' And AED='A' AND UpdateDate =@UDate) " & _
                        "           BEGIN " & _
                        "		        EXEC Create_LogTableRecords  '" & AgL.XNull(.Rows(I)("Table_Name")) & "','A',@Code,@UniqueKey " & _
                        "           End " & _
                        "	END "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

                mQry = "CREATE TRIGGER Tr_" & AgL.XNull(.Rows(I)("Table_Name")) & "_E ON " & AgL.XNull(.Rows(I)("Table_Name")) & "  " & _
                        "FOR Update " & _
                        "AS " & _
                        "DECLARE @Code NVARCHAR(21) " & _
                        "DECLARE @UniqueKey NVARCHAR(Max) " & _
                        "DECLARE @UDate DateTime " & _
                        "IF EXISTS (SELECT * FROM Inserted) 	 " & _
                        "	BEGIN  " & _
                        "		SET @Code=(SELECT Top 1 " & AgL.XNull(.Rows(I)("Search_Field")) & " FROM Inserted i) " & _
                        "		SET @UniqueKey=(SELECT Top 1 " & AgL.XNull(.Rows(I)("UniqueField")) & " FROM Inserted i) " & _
                        "       Set @UDate = (SELECT GetDate()) " & _
                        "       IF NOT EXISTS(SELECT * FROM Log_TableRecords WHERE SearchKey = @Code AND TableName = '" & AgL.XNull(.Rows(I)("Table_Name")) & "' And AED='E' AND UpdateDate =@UDate) " & _
                        "           BEGIN " & _
                        "		        EXEC Create_LogTableRecords  '" & AgL.XNull(.Rows(I)("Table_Name")) & "','E',@Code,@UniqueKey " & _
                        "           End " & _
                        "	END "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

                mQry = "CREATE TRIGGER Tr_" & AgL.XNull(.Rows(I)("Table_Name")) & "_D ON " & AgL.XNull(.Rows(I)("Table_Name")) & "  " & _
                        "FOR Delete " & _
                        "AS " & _
                        "DECLARE @Code NVARCHAR(21) " & _
                        "DECLARE @UniqueKey NVARCHAR(Max) " & _
                        "DECLARE @UDate DateTime " & _
                        "IF EXISTS (SELECT * FROM Deleted) 	 " & _
                        "	BEGIN  " & _
                        "		SET @Code=(SELECT Top 1 " & AgL.XNull(.Rows(I)("Search_Field")) & " FROM Deleted i) " & _
                        "		SET @UniqueKey=(SELECT Top 1 " & AgL.XNull(.Rows(I)("UniqueField")) & " FROM Inserted i) " & _
                        "       Set @UDate = (SELECT GetDate()) " & _
                        "       IF NOT EXISTS(SELECT * FROM Log_TableRecords WHERE SearchKey = @Code AND TableName = '" & AgL.XNull(.Rows(I)("Table_Name")) & "' And AED='D' AND UpdateDate =@UDate) " & _
                        "           BEGIN " & _
                        "		        EXEC Create_LogTableRecords  '" & AgL.XNull(.Rows(I)("Table_Name")) & "','D',@Code,@UniqueKey " & _
                        "           End " & _
                        "	END "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

            Next
        End With
        DtTemp.Dispose()

    End Sub


End Class