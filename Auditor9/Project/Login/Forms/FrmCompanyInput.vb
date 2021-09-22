Imports System.Data.SQLite
Imports System.Data.SqlClient
Imports System.IO
Public Class FrmCompanyInput
    Private Declare Unicode Function WritePrivateProfileString Lib "kernel32" _
Alias "WritePrivateProfileStringW" (ByVal lpApplicationName As String,
ByVal lpKeyName As String, ByVal lpString As String,
ByVal lpFileName As String) As Int32
    Private Sub ExecuteNonQuery(ByVal sql As String, mSqlConn As SqlConnection, Optional mSqlCmd As SqlCommand = Nothing)
        ' Open the connection
        'If mSqlConn.State = ConnectionState.Open Then
        '    mSqlConn.Close()
        'End If

        If mSqlCmd Is Nothing Then
            mSqlCmd = New SqlCommand
        End If

        'mSqlConn.ConnectionString = "Persist Security Info=False;User ID='SA';pwd=;Initial Catalog=" & AgL.PubCompanyDBName & ";Data Source=" & AgL.PubServerName
        'mSqlConn.Open()
        mSqlCmd = New SqlCommand(sql, mSqlConn)
        Try
            mSqlCmd.ExecuteNonQuery()
        Catch ae As SqlException
            MessageBox.Show(ae.Message.ToString())
        End Try
    End Sub 'ExecuteSQLStmt


    Sub FCreateSqliteDatabase()
        If (Not System.IO.Directory.Exists("..\Data")) Then
            System.IO.Directory.CreateDirectory("..\Data")
            AgL.PubCompanyDBPath = "..\Data\"
        End If

        Dim mDbPath As String = AgL.PubCompanyDBPath & TxtDatabase.Text  '& AgL.PubCompanyDBName
        Dim Connection As New SQLiteConnection
        SQLiteConnection.CreateFile(mDbPath)
        Using Query As New SQLiteCommand()
            Connection.ConnectionString = "DataSource=" & mDbPath & ";Version=3;New=False;Compress=True;"
            Connection.SetPassword(AgLibrary.ClsConstant.PubDbPassword)
            Connection.Open()
            With Query
                .Connection = Connection
                .CommandText = "   CREATE TABLE [Company] (
                                   [Comp_Code] nvarchar(5) NOT NULL COLLATE NOCASE,
                                   [Div_Code] nvarchar(1) COLLATE NOCASE,
                                   [Comp_Name] nvarchar(100) COLLATE NOCASE,
                                   [CentralData_Path] nvarchar(100) COLLATE NOCASE,
                                   [PrevDBName] varchar(50) COLLATE NOCASE,
                                   [DbPrefix] varchar(50) COLLATE NOCASE,
                                   [Repo_Path] nvarchar(100) COLLATE NOCASE,
                                   [Start_Dt] datetime,
                                   [End_Dt] datetime,
                                   [address1] nvarchar(35) COLLATE NOCASE,
                                   [address2] nvarchar(35) COLLATE NOCASE,
                                   [city] nvarchar(35) COLLATE NOCASE,
                                   [pin] nvarchar(6) COLLATE NOCASE,
                                   [phone] nvarchar(30) COLLATE NOCASE,
                                   [Email] nvarchar(100) COLLATE NOCASE,
                                   [fax] nvarchar(25) COLLATE NOCASE,
                                   [lstno] nvarchar(35) COLLATE NOCASE,
                                   [lstdate] nvarchar(12) COLLATE NOCASE,
                                   [cstno] nvarchar(35) COLLATE NOCASE,
                                   [cstdate] nvarchar(12) COLLATE NOCASE,
                                   [cyear] nvarchar(9) COLLATE NOCASE,
                                   [pyear] nvarchar(9) COLLATE NOCASE,
                                   [State] varchar(35) COLLATE NOCASE,
                                   [U_Name] varchar(35) COLLATE NOCASE,
                                   [U_EntDt] datetime,
                                   [U_AE] nvarchar(1) COLLATE NOCASE,
                                   [DeletedYN] nvarchar(1) COLLATE NOCASE,
                                   [Country] nvarchar(50) COLLATE NOCASE,
                                   [V_Prefix] nvarchar(5) COLLATE NOCASE,
                                   [SerialKeyNo] nvarchar(50) COLLATE NOCASE,
                                   PRIMARY KEY ([Comp_Code])
                                );               

                        INSERT INTO Company
                        (Comp_Code, Div_Code, Comp_Name, CentralData_Path, PrevDBName, DbPrefix, Repo_Path, Start_Dt, End_Dt, address1, address2, city, pin, phone, fax, lstno, lstdate, cstno, cstdate, cyear, pyear, State, U_Name, U_EntDt, U_AE, DeletedYN, Country, V_Prefix)
                        VALUES('1', 'D', '" & TxtDispName.Text & "', Null, NULL, Null, NULL, '2018-04-01 00:00:00', '2019-03-31 00:00:00', Null, NULL, '" & TxtCity.Text & "', NULL, NULL, '-', NULL, NULL, '-', '12/Nov/2017', '2018-2019', '2017-2018', 'U.P.', 'SA', " & AgL.Chk_Date(System.DateTime.Now()) & ", 'E', 'N', 'INDIA', '2010');

                        CREATE TABLE [UserMast] (
                                        [USER_NAME] nvarchar(10) NOT NULL COLLATE NOCASE,
                                        [Code] nvarchar(15) COLLATE NOCASE,
                                        [PASSWD] nvarchar(16) COLLATE NOCASE,
                                        [Description] nvarchar(50) COLLATE NOCASE,
                                        [Admin] nvarchar(1) COLLATE NOCASE,
                                        [RowId] bigint NOT NULL,
                                        [UpLoadDate] datetime,
                                        [ModuleList] nvarchar(2147483647) COLLATE NOCASE,
                                        [SeniorName] nvarchar(10) COLLATE NOCASE,
                                        [MainStreamCode] nvarchar(2147483647) COLLATE NOCASE,
                                        [EMail] nvarchar(100) COLLATE NOCASE,
                                        [Mobile] nvarchar(10) COLLATE NOCASE,
                                        [IsActive] bit,
                                        [InActiveDate] datetime,
                                        PRIMARY KEY ([USER_NAME])
                                        );
                        
                        CREATE UNIQUE INDEX [IX_UserMast]
                                        ON [UserMast]
                                        ([USER_NAME]);

                        INSERT INTO UserMast
                        (USER_NAME, Code, PASSWD, Description, Admin, RowId, UpLoadDate, ModuleList, SeniorName, MainStreamCode, EMail, Mobile, IsActive, InActiveDate)
                        VALUES('SA', '1', '@', 'CEO', 'Y', 1, NULL, NULL, NULL, '010', NULL, NULL, 1, NULL);

                        CREATE TABLE LogTable (
                            DocId       NVARCHAR (36)  COLLATE NOCASE,
                            EntryPoint  NVARCHAR (100) COLLATE NOCASE,
                            MachineName NVARCHAR (50)  COLLATE NOCASE,
                            U_Name      NVARCHAR (10)  COLLATE NOCASE,
                            U_EntDt     DATETIME,
                            U_AE        NVARCHAR (1)   COLLATE NOCASE,
                            Remark      NVARCHAR (255) COLLATE NOCASE,
                            V_Date      DATETIME,
                            SubCode     NVARCHAR (10)  COLLATE NOCASE,
                            PartyDetail NVARCHAR (255) COLLATE NOCASE,
                            Amount      FLOAT,
                            Site_Code   NVARCHAR (2)   COLLATE NOCASE,
                            Div_Code    NVARCHAR (1)   COLLATE NOCASE,
                            UpLoadDate  DATETIME
                        );


                        "
            End With
            Query.ExecuteNonQuery()
            Connection.Close()

            WritePrivateProfileString("CompanyInfo", "Path", AgL.PubCompanyDBPath, StrPath + "\" + IniName)
            WritePrivateProfileString("CompanyInfo", "DbName", TxtDatabase.Text, StrPath + "\" + IniName)

            If FOpenIni(StrPath + "\" + IniName, "SA", "") Then
                AgL.PubDivCode = "D"
                AgL.PubSiteCode = "1"
                If CboScopeOfWork.Text.ToUpper.Contains("RETAIL") Then
                    AgL.PubScopeOfWork = CboScopeOfWork.Text.ToUpper + "+CLOTH TRADING WHOLESALE"
                Else
                    AgL.PubScopeOfWork = CboScopeOfWork.Text.ToUpper
                End If


                Dim Cls_Customised As New Customised.ClsMain(AgL)
                Cls_Customised.UpdateTableStructure(True)
            End If
            Me.Dispose()
        End Using

    End Sub


    Sub FCreateSqlServerDatabase()
        Dim mQry As String
        Dim mConn As New SqlClient.SqlConnection
        Dim mSqlCmd As SqlCommand

        mConn.ConnectionString = "Persist Security Info=False;User ID='SA';pwd=;Initial Catalog=Master;Data Source=" & AgL.PubServerName
        Try
            mConn.Open()
        Catch ex As Exception
            MsgBox(ex.Message & vbCrLf & "Some Error Ocurred while connecting Master SqlServer Database")
            End
        End Try


        mQry = "Create Database " & TxtDatabase.Text
        mSqlCmd = New SqlCommand(mQry, mConn)
        Try
            mSqlCmd.ExecuteNonQuery()
        Catch EX As Exception
            MessageBox.Show(EX.Message & vbCrLf & "Some Error Ocurred while creating SqlServer Database")
        End Try

        If mConn.State = ConnectionState.Open Then mConn.Close()
        mConn.ConnectionString = "Persist Security Info=False;User ID='SA';pwd=;Initial Catalog=" & TxtDatabase.Text & ";Data Source=" & AgL.PubServerName
        Try
            mConn.Open()
        Catch ex As Exception
            MsgBox(ex.Message & vbCrLf & "Some Error Ocurred while connecting newly created SqlServer Database")
            End
        End Try





        mQry = "   CREATE TABLE [Company] (
                                   [Comp_Code] nvarchar(5) NOT NULL ,
                                   [Div_Code] nvarchar(1) ,
                                   [Comp_Name] nvarchar(100) ,
                                   [CentralData_Path] nvarchar(100) ,
                                   [PrevDBName] varchar(50) ,
                                   [DbPrefix] varchar(50) ,
                                   [Repo_Path] nvarchar(100) ,
                                   [Start_Dt] datetime,
                                   [End_Dt] datetime,
                                   [address1] nvarchar(35) ,
                                   [address2] nvarchar(35) ,
                                   [city] nvarchar(35) ,
                                   [pin] nvarchar(6) ,
                                   [phone] nvarchar(30) ,
                                   [Email] nvarchar(100) ,
                                   [fax] nvarchar(25) ,
                                   [lstno] nvarchar(35) ,
                                   [lstdate] nvarchar(12) ,
                                   [cstno] nvarchar(35) ,
                                   [cstdate] nvarchar(12) ,
                                   [cyear] nvarchar(9) ,
                                   [pyear] nvarchar(9) ,
                                   [State] varchar(35) ,
                                   [U_Name] varchar(35) ,
                                   [U_EntDt] datetime,
                                   [U_AE] nvarchar(1) ,
                                   [DeletedYN] nvarchar(1) ,
                                   [Country] nvarchar(50) ,
                                   [V_Prefix] nvarchar(5) ,
                                   [SerialKeyNo] nvarchar(50) ,
                                   PRIMARY KEY ([Comp_Code])
                                );               

                        INSERT INTO Company
                        (Comp_Code, Div_Code, Comp_Name, CentralData_Path, PrevDBName, DbPrefix, Repo_Path, Start_Dt, End_Dt, address1, address2, city, pin, phone, fax, lstno, lstdate, cstno, cstdate, cyear, pyear, State, U_Name, U_EntDt, U_AE, DeletedYN, Country, V_Prefix)
                        VALUES('1', 'D', '" & TxtDispName.Text & "', Null, NULL, Null, NULL, '2018-04-01 00:00:00', '2019-03-31 00:00:00', Null, NULL, '" & TxtCity.Text & "', NULL, NULL, '-', NULL, NULL, '-', '12/Nov/2017', '2018-2019', '2017-2018', 'U.P.', 'SA', " & AgL.Chk_Date(System.DateTime.Now()) & ", 'E', 'N', 'INDIA', '2010');

                        CREATE TABLE [UserMast] (
                                        [USER_NAME] nvarchar(10) NOT NULL ,
                                        [Code] nvarchar(15) ,
                                        [PASSWD] nvarchar(16) ,
                                        [Description] nvarchar(50) ,
                                        [Admin] nvarchar(1) ,
                                        [RowId] bigint NOT NULL,
                                        [UpLoadDate] datetime,
                                        [ModuleList] nvarchar(1000) ,
                                        [SeniorName] nvarchar(10) ,
                                        [MainStreamCode] nvarchar(1000) ,
                                        [EMail] nvarchar(100) ,
                                        [Mobile] nvarchar(10) ,
                                        [IsActive] bit,
                                        [InActiveDate] datetime,
                                        PRIMARY KEY ([USER_NAME])
                                        );
                        
                        CREATE UNIQUE INDEX [IX_UserMast]
                                        ON [UserMast]
                                        ([USER_NAME]);

                        INSERT INTO UserMast
                        (USER_NAME, Code, PASSWD, Description, Admin, RowId, UpLoadDate, ModuleList, SeniorName, MainStreamCode, EMail, Mobile, IsActive, InActiveDate)
                        VALUES('SA', '1', '@', 'CEO', 'Y', 1, NULL, NULL, NULL, '010', NULL, NULL, 1, NULL);


                        CREATE TABLE LogTable (
                            DocId       NVARCHAR (36)  ,
                            EntryPoint  NVARCHAR (100) ,
                            MachineName NVARCHAR (50)  ,
                            U_Name      NVARCHAR (10)  ,
                            U_EntDt     DATETIME,
                            U_AE        NVARCHAR (1)   ,
                            Remark      NVARCHAR (255) ,
                            V_Date      DATETIME,
                            SubCode     NVARCHAR (10)  ,
                            PartyDetail NVARCHAR (255) ,
                            Amount      FLOAT,
                            Site_Code   NVARCHAR (2)   ,
                            Div_Code    NVARCHAR (1)   ,
                            UpLoadDate  DATETIME
                        );


                        "

        ExecuteNonQuery(mQry, mConn)

        WritePrivateProfileString("CompanyInfo", "Path", AgL.PubCompanyDBPath, StrPath + "\" + IniName)
        WritePrivateProfileString("CompanyInfo", "DbName", TxtDatabase.Text, StrPath + "\" + IniName)

        If FOpenIni(StrPath + "\" + IniName, "SA", "") Then
            AgL.PubDivCode = "D"
            AgL.PubSiteCode = "1"
            If CboScopeOfWork.Text.ToUpper.Contains("RETAIL") Then
                AgL.PubScopeOfWork = CboScopeOfWork.Text.ToUpper + "+CLOTH TRADING WHOLESALE"
            Else
                AgL.PubScopeOfWork = CboScopeOfWork.Text.ToUpper
            End If
            Dim Cls_Customised As New Customised.ClsMain(AgL)
            Cls_Customised.UpdateTableStructure(True)
        End If
        Me.Dispose()
    End Sub

    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnCreateDatabase.Click
        If TxtPassword.Text = "P@ssw0rd!" Then
            If AgL.PubServerName = "" Then
                FCreateSqliteDatabase()
            Else
                FCreateSqlServerDatabase()
            End If
        Else
            MsgBox("Password is incorrect...!", MsgBoxStyle.Information)
            Exit Sub
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim mPath As String
        Dim mFileName As String
        OpenFileDialog1.ShowDialog()
        mPath = Replace(OpenFileDialog1.FileName, OpenFileDialog1.SafeFileName, "")
        mFileName = OpenFileDialog1.SafeFileName
        TextBox1.Text = OpenFileDialog1.FileName


        If AgL.FCheckDatabase(OpenFileDialog1.FileName) Then
            WritePrivateProfileString("CompanyInfo", "Path", mPath, StrPath + "\" + IniName)
            WritePrivateProfileString("CompanyInfo", "DbName", mFileName, StrPath + "\" + IniName)
        Else
            TextBox1.Text = ""
        End If

    End Sub

    Private Sub FrmCompanyInput_Load(sender As Object, e As EventArgs) Handles Me.Load
        TxtDispName.Focus()
    End Sub
End Class