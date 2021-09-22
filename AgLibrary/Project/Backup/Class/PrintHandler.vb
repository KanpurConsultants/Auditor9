Imports System.Drawing.Printing

Public Class PrintHandler

    Dim AgL As AgLibrary.ClsMain

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
    Private myRowsProcessedSoFar As Integer = -1

    'Number of DataSet rows to print.
    Private myDataSetRows As Integer = 0

    'Number of DataSet columns to print.
    Private myNumberOfColumns As Integer = 0

    'Index of the table within the DataSet to print.
    Private myTableIndex As Integer = 0

    'Input DataSet whose rows will be printed. If the dataset has a
    'filter applied, the remaining rows are stored in an array. The 
    'array's contents are then printed. 
    Private myDataSetToPrint As DataSet
    Private myFilteredRows As DataRow()

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
    'Private myDrawFont As New Font("Courier New", 8)
    Private myDrawFont As New Font("Courier New", 8)

    'Font to use to print the report title.

    Private myDrawFontBold As New Font("Microsoft Sans Serif", 10, FontStyle.Bold)

    'Code By Satyam on 11/03/2011
    Private myDrawFontRegular As New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
    Private myDrawFontBoldLarge As New Font("Microsoft Sans Serif", 14, FontStyle.Bold)
    'End Code By Satyam on 11/03/2011

    'Flag indicating if the PageSetupDialog was shown.
    Private myPageSetUp As Boolean = False


    Private myPPE As PrintPageEventArgs = Nothing
    'PrintDocument to process. Declared WithEvents so
    'Windows can fire its PrintPage event when its
    'Print method is invoked.
    Private WithEvents myDocumentToPrint As PrintDocument





    Dim aLeftMargin As Single
    Dim aTextHeight As Single
    Dim aXPos As Single
    Dim aYPos As Single
    Dim aColIndex As Integer = 0
    Dim aRowIndex As Integer
    Dim aLinesFilled As Integer
    Dim aLinesPerPage As Integer = 0
    Dim aCharactersFitted As Integer
    Dim aLinesPrintedThisPage As Integer = 0
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
    Dim mMaxColWidth(aColsToPrint) As Double
    Dim mTotalColWidth As Double = 0
    Dim mMaxColWidthStr(aColsToPrint) As String
    Dim mIsInFooter(aColsToPrint) As Boolean
    Dim mFooterAgFunction(aColsToPrint) As String
    Dim mIsGrpField(aColsToPrint) As Boolean
    Dim mSpaceBetween2PrintedColumns As Double = 0
    Dim mGroupHeaderField As String
    Dim mGroupHeaderFieldIndex As String = -1



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


    Public Structure StructFooter
        Dim FieldName As String
        Dim AggregateFunction As String


        Sub StructFooter()
            FieldName = ""
            AggregateFunction = ""
        End Sub
    End Structure

    Public Structure StructGroupHeader
        Dim FieldName As String
        Dim FieldIndex As Integer

        Sub StructGroupHeader()
            FieldName = ""
            FieldIndex = -1
        End Sub
    End Structure


    Public Structure StructColumnPrintYn
        Dim FieldName As String
        Dim PrintYn As Boolean


        Sub StructColumnPrintYn()
            FieldName = ""
            PrintYn = True
        End Sub
    End Structure



    Dim mAgGroupBy() As StructGroupBy
    Dim mAgFooter() As StructFooter
    Dim mAgGroupHeader() As StructGroupHeader
    Dim mAgColumnPrintYn() As StructColumnPrintYn

#End Region

