Public Class FrmHelpGrid_Multi
    '==============================================
    'DVMain Is Binded With FGMain And Is Being Used Throughout The Form 
    'BytBtnValue Is For Returning Action Done By User eg:[0=Ok],[1=Cancel],[2=Forcely Close]
    '==============================================
    Public DVMain As DataView
    Private DVHold As DataView
    Public BytBtnValue As Byte = Nothing '[0=Ok],[1=Cancel],[2=Forcely Close],[3=UnKnown]
    Private BlnOnCloseDestroy As Boolean

    '===================================
    'This Is A Parameterized Constructor
    '===================================
    Public Sub New(ByVal DVPara As DataView, ByVal StrFindText As String, ByVal SrtHeight As Short, ByVal SrtWidth As Short, _
    Optional ByVal SrtTop As Short = Nothing, Optional ByVal SrtLeft As Short = Nothing, Optional ByVal BlnOnCloseDestroyVar As Boolean = True, Optional ByVal StrDefaultValue As String = "")
        Dim I As Integer
        InitializeComponent()
        DVMain = DVPara
        DVPara = Nothing
        DVMain.AllowEdit = True
        Height = SrtHeight
        Width = SrtWidth
        If Not SrtTop = Nothing Then
            Top = SrtTop
        End If
        If Not SrtLeft = Nothing Then
            Left = SrtLeft
        End If
        BlnOnCloseDestroy = BlnOnCloseDestroyVar
        FGMain.DataSource = DVMain
        IniGrid(FGMain)
        FGMain.MultiSelect = True
        FGMain.Location = New System.Drawing.Point(2, 30)
        FGMain.Width = Me.Width - 10
        FGMain.Height = Me.Height - 87
        FGMain.Columns(0).DefaultCellStyle.Font = New Font(New FontFamily("wingdings"), 14)

        If Trim(StrDefaultValue) <> "" Then
            For I = 0 To DVMain.Count - 1
                If UCase(Trim(DVMain.Item(I).Item(1))) = Trim(UCase(StrDefaultValue)) Then
                    DVMain.Item(I).Item(0) = "þ"
                    Exit For
                End If
            Next
        End If
        TxtSearch.Text = FFilterRecursive(DVMain.Table.Columns.Item(1).ColumnName, StrFindText)
    End Sub
    '================================================
    'This Is For Managing Button eg: Selecting,Cancel
    '================================================
    Public Sub FManageButtons(ByVal StrBtnName As String)
        BytBtnValue = 3
        Try
            Select Case UCase(StrBtnName)
                Case UCase(BtnOK.Name)
                    If DVMain.Count > 0 Then
                        BytBtnValue = 0
                        TxtSearch.Text = ""
                        Me.Close()
                    End If
                Case UCase(BtnClose.Name)
                    BytBtnValue = 1
                    TxtSearch.Text = ""
                    Me.Close()
                Case UCase(TSMHold.Name)
                    If DVHold Is Nothing Then DVHold = DVMain
                    DVMain = New DataView(DVMain.ToTable())
                    FGMain.DataSource = DVMain
                Case UCase(TSMRelease.Name)
                    If Not (DVHold Is Nothing) Then DVMain = DVHold : TxtSearch.Text = "" : DVMain.RowFilter = "" : FGMain.DataSource = DVMain
            End Select
        Catch Ex As Exception
            MsgBox("System Exception : " & vbCrLf & Ex.Message, MsgBoxStyle.Exclamation, StrMsgTitle)
            BytBtnValue = 2
            Me.Close()
        End Try
    End Sub

    Private Sub FrmHelpGrid_Multi_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If BytBtnValue = 3 Then
            e.Cancel = True
        End If
        If BlnOnCloseDestroy Then
            DVMain = Nothing
        End If
    End Sub
    '==============================================
    'This Is For Formating Columns Of FGMain
    '==============================================
    Public Sub FFormatColumn(ByVal SrtColIndex As Short, Optional ByVal StrColHeaderText As String = Nothing, _
                         Optional ByVal SrtWidth As Short = Nothing, _
                         Optional ByVal DGVCAlign As DataGridViewContentAlignment = Nothing, _
                         Optional ByVal BlnHideCol As Boolean = True)

        If Not StrColHeaderText = Nothing Then
            FGMain.Columns(SrtColIndex).HeaderText = StrColHeaderText
        End If

        If Not SrtWidth = Nothing Then
            FGMain.Columns(SrtColIndex).Width = SrtWidth
        End If
        If Not DGVCAlign = Nothing Then
            FGMain.Columns(SrtColIndex).DefaultCellStyle.Alignment = DGVCAlign
            FGMain.Columns(SrtColIndex).HeaderCell.Style.Alignment = DGVCAlign
            Select Case DGVCAlign
                Case DataGridViewContentAlignment.MiddleRight, DataGridViewContentAlignment.BottomRight, DataGridViewContentAlignment.TopRight
                    FGMain.Columns(SrtColIndex).SortMode = DataGridViewColumnSortMode.NotSortable
            End Select
        End If
        FGMain.Columns(SrtColIndex).Visible = BlnHideCol
    End Sub
    Private Sub FGMain_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles FGMain.DoubleClick
        FManageButtons(BtnOK.Name)
    End Sub
    Private Sub FGMain_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FGMain.KeyDown
        If e.KeyCode = Keys.Enter Then
            FManageButtons(BtnOK.Name)
        ElseIf e.KeyCode = Keys.Space Then
            FManageTick()
        End If
    End Sub
    Private Sub FGMain_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles FGMain.KeyPress
        Try
            If Asc(e.KeyChar) = Keys.Back Then
                If TxtSearch.Text <> "" Then TxtSearch.Text = Microsoft.VisualBasic.Left(TxtSearch.Text, Len(TxtSearch.Text) - 1)
            End If
            If FGMain.CurrentCell.ColumnIndex <> 0 Then
                TxtSearch_KeyPress(TxtSearch, e)
            End If
        Catch ex As Exception
            MsgBox("System Exception : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, StrMsgTitle)
        End Try
    End Sub
    Private Sub FGMain_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles FGMain.MouseUp
        Try
            TxtSearch.Text = ""
            If e.Button = Windows.Forms.MouseButtons.Left Then FManageTick()
        Catch ex As Exception
            MsgBox("System Exception : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, StrMsgTitle)
        End Try
    End Sub
    Private Sub FGMain_PreviewKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles FGMain.PreviewKeyDown
        Try
            If e.KeyCode = Keys.Delete Then TxtSearch.Text = "" : FGMain.CurrentCell = FGMain(FGMain.CurrentCell.ColumnIndex, 0)
            If e.KeyCode = Keys.Left Then TxtSearch.Text = ""
            If e.KeyCode = Keys.Right Then TxtSearch.Text = ""
        Catch ex As Exception
            MsgBox("System Exception : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, StrMsgTitle)
        End Try
    End Sub
    '=============================================
    'This Function Is For Filtering(Searching) Row
    'And Returning Searched Text
    '==============================================
    Private Function RowsFilter(ByVal e As System.Windows.Forms.KeyPressEventArgs) As Int16
        Try
            Dim StrExpr As String, StrFind As String
            Dim StrValue As String, StrField As String
            Dim SrtCol As Short

            If Not FGMain.Rows.Count > 0 Then Exit Function

            SrtCol = FGMain.CurrentCell.ColumnIndex
            StrField = FGMain.Columns(FGMain.CurrentCell.ColumnIndex).Name

            StrValue = TxtSearch.Text
            If TxtSearch.Text = "(null)" Then
                StrFind = e.KeyChar
            Else
                StrFind = IIf(Asc(e.KeyChar) = Keys.Back Or Asc(e.KeyChar) = 4 Or Asc(e.KeyChar) = 19, TxtSearch.Text, TxtSearch.Text + e.KeyChar)
            End If

            StrExpr = "[" & StrField & "] like '" & StrFind & "%' "
            DVMain.RowFilter = StrExpr
            If Not DVMain.Count > 0 Then
                TxtSearch.Text = FFilterRecursive(StrField, Microsoft.VisualBasic.Left(StrFind, Microsoft.VisualBasic.Len(StrFind) - 1))
            Else
                TxtSearch.Text = TxtSearch.Text + IIf(Asc(e.KeyChar) = Keys.Back Or Asc(e.KeyChar) = 4 Or Asc(e.KeyChar) = 19, "", e.KeyChar)
            End If
            If Asc(e.KeyChar) <> Keys.Back Then e.Handled = True
            FGMain.CurrentCell = FGMain(SrtCol, 0)
        Catch ex As Exception
            MsgBox("System Exception : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, StrMsgTitle)
            DVMain.RowFilter = Nothing
        End Try
    End Function
    '========================================================
    'This Function Is For Filtering(Searching) Row Recursivly
    'And Returning Searched Text
    '========================================================
    Private Function FFilterRecursive(ByVal StrField As String, ByVal StrFind As String) As String
        Dim StrExpr As String
        Try
            StrExpr = "[" & StrField & "] like '" & StrFind & "%' "
            DVMain.RowFilter = StrExpr
            If Not DVMain.Count > 0 Then
                StrFind = FFilterRecursive(StrField, Microsoft.VisualBasic.Left(StrFind, Microsoft.VisualBasic.Len(StrFind) - 1))
            End If
        Catch ex As Exception
            DVMain.RowFilter = Nothing
        End Try
        Return StrFind
    End Function
    Private Sub TxtSearch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Try
            RowsFilter(e)
        Catch ex As Exception
            MsgBox("System Exception : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, StrMsgTitle)
        End Try
    End Sub
    Private Sub FrmHelpGrid_Multi_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            FManageButtons(BtnClose.Name)
        End If
    End Sub
    Private Sub FManageTick()
        Dim I As Integer

        If FGMain.CurrentCell.RowIndex < 0 Then Exit Sub
        If FGMain.CurrentCell.ColumnIndex <> 0 Then Exit Sub

        For I = 0 To FGMain.SelectedCells.Count - 1
            FTick(0, FGMain.SelectedCells.Item(I).RowIndex)
        Next

    End Sub
    Private Sub FTick(ByVal IntColIndex As Integer, ByVal IntRowIndex As Integer)
        If IntRowIndex < 0 Then Exit Sub
        If IntColIndex <> 0 Then Exit Sub

        If FGMain(0, IntRowIndex).Value = "þ" Then
            FGMain(0, IntRowIndex).Value = "o"
        Else
            FGMain(0, IntRowIndex).Value = "þ"
        End If
    End Sub

    Public Function FFetchData(ByVal IntCol As Int16, ByVal StrPrefix As String, ByVal StrSuffix As String, ByVal StrSeprator As String, _
    Optional ByVal BlnInPrimaryCode As Boolean = False) As String
        Dim I As Integer
        Dim StrRtn As String = ""
        Dim DTRow() As DataRow

        Try
            If Not ChkAll.Checked Then
                DVMain.Table.AcceptChanges()
                DTRow = DVMain.Table.Select("Tick='þ'")
                For I = 0 To UBound(DTRow)
                    If StrRtn <> "" Then
                        StrRtn = StrRtn + StrSeprator
                    End If
                    StrRtn = StrRtn + StrPrefix + XNull(DTRow(I).Item(IntCol)) + StrSuffix
                Next
                If StrRtn = "" Then
                    If BlnInPrimaryCode Then
                        StrRtn = ""
                    Else
                        StrRtn = "All"
                    End If
                    ChkAll.Checked = True
                    FManageCheckAllClick()
                End If
            Else
                If BlnInPrimaryCode Then
                    StrRtn = ""
                Else
                    StrRtn = "All"
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, "Information Window ....")
        End Try
        Return StrRtn
    End Function
    Public Sub FRefresh()
        Dim I As Integer
        Dim StrRtn As String = ""
        Dim DTRow() As DataRow

        DTRow = DVMain.Table.Select("Tick='þ'")
        For I = 0 To UBound(DTRow)
            DTRow(I).Item(0) = "o"
        Next
    End Sub
    Private Sub BtnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles BtnClose.Click, BtnOK.Click
        Select Case sender.name
            Case BtnOK.Name, BtnClose.Name
                FManageButtons(sender.Name)
        End Select
    End Sub
    Private Sub TSMHold_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles TSMHold.Click, TSMRelease.Click
        Select Case sender.name
            Case TSMHold.Name, TSMRelease.Name
                FManageButtons(sender.Name)
        End Select
    End Sub
    Private Sub ChkAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkAll.Click
        FManageCheckAllClick()
    End Sub
    Private Sub FManageCheckAllClick()
        If ChkAll.Checked Then
            FGMain.Enabled = False
        Else
            FGMain.Enabled = True
        End If
        FRefresh()
    End Sub
End Class
