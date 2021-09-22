Imports System.Data.SqlClient
Imports System.Data.SQLite
Imports System.IO
Imports AgLibrary.ClsMain
Imports AgLibrary.ClsMain.agConstants
Public Class ClsPartyWiseItemWiseOutstandingReport
    Dim StrArr1() As String = Nothing, StrArr2() As String = Nothing, StrArr3() As String = Nothing, StrArr4() As String = Nothing, StrArr5() As String = Nothing

    Dim mGRepFormName As String = ""
    Dim ErrorLog As String = ""

    Dim rowFromDate As Integer = 0
    Dim rowToDate As Integer = 1
    Dim rowParty As Integer = 2
    Dim rowItem As Integer = 3
    Dim rowStatus As Integer = 4
    Dim rowDivision As Integer = 5
    Dim rowSite As Integer = 6

    'Dim WithEvents ReportFrm As Aglibrary.FrmReportLayout
    Dim WithEvents ReportFrm As AgLibrary.FrmReportLayout
    Public Property GRepFormName() As String
        Get
            GRepFormName = mGRepFormName
        End Get
        Set(ByVal value As String)
            mGRepFormName = value
        End Set
    End Property

    Dim mHelpSiteQry$ = "Select 'o' As Tick, Code, Name As [Site] From SiteMast Where  Code In (" & AgL.PubSiteList & ")  "
    Dim mHelpDivisionQry$ = "Select 'o' As Tick, Div_Code as Code, Div_Name As [Division] From Division "
    Dim mHelpPartyQry$ = " Select 'o' As Tick,  Sg.Code, Sg.Name AS Party, Sg.Address, Ag.GroupName FROM viewHelpSubGroup Sg  Left Join AcGroup Ag On Sg.groupCode= ag.GroupCode Where Sg.Nature In ('Customer') Order By Name "
    Dim mHelpItemQry$ = "Select 'o' As Tick, Code, Description As [Item] From Item Where V_Type = '" & ItemV_Type.Item & "'"
    Dim mHelpYesNo$ = " Select 'Yes' As Code, 'Yes' AS [Value] Union All Select 'No' As Code, 'No' AS [Value] "

    Dim DsRep As DataSet = Nothing, DsRep1 As DataSet = Nothing, DsRep2 As DataSet = Nothing
    Dim mQry$ = "", RepName$ = "", RepTitle$ = "", OrderByStr$ = ""

    Dim StrMonth$ = ""
    Dim StrQuarter$ = ""
    Dim StrFinancialYear$ = ""
    Dim StrTaxPeriod$ = ""
    Public Sub Ini_Grid()
        Try
            ReportFrm.CreateHelpGrid("From Date", "From Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.PubStartDate)
            ReportFrm.CreateHelpGrid("To Date", "To Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.PubLoginDate)
            ReportFrm.CreateHelpGrid("Party", "Party", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpPartyQry, , 450, 850, 300)
            ReportFrm.CreateHelpGrid("Item", "Item", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemQry, , 450, 650, 300)
            mQry = "Select 'Balance' as Code, 'Balance' as Name 
                    Union All Select 'All' as Code, 'All' as Name "
            ReportFrm.CreateHelpGrid("Status", "Status", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.SingleSelection, mQry, "Balance")
            ReportFrm.CreateHelpGrid("Division", "Division", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpDivisionQry, "[DIVISIONCODE]")
            ReportFrm.CreateHelpGrid("Site", "Site", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpSiteQry, "[SITECODE]")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Function FGetVoucher_TypeQry(ByVal TableName As String) As String
        FGetVoucher_TypeQry = " SELECT Distinct 'o' As Tick, H.V_Type AS Code, Vt.Description AS [Voucher Type] " &
                                " FROM " & TableName & " H  " &
                                " LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type "
    End Function
    Private Sub ObjRepFormGlobal_ProcessReport() Handles ReportFrm.ProcessReport
        PartyWiseItemWiseOutstandingReport()
    End Sub
    Public Sub New(ByVal mReportFrm As AgLibrary.FrmReportLayout)
        ReportFrm = mReportFrm
    End Sub
    Private Sub PartyWiseItemWiseOutstandingReport()
        Dim mMainQry As String = ""
        Dim bReceiptAmount As Double = 0

        Dim bTableName_Invoices As String = "[" + Guid.NewGuid().ToString() + "]"

        Try
            Dim bSaleInvoiceConStr As String = ""
            bSaleInvoiceConStr = bSaleInvoiceConStr & ReportFrm.GetWhereCondition("H.BillToParty", rowParty)
            bSaleInvoiceConStr = bSaleInvoiceConStr & ReportFrm.GetWhereCondition("H.Div_Code", rowDivision).Replace("''", "'")
            bSaleInvoiceConStr = bSaleInvoiceConStr & ReportFrm.GetWhereCondition("H.Site_Code", rowSite).Replace("''", "'")

            Dim bLedgerConStr As String = ""
            bLedgerConStr = bLedgerConStr & ReportFrm.GetWhereCondition("L.SubCode", rowParty)
            bLedgerConStr = bLedgerConStr & ReportFrm.GetWhereCondition("L.DivCode", rowDivision).Replace("''", "'")
            bLedgerConStr = bLedgerConStr & ReportFrm.GetWhereCondition("L.Site_Code", rowSite).Replace("''", "'")

            mQry = "Create Temporary Table " & bTableName_Invoices & "
                    (
                        InvoiceDocID nVarchar(21),
                        InvoiceNo nVarchar(50),
                        InvoiceItem nVarchar(10),
                        InvoiceParty nVarchar(10),
                        InvoiceDate DateTime,
                        InvoiceQty Float Default 0,
                        InvoiceRate Float Default 0,
                        InvoiceAmount Float Default 0,
                        ReceiptAmount Float Default 0
                    ) "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

            mQry = "SELECT L.DocID, L.Item, Max(H.V_Date) As V_Date, Max(H.BillToParty) As SubCode, 
                    Max(H.V_Type || '-' || H.ManualRefNo) As ManualRefNo, 
                    Sum(L.Qty) AS InvoiceQty, Max(L.Rate) AS InvoiceRate,
                    Sum(L.Net_Amount) AS InvoiceAmount
                    FROM SaleInvoice H 
                    LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type
                    LEFT JOIN SaleInvoiceDetail L ON H.DocID = L.DocID
                    WHERE Vt.NCat = '" & Ncat.SaleInvoice & "'" & bSaleInvoiceConStr &
                    " GROUP BY L.DocID, L.Item

                    UNION ALL 

                    SELECT L.DocId, NULL AS Item, Max(L.V_Date) As V_Date, Max(L.SubCode) As SubCode, 
                    Max(L.V_Type || '-' || L.RecId) As ManualRefNo, 
                    0 AS InvoiceQty, 0 AS InvoiceRate,
                    Sum(L.AmtDr) AS InvoiceAmount
                    FROM Ledger L 
                    LEFT JOIN Subgroup Sg ON L.SubCode = Sg.Subcode
                    LEFT JOIN Voucher_Type Vt ON L.V_Type = Vt.V_Type
                    WHERE Vt.NCat <> '" & Ncat.SaleInvoice & "'
                    AND Sg.SubgroupType = '" & SubgroupType.Customer & "'
                    AND IfNull(L.AmtDr,0) > 0 " & bLedgerConStr &
                    " GROUP BY L.DocId
                    ORDER BY V_Date "
            Dim DtInvoices As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)


            mQry = "SELECT L.SubCode As SubCode, Sum(L.AmtCr) AS ReceiptAmount
                    FROM Ledger L 
                    LEFT JOIN Subgroup Sg ON L.SubCode = Sg.Subcode
                    LEFT JOIN Voucher_Type Vt ON L.V_Type = Vt.V_Type
                    WHERE Sg.SubgroupType = '" & SubgroupType.Customer & "'
                    AND IsNull(L.AmtCr,0) > 0 " & bLedgerConStr &
                    " GROUP BY L.SubCode "
            Dim DtReceipts As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

            For I As Integer = 0 To DtInvoices.Rows.Count - 1
                For J As Integer = 0 To DtReceipts.Rows.Count - 1
                    If AgL.XNull(DtInvoices.Rows(I)("SubCode")) = AgL.XNull(DtReceipts.Rows(J)("SubCode")) Then
                        If AgL.VNull(DtInvoices.Rows(I)("InvoiceAmount")) > AgL.VNull(DtReceipts.Rows(J)("ReceiptAmount")) Then
                            bReceiptAmount = AgL.VNull(DtReceipts.Rows(J)("ReceiptAmount"))
                        Else
                            bReceiptAmount = AgL.VNull(DtInvoices.Rows(I)("InvoiceAmount"))
                        End If
                        mQry = "Insert Into " & bTableName_Invoices & "(
                                InvoiceDocId, InvoiceNo, InvoiceDate, InvoiceParty, InvoiceItem, InvoiceQty, InvoiceRate, InvoiceAmount, ReceiptAmount)
                                Values(" & AgL.Chk_Text(AgL.XNull(DtInvoices.Rows(I)("DocId"))) & ",
                                " & AgL.Chk_Text(AgL.XNull(DtInvoices.Rows(I)("ManualRefNo"))) & ",
                                " & AgL.Chk_Date(AgL.XNull(DtInvoices.Rows(I)("V_Date"))) & ",
                                " & AgL.Chk_Text(AgL.XNull(DtInvoices.Rows(I)("SubCode"))) & ",
                                " & AgL.Chk_Text(AgL.XNull(DtInvoices.Rows(I)("Item"))) & ",
                                " & AgL.VNull(DtInvoices.Rows(I)("InvoiceQty")) & ",
                                " & AgL.VNull(DtInvoices.Rows(I)("InvoiceRate")) & ",
                                " & AgL.VNull(DtInvoices.Rows(I)("InvoiceAmount")) & ",
                                " & bReceiptAmount & ")"
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

                        DtReceipts.Rows(J)("ReceiptAmount") = AgL.VNull(DtReceipts.Rows(J)("ReceiptAmount")) - bReceiptAmount
                    End If
                Next
            Next

            Dim bMainConStr As String = ""
            bMainConStr = " Where 1=1 "
            bMainConStr = bMainConStr & ReportFrm.GetWhereCondition("L.InvoiceItem", rowItem)
            bMainConStr = bMainConStr & " AND Date(L.InvoiceDate) >= " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowFromDate)).ToString("s")) & ""
            bMainConStr = bMainConStr & " AND Date(L.InvoiceDate) <= " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowToDate)).ToString("s")) & ""

            mQry = " Select L.InvoiceDocId, L.InvoiceParty, L.InvoiceItem, Max(L.InvoiceNo) As InvoiceNo, 
                    " & IIf(AgL.PubServerName = "", "L.InvoiceDate", "Max(L.InvoiceDate)") & "  As InvoiceDate, 
                    Max(Sg.Name) As PartyName, Max(I.Description) As ItemDesc,
                    Sum(L.InvoiceQty) As Qty, Max(L.InvoiceRate) As Rate, 
                    Sum(L.InvoiceAmount) As InvoiceAmount, Sum(L.ReceiptAmount) As ReceiptAmount,
                    IfNull(Sum(L.InvoiceAmount),0) - IfNull(Sum(L.ReceiptAmount),0) As BalanceAmount
                    From " & bTableName_Invoices & " L 
                    LEFT JOIN ViewHelpSubGroup Sg On L.InvoiceParty = Sg.Code
                    LEFT JOIN Item I ON L.InvoiceItem = I.Code " & bMainConStr &
                    " Group By L.InvoiceDocId, L.InvoiceParty, L.InvoiceItem "
            If ReportFrm.FGetText(rowStatus) = "Balance" Then mQry += "Having IfNull(Sum(L.InvoiceAmount),0) - IfNull(Sum(L.ReceiptAmount),0) <> 0 "
            mQry += " Order By InvoiceDate"
            DsRep = AgL.FillData(mQry, AgL.GCn)

            RepTitle = "Party Outstanding" : RepName = "PartyWiseItemWiseOutstandingReport"

            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records to Print!")
            ReportFrm.PrintReport(DsRep, RepName, RepTitle)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        Finally
            Try
                mQry = "Drop Table " + bTableName_Invoices
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
            Catch ex As Exception
            End Try
        End Try
    End Sub
End Class
