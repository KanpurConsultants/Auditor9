Imports System.Xml
Imports System.IO
Imports CrystalDecisions.CrystalReports.Engine
Public Class FrmReportWindow
    Dim mQry As String = ""
    Dim mMainQry As String = ""
    Dim mReportName As String = ""

    Dim mColumnIndex As Integer
    Dim mRowIndex As Integer

    Dim DsMaster As DataSet = Nothing
    Dim DtMainWithTotals As DataTable = Nothing

    Dim DsTotal As DataSet = Nothing

    Public WithEvents DGL1 As New AgControls.AgDataGrid
    Public WithEvents DGL2 As New AgControls.AgDataGrid

    Public WithEvents FrmObj As FrmReportFormat

    Private Const FilterType_Filter As String = "Filter"
    Private Const FilterType_RemoveFilter As String = "Remove Filter"
    Private Const FilterType_RemoveAllFilter As String = "Remove All Filter"

    Private Const SortType_SortAsc As String = "Ascending"
    Private Const SortType_SortDesc As String = "Descending"
    Private Const SortType_RemoveSort As String = "Remove Sort"

    Private Const MnuType_More As String = "More..."

    Enum ColumnDataType
        NumberType
        DateTimeType
        StringType
    End Enum

    Public Sub New(ByVal ReportQry As String, ByVal Title As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        mMainQry = " SELECT * From (" & ReportQry & ") AS V1"
        mReportName = Title
    End Sub

    Private Sub IniGrid()
        AgL.AddAgDataGrid(DGL1, Pnl1)
        DGL1.AllowUserToAddRows = False
        DGL1.EnableHeadersVisualStyles = False
        DGL1.ReadOnly = True
        DGL1.ContextMenuStrip = MnuMain
        DGL1.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
        DGL1.AllowUserToOrderColumns = True


        AgL.AddAgDataGrid(DGL2, Pnl2)
        DGL2.ColumnHeadersVisible = False
        DGL2.AllowUserToAddRows = False
        DGL2.EnableHeadersVisualStyles = False
        DGL2.ScrollBars = ScrollBars.None
        DGL2.RowHeadersVisible = False
        DGL2.ReadOnly = True
        DGL2.AllowUserToResizeColumns = False

        DGL2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DGL2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer))

        DGL1.Anchor = AnchorStyles.Bottom + AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Top
        DGL2.Anchor = Pnl2.Anchor
    End Sub

    Private Sub FrmReportWindow_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.Text = mReportName
            AgL.GridDesign(DGL1)
            AgL.GridDesign(DGL2)
            IniGrid()


            ProcFillGrid(mMainQry)
            ProcFillVisibleColumnMenu()

            FrmObj = New FrmReportFormat(FunRetColumnList)
            FrmObj.IniGrid()
            FrmObj.Ini_List()
            FrmObj.ProcFillVisibleGrids(FunRetVisibleColumnList())

            Call ProcShowSortSettings(Me.Text + "-Sort")
            Call ProcShowFilterSettings(Me.Text + "-Filter")

            Call ProcSortGrid()
            Call ProcFilterGrid()
            Call ProcApplyAggregateFunction()

            RbtComprehensiveSearch.Checked = True
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
                    If mQry = "" Then

                        mQry = " Select '" & DGL1.Columns(I).HeaderText & "' AS FieldName, " & _
                                " '" & DGL1.Columns(I).Visible & "' AS IsSelect, " & _
                                " '" & DGL1.Columns(I).Tag & "' AS AggregateFunction, " & DGL1.Columns(I).DisplayIndex & " as DispIndex "
                    Else
                        mQry = mQry & " UNION ALL "
                        mQry = mQry & " Select '" & DGL1.Columns(I).HeaderText & "' AS FieldName, " & _
                                " '" & DGL1.Columns(I).Visible & "' AS IsSelect, " & _
                                " '" & DGL1.Columns(I).Tag & "' AS AggregateFunction, " & DGL1.Columns(I).DisplayIndex & " as DispIndex "
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
                    If mQry = "" Then
                        mQry = " Select '" & DGL1.Columns(I).HeaderText & "' AS Code, '" & DGL1.Columns(I).HeaderText & "' AS FieldName "
                    Else
                        mQry = mQry & " UNION ALL "
                        mQry = mQry & " Select '" & DGL1.Columns(I).HeaderText & "' AS Code, '" & DGL1.Columns(I).HeaderText & "' AS FieldName"
                    End If
                Next
            End With
            FunRetColumnList = mQry
        Catch ex As Exception
            FunRetColumnList = ""
            MsgBox(ex.Message)
        End Try
    End Function

    Public Sub ProcFillGrid(ByVal mMQry As String)
        Dim I As Integer
        Try
            DsMaster = AgL.FillData(mMQry, AgL.GCn)
            DGL1.DataSource = DsMaster.Tables(0)

            DGL2.ColumnCount = DGL1.Columns.Count
            DGL2.RowCount = 1


            DGL1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells)

            AgCL.GridSetiingShowXml(Me.Text & "-Visible", DGL1)
            AgCL.GridSetiingShowXml(Me.Text & "-Visible", DGL2)


            For I = 0 To DsMaster.Tables(0).Columns.Count - 1
                Select Case FunRetDataType(UCase(DsMaster.Tables(0).Columns(I).DataType.ToString))
                    Case ColumnDataType.NumberType
                        DGL1.Columns(I).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                        DGL1.Columns(I).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                End Select
            Next
            DGL1.AutoResizeColumnHeadersHeight()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub BlankText()
        DGL1.RowCount = 1 : DGL1.Rows.Clear()
        DGL2.RowCount = 1 : DGL2.Rows.Clear()
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
    End Sub

    Private Sub DGL1_Scroll(ByVal sender As Object, ByVal e As System.Windows.Forms.ScrollEventArgs) Handles DGL1.Scroll
        If e.ScrollOrientation = ScrollOrientation.HorizontalScroll Then
            DGL2.HorizontalScrollingOffset = e.NewValue
        End If
    End Sub

    Private Sub ProcFillVisibleColumnMenu()
        Dim MnuChild As ToolStripMenuItem
        Dim I As Integer = 0
        Try
            MnuVisible.DropDownItems.Clear()

            With DGL1
                For I = 0 To .Columns.Count - 1
                    MnuChild = New ToolStripMenuItem(.Columns(I).HeaderText)
                    MnuChild.CheckOnClick = True
                    MnuChild.Name = .Columns(I).HeaderText
                    MnuChild.Tag = .Columns(I).DisplayIndex
                    MnuChild.Checked = DGL1.Columns(I).Visible
                    MnuVisible.DropDownItems.Add(MnuChild)
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
                    If .Item(FrmReportFormat.ColFieldName, I).Value <> "" Then
                        MnuChild = New ToolStripMenuItem(.Item(FrmReportFormat.ColFieldName, I).Value.ToString)
                        MnuChild.CheckOnClick = True
                        MnuChild.Name = .Item(FrmReportFormat.ColFieldName, I).Value.ToString
                        MnuChild.Tag = I
                        MnuChild.Checked = IIf(AgL.StrCmp(.Item(FrmReportFormat.ColIsSelect, I).Value, AgLibrary.ClsConstant.StrCheckedValue), True, False)
                        MnuVisible.DropDownItems.Add(MnuChild)

                        DGL1.Columns(.Item(FrmReportFormat.ColFieldName, I).Value.ToString).Visible = IIf(AgL.StrCmp(.Item(FrmReportFormat.ColIsSelect, I).Value, AgLibrary.ClsConstant.StrCheckedValue), True, False)
                        DGL1.Columns(.Item(FrmReportFormat.ColFieldName, I).Value.ToString).DisplayIndex = I
                        DGL1.Columns(.Item(FrmReportFormat.ColFieldName, I).Value.ToString).Tag = .AgSelectedValue(FrmReportFormat.Col1Function, I)
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
                    If .Item(FrmReportFormat.ColFieldName, I).Value <> "" Then
                        MnRemoveSortCol = New ToolStripMenuItem("Remove Sort " & .Item(FrmReportFormat.ColFieldName, I).Value.ToString & " (" & .Item(FrmReportFormat.Col2AscDsc, I).Value.ToString & ")")
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
                    If .Item(FrmReportFormat.ColFieldName, I).Value <> "" Then
                        MnRemoveFilterCol = New ToolStripMenuItem((FilterType_RemoveFilter & " " & .Item(FrmReportFormat.ColFieldName, I).Value & " " & .AgSelectedValue(FrmReportFormat.ColFilterOperator, I) & " " & .Item(FrmReportFormat.ColValue1, I).Value.ToString).ToString)
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
                MnuVisible.DropDownItemClicked, MnuSort.DropDownItemClicked, _
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
                                .Item(FrmReportFormat.ColFieldName, .Rows.Count - 2).Value = DGL1.Columns(DGL1.CurrentCell.ColumnIndex).Name
                                .Item(FrmReportFormat.Col2AscDsc, .Rows.Count - 2).Value = "Asc"
                            End With

                        Case SortType_SortDesc
                            With FrmObj.DglSort
                                .Rows.Add()
                                .Item(FrmReportFormat.ColFieldName, .Rows.Count - 2).Value = DGL1.Columns(DGL1.CurrentCell.ColumnIndex).Name
                                .Item(FrmReportFormat.Col2AscDsc, .Rows.Count - 2).Value = "Desc"
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
                                .AgSelectedValue(FrmReportFormat.ColDataType, .Rows.Count - 2) = FunRetDataType(DGL1.Item(DGL1.CurrentCell.ColumnIndex, DGL1.CurrentCell.RowIndex).ValueType.ToString)
                                .Item(FrmReportFormat.ColFieldName, .Rows.Count - 2).Value = DGL1.Columns(DGL1.CurrentCell.ColumnIndex).Name
                                .AgSelectedValue(FrmReportFormat.ColFilterOperator, .Rows.Count - 2) = e.ClickedItem.Tag
                                .Item(FrmReportFormat.ColValue1, .Rows.Count - 2).Value = DGL1.Item(DGL1.CurrentCell.ColumnIndex, DGL1.CurrentCell.RowIndex).Value
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
                    If .Item(FrmReportFormat.ColFieldName, I).Value <> "" Then
                        bConStr &= IIf(bConStr <> "", " And ", "") & "[" & .Item(FrmReportFormat.ColFieldName, I).Value & "]" & .AgSelectedValue(FrmReportFormat.ColFilterOperator, I) & FunFormatField(.Item(FrmReportFormat.ColValue1, I).Value.ToString)
                    End If
                Next
            End With
            FunGetFilterConStr = bConStr
        Catch ex As Exception
            FunGetFilterConStr = ""
            MsgBox(ex.Message)
        End Try
    End Function

    Private Sub ProcFilterGrid()
        DsMaster.Tables(0).DefaultView.RowFilter = FunGetFilterConStr()
        ProcApplyAggregateFunction()
    End Sub

    Private Function FunRetDataType(ByVal DataType As String) As ColumnDataType
        Try
            Select Case UCase(DataType)
                Case "SYSTEM.INT32", "SYSTEM.DECIMAL", "SYSTEM.DOUBLE"
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

    Private Sub ProcApplyAggregateFunction()
        Dim I As Integer = 0
        Try
            With DGL1
                For I = 0 To .Columns.Count - 1
                    DGL2.Item(I, 0).Value = ""
                    If .Columns(I).Tag IsNot Nothing AndAlso .Columns(I).Tag <> "" Then
                        DGL2.Item(I, 0).Value = DsMaster.Tables(0).Compute(.Columns(I).Tag & "([" & .Columns(I).HeaderText & "])", FunGetFilterConStr())
                        If FunRetDataType(DGL2.Item(I, 0).Value.GetType.ToString) = ColumnDataType.NumberType Then DGL2.Item(I, 0).Style.Alignment = DataGridViewContentAlignment.BottomRight
                    End If
                Next
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ProcSortGrid()
        Dim MnuChild As ToolStripMenuItem
        Dim I As Integer = 0
        Try
            MnuSort.DropDownItems.Clear()
            DGL1.DataSource.DefaultView.Sort = ""

            With FrmObj.DglSort
                For I = 0 To .Rows.Count - 1
                    If .Item(FrmReportFormat.ColFieldName, I).Value <> "" Then
                        DGL1.DataSource.DefaultView.Sort &= IIf(DGL1.DataSource.DefaultView.Sort <> "", ",", "") & .Item(FrmReportFormat.ColFieldName, I).Value.ToString & " " & .Item(FrmReportFormat.Col2AscDsc, I).Value.ToString
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
                FrmObj.DglSort.Item(FrmReportFormat.ColFieldName, i).Value = bReader.ReadElementString("FieldName")
                FrmObj.DglSort.Item(FrmReportFormat.Col2AscDsc, i).Value = bReader.ReadElementString("AscDesc")
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
                        If .Item(FrmReportFormat.ColFieldName, i).Value <> "" Then
                            writer.WriteStartElement("Column")
                            writer.WriteElementString("FieldName", .Item(FrmReportFormat.ColFieldName, i).Value.ToString)
                            writer.WriteElementString("AscDesc", .Item(FrmReportFormat.Col2AscDsc, i).Value.ToString)
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
                FrmObj.DglFilter.Rows.Add()
                FrmObj.DglFilter.AgSelectedValue(FrmReportFormat.ColDataType, i) = bReader.ReadElementString("DataType")
                FrmObj.DglFilter.Item(FrmReportFormat.ColFieldName, i).Value = bReader.ReadElementString("FieldName")
                FrmObj.DglFilter.AgSelectedValue(FrmReportFormat.ColFilterOperator, i) = bReader.ReadElementString("FilterOperator")
                FrmObj.DglFilter.Item(FrmReportFormat.ColValue1, i).Value = bReader.ReadElementString("Value1")
                FrmObj.DglFilter.Item(FrmReportFormat.ColValue2, i).Value = bReader.ReadElementString("Value2")
                i = i + 1
            End While
            bReader.Close()
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
                        If .Item(FrmReportFormat.ColFieldName, i).Value <> "" Then
                            writer.WriteStartElement("Column")
                            writer.WriteElementString("DataType", .AgSelectedValue(FrmReportFormat.ColDataType, i))
                            writer.WriteElementString("FieldName", .Item(FrmReportFormat.ColFieldName, i).Value.ToString)
                            writer.WriteElementString("FilterOperator", .AgSelectedValue(FrmReportFormat.ColFilterOperator, i))
                            writer.WriteElementString("Value1", .Item(FrmReportFormat.ColValue1, i).Value)
                            writer.WriteElementString("Value2", .Item(FrmReportFormat.ColValue2, i).Value)
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
            If e.KeyCode = Keys.Enter Then
                Me.Close()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub DGL1_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGL1.CellDoubleClick
        Me.Close()
    End Sub

    Private Sub DGL1_ColumnDisplayIndexChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewColumnEventArgs) Handles DGL1.ColumnDisplayIndexChanged
        If DGL2.Columns.Count = DGL1.Columns.Count And DGL1.Columns.Count <> 0 Then
            DGL2.Columns(e.Column.Index).DisplayIndex = e.Column.DisplayIndex
            'Call FrmObj.ProcFillVisibleGrids(FunRetVisibleColumnList())
        End If
    End Sub

    Private Sub ProcAssignColumnStructure()
        Dim SCFMain As ClsStructure.StrucColumnFormating
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

    Private Sub MnuExportToExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles MnuExportToExcel.Click, MnuPreview.Click
        Dim FileName As String
        Try
            Select Case sender.Name
                Case MnuExportToExcel.Name
                    If MsgBox("Want to Export Grid Data", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Export Grid?...") = vbNo Then Exit Sub
                    FileName = AgControls.Export.GetFileName(My.Computer.FileSystem.SpecialDirectories.Desktop)
                    If FileName.Trim <> "" Then
                        Call AgControls.Export.exportExcel(DGL1, FileName, DGL1.Handle)
                    End If

                Case MnuPreview.Name
                    'Call ProcAssignColumnStructure()
                    'Call ProcCrateTotals()
                    'Dim FrmObj As New FrmSearchReport(DtMainWithTotals, Me)
                    'FrmObj.MdiParent = Me.MdiParent
                    'FrmObj.Show()


            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
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

    Private Sub RbtLeftToRightSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RbtLeftToRightSearch.Click, RbtComprehensiveSearch.Click
        Try
            Select Case sender.Name
                Case RbtComprehensiveSearch.Name
                    DGL1.GridSearchMethod = AgControls.AgLib.TxtSearchMethod.Comprehensive

                Case RbtLeftToRightSearch.Name
                    DGL1.GridSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class