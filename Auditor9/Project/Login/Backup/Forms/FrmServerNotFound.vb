Public Class FrmServerNotFound
    Private Declare Unicode Function WritePrivateProfileString Lib "kernel32" _
Alias "WritePrivateProfileStringW" (ByVal lpApplicationName As String, _
ByVal lpKeyName As String, ByVal lpString As String, _
ByVal lpFileName As String) As Int32

    Dim mIniPath As String = StrPath + "\" + IniName

    Private Sub FrmServerNotFound_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TxtSqlServerName.Text = Agl.PubServerName
        TxtDatabaseName.Text = Agl.PubCompanyDBName
        TxtDatabasePassword.Text = Agl.PubChkPasswordSQL
    End Sub

    Private Sub BtnOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnOk.Click
        Dim mErr As String
        mErr = ConnectDb(TxtSqlServerName.Text, TxtDatabaseName.Text, AgL.PubDBUserSQL, TxtDatabasePassword.Text)
        If mErr <> "" Then
            MsgBox(CheckDatabaseErrors(mErr))
        Else
            If MsgBox("Database is successfully connected. Do you want to update information in configuration settings", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                WritePrivateProfileString("Server", "Name", TxtSqlServerName.Text, mIniPath)
                WritePrivateProfileString("Database", "Name", TxtDatabaseName.Text, mIniPath)
                WritePrivateProfileString("Security", "Password", TxtDatabasePassword.Text, mIniPath)
            End If
            Me.Hide()
        End If
    End Sub

    Public Function CheckDatabaseErrors(ByVal Err As String)

        If Err = "A network-related or instance-specific error occurred while establishing a connection to SQL Server. The server was not found or was not accessible. Verify that the instance name is correct and that SQL Server is configured to allow remote connections. (provider: Named Pipes Provider, error: 40 - Could not open a connection to SQL Server)" Then
            CheckDatabaseErrors = "Sql Server Name is either incorrect or not found"
        ElseIf Err = "A network-related or instance-specific error occurred while establishing a connection to SQL Server. The server was not found or was not accessible. Verify that the instance name is correct and that SQL Server is configured to allow remote connections. (provider: SQL Network Interfaces, error: 26 - Error Locating Server/Instance Specified)" Then
            CheckDatabaseErrors = "Sql Server Name is either incorrect or not found"
        ElseIf Err.Length > 21 AndAlso Err.Substring(0, 21) = "Login failed for user" Then
            CheckDatabaseErrors = "Database password is incorrect"
        ElseIf Err.Length > 20 AndAlso Err.Substring(0, 20) = "Cannot open database" Then
            CheckDatabaseErrors = "Database name is incorrect"
        Else
            CheckDatabaseErrors = Err
        End If
    End Function

    Private Sub BtnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnCancel.Click
        If MsgBox("Sure to close software?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            End
        End If
    End Sub

    Private Sub CreateDB()
        Dim mErr As String
        Dim mScript As String = ""
        mErr = ConnectDb(TxtSqlServerName.Text, "Master", AgL.PubDBUserSQL, TxtDatabasePassword.Text)
        If mErr = "" Then

            mScript = "  "
            mScript += "         USE [master] "
            Agl.Dman_ExecuteNonQry(mScript, Agl.GCn)            
            mScript = " /****** Object:  Database [" & TxtDatabaseName.Text & "]    Script Date: 10/14/2011 15:48:27 ******/ "
            mScript = " CREATE DATABASE [" & TxtDatabaseName.Text & "]  "
            Agl.Dman_ExecuteNonQry(mScript, Agl.GCn)
            mScript = " EXEC dbo.sp_dbcmptlevel @dbname=N'" & TxtDatabaseName.Text & "', @new_cmptlevel=80 "
            mScript += "           "
            mScript += " IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled')) "
            mScript += "             begin "
            mScript += " EXEC [" & TxtDatabaseName.Text & "].[dbo].[sp_fulltext_database] @action = 'disable' "
            mScript += "             End "
            mScript += "               "
            mScript += " ALTER DATABASE [" & TxtDatabaseName.Text & "] SET ANSI_NULL_DEFAULT OFF  "
            mScript += "               "
            mScript += " ALTER DATABASE [" & TxtDatabaseName.Text & "] SET ANSI_NULLS OFF  "
            mScript += "               "
            mScript += " ALTER DATABASE [" & TxtDatabaseName.Text & "] SET ANSI_PADDING OFF  "
            mScript += "               "
            mScript += " ALTER DATABASE [" & TxtDatabaseName.Text & "] SET ANSI_WARNINGS OFF  "
            mScript += "               "
            mScript += " ALTER DATABASE [" & TxtDatabaseName.Text & "] SET ARITHABORT OFF  "
            mScript += "               "
            mScript += " ALTER DATABASE [" & TxtDatabaseName.Text & "] SET AUTO_CLOSE OFF  "
            mScript += "               "
            mScript += " ALTER DATABASE [" & TxtDatabaseName.Text & "] SET AUTO_CREATE_STATISTICS ON  "
            mScript += "               "
            mScript += " ALTER DATABASE [" & TxtDatabaseName.Text & "] SET AUTO_SHRINK OFF  "
            mScript += "               "
            mScript += " ALTER DATABASE [" & TxtDatabaseName.Text & "] SET AUTO_UPDATE_STATISTICS ON  "
            mScript += "               "
            mScript += " ALTER DATABASE [" & TxtDatabaseName.Text & "] SET CURSOR_CLOSE_ON_COMMIT OFF  "
            mScript += "               "
            mScript += " ALTER DATABASE [" & TxtDatabaseName.Text & "] SET CURSOR_DEFAULT  GLOBAL  "
            mScript += "               "
            mScript += " ALTER DATABASE [" & TxtDatabaseName.Text & "] SET CONCAT_NULL_YIELDS_NULL OFF  "
            mScript += "               "
            mScript += " ALTER DATABASE [" & TxtDatabaseName.Text & "] SET NUMERIC_ROUNDABORT OFF  "
            mScript += "               "
            mScript += " ALTER DATABASE [" & TxtDatabaseName.Text & "] SET QUOTED_IDENTIFIER OFF  "
            mScript += "               "
            mScript += " ALTER DATABASE [" & TxtDatabaseName.Text & "] SET RECURSIVE_TRIGGERS OFF  "
            mScript += "               "
            mScript += " ALTER DATABASE [" & TxtDatabaseName.Text & "] SET  DISABLE_BROKER  "
            mScript += "               "
            mScript += " ALTER DATABASE [" & TxtDatabaseName.Text & "] SET AUTO_UPDATE_STATISTICS_ASYNC OFF  "
            mScript += "               "
            mScript += " ALTER DATABASE [" & TxtDatabaseName.Text & "] SET DATE_CORRELATION_OPTIMIZATION OFF  "
            mScript += "               "
            mScript += " ALTER DATABASE [" & TxtDatabaseName.Text & "] SET TRUSTWORTHY OFF  "
            mScript += "               "
            mScript += " ALTER DATABASE [" & TxtDatabaseName.Text & "] SET ALLOW_SNAPSHOT_ISOLATION OFF  "
            mScript += "               "
            mScript += " ALTER DATABASE [" & TxtDatabaseName.Text & "] SET PARAMETERIZATION SIMPLE  "
            mScript += "               "
            mScript += " ALTER DATABASE [" & TxtDatabaseName.Text & "] SET  READ_WRITE  "
            mScript += "               "
            mScript += " ALTER DATABASE [" & TxtDatabaseName.Text & "] SET RECOVERY SIMPLE  "
            mScript += "               "
            mScript += " ALTER DATABASE [" & TxtDatabaseName.Text & "] SET  MULTI_USER  "
            mScript += "               "
            mScript += " ALTER DATABASE [" & TxtDatabaseName.Text & "] SET PAGE_VERIFY TORN_PAGE_DETECTION   "
            mScript += "               "
            mScript += " ALTER DATABASE [" & TxtDatabaseName.Text & "] SET DB_CHAINING OFF  "
            Agl.Dman_ExecuteNonQry(mScript, Agl.GCn)
            mScript = "             USE [" & TxtDatabaseName.Text & "] "
            Agl.Dman_ExecuteNonQry(mScript, Agl.GCn)
            mScript = " /****** Object:  Table [dbo].[User]    Script Date: 10/14/2011 15:48:29 ******/ "
            mScript += " SET ANSI_NULLS ON "
            mScript += "               "
            mScript += " SET QUOTED_IDENTIFIER ON "
            mScript += "               "
            mScript += " CREATE TABLE [dbo].[User]( "
            mScript += " 	[UserName] [nvarchar](20) NOT NULL, "
            mScript += " 	[Password] [nvarchar](20) NULL, "
            mScript += "  CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED  "
            mScript += " ( "
            mScript += "             [UserName]  Asc "
            mScript += " )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] "
            mScript += " ) ON [PRIMARY] "
            mScript += "               "
            mScript += " /****** Object:  Table [dbo].[KeyGen]    Script Date: 10/14/2011 15:48:29 ******/ "
            mScript += " SET ANSI_NULLS ON "
            mScript += "               "
            mScript += " SET QUOTED_IDENTIFIER ON "
            mScript += "               "
            mScript += " CREATE TABLE [dbo].[KeyGen]( "
            mScript += " 	[Code] [nvarchar](10) NOT NULL, "
            mScript += " 	[Customer] [nvarchar](10) NOT NULL, "
            mScript += " 	[Issue_Date] [smalldatetime] NULL, "
            mScript += " 	[HDSN] [nvarchar](50) NOT NULL, "
            mScript += " 	[KeyNo] [nvarchar](50) NOT NULL, "
            mScript += " 	[EntryBy] [nvarchar](20) NOT NULL, "
            mScript += " 	[EntryDate] [smalldatetime] NOT NULL, "
            mScript += "  CONSTRAINT [PK_KeyGen] PRIMARY KEY CLUSTERED  "
            mScript += " ( "
            mScript += "             [Code] Asc "
            mScript += " )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY], "
            mScript += "  CONSTRAINT [IX_KeyGen] UNIQUE NONCLUSTERED  "
            mScript += " ( "
            mScript += "             [HDSN] Asc "
            mScript += " )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] "
            mScript += " ) ON [PRIMARY] "
            mScript += "               "
            mScript += " /****** Object:  Table [dbo].[Customer]    Script Date: 10/14/2011 15:48:30 ******/ "
            mScript += " SET ANSI_NULLS ON "
            mScript += "               "
            mScript += " SET QUOTED_IDENTIFIER ON "
            mScript += "               "
            mScript += " CREATE TABLE [dbo].[Customer]( "
            mScript += " 	[Code] [nvarchar](10) NOT NULL, "
            mScript += " 	[Name] [nvarchar](255) NOT NULL, "
            mScript += " 	[Address] [nvarchar](255) NULL, "
            mScript += " 	[EntryBy] [nvarchar](20) NULL, "
            mScript += " 	[EntryDate] [smalldatetime] NULL, "
            mScript += "  CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED  "
            mScript += " ( "
            mScript += "             [Code] Asc "
            mScript += " )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY], "
            mScript += "  CONSTRAINT [IX_Customer] UNIQUE NONCLUSTERED  "
            mScript += " ( "
            mScript += "             [Name] Asc "
            mScript += " )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] "
            mScript += " ) ON [PRIMARY] "
            mScript += "               "

            Agl.Dman_ExecuteNonQry(mScript, Agl.GCn)
            mErr = ConnectDb(TxtSqlServerName.Text, TxtDatabaseName.Text, AgL.PubDBUserSQL, TxtDatabasePassword.Text)
            If mErr = "" Then
                WritePrivateProfileString("Server", "Name", TxtSqlServerName.Text, mIniPath)
                WritePrivateProfileString("CompanyInfo", "Path", TxtDatabaseName.Text, mIniPath)
                WritePrivateProfileString("Security", "Password", TxtDatabasePassword.Text, mIniPath)
                Me.Hide()
            Else
                MsgBox(CheckDatabaseErrors(mErr))
            End If
        Else
            MsgBox(CheckDatabaseErrors(mErr))
        End If

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCreateNewDatabase.Click
        CreateDB()
    End Sub

    Public Function ConnectDb(ByVal ServerName As String, ByVal DatabaseName As String, Optional ByVal DatabaseUser As String = "sa", Optional ByVal DatabasePassword As String = "") As String
        Agl.AglObj = Agl
        Agl.PubDBUserSQL = "SA"
        Agl.GCn = New SqlClient.SqlConnection
        Agl.GcnRead = New SqlClient.SqlConnection
        Agl.Gcn_ConnectionString = "Persist Security Info=False;User ID='" & DatabaseUser & "';pwd=" & DatabasePassword & ";Initial Catalog=" & DatabaseName & ";Data Source=" & ServerName
        Agl.GCn.ConnectionString = Agl.Gcn_ConnectionString
        AgL.GcnRead.ConnectionString = AgL.Gcn_ConnectionString
        'AgL.GCnComp.ConnectionString = AgL.Gcn_ConnectionString
        ConnectDb = ""
        Try

            Agl.GCn.Open()
            Agl.GcnRead.Open()
            Agl.ECmd = New SqlClient.SqlCommand
            Agl.ECmd = Agl.GCn.CreateCommand

        Catch ex As Exception
            ConnectDb = ex.Message
        End Try
    End Function
End Class