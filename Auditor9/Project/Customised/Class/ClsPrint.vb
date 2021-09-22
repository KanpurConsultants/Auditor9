Imports System.Drawing.Printing


Public Class ClsPrint

#Region " Declarations "


    Private Const DATE_FORMAT_MDY As String = "dd/MMM/yyyy"

    ' Count of columns printed per page.
    Private myLastColPrinted As Integer = -1

    ' Flag indicating if number of columns exceedes
    ' width of page.
    Private myPageWidthExceeded As Boolean = False

    'Page counter.
    Private myPageNo As Integer = 0

    'Count of lines printed so far.
    Private myLinesPrintedSoFar As Integer = 0

    'Count of dataset lines processed so far.


    'Number of DataSet rows to print.
    Private myDataSetRows As Integer = 0

    'Number of DataSet columns to print.
    Private myNumberOfColumns As Integer = 0
    Public myRowsProcessedSoFar As Integer = 0

    'Index of the table within the DataSet to print.
    Private myTableIndex As Integer = 0

    'Input DataSet whose rows will be printed. If the dataset has a
    'filter applied, the remaining rows are stored in an array. The 
    'array's contents are then printed. 
    Private myDataSetToPrint As DataSet
    Private myFilteredRows As DataRow()
    Public myDataGrid As AgControls.AgDataGrid

    'Flag indicating dataset is filtered.
    Private myDatasetFiltered As Boolean = False

    'Title to print on the report.
    Private myReportTitle As String = ""

    'If the number of lines to print exceeds this
    ' value a confirmation dialog is displayed.
    ' A value of 0 means "do not prompt".
    Private myLineThreshold As Integer = 0

    'Brush to use for printing.
    Private myDrawBrush As New SolidBrush(Color.Black)

    'Font to use for printing. This is different than the
    ' font displayed in the grid but looks better when printed.
    'The grid font is "Microsoft Sans Serif", 8.25

    Private myDrawFont As New Font("Courier New", 8)

    'Font to use to print the report title.
    Private myDrawFontBold As New Font("Courier New", 8, FontStyle.Bold)

    Private myDrawFontHeader As New Font("Courier New", 10, FontStyle.Bold)

    'Flag indicating if the PageSetupDialog was shown.
    Private myPageSetUp As Boolean = False


    Private myPPE As PrintPageEventArgs = Nothing
    'PrintDocument to process. Declared WithEvents so
    'Windows can fire its PrintPage event when its
    'Print method is invoked.
    Private WithEvents myDocumentToPrint As PrintDocument
    Private Const aSpaceBetweenTwoGroupFields As Integer = 30




    Dim aLeftMargin As Single
    Dim aTextHeight As Single
    Dim aTextHeaderHeight As Single
    Dim aXPos As Single
    Dim aYPos As Single
    Dim aColIndex As Integer = 0
    Dim aRowIndex As Integer
    Dim aLinesFilled As Integer
    Dim aLinesPerPage As Integer = 0
    Dim aCharactersFitted As Integer
    Dim aLinesPrintedThisPage As Single = 0
    Dim aPageWidth As Integer
    Dim aColsToPrint As Integer
    Dim aLinesPrintedSoFar As Integer
    Dim aFirstColToPrint As Integer
    Dim aRow As DataRow
    Dim aCol As DataColumn
    Dim aStringSize As New SizeF
    Dim aLayoutSize As New SizeF
    Dim aNewStringFormat As New StringFormat
    Dim aHeaderObj As New System.Text.StringBuilder
    Dim aPrintIt As Boolean
    Dim aPrintStr As String
    Dim aColWidth() As Int16
    Dim NumberOfRowsToScan As Long = 100
    Dim mSpaceBetween2PrintedColumns As Double = 0
    Public arrGrpField() As Boolean
    Public arrGrpFieldName() As String
    Public arrLastRecord() As String
    Public arrColumnOrder() As Integer
    Public arrWrapText() As Boolean
    Public arrRow1Columns() As String
    Public arrRow2Columns() As String
    Public arrRow3Columns() As String
    Public Row1ColumnCount As Integer, Row2ColumnCount As Integer, Row3ColumnCount As Integer
    Public arrColumnMaxWidth() As Integer
    Public arrHeaderColumn() As String
    Public arrLineDetailColumn() As String

    Dim mFooterValue()
    Dim mSubFooterValue()
    Dim mLastValues()
    Dim mNextValues()
    Dim mColumnDataType()


    Public Enum myAlignment
        Left = 0
        Right = 1
        Center = 2
    End Enum

    Public Structure StructGroupBy
        Dim FieldName As String
        Dim Ascending As Boolean
        Dim SubTotal As Boolean
        Dim GroupHeader As Boolean
        Dim FieldIndex As Integer

        Sub StructGroupBy()
            FieldName = ""
            Ascending = True
            SubTotal = True
            GroupHeader = False
            FieldIndex = -1
        End Sub
    End Structure








#End Region

#Region " Constructor Code "

    Friend Sub New()

        myDocumentToPrint = New PrintDocument


    End Sub

#End Region

#Region " Class Properties "

    Public Property NumberOfColumns() As Integer

        '
        ' Number of dataset columns to print.
        '

        Get
            NumberOfColumns = myNumberOfColumns
        End Get

        Set(ByVal theValue As Integer)
            myNumberOfColumns = theValue
            ReDim mFooterValue(theValue)
            ReDim mSubFooterValue(theValue)
            ReDim mLastValues(theValue)
            ReDim mNextValues(theValue)
            ReDim mColumnDataType(theValue)
        End Set

    End Property

    Public WriteOnly Property ReportTitle() As String

        '
        ' Allows setting the title to be used for the report.
        '
        Set(ByVal theValue As String)
            myReportTitle = theValue
        End Set

    End Property

    Public WriteOnly Property LineThreshold() As Integer

        '
        ' If the number of lines to print exceeds this
        ' value a confirmation prompt is displayed.
        '
        Set(ByVal theValue As Integer)
            myLineThreshold = theValue
        End Set

    End Property

    Public WriteOnly Property TableIndex() As Integer

        '
        ' Index of the table within the DataSet to print.
        '

        Set(ByVal theValue As Integer)
            myTableIndex = theValue
        End Set

    End Property




    Public WriteOnly Property DataSetToPrint() As DataSet

        '
        ' Sets the dataset whose content is to be printed.
        '

        Set(ByVal theValue As DataSet)
            Try
                myDataSetToPrint = theValue
                '
                ' Get the total number of DataSet rows to print.
                '
                Dim aFilter As String = myDataSetToPrint.Tables(myTableIndex).DefaultView.RowFilter.Trim

                If aFilter = "" Then
                    myDatasetFiltered = False
                    myDataSetRows = myDataSetToPrint.Tables(myTableIndex).Rows.Count - 1
                Else
                    myDatasetFiltered = True
                    myDataSetRows = myDataSetToPrint.Tables(myTableIndex).DefaultView.Count - 1
                    myFilteredRows = myDataSetToPrint.Tables(myTableIndex).Select(aFilter)
                End If

            Catch e As Exception
                Throw New Exception("Error initializing the print data.", e)
            End Try
        End Set

    End Property

