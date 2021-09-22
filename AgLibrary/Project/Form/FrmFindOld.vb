Imports System.Data.SqlClient
Imports System.Data.SQLite
Public Class FrmFindOld
    Dim AgL As AgLibrary.ClsMain
    Dim dtf As DataTable
    Dim DTFind As New DataTable
    Dim FindCmd As New SQLiteCommand
    Dim FindTrans As SQLiteTransaction
    Dim fld As String
    Dim mFlag As Boolean
    Dim ColNo As Integer
    Dim FdName As String
    Dim CdName As String
    Dim RwNo As Integer
    Public HlpSt As String
    Dim View_Name As String
    Dim HlpS As String
    ''' <summary>
    ''' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' Forms Events
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>

    Private Sub frmFind_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim i As Integer, j As Integer = 0, k As Integer        '' For Loop
        Dim GWidth As Double                                    '' For Grid Width

        Try
            'Me.BackColor = System.Drawing.ColorConverter(0, 255, 265)


            '' Grid Initialization Process
            ' Dim selSq As String
            ''''Try
            ''''    FindTrans = AgL.GCn.BeginTransaction(IsolationLevel.Serializable)
            ''''    FindCmd.Transaction = FindTrans
            ''''    FindCmd.Connection = AgL.GCn
            ''''    Dim ds_View As New DataSet
            ''''    View_Name = "View_FindOperations_"
            ''''    FindCmd.CommandText = "select Name from sysobjects where name like '" & View_Name & "%' and xtype='v' order by convert(integer,right(name,len(Name)-20)) desc"
            ''''    'FindCmd.CommandText = "select Name from sysobjects where name like '" & View_Name & "%' and xtype='v' order by name desc"
            ''''    If IsDBNull(FindCmd.ExecuteScalar) Or FindCmd.ExecuteScalar Is Nothing Then
            ''''        View_Name = View_Name + "1"
            ''''    Else
            ''''        View_Name = View_Name + CInt((Mid(FindCmd.ExecuteScalar, View_Name.Length + 1)) + 1).ToString
            ''''    End If
            ''''    FindCmd.CommandText = "Create View " & View_Name & " as " & AgL.PubFindQry
            ''''    FindCmd.ExecuteNonQuery()
            ''''    FindTrans.Commit()
            ''''Catch ex As SqlException
            ''''    MsgBox(ex.Message)
            ''''    FindTrans.Rollback()
            ''''End Try
            Dim StSql As String
            'HlpSt = "select * from " & View_Name & ""
            HlpSt = "select * from (" & AgL.PubFindQry & ")"
            HlpS = HlpSt
            StSql = HlpSt
            Try
                GrdFind.ColumnHeadersDefaultCellStyle.Font = New Font(New FontFamily("Arial"), 9)
                GrdFind.DefaultCellStyle.Font = New Font(New FontFamily("Arial"), 8)

                If AgL.PubFindQryOrdBy = "" Then
                Else
                    StSql = StSql + " Order by " + AgL.PubFindQryOrdBy
                End If
                DTFind = AgL.FillData(StSql, AgL.GCn)

                ''Primary Key Assign To Data Table
                Dim mCol(1) As DataColumn

                mCol(0) = DTFind.Columns(0)
                DTFind.PrimaryKey = mCol
                ''====================================

                GrdFind.DataSource = DTFind
            Catch ex As SqlException
                If ex.Number = 207 Then
                    MsgBox("Invalid Orderby Column Name " & AgL.PubFindQryOrdBy)
                Else
                    MsgBox(ex.Message)
                End If


                AgL.PubFindQryOrdBy = ""
            Finally
                AgL.PubFindQryOrdBy = ""
            End Try
            GrdFind.Columns(0).Visible = False
            GrdFind.RowTemplate.Height = 18
            For k = 0 To Me.GrdFind.Columns.Count - 1
                GrdFind.Columns(k).ReadOnly = True
            Next
            i = GrdFind.ColumnCount
            GWidth = DataGridApplyAutoWidths(GrdFind, 100)
            Me.GrdFind.Width = GWidth + 40
            GrdFind.ColumnHeadersHeight = 40
            AgL.PubSearchRow = ""

        Catch ex As Exception
            'FindTrans.Rollback()
            MsgBox(ex.Message)
            Exit Try
        Finally
            Me.Width = GWidth + 50
            If Me.Width > 950 Then Me.Width = 950
            TextBox1.Width = GWidth + 40
            If TextBox1.Width > 950 Then TextBox1.Width = 950
            If i = 2 Then Me.Width = 349 : GrdFind.Width = 349 : GrdFind.Columns(1).Width = 300
            If GrdFind.Width > 950 Then GrdFind.Width = 920
            Me.CenterToScreen()
        End Try
    End Sub

    Private Sub frmFind_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Click
        Try
            GroupBox1.Visible = False
        Catch ex As Exception
        End Try
    End Sub
    Private Sub frmFind_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        ''''Try
        ''''    FindCmd.CommandText = "Drop view " & View_Name & ""
        ''''    FindCmd.ExecuteNonQuery()
        ''''    AgL.PubFindQryOrdBy = ""
        ''''Catch ex As Exception

        ''''End Try
    End Sub
    Private Sub frmFind_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.Escape Then
                Me.Close()
            End If
        Catch ex As Exception
        End Try
    End Sub


    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '' Grid Events
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Private Sub GrdFind_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GrdFind.CellClick
        Try
            GroupBox1.Visible = False
        Catch ex As Exception
        End Try
    End Sub
    ''Double Click event on Grid for returning the selected Row
    Private Sub GrdFind_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GrdFind.DoubleClick
        AgL.PubSearchRow = GrdFind.Item(0, GrdFind.CurrentCell.RowIndex).Value.ToString
        AgL.PubFindQryOrdBy = ""
        mFlag = True
        Me.Close()
    End Sub
    ''Enter press event on Grid for returning the selected Row
    Private Sub GrdFind_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GrdFind.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                AgL.PubSearchRow = GrdFind.Item(0, GrdFind.CurrentCell.RowIndex).Value.ToString
                AgL.PubFindQryOrdBy = ""
                mFlag = True
                Me.Close()
            End If
        Catch ex As Exception
        End Try
    End Sub
    '' BackSpace usage
    Private Sub GrdFind_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles GrdFind.KeyPress
        Try
            If GrdFind.CurrentCell Is Nothing Then
                DTFind.DefaultView.RowFilter = Nothing
            End If

            fld = GrdFind.Columns(GrdFind.CurrentCell.ColumnIndex).Name
            If Asc(e.KeyChar) = Keys.Back Then
                If TextBox1.Text <> "" Then TextBox1.Text = Microsoft.VisualBasic.Left(TextBox1.Text, Len(TextBox1.Text) - 1)
            End If

            TextBox1_KeyPress(TextBox1, e)
        Catch ex As Exception
        End Try
    End Sub

    '' Process for sorting columns on pressing Ctrl + s and Ctrl + D
    Private Sub GrdFind_PreviewKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles GrdFind.PreviewKeyDown
        Try
            If e.Control = True Then
                Select Case e.KeyCode
                    Case Keys.S
                        If GrdFind.CurrentCell.ColumnIndex < GrdFind.ColumnCount - 1 Then
                            If UCase(GrdFind.Columns(GrdFind.CurrentCell.ColumnIndex + 1).Name) = "SEARCH" Then
                                Me.GrdFind.Sort(GrdFind.Columns(GrdFind.CurrentCell.ColumnIndex + 1), System.ComponentModel.ListSortDirection.Ascending)
                            Else
                                Me.GrdFind.Sort(GrdFind.Columns(GrdFind.CurrentCell.ColumnIndex), System.ComponentModel.ListSortDirection.Ascending)
                            End If
                        Else
                            Me.GrdFind.Sort(GrdFind.Columns(GrdFind.CurrentCell.ColumnIndex), System.ComponentModel.ListSortDirection.Ascending)
                        End If
                        ColNo = GrdFind.CurrentCell.ColumnIndex
                    Case Keys.D
                        If GrdFind.CurrentCell.ColumnIndex < GrdFind.ColumnCount - 1 Then
                            If UCase(GrdFind.Columns(GrdFind.CurrentCell.ColumnIndex + 1).Name) = "SEARCH" Then
                                Me.GrdFind.Sort(GrdFind.Columns(GrdFind.CurrentCell.ColumnIndex + 1), System.ComponentModel.ListSortDirection.Descending)
                            Else
                                Me.GrdFind.Sort(GrdFind.Columns(GrdFind.CurrentCell.ColumnIndex), System.ComponentModel.ListSortDirection.Descending)
                            End If
                        Else
                            Me.GrdFind.Sort(GrdFind.Columns(GrdFind.CurrentCell.ColumnIndex), System.ComponentModel.ListSortDirection.Descending)
                        End If
                        ColNo = GrdFind.CurrentCell.ColumnIndex
                End Select
            End If
            If e.KeyCode = Keys.Delete Then TextBox1.Text = "" : DTFind.DefaultView.RowFilter = Nothing : GrdFind.CurrentCell = GrdFind(fld, 0) : DTFind.DefaultView.RowFilter = Nothing

            If e.KeyCode = Keys.Left Or e.KeyCode = Keys.Right Or e.KeyCode = Keys.Up Or e.KeyCode = Keys.Down Then
                TextBox1.Text = ""
            End If


        Catch ex As Exception
        End Try
    End Sub
    '' Code for Click event on Grid
    Private Sub GrdFind_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles GrdFind.Click
        Try
            GroupBox1.Visible = False
            TextBox1.Text = ""
            FdName = GrdFind.Columns(GrdFind.CurrentCell.ColumnIndex).Name
            RwNo = GrdFind.CurrentRow.Index
        Catch ex As Exception
        End Try
    End Sub
    '' Code for Context menu click
    Private Sub mnuFilterNotSame_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuFilterNotSame.Click, mnuFilterSame.Click, mnuRemoveFilter.Click, mnuSortAsc.Click, mnuSortDesc.Click
        Try
            Select Case sender.text
                Case "Filter Similar Records"
                    FilterSameRec() : TextBox1.Text = ""
                Case "Filter Dissimilar Records"
                    FilterNotSameRec() : TextBox1.Text = ""
                Case "Remove All Filter"
                    RemFilter() : TextBox1.Text = ""
                Case "Sort Ascending"
                    If GrdFind.CurrentCell.ColumnIndex < GrdFind.ColumnCount - 1 Then
                        If Microsoft.VisualBasic.Left(UCase(GrdFind.Columns(GrdFind.CurrentCell.ColumnIndex + 1).Name), 6) = "SEARCH" Then
                            Me.GrdFind.Sort(GrdFind.Columns(GrdFind.CurrentCell.ColumnIndex + 1), System.ComponentModel.ListSortDirection.Ascending)
                        Else
                            Me.GrdFind.Sort(GrdFind.Columns(GrdFind.CurrentCell.ColumnIndex), System.ComponentModel.ListSortDirection.Ascending)
                        End If
                    Else
                        Me.GrdFind.Sort(GrdFind.Columns(GrdFind.CurrentCell.ColumnIndex), System.ComponentModel.ListSortDirection.Ascending)
                    End If
                    ColNo = GrdFind.CurrentCell.ColumnIndex : TextBox1.Text = ""
                Case "Sort Descending"
                    If GrdFind.CurrentCell.ColumnIndex < GrdFind.ColumnCount - 1 Then
                        If Microsoft.VisualBasic.Left(UCase(GrdFind.Columns(GrdFind.CurrentCell.ColumnIndex + 1).Name), 6) = "SEARCH" Then
                            Me.GrdFind.Sort(GrdFind.Columns(GrdFind.CurrentCell.ColumnIndex + 1), System.ComponentModel.ListSortDirection.Descending)
                        Else
                            Me.GrdFind.Sort(GrdFind.Columns(GrdFind.CurrentCell.ColumnIndex), System.ComponentModel.ListSortDirection.Descending)
                        End If
                    Else
                        Me.GrdFind.Sort(GrdFind.Columns(GrdFind.CurrentCell.ColumnIndex), System.ComponentModel.ListSortDirection.Descending)
                    End If
                    ColNo = GrdFind.CurrentCell.ColumnIndex : TextBox1.Text = ""
            End Select

        Catch ex As Exception

        End Try
    End Sub
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '' Text Box & Botton Events
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

    '' Textbox KeyPress event for calling Row filteration function

    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        Try
            RowsFilter(HlpSt, GrdFind, sender, e, fld, DTFind)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHelp.Click
        Try
            If GroupBox1.Visible = True Then
                GroupBox1.Visible = False
            Else
                GroupBox1.Visible = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '' User Defined Functions Area
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '' Function For Rows Filteration in Grid while pressing a key on grid

    Private Function RowsFilter(ByVal selStr As String, ByVal CtrlObj As Object, ByVal TXT As TextBox, ByVal e As System.Windows.Forms.KeyPressEventArgs, ByVal FndFldName As String, ByVal DTable As DataTable) As Integer
        Try
            Dim strExpr As String, findStr As String, bSelStr As String = ""
            Dim sa As String
            Dim IntRow As Integer
            Dim i As Integer
            sa = TXT.Text
            bSelStr = selStr

            If sa.Length = 0 And Asc(e.KeyChar) = 8 Then IntRow = 0 : CtrlObj.CurrentCell = CtrlObj(FndFldName, IntRow) : Exit Function
            If TXT.Text = "(null)" Then
                findStr = e.KeyChar
            Else
                findStr = IIf(Asc(e.KeyChar) = Keys.Back Or Asc(e.KeyChar) = 4 Or Asc(e.KeyChar) = 19, TXT.Text, TXT.Text + e.KeyChar)
            End If
            strExpr = "ltrim([" & FndFldName & "])  like '%" & findStr & "%' "
            i = InStr(selStr, "where", CompareMethod.Text)
            If i = 0 Then
                selStr = selStr + " where " + strExpr + "order by [" & FndFldName & "]"
            Else
                selStr = selStr + " and " + strExpr + "order by [" & FndFldName & "]"
            End If

            ''==================================< Filter DTFind For Searching >====================================================
            DTFind.DefaultView.RowFilter = Nothing
            DTFind.DefaultView.RowFilter = " [" & FndFldName & "] like '%" & findStr & "%' "
            ''==================================< *************************** >====================================================

            Dim dtt As New DataTable, j As Integer
            dtt = AgL.FillData(selStr, AgL.GCn)
            If dtt.Rows.Count > 0 Then
                For i = 0 To dtt.Rows.Count - 1
                    For j = 0 To GrdFind.RowCount - 1
                        If GrdFind.Item(FndFldName, j).Value = dtt.Rows(i).Item(FndFldName) Then
                            GrdFind.CurrentCell = GrdFind(FndFldName, j)
                            TXT.Text = TXT.Text + IIf(Asc(e.KeyChar) = Keys.Back Or Asc(e.KeyChar) = 4 Or Asc(e.KeyChar) = 19, "", e.KeyChar)
                            Exit Try
                        End If
                    Next
                Next

            Else
                strExpr = "[" & FndFldName & "]  like '%" & sa & "%' "
                'selStr = selStr + " where " + strExpr + "order by " & FndFldName & ""
                selStr = "Select * From (" & bSelStr & ") xyz where " + strExpr & " order by [" & FndFldName & "]"

                ''==================================< Filter DTFind For Searching >====================================================
                DTFind.DefaultView.RowFilter = Nothing
                DTFind.DefaultView.RowFilter = " [" & FndFldName & "] like '%" & findStr & "%' "
                ''==================================< *************************** >====================================================

                dtt = AgL.FillData(selStr, AgL.GCn)

                If dtt.Rows.Count > 0 Then
                    For i = 0 To dtt.Rows.Count - 1
                        For j = 0 To GrdFind.RowCount - 1
                            If GrdFind.Item(FndFldName, j).Value = dtt.Rows(i).Item(FndFldName) Then
                                GrdFind.CurrentCell = GrdFind(FndFldName, j)
                                TXT.Text = TXT.Text + IIf(Asc(e.KeyChar) = Keys.Back Or Asc(e.KeyChar) = 4 Or Asc(e.KeyChar) = 19, "", e.KeyChar)
                                Exit Try
                            End If
                        Next
                    Next
                Else
                    DTFind.DefaultView.RowFilter = Nothing
                End If
                If Asc(e.KeyChar) <> Keys.Back Then e.Handled = True

            End If
        Catch ex As Exception
        End Try

    End Function
    '' Function For Filtering Same Record
    Private Sub FilterSameRec()
        Try
            Dim dtFilter As New DataTable
            Dim FdVal As String, i As Integer
            FdVal = GrdFind.Item(FdName, RwNo).Value
            i = InStr(HlpSt, "where", CompareMethod.Text)
            If i = 0 Then
                HlpSt = HlpSt + " where [" & FdName & "]= '" & FdVal & "'"
            Else
                HlpSt = HlpSt + " and [" & FdName & "]= '" & FdVal & "'"
            End If
            dtFilter = AgL.FillData(HlpSt, AgL.GCn)
            GrdFind.DataSource = dtFilter
        Catch ex As Exception
        End Try
    End Sub
    '''' Function For Remove filter Record
    Private Sub RemFilter()
        Try
            GrdFind.DataSource = DTFind
            HlpSt = HlpS
        Catch ex As Exception

        End Try
    End Sub
    '' Function For Filtering Not Same Record
    Private Sub FilterNotSameRec()
        Try
            Dim dtFilter As New DataTable, i As Integer
            Dim FdVal As String ', cm As CurrencyManager
            FdVal = GrdFind.Item(FdName, RwNo).Value
            i = InStr(HlpSt, "where", CompareMethod.Text)
            If i = 0 Then
                HlpSt = HlpSt + " where [" & FdName & "]<> '" & FdVal & "'"
            Else
                HlpSt = HlpSt + " and [" & FdName & "]<> '" & FdVal & "'"
            End If
            dtFilter = AgL.FillData(HlpSt, AgL.GCn)

            GrdFind.DataSource = dtFilter
        Catch ex As Exception
        End Try
    End Sub

    Public Function DataGridApplyAutoWidths(ByVal DataGrid As DataGridView, ByVal NumberOfRowsToScan As Integer) As Integer
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
                    If Not IsDBNull(DataGrid.Item(I, iRow).Value) Then
                        Width = System.Math.Max(Width, Graphics.MeasureString(DataGrid.Item(I, iRow).Value.ToString, DataGrid.Font).Width)
                    End If
                Next
                Width = Width + 4
                DataGrid.Columns(I).Width = Width
                mTotalWidth = mTotalWidth + Width
            Next
            If DataGrid.RowHeadersVisible Then
                DataGrid.Width = mTotalWidth
            Else
                DataGrid.Width = mTotalWidth - DataGrid.RowHeadersWidth
            End If
            DataGridApplyAutoWidths = mTotalWidth
        Catch Ex As Exception
            MsgBox(Ex.Message)
        Finally
            Graphics.Dispose()
        End Try
    End Function

    Public Sub New(ByVal AgLibVar As ClsMain)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        AgL = AgLibVar
    End Sub


End Class
