Public Class Export
    Private Declare Function ShellEx Lib "shell32.dll" Alias "ShellExecuteA" ( _
        ByVal hWnd As Integer, ByVal lpOperation As String, _
        ByVal lpFile As String, ByVal lpParameters As String, _
        ByVal lpDirectory As String, ByVal nShowCmd As Integer) As Integer

    Public Shared Sub exportExcel(ByVal DGV As DataGridView, ByVal mFileName As String, ByVal hWnd As Integer)

        Try
            Dim DTB = New DataTable, RWS As Integer, CLS As Integer
            Dim J As Integer
            Dim ExportOnlyTickedRecords As Boolean = False

            If DGV.Columns.Contains("Tick") Then
                If MsgBox("Do you want to export only Ticked records?", vbYesNo) = vbYes Then
                    ExportOnlyTickedRecords = True
                End If
            End If

            For CLS = 0 To DGV.ColumnCount - 1 ' COLUMNS OF DTB
                'MsgBox(DGV.Columns(CLS).Name.ToString)
                For J = 0 To DGV.ColumnCount - 1
                    If CLS = DGV.Columns(J).DisplayIndex And DGV.Columns(CLS).Visible And DGV.Columns(J).Name <> "Tick" Then
                        DTB.Columns.Add(DGV.Columns(CLS).Name.ToString)
                    End If
                Next
            Next

            Dim DRW As DataRow

            For RWS = 0 To DGV.Rows.Count - 1 ' FILL DTB WITH DATAGRIDVIEW
                DRW = DTB.NewRow
                For CLS = 0 To DGV.ColumnCount - 1
                    For J = 0 To DGV.ColumnCount - 1
                        If CLS = DGV.Columns(J).DisplayIndex And DGV.Columns(CLS).Visible And DGV.Columns(J).Name <> "Tick" Then
                            Try
                                If ExportOnlyTickedRecords Then
                                    If DGV.Rows(RWS).Cells("Tick").Value.ToString = "þ" Then
                                        DRW(DGV.Columns(CLS).Name.ToString) = DGV.Rows(RWS).Cells(CLS).Value.ToString
                                    End If
                                Else
                                        DRW(DGV.Columns(CLS).Name.ToString) = DGV.Rows(RWS).Cells(CLS).Value.ToString
                                End If
                            Catch ex As Exception

                            End Try
                        End If
                    Next
                Next

                DTB.Rows.Add(DRW)
            Next

            DTB.AcceptChanges()

            Dim DST As New DataSet
            DST.Tables.Add(DTB)
            Dim FLE As String = My.Computer.FileSystem.SpecialDirectories.Desktop & "\tmp.xml" ' PATH AND FILE NAME WHERE THE XML WIL BE CREATED (EXEMPLE: C:\REPS\XML.xml)
            DTB.WriteXml(FLE)
            Dim EXL As String = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\excel.exe", "Path", "Key does not exist") ' PATH OF/ EXCEL.EXE IN YOUR MICROSOFT OFFICE
            EXL = EXL & "EXCEL.EXE"
            Shell(Chr(34) & EXL & Chr(34) & " " & Chr(34) & FLE & Chr(34), vbNormalFocus) ' OPEN XML WITH EXCEL


        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Public Shared Sub exportExcel1(ByVal grdView As DataGridView, ByVal mFileName As String, ByVal hWnd As Integer)

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
        fs.WriteLine("      <ss:Alignment ss:Horizontal=""Center"" ss:Vertical=""Center"" " &
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
                fs.WriteLine(String.Format("    <ss:Column ss:Width=""{0}""/>",
                grdView.Columns.Item(i).Width))
            End If
        Next
        fs.WriteLine("    <ss:Row>")
        For i As Integer = 0 To grdView.Columns.Count - 1
            If grdView.Columns(i).Visible = True Then
                fs.WriteLine(String.Format("      <ss:Cell ss:StyleID=""1"">" &
                    "<ss:Data ss:Type=""String"">{0}</ss:Data></ss:Cell>",
                    grdView.Columns.Item(i).HeaderText))
            End If
        Next
        fs.WriteLine("    </ss:Row>")

        ' Check for an empty row at the end due to Adding allowed on the DataGridView
        Dim subtractBy As Integer, cellText As String
        If grdView.AllowUserToAddRows = True Then subtractBy = 2 Else subtractBy = 1
        ' Write contents for each cell
        For i As Integer = 0 To grdView.RowCount - subtractBy
            fs.WriteLine(String.Format("    <ss:Row ss:Height=""{0}"">",
                grdView.Rows(i).Height))
            For intCol As Integer = 0 To grdView.Columns.Count - 1
                If grdView.Columns(intCol).Visible = True Then
                    cellText = CStr(IIf(IsDBNull(grdView.Item(intCol, i).FormattedValue), "", grdView.Item(intCol, i).FormattedValue))
                    ' Check for null cell and change it to empty to avoid error
                    If cellText = vbNullString Then cellText = ""

                    fs.WriteLine(String.Format("      <ss:Cell ss:StyleID=""2"">" &
                        "<ss:Data ss:Type=""String"">{0}</ss:Data></ss:Cell>",
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
