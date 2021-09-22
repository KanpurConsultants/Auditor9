Imports System.ComponentModel
Imports System.IO
Imports System.Linq
Imports AgLibrary.ClsMain.agConstants
Imports Customised.ClsMain

Public Class FrmSaleReturnW
    Dim mSearchCode$ = ""

    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Public Const ColSNo As String = "S.No."
    Public Const Col1SaleReturnDocId As String = "SaleReturnDocId"
    Public Const Col1PurchReturnDocId As String = "PurchReturnDocId"
    Public Const Col1SyncedPurchReturnDocId As String = "SyncedPurchReturnDocId"
    Public Const Col1Supplier As String = "Supplier"
    Public Const Col1ReturnNo As String = "Return No"
    Public Const Col1ReturnDate As String = "Return Date"
    Public Const Col1ItemGroup As String = "Brand"
    Public Const Col1ReturnDiscountPer As String = "Return Discount @"
    Public Const Col1ReturnAdditionalDiscountPer As String = "Return Additional Discount @"
    Public Const Col1DiscountPer As String = "Pcs Less"
    Public Const Col1AdditionalDiscountPer As String = "Additional Discount @"
    Public Const Col1AdditionPer As String = "Addition @"
    Public Const Col1Amount As String = "Purch Return Amount"
    Public Const Col1AmountWithoutDiscountAndTax As String = "Actual Goods Value Without Discount And Tax"

    Public Const Col1MasterSupplier As String = "Master Supplier"
    Public Const Col1WReturnNo As String = "W Return No"
    Public Const Col1WReturnDate As String = "W Return Date"
    Public Const Col1WQty As String = "W Qty"
    Public Const Col1WFreight As String = "W Freight"
    Public Const Col1WPacking As String = "W Packing"
    Public Const Col1WAmount As String = "W Amount"
    Public Const Col1WPurchReturnAmount As String = "W Purch Return Amount"
    Public Const Col1WPurchReturnDocId As String = "W Purch Return DocId"


    Public WithEvents Dgl2 As New AgControls.AgDataGrid
    Public Const Col2SaleReturnDocId As String = "SaleReturnDocId"
    Public Const Col2SyncedSaleReturnDocId As String = "SyncedSaleReturnDocId"
    Public Const Col2Party As String = "Party"
    Public Const Col2ReturnNo As String = "Return No"
    Public Const Col2ReturnDate As String = "Return Date"
    Public Const Col2ItemGroup As String = "Brand"
    Public Const Col2DiscountPer As String = "Pcs Less"
    Public Const Col2AdditionalDiscountPer As String = "Additional Discount @"
    Public Const Col2AdditionPer As String = "Addition @"
    Public Const Col2Amount As String = "Sale Return Amount"
    Public Const Col2AmountWithoutTax As String = "Actual Goods Value Without Discount"
    Public Const Col2Discount As String = "Discount"

    Public Const Col2MasterParty As String = "Master Party"
    Public Const Col2WSaleOrderDocId As String = "W SaleOrderDocId"
    Public Const Col2WReturnNo As String = "W Return No"
    Public Const Col2WReturnDate As String = "W Return Date"
    Public Const Col2WQty As String = "W Qty"
    Public Const Col2WFreight As String = "W Freight"
    Public Const Col2WPacking As String = "W Packing"
    Public Const Col2WDiscount As String = "W Discount"
    Public Const Col2WSaleReturnAmount As String = "W Sale Return Amount"
    Public Const Col2WSaleReturnDocId As String = "W Sale Return DocId"

    Public WithEvents Dgl3 As New AgControls.AgDataGrid
    Public Const Col3DrCr As String = "Debit/Credit Note"
    Public Const Col3V_Date As String = "Date"
    Public Const Col3Party As String = "Party Name"
    Public Const Col3ReasonAc As String = "Reason Ac"
    Public Const Col3Amount As String = "Amount"
    Public Const Col3SyncedPurchReturnDocId As String = "SyncedPurchReturnDocId"
    Public Const Col3Remark As String = "Remark"



    Dim mQry As String = ""
    Dim mOrderNCat As String = "SO"
    Public mDbPath As String = ""
    Public mDbEncryption As String = ""
    Dim Connection_Pakka As New SQLite.SQLiteConnection

    Dim DtItem As DataTable
    Dim DtSubGroup As DataTable
    Public Sub Ini_Grid()
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl1, Col1SaleReturnDocId, 100, 0, Col1SaleReturnDocId, False, True)
            .AddAgTextColumn(Dgl1, Col1PurchReturnDocId, 100, 0, Col1PurchReturnDocId, False, True)
            .AddAgTextColumn(Dgl1, Col1SyncedPurchReturnDocId, 100, 0, Col1SyncedPurchReturnDocId, False, True)
            .AddAgTextColumn(Dgl1, Col1Supplier, 100, 0, Col1Supplier, True, True)
            .AddAgTextColumn(Dgl1, Col1ReturnNo, 80, 0, Col1ReturnNo, True, True)
            .AddAgDateColumn(Dgl1, Col1ReturnDate, 80, Col1ReturnDate, True, True)
            .AddAgTextColumn(Dgl1, Col1ItemGroup, 80, 0, Col1ItemGroup, True, True)
            .AddAgNumberColumn(Dgl1, Col1ReturnDiscountPer, 80, 0, 0, False, Col1ReturnDiscountPer, False, True)
            .AddAgNumberColumn(Dgl1, Col1ReturnAdditionalDiscountPer, 80, 0, 0, False, Col1ReturnAdditionalDiscountPer, False, True)
            .AddAgNumberColumn(Dgl1, Col1DiscountPer, 80, 2, 2, False, Col1DiscountPer,, False)
            .AddAgNumberColumn(Dgl1, Col1AdditionalDiscountPer, 70, 2, 2, False, Col1AdditionalDiscountPer,, False)
            .AddAgNumberColumn(Dgl1, Col1AdditionPer, 80, 0, 0, False, Col1AdditionPer, False, True)
            .AddAgNumberColumn(Dgl1, Col1Amount, 90, 0, 0, False, Col1Amount,, True)
            .AddAgNumberColumn(Dgl1, Col1AmountWithoutDiscountAndTax, 90, 0, 0, False, Col1AmountWithoutDiscountAndTax, True, True)

            .AddAgTextColumn(Dgl1, Col1MasterSupplier, 100, 0, Col1MasterSupplier, False, True)
            .AddAgTextColumn(Dgl1, Col1WReturnNo, 90, 0, Col1WReturnNo, True, False)
            .AddAgDateColumn(Dgl1, Col1WReturnDate, 90, Col1WReturnDate, True, False)
            .AddAgNumberColumn(Dgl1, Col1WQty, 90, 0, 0, False, Col1WQty)
            .AddAgNumberColumn(Dgl1, Col1WFreight, 80, 0, 0, False, Col1WFreight)
            .AddAgNumberColumn(Dgl1, Col1WPacking, 80, 0, 0, False, Col1WPacking)
            .AddAgNumberColumn(Dgl1, Col1WAmount, 90, 0, 0, False, Col1WAmount)
            .AddAgNumberColumn(Dgl1, Col1WPurchReturnAmount, 100, 0, 0, False, Col1WPurchReturnAmount, True, True)
            .AddAgTextColumn(Dgl1, Col1WPurchReturnDocId, 90, 0, Col1WPurchReturnDocId, False, True)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 50
        Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
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
            .AddAgTextColumn(Dgl2, Col2SaleReturnDocId, 100, 0, Col2SaleReturnDocId, False, True)
            .AddAgTextColumn(Dgl2, Col2SyncedSaleReturnDocId, 100, 0, Col2SyncedSaleReturnDocId, False, True)
            .AddAgTextColumn(Dgl2, Col2Party, 100, 0, Col2Party, True, True)
            .AddAgTextColumn(Dgl2, Col2ReturnNo, 80, 0, Col2ReturnNo, True, True)
            .AddAgDateColumn(Dgl2, Col2ReturnDate, 80, Col2ReturnDate, True, True)
            .AddAgTextColumn(Dgl2, Col2ItemGroup, 90, 0, Col2ItemGroup, True, True)
            .AddAgNumberColumn(Dgl2, Col2DiscountPer, 90, 2, 2, False, Col2DiscountPer,, False)
            .AddAgNumberColumn(Dgl2, Col2AdditionalDiscountPer, 90, 2, 2, False, Col2AdditionalDiscountPer, , False)
            .AddAgNumberColumn(Dgl2, Col2AdditionPer, 90, 2, 2, False, Col2AdditionPer,, False)
            .AddAgNumberColumn(Dgl2, Col2Amount, 90, 0, 0, False, Col2Amount,, True)
            .AddAgNumberColumn(Dgl2, Col2AmountWithoutTax, 90, 0, 0, False, Col2AmountWithoutTax,, True)
            .AddAgNumberColumn(Dgl2, Col2Discount, 90, 0, 0, False, Col2Discount, True, True)

            .AddAgTextColumn(Dgl2, Col2MasterParty, 100, 0, Col2MasterParty, False, True)
            .AddAgTextColumn(Dgl2, Col2WSaleOrderDocId, 100, 0, Col2WSaleOrderDocId, False, True)
            .AddAgTextColumn(Dgl2, Col2WReturnNo, 90, 0, Col2WReturnNo, True, False)
            .AddAgDateColumn(Dgl2, Col2WReturnDate, 90, Col2WReturnDate, True, False)
            .AddAgNumberColumn(Dgl2, Col2WQty, 90, 0, 0, False, Col2WQty)
            .AddAgNumberColumn(Dgl2, Col2WFreight, 90, 0, 0, False, Col2WFreight)
            .AddAgNumberColumn(Dgl2, Col2WPacking, 90, 0, 0, False, Col2WPacking)
            .AddAgNumberColumn(Dgl2, Col2WDiscount, 90, 0, 0, False, Col2WDiscount, False, True)
            .AddAgNumberColumn(Dgl2, Col2WSaleReturnAmount, 100, 0, 0, False, Col2WSaleReturnAmount,, True)
            .AddAgTextColumn(Dgl2, Col2WSaleReturnDocId, 90, 0, Col2WSaleReturnDocId, False, True)
        End With
        AgL.AddAgDataGrid(Dgl2, Pnl2)
        Dgl2.EnableHeadersVisualStyles = False
        Dgl2.ColumnHeadersHeight = 55
        Dgl2.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
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
            .AddAgTextColumn(Dgl3, Col3Party, 300, 0, Col3Party, True, True)
            .AddAgTextColumn(Dgl3, Col3ReasonAc, 300, 0, Col3ReasonAc, False, True)
            .AddAgNumberColumn(Dgl3, Col3Amount, 200, 0, 0, False, Col3Amount,, True)
            .AddAgTextColumn(Dgl3, Col3SyncedPurchReturnDocId, 300, 0, Col3SyncedPurchReturnDocId, False, True)
            .AddAgTextColumn(Dgl3, Col3Remark, 300, 0, Col3Remark, True, False)
        End With
        AgL.AddAgDataGrid(Dgl3, Pnl3)
        Dgl3.EnableHeadersVisualStyles = False
        Dgl3.ColumnHeadersHeight = 40
        Dgl3.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
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
        mDbEncryption = AgL.INIRead(StrPath + "\" + IniName, "CompanyInfo", "Encryption", "")
        If mDbEncryption = "N" Then
            Connection_Pakka.ConnectionString = "DataSource=" & mDbPath & ";Version=3;"
        Else
            Connection_Pakka.ConnectionString = "DataSource=" & mDbPath & ";Version=3;Password=" & AgLibrary.ClsConstant.PubDbPassword & ";"
        End If
        Connection_Pakka.Open()

        FIniList()
        'FSyncSaleOrderDocuments()
        'FSeedRequiredData()

        Ini_Grid()
        TxtOrderNo.Focus()
        TxtPartyName.Enabled = False
        BtnApprove.Visible = False
        Me.WindowState = FormWindowState.Maximized
    End Sub
    Private Sub FIniList()
        mQry = " Select * From SubGroup "
        DtSubGroup = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = " Select Code, OMSId From Item "
        DtItem = AgL.FillData(mQry, AgL.GCn).Tables(0)
    End Sub
    Private Sub FSyncSaleOrderDocuments()
        mQry = "Select DocId, OMSId From SaleInvoice Where V_Type = 'SO' And OMSId Is Not Null"
        Dim DtSaleOrder As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
        For I As Integer = 0 To DtSaleOrder.Rows.Count - 1
            CopyAttachments(AgL.XNull(DtSaleOrder.Rows(I)("OMSId")), AgL.XNull(DtSaleOrder.Rows(I)("DocId")))
        Next
    End Sub
    Private Sub FSyncPurchInvoiceDocuments()
        mQry = "Select DocId, OMSId From PurchInvoice Where OMSId Is Not Null "
        Dim DtPurchInvoice As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
        For I As Integer = 0 To DtPurchInvoice.Rows.Count - 1
            CopyAttachments(AgL.XNull(DtPurchInvoice.Rows(I)("OMSId")), AgL.XNull(DtPurchInvoice.Rows(I)("DocId")))
        Next
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
                    mQry = "Select H.SaleToParty, Sg.Name As SaleToPartyName, H.UploadDate
                            From SaleInvoice H  With (NoLock)
                            LEFT JOIN SubGroup Sg On H.SaleToParty = Sg.SubCode
                            Where H.DocId = '" & TxtOrderNo.Tag & "'"
                    DtTemp = AgL.FillData(mQry, Connection_Pakka).Tables(0)
                    If DtTemp.Rows.Count > 0 Then
                        If AgL.XNull(DtTemp.Rows(0)("UploadDate")) = "" Then
                            MsgBox("Sale Return is not synced in Kachha.", MsgBoxStyle.Information)
                            Exit Sub
                        Else
                            TxtPartyName.Tag = AgL.XNull(DtTemp.Rows(0)("SaleToParty"))
                            TxtPartyName.Text = AgL.XNull(DtTemp.Rows(0)("SaleToPartyName"))

                            TxtSaleOrderDocId_W.Text = AgL.XNull(AgL.Dman_Execute("Select DocId 
                                    From SaleInvoice Where OMSId = '" & TxtOrderNo.Tag & "'", AgL.GCn).ExecuteScalar())
                        End If
                    End If

                    Dim bSaleReturnDocIdStr As String = ""

                    mQry = "Select H.DocId As SaleReturnDocId, Sg.Name As SaleToPartyName, Max(Sg1.Name) As BillToPartyName, 
                            H.DocId As InvoiceDocId, H.ManualRefNo As invoiceNo, H.V_Type As InvoiceV_Type, H.V_Date As InvoiceDate, 
                            Ig.Code As ItemGroup, Ig.Description As ItemGroupDesc,
                            Max(H.SaleToParty) As SaleToParty, Max(H.BillToParty) As BillToParty, Max(H.Site_Code) As Site_Code, 
                            Max(H.Div_Code) As Div_Code, Abs(Max(H.Net_Amount)) As Amount,
                            Max(H.V_Type) As OrderV_Type, Max(H.ManualRefNo) As OrderManualRefNo,
                            Sum(L.DiscountAmount + L.AdditionalDiscountAmount - L.AdditionAmount) As TotalDiscount,
                            Sum(Abs(L.Taxable_Amount)) As AmountWithoutTax,
                            L.DocId As SaleOrder, L.Sr As SaleOrderSr, Max(H.UploadDate) As UploadDate
                            From (Select * From SaleInvoice Where DocId = '" & TxtOrderNo.Tag & "') AS  H 
                            LEFT JOIN SaleInvoiceDetail L ON H.DocId = L.DocId
                            LEFT JOIN SubGroup Sg ON H.SaleToParty = Sg.SubCode 
                            LEFT JOIN SubGroup Sg1 ON H.BillToParty = Sg1.SubCode
                            LEFT JOIN Item I On L.Item = I.Code
                            LEFT JOIN ItemGroup Ig On I.ItemGroup = Ig.Code
                            Where H.V_Type = 'SR'
                            And I.ItemType Not In ('" & ItemTypeCode.ServiceProduct & "')
                            Group By H.ManualRefNo, H.V_Date, Ig.Code, Ig.Description "

                    'And I.Description Not In ('" & ItemCode.Packing & "','" & ItemCode.Freight & "')
                    DtTemp = AgL.FillData(mQry, Connection_Pakka).Tables(0)
                    Dgl2.RowCount = 1 : Dgl2.Rows.Clear()
                    If DtTemp.Rows.Count > 0 Then
                        For I = 0 To DtTemp.Rows.Count - 1
                            If AgL.XNull(DtTemp.Rows(I)("UploadDate")) = "" Then
                                MsgBox("Sale Return is not synced in Kachha.", MsgBoxStyle.Information)
                                BtnOk.Enabled = False
                                Exit Sub
                            Else
                                BtnOk.Enabled = True
                            End If

                            mQry = " SELECT Count(*) 
                                    FROM SaleInvoiceGeneratedEntries G 
                                    LEFT JOIN SaleInvoice Si ON G.DocId = Si.DocID
                                    WHERE Si.OmsId =  '" & AgL.XNull(DtTemp.Rows(I)("SaleReturnDocId")) & "'"
                            If AgL.XNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar()) = 0 Then
                                Dgl2.Rows.Add()
                                Dgl2.Item(ColSNo, Dgl2.Rows.Count - 1).Value = Dgl2.Rows.Count
                                Dgl2.Item(Col2SaleReturnDocId, Dgl2.Rows.Count - 1).Value = AgL.XNull(DtTemp.Rows(I)("SaleReturnDocId"))
                                Dgl2.Item(Col2Party, Dgl2.Rows.Count - 1).Tag = FGetSubCodeFromOMSId(AgL.XNull(DtTemp.Rows(I)("SaleToParty")))
                                Dgl2.Item(Col2Party, Dgl2.Rows.Count - 1).Value = AgL.XNull(DtTemp.Rows(I)("SaleToPartyName"))
                                Dgl2.Item(Col2MasterParty, Dgl2.Rows.Count - 1).Tag = FGetSubCodeFromOMSId(AgL.XNull(DtTemp.Rows(I)("BillToParty")))
                                Dgl2.Item(Col2MasterParty, Dgl2.Rows.Count - 1).Value = AgL.XNull(DtTemp.Rows(I)("BillToPartyName"))
                                Dgl2.Item(Col2ReturnNo, Dgl2.Rows.Count - 1).Tag = AgL.XNull(DtTemp.Rows(I)("InvoiceDocId"))
                                Dgl2.Item(Col2ReturnNo, Dgl2.Rows.Count - 1).Value = AgL.XNull(DtTemp.Rows(I)("InvoiceNo"))
                                Dgl2.Item(Col2ReturnDate, Dgl2.Rows.Count - 1).Value = AgL.XNull(DtTemp.Rows(I)("InvoiceDate"))




                                Dgl2.Item(Col2ItemGroup, Dgl2.Rows.Count - 1).Tag = FGetItemCodeFromOMSId(AgL.XNull(DtTemp.Rows(I)("ItemGroup")))
                                Dgl2.Item(Col2ItemGroup, Dgl2.Rows.Count - 1).Value = AgL.XNull(DtTemp.Rows(I)("ItemGroupDesc"))
                                Dgl2.Item(Col2Amount, Dgl2.Rows.Count - 1).Value = AgL.XNull(DtTemp.Rows(I)("Amount"))
                                Dgl2.Item(Col2Discount, Dgl2.Rows.Count - 1).Value = AgL.XNull(DtTemp.Rows(I)("TotalDiscount"))
                                Dgl2.Item(Col2AmountWithoutTax, Dgl2.Rows.Count - 1).Value = AgL.XNull(DtTemp.Rows(I)("AmountWithoutTax"))

                                mQry = " Select DocId From SaleInvoice Where OMSId = '" & AgL.XNull(DtTemp.Rows(I)("SaleReturnDocId")) & "'"
                                Dgl2.Item(Col2SyncedSaleReturnDocId, Dgl2.Rows.Count - 1).Value = AgL.XNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar())


                                If bSaleReturnDocIdStr <> "" Then bSaleReturnDocIdStr = bSaleReturnDocIdStr + ","
                                bSaleReturnDocIdStr = bSaleReturnDocIdStr + Dgl2.Item(Col2ReturnNo, Dgl2.Rows.Count - 1).Tag

                                Dim DTDiscounts As DataTable = FGetDiscountRates(AgL.XNull(DtTemp.Rows(I)("BillToParty")),
                                                AgL.XNull(DtTemp.Rows(I)("Site_Code")),
                                                AgL.XNull(DtTemp.Rows(I)("Div_Code")),
                                                AgL.XNull(DtTemp.Rows(I)("ItemGroup")))
                                If DTDiscounts.Rows.Count > 0 Then
                                    Dgl2.Item(Col2DiscountPer, Dgl2.Rows.Count - 1).Value = AgL.VNull(DTDiscounts.Rows(0)("Default_DiscountPerSale"))
                                    Dgl2.Item(Col2AdditionalDiscountPer, Dgl2.Rows.Count - 1).Value = AgL.VNull(DTDiscounts.Rows(0)("Default_AdditionalDiscountPerSale"))
                                    Dgl2.Item(Col2AdditionPer, Dgl2.Rows.Count - 1).Value = AgL.VNull(DTDiscounts.Rows(0)("Default_AdditionPerSale"))
                                End If

                                If AgL.XNull(DtTemp.Rows(I)("OrderV_Type")) <> "" And
                                AgL.XNull(DtTemp.Rows(I)("OrderManualRefNo")) <> "" Then
                                    mQry = " Select H.DocId
                                            From SaleInvoice H
                                            Where OMSId = '" & AgL.XNull(DtTemp.Rows(I)("SaleOrder")) & "'"
                                    Dgl2.Item(Col2WSaleOrderDocId, Dgl2.Rows.Count - 1).Value = AgL.XNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar)
                                End If
                            End If
                        Next I

                        mQry = "Select H.GenDocId As SaleReturnDocId, H.DocId As PurchInvoiceDocId, Max(H.Vendor) As Supplier, Supp.Name As SupplierName, 
                            Max(H.VendorDocNo) As InvoiceNo, H.V_Date As InvoiceDate, 
                            Ig.Code as ItemGroup, Ig.Description As ItemGroupDesc,
                            Abs(Max(H.Net_Amount)) As Amount, 
                            Max(H.BillToParty) As BillToParty, Max(H.Site_Code) As Site_Code, 
                            Max(H.Div_Code) As Div_Code, Min(L.DiscountPer) As InvoiceDiscountPer,
                            Min(L.AdditionalDiscountPer) As InvoiceAdditionalDiscountPer,
                            Abs(Sum(L.Rate * L.Qty)) As AmountWithoutTaxAndDiscount,
                            Max(Sg1.Name) As BillToPartyName, Max(H.UploadDate) As UploadDate
                            From PurchInvoice H  With (NoLock)
                            LEFT JOIN PurchInvoiceDetail L On H.DocId = L.DocId
                            LEFT JOIN Item I On L.Item = I.Code
                            LEFT JOIN ItemGroup Ig ON I.ItemGroup = Ig.Code
                            LEFT JOIN SubGroup Supp On H.Vendor = Supp.SubCode
                            LEFT JOIN SubGroup Sg1 ON H.BillToParty = Sg1.SubCode
                            Where H.GenDocId In ('" & bSaleReturnDocIdStr.Replace(",", "','") & "')
                            And I.ItemType Not In ('" & ItemTypeCode.ServiceProduct & "')
                            Group By H.GenDocId, H.DocId, Supp.Name, H.ManualRefNo, H.V_Date, Ig.Code, Ig.Description "

                        'And I.Description Not In ('" & ItemCode.Packing & "','" & ItemCode.Freight & "')
                        DtTemp = AgL.FillData(mQry, Connection_Pakka).Tables(0)
                        Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
                        If DtTemp.Rows.Count > 0 Then
                            For I = 0 To DtTemp.Rows.Count - 1
                                If AgL.XNull(DtTemp.Rows(I)("UploadDate")) = "" Then
                                    MsgBox("Purchase Return is not synced in Kachha.", MsgBoxStyle.Information)
                                    BtnOk.Enabled = False
                                    Exit Sub
                                Else
                                    BtnOk.Enabled = True
                                End If

                                Dgl1.Rows.Add()
                                Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count
                                Dgl1.Item(Col1SaleReturnDocId, I).Value = AgL.XNull(DtTemp.Rows(I)("SaleReturnDocId"))
                                Dgl1.Item(Col1PurchReturnDocId, I).Value = AgL.XNull(DtTemp.Rows(I)("PurchInvoiceDocId"))
                                Dgl1.Item(Col1Supplier, I).Tag = FGetSubCodeFromOMSId(AgL.XNull(DtTemp.Rows(I)("Supplier")))
                                Dgl1.Item(Col1Supplier, I).Value = AgL.XNull(DtTemp.Rows(I)("SupplierName"))
                                Dgl1.Item(Col1MasterSupplier, I).Tag = FGetSubCodeFromOMSId(AgL.XNull(DtTemp.Rows(I)("BillToParty")))
                                Dgl1.Item(Col1MasterSupplier, I).Value = AgL.XNull(DtTemp.Rows(I)("BillToPartyName"))
                                Dgl1.Item(Col1ReturnNo, I).Value = AgL.XNull(DtTemp.Rows(I)("InvoiceNo"))
                                Dgl1.Item(Col1ReturnDate, I).Value = AgL.XNull(DtTemp.Rows(I)("InvoiceDate"))
                                Dgl1.Item(Col1ItemGroup, I).Tag = FGetItemCodeFromOMSId(AgL.XNull(DtTemp.Rows(I)("ItemGroup")))
                                Dgl1.Item(Col1ItemGroup, I).Value = AgL.XNull(DtTemp.Rows(I)("ItemGroupDesc"))
                                Dgl1.Item(Col1ReturnDiscountPer, I).Value = AgL.VNull(DtTemp.Rows(I)("InvoiceDiscountPer"))
                                Dgl1.Item(Col1ReturnAdditionalDiscountPer, I).Value = AgL.VNull(DtTemp.Rows(I)("InvoiceAdditionalDiscountPer"))
                                Dgl1.Item(Col1Amount, I).Value = AgL.VNull(DtTemp.Rows(I)("Amount"))
                                Dgl1.Item(Col1AmountWithoutDiscountAndTax, I).Value = AgL.VNull(DtTemp.Rows(I)("AmountWithoutTaxAndDiscount"))

                                mQry = " Select DocId From PurchInvoice Where OMSId = '" & AgL.XNull(DtTemp.Rows(I)("PurchInvoiceDocId")) & "'"
                                Dgl1.Item(Col1SyncedPurchReturnDocId, Dgl1.Rows.Count - 1).Value = AgL.XNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar())


                                mQry = "Select IG.Default_DiscountPerPurchase, IG.Default_AdditionalDiscountPerPurchase,
                                    0 As Default_AdditionPerPurchase
                                    From ItemGroup IG  
                                    Where IG.Code ='" & AgL.XNull(DtTemp.Rows(I)("ItemGroup")) & "'"
                                Dim DTDiscounts As DataTable = AgL.FillData(mQry, Connection_Pakka).Tables(0)
                                If DTDiscounts.Rows.Count > 0 Then
                                    Dgl1.Item(Col1DiscountPer, I).Value = AgL.VNull(DTDiscounts.Rows(0)("Default_DiscountPerPurchase"))
                                    Dgl1.Item(Col1AdditionalDiscountPer, I).Value = AgL.VNull(DTDiscounts.Rows(0)("Default_AdditionalDiscountPerPurchase"))
                                    Dgl1.Item(Col1AdditionPer, I).Value = AgL.VNull(DTDiscounts.Rows(0)("Default_AdditionPerPurchase"))
                                End If
                            Next I
                        End If
                    Else
                        mQry = " Select H.DocId As SaleOrderDocId, H.V_Type As OrderV_Type, H.ManualRefNo As OrderManualRefNo, 
                                H.SaleToParty, H.BillToParty, H.Site_Code, H.Div_Code, L.Item As ItemGroup,
                                I.Description As ItemGroupDesc, Sg.Name As SaleToPartyName,
                                Supp.SubCode As Supplier, Supp.Name As SupplierName, Sg1.Name As BillToPartyName
                                From SaleInvoice H 
                                LEFT JOIN SaleInvoiceDetail L ON H.DocId = L.DocId
                                LEFT JOIN Item I ON L.Item = I.Code
                                LEFT JOIN SubGroup Sg On H.SaleToParty = Sg.SubCode
                                LEFT JOIN SubGroup Supp On I.DefaultSupplier = Supp.SubCode
                                LEFT JOIN SubGroup Sg1 ON H.BillToParty = Sg1.SubCode
                                LEFT JOIN SubGroup Sg2 On Supp.Parent = Sg2.SubCode
                                Where IfNull(H.ReferenceDocId,H.DocId)  = '" & TxtOrderNo.Tag & "'"
                        Dim DtSaleOrderDetail As DataTable = AgL.FillData(mQry, Connection_Pakka).Tables(0)
                        Dgl2.RowCount = 1 : Dgl2.Rows.Clear()
                        If DtSaleOrderDetail.Rows.Count > 0 Then
                            Dgl2.Rows.Add()
                            Dgl2.Item(ColSNo, I).Value = Dgl2.Rows.Count
                            Dgl2.Item(Col2SaleReturnDocId, I).Value = ""
                            Dgl2.Item(Col2Party, I).Tag = FGetSubCodeFromOMSId(AgL.XNull(DtSaleOrderDetail.Rows(I)("SaleToParty")))
                            Dgl2.Item(Col2Party, I).Value = AgL.XNull(DtSaleOrderDetail.Rows(I)("SaleToPartyName"))
                            Dgl2.Item(Col2MasterParty, I).Tag = FGetSubCodeFromOMSId(AgL.XNull(DtSaleOrderDetail.Rows(I)("BillToParty")))
                            Dgl2.Item(Col2MasterParty, I).Value = AgL.XNull(DtSaleOrderDetail.Rows(I)("BillToPartyName"))
                            Dgl2.Item(Col2ReturnNo, I).Tag = ""
                            Dgl2.Item(Col2ReturnNo, I).Value = ""
                            Dgl2.Item(Col2ReturnDate, I).Value = ""
                            Dgl2.Item(Col2ItemGroup, I).Tag = FGetItemCodeFromOMSId(AgL.XNull(DtSaleOrderDetail.Rows(I)("ItemGroup")))
                            Dgl2.Item(Col2ItemGroup, I).Value = AgL.XNull(DtSaleOrderDetail.Rows(I)("ItemGroupDesc"))
                            Dgl2.Item(Col2Amount, I).Value = 0


                            Dim DTSaleDiscounts As DataTable = FGetDiscountRates(AgL.XNull(DtSaleOrderDetail.Rows(I)("BillToParty")),
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
                                mQry = " Select H.DocId
                                        From SaleInvoice H
                                        Where OMSId = '" & AgL.XNull(DtSaleOrderDetail.Rows(I)("SaleOrderDocId")) & "'"
                                Dgl2.Item(Col2WSaleOrderDocId, I).Value = AgL.XNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar)
                            End If

                            'For Purchase Data

                            Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
                            Dgl1.Rows.Add()
                            Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count
                            Dgl1.Item(Col1SaleReturnDocId, I).Value = ""
                            Dgl1.Item(Col1PurchReturnDocId, I).Value = ""
                            Dgl1.Item(Col1Supplier, I).Tag = FGetSubCodeFromOMSId(AgL.XNull(DtSaleOrderDetail.Rows(I)("Supplier")))
                            Dgl1.Item(Col1Supplier, I).Value = AgL.XNull(DtSaleOrderDetail.Rows(I)("SupplierName"))
                            Dgl1.Item(Col1MasterSupplier, I).Tag = FGetSubCodeFromOMSId(AgL.XNull(DtSaleOrderDetail.Rows(I)("Supplier")))
                            Dgl1.Item(Col1MasterSupplier, I).Value = AgL.XNull(DtSaleOrderDetail.Rows(I)("SupplierName"))
                            Dgl1.Item(Col1ReturnNo, I).Value = ""
                            Dgl1.Item(Col1ReturnDate, I).Value = ""
                            Dgl1.Item(Col1ItemGroup, I).Tag = FGetItemCodeFromOMSId(AgL.XNull(DtSaleOrderDetail.Rows(I)("ItemGroup")))
                            Dgl1.Item(Col1ItemGroup, I).Value = AgL.XNull(DtSaleOrderDetail.Rows(I)("ItemGroupDesc"))
                            Dgl1.Item(Col1ReturnDiscountPer, I).Value = 0
                            Dgl1.Item(Col1ReturnAdditionalDiscountPer, I).Value = 0
                            Dgl1.Item(Col1Amount, I).Value = 0

                            mQry = "Select IG.Default_DiscountPerPurchase, IG.Default_AdditionalDiscountPerPurchase,
                                    0 As Default_AdditionPerPurchase
                                    From ItemGroup IG  
                                    Where IG.Code ='" & AgL.XNull(DtSaleOrderDetail.Rows(I)("ItemGroup")) & "'"
                            Dim DTPurchaseDiscounts As DataTable = AgL.FillData(mQry, Connection_Pakka).Tables(0)
                            If DTPurchaseDiscounts.Rows.Count > 0 Then
                                Dgl1.Item(Col1DiscountPer, I).Value = AgL.VNull(DTPurchaseDiscounts.Rows(0)("Default_DiscountPerPurchase"))
                                Dgl1.Item(Col1AdditionalDiscountPer, I).Value = AgL.VNull(DTPurchaseDiscounts.Rows(0)("Default_AdditionalDiscountPerPurchase"))
                                Dgl1.Item(Col1AdditionPer, I).Value = AgL.VNull(DTPurchaseDiscounts.Rows(0)("Default_AdditionPerPurchase"))
                            End If
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Function FGetDiscountRates(SubCode As String, Site_Code As String, Div_Code As String, ItemGroup As String) As DataTable
        mQry = "Select IfNull(Max(DiscountPer),0) As Default_DiscountPerSale,
                IfNull(Max(AdditionalDiscountPer),0) As Default_AdditionalDiscountPerSale,
                IfNull(Max(AdditionPer),0) As Default_AdditionPerSale
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
            If TxtOrderNo.Focused = True And e.KeyCode = Keys.Return Then SendKeys.Send("{Tab}")

            If e.KeyCode = (Keys.S And e.Control) Then
                FProcSave()
            End If
        End If
    End Sub
    Private Sub TxtBuyer_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtOrderNo.KeyDown, TxtTag.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then Exit Sub
            Select Case sender.name
                Case TxtTag.Name
                    If e.KeyCode <> Keys.Enter Then
                        If sender.AgHelpDataset Is Nothing Then
                            mQry = "Select Code, Description From Tag Where V_Type ='WSI' Order By Description"
                            TxtTag.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If

                Case TxtOrderNo.Name
                    If e.KeyCode <> Keys.Enter Then
                        If sender.AgHelpDataset Is Nothing Then

                            Dim Connection_Pakka_Temp As New SQLite.SQLiteConnection
                            If mDbEncryption = "N" Then
                                Connection_Pakka_Temp.ConnectionString = "DataSource=" & mDbPath & ";Version=3;"
                            Else
                                Connection_Pakka_Temp.ConnectionString = "DataSource=" & mDbPath & ";Version=3;Password=" & AgLibrary.ClsConstant.PubDbPassword & ";"
                            End If
                            Connection_Pakka_Temp.Open()



                            mQry = " Select H.DocId, H.ManualRefNo As OrderNo, H.V_Date As OrderDate
                                    From SaleInvoice H 
                                    Where H.V_Type = '" & Ncat.SaleReturn & "' 
                                    And H.Site_Code = '" & AgL.PubSiteCode & "'
                                    And H.Div_Code = '" & AgL.PubDivCode & "'
                                    And IfNull(H.Status,'" & AgTemplate.ClsMain.SaleOrderStatus.Active & "') <> '" & AgTemplate.ClsMain.SaleOrderStatus.Closed & "' "
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
        Dim bNoInvoicesFeed As Boolean = True
        For I As Integer = 0 To Dgl1.Rows.Count - 1
            If Dgl1.Item(Col1WReturnNo, I).Value <> "" Then
                bNoInvoicesFeed = False
            End If
        Next



        If bNoInvoicesFeed = True Then
            MsgBox("No Invoice Detail Entered...!", MsgBoxStyle.Information)
            FDataValidation = False
            Exit Function
        End If

        For I As Integer = 0 To Dgl1.Rows.Count - 1
            If AgL.XNull(Dgl1.Item(Col1WReturnNo, I).Value) <> "" Then
                If Dgl1.Item(Col1WReturnDate, I).Value = "" Then
                    MsgBox("W Invoice Date is blank for line no " & Dgl1.Item(ColSNo, I).Value & "", MsgBoxStyle.Information)
                    Dgl1.CurrentCell = Dgl1.Item(Col1WReturnDate, I)
                    Dgl1.Focus()
                    FDataValidation = False
                    Exit Function
                End If

                If CDate(Dgl1.Item(Col1WReturnDate, I).Value) > CDate(AgL.PubLoginDate) Then
                    MsgBox("Future date transaction is not allowed.", MsgBoxStyle.Information)
                    Dgl1.CurrentCell = Dgl1.Item(Col1WReturnDate, I)
                    Dgl1.Focus()
                    FDataValidation = False
                    Exit Function
                End If

                If Val(Dgl1.Item(Col1WQty, I).Value) = 0 Then
                    MsgBox("W Qty is blank for line no " & Dgl1.Item(ColSNo, I).Value & "", MsgBoxStyle.Information)
                    Dgl1.CurrentCell = Dgl1.Item(Col1WQty, I)
                    Dgl1.Focus()
                    FDataValidation = False
                    Exit Function
                End If

                If Val(Dgl1.Item(Col1WPurchReturnAmount, I).Value) < 0 Then
                    MsgBox("W Purchase Return Amount is negative for line no " & Dgl1.Item(ColSNo, I).Value & "", MsgBoxStyle.Information)
                    Dgl1.CurrentCell = Dgl1.Item(Col1WPurchReturnAmount, I)
                    Dgl1.Focus()
                    FDataValidation = False
                    Exit Function
                End If

                If AgL.XNull(Dgl1.Item(Col1ReturnNo, I).Value) <> "" And
                        AgL.XNull(Dgl1.Item(Col1Supplier, I).Value) = "" Then
                    MsgBox("Purchase Return No is not blank But Party is blank at line no " & Dgl1.Item(ColSNo, I).Value & "", MsgBoxStyle.Information)
                    Dgl1.CurrentCell = Dgl1.Item(Col1Supplier, I)
                    Dgl1.Focus()
                    FDataValidation = False
                    Exit Function
                End If

                For J As Integer = 0 To Dgl2.Rows.Count - 1
                    If Dgl1.Item(Col1SaleReturnDocId, I).Value = Dgl2.Item(Col2SaleReturnDocId, J).Value Then
                        If Dgl2.Item(Col2WReturnNo, J).Value = "" Then
                            MsgBox("W Sale Return No is blank for line no " & Dgl2.Item(ColSNo, J).Value & "", MsgBoxStyle.Information)
                            Dgl2.CurrentCell = Dgl2.Item(Col2WReturnNo, J)
                            Dgl2.Focus()
                            FDataValidation = False
                            Exit Function
                        End If

                        If Dgl2.Item(Col2ReturnDate, J).Value = "" Then
                            If Dgl2.Item(Col2WReturnDate, J).Value = "" Then
                                MsgBox("W Invoice Date is blank for line no " & Dgl2.Item(ColSNo, J).Value & "", MsgBoxStyle.Information)
                                Dgl2.CurrentCell = Dgl2.Item(Col2WReturnDate, J)
                                Dgl2.Focus()
                                FDataValidation = False
                                Exit Function
                            End If
                        End If

                        If AgL.XNull(Dgl2.Item(Col2WReturnDate, J).Value) <> "" Then
                            If CDate(Dgl2.Item(Col2WReturnDate, J).Value) > CDate(AgL.PubLoginDate) Then
                                MsgBox("Future date transaction is not allowed.", MsgBoxStyle.Information)
                                Dgl2.CurrentCell = Dgl2.Item(Col2WReturnDate, J)
                                Dgl2.Focus()
                                FDataValidation = False
                                Exit Function
                            End If
                        End If




                        If Val(Dgl2.Item(Col2WQty, J).Value) = 0 Then
                            MsgBox("W Qty is blank for line no " & Dgl2.Item(ColSNo, J).Value & "", MsgBoxStyle.Information)
                            Dgl2.CurrentCell = Dgl2.Item(Col2WQty, J)
                            Dgl2.Focus()
                            FDataValidation = False
                            Exit Function
                        End If

                        If Val(Dgl2.Item(Col2WSaleReturnAmount, J).Value) < 0 Then
                            MsgBox("W Sale Return Amount is negative for line no " & Dgl2.Item(ColSNo, J).Value & "", MsgBoxStyle.Information)
                            Dgl2.CurrentCell = Dgl2.Item(Col2WSaleReturnAmount, J)
                            Dgl2.Focus()
                            FDataValidation = False
                            Exit Function
                        End If
                    End If
                Next
            End If
        Next

        For J As Integer = 0 To Dgl2.Rows.Count - 1
            If Dgl2.Item(Col2WSaleOrderDocId, J).Value = "" Then
                MsgBox("Sale Order is not Synced...!", MsgBoxStyle.Information)
                Dgl2.CurrentCell = Dgl2.Item(Col2WReturnNo, J)
                Dgl2.Focus()
                FDataValidation = False
                Exit Function
            End If



            If J > 0 Then
                If Dgl2.Item(Col2WReturnNo, J).Value <> "" Then
                    If Dgl2.Item(Col2WReturnNo, J).Value <> "" And Dgl2.Item(Col2WReturnNo, J - 1).Value <> "" Then
                        If Dgl2.Item(Col2WReturnNo, J).Value <> Dgl2.Item(Col2WReturnNo, J - 1).Value Then
                            MsgBox("Multiple Kachha Sale Returns are not allowed in single entry.", MsgBoxStyle.Information)
                            Dgl2.CurrentCell = Dgl2.Item(Col2WReturnNo, J)
                            Dgl2.Focus()
                            FDataValidation = False
                            Exit Function
                        End If
                    End If
                End If
            End If

            If J > 0 Then
                If Dgl2.Item(Col2ReturnNo, J).Value <> "" Then
                    If Dgl2.Item(Col2ReturnNo, J).Value = Dgl2.Item(Col2ReturnNo, J - 1).Value Then
                        If Dgl2.Item(Col2WReturnNo, J).Value <> Dgl2.Item(Col2WReturnNo, J - 1).Value Then
                            MsgBox("Pakka Sale Returns are same but Kachha Sale Returns Nos are different.Can't allow.", MsgBoxStyle.Information)
                            Dgl2.CurrentCell = Dgl2.Item(Col2WReturnNo, J)
                            Dgl2.Focus()
                            FDataValidation = False
                            Exit Function
                        End If
                    End If
                End If
            End If


            If AgL.XNull(Dgl2.Item(Col2ReturnNo, J).Value) <> "" And
                        AgL.XNull(Dgl2.Item(Col2Party, J).Value) = "" Then
                MsgBox("Purchase Return No is not blank But Party is blank at line no " & Dgl2.Item(ColSNo, J).Value & "", MsgBoxStyle.Information)
                Dgl2.CurrentCell = Dgl2.Item(Col2Party, J)
                Dgl2.Focus()
                FDataValidation = False
                Exit Function
            End If


            For I As Integer = 0 To Dgl1.Rows.Count - 1
                If Dgl1.Item(Col1SaleReturnDocId, I).Value = Dgl2.Item(Col2SaleReturnDocId, J).Value Then
                    If Dgl2.Item(Col2WReturnNo, J).Value <> "" Then
                        If Dgl1.Item(Col1WReturnNo, I).Value = "" Then
                            MsgBox("W Purchase Return No is blank for line no " & Dgl1.Item(ColSNo, I).Value & "", MsgBoxStyle.Information)
                            Dgl1.CurrentCell = Dgl1.Item(Col1WReturnNo, I)
                            Dgl1.Focus()
                            FDataValidation = False
                            Exit Function
                        End If
                    End If
                End If
            Next
        Next

        FDataValidation = True
    End Function
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
            If AgL.XNull(Dgl1.Item(Col1WReturnNo, I).Value) <> "" Then
                'If AgL.VNull(Dgl1.Item(Col1WPurchInvoiceAmount, I).Value) <> 0 Then
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
                PurchInvoiceTable.V_Type = "WPR"
                PurchInvoiceTable.V_Prefix = ""
                PurchInvoiceTable.Site_Code = AgL.PubSiteCode
                PurchInvoiceTable.Div_Code = AgL.PubDivCode
                PurchInvoiceTable.V_No = 0
                PurchInvoiceTable.V_Date = Dgl1.Item(Col1WReturnDate, I).Value
                PurchInvoiceTable.ManualRefNo = ""
                PurchInvoiceTable.Vendor = Dgl1.Item(Col1MasterSupplier, I).Tag
                PurchInvoiceTable.VendorName = Dgl1.Item(Col1MasterSupplier, I).Value
                PurchInvoiceTable.AgentCode = ""
                PurchInvoiceTable.AgentName = ""
                PurchInvoiceTable.BillToPartyCode = Dgl1.Item(Col1MasterSupplier, I).Tag
                PurchInvoiceTable.BillToPartyName = Dgl1.Item(Col1MasterSupplier, I).Value
                PurchInvoiceTable.VendorAddress = ""
                PurchInvoiceTable.VendorCity = ""
                PurchInvoiceTable.VendorMobile = ""
                PurchInvoiceTable.VendorSalesTaxNo = ""
                PurchInvoiceTable.SalesTaxGroupParty = ""
                PurchInvoiceTable.PlaceOfSupply = ""
                PurchInvoiceTable.StructureCode = ""
                PurchInvoiceTable.CustomFields = ""
                PurchInvoiceTable.VendorDocNo = Dgl1.Item(Col1WReturnNo, I).Value
                PurchInvoiceTable.VendorDocDate = Dgl1.Item(Col1WReturnDate, I).Value
                PurchInvoiceTable.ReferenceDocId = ""
                PurchInvoiceTable.GenDocId = ""
                PurchInvoiceTable.GenDocIdSr = ""
                PurchInvoiceTable.Tags = "+" & TxtTag.Text
                PurchInvoiceTable.Remarks = "Kachha Invoice Amount : " + Dgl1.Item(Col1WAmount, I).Value.ToString
                PurchInvoiceTable.Status = "Active"
                PurchInvoiceTable.EntryBy = AgL.PubUserName
                PurchInvoiceTable.EntryDate = AgL.GetDateTime(AgL.GcnRead)
                PurchInvoiceTable.ApproveBy = ""
                PurchInvoiceTable.ApproveDate = ""
                PurchInvoiceTable.MoveToLog = ""
                PurchInvoiceTable.MoveToLogDate = ""
                PurchInvoiceTable.UploadDate = ""
                PurchInvoiceTable.LockText = "Genereded From Sale Return W Entry.Can't Edit."

                PurchInvoiceTable.Deduction_Per = 0
                PurchInvoiceTable.Deduction = 0
                PurchInvoiceTable.Other_Charge_Per = 0
                PurchInvoiceTable.Other_Charge = 0
                PurchInvoiceTable.Round_Off = 0
                PurchInvoiceTable.Net_Amount = 0


                'For Line Detail
                PurchInvoiceTable.Line_Sr = 1
                PurchInvoiceTable.Line_ItemCode = Dgl1.Item(Col1ItemGroup, I).Tag
                PurchInvoiceTable.Line_ItemName = Dgl1.Item(Col1ItemGroup, I).Value
                PurchInvoiceTable.Line_Specification = ""
                PurchInvoiceTable.Line_SalesTaxGroupItem = "GST 0%"
                PurchInvoiceTable.Line_ReferenceNo = ""
                PurchInvoiceTable.Line_DocQty = -Val(Dgl1.Item(Col1WQty, I).Value)
                PurchInvoiceTable.Line_FreeQty = 0
                PurchInvoiceTable.Line_Qty = -Val(Dgl1.Item(Col1WQty, I).Value)
                PurchInvoiceTable.Line_Unit = "Nos"
                PurchInvoiceTable.Line_Pcs = 0
                PurchInvoiceTable.Line_UnitMultiplier = 0
                PurchInvoiceTable.Line_DealUnit = ""
                PurchInvoiceTable.Line_DocDealQty = ""
                PurchInvoiceTable.Line_DiscountPer = 0
                PurchInvoiceTable.Line_DiscountAmount = 0
                PurchInvoiceTable.Line_AdditionalDiscountPer = 0
                PurchInvoiceTable.Line_AdditionalDiscountAmount = 0
                PurchInvoiceTable.Line_Amount = -Val(Dgl1.Item(Col1WPurchReturnAmount, I).Value)
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
                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Sr = 2
                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_ItemCode = ""
                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_ItemName = ItemCode.Packing
                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Specification = ""
                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_SalesTaxGroupItem = "GST 0%"
                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_ReferenceNo = ""
                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_DocQty = 1
                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_FreeQty = 0
                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Qty = 1
                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Unit = "Nos"
                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Pcs = 0
                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_UnitMultiplier = 0
                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_DealUnit = ""
                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_DocDealQty = ""
                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_DiscountPer = 0
                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_DiscountAmount = 0
                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_AdditionalDiscountPer = 0
                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_AdditionalDiscountAmount = 0
                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Amount = Val(Dgl1.Item(Col1WPacking, I).Value)
                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Rate = Val(Dgl1.Item(Col1WPacking, I).Value)
                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Remark = ""
                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_BaleNo = ""
                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_LotNo = ""
                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_ReferenceDocId = ""
                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_ReferenceSr = ""
                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_PurchInvoice = ""
                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_PurchInvoiceSr = ""
                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_GrossWeight = 0
                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_NetWeight = 0
                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Gross_Amount = PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Amount
                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Taxable_Amount = PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Amount
                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Tax1_Per = 0
                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Tax1 = PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Taxable_Amount * PurchInvoiceTable.Line_Tax1_Per / 100
                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Tax2_Per = 0
                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Tax2 = PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Taxable_Amount * PurchInvoiceTable.Line_Tax2_Per / 100
                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Tax3_Per = 0
                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Tax3 = PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Taxable_Amount * PurchInvoiceTable.Line_Tax3_Per / 100
                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Tax4_Per = 0
                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Tax4 = PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Taxable_Amount * PurchInvoiceTable.Line_Tax4_Per / 100
                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Tax5_Per = 0
                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Tax5 = PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Taxable_Amount * PurchInvoiceTable.Line_Tax5_Per / 100
                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_SubTotal1 = PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Amount + PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Tax1 + PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Tax2 +
                                                                PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Tax3 + PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Tax4 + PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Tax5
                    'For Header Values
                    Tot_Gross_Amount += PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Gross_Amount
                    Tot_Taxable_Amount += PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Taxable_Amount
                    Tot_Tax1 += PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Tax1
                    Tot_Tax2 += PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Tax2
                    Tot_Tax3 += PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Tax3
                    Tot_Tax4 += PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Tax4
                    Tot_Tax5 += PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Tax5
                    Tot_SubTotal1 += PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_SubTotal1

                    'PurchInvoiceTableList(UBound(PurchInvoiceTableList)) = PurchInvoiceTable
                    ReDim Preserve PurchInvoiceTableList(UBound(PurchInvoiceTableList) + 1)
                End If
