Public Class FrmModuleList
    Private DTMaster As New DataTable()
    Public BMBMaster As BindingManagerBase
    Private KEAMainKeyCode As System.Windows.Forms.KeyEventArgs
    Dim mQry As String = "", mSearchCode As String = ""

    Private Const Col1Select As Byte = 0
    Private Const Col1Module As Byte = 1


    Public mStrModuleList As String = ""

    Private Const StrModuleSql As String = "SELECT MnuModule AS Module FROM User_Permission WHERE UserName = 'SA' GROUP BY MnuModule"

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Public Property StrMoudleList() As String
        Get
            StrMoudleList = mStrModuleList
        End Get
        Set(ByVal value As String)
            mStrModuleList = value
        End Set
    End Property

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, 0)
    End Sub

    Private Sub Form_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        DTMaster = Nothing
    End Sub

    Private Sub IniGrid()
        Dim I As Integer
        With Dgl2
            .Columns(Col1Select).Width = 50
            .Columns(Col1Module).Width = 390
            .RowHeadersVisible = False

            For I = 1 To .Columns.Count - 1
                .Columns(I).ReadOnly = True
            Next

        End With
    End Sub

    Private Sub KeyDown_Form(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        'If e.KeyCode = Keys.F2 Or e.KeyCode = Keys.F3 Or e.KeyCode = Keys.F4 Or e.KeyCode = (Keys.F And e.Control) Or e.KeyCode = (Keys.P And e.Control) _
        'Or e.KeyCode = (Keys.S And e.Control) Or e.KeyCode = Keys.Escape Or e.KeyCode = Keys.F5 Or e.KeyCode = Keys.F10 _
        'Or e.KeyCode = Keys.Home Or e.KeyCode = Keys.PageUp Or e.KeyCode = Keys.PageDown Or e.KeyCode = Keys.End Then
        '    Topctrl1.TopKey_Down(e)
        'End If


        If Me.ActiveControl IsNot Nothing Then
            If Not (TypeOf (Me.ActiveControl) Is AgControls.AgDataGrid) Then
                If e.KeyCode = Keys.Return Then SendKeys.Send("{Tab}")
            End If
        End If
    End Sub

    Sub KeyPress_Form(ByVal Sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        If e.KeyChar = Chr(Keys.Escape) Then Exit Sub
        If Me.ActiveControl Is Nothing Then Exit Sub
        AgL.CheckQuote(e)
    End Sub

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'AgL.WinSetting(Me, 340, 650, 0, 0)
            AgL.GridDesign(Dgl2)
            MoveRec()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub MoveRec()
        Dim DsTemp As DataSet = Nothing
        Dim mCondStr$ = "", mCondStr1$ = ""

        Try
            Dgl2.DataSource = Nothing

            If mStrModuleList.Trim <> "" Then
                mCondStr = " Where vUp.Module In (" & Replace(mStrModuleList, "|", "'") & ") "
                mCondStr1 = " Where vUp.Module Not In (" & Replace(mStrModuleList, "|", "'") & ") "
            Else
                mCondStr = " Where 1 = 2 "
            End If

            mQry = "Select Convert(BIT,1) AS [Select], vUp.Module As [Module] " & _
                    " From (" & StrModuleSql & ") As vUp " & mCondStr
            mQry = mQry & " UNION ALL " & _
                    " Select Convert(BIT,0) AS [Select], vUp.Module As [Module] " & _
                    " From (" & StrModuleSql & ") As vUp " & mCondStr1

            DsTemp = AgL.FillData(mQry, AgL.GCn)
            Dgl2.DataSource = DsTemp.Tables(0)
            Call IniGrid()

            If DsTemp.Tables(0).Rows.Count = 0 Then MsgBox("Module Details Is Not Available!...")
        Catch ex As Exception
            Dgl2.DataSource = Nothing
            MsgBox(ex.Message)
        Finally
            DsTemp = Nothing
        End Try
    End Sub



    Private Sub DGL1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl2.CellEnter
        Dim mRowIndex As Integer, mColumnIndex As Integer

        Try
            mRowIndex = Dgl2.CurrentCell.RowIndex
            mColumnIndex = Dgl2.CurrentCell.ColumnIndex

            Select Case Dgl2.CurrentCell.ColumnIndex
                Case Col1Select
            End Select
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Dgl1_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl2.CellValueChanged
        Dim mRowIndex As Integer, mColumnIndex As Integer

        Try
            mRowIndex = Dgl2.CurrentCell.RowIndex
            mColumnIndex = Dgl2.CurrentCell.ColumnIndex

            Select Case Dgl2.CurrentCell.ColumnIndex
                Case Col1Select
                    '<Executable Code>
            End Select

        Catch ex As Exception

        End Try
    End Sub

    Private Sub DGL1_EditingControl_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Dgl2.EditingControl_Validating
        Dim mRowIndex As Integer, mColumnIndex As Integer

        Try
            mRowIndex = Dgl2.CurrentCell.RowIndex
            mColumnIndex = Dgl2.CurrentCell.ColumnIndex

            If Dgl2.Item(mColumnIndex, mRowIndex).Value Is Nothing Then Dgl2.Item(mColumnIndex, mRowIndex).Value = ""

            Select Case Dgl2.CurrentCell.ColumnIndex
                'Case ColumnIndex
                '   <Executable Code>
            End Select

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub DGL1_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles Dgl2.EditingControlShowing
        If TypeOf e.Control Is ComboBox Then
            e.Control.Text = ""
        End If
    End Sub

    Private Sub DGL1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl2.KeyDown
        'If e.Control And e.KeyCode = Keys.D Then
        '    sender.CurrentRow.Selected = True
        'End If
        If e.Control Or e.Shift Or e.Alt Then Exit Sub

        Try
            Select Case sender.CurrentCell.ColumnIndex
                'Case <Dgl_Column>
                '    <Executable Code>
            End Select
        Catch Ex As NullReferenceException
        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try
    End Sub

    Private Sub DGL1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Dgl2.KeyPress
        Dim mRowIndex As Integer, mColumnIndex As Integer
        Try
            mRowIndex = Dgl2.CurrentCell.RowIndex
            mColumnIndex = Dgl2.CurrentCell.ColumnIndex

            Select Case sender.CurrentCell.ColumnIndex
                'Case <ColumnIndex>
                '<Executable Code>
            End Select
        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try
    End Sub



    Private Function Data_Validation() As Boolean
        Dim I As Integer = 0
        Try
            Call GetModuleList()

            If AgCL.AgIsBlankGrid(Dgl2, Col1Module) Then Exit Function

            Data_Validation = True
        Catch ex As Exception
            MsgBox(ex.Message)
            Data_Validation = False
        End Try
    End Function

    Private Function GetModuleList() As String
        Dim I As Integer = 0
        Dim bStrModuleList$ = ""
        Try
            With Dgl2
                For I = 0 To .Rows.Count - 1
                    If .Item(Col1Select, I).Value Is Nothing Then .Item(Col1Select, I).Value = 0
                    If IsDBNull(.Item(Col1Select, I).Value) Then .Item(Col1Select, I).Value = 0
                    If .Item(Col1Module, I).Value Is Nothing Then .Item(Col1Module, I).Value = ""

                    If CBool(.Item(Col1Select, I).Value) And .Item(Col1Module, I).Value.ToString.Trim <> "" Then
                        If bStrModuleList.Trim = "" Then
                            bStrModuleList = "|" + .Item(Col1Module, I).Value.ToString + "|"
                        Else
                            bStrModuleList = bStrModuleList + ", |" + .Item(Col1Module, I).Value.ToString + "|"
                        End If

                    End If
                Next
            End With
        Catch ex As Exception
            bStrModuleList = ""
        Finally
            GetModuleList = bStrModuleList
        End Try
    End Function

    Private Sub BtnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnOk.Click, BtnCancel.Click
        Try
            Select Case sender.name
                Case BtnOk.Name
                    mStrModuleList = GetModuleList()
                Case BtnCancel.Name
                    '<Executable Code>
            End Select
        Catch ex As Exception
            mStrModuleList = ""
        Finally
            Me.Dispose()
        End Try
    End Sub
End Class
