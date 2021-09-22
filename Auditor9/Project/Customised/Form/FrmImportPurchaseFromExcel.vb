Public Class FrmImportPurchaseFromExcel
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Public WithEvents Dgl2 As New AgControls.AgDataGrid

    Dim mUserAction As String = "None"
    Dim DsExcelData_File1 As New DataSet
    Dim DsExcelData_File2 As New DataSet
    Dim MyConnection_File1 As System.Data.OleDb.OleDbConnection
    Dim MyConnection_File2 As System.Data.OleDb.OleDbConnection
    Public ReadOnly Property UserAction() As String
        Get
            UserAction = mUserAction
        End Get
    End Property
    Public ReadOnly Property P_DsExcelData_PurchInvoice() As DataSet
        Get
            Return DsExcelData_File1
        End Get
    End Property
    Public ReadOnly Property P_DsExcelData_PurchInvoiceDetail() As DataSet
        Get
            Return DsExcelData_File2
        End Get
    End Property
    Private Sub Ini_Grid()
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.ColumnHeadersHeight = 30
        Dgl1.EnableHeadersVisualStyles = False
        AgL.GridDesign(Dgl1)
        Dgl1.Columns(0).Width = 40
        Dgl1.Columns(1).Width = 180
        Dgl1.Columns(2).Width = 90
        Dgl1.Columns(3).Width = 70
        Dgl1.Columns(4).Width = 195
        Dgl1.ReadOnly = True
        Dgl1.AllowUserToAddRows = False
        Dgl1.Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        Dgl1.Columns(3).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight

        Dgl1.DefaultCellStyle.WrapMode = DataGridViewTriState.True
        Dgl1.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells)

        AgCL.AddAgTextColumn(Dgl1, "CFieldName", 100, 0, "CFieldName", False)

        AgL.AddAgDataGrid(Dgl2, Pnl2)
        Dgl2.ColumnHeadersHeight = 30
        Dgl2.EnableHeadersVisualStyles = False
        AgL.GridDesign(Dgl2)
        Dgl2.Columns(0).Width = 40
        Dgl2.Columns(1).Width = 180
        Dgl2.Columns(2).Width = 90
        Dgl2.Columns(3).Width = 70
        Dgl2.Columns(4).Width = 195
        Dgl2.ReadOnly = True
        Dgl2.AllowUserToAddRows = False
        Dgl2.Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        Dgl2.Columns(3).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
        Dgl2.DefaultCellStyle.WrapMode = DataGridViewTriState.True
        Dgl2.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells)

        AgCL.AddAgTextColumn(Dgl2, "CFieldName", 100, 0, "CFieldName", False)
    End Sub

    Private Sub FrmImportPurchaseFromExcel_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Ini_Grid()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSelectExcelFile_File1.Click, BtnSelectExcelFile_File2.Click
        Dim MyCommand As System.Data.OleDb.OleDbDataAdapter = Nothing
        Dim DsTemp As New DataSet
        Dim myExcelFilePath As String
        'Opn.Filter = "Excel Files (*.xls)|*.xls"
        'Opn.Filter = "CSV Files (*.csv)|*.csv"
        Opn.ShowDialog()
        myExcelFilePath = Opn.FileName

        Select Case sender.name
            Case BtnSelectExcelFile_File1.Name
                TxtExcelPath_File1.Text = myExcelFilePath
                'MyConnection_File1 = New System.Data.OleDb.OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0; " &
                '"data source='" & myExcelFilePath & " '; " & "Extended Properties=Excel 8.0;")
                MyConnection_File1 = New System.Data.OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + myExcelFilePath + ";Extended Properties=Excel 12.0;")
                MyConnection_File1.Open()
            Case BtnSelectExcelFile_File2.Name
                TxtExcelPath_File2.Text = myExcelFilePath
                'MyConnection_File2 = New System.Data.OleDb.OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0; " &
                '"data source='" & myExcelFilePath & " '; " & "Extended Properties=Excel 8.0;")
                MyConnection_File2 = New System.Data.OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + myExcelFilePath + ";Extended Properties=Excel 12.0;")
                MyConnection_File2.Open()
        End Select
    End Sub

    Private Sub BtnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnOK.Click, BtnCancel.Click
        Dim MyCommand As OleDb.OleDbDataAdapter = Nothing
        Select Case sender.name
            Case BtnOK.Name
                MyCommand = New System.Data.OleDb.OleDbDataAdapter("select *  from [sheet1$] ", MyConnection_File1)
                MyCommand.Fill(DsExcelData_File1)

                MyCommand = New System.Data.OleDb.OleDbDataAdapter("select *  from [sheet1$] ", MyConnection_File2)
                MyCommand.Fill(DsExcelData_File2)

                mUserAction = sender.text
                Me.Dispose()
            Case BtnCancel.Name
                mUserAction = sender.text
                Me.Dispose()
        End Select
    End Sub

    Private Sub FrmImportPurchaseFromExcel_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub
End Class