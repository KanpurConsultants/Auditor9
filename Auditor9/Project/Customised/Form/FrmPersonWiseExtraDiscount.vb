﻿Imports System.Data.SQLite
Imports AgLibrary.ClsMain.agConstants

Public Class FrmPersonWiseExtraDiscount

    Dim mQry As String = ""
    Public mOkButtonPressed As Boolean

    Public Const ColSNo As String = "S.No."
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Public Const Col1xItemCategory As String = "xItem Category"
    Public Const Col1xItemGroup As String = "xItem Group"
    Public Const Col1ItemCategory As String = "Item Category"
    Public Const Col1ItemGroup As String = "Item Group"
    Public Const Col1ExtraDiscountPattern As String = "Pattern"
    Public Const Col1ExtraDiscountPer As String = "Extra Discount @"

    Dim mSearchCode As String
    Dim mDtSubgroupTypeSettings As DataTable

    Dim mEntryMode$ = ""
    Public Property EntryMode() As String
        Get
            EntryMode = mEntryMode
        End Get
        Set(ByVal value As String)
            mEntryMode = value
        End Set
    End Property


    Public Property DtSubgroupTypeSettings() As DataTable
        Get
            DtSubgroupTypeSettings = mDtSubgroupTypeSettings
        End Get
        Set(ByVal value As DataTable)
            mDtSubgroupTypeSettings = value
        End Set
    End Property

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    'Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
    '    AgL.FPaintForm(Me, e, 0)
    'End Sub

    Public Sub IniGrid(DocID As String)

        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 35, 5, ColSNo, False, True, False)
            .AddAgTextColumn(Dgl1, Col1xItemCategory, 160, 255, Col1xItemCategory, False, False)
            .AddAgTextColumn(Dgl1, Col1xItemGroup, 160, 255, Col1xItemGroup, False, False)
            .AddAgTextColumn(Dgl1, Col1ItemCategory, 160, 255, Col1ItemCategory, CBool(AgL.VNull(DtSubgroupTypeSettings.Rows(0)("PersonCanHaveItemCategoryWiseDiscountYn"))), False)
            .AddAgTextColumn(Dgl1, Col1ItemGroup, 160, 255, Col1ItemGroup, CBool(AgL.VNull(DtSubgroupTypeSettings.Rows(0)("PersonCanHaveItemGroupWiseDiscountYn"))), False)
            .AddAgTextColumn(Dgl1, Col1ExtraDiscountPattern, 100, 255, Col1ExtraDiscountPattern, False, False)
            .AddAgNumberColumn(Dgl1, Col1ExtraDiscountPer, 90, 5, 0, False, Col1ExtraDiscountPer, True, False, True)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 35
        Dgl1.AgSkipReadOnlyColumns = True
        Dgl1.AllowUserToOrderColumns = True

        AgCL.GridSetiingShowXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1, False)

        FMoverec(DocID)
    End Sub
    Public Sub FMoverec(DocID As String)
        Dim DsTemp As DataSet = Nothing
        Dim I As Integer
        mQry = "Select L.ItemGroup, L.ItemCategory, L.ExtraDiscountCalculationPattern, L.ExtraDiscountPer, 
                IG.Description as ItemGroupName, IC.Description as ItemCategoryName
                From PersonExtraDiscount L  With (NoLock)                
                Left Join ItemGroup IG With (NoLock) On L.ItemGroup = IG.Code
                Left Join ItemCategory IC With (NoLock) on L.ItemCategory = IC.Code               
                Where L.Person = '" & DocID & "'  "
        DsTemp = AgL.FillData(mQry, AgL.GCn)
        With DsTemp.Tables(0)
            Dgl1.RowCount = 1
            Dgl1.Rows.Clear()
            If .Rows.Count > 0 Then
                For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                    Dgl1.Rows.Add()
                    Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1
                    'Dgl1.Item(ColSNo, I).Tag = AgL.XNull(.Rows(I)("Sr"))
                    Dgl1.Item(Col1xItemGroup, I).Tag = AgL.XNull(.Rows(I)("ItemGroup"))
                    Dgl1.Item(Col1xItemGroup, I).Value = AgL.XNull(.Rows(I)("ItemGroupName"))
                    Dgl1.Item(Col1ItemGroup, I).Tag = AgL.XNull(.Rows(I)("ItemGroup"))
                    Dgl1.Item(Col1ItemGroup, I).Value = AgL.XNull(.Rows(I)("ItemGroupName"))
                    Dgl1.Item(Col1xItemCategory, I).Tag = AgL.XNull(.Rows(I)("ItemCategory"))
                    Dgl1.Item(Col1xItemCategory, I).Value = AgL.XNull(.Rows(I)("ItemCategoryName"))
                    Dgl1.Item(Col1ItemCategory, I).Tag = AgL.XNull(.Rows(I)("ItemCategory"))
                    Dgl1.Item(Col1ItemCategory, I).Value = AgL.XNull(.Rows(I)("ItemCategoryName"))
                    Dgl1.Item(Col1ExtraDiscountPattern, I).Value = AgL.XNull(.Rows(I)("ExtraDiscountCalculationPattern"))
                    Dgl1.Item(Col1ExtraDiscountPer, I).Value = Format(Math.Abs(AgL.VNull(.Rows(I)("ExtraDiscountPer"))), "0.00")
                Next I
            End If
        End With
        Calculation()
    End Sub
    Sub KeyPress_Form(ByVal Sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        If e.KeyChar = Chr(Keys.Escape) Then
            Me.Close()
        End If
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            AgL.GridDesign(Dgl1)

            Me.Top = 400
            Me.Left = 400
            Dgl1.Focus()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Dgl1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        Try
            If AgL.StrCmp(EntryMode, "Browse") Then Exit Sub
            If Dgl1.CurrentCell Is Nothing Then Exit Sub
            Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) = Nothing

            Select Case Dgl1.CurrentCell.RowIndex




            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub Dgl1_EditingControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.EditingControl_KeyDown
        Dim bRowIndex As Integer = 0, bColumnIndex As Integer = 0
        Dim bItemCode As String = ""
        Dim DrTemp As DataRow() = Nothing
        Try
            bRowIndex = Dgl1.CurrentCell.RowIndex
            bColumnIndex = Dgl1.CurrentCell.ColumnIndex

            If e.KeyCode = Keys.Enter Then Exit Sub
            If mEntryMode = "Browse" Then Exit Sub


            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1ItemGroup
                    If e.KeyCode <> Keys.Enter And e.KeyCode <> Keys.Insert Then
                        If Dgl1.AgHelpDataSet(Col1ItemGroup) Is Nothing Then
                            mQry = "Select Code, Description from ItemGroup Order By Description"
                            Dgl1.AgHelpDataSet(Col1ItemGroup) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If
                Case Col1ItemCategory
                    If e.KeyCode <> Keys.Enter And e.KeyCode <> Keys.Insert Then
                        If Dgl1.AgHelpDataSet(Col1ItemCategory) Is Nothing Then
                            mQry = "Select Code, Description from ItemCategory Order By Description"
                            Dgl1.AgHelpDataSet(Col1ItemCategory) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If
                Case Col1ExtraDiscountPattern
                    If e.KeyCode <> Keys.Enter And e.KeyCode <> Keys.Insert Then
                        If Dgl1.AgHelpDataSet(Col1ExtraDiscountPattern) Is Nothing Then
                            mQry = ClsMain.GetStringsFromClassConstants(GetType(DiscountCalculationPattern))
                            Dgl1.AgHelpDataSet(Col1ExtraDiscountPattern) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub Dgl1_EditingControl_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Dgl1.EditingControl_Validating
        If EntryMode = "Browse" Then Exit Sub
        Dim mRowIndex As Integer, mColumnIndex As Integer
        Try
            mRowIndex = Dgl1.CurrentCell.RowIndex
            mColumnIndex = Dgl1.CurrentCell.ColumnIndex
            If Dgl1.Item(mColumnIndex, mRowIndex).Value Is Nothing Then Dgl1.Item(mColumnIndex, mRowIndex).Value = ""
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
            End Select
            Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub BtnChargeDuw_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim I As Integer = 0
        Select Case sender.Name
            Case BtnOk.Name
                If AgL.StrCmp(EntryMode, "Browse") Then Me.Close() : Exit Sub
                mOkButtonPressed = True
                Me.Close()
        End Select
    End Sub

    Private Sub DGL1_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles Dgl1.RowsAdded, Dgl1.RowsAdded
        sender(ColSNo, e.RowIndex).Value = e.RowIndex + 1
    End Sub
    Public Sub Calculation()
    End Sub
    Public Sub FSave(DocId As String, ByVal Conn As Object, ByVal Cmd As Object)
        Dim I As Integer

        mQry = " Delete From PersonExtraDiscount Where ItemGroup Is NUll And ItemCategory Is Null "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Rows(I).Visible Then
                If (Dgl1.Item(Col1ItemGroup, I).Value <> "" Or Dgl1.Item(Col1ItemCategory, I).Value <> "") And Val(Dgl1.Item(Col1ExtraDiscountPer, I).Value) <> 0 Then
                    mQry = " Insert Into PersonExtraDiscount(Person, ItemCategory, ItemGroup, ExtraDiscountPer) 
                            Values(" & AgL.Chk_Text(DocId) & ", " & AgL.Chk_Text(Dgl1.Item(Col1ItemCategory, I).Tag) & ", 
                            " & AgL.Chk_Text(Dgl1.Item(Col1ItemGroup, I).Tag) & ", 
                            " & AgL.Chk_Text(Dgl1.Item(Col1ExtraDiscountPer, I).Value) & ") "
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                End If
            End If
        Next

        If AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Or AgL.StrCmp(AgL.PubUserName, "sa") Then
            AgCL.GridSetiingWriteXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1)
        End If
    End Sub

    Private Sub Dgl1_KeyDown(sender As Object, e As KeyEventArgs) Handles Dgl1.KeyDown
        If EntryMode = "Browse" Then
            Select Case e.KeyCode
                Case Keys.Right, Keys.Up, Keys.Left, Keys.Down, Keys.Enter
                Case Else
                    e.Handled = True
            End Select
            Exit Sub
        End If

        If e.Control And e.KeyCode = Keys.D Then
            sender.CurrentRow.Selected = True
        End If

        If e.KeyCode = Keys.Delete Then
            If sender.currentrow.selected Then
                sender.Rows(sender.currentcell.rowindex).Visible = False
                Calculation()
                e.Handled = True
            End If
        End If

        If e.Control Or e.Shift Or e.Alt Then Exit Sub



    End Sub

    Private Sub BtnOk_Click(sender As Object, e As EventArgs) Handles BtnOk.Click
        Me.Close()
    End Sub
End Class