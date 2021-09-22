Imports System.Data.SQLite
Public Class FrmPartyAcSettlementInvoiceLine
    Dim mQry As String = ""
    Public mOkButtonPressed As Boolean

    Public Const ColSNo As String = "S.No."
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Public Const Col1DocID As String = "DocID"
    Public Const Col1Item As String = "Item"
    Public Const Col1Qty As String = "Qty"
    Public Const Col1Unit As String = "Unit"
    Public Const Col1Rate As String = "Rate"
    Public Const Col1DiscountPer As String = "Disc. %"
    Public Const Col1DiscountAmount As String = "Disc."
    Public Const Col1AdditionalDiscountPer As String = "Add. Disc. %"
    Public Const Col1AdditionalDiscountAmount As String = "Add. Disc."
    Public Const Col1Amount As String = "Amount"
    Public Const Col1RDRate As String = "RD Rate"
    Public Const Col1RDAmount As String = "RD Amount"
    Public Const Col1ShortQty As String = "Short Qty"
    Public Const Col1ShortAmount As String = "Short Amount"
    Public Const Col1Remark As String = "Remark"


    Dim mSearchCode As String
    Dim mSearchCodeSr As Integer

    Dim mEntryMode$ = ""

    Public Property InvoiceNo() As String
        Get
            InvoiceNo = LblInvoiceNo.Text
        End Get
        Set(ByVal value As String)
            LblInvoiceNo.Text = value
        End Set
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

    Public Sub IniGrid(DocID As String, Sr As Integer, InvoiceDocID As String)

        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 35, 5, ColSNo, False, True, False)
            .AddAgTextColumn(Dgl1, Col1DocID, 120, 255, Col1DocID, False, True)
            .AddAgTextColumn(Dgl1, Col1Item, 180, 255, Col1Item, True, True)
            .AddAgNumberColumn(Dgl1, Col1Qty, 50, 8, 2, False, Col1Rate, True, True, True)
            .AddAgTextColumn(Dgl1, Col1Unit, 60, 255, Col1Unit, True, True)
            .AddAgNumberColumn(Dgl1, Col1Rate, 50, 8, 2, False, Col1Rate, True, True, True)
            .AddAgNumberColumn(Dgl1, Col1DiscountPer, 50, 4, 2, False, Col1DiscountPer, True, True, True)
            .AddAgNumberColumn(Dgl1, Col1DiscountAmount, 50, 8, 2, False, Col1DiscountAmount, True, True, True)
            .AddAgNumberColumn(Dgl1, Col1AdditionalDiscountPer, 50, 4, 2, False, Col1AdditionalDiscountPer, True, True, True)
            .AddAgNumberColumn(Dgl1, Col1AdditionalDiscountAmount, 50, 8, 2, False, Col1AdditionalDiscountAmount, True, True, True)
            .AddAgNumberColumn(Dgl1, Col1Amount, 80, 8, 2, False, Col1Amount, True, True, True)
            .AddAgNumberColumn(Dgl1, Col1RDRate, 50, 4, 2, False, Col1RDAmount, True, False, True)
            .AddAgNumberColumn(Dgl1, Col1RDAmount, 50, 8, 2, False, Col1RDAmount, True, False, True)
            .AddAgNumberColumn(Dgl1, Col1ShortQty, 50, 4, 2, False, Col1ShortQty, True, False, True)
            .AddAgNumberColumn(Dgl1, Col1ShortAmount, 50, 8, 2, False, Col1ShortAmount, True, False, True)
            .AddAgTextColumn(Dgl1, Col1Remark, 100, 255, Col1Remark, False, False)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 45
        Dgl1.AgSkipReadOnlyColumns = True

        FMoverec(DocID, Sr, InvoiceDocID)
    End Sub
    Public Sub FMoverec(DocID As String, Sr As Integer, InvoiceDocID As String)
        Dim DsTemp As DataSet = Nothing
        Dim I As Integer
        mQry = "select L.DocID, L.Sr, L.Item, I.Description as ItemName, L.Specification, 
                L.DocQty, L.Qty, L.Unit, L.Rate, L.DiscountPer, L.DiscountAmount, L.AdditionalDiscountPer, L.AdditionalDiscountAmount,
                L.Amount, L1.RdRate, L1.RdAmount, L1.ShortQty, L1.ShortAmount, L1.Remark
                from PurchInvoiceDetail L
                Left Join Cloth_SupplierSettlementInvoicesLine L1 On L.DocID = L1.InvoiceDocID And L.Sr = L1.InvoiceDocIDSr
                Left Join Item I On L.Item = I.Code
                Where L.DocId = '" & InvoiceDocID & "'  
                Order By L.Sr "
        DsTemp = AgL.FillData(mQry, AgL.GCn)
        With DsTemp.Tables(0)
            Dgl1.RowCount = 1
            Dgl1.Rows.Clear()
            If .Rows.Count > 0 Then
                For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                    Dgl1.Rows.Add()
                    Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1
                    Dgl1.Item(Col1DocID, I).Value = AgL.XNull(.Rows(I)("DocID"))
                    Dgl1.Item(Col1DocID, I).Tag = AgL.XNull(.Rows(I)("Sr"))
                    Dgl1.Item(Col1Item, I).Tag = AgL.XNull(.Rows(I)("Item"))
                    Dgl1.Item(Col1Item, I).Value = AgL.XNull(.Rows(I)("ItemName"))
                    Dgl1.Item(Col1Qty, I).Value = AgL.VNull(.Rows(I)("DocQty"))
                    Dgl1.Item(Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                    Dgl1.Item(Col1Rate, I).Value = Format(AgL.VNull(.Rows(I)("Rate")), "0.00")
                    Dgl1.Item(Col1DiscountPer, I).Value = Format(AgL.VNull(.Rows(I)("DiscountPer")), "0.00")
                    Dgl1.Item(Col1DiscountAmount, I).Value = Format(AgL.VNull(.Rows(I)("DiscountAmount")), "0.00")
                    Dgl1.Item(Col1AdditionalDiscountPer, I).Value = Format(AgL.VNull(.Rows(I)("AdditionalDiscountPer")), "0.00")
                    Dgl1.Item(Col1AdditionalDiscountAmount, I).Value = Format(AgL.VNull(.Rows(I)("AdditionalDiscountAmount")), "0.00")
                    Dgl1.Item(Col1Amount, I).Value = Format(AgL.VNull(.Rows(I)("Amount")), "0.00")
                    Dgl1.Item(Col1RDRate, I).Value = Format(AgL.VNull(.Rows(I)("RDRate")), "0.00")
                    Dgl1.Item(Col1RDAmount, I).Value = Format(AgL.VNull(.Rows(I)("RDAmount")), "0.00")
                    Dgl1.Item(Col1ShortQty, I).Value = Format(AgL.VNull(.Rows(I)("ShortQty")), "0.00")
                    Dgl1.Item(Col1ShortAmount, I).Value = Format(AgL.VNull(.Rows(I)("ShortAmount")), "0.00")
                    Dgl1.Item(Col1Remark, I).Value = AgL.XNull(.Rows(I)("Remark"))
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
            Me.Width = 880
            Me.Height = 300
            Me.Top = 350
            Me.Left = 25
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
        Dim mTotalDeduction As Double

        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1Item, I).Value <> "" Then
                If Val(Dgl1.Item(Col1RDRate, I).Value) > 0 Then
                    Dgl1.Item(Col1RDAmount, I).Value = Format(Val(Dgl1.Item(Col1Qty, I).Value) * Val(Dgl1.Item(Col1RDRate, I).Value), "0.00")
                End If

                If Val(Dgl1.Item(Col1ShortQty, I).Value) > 0 Then
                    Dgl1.Item(Col1ShortAmount, I).Value = Format(Val(Dgl1.Item(Col1ShortQty, I).Value) * ((Val(Dgl1.Item(Col1Amount, I).Value) - Val(Dgl1.Item(Col1RDAmount, I).Value)) / Val(Dgl1.Item(Col1Qty, I).Value)), "0.00")
                End If

                mTotalDeduction += Val(Dgl1.Item(Col1ShortAmount, I).Value) + Dgl1.Item(Col1RDAmount, I).Value
            End If
        Next



        LblTotalDeductions.Text = mTotalDeduction.ToString()
    End Sub



    Public Sub FSave(DocId As String, TSr As Integer, ByVal Conn As Object, ByVal Cmd As Object)
        Dim I As Integer
        Dim mSr As Integer
        mQry = "Delete From Cloth_SupplierSettlementInvoicesLine Where DocId = '" & DocId & "' and TSr = " & TSr & " "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)


        For I = 0 To Dgl1.RowCount - 1
            If Val(Dgl1.Item(Col1RDAmount, I).Value) > 0 Or Val(Dgl1.Item(Col1ShortAmount, I).Value) > 0 Then
                mSr += 1
                mQry = " INSERT INTO Cloth_SupplierSettlementInvoicesLine (DocID, TSr, Sr, InvoiceDocId, InvoiceDocIdSr, RdRate, RdAmount, ShortQty, ShortAmount, Remark) 
                        VALUES (" & AgL.Chk_Text(DocId) & ", 
                        " & TSr & ", 
                        " & mSr & ", 
                        " & AgL.Chk_Text(Dgl1.Item(Col1DocID, I).Value) & ",
                        " & Val(Dgl1.Item(Col1DocID, I).Tag) & ", 
                        " & Val(Dgl1.Item(Col1RDRate, I).Value) & ", 
                        " & Val(Dgl1.Item(Col1RDAmount, I).Value) & ", 
                        " & Val(Dgl1.Item(Col1ShortQty, I).Value) & ", 
                        " & Val(Dgl1.Item(Col1ShortAmount, I).Value) & ", 
                        " & AgL.Chk_Text(Dgl1.Item(Col1Remark, I).Value) & ") "
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            End If
        Next
    End Sub

End Class