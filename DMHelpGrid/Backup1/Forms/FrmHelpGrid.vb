Public Class FrmHelpGrid
    '==============================================
    'DVMain Is Binded With FGMain And Is Being Used Throughout The Form 
    'DRReturn Is For Returning The Selected Row Back To The Calling Form
    'BytBtnValue Is For Returning Action Done By User eg:[0=Ok],[1=Cancel],[2=Forcely Close]
    '==============================================
    Private DVMain As DataView
    Private DVHold As DataView
    Public DRReturn As DataRow
    Public BytBtnValue As Byte = Nothing '[0=Ok],[1=Cancel],[2=Forcely Close],[3=UnKnown]
    Private BlnOnCloseDestroy As Boolean

    '===================================
    'This Is A Parameterized Constructor
    '===================================
    Public Sub New(ByVal DVPara As DataView, ByVal StrFindText As String, ByVal SrtHeight As Short, ByVal SrtWidth As Short, _
    Optional ByVal SrtTop As Short = Nothing, Optional ByVal SrtLeft As Short = Nothing, Optional ByVal BlnOnCloseDestroyVar As Boolean = True)
        InitializeComponent()
        DVMain = DVPara
        DVPara = Nothing
        Height = SrtHeight + 35
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
        FGMain.Location = New System.Drawing.Point(2, 30)
        FGMain.Width = Me.Width - 10
        FGMain.Height = (Me.Height - 32) - 55
        TxtSearch.Text = FFilterRecursive(DVMain.Table.Columns.Item(1).ColumnName, StrFindText)
    End Sub

    '================================================
    'This Is For Managing Button eg: Selecting,Cancel
    '================================================
    Public Sub FManageButtons(ByVal StrBtnName As String)
        BytBtnValue = 3
        DRReturn = Nothing
        Try
            Select Case UCase(StrBtnName)
                Case UCase(BtnOk.Name)
                    If DVMain.Count > 0 Then
                        DVMain.RowFilter = " " & DVMain.Table.Columns.Item(0).ColumnName & "='" & FGMain(0, FGMain.CurrentCell.RowIndex).Value & "'"
                        DRReturn = DVMain.Item(0).Row
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
                    TxtSearch.Text = ""
                Case UCase(TSMRelease.Name)
                    If Not (DVHold Is Nothing) Then DVMain = DVHold : TxtSearch.Text = "" : DVMain.RowFilter = "" : FGMain.DataSource = DVMain
            End Select
        Catch Ex As Exception
            MsgBox("System Exception : " & vbCrLf & Ex.Message, MsgBoxStyle.Exclamation, StrMsgTitle)
            DRReturn = Nothing
            BytBtnValue = 2
            If BlnOnCloseDestroy Then
                Me.Close()
            Else
                Me.Hide()
            End If
        End Try
    End Sub

    Private Sub FrmHelpGrid_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
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
    Private Sub FGMain_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles FGMain.Click
        Try
            TxtSearch.Text = ""
        Catch ex As Exception
            MsgBox("System Exception : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, StrMsgTitle)
        End Try
    End Sub
    Private Sub FGMain_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles FGMain.DoubleClick
        FManageButtons(BtnOk.Name)
    End Sub
    Private Sub FGMain_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FGMain.KeyDown
        If e.KeyCode = Keys.Enter Then
            FManageButtons(BtnOk.Name)
        End If
    End Sub
    Private Sub FGMain_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles FGMain.KeyPress
        Try
            If Asc(e.KeyChar) = Keys.Back Then
                If TxtSearch.Text <> "" Then TxtSearch.Text = Microsoft.VisualBasic.Left(TxtSearch.Text, Len(TxtSearch.Text) - 1)
            End If
            TxtSearch_KeyPress(TxtSearch, e)
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
            'MsgBox("System Exception : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, StrMsgTitle)
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
    Private Sub FrmHelpGrid_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            FManageButtons(BtnClose.Name)
        End If
    End Sub
    Private Sub BtnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles BtnClose.Click, BtnOk.Click
        Select Case sender.name
            Case BtnClose.Name, BtnOk.Name
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
End Class
