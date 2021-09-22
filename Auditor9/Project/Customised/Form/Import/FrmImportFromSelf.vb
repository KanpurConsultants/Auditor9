Imports System.Data.SQLite
Imports Customised.FrmSaleInvoiceDirect_WithDimension

Public Class FrmImportFromSelf
    Public WithEvents Dgl1 As New AgControls.AgDataGrid

    Dim mUserAction As String = "None"
    Dim DsExcelData As New DataSet
    Dim MyConnection As Object
    Public ReadOnly Property UserAction() As String
        Get
            UserAction = mUserAction
        End Get
    End Property
    Private Sub Ini_Grid()
        Dim mQry As String = ""
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.ColumnHeadersHeight = 30
        Dgl1.EnableHeadersVisualStyles = False
        AgL.GridDesign(Dgl1)

        'mQry = "Select '' as Srl, 'Item' as [Data], Null As [Process Done] "
        'mQry = mQry + "Union All Select  '' as Srl,'Party' as [Data], Null As [Process Done] "
        'mQry = mQry + "Union All Select  '' as Srl,'Sale' as [Data], Null As [Process Done] "
        ''mQry = mQry + "Union All Select  '' as Srl,'Purchase' as [Data], Null As [Process Done] "
        ''mQry = mQry + "Union All Select  '' as Srl,'Ledger' as [Data], Null As [Process Done] "
        'Dgl1.DataSource = AgL.FillData(mQry, AgL.GCn).Tables(0)



        'Dgl1.Columns(0).Width = 40
        'Dgl1.Columns(1).Width = 180
        'Dgl1.Columns(2).Width = 200
        Dgl1.ReadOnly = True
        Dgl1.AllowUserToAddRows = False

        ''Dgl1.DefaultCellStyle.WrapMode = DataGridViewTriState.True
        ''Dgl1.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells)

        'AgCL.AddAgTextColumn(Dgl1, "CFieldName", 100, 0, "CFieldName", False)
    End Sub
    Private Sub FrmImportFromSelf_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Ini_Grid()
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSelectExcelFile.Click
        Dim MyCommand As SQLite.SQLiteCommand = Nothing
        Dim DsTemp As New DataSet
        Dim ImportedFile As String

        Opn.ShowDialog()
        ImportedFile = Opn.FileName
        TxtExcelPath.Text = ImportedFile
        MyConnection = New SQLiteConnection("Data Source=" & ImportedFile & ";Version=3;Password=" & AgLibrary.ClsConstant.PubDbPassword & ";")
        'MyConnection = New Object("Data Source=" & AgL.PubCompanyDBPath & AgL.PubCompanyDBName & ";Version=3;Password=" & AgLibrary.ClsConstant.PubDbPassword & ";")
        MyConnection.Open()
    End Sub
    Private Sub BtnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnOK.Click, BtnCancel.Click

        Select Case sender.name
            Case BtnOK.Name
                MsgBox("Process Done.")

            Case BtnCancel.Name
                mUserAction = sender.text
                Me.Dispose()
        End Select
    End Sub
    Private Sub FrmImportPurchaseFromExcel_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub ImportData()
        Dim mQry As String = ""
        Dim I As Integer = 0, J As Integer = 0, K As Integer = 0
        Dim DtSaleInvoice As DataTable
        Dim DtSaleInvoiceDetail_ForHeader As DataTable
        Dim DtSaleInvoiceDimensionDetail_ForHeader As DataTable

        mQry = "Select * From SaleInvoice "
        DtSaleInvoice = AgL.FillData(mQry, AgL.GCn).Tables(0)

        For I = 0 To DtSaleInvoice.Rows.Count - 1
            Dim SaleInvoiceTableList(0) As StructSaleInvoice
            Dim SaleInvoiceDimensionTableList(0) As StructSaleInvoiceDimensionDetail
            Dim SaleInvoiceTable As New StructSaleInvoice

            SaleInvoiceTable.DocID = AgL.XNull(DtSaleInvoice.Rows(I)("DocId"))
            SaleInvoiceTable.V_Type = AgL.XNull(DtSaleInvoice.Rows(I)("V_Type"))
            SaleInvoiceTable.V_Prefix = AgL.XNull(DtSaleInvoice.Rows(I)("V_Prefix"))
            SaleInvoiceTable.Site_Code = AgL.XNull(DtSaleInvoice.Rows(I)("Site_Code"))
            SaleInvoiceTable.Div_Code = AgL.XNull(DtSaleInvoice.Rows(I)("Div_Code"))
            SaleInvoiceTable.V_No = AgL.VNull(DtSaleInvoice.Rows(I)("V_No"))
            SaleInvoiceTable.V_Date = AgL.XNull(DtSaleInvoice.Rows(I)("V_Date"))
            SaleInvoiceTable.ManualRefNo = AgL.XNull(DtSaleInvoice.Rows(I)("ManualRefNo"))
            SaleInvoiceTable.SaleToParty = AgL.XNull(DtSaleInvoice.Rows(I)("SaleToParty"))
            SaleInvoiceTable.AgentCode = AgL.XNull(DtSaleInvoice.Rows(I)("Agent"))
            SaleInvoiceTable.AgentName = ""
            SaleInvoiceTable.SaleToPartyName = ""
            SaleInvoiceTable.BillToPartyCode = AgL.XNull(DtSaleInvoice.Rows(I)("BillToParty"))
            SaleInvoiceTable.BillToPartyName = ""
            SaleInvoiceTable.SaleToPartyAddress = AgL.XNull(DtSaleInvoice.Rows(I)("SaleToPartyAddress"))
            SaleInvoiceTable.SaleToPartyCityCode = AgL.XNull(DtSaleInvoice.Rows(I)("SaleToPartyCity"))
            SaleInvoiceTable.SaleToPartyMobile = AgL.XNull(DtSaleInvoice.Rows(I)("SaleToPartyMobile"))
            SaleInvoiceTable.SaleToPartySalesTaxNo = AgL.XNull(DtSaleInvoice.Rows(I)("SaleToPartySalesTaxNo"))
            SaleInvoiceTable.ShipToAddress = AgL.XNull(DtSaleInvoice.Rows(I)("ShipToAddress"))
            SaleInvoiceTable.RateType = AgL.XNull(DtSaleInvoice.Rows(I)("RateType"))
            SaleInvoiceTable.SalesTaxGroupParty = AgL.XNull(DtSaleInvoice.Rows(I)("SalesTaxGroupParty"))
            SaleInvoiceTable.PlaceOfSupply = AgL.XNull(DtSaleInvoice.Rows(I)("PlaceOfSupply"))
            SaleInvoiceTable.StructureCode = AgL.XNull(DtSaleInvoice.Rows(I)("StructureCode"))
            SaleInvoiceTable.CustomFields = AgL.XNull(DtSaleInvoice.Rows(I)("CustomFields"))
            SaleInvoiceTable.SaleToPartyDocNo = AgL.XNull(DtSaleInvoice.Rows(I)("SaleToPartyDocNo"))
            SaleInvoiceTable.SaleToPartyDocDate = AgL.XNull(DtSaleInvoice.Rows(I)("SaleToPartyDocDate"))
            SaleInvoiceTable.ReferenceDocId = AgL.XNull(DtSaleInvoice.Rows(I)("PlaceOfSupply"))
            SaleInvoiceTable.Remarks = AgL.XNull(DtSaleInvoice.Rows(I)("Remark"))
            SaleInvoiceTable.TermsAndConditions = AgL.XNull(DtSaleInvoice.Rows(I)("TermsAndConditions"))
            SaleInvoiceTable.PaidAmt = AgL.XNull(DtSaleInvoice.Rows(I)("PlaceOfSupply"))
            SaleInvoiceTable.CreditLimit = AgL.VNull(DtSaleInvoice.Rows(I)("CreditLimit"))
            SaleInvoiceTable.CreditDays = AgL.VNull(DtSaleInvoice.Rows(I)("CreditDays"))
            SaleInvoiceTable.Status = AgL.XNull(DtSaleInvoice.Rows(I)("Status"))
            SaleInvoiceTable.EntryBy = AgL.XNull(DtSaleInvoice.Rows(I)("EntryBy"))
            SaleInvoiceTable.EntryDate = AgL.XNull(DtSaleInvoice.Rows(I)("EntryDate"))
            SaleInvoiceTable.ApproveBy = AgL.XNull(DtSaleInvoice.Rows(I)("ApproveBy"))
            SaleInvoiceTable.ApproveDate = AgL.XNull(DtSaleInvoice.Rows(I)("ApproveDate"))
            SaleInvoiceTable.MoveToLog = AgL.XNull(DtSaleInvoice.Rows(I)("MoveToLog"))
            SaleInvoiceTable.MoveToLogDate = AgL.XNull(DtSaleInvoice.Rows(I)("MoveToLogDate"))
            SaleInvoiceTable.UploadDate = AgL.XNull(DtSaleInvoice.Rows(I)("UploadDate"))

            mQry = "Select * From SaleInvoiceDetail Where DocId = '" & SaleInvoiceTable.DocID & "'"
            DtSaleInvoiceDetail_ForHeader = AgL.FillData(mQry, AgL.GCn).Tables(0)

            For J = 0 To DtSaleInvoiceDetail_ForHeader.Rows.Count - 1
                SaleInvoiceTable.Line_Sr = J + 1
                SaleInvoiceTable.Line_ItemName = AgL.XNull(DtSaleInvoice.Rows(J)("Item Name"))
                SaleInvoiceTable.Line_Specification = AgL.XNull(DtSaleInvoice.Rows(J)("Specification"))
                SaleInvoiceTable.Line_SalesTaxGroupItem = AgL.XNull(DtSaleInvoice.Rows(J)("Sales Tax Group Item"))
                SaleInvoiceTable.Line_ReferenceNo = AgL.XNull(DtSaleInvoice.Rows(J)("ReferenceNo"))
                SaleInvoiceTable.Line_DocQty = AgL.VNull(DtSaleInvoice.Rows(J)("DocQty"))
                SaleInvoiceTable.Line_FreeQty = AgL.VNull(DtSaleInvoice.Rows(J)("FreeQty"))
                SaleInvoiceTable.Line_Qty = AgL.VNull(DtSaleInvoice.Rows(J)("Qty"))
                SaleInvoiceTable.Line_Unit = AgL.VNull(DtSaleInvoice.Rows(J)("Unit"))
                SaleInvoiceTable.Line_Pcs = AgL.VNull(DtSaleInvoice.Rows(J)("Pcs"))
                SaleInvoiceTable.Line_UnitMultiplier = AgL.VNull(DtSaleInvoice.Rows(J)("UnitMultiplier"))
                SaleInvoiceTable.Line_DealUnit = AgL.XNull(DtSaleInvoice.Rows(J)("DealUnit"))
                SaleInvoiceTable.Line_DocDealQty = AgL.VNull(DtSaleInvoice.Rows(J)("DocDealQty"))
                SaleInvoiceTable.Line_Rate = AgL.VNull(DtSaleInvoice.Rows(J)("Rate"))
                SaleInvoiceTable.Line_DiscountPer = AgL.VNull(DtSaleInvoice.Rows(J)("DiscountPer"))
                SaleInvoiceTable.Line_DiscountAmount = AgL.VNull(DtSaleInvoice.Rows(J)("DiscountAmount"))
                SaleInvoiceTable.Line_AdditionalDiscountPer = AgL.VNull(DtSaleInvoice.Rows(J)("AdditionalDiscountPer"))
                SaleInvoiceTable.Line_AdditionalDiscountAmount = AgL.VNull(DtSaleInvoice.Rows(J)("AdditionalDiscountAmount"))
                SaleInvoiceTable.Line_Amount = AgL.VNull(DtSaleInvoice.Rows(J)("Amount"))
                SaleInvoiceTable.Line_Remark = AgL.XNull(DtSaleInvoice.Rows(J)("Remark"))
                SaleInvoiceTable.Line_BaleNo = AgL.XNull(DtSaleInvoice.Rows(J)("BaleNo"))
                SaleInvoiceTable.Line_LotNo = AgL.XNull(DtSaleInvoice.Rows(J)("LotNo"))
                SaleInvoiceTable.Line_ReferenceDocId = AgL.XNull(DtSaleInvoice.Rows(J)("ReferenceDocId"))
                SaleInvoiceTable.Line_ReferenceDocIdSr = AgL.XNull(DtSaleInvoice.Rows(J)("ReferenceDocIdSr"))
                SaleInvoiceTable.Line_SaleInvoice = AgL.XNull(DtSaleInvoice.Rows(J)("ReferenceDocIdSr"))
                SaleInvoiceTable.Line_SaleInvoiceSr = AgL.XNull(DtSaleInvoice.Rows(J)("SaleInvoiceSr"))
                SaleInvoiceTable.Line_V_Nature = AgL.XNull(DtSaleInvoice.Rows(J)("V_Nature"))
                SaleInvoiceTable.Line_GrossWeight = AgL.XNull(DtSaleInvoice.Rows(J)("GrossWeight"))
                SaleInvoiceTable.Line_NetWeight = AgL.XNull(DtSaleInvoice.Rows(J)("NetWeight"))
                SaleInvoiceTable.Line_Gross_Amount = AgL.VNull(DtSaleInvoice.Rows(J)("Gross_Amount"))
                SaleInvoiceTable.Line_Taxable_Amount = AgL.VNull(DtSaleInvoice.Rows(J)("Taxable_Amount"))
                SaleInvoiceTable.Line_Tax1_Per = AgL.VNull(DtSaleInvoice.Rows(J)("Tax1_Per"))
                SaleInvoiceTable.Line_Tax1 = AgL.VNull(DtSaleInvoice.Rows(J)("Tax1"))
                SaleInvoiceTable.Line_Tax2_Per = AgL.VNull(DtSaleInvoice.Rows(J)("Tax2_Per"))
                SaleInvoiceTable.Line_Tax2 = AgL.VNull(DtSaleInvoice.Rows(J)("Tax2"))
                SaleInvoiceTable.Line_Tax3_Per = AgL.VNull(DtSaleInvoice.Rows(J)("Tax3_Per"))
                SaleInvoiceTable.Line_Tax3 = AgL.VNull(DtSaleInvoice.Rows(J)("Tax3"))
                SaleInvoiceTable.Line_Tax4_Per = AgL.VNull(DtSaleInvoice.Rows(J)("Tax4_Per"))
                SaleInvoiceTable.Line_Tax4 = AgL.VNull(DtSaleInvoice.Rows(J)("Tax4"))
                SaleInvoiceTable.Line_Tax5_Per = AgL.VNull(DtSaleInvoice.Rows(J)("Tax5_Per"))
                SaleInvoiceTable.Line_Tax5 = AgL.VNull(DtSaleInvoice.Rows(J)("Tax5"))
                SaleInvoiceTable.Line_SubTotal1 = AgL.VNull(DtSaleInvoice.Rows(J)("SubTotal1"))
                SaleInvoiceTable.Line_Deduction_Per = AgL.VNull(DtSaleInvoice.Rows(J)("Deduction_Per"))
                SaleInvoiceTable.Line_Deduction = AgL.VNull(DtSaleInvoice.Rows(J)("Deduction"))
                SaleInvoiceTable.Line_Other_Charge_Per = AgL.VNull(DtSaleInvoice.Rows(J)("Other_Charge_Per"))
                SaleInvoiceTable.Line_Other_Charge = AgL.VNull(DtSaleInvoice.Rows(J)("Other_Charge"))
                SaleInvoiceTable.Line_Round_Off = AgL.VNull(DtSaleInvoice.Rows(J)("Round_Off"))
                SaleInvoiceTable.Line_Net_Amount = AgL.VNull(DtSaleInvoice.Rows(J)("Net_Amount"))


                mQry = "Select * From SaleInvoiceDetail Where DocId = '" & SaleInvoiceTable.DocID & "'"
                DtSaleInvoiceDimensionDetail_ForHeader = AgL.FillData(mQry, AgL.GCn).Tables(0)

                For K = 0 To DtSaleInvoiceDimensionDetail_ForHeader.Rows.Count - 1
                    Dim SaleInvoiceDimensionTable As New StructSaleInvoiceDimensionDetail

                    SaleInvoiceDimensionTable.TSr = AgL.VNull(DtSaleInvoiceDimensionDetail_ForHeader.Rows(K)("TSr"))
                    SaleInvoiceDimensionTable.Sr = AgL.VNull(DtSaleInvoiceDimensionDetail_ForHeader.Rows(K)("Sr"))
                    SaleInvoiceDimensionTable.Specification = AgL.XNull(DtSaleInvoiceDimensionDetail_ForHeader.Rows(K)("Specification"))
                    SaleInvoiceDimensionTable.Pcs = AgL.VNull(DtSaleInvoiceDimensionDetail_ForHeader.Rows(K)("Pcs"))
                    SaleInvoiceDimensionTable.Qty = AgL.VNull(DtSaleInvoiceDimensionDetail_ForHeader.Rows(K)("Qty"))
                    SaleInvoiceDimensionTable.TotalQty = AgL.VNull(DtSaleInvoiceDimensionDetail_ForHeader.Rows(K)("TotalQty"))

                    SaleInvoiceDimensionTableList(UBound(SaleInvoiceDimensionTableList)) = SaleInvoiceDimensionTable
                    ReDim Preserve SaleInvoiceDimensionTableList(UBound(SaleInvoiceDimensionTableList) + 1)
                Next
            Next

            SaleInvoiceTableList(UBound(SaleInvoiceTableList)) = SaleInvoiceTable
            ReDim Preserve SaleInvoiceTableList(UBound(SaleInvoiceTableList) + 1)

            'InsertSaleInvoice(SaleInvoiceTableList, SaleInvoiceDimensionTableList)
        Next
    End Sub
End Class