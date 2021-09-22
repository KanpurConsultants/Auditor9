Public Class FrmQuery
    Dim DtTemp As DataTable
    Private Const FilterType_Filter As String = "Filter"
    Private Const FilterType_RemoveAllFilter As String = "Remove All Filter"

    Private Flag_DataBindingCompleted As Boolean = False
    Enum ColumnDataType
        NumberType
        DateTimeType
        StringType
    End Enum
    Private Sub ExecuteQuery()
        Dim mQry As String

        If Not TxtPassword.Text = "P@ssw0rd!" Then
            LblMessage.Text = "Incorrect Password"
            Exit Sub
        End If

        mQry = TxtQuery.SelectedText

        If mQry = "" And TxtQuery.Text <> "" Then
            mQry = TxtQuery.Text
        End If


        If mQry.ToUpper.Contains("SELECT") Then
            Try
                DGL1.DataSource = Nothing
                DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
                DGL1.DataSource = DtTemp
                LblMessage.Text = "Query Executed Successfully " & DateTime.Now().ToString
            Catch ex As Exception
                DGL1.DataSource = Nothing
                LblMessage.Text = ex.Message & "  " & DateTime.Now().ToString
            End Try
        Else
            Try
                DGL1.DataSource = Nothing
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
                LblMessage.Text = "Query Executed Successfully " & DateTime.Now().ToString
            Catch ex As Exception
                DGL1.DataSource = Nothing
                LblMessage.Text = ex.Message & "  " & DateTime.Now().ToString
            End Try
        End If
        Flag_DataBindingCompleted = False
        DGL1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells)
        Flag_DataBindingCompleted = True
    End Sub

    Private Sub BtnGo_Click(sender As Object, e As EventArgs) Handles BtnGo.Click
        LblMessage.Text = ""
        ExecuteQuery()
        TxtQuery.Focus()
    End Sub

    Private Sub FrmQuery_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
        'If e.KeyChar = Chr(Keys.F5) Then
        '    ExecuteQuery()
        'End If
    End Sub
    Private Sub FrmQuery_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        'If e.KeyCode = Keys.F5 Then
        '    ExecuteQuery()
        'End If

        If e.Control And e.KeyCode = Keys.A Then
            TxtQuery.SelectAll()
        End If

    End Sub

    Private Sub FrmQuery_Load(sender As Object, e As EventArgs) Handles Me.Load
        DGL1.ReadOnly = True
        AgL.GridDesign(DGL1)
        DGL1.MultiSelect = True
        DGL1.AllowUserToAddRows = False
        DGL1.RowHeadersVisible = True
        DGL1.RowHeadersWidth = 70
        DGL1.ContextMenuStrip = MnuMain

        AgL.GridDesign(Dgl2)
        Dgl2.ColumnHeadersVisible = False
        Dgl2.AllowUserToAddRows = False
        Dgl2.EnableHeadersVisualStyles = False
        Dgl2.ScrollBars = ScrollBars.None
        Dgl2.RowHeadersVisible = True
        Dgl2.RowHeadersWidth = 70
        Dgl2.ReadOnly = True
        Dgl2.AllowUserToResizeColumns = False
        Dgl2.Visible = False


        Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub DGL1_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles DGL1.RowPostPaint
        Dim b As SolidBrush = New SolidBrush(DGL1.RowHeadersDefaultCellStyle.ForeColor)
        e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4)
    End Sub
    Private Sub MnuExportToExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles MnuInsert.Click, MnuWhereIn.Click, MnuExportToExcel.Click, MnuFilter.Click, MnuShowColumnTotals.Click
        Try
            Select Case sender.Name
                Case MnuInsert.Name
                    FGenerateInsert()
                Case MnuWhereIn.Name
                    FGenerateWhereIn()
                Case MnuExportToExcel.Name
                    Dim FileName As String
                    If MsgBox("Want to Export Grid Data", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Export Grid?...") = vbNo Then Exit Sub
                    FileName = AgControls.Export.GetFileName(My.Computer.FileSystem.SpecialDirectories.Desktop)
                    If FileName.Trim <> "" Then
                        Call AgControls.Export.exportExcel(DGL1, FileName, DGL1.Handle)
                    End If
                Case MnuShowColumnTotals.Name
                    ProcApplyAggregateFunction()
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub FGenerateInsert()
        Dim bColumnQry As String = ""
        Dim bRowQry As String = ""
        Dim bQry As String = ""
        Dim DtColumnIndex As DataTable
        Dim DtRowIndex As DataTable
        For I As Integer = 0 To DGL1.SelectedCells.Count - 1
            If bColumnQry <> "" Then bColumnQry += " UNION ALL"
            bColumnQry += " Select " & DGL1.SelectedCells(I).ColumnIndex & " As DIndex"
            If bRowQry <> "" Then bRowQry += " UNION ALL"
            bRowQry += " Select " & DGL1.SelectedCells(I).RowIndex & " As DIndex"
        Next

        bQry = " Select Distinct DIndex From (" & bColumnQry & ") As V1 "
        DtColumnIndex = AgL.FillData(bQry, AgL.GCn).Tables(0)
        bQry = " Select Distinct DIndex From (" & bRowQry & ") As V1 "
        DtRowIndex = AgL.FillData(bQry, AgL.GCn).Tables(0)

        bQry = ""
        For I As Integer = 0 To DtRowIndex.Rows.Count - 1
            bQry += "INSERT INTO <TableName>("
            For J As Integer = 0 To DtColumnIndex.Rows.Count - 1
                If J > 0 Then bQry += ", "
                bQry += DGL1.Columns(J).Name
                If J = DtColumnIndex.Rows.Count - 1 Then bQry += ")" : bQry += vbCrLf
            Next

            bQry += "Values("
            For J As Integer = 0 To DtColumnIndex.Rows.Count - 1
                If J > 0 Then bQry += ", "
                bQry += AgL.Chk_Text(DGL1.Item(J, I).Value)
                If J = DtColumnIndex.Rows.Count - 1 Then bQry += ")" : bQry += vbCrLf + vbCrLf
            Next
        Next

        TxtQuery.Text += bQry
    End Sub
    Private Sub FGenerateWhereIn()
        Dim bQry As String = ""
        For I As Integer = 0 To DGL1.SelectedCells.Count - 1
            If I > 0 Then
                If DGL1.SelectedCells(I).ColumnIndex <> DGL1.SelectedCells(I - 1).ColumnIndex Then
                    MsgBox("Selection has to be one column wide in order to generate WHERE IN clause.", MsgBoxStyle.Information)
                    Exit Sub
                End If
            End If
        Next

        bQry = "Where " + DGL1.Columns(DGL1.SelectedCells(0).ColumnIndex).Name + " In ("
        For I As Integer = 0 To DGL1.SelectedCells.Count - 1
            If I > 0 Then bQry += ", "
            bQry += AgL.Chk_Text(DGL1.Item(DGL1.SelectedCells(0).ColumnIndex, I).Value)
            If I = DGL1.SelectedCells.Count - 1 Then bQry += ")"
        Next
        TxtQuery.Text += bQry
    End Sub
    Private Sub ProcFillFilterMnu()
        Dim mColumnIndex As Integer = 0
        Dim mRowIndex As Integer = 0
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

            MnuChild = New System.Windows.Forms.ToolStripMenuItem(FilterType_RemoveAllFilter)
            MnuChild.Name = FilterType_RemoveAllFilter
            MnuChild.ToolTipText = FilterType_RemoveAllFilter
            MnuFilter.DropDownItems.Add(MnuChild)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
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
    Private Sub CMSVisible_DropDownItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles _
                MnuFilter.DropDownItemClicked
        Try
            Select Case sender.Name
                Case MnuFilter.Name
                    Select Case e.ClickedItem.ToolTipText
                        Case FilterType_Filter
                            Call ProcFilterGrid(e.ClickedItem)

                        Case FilterType_RemoveAllFilter
                            Call ProcFilterGrid(e.ClickedItem)
                    End Select
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub ProcFilterGrid(MnuChild As ToolStripMenuItem)
        If MnuChild.ToolTipText = FilterType_RemoveAllFilter Then
            DtTemp.DefaultView.RowFilter = ""
        Else
            If DtTemp.DefaultView.RowFilter <> "" Then
                DtTemp.DefaultView.RowFilter += " And " + MnuChild.Name
            Else
                DtTemp.DefaultView.RowFilter += MnuChild.Name
            End If
        End If
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
    Private Sub DGL1_CellMouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DGL1.CellMouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Call ProcFillFilterMnu()
        End If
    End Sub
    Private Sub DGL1_ColumnWidthChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewColumnEventArgs) Handles DGL1.ColumnWidthChanged
        If Flag_DataBindingCompleted = True Then
            If Dgl2.Columns.Count = DGL1.Columns.Count Then
                Dgl2.Columns(e.Column.Index).Width = e.Column.Width
            End If
            DGL1.AutoResizeColumnHeadersHeight()
            DGL1.AutoResizeRows(DataGridViewAutoSizeRowsMode.DisplayedCells)
        End If
    End Sub

    Private Sub DGL1_Scroll(ByVal sender As Object, ByVal e As System.Windows.Forms.ScrollEventArgs) Handles DGL1.Scroll
        If e.ScrollOrientation = ScrollOrientation.HorizontalScroll Then
            Dgl2.HorizontalScrollingOffset = e.NewValue
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
    Public Sub ProcApplyAggregateFunction()
        Dim I As Integer = 0
        Try
            If Dgl2.Visible = False Then
                Dgl2.Visible = True
            Else
                Dgl2.Visible = False
                Exit Sub
            End If

            Dgl2.Rows.Clear()
            Dgl2.ColumnCount = DGL1.Columns.Count
            Dgl2.RowCount = 1
            For I = 0 To DGL1.Columns.Count - 1
                Dgl2.Columns(I).Visible = DGL1.Columns(I).Visible
                Dgl2.Columns(I).Width = DGL1.Columns(I).Width
                Dgl2.Columns(I).DisplayIndex = DGL1.Columns(I).DisplayIndex
            Next

            With DGL1
                For I = 0 To .Columns.Count - 1
                    Dgl2.Item(I, 0).Value = ""
                    If (FunRetDataType(DGL1.Item(I, 0).Value.GetType.ToString) = ColumnDataType.NumberType Or FunRetColumnDataType(I) = ColumnDataType.NumberType) And Not DGL1.Columns(I).Name.Contains("Rate") Then Dgl2.Item(I, 0).Value = DtTemp.Compute("Sum([" & .Columns(I).HeaderText & "])", "")
                    'If (FunRetDataType(Dgl2.Item(I, 0).Value.GetType.ToString) = ColumnDataType.NumberType Or FunRetColumnDataType(I) = ColumnDataType.NumberType) Then Dgl2.Item(I, 0).Style.Alignment = DataGridViewContentAlignment.BottomRight

                    'If .Columns(I).Tag IsNot Nothing AndAlso .Columns(I).Tag <> "" Then
                    '    Dgl2.Item(I, 0).Value = DtTemp.Compute(.Columns(I).Tag & "([" & .Columns(I).HeaderText & "])", "")
                    'End If
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
End Class