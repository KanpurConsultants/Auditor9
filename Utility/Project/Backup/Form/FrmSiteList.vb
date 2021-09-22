Public Class FrmSiteList
    Private DTMaster As New DataTable()
    Public BMBMaster As BindingManagerBase
    Private KEAMainKeyCode As System.Windows.Forms.KeyEventArgs
    Dim mQry As String = "", mSearchCode As String = ""

    Private Const Col1Select As Byte = 0
    Private Const Col1Site_Code As Byte = 1
    Private Const Col1Site_Name As Byte = 2


    Public mSiteList As String = ""

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Public Property SiteListStr() As String
        Get
            SiteListStr = mSiteList
        End Get
        Set(ByVal value As String)
            mSiteList = value
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
        With Dgl1
            .Columns(Col1Select).Width = 50
            .Columns(Col1Site_Code).Width = 85
            .Columns(Col1Site_Name).Width = 390
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
            AgL.GridDesign(Dgl1)
            MoveRec()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub MoveRec()
        Dim DsTemp As DataSet = Nothing
        Dim mCondStr$ = "", mCondStr1$ = ""

        Try
            Dgl1.DataSource = Nothing

            If mSiteList.Trim <> "" Then
                mCondStr = " Where IsNull(S.Active,0) <> 0 And S.Code In (" & Replace(mSiteList, "|", "'") & ") "
                mCondStr1 = " Where IsNull(S.Active,0) <> 0 And S.Code Not In (" & Replace(mSiteList, "|", "'") & ") "
            Else
                mCondStr = " Where 1 = 2 "
            End If

            mQry = "Select Convert(BIT,1) AS [Select], Code As [Site/Branch Code], Name As [Site/Branch Name] " & _
                    " From SiteMast S " & mCondStr
            mQry = mQry & " UNION ALL " & _
                    " Select Convert(BIT,0) AS [Select], Code As [Site/Branch Code], Name As [Site/Branch Name] " & _
                    " From SiteMast S " & mCondStr1

            DsTemp = AgL.FillData(mQry, AgL.GCn)
            Dgl1.DataSource = DsTemp.Tables(0)
            Call IniGrid()

            If DsTemp.Tables(0).Rows.Count = 0 Then MsgBox("Site/Branch Details Is Not Available!...")
        Catch ex As Exception
            Dgl1.DataSource = Nothing
            MsgBox(ex.Message)
        Finally
            DsTemp = Nothing
        End Try
    End Sub

  

    Private Sub DGL1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        Dim mRowIndex As Integer, mColumnIndex As Integer

        Try
            mRowIndex = Dgl1.CurrentCell.RowIndex
            mColumnIndex = Dgl1.CurrentCell.ColumnIndex

            Select Case Dgl1.CurrentCell.ColumnIndex
                Case Col1Select
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
                Case Col1Select
                    '<Executable Code>
            End Select

        Catch ex As Exception

        End Try
    End Sub

    Private Sub DGL1_EditingControl_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Dgl1.EditingControl_Validating
        Dim mRowIndex As Integer, mColumnIndex As Integer

        Try
            mRowIndex = Dgl1.CurrentCell.RowIndex
            mColumnIndex = Dgl1.CurrentCell.ColumnIndex

            If Dgl1.Item(mColumnIndex, mRowIndex).Value Is Nothing Then Dgl1.Item(mColumnIndex, mRowIndex).Value = ""

            Select Case Dgl1.CurrentCell.ColumnIndex
                'Case ColumnIndex
                '   <Executable Code>
            End Select

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub DGL1_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles Dgl1.EditingControlShowing
        If TypeOf e.Control Is ComboBox Then
            e.Control.Text = ""
        End If
    End Sub

    Private Sub DGL1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.KeyDown
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

    Private Sub DGL1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Dgl1.KeyPress
        Dim mRowIndex As Integer, mColumnIndex As Integer
        Try
            mRowIndex = Dgl1.CurrentCell.RowIndex
            mColumnIndex = Dgl1.CurrentCell.ColumnIndex

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
            Call GetSiteList()

            If AgCL.AgIsBlankGrid(Dgl1, Col1Site_Code) Then Exit Function

            Data_Validation = True
        Catch ex As Exception
            MsgBox(ex.Message)
            Data_Validation = False
        End Try
    End Function

    Private Function GetSiteList() As String
        Dim I As Integer = 0
        Dim mTmSiteList$ = ""
        Try
            With Dgl1
                For I = 0 To .Rows.Count - 1
                    If .Item(Col1Select, I).Value Is Nothing Then .Item(Col1Select, I).Value = 0
                    If IsDBNull(.Item(Col1Select, I).Value) Then .Item(Col1Select, I).Value = 0
                    If .Item(Col1Site_Code, I).Value Is Nothing Then .Item(Col1Site_Code, I).Value = ""

                    If CBool(.Item(Col1Select, I).Value) And .Item(Col1Site_Code, I).Value.ToString.Trim <> "" Then
                        If mTmSiteList.Trim = "" Then
                            mTmSiteList = "|" + .Item(Col1Site_Code, I).Value.ToString + "|"
                        Else
                            mTmSiteList = mTmSiteList + ", |" + .Item(Col1Site_Code, I).Value.ToString + "|"
                        End If

                    End If
                Next
            End With
        Catch ex As Exception
            mTmSiteList = ""
        Finally
            GetSiteList = mTmSiteList
        End Try
    End Function

    Private Sub BtnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnOk.Click, BtnCancel.Click
        Try
            Select Case sender.name
                Case BtnOk.Name
                    mSiteList = GetSiteList()
                Case BtnCancel.Name
                    '<Executable Code>
            End Select
        Catch ex As Exception
            mSiteList = ""
        Finally
            Me.Dispose()
        End Try
    End Sub
End Class
