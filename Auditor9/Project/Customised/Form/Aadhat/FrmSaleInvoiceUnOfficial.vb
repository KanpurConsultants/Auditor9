Imports System.ComponentModel
Imports AgLibrary.ClsMain.agConstants
Imports Customised.ClsMain

Public Class FrmSaleInvoiceUnOfficial
    Dim mSearchCode$ = ""

    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Public Const ColSNo As String = "S.No."
    Public Const Col1SaleInvoiceDocId As String = "SaleInvoiceDocId"
    Public Const Col1PurchInvoiceDocId As String = "PurchInvoiceDocId"
    Public Const Col1Supplier As String = "Supplier"
    Public Const Col1InvoiceNo As String = "Invoice No"
    Public Const Col1InvoiceDate As String = "Invoice Date"
    Public Const Col1ItemGroup As String = "Brand"
    Public Const Col1InvoiceDiscountPer As String = "Invoice Discount @"
    Public Const Col1DiscountPer As String = "Discount @"
    Public Const Col1AdditionalDiscountPer As String = "Pcs Less"
    Public Const Col1AdditionPer As String = "Addition @"
    Public Const Col1Amount As String = "Purch Amount"

    Public Const Col1WInvoiceNo As String = "W Invoice No"
    Public Const Col1WInvoiceDate As String = "W Invoice Date"
    Public Const Col1WQty As String = "W Qty"
    Public Const Col1WFreight As String = "W Freight"
    Public Const Col1WPacking As String = "W Packing"
    Public Const Col1WAmount As String = "W Invoice Amount"


    Public WithEvents Dgl2 As New AgControls.AgDataGrid
    Public Const Col2SaleInvoiceDocId As String = "SaleInvoiceDocId"
    Public Const Col2Party As String = "Party"
    Public Const Col2InvoiceNo As String = "Invoice No"
    Public Const Col2InvoiceDate As String = "Invoice Date"
    Public Const Col2ItemGroup As String = "Brand"
    Public Const Col2DiscountPer As String = "Discount @"
    Public Const Col2AdditionalDiscountPer As String = "Additional Discount @"
    Public Const Col2AdditionPer As String = "Addition @"
    Public Const Col2Amount As String = "Sale Amount"

    Public Const Col2WSaleOrderDocId As String = "W SaleOrderDocId"
    Public Const Col2WInvoiceNo As String = "W Invoice No"
    Public Const Col2WInvoiceDate As String = "W Invoice Date"
    Public Const Col2WQty As String = "W Qty"
    Public Const Col2WFreight As String = "W Freight"
    Public Const Col2WPacking As String = "W Packing"
    Public Const Col2WAmount As String = "W Invoice Amount"


    Public WithEvents Dgl3 As New AgControls.AgDataGrid
    Public Const Col3DrCr As String = "Debit/Credit Note"
    Public Const Col3V_Date As String = "Date"
    Public Const Col3PartyName As String = "Party Name"
    Public Const Col3ReasonAc As String = "Reason Ac"
    Public Const Col3Amount As String = "Amount"
    Public Const Col3Remark As String = "Remark"

    Dim mQry As String = ""
    Dim mOrderNCat As String = "SO"
    Public mDbPath As String = ""
    Dim Connection_Pakka As New SQLite.SQLiteConnection

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
            .AddAgTextColumn(Dgl1, Col1SaleInvoiceDocId, 100, 0, Col1SaleInvoiceDocId, False, True)
            .AddAgTextColumn(Dgl1, Col1PurchInvoiceDocId, 100, 0, Col1PurchInvoiceDocId, False, True)
            .AddAgTextColumn(Dgl1, Col1Supplier, 100, 0, Col1Supplier, True, True)
            .AddAgTextColumn(Dgl1, Col1InvoiceNo, 80, 0, Col1InvoiceNo, True, True)
            .AddAgDateColumn(Dgl1, Col1InvoiceDate, 80, Col1InvoiceDate, True, True)
            .AddAgTextColumn(Dgl1, Col1ItemGroup, 90, 0, Col1ItemGroup, True, True)
            .AddAgNumberColumn(Dgl1, Col1InvoiceDiscountPer, 90, 0, 0, False, Col1InvoiceDiscountPer, False, True)
            .AddAgNumberColumn(Dgl1, Col1DiscountPer, 90, 0, 0, False, Col1DiscountPer,, True)
            .AddAgNumberColumn(Dgl1, Col1AdditionalDiscountPer, 90, 0, 0, False, Col1AdditionalDiscountPer,, True)
            .AddAgNumberColumn(Dgl1, Col1AdditionPer, 90, 0, 0, False, Col1AdditionPer, False, True)
            .AddAgNumberColumn(Dgl1, Col1Amount, 90, 0, 0, False, Col1Amount,, True)

            .AddAgTextColumn(Dgl1, Col1WInvoiceNo, 90, 0, Col1WInvoiceNo, True, False)
            .AddAgDateColumn(Dgl1, Col1WInvoiceDate, 90, Col1WInvoiceDate, True, False)
            .AddAgNumberColumn(Dgl1, Col1WQty, 90, 0, 0, False, Col1WQty)
            .AddAgNumberColumn(Dgl1, Col1WFreight, 90, 0, 0, False, Col1WFreight)
            .AddAgNumberColumn(Dgl1, Col1WPacking, 90, 0, 0, False, Col1WPacking)
            .AddAgNumberColumn(Dgl1, Col1WAmount, 100, 0, 0, False, Col1WAmount)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 50
        Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Comprehensive
        AgL.GridDesign(Dgl1)
        Dgl1.AllowUserToAddRows = False
        Dgl1.AgSkipReadOnlyColumns = True
        Dgl1.AllowUserToOrderColumns = True
        Dgl1.Anchor = AnchorStyles.Bottom + AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Top
        Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        AgCL.GridSetiingShowXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1, False)
        Dgl1.Anchor = AnchorStyles.Top + AnchorStyles.Left + AnchorStyles.Right


        Dgl2.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl2, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl2, Col2SaleInvoiceDocId, 100, 0, Col2SaleInvoiceDocId, False, True)
            .AddAgTextColumn(Dgl2, Col2Party, 100, 0, Col2Party, True, True)
            .AddAgTextColumn(Dgl2, Col2InvoiceNo, 80, 0, Col2InvoiceNo, True, True)
            .AddAgDateColumn(Dgl2, Col2InvoiceDate, 80, Col2InvoiceDate, True, True)
            .AddAgTextColumn(Dgl2, Col2ItemGroup, 90, 0, Col2ItemGroup, True, True)
            .AddAgNumberColumn(Dgl2, Col2DiscountPer, 90, 0, 0, False, Col2DiscountPer,, True)
            .AddAgNumberColumn(Dgl2, Col2AdditionalDiscountPer, 90, 0, 0, False, Col2AdditionalDiscountPer, False, True)
            .AddAgNumberColumn(Dgl2, Col2AdditionPer, 90, 0, 0, False, Col2AdditionPer,, True)
            .AddAgNumberColumn(Dgl2, Col2Amount, 90, 0, 0, False, Col2Amount,, True)

            .AddAgTextColumn(Dgl2, Col2WSaleOrderDocId, 100, 0, Col2WSaleOrderDocId, False, True)
            .AddAgTextColumn(Dgl2, Col2WInvoiceNo, 90, 0, Col2WInvoiceNo, True, False)
            .AddAgDateColumn(Dgl2, Col2WInvoiceDate, 90, Col2WInvoiceDate, True, False)
            .AddAgNumberColumn(Dgl2, Col2WQty, 90, 0, 0, False, Col2WQty)
            .AddAgNumberColumn(Dgl2, Col2WFreight, 90, 0, 0, False, Col2WFreight)
            .AddAgNumberColumn(Dgl2, Col2WPacking, 90, 0, 0, False, Col2WPacking)
            .AddAgNumberColumn(Dgl2, Col2WAmount, 100, 0, 0, False, Col2WAmount,, True)
        End With
        AgL.AddAgDataGrid(Dgl2, Pnl2)
        Dgl2.EnableHeadersVisualStyles = False
        Dgl2.ColumnHeadersHeight = 55
        Dgl2.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Comprehensive
        AgL.GridDesign(Dgl2)
        Dgl2.AllowUserToAddRows = False
        Dgl2.AgSkipReadOnlyColumns = True
        Dgl2.AllowUserToOrderColumns = True
        Dgl2.Anchor = AnchorStyles.Bottom + AnchorStyles.Left + AnchorStyles.Right
        Dgl2.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        AgCL.GridSetiingShowXml(Me.Text & Dgl2.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl2, False)
        Dgl2.Anchor = AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Bottom



        Dgl3.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl3, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl3, Col3DrCr, 100, 0, Col3DrCr, True, True)
            .AddAgDateColumn(Dgl3, Col3V_Date, 80, Col3V_Date, True, True)
            .AddAgTextColumn(Dgl3, Col3PartyName, 300, 0, Col3PartyName, True, True)
            .AddAgTextColumn(Dgl3, Col3ReasonAc, 300, 0, Col3ReasonAc, False, True)
            .AddAgNumberColumn(Dgl3, Col3Amount, 200, 0, 0, False, Col3Amount,, True)
            .AddAgTextColumn(Dgl3, Col3Remark, 300, 0, Col3Remark, True, True)
        End With
        AgL.AddAgDataGrid(Dgl3, Pnl3)
        Dgl3.EnableHeadersVisualStyles = False
        Dgl3.ColumnHeadersHeight = 40
        Dgl3.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Comprehensive
        AgL.GridDesign(Dgl3)
        Dgl3.AllowUserToAddRows = False
        Dgl3.AgSkipReadOnlyColumns = True
        Dgl3.AllowUserToOrderColumns = True
        Dgl3.Anchor = AnchorStyles.Bottom + AnchorStyles.Left + AnchorStyles.Right
        Dgl3.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        AgCL.GridSetiingShowXml(Me.Text & Dgl3.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl3, False)
        Dgl3.Anchor = AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Bottom
    End Sub

    Private Sub FrmReconcile_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        mDbPath = AgL.INIRead(StrPath + "\" + IniName, "CompanyInfo", "ActualDBPath", "")
        Connection_Pakka.ConnectionString = "DataSource=" & mDbPath & ";Version=3;Password=" & AgLibrary.ClsConstant.PubDbPassword & ";"
        Connection_Pakka.Open()

        Ini_Grid()
        TxtOrderNo.Focus()
        TxtPartyName.Enabled = False
        Me.WindowState = FormWindowState.Maximized
        FSyncSaleOrders()
        FSeedRequiredData()
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
    Private Sub Dgl2_ColumnDisplayIndexChanged(sender As Object, e As DataGridViewColumnEventArgs) Handles Dgl2.ColumnDisplayIndexChanged
        Try
            AgCL.GridSetiingWriteXml(Me.Text & Dgl2.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl2)
        Catch ex As Exception
        End Try
    End Sub
    Private Sub Dgl2_ColumnWidthChanged(sender As Object, e As DataGridViewColumnEventArgs) Handles Dgl2.ColumnWidthChanged
        Try
            AgCL.GridSetiingWriteXml(Me.Text & Dgl2.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl2)
        Catch ex As Exception
        End Try
    End Sub
    Private Sub FSave(DocId As String, Sr As Integer, SelectValue As String, mRowIndex As Integer)
    End Sub
    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, 0)
    End Sub
    Private Sub Txt_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtOrderNo.Validating
        Dim DrTemp As DataRow() = Nothing
        Dim DtTemp As DataTable = Nothing
        Dim I As Integer = 0
        Dim mDiscountQry As String = ""

        Try
            Select Case sender.NAME
                Case TxtOrderNo.Name
                    mQry = "Select H.SaleToParty, Sg.Name As SaleToPartyName
                            From SaleInvoice H  With (NoLock)
                            LEFT JOIN SubGroup Sg On H.SaleToParty = Sg.SubCode
                            Where H.DocId = '" & TxtOrderNo.Tag & "'"
                    DtTemp = AgL.FillData(mQry, Connection_Pakka).Tables(0)
                    If DtTemp.Rows.Count > 0 Then
                        TxtPartyName.Tag = AgL.XNull(DtTemp.Rows(0)("SaleToParty"))
                        TxtPartyName.Text = AgL.XNull(DtTemp.Rows(0)("SaleToPartyName"))
                    End If

                    Dim bSaleInvoiceDocIdStr As String = ""

                    mQry = "Select Si.DocId As SaleInvoiceDocId, Sg.Name As BillToPartyName, Si.DocId As InvoiceDocId, Si.ManualRefNo As invoiceNo, Si.V_Date As InvoiceDate, 
                            Ig.Code As ItemGroup, Ig.Description As ItemGroupDesc,
                            Max(Si.BillToParty) As SubCode, Max(Si.Site_Code) As Site_Code, 
                            Max(Si.Div_Code) As Div_Code, Max(Si.Net_Amount) As Amount,
                            Max(H.V_Type) As OrderV_Type, Max(H.ManualRefNo) As OrderManualRefNo
                            From (Select * From SaleInvoice Where DocId = '" & TxtOrderNo.Tag & "') H 
                            LEFT JOIN SaleInvoiceDetail L ON H.DocId = L.DocId
                            LEFT JOIN SaleInvoiceDetail Sil On L.Docid = Sil.SaleInvoice And L.Sr = Sil.SaleInvoiceSr
                            LEFT JOIN SaleInvoice Si ON Sil.DocID = Si.DocId
                            LEFT JOIN SubGroup Sg ON Si.BillToParty = Sg.SubCode 
                            LEFT JOIN Item I On Sil.Item = I.Code
                            LEFT JOIN ItemGroup Ig On I.ItemGroup = Ig.Code
                            Where Sil.DocId Is Not Null
                            And I.Description Not In ('" & ItemCode.Packing & "','" & ItemCode.Freight & "')
                            Group By Si.ManualRefNo, Si.V_Date, Ig.Code, Ig.Description "
                    DtTemp = AgL.FillData(mQry, Connection_Pakka).Tables(0)
                    If DtTemp.Rows.Count > 0 Then
                        Dgl2.RowCount = 1 : Dgl2.Rows.Clear()
                        For I = 0 To DtTemp.Rows.Count - 1
                            Dgl2.Rows.Add()
                            Dgl2.Item(ColSNo, I).Value = Dgl2.Rows.Count
                            Dgl2.Item(Col2SaleInvoiceDocId, I).Value = AgL.XNull(DtTemp.Rows(I)("SaleInvoiceDocId"))
                            Dgl2.Item(Col2Party, I).Value = AgL.XNull(DtTemp.Rows(I)("BillToPartyName"))
                            Dgl2.Item(Col2InvoiceNo, I).Tag = AgL.XNull(DtTemp.Rows(I)("InvoiceDocId"))
                            Dgl2.Item(Col2InvoiceNo, I).Value = AgL.XNull(DtTemp.Rows(I)("InvoiceNo"))
                            Dgl2.Item(Col2InvoiceDate, I).Value = AgL.XNull(DtTemp.Rows(I)("InvoiceDate"))
                            Dgl2.Item(Col2ItemGroup, I).Value = AgL.XNull(DtTemp.Rows(I)("ItemGroupDesc"))
                            Dgl2.Item(Col2Amount, I).Value = AgL.XNull(DtTemp.Rows(I)("Amount"))

                            If bSaleInvoiceDocIdStr <> "" Then bSaleInvoiceDocIdStr = bSaleInvoiceDocIdStr + ","
                            bSaleInvoiceDocIdStr = bSaleInvoiceDocIdStr + Dgl2.Item(Col2InvoiceNo, I).Tag

                            Dim DTDiscounts As DataTable = FGetDiscountRates(AgL.XNull(DtTemp.Rows(I)("SubCode")),
                                                AgL.XNull(DtTemp.Rows(I)("Site_Code")),
                                                AgL.XNull(DtTemp.Rows(I)("Div_Code")),
                                                AgL.XNull(DtTemp.Rows(I)("ItemGroup")))
                            If DTDiscounts.Rows.Count > 0 Then
                                Dgl2.Item(Col2DiscountPer, I).Value = AgL.VNull(DTDiscounts.Rows(0)("Default_DiscountPerSale"))
                                Dgl2.Item(Col2AdditionalDiscountPer, I).Value = AgL.VNull(DTDiscounts.Rows(0)("Default_AdditionalDiscountPerSale"))
                                Dgl2.Item(Col2AdditionPer, I).Value = AgL.VNull(DTDiscounts.Rows(0)("Default_AdditionPerSale"))
                            End If

                            If AgL.XNull(DtTemp.Rows(I)("OrderV_Type")) <> "" And
                                AgL.XNull(DtTemp.Rows(I)("OrderManualRefNo")) <> "" Then
                                mQry = " Select DocId 
                                        From SaleInvoice 
                                        Where V_Type = '" & AgL.XNull(DtTemp.Rows(I)("OrderV_Type")) & "'
                                        And ManualRefNo = '" & AgL.XNull(DtTemp.Rows(I)("OrderManualRefNo")) & "'"
                                Dgl2.Item(Col2WSaleOrderDocId, I).Value = AgL.XNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar)
                            End If
                        Next I
                    Else
                        mQry = " Select H.V_Type As OrderV_Type, H.ManualRefNo As OrderManualRefNo, 
                                H.SaleToParty As SUbCode, H.Site_Code, H.Div_Code, L.Item As ItemGroup,
                                I.Description As ItemGroupDesc, Sg.Name As BillToPartyName,
                                Supp.Name As SupplierName
                                From SaleInvoice H 
                                LEFT JOIN SaleInvoiceDetail L ON H.DocId = L.DocId
                                LEFT JOIN Item I ON L.Item = I.Code
                                LEFT JOIN SubGroup Sg On H.BillToParty = Sg.SubCode
                                LEFT JOIN SubGroup Supp On I.DefaultSupplier = Supp.SubCode
                                Where H.DocId = '" & TxtOrderNo.Tag & "'"
                        Dim DtSaleOrderDetail As DataTable = AgL.FillData(mQry, Connection_Pakka).Tables(0)
                        If DtSaleOrderDetail.Rows.Count > 0 Then
                            Dgl2.Rows.Add()
                            Dgl2.Item(ColSNo, I).Value = Dgl2.Rows.Count
                            Dgl2.Item(Col2SaleInvoiceDocId, I).Value = ""
                            Dgl2.Item(Col2Party, I).Value = AgL.XNull(DtSaleOrderDetail.Rows(I)("BillToPartyName"))
                            Dgl2.Item(Col2InvoiceNo, I).Tag = ""
                            Dgl2.Item(Col2InvoiceNo, I).Value = ""
                            Dgl2.Item(Col2InvoiceDate, I).Value = ""
                            Dgl2.Item(Col2ItemGroup, I).Value = AgL.XNull(DtSaleOrderDetail.Rows(I)("ItemGroupDesc"))
                            Dgl2.Item(Col2Amount, I).Value = 0

                            Dim DTSaleDiscounts As DataTable = FGetDiscountRates(AgL.XNull(DtSaleOrderDetail.Rows(I)("SubCode")),
                                AgL.XNull(DtSaleOrderDetail.Rows(I)("Site_Code")),
                                AgL.XNull(DtSaleOrderDetail.Rows(I)("Div_Code")),
                                AgL.XNull(DtSaleOrderDetail.Rows(I)("ItemGroup")))
                            If DTSaleDiscounts.Rows.Count > 0 Then
                                Dgl2.Item(Col2DiscountPer, I).Value = AgL.VNull(DTSaleDiscounts.Rows(0)("Default_DiscountPerSale"))
                                Dgl2.Item(Col2AdditionalDiscountPer, I).Value = AgL.VNull(DTSaleDiscounts.Rows(0)("Default_AdditionalDiscountPerSale"))
                                Dgl2.Item(Col2AdditionPer, I).Value = AgL.VNull(DTSaleDiscounts.Rows(0)("Default_AdditionPerSale"))
                            End If

                            If AgL.XNull(DtSaleOrderDetail.Rows(I)("OrderV_Type")) <> "" And
                                    AgL.XNull(DtSaleOrderDetail.Rows(I)("OrderManualRefNo")) <> "" Then
                                mQry = " Select DocId 
                                        From SaleInvoice 
                                        Where V_Type = '" & AgL.XNull(DtSaleOrderDetail.Rows(I)("OrderV_Type")) & "'
                                        And ManualRefNo = '" & AgL.XNull(DtSaleOrderDetail.Rows(I)("OrderManualRefNo")) & "'"
                                Dgl2.Item(Col2WSaleOrderDocId, I).Value = AgL.XNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar)
                            End If

                            'For Purchase Data

                            Dgl1.Rows.Add()
                            Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count
                            Dgl1.Item(Col1SaleInvoiceDocId, I).Value = ""
                            Dgl1.Item(Col1PurchInvoiceDocId, I).Value = ""
                            Dgl1.Item(Col1Supplier, I).Value = AgL.XNull(DtSaleOrderDetail.Rows(I)("SupplierName"))
                            Dgl1.Item(Col1InvoiceNo, I).Value = ""
                            Dgl1.Item(Col1InvoiceDate, I).Value = ""
                            Dgl1.Item(Col1ItemGroup, I).Value = AgL.XNull(DtSaleOrderDetail.Rows(I)("ItemGroupDesc"))
                            Dgl1.Item(Col1InvoiceDiscountPer, I).Value = 0
                            Dgl1.Item(Col1Amount, I).Value = 0

                            mQry = "Select IG.Default_DiscountPerPurchase, IG.Default_AdditionalDiscountPerPurchase,
                                    0 As Default_AdditionPerSale
                                    From ItemGroup IG  
                                    Where IG.Code ='" & AgL.XNull(DtSaleOrderDetail.Rows(I)("ItemGroup")) & "'"
                            Dim DTPurchaseDiscounts As DataTable = AgL.FillData(mQry, Connection_Pakka).Tables(0)
                            If DTPurchaseDiscounts.Rows.Count > 0 Then
                                Dgl1.Item(Col1DiscountPer, I).Value = AgL.VNull(DTPurchaseDiscounts.Rows(0)("Default_DiscountPerPurchase"))
                                Dgl1.Item(Col1AdditionalDiscountPer, I).Value = AgL.VNull(DTPurchaseDiscounts.Rows(0)("Default_AdditionalDiscountPerPurchase"))
                                Dgl1.Item(Col1AdditionPer, I).Value = AgL.VNull(DTPurchaseDiscounts.Rows(0)("Default_AdditionPerSale"))
                            End If
                        End If
                    End If

                    mQry = "Select H.GenDocId As SaleInvoiceDocId, H.DocId As PurchInvoiceDocId, Supp.Name As SupplierName, H.ManualRefNo As InvoiceNo, H.V_Date As InvoiceDate, 
                            Ig.Code as ItemGroup, Ig.Description As ItemGroupDesc,
                            Max(H.Net_Amount) As Amount, 
                            Max(H.Vendor) As SubCode, Max(H.Site_Code) As Site_Code, 
                            Max(H.Div_Code) As Div_Code, Min(L.DiscountPer) As InvoiceDiscountPer  
                            From PurchInvoice H  With (NoLock)
                            LEFT JOIN PurchInvoiceDetail L On H.DocId = L.DocId
                            LEFT JOIN Item I On L.Item = I.Code
                            LEFT JOIN ItemGroup Ig ON I.ItemGroup = Ig.Code
                            LEFT JOIN SubGroup Supp On H.Vendor = Supp.SubCode
                            Where H.GenDocId In ('" & bSaleInvoiceDocIdStr.Replace(",", "','") & "')
                            And I.Description Not In ('" & ItemCode.Packing & "','" & ItemCode.Freight & "')
                            Group By H.GenDocId, H.DocId, Supp.Name, H.ManualRefNo, H.V_Date, Ig.Code, Ig.Description "
                    DtTemp = AgL.FillData(mQry, Connection_Pakka).Tables(0)
                    If DtTemp.Rows.Count > 0 Then
                        Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
                        For I = 0 To DtTemp.Rows.Count - 1
                            Dgl1.Rows.Add()
                            Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count
                            Dgl1.Item(Col1SaleInvoiceDocId, I).Value = AgL.XNull(DtTemp.Rows(I)("SaleInvoiceDocId"))
                            Dgl1.Item(Col1PurchInvoiceDocId, I).Value = AgL.XNull(DtTemp.Rows(I)("PurchInvoiceDocId"))
                            Dgl1.Item(Col1Supplier, I).Value = AgL.XNull(DtTemp.Rows(I)("SupplierName"))
                            Dgl1.Item(Col1InvoiceNo, I).Value = AgL.XNull(DtTemp.Rows(I)("InvoiceNo"))
                            Dgl1.Item(Col1InvoiceDate, I).Value = AgL.XNull(DtTemp.Rows(I)("InvoiceDate"))
                            Dgl1.Item(Col1ItemGroup, I).Value = AgL.XNull(DtTemp.Rows(I)("ItemGroupDesc"))
                            Dgl1.Item(Col1InvoiceDiscountPer, I).Value = AgL.VNull(DtTemp.Rows(I)("InvoiceDiscountPer"))
                            Dgl1.Item(Col1Amount, I).Value = AgL.VNull(DtTemp.Rows(I)("Amount"))

                            mQry = "Select IG.Default_DiscountPerPurchase, IG.Default_AdditionalDiscountPerPurchase,
                                    0 As Default_AdditionPerSale
                                    From ItemGroup IG  
                                    Where IG.Code ='" & AgL.XNull(DtTemp.Rows(I)("ItemGroup")) & "'"
                            Dim DTDiscounts As DataTable = AgL.FillData(mQry, Connection_Pakka).Tables(0)
                            If DTDiscounts.Rows.Count > 0 Then
                                Dgl1.Item(Col1DiscountPer, I).Value = AgL.VNull(DTDiscounts.Rows(0)("Default_DiscountPerPurchase"))
                                Dgl1.Item(Col1AdditionalDiscountPer, I).Value = AgL.VNull(DTDiscounts.Rows(0)("Default_AdditionalDiscountPerPurchase"))
                                Dgl1.Item(Col1AdditionPer, I).Value = AgL.VNull(DTDiscounts.Rows(0)("Default_AdditionPerSale"))
                            End If
                        Next I
                    Else

                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function FGetDiscountRates(SubCode As String, Site_Code As String, Div_Code As String, ItemGroup As String) As DataTable
        mQry = "Select IFNull(Max(DiscountPer),0) As Default_DiscountPerSale,
                IFNull(Max(AdditionalDiscountPer),0) As Default_AdditionalDiscountPerSale,
                IFNull(Max(AdditionPer),0) As Default_AdditionPerSale
                From (
                    Select * From SubgroupSiteDivisionDetail 
                    Where SubCode = '" & SubCode & "'
                    And Site_Code = '" & Site_Code & "'
                    And Div_Code = '" & Div_Code & "') As VSubGroup
                LEFT JOIN ItemGroupRateType H  With (NoLock) On VSubGroup.RateType = H.RateType
                Where Code = '" & ItemGroup & "' "
        Dim DTDiscounts As DataTable = AgL.FillData(mQry, Connection_Pakka).Tables(0)
        Return DTDiscounts
    End Function
    Private Sub KeyDown_Form(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If Me.ActiveControl IsNot Nothing Then
            If e.KeyCode = Keys.Return Then SendKeys.Send("{Tab}")
        End If
    End Sub
    Private Sub TxtBuyer_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtOrderNo.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then Exit Sub
            Select Case sender.name
                Case TxtOrderNo.Name
                    If e.KeyCode <> Keys.Enter Then
                        If sender.AgHelpDataset Is Nothing Then

                            Dim Connection_Pakka_Temp As New SQLite.SQLiteConnection
                            Connection_Pakka_Temp.ConnectionString = "DataSource=" & mDbPath & ";Version=3;Password=" & AgLibrary.ClsConstant.PubDbPassword & ";"
                            Connection_Pakka_Temp.Open()

                            mQry = " CREATE Temp TABLE SaleOrder_Temp( "
                            mQry += " V_Type nvarchar(10), "
                            mQry += " ManualRefNo nvarchar(20) "
                            mQry += " ) "
                            AgL.Dman_ExecuteNonQry(mQry, Connection_Pakka_Temp)

                            mQry = "Select SOrder.V_Type, SOrder.ManualRefNo
                                    From SaleInvoice H 
                                    LEFT JOIN SaleInvoiceDetail L ON H.DocId = L.DocId
                                    LEFT JOIN SaleInvoice SOrder On L.SaleInvoice = SOrder.DocId
                                    LEFT JOIN Voucher_Type Vt On SOrder.V_Type = Vt.V_Type
                                    Where Vt.NCat = '" & Ncat.SaleOrder & "'
                                    Group By SOrder.V_Type, SOrder.ManualRefNo "
                            Dim DtTemp As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)


                            For I As Integer = 0 To DtTemp.Rows.Count - 1
                                mQry = "INSERT INTO SaleOrder_Temp(V_Type, ManualRefNo)
                                    Select '" & DtTemp.Rows(I)("V_Type") & "', '" & DtTemp.Rows(I)("ManualRefNo") & "'"
                                AgL.Dman_ExecuteNonQry(mQry, Connection_Pakka_Temp)
                            Next

                            mQry = "Select H.DocId, H.ManualRefNo As OrderNo, H.V_Date As OrderDate
                                    FROM SaleInvoice H With (NoLock) 
                                    LEFT JOIN Voucher_Type Vt With (NoLock) On H.V_Type = Vt.V_Type
                                    LEFT JOIN SaleOrder_Temp Sot On H.V_Type = Sot.V_Type And H.ManualRefNo = Sot.ManualRefNo
                                    WHERE Vt.NCat = '" & Ncat.SaleOrder & "' 
                                    And Sot.ManualRefNo Is Null
                                    ORDER By H.V_Date, H.ManualRefNo "
                            TxtOrderNo.AgHelpDataSet() = AgL.FillData(mQry, Connection_Pakka_Temp)

                            Connection_Pakka_Temp.Close()
                            Connection_Pakka_Temp.Dispose()
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub Dgl1_EditingControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.EditingControl_KeyDown
        Try
            Dim bRowIndex As Integer = Dgl1.CurrentCell.RowIndex
            Dim bColumnIndex As Integer = Dgl1.CurrentCell.ColumnIndex

            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Supplier
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.Item(ColSNo, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                            'Dim bParentSubCode = AgL.Dman_Execute("SELECT Max(Sg.Subcode) AS SubCode
                            '        FROM SaleInvoice H 
                            '        LEFT JOIN SaleInvoiceDetail L ON H.DocID = L.DocID
                            '        LEFT JOIN Item I ON L.Item = I.Code
                            '        LEFT JOIN ItemGroup Ig ON I.ItemGroup = Ig.Code
                            '        LEFT JOIN ItemGroupPerson Igp ON Ig.Code = Igp.ItemGroup
                            '        LEFT JOIN Subgroup Sg ON Igp.Person = Sg.Subcode
                            '        WHERE H.DocID = '" & Dgl1.Item(Col1SaleOrderNo, bRowIndex).Tag & "'
                            '        AND Sg.Subcode IS NOT NULL", AgL.GCn).ExecuteScalar()

                            'mQry = " WITH cte AS  (
                            '        SELECT Sg.SubCode, Sg.Parent , Sg.name
                            '        FROM Subgroup Sg WHERE Sg.Subcode  = '" & bParentSubCode & "'
                            '        UNION ALL
                            '        SELECT Sg.SubCode, Sg.Parent, Sg.Name
                            '        FROM Subgroup Sg JOIN cte c ON Sg.Parent = c.SubCode
                            '    ) SELECT SubCode, Name FROM cte "
                            'Dgl1.Item(ColSNo, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                        End If

                        If Dgl1.AgHelpDataSet(Col1Supplier) Is Nothing Then
                            Dgl1.AgHelpDataSet(Col1Supplier) = Dgl1.Item(ColSNo, Dgl1.CurrentCell.RowIndex).Tag
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Dgl1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        Try
            If Dgl1.CurrentCell Is Nothing Then Exit Sub

            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Supplier
                    Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) = Nothing
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Function FDataValidation() As Boolean
        For I As Integer = 0 To Dgl1.Rows.Count - 1
            If Dgl1.Item(Col1WInvoiceNo, I).Value = "" Then
                MsgBox("W Purchase Invoice No is blank for line no " & Dgl1.Item(ColSNo, I).Value & "", MsgBoxStyle.Information)
                FDataValidation = False
                Exit Function
            End If

            If Dgl1.Item(Col1WInvoiceDate, I).Value = "" Then
                MsgBox("W Invoice Date is blank for line no " & Dgl1.Item(ColSNo, I).Value & "", MsgBoxStyle.Information)
                FDataValidation = False
                Exit Function
            End If

            If Dgl1.Item(Col1WQty, I).Value = "" Or Dgl1.Item(Col1WQty, I).Value = 0 Then
                MsgBox("W Qty is blank for line no " & Dgl1.Item(ColSNo, I).Value & "", MsgBoxStyle.Information)
                FDataValidation = False
                Exit Function
            End If
        Next

        For I As Integer = 0 To Dgl2.Rows.Count - 1
            If Dgl2.Item(Col2WInvoiceNo, I).Value = "" Then
                MsgBox("W Purchase Invoice No is blank for line no " & Dgl2.Item(ColSNo, I).Value & "", MsgBoxStyle.Information)
                FDataValidation = False
                Exit Function
            End If

            If Dgl2.Item(Col2WInvoiceDate, I).Value = "" Then
                MsgBox("W Invoice Date is blank for line no " & Dgl2.Item(ColSNo, I).Value & "", MsgBoxStyle.Information)
                FDataValidation = False
                Exit Function
            End If

            If Dgl2.Item(Col2WQty, I).Value = "" Or Dgl2.Item(Col2WQty, I).Value = 0 Then
                MsgBox("W Qty is blank for line no " & Dgl2.Item(ColSNo, I).Value & "", MsgBoxStyle.Information)
                FDataValidation = False
                Exit Function
            End If
        Next
        FDataValidation = True
    End Function
    Private Sub FrmSaleInvoiceUnOfficial_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing

    End Sub

    Private Sub FSyncSaleOrders()
        Dim mTrans As String = ""

        mQry = "Select Ic.Description As ItemCategoryDesc, Ig.Description As ItemGroupDesc, I.*
                From SaleInvoice H 
                LEFT JOIN SaleInvoiceDetail L ON H.DocID = L.DocID
                LEFT JOIN Item I On L.Item = I.Code 
                LEFT JOIN ItemCategory Ic On I.ItemCategory = Ic.Code
                LEFT JOIN ItemGroup Ig On I.ItemGroup = Ig.Code
                LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                Where Vt.NCat = '" & Ncat.SaleOrder & "'"
        Dim DtItemSource As DataTable = AgL.FillData(mQry, Connection_Pakka).Tables(0)

        Dim mPartyQry As String = " Select VReg.SalesTaxNo, VReg.PanNo, VReg.AadharNo,  
                C.CityName, S.Description As StateName, Ag.GroupName, Sg.*
                From SaleInvoice H 
                LEFT JOIN SubGroup Sg On H.SaleToParty = Sg.SubCode
                LEFT JOIN AcGroup Ag On Sg.GroupCode = Ag.GroupCode
                LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                LEFT JOIN City C ON Sg.CityCode = C.CityCode 
                LEFT JOIN State S ON C.State = S.Code
                LEFT JOIN (
	                SELECT Sgr.Subcode, 
	                Max(CASE WHEN Sgr.RegistrationType =  'Sales Tax No' THEN Sgr.RegistrationNo ELSE NULL END) AS SalesTaxNo,
	                Max(CASE WHEN Sgr.RegistrationType =  'PAN No' THEN Sgr.RegistrationNo ELSE NULL END) AS PanNo,
	                Max(CASE WHEN Sgr.RegistrationType =  'AADHAR NO' THEN Sgr.RegistrationNo ELSE NULL END) AS AadharNo
	                FROM SubgroupRegistration Sgr 
	                GROUP BY Sgr.Subcode         
                ) AS VReg ON H.SaleToParty = VReg.SubCode
                Where Vt.NCat = '" & Ncat.SaleOrder & "'"
        Dim DtSaleToPartySource As DataTable = AgL.FillData(mPartyQry, Connection_Pakka).Tables(0)

        Dim DtBillToPartySource As DataTable = AgL.FillData(mPartyQry.Replace("H.SaleToParty", "H.BillToParty"), Connection_Pakka).Tables(0)

        mQry = " Select Sg.Name As BillToPartyName,  H.*
            From SaleInvoice H 
            LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
            LEFT JOIN SubGroup Sg On H.BillToParty = Sg.SubCode
            Where Vt.NCat = '" & mOrderNCat & "'"
        Dim DtSaleOrderSource As DataTable = AgL.FillData(mQry, Connection_Pakka).Tables(0)
        Dim DtSaleOrderDestination As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = " SELECT H.V_Type, H.ManualRefNo, I.Description As ItemDesc, L.*
                FROM SaleInvoice H 
                LEFT JOIN SaleInvoiceDetail L ON H.DocID = L.DocID
                LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type
                LEFT JOIN Item I ON L.Item = I.Code
                WHERE Vt.NCat = '" & mOrderNCat & "'"
        Dim DtSaleOrderDetailSource As DataTable = AgL.FillData(mQry, Connection_Pakka).Tables(0)

        Try
            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = "Begin"
            FSyncItem(DtItemSource, AgL.GCn, AgL.ECmd)
            FSyncSubGroup(DtSaleToPartySource, AgL.GCn, AgL.ECmd)
            FSyncSubGroup(DtBillToPartySource, AgL.GCn, AgL.ECmd)
            FSyncSaleData(DtSaleOrderSource, DtSaleOrderDestination, DtSaleOrderDetailSource, AgL.GCn, AgL.ECmd)
            AgL.ETrans.Commit()
            mTrans = "Commit"
        Catch ex As Exception
            AgL.ETrans.Rollback()
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub FSyncSaleInvoices()
        Dim bSaleInvoiceDocIdStr As String = ""
        For I As Integer = 0 To Dgl2.Rows.Count - 1
            If Dgl2.Item(Col2SaleInvoiceDocId, I).Value <> "" Then
                If bSaleInvoiceDocIdStr <> "" Then bSaleInvoiceDocIdStr += ","
                bSaleInvoiceDocIdStr += Dgl2.Item(Col2SaleInvoiceDocId, I).Value
            End If
        Next

        mQry = "Select Ic.Description As ItemCategoryDesc, Ig.Description As ItemGroupDesc, I.*
                From SaleInvoice H 
                LEFT JOIN SaleInvoiceDetail L ON H.DocID = L.DocID
                LEFT JOIN Item I On L.Item = I.Code 
                LEFT JOIN ItemCategory Ic On I.ItemCategory = Ic.Code
                LEFT JOIN ItemGroup Ig On I.ItemGroup = Ig.Code
                LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                Where H.DocId In ('" & bSaleInvoiceDocIdStr.Replace(",", "','") & "')"
        Dim DtItemSource As DataTable = AgL.FillData(mQry, Connection_Pakka).Tables(0)

        Dim mPartyQry As String = " Select VReg.SalesTaxNo, VReg.PanNo, VReg.AadharNo,  
                C.CityName, S.Description As StateName, Ag.GroupName, Sg.*
                From SaleInvoice H 
                LEFT JOIN SubGroup Sg On H.SaleToParty = Sg.SubCode
                LEFT JOIN AcGroup Ag On Sg.GroupCode = Ag.GroupCode
                LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                LEFT JOIN City C ON Sg.CityCode = C.CityCode 
                LEFT JOIN State S ON C.State = S.Code
                LEFT JOIN (
	                SELECT Sgr.Subcode, 
	                Max(CASE WHEN Sgr.RegistrationType =  'Sales Tax No' THEN Sgr.RegistrationNo ELSE NULL END) AS SalesTaxNo,
	                Max(CASE WHEN Sgr.RegistrationType =  'PAN No' THEN Sgr.RegistrationNo ELSE NULL END) AS PanNo,
	                Max(CASE WHEN Sgr.RegistrationType =  'AADHAR NO' THEN Sgr.RegistrationNo ELSE NULL END) AS AadharNo
	                FROM SubgroupRegistration Sgr 
	                GROUP BY Sgr.Subcode         
                ) AS VReg ON H.SaleToParty = VReg.SubCode
                Where H.DocId In ('" & bSaleInvoiceDocIdStr.Replace(",", "','") & "')"
        Dim DtPartySource As DataTable = AgL.FillData(mPartyQry, Connection_Pakka).Tables(0)

        Dim DtBillToPartySource As DataTable = AgL.FillData(mPartyQry.Replace("H.SaleToParty", "H.BillToParty"), Connection_Pakka).Tables(0)

        mQry = " Select Sg.Name As BillToPartyName,  H.*
            From SaleInvoice H 
            LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
            LEFT JOIN SubGroup Sg On H.BillToParty = Sg.SubCode
            Where H.DocId In ('" & bSaleInvoiceDocIdStr.Replace(",", "','") & "')"
        Dim DtHeaderSource As DataTable = AgL.FillData(mQry, Connection_Pakka).Tables(0)
        Dim DtHeaderDestination As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = " SELECT H.V_Type, H.ManualRefNo, I.Description As ItemDesc, OrderH.V_Type As OrderV_Type, 
                OrderH.ManualRefNo As OrderManualRefNo, L.*
                FROM SaleInvoice H 
                LEFT JOIN SaleInvoiceDetail L ON H.DocID = L.DocID
                LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type
                LEFT JOIN SaleInvoice OrderH On L.SaleInvoice = OrderH.DocId
                LEFT JOIN Item I ON L.Item = I.Code
                Where H.DocId In ('" & bSaleInvoiceDocIdStr.Replace(",", "','") & "')"
        Dim DtLineDetailSource As DataTable = AgL.FillData(mQry, Connection_Pakka).Tables(0)

        FSyncItem(DtItemSource, AgL.GCn, AgL.ECmd)
        FSyncSubGroup(DtPartySource, AgL.GCn, AgL.ECmd)
        FSyncSubGroup(DtBillToPartySource, AgL.GCn, AgL.ECmd)
        FSyncSaleData(DtHeaderSource, DtHeaderDestination, DtLineDetailSource, AgL.GCn, AgL.ECmd)
    End Sub
    Private Sub FSyncPurchaseInvoices()
        Dim bPurchaseInvoiceDocIdStr As String = ""
        For I As Integer = 0 To Dgl1.Rows.Count - 1
            If Dgl1.Item(Col1PurchInvoiceDocId, I).Value <> "" Then
                If bPurchaseInvoiceDocIdStr <> "" Then bPurchaseInvoiceDocIdStr += ","
                bPurchaseInvoiceDocIdStr += Dgl1.Item(Col1PurchInvoiceDocId, I).Value
            End If
        Next

        mQry = "Select Ic.Description As ItemCategoryDesc, Ig.Description As ItemGroupDesc, I.*
                From PurchInvoice H 
                LEFT JOIN PurchInvoiceDetail L ON H.DocID = L.DocID
                LEFT JOIN Item I On L.Item = I.Code 
                LEFT JOIN ItemCategory Ic On I.ItemCategory = Ic.Code
                LEFT JOIN ItemGroup Ig On I.ItemGroup = Ig.Code
                LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                Where H.DocId In ('" & bPurchaseInvoiceDocIdStr.Replace(",", "','") & "')"
        Dim DtItemSource As DataTable = AgL.FillData(mQry, Connection_Pakka).Tables(0)

        Dim mPartyQry As String = " Select VReg.SalesTaxNo, VReg.PanNo, VReg.AadharNo,  
                C.CityName, S.Description As StateName, Ag.GroupName, Sg.*
                From PurchInvoice H 
                LEFT JOIN SubGroup Sg On H.Vendor = Sg.SubCode
                LEFT JOIN AcGroup Ag On Sg.GroupCode = Ag.GroupCode
                LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                LEFT JOIN City C ON Sg.CityCode = C.CityCode 
                LEFT JOIN State S ON C.State = S.Code
                LEFT JOIN (
	                SELECT Sgr.Subcode, 
	                Max(CASE WHEN Sgr.RegistrationType =  'Sales Tax No' THEN Sgr.RegistrationNo ELSE NULL END) AS SalesTaxNo,
	                Max(CASE WHEN Sgr.RegistrationType =  'PAN No' THEN Sgr.RegistrationNo ELSE NULL END) AS PanNo,
	                Max(CASE WHEN Sgr.RegistrationType =  'AADHAR NO' THEN Sgr.RegistrationNo ELSE NULL END) AS AadharNo
	                FROM SubgroupRegistration Sgr 
	                GROUP BY Sgr.Subcode         
                ) AS VReg ON H.Vendor = VReg.SubCode
                Where H.DocId In ('" & bPurchaseInvoiceDocIdStr.Replace(",", "','") & "')"
        Dim DtPartySource As DataTable = AgL.FillData(mPartyQry, Connection_Pakka).Tables(0)

        Dim DtBillToPartySource As DataTable = AgL.FillData(mPartyQry.Replace("H.Vendor", "H.BillToParty"), Connection_Pakka).Tables(0)

        mQry = " Select Sg.Name As BillToPartyName,  H.*
            From PurchInvoice H 
            LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
            LEFT JOIN SubGroup Sg On H.BillToParty = Sg.SubCode
            Where H.DocId In ('" & bPurchaseInvoiceDocIdStr.Replace(",", "','") & "')"
        Dim DtHeaderSource As DataTable = AgL.FillData(mQry, Connection_Pakka).Tables(0)
        Dim DtHeaderDestination As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = " SELECT H.V_Type, H.ManualRefNo, I.Description As ItemDesc, L.*
                FROM PurchInvoice H 
                LEFT JOIN PurchInvoiceDetail L ON H.DocID = L.DocID
                LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type
                LEFT JOIN Item I ON L.Item = I.Code
                Where H.DocId In ('" & bPurchaseInvoiceDocIdStr.Replace(",", "','") & "')"
        Dim DtLineDetailSource As DataTable = AgL.FillData(mQry, Connection_Pakka).Tables(0)

        FSyncItem(DtItemSource, AgL.GCn, AgL.ECmd)
        FSyncSubGroup(DtPartySource, AgL.GCn, AgL.ECmd)
        FSyncSubGroup(DtBillToPartySource, AgL.GCn, AgL.ECmd)
        FSyncPurchaseData(DtHeaderSource, DtHeaderDestination, DtLineDetailSource, AgL.GCn, AgL.ECmd)
    End Sub

    Public Sub FSyncSaleData(DtHeaderSource As DataTable,
                                  DtHeaderDestination As DataTable,
                                  DtLineDetailSource As DataTable,
                                  Conn As Object, Cmd As Object)
        Dim ErrorLog As String = ""
        Dim DtMain As DataTable = Nothing
        Dim I As Integer
        Dim J As Integer
        Dim StrErrLog As String = ""


        For I = 0 To DtHeaderSource.Rows.Count - 1
            Dim DtRowSaleOrderDestination As DataRow() = DtHeaderDestination.Select("ManualRefNo = " + AgL.Chk_Text(AgL.XNull(DtHeaderSource.Rows(I)("ManualRefNo"))) +
                                                                                       "And V_Type = " + AgL.Chk_Text(AgL.XNull(DtHeaderSource.Rows(I)("V_Type"))))
            If DtRowSaleOrderDestination.Length = 0 Then
                Dim SaleInvoiceTableList(0) As FrmSaleInvoiceDirect.StructSaleInvoice
                Dim SaleInvoiceTable As New FrmSaleInvoiceDirect.StructSaleInvoice

                SaleInvoiceTable.DocID = ""
                SaleInvoiceTable.V_Type = AgL.XNull(DtHeaderSource.Rows(I)("V_Type"))
                SaleInvoiceTable.V_Prefix = AgL.XNull(DtHeaderSource.Rows(I)("V_Prefix"))
                SaleInvoiceTable.Site_Code = AgL.PubSiteCode
                SaleInvoiceTable.Div_Code = AgL.PubDivCode
                SaleInvoiceTable.V_No = 0
                SaleInvoiceTable.V_Date = AgL.XNull(DtHeaderSource.Rows(I)("V_Date"))
                SaleInvoiceTable.ManualRefNo = AgL.XNull(DtHeaderSource.Rows(I)("ManualRefNo"))
                SaleInvoiceTable.SaleToParty = ""
                SaleInvoiceTable.SaleToPartyName = AgL.XNull(DtHeaderSource.Rows(I)("SaleToPartyName"))
                SaleInvoiceTable.AgentCode = ""
                SaleInvoiceTable.AgentName = ""
                SaleInvoiceTable.BillToPartyCode = ""
                SaleInvoiceTable.BillToPartyName = AgL.XNull(DtHeaderSource.Rows(I)("BillToPartyName"))
                SaleInvoiceTable.SaleToPartyAddress = AgL.XNull(DtHeaderSource.Rows(I)("SaleToPartyAddress"))
                SaleInvoiceTable.SaleToPartyCity = AgL.XNull(DtHeaderSource.Rows(I)("SaleToPartyCity"))
                SaleInvoiceTable.SaleToPartyMobile = AgL.XNull(DtHeaderSource.Rows(I)("SaleToPartyMobile"))
                SaleInvoiceTable.SaleToPartySalesTaxNo = AgL.XNull(DtHeaderSource.Rows(I)("SaleToPartySalesTaxNo"))
                SaleInvoiceTable.ShipToAddress = AgL.XNull(DtHeaderSource.Rows(I)("ShipToAddress"))
                SaleInvoiceTable.SalesTaxGroupParty = AgL.XNull(DtHeaderSource.Rows(I)("SalesTaxGroupParty"))
                SaleInvoiceTable.PlaceOfSupply = AgL.XNull(DtHeaderSource.Rows(I)("PlaceOfSupply"))
                SaleInvoiceTable.StructureCode = AgL.XNull(DtHeaderSource.Rows(I)("Structure"))
                SaleInvoiceTable.CustomFields = AgL.XNull(DtHeaderSource.Rows(I)("CustomFields"))
                SaleInvoiceTable.SaleToPartyDocNo = AgL.XNull(DtHeaderSource.Rows(I)("SaleToPartyDocNo"))
                SaleInvoiceTable.SaleToPartyDocDate = AgL.XNull(DtHeaderSource.Rows(I)("SaleToPartyDocDate"))
                SaleInvoiceTable.ReferenceDocId = ""
                SaleInvoiceTable.Remarks = AgL.XNull(DtHeaderSource.Rows(I)("Remarks"))
                SaleInvoiceTable.Status = "Active"
                SaleInvoiceTable.EntryBy = AgL.PubUserName
                SaleInvoiceTable.EntryDate = AgL.GetDateTime(AgL.GcnRead)
                SaleInvoiceTable.ApproveBy = ""
                SaleInvoiceTable.ApproveDate = ""
                SaleInvoiceTable.MoveToLog = ""
                SaleInvoiceTable.MoveToLogDate = ""
                SaleInvoiceTable.UploadDate = ""

                SaleInvoiceTable.Gross_Amount = AgL.VNull(DtHeaderSource.Rows(I)("Gross_Amount"))
                SaleInvoiceTable.Taxable_Amount = AgL.VNull(DtHeaderSource.Rows(I)("Taxable_Amount"))
                SaleInvoiceTable.Tax1 = AgL.VNull(DtHeaderSource.Rows(I)("Tax1"))
                SaleInvoiceTable.Tax2 = AgL.VNull(DtHeaderSource.Rows(I)("Tax2"))
                SaleInvoiceTable.Tax3 = AgL.VNull(DtHeaderSource.Rows(I)("Tax3"))
                SaleInvoiceTable.Tax4 = AgL.VNull(DtHeaderSource.Rows(I)("Tax4"))
                SaleInvoiceTable.Tax5 = AgL.VNull(DtHeaderSource.Rows(I)("Tax5"))
                SaleInvoiceTable.SubTotal1 = AgL.VNull(DtHeaderSource.Rows(I)("SubTotal1"))
                SaleInvoiceTable.Other_Charge = AgL.VNull(DtHeaderSource.Rows(I)("Other_Charge"))
                SaleInvoiceTable.Deduction = AgL.VNull(DtHeaderSource.Rows(I)("Deduction"))
                SaleInvoiceTable.Round_Off = AgL.VNull(DtHeaderSource.Rows(I)("Round_Off"))
                SaleInvoiceTable.Net_Amount = AgL.VNull(DtHeaderSource.Rows(I)("Net_Amount"))

                Dim DtSaleInvoiceDetail_ForHeader As New DataTable
                For M As Integer = 0 To DtLineDetailSource.Columns.Count - 1
                    Dim DColumn As New DataColumn
                    DColumn.ColumnName = DtLineDetailSource.Columns(M).ColumnName
                    DtSaleInvoiceDetail_ForHeader.Columns.Add(DColumn)
                Next

                Dim DtRowSaleInvoiceDetail_ForHeader As DataRow() = DtLineDetailSource.Select("ManualRefNo = " + AgL.Chk_Text(AgL.XNull(DtHeaderSource.Rows(I)("ManualRefNo"))) +
                                                                                       "And V_Type = " + AgL.Chk_Text(AgL.XNull(DtHeaderSource.Rows(I)("V_Type"))))
                If DtRowSaleInvoiceDetail_ForHeader.Length > 0 Then
                    For M As Integer = 0 To DtRowSaleInvoiceDetail_ForHeader.Length - 1
                        DtSaleInvoiceDetail_ForHeader.Rows.Add()
                        For N As Integer = 0 To DtSaleInvoiceDetail_ForHeader.Columns.Count - 1
                            DtSaleInvoiceDetail_ForHeader.Rows(M)(N) = DtRowSaleInvoiceDetail_ForHeader(M)(N)
                        Next
                    Next
                End If


                For J = 0 To DtSaleInvoiceDetail_ForHeader.Rows.Count - 1
                    SaleInvoiceTable.Line_Sr = AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Sr"))
                    SaleInvoiceTable.Line_ItemCode = ""
                    SaleInvoiceTable.Line_ItemName = AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("ItemDesc"))
                    SaleInvoiceTable.Line_Specification = AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Specification"))
                    SaleInvoiceTable.Line_SalesTaxGroupItem = AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("SalesTaxGroupItem"))
                    SaleInvoiceTable.Line_ReferenceNo = AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("ReferenceNo"))
                    SaleInvoiceTable.Line_DocQty = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("DocQty"))
                    SaleInvoiceTable.Line_FreeQty = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("FreeQty"))
                    SaleInvoiceTable.Line_Qty = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Qty"))
                    SaleInvoiceTable.Line_Unit = AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Unit"))
                    SaleInvoiceTable.Line_Pcs = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Pcs"))
                    SaleInvoiceTable.Line_UnitMultiplier = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("UnitMultiplier"))
                    SaleInvoiceTable.Line_DealUnit = AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("DealUnit"))
                    SaleInvoiceTable.Line_DocDealQty = AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("DocDealQty"))



                    If DtSaleInvoiceDetail_ForHeader.Columns.Contains("OrderManualRefNo") Then
                        mQry = " Select DocId
                                From SaleInvoice 
                                Where ManualRefNo = '" & AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("OrderManualRefNo")) & "'
                                And V_Type = '" & AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("OrderV_Type")) & "'"
                        Dim DtSaleOrder As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

                        If DtSaleOrder.Rows.Count > 0 Then
                            SaleInvoiceTable.Line_SaleInvoice = AgL.XNull(DtSaleOrder.Rows(0)("DocId"))
                            SaleInvoiceTable.Line_SaleInvoiceSr = AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(0)("SaleInvoiceSr"))
                        End If
                    End If



                    SaleInvoiceTable.Line_Rate = AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Rate"))
                    SaleInvoiceTable.Line_DiscountPer = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("DiscountPer"))
                    SaleInvoiceTable.Line_DiscountAmount = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("DiscountAmount"))
                    SaleInvoiceTable.Line_AdditionalDiscountPer = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("AdditionalDiscountPer"))
                    SaleInvoiceTable.Line_AdditionalDiscountAmount = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("AdditionalDiscountAmount"))
                    SaleInvoiceTable.Line_Amount = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Amount"))
                    SaleInvoiceTable.Line_Remark = AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Remark"))
                    SaleInvoiceTable.Line_BaleNo = AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("BaleNo"))
                    SaleInvoiceTable.Line_LotNo = AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("LotNo"))
                    SaleInvoiceTable.Line_ReferenceDocId = ""
                    SaleInvoiceTable.Line_GrossWeight = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("GrossWeight"))
                    SaleInvoiceTable.Line_NetWeight = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("NetWeight"))
                    SaleInvoiceTable.Line_Gross_Amount = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Gross_Amount"))
                    SaleInvoiceTable.Line_Taxable_Amount = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Taxable_Amount"))
                    SaleInvoiceTable.Line_Tax1_Per = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Tax1_Per"))
                    SaleInvoiceTable.Line_Tax1 = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Tax1"))
                    SaleInvoiceTable.Line_Tax2_Per = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Tax2_Per"))
                    SaleInvoiceTable.Line_Tax2 = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Tax2"))
                    SaleInvoiceTable.Line_Tax3_Per = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Tax3_Per"))
                    SaleInvoiceTable.Line_Tax3 = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Tax3"))
                    SaleInvoiceTable.Line_Tax4_Per = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Tax4_Per"))
                    SaleInvoiceTable.Line_Tax4 = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Tax4"))
                    SaleInvoiceTable.Line_Tax5_Per = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Tax5_Per"))
                    SaleInvoiceTable.Line_Tax5 = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Tax5"))
                    SaleInvoiceTable.Line_SubTotal1 = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("SubTotal1"))
                    SaleInvoiceTable.Line_Other_Charge = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Other_Charge"))
                    SaleInvoiceTable.Line_Deduction = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Deduction"))
                    SaleInvoiceTable.Line_Round_Off = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Round_Off"))
                    SaleInvoiceTable.Line_Net_Amount = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Net_Amount"))

                    SaleInvoiceTableList(UBound(SaleInvoiceTableList)) = SaleInvoiceTable
                    ReDim Preserve SaleInvoiceTableList(UBound(SaleInvoiceTableList) + 1)
                Next
                FrmSaleInvoiceDirect.InsertSaleInvoice(SaleInvoiceTableList)
            End If
        Next
    End Sub
    Public Sub FSyncPurchaseData(DtHeaderSource As DataTable,
                                  DtHeaderDestination As DataTable,
                                  DtLineDetailSource As DataTable,
                                  Conn As Object, Cmd As Object)
        Dim ErrorLog As String = ""
        Dim DtMain As DataTable = Nothing
        Dim I As Integer
        Dim J As Integer
        Dim StrErrLog As String = ""


        For I = 0 To DtHeaderSource.Rows.Count - 1
            Dim DtRowSaleOrderDestination As DataRow() = DtHeaderDestination.Select("ManualRefNo = " + AgL.Chk_Text(AgL.XNull(DtHeaderSource.Rows(I)("ManualRefNo"))) +
                                                                                       "And V_Type = " + AgL.Chk_Text(AgL.XNull(DtHeaderSource.Rows(I)("V_Type"))))
            If DtRowSaleOrderDestination.Length = 0 Then
                Dim PurchInvoiceTableList(0) As FrmPurchInvoiceDirect.StructPurchInvoice
                Dim PurchInvoiceTable As New FrmPurchInvoiceDirect.StructPurchInvoice

                PurchInvoiceTable.DocID = ""
                PurchInvoiceTable.V_Type = AgL.XNull(DtHeaderSource.Rows(I)("V_Type"))
                PurchInvoiceTable.V_Prefix = AgL.XNull(DtHeaderSource.Rows(I)("V_Prefix"))
                PurchInvoiceTable.Site_Code = AgL.PubSiteCode
                PurchInvoiceTable.Div_Code = AgL.PubDivCode
                PurchInvoiceTable.V_No = 0
                PurchInvoiceTable.V_Date = AgL.XNull(DtHeaderSource.Rows(I)("V_Date"))
                PurchInvoiceTable.ManualRefNo = AgL.XNull(DtHeaderSource.Rows(I)("ManualRefNo"))
                PurchInvoiceTable.Vendor = ""
                PurchInvoiceTable.VendorName = AgL.XNull(DtHeaderSource.Rows(I)("VendorName"))
                PurchInvoiceTable.AgentCode = ""
                PurchInvoiceTable.AgentName = ""
                PurchInvoiceTable.BillToPartyCode = ""
                PurchInvoiceTable.BillToPartyName = AgL.XNull(DtHeaderSource.Rows(I)("BillToPartyName"))
                PurchInvoiceTable.VendorAddress = AgL.XNull(DtHeaderSource.Rows(I)("VendorAddress"))
                PurchInvoiceTable.VendorCity = AgL.XNull(DtHeaderSource.Rows(I)("VendorCity"))
                PurchInvoiceTable.VendorMobile = AgL.XNull(DtHeaderSource.Rows(I)("VendorMobile"))
                PurchInvoiceTable.VendorSalesTaxNo = AgL.XNull(DtHeaderSource.Rows(I)("VendorSalesTaxNo"))
                PurchInvoiceTable.ShipToAddress = AgL.XNull(DtHeaderSource.Rows(I)("ShipToAddress"))
                PurchInvoiceTable.SalesTaxGroupParty = AgL.XNull(DtHeaderSource.Rows(I)("SalesTaxGroupParty"))
                PurchInvoiceTable.PlaceOfSupply = AgL.XNull(DtHeaderSource.Rows(I)("PlaceOfSupply"))
                PurchInvoiceTable.StructureCode = AgL.XNull(DtHeaderSource.Rows(I)("Structure"))
                PurchInvoiceTable.CustomFields = AgL.XNull(DtHeaderSource.Rows(I)("CustomFields"))
                PurchInvoiceTable.VendorDocNo = AgL.XNull(DtHeaderSource.Rows(I)("VendorDocNo"))
                PurchInvoiceTable.VendorDocDate = AgL.XNull(DtHeaderSource.Rows(I)("VendorDocDate"))
                PurchInvoiceTable.ReferenceDocId = ""
                PurchInvoiceTable.Remarks = AgL.XNull(DtHeaderSource.Rows(I)("Remarks"))
                PurchInvoiceTable.Status = "Active"
                PurchInvoiceTable.EntryBy = AgL.PubUserName
                PurchInvoiceTable.EntryDate = AgL.GetDateTime(AgL.GcnRead)
                PurchInvoiceTable.ApproveBy = ""
                PurchInvoiceTable.ApproveDate = ""
                PurchInvoiceTable.MoveToLog = ""
                PurchInvoiceTable.MoveToLogDate = ""
                PurchInvoiceTable.UploadDate = ""

                PurchInvoiceTable.Gross_Amount = AgL.VNull(DtHeaderSource.Rows(I)("Gross_Amount"))
                PurchInvoiceTable.Taxable_Amount = AgL.VNull(DtHeaderSource.Rows(I)("Taxable_Amount"))
                PurchInvoiceTable.Tax1 = AgL.VNull(DtHeaderSource.Rows(I)("Tax1"))
                PurchInvoiceTable.Tax2 = AgL.VNull(DtHeaderSource.Rows(I)("Tax2"))
                PurchInvoiceTable.Tax3 = AgL.VNull(DtHeaderSource.Rows(I)("Tax3"))
                PurchInvoiceTable.Tax4 = AgL.VNull(DtHeaderSource.Rows(I)("Tax4"))
                PurchInvoiceTable.Tax5 = AgL.VNull(DtHeaderSource.Rows(I)("Tax5"))
                PurchInvoiceTable.SubTotal1 = AgL.VNull(DtHeaderSource.Rows(I)("SubTotal1"))
                PurchInvoiceTable.Other_Charge = AgL.VNull(DtHeaderSource.Rows(I)("Other_Charge"))
                PurchInvoiceTable.Deduction = AgL.VNull(DtHeaderSource.Rows(I)("Deduction"))
                PurchInvoiceTable.Round_Off = AgL.VNull(DtHeaderSource.Rows(I)("Round_Off"))
                PurchInvoiceTable.Net_Amount = AgL.VNull(DtHeaderSource.Rows(I)("Net_Amount"))

                Dim DtSaleInvoiceDetail_ForHeader As New DataTable
                For M As Integer = 0 To DtLineDetailSource.Columns.Count - 1
                    Dim DColumn As New DataColumn
                    DColumn.ColumnName = DtLineDetailSource.Columns(M).ColumnName
                    DtSaleInvoiceDetail_ForHeader.Columns.Add(DColumn)
                Next

                Dim DtRowSaleInvoiceDetail_ForHeader As DataRow() = DtLineDetailSource.Select("ManualRefNo = " + AgL.Chk_Text(AgL.XNull(DtHeaderSource.Rows(I)("ManualRefNo"))) +
                                                                                       "And V_Type = " + AgL.Chk_Text(AgL.XNull(DtHeaderSource.Rows(I)("V_Type"))))
                If DtRowSaleInvoiceDetail_ForHeader.Length > 0 Then
                    For M As Integer = 0 To DtRowSaleInvoiceDetail_ForHeader.Length - 1
                        DtSaleInvoiceDetail_ForHeader.Rows.Add()
                        For N As Integer = 0 To DtSaleInvoiceDetail_ForHeader.Columns.Count - 1
                            DtSaleInvoiceDetail_ForHeader.Rows(M)(N) = DtRowSaleInvoiceDetail_ForHeader(M)(N)
                        Next
                    Next
                End If


                For J = 0 To DtSaleInvoiceDetail_ForHeader.Rows.Count - 1
                    PurchInvoiceTable.Line_Sr = AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Sr"))
                    PurchInvoiceTable.Line_ItemCode = ""
                    PurchInvoiceTable.Line_ItemName = AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("ItemDesc"))
                    PurchInvoiceTable.Line_Specification = AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Specification"))
                    PurchInvoiceTable.Line_SalesTaxGroupItem = AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("SalesTaxGroupItem"))
                    PurchInvoiceTable.Line_ReferenceNo = AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("ReferenceNo"))
                    PurchInvoiceTable.Line_DocQty = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("DocQty"))
                    PurchInvoiceTable.Line_FreeQty = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("FreeQty"))
                    PurchInvoiceTable.Line_Qty = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Qty"))
                    PurchInvoiceTable.Line_Unit = AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Unit"))
                    PurchInvoiceTable.Line_Pcs = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Pcs"))
                    PurchInvoiceTable.Line_UnitMultiplier = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("UnitMultiplier"))
                    PurchInvoiceTable.Line_DealUnit = AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("DealUnit"))
                    PurchInvoiceTable.Line_DocDealQty = AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("DocDealQty"))
                    PurchInvoiceTable.Line_Rate = AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Rate"))
                    PurchInvoiceTable.Line_DiscountPer = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("DiscountPer"))
                    PurchInvoiceTable.Line_DiscountAmount = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("DiscountAmount"))
                    PurchInvoiceTable.Line_AdditionalDiscountPer = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("AdditionalDiscountPer"))
                    PurchInvoiceTable.Line_AdditionalDiscountAmount = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("AdditionalDiscountAmount"))
                    PurchInvoiceTable.Line_Amount = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Amount"))
                    PurchInvoiceTable.Line_Remark = AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Remark"))
                    PurchInvoiceTable.Line_BaleNo = AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("BaleNo"))
                    PurchInvoiceTable.Line_LotNo = AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("LotNo"))
                    PurchInvoiceTable.Line_ReferenceDocId = ""
                    PurchInvoiceTable.Line_GrossWeight = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("GrossWeight"))
                    PurchInvoiceTable.Line_NetWeight = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("NetWeight"))
                    PurchInvoiceTable.Line_Gross_Amount = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Gross_Amount"))
                    PurchInvoiceTable.Line_Taxable_Amount = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Taxable_Amount"))
                    PurchInvoiceTable.Line_Tax1_Per = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Tax1_Per"))
                    PurchInvoiceTable.Line_Tax1 = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Tax1"))
                    PurchInvoiceTable.Line_Tax2_Per = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Tax2_Per"))
                    PurchInvoiceTable.Line_Tax2 = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Tax2"))
                    PurchInvoiceTable.Line_Tax3_Per = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Tax3_Per"))
                    PurchInvoiceTable.Line_Tax3 = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Tax3"))
                    PurchInvoiceTable.Line_Tax4_Per = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Tax4_Per"))
                    PurchInvoiceTable.Line_Tax4 = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Tax4"))
                    PurchInvoiceTable.Line_Tax5_Per = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Tax5_Per"))
                    PurchInvoiceTable.Line_Tax5 = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Tax5"))
                    PurchInvoiceTable.Line_SubTotal1 = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("SubTotal1"))

                    PurchInvoiceTable.Line_Other_Charge = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Other_Charge"))
                    PurchInvoiceTable.Line_Deduction = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Deduction"))
                    PurchInvoiceTable.Line_Round_Off = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Round_Off"))
                    PurchInvoiceTable.Line_Net_Amount = AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Net_Amount"))


                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)) = PurchInvoiceTable
                    ReDim Preserve PurchInvoiceTableList(UBound(PurchInvoiceTableList) + 1)
                Next
                FrmPurchInvoiceDirect.InsertPurchInvoice(PurchInvoiceTableList)
            End If
        Next
    End Sub
    Public Sub FSyncSubGroup(DtPartySource As DataTable, Conn As Object, Cmd As Object)
        Dim mTrans As String = ""
        Dim ErrorLog As String = ""
        Dim DtMain As DataTable = Nothing
        Dim I As Integer

        Dim bLastAcGroupCode As Integer = AgL.XNull(AgL.Dman_Execute("SELECT  IfNull(Max(CAST(GroupCode AS INTEGER)),0) FROM AcGroup WHERE ABS(GroupCode)>0", AgL.GcnRead).ExecuteScalar)
        Dim DtAccountGroup = DtPartySource.DefaultView.ToTable(True, "GroupName")
        For I = 0 To DtAccountGroup.Rows.Count - 1
            Dim AcGroupTable As New FrmPerson.StructAcGroup
            Dim bAcGroupCode As String = (bLastAcGroupCode + (I + 1)).ToString.PadLeft(4).Replace(" ", "0")

            AcGroupTable.GroupCode = bAcGroupCode
            AcGroupTable.SNo = ""
            AcGroupTable.GroupName = AgL.XNull(DtAccountGroup.Rows(I)("GroupName"))
            AcGroupTable.ContraGroupName = AgL.XNull(DtAccountGroup.Rows(I)("GroupName"))
            AcGroupTable.GroupUnder = ""
            AcGroupTable.GroupNature = ""
            AcGroupTable.Nature = ""
            AcGroupTable.SysGroup = ""
            AcGroupTable.U_Name = AgL.PubUserName
            AcGroupTable.U_EntDt = AgL.GetDateTime(AgL.GcnRead)
            AcGroupTable.U_AE = "A"

            FrmPerson.ImportAcGroupTable(AcGroupTable)
        Next

        Dim bLastSubCode As String = AgL.GetMaxId("SubGroup", "SubCode", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 8, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)

        For I = 0 To DtPartySource.Rows.Count - 1
            Dim SubGroupTable As New FrmPerson.StructSubGroupTable
            Dim bSubCode = AgL.PubDivCode & AgL.PubSiteCode & (Convert.ToInt32(bLastSubCode.Replace(AgL.PubDivCode + AgL.PubSiteCode, "")) + I).ToString().PadLeft(8, "0")

            SubGroupTable.SubCode = bSubCode
            SubGroupTable.Site_Code = AgL.PubSiteCode
            SubGroupTable.Name = AgL.XNull(DtPartySource.Rows(I)("Name"))
            SubGroupTable.DispName = AgL.XNull(DtPartySource.Rows(I)("DispName"))
            SubGroupTable.ManualCode = AgL.XNull(DtPartySource.Rows(I)("ManualCode"))
            SubGroupTable.AccountGroup = AgL.XNull(DtPartySource.Rows(I)("GroupName"))
            SubGroupTable.StateName = AgL.XNull(DtPartySource.Rows(I)("StateName"))
            SubGroupTable.AgentName = ""
            SubGroupTable.TransporterName = ""
            SubGroupTable.AreaName = ""
            SubGroupTable.CityName = AgL.XNull(DtPartySource.Rows(I)("CityName"))
            SubGroupTable.GroupCode = AgL.XNull(DtPartySource.Rows(I)("GroupCode"))
            SubGroupTable.GroupNature = AgL.XNull(DtPartySource.Rows(I)("GroupNature"))
            SubGroupTable.Nature = AgL.XNull(DtPartySource.Rows(I)("Nature"))
            SubGroupTable.Address = AgL.XNull(DtPartySource.Rows(I)("Address"))
            SubGroupTable.CityCode = AgL.XNull(DtPartySource.Rows(I)("CityCode"))
            SubGroupTable.PIN = AgL.XNull(DtPartySource.Rows(I)("PIN"))
            SubGroupTable.Phone = AgL.XNull(DtPartySource.Rows(I)("Phone"))
            SubGroupTable.ContactPerson = AgL.XNull(DtPartySource.Rows(I)("ContactPerson"))
            SubGroupTable.SubgroupType = AgL.XNull(DtPartySource.Rows(I)("SubgroupType"))
            SubGroupTable.Mobile = AgL.XNull(DtPartySource.Rows(I)("Mobile"))
            SubGroupTable.CreditDays = AgL.XNull(DtPartySource.Rows(I)("CreditDays"))
            SubGroupTable.CreditLimit = AgL.XNull(DtPartySource.Rows(I)("CreditLimit"))
            SubGroupTable.EMail = AgL.XNull(DtPartySource.Rows(I)("EMail"))
            SubGroupTable.Parent = AgL.XNull(DtPartySource.Rows(I)("Parent"))
            SubGroupTable.SalesTaxPostingGroup = AgL.XNull(DtPartySource.Rows(I)("SalesTaxPostingGroup"))
            SubGroupTable.EntryBy = AgL.PubUserName
            SubGroupTable.EntryDate = AgL.GetDateTime(AgL.GcnRead)
            SubGroupTable.EntryType = "Add"
            SubGroupTable.EntryStatus = LogStatus.LogOpen
            SubGroupTable.Div_Code = AgL.PubDivCode
            SubGroupTable.Status = "Active"
            SubGroupTable.SalesTaxNo = AgL.XNull(DtPartySource.Rows(I)("SalesTaxNo"))
            SubGroupTable.PANNo = AgL.XNull(DtPartySource.Rows(I)("PANNo"))
            SubGroupTable.AadharNo = AgL.XNull(DtPartySource.Rows(I)("AadharNo"))
            SubGroupTable.Cnt = I
            FrmPerson.ImportSubgroupTable(SubGroupTable)
        Next
    End Sub
    Public Sub FSyncItem(DtItemSource As DataTable, Conn As Object, Cmd As Object)
        Dim mTrans As String = ""
        Dim ErrorLog As String = ""
        Dim DtMain As DataTable = Nothing
        Dim I As Integer
        Dim bLastItemCategoryCode = AgL.GetMaxId("Item", "Code", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 4, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)

        Dim DtItemCategory = DtItemSource.DefaultView.ToTable(True, "ItemCategoryDesc")
        For I = 0 To DtItemCategory.Rows.Count - 1
            If AgL.XNull(DtItemCategory.Rows(I)("ItemCategoryDesc")) <> "" Then
                Dim ItemCategoryTable As New FrmItemMaster.StructItemCategory
                Dim bItemCategoryCode As String = AgL.PubDivCode & AgL.PubSiteCode & (Convert.ToInt32(bLastItemCategoryCode.Replace(AgL.PubDivCode + AgL.PubSiteCode, "")) + I).ToString().PadLeft(4, "0")

                ItemCategoryTable.Code = bItemCategoryCode
                ItemCategoryTable.Description = AgL.XNull(DtItemCategory.Rows(I)("ItemCategoryDesc"))
                ItemCategoryTable.ItemType = ItemTypeCode.TradingProduct
                ItemCategoryTable.SalesTaxPostingGroup = "GST 0%"
                ItemCategoryTable.Unit = "Nos"
                ItemCategoryTable.EntryBy = AgL.PubUserName
                ItemCategoryTable.EntryDate = AgL.GetDateTime(AgL.GcnRead)
                ItemCategoryTable.EntryType = "Add"
                ItemCategoryTable.EntryStatus = LogStatus.LogOpen
                ItemCategoryTable.Div_Code = AgL.PubDivCode
                ItemCategoryTable.Status = "Active"

                FrmItemMaster.ImportItemCategoryTable(ItemCategoryTable)
            End If
        Next

        Dim bLastItemGroupCode = AgL.GetMaxId("Item", "Code", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 4, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)

        Dim DtItemGroup = DtItemSource.DefaultView.ToTable(True, "ItemGroupDesc", "ItemCategoryDesc")
        For I = 0 To DtItemGroup.Rows.Count - 1
            If AgL.XNull(DtItemGroup.Rows(I)("ItemGroupDesc")) <> "" Then
                Dim ItemGroupTable As New FrmItemMaster.StructItemGroup
                Dim bItemGroupCode As String = AgL.PubDivCode & AgL.PubSiteCode & (Convert.ToInt32(bLastItemGroupCode.Replace(AgL.PubDivCode + AgL.PubSiteCode, "")) + I).ToString().PadLeft(4, "0")

                ItemGroupTable.Code = bItemGroupCode
                ItemGroupTable.Description = AgL.XNull(DtItemGroup.Rows(I)("ItemGroupDesc"))
                ItemGroupTable.ItemCategory = AgL.XNull(DtItemGroup.Rows(I)("ItemCategoryDesc"))
                ItemGroupTable.ItemType = ItemTypeCode.TradingProduct
                ItemGroupTable.SalesTaxPostingGroup = "GST 0%"
                ItemGroupTable.Unit = "Nos"
                ItemGroupTable.EntryBy = AgL.PubUserName
                ItemGroupTable.EntryDate = AgL.GetDateTime(AgL.GcnRead)
                ItemGroupTable.EntryType = "Add"
                ItemGroupTable.EntryStatus = LogStatus.LogOpen
                ItemGroupTable.Div_Code = AgL.PubDivCode
                ItemGroupTable.Status = "Active"

                FrmItemMaster.ImportItemGroupTable(ItemGroupTable)
            End If
        Next

        Dim bLastItemCode As String = AgL.GetMaxId("Item", "Code", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 4, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)

        For I = 0 To DtItemSource.Rows.Count - 1
            If AgL.XNull(DtItemSource.Rows(I)("Description")) <> "" Then

                Dim ItemTable As New FrmItemMaster.StructItem
                Dim bItemCode As String = AgL.PubDivCode & AgL.PubSiteCode & (Convert.ToInt32(bLastItemCode.Replace(AgL.PubDivCode + AgL.PubSiteCode, "")) + I).ToString().PadLeft(4, "0")

                ItemTable.Code = bItemCode
                ItemTable.ManualCode = AgL.XNull(DtItemSource.Rows(I)("ManualCode"))
                ItemTable.Description = AgL.XNull(DtItemSource.Rows(I)("Description"))
                ItemTable.DisplayName = AgL.XNull(DtItemSource.Rows(I)("DisplayName"))
                ItemTable.Specification = AgL.XNull(DtItemSource.Rows(I)("Specification"))
                ItemTable.ItemGroup = AgL.XNull(DtItemSource.Rows(I)("ItemGroup"))
                ItemTable.ItemCategory = AgL.XNull(DtItemSource.Rows(I)("ItemCategory"))
                ItemTable.ItemType = AgL.XNull(DtItemSource.Rows(I)("ItemType"))
                ItemTable.V_Type = AgL.XNull(DtItemSource.Rows(I)("V_Type"))
                ItemTable.Unit = AgL.XNull(DtItemSource.Rows(I)("Unit"))
                ItemTable.PurchaseRate = AgL.XNull(DtItemSource.Rows(I)("PurchaseRate"))
                ItemTable.Rate = AgL.XNull(DtItemSource.Rows(I)("Rate"))
                ItemTable.SalesTaxPostingGroup = AgL.XNull(DtItemSource.Rows(I)("SalesTaxPostingGroup"))
                ItemTable.HSN = AgL.XNull(DtItemSource.Rows(I)("HSN"))
                ItemTable.EntryBy = AgL.PubUserName
                ItemTable.EntryDate = AgL.GetDateTime(AgL.GcnRead)
                ItemTable.EntryType = "Add"
                ItemTable.EntryStatus = LogStatus.LogOpen
                ItemTable.Div_Code = AgL.PubDivCode
                ItemTable.Status = "Active"
                ItemTable.StockYN = 1
                ItemTable.IsSystemDefine = 0

                FrmItemMaster.ImportItemTable(ItemTable)
            End If
        Next
    End Sub
    Public Sub FPostPurchaseData_ForDifference(Conn As Object, Cmd As Object)
        Dim ErrorLog As String = ""
        Dim DtMain As DataTable = Nothing
        Dim I As Integer
        Dim J As Integer
        Dim StrErrLog As String = ""


        Dim Tot_Gross_Amount As Double = 0
        Dim Tot_Taxable_Amount As Double = 0
        Dim Tot_Tax1 As Double = 0
        Dim Tot_Tax2 As Double = 0
        Dim Tot_Tax3 As Double = 0
        Dim Tot_Tax4 As Double = 0
        Dim Tot_Tax5 As Double = 0
        Dim Tot_SubTotal1 As Double = 0


        For I = 0 To Dgl1.Rows.Count - 1

            Tot_Gross_Amount = 0
            Tot_Taxable_Amount = 0
            Tot_Tax1 = 0
            Tot_Tax2 = 0
            Tot_Tax3 = 0
            Tot_Tax4 = 0
            Tot_Tax5 = 0
            Tot_SubTotal1 = 0

            Dim PurchInvoiceTableList(0) As FrmPurchInvoiceDirect.StructPurchInvoice
            Dim PurchInvoiceTable As New FrmPurchInvoiceDirect.StructPurchInvoice

            PurchInvoiceTable.DocID = ""
            PurchInvoiceTable.V_Type = "PI"
            PurchInvoiceTable.V_Prefix = ""
            PurchInvoiceTable.Site_Code = AgL.PubSiteCode
            PurchInvoiceTable.Div_Code = AgL.PubDivCode
            PurchInvoiceTable.V_No = 0
            PurchInvoiceTable.V_Date = Dgl1.Item(Col1WInvoiceDate, I).Value
            PurchInvoiceTable.ManualRefNo = ""
            PurchInvoiceTable.Vendor = ""
            PurchInvoiceTable.VendorName = Dgl1.Item(Col1Supplier, I).Value
            PurchInvoiceTable.AgentCode = ""
            PurchInvoiceTable.AgentName = ""
            PurchInvoiceTable.BillToPartyCode = ""
            PurchInvoiceTable.BillToPartyName = Dgl1.Item(Col1Supplier, I).Value
            PurchInvoiceTable.VendorAddress = ""
            PurchInvoiceTable.VendorCity = ""
            PurchInvoiceTable.VendorMobile = ""
            PurchInvoiceTable.VendorSalesTaxNo = ""
            PurchInvoiceTable.ShipToAddress = ""
            PurchInvoiceTable.SalesTaxGroupParty = ""
            PurchInvoiceTable.PlaceOfSupply = ""
            PurchInvoiceTable.StructureCode = ""
            PurchInvoiceTable.CustomFields = ""
            PurchInvoiceTable.VendorDocNo = Dgl1.Item(Col1InvoiceNo, I).Value
            PurchInvoiceTable.VendorDocDate = Dgl1.Item(Col1InvoiceDate, I).Value
            PurchInvoiceTable.ReferenceDocId = ""
            PurchInvoiceTable.GenDocId = ""
            PurchInvoiceTable.GenDocIdSr = ""
            PurchInvoiceTable.Remarks = ""
            PurchInvoiceTable.Status = "Active"
            PurchInvoiceTable.EntryBy = AgL.PubUserName
            PurchInvoiceTable.EntryDate = AgL.GetDateTime(AgL.GcnRead)
            PurchInvoiceTable.ApproveBy = ""
            PurchInvoiceTable.ApproveDate = ""
            PurchInvoiceTable.MoveToLog = ""
            PurchInvoiceTable.MoveToLogDate = ""
            PurchInvoiceTable.UploadDate = ""

            PurchInvoiceTable.Deduction_Per = 0
            PurchInvoiceTable.Deduction = 0
            PurchInvoiceTable.Other_Charge_Per = 0
            PurchInvoiceTable.Other_Charge = 0
            PurchInvoiceTable.Round_Off = 0
            PurchInvoiceTable.Net_Amount = 0


            'For Line Detail
            PurchInvoiceTable.Line_Sr = 1
            PurchInvoiceTable.Line_ItemCode = ""
            PurchInvoiceTable.Line_ItemName = Dgl1.Item(Col1ItemGroup, I).Value
            PurchInvoiceTable.Line_Specification = ""
            PurchInvoiceTable.Line_SalesTaxGroupItem = "GST 0%"
            PurchInvoiceTable.Line_ReferenceNo = ""
            PurchInvoiceTable.Line_DocQty = Val(Dgl1.Item(Col1WQty, I).Value)
            PurchInvoiceTable.Line_FreeQty = 0
            PurchInvoiceTable.Line_Qty = Val(Dgl1.Item(Col1WQty, I).Value)
            PurchInvoiceTable.Line_Unit = "Nos"
            PurchInvoiceTable.Line_Pcs = 0
            PurchInvoiceTable.Line_UnitMultiplier = 0
            PurchInvoiceTable.Line_DealUnit = ""
            PurchInvoiceTable.Line_DocDealQty = ""
            PurchInvoiceTable.Line_DiscountPer = 0
            PurchInvoiceTable.Line_DiscountAmount = 0
            PurchInvoiceTable.Line_AdditionalDiscountPer = 0
            PurchInvoiceTable.Line_AdditionalDiscountAmount = 0
            PurchInvoiceTable.Line_Amount = Val(Dgl1.Item(Col1WAmount, I).Value) - Val(Dgl1.Item(Col1Amount, I).Value)
            PurchInvoiceTable.Line_Rate = Math.Round(Val(PurchInvoiceTable.Line_Amount) / Val(PurchInvoiceTable.Line_Qty), 2)
            PurchInvoiceTable.Line_Remark = ""
            PurchInvoiceTable.Line_BaleNo = ""
            PurchInvoiceTable.Line_LotNo = ""
            PurchInvoiceTable.Line_ReferenceDocId = ""
            PurchInvoiceTable.Line_ReferenceSr = ""
            PurchInvoiceTable.Line_PurchInvoice = ""
            PurchInvoiceTable.Line_PurchInvoiceSr = ""
            PurchInvoiceTable.Line_GrossWeight = 0
            PurchInvoiceTable.Line_NetWeight = 0
            PurchInvoiceTable.Line_Gross_Amount = PurchInvoiceTable.Line_Amount
            PurchInvoiceTable.Line_Taxable_Amount = PurchInvoiceTable.Line_Amount
            PurchInvoiceTable.Line_Tax1_Per = 0
            PurchInvoiceTable.Line_Tax1 = PurchInvoiceTable.Line_Amount * PurchInvoiceTable.Line_Tax1_Per / 100
            PurchInvoiceTable.Line_Tax2_Per = 0
            PurchInvoiceTable.Line_Tax2 = PurchInvoiceTable.Line_Amount * PurchInvoiceTable.Line_Tax2_Per / 100
            PurchInvoiceTable.Line_Tax3_Per = 0
            PurchInvoiceTable.Line_Tax3 = PurchInvoiceTable.Line_Amount * PurchInvoiceTable.Line_Tax3_Per / 100
            PurchInvoiceTable.Line_Tax4_Per = 0
            PurchInvoiceTable.Line_Tax4 = PurchInvoiceTable.Line_Amount * PurchInvoiceTable.Line_Tax4_Per / 100
            PurchInvoiceTable.Line_Tax5_Per = 0
            PurchInvoiceTable.Line_Tax5 = PurchInvoiceTable.Line_Amount * PurchInvoiceTable.Line_Tax5_Per / 100
            PurchInvoiceTable.Line_SubTotal1 = PurchInvoiceTable.Line_Amount + PurchInvoiceTable.Line_Tax1 + PurchInvoiceTable.Line_Tax2 +
                                                    PurchInvoiceTable.Line_Tax3 + PurchInvoiceTable.Line_Tax4 + PurchInvoiceTable.Line_Tax5

            'For Header Values
            Tot_Gross_Amount += PurchInvoiceTable.Line_Gross_Amount
            Tot_Taxable_Amount += PurchInvoiceTable.Line_Taxable_Amount
            Tot_Tax1 += PurchInvoiceTable.Line_Tax1
            Tot_Tax2 += PurchInvoiceTable.Line_Tax2
            Tot_Tax3 += PurchInvoiceTable.Line_Tax3
            Tot_Tax4 += PurchInvoiceTable.Line_Tax4
            Tot_Tax5 += PurchInvoiceTable.Line_Tax5
            Tot_SubTotal1 += PurchInvoiceTable.Line_SubTotal1

            PurchInvoiceTableList(UBound(PurchInvoiceTableList)) = PurchInvoiceTable
            ReDim Preserve PurchInvoiceTableList(UBound(PurchInvoiceTableList) + 1)

