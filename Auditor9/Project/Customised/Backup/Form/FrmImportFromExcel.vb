Public Class FrmImportFromExcel
    Public WithEvents Dgl1 As New AgControls.AgDataGrid

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

    Private Sub Ini_Grid()
        AgL.AddAgDataGrid(Dgl1, Panel2)
        Dgl1.ColumnHeadersHeight = 40
        Dgl1.EnableHeadersVisualStyles = False
        AgL.GridDesign(Dgl1)
        Dgl1.Columns(0).Width = 40
        Dgl1.Columns(1).Width = 150
        Dgl1.Columns(2).Width = 80
        Dgl1.Columns(3).Width = 80
        Dgl1.ReadOnly = True
        Dgl1.AllowUserToAddRows = False

        AgCL.AddAgTextColumn(Dgl1, "CFieldName", 100, 0, "CFieldName", False)
    End Sub

    Private Sub FrmImportFromExcel_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Ini_Grid()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSelectExcelFile.Click
        Dim MyCommand As System.Data.OleDb.OleDbDataAdapter = Nothing
        Dim DsTemp As New DataSet
        Dim myExcelFilePath As String        

        Opn.Filter = "Excel Files (*.xls)|*.xls"
        Opn.ShowDialog()
        myExcelFilePath = Opn.FileName
        TxtExcelPath.Text = myExcelFilePath
        MyConnection = New System.Data.OleDb.OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0; " & _
                       "data source='" & myExcelFilePath & " '; " & "Extended Properties=Excel 8.0;")
        MyConnection.Open()

        FCheckExcelFile(MyConnection)
    End Sub

    Private Sub BtnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnOK.Click, BtnCancel.Click
        Dim MyCommand As OleDb.OleDbDataAdapter = Nothing
        Select Case sender.name
            Case BtnOK.Name
                MyCommand = New System.Data.OleDb.OleDbDataAdapter("select *  from [sheet1$] ", MyConnection)
                MyCommand.Fill(DsExcelData)
                mUserAction = sender.text
                Me.Dispose()

            Case BtnCancel.Name
                mUserAction = sender.text
                Me.Dispose()
        End Select
    End Sub

    Private Sub FCheckExcelFile(ByVal mConn As OleDb.OleDbConnection)
        Dim MyCommand As System.Data.OleDb.OleDbDataAdapter = Nothing
        Dim DsTemp As New DataSet
        Dim I As Integer, J As Integer
        Dim mFieldExist As Boolean = False
        Try
            MyCommand = New System.Data.OleDb.OleDbDataAdapter("select *  from [sheet1$] Where 1=2", mConn)
            MyCommand.Fill(DsTemp)

            For I = 0 To Dgl1.Rows.Count - 1
                If AgL.StrCmp(Dgl1.Item(4, I).Value, "Yes") Then
                    mFieldExist = False
                    For J = 0 To DsTemp.Tables(0).Columns.Count - 1

                        If AgL.StrCmp(Dgl1.Item(1, I).Value, DsTemp.Tables(0).Columns(J).ColumnName) Then
                            mFieldExist = True
                            Exit For
                        End If

                    Next

                    If mFieldExist = False Then
                        Dgl1.Item("CFieldName", I).Value = "1"
                    End If
                Else
                    Dgl1.Item("CFieldName", I).Value = ""
                End If
            Next

            Dim StrMsg$ = ""
            For I = 0 To Dgl1.Rows.Count - 1
                If AgL.StrCmp(Dgl1.Item("CFieldName", I).Value, "1") Then
                    If StrMsg.ToString.Length <> 0 Then StrMsg += ", "
                    StrMsg += Dgl1.Item(1, I).Value
                End If
            Next
            If StrMsg.ToString.Length > 0 Then
                MsgBox(StrMsg & " - Fields not found in excel file. Please Select Correct File. ")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            'DsTemp.Dispose()
        End Try
    End Sub

    
End Class