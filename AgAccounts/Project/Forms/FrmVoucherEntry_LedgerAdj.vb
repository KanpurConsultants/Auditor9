Public Class FrmVoucherEntry_LedgerAdj
    Private Const GDocId As Byte = 0
    Private Const GV_No As Byte = 1
    Private Const GV_SNo As Byte = 2
    Private Const GV_Type As Byte = 3
    Private Const GV_Date As Byte = 4
    Private Const GNarration As Byte = 5
    Private Const GBillAmount As Byte = 6
    Private Const GAdjustedAmt As Byte = 7
    Private Const GAdjustment As Byte = 8

    Dim FrmVEMain As FrmVoucherEntry
    Dim IntRowMain As Integer
    Private WithEvents FGMain As New AgControls.AgDataGrid
    Private LIEvent As ClsEvents
    Sub New(ByVal FrmVEVar As FrmVoucherEntry, ByVal IntRowVar As Integer, ByVal StrAcName As String, ByVal DblAmountToBeAdj As Double, ByVal BlnMode As Boolean)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        FrmVEMain = FrmVEVar
        IntRowMain = IntRowVar
        TxtAmountToBeAdj.Text = Format(DblAmountToBeAdj, "0.00")
        LblDisplay.Text = StrAcName
        TxtPendingAmt.ReadOnly = True
        TxtTotalAdjustment.ReadOnly = True
        TxtAmountToBeAdj.ReadOnly = True
        BtnOK.Enabled = BlnMode
    End Sub
    Private Sub FrmVoucherEntry_LedgerAdj_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim I As Int16
        LIEvent = New ClsEvents(Me)
        Me.BackColor = CMain.ClrPubBackColorForm
        Agl.GridDesign(FGMain)
        IniGrid()
        CMain.SetEnableDisable(Me, BtnOK.Enabled)
        FGMain.Enabled = BtnOK.Enabled
        FGMain.Rows.Clear()

        Try
            FGMain.Rows.Add(UBound(FrmVEMain.SVTMain.LAdjVar))
            For I = 0 To UBound(FrmVEMain.SVTMain.LAdjVar)
                FGMain(GDocId, I).Value = FrmVEMain.SVTMain.LAdjVar(I).StrDocId
                FGMain(GV_No, I).Value = FrmVEMain.SVTMain.LAdjVar(I).StrV_No
                FGMain(GV_SNo, I).Value = FrmVEMain.SVTMain.LAdjVar(I).StrV_SNo
                FGMain(GV_Type, I).Value = FrmVEMain.SVTMain.LAdjVar(I).StrV_Type
                FGMain(GV_Date, I).Value = FrmVEMain.SVTMain.LAdjVar(I).StrV_Date
                FGMain(GNarration, I).Value = FrmVEMain.SVTMain.LAdjVar(I).StrNarration
                FGMain(GBillAmount, I).Value = Format(FrmVEMain.SVTMain.LAdjVar(I).DblBillAmt, "0.00")
                FGMain(GAdjustedAmt, I).Value = Format(FrmVEMain.SVTMain.LAdjVar(I).DblAdjusted, "0.00")
                FGMain(GAdjustment, I).Value = Format(FrmVEMain.SVTMain.LAdjVar(I).DblAdjustment, "0.00")
            Next
        Catch Ex As Exception
        End Try
        FCalculate()
    End Sub
    Private Sub FCalculate()
        Dim I As Int16
        Dim DblTotal As Double = 0

        For I = 0 To FGMain.Rows.Count - 1
            DblTotal = DblTotal + Val(FGMain(GAdjustment, I).Value)
        Next

        TxtPendingAmt.Text = Format(Val(TxtAmountToBeAdj.Text) - DblTotal, "0.00")
        TxtTotalAdjustment.Text = Format(DblTotal, "0.00")
    End Sub
    Private Sub IniGrid()
        FGMain.Height = PnlMain.Height
        FGMain.Width = PnlMain.Width
        FGMain.Top = PnlMain.Top
        FGMain.Left = PnlMain.Left
        Controls.Add(FGMain)
        FGMain.Visible = True
        FGMain.BringToFront()
        AgCl.AddAgTextColumn(FGMain, "DocId", 0, 0, "DocId", False, True, False)
        AgCl.AddAgTextColumn(FGMain, "V_No", 100, 5, "Vr.No.", True, True, False)
        AgCl.AddAgTextColumn(FGMain, "V_SNo", 0, 0, "Vr.SNo.", False, True, False)
        AgCl.AddAgTextColumn(FGMain, "V_Type", 50, 10, "Type", True, True, False)
        AgCl.AddAgTextColumn(FGMain, "Date", 90, 10, "Date", True, True, False)
        AgCl.AddAgTextColumn(FGMain, "Narration", 310, 10, "Narration", True, True, False)
        AgCl.AddAgTextColumn(FGMain, "BillAmt", 100, 10, "Bill Amt.", True, True, True)
        AgCl.AddAgTextColumn(FGMain, "Adjusted", 100, 10, "Adjusted", True, True, True)
        AgCl.AddAgTextColumn(FGMain, "Adjustment", 100, 15, "Adjustment", True, False, True)

        FGMain.ColumnHeadersDefaultCellStyle.Font = New Font("Arial", 9, FontStyle.Regular)
        FGMain.DefaultCellStyle.Font = New Font("Arial", 9, FontStyle.Regular)

        FGMain.Anchor = (AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Bottom)
        FGMain.AllowUserToAddRows = False
        Agl.FSetSNo(FGMain, GV_No)
        FGMain.TabIndex = 0
    End Sub
    Private Sub FGMain_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles FGMain.EditingControlShowing
        If TypeOf e.Control Is AgControls.AgTextBox Then

            RemoveHandler DirectCast(e.Control, AgControls.AgTextBox).KeyPress, AddressOf _
                                FGrdNumPress
            AddHandler DirectCast(e.Control, AgControls.AgTextBox).KeyPress, AddressOf _
                                FGrdNumPress
        End If
    End Sub

    Private Sub FGrdNumPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Select Case FGMain.CurrentCell.ColumnIndex
            Case GAdjustment
                CMain.NumPress(sender, e, 10, 2, False)
        End Select
    End Sub
    Private Sub FGMain_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles FGMain.CellEndEdit
        FCalculate()
    End Sub
    Private Sub FGMain_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FGMain.KeyDown
        If e.Control Or e.Shift Or e.Alt Then Exit Sub
    End Sub
    Private Sub BtnOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles BtnOK.Click, BtnCancel.Click
        Dim I As Integer

        Select Case sender.name
            Case BtnOK.Name
                If AgL.RequiredField(TxtAmountToBeAdj, "Amount To Be Adjusted") Then Exit Sub
                If Val(TxtPendingAmt.Text) < 0 Then MsgBox("Pending Amount") : Exit Sub
                If Not FCkhGrid() Then Exit Sub

                ReDim FrmVEMain.SVTMain.LAdjVar(FGMain.Rows.Count)
                For I = 0 To FGMain.Rows.Count - 1
                    FrmVEMain.SVTMain.LAdjVar(I).StrDocId = FGMain(GDocId, I).Value
                    FrmVEMain.SVTMain.LAdjVar(I).StrV_No = FGMain(GV_No, I).Value
                    FrmVEMain.SVTMain.LAdjVar(I).StrV_SNo = FGMain(GV_SNo, I).Value
                    FrmVEMain.SVTMain.LAdjVar(I).StrV_Type = FGMain(GV_Type, I).Value
                    FrmVEMain.SVTMain.LAdjVar(I).StrV_Date = FGMain(GV_Date, I).Value
                    FrmVEMain.SVTMain.LAdjVar(I).StrNarration = FGMain(GNarration, I).Value
                    FrmVEMain.SVTMain.LAdjVar(I).DblBillAmt = Format(Val(FGMain(GBillAmount, I).Value), "0.00")
                    FrmVEMain.SVTMain.LAdjVar(I).DblAdjusted = Format(Val(FGMain(GAdjustedAmt, I).Value), "0.00")
                    FrmVEMain.SVTMain.LAdjVar(I).DblAdjustment = Format(Val(FGMain(GAdjustment, I).Value), "0.00")
                Next
                FrmVEMain.FUpdateRowStructure(FrmVEMain.SVTMain, IntRowMain)
                Me.Close()
            Case BtnCancel.Name
                Me.Close()
        End Select
    End Sub
    Private Function FCkhGrid() As Boolean
        Dim I As Integer
        Dim BlnRtn As Boolean

        BlnRtn = True
        For I = 0 To FGMain.Rows.Count - 1
            If (Val(FGMain(GAdjustedAmt, I).Value) + Val(FGMain(GAdjustment, I).Value)) > Val(FGMain(GBillAmount, I).Value) Then
                MsgBox(ClsMain.Msg_4)
                FGMain(GAdjustment, I).Selected = True
                FGMain.Focus()
                BlnRtn = False
                Exit For
            End If
            
            If Not BlnRtn Then
                Exit For
            End If
        Next

        FCkhGrid = BlnRtn
    End Function
    Private Sub FrmVoucherEntry_LedgerAdj_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        Agl.FPaintForm(Me, e, 0)
    End Sub

    Public Sub FTxtGotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        '======== Write Your Code Below =============
    End Sub
    Public Sub FTxtKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        '======== Write Your Code Below =============
    End Sub
    Public Sub FTxtKeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        '======== Write Your Code Below =============
    End Sub

End Class