#Region "Packing Charge"
            If Val(Dgl1.Item(Col1WPacking, I).Value) > 0 Then
                PurchInvoiceTableList(1).Line_Sr = 2
                PurchInvoiceTableList(1).Line_ItemCode = ""
                PurchInvoiceTableList(1).Line_ItemName = ItemCode.Packing
                PurchInvoiceTableList(1).Line_Specification = ""
                PurchInvoiceTableList(1).Line_SalesTaxGroupItem = "GST 0%"
                PurchInvoiceTableList(1).Line_ReferenceNo = ""
                PurchInvoiceTableList(1).Line_DocQty = 1
                PurchInvoiceTableList(1).Line_FreeQty = 0
                PurchInvoiceTableList(1).Line_Qty = 1
                PurchInvoiceTableList(1).Line_Unit = "Nos"
                PurchInvoiceTableList(1).Line_Pcs = 0
                PurchInvoiceTableList(1).Line_UnitMultiplier = 0
                PurchInvoiceTableList(1).Line_DealUnit = ""
                PurchInvoiceTableList(1).Line_DocDealQty = ""
                PurchInvoiceTableList(1).Line_DiscountPer = 0
                PurchInvoiceTableList(1).Line_DiscountAmount = 0
                PurchInvoiceTableList(1).Line_AdditionalDiscountPer = 0
                PurchInvoiceTableList(1).Line_AdditionalDiscountAmount = 0
                PurchInvoiceTableList(1).Line_Amount = Val(Dgl1.Item(Col1WPacking, I).Value)
                PurchInvoiceTableList(1).Line_Rate = Val(Dgl1.Item(Col1WPacking, I).Value)
                PurchInvoiceTableList(1).Line_Remark = ""
                PurchInvoiceTableList(1).Line_BaleNo = ""
                PurchInvoiceTableList(1).Line_LotNo = ""
                PurchInvoiceTableList(1).Line_ReferenceDocId = ""
                PurchInvoiceTableList(1).Line_ReferenceSr = ""
                PurchInvoiceTableList(1).Line_PurchInvoice = ""
                PurchInvoiceTableList(1).Line_PurchInvoiceSr = ""
                PurchInvoiceTableList(1).Line_GrossWeight = 0
                PurchInvoiceTableList(1).Line_NetWeight = 0
                PurchInvoiceTableList(1).Line_Gross_Amount = PurchInvoiceTableList(1).Line_Amount
                PurchInvoiceTableList(1).Line_Taxable_Amount = PurchInvoiceTableList(1).Line_Amount
                PurchInvoiceTableList(1).Line_Tax1_Per = 0
                PurchInvoiceTableList(1).Line_Tax1 = PurchInvoiceTableList(1).Line_Taxable_Amount * PurchInvoiceTable.Line_Tax1_Per / 100
                PurchInvoiceTableList(1).Line_Tax2_Per = 0
                PurchInvoiceTableList(1).Line_Tax2 = PurchInvoiceTableList(1).Line_Taxable_Amount * PurchInvoiceTable.Line_Tax2_Per / 100
                PurchInvoiceTableList(1).Line_Tax3_Per = 0
                PurchInvoiceTableList(1).Line_Tax3 = PurchInvoiceTableList(1).Line_Taxable_Amount * PurchInvoiceTable.Line_Tax3_Per / 100
                PurchInvoiceTableList(1).Line_Tax4_Per = 0
                PurchInvoiceTableList(1).Line_Tax4 = PurchInvoiceTableList(1).Line_Taxable_Amount * PurchInvoiceTable.Line_Tax4_Per / 100
                PurchInvoiceTableList(1).Line_Tax5_Per = 0
                PurchInvoiceTableList(1).Line_Tax5 = PurchInvoiceTableList(1).Line_Taxable_Amount * PurchInvoiceTable.Line_Tax5_Per / 100
                PurchInvoiceTableList(1).Line_SubTotal1 = PurchInvoiceTableList(1).Line_Amount + PurchInvoiceTableList(1).Line_Tax1 + PurchInvoiceTableList(1).Line_Tax2 +
                                                        PurchInvoiceTableList(1).Line_Tax3 + PurchInvoiceTableList(1).Line_Tax4 + PurchInvoiceTableList(1).Line_Tax5
                'For Header Values
                Tot_Gross_Amount += PurchInvoiceTableList(1).Line_Gross_Amount
                Tot_Taxable_Amount += PurchInvoiceTableList(1).Line_Taxable_Amount
                Tot_Tax1 += PurchInvoiceTableList(1).Line_Tax1
                Tot_Tax2 += PurchInvoiceTableList(1).Line_Tax2
                Tot_Tax3 += PurchInvoiceTableList(1).Line_Tax3
                Tot_Tax4 += PurchInvoiceTableList(1).Line_Tax4
                Tot_Tax5 += PurchInvoiceTableList(1).Line_Tax5
                Tot_SubTotal1 += PurchInvoiceTableList(1).Line_SubTotal1

                'PurchInvoiceTableList(UBound(PurchInvoiceTableList)) = PurchInvoiceTable
                ReDim Preserve PurchInvoiceTableList(UBound(PurchInvoiceTableList) + 1)
            End If