#Region " Constructor Code "

    Public Sub New(ByVal AgLibVar As ClsMain)
        myDocumentToPrint = New PrintDocument

        AgL = AgLibVar
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

    Public WriteOnly Property AgGroupBy() As StructGroupBy()
        Set(ByVal value() As StructGroupBy)
            mAgGroupBy = value
        End Set
    End Property

    Public WriteOnly Property AgFooter() As StructFooter()
        Set(ByVal value() As StructFooter)
            mAgFooter = value
        End Set
    End Property

    Public WriteOnly Property AgGroupHeader() As StructGroupHeader()
        Set(ByVal value() As StructGroupHeader)
            mAgGroupHeader = value
        End Set
    End Property


    Public WriteOnly Property AgColumnPrintYn() As StructColumnPrintYn()
        Set(ByVal value() As StructColumnPrintYn)
            mAgColumnPrintYn = value
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
            '
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
            If myDataSetToPrint Is Nothing Then Exit Sub
            If myTableIndex > myDataSetToPrint.Tables.Count Then Exit Sub
            If myDataSetToPrint.Tables(myTableIndex).Rows.Count = 0 Then Exit Sub

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

        On Error GoTo ErrorHandler        
        Dim aValue As String
        aLeftMargin = ev.MarginBounds.Left
        aTextHeight = myDrawFont.GetHeight(ev.Graphics)
        aXPos = aLeftMargin
        aYPos = ev.MarginBounds.Top + 30
        aLinesPrintedSoFar = myLinesPrintedSoFar
        aLinesPrintedThisPage = 0
        '
        ' Set the number of dataset columns to print. Use the
        ' "NumberOfColumns" property value if set otherwise 
        ' print all columns.
        '
        myPPE = ev



        With myDataSetToPrint.Tables(myTableIndex).Columns
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




        '
        ' Calculate the number of lines per page.
        '
        aLinesPerPage = Int(ev.MarginBounds.Height / aTextHeight)
        aLayoutSize.Height = aTextHeight
        aNewStringFormat.FormatFlags = StringFormatFlags.NoWrap
        myPageNo += 1


        '
        ' Create a header line.
        '
        If myReportTitle = "" Then
            myReportTitle = myDataSetToPrint.Tables(myTableIndex).TableName
        End If
        ''STart

        'Code Change By Satyam on 11/02/2011
        aPrintStr = AgL.PubCompName
        PrintString(aPrintStr, " ", myAlignment.Center, aXPos, aYPos - 52, , , myDrawFontBoldLarge)

        aPrintStr = AgL.PubCompAdd1
        PrintString(aPrintStr, " ", myAlignment.Center, aXPos, aYPos - 36, , , myDrawFontRegular)

        aPrintStr = AgL.PubCompAdd2
        PrintString(aPrintStr, " ", myAlignment.Center, aXPos, aYPos - 24, , , myDrawFontRegular)

        aPrintStr = AgL.PubCompCity
        PrintString(aPrintStr, " ", myAlignment.Center, aXPos, aYPos - 12, , , myDrawFontRegular)

        aPrintStr = myReportTitle
        PrintString(aPrintStr, " ", myAlignment.Center, aXPos, aYPos, , , myDrawFontBold)
        'End Code Change By Satyam on 11/02/2011
        aPrintStr = "Date : " & Format(Today, DATE_FORMAT_MDY)



        PrintString(aPrintStr, " ", myAlignment.Left, aXPos, aYPos, , , myDrawFontBold)
        aPrintStr = "Page : " & myPageNo.ToString

        PrintString(aPrintStr, " ", myAlignment.Right, aXPos, aYPos, , , myDrawFontBold)

        aYPos += 2 * aTextHeight
        aLinesPrintedThisPage += 2

        PrintString("_", "_", myAlignment.Right, aXPos, aYPos)
        aYPos += 2 * aTextHeight : aLinesPrintedThisPage += 2


        '
        ' Print the column names.
        '

        If myPageNo = 1 Then
            ReDim mMaxColWidth(aColsToPrint)
            ReDim mMaxColWidthStr(aColsToPrint)
            ReDim mIsInFooter(aColsToPrint)
            ReDim mFooterAgFunction(aColsToPrint)
            ReDim mIsGrpField(aColsToPrint)

            Dim iCol As Integer, iFooter As Integer, iGrp As Integer
            NumberOfRowsToScan = System.Math.Min(NumberOfRowsToScan, myDataSetToPrint.Tables(myTableIndex).Rows.Count)

            For iCol = 0 To aColsToPrint
                mColumnDataType(iCol) = myDataSetToPrint.Tables(myTableIndex).Columns(iCol).DataType
                mMaxColWidthStr(iCol) = myDataSetToPrint.Tables(myTableIndex).Columns(iCol).Caption
                mMaxColWidth(iCol) = ev.Graphics.MeasureString(myDataSetToPrint.Tables(myTableIndex).Columns(iCol).Caption, myDrawFont).Width

                Dim iRow As Integer

                For iRow = 0 To NumberOfRowsToScan - 1
                    If Not IsDBNull(myDataSetToPrint.Tables(myTableIndex).Rows(iRow)(iCol)) Then
                        mMaxColWidthStr(iCol) = IIf(Len(mMaxColWidthStr(iCol)) > Len(CStr(myDataSetToPrint.Tables(myTableIndex).Rows(iRow)(iCol))), mMaxColWidthStr(iCol), myDataSetToPrint.Tables(myTableIndex).Rows(iRow)(iCol))
                        mMaxColWidth(iCol) = System.Math.Max(mMaxColWidth(iCol), ev.Graphics.MeasureString(myDataSetToPrint.Tables(myTableIndex).Rows(iRow)(iCol), myDrawFont).Width)
                    End If
                Next

                mTotalColWidth += mMaxColWidth(iCol)

                mIsInFooter(iCol) = False
                If mAgFooter IsNot Nothing Then
                    For iFooter = 0 To UBound(mAgFooter) - 1
                        If AgL.StrCmp("[" & myDataSetToPrint.Tables(myTableIndex).Columns(iCol).Caption & "]", mAgFooter(iFooter).FieldName) Then
                            mIsInFooter(iCol) = True
                            mFooterAgFunction(iCol) = mAgFooter(iFooter).AggregateFunction

                        End If
                    Next
                End If

                mIsGrpField(iCol) = False
                If mAgGroupBy IsNot Nothing Then
                    For iGrp = 0 To UBound(mAgGroupBy) - 1
                        If AgL.StrCmp("[" & myDataSetToPrint.Tables(myTableIndex).Columns(iCol).Caption & "]", mAgGroupBy(iGrp).FieldName) Then
                            mIsGrpField(iCol) = True
                            mAgGroupBy(iGrp).FieldIndex = iCol
                            If mAgGroupBy(iGrp).GroupHeader = True Then
                                mGroupHeaderFieldIndex = iCol
                            End If
                        End If
                    Next
                End If

                If mAgGroupHeader IsNot Nothing Then
                    For iGrp = 0 To UBound(mAgGroupHeader) - 1
                        If AgL.StrCmp("[" & myDataSetToPrint.Tables(myTableIndex).Columns(iCol).Caption & "]", mAgGroupHeader(iGrp).FieldName) Then
                            mAgGroupHeader(iGrp).FieldIndex = iCol
                        End If
                    Next
                End If


            Next


            mSpaceBetween2PrintedColumns = ((ev.MarginBounds.Right - ev.MarginBounds.Left) - mTotalColWidth) / IIf(aColsToPrint > 1, (aColsToPrint + 1), 2)
            If mSpaceBetween2PrintedColumns <= 0 Then mSpaceBetween2PrintedColumns = 10

        End If



        myPageWidthExceeded = False
        aFirstColToPrint = myLastColPrinted + 1

        For aColIndex = aFirstColToPrint To aColsToPrint
            aCol = myDataSetToPrint.Tables(myTableIndex).Columns(aColIndex)

            aPrintIt = (mMaxColWidth(aColIndex) > 0)
            aLayoutSize.Width = mMaxColWidth(aColIndex)

            aStringSize = ev.Graphics.MeasureString(mMaxColWidthStr(aColIndex), myDrawFont, _
                          aLayoutSize, aNewStringFormat, aCharactersFitted, aLinesFilled)


            If aPrintIt Then
                '
                ' See if the column can fit on the page.
                '
                If (aXPos + aLayoutSize.Width) <= ev.MarginBounds.Right Then
                    PrintString(Left(aCol.Caption, aCharactersFitted), " ", myAlignment.Left, aXPos, aYPos)
                    aXPos += aLayoutSize.Width + mSpaceBetween2PrintedColumns
                    myLastColPrinted = aColIndex
                Else
                    myPageWidthExceeded = True
                    Err.Raise(1, , "Columns Printed Exceeding Page Width " & vbCrLf & "Change Page Settings Or Reduce Some Columns")
                    Exit For
                End If
            Else
                myLastColPrinted = aColIndex
            End If
        Next

        aLinesPrintedThisPage += 1
        aYPos += aTextHeight : aLinesPrintedThisPage += 1

        aXPos = aLeftMargin
        PrintString("_", "_", myAlignment.Right, aXPos, aYPos)
        aYPos += aTextHeight : aLinesPrintedThisPage += 1


        For aRowIndex = myLinesPrintedSoFar To myDataSetRows
            If myDatasetFiltered Then
                aRow = myFilteredRows(aRowIndex)
            Else
                aRow = myDataSetToPrint.Tables(myTableIndex).Rows.Item(aRowIndex)
            End If

            myRowsProcessedSoFar += 1


            If aRow.RowState <> DataRowState.Deleted Then

                aXPos = aLeftMargin
                aYPos += aTextHeight


                Dim mValue As String
                Dim iColIndex As Integer
                Dim mStrPrint As String




                Dim iGH As Integer
                '######### PRINT GROUP FOOTER  / HEADER ###############
                If mAgGroupBy IsNot Nothing Then
                    For iColIndex = UBound(mAgGroupBy) - 1 To 0 Step -1
                        mValue = aRow(mAgGroupBy(iColIndex).FieldIndex).ToString
                        If Not AgL.StrCmp(mLastValues(mAgGroupBy(iColIndex).FieldIndex), mValue) And myRowsProcessedSoFar <> 0 Then
                            Print_Footer(iColIndex)
                        End If
                        If mAgGroupBy(iColIndex).GroupHeader = True Then
                            If Not AgL.StrCmp(mLastValues(mAgGroupBy(iColIndex).FieldIndex), mValue) Then
                                If mAgGroupHeader IsNot Nothing Then
                                    For iGH = 0 To UBound(mAgGroupHeader) - 1
                                        mStrPrint = aRow(mAgGroupHeader(iGH).FieldIndex).ToString
                                        PrintString(mStrPrint, " ", myAlignment.Left, aXPos, aYPos, , mMaxColWidth(mAgGroupHeader(iGH).FieldIndex))
                                        aYPos += aTextHeight : aLinesPrintedThisPage += 1
                                    Next
                                End If
                            End If
                        End If
                    Next
                End If
                '############################################

                For aColIndex = aFirstColToPrint To myLastColPrinted
                    aValue = aRow(aColIndex).ToString

                    '                    aCol = myDataSetToPrint.Tables(myTableIndex).Columns(aColIndex)

                    aPrintIt = (mMaxColWidth(aColIndex) > 0)
                    aLayoutSize.Width = mMaxColWidth(aColIndex)

                    aStringSize = ev.Graphics.MeasureString(aValue, myDrawFont, aLayoutSize, aNewStringFormat, aCharactersFitted, aLinesFilled)

                    '
                    ' Trim the column's value to the specified width.
                    '
                    If aPrintIt Then
                        If aValue.Length > aCharactersFitted Then
                            aValue = Left(aValue, aCharactersFitted)
                        End If

                        '
                        ' Apply additional formatting based on the column's data type.
                        '


                        If (aXPos + aLayoutSize.Width) <= ev.MarginBounds.Right Then
                            Select Case aRow(aColIndex).GetType.Name.ToUpper
                                Case "STRING"
                                    PrintString(aValue, " ", myAlignment.Left, aXPos, aYPos)
                                Case "INT32"
                                    PrintString(aValue, " ", myAlignment.Right, aXPos, aYPos, , aLayoutSize.Width)
                                Case "DECIMAL"
                                    PrintString(aValue, " ", myAlignment.Right, aXPos, aYPos, , aLayoutSize.Width)
                                Case Else
                                    PrintString(aValue, " ", myAlignment.Left, aXPos, aYPos)
                            End Select

                            If mIsInFooter(aColIndex) = True Then
                                Select Case UCase(mFooterAgFunction(aColIndex))
                                    Case "SUM"
                                        mSubFooterValue(aColIndex) = Val(mSubFooterValue(aColIndex)) + Val(aValue)
                                        mFooterValue(aColIndex) = Val(mFooterValue(aColIndex)) + Val(aValue)
                                    Case "MAX"
                                        Select Case aRow(aColIndex).GetType.Name.ToUpper
                                            Case "INT32", "DECIMAL"
                                                mSubFooterValue(aColIndex) = IIf(Val(aValue) > Val(mSubFooterValue(aColIndex)), Val(aValue), Val(mSubFooterValue(aColIndex)))
                                                mFooterValue(aColIndex) = IIf(Val(aValue) > Val(mFooterValue(aColIndex)), Val(aValue), Val(mFooterValue(aColIndex)))
                                            Case Else
                                                mSubFooterValue(aColIndex) = IIf(aValue > mSubFooterValue(aColIndex), aValue, mSubFooterValue(aColIndex))
                                                mFooterValue(aColIndex) = IIf(aValue > mFooterValue(aColIndex), aValue, mFooterValue(aColIndex))
                                        End Select
                                    Case "MIN"
                                        Select Case aRow(aColIndex).GetType.Name.ToUpper
                                            Case "INT32", "DECIMAL"
                                                mSubFooterValue(aColIndex) = IIf(Val(aValue) < Val(mSubFooterValue(aColIndex)), Val(aValue), Val(mSubFooterValue(aColIndex)))
                                                mFooterValue(aColIndex) = IIf(Val(aValue) < Val(mFooterValue(aColIndex)), Val(aValue), Val(mFooterValue(aColIndex)))
                                            Case Else
                                                mSubFooterValue(aColIndex) = IIf(aValue < mSubFooterValue(aColIndex), aValue, mSubFooterValue(aColIndex))
                                                mFooterValue(aColIndex) = IIf(aValue < mFooterValue(aColIndex), aValue, mFooterValue(aColIndex))
                                        End Select
                                End Select
                            End If

                            mLastValues(aColIndex) = aValue
                            aXPos += aLayoutSize.Width + +mSpaceBetween2PrintedColumns
                        Else
                            Exit For
                        End If

                    End If
                Next


                aLinesPrintedThisPage += 1
                myLinesPrintedSoFar += 1
                If aLinesPrintedThisPage > aLinesPerPage Then Exit For
                'If myLinesPrintedSoFar > myDataSetRows Then Exit For
            End If



            If myRowsProcessedSoFar >= myDataSetRows Then
                If mAgGroupBy IsNot Nothing Then
                    Print_Footer(0)
                End If

                Print_ReportFooter()

                aLinesPrintedThisPage += 1
                aYPos += aTextHeight : aLinesPrintedThisPage += 1
                Exit For
            End If

        Next


        '
        ' If the number of columns to print exceeds the
        ' page with, reset the number of lines printed so
        ' far for the remainder of the columns print the
        ' data for the same set of rows.
        '
        If myPageWidthExceeded Then
            myLinesPrintedSoFar = aLinesPrintedSoFar
        End If












        '
        ' If all columns have been printed, reset
        ' the starting column to print.
        '
        If myLastColPrinted >= aColsToPrint Then
            myLastColPrinted = -1
        End If


        '
        ' If there are more lines to be printed, print another page.
        ' Setting HasMorePages True causes this procedure to be re-invoked.
        '
        If myLinesPrintedSoFar <= myDataSetRows And myRowsProcessedSoFar < myDataSetRows Then
            ev.HasMorePages = True
        Else
            ev.HasMorePages = False
            myPageNo = 0
            myLinesPrintedSoFar = 0
            myRowsProcessedSoFar = -1
            myLastColPrinted = -1
        End If

        aCol = Nothing
        aRow = Nothing
        aHeaderObj = Nothing
        myPPE = Nothing
        Exit Sub
