Imports System.Data.SqlClient
Imports System.Data.SQLite

Public Class FrmImportFromExcelToDatabase
    Public WithEvents Dgl1 As New AgControls.AgDataGrid

    Dim mQry As String = ""
    Dim mUserAction As String = "None"
    Dim DsExcelData As New DataSet
    Dim MyConnection As System.Data.OleDb.OleDbConnection
    Public ReadOnly Property UserAction() As String
        Get
            UserAction = mUserAction
        End Get
    End Property
    Public ReadOnly Property P_DsExcelData() As DataSet
        Get
            Return DsExcelData
        End Get
    End Property
    Private Sub FrmImportFromExcelToDatabase_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSelectExcelFile.Click
        Dim MyCommand As System.Data.OleDb.OleDbDataAdapter = Nothing
        Dim DsTemp As New DataSet
        Dim myExcelFilePath As String

        Opn.Filter = "Excel Files (*.xls)|*.xls"
        Opn.ShowDialog()
        myExcelFilePath = Opn.FileName
        TxtExcelPath.Text = myExcelFilePath
        MyConnection = New System.Data.OleDb.OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0; " &
                       "data source='" & myExcelFilePath & " '; " & "Extended Properties=Excel 8.0;")
        MyConnection.Open()
    End Sub

    Private Sub BtnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnOK.Click, BtnCancel.Click
        Dim MyCommand As OleDb.OleDbDataAdapter = Nothing
        Select Case sender.name
            Case BtnOK.Name

                Try
                    MyCommand = New System.Data.OleDb.OleDbDataAdapter("select *  from [sheet1$] ", MyConnection)
                    MyCommand.Fill(DsExcelData)
                Catch ex As Exception
                    MsgBox(ex.Message, MsgBoxStyle.Exclamation)
                    Exit Sub
                End Try


                CreateSqlTableFromDataTable(DsExcelData.Tables(0), TxtTableName.Text)
                MsgBox("Import Completed.", MsgBoxStyle.Information)

            Case BtnCancel.Name
                mUserAction = sender.text
                Me.Dispose()
        End Select
    End Sub

    Public Sub CreateSqlTableFromDataTable(ByVal DtTable As DataTable, ByVal TableName As String)
        Dim I As Integer
        Dim J As Integer
        Dim mTrans As String = ""
        Dim CreateTableQry As String = ""

        If AgL.Dman_Execute("SELECT Count(*) FROM sqlite_master WHERE type='table' AND name='" & TableName & "'", AgL.GCn).ExecuteScalar <> 0 Then
            MsgBox("Table already exists...!", MsgBoxStyle.Information)
            Exit Sub
        End If

        Try

            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = "Begin"

            CreateTableQry += "CREATE TABLE [" + TableName + "]"
            CreateTableQry += "("
            For I = 0 To DtTable.Columns.Count - 1
                If (I = DtTable.Columns.Count - 1) Then
                    CreateTableQry += "[" + DtTable.Columns(I).ColumnName + "] " + "TEXT"
                Else
                    CreateTableQry += "[" + DtTable.Columns(I).ColumnName + "] " + "TEXT,"
                End If
            Next

            CreateTableQry += ") "

            AgL.Dman_ExecuteNonQry(CreateTableQry, AgL.GCn)

            For I = 0 To DtTable.Rows.Count - 1
                mQry = "INSERT Into " + TableName + "("
                For J = 0 To DtTable.Columns.Count - 1
                    If (J = DtTable.Columns.Count - 1) Then
                        mQry += "[" + DtTable.Columns(J).ColumnName + "] "
                    Else
                        mQry += "[" + DtTable.Columns(J).ColumnName + "], "
                    End If
                Next
                mQry += ")"
                mQry += "Values("
                For J = 0 To DtTable.Columns.Count - 1
                    If (J = DtTable.Columns.Count - 1) Then
                        mQry += "'" + AgL.XNull(DtTable.Rows(I)(J)) + "'"
                    Else
                        mQry += "'" + AgL.XNull(DtTable.Rows(I)(J)) + "', "
                    End If
                Next
                mQry += ")"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
            Next

            AgL.ETrans.Commit()
            mTrans = "Commit"

        Catch ex As Exception
            AgL.ETrans.Rollback()
            MsgBox(ex.Message)
        End Try
    End Sub
End Class