Imports AgLibrary.ClsMain.agConstants
Imports Customised.ClsMain

Public Class FrmSaleInvoicePurchaseSummary
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Dim mSearchCode$ = ""
    Public Const ColSNo As String = "S.No."
    Public Const Col1SaleOrderNo As String = "Order No"
    Public Const Col1SaleOrderDate As String = "Order Date"
    Public Const Col1Supplier As String = "Supplier"
    Public Const Col1PurchInvoiceNo As String = "Purch Invoice No"
    Public Const Col1PurchInvoiceDate As String = "Purch Invoice Date"
    Public Const Col1GrossAmount As String = "Gross Amount"
    Public Const Col1TotalTax As String = "Total Tax"
    Public Const Col1NetAmount As String = "Net Amount"


    Dim mSaleInvoiceDgl As AgControls.AgDataGrid
    Dim mSaleInvoiceOrderSummaryDgl As AgControls.AgDataGrid
    Dim mQry As String = ""

    Public Property SearchCode() As String
        Get
            SearchCode = mSearchCode
        End Get
        Set(ByVal value As String)
            mSearchCode = value
        End Set
    End Property

    Public Property SaleInvoiceDgl() As AgControls.AgDataGrid
        Get
            SaleInvoiceDgl = mSaleInvoiceDgl
        End Get
        Set(ByVal value As AgControls.AgDataGrid)
            mSaleInvoiceDgl = value
        End Set
    End Property

    Public Property SaleInvoiceOrderSummaryDgl() As AgControls.AgDataGrid
        Get
            SaleInvoiceOrderSummaryDgl = mSaleInvoiceOrderSummaryDgl
        End Get
        Set(ByVal value As AgControls.AgDataGrid)
            mSaleInvoiceOrderSummaryDgl = value
        End Set
    End Property

    Public Sub Ini_Grid()
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl1, Col1SaleOrderNo, 100, 0, Col1SaleOrderNo, False, True)
            .AddAgTextColumn(Dgl1, Col1SaleOrderDate, 100, 0, Col1SaleOrderDate, False, True)
            .AddAgTextColumn(Dgl1, Col1Supplier, 130, 0, Col1Supplier, False, True)
            .AddAgTextColumn(Dgl1, Col1PurchInvoiceNo, 130, 0, Col1PurchInvoiceNo, True, True)
            .AddAgDateColumn(Dgl1, Col1PurchInvoiceDate, 130, Col1PurchInvoiceDate, True, True)
            .AddAgTextColumn(Dgl1, Col1GrossAmount, 130, 0, Col1GrossAmount, True, True)
            .AddAgTextColumn(Dgl1, Col1TotalTax, 130, 0, Col1TotalTax, True, True)
            .AddAgTextColumn(Dgl1, Col1NetAmount, 130, 0, Col1NetAmount, True, True)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 35
        Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Comprehensive
        AgL.GridDesign(Dgl1)

        Dgl1.AllowUserToAddRows = False
        Dgl1.AgSkipReadOnlyColumns = True
        Dgl1.AllowUserToOrderColumns = True

        Dgl1.Anchor = AnchorStyles.Bottom + AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Top
        Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        AgCL.GridSetiingShowXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1, False)
    End Sub

    Private Sub FrmReconcile_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Ini_Grid()
        FLoadPurchaseDataFromSaleInvoice()
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
    Private Sub FSave(DocId As String, Sr As Integer, SelectValue As String, mRowIndex As Integer)

    End Sub
    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, 0)
    End Sub
    Public Sub FLoadPurchaseDataFromSaleInvoice()
        Dim I As Integer = 0
        Dim J As Integer = 0

        Dim Col1Item As String = "Item"
        Dim Col1SaleInvoice As String = "Sale Invoice DocID"
        Dim Col1GrossAmount As String = "Gross Amount"
        Dim Col1CGST As String = "CGST"
        Dim Col1SGST As String = "SGST"
        Dim Col1IGST As String = "IGST"
        Dim Col1NetAmount As String = "Net Amount"

        Dim mConn As Object = Nothing
        If AgL.PubServerName = "" Then
            mConn = New SQLite.SQLiteConnection(AgL.GCn.ConnectionString.ToString)
        Else
            mConn = New SqlClient.SqlConnection(AgL.GCn.ConnectionString)
        End If
        mConn.Open()

        mQry = " CREATE " & IIf(AgL.PubServerName = "", "Temp", "") & " TABLE [#TempSaleInvoicePurchaseSummary](
                SaleOrderDocId NVARCHAR(21),
                SaleOrderNo NVARCHAR(20),
                GrossAmount Float,
                TotalTax Float,
                Netamount Float
                ); "
        AgL.Dman_ExecuteNonQry(mQry, mConn)


        For I = 0 To mSaleInvoiceDgl.Rows.Count - 1
            If mSaleInvoiceDgl.Item(Col1Item, I).Value <> "" Then


                mQry = " INSERT INTO [#TempSaleInvoicePurchaseSummary](SaleOrderDocId, SaleOrderNo, GrossAmount, TotalTax, Netamount) "
                mQry += " Select " & AgL.Chk_Text(mSaleInvoiceDgl.Item(Col1SaleInvoice, I).Tag) & " As SaleOrderDocId, 
                    " & AgL.Chk_Text(mSaleInvoiceDgl.Item(Col1SaleInvoice, I).Value) & " As SaleOrderNo, 
                    " & Val(mSaleInvoiceDgl.Item(Col1GrossAmount, I).Value) & " As GrossAmount, 
                    " & Val(mSaleInvoiceDgl.Item(Col1CGST, I).Value) +
                              Val(mSaleInvoiceDgl.Item(Col1SGST, I).Value) +
                              Val(mSaleInvoiceDgl.Item(Col1IGST, I).Value) & " As TotalTax, 
                    " & Val(mSaleInvoiceDgl.Item(Col1NetAmount, I).Value) & " As NetAmount "


                'mQry += " Select 'D1    SI 2018    9239' As SaleOrderDocId, 
                '        '4058' As SaleOrderNo, 
                '        " & Val(mSaleInvoiceDgl.Item(Col1GrossAmount, I).Value) & " As GrossAmount, 
                '        " & Val(mSaleInvoiceDgl.Item(Col1CGST, I).Value) +
                '              Val(mSaleInvoiceDgl.Item(Col1SGST, I).Value) +
                '              Val(mSaleInvoiceDgl.Item(Col1IGST, I).Value) & " As TotalTax, 
                '        " & Val(mSaleInvoiceDgl.Item(Col1NetAmount, I).Value) & " As NetAmount "

                AgL.Dman_ExecuteNonQry(mQry, mConn)
            End If
        Next

        mQry = " Select H.SaleOrderDocId, Max(H.SaleOrderNo) As SaleOrderNo, Sum(H.GrossAmount) As GrossAmount,
                    Sum(H.TotalTax) As TotalTax, Sum(H.NetAmount) As NetAmount
                    From [#TempSaleInvoicePurchaseSummary] H
                    Group By H.SaleOrderDocId "
        Dim DtTemp As DataTable = AgL.FillData(mQry, mConn).Tables(0)

        If DtTemp.Rows.Count > 0 Then
            Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
            For I = 0 To DtTemp.Rows.Count - 1
                Dgl1.Rows.Add()
                Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count
                Dgl1.Item(Col1SaleOrderNo, I).Tag = AgL.XNull(DtTemp.Rows(I)("SaleOrderDocId"))
                Dgl1.Item(Col1SaleOrderNo, I).Value = AgL.XNull(DtTemp.Rows(I)("SaleOrderNo"))

                For J = 0 To mSaleInvoiceOrderSummaryDgl.Rows.Count - 1
                    If Dgl1.Item(Col1SaleOrderNo, I).Tag = mSaleInvoiceOrderSummaryDgl.Item(Col1SaleOrderNo, J).Tag Then
                        Dgl1.Item(Col1PurchInvoiceNo, I).Value = mSaleInvoiceOrderSummaryDgl.Item(Col1PurchInvoiceNo, J).Value
                        Dgl1.Item(Col1PurchInvoiceDate, I).Value = mSaleInvoiceOrderSummaryDgl.Item(Col1PurchInvoiceDate, J).Value
                        Dgl1.Item(Col1Supplier, I).Tag = mSaleInvoiceOrderSummaryDgl.Item(Col1Supplier, J).Tag
                        Dgl1.Item(Col1Supplier, I).Value = mSaleInvoiceOrderSummaryDgl.Item(Col1Supplier, J).Value
                    End If
                Next

                Dgl1.Item(Col1GrossAmount, I).Value = AgL.VNull(DtTemp.Rows(I)("GrossAmount"))
                Dgl1.Item(Col1TotalTax, I).Value = AgL.VNull(DtTemp.Rows(I)("TotalTax"))
                Dgl1.Item(Col1NetAmount, I).Value = AgL.VNull(DtTemp.Rows(I)("NetAmount"))
            Next I
        End If
        mConn.Close()
    End Sub
    'Public Sub FPostPurchaseData()
    '    Dim I As Integer = 0
    '    Dim J As Integer = 0

    '    Dim StrUserPermission As String = ""
    '    Dim DTUP As New DataTable
    '    DTUP.Columns.Add("UP")
    '    Dim mFrmObj As New FrmPurchInvoiceDirect(StrUserPermission, DTUP, AgLibrary.ClsMain.agConstants.Ncat.PurchaseInvoice)
    '    mFrmObj.Show()
    '    mFrmObj.Topctrl1.FButtonClick(0, True)

    '    For I = 0 To Dgl1.Rows.Count - 1
    '        mFrmObj.TxtVendor.Tag = Dgl1.Item(Col1Supplier, I).Tag
    '        mFrmObj.TxtVendor.Text = Dgl1.Item(Col1Supplier, I).Value
    '        mFrmObj.TxtV_Date.Text = Dgl1.Item(Col1PurchInvoiceDate, I).Value
    '        mFrmObj.TxtVendorDocNo.Text = Dgl1.Item(Col1PurchInvoiceNo, I).Value
    '        mFrmObj.TxtVendorDocDate.Text = Dgl1.Item(Col1PurchInvoiceDate, I).Value

    '        mFrmObj.TxtVendor.Focus()
    '        mFrmObj.TxtRemarks.Focus()

    '        mFrmObj.TxtNature.Text = AgL.Dman_Execute("Select Nature 
    '                From Subgroup Where SubCode = '" & mFrmObj.TxtVendor.Tag & "'", AgL.GCn).ExecuteScalar()

    '        'mQry = " SELECT L.Item, I.Description As ItemDesc, L.Qty, L.Rate, L.Amount
    '        '    FROM SaleInvoiceDetail L 
    '        '    LEFT JOIN Item I On L.Item = I.Code
    '        '    WHERE L.DocID = '" & mSearchCode & "'
    '        '    AND L.ReferenceDocId = '" & Dgl1.Item(Col1SaleOrderNo, I).Tag & "'"


    '        mQry = " SELECT L.Item, I.Description As ItemDesc, L.Qty, L.Rate, L.Amount
    '            FROM SaleInvoiceDetail L 
    '            LEFT JOIN Item I On L.Item = I.Code
    '            WHERE L.DocID = '" & mSearchCode & "'"
    '        Dim DtTemp As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

    '        If DtTemp.Rows.Count > 0 Then
    '            mFrmObj.Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
    '            For J = 0 To DtTemp.Rows.Count - 1
    '                mFrmObj.Dgl1.Rows.Add()
    '                mFrmObj.Dgl1.Item(FrmPurchInvoiceDirect.Col1Item, J).Tag = AgL.XNull(DtTemp.Rows(J)("Item"))
    '                mFrmObj.Dgl1.Item(FrmPurchInvoiceDirect.Col1Item, J).Value = AgL.XNull(DtTemp.Rows(J)("ItemDesc"))

    '                If mFrmObj.Dgl1.AgHelpDataSet(FrmPurchInvoiceDirect.Col1Item) Is Nothing Then
    '                    mFrmObj.FCreateHelpItem(J)
    '                End If
    '                Dim DrTemp As DataRow() = mFrmObj.Dgl1.AgHelpDataSet(FrmPurchInvoiceDirect.Col1Item).
    '                        Tables(0).Select("Code = '" & mFrmObj.Dgl1.Item(FrmPurchInvoiceDirect.Col1Item, J).Tag & "'")
    '                Call mFrmObj.Validating_ItemCode(mFrmObj.Dgl1.Columns(FrmPurchInvoiceDirect.Col1Item).Index, J, DrTemp)
    '                Call mFrmObj.FGetUnitMultiplier(J)

    '                mFrmObj.Dgl1.Item(FrmPurchInvoiceDirect.Col1Qty, J).Value = AgL.VNull(DtTemp.Rows(J)("Qty"))
    '                mFrmObj.Dgl1.Item(FrmPurchInvoiceDirect.Col1DocQty, J).Value = AgL.VNull(DtTemp.Rows(J)("Qty"))
    '                mFrmObj.Dgl1.Item(FrmPurchInvoiceDirect.Col1Rate, J).Value = AgL.VNull(DtTemp.Rows(J)("Rate"))
    '                mFrmObj.Dgl1.Item(FrmPurchInvoiceDirect.Col1Amount, J).Value = AgL.VNull(DtTemp.Rows(J)("Amount"))

    '            Next
    '        End If
    '    Next
    '    mFrmObj.Calculation()
    '    mFrmObj.Topctrl1.FButtonClick(13, True)
    '    mFrmObj.Close()
    'End Sub



    Public Sub FPostPurchaseData(Conn As Object, Cmd As Object)
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
            Dim PurchInvoiceTableList(0) As FrmPurchInvoiceDirect.StructPurchInvoice
            Dim PurchInvoiceTable As New FrmPurchInvoiceDirect.StructPurchInvoice


            PurchInvoiceTable.DocID = ""
            PurchInvoiceTable.V_Type = "PI"
            PurchInvoiceTable.V_Prefix = ""
            PurchInvoiceTable.Site_Code = AgL.PubSiteCode
            PurchInvoiceTable.Div_Code = AgL.PubDivCode
            PurchInvoiceTable.V_No = 0
            PurchInvoiceTable.V_Date = Dgl1.Item(Col1PurchInvoiceDate, I).Value
            PurchInvoiceTable.ManualRefNo = ""
            PurchInvoiceTable.Vendor = Dgl1.Item(Col1Supplier, I).Tag
            PurchInvoiceTable.AgentCode = ""
            PurchInvoiceTable.AgentName = ""
            PurchInvoiceTable.VendorName = ""
            PurchInvoiceTable.BillToPartyCode = Dgl1.Item(Col1Supplier, I).Tag
            PurchInvoiceTable.BillToPartyName = ""
            PurchInvoiceTable.VendorAddress = ""
            PurchInvoiceTable.VendorCity = ""
            PurchInvoiceTable.VendorMobile = ""
            PurchInvoiceTable.VendorSalesTaxNo = ""
            PurchInvoiceTable.ShipToAddress = ""
            PurchInvoiceTable.SalesTaxGroupParty = ""
            PurchInvoiceTable.PlaceOfSupply = ""
            PurchInvoiceTable.StructureCode = ""
            PurchInvoiceTable.CustomFields = ""
            PurchInvoiceTable.VendorDocNo = Dgl1.Item(Col1PurchInvoiceNo, I).Value
            PurchInvoiceTable.VendorDocDate = Dgl1.Item(Col1PurchInvoiceDate, I).Value
            PurchInvoiceTable.ReferenceDocId = ""
            PurchInvoiceTable.GenDocId = SearchCode
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


            'mQry = " SELECT L.*
            '    FROM SaleInvoiceDetail L With (NoLock) 
            '    WHERE L.DocID = '" & mSearchCode & "'
            '    AND L.ReferenceDocId = '" & Dgl1.Item(Col1SaleOrderNo, I).Tag & "'"


            mQry = " SELECT Lv.PurchaseDiscountPer, L.*
                        FROM SaleInvoiceDetail L With (NoLock)
                        LEFT JOIN SaleInvoiceDetailHelpValues Lv ON L.DocID = Lv.DocId And L.Sr = Lv.Sr
                        WHERE L.DocID = '" & mSearchCode & "'"
            Dim DtTemp As DataTable = AgL.FillData(mQry, IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).Tables(0)


            For J = 0 To DtTemp.Rows.Count - 1
                PurchInvoiceTable.Line_Sr = J + 1
                PurchInvoiceTable.Line_ItemCode = AgL.XNull(DtTemp.Rows(J)("Item"))
                PurchInvoiceTable.Line_ItemName = ""
                PurchInvoiceTable.Line_Specification = ""
                PurchInvoiceTable.Line_SalesTaxGroupItem = AgL.XNull(DtTemp.Rows(J)("SalesTaxGroupItem"))
                PurchInvoiceTable.Line_ReferenceNo = ""
                PurchInvoiceTable.Line_DocQty = AgL.VNull(DtTemp.Rows(J)("DocQty"))
                PurchInvoiceTable.Line_FreeQty = 0
                PurchInvoiceTable.Line_Qty = AgL.VNull(DtTemp.Rows(J)("Qty"))
                PurchInvoiceTable.Line_Unit = AgL.XNull(DtTemp.Rows(J)("Unit"))
                PurchInvoiceTable.Line_Pcs = AgL.VNull(DtTemp.Rows(J)("Pcs"))
                PurchInvoiceTable.Line_UnitMultiplier = AgL.VNull(DtTemp.Rows(J)("UnitMultiplier"))
                PurchInvoiceTable.Line_DealUnit = AgL.XNull(DtTemp.Rows(J)("DealUnit"))
                PurchInvoiceTable.Line_DocDealQty = AgL.XNull(DtTemp.Rows(J)("DocDealQty"))
                PurchInvoiceTable.Line_Rate = AgL.XNull(DtTemp.Rows(J)("Rate"))

                PurchInvoiceTable.Line_DiscountPer = AgL.XNull(DtTemp.Rows(J)("PurchaseDiscountPer"))
                PurchInvoiceTable.Line_DiscountAmount = (PurchInvoiceTable.Line_Qty * PurchInvoiceTable.Line_Rate) * PurchInvoiceTable.Line_DiscountPer / 100

                PurchInvoiceTable.Line_AdditionalDiscountPer = AgL.XNull(DtTemp.Rows(J)("AdditionalDiscountPer"))
                PurchInvoiceTable.Line_AdditionalDiscountAmount = AgL.XNull(DtTemp.Rows(J)("AdditionalDiscountAmount"))


                PurchInvoiceTable.Line_Amount = AgL.VNull(DtTemp.Rows(J)("Amount"))

                'Patch
                PurchInvoiceTable.Line_Amount = (PurchInvoiceTable.Line_Qty * PurchInvoiceTable.Line_Rate) - PurchInvoiceTable.Line_DiscountAmount

                PurchInvoiceTable.Line_Remark = ""
                PurchInvoiceTable.Line_BaleNo = AgL.XNull(DtTemp.Rows(J)("BaleNo"))
                PurchInvoiceTable.Line_LotNo = AgL.XNull(DtTemp.Rows(J)("LotNo"))
                PurchInvoiceTable.Line_ReferenceDocId = ""
                PurchInvoiceTable.Line_ReferenceSr = ""
                PurchInvoiceTable.Line_PurchInvoice = ""
                PurchInvoiceTable.Line_PurchInvoiceSr = ""
                PurchInvoiceTable.Line_GrossWeight = 0
                PurchInvoiceTable.Line_NetWeight = 0
                PurchInvoiceTable.Line_Gross_Amount = PurchInvoiceTable.Line_Amount
                PurchInvoiceTable.Line_Taxable_Amount = PurchInvoiceTable.Line_Amount
                PurchInvoiceTable.Line_Tax1_Per = AgL.VNull(DtTemp.Rows(J)("Tax1_Per"))
                PurchInvoiceTable.Line_Tax1 = AgL.VNull(DtTemp.Rows(J)("Tax1"))
                PurchInvoiceTable.Line_Tax2_Per = AgL.VNull(DtTemp.Rows(J)("Tax2_Per"))
                PurchInvoiceTable.Line_Tax2 = AgL.VNull(DtTemp.Rows(J)("Tax2"))
                PurchInvoiceTable.Line_Tax3_Per = AgL.VNull(DtTemp.Rows(J)("Tax3_Per"))
                PurchInvoiceTable.Line_Tax3 = AgL.VNull(DtTemp.Rows(J)("Tax3"))
                PurchInvoiceTable.Line_Tax4_Per = AgL.VNull(DtTemp.Rows(J)("Tax4_Per"))
                PurchInvoiceTable.Line_Tax4 = AgL.VNull(DtTemp.Rows(J)("Tax4"))
                PurchInvoiceTable.Line_Tax5_Per = AgL.VNull(DtTemp.Rows(J)("Tax5_Per"))
                PurchInvoiceTable.Line_Tax5 = AgL.VNull(DtTemp.Rows(J)("Tax5"))
                PurchInvoiceTable.Line_SubTotal1 = PurchInvoiceTable.Line_Amount +
                                                    PurchInvoiceTable.Line_Tax1 +
                                                    PurchInvoiceTable.Line_Tax2 +
                                                    PurchInvoiceTable.Line_Tax3 +
                                                    PurchInvoiceTable.Line_Tax4 +
                                                    PurchInvoiceTable.Line_Tax5


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
            Next


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


End Class