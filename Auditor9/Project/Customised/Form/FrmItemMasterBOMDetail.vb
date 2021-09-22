Public Class FrmItemMasterBOMDetail
    Dim mQry As String = ""
    Public mOkButtonPressed As Boolean

    Public Const ColSNo As String = "S.No."
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Public Const Col1Process As String = "Process"
    Public Const Col1Item As String = "Item"
    Public Const Col1Dimension1 As String = "Dimension1"
    Public Const Col1Dimension2 As String = "Dimension2"
    Public Const Col1Qty As String = "Qty"
    Public Const Col1Unit As String = "Unit"
    Public Const Col1QtyDecimalPlace As String = "QtyDecimalPlace"
    Public Const Col1WastagePer As String = "Wastage %"
    Public Const Col1BtnBOMDetail As String = "BOM"

    Dim mEntryMode$ = ""
    Dim mBatchQtyDecimalPlace As Integer

    Public Property EntryMode() As String
        Get
            EntryMode = mEntryMode
        End Get
        Set(ByVal value As String)
            mEntryMode = value
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
            .AddAgTextColumn(Dgl1, Col1Process, 120, 20, Col1Process, True)
            .AddAgTextColumn(Dgl1, Col1Item, 220, 20, Col1Item, True)
            .AddAgTextColumn(Dgl1, Col1Dimension1, 100, 20, Col1Dimension1, True)
            .AddAgTextColumn(Dgl1, Col1Dimension2, 100, 20, Col1Dimension2, True)
            .AddAgNumberColumn(Dgl1, Col1Qty, 60, 5, 4, False, Col1Qty, True)
            .AddAgTextColumn(Dgl1, Col1Unit, 60, 20, Col1Unit, True, True)
            .AddAgNumberColumn(Dgl1, Col1WastagePer, 60, 5, 4, False, Col1WastagePer)
            .AddAgTextColumn(Dgl1, Col1QtyDecimalPlace, 50, 0, Col1QtyDecimalPlace, False, True, False)
            .AddAgButtonColumn(Dgl1, Col1BtnBOMDetail, 60, Col1BtnBOMDetail, True, False)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 35
        Dgl1.AgSkipReadOnlyColumns = True

        Dgl1.Columns(Col1Dimension1).HeaderText = CType(AgL.XNull(AgL.PubDtEnviro.Rows(0)("Caption_Dimension1")), String)
        Dgl1.Columns(Col1Dimension2).HeaderText = CType(AgL.XNull(AgL.PubDtEnviro.Rows(0)("Caption_Dimension2")), String)
    End Sub

    Function FData_Validation() As Boolean
        Dim I As Integer
        For I = 0 To Dgl1.Rows.Count - 1
            If Dgl1.Item(Col1Item, I).Tag = LblItemName.Tag Then
                MsgBox("Base Item And Main Item should not be same at row no. " & I + 1 & ". can't continue.")
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
            mQry = "SELECT DecimalPlaces FROM Unit WHERE Code ='" & LblUnit.Text & "' "
            mBatchQtyDecimalPlace = AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Dgl1_CellContentClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellContentClick
        Dim FrmObj As FrmItemMasterBOMDetail = Nothing
        Dim bColumnIndex As Integer = 0
        Dim bRowIndex As Integer = 0
        Dim I As Integer = 0
        Try
            bColumnIndex = Dgl1.CurrentCell.ColumnIndex
            bRowIndex = Dgl1.CurrentCell.RowIndex
            If Dgl1.Item(Col1Item, bRowIndex).Value = "" Then Exit Sub
            Select Case Dgl1.Columns(e.ColumnIndex).Name
                Case Col1BtnBOMDetail
                    If Dgl1.Item(Col1BtnBOMDetail, bRowIndex).Style.ForeColor = Color.Red Then
                        FMoveRecItemBOMDetail(Dgl1.Item(Col1Item, bRowIndex).Tag, bRowIndex)
                        Dgl1.Item(Col1BtnBOMDetail, bRowIndex).Tag.Text = Dgl1.Item(Col1Item, bRowIndex).Value
                        Dgl1.Item(Col1BtnBOMDetail, bRowIndex).Tag.Location() = New Point(Me.Left + 40, Me.Top + 40)
                        Dgl1.Item(Col1BtnBOMDetail, bRowIndex).Tag.ShowDialog()
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message & " in Dgl1_CellContentClick function")
        End Try
    End Sub
    Public Sub FMoveRecItemBOMDetail(ByVal SearchCode As String, ByVal bRowIndex As Integer)
        Dim DtTemp As DataTable = Nothing
        Dim I As Integer = 0
        Try
            Dgl1.Item(Col1BtnBOMDetail, bRowIndex).Tag = FunRetNewBOMDetailObject()
            Dgl1.Item(Col1BtnBOMDetail, bRowIndex).Tag.Dgl1.Readonly = True
            mQry = " SELECT BD.*, IB.Description AS BaseItemDesc , I.Description AS ItemDesc , P.Description AS ProcessDesc, " & _
                    " D1.Description AS Dimension1Desc, D2.Description AS Dimension2Desc, IfNull(V.Cnt,0) AS Cnt " & _
                    " FROM BomDetail BD " & _
                    " LEFT JOIN Item IB On IB.Code = BD.BaseItem  " & _
                    " LEFT JOIN Process P ON P.NCat = BD.Process  " & _
                    " LEFT JOIN Dimension1 D1 ON D1.Code = BD.Dimension1  " & _
                    " LEFT JOIN Dimension2 D2 ON D2.Code = BD.Dimension2  " & _
                    " LEFT JOIN Item I On I.Code = BD.Item  " & _
                    " LEFT JOIN ( SELECT L.BaseItem, count(*) AS Cnt  FROM BomDetail L GROUP BY L.BaseItem ) V ON V.BaseItem = BD.Item " & _
                    " WHERE BD.BaseItem = '" & SearchCode & "' " & _
                    " ORDER BY BD.Sr "
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

            With DtTemp
                Dgl1.Item(Col1BtnBOMDetail, bRowIndex).Tag.Dgl1.RowCount = 1 : Dgl1.Item(Col1BtnBOMDetail, bRowIndex).Tag.Dgl1.Rows.Clear()
                If DtTemp.Rows.Count > 0 Then
                    For I = 0 To DtTemp.Rows.Count - 1
                        Dgl1.Item(Col1BtnBOMDetail, bRowIndex).Tag.Dgl1.Rows.Add()
                        Dgl1.Item(Col1BtnBOMDetail, bRowIndex).Tag.LblItemName.Text = AgL.XNull(.Rows(I)("BaseItemDesc"))
                        Dgl1.Item(Col1BtnBOMDetail, bRowIndex).Tag.LblItemName.tag = AgL.XNull(.Rows(I)("BaseItem"))
                        Dgl1.Item(Col1BtnBOMDetail, bRowIndex).Tag.LblUnit.Text = AgL.XNull(.Rows(I)("BatchUnit"))
                        Dgl1.Item(Col1BtnBOMDetail, bRowIndex).Tag.TxtBatchQty.Text = AgL.VNull(.Rows(I)("BatchQty"))
                        Dgl1.Item(Col1BtnBOMDetail, bRowIndex).Tag.Dgl1.Item(FrmItemMasterBOMDetail.ColSNo, I).Value = Dgl1.Item(Col1BtnBOMDetail, bRowIndex).Tag.Dgl1.Rows.Count - 1
                        Dgl1.Item(Col1BtnBOMDetail, bRowIndex).Tag.Dgl1.Item(FrmItemMasterBOMDetail.Col1Process, I).Value = AgL.XNull(.Rows(I)("ProcessDesc"))
                        Dgl1.Item(Col1BtnBOMDetail, bRowIndex).Tag.Dgl1.Item(FrmItemMasterBOMDetail.Col1Process, I).Tag = AgL.XNull(.Rows(I)("Process"))
                        Dgl1.Item(Col1BtnBOMDetail, bRowIndex).Tag.Dgl1.Item(FrmItemMasterBOMDetail.Col1Item, I).Value = AgL.XNull(.Rows(I)("ItemDesc"))
                        Dgl1.Item(Col1BtnBOMDetail, bRowIndex).Tag.Dgl1.Item(FrmItemMasterBOMDetail.Col1Item, I).Tag = AgL.XNull(.Rows(I)("Item"))
                        Dgl1.Item(Col1BtnBOMDetail, bRowIndex).Tag.Dgl1.Item(FrmItemMasterBOMDetail.Col1Dimension1, I).Value = AgL.XNull(.Rows(I)("Dimension1Desc"))
                        Dgl1.Item(Col1BtnBOMDetail, bRowIndex).Tag.Dgl1.Item(FrmItemMasterBOMDetail.Col1Dimension1, I).Tag = AgL.XNull(.Rows(I)("Dimension1"))
                        Dgl1.Item(Col1BtnBOMDetail, bRowIndex).Tag.Dgl1.Item(FrmItemMasterBOMDetail.Col1Dimension2, I).Value = AgL.XNull(.Rows(I)("Dimension2Desc"))
                        Dgl1.Item(Col1BtnBOMDetail, bRowIndex).Tag.Dgl1.Item(FrmItemMasterBOMDetail.Col1Dimension2, I).Tag = AgL.XNull(.Rows(I)("Dimension2"))
                        Dgl1.Item(Col1BtnBOMDetail, bRowIndex).Tag.Dgl1.Item(FrmItemMasterBOMDetail.Col1Qty, I).Value = AgL.VNull(.Rows(I)("Qty"))
                        Dgl1.Item(Col1BtnBOMDetail, bRowIndex).Tag.Dgl1.Item(FrmItemMasterBOMDetail.Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                        Dgl1.Item(Col1BtnBOMDetail, bRowIndex).Tag.Dgl1.Item(FrmItemMasterBOMDetail.Col1WastagePer, I).Value = AgL.VNull(.Rows(I)("WastagePer"))

                        If AgL.VNull(.Rows(I)("Cnt")) > 0 Then
                            Dgl1.Item(Col1BtnBOMDetail, bRowIndex).Tag.Dgl1.Item(FrmItemMasterBOMDetail.Col1BtnBOMDetail, I).Style.ForeColor = Color.Red
                        End If

                        Dgl1.Item(Col1BtnBOMDetail, bRowIndex).Tag.EntryMode = "Browse"
                    Next I
                End If
            End With

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function FunRetNewBOMDetailObject() As Object
        Dim FrmObj As FrmItemMasterBOMDetail
        Try
            FrmObj = New FrmItemMasterBOMDetail
            FrmObj.IniGrid()
            FunRetNewBOMDetailObject = FrmObj
        Catch ex As Exception
            FunRetNewBOMDetailObject = Nothing
            MsgBox(ex.Message)
        End Try
    End Function

    Private Sub Dgl1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        Try
            If AgL.StrCmp(EntryMode, "Browse") Then Exit Sub
            If Dgl1.CurrentCell Is Nothing Then Exit Sub
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Qty
                    ' CType(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex), AgControls.AgTextColumn).AgNumberRightPlaces = Val(Dgl1.Item(Col1QtyDecimalPlace, Dgl1.CurrentCell.RowIndex).Value)

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
                Case Col1Unit
                    If Dgl1.AgHelpDataSet(Col1Unit) Is Nothing Then
                        mQry = " SELECT Code, Code AS Unit, DecimalPlaces  FROM Unit ORDER BY Code "
                        Dgl1.AgHelpDataSet(Col1Unit, 1) = AgL.FillData(mQry, AgL.GCn)
                    End If

                Case Col1Item
                    If Dgl1.AgHelpDataSet(Col1Item) Is Nothing Then
                        mQry = " SELECT I.Code, I.Description AS Item, I.Unit, U.DecimalPlaces, IfNull(V.Cnt,0) AS Cnt " & _
                                " FROM Item I  " & _
                                " LEFT JOIN Unit U ON U.Code = I.Unit  " & _
                                " LEFT JOIN ( SELECT L.BaseItem, count(*) AS Cnt  FROM BomDetail L GROUP BY L.BaseItem ) V ON V.BaseItem = I.Code " & _
                                " ORDER BY I.Description "
                        Dgl1.AgHelpDataSet(Col1Item, 1) = AgL.FillData(mQry, AgL.GCn)
                    End If

                Case Col1Process
                    If Dgl1.AgHelpDataSet(Col1Process) Is Nothing Then
                        mQry = " SELECT NCat AS Code, Description  FROM Process  ORDER BY Description "
                        Dgl1.AgHelpDataSet(Col1Process) = AgL.FillData(mQry, AgL.GCn)
                    End If

                Case Col1Dimension1
                    If Dgl1.AgHelpDataSet(Col1Dimension1) Is Nothing Then
                        mQry = " SELECT Code, Description  FROM Dimension1 ORDER BY Description "
                        Dgl1.AgHelpDataSet(Col1Dimension1) = AgL.FillData(mQry, AgL.GCn)
                    End If

                Case Col1Dimension2
                    If Dgl1.AgHelpDataSet(Col1Dimension2) Is Nothing Then
                        mQry = " SELECT Code, Description  FROM Dimension2 ORDER BY Description "
                        Dgl1.AgHelpDataSet(Col1Dimension2) = AgL.FillData(mQry, AgL.GCn)
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
                Case Col1Item
                    If Dgl1.AgSelectedValue(Col1Item, mRowIndex) Is Nothing Then Dgl1.AgSelectedValue(Col1Item, mRowIndex) = ""
                    If Dgl1.Item(Col1Item, mRowIndex).Value.ToString.Trim = "" Or Dgl1.AgSelectedValue(Col1Item, mRowIndex).ToString.Trim = "" Then
                        Dgl1.Item(Col1QtyDecimalPlace, mRowIndex).Value = ""
                        Dgl1.Item(Col1Unit, mRowIndex).Value = ""
                        Dgl1.Item(Col1BtnBOMDetail, mRowIndex).Style.ForeColor = Color.Transparent
                    Else
                        If Dgl1.AgDataRow IsNot Nothing Then
                            Dgl1.Item(Col1QtyDecimalPlace, mRowIndex).Value = AgL.XNull(Dgl1.AgDataRow.Cells("DecimalPlaces").Value)
                            Dgl1.Item(Col1Unit, mRowIndex).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Unit").Value)

                            If AgL.VNull(Dgl1.AgDataRow.Cells("Cnt").Value) Then
                                Dgl1.Item(Col1BtnBOMDetail, mRowIndex).Style.ForeColor = Color.Red
                            Else
                                Dgl1.Item(Col1BtnBOMDetail, mRowIndex).Style.ForeColor = Color.Transparent
                            End If
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
                If FData_Validation() = False Then Exit Sub
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
            'If Dgl1.Item(Col1FromUnit, I).Value <> "" And Val(Dgl1.Item(Col1ToQty, I).Value) <> 0 Then
            '    Dgl1.Item(Col1Multiplier, I).Value = Val(Dgl1.Item(Col1ToQty, I).Value) / Val(Dgl1.Item(Col1FromQty, I).Value)
            'End If
        Next
    End Sub

    Private Sub TxtBatchQty_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtBatchQty.Enter
        Try
            If AgL.StrCmp(EntryMode, "Browse") Then Exit Sub
            Select Case sender.Name
                Case TxtBatchQty.Name
                    CType(TxtBatchQty, AgControls.AgTextBox).AgNumberRightPlaces = mBatchQtyDecimalPlace

            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class