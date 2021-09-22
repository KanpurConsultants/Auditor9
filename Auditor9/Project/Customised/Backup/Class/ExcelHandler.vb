
#Region " Information "
' Class Name : Excel File Handler
' Programmer : Vivek Purohit
' Purpose    : Handle Excel File Operations.
' Date       : 20-Dec-2008
#End Region

#Region " Import Section"
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.OleDb
Imports Excel
Imports System.Reflection
Imports System.Runtime.InteropServices
#End Region

''' <summary>
''' Excel File handler used to read and write excel file.
''' </summary>
''' <remarks></remarks>
''' 
Public Class ExcelHandler

    ''' <summary>
    ''' Return data in dataset from excel file.
    ''' </summary>
    ''' <param name="a_sFilepath">Excel file name for extract data.</param>
    ''' <returns>DataSet</returns>
    Public Function GetDataFromExcel(ByVal a_sFilepath As String) As DataSet
        Dim ds As New DataSet()
        Dim cn As New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & a_sFilepath & ";Extended Properties= Excel 8.0")
        Try
            cn.Open()
        Catch ex As OleDbException
            Console.WriteLine(ex.Message)
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try

        ' It Represents Excel data table Schema.
        Dim dt As New System.Data.DataTable()
        dt = cn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)
        If dt IsNot Nothing OrElse dt.Rows.Count > 0 Then
            For sheet_count As Integer = 0 To dt.Rows.Count - 1
                Try
                    ' Create Query to get Data from sheet.
                    Dim sheetname As String = dt.Rows(sheet_count)("table_name").ToString()
                    Dim da As New OleDbDataAdapter("SELECT * FROM [" & sheetname & "]", cn)
                    da.Fill(ds, sheetname)
                Catch ex As DataException
                    Console.WriteLine(ex.Message)
                Catch ex As Exception
                    Console.WriteLine(ex.Message)
                End Try
            Next
        End If
        cn.Close()
        Return ds
    End Function

    ''' <summary>
    ''' Write Excel file as given file name with given data.
    ''' </summary>
    ''' <param name="a_sFilename">full file name for create excel file.</param>
    ''' <param name="a_sData">data in dataset to be fill in excel shhet.</param>
    ''' <param name="a_sFileTitle">Title of Excel file.</param>
    ''' <param name="a_sErrorMessage">output parameter contains error message if error occurrs.</param>
    ''' <returns>bool</returns>
    Public Function ExportToExcel(ByVal a_sFilename As String, ByVal a_sData As DataSet, ByVal a_sFileTitle As String, ByRef a_sErrorMessage As String) As Boolean
        a_sErrorMessage = String.Empty
        Dim bRetVal As Boolean = False
        Dim dsDataSet As DataSet = Nothing
        Try
            dsDataSet = a_sData

            Dim xlObject As Excel.Application = Nothing
            Dim xlWB As Excel.Workbook = Nothing
            Dim xlSh As Excel.Worksheet = Nothing
            Dim rg As Range = Nothing
            Try
                xlObject = New Excel.Application()
                xlObject.AlertBeforeOverwriting = False
                xlObject.DisplayAlerts = False

                ''This Adds a new woorkbook, you could open the workbook from file also
                xlWB = xlObject.Workbooks.Add(Type.Missing)
                xlWB.SaveAs(a_sFilename, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, _
                Excel.XlSaveAsAccessMode.xlNoChange, Missing.Value, Missing.Value, Missing.Value, Missing.Value)

                xlSh = DirectCast(xlObject.ActiveWorkbook.ActiveSheet, Excel.Worksheet)

                Dim sUpperRange As String = "A1"
                Dim sLastCol As String = "E"
                Dim sLowerRange As String = sLastCol + (dsDataSet.Tables(0).Rows.Count + 1).ToString()

                rg = xlSh.Range(sUpperRange, sLowerRange)
                rg.Value2 = GetData(dsDataSet.Tables(0))

                ' formating
                xlSh.Range("A1", sLastCol & "1").Font.Bold = True
                xlSh.Range("A1", sLastCol & "1").HorizontalAlignment = XlHAlign.xlHAlignCenter
                xlSh.Range(sUpperRange, sLowerRange).EntireColumn.AutoFit()

                Dim r As Excel.Range
                r = xlSh.UsedRange
                r.Font.Name = "Courier New"
                r.Font.Size = 10

                If String.IsNullOrEmpty(a_sFileTitle) Then
                    xlObject.Caption = "untitled"
                Else
                    xlObject.Caption = a_sFileTitle
                End If

                xlWB.Save()
                bRetVal = True
            Catch ex As System.Runtime.InteropServices.COMException
                If ex.ErrorCode = -2147221164 Then
                    a_sErrorMessage = "Error in export: Please install Microsoft Office (Excel) to use the Export to Excel feature."
                ElseIf ex.ErrorCode = -2146827284 Then
                    a_sErrorMessage = "Error in export: Excel allows only 65,536 maximum rows in a sheet."
                Else
                    a_sErrorMessage = (("Error in export: " & ex.Message) + Environment.NewLine & " Error: ") + ex.ErrorCode
                End If
            Catch ex As Exception
                a_sErrorMessage = "Error in export: " & ex.Message
            Finally
                Try
                    If xlWB IsNot Nothing Then
                        xlWB.Close(Nothing, Nothing, Nothing)
                    End If
                    xlObject.Workbooks.Close()
                    xlObject.Quit()
                    If rg IsNot Nothing Then
                        Marshal.ReleaseComObject(rg)
                    End If
                    If xlSh IsNot Nothing Then
                        Marshal.ReleaseComObject(xlSh)
                    End If
                    If xlWB IsNot Nothing Then
                        Marshal.ReleaseComObject(xlWB)
                    End If
                    If xlObject IsNot Nothing Then
                        Marshal.ReleaseComObject(xlObject)
                    End If

                Catch
                End Try
                xlSh = Nothing
                xlWB = Nothing
                xlObject = Nothing
                ' force final cleanup!
                GC.Collect()
                GC.WaitForPendingFinalizers()
            End Try
        Catch ex As Exception
            a_sErrorMessage = "Error in export: " & ex.Message
        End Try

        Return bRetVal
    End Function

    Public Function ExportToExcelMainForm(ByVal a_sData As DataSet, ByVal a_sFileTitle As String, ByRef a_sErrorMessage As String, _
                                         ByVal xlObject As Excel.Application, _
                                         ByVal xlWB As Excel.Workbook) As Boolean
        a_sErrorMessage = String.Empty
        Dim bRetVal As Boolean = False
        Dim dsDataSet As DataSet = Nothing
        Try
            dsDataSet = a_sData

            Dim rg As Range = Nothing
            Dim xlSh As Excel.Worksheet = Nothing

            Try
                xlSh = DirectCast(xlObject.ActiveWorkbook.ActiveSheet, Excel.Worksheet)

                Dim sUpperRange As String = "A1"
                Dim sLastCol As String = "B"
                Dim sLastRow As String = "29"
                Dim sLowerRange As String = sLastCol + (dsDataSet.Tables(0).Rows.Count + 1).ToString()

                rg = xlSh.Range(sUpperRange, sLowerRange)
                rg.Value2 = GetData(dsDataSet.Tables(0))

                'xlSh.Columns(0).Enable = False

                ' formating
                xlSh.Range("A1", sLastCol & "1").Font.Bold = True
                xlSh.Range("A1", sLastCol & "1").Cells.Interior.ColorIndex = 35
                xlSh.Range("A1", "A" & sLastRow).Cells.Interior.ColorIndex = 35


                ' xlSh.Range("A1", sLastCol & "1").Locked = True
                ' xlSh.Range("A1", "A" & sLastRow).EntireColumn.Locked = True
                'xlSh.Range("B2:B29").CellStyle.Locked = False
                'xlSh.Protect("syncfusion", ExcelSheetProtection.FormattingColumns)
                'xlSh.Protect()

                'xlSh.Range("A1", sLastCol & sLastRow).BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, 1)


                xlSh.Range("A1", sLastCol & "1").HorizontalAlignment = XlHAlign.xlHAlignCenter
                xlSh.Range(sUpperRange, sLowerRange).EntireColumn.AutoFit()

                'xlSh.Range("A:A").Locked = True
                'xlSh.Protect()
                'xlSh.EnableSelection = XlEnableSelection.xlNoSelection

                Dim r As Excel.Range
                r = xlSh.UsedRange
                r.Font.Name = "Arial"
                r.Font.Size = 10


                If String.IsNullOrEmpty(a_sFileTitle) Then
                    xlObject.Caption = "untitled"
                Else
                    xlObject.Caption = a_sFileTitle
                End If


                xlWB.Save()
                bRetVal = True
            Catch ex As System.Runtime.InteropServices.COMException
                If ex.ErrorCode = -2147221164 Then
                    a_sErrorMessage = "Error in export: Please install Microsoft Office (Excel) to use the Export to Excel feature."
                ElseIf ex.ErrorCode = -2146827284 Then
                    a_sErrorMessage = "Error in export: Excel allows only 65,536 maximum rows in a sheet."
                Else
                    a_sErrorMessage = (("Error in export: " & ex.Message) + Environment.NewLine & " Error: ") + ex.ErrorCode
                End If
            Catch ex As Exception
                a_sErrorMessage = "Error in export: " & ex.Message
            End Try
        Catch ex As Exception
            a_sErrorMessage = "Error in export: " & ex.Message
        End Try

        Return bRetVal
    End Function

    Public Function ExportToExcelVatNonVat(ByVal a_sData As DataSet, ByVal a_sFileTitle As String, ByRef a_sErrorMessage As String, _
                                         ByVal xlObject As Excel.Application, _
                                         ByVal xlWB As Excel.Workbook) As Boolean
        a_sErrorMessage = String.Empty
        Dim bRetVal As Boolean = False
        Dim dsDataSet As DataSet = Nothing
        Try
            dsDataSet = a_sData
            Dim xlSh As Excel.Worksheet = Nothing
            Dim rg As Range = Nothing
            Try
                xlSh = DirectCast(xlObject.ActiveWorkbook.ActiveSheet, Excel.Worksheet)

                Dim sUpperRange As String = "A1"
                Dim sLastCol As String = "J"
                Dim sLastRow As String = "42"
                Dim sLowerRange As String = sLastCol + (dsDataSet.Tables(0).Rows.Count + 1).ToString()

                rg = xlSh.Range(sUpperRange, sLowerRange)
                rg.Value2 = GetData(dsDataSet.Tables(0))

                ' formating
                xlSh.Range("A1", sLastCol & "1").Font.Bold = True
                xlSh.Range("A1", sLastCol & "1").Cells.Interior.ColorIndex = 35
                xlSh.Range("F1", "I" & sLastRow).Font.Bold = True
                xlSh.Range("F1", "I" & sLastRow).Cells.Interior.ColorIndex = 35
                ' xlSh.Range("A1", sLastCol & "1").Cells.Locked = True
                xlSh.Range("A1", sLastCol & "1").HorizontalAlignment = XlHAlign.xlHAlignCenter
                xlSh.Range(sUpperRange, sLowerRange).EntireColumn.AutoFit()

                Dim r As Excel.Range
                r = xlSh.UsedRange
                r.Font.Name = "Arial"
                r.Font.Size = 10
                If String.IsNullOrEmpty(a_sFileTitle) Then
                    xlObject.Caption = "untitled"
                Else
                    xlObject.Caption = a_sFileTitle
                End If

                xlWB.Save()
                bRetVal = True
            Catch ex As System.Runtime.InteropServices.COMException
                If ex.ErrorCode = -2147221164 Then
                    a_sErrorMessage = "Error in export: Please install Microsoft Office (Excel) to use the Export to Excel feature."
                ElseIf ex.ErrorCode = -2146827284 Then
                    a_sErrorMessage = "Error in export: Excel allows only 65,536 maximum rows in a sheet."
                Else
                    a_sErrorMessage = (("Error in export: " & ex.Message) + Environment.NewLine & " Error: ") + ex.ErrorCode
                End If
            Catch ex As Exception
                a_sErrorMessage = "Error in export: " & ex.Message
            End Try
        Catch ex As Exception
            a_sErrorMessage = "Error in export: " & ex.Message
        End Try

        Return bRetVal
    End Function

    Public Function ExportTaxSaleDetail(ByVal a_sData As DataSet, ByVal a_sFileTitle As String, ByRef a_sErrorMessage As String, _
                                         ByVal xlObject As Excel.Application, _
                                         ByVal xlWB As Excel.Workbook) As Boolean
        a_sErrorMessage = String.Empty
        Dim bRetVal As Boolean = False
        Dim dsDataSet As DataSet = Nothing
        Try
            dsDataSet = a_sData

            Dim xlSh As Excel.Worksheet = Nothing
            Dim rg As Range = Nothing
            Try
                xlSh = DirectCast(xlObject.ActiveWorkbook.ActiveSheet, Excel.Worksheet)

                Dim sUpperRange As String = "A1"
                Dim sLastCol As String = "J"
                Dim sLastRow As String = "29"
                Dim sLowerRange As String = sLastCol + (dsDataSet.Tables(0).Rows.Count + 1).ToString()

                rg = xlSh.Range(sUpperRange, sLowerRange)
                rg.Value2 = GetData(dsDataSet.Tables(0))

                ' formating
                xlSh.Range("A1", sLastCol & "1").Font.Bold = True
                xlSh.Range("A1", sLastCol & "1").Cells.Interior.ColorIndex = 35

                xlSh.Range("A1", sLastCol & "1").HorizontalAlignment = XlHAlign.xlHAlignCenter
                xlSh.Range(sUpperRange, sLowerRange).EntireColumn.AutoFit()

                Dim r As Excel.Range
                r = xlSh.UsedRange
                r.Font.Name = "Arial"
                r.Font.Size = 10


                If String.IsNullOrEmpty(a_sFileTitle) Then
                    xlObject.Caption = "untitled"
                Else
                    xlObject.Caption = a_sFileTitle
                End If

                xlWB.Save()
                bRetVal = True
            Catch ex As System.Runtime.InteropServices.COMException
                If ex.ErrorCode = -2147221164 Then
                    a_sErrorMessage = "Error in export: Please install Microsoft Office (Excel) to use the Export to Excel feature."
                ElseIf ex.ErrorCode = -2146827284 Then
                    a_sErrorMessage = "Error in export: Excel allows only 65,536 maximum rows in a sheet."
                Else
                    a_sErrorMessage = (("Error in export: " & ex.Message) + Environment.NewLine & " Error: ") + ex.ErrorCode
                End If
            Catch ex As Exception
                a_sErrorMessage = "Error in export: " & ex.Message
            End Try
        Catch ex As Exception
            a_sErrorMessage = "Error in export: " & ex.Message
        End Try
        Return bRetVal
    End Function

    Public Function ExportVatBankDetail(ByVal a_sData As DataSet, ByVal a_sFileTitle As String, ByRef a_sErrorMessage As String, _
                                         ByVal xlObject As Excel.Application, _
                                         ByVal xlWB As Excel.Workbook) As Boolean
        a_sErrorMessage = String.Empty
        Dim bRetVal As Boolean = False
        Dim dsDataSet As DataSet = Nothing
        Try
            dsDataSet = a_sData
            Dim xlSh As Excel.Worksheet = Nothing
            Dim rg As Range = Nothing
            Try
                xlSh = DirectCast(xlObject.ActiveWorkbook.ActiveSheet, Excel.Worksheet)

                Dim sUpperRange As String = "A1"
                Dim sLastCol As String = "L"
                Dim sLastRow As String = "29"
                Dim sLowerRange As String = sLastCol + (dsDataSet.Tables(0).Rows.Count + 1).ToString()

                rg = xlSh.Range(sUpperRange, sLowerRange)
                rg.Value2 = GetData(dsDataSet.Tables(0))

                ' formating
                xlSh.Range("A1", sLastCol & "1").Font.Bold = True
                xlSh.Range("A1", sLastCol & "1").Cells.Interior.ColorIndex = 35



                'xlSh.Range("A1", sLastCol & sLastRow).BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, 1)

                xlSh.Range("A1", sLastCol & "1").HorizontalAlignment = XlHAlign.xlHAlignCenter
                xlSh.Range(sUpperRange, sLowerRange).EntireColumn.AutoFit()

                Dim r As Excel.Range
                r = xlSh.UsedRange
                r.Font.Name = "Arial"
                r.Font.Size = 10


                If String.IsNullOrEmpty(a_sFileTitle) Then
                    xlObject.Caption = "untitled"
                Else
                    xlObject.Caption = a_sFileTitle
                End If

                xlWB.Save()
                bRetVal = True
            Catch ex As System.Runtime.InteropServices.COMException
                If ex.ErrorCode = -2147221164 Then
                    a_sErrorMessage = "Error in export: Please install Microsoft Office (Excel) to use the Export to Excel feature."
                ElseIf ex.ErrorCode = -2146827284 Then
                    a_sErrorMessage = "Error in export: Excel allows only 65,536 maximum rows in a sheet."
                Else
                    a_sErrorMessage = (("Error in export: " & ex.Message) + Environment.NewLine & " Error: ") + ex.ErrorCode
                End If
            Catch ex As Exception
                a_sErrorMessage = "Error in export: " & ex.Message
            End Try
        Catch ex As Exception
            a_sErrorMessage = "Error in export: " & ex.Message
        End Try

        Return bRetVal
    End Function

    Public Function ExportAnnuxer24(ByVal a_sData As DataSet, ByVal a_sFileTitle As String, ByRef a_sErrorMessage As String, _
                                         ByVal xlObject As Excel.Application, _
                                         ByVal xlWB As Excel.Workbook) As Boolean
        a_sErrorMessage = String.Empty
        Dim bRetVal As Boolean = False
        Dim dsDataSet As DataSet = Nothing

        Try
            dsDataSet = a_sData

            Dim xlSh As Excel.Worksheet = Nothing
            Dim rg As Range = Nothing
            Try
                xlSh = DirectCast(xlObject.ActiveWorkbook.ActiveSheet, Excel.Worksheet)

                Dim sUpperRange As String = "A1"
                Dim sLastCol As String = "P"
                Dim sLastRow As String = "500"
                Dim sLowerRange As String = sLastCol + (dsDataSet.Tables(0).Rows.Count + 1).ToString()

                rg = xlSh.Range(sUpperRange, sLowerRange)
                rg.Value2 = GetData(dsDataSet.Tables(0))

                ' formating
                xlSh.Range("A1", sLastCol & "1").Font.Bold = True
                xlSh.Range("A1", sLastCol & "1").Cells.Interior.ColorIndex = 35



                'xlSh.Range("A1", sLastCol & sLastRow).BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, 1)

                xlSh.Range("A1", sLastCol & "1").HorizontalAlignment = XlHAlign.xlHAlignLeft
                xlSh.Range(sUpperRange, sLowerRange).EntireColumn.AutoFit()

                Dim r As Excel.Range
                r = xlSh.UsedRange
                r.HorizontalAlignment = XlHAlign.xlHAlignLeft
                r.Font.Name = "Arial"
                r.Font.Size = 10


                If String.IsNullOrEmpty(a_sFileTitle) Then
                    xlObject.Caption = "untitled"
                Else
                    xlObject.Caption = a_sFileTitle
                End If

                xlWB.Save()
                bRetVal = True
            Catch ex As System.Runtime.InteropServices.COMException
                If ex.ErrorCode = -2147221164 Then
                    a_sErrorMessage = "Error in export: Please install Microsoft Office (Excel) to use the Export to Excel feature."
                ElseIf ex.ErrorCode = -2146827284 Then
                    a_sErrorMessage = "Error in export: Excel allows only 65,536 maximum rows in a sheet."
                Else
                    a_sErrorMessage = (("Error in export: " & ex.Message) + Environment.NewLine & " Error: ") + ex.ErrorCode
                End If
            Catch ex As Exception
                a_sErrorMessage = "Error in export: " & ex.Message
            End Try
        Catch ex As Exception
            a_sErrorMessage = "Error in export: " & ex.Message
        End Try

        Return bRetVal
    End Function

    Public Function ExportForm24_Annexure_A1(ByVal a_sData As DataSet, ByVal a_sFileTitle As String, ByRef a_sErrorMessage As String, _
                                         ByVal xlObject As Excel.Application, _
                                         ByVal xlWB As Excel.Workbook) As Boolean
        a_sErrorMessage = String.Empty
        Dim bRetVal As Boolean = False
        Dim dsDataSet As DataSet = Nothing
        Try
            dsDataSet = a_sData
            Dim xlSh As Excel.Worksheet = Nothing
            Dim rg As Range = Nothing
            Try
                xlSh = DirectCast(xlObject.ActiveWorkbook.ActiveSheet, Excel.Worksheet)

                Dim sUpperRange As String = "A1"
                Dim sLastCol As String = "T"
                Dim sLastRow As String = "29"
                Dim sLowerRange As String = sLastCol + (dsDataSet.Tables(0).Rows.Count + 1).ToString()

                rg = xlSh.Range(sUpperRange, sLowerRange)
                rg.Value2 = GetData(dsDataSet.Tables(0))

                ' formating
                xlSh.Range("A1", sLastCol & "1").Font.Bold = True
                xlSh.Range("A1", sLastCol & "1").Cells.Interior.ColorIndex = 35



                'xlSh.Range("A1", sLastCol & sLastRow).BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, 1)

                xlSh.Range("A1", sLastCol & "1").HorizontalAlignment = XlHAlign.xlHAlignCenter
                xlSh.Range(sUpperRange, sLowerRange).EntireColumn.AutoFit()

                Dim r As Excel.Range
                r = xlSh.UsedRange
                r.Font.Name = "Arial"
                r.Font.Size = 10


                If String.IsNullOrEmpty(a_sFileTitle) Then
                    xlObject.Caption = "untitled"
                Else
                    xlObject.Caption = a_sFileTitle
                End If

                xlWB.Save()
                bRetVal = True
            Catch ex As System.Runtime.InteropServices.COMException
                If ex.ErrorCode = -2147221164 Then
                    a_sErrorMessage = "Error in export: Please install Microsoft Office (Excel) to use the Export to Excel feature."
                ElseIf ex.ErrorCode = -2146827284 Then
                    a_sErrorMessage = "Error in export: Excel allows only 65,536 maximum rows in a sheet."
                Else
                    a_sErrorMessage = (("Error in export: " & ex.Message) + Environment.NewLine & " Error: ") + ex.ErrorCode
                End If
            Catch ex As Exception
                a_sErrorMessage = "Error in export: " & ex.Message
            End Try
        Catch ex As Exception
            a_sErrorMessage = "Error in export: " & ex.Message
        End Try
        Return bRetVal
    End Function

    Public Function ExportForm24_Annexure_A2(ByVal a_sData As DataSet, ByVal a_sFileTitle As String, ByRef a_sErrorMessage As String, _
                                         ByVal xlObject As Excel.Application, _
                                         ByVal xlWB As Excel.Workbook) As Boolean
        a_sErrorMessage = String.Empty
        Dim bRetVal As Boolean = False
        Dim dsDataSet As DataSet = Nothing
        Try
            dsDataSet = a_sData

            Dim xlSh As Excel.Worksheet = Nothing
            Dim rg As Range = Nothing
            Try
                xlSh = DirectCast(xlObject.ActiveWorkbook.ActiveSheet, Excel.Worksheet)

                Dim sUpperRange As String = "A1"
                Dim sLastCol As String = "P"
                Dim sLastRow As String = "29"
                Dim sLowerRange As String = sLastCol + (dsDataSet.Tables(0).Rows.Count + 1).ToString()

                rg = xlSh.Range(sUpperRange, sLowerRange)
                rg.Value2 = GetData(dsDataSet.Tables(0))

                ' formating
                xlSh.Range("A1", sLastCol & "1").Font.Bold = True
                xlSh.Range("A1", sLastCol & "1").Cells.Interior.ColorIndex = 35



                'xlSh.Range("A1", sLastCol & sLastRow).BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, 1)

                xlSh.Range("A1", sLastCol & "1").HorizontalAlignment = XlHAlign.xlHAlignCenter
                xlSh.Range(sUpperRange, sLowerRange).EntireColumn.AutoFit()

                Dim r As Excel.Range
                r = xlSh.UsedRange
                r.Font.Name = "Arial"
                r.Font.Size = 10


                If String.IsNullOrEmpty(a_sFileTitle) Then
                    xlObject.Caption = "untitled"
                Else
                    xlObject.Caption = a_sFileTitle
                End If

                xlWB.Save()
                bRetVal = True
            Catch ex As System.Runtime.InteropServices.COMException
                If ex.ErrorCode = -2147221164 Then
                    a_sErrorMessage = "Error in export: Please install Microsoft Office (Excel) to use the Export to Excel feature."
                ElseIf ex.ErrorCode = -2146827284 Then
                    a_sErrorMessage = "Error in export: Excel allows only 65,536 maximum rows in a sheet."
                Else
                    a_sErrorMessage = (("Error in export: " & ex.Message) + Environment.NewLine & " Error: ") + ex.ErrorCode
                End If
            Catch ex As Exception
                a_sErrorMessage = "Error in export: " & ex.Message
            Finally
            End Try
        Catch ex As Exception
            a_sErrorMessage = "Error in export: " & ex.Message
        End Try
        Return bRetVal
    End Function

    Public Function ExportCommAgent(ByVal a_sData As DataSet, ByVal a_sFileTitle As String, ByRef a_sErrorMessage As String, _
                                         ByVal xlObject As Excel.Application, _
                                         ByVal xlWB As Excel.Workbook) As Boolean
        a_sErrorMessage = String.Empty
        Dim bRetVal As Boolean = False
        Dim dsDataSet As DataSet = Nothing
        Try
            dsDataSet = a_sData
            Dim xlSh As Excel.Worksheet = Nothing
            Dim rg As Range = Nothing
            Try
                xlSh = DirectCast(xlObject.ActiveWorkbook.ActiveSheet, Excel.Worksheet)

                Dim sUpperRange As String = "A1"
                Dim sLastCol As String = "J"
                Dim sLastRow As String = "29"
                Dim sLowerRange As String = sLastCol + (dsDataSet.Tables(0).Rows.Count + 1).ToString()

                rg = xlSh.Range(sUpperRange, sLowerRange)
                rg.Value2 = GetData(dsDataSet.Tables(0))

                ' formating
                xlSh.Range("A1", sLastCol & "1").Font.Bold = True
                xlSh.Range("A1", sLastCol & "1").Cells.Interior.ColorIndex = 35



                'xlSh.Range("A1", sLastCol & sLastRow).BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, 1)

                xlSh.Range("A1", sLastCol & "1").HorizontalAlignment = XlHAlign.xlHAlignCenter
                xlSh.Range(sUpperRange, sLowerRange).EntireColumn.AutoFit()

                Dim r As Excel.Range
                r = xlSh.UsedRange
                r.Font.Name = "Arial"
                r.Font.Size = 10


                If String.IsNullOrEmpty(a_sFileTitle) Then
                    xlObject.Caption = "untitled"
                Else
                    xlObject.Caption = a_sFileTitle
                End If

                xlWB.Save()
                bRetVal = True
            Catch ex As System.Runtime.InteropServices.COMException
                If ex.ErrorCode = -2147221164 Then
                    a_sErrorMessage = "Error in export: Please install Microsoft Office (Excel) to use the Export to Excel feature."
                ElseIf ex.ErrorCode = -2146827284 Then
                    a_sErrorMessage = "Error in export: Excel allows only 65,536 maximum rows in a sheet."
                Else
                    a_sErrorMessage = (("Error in export: " & ex.Message) + Environment.NewLine & " Error: ") + ex.ErrorCode
                End If
            Catch ex As Exception
                a_sErrorMessage = "Error in export: " & ex.Message
            End Try
        Catch ex As Exception
            a_sErrorMessage = "Error in export: " & ex.Message
        End Try

        Return bRetVal
    End Function

    Public Function ExportForm24_Annexure_C(ByVal a_sData As DataSet, ByVal a_sFileTitle As String, ByRef a_sErrorMessage As String, _
                                         ByVal xlObject As Excel.Application, _
                                         ByVal xlWB As Excel.Workbook) As Boolean
        a_sErrorMessage = String.Empty
        Dim bRetVal As Boolean = False
        Dim dsDataSet As DataSet = Nothing
        Try
            dsDataSet = a_sData
            Dim xlSh As Excel.Worksheet = Nothing
            Dim rg As Range = Nothing
            Try
                xlSh = DirectCast(xlObject.ActiveWorkbook.ActiveSheet, Excel.Worksheet)

                Dim sUpperRange As String = "A1"
                Dim sLastCol As String = "U"
                Dim sLastRow As String = "2"
                Dim sLowerRange As String = sLastCol + (dsDataSet.Tables(0).Rows.Count + 1).ToString()

                rg = xlSh.Range(sUpperRange, sLowerRange)
                rg.Value2 = GetData(dsDataSet.Tables(0))

                ' formating
                xlSh.Range("A1", sLastCol & "1").Font.Bold = True
                xlSh.Range("A1", sLastCol & "1").Cells.Interior.ColorIndex = 35



                'xlSh.Range("A1", sLastCol & sLastRow).BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, 1)

                xlSh.Range("A1", sLastCol & "1").HorizontalAlignment = XlHAlign.xlHAlignCenter
                xlSh.Range(sUpperRange, sLowerRange).EntireColumn.AutoFit()

                Dim r As Excel.Range
                r = xlSh.UsedRange
                r.Font.Name = "Arial"
                r.Font.Size = 10


                If String.IsNullOrEmpty(a_sFileTitle) Then
                    xlObject.Caption = "untitled"
                Else
                    xlObject.Caption = a_sFileTitle
                End If

                xlWB.Save()
                bRetVal = True
            Catch ex As System.Runtime.InteropServices.COMException
                If ex.ErrorCode = -2147221164 Then
                    a_sErrorMessage = "Error in export: Please install Microsoft Office (Excel) to use the Export to Excel feature."
                ElseIf ex.ErrorCode = -2146827284 Then
                    a_sErrorMessage = "Error in export: Excel allows only 65,536 maximum rows in a sheet."
                Else
                    a_sErrorMessage = (("Error in export: " & ex.Message) + Environment.NewLine & " Error: ") + ex.ErrorCode
                End If
            Catch ex As Exception
                a_sErrorMessage = "Error in export: " & ex.Message
            End Try
        Catch ex As Exception
            a_sErrorMessage = "Error in export: " & ex.Message
        End Try

        Return bRetVal
    End Function

    Public Function ExportForm24_Annexure_D(ByVal a_sData As DataSet, ByVal a_sFileTitle As String, ByRef a_sErrorMessage As String, _
                                         ByVal xlObject As Excel.Application, _
                                         ByVal xlWB As Excel.Workbook) As Boolean
        a_sErrorMessage = String.Empty
        Dim bRetVal As Boolean = False
        Dim dsDataSet As DataSet = Nothing
        Try
            dsDataSet = a_sData
            Dim xlSh As Excel.Worksheet = Nothing
            Dim rg As Range = Nothing
            Try
                xlSh = DirectCast(xlObject.ActiveWorkbook.ActiveSheet, Excel.Worksheet)

                Dim sUpperRange As String = "A1"
                Dim sLastCol As String = "S"
                Dim sLastRow As String = "29"
                Dim sLowerRange As String = sLastCol + (dsDataSet.Tables(0).Rows.Count + 1).ToString()

                rg = xlSh.Range(sUpperRange, sLowerRange)
                rg.Value2 = GetData(dsDataSet.Tables(0))

                ' formating
                xlSh.Range("A1", sLastCol & "1").Font.Bold = True
                xlSh.Range("A1", sLastCol & "1").Cells.Interior.ColorIndex = 35



                'xlSh.Range("A1", sLastCol & sLastRow).BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, 1)

                xlSh.Range("A1", sLastCol & "1").HorizontalAlignment = XlHAlign.xlHAlignCenter
                xlSh.Range(sUpperRange, sLowerRange).EntireColumn.AutoFit()

                Dim r As Excel.Range
                r = xlSh.UsedRange
                r.Font.Name = "Arial"
                r.Font.Size = 10


                If String.IsNullOrEmpty(a_sFileTitle) Then
                    xlObject.Caption = "untitled"
                Else
                    xlObject.Caption = a_sFileTitle
                End If

                xlWB.Save()
                bRetVal = True
            Catch ex As System.Runtime.InteropServices.COMException
                If ex.ErrorCode = -2147221164 Then
                    a_sErrorMessage = "Error in export: Please install Microsoft Office (Excel) to use the Export to Excel feature."
                ElseIf ex.ErrorCode = -2146827284 Then
                    a_sErrorMessage = "Error in export: Excel allows only 65,536 maximum rows in a sheet."
                Else
                    a_sErrorMessage = (("Error in export: " & ex.Message) + Environment.NewLine & " Error: ") + ex.ErrorCode
                End If
            Catch ex As Exception
                a_sErrorMessage = "Error in export: " & ex.Message
            End Try
        Catch ex As Exception
            a_sErrorMessage = "Error in export: " & ex.Message
        End Try

        Return bRetVal
    End Function

    Public Function ExportForm24_Annexure_E(ByVal a_sData As DataSet, ByVal a_sFileTitle As String, ByRef a_sErrorMessage As String, _
                                         ByVal xlObject As Excel.Application, _
                                         ByVal xlWB As Excel.Workbook) As Boolean
        a_sErrorMessage = String.Empty
        Dim bRetVal As Boolean = False
        Dim dsDataSet As DataSet = Nothing
        Try
            dsDataSet = a_sData
            Dim xlSh As Excel.Worksheet = Nothing
            Dim rg As Range = Nothing
            Try
                xlSh = DirectCast(xlObject.ActiveWorkbook.ActiveSheet, Excel.Worksheet)

                Dim sUpperRange As String = "A1"
                Dim sLastCol As String = "AA"
                Dim sLastRow As String = "29"
                Dim sLowerRange As String = sLastCol + (dsDataSet.Tables(0).Rows.Count + 1).ToString()

                rg = xlSh.Range(sUpperRange, sLowerRange)
                rg.Value2 = GetData(dsDataSet.Tables(0))

                ' formating
                xlSh.Range("A1", sLastCol & "1").Font.Bold = True
                xlSh.Range("A1", sLastCol & "1").Cells.Interior.ColorIndex = 35



                'xlSh.Range("A1", sLastCol & sLastRow).BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, 1)

                xlSh.Range("A1", sLastCol & "1").HorizontalAlignment = XlHAlign.xlHAlignCenter
                xlSh.Range(sUpperRange, sLowerRange).EntireColumn.AutoFit()

                Dim r As Excel.Range
                r = xlSh.UsedRange
                r.Font.Name = "Arial"
                r.Font.Size = 10


                If String.IsNullOrEmpty(a_sFileTitle) Then
                    xlObject.Caption = "untitled"
                Else
                    xlObject.Caption = a_sFileTitle
                End If

                xlWB.Save()
                bRetVal = True
            Catch ex As System.Runtime.InteropServices.COMException
                If ex.ErrorCode = -2147221164 Then
                    a_sErrorMessage = "Error in export: Please install Microsoft Office (Excel) to use the Export to Excel feature."
                ElseIf ex.ErrorCode = -2146827284 Then
                    a_sErrorMessage = "Error in export: Excel allows only 65,536 maximum rows in a sheet."
                Else
                    a_sErrorMessage = (("Error in export: " & ex.Message) + Environment.NewLine & " Error: ") + ex.ErrorCode
                End If
            Catch ex As Exception
                a_sErrorMessage = "Error in export: " & ex.Message
            End Try
        Catch ex As Exception
            a_sErrorMessage = "Error in export: " & ex.Message
        End Try

        Return bRetVal
    End Function

    Public Function ExportForm24_Annexure_F(ByVal a_sData As DataSet, ByVal a_sFileTitle As String, ByRef a_sErrorMessage As String, _
                                         ByVal xlObject As Excel.Application, _
                                         ByVal xlWB As Excel.Workbook) As Boolean
        a_sErrorMessage = String.Empty
        Dim bRetVal As Boolean = False
        Dim dsDataSet As DataSet = Nothing
        Try
            dsDataSet = a_sData
            Dim xlSh As Excel.Worksheet = Nothing
            Dim rg As Range = Nothing
            Try
                xlSh = DirectCast(xlObject.ActiveWorkbook.ActiveSheet, Excel.Worksheet)

                Dim sUpperRange As String = "A1"
                Dim sLastCol As String = "O"
                Dim sLastRow As String = "29"
                Dim sLowerRange As String = sLastCol + (dsDataSet.Tables(0).Rows.Count + 1).ToString()

                rg = xlSh.Range(sUpperRange, sLowerRange)
                rg.Value2 = GetData(dsDataSet.Tables(0))

                ' formating
                xlSh.Range("A1", sLastCol & "1").Font.Bold = True
                xlSh.Range("A1", sLastCol & "1").Cells.Interior.ColorIndex = 35



                'xlSh.Range("A1", sLastCol & sLastRow).BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, 1)

                xlSh.Range("A1", sLastCol & "1").HorizontalAlignment = XlHAlign.xlHAlignCenter
                xlSh.Range(sUpperRange, sLowerRange).EntireColumn.AutoFit()

                Dim r As Excel.Range
                r = xlSh.UsedRange
                r.Font.Name = "Arial"
                r.Font.Size = 10


                If String.IsNullOrEmpty(a_sFileTitle) Then
                    xlObject.Caption = "untitled"
                Else
                    xlObject.Caption = a_sFileTitle
                End If

                xlWB.Save()
                bRetVal = True
            Catch ex As System.Runtime.InteropServices.COMException
                If ex.ErrorCode = -2147221164 Then
                    a_sErrorMessage = "Error in export: Please install Microsoft Office (Excel) to use the Export to Excel feature."
                ElseIf ex.ErrorCode = -2146827284 Then
                    a_sErrorMessage = "Error in export: Excel allows only 65,536 maximum rows in a sheet."
                Else
                    a_sErrorMessage = (("Error in export: " & ex.Message) + Environment.NewLine & " Error: ") + ex.ErrorCode
                End If
            Catch ex As Exception
                a_sErrorMessage = "Error in export: " & ex.Message
            End Try
        Catch ex As Exception
            a_sErrorMessage = "Error in export: " & ex.Message
        End Try

        Return bRetVal
    End Function

    ''' <summary>
    ''' returns data as two dimentional object array.
    ''' </summary>
    ''' <param name="a_dtData">DataTable of data.</param>
    ''' <returns>Object Array</returns>
    Private Function GetData(ByVal a_dtData As System.Data.DataTable) As Object(,)
        Dim obj As Object(,) = New Object((a_dtData.Rows.Count + 1) - 1, a_dtData.Columns.Count - 1) {}

        Try
            For j As Integer = 0 To a_dtData.Columns.Count - 1
                obj(0, j) = a_dtData.Columns(j).Caption
            Next

            Dim dt As New DateTime()
            Dim sTmpStr As String = String.Empty

            For i As Integer = 1 To a_dtData.Rows.Count
                For j As Integer = 0 To a_dtData.Columns.Count - 1
                    If a_dtData.Columns(j).DataType Is dt.[GetType]() Then
                        If a_dtData.Rows(i - 1)(j) IsNot DBNull.Value Then
                            DateTime.TryParse(a_dtData.Rows(i - 1)(j).ToString(), dt)
                            obj(i, j) = dt.ToString("MM/dd/yy hh:mm tt")
                        Else
                            obj(i, j) = a_dtData.Rows(i - 1)(j)
                        End If
                    ElseIf a_dtData.Columns(j).DataType Is sTmpStr.[GetType]() Then
                        If a_dtData.Rows(i - 1)(j) IsNot DBNull.Value Then
                            sTmpStr = a_dtData.Rows(i - 1)(j).ToString().Replace(vbCr, "")
                            obj(i, j) = sTmpStr
                        Else
                            obj(i, j) = a_dtData.Rows(i - 1)(j)
                        End If
                    Else
                        obj(i, j) = a_dtData.Rows(i - 1)(j)
                    End If

                Next
            Next
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
        Return obj
    End Function

End Class