ErrorHandler:
        MsgBox(Err.Description)
        Throw New Exception("Error formatting report output.", Err.GetException)
    End Sub

    Sub Print_ReportFooter()
        aColIndex = 0
        aXPos = aLeftMargin
        aYPos += aTextHeight : aLinesPrintedThisPage += 1
        PrintString("_", "_", myAlignment.Right, aXPos, aYPos)
        aYPos += aTextHeight : aLinesPrintedThisPage += 1

        For aColIndex = aFirstColToPrint To aColsToPrint
            aCol = myDataSetToPrint.Tables(myTableIndex).Columns(aColIndex)

            aPrintIt = (mMaxColWidth(aColIndex) > 0)
            aLayoutSize.Width = mMaxColWidth(aColIndex)

            aStringSize = myPPE.Graphics.MeasureString(mMaxColWidthStr(aColIndex), myDrawFont, _
                          aLayoutSize, aNewStringFormat, aCharactersFitted, aLinesFilled)


            '
            ' Conditionally print the column header. If the Caption 
            ' property was not set it defaults to the actual column name.
            '
            If aPrintIt Then
                '
                ' See if the column can fit on the page.
                '
                If (aXPos + aLayoutSize.Width) <= myPPE.MarginBounds.Right Then
                    Select Case myDataSetToPrint.Tables(0).Columns(aColIndex).DataType.Name.ToUpper
                        Case "SYSTEM.STRING"
                            PrintString(Left(mFooterValue(aColIndex), aCharactersFitted), " ", myAlignment.Left, aXPos, aYPos)
                        Case "SYSTEM.INT32"
                            PrintString(Left(mFooterValue(aColIndex), aCharactersFitted), " ", myAlignment.Right, aXPos, aYPos)
                        Case "SYSTEM.DECIMAL"
                            PrintString(Left(mFooterValue(aColIndex), aCharactersFitted), " ", myAlignment.Right, aXPos, aYPos)
                        Case Else
                            PrintString(Left(mFooterValue(aColIndex), aCharactersFitted), " ", myAlignment.Left, aXPos, aYPos)
                    End Select


                    aXPos += aLayoutSize.Width + mSpaceBetween2PrintedColumns

                    myLastColPrinted = aColIndex
                Else
                    '
                    ' Column exceeds page with.
                    '
                    myPageWidthExceeded = True
                    Exit For
                End If
            Else
                myLastColPrinted = aColIndex
            End If
        Next

    End Sub

    Sub Print_Footer(ByVal GrpIndex As Integer)
        Dim mColIndex As Int16
        Dim mXPos As Single, mStrPrint$

        mColIndex = 0
        mXPos = aLeftMargin
        aYPos += aTextHeight : aLinesPrintedThisPage += 1
        PrintString("_", "_", myAlignment.Right, mXPos, aYPos)
        aYPos += aTextHeight : aLinesPrintedThisPage += 1

        For mColIndex = aFirstColToPrint To aColsToPrint
            aPrintIt = (mMaxColWidth(mColIndex) > 0)
            aLayoutSize.Width = mMaxColWidth(mColIndex)

            If mColIndex = mAgGroupBy(GrpIndex).FieldIndex Then
                mStrPrint = mLastValues(mAgGroupBy(GrpIndex).FieldIndex)
            Else

                mStrPrint = mSubFooterValue(mColIndex)
                mSubFooterValue(mColIndex) = ""
            End If

            If aPrintIt Then
                If (mXPos + aLayoutSize.Width) <= myPPE.MarginBounds.Right Then
                    Select Case mColumnDataType(mColIndex).ToString.ToUpper
                        Case "SYSTEM.STRING"
                            PrintString(Left(mStrPrint, aCharactersFitted), " ", myAlignment.Left, mXPos, aYPos, , mMaxColWidth(mColIndex))
                        Case "SYSTEM.INT32"
                            PrintString(Left(mStrPrint, aCharactersFitted), " ", myAlignment.Right, mXPos, aYPos, , mMaxColWidth(mColIndex))
                        Case "SYSTEM.DECIMAL"
                            PrintString(Left(mStrPrint, aCharactersFitted), " ", myAlignment.Right, mXPos, aYPos, , mMaxColWidth(mColIndex))
                        Case Else
                            PrintString(Left(mStrPrint, aCharactersFitted), " ", myAlignment.Left, mXPos, aYPos, , mMaxColWidth(mColIndex))
                    End Select
                    mXPos += aLayoutSize.Width + mSpaceBetween2PrintedColumns

                    myLastColPrinted = mColIndex
                Else
                    myPageWidthExceeded = True
                    Exit For
                End If
            Else
                myLastColPrinted = mColIndex
            End If
        Next

        aYPos += aTextHeight * 2 : aLinesPrintedThisPage += 2

    End Sub




