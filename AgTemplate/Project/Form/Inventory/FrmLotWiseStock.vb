Imports System.Data.SqlClient
Public Class FrmLotWiseStock
    Dim mQry As String = ""

    Public Const Col_SNo As String = "S.No"
    Public WithEvents DGL1 As New AgControls.AgDataGrid
    Public Const Col1LotNo As String = "Lot No."
    Public Const Col1Stock As String = "Stock"

    Dim mQty As Integer = 0
    Dim mItem$ = "", mItemName$ = "", mV_Date$ = ""

    Public Property Item() As String
        Get
            Item = mItem
        End Get
        Set(ByVal value As String)
            mItem = value
        End Set
    End Property

    Public Property V_Date() As String
        Get
            V_Date = mV_Date
        End Get
        Set(ByVal value As String)
            mV_Date = value
        End Set
    End Property

    Public Property ItemName() As String
        Get
            ItemName = mItemName
        End Get
        Set(ByVal value As String)
            mItemName = value
        End Set
    End Property

    Public Property Qty() As Integer
        Get
            Qty = mQty
        End Get
        Set(ByVal value As Integer)
            mQty = value
        End Set
    End Property

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        'AgL.FPaintForm(Me, e, 0)
    End Sub

    Private Sub IniGrid()
        ''==============================================================================
        ''================< Member Data Grid >====================================
        ''==============================================================================

        With AgCL
            .AddAgTextColumn(DGL1, Col_SNo, 65, 5, Col_SNo, True, True, False)
            .AddAgTextColumn(DGL1, Col1LotNo, 220, 50, Col1LotNo, True, False)
            .AddAgNumberColumn(DGL1, Col1Stock, 100, 8, 0, False, Col1Stock, True, False)
        End With
        AgL.AddAgDataGrid(DGL1, Pnl1)
        DGL1.ColumnHeadersHeight = 30
        DGL1.AllowUserToAddRows = False
        DGL1.EnableHeadersVisualStyles = False
        DGL1.Anchor = AnchorStyles.Top + AnchorStyles.Bottom + AnchorStyles.Left + AnchorStyles.Right

        DGL1.AgSkipReadOnlyColumns = False
    End Sub

    Private Sub KeyDown_Form(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If Me.ActiveControl IsNot Nothing Then
            If Not (TypeOf (Me.ActiveControl) Is AgControls.AgDataGrid) Then
                If e.KeyCode = Keys.Return Then SendKeys.Send("{Tab}")
            End If
            If e.KeyCode = Keys.Escape Then Me.Close()
        End If
    End Sub

    Sub KeyPress_Form(ByVal Sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        If e.KeyChar = Chr(Keys.Escape) Then Exit Sub
        If Me.ActiveControl Is Nothing Then Exit Sub
        AgL.CheckQuote(e)
    End Sub

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            AgL.GridDesign(DGL1)
            IniGrid()
            Ini_List()
            DispText()
            ProcFill()
            BtnClose.Anchor = AnchorStyles.Top + AnchorStyles.Bottom + AnchorStyles.Left + AnchorStyles.Right
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Sub Ini_List()
        Try

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub BlankText()
        DGL1.RowCount = 1 : DGL1.Rows.Clear()
    End Sub

    Private Sub DispText(Optional ByVal Enb As Boolean = False)
        'Coding To Enable/Disable Controls
        LblItemName.Text = mItemName
        LblCurrentStock.Text = mQty
        DGL1.ReadOnly = True
    End Sub

    Private Sub DGL1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DGL1.KeyDown
        If e.Control And e.KeyCode = Keys.D Then
            sender.CurrentRow.Selected = True
        End If
        If e.Control Or e.Shift Or e.Alt Then Exit Sub
    End Sub

    Private Sub DGL1_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles DGL1.RowsAdded
        sender(Col_SNo, sender.Rows.Count - 1).Value = Trim(sender.Rows.Count)
    End Sub

    Private Sub BtnChargeDuw_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnClose.Click
        Try
            Select Case sender.Name
                Case BtnClose.Name
                    Me.Close()
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ProcFill()
        Dim DtMaster As DataTable = Nothing
        Dim I As Integer = 0
        Try
            mQry = " SELECT S.LotNo, IfNull(Sum(S.Qty_Rec),0) - IfNull(Sum(S.Qty_Iss),0) As CurrentStock " & _
                    " FROM Stock S " & _
                    " WHERE S.Item = '" & mItem & "' " & _
                    " And S.V_Date  <= '" & mV_Date & "'  " & _
                    " GROUP By S.LotNo  "
            DtMaster = AgL.FillData(mQry, AgL.GCn).Tables(0)
            With DtMaster
                If .Rows.Count > 0 Then
                    DGL1.RowCount = 0 : DGL1.Rows.Clear()
                    For I = 0 To .Rows.Count - 1
                        DGL1.Rows.Add()
                        DGL1.Item(Col_SNo, I).Value = DGL1.Rows.Count
                        DGL1.Item(Col1LotNo, I).Value = AgL.XNull(.Rows(I)("LotNo"))
                        DGL1.Item(Col1Stock, I).Value = AgL.XNull(.Rows(I)("CurrentStock"))
                    Next I
                End If
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class