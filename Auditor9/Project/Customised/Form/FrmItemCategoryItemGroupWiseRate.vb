Imports System.Data.SQLite
Imports AgLibrary.ClsMain.agConstants

Public Class FrmItemCategoryItemGroupWiseRate

    Dim mQry As String = ""
    Public mOkButtonPressed As Boolean

    Public Const ColSNo As String = "S.No."
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Public Const Col1ItemCategory As String = "Item Category"
    Public Const Col1ItemGroup As String = "Item Group"
    Public Const Col1PurchaseRate As String = "Purchase Rate"
    Public Const Col1SaleRate As String = "Sale Rate"


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

    Public Sub IniGrid()

        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 35, 5, ColSNo, False, True, False)
            .AddAgTextColumn(Dgl1, Col1ItemCategory, 130, 255, Col1ItemCategory, True, False)
            .AddAgTextColumn(Dgl1, Col1ItemGroup, 2000, 255, Col1ItemGroup, True, False)
            .AddAgNumberColumn(Dgl1, Col1PurchaseRate, 100, 5, 0, False, Col1PurchaseRate, True, False, True)
            .AddAgNumberColumn(Dgl1, Col1SaleRate, 100, 5, 0, False, Col1SaleRate, True, False, True)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 35
        Dgl1.AgSkipReadOnlyColumns = True
        Dgl1.AllowUserToOrderColumns = True

        AgCL.GridSetiingShowXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1, False)

        FMoverec()
    End Sub

    Public Sub FMoverec()
        Dim DsTemp As DataSet = Nothing
        Dim I As Integer
        mQry = "Select IC.Code as ItemCategory, Max(IC.Description) as ItemCategoryName, IG.Code as ItemGroup, Max(IG.Description) as ItemGroupName, Max(I.PurchaseRate) as PurchaseRate, Max(I.Rate) as SaleRate
                From  ItemGroup IG 
                Left Join ItemCategory IC On 1=1
                Left Join Item I On I.ItemGroup = IG.Code And I.ItemCategory=IC.Code               
                Where I.Code Is Not Null
                Group By IG.Code, IC.Code "
        DsTemp = AgL.FillData(mQry, AgL.GCn)
        With DsTemp.Tables(0)
            Dgl1.RowCount = 1
            Dgl1.Rows.Clear()
            If .Rows.Count > 0 Then
                For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                    Dgl1.Rows.Add()
                    Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1
                    'Dgl1.Item(ColSNo, I).Tag = AgL.XNull(.Rows(I)("Sr"))
                    Dgl1.Item(Col1ItemGroup, I).Tag = AgL.XNull(.Rows(I)("ItemGroup"))
                    Dgl1.Item(Col1ItemGroup, I).Value = AgL.XNull(.Rows(I)("ItemGroupName"))
                    Dgl1.Item(Col1ItemCategory, I).Tag = AgL.XNull(.Rows(I)("ItemCategory"))
                    Dgl1.Item(Col1ItemCategory, I).Value = AgL.XNull(.Rows(I)("ItemCategoryName"))
                    Dgl1.Item(Col1PurchaseRate, I).Value = Format(Math.Abs(AgL.VNull(.Rows(I)("PurchaseRate"))), "0.00")
                    Dgl1.Item(Col1SaleRate, I).Value = Format(Math.Abs(AgL.VNull(.Rows(I)("SaleRate"))), "0.00")
                Next I
            End If
        End With
        Calculation()
    End Sub

    'Function FData_Validation() As Boolean
    '    Dim I As Integer
    '    For I = 0 To Dgl1.Rows.Count - 1
    '        'If Dgl1.Item(Col1FromUnit, I).Value = Dgl1.Item(Col1ToUnit, I).Value Then
    '        '    MsgBox("From Unit And To Unit should not be same at row no. " & I & ". can't continue.")
    '        '    Exit Function
    '        'End If
    '    Next
    '    FData_Validation = True
    'End Function

    Sub KeyPress_Form(ByVal Sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        If e.KeyChar = Chr(Keys.Escape) Then
            Me.Close()
        End If
    End Sub

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            AgL.GridDesign(Dgl1)

            IniGrid()

            Me.Top = 0
            Me.Left = 0
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

    'Private Sub DGL1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.KeyDown
    '    If e.Control And e.KeyCode = Keys.D Then
    '        sender.CurrentRow.Selected = True
    '    End If
    '    If e.Control Or e.Shift Or e.Alt Then Exit Sub
    'End Sub

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
                'Case Col1FromUnit
                '    Dgl1.Item(Col1Equal, mRowIndex).Value = "="
                '    Dgl1.Item(Col1ToUnit, mRowIndex).Value = mUnit
                '    Dgl1.Item(Col1ToQtyDecimalPlaces, mRowIndex).Value = mToQtyDecimalPlace
                '    If Val(Dgl1.Item(Col1FromQty, mRowIndex).Value) = 0 Then
                '        Dgl1.Item(Col1FromQty, mRowIndex).Value = "1"
                '    End If

                '    If Dgl1.AgSelectedValue(Col1FromUnit, mRowIndex) Is Nothing Then Dgl1.AgSelectedValue(Col1FromUnit, mRowIndex) = ""

                '    If Dgl1.Item(Col1FromUnit, mRowIndex).Value.ToString.Trim = "" Or Dgl1.AgSelectedValue(Col1FromUnit, mRowIndex).ToString.Trim = "" Then
                '        Dgl1.Item(Col1FromQtyDecimalPlaces, mRowIndex).Value = ""
                '    Else
                '        If Dgl1.AgDataRow IsNot Nothing Then
                '            Dgl1.Item(Col1FromQtyDecimalPlaces, mRowIndex).Value = AgL.XNull(Dgl1.AgDataRow.Cells("DecimalPlaces").Value)
                '        End If
                '    End If


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



    Public Sub FSave()
        Dim I As Integer

        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Rows(I).Visible Then
                If Val(Dgl1.Item(Col1PurchaseRate, I).Value) <> 0 Then
                    mQry = " Update Item Set                                 
                                PurchaseRate=" & Val(Dgl1.Item(Col1PurchaseRate, I).Value) & "                                
                                Where  ItemCategory = '" & Dgl1.Item(Col1ItemCategory, I).Tag & "' And ItemGroup = '" & Dgl1.Item(Col1ItemGroup, I).Tag & "' "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
                End If

                If Val(Dgl1.Item(Col1SaleRate, I).Value) <> 0 Then
                    mQry = " Update Item Set                                                                 
                                Rate=" & Val(Dgl1.Item(Col1SaleRate, I).Value) & "
                                Where  ItemCategory = '" & Dgl1.Item(Col1ItemCategory, I).Tag & "' And ItemGroup = '" & Dgl1.Item(Col1ItemGroup, I).Tag & "' "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

                    mQry = " Update RateListDetail Set Rate=" & Val(Dgl1.Item(Col1SaleRate, I).Value) & " 
                           Where Item In (Select Code From Item Where ItemCategory = '" & Dgl1.Item(Col1ItemCategory, I).Tag & "' And ItemGroup = '" & Dgl1.Item(Col1ItemGroup, I).Tag & "') "

                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
                End If
            End If
        Next
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
        FSave()
        Me.Close()
    End Sub
End Class