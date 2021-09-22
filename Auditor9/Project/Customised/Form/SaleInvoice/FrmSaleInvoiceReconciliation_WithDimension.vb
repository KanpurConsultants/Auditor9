Imports Customised.ClsMain

Public Class FrmSaleInvoiceReconciliation_WithDimension
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Dim mSearchCode$ = ""
    Public DtV_TypeSettings As DataTable
    Protected Const Col1Select As String = "Tick"
    Public Const ColSNo As String = "S.No."
    Public Const Col1ImportStatus As String = "Import Status"
    Public Const Col1V_Nature As String = "V_Nature"
    Public Const Col1Barcode As String = "Barcode"
    Public Const Col1SKU As String = "SKU"
    Public Const Col1ItemCategory As String = "Item Category"
    Public Const Col1ItemGroup As String = "Item Group"
    Public Const Col1ItemCode As String = "Item Code"
    Public Const Col1Item As String = "Item"
    Public Const Col1Dimension1 As String = "Dimension1"
    Public Const Col1Dimension2 As String = "Dimension2"
    Public Const Col1Dimension3 As String = "Dimension3"
    Public Const Col1Dimension4 As String = "Dimension4"
    Public Const Col1Size As String = "Size"
    Public Const Col1Specification As String = "Specification"
    Public Const Col1SalesTaxGroup As String = "Sales Tax Group Item"
    Public Const Col1BaleNo As String = "Bale No"
    Public Const Col1LotNo As String = "Lot No"
    Public Const Col1DocQty As String = "Doc Qty"
    Public Const Col1FreeQty As String = "Free Qty"
    Public Const Col1Qty As String = "Qty"
    Public Const Col1Unit As String = "Unit"
    Public Const Col1QtyDecimalPlaces As String = "Qty Decimal Places"
    Public Const Col1Pcs As String = "Pcs"
    Public Const Col1UnitMultiplier As String = "Unit Multiplier"
    Public Const Col1DealQty As String = "Deal Qty"
    Public Const Col1DealUnit As String = "Deal Unit"
    Public Const Col1DealUnitDecimalPlaces As String = "Deal Decimal Places"
    Public Const Col1Rate As String = "Rate"
    Public Const Col1DiscountPer As String = "Disc. %"
    Public Const Col1DiscountAmount As String = "Disc. Amt"
    Public Const Col1AdditionalDiscountPer As String = "Add. Disc. %"
    Public Const Col1AdditionalDiscountAmount As String = "Add. Disc. Amt"
    Public Const Col1Amount As String = "Amount"
    Public Const Col1DimensionDetail As String = "Dimension Detail"
    Public Const Col1Remark As String = "Remark"

    Dim mQry As String = ""

    Public Property SearchCode() As String
        Get
            SearchCode = mSearchCode
        End Get
        Set(ByVal value As String)
            mSearchCode = value
        End Set
    End Property
    Public Sub Ini_Grid()
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl1, Col1Select, 35, 0, Col1Select, True, True, False)
            .AddAgTextColumn(Dgl1, Col1Barcode, 100, 0, Col1Barcode, False, True)
            .AddAgTextColumn(Dgl1, Col1SKU, 300, 0, Col1SKU, True, False, False)
            .AddAgTextColumn(Dgl1, Col1ItemCategory, 100, 0, Col1ItemCategory, False, True)
            .AddAgTextColumn(Dgl1, Col1ItemGroup, 100, 0, Col1ItemGroup, False, True)
            .AddAgTextColumn(Dgl1, Col1ItemCode, 100, 0, Col1ItemCode, False, True, False)
            .AddAgTextColumn(Dgl1, Col1Item, 130, 0, Col1Item, True, True)
            .AddAgTextColumn(Dgl1, Col1Dimension1, 100, 0, Col1Dimension1, False, True)
            .AddAgTextColumn(Dgl1, Col1Dimension2, 100, 0, Col1Dimension2, False, True)
            .AddAgTextColumn(Dgl1, Col1Dimension3, 100, 0, Col1Dimension3, False, True)
            .AddAgTextColumn(Dgl1, Col1Dimension4, 100, 0, Col1Dimension4, False, True)
            .AddAgTextColumn(Dgl1, Col1Size, 100, 0, Col1Size, False, True)
            .AddAgTextColumn(Dgl1, Col1Specification, 130, 0, Col1Specification, True, True)
            .AddAgTextColumn(Dgl1, Col1SalesTaxGroup, 100, 0, Col1SalesTaxGroup, False, True)
            .AddAgTextColumn(Dgl1, Col1BaleNo, 60, 255, Col1BaleNo, False, True)
            .AddAgTextColumn(Dgl1, Col1LotNo, 60, 255, Col1LotNo, False, True)
            .AddAgTextColumn(Dgl1, Col1QtyDecimalPlaces, 50, 0, Col1QtyDecimalPlaces, False, True, False)
            .AddAgNumberColumn(Dgl1, Col1DocQty, 70, 8, 4, False, Col1DocQty, True, True, True)
            .AddAgNumberColumn(Dgl1, Col1FreeQty, 80, 8, 4, False, Col1FreeQty, False, True, True)
            .AddAgNumberColumn(Dgl1, Col1Qty, 80, 8, 4, False, Col1Qty, False, True, True)
            .AddAgTextColumn(Dgl1, Col1Unit, 50, 0, Col1Unit, True, True)
            .AddAgNumberColumn(Dgl1, Col1Pcs, 80, 8, 4, False, Col1Pcs, True, True, True)
            .AddAgNumberColumn(Dgl1, Col1Rate, 80, 8, 2, False, Col1Rate, True, True, True)
            .AddAgNumberColumn(Dgl1, Col1DiscountPer, 80, 2, 3, False, Col1DiscountPer, True, True, True)
            .AddAgNumberColumn(Dgl1, Col1DiscountAmount, 100, 8, 3, False, Col1DiscountAmount, True, True, True)
            .AddAgNumberColumn(Dgl1, Col1AdditionalDiscountPer, 80, 2, 3, False, Col1AdditionalDiscountPer, True, True, True)
            .AddAgNumberColumn(Dgl1, Col1AdditionalDiscountAmount, 100, 8, 3, False, Col1AdditionalDiscountAmount, True, True, True)
            .AddAgNumberColumn(Dgl1, Col1Amount, 100, 8, 2, False, Col1Amount, True, True, True)
            .AddAgNumberColumn(Dgl1, Col1UnitMultiplier, 70, 8, 4, False, Col1UnitMultiplier, False, True, True)
            .AddAgNumberColumn(Dgl1, Col1DealQty, 70, 8, 3, False, Col1DealQty, False, True, True)
            .AddAgTextColumn(Dgl1, Col1DealUnit, 60, 0, Col1DealUnit, False, True)
            .AddAgTextColumn(Dgl1, Col1DealUnitDecimalPlaces, 50, 0, Col1DealUnitDecimalPlaces, False, True, False)
            .AddAgTextColumn(Dgl1, Col1DimensionDetail, 150, 255, Col1DimensionDetail, False, True)
            .AddAgTextColumn(Dgl1, Col1Remark, 150, 255, Col1Remark, False, True)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 35
        Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        AgL.GridDesign(Dgl1)
        Dgl1.Name = "Dgl1"
        AgL.FSetDimensionCaptionForHorizontalGrid(Dgl1, AgL)

        Dgl1.AllowUserToAddRows = False
        Dgl1.AgSkipReadOnlyColumns = True
        Dgl1.AllowUserToOrderColumns = True
        Dgl1.Columns(Col1Select).DefaultCellStyle.Font = New Font(New FontFamily("wingdings"), 14)

        ApplyUISetting()

        Dgl1.Anchor = AnchorStyles.Bottom + AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Top
        Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        AgCL.GridSetiingShowXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1, False)
    End Sub

    Private Sub FrmReconcile_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Ini_Grid()
        MovRec()
    End Sub

    Private Sub MovRec()
        Dim mQry As String = ""

        LblTotalPcs.Text = 0
        LblTotalQty.Text = 0
        LblTotalAmount.Text = 0


        mQry = "Select L.*, 
                        Si.V_Type || '-' || Si.ManualRefNo As SaleInvoiceNo, 
                        Stock.V_Type || '-' || Stock.RecID As PurchaseNo, 
                        U.DecimalPlaces, U.DecimalPlaces As QtyDecimalPlaces, U.ShowDimensionDetailInSales, MU.DecimalPlaces As DealUnitDecimalPlaces, 
                        (Stock.Landed_Value/Stock.Qty_Rec) + (Stock.Landed_Value/Stock.Qty_Rec)*1/100 As PurchaseRate, 
                        Sku.Code As SkuCode, Sku.Description As SkuDesc, 
                        Bc.Description As BarcodeName, I.Description As ItemDesc, I.ManualCode, 
                        It.Code As ItemType, It.Name As ItemTypeDesc,
                        IG.Description As ItemGroupDesc, IC.Description As ItemCategoryDesc, 
                        Sids.Item As ItemCode, Sids.ItemCategory, Sids.ItemGroup, 
                        Sids.Dimension1, Sids.Dimension2, 
                        Sids.Dimension3, Sids.Dimension4, Sids.Size, 
                        D1.Description as Dimension1Desc, D2.Description as Dimension2Desc,
                        D3.Description as Dimension3Desc, D4.Description as Dimension4Desc, Size.Description as SizeDesc,
                        HV.PurchaseRate, HV.DefaultDiscountPer, HV.PersonalDiscountPer, HV.PersonalAdditionalDiscountPer,
                        L.DimensionDetail         
                        From (Select * From SaleInvoiceDetail  Where DocId = '" & SearchCode & "') As L 
                        LEFT JOIN SaleInvoiceDetailSku Sids With (NoLock) On L.DocId = Sids.DocId And L.Sr = Sids.Sr
                        Left Join SaleInvoiceDetailHelpValues HV On L.DocID = HV.DocId And L.Sr = HV.Sr
                        LEFT JOIN Item Sku ON Sku.Code = L.Item
                        LEFT JOIN ItemType It On Sku.ItemType = It.Code
                        Left Join Item IC On Sids.ItemCategory = IC.Code
                        Left Join Item IG On Sids.ItemGroup = IG.Code
                        LEFT JOIN Item I ON Sids.Item = I.Code
                        LEFT JOIN Item D1 ON Sids.Dimension1 = D1.Code
                        LEFT JOIN Item D2 ON Sids.Dimension2 = D2.Code
                        LEFT JOIN Item D3 ON Sids.Dimension3 = D3.Code
                        LEFT JOIN Item D4 ON Sids.Dimension4 = D4.Code
                        LEFT JOIN Item Size ON Sids.Size = Size.Code
                        LEFT JOIN Stock On L.ReferenceDocId = Stock.docid And l.ReferenceDocIdSr = Stock.Sr  
                        LEFT JOIN SaleInvoice Si On L.SaleInvoice = Si.DocId 
                        Left Join Barcode Bc On L.Barcode = Bc.Code
                        Left Join Unit U On L.Unit = U.Code 
                        Left Join Unit MU On L.DealUnit = MU.Code 
                        Order By L.Sr "
        Dim DtTemp As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

        With DtTemp
            Dgl1.RowCount = 1
            Dgl1.Rows.Clear()
            If .Rows.Count > 0 Then
                For I As Integer = 0 To .Rows.Count - 1
                    Dgl1.Rows.Add()
                    Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count
                    Dgl1.Item(ColSNo, I).Tag = AgL.XNull(.Rows(I)("Sr"))
                    If AgL.XNull(.Rows(I)("ReconcileBy")) = "" Then
                        Dgl1.Item(Col1Select, I).Value = "o"
                    Else
                        Dgl1.Item(Col1Select, I).Value = "þ"
                        Dgl1.Rows(I).DefaultCellStyle.BackColor = ColorConstants.Verified
                    End If
                    Dgl1.Item(Col1Barcode, I).Value = AgL.XNull(.Rows(I)("BarcodeName"))
                    Dgl1.Item(Col1SKU, I).Value = AgL.XNull(.Rows(I)("SkuDesc"))
                    Dgl1.Item(Col1ItemCategory, I).Value = AgL.XNull(.Rows(I)("ItemCategoryDesc"))
                    Dgl1.Item(Col1ItemGroup, I).Value = AgL.XNull(.Rows(I)("ItemGroupDesc"))
                    Dgl1.Item(Col1ItemCode, I).Value = AgL.XNull(.Rows(I)("ManualCode"))
                    Dgl1.Item(Col1Item, I).Value = AgL.XNull(.Rows(I)("ItemDesc"))
                    Dgl1.Item(Col1Dimension1, I).Value = AgL.XNull(.Rows(I)("Dimension1Desc"))
                    Dgl1.Item(Col1Dimension2, I).Value = AgL.XNull(.Rows(I)("Dimension2Desc"))
                    Dgl1.Item(Col1Dimension3, I).Value = AgL.XNull(.Rows(I)("Dimension3Desc"))
                    Dgl1.Item(Col1Dimension4, I).Value = AgL.XNull(.Rows(I)("Dimension4Desc"))
                    Dgl1.Item(Col1Size, I).Value = AgL.XNull(.Rows(I)("SizeDesc"))
                    Dgl1.Item(Col1Specification, I).Value = AgL.XNull(.Rows(I)("Specification"))
                    Dgl1.Item(Col1SalesTaxGroup, I).Value = AgL.XNull(.Rows(I)("SalesTaxGroupItem"))
                    Dgl1.Item(Col1DocQty, I).Value = Format(Math.Abs(AgL.VNull(.Rows(I)("DocQty"))), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                    Dgl1.Item(Col1FreeQty, I).Value = Format(AgL.VNull(.Rows(I)("FreeQty")), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                    Dgl1.Item(Col1Qty, I).Value = Format(Math.Abs(AgL.VNull(.Rows(I)("Qty"))), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                    Dgl1.Item(Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                    Dgl1.Item(Col1Pcs, I).Value = AgL.VNull(.Rows(I)("Pcs"))
                    Dgl1.Item(Col1DealUnitDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("DealUnitDecimalPlaces"))
                    Dgl1.Item(Col1UnitMultiplier, I).Value = Format(AgL.VNull(.Rows(I)("UnitMultiplier")), "0.".PadRight(AgL.VNull(.Rows(I)("DealUnitDecimalPlaces")) + 2, "0"))
                    Dgl1.Item(Col1DealUnit, I).Value = AgL.XNull(.Rows(I)("DealUnit"))
                    Dgl1.Item(Col1DealQty, I).Value = Format(AgL.VNull(.Rows(I)("DocDealQty")), "0.".PadRight(AgL.VNull(.Rows(I)("DealUnitDecimalPlaces")) + 2, "0"))
                    Dgl1.Item(Col1Rate, I).Value = AgL.VNull(.Rows(I)("Rate"))
                    Dgl1.Item(Col1Amount, I).Value = Format(Math.Abs(AgL.VNull(.Rows(I)("Amount"))), "0.00")
                    Dgl1.Item(Col1DiscountPer, I).Value = AgL.VNull(.Rows(I)("DiscountPer"))
                    Dgl1.Item(Col1DiscountAmount, I).Value = AgL.VNull(.Rows(I)("DiscountAmount"))
                    Dgl1.Item(Col1AdditionalDiscountPer, I).Value = AgL.VNull(.Rows(I)("AdditionalDiscountPer"))
                    Dgl1.Item(Col1AdditionalDiscountAmount, I).Value = AgL.VNull(.Rows(I)("AdditionalDiscountAmount"))
                    Dgl1.Item(Col1DimensionDetail, I).Value = AgL.XNull(.Rows(I)("DimensionDetail"))
                    Dgl1.Item(Col1Remark, I).Value = AgL.XNull(.Rows(I)("Remark"))
                    Dgl1.Item(Col1BaleNo, I).Value = AgL.XNull(.Rows(I)("BaleNo"))
                    Dgl1.Item(Col1LotNo, I).Value = AgL.XNull(.Rows(I)("LotNo"))

                    If Dgl1.Item(Col1Select, I).Value = "þ" Then
                        LblTotalPcs.Text = Val(LblTotalQty.Text) + Val(Dgl1.Item(Col1Qty, I).Value)
                        LblTotalQty.Text = Val(LblTotalQty.Text) + Val(Dgl1.Item(Col1Qty, I).Value)
                        LblTotalAmount.Text = Val(LblTotalAmount.Text) + Val(Dgl1.Item(Col1Amount, I).Value)
                    End If
                Next I
            End If
        End With

        Dgl1.DefaultCellStyle.WrapMode = DataGridViewTriState.True
        Dgl1.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells)
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
                            ClsMain.FManageTick(Dgl1, Dgl1.CurrentCell.ColumnIndex, Dgl1.Columns(Col1Sku).Index)
                            FSave(mSearchCode, Dgl1.Item(ColSNo, mRowIndex).Tag, Dgl1.Item(Col1Select, mRowIndex).Value, mRowIndex)
                        End If
                    End If
            End Select
            Calculation()
        Catch ex As Exception
            MsgBox("System Exception : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
        End Try
    End Sub
    Private Sub Dgl1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.KeyDown
        Dim mRowIndex As Integer = Dgl1.CurrentCell.RowIndex
        Dim mColumnIndex As Integer = Dgl1.CurrentCell.ColumnIndex
        Try
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Select
                    If e.KeyCode = Keys.Space Then
                        ClsMain.FManageTick(Dgl1, Dgl1.CurrentCell.ColumnIndex, Dgl1.Columns(Col1SKU).Index)
                        FSave(mSearchCode, Dgl1.Item(ColSNo, mRowIndex).Tag, Dgl1.Item(Col1Select, mRowIndex).Value, mRowIndex)
                    End If
            End Select
            Calculation()
        Catch ex As Exception
            MsgBox("System Exception : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
        End Try
    End Sub
    Private Sub FSave(DocId As String, Sr As Integer, SelectValue As String, mRowIndex As Integer)
        If SelectValue = "þ" Then
            If AgL.PubServerName = "" Then
                mQry = "UPDATE SaleInvoiceDetail Set ReconcileDateTime = strftime('%Y-%m-%d %H:%M:%S','now'), ReconcileBy = '" & AgL.PubUserName & "'
                    Where DocId = '" & DocId & "' And Sr = " & Sr & ""
            Else
                mQry = "UPDATE SaleInvoiceDetail Set ReconcileDateTime = getdate(), ReconcileBy = '" & AgL.PubUserName & "'
                    Where DocId = '" & DocId & "' And Sr = " & Sr & ""
            End If
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                Dgl1.Rows(mRowIndex).DefaultCellStyle.BackColor = ColorConstants.Verified
            ElseIf SelectValue = "o" Then
                mQry = "UPDATE SaleInvoiceDetail Set ReconcileDateTime = Null, ReconcileBy = Null
                    Where DocId = '" & DocId & "' And Sr = " & Sr & ""
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            Dgl1.Rows(mRowIndex).DefaultCellStyle.BackColor = Color.White
        End If
    End Sub

    Private Sub Calculation()
        LblTotalPcs.Text = 0
        LblTotalQty.Text = 0
        LblTotalAmount.Text = 0
        For I As Integer = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1Select, I).Value = "þ" Then
                LblTotalPcs.Text = Val(LblTotalPcs.Text) + Val(Dgl1.Item(Col1Pcs, I).Value)
                LblTotalQty.Text = Val(LblTotalQty.Text) + Val(Dgl1.Item(Col1Qty, I).Value)
                LblTotalAmount.Text = Val(LblTotalAmount.Text) + Val(Dgl1.Item(Col1Amount, I).Value)
            End If
        Next

    End Sub

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, 0)
    End Sub
    Private Sub ApplyUISetting()
        mQry = "Select H.V_Type, Vt.NCat, H.SettingGroup 
                From SaleInvoice H
                LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                Where DocId = '" & mSearchCode & "' "
        Dim DtV_Type As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
        GetUISetting(Dgl1, Me.Name, AgL.PubDivCode, AgL.PubSiteCode, AgL.XNull(DtV_Type.Rows(0)("NCat")), AgL.XNull(DtV_Type.Rows(0)("V_Type")), "", AgL.XNull(DtV_Type.Rows(0)("SettingGroup")), ClsMain.GridTypeConstants.HorizontalGrid)

        Dgl1.Columns(Col1Select).Visible = True
    End Sub
End Class