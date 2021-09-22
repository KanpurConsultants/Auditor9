Imports AgLibrary.ClsMain.agConstants
Imports Customised.ClsMain

Public Class FrmPurchPlanBase
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Dim mSearchCode$ = ""
    Dim mSr As Integer = 0

    Public Const ColSNo As String = "S.No."
    Public Const Col1BasePlanNo As String = "Base Plan No"
    Public Const Col1BasePlanItemCategory As String = "Base Plan Item Category"
    Public Const Col1BasePlanItemGroup As String = "Base Plan Item Group"
    Public Const Col1BasePlanItem As String = "Base Plan Item"
    Public Const Col1BasePlanDimension1 As String = "Base Plan Dimension1"
    Public Const Col1BasePlanDimension2 As String = "Base Plan Dimension2"
    Public Const Col1BasePlanDimension3 As String = "Base Plan Dimension3"
    Public Const Col1BasePlanDimension4 As String = "Base Plan Dimension4"
    Public Const Col1BasePlanQty As String = "Base Plan Qty"
    Public Const Col1Qty As String = "Qty"

    Dim mQry As String = ""
    Dim mEntryNCat As String = ""
    Public Property SearchCode() As String
        Get
            SearchCode = mSearchCode
        End Get
        Set(ByVal value As String)
            mSearchCode = value
        End Set
    End Property
    Public Property Sr() As Integer
        Get
            Sr = mSr
        End Get
        Set(ByVal value As Integer)
            mSr = value
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
    Public Sub Ini_Grid()
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl1, Col1BasePlanNo, 100, 0, Col1BasePlanNo, True, True)
            .AddAgTextColumn(Dgl1, Col1BasePlanItemCategory, 100, 0, Col1BasePlanItemCategory, True, True)
            .AddAgTextColumn(Dgl1, Col1BasePlanItemGroup, 100, 0, Col1BasePlanItemGroup, True, True)
            .AddAgTextColumn(Dgl1, Col1BasePlanItem, 300, 0, Col1BasePlanItem, True, True)
            .AddAgTextColumn(Dgl1, Col1BasePlanDimension1, 100, 0, Col1BasePlanDimension1, True, True)
            .AddAgTextColumn(Dgl1, Col1BasePlanDimension2, 100, 0, Col1BasePlanDimension2, True, True)
            .AddAgTextColumn(Dgl1, Col1BasePlanDimension3, 100, 0, Col1BasePlanDimension3, True, True)
            .AddAgTextColumn(Dgl1, Col1BasePlanDimension4, 100, 0, Col1BasePlanDimension4, True, True)
            .AddAgNumberColumn(Dgl1, Col1BasePlanQty, 80, 8, 4, False, Col1BasePlanQty, True, True, True)
            .AddAgNumberColumn(Dgl1, Col1Qty, 80, 8, 4, False, Col1Qty, True, True, True)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 35
        Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        AgL.GridDesign(Dgl1)

        Dgl1.Name = "Dgl1"
        Dgl1.AllowUserToAddRows = False
        Dgl1.AgSkipReadOnlyColumns = True
        Dgl1.AllowUserToOrderColumns = True
        Dgl1.BackgroundColor = Me.BackColor
        Dgl1.BorderStyle = BorderStyle.None

        AgL.FSetDimensionCaptionForHorizontalGrid(Dgl1, AgL)
        ApplyUISettings(EntryNCat)

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

        LblTotalQty.Text = 0

        If AgL.VNull(AgL.Dman_Execute("Select Count(*) From PurchPlanDetailBaseSaleOrder Where GenDocID = '" & mSearchCode & "'", AgL.GCn).ExecuteScalar()) > 0 Then
            mQry = "SELECT So.ManualRefNo As BasePlanNo,
                I.Description As ItemDesc, I.ManualCode, 
                U.DecimalPlaces, U.DecimalPlaces As QtyDecimalPlaces, 
                MU.DecimalPlaces As DealUnitDecimalPlaces, 
                IG.Description As ItemGroupDesc, I.ItemCategory, I.ItemGroup, 
                IC.Description As ItemCategoryDesc, 
                SKU.Dimension1, SKU.Dimension2, 
                Sku.Dimension3, Sku.Dimension4, Sku.Size, 
                D1.Description as Dimension1Desc, D2.Description as Dimension2Desc,
                D3.Description as Dimension3Desc, D4.Description as Dimension4Desc, 
                Size.Description as SizeDesc, Lb.Qty As BasePlanQty
                From (Select * From PurchPlanDetailBase  With (NoLock)  Where DocId = '" & SearchCode & "' And TSr = " & mSr & ") As L 
                LEFT JOIN PurchPlanDetailBaseSaleOrder Lb ON L.DocID = Lb.GenDocID AND L.TSr = Lb.GenSr
                LEFT JOIN SaleOrderDetail Sod ON Lb.SaleInvoice = Sod.DocID AND Lb.SaleInvoiceSr = Sod.Sr
                LEFT JOIN SaleOrder So On Sod.DocId = So.DocId
                LEFT JOIN Item Sku ON Sku.Code = Sod.Item
                LEFT JOIN Item I ON I.Code = IsNull(Sku.BaseItem,Sku.Code) And I.V_Type <> '" & ItemV_Type.SKU & "'
                Left Join Item IC On Sku.ItemCategory = IC.Code
                Left Join Item IG On Sku.ItemGroup = IG.Code
                LEFT JOIN Item D1 ON D1.Code = Sku.Dimension1  
                LEFT JOIN Item D2 ON D2.Code = Sku.Dimension2
                LEFT JOIN Item D3 ON D3.Code = Sku.Dimension3
                LEFT JOIN Item D4 ON D4.Code = Sku.Dimension4
                LEFT JOIN Item Size ON Size.Code = Sku.Size
                Left Join Unit U  With (NoLock) On Sod.Unit = U.Code 
                Left Join Unit MU  With (NoLock) On Sod.DealUnit = MU.Code 
                ORDER BY L.Sr "
        Else
            mQry = "Select L.*, Pph.ManualRefNo As BasePlanNo, Prc.Name As ProcessDesc,
                    I.Description As ItemDesc, I.ManualCode, 
                    U.DecimalPlaces, U.DecimalPlaces As QtyDecimalPlaces, 
                    MU.DecimalPlaces As DealUnitDecimalPlaces, 
                    IG.Description As ItemGroupDesc, I.ItemCategory, I.ItemGroup, 
                    IC.Description As ItemCategoryDesc, Prc.Name As ProcessDesc,
                    SKU.Dimension1, SKU.Dimension2, 
                    Sku.Dimension3, Sku.Dimension4, Sku.Size, 
                    D1.Description as Dimension1Desc, D2.Description as Dimension2Desc,
                    D3.Description as Dimension3Desc, D4.Description as Dimension4Desc, 
                    Size.Description as SizeDesc
                    From (Select * From PurchPlanDetailBase  With (NoLock)  Where DocId = '" & SearchCode & "' And TSr = " & mSr & ") As L 
                    LEFT JOIN PurchPlanDetail Ppd ON L.PurchPlan = Ppd.DocID AND L.PurchPlanSr = Ppd.Sr
                    LEFT JOIN PurchPlan Pph On Ppd.DocId = Pph.DocId
                    LEFT JOIN SubGroup Prc On Ppd.Process = Prc.SubCode
                    LEFT JOIN Item Sku ON Sku.Code = Ppd.Item
                    LEFT JOIN Item I ON I.Code = IsNull(Sku.BaseItem,Sku.Code) And I.V_Type <> '" & ItemV_Type.SKU & "'
                    Left Join Item IC On Sku.ItemCategory = IC.Code
                    Left Join Item IG On Sku.ItemGroup = IG.Code
                    LEFT JOIN Item D1 ON D1.Code = Sku.Dimension1  
                    LEFT JOIN Item D2 ON D2.Code = Sku.Dimension2
                    LEFT JOIN Item D3 ON D3.Code = Sku.Dimension3
                    LEFT JOIN Item D4 ON D4.Code = Sku.Dimension4
                    LEFT JOIN Item Size ON Size.Code = Sku.Size
                    Left Join Unit U  With (NoLock) On Ppd.Unit = U.Code 
                    Left Join Unit MU  With (NoLock) On Ppd.DealUnit = MU.Code 
                    Order By L.Sr "
        End If


        Dim DtTemp As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

        With DtTemp
            Dgl1.RowCount = 1
            Dgl1.Rows.Clear()
            If .Rows.Count > 0 Then
                For I As Integer = 0 To .Rows.Count - 1
                    Dgl1.Rows.Add()
                    Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count
                    Dgl1.Item(Col1BasePlanNo, I).Value = AgL.XNull(.Rows(I)("BasePlanNo"))
                    Dgl1.Item(Col1BasePlanItemCategory, I).Value = AgL.XNull(.Rows(I)("ItemCategoryDesc"))
                    Dgl1.Item(Col1BasePlanItemGroup, I).Value = AgL.XNull(.Rows(I)("ItemGroupDesc"))
                    Dgl1.Item(Col1BasePlanItem, I).Value = AgL.XNull(.Rows(I)("ItemDesc"))
                    Dgl1.Item(Col1BasePlanDimension1, I).Value = AgL.XNull(.Rows(I)("Dimension1Desc"))
                    Dgl1.Item(Col1BasePlanDimension2, I).Value = AgL.XNull(.Rows(I)("Dimension2Desc"))
                    Dgl1.Item(Col1BasePlanDimension3, I).Value = AgL.XNull(.Rows(I)("Dimension3Desc"))
                    Dgl1.Item(Col1BasePlanDimension4, I).Value = AgL.XNull(.Rows(I)("Dimension4Desc"))
                    Dgl1.Item(Col1BasePlanQty, I).Value = Format(Math.Abs(AgL.VNull(.Rows(I)("BasePlanQty"))), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))

                    LblTotalQty.Text = Val(LblTotalQty.Text) + Val(Dgl1.Item(Col1Qty, I).Value)
                Next I
            End If
        End With
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
    Private Sub Calculation()
        LblTotalQty.Text = 0
        For I As Integer = 0 To Dgl1.RowCount - 1
            LblTotalQty.Text = Val(LblTotalQty.Text) + Val(Dgl1.Item(Col1Qty, I).Value)
        Next
    End Sub
    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, 0)
    End Sub
    Private Sub ApplyUISettings(NCAT As String)
        Dim mQry As String
        Dim DtTemp As DataTable
        Dim I As Integer, J As Integer
        Try
            For I = 1 To Dgl1.Columns.Count - 1
                Dgl1.Columns(I).Visible = False
            Next

            mQry = "Select H.*
                    from EntryLineUISetting H                    
                    Where EntryName='" & Me.Name & "' And NCat = '" & NCAT & "' 
                    And GridName ='" & Dgl1.Name & "' "
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

            If DtTemp.Rows.Count > 0 Then
                For I = 0 To DtTemp.Rows.Count - 1
                    For J = 0 To Dgl1.Columns.Count - 1
                        If AgL.XNull(DtTemp.Rows(I)("FieldName")) = Dgl1.Columns(J).Name Then
                            Dgl1.Columns(J).Visible = AgL.VNull(DtTemp.Rows(I)("IsVisible"))
                            Dgl1.Columns(J).ReadOnly = Not AgL.VNull(DtTemp.Rows(I)("IsEditable"))
                            If Not IsDBNull(DtTemp.Rows(I)("DisplayIndex")) Then
                                Dgl1.Columns(J).DisplayIndex = AgL.VNull(DtTemp.Rows(I)("DisplayIndex"))
                            End If
                            If AgL.XNull(DtTemp.Rows(I)("Caption")) <> "" Then
                                Dgl1.Columns(J).HeaderText = AgL.XNull(DtTemp.Rows(I)("Caption"))
                            End If
                        End If
                    Next
                Next
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " [ApplySubgroupTypeSetting]")
        End Try
    End Sub
End Class