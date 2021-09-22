Imports System.ComponentModel
Imports Customised.ClsMain

Public Class FrmBarcodeHistory
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Dim mSearchCode$ = ""
    Public DtV_TypeSettings As DataTable

    Public Const ColSNo As String = "S.No."
    Public WithEvents DglMain As New AgControls.AgDataGrid
    Public Const Col1Head As String = "Head"
    Public Const Col1HeadOriginal As String = "Head Original"
    Public Const Col1Mandatory As String = ""
    Public Const Col1Value As String = "Value"

    Public Const rowCurrentGodown As Integer = 0
    Public Const rowCurrentProcess As Integer = 1
    Public Const rowItemCategory As Integer = 2
    Public Const rowItemGroup As Integer = 3
    Public Const rowItem As Integer = 4
    Public Const rowDimension1 As Integer = 5
    Public Const rowDimension2 As Integer = 6
    Public Const rowDimension3 As Integer = 7
    Public Const rowDimension4 As Integer = 8
    Public Const rowSize As Integer = 9
    Public Const rowPurchaseRate As Integer = 10
    Public Const rowSaleRate As Integer = 11
    Public Const rowMRP As Integer = 12


    Public Const HcCurrentGodown As String = "Current Godown"
    Public Const HcCurrentProcess As String = "Current Process"
    Public Const HcItemCategory As String = "Item Category"
    Public Const HcItemGroup As String = "Item Group"
    Public Const HcItem As String = "Item"
    Public Const HcDimension1 As String = "Dimension1"
    Public Const HcDimension2 As String = "Dimension2"
    Public Const HcDimension3 As String = "Dimension3"
    Public Const HcDimension4 As String = "Dimension4"
    Public Const HcSize As String = "Size"
    Public Const HcPurchaseRate As String = "Purchase Rate"
    Public Const HcSaleRate As String = "Sale Rate"
    Public Const HcMRP As String = "MRP"


    Public Const Col1DocType As String = "Doc Type"
    Public Const Col1DocDate As String = "Doc Date"
    Public Const Col1PartyName As String = "Party Name"
    Public Const Col1IssuedQty As String = "Issued Qty"
    Public Const Col1ReceiveQty As String = "Receive Qty"

    Dim mQry As String = ""
    Public Property SearchCode() As String
        Get
            SearchCode = mSearchCode
        End Get
        Set(ByVal value As String)
            mSearchCode = value
        End Set
    End Property
    Private Sub FrmReconcile_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Ini_Grid()
        AgL.GridDesign(DglMain)
        AgL.GridDesign(Dgl1)
        TxtBarcode.Focus()
    End Sub
    Private Sub FillGrid()
        Dim mQry As String = ""


        LblTotalPcs.Text = 0
        LblTotalQty.Text = 0
        LblTotalAmount.Text = 0

        mQry = "SELECT Bc.Code As BarcodeCode, BC.PurchaseRate, BC.SaleRate, BC.MRP,
                I.Description As ItemDesc, IC.Description As ItemCategoryDesc, IG.Description As ItemGroupDesc, 
                D1.Description as Dimension1Desc, D2.Description as Dimension2Desc,
                D3.Description as Dimension3Desc, D4.Description as Dimension4Desc, Size.Description as SizeDesc               
                FROM Barcode Bc 
                LEFT JOIN Item Sku ON Sku.Code = Bc.Item
                LEFT JOIN ItemType It On Sku.ItemType = It.Code
                Left Join Item IC On Sku.ItemCategory = IC.Code
                Left Join Item IG On Sku.ItemGroup = IG.Code
                LEFT JOIN Item I ON Sku.BaseItem = I.Code
                LEFT JOIN Item D1 ON Sku.Dimension1 = D1.Code
                LEFT JOIN Item D2 ON Sku.Dimension2 = D2.Code
                LEFT JOIN Item D3 ON Sku.Dimension3 = D3.Code
                LEFT JOIN Item D4 ON Sku.Dimension4 = D4.Code
                LEFT JOIN Item Size ON Sku.Size = Size.Code
                WHERE Bc.Description = '" & TxtBarcode.Text & "' "
        Dim DtHead As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

        If DtHead.Rows.Count > 0 Then
            TxtBarcode.Tag = AgL.XNull(DtHead.Rows(0)("BarcodeCode"))
            DglMain.Item(Col1Value, rowItemCategory).Value = AgL.XNull(DtHead.Rows(0)("ItemCategoryDesc"))
            DglMain.Item(Col1Value, rowItemGroup).Value = AgL.XNull(DtHead.Rows(0)("ItemGroupDesc"))
            DglMain.Item(Col1Value, rowItem).Value = AgL.XNull(DtHead.Rows(0)("ItemDesc"))
            DglMain.Item(Col1Value, rowDimension1).Value = AgL.XNull(DtHead.Rows(0)("Dimension1Desc"))
            DglMain.Item(Col1Value, rowDimension2).Value = AgL.XNull(DtHead.Rows(0)("Dimension2Desc"))
            DglMain.Item(Col1Value, rowDimension3).Value = AgL.XNull(DtHead.Rows(0)("Dimension3Desc"))
            DglMain.Item(Col1Value, rowDimension4).Value = AgL.XNull(DtHead.Rows(0)("Dimension4Desc"))
            DglMain.Item(Col1Value, rowSize).Value = AgL.XNull(DtHead.Rows(0)("SizeDesc"))
            DglMain.Item(Col1Value, rowPurchaseRate).Value = AgL.VNull(DtHead.Rows(0)("PurchaseRate"))
            DglMain.Item(Col1Value, rowSaleRate).Value = AgL.VNull(DtHead.Rows(0)("SaleRate"))
            DglMain.Item(Col1Value, rowMRP).Value = AgL.VNull(DtHead.Rows(0)("MRP"))
        Else
            TxtBarcode.Tag = ""
            DglMain.Item(Col1Value, rowItemCategory).Value = ""
            DglMain.Item(Col1Value, rowItemGroup).Value = ""
            DglMain.Item(Col1Value, rowItem).Value = ""
            DglMain.Item(Col1Value, rowDimension1).Value = ""
            DglMain.Item(Col1Value, rowDimension2).Value = ""
            DglMain.Item(Col1Value, rowDimension3).Value = ""
            DglMain.Item(Col1Value, rowDimension4).Value = ""
            DglMain.Item(Col1Value, rowSize).Value = ""
            DglMain.Item(Col1Value, rowPurchaseRate).Value = ""
            DglMain.Item(Col1Value, rowSaleRate).Value = ""
            DglMain.Item(Col1Value, rowMRP).Value = ""
        End If

        If AgL.XNull(TxtBarcode.Tag) <> "" Then
            mQry = "SELECT Vt.Description AS [Doc Type], H.V_Date AS [Doc Date], H.V_Type || '-' || H.ManualRefNo As [Doc No], 
                    Sg.Name AS [Party Name], 0 AS [Issued Qty], B.Qty  AS [Receive Qty] 
                    FROM Barcode B
                    LEFT JOIN PurchInvoice H ON B.GenDocID = H.DocID
                    LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type
                    LEFT JOIN Subgroup Sg ON H.Vendor = Sg.Subcode
                    WHERE B.Code = " & TxtBarcode.Tag & ""
            mQry += " UNION ALL "
            mQry += "SELECT Vt.Description AS [Doc Type], L.V_Date AS [Doc Date], L.V_Type || '-' || L.RecId As [Doc No], 
                    Sg.Name AS [Party Name], L.Qty_Iss AS [Issued Qty], L.Qty_Rec AS [Receive Qty] 
                    FROM Stock L 
                    LEFT JOIN Voucher_Type Vt ON L.V_Type = Vt.V_Type
                    LEFT JOIN Subgroup Sg ON L.SubCode = Sg.Subcode
                    WHERE L.Barcode = " & TxtBarcode.Tag & ""
            Dim DtTemp As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
            Dgl1.DataSource = DtTemp
            Dgl1.Columns(Col1DocType).Width = 200
            Dgl1.Columns(Col1DocDate).Width = 200
            Dgl1.Columns(Col1PartyName).Width = 200
            Dgl1.Columns(Col1IssuedQty).Width = 200
            Dgl1.Columns(Col1ReceiveQty).Width = 200
            Dgl1.DefaultCellStyle.WrapMode = DataGridViewTriState.True
            Dgl1.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells)
        Else
            Dgl1.DataSource = Nothing
        End If
    End Sub
    Private Sub FrmImportPurchaseFromExcel_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If

        If Me.ActiveControl IsNot Nothing Then
            If TypeOf (Me.ActiveControl) Is TextBox Then
                If Not CType(Me.ActiveControl, TextBox).Multiline Then
                    If e.KeyCode = Keys.Return Then SendKeys.Send("{Tab}")
                End If
            ElseIf (TypeOf (Me.ActiveControl) Is AgControls.AgDataGrid) Then
                If e.KeyCode = Keys.Return Then SendKeys.Send("{Tab}")
            End If

            'If e.KeyCode = Keys.Insert Then OpenLinkForm(Me.ActiveControl)
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
    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, 0)
    End Sub

    Private Sub TxtBarcode_Validating(sender As Object, e As CancelEventArgs) Handles TxtBarcode.Validating
        FillGrid()
    End Sub
    Public Sub Ini_Grid()
        AgL.AddAgDataGrid(Dgl1, Pnl2)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 35
        Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        AgL.GridDesign(Dgl1)
        Dgl1.Name = "Dgl1"
        AgL.FSetDimensionCaptionForHorizontalGrid(Dgl1, AgL)

        Dgl1.AllowUserToAddRows = False
        Dgl1.AgSkipReadOnlyColumns = True
        Dgl1.AllowUserToOrderColumns = True

        Dgl1.Anchor = AnchorStyles.Bottom + AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Top
        Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        AgCL.GridSetiingShowXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1, False)

        DglMain.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(DglMain, ColSNo, 35, 5, ColSNo, False, True, False)
            .AddAgTextColumn(DglMain, Col1Head, 360, 255, Col1Head, True, True)
            .AddAgTextColumn(DglMain, Col1HeadOriginal, 260, 255, Col1HeadOriginal, False, True)
            .AddAgTextColumn(DglMain, Col1Mandatory, 10, 20, Col1Mandatory, True, True)
            .AddAgTextColumn(DglMain, Col1Value, 480, 255, Col1Value, True, True)
        End With
        AgL.AddAgDataGrid(DglMain, Pnl1)
        DglMain.EnableHeadersVisualStyles = False
        DglMain.ColumnHeadersHeight = 35
        DglMain.AgSkipReadOnlyColumns = True
        DglMain.AllowUserToAddRows = False
        DglMain.Name = "DglMain"
        DglMain.Tag = "VerticalGrid"

        DglMain.Rows.Add(13)
        DglMain.Item(Col1Head, rowCurrentGodown).Value = HcCurrentGodown
        DglMain.Item(Col1Head, rowCurrentProcess).Value = HcCurrentProcess
        DglMain.Item(Col1Head, rowItemCategory).Value = HcItemCategory
        DglMain.Item(Col1Head, rowItemGroup).Value = HcItemGroup
        DglMain.Item(Col1Head, rowItem).Value = HcItem
        DglMain.Item(Col1Head, rowItemCategory).Value = HcItemCategory
        DglMain.Item(Col1Head, rowDimension1).Value = HcDimension1
        DglMain.Item(Col1Head, rowDimension2).Value = HcDimension2
        DglMain.Item(Col1Head, rowDimension3).Value = HcDimension3
        DglMain.Item(Col1Head, rowDimension4).Value = HcDimension4
        DglMain.Item(Col1Head, rowSize).Value = HcSize
        DglMain.Item(Col1Head, rowPurchaseRate).Value = HcPurchaseRate
        DglMain.Item(Col1Head, rowSaleRate).Value = HcSaleRate
        DglMain.Item(Col1Head, rowMRP).Value = HcMRP
        For I As Integer = 0 To DglMain.Rows.Count - 1
            If AgL.XNull(DglMain(Col1HeadOriginal, I).Value) = "" Then
                DglMain(Col1HeadOriginal, I).Value = DglMain(Col1Head, I).Value
            End If
        Next

        AgL.FSetDimensionCaptionForVerticalGrid(DglMain, AgL)

        ApplyUISetting()
    End Sub
    Private Sub ApplyUISetting()
        ClsMain.GetUISetting_WithDataTables(DglMain, Me.Name, AgL.PubDivCode, AgL.PubSiteCode,
                "", "", "", "", ClsMain.GridTypeConstants.VerticalGrid)
    End Sub
End Class