#End Region

#Region "Freight Charge"
                If Val(Dgl1.Item(Col1WFreight, I).Value) > 0 Then
                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Sr = 3
                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_ItemCode = ""
                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_ItemName = ItemCode.Freight
                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Specification = ""
                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_SalesTaxGroupItem = "GST 0%"
                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_ReferenceNo = ""
                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_DocQty = 1
                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_FreeQty = 0
                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Qty = 1
                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Unit = "Nos"
                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Pcs = 0
                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_UnitMultiplier = 0
                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_DealUnit = ""
                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_DocDealQty = ""
                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_DiscountPer = 0
                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_DiscountAmount = 0
                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_AdditionalDiscountPer = 0
                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_AdditionalDiscountAmount = 0
                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Amount = Val(Dgl1.Item(Col1WFreight, I).Value)
                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Rate = Val(Dgl1.Item(Col1WFreight, I).Value)
                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Remark = ""
                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_BaleNo = ""
                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_LotNo = ""
                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_ReferenceDocId = ""
                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_ReferenceSr = ""
                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_PurchInvoice = ""
                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_PurchInvoiceSr = ""
                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_GrossWeight = 0
                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_NetWeight = 0
                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Gross_Amount = PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Amount
                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Taxable_Amount = PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Amount
                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Tax1_Per = 0
                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Tax1 = PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Taxable_Amount * PurchInvoiceTable.Line_Tax1_Per / 100
                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Tax2_Per = 0
                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Tax2 = PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Taxable_Amount * PurchInvoiceTable.Line_Tax2_Per / 100
                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Tax3_Per = 0
                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Tax3 = PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Taxable_Amount * PurchInvoiceTable.Line_Tax3_Per / 100
                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Tax4_Per = 0
                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Tax4 = PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Taxable_Amount * PurchInvoiceTable.Line_Tax4_Per / 100
                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Tax5_Per = 0
                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Tax5 = PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Taxable_Amount * PurchInvoiceTable.Line_Tax5_Per / 100
                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_SubTotal1 = PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Taxable_Amount + PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Tax1 + PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Tax2 +
                                                                PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Tax3 + PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Tax4 + PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Tax5
                    'For Header Values
                    Tot_Gross_Amount += PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Gross_Amount
                    Tot_Taxable_Amount += PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Taxable_Amount
                    Tot_Tax1 += PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Tax1
                    Tot_Tax2 += PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Tax2
                    Tot_Tax3 += PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Tax3
                    Tot_Tax4 += PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Tax4
                    Tot_Tax5 += PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Tax5
                    Tot_SubTotal1 += PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_SubTotal1

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
                    If Val(PurchInvoiceTableList(0).Gross_Amount) > 0 Then
                        PurchInvoiceTableList(J).Line_Round_Off = Math.Round(PurchInvoiceTableList(0).Round_Off * PurchInvoiceTableList(J).Line_Gross_Amount / PurchInvoiceTableList(0).Gross_Amount, 2)
                        PurchInvoiceTableList(J).Line_Net_Amount = Math.Round(PurchInvoiceTableList(0).Net_Amount * PurchInvoiceTableList(J).Line_Gross_Amount / PurchInvoiceTableList(0).Gross_Amount, 2)
                    End If
                    Tot_RoundOff += Val(PurchInvoiceTableList(J).Line_Round_Off)
                    Tot_NetAmount += Val(PurchInvoiceTableList(J).Line_Net_Amount)
                Next

                If Tot_RoundOff <> PurchInvoiceTableList(0).Round_Off Then
                    PurchInvoiceTableList(0).Line_Round_Off = PurchInvoiceTableList(0).Line_Round_Off + (PurchInvoiceTableList(0).Round_Off - Tot_RoundOff)
                End If

                If Tot_NetAmount <> PurchInvoiceTableList(0).Net_Amount Then
                    PurchInvoiceTableList(0).Line_Net_Amount = PurchInvoiceTableList(0).Line_Net_Amount + (PurchInvoiceTableList(0).Net_Amount - Tot_NetAmount)
                End If
                'If PurchInvoiceTableList(0).Net_Amount > 0 Then
                Dim bDocId As String = FrmPurchInvoiceDirect.InsertPurchInvoice(PurchInvoiceTableList)
                If AgL.XNull(bDocId) <> "" Then
                    Dgl1.Item(Col1WPurchReturnDocId, I).Value = bDocId
                    mQry = " INSERT INTO SaleInvoiceGeneratedEntries(Code, Type, DocId, SaleOrderNo, SaleOrderDocId, Site_Code, Div_Code, V_Type) 
                                    Select '" & mSearchCode & "' As Code, 'Purchase Return', '" & bDocId & "', '" & TxtOrderNo.Text & "', 
                                    '" & TxtSaleOrderDocId_W.Text & "', '" & AgL.PubSiteCode & "', '" & AgL.PubDivCode & "', '" & PurchInvoiceTableList(0).V_Type & "' "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                    'mQry = "INSERT INTO PurchInvoiceTransport (DocID, Transporter, LrNo, LrDate, PrivateMark, Weight, Freight, PaymentType, RoadPermitNo, RoadPermitDate, UploadDate)
                    '        SELECT PI.DocID, TP.Subcode Transporter, PPT.LrNo, PPT.LrDate, PPT.PrivateMark, PPT.Weight, PPT.Freight, PPT.PaymentType, PPT.RoadPermitNo, PPT.RoadPermitDate, PPT.UploadDate
                    '        FROM PurchInvoice PI
                    '        LEFT JOIN Pakka.PurchInvoice PPI ON PI.OmsID = PPI.DocID 
                    '        LEFT JOIN Pakka.PurchInvoiceTransport PPT ON PPT.DocID = PPI.DocID 
                    '        LEFT JOIN Subgroup TP ON PPT.Transporter = TP.OmsId 
                    '        WHERE PI.V_Type ='PI' AND PPT.DocID  IS NOT NULL "

                    If AgL.XNull(Dgl1.Item(Col1SyncedPurchReturnDocId, I).Value) <> "" Then
                        mQry = "Select Count(*) from SaleInvoiceGeneratedEntries Where DocID = '" & AgL.XNull(Dgl1.Item(Col1SyncedPurchReturnDocId, I).Value) & "'"
                        If AgL.Dman_Execute(mQry, AgL.GCn, AgL.ECmd).ExecuteScalar() = 0 Then
                            mQry = " INSERT INTO SaleInvoiceGeneratedEntries(Code, Type, DocId, SaleOrderNo, SaleOrderDocId, Site_Code, Div_Code, V_Type) 
                                    Select '" & mSearchCode & "' As Code, 'Purchase Return', '" & Dgl1.Item(Col1SyncedPurchReturnDocId, I).Value & "', '" & TxtOrderNo.Text & "', 
                                    '" & TxtSaleOrderDocId_W.Text & "', '" & AgL.PubSiteCode & "', '" & AgL.PubDivCode & "','PI' As V_Type "
                            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                            mQry = " UPDATE PurchInvoice Set GenDocId = '" & Dgl1.Item(Col1SyncedPurchReturnDocId, I).Value & "'
                                    Where DocId = '" & bDocId & "'"
                            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                            mQry = " UPDATE Ledger Set EffectiveDate = " & AgL.Chk_Date(Dgl1.Item(Col1WReturnDate, I).Value) & "
                                Where DocId = '" & Dgl1.Item(Col1SyncedPurchReturnDocId, I).Value & "'"
                            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                            mQry = " UPDATE Ledger Set EffectiveDate = " & AgL.Chk_Date(Dgl1.Item(Col1WReturnDate, I).Value) & "
                                Where DocId = '" & Dgl1.Item(Col1PurchReturnDocId, I).Value & "'"
                            AgL.Dman_ExecuteNonQry(mQry, Connection_Pakka)

                            mQry = "Insert Into TransactionReferences (DocID, ReferenceDocID, IsEditingAllowed, IsDeletingAllowed) 
                                    Values (" & AgL.Chk_Text(Dgl1.Item(Col1SyncedPurchReturnDocId, I).Value) & ", '" & bDocId & "', 1, 0) "
                            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                        End If
                    Else
                        mQry = "Insert Into TransactionReferences (DocID, ReferenceDocID, IsEditingAllowed, IsDeletingAllowed) 
                                    Values (" & AgL.Chk_Text(bDocId) & ", '" & bDocId & "', 1, 0) "
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                    End If
                Else
                    If AgL.XNull(Dgl1.Item(Col1SyncedPurchReturnDocId, I).Value) <> "" Then
                        mQry = " INSERT INTO SaleInvoiceGeneratedEntries(Code, Type, DocId, SaleOrderNo, SaleOrderDocId, Site_Code, Div_Code, V_Type) 
                                        Select '" & mSearchCode & "' As Code, 'Purchase Return', '" & Dgl1.Item(Col1SyncedPurchReturnDocId, I).Value & "', '" & TxtOrderNo.Text & "', 
                                        '" & TxtSaleOrderDocId_W.Text & "', '" & AgL.PubSiteCode & "', '" & AgL.PubDivCode & "','PI' As V_Type "
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                        mQry = " UPDATE Ledger Set EffectiveDate = " & AgL.Chk_Date(Dgl1.Item(Col1WReturnDate, I).Value) & "
                                Where DocId = '" & Dgl1.Item(Col1SyncedPurchReturnDocId, I).Value & "'"
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                        mQry = " UPDATE Ledger Set EffectiveDate = " & AgL.Chk_Date(Dgl1.Item(Col1WReturnDate, I).Value) & "
                                Where DocId = '" & Dgl1.Item(Col1PurchReturnDocId, I).Value & "'"
                        AgL.Dman_ExecuteNonQry(mQry, Connection_Pakka)
                    End If
                End If
            End If
        Next
    End Sub
    Public Sub FPostSaleData_ForDifference(Conn As Object, Cmd As Object)
        Dim ErrorLog As String = ""
        Dim DtMain As DataTable = Nothing
        Dim dtTemp As DataTable = Nothing
        Dim I As Integer
        Dim StrErrLog As String = ""
        Dim mRow As Integer = 0


        Dim Tot_Gross_Amount As Double = 0
        Dim Tot_Taxable_Amount As Double = 0
        Dim Tot_Tax1 As Double = 0
        Dim Tot_Tax2 As Double = 0
        Dim Tot_Tax3 As Double = 0
        Dim Tot_Tax4 As Double = 0
        Dim Tot_Tax5 As Double = 0
        Dim Tot_SubTotal1 As Double = 0


        Tot_Gross_Amount = 0
        Tot_Taxable_Amount = 0
        Tot_Tax1 = 0
        Tot_Tax2 = 0
        Tot_Tax3 = 0
        Tot_Tax4 = 0
        Tot_Tax5 = 0
        Tot_SubTotal1 = 0


        For M As Integer = 0 To Dgl2.Rows.Count - 1
            If AgL.XNull(Dgl2.Item(Col2WReturnNo, M).Value) <> "" Then
                mRow = M
            End If
        Next

        Dim SaleReturnTableList(0) As FrmSaleInvoiceDirect.StructSaleInvoice

        'In One Transaction only one Sale Return Can be Generated.The First Sale Return No will be 
        'Considered As Sale Return No
        If AgL.XNull(Dgl2.Item(Col2WReturnNo, mRow).Value) <> "" Then
            'If AgL.VNull(Dgl2.Item(Col2WSaleReturnAmount, mRow).Value) <> 0 Then
            SaleReturnTableList(0).DocID = ""
            SaleReturnTableList(0).V_Type = "WSR"
            SaleReturnTableList(0).V_Prefix = ""
            SaleReturnTableList(0).Site_Code = AgL.PubSiteCode
            SaleReturnTableList(0).Div_Code = AgL.PubDivCode
            SaleReturnTableList(0).V_No = 0
            SaleReturnTableList(0).V_Date = Dgl2.Item(Col2WReturnDate, mRow).Value
            'SaleInvoiceTableList(0).V_Date = Dgl2.Item(Col2InvoiceDate, mRow).Value
            SaleReturnTableList(0).ManualRefNo = Dgl2.Item(Col2WReturnNo, mRow).Value
            SaleReturnTableList(0).SaleToParty = Dgl2.Item(Col2Party, mRow).Tag
            SaleReturnTableList(0).SaleToPartyName = Dgl2.Item(Col2Party, mRow).Value
            SaleReturnTableList(0).AgentCode = ""
            SaleReturnTableList(0).AgentName = ""
            SaleReturnTableList(0).BillToPartyCode = Dgl2.Item(Col2MasterParty, mRow).Tag
            SaleReturnTableList(0).BillToPartyName = Dgl2.Item(Col2MasterParty, mRow).Value
            SaleReturnTableList(0).SaleToPartyAddress = ""
            SaleReturnTableList(0).SaleToPartyCity = ""
            SaleReturnTableList(0).SaleToPartyMobile = ""
            SaleReturnTableList(0).SaleToPartySalesTaxNo = ""
            SaleReturnTableList(0).ShipToAddress = ""
            SaleReturnTableList(0).SalesTaxGroupParty = ""
            SaleReturnTableList(0).PlaceOfSupply = PlaceOfSupplay.WithinState
            SaleReturnTableList(0).StructureCode = ""
            SaleReturnTableList(0).CustomFields = ""
            SaleReturnTableList(0).ReferenceDocId = ""
            SaleReturnTableList(0).Tags = "+" & TxtTag.Text
            SaleReturnTableList(0).Remarks = "Pakka Invoice No : " + Dgl2.Item(Col2ReturnNo, mRow).Value.ToString +
                                                        " And Invoice Amount : " + Dgl2.Item(Col2Amount, mRow).Value.ToString
            SaleReturnTableList(0).Status = "Active"
            SaleReturnTableList(0).EntryBy = AgL.PubUserName
            SaleReturnTableList(0).EntryDate = AgL.GetDateTime(AgL.GcnRead)
            SaleReturnTableList(0).ApproveBy = ""
            SaleReturnTableList(0).ApproveDate = ""
            SaleReturnTableList(0).MoveToLog = ""
            SaleReturnTableList(0).MoveToLogDate = ""
            SaleReturnTableList(0).UploadDate = ""
            SaleReturnTableList(0).LockText = "Genereded From Sale Return W Entry.Can't Edit."

            SaleReturnTableList(0).Deduction_Per = 0
            SaleReturnTableList(0).Deduction = 0
            SaleReturnTableList(0).Other_Charge_Per = 0
            SaleReturnTableList(0).Other_Charge = 0
            SaleReturnTableList(0).Round_Off = 0
            SaleReturnTableList(0).Net_Amount = 0

            For I = 0 To Dgl2.Rows.Count - 1
                If Val(Dgl2.Item(Col2WQty, I).Value) > 0 Then
                    SaleReturnTableList(UBound(SaleReturnTableList)).Line_Sr = UBound(SaleReturnTableList) + 1
                    SaleReturnTableList(UBound(SaleReturnTableList)).Line_ItemCode = Dgl2.Item(Col2ItemGroup, I).Tag
                    SaleReturnTableList(UBound(SaleReturnTableList)).Line_ItemName = Dgl2.Item(Col2ItemGroup, I).Value
                    SaleReturnTableList(UBound(SaleReturnTableList)).Line_Specification = ""
                    SaleReturnTableList(UBound(SaleReturnTableList)).Line_SalesTaxGroupItem = "GST 0%"
                    SaleReturnTableList(UBound(SaleReturnTableList)).Line_ReferenceNo = ""
                    SaleReturnTableList(UBound(SaleReturnTableList)).Line_DocQty = -Val(Dgl2.Item(Col2WQty, I).Value)
                    SaleReturnTableList(UBound(SaleReturnTableList)).Line_FreeQty = 0
                    SaleReturnTableList(UBound(SaleReturnTableList)).Line_Qty = -Val(Dgl2.Item(Col2WQty, I).Value)
                    SaleReturnTableList(UBound(SaleReturnTableList)).Line_Unit = "Nos"
                    SaleReturnTableList(UBound(SaleReturnTableList)).Line_Pcs = 0
                    SaleReturnTableList(UBound(SaleReturnTableList)).Line_UnitMultiplier = 0
                    SaleReturnTableList(UBound(SaleReturnTableList)).Line_DealUnit = ""
                    SaleReturnTableList(UBound(SaleReturnTableList)).Line_DocDealQty = ""
                    SaleReturnTableList(UBound(SaleReturnTableList)).Line_DiscountPer = Val(Dgl2.Item(Col2DiscountPer, I).Value)
                    SaleReturnTableList(UBound(SaleReturnTableList)).Line_DiscountAmount = Val(Dgl2.Item(Col2WDiscount, I).Value)
                    SaleReturnTableList(UBound(SaleReturnTableList)).Line_AdditionalDiscountPer = Val(Dgl2.Item(Col2AdditionalDiscountPer, I).Value)
                    SaleReturnTableList(UBound(SaleReturnTableList)).Line_AdditionalDiscountAmount = 0
                    SaleReturnTableList(UBound(SaleReturnTableList)).Line_AdditionPer = Val(Dgl2.Item(Col2AdditionPer, I).Value)
                    SaleReturnTableList(UBound(SaleReturnTableList)).Line_AdditionAmount = 0
                    SaleReturnTableList(UBound(SaleReturnTableList)).Line_Amount = -Val(Dgl2.Item(Col2WSaleReturnAmount, I).Value)
                    SaleReturnTableList(UBound(SaleReturnTableList)).Line_Rate = Math.Abs((Val(Dgl2.Item(Col2WSaleReturnAmount, I).Value) + Val(Dgl2.Item(Col2WDiscount, I).Value)) / Val(Dgl2.Item(Col2WQty, I).Value))
                    SaleReturnTableList(UBound(SaleReturnTableList)).Line_Remark = ""
                    SaleReturnTableList(UBound(SaleReturnTableList)).Line_BaleNo = ""
                    SaleReturnTableList(UBound(SaleReturnTableList)).Line_LotNo = ""
                    SaleReturnTableList(UBound(SaleReturnTableList)).Line_ReferenceDocId = ""
                    SaleReturnTableList(UBound(SaleReturnTableList)).Line_SaleInvoice = Dgl2.Item(Col2WSaleOrderDocId, I).Value
                    SaleReturnTableList(UBound(SaleReturnTableList)).Line_SaleInvoiceSr = 1
                    SaleReturnTableList(UBound(SaleReturnTableList)).Line_GrossWeight = 0
                    SaleReturnTableList(UBound(SaleReturnTableList)).Line_NetWeight = 0
                    SaleReturnTableList(UBound(SaleReturnTableList)).Line_Gross_Amount = SaleReturnTableList(UBound(SaleReturnTableList)).Line_Amount
                    SaleReturnTableList(UBound(SaleReturnTableList)).Line_Taxable_Amount = SaleReturnTableList(UBound(SaleReturnTableList)).Line_Amount
                    SaleReturnTableList(UBound(SaleReturnTableList)).Line_Tax1_Per = 0
                    SaleReturnTableList(UBound(SaleReturnTableList)).Line_Tax1 = SaleReturnTableList(UBound(SaleReturnTableList)).Line_Amount * SaleReturnTableList(UBound(SaleReturnTableList)).Line_Tax1_Per / 100
                    SaleReturnTableList(UBound(SaleReturnTableList)).Line_Tax2_Per = 0
                    SaleReturnTableList(UBound(SaleReturnTableList)).Line_Tax2 = SaleReturnTableList(UBound(SaleReturnTableList)).Line_Amount * SaleReturnTableList(UBound(SaleReturnTableList)).Line_Tax2_Per / 100
                    SaleReturnTableList(UBound(SaleReturnTableList)).Line_Tax3_Per = 0
                    SaleReturnTableList(UBound(SaleReturnTableList)).Line_Tax3 = SaleReturnTableList(UBound(SaleReturnTableList)).Line_Amount * SaleReturnTableList(UBound(SaleReturnTableList)).Line_Tax3_Per / 100
                    SaleReturnTableList(UBound(SaleReturnTableList)).Line_Tax4_Per = 0
                    SaleReturnTableList(UBound(SaleReturnTableList)).Line_Tax4 = SaleReturnTableList(UBound(SaleReturnTableList)).Line_Amount * SaleReturnTableList(UBound(SaleReturnTableList)).Line_Tax4_Per / 100
                    SaleReturnTableList(UBound(SaleReturnTableList)).Line_Tax5_Per = 0
                    SaleReturnTableList(UBound(SaleReturnTableList)).Line_Tax5 = SaleReturnTableList(UBound(SaleReturnTableList)).Line_Amount * SaleReturnTableList(UBound(SaleReturnTableList)).Line_Tax5_Per / 100
                    SaleReturnTableList(UBound(SaleReturnTableList)).Line_SubTotal1 = SaleReturnTableList(UBound(SaleReturnTableList)).Line_Amount +
                                                                SaleReturnTableList(UBound(SaleReturnTableList)).Line_Tax1 +
                                                                SaleReturnTableList(UBound(SaleReturnTableList)).Line_Tax2 +
                                                                SaleReturnTableList(UBound(SaleReturnTableList)).Line_Tax3 +
                                                                SaleReturnTableList(UBound(SaleReturnTableList)).Line_Tax4 +
                                                                SaleReturnTableList(UBound(SaleReturnTableList)).Line_Tax5


                    'For Header Values
                    Tot_Gross_Amount += SaleReturnTableList(UBound(SaleReturnTableList)).Line_Gross_Amount
                    Tot_Taxable_Amount += SaleReturnTableList(UBound(SaleReturnTableList)).Line_Taxable_Amount
                    Tot_Tax1 += SaleReturnTableList(UBound(SaleReturnTableList)).Line_Tax1
                    Tot_Tax2 += SaleReturnTableList(UBound(SaleReturnTableList)).Line_Tax2
                    Tot_Tax3 += SaleReturnTableList(UBound(SaleReturnTableList)).Line_Tax3
                    Tot_Tax4 += SaleReturnTableList(UBound(SaleReturnTableList)).Line_Tax4
                    Tot_Tax5 += SaleReturnTableList(UBound(SaleReturnTableList)).Line_Tax5
                    Tot_SubTotal1 += SaleReturnTableList(UBound(SaleReturnTableList)).Line_SubTotal1


                    'SaleInvoiceTableList(UBound(SaleInvoiceTableList)) = SaleInvoiceTable
                    ReDim Preserve SaleReturnTableList(UBound(SaleReturnTableList) + 1)

