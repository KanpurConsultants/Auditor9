Imports System.IO
Imports AgLibrary
Imports AgLibrary.ClsMain
Imports AgLibrary.ClsMain.agConstants
Imports Customised.ClsMain
Imports Microsoft.Reporting.WinForms
Public Class ClsPurchaseInvoiceWizard

    Dim StrArr1() As String = Nothing, StrArr2() As String = Nothing, StrArr3() As String = Nothing, StrArr4() As String = Nothing, StrArr5() As String = Nothing

    Dim mGRepFormName As String = ""
    Dim mQry As String = ""
    Dim RepTitle As String = ""

    Dim DsReport As DataSet = New DataSet
    Dim DTReport As DataTable = New DataTable
    Dim IntLevel As Int16 = 0

    Dim WithEvents ReportFrm As FrmRepDisplay
    Public Const GFilter As Byte = 2
    Public Const GFilterCode As Byte = 4
    Dim StrSQLQuery As String = ""
    Private Const CnsProfitAndLoss As String = "PRLS"

    Dim mShowReportType As String = ""
    Dim mV_Type As String = ""
    Dim mObjFrm As Object

    Public Const Col1Select As String = "Tick"
    Public Const Col1DocId As String = "Search Code"
    Public Const Col1VoucherDate As String = "Voucher Date"
    Public Const Col1VoucherType As String = "Voucher Type"
    Public Const Col1DivCode As String = "Div Code"
    Public Const Col1SiteCode As String = "Site Code"
    Public Const Col1RateTypeCode As String = "Rate Type Code"
    Public Const Col1SettingGroupCode As String = "Setting Group Code"
    Public Const Col1VoucherNo As String = "Voucher No"
    Public Const Col1PartyCode As String = "Party Code"
    Public Const Col1Party As String = "Party"
    Public Const Col1StockReceiveNo As String = "Stock Receive No"
    Public Const Col1StockReceive As String = "Stock Receive"
    Public Const Col1StockReceiveSr As String = "Stock Receive Sr"

    Public Const Col1ReferenceDocId As String = "Reference Doc Id"
    Public Const Col1ReferenceTSr As String = "Referencetsr"
    Public Const Col1ReferenceSr As String = "Referencesr"

    Public Const Col1SkuCode As String = "Sku Code"
    Public Const Col1ItemTypeCode As String = "Item Type Code"
    Public Const Col1ItemCategoryCode As String = "Item Category Code"
    Public Const Col1ItemGroupCode As String = "Item Group Code"
    Public Const Col1ItemCode As String = "Item Code"
    Public Const Col1Dimension1Code As String = "Dimension1Code"
    Public Const Col1Dimension2Code As String = "Dimension2Code"
    Public Const Col1Dimension3Code As String = "Dimension3Code"
    Public Const Col1Dimension4Code As String = "Dimension4Code"
    Public Const Col1SizeCode As String = "Size Code"

    Public Col1ItemType As String = AgL.PubCaptionItemType
    Public Const Col1ItemCategory As String = "Item Category"
    Public Const Col1ItemGroup As String = "Item Group"
    Public Const Col1Item As String = "Item"
    Public Const Col1Dimension1 As String = "Dimension1"
    Public Const Col1Dimension2 As String = "Dimension2"
    Public Const Col1Dimension3 As String = "Dimension3"
    Public Const Col1Dimension4 As String = "Dimension4"
    Public Const Col1Size As String = "Size"


    Public Const Col1SalesTaxGroupItem As String = "Sales Tax Group Item"
    Public Const Col1Qty As String = "Qty"
    Public Const Col1Unit As String = "Unit"
    Public Const Col1Rate As String = "Rate"
    Public Const Col1Remark As String = "Remark"

    Dim rowFromDate As Integer = 0
    Dim rowToDate As Integer = 1
    Dim rowVendor As Integer = 2
    Dim rowProcess As Integer = 3
    Dim rowInvoiceNo As Integer = 4
    Dim rowInvoiceDate As Integer = 5
    Dim rowRemarks As Integer = 6
    Public Property GRepFormName() As String
        Get
            GRepFormName = mGRepFormName
        End Get
        Set(ByVal value As String)
            mGRepFormName = value
        End Set
    End Property
    Public Property V_Type() As String
        Get
            V_Type = mV_Type
        End Get
        Set(ByVal value As String)
            mV_Type = value
        End Set
    End Property
    Public Property ShowReportType() As String
        Get
            ShowReportType = mShowReportType
        End Get
        Set(ByVal value As String)
            mShowReportType = value
        End Set
    End Property
    Public Property ObjFrm() As Object
        Get
            ObjFrm = mObjFrm
        End Get
        Set(ByVal value As Object)
            mObjFrm = value
        End Set
    End Property

    Dim mHelpSiteQry$ = "Select 'o' As Tick, Code, Name FROM SiteMast "
    Dim mHelpDivisionQry$ = "Select 'o' As Tick, Div_Code As Code, Div_Name As Name From Division "
    Dim mHelpYesNoQry$ = " Select 'Yes' As Code, 'Yes' AS [Value] Union All Select 'No' As Code, 'No' AS [Value] "
    Dim mHelpSubGroupQry$ = "Select 'o' As Tick, Sg.Code, Sg.Name FROM ViewHelpSubgroup Sg  "
    Dim mHelpProcessQry$ = "Select Sg.Code, Sg.Name FROM ViewHelpSubgroup Sg Where Sg.SubGroupType = '" & SubgroupType.Process & "' "
    Public Sub Ini_Grid()
        Try
            Dim mLastMonthDate As String = DateAdd(DateInterval.Month, -1, CDate(AgL.Dman_Execute("SELECT date('now')", AgL.GCn).ExecuteScalar()))
            ReportFrm.CreateHelpGrid("FromDate", "From Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.RetMonthStartDate(mLastMonthDate))
            ReportFrm.CreateHelpGrid("ToDate", "To Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubLoginDate)
            ReportFrm.CreateHelpGrid("Vendor", "Vendor", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSubGroupQry, "")
            ReportFrm.CreateHelpGrid("Process", "Process", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mHelpProcessQry, "")
            Dim bManualRefNo$ = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "PurchInvoice", "JI", AgL.PubLoginDate, AgL.PubDivCode, AgL.PubSiteCode, AgTemplate.ClsMain.ManualRefType.Max)
            ReportFrm.CreateHelpGrid("InvoiceNo", "Invoice No", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.StringType, "", bManualRefNo)
            ReportFrm.CreateHelpGrid("InvoiceDate", "Invoice Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubLoginDate)
            ReportFrm.CreateHelpGrid("Remarks", "Remarks", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.StringType, "", "")
            ReportFrm.BtnProceed.Visible = True
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub ObjRepFormGlobal_ProcessReport() Handles ReportFrm.ProcessReport
        ProcPurchaseInvoiceWizard()
    End Sub
    Public Sub New(ByVal mReportFrm As FrmRepDisplay)
        ReportFrm = mReportFrm
    End Sub
    'Public Sub ProcPurchaseInvoiceWizard(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
    '                            Optional mGridRow As DataGridViewRow = Nothing)
    '    Try
    '        Dim mCondStr$ = ""
    '        Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"

    '        RepTitle = "Invoice Wizard"

    '        If mFilterGrid IsNot Nothing And mGridRow IsNot Nothing Then
    '            If mGridRow.DataGridView.Columns.Contains("Search Code") = True Then
    '                ClsMain.FOpenForm(mGridRow.Cells("Search Code").Value, ReportFrm)
    '                ReportFrm.FiterGridCopy_Arr.RemoveAt(ReportFrm.FiterGridCopy_Arr.Count - 1)
    '                Exit Sub
    '            Else
    '                Exit Sub
    '            End If
    '        End If

    '        If AgL.XNull(ReportFrm.FGetText(rowProcess)) = "" Then
    '            MsgBox("Please select process first.", MsgBoxStyle.Information)
    '            Exit Sub
    '        End If

    '        mCondStr = mCondStr & " AND Date(L.V_Date) Between " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowFromDate)).ToString("s")) & " And " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowToDate)).ToString("s")) & " "
    '        mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.SubCode", rowVendor)
    '        'mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Process", rowProcess)
    '        mCondStr = mCondStr & " And L.Process = " & AgL.Chk_Text(ReportFrm.FGetCode(rowProcess)) & ""

    '        Dim bPendingStockProcessQry$ = ""
    '        bPendingStockProcessQry = " SELECT L.StockProcess, L.StockProcessTSr, L.StockProcessSr, 
    '                IsNull(Sum(L.Qty_Iss),0) - IsNull(Sum(L.Qty_Rec),0) AS BalanceQty
    '                FROM StockProcess L 
    '                Where 1=1 And L.StockProcess Is Not Null " & mCondStr &
    '                " GROUP BY L.StockProcess, L.StockProcessTSr, L.StockProcessSr
    '                HAVING IsNull(Sum(L.Qty_Iss),0) - IsNull(Sum(L.Qty_Rec),0) > 0 "

    '        mQry = " Select 'o' As Tick, L.DocID || '#' || Cast(L.TSr as Varchar) || '#' || Cast(L.Sr as Varchar) As SearchCode, 
    '            H.SubCode As PartyCode, Sg.Name As Party,
    '            H.V_Type || '-' || H.ManualRefNo As StockReceiveNo, H.V_Date As StockReceiveDate, 
    '            Ic.Description As ItemCategory, Ig.Description As ItemGroup, I.Description As Item,
    '            D1.Description As Dimension1, D2.Description As Dimension2, 
    '            D3.Description As Dimension3, D4.Description As Dimension4,
    '            Size.Description As Size, VPendingStockProcess.BalanceQty As Qty, L.Unit,
    '            L.Item As SkuCode, Ic.Code As ItemCategoryCode, Ig.Code As ItemGroupCode, I.Code As ItemCode,
    '            D1.Code As Dimension1Code, D2.Code As Dimension2Code, 
    '            D3.Code As Dimension3Code, D4.Code As Dimension4Code,
    '            Size.Code As SizeCode, It.Code As ItemTypeCode, It.Name As ItemType, 
    '            VPendingStockProcess.StockProcess, VPendingStockProcess.StockProcesstsr, VPendingStockProcess.StockProcessSr,
    '            IfNull(Sku.SalesTaxPostingGroup,Ic.SalesTaxPostingGroup) As SalesTaxGroupItem,
    '            L.DealUnit, L.UnitMultiplier, L.UnitMultiplier * VPendingStockProcess.BalanceQty As DealQty,
    '            L.Barcode, Bc.Description As BarcodeDesc, L.Rate,
    '            L.ReferenceDocId, L.Referencetsr As ReferenceDocIdtsr, L.ReferenceDocIdSr
    '            FROM (" & bPendingStockProcessQry & ") As VPendingStockProcess
    '            LEFT JOIN StockProcess L On VPendingStockProcess.StockProcess = L.DocId 
    '                        And VPendingStockProcess.StockProcessTSr = L.TSr 
    '                        And VPendingStockProcess.StockProcessSr = L.Sr 
    '            LEFT JOIN StockHead H On L.StockProcess = H.DocId 
    '            LEFT JOIN ViewHelpSubgroup Sg On H.SubCode = Sg.Code
    '            LEFT JOIN Item Sku ON Sku.Code = L.Item
    '            LEFT JOIN Item I ON I.Code = IsNull(Sku.BaseItem,Sku.Code) And I.V_Type <> '" & ItemV_Type.SKU & "'
    '            LEFT JOIN ItemType It On Sku.ItemType = It.Code
    '            LEFT JOIN Item IC On Sku.ItemCategory = IC.Code
    '            LEFT JOIN Item IG On Sku.ItemGroup = IG.Code
    '            LEFT JOIN Item D1 ON D1.Code = Sku.Dimension1  
    '            LEFT JOIN Item D2 ON D2.Code = Sku.Dimension2
    '            LEFT JOIN Item D3 ON D3.Code = Sku.Dimension3
    '            LEFT JOIN Item D4 ON D4.Code = Sku.Dimension4
    '            LEFT JOIN Item Size ON Size.Code = Sku.Size 
    '            LEFT JOIN Barcode Bc On L.Barcode = Bc.Code
    '            Where 1=1 "

    '        DsReport = AgL.FillData(mQry, AgL.GCn)

    '        If DsReport.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

    '        ReportFrm.Text = "Invoice Wizard"
    '        ReportFrm.ClsRep = Me
    '        ReportFrm.ReportProcName = "ProcPurchaseInvoiceWizard"
    '        ReportFrm.AllowAutoResizeRows = False
    '        ReportFrm.InputColumnsStr = "|" + Col1Remark + "|"
    '        ReportFrm.ProcFillGrid(DsReport)

    '        For I As Integer = 0 To ReportFrm.DGL1.Rows.Count - 1
    '            FGetRateConsideringAllDimensions(I)
    '        Next

    '        ReportFrm.DGL1.Columns(Col1Rate).Visible = True

    '        ReportFrm.DGL1.Columns(Col1PartyCode).Visible = False
    '        ReportFrm.DGL1.Columns(Col1ItemType).Visible = False
    '        ReportFrm.DGL1.Columns(Col1ItemTypeCode).Visible = False
    '        ReportFrm.DGL1.Columns(Col1SkuCode).Visible = False
    '        ReportFrm.DGL1.Columns(Col1ItemCategoryCode).Visible = False
    '        ReportFrm.DGL1.Columns(Col1ItemGroupCode).Visible = False
    '        ReportFrm.DGL1.Columns(Col1Dimension1Code).Visible = False
    '        ReportFrm.DGL1.Columns(Col1Dimension2Code).Visible = False
    '        ReportFrm.DGL1.Columns(Col1Dimension3Code).Visible = False
    '        ReportFrm.DGL1.Columns(Col1Dimension4Code).Visible = False
    '        ReportFrm.DGL1.Columns(Col1SizeCode).Visible = False
    '        ReportFrm.DGL1.Columns(Col1StockProcess).Visible = False
    '        ReportFrm.DGL1.Columns(Col1StockProcessTSr).Visible = False
    '        ReportFrm.DGL1.Columns(Col1StockProcessSr).Visible = False
    '        ReportFrm.DGL1.Columns(Col1ReferenceDocId).Visible = False
    '        ReportFrm.DGL1.Columns(Col1ReferenceDocIdTSr).Visible = False
    '        ReportFrm.DGL1.Columns(Col1ReferenceDocIdSr).Visible = False
    '        ReportFrm.DGL1.Columns(Col1SalesTaxGroupItem).Visible = False

    '        ReportFrm.BtnProceed.Text = "Save"
    '        AgL.FSetDimensionCaptionForHorizontalGrid(ReportFrm.DGL1, AgL)
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '        DsReport = Nothing
    '    Finally
    '        For I As Integer = 0 To ReportFrm.DGL1.Columns.Count - 1
    '            ReportFrm.DGL2.Columns(I).Visible = ReportFrm.DGL1.Columns(I).Visible
    '            ReportFrm.DGL2.Columns(I).Width = ReportFrm.DGL1.Columns(I).Width
    '            ReportFrm.DGL2.Columns(I).DisplayIndex = ReportFrm.DGL1.Columns(I).DisplayIndex
    '        Next
    '    End Try
    'End Sub
    Private Sub ReportFrm_BtnProceedPressed() Handles ReportFrm.BtnProceedPressed
        Dim I As Integer = 0, J As Integer = 0, bSr As Integer = 0
        If FDataValidation() = False Then Exit Sub

        Dim bTempTable As String = "[" + Guid.NewGuid().ToString() + "]"

        If AgL.IsTableExist(bTempTable.Replace("[", "").Replace("]", ""), AgL.GCn) Then
            mQry = "Drop Table " + bTempTable
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If

        mQry = " CREATE TABLE " & bTempTable & "(Vendor nvarchar(10), Item nvarchar(10), SalesTaxGroupItem nvarchar(10), StockReceiveNo nvarchar(20), StockReceive nvarchar(21), StockReceiveSr Int, 
                    ReferenceDocId nvarchar(21), ReferenceDocIdtsr Int, ReferenceDocIdSr Int, Qty Float, Unit nvarchar(10), Rate Float) "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

        For I = 0 To ReportFrm.DGL1.Rows.Count - 1
            If ReportFrm.DGL1.Item(Col1Select, I).Value = "þ" Then
                mQry = " INSERT INTO " & bTempTable & "(Vendor, Item, SalesTaxGroupItem,
                        StockReceiveNo, StockReceive, StockReceiveSr, 
                        ReferenceDocId, ReferenceDocIdtsr, ReferenceDocIdSr, Qty, Unit, Rate)"
                mQry += " Select " & AgL.Chk_Text(AgL.XNull(ReportFrm.DGL1.Item(Col1PartyCode, I).Value)) & " As Vendor, 
                        " & AgL.Chk_Text(AgL.XNull(ReportFrm.DGL1.Item(Col1SkuCode, I).Value)) & " As Sku, 
                        " & AgL.Chk_Text(AgL.XNull(ReportFrm.DGL1.Item(Col1SalesTaxGroupItem, I).Value)) & " As SalesTaxGroupItem, 
                        " & AgL.Chk_Text(AgL.XNull(ReportFrm.DGL1.Item(Col1StockReceiveNo, I).Value)) & " As StockReceiveNo, 
                        " & AgL.Chk_Text(AgL.XNull(ReportFrm.DGL1.Item(Col1StockReceive, I).Value)) & " As StockReceive, 
                        " & AgL.VNull(ReportFrm.DGL1.Item(Col1StockReceiveSr, I).Value) & " As StockReceiveSr,
                        " & AgL.Chk_Text(AgL.XNull(ReportFrm.DGL1.Item(Col1ReferenceDocId, I).Value)) & " As ReferenceDocId, 
                        " & AgL.VNull(ReportFrm.DGL1.Item(Col1ReferenceTSr, I).Value) & " As ReferenceDocIdtsr, 
                        " & AgL.VNull(ReportFrm.DGL1.Item(Col1ReferenceSr, I).Value) & " As ReferenceDocIdSr,
                        " & AgL.VNull(ReportFrm.DGL1.Item(Col1Qty, I).Value) & " As Qty, 
                        " & AgL.Chk_Text(AgL.XNull(ReportFrm.DGL1.Item(Col1Unit, I).Value)) & " As Unit,
                        " & AgL.VNull(ReportFrm.DGL1.Item(Col1Rate, I).Value) & " As Rate "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
            End If
        Next


        mQry = " Select L.Vendor
                From " & bTempTable & " L 
                Group By L.Vendor "
        Dim DtPurchaseInvoice As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = " Select L.* From " & bTempTable & " L "
        Dim DtPurchaseInvoiceDetail As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)


        If AgL.IsTableExist(bTempTable.Replace("[", "").Replace("]", ""), AgL.GCn) Then
            mQry = "Drop Table " + bTempTable
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If


        Try
            Dim mTrans As String = ""
            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = "Begin"

            For I = 0 To DtPurchaseInvoice.Rows.Count - 1
                Dim Tot_Gross_Amount As Double = 0
                Dim Tot_Taxable_Amount As Double = 0
                Dim Tot_Tax1 As Double = 0
                Dim Tot_Tax2 As Double = 0
                Dim Tot_Tax3 As Double = 0
                Dim Tot_Tax4 As Double = 0
                Dim Tot_Tax5 As Double = 0
                Dim Tot_SubTotal1 As Double = 0


                Dim PurchInvoiceTableList(0) As FrmPurchInvoiceDirect_WithDimension.StructPurchInvoice
                Dim PurchInvoiceDimensionTableList(0) As FrmPurchInvoiceDirect_WithDimension.StructPurchInvoiceDimensionDetail
                Dim PurchInvoiceTable As New FrmPurchInvoiceDirect_WithDimension.StructPurchInvoice

                PurchInvoiceTable.DocID = ""
                PurchInvoiceTable.V_Type = mV_Type
                PurchInvoiceTable.V_Prefix = ""
                PurchInvoiceTable.Site_Code = AgL.PubSiteCode
                PurchInvoiceTable.Div_Code = AgL.PubDivCode
                PurchInvoiceTable.V_No = 0
                PurchInvoiceTable.V_Date = AgL.XNull(ReportFrm.FGetText(rowInvoiceDate))
                PurchInvoiceTable.ManualRefNo = AgL.XNull(ReportFrm.FGetText(rowInvoiceNo))
                PurchInvoiceTable.Process = AgL.XNull(ReportFrm.FGetCode(rowProcess))
                PurchInvoiceTable.AgentCode = ""
                PurchInvoiceTable.AgentName = ""
                PurchInvoiceTable.Vendor = AgL.XNull(DtPurchaseInvoice.Rows(I)("Vendor"))
                PurchInvoiceTable.VendorName = ""
                PurchInvoiceTable.BillToPartyCode = AgL.XNull(DtPurchaseInvoice.Rows(I)("Vendor"))
                PurchInvoiceTable.BillToPartyName = ""
                PurchInvoiceTable.VendorAddress = ""
                PurchInvoiceTable.VendorCity = ""
                PurchInvoiceTable.VendorMobile = ""

                PurchInvoiceTable.VendorSalesTaxNo = ""
                PurchInvoiceTable.SalesTaxGroupParty = ""
                PurchInvoiceTable.PlaceOfSupply = ""
                PurchInvoiceTable.StructureCode = ""
                PurchInvoiceTable.CustomFields = ""
                PurchInvoiceTable.VendorDocNo = ""
                PurchInvoiceTable.VendorDocDate = ""

                PurchInvoiceTable.ReferenceDocId = ""
                PurchInvoiceTable.Remarks = AgL.XNull(ReportFrm.FGetText(rowRemarks))
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

                Dim DtPurchInvoiceDetail_ForHeader As New DataTable
                For M As Integer = 0 To DtPurchaseInvoiceDetail.Columns.Count - 1
                    Dim DColumn As New DataColumn
                    DColumn.ColumnName = DtPurchaseInvoiceDetail.Columns(M).ColumnName
                    DtPurchInvoiceDetail_ForHeader.Columns.Add(DColumn)
                Next

                Dim DtRowPurchInvoiceDetail_ForHeader As DataRow() = DtPurchaseInvoiceDetail.Select("[Vendor] = " + AgL.Chk_Text(AgL.XNull(DtPurchaseInvoice.Rows(I)("Vendor"))))
                If DtRowPurchInvoiceDetail_ForHeader.Length > 0 Then
                    For M As Integer = 0 To DtRowPurchInvoiceDetail_ForHeader.Length - 1
                        DtPurchInvoiceDetail_ForHeader.Rows.Add()
                        For N As Integer = 0 To DtPurchInvoiceDetail_ForHeader.Columns.Count - 1
                            DtPurchInvoiceDetail_ForHeader.Rows(M)(N) = DtRowPurchInvoiceDetail_ForHeader(M)(N)
                        Next
                    Next
                End If

                For J = 0 To DtPurchInvoiceDetail_ForHeader.Rows.Count - 1
                    PurchInvoiceTable.Line_Sr = J + 1
                    PurchInvoiceTable.Line_ItemCode = AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Item"))
                    PurchInvoiceTable.Line_ItemName = ""
                    PurchInvoiceTable.Line_Specification = ""
                    PurchInvoiceTable.Line_SalesTaxGroupItem = AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("SalesTaxGroupItem"))
                    PurchInvoiceTable.Line_ReferenceNo = AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("StockReceiveNo"))
                    PurchInvoiceTable.Line_DocQty = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Qty"))
                    PurchInvoiceTable.Line_FreeQty = 0
                    PurchInvoiceTable.Line_Qty = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Qty"))
                    PurchInvoiceTable.Line_Unit = AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Unit"))
                    PurchInvoiceTable.Line_Pcs = 0

                    PurchInvoiceTable.Line_UnitMultiplier = 1
                    PurchInvoiceTable.Line_DealUnit = PurchInvoiceTable.Line_Unit
                    PurchInvoiceTable.Line_DocDealQty = PurchInvoiceTable.Line_Qty
                    PurchInvoiceTable.Line_Rate = AgL.VNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("Rate"))
                    PurchInvoiceTable.Line_DiscountPer = 0
                    PurchInvoiceTable.Line_DiscountAmount = 0
                    PurchInvoiceTable.Line_AdditionalDiscountPer = 0
                    PurchInvoiceTable.Line_AdditionalDiscountAmount = 0
                    PurchInvoiceTable.Line_Amount = PurchInvoiceTable.Line_DocQty * PurchInvoiceTable.Line_Rate
                    PurchInvoiceTable.Line_Remark = ""
                    PurchInvoiceTable.Line_BaleNo = ""
                    PurchInvoiceTable.Line_LotNo = ""
                    PurchInvoiceTable.Line_ReferenceDocId = AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("StockReceive"))
                    PurchInvoiceTable.Line_ReferenceTSr = AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("StockReceiveSr"))
                    PurchInvoiceTable.Line_ReferenceSr = AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("StockReceiveSr"))
                    PurchInvoiceTable.Line_PurchInvoice = AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("ReferenceDocId"))
                    PurchInvoiceTable.Line_PurchInvoiceSr = AgL.XNull(DtPurchInvoiceDetail_ForHeader.Rows(J)("ReferenceDocIdtsr"))
                    PurchInvoiceTable.Line_GrossWeight = 0
                    PurchInvoiceTable.Line_NetWeight = 0
                    PurchInvoiceTable.Line_Gross_Amount = PurchInvoiceTable.Line_Amount
                    PurchInvoiceTable.Line_Taxable_Amount = PurchInvoiceTable.Line_Amount
                    PurchInvoiceTable.Line_Tax1_Per = 0
                    PurchInvoiceTable.Line_Tax1 = 0
                    PurchInvoiceTable.Line_Tax2_Per = 0
                    PurchInvoiceTable.Line_Tax2 = 0
                    PurchInvoiceTable.Line_Tax3_Per = 0
                    PurchInvoiceTable.Line_Tax3 = 0
                    PurchInvoiceTable.Line_Tax4_Per = 0
                    PurchInvoiceTable.Line_Tax4 = 0
                    PurchInvoiceTable.Line_Tax5_Per = 0
                    PurchInvoiceTable.Line_Tax5 = 0
                    PurchInvoiceTable.Line_SubTotal1 = PurchInvoiceTable.Line_Amount


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
                    If Val(PurchInvoiceTableList(J).Line_Gross_Amount) > 0 Then
                        PurchInvoiceTableList(J).Line_Round_Off = Math.Round(PurchInvoiceTableList(0).Round_Off * PurchInvoiceTableList(J).Line_Gross_Amount / PurchInvoiceTableList(0).Gross_Amount, 2)
                        PurchInvoiceTableList(J).Line_Net_Amount = Math.Round(PurchInvoiceTableList(0).Net_Amount * PurchInvoiceTableList(J).Line_Gross_Amount / PurchInvoiceTableList(0).Gross_Amount, 2)
                        Tot_RoundOff += PurchInvoiceTableList(J).Line_Round_Off
                        Tot_NetAmount += PurchInvoiceTableList(J).Line_Net_Amount
                    End If
                Next

                Tot_RoundOff = Math.Round(Tot_RoundOff, 2)

                If Tot_RoundOff <> PurchInvoiceTableList(0).Round_Off Then
                    PurchInvoiceTableList(0).Line_Round_Off = PurchInvoiceTableList(0).Line_Round_Off + (PurchInvoiceTableList(0).Round_Off - Tot_RoundOff)
                End If

                If Tot_NetAmount <> PurchInvoiceTableList(0).Net_Amount Then
                    PurchInvoiceTableList(0).Line_Net_Amount = PurchInvoiceTableList(0).Line_Net_Amount + (PurchInvoiceTableList(0).Net_Amount - Tot_NetAmount)
                End If

                Dim bDocId As String = FrmPurchInvoiceDirect_WithDimension.InsertPurchInvoice(PurchInvoiceTableList)

                mQry = " Delete From PurchInvoiceDetailSku Where DocId = '" & bDocId & "' "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                mQry = " INSERT INTO PurchInvoiceDetailSku (DocID, Sr, ItemCategory, ItemGroup, Item, Dimension1, Dimension2, Dimension3, Dimension4, Size)
                        SELECT L.DocID, L.Sr, Sku.ItemCategory, Sku.ItemGroup, Sku.BaseItem AS Item, Sku.Dimension1, Sku.Dimension2, Sku.Dimension3, Sku.Dimension4, Sku.Size
                        FROM PurchInvoiceDetail L With (NoLock)
                        LEFT JOIN Item Sku With (NoLock) ON L.Item = Sku.Code
                        WHERE L.DocID = '" & bDocId & "' "
                AgL.Dman_ExecuteNonQry(mQry, IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead), AgL.ECmd)
            Next

            AgL.ETrans.Commit()
            mTrans = "Commit"
            MsgBox("Process Completed...!", MsgBoxStyle.Information)
            ReportFrm.DGL1.DataSource = Nothing

            Try
                ObjFrm.FRefreshMovRec()
            Catch ex As Exception
            End Try
        Catch ex As Exception
            AgL.ETrans.Rollback()
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Function FDataValidation() As Boolean
        FDataValidation = False

        If AgL.XNull(ReportFrm.FGetText(rowInvoiceNo)).ToString() = "" Then
            MsgBox("Invoice No is required...!", MsgBoxStyle.Information)
            Exit Function
        End If

        Dim bTableName_StockReceive As String = "[" + Guid.NewGuid().ToString() + "]"

        If AgL.IsTableExist(bTableName_StockReceive.Replace("[", "").Replace("]", ""), AgL.GCn) Then
            mQry = "Drop Table " + bTableName_StockReceive
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If

        mQry = " CREATE TABLE " & bTableName_StockReceive & "(StockReceive nvarchar(21), StockReceiveSr Int, Qty Float, RowIndex Int) "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

        For I As Integer = 0 To ReportFrm.DGL1.Rows.Count - 1
            If ReportFrm.DGL1.Item(Col1Select, I).Value = "þ" Then
                mQry = " INSERT INTO " & bTableName_StockReceive & "(StockReceive, StockReceiveSr, Qty, RowIndex)"
                mQry += " Select " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1StockReceive, I).Value) & " As StockReceive, 
                            " & Val(ReportFrm.DGL1.Item(Col1StockReceiveSr, I).Value) & " As StockReceiveSr,
                            " & Val(ReportFrm.DGL1.Item(Col1Qty, I).Value) & " As Qty,
                            " & I + 1 & " As RowIndex "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
            End If
        Next

        Dim bPendingReceiveQry As String = " SELECT VReceive.StockReceive, VReceive.StockReceiveSr, IsNull(VReceive.ReceiveQty,0) - IsNull(VInvoice.InvoiceQty,0) AS BalanceQty
                FROM (
                    SELECT L.DocId As StockReceive, L.Sr As StockReceiveSr, Sum(L.Qty) AS ReceiveQty
                    FROM PurchInvoice H 
                    LEFT JOIN PurchInvoiceDetail L ON H.DocID = L.DocID
                    LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type
                    And Vt.Nature = 'Receive'
                    And L.SubRecordType Is Null
	                GROUP BY L.DocId, L.Sr
                ) AS VReceive
                LEFT JOIN (
                    SELECT L.ReferenceDocId, L.ReferenceTSr, Sum(L.Qty) AS InvoiceQty
                    FROM PurchBillDetail L 
                    GROUP BY L.ReferenceDocId, L.ReferenceTSr	
                ) AS VInvoice ON VReceive.StockReceive = VInvoice.ReferenceDocId AND VReceive.StockReceiveSr = VInvoice.ReferenceTSr 
                WHERE 1=1 
                And IsNull(VReceive.ReceiveQty,0) - IsNull(VInvoice.InvoiceQty,0) > 0 "

        If AgL.FillData(" Select * From " & bTableName_StockReceive & "", AgL.GCn).Tables(0).Rows.Count > 0 Then
            mQry = " Select Temp.RowIndex, Temp.Qty, VStockReceive.BalanceQty
                From (
                    SELECT IfNull(L.StockReceive,'') As StockReceive, 
                    IfNull(L.StockReceiveSr,0) As StockReceiveSr, 
                    Max(L.RowIndex) As RowIndex,
                    IsNull(Sum(L.Qty),0) AS Qty
                    FROM " & bTableName_StockReceive & " L 
                    GROUP BY L.StockReceive, L.StockReceiveSr) As Temp
                LEFT JOIN (" & bPendingReceiveQry & ") As VStockReceive On Temp.StockReceive = VStockReceive.StockReceive
                        And Temp.StockReceiveSr = VStockReceive.StockReceiveSr 
                Where IfNull(Temp.Qty,0) > IfNull(VStockReceive.BalanceQty,0)"
            Dim DtStockReceiveBalance As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

            If DtStockReceiveBalance.Rows.Count > 0 Then
                MsgBox("Qty entered is greater then balance qty at row number " & DtStockReceiveBalance.Rows(0)("RowIndex") & "...!", MsgBoxStyle.Information)
                Exit Function
            End If
        End If

        If AgL.IsTableExist(bTableName_StockReceive.Replace("[", "").Replace("]", ""), AgL.GCn) Then
            mQry = "Drop Table " + bTableName_StockReceive
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If

        FDataValidation = True
    End Function
    Private Sub FGetRateConsideringAllDimensions(mRowIndex As Integer)
        If FDivisionNameForCustomization(14) = "PRATHAM APPARE" Or
            FDivisionNameForCustomization(15) = "AGARWAL UNIFORM" Then
            ReportFrm.DGL1.Item(Col1Rate, mRowIndex).Value = ClsMain.FGetRateWithRatePattern("", AgL.XNull(ReportFrm.DGL1.Item(Col1PartyCode, mRowIndex).Value), AgL.XNull(ReportFrm.DGL1.Item(Col1SettingGroupCode, mRowIndex).Value), AgL.XNull(ReportFrm.DGL1.Item(Col1DivCode, mRowIndex).Value), AgL.XNull(ReportFrm.DGL1.Item(Col1SiteCode, mRowIndex).Value), AgL.XNull(ReportFrm.FGetCode(rowProcess)), AgL.XNull(ReportFrm.DGL1.Item(Col1VoucherType, mRowIndex).Value), AgL.XNull(ReportFrm.DGL1.Item(Col1RateTypeCode, mRowIndex).Value),
                    AgL.XNull(ReportFrm.DGL1.Item(Col1ItemCategoryCode, mRowIndex).Value),
                    AgL.XNull(ReportFrm.DGL1.Item(Col1ItemGroupCode, mRowIndex).Value),
                    AgL.XNull(ReportFrm.DGL1.Item(Col1ItemCode, mRowIndex).Value),
                    AgL.XNull(ReportFrm.DGL1.Item(Col1Dimension1Code, mRowIndex).Value),
                    AgL.XNull(ReportFrm.DGL1.Item(Col1Dimension2Code, mRowIndex).Value),
                    AgL.XNull(ReportFrm.DGL1.Item(Col1Dimension3Code, mRowIndex).Value),
                    AgL.XNull(ReportFrm.DGL1.Item(Col1Dimension4Code, mRowIndex).Value),
                    AgL.XNull(ReportFrm.DGL1.Item(Col1SizeCode, mRowIndex).Value))

            If Val(ReportFrm.DGL1.Item(Col1Rate, mRowIndex).Value) > 0 Then
                ReportFrm.DGL1.Item(Col1Rate, mRowIndex).Value = Val(ReportFrm.DGL1.Item(Col1Rate, mRowIndex).Value) +
                    ClsMain.FGetRateWithRatePattern(RateCategory.RateAddition, AgL.XNull(ReportFrm.DGL1.Item(Col1PartyCode, mRowIndex).Value), AgL.XNull(ReportFrm.DGL1.Item(Col1SettingGroupCode, mRowIndex).Value), AgL.XNull(ReportFrm.DGL1.Item(Col1DivCode, mRowIndex).Value), AgL.XNull(ReportFrm.DGL1.Item(Col1SiteCode, mRowIndex).Value), AgL.XNull(ReportFrm.FGetCode(rowProcess)), AgL.XNull(ReportFrm.DGL1.Item(Col1VoucherType, mRowIndex).Value), AgL.XNull(ReportFrm.DGL1.Item(Col1RateTypeCode, mRowIndex).Value),
                        AgL.XNull(ReportFrm.DGL1.Item(Col1ItemCategoryCode, mRowIndex).Value),
                        AgL.XNull(ReportFrm.DGL1.Item(Col1ItemGroupCode, mRowIndex).Value),
                        AgL.XNull(ReportFrm.DGL1.Item(Col1ItemCode, mRowIndex).Value),
                        AgL.XNull(ReportFrm.DGL1.Item(Col1Dimension1Code, mRowIndex).Value),
                        AgL.XNull(ReportFrm.DGL1.Item(Col1Dimension2Code, mRowIndex).Value),
                        AgL.XNull(ReportFrm.DGL1.Item(Col1Dimension3Code, mRowIndex).Value),
                        AgL.XNull(ReportFrm.DGL1.Item(Col1Dimension4Code, mRowIndex).Value),
                        AgL.XNull(ReportFrm.DGL1.Item(Col1SizeCode, mRowIndex).Value))
            End If
        End If
    End Sub
    Public Sub ProcPurchaseInvoiceWizard(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try
            Dim mCondStr$ = ""
            Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"

            RepTitle = "Invoice Wizard"

            If mFilterGrid IsNot Nothing And mGridRow IsNot Nothing Then
                If mGridRow.DataGridView.Columns.Contains("Search Code") = True Then
                    ClsMain.FOpenForm(mGridRow.Cells("Search Code").Value, ReportFrm)
                    ReportFrm.FiterGridCopy_Arr.RemoveAt(ReportFrm.FiterGridCopy_Arr.Count - 1)
                    Exit Sub
                Else
                    Exit Sub
                End If
            End If

            If AgL.XNull(ReportFrm.FGetText(rowProcess)) = "" Then
                MsgBox("Please select process first.", MsgBoxStyle.Information)
                Exit Sub
            End If

            mCondStr = mCondStr & " AND Date(H.V_Date) Between " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowFromDate)).ToString("s")) & " And " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowToDate)).ToString("s")) & " "
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.Vendor", rowVendor)
            'mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Process", rowProcess)
            mCondStr = mCondStr & " And H.Process = " & AgL.Chk_Text(ReportFrm.FGetCode(rowProcess)) & ""

            Dim bPendingReceiveQry = ""
            bPendingReceiveQry = " SELECT VReceive.StockReceive, VReceive.StockReceiveSr, IsNull(VReceive.ReceiveQty,0) - IsNull(VInvoice.InvoiceQty,0) AS BalanceQty
                FROM (
                    SELECT L.DocId As StockReceive, L.Sr As StockReceiveSr, Sum(L.Qty) AS ReceiveQty
                    FROM PurchInvoice H 
                    LEFT JOIN PurchInvoiceDetail L ON H.DocID = L.DocID
                    LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type
                    Where 1 = 1 " & mCondStr &
                    " And Vt.Nature = 'Receive'
                    And L.SubRecordType Is Null
                    GROUP BY L.DocId, L.Sr
                ) AS VReceive
                LEFT JOIN (
                    SELECT L.ReferenceDocId, L.ReferenceTSr, Sum(L.Qty) AS InvoiceQty
                    FROM PurchBillDetail L 
                    GROUP BY L.ReferenceDocId, L.ReferenceTSr	
                ) AS VInvoice ON VReceive.StockReceive = VInvoice.ReferenceDocId AND VReceive.StockReceiveSr = VInvoice.ReferenceTSr 
                WHERE 1=1 
                And IsNull(VReceive.ReceiveQty,0) - IsNull(VInvoice.InvoiceQty,0) > 0 "

            mQry = " Select 'o' As Tick, L.DocID || '#' || Cast(L.Sr as Varchar) As SearchCode, 
                H.Vendor As PartyCode, Sg.Name As Party,
                H.V_Type || '-' || H.ManualRefNo As StockReceiveNo, H.V_Date As StockReceiveDate, 
                Ic.Description As ItemCategory, Ig.Description As ItemGroup, I.Description As Item,
                D1.Description As Dimension1, D2.Description As Dimension2, 
                D3.Description As Dimension3, D4.Description As Dimension4,
                Size.Description As Size, VPendingReceive.BalanceQty As Qty, L.Unit, L.Rate,
                H.V_type as VoucherType, H.SettingGroup as SettingGroupCode, H.Div_Code, H.Site_Code, H.RateType as RateTypeCode, L.Item As SkuCode, Ic.Code As ItemCategoryCode, Ig.Code As ItemGroupCode, I.Code As ItemCode,
                D1.Code As Dimension1Code, D2.Code As Dimension2Code, 
                D3.Code As Dimension3Code, D4.Code As Dimension4Code,
                Size.Code As SizeCode, It.Code As ItemTypeCode, It.Name As ItemType, 
                VPendingReceive.StockReceive, VPendingReceive.StockReceiveSr, 
                L.SalesTaxGroupItem, L.ReferenceDocId, L.Referencetsr, L.Referencesr
                FROM (" & bPendingReceiveQry & ") As VPendingReceive
                LEFT JOIN PurchInvoiceDetail L On VPendingReceive.StockReceive = L.DocId And VPendingReceive.StockReceiveSr = L.Sr 
                LEFT JOIN PurchInvoice H On L.DocId = H.DocId 
                LEFT JOIN SubGroup Sg On H.Vendor = Sg.SubCode
                LEFT JOIN Item Sku ON Sku.Code = L.Item
                LEFT JOIN Item I ON I.Code = IsNull(Sku.BaseItem,Sku.Code) And I.V_Type <> '" & ItemV_Type.SKU & "'
                LEFT JOIN ItemType It On Sku.ItemType = It.Code
                LEFT JOIN Item IC On Sku.ItemCategory = IC.Code
                LEFT JOIN Item IG On Sku.ItemGroup = IG.Code
                LEFT JOIN Item D1 ON D1.Code = Sku.Dimension1  
                LEFT JOIN Item D2 ON D2.Code = Sku.Dimension2
                LEFT JOIN Item D3 ON D3.Code = Sku.Dimension3
                LEFT JOIN Item D4 ON D4.Code = Sku.Dimension4
                LEFT JOIN Item Size ON Size.Code = Sku.Size 
                Where 1=1 
                Order By StockReceiveDate, StockReceiveNo "

            DsReport = AgL.FillData(mQry, AgL.GCn)

            If DsReport.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            ReportFrm.Text = "Invoice Wizard"
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = "ProcPurchaseInvoiceWizard"
            ReportFrm.AllowAutoResizeRows = False
            ReportFrm.InputColumnsStr = "|" + Col1Remark + "|"
            ReportFrm.ProcFillGrid(DsReport)

            For I As Integer = 0 To ReportFrm.DGL1.Rows.Count - 1
                FGetRateConsideringAllDimensions(I)
            Next

            ReportFrm.DGL1.Columns(Col1Rate).Visible = True

            ReportFrm.DGL1.Columns(Col1SettingGroupCode).Visible = False
            ReportFrm.DGL1.Columns(Col1DivCode).Visible = False
            ReportFrm.DGL1.Columns(Col1SiteCode).Visible = False
            ReportFrm.DGL1.Columns(Col1VoucherType).Visible = False
            ReportFrm.DGL1.Columns(Col1RateTypeCode).Visible = False
            ReportFrm.DGL1.Columns(Col1PartyCode).Visible = False
            ReportFrm.DGL1.Columns(Col1ItemType).Visible = False
            ReportFrm.DGL1.Columns(Col1ItemTypeCode).Visible = False
            ReportFrm.DGL1.Columns(Col1SkuCode).Visible = False
            ReportFrm.DGL1.Columns(Col1ItemCategoryCode).Visible = False
            ReportFrm.DGL1.Columns(Col1ItemGroupCode).Visible = False
            ReportFrm.DGL1.Columns(Col1Dimension1Code).Visible = False
            ReportFrm.DGL1.Columns(Col1Dimension2Code).Visible = False
            ReportFrm.DGL1.Columns(Col1Dimension3Code).Visible = False
            ReportFrm.DGL1.Columns(Col1Dimension4Code).Visible = False
            ReportFrm.DGL1.Columns(Col1SizeCode).Visible = False
            ReportFrm.DGL1.Columns(Col1StockReceive).Visible = False
            ReportFrm.DGL1.Columns(Col1StockReceiveSr).Visible = False
            ReportFrm.DGL1.Columns(Col1ReferenceDocId).Visible = False
            ReportFrm.DGL1.Columns(Col1ReferenceTSr).Visible = False
            ReportFrm.DGL1.Columns(Col1ReferenceSr).Visible = False
            ReportFrm.DGL1.Columns(Col1SalesTaxGroupItem).Visible = False

            ReportFrm.BtnProceed.Text = "Save"
            AgL.FSetDimensionCaptionForHorizontalGrid(ReportFrm.DGL1, AgL)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsReport = Nothing
        Finally
            For I As Integer = 0 To ReportFrm.DGL1.Columns.Count - 1
                ReportFrm.DGL2.Columns(I).Visible = ReportFrm.DGL1.Columns(I).Visible
                ReportFrm.DGL2.Columns(I).Width = ReportFrm.DGL1.Columns(I).Width
                ReportFrm.DGL2.Columns(I).DisplayIndex = ReportFrm.DGL1.Columns(I).DisplayIndex
            Next
        End Try
    End Sub







End Class