#End Region

#Region " Printing Functions "

    Private Sub PrintString(ByVal strPrint As String, ByVal strStuff As String, ByVal Alignment As myAlignment, ByVal xPos As Single, ByVal yPos As Single, Optional ByVal xPos1 As Single = 0, Optional ByVal mWidth As Single = 0, Optional ByVal mFont As Font = Nothing)
        Dim strPrintWidth As Double = myPPE.Graphics.MeasureString(strPrint, myDrawFont).Width
        If xPos1 = 0 Then xPos1 = myPPE.MarginBounds.Right
        If mWidth > 0 Then xPos1 = xPos + mWidth
        If mFont Is Nothing Then mFont = myDrawFont
        If strPrint = "" Then Exit Sub
        If strStuff <> "" Then
            Select Case Alignment
                Case myAlignment.Right
                    Do Until myPPE.Graphics.MeasureString(strPrint, myDrawFont).Width >= (xPos1 - xPos)
                        strPrint = strStuff & strPrint
                    Loop
                Case myAlignment.Center
                    Do Until myPPE.Graphics.MeasureString(strPrint, myDrawFont).Width >= (xPos1 - xPos + strPrintWidth) / 2
                        strPrint = strStuff & strPrint
                        strPrint = strPrint & strStuff
                    Loop
            End Select
        End If
        myPPE.Graphics.DrawString(strPrint, myDrawFont, myDrawBrush, xPos, yPos)
    End Sub




#End Region


End Class
