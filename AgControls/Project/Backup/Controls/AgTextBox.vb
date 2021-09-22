Imports System.Windows.Forms

Public Class AgTextBox
    Inherits TextBox
    Dim WithEvents Dg As AgDataGrid
    Dim mAgLib As New AgLib

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


    Dim mLastHiddenColumns As Integer
    Dim mSelectedValue As String = ""
    Dim mHelpDataSet As DataSet = Nothing    
    Dim mMandatory As Boolean = False
    Dim mNumLeft As Integer = 0
    Dim mNumRight As Integer = 0
    Dim mNegetiveAllow As Boolean = False
    Dim mTitleCase As Boolean = False
    Dim mValueType As TxtValueType = TxtValueType.Text_Value
    Dim mTxtCase As TxtCase = TxtCase.None
    Dim mTopofContainer As Integer
    Dim mLeftOfContainer As Integer
    Dim mHeightHelpGrid As Integer
    Dim mMasterHelp As Boolean = False
    Dim mAllowUserToEnableMasterHelp As Boolean = False
    Dim mLastValueTag As String
    Dim mLastValueText As String
    Dim mPickFromLastValue As Boolean = False
    Dim mSearchMethod As AgLib.TxtSearchMethod = AgLib.TxtSearchMethod.Simple
    Dim mAgRowFilter As String = ""
    Dim mHelpColumnIndex As Integer = 1
    Dim mAgDataRow As DataGridViewRow = Nothing

    Public Property AgRowFilter() As String
        Get
            AgRowFilter = mAgRowFilter
        End Get
        Set(ByVal value As String)
            mAgRowFilter = value
        End Set
    End Property

    Public ReadOnly Property AgDataRow() As DataGridViewRow
        Get
            AgDataRow = mAgDataRow
        End Get
    End Property

    Public Property AgSelectedValue() As String
        Get
            Return Me.Tag
        End Get
        Set(ByVal value As String)
            Me.Tag = value

            'Dim I As Long, mFound As Boolean = False
            Dim DrTemp As DataRow() = Nothing
            If Me.AgHelpDataSet IsNot Nothing And value <> "" Then
                DrTemp = Me.AgHelpDataSet.Tables(0).Select("" & Me.AgHelpDataSet.Tables(0).Columns(0).ColumnName & " = '" & value & "'")
                If DrTemp.Length > 0 Then
                    Me.Text = mAgLib.XNull(DrTemp(0)(1))
                Else
                    MsgBox("Corresponding Data not found!", MsgBoxStyle.OkOnly, Me.Name)
                End If
                'For I = 0 To Me.AgHelpDataSet.Tables(0).Rows.Count - 1
                '    If UCase(Me.AgHelpDataSet.Tables(0).Rows(I)(0)) = UCase(value) Then
                '        Me.Text = mAgLib.XNull(Me.AgHelpDataSet.Tables(0).Rows(I)(1))
                '        mFound = True : Exit For
                '    End If
                'Next
                'If Not mFound Then
                '    MsgBox("Corresponding Data not found!")
                'End If
            Else
                Me.Text = ""
            End If

        End Set
    End Property

    Public ReadOnly Property AgDataSetAbsolutePosition() As Long
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

    Public Property AgLastValueTag() As String
        Get
            AgLastValueTag = mLastValueTag
        End Get
        Set(ByVal value As String)
            mLastValueTag = value
        End Set
    End Property

    Public Property AgLastValueText() As String
        Get
            AgLastValueText = mLastValueText
        End Get
        Set(ByVal value As String)
            mLastValueText = value
        End Set

    End Property

    Public Property AgPickFromLastValue() As Boolean
        Get
            AgPickFromLastValue = mPickFromLastValue
        End Get

        Set(ByVal value As Boolean)
            mPickFromLastValue = value
        End Set
    End Property

    Public Property AgMasterHelp() As Boolean
        Get
            AgMasterHelp = mMasterHelp
        End Get
        Set(ByVal value As Boolean)
            mMasterHelp = value
        End Set
    End Property

    Public Property AgAllowUserToEnableMasterHelp() As Boolean
        Get
            AgMasterHelp = mMasterHelp
        End Get
        Set(ByVal value As Boolean)
            mMasterHelp = value
        End Set
    End Property


    Public Property AgHelpDataSet(Optional ByVal LastHiddenColumns As Integer = 0, Optional ByVal TopOfContainer As Integer = 0, Optional ByVal LeftOfContainer As Integer = 0, Optional ByVal Height As Integer = 0) As DataSet
        Get
            AgHelpDataSet = mHelpDataSet
        End Get
        Set(ByVal value As DataSet)
            mHelpDataSet = value
            mTopofContainer = TopOfContainer
            mLeftOfContainer = LeftOfContainer
            mHeightHelpGrid = Height
            mLastHiddenColumns = LastHiddenColumns

            If mHelpDataSet IsNot Nothing Then
                Me.AgHelpDataSet.Tables(0).DefaultView.Sort = Me.AgHelpDataSet.Tables(0).Columns(1).ColumnName
                AgCreateHelpGrid(Me)
            End If
        End Set
    End Property


    Public Property AgMandatory() As Boolean
        Get
            AgMandatory = mMandatory
        End Get
        Set(ByVal value As Boolean)
            mMandatory = value
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


    Public Property AgNumberLeftPlaces() As Integer
        Get
            AgNumberLeftPlaces = mNumLeft
        End Get
        Set(ByVal value As Integer)
            mNumLeft = value
        End Set
    End Property


    Public Property AgNumberRightPlaces() As Integer
        Get
            AgNumberRightPlaces = mNumRight
        End Get
        Set(ByVal value As Integer)
            mNumRight = value
        End Set
    End Property

    Public Property AgNumberNegetiveAllow() As Boolean
        Get
            AgNumberNegetiveAllow = mNegetiveAllow
        End Get
        Set(ByVal value As Boolean)
            mNegetiveAllow = value
        End Set
    End Property

    Public Property AgValueType() As TxtValueType
        Get
            AgValueType = mValueType
        End Get
        Set(ByVal value As TxtValueType)
            mValueType = value
        End Set
    End Property

    Public Property AgTxtCase() As TxtCase
        Get
            AgTxtCase = mTxtCase
        End Get
        Set(ByVal value As TxtCase)
            mTxtCase = value
        End Set
    End Property

    Private Sub AgTextBox_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Enter
        'If mPickFromLastValue Then
        mLastValueTag = Me.Tag
        mLastValueText = Me.Text
        'End If
    End Sub

    Public Sub AgInitialChar(ByVal mChr As Char)
        Me.OnKeyPress(New System.Windows.Forms.KeyPressEventArgs(mChr))
        If Dg IsNot Nothing Then Dg.Visible = True
    End Sub

    Public Sub AgCreateHelpGrid(ByVal sender As Object)
        If CType(sender, AgTextBox).AgHelpDataSet IsNot Nothing Then
            If CType(sender, AgTextBox).FindForm IsNot Nothing Then
                If CType(sender, AgTextBox).FindForm.Controls("HelpDg") IsNot Nothing Then
                    CType(sender, AgTextBox).FindForm.Controls("HelpDg").Dispose()
                End If
                Dg = New AgDataGrid
                CType(sender, AgTextBox).FindForm.Controls.Add(Dg)
                Dg.Name = "HelpDg"
                Dg.Visible = False
                Dg.Height = IIf(mHeightHelpGrid > 0, mHeightHelpGrid, 100)
                Dg.Top = Me.Top + mTopofContainer + Me.Height + 2
                Dg.Left = Me.Left + mLeftOfContainer
                'Dg.AllowUserToAddRows = False

                If mAgRowFilter <> "" Then
                    Me.AgHelpDataSet.Tables(0).DefaultView.RowFilter = Nothing
                    Me.AgHelpDataSet.Tables(0).DefaultView.RowFilter = mAgRowFilter
                End If
                Dg.DataSource = Me.AgHelpDataSet.Tables(0).DefaultView
                If Dg.Columns.Count <= 2 Then
                    Dg.ColumnHeadersVisible = False
                End If
                Dg.RowHeadersVisible = False
                Dg.BringToFront()

                'x.AgSetDataGridAutoWidths(Dg, 100, 100)
                'Dg.Columns(0).Visible = False
                'Dg.Columns(1).Width = sender.width
                'Dg.Width = 0
                'For I = 1 To Dg.Columns.Count - 1
                '    If I > (Dg.ColumnCount - 1 - mLastHiddenColumns) Then
                '        Dg.Columns(I).Visible = False
                '    Else
                '        Dg.Width = Dg.Width + Dg.Columns(I).Width
                '        Dg.Columns(I).ToolTipText = "Click on respective column for searching!..."
                '    End If
                'Next
                'Dg.Width = Dg.Width - IIf(Dg.RowHeadersVisible, Dg.RowHeadersWidth, 0) + 25

                Call ProcSetHelpDgWidth(sender)

                If mLeftOfContainer > 0 Then
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

    Public Sub AgTextBox_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.GotFocus
        AgCreateHelpGrid(sender)

    End Sub

    Public Sub AgTextBox_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Return Or e.KeyCode = Keys.Tab Then
            If mValueType = TxtValueType.Number_Value Then
                If Me.Text.IndexOf("=") = 0 Then
                    Me.Text = ComputeNum(Me.Text)
                End If
            End If
            If mMandatory = True Then
                If Me.Text.Trim = "" And mValueType <> TxtValueType.Number_Value Then
                    MsgBox("Required Field" & vbCrLf & "Can't Be Blank!")
                    e.Handled = True
                ElseIf Val(Me.Text) = 0 And mValueType = TxtValueType.Number_Value Then
                    MsgBox("Required Field" & vbCrLf & "Can't Be Blank/Zero!")
                    e.Handled = True
                End If
            End If
        End If



        If CType(sender, AgTextBox).AgHelpDataSet IsNot Nothing And Dg IsNot Nothing Then
            If Dg.Visible = True Then
                Select Case e.KeyCode
                    Case Keys.Up
                        If Dg.CurrentCell IsNot Nothing Then
                            If Dg.CurrentCell.RowIndex >= 1 Then
                                Dg.CurrentCell = Dg(Dg.CurrentCell.ColumnIndex, Dg.CurrentCell.RowIndex - 1)
                                Me.AgSelectedValue = mAgLib.XNull(Dg.Item(0, Dg.CurrentCell.RowIndex).Value)
                            End If
                        Else
                            Dg.CurrentCell = Dg(1, 1)
                            Me.AgSelectedValue = mAgLib.XNull(Dg.Item(0, Dg.CurrentCell.RowIndex).Value)
                        End If
                        e.Handled = True
                    Case Keys.Down
                        If Dg.CurrentCell IsNot Nothing Then
                            If Dg.CurrentCell.RowIndex <= Dg.Rows.Count - 2 Then
                                Dg.CurrentCell = Dg(Dg.CurrentCell.ColumnIndex, Dg.CurrentCell.RowIndex + 1)
                                Me.AgSelectedValue = mAgLib.XNull(Dg.Item(0, Dg.CurrentCell.RowIndex).Value)
                            End If
                        Else
                            Dg.CurrentCell = Dg(1, Dg.Rows.Count - 1)
                            Me.AgSelectedValue = mAgLib.XNull(Dg.Item(0, Dg.CurrentCell.RowIndex).Value)
                        End If
                        e.Handled = True

                    Case Keys.Right
                        If e.Control Then
                            If Dg.CurrentCell IsNot Nothing Then
                                If mHelpColumnIndex < Dg.Columns.Count - mLastHiddenColumns - 1 Then
                                    mHelpColumnIndex += 1
                                Else
                                    mHelpColumnIndex = 1
                                End If

                                Dg.Columns(mHelpColumnIndex).DisplayIndex = 1
                                Dg.CurrentCell = Dg(mHelpColumnIndex, Dg.CurrentCell.RowIndex)
                                Me.AgSelectedValue = mAgLib.XNull(Dg.Item(0, Dg.CurrentCell.RowIndex).Value)
                                Me.Text = mAgLib.XNull(Dg.Item(mHelpColumnIndex, Dg.CurrentCell.RowIndex).Value)
                            Else
                                Dg.CurrentCell = Dg(1, Dg.Rows.Count - 1)
                                Me.AgSelectedValue = mAgLib.XNull(Dg.Item(0, Dg.CurrentCell.RowIndex).Value)
                            End If
                            e.Handled = True
                        End If

                    Case Keys.Left
                        If e.Control Then
                            If Dg.CurrentCell IsNot Nothing Then
                                If mHelpColumnIndex > 1 Then
                                    mHelpColumnIndex -= 1
                                Else
                                    mHelpColumnIndex = Dg.Columns.Count - mLastHiddenColumns - 1
                                End If

                                Dg.Columns(mHelpColumnIndex).DisplayIndex = 1
                                Dg.CurrentCell = Dg(mHelpColumnIndex, Dg.CurrentCell.RowIndex)
                                Me.AgSelectedValue = mAgLib.XNull(Dg.Item(0, Dg.CurrentCell.RowIndex).Value)
                                Me.Text = mAgLib.XNull(Dg.Item(mHelpColumnIndex, Dg.CurrentCell.RowIndex).Value)
                            Else
                                Dg.CurrentCell = Dg(1, Dg.Rows.Count - 1)
                                Me.AgSelectedValue = mAgLib.XNull(Dg.Item(0, Dg.CurrentCell.RowIndex).Value)
                            End If
                            e.Handled = True
                        End If

                    Case Keys.Escape
                        If Dg.Visible = True Then Dg.Visible = False
                        e.Handled = True
                End Select

                'If Me.AgSearchMethod = AgLib.TxtSearchMethod.Comprehensive Then
                '    If Not (e.KeyCode = Keys.Up Or e.KeyCode = Keys.Down Or (e.KeyCode = Keys.Left And e.Control) Or (e.KeyCode = Keys.Right And e.Control)) Then
                '        Me.AgHelpDataSet.Tables(0).DefaultView.RowFilter = Nothing
                '        Me.AgHelpDataSet.Tables(0).DefaultView.RowFilter = IIf(mAgRowFilter <> "", mAgRowFilter, "")

                '        If sender.text.ToString.Trim <> "" Then
                '            'Me.AgHelpDataSet.Tables(0).DefaultView.RowFilter = IIf(mAgRowFilter <> "", mAgRowFilter & " And ", "") & "[" + Me.AgHelpDataSet.Tables(0).Columns(1).ColumnName + "] Like '%" + AgLib.GetFindStr(sender.text) + "%'"
                '            Me.AgHelpDataSet.Tables(0).DefaultView.RowFilter = IIf(mAgRowFilter <> "", mAgRowFilter & " And ", "") & "[" + Me.AgHelpDataSet.Tables(0).Columns(mHelpColumnIndex).ColumnName + "] Like '%" + AgLib.GetFindStr(sender.text) + "%'"
                '        End If
                '    End If
                'End If

            End If
        End If

    End Sub


    Public Sub AgTxt_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        Dim mAgLib As New AgLib

        Select Case mValueType
            Case TxtValueType.Number_Value
                If mNumLeft > 0 Or mNumRight > 0 Then
                    NumPress(sender, e, mNumLeft, mNumRight, mNegetiveAllow)
                End If
            Case TxtValueType.YesNo_Value
                If e.KeyChar.ToString.ToUpper = "Y" Then
                    Me.Text = "Yes"
                ElseIf e.KeyChar.ToString.ToUpper = "N" Then
                    Me.Text = "No"
                End If
                e.KeyChar = ""
            Case TxtValueType.Text_Value
                Select Case mTxtCase
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

        If Me.AgAllowUserToEnableMasterHelp Then
            If Asc(e.KeyChar) = Keys.Insert Then
                Me.AgMasterHelp = True
            End If
        End If


        If CType(sender, AgTextBox).AgHelpDataSet IsNot Nothing And Dg IsNot Nothing Then
            If e.KeyChar <> Chr(Keys.Enter) Then If Dg.Visible = False Then Dg.Visible = True : Dg.BringToFront()

            'If Me.AgSearchMethod = AgLib.TxtSearchMethod.Comprehensive Then Me.AgHelpDataSet.Tables(0).DefaultView.RowFilter = Nothing
            'Me.AgHelpDataSet.Tables(0).DefaultView.RowFilter = IIf(mAgRowFilter <> "", mAgRowFilter, "")

            'If sender.text.ToString.Trim <> "" Then
            '    'If Me.AgSearchMethod = AgLib.TxtSearchMethod.Comprehensive Then Me.AgHelpDataSet.Tables(0).DefaultView.RowFilter = IIf(mAgRowFilter <> "", mAgRowFilter & " And ", "") & "[" + Me.AgHelpDataSet.Tables(0).Columns(1).ColumnName + "] Like '%" + AgLib.GetFindStr(sender.text) + "%'"
            '    If Me.AgSearchMethod = AgLib.TxtSearchMethod.Comprehensive Then Me.AgHelpDataSet.Tables(0).DefaultView.RowFilter = IIf(mAgRowFilter <> "", mAgRowFilter & " And ", "") & "[" + Me.AgHelpDataSet.Tables(0).Columns(mHelpColumnIndex).ColumnName + "] Like '%" + AgLib.GetFindStr(sender.text) + "%'"
            'End If
            If e.KeyChar <> Chr(Keys.Enter) Then
                mAgLib.RowsFilter(sender, Dg, mAgRowFilter, e, mMasterHelp, Me.AgSearchMethod, mHelpColumnIndex)
            Else
                mAgLib.RowsFilter(sender, Dg, mAgRowFilter, e, mMasterHelp, Me.AgSearchMethod, mHelpColumnIndex)
            End If
        End If

    End Sub

    Public Sub AgTextBox_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        If CType(sender, AgTextBox).AgHelpDataSet IsNot Nothing And Dg IsNot Nothing Then

            Dim bColumnIndex As Integer = 0

            If Dg.Visible = True Then
                If Dg.ColumnCount > 2 Then
                    Dg.Columns(mHelpColumnIndex).DisplayIndex = 1
                End If
                'Call ProcSetHelpDgWidth(sender)

                'If Me.AgSearchMethod = AgLib.TxtSearchMethod.Comprehensive Then
                '    If Not (e.KeyCode = Keys.Up Or e.KeyCode = Keys.Down Or (e.KeyCode = Keys.Left And e.Control) Or (e.KeyCode = Keys.Right And e.Control)) Then
                '        Me.AgHelpDataSet.Tables(0).DefaultView.RowFilter = Nothing
                '        Me.AgHelpDataSet.Tables(0).DefaultView.RowFilter = IIf(mAgRowFilter <> "", mAgRowFilter, "")
                '        If sender.text.ToString.Trim <> "" Then
                '            'Me.AgHelpDataSet.Tables(0).DefaultView.RowFilter = IIf(mAgRowFilter <> "", mAgRowFilter & " And ", "") & "[" + Me.AgHelpDataSet.Tables(0).Columns(1).ColumnName + "] Like '%" + AgLib.GetFindStr(sender.text) + "%'"
                '            Me.AgHelpDataSet.Tables(0).DefaultView.RowFilter = IIf(mAgRowFilter <> "", mAgRowFilter & " And ", "") & "[" + Me.AgHelpDataSet.Tables(0).Columns(mHelpColumnIndex).ColumnName + "] Like '%" + AgLib.GetFindStr(sender.text) + "%'"
                '        End If
                '    End If
                'End If

                If Dg.CurrentCell IsNot Nothing Then
                    If mHelpColumnIndex <> Dg.CurrentCell.ColumnIndex Then
                        Dg.CurrentCell = Dg(mHelpColumnIndex, Dg.CurrentCell.RowIndex)
                    End If
                End If

            End If
        End If

    End Sub

    Public Sub AgTextBox_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LostFocus
        mAgDataRow = Nothing
        With CType(sender, AgTextBox)
            If .AgHelpDataSet IsNot Nothing And Dg IsNot Nothing Then
                If Dg.Visible = True And .Text <> "" Then
                    If Not Dg.Focused Then Dg.Visible = False
                    If Not mMasterHelp Then
                        If Dg.CurrentCell IsNot Nothing Then
                            .Text = mAgLib.XNull(Dg.Item(1, Dg.CurrentCell.RowIndex).Value)
                            .Tag = mAgLib.XNull(Dg.Item(0, Dg.CurrentCell.RowIndex).Value)
                            mAgDataRow = Dg.CurrentRow
                        Else
                            .Text = ""
                            .Tag = ""
                        End If
                    End If
                ElseIf Dg.Visible = True And .Text = "" Then
                    If Not Dg.Focused Then Dg.Visible = False
                    .Text = ""
                    .Tag = ""
                ElseIf .Text = "" Then
                    .Text = ""
                    .Tag = ""
                End If

                mHelpColumnIndex = 1
            End If
        End With
    End Sub

    Public Sub AgTxt_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Me.Validating
        Select Case mValueType
            Case TxtValueType.Number_Value
                If Me.Text.IndexOf("=") = 0 Then
                    Me.Text = ComputeNum(Me.Text)
                Else
                    Me.Text = Format(Val(Me.Text), "0.".PadRight(mNumRight + 2, "0"))
                End If
            Case TxtValueType.Date_Value
                Me.Text = RetDate(Me.Text)
            Case TxtValueType.Text_Value
                Select Case mTxtCase
                    Case TxtCase.Lower_Case
                        Me.Text = Me.Text.ToString.ToLower
                    Case TxtCase.Upper_Case
                        Me.Text = Me.Text.ToString.ToUpper
                    Case TxtCase.Sentance_Case
                        If Me.Text.Trim.Length > 0 Then
                            Me.Text = Me.Text.Substring(0, 1).ToUpper + Me.Text.Substring(1, Me.Text.ToString.Length - 1)
                        End If
                    Case TxtCase.Proper_Case
                        Me.Text = StrConv(Me.Text.ToString, VbStrConv.ProperCase)
                End Select

        End Select

        mAgDataRow = Nothing
        With CType(sender, AgTextBox)
            If .AgHelpDataSet IsNot Nothing And Dg IsNot Nothing Then
                If Dg.Visible = True And .Text <> "" Then
                    If Not Dg.Focused Then Dg.Visible = False
                    If Not mMasterHelp Then
                        If Dg.CurrentCell IsNot Nothing Then
                            .Text = mAgLib.XNull(Dg.Item(1, Dg.CurrentCell.RowIndex).Value)
                            .Tag = mAgLib.XNull(Dg.Item(0, Dg.CurrentCell.RowIndex).Value)
                            mAgDataRow = Dg.CurrentRow
                        Else
                            .Text = ""
                            .Tag = ""
                        End If
                    End If
                ElseIf Dg.Visible = True And .Text = "" Then
                    If Not Dg.Focused Then Dg.Visible = False
                    .Text = ""
                    .Tag = ""
                ElseIf .Text = "" Then
                    .Text = ""
                    .Tag = ""
                End If
            End If
        End With


        'If mPickFromLastValue Then
        '    mLastValueTag = Me.Tag
        '    mLastValueText = Me.Text
        'End If

        If Me.AgAllowUserToEnableMasterHelp Then
            Me.AgMasterHelp = False
        End If
    End Sub

    Private Sub Dg_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Dg.Click
        Try
            With CType(Me, AgTextBox)
                If .AgHelpDataSet IsNot Nothing Then
                    mHelpColumnIndex = Dg.CurrentCell.ColumnIndex
                    Dg.Columns(mHelpColumnIndex).DisplayIndex = 1
                End If
            End With

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Dg_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Dg.DoubleClick
        Me.Focus()
    End Sub


    Private Sub Dg_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Dg.LostFocus
        With CType(Me, AgTextBox)
            If .AgHelpDataSet IsNot Nothing Then
                .Text = mAgLib.XNull(Dg.Item(1, Dg.CurrentCell.RowIndex).Value)
                .AgSelectedValue = mAgLib.XNull(Dg.Item(0, Dg.CurrentCell.RowIndex).Value)
                'Application.DoEvents()

            End If
        End With

        sender.visible = False
    End Sub

    Private Sub ProcSetHelpDgWidth(ByVal sender As Object)
        Dim x As New AgLib
        Dim I As Integer

        x.AgSetDataGridAutoWidths(Dg, 100, 100)
        Dg.Columns(0).Visible = False
        Dg.Columns(mHelpColumnIndex).Width = sender.width
        Dg.Width = 0
        For I = 1 To Dg.Columns.Count - 1
            If I > (Dg.ColumnCount - 1 - mLastHiddenColumns) Then
                Dg.Columns(I).Visible = False
            Else
                Dg.Width = Dg.Width + Dg.Columns(I).Width
                Dg.Columns(I).ToolTipText = "Click on respective column for searching!..."
            End If
        Next
        Dg.Width = Dg.Width - IIf(Dg.RowHeadersVisible, Dg.RowHeadersWidth, 0) + 25

    End Sub

    Private Sub Dg_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Dg.Validating
        Me.OnValidating(New System.ComponentModel.CancelEventArgs())
    End Sub
End Class