#End Region

#Region "Freight Charge"
            If Val(Dgl1.Item(Col1WFreight, I).Value) > 0 Then
                PurchInvoiceTableList(2).Line_Sr = 3
                PurchInvoiceTableList(2).Line_ItemCode = ""
                PurchInvoiceTableList(2).Line_ItemName = ItemCode.Freight
                PurchInvoiceTableList(2).Line_Specification = ""
                PurchInvoiceTableList(2).Line_SalesTaxGroupItem = "GST 0%"
                PurchInvoiceTableList(2).Line_ReferenceNo = ""
                PurchInvoiceTableList(2).Line_DocQty = 1
                PurchInvoiceTableList(2).Line_FreeQty = 0
                PurchInvoiceTableList(2).Line_Qty = 1
                PurchInvoiceTableList(2).Line_Unit = "Nos"
                PurchInvoiceTableList(2).Line_Pcs = 0
                PurchInvoiceTableList(2).Line_UnitMultiplier = 0
                PurchInvoiceTableList(2).Line_DealUnit = ""
                PurchInvoiceTableList(2).Line_DocDealQty = ""
                PurchInvoiceTableList(2).Line_DiscountPer = 0
                PurchInvoiceTableList(2).Line_DiscountAmount = 0
                PurchInvoiceTableList(2).Line_AdditionalDiscountPer = 0
                PurchInvoiceTableList(2).Line_AdditionalDiscountAmount = 0
                PurchInvoiceTableList(2).Line_Amount = Val(Dgl1.Item(Col1WFreight, I).Value)
                PurchInvoiceTableList(2).Line_Rate = Val(Dgl1.Item(Col1WFreight, I).Value)
                PurchInvoiceTableList(2).Line_Remark = ""
                PurchInvoiceTableList(2).Line_BaleNo = ""
                PurchInvoiceTableList(2).Line_LotNo = ""
                PurchInvoiceTableList(2).Line_ReferenceDocId = ""
                PurchInvoiceTableList(2).Line_ReferenceSr = ""
                PurchInvoiceTableList(2).Line_PurchInvoice = ""
                PurchInvoiceTableList(2).Line_PurchInvoiceSr = ""
                PurchInvoiceTableList(2).Line_GrossWeight = 0
                PurchInvoiceTableList(2).Line_NetWeight = 0
                PurchInvoiceTableList(2).Line_Gross_Amount = PurchInvoiceTableList(2).Line_Amount
                PurchInvoiceTableList(2).Line_Taxable_Amount = PurchInvoiceTableList(2).Line_Amount
                PurchInvoiceTableList(2).Line_Tax1_Per = 0
                PurchInvoiceTableList(2).Line_Tax1 = PurchInvoiceTableList(2).Line_Taxable_Amount * PurchInvoiceTable.Line_Tax1_Per / 100
                PurchInvoiceTableList(2).Line_Tax2_Per = 0
                PurchInvoiceTableList(2).Line_Tax2 = PurchInvoiceTableList(2).Line_Taxable_Amount * PurchInvoiceTable.Line_Tax2_Per / 100
                PurchInvoiceTableList(2).Line_Tax3_Per = 0
                PurchInvoiceTableList(2).Line_Tax3 = PurchInvoiceTableList(2).Line_Taxable_Amount * PurchInvoiceTable.Line_Tax3_Per / 100
                PurchInvoiceTableList(2).Line_Tax4_Per = 0
                PurchInvoiceTableList(2).Line_Tax4 = PurchInvoiceTableList(2).Line_Taxable_Amount * PurchInvoiceTable.Line_Tax4_Per / 100
                PurchInvoiceTableList(2).Line_Tax5_Per = 0
                PurchInvoiceTableList(2).Line_Tax5 = PurchInvoiceTableList(2).Line_Taxable_Amount * PurchInvoiceTable.Line_Tax5_Per / 100
                PurchInvoiceTableList(2).Line_SubTotal1 = PurchInvoiceTableList(2).Line_Taxable_Amount + PurchInvoiceTableList(2).Line_Tax1 + PurchInvoiceTableList(2).Line_Tax2 +
                                                        PurchInvoiceTableList(2).Line_Tax3 + PurchInvoiceTableList(2).Line_Tax4 + PurchInvoiceTableList(2).Line_Tax5
                'For Header Values
                Tot_Gross_Amount += PurchInvoiceTableList(2).Line_Gross_Amount
                Tot_Taxable_Amount += PurchInvoiceTableList(2).Line_Taxable_Amount
                Tot_Tax1 += PurchInvoiceTableList(2).Line_Tax1
                Tot_Tax2 += PurchInvoiceTableList(2).Line_Tax2
                Tot_Tax3 += PurchInvoiceTableList(2).Line_Tax3
                Tot_Tax4 += PurchInvoiceTableList(2).Line_Tax4
                Tot_Tax5 += PurchInvoiceTableList(2).Line_Tax5
                Tot_SubTotal1 += PurchInvoiceTableList(2).Line_SubTotal1

                'PurchInvoiceTableList(UBound(PurchInvoiceTableList)) = PurchInvoiceTable
                ReDim Preserve PurchInvoiceTableList(UBound(PurchInvoiceTableList) + 1)
            End If