#End Region

#Region " Public Method to Setup Print Page "

    Public Sub PageSetupDialog(ByVal theShowDialogFlag As Boolean)

        '
        ' Display the Page Setup Dialog.
        '

        Try
            '
            ' Set the PageSetupDialog's print document to 
            ' the current document.

            Dim aPS As New PageSetupDialog

            aPS.Document = myDocumentToPrint


            '
            ' On the first call to the print dialog
            ' initialize the document's properties.
            '

            If Not myPageSetUp Then
                With aPS.Document.DefaultPageSettings
                    .Margins.Top = 50
                    .Margins.Left = 50
                    .Margins.Right = 50
                    .Margins.Bottom = 50
                    .Landscape = True
                End With
            End If

            '
            ' Display the PageSetupDialog.
            '
            If theShowDialogFlag Then aPS.ShowDialog()
            myPageSetUp = True

        Catch e As Exception
            Throw New Exception("Error displaying Page Setup dialog.", e)
        End Try

    End Sub

#End Region

#Region " Public Print/Preview Methods "

    Public Sub PrintPreview()

        '
        ' Display a page Preview window showing what the 
        ' printed dataset will look like.
        '

        Try
            '
            ' Get out if no dataset was passed in.
            '
            'If myDataSetToPrint Is Nothing Then Exit Sub
            'If myTableIndex > myDataSetToPrint.Tables.Count Then Exit Sub
            'If myDataSetToPrint.Tables(myTableIndex).Rows.Count = 0 Then Exit Sub

            '
            ' Reset counters.
            '
            myPageNo = 0
            myLinesPrintedSoFar = 0


            '
            ' Inintialize the page settings.
            '
            If Not myPageSetUp Then
                PageSetupDialog(False)
            End If


            '
            ' Show the Print Preview Dialog.
            '
            Dim aPrevDialog As New PrintPreviewDialog

            With aPrevDialog
                .Document = myDocumentToPrint
                .Size = New System.Drawing.Size(600, 400)
                .Top = (Screen.PrimaryScreen.Bounds.Height - 600) \ 2
                .Left = (Screen.PrimaryScreen.Bounds.Width - 400) \ 2




                .ShowDialog()
            End With

        Catch e As Exception
            Throw New Exception("Unable to preview report.", e)
        End Try

    End Sub

    Public Sub Print()

        '
        ' Print the contents of a dataset.
        '

        Try
            '
            ' Get out if no dataset was passed in.
            '
            If myDataSetToPrint Is Nothing Then Exit Sub
            If myTableIndex > myDataSetToPrint.Tables.Count Then Exit Sub
            If myDataSetToPrint.Tables(myTableIndex).Rows.Count = 0 Then Exit Sub


            '
            ' Confirm printing large amounts of data.
            '

            If myLineThreshold > 0 Then
                Dim aLines As Integer = myDataSetToPrint.Tables(myTableIndex).Rows.Count

                If aLines > myLineThreshold And myLineThreshold <> 0 Then
                    If MessageBox.Show( _
                            "There are approximately " & aLines.ToString & " lines to print." & _
                             vbCrLf & vbCrLf & _
                            "Print anyway?", "Print Confirmation", _
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question, _
                            MessageBoxDefaultButton.Button2) = DialogResult.No Then

                        Exit Sub
                    End If
                End If
            End If

            '
            ' Reset counters.
            '
            myPageNo = 0
            myLinesPrintedSoFar = 0


            '
            ' Inintialize the PageSetup.
            '
            If Not myPageSetUp Then PageSetupDialog(False)

            '
            ' Print the document.
            '
            myDocumentToPrint.Print()

        Catch e As Exception
            Throw New Exception("Unable to print report.", e)
        End Try

    End Sub

#End Region

