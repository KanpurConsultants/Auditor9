Imports System.Data.SQLite
Public Class FrmJobQcDetail
    Dim mQry As String = ""
    Public mOkButtonPressed As Boolean

    Public Const ColSNo As String = "S.No."
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Public Const Col1Parameter As String = "Parameter"
    Public Const Col1StdValue As String = "Std. Value"
    Public Const Col1ActValue As String = "Actual Value"
    Public Const Col1Qty As String = "Qty"
    Public Const Col1QcQty As String = "Qc Qty"
    Public Const Col1PassedQty As String = "Passed Qty"
    Public Const Col1RejectQty As String = "Reject Qty"
    Public Const Col1Unit As String = "Unit"
    Public Const Col1MeasurePerPcs As String = "Measure Per Pcs"
    Public Const Col1MeasureUnit As String = "Measure Unit"
    Public Const Col1TotalMeasure As String = "Total Measure"
    Public Const Col1TotalPassedMeasure As String = "Total Passed Measure"
    Public Const Col1TotalRejectMeasure As String = "Total Reject Measure"
    Public Const Col1Remarks As String = "Remarks"

    Dim mFrmReadOnly As Boolean = False

    Public Property FrmReadonly() As Boolean
        Get
            FrmReadonly = mFrmReadOnly
        End Get
        Set(ByVal value As Boolean)
            mFrmReadOnly = value
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
            .AddAgTextColumn(Dgl1, Col1Parameter, 150, 0, Col1Parameter, True, True)
            .AddAgTextColumn(Dgl1, Col1StdValue, 100, 0, Col1StdValue, True, True)
            .AddAgTextColumn(Dgl1, Col1ActValue, 100, 0, Col1ActValue, True, False)
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
            .AddAgTextColumn(Dgl1, Col1Remarks, 165, 255, Col1Remarks, True, False)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 35
        Dgl1.AllowUserToAddRows = False
        Dgl1.AgSkipReadOnlyColumns = True
    End Sub

    Sub KeyPress_Form(ByVal Sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        If e.KeyChar = Chr(Keys.Escape) Then Me.Close()
    End Sub

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            AgL.GridDesign(Dgl1)
            Dgl1.ReadOnly = FrmReadonly
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Calculation()
        Dim I As Integer
        Dim bTotalMeasure As Double = 0, bPassedMeasure As Double = 0, bRejectedMeasure As Double = 0

        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1Parameter, I).Value <> "" Then
                Dgl1.Item(Col1TotalMeasure, I).Value = Val(Dgl1.Item(Col1MeasurePerPcs, I).Value) * Val(Dgl1.Item(Col1Qty, I).Value)
                Dgl1.Item(Col1TotalPassedMeasure, I).Value = Val(Dgl1.Item(Col1MeasurePerPcs, I).Value) * Val(Dgl1.Item(Col1PassedQty, I).Value)
                Dgl1.Item(Col1TotalRejectMeasure, I).Value = Val(Dgl1.Item(Col1MeasurePerPcs, I).Value) * Val(Dgl1.Item(Col1RejectQty, I).Value)

                bTotalMeasure = bTotalMeasure + Val(Dgl1.Item(Col1TotalMeasure, I).Value)
                bPassedMeasure = bPassedMeasure + Val(Dgl1.Item(Col1TotalPassedMeasure, I).Value)
                bRejectedMeasure = bRejectedMeasure + Val(Dgl1.Item(Col1TotalRejectMeasure, I).Value)
            End If
        Next
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

    Private Sub BtnChargeDuw_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnOk.Click, BtnCancel.Click
        Select Case sender.Name
            Case BtnOk.Name
                mOkButtonPressed = True
                Me.Close()

            Case BtnCancel.Name
                Me.Close()
        End Select
    End Sub

    Public Sub ProcSaveQcParameterDetail(ByVal Conn As SqliteConnection, ByVal Cmd As SqliteCommand,
                           ByVal DocId As String, ByVal Uid As String, ByVal Sr As Integer)
        Dim J As Integer = 0, mLineSr As Integer = 0
        mQry = "Delete from QcParameterDetail_Log Where UID='" & Uid & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        With Dgl1
            For J = 0 To .Rows.Count - 1
                If .Item(FrmJobQcDetail.Col1Parameter, J).Value <> "" Then
                    mLineSr += 1
                    mQry = " INSERT INTO QcParameterDetail_Log(DocId, TSr, Sr, Parameter, StdValue, " &
                            " ActValue, Qty, QcQty, PassedQty, RejectQty, Unit, MeasurePerPcs, " &
                            " MeasureUnit, TotalMeasure,TotalPassedMeasure, " &
                            " TotalRejectMeasure, Remarks, UID) " &
                            " VALUES (" & AgL.Chk_Text(DocId) & ", " &
                            " " & Val(Sr) & ", " &
                            " " & Val(mLineSr) & ", " &
                            " " & AgL.Chk_Text(.Item(Col1Parameter, J).Value) & ", " &
                            " " & AgL.Chk_Text(.Item(Col1StdValue, J).Value) & ", " &
                            " " & AgL.Chk_Text(.Item(Col1ActValue, J).Value) & ", " &
                            " " & Val(.Item(Col1Qty, J).Value) & ", " &
                            " " & Val(.Item(Col1QcQty, J).Value) & ", " &
                            " " & Val(.Item(Col1PassedQty, J).Value) & ", " &
                            " " & Val(.Item(Col1RejectQty, J).Value) & ", " &
                            " " & AgL.Chk_Text(.Item(Col1Unit, J).Value) & ", " &
                            " " & Val(.Item(Col1MeasurePerPcs, J).Value) & ", " &
                            " " & AgL.Chk_Text(.Item(Col1MeasureUnit, J).Value) & ", " &
                            " " & Val(.Item(Col1TotalMeasure, J).Value) & ", " &
                            " " & Val(.Item(Col1TotalPassedMeasure, J).Value) & ", " &
                            " " & Val(.Item(Col1TotalRejectMeasure, J).Value) & ", " &
                            " " & AgL.Chk_Text(.Item(Col1Remarks, J).Value) & ", " &
                            " " & AgL.Chk_Text(Uid) & ")"
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                End If
            Next
        End With
    End Sub

    Public Function FunRetQcParameterDetail(ByVal SearchCode As String, ByVal TSr As Integer,
                           ByVal FrmType As ClsMain.EntryPointType) As FrmJobQcDetail
        Dim FrmObj As Form = Nothing
        Dim DtTemp As DataTable = Nothing
        Call IniGrid()
        Dim I As Integer = 0
        Try
            If FrmType = ClsMain.EntryPointType.Main Then
                mQry = "Select * from QcParameterDetail  " &
                    " Where DocId = '" & SearchCode & "' " &
                    " And TSr = " & Val(TSr) & " " &
                    " Order By Sr "
            Else
                mQry = "Select * from QcParameterDetail_Log  " &
                    " Where UID = '" & SearchCode & "' " &
                    " And TSr = " & Val(TSr) & " " &
                    " Order By Sr "
            End If

            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

            With DtTemp
                Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
                If DtTemp.Rows.Count > 0 Then
                    For I = 0 To DtTemp.Rows.Count - 1
                        Dgl1.Rows.Add()
                        Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count
                        Dgl1.Item(Col1Parameter, I).Value = AgL.XNull(.Rows(I)("Parameter"))
                        Dgl1.Item(Col1StdValue, I).Value = AgL.XNull(.Rows(I)("StdValue"))
                        Dgl1.Item(Col1ActValue, I).Value = AgL.XNull(.Rows(I)("ActValue"))
                        Dgl1.Item(Col1Qty, I).Value = AgL.VNull(.Rows(I)("Qty"))
                        Dgl1.Item(Col1QcQty, I).Value = AgL.VNull(.Rows(I)("QcQty"))
                        Dgl1.Item(Col1PassedQty, I).Value = AgL.VNull(.Rows(I)("PassedQty"))
                        Dgl1.Item(Col1RejectQty, I).Value = AgL.VNull(.Rows(I)("RejectQty"))
                        Dgl1.Item(Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                        Dgl1.Item(Col1MeasurePerPcs, I).Value = AgL.VNull(.Rows(I)("MeasurePerPcs"))
                        Dgl1.Item(Col1MeasureUnit, I).Value = AgL.XNull(.Rows(I)("MeasureUnit"))
                        Dgl1.Item(Col1TotalMeasure, I).Value = AgL.VNull(.Rows(I)("TotalMeasure"))
                        Dgl1.Item(Col1TotalPassedMeasure, I).Value = AgL.VNull(.Rows(I)("TotalPassedMeasure"))
                        Dgl1.Item(Col1TotalRejectMeasure, I).Value = AgL.VNull(.Rows(I)("TotalRejectMeasure"))
                        Dgl1.Item(Col1Remarks, I).Value = AgL.XNull(.Rows(I)("Remarks"))
                    Next I
                End If
            End With
            FunRetQcParameterDetail = Me
        Catch ex As Exception
            FunRetQcParameterDetail = Nothing
            MsgBox(ex.Message)
        End Try
    End Function
End Class