#End Region


            PurchInvoiceTableList(0).Gross_Amount = Tot_Gross_Amount
            PurchInvoiceTableList(0).Taxable_Amount = Tot_Taxable_Amount
            PurchInvoiceTableList(0).Tax1 = Tot_Tax1
            PurchInvoiceTableList(0).Tax2 = Tot_Tax2
            PurchInvoiceTableList(0).Tax3 = Tot_Tax3
            PurchInvoiceTableList(0).Tax4 = Tot_Tax4
            PurchInvoiceTableList(0).Tax5 = Tot_Tax5
            PurchInvoiceTableList(0).SubTotal1 = Tot_SubTotal1
            PurchInvoiceTableList(0).Other_Charge = 0
            PurchInvoiceTableList(0).Deduction = 0
            PurchInvoiceTableList(0).Round_Off = Math.Round(Math.Round(PurchInvoiceTableList(0).SubTotal1) - PurchInvoiceTableList(0).SubTotal1, 2)
            PurchInvoiceTableList(0).Net_Amount = Math.Round(PurchInvoiceTableList(0).SubTotal1)

            Dim Tot_RoundOff As Double = 0
            Dim Tot_NetAmount As Double = 0
            For J = 0 To PurchInvoiceTableList.Length - 1
                PurchInvoiceTableList(J).Line_Round_Off = Math.Round(PurchInvoiceTableList(0).Round_Off * PurchInvoiceTableList(J).Line_Gross_Amount / PurchInvoiceTableList(0).Gross_Amount, 2)
                PurchInvoiceTableList(J).Line_Net_Amount = Math.Round(PurchInvoiceTableList(0).Net_Amount * PurchInvoiceTableList(J).Line_Gross_Amount / PurchInvoiceTableList(0).Gross_Amount, 2)
                Tot_RoundOff += PurchInvoiceTableList(J).Line_Round_Off
                Tot_NetAmount += PurchInvoiceTableList(J).Line_Net_Amount
            Next

            If Tot_RoundOff <> PurchInvoiceTableList(0).Round_Off Then
                PurchInvoiceTableList(0).Line_Round_Off = PurchInvoiceTableList(0).Line_Round_Off + (PurchInvoiceTableList(0).Round_Off - Tot_RoundOff)
            End If

            If Tot_NetAmount <> PurchInvoiceTableList(0).Net_Amount Then
                PurchInvoiceTableList(0).Line_Net_Amount = PurchInvoiceTableList(0).Line_Net_Amount + (PurchInvoiceTableList(0).Net_Amount - Tot_NetAmount)
            End If
            FrmPurchInvoiceDirect.InsertPurchInvoice(PurchInvoiceTableList)
        Next
    End Sub
    Public Sub FPostSaleData_ForDifference(Conn As Object, Cmd As Object)
        Dim ErrorLog As String = ""
        Dim DtMain As DataTable = Nothing
        Dim I As Integer
        Dim J As Integer
        Dim StrErrLog As String = ""


        Dim Tot_Gross_Amount As Double = 0
        Dim Tot_Taxable_Amount As Double = 0
        Dim Tot_Tax1 As Double = 0
        Dim Tot_Tax2 As Double = 0
        Dim Tot_Tax3 As Double = 0
        Dim Tot_Tax4 As Double = 0
        Dim Tot_Tax5 As Double = 0
        Dim Tot_SubTotal1 As Double = 0


        For I = 0 To Dgl2.Rows.Count - 1
            Tot_Gross_Amount = 0
            Tot_Taxable_Amount = 0
            Tot_Tax1 = 0
            Tot_Tax2 = 0
            Tot_Tax3 = 0
            Tot_Tax4 = 0
            Tot_Tax5 = 0
            Tot_SubTotal1 = 0


            Dim SaleInvoiceTableList(0) As FrmSaleInvoiceDirect.StructSaleInvoice
            Dim SaleInvoiceTable As New FrmSaleInvoiceDirect.StructSaleInvoice

            SaleInvoiceTable.DocID = ""
            SaleInvoiceTable.V_Type = "SI"
            SaleInvoiceTable.V_Prefix = ""
            SaleInvoiceTable.Site_Code = AgL.PubSiteCode
            SaleInvoiceTable.Div_Code = AgL.PubDivCode
            SaleInvoiceTable.V_No = 0
            SaleInvoiceTable.V_Date = Dgl2.Item(Col2WInvoiceDate, I).Value
            SaleInvoiceTable.ManualRefNo = ""
            SaleInvoiceTable.SaleToParty = ""
            SaleInvoiceTable.SaleToPartyName = Dgl2.Item(Col2Party, I).Value
            SaleInvoiceTable.AgentCode = ""
            SaleInvoiceTable.AgentName = ""
            SaleInvoiceTable.BillToPartyCode = ""
            SaleInvoiceTable.BillToPartyName = Dgl2.Item(Col2Party, I).Value
            SaleInvoiceTable.SaleToPartyAddress = ""
            SaleInvoiceTable.SaleToPartyCity = ""
            SaleInvoiceTable.SaleToPartyMobile = ""
            SaleInvoiceTable.SaleToPartySalesTaxNo = ""
            SaleInvoiceTable.ShipToAddress = ""
            SaleInvoiceTable.SalesTaxGroupParty = ""
            SaleInvoiceTable.PlaceOfSupply = PlaceOfSupplay.WithinState
            SaleInvoiceTable.StructureCode = ""
            SaleInvoiceTable.CustomFields = ""
            SaleInvoiceTable.ReferenceDocId = ""
            SaleInvoiceTable.Remarks = ""
            SaleInvoiceTable.Status = "Active"
            SaleInvoiceTable.EntryBy = AgL.PubUserName
            SaleInvoiceTable.EntryDate = AgL.GetDateTime(AgL.GcnRead)
            SaleInvoiceTable.ApproveBy = ""
            SaleInvoiceTable.ApproveDate = ""
            SaleInvoiceTable.MoveToLog = ""
            SaleInvoiceTable.MoveToLogDate = ""
            SaleInvoiceTable.UploadDate = ""

            SaleInvoiceTable.Deduction_Per = 0
            SaleInvoiceTable.Deduction = 0
            SaleInvoiceTable.Other_Charge_Per = 0
            SaleInvoiceTable.Other_Charge = 0
            SaleInvoiceTable.Round_Off = 0
            SaleInvoiceTable.Net_Amount = 0

            SaleInvoiceTable.Line_Sr = J + 1
            SaleInvoiceTable.Line_ItemCode = ""
            SaleInvoiceTable.Line_ItemName = Dgl2.Item(Col2ItemGroup, I).Value
            SaleInvoiceTable.Line_Specification = ""
            SaleInvoiceTable.Line_SalesTaxGroupItem = "GST 0%"
            SaleInvoiceTable.Line_ReferenceNo = ""
            SaleInvoiceTable.Line_DocQty = Dgl2.Item(Col2WQty, I).Value
            SaleInvoiceTable.Line_FreeQty = 0
            SaleInvoiceTable.Line_Qty = Val(Dgl2.Item(Col2WQty, I).Value)
            SaleInvoiceTable.Line_Unit = "Nos"
            SaleInvoiceTable.Line_Pcs = 0
            SaleInvoiceTable.Line_UnitMultiplier = 0
            SaleInvoiceTable.Line_DealUnit = ""
            SaleInvoiceTable.Line_DocDealQty = ""
            SaleInvoiceTable.Line_DiscountPer = 0
            SaleInvoiceTable.Line_DiscountAmount = 0
            SaleInvoiceTable.Line_AdditionalDiscountPer = 0
            SaleInvoiceTable.Line_AdditionalDiscountAmount = 0
            SaleInvoiceTable.Line_Amount = Val(Dgl2.Item(Col2WAmount, I).Value)
            SaleInvoiceTable.Line_Rate = Val(Dgl2.Item(Col2WAmount, I).Value) / Val(Dgl2.Item(Col2WQty, I).Value)
            SaleInvoiceTable.Line_Remark = ""
            SaleInvoiceTable.Line_BaleNo = ""
            SaleInvoiceTable.Line_LotNo = ""
            SaleInvoiceTable.Line_ReferenceDocId = ""
            SaleInvoiceTable.Line_SaleInvoice = Dgl2.Item(Col2WSaleOrderDocId, I).Value
            SaleInvoiceTable.Line_SaleInvoiceSr = 1
            SaleInvoiceTable.Line_GrossWeight = 0
            SaleInvoiceTable.Line_NetWeight = 0
            SaleInvoiceTable.Line_Gross_Amount = SaleInvoiceTable.Line_Amount
            SaleInvoiceTable.Line_Taxable_Amount = SaleInvoiceTable.Line_Amount
            SaleInvoiceTable.Line_Tax1_Per = 0
            SaleInvoiceTable.Line_Tax1 = SaleInvoiceTable.Line_Amount * SaleInvoiceTable.Line_Tax1_Per / 100
            SaleInvoiceTable.Line_Tax2_Per = 0
            SaleInvoiceTable.Line_Tax2 = SaleInvoiceTable.Line_Amount * SaleInvoiceTable.Line_Tax2_Per / 100
            SaleInvoiceTable.Line_Tax3_Per = 0
            SaleInvoiceTable.Line_Tax3 = SaleInvoiceTable.Line_Amount * SaleInvoiceTable.Line_Tax3_Per / 100
            SaleInvoiceTable.Line_Tax4_Per = 0
            SaleInvoiceTable.Line_Tax4 = SaleInvoiceTable.Line_Amount * SaleInvoiceTable.Line_Tax4_Per / 100
            SaleInvoiceTable.Line_Tax5_Per = 0
            SaleInvoiceTable.Line_Tax5 = SaleInvoiceTable.Line_Amount * SaleInvoiceTable.Line_Tax5_Per / 100
            SaleInvoiceTable.Line_SubTotal1 = SaleInvoiceTable.Line_Amount +
                                                    SaleInvoiceTable.Line_Tax1 +
                                                    SaleInvoiceTable.Line_Tax2 +
                                                    SaleInvoiceTable.Line_Tax3 +
                                                    SaleInvoiceTable.Line_Tax4 +
                                                    SaleInvoiceTable.Line_Tax5


            'For Header Values
            Tot_Gross_Amount += SaleInvoiceTable.Line_Gross_Amount
            Tot_Taxable_Amount += SaleInvoiceTable.Line_Taxable_Amount
            Tot_Tax1 += SaleInvoiceTable.Line_Tax1
            Tot_Tax2 += SaleInvoiceTable.Line_Tax2
            Tot_Tax3 += SaleInvoiceTable.Line_Tax3
            Tot_Tax4 += SaleInvoiceTable.Line_Tax4
            Tot_Tax5 += SaleInvoiceTable.Line_Tax5
            Tot_SubTotal1 += SaleInvoiceTable.Line_SubTotal1


            SaleInvoiceTableList(UBound(SaleInvoiceTableList)) = SaleInvoiceTable
            ReDim Preserve SaleInvoiceTableList(UBound(SaleInvoiceTableList) + 1)


            SaleInvoiceTableList(0).Gross_Amount = Tot_Gross_Amount
            SaleInvoiceTableList(0).Taxable_Amount = Tot_Taxable_Amount
            SaleInvoiceTableList(0).Tax1 = Tot_Tax1
            SaleInvoiceTableList(0).Tax2 = Tot_Tax2
            SaleInvoiceTableList(0).Tax3 = Tot_Tax3
            SaleInvoiceTableList(0).Tax4 = Tot_Tax4
            SaleInvoiceTableList(0).Tax5 = Tot_Tax5
            SaleInvoiceTableList(0).SubTotal1 = Tot_SubTotal1
            SaleInvoiceTableList(0).Other_Charge = 0
            SaleInvoiceTableList(0).Deduction = 0
            SaleInvoiceTableList(0).Round_Off = Math.Round(Math.Round(SaleInvoiceTableList(0).SubTotal1) - SaleInvoiceTableList(0).SubTotal1, 2)
            SaleInvoiceTableList(0).Net_Amount = Math.Round(SaleInvoiceTableList(0).SubTotal1)

            Dim Tot_RoundOff As Double = 0
            Dim Tot_NetAmount As Double = 0
            For J = 0 To SaleInvoiceTableList.Length - 1
                SaleInvoiceTableList(J).Line_Round_Off = Math.Round(SaleInvoiceTableList(0).Round_Off * SaleInvoiceTableList(J).Line_Gross_Amount / SaleInvoiceTableList(0).Gross_Amount, 2)
                SaleInvoiceTableList(J).Line_Net_Amount = Math.Round(SaleInvoiceTableList(0).Net_Amount * SaleInvoiceTableList(J).Line_Gross_Amount / SaleInvoiceTableList(0).Gross_Amount, 2)
                Tot_RoundOff += SaleInvoiceTableList(J).Line_Round_Off
                Tot_NetAmount += SaleInvoiceTableList(J).Line_Net_Amount
            Next

            If Tot_RoundOff <> SaleInvoiceTableList(0).Round_Off Then
                SaleInvoiceTableList(0).Line_Round_Off = SaleInvoiceTableList(0).Line_Round_Off + (SaleInvoiceTableList(0).Round_Off - Tot_RoundOff)
            End If

            If Tot_NetAmount <> SaleInvoiceTableList(0).Net_Amount Then
                SaleInvoiceTableList(0).Line_Net_Amount = SaleInvoiceTableList(0).Line_Net_Amount + (SaleInvoiceTableList(0).Net_Amount - Tot_NetAmount)
            End If
            FrmSaleInvoiceDirect.InsertSaleInvoice(SaleInvoiceTableList)
        Next
    End Sub
    Private Sub BtnOk_Click(sender As Object, e As EventArgs) Handles BtnOk.Click
        Dim mTrans As String = ""
        If FDataValidation() = False Then Exit Sub
        Try
            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = "Begin"

            FSyncSaleInvoices()
            FSyncPurchaseInvoices()

            FPostPurchaseData_ForDifference(AgL.GCn, AgL.ECmd)
            FPostSaleData_ForDifference(AgL.GCn, AgL.ECmd)
            FPostDebitCreditNoteData_ForDifference(AgL.GCn, AgL.ECmd, "DNS")
            AgL.ETrans.Commit()
            mTrans = "Commit"
            MsgBox("Entry Saved Successfullt...", MsgBoxStyle.Information)
            BlankText()
        Catch ex As Exception
            AgL.ETrans.Rollback()
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub BlankText()
        Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
        Dgl2.RowCount = 1 : Dgl2.Rows.Clear()
        Dgl3.RowCount = 1 : Dgl3.Rows.Clear()
        TxtOrderNo.Tag = "" : TxtOrderNo.Text = ""
        TxtPartyName.Tag = "" : TxtPartyName.Text = ""
        TxtRemark.Tag = "" : TxtRemark.Text = ""
    End Sub
    Private Sub FSeedRequiredData()
        If AgL.FillData("Select * from SubGroup Where SubCode='RateDiff'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
            mQry = " 
                    INSERT INTO SubGroup
                    (SubCode, Site_Code, Div_Code, NamePrefix, Name, DispName, GroupCode, GroupNature, ManualCode, Nature, CityCode, PIN, Phone, Mobile, EMail, Status, SalesTaxPostingGroup, Parent, SubgroupType, Address)
                    VALUES('RateDiff', '1', 'D', NULL, 'Rate Diff A/c', 'Rate Diff A/c', '0023', '', 'Rate Diff', 'Others', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
                    "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If

        If AgL.FillData("Select * from SubGroup Where SubCode='DiscDiff'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
            mQry = " 
                    INSERT INTO SubGroup
                    (SubCode, Site_Code, Div_Code, NamePrefix, Name, DispName, GroupCode, GroupNature, ManualCode, Nature, CityCode, PIN, Phone, Mobile, EMail, Status, SalesTaxPostingGroup, Parent, SubgroupType, Address)
                    VALUES('DiscDiff', '1', 'D', NULL, 'Discount Diff A/c', 'Discount Diff A/c', '0023', '', 'Discount Diff', 'Others', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
                    "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If
    End Sub
    Private Sub Calculation()
        For I As Integer = 0 To Dgl1.Rows.Count - 1
            For J As Integer = 0 To Dgl2.Rows.Count - 1
                If Dgl1.Item(Col1SaleInvoiceDocId, I).Value = Dgl2.Item(Col2SaleInvoiceDocId, J).Value Then
                    'Dgl2.Item(Col2KAmount, J).Value = Val(Dgl2.Item(Col2Amount, J).Value) + (
                    '            Val(Dgl1.Item(Col1KAmount, I).Value) - Val(Dgl1.Item(Col1Amount, I).Value)
                    '    )

                    Dgl2.Item(Col2WAmount, J).Value = (Val(Dgl1.Item(Col1WAmount, I).Value) - Val(Dgl1.Item(Col1Amount, I).Value)) -
                                    ((Val(Dgl1.Item(Col1WAmount, I).Value) - Val(Dgl1.Item(Col1Amount, I).Value)) * Val(Dgl2.Item(Col2DiscountPer, J).Value) / 100)
                End If
            Next
        Next
        FCreateDebitCreditNoteRemark()
    End Sub
    Private Sub Dgl1_EditingControl_Validating(sender As Object, e As CancelEventArgs) Handles Dgl1.EditingControl_Validating
        Calculation()
    End Sub
    Private Sub FCreateDebitCreditNoteRemark()
        Dgl3.Rows.Clear()

        For I As Integer = 0 To Dgl1.Rows.Count - 1
            Dim bAmountDiffDebitNote As Double = 0
            bAmountDiffDebitNote = ((Val(Dgl1.Item(Col1WAmount, I).Value) -
                    Val(Dgl1.Item(Col1WPacking, I).Value) -
                    Val(Dgl1.Item(Col1WFreight, I).Value) -
                    Val(Dgl1.Item(Col1Amount, I).Value)) * Val(Dgl1.Item(Col1DiscountPer, I).Value) / 100) +
                    (Val(Dgl1.Item(Col1WQty, I).Value) * Val(Dgl1.Item(Col1AdditionalDiscountPer, I).Value))

            If bAmountDiffDebitNote > 0 Then
                Dgl3.Rows.Add()
                Dgl3.Item(ColSNo, Dgl3.Rows.Count - 1).Value = Dgl3.Rows.Count
                Dgl3.Item(Col3DrCr, Dgl3.Rows.Count - 1).Value = "Debit Note"
                Dgl3.Item(Col3V_Date, Dgl3.Rows.Count - 1).Value = Dgl1.Item(Col1WInvoiceDate, I).Value
                Dgl3.Item(Col3PartyName, Dgl3.Rows.Count - 1).Value = Dgl1.Item(Col1Supplier, I).Value
                Dgl3.Item(Col3ReasonAc, Dgl3.Rows.Count - 1).Value = "Rate Diff A/c"
                Dgl3.Item(Col3Amount, Dgl3.Rows.Count - 1).Value = bAmountDiffDebitNote
                Dgl3.Item(Col3Remark, Dgl3.Rows.Count - 1).Value = "Debit Note due to Amount Differnece In Kachha and Pakka Invoice."
            End If

            If Dgl1.Item(Col1InvoiceDiscountPer, I).Value < Dgl1.Item(Col1DiscountPer, I).Value Then
                Dim bDiscountDiffDebitNote As Double = 0
                bDiscountDiffDebitNote = Val(Dgl1.Item(Col1Amount, I).Value) *
                    (Val(Dgl1.Item(Col1DiscountPer, I).Value) - Val(Dgl1.Item(Col1InvoiceDiscountPer, I).Value)) / 100

                If bDiscountDiffDebitNote > 0 Then
                    Dgl3.Rows.Add()
                    Dgl3.Item(ColSNo, Dgl3.Rows.Count - 1).Value = Dgl3.Rows.Count
                    Dgl3.Item(Col3DrCr, Dgl3.Rows.Count - 1).Value = "Debit Note"
                    Dgl3.Item(Col3V_Date, Dgl3.Rows.Count - 1).Value = Dgl1.Item(Col1WInvoiceDate, I).Value
                    Dgl3.Item(Col3PartyName, Dgl3.Rows.Count - 1).Value = Dgl1.Item(Col1Supplier, I).Value
                    Dgl3.Item(Col3ReasonAc, Dgl3.Rows.Count - 1).Value = "Discount Diff A/c"
                    Dgl3.Item(Col3Amount, Dgl3.Rows.Count - 1).Value = bDiscountDiffDebitNote
                    Dgl3.Item(Col3Remark, Dgl3.Rows.Count - 1).Value = "Debit Note due to Discount Differnece In Kachha and Pakka Invoice."
                End If
            End If
        Next
    End Sub
    Public Sub FPostDebitCreditNoteData_ForDifference(Conn As Object, Cmd As Object,
                                                      V_Type As String)
        Dim ErrorLog As String = ""
        Dim DtMain As DataTable = Nothing
        Dim bMultiplier As Integer = 1
        Dim I As Integer
        Dim J As Integer
        Dim StrErrLog As String = ""


        Dim Tot_Gross_Amount As Double = 0
        Dim Tot_Taxable_Amount As Double = 0
        Dim Tot_Tax1 As Double = 0
        Dim Tot_Tax2 As Double = 0
        Dim Tot_Tax3 As Double = 0
        Dim Tot_Tax4 As Double = 0
        Dim Tot_Tax5 As Double = 0
        Dim Tot_SubTotal1 As Double = 0

        If V_Type = "DNS" Then
            bMultiplier = -1
        End If


        For I = 0 To Dgl3.Rows.Count - 1
            If Val(Dgl3.Item(Col3Amount, I).Value) > 0 Then
                Tot_Gross_Amount = 0
                Tot_Taxable_Amount = 0
                Tot_Tax1 = 0
                Tot_Tax2 = 0
                Tot_Tax3 = 0
                Tot_Tax4 = 0
                Tot_Tax5 = 0
                Tot_SubTotal1 = 0

                Dim VoucherEntryTableList(0) As FrmVouhcerEntry.StructLedgerHead
                Dim VoucherEntryTable As New FrmVouhcerEntry.StructLedgerHead

                VoucherEntryTable.DocID = ""
                VoucherEntryTable.V_Type = V_Type
                VoucherEntryTable.V_Prefix = ""
                VoucherEntryTable.Site_Code = AgL.PubSiteCode
                VoucherEntryTable.Div_Code = AgL.PubDivCode
                VoucherEntryTable.V_No = 0
                VoucherEntryTable.V_Date = Dgl3.Item(Col3V_Date, I).Value
                VoucherEntryTable.ReferenceNo = ""
                VoucherEntryTable.Subcode = ""
                VoucherEntryTable.SubcodeName = Dgl3.Item(Col3PartyName, I).Value

                If VoucherEntryTable.V_Type = "DNS" Or VoucherEntryTable.V_Type = "DNC" Then
                    VoucherEntryTable.DrCr = "Dr"
                ElseIf VoucherEntryTable.V_Type = "CNS" Or VoucherEntryTable.V_Type = "CNC" Then
                    VoucherEntryTable.DrCr = "Cr"
                End If

                VoucherEntryTable.SalesTaxGroupParty = ""
                VoucherEntryTable.PlaceOfSupply = PlaceOfSupplay.WithinState
                VoucherEntryTable.StructureCode = ""
                VoucherEntryTable.CustomFields = ""
                VoucherEntryTable.Remarks = Dgl3.Item(Col3Remark, I).Value
                VoucherEntryTable.Status = "Active"
                VoucherEntryTable.EntryBy = AgL.PubUserName
                VoucherEntryTable.EntryDate = AgL.GetDateTime(AgL.GcnRead)
                VoucherEntryTable.ApproveBy = ""
                VoucherEntryTable.ApproveDate = ""
                VoucherEntryTable.MoveToLog = ""
                VoucherEntryTable.MoveToLogDate = ""
                VoucherEntryTable.UploadDate = ""

                VoucherEntryTable.Deduction_Per = 0
                VoucherEntryTable.Deduction = 0
                VoucherEntryTable.Other_Charge_Per = 0
                VoucherEntryTable.Other_Charge = 0
                VoucherEntryTable.Round_Off = 0
                VoucherEntryTable.Net_Amount = 0

                VoucherEntryTable.Line_Sr = J + 1
                VoucherEntryTable.Line_SubCode = ""
                VoucherEntryTable.Line_SubCodeName = Dgl3.Item(Col3ReasonAc, I).Value
                VoucherEntryTable.Line_Specification = ""
                VoucherEntryTable.Line_SalesTaxGroupItem = "GST 0%"
                VoucherEntryTable.Line_Qty = 0
                VoucherEntryTable.Line_Unit = ""
                VoucherEntryTable.Line_Rate = 0
                VoucherEntryTable.Line_Amount = Val(Dgl3.Item(Col3Amount, I).Value)
                VoucherEntryTable.Line_Amount = VoucherEntryTable.Line_Amount * bMultiplier
                VoucherEntryTable.Line_ChqRefNo = ""
                VoucherEntryTable.Line_ChqRefDate = ""
                VoucherEntryTable.Line_Remarks = ""


                VoucherEntryTable.Line_Gross_Amount = VoucherEntryTable.Line_Amount
                VoucherEntryTable.Line_Taxable_Amount = VoucherEntryTable.Line_Amount
                VoucherEntryTable.Line_Tax1_Per = 0
                VoucherEntryTable.Line_Tax1 = (VoucherEntryTable.Line_Amount * VoucherEntryTable.Line_Tax1_Per / 100)
                VoucherEntryTable.Line_Tax2_Per = 0
                VoucherEntryTable.Line_Tax2 = (VoucherEntryTable.Line_Amount * VoucherEntryTable.Line_Tax2_Per / 100)
                VoucherEntryTable.Line_Tax3_Per = 0
                VoucherEntryTable.Line_Tax3 = (VoucherEntryTable.Line_Amount * VoucherEntryTable.Line_Tax3_Per / 100)
                VoucherEntryTable.Line_Tax4_Per = 0
                VoucherEntryTable.Line_Tax4 = (VoucherEntryTable.Line_Amount * VoucherEntryTable.Line_Tax4_Per / 100)
                VoucherEntryTable.Line_Tax5_Per = 0
                VoucherEntryTable.Line_Tax5 = (VoucherEntryTable.Line_Amount * VoucherEntryTable.Line_Tax5_Per / 100)
                VoucherEntryTable.Line_SubTotal1 = (VoucherEntryTable.Line_Amount +
                                                        VoucherEntryTable.Line_Tax1 +
                                                        VoucherEntryTable.Line_Tax2 +
                                                        VoucherEntryTable.Line_Tax3 +
                                                        VoucherEntryTable.Line_Tax4 +
                                                        VoucherEntryTable.Line_Tax5)


                'For Header Values
                Tot_Gross_Amount += VoucherEntryTable.Line_Gross_Amount
                Tot_Taxable_Amount += VoucherEntryTable.Line_Taxable_Amount
                Tot_Tax1 += VoucherEntryTable.Line_Tax1
                Tot_Tax2 += VoucherEntryTable.Line_Tax2
                Tot_Tax3 += VoucherEntryTable.Line_Tax3
                Tot_Tax4 += VoucherEntryTable.Line_Tax4
                Tot_Tax5 += VoucherEntryTable.Line_Tax5
                Tot_SubTotal1 += VoucherEntryTable.Line_SubTotal1


                VoucherEntryTableList(UBound(VoucherEntryTableList)) = VoucherEntryTable
                ReDim Preserve VoucherEntryTableList(UBound(VoucherEntryTableList) + 1)


                VoucherEntryTableList(0).Gross_Amount = Tot_Gross_Amount
                VoucherEntryTableList(0).Taxable_Amount = Tot_Taxable_Amount
                VoucherEntryTableList(0).Tax1 = Tot_Tax1
                VoucherEntryTableList(0).Tax2 = Tot_Tax2
                VoucherEntryTableList(0).Tax3 = Tot_Tax3
                VoucherEntryTableList(0).Tax4 = Tot_Tax4
                VoucherEntryTableList(0).Tax5 = Tot_Tax5
                VoucherEntryTableList(0).SubTotal1 = Tot_SubTotal1
                VoucherEntryTableList(0).Other_Charge = 0
                VoucherEntryTableList(0).Deduction = 0
                VoucherEntryTableList(0).Round_Off = Math.Round(Math.Round(VoucherEntryTableList(0).SubTotal1) - VoucherEntryTableList(0).SubTotal1, 2)
                VoucherEntryTableList(0).Net_Amount = Math.Round(VoucherEntryTableList(0).SubTotal1)

                Dim Tot_RoundOff As Double = 0
                Dim Tot_NetAmount As Double = 0
                For J = 0 To VoucherEntryTableList.Length - 1
                    VoucherEntryTableList(J).Line_Round_Off = Math.Round(VoucherEntryTableList(0).Round_Off * VoucherEntryTableList(J).Line_Gross_Amount / VoucherEntryTableList(0).Gross_Amount, 2)
                    VoucherEntryTableList(J).Line_Net_Amount = Math.Round(VoucherEntryTableList(0).Net_Amount * VoucherEntryTableList(J).Line_Gross_Amount / VoucherEntryTableList(0).Gross_Amount, 2)
                    Tot_RoundOff += VoucherEntryTableList(J).Line_Round_Off
                    Tot_NetAmount += VoucherEntryTableList(J).Line_Net_Amount
                Next

                If Tot_RoundOff <> VoucherEntryTableList(0).Round_Off Then
                    VoucherEntryTableList(0).Line_Round_Off = VoucherEntryTableList(0).Line_Round_Off + (VoucherEntryTableList(0).Round_Off - Tot_RoundOff)
                End If

                If Tot_NetAmount <> VoucherEntryTableList(0).Net_Amount Then
                    VoucherEntryTableList(0).Line_Net_Amount = VoucherEntryTableList(0).Line_Net_Amount + (VoucherEntryTableList(0).Net_Amount - Tot_NetAmount)
                End If
                FrmVouhcerEntry.InsertLedgerHead(VoucherEntryTableList)
            End If
        Next
    End Sub
End Class