#Region " PrintPage Callback event "

    Private Sub PrintDataSet(ByVal Sender As Object, ByVal ev As PrintPageEventArgs) Handles myDocumentToPrint.PrintPage

        '
        '---------------------------------------------------------------------
        ' This call-back procedure is called by the PrintDocument's PrintPage 
        ' event for each page to be printed (until ev.HasMorePages = False.
        '---------------------------------------------------------------------
        '

        '    On Error GoTo ErrorHandler

        Dim aOneCharacterWidth As Single
        Dim iGrp As Integer
        aLeftMargin = ev.MarginBounds.Left
        aTextHeight = myDrawFont.GetHeight(ev.Graphics)
        aTextHeaderHeight = myDrawFontHeader.GetHeight(ev.Graphics)
        aOneCharacterWidth = ev.Graphics.MeasureString("W", myDrawFont).Width
        aXPos = aLeftMargin
        aYPos = ev.MarginBounds.Top
        aLinesPrintedSoFar = myLinesPrintedSoFar
        aLinesPrintedThisPage = 0
        Dim arrWrapValue(myDataGrid.ColumnCount) As String

        Dim mWrapXPos As Single, mWrapYPos As Single, mWrapYPosMax As Single
        Dim mRow1YPos As Single, mRow2YPos As Single, mRow3YPos As Single
        Dim Is3rdRowApplicable As Boolean
        Dim mRowsInOneRecord As Short



        '
        ' Set the number of dataset columns to print. Use the
        ' "NumberOfColumns" property value if set otherwise 
        ' print all columns.
        '

        myPPE = ev



        With myDataGrid.Columns
            If myNumberOfColumns = 0 Then
                aColsToPrint = .Count
            Else
                If myNumberOfColumns > .Count Then
                    aColsToPrint = .Count
                Else
                    aColsToPrint = myNumberOfColumns
                End If
            End If
        End With


        ReDim Preserve arrLastRecord(myDataGrid.ColumnCount)

        '
        ' Calculate the number of lines per page.
        '
        mSpaceBetween2PrintedColumns = 10
        aPageWidth = ev.MarginBounds.Width - aLeftMargin
        aLinesPerPage = Int((ev.MarginBounds.Height - ev.MarginBounds.Top) / aTextHeight)
        aLayoutSize.Height = aTextHeight
        aNewStringFormat.FormatFlags = StringFormatFlags.NoWrap
        myPageNo += 1


        mRowsInOneRecord = 0
        If Row1ColumnCount > 0 Then mRowsInOneRecord += 1
        If Row2ColumnCount > 0 Then mRowsInOneRecord += 1
        If Row3ColumnCount > 0 Then mRowsInOneRecord += 1


        '
        ' Create a header line.
        '

        aPrintStr = AgL.PubCompName
        PrintString(aPrintStr, " ", myAlignment.Center, aXPos, aYPos, , , myDrawFontHeader)

        aYPos += 1 * aTextHeight
        aLinesPrintedThisPage += 1

        aYPos += 1 * aTextHeaderHeight
        aLinesPrintedThisPage += 1

        If AgL.PubCompAdd1.Trim <> "" Then
            aPrintStr = AgL.PubCompAdd1
            PrintString(aPrintStr, " ", myAlignment.Center, aXPos, aYPos, , , myDrawFontBold)

            aYPos += 1 * aTextHeight
            aLinesPrintedThisPage += 1
        End If

        If AgL.PubCompAdd2.Trim <> "" Then
            aPrintStr = AgL.PubCompAdd2
            PrintString(aPrintStr, " ", myAlignment.Center, aXPos, aYPos, , , myDrawFontBold)

            aYPos += 1 * aTextHeight
            aLinesPrintedThisPage += 1
        End If


        aPrintStr = "Date : " & Format(Today, DATE_FORMAT_MDY)
        PrintString(aPrintStr, " ", myAlignment.Left, aXPos, aYPos, , , myDrawFontBold)

        aPrintStr = "Page : " & myPageNo.ToString
        PrintString(aPrintStr, " ", myAlignment.Right, aXPos, aYPos, , , myDrawFontBold)

        aPrintStr = myReportTitle
        PrintString(aPrintStr, " ", myAlignment.Center, aXPos, aYPos, , , myDrawFontHeader)
        aYPos += 1 * aTextHeaderHeight
        aLinesPrintedThisPage += 1




        PrintString("_", "_", myAlignment.Right, aXPos, aYPos)
        aYPos += 2 * aTextHeight : aLinesPrintedThisPage += 2


        '
        ' Print the column names.
        '


        Dim iCol As Integer
        Dim iRow As Integer        
        Dim mStrPrint As String
        Dim mAlignment As myAlignment


        If UBound(arrRow1Columns) = -1 And UBound(arrLineDetailColumn) = -1 Then 'To Print Single Record In Single Row
            For iCol = 2 To myDataGrid.ColumnCount - 1
                Select Case UCase(myDataGrid.Columns(arrColumnOrder(iCol)).ValueType.ToString)
                    Case "SYSTEM.INT32", "SYSTEM.DECIMAL", "SYSTEM.DOUBLE"
                        mAlignment = myAlignment.Right
                    Case Else
                        mAlignment = myAlignment.Left
                End Select



                mStrPrint = AgL.XNull(myDataGrid.Columns(arrColumnOrder(arrColumnOrder(iCol))).HeaderText)
                PrintString(mStrPrint, " ", mAlignment, aXPos, aYPos, , Val(myDataGrid.Columns(arrColumnOrder(iCol)).Tag), myDrawFontBold)
                aXPos += aLayoutSize.Width + Val(myDataGrid.Columns(arrColumnOrder(iCol)).Tag) + mSpaceBetween2PrintedColumns
                If aXPos >= aPageWidth Then Exit For
            Next

            aYPos += aTextHeight : aLinesPrintedThisPage += 1
            aXPos = aLeftMargin
            PrintString("_", "_", myAlignment.Right, aXPos, aYPos)
            aYPos += 2 * aTextHeight : aLinesPrintedThisPage += 2


            For iRow = myRowsProcessedSoFar To myDataGrid.RowCount - 1
                'For iRow = 1 To myDataGrid.RowCount - 1
                mWrapYPosMax = 0
                For iCol = 2 To myDataGrid.ColumnCount - 1
                    Select Case UCase(myDataGrid.Columns(arrColumnOrder(iCol)).ValueType.ToString)
                        Case "SYSTEM.INT32", "SYSTEM.DECIMAL", "SYSTEM.DOUBLE"
                            mAlignment = myAlignment.Right
                        Case Else
                            mAlignment = myAlignment.Left
                    End Select


                    If AgL.XNull(myDataGrid.Item(1, iRow).Value).ToString.Trim <> "" Then
                        If Val(myDataGrid.Item(1, iRow).Value) = arrColumnOrder(iCol) - 2 Then
                            PrintString("_", "_", myAlignment.Right, aXPos, aYPos)
                            aYPos += 1.25 * aTextHeight : aLinesPrintedThisPage += 1.25
                        End If
                    End If


                    If AgL.XNull(myDataGrid.Item(arrColumnOrder(iCol), iRow).Value) = arrLastRecord(iCol) And arrGrpField(iCol - 2) Then
                        mStrPrint = ""
                    Else
                        mStrPrint = AgL.XNull(myDataGrid.Item(arrColumnOrder(iCol), iRow).Value)
                    End If
                    If arrWrapText(arrColumnOrder(iCol)) Then
                        arrWrapValue(arrColumnOrder(iCol)) = AgL.XNull(myDataGrid.Item(arrColumnOrder(iCol), iRow).Value)
                        mWrapXPos = aXPos : mWrapYPos = aYPos
                        If arrWrapText(arrColumnOrder(iCol)) And ev.Graphics.MeasureString(arrWrapValue(arrColumnOrder(iCol)), myDrawFont).Width > Val(myDataGrid.Columns(arrColumnOrder(iCol)).Tag) Then
                            Do While ev.Graphics.MeasureString(arrWrapValue(arrColumnOrder(iCol)), myDrawFont).Width > Val(myDataGrid.Columns(arrColumnOrder(iCol)).Tag) Or arrWrapValue(arrColumnOrder(iCol)) <> ""
                                mStrPrint = PrintString(arrWrapValue(arrColumnOrder(iCol)), " ", mAlignment, mWrapXPos, mWrapYPos, , Val(myDataGrid.Columns(arrColumnOrder(iCol)).Tag))
                                mWrapYPos += aTextHeight
                                If mWrapYPos > mWrapYPosMax Then mWrapYPosMax = mWrapYPos
                                If ev.Graphics.MeasureString(arrWrapValue(arrColumnOrder(iCol)), myDrawFont).Width > Val(myDataGrid.Columns(arrColumnOrder(iCol)).Tag) Then
                                    arrWrapValue(arrColumnOrder(iCol)) = Right(arrWrapValue(arrColumnOrder(iCol)), Len(arrWrapValue(arrColumnOrder(iCol))) - Len(mStrPrint))
                                    'aLinesPrintedThisPage += 1
                                Else
                                    arrWrapValue(arrColumnOrder(iCol)) = ""
                                End If
                            Loop
                        Else
                            PrintString(mStrPrint, " ", mAlignment, aXPos, aYPos, , Val(myDataGrid.Columns(arrColumnOrder(iCol)).Tag))
                        End If
                    Else
                        PrintString(mStrPrint, " ", mAlignment, aXPos, aYPos, , Val(myDataGrid.Columns(arrColumnOrder(iCol)).Tag))
                    End If





                    arrLastRecord(iCol) = AgL.XNull(myDataGrid.Item(arrColumnOrder(iCol), iRow).Value)
                    aXPos += aLayoutSize.Width + Val(myDataGrid.Columns(arrColumnOrder(iCol)).Tag) + mSpaceBetween2PrintedColumns
                    If aXPos >= aPageWidth Then Exit For
                Next

                If iRow > 0 Then
                    If AgL.XNull(myDataGrid.Item(1, iRow).Value).ToString.Trim <> "" Then
                        'If Val(myDataGrid.Item(1, iRow - 1).Value) = arrColumnOrder(iCol) - 2 Then
                        aYPos += 1.25 * aTextHeight : aLinesPrintedThisPage += 2
                        'End If
                    End If
                End If

                aXPos = aLeftMargin
                If mWrapYPosMax > 0 Then
                    aLinesPrintedThisPage += Math.Round((mWrapYPosMax - aYPos) / aTextHeight)
                    aYPos = mWrapYPosMax
                Else
                    aYPos += aTextHeight
                    aLinesPrintedThisPage += 1
                End If

                aLinesPrintedSoFar += 1
                myRowsProcessedSoFar += 1

                If aLinesPrintedThisPage >= aLinesPerPage And myRowsProcessedSoFar <= myDataGrid.RowCount Then
                    ev.HasMorePages = True
                    aLinesPrintedThisPage = 0
                    Exit For
                Else
                    If myRowsProcessedSoFar >= myDataGrid.RowCount Then
                        ev.HasMorePages = False
                        myRowsProcessedSoFar = 0
                        myPageNo = 0
                    End If
                End If
            Next


        Else
            '#######   To Print Single Record In Multiple Rows   #######
            If UBound(arrLineDetailColumn) = -1 Then

                aLeftMargin += IIf(UBound(arrGrpFieldName) <= 0, 1, UBound(arrGrpFieldName)) * aSpaceBetweenTwoGroupFields
                aXPos = aLeftMargin
                For iCol = 0 To UBound(arrRow1Columns)
                    Select Case UCase(myDataGrid.Columns(arrRow1Columns(iCol)).ValueType.ToString)
                        Case "SYSTEM.INT32", "SYSTEM.DECIMAL", "SYSTEM.DOUBLE"
                            mAlignment = myAlignment.Right
                        Case Else
                            mAlignment = myAlignment.Left
                    End Select


                    mRow1YPos = aYPos
                    mStrPrint = IIf(arrRow1Columns(iCol) <> "", AgL.XNull(myDataGrid.Columns(arrRow1Columns(iCol)).HeaderText), "")
                    PrintString(mStrPrint, " ", mAlignment, aXPos, mRow1YPos, , Val(arrColumnMaxWidth(iCol)), myDrawFontBold)

                    If Not arrRow2Columns(iCol) Is Nothing Then
                        If arrRow2Columns(iCol) <> "" Then
                            mRow2YPos = aYPos + aTextHeight
                            If arrRow2Columns(iCol) <> "" Then
                                mStrPrint = AgL.XNull(myDataGrid.Columns(arrRow2Columns(iCol)).HeaderText)
                            Else
                                mStrPrint = ""
                            End If
                            PrintString(mStrPrint, " ", mAlignment, aXPos, mRow2YPos, , Val(arrColumnMaxWidth(iCol)), myDrawFontBold)
                        End If
                    End If

                    If Not arrRow3Columns(0) Is Nothing Then
                        mRow3YPos = mRow2YPos + aTextHeight
                        If arrRow3Columns(iCol) <> "" Then
                            mStrPrint = AgL.XNull(myDataGrid.Columns(arrRow3Columns(iCol)).HeaderText)
                        Else
                            mStrPrint = ""
                        End If
                        PrintString(mStrPrint, " ", mAlignment, aXPos, mRow3YPos, , Val(arrColumnMaxWidth(iCol)), myDrawFontBold)
                    End If


                    aXPos += aLayoutSize.Width + Val(arrColumnMaxWidth(iCol)) + mSpaceBetween2PrintedColumns
                    If aXPos >= aPageWidth Then Exit For

                Next


                aYPos += (mRowsInOneRecord + 1) * aTextHeight : aLinesPrintedThisPage += (mRowsInOneRecord + 1)
                aXPos = aLeftMargin
                PrintString("_", "_", myAlignment.Right, ev.MarginBounds.Left, aYPos)
                aYPos += 2 * aTextHeight : aLinesPrintedThisPage += 2

                If myRowsProcessedSoFar < 0 Then myRowsProcessedSoFar = 0
                For iRow = myRowsProcessedSoFar To myDataGrid.RowCount - 1
                    'For iRow = 1 To myDataGrid.RowCount - 1

                    For iGrp = 0 To UBound(arrGrpFieldName)
                        aXPos = ev.MarginBounds.Left + (iGrp * aSpaceBetweenTwoGroupFields)


                        mStrPrint = AgL.XNull(myDataGrid.Item(arrGrpFieldName(iGrp), iRow).Value)
                        If Not AgL.StrCmp(mStrPrint, arrLastRecord(myDataGrid.Columns(arrGrpFieldName(iGrp)).Index)) Then
                            If mStrPrint <> "" Then
                                If AgL.XNull(myDataGrid.Item(1, iRow).Value).ToString.Trim <> "" Then
                                    'If Val(myDataGrid.Item(1, iRow).Value) = arrColumnOrder(iCol) - 2 Then
                                    PrintString("_", "_", myAlignment.Right, aXPos, aYPos)
                                    aYPos += 1.25 * aTextHeight : aLinesPrintedThisPage += 1.25
                                    'End If
                                End If

                                mStrPrint = arrGrpFieldName(iGrp) & " : " & AgL.XNull(myDataGrid.Item(arrGrpFieldName(iGrp), iRow).Value)

                                PrintString(mStrPrint, " ", myAlignment.Left, aXPos, aYPos, , , myDrawFontBold)
                                If AgL.XNull(myDataGrid.Item(1, iRow).Value).ToString.Trim = "" Then
                                    aYPos += aTextHeight
                                    aLinesPrintedThisPage += 1
                                    aXPos += aSpaceBetweenTwoGroupFields
                                End If
                                arrLastRecord(myDataGrid.Columns(arrGrpFieldName(iGrp)).Index) = AgL.XNull(myDataGrid.Item(arrGrpFieldName(iGrp), iRow).Value)
                            End If
                        End If
                    Next
                    aXPos = aLeftMargin

                    mWrapYPosMax = 0
                    mRow1YPos = aYPos
                    If Row2ColumnCount > 0 Then mRow2YPos = mRow1YPos + aTextHeight
                    If Row3ColumnCount > 0 Then mRow3YPos = mRow2YPos + aTextHeight

                    For iCol = 0 To UBound(arrRow1Columns)

                        mStrPrint = AgL.XNull(myDataGrid.Item(arrRow1Columns(iCol), iRow).Value)

                        Select Case UCase(myDataGrid.Columns(arrRow1Columns(iCol)).ValueType.ToString)
                            Case "SYSTEM.INT32", "SYSTEM.DECIMAL", "SYSTEM.DOUBLE"
                                mAlignment = myAlignment.Right
                            Case Else
                                mAlignment = myAlignment.Left
                        End Select


                        mStrPrint = AgL.XNull(myDataGrid.Item(arrRow1Columns(iCol), iRow).Value)

                        If arrWrapText(myDataGrid.Columns(arrRow1Columns(iCol)).Index) Then
                            arrWrapValue(myDataGrid.Columns(arrRow1Columns(iCol)).Index) = AgL.XNull(myDataGrid.Item(arrRow1Columns(iCol), iRow).Value)
                            mWrapXPos = aXPos : mWrapYPos = aYPos
                            If arrWrapText(myDataGrid.Columns(arrRow1Columns(iCol)).Index) And ev.Graphics.MeasureString(arrWrapValue(myDataGrid.Columns(arrRow1Columns(iCol)).Index), myDrawFont).Width > Val(myDataGrid.Columns(arrRow1Columns(iCol)).Tag) Then
                                Do While ev.Graphics.MeasureString(arrWrapValue(arrRow1Columns(iCol)), myDrawFont).Width > Val(myDataGrid.Columns(arrRow1Columns(iCol)).Tag) Or arrWrapValue(arrRow1Columns(iCol)) <> ""
                                    mStrPrint = PrintString(arrWrapValue(myDataGrid.Columns(arrRow1Columns(iCol)).Index), " ", mAlignment, mWrapXPos, mWrapYPos, , Val(myDataGrid.Columns(arrRow1Columns(iCol)).Tag))
                                    mWrapYPos += aTextHeight
                                    If mWrapYPos > mWrapYPosMax Then mWrapYPosMax = mWrapYPos
                                    If ev.Graphics.MeasureString(arrWrapValue(myDataGrid.Columns(arrRow1Columns(iCol)).Index), myDrawFont).Width > Val(myDataGrid.Columns(arrRow1Columns(iCol)).Tag) Then
                                        arrWrapValue(myDataGrid.Columns(arrRow1Columns(iCol)).Index) = Right(arrWrapValue(myDataGrid.Columns(arrRow1Columns(iCol)).Index), Len(arrWrapValue(myDataGrid.Columns(arrRow1Columns(iCol)).Index)) - Len(mStrPrint))
                                        'aLinesPrintedThisPage += 1
                                    Else
                                        arrWrapValue(myDataGrid.Columns(arrRow1Columns(iCol)).Index) = ""
                                    End If
                                Loop
                            Else
                                PrintString(mStrPrint, " ", mAlignment, aXPos, mRow1YPos, , Val(arrColumnMaxWidth(iCol)))
                            End If
                        Else
                            PrintString(mStrPrint, " ", mAlignment, aXPos, mRow1YPos, , Val(arrColumnMaxWidth(iCol)))
                        End If




                        'Row2Colums Printing
                        If arrRow2Columns(iCol) <> "" Then
                            mStrPrint = AgL.XNull(myDataGrid.Item(arrRow2Columns(iCol), iRow).Value)

                            If arrWrapText(myDataGrid.Columns(arrRow2Columns(iCol)).Index) Then
                                arrWrapValue(myDataGrid.Columns(arrRow2Columns(iCol)).Index) = AgL.XNull(myDataGrid.Item(arrRow2Columns(iCol), iRow).Value)
                                mWrapXPos = aXPos : mWrapYPos = aYPos
                                If arrWrapText(myDataGrid.Columns(arrRow2Columns(iCol)).Index) And ev.Graphics.MeasureString(arrWrapValue(myDataGrid.Columns(arrRow2Columns(iCol)).Index), myDrawFont).Width > Val(myDataGrid.Columns(arrRow2Columns(iCol)).Tag) Then
                                    Do While ev.Graphics.MeasureString(arrWrapValue(arrRow2Columns(iCol)), myDrawFont).Width > Val(myDataGrid.Columns(arrRow2Columns(iCol)).Tag) Or arrWrapValue(arrRow2Columns(iCol)) <> ""
                                        mStrPrint = PrintString(arrWrapValue(myDataGrid.Columns(arrRow2Columns(iCol)).Index), " ", mAlignment, mWrapXPos, mWrapYPos, , Val(arrColumnMaxWidth(iCol)))
                                        mWrapYPos += aTextHeight
                                        If mWrapYPos > mWrapYPosMax Then mWrapYPosMax = mWrapYPos
                                        If ev.Graphics.MeasureString(arrWrapValue(myDataGrid.Columns(arrRow2Columns(iCol)).Index), myDrawFont).Width > Val(myDataGrid.Columns(arrRow2Columns(iCol)).Tag) Then
                                            arrWrapValue(myDataGrid.Columns(arrRow2Columns(iCol)).Index) = Right(arrWrapValue(myDataGrid.Columns(arrRow2Columns(iCol)).Index), Len(arrWrapValue(myDataGrid.Columns(arrRow2Columns(iCol)).Index)) - Len(mStrPrint))
                                            'aLinesPrintedThisPage += 1
                                        Else
                                            arrWrapValue(myDataGrid.Columns(arrRow2Columns(iCol)).Index) = ""
                                        End If
                                    Loop
                                Else
                                    PrintString(mStrPrint, " ", mAlignment, aXPos, mRow2YPos, , Val(arrColumnMaxWidth(iCol)))
                                End If
                            Else
                                PrintString(mStrPrint, " ", mAlignment, aXPos, mRow2YPos, , Val(arrColumnMaxWidth(iCol)))
                            End If

                        Else
                            mStrPrint = ""
                            PrintString(mStrPrint, " ", mAlignment, aXPos, mRow2YPos, , Val(arrColumnMaxWidth(iCol)))
                        End If


                        'Row3Colums Printing
                        If arrRow3Columns(iCol) <> "" Then
                            Is3rdRowApplicable = True
                            mStrPrint = AgL.XNull(myDataGrid.Item(arrRow3Columns(iCol), iRow).Value)

                            If arrWrapText(myDataGrid.Columns(arrRow3Columns(iCol)).Index) Then
                                arrWrapValue(myDataGrid.Columns(arrRow3Columns(iCol)).Index) = AgL.XNull(myDataGrid.Item(arrRow3Columns(iCol), iRow).Value)
                                mWrapXPos = aXPos : mWrapYPos = aYPos
                                If arrWrapText(myDataGrid.Columns(arrRow3Columns(iCol)).Index) And ev.Graphics.MeasureString(arrWrapValue(myDataGrid.Columns(arrRow3Columns(iCol)).Index), myDrawFont).Width > Val(myDataGrid.Columns(arrRow3Columns(iCol)).Tag) Then
                                    Do While ev.Graphics.MeasureString(arrWrapValue(arrRow3Columns(iCol)), myDrawFont).Width > Val(myDataGrid.Columns(arrRow3Columns(iCol)).Tag) Or arrWrapValue(arrRow3Columns(iCol)) <> ""
                                        mStrPrint = PrintString(arrWrapValue(myDataGrid.Columns(arrRow3Columns(iCol)).Index), " ", mAlignment, mWrapXPos, mWrapYPos, , Val(arrColumnMaxWidth(iCol)))
                                        mWrapYPos += aTextHeight
                                        If mWrapYPos > mWrapYPosMax Then mWrapYPosMax = mWrapYPos
                                        If ev.Graphics.MeasureString(arrWrapValue(myDataGrid.Columns(arrRow3Columns(iCol)).Index), myDrawFont).Width > Val(myDataGrid.Columns(arrRow3Columns(iCol)).Tag) Then
                                            arrWrapValue(myDataGrid.Columns(arrRow3Columns(iCol)).Index) = Right(arrWrapValue(myDataGrid.Columns(arrRow3Columns(iCol)).Index), Len(arrWrapValue(myDataGrid.Columns(arrRow3Columns(iCol)).Index)) - Len(mStrPrint))
                                            'aLinesPrintedThisPage += 1
                                        Else
                                            arrWrapValue(myDataGrid.Columns(arrRow3Columns(iCol)).Index) = ""
                                        End If
                                    Loop
                                Else
                                    PrintString(mStrPrint, " ", mAlignment, aXPos, mRow3YPos, , Val(arrColumnMaxWidth(iCol)))
                                End If
                            Else
                                PrintString(mStrPrint, " ", mAlignment, aXPos, mRow3YPos, , Val(arrColumnMaxWidth(iCol)))
                            End If

                        Else
                            mStrPrint = ""
                            PrintString(mStrPrint, " ", mAlignment, aXPos, mRow3YPos, , Val(arrColumnMaxWidth(iCol)))
                        End If









                        arrLastRecord(myDataGrid.Columns(arrRow1Columns(iCol)).Index) = AgL.XNull(myDataGrid.Item(arrRow1Columns(iCol), iRow).Value)
                        aXPos += aLayoutSize.Width + Val(arrColumnMaxWidth(iCol)) + mSpaceBetween2PrintedColumns
                        If aXPos >= aPageWidth Then Exit For
                    Next

                    If iRow > 0 Then
                        If AgL.XNull(myDataGrid.Item(1, iRow).Value).ToString.Trim <> "" Then
                            'If Val(myDataGrid.Item(1, iRow - 1).Value) = arrColumnOrder(iCol) - 2 Then
                            aYPos += 1.25 * aTextHeight : aLinesPrintedThisPage += 2
                            'End If
                        End If
                    End If

                    aXPos = aLeftMargin
                    If mWrapYPosMax > 0 Then
                        aLinesPrintedThisPage += Math.Round((mWrapYPosMax - aYPos) / aTextHeight)
                        aYPos = mWrapYPosMax
                    Else
                        If Is3rdRowApplicable Then
                            aYPos += mRowsInOneRecord * aTextHeight
                            aLinesPrintedThisPage += mRowsInOneRecord
                            Is3rdRowApplicable = False
                        Else
                            aYPos += mRowsInOneRecord * aTextHeight
                            aLinesPrintedThisPage += mRowsInOneRecord
                        End If
                    End If

                    aLinesPrintedSoFar += 1
                    myRowsProcessedSoFar += 1

                    If aLinesPrintedThisPage >= aLinesPerPage And myRowsProcessedSoFar < myDataGrid.RowCount Then
                        ev.HasMorePages = True
                        aLinesPrintedThisPage = 0
                        Exit For
                    Else
                        If myRowsProcessedSoFar >= myDataGrid.RowCount Then
                            ev.HasMorePages = False
                            myRowsProcessedSoFar = -1
                            myPageNo = 0
                        End If

                    End If
                Next
            Else

                '#######  TO PRINT LINE AND HEADER WISE RECORDS   #######

                aLeftMargin += UBound(arrGrpFieldName) * aSpaceBetweenTwoGroupFields
                aXPos = aLeftMargin
                For iCol = 0 To UBound(arrRow1Columns)
                    Select Case UCase(myDataGrid.Columns(arrRow1Columns(iCol)).ValueType.ToString)
                        Case "SYSTEM.INT32", "SYSTEM.DECIMAL", "SYSTEM.DOUBLE"
                            mAlignment = myAlignment.Right
                        Case Else
                            mAlignment = myAlignment.Left
                    End Select


                    mRow1YPos = aYPos
                    mStrPrint = IIf(arrRow1Columns(iCol) <> "", AgL.XNull(myDataGrid.Columns(arrRow1Columns(iCol)).HeaderText), "")
                    PrintString(mStrPrint, " ", mAlignment, aXPos, mRow1YPos, , Val(arrColumnMaxWidth(iCol)), myDrawFontBold)

                    If arrRow2Columns(iCol) <> "" Then
                        mRow2YPos = aYPos + aTextHeight
                        If arrRow2Columns(iCol) <> "" Then
                            mStrPrint = AgL.XNull(myDataGrid.Columns(arrRow2Columns(iCol)).HeaderText)
                        Else
                            mStrPrint = ""
                        End If
                        PrintString(mStrPrint, " ", mAlignment, aXPos, mRow2YPos, , Val(arrColumnMaxWidth(iCol)), myDrawFontBold)
                    End If

                    mRow3YPos = mRow2YPos + aTextHeight
                    If arrRow3Columns(iCol) <> "" Then
                        mStrPrint = AgL.XNull(myDataGrid.Columns(arrRow3Columns(iCol)).HeaderText)
                    Else
                        mStrPrint = ""
                    End If
                    PrintString(mStrPrint, " ", mAlignment, aXPos, mRow3YPos, , Val(arrColumnMaxWidth(iCol)), myDrawFontBold)


                    aXPos += aLayoutSize.Width + Val(arrColumnMaxWidth(iCol)) + mSpaceBetween2PrintedColumns
                    If aXPos >= aPageWidth Then Exit For

                Next


                aYPos += 3 * aTextHeight : aLinesPrintedThisPage += 3
                aXPos = aLeftMargin
                PrintString("_", "_", myAlignment.Right, ev.MarginBounds.Left, aYPos)
                aYPos += 2 * aTextHeight : aLinesPrintedThisPage += 2


                For iRow = myRowsProcessedSoFar To myDataGrid.RowCount - 1

                    For iGrp = 0 To UBound(arrGrpFieldName)
                        aXPos = ev.MarginBounds.Left + (iGrp * aSpaceBetweenTwoGroupFields)


                        mStrPrint = AgL.XNull(myDataGrid.Item(arrGrpFieldName(iGrp), iRow).Value)
                        If Not AgL.StrCmp(mStrPrint, arrLastRecord(myDataGrid.Columns(arrGrpFieldName(iGrp)).Index)) Then
                            If mStrPrint <> "" Then
                                If AgL.XNull(myDataGrid.Item(1, iRow).Value).ToString.Trim <> "" Then
                                    PrintString("_", "_", myAlignment.Right, aXPos, aYPos)
                                    aYPos += 1.25 * aTextHeight : aLinesPrintedThisPage += 1.25
                                End If

                                mStrPrint = arrGrpFieldName(iGrp) & " : " & AgL.XNull(myDataGrid.Item(arrGrpFieldName(iGrp), iRow).Value)

                                PrintString(mStrPrint, " ", myAlignment.Left, aXPos, aYPos, , , myDrawFontBold)
                                If AgL.XNull(myDataGrid.Item(1, iRow).Value).ToString.Trim = "" Then
                                    aYPos += aTextHeight
                                    aLinesPrintedThisPage += 1
                                    aXPos += aSpaceBetweenTwoGroupFields
                                End If
                                arrLastRecord(myDataGrid.Columns(arrGrpFieldName(iGrp)).Index) = AgL.XNull(myDataGrid.Item(arrGrpFieldName(iGrp), iRow).Value)

                                If iGrp = UBound(arrGrpFieldName) Then
                                    aYPos = aYPos + aTextHeight : aLinesPrintedThisPage += 1
                                    aXPos = aLeftMargin

                                    For iCol = 0 To UBound(arrRow1Columns)

                                        mStrPrint = AgL.XNull(myDataGrid.Item(arrRow1Columns(iCol), iRow).Value)

                                        Select Case UCase(myDataGrid.Columns(arrRow1Columns(iCol)).ValueType.ToString)
                                            Case "SYSTEM.INT32", "SYSTEM.DECIMAL", "SYSTEM.DOUBLE"
                                                mAlignment = myAlignment.Right
                                            Case Else
                                                mAlignment = myAlignment.Left
                                        End Select


                                        mStrPrint = AgL.XNull(myDataGrid.Item(arrRow1Columns(iCol), iRow).Value)

                                        If arrWrapText(myDataGrid.Columns(arrRow1Columns(iCol)).Index) Then
                                            arrWrapValue(myDataGrid.Columns(arrRow1Columns(iCol)).Index) = AgL.XNull(myDataGrid.Item(arrRow1Columns(iCol), iRow).Value)
                                            mWrapXPos = aXPos : mWrapYPos = aYPos
                                            If arrWrapText(myDataGrid.Columns(arrRow1Columns(iCol)).Index) And ev.Graphics.MeasureString(arrWrapValue(myDataGrid.Columns(arrRow1Columns(iCol)).Index), myDrawFont).Width > Val(myDataGrid.Columns(arrRow1Columns(iCol)).Tag) Then
                                                Do While ev.Graphics.MeasureString(arrWrapValue(arrRow1Columns(iCol)), myDrawFont).Width > Val(myDataGrid.Columns(arrRow1Columns(iCol)).Tag) Or arrWrapValue(arrRow1Columns(iCol)) <> ""
                                                    mStrPrint = PrintString(arrWrapValue(myDataGrid.Columns(arrRow1Columns(iCol)).Index), " ", mAlignment, mWrapXPos, mWrapYPos, , Val(myDataGrid.Columns(arrRow1Columns(iCol)).Tag))
                                                    mWrapYPos += aTextHeight
                                                    If mWrapYPos > mWrapYPosMax Then mWrapYPosMax = mWrapYPos
                                                    If ev.Graphics.MeasureString(arrWrapValue(myDataGrid.Columns(arrRow1Columns(iCol)).Index), myDrawFont).Width > Val(myDataGrid.Columns(arrRow1Columns(iCol)).Tag) Then
                                                        arrWrapValue(myDataGrid.Columns(arrRow1Columns(iCol)).Index) = Right(arrWrapValue(myDataGrid.Columns(arrRow1Columns(iCol)).Index), Len(arrWrapValue(myDataGrid.Columns(arrRow1Columns(iCol)).Index)) - Len(mStrPrint))
                                                        'aLinesPrintedThisPage += 1
                                                    Else
                                                        arrWrapValue(myDataGrid.Columns(arrRow1Columns(iCol)).Index) = ""
                                                    End If
                                                Loop
                                            Else
                                                PrintString(mStrPrint, " ", mAlignment, aXPos, mRow1YPos, , Val(arrColumnMaxWidth(iCol)))
                                            End If
                                        Else
                                            PrintString(mStrPrint, " ", mAlignment, aXPos, mRow1YPos, , Val(arrColumnMaxWidth(iCol)))
                                        End If
                                    Next

                                End If

                            End If
                        End If
                    Next
                    aXPos = aLeftMargin

                    mWrapYPosMax = 0
                    mRow1YPos = aYPos
                    mRow2YPos = mRow1YPos + aTextHeight
                    mRow3YPos = mRow2YPos + aTextHeight

                    For iCol = 0 To UBound(arrRow1Columns)

                        mStrPrint = AgL.XNull(myDataGrid.Item(arrRow1Columns(iCol), iRow).Value)

                        Select Case UCase(myDataGrid.Columns(arrRow1Columns(iCol)).ValueType.ToString)
                            Case "SYSTEM.INT32", "SYSTEM.DECIMAL", "SYSTEM.DOUBLE"
                                mAlignment = myAlignment.Right
                            Case Else
                                mAlignment = myAlignment.Left
                        End Select



                        'Row2Colums Printing
                        If arrRow2Columns(iCol) <> "" Then
                            mStrPrint = AgL.XNull(myDataGrid.Item(arrRow2Columns(iCol), iRow).Value)

                            If arrWrapText(myDataGrid.Columns(arrRow2Columns(iCol)).Index) Then
                                arrWrapValue(myDataGrid.Columns(arrRow2Columns(iCol)).Index) = AgL.XNull(myDataGrid.Item(arrRow2Columns(iCol), iRow).Value)
                                mWrapXPos = aXPos : mWrapYPos = aYPos
                                If arrWrapText(myDataGrid.Columns(arrRow2Columns(iCol)).Index) And ev.Graphics.MeasureString(arrWrapValue(myDataGrid.Columns(arrRow2Columns(iCol)).Index), myDrawFont).Width > Val(myDataGrid.Columns(arrRow2Columns(iCol)).Tag) Then
                                    Do While ev.Graphics.MeasureString(arrWrapValue(arrRow2Columns(iCol)), myDrawFont).Width > Val(myDataGrid.Columns(arrRow2Columns(iCol)).Tag) Or arrWrapValue(arrRow2Columns(iCol)) <> ""
                                        mStrPrint = PrintString(arrWrapValue(myDataGrid.Columns(arrRow2Columns(iCol)).Index), " ", mAlignment, mWrapXPos, mWrapYPos, , Val(arrColumnMaxWidth(iCol)))
                                        mWrapYPos += aTextHeight
                                        If mWrapYPos > mWrapYPosMax Then mWrapYPosMax = mWrapYPos
                                        If ev.Graphics.MeasureString(arrWrapValue(myDataGrid.Columns(arrRow2Columns(iCol)).Index), myDrawFont).Width > Val(myDataGrid.Columns(arrRow2Columns(iCol)).Tag) Then
                                            arrWrapValue(myDataGrid.Columns(arrRow2Columns(iCol)).Index) = Right(arrWrapValue(myDataGrid.Columns(arrRow2Columns(iCol)).Index), Len(arrWrapValue(myDataGrid.Columns(arrRow2Columns(iCol)).Index)) - Len(mStrPrint))
                                            'aLinesPrintedThisPage += 1
                                        Else
                                            arrWrapValue(myDataGrid.Columns(arrRow2Columns(iCol)).Index) = ""
                                        End If
                                    Loop
                                Else
                                    PrintString(mStrPrint, " ", mAlignment, aXPos, mRow2YPos, , Val(arrColumnMaxWidth(iCol)))
                                End If
                            Else
                                PrintString(mStrPrint, " ", mAlignment, aXPos, mRow2YPos, , Val(arrColumnMaxWidth(iCol)))
                            End If

                        Else
                            mStrPrint = ""
                            PrintString(mStrPrint, " ", mAlignment, aXPos, mRow2YPos, , Val(arrColumnMaxWidth(iCol)))
                        End If


                        'Row3Colums Printing
                        If arrRow3Columns(iCol) <> "" Then
                            Is3rdRowApplicable = True
                            mStrPrint = AgL.XNull(myDataGrid.Item(arrRow3Columns(iCol), iRow).Value)

                            If arrWrapText(myDataGrid.Columns(arrRow3Columns(iCol)).Index) Then
                                arrWrapValue(myDataGrid.Columns(arrRow3Columns(iCol)).Index) = AgL.XNull(myDataGrid.Item(arrRow3Columns(iCol), iRow).Value)
                                mWrapXPos = aXPos : mWrapYPos = aYPos
                                If arrWrapText(myDataGrid.Columns(arrRow3Columns(iCol)).Index) And ev.Graphics.MeasureString(arrWrapValue(myDataGrid.Columns(arrRow3Columns(iCol)).Index), myDrawFont).Width > Val(myDataGrid.Columns(arrRow3Columns(iCol)).Tag) Then
                                    Do While ev.Graphics.MeasureString(arrWrapValue(arrRow3Columns(iCol)), myDrawFont).Width > Val(myDataGrid.Columns(arrRow3Columns(iCol)).Tag) Or arrWrapValue(arrRow3Columns(iCol)) <> ""
                                        mStrPrint = PrintString(arrWrapValue(myDataGrid.Columns(arrRow3Columns(iCol)).Index), " ", mAlignment, mWrapXPos, mWrapYPos, , Val(arrColumnMaxWidth(iCol)))
                                        mWrapYPos += aTextHeight
                                        If mWrapYPos > mWrapYPosMax Then mWrapYPosMax = mWrapYPos
                                        If ev.Graphics.MeasureString(arrWrapValue(myDataGrid.Columns(arrRow3Columns(iCol)).Index), myDrawFont).Width > Val(myDataGrid.Columns(arrRow3Columns(iCol)).Tag) Then
                                            arrWrapValue(myDataGrid.Columns(arrRow3Columns(iCol)).Index) = Right(arrWrapValue(myDataGrid.Columns(arrRow3Columns(iCol)).Index), Len(arrWrapValue(myDataGrid.Columns(arrRow3Columns(iCol)).Index)) - Len(mStrPrint))
                                            'aLinesPrintedThisPage += 1
                                        Else
                                            arrWrapValue(myDataGrid.Columns(arrRow3Columns(iCol)).Index) = ""
                                        End If
                                    Loop
                                Else
                                    PrintString(mStrPrint, " ", mAlignment, aXPos, mRow3YPos, , Val(arrColumnMaxWidth(iCol)))
                                End If
                            Else
                                PrintString(mStrPrint, " ", mAlignment, aXPos, mRow3YPos, , Val(arrColumnMaxWidth(iCol)))
                            End If

                        Else
                            mStrPrint = ""
                            PrintString(mStrPrint, " ", mAlignment, aXPos, mRow3YPos, , Val(arrColumnMaxWidth(iCol)))
                        End If









                        arrLastRecord(myDataGrid.Columns(arrRow1Columns(iCol)).Index) = AgL.XNull(myDataGrid.Item(arrRow1Columns(iCol), iRow).Value)
                        aXPos += aLayoutSize.Width + Val(arrColumnMaxWidth(iCol)) + mSpaceBetween2PrintedColumns
                        If aXPos >= aPageWidth Then Exit For
                    Next

                    If iRow > 0 Then
                        If AgL.XNull(myDataGrid.Item(1, iRow).Value).ToString.Trim <> "" Then
                            'If Val(myDataGrid.Item(1, iRow - 1).Value) = arrColumnOrder(iCol) - 2 Then
                            aYPos += 1.25 * aTextHeight : aLinesPrintedThisPage += 2
                            'End If
                        End If
                    End If

                    aXPos = aLeftMargin
                    If mWrapYPosMax > 0 Then
                        aLinesPrintedThisPage += Math.Round((mWrapYPosMax - aYPos) / aTextHeight)
                        aYPos = mWrapYPosMax
                    Else
                        If Is3rdRowApplicable Then
                            aYPos += 3 * aTextHeight
                            aLinesPrintedThisPage += 3
                            Is3rdRowApplicable = False
                        Else
                            aYPos += 2 * aTextHeight
                            aLinesPrintedThisPage += 2
                        End If
                    End If

                    aLinesPrintedSoFar += 1
                    myRowsProcessedSoFar += 1

                    If aLinesPrintedThisPage >= aLinesPerPage And myRowsProcessedSoFar <= myDataGrid.RowCount Then
                        ev.HasMorePages = True
                        aLinesPrintedThisPage = 0
                        Exit For
                    Else
                        If myRowsProcessedSoFar >= myDataGrid.RowCount Then
                            ev.HasMorePages = False
                            myRowsProcessedSoFar = 1
                            myPageNo = 0
                        End If
                    End If
                Next




            End If

        End If


        aCol = Nothing
        aRow = Nothing
        aHeaderObj = Nothing
        myPPE = Nothing
        Exit Sub









        'ErrorHandler:
        '        MsgBox(Err.Description)
        '        Throw New Exception("Error formatting report output.", Err.GetException)
    End Sub


 





