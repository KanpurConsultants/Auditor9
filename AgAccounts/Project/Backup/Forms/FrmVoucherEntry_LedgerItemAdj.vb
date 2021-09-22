Public Class FrmVoucherEntry_LedgerItemAdj
    Private Const GSNo As Byte = 0
    Private Const GItemName As Byte = 1
    Private Const GItemCode As Byte = 2
    Private Const GRemark As Byte = 3
    Private Const GQty As Byte = 4
    Private Const GUnit As Byte = 5
    Private Const GRate As Byte = 6
    Private Const GAmount As Byte = 7

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
    Private Sub FrmVoucherEntry_LedgerItemAdj_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim I As Int16
        LIEvent = New ClsEvents(Me)
        Me.BackColor = CMain.ClrPubBackColorForm
        Agl.GridDesign(FGMain)
        IniGrid()
        CMain.SetEnableDisable(Me, BtnOK.Enabled)
        FGMain.Rows.Clear()

        Try
            FGMain.Rows.Add(UBound(FrmVEMain.SVTMain.LIAdjVar))
        Catch Ex As Exception
        End Try

        For I = 0 To UBound(FrmVEMain.SVTMain.LIAdjVar)
            FGMain(GSNo, I).Value = Trim(I + 1)
            FGMain(GItemName, I).Value = FrmVEMain.SVTMain.LIAdjVar(I).StrItemName
            FGMain(GItemCode, I).Value = FrmVEMain.SVTMain.LIAdjVar(I).StrItemCode
            FGMain(GRemark, I).Value = FrmVEMain.SVTMain.LIAdjVar(I).StrRemark
            FGMain(GQty, I).Value = Format(FrmVEMain.SVTMain.LIAdjVar(I).DblQuantity, "0.000")
            FGMain(GUnit, I).Value = FrmVEMain.SVTMain.LIAdjVar(I).StrUnit
            FGMain(GAmount, I).Value = Format(FrmVEMain.SVTMain.LIAdjVar(I).DblAmount, "0.00")
        Next
        FCalculate()
    End Sub

    Sub IniList()
        Dim mQry$
        mQry = "Select IM.Code As SearchCode,IM.Description as Name,IM.ManualCode," & _
              "IM.Unit " & _
              "From Item IM " & _
              "Order By IM.Name "
        'mQry = "Select IM.Code As SearchCode,IM.Name,IM.ManualCode," & _
        '      "IM.SKU " & _
        '      "From Item IM " & _
        '      "Where IM.SiteList Like '%|" & AgL.PubSiteCode & "|%' Order By IM.Name "

        FGMain.AgHelpDataSet(GItemName) = AgL.FillData(mQry, AgL.GCn)
    End Sub
    Private Sub IniGrid()
        FGMain.Height = PnlMain.Height
        FGMain.Width = PnlMain.Width
        FGMain.Top = PnlMain.Top
        FGMain.Left = PnlMain.Left
        Controls.Add(FGMain)
        FGMain.Visible = True
        FGMain.BringToFront()
        AgCl.AddAgTextColumn(FGMain, "SNo", 40, 5, "S.No.", True, True, False)
        AgCl.AddAgTextColumn(FGMain, "ItemName", 243, 10, "Item Name", True, True, False)
        AgCl.AddAgTextColumn(FGMain, "ItemCode", 0, 0, "ItemCode", False, True, False)
        AgCl.AddAgTextColumn(FGMain, "Remark", 220, 100, "Remark", True, False, False)
        AgCl.AddAgTextColumn(FGMain, "Quantity", 100, 15, "Quantity", True, False, True)
        AgCl.AddAgTextColumn(FGMain, "Unit", 60, 10, "Unit", True, True, False)
        AgCl.AddAgTextColumn(FGMain, "Rate", 90, 15, "Rate", True, True, True)
        AgCl.AddAgTextColumn(FGMain, "Amount", 100, 15, "Amount", True, False, True)

        FGMain.ColumnHeadersDefaultCellStyle.Font = New Font("Arial", 9, FontStyle.Regular)
        FGMain.DefaultCellStyle.Font = New Font("Arial", 9, FontStyle.Regular)
        FGMain.Anchor = (AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Bottom)
        AgL.FSetSNo(FGMain, GSNo)
        FGMain.TabIndex = PnlMain.TabIndex
    End Sub
    Private Sub FGMain_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles FGMain.CellEndEdit
        Select Case e.ColumnIndex
            Case GQty, GAmount
                FCalculate()
        End Select
    End Sub
    Private Sub FGMain_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FGMain.KeyDown
        If BtnOK.Enabled Then
            If e.Control And e.KeyCode = Keys.D Then
                FGMain.CurrentRow.Selected = True
            End If
        End If

        If e.Control Or e.Shift Or e.Alt Then Exit Sub
        Try
            Select Case FGMain.CurrentCell.ColumnIndex
            End Select
        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try
    End Sub
    Private Sub FGMain_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles FGMain.RowsAdded
        FGMain(GSNo, FGMain.Rows.Count - 1).Value = Trim(FGMain.Rows.Count)
    End Sub
    Private Sub FGMain_RowsRemoved(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs) Handles FGMain.RowsRemoved
        Agl.FSetSNo(FGMain, GSNo)
        FCalculate()
    End Sub
    Private Sub FGMain_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles FGMain.EditingControlShowing
        If TypeOf e.Control Is AgControls.AgTextBox Then
            RemoveHandler DirectCast(e.Control, AgControls.AgTextBox).KeyPress, AddressOf FGrdNumPress
            AddHandler DirectCast(e.Control, AgControls.AgTextBox).KeyPress, AddressOf FGrdNumPress
        End If
    End Sub
    Private Sub FGrdNumPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Select Case FGMain.CurrentCell.ColumnIndex
            Case GQty
                CMain.NumPress(sender, e, 10, 3, False)
            Case GAmount
                CMain.NumPress(sender, e, 10, 2, False)
        End Select
    End Sub

    Private Sub BtnOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles BtnOK.Click, BtnCancel.Click
        Dim I As Integer

        Select Case sender.name
            Case BtnOK.Name
                FCalculate()
                If Not Val(TxtPendingAmt.Text) >= 0 Then MsgBox("You Can Not Adjust Amount More Than Amount To Be Adjusted.") : Exit Sub
                If Not FCkhGrid() Then Exit Sub
                ReDim FrmVEMain.SVTMain.LIAdjVar(FGMain.Rows.Count - 1)
                For I = 0 To FGMain.Rows.Count - 1
                    If Trim(FGMain(GItemName, I).Value) <> "" Then
                        FrmVEMain.SVTMain.LIAdjVar(I).StrItemName = FGMain(GItemName, I).Value
                        FrmVEMain.SVTMain.LIAdjVar(I).StrItemCode = FGMain(GItemCode, I).Value
                        FrmVEMain.SVTMain.LIAdjVar(I).StrRemark = FGMain(GRemark, I).Value
                        FrmVEMain.SVTMain.LIAdjVar(I).DblQuantity = Val(FGMain(GQty, I).Value)
                        FrmVEMain.SVTMain.LIAdjVar(I).StrUnit = FGMain(GUnit, I).Value
                        FrmVEMain.SVTMain.LIAdjVar(I).DblAmount = Val(FGMain(GAmount, I).Value)
                    End If
                Next
                FrmVEMain.FUpdateRowStructure(FrmVEMain.SVTMain, IntRowMain)
                Me.Close()
            Case BtnCancel.Name
                Me.Close()
        End Select
    End Sub
    Private Sub FrmVoucherEntry_LedgerItemAdj_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        Agl.FPaintForm(Me, e, 0)
    End Sub
    Private Sub FCalculate()
        Dim I As Int16
        Dim DblTotal As Double = 0

        For I = 0 To FGMain.Rows.Count - 1
            DblTotal = DblTotal + Val(FGMain(GAmount, I).Value)
            If Val(FGMain(GQty, I).Value) > 0 Then
                FGMain(GRate, I).Value = Format(Val(FGMain(GAmount, I).Value) / Val(FGMain(GQty, I).Value), "0.000")
            End If
        Next

        TxtPendingAmt.Text = Format(Val(TxtAmountToBeAdj.Text) - DblTotal, "0.00")
        TxtTotalAdjustment.Text = Format(DblTotal, "0.00")
    End Sub
    Private Function FCkhGrid() As Boolean
        Dim I As Integer, J As Integer
        Dim BlnRtn As Boolean, BlnItemExists As Boolean

        BlnItemExists = False
        BlnRtn = True
        For I = 0 To FGMain.Rows.Count - 1
            If Trim(FGMain(GItemName, I).Value) <> "" Then
                BlnItemExists = True
                If Not Val(FGMain(GAmount, I).Value) > 0 Then
                    MsgBox("Please Define in Enviro" & " Amount.")
                    FGMain(GAmount, I).Selected = True
                    BlnRtn = False
                    FGMain.Focus()
                    Exit For
                End If

                For J = I + 1 To FGMain.Rows.Count - 1
                    If Trim(UCase(FGMain(GItemName, I).Value)) = Trim(UCase(FGMain(GItemName, J).Value)) Then
                        MsgBox(ClsMain.MsgDuplicate & " Item Name.")
                        FGMain(GItemName, J).Selected = True
                        BlnRtn = False
                        FGMain.Focus()
                        Exit For
                    End If
                Next
            End If
            If Not BlnRtn Then
                Exit For
            End If
        Next

        If Not BlnItemExists Then
            MsgBox("Please Define in Enviro" & " Item Name.")
            BlnRtn = False
            FGMain.Focus()
        End If
        FCkhGrid = BlnRtn
    End Function
End Class