#Region "Packing Charge"
                    If Val(Dgl2.Item(Col2WPacking, I).Value) > 0 Then
                        SaleReturnTableList(UBound(SaleReturnTableList)).Line_Sr = UBound(SaleReturnTableList) + 1
                        SaleReturnTableList(UBound(SaleReturnTableList)).Line_ItemCode = ""
                        SaleReturnTableList(UBound(SaleReturnTableList)).Line_ItemName = ItemCode.Packing
                        SaleReturnTableList(UBound(SaleReturnTableList)).Line_Specification = ""
                        SaleReturnTableList(UBound(SaleReturnTableList)).Line_SalesTaxGroupItem = "GST 0%"
                        SaleReturnTableList(UBound(SaleReturnTableList)).Line_ReferenceNo = ""
                        SaleReturnTableList(UBound(SaleReturnTableList)).Line_DocQty = 1
                        SaleReturnTableList(UBound(SaleReturnTableList)).Line_FreeQty = 0
                        SaleReturnTableList(UBound(SaleReturnTableList)).Line_Qty = 1
                        SaleReturnTableList(UBound(SaleReturnTableList)).Line_Unit = "Nos"
                        SaleReturnTableList(UBound(SaleReturnTableList)).Line_Pcs = 0
                        SaleReturnTableList(UBound(SaleReturnTableList)).Line_UnitMultiplier = 0
                        SaleReturnTableList(UBound(SaleReturnTableList)).Line_DealUnit = ""
                        SaleReturnTableList(UBound(SaleReturnTableList)).Line_DocDealQty = ""
                        SaleReturnTableList(UBound(SaleReturnTableList)).Line_DiscountPer = 0
                        SaleReturnTableList(UBound(SaleReturnTableList)).Line_DiscountAmount = 0
                        SaleReturnTableList(UBound(SaleReturnTableList)).Line_AdditionalDiscountPer = 0
                        SaleReturnTableList(UBound(SaleReturnTableList)).Line_AdditionalDiscountAmount = 0
                        SaleReturnTableList(UBound(SaleReturnTableList)).Line_Amount = Val(Dgl2.Item(Col2WPacking, I).Value)
                        SaleReturnTableList(UBound(SaleReturnTableList)).Line_Rate = Val(Dgl2.Item(Col2WPacking, I).Value)
                        SaleReturnTableList(UBound(SaleReturnTableList)).Line_Remark = ""
                        SaleReturnTableList(UBound(SaleReturnTableList)).Line_BaleNo = ""
                        SaleReturnTableList(UBound(SaleReturnTableList)).Line_LotNo = ""
                        SaleReturnTableList(UBound(SaleReturnTableList)).Line_ReferenceDocId = ""
                        SaleReturnTableList(UBound(SaleReturnTableList)).Line_SaleInvoice = ""
                        SaleReturnTableList(UBound(SaleReturnTableList)).Line_SaleInvoiceSr = ""
                        SaleReturnTableList(UBound(SaleReturnTableList)).Line_GrossWeight = 0
                        SaleReturnTableList(UBound(SaleReturnTableList)).Line_NetWeight = 0
                        SaleReturnTableList(UBound(SaleReturnTableList)).Line_Gross_Amount = SaleReturnTableList(UBound(SaleReturnTableList)).Line_Amount
                        SaleReturnTableList(UBound(SaleReturnTableList)).Line_Taxable_Amount = SaleReturnTableList(UBound(SaleReturnTableList)).Line_Amount
                        SaleReturnTableList(UBound(SaleReturnTableList)).Line_Tax1_Per = 0
                        SaleReturnTableList(UBound(SaleReturnTableList)).Line_Tax1 = SaleReturnTableList(UBound(SaleReturnTableList)).Line_Taxable_Amount * SaleReturnTableList(UBound(SaleReturnTableList)).Line_Tax1_Per / 100
                        SaleReturnTableList(UBound(SaleReturnTableList)).Line_Tax2_Per = 0
                        SaleReturnTableList(UBound(SaleReturnTableList)).Line_Tax2 = SaleReturnTableList(UBound(SaleReturnTableList)).Line_Taxable_Amount * SaleReturnTableList(UBound(SaleReturnTableList)).Line_Tax2_Per / 100
                        SaleReturnTableList(UBound(SaleReturnTableList)).Line_Tax3_Per = 0
                        SaleReturnTableList(UBound(SaleReturnTableList)).Line_Tax3 = SaleReturnTableList(UBound(SaleReturnTableList)).Line_Taxable_Amount * SaleReturnTableList(UBound(SaleReturnTableList)).Line_Tax3_Per / 100
                        SaleReturnTableList(UBound(SaleReturnTableList)).Line_Tax4_Per = 0
                        SaleReturnTableList(UBound(SaleReturnTableList)).Line_Tax4 = SaleReturnTableList(UBound(SaleReturnTableList)).Line_Taxable_Amount * SaleReturnTableList(UBound(SaleReturnTableList)).Line_Tax4_Per / 100
                        SaleReturnTableList(UBound(SaleReturnTableList)).Line_Tax5_Per = 0
                        SaleReturnTableList(UBound(SaleReturnTableList)).Line_Tax5 = SaleReturnTableList(UBound(SaleReturnTableList)).Line_Taxable_Amount * SaleReturnTableList(UBound(SaleReturnTableList)).Line_Tax5_Per / 100
                        SaleReturnTableList(UBound(SaleReturnTableList)).Line_SubTotal1 = SaleReturnTableList(UBound(SaleReturnTableList)).Line_Amount + SaleReturnTableList(UBound(SaleReturnTableList)).Line_Tax1 + SaleReturnTableList(UBound(SaleReturnTableList)).Line_Tax2 +
                                                                    SaleReturnTableList(UBound(SaleReturnTableList)).Line_Tax3 + SaleReturnTableList(UBound(SaleReturnTableList)).Line_Tax4 + SaleReturnTableList(UBound(SaleReturnTableList)).Line_Tax5
                        'For Header Values
                        Tot_Gross_Amount += SaleReturnTableList(UBound(SaleReturnTableList)).Line_Gross_Amount
                        Tot_Taxable_Amount += SaleReturnTableList(UBound(SaleReturnTableList)).Line_Taxable_Amount
                        Tot_Tax1 += SaleReturnTableList(UBound(SaleReturnTableList)).Line_Tax1
                        Tot_Tax2 += SaleReturnTableList(UBound(SaleReturnTableList)).Line_Tax2
                        Tot_Tax3 += SaleReturnTableList(UBound(SaleReturnTableList)).Line_Tax3
                        Tot_Tax4 += SaleReturnTableList(UBound(SaleReturnTableList)).Line_Tax4
                        Tot_Tax5 += SaleReturnTableList(UBound(SaleReturnTableList)).Line_Tax5
                        Tot_SubTotal1 += SaleReturnTableList(UBound(SaleReturnTableList)).Line_SubTotal1

                        'SaleInvoiceTableList(UBound(SaleInvoiceTableList)) = SaleInvoiceTable
                        ReDim Preserve SaleReturnTableList(UBound(SaleReturnTableList) + 1)
                    End If
