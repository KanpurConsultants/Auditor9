Imports System.Data.SQLite
Imports AgLibrary.ClsMain.agConstants
Imports Customised.ClsMain

Public Class FrmVoucherEntryCancel
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Dim mSearchCode$ = "", mEntryNCat$ = "", mV_Date$ = "", mParty$ = ""

    Public WithEvents AgCalcGrid1 As New AgStructure.AgCalcGrid

    Public DtV_TypeSettings As DataTable
    Protected Const Col1Select As String = "Tick"
    Protected Const ColSNo As String = "S.No."
    Protected Const Col1EffectiveDate As String = "Effective Date"
    Protected Const Col1Subcode As String = "Subcode"
    Protected Const Col1LinkedSubcode As String = "Linked Account"
    Protected Const Col1DrCr As String = "DrCr"
    Protected Const Col1HeaderSubcode As String = "Header Subcode"
    Protected Const Col1SalesTaxGroup As String = "Sales Tax Group Item"
    Protected Const Col1HSN As String = "HSN"
    Protected Const Col1SpecificationDocId As String = "Specification DocId"
    Protected Const Col1SpecificationDocIdSr As String = "Specification DocId Sr"
    Protected Const Col1Specification As String = "Specification"
    Protected Const Col1Qty As String = "Qty"
    Protected Const Col1Unit As String = "Unit"
    Protected Const Col1QtyDecimalPlaces As String = "Qty Decimal Places"
    Protected Const Col1Rate As String = "Rate"
    Protected Const Col1Amount As String = "Amount"
    Protected Const Col1ChqRefNo As String = "Chq/Ref No"
    Protected Const Col1ChqRefDate As String = "Chq/Ref Date"
    Protected Const Col1Deduction As String = "Deduction"
    Protected Const Col1OtherCharges As String = "Other Charges"
    Protected Const Col1Remark As String = "Remark"
    Protected Const Col1CurrentBalance As String = "Current Balance"
    Protected Const Col1CancelDate As String = "Cancel Date"
    Protected Const Col1AdditionalCharge As String = "Additional Charge"
    Protected Const Col1AdditionalChargeAc As String = "Additional Charge A/c"
    Protected Const Col1CancelRemark As String = "Cancel Remark"
    Protected Const Col1BtnDeleteCancellation As String = "Delete Cancellation"

    Dim mQry As String = ""
    Dim mTransactionType As String = ""
    Public Property SearchCode() As String
        Get
            SearchCode = mSearchCode
        End Get
        Set(ByVal value As String)
            mSearchCode = value
        End Set
    End Property
    Public Property EntryNCat() As String
        Get
            EntryNCat = mEntryNCat
        End Get
        Set(ByVal value As String)
            mEntryNCat = value
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
    Public Property Party() As String
        Get
            Party = mParty
        End Get
        Set(ByVal value As String)
            mParty = value
        End Set
    End Property
    Public Sub Ini_Grid()
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl1, Col1Select, 35, 0, Col1Select, True, True, False)
            .AddAgDateColumn(Dgl1, Col1EffectiveDate, 100, Col1EffectiveDate, False, True)
            .AddAgTextColumn(Dgl1, Col1Subcode, 150, 0, AgL.XNull(DtV_TypeSettings.Rows(0)("Caption_SubcodeLine")), True, True)
            .AddAgTextColumn(Dgl1, Col1LinkedSubcode, 150, 0, Col1LinkedSubcode, True, True)
            .AddAgTextColumn(Dgl1, Col1DrCr, 150, 0, Col1DrCr, False, True)
            .AddAgTextColumn(Dgl1, Col1HeaderSubcode, 150, 0, Col1HeaderSubcode, False, True)
            .AddAgTextColumn(Dgl1, Col1Specification, 130, 0, AgL.XNull(DtV_TypeSettings.Rows(0)("Caption_Specification")), CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Specification")), Boolean), True)
            .AddAgTextColumn(Dgl1, Col1SalesTaxGroup, 100, 0, Col1SalesTaxGroup, False, True)
            .AddAgTextColumn(Dgl1, Col1QtyDecimalPlaces, 50, 0, Col1QtyDecimalPlaces, False, True, False)
            .AddAgNumberColumn(Dgl1, Col1Qty, 80, 8, 4, False, Col1Qty, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Qty")), Boolean), True, True)
            .AddAgTextColumn(Dgl1, Col1Unit, 50, 0, Col1Unit, False, True)
            .AddAgNumberColumn(Dgl1, Col1Rate, 80, 8, 2, False, Col1Rate, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Rate")), Boolean), True, True)
            .AddAgNumberColumn(Dgl1, Col1Amount, 90, 8, 2, False, Col1Amount, True, True, True)
            .AddAgTextColumn(Dgl1, Col1ChqRefNo, 80, 255, Col1ChqRefNo, True, True)
            .AddAgDateColumn(Dgl1, Col1ChqRefDate, 110, Col1ChqRefDate, True, True)
            .AddAgNumberColumn(Dgl1, Col1Deduction, 90, 8, 2, False, Col1Deduction, True, True, True)
            .AddAgNumberColumn(Dgl1, Col1OtherCharges, 90, 8, 2, False, Col1OtherCharges, True, True, True)
            .AddAgTextColumn(Dgl1, Col1Remark, 150, 255, Col1Remark, False, True)
            .AddAgTextColumn(Dgl1, Col1SpecificationDocId, 40, 5, Col1SpecificationDocId, False, True, False)
            .AddAgTextColumn(Dgl1, Col1SpecificationDocIdSr, 40, 5, Col1SpecificationDocIdSr, False, True, False)
            .AddAgTextColumn(Dgl1, Col1CurrentBalance, 150, 255, Col1CurrentBalance, False, True)
            .AddAgDateColumn(Dgl1, Col1CancelDate, 110, Col1CancelDate, True, False)
            .AddAgNumberColumn(Dgl1, Col1AdditionalCharge, 90, 8, 2, False, Col1AdditionalCharge, True, False, True)
            .AddAgTextColumn(Dgl1, Col1AdditionalChargeAc, 130, 255, Col1AdditionalChargeAc, True, False)
            .AddAgTextColumn(Dgl1, Col1CancelRemark, 100, 255, Col1CancelRemark, True, False)
            .AddAgButtonColumn(Dgl1, Col1BtnDeleteCancellation, 25, " ", True, False)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 40
        Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        AgL.GridDesign(Dgl1)

        Dgl1.AllowUserToAddRows = False
        Dgl1.AgSkipReadOnlyColumns = True
        Dgl1.AllowUserToOrderColumns = True
        Dgl1.Columns(Col1Select).DefaultCellStyle.Font = New Font(New FontFamily("wingdings"), 14)

        Dgl1.Anchor = AnchorStyles.Bottom + AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Top
        Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        AgCL.GridSetiingShowXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1, False)

        AgCalcGrid1.Ini_Grid(mEntryNCat, mV_Date)

        AgCalcGrid1.AgLineGrid = Dgl1
        AgCalcGrid1.AgLineGridMandatoryColumn = Dgl1.Columns(Col1Subcode).Index
        AgCalcGrid1.AgLineGridGrossColumn = Dgl1.Columns(Col1Amount).Index
        AgCalcGrid1.AgLineGridPostingGroupSalesTaxProd = Dgl1.Columns(Col1SalesTaxGroup).Index
        AgCalcGrid1.AgPostingPartyAc = mParty
        AgCalcGrid1.Visible = False

        For I As Integer = 0 To Dgl1.Columns.Count - 1
            If Dgl1.Columns(I).Name <> Col1CancelDate And Dgl1.Columns(I).Name <> Col1AdditionalCharge And
                Dgl1.Columns(I).Name <> Col1AdditionalChargeAc And Dgl1.Columns(I).Name <> Col1CancelRemark Then
                Dgl1.Columns(I).ReadOnly = True
            End If
        Next

        'For I As Integer = 0 To Dgl1.Columns.Count - 1
        '    Dgl1.Columns(I).Visible = True
        'Next
    End Sub
    Private Sub FrmReconcile_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AgL.AddAgDataGrid(AgCalcGrid1, PnlCalcGrid)

        AgCalcGrid1.AgLibVar = AgL
        AgCalcGrid1.Visible = False

        Ini_Grid()
        MovRec()

        If CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Qty")), Boolean) = False Then
            LblTotalQty.Visible = False
            LblTotalQtyText.Visible = False
        Else
            LblTotalQty.Visible = True
            LblTotalQtyText.Visible = True
        End If

        mQry = "Select Vt.Category
                From LedgerHead H 
                LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                Where H.DocId = '" & mSearchCode & "'"
        If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar() = "RCT" Then
            mTransactionType = AgLibrary.ClsMain.agConstants.VoucherCategory.Receipt
        Else
            mTransactionType = AgLibrary.ClsMain.agConstants.VoucherCategory.Payment
        End If
    End Sub
    Private Sub MovRec()
        Dim mQry As String = ""
        Dim DsTemp As DataSet
        Dim I As Integer = 0


        mQry = " Select H.*, Sg.Name as AccountName, Sg.Nature, VT.Category as VoucherCategory, Vt.NCat, HC.*                                 
                From (Select * From LedgerHead  Where DocID='" & SearchCode & "') H 
                Left Join Voucher_Type Vt On H.V_Type = Vt.V_Type
                Left Join LedgerHeadCharges Hc on H.DocID = HC.DocID
                LEFT JOIN viewHelpSubgroup Sg  ON H.Subcode = Sg.Code "
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                AgCalcGrid1.AgStructure = AgL.XNull(.Rows(0)("Structure"))
                AgCalcGrid1.FMoveRecFooterTable(DsTemp.Tables(0), mEntryNCat, mV_Date)
                Ini_Grid()
            End If
        End With

        mQry = "Select L.*, Sg.Name as AccountName, Lsg.Name as LinkedAccountName, H.SubCode As HeaderSubCode, Sg1.Name As HeaderAccountName,
                U.DecimalPlaces, U.DecimalPlaces As QtyDecimalPlaces, 
                Case When Vt.NCat = '" & Ncat.Payment & "' Then 'Dr'
                     When Vt.NCat In ('" & Ncat.Receipt & "','" & Ncat.VisitReceipt & "') Then 'Cr'
                Else Null End As DrCr, TRef.DocId As CancelDocId, LC.* 
                From (Select * From LedgerHeadDetail  Where DocId = '" & SearchCode & "') As L 
                LEFT JOIN viewHelpSubgroup Sg  ON L.Subcode = Sg.Code 
                LEFT join viewHelpSubgroup Lsg ON L.LinkedSubcode = Lsg.Code
                Left Join Unit U On L.Unit = U.Code 
                Left Join LedgerHeadDetailCharges LC on L.DocID = LC.DocID And L.Sr = LC.Sr
                LEFT JOIN TransactionReferences TRef On L.DocId = TRef.ReferenceDocId And L.Sr = TRef.ReferenceSr
                LEFT JOIN LedgerHead H On L.DocId = H.DocId
                LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                LEFT JOIN viewHelpSubgroup Sg1 ON H.Subcode = Sg1.Code 
                Order By L.Sr "

        DsTemp = AgL.FillData(mQry, AgL.GCn)
        With DsTemp.Tables(0)
            Dgl1.RowCount = 1
            Dgl1.Rows.Clear()
            If .Rows.Count > 0 Then
                For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                    Dgl1.Rows.Add()
                    Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count
                    Dgl1.Item(ColSNo, I).Tag = AgL.XNull(.Rows(I)("Sr"))

                    If AgL.XNull(.Rows(I)("CancelDocId")) <> "" Then
                        Dgl1.Item(Col1Select, I).Value = "þ"
                        Dgl1.Rows(I).DefaultCellStyle.BackColor = ColorConstants.Cancelled
                        Dgl1.Rows(I).ReadOnly = True
                        Dgl1.Item(Col1BtnDeleteCancellation, I).Tag = AgL.XNull(.Rows(I)("CancelDocId"))
                    Else
                        Dgl1.Item(Col1Select, I).Value = "o"
                        Dgl1.Item(Col1BtnDeleteCancellation, I) = New DataGridViewTextBoxCell
                        Dgl1.Item(Col1BtnDeleteCancellation, I).ReadOnly = True

                    End If

                    Dgl1.Item(Col1Subcode, I).Tag = AgL.XNull(.Rows(I)("Subcode"))
                    Dgl1.Item(Col1Subcode, I).Value = AgL.XNull(.Rows(I)("AccountName"))
                    Dgl1.Item(Col1LinkedSubcode, I).Tag = AgL.XNull(.Rows(I)("LinkedSubcode"))
                    Dgl1.Item(Col1LinkedSubcode, I).Value = AgL.XNull(.Rows(I)("LinkedAccountName"))
                    Dgl1.Item(Col1DrCr, I).Value = AgL.XNull(.Rows(I)("DrCr"))
                    Dgl1.Item(Col1HeaderSubcode, I).Tag = AgL.XNull(.Rows(I)("HeaderSubcode"))
                    Dgl1.Item(Col1HeaderSubcode, I).Value = AgL.XNull(.Rows(I)("HeaderAccountName"))
                    Dgl1.Item(Col1Specification, I).Value = AgL.XNull(.Rows(I)("Specification"))
                    Dgl1.Item(Col1Specification, I).Tag = AgL.XNull(.Rows(I)("SpecificationDocId"))
                    Dgl1.Item(Col1SpecificationDocIdSr, I).Value = AgL.VNull(.Rows(I)("SpecificationDocIdSr"))
                    Dgl1.Item(Col1SalesTaxGroup, I).Tag = AgL.XNull(.Rows(I)("SalesTaxGroupItem"))
                    Dgl1.Item(Col1SalesTaxGroup, I).Value = AgL.XNull(.Rows(I)("SalesTaxGroupItem"))
                    Dgl1.Item(Col1QtyDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("QtyDecimalPlaces"))
                    Dgl1.Item(Col1Qty, I).Value = Format(Math.Abs(AgL.VNull(.Rows(I)("Qty"))), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                    Dgl1.Item(Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                    Dgl1.Item(Col1Rate, I).Value = AgL.VNull(.Rows(I)("Rate"))
                    Dgl1.Item(Col1Amount, I).Value = Format(AgL.VNull(.Rows(I)("Amount")), "0.00")
                    Dgl1.Item(Col1ChqRefNo, I).Value = AgL.XNull(.Rows(I)("ChqRefNo"))
                    Dgl1.Item(Col1ChqRefDate, I).Value = ClsMain.FormatDate(AgL.XNull(.Rows(I)("ChqRefDate")))
                    Dgl1.Item(Col1Deduction, I).Value = Format(AgL.VNull(.Rows(I)("Deduction")), "0.00")
                    Dgl1.Item(Col1OtherCharges, I).Value = Format(AgL.VNull(.Rows(I)("Other_Charge")), "0.00")
                    Dgl1.Item(Col1EffectiveDate, I).Value = ClsMain.FormatDate(AgL.XNull(.Rows(I)("EffectiveDate")))
                    Dgl1.Item(Col1Remark, I).Value = AgL.XNull(.Rows(I)("Remarks"))

                    Dgl1.Item(Col1CancelDate, I).Value = AgL.PubLoginDate



                    Call AgCalcGrid1.FMoveRecLineTable(DsTemp.Tables(0), I)

                    LblTotalQty.Text = Val(LblTotalQty.Text) + Val(Dgl1.Item(Col1Qty, I).Value)
                    LblTotalAmount.Text = Val(LblTotalAmount.Text) + Val(Dgl1.Item(Col1Amount, I).Value)
                Next I
            End If
        End With

        Dgl1.DefaultCellStyle.WrapMode = DataGridViewTriState.True
        Dgl1.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells)

        mQry = "Select SD.Code,SD.Sr,SD.Charges,SD.Charge_Type,SD.Value_Type,SD.Value, " &
               "SD.Calculation,SD.BaseColumn,SD.LineItem,SD.AffectCost, " &
               "SD.VisibleInMaster, SD.VisibleInMasterLine,SD.VisibleInTransactionLine, " &
               "SD.VisibleInTransactionFooter,SD.HeaderPerField,SD.HeaderAmtField,SD.LinePerField, " &
               "SD.LineAmtField, C.ManualCode, SD.PostAc, SD.DrCr,SD.PostAcFromColumn, " &
               "SD.GridDisplayIndex " &
               "From StructureDetail SD " &
               "Left Join Charges C On SD.Charges=C.Code " &
               "Where SD.Code = '" & AgCalcGrid1.AgStructure & "' " &
               "Order By SD.Sr"
        Dim DtTemp As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
        If DtTemp.Rows.Count > 0 Then
            With DtTemp
                If .Rows.Count > 0 Then
                    For I = 0 To .Rows.Count - 1
                        If AgL.XNull(.Rows(I)("DrCr")) = "Dr" Then
                            AgCalcGrid1.Item(8, I).Value = "Cr"
                        ElseIf AgL.XNull(.Rows(I)("DrCr")) = "Cr" Then
                            AgCalcGrid1.Item(8, I).Value = "Dr"
                        End If
                    Next
                End If
            End With
        End If
    End Sub
    Private Sub FrmImportPurchaseFromExcel_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub
    Private Sub Dgl1_ColumnDisplayIndexChanged(sender As Object, e As DataGridViewColumnEventArgs) Handles Dgl1.ColumnDisplayIndexChanged
        Try
            AgCL.GridSetiingWriteXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1)
        Catch ex As Exception
        End Try
    End Sub
    Private Sub Dgl1_ColumnWidthChanged(sender As Object, e As DataGridViewColumnEventArgs) Handles Dgl1.ColumnWidthChanged
        Try
            AgCL.GridSetiingWriteXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Dgl1_MouseUp(sender As Object, e As MouseEventArgs) Handles Dgl1.MouseUp
        Dim mRowIndex As Integer = Dgl1.CurrentCell.RowIndex
        Dim mColumnIndex As Integer = Dgl1.CurrentCell.ColumnIndex
        Try
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Select
                    If e.Button = Windows.Forms.MouseButtons.Left Then
                        If Dgl1.CurrentCell.ColumnIndex = Dgl1.Columns(Col1Select).Index Then
                            If Dgl1.Rows(mRowIndex).DefaultCellStyle.BackColor <> ColorConstants.Cancelled Then
                                ClsMain.FManageTick(Dgl1, Dgl1.CurrentCell.ColumnIndex, Dgl1.Columns(Col1Subcode).Index)
                            End If
                        End If
                    End If
            End Select
            Calculation()
        Catch ex As Exception
            MsgBox("System Exception : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
        End Try
    End Sub
    Private Sub Dgl1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.KeyDown
        If Dgl1.CurrentCell Is Nothing Then Exit Sub
        Dim mRowIndex As Integer = Dgl1.CurrentCell.RowIndex
        Dim mColumnIndex As Integer = Dgl1.CurrentCell.ColumnIndex
        Try
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Select
                    If e.KeyCode = Keys.Space Then
                        If Dgl1.Rows(mRowIndex).DefaultCellStyle.BackColor <> ColorConstants.Cancelled Then
                            ClsMain.FManageTick(Dgl1, Dgl1.CurrentCell.ColumnIndex, Dgl1.Columns(Col1Subcode).Index)
                        End If
                    End If
            End Select
            Calculation()
        Catch ex As Exception
            MsgBox("System Exception : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
        End Try
    End Sub

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, 0)
    End Sub

    Private Sub Calculation()
        Dim I As Integer

        LblTotalQty.Text = 0
        LblTotalAmount.Text = 0


        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1Subcode, I).Value <> "" Then

                If Dgl1.Item(Col1Rate, I).Value > 0 Then
                    Dgl1.Item(Col1Amount, I).Value = Format(Val(Dgl1.Item(Col1Qty, I).Value) * Val(Dgl1.Item(Col1Rate, I).Value), "0.".PadRight(CType(Dgl1.Columns(Col1Amount), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                End If

                'Footer Calculation
                Dim bQty As Double = 0
                bQty = Val(Dgl1.Item(Col1Qty, I).Value)

                LblTotalQty.Text = Val(LblTotalQty.Text) + bQty
                LblTotalAmount.Text = Val(LblTotalAmount.Text) + Val(Dgl1.Item(Col1Amount, I).Value)

            End If

            AgCalcGrid1.AgPostingPartyAc = Dgl1.Item(Col1Subcode, I).Tag
            'mQry = "Select SalesTaxPostinGroup From SubGroup Where SubCode = '" & Dgl1.Item(Col1Subcode, I).Tag & "'"
            'AgCalcGrid1.AgPostingGroupSalesTaxParty = BtnFillPartyDetail.Tag.Dgl1.Item(BtnFillPartyDetail.Tag.Col1Value, BtnFillPartyDetail.Tag.rowSalesTaxGroup).Value
            'AgCalcGrid1.AgPlaceOfSupply = BtnFillPartyDetail.Tag.Dgl1.Item(BtnFillPartyDetail.Tag.Col1Value, BtnFillPartyDetail.Tag.rowPlaceOfSupply).Value
        Next

        AgCalcGrid1.AgLineGrid = Dgl1
        AgCalcGrid1.AgLineGridMandatoryColumn = Dgl1.Columns(Col1Amount).Index
        AgCalcGrid1.AgLineGridGrossColumn = Dgl1.Columns(Col1Amount).Index
        If AgL.VNull(AgL.PubDtDivisionSiteSetting.Rows(0)("IsSalesTaxApplicable")) = True Then
            AgCalcGrid1.AgLineGridPostingGroupSalesTaxProd = Dgl1.Columns(Col1SalesTaxGroup).Index
        Else
            AgCalcGrid1.AgLineGridPostingGroupSalesTaxProd = -1
        End If


        'AgCalcGrid1.AgVoucherCategory = TxtVoucherCategory.Text.ToUpper
        AgCalcGrid1.Calculation()
        LblTotalQty.Text = Val(LblTotalQty.Text)
        LblTotalAmount.Text = Val(LblTotalAmount.Text)
    End Sub




    Private Sub FSave()
        Dim mTrans As String = ""
        Dim I As Integer = 0

        Dim bIsSelectedAll As Boolean = False
        For I = 0 To Dgl1.Rows.Count - 1
            If Dgl1.Item(Col1Select, I).Value = "þ" Then
                bIsSelectedAll = True
            End If
        Next

        If bIsSelectedAll = False Then
            MsgBox("No Record Selected...!", MsgBoxStyle.Information)
            Exit Sub
        End If

        For I = 0 To Dgl1.Rows.Count - 1
            If Dgl1.Item(Col1Select, I).Value = "þ" Then
                If Dgl1.Item(Col1CancelDate, I).Value = "" Or Dgl1.Item(Col1CancelDate, I).Value Is Nothing Then
                    MsgBox("Cancel Date is blank at row no." + Dgl1.Item(ColSNo, I).Value.ToString(), MsgBoxStyle.Information)
                    Exit Sub
                End If
            End If
        Next

        Try
            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = "Begin"


            For I = 0 To Dgl1.Rows.Count - 1
                If Dgl1.Item(Col1Select, I).Value = "þ" And Dgl1.Item(Col1BtnDeleteCancellation, I).Tag = "" Then
                    Dim mV_Type As String = "JV"
                    'Dim mDocId As String = AgL.GetDocId(mV_Type, CStr(0), CDate(Dgl1.Item(Col1CancelDate, I).Value), IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead), AgL.PubDivCode, AgL.PubSiteCode)
                    Dim mDocId As String = AgL.CreateDocId(AgL, "LedgerHead", mV_Type, CStr(0), CDate(Dgl1.Item(Col1CancelDate, I).Value), IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead), AgL.PubDivCode, AgL.PubSiteCode)
                    Dim mV_No As String = Val(AgL.DeCodeDocID(mDocId, AgLibrary.ClsMain.DocIdPart.VoucherNo))
                    Dim mV_Prefix As String = AgL.DeCodeDocID(mDocId, AgLibrary.ClsMain.DocIdPart.VoucherPrefix)
                    Dim mDivCode As String = AgL.DeCodeDocID(mDocId, AgLibrary.ClsMain.DocIdPart.Division)
                    Dim mSiteCode As String = AgL.DeCodeDocID(mDocId, AgLibrary.ClsMain.DocIdPart.Site)
                    Dim mRefVType As String = AgL.DeCodeDocID(mDocId, AgLibrary.ClsMain.DocIdPart.VoucherType)
                    mQry = "Select Category,NCat From Voucher_Type Where V_Type = '" & mV_Type & "'"
                    Dim dtVType As DataTable
                    dtVType = AgL.FillData(mQry, IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).Tables(0)

                    Dim mPostingAcDeductions As String = ClsMain.FGetSettings(SettingFields.PostingAcDeductions, SettingType.General, mDivCode, mSiteCode, dtVType.Rows(0)("Category"), dtVType.Rows(0)("NCAT"), mRefVType, "", "")
                    Dim mPostingAcOtherCharges As String = ClsMain.FGetSettings(SettingFields.PostingAcDeductions, SettingType.General, mDivCode, mSiteCode, dtVType.Rows(0)("Category"), dtVType.Rows(0)("NCAT"), mRefVType, "", "")

                    mQry = "Insert Into LedgerM(DocId,V_Type,v_Prefix,Site_Code, Div_Code,V_No,V_Date,SubCode," &
                            "Narration,PostedBy,RecId," &
                            "U_Name,U_EntDt,U_AE,PreparedBy) Values " &
                            "('" & (mDocId) & "','" & mV_Type & "','" & mV_Prefix & "','" & AgL.PubSiteCode & "', '" & AgL.PubDivCode & "', " &
                            "'" & mV_No & "'," & AgL.Chk_Date(CDate(Dgl1.Item(Col1CancelDate, I).Value).ToString("u")) & ",Null, " &
                            "Null,'" & AgL.PubUserName & "','" & mV_No & "'," &
                            "'" & AgL.PubUserName & "','" & Format(AgL.PubLoginDate, "Short Date") & "'," &
                            "'A','" & AgL.PubUserName & "')"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                    mQry = "Insert Into LedgerHead(DocId,V_Type,v_Prefix,Site_Code, Div_Code,V_No,V_Date,SubCode," &
                            "Remarks,ManualRefNo," &
                            "EntryBy,EntryDate) Values " &
                            "('" & (mDocId) & "','" & mV_Type & "','" & mV_Prefix & "','" & AgL.PubSiteCode & "', '" & AgL.PubDivCode & "', " &
                            "'" & mV_No & "'," & AgL.Chk_Date(CDate(Dgl1.Item(Col1CancelDate, I).Value).ToString("u")) & ",Null, " &
                            "Null,'" & mV_No & "'," &
                            "'" & AgL.PubUserName & "'," & AgL.Chk_Date(Format(AgL.PubLoginDate, "Short Date")) & ")"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)




                    Dim bNarration As String = " Cancelled "
                    If Dgl1.Item(Col1ChqRefNo, I).Value <> "" Then
                        bNarration = "Chq No : " & Dgl1.Item(Col1ChqRefNo, I).Value + ","
                    End If
                    If Dgl1.Item(Col1ChqRefDate, I).Value <> "" Then
                        bNarration += "Chq Date : " & Dgl1.Item(Col1ChqRefDate, I).Value + ","
                    End If
                    If Dgl1.Item(Col1CancelRemark, I).Value <> "" Then
                        bNarration += Dgl1.Item(Col1CancelRemark, I).Value
                    End If

                    'If AgCalcGrid1.AgStructure = "" Then
                    Dim bDebit_Amount As Double = 0
                    Dim bCredit_Amount As Double = 0
                    Dim bDebit_Deduction As Double = 0
                    Dim bCredit_Deduction As Double = 0
                    Dim bDebit_OtherCharges As Double = 0
                    Dim bCredit_OtherCharges As Double = 0


                    Dim mSrl As Integer = AgL.Dman_Execute(" Select IfNull(Max(V_SNo),0) From Ledger With (NoLock)
                        Where DocId = '" & mDocId & "'", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()

                    If Dgl1.Item(Col1DrCr, I).Value = "Dr" Then
                        bDebit_Amount = 0
                        bCredit_Amount = Val(Dgl1.Item(Col1Amount, I).Value)
                        bDebit_Deduction = 0
                        bCredit_Deduction = Val(Dgl1.Item(Col1Deduction, I).Value)
                        bDebit_OtherCharges = Val(Dgl1.Item(Col1OtherCharges, I).Value)
                        bCredit_OtherCharges = 0
                    ElseIf Dgl1.Item(Col1DrCr, I).Value = "Cr" Then
                        bDebit_Amount = Val(Dgl1.Item(Col1Amount, I).Value)
                        bCredit_Amount = 0
                        bDebit_Deduction = Val(Dgl1.Item(Col1Deduction, I).Value)
                        bCredit_Deduction = 0
                        bDebit_OtherCharges = 0
                        bCredit_OtherCharges = Val(Dgl1.Item(Col1OtherCharges, I).Value)
                    End If

                    mSrl += 1
                    mQry = "Insert Into LedgerHeadDetail(DocId,Sr, SubCode, LinkedSubcode,Amount,AmountCr," &
                                " Remarks) Values " &
                                " ('" & mDocId & "'," & mSrl & ",
                                " & AgL.Chk_Text(Dgl1.Item(Col1Subcode, I).Tag) & "," & AgL.Chk_Text(Dgl1.Item(Col1LinkedSubcode, I).Tag) & ", " &
                                " " & bDebit_Amount & "," & bCredit_Amount & ", " &
                                " " & AgL.Chk_Text(bNarration) & ")"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)


                    mQry = "Insert Into Ledger(DocId,RecId,V_SNo, TSr,V_Date,SubCode, LinkedSubcode,ContraSub,AmtDr,AmtCr," &
                                " Narration,V_Type,V_No,V_Prefix,Site_Code,DivCode,Chq_No,Chq_Date,TDSCategory,TDSOnAmt,TDSDesc," &
                                " TDSPer,TDS_Of_V_SNo,System_Generated,FormulaString) Values " &
                                " ('" & mDocId & "','" & mV_No & "'," & mSrl & ", " & AgL.Chk_Text(Dgl1.Item(ColSNo, I).Tag) & "," & AgL.Chk_Text(CDate(Dgl1.Item(Col1CancelDate, I).Value).ToString("s")) & ",
                                " & AgL.Chk_Text(Dgl1.Item(Col1Subcode, I).Tag) & "," & AgL.Chk_Text(Dgl1.Item(Col1LinkedSubcode, I).Tag) & "," & AgL.Chk_Text(Dgl1.Item(Col1HeaderSubcode, I).Tag) & ", " &
                                " " & bDebit_Amount & "," & bCredit_Amount & ", " &
                                " " & AgL.Chk_Text(bNarration) & ",'" & mV_Type & "','" & mV_No & "','" & mV_Prefix & "'," &
                                " '" & AgL.PubSiteCode & "','" & AgL.PubDivCode & "'," & AgL.Chk_Text("") & "," &
                                " " & AgL.Chk_Text("") & "," & AgL.Chk_Text("") & "," &
                                " " & Val("") & "," & AgL.Chk_Text("") & "," & Val("") & "," & 0 & ",'N','" & "" & "')"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)


                    mQry = "INSERT INTO TransactionReferences(DocId, DocIdSr, ReferenceDocId, ReferenceSr, Remark,Type) 
                            Select '" & mDocId & "', " & mSrl & ", '" & mSearchCode & "', '" & Dgl1.Item(ColSNo, I).Tag & "',
                            " & AgL.Chk_Text(Dgl1.Item(Col1Remark, I).Tag) & ", " & AgL.Chk_Text(TransactionReferenceTypeConstants.Cancelled) & ""
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)


                    mSrl += 1

                    mQry = "Insert Into LedgerHeadDetail(DocId,Sr, SubCode, LinkedSubcode,Amount,AmountCr," &
                                " Remarks) Values " &
                                " ('" & mDocId & "'," & mSrl & ",
                                " & AgL.Chk_Text(Dgl1.Item(Col1HeaderSubcode, I).Tag) & ",Null, " &
                                " " & bCredit_Amount & "," & bDebit_Amount & ", " &
                                " " & AgL.Chk_Text(bNarration) & ")"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)


                    mQry = "Insert Into Ledger(DocId,RecId,V_SNo, TSr,V_Date,SubCode, ContraSub,AmtDr,AmtCr," &
                                " Narration,V_Type,V_No,V_Prefix,Site_Code,DivCode,Chq_No,Chq_Date,TDSCategory,TDSOnAmt,TDSDesc," &
                                " TDSPer,TDS_Of_V_SNo,System_Generated,FormulaString) Values " &
                                " ('" & mDocId & "','" & mV_No & "'," & mSrl & ", " & AgL.Chk_Text(Dgl1.Item(ColSNo, I).Tag) & "," & AgL.Chk_Text(CDate(Dgl1.Item(Col1CancelDate, I).Value).ToString("s")) & ",
                                " & AgL.Chk_Text(Dgl1.Item(Col1HeaderSubcode, I).Tag) & "," & AgL.Chk_Text(Dgl1.Item(Col1Subcode, I).Tag) & ", " &
                                " " & bCredit_Amount & "," & bDebit_Amount & ", " &
                                " " & AgL.Chk_Text(bNarration) & ",'" & mV_Type & "','" & mV_No & "','" & mV_Prefix & "'," &
                                " '" & AgL.PubSiteCode & "','" & AgL.PubDivCode & "'," & AgL.Chk_Text("") & "," &
                                " " & AgL.Chk_Text("") & "," & AgL.Chk_Text("") & "," &
                                " " & Val("") & "," & AgL.Chk_Text("") & "," & Val("") & "," & 0 & ",'N','" & "" & "')"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)




                    If Val(Dgl1.Item(Col1Deduction, I).Value) > 0 Then
                        mSrl += 1
                        mQry = "Insert Into LedgerHeadDetail(DocId,Sr, SubCode, LinkedSubcode,Amount,AmountCr," &
                                " Remarks) Values " &
                                " ('" & mDocId & "'," & mSrl & ",
                                " & AgL.Chk_Text(Dgl1.Item(Col1Subcode, I).Tag) & "," & AgL.Chk_Text(Dgl1.Item(Col1LinkedSubcode, I).Tag) & ", " &
                                " " & bDebit_Deduction & "," & bCredit_Deduction & ", " &
                                " " & AgL.Chk_Text(bNarration) & ")"
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)


                        mQry = "Insert Into Ledger(DocId,RecId,V_SNo, TSr,V_Date,SubCode, LinkedSubcode,ContraSub,AmtDr,AmtCr," &
                                " Narration,V_Type,V_No,V_Prefix,Site_Code,DivCode,Chq_No,Chq_Date,TDSCategory,TDSOnAmt,TDSDesc," &
                                " TDSPer,TDS_Of_V_SNo,System_Generated,FormulaString) Values " &
                                " ('" & mDocId & "','" & mV_No & "'," & mSrl & ", " & AgL.Chk_Text(Dgl1.Item(ColSNo, I).Tag) & "," & AgL.Chk_Text(CDate(Dgl1.Item(Col1CancelDate, I).Value).ToString("s")) & ",
                                " & AgL.Chk_Text(Dgl1.Item(Col1Subcode, I).Tag) & "," & AgL.Chk_Text(Dgl1.Item(Col1LinkedSubcode, I).Tag) & "," & AgL.Chk_Text(mPostingAcDeductions) & ", " &
                                " " & bDebit_Deduction & "," & bCredit_Deduction & ", " &
                                " " & AgL.Chk_Text(bNarration) & ",'" & mV_Type & "','" & mV_No & "','" & mV_Prefix & "'," &
                                " '" & AgL.PubSiteCode & "','" & AgL.PubDivCode & "'," & AgL.Chk_Text("") & "," &
                                " " & AgL.Chk_Text("") & "," & AgL.Chk_Text("") & "," &
                                " " & Val("") & "," & AgL.Chk_Text("") & "," & Val("") & "," & 0 & ",'N','" & "" & "')"
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)




                        mSrl += 1

                        mQry = "Insert Into LedgerHeadDetail(DocId,Sr, SubCode, LinkedSubcode,Amount,AmountCr," &
                                " Remarks) Values " &
                                " ('" & mDocId & "'," & mSrl & ",
                                " & AgL.Chk_Text(mPostingAcDeductions) & ",Null, " &
                                " " & bCredit_Deduction & "," & bDebit_Deduction & ", " &
                                " " & AgL.Chk_Text(bNarration) & ")"
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)


                        mQry = "Insert Into Ledger(DocId,RecId,V_SNo, TSr,V_Date,SubCode, ContraSub,AmtDr,AmtCr," &
                                " Narration,V_Type,V_No,V_Prefix,Site_Code,DivCode,Chq_No,Chq_Date,TDSCategory,TDSOnAmt,TDSDesc," &
                                " TDSPer,TDS_Of_V_SNo,System_Generated,FormulaString) Values " &
                                " ('" & mDocId & "','" & mV_No & "'," & mSrl & ", " & AgL.Chk_Text(Dgl1.Item(ColSNo, I).Tag) & "," & AgL.Chk_Text(CDate(Dgl1.Item(Col1CancelDate, I).Value).ToString("s")) & ",
                                " & AgL.Chk_Text(mPostingAcDeductions) & "," & AgL.Chk_Text(Dgl1.Item(Col1Subcode, I).Tag) & ", " &
                                " " & bCredit_Deduction & "," & bDebit_Deduction & ", " &
                                " " & AgL.Chk_Text(bNarration) & ",'" & mV_Type & "','" & mV_No & "','" & mV_Prefix & "'," &
                                " '" & AgL.PubSiteCode & "','" & AgL.PubDivCode & "'," & AgL.Chk_Text("") & "," &
                                " " & AgL.Chk_Text("") & "," & AgL.Chk_Text("") & "," &
                                " " & Val("") & "," & AgL.Chk_Text("") & "," & Val("") & "," & 0 & ",'N','" & "" & "')"
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    End If





                    If Val(Dgl1.Item(Col1OtherCharges, I).Value) > 0 Then
                        mSrl += 1
                        mQry = "Insert Into LedgerHeadDetail(DocId,Sr, SubCode, LinkedSubcode,Amount,AmountCr," &
                                " Remarks) Values " &
                                " ('" & mDocId & "'," & mSrl & ",
                                " & AgL.Chk_Text(Dgl1.Item(Col1Subcode, I).Tag) & "," & AgL.Chk_Text(Dgl1.Item(Col1LinkedSubcode, I).Tag) & ", " &
                                " " & bDebit_OtherCharges & "," & bCredit_OtherCharges & ", " &
                                " " & AgL.Chk_Text(bNarration) & ")"
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)


                        mQry = "Insert Into Ledger(DocId,RecId,V_SNo, TSr,V_Date,SubCode, LinkedSubcode,ContraSub,AmtDr,AmtCr," &
                                " Narration,V_Type,V_No,V_Prefix,Site_Code,DivCode,Chq_No,Chq_Date,TDSCategory,TDSOnAmt,TDSDesc," &
                                " TDSPer,TDS_Of_V_SNo,System_Generated,FormulaString) Values " &
                                " ('" & mDocId & "','" & mV_No & "'," & mSrl & ", " & AgL.Chk_Text(Dgl1.Item(ColSNo, I).Tag) & "," & AgL.Chk_Text(CDate(Dgl1.Item(Col1CancelDate, I).Value).ToString("s")) & ",
                                " & AgL.Chk_Text(Dgl1.Item(Col1Subcode, I).Tag) & "," & AgL.Chk_Text(Dgl1.Item(Col1LinkedSubcode, I).Tag) & "," & AgL.Chk_Text(mPostingAcOtherCharges) & ", " &
                                " " & bDebit_OtherCharges & "," & bCredit_OtherCharges & ", " &
                                " " & AgL.Chk_Text(bNarration) & ",'" & mV_Type & "','" & mV_No & "','" & mV_Prefix & "'," &
                                " '" & AgL.PubSiteCode & "','" & AgL.PubDivCode & "'," & AgL.Chk_Text("") & "," &
                                " " & AgL.Chk_Text("") & "," & AgL.Chk_Text("") & "," &
                                " " & Val("") & "," & AgL.Chk_Text("") & "," & Val("") & "," & 0 & ",'N','" & "" & "')"
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)




                        mSrl += 1

                        mQry = "Insert Into LedgerHeadDetail(DocId,Sr, SubCode, LinkedSubcode,Amount,AmountCr," &
                                " Remarks) Values " &
                                " ('" & mDocId & "'," & mSrl & ",
                                " & AgL.Chk_Text(mPostingAcOtherCharges) & ",Null, " &
                                " " & bCredit_OtherCharges & "," & bDebit_OtherCharges & ", " &
                                " " & AgL.Chk_Text(bNarration) & ")"
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)


                        mQry = "Insert Into Ledger(DocId,RecId,V_SNo, TSr,V_Date,SubCode, ContraSub,AmtDr,AmtCr," &
                                " Narration,V_Type,V_No,V_Prefix,Site_Code,DivCode,Chq_No,Chq_Date,TDSCategory,TDSOnAmt,TDSDesc," &
                                " TDSPer,TDS_Of_V_SNo,System_Generated,FormulaString) Values " &
                                " ('" & mDocId & "','" & mV_No & "'," & mSrl & ", " & AgL.Chk_Text(Dgl1.Item(ColSNo, I).Tag) & "," & AgL.Chk_Text(CDate(Dgl1.Item(Col1CancelDate, I).Value).ToString("s")) & ",
                                " & AgL.Chk_Text(mPostingAcOtherCharges) & "," & AgL.Chk_Text(Dgl1.Item(Col1Subcode, I).Tag) & ", " &
                                " " & bCredit_OtherCharges & "," & bDebit_OtherCharges & ", " &
                                " " & AgL.Chk_Text(bNarration) & ",'" & mV_Type & "','" & mV_No & "','" & mV_Prefix & "'," &
                                " '" & AgL.PubSiteCode & "','" & AgL.PubDivCode & "'," & AgL.Chk_Text("") & "," &
                                " " & AgL.Chk_Text("") & "," & AgL.Chk_Text("") & "," &
                                " " & Val("") & "," & AgL.Chk_Text("") & "," & Val("") & "," & 0 & ",'N','" & "" & "')"
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    End If


                    'For Additional Charges
                    If Val(Dgl1.Item(Col1AdditionalCharge, I).Value) > 0 Then
                            If Dgl1.Item(Col1DrCr, I).Value = "Dr" Then
                                bDebit_Amount = 0
                                bCredit_Amount = Val(Dgl1.Item(Col1AdditionalCharge, I).Value)
                            ElseIf Dgl1.Item(Col1DrCr, I).Value = "Cr" Then
                                bDebit_Amount = Val(Dgl1.Item(Col1AdditionalCharge, I).Value)
                                bCredit_Amount = 0
                            End If

                        mSrl += 1

                        mQry = "Insert Into LedgerHeadDetail(DocId,Sr, SubCode, LinkedSubcode,Amount,AmountCr," &
                                " Remarks) Values " &
                                " ('" & mDocId & "'," & mSrl & ",
                                " & AgL.Chk_Text(Dgl1.Item(Col1Subcode, I).Tag) & "," & AgL.Chk_Text(Dgl1.Item(Col1LinkedSubcode, I).Tag) & ", " &
                                " " & bDebit_Amount & "," & bCredit_Amount & ", " &
                                " " & AgL.Chk_Text(bNarration) & ")"
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                        mQry = "Insert Into Ledger(DocId,RecId,V_SNo,V_Date,SubCode, LinkedSubcode,ContraSub,AmtDr,AmtCr," &
                                " Narration,V_Type,V_No,V_Prefix,Site_Code,DivCode,Chq_No,Chq_Date,TDSCategory,TDSOnAmt,TDSDesc," &
                                " TDSPer,TDS_Of_V_SNo,System_Generated,FormulaString) Values " &
                                " ('" & mDocId & "','" & mV_No & "'," & mSrl & "," & AgL.Chk_Text(CDate(Dgl1.Item(Col1CancelDate, I).Value).ToString("s")) & ",
                                " & AgL.Chk_Text(Dgl1.Item(Col1Subcode, I).Tag) & "," & AgL.Chk_Text(Dgl1.Item(Col1LinkedSubcode, I).Tag) & "," & AgL.Chk_Text(Dgl1.Item(Col1AdditionalChargeAc, I).Tag) & ", " &
                                " " & bDebit_Amount & "," & bCredit_Amount & ", " &
                                " " & AgL.Chk_Text(bNarration) & ",'" & mV_Type & "','" & mV_No & "','" & mV_Prefix & "'," &
                                " '" & AgL.PubSiteCode & "','" & AgL.PubDivCode & "'," & AgL.Chk_Text("") & "," &
                                " " & AgL.Chk_Text("") & "," & AgL.Chk_Text("") & "," &
                                " " & Val("") & "," & AgL.Chk_Text("") & "," & Val("") & "," & 0 & ",'N','" & "" & "')"
                            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                        mSrl += 1
                        mQry = "Insert Into LedgerHeadDetail(DocId,Sr, SubCode, LinkedSubcode,Amount,AmountCr," &
                                " Remarks) Values " &
                                " ('" & mDocId & "'," & mSrl & ",
                                " & AgL.Chk_Text(Dgl1.Item(Col1AdditionalChargeAc, I).Tag) & ",Null, " &
                                " " & bCredit_Amount & "," & bDebit_Amount & ", " &
                                " " & AgL.Chk_Text(bNarration) & ")"
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                        mQry = "Insert Into Ledger(DocId,RecId,V_SNo,V_Date,SubCode,ContraSub,AmtDr,AmtCr," &
                                " Narration,V_Type,V_No,V_Prefix,Site_Code,DivCode,Chq_No,Chq_Date,TDSCategory,TDSOnAmt,TDSDesc," &
                                " TDSPer,TDS_Of_V_SNo,System_Generated,FormulaString) Values " &
                                " ('" & mDocId & "','" & mV_No & "'," & mSrl & "," & AgL.Chk_Text(CDate(Dgl1.Item(Col1CancelDate, I).Value).ToString("s")) & ",
                                " & AgL.Chk_Text(Dgl1.Item(Col1AdditionalChargeAc, I).Tag) & "," & AgL.Chk_Text(Dgl1.Item(Col1Subcode, I).Tag) & ", " &
                                " " & bCredit_Amount & "," & bDebit_Amount & ", " &
                                " " & AgL.Chk_Text(bNarration) & ",'" & mV_Type & "','" & mV_No & "','" & mV_Prefix & "'," &
                                " '" & AgL.PubSiteCode & "','" & AgL.PubDivCode & "'," & AgL.Chk_Text("") & "," &
                                " " & AgL.Chk_Text("") & "," & AgL.Chk_Text("") & "," &
                                " " & Val("") & "," & AgL.Chk_Text("") & "," & Val("") & "," & 0 & ",'N','" & "" & "')"
                            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                        End If
                    'Else
                    'PostStructureLineToAccounts(I, AgCalcGrid1, bNarration, bNarration, mDocId, AgL.PubDivCode, AgL.PubSiteCode, AgL.PubDivCode, mV_Type, mV_Prefix, mV_No, mV_No, mParty, Dgl1.Item(Col1CancelDate, I).Value, AgL.GCn, AgL.ECmd)


                    'End If


                    If AgL.VNull(AgL.Dman_Execute(" Select IfNull(Round(Sum(L.AmtDr),2),0) - IfNull(Round(Sum(L.AmtCr),2),0) As Diff
                            From Ledger L With (NoLock)
                            Where DocId = '" & mDocId & "'", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()) > 0 Then
                        Err.Raise(1, "", "Debit And Credit Value is not matched.")
                    End If


                    AgL.UpdateVoucherCounter(mDocId, CDate(Dgl1.Item(Col1CancelDate, I).Value), AgL.GCn, AgL.ECmd, AgL.PubDivCode, AgL.PubSiteCode)


                    'mQry = "Update voucher_prefix set start_srl_no = " & Val(mV_No) & " 
                    'where v_type = " & AgL.Chk_Text(mV_Type) & " and prefix=" & AgL.Chk_Text(mV_Prefix) & ""
                    'AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                    Call AgL.LogTableEntry(mSearchCode, Me.Text, "A", AgL.PubMachineName, AgL.PubUserName, AgL.GetDateTime(AgL.GcnRead), AgL.GCn, AgL.ECmd, , Dgl1.Item(Col1CancelDate, I).Value,,,, AgL.PubSiteCode, AgL.PubDivCode, "", mV_Type, mV_No)
                End If
            Next

            AgL.ETrans.Commit()
            mTrans = "Commit"

            MsgBox("Cancellation Done...!", MsgBoxStyle.Information)
            Me.Close()
        Catch ex As Exception
            AgL.ETrans.Rollback()
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub BtnOk_Click(sender As Object, e As EventArgs) Handles BtnOk.Click, BtnCancel.Click
        Select Case sender.name
            Case BtnOk.Name
                FSave()

            Case BtnCancel.Name
                Me.Close()
        End Select
    End Sub

    Public Shared Sub FPrepareContraText(ByVal BlnOverWrite As Boolean, ByRef StrContraTextVar As String,
                                       ByVal StrContraName As String, ByVal DblAmount As Double, ByVal StrDrCr As String)
        Dim IntNameMaxLen As Integer = 35, IntAmtMaxLen As Integer = 18, IntSpaceNeeded As Integer = 2
        StrContraName = AgL.XNull(AgL.Dman_Execute("Select Name from Subgroup  Where SubCode = '" & StrContraName & "'  ", AgL.GcnRead).ExecuteScalar)

        If BlnOverWrite Then
            StrContraTextVar = Mid(Trim(StrContraName), 1, IntNameMaxLen) & Space((IntNameMaxLen + IntSpaceNeeded) - Len(Mid(Trim(StrContraName), 1, IntNameMaxLen))) & Space(IntAmtMaxLen - Len(Format(Val(DblAmount), "##,##,##,##,##0.00"))) & Format(Val(DblAmount), "##,##,##,##,##0.00") & " " & Trim(StrDrCr)
        Else
            StrContraTextVar += Mid(Trim(StrContraName), 1, IntNameMaxLen) & Space((IntNameMaxLen + IntSpaceNeeded) - Len(Mid(Trim(StrContraName), 1, IntNameMaxLen))) & Space(IntAmtMaxLen - Len(Format(Val(DblAmount), "##,##,##,##,##0.00"))) & Format(Val(DblAmount), "##,##,##,##,##0.00") & " " & Trim(StrDrCr)
        End If
    End Sub
    'Private Sub FGetPostingData()
    '    Dim J As Integer = 0, I As Integer = 0
    '    For J = 0 To Dgl1.Rows.Count - 1
    '        If Dgl1.Item(Col1Select, J).Value = "þ" Then
    '            If Dgl1.Item(Col1CancelDate, J).Value = "" Or Dgl1.Item(Col1CancelDate, J).Value Is Nothing Then
    '                MsgBox("Cancel Date is blank at row no." + Dgl1.Item(ColSNo, J).Value.ToString(), MsgBoxStyle.Information)
    '                Exit Sub
    '            End If
    '            FSave(J)
    '        End If
    '    Next

    '    MsgBox("Cancellation Done...!", MsgBoxStyle.Information)
    '    Me.Close()
    'End Sub

    Private Sub Dgl1_EditingControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.EditingControl_KeyDown
        Try
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1AdditionalChargeAc
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) Is Nothing Then
                            mQry = "Select SubCode as Code, Name as Name From SubGroup "
                            Dgl1.AgHelpDataSet(Col1AdditionalChargeAc) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    'Public Sub PostStructureLineToAccounts(ByVal J As Integer, ByVal FGMain As AgStructure.AgCalcGrid, ByVal mNarrParty As String, ByVal mNarr As String, ByVal mDocID As String, ByVal mDiv_Code As String,
    '                                           ByVal mSite_Code As String, ByVal Div_Code As String, ByVal mV_Type As String, ByVal mV_Prefix As String, ByVal mV_No As Integer,
    '                                           ByVal mRecID As String, ByVal PostingPartyAc As String, ByVal mV_Date As String,
    '                                           ByVal Conn As Object, ByVal Cmd As Object, Optional ByVal mCostCenter As String = "", Optional MultiplyWithMinus As Boolean = False)
    '    Dim StrContraTextJV As String = ""
    '    Dim mPostSubCode = ""
    '    Dim I As Integer
    '    Dim mQry$ = "", bSelectionQry$ = ""
    '    Dim DtTemp As DataTable = Nothing

    '    bSelectionQry = ""
    '    For I = 0 To FGMain.Rows.Count - 1
    '        If AgL.XNull(FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.PostAc)) <> "" Then
    '            If bSelectionQry <> "" Then bSelectionQry += " UNION ALL "

    '            bSelectionQry += " Select 1 as TmpCol, '" & FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.PostAc) & "' As PostAc, " &
    '            " Case When " & AgL.Chk_Text(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_DrCr, I).Value) & " = 'Dr' Then " & Val(FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.Amount)) * 1.0 & "  " &
    '            "      When " & AgL.Chk_Text(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_DrCr, I).Value) & " = 'Cr' Then " & -Val(FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.Amount)) * 1.0 & " End As Amount "
    '        ElseIf Trim(AgL.XNull(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_PostAc, I).Value)) <> "" Then
    '            If bSelectionQry <> "" Then bSelectionQry += " UNION ALL "

    '            bSelectionQry += " Select 1 as TmpCol,'" & FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_PostAc, I).Value & "' As PostAc, " &
    '                " Case When " & AgL.Chk_Text(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_DrCr, I).Value) & " = 'Dr' Then " & Val(FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.Amount)) * 1.0 & "  " &
    '                "      When " & AgL.Chk_Text(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_DrCr, I).Value) & " = 'Cr' Then " & -Val(FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.Amount)) * 1.0 & " End As Amount "

    '        End If
    '    Next

    '    'If Dgl1.Item(Col1AdditionalCharge, J).Value > 0 And Dgl1.Item(Col1AdditionalChargeAc, J).Value <> "" And
    '    '        Dgl1.Item(Col1AdditionalChargeAc, J).Value IsNot Nothing Then

    '    '    If bSelectionQry <> "" Then bSelectionQry += " UNION ALL "
    '    '    bSelectionQry += " Select 1 as TmpCol,'" & Dgl1.Item(Col1Subcode, J).Tag & "'  As PostAc, " &
    '    '        " Case When '" & mTransactionType & "' = '" & AgLibrary.ClsMain.agConstants.VoucherCategory.Receipt & "' Then " & Val(Dgl1.Item(Col1AdditionalCharge, J).Value) * 1.0 & "  " &
    '    '        "      When '" & mTransactionType & "' = '" & AgLibrary.ClsMain.agConstants.VoucherCategory.Payment & "' Then " & -Val(Dgl1.Item(Col1AdditionalCharge, J).Value) * 1.0 & " End As Amount "

    '    '    If bSelectionQry <> "" Then bSelectionQry += " UNION ALL "
    '    '    bSelectionQry += " Select 1 as TmpCol,'" & Dgl1.Item(Col1AdditionalChargeAc, J).Tag & "'  As PostAc, " &
    '    '        " Case When '" & mTransactionType & "' = '" & AgLibrary.ClsMain.agConstants.VoucherCategory.Receipt & "' Then " & -Val(Dgl1.Item(Col1AdditionalCharge, J).Value) * 1.0 & "  " &
    '    '        "      When '" & mTransactionType & "' = '" & AgLibrary.ClsMain.agConstants.VoucherCategory.Payment & "' Then " & Val(Dgl1.Item(Col1AdditionalCharge, J).Value) * 1.0 & " End As Amount "
    '    'End If


    '    If bSelectionQry = "" Then Exit Sub


    '    mQry = " Select Count(*)  " &
    '            " From (" & bSelectionQry & ") As V1 Group by tmpCol " &
    '            " Having Round(Sum(Case When IfNull(V1.Amount*1.0,0) > 0 Then IfNull(V1.Amount*1.0,0) Else 0 End),3) <> Round(abs(Sum(Case When IfNull(V1.Amount*1.0,0) < 0 Then IfNull(V1.Amount*1.0,0) Else 0 End)),3)  "
    '    DtTemp = AgL.FillData(mQry, AgL.GcnRead).Tables(0)
    '    If DtTemp.Rows.Count > 0 Then
    '        If AgL.VNull(DtTemp.Rows(0)(0)) > 0 Then
    '            Console.Write(mQry)
    '            Err.Raise(1, , "Error In Ledger Posting. Debit and Credit balances are not equal.")
    '        End If
    '    End If


    '    If MultiplyWithMinus Then
    '        mQry = " Select V1.PostAc, IfNull(Sum(Cast(V1.Amount as Float)),0) As Amount, " &
    '            " Case When IfNull(Sum(V1.Amount),0) > 0 Then 'Cr' " &
    '            "      When IfNull(Sum(V1.Amount),0) < 0 Then 'Dr' End As DrCr " &
    '            " From (" & bSelectionQry & ") As V1 " &
    '            " Group BY V1.PostAc "
    '    Else
    '        mQry = " Select V1.PostAc, IfNull(Sum(Cast(V1.Amount as Float)),0) As Amount, " &
    '            " Case When IfNull(Sum(V1.Amount),0) > 0 Then 'Dr' " &
    '            "      When IfNull(Sum(V1.Amount),0) < 0 Then 'Cr' End As DrCr " &
    '            " From (" & bSelectionQry & ") As V1 " &
    '            " Group BY V1.PostAc "
    '    End If

    '    DtTemp = AgL.FillData(mQry, AgL.GcnRead).Tables(0)

    '    With DtTemp
    '        For I = 0 To .Rows.Count - 1
    '            If Trim(AgL.XNull(.Rows(I)("PostAc"))) <> "" Then
    '                If AgL.StrCmp(AgL.XNull(.Rows(I)("PostAc")), "|PARTY|") Then
    '                    If AgL.VNull(.Rows(I)("Amount")) <> 0 And AgL.XNull(.Rows(I)("DrCr")) <> "" Then
    '                        If StrContraTextJV <> "" Then StrContraTextJV += vbCrLf
    '                        FPrepareContraText(False, StrContraTextJV, PostingPartyAc, Math.Abs(AgL.VNull(.Rows(I)("Amount"))), AgL.XNull(.Rows(I)("DrCr")))
    '                    End If
    '                Else
    '                    If AgL.VNull(.Rows(I)("Amount")) <> 0 And AgL.XNull(.Rows(I)("DrCr")) <> "" Then
    '                        If StrContraTextJV <> "" Then StrContraTextJV += vbCrLf
    '                        FPrepareContraText(False, StrContraTextJV, AgL.XNull(.Rows(I)("PostAc")), Math.Abs(Val(AgL.VNull(.Rows(I)("Amount")))), AgL.XNull(.Rows(I)("DrCr")))
    '                    End If
    '                End If
    '            End If
    '        Next
    '    End With

    '    Dim mSrl As Integer = 0, mDebit As Double, mCredit As Double

    '    mSrl = AgL.Dman_Execute(" Select IfNull(Max(V_SNo),0) From Ledger With (NoLock)
    '                    Where DocId = '" & mDocID & "'", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()


    '    With DtTemp
    '        For I = 0 To .Rows.Count - 1
    '            If Trim(AgL.XNull(.Rows(I)("PostAc"))) <> "" And Val(AgL.VNull(.Rows(I)("Amount"))) <> 0 Then
    '                mSrl += 1

    '                mDebit = 0 : mCredit = 0
    '                If AgL.StrCmp(AgL.XNull(.Rows(I)("PostAc")), "|PARTY|") Then
    '                    mPostSubCode = PostingPartyAc
    '                Else
    '                    mPostSubCode = AgL.XNull(.Rows(I)("PostAc"))
    '                End If

    '                If AgL.StrCmp(AgL.XNull(.Rows(I)("DrCr")), "Dr") Then
    '                    mDebit = Math.Abs(AgL.VNull(.Rows(I)("Amount")))
    '                ElseIf AgL.StrCmp(AgL.XNull(.Rows(I)("DrCr")), "Cr") Then
    '                    mCredit = Math.Abs(AgL.VNull(.Rows(I)("Amount")))
    '                End If

    '                mQry = "Insert Into Ledger(DocId,RecId,V_SNo,V_Date,SubCode,ContraSub,AmtDr,AmtCr," &
    '                     " Narration,V_Type,V_No,V_Prefix,Site_Code,DivCode,Chq_No,Chq_Date,TDSCategory,TDSOnAmt,TDSDesc," &
    '                     " TDSPer,TDS_Of_V_SNo,System_Generated,FormulaString,ContraText, CostCenter) Values " &
    '                     " ('" & mDocID & "','" & mRecID & "'," & mSrl & "," & AgL.Chk_Text(CDate(mV_Date).ToString("s")) & "," & AgL.Chk_Text(mPostSubCode) & "," & AgL.Chk_Text("") & ", " &
    '                     " " & mDebit & "," & mCredit & ", " &
    '                     " " & AgL.Chk_Text(IIf(AgL.StrCmp(AgL.XNull(.Rows(I)("PostAc")), "|PARTY|"), mNarrParty, mNarr)) & ",'" & mV_Type & "','" & mV_No & "','" & mV_Prefix & "'," &
    '                     " '" & mSite_Code & "','" & mDiv_Code & "','" & AgL.Chk_Text("") & "'," &
    '                     " " & AgL.Chk_Text("") & "," & AgL.Chk_Text("") & "," &
    '                     " " & Val("") & "," & AgL.Chk_Text("") & "," & Val("") & "," & 0 & ",'N','" & "" & "','" & StrContraTextJV & "', " & AgL.Chk_Text(mCostCenter) & ")"
    '                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

    '                If mPostSubCode = Dgl1.Item(Col1Subcode, J).Tag Then
    '                    mQry = "INSERT INTO LedgerAdj (Vr_DocId, Vr_V_SNo, Adj_DocID, Adj_V_SNo,
    '                      Amount, Site_Code, Div_Code, Adj_Type)
    '                      Select '" & mDocID & "'," & mSrl & ",'" & mSearchCode & "'," & Dgl1.Item(ColSNo, J).Tag & ",
    '                      Case When " & mDebit & " > 0 Then " & mDebit & " ELse " & mCredit & " End As Amount,
    '                      '" & AgL.PubSiteCode & "', '" & AgL.PubDivCode & "', 'Adjustment' "
    '                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    '                End If
    '            End If
    '        Next I
    '    End With
    'End Sub
    Private Sub Dgl1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Dgl1.CellContentClick
        Try
            Dim bRowIndex As Integer = Dgl1.CurrentCell.RowIndex
            Dim bColumnIndex As Integer = Dgl1.CurrentCell.ColumnIndex

            Select Case Dgl1.Columns(bColumnIndex).Name
                Case Col1BtnDeleteCancellation
                    If MsgBox("Do you want to delete cancellation ? ", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                        FDeleteCancellation(Dgl1.Item(Col1BtnDeleteCancellation, bRowIndex).Tag)
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub FDeleteCancellation(bDocId As String)
        Dim mTrans As String = ""
        Try
            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = "Begin"

            mQry = "Delete From TransactionReferences Where DocId = '" & bDocId & "'"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

            mQry = "Delete From Ledger Where DocId = '" & bDocId & "'"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

            mQry = "Delete From LedgerM Where DocId = '" & bDocId & "'"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

            mQry = "Delete From LedgerHeadDetail Where DocId = '" & bDocId & "'"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

            mQry = "Delete From LedgerHead Where DocId = '" & bDocId & "'"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

            AgL.ETrans.Commit()
            mTrans = "Commit"

            MsgBox("Cancellation Deleted...!", MsgBoxStyle.Information)
            MovRec()
        Catch ex As Exception
            AgL.ETrans.Rollback()
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub PostStructureLineToAccounts(bRowIndex As Integer, ByVal FGMain As AgStructure.AgCalcGrid, ByVal mNarrParty As String, ByVal mNarr As String, ByVal mDocID As String, ByVal mDiv_Code As String,
                                                   ByVal mSite_Code As String, ByVal Div_Code As String, ByVal mV_Type As String, ByVal mV_Prefix As String, ByVal mV_No As Integer,
                                                   ByVal mRecID As String, ByVal PostingPartyAc As String, ByVal mV_Date As String,
                                                   ByVal Conn As Object, ByVal Cmd As Object, Optional ByVal mCostCenter As String = "", Optional MultiplyWithMinus As Boolean = False)
        Dim StrContraTextJV As String = ""
        Dim mPostSubCode = ""
        Dim mPostContraSub = ""
        Dim I As Integer, J As Integer
        Dim mQry$ = "", bSelectionQry$ = ""
        Dim DtTemp As DataTable = Nothing

        bSelectionQry = ""


        'If Dgl1.Item(Col1AdditionalCharge, bRowIndex).Value > 0 And Dgl1.Item(Col1AdditionalChargeAc, bRowIndex).Value <> "" And
        '        Dgl1.Item(Col1AdditionalChargeAc, bRowIndex).Value IsNot Nothing Then

        '    If bSelectionQry <> "" Then bSelectionQry += " UNION ALL "
        '    bSelectionQry += " Select 1 as TmpCol,'" & Dgl1.Item(Col1Subcode, bRowIndex).Tag & "'  As PostAc, 
        '        '" & Dgl1.Item(Col1AdditionalChargeAc, bRowIndex).Tag & "' As ContraAc, " &
        '        " Case When '" & mTransactionType & "' = '" & AgLibrary.ClsMain.agConstants.VoucherCategory.Payment & "' Then " & Val(Dgl1.Item(Col1AdditionalCharge, bRowIndex).Value) * 1.0 & "  " &
        '        "      When '" & mTransactionType & "' = '" & AgLibrary.ClsMain.agConstants.VoucherCategory.Receipt & "' Then " & -Val(Dgl1.Item(Col1AdditionalCharge, bRowIndex).Value) * 1.0 & " End As Amount, 
        '        " & AgL.Chk_Date(FGMain.AgLineGrid.Item(Col1EffectiveDate, bRowIndex).Value) & " As EffectiveDate, " & AgL.Chk_Text(FGMain.AgLineGrid.Item(Col1Remark, bRowIndex).Value) & " As Narration, " & AgL.Chk_Text(FGMain.AgLineGrid.Item(Col1ChqRefNo, bRowIndex).Value) & " As ChqNo, " & AgL.Chk_Date(FGMain.AgLineGrid.Item(Col1ChqRefDate, bRowIndex).Value) & " As ChqDate "

        '    If bSelectionQry <> "" Then bSelectionQry += " UNION ALL "
        '    bSelectionQry += " Select 1 As TmpCol,'" & Dgl1.Item(Col1AdditionalChargeAc, bRowIndex).Tag & "'  As PostAc, 
        '        '" & Dgl1.Item(Col1AdditionalChargeAc, bRowIndex).Tag & "' As ContraAc, " &
        '        " Case When '" & mTransactionType & "' = '" & AgLibrary.ClsMain.agConstants.VoucherCategory.Payment & "' Then " & -Val(Dgl1.Item(Col1AdditionalCharge, bRowIndex).Value) * 1.0 & "  " &
        '        "      When '" & mTransactionType & "' = '" & AgLibrary.ClsMain.agConstants.VoucherCategory.Receipt & "' Then " & Val(Dgl1.Item(Col1AdditionalCharge, bRowIndex).Value) * 1.0 & " End As Amount ,
        '        " & AgL.Chk_Date(FGMain.AgLineGrid.Item(Col1EffectiveDate, bRowIndex).Value) & " As EffectiveDate, " & AgL.Chk_Text(FGMain.AgLineGrid.Item(Col1Remark, bRowIndex).Value) & " As Narration, " & AgL.Chk_Text(FGMain.AgLineGrid.Item(Col1ChqRefNo, bRowIndex).Value) & " As ChqNo, " & AgL.Chk_Date(FGMain.AgLineGrid.Item(Col1ChqRefDate, bRowIndex).Value) & " As ChqDate "
        'End If

        J = bRowIndex
        For I = 0 To FGMain.Rows.Count - 1
            'For J = 0 To FGMain.AgLineGrid.Rows.Count - 1
            If FGMain.AgLineGrid.Rows(J).Visible Then
                    If AgL.XNull(FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.PostAc)) <> "" Then
                        If Dgl1.Item(Col1Amount, J).Style.ForeColor = Color.Blue And FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag.ToString.ToUpper = "GAMT" Then
                            ' Not Fore Colour = Blue Means This Entry is Splitted into several Cash Entries

                        Else
                            If bSelectionQry <> "" Then bSelectionQry += " UNION ALL "

                            bSelectionQry += " Select 1 as TmpCol, '" & FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.PostAc) & "' As PostAc, 
                        '" & FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.ContraAc) & "' As ContraAc, 
                        Case When " & AgL.Chk_Text(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_DrCr, I).Value) & " = 'Dr' Then " & Val(FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.Amount)) * 1.0 & " 
                             When " & AgL.Chk_Text(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_DrCr, I).Value) & " = 'Cr' Then " & -Val(FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.Amount)) * 1.0 & " End As Amount,  
                        " & AgL.Chk_Date(FGMain.AgLineGrid.Item(Col1EffectiveDate, J).Value) & " as EffectiveDate, " & AgL.Chk_Text(FGMain.AgLineGrid.Item(Col1Remark, J).Value) & " as Narration, " & AgL.Chk_Text(FGMain.AgLineGrid.Item(Col1ChqRefNo, J).Value) & " as ChqNo, " & AgL.Chk_Date(FGMain.AgLineGrid.Item(Col1ChqRefDate, J).Value) & " as ChqDate "

                            If AgL.XNull(FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.ContraAc)) <> "" Then
                                If bSelectionQry <> "" Then bSelectionQry += " UNION ALL "

                                bSelectionQry += " Select 1 as TmpCol, '" & FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.ContraAc) & "' As PostAc, 
                            '" & FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.PostAc) & "' As ContraAc, 
                            Case When " & AgL.Chk_Text(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_DrCr, I).Value) & " = 'Dr' Then " & -Val(FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.Amount)) * 1.0 & " 
                                 When " & AgL.Chk_Text(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_DrCr, I).Value) & " = 'Cr' Then " & Val(FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.Amount)) * 1.0 & " End As Amount,  
                            " & AgL.Chk_Date(FGMain.AgLineGrid.Item(Col1EffectiveDate, J).Value) & " as EffectiveDate, " & AgL.Chk_Text(FGMain.AgLineGrid.Item(Col1Remark, J).Value) & " as Narration, " & AgL.Chk_Text(FGMain.AgLineGrid.Item(Col1ChqRefNo, J).Value) & " as ChqNo, " & AgL.Chk_Date(FGMain.AgLineGrid.Item(Col1ChqRefDate, J).Value) & " as ChqDate "
                            End If
                        End If
                    ElseIf Trim(AgL.XNull(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_PostAc, I).Value)) <> "" Then
                        If Dgl1.Item(Col1Amount, J).Style.ForeColor = Color.Blue And FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag.ToString.ToUpper = "GAMT" Then
                            ' Not Fore Colour = Blue Means This Entry is Splitted into several Cash Entries
                        Else
                            If bSelectionQry <> "" Then bSelectionQry += " UNION ALL "

                            bSelectionQry += " Select 1 as TmpCol,'" & FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_PostAc, I).Value & "' As PostAc,
                            '" & FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.ContraAc) & "' As ContraAc,
                            Case When " & AgL.Chk_Text(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_DrCr, I).Value) & " = 'Dr' Then " & Val(FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.Amount)) * 1.0 & " 
                                 When " & AgL.Chk_Text(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_DrCr, I).Value) & " = 'Cr' Then " & -Val(FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.Amount)) * 1.0 & " End As Amount,
                            " & AgL.Chk_Date(FGMain.AgLineGrid.Item(Col1EffectiveDate, J).Value) & " as EffectiveDate, " & AgL.Chk_Text(FGMain.AgLineGrid.Item(Col1Remark, J).Value) & " as Narration, " & AgL.Chk_Text(FGMain.AgLineGrid.Item(Col1ChqRefNo, J).Value) & " as ChqNo, " & AgL.Chk_Date(FGMain.AgLineGrid.Item(Col1ChqRefDate, J).Value) & " as ChqDate "


                            If AgL.XNull(FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.ContraAc)) <> "" Then
                                If bSelectionQry <> "" Then bSelectionQry += " UNION ALL "

                            bSelectionQry += " Select 1 as TmpCol, '" & FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.ContraAc) & "' As PostAc, 
                            '" & FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_PostAc, I).Value & "' As ContraAc, 
                            Case When " & AgL.Chk_Text(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_DrCr, I).Value) & " = 'Dr' Then " & -Val(FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.Amount)) * 1.0 & " 
                                 When " & AgL.Chk_Text(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_DrCr, I).Value) & " = 'Cr' Then " & Val(FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.Amount)) * 1.0 & " End As Amount,  
                            " & AgL.Chk_Date(FGMain.AgLineGrid.Item(Col1EffectiveDate, J).Value) & " as EffectiveDate, " & AgL.Chk_Text(FGMain.AgLineGrid.Item(Col1Remark, J).Value) & " as Narration, " & AgL.Chk_Text(FGMain.AgLineGrid.Item(Col1ChqRefNo, J).Value) & " as ChqNo, " & AgL.Chk_Date(FGMain.AgLineGrid.Item(Col1ChqRefDate, J).Value) & " as ChqDate "
                        End If
                        End If
                    End If

                    If Val(FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.Amount)) <> 0 Then
                        If AgL.Chk_Text(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_DrCr, I).Value) Is Nothing Then
                            Err.Raise(1, , "Error In Ledger Posting. Dr/Cr Not defined for any value.")
                        End If
                    End If
                End If
            'Next
        Next




        If bSelectionQry = "" Then Exit Sub


        mQry = " Select Count(*)  " &
                    " From (" & bSelectionQry & ") As V1 Group by tmpCol " &
                    " Having Round(Sum(Case When IfNull(V1.Amount*1.0,0) > 0 Then IfNull(V1.Amount*1.0,0) Else 0 End),3) <> Round(abs(Sum(Case When IfNull(V1.Amount*1.0,0) < 0 Then IfNull(V1.Amount*1.0,0) Else 0 End)),3)  "
        DtTemp = AgL.FillData(mQry, AgL.GcnRead).Tables(0)
        If DtTemp.Rows.Count > 0 Then
            If AgL.VNull(DtTemp.Rows(0)(0)) > 0 Then
                Console.Write(mQry)
                Err.Raise(1, , "Error In Ledger Posting. Debit And Credit balances are Not equal.")
            End If
        End If


        If MultiplyWithMinus Then
            mQry = " Select V1.EffectiveDate, V1.Narration, V1.ChqNo, V1.ChqDate,V1.PostAc, V1.ContraAc, cSg.Name as ContraName, IfNull(Sum(Cast(V1.Amount as Float)),0) As Amount, 
                Case When IfNull(Sum(V1.Amount),0) > 0 Then 'Cr' 
                     When IfNull(Sum(V1.Amount),0) < 0 Then 'Dr' End As DrCr 
                From (" & bSelectionQry & ") As V1 
                Left Join Subgroup cSg  on V1.ContraAc = cSg.Subcode
                Group BY V1.PostAc, V1.ContraAc, cSg.Name, V1.EffectiveDate, V1.Narration, V1.ChqNo, V1.ChqDate "
        Else
            mQry = " Select V1.EffectiveDate, V1.Narration, V1.ChqNo, V1.ChqDate,V1.PostAc, V1.ContraAc, cSg.Name as ContraName, IfNull(Sum(Cast(V1.Amount As Float)),0) As Amount, 
                 Case When IfNull(Sum(V1.Amount),0) > 0 Then 'Dr' 
                      When IfNull(Sum(V1.Amount),0) < 0 Then 'Cr' End As DrCr 
                From(" & bSelectionQry & ") As V1 
                Left Join Subgroup cSg  on V1.ContraAc = cSg.Subcode
                Group BY V1.PostAc, V1.ContraAc, cSg.Name, V1.EffectiveDate, V1.Narration, V1.ChqNo, V1.ChqDate "
        End If

        DtTemp = AgL.FillData(mQry, AgL.GcnRead).Tables(0)

        With DtTemp
            For I = 0 To .Rows.Count - 1
                If Trim(AgL.XNull(.Rows(I)("PostAc"))) <> "" Then
                    If AgL.StrCmp(AgL.XNull(.Rows(I)("PostAc")), "|PARTY|") Then
                        If AgL.VNull(.Rows(I)("Amount")) <> 0 And AgL.XNull(.Rows(I)("DrCr")) <> "" Then
                            If StrContraTextJV <> "" Then StrContraTextJV += vbCrLf
                            FPrepareContraText(False, StrContraTextJV, PostingPartyAc, Math.Abs(AgL.VNull(.Rows(I)("Amount"))), AgL.XNull(.Rows(I)("DrCr")))
                        End If
                    Else
                        If AgL.VNull(.Rows(I)("Amount")) <> 0 And AgL.XNull(.Rows(I)("DrCr")) <> "" Then
                            If StrContraTextJV <> "" Then StrContraTextJV += vbCrLf
                            FPrepareContraText(False, StrContraTextJV, AgL.XNull(.Rows(I)("PostAc")), Math.Abs(Val(AgL.VNull(.Rows(I)("Amount")))), AgL.XNull(.Rows(I)("DrCr")))
                        End If
                    End If
                End If
            Next
        End With

        Dim mSrl As Integer = 0, mDebit As Double, mCredit As Double
        Dim mNarration As String = ""

        mSrl = AgL.Dman_Execute(" Select IfNull(Max(V_SNo),0) From Ledger With (NoLock)
                            Where DocId = '" & mDocID & "'", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()


        With DtTemp
            For I = 0 To .Rows.Count - 1
                If Trim(AgL.XNull(.Rows(I)("PostAc"))) <> "" And Val(AgL.VNull(.Rows(I)("Amount"))) <> 0 Then
                    mSrl += 1

                    mDebit = 0 : mCredit = 0
                    If AgL.StrCmp(AgL.XNull(.Rows(I)("PostAc")), "|PARTY|") Then
                        mPostSubCode = PostingPartyAc
                    Else
                        mPostSubCode = AgL.XNull(.Rows(I)("PostAc"))
                    End If

                    If AgL.StrCmp(AgL.XNull(.Rows(I)("ContraAc")), "|PARTY|") Then
                        mPostContraSub = PostingPartyAc
                    Else
                        mPostContraSub = AgL.XNull(.Rows(I)("ContraAc"))
                    End If


                    'If AgL.StrCmp(AgL.XNull(.Rows(I)("DrCr")), "Dr") Then
                    '    mDebit = Math.Abs(AgL.VNull(.Rows(I)("Amount")))
                    'ElseIf AgL.StrCmp(AgL.XNull(.Rows(I)("DrCr")), "Cr") Then
                    '    mCredit = Math.Abs(AgL.VNull(.Rows(I)("Amount")))
                    'End If

                    'Because Cancellation Entry will be reverse posted.
                    If AgL.StrCmp(AgL.XNull(.Rows(I)("DrCr")), "Dr") Then
                        mCredit = Math.Abs(AgL.VNull(.Rows(I)("Amount")))
                    ElseIf AgL.StrCmp(AgL.XNull(.Rows(I)("DrCr")), "Cr") Then
                        mDebit = Math.Abs(AgL.VNull(.Rows(I)("Amount")))
                    End If


                    mNarration = AgL.XNull(AgL.Dman_Execute("Select Max(Name) From Subgroup  With (NoLock) Where Subcode = '" & mPostContraSub & "'", AgL.GcnRead).ExecuteScalar)
                    If IIf(AgL.StrCmp(AgL.XNull(.Rows(I)("PostAc")), "|PARTY|"), mNarrParty, AgL.XNull(.Rows(I)("Narration"))) <> "" Then mNarration = mNarration & vbCrLf
                    mNarration = mNarration & IIf(AgL.StrCmp(AgL.XNull(.Rows(I)("PostAc")), "|PARTY|"), mNarrParty, AgL.XNull(.Rows(I)("Narration")))



                    mQry = "Insert Into Ledger(DocId,RecId,V_SNo,V_Date,SubCode,ContraSub,AmtDr,AmtCr," &
                         " Narration,V_Type,V_No,V_Prefix,Site_Code,DivCode,Chq_No,Chq_Date,TDSCategory,TDSOnAmt,TDSDesc," &
                         " TDSPer,TDS_Of_V_SNo,System_Generated,FormulaString,ContraText, CostCenter,EffectiveDate) Values " &
                         " ('" & mDocID & "','" & mRecID & "'," & mSrl & "," & AgL.Chk_Text(CDate(mV_Date).ToString("s")) & "," & AgL.Chk_Text(mPostSubCode) & "," & AgL.Chk_Text(mPostContraSub) & ", " &
                         " " & mDebit & "," & mCredit & ", " &
                         " " & AgL.Chk_Text(mNarration) & ",'" & mV_Type & "','" & mV_No & "','" & mV_Prefix & "'," &
                         " '" & mSite_Code & "','" & mDiv_Code & "'," & AgL.Chk_Text(AgL.XNull(.Rows(I)("ChqNo"))) & "," &
                         " " & AgL.Chk_Date(AgL.XNull(.Rows(I)("ChqDate"))) & "," & AgL.Chk_Text("") & "," &
                         " " & Val("") & "," & AgL.Chk_Text("") & "," & Val("") & "," & 0 & ",'N','" & "" & "'," & AgL.Chk_Text(StrContraTextJV) & ", " & AgL.Chk_Text(mCostCenter) & ", " & AgL.Chk_Date(AgL.XNull(.Rows(I)("EffectiveDate"))) & ")"
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                End If
            Next I
        End With
    End Sub
End Class