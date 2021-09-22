Public Class FrmVoucherEntry_LedgerAdjNew

    Public WithEvents FGMain As New AgControls.AgDataGrid

    Public Const GAdjustmentType As Byte = 0
    Public Const GFilter As Byte = 1
    Public Const GDocId As Byte = 2
    Public Const GV_No As Byte = 3
    Public Const GV_SNo As Byte = 4
    Public Const GV_Type As Byte = 5
    Public Const GV_Date As Byte = 6
    Public Const GNarration As Byte = 7
    Public Const GBillAmount As Byte = 8
    Public Const GAdjustedAmt As Byte = 9
    Public Const GBalanceAmt As Byte = 10
    Public Const GBalanceAmtDrCr As Byte = 11
    Public Const GAdjustment As Byte = 12
    Public Const GAdjustmentDrCr As Byte = 13
    Public Const GFilter_FromDate As Byte = 14
    Public Const GFilter_ToDate As Byte = 15
    Public Const GFilter_BillNo As Byte = 16
    Public Const GFilter_Party As Byte = 17


    Dim FrmVEMain As FrmVoucherEntry
    Dim IntRowMain As Integer
    Private LIEvent As ClsEvents

    Dim mSubCode$ = ""

    Dim mQry$ = ""
    Dim mSearchCode$ = ""

    Private Const AdjustmentType_Adjustment As String = "Adjustment"
    Private Const AdjustmentType_Reference As String = "Reference"

    Sub New(ByVal FrmVEVar As FrmVoucherEntry, ByVal IntRowVar As Integer, ByVal StrAcCode As String, ByVal StrAcName As String, ByVal DblAmountToBeAdj As Double, ByVal SearchCode As String, ByVal BlnMode As Boolean)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        FrmVEMain = FrmVEVar
        IntRowMain = IntRowVar
        TxtAmountToBeAdj.Text = Format(DblAmountToBeAdj, "0.00")
        mSubCode = StrAcCode
        mSearchCode = SearchCode
        LblDisplay.Text = StrAcName
        TxtPendingAmt.ReadOnly = True
        TxtTotalAdjustment.ReadOnly = True
        TxtAmountToBeAdj.ReadOnly = True
        BtnOK.Enabled = BlnMode
    End Sub

    Private Sub FrmVoucherEntry_LedgerAdj_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub FrmVoucherEntry_LedgerAdj_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim I As Int16
        LIEvent = New ClsEvents(Me)
        Me.BackColor = CMain.ClrPubBackColorForm
        AgL.GridDesign(FGMain)
        IniGrid()
        CMain.SetEnableDisable(Me, BtnOK.Enabled)
        FGMain.Enabled = BtnOK.Enabled
        FGMain.Rows.Clear()

        Try
            If FrmVEMain.SVTMain.LAdjVar Is Nothing And BtnOK.Enabled <> False Then FGMain.Item(GAdjustmentType, 0).Value = AdjustmentType_Adjustment

            FGMain.Rows.Add(UBound(FrmVEMain.SVTMain.LAdjVar))
            For I = 0 To UBound(FrmVEMain.SVTMain.LAdjVar)
                FGMain(GAdjustmentType, I).Value = FrmVEMain.SVTMain.LAdjVar(I).StrAdj_Type
                FGMain(GDocId, I).Value = FrmVEMain.SVTMain.LAdjVar(I).StrDocId
                FGMain(GV_No, I).Value = FrmVEMain.SVTMain.LAdjVar(I).StrV_No
                FGMain(GV_SNo, I).Value = FrmVEMain.SVTMain.LAdjVar(I).StrV_SNo
                FGMain(GV_Type, I).Value = FrmVEMain.SVTMain.LAdjVar(I).StrV_Type
                FGMain(GV_Date, I).Value = FrmVEMain.SVTMain.LAdjVar(I).StrV_Date
                FGMain(GNarration, I).Value = FrmVEMain.SVTMain.LAdjVar(I).StrNarration
                FGMain(GBillAmount, I).Value = Format(FrmVEMain.SVTMain.LAdjVar(I).DblBillAmt, "0.00")
                FGMain(GAdjustedAmt, I).Value = Format(FrmVEMain.SVTMain.LAdjVar(I).DblAdjusted, "0.00")
                FGMain(GBalanceAmt, I).Value = Format(FrmVEMain.SVTMain.LAdjVar(I).DblBalanceAmt, "0.00")
                FGMain(GBalanceAmtDrCr, I).Value = FrmVEMain.SVTMain.LAdjVar(I).StrBalanceAmtDrCr
                FGMain(GAdjustment, I).Value = Format(FrmVEMain.SVTMain.LAdjVar(I).DblAdjustment, "0.00")
                FGMain(GAdjustmentDrCr, I).Value = FrmVEMain.SVTMain.LAdjVar(I).StrAdjustmentDrCr
            Next
        Catch Ex As Exception
        End Try
        FCalculate()
    End Sub

    Private Sub FCalculate()
        Dim I As Int16
        Dim DblTotal As Double = 0

        For I = 0 To FGMain.Rows.Count - 1
            FGMain.Item(GBalanceAmt, I).Value = Val(FGMain.Item(GBillAmount, I).Value) - Val(FGMain.Item(GAdjustedAmt, I).Value)
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
        AgCl.AddAgTextColumn(FGMain, "Adjustment Type", 100, 100, "Adjustment Type", True, False, False)
        AgCl.AddAgButtonColumn(FGMain, "Filter", 35, "Filter")
        AgCl.AddAgTextColumn(FGMain, "DocId", 0, 0, "DocId", False, True, False)
        AgCl.AddAgTextColumn(FGMain, "V_No", 100, 5, "Vr.No.", True, True, False)
        AgCl.AddAgTextColumn(FGMain, "V_SNo", 0, 0, "Vr.SNo.", False, True, False)
        AgCl.AddAgTextColumn(FGMain, "V_Type", 50, 10, "Type", True, True, False)
        AgCl.AddAgTextColumn(FGMain, "Date", 90, 10, "Date", True, True, False)
        AgCl.AddAgTextColumn(FGMain, "Narration", 170, 10, "Narration", True, True, False)
        AgCl.AddAgTextColumn(FGMain, "BillAmt", 100, 10, "Bill Amt.", False, True, True)
        AgCl.AddAgTextColumn(FGMain, "Adjusted", 100, 10, "Adjusted", False, True, True)
        AgCl.AddAgTextColumn(FGMain, "Bal.Amt", 100, 15, "Bal.Amt", True, True, True)
        AgCl.AddAgTextColumn(FGMain, "BalAmtDrCr", 25, 0, " ", True, True, False)
        AgCl.AddAgTextColumn(FGMain, "Adjustment", 100, 15, "Adjustment", True, False, True)
        AgCl.AddAgTextColumn(FGMain, "AdjustmentDrCr", 25, 0, " ", True, True, False)
        AgCl.AddAgTextColumn(FGMain, "Filter_FromDate", 25, 0, "From Date", False, True, False)
        AgCl.AddAgTextColumn(FGMain, "Filter_ToDate", 25, 0, "To Date", False, True, False)
        AgCl.AddAgTextColumn(FGMain, "Filter_BillNo", 25, 0, "Bill No", False, True, False)
        AgCl.AddAgTextColumn(FGMain, "Filter_Party", 25, 0, "Party", False, True, False)


        FGMain.ColumnHeadersDefaultCellStyle.Font = New Font("Arial", 9, FontStyle.Regular)
        FGMain.DefaultCellStyle.Font = New Font("Arial", 9, FontStyle.Regular)

        FGMain.Anchor = (AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Bottom)
        AgL.FSetSNo(FGMain, GV_No)
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
        Try
            Select Case FGMain.CurrentCell.ColumnIndex
                Case GV_No
                    If FGMain.AgHelpDataSet(GV_No) Is Nothing Then
                        If Not CMain.FGrdDisableKeys(e) Then Exit Sub
                        'If AgL.StrCmp(FGMain.Item(GAdjustmentType, FGMain.CurrentCell.RowIndex).Value, AdjustmentType_Reference) Then
                        '    FHelp_BillNoForReference(FGMain.CurrentCell.RowIndex)
                        'Else
                        '    FHelp_BillNoForAdjustment()
                        'End If
                        FHelp_BillNoForReference(FGMain.CurrentCell.RowIndex)
                    End If

                Case GAdjustmentType
                    If e.KeyCode = Keys.Enter Then
                        If FGMain.Item(GAdjustmentType, FGMain.CurrentCell.RowIndex).Value <> "" Then
                            If AgL.StrCmp(FGMain.Item(GAdjustmentType, FGMain.CurrentCell.RowIndex).Value, AdjustmentType_Reference) Then
                            Else
                            End If
                        End If
                    End If

                Case GAdjustmentDrCr
                    If e.KeyCode = Keys.D Then FGMain(GAdjustmentDrCr, FGMain.CurrentCell.RowIndex).Value = "Dr"
                    If e.KeyCode = Keys.C Then FGMain(GAdjustmentDrCr, FGMain.CurrentCell.RowIndex).Value = "Cr"
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

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

                ReDim FrmVEMain.SVTMain.LAdjVar(FGMain.Rows.Count - 1)
                For I = 0 To FGMain.Rows.Count - 1
                    If FGMain(GV_No, I).Value <> "" Then
                        FrmVEMain.SVTMain.LAdjVar(I).StrAdj_Type = FGMain(GAdjustmentType, I).Value
                        FrmVEMain.SVTMain.LAdjVar(I).StrDocId = FGMain(GDocId, I).Value
                        FrmVEMain.SVTMain.LAdjVar(I).StrV_No = FGMain(GV_No, I).Value
                        FrmVEMain.SVTMain.LAdjVar(I).StrV_SNo = FGMain(GV_SNo, I).Value
                        FrmVEMain.SVTMain.LAdjVar(I).StrV_Type = FGMain(GV_Type, I).Value
                        FrmVEMain.SVTMain.LAdjVar(I).StrV_Date = FGMain(GV_Date, I).Value
                        FrmVEMain.SVTMain.LAdjVar(I).StrNarration = FGMain(GNarration, I).Value
                        FrmVEMain.SVTMain.LAdjVar(I).DblBillAmt = Format(Val(FGMain(GBillAmount, I).Value), "0.00")
                        FrmVEMain.SVTMain.LAdjVar(I).DblAdjusted = Format(Val(FGMain(GAdjustedAmt, I).Value), "0.00")
                        FrmVEMain.SVTMain.LAdjVar(I).DblBalanceAmt = Format(Val(FGMain(GBalanceAmt, I).Value), "0.00")
                        FrmVEMain.SVTMain.LAdjVar(I).StrBalanceAmtDrCr = FGMain(GBalanceAmtDrCr, I).Value
                        FrmVEMain.SVTMain.LAdjVar(I).DblAdjustment = Format(Val(FGMain(GAdjustment, I).Value), "0.00")
                        FrmVEMain.SVTMain.LAdjVar(I).StrAdjustmentDrCr = FGMain(GAdjustmentDrCr, I).Value
                    End If
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
        AgL.FPaintForm(Me, e, 0)
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

    Private Sub FGMain_EditingControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FGMain.EditingControl_KeyDown
        Try
            Select Case FGMain.CurrentCell.ColumnIndex
                Case GAdjustmentType
                    If FGMain.AgHelpDataSet(GAdjustmentType) Is Nothing Then
                        If e.KeyCode <> Keys.Enter Then
                            mQry = " Select '" & AdjustmentType_Adjustment & "' As Code, '" & AdjustmentType_Adjustment & "' As Name " & _
                                    " UNION ALL " & _
                                    " Select '" & AdjustmentType_Reference & "' As Code, '" & AdjustmentType_Reference & "' As Name "
                            FGMain.AgHelpDataSet(GAdjustmentType) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FGMain_CellContentClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles FGMain.CellContentClick
        Dim FrmObj As Form

        If e.RowIndex < 0 Then Exit Sub
        Select Case e.ColumnIndex
            Case GFilter
                FrmObj = New FrmVoucherEntry_ReferenceFilter(Me, e.RowIndex)
                FrmObj.ShowDialog()
                FrmObj.Dispose()
                FrmObj = Nothing
        End Select
    End Sub

    'Private Sub FHelp_BillNoForAdjustment()
    '    Dim DTMain As New DataTable
    '    Dim FRH As DMHelpGrid.FrmHelpGrid
    '    Dim StrSendText As String = ""
    '    Dim StrCondition As String
    '    Dim StrFieldContra As String = ""
    '    Dim StrCurrentDocId As String = ""
    '    Dim StrBalanceDrCrField As String = ""

    '    'StrSendText = CMain.FSendText(FGMain, Chr(e.KeyCode))

    '    StrCurrentDocId = mSearchCode
    '    If Val(FrmVEMain.FGMain(FrmVoucherEntry.GDebit, IntRowMain).Value) > 0 Then
    '        StrFieldContra = " LG.AmtCr "
    '        StrBalanceDrCrField = "Case When Max(BillAmt) - IsNull(Sum(Adjusted),0) < 0 Then 'Dr' Else 'Cr' End BalanceDrCr "
    '    ElseIf Val(FrmVEMain.FGMain(FrmVoucherEntry.GCredit, IntRowMain).Value) > 0 Then
    '        StrFieldContra = " LG.AmtDr "
    '        StrBalanceDrCrField = "Case When Max(BillAmt) - IsNull(Sum(Adjusted),0) < 0 Then 'Cr' Else 'Dr' End BalanceDrCr "
    '    End If
    '    If StrFieldContra = "" Then Exit Sub
    '    StrCondition = "Where LG.SubCode='" & FrmVEMain.FGMain(FrmVoucherEntry.GAcCode, IntRowMain).Value & "' "
    '    If Not AgL.PubIsHo Then StrCondition += "And LG.Site_Code='" & AgL.PubSiteCode & "' "

    '    AgL.ADMain = New SqlClient.SqlDataAdapter("Select DocId, V_SNo, Max(RecId) As RecId, Max(V_Type) As V_Type, " & _
    '                        " Max(V_Date) As V_Date, Max(Narration) As Narration, " & _
    '                        " Max(BillAmt) As BillAmt, Max(DueDate) As DueDate, " & _
    '                        " Max(BillAmt) - IsNull(Sum(Adjusted),0) As PendingAmt , " & _
    '                        " " & StrBalanceDrCrField & ", " & _
    '                        " Sum(Adjusted) As Adjusted, Sum(Adjustment) As Adjustment " & _
    '                        " From ( " & _
    '                        "       Select  LG.DocId,LG.V_SNo,LG.RecId, " & _
    '                        "       LG.V_Type, LG.V_Date, Dateadd(Day,IsNull(LG.CreditDays,0), LG.V_Date) As DueDate, LG.Narration, " & _
    '                        "       IsNull(" & StrFieldContra & ",0) As BillAmt,0 As Adjusted,0 As Adjustment " & _
    '                        "       From Ledger LG " & StrCondition & _
    '                        "       And IsNull(" & StrFieldContra & ",0)>0 " & _
    '                        " Union All " & _
    '                        "       Select	LA.Adj_DocId As DocId,LA.Adj_V_SNo As V_SNo,Null As RecId,Null As V_Type,Null As V_Date, " & _
    '                        "       Null As Narration,0 As BillAmt, Null As DueDate, Abs(LA.Amount) As Adjusted,0 As Adjustment	 " & _
    '                        "       From LedgerAdj LA " & _
    '                        "       Left Join Ledger LG On LA.Adj_DocId=LG.DocId And LA.Adj_V_SNo=LG.V_SNo " & StrCondition & _
    '                        "       And LA.Vr_DocId<>'" & StrCurrentDocId & "' " & _
    '                        " Union All " & _
    '                        "       Select	LA.Adj_DocId As DocId,LA.Adj_V_SNo As V_SNo,Null As RecId,Null As V_Type,Null As V_Date, " & _
    '                        "       Null As Narration, 0 As BillAmt, Null As DueDate,0 As Adjusted, Abs(LA.Amount) As Adjustment	" & _
    '                        "       From LedgerAdj LA " & _
    '                        "       Left Join Ledger LG On LA.Adj_DocId=LG.DocId And LA.Adj_V_SNo=LG.V_SNo " & StrCondition & _
    '                        "       And LA.Vr_DocId='" & StrCurrentDocId & "' " & _
    '                        " ) As Tmp " & _
    '                        " Group By DocId, V_SNo " & _
    '                        " Having (IsNull(Max(BillAmt),0)-IsNull(Sum(Adjusted),0))>0" & _
    '                        " Order By Max(V_Date),Max(RecId) ", AgL.GCn)

    '    AgL.ADMain.Fill(DTMain)
    '    FRH = New DMHelpGrid.FrmHelpGrid(New DataView(DTMain), StrSendText, 300, 640)
    '    FRH.FFormatColumn(0, , 0, , False)
    '    FRH.FFormatColumn(1, "V_SNo", 0, , False)
    '    FRH.FFormatColumn(2, "Bill No", 100, DataGridViewContentAlignment.MiddleLeft)
    '    FRH.FFormatColumn(3, "Nature", 60, DataGridViewContentAlignment.MiddleLeft)
    '    FRH.FFormatColumn(4, "Date", 100, DataGridViewContentAlignment.MiddleLeft)
    '    FRH.FFormatColumn(5, "Narration", 100, , False)
    '    FRH.FFormatColumn(6, "Bill Amt", 80, DataGridViewContentAlignment.MiddleRight)
    '    FRH.FFormatColumn(7, "Due Date", 100, DataGridViewContentAlignment.MiddleLeft)
    '    FRH.FFormatColumn(8, "Bal. Amt", 80, DataGridViewContentAlignment.MiddleRight)
    '    FRH.FFormatColumn(9, " ", 25, DataGridViewContentAlignment.MiddleRight)
    '    FRH.FFormatColumn(10, "Adjusted Amt", 100, DataGridViewContentAlignment.MiddleRight, False)
    '    FRH.FFormatColumn(11, "Adjustment", 80, DataGridViewContentAlignment.MiddleRight, False)
    '    FRH.StartPosition = FormStartPosition.CenterScreen
    '    FRH.ShowDialog()

    '    If FRH.BytBtnValue = 0 Then
    '        If Not FRH.DRReturn.Equals(Nothing) Then
    '            FGMain(GDocId, FGMain.CurrentCell.RowIndex).Value = FRH.DRReturn.Item(0)
    '            FGMain(GV_SNo, FGMain.CurrentCell.RowIndex).Value = AgL.XNull(FRH.DRReturn.Item(1))
    '            FGMain(GV_No, FGMain.CurrentCell.RowIndex).Value = AgL.XNull(FRH.DRReturn.Item(2))
    '            FGMain(GV_Type, FGMain.CurrentCell.RowIndex).Value = AgL.XNull(FRH.DRReturn.Item(3))
    '            FGMain(GV_Date, FGMain.CurrentCell.RowIndex).Value = AgL.XNull(FRH.DRReturn.Item(4))
    '            FGMain(GNarration, FGMain.CurrentCell.RowIndex).Value = AgL.XNull(FRH.DRReturn.Item(5))
    '            FGMain(GBillAmount, FGMain.CurrentCell.RowIndex).Value = AgL.XNull(FRH.DRReturn.Item(6))
    '            FGMain(GBalanceAmtDrCr, FGMain.CurrentCell.RowIndex).Value = AgL.XNull(FRH.DRReturn.Item(9))
    '            FGMain(GAdjustedAmt, FGMain.CurrentCell.RowIndex).Value = AgL.XNull(FRH.DRReturn.Item(10))


    '            If FGMain(GBalanceAmtDrCr, FGMain.CurrentCell.RowIndex).Value = "Cr" Then
    '                FGMain(GAdjustmentDrCr, FGMain.CurrentCell.RowIndex).Value = "Dr"
    '            Else
    '                FGMain(GAdjustmentDrCr, FGMain.CurrentCell.RowIndex).Value = "Cr"
    '            End If


    '            If Val(FGMain(GAdjustment, FGMain.CurrentCell.RowIndex).Value) = 0 Then
    '                If Val(TxtPendingAmt.Text) > Val(FGMain(GBillAmount, FGMain.CurrentCell.RowIndex).Value) - Val(FGMain(GAdjustedAmt, FGMain.CurrentCell.RowIndex).Value) Then
    '                    FGMain(GAdjustment, FGMain.CurrentCell.RowIndex).Value = Val(FGMain(GBillAmount, FGMain.CurrentCell.RowIndex).Value) - Val(FGMain(GAdjustedAmt, FGMain.CurrentCell.RowIndex).Value)
    '                Else
    '                    FGMain(GAdjustment, FGMain.CurrentCell.RowIndex).Value = Val(TxtPendingAmt.Text)
    '                End If
    '            End If
    '        End If
    '    End If
    '    FRH = Nothing
    '    FCalculate()
    'End Sub

    Private Sub FHelp_BillNoForReference(ByVal mRow As Integer)
        Dim DTMain As New DataTable
        Dim FRH As DMHelpGrid.FrmHelpGrid
        Dim StrSendText As String = ""
        Dim StrCondition As String
        Dim StrFieldContra As String = ""
        Dim StrCurrentDocId As String = ""
        Dim StrBalanceDrCrField As String = ""

        'StrSendText = CMain.FSendText(FGMain, Chr(e.KeyCode))

        StrCurrentDocId = mSearchCode
        StrBalanceDrCrField = "Case When Sum(BillAmt) + IsNull(Sum(Adjusted),0) < 0 Then 'Cr' Else 'Dr' End BalanceDrCr "
        StrCondition = "Where 1=1 "
        If Not AgL.PubIsHo Then StrCondition += "And LG.Site_Code='" & AgL.PubSiteCode & "' "


        If FGMain(GAdjustmentType, mRow).Value = AdjustmentType_Reference Then
            If FGMain(GFilter_FromDate, mRow).Value <> "" Then
                StrCondition += " And LG.V_Date >= '" & FGMain(GFilter_FromDate, mRow).Value & "' "
            End If

            If FGMain(GFilter_ToDate, mRow).Value <> "" Then
                StrCondition += " And LG.V_Date <= '" & FGMain(GFilter_ToDate, mRow).Value & "' "
            End If

            If FGMain(GFilter_Party, mRow).Value <> "" Then
                StrCondition += " And LG.SubCode = '" & FGMain(GFilter_Party, mRow).Tag & "' "
            End If

            If FGMain(GFilter_BillNo, mRow).Value <> "" Then
                StrCondition += " And LG.RecId Like '%" & FGMain(GFilter_BillNo, mRow).Value & "%' "
            End If
        Else
            StrCondition += "And LG.SubCode='" & FrmVEMain.FGMain(FrmVoucherEntry.GAcCode, IntRowMain).Value & "' "
        End If

        AgL.ADMain = New SqlClient.SqlDataAdapter("Select DocId, V_SNo, Max(RecId) As RecId, Max(V_Type) As V_Type, " & _
                            " Max(V_Date) As V_Date, Max(Narration) As Narration, Max(Name) As Name, " & _
                            " Abs(Sum(BillAmt)) As BillAmt, Max(DueDate) As DueDate, " & _
                            " Abs(Sum(BillAmt) + IsNull(Sum(Adjusted),0)) As PendingAmt , " & _
                            " " & StrBalanceDrCrField & ", " & _
                            " Sum(Adjusted) As Adjusted, Sum(Adjustment) As Adjustment " & _
                            " From ( " & _
                            "       Select  LG.DocId,LG.V_SNo,LG.RecId, " & _
                            "       LG.V_Type, LG.V_Date, " & _
                            "       Dateadd(Day,IsNull(LG.CreditDays,0), LG.V_Date) As DueDate, LG.Narration, Sg.Name," & _
                            "       IsNull(LG.AmtDr,0) - IsNull(LG.AmtCr,0) As BillAmt,0 As Adjusted,0 As Adjustment " & _
                            "       From Ledger LG " & _
                            "       LEFT JOIN SubGroup Sg On Lg.SubCode = Sg.SubCode " & StrCondition & _
                            "       And IsNull(LG.AmtDr,0) - IsNull(LG.AmtCr,0) <> 0 " & _
                            " Union All " & _
                            "       Select	LA.Adj_DocId As DocId,LA.Adj_V_SNo As V_SNo,Null As RecId, " & _
                            "       Null As V_Type,Null As V_Date, " & _
                            "       Null As DueDate, Null As Narration, Sg.Name As Name, " & _
                            "       0 As BillAmt, LA.Amount As Adjusted,0 As Adjustment	 " & _
                            "       From LedgerAdj LA " & _
                            "       Left Join Ledger LG On LA.Adj_DocId=LG.DocId And LA.Adj_V_SNo=LG.V_SNo " & _
                            "       LEFT JOIN SubGroup Sg On Lg.SubCode = Sg.SubCode " & StrCondition & _
                            "       And LA.Vr_DocId<>'" & StrCurrentDocId & "' " & _
                            " Union All " & _
                            "       Select	LA.Adj_DocId As DocId,LA.Adj_V_SNo As V_SNo,Null As RecId, " & _
                            "       Null As V_Type,Null As V_Date, " & _
                            "       Null As DueDate,Null As Narration, Sg.Name As Name, " & _
                            "       0 As BillAmt, 0 As Adjusted, LA.Amount As Adjustment	" & _
                            "       From LedgerAdj LA " & _
                            "       Left Join Ledger LG On LA.Adj_DocId=LG.DocId And LA.Adj_V_SNo=LG.V_SNo " & _
                            "       LEFT JOIN SubGroup Sg On Lg.SubCode = Sg.SubCode " & StrCondition & _
                            "       And LA.Vr_DocId='" & StrCurrentDocId & "' " & _
                            " ) As Tmp " & _
                            " Group By DocId, V_SNo " & _
                            " Having (IsNull(Max(BillAmt),0) + IsNull(Sum(Adjusted),0)) <> 0 " & _
                            " Order By Max(V_Date),Max(RecId) ", AgL.GCn)

        AgL.ADMain.Fill(DTMain)
        FRH = New DMHelpGrid.FrmHelpGrid(New DataView(DTMain), StrSendText, 500, 1000)
        FRH.FFormatColumn(0, , 0, , False)
        FRH.FFormatColumn(1, "V_SNo", 0, , False)
        FRH.FFormatColumn(2, "Bill No", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH.FFormatColumn(3, "Nature", 60, DataGridViewContentAlignment.MiddleLeft)
        FRH.FFormatColumn(4, "Date", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH.FFormatColumn(5, "Narration", 100, , False)
        FRH.FFormatColumn(6, "Name", 370, DataGridViewContentAlignment.MiddleLeft, True)
        FRH.FFormatColumn(7, "Bill Amt", 80, DataGridViewContentAlignment.MiddleRight)
        FRH.FFormatColumn(8, "Due Date", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH.FFormatColumn(9, "Bal. Amt", 80, DataGridViewContentAlignment.MiddleRight)
        FRH.FFormatColumn(10, " ", 25, DataGridViewContentAlignment.MiddleRight)
        FRH.FFormatColumn(11, "Adjusted Amt", 100, DataGridViewContentAlignment.MiddleRight, False)
        FRH.FFormatColumn(12, "Adjustment", 80, DataGridViewContentAlignment.MiddleRight, False)
        FRH.StartPosition = FormStartPosition.CenterScreen
        FRH.ShowDialog()

        If FRH.BytBtnValue = 0 Then
            If Not FRH.DRReturn.Equals(Nothing) Then
                FGMain(GDocId, FGMain.CurrentCell.RowIndex).Value = FRH.DRReturn.Item(0)
                FGMain(GV_SNo, FGMain.CurrentCell.RowIndex).Value = AgL.XNull(FRH.DRReturn.Item(1))
                FGMain(GV_No, FGMain.CurrentCell.RowIndex).Value = AgL.XNull(FRH.DRReturn.Item(2))
                FGMain(GV_Type, FGMain.CurrentCell.RowIndex).Value = AgL.XNull(FRH.DRReturn.Item(3))
                FGMain(GV_Date, FGMain.CurrentCell.RowIndex).Value = AgL.XNull(FRH.DRReturn.Item(4))
                FGMain(GNarration, FGMain.CurrentCell.RowIndex).Value = AgL.XNull(FRH.DRReturn.Item(5))
                FGMain(GBillAmount, FGMain.CurrentCell.RowIndex).Value = AgL.XNull(FRH.DRReturn.Item(7))
                FGMain(GBalanceAmtDrCr, FGMain.CurrentCell.RowIndex).Value = AgL.XNull(FRH.DRReturn.Item(10))
                FGMain(GAdjustedAmt, FGMain.CurrentCell.RowIndex).Value = AgL.XNull(FRH.DRReturn.Item(11))


                If FGMain(GBalanceAmtDrCr, FGMain.CurrentCell.RowIndex).Value = "Cr" Then
                    FGMain(GAdjustmentDrCr, FGMain.CurrentCell.RowIndex).Value = "Dr"
                Else
                    FGMain(GAdjustmentDrCr, FGMain.CurrentCell.RowIndex).Value = "Cr"
                End If


                If Val(FGMain(GAdjustment, FGMain.CurrentCell.RowIndex).Value) = 0 Then
                    If Val(TxtPendingAmt.Text) > Val(FGMain(GBillAmount, FGMain.CurrentCell.RowIndex).Value) - Val(FGMain(GAdjustedAmt, FGMain.CurrentCell.RowIndex).Value) Then
                        FGMain(GAdjustment, FGMain.CurrentCell.RowIndex).Value = Val(FGMain(GBillAmount, FGMain.CurrentCell.RowIndex).Value) - Val(FGMain(GAdjustedAmt, FGMain.CurrentCell.RowIndex).Value)
                    Else
                        FGMain(GAdjustment, FGMain.CurrentCell.RowIndex).Value = Val(TxtPendingAmt.Text)
                    End If
                End If
            End If
        End If
        FRH = Nothing
        FCalculate()
    End Sub
End Class