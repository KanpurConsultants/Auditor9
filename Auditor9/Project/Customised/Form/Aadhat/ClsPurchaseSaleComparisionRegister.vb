Imports System.IO
Imports AgLibrary
Imports AgLibrary.ClsMain
Imports AgLibrary.ClsMain.agConstants
Imports Microsoft.Reporting.WinForms
Public Class ClsPurchaseSaleComparisonRegister

    Dim StrArr1() As String = Nothing, StrArr2() As String = Nothing, StrArr3() As String = Nothing, StrArr4() As String = Nothing, StrArr5() As String = Nothing

    Dim mGRepFormName As String = ""
    Dim mQry As String = ""
    Dim RepTitle As String = ""
    Dim EntryNCat As String = ""


    Dim DsReport As DataSet = New DataSet
    Dim DTReport As DataTable = New DataTable
    Dim IntLevel As Int16 = 0

    Dim WithEvents ReportFrm As FrmRepDisplay
    Public Const GFilter As Byte = 2
    Public Const GFilterCode As Byte = 4


    Dim mShowReportType As String = ""
    Dim mReportDefaultText$ = ""

    Dim DsHeader As DataSet = Nothing

    Dim rowReportType As Integer = 0
    Dim rowFromDate As Integer = 1
    Dim rowToDate As Integer = 2
    Dim rowSite As Integer = 3
    Dim rowDivision As Integer = 4
    Public Property GRepFormName() As String
        Get
            GRepFormName = mGRepFormName
        End Get
        Set(ByVal value As String)
            mGRepFormName = value
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

    Dim mHelpSiteQry$ = "Select 'o' As Tick, Code, Name FROM SiteMast "
    Dim mHelpDivisionQry$ = "Select 'o' As Tick, Div_Code As Code, Div_Name As Name From Division "
    Dim mHelpYesNoQry$ = " Select 'Yes' As Code, 'Yes' AS [Value] Union All Select 'No' As Code, 'No' AS [Value] "
    Dim mHelpPartyQry$ = " Select 'o' As Tick,  Sg.SubCode As Code, Sg.DispName || ',' ||  City.CityName AS Party, Sg.Address FROM SubGroup Sg Left Join City On Sg.CityCode = City.CityCode Where Sg.Nature In ('Customer','Supplier','Cash') "
    Dim mHelpCityQry$ = "Select 'o' As Tick, CityCode, CityName From City "
    Dim mHelpStateQry$ = "Select 'o' As Tick, Code, Description From State "
    Dim mHelpItemQry$ = "Select 'o' As Tick, Code, Description As [Item] From Item Where V_Type = '" & ItemV_Type.Item & "'"
    Dim mHelpPurchaseAgentQry$ = " Select 'o' As Tick, Sg.Code, Sg.Name AS Agent FROM viewHelpSubgroup Sg Where Sg.SubgroupType = '" & SubgroupType.PurchaseAgent & "' "
    Dim mHelpItemGroupQry$ = "Select 'o' As Tick, Code, Description As [Item Group] From ItemGroup "
    Dim mHelpItemCategoryQry$ = "Select 'o' As Tick, Code, Description As [Item Category] From ItemCategory "
    Dim mHelpItemTypeQry$ = "Select 'o' As Tick, Code, Name FROM ItemType "
    Dim mHelpLocationQry$ = " Select 'o' As Tick,  Sg.Code, Sg.Name AS Party FROM viewHelpSubGroup Sg Where Sg.Nature In ('Supplier','Stock') "
    Dim mHelpTagQry$ = "Select Distinct 'o' As Tick, H.Tags as Code, H.Tags as Description  FROM PurchInvoiceDetail H "
    Dim mHelpSalesAgentQry$ = " Select 'o' As Tick, Sg.Code, Sg.Name AS [Responsible Person] FROM viewHelpSubgroup Sg Where Sg.SubgroupType = '" & SubgroupType.SalesAgent & "' "
    Public Sub Ini_Grid()
        Try
            mQry = "Select 'Comparison' as Code, 'Comparison' as Name 
                    Union All Select 'Difference' as Code, 'Difference' as Name 
                    Union All Select 'Inconsistency' as Code, 'Inconsistency' as Name "
            ReportFrm.CreateHelpGrid("Report Type", "Report Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mQry, "Comparison")
            ReportFrm.CreateHelpGrid("FromDate", "From Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubStartDate)
            ReportFrm.CreateHelpGrid("ToDate", "To Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubEndDate)
            ReportFrm.CreateHelpGrid("Site", "Site", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSiteQry, "[SITECODE]")
            ReportFrm.CreateHelpGrid("Division", "Division", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpDivisionQry, "[DIVISIONCODE]")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub ObjRepFormGlobal_ProcessReport() Handles ReportFrm.ProcessReport
        ProcPurchaseSaleComparisonReport()
    End Sub
    Public Sub New(ByVal mReportFrm As FrmRepDisplay)
        ReportFrm = mReportFrm
    End Sub
    Public Sub ProcPurchaseSaleComparisonReport(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try
            Dim mPurchCondStr$ = "", mSaleCondStr$ = ""
            Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"


            mPurchCondStr = " Where 1=1"
            mPurchCondStr = mPurchCondStr & " AND Vt.NCat = '" & Ncat.PurchaseInvoice & "' "
            mPurchCondStr = mPurchCondStr & " AND Date(H.VendorDocDate) Between " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowFromDate)).ToString("s")) & " And " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowToDate)).ToString("s")) & " "
            mPurchCondStr = mPurchCondStr & Replace(ReportFrm.GetWhereCondition("H.Site_Code", rowSite), "''", "'")
            mPurchCondStr = mPurchCondStr & Replace(ReportFrm.GetWhereCondition("H.Div_Code", rowDivision), "''", "'")


            mSaleCondStr = " Where 1=1"
            mSaleCondStr = mSaleCondStr & " AND Vt.NCat = '" & Ncat.SaleInvoice & "' "
            mSaleCondStr = mSaleCondStr & " AND Date(H.V_Date) Between " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowFromDate)).ToString("s")) & " And " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowToDate)).ToString("s")) & " "
            mSaleCondStr = mSaleCondStr & Replace(ReportFrm.GetWhereCondition("H.Site_Code", rowSite), "''", "'")
            mSaleCondStr = mSaleCondStr & Replace(ReportFrm.GetWhereCondition("H.Div_Code", rowDivision), "''", "'")

            If ReportFrm.FGetText(rowReportType) = "Comparison" Then
                mQry = " SELECT V1.SaleInvoiceDocId As SearchCode, 
                        Max(V1.PurchInvoiceNo) AS PurchInvoiceNo, 
                        Max(strftime('%d/%m/%Y', V1.PartyDocDate)) AS PartyDocDate, 
                        Max(V1.PartyDocNo) AS PartyDocNo, 
                        Max(V1.VendorName) AS VendorName, Max(V1.Amount) AS Amount, 
                        Max(V1.SaleInvoiceNo) AS SaleInvoiceNo, 
                        Max(strftime('%d/%m/%Y', V1.SaleInvoiceDate)) AS SaleInvoiceDate, 
                        Max(V1.SaleToPartyName) AS SaleToPartyName
                        FROM (
	                        SELECT H.GenDocId AS SaleInvoiceDocId, H.ManualRefNo AS PurchInvoiceNo, H.VendorDocDate AS PartyDocDate, H.VendorDocNo AS PartyDocNo, H.VendorName, H.Net_Amount AS Amount, 
	                        NULL AS SaleInvoiceNo, NULL AS SaleInvoiceDate, NULL AS SaleToPartyName
	                        FROM PurchInvoice H 
                            LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type " & mPurchCondStr &
                            " UNION ALL 
	                        SELECT H.DocID AS SaleInvoiceDocId, NULL AS PurchInvoiceNo, NULL AS PartyDocDate, NULL AS PartyDocNo, NULL AS VendorName, 0 AS Amount, 
	                        H.ManualRefNo AS SaleInvoiceNo, H.V_Date AS SaleInvoiceDate, H.SaleToPartyName
	                        FROM SaleInvoice H 
                            LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type " & mSaleCondStr &
                        " ) AS V1
                        GROUP BY V1.SaleInvoiceDocId "
            ElseIf ReportFrm.FGetText(rowReportType) = "Difference" Then
                mQry = "SELECT VSale.DocID as SearchCode, VSale.SaleBillNo, VSale.SaleBillDate, VSale.Buyer, VSale.SaleAmount,
                    VPurch.PurchEntryNo, VPurch.PurchBillDate, VPurch.PurchBillNo, VPurch.Supplier, IfNull(VPurch.PurchAmount,0.00) As PurchAmount
                    FROM (
	                    SELECT H.DocID, H.ManualRefNo AS SaleBillNo, H.V_Date AS SaleBillDate, Sg.Name AS Buyer, VLine.Amount AS SaleAmount
	                    FROM SaleInvoice H 
	                    LEFT JOIN (
		                    SELECT L.DocID, Round(Sum(L.Amount + L.DiscountAmount + L.AdditionalDiscountAmount + L.ExtraDiscountAmount  - L.AdditionAmount),2) AS Amount
		                    FROM SaleInvoiceDetail L 
		                    GROUP BY L.DocID
	                    ) AS VLine ON H.DocId = VLine.DocId
                        LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type 
	                    LEFT JOIN ViewHelpSubgroup Sg On H.BillToParty = Sg.Code " & mSaleCondStr &
                    " ) AS VSale
                    LEFT JOIN (
	                    SELECT IfNull(H.GenDocId, H.DocID) as DocID, H.ManualRefNo AS PurchEntryNo, H.VendorDocDate AS PurchBillDate, H.VendorDocNo AS PurchBillNo, 
	                    Sg.Name AS Supplier, VLine.Amount AS PurchAmount
	                    FROM PurchInvoice H 
	                    LEFT JOIN (
		                    SELECT IfNull(H1.GenDocID,L.DocId) as DocID, Round(Sum(L.Amount + L.DiscountAmount + L.AdditionalDiscountAmount - L.AdditionAmount),2) AS Amount
		                    FROM PurchInvoiceDetail L 
                            Left Join PurchInvoice H1 On L.DocID = H1.DocID
		                    GROUP BY IfNull(H1.GenDocID,L.DocId)
	                    ) AS VLine ON IfNull(H.GenDocId, H.DocID) = VLine.DocId
                        LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type 
	                    LEFT JOIN ViewHelpSubgroup Sg ON H.BillToParty = Sg.Code " & mPurchCondStr &
                    " ) AS VPurch ON VSale.DocId = VPurch.DocId
                    WHERE Round(IfNull(VSale.SaleAmount,0),0) <> Round(IfNull(VPurch.PurchAmount,0),0)"
            ElseIf ReportFrm.FGetText(rowReportType) = "Inconsistency" Then
                mQry = "SELECT H.DocID as SearchCode, H.ManualRefNo AS PurchEntryNo, H.VendorDocDate AS BillDate, H.VendorDocNo AS BillNo, 
                        Sg.Name AS SupplierName, H.Net_Amount AS BillAmount
                        FROM PurchInvoice H 
                        LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type 
                        LEFT JOIN ViewHelpSubgroup Sg ON H.BillToParty = Sg.Code " & mPurchCondStr &
                        " And H.GenDocId IS NULL "
            End If
            DsHeader = AgL.FillData(mQry, AgL.GCn)

            If DsHeader.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            ReportFrm.Text = "Purchase Sale Comparison Report - " + ReportFrm.FGetText(rowReportType)
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = "ProcPurchaseSaleComparisonReport"
            ReportFrm.ProcFillGrid(DsHeader)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsHeader = Nothing
        End Try
    End Sub
End Class
