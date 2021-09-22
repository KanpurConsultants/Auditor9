Imports System.Data.SQLite
Public Class FrmPartyAcSettlementInvoiceAdjKirana
    Dim mQry As String = ""
    Public mOkButtonPressed As Boolean

    Public Const ColSNo As String = "S.No."
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Public Const Col1Head As String = "Head"
    Public Const Col1AdditionDeduction As String = "Add/Ded"
    Public Const Col1PostingAc As String = "PostingAc"
    Public Const Col1RateCalculationType As String = "Rate Type"
    Public Const Col1CalculateOn As String = "Calculate On"
    Public Const Col1Rate As String = "@"
    Public Const Col1Amount As String = "Amount"
    Public Const Col1Remark As String = "Remark"


    Public WithEvents Dgl2 As New AgControls.AgDataGrid
    Public Const Col2DocID As String = "DocID"
    Public Const Col2Item As String = "Item"
    Public Const Col2Qty As String = "Qty"
    Public Const Col2Unit As String = "Unit"
    Public Const Col2DealQty As String = "Deal Qty"
    Public Const Col2DealUnit As String = "Unit"
    Public Const Col2Rate As String = "Rate"
    Public Const Col2Remark As String = "Remark"


    Dim mSearchCode As String
    Dim mSearchCodeSr As Integer

    Dim mEntryMode$ = ""
    Dim mPartyDrCr As String = ""

    Public Property PartyDrCr() As String
        Get
            PartyDrCr = mPartyDrCr
        End Get
        Set(ByVal value As String)
            mPartyDrCr = value
        End Set
    End Property

    Public Property IntRate() As String
        Get
            IntRate = TxtIntRate.Text
        End Get
        Set(ByVal value As String)
            TxtIntRate.Text = value
        End Set
    End Property


    Public Property InvoiceNo() As String
        Get
            InvoiceNo = LblInvoiceNo.Text
        End Get
        Set(ByVal value As String)
            LblInvoiceNo.Text = value
        End Set
    End Property

    Public Property InvoiceDocID() As String
        Get
            InvoiceDocID = LblInvoiceNo.Tag
        End Get
        Set(ByVal value As String)
            LblInvoiceNo.Tag = value
        End Set
    End Property

    Public ReadOnly Property GetAdditions() As Double
        Get
            GetAdditions = Val(LblTotalAdditions.Text)
        End Get
    End Property
    Public ReadOnly Property GetDeductions() As Double
        Get
            GetDeductions = Val(LblTotalDeductions.Text)
        End Get
    End Property


    Public Property EntryMode() As String
        Get
            EntryMode = mEntryMode
        End Get
        Set(ByVal value As String)
            mEntryMode = value
        End Set
    End Property

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    'Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
    '    AgL.FPaintForm(Me, e, 0)
    'End Sub

    Public Sub IniGrid(DocID As String, Sr As Integer)

        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 35, 5, ColSNo, False, True, False)
            .AddAgTextColumn(Dgl1, Col1Head, 160, 255, Col1Head, True, True)
            .AddAgTextColumn(Dgl1, Col1AdditionDeduction, 160, 255, Col1AdditionDeduction, False, True)
            .AddAgTextColumn(Dgl1, Col1PostingAc, 160, 255, Col1PostingAc, False, True)
            .AddAgTextColumn(Dgl1, Col1RateCalculationType, 160, 255, Col1RateCalculationType, False, True)
            .AddAgTextColumn(Dgl1, Col1CalculateOn, 160, 255, Col1CalculateOn, False, True)
            .AddAgNumberColumn(Dgl1, Col1Rate, 50, 4, 2, False, Col1Rate, True, False, True)
            .AddAgNumberColumn(Dgl1, Col1Amount, 100, 8, 2, False, Col1Amount, True, False, True)
            .AddAgTextColumn(Dgl1, Col1Remark, 160, 255, Col1Remark, True, False)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 35
        Dgl1.AgSkipReadOnlyColumns = True


        Dgl2.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl2, ColSNo, 35, 5, ColSNo, False, True, False)
            .AddAgTextColumn(Dgl2, Col2DocID, 120, 255, Col2DocID, False, True)
            .AddAgTextColumn(Dgl2, Col2Item, 180, 255, Col2Item, True, True)
            .AddAgNumberColumn(Dgl2, Col2Qty, 50, 8, 2, False, Col2Rate, True, True, True)
            .AddAgTextColumn(Dgl2, Col2Unit, 60, 255, Col2Unit, True, True)
            .AddAgNumberColumn(Dgl2, Col2Rate, 50, 8, 2, False, Col2Rate, True, True, True)
            .AddAgNumberColumn(Dgl2, Col2DealQty, 50, 4, 2, False, Col2DealQty, True, True, True)
            .AddAgTextColumn(Dgl2, Col2DealUnit, 60, 255, Col2DealUnit, True, True)
            .AddAgTextColumn(Dgl2, Col2Remark, 100, 255, Col2Remark, False, False)
        End With
        AgL.AddAgDataGrid(Dgl2, Panel3)
        Dgl2.EnableHeadersVisualStyles = False
        Dgl2.ColumnHeadersHeight = 45
        Dgl2.AgSkipReadOnlyColumns = True


        FMoverec(DocID, Sr)
    End Sub
    Public Sub FMoverec(DocID As String, Sr As Integer)
        Dim DsTemp As DataSet = Nothing
        Dim I As Integer

        If PartyDrCr.ToUpper = "DEBIT" Then
            mQry = "Select Taxable_Amount, Net_Amount From SaleInvoice Where DocID = '" & InvoiceDocID & "'"
        Else
            mQry = "Select Taxable_Amount, Net_Amount From PurchInvoice Where DocID = '" & InvoiceDocID & "'"
        End If


        DsTemp = AgL.FillData(mQry, AgL.GCn)
        If DsTemp.Tables(0).Rows.Count > 0 Then
            LblTaxableAmountValue.Text = AgL.VNull(DsTemp.Tables(0).Rows(0)("Taxable_Amount"))
            LblNetAmountValue.Text = AgL.VNull(DsTemp.Tables(0).Rows(0)("Net_Amount"))
        End If

        mQry = "Select Head.Description as HeadDescription, Head.Code as HeadCode, Head.PostInAc,
                IfNull(L.AdditionDeduction, Head.AdditionDeduction) as AdditionDeduction, Head.RateCalculationType, Head.CalculateOn, L.Rate, L.Amount, L.Remark, L.IntRate
                From Cloth_SupplierSettlementAdjustmentHead Head 
                Left Join Cloth_SupplierSettlementInvoicesAdjustment L on L.AdjustmentHead = Head.Code And L.DocId = '" & DocID & "' And L.TSr ='" & Sr & "'                                               
                Order By Head.Sr "
        DsTemp = AgL.FillData(mQry, AgL.GCn)
        With DsTemp.Tables(0)
            Dgl1.RowCount = 1
            Dgl1.Rows.Clear()
            If .Rows.Count > 0 Then
                For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                    Dgl1.Rows.Add()
                    Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1
                    Dgl1.Item(Col1Head, I).Tag = AgL.XNull(.Rows(I)("HeadCode"))
                    Dgl1.Item(Col1Head, I).Value = AgL.XNull(.Rows(I)("HeadDescription"))
                    Dgl1.Item(Col1AdditionDeduction, I).Value = AgL.XNull(.Rows(I)("AdditionDeduction"))
                    Dgl1.Item(Col1PostingAc, I).Value = AgL.XNull(.Rows(I)("PostInAc"))
                    Dgl1.Item(Col1RateCalculationType, I).Value = AgL.XNull(.Rows(I)("RateCalculationType"))
                    If Dgl1.Item(Col1RateCalculationType, I).Value = "N/A" Then
                        Dgl1.CurrentCell = Dgl1(Col1Rate, I)
                        Dgl1.CurrentCell.ReadOnly = True
                        Dgl1.CurrentCell.Style.BackColor = Color.Beige
                    End If
                    Dgl1.Item(Col1CalculateOn, I).Tag = AgL.XNull(.Rows(I)("CalculateOn"))
                    Select Case Dgl1.Item(Col1CalculateOn, I).Tag.ToString.ToUpper
                        Case "QTY", "TAXABLE AMOUNT", "NET AMOUNT", "SUB TOTAL"
                            Dgl1.Item(Col1CalculateOn, I).Value = Dgl1.Item(Col1CalculateOn, I).Tag
                        Case Else
                            Dgl1.Item(Col1CalculateOn, I).Value = AgL.XNull(AgL.Dman_Execute("Select Description From Cloth_SupplierSettlementAdjustmentHead where Code='" & Dgl1.Item(Col1CalculateOn, I).Tag & "'", AgL.GCn).ExecuteScalar)
                    End Select
                    Dgl1.Item(Col1Rate, I).Value = AgL.XNull(.Rows(I)("Rate"))
                    Dgl1.Item(Col1Amount, I).Value = Format(AgL.VNull(.Rows(I)("Amount")), "0.00")
                    Dgl1.Item(Col1Remark, I).Value = AgL.XNull(.Rows(I)("Remark"))
                    If AgL.VNull(.Rows(I)("IntRate")) > 0 Then
                        TxtIntRate.Text = AgL.VNull(.Rows(I)("IntRate"))
                    End If
                Next I
            End If
        End With





        mQry = "select L.DocID, L.Sr, L.Item, I.Description as ItemName, L.Specification, 
                L.DocQty, L.Qty, L.Unit, L.DocDealQty, L.DealQty, L.DealUnit, L.Rate, L.DiscountPer, L.DiscountAmount, L.AdditionalDiscountPer, L.AdditionalDiscountAmount,
                L.Amount, L1.RdRate, L1.RdAmount, L1.ShortQty, L1.ShortAmount, L1.Remark
                from SaleInvoiceDetail L
                Left Join Cloth_SupplierSettlementInvoicesLine L1 On L.DocID = L1.InvoiceDocID And L.Sr = L1.InvoiceDocIDSr
                Left Join Item I On L.Item = I.Code
                Where L.DocId = '" & InvoiceDocID & "'  
                Order By L.Sr "
        DsTemp = AgL.FillData(mQry, AgL.GCn)
        With DsTemp.Tables(0)
            Dgl2.RowCount = 1
            Dgl2.Rows.Clear()
            If .Rows.Count > 0 Then
                For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                    Dgl2.Rows.Add()
                    Dgl2.Item(ColSNo, I).Value = Dgl2.Rows.Count - 1
                    Dgl2.Item(Col2DocID, I).Value = AgL.XNull(.Rows(I)("DocID"))
                    Dgl2.Item(Col2DocID, I).Tag = AgL.XNull(.Rows(I)("Sr"))
                    Dgl2.Item(Col2Item, I).Tag = AgL.XNull(.Rows(I)("Item"))
                    Dgl2.Item(Col2Item, I).Value = AgL.XNull(.Rows(I)("ItemName"))
                    Dgl2.Item(Col2Qty, I).Value = AgL.VNull(.Rows(I)("DocQty"))
                    Dgl2.Item(Col2Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                    Dgl2.Item(Col2DealQty, I).Value = AgL.VNull(.Rows(I)("DocDealQty"))
                    Dgl2.Item(Col2DealUnit, I).Value = AgL.XNull(.Rows(I)("DealUnit"))
                    Dgl2.Item(Col2Rate, I).Value = Format(AgL.VNull(.Rows(I)("Rate")), "0.00")
                    Dgl2.Item(Col2Remark, I).Value = AgL.XNull(.Rows(I)("Remark"))
                Next I
            End If
        End With

        Calculation()
    End Sub

    'Function FData_Validation() As Boolean
    '    Dim I As Integer
    '    For I = 0 To Dgl1.Rows.Count - 1
    '        'If Dgl1.Item(Col1FromUnit, I).Value = Dgl1.Item(Col1ToUnit, I).Value Then
    '        '    MsgBox("From Unit And To Unit should not be same at row no. " & I & ". can't continue.")
    '        '    Exit Function
    '        'End If
    '    Next
    '    FData_Validation = True
    'End Function


    Sub KeyPress_Form(ByVal Sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        If e.KeyChar = Chr(Keys.Escape) Then
            Me.Close()
        End If
    End Sub

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            AgL.GridDesign(Dgl1)

            Me.Top = 350
            Me.Left = 350
            Dgl1.Focus()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Dgl1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        Try
            If AgL.StrCmp(EntryMode, "Browse") Then Exit Sub
            If Dgl1.CurrentCell Is Nothing Then Exit Sub
            Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) = Nothing

            Select Case Dgl1.CurrentCell.RowIndex




            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    'Private Sub DGL1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.KeyDown
    '    If e.Control And e.KeyCode = Keys.D Then
    '        sender.CurrentRow.Selected = True
    '    End If
    '    If e.Control Or e.Shift Or e.Alt Then Exit Sub
    'End Sub

    Private Sub Dgl1_EditingControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.EditingControl_KeyDown
        Dim bRowIndex As Integer = 0, bColumnIndex As Integer = 0
        Dim bItemCode As String = ""
        Dim DrTemp As DataRow() = Nothing
        Try
            bRowIndex = Dgl1.CurrentCell.RowIndex
            bColumnIndex = Dgl1.CurrentCell.ColumnIndex

            If e.KeyCode = Keys.Enter Then Exit Sub
            If mEntryMode = "Browse" Then Exit Sub


            Select Case Dgl1.CurrentCell.RowIndex

            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Dgl1_EditingControl_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Dgl1.EditingControl_Validating
        If EntryMode = "Browse" Then Exit Sub
        Dim mRowIndex As Integer, mColumnIndex As Integer
        Try
            mRowIndex = Dgl1.CurrentCell.RowIndex
            mColumnIndex = Dgl1.CurrentCell.ColumnIndex
            If Dgl1.Item(mColumnIndex, mRowIndex).Value Is Nothing Then Dgl1.Item(mColumnIndex, mRowIndex).Value = ""
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                'Case Col1FromUnit
                '    Dgl1.Item(Col1Equal, mRowIndex).Value = "="
                '    Dgl1.Item(Col1ToUnit, mRowIndex).Value = mUnit
                '    Dgl1.Item(Col1ToQtyDecimalPlaces, mRowIndex).Value = mToQtyDecimalPlace
                '    If Val(Dgl1.Item(Col1FromQty, mRowIndex).Value) = 0 Then
                '        Dgl1.Item(Col1FromQty, mRowIndex).Value = "1"
                '    End If

                '    If Dgl1.AgSelectedValue(Col1FromUnit, mRowIndex) Is Nothing Then Dgl1.AgSelectedValue(Col1FromUnit, mRowIndex) = ""

                '    If Dgl1.Item(Col1FromUnit, mRowIndex).Value.ToString.Trim = "" Or Dgl1.AgSelectedValue(Col1FromUnit, mRowIndex).ToString.Trim = "" Then
                '        Dgl1.Item(Col1FromQtyDecimalPlaces, mRowIndex).Value = ""
                '    Else
                '        If Dgl1.AgDataRow IsNot Nothing Then
                '            Dgl1.Item(Col1FromQtyDecimalPlaces, mRowIndex).Value = AgL.XNull(Dgl1.AgDataRow.Cells("DecimalPlaces").Value)
                '        End If
                '    End If


            End Select
            Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub BtnChargeDuw_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnOk.Click
        Dim I As Integer = 0
        Select Case sender.Name
            Case BtnOk.Name
                If AgL.StrCmp(EntryMode, "Browse") Then Me.Close() : Exit Sub
                mOkButtonPressed = True
                Me.Close()
        End Select
    End Sub

    Private Sub DGL1_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles Dgl1.RowsAdded, Dgl1.RowsAdded
        sender(ColSNo, e.RowIndex).Value = e.RowIndex + 1
    End Sub

    Public Sub Calculation()
        Dim I As Integer, J As Integer
        Dim mTotalAddition As Double
        Dim mTotalDeduction As Double
        Dim mSubTotal As Double

        mSubTotal = Val(LblTaxableAmountValue.Text)
        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1Head, I).Value <> "" And Val(Dgl1.Item(Col1Rate, I).Value) > 0 Then
                If Dgl1.Item(Col1RateCalculationType, I).Value.ToString.ToUpper = "MULTIPLY" Then
                    If Dgl1.Item(Col1CalculateOn, I).Value.ToString.ToUpper = "TAXABLE AMOUNT" Then
                        Dgl1.Item(Col1Amount, I).Value = Format(Val(LblTaxableAmountValue.Text) * Val(Dgl1.Item(Col1Rate, I).Value), "0.00")
                    ElseIf Dgl1.Item(Col1CalculateOn, I).Value.ToString.ToUpper = "NET AMOUNT" Then
                        Dgl1.Item(Col1Amount, I).Value = Format(Val(LblNetAmountValue.Text) * Val(Dgl1.Item(Col1Rate, I).Value), "0.00")
                    ElseIf Dgl1.Item(Col1CalculateOn, I).Value.ToString.ToUpper = "SUB TOTAL" Then
                        Dgl1.Item(Col1Amount, I).Value = Format(Math.Abs(mSubTotal) * Val(Dgl1.Item(Col1Rate, I).Value), "0.00")
                    Else
                        For J = 0 To Dgl1.RowCount - 1
                            If Dgl1.Item(Col1CalculateOn, I).Tag = Dgl1.Item(Col1Head, J).Tag Then
                                Dgl1.Item(Col1Amount, I).Value = Format(Val(Dgl1.Item(Col1Amount, J).Value) * Val(Dgl1.Item(Col1Rate, I).Value), "0.00")
                            End If
                        Next
                    End If

                ElseIf Dgl1.Item(Col1RateCalculationType, I).Value.ToString.ToUpper = "PERCENTAGE" Then
                    If Dgl1.Item(Col1CalculateOn, I).Value.ToString.ToUpper = "TAXABLE AMOUNT" Then
                        Dgl1.Item(Col1Amount, I).Value = Format((Val(LblTaxableAmountValue.Text) * Val(Dgl1.Item(Col1Rate, I).Value)) / 100, "0.00")
                    ElseIf Dgl1.Item(Col1CalculateOn, I).Value.ToString.ToUpper = "NET AMOUNT" Then
                        Dgl1.Item(Col1Amount, I).Value = Format((Val(LblNetAmountValue.Text) * Val(Dgl1.Item(Col1Rate, I).Value)) / 100, "0.00")
                    ElseIf Dgl1.Item(Col1CalculateOn, I).Value.ToString.ToUpper = "SUB TOTAL" Then
                        Dgl1.Item(Col1Amount, I).Value = Format(Math.Abs(mSubTotal) * Val(Dgl1.Item(Col1Rate, I).Value) / 100, "0.00")
                    Else
                        For J = 0 To Dgl1.RowCount - 1
                            If Dgl1.Item(Col1CalculateOn, I).Tag = Dgl1.Item(Col1Head, J).Tag Then
                                Dgl1.Item(Col1Amount, I).Value = Format((Val(Dgl1.Item(Col1Amount, J).Value) * Val(Dgl1.Item(Col1Rate, I).Value)) / 100, "0.00")
                            End If
                        Next
                    End If
                ElseIf Dgl1.Item(Col1RateCalculationType, I).Value.ToString.ToUpper = "INTEREST" Then
                    Dim mInterestRate As Integer
                    If Val(TxtIntRate.Text) > 0 Then
                        mInterestRate = Val(TxtIntRate.Text)
                    Else
                        mInterestRate = 0
                    End If
                    If Dgl1.Item(Col1CalculateOn, I).Value.ToString.ToUpper = "TAXABLE AMOUNT" Then
                        Dgl1.Item(Col1Amount, I).Value = Format((Val(LblTaxableAmountValue.Text) * Val(Dgl1.Item(Col1Rate, I).Value)) * mInterestRate / 36500, "0.00")
                    ElseIf Dgl1.Item(Col1CalculateOn, I).Value.ToString.ToUpper = "NET AMOUNT" Then
                        Dgl1.Item(Col1Amount, I).Value = Format((Val(LblNetAmountValue.Text) * Val(Dgl1.Item(Col1Rate, I).Value)) * mInterestRate / 36500, "0.00")
                    ElseIf Dgl1.Item(Col1CalculateOn, I).Value.ToString.ToUpper = "SUB TOTAL" Then
                        Dgl1.Item(Col1Amount, I).Value = Format(Math.Abs(mSubTotal) * Val(Dgl1.Item(Col1Rate, I).Value) * mInterestRate / 36500, "0.00")
                    Else
                        For J = 0 To Dgl1.RowCount - 1
                            If Dgl1.Item(Col1CalculateOn, I).Tag = Dgl1.Item(Col1Head, J).Tag Then
                                Dgl1.Item(Col1Amount, I).Value = Format((Val(Dgl1.Item(Col1Amount, J).Value) * Val(Dgl1.Item(Col1Rate, I).Value)) * mInterestRate / 36500, "0.00")
                            End If
                        Next
                    End If
                End If
            End If

            'If Val(Dgl1.Item(Col1Amount, I).Value) > 0 Then
            If AgL.XNull(Dgl1.Item(Col1AdditionDeduction, I).Value).ToString.ToUpper() = "ADDITION" Then
                mSubTotal += AgL.VNull(Dgl1.Item(Col1Amount, I).Value)
            Else
                mSubTotal -= AgL.VNull(Dgl1.Item(Col1Amount, I).Value)
            End If
            'End If

        Next



        For I = 0 To Dgl1.RowCount - 1
            'If Val(Dgl1.Item(Col1Amount, I).Value) > 0 Then
            If AgL.XNull(Dgl1.Item(Col1AdditionDeduction, I).Value).ToString.ToUpper() = "ADDITION" Then
                mTotalAddition += AgL.VNull(Dgl1.Item(Col1Amount, I).Value)
            Else
                mTotalDeduction += AgL.VNull(Dgl1.Item(Col1Amount, I).Value)
            End If
            'End If
        Next
        LblTotalDeductions.Text = mTotalDeduction.ToString()
        LblTotalAdditions.Text = mTotalAddition.ToString()
    End Sub



    Public Sub FSave(DocId As String, TSr As Integer, ByVal Conn As Object, ByVal Cmd As Object)
        Dim I As Integer
        Dim mSr As Integer
        mQry = "Delete From Cloth_SupplierSettlementInvoicesAdjustment Where DocId = '" & DocId & "' and TSr = " & TSr & " "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)


        For I = 0 To Dgl1.RowCount - 1
            If Val(Dgl1.Item(Col1Amount, I).Value) > 0 Then
                mSr += 1
                mQry = " INSERT INTO Cloth_SupplierSettlementInvoicesAdjustment (DocID, TSr, Sr, AdjustmentHead, AdditionDeduction, Rate, Amount, Remark, IntRate) 
                        VALUES (" & AgL.Chk_Text(DocId) & ", 
                        " & TSr & ", 
                        " & mSr & ", 
                        " & AgL.Chk_Text(Dgl1.Item(Col1Head, I).Tag) & ", 
                        " & AgL.Chk_Text(Dgl1.Item(Col1AdditionDeduction, I).Value) & ", 
                        " & Val(Dgl1.Item(Col1Rate, I).Value) & ", 
                        " & Val(Dgl1.Item(Col1Amount, I).Value) & ", 
                        " & AgL.Chk_Text(Dgl1.Item(Col1Remark, I).Value) & ",
                        " & Val(TxtIntRate.Text) & ") "
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            End If
        Next
    End Sub

End Class