#End Region

#Region "Freight Charge"
                    If Val(Dgl2.Item(Col2WFreight, I).Value) > 0 Then
                        SaleReturnTableList(UBound(SaleReturnTableList)).Line_Sr = UBound(SaleReturnTableList) + 1
                        SaleReturnTableList(UBound(SaleReturnTableList)).Line_ItemCode = ""
                        SaleReturnTableList(UBound(SaleReturnTableList)).Line_ItemName = ItemCode.Freight
                        SaleReturnTableList(UBound(SaleReturnTableList)).Line_Specification = ""
                        SaleReturnTableList(UBound(SaleReturnTableList)).Line_SalesTaxGroupItem = "GST 0%"
                        SaleReturnTableList(UBound(SaleReturnTableList)).Line_ReferenceNo = ""
                        SaleReturnTableList(UBound(SaleReturnTableList)).Line_DocQty = 1
                        SaleReturnTableList(UBound(SaleReturnTableList)).Line_FreeQty = 0
                        SaleReturnTableList(UBound(SaleReturnTableList)).Line_Qty = 1
                        SaleReturnTableList(UBound(SaleReturnTableList)).Line_Unit = "Nos"
                        SaleReturnTableList(UBound(SaleReturnTableList)).Line_Pcs = 0
                        SaleReturnTableList(UBound(SaleReturnTableList)).Line_UnitMultiplier = 0
                        SaleReturnTableList(UBound(SaleReturnTableList)).Line_DealUnit = ""
                        SaleReturnTableList(UBound(SaleReturnTableList)).Line_DocDealQty = ""
                        SaleReturnTableList(UBound(SaleReturnTableList)).Line_DiscountPer = 0
                        SaleReturnTableList(UBound(SaleReturnTableList)).Line_DiscountAmount = 0
                        SaleReturnTableList(UBound(SaleReturnTableList)).Line_AdditionalDiscountPer = 0
                        SaleReturnTableList(UBound(SaleReturnTableList)).Line_AdditionalDiscountAmount = 0
                        SaleReturnTableList(UBound(SaleReturnTableList)).Line_Amount = Val(Dgl2.Item(Col2WFreight, I).Value)
                        SaleReturnTableList(UBound(SaleReturnTableList)).Line_Rate = Val(Dgl2.Item(Col2WFreight, I).Value)
                        SaleReturnTableList(UBound(SaleReturnTableList)).Line_Remark = ""
                        SaleReturnTableList(UBound(SaleReturnTableList)).Line_BaleNo = ""
                        SaleReturnTableList(UBound(SaleReturnTableList)).Line_LotNo = ""
                        SaleReturnTableList(UBound(SaleReturnTableList)).Line_ReferenceDocId = ""
                        SaleReturnTableList(UBound(SaleReturnTableList)).Line_SaleInvoice = ""
                        SaleReturnTableList(UBound(SaleReturnTableList)).Line_SaleInvoiceSr = ""
                        SaleReturnTableList(UBound(SaleReturnTableList)).Line_GrossWeight = 0
                        SaleReturnTableList(UBound(SaleReturnTableList)).Line_NetWeight = 0
                        SaleReturnTableList(UBound(SaleReturnTableList)).Line_Gross_Amount = SaleReturnTableList(UBound(SaleReturnTableList)).Line_Amount
                        SaleReturnTableList(UBound(SaleReturnTableList)).Line_Taxable_Amount = SaleReturnTableList(UBound(SaleReturnTableList)).Line_Amount
                        SaleReturnTableList(UBound(SaleReturnTableList)).Line_Tax1_Per = 0
                        SaleReturnTableList(UBound(SaleReturnTableList)).Line_Tax1 = SaleReturnTableList(UBound(SaleReturnTableList)).Line_Taxable_Amount * SaleReturnTableList(UBound(SaleReturnTableList)).Line_Tax1_Per / 100
                        SaleReturnTableList(UBound(SaleReturnTableList)).Line_Tax2_Per = 0
                        SaleReturnTableList(UBound(SaleReturnTableList)).Line_Tax2 = SaleReturnTableList(UBound(SaleReturnTableList)).Line_Taxable_Amount * SaleReturnTableList(UBound(SaleReturnTableList)).Line_Tax2_Per / 100
                        SaleReturnTableList(UBound(SaleReturnTableList)).Line_Tax3_Per = 0
                        SaleReturnTableList(UBound(SaleReturnTableList)).Line_Tax3 = SaleReturnTableList(UBound(SaleReturnTableList)).Line_Taxable_Amount * SaleReturnTableList(UBound(SaleReturnTableList)).Line_Tax3_Per / 100
                        SaleReturnTableList(UBound(SaleReturnTableList)).Line_Tax4_Per = 0
                        SaleReturnTableList(UBound(SaleReturnTableList)).Line_Tax4 = SaleReturnTableList(UBound(SaleReturnTableList)).Line_Taxable_Amount * SaleReturnTableList(UBound(SaleReturnTableList)).Line_Tax4_Per / 100
                        SaleReturnTableList(UBound(SaleReturnTableList)).Line_Tax5_Per = 0
                        SaleReturnTableList(UBound(SaleReturnTableList)).Line_Tax5 = SaleReturnTableList(UBound(SaleReturnTableList)).Line_Taxable_Amount * SaleReturnTableList(UBound(SaleReturnTableList)).Line_Tax5_Per / 100
                        SaleReturnTableList(UBound(SaleReturnTableList)).Line_SubTotal1 = SaleReturnTableList(UBound(SaleReturnTableList)).Line_Taxable_Amount + SaleReturnTableList(UBound(SaleReturnTableList)).Line_Tax1 + SaleReturnTableList(UBound(SaleReturnTableList)).Line_Tax2 +
                                                                    SaleReturnTableList(UBound(SaleReturnTableList)).Line_Tax3 + SaleReturnTableList(UBound(SaleReturnTableList)).Line_Tax4 + SaleReturnTableList(UBound(SaleReturnTableList)).Line_Tax5
                        'For Header Values
                        Tot_Gross_Amount += SaleReturnTableList(UBound(SaleReturnTableList)).Line_Gross_Amount
                        Tot_Taxable_Amount += SaleReturnTableList(UBound(SaleReturnTableList)).Line_Taxable_Amount
                        Tot_Tax1 += SaleReturnTableList(UBound(SaleReturnTableList)).Line_Tax1
                        Tot_Tax2 += SaleReturnTableList(UBound(SaleReturnTableList)).Line_Tax2
                        Tot_Tax3 += SaleReturnTableList(UBound(SaleReturnTableList)).Line_Tax3
                        Tot_Tax4 += SaleReturnTableList(UBound(SaleReturnTableList)).Line_Tax4
                        Tot_Tax5 += SaleReturnTableList(UBound(SaleReturnTableList)).Line_Tax5
                        Tot_SubTotal1 += SaleReturnTableList(UBound(SaleReturnTableList)).Line_SubTotal1

                        'SaleInvoiceTableList(UBound(SaleInvoiceTableList)) = SaleInvoiceTable
                        ReDim Preserve SaleReturnTableList(UBound(SaleReturnTableList) + 1)
                    End If
#End Region
                End If
            Next

            SaleReturnTableList(0).Gross_Amount = Tot_Gross_Amount
            SaleReturnTableList(0).Taxable_Amount = Tot_Taxable_Amount
            SaleReturnTableList(0).Tax1 = Tot_Tax1
            SaleReturnTableList(0).Tax2 = Tot_Tax2
            SaleReturnTableList(0).Tax3 = Tot_Tax3
            SaleReturnTableList(0).Tax4 = Tot_Tax4
            SaleReturnTableList(0).Tax5 = Tot_Tax5
            SaleReturnTableList(0).SubTotal1 = Tot_SubTotal1
            SaleReturnTableList(0).Other_Charge = 0
            SaleReturnTableList(0).Deduction = 0
            SaleReturnTableList(0).Round_Off = Math.Round(Math.Round(SaleReturnTableList(0).SubTotal1) - SaleReturnTableList(0).SubTotal1, 2)
            SaleReturnTableList(0).Net_Amount = Math.Round(SaleReturnTableList(0).SubTotal1)



            Dim Tot_RoundOff As Double = 0
            Dim Tot_NetAmount As Double = 0
            For J As Integer = 0 To SaleReturnTableList.Length - 1
                If Val(SaleReturnTableList(0).Gross_Amount) > 0 Then
                    SaleReturnTableList(J).Line_Round_Off = Math.Round(SaleReturnTableList(0).Round_Off * SaleReturnTableList(J).Line_Gross_Amount / SaleReturnTableList(0).Gross_Amount, 2)
                    SaleReturnTableList(J).Line_Net_Amount = Math.Round(SaleReturnTableList(0).Net_Amount * SaleReturnTableList(J).Line_Gross_Amount / SaleReturnTableList(0).Gross_Amount, 2)
                End If
                Tot_RoundOff += SaleReturnTableList(J).Line_Round_Off
                Tot_NetAmount += SaleReturnTableList(J).Line_Net_Amount
            Next

            If Tot_RoundOff <> SaleReturnTableList(0).Round_Off Then
                SaleReturnTableList(0).Line_Round_Off = SaleReturnTableList(0).Line_Round_Off + (SaleReturnTableList(0).Round_Off - Tot_RoundOff)
            End If

            If Tot_NetAmount <> SaleReturnTableList(0).Net_Amount Then
                SaleReturnTableList(0).Line_Net_Amount = SaleReturnTableList(0).Line_Net_Amount + (SaleReturnTableList(0).Net_Amount - Tot_NetAmount)
            End If

            'If SaleReturnTableList(0).Net_Amount > 0 Then
            Dim bDocId As String = FrmSaleInvoiceDirect.InsertSaleInvoice(SaleReturnTableList)
            If AgL.XNull(bDocId) <> "" And (AgL.XNull(SaleReturnTableList(0).V_Type) = "SR" Or AgL.XNull(SaleReturnTableList(0).V_Type) = "WSR") Then
                Dgl2.Item(Col2WSaleReturnDocId, mRow).Value = bDocId

                mQry = " INSERT INTO SaleInvoiceGeneratedEntries(Code, Type, DocId, SaleOrderNo, SaleOrderDocId, Site_Code, Div_Code, V_Type) 
                            Select '" & mSearchCode & "' As Code, 'Sale Return', '" & bDocId & "', '" & TxtOrderNo.Text & "', 
                            '" & TxtSaleOrderDocId_W.Text & "', '" & AgL.PubSiteCode & "', '" & AgL.PubDivCode & "', '" & SaleReturnTableList(0).V_Type & "' "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)



                If AgL.XNull(Dgl2.Item(Col2SyncedSaleReturnDocId, mRow).Value) <> "" Then
                    mQry = "Delete From SaleInvoiceTransport Where DocId = '" & Dgl2.Item(Col2SyncedSaleReturnDocId, mRow).Value & "'"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    mQry = "Select * from SaleInvoiceTransport Where DocId =  '" & Dgl2.Item(Col2SaleReturnDocId, mRow).Value & "'"
                    dtTemp = AgL.FillData(mQry, Connection_Pakka).Tables(0)
                    If dtTemp.Rows.Count > 0 Then
                        mQry = "
                                    INSERT INTO SaleInvoiceTransport (DocID, Transporter, LrNo, LrDate, 
                                    PrivateMark, Weight, Freight, PaymentType, 
                                    RoadPermitNo, RoadPermitDate, VehicleNo, 
                                    ShipMethod, BookedFrom, BookedTo, Destination, 
                                    ChargedWeight, PreCarriageBy, PreCarriagePlace, 
                                    DescriptionOfGoods, DescriptionOfPacking, NoOfBales)
                                    Values( '" & Dgl2.Item(Col2SyncedSaleReturnDocId, mRow).Value & "', " & AgL.Chk_Text(FGetSubCodeFromOMSId(AgL.XNull(dtTemp.Rows(0)("Transporter")))) & ", " & AgL.Chk_Text(AgL.XNull(dtTemp.Rows(0)("LRNo"))) & ", " & AgL.Chk_Date(AgL.XNull(dtTemp.Rows(0)("LRDate"))) & ", 
                                    " & AgL.Chk_Text(AgL.XNull(dtTemp.Rows(0)("PrivateMark"))) & ", " & Val(AgL.XNull(dtTemp.Rows(0)("Weight"))) & ", " & Val(AgL.XNull(dtTemp.Rows(0)("Freight"))) & ", " & AgL.Chk_Text(AgL.XNull(dtTemp.Rows(0)("PaymentType"))) & ", 
                                    " & AgL.Chk_Text(AgL.XNull(dtTemp.Rows(0)("RoadPermitNo"))) & ", " & AgL.Chk_Date(AgL.XNull(dtTemp.Rows(0)("RoadPermitDate"))) & ", " & AgL.Chk_Text(AgL.XNull(dtTemp.Rows(0)("VehicleNo"))) & ", 
                                    " & AgL.Chk_Text(AgL.XNull(dtTemp.Rows(0)("ShipMethod"))) & ", " & AgL.Chk_Text(AgL.XNull(dtTemp.Rows(0)("BookedFrom"))) & ", " & AgL.Chk_Text(AgL.XNull(dtTemp.Rows(0)("BookedTo"))) & ", " & AgL.Chk_Text(AgL.XNull(dtTemp.Rows(0)("Destination"))) & ", 
                                    " & Val(AgL.XNull(dtTemp.Rows(0)("ChargedWeight"))) & ", " & AgL.Chk_Text(AgL.XNull(dtTemp.Rows(0)("PreCarriageBy"))) & ", " & AgL.Chk_Text(AgL.XNull(dtTemp.Rows(0)("PreCarriagePlace"))) & ", " & AgL.Chk_Text(AgL.XNull(dtTemp.Rows(0)("DescriptionOfGoods"))) & ", " & AgL.Chk_Text(AgL.XNull(dtTemp.Rows(0)("DescriptionOfPacking"))) & ", " & Val(AgL.XNull(dtTemp.Rows(0)("NoOfBales"))) & ")
                                    "
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    End If




                    mQry = " INSERT INTO SaleInvoiceGeneratedEntries(Code, Type, DocId, SaleOrderNo, SaleOrderDocId, Site_Code, Div_Code, V_Type) 
                                    Select '" & mSearchCode & "' As Code, 'Sale Return', '" & Dgl2.Item(Col2SyncedSaleReturnDocId, mRow).Value & "', '" & TxtOrderNo.Text & "', 
                                    '" & TxtSaleOrderDocId_W.Text & "', '" & AgL.PubSiteCode & "', '" & AgL.PubDivCode & "', 'SI' "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                    mQry = " UPDATE SaleInvoice Set GenDocId = '" & Dgl2.Item(Col2SyncedSaleReturnDocId, mRow).Value & "'
                            Where DocId = '" & bDocId & "'"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                    mQry = "Insert Into TransactionReferences (DocID, ReferenceDocID, IsEditingAllowed, IsDeletingAllowed) 
                                    Values (" & AgL.Chk_Text(Dgl2.Item(Col2SyncedSaleReturnDocId, mRow).Value) & ", '" & bDocId & "', 1, 0) "
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                Else
                    If BtnTransportDetail.Tag IsNot Nothing Then
                        CType(BtnTransportDetail.Tag, FrmSaleInvoiceTransport).FSave(bDocId, Conn, Cmd)
                    End If


                    mQry = "Insert Into TransactionReferences (DocID, ReferenceDocID, IsEditingAllowed, IsDeletingAllowed) 
                                    Values (" & AgL.Chk_Text(bDocId) & ", '" & bDocId & "', 1, 0) "
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                End If
            Else
                If AgL.XNull(Dgl2.Item(Col2SyncedSaleReturnDocId, mRow).Value) <> "" Then
                    mQry = " INSERT INTO SaleInvoiceGeneratedEntries(Code, Type, DocId, SaleOrderNo, SaleOrderDocId, Site_Code, Div_Code, V_Type) 
                                    Select '" & mSearchCode & "' As Code, 'Sale Return', '" & Dgl2.Item(Col2SyncedSaleReturnDocId, mRow).Value & "', '" & TxtOrderNo.Text & "', 
                                    '" & TxtSaleOrderDocId_W.Text & "', '" & AgL.PubSiteCode & "', '" & AgL.PubDivCode & "', 'SI' "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                End If
            End If
        End If
    End Sub
    Private Sub BtnOk_Click(sender As Object, e As EventArgs) Handles BtnOk.Click

        FProcSave()


    End Sub

    Private Sub FProcSave()
        Dim mTrans As String = ""
        If FDataValidation() = False Then Exit Sub
        Try
            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = "Begin"

            mSearchCode = AgL.GetMaxId("SaleInvoiceGeneratedEntries", "Code", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 4, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)

            'FSyncSaleInvoices(AgL.GCn, AgL.ECmd)
            'FSyncPurchaseInvoices(AgL.GCn, AgL.ECmd)

            FPostPurchaseData_ForDifference(AgL.GCn, AgL.ECmd)
            FPostSaleData_ForDifference(AgL.GCn, AgL.ECmd)
            FPostDebitCreditNoteData_ForDifference(AgL.GCn, AgL.ECmd, "CNS")

            FPostUIValues(mSearchCode, AgL.GCn, AgL.ECmd)

            'mQry = "Insert Into TransactionReferences (DocID, ReferenceDocID, IsEditingAllowed, IsDeletingAllowed) 
            '        Values (" & AgL.Chk_Text(mSearchCode) & ", '" & bDocId & "', 1, 1) "
            'AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

            mQry = " UPDATE SaleInvoiceGeneratedEntries Set TransactionType = 'Return'
                        Where Code = '" & mSearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)


            AgL.ETrans.Commit()
            mTrans = "Commit"

            If MsgBox("Do you want to print?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton1) = MsgBoxResult.Yes Then
                Dim dtTemp As DataTable
                mQry = "Select DocID From SaleInvoiceGeneratedEntries Where Code = '" & mSearchCode & "' And V_Type = 'SI' "
                dtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
                If dtTemp.Rows.Count > 0 Then
                    'FrmSaleInvoiceDirect.FGetPrintCrystal_Aadhat(Me, AgL.XNull(dtTemp.Rows(0)("DocID")), PrintFor.DocumentPrint, False, "", "")
                End If

                FGetPrintCrystal(PrintFor.DocumentPrint, False, "")
            Else
                MsgBox("Entry Saved Successfully...", MsgBoxStyle.Information)
            End If


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
        TxtSaleOrderDocId_W.Tag = "" : TxtSaleOrderDocId_W.Text = ""
        TxtPartyName.Tag = "" : TxtPartyName.Text = ""
        TxtRemark.Tag = "" : TxtRemark.Text = ""
        mSearchCode = ""
    End Sub

    Private Sub Calculation()
        Dgl3.Rows.Clear()



        For J As Integer = 0 To Dgl2.Rows.Count - 1
            Dgl2.Item(Col2WSaleReturnAmount, J).Tag = "0"
            Dgl2.Item(Col2WDiscount, J).Tag = "0"
        Next

        For I As Integer = 0 To Dgl1.Rows.Count - 1
            If Dgl1.Item(Col1WReturnDate, I).Value <> "" Then
                Dgl1.Item(Col1WPurchReturnAmount, I).Value = Val(Dgl1.Item(Col1WAmount, I).Value) -
                Val(Dgl1.Item(Col1AmountWithoutDiscountAndTax, I).Value)


                For J As Integer = 0 To Dgl2.Rows.Count - 1
                    If Dgl1.Item(Col1SaleReturnDocId, I).Value = Dgl2.Item(Col2SaleReturnDocId, J).Value And
                            Dgl1.Item(Col1ItemGroup, I).Value = Dgl2.Item(Col2ItemGroup, J).Value Then

                        Dgl2.Item(Col2WSaleReturnAmount, J).Value = Val(Dgl2.Item(Col2WSaleReturnAmount, J).Tag) + Val(Dgl1.Item(Col1WAmount, I).Value) +
                                (Val(Dgl1.Item(Col1WAmount, I).Value) * Val(Dgl2.Item(Col2AdditionPer, J).Value) / 100) -
                                (Val(Dgl1.Item(Col1WAmount, I).Value) * Val(Dgl2.Item(Col2AdditionalDiscountPer, J).Value) / 100) -
                                (Val(Dgl1.Item(Col1WQty, I).Value) * Val(Dgl2.Item(Col2DiscountPer, J).Value)) -
                                (Val(Dgl2.Item(Col2AmountWithoutTax, J).Value))

                        'Dgl2.Item(Col2WSaleReturnAmount, J).Tag = Val(Dgl1.Item(Col1WAmount, I).Value) +
                        '        (Val(Dgl1.Item(Col1WAmount, I).Value) * Val(Dgl2.Item(Col2AdditionPer, J).Value) / 100) -
                        '        (Val(Dgl1.Item(Col1WAmount, I).Value) * Val(Dgl2.Item(Col2AdditionalDiscountPer, J).Value) / 100) -
                        '        (Val(Dgl1.Item(Col1WQty, I).Value) * Val(Dgl2.Item(Col2DiscountPer, J).Value))

                        Dgl2.Item(Col2WDiscount, J).Value = Val(Dgl2.Item(Col2WDiscount, J).Tag) +
                                -(Val(Dgl1.Item(Col1WAmount, I).Value) * Val(Dgl2.Item(Col2AdditionPer, J).Value) / 100) +
                                (Val(Dgl1.Item(Col1WAmount, I).Value) * Val(Dgl2.Item(Col2AdditionalDiscountPer, J).Value) / 100) -
                                (Val(Dgl1.Item(Col1WQty, I).Value) * Val(Dgl2.Item(Col2DiscountPer, J).Value)) -
                                (Val(Dgl2.Item(Col2Discount, J).Value))

                        'Dgl2.Item(Col2WDiscount, J).Tag = -(Val(Dgl1.Item(Col1WAmount, I).Value) * Val(Dgl2.Item(Col2AdditionPer, J).Value) / 100) +
                        '        (Val(Dgl1.Item(Col1WAmount, I).Value) * Val(Dgl2.Item(Col2AdditionalDiscountPer, J).Value) / 100) -
                        '        (Val(Dgl1.Item(Col1WQty, I).Value) * Val(Dgl2.Item(Col2DiscountPer, J).Value))



                        If AgL.XNull(Dgl2.Item(Col2SyncedSaleReturnDocId, J).Value) <> "" Then
                            If AgL.XNull(AgL.Dman_Execute("Select Structure  From SaleInvoice Where DocId = '" & Dgl2.Item(Col2SyncedSaleReturnDocId, J).Value & "'", AgL.GCn).ExecuteScalar()) = "GstSaleMrp" Then
                                Dgl2.Item(Col2WSaleReturnAmount, J).Value = 0
                            End If
                        End If
                    End If
                Next

                Dim bAmountDiffDebitNote As Double = 0
                bAmountDiffDebitNote = Math.Round(Dgl1.Item(Col1WPurchReturnAmount, I).Value *
                        Val(Dgl1.Item(Col1AdditionalDiscountPer, I).Value) / 100, 2)

                bAmountDiffDebitNote = bAmountDiffDebitNote + (Math.Round(Val(Dgl1.Item(Col1WQty, I).Value) *
                        Val(Dgl1.Item(Col1DiscountPer, I).Value)))

                If bAmountDiffDebitNote > 0 Then
                    Dgl3.Rows.Add()
                    Dgl3.Item(ColSNo, Dgl3.Rows.Count - 1).Value = Dgl3.Rows.Count
                    Dgl3.Item(Col3DrCr, Dgl3.Rows.Count - 1).Value = "Credit Note"
                    Dgl3.Item(Col3V_Date, Dgl3.Rows.Count - 1).Value = Dgl1.Item(Col1WReturnDate, I).Value
                    Dgl3.Item(Col3Party, Dgl3.Rows.Count - 1).Tag = Dgl1.Item(Col1MasterSupplier, I).Tag
                    Dgl3.Item(Col3Party, Dgl3.Rows.Count - 1).Value = Dgl1.Item(Col1MasterSupplier, I).Value
                    Dgl3.Item(Col3ReasonAc, Dgl3.Rows.Count - 1).Value = "Rate Diff A/c"
                    Dgl3.Item(Col3SyncedPurchReturnDocId, Dgl3.Rows.Count - 1).Value = Dgl1.Item(Col1SyncedPurchReturnDocId, I).Value
                    Dgl3.Item(Col3Amount, Dgl3.Rows.Count - 1).Value = bAmountDiffDebitNote
                    Dgl3.Item(Col3Remark, Dgl3.Rows.Count - 1).Value = "Credit Note due to Amount Differnece In Kachha and Pakka Invoice. Pakka Purchase Return No " & Dgl1.Item(Col1ReturnNo, I).Value & " And Kachha Purchase Return No." & Dgl1.Item(Col1WReturnNo, I).Value & "."
                End If


                Dim bDiscountDiffDebitNote As Double = 0

                If Dgl1.Item(Col1ReturnAdditionalDiscountPer, I).Value < Dgl1.Item(Col1AdditionalDiscountPer, I).Value Then
                    bDiscountDiffDebitNote = Math.Round(Val(Dgl1.Item(Col1Amount, I).Value) *
                    (Val(Dgl1.Item(Col1AdditionalDiscountPer, I).Value) - Val(Dgl1.Item(Col1ReturnAdditionalDiscountPer, I).Value)) / 100, 2)
                End If

                If Dgl1.Item(Col1ReturnDiscountPer, I).Value < Dgl1.Item(Col1DiscountPer, I).Value Then
                    bDiscountDiffDebitNote = bDiscountDiffDebitNote + (Math.Round(Val(Dgl1.Item(Col1Amount, I).Value) *
                    (Val(Dgl1.Item(Col1DiscountPer, I).Value) - Val(Dgl1.Item(Col1ReturnDiscountPer, I).Value)) / 100, 2))
                End If



                If bDiscountDiffDebitNote > 0 Then
                    Dgl3.Rows.Add()
                    Dgl3.Item(ColSNo, Dgl3.Rows.Count - 1).Value = Dgl3.Rows.Count
                    Dgl3.Item(Col3DrCr, Dgl3.Rows.Count - 1).Value = "Credit Note"
                    Dgl3.Item(Col3V_Date, Dgl3.Rows.Count - 1).Value = Dgl1.Item(Col1WReturnDate, I).Value
                    Dgl3.Item(Col3Party, Dgl3.Rows.Count - 1).Tag = Dgl1.Item(Col1MasterSupplier, I).Tag
                    Dgl3.Item(Col3Party, Dgl3.Rows.Count - 1).Value = Dgl1.Item(Col1MasterSupplier, I).Value
                    Dgl3.Item(Col3ReasonAc, Dgl3.Rows.Count - 1).Value = "Discount Diff A/c"
                    Dgl3.Item(Col3SyncedPurchReturnDocId, Dgl3.Rows.Count - 1).Value = Dgl1.Item(Col1SyncedPurchReturnDocId, I).Value
                    Dgl3.Item(Col3Amount, Dgl3.Rows.Count - 1).Value = bDiscountDiffDebitNote
                    Dgl3.Item(Col3Remark, Dgl3.Rows.Count - 1).Value = "Credit Note due to Discount Differnece In Kachha and Pakka Invoice."
                End If
            End If

            If AgL.XNull(Dgl1.Item(Col1SyncedPurchReturnDocId, I).Value) <> "" Then
                If AgL.XNull(AgL.Dman_Execute("Select Structure  From PurchInvoice Where DocId = '" & Dgl1.Item(Col1SyncedPurchReturnDocId, I).Value & "'", AgL.GCn).ExecuteScalar()) = "GstPurMrp" Then
                    Dgl1.Item(Col1WPurchReturnAmount, I).Value = 0
                End If
            End If
        Next
    End Sub
    Private Sub Dgl1_EditingControl_Validating(sender As Object, e As CancelEventArgs) Handles Dgl1.EditingControl_Validating




        Calculation()
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

                Dim VoucherEntryTableList(0) As FrmVoucherEntry.StructLedgerHead
                Dim VoucherEntryTable As New FrmVoucherEntry.StructLedgerHead

                VoucherEntryTable.DocID = ""
                VoucherEntryTable.V_Type = V_Type
                VoucherEntryTable.V_Prefix = ""
                VoucherEntryTable.Site_Code = AgL.PubSiteCode
                VoucherEntryTable.Div_Code = AgL.PubDivCode
                VoucherEntryTable.V_No = 0
                VoucherEntryTable.V_Date = Dgl3.Item(Col3V_Date, I).Value
                VoucherEntryTable.ManualRefNo = ""
                VoucherEntryTable.Subcode = Dgl3.Item(Col3Party, I).Tag
                VoucherEntryTable.SubcodeName = Dgl3.Item(Col3Party, I).Value

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
                VoucherEntryTable.LockText = "Genereded From Sale Return W Entry.Can't Edit."

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
                Dim bDocId As String = FrmVoucherEntry.InsertLedgerHead(VoucherEntryTableList)
                If AgL.XNull(bDocId) <> "" Then
                    mQry = " INSERT INTO SaleInvoiceGeneratedEntries(Code, Type, DocId, SaleOrderNo, SaleOrderDocId, Site_Code, Div_Code, V_Type) 
                            Select '" & mSearchCode & "' As Code, 'Credit Note', '" & bDocId & "', '" & TxtOrderNo.Text & "', 
                            '" & TxtSaleOrderDocId_W.Text & "', '" & AgL.PubSiteCode & "', '" & AgL.PubDivCode & "','" & VoucherEntryTableList(0).V_Type & "' "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                    If AgL.XNull(Dgl3.Item(Col3SyncedPurchReturnDocId, I).Value) <> "" Then
                        mQry = "Insert Into TransactionReferences (DocID, ReferenceDocID, IsEditingAllowed, IsDeletingAllowed) 
                            Values (" & AgL.Chk_Text(Dgl3.Item(Col3SyncedPurchReturnDocId, I).Value) & ", '" & bDocId & "', 1, 0) "
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                    Else
                        mQry = "Insert Into TransactionReferences (DocID, ReferenceDocID, IsEditingAllowed, IsDeletingAllowed) 
                            Values (" & AgL.Chk_Text(bDocId) & ", '" & bDocId & "', 1, 0) "
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                    End If
                End If
            End If
        Next
    End Sub
    Private Sub Find()
        'mQry = "Select Distinct Code, SaleOrderNo From SaleInvoiceGeneratedEntries 
        '            Where Site_Code = '" & AgL.PubSiteCode & "' 
        '            And Div_Code = '" & AgL.PubDivCode & "'"

        mQry = " SELECT Ge.Code As SearchCode, Max(So.ManualRefNo) AS SaleOrderNo,  
                    Max(CASE WHEN Si.V_Type = 'SR' THEN Si.ManualRefNo ELSE NULL END) AS PakkaSaleInvoiceNo,
                    Max(CASE WHEN Pi.V_Type = 'PR' THEN Pi.VendorDocNo ELSE NULL END) AS PakkaPurchaseInvoiceNo,
                    Max(CASE WHEN Si.V_Type = 'WSR' THEN Si.ManualRefNo ELSE NULL END) AS KachhaSaleInvoiceNo,
                    Max(CASE WHEN Pi.V_Type = 'WPR' THEN Pi.VendorDocNo ELSE NULL END) AS KachhaPurchaseInvoiceNo,
                    Max(CASE WHEN Lh.V_Type = 'CNS' THEN Lh.ManualRefNo ELSE NULL END) AS KachhaPurchaseCreditNoteNo
                    FROM SaleInvoiceGeneratedEntries Ge 
                    LEFT JOIN SaleInvoice Si ON Ge.DocId = Si.DocID
                    LEFT JOIN PurchInvoice Pi ON Ge.DocId = Pi.DocID
                    LEFT JOIN LedgerHead Lh ON Ge.DocId = Lh.DocID
                    LEFT JOIN SaleOrder So On Ge.SaleOrderDocId = So.DocId
                    Where Ge.Site_Code = '" & AgL.PubSiteCode & "' 
                    And Ge.Div_Code = '" & AgL.PubDivCode & "'
                    And IsNull(Ge.TransactionType,'Invoice') = 'Return'
                    GROUP BY Ge.Code "

        Dim Frmbj As AgLibrary.FrmFind = New AgLibrary.FrmFind(mQry, Me.Text & " Find", AgL)
        Frmbj.ShowDialog()
        AgL.PubSearchRow = AgL.XNull(Frmbj.DGL1.Item(0, Frmbj.DGL1.CurrentRow.Index).Value)
        If AgL.PubSearchRow <> "" Then
            mSearchCode = AgL.PubSearchRow
            MoveRec()
            If Dgl1.Rows.Count = 0 And Dgl2.Rows.Count = 0 Then
                MoveRecForUnSavedData()
            End If
        End If
    End Sub

    'Private Sub MoveRec()
    '    Dgl1.Rows.Clear()
    '    Dgl2.Rows.Clear()
    '    Dgl3.Rows.Clear()
    Private Sub MoveRecForUnSavedData()
        Dgl1.Rows.Clear()
        Dgl2.Rows.Clear()
        Dgl3.Rows.Clear()

        mQry = " Select * From SaleInvoiceGeneratedEntries Where Code = '" & mSearchCode & "' "
        Dim DtTemp As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

        TxtOrderNo.Text = AgL.XNull(DtTemp.Rows(0)("SaleOrderNo"))

        For I As Integer = 0 To DtTemp.Rows.Count - 1
            If AgL.XNull(DtTemp.Rows(I)("Type")) = "Sale Return" Then
                mQry = " Select Si.ManualRefNo As PakkaInvoiceNo, Si.V_Date As PakkaInvoiceDate, H.* 
                    From SaleInvoice H
                    LEFT JOIN SaleInvoice Si On H.GenDocId = Si.DocId
                    Where H.DocId = '" & AgL.XNull(DtTemp.Rows(I)("DocId")) & "' 
                    And H.V_Type = 'WSI'"
                Dim DtTransaction As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
                For J As Integer = 0 To DtTransaction.Rows.Count - 1
                    Dgl2.Rows.Add()
                    Dgl2.Item(ColSNo, Dgl2.Rows.Count - 1).Value = Dgl2.Rows.Count
                    Dgl2.Item(Col2WReturnNo, Dgl2.Rows.Count - 1).Value = AgL.XNull(DtTransaction.Rows(J)("ManualRefNo"))
                    Dgl2.Item(Col2WReturnDate, Dgl2.Rows.Count - 1).Value = AgL.XNull(DtTransaction.Rows(J)("V_Date"))
                    Dgl2.Item(Col2WSaleReturnAmount, Dgl2.Rows.Count - 1).Value = AgL.XNull(DtTransaction.Rows(J)("Net_Amount"))

                    Dgl2.Item(Col2ReturnNo, Dgl2.Rows.Count - 1).Value = AgL.XNull(DtTransaction.Rows(J)("PakkaInvoiceNo"))
                    Dgl2.Item(Col2ReturnDate, Dgl2.Rows.Count - 1).Value = AgL.XNull(DtTransaction.Rows(J)("PakkaInvoiceDate"))
                Next
            ElseIf AgL.XNull(DtTemp.Rows(I)("Type")) = "Purchase Return" Then
                mQry = " Select Pi.ManualRefNo As PakkaInvoiceNo, Pi.V_Date As PakkaInvoiceDate, H.* 
                        From PurchInvoice H
                        LEFT JOIN PurchInvoice Pi On H.GenDocId = Pi.DocId
                        Where H.DocId = '" & AgL.XNull(DtTemp.Rows(I)("DocId")) & "' 
                        And H.V_Type = 'WPI'"
                Dim DtTransaction As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
                For J As Integer = 0 To DtTransaction.Rows.Count - 1
                    Dgl1.Rows.Add()
                    Dgl1.Item(ColSNo, Dgl1.Rows.Count - 1).Value = Dgl1.Rows.Count
                    Dgl1.Item(Col1WReturnNo, Dgl1.Rows.Count - 1).Value = AgL.XNull(DtTransaction.Rows(J)("VendorDocNo"))
                    Dgl1.Item(Col1WReturnDate, Dgl1.Rows.Count - 1).Value = AgL.XNull(DtTransaction.Rows(J)("V_Date"))
                    Dgl1.Item(Col1WPurchReturnAmount, Dgl1.Rows.Count - 1).Value = AgL.XNull(DtTransaction.Rows(J)("Net_Amount"))

                    Dgl1.Item(Col1ReturnNo, Dgl1.Rows.Count - 1).Value = AgL.XNull(DtTransaction.Rows(J)("PakkaInvoiceNo"))
                    Dgl1.Item(Col1ReturnDate, Dgl1.Rows.Count - 1).Value = AgL.XNull(DtTransaction.Rows(J)("PakkaInvoiceDate"))
                Next
            ElseIf AgL.XNull(DtTemp.Rows(I)("Type")) = "Credit Note" Then
                mQry = " Select H.V_Date, Sg.Name As PartyName, H.ManualRefNo, Hc.Net_Amount 
                        From LedgerHead H 
                        LEFT JOIN LedgerHeadCharges Hc On H.DocId = Hc.DocId
                        LEFT JOIN SubGroup Sg On H.SubCode = Sg.SubCode
                        Where H.DocId = '" & AgL.XNull(DtTemp.Rows(I)("DocId")) & "' "
                Dim DtTransaction As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
                For J As Integer = 0 To DtTransaction.Rows.Count - 1
                    Dgl3.Rows.Add()
                    Dgl3.Item(ColSNo, Dgl3.Rows.Count - 1).Value = Dgl3.Rows.Count
                    Dgl3.Item(Col3DrCr, Dgl3.Rows.Count - 1).Value = "Credit Note"
                    Dgl3.Item(Col3Remark, Dgl3.Rows.Count - 1).Value = AgL.XNull(DtTransaction.Rows(J)("ManualRefNo"))
                    Dgl3.Item(Col3V_Date, Dgl3.Rows.Count - 1).Value = AgL.XNull(DtTransaction.Rows(J)("V_Date"))
                    Dgl3.Item(Col3Party, Dgl3.Rows.Count - 1).Value = AgL.XNull(DtTransaction.Rows(J)("PartyName"))
                    Dgl3.Item(Col3Amount, Dgl3.Rows.Count - 1).Value = Math.Abs(AgL.VNull(DtTransaction.Rows(J)("Net_Amount")))
                Next
            End If
        Next

        Dgl1.ReadOnly = True
        Dgl2.ReadOnly = True
        Dgl3.ReadOnly = True
        TxtOrderNo.Enabled = False
        BtnOk.Enabled = False
    End Sub

    Private Sub MoveRec()
        Dgl1.Rows.Clear()
        Dgl2.Rows.Clear()
        Dgl3.Rows.Clear()

        mQry = " Select So.ManualRefNo As SaleOrderManualRefNo, Sg.Name As SaleToPartyName, L.* 
                From SaleInvoiceGeneratedEntries L
                LEFT JOIN SaleOrder So On L.SaleOrderDocId = So.DocId
                LEFT JOIN SubGroup Sg On So.SaleToParty = Sg.SubCode
                Where Code = '" & mSearchCode & "' "
        Dim DtTemp As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

        TxtOrderNo.Text = AgL.XNull(DtTemp.Rows(0)("SaleOrderManualRefNo"))
        TxtPartyName.Text = AgL.XNull(DtTemp.Rows(0)("SaleToPartyName"))
        LblApproveBy.Text = AgL.XNull(DtTemp.Rows(0)("ApproveBy")) & IIf(AgL.XNull(DtTemp.Rows(0)("ApproveBy")) <> "", "  " & AgL.XNull(DtTemp.Rows(0)("ApproveDate")), "")
        If LblApproveBy.Text <> "" Then
            BtnApprove.Visible = False
        Else
            BtnApprove.Visible = True
        End If

        mQry = "SELECT SG.Name AS SupplierName, MSg.Name AS MasterSupplierName, Ig.Description AS ItemGroupDesc, L.* 
                FROM WPurchInvoiceDetail L 
                LEFT JOIN Subgroup SG ON L.Supplier = Sg.Subcode
                LEFT JOIN Subgroup MSg ON L.MasterSupplier = Msg.Subcode
                LEFT JOIN ItemGroup Ig ON L.ItemGroup = Ig.Code
                Where L.Code = '" & mSearchCode & "'"
        Dim DtPurchInvoiceDetail As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

        Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
        For I As Integer = 0 To DtPurchInvoiceDetail.Rows.Count - 1
            Dgl1.Rows.Add()
            Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count
            Dgl1.Item(Col1SyncedPurchReturnDocId, I).Value = AgL.XNull(DtPurchInvoiceDetail.Rows(I)("SyncedPurchInvoiceDocId"))
            Dgl1.Item(Col1Supplier, I).Tag = AgL.XNull(DtPurchInvoiceDetail.Rows(I)("Supplier"))
            Dgl1.Item(Col1Supplier, I).Value = AgL.XNull(DtPurchInvoiceDetail.Rows(I)("SupplierName"))
            Dgl1.Item(Col1ReturnNo, I).Value = AgL.XNull(DtPurchInvoiceDetail.Rows(I)("InvoiceNo"))
            Dgl1.Item(Col1ReturnDate, I).Value = AgL.XNull(DtPurchInvoiceDetail.Rows(I)("InvoiceDate"))
            Dgl1.Item(Col1ItemGroup, I).Tag = AgL.XNull(DtPurchInvoiceDetail.Rows(I)("ItemGroup"))
            Dgl1.Item(Col1ItemGroup, I).Value = AgL.XNull(DtPurchInvoiceDetail.Rows(I)("ItemGroupDesc"))
            Dgl1.Item(Col1ReturnDiscountPer, I).Value = AgL.VNull(DtPurchInvoiceDetail.Rows(I)("InvoiceDiscountPer"))
            Dgl1.Item(Col1ReturnAdditionalDiscountPer, I).Value = AgL.VNull(DtPurchInvoiceDetail.Rows(I)("InvoiceAdditionalDiscountPer"))
            Dgl1.Item(Col1DiscountPer, I).Value = AgL.VNull(DtPurchInvoiceDetail.Rows(I)("DiscountPer"))
            Dgl1.Item(Col1AdditionalDiscountPer, I).Value = AgL.VNull(DtPurchInvoiceDetail.Rows(I)("AdditionalDiscountPer"))
            Dgl1.Item(Col1AdditionPer, I).Value = AgL.VNull(DtPurchInvoiceDetail.Rows(I)("AdditionPer"))
            Dgl1.Item(Col1Amount, I).Value = AgL.VNull(DtPurchInvoiceDetail.Rows(I)("Amount"))
            Dgl1.Item(Col1AmountWithoutDiscountAndTax, I).Value = AgL.VNull(DtPurchInvoiceDetail.Rows(I)("AmountWithoutDiscountAndTax"))
            Dgl1.Item(Col1MasterSupplier, I).Value = AgL.XNull(DtPurchInvoiceDetail.Rows(I)("MasterSupplier"))
            Dgl1.Item(Col1WReturnNo, I).Value = AgL.XNull(DtPurchInvoiceDetail.Rows(I)("WInvoiceNo"))
            Dgl1.Item(Col1WReturnDate, I).Value = AgL.XNull(DtPurchInvoiceDetail.Rows(I)("WInvoiceDate"))
            Dgl1.Item(Col1WQty, I).Value = AgL.VNull(DtPurchInvoiceDetail.Rows(I)("WQty"))
            Dgl1.Item(Col1WFreight, I).Value = AgL.VNull(DtPurchInvoiceDetail.Rows(I)("WFreight"))
            Dgl1.Item(Col1WPacking, I).Value = AgL.VNull(DtPurchInvoiceDetail.Rows(I)("WPacking"))
            Dgl1.Item(Col1WAmount, I).Value = AgL.VNull(DtPurchInvoiceDetail.Rows(I)("WAmount"))
            Dgl1.Item(Col1WPurchReturnAmount, I).Value = AgL.VNull(DtPurchInvoiceDetail.Rows(I)("WPurchInvoiceAmount"))
            Dgl1.Item(Col1WPurchReturnDocId, I).Value = AgL.XNull(DtPurchInvoiceDetail.Rows(I)("WPurchInvoiceDocId"))
        Next

        mQry = "SELECT SG.Name AS PartyName, MSg.Name AS MasterPartyName, Ig.Description AS ItemGroupDesc, L.* 
                FROM WSaleInvoiceDetail L 
                LEFT JOIN Subgroup SG ON L.Party = Sg.Subcode
                LEFT JOIN Subgroup MSg ON L.MasterParty = Msg.Subcode
                LEFT JOIN ItemGroup Ig ON L.ItemGroup = Ig.Code
                Where L.Code = '" & mSearchCode & "'"
        Dim DtSaleInvoiceDetail As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

        Dgl2.RowCount = 1 : Dgl2.Rows.Clear()
        For I As Integer = 0 To DtSaleInvoiceDetail.Rows.Count - 1
            Dgl2.Rows.Add()
            Dgl2.Item(ColSNo, I).Value = Dgl2.Rows.Count
            Dgl2.Item(Col2SyncedSaleReturnDocId, I).Value = AgL.XNull(DtSaleInvoiceDetail.Rows(I)("SyncedSaleInvoiceDocId"))
            Dgl2.Item(Col2Party, I).Tag = AgL.XNull(DtSaleInvoiceDetail.Rows(I)("Party"))
            Dgl2.Item(Col2Party, I).Value = AgL.XNull(DtSaleInvoiceDetail.Rows(I)("PartyName"))
            Dgl2.Item(Col2ReturnNo, I).Value = AgL.XNull(DtSaleInvoiceDetail.Rows(I)("InvoiceNo"))
            Dgl2.Item(Col2ReturnDate, I).Value = AgL.XNull(DtSaleInvoiceDetail.Rows(I)("InvoiceDate"))
            Dgl2.Item(Col2ItemGroup, I).Tag = AgL.XNull(DtSaleInvoiceDetail.Rows(I)("ItemGroup"))
            Dgl2.Item(Col2ItemGroup, I).Value = AgL.XNull(DtSaleInvoiceDetail.Rows(I)("ItemGroupDesc"))
            Dgl2.Item(Col2DiscountPer, I).Value = AgL.VNull(DtSaleInvoiceDetail.Rows(I)("DiscountPer"))
            Dgl2.Item(Col2AdditionalDiscountPer, I).Value = AgL.VNull(DtSaleInvoiceDetail.Rows(I)("AdditionalDiscountPer"))
            Dgl2.Item(Col2AdditionPer, I).Value = AgL.VNull(DtSaleInvoiceDetail.Rows(I)("AdditionPer"))
            Dgl2.Item(Col2Amount, I).Value = AgL.VNull(DtSaleInvoiceDetail.Rows(I)("Amount"))
            Dgl2.Item(Col2AmountWithoutTax, I).Value = AgL.VNull(DtSaleInvoiceDetail.Rows(I)("AmountWithoutTax"))
            Dgl2.Item(Col2Discount, I).Value = AgL.VNull(DtSaleInvoiceDetail.Rows(I)("Discount"))
            Dgl2.Item(Col2MasterParty, I).Tag = AgL.XNull(DtSaleInvoiceDetail.Rows(I)("MasterParty"))
            Dgl2.Item(Col2MasterParty, I).Value = AgL.XNull(DtSaleInvoiceDetail.Rows(I)("MasterPartyName"))
            Dgl2.Item(Col2WSaleOrderDocId, I).Value = AgL.XNull(DtSaleInvoiceDetail.Rows(I)("WSaleOrderDocId"))
            Dgl2.Item(Col2WReturnNo, I).Value = AgL.XNull(DtSaleInvoiceDetail.Rows(I)("WInvoiceNo"))
            Dgl2.Item(Col2WReturnDate, I).Value = AgL.XNull(DtSaleInvoiceDetail.Rows(I)("WInvoiceDate"))
            Dgl2.Item(Col2WQty, I).Value = AgL.VNull(DtSaleInvoiceDetail.Rows(I)("WQty"))
            Dgl2.Item(Col2WFreight, I).Value = AgL.VNull(DtSaleInvoiceDetail.Rows(I)("WFreight"))
            Dgl2.Item(Col2WPacking, I).Value = AgL.VNull(DtSaleInvoiceDetail.Rows(I)("WPacking"))
            Dgl2.Item(Col2WDiscount, I).Value = AgL.VNull(DtSaleInvoiceDetail.Rows(I)("WDiscount"))
            Dgl2.Item(Col2WSaleReturnAmount, I).Value = AgL.VNull(DtSaleInvoiceDetail.Rows(I)("WSaleInvoiceAmount"))
            Dgl2.Item(Col2WSaleReturnDocId, I).Value = AgL.XNull(DtSaleInvoiceDetail.Rows(I)("WSaleInvoiceDocId"))
        Next

        mQry = " SELECT SG.Name AS PartyName, L.* 
                FROM WLedgerHeadDetail L 
                LEFT JOIN Subgroup SG ON L.Party = Sg.Subcode
                Where L.Code = '" & mSearchCode & "'"
        Dim DtLedgerHeadDetail As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

        Dgl3.RowCount = 1 : Dgl3.Rows.Clear()
        For I As Integer = 0 To DtLedgerHeadDetail.Rows.Count - 1
            Dgl3.Rows.Add()
            Dgl3.Item(ColSNo, I).Value = Dgl3.Rows.Count
            Dgl3.Item(Col3DrCr, I).Value = AgL.XNull(DtLedgerHeadDetail.Rows(I)("DrCr"))
            Dgl3.Item(Col3V_Date, I).Value = AgL.XNull(DtLedgerHeadDetail.Rows(I)("V_Date"))
            Dgl3.Item(Col3Party, I).Tag = AgL.XNull(DtLedgerHeadDetail.Rows(I)("Party"))
            Dgl3.Item(Col3Party, I).Value = AgL.XNull(DtLedgerHeadDetail.Rows(I)("PartyName"))
            Dgl3.Item(Col3ReasonAc, I).Value = AgL.XNull(DtLedgerHeadDetail.Rows(I)("ReasonAc"))
            Dgl3.Item(Col3Amount, I).Value = AgL.VNull(DtLedgerHeadDetail.Rows(I)("Amount"))
            Dgl3.Item(Col3SyncedPurchReturnDocId, I).Value = AgL.XNull(DtLedgerHeadDetail.Rows(I)("SyncedPurchInvoiceDocId"))
            Dgl3.Item(Col3Remark, I).Value = AgL.XNull(DtLedgerHeadDetail.Rows(I)("Remark"))
        Next

        Dgl1.ReadOnly = True
        Dgl2.ReadOnly = True
        Dgl3.ReadOnly = True
        TxtOrderNo.Enabled = False
        BtnOk.Enabled = False
    End Sub
    Private Sub BtnFind_Click(sender As Object, e As EventArgs) Handles BtnFind.Click
        Find()
    End Sub
    Private Sub FDelete()
        mQry = " Select * From SaleInvoiceGeneratedEntries Where Code = '" & mSearchCode & "' "
        Dim DtTemp As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

        Dim mTrans As String = ""
        Try
            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = "Begin"

            mQry = " Delete From WPurchInvoiceDetail Where Code = '" & mSearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            mQry = " Delete From WSaleInvoiceDetail Where Code = '" & mSearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            mQry = " Delete From WLedgerHeadDetail Where Code = '" & mSearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

            For I As Integer = 0 To DtTemp.Rows.Count - 1
                If AgL.XNull(DtTemp.Rows(I)("Type")) = "Sale Return" And AgL.XNull(DtTemp.Rows(I)("V_Type")) = "WSR" Then
                    FDeleteSaleInvoice(AgL.XNull(DtTemp.Rows(I)("DocId")), AgL.GCn, AgL.ECmd)
                End If

                If AgL.XNull(DtTemp.Rows(I)("Type")) = "Purchase Return" And AgL.XNull(DtTemp.Rows(I)("V_Type")) = "WPR" Then
                    FDeletePurchaseInvoice(AgL.XNull(DtTemp.Rows(I)("DocId")), AgL.GCn, AgL.ECmd)
                End If

                If AgL.XNull(DtTemp.Rows(I)("Type")) = "Credit Note" Then
                    FDeleteLedgerHeads(AgL.XNull(DtTemp.Rows(I)("DocId")), AgL.GCn, AgL.ECmd)
                End If
            Next

            mQry = "Delete From SaleInvoiceGeneratedEntries Where Code = '" & mSearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

            AgL.ETrans.Commit()
            mTrans = "Commit"

            MsgBox("Record Deleted Successfull...!", MsgBoxStyle.Information)
            BlankText()
        Catch ex As Exception
            AgL.ETrans.Rollback()
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub FDeleteSaleInvoice(bDocId As String, Conn As Object, Cmd As Object)
        mQry = " Delete From SaleInvoiceTrnSetting Where DocId = '" & bDocId & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " Delete From Stock Where DocId = '" & bDocId & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " Delete From Ledger Where DocId = '" & bDocId & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " Delete From SaleInvoiceDimensionDetail Where DocId = '" & bDocId & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " Delete From SaleInvoiceDetailHelpValues Where DocId = '" & bDocId & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " Delete From SaleInvoiceTransport Where DocId = '" & bDocId & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " Delete From SaleInvoiceDetail Where DocId = '" & bDocId & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " Delete From TransactionReferences Where ReferenceDocId = '" & bDocId & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " Delete From SaleInvoice Where DocId = '" & bDocId & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    End Sub
    Private Sub FDeletePurchaseInvoice(bDocId As String, Conn As Object, Cmd As Object)
        mQry = " Delete From Stock Where DocId = '" & bDocId & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " Delete From Ledger Where DocId = '" & bDocId & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " Delete From PurchInvoiceTransport Where DocId = '" & bDocId & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " Delete From PurchInvoiceDimensionDetail Where DocId = '" & bDocId & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " Delete From PurchInvoiceDetail Where DocId = '" & bDocId & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " Delete From TransactionReferences Where ReferenceDocId = '" & bDocId & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)


        mQry = " Delete From PurchInvoice Where DocId = '" & bDocId & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    End Sub

    Private Sub FDeleteLedgerHeads(bDocId As String, Conn As Object, Cmd As Object)
        mQry = "Delete From Ledger Where DocID = '" & bDocId & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Delete From LedgerHeadDetail Where DocID = '" & bDocId & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " Delete From TransactionReferences Where ReferenceDocId = '" & bDocId & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Delete From LedgerHead Where DocID = '" & bDocId & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Delete From LedgerM Where DocID = '" & bDocId & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    End Sub

    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles BtnDelete.Click
        If mSearchCode = "" Then
            MsgBox("No Record Selected...!", MsgBoxStyle.Information)
            Exit Sub
        End If

        If MsgBox("Are tou sure to delete ? ", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            FDelete()
        End If
    End Sub
    Private Sub BtnAdd_Click(sender As Object, e As EventArgs) Handles BtnAdd.Click
        BlankText()
        BtnOk.Enabled = True
        BtnDelete.Enabled = False
        TxtOrderNo.Enabled = True
        Dgl1.ReadOnly = False
        Dgl2.ReadOnly = False
        Dgl3.ReadOnly = False
    End Sub
    Private Sub CopyAttachments(SourceDocId As String, DestinationDocId As String)
        Dim SourceDatabasePath As String = AgL.INIRead(StrPath + "\" + IniName, "CompanyInfo", "ActualDBPath", "")
        Dim SourcePath As String = System.IO.Path.GetDirectoryName(SourceDatabasePath) + "\Images\" + SourceDocId
        'Dim DestinationPath As String = "D:\DesktopApp\trunk\Auditor9\Data\Images\" + DestinationDocId
        Dim DestinationPath As String = PubAttachmentPath + DestinationDocId

        If (Directory.Exists(SourcePath)) Then
            Dim bDirectoryInfo As New DirectoryInfo(SourcePath)
            Dim mFileArr As FileInfo() = bDirectoryInfo.GetFiles()

            If mFileArr.Count = 0 Then Exit Sub

            Dim mFile As FileInfo
            For Each mFile In mFileArr
                'My.Computer.FileSystem.MoveFile(SourcePath + "\" + mFile.Name, DestinationPath + "\" + mFile.Name)
                Dim destinationFileName As String = System.IO.Path.Combine(DestinationPath, mFile.Name)
                My.Computer.FileSystem.CopyFile(SourcePath + "\" + mFile.Name, destinationFileName, True)
                My.Computer.FileSystem.DeleteFile(SourcePath + "\" + mFile.Name)
            Next mFile
            My.Computer.FileSystem.DeleteDirectory(SourcePath, FileIO.DeleteDirectoryOption.DeleteAllContents)
        End If
    End Sub
    Private Sub BtnSync_Click(sender As Object, e As EventArgs)
        'FSyncSaleOrders()
        'FSyncSaleOrderDocuments()
        'FSyncPurchInvoiceDocuments()
    End Sub


    Private Function FGetSubCodeFromOMSId(SubCode As String) As String
        Dim DtSubGroupRow As DataRow() = DtSubGroup.Select("OMSId = '" & SubCode & "'")
        If DtSubGroupRow.Length > 0 Then
            FGetSubCodeFromOMSId = DtSubGroupRow(0)("SubCode")
        Else
            FGetSubCodeFromOMSId = ""
        End If
    End Function
    Private Function FGetItemCodeFromOMSId(Code As String) As String
        Dim DtItemRow As DataRow() = DtItem.Select("OMSId = '" & Code & "'")
        If DtItemRow.Length > 0 Then
            FGetItemCodeFromOMSId = DtItemRow(0)("Code")
        Else
            FGetItemCodeFromOMSId = ""
        End If
    End Function

    Private Sub BtnPrint_Click(sender As Object, e As EventArgs) Handles BtnPrint.Click
        Dim dtTemp As DataTable
        mQry = "Select DocID From SaleInvoiceGeneratedEntries Where Code = '" & mSearchCode & "' And V_Type = 'SI' "
        dtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
        If dtTemp.Rows.Count > 0 Then
            'FrmSaleInvoiceDirect.FGetPrintCrystal_Aadhat(Me, AgL.XNull(dtTemp.Rows(0)("DocID")), PrintFor.DocumentPrint, False, "", "")
        End If
    End Sub

    Private Sub BtnPrintW_Click(sender As Object, e As EventArgs) Handles BtnPrintW.Click

        FGetPrintCrystal(PrintFor.DocumentPrint, False, "")
    End Sub

    Sub FGetPrintCrystal(mPrintFor As ClsMain.PrintFor, Optional ByVal IsPrintToPrinter As Boolean = False, Optional BulkCondStr As String = "")

        Try
            'Dim I As Integer


            'Dim dtTemp As DataTable
            'mQry = "Select DocID From SaleInvoiceGeneratedEntries Where Code = '" & mSearchCode & "' And V_Type = 'SI' "
            'dtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
            'If dtTemp.Rows.Count > 0 Then
            '    mQry = ""
            '    FrmSaleInvoiceDirect.FGetPrintCrystal_Aadhat(Me, AgL.XNull(dtTemp.Rows(0)("DocID")), PrintFor.DocumentPrint, False, "", "")
            'End If


            mQry = "Select LE.Code, 'A' AS ATYPE, SM.Name as SiteName,  H.DocID, Max(L.Sr) as Sr, Max(H.ManualRefNo) AS InvoiceNo, 
                    (Select Max(ManualRefNo) From SaleInvoice Where DocID = (Select DocID From SaleInvoiceGeneratedEntries Where Code='" & mSearchCode & "' And V_Type ='WSI' Limit 1)) as InvoiceNoW, 
                    H.V_Date, Max(Sp.Name) as SaleToPartyName, Max(Sg.DispName) AS PartyName, Max(Sg.ManualCode) AS PartyCode, Max(Sg.Address) Address, Max(c.CityName) AS CityName, 
                    Max(IG.Description) As Brand, 
                    (select sPI.VendorDocNo from purchInvoice sPI 
                    Left Join PurchInvoiceDetail sPIL On sPI.DocID = sPIL.DocId                    
                    where sPI.DocId In (Select DocID from SaleInvoiceGeneratedEntries Where Code='" & mSearchCode & "' And V_Type='PI' )
                    And sPIL.Item = L.Item) as PInvNo, 
                    Sum(L.Qty) as Qty,sUM(L.Amount) As Amount, 
                    Sum(L.Amount + (L.DiscountAmount + L.AdditionalDiscountAmount - L.AdditionAmount)) As GoodsValue, 
                    Max(L.DiscountPer + L.AdditionalDiscountPer - L.AdditionPer) As DiscountPer,
                    Sum(L.DiscountAmount + L.AdditionalDiscountAmount - L.AdditionAmount) As TotalDiscount, 
                    Sum(L.Tax1 + L.Tax2 + L.Tax3 + L.Tax4_Per + L.Tax5) As Tax, 
                    Sum(L.Net_Amount) As NetAmount, Max(Tr.DispName) As TransportName, Max(SIT.LrNo) As LRNO, Date(Max(SIT.LrDate)) As LRDate, 
                    Max(SIT.NoOfBales) As NOOfBales, Max(SIT.PrivateMark) As PrivateMark , Max(SIT.BookedFrom) BookedFrom, Max(SIT.Destination) As Destination,
                    '" & AgL.PubUserName & "' as PrintedByUser, 
                    (Select Max(EntryBy) From SaleInvoice Where DocID = (Select DocID From SaleInvoiceGeneratedEntries Where Code='" & mSearchCode & "' And V_Type ='WSI' Limit 1)) as EntryByUser, 
                    (Select Date(Max(EntryDate)) From SaleInvoice Where DocID = (Select DocID From SaleInvoiceGeneratedEntries Where Code='" & mSearchCode & "' And V_Type ='WSI' Limit 1)) as UserEntryDate  
                    From SaleInvoice H 
                    Left Join SaleInvoiceDetail L ON H.DocID = L.DocID 
                    Left Join Item I ON L.Item = I.Code 
                    Left Join Item IG ON I.ItemGroup = IG.Code 
                    Left Join viewHelpSubgroup Sp ON H.SaleToParty = Sp.Code  
                    Left Join Subgroup Sg ON H.BillToParty = Sg.Subcode  
                    Left Join City C ON Sg.CityCode = C.CityCode 
                    Left Join SaleInvoiceTransport SIT ON H.DocID = SIT.DocID 
                    Left Join Subgroup Tr ON SIT.Transporter = Tr.Subcode 
                    Left Join SaleInvoiceGeneratedEntries LE ON H.DocID = LE.DocId
                    Left Join SiteMast SM On H.Site_Code= Sm.Code
                    WHERE H.V_Type ='SI' And LE.Code ='" & mSearchCode & "'
                    GROUP BY H.DocID, IG.Description 

                    UNION All

                    Select LE.Code, 'W' AS ATYPE, SM.Name as SiteName,  H.DocID, L.Sr, Null as InvoiceNo, Max(H.ManualRefNo) AS InvoiceNoW, 
                    H.V_Date, Max(Sp.Name) as SaleToPartyName, Max(Sg.DispName) AS PartyName, Max(Sg.ManualCode) AS PartyCode, Max(Sg.Address) Address, Max(c.CityName) AS CityName, 
                    Max(I.Description) As Brand, 
                    (select sPI.VendorDocNo from purchInvoice sPI 
                    Left Join PurchInvoiceDetail sPIL On sPI.DocID = sPIL.DocId                    
                    where sPI.DocId In (Select DocID from SaleInvoiceGeneratedEntries Where Code='" & mSearchCode & "' And V_Type='WPI' )
                    And sPIL.Item = L.Item) as PInvNo, 
                    Sum(Case When I.ItemType <> '" & ItemTypeCode.ServiceProduct & "' Then L.Qty else 0 End) as Qty, sUM(L.Amount) As Amount,
                    Sum(L.Amount + (L.DiscountAmount + L.AdditionalDiscountAmount - L.AdditionAmount)) As GoodsValue, 
                    Max(L.DiscountPer + L.AdditionalDiscountPer - L.AdditionPer) As DiscountPer,
                    Sum(L.DiscountAmount + L.AdditionalDiscountAmount - L.AdditionAmount) As TotalDiscount, 
                    Sum(L.Tax1 + L.Tax2 + L.Tax3 + L.Tax4_Per + L.Tax5) As Tax, 
                    Sum(L.Net_Amount) As NetAmount, Max(Tr.DispName) As TransportName, Max(SIT.LrNo) As LRNO, Date(Max(SIT.LrDate)) As LRDate, 
                    Max(SIT.NoOfBales) As NOOfBales, Max(SIT.PrivateMark) As PrivateMark , Max(SIT.BookedFrom) BookedFrom, Max(SIT.Destination) As Destination,
                    '" & AgL.PubUserName & "' as PrintedByUser, H.EntryBy as EntryByUser, Date(H.EntryDate) as UserEntryDate     
                    From SaleInvoice H 
                    Left Join SaleInvoiceDetail L ON H.DocID = L.DocID 
                    Left Join Item I ON L.Item = I.Code 
                    Left Join Item IG ON I.ItemGroup = IG.Code 
                    Left Join viewHelpSubgroup Sp ON H.SaleToParty = Sp.Code  
                    Left Join Subgroup Sg ON H.BillToParty = Sg.Subcode 
                    Left Join City C ON Sg.CityCode = C.CityCode 
                    Left Join SaleInvoiceTransport SIT ON H.DocID = SIT.DocID 
                    Left Join Subgroup Tr ON SIT.Transporter = Tr.Subcode 
                    Left Join SaleInvoiceGeneratedEntries LE ON H.DocID = LE.DocId 
                    Left Join SiteMast SM On H.Site_Code= Sm.Code
                    WHERE H.V_Type ='WSI' And H.Net_Amount>0 And LE.Code ='" & mSearchCode & "'
                    GROUP BY H.DocID, IG.Description, L.Sr                     
                    ORDER BY LE.Code, H.DocID, L.Sr
                   "

            Dim objRepPrint As Object
            If mPrintFor = ClsMain.PrintFor.EMail Then
                objRepPrint = New AgLibrary.FrmMailComposeWithCrystal(AgL)
            Else
                objRepPrint = New AgLibrary.RepView()
            End If


            ClsMain.FPrintThisDocument(objRepPrint, objRepPrint, "", mQry, "SaleInvoice_Print_AadhatW2.rpt", "CHALLAN", , , , "", AgL.PubLoginDate, IsPrintToPrinter)
        Catch ex As Exception
            MsgBox(ex.Message & "  In FGetPrintCrysal Procedure of ClsMasterPartyLedgerAadhat")
        End Try
    End Sub

    Private Sub BtnTransportDetail_Click(sender As Object, e As EventArgs) Handles BtnTransportDetail.Click
        ShowSaleInvoiceHeader()
    End Sub

    Private Sub ShowSaleInvoiceHeader()
        If BtnTransportDetail.Tag IsNot Nothing Then
            CType(BtnTransportDetail.Tag, FrmSaleInvoiceTransport).EntryMode = "Add"
            CType(BtnTransportDetail.Tag, FrmSaleInvoiceTransport).Ncat = Ncat.SaleInvoice
            BtnTransportDetail.Tag.ShowDialog()
        Else
            Dim FrmObj As FrmSaleInvoiceTransport
            FrmObj = New FrmSaleInvoiceTransport
            FrmObj.Ncat = Ncat.SaleInvoice
            FrmObj.IniGrid(mSearchCode)
            FrmObj.EntryMode = "Add"

            BtnTransportDetail.Tag = FrmObj
            BtnTransportDetail.Tag.ShowDialog()
        End If
    End Sub
    Private Sub FPostUIValues(SearchCode As String, Conn As Object, Cmd As Object)
        Dim mSr As Integer = 0
        For I As Integer = 0 To Dgl1.Rows.Count - 1
            mSr += 1
            mQry = "INSERT INTO WPurchInvoiceDetail (Code, Sr, SyncedPurchInvoiceDocId, Supplier, InvoiceNo, InvoiceDate, ItemGroup, InvoiceDiscountPer, InvoiceAdditionalDiscountPer, DiscountPer, AdditionalDiscountPer, AdditionPer, Amount, AmountWithoutDiscountAndTax, MasterSupplier, WInvoiceNo, WInvoiceDate, WQty, WFreight, WPacking, WAmount, WPurchInvoiceAmount, WPurchInvoiceDocId)
                Select " & AgL.Chk_Text(SearchCode) & " As Code,  " & mSr & " As Sr, 
                " & AgL.Chk_Text(Dgl1.Item(Col1SyncedPurchReturnDocId, I).Value) & " As SyncedPurchInvoiceDocId, 
                " & AgL.Chk_Text(Dgl1.Item(Col1Supplier, I).Tag) & " As Supplier, 
                " & AgL.Chk_Text(Dgl1.Item(Col1ReturnNo, I).Value) & " As InvoiceNo, 
                " & AgL.Chk_Date(Dgl1.Item(Col1ReturnDate, I).Value) & " As InvoiceDate, 
                " & AgL.Chk_Text(Dgl1.Item(Col1ItemGroup, I).Tag) & " As ItemGroup, 
                " & Val(Dgl1.Item(Col1ReturnDiscountPer, I).Value) & " As InvoiceDiscountPer, 
                " & Val(Dgl1.Item(Col1ReturnAdditionalDiscountPer, I).Value) & " As InvoiceAdditionalDiscountPer, 
                " & Val(Dgl1.Item(Col1DiscountPer, I).Value) & " As DiscountPer, 
                " & Val(Dgl1.Item(Col1AdditionalDiscountPer, I).Value) & " As AdditionalDiscountPer, 
                " & Val(Dgl1.Item(Col1AdditionPer, I).Value) & " As AdditionPer, 
                " & Val(Dgl1.Item(Col1Amount, I).Value) & " As Amount, 
                " & Val(Dgl1.Item(Col1AmountWithoutDiscountAndTax, I).Value) & " As AmountWithoutDiscountAndTax, 
                " & AgL.Chk_Text(Dgl1.Item(Col1MasterSupplier, I).Tag) & " As MasterSupplier, 
                " & AgL.Chk_Text(Dgl1.Item(Col1WReturnNo, I).Value) & " As WInvoiceNo, 
                " & AgL.Chk_Date(Dgl1.Item(Col1WReturnDate, I).Value) & " As WInvoiceDate, 
                " & Val(Dgl1.Item(Col1WQty, I).Value) & " As WQty, 
                " & Val(Dgl1.Item(Col1WFreight, I).Value) & " As WFreight, 
                " & Val(Dgl1.Item(Col1WPacking, I).Value) & " As WPacking, 
                " & Val(Dgl1.Item(Col1WAmount, I).Value) & " As WAmount, 
                " & Val(Dgl1.Item(Col1WPurchReturnAmount, I).Value) & " As WPurchInvoiceAmount,
                " & AgL.Chk_Text(Dgl1.Item(Col1WPurchReturnDocId, I).Value) & " As WPurchInvoiceDocId "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        Next

        mSr = 0
        For I As Integer = 0 To Dgl2.Rows.Count - 1
            mSr += 1
            mQry = "INSERT INTO WSaleInvoiceDetail (Code, Sr, SyncedSaleInvoiceDocId, Party, InvoiceNo, InvoiceDate, ItemGroup, DiscountPer, AdditionalDiscountPer, AdditionPer, Amount, AmountWithoutTax, Discount, MasterParty, WSaleOrderDocId, WInvoiceNo, WInvoiceDate, WQty, WFreight, WPacking, WDiscount, WSaleInvoiceAmount, WSaleInvoiceDocId)
                    Select " & AgL.Chk_Text(SearchCode) & " As Code,  " & mSr & " As Sr, 
                    " & AgL.Chk_Text(Dgl2.Item(Col2SyncedSaleReturnDocId, I).Value) & " As SyncedSaleInvoiceDocId, 
                    " & AgL.Chk_Text(Dgl2.Item(Col2Party, I).Tag) & " As Party, 
                    " & AgL.Chk_Text(Dgl2.Item(Col2ReturnNo, I).Value) & " As InvoiceNo, 
                    " & AgL.Chk_Date(Dgl2.Item(Col2ReturnDate, I).Value) & " As InvoiceDate, 
                    " & AgL.Chk_Text(Dgl2.Item(Col2ItemGroup, I).Tag) & " As ItemGroup, 
                    " & Val(Dgl2.Item(Col2DiscountPer, I).Value) & " As DiscountPer, 
                    " & Val(Dgl2.Item(Col2AdditionalDiscountPer, I).Value) & " As AdditionalDiscountPer, 
                    " & Val(Dgl2.Item(Col2AdditionPer, I).Value) & " As AdditionPer, 
                    " & Val(Dgl2.Item(Col2Amount, I).Value) & " As Amount, 
                    " & Val(Dgl2.Item(Col2AmountWithoutTax, I).Value) & " As AmountWithoutTax, 
                    " & Val(Dgl2.Item(Col2Discount, I).Value) & " As Discount, 
                    " & AgL.Chk_Text(Dgl2.Item(Col2MasterParty, I).Tag) & " As MasterParty, 
                    " & AgL.Chk_Text(Dgl2.Item(Col2WSaleOrderDocId, I).Value) & " As WSaleOrderDocId, 
                    " & AgL.Chk_Text(Dgl2.Item(Col2WReturnNo, I).Value) & " As WInvoiceNo, 
                    " & AgL.Chk_Date(Dgl2.Item(Col2WReturnDate, I).Value) & " As WInvoiceDate, 
                    " & Val(Dgl2.Item(Col2WQty, I).Value) & " As WQty, 
                    " & Val(Dgl2.Item(Col2WFreight, I).Value) & " As WFreight, 
                    " & Val(Dgl2.Item(Col2WPacking, I).Value) & " As WPacking, 
                    " & Val(Dgl2.Item(Col2WDiscount, I).Value) & " As WDiscount, 
                    " & Val(Dgl2.Item(Col2WSaleReturnAmount, I).Value) & " As WSaleInvoiceAmount ,
                    " & AgL.Chk_Text(Dgl2.Item(Col2WSaleReturnDocId, I).Value) & " As WSaleInvoiceDocId "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        Next

        mSr = 0
        For I As Integer = 0 To Dgl3.Rows.Count - 1
            mSr += 1
            mQry = "INSERT INTO WLedgerHeadDetail (Code, Sr, DrCr, V_Date, Party, ReasonAc, Amount, SyncedPurchInvoiceDocId, Remark)
                    Select " & AgL.Chk_Text(SearchCode) & " As Code,  " & mSr & " As Sr, 
                    " & AgL.Chk_Text(Dgl3.Item(Col3DrCr, I).Value) & " As DrCr, 
                    " & AgL.Chk_Date(Dgl3.Item(Col3V_Date, I).Value) & " As V_Date, 
                    " & AgL.Chk_Text(Dgl3.Item(Col3Party, I).Tag) & " As Party, 
                    " & AgL.Chk_Text(Dgl3.Item(Col3ReasonAc, I).Value) & " As ReasonAc, 
                    " & Val(Dgl3.Item(Col3Amount, I).Value) & " As Amount, 
                    " & AgL.Chk_Text(Dgl3.Item(Col3SyncedPurchReturnDocId, I).Value) & " As SyncedPurchInvoiceDocId, 
                    " & AgL.Chk_Text(Dgl3.Item(Col3Remark, I).Value) & " As Remark "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        Next
    End Sub
    Private Sub Dgl2_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles Dgl2.CellEnter
        Try
            If Dgl2.CurrentCell Is Nothing Then Exit Sub

            Select Case Dgl2.Columns(Dgl2.CurrentCell.ColumnIndex).Name
                Case Col2WReturnDate
                    If Dgl2.Item(Col2ReturnDate, Dgl2.CurrentCell.RowIndex).Value <> "" Then
                        Dgl2.Columns(Col2WReturnDate).ReadOnly = True
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Dgl2_EditingControl_Validating(sender As Object, e As CancelEventArgs) Handles Dgl2.EditingControl_Validating
        Try
            If Dgl2.CurrentCell Is Nothing Then Exit Sub

            Select Case Dgl2.Columns(Dgl2.CurrentCell.ColumnIndex).Name
                Case Col2WReturnNo
                    If Dgl2.Item(Col2ReturnDate, Dgl2.CurrentCell.RowIndex).Value <> "" And
                            Dgl2.Item(Col2WReturnNo, Dgl2.CurrentCell.RowIndex).Value <> "" Then
                        Dgl2.Item(Col2WReturnDate, Dgl2.CurrentCell.RowIndex).Value = Dgl2.Item(Col2ReturnDate, Dgl2.CurrentCell.RowIndex).Value
                    End If
            End Select
            Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub BtnApprove_Click(sender As Object, e As EventArgs) Handles BtnApprove.Click
        mQry = "Update SaleInvoiceGeneratedEntries set ApproveBy = '" & AgL.PubUserName & "', ApproveDate = " & AgL.Chk_DateTime(Now()) & " Where Code = '" & mSearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

        mQry = " Select L.ApproveBy, L.ApproveDate 
                From SaleInvoiceGeneratedEntries L
                Where Code = '" & mSearchCode & "' "
        Dim DtTemp As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

        LblApproveBy.Text = AgL.XNull(DtTemp.Rows(0)("ApproveBy")) & IIf(AgL.XNull(DtTemp.Rows(0)("ApproveBy")) <> "", "  " & AgL.XNull(DtTemp.Rows(0)("ApproveDate")), "")
        If LblApproveBy.Text <> "" Then
            BtnApprove.Visible = False
        Else
            BtnApprove.Visible = True
        End If

    End Sub
End Class