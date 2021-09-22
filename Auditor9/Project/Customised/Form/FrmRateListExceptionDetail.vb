Imports System.Data.SQLite
Imports AgLibrary.ClsMain.agConstants

Public Class FrmRateListExceptionDetail
    Dim mQry As String = ""
    Public mOkButtonPressed As Boolean

    Public Const ColSNo As String = "S.No."
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Public Const Col1Process As String = "Process"
    Public Const Col1Party As String = "Party"
    Public Const Col1RateType As String = "Rate Type"
    Public Const Col1ItemCategory As String = "Item Category"
    Public Const Col1ItemGroup As String = "Item Group"
    Public Const Col1Item As String = "Item"
    Public Const Col1Dimension1 As String = "Dimension1"
    Public Const Col1Dimension2 As String = "Dimension2"
    Public Const Col1Dimension3 As String = "Dimension3"
    Public Const Col1Dimension4 As String = "Dimension4"
    Public Const Col1Size As String = "Size"
    Public Const Col1Rate As String = "Rate"


    Dim mEntryMode$ = ""
    Dim mDglRow As DataGridViewRow
    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub
    Public Property EntryMode() As String
        Get
            EntryMode = mEntryMode
        End Get
        Set(ByVal value As String)
            mEntryMode = value
        End Set
    End Property
    Public Property DglRow() As DataGridViewRow
        Get
            DglRow = mDglRow
        End Get
        Set(ByVal value As DataGridViewRow)
            mDglRow = value
        End Set
    End Property
    Public Sub IniGrid(SearchCode As String)
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 35, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl1, Col1Process, 220, 0, Col1Process, True, False)
            .AddAgTextColumn(Dgl1, Col1Party, 100, 0, Col1Party, True, False)
            .AddAgTextColumn(Dgl1, Col1RateType, 100, 0, Col1RateType, True, False)
            .AddAgTextColumn(Dgl1, Col1ItemCategory, 200, 0, Col1ItemCategory, True, False)
            .AddAgTextColumn(Dgl1, Col1ItemGroup, 200, 0, Col1ItemGroup, True, False)
            .AddAgTextColumn(Dgl1, Col1Item, 400, 0, Col1Item, True, False)
            .AddAgTextColumn(Dgl1, Col1Dimension1, 100, 0, Col1Dimension1, True, False)
            .AddAgTextColumn(Dgl1, Col1Dimension2, 100, 0, Col1Dimension2, True, False)
            .AddAgTextColumn(Dgl1, Col1Dimension3, 100, 0, Col1Dimension3, True, False)
            .AddAgTextColumn(Dgl1, Col1Dimension4, 100, 0, Col1Dimension4, True, False)
            .AddAgTextColumn(Dgl1, Col1Size, 100, 0, Col1Size, True, False)
            .AddAgNumberColumn(Dgl1, Col1Rate, 80, 8, 2, False, Col1Rate, True, False, True)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 35
        Dgl1.AgSkipReadOnlyColumns = True
        Dgl1.AllowUserToOrderColumns = True

        ApplyUISetting()

        FMoverec(SearchCode)

        AgCL.GridSetiingShowXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1, False)
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
            If Dgl1.CurrentCell Is Nothing Then Exit Sub
            Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) = Nothing

            Select Case Dgl1.CurrentCell.RowIndex




            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub Dgl1_EditingControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.EditingControl_KeyDown
        Try
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Process
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1Process) Is Nothing Then
                            mQry = " Select H.SubCode, H.Name From SubGroup H Where H.SubGroupType = '" & SubgroupType.Process & "' Order By H.Name "
                            Dgl1.AgHelpDataSet(Col1Process) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub Dgl1_EditingControl_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Dgl1.EditingControl_Validating
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
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub BtnChargeDuw_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnOk.Click
        Dim I As Integer = 0
        Select Case sender.Name
            Case BtnOk.Name
                mOkButtonPressed = True
                Me.Close()
        End Select
    End Sub

    Private Sub DGL1_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles Dgl1.RowsAdded, Dgl1.RowsAdded
        sender(ColSNo, e.RowIndex).Value = e.RowIndex + 1
    End Sub
    Public Sub FPostRateListDetail(SearchCode As String, ByVal Conn As Object, ByVal Cmd As Object)
        Dim bRateListCode$ = ""
        Dim I As Integer, mSr As Integer

        FDataValidation()

        mSr = AgL.Dman_Execute("Select IfNull(Max(Sr),0) From RateListDetail With (NoLock) Where Code = '" & SearchCode & "'", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()
        For I = 0 To Dgl1.RowCount - 1
            If Val(Dgl1.Item(Col1Rate, I).Value) <> 0 Then
                mSr += 1
                mQry = "INSERT INTO RateListDetail(Code, Sr, Process, SubCode, RateType, ItemCategory, ItemGroup, Item, 
                        Dimension1, Dimension2, Dimension3, Dimension4, Size, Rate) 
                        VALUES(" & AgL.Chk_Text(SearchCode) & ", 
                        " & mSr & ", 
                        " & AgL.Chk_Text(Dgl1.Item(Col1Process, I).Tag) & ",
                        " & AgL.Chk_Text(Dgl1.Item(Col1Party, I).Tag) & ",
                        " & AgL.Chk_Text(Dgl1.Item(Col1RateType, I).Tag) & ",
                        " & AgL.Chk_Text(Dgl1.Item(Col1ItemCategory, I).Tag) & ",
                        " & AgL.Chk_Text(Dgl1.Item(Col1ItemGroup, I).Tag) & ",
                        " & AgL.Chk_Text(Dgl1.Item(Col1Item, I).Tag) & ",
                        " & AgL.Chk_Text(Dgl1.Item(Col1Dimension1, I).Tag) & ",
                        " & AgL.Chk_Text(Dgl1.Item(Col1Dimension2, I).Tag) & ",
                        " & AgL.Chk_Text(Dgl1.Item(Col1Dimension3, I).Tag) & ",
                        " & AgL.Chk_Text(Dgl1.Item(Col1Dimension4, I).Tag) & ",
                        " & AgL.Chk_Text(Dgl1.Item(Col1Size, I).Tag) & ",
                        " & Val(Dgl1.Item(Col1Rate, I).Value) & ") "
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            End If

            For J As Integer = 0 To Dgl1.Columns.Count - 1
                If AgL.XNull(Dgl1.Columns(J).Tag) <> "" And Dgl1.Columns(J).HeaderText.Contains("Rate") Then
                    If Val(Dgl1.Item(J, I).Value) > 0 Then
                        mSr += 1
                        mQry = "INSERT INTO RateListDetail(Code, Sr, Process, SubCode, RateType, ItemCategory, ItemGroup, Item, 
                        Dimension1, Dimension2, Dimension3, Dimension4, Size, Rate) 
                        VALUES(" & AgL.Chk_Text(SearchCode) & ", 
                        " & mSr & ", 
                        " & AgL.Chk_Text(Dgl1.Item(Col1Process, I).Tag) & ",
                        " & AgL.Chk_Text(Dgl1.Item(Col1Party, I).Tag) & ",
                        " & AgL.Chk_Text(Dgl1.Columns(J).Tag) & ",
                        " & AgL.Chk_Text(Dgl1.Item(Col1ItemCategory, I).Tag) & ",
                        " & AgL.Chk_Text(Dgl1.Item(Col1ItemGroup, I).Tag) & ",
                        " & AgL.Chk_Text(Dgl1.Item(Col1Item, I).Tag) & ",
                        " & AgL.Chk_Text(Dgl1.Item(Col1Dimension1, I).Tag) & ",
                        " & AgL.Chk_Text(Dgl1.Item(Col1Dimension2, I).Tag) & ",
                        " & AgL.Chk_Text(Dgl1.Item(Col1Dimension3, I).Tag) & ",
                        " & AgL.Chk_Text(Dgl1.Item(Col1Dimension4, I).Tag) & ",
                        " & AgL.Chk_Text(Dgl1.Item(Col1Size, I).Tag) & ",
                        " & Val(Dgl1.Item(J, I).Value) & ") "
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                    End If
                End If
            Next
        Next

        FPostItemRelation(SearchCode, Conn, Cmd)

        If AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Or AgL.StrCmp(AgL.PubUserName, "sa") Then
            AgCL.GridSetiingWriteXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1)
        End If
    End Sub
    Private Sub FPostItemRelation(SearchCode As String, Conn As Object, Cmd As Object)
        Dim bSr As Integer = 0

        bSr = AgL.Dman_Execute("Select IfNull(Max(Sr),0) From ItemRelation With (NoLock) 
                    Where Code = '" & SearchCode & "'", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()

        For I As Integer = 0 To Dgl1.Rows.Count - 1
            If Val(Dgl1.Item(Col1Rate, I).Value) <> 0 Then


                bSr += 1
                If AgL.VNull(AgL.Dman_Execute("Select Count(*) From ItemRelation With (NoLock) Where Item = '" & Dgl1.Item(Col1Dimension3, I).Tag & "'
                                And RelatedItem = '" & Dgl1.Item(Col1Dimension1, I).Tag & "'", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()) = 0 Then
                    mQry = " INSERT INTO ItemRelation(Code, Sr, Item, RelatedItem)
                    Select '" & SearchCode & "', " & bSr & ", " & AgL.Chk_Text(Dgl1.Item(Col1Dimension3, I).Tag) & ", 
                    " & AgL.Chk_Text(Dgl1.Item(Col1Dimension1, I).Tag) & ""
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                End If

                bSr += 1
                If AgL.VNull(AgL.Dman_Execute("Select Count(*) From ItemRelation With (NoLock) Where Item = '" & Dgl1.Item(Col1Dimension3, I).Tag & "'
                                And RelatedItem = '" & Dgl1.Item(Col1Dimension2, I).Tag & "'", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()) = 0 Then
                    mQry = " INSERT INTO ItemRelation(Code, Sr, Item, RelatedItem)
                    Select '" & SearchCode & "', " & bSr & ", " & AgL.Chk_Text(Dgl1.Item(Col1Dimension3, I).Tag) & ", 
                    " & AgL.Chk_Text(Dgl1.Item(Col1Dimension2, I).Tag) & ""
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                End If
            End If
        Next
    End Sub
    Private Sub Dgl1_KeyDown(sender As Object, e As KeyEventArgs) Handles Dgl1.KeyDown
        Select Case e.KeyCode
            Case Keys.Right, Keys.Up, Keys.Left, Keys.Down, Keys.Enter
            Case Else
                e.Handled = True
        End Select

        If e.Control And e.KeyCode = Keys.D Then
            sender.CurrentRow.Selected = True
        End If

        If e.KeyCode = Keys.Delete Then
            If sender.currentrow.selected Then
                sender.Rows(sender.currentcell.rowindex).Visible = False
                e.Handled = True
            End If
        End If

        If e.Control Or e.Shift Or e.Alt Then Exit Sub
    End Sub
    Private Sub FDataValidation()
        Dim I As Integer = 0
        For I = 0 To Dgl1.Rows.Count - 1
            If Val(Dgl1.Item(Col1Rate, I).Value) <> 0 Then
                If AgL.XNull(Dgl1.Item(Col1Process, I).Value) = "" Then
                    If AgL.XNull(DglRow.Cells(Col1Process).Value) <> "" Then
                        Dgl1.Item(Col1Process, I).Value = AgL.XNull(DglRow.Cells(Col1Process).Value)
                        Dgl1.Item(Col1Process, I).Tag = AgL.XNull(DglRow.Cells(Col1Process).Tag)
                    End If
                End If

                If AgL.XNull(Dgl1.Item(Col1Party, I).Value) = "" Then
                    If AgL.XNull(DglRow.Cells(Col1Party).Value) <> "" Then
                        Dgl1.Item(Col1Party, I).Value = AgL.XNull(DglRow.Cells(Col1Party).Value)
                        Dgl1.Item(Col1Party, I).Tag = AgL.XNull(DglRow.Cells(Col1Party).Value)
                    End If
                End If

                If AgL.XNull(Dgl1.Item(Col1ItemCategory, I).Value) = "" Then
                    If AgL.XNull(DglRow.Cells(Col1ItemCategory).Value) <> "" Then
                        Dgl1.Item(Col1ItemCategory, I).Value = AgL.XNull(DglRow.Cells(Col1ItemCategory).Value)
                        Dgl1.Item(Col1ItemCategory, I).Tag = AgL.XNull(DglRow.Cells(Col1ItemCategory).Tag)
                    End If
                End If

                If AgL.XNull(Dgl1.Item(Col1ItemGroup, I).Value) = "" Then
                    If AgL.XNull(DglRow.Cells(Col1ItemGroup).Value) <> "" Then
                        Dgl1.Item(Col1ItemGroup, I).Value = AgL.XNull(DglRow.Cells(Col1ItemGroup).Value)
                        Dgl1.Item(Col1ItemGroup, I).Tag = AgL.XNull(DglRow.Cells(Col1ItemGroup).Tag)
                    End If
                End If

                If AgL.XNull(Dgl1.Item(Col1Dimension1, I).Value) = "" Then
                    If AgL.XNull(DglRow.Cells(Col1Dimension1).Value) <> "" Then
                        Dgl1.Item(Col1Dimension1, I).Value = AgL.XNull(DglRow.Cells(Col1Dimension1).Value)
                        Dgl1.Item(Col1Dimension1, I).Tag = AgL.XNull(DglRow.Cells(Col1Dimension1).Tag)
                    End If
                End If

                If AgL.XNull(Dgl1.Item(Col1Dimension2, I).Value) = "" Then
                    If AgL.XNull(DglRow.Cells(Col1Dimension2).Value) <> "" Then
                        Dgl1.Item(Col1Dimension2, I).Value = AgL.XNull(DglRow.Cells(Col1Dimension2).Value)
                        Dgl1.Item(Col1Dimension2, I).Tag = AgL.XNull(DglRow.Cells(Col1Dimension2).Tag)
                    End If
                End If

                If AgL.XNull(Dgl1.Item(Col1Dimension3, I).Value) = "" Then
                    If AgL.XNull(DglRow.Cells(Col1Dimension3).Value) <> "" Then
                        Dgl1.Item(Col1Dimension3, I).Value = AgL.XNull(DglRow.Cells(Col1Dimension3).Value)
                        Dgl1.Item(Col1Dimension3, I).Tag = AgL.XNull(DglRow.Cells(Col1Dimension3).Tag)
                    End If
                End If

                If AgL.XNull(Dgl1.Item(Col1Dimension4, I).Value) = "" Then
                    If AgL.XNull(DglRow.Cells(Col1Dimension4).Value) <> "" Then
                        Dgl1.Item(Col1Dimension4, I).Value = AgL.XNull(DglRow.Cells(Col1Dimension4).Value)
                        Dgl1.Item(Col1Dimension4, I).Tag = AgL.XNull(DglRow.Cells(Col1Dimension4).Tag)
                    End If
                End If

                If AgL.XNull(Dgl1.Item(Col1Size, I).Value) = "" Then
                    If AgL.XNull(DglRow.Cells(Col1Size).Value) <> "" Then
                        Dgl1.Item(Col1Size, I).Value = AgL.XNull(DglRow.Cells(Col1Size).Value)
                        Dgl1.Item(Col1Size, I).Tag = AgL.XNull(DglRow.Cells(Col1Size).Tag)
                    End If
                End If
            End If
        Next
    End Sub
    Private Sub ApplyUISetting()
        For I As Integer = 0 To Dgl1.Columns.Count - 1
            Dgl1.Columns(I).Visible = False
        Next

        Dgl1.Columns(ColSNo).Visible = True
        Dgl1.Columns(Col1Process).Visible = True
        Dgl1.Columns(Col1Rate).Visible = True

        AddRateTypeVariant()
    End Sub
    Public Sub FMoverec(SearchCode As String)
        mQry = " SELECT Rt.Code As RateTypeCode, Rt.Description AS RateType
                FROM RateType Rt 
                LEFT JOIN RateTypeProcess Rtp ON Rtp.Code = Rt.Code
                WHERE IsNull(Rtp.Process,'') <> '" & Process.Sales & "' "
        Dim DtRateTypeForProcess As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
        If DtRateTypeForProcess.Rows.Count > 0 Then
            mQry = "Select L.Process, Max(P.Name) As ProcessName, Max(L.Sr) As Sr
                        From RateListDetail L
                        LEFT JOIN SubGroup P On L.Process = P.SubCode
                        Where L.Code = '" & SearchCode & "'
                        And IsNull(L.SubCode,'') = '" & DglRow.Cells(Col1Party).Tag & "'
                        And IsNull(L.ItemCategory,'') = '" & DglRow.Cells(Col1ItemCategory).Tag & "'
                        And IsNull(L.ItemGroup,'') = '" & DglRow.Cells(Col1ItemGroup).Tag & "'
                        And IsNull(L.Item,'') = '" & DglRow.Cells(Col1Item).Tag & "'
                        And IsNull(L.Dimension1,'') = '" & DglRow.Cells(Col1Dimension1).Tag & "'
                        And IsNull(L.Dimension2,'') = '" & DglRow.Cells(Col1Dimension2).Tag & "'
                        And IsNull(L.Dimension3,'') = '" & DglRow.Cells(Col1Dimension3).Tag & "'
                        And IsNull(L.Dimension4,'') = '" & DglRow.Cells(Col1Dimension4).Tag & "'
                        And IsNull(L.Size,'') = '" & DglRow.Cells(Col1Size).Tag & "'
                        Group By L.Process
                        Order By Sr "
            Dim DtProcess As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

            Dgl1.RowCount = 1
            Dgl1.Rows.Clear()

            For I As Integer = 0 To DtProcess.Rows.Count - 1
                Dgl1.Rows.Add()
                Dgl1.Item(Col1Process, I).Tag = AgL.XNull(DtProcess.Rows(I)("Process"))
                Dgl1.Item(Col1Process, I).Value = AgL.XNull(DtProcess.Rows(I)("ProcessName"))

                mQry = " Select L.RateType As RateTypeCode, Rt.Description As RateType, L.Rate
                        From RateListDetail L 
                        LEFT JOIN RateType Rt On L.RateType = Rt.Code
                        Where L.Code = '" & SearchCode & "'
                        And IsNull(L.Process,'') = '" & Dgl1.Item(Col1Process, I).Tag & "'
                        And IsNull(L.SubCode,'') = '" & DglRow.Cells(Col1Party).Tag & "'
                        And IsNull(L.ItemCategory,'') = '" & DglRow.Cells(Col1ItemCategory).Tag & "'
                        And IsNull(L.ItemGroup,'') = '" & DglRow.Cells(Col1ItemGroup).Tag & "'
                        And IsNull(L.Item,'') = '" & DglRow.Cells(Col1Item).Tag & "'
                        And IsNull(L.Dimension1,'') = '" & DglRow.Cells(Col1Dimension1).Tag & "'
                        And IsNull(L.Dimension2,'') = '" & DglRow.Cells(Col1Dimension2).Tag & "'
                        And IsNull(L.Dimension3,'') = '" & DglRow.Cells(Col1Dimension3).Tag & "'
                        And IsNull(L.Dimension4,'') = '" & DglRow.Cells(Col1Dimension4).Tag & "'
                        And IsNull(L.Size,'') = '" & DglRow.Cells(Col1Size).Tag & "' "
                Dim DtRateTypes As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

                For J As Integer = 0 To DtRateTypes.Rows.Count - 1
                    If AgL.XNull(DtRateTypes.Rows(J)("RateType")) <> "" Then
                        Dgl1.Item(Col1Rate + " " + AgL.XNull(DtRateTypes.Rows(J)("RateType")), I).Value = AgL.VNull(DtRateTypes.Rows(J)("Rate"))
                    Else
                        Dgl1.Item(Col1Rate, I).Value = AgL.VNull(DtRateTypes.Rows(J)("Rate"))
                    End If
                Next
            Next
        Else
            mQry = " Select L.Process, P.Name As ProcessName, L.Rate
                    From RateListDetail L 
                    LEFT JOIN Subgroup P On L.Process = P.SubCode
                    Where L.Code = '" & SearchCode & "'
                    And IsNull(L.SubCode,'') = '" & DglRow.Cells(Col1Party).Tag & "'
                    And IsNull(L.ItemCategory,'') = '" & DglRow.Cells(Col1ItemCategory).Tag & "'
                    And IsNull(L.ItemGroup,'') = '" & DglRow.Cells(Col1ItemGroup).Tag & "'
                    And IsNull(L.Item,'') = '" & DglRow.Cells(Col1Item).Tag & "'
                    And IsNull(L.Dimension1,'') = '" & DglRow.Cells(Col1Dimension1).Tag & "'
                    And IsNull(L.Dimension2,'') = '" & DglRow.Cells(Col1Dimension2).Tag & "'
                    And IsNull(L.Dimension3,'') = '" & DglRow.Cells(Col1Dimension3).Tag & "'
                    And IsNull(L.Dimension4,'') = '" & DglRow.Cells(Col1Dimension4).Tag & "'
                    And IsNull(L.Size,'') = '" & DglRow.Cells(Col1Size).Tag & "' "
            Dim DtProcess As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

            Dgl1.RowCount = 1
            Dgl1.Rows.Clear()

            For I As Integer = 0 To DtProcess.Rows.Count - 1
                Dgl1.Rows.Add()
                Dgl1.Item(Col1Process, I).Tag = AgL.XNull(DtProcess.Rows(I)("Process"))
                Dgl1.Item(Col1Process, I).Value = AgL.XNull(DtProcess.Rows(I)("ProcessName"))
                Dgl1.Item(Col1Rate, I).Value = AgL.VNull(DtProcess.Rows(I)("Rate"))
            Next
        End If
    End Sub
    Private Sub AddRateTypeVariant()
        mQry = " SELECT Rt.Code As RateTypeCode, Rt.Description AS RateType
                FROM RateType Rt 
                LEFT JOIN RateTypeProcess Rtp ON Rtp.Code = Rt.Code
                WHERE IsNull(Rtp.Process,'') <> '" & Process.Sales & "' "
        Dim DtRateTypeForProcess As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
        If DtRateTypeForProcess.Rows.Count > 0 Then
            'Dgl1.Columns(Col1Rate).Visible = False
            For I As Integer = 0 To DtRateTypeForProcess.Rows.Count - 1
                If Not Dgl1.Columns.Contains(Col1Rate + " " + AgL.XNull(DtRateTypeForProcess.Rows(I)("RateType"))) Then
                    AgCL.AddAgNumberColumn(Dgl1, Col1Rate + " " + AgL.XNull(DtRateTypeForProcess.Rows(I)("RateType")),
                        90, 8, 2, False, Col1Rate + " " + AgL.XNull(DtRateTypeForProcess.Rows(I)("RateType")),
                        True, False, True)
                    Dgl1.Columns(Col1Rate + " " + AgL.XNull(DtRateTypeForProcess.Rows(I)("RateType"))).Tag = AgL.XNull(DtRateTypeForProcess.Rows(I)("RateTypeCode"))
                Else
                    Dgl1.Columns(Col1Rate + " " + AgL.XNull(DtRateTypeForProcess.Rows(I)("RateType"))).visible = True
                End If
            Next
        End If
    End Sub
End Class