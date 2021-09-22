Imports System.Windows.Forms
Public Class AgDataGrid

    Inherits DataGridView



    Dim WithEvents Dg As AgDataGrid
    Dim WithEvents TxtFind As New AgTextBox
    Dim WithEvents PBox As New PictureBox

    Dim mAgLib As New AgLib
    Dim mAgValue()() As String
    Dim mCancelEditingControlValidating As Boolean
    Public Event EditingControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
    Public Event EditingControl_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
    Public Event EditingControl_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
    Public Event EditingControl_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
    Public Event EditingControl_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)

    Dim x As New BindingSource
    Dim mSearchMethod As AgLib.TxtSearchMethod
    Dim mReadOnlyColumnColor As System.Drawing.Color = Color.Ivory
	Dim mMandatoryColumn As Integer
    Dim mHelpColumnIndex As Integer = 1
    Dim mGridSearchMethod As AgLib.TxtSearchMethod = AgLib.TxtSearchMethod.Simple

    Dim mLastColumn As Integer = -1
    Dim mSkipReadonlyColumns As Boolean = False
    Dim mAgDataRow As DataGridViewRow = Nothing
    Dim mAgAllowFind As Boolean = True
    Dim IsControlPressed As Boolean = False
    Dim IsEnterPressed As Boolean = False
    Dim ControlPressedData As String

    Sub New()
    End Sub

    Public Enum TxtValueType
        Text_Value = 0
        Number_Value = 1
        Date_Value = 2
        YesNo_Value = 3
    End Enum

    Public Enum TxtCase
        None = 0
        Upper_Case = 1
        Lower_Case = 2
        Sentance_Case = 3
        Proper_Case = 4
    End Enum

    Public Property AgAllowFind() As Boolean
        Get
            AgAllowFind = mAgAllowFind
        End Get
        Set(ByVal value As Boolean)
            mAgAllowFind = value
        End Set
    End Property


    Public Property AgSkipReadOnlyColumns() As Boolean
        Get
            AgSkipReadOnlyColumns = mSkipReadonlyColumns
        End Get
        Set(ByVal value As Boolean)
            mSkipReadonlyColumns = value
        End Set
    End Property

    Public ReadOnly Property AgDataRow() As DataGridViewRow
        Get
            AgDataRow = mAgDataRow
        End Get
    End Property


    Public Property AgLastColumn() As Integer
        Get
            If mLastColumn > -1 Then
                AgLastColumn = mLastColumn
            Else
                AgLastColumn = Me.Columns.Count - 1
            End If
        End Get
        Set(ByVal value As Integer)
            mLastColumn = value
        End Set
    End Property

    Public Property CancelEditingControlValidating() As Boolean
        Get
            CancelEditingControlValidating = mCancelEditingControlValidating
        End Get
        Set(ByVal value As Boolean)
            mCancelEditingControlValidating = value
        End Set
    End Property


    Public Property AgAllowDuplicate(ByVal mColumn As Integer) As Boolean
        Get
            If TypeOf Me.Columns(mColumn) Is AgTextColumn Then
                Return CType(Me.Columns(mColumn), AgTextColumn).AgAllowDuplicate
            ElseIf TypeOf Me.Columns(mColumn) Is AgComboColumn Then
                Return CType(Me.Columns(mColumn), AgComboColumn).AgAllowDuplicate
            Else
                Return Nothing
            End If
        End Get
        Set(ByVal value As Boolean)
            If TypeOf Me.Columns(mColumn) Is AgTextColumn Then
                CType(Me.Columns(mColumn), AgTextColumn).AgAllowDuplicate = value
            ElseIf TypeOf Me.Columns(mColumn) Is AgComboColumn Then
                CType(Me.Columns(mColumn), AgComboColumn).AgAllowDuplicate = value
            End If
        End Set
    End Property

    Public Property AgSearchMethod() As AgLib.TxtSearchMethod
        Get
            AgSearchMethod = mSearchMethod
        End Get
        Set(ByVal value As AgLib.TxtSearchMethod)
            mSearchMethod = value
        End Set
    End Property

    Public Property GridSearchMethod() As AgLib.TxtSearchMethod
        Get
            GridSearchMethod = mGridSearchMethod
        End Get
        Set(ByVal value As AgLib.TxtSearchMethod)
            mGridSearchMethod = value
        End Set
    End Property

    Public Property AgReadOnlyColumnColor() As System.Drawing.Color
        Get
            AgReadOnlyColumnColor = mReadOnlyColumnColor
        End Get
        Set(ByVal value As System.Drawing.Color)
            mReadOnlyColumnColor = value
        End Set
    End Property

    Public Property AgDefaultValue(ByVal mColumn As Integer) As String
        Get
            If TypeOf Me.Columns(mColumn) Is AgTextColumn Then
                Return CType(Me.Columns(mColumn), AgTextColumn).AgDefaultValue
            Else
                Return Nothing
            End If
        End Get
        Set(ByVal value As String)
            If TypeOf Me.Columns(mColumn) Is AgTextColumn Then
                CType(Me.Columns(mColumn), AgTextColumn).AgDefaultValue = value
            End If
        End Set
    End Property

    Public Property AgMandatoryColumn() As Integer
        Get
            AgMandatoryColumn = mMandatoryColumn
        End Get
        Set(ByVal value As Integer)
            mMandatoryColumn = value
        End Set
    End Property

    Public Property AgDefaultValue(ByVal mColumn As String) As String
        Get
            If TypeOf Me.Columns(mColumn) Is AgTextColumn Then
                Return CType(Me.Columns(mColumn), AgTextColumn).AgDefaultValue
            Else
                Return Nothing
            End If
        End Get
        Set(ByVal value As String)
            If TypeOf Me.Columns(mColumn) Is AgTextColumn Then
                CType(Me.Columns(mColumn), AgTextColumn).AgDefaultValue = value
            End If
        End Set
    End Property


    Public Property AgRowFilter(ByVal mColumn As Integer) As String
        Get
            If TypeOf Me.Columns(mColumn) Is AgTextColumn Then
                Return CType(Me.Columns(mColumn), AgTextColumn).AgRowFilter
            Else
                Return Nothing
            End If
        End Get
        Set(ByVal value As String)
            If TypeOf Me.Columns(mColumn) Is AgTextColumn Then
                CType(Me.Columns(mColumn), AgTextColumn).AgRowFilter = value
            End If
        End Set
    End Property



    Public Property AgHelpDataSet(ByVal mColumn As Integer, Optional ByVal LastHiddenColumns As Integer = 0, Optional ByVal TopOfContainer As Integer = 0, Optional ByVal LeftOfContainer As Integer = 0, Optional ByVal Height As Integer = 0, Optional ByVal IsMasterHelp As Boolean = False) As DataSet
        Get
            If TypeOf Me.Columns(mColumn) Is AgTextColumn Then
                Return CType(Me.Columns(mColumn), AgTextColumn).AgHelpDataSet
            Else
                Return Nothing
            End If
        End Get
        Set(ByVal value As DataSet)
            AgHelpDataSetAction(value, mColumn, LastHiddenColumns, TopOfContainer, LeftOfContainer, Height, IsMasterHelp)
            AgCreateHelpGrid()
        End Set
    End Property

    Public Property AgHelpDataSet(ByVal mColumnName As String, Optional ByVal LastHiddenColumns As Integer = 0, Optional ByVal TopOfContainer As Integer = 0, Optional ByVal LeftOfContainer As Integer = 0, Optional ByVal Height As Integer = 0, Optional ByVal IsMasterHelp As Boolean = False) As DataSet
        Get
            Dim mColumn As Integer
            mColumn = Me.Columns(mColumnName).Index
            If TypeOf Me.Columns(mColumn) Is AgTextColumn Then
                Return CType(Me.Columns(mColumn), AgTextColumn).AgHelpDataSet
            Else
                Return Nothing
            End If
        End Get
        Set(ByVal value As DataSet)
            Dim mColumn As Integer
            mColumn = Me.Columns(mColumnName).Index

            AgHelpDataSetAction(value, mColumn, LastHiddenColumns, TopOfContainer, LeftOfContainer, Height, IsMasterHelp)
            AgCreateHelpGrid()
        End Set
    End Property

    Sub AgHelpDataSetAction(ByVal Value As DataSet, ByVal mColumn As Integer, Optional ByVal LastHiddenColumns As Integer = 0, Optional ByVal TopOfContainer As Integer = 0, Optional ByVal LeftOfContainer As Integer = 0, Optional ByVal Height As Integer = 0, Optional ByVal IsMasterHelp As Boolean = False)
        If TypeOf Me.Columns(mColumn) Is AgTextColumn Then
            CType(Me.Columns(mColumn), AgTextColumn).AgHelpDataSet = Value
            CType(Me.Columns(mColumn), AgTextColumn).AgTopOfContainer = TopOfContainer
            CType(Me.Columns(mColumn), AgTextColumn).AgLeftOfContainer = LeftOfContainer
            CType(Me.Columns(mColumn), AgTextColumn).AgHelpGridHeight = Height
            CType(Me.Columns(mColumn), AgTextColumn).AgLastHiddenColumns = LastHiddenColumns
            CType(Me.Columns(mColumn), AgTextColumn).AgMasterHelp = IsMasterHelp

            If CType(Me.Columns(mColumn), AgTextColumn).AgHelpDataSet IsNot Nothing Then
                CType(Me.Columns(mColumn), AgTextColumn).AgHelpDataSet.Tables(0).DefaultView.Sort = CType(Me.Columns(mColumn), AgTextColumn).AgHelpDataSet.Tables(0).Columns(1).ColumnName
            End If
            'Dim PkCol(1) As DataColumn
            'PkCol(0) = CType(Me.Columns(mColumn), AgTextColumn).AgHelpDataSet.Tables(0).Columns(0)
            'CType(Me.Columns(mColumn), AgTextColumn).AgHelpDataSet.Tables(0).PrimaryKey = PkCol
        End If
    End Sub


    Public ReadOnly Property AgDatasetAbsolutePosition(ByVal mColumn As Integer) As Long
        Get
            If Dg IsNot Nothing Then
                If Dg.CurrentCell IsNot Nothing Then
                    Return Dg.CurrentCell.RowIndex
                Else
                    Return -1
                End If
            Else
                Return -1
            End If
        End Get
    End Property


    Public Property AgSelectedValue(ByVal mColumn As Integer, ByVal mRow As Integer) As String
        Get
            Return Me.Item(mColumn, mRow).Tag
        End Get
        Set(ByVal value As String)
            Me.Item(mColumn, mRow).Tag = value

            AgSelectedValueAction(mColumn, mRow, value)
        End Set
    End Property

    Public Property AgSelectedValue(ByVal mColumnName As String, ByVal mRow As Integer) As String
        Get
            Dim mColumn As Integer
            mColumn = Me.Columns(mColumnName).Index
            Return Me.Item(mColumn, mRow).Tag
        End Get
        Set(ByVal value As String)
            Dim mColumn As Integer
            mColumn = Me.Columns(mColumnName).Index

            Me.Item(mColumn, mRow).Tag = value

            AgSelectedValueAction(mColumn, mRow, value)
        End Set
    End Property
    Private Sub AgSelectedValueAction(ByVal mColumn As Integer, ByVal mRow As Integer, ByVal Value As String)

        Dim mAgTxtColumn As New AgTextColumn
        'Dim I As Long, mFound As Boolean = False
        Dim DrTemp As DataRow() = Nothing

        If TypeOf Me.Rows(mRow).Cells(mColumn).OwningColumn Is AgTextColumn Then
            mAgTxtColumn = Me.Rows(mRow).Cells(mColumn).OwningColumn
            If mAgTxtColumn.AgHelpDataSet IsNot Nothing And Value <> "" Then

                DrTemp = mAgTxtColumn.AgHelpDataSet.Tables(0).Select("" & mAgTxtColumn.AgHelpDataSet.Tables(0).Columns(0).ColumnName & " = '" & Value & "'")
                If DrTemp.Length > 0 Then
                    If Me.EditingControl IsNot Nothing Then Me.EditingControl.Text = mAgLib.XNull(DrTemp(0)(1))
                    Me.Item(mColumn, mRow).Value = mAgLib.XNull(DrTemp(0)(1))
                Else
                    MsgBox("Corresponding Data not found!", MsgBoxStyle.OkOnly, mAgTxtColumn.Name)
                End If
            Else
                Me.Item(mColumn, mRow).Value = ""
            End If
        End If

        'Dim mAgTxtColumn As New AgTextColumn
        'Dim I As Long, mFound As Boolean = False
        'If TypeOf Me.Rows(mRow).Cells(mColumn).OwningColumn Is AgTextColumn Then
        '    mAgTxtColumn = Me.Rows(mRow).Cells(mColumn).OwningColumn
        '    If mAgTxtColumn.AgHelpDataSet IsNot Nothing And Value <> "" Then
        '        For I = 0 To mAgTxtColumn.AgHelpDataSet.Tables(0).Rows.Count - 1
        '            If UCase(mAgTxtColumn.AgHelpDataSet.Tables(0).Rows(I)(0)) = UCase(Value) Then
        '                If Me.EditingControl IsNot Nothing Then Me.EditingControl.Text = mAgLib.XNull(mAgTxtColumn.AgHelpDataSet.Tables(0).Rows(I)(1))
        '                Me.Item(mColumn, mRow).Value = mAgLib.XNull(mAgTxtColumn.AgHelpDataSet.Tables(0).Rows(I)(1))
        '                mFound = True : Exit For
        '            End If
        '        Next
        '        If Not mFound Then
        '            MsgBox("Corresponding Data not found!")
        '        End If
        '    Else
        '        Me.Item(mColumn, mRow).Value = ""
        '    End If
        'End If

    End Sub


    <System.Security.Permissions.UIPermission(
    System.Security.Permissions.SecurityAction.LinkDemand, Window:=System.Security.Permissions.UIPermissionWindow.AllWindows)>
    Protected Overrides Function ProcessDialogKey(ByVal keyData As Keys) As Boolean ' Extract the key code from the key value. 
        Dim key As Keys = keyData And Keys.KeyCode ' Handle the ENTER key as if it were a RIGHT ARROW key.
        Try

            If key = Keys.Enter Then
                If mCancelEditingControlValidating Then
                    keyData = 0
                    'Return Me.ProcessZeroKey(keyData)
                    mCancelEditingControlValidating = False
                Else
                    If Not IsControlPressed Then
                        Try
                            Return FProcessDataGridViewKey()
                            'Return Me.ProcessTabKey(keyData)
                            keyData = 0
                        Catch ex As Exception
                        End Try
                    End If
                End If

            End If


            If Me.CurrentCell IsNot Nothing Then
                If TypeOf Me.CurrentCell.OwningColumn Is AgTextColumn Then
                    If CType(Me.CurrentCell.OwningColumn, AgTextColumn).AgHelpDataSet IsNot Nothing Then
                        If Dg IsNot Nothing Then
                            If Dg.Visible = True Then
                                Select Case key
                                    Case Keys.Up
                                        If Dg.CurrentCell IsNot Nothing Then
                                            If Dg.CurrentCell.RowIndex >= 1 Then
                                                Dg.CurrentCell = Dg(Dg.CurrentCell.ColumnIndex, Dg.CurrentCell.RowIndex - 1)
                                                Me.AgSelectedValue(Me.CurrentCell.ColumnIndex, Me.CurrentCell.RowIndex) = mAgLib.XNull(Dg.Item(0, Dg.CurrentCell.RowIndex).Value)
                                            End If
                                        Else
                                            Dg.CurrentCell = Dg(1, 1)
                                            Me.AgSelectedValue(Me.CurrentCell.ColumnIndex, Me.CurrentCell.RowIndex) = mAgLib.XNull(Dg.Item(0, Dg.CurrentCell.RowIndex).Value)
                                        End If
                                        Return Me.ProcessZeroKey(keyData)
                                    Case Keys.Down
                                        If Dg.CurrentCell IsNot Nothing Then
                                            If Dg.CurrentCell.RowIndex <= Dg.Rows.Count - 2 Then
                                                Dg.CurrentCell = Dg(Dg.CurrentCell.ColumnIndex, Dg.CurrentCell.RowIndex + 1)
                                                Me.AgSelectedValue(Me.CurrentCell.ColumnIndex, Me.CurrentCell.RowIndex) = mAgLib.XNull(Dg.Item(0, Dg.CurrentCell.RowIndex).Value)
                                            End If
                                        Else
                                            Dg.CurrentCell = Dg(1, Dg.Rows.Count - 1)
                                            Me.AgSelectedValue(Me.CurrentCell.ColumnIndex, Me.CurrentCell.RowIndex) = mAgLib.XNull(Dg.Item(0, Dg.CurrentCell.RowIndex).Value)
                                        End If
                                        Return Me.ProcessZeroKey(keyData)
                                    Case Keys.Escape
                                        Me.Focus()
                                        Dg.Visible = False
                                        Return Me.ProcessTabKey(keyData)
                                End Select
                            End If
                        End If
                    End If
                End If
            End If

            Return MyBase.ProcessDialogKey(keyData)
        Catch ex As Exception
        End Try
    End Function

    <System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.LinkDemand, Flags:=System.Security.Permissions.SecurityPermissionFlag.UnmanagedCode)>
    Protected Overrides Function ProcessDataGridViewKey(ByVal e As System.Windows.Forms.KeyEventArgs) As Boolean ' Handle the ENTER key as if it were a RIGHT ARROW key. 



        Try

            If e.KeyCode = Keys.Enter Then
                If mCancelEditingControlValidating Then
                    'Return Me.ProcessZeKey(e.KeyData)
                    e.Handled = True
                    mCancelEditingControlValidating = False
                Else
                    If Not IsControlPressed Then
                        Return FProcessDataGridViewKey()
                        'Return Me.ProcessTabKey(e.KeyData)
                        e.Handled = True
                    Else
                        If Me.EditingControl.Visible = True Then
                            'Me.EditingControl.Text = ControlPressedData & vbCrLf
                            'CType(Me.EditingControl, TextBox).Select(0, Me.EditingControl.Text.Length)
                            CType(Me.EditingControl, TextBox).SelectedText = vbCrLf
                        End If
                        IsEnterPressed = True
                        e.Handled = True
                        Exit Function
                    End If
                End If

            End If

            If Me.CurrentCell IsNot Nothing Then
                If TypeOf Me.CurrentCell.OwningColumn Is AgTextColumn Then
                    If CType(Me.CurrentCell.OwningColumn, AgTextColumn).AgHelpDataSet IsNot Nothing Then
                        If Dg IsNot Nothing Then
                            If Dg.Visible = True Then
                                Select Case e.KeyCode
                                    Case Keys.Up
                                        If Dg.CurrentCell IsNot Nothing Then
                                            If Dg.CurrentCell.RowIndex >= 1 Then
                                                Dg.CurrentCell = Dg(Dg.CurrentCell.ColumnIndex, Dg.CurrentCell.RowIndex - 1)
                                                Me.AgSelectedValue(Me.CurrentCell.ColumnIndex, Me.CurrentCell.RowIndex) = mAgLib.XNull(Dg.Item(0, Dg.CurrentCell.RowIndex).Value)
                                            End If
                                        Else
                                            Dg.CurrentCell = Dg(1, 1)
                                            Me.AgSelectedValue(Me.CurrentCell.ColumnIndex, Me.CurrentCell.RowIndex) = mAgLib.XNull(Dg.Item(0, Dg.CurrentCell.RowIndex).Value)
                                        End If
                                        Return Me.ProcessZeroKey(e.KeyData)
                                    Case Keys.Down
                                        If Dg.CurrentCell IsNot Nothing Then
                                            If Dg.CurrentCell.RowIndex <= Dg.Rows.Count - 2 Then
                                                Dg.CurrentCell = Dg(Dg.CurrentCell.ColumnIndex, Dg.CurrentCell.RowIndex + 1)
                                                Me.AgSelectedValue(Me.CurrentCell.ColumnIndex, Me.CurrentCell.RowIndex) = mAgLib.XNull(Dg.Item(0, Dg.CurrentCell.RowIndex).Value)
                                            End If
                                        Else
                                            Dg.CurrentCell = Dg(1, Dg.Rows.Count - 1)
                                            Me.AgSelectedValue(Me.CurrentCell.ColumnIndex, Me.CurrentCell.RowIndex) = mAgLib.XNull(Dg.Item(0, Dg.CurrentCell.RowIndex).Value)
                                        End If
                                        Return Me.ProcessZeroKey(e.KeyData)
                                    Case Keys.Escape
                                        Me.Focus()
                                        Dg.Visible = False
                                        Return Me.ProcessTabKey(e.KeyData)
                                End Select
                            End If


                        End If
                    End If
                End If
            End If

            Return MyBase.ProcessDataGridViewKey(e)
        Catch ex As Exception
        End Try
    End Function

    Public Function FProcessDataGridViewKey() As Boolean
        Try
            If AgSkipReadOnlyColumns Then
                If IsNothing(Me.CurrentCell) Then
                    Return True
                End If

                Dim nextColumn As DataGridViewColumn
                Dim nextRow As Integer
                nextColumn = Me.Columns.GetNextColumn(Me.Columns(Me.CurrentCell.ColumnIndex), DataGridViewElementStates.Visible, DataGridViewElementStates.ReadOnly)

                If Not IsNothing(nextColumn) And Not Me.CurrentCell.ColumnIndex = AgLastColumn Then
                    Me.CurrentCell = Me.Rows(Me.CurrentCell.RowIndex).Cells(nextColumn.Index)
                Else
                    nextColumn = Me.Columns.GetFirstColumn(DataGridViewElementStates.Visible, DataGridViewElementStates.ReadOnly)
                    nextRow = Me.Rows.GetNextRow(Me.CurrentCell.RowIndex, DataGridViewElementStates.Visible)
                    If (Me.CurrentCell.RowIndex + 1) = Me.Rows.Count Then
                        SendKeys.Send("{Tab}")
                        'Me.CurrentCell = Me.Rows(0).Cells(nextColumn.Index)
                    Else
                        'Me.CurrentCell = Me.Rows(Me.CurrentCell.RowIndex + 1).Cells(nextColumn.Index)
                        Me.CurrentCell = Me.Rows(nextRow).Cells(nextColumn.Index)
                    End If
                End If
            Else
                SendKeys.Send("{Tab}")
            End If
            Return True
        Catch ex As Exception
        End Try
    End Function

    Private Sub AgDataGrid_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Me.CellEnter
        'If TypeOf Me.CurrentCell.OwningColumn Is AgTextColumn Then
        '    If CType(Me.CurrentCell.OwningColumn, AgTextColumn).AgHelpDataSet IsNot Nothing Then
        '        If Dg IsNot Nothing Then
        '            If Dg.Visible = False Then Dg.Visible = True
        '        End If
        '    End If
        'End If
    End Sub


    Private Sub CustomDataGridView_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles Me.DataError
        If e.Exception.Message = "DataGridViewComboBoxCell value is not valid." Then
            e.Cancel = True
        End If
    End Sub


    Private Sub AgDataGrid_EditingControl_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Me.EditingControl_Validating
        Dim I As Integer
        If Me.CurrentCell.ColumnIndex = mMandatoryColumn Then
            For I = 0 To Me.ColumnCount - 1
                Try
                    If Me.Item(I, Me.CurrentCell.RowIndex).Value Is Nothing Then Me.Item(I, Me.CurrentCell.RowIndex).Value = ""
                Catch ex As Exception

                End Try
                If I <> mMandatoryColumn And Me.Item(I, Me.CurrentCell.RowIndex).Value.ToString = "" Then
                    Me.Item(I, Me.CurrentCell.RowIndex).Value = Me.AgDefaultValue(I)
                End If
            Next
        End If

        If IsControlPressed And IsEnterPressed Then
            e.Cancel = True
        End If


        IsControlPressed = False
        IsEnterPressed = False
        ControlPressedData = ""
    End Sub


    Private Sub CustomDataGridView_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles Me.EditingControlShowing
        If TypeOf e.Control Is DataGridViewComboBoxEditingControl Then
            e.Control.Text = ""
            CType(e.Control, ComboBox).SelectedIndex = -1
            With DirectCast(e.Control, System.Windows.Forms.ComboBox)
                .DropDownStyle = ComboBoxStyle.DropDown
                .AutoCompleteSource = AutoCompleteSource.ListItems
                .AutoCompleteMode = AutoCompleteMode.Suggest
            End With
        End If

        RemoveHandler e.Control.KeyDown, AddressOf DgKeyDown
        AddHandler e.Control.KeyDown, AddressOf DgKeyDown

        RemoveHandler e.Control.KeyPress, AddressOf DgKeyPress
        AddHandler e.Control.KeyPress, AddressOf DgKeyPress

        RemoveHandler e.Control.KeyUp, AddressOf DgKeyUp
        AddHandler e.Control.KeyUp, AddressOf DgKeyUp

        RemoveHandler e.Control.Validating, AddressOf DgValidating
        AddHandler e.Control.Validating, AddressOf DgValidating

        RemoveHandler e.Control.LostFocus, AddressOf DgLostFocus
        AddHandler e.Control.LostFocus, AddressOf DgLostFocus

        AgCreateHelpGrid()
    End Sub
    Public Sub AgCreateHelpGrid()
        If Me.CurrentCell Is Nothing Then Exit Sub
        If TypeOf Me.CurrentCell.OwningColumn Is AgTextColumn Then
            If CType(Me.CurrentCell.OwningColumn, AgTextColumn).AgHelpDataSet IsNot Nothing Then
                If Me.FindForm.Controls("HelpDg") IsNot Nothing Then
                    Me.FindForm.Controls("HelpDg").Dispose()
                End If

                Dg = New AgDataGrid
                Me.FindForm.Controls.Add(Dg)
                Dg.Name = "HelpDg"
                Dg.ColumnHeadersDefaultCellStyle.Font = New Font("Verdana", 9, FontStyle.Bold)
                Dg.DefaultCellStyle.Font = New Font("Verdana", 9, FontStyle.Bold)
                Dg.AgAllowFind = False
                Dg.BackgroundColor = Color.Ivory
                Dg.Visible = False
                Dg.Height = IIf(CType(Me.CurrentCell.OwningColumn, AgTextColumn).AgHelpGridHeight > 0, CType(Me.CurrentCell.OwningColumn, AgTextColumn).AgHelpGridHeight, 200)

                If CType(Me.CurrentCell.OwningColumn, AgTextColumn).AgTopOfContainer > 0 Then
                    Dg.Top = Me.Top + CType(Me.CurrentCell.OwningColumn, AgTextColumn).AgTopOfContainer + Me.ColumnHeadersHeight + Me.GetCellDisplayRectangle(Me.CurrentCell.ColumnIndex, Me.CurrentCell.RowIndex, False).Top - 1
                Else
                    Dg.Top = Me.Top + Me.ColumnHeadersHeight + Me.GetCellDisplayRectangle(Me.CurrentCell.ColumnIndex, Me.CurrentCell.RowIndex, False).Top - 1
                End If
                If Me.RowHeadersVisible Then
                    If CType(Me.CurrentCell.OwningColumn, AgTextColumn).AgLeftOfContainer > 0 Then
                        Dg.Left = Me.Left + CType(Me.CurrentCell.OwningColumn, AgTextColumn).AgLeftOfContainer + Me.RowHeadersWidth + Me.GetCellDisplayRectangle(Me.CurrentCell.ColumnIndex, Me.CurrentCell.RowIndex, False).Left - 5
                    Else
                        Dg.Left = Me.Left + Me.RowHeadersWidth + Me.GetCellDisplayRectangle(Me.CurrentCell.ColumnIndex, Me.CurrentCell.RowIndex, False).Left - 5
                    End If
                Else
                    If CType(Me.CurrentCell.OwningColumn, AgTextColumn).AgLeftOfContainer > 0 Then
                        Dg.Left = Me.Left + CType(Me.CurrentCell.OwningColumn, AgTextColumn).AgLeftOfContainer + Me.GetCellDisplayRectangle(Me.CurrentCell.ColumnIndex, Me.CurrentCell.RowIndex, False).Left
                    Else
                        Dg.Left = Me.Left + Me.GetCellDisplayRectangle(Me.CurrentCell.ColumnIndex, Me.CurrentCell.RowIndex, False).Left
                    End If
                End If


                If CType(Me.CurrentCell.OwningColumn, AgTextColumn).AgRowFilter <> "" Then
                    CType(Me.CurrentCell.OwningColumn, AgTextColumn).AgHelpDataSet.Tables(0).DefaultView.RowFilter = Nothing
                    CType(Me.CurrentCell.OwningColumn, AgTextColumn).AgHelpDataSet.Tables(0).DefaultView.RowFilter = CType(Me.CurrentCell.OwningColumn, AgTextColumn).AgRowFilter
                End If

                Dg.DataSource = CType(Me.CurrentCell.OwningColumn, AgTextColumn).AgHelpDataSet.Tables(0).DefaultView
                If Dg.Columns.Count <= 2 Then
                    Dg.ColumnHeadersVisible = False
                End If
                Dg.RowHeadersVisible = False
                Dg.BringToFront()

                'x.AgSetDataGridAutoWidths(Dg, 100, 100)
                'Dg.Columns(0).Visible = False
                'Dg.Columns(1).Width = Me.Columns(Me.CurrentCell.ColumnIndex).Width
                'Dg.Width = 0
                'For I = 1 To Dg.Columns.Count - 1
                '    If I > (Dg.ColumnCount - 1 - CType(Me.CurrentCell.OwningColumn, AgTextColumn).AgLastHiddenColumns) Then
                '        Dg.Columns(I).Visible = False
                '    Else
                '        Dg.Width = Dg.Width + 4 + Dg.Columns(I).Width
                '        Dg.Columns(I).ToolTipText = "Click on respective column for searching!..."
                '    End If
                'Next
                'If dg.width > (Dg.FindForm.Width + 50) Then Dg.Width = Dg.FindForm.Width - 100
                'Dg.Width = Dg.Width - IIf(Dg.RowHeadersVisible, Dg.RowHeadersWidth, 0) + 25

                Call ProcSetHelpDgWidth()

                If CType(Me.CurrentCell.OwningColumn, AgTextColumn).AgLeftOfContainer > 0 Then
                    If Dg.Left + Dg.Width > Me.FindForm.Left + Me.FindForm.Width Then
                        Dg.Left = (Me.FindForm.Left + Me.FindForm.Width) - Dg.Width
                    End If
                Else
                    If Dg.Left + Dg.Width > Me.FindForm.Left + Me.FindForm.Width Then
                        Dg.Left = (Me.FindForm.Left + Me.FindForm.Width) - Dg.Width
                    End If
                End If
                Dg.ReadOnly = True
                Dg.TabStop = False
            End If
        End If

    End Sub

    Private Sub DgKeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Dim sTypedText As String
        Dim iFoundIndex As Integer
        Dim mAgLib As New AgLib
        If TypeOf sender Is DataGridViewComboBoxEditingControl Then
            Select Case e.KeyChar
                Case Chr(Keys.Back), Chr(Keys.Delete)
                Case Chr(Keys.Enter), Chr(Keys.Return), Chr(Keys.Tab), Chr(3)
                Case Else
                    sTypedText = sender.Text + e.KeyChar
                    iFoundIndex = sender.FindString(sTypedText)
                    If iFoundIndex < 0 Then
                        e.KeyChar = ""
                    End If
            End Select
        Else
            If Me.CurrentCell Is Nothing Then Exit Sub
            If TypeOf Me.CurrentCell.OwningColumn Is AgTextColumn Then

                With CType(Me.CurrentCell.OwningColumn, AgTextColumn)

                    If e.KeyChar = Chr(Keys.Return) Or e.KeyChar = Chr(Keys.Tab) Then
                        If .AgMandatory = True Then
                            If sender.Text = "" And .AgValueType <> TxtValueType.Number_Value Then
                                MsgBox("Required Field" & vbCrLf & "Can't Be Blank!")
                                e.Handled = True
                            ElseIf Val(sender.Text) = 0 And .AgValueType = TxtValueType.Number_Value Then
                                MsgBox("Required Field" & vbCrLf & "Can't Be Blank/Zero!")
                                e.Handled = True
                            End If
                        End If
                    End If


                    Select Case .AgValueType
                        Case TxtValueType.Number_Value
                            If .AgNumberLeftPlaces > 0 Or .AgNumberRightPlaces > 0 Then
                                NumPress(sender, e, .AgNumberLeftPlaces, .AgNumberRightPlaces, .AgNumberNegetiveAllow)
                            End If
                        Case TxtValueType.YesNo_Value
                            If e.KeyChar.ToString.ToUpper = "Y" Then
                                Me.Text = "Yes"
                            ElseIf e.KeyChar.ToString.ToUpper = "N" Then
                                Me.Text = "No"
                            End If
                            e.KeyChar = ""
                        Case TxtValueType.Text_Value
                            Select Case .AgTxtCase
                                Case TxtCase.Lower_Case
                                    e.KeyChar = e.KeyChar.ToString.ToLower
                                Case TxtCase.Upper_Case
                                    e.KeyChar = e.KeyChar.ToString.ToUpper
                                Case TxtCase.Sentance_Case
                                    If Me.Text.ToString.Length = 0 Then
                                        e.KeyChar = e.KeyChar.ToString.ToUpper
                                    End If
                            End Select
                    End Select
                End With
            End If
        End If

        If TypeOf Me.CurrentCell.OwningColumn Is AgTextColumn Then
            If CType(Me.CurrentCell.OwningColumn, AgTextColumn).AgHelpDataSet IsNot Nothing Then
                If e.KeyChar <> Chr(Keys.Enter) Then If Dg.Visible = False Then Dg.Visible = True

                mAgLib.RowsFilter(sender, Dg, CType(Me.CurrentCell.OwningColumn, AgTextColumn).AgRowFilter, e, CType(Me.CurrentCell.OwningColumn, AgTextColumn).AgMasterHelp, Me.AgSearchMethod, mHelpColumnIndex)


            End If
        End If

        RaiseEvent EditingControl_KeyPress(sender, e)
    End Sub

    Private Sub DgValidating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        If Not TypeOf sender Is ComboBox Then
            If Me.CurrentCell Is Nothing Then Exit Sub
            If TypeOf Me.CurrentCell.OwningColumn Is AgTextColumn Then
                With CType(Me.CurrentCell.OwningColumn, AgTextColumn)
                    Select Case .AgValueType
                        Case TxtValueType.Number_Value
                            Me.Item(Me.CurrentCell.ColumnIndex, Me.CurrentCell.RowIndex).Value = Format(Val(sender.Text), "0.".PadRight(.AgNumberRightPlaces + 2, "0"))
                        Case TxtValueType.Date_Value
                            Me.Item(Me.CurrentCell.ColumnIndex, Me.CurrentCell.RowIndex).Value = RetDate(sender.Text)
                        Case TxtValueType.Text_Value
                            Select Case .AgTxtCase
                                Case TxtCase.Lower_Case
                                    Me.Item(Me.CurrentCell.ColumnIndex, Me.CurrentCell.RowIndex).Value = sender.Text.ToString.ToLower
                                Case TxtCase.Upper_Case
                                    Me.Item(Me.CurrentCell.ColumnIndex, Me.CurrentCell.RowIndex).Value = sender.Text.ToString.ToUpper
                                Case TxtCase.Sentance_Case
                                    If sender.Text.Trim.Length > 0 Then
                                        Me.Item(Me.CurrentCell.ColumnIndex, Me.CurrentCell.RowIndex).Value = sender.Text.Substring(0, 1).ToUpper + sender.Text.Substring(1, sender.Text.ToString.Length - 1)
                                    End If
                                Case TxtCase.Proper_Case
                                    Me.Item(Me.CurrentCell.ColumnIndex, Me.CurrentCell.RowIndex).Value = StrConv(sender.Text.ToString, VbStrConv.ProperCase)

                            End Select
                    End Select
                End With
            End If
        End If

        RaiseEvent EditingControl_Validating(sender, e)
    End Sub

    Private Sub DgKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Escape Then
            If Dg IsNot Nothing Then
                If Dg.Visible = True Then Dg.Visible = False
            End If
        End If
        If TypeOf sender Is ComboBox Then
            Select Case e.KeyCode
                Case Keys.Enter, Keys.Tab, Keys.Return, 3, Keys.Escape
                    Me.Focus()
            End Select
        End If

        'If TypeOf Me.CurrentCell.OwningColumn Is AgTextColumn Then
        '    If CType(Me.CurrentCell.OwningColumn, AgTextColumn).AgHelpDataSet IsNot Nothing Then

        '        If Me.AgSearchMethod = AgLib.TxtSearchMethod.Comprehensive Then
        '            If Not (e.KeyCode = Keys.Up Or e.KeyCode = Keys.Down Or (e.KeyCode = Keys.Left And e.Control) Or (e.KeyCode = Keys.Right And e.Control)) Then
        '                Me.AgHelpDataSet(Me.CurrentCell.ColumnIndex).Tables(0).DefaultView.RowFilter = Nothing
        '                CType(Me.CurrentCell.OwningColumn, AgTextColumn).AgHelpDataSet.Tables(0).DefaultView.RowFilter = CType(Me.CurrentCell.OwningColumn, AgTextColumn).AgRowFilter
        '                If sender.text.ToString.Trim <> "" Then
        '                    'Me.AgHelpDataSet(Me.CurrentCell.ColumnIndex).Tables(0).DefaultView.RowFilter = IIf(CType(Me.CurrentCell.OwningColumn, AgTextColumn).AgRowFilter <> "", CType(Me.CurrentCell.OwningColumn, AgTextColumn).AgRowFilter & " And ", "") & "[" + Me.AgHelpDataSet(Me.CurrentCell.ColumnIndex).Tables(0).Columns(1).ColumnName + "] Like '%" + AgLib.GetFindStr(sender.text) + "%'"
        '                    Me.AgHelpDataSet(Me.CurrentCell.ColumnIndex).Tables(0).DefaultView.RowFilter = IIf(CType(Me.CurrentCell.OwningColumn, AgTextColumn).AgRowFilter <> "", CType(Me.CurrentCell.OwningColumn, AgTextColumn).AgRowFilter & " And ", "") & "[" + Me.AgHelpDataSet(Me.CurrentCell.ColumnIndex).Tables(0).Columns(mHelpColumnIndex).ColumnName + "] Like '%" + AgLib.GetFindStr(sender.text) + "%'"
        '                End If
        '            End If
        '        End If
        '    End If
        'End If


        RaiseEvent EditingControl_KeyDown(sender, e)
    End Sub

    Private Sub DgKeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Escape Then If Dg.Visible = True Then Dg.Visible = False
        If TypeOf sender Is ComboBox Then
            Select Case e.KeyCode
                Case Keys.Enter, Keys.Tab, Keys.Return, 3, Keys.Escape
                    Me.Focus()
            End Select
        End If

        'If TypeOf Me.CurrentCell.OwningColumn Is AgTextColumn Then
        '    If CType(Me.CurrentCell.OwningColumn, AgTextColumn).AgHelpDataSet IsNot Nothing Then
        '        If Dg IsNot Nothing Then
        '            If Dg.Visible = True Then
        '                Dg.Columns(mHelpColumnIndex).DisplayIndex = 1
        '                Call ProcSetHelpDgWidth()

        '                If Me.AgSearchMethod = AgLib.TxtSearchMethod.Comprehensive Then
        '                    If Not (e.KeyCode = Keys.Up Or e.KeyCode = Keys.Down Or (e.KeyCode = Keys.Left And e.Control) Or (e.KeyCode = Keys.Right And e.Control)) Then
        '                        Me.AgHelpDataSet(Me.CurrentCell.ColumnIndex).Tables(0).DefaultView.RowFilter = Nothing
        '                        CType(Me.CurrentCell.OwningColumn, AgTextColumn).AgHelpDataSet.Tables(0).DefaultView.RowFilter = CType(Me.CurrentCell.OwningColumn, AgTextColumn).AgRowFilter
        '                        If sender.text.ToString.Trim <> "" Then
        '                            'Me.AgHelpDataSet(Me.CurrentCell.ColumnIndex).Tables(0).DefaultView.RowFilter = IIf(CType(Me.CurrentCell.OwningColumn, AgTextColumn).AgRowFilter <> "", CType(Me.CurrentCell.OwningColumn, AgTextColumn).AgRowFilter & " And ", "") & "[" + Me.AgHelpDataSet(Me.CurrentCell.ColumnIndex).Tables(0).Columns(1).ColumnName + "] Like '%" + AgLib.GetFindStr(sender.text) + "%'"
        '                            Me.AgHelpDataSet(Me.CurrentCell.ColumnIndex).Tables(0).DefaultView.RowFilter = IIf(CType(Me.CurrentCell.OwningColumn, AgTextColumn).AgRowFilter <> "", CType(Me.CurrentCell.OwningColumn, AgTextColumn).AgRowFilter & " And ", "") & "[" + Me.AgHelpDataSet(Me.CurrentCell.ColumnIndex).Tables(0).Columns(mHelpColumnIndex).ColumnName + "] Like '%" + AgLib.GetFindStr(sender.text) + "%'"
        '                        End If
        '                    End If
        '                End If
        '            End If

        '            If Dg.CurrentCell IsNot Nothing Then
        '                If mHelpColumnIndex <> Dg.CurrentCell.ColumnIndex Then
        '                    Dg.CurrentCell = Dg(mHelpColumnIndex, Dg.CurrentCell.RowIndex)
        '                End If
        '            End If

        '        End If

        '    End If
        'End If

        RaiseEvent EditingControl_KeyUp(sender, e)
    End Sub

    Private Sub DgLostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        If Me.CurrentCell Is Nothing Then Exit Sub
        If TypeOf sender Is ComboBox Then
            CType(Me.EditingControl, ComboBox).SelectedIndex = CType(Me.EditingControl, ComboBox).FindString(CType(Me.EditingControl, ComboBox).Text)

        End If


        If sender.Text.IndexOf("=") = 0 Then
            sender.Text = ComputeNum(sender.Text)
        End If


        mAgDataRow = Nothing
        If TypeOf Me.CurrentCell.OwningColumn Is AgTextColumn And Dg IsNot Nothing Then
            If CType(Me.CurrentCell.OwningColumn, AgTextColumn).AgHelpDataSet IsNot Nothing Then
                With CType(sender, TextBox)
                    If Dg.Visible = True And .Text <> "" Then
                        If Not Dg.Focused Then Dg.Visible = False
                        If Not CType(Me.CurrentCell.OwningColumn, AgTextColumn).AgMasterHelp Then
                            'If Me.CurrentCell.RowIndex >= Me.RowCount - 2 Then
                            '    Me.Rows.Add()
                            'End If
                            If Dg.CurrentCell IsNot Nothing Then
                                .Text = mAgLib.XNull(Dg.Item(1, Dg.CurrentCell.RowIndex).Value)
                                .Tag = mAgLib.XNull(Dg.Item(0, Dg.CurrentCell.RowIndex).Value)
                                Me.AgSelectedValue(Me.CurrentCell.ColumnIndex, Me.CurrentCell.RowIndex) = mAgLib.XNull(Dg.Item(0, Dg.CurrentCell.RowIndex).Value)
                                mAgDataRow = Dg.CurrentRow
                            Else
                                .Text = ""
                                .Tag = ""
                                Me.AgSelectedValue(Me.CurrentCell.ColumnIndex, Me.CurrentCell.RowIndex) = ""
                            End If
                        End If
                    ElseIf Dg.Visible = True And .Text = "" Then
                        If Not Dg.Focused Then Dg.Visible = False
                        .Text = ""
                        .Tag = ""
                        Me.AgSelectedValue(Me.CurrentCell.ColumnIndex, Me.CurrentCell.RowIndex) = ""
                    ElseIf .Text = "" Then
                        .Text = ""
                        .Tag = ""
                        Me.AgSelectedValue(Me.CurrentCell.ColumnIndex, Me.CurrentCell.RowIndex) = ""
                    End If

                    'Akash
                    'If Me.DataSource IsNot Nothing Then Me.DataSource.RowFilter = ""
                End With




                mHelpColumnIndex = 1

            End If
        End If
        RaiseEvent EditingControl_LostFocus(sender, e)
    End Sub

    Private Sub Dg_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Dg.Click
        'Try
        '    If TypeOf Me.CurrentCell.OwningColumn Is AgTextColumn Then
        '        If CType(Me.CurrentCell.OwningColumn, AgTextColumn).AgHelpDataSet IsNot Nothing Then
        '            mHelpColumnIndex = Dg.CurrentCell.ColumnIndex
        '            Dg.Columns(mHelpColumnIndex).DisplayIndex = 1
        '        End If
        '    End If
        'Catch ex As Exception

        'End Try
        Me.Focus()
    End Sub

    Private Sub Dg_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Dg.DoubleClick
        Me.Focus()
    End Sub

    Private Sub Dg_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Dg.LostFocus
        If TypeOf Me.CurrentCell.OwningColumn Is AgTextColumn Then
            If CType(Me.CurrentCell.OwningColumn, AgTextColumn).AgHelpDataSet IsNot Nothing Then
                Me.AgSelectedValue(Me.CurrentCell.ColumnIndex, Me.CurrentCell.RowIndex) = mAgLib.XNull(Dg.Item(0, Dg.CurrentCell.RowIndex).Value)
            End If
        End If
        sender.visible = False
    End Sub

    Private Sub AgDataGrid_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Enter
        Me.FindForm.Controls.Add(TxtFind)
        TxtFind.Visible = False
        'TxtFind.BorderStyle = Windows.Forms.BorderStyle.None
        TxtFind.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FindForm.Controls.Add(PBox)
        PBox.Visible = False
        PBox.Image = My.Resources.SearchIcon
        PBox.SizeMode = PictureBoxSizeMode.AutoSize
        PBox.BorderStyle = Windows.Forms.BorderStyle.FixedSingle
    End Sub

    Private Sub AgDataGrid_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        If Me.CurrentCell IsNot Nothing Then
            If Me.Columns(Me.CurrentCell.ColumnIndex).ReadOnly = True Then
                If My.Computer.Keyboard.CtrlKeyDown Then Exit Sub
                If My.Computer.Keyboard.AltKeyDown Then Exit Sub
                If Me.CurrentCell.ColumnIndex < 0 Then Exit Sub
                If Asc(e.KeyChar) = Keys.Enter Or Asc(e.KeyChar) = Keys.Tab Then TxtFind.Text = "" : TxtFind.Visible = False : PBox.Visible = False : Exit Sub
                If Asc(e.KeyChar) = Keys.Back Then
                    If TxtFind.Text <> "" Then TxtFind.Text = Microsoft.VisualBasic.Left(TxtFind.Text, Len(TxtFind.Text) - 1)
                End If

                Select Case Asc(e.KeyChar)
                    Case Keys.Enter, Keys.Escape
                    Case Else
                        TextBox1_KeyPress(TxtFind, e)
                End Select

            End If
        End If
    End Sub

    'Private Sub AgDataGrid_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseClick
    '    Dim FileName As String
    '    Try
    '        If e.Button = Windows.Forms.MouseButtons.Right Then
    '            If MsgBox("Want to Export Grid Data", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Export Grid?...") = vbNo Then Exit Sub
    '            FileName = GetFileName(My.Computer.FileSystem.SpecialDirectories.Desktop)
    '            If FileName.Trim <> "" Then
    '                Call Export.exportExcel(sender, FileName, Me.Handle)
    '            End If
    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub

    Private Sub ProcSetHelpDgWidth()
        Dim x As New AgLib
        Dim I As Integer

        x.AgSetDataGridAutoWidths(Dg, 100, 100)
        Dg.Columns(0).Visible = False
        Dg.Columns(mHelpColumnIndex).Width = If(Me.Columns(Me.CurrentCell.ColumnIndex).Width > 400, 400, Me.Columns(Me.CurrentCell.ColumnIndex).Width)
        Dg.Width = 0
        For I = 1 To Dg.Columns.Count - 1
            If I > (Dg.ColumnCount - 1 - CType(Me.CurrentCell.OwningColumn, AgTextColumn).AgLastHiddenColumns) Then
                Dg.Columns(I).Visible = False
            Else
                If Dg.Columns(I).Width < 150 Then Dg.Columns(I).Width = 150
                Dg.Width = Dg.Width + 4 + Dg.Columns(I).Width
                Dg.Columns(I).ToolTipText = "Click on respective column for searching!..."
            End If
        Next
        If Dg.Width > (Dg.FindForm.Width + 50) Then Dg.Width = Dg.FindForm.Width - 100

        Dg.Width = Dg.Width - IIf(Dg.RowHeadersVisible, Dg.RowHeadersWidth, 0) + 25
    End Sub



    Private Function RowFilter(ByVal GrdFind As AgControls.AgDataGrid, ByVal FldName As String, ByVal FindVal As TextBox, ByVal e As System.Windows.Forms.KeyPressEventArgs) As Integer
        Dim StrFind$ = ""
        Dim mIsKeyFound As Boolean = False
        Dim mRIndex As Integer = -1

        Dim x As Integer
        Dim i As Integer
        If FindVal.Text = "" Then
            If Asc(e.KeyChar) = Keys.Back Or Asc(e.KeyChar) = 4 Or Asc(e.KeyChar) = 19 Or Asc(e.KeyChar) = 13 Or Asc(e.KeyChar) = 10 Then
            Else
                StrFind = e.KeyChar
            End If

            FindVal.Text = IIf(Asc(e.KeyChar) = Keys.Back Or Asc(e.KeyChar) = 4 Or Asc(e.KeyChar) = 19 Or Asc(e.KeyChar) = 13 Or Asc(e.KeyChar) = 10, TxtFind.Text, TxtFind.Text + e.KeyChar)
        Else
            StrFind = IIf(Asc(e.KeyChar) = Keys.Back Or Asc(e.KeyChar) = 4 Or Asc(e.KeyChar) = 19, TxtFind.Text, TxtFind.Text + e.KeyChar)
            FindVal.Text = StrFind
        End If


        mRIndex = GrdFind.CurrentCell.RowIndex
        x = GrdFind.CurrentCell.ColumnIndex
        For i = 0 To GrdFind.RowCount - 1
            If GrdFind.Item(x, i).Value Is Nothing Then GrdFind.Item(x, i).Value = ""
            If GrdFind.Item(x, i).Value.ToString.Length >= StrFind.Length Then
                If mGridSearchMethod = AgLib.TxtSearchMethod.Comprehensive Then
                    If UCase(GrdFind.Item(x, i).Value).ToString.Contains(UCase(StrFind)) Then
                        mRIndex = i
                        mIsKeyFound = True
                        Exit For
                    End If
                Else
                    If UCase(GrdFind.Item(x, i).Value).ToString.Substring(0, StrFind.Length) = UCase(StrFind) Then
                        mRIndex = i
                        mIsKeyFound = True
                        Exit For
                    End If
                End If
            End If
        Next

        GrdFind.CurrentCell = GrdFind(x, mRIndex)
        If FindVal.Text = "" Or Not mIsKeyFound Then
            TxtFind.Text = ""
            TxtFind.Visible = False
            PBox.Visible = False
        Else
            TxtFind.Visible = True
            TxtFind.BringToFront()
            PBox.Visible = True
            PBox.BringToFront()
        End If

        TxtFind.Width = GrdFind.Columns(GrdFind.CurrentCell.ColumnIndex).Width
        TxtFind.Top = GrdFind.Top - TxtFind.Height
        'TxtFind.Top = AgLib.FActualTop(GrdFind) - TxtFind.Height
        'TxtFind.Left = AgLib.FActualLeft(GrdFind) + GrdFind.Width - TxtFind.Width
        TxtFind.Left = GrdFind.Left + GrdFind.Width - TxtFind.Width
        PBox.Top = TxtFind.Top + TxtFind.Height - PBox.Height
        PBox.Left = TxtFind.Left - PBox.Width
    End Function

    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtFind.KeyPress
        Try
            If mAgAllowFind Then
                RowFilter(Me, Me.Columns(Me.CurrentCell.ColumnIndex).HeaderText, TxtFind, e)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Dg_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Dg.Validating
        Me.OnKeyDown(New System.Windows.Forms.KeyEventArgs(Keys.F2))

        If Me.EditingControl IsNot Nothing Then
            DgValidating(Me.EditingControl, New System.ComponentModel.CancelEventArgs())
        End If
    End Sub

    Private Sub AgDataGrid_EditingControl_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.EditingControl_KeyDown
        If e.Control = True Then
            IsControlPressed = True
            ControlPressedData = sender.Text
        End If
    End Sub

    Private Sub AgDataGrid_EditingControl_LostFocus(sender As Object, e As EventArgs) Handles Me.EditingControl_LostFocus

    End Sub
End Class

