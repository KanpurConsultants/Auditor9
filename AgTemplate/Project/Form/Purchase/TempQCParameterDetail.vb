Imports System.Data.SqlClient
Public Class TempQCParameterDetail
    Private DTMaster As New DataTable()
    Private KEAMainKeyCode As System.Windows.Forms.KeyEventArgs
    Private DTStruct As New DataTable
    Dim mQry As String = "", mSearchCode As String = ""
    Public mOkButtonPressed As Boolean

    ' Protected Const ColSNo As String = "S.No."
    Private Const ColSNo As Byte = 0
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Public Col1DocId As String = "DocId"
    Public Col1Parameter As String = "Parameter"
    Public Col1StdValue As String = "Std. Value"
    Public Col1ActualValue As String = "Actual Value"
    Public Col1Qty As String = "Qty"
    Public Col1QcQty As String = "Qc Qty"
    Public Col1PassedQty As String = "Passed Qty"
    Public Col1RejectQty As String = "Reject Qty"
    Public Col1Unit As String = "Unit"
    Public Col1MeasurePerPcs As String = "Measure Per Pcs"
    Public Col1MeasureUnit As String = "Measure Unit"
    Public Col1TotalMeasure As String = "Total Measure"
    Public Col1TotalPassedMeasure As String = "Total Passed Measure"
    Public Col1TotalRejectMeasure As String = "Total Reject Measure"
    Public Col1Remarks As String = "Remarks"
    Public Col1UID As String = "UID"

    Dim _ItemCode As String = "", _QCDocId As String = ""
    ' Dim _mIsNew As Boolean = False
    Dim _ItemQty As Double = 0, _QCQty As Double = 0
    ' Dim _PassedQty As Double = 0, _RejectedQty As Double = 0
    Dim _DataGridValue As DataTable = Nothing

    Public WriteOnly Property StrItemCode() As String
        'Get
        '    StrItemCode = _ItemCode
        'End Get
        Set(ByVal value As String)
            _ItemCode = value
        End Set
    End Property

    Public WriteOnly Property StrQCDocId() As String
        Set(ByVal value As String)
            _QCDocId = value
        End Set
    End Property

    Public WriteOnly Property DblItemQty() As Double
        Set(ByVal value As Double)
            _ItemQty = value
        End Set
    End Property

    Public WriteOnly Property DblQCQty() As Double
        Set(ByVal value As Double)
            _QCQty = value
        End Set
    End Property

    Public Property DataGridValue() As DataTable
        Get
            DataGridValue = _DataGridValue
        End Get
        Set(ByVal value As DataTable)
            _DataGridValue = value
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

    Private Sub Form_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        DTMaster = Nothing
    End Sub

    Private Sub IniGrid()
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 35, 5, "S.No.", True, True, False)
            .AddAgTextColumn(Dgl1, Col1DocId, 100, 20, Col1DocId, False, True)
            .AddAgTextColumn(Dgl1, Col1Parameter, 150, 0, Col1Parameter, True, True)
            .AddAgTextColumn(Dgl1, Col1StdValue, 100, 0, Col1StdValue, True, True)
            .AddAgTextColumn(Dgl1, Col1ActualValue, 100, 0, Col1ActualValue, True, False)
            .AddAgNumberColumn(Dgl1, Col1Qty, 65, 5, 4, False, Col1Qty, False, True, True)
            .AddAgNumberColumn(Dgl1, Col1QcQty, 65, 5, 4, False, Col1QcQty, False, True, True)
            .AddAgNumberColumn(Dgl1, Col1PassedQty, 65, 5, 4, False, Col1PassedQty, True, False, True)
            .AddAgNumberColumn(Dgl1, Col1RejectQty, 65, 5, 4, False, Col1RejectQty, True, False, True)
            .AddAgTextColumn(Dgl1, Col1Unit, 80, 20, Col1Unit, True, True)
            .AddAgNumberColumn(Dgl1, Col1MeasurePerPcs, 65, 5, 4, False, Col1MeasurePerPcs, False, True, True)
            .AddAgTextColumn(Dgl1, Col1MeasureUnit, 80, 20, Col1MeasureUnit, False, True)
            .AddAgNumberColumn(Dgl1, Col1TotalMeasure, 65, 5, 4, False, Col1TotalMeasure, False, False, True)
            .AddAgNumberColumn(Dgl1, Col1TotalPassedMeasure, 65, 5, 4, False, Col1TotalPassedMeasure, False, False, True)
            .AddAgNumberColumn(Dgl1, Col1TotalRejectMeasure, 65, 5, 4, False, Col1TotalRejectMeasure, False, False, True)
            .AddAgTextColumn(Dgl1, Col1Remarks, 150, 255, Col1Remarks, True, False)
            .AddAgTextColumn(Dgl1, Col1UID, 80, 21, Col1UID, False, True)

        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 35
        Dgl1.AllowUserToAddRows = False

        Dgl1.AgSkipReadOnlyColumns = True
        'Dgl1.Anchor = AnchorStyles.None
        'Panel1.Anchor = Dgl1.Anchor
    End Sub


    Private Sub KeyDown_Form(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F5 Then
            Topctrl1.TopKey_Down(e)
        End If
        If Me.ActiveControl IsNot Nothing Then
            If Not (TypeOf (Me.ActiveControl) Is AgControls.AgDataGrid) Then
                If e.KeyCode = Keys.Return Then SendKeys.Send("{Tab}")
            End If

            'If e.KeyCode = Keys.Insert Then OpenLinkForm(Me.ActiveControl)
        End If
    End Sub

    Private Sub OpenLinkForm(ByVal Sender As Object)

        Try
            Me.Cursor = Cursors.WaitCursor
            'If Topctrl1.Mode = "Browse" Then Exit Sub

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            Me.Cursor = Cursors.Default

        End Try
    End Sub

    Sub KeyPress_Form(ByVal Sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        If e.KeyChar = Chr(Keys.Escape) Then Exit Sub
        If Me.ActiveControl Is Nothing Then Exit Sub
        AgL.CheckQuote(e)
    End Sub

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'AgL.WinSetting(Me, 297, 333, 0, 0)
            AgL.GridDesign(DGL1)
            IniGrid()
            'Topctrl1.ChangeAgGridState(DGL1, False)
            Ini_List()
            DispText()
            Topctrl1.Mode = "Add"
            If _DataGridValue.Rows.Count > 0 Then
                Call Moverec()
            Else
                Call FillParameterDetail()
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Calculation()
        Dim I As Integer
        ' Dim bTotalQty As Double = 0, bTotalQCQty As Double = 0, bTotalPassedQty As Double = 0, bTotalRejectedQty As Double = 0
        Dim bTotalMeasure As Double = 0, bPassedMeasure As Double = 0, bRejectedMeasure As Double = 0

        ' LblValTotalQty.Text = 0 : LblValueTotalPassed.Text = 0 : LblValTotalRejected.Text = 0

        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1Parameter, I).Value <> "" Then
                'bTotalQty = bTotalQty + Val(Dgl1.Item(Col1Qty, I).Value)
                'bTotalQCQty = bTotalQCQty + Val(Dgl1.Item(Col1QcQty, I).Value)
                'bTotalPassedQty = bTotalPassedQty + Val(Dgl1.Item(Col1PassedQty, I).Value)
                'bTotalRejectedQty = bTotalRejectedQty + Val(Dgl1.Item(Col1RejectedQty, I).Value)

                Dgl1.Item(Col1TotalMeasure, I).Value = Val(Dgl1.Item(Col1MeasurePerPcs, I).Value) * Val(Dgl1.Item(Col1Qty, I).Value)
                Dgl1.Item(Col1TotalPassedMeasure, I).Value = Val(Dgl1.Item(Col1MeasurePerPcs, I).Value) * Val(Dgl1.Item(Col1PassedQty, I).Value)
                Dgl1.Item(Col1TotalRejectMeasure, I).Value = Val(Dgl1.Item(Col1MeasurePerPcs, I).Value) * Val(Dgl1.Item(Col1RejectQty, I).Value)

                bTotalMeasure = bTotalMeasure + Val(Dgl1.Item(Col1TotalMeasure, I).Value)
                bPassedMeasure = bPassedMeasure + Val(Dgl1.Item(Col1TotalPassedMeasure, I).Value)
                bRejectedMeasure = bRejectedMeasure + Val(Dgl1.Item(Col1TotalRejectMeasure, I).Value)

            End If
        Next

    End Sub

    Private Sub Moverec()
        Dim I As Integer
        With _DataGridValue
            If _DataGridValue.Rows.Count > 0 Then
                ' Dgl1.RowCount = 0
                'Dgl1.Rows.Clear()

                For I = 0 To _DataGridValue.Rows.Count - 1
                    Dgl1.Rows.Add()
                    Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count
                    Dgl1.Item(Col1Parameter, I).Value = AgL.XNull(.Rows(I)("Parameter"))
                    Dgl1.Item(Col1StdValue, I).Value = AgL.XNull(.Rows(I)("StdValue"))
                    Dgl1.Item(Col1ActualValue, I).Value = AgL.XNull(.Rows(I)("ActValue"))
                    Dgl1.Item(Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                    Dgl1.Item(Col1MeasurePerPcs, I).Value = AgL.VNull(.Rows(I)("MeasurePerPcs"))
                    Dgl1.Item(Col1MeasureUnit, I).Value = AgL.XNull(.Rows(I)("MeasureUnit"))
                    Dgl1.Item(Col1Qty, I).Value = AgL.VNull(.Rows(I)("Qty"))
                    Dgl1.Item(Col1QcQty, I).Value = AgL.VNull(.Rows(I)("QCQty"))
                    Dgl1.Item(Col1PassedQty, I).Value = AgL.VNull(.Rows(I)("PassedQty"))
                    Dgl1.Item(Col1RejectQty, I).Value = AgL.VNull(.Rows(I)("RejectQty"))
                    Dgl1.Item(Col1TotalMeasure, I).Value = AgL.VNull(.Rows(I)("TotalMeasure"))
                    Dgl1.Item(Col1TotalPassedMeasure, I).Value = AgL.VNull(.Rows(I)("TotalPassedMeasure"))
                    Dgl1.Item(Col1TotalRejectMeasure, I).Value = AgL.VNull(.Rows(I)("TotalRejectMeasure"))
                    Dgl1.Item(Col1Remarks, I).Value = AgL.XNull(.Rows(I)("Remarks"))
                Next I

                TxtItem.AgSelectedValue = _ItemCode
                LblItemName.Text = TxtItem.Text
                LblQcQty.Text = Val(_QCQty)
                LblQty.Text = Val(_ItemQty)
            End If
        End With
    End Sub

    Private Sub FillParameterDetail()
        Dim I As Integer
        Dim DsTemp As DataSet
        If _ItemCode <> "" Then
            mQry = " SELECT I.Code ,I.Description AS ItemDesc,I.Unit,I.Measure,I.MeasureUnit, " & _
                    " QG.Parameter, QG.StdValue " & _
                    " FROM Item I " & _
                    " LEFT JOIN QcGroupDetail  QG ON QG.Code=I.QcGroup  " & _
                    " Where I.Code =" & AgL.Chk_Text(_ItemCode) & " AND IfNull(QG.Parameter,'') <> '' "
            DsTemp = AgL.FillData(mQry, AgL.GCn)
            With DsTemp.Tables(0)
                ' Dgl1.RowCount = 0
                'Dgl1.Rows.Clear()
                If .Rows.Count > 0 Then
                    For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                        Dgl1.Rows.Add()
                        Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count
                        Dgl1.Item(Col1Parameter, I).Value = AgL.XNull(.Rows(I)("Parameter"))
                        Dgl1.Item(Col1StdValue, I).Value = AgL.XNull(.Rows(I)("StdValue"))
                        Dgl1.Item(Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                        Dgl1.Item(Col1MeasurePerPcs, I).Value = Format(AgL.VNull(.Rows(I)("Measure")), "0.000")
                        Dgl1.Item(Col1MeasureUnit, I).Value = AgL.XNull(.Rows(I)("MeasureUnit"))
                        Dgl1.Item(Col1Qty, I).Value = Format(Val(_ItemQty), "0.000")
                        Dgl1.Item(Col1QcQty, I).Value = Format(Val(_QCQty), "0.000")
                        Dgl1.Item(Col1PassedQty, I).Value = Format(Val(_QCQty), "0.000")
                    Next I
                    TxtItem.AgSelectedValue = _ItemCode
                    LblItemName.Text = TxtItem.Text
                    LblQcQty.Text = Format(Val(_QCQty), "0.000")
                    LblQty.Text = Format(Val(_ItemQty), "0.000")
                Else
                    MsgBox("Parameter is not Find ") : Me.Close()
                End If

            End With
        End If

    End Sub

    Sub Ini_List()
        Try
            '<Executable Code>
            mQry = " SELECT I.Code, I.Description AS Item,I.Unit,IfNull(I.IsDeleted,0) AS IsDeleted, '' AS ChallanDocId, " & _
                    " IfNull(I.Status , '" & AgTemplate.ClsMain.EntryStatus.Active & "') AS Status,I.Div_Code  FROM Item I "
            'Dgl1.AgHelpDataSet(Col1Item, 3) = AgL.FillData(mQry, AgL.GCn)
            TxtItem.AgHelpDataSet(3) = AgL.FillData(mQry, AgL.GCn)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Topctrl1_tbRef() Handles Topctrl1.tbRef
        Ini_List()
    End Sub

    Private Sub BlankText()
        Topctrl1.BlankTextBoxes(Me)
        Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
    End Sub

    Private Sub DispText(Optional ByVal Enb As Boolean = False)
        'Coding To Enable/Disable Controls
        TxtItem.Visible = False
    End Sub

    Private Sub DGL1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        Dim mRowIndex As Integer, mColumnIndex As Integer

        Try
            mRowIndex = Dgl1.CurrentCell.RowIndex
            mColumnIndex = Dgl1.CurrentCell.ColumnIndex

            If Dgl1.Item(mColumnIndex, mRowIndex).Value Is Nothing Then Dgl1.Item(mColumnIndex, mRowIndex).Value = ""
            Select Case Dgl1.CurrentCell.ColumnIndex

            End Select

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Dgl1_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellValueChanged
        Dim mRowIndex As Integer, mColumnIndex As Integer

        Try
            mRowIndex = Dgl1.CurrentCell.RowIndex
            mColumnIndex = Dgl1.CurrentCell.ColumnIndex

            Select Case Dgl1.CurrentCell.ColumnIndex
                'Case <ColumnIndex>
                'Call Calculation()
            End Select

        Catch ex As Exception

        End Try
    End Sub

    Private Sub DGL1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.KeyDown
        If e.Control And e.KeyCode = Keys.D Then
            sender.CurrentRow.Selected = True
        End If
        If e.Control Or e.Shift Or e.Alt Then Exit Sub
    End Sub

    Private Sub DGL1_EditingControl_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Dgl1.EditingControl_Validating
        Dim DrTemp As DataRow() = Nothing
        Dim mRowIndex As Integer, mColumnIndex As Integer
        Try
            mRowIndex = Dgl1.CurrentCell.RowIndex
            mColumnIndex = Dgl1.CurrentCell.ColumnIndex

            If Dgl1.Item(mColumnIndex, mRowIndex).Value Is Nothing Then Dgl1.Item(mColumnIndex, mRowIndex).Value = ""

            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1PassedQty
                    If Val(Dgl1.Item(Col1PassedQty, mRowIndex).Value) > Val(LblQcQty.Text) Then
                        MsgBox(" Qty Is Invalid !") : Dgl1.Item(Col1PassedQty, mRowIndex).Value = LblQcQty.Text
                        Dgl1.Item(Col1RejectQty, mRowIndex).Value = Val(Dgl1.Item(Col1QcQty, mRowIndex).Value) - Val(Dgl1.Item(Col1PassedQty, mRowIndex).Value)
                    Else
                        Dgl1.Item(Col1RejectQty, mRowIndex).Value = Val(LblQcQty.Text) - Val(Dgl1.Item(Col1PassedQty, mRowIndex).Value)
                    End If

                Case Col1RejectQty
                    If Val(Dgl1.Item(Col1RejectQty, mRowIndex).Value) > Val(LblQcQty.Text) Then
                        MsgBox(" Qty Is Invalid !") : Dgl1.Item(Col1RejectQty, mRowIndex).Value = LblQcQty.Text
                        Dgl1.Item(Col1PassedQty, mRowIndex).Value = Val(Dgl1.Item(Col1QcQty, mRowIndex).Value) - Val(Dgl1.Item(Col1RejectQty, mRowIndex).Value)
                    Else
                        Dgl1.Item(Col1PassedQty, mRowIndex).Value = Val(LblQcQty.Text) - Val(Dgl1.Item(Col1RejectQty, mRowIndex).Value)
                    End If
            End Select

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub DGL1_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles Dgl1.RowsAdded
        sender(ColSNo, sender.Rows.Count - 1).Value = Trim(sender.Rows.Count)
    End Sub

    Private Sub DGL1_RowsRemoved(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs) Handles Dgl1.RowsRemoved
        Try
            DTStruct.Rows.Remove(DTStruct.Rows.Item(e.RowIndex))
        Catch ex As Exception
        End Try
        AgL.FSetSNo(sender, ColSNo)
    End Sub

    Private Sub FClear()
        DTStruct.Clear()
    End Sub

    Private Sub FAddRowStructure()
        Dim DRStruct As DataRow
        Try
            DRStruct = DTStruct.NewRow
            DTStruct.Rows.Add(DRStruct)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Control_Enter(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Select Case sender.name
                '<Executable Code>
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Control_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Dim DtTemp As DataTable = Nothing
        Dim DrTemp As DataRow() = Nothing
        Try
            Select Case sender.NAME
                '<Executable Code>
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            If DtTemp IsNot Nothing Then DtTemp.Dispose()
        End Try
    End Sub

    Private Function Data_Validation() As Boolean
        Dim I As Integer = 0, bIntTotalSubjects As Integer = 0
        Dim bStrSubjectList As String = "", bStrMarksList As String = "", bStrPercentageList As String = ""
        Dim bDblTotalMarks As Double = 0, bDblTotalPercentage As Double = 0
        Try
            If AgCL.AgIsDuplicate(Dgl1, "" & Col1Parameter & "") Then Exit Function

            With Dgl1
                For I = 0 To .Rows.Count - 1

                    If .Item(Col1Parameter, I).Value <> "" Then

                    End If
                Next
            End With



            Data_Validation = True
        Catch ex As Exception
            MsgBox(ex.Message)
            Data_Validation = False
        End Try
    End Function

    Private Sub BtnChargeDuw_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnOk.Click, BtnCancel.Click
        Select Case sender.Name
            Case BtnOk.Name
                If Not Data_Validation() Then Exit Sub
                mOkButtonPressed = True
                Me.Visible = False
            Case BtnCancel.Name
                Me.Visible = False
        End Select

    End Sub

End Class