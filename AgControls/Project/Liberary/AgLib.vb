
Imports System.Data.SQLite
Imports System.Xml
Imports System.IO

Public Class AgLib
    Public ECmd As SQLiteCommand
    Public EAdptr As SQLiteDataAdapter
    Public ETrans As SQLiteTransaction
    Dim ColorReadOnlyControl As Color = Color.Ivory
    Public Const StrCheckedValue As String = "þ" '"ü"
    Public Const StrUnCheckedValue As String = "¨" '""


    Public Enum TxtSearchMethod
        Simple = 0
        Comprehensive = 1
    End Enum


    Public Sub AgSetDataGridAutoWidths(ByVal DataGrid As AgDataGrid, ByVal NumberOfRowsToScan As Integer, Optional ByVal ChangeGridWidth As Boolean = True, Optional ByVal HideLastColumns As Integer = 0)

        Dim Graphics As Graphics = DataGrid.CreateGraphics()
        Dim I As Integer

        Try
            NumberOfRowsToScan = System.Math.Min(NumberOfRowsToScan, DataGrid.Rows.Count)

            Dim Width As Integer
            Dim mTotalWidth As Integer = 0
            For I = 0 To DataGrid.Columns.Count - 1
                Width = Graphics.MeasureString(DataGrid.Columns(I).HeaderText, DataGrid.Font).Width

                Dim iRow As Integer

                For iRow = 0 To NumberOfRowsToScan - 1
                    If Not IsDBNull(DataGrid.Item(I, iRow).Value) And DataGrid.Item(I, iRow).Value IsNot Nothing Then
                        Width = System.Math.Max(Width, Graphics.MeasureString(DataGrid.Item(I, iRow).Value.ToString, DataGrid.Font).Width)
                    End If
                Next
                Width = Width + 4

                DataGrid.Columns(I).Width = Width


                If I >= DataGrid.ColumnCount - HideLastColumns Then
                    DataGrid.Columns(I).Visible = False
                Else
                    mTotalWidth = mTotalWidth + Width
                End If


            Next
            If ChangeGridWidth Then
                If DataGrid.RowHeadersVisible Then
                    DataGrid.Width = mTotalWidth + 50
                Else
                    If mTotalWidth > (DataGrid.FindForm.Width + 50) Then
                        mTotalWidth = (DataGrid.FindForm.Width - 200)
                    End If
                    DataGrid.Width = mTotalWidth - DataGrid.RowHeadersWidth + 50
                End If
            End If
        Finally
            Graphics.Dispose()
        End Try
    End Sub



    'Public Function RowsFilter(ByVal TXT As Object, ByVal Dg As AgDataGrid, ByRef e As System.Windows.Forms.KeyPressEventArgs, Optional ByVal MasterHelp As Boolean = False, Optional ByVal SearchMethod As AgLib.TxtSearchMethod = TxtSearchMethod.Simple, Optional ByVal HelpColumnIndex As Integer = 1) As Integer
    '    Try
    '        Dim findStr As String, FndFldName$
    '        Dim sa As String
    '        Dim mLen%
    '        Dim IntRow As Integer
    '        Dim J As Integer
    '        sa = TXT.Text

    '        'FndFldName = Dg.Columns(1).HeaderText
    '        'If Dg.CurrentCell IsNot Nothing Then
    '        '    FndFldName = Dg.Columns(Dg.CurrentCell.ColumnIndex).HeaderText
    '        'End If

    '        FndFldName = Dg.Columns(HelpColumnIndex).HeaderText

    '        If sa.Length = 0 And Asc(e.KeyChar) = 8 Then IntRow = 0 : Dg.CurrentCell = Dg(FndFldName, IntRow) : Exit Function
    '        If TXT.Text = "(null)" Then
    '            findStr = e.KeyChar
    '        Else
    '            findStr = IIf(Asc(e.KeyChar) = Keys.Back Or Asc(e.KeyChar) = 4 Or Asc(e.KeyChar) = Keys.Up Or Asc(e.KeyChar) = Keys.Down Or Asc(e.KeyChar) = Keys.Left Or Asc(e.KeyChar) = Keys.Right Or Asc(e.KeyChar) = Keys.Up Or Asc(e.KeyChar) = 19, TXT.Text, TXT.Text + e.KeyChar)
    '        End If

    '        For J = 0 To Dg.RowCount - 1
    '            If Len(XNull(Dg.Item(FndFldName, J).Value)) < Len(findStr) Then
    '                mLen = Len(XNull(Dg.Item(FndFldName, J).Value))
    '            Else
    '                mLen = Len(findStr)
    '            End If

    '            If mLen = 2 Then
    '                mLen = 2
    '            End If

    '            If XNull(Dg.Item(FndFldName, J).Value) <> "" Then
    '                If SearchMethod = TxtSearchMethod.Comprehensive Then
    '                    If InStr(UCase(XNull(Dg.Item(FndFldName, J).Value).ToString), UCase(findStr)) > 0 Then
    '                        Dg.CurrentCell = Dg(FndFldName, J)
    '                        If Not MasterHelp Then
    '                            'e.Handled = True
    '                            'TXT.Text = TXT.Text + IIf(Asc(e.KeyChar) = Keys.Back Or Asc(e.KeyChar) = 4 Or Asc(e.KeyChar) = 19, "", e.KeyChar)
    '                            If Asc(e.KeyChar) = 4 Or Asc(e.KeyChar) = 19 Then
    '                                e.Handled = True
    '                            End If
    '                        End If
    '                        Exit Try
    '                    End If
    '                Else
    '                    If UCase(XNull(Dg.Item(FndFldName, J).Value).ToString.Substring(0, mLen)) = UCase(findStr) Then
    '                        Dg.CurrentCell = Dg(FndFldName, J)
    '                        If Not MasterHelp Then
    '                            'TXT.Text = TXT.Text + IIf(Asc(e.KeyChar) = Keys.Back Or Asc(e.KeyChar) = 4 Or Asc(e.KeyChar) = 19, "", e.KeyChar)
    '                            If Asc(e.KeyChar) = 4 Or Asc(e.KeyChar) = 19 Then
    '                                e.Handled = True
    '                            End If
    '                        End If
    '                        Exit Try
    '                    End If
    '                End If
    '            End If
    '        Next



    '        For J = 0 To Dg.RowCount - 1
    '            If Len(XNull(Dg.Item(FndFldName, J).Value)) < Len(sa) Then
    '                mLen = Len(XNull(Dg.Item(FndFldName, J).Value))
    '            Else
    '                mLen = Len(sa)
    '            End If
    '            If XNull(Dg.Item(FndFldName, J).Value) <> "" Then
    '                If UCase(XNull(Dg.Item(FndFldName, J).Value).ToString.Substring(0, mLen)) = UCase(sa) Then

    '                    Dg.CurrentCell = Dg(FndFldName, J)
    '                    'TXT.Text = TXT.Text + IIf(Asc(e.KeyChar) = Keys.Back Or Asc(e.KeyChar) = 4 Or Asc(e.KeyChar) = 19, "", e.KeyChar)
    '                    Exit Try
    '                End If
    '            End If
    '        Next

    '    Catch ex As Exception
    '        'MsgBox(ex.Message)
    '    Finally
    '        'TXT.SelectionStart = TXT.Text.Length
    '        'If Asc(e.KeyChar) <> Keys.Back And Not MasterHelp Then e.Handled = True 'e.KeyChar = ""
    '    End Try

    'End Function


    'Public Function RowsFilter(ByRef TxtSearch As TextBox, ByRef FgMain As DataGridView, ByVal ConstFilter As String, ByVal e As System.Windows.Forms.KeyPressEventArgs, Optional ByVal MasterHelp As Boolean = False, Optional ByVal SearchMethod As AgLib.TxtSearchMethod = TxtSearchMethod.Simple, Optional ByVal HelpColumnIndex As Integer = 1) As Int16
    '    Dim DvMain As DataView
    '    Try
    '        Dim StrExpr As String, StrFind As String = ""
    '        Dim StrValue As String, StrField As String
    '        Dim SrtCol As Short, TxtSearchCurLocation As Integer


    '        If Not FgMain.Rows.Count > 0 Then Exit Function
    '        DvMain = FgMain.DataSource
    '        DvMain.Sort = FgMain.Columns(1).Name

    '        If FgMain.CurrentCell IsNot Nothing Then
    '            SrtCol = FgMain.CurrentCell.ColumnIndex
    '            StrField = FgMain.Columns(FgMain.CurrentCell.ColumnIndex).Name
    '        Else
    '            SrtCol = 1
    '            StrField = FgMain.Columns(1).Name
    '        End If

    '        TxtSearchCurLocation = TxtSearch.SelectionStart



    '        StrValue = TxtSearch.Text
    '        If TxtSearch.Text = "(null)" Then
    '            StrFind = e.KeyChar
    '        Else
    '            'StrFind = IIf(Asc(e.KeyChar) = Keys.Back Or Asc(e.KeyChar) = 4 Or Asc(e.KeyChar) = 19 Or Asc(e.KeyChar) = Keys.Enter, TxtSearch.Text, TxtSearch.Text + e.KeyChar)
    '            If Asc(e.KeyChar) = Keys.Back Or Asc(e.KeyChar) = 4 Or Asc(e.KeyChar) = 19 Or Asc(e.KeyChar) = Keys.Enter Or e.KeyChar = Nothing Then
    '                StrFind = TxtSearch.Text
    '            Else
    '                StrFind = TxtSearch.Text
    '                If TxtSearch.SelectionLength > 0 Then
    '                    StrFind = Replace(StrFind, StrFind.Substring(TxtSearch.SelectionStart, TxtSearch.SelectionLength), e.KeyChar)
    '                Else
    '                    StrFind = StrFind.Insert(TxtSearchCurLocation, e.KeyChar)
    '                End If
    '            End If

    '            If Asc(e.KeyChar) = Keys.Back And StrFind.Length > 0 Then
    '                'StrFind =      Microsoft.VisualBasic.Left(StrFind, Microsoft.VisualBasic.Len(StrFind) - 1)
    '                If TxtSearch.SelectionLength > 0 Then
    '                    StrFind = Replace(StrFind, StrFind.Substring(TxtSearch.SelectionStart, TxtSearch.SelectionLength), "")
    '                Else
    '                    If TxtSearch.SelectionStart > 0 Then
    '                        StrFind = StrFind.Remove(TxtSearch.SelectionStart - 1, 1)
    '                    End If
    '                End If
    '            End If
    '        End If

    '        StrExpr = ""
    '        If StrFind <> "" Then
    '            If SearchMethod = TxtSearchMethod.Simple Then
    '                StrExpr = "[" & StrField & "] like '" & StrFind & "%' "
    '            Else
    '                StrExpr = "[" & StrField & "] like '%" & StrFind & "%' "
    '            End If
    '            If ConstFilter <> "" Then StrExpr = " And " + StrExpr
    '        End If

    '        If ConstFilter <> "" Then
    '            DvMain.RowFilter = ConstFilter + StrExpr
    '        Else
    '            DvMain.RowFilter = StrExpr
    '        End If

    '        If Not DvMain.Count >= 0 And Not MasterHelp Then
    '            If StrFind.ToString.Length > 0 Then
    '                TxtSearch.Text = FFilterRecursive(TxtSearch, DvMain, StrField, StrFind.Remove(TxtSearch.SelectionStart, 1), ConstFilter, SearchMethod)
    '            Else
    '                TxtSearch.Text = FFilterRecursive(TxtSearch, DvMain, StrField, "", ConstFilter, SearchMethod)
    '            End If
    '        Else
    '            If Asc(e.KeyChar) = Keys.Back Or Asc(e.KeyChar) = 4 Or Asc(e.KeyChar) = 19 Or Asc(e.KeyChar) = Keys.Enter Or e.KeyChar = Nothing Then
    '                TxtSearch.Text = TxtSearch.Text
    '            Else
    '                If TxtSearch.SelectionLength > 0 Then
    '                    TxtSearch.Text = Replace(TxtSearch.Text, TxtSearch.Text.Substring(TxtSearch.SelectionStart, TxtSearch.SelectionLength), e.KeyChar)
    '                    TxtSearch.SelectionStart = TxtSearchCurLocation + 1
    '                Else
    '                    TxtSearch.Text = TxtSearch.Text.Insert(TxtSearch.SelectionStart, e.KeyChar)
    '                    TxtSearch.SelectionStart = TxtSearchCurLocation + 1
    '                End If
    '            End If

    '        End If
    '        If Asc(e.KeyChar) <> Keys.Back Then e.Handled = True
    '        FgMain.CurrentCell = FgMain(SrtCol, 0)
    '    Catch ex As Exception
    '        MsgBox("System Exception : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
    '        DvMain.RowFilter = ConstFilter
    '    End Try
    'End Function

    Public Function RowsFilter(ByRef TxtSearch As TextBox, ByRef FgMain As AgControls.AgDataGrid, ByVal ConstFilter As String, ByVal e As System.Windows.Forms.KeyPressEventArgs, Optional ByVal MasterHelp As Boolean = False, Optional ByVal SearchMethod As AgLib.TxtSearchMethod = TxtSearchMethod.Simple, Optional ByVal HelpColumnIndex As Integer = 1) As Int16
        'Dim DvMain As DataView
        Try
            Dim StrExpr As String, StrFind As String = ""
            Dim StrValue As String, StrField As String
            Dim SrtCol As Short, TxtSearchCurLocation As Integer


            If Not FgMain.Rows.Count > 0 Then Exit Function
            'DvMain = FgMain.DataSource
            FgMain.DataSource.Sort = FgMain.Columns(1).Name

            If FgMain.CurrentCell IsNot Nothing Then
                SrtCol = FgMain.CurrentCell.ColumnIndex
                StrField = FgMain.Columns(FgMain.CurrentCell.ColumnIndex).Name
            Else
                SrtCol = 1
                StrField = FgMain.Columns(1).Name
            End If

            TxtSearchCurLocation = TxtSearch.SelectionStart



            StrValue = TxtSearch.Text
            If TxtSearch.Text = "(null)" Then
                'If TxtSearch.Text = "" Then
                StrFind = e.KeyChar
            Else
                'StrFind = IIf(Asc(e.KeyChar) = Keys.Back Or Asc(e.KeyChar) = 4 Or Asc(e.KeyChar) = 19 Or Asc(e.KeyChar) = Keys.Enter, TxtSearch.Text, TxtSearch.Text + e.KeyChar)
                If Asc(e.KeyChar) = Keys.Back Or Asc(e.KeyChar) = 4 Or Asc(e.KeyChar) = 19 Or Asc(e.KeyChar) = Keys.Enter Or e.KeyChar = Nothing Then
                    StrFind = TxtSearch.Text
                Else
                    StrFind = TxtSearch.Text
                    If TxtSearch.SelectionLength > 0 Then
                        StrFind = Replace(StrFind, StrFind.Substring(TxtSearch.SelectionStart, TxtSearch.SelectionLength), e.KeyChar)
                    Else
                        StrFind = StrFind.Insert(TxtSearchCurLocation, e.KeyChar)
                    End If
                End If

                If Asc(e.KeyChar) = Keys.Back And StrFind.Length > 0 Then
                    'StrFind =      Microsoft.VisualBasic.Left(StrFind, Microsoft.VisualBasic.Len(StrFind) - 1)
                    If TxtSearch.SelectionLength > 0 Then
                        StrFind = Replace(StrFind, StrFind.Substring(TxtSearch.SelectionStart, TxtSearch.SelectionLength), "")
                    Else
                        If TxtSearch.SelectionStart > 0 Then
                            StrFind = StrFind.Remove(TxtSearch.SelectionStart - 1, 1)
                        End If
                    End If
                End If
            End If

            StrExpr = ""
            If StrFind <> "" Then
                If SearchMethod = TxtSearchMethod.Simple Then
                    StrExpr = "[" & StrField & "] like '" & StrFind & "%' "
                Else
                    StrExpr = "[" & StrField & "] like '%" & StrFind & "%' "
                End If
                If ConstFilter <> "" Then StrExpr = " And " + StrExpr
            End If

            If ConstFilter <> "" Then
                FgMain.DataSource.RowFilter = ConstFilter + StrExpr
            Else
                FgMain.DataSource.RowFilter = StrExpr
            End If


            If Not FgMain.DataSource.Count > 0 And Not MasterHelp Then
                If StrFind.ToString.Length > 0 And TxtSearch.SelectionStart <> StrFind.ToString.Length Then
                    TxtSearch.Text = FFilterRecursive(TxtSearch, FgMain.DataSource, StrField, StrFind.Remove(TxtSearch.SelectionStart, 1), ConstFilter, SearchMethod)
                    If Asc(e.KeyChar) <> Keys.Back Then e.Handled = True
                Else
                    TxtSearch.Text = FFilterRecursive(TxtSearch, FgMain.DataSource, StrField, "", ConstFilter, SearchMethod)
                    If Asc(e.KeyChar) <> Keys.Back Then e.Handled = True
                End If
            Else
                If Asc(e.KeyChar) = Keys.Back Or Asc(e.KeyChar) = 4 Or Asc(e.KeyChar) = 19 Or Asc(e.KeyChar) = Keys.Enter Or e.KeyChar = Nothing Then
                    TxtSearch.Text = TxtSearch.Text
                Else
                    If TxtSearch.SelectionLength > 0 Then
                        'TxtSearch.Text = Replace(TxtSearch.Text, TxtSearch.Text.Substring(TxtSearch.SelectionStart, TxtSearch.SelectionLength), e.KeyChar)
                        TxtSearch.SelectionStart = TxtSearchCurLocation
                    Else
                        If TxtSearch.Text.Length >= TxtSearch.MaxLength And MasterHelp Then
                        Else
                            'TxtSearch.Text = TxtSearch.Text.Insert(TxtSearch.SelectionStart, e.KeyChar)
                            TxtSearch.SelectionStart = TxtSearchCurLocation
                        End If
                    End If
                End If
            End If

            'If FgMain.CurrentCell IsNot Nothing Then
            '    FgMain.CurrentCell = FgMain(SrtCol, FgMain.CurrentCell.RowIndex)
            'End If
        Catch ex As Exception
            'MsgBox("System Exception : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
            TxtSearch.Text = ""
            FgMain.DataSource.RowFilter = ConstFilter
        End Try
    End Function


    '========================================================
    'This Function Is For Filtering(Searching) Row Recursivly
    'And Returning Searched Text
    '========================================================
    Private Function FFilterRecursive(ByVal TxtSearch As TextBox, ByRef DvMain As DataView, ByVal StrField As String, ByVal StrFind As String, ByVal ConstFilter As String, Optional ByVal SearchMethod As AgLib.TxtSearchMethod = TxtSearchMethod.Simple) As String
        Dim StrExpr As String = ""
        Try
            If StrFind <> "" Then
                If SearchMethod = TxtSearchMethod.Simple Then
                    StrExpr = "[" & StrField & "] like '" & StrFind & "%' "
                Else
                    StrExpr = "[" & StrField & "] like '%" & StrFind & "%' "
                End If
                If ConstFilter <> "" Then StrExpr = " And " + StrExpr
            End If

            If ConstFilter <> "" Then
                DvMain.RowFilter = ConstFilter + StrExpr
            Else
                DvMain.RowFilter = StrExpr
            End If

            If Not DvMain.Count > 0 Then
                'StrFind = FFilterRecursive(DvMain, StrField, Microsoft.VisualBasic.Left(StrFind, Microsoft.VisualBasic.Len(StrFind) - 1), ConstFilter)
                StrFind = FFilterRecursive(TxtSearch, DvMain, StrField, StrFind.Remove(TxtSearch.SelectionStart, 1), ConstFilter)

            End If
        Catch ex As Exception
            'MsgBox("System Exception : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, StrMsgTitle)
            TxtSearch.Text = ""
            DvMain.RowFilter = ConstFilter
        End Try
        Return StrFind
    End Function


    Public Sub AddAgDataListColumn(ByVal Dg1 As DataGridView, ByVal ListItem As String, ByVal columnName As String, ByVal ColWidth As Integer, Optional ByVal DatabaseValue As String = "", Optional ByVal columnHeaderTxt As String = "", Optional ByVal columnVisible As Boolean = True, Optional ByVal isReadOnly As Boolean = False, Optional ByVal mSortMode As DataGridViewColumnSortMode = DataGridViewColumnSortMode.NotSortable)
        Dim column As DataGridViewColumn
        column = New AgComboColumn()
        '' Populate the drop-down list with the enumeration values.
        CType(column, DataGridViewComboBoxColumn).Name = columnName
        CType(column, DataGridViewComboBoxColumn).Width = ColWidth
        CType(column, DataGridViewComboBoxColumn).HeaderText = IIf(columnHeaderTxt = "", columnName, columnHeaderTxt)
        CType(column, DataGridViewComboBoxColumn).DisplayStyleForCurrentCellOnly = True
        IniAgGridList(column, ListItem, DatabaseValue)
        column.ReadOnly = isReadOnly
        CType(column, AgComboColumn).AgReadOnly = isReadOnly
        Dg1.Columns.Add(column)
        column.Visible = columnVisible
        column.SortMode = mSortMode
        If isReadOnly Then column.DefaultCellStyle.BackColor = DirectCast(Dg1, AgControls.AgDataGrid).AgReadOnlyColumnColor
    End Sub

    Public Sub AddAgListColumn(ByVal Dg1 As DataGridView, ByVal ListItem As String, ByVal columnName As String, ByVal ColWidth As Integer, Optional ByVal DatabaseValue As String = "", Optional ByVal columnHeaderTxt As String = "", Optional ByVal columnVisible As Boolean = True, Optional ByVal isReadOnly As Boolean = False, Optional ByVal mSortMode As DataGridViewColumnSortMode = DataGridViewColumnSortMode.NotSortable)
        Dim column As DataGridViewColumn
        column = New AgTextColumn
        '' Populate the drop-down list with the enumeration values.
        column.Name = columnName
        column.Width = ColWidth
        column.HeaderText = IIf(columnHeaderTxt = "", columnName, columnHeaderTxt)
        'column.DisplayStyleForCurrentCellOnly = True

        IniAgGridList(column, ListItem, DatabaseValue)
        column.ReadOnly = isReadOnly
        CType(column, AgTextColumn).AgReadOnly = isReadOnly
        Dg1.Columns.Add(column)
        column.Visible = columnVisible
        column.SortMode = mSortMode
        If isReadOnly Then column.DefaultCellStyle.BackColor = DirectCast(Dg1, AgControls.AgDataGrid).AgReadOnlyColumnColor
    End Sub


    Public Sub AddAgCheckColumn(ByVal DGL As System.Windows.Forms.DataGridView,
                                    ByVal ColumnName As String,
                                    ByVal ColWidth As Integer,
                                    Optional ByVal ColumnHeaderTxt As String = "",
                                    Optional ByVal ColumnVisible As Boolean = True,
                                    Optional ByVal mSortMode As System.Windows.Forms.DataGridViewColumnSortMode = DataGridViewColumnSortMode.NotSortable)

        AddAgTextColumn(DGL, ColumnName, ColWidth, 0, ColumnHeaderTxt, ColumnVisible, True, False, False, mSortMode)
        DGL.Columns(ColumnName).DefaultCellStyle.Font = New System.Drawing.Font("Wingdings", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        DGL.Columns(ColumnName).DefaultCellStyle.ForeColor = Color.Black
        DGL.Columns(ColumnName).DefaultCellStyle.BackColor = Color.White
        DGL.Columns(ColumnName).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    End Sub

    Public Sub ProcSetCheckColumnCellValue(ByVal DGL As System.Windows.Forms.DataGridView, ByVal ColumnIndex As Integer)
        Dim I As Integer = 0
        For I = 0 To DGL.SelectedCells.Count
            If DGL.SelectedCells.Item(I).ColumnIndex = ColumnIndex Then
                If DGL.SelectedCells.Item(I).Value Is Nothing Then DGL.SelectedCells.Item(I).Value = StrUnCheckedValue

                If DGL.SelectedCells.Item(I).Value.ToString.Trim = "" _
                    Or DGL.SelectedCells.Item(I).Value.ToString.Trim = StrUnCheckedValue Then

                    DGL.SelectedCells.Item(I).Value = StrCheckedValue
                Else
                    DGL.SelectedCells.Item(I).Value = StrUnCheckedValue
                End If
            End If
        Next
    End Sub



    Public Sub AddAgNumberColumn(ByVal Dg1 As DataGridView, ByVal ColumnName As String, ByVal ColWidth As Integer, ByVal LeftPlaces As Integer, ByVal RightPlaces As Integer, ByVal AllowNegetive As Boolean, Optional ByVal columnHeaderTxt As String = "", Optional ByVal columnVisible As Boolean = True, Optional ByVal isReadOnly As Boolean = False, Optional ByVal isRightAlign As Boolean = True, Optional ByVal isMandatory As Boolean = False, Optional ByVal mSortMode As DataGridViewColumnSortMode = DataGridViewColumnSortMode.NotSortable)
        Dim column As AgControls.AgTextColumn
        column = New AgControls.AgTextColumn

        '' Populate the drop-down list with the enumeration values.
        column.Name = ColumnName
        column.HeaderText = IIf(columnHeaderTxt = "", ColumnName, columnHeaderTxt)
        column.Width = ColWidth
        column.AgValueType = AgControls.AgTextColumn.TxtValueType.Number_Value
        column.AgNumberLeftPlaces = LeftPlaces
        column.AgNumberRightPlaces = RightPlaces
        column.AgNumberNegetiveAllow = AllowNegetive
        column.AgMandatory = isMandatory

        If isRightAlign = True Then
            CType(column, DataGridViewTextBoxColumn).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            CType(column, DataGridViewTextBoxColumn).CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleRight
        End If
        CType(column, DataGridViewTextBoxColumn).ReadOnly = isReadOnly
        column.AgReadOnly = isReadOnly
        Dg1.Columns.Add(column)
        column.Visible = columnVisible
        column.SortMode = mSortMode
        If isReadOnly Then column.DefaultCellStyle.BackColor = DirectCast(Dg1, AgControls.AgDataGrid).AgReadOnlyColumnColor
    End Sub


    Public Sub AddAgTextColumn(ByVal Dg1 As DataGridView, ByVal ColumnName As String, ByVal ColWidth As Integer, ByVal mMaxInputLength As Integer, Optional ByVal columnHeaderTxt As String = "", Optional ByVal columnVisible As Boolean = True, Optional ByVal isReadOnly As Boolean = False, Optional ByVal isRightAlign As Boolean = False, Optional ByVal isMandatory As Boolean = False, Optional ByVal mSortMode As DataGridViewColumnSortMode = DataGridViewColumnSortMode.NotSortable)
        Dim column As AgControls.AgTextColumn
        column = New AgControls.AgTextColumn

        '' Populate the drop-down list with the enumeration values.
        column.Name = ColumnName
        column.HeaderText = IIf(columnHeaderTxt = "", ColumnName, columnHeaderTxt)
        column.MaxInputLength = mMaxInputLength
        column.Width = ColWidth
        column.AgValueType = AgControls.AgTextColumn.TxtValueType.Text_Value
        column.AgMandatory = isMandatory
        If isRightAlign = True Then
            CType(column, DataGridViewTextBoxColumn).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            CType(column, DataGridViewTextBoxColumn).CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleRight
        End If
        CType(column, DataGridViewTextBoxColumn).ReadOnly = isReadOnly
        column.AgReadOnly = isReadOnly

        Dg1.Columns.Add(column)
        column.Visible = columnVisible
        column.SortMode = mSortMode
        If isReadOnly Then column.DefaultCellStyle.BackColor = DirectCast(Dg1, AgControls.AgDataGrid).AgReadOnlyColumnColor
    End Sub

    Public Sub AddAgDateColumn(ByVal Dg1 As DataGridView, ByVal ColumnName As String, ByVal ColWidth As Integer, Optional ByVal columnHeaderTxt As String = "", Optional ByVal columnVisible As Boolean = True, Optional ByVal isReadOnly As Boolean = False, Optional ByVal isRightAlign As Boolean = False, Optional ByVal IsMandatory As Boolean = False, Optional ByVal mSortMode As DataGridViewColumnSortMode = DataGridViewColumnSortMode.NotSortable)
        Dim column As AgControls.AgTextColumn
        column = New AgControls.AgTextColumn

        '' Populate the drop-down list with the enumeration values.
        column.Name = ColumnName
        column.HeaderText = IIf(columnHeaderTxt = "", ColumnName, columnHeaderTxt)
        column.Width = ColWidth
        column.AgValueType = AgControls.AgTextColumn.TxtValueType.Date_Value
        column.AgMandatory = IsMandatory
        If isRightAlign = True Then
            CType(column, DataGridViewTextBoxColumn).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            CType(column, DataGridViewTextBoxColumn).CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleRight
        End If
        CType(column, DataGridViewTextBoxColumn).ReadOnly = isReadOnly
        column.AgReadOnly = isReadOnly
        Dg1.Columns.Add(column)
        column.Visible = columnVisible
        column.SortMode = mSortMode
        If isReadOnly Then column.DefaultCellStyle.BackColor = DirectCast(Dg1, AgControls.AgDataGrid).AgReadOnlyColumnColor
    End Sub

    Public Sub AddAgYesNoColumn(ByVal Dg1 As DataGridView, ByVal ColumnName As String, ByVal ColWidth As Integer, Optional ByVal columnHeaderTxt As String = "", Optional ByVal columnVisible As Boolean = True, Optional ByVal isReadOnly As Boolean = False, Optional ByVal isRightAlign As Boolean = False, Optional ByVal mSortMode As DataGridViewColumnSortMode = DataGridViewColumnSortMode.NotSortable)
        Dim column As AgControls.AgTextColumn
        column = New AgControls.AgTextColumn

        '' Populate the drop-down list with the enumeration values.
        column.Name = ColumnName
        column.HeaderText = IIf(columnHeaderTxt = "", ColumnName, columnHeaderTxt)
        column.Width = ColWidth
        column.AgValueType = AgControls.AgTextColumn.TxtValueType.YesNo_Value
        If isRightAlign = True Then
            CType(column, DataGridViewTextBoxColumn).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            CType(column, DataGridViewTextBoxColumn).CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleRight
        End If
        CType(column, DataGridViewTextBoxColumn).ReadOnly = isReadOnly
        column.AgReadOnly = isReadOnly
        Dg1.Columns.Add(column)
        column.Visible = columnVisible
        column.SortMode = mSortMode
        If isReadOnly Then column.DefaultCellStyle.BackColor = DirectCast(Dg1, AgControls.AgDataGrid).AgReadOnlyColumnColor
    End Sub


    Public Sub AddAgButtonColumn(ByVal Dg1 As DataGridView, ByVal ColumnName As String, ByVal ColWidth As Integer, Optional ByVal ColumnHeaderTxt As String = "", Optional ByVal columnVisible As Boolean = True, Optional ByVal isReadOnly As Boolean = False, Optional ByVal mSortMode As DataGridViewColumnSortMode = DataGridViewColumnSortMode.NotSortable, _
                                Optional ByVal mFlatStyle As FlatStyle = FlatStyle.Popup, _
                                Optional ByVal mAlignment As DataGridViewContentAlignment = DataGridViewContentAlignment.MiddleCenter, _
                                Optional ByVal mFontName As String = "Wingdings", _
                                Optional ByVal mFontSize As Single = 9, _
                                Optional ByVal mColumnText As String = "", _
                                Optional ByVal mFontStyle As FontStyle = FontStyle.Regular)
        Dim column As AgControls.AgButtonColumn
        column = New AgControls.AgButtonColumn

        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle


        column.Name = ColumnName
        column.HeaderText = IIf(ColumnHeaderTxt = "", ColumnName, ColumnHeaderTxt)
        column.Width = ColWidth
        CType(column, DataGridViewButtonColumn).ReadOnly = isReadOnly
        column.AgReadOnly = isReadOnly


        Dg1.Columns.Add(column)
        column.Visible = columnVisible
        column.SortMode = mSortMode

        DataGridViewCellStyle1.Alignment = mAlignment
        DataGridViewCellStyle1.Font = New System.Drawing.Font(mFontName, mFontSize, mFontStyle, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.Blue
        column.DefaultCellStyle = DataGridViewCellStyle1
        column.DefaultCellStyle.BackColor = Color.WhiteSmoke
        column.FlatStyle = mFlatStyle
        column.Text = mColumnText
        column.UseColumnTextForButtonValue = True
        If isReadOnly Then column.DefaultCellStyle.BackColor = DirectCast(Dg1, AgControls.AgDataGrid).AgReadOnlyColumnColor
    End Sub

    Public Sub AddAgImageColumn(ByVal Dg1 As DataGridView, ByVal ColumnName As String, ByVal ColWidth As Integer, Optional ByVal ColumnHeaderTxt As String = "", Optional ByVal columnVisible As Boolean = True, Optional ByVal isReadOnly As Boolean = False, Optional ByVal isRightAlign As Boolean = False, Optional ByVal mSortMode As DataGridViewColumnSortMode = DataGridViewColumnSortMode.NotSortable)
        Dim column As AgControls.AgImageColumn
        column = New AgControls.AgImageColumn

        column.Name = ColumnName
        column.HeaderText = IIf(ColumnHeaderTxt = "", ColumnName, ColumnHeaderTxt)
        column.Width = ColWidth
        CType(column, DataGridViewImageColumn).ReadOnly = isReadOnly
        column.AgReadOnly = isReadOnly
        Dg1.Columns.Add(column)
        column.Visible = columnVisible
        column.SortMode = mSortMode
        If isReadOnly Then column.DefaultCellStyle.BackColor = DirectCast(Dg1, AgControls.AgDataGrid).AgReadOnlyColumnColor
    End Sub

    Public Sub AddAgLinkColumn(ByVal Dg1 As DataGridView, ByVal ColumnName As String, ByVal ColWidth As Integer, Optional ByVal ColumnHeaderTxt As String = "", Optional ByVal columnVisible As Boolean = True, Optional ByVal isReadOnly As Boolean = False, Optional ByVal isRightAlign As Boolean = False, Optional ByVal mSortMode As DataGridViewColumnSortMode = DataGridViewColumnSortMode.NotSortable)
        Dim column As AgControls.AgLinkColumn
        column = New AgControls.AgLinkColumn

        column.Name = ColumnName
        column.HeaderText = IIf(ColumnHeaderTxt = "", ColumnName, ColumnHeaderTxt)
        column.Width = ColWidth
        CType(column, DataGridViewLinkColumn).ReadOnly = isReadOnly
        column.AgReadOnly = isReadOnly
        Dg1.Columns.Add(column)
        column.Visible = columnVisible
        column.SortMode = mSortMode
        If isReadOnly Then column.DefaultCellStyle.BackColor = DirectCast(Dg1, AgControls.AgDataGrid).AgReadOnlyColumnColor
    End Sub



    'Public Sub IniMasterHelpList(ByVal mConn As SqlClient.SqlConnection, ByVal ListBox As System.Windows.Forms.ComboBox, ByVal QryStr As String, ByVal DispField As String, ByVal HiddenField As String)
    '    Dim mSelectedValue As String
    '    Dim mText As String

    '    Try
    '        mSelectedValue = ListBox.SelectedValue
    '        mText = ListBox.Text

    '        ListBox.DropDownStyle = ComboBoxStyle.DropDown
    '        ListBox.AutoCompleteSource = AutoCompleteSource.ListItems
    '        ListBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend

    '        Dim DS As New DataTable
    '        EAdptr = New SqlClient.SqlDataAdapter(QryStr, mConn)
    '        EAdptr.Fill(DS)
    '        ListBox.DataSource = DS
    '        ListBox.DisplayMember = DispField
    '        ListBox.ValueMember = HiddenField
    '        If ListBox.Items.Count = 0 Then ListBox.Text = ""
    '        ListBox.SelectedValue = IIf(mSelectedValue Is Nothing, "", mSelectedValue)
    '        ListBox.Text = mText
    '    Catch Ex As Exception
    '        MsgBox(Ex.Message)
    '    End Try
    'End Sub

    'Public Sub IniHelpList(ByVal mConn As SqlClient.SqlConnection, ByVal ListBox As System.Windows.Forms.ComboBox, ByVal QryStr As String, ByVal DispField As String, ByVal HiddenField As String)
    '    Dim mSelectedValue As String
    '    Try
    '        mSelectedValue = ListBox.SelectedValue

    '        ListBox.DropDownStyle = ComboBoxStyle.DropDown
    '        ListBox.AutoCompleteSource = AutoCompleteSource.ListItems
    '        ListBox.AutoCompleteMode = AutoCompleteMode.Suggest

    '        Dim DS As New DataTable
    '        EAdptr = New SqlClient.SqlDataAdapter(QryStr, mConn)
    '        EAdptr.Fill(DS)
    '        ListBox.DataSource = DS
    '        ListBox.DisplayMember = DispField
    '        ListBox.ValueMember = HiddenField
    '        If ListBox.Items.Count = 0 Then ListBox.Text = ""
    '        If mSelectedValue Is Nothing Then
    '            ListBox.SelectedValue = ""
    '        Else
    '            ListBox.SelectedValue = mSelectedValue
    '        End If
    '    Catch Ex As Exception
    '        MsgBox(Ex.Message)
    '    End Try
    'End Sub

    Public Sub IniAgHelpList(ByVal mConn As SQLiteConnection, ByVal ListBox As System.Windows.Forms.ComboBox, ByVal QryStr As String, ByVal DispField As String, ByVal HiddenField As String)
        Dim mSelectedValue As String
        Dim mText As String

        mSelectedValue = ListBox.SelectedValue
        mText = ListBox.Text

        ListBox.DropDownStyle = ComboBoxStyle.DropDown
        ListBox.AutoCompleteSource = AutoCompleteSource.ListItems
        ListBox.AutoCompleteMode = AutoCompleteMode.Suggest

        Dim DS As New DataTable
        EAdptr = New SQLiteDataAdapter(QryStr, mConn)
        EAdptr.Fill(DS)
        ListBox.DataSource = DS
        ListBox.DisplayMember = DispField
        ListBox.ValueMember = HiddenField
        If ListBox.Items.Count = 0 Then ListBox.Text = ""
        ListBox.SelectedValue = IIf(mSelectedValue Is Nothing, "", mSelectedValue)
        ListBox.Text = mText
    End Sub

    Public Sub IniAgHelpList(ByVal mConn As SqlClient.SqlConnection, ByVal ListBox As System.Windows.Forms.ComboBox, ByVal QryStr As String, ByVal DispField As String, ByVal HiddenField As String)
        Dim mSelectedValue As String
        Dim mText As String
        Dim EAdptr As SqlClient.SqlDataAdapter

        mSelectedValue = ListBox.SelectedValue
        mText = ListBox.Text

        ListBox.DropDownStyle = ComboBoxStyle.DropDown
        ListBox.AutoCompleteSource = AutoCompleteSource.ListItems
        ListBox.AutoCompleteMode = AutoCompleteMode.Suggest

        Dim DS As New DataTable
        EAdptr = New SqlClient.SqlDataAdapter(QryStr, mConn)
        EAdptr.Fill(DS)
        ListBox.DataSource = DS
        ListBox.DisplayMember = DispField
        ListBox.ValueMember = HiddenField
        If ListBox.Items.Count = 0 Then ListBox.Text = ""
        ListBox.SelectedValue = IIf(mSelectedValue Is Nothing, "", mSelectedValue)
        ListBox.Text = mText
    End Sub

    Public Sub IniAgHelpList(ByVal ListBox As System.Windows.Forms.ComboBox, ByVal ListItem As String, Optional ByVal DatabaseValue As String = "")
        Dim ListItemStr() As String = Split(ListItem, ",")
        Dim DatabaseValueStr() As String = Nothing
        If DatabaseValue <> "" Then DatabaseValueStr = Split(DatabaseValue, ",")
        Dim DS As New DataTable

        DS.Columns.Add("Name")
        DS.Columns.Add("Code")
        For i As Integer = 0 To ListItemStr.Length - 1
            DS.Rows.Add(i)
            DS.Rows(i).Item("Name") = ListItemStr(i)
            If DatabaseValue <> "" Then
                DS.Rows(i).Item("Code") = DatabaseValueStr(i)
            Else
                DS.Rows(i).Item("Code") = ListItemStr(i)
            End If
        Next
        ListBox.DataSource = DS
        ListBox.DisplayMember = "name"
        ListBox.ValueMember = "code"
    End Sub

    Public Sub IniAgHelpList(ByVal ListBox As AgTextBox, ByVal ListItem As String, Optional ByVal DatabaseValue As String = "")
        Dim ListItemStr() As String = Split(ListItem, ",")
        Dim DatabaseValueStr() As String = Nothing
        If DatabaseValue <> "" Then DatabaseValueStr = Split(DatabaseValue, ",")
        Dim DS As New DataTable
        Dim DsTemp As New DataSet

        DS.Columns.Add("Code")
        DS.Columns.Add("Name")
        For i As Integer = 0 To ListItemStr.Length - 1
            DS.Rows.Add(i)
            DS.Rows(i).Item("Name") = ListItemStr(i)
            If DatabaseValue <> "" Then
                DS.Rows(i).Item("Code") = DatabaseValueStr(i)
            Else
                DS.Rows(i).Item("Code") = ListItemStr(i)
            End If
        Next
        DsTemp.Tables.Add(DS)
        ListBox.AgHelpDataSet = DsTemp

        DsTemp.Dispose()
        DS.Dispose()
    End Sub


    Public Sub AddAgCheckBoxColumn(ByVal Dg1 As DataGridView, ByVal columnName As String, ByVal ColWidth As Integer, Optional ByVal columnHeaderTxt As String = "", Optional ByVal columnVisible As Boolean = True, Optional ByVal isReadOnly As Boolean = False, Optional ByVal isRightAlign As Boolean = False, Optional ByVal mSortMode As DataGridViewColumnSortMode = DataGridViewColumnSortMode.NotSortable)
        Dim column As AgCheckBoxColumn
        column = New AgCheckBoxColumn()


        CType(column, AgCheckBoxColumn).Name = columnName
        CType(column, AgCheckBoxColumn).HeaderText = IIf(columnHeaderTxt = "", columnName, columnHeaderTxt)
        CType(column, AgCheckBoxColumn).Width = ColWidth
        column.ReadOnly = isReadOnly
        Dg1.Columns.Add(column)
        column.Visible = columnVisible
        column.SortMode = mSortMode
        If isReadOnly Then column.DefaultCellStyle.BackColor = DirectCast(Dg1, AgControls.AgDataGrid).AgReadOnlyColumnColor
    End Sub


    Public Sub AddAgComboColumn(ByVal mConn As SQLiteConnection, ByVal Dg1 As DataGridView, ByVal QryStr As String, ByVal columnName As String, ByVal ColWidth As Integer, Optional ByVal columnHeaderTxt As String = "", Optional ByVal columnVisible As Boolean = True, Optional ByVal isReadOnly As Boolean = False, Optional ByVal mSortMode As DataGridViewColumnSortMode = DataGridViewColumnSortMode.NotSortable)
        Dim column As AgComboColumn
        column = New AgComboColumn()


        CType(column, AgComboColumn).Name = columnName
        CType(column, AgComboColumn).Width = ColWidth
        CType(column, AgComboColumn).HeaderText = IIf(columnHeaderTxt = "", columnName, columnHeaderTxt)
        CType(column, AgComboColumn).DisplayStyleForCurrentCellOnly = True

        IniGridHelp(mConn, column, QryStr)
        column.ReadOnly = isReadOnly
        Dg1.Columns.Add(column)
        column.Visible = columnVisible
        column.SortMode = mSortMode
        If isReadOnly Then column.DefaultCellStyle.BackColor = DirectCast(Dg1, AgControls.AgDataGrid).AgReadOnlyColumnColor
    End Sub


    Public Sub RefGridHelp(ByVal mConn As SQLiteConnection, ByVal Dg1 As DataGridView, ByVal ColumnIndex As Byte, ByVal QryStr As String)
        If Dg1.Item(ColumnIndex, 0).Visible = False Then Exit Sub
        Dg1.CurrentCell = Dg1.Item(ColumnIndex, 0)
        IniGridHelp(mConn, Dg1.CurrentCell.OwningColumn, QryStr)
    End Sub

    Public Sub RefGridHelp_Line(ByVal mConn As SQLiteConnection, ByVal Dg1 As DataGridView, ByVal ColumnIndex As Byte, ByVal QryStr As String)
        If Dg1.Item(ColumnIndex, 0).Visible = False Then Exit Sub
        IniGridHelp(mConn, Dg1.CurrentCell.OwningColumn, QryStr)
    End Sub

    Public Function XNull(ByVal temp As Object) As Object
        XNull = CStr(IIf(IsDBNull(temp), "", temp))
    End Function

    Public Function VNull(ByRef temp As Object) As Object
        VNull = Val(IIf(IsDBNull(temp), 0, temp))
    End Function

    Private Sub IniAgGridList(ByVal column As DataGridViewColumn, ByVal ListItem As String, Optional ByVal DatabaseValue As String = "")
        Dim ListItemStr() As String = Split(ListItem, ",")
        Dim DatabaseValueStr() As String = Nothing
        If DatabaseValue <> "" Then DatabaseValueStr = Split(DatabaseValue, ",")
        Dim DS As New DataTable
        Dim myDs As New DataSet
        DS.Columns.Add("Code")
        DS.Columns.Add("Name")
        For i As Integer = 0 To ListItemStr.Length - 1
            DS.Rows.Add(i)
            DS.Rows(i).Item("Name") = ListItemStr(i)
            If DatabaseValue <> "" Then
                DS.Rows(i).Item("Code") = DatabaseValueStr(i)
            Else
                DS.Rows(i).Item("Code") = ListItemStr(i)
            End If
        Next

        If TypeOf column Is AgTextColumn Then
            myDs.Tables.Add(DS)
            CType(column, AgTextColumn).AgHelpDataSet = myDs
        Else
            CType(column, DataGridViewComboBoxColumn).DataSource = DS
            CType(column, DataGridViewComboBoxColumn).DisplayMember = "name"
            CType(column, DataGridViewComboBoxColumn).ValueMember = "code"
        End If
    End Sub

    Private Sub IniGridHelp(ByVal mConn As SQLiteConnection, ByVal column As DataGridViewColumn, ByVal QryStr As String)
        Dim DS As New DataTable
        DS.Clear()
        EAdptr = New SQLiteDataAdapter(QryStr, mConn)
        EAdptr.Fill(DS)
        CType(column, DataGridViewComboBoxColumn).DataSource = DS
        CType(column, DataGridViewComboBoxColumn).DisplayMember = "name"
        CType(column, DataGridViewComboBoxColumn).ValueMember = "code"
    End Sub


    Public Function AgCheckMandatory(ByVal Frm As Form) As Boolean
        Dim Obj As Control
        Dim Dg As AgDataGrid
        Dim I%, J%

        Try
            AgCheckMandatory = True
            For Each Obj In Frm.Controls
                If TypeOf Obj Is AgTextBox Then
                    With CType(Obj, AgTextBox)
                        If .AgMandatory = True Then
                            If .Text.Trim = "" And .AgValueType <> AgTextBox.TxtValueType.Number_Value Then
                                .Focus()
                                Err.Raise(1, , ("Required Field " & Replace(Replace(Obj.Name, "Txt", ""), "txt", "") & vbCrLf & "Can't Be Blank!"))
                            ElseIf Val(.Text) = 0 And .AgValueType = AgTextBox.TxtValueType.Number_Value Then
                                .Focus()
                                Err.Raise(1, , ("Required Field " & Replace(Replace(Obj.Name, "Txt", ""), "txt", "") & vbCrLf & "Can't Be Blank/Zero!"))
                            ElseIf Val(.Text) < 0 And .AgValueType = AgTextBox.TxtValueType.Number_Value And .AgNumberNegetiveAllow = False Then
                                .Focus()
                                Err.Raise(1, , ("Required Field " & Replace(Replace(Obj.Name, "Txt", ""), "txt", "") & vbCrLf & "Can't Be Less Than Zero!"))
                            End If
                        Else
                            If Val(.Text) < 0 And .AgValueType = AgTextBox.TxtValueType.Number_Value And .AgNumberNegetiveAllow = False Then
                                .Focus()
                                Err.Raise(1, , ("Required Field " & Replace(Replace(Obj.Name, "Txt", ""), "txt", "") & vbCrLf & "Can't Be Less Than Zero!"))
                            End If
                        End If
                    End With
                ElseIf TypeOf Obj Is AgDataGrid Then
                    Dg = Obj

                    For I = 0 To Dg.ColumnCount - 1
                        If TypeOf Dg.Columns(I) Is AgTextColumn Then
                            With CType(Dg.Columns(I), AgTextColumn)
                                If .AgMandatory = True Then
                                    For J = 0 To Dg.RowCount - 2
                                        If .AgValueType <> AgControls.AgTextColumn.TxtValueType.Number_Value Then
                                            If Dg.Item(I, J).Value = "" Then
                                                If Dg.Columns(I).Visible = True Then Dg.CurrentCell = Dg(I, J) : Dg.Focus()
                                                Err.Raise(1, , Dg.Columns(I).HeaderText & " is a Required Field" & vbCrLf & "Can't Be Blank!")
                                            End If
                                        ElseIf .AgValueType = AgControls.AgTextColumn.TxtValueType.Number_Value Then
                                            If Val(Dg.Item(I, J).Value) = 0 Then
                                                If Dg.Columns(I).Visible = True Then Dg.CurrentCell = Dg(I, J) : Dg.Focus()
                                                Err.Raise(1, , Dg.Columns(I).HeaderText & " is a Required Field" & vbCrLf & "Can't Be Blank/Zero!")
                                            ElseIf Val(Dg.Item(I, J).Value) < 0 And .AgNumberNegetiveAllow = False Then
                                                If Dg.Columns(I).Visible = True Then Dg.CurrentCell = Dg(I, J) : Dg.Focus()
                                                Err.Raise(1, , Dg.Columns(I).HeaderText & " is a Required Field" & vbCrLf & "Can't Be Blank/Zero!")
                                            End If
                                        End If
                                    Next J
                                End If
                            End With
                        End If
                    Next I
                End If
            Next
        Catch Ex As Exception
            MsgBox(Ex.Message)
            AgCheckMandatory = False
        End Try
    End Function

    Public Function AgIsBlankGrid(ByVal Dg As DataGridView, ByVal ColumnIndex As Integer) As Boolean
        Dim I%, Count%

        Try
            If Dg.RowCount > 0 Then
                For I = 0 To Dg.RowCount - 1
                    If Dg.Item(ColumnIndex, I).Value IsNot Nothing Then
                        If Dg.Item(ColumnIndex, I).Value.ToString <> "" Then
                            Count += 1
                        End If
                    End If
                Next I
            End If

            If Count = 0 Then
                Dg.CurrentCell = Dg(ColumnIndex, 0) : Dg.Focus()
                Err.Raise(1, , "No Transaction Data in Grid")
            End If

        Catch Ex As Exception
            MsgBox(Ex.Message)
            AgIsBlankGrid = True
        End Try
    End Function

    Public Function AgBlankNothingCells(ByVal Dg As DataGridView) As Boolean
        Dim I%, J%

        Try
            AgBlankNothingCells = True

            For I = 0 To Dg.Rows.Count - 1
                For J = 0 To Dg.Columns.Count - 1
                    If Dg.Item(J, I).Value Is Nothing Then Dg.Item(J, I).Value = ""
                Next
            Next I

        Catch Ex As Exception
            MsgBox(Ex.Message)
            AgBlankNothingCells = False
        End Try
    End Function


    Public Function AgIsDuplicate(ByVal Dg As AgDataGrid, ByVal ColumnList As String) As Boolean
        Dim myArrColstr() As String = Split(ColumnList, ",")
        Dim myArrCol(myArrColstr.Length - 1) As Integer
        Dim i As Integer, J As Integer, K As Integer
        Dim StrTemp$, StrSearch$

        Try
            For i = 0 To myArrColstr.Length - 1
                myArrCol(i) = Val(myArrColstr(i))
            Next


            For i = 0 To Dg.RowCount - 1
                StrSearch = ""
                For K = 0 To myArrCol.Length - 1
                    StrSearch = StrSearch & CStr(Dg.Item(myArrCol(K), i).Value)
                Next

                If StrSearch.Trim <> "" Then
                    For J = 0 To Dg.RowCount - 1
                        If J <> i And Dg.Rows(i).Visible And Dg.Rows(J).Visible Then
                            StrTemp = ""
                            For K = 0 To myArrCol.Length - 1
                                StrTemp = StrTemp & CStr(Dg.Item(myArrCol(K), J).Value)
                            Next

                            If StrTemp = StrSearch Then
                                Dg.CurrentCell = Dg(myArrCol(0), J) : Dg.Focus()
                                Err.Raise(1, , " Duplicate Key In Row  " & i + 1 & " And Row  " & J + 1)
                            End If
                        End If
                    Next
                End If
            Next
        Catch Ex As Exception
            MsgBox(Ex.Message)
            AgIsDuplicate = True
        End Try
    End Function

    Public Shared Function GetFindStr(ByVal SearchStr As String) As String
        GetFindStr = SearchStr
    End Function


    Public Shared Function FActualLeft(ByVal mObj As Object) As Integer
        Dim mLeft As Integer = mObj.Left

        While Not TypeOf (mObj.Parent) Is Form
            mLeft += mObj.Left
            mObj = mObj.parent
        End While

        FActualLeft = mLeft
    End Function

    Public Shared Function FActualTop(ByVal mObj As Object) As Integer
        Dim mTop As Integer = mObj.Top

        While Not TypeOf (mObj.Parent) Is Form
            mTop += mObj.Top
            mObj = mObj.parent
        End While

        FActualTop = mTop
    End Function

    Class GridSetting
        Public Sub New(ByVal Item As String, _
                ByVal ColIndex As Integer, ByVal Width As Integer, ByVal Visible As Boolean, ByVal AggregateFunction As String)
            ' Set fields.

            Me._Item = Item
            Me._ColIndex = ColIndex
            Me._Width = Width
            Me._Visible = Visible
            Me._AggregateFunction = IIf(AggregateFunction Is Nothing, "", AggregateFunction)
        End Sub

        ' Storage of Grid data.
        Public _Item As String
        Public _ColIndex As Integer
        Public _Width As Integer
        Public _Visible As Boolean
        Public _AggregateFunction As String
    End Class

    Public Sub GridSetiingShowXml(ByVal File_Name As String, ByVal mGrid As DataGridView, Optional ByVal ApplyVisibility As Boolean = True)
        Dim i As Integer
        Dim m_xmlr As XmlTextReader = Nothing

        Try
            If File.Exists(My.Application.Info.DirectoryPath & "\Setting\" & File_Name & ".xml") = False Then Exit Sub
            m_xmlr = New XmlTextReader(My.Application.Info.DirectoryPath & "\Setting\" & File_Name & ".xml")
            'Disable whitespace so that you don't have to read over whitespaces

            m_xmlr.WhitespaceHandling = WhitespaceHandling.None
            'read the xml declaration and advance to family tag

            m_xmlr.Read()
            'read the family tag

            m_xmlr.Read()
            'Load the Loop

            While Not m_xmlr.EOF
                'Go to the name tag

                m_xmlr.Read()
                'if not start element exit while loop

                If Not m_xmlr.IsStartElement() Then
                    Exit While
                End If
                'Get the Gender Attribute Value

                'Dim genderAttribute = m_xmlr.GetAttribute("gender")
                ''Read elements firstname and lastname

                m_xmlr.Read()
                'Get the firstName Element Value

                Dim NameValue = m_xmlr.ReadElementString("Name")
                Dim Mindex = m_xmlr.ReadElementString("Index")
                Dim MWidth = m_xmlr.ReadElementString("Width")
                Dim MVisible = m_xmlr.ReadElementString("Visible")
                Dim MAggregateFunction = m_xmlr.ReadElementString("AggregateFunction")

                For i = 0 To mGrid.ColumnCount - 1
                    If mGrid.Columns(i).Name = NameValue Then
                        mGrid.Columns(i).DisplayIndex = Mindex
                        mGrid.Columns(i).Width = MWidth
                        If ApplyVisibility Then mGrid.Columns(i).Visible = MVisible
                        mGrid.Columns(i).Tag = MAggregateFunction
                    End If
                Next


                Console.Write(vbCrLf)
            End While
        Catch EX As Exception
        Finally
            'close the reader
            If m_xmlr IsNot Nothing Then
                m_xmlr.Close()
            End If
        End Try
    End Sub

    Public Sub GridSetiingWriteXml(ByVal File_Name As String, ByVal mGrid As DataGridView)

        Dim GridSave(0) As GridSetting
        Dim i As Integer

        For i = 0 To mGrid.ColumnCount - 1
            ReDim Preserve GridSave(i)
            GridSave(i) = New GridSetting(mGrid.Columns(i).Name, mGrid.Columns(i).DisplayIndex, mGrid.Columns(i).Width, mGrid.Columns(i).Visible, mGrid.Columns(i).Tag)
        Next

        ' Create XmlWriterSettings.
        Dim settings As XmlWriterSettings = New XmlWriterSettings()
        settings.Indent = True
        If My.Computer.FileSystem.DirectoryExists(My.Application.Info.DirectoryPath & "\Setting") = False Then
            My.Computer.FileSystem.CreateDirectory(My.Application.Info.DirectoryPath & "\Setting")
        End If





        Using writer As XmlWriter = XmlWriter.Create(My.Application.Info.DirectoryPath & "\Setting\" & File_Name & ".xml", settings)
            ' Begin writing.
            writer.WriteStartDocument()
            writer.WriteStartElement("GridSave") ' Root.


            Dim Column As GridSetting
            For Each Column In GridSave
                writer.WriteStartElement("Column")

                writer.WriteElementString("Name", Column._Item)
                writer.WriteElementString("Index", Column._ColIndex.ToString)
                writer.WriteElementString("Width", Column._Width.ToString)
                writer.WriteElementString("Visible", Column._Visible.ToString)
                writer.WriteElementString("AggregateFunction", Column._AggregateFunction.ToString)
                writer.WriteEndElement()
            Next

            ' End document.
            writer.WriteEndElement()
            writer.WriteEndDocument()
        End Using
    End Sub

 

End Class
