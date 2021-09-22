Imports System.Data.SqlClient
Imports System.Data.SQLite
Imports System.Drawing.Printing
Imports System.IO
Imports System.Linq
Imports System.Net

Public Class FrmExportDataToSqlServer
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
                mConnectionStr = "Server=" & TxtServerName.Text & ";Database=" & TxtDatabaseName.Text & ";User Id=" & TxtUserName.Text & ";Password=" & TxtPassword.Text & ""
                FExportDataToSqlServerBulk()
                FExportDataToSqlServer()
            Case BtnCancel.Name
                Me.Close()
        End Select
    End Sub

    Private Function FCheckRecordCountsOfBothTables(TableName As String)
        Dim mSqliteRowsCount As Integer = 0
        Dim mSqliteServerCount As Integer = 0
        mSqliteRowsCount = AgL.FillData("SELECT Count(*) As Cnt FROM " & TableName & " ", AgL.GCn).Tables(0).Rows(0)("Cnt")
        mSqliteServerCount = FillData("SELECT Count(*) As Cnt FROM " & TableName & " ", mConnectionStr).Tables(0).Rows(0)("Cnt")

        If mSqliteRowsCount = mSqliteServerCount Then Return True Else Return False
    End Function


    Private Sub FExportDataToSqlServer()
        Dim mTrans As String = ""
        Dim DtTables As DataTable = Nothing
        Dim DtFields As DataTable = Nothing
        Dim DtSqliteTableData As DataTable = Nothing
        Dim I As Integer = 0
        Dim J As Integer = 0
        Dim K As Integer = 0
        Dim StrInsertionQry As String = ""
        Dim StrSelectionQry As String = ""


        mQry = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.Tables WHERE TABLE_TYPE = 'BASE TABLE' ORDER BY TABLE_NAME "
            DtTables = FillData(mQry, mConnectionStr).Tables(0)

        Try
            mQry = "EXEC sp_MSforeachtable ""ALTER TABLE ? NOCHECK CONSTRAINT all"""
            ExecuteDML(mQry, mConnectionStr)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        For I = 0 To DtTables.Rows.Count - 1
            If FCheckRecordCountsOfBothTables(DtTables.Rows(I)("TABLE_NAME")) = False Then
                Try
                    AgL.ECmd = AgL.GCn.CreateCommand
                    AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
                    AgL.ECmd.Transaction = AgL.ETrans
                    mTrans = "Begin"





                    mQry = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.Columns WHERE TABLE_NAME = '" & DtTables.Rows(I)("TABLE_NAME") & "'  ORDER BY COLUMN_NAME  "
                    DtFields = FillData(mQry, mConnectionStr).Tables(0)

                    mQry = "Select * From " & DtTables.Rows(I)("TABLE_NAME") & ""
                    DtSqliteTableData = AgL.FillData(mQry, AgL.GCn).Tables(0)

                    For J = 0 To DtFields.Rows.Count - 1
                        If J = 0 Then
                            StrInsertionQry = " INSERT INTO " & DtTables.Rows(I)("TABLE_NAME") & "(" & DtFields.Rows(J)("COLUMN_NAME")
                        ElseIf J = DtFields.Rows.Count - 1 Then
                            StrInsertionQry += ", " & DtFields.Rows(J)("COLUMN_NAME") + ")"
                        Else
                            StrInsertionQry += ", " & DtFields.Rows(J)("COLUMN_NAME")
                        End If
                    Next

                    For K = 0 To DtSqliteTableData.Rows.Count - 1
                        StrSelectionQry = ""
                        For J = 0 To DtFields.Rows.Count - 1
                            If StrSelectionQry = "" Then
                                StrSelectionQry = " Select " & AgL.Chk_Text(DtSqliteTableData.Rows(K)(DtFields.Rows(J)("COLUMN_NAME")))
                            Else
                                StrSelectionQry += ", " & AgL.Chk_Text(DtSqliteTableData.Rows(K)(DtFields.Rows(J)("COLUMN_NAME")))
                            End If
                        Next
                        ExecuteDML(StrInsertionQry + StrSelectionQry, mConnectionStr)
                    Next



                    AgL.ETrans.Commit()
                    mTrans = "Commit"

                Catch ex As Exception
                    AgL.ETrans.Rollback()
                    MsgBox(ex.Message)
                End Try
            End If
        Next


        Try
            mQry = "exec sp_MSforeachtable @command1=""print '?'"", @command2=""ALTER TABLE ? With CHECK CHECK CONSTRAINT all"""
            ExecuteDML(mQry, mConnectionStr)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub FExportDataToSqlServerBulk()
        Dim mTrans As String = ""
        Dim DtTables As DataTable = Nothing
        Dim DtFields As DataTable = Nothing
        Dim DtSqliteTableData As DataTable = Nothing
        Dim I As Integer = 0
        Dim J As Integer = 0
        Dim K As Integer = 0
        Dim StrColumnList As String = ""



        mQry = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.Tables WHERE TABLE_TYPE = 'BASE TABLE' ORDER BY TABLE_NAME "
        DtTables = FillData(mQry, mConnectionStr).Tables(0)

        Try
            mQry = "EXEC sp_MSforeachtable ""ALTER TABLE ? NOCHECK CONSTRAINT all"""
            ExecuteDML(mQry, mConnectionStr)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        For I = 0 To DtTables.Rows.Count - 1
            If FCheckRecordCountsOfBothTables(DtTables.Rows(I)("TABLE_NAME")) = False Then
                Try
                    AgL.ECmd = AgL.GCn.CreateCommand
                    AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
                    AgL.ECmd.Transaction = AgL.ETrans
                    mTrans = "Begin"


                    mQry = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.Columns WHERE TABLE_NAME = '" & DtTables.Rows(I)("TABLE_NAME") & "'  ORDER BY ORDINAL_POSITION "
                    DtFields = FillData(mQry, mConnectionStr).Tables(0)
                    StrColumnList = ""
                    For J = 0 To DtFields.Rows.Count - 1
                        If StrColumnList = "" Then
                            StrColumnList = DtFields.Rows(J)("COLUMN_NAME")
                        Else
                            StrColumnList += ", " & DtFields.Rows(J)("COLUMN_NAME")
                        End If
                    Next

                    Dim commandSourceData As SQLiteCommand = New SQLiteCommand("Select " & StrColumnList & " From " & DtTables.Rows(I)("TABLE_NAME") & "", AgL.GCn)
                    Dim reader As SQLiteDataReader = commandSourceData.ExecuteReader

                    Using destinationConnection As SqlConnection = New SqlConnection(mConnectionStr)
                        destinationConnection.Open()

                        ' Set up the bulk copy object. 
                        ' The column positions in the source data reader 
                        ' match the column positions in the destination table, 
                        ' so there is no need to map columns.
                        Using bulkCopy As SqlBulkCopy = New SqlBulkCopy(destinationConnection)
                            bulkCopy.DestinationTableName = DtTables.Rows(I)("TABLE_NAME")
                            bulkCopy.BulkCopyTimeout = 500
                            bulkCopy.WriteToServer(reader)
                            reader.Close()
                        End Using
                    End Using


                    AgL.ETrans.Commit()
                    mTrans = "Commit"

                Catch ex As Exception
                    AgL.ETrans.Rollback()
                    MsgBox(ex.Message)
                End Try
            End If
        Next

        Try
            mQry = "exec sp_MSforeachtable @command1=""print '?'"", @command2=""ALTER TABLE ? With CHECK CHECK CONSTRAINT all"""
            ExecuteDML(mQry, mConnectionStr)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Function FillData(Qry As String, ConnStr As String)
        Dim DsTemp As New DataSet
        Dim Da As New SqlClient.SqlDataAdapter(Qry, ConnStr)
        Da.Fill(DsTemp)
        Return DsTemp
    End Function
    Private Sub ExecuteDML(Qry As String, ConnStr As String)
        Using conn As New SqlConnection(ConnStr)
            Using comm As New SqlCommand()
                With comm
                    .Connection = conn
                    .CommandType = CommandType.Text
                    .CommandText = Qry
                End With

                conn.Open()
                comm.ExecuteNonQuery()
            End Using
        End Using
    End Sub
End Class
