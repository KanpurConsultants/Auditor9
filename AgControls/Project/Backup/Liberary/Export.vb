Public Class Export
    Private Declare Function ShellEx Lib "shell32.dll" Alias "ShellExecuteA" ( _
        ByVal hWnd As Integer, ByVal lpOperation As String, _
        ByVal lpFile As String, ByVal lpParameters As String, _
        ByVal lpDirectory As String, ByVal nShowCmd As Integer) As Integer

    Public Shared Sub exportExcel(ByVal grdView As DataGridView, ByVal mFileName As String, ByVal hWnd As Integer)

        ' Choose the path, name, and extension for the Excel file
        Dim myFile As String = mFileName
        ' Open the file and write the headers
        Dim fs As New IO.StreamWriter(myFile, False)
        fs.WriteLine("<?xml version=""1.0""?>")
        fs.WriteLine("<?mso-application progid=""Excel.Sheet""?>")
        fs.WriteLine("<ss:Workbook xmlns:ss=""urn:schemas-microsoft-com:office:spreadsheet"">")

        'Create the styles for the worksheet
        fs.WriteLine("  <ss:Styles>")
        ' Style for the column headers
        fs.WriteLine("    <ss:Style ss:ID=""1"">")
        fs.WriteLine("      <ss:Font ss:Bold=""1""/>")
        fs.WriteLine("      <ss:Alignment ss:Horizontal=""Center"" ss:Vertical=""Center"" " & _
            "ss:WrapText=""1""/>")
        fs.WriteLine("      <ss:Interior ss:Color=""#C0C0C0"" ss:Pattern=""Solid""/>")
        fs.WriteLine("    </ss:Style>")
        ' Style for the column information
        fs.WriteLine("    <ss:Style ss:ID=""2"">")
        fs.WriteLine("      <ss:Alignment ss:Vertical=""Center"" ss:WrapText=""1""/>")
        fs.WriteLine("    </ss:Style>")
        fs.WriteLine("  </ss:Styles>")

        ' Write the worksheet contents
        fs.WriteLine("<ss:Worksheet ss:Name=""Sheet1"">")
        fs.WriteLine("  <ss:Table>")
        For i As Integer = 0 To grdView.Columns.Count - 1
            If grdView.Columns(i).Visible = True Then
                fs.WriteLine(String.Format("    <ss:Column ss:Width=""{0}""/>", _
                grdView.Columns.Item(i).Width))
            End If
        Next
        fs.WriteLine("    <ss:Row>")
        For i As Integer = 0 To grdView.Columns.Count - 1
            If grdView.Columns(i).Visible = True Then
                fs.WriteLine(String.Format("      <ss:Cell ss:StyleID=""1"">" & _
                    "<ss:Data ss:Type=""String"">{0}</ss:Data></ss:Cell>", _
                    grdView.Columns.Item(i).HeaderText))
            End If
        Next
        fs.WriteLine("    </ss:Row>")

        ' Check for an empty row at the end due to Adding allowed on the DataGridView
        Dim subtractBy As Integer, cellText As String
        If grdView.AllowUserToAddRows = True Then subtractBy = 2 Else subtractBy = 1
        ' Write contents for each cell
        For i As Integer = 0 To grdView.RowCount - subtractBy
            fs.WriteLine(String.Format("    <ss:Row ss:Height=""{0}"">", _
                grdView.Rows(i).Height))
            For intCol As Integer = 0 To grdView.Columns.Count - 1
                If grdView.Columns(intCol).Visible = True Then
                    cellText = CStr(IIf(IsDBNull(grdView.Item(intCol, i).FormattedValue), "", grdView.Item(intCol, i).FormattedValue))
                    ' Check for null cell and change it to empty to avoid error
                    If cellText = vbNullString Then cellText = ""

                    fs.WriteLine(String.Format("      <ss:Cell ss:StyleID=""2"">" & _
                        "<ss:Data ss:Type=""String"">{0}</ss:Data></ss:Cell>", _
                        cellText.ToString))
                End If
            Next
            fs.WriteLine("    </ss:Row>")
        Next

        ' Close up the document
        fs.WriteLine("  </ss:Table>")
        fs.WriteLine("</ss:Worksheet>")
        fs.WriteLine("</ss:Workbook>")
        fs.Close()

        ' Open the file in Microsoft Excel
        ' 10 = SW_SHOWDEFAULT
        ShellEx(hWnd, "Open", myFile, "", "", 10)
    End Sub

    Public Shared Function GetFileName(Optional ByVal FilePath As String = "") As String
        Dim SaveFileDialogBox As SaveFileDialog
        Dim sFilePath As String = ""
        Try
            SaveFileDialogBox = New SaveFileDialog

            SaveFileDialogBox.Title = "File Name"
            SaveFileDialogBox.Filter = "Microsoft Excel Worksheet(*.xls)|*.xls|XLSX Files(*.xlsx)|*.xlsx"

            If FilePath.Trim = "" Then FilePath = My.Application.Info.DirectoryPath
            SaveFileDialogBox.InitialDirectory = FilePath
            SaveFileDialogBox.DefaultExt = "*.xls"
            SaveFileDialogBox.FilterIndex = 1


            SaveFileDialogBox.FileName = ""

            If SaveFileDialogBox.ShowDialog = Windows.Forms.DialogResult.Cancel Then Exit Function

            sFilePath = SaveFileDialogBox.FileName
        Catch ex As Exception
        Finally
            GetFileName = sFilePath
        End Try
    End Function
End Class
