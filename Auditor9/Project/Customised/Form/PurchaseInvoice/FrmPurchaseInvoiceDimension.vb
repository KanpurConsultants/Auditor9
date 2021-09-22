Imports System.Data.SQLite
Public Class FrmPurchaseInvoiceDimension
    Dim mQry As String = ""
    Public mOkButtonPressed As Boolean

    Public Const ColSNo As String = "S.No."
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Public Const Col1Specification As String = "Specification"
    Public Const Col1Pcs As String = "Pcs"
    Public Const Col1Qty As String = "Qty"
    Public Const Col1TotalQty As String = "Total Qty"


    Dim mSearchCode As String
    Dim mSearchCodeSr As Integer

    Dim mEntryMode$ = ""
    Dim mUnit$ = ""
    Dim mUnitDecimalPlace As Integer
    Dim mItemName As String
    Dim mObjFrmPurchInvoice As FrmPurchInvoiceDirect

    Public Property objFrmPurchInvoice() As FrmPurchInvoiceDirect
        Get
            objFrmPurchInvoice = mObjFrmPurchInvoice
        End Get
        Set(ByVal value As FrmPurchInvoiceDirect)
            mObjFrmPurchInvoice = value
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

    Public Property Unit() As String
        Get
            Unit = mUnit
        End Get
        Set(ByVal value As String)
            mUnit = value
        End Set
    End Property

    Public Property UnitDecimalPlace() As Integer
        Get
            UnitDecimalPlace = mUnitDecimalPlace
        End Get
        Set(ByVal value As Integer)
            mUnitDecimalPlace = value
        End Set
    End Property

    Public ReadOnly Property GetTotalQty() As Double
        Get
            GetTotalQty = Val(LblTotalQty.Text)
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
            .AddAgTextColumn(Dgl1, Col1Specification, 160, 255, Col1Specification, False, True)
            .AddAgNumberColumn(Dgl1, Col1Qty, 100, 8, mUnitDecimalPlace, False, mUnit, True, False, True)
            .AddAgNumberColumn(Dgl1, Col1Pcs, 100, 5, 0, False, Col1Pcs, True, False, True)
            .AddAgNumberColumn(Dgl1, Col1TotalQty, 100, 8, 4, False, "Total " & mUnit, True, False, True)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 35
        Dgl1.AgSkipReadOnlyColumns = True

        FMoverec(DocID, Sr)
    End Sub
    Public Sub FMoverec(DocID As String, Sr As Integer)
        Dim DsTemp As DataSet = Nothing
        Dim I As Integer
        mQry = "Select L.*, U.DecimalPlaces as QtyDecimalPlaces 
                From PurchInvoiceDimensionDetail L
                Left Join PurchInvoiceDetail IL on L.DocId = IL.DocId And L.Tsr = IL.Sr                
                Left Join Unit U On IL.Unit = U.Code
                Where L.DocId = '" & DocID & "' And L.TSr ='" & Sr & "'
                Order By L.Sr "
        DsTemp = AgL.FillData(mQry, AgL.GCn)
        With DsTemp.Tables(0)
            Dgl1.RowCount = 1
            Dgl1.Rows.Clear()
            If .Rows.Count > 0 Then
                For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                    Dgl1.Rows.Add()
                    Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1
                    Dgl1.Item(ColSNo, I).Tag = AgL.XNull(.Rows(I)("Sr"))
                    Dgl1.Item(Col1Specification, I).Value = AgL.XNull(.Rows(I)("Specification"))
                    Dgl1.Item(Col1Qty, I).Value = Format(Math.Abs(AgL.VNull(.Rows(I)("Qty"))), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                    Dgl1.Item(Col1Pcs, I).Value = Math.Abs(AgL.VNull(.Rows(I)("Pcs")))
                    Dgl1.Item(Col1TotalQty, I).Value = Format(Math.Abs(AgL.VNull(.Rows(I)("TotalQty"))), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
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

            Me.Top = 400
            Me.Left = 400
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
        Dim I As Integer
        Dim mTotalQty As Double
        For I = 0 To Dgl1.RowCount - 1
            If Val(Dgl1.Item(Col1Pcs, I).Value) > 0 And Val(Dgl1.Item(Col1Qty, I).Value) > 0 Then
                Dgl1.Item(Col1TotalQty, I).Value = Val(Dgl1.Item(Col1Pcs, I).Value) * Val(Dgl1.Item(Col1Qty, I).Value)
            End If
            If Val(Dgl1.Item(Col1TotalQty, I).Value) > 0 Then
                mTotalQty += Val(Dgl1.Item(Col1TotalQty, I).Value)
            End If
        Next
        LblTotalQty.Text = mTotalQty.ToString()
    End Sub

    Public Sub FSave(DocId As String, TSr As Integer, mGridRowIndex As Integer, ByVal Conn As Object, ByVal Cmd As Object, Optional MultiplyWithMinus As Boolean = False)
        Dim I As Integer
        Dim mSr As Integer
        Dim mDimensionDetail As String

        Dim bSalesTaxGroupParty As String = ""
        If mObjFrmPurchInvoice.BtnFillPartyDetail.Tag IsNot Nothing Then
            bSalesTaxGroupParty = mObjFrmPurchInvoice.BtnFillPartyDetail.Tag.Dgl1.Item(mObjFrmPurchInvoice.BtnFillPartyDetail.Tag.Col1Value, mObjFrmPurchInvoice.BtnFillPartyDetail.Tag.rowSalesTaxGroup).Value
        End If
        'mQry = "Delete From PurchInvoiceDimensionDetail Where DocId = '" & DocId & "' and TSr = " & TSr & " "
        'AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)


        'For I = 0 To Dgl1.RowCount - 1
        '    If Val(Dgl1.Item(Col1TotalQty, I).Value) > 0 Then
        '        mSr += 1
        '        mQry = " INSERT INTO PurchInvoiceDimensionDetail (DocID, TSr, Sr, Specification, Pcs, Qty,TotalQty) " &
        '               " VALUES (" & AgL.Chk_Text(DocId) & ", " &
        '               " " & TSr & ", " &
        '                " " & mSr & ", " &
        '                " " & AgL.Chk_Text(Dgl1.Item(Col1Specification, I).Tag) & ", " &
        '                " " & Val(Dgl1.Item(Col1Pcs, I).Value) & ", " &
        '                " " & Val(Dgl1.Item(Col1Qty, I).Value) & ", " & Val(Dgl1.Item(Col1TotalQty, I).Value) & ") "
        '        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        '    End If
        'Next


        mDimensionDetail = ""
        For I = 0 To Dgl1.RowCount - 1
            If MultiplyWithMinus Then
                Dgl1.Item(Col1Pcs, I).Value = Val(Dgl1.Item(Col1Pcs, I).Value) * -1
                Dgl1.Item(Col1Qty, I).Value = Val(Dgl1.Item(Col1Qty, I).Value) * -1
                Dgl1.Item(Col1TotalQty, I).Value = Val(Dgl1.Item(Col1TotalQty, I).Value) * -1
            End If

            If Dgl1.Rows(I).Visible Then
                If Val(Dgl1.Item(Col1TotalQty, I).Value) <> 0 Then
                    If Dgl1.Item(ColSNo, I).Tag Is Nothing Then
                        mObjFrmPurchInvoice.mDimensionSrl += 1
                        mQry = " INSERT INTO PurchInvoiceDimensionDetail (DocID, TSr, Sr, Specification, Pcs, Qty, TotalQty) " &
                           " VALUES (" & AgL.Chk_Text(DocId) & ", " &
                           " " & TSr & ", " &
                            " " & mObjFrmPurchInvoice.mDimensionSrl & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1Specification, I).Tag) & ", " &
                            " " & Val(Dgl1.Item(Col1Pcs, I).Value) & ", " &
                            " " & Val(Dgl1.Item(Col1Qty, I).Value) & ", " & Val(Dgl1.Item(Col1TotalQty, I).Value) & ") "
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)



                        mQry = "Insert Into Stock(DocID, TSr, Sr, V_Type, 
                                V_Prefix, V_Date, V_No, RecID, 
                                Div_Code, Site_Code, SubCode, SalesTaxGroupParty, Item, SalesTaxGroupItem,  
                                LotNo, EType_IR, Qty_Rec, Qty_Iss, 
                                Unit, Rate, Amount, Landed_Value) 
                                Values
                                (
                                    '" & DocId & "', " & TSr & ", " & mObjFrmPurchInvoice.mDimensionSrl & ", " & AgL.Chk_Text(mObjFrmPurchInvoice.TxtV_Type.Tag) & ",
                                    " & AgL.Chk_Text(mObjFrmPurchInvoice.LblPrefix.Text) & ", " & AgL.Chk_Date(mObjFrmPurchInvoice.TxtV_Date.Text) & ", " & AgL.Chk_Text(mObjFrmPurchInvoice.TxtV_No.Text) & ", " & AgL.Chk_Text(mObjFrmPurchInvoice.TxtReferenceNo.Text) & ",
                                    " & AgL.Chk_Text(mObjFrmPurchInvoice.TxtDivision.Tag) & ", " & AgL.Chk_Text(mObjFrmPurchInvoice.TxtSite_Code.Tag) & ", " & AgL.Chk_Text(mObjFrmPurchInvoice.TxtVendor.Tag) & ", " & AgL.Chk_Text(bSalesTaxGroupParty) & ", " & AgL.Chk_Text(mObjFrmPurchInvoice.Dgl1(FrmPurchInvoiceDirect.Col1Item, mGridRowIndex).Tag) & ", " & AgL.Chk_Text(mObjFrmPurchInvoice.Dgl1(FrmPurchInvoiceDirect.Col1SalesTaxGroup, mGridRowIndex).Tag) & ",
                                    " & AgL.Chk_Text(mObjFrmPurchInvoice.Dgl1(FrmPurchInvoiceDirect.Col1LotNo, mGridRowIndex).Value) & ", 'R', 
                                    " & Val(Dgl1(Col1TotalQty, I).Value) & ", 0,
                                    " & AgL.Chk_Text(mObjFrmPurchInvoice.Dgl1(FrmPurchInvoiceDirect.Col1Unit, mGridRowIndex).Value) & ", " & AgL.Chk_Text(mObjFrmPurchInvoice.Dgl1(FrmPurchInvoiceDirect.Col1Rate, mGridRowIndex).Value) & ", " & AgL.Chk_Text(mObjFrmPurchInvoice.Dgl1(FrmPurchInvoiceDirect.Col1Amount, mGridRowIndex).Value) & ",0
                                )   "
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)


                        If mDimensionDetail <> "" Then
                            If Dgl1.Columns(Col1Pcs).DisplayIndex < Dgl1.Columns(Col1Qty).DisplayIndex Then
                                mDimensionDetail += ", " + AgL.XNull(Dgl1.Item(Col1Pcs, I).Value) + " X " + AgL.XNull(Dgl1.Item(Col1Qty, I).Value)
                            Else
                                mDimensionDetail += ", " + AgL.XNull(Dgl1.Item(Col1Qty, I).Value) + " X " + AgL.XNull(Dgl1.Item(Col1Pcs, I).Value)
                            End If
                        Else
                            If Dgl1.Columns(Col1Pcs).DisplayIndex < Dgl1.Columns(Col1Qty).DisplayIndex Then
                                mDimensionDetail = Dgl1.Item(Col1Pcs, I).Value.ToString + " X " + Dgl1.Item(Col1Qty, I).Value.ToString
                            Else
                                mDimensionDetail = AgL.XNull(Dgl1.Item(Col1Qty, I).Value) + " X " + AgL.XNull(Dgl1.Item(Col1Pcs, I).Value)
                            End If
                        End If


                    Else
                        mQry = "Update PurchInvoiceDimensionDetail Set 
                            Specification = " & AgL.Chk_Text(Dgl1.Item(Col1Specification, I).Tag) & ",
                            Pcs = " & Val(Dgl1.Item(Col1Pcs, I).Value) & ",
                            Qty = " & Val(Dgl1.Item(Col1Qty, I).Value) & ",
                            TotalQty = " & Val(Dgl1.Item(Col1TotalQty, I).Value) & "
                            Where DocID = '" & DocId & "' And TSr = " & TSr & " And Sr = " & Dgl1.Item(ColSNo, I).Tag & "
                            "
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)


                        mQry = "Update Stock Set V_Type = " & AgL.Chk_Text(mObjFrmPurchInvoice.TxtV_Type.Tag) & ",
                                    V_Prefix = " & AgL.Chk_Text(mObjFrmPurchInvoice.LblPrefix.Text) & ", 
                                    V_Date = " & AgL.Chk_Date(mObjFrmPurchInvoice.TxtV_Date.Text) & ", 
                                    V_No = " & AgL.Chk_Text(mObjFrmPurchInvoice.TxtV_No.Text) & ", 
                                    RecId = " & AgL.Chk_Text(mObjFrmPurchInvoice.TxtReferenceNo.Text) & ",
                                    Div_Code = " & AgL.Chk_Text(mObjFrmPurchInvoice.TxtDivision.Tag) & ", 
                                    Site_Code = " & AgL.Chk_Text(mObjFrmPurchInvoice.TxtSite_Code.Tag) & ", 
                                    Subcode = " & AgL.Chk_Text(mObjFrmPurchInvoice.TxtVendor.Tag) & ", 
                                    SalesTaxGroupParty = " & AgL.Chk_Text(bSalesTaxGroupParty) & ", 
                                    Item = " & AgL.Chk_Text(mObjFrmPurchInvoice.Dgl1(FrmPurchInvoiceDirect.Col1Item, mGridRowIndex).Tag) & ",
                                    SalesTaxGroupItem = " & AgL.Chk_Text(mObjFrmPurchInvoice.Dgl1(FrmPurchInvoiceDirect.Col1SalesTaxGroup, mGridRowIndex).Value) & ",
                                    LotNo = " & AgL.Chk_Text(mObjFrmPurchInvoice.Dgl1(FrmPurchInvoiceDirect.Col1LotNo, mGridRowIndex).Value) & ", 
                                    EType_IR = 'R', 
                                    Qty_Rec = " & Val(Dgl1(Col1TotalQty, I).Value) & ", 
                                    Qty_Iss=0,
                                    Unit=" & AgL.Chk_Text(mObjFrmPurchInvoice.Dgl1(FrmPurchInvoiceDirect.Col1Unit, mGridRowIndex).Value) & ", 
                                    Rate=" & AgL.Chk_Text(mObjFrmPurchInvoice.Dgl1(FrmPurchInvoiceDirect.Col1Rate, mGridRowIndex).Value) & ", 
                                    Amount=" & AgL.Chk_Text(mObjFrmPurchInvoice.Dgl1(FrmPurchInvoiceDirect.Col1Amount, mGridRowIndex).Value) & ",
                                    Landed_Value=0                                
                                    Where DocID = '" & DocId & "' And TSr = " & TSr & " And Sr = " & Dgl1.Item(ColSNo, I).Tag & "
                                "
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                        If mDimensionDetail <> "" Then
                            If Dgl1.Columns(Col1Pcs).DisplayIndex < Dgl1.Columns(Col1Qty).DisplayIndex Then
                                mDimensionDetail += ", " + AgL.XNull(Dgl1.Item(Col1Pcs, I).Value.ToString) + " X " + AgL.XNull(Dgl1.Item(Col1Qty, I).Value)
                            Else
                                mDimensionDetail += ", " + AgL.XNull(Dgl1.Item(Col1Qty, I).Value.ToString) + " X " + AgL.XNull(Dgl1.Item(Col1Pcs, I).Value)
                            End If
                        Else
                            If Dgl1.Columns(Col1Pcs).DisplayIndex < Dgl1.Columns(Col1Qty).DisplayIndex Then
                                mDimensionDetail = AgL.XNull(Dgl1.Item(Col1Pcs, I).Value) + " X " + AgL.XNull(Dgl1.Item(Col1Qty, I).Value)
                            Else
                                mDimensionDetail = AgL.XNull(Dgl1.Item(Col1Qty, I).Value) + " X " + AgL.XNull(Dgl1.Item(Col1Pcs, I).Value)
                            End If
                        End If
                    End If
                End If
            Else
                If Dgl1.Item(ColSNo, I).Tag IsNot Nothing Then
                    mQry = "Delete from Stock Where DocID = '" & DocId & "' And TSr = " & TSr & " And Sr = " & Dgl1.Item(ColSNo, I).Tag & ""
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                    mQry = "Delete from PurchInvoiceDimensionDetail  Where DocID = '" & DocId & "' And TSr = " & TSr & " And Sr = " & Dgl1.Item(ColSNo, I).Tag & ""
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                End If
            End If
        Next

        mQry = "Update PurchInvoiceDetail Set DimensionDetail = " & AgL.Chk_Text(mDimensionDetail) & " Where DocID = '" & DocId & "' and Sr = " & TSr & "  "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    End Sub

End Class