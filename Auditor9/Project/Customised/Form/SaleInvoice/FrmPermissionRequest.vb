Imports System.ComponentModel
Imports System.Data.SQLite
Imports AgLibrary.ClsMain.agConstants
Public Class FrmPermissionRequest
    Dim mQry As String = ""
    Public mOkButtonPressed As Boolean

    Public Const ColSNo As String = "S.No."
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Public Const Col1Head As String = "Head"
    Public Const Col1Mandatory As String = ""
    Public Const Col1Value As String = "Value"
    Public Const Col1HeadOriginal As String = "Head Original"

    Public Const rowFromDate As Integer = 0
    Public Const rowToDate As Integer = 1
    Public Const rowV_Type As Integer = 2
    Public Const rowDiv_Code As Integer = 3
    Public Const rowSite_Code As Integer = 4
    Public Const rowEntryNo As Integer = 5
    Public Const rowReason As Integer = 6

    Public Const HcFromDate As String = "From Date"
    Public Const HcToDate As String = "To Date"
    Public Const HcV_Type As String = "Voucher Type"
    Public Const HcDiv_Code As String = "Division"
    Public Const HcSite_Code As String = "Site"
    Public Const HcEntryNo As String = "Entry No"
    Public Const HcReason As String = "Reason"

    Private mSearchCode As String = ""
    Private mV_Type As String = ""
    Private mDiv_Code As String = ""
    Private mSite_Code As String = ""
    Private mManualRefNo As String = ""
    Public Property SearchCode() As String
        Get
            Return mSearchCode
        End Get
        Set(ByVal value As String)
            mSearchCode = value
        End Set
    End Property
    Public Property V_Type() As String
        Get
            Return mV_Type
        End Get
        Set(ByVal value As String)
            mV_Type = value
        End Set
    End Property
    Public Property Div_Code() As String
        Get
            Return mDiv_Code
        End Get
        Set(ByVal value As String)
            mDiv_Code = value
        End Set
    End Property
    Public Property Site_Code() As String
        Get
            Return mSite_Code
        End Get
        Set(ByVal value As String)
            mSite_Code = value
        End Set
    End Property
    Public Property ManualRefNo() As String
        Get
            Return mManualRefNo
        End Get
        Set(ByVal value As String)
            mManualRefNo = value
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
    Public Sub IniGrid(DocID As String, PartyCode As String, AcGroupNature As String)
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 35, 5, ColSNo, False, True, False)
            .AddAgTextColumn(Dgl1, Col1Head, 160, 255, Col1Head, True, True)
            .AddAgTextColumn(Dgl1, Col1Mandatory, 10, 20, Col1Mandatory, False, True)
            .AddAgTextColumn(Dgl1, Col1Value, 350, 255, Col1Value, True, False)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 35
        'Dgl1.AgSkipReadOnlyColumns = True
        Dgl1.AllowUserToAddRows = False
        Dgl1.TabIndex = Pnl1.TabIndex
        AgL.GridDesign(Dgl1)


        Dgl1.Rows.Add(7)
        Dgl1.Item(Col1Head, rowFromDate).Value = HcFromDate
        Dgl1.Item(Col1Head, rowToDate).Value = HcToDate
        Dgl1.Item(Col1Head, rowV_Type).Value = HcV_Type
        Dgl1.Item(Col1Head, rowDiv_Code).Value = HcEntryNo
        Dgl1.Item(Col1Head, rowSite_Code).Value = HcSite_Code
        Dgl1.Item(Col1Head, rowEntryNo).Value = HcEntryNo
        Dgl1.Item(Col1Head, rowReason).Value = HcReason

        FMoveRec()
    End Sub
    Sub KeyPress_Form(ByVal Sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        If e.KeyChar = Chr(Keys.Escape) Then
            Me.Close()
        End If
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            AgL.GridDesign(Dgl1)
            Me.Top = 230
            Me.Left = 300
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub Dgl1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        Try
            If Dgl1.CurrentCell Is Nothing Then Exit Sub

            If Me.Visible And Dgl1.ReadOnly = False And Dgl1.CurrentCell.RowIndex > 0 Then
                If Dgl1.CurrentCell.ColumnIndex = Dgl1.Columns(Col1Head).Index Then
                    SendKeys.Send("{Tab}")
                End If
            End If

            If Dgl1.CurrentCell.ColumnIndex <> Dgl1.Columns(Col1Value).Index Then Exit Sub

            Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) = Nothing
            CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Text_Value
            CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).MaxInputLength = 0
            Dgl1.Columns(Col1Value).DefaultCellStyle.WrapMode = DataGridViewTriState.True

            Select Case Dgl1.CurrentCell.RowIndex
                Case rowFromDate
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Date_Value
                Case rowToDate
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Date_Value
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

            If bColumnIndex <> Dgl1.Columns(Col1Value).Index Then Exit Sub

            Select Case Dgl1.CurrentCell.RowIndex
                Case rowEntryNo
                    If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = "select C.CityCode, C.CityName from City C  With (NoLock) Order by c.CityName "
                        Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                        Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
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
            Select Case Dgl1.CurrentCell.RowIndex
                Case rowEntryNo
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
    Public Sub FMoveRec()
        Dim DtTemp As DataTable = Nothing
        Dim I As Integer = 0

        Try
            Dgl1.Item(Col1Value, rowDiv_Code).Tag = mDiv_Code
            Dgl1.Item(Col1Value, rowDiv_Code).Value = AgL.Dman_Execute(" SELECT Name FROM Division WHERE Code = '" & mDiv_Code & "'", AgL.GCn).ExecuteScalar()

            Dgl1.Item(Col1Value, rowSite_Code).Tag = mSite_Code
            Dgl1.Item(Col1Value, rowSite_Code).Value = AgL.Dman_Execute(" SELECT Name FROM SiteMast WHERE Code = '" & mSite_Code & "'", AgL.GCn).ExecuteScalar()

            Dgl1.Item(Col1Value, rowEntryNo).Tag = mSearchCode
            'Dgl1.Item(Col1Value, rowEntryNo).Value = AgL.Dman_Execute(" SELECT ManualRefNo FROM " & mTableName & " WHERE DocId = '" & mSearchCode & "'", AgL.GCn).ExecuteScalar()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Public Sub FSave(ByVal SearchCode As String, ByVal Conn As Object, ByVal Cmd As Object)

        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    End Sub
    Private Sub FrmPermissionRequest_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If Me.ActiveControl IsNot Nothing Then
            If Not (TypeOf (Me.ActiveControl) Is AgControls.AgDataGrid) Then
                If e.KeyCode = Keys.Return Then SendKeys.Send("{Tab}")
            End If
        End If
    End Sub
    Private Sub FrmPermissionRequest_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        If Dgl1 IsNot Nothing Then
            If Dgl1.FirstDisplayedCell IsNot Nothing Then
                Dgl1.CurrentCell = Dgl1(Col1Value, rowFromDate) 'Dgl1.FirstDisplayedCell
                Dgl1.Focus()
            End If
        End If
    End Sub
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
    Private Sub TxtCashReceived_GotFocus(sender As Object, e As EventArgs)
        If Dgl1 IsNot Nothing Then
            If Dgl1.FirstDisplayedCell IsNot Nothing Then
                If Dgl1.Item(Col1Value, rowFromDate).Value = "" Then
                    Dgl1.CurrentCell = Dgl1(Col1Value, rowFromDate) 'Dgl1.FirstDisplayedCell
                    Dgl1.Focus()
                End If
            End If
        End If
    End Sub
End Class