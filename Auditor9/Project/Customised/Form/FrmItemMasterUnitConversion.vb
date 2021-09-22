Public Class FrmItemMasterUnitConversion
    Dim mQry As String = ""
    Public mOkButtonPressed As Boolean

    Public Const ColSNo As String = "S.No."
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Public Const Col1FromUnit As String = "From Unit"
    Public Const Col1FromQty As String = "From Qty"
    Public Const Col1Equal As String = "="
    Public Const Col1ToUnit As String = "To Unit"
    Public Const Col1ToQty As String = "To Qty"
    Public Const Col1Multiplier As String = "Multiplier"
    Public Const Col1FromQtyDecimalPlaces As String = "From Qty Decimal Places"
    Public Const Col1ToQtyDecimalPlaces As String = "To Qty Decimal Places"

    Dim mEntryMode$ = ""
    Dim mUnit$ = ""
    Dim mToQtyDecimalPlace As Integer

    Public Property EntryMode() As String
        Get
            EntryMode = mEntryMode
        End Get
        Set(ByVal value As String)
            mEntryMode = value
        End Set
    End Property

    Public Property Unit() As String
        Get
            Unit = mUnit
        End Get
        Set(ByVal value As String)
            mUnit = value
        End Set
    End Property


    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, 0)
    End Sub

    Public Sub IniGrid()
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 35, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl1, Col1FromUnit, 80, 20, Col1FromUnit, True)
            .AddAgNumberColumn(Dgl1, Col1FromQty, 80, 5, 4, False, Col1FromQty, True, False)
            .AddAgTextColumn(Dgl1, Col1Equal, 20, 20, Col1Equal, True, True)
            .AddAgNumberColumn(Dgl1, Col1ToQty, 80, 5, 4, False, Col1ToQty, True)
            .AddAgTextColumn(Dgl1, Col1ToUnit, 80, 20, Col1ToUnit, True, True)
            .AddAgNumberColumn(Dgl1, Col1Multiplier, 60, 5, 4, False, Col1Multiplier, True, True)
            .AddAgTextColumn(Dgl1, Col1FromQtyDecimalPlaces, 50, 0, Col1FromQtyDecimalPlaces, False, True, False)
            .AddAgTextColumn(Dgl1, Col1ToQtyDecimalPlaces, 50, 0, Col1ToQtyDecimalPlaces, False, True, False)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 35
        Dgl1.AgSkipReadOnlyColumns = True
    End Sub

    Function FData_Validation() As Boolean
        Dim I As Integer
        For I = 0 To Dgl1.Rows.Count - 1
            If Dgl1.Item(Col1FromUnit, I).Value = Dgl1.Item(Col1ToUnit, I).Value Then
                MsgBox("From Unit And To Unit should not be same at row no. " & I & ". can't continue.")
                Exit Function
            End If
        Next
        FData_Validation = True
    End Function

    Sub KeyPress_Form(ByVal Sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        If e.KeyChar = Chr(Keys.Escape) Then
            Me.Close()
        End If
    End Sub

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            AgL.GridDesign(Dgl1)
            mQry = "SELECT DecimalPlaces FROM Unit WHERE Code ='" & mUnit & "' "
            mToQtyDecimalPlace = AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Dgl1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        Try
            If AgL.StrCmp(EntryMode, "Browse") Then Exit Sub
            If Dgl1.CurrentCell Is Nothing Then Exit Sub
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1FromQty
                    CType(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex), AgControls.AgTextColumn).AgNumberRightPlaces = Val(Dgl1.Item(Col1FromQtyDecimalPlaces, Dgl1.CurrentCell.RowIndex).Value)

                Case Col1ToQty
                    CType(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex), AgControls.AgTextColumn).AgNumberRightPlaces = Val(Dgl1.Item(Col1ToQtyDecimalPlaces, Dgl1.CurrentCell.RowIndex).Value)

            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub DGL1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.KeyDown
        If e.Control And e.KeyCode = Keys.D Then
            sender.CurrentRow.Selected = True
        End If
        If e.Control Or e.Shift Or e.Alt Then Exit Sub
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
                Case Col1FromUnit
                    If Dgl1.AgHelpDataSet(Col1FromUnit) Is Nothing Then
                        mQry = " SELECT Code, Code AS Unit, DecimalPlaces  FROM Unit ORDER BY Code "
                        Dgl1.AgHelpDataSet(Col1FromUnit, 1) = AgL.FillData(mQry, AgL.GCn)
                    End If

                Case Col1ToUnit
                    If Dgl1.AgHelpDataSet(Col1ToUnit) Is Nothing Then
                        mQry = " SELECT Code, Code AS Unit, DecimalPlaces  FROM Unit ORDER BY Code "
                        Dgl1.AgHelpDataSet(Col1ToUnit, 1) = AgL.FillData(mQry, AgL.GCn)
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
                Case Col1FromUnit
                    Dgl1.Item(Col1Equal, mRowIndex).Value = "="
                    Dgl1.Item(Col1ToUnit, mRowIndex).Value = mUnit
                    Dgl1.Item(Col1ToQtyDecimalPlaces, mRowIndex).Value = mToQtyDecimalPlace
                    If Val(Dgl1.Item(Col1FromQty, mRowIndex).Value) = 0 Then
                        Dgl1.Item(Col1FromQty, mRowIndex).Value = "1"
                    End If

                    If Dgl1.AgSelectedValue(Col1FromUnit, mRowIndex) Is Nothing Then Dgl1.AgSelectedValue(Col1FromUnit, mRowIndex) = ""

                    If Dgl1.Item(Col1FromUnit, mRowIndex).Value.ToString.Trim = "" Or Dgl1.AgSelectedValue(Col1FromUnit, mRowIndex).ToString.Trim = "" Then
                        Dgl1.Item(Col1FromQtyDecimalPlaces, mRowIndex).Value = ""
                    Else
                        If Dgl1.AgDataRow IsNot Nothing Then
                            Dgl1.Item(Col1FromQtyDecimalPlaces, mRowIndex).Value = AgL.XNull(Dgl1.AgDataRow.Cells("DecimalPlaces").Value)
                        End If
                    End If


            End Select
            Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub BtnChargeDuw_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnOk.Click
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
        Dim I As Integer
        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1FromUnit, I).Value <> "" And Val(Dgl1.Item(Col1ToQty, I).Value) <> 0 Then
                Dgl1.Item(Col1Multiplier, I).Value = Val(Dgl1.Item(Col1ToQty, I).Value) / Val(Dgl1.Item(Col1FromQty, I).Value)
            End If
        Next
    End Sub

End Class