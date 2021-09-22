Imports System.Xml
Imports System.IO
Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.SQLite
Imports System.Drawing.Printing
Imports System.Text.RegularExpressions
Imports System.ComponentModel

Public Class FrmRepDisplay
    Dim mQry As String = ""
    Dim mMainQry As String = ""
    Dim mReportName As String = ""
    Dim mReportFormatName As String = ""
    Dim mReportSubTitle As String = ""
    Dim AgL As AgLibrary.ClsMain

    Dim mColumnIndex As Integer
    Dim mRowIndex As Integer

    Dim DsMaster As DataSet = Nothing
    Dim DtMainWithTotals As DataTable = Nothing

    Dim DsTotal As DataSet = Nothing

    Public WithEvents DGL1 As New AgControls.AgDataGrid
    Public WithEvents DGL2 As New AgControls.AgDataGrid
    Public WithEvents FilterGrid As New AgControls.AgDataGrid
    Public WithEvents FilterGridDisplay As New AgControls.AgDataGrid

    Public WithEvents FrmObj As FrmRepFormat

    Private Const FilterType_Filter As String = "Filter"
    Private Const FilterType_RemoveFilter As String = "Remove Filter"
    Private Const FilterType_RemoveAllFilter As String = "Remove All Filter"

    Private Const SortType_SortAsc As String = "Ascending"
    Private Const SortType_SortDesc As String = "Descending"
    Private Const SortType_RemoveSort As String = "Remove Sort"

    Private Const MnuType_More As String = "More..."


    Dim TypingFilter As String = ""
    Dim Flag_IsFilterOpen = False
    Dim mReportLineUISetting As DataTable

    'Find Start
    Dim fld As String
    Dim mFlag As Boolean
    Dim ColNo As Integer
    Dim FdName As String
    Dim CdName As String
    Dim RwNo As Integer
    Public HlpSt As String
    Dim View_Name As String
    Dim HlpS As String
    Dim mReportProcName As String
    Dim mIsHideZeroColumns As Boolean = True
    Dim mIsAutoColumnWidth As Boolean = True
    Dim mIsAllowFind As Boolean = True
    Dim mIsManualAggregate As Boolean = False
    Dim mInputColumnsStr As String = ""
    Dim mAllowAutoResizeRows As Boolean = True

    Dim mClsRep As Object
    Dim mDTCustomMenu As DataTable
    'Public WithEvents TextBox1 As New TextBox

    'End Find

    'Filter Start
    '===== For Dgl3 Columns In Grid ======
    '================ Start ================
    '=======================================
    Public Const GFieldCode As Byte = 0
    Public Const GFieldName As Byte = 1
    Public Const GFilter As Byte = 2
    Public Const GButton As Byte = 3
    Public Const GFilterCode As Byte = 4
    Public Const GFilterCodeDataType As Byte = 5
    Public Const GDataType As Byte = 6
    Public Const GDisplayOnReport As Byte = 7
    Public Const GHelpQuery As Byte = 8
    Public Const GHGHeight As Byte = 9
    Public Const GHGWidth As Byte = 10
    Public Const GHGColWidth As Byte = 11
    Public Const GHGColAlignment As Byte = 12
    '=======================================
    '===== For Dgl3 Columns In Grid ======
    '================= End =================
    '=======================================

    'Dim FrmObjArr(0) As FrmReportWindow
    'Dim FiterGridCopy_Arr(0) As AgControls.AgDataGrid

    Public FiterGridCopy_Arr As New List(Of AgControls.AgDataGrid)()
    Public FindTextArr As New List(Of String)()
    Public FocusedRowIndexArr As New List(Of Integer)()
    Public FiterSettingGridCopy_Arr As New List(Of AgControls.AgDataGrid)()


    Public FocusedRowIndex As Integer = -1

    Dim FRH_Single() As DMHelpGrid.FrmHelpGrid
    Dim FRH_Multiple() As DMHelpGrid.FrmHelpGrid_Multi
    'End Filter

    Public Event ProcessReport()
    Public Event FilterApplied()
    Public Event BtnProceedPressed()
    Public Event PostLoad()
    Public Event FormatFilterDisplayGrid()
    Public Event Dgl1KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
    Public Event DGL1EditingControl_Validating(sender As Object, e As CancelEventArgs)
    Public Event DGL1CellBeginEdit(sender As Object, e As DataGridViewCellCancelEventArgs)
    Public Event DGL1CellEnter(sender As Object, e As DataGridViewCellEventArgs)
    Public Event DGL1CellContentClick(sender As Object, e As DataGridViewCellEventArgs)
    Public Event DGL1CheckedColumnValueChanged(Sender As Object, columnIndex As Integer)

    Public Event FilterGridCellBeginEdit(sender As Object, e As DataGridViewCellCancelEventArgs)
    Public Event FilterGridEditingControl_Validating(sender As Object, e As CancelEventArgs)
    Public Event FilterSelectionValidated(rowIndex As Integer)

    Enum FGDataType
        DT_Date = 0
        DT_Numeric = 1
        DT_Float = 2
        DT_String = 3
        DT_None = 4
        DT_Selection_Single = 5
        DT_Selection_Multiple = 6
    End Enum

    Enum FilterCodeType
        DTNone = 0
        DTNumeric = 1
        DTString = 2
    End Enum

    Enum ColumnDataType
        NumberType
        DateTimeType
        StringType
    End Enum

    Public Enum FieldDataType
        DateType = 0
        NumericType = 1
        FloatType = 2
        StringType = 3
        None = 4
        SingleSelection = 5
        MultiSelection = 6
    End Enum

    Enum FieldFilterDataType
        None = 0
        NumericType = 1
        StringType = 2
    End Enum

    Structure StrucColumnFormating
        Dim StrHideColumn As String
        Dim IntWidth As Integer
        Dim StrWrapColumn As String
    End Structure

    Public Property ReportProcName() As String
        Get
            ReportProcName = mReportProcName
        End Get
        Set(ByVal value As String)
            mReportProcName = value
        End Set
    End Property
    Public Property IsAutoColumnWidth() As Boolean
        Get
            IsAutoColumnWidth = mIsAutoColumnWidth
        End Get
        Set(ByVal value As Boolean)
            mIsAutoColumnWidth = value
        End Set
    End Property

    Public Property IsHideZeroColumns() As Boolean
        Get
            IsHideZeroColumns = mIsHideZeroColumns
        End Get
        Set(ByVal value As Boolean)
            mIsHideZeroColumns = value
        End Set
    End Property


    Public Property ClsRep() As Object
        Get
            ClsRep = mClsRep
        End Get
        Set(ByVal value As Object)
            mClsRep = value
        End Set
    End Property

    Public Property DTCustomMenus() As DataTable
        Get
            DTCustomMenus = mDTCustomMenu
        End Get
        Set(ByVal value As DataTable)
            mDTCustomMenu = value
        End Set
    End Property
    Public Property AllowAutoResizeRows() As Boolean
        Get
            AllowAutoResizeRows = mAllowAutoResizeRows
        End Get
        Set(ByVal value As Boolean)
            mAllowAutoResizeRows = value
        End Set
    End Property

    Public Property IsAllowFind() As Boolean
        Get
            IsAllowFind = mIsAllowFind
        End Get
        Set(ByVal value As Boolean)
            mIsAllowFind = value
        End Set
    End Property
    Public Property IsManualAggregate() As Boolean
        Get
            IsManualAggregate = mIsManualAggregate
        End Get
        Set(ByVal value As Boolean)
            mIsManualAggregate = value
        End Set
    End Property
    Public Property InputColumnsStr() As String
        Get
            InputColumnsStr = mInputColumnsStr
        End Get
        Set(ByVal value As String)
            mInputColumnsStr = value
        End Set
    End Property
    Public Property ReportName() As String
        Get
            ReportName = mReportName
        End Get
        Set(ByVal value As String)
            mReportName = value
        End Set
    End Property
    Public Property ReportFormatName() As String
        Get
            ReportFormatName = mReportFormatName
        End Get
        Set(ByVal value As String)
            mReportFormatName = value
        End Set
    End Property
    Public Property ReportSubTitle() As String
        Get
            ReportSubTitle = mReportSubTitle
        End Get
        Set(ByVal value As String)
            mReportSubTitle = value
        End Set
    End Property
    Public Sub New(ByVal StrFormCaption As String, ByVal AgLibVar As ClsMain)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        AgL = AgLibVar
        Me.Text = StrFormCaption
    End Sub

    Private Sub IniGrid()
        AgL.AddAgDataGrid(DGL1, Pnl1)
        DGL1.AllowUserToAddRows = False
        DGL1.EnableHeadersVisualStyles = False
        DGL1.ReadOnly = True
        DGL1.ContextMenuStrip = MnuMain
        DGL1.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
        DGL1.AllowUserToOrderColumns = True
        DGL1.AgAllowFind = False
        DGL1.BackgroundColor = Color.White
        DGL1.DefaultCellStyle.WrapMode = DataGridViewTriState.True
        DGL1.AutoResizeColumnHeadersHeight()
        DGL1.AllowUserToDeleteRows = False
        DGL1.DefaultCellStyle.Padding = New Padding(0, 5, 5, 0)
        DGL1.Name = "DGL1"


        DGL1.DefaultCellStyle.Font = New Font("Verdana", 9, FontStyle.Regular)
        DGL1.ColumnHeadersDefaultCellStyle.Font = New Font("Verdana", 9, FontStyle.Bold)
        DGL1.ColumnHeadersDefaultCellStyle.BackColor = Color.Black
        DGL1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        DGL1.CellBorderStyle = DataGridViewCellBorderStyle.None
        DGL1.CellBorderStyle = DataGridViewCellBorderStyle.None


        'DGL1.AdvancedCellBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.Inset
        'DGL1.AdvancedCellBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.Inset



        AgL.AddAgDataGrid(DGL2, Pnl2)
        DGL2.ColumnHeadersVisible = False
        DGL2.AllowUserToAddRows = False
        DGL2.EnableHeadersVisualStyles = False
        DGL2.ScrollBars = ScrollBars.None
        DGL2.RowHeadersVisible = False
        DGL2.ReadOnly = True
        DGL2.AllowUserToResizeColumns = False
        DGL2.AgAllowFind = False
        DGL2.Name = "DGL2"

        'DGL2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'DGL2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer))
        DGL2.DefaultCellStyle.Font = New Font("Verdana", 9, FontStyle.Bold)
        DGL2.ForeColor = Color.White
        DGL2.BackgroundColor = Color.White
        DGL2.DefaultCellStyle.BackColor = Color.Black
        DGL2.DefaultCellStyle.Padding = New Padding(0, 5, 5, 0)
        DGL2.CellBorderStyle = DataGridViewCellBorderStyle.None

        'DGL2.Enabled = False
        'DGL2.CurrentCell = Nothing
        'DGL2.ClearSelection()
        'DGL2.DefaultCellStyle.SelectionBackColor = Color.Empty
        'DGL2.DefaultCellStyle.SelectionBackColor = DGL2.DefaultCellStyle.BackColor
        'DGL2.DefaultCellStyle.SelectionForeColor = DGL2.DefaultCellStyle.ForeColor

        'DGL2.DefaultCellStyle.SelectionBackColor = Color.Transparent
        'DGL2.DefaultCellStyle.SelectionBackColor = Color.White
        'DGL2.DefaultCellStyle.SelectionForeColor = Color.Black
        'DGL2.RowHeadersDefaultCellStyle.SelectionBackColor = Color.Empty


        DGL1.Anchor = AnchorStyles.Bottom + AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Top
        DGL2.Anchor = Pnl2.Anchor
        FilterGrid.Anchor = Pnl3.Anchor
        FilterGrid.Name = "FilterGrid"
        FilterGrid.ScrollBars = ScrollBars.Vertical


        AgL.AddAgDataGrid(FilterGridDisplay, PnlFilterDisplay)
        FilterGridDisplay.ColumnHeadersVisible = False
        FilterGridDisplay.AllowUserToAddRows = False
        FilterGridDisplay.EnableHeadersVisualStyles = False
        FilterGridDisplay.ReadOnly = True
        FilterGridDisplay.ContextMenuStrip = MnuMain
        FilterGridDisplay.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
        FilterGridDisplay.AllowUserToOrderColumns = True
        FilterGridDisplay.AgAllowFind = False
        FilterGridDisplay.BackgroundColor = Color.White
        FilterGridDisplay.DefaultCellStyle.WrapMode = DataGridViewTriState.True
        FilterGridDisplay.AutoResizeColumnHeadersHeight()
        FilterGridDisplay.DefaultCellStyle.Padding = New Padding(0, 5, 5, 0)
        FilterGridDisplay.DefaultCellStyle.Font = New Font("Verdana", 9, FontStyle.Regular)
        FilterGridDisplay.ColumnHeadersDefaultCellStyle.Font = New Font("Verdana", 9, FontStyle.Bold)
        FilterGridDisplay.ColumnHeadersDefaultCellStyle.BackColor = Color.Black
        FilterGridDisplay.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        FilterGridDisplay.CellBorderStyle = DataGridViewCellBorderStyle.None
        FilterGridDisplay.CellBorderStyle = DataGridViewCellBorderStyle.None
        FilterGridDisplay.AllowUserToResizeColumns = False
        FilterGridDisplay.AllowUserToResizeRows = False
        'FilterGridDisplay.DefaultCellStyle.SelectionBackColor = Color.Transparent
        FilterGridDisplay.Anchor = PnlFilterDisplay.Anchor
        FilterGridDisplay.Name = "FilterGridDisplay"

        FilterGridDisplay.BorderStyle = BorderStyle.None
        FilterGridDisplay.ScrollBars = ScrollBars.None
        FilterGridDisplay.Anchor = AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Top
    End Sub

    Private Sub FrmReportWindow_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F10
                Me.Close()

            Case Keys.F5
                RaiseEvent ProcessReport()

            Case Keys.F3
                FManageFilterVisibility()

            Case Keys.Escape
                FProcessEscapeButton()
        End Select
    End Sub
    Public Sub FProcessEscapeButton(Optional bFillGrid As Boolean = True)
        'If FiterGridCopy_Arr.Length = 0 Then
        '    Me.Close()
        'Else
        '    CopyDataGridViewFromOneToOther(FiterGridCopy_Arr(FiterGridCopy_Arr.Length - 2), DGL3)
        '    RaiseEvent ProcessReport()
        'End If

        If FindTextArr.Count > 0 Then
            TxtFind.Text = FindTextArr(FindTextArr.Count - 1)
            FindTextArr.RemoveAt(FindTextArr.Count - 1)
        End If

        If FocusedRowIndexArr.Count > 0 Then
            FocusedRowIndex = FocusedRowIndexArr(FocusedRowIndexArr.Count - 1)
            FocusedRowIndexArr.RemoveAt(FocusedRowIndexArr.Count - 1)
        End If

        If FiterSettingGridCopy_Arr.Count > 0 Then
            CopyDataGridViewFromOneToOther(FiterSettingGridCopy_Arr(FiterSettingGridCopy_Arr.Count - 1), FrmObj.DglFilter)
            FiterSettingGridCopy_Arr.RemoveAt(FiterSettingGridCopy_Arr.Count - 1)
        End If

        If FiterGridCopy_Arr.Count = 0 Then
            'Me.Close()
        Else
            CopyDataGridViewFromOneToOther(FiterGridCopy_Arr(FiterGridCopy_Arr.Count - 1), FilterGrid)
            'RaiseEvent ProcessReport()
            If bFillGrid = True Then
                Dim Result$ = CStr(CallByName(mClsRep, mReportProcName, CallType.Method, FilterGrid))
            End If
            FAdjustFooter()
            FiterGridCopy_Arr.RemoveAt(FiterGridCopy_Arr.Count - 1)
            FManagerFilterDisplayGrid(Flag_IsFilterOpen)
        End If
    End Sub

    Private Sub CopyDataGridViewFromOneToOther(CopyFrom As AgControls.AgDataGrid, CopyTo As AgControls.AgDataGrid)
        CopyTo.Columns.Clear()
        CopyTo.Rows.Clear()

        If CopyTo.Columns.Count = 0 Then
            For Each dgvc As DataGridViewColumn In CopyFrom.Columns
                CopyTo.Columns.Add(TryCast(dgvc.Clone(), DataGridViewColumn))
            Next
        End If

        Dim row As New DataGridViewRow()

        For i As Integer = 0 To CopyFrom.Rows.Count - 1
            row = DirectCast(CopyFrom.Rows(i).Clone(), DataGridViewRow)
            Dim intColIndex As Integer = 0
            For Each cell As DataGridViewCell In CopyFrom.Rows(i).Cells
                row.Cells(intColIndex).Value = cell.Value
                intColIndex += 1
            Next
            CopyTo.Rows.Add(row)
        Next
    End Sub

    Private Sub CopySelectedDataGridViewRowFromOneToOther(CopyFrom As AgControls.AgDataGrid, CopyTo As AgControls.AgDataGrid)
        CopyTo.Columns.Clear()
        CopyTo.Rows.Clear()

        If CopyTo.Columns.Count = 0 Then
            For Each dgvc As DataGridViewColumn In CopyFrom.Columns
                CopyTo.Columns.Add(TryCast(dgvc.Clone(), DataGridViewColumn))
            Next
        End If

        Dim row As New DataGridViewRow()

        For i As Integer = 0 To CopyFrom.Rows.Count - 1
            If DGL1.Columns.Contains("Tick") Then
                If DGL1.Item("Tick", i).Value = "þ" Then
                    row = DirectCast(CopyFrom.Rows(i).Clone(), DataGridViewRow)
                    Dim intColIndex As Integer = 0
                    For Each cell As DataGridViewCell In CopyFrom.Rows(i).Cells
                        row.Cells(intColIndex).Value = cell.Value
                        intColIndex += 1
                    Next
                    CopyTo.Rows.Add(row)
                End If
            Else
                row = DirectCast(CopyFrom.Rows(i).Clone(), DataGridViewRow)
                Dim intColIndex As Integer = 0
                For Each cell As DataGridViewCell In CopyFrom.Rows(i).Cells
                    row.Cells(intColIndex).Value = cell.Value
                    intColIndex += 1
                Next
                CopyTo.Rows.Add(row)
            End If
        Next
    End Sub

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'Me.Text = mReportName
            'Me.Dock = DockStyle.Fill
            'Pnl1.Width = Me.Width - 10
            'Pnl2.Width = Me.Width - 10
            'FilterGrid.Width = Me.Width - 10
            'BtnCustomMenu.Left = Me.Width - BtnCustomMenu.Width - 15
            'BtnFill.Left = Me.Width - BtnFill.Width - BtnCustomMenu.Width - 15
            'BtnFilter.Left = Me.Width - BtnFilter.Width - BtnFill.Width - BtnCustomMenu.Width - 15

            AgL.GridDesign(DGL1)
            AgL.GridDesign(DGL2)
            AgL.GridDesign(FilterGrid)
            AgL.GridDesign(FilterGridDisplay)
            IniGrid()
            AgL.GetReportUISetting(FilterGrid, mReportName, mReportFormatName, AgL.PubDivCode, AgL.PubSiteCode, ClsMain.GridTypeConstants.VerticalGrid, AgL)

            'ProcFillGrid(mMainQry)


            'ProcFillVisibleColumnMenu()

            'FrmObj = New FrmReportFormat(FunRetColumnList)
            'FrmObj.IniGrid()
            'FrmObj.Ini_List()
            'FrmObj.ProcFillVisibleGrids(FunRetVisibleColumnList())

            'Call ProcShowSortSettings(Me.Text + "-Sort")
            'Call ProcShowFilterSettings(Me.Text + "-Filter")

            'Call ProcSortGrid()
            'Call ProcFilterGrid()
            'Call ProcApplyAggregateFunction()


            FilterGrid.Height = 0
            FManageFilterVisibility()

            RaiseEvent PostLoad()


            Me.WindowState = FormWindowState.Maximized

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function FunRetVisibleColumnList() As String
        Dim I As Integer = 0
        Try
            mQry = ""
            With DGL1
                For I = 0 To .Columns.Count - 1
                    If DGL1.Columns(I).HeaderText <> "Search Code" Then
                        If mQry = "" Then

                            mQry = " Select '" & DGL1.Columns(I).HeaderText & "' AS FieldName, " &
                                    " '" & DGL1.Columns(I).Visible & "' AS IsSelect, " &
                                    " '" & DGL1.Columns(I).Tag & "' AS AggregateFunction, " & DGL1.Columns(I).DisplayIndex & " as DispIndex "
                        Else
                            mQry = mQry & " UNION ALL "
                            mQry = mQry & " Select '" & DGL1.Columns(I).HeaderText & "' AS FieldName, " &
                                    " '" & DGL1.Columns(I).Visible & "' AS IsSelect, " &
                                    " '" & DGL1.Columns(I).Tag & "' AS AggregateFunction, " & DGL1.Columns(I).DisplayIndex & " as DispIndex "
                        End If
                    End If
                Next
                mQry = mQry & " Order By DispIndex "
            End With
            FunRetVisibleColumnList = mQry
        Catch ex As Exception
            FunRetVisibleColumnList = ""
            MsgBox(ex.Message)
        End Try
    End Function

    Private Function FunRetColumnList() As String
        Dim I As Integer = 0
        Try
            mQry = ""
            With DGL1
                For I = 0 To .Columns.Count - 1
                    If DGL1.Columns(I).HeaderText <> "Search Code" Then
                        If mQry = "" Then
                            mQry = " Select '" & DGL1.Columns(I).Name & "' AS Code, '" & DGL1.Columns(I).HeaderText & "' AS FieldName "
                        Else
                            mQry = mQry & " UNION ALL "
                            mQry = mQry & " Select '" & DGL1.Columns(I).Name & "' AS Code, '" & DGL1.Columns(I).HeaderText & "' AS FieldName"
                        End If
                    End If
                Next
            End With
            FunRetColumnList = mQry
        Catch ex As Exception
            FunRetColumnList = ""
            MsgBox(ex.Message)
        End Try
    End Function
    Function HasNumber(strData As String) As Boolean
        Dim iCnt As Integer

        For iCnt = 1 To Len(strData)
            If IsNumeric(Mid(strData, iCnt, 1)) Then
                HasNumber = True
                Exit Function
            End If
        Next iCnt
    End Function

    Public Sub ProcFillGrid(ByVal DsRep As DataSet)
        Dim I As Integer
        Try
            For I = 0 To DsRep.Tables(0).Columns.Count - 1
                If HasNumber(DsRep.Tables(0).Columns(I).ColumnName.Trim()) = False Then
                    Dim ColumnNameArr As MatchCollection = Regex.Matches(DsRep.Tables(0).Columns(I).ColumnName.Trim(), "[A-Z][a-z]+")
                    Dim strColumnName As String = ""
                    For J As Integer = 0 To ColumnNameArr.Count - 1
                        If strColumnName = "" Then
                            strColumnName = ColumnNameArr(J).ToString
                        Else
                            strColumnName += " " + ColumnNameArr(J).ToString
                        End If
                    Next
                    If strColumnName <> "" Then
                        DsRep.Tables(0).Columns(I).ColumnName = strColumnName
                    Else
                        DsRep.Tables(0).Columns(I).ColumnName = DsRep.Tables(0).Columns(I).ColumnName
                    End If
                End If
            Next


            DsMaster = DsRep
            DGL1.DataSource = Nothing
            DGL1.DataSource = DsMaster.Tables(0)


            DGL2.ColumnCount = DGL1.Columns.Count
            DGL2.RowCount = 1


            If IsAutoColumnWidth Then
                DGL1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells)
            End If


            AgCL.GridSetiingShowXml(Me.Text & "-Visible", DGL1)
            AgCL.GridSetiingShowXml(Me.Text & "-Visible", DGL2)
            mReportLineUISetting = AgL.GetReportUISetting(DGL1, mReportName, mReportFormatName, AgL.PubDivCode, AgL.PubSiteCode, ClsMain.GridTypeConstants.HorizontalGrid, AgL)


            For I = 0 To DsMaster.Tables(0).Columns.Count - 1
                Select Case FunRetDataType(UCase(DsMaster.Tables(0).Columns(I).DataType.ToString))
                    Case ColumnDataType.NumberType
                        DGL1.Columns(I).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                        DGL1.Columns(I).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                End Select
            Next
            DGL1.AutoResizeColumnHeadersHeight()

            Dim mTotalColumnWidth As Integer = 0
            For I = 0 To DGL1.Columns.Count - 1
                mTotalColumnWidth = mTotalColumnWidth + DGL1.Columns(I).Width
            Next

            If mTotalColumnWidth < DGL1.Width - 100 And DGL1.Columns.Count < 5 Then
                'DGL1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            End If

            If DGL1.Columns.Contains("Search Code") Then DGL1.Columns("Search Code").Visible = False : DGL2.Columns(DGL1.Columns("Search Code").Index).Visible = False


            For I = 0 To DGL1.Columns.Count - 1
                If mInputColumnsStr.Contains("|" + DGL1.Columns(I).Name + "|") Then
                    DGL1.Columns(I).HeaderCell.Style.BackColor = Color.LightCyan
                    DGL1.Columns(I).HeaderCell.Style.ForeColor = Color.Black

                End If

                If mInputColumnsStr <> "" Then
                    DGL1.AgSkipReadOnlyColumns = True
                End If

                'DGL1.Columns(I).SortMode = DataGridViewColumnSortMode.NotSortable

                If DGL1.Columns(I).ValueType.ToString() = "System.Int32" Or
                        DGL1.Columns(I).ValueType.ToString() = "System.Int64" Or
                        DGL1.Columns(I).ValueType.ToString() = "System.Decimal" Or
                        DGL1.Columns(I).ValueType.ToString() = "System.Float" Or
                        DGL1.Columns(I).ValueType.ToString() = "System.Double" Then

                    If Not DGL1.Columns(I).Name.Contains("Qty") And Not DGL1.Columns(I).Name.Contains("Rate") Then
                        DGL1.Columns(I).DefaultCellStyle.Format = "N2"
                        DGL2.Columns(I).DefaultCellStyle.Format = "N2"
                    End If

                    If DGL1.Columns(I).ValueType.ToString() = "System.Int32" Or
                            DGL1.Columns(I).ValueType.ToString() = "System.Int64" Then
                        DGL1.Columns(I).DefaultCellStyle.Format = "N0"
                        DGL2.Columns(I).DefaultCellStyle.Format = "N0"
                    End If

                    If (mIsHideZeroColumns = True) Then
                        Dim ZeroValueColumn As DataRow() = DsMaster.Tables(0).Select("[" + DGL1.Columns(I).Name + "] <> 0 ")
                        If ZeroValueColumn.Length = 0 Then
                            DGL1.Columns(I).Visible = False
                            DGL2.Columns(I).Visible = False
                        End If
                    End If
                ElseIf DGL1.Columns(I).ValueType.ToString() = "System.DateTime" Or
                        DGL1.Columns(I).ValueType.ToString() = "System.SmallDateTime" Then
                    DGL1.Columns(I).DefaultCellStyle.Format = "dd-MMM-yyyy"
                ElseIf DGL1.Columns(I).ValueType.ToString() = "System.Object" Then
                    Try
                        If (mIsHideZeroColumns = True) Then
                            Dim BlankValueColumn As DataRow() = DsMaster.Tables(0).Select("[" + DGL1.Columns(I).Name + "] <> '' ")
                            If BlankValueColumn.Length = 0 Then
                                DGL1.Columns(I).Visible = False
                                DGL2.Columns(I).Visible = False
                            End If
                        End If
                    Catch ex As Exception
                    End Try
                Else
                    If (mIsHideZeroColumns = True) Then
                        Dim BlankValueColumn As DataRow() = DsMaster.Tables(0).Select("[" + DGL1.Columns(I).Name + "] <> '' ")
                        If BlankValueColumn.Length = 0 Then
                            DGL1.Columns(I).Visible = False
                            DGL2.Columns(I).Visible = False
                        End If
                    End If
                End If
            Next



            ProcFillVisibleColumnMenu()

            If FrmObj Is Nothing Then
                FrmObj = New FrmRepFormat(FunRetColumnList, AgL)
                FrmObj.IniGrid()
            End If
            FrmObj.Ini_List()
            FrmObj.ProcFillVisibleGrids(FunRetVisibleColumnList())

            TypingFilter = ""

            Call ProcShowSortSettings(Me.Text + "-Sort")
            Call ProcShowFilterSettings(Me.Text + "-Filter")



            Call ProcSortGrid()
            Call ProcFilterGrid()
            Call ProcApplyAggregateFunction()
            If DGL1.ColumnHeadersHeight < 40 Then DGL1.ColumnHeadersHeight = 40
            If Flag_IsFilterOpen = True Then FManageFilterVisibility()

            FAdjustFooter()

            If DGL1.Rows.Count < 500 Then
                DGL1.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells)
            Else
                DGL1.AutoResizeRows(DataGridViewAutoSizeRowsMode.DisplayedCells)
            End If

            DGL1.CurrentCell = DGL1.FirstDisplayedCell
            DGL1.Focus()

            FCreateCheckBoxColumn()
            FCreateExceptionColumn()
            FCreateCustomMenus()

            If TxtFind.Text <> "" Then
                Dim KeyPressed As New System.Windows.Forms.KeyPressEventArgs(TxtFind.Text(TxtFind.Text.Length - 1))
                TxtFind.Text = TxtFind.Text.Substring(0, TxtFind.Text.Length - 1)
                TxtFind_KeyPress(TxtFind, KeyPressed)
            End If

            Try
                If FocusedRowIndex <> -1 Then
                    If DGL1.Rows.Count > 0 Then
                        DGL1.CurrentCell = DGL1.Item(DGL1.FirstDisplayedCell.ColumnIndex, FocusedRowIndex)
                        DGL1.Focus()
                    End If
                End If
            Catch ex As Exception
            End Try


            If DGL1.ColumnHeadersHeight < 35 Then DGL1.ColumnHeadersHeight = 35
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            For I = 0 To DGL1.Columns.Count - 1
                DGL2.Columns(I).Visible = DGL1.Columns(I).Visible
                DGL2.Columns(I).Width = DGL1.Columns(I).Width
                DGL2.Columns(I).DisplayIndex = DGL1.Columns(I).DisplayIndex
            Next
        End Try
    End Sub
    Private Sub FCreateCheckBoxColumn()
        If DGL1.Rows.Count > 0 And DGL1.Columns.Count > 0 Then
            If AgL.XNull(DGL1.Item(0, 0).Value) = "þ" Or AgL.XNull(DGL1.Item(0, 0).Value) = "o" Then
                DGL1.Columns(0).DefaultCellStyle.Font = New Font(New FontFamily("wingdings"), 14)
                DGL1.Columns(0).Name = "Tick"
                DGL1.MultiSelect = True
            End If
        End If
    End Sub
    Private Sub FCreateExceptionColumn()
        For I As Integer = 0 To DGL1.Columns.Count - 1
            If DGL1.Columns(I).Name = "Exception" Then
                DGL1.Columns(I).DefaultCellStyle.ForeColor = Color.Maroon
                DGL1.Columns(I).DefaultCellStyle.Font = New Font("Verdana", 9, FontStyle.Bold)
            End If
        Next
    End Sub
    Public Sub FCreateCustomMenus()
        Dim I As Integer = 0
        Dim J As Integer = 0
        Dim bMenuAdded As Boolean = False
        Dim MnuCustomMenuChild As ToolStripMenuItem
        If mDTCustomMenu IsNot Nothing Then
            For I = 0 To mDTCustomMenu.Rows.Count - 1
                If AgL.XNull(mDTCustomMenu.Rows(I)("MenuText")) <> "" Then
                    For J = 0 To MnuCustomOption.Items.Count - 1
                        If AgL.XNull(mDTCustomMenu.Rows(I)("MenuText")) = MnuCustomOption.Items(J).Name Then
                            bMenuAdded = True
                            Exit For
                        End If
                    Next
                    If bMenuAdded = False Then
                        MnuCustomMenuChild = New System.Windows.Forms.ToolStripMenuItem(AgL.XNull(mDTCustomMenu.Rows(I)("MenuText")).ToString)
                        MnuCustomMenuChild.Name = AgL.XNull(mDTCustomMenu.Rows(I)("MenuText")).ToString
                        MnuCustomMenuChild.Tag = AgL.XNull(mDTCustomMenu.Rows(I)("FunctionName")).ToString
                        MnuCustomOption.Items.Add(MnuCustomMenuChild)
                    End If
                End If
            Next
        Else
            For J = 0 To MnuCustomOption.Items.Count - 1
                MnuCustomOption.Items.RemoveAt(0)
            Next
        End If
        If MnuCustomOption.Items.Count > 0 Then
            BtnCustomMenu.Visible = True
        Else
            BtnCustomMenu.Visible = False
        End If
    End Sub

    Private Sub FAdjustFooter()
        If DGL2.Columns.Count = DGL1.Columns.Count Then
            For I As Integer = 0 To DGL1.Columns.Count - 1
                DGL2.Columns(I).Visible = DGL1.Columns(I).Visible
                DGL2.Columns(I).Width = DGL1.Columns(I).Width
                DGL2.Columns(I).Name = DGL1.Columns(I).Name
                'DGL2.Columns(I).ValueType = DGL1.Columns(I).ValueType
            Next
        End If
    End Sub

    Private Sub BlankText()
        DGL1.RowCount = 1 : DGL1.Rows.Clear()
        DGL2.RowCount = 1 : DGL2.Rows.Clear()
        FilterGrid.RowCount = 1 : FilterGrid.Rows.Clear()
    End Sub

    Private Sub DGL1_CellMouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DGL1.CellMouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Call ProcFillFilterMnu()
            Call ProcFillSortMnu()
        End If
    End Sub

    'Private Sub DGL1_ColumnStateChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewColumnStateChangedEventArgs) Handles DGL1.ColumnStateChanged
    '    If DGL2.Columns.Count = DGL1.Columns.Count Then
    '        DGL2.Columns(e.Column.Index).Visible = DGL1.Columns(e.Column.Index).Visible
    '    End If
    'End Sub

    Private Sub DGL1_ColumnWidthChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewColumnEventArgs) Handles DGL1.ColumnWidthChanged
        If DGL2.Columns.Count = DGL1.Columns.Count Then
            DGL2.Columns(e.Column.Index).Width = e.Column.Width
        End If
        DGL1.AutoResizeColumnHeadersHeight()
        'If DGL1.ColumnHeadersHeight < 40 Then DGL1.ColumnHeadersHeight = 40
        DGL1.AutoResizeRows(DataGridViewAutoSizeRowsMode.DisplayedCells)
    End Sub

    Private Sub DGL1_Scroll(ByVal sender As Object, ByVal e As System.Windows.Forms.ScrollEventArgs) Handles DGL1.Scroll
        If e.ScrollOrientation = ScrollOrientation.HorizontalScroll Then
            DGL2.HorizontalScrollingOffset = e.NewValue
            DGL1.AutoResizeRows(DataGridViewAutoSizeRowsMode.DisplayedCells)
        End If
        If DGL1.Rows.Count > 500 Then
            If e.ScrollOrientation = ScrollOrientation.VerticalScroll Then
                If e.Type = ScrollEventType.LargeIncrement Or e.Type = ScrollEventType.LargeDecrement Then
                    DGL1.AutoResizeRows(DataGridViewAutoSizeRowsMode.DisplayedCells)
                End If
            End If
        End If
    End Sub

    Private Sub ProcFillVisibleColumnMenu()
        Dim MnuChild As ToolStripMenuItem
        Dim I As Integer = 0
        Try
            MnuVisible.DropDownItems.Clear()



            With DGL1
                For I = 0 To .Columns.Count - 1
                    If (.Columns(I).HeaderText <> "Search Code") Then
                        'If mReportLineUISetting.Select(" FieldName = '" & .Columns(I).Name & "' ").Length() > 0 Then
                        MnuChild = New ToolStripMenuItem(.Columns(I).HeaderText)
                        MnuChild.CheckOnClick = True
                        MnuChild.Name = .Columns(I).Name
                        MnuChild.Text = .Columns(I).HeaderText
                        MnuChild.Tag = .Columns(I).DisplayIndex
                        MnuChild.Checked = DGL1.Columns(I).Visible
                        MnuVisible.DropDownItems.Add(MnuChild)
                        'End If
                    End If
                Next
            End With

            MnuChild = New ToolStripMenuItem(MnuType_More)
            MnuChild.Name = MnuType_More
            MnuVisible.DropDownItems.Add(MnuChild)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ProcFillVisibleColumnMenuFromVisibleGrid()
        Dim MnuChild As ToolStripMenuItem
        Dim I As Integer = 0
        Try
            MnuVisible.DropDownItems.Clear()

            With FrmObj.DglVisible
                For I = 0 To .Rows.Count - 1
                    If .Item(FrmRepFormat.ColFieldName, I).Value <> "" And .Item(FrmRepFormat.ColFieldName, I).Value <> "Search Code" Then
                        MnuChild = New ToolStripMenuItem(.Item(FrmRepFormat.ColFieldName, I).Value.ToString)
                        MnuChild.CheckOnClick = True
                        MnuChild.Name = .Item(FrmRepFormat.ColFieldName, I).Value.ToString
                        MnuChild.Tag = I
                        MnuChild.Checked = IIf(AgL.StrCmp(.Item(FrmRepFormat.ColIsSelect, I).Value, AgLibrary.ClsConstant.StrCheckedValue), True, False)
                        MnuVisible.DropDownItems.Add(MnuChild)

                        DGL1.Columns(.Item(FrmRepFormat.ColFieldName, I).Value.ToString).Visible = IIf(AgL.StrCmp(.Item(FrmRepFormat.ColIsSelect, I).Value, AgLibrary.ClsConstant.StrCheckedValue), True, False)
                        DGL1.Columns(.Item(FrmRepFormat.ColFieldName, I).Value.ToString).DisplayIndex = I
                        DGL1.Columns(.Item(FrmRepFormat.ColFieldName, I).Value.ToString).Tag = .AgSelectedValue(FrmRepFormat.Col1Function, I)
                    End If
                Next
            End With

            MnuChild = New ToolStripMenuItem(MnuType_More)
            MnuChild.Name = MnuType_More
            MnuVisible.DropDownItems.Add(MnuChild)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub



    Private Sub ProcFillSortMnu()
        Dim MnuChild As ToolStripMenuItem
        Try
            MnuSort.DropDownItems.Clear()

            MnuChild = New ToolStripMenuItem(SortType_SortAsc)
            MnuChild.Name = SortType_SortAsc
            MnuChild.ToolTipText = SortType_SortAsc
            MnuSort.DropDownItems.Add(MnuChild)

            MnuChild = New ToolStripMenuItem(SortType_SortDesc)
            MnuChild.Name = SortType_SortDesc
            MnuChild.ToolTipText = SortType_SortDesc
            MnuSort.DropDownItems.Add(MnuChild)

            ProcCreateRemoveSort(MnuSort)

            MnuChild = New System.Windows.Forms.ToolStripMenuItem(MnuType_More)
            MnuChild.Name = MnuType_More
            MnuChild.ToolTipText = MnuType_More
            MnuSort.DropDownItems.Add(MnuChild)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ProcCreateRemoveSort(ByRef MnChkSortCol As ToolStripMenuItem)
        Dim MnRemoveSortCol As ToolStripMenuItem
        Dim I As Integer = 0
        Dim bConStr$ = ""
        Try
            With FrmObj.DglSort
                For I = 0 To .Rows.Count - 1
                    If .Item(FrmRepFormat.ColFieldName, I).Value <> "" Then
                        MnRemoveSortCol = New ToolStripMenuItem("Remove Sort " & .Item(FrmRepFormat.ColFieldName, I).Value.ToString & " (" & .Item(FrmRepFormat.Col2AscDsc, I).Value.ToString & ")")
                        MnRemoveSortCol.Tag = I
                        MnRemoveSortCol.ToolTipText = "Remove Sort"
                        MnChkSortCol.DropDownItems.Add(MnRemoveSortCol)
                    End If
                Next
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ProcFillFilterMnu()
        Dim MnuChild As ToolStripMenuItem
        Try
            MnuFilter.DropDownItems.Clear()
            If DGL1.CurrentCell Is Nothing Then
                mColumnIndex = 0
                mRowIndex = 0
            Else
                mColumnIndex = DGL1.CurrentCell.ColumnIndex
                mRowIndex = DGL1.CurrentCell.RowIndex

                Call ProcCreateFilterMnu(mColumnIndex, mRowIndex, "=")
                Call ProcCreateFilterMnu(mColumnIndex, mRowIndex, "<>")
                Call ProcCreateFilterMnu(mColumnIndex, mRowIndex, "<")
                Call ProcCreateFilterMnu(mColumnIndex, mRowIndex, ">")

            End If

            ProcFillRemoveFilter(MnuFilter)

            MnuChild = New System.Windows.Forms.ToolStripMenuItem(FilterType_RemoveAllFilter)
            MnuChild.Name = FilterType_RemoveAllFilter
            MnuChild.ToolTipText = FilterType_RemoveAllFilter
            MnuFilter.DropDownItems.Add(MnuChild)

            MnuChild = New System.Windows.Forms.ToolStripMenuItem(MnuType_More)
            MnuChild.Name = MnuType_More
            MnuChild.ToolTipText = MnuType_More
            MnuFilter.DropDownItems.Add(MnuChild)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ProcFillRemoveFilter(ByRef MnChkFilterCol As ToolStripMenuItem)
        Dim MnRemoveFilterCol As ToolStripMenuItem
        Dim I As Integer = 0
        Dim bConStr$ = ""
        Try
            With FrmObj.DglFilter
                For I = 0 To .Rows.Count - 1
                    If .Item(FrmRepFormat.ColFieldName, I).Value <> "" Then
                        MnRemoveFilterCol = New ToolStripMenuItem((FilterType_RemoveFilter & " " & .Item(FrmRepFormat.ColFieldName, I).Value & " " & .AgSelectedValue(FrmRepFormat.ColFilterOperator, I) & " " & .Item(FrmRepFormat.ColValue1, I).Value.ToString).ToString)
                        MnRemoveFilterCol.Tag = I
                        MnRemoveFilterCol.ToolTipText = FilterType_RemoveFilter
                        MnChkFilterCol.DropDownItems.Add(MnRemoveFilterCol)
                    End If
                Next
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub CMSVisible_DropDownItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles _
                MnuVisible.DropDownItemClicked, MnuSort.DropDownItemClicked,
                MnuGroupOn.DropDownItemClicked, MnuFilter.DropDownItemClicked, MnuSaveSettings.DropDownItemClicked
        Try
            Select Case sender.Name
                Case MnuVisible.Name
                    If e.ClickedItem.Text = MnuType_More Then
                        FrmObj.TCMain.SelectedTab = FrmObj.TPDisplaySet
                        FrmObj.ShowDialog()
                    Else
                        DGL1.Columns(e.ClickedItem.Name).Visible = Not CType(e.ClickedItem, System.Windows.Forms.ToolStripMenuItem).Checked
                        DGL2.Columns(DGL1.Columns(e.ClickedItem.Name).Index).Visible = Not CType(e.ClickedItem, System.Windows.Forms.ToolStripMenuItem).Checked
                        Call FrmObj.ProcFillVisibleGrids(FunRetVisibleColumnList())
                    End If

                Case MnuSort.Name
                    Select Case e.ClickedItem.ToolTipText
                        Case SortType_SortAsc
                            With FrmObj.DglSort
                                .Rows.Add()
                                .Item(FrmRepFormat.ColFieldName, .Rows.Count - 2).Value = DGL1.Columns(DGL1.CurrentCell.ColumnIndex).Name
                                .Item(FrmRepFormat.ColFieldName, .Rows.Count - 2).Tag = DGL1.Columns(DGL1.CurrentCell.ColumnIndex).Name
                                .Item(FrmRepFormat.Col2AscDsc, .Rows.Count - 2).Value = "Asc"
                            End With

                        Case SortType_SortDesc
                            With FrmObj.DglSort
                                .Rows.Add()
                                .Item(FrmRepFormat.ColFieldName, .Rows.Count - 2).Value = DGL1.Columns(DGL1.CurrentCell.ColumnIndex).Name
                                .Item(FrmRepFormat.Col2AscDsc, .Rows.Count - 2).Value = "Desc"
                            End With

                        Case SortType_RemoveSort
                            FrmObj.DglSort.Rows.RemoveAt(e.ClickedItem.Tag)

                        Case MnuType_More
                            FrmObj.TCMain.SelectedTab = FrmObj.TPSortSet
                            FrmObj.ShowDialog()
                    End Select
                    Call ProcSortGrid()

                Case MnuFilter.Name
                    Select Case e.ClickedItem.ToolTipText
                        Case FilterType_Filter
                            With FrmObj.DglFilter
                                .Rows.Add()
                                .AgSelectedValue(FrmRepFormat.ColDataType, .Rows.Count - 2) = FunRetDataType(DGL1.Item(DGL1.CurrentCell.ColumnIndex, DGL1.CurrentCell.RowIndex).ValueType.ToString)
                                .Item(FrmRepFormat.ColFieldName, .Rows.Count - 2).Value = DGL1.Columns(DGL1.CurrentCell.ColumnIndex).Name
                                .AgSelectedValue(FrmRepFormat.ColFilterOperator, .Rows.Count - 2) = e.ClickedItem.Tag
                                .Item(FrmRepFormat.ColValue1, .Rows.Count - 2).Value = DGL1.Item(DGL1.CurrentCell.ColumnIndex, DGL1.CurrentCell.RowIndex).Value
                            End With
                            Call ProcFilterGrid()

                        Case FilterType_RemoveFilter
                            FrmObj.DglFilter.Rows.RemoveAt(e.ClickedItem.Tag)
                            Call ProcFilterGrid()

                        Case FilterType_RemoveAllFilter
                            FrmObj.DglFilter.Rows.Clear()
                            Call ProcFilterGrid()

                        Case MnuType_More
                            FrmObj.TCMain.SelectedTab = FrmObj.TpFilterSetting
                            FrmObj.ShowDialog()
                    End Select

                Case MnuSaveSettings.Name
                    Select Case e.ClickedItem.Name
                        Case MnuSaveDisplaySettings.Name
                            Call AgCL.GridSetiingWriteXml(Me.Text + "-Visible", DGL1)
                            Call FSaveReprtDisplaySettings(mReportName, mReportFormatName, DGL1)
                            MsgBox("Disiplay Settings Saved...!", MsgBoxStyle.Information)

                        Case MnuSaveSortingSettings.Name
                            Call ProcSaveSortSettings(Me.Text + "-Sort")

                        Case MnuSaveFilterSettings.Name
                            Call ProcSaveFilterSettings(Me.Text + "-Filter")
                    End Select
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function FunGetFilterConStr() As String
        Dim I As Integer = 0
        Dim bConStr$ = ""
        Try
            With FrmObj.DglFilter
                For I = 0 To .Rows.Count - 1
                    If .Item(FrmRepFormat.ColFieldName, I).Value <> "" Then
                        bConStr &= IIf(bConStr <> "", " And ", "") & "[" & .Item(FrmRepFormat.ColFieldName, I).Value & "]" & .AgSelectedValue(FrmRepFormat.ColFilterOperator, I) & FunFormatField(.Item(FrmRepFormat.ColValue1, I).Value.ToString)
                    End If
                Next
            End With
            FunGetFilterConStr = bConStr
            If bConStr <> "" And TypingFilter <> "" Then
                FunGetFilterConStr = bConStr + " And " + TypingFilter
            ElseIf TypingFilter <> "" And bConStr = "" Then
                FunGetFilterConStr = TypingFilter
            ElseIf bConStr <> "" And TypingFilter = "" Then
                FunGetFilterConStr = bConStr
            ElseIf bConStr = "" And TypingFilter = "" Then
                FunGetFilterConStr = ""
            End If
        Catch ex As Exception
            FunGetFilterConStr = ""
            MsgBox(ex.Message)
        End Try
    End Function

    Private Sub ProcFilterGrid()
        DsMaster.Tables(0).DefaultView.RowFilter = FunGetFilterConStr()
        ProcApplyAggregateFunction()
        RaiseEvent FilterApplied()
    End Sub

    Private Function FunRetDataType(ByVal Value As String) As ColumnDataType
        Try
            Select Case UCase(Value)
                Case "SYSTEM.INT32", "SYSTEM.DECIMAL", "SYSTEM.DOUBLE", "SYSTEM.INT64"
                    FunRetDataType = ColumnDataType.NumberType

                Case "SYSTEM.DATETIME"
                    FunRetDataType = ColumnDataType.DateTimeType

                Case Else
                    FunRetDataType = ColumnDataType.StringType
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function

    Private Sub ProcCreateFilterMnu(ByVal mColumnIndex As Integer, ByVal mRowIndex As Integer, ByVal bOperator As String)
        Dim MnuChild As ToolStripMenuItem
        Try
            MnuChild = New System.Windows.Forms.ToolStripMenuItem((DGL1.Columns(mColumnIndex).HeaderText & " " & bOperator & " " & DGL1.Item(mColumnIndex, mRowIndex).Value.ToString).ToString)
            MnuFilter.DropDownItems.Add(MnuChild)
            MnuChild.Name = "[" & DGL1.Columns(mColumnIndex).HeaderText & "] " & bOperator & FunFormatField(DGL1.Item(mColumnIndex, mRowIndex).Value.ToString)
            MnuChild.Tag = bOperator
            MnuChild.ToolTipText = FilterType_Filter
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function FunFormatField(ByVal bValue As Object) As Object
        Try
            Select Case FunRetDataType(bValue.GetType.ToString)
                Case ColumnDataType.NumberType
                    FunFormatField = bValue

                Case Else
                    FunFormatField = "'" & bValue & "'"
            End Select
        Catch ex As Exception
            FunFormatField = Nothing
            MsgBox(ex.Message)
        End Try
    End Function

    Private Sub FrmObj_BaseEvent_BtnOkClick() Handles FrmObj.BaseEvent_BtnOkClick
        Try
            Call ProcFillVisibleColumnMenuFromVisibleGrid()
            Call ProcSortGrid()
            Call ProcFilterGrid()
            Call ProcApplyAggregateFunction()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub ProcApplyAggregateFunction()
        Dim I As Integer = 0
        Try
            With DGL1
                For I = 0 To .Columns.Count - 1
                    If mIsManualAggregate = True Then
                        If (FunRetDataType(DGL1.Item(I, 0).Value.GetType.ToString) = ColumnDataType.NumberType Or FunRetColumnDataType(I) = ColumnDataType.NumberType) Then DGL2.Item(I, 0).Style.Alignment = DataGridViewContentAlignment.BottomRight
                    Else
                        DGL2.Item(I, 0).Value = ""
                        If (FunRetDataType(DGL1.Item(I, 0).Value.GetType.ToString) = ColumnDataType.NumberType Or FunRetColumnDataType(I) = ColumnDataType.NumberType) And Not DGL1.Columns(I).Name.Contains("Rate") Then
                            DGL2.Item(I, 0).Value = DsMaster.Tables(0).Compute("Sum([" & .Columns(I).HeaderText & "])", FunGetFilterConStr())
                        End If
                        If (FunRetDataType(DGL2.Item(I, 0).Value.GetType.ToString) = ColumnDataType.NumberType Or FunRetColumnDataType(I) = ColumnDataType.NumberType) Then
                            DGL2.Item(I, 0).Style.Alignment = DataGridViewContentAlignment.BottomRight
                        End If
                        If .Columns(I).Tag IsNot Nothing AndAlso .Columns(I).Tag <> "" Then
                            DGL2.Item(I, 0).Value = DsMaster.Tables(0).Compute(.Columns(I).Tag & "([" & .Columns(I).HeaderText & "])", FunGetFilterConStr())
                        End If
                    End If
                Next
            End With
        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try
    End Sub
    Private Function FunRetColumnDataType(ColumnIndex As Integer)
        Try
            Select Case DGL1.Columns(ColumnIndex).ValueType.ToString.ToUpper
                Case "SYSTEM.INT32", "SYSTEM.DECIMAL", "SYSTEM.DOUBLE", "SYSTEM.INT64"
                    FunRetColumnDataType = ColumnDataType.NumberType

                Case "SYSTEM.DATETIME"
                    FunRetColumnDataType = ColumnDataType.DateTimeType

                Case Else
                    FunRetColumnDataType = ColumnDataType.StringType
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function
    Private Sub ProcSortGrid()
        Dim MnuChild As ToolStripMenuItem
        Dim I As Integer = 0
        Try
            MnuSort.DropDownItems.Clear()
            DGL1.DataSource.DefaultView.Sort = ""

            With FrmObj.DglSort
                For I = 0 To .Rows.Count - 1
                    If .Item(FrmRepFormat.ColFieldName, I).Value <> "" Then
                        If DGL1.Columns.Contains(AgL.XNull(.Item(FrmRepFormat.ColFieldName, I).Tag).ToString) = True Then
                            DGL1.DataSource.DefaultView.Sort &= IIf(DGL1.DataSource.DefaultView.Sort <> "", ",", "") & .Item(FrmRepFormat.ColFieldName, I).Tag.ToString & " " & .Item(FrmRepFormat.Col2AscDsc, I).Value.ToString
                        End If
                    End If
                Next
            End With

            MnuChild = New ToolStripMenuItem(MnuType_More)
            MnuChild.Name = MnuType_More
            MnuSort.DropDownItems.Add(MnuChild)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub ProcShowSortSettings(ByVal File_Name As String)
        Dim i As Integer
        Dim bReader As XmlTextReader
        Try
            If File.Exists(My.Application.Info.DirectoryPath & "\Setting\" & File_Name & ".xml") = False Then Exit Sub
            bReader = New XmlTextReader(My.Application.Info.DirectoryPath & "\Setting\" & File_Name & ".xml")
            bReader.WhitespaceHandling = WhitespaceHandling.None
            bReader.Read()
            bReader.Read()
            While Not bReader.EOF
                bReader.Read()
                If Not bReader.IsStartElement() Then
                    Exit While
                End If
                bReader.Read()
                FrmObj.DglSort.Rows.Add()
                FrmObj.DglSort.Item(FrmRepFormat.ColFieldName, i).Value = bReader.ReadElementString("FieldName")
                FrmObj.DglSort.Item(FrmRepFormat.Col2AscDsc, i).Value = bReader.ReadElementString("AscDesc")
                FrmObj.DglSort.Item(FrmRepFormat.ColFieldName, i).Tag = FrmObj.DglSort.Item(FrmRepFormat.ColFieldName, i).Value
                i = i + 1
            End While
            bReader.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub ProcSaveSortSettings(ByVal File_Name As String)
        Try
            Dim i As Integer
            Dim settings As XmlWriterSettings = New XmlWriterSettings()
            settings.Indent = True
            If My.Computer.FileSystem.DirectoryExists(My.Application.Info.DirectoryPath & "\Setting") = False Then
                My.Computer.FileSystem.CreateDirectory(My.Application.Info.DirectoryPath & "\Setting")
            End If

            Using writer As XmlWriter = XmlWriter.Create(My.Application.Info.DirectoryPath & "\Setting\" & File_Name & ".xml", settings)
                writer.WriteStartDocument()
                writer.WriteStartElement("SaveSortSettings")
                With FrmObj.DglSort
                    For i = 0 To .Rows.Count - 1
                        If .Item(FrmRepFormat.ColFieldName, i).Value <> "" Then
                            writer.WriteStartElement("Column")
                            writer.WriteElementString("FieldName", .Item(FrmRepFormat.ColFieldName, i).Tag.ToString)
                            writer.WriteElementString("AscDesc", .Item(FrmRepFormat.Col2AscDsc, i).Value.ToString)
                            writer.WriteEndElement()
                        End If
                    Next
                    writer.WriteEndElement()
                    writer.WriteEndDocument()
                End With
            End Using
            MsgBox("Sort Settings Saves...!", MsgBoxStyle.Information)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub ProcShowFilterSettings(ByVal File_Name As String)
        Dim i As Integer
        Dim bReader As XmlTextReader
        Try
            If File.Exists(My.Application.Info.DirectoryPath & "\Setting\" & File_Name & ".xml") = True Then
                bReader = New XmlTextReader(My.Application.Info.DirectoryPath & "\Setting\" & File_Name & ".xml")
                bReader.WhitespaceHandling = WhitespaceHandling.None
                bReader.Read()
                bReader.Read()
                While Not bReader.EOF
                    bReader.Read()
                    If Not bReader.IsStartElement() Then
                        Exit While
                    End If
                    bReader.Read()
                    FrmObj.DglFilter.Rows.Add()
                    FrmObj.DglFilter.AgSelectedValue(FrmRepFormat.ColDataType, i) = bReader.ReadElementString("DataType")
                    FrmObj.DglFilter.Item(FrmRepFormat.ColFieldName, i).Value = bReader.ReadElementString("FieldName")
                    FrmObj.DglFilter.AgSelectedValue(FrmRepFormat.ColFilterOperator, i) = bReader.ReadElementString("FilterOperator")
                    FrmObj.DglFilter.Item(FrmRepFormat.ColValue1, i).Value = bReader.ReadElementString("Value1")
                    FrmObj.DglFilter.Item(FrmRepFormat.ColValue2, i).Value = bReader.ReadElementString("Value2")
                    i = i + 1
                End While
                bReader.Close()
            Else
                'If FiterSettingGridCopy_Arr.Count > 0 Then
                '    CopyDataGridViewFromOneToOther(FiterSettingGridCopy_Arr(FiterSettingGridCopy_Arr.Count - 1), FrmObj.DglFilter)
                'End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub ProcSaveFilterSettings(ByVal File_Name As String)
        Try
            Dim i As Integer
            Dim settings As XmlWriterSettings = New XmlWriterSettings()
            settings.Indent = True
            If My.Computer.FileSystem.DirectoryExists(My.Application.Info.DirectoryPath & "\Setting") = False Then
                My.Computer.FileSystem.CreateDirectory(My.Application.Info.DirectoryPath & "\Setting")
            End If

            Using writer As XmlWriter = XmlWriter.Create(My.Application.Info.DirectoryPath & "\Setting\" & File_Name & ".xml", settings)
                writer.WriteStartDocument()
                writer.WriteStartElement("SaveFilterSettings")
                With FrmObj.DglFilter
                    For i = 0 To .Rows.Count - 1
                        If .Item(FrmRepFormat.ColFieldName, i).Value <> "" Then
                            writer.WriteStartElement("Column")
                            writer.WriteElementString("DataType", .AgSelectedValue(FrmRepFormat.ColDataType, i))
                            writer.WriteElementString("FieldName", .Item(FrmRepFormat.ColFieldName, i).Value.ToString)
                            writer.WriteElementString("FilterOperator", .AgSelectedValue(FrmRepFormat.ColFilterOperator, i))
                            writer.WriteElementString("Value1", .Item(FrmRepFormat.ColValue1, i).Value)
                            writer.WriteElementString("Value2", .Item(FrmRepFormat.ColValue2, i).Value)
                            writer.WriteEndElement()
                        End If
                    Next
                    writer.WriteEndElement()
                    writer.WriteEndDocument()
                End With
            End Using
            MsgBox("Filter Settings Saves...!", MsgBoxStyle.Information)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FrmObj_BaseEvent_BtnSaveDisplayClick() Handles FrmObj.BaseEvent_BtnSaveDisplayClick
        Try
            Call AgCL.GridSetiingWriteXml(Me.Text + "-Visible", DGL1)
            MsgBox("Display Settings Saves...!", MsgBoxStyle.Information)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FrmObj_BaseEvent_BtnSaveFilterClick() Handles FrmObj.BaseEvent_BtnSaveFilterClick
        Call ProcSaveFilterSettings(Me.Text + "-Filter")
    End Sub

    Private Sub FrmObj_BaseEvent_BtnSaveSortClick() Handles FrmObj.BaseEvent_BtnSaveSortClick
        Call ProcSaveSortSettings(Me.Text + "-Sort")
    End Sub
    Private Sub DGL1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DGL1.KeyDown
        Try
            Dim mRowIndex As Integer = DGL1.CurrentCell.RowIndex
            Dim mColumnIndex As Integer = DGL1.CurrentCell.ColumnIndex

            If e.KeyCode = Keys.Enter Then
                FOpenNextFormat()
            Else
                Select Case DGL1.Columns(DGL1.CurrentCell.ColumnIndex).Name
                    Case "Tick"
                        If e.KeyCode = Keys.Space Then
                            If DGL1.CurrentCell.ColumnIndex = DGL1.Columns("Tick").Index Then
                                FManageTick(DGL1, DGL1.CurrentCell.ColumnIndex)
                            End If
                        End If
                End Select

                RaiseEvent Dgl1KeyDown(sender, e)
            End If
        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub DGL1_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGL1.CellDoubleClick
        FOpenNextFormat()

    End Sub
    Private Sub FOpenNextFormat()
        If DGL1.Columns.Contains("Search Code") = True Then
            Dim FilterGrid_Copy As New AgControls.AgDataGrid
            FilterGrid_Copy.AllowUserToAddRows = False
            CopyDataGridViewFromOneToOther(FilterGrid, FilterGrid_Copy)
            FiterGridCopy_Arr.Add(FilterGrid_Copy)
            FindTextArr.Add(TxtFind.Text)


            Dim FilterSettingGrid_Copy As New AgControls.AgDataGrid
            FilterSettingGrid_Copy.AllowUserToAddRows = False
            CopyDataGridViewFromOneToOther(FrmObj.DglFilter, FilterSettingGrid_Copy)
            FiterSettingGridCopy_Arr.Add(FilterSettingGrid_Copy)
            FrmObj.DglFilter.Rows.Clear()
            FrmObj.DglFilter.RowCount = 1

            TxtFind.Text = ""
            TypingFilter = ""
            FocusedRowIndexArr.Add(DGL1.CurrentCell.RowIndex)
            FocusedRowIndex = -1
            Dim Result$ = CStr(CallByName(mClsRep, mReportProcName, CallType.Method, FilterGrid, DGL1.CurrentRow))
            FAdjustFooter()
            FManagerFilterDisplayGrid(Flag_IsFilterOpen)

            If Me.MdiParent.ActiveMdiChild.Text <> Me.Text Then
                FProcessEscapeButton(False)
            End If
        End If
    End Sub
    Private Sub DGL1_ColumnDisplayIndexChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewColumnEventArgs) Handles DGL1.ColumnDisplayIndexChanged
        If DGL2.Columns.Count = DGL1.Columns.Count And DGL1.Columns.Count <> 0 Then
            DGL2.Columns(e.Column.Index).DisplayIndex = e.Column.DisplayIndex
            'Call FrmObj.ProcFillVisibleGrids(FunRetVisibleColumnList())
        End If
    End Sub

    Private Sub ProcAssignColumnStructure()
        Dim SCFMain As StrucColumnFormating
        Dim I As Integer = 0
        Try
            With DGL1
                For I = 0 To .Columns.Count - 1
                    SCFMain.IntWidth = .Columns(I).Width
                    SCFMain.StrHideColumn = IIf(.Columns(I).Visible, "N", "Y")
                    SCFMain.StrWrapColumn = "N"
                    DGL1.Columns(I).HeaderCell.Tag = SCFMain
                Next
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub MnuExportToExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles MnuExportToExcel.Click, MnuPreview.Click, MnuEMail.Click
        Dim FileName As String
        'Try
        Select Case sender.Name
            Case MnuExportToExcel.Name
                'If MsgBox("Want to Export Grid Data", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Export Grid?...") = vbNo Then Exit Sub
                'FileName = AgControls.Export.GetFileName(My.Computer.FileSystem.SpecialDirectories.Desktop)
                'If FileName.Trim <> "" Then
                Call AgControls.Export.exportExcel(DGL1, FileName, DGL1.Handle)
                'End If

            Case MnuPreview.Name
                'Call ProcAssignColumnStructure()
                'Call ProcCrateTotals()
                'Dim FrmObj As New FrmSearchReport(DtMainWithTotals, Me)
                'FrmObj.MdiParent = Me.MdiParent
                'FrmObj.Show()
                'PreparePrintDocument()
                If DGL2.Visible = False And DGL2.Rows.Count > 0 Then
                    For I As Integer = 0 To DGL2.Columns.Count - 1
                        DGL2.Item(I, 0).Value = ""
                    Next
                End If
                Dim FrmObj As New FrmRepPrint(AgL)
                FrmObj.DGL1 = DGL1
                FrmObj.DGL2 = DGL2
                FrmObj.FilterGrid = FilterGrid
                'If DGL2.Visible = False Then
                '    FrmObj.ShowReportFooter = False
                'End If
                FrmObj.ReportTitle = AgL.PubDivPrintName
                If mReportSubTitle <> "" Then
                    FrmObj.ReportSubTitle = mReportSubTitle
                Else
                    FrmObj.ReportSubTitle = Me.Text
                End If
                FrmObj.reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout)
                FrmObj.reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent
                FrmObj.reportViewer1.ZoomPercent = 100
                FrmObj.ProcessPrint(FrmObj.reportViewer1)
                FrmObj.MdiParent = Me.MdiParent
                FrmObj.Show()
                FrmObj.reportViewer1.RefreshReport()

            Case MnuEMail.Name
                'Dim FrmObj As New FrmMailCompose(AgL)

                'Dim FrmRepPrint As New FrmRepPrint(AgL)
                'FrmRepPrint.DGL1 = DGL1
                'FrmRepPrint.FilterGrid = FilterGrid
                'FrmRepPrint.ProcessPrint(FrmObj.reportViewer1)

                'FrmObj.ReportTitle = Me.Text
                'FrmObj.AttachmentName = Me.Text
                'FrmObj.MdiParent = Me.MdiParent
                'FrmObj.reportViewer1.Visible = True
                'FrmObj.reportViewer1.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local
                'FrmObj.reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout)
                'FrmObj.reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent
                'FrmObj.reportViewer1.ZoomPercent = 50
                'FrmObj.Show()
                'FrmObj.reportViewer1.RefreshReport()
                Dim FrmObj As New FrmMailCompose(AgL)

                Dim FrmRepPrint As New FrmRepPrint(AgL)
                FrmRepPrint.DGL1 = DGL1
                FrmRepPrint.DGL2 = DGL2
                FrmRepPrint.FilterGrid = FilterGrid
                FrmRepPrint.ReportTitle = AgL.PubDivPrintName
                If mReportSubTitle <> "" Then
                    FrmRepPrint.ReportSubTitle = mReportSubTitle
                Else
                    FrmRepPrint.ReportSubTitle = Me.Text
                End If
                FrmRepPrint.ProcessPrint(FrmObj.reportViewer1)

                'FrmObj.ReportTitle = Me.Text
                FrmObj.AttachmentName = Me.Text
                FrmObj.MdiParent = Me.MdiParent
                FrmObj.reportViewer1.Visible = True
                FrmObj.reportViewer1.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local
                FrmObj.reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout)
                FrmObj.reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent
                FrmObj.reportViewer1.ZoomPercent = 50
                FrmObj.Show()
                FrmObj.reportViewer1.RefreshReport()
        End Select
        'Catch ex As Exception
        '    MsgBox(ex.Message)
        'End Try
    End Sub

    Private Sub ProcCrateTotals()
        Dim I As Integer = 0
        Dim TempRow As DataRow = Nothing
        Dim TempTable As New DataTable
        Dim ColumnName As DataColumn

        Try
            DtMainWithTotals = DsMaster.Tables(0).Copy
            With DGL1
                For I = 0 To .Columns.Count - 1
                    ColumnName = New DataColumn(.Columns(I).Name, .Columns(I).ValueType)
                    TempTable.Columns.Add(ColumnName)
                Next
            End With

            TempRow = TempTable.NewRow()

            With DGL2
                For I = 0 To .Columns.Count - 1
                    Try
                        TempRow.Item(I) = .Item(I, 0).Value
                    Catch ex As Exception
                    End Try
                Next
            End With
            TempTable.Rows.Add(TempRow)
            DtMainWithTotals.ImportRow(TempRow)
        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try
    End Sub

    Private Sub RbtLeftToRightSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Select Case sender.Name
                'Case RbtComprehensiveSearch.Name
                '    DGL1.GridSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple

                'Case RbtLeftToRightSearch.Name
                '    DGL1.GridSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub BtnFill_Click(sender As Object, e As EventArgs) Handles BtnFill.Click, BtnFilter.Click, BtnCustomMenu.Click
        Try
            Select Case sender.Name
                Case BtnFilter.Name
                    FManageFilterVisibility()

                Case BtnFill.Name
                    RaiseEvent ProcessReport()

                Case BtnCustomMenu.Name
                    MnuCustomOption.Show(BtnCustomMenu, New Point(0, BtnCustomMenu.Height))
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub FManageFilterVisibility()
        If Flag_IsFilterOpen = False Then
            FilterGrid.Height = 500
            DGL1.Top = DGL1.Top + (FilterGrid.Height + 10)
            DGL1.Height = DGL1.Height - (FilterGrid.Height + 10)
            If FilterGrid.Visible = True Then
                If FilterGrid.Rows.Count > 0 Then
                    FilterGrid.CurrentCell = FilterGrid.Item(GFilter, FilterGrid.FirstDisplayedCell.RowIndex)
                    FilterGrid.Focus()
                End If
            End If
            Flag_IsFilterOpen = True
            FManagerFilterDisplayGrid(Flag_IsFilterOpen)
        Else
            DGL1.Top = DGL1.Top - (FilterGrid.Height + 10)
            DGL1.Height = DGL1.Height + (FilterGrid.Height + 10)
            FilterGrid.Height = 0
            Flag_IsFilterOpen = False
            FManagerFilterDisplayGrid(Flag_IsFilterOpen)
        End If
    End Sub
    Private Sub FManageFindTextboxVisibility()
        If TxtFind.Text = "" Then TxtFind.Visible = False : TxtFind.Visible = True
    End Sub
    Private Sub Dgl1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles DGL1.KeyPress
        Try
            If mIsAllowFind = False Then Exit Sub

            If e.KeyChar = vbCr Or e.KeyChar = vbCrLf Or e.KeyChar = vbTab Or e.KeyChar = ChrW(27) Then Exit Sub

            If DGL1.CurrentCell IsNot Nothing Then
                If DGL1.Columns(DGL1.CurrentCell.ColumnIndex).Name = "Tick" Then Exit Sub
                If mInputColumnsStr.Contains(DGL1.Columns(DGL1.CurrentCell.ColumnIndex).Name) Then Exit Sub
                fld = DGL1.Columns(DGL1.CurrentCell.ColumnIndex).Name
            End If

            If DGL1.CurrentCell Is Nothing Then
                DsMaster.Tables(0).DefaultView.RowFilter = Nothing
                TypingFilter = ""
            End If

            If Asc(e.KeyChar) = Keys.Back Then
                If TxtFind.Text <> "" Then TxtFind.Text = Microsoft.VisualBasic.Left(TxtFind.Text, Len(TxtFind.Text) - 1)
            End If

            FManageFindTextboxVisibility()

            TxtFind_KeyPress(TxtFind, e)

            DGL1.CurrentCell = DGL1.Item(fld, 0)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub TxtFind_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtFind.KeyPress
        Try
            RowsFilter(HlpSt, DGL1, sender, e, fld, DsMaster.Tables(0))
        Catch ex As Exception
        End Try
    End Sub

    Private Function RowsFilter(ByVal selStr As String, ByVal CtrlObj As Object, ByVal TXT As TextBox, ByVal e As System.Windows.Forms.KeyPressEventArgs, ByVal FndFldName As String, ByVal DTable As DataTable) As Integer
        Try
            Dim strExpr As String, findStr As String, bSelStr As String = ""
            Dim sa As String
            Dim IntRow As Integer
            Dim i As Integer
            sa = TXT.Text
            bSelStr = selStr

            If sa.Length = 0 And Asc(e.KeyChar) = 8 Then IntRow = 0 : CtrlObj.CurrentCell = CtrlObj(FndFldName, IntRow) : DsMaster.Tables(0).DefaultView.RowFilter = Nothing : TypingFilter = "" : DGL1.CurrentCell = DGL1(FndFldName, 0) : ProcFilterGrid() : Exit Function
            If TXT.Text = "(null)" Then
                findStr = e.KeyChar
            Else
                findStr = IIf(Asc(e.KeyChar) = Keys.Back Or Asc(e.KeyChar) = 4 Or Asc(e.KeyChar) = 19, TXT.Text, TXT.Text + e.KeyChar)
            End If
            strExpr = "ltrim(Convert([" & FndFldName & "], 'System.String'))  like '%" & findStr & "%' "
            i = InStr(selStr, "where", CompareMethod.Text)
            If i = 0 Then
                selStr = selStr + " where " + strExpr + "order by [" & FndFldName & "]"
            Else
                selStr = selStr + " and " + strExpr + "order by [" & FndFldName & "]"
            End If

            ''==================================< Filter DTFind For Searching >====================================================
            DsMaster.Tables(0).DefaultView.RowFilter = Nothing
            TypingFilter = ""
            'DsMaster.Tables(0).DefaultView.RowFilter = " [" & FndFldName & "] like '%" & findStr & "%' "
            ProcFilterGrid()
            If DsMaster.Tables(0).DefaultView.RowFilter <> "" And DsMaster.Tables(0).DefaultView.RowFilter <> Nothing Then
                DsMaster.Tables(0).DefaultView.RowFilter += " And " + " Convert([" & FndFldName & "], 'System.String') like '" & findStr & "%' "
            Else
                DsMaster.Tables(0).DefaultView.RowFilter += " Convert([" & FndFldName & "], 'System.String') like '" & findStr & "%' "
            End If
            TypingFilter = DsMaster.Tables(0).DefaultView.RowFilter
            Try
                DGL1.CurrentCell = DGL1(FndFldName, 0)
            Catch ex As Exception
            End Try
            TXT.Text = TXT.Text + IIf(Asc(e.KeyChar) = Keys.Back Or Asc(e.KeyChar) = 4 Or Asc(e.KeyChar) = 19, "", e.KeyChar)

            FManageFindTextboxVisibility()
            ProcApplyAggregateFunction()
            RaiseEvent FilterApplied()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function

    Private Sub DGL1_Click(sender As Object, e As EventArgs) Handles DGL1.Click
        TxtFind.Text = ""
        FManageFindTextboxVisibility()
    End Sub

    Private Sub DGL1_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles DGL1.PreviewKeyDown
        If e.KeyCode = Keys.Delete Then TxtFind.Text = "" : FManageFindTextboxVisibility() : DsMaster.Tables(0).DefaultView.RowFilter = Nothing : TypingFilter = "" : DGL1.CurrentCell = DGL1(fld, 0) : DsMaster.Tables(0).DefaultView.RowFilter = Nothing : TypingFilter = "" : ProcFilterGrid()
        'If e.KeyCode = Keys.Left Or e.KeyCode = Keys.Right Or e.KeyCode = Keys.Up Or e.KeyCode = Keys.Down Then
        '    TxtFind.Text = ""
        '    FManageFindTextboxVisibility()
        '    ProcFilterGrid()
        'End If
    End Sub

    Public Sub CreateHelpGrid(ByVal FieldCode As String, ByVal FieldName As String,
                           ByVal FieldFilterDataType As FieldFilterDataType,
                           ByVal FieldDataType As FieldDataType,
                           ByVal DMHGQuery As String,
                           Optional ByVal DefaultValue As String = "All",
                           Optional ByVal DMHGHeight As Integer = 400,
                           Optional ByVal DMHGWidth As Integer = 400,
                           Optional ByVal ColWidth As Integer = 200,
                           Optional ByVal ColAlignment As DataGridViewContentAlignment = DataGridViewContentAlignment.NotSet,
                           Optional ByVal DisplayOnReport As Boolean = True)

        Try
            Dim mRow As Integer
            Dim dtDefualtData As DataTable
            ReDim Preserve FRH_Single(FilterGrid.Rows.Count)
            ReDim Preserve FRH_Multiple(FilterGrid.Rows.Count)

            FilterGrid.Rows.Add()
            FSetValue(FilterGrid.Rows.Count - 1, FieldCode, FieldName, FieldDataType, FieldFilterDataType, DMHGQuery, DefaultValue, DisplayOnReport, DMHGHeight, DMHGWidth, ColWidth, ColAlignment)


            dtDefualtData = AgL.FillData("Select * From ReportFilterDefaultValues Where MenuText='" & Me.Text & "' And User_Name = '" & AgL.PubUserName & "' And Head = '" & FilterGrid.Item("FieldName", FilterGrid.Rows.Count - 1).Value & "' ", AgL.GCn).Tables(0)
            If dtDefualtData.Rows.Count = 0 Then
                dtDefualtData = AgL.FillData("Select * From ReportFilterDefaultValues Where MenuText='" & Me.Text & "' And User_Name Is Null And Head = '" & FilterGrid.Item("FieldName", FilterGrid.Rows.Count - 1).Value & "' ", AgL.GCn).Tables(0)
            End If
            If dtDefualtData.Rows.Count > 0 Then
                FilterGrid.Item("Filter", FilterGrid.Rows.Count - 1).Value = AgL.XNull(dtDefualtData.Rows(0)("Value"))
                FilterGrid.Item("FilterCode", FilterGrid.Rows.Count - 1).Value = Replace(AgL.XNull(dtDefualtData.Rows(0)("ValueCode")), "`", "'")

                mRow = FilterGrid.Rows.Count - 1
                If FieldDataType = FGDataType.DT_Date Then
                    If FilterGrid.Item(GFilter, mRow).Value = "Today" Then
                        FilterGrid.Item(GFilter, mRow).Value = AgL.RetDate(AgL.PubLoginDate)
                    ElseIf FilterGrid.Item(GFilter, mRow).Value = "Yesterday" Then
                        FilterGrid.Item(GFilter, mRow).Value = AgL.RetDate(DateAdd(DateInterval.Day, -1, CDate(AgL.PubLoginDate)))
                    ElseIf FilterGrid.Item(GFilter, mRow).Value = "Month Start Date" Then
                        FilterGrid.Item(GFilter, mRow).Value = AgL.RetMonthStartDate(AgL.PubLoginDate)
                    ElseIf FilterGrid.Item(GFilter, mRow).Value = "Month End Date" Then
                        FilterGrid.Item(GFilter, mRow).Value = AgL.RetMonthEndDate(AgL.PubLoginDate)
                    ElseIf FilterGrid.Item(GFilter, mRow).Value = "Last Month Start Date" Then
                        FilterGrid.Item(GFilter, mRow).Value = AgL.RetMonthStartDate(DateAdd(DateInterval.Month, -1, CDate(AgL.PubLoginDate)))
                    ElseIf FilterGrid.Item(GFilter, mRow).Value = "Last Month End Date" Then
                        FilterGrid.Item(GFilter, mRow).Value = AgL.RetMonthEndDate(DateAdd(DateInterval.Month, -1, CDate(AgL.PubLoginDate)))
                    ElseIf FilterGrid.Item(GFilter, mRow).Value = "Year Start Date" Then
                        FilterGrid.Item(GFilter, mRow).Value = AgL.PubStartDate
                    ElseIf FilterGrid.Item(GFilter, mRow).Value = "Year End Date" Then
                        FilterGrid.Item(GFilter, mRow).Value = AgL.PubEndDate
                    End If
                End If

            End If


            FilterGrid(GFilter, 0).Selected = True
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
        End Try
    End Sub

    Private Sub FSetValue(ByVal IntRow As Int16, ByVal StrFieldCode As String, ByVal StrFieldName As String,
    ByVal BytDataType As FGDataType, ByVal FCTType As FilterCodeType, ByVal strHelpQuery As String, Optional ByVal StrDefaultValue As String = "",
    Optional ByVal BlnDisplayOnReport As Boolean = True, Optional ByVal intHgHeight As Integer = 400, Optional ByVal intHgWidth As Integer = 400, Optional ByVal intColWidth As Integer = 200, Optional ByVal ColAlignment As DataGridViewContentAlignment = DataGridViewContentAlignment.NotSet)

        Dim BtnCell As DataGridViewButtonCell
        Dim StrArray() As String

        FilterGrid(GFieldCode, IntRow).Value = StrFieldCode
        FilterGrid(GFieldName, IntRow).Value = StrFieldName
        FilterGrid(GDataType, IntRow).Value = BytDataType
        FilterGrid(GFilterCodeDataType, IntRow).Value = FCTType
        FilterGrid(GHelpQuery, IntRow).Value = strHelpQuery
        FilterGrid(GHGHeight, IntRow).Value = intHgHeight
        FilterGrid(GHGWidth, IntRow).Value = intHgWidth
        FilterGrid(GHGColWidth, IntRow).Value = intColWidth
        FilterGrid(GHGColAlignment, IntRow).Value = ColAlignment

        If StrDefaultValue <> "" Then
            StrArray = Split(StrDefaultValue, "|")
            Select Case UCase(Trim(StrArray(0)))
                Case "[LOGINDATE]"
                    FilterGrid(GFilter, IntRow).Value = AgL.PubLoginDate
                Case "[STARTDATE]"
                    FilterGrid(GFilter, IntRow).Value = AgL.PubStartDate
                Case "[ENDDATE]"
                    FilterGrid(GFilter, IntRow).Value = AgL.PubEndDate
                Case Else
                    FilterGrid(GFilter, IntRow).Value = StrArray(0)
            End Select
            If UBound(StrArray) > 0 Then
                FilterGrid(GFilterCode, IntRow).Value = StrArray(1)
            End If

            '======== For Both Name, Code Pick From Variables ===========
            Select Case UCase(Trim(StrArray(0)))
                Case "[SITECODE]"
                    FilterGrid(GFilter, IntRow).Value = AgL.PubSiteName
                    FilterGrid(GFilterCode, IntRow).Value = AgL.PubSiteCode
                Case "[DIVISIONCODE]"
                    FilterGrid(GFilter, IntRow).Value = AgL.PubDivName
                    FilterGrid(GFilterCode, IntRow).Value = AgL.PubDivCode
            End Select

            If FCTType = FilterCodeType.DTString Then
                If FilterGrid(GFilterCode, IntRow).Value <> "" Then
                    If BytDataType = FGDataType.DT_Selection_Multiple Then FilterGrid(GFilterCode, IntRow).Value = "''" & FilterGrid(GFilterCode, IntRow).Value & "''"
                    If BytDataType = FGDataType.DT_Selection_Single Then FilterGrid(GFilterCode, IntRow).Value = "'" & FilterGrid(GFilterCode, IntRow).Value & "'"
                End If
            End If
        End If

        If BytDataType = FGDataType.DT_Selection_Multiple Or BytDataType = FGDataType.DT_Selection_Single Then
            BtnCell = New DataGridViewButtonCell
            BtnCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
            BtnCell.Style.SelectionBackColor = Color.WhiteSmoke
            BtnCell.Style.BackColor = Color.WhiteSmoke
            BtnCell.Style.ForeColor = Color.BlueViolet
            BtnCell.Style.Font = New Font("Webdings", 9, FontStyle.Regular)
            BtnCell.Value = "6"
            BtnCell.FlatStyle = FlatStyle.Popup
            FilterGrid(GButton, IntRow) = BtnCell
        End If
        If BlnDisplayOnReport Then
            FilterGrid(GDisplayOnReport, IntRow).Value = "þ"
        Else
            FilterGrid(GDisplayOnReport, IntRow).Value = "o"
        End If
    End Sub
    Public Sub Filter_IniGrid()
        AgL.AddAgDataGrid(FilterGrid, Pnl3)
        FilterGrid.ColumnHeadersVisible = False
        FilterGrid.AllowUserToAddRows = False
        FilterGrid.EnableHeadersVisualStyles = False
        FilterGrid.ScrollBars = ScrollBars.None
        FilterGrid.RowHeadersVisible = False
        'DGL3.ReadOnly = True
        FilterGrid.AllowUserToResizeColumns = False
        FilterGrid.BackgroundColor = Color.White
        FilterGrid.AgAllowFind = False

        AgL.AddTextColumn(FilterGrid, "FieldCode", 0, 0, "FieldCode", False, True, False)
        AgL.AddTextColumn(FilterGrid, "FieldName", 440, 0, "Field", True, True, False)
        AgL.AddTextColumn(FilterGrid, "Filter", 440, 0, "Filter", True, False, False)
        FilterGrid.Columns.Add("Button", "")
        FilterGrid.Columns(GButton).Width = 27
        FilterGrid.Columns(GButton).ReadOnly = True
        AgL.AddTextColumn(FilterGrid, "FilterCode", 0, 0, "FilterCode", False, True, False)
        AgL.AddTextColumn(FilterGrid, "FilterCodeDataType", 0, 0, "FilterCodeDataType", False, True, False)
        AgL.AddTextColumn(FilterGrid, "DataType", 0, 0, "DataType", False, True, False)
        AgL.AddTextColumn(FilterGrid, "", 25, 0, "", False, True, False)
        AgL.AddTextColumn(FilterGrid, "HelpQuery", 0, 0, "HelpQuery", False, True, False)
        AgL.AddTextColumn(FilterGrid, "HGHeight", 0, 0, "HGHeight", False, True, False)
        AgL.AddTextColumn(FilterGrid, "HGWidth", 0, 0, "HGWidth", False, True, False)
        AgL.AddTextColumn(FilterGrid, "ColWidth", 0, 0, "ColWidth", False, True, False)
        AgL.AddTextColumn(FilterGrid, "ColAlignment", 0, 0, "ColAlignment", False, True, False)
        FilterGrid.Anchor = (AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Bottom)
        FilterGrid.AllowUserToAddRows = False
        FilterGrid.BackgroundColor = Color.White
        FilterGrid.Columns(GFieldName).DefaultCellStyle.BackColor = Color.FromArgb(230, 230, 250)

        FilterGrid.DefaultCellStyle.Font = New Font("Arial", 9, FontStyle.Regular)
        FilterGrid.ColumnHeadersDefaultCellStyle.Font = New Font("Arial", 9, FontStyle.Regular)
        FilterGrid.Columns(GFieldName).DefaultCellStyle.Font = New Font("Arial", 9, FontStyle.Regular)
        FilterGrid.Columns(GDisplayOnReport).DefaultCellStyle.Font = New Font("wingdings", 12, FontStyle.Regular)
        FilterGrid.Columns(GDisplayOnReport).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        FilterGrid.Columns(GDisplayOnReport).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter

        FilterGrid.AgSkipReadOnlyColumns = True
        FilterGrid.Anchor = Pnl3.Anchor
        FilterGrid.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells)
    End Sub


    Private Sub DGL3_CellBeginEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles FilterGrid.CellBeginEdit
        Select Case Val(FilterGrid(GDataType, e.RowIndex).Value)
            Case FGDataType.DT_None, FGDataType.DT_Selection_Single, FGDataType.DT_Selection_Multiple
                e.Cancel = True
        End Select
    End Sub

    Private Sub DGL3_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles FilterGrid.CellEndEdit
        If FilterGrid(GDataType, e.RowIndex).Value = FGDataType.DT_Date Then
            FilterGrid(GFilter, e.RowIndex).Value = AgL.RetDate(FilterGrid(GFilter, e.RowIndex).Value)
        End If
    End Sub
    Public Function FGetText(ByVal Index As Integer) As Object
        FGetText = FilterGrid.Item(GFilter, Index).Value
    End Function
    Public Function FGetCode(ByVal Index As Integer) As Object
        FGetCode = FilterGrid.Item(GFilterCode, Index).Value
    End Function
    Public Function GetWhereCondition(ByVal FieldName As String, ByVal Index As Integer) As String
        If FilterGrid(GFilterCode, Index).Value = "''" Then
            GetWhereCondition = " And " & FieldName & " Is Null "
        ElseIf FilterGrid(GFilterCode, Index).Value <> "" Then
            GetWhereCondition = " And " & FieldName & " In (" & FilterGrid(GFilterCode, Index).Value & ")"
        Else
            GetWhereCondition = ""
        End If
    End Function
    Private Sub FHPGD_Show_Multiple(ByRef e As System.Windows.Forms.KeyEventArgs)
        Dim StrSendText As String
        Dim StrPrefix As String = "", StrSufix As String = "", StrSeprator As String = ""

        If Not AgL.FGrdDisableKeys(e) Then Exit Sub
        StrSendText = AgL.FSendText(FilterGrid, Chr(e.KeyCode))

        If Val(FilterGrid(GFilterCodeDataType, FilterGrid.CurrentCell.RowIndex).Value) = FilterCodeType.DTString Then
            StrPrefix = "'"
            StrSufix = "'"
            StrSeprator = ","
        ElseIf Val(FilterGrid(GFilterCodeDataType, FilterGrid.CurrentCell.RowIndex).Value) = FilterCodeType.DTNumeric Then
            StrPrefix = ""
            StrSufix = ""
            StrSeprator = ","
        End If

        FRH_Multiple(FilterGrid.CurrentCell.RowIndex).StartPosition = FormStartPosition.CenterScreen
        FRH_Multiple(FilterGrid.CurrentCell.RowIndex).ShowDialog()

        If FRH_Multiple(FilterGrid.CurrentCell.RowIndex).BytBtnValue = 0 Then
            FilterGrid(GFilter, FilterGrid.CurrentCell.RowIndex).Value = FRH_Multiple(FilterGrid.CurrentCell.RowIndex).FFetchData(2, "", "", ",")
            FilterGrid(GFilterCode, FilterGrid.CurrentCell.RowIndex).Value = FRH_Multiple(FilterGrid.CurrentCell.RowIndex).FFetchData(1, StrPrefix, StrSufix, StrSeprator, True)
        End If
    End Sub
    Private Sub FHPGD_Show_Single(ByRef e As System.Windows.Forms.KeyEventArgs)
        Dim StrSendText As String

        If Not AgL.FGrdDisableKeys(e) Then Exit Sub
        StrSendText = AgL.FSendText(FilterGrid, Chr(e.KeyCode))

        FRH_Single(FilterGrid.CurrentCell.RowIndex).StartPosition = FormStartPosition.CenterScreen
        FRH_Single(FilterGrid.CurrentCell.RowIndex).ShowDialog()

        If FRH_Single(FilterGrid.CurrentCell.RowIndex).BytBtnValue = 0 Then
            If Not FRH_Single(FilterGrid.CurrentCell.RowIndex).DRReturn.Equals(Nothing) Then
                FilterGrid(GFilter, FilterGrid.CurrentCell.RowIndex).Value = FRH_Single(FilterGrid.CurrentCell.RowIndex).DRReturn.Item(1)
                FilterGrid(GFilterCode, FilterGrid.CurrentCell.RowIndex).Value = FRH_Single(FilterGrid.CurrentCell.RowIndex).DRReturn.Item(0)
            End If
        End If
    End Sub
    Private Function FRefineSQLQuery(ByVal StrSQL As String) As String
        StrSQL = Replace(StrSQL, "[SITECODE]", AgL.PubSiteCode)
        Return StrSQL
    End Function

    Private Sub DGL1_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles DGL1.DataBindingComplete
        If mAllowAutoResizeRows Then
            If DGL1.Rows.Count < 500 Then
                DGL1.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells)
            Else
                DGL1.AutoResizeRows(DataGridViewAutoSizeRowsMode.DisplayedCells)
            End If
        End If
    End Sub

    Private Sub Dgl1_MouseUp(sender As Object, e As MouseEventArgs) Handles DGL1.MouseUp
        If DGL1.CurrentCell Is Nothing Then Exit Sub
        Dim mRowIndex As Integer = DGL1.CurrentCell.RowIndex
        Dim mColumnIndex As Integer = DGL1.CurrentCell.ColumnIndex
        Try
            Select Case DGL1.Columns(DGL1.CurrentCell.ColumnIndex).Name
                Case "Tick"
                    If e.Button = Windows.Forms.MouseButtons.Left Then
                        If DGL1.CurrentCell.ColumnIndex = DGL1.Columns("Tick").Index Then
                            FManageTick(DGL1, DGL1.CurrentCell.ColumnIndex)
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox("System Exception : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
        End Try
    End Sub
    Public Sub FManageTick(dgl As DataGridView, columnIndex As Integer)
        Dim I As Integer = 0
        Dim IntRowIndex As Integer = 0

        If dgl.CurrentCell.RowIndex < 0 Then Exit Sub
        For I = 0 To dgl.SelectedCells.Count - 1
            IntRowIndex = dgl.SelectedCells.Item(I).RowIndex
            If dgl.CurrentCell.ColumnIndex = columnIndex Then
                If dgl(columnIndex, IntRowIndex).Value = "þ" Then
                    dgl(columnIndex, IntRowIndex).Value = "o"
                Else
                    dgl(columnIndex, IntRowIndex).Value = "þ"
                End If
                RaiseEvent DGL1CheckedColumnValueChanged(dgl, columnIndex)
            End If
        Next
    End Sub
    Private Sub MnuCustomOption_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles MnuCustomOption.ItemClicked
        'If DGL1.Columns.Contains("Search Code") = True Then
        Dim DGL1_Copy As New AgControls.AgDataGrid
        CopySelectedDataGridViewRowFromOneToOther(DGL1, DGL1_Copy)
        Dim Result$ = CStr(CallByName(mClsRep, e.ClickedItem.Tag, CallType.Method, DGL1_Copy))

        'End If
    End Sub

    Private Function CreateRDLC() As String
        DsMaster.Tables(0).WriteXmlSchema("C:\Employee.xsd")
        Dim StringCode As String = ""

        StringCode = "<?xml version=" + "1.0" + " encoding=" + "utf-8" + "?>
                <Report xmlns=" + "http//schemas.microsoft.com/sqlserver/reporting/2008/01/reportdefinition" + " xmlns:rd=" + "http//schemas.microsoft.com/SQLServer/reporting/reportdesigner" + ">
                  <Body>
                    <ReportItems>
                      <Textbox Name=" + "Name" + ">
                        <CanGrow>true</CanGrow>
                        <KeepTogether>true</KeepTogether>
                        <Paragraphs>
                          <Paragraph>
                            <TextRuns>
                            </TextRuns>
                            <Style />
                          </Paragraph>
                        </Paragraphs>
                        <rd:DefaultName>Name</rd:DefaultName>
                        <Top>0.93625in</Top>
                        <Left>3.26958in</Left>
                        <Height>0.25in</Height>
                        <Width>1.53125in</Width>
                        <Style>
                          <Border>
                            <Style>None</Style>
                          </Border>
                          <PaddingLeft>2pt</PaddingLeft>
                          <PaddingRight>2pt</PaddingRight>
                          <PaddingTop>2pt</PaddingTop>
                          <PaddingBottom>2pt</PaddingBottom>
                        </Style>
                      </Textbox>
                    </ReportItems>
                    <Height>3.72917in</Height>
                    <Style />
                  </Body>
                  <Width>6.5in</Width>
                  <Page>
                    <LeftMargin>1in</LeftMargin>
                    <RightMargin>1in</RightMargin>
                    <TopMargin>1in</TopMargin>
                    <BottomMargin>1in</BottomMargin>
                    <Style />
                  </Page>
                  <AutoRefresh>0</AutoRefresh>
                  <DataSources>
                    <DataSource Name=" + "DsMaster" + ">
                      <ConnectionProperties>
                        <DataProvider>System.Data.DataSet</DataProvider>
                        <ConnectString>/* Local Connection */</ConnectString>
                      </ConnectionProperties>
                      <rd:DataSourceID>d6f9e529-b27b-4aae-953e-cbd6044b1df1</rd:DataSourceID>
                    </DataSource>
                  </DataSources>
                  <rd:ReportUnitType>Inch</rd:ReportUnitType>
                  <rd:ReportID>bc0efbf1-9e61-441d-9bc5-3b8f604c6388</rd:ReportID>
                </Report>"
        Return StringCode
    End Function

    Private Sub FrmRepDisplay_ResizeEnd(sender As Object, e As EventArgs) Handles Me.ResizeEnd
        Me.Dock = DockStyle.None
    End Sub

    Private Sub FilterGrid_MouseClick(sender As Object, e As MouseEventArgs) Handles FilterGrid.MouseClick
        If e.Button = MouseButtons.Right Then
            Dim currentMouseOverRow As Integer
            currentMouseOverRow = FilterGrid.HitTest(e.X, e.Y).RowIndex

            If currentMouseOverRow >= 0 Then
                MnuSave.Text = "Save [" + FilterGrid.Item("FieldName", currentMouseOverRow).Value + "] For Me"
                MnuSave.Tag = currentMouseOverRow
                MnuSaveForEveryoneToolStripMenuItem.Text = "Save [" + FilterGrid.Item("FieldName", currentMouseOverRow).Value + "] For Everyone"
                MnuSaveForEveryoneToolStripMenuItem.Tag = currentMouseOverRow
            End If

            MnuOptions.Show(FilterGrid, New Point(e.X, e.Y))
        End If

    End Sub

    Private Sub MnuSave_Click(sender As Object, e As EventArgs) Handles MnuSave.Click, MnuSaveForEveryoneToolStripMenuItem.Click
        'MsgBox("Data : " & FGMain.Item("Filter", CInt(sender.Tag)).Value + " / Code : " & FGMain.Item("FilterCode", CInt(sender.Tag)).Value)
        Dim mQry As String
        Dim mFilter As String

        mFilter = FilterGrid.Item(GFilter, CInt(sender.Tag)).Value
        If FilterGrid(GDataType, CInt(sender.Tag)).Value = FGDataType.DT_Date Then
            If FilterGrid.Item(GFilter, CInt(sender.Tag)).Value = AgL.RetDate(AgL.PubLoginDate) Then
                mFilter = "Today"
            ElseIf FilterGrid.Item(GFilter, CInt(sender.Tag)).Value = AgL.RetDate(DateAdd(DateInterval.Day, -1, CDate(AgL.PubLoginDate))) Then
                mFilter = "Yesterday"
            ElseIf FilterGrid.Item(GFilter, CInt(sender.Tag)).Value = AgL.RetMonthStartDate(AgL.PubLoginDate) Then
                mFilter = "Month Start Date"
            ElseIf FilterGrid.Item(GFilter, CInt(sender.Tag)).Value = AgL.RetMonthEndDate(AgL.PubLoginDate) Then
                mFilter = "Month End Date"
            ElseIf FilterGrid.Item(GFilter, CInt(sender.Tag)).Value = AgL.RetMonthStartDate(DateAdd(DateInterval.Month, -1, CDate(AgL.PubLoginDate))) Then
                mFilter = "Last Month Start Date"
            ElseIf FilterGrid.Item(GFilter, CInt(sender.Tag)).Value = AgL.RetMonthEndDate(DateAdd(DateInterval.Month, -1, CDate(AgL.PubLoginDate))) Then
                mFilter = "Last Month End Date"
            ElseIf FilterGrid.Item(GFilter, CInt(sender.Tag)).Value = AgL.PubStartDate Then
                mFilter = "Year Start Date"
            ElseIf FilterGrid.Item(GFilter, CInt(sender.Tag)).Value = AgL.PubEndDate Then
                mFilter = "Year End Date"
            End If
        End If


        Select Case sender.name
            Case MnuSave.Name
                mQry = "Delete From ReportFilterDefaultValues Where MenuText = '" & Me.Text & "' And User_Name ='" & AgL.PubUserName & "' And Head = '" & FilterGrid.Item("FieldName", CInt(sender.Tag)).Value & "' "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
                mQry = "Insert Into ReportFilterDefaultValues (MenuText, User_Name, Head, Value, ValueCode, EntryBy, EntryDate) 
                       Values('" & Me.Text & "', '" & AgL.PubUserName & "', '" & FilterGrid.Item("FieldName", CInt(sender.Tag)).Value & "', '" & mFilter & "', '" & Replace(FilterGrid.Item("FilterCode", CInt(sender.Tag)).Value, "'", "`") & "', '" & AgL.PubUserName & "', " & AgL.Chk_Date(AgL.PubLoginDate) & ") "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
            Case MnuSaveForEveryoneToolStripMenuItem.Name
                mQry = "Delete From ReportFilterDefaultValues Where MenuText = '" & Me.Text & "' And User_Name ='" & AgL.PubUserName & "' And Head = '" & FilterGrid.Item("FieldName", CInt(sender.Tag)).Value & "' "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
                mQry = "Delete From ReportFilterDefaultValues Where MenuText = '" & Me.Text & "' And User_Name Is Null And Head = '" & FilterGrid.Item("FieldName", CInt(sender.Tag)).Value & "' "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
                mQry = "Insert Into ReportFilterDefaultValues (MenuText, User_Name, Head, Value, ValueCode, EntryBy, EntryDate) 
                       Values('" & Me.Text & "', Null, '" & FilterGrid.Item("FieldName", CInt(sender.Tag)).Value & "', '" & mFilter & "', '" & Replace(FilterGrid.Item("FilterCode", CInt(sender.Tag)).Value, "'", "`") & "', '" & AgL.PubUserName & "', " & AgL.Chk_Date(AgL.PubLoginDate) & ") "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End Select

    End Sub
    Private Sub BtnProceed_Click(sender As Object, e As EventArgs) Handles BtnProceed.Click
        RaiseEvent BtnProceedPressed()
    End Sub
    Public Sub FManagerFilterDisplayGrid(IsFilterOpen As Boolean)
        If IsFilterOpen = False Then
            FilterGridDisplay.Columns.Clear()
            For I As Integer = 0 To FilterGrid.Rows.Count - 1
                FilterGridDisplay.Columns.Add(I.ToString, I.ToString)
                If FilterGridDisplay.Rows.Count = 0 Then FilterGridDisplay.Rows.Add() : FilterGridDisplay.Rows(0).Height = 31
                If AgL.XNull(FilterGrid.Item(GFilter, I).Value) <> "" And FilterGrid.Item(GFilter, I).Value IsNot Nothing And
                    AgL.XNull(FilterGrid.Item(GFilter, I).Value) <> "All" And
                    AgL.XNull(FilterGrid(GFieldName, I).Value) <> "Report Type" Then
                    FilterGridDisplay.Item(I, 0).Value = FilterGrid(GFieldName, I).Value + " : " + AgL.XNull(FilterGrid.Item(GFilter, I).Value)
                    FilterGridDisplay.Item(I, 0).Style.SelectionBackColor = Color.Purple
                    FilterGridDisplay.Item(I, 0).Style.SelectionForeColor = Color.White
                Else
                    FilterGridDisplay.Columns(I).Visible = False
                End If
            Next
            FilterGridDisplay.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells)
            FilterGridDisplay.Visible = True
        Else
            FilterGridDisplay.Visible = False
        End If
        RaiseEvent FormatFilterDisplayGrid()
    End Sub

    Private Sub DGL1_EditingControl_Validating(sender As Object, e As CancelEventArgs) Handles DGL1.EditingControl_Validating
        RaiseEvent DGL1EditingControl_Validating(sender, e)
    End Sub

    Private Sub FilterGrid_EditingControl_Validating(sender As Object, e As CancelEventArgs) Handles FilterGrid.EditingControl_Validating
        RaiseEvent FilterGridEditingControl_Validating(sender, e)
    End Sub

    Private Sub FilterGrid_CellBeginEdit(sender As Object, e As DataGridViewCellCancelEventArgs) Handles FilterGrid.CellBeginEdit
        RaiseEvent FilterGridCellBeginEdit(sender, e)
    End Sub


    Private Sub DGL1_CellBeginEdit(sender As Object, e As DataGridViewCellCancelEventArgs) Handles DGL1.CellBeginEdit
        RaiseEvent DGL1CellBeginEdit(sender, e)
    End Sub

    Private Sub DGL1_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DGL1.CellEnter
        RaiseEvent DGL1CellEnter(sender, e)
    End Sub

    Private Sub DGL1_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DGL1.RowEnter
        If Not DGL1.Columns.Contains("Tick") Then
            If e.RowIndex > -1 Then DGL1.Rows(e.RowIndex).Selected = True
            DGL1.RowsDefaultCellStyle.SelectionBackColor = Color.LightGray
        End If
    End Sub


    'Private Sub FGMain_RowLeave(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGL1.RowLeave
    '    Try
    '        DGL1.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.White
    '    Catch ex As Exception
    '    End Try
    'End Sub
    'Private Sub FGMain_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGL1.RowEnter
    '    Try
    '        DGL1.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.LightCyan
    '    Catch ex As Exception
    '    End Try
    'End Sub
    Private Sub FSaveReprtDisplaySettings(bReportName As String, bReportFormatName As String, DGL As AgControls.AgDataGrid)
        For I As Integer = 0 To DGL.Columns.Count - 1
            mQry = " UPDATE ReportLineUISetting 
                Set IsVisible = " & Val(DGL.Columns(I).Visible) & ",
                ColumnWidth = " & Val(DGL.Columns(I).Width) & ",
                DisplayIndex = " & Val(DGL.Columns(I).DisplayIndex) & "
                Where ReportName = '" & bReportName & "'
                And IfNull(ReportFormatName,'') = '" & bReportFormatName & "'
                And GridName = '" & DGL.Name & "'
                And FieldName = '" & DGL.Columns(I).Name & "'"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        Next
    End Sub
    Private Sub FilterGrid_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles FilterGrid.CellEnter
        If Me.Visible And sender.ReadOnly = False And sender.CurrentCell.RowIndex > 0 Then
            If sender.CurrentCell.ColumnIndex = sender.Columns(GFieldName).Index Then
                SendKeys.Send("{Tab}")
            End If
        End If
    End Sub
    Private Sub FilterGrid_CellContentClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles FilterGrid.CellContentClick
        If FilterGrid.CurrentCell Is Nothing Then Exit Sub
        Select Case FilterGrid.CurrentCell.ColumnIndex
            Case GButton
                FOpenFilterSelectionWindow(FilterGrid.CurrentCell.RowIndex)
        End Select
    End Sub
    Private Sub FilterGrid_KeyDown(sender As Object, e As KeyEventArgs) Handles FilterGrid.KeyDown
        If FilterGrid.CurrentCell Is Nothing Then Exit Sub
        Select Case FilterGrid.CurrentCell.ColumnIndex
            Case GFilter
                If ClsMain.IsSpecialKeyPressedForSelectionWindow(e) = False Then
                    FOpenFilterSelectionWindow(FilterGrid.CurrentCell.RowIndex)
                End If

                'If e.KeyCode = Keys.Enter Then
                '    If FilterGrid.CurrentCell.RowIndex <> FilterGrid.Rows.Count - 1 Then
                '        FilterGrid.CurrentCell = FilterGrid.Item(GFieldName, FilterGrid.CurrentCell.RowIndex)
                '    End If
                'End If
        End Select
    End Sub
    Private Sub FOpenFilterSelectionWindow(RowIndex)
        FilterGrid(GFilter, FilterGrid.CurrentCell.RowIndex).Selected = True
        If FilterGrid(GDataType, RowIndex).Value = FGDataType.DT_Selection_Multiple Then
            If FRH_Multiple(RowIndex) Is Nothing Then
                FRH_Multiple(RowIndex) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(AgL.FillData(FRefineSQLQuery(FilterGrid.Item(GHelpQuery, RowIndex).Value), AgL.GCn).Tables(0)), "", Val(FilterGrid.Item(GHGHeight, RowIndex).Value), Val(FilterGrid.Item(GHGWidth, RowIndex).Value), , , False, Replace(FilterGrid(GFilterCode, RowIndex).Value, "'", ""))
                FRH_Multiple(RowIndex).FFormatColumn(0, "Select", 50, DataGridViewContentAlignment.MiddleCenter)
                FRH_Multiple(RowIndex).FFormatColumn(1, , 0, , False)
                FRH_Multiple(RowIndex).FFormatColumn(2, FilterGrid(GFieldName, RowIndex).Value, Val(FilterGrid(GHGColWidth, RowIndex).Value), FilterGrid.Item(GHGColAlignment, RowIndex).Value)
            End If
            FHPGD_Show_Multiple(New System.Windows.Forms.KeyEventArgs(Keys.A))
        ElseIf FilterGrid(GDataType, RowIndex).Value = FGDataType.DT_Selection_Single Then
            'If FRH_Single(RowIndex) Is Nothing Then
            FRH_Single(RowIndex) = New DMHelpGrid.FrmHelpGrid(New DataView(AgL.FillData(FRefineSQLQuery(FilterGrid.Item(GHelpQuery, RowIndex).Value), AgL.GCn).Tables(0)), "", Val(FilterGrid.Item(GHGHeight, RowIndex).Value), Val(FilterGrid.Item(GHGWidth, RowIndex).Value), , , False)
            FRH_Single(RowIndex).FFormatColumn(0, , 0, , False)
            FRH_Single(RowIndex).FFormatColumn(1, FilterGrid(GFieldName, RowIndex).Value, Val(FilterGrid(GHGColWidth, RowIndex).Value), FilterGrid.Item(GHGColAlignment, RowIndex).Value)
            'End If
            FHPGD_Show_Single(New System.Windows.Forms.KeyEventArgs(Keys.A))
        End If

        RaiseEvent FilterSelectionValidated(RowIndex)
    End Sub

    Private Sub DGL1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGL1.CellContentClick
        RaiseEvent DGL1CellContentClick(sender, e)
    End Sub

    Private Sub DGL1_KeyUp(sender As Object, e As KeyEventArgs) Handles DGL1.KeyUp

    End Sub
End Class