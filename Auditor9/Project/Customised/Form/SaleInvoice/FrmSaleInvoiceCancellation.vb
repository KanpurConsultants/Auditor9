Imports System.Data.SQLite
Public Class FrmSaleInvoiceCancellation
    Dim mQry As String = ""
    Public mOkButtonPressed As Boolean

    Public Const ColSNo As String = "S.No."
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Public Const Col1Head As String = "Head"
    Public Const Col1Mandatory As String = ""
    Public Const Col1Value As String = "Value"
    Public Const Col1HeadOriginal As String = "Head Original"

    Public Const rowCancellationDate As Integer = 0
    Public Const rowCancellationRemark As Integer = 1
    Public Const rowCancelledBy As Integer = 2

    Public Const hcCancellationDate As String = "Cancel Date"
    Public Const hcCancellationRemark As String = "Remark"
    Public Const hcCancelledBy As String = "Cancelled By"

    Dim mSearchcode As String
    Public Property SearchCode() As String
        Get
            SearchCode = mSearchcode
        End Get
        Set(ByVal value As String)
            mSearchcode = value
        End Set
    End Property
    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub
    Public Sub IniGrid(SearchCode As String)
        Dim I As Integer

        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 35, 5, ColSNo, False, True, False)
            .AddAgTextColumn(Dgl1, Col1Head, 160, 255, Col1Head, True, True)
            .AddAgTextColumn(Dgl1, Col1Mandatory, 10, 20, Col1Mandatory, True, True)
            .AddAgTextColumn(Dgl1, Col1Value, 300, 255, Col1Value, True, False)
            .AddAgTextColumn(Dgl1, Col1HeadOriginal, 0, 255, Col1HeadOriginal, False, False)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.Columns(Col1Mandatory).DefaultCellStyle.Font = New System.Drawing.Font("Wingdings 2", 5.25, FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Dgl1.ColumnHeadersHeight = 35
        Dgl1.AgSkipReadOnlyColumns = True
        Dgl1.AllowUserToAddRows = False
        Dgl1.RowHeadersVisible = False
        Dgl1.Name = "Dgl1"


        Dgl1.Rows.Add(3)
        Dgl1.Item(Col1Head, rowCancellationDate).Value = hcCancellationDate
        Dgl1.Item(Col1Head, rowCancellationRemark).Value = hcCancellationRemark
        Dgl1.Item(Col1Head, rowCancelledBy).Value = hcCancelledBy

        For I = 0 To Dgl1.Rows.Count - 1
            Dgl1(Col1HeadOriginal, I).Value = Dgl1(Col1Head, I).Value
        Next

        FMoveRec(SearchCode)
    End Sub
    Sub KeyPress_Form(ByVal Sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        If e.KeyChar = Chr(Keys.Escape) Then
            Me.Close()
        End If
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            AgL.GridDesign(Dgl1)
            Me.Top = 300
            Me.Left = 300
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub Dgl1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        Try
            'If AgL.StrCmp(EntryMode, "Browse") Then Exit Sub
            If Dgl1.CurrentCell Is Nothing Then Exit Sub
            If Dgl1.CurrentCell.ColumnIndex <> Dgl1.Columns(Col1Value).Index Then Exit Sub
            Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) = Nothing
            CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Text_Value
            CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).MaxInputLength = 0
            Select Case Dgl1.CurrentCell.RowIndex
                Case rowCancellationRemark
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).MaxInputLength = 50
                Case rowCancellationDate
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Date_Value
                Case rowCancelledBy
                    Dgl1.Item(Col1Value, rowCancelledBy).ReadOnly = True
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
            If bColumnIndex <> Dgl1.Columns(Col1Value).Index Then Exit Sub

            Select Case Dgl1.CurrentCell.RowIndex
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Public Function DataValidation() As Boolean
        DataValidation = False

        For I As Integer = 0 To Dgl1.Rows.Count - 1
            If Dgl1.Item(Col1Mandatory, I).Value <> "" Then
                If Dgl1(Col1Value, I).Value = "" Then
                    MsgBox(Dgl1.Item(Col1Head, I).Value & " can not be blank...!", MsgBoxStyle.Information)
                    Exit Function
                End If
            End If
        Next

        DataValidation = True
    End Function
    Private Sub BtnChargeDuw_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnOk.Click
        Select Case sender.Name
            Case BtnOk.Name
                If mSearchcode <> "" Then
                    If DataValidation() = False Then Exit Sub
                    FSave(mSearchcode)
                End If
                Me.Close()
                Exit Sub
        End Select
    End Sub
    Public Sub FMoveRec(ByVal SearchCode As String)
        Dim DtTemp As DataTable = Nothing
        Dim I As Integer = 0

        If SearchCode = "" Then Exit Sub
        mSearchcode = SearchCode

        Try
            mQry = "SELECT H.CancellationDate, H.CancellationRemark, H.CancelledBy
                    FROM SaleInvoice H                      
                    WHERE H.DocId = '" & SearchCode & "' "
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

            With DtTemp
                If DtTemp.Rows.Count > 0 Then
                    Dgl1.Item(Col1Value, rowCancellationDate).Value = AgL.RetDate(AgL.XNull(.Rows(0)("CancellationDate")))
                    Dgl1.Item(Col1Value, rowCancellationRemark).Value = AgL.XNull(.Rows(0)("CancellationRemark"))
                    If AgL.XNull(.Rows(0)("CancelledBy")) <> "" Then
                        Dgl1.Item(Col1Value, rowCancelledBy).Value = AgL.XNull(.Rows(0)("CancelledBy"))
                    Else
                        Dgl1.Item(Col1Value, rowCancelledBy).Value = AgL.PubUserName
                    End If
                End If
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Public Sub FSave(ByVal SearchCode As String)
        Dim mTrans As String = ""
        Try
            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = "Begin"

            mQry = "UPDATE SaleInvoice 
                    Set CancellationDate = " & AgL.Chk_Date(Dgl1.Item(Col1Value, rowCancellationDate).Value) & ", 
                    CancellationRemark = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowCancellationRemark).Value) & ", 
                    CancelledBy = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowCancelledBy).Value) & "
                    Where DocId = '" & SearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

            mQry = "Delete From Ledger Where DocId = '" & mSearchcode & "'"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

            mQry = "Delete From Stock Where DocId = '" & mSearchcode & "'"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

            AgL.ETrans.Commit()
            mTrans = "Commit"
        Catch ex As Exception
            AgL.ETrans.Rollback()
            MsgBox(ex.Message)
        End Try
    End Sub
End Class