Imports System.Windows.Forms
Public Class AgCalcShowGrid

    Inherits AgControls.AgDataGrid



    Dim Agl As AgLibrary.ClsMain
    Dim AgCl As New AgControls.AgLib




    Dim mReadOnlyColor As System.Drawing.Color = Color.Beige
    Dim mQry$
    Dim mParentCalcGrid As AgCalcGrid
    Dim mIsFixedRows As Boolean = False

    Public Property AgIsFixedRows() As Boolean
        Get
            Return mIsFixedRows
        End Get
        Set(ByVal value As Boolean)
            mIsFixedRows = value
        End Set
    End Property

    Public Property AgParentCalcGrid() As AgCalcGrid
        Get
            Return mParentCalcGrid
        End Get
        Set(ByVal value As AgCalcGrid)
            mParentCalcGrid = value
        End Set
    End Property

    Public Class AgCalcShowGridColumn
        Public Const Col_Charges As Byte = 0
        Public Const Col_Percentage As Byte = 1
        Public Const Col_Amount As Byte = 2
        Public Const Col_CalcGridRow As Byte = 3
        Public Const Col_ValueType As Byte = 4
    End Class

    Sub New()
        IniMe()
    End Sub

    Sub IniMe()
        Try

            Me.Anchor = (AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Bottom)

            Me.ColumnHeadersDefaultCellStyle.Font = New Font(New FontFamily("Arial"), 9)
            Me.DefaultCellStyle.Font = New Font(New FontFamily("Arial"), 8)
            Me.ColumnHeadersHeight = 25
            Me.AllowUserToAddRows = False
            Me.ColumnCount = 0
            Me.RowHeadersVisible = False
            Me.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue
            Me.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
            Dim mColumnNamePrefix As String = "AgFG"
            With AgCl
                .AddAgTextColumn(Me, mColumnNamePrefix & "Charges", 150, 0, "HEAD", True, True, False)
                .AddAgNumberColumn(Me, mColumnNamePrefix & "Percentage", 50, 3, 2, False, " @ ", True, False, True)
                .AddAgNumberColumn(Me, mColumnNamePrefix & "Amount", 90, 10, 2, False, " Amount ", True, False, True)
                .AddAgNumberColumn(Me, mColumnNamePrefix & "CalcGridRow", 100, 3, 0, False, "Calc Grid Row", False, True, False)
                .AddAgTextColumn(Me, mColumnNamePrefix & "ValueType", 170, 0, "ValueType", False, True, False)
            End With
            Me.EnableHeadersVisualStyles = False
        Catch ex As Exception
            MsgBox(ex.Message & " In IniMe function of AgCalcShowGrid ")
        End Try
    End Sub


    Public Sub Ini_Grid()
        Dim I As Integer
        Dim J As Integer
        Try


            Me.RowCount = 1 : Me.Rows.Clear()
            If mIsFixedRows Then
                J = 0
                For I = AgParentCalcGrid.Rows.Count - (AgParentCalcGrid.AgFixedRows) To AgParentCalcGrid.Rows.Count - 1
                    Me.Rows.Add()
                    Me.Item(AgCalcShowGridColumn.Col_Charges, J).Value = AgParentCalcGrid.Item(AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Value
                    Me.Item(AgCalcShowGridColumn.Col_ValueType, J).Value = AgParentCalcGrid.Item(AgCalcGrid.AgCalcGridColumn.Col_Value_Type, I).Value
                    Me.Item(AgCalcShowGridColumn.Col_CalcGridRow, J).Value = I
                    J += 1
                Next
            Else
                J = 0                
                For I = 0 To AgParentCalcGrid.Rows.Count - (AgParentCalcGrid.AgFixedRows + 1)
                    Me.Rows.Add()
                    Me.Item(AgCalcShowGridColumn.Col_Charges, J).Value = AgParentCalcGrid.Item(AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Value
                    Me.Item(AgCalcShowGridColumn.Col_ValueType, J).Value = AgParentCalcGrid.Item(AgCalcGrid.AgCalcGridColumn.Col_Value_Type, I).Value
                    Me.Item(AgCalcShowGridColumn.Col_CalcGridRow, J).Value = I
                    J += 1
                Next
            End If

            Grid_Disp()
        Catch ex As Exception
            MsgBox(ex.Message & " In Ini_Grid function of AgCalcShowGrid ")
        End Try
    End Sub

    Public Sub Grid_Disp()
        Dim I As Integer
        Try

            For I = 0 To Me.Rows.Count - 1
                Select Case UCase(Me.Item(AgCalcShowGridColumn.Col_ValueType, I).Value)
                    Case "FIXEDVALUE"
                        If Me.Rows(I).Visible = False Then Me.Rows(I).Visible = True
                        Me.CurrentCell = Me(AgCalcShowGridColumn.Col_Percentage, I)
                        Me.CurrentCell.ReadOnly = True
                        Me.CurrentCell.Style.BackColor = mReadOnlyColor
                        Me.CurrentCell = Me(AgCalcShowGridColumn.Col_Amount, I)
                        Me.CurrentCell.ReadOnly = True
                        Me.CurrentCell.Style.BackColor = mReadOnlyColor


                        Me.CurrentRow.Visible = CBool(AgParentCalcGrid.Item(AgCalcGrid.AgCalcGridColumn.Col_VisibleInTransactionFooter, Me.Item(AgCalcShowGridColumn.Col_CalcGridRow, I).Value).Value = 1)

                    Case "PERCENTAGE"
                        If Me.Rows(I).Visible = False Then Me.Rows(I).Visible = True
                        Me.CurrentCell = Me(AgCalcShowGridColumn.Col_Percentage, I)
                        Me.CurrentCell.ReadOnly = True
                        Me.CurrentCell.Style.BackColor = mReadOnlyColor
                        Me.CurrentCell = Me(AgCalcShowGridColumn.Col_Amount, I)
                        Me.CurrentCell.ReadOnly = True
                        Me.CurrentCell.Style.BackColor = mReadOnlyColor

                        Me.CurrentRow.Visible = CBool(AgParentCalcGrid.Item(AgCalcGrid.AgCalcGridColumn.Col_VisibleInTransactionFooter, Me.Item(AgCalcShowGridColumn.Col_CalcGridRow, I).Value).Value = 1)




                    Case "FIXEDVALUE CHANGEABLE"
                        If Me.Rows(I).Visible = False Then Me.Rows(I).Visible = True
                        Me.CurrentCell = Me(AgCalcShowGridColumn.Col_Percentage, I)
                        Me.CurrentCell.ReadOnly = True
                        Me.CurrentCell.Style.BackColor = mReadOnlyColor


                        If CBool(AgParentCalcGrid.Item(AgCalcGrid.AgCalcGridColumn.Col_LineItem, Me.Item(AgCalcShowGridColumn.Col_CalcGridRow, I).Value).Value = "1") Then
                            Me.CurrentCell = Me(AgCalcShowGridColumn.Col_Amount, I)
                            Me.CurrentCell.ReadOnly = True
                            Me.CurrentCell.Style.BackColor = mReadOnlyColor
                        End If

                        Me.CurrentRow.Visible = CBool(AgParentCalcGrid.Item(AgCalcGrid.AgCalcGridColumn.Col_VisibleInTransactionFooter, Me.Item(AgCalcShowGridColumn.Col_CalcGridRow, I).Value).Value = 1)
                    Case "PERCENTAGE CHANGEABLE"
                        If Me.Rows(I).Visible = False Then Me.Rows(I).Visible = True
                        Me.CurrentCell = Me(AgCalcShowGridColumn.Col_Amount, I)
                        Me.CurrentCell.ReadOnly = True
                        Me.CurrentCell.Style.BackColor = mReadOnlyColor




                        If CBool(AgParentCalcGrid.Item(AgCalcGrid.AgCalcGridColumn.Col_LineItem, Me.Item(AgCalcShowGridColumn.Col_CalcGridRow, I).Value).Value = "1") Then
                            Me.CurrentCell = Me(AgCalcShowGridColumn.Col_Percentage, I)
                            Me.CurrentCell.ReadOnly = True
                            Me.CurrentCell.Style.BackColor = mReadOnlyColor
                        End If
                        Me.CurrentRow.Visible = CBool(AgParentCalcGrid.Item(AgCalcGrid.AgCalcGridColumn.Col_VisibleInTransactionFooter, Me.Item(AgCalcShowGridColumn.Col_CalcGridRow, I).Value).Value = 1)
                    Case "PERCENTAGE FROM COLUMN"
                        If Me.Rows(I).Visible = False Then Me.Rows(I).Visible = True
                        Me.CurrentCell = Me(AgCalcShowGridColumn.Col_Amount, I)
                        Me.CurrentCell.ReadOnly = True
                        Me.CurrentCell.Style.BackColor = mReadOnlyColor
                        Me.CurrentCell = Me(AgCalcShowGridColumn.Col_Amount, I)
                        Me.CurrentCell.ReadOnly = True
                        Me.CurrentCell.Style.BackColor = mReadOnlyColor

                        Me.CurrentRow.Visible = CBool(AgParentCalcGrid.Item(AgCalcGrid.AgCalcGridColumn.Col_VisibleInTransactionFooter, Me.Item(AgCalcShowGridColumn.Col_CalcGridRow, I).Value).Value = 1)


                    Case "FIXEDVALUE FROM COLUMN"
                        If Me.Rows(I).Visible = False Then Me.Rows(I).Visible = True
                        Me.CurrentCell = Me(AgCalcShowGridColumn.Col_Amount, I)
                        Me.CurrentCell.ReadOnly = True
                        Me.CurrentCell.Style.BackColor = mReadOnlyColor
                        Me.CurrentCell = Me(AgCalcShowGridColumn.Col_Amount, I)
                        Me.CurrentCell.ReadOnly = True
                        Me.CurrentCell.Style.BackColor = mReadOnlyColor

                        Me.CurrentRow.Visible = CBool(AgParentCalcGrid.Item(AgCalcGrid.AgCalcGridColumn.Col_VisibleInTransactionFooter, Me.Item(AgCalcShowGridColumn.Col_CalcGridRow, I).Value).Value = 1)

                    Case Else

                End Select
            Next I
        Catch ex As Exception
            MsgBox(ex.Message & " In Grid_Disp function of AgCalcShowGrid ")
        End Try

    End Sub



    Public Sub MoveRec_FromCalcGrid()
        Dim I As Integer
        Try

            For I = 0 To Me.Rows.Count - 1
                If Me.Item(AgCalcShowGridColumn.Col_CalcGridRow, I).Value.ToString.Trim <> "" Then
                    Me.Item(AgCalcShowGridColumn.Col_Percentage, I).Value = AgParentCalcGrid.Item(AgCalcGrid.AgCalcGridColumn.Col_Percentage, Me.Item(AgCalcShowGridColumn.Col_CalcGridRow, I).Value).Value
                    Me.Item(AgCalcShowGridColumn.Col_Amount, I).Value = AgParentCalcGrid.Item(AgCalcGrid.AgCalcGridColumn.Col_Amount, Me.Item(AgCalcShowGridColumn.Col_CalcGridRow, I).Value).Value
                End If
            Next
        Catch ex As Exception
            MsgBox(ex.Message & " In MoveRec_FromCalcGrid function of AgCalcShowGrid ")
        End Try

    End Sub


    Private Sub ME_EditingControl_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Me.EditingControl_Validating

        Select Case Me.CurrentCell.ColumnIndex
            Case AgCalcShowGridColumn.Col_Percentage
                AgParentCalcGrid.Item(AgCalcGrid.AgCalcGridColumn.Col_Percentage, Me.Item(AgCalcShowGridColumn.Col_CalcGridRow, Me.CurrentCell.RowIndex).Value).Value = Me.Item(AgCalcShowGridColumn.Col_Percentage, Me.CurrentCell.RowIndex).Value
            Case AgCalcShowGridColumn.Col_Amount
                AgParentCalcGrid.Item(AgCalcGrid.AgCalcGridColumn.Col_Amount, Me.Item(AgCalcShowGridColumn.Col_CalcGridRow, Me.CurrentCell.RowIndex).Value).Value = Me.Item(AgCalcShowGridColumn.Col_Amount, Me.CurrentCell.RowIndex).Value
        End Select
        AgParentCalcGrid.Calculation()
        AgParentCalcGrid.Calculation()
    End Sub

End Class