#End Region

#Region " Printing Functions "

    Private Function PrintString(ByVal strPrint As String, ByVal strStuff As String, ByVal Alignment As myAlignment, ByVal xPos As Single, ByVal yPos As Single, Optional ByVal xPos1 As Single = 0, Optional ByVal mWidth As Single = 0, Optional ByVal mFont As Font = Nothing) As String
        Dim strPrintWidth As Double = myPPE.Graphics.MeasureString(strPrint, myDrawFont).Width
        PrintString = ""
        If xPos1 = 0 Then xPos1 = myPPE.MarginBounds.Right
        If mWidth > 0 Then xPos1 = xPos + mWidth
        If mFont Is Nothing Then mFont = myDrawFont
        If strPrint = "" Then Exit Function
        If strStuff <> "" Then
            Select Case Alignment
                Case myAlignment.Right
                    Do Until myPPE.Graphics.MeasureString(strPrint, mFont).Width >= (xPos1 - xPos)
                        strPrint = strStuff & strPrint
                    Loop
                Case myAlignment.Center
                    Do Until myPPE.Graphics.MeasureString(strPrint, mFont).Width >= (xPos1 - xPos + strPrintWidth) / 2
                        strPrint = strStuff & strPrint
                        strPrint = strPrint & strStuff
                    Loop
                Case myAlignment.Left
                    Do Until myPPE.Graphics.MeasureString(strPrint, mFont).Width <= (xPos1 - xPos)
                        strPrint = Left(strPrint, Len(strPrint) - 1)
                    Loop


            End Select
        End If

        myPPE.Graphics.DrawString(strPrint, mFont, myDrawBrush, xPos, yPos)
        PrintString = strPrint
    End Function




#End Region

End Class
