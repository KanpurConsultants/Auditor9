Imports System.Data.OleDb

Public Class FrmImportSaleFromExcel
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Public WithEvents Dgl2 As New AgControls.AgDataGrid
    Public WithEvents Dgl3 As New AgControls.AgDataGrid

    Dim mUserAction As String = "None"
    Dim DsExcelData_SaleInvoice As New DataSet
    Dim DsExcelData_SaleInvoiceDetail As New DataSet
    Dim DsExcelData_SaleInvoiceDimensionDetail As New DataSet
    Dim MyConnection_SaleInvoice As System.Data.OleDb.OleDbConnection
    Dim MyConnection_SaleInvoiceDetail As System.Data.OleDb.OleDbConnection
    Dim MyConnection_SaleInvoiceDimensionDetail As System.Data.OleDb.OleDbConnection

    Public ReadOnly Property UserAction() As String
        Get
            UserAction = mUserAction
        End Get
    End Property
    Public ReadOnly Property P_DsExcelData_SaleInvoice() As DataSet
        Get
            Return DsExcelData_SaleInvoice
        End Get
    End Property
    Public ReadOnly Property P_DsExcelData_SaleInvoiceDetail() As DataSet
        Get
            Return DsExcelData_SaleInvoiceDetail
        End Get
    End Property
    Public ReadOnly Property P_DsExcelData_SaleInvoiceDimensionDetail() As DataSet
        Get
            Return DsExcelData_SaleInvoiceDimensionDetail
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
        Dgl1.DefaultCellStyle.WrapMode = DataGridViewTriState.True
        Dgl1.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells)
        Dgl1.Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        Dgl1.Columns(3).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight

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
        Dgl2.DefaultCellStyle.WrapMode = DataGridViewTriState.True
        Dgl2.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells)
        Dgl2.Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        Dgl2.Columns(3).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight


        AgCL.AddAgTextColumn(Dgl2, "CFieldName", 100, 0, "CFieldName", False)


        AgL.AddAgDataGrid(Dgl3, Pnl3)
        Dgl3.ColumnHeadersHeight = 30
        Dgl3.EnableHeadersVisualStyles = False
        AgL.GridDesign(Dgl3)
        Dgl3.Columns(0).Width = 40
        Dgl3.Columns(1).Width = 180
        Dgl3.Columns(2).Width = 90
        Dgl3.Columns(3).Width = 70
        Dgl3.Columns(4).Width = 195
        Dgl3.ReadOnly = True
        Dgl3.AllowUserToAddRows = False
        Dgl3.DefaultCellStyle.WrapMode = DataGridViewTriState.True
        Dgl3.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells)
        Dgl3.Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        Dgl3.Columns(3).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight

        AgCL.AddAgTextColumn(Dgl3, "CFieldName", 100, 0, "CFieldName", False)
    End Sub

    Private Sub FrmImportSaleFromExcel_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Ini_Grid()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSelectExcelFile_SaleInvoice.Click, BtnSelectExcelFile_SaleInvoiceDetail.Click, BtnSelectExcelFile_SaleInvoiceDimensionDetail.Click
        Dim MyCommand As System.Data.OleDb.OleDbDataAdapter = Nothing
        Dim DsTemp As New DataSet
        Dim myExcelFilePath As String
        'Opn.Filter = "Excel Files (*.xls)|*.xls"
        'Opn.Filter = "CSV Files (*.csv)|*.csv"
        Opn.ShowDialog()
        myExcelFilePath = Opn.FileName

        Select Case sender.name
            Case BtnSelectExcelFile_SaleInvoice.Name
                TxtExcelPath_SaleInvoice.Text = myExcelFilePath
                'MyConnection_SaleInvoice = New System.Data.OleDb.OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0; " &
                '"data source='" & myExcelFilePath & " '; " & "Extended Properties=Excel 8.0;")
                MyConnection_SaleInvoice = New System.Data.OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + myExcelFilePath + ";Extended Properties=Excel 12.0;")
                MyConnection_SaleInvoice.Open()
            Case BtnSelectExcelFile_SaleInvoiceDetail.Name
                TxtExcelPath_SaleInvoiceDetail.Text = myExcelFilePath
                'MyConnection_SaleInvoiceDetail = New System.Data.OleDb.OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0; " &
                '"data source='" & myExcelFilePath & " '; " & "Extended Properties=Excel 8.0;")
                MyConnection_SaleInvoiceDetail = New System.Data.OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + myExcelFilePath + ";Extended Properties=Excel 12.0;")
                MyConnection_SaleInvoiceDetail.Open()
            Case BtnSelectExcelFile_SaleInvoiceDimensionDetail.Name
                TxtExcelPath_SaleInvoiceDimensionDetail.Text = myExcelFilePath
                'MyConnection_SaleInvoiceDimensionDetail = New System.Data.OleDb.OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0; " &
                '"data source='" & myExcelFilePath & " '; " & "Extended Properties=Excel 8.0;")
                MyConnection_SaleInvoiceDimensionDetail = New System.Data.OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + myExcelFilePath + ";Extended Properties=Excel 12.0;")
                MyConnection_SaleInvoiceDimensionDetail.Open()
        End Select


    End Sub

    Private Sub BtnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnOK.Click, BtnCancel.Click
        Dim MyCommand As OleDb.OleDbDataAdapter = Nothing
        Select Case sender.name
            Case BtnOK.Name
                Dim DtSheetNames_SaleInvoice As DataTable = MyConnection_SaleInvoice.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)
                Dim IsShee1Exist_SaleInvoice As Boolean = False
                For I As Integer = 0 To DtSheetNames_SaleInvoice.Rows.Count - 1
                    If AgL.StrCmp(DtSheetNames_SaleInvoice.Rows(I)("Table_Name"), "sheet1$") Then
                        IsShee1Exist_SaleInvoice = True
                        Exit For
                    End If
                Next
                If IsShee1Exist_SaleInvoice = False Then
                    MsgBox("Sheet1 does not exist in selected file.", MsgBoxStyle.Information)
                    MyConnection_SaleInvoice.Close()
                    Exit Sub
                End If


                Dim DtSheetNames_SaleInvoiceDetail As DataTable = MyConnection_SaleInvoiceDetail.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)
                Dim IsShee1Exist_SaleInvoiceDetail As Boolean = False
                For I As Integer = 0 To DtSheetNames_SaleInvoiceDetail.Rows.Count - 1
                    If AgL.StrCmp(DtSheetNames_SaleInvoiceDetail.Rows(I)("Table_Name"), "sheet1$") Then
                        IsShee1Exist_SaleInvoiceDetail = True
                        Exit For
                    End If
                Next
                If IsShee1Exist_SaleInvoiceDetail = False Then
                    MsgBox("Sheet1 does not exist in selected file.", MsgBoxStyle.Information)
                    MyConnection_SaleInvoiceDetail.Close()
                    Exit Sub
                End If


                Dim DtSheetNames_SaleInvoiceDimensionDetail As DataTable = MyConnection_SaleInvoiceDimensionDetail.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)
                Dim IsShee1Exist_SaleInvoiceDimensionDetail As Boolean = False
                For I As Integer = 0 To DtSheetNames_SaleInvoiceDimensionDetail.Rows.Count - 1
                    If AgL.StrCmp(DtSheetNames_SaleInvoiceDimensionDetail.Rows(I)("Table_Name"), "sheet1$") Then
                        IsShee1Exist_SaleInvoiceDimensionDetail = True
                        Exit For
                    End If
                Next
                If IsShee1Exist_SaleInvoiceDimensionDetail = False Then
                    MsgBox("Sheet1 does not exist in selected file.", MsgBoxStyle.Information)
                    MyConnection_SaleInvoiceDimensionDetail.Close()
                    Exit Sub
                End If


                MyCommand = New System.Data.OleDb.OleDbDataAdapter("select *  from [sheet1$] ", MyConnection_SaleInvoice)
                MyCommand.Fill(DsExcelData_SaleInvoice)

                MyCommand = New System.Data.OleDb.OleDbDataAdapter("select *  from [sheet1$] ", MyConnection_SaleInvoiceDetail)
                MyCommand.Fill(DsExcelData_SaleInvoiceDetail)

                MyCommand = New System.Data.OleDb.OleDbDataAdapter("select *  from [sheet1$] ", MyConnection_SaleInvoiceDimensionDetail)
                MyCommand.Fill(DsExcelData_SaleInvoiceDimensionDetail)

                mUserAction = sender.text
                Me.Dispose()

            Case BtnCancel.Name
                mUserAction = sender.text
                Me.Dispose()
        End Select
    End Sub

    Private Sub FrmImportSaleFromExcel_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub
End Class