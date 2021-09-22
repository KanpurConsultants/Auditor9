Imports System.Data.SqlClient
Imports System.Data.SQLite
Imports System.Drawing.Printing
Imports System.IO
Imports System.Linq
Imports System.Net

Public Class FrmRestoreDatabase
    Dim AgL As AgLibrary.ClsMain
    Dim mConnectionStr As String = "", mQry As String
    Public Sub New(ByVal AgLibVar As AgLibrary.ClsMain)
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        AgL = AgLibVar
    End Sub
    Private Sub FrmReportPrint_Load(sender As Object, e As EventArgs) Handles Me.Load
        'AgL.WinSetting(Me, 654, 990, 0, 0)
        'Me.Location = New System.Drawing.Point(0, 0)
        TxtDatabaseName.Text = AgL.PubDBName
        TxtDatabaseName.ReadOnly = True
        Dim mDatabaseFilePath As String = AgL.INIRead(StrPath + "\" + IniName, "CompanyInfo", "DatabaseFilePath", "")
        If mDatabaseFilePath <> "" Then
            TxtDatabaseFilePath.Text = mDatabaseFilePath
            TxtDatabaseFilePath.ReadOnly = True
        End If
    End Sub
    Private Sub FrmReportPrint_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub
    Private Sub BtnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnOK.Click, BtnCancel.Click
        Dim MyCommand As OleDb.OleDbDataAdapter = Nothing
        Select Case sender.name
            Case BtnOK.Name
                ProcRestoreDatabase()
            Case BtnCancel.Name
                Me.Close()
        End Select
    End Sub

    Private Sub BtnSelectFile_Click(sender As Object, e As EventArgs) Handles BtnSelectFile.Click
        Dim FilePath As String = My.Computer.FileSystem.SpecialDirectories.Desktop
        Dim OpenFileDialogBox As OpenFileDialog = New OpenFileDialog
        OpenFileDialogBox.Title = "File Name"
        OpenFileDialogBox.InitialDirectory = FilePath
        If OpenFileDialogBox.ShowDialog = Windows.Forms.DialogResult.Cancel Then Exit Sub
        Dim mDbPath As String = OpenFileDialogBox.FileName
        TxtBackupFilePath.Text = mDbPath
    End Sub

    Private Sub ProcRestoreDatabase()
        Try
            If MsgBox("Are you sure you want to proceed with the data file' restore?" & vbNewLine & "This will overwrite your data files in the Back-Up file.", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "") = MsgBoxResult.Yes Then
                Dim bCurrentSite_Code As String = AgL.PubSiteCode
                Dim bCurrentDiv_Code As String = AgL.PubDivCode
                Dim bCurrentComp_Code As String = AgL.PubCompCode


                Dim Conn As New SqlConnection
                Dim Cmd As New SqlCommand
                Conn.ConnectionString = AgL.GCn.ConnectionString
                Conn.Open()
                Cmd.Connection = Conn
                Cmd.CommandTimeout = 1000

                Cmd.CommandText = "Alter Database " & TxtDatabaseName.Text & " Set Single_user With Rollback Immediate"
                Cmd.ExecuteNonQuery()

                Cmd.CommandText = "Use Master"
                Cmd.ExecuteNonQuery()

                Cmd.CommandText = " Restore Database " & TxtDatabaseName.Text & " FROM DISK='" & TxtBackupFilePath.Text & "' with replace,
                        MOVE '" & TxtDatabaseName.Text & "' TO '" & TxtDatabaseFilePath.Text & TxtDatabaseName.Text & ".mdf ',
                        MOVE '" & TxtDatabaseName.Text + "_log" & "' TO '" & TxtDatabaseFilePath.Text & TxtDatabaseName.Text & ".ldf'"
                Cmd.ExecuteNonQuery()

                Cmd.CommandText = "Alter Database " & TxtDatabaseName.Text & " Set MULTI_USER"
                Cmd.ExecuteNonQuery()

                MsgBox("Process Complete.Please Reload Software.", MsgBoxStyle.Information)
                Application.Exit()

                'If Not FOpenIni(StrPath + "\" + IniName, AgL.PubUserName, AgL.PubUserPassword) Then
                '    MsgBox("Can't Connect to Database")
                'Else
                '    AgL.PubSiteCode = bCurrentSite_Code
                '    AgL.PubDivCode = bCurrentDiv_Code
                '    AgL.PubCompCode = bCurrentComp_Code
                '    AgL.PubLoginDate = DateTime.Now()
                '    AgIniVar.FOpenConnection(AgL.PubCompCode, AgL.PubSiteCode)
                '    AgIniVar.ProcSwapSiteCompanyDetail()
                'End If

                'MsgBox("Process Complete.")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)

            'Dim Conn As New SqlConnection
            'Dim Cmd As New SqlCommand
            'Dim bConnString_New As String = AgL.GCn.ConnectionString.ToString.Replace(AgL.PubDBName, "master")

            'Conn.ConnectionString = bConnString_New
            'Conn.Open()
            'Cmd.Connection = Conn
            'Cmd.CommandTimeout = 1000

            'Cmd.CommandText = "Alter Database " & TxtDatabaseName.Text & " Set Single_user With Rollback Immediate"
            'Cmd.ExecuteNonQuery()
        End Try
    End Sub
End Class
