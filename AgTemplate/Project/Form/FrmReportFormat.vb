Imports CrystalDecisions.CrystalReports.Engine
Public Class FrmReportFormat
    Private DTMaster As New DataTable()
    Public BMBMaster As BindingManagerBase
    Private KEAMainKeyCode As System.Windows.Forms.KeyEventArgs
    Private DTStruct As New DataTable
    Dim mQry As String = ""
    Dim mRetVisibleQry$ = ""

    Public WithEvents DglVisible As New AgControls.AgDataGrid
    Public WithEvents DglSort As New AgControls.AgDataGrid
    Public WithEvents DglGroup As New AgControls.AgDataGrid
    Public WithEvents DglFilter As New AgControls.AgDataGrid

    Public Const ColIsSelect As String = "Select"
    Public Const ColFieldName As String = "Field"
    Public Const Col1Function As String = "Function"

    Public Const Col2AscDsc As String = "Asc Dsc"

    Public Const Col3SubTotalYN As String = "Sub Total Y/N"

    Public Const ColSno As String = "SNo"

    Public Const ColDataType As String = "Data Type"
    Public Const ColFilterOperator As String = "Operator"
    Public Const ColValue1 As String = "Value1"
    Public Const ColValue2 As String = "Value2"
    Public Const ColMValue As String = "MValue"

    Public Event BaseEvent_BtnOkClick()
    Public Event BaseEvent_BtnSaveDisplayClick()
    Public Event BaseEvent_BtnSaveSortClick()
    Public Event BaseEvent_BtnSaveFilterClick()

    Dim mColumnList$ = ""

    Enum ColumnDataType
        NumberType
        DateTimeType
        StringType
    End Enum

    Public Sub New(ByVal ColumnList As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        mColumnList = ColumnList
    End Sub

    Private Sub FrmReportFormat_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, 0)
    End Sub

    Public Sub IniGrid()
        ''==============================================================================
        ''================< Grid >====================================
        ''==============================================================================
        With AgCL

            .AddAgCheckColumn(DglVisible, ColIsSelect, 50, ColIsSelect, True)
            .AddAgTextColumn(DglVisible, ColFieldName, 203, 100, ColFieldName, True, True, False)
            .AddAgTextColumn(DglVisible, Col1Function, 100, 50, Col1Function, True, False, False)
            DglVisible.Columns(ColIsSelect).CellTemplate.Style.Font = New Font(New FontFamily("Wingdings"), 12)
            DglVisible.Columns(ColFieldName).CellTemplate.Style.BackColor = Color.White

            .AddAgNumberColumn(DglSort, ColSno, 40, 100, 0, False, ColSno, False, False)
            .AddAgTextColumn(DglSort, ColFieldName, 200, 100, ColFieldName, True, False, False)
            .AddAgTextColumn(DglSort, Col2AscDsc, 100, 50, Col2AscDsc, True, False, False)

            .AddAgCheckColumn(DglGroup, ColIsSelect, 40, ColIsSelect, True)
            .AddAgTextColumn(DglGroup, ColFieldName, 200, 100, ColFieldName, True, True, False)
            .AddAgYesNoColumn(DglGroup, Col3SubTotalYN, 100, Col3SubTotalYN, True)
            DglGroup.Columns(ColIsSelect).CellTemplate.Style.Font = New Font(New FontFamily("Wingdings"), 12)

            .AddAgNumberColumn(DglFilter, ColSno, 40, 100, 0, False, ColSno, False, False)
            .AddAgTextColumn(DglFilter, ColDataType, 200, 100, ColDataType, False, False)
            .AddAgTextColumn(DglFilter, ColFieldName, 160, 100, ColFieldName, True, False)
            .AddAgTextColumn(DglFilter, ColFilterOperator, 100, 100, ColFilterOperator, True, False)
            .AddAgTextColumn(DglFilter, ColValue1, 90, 100, ColValue1, True, False)
            .AddAgTextColumn(DglFilter, ColValue2, 90, 100, ColValue2, True, False)
            .AddAgButtonColumn(DglFilter, ColMValue, 50, ColMValue, True, False)
        End With

        AgL.AddAgDataGrid(DglVisible, Pnl1)
        DglVisible.EnableHeadersVisualStyles = False
        DglVisible.AllowUserToAddRows = False
        DglVisible.MultiSelect = True

        AgL.AddAgDataGrid(DglSort, Pnl2)
        DglSort.EnableHeadersVisualStyles = False
        DglSort.MultiSelect = True

        AgL.AddAgDataGrid(DglGroup, Pnl3)
        DglGroup.EnableHeadersVisualStyles = False
        DglGroup.AllowUserToAddRows = False

        AgL.AddAgDataGrid(DglFilter, Pnl4)
        DglFilter.EnableHeadersVisualStyles = False
    End Sub

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.StartPosition = FormStartPosition.CenterScreen
            AgL.GridDesign(DglVisible)
            AgL.GridDesign(DglSort)
            AgL.GridDesign(DglGroup)
            AgL.GridDesign(DglFilter)
            TCMain.TabPages.Remove(TPGroupSet)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub Ini_List()
        Try
            mQry = " SELECT 'Asc' AS Code, 'Asc' AS Name " & _
                    " UNION ALL " & _
                    " SELECT 'Desc' AS Code, 'Desc' AS Name "
            DglSort.AgHelpDataSet(Col2AscDsc, , TPDisplaySet.Top, TPDisplaySet.Left) = AgL.FillData(mQry, AgL.GCn)

            mQry = " SELECT 'Sum' AS Code, 'Sum' AS Name " & _
                    " UNION ALL " & _
                    " SELECT 'Max' AS Code, 'Maximum' AS Name " & _
                    " UNION ALL  " & _
                    " SELECT 'Min' AS Code, 'Minimum' AS Name " & _
                    " UNION ALL  " & _
                    " SELECT 'Count' AS Code, 'Count' AS Name " & _
                    " UNION ALL  " & _
                    " SELECT 'Avg' AS Code, 'Average' AS Name "
            DglVisible.AgHelpDataSet(Col1Function, , TPDisplaySet.Top, TPDisplaySet.Left) = AgL.FillData(mQry, AgL.GCn)

            mQry = " Select '=' As Code, 'Equals' As Description " & _
                    " UNION ALL " & _
                    " Select '<>' As Code, 'Not Equal' As Description " & _
                    " UNION ALL " & _
                    " Select '<' As Code, 'Less Than' As Description " & _
                    " UNION ALL " & _
                    " Select '>' As Code, 'Greater Than' As Description "
            DglFilter.AgHelpDataSet(ColFilterOperator, , TPDisplaySet.Top, TPDisplaySet.Left) = AgL.FillData(mQry, AgL.GCn)

            mQry = " Select '" & ColumnDataType.NumberType & "'   As Code, 'Number' As Description " & _
                    " UNION ALL " & _
                    " Select '" & ColumnDataType.DateTimeType & "'   As Code, 'Date Time' As Description " & _
                    " UNION ALL " & _
                    " Select '" & ColumnDataType.StringType & "' As Code, 'String' As Description "
            DglFilter.AgHelpDataSet(ColDataType, , TPDisplaySet.Top, TPDisplaySet.Left) = AgL.FillData(mQry, AgL.GCn)

            DglSort.AgHelpDataSet(ColFieldName, , TPDisplaySet.Top, TPDisplaySet.Left) = AgL.FillData(mColumnList, AgL.GCn)
            DglFilter.AgHelpDataSet(ColFieldName, , TPDisplaySet.Top, TPDisplaySet.Left) = AgL.FillData(mColumnList, AgL.GCn)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub ProcFillVisibleGrids(ByVal mVisibleQry As String)
        Dim DsTemp As DataSet = Nothing
        Dim I As Integer = 0
        Try
            If mVisibleQry <> "" Then
                DsTemp = AgL.FillData(mVisibleQry, AgL.GCn)
                With DsTemp.Tables(0)
                    DglVisible.RowCount = 1 : DglVisible.Rows.Clear()
                    If .Rows.Count > 0 Then
                        For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                            DglVisible.Rows.Add()
                            DglVisible.Item(ColFieldName, I).Value = AgL.XNull(.Rows(I)("FieldName"))
                            DglVisible.Item(ColIsSelect, I).Value = IIf(AgL.StrCmp(AgL.XNull(.Rows(I)("IsSelect")), "True"), AgLibrary.ClsConstant.StrCheckedValue, AgLibrary.ClsConstant.StrUnCheckedValue)
                            DglVisible.AgSelectedValue(Col1Function, I) = AgL.XNull(.Rows(I)("AggregateFunction"))
                        Next I
                    End If
                End With
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub BlankText()
        DglVisible.RowCount = 1 : DglVisible.Rows.Clear()
    End Sub

    Private Sub DglVisible_CellMouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DglVisible.CellMouseUp, DglSort.CellMouseUp, DglGroup.CellMouseUp
        Dim mRowIndex As Integer, mColumnIndex As Integer
        Try
            mRowIndex = sender.CurrentCell.RowIndex
            mColumnIndex = sender.CurrentCell.ColumnIndex

            If sender.Item(mColumnIndex, mRowIndex).Value Is Nothing Then sender.Item(mColumnIndex, mRowIndex).Value = ""
            Select Case sender.Columns(sender.CurrentCell.ColumnIndex).Name
                Case ColIsSelect
                    Call AgL.ProcSetCheckColumnCellValue(sender, DglVisible.CurrentCell.ColumnIndex)
            End Select
        Catch ex As Exception
        End Try
    End Sub


    Private Sub BtnRowDownDisplay_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
            BtnRowDownDisplay.Click, BtnRowUpDisplay.Click, BtnRowDownSort.Click, BtnRowUpSort.Click, _
            BtnRowDownGroup.Click, BtnRowUpGroup.Click
        Try
            Select Case sender.Name
                Case BtnRowDownDisplay.Name
                    ProcShiftRows(DglVisible, +1)

                Case BtnRowUpDisplay.Name
                    ProcShiftRows(DglVisible, -1)

                Case BtnRowDownSort.Name
                    ProcShiftRows(DglSort, +1)

                Case BtnRowUpSort.Name
                    ProcShiftRows(DglSort, -1)

                Case BtnRowDownGroup.Name
                    ProcShiftRows(DglGroup, +1)

                Case BtnRowUpGroup.Name
                    ProcShiftRows(DglGroup, -1)
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ProcShiftRows(ByVal DGL As DataGridView, ByVal CntShift As Integer)
        Dim DglRow As DataGridViewRow = Nothing
        Dim bRowIndex As Integer = 0
        Try
            If DGL.CurrentRow.Index = 0 And CntShift = -1 Then Exit Sub
            If DGL.AllowUserToAddRows = False Then
                If DGL.CurrentRow.Index = DGL.Rows.Count - 1 And CntShift = +1 Then Exit Sub
            Else
                If DGL.CurrentRow.Index = DGL.Rows.Count - 2 And CntShift = +1 Then Exit Sub
            End If

            DglRow = DGL.CurrentRow
            bRowIndex = DGL.CurrentRow.Index
            DGL.Rows.Remove(DglRow)
            DGL.Rows.Insert(bRowIndex + CntShift, DglRow)
            DGL.CurrentCell = DGL.Item(ColFieldName, DglRow.Index)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub BtnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnOk.Click, _
    BtnCancel.Click, BtnSaveDisplaySettings.Click, BtnSaveSortSetting.Click, BtnSaveFilterSettings.Click
        Try
            Select Case sender.Name
                Case BtnOk.Name
                    RaiseEvent BaseEvent_BtnOkClick()
                    Me.Close()

                Case BtnSaveDisplaySettings.Name
                    RaiseEvent BaseEvent_BtnSaveDisplayClick()

                Case BtnSaveSortSetting.Name
                    RaiseEvent BaseEvent_BtnSaveSortClick()

                Case BtnSaveFilterSettings.Name
                    RaiseEvent BaseEvent_BtnSaveFilterClick()

                Case BtnCancel.Name
                    Me.Close()
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub DglVisible_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DglVisible.KeyDown
        Dim mRowIndex As Integer, mColumnIndex As Integer
        Try
            mRowIndex = sender.CurrentCell.RowIndex
            mColumnIndex = sender.CurrentCell.ColumnIndex

            If sender.Item(mColumnIndex, mRowIndex).Value Is Nothing Then sender.Item(mColumnIndex, mRowIndex).Value = ""
            Select Case sender.Columns(sender.CurrentCell.ColumnIndex).Name
                Case ColIsSelect
                    Call AgL.ProcSetCheckColumnCellValue(sender, DglVisible.CurrentCell.ColumnIndex)
            End Select
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Dgl1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DglSort.KeyDown, DglFilter.KeyDown
        If e.Control And e.KeyCode = Keys.D Then
            sender.CurrentRow.Selected = True
        End If
        If e.Control Or e.Shift Or e.Alt Then Exit Sub
    End Sub
End Class