Imports System.ComponentModel
Imports System.IO
Imports AgLibrary
Imports AgLibrary.ClsMain
Imports AgLibrary.ClsMain.agConstants
Imports Customised.ClsMain
Imports Microsoft.Reporting.WinForms
Imports CrystalDecisions.CrystalReports.Engine
Public Class ClsMasterPartyLedgerAadhat

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

    Public Const Col1Select As String = "Tick"
    Public Const Col1SearchCode As String = "Search Code"
    Public Const Col1SearchSr As String = "Search Sr"
    Public Const Col1Exception As String = "Exception"
    Public Const Col1Subcode As String = "Subcode"
    Public Const Col1Customer As String = "Customer"
    Public Const Col1Site As String = "Site"
    Public Const Col1DocDate As String = "Doc Date"
    Public Const Col1DocType As String = "Doc Type"
    Public Const Col1DocNo As String = "Doc No"
    Public Const Col1Brand As String = "Brand"
    Public Const Col1LRNo As String = "Lr No"
    Public Const Col1TaxableAmount As String = "Taxable Amount"
    Public Const Col1TaxAmount As String = "Tax Amount"
    Public Const Col1InvoiceAmount As String = "Invoice Amount"
    Public Const Col1AmtDr As String = "Amt Dr"
    Public Const Col1AmtCr As String = "Amt Cr"
    Public Const Col1Balance As String = "Balance"
    Public Const Col1WStatus As String = "Wa Status"
    Public Const Col1WDocType As String = "Wa Doc Type"
    Public Const Col1WDocNo As String = "Wa Doc No"
    Public Const Col1WGrossAmount As String = "Gross Amount"
    Public Const Col1WAdditionalAmount As String = "Additional Amount"
    Public Const Col1WaInvoiceAmount As String = "Wa Invoice Amount"
    Public Const Col1WaAmtDr As String = "Wa Amt Dr"
    Public Const Col1WaAmtCr As String = "Wa Amt Cr"
    Public Const Col1WPayment As String = "Payment"
    Public Const Col1WDCNote As String = "Debit Credit Note"
    Public Const Col1WaBalance As String = "Wa Balance"
    Public Const Col1TotalDr As String = "Total Dr"
    Public Const Col1TotalCr As String = "Total Cr"



    Dim mShowReportType As String = ""
    Dim DsHeader As DataSet = Nothing

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

    Public Sub Ini_Grid()
        Dim mQry As String = ""

        Try
            mQry = "Select Sg.Code, Sg.Name, Ag.GroupName From viewHelpSubgroup Sg Left Join AcGroup Ag On Sg.GroupCode = Ag.GroupCode Where Sg.SubgroupType In ('Master Customer','Master Supplier')"
            ReportFrm.CreateHelpGrid("Party", "Party", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mQry, "", 400, 600, 400)
            ReportFrm.CreateHelpGrid("FromDate", "From Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubStartDate)
            ReportFrm.CreateHelpGrid("To Date", "To Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubEndDate)
            'ReportFrm.CreateHelpGrid("Site", "Site", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, ClsReports.mHelpSiteQry, "[SITECODE]")
            'ReportFrm.CreateHelpGrid("Division", "Division", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, ClsReports.mHelpDivisionQry, "[DIVISIONCODE]")
            ReportFrm.CreateHelpGrid("Site", "Site", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, ClsReports.mHelpSiteQry)
            ReportFrm.CreateHelpGrid("Division", "Division", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, ClsReports.mHelpDivisionQry)


            ReportFrm.BtnProceed.Visible = False
            ReportFrm.BtnProceed.Text = "Proceed"
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub ObjRepFormGlobal_ProcessReport() Handles ReportFrm.ProcessReport
        ProcMain()
    End Sub
    Public Sub New(ByVal mReportFrm As FrmRepDisplay)
        ReportFrm = mReportFrm
    End Sub

    Public Sub ProcMain(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing, Optional bDocId As String = "")


        Try
            Dim mCondStr$ = "", mCondStr1$ = ""
            Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"
            Dim mTags As String() = Nothing
            Dim J As Integer



            RepTitle = "Master Party Ledger"


            If mFilterGrid IsNot Nothing And mGridRow IsNot Nothing Then
                If mGridRow.DataGridView.Columns.Contains("Search Code") = True Then
                    ClsMain.FOpenForm(mGridRow.Cells("Search Code").Value, ReportFrm)
                    ReportFrm.FiterGridCopy_Arr.RemoveAt(ReportFrm.FiterGridCopy_Arr.Count - 1)
                Else
                    Exit Sub
                End If
            End If

            If ReportFrm.FGetCode(0) = "" Then
                MsgBox("Please Select any Party First")
                Exit Sub
            End If


            mCondStr = " "
            mCondStr = " "
            mCondStr = mCondStr & " And  L.LinkedSubcode = '" & ReportFrm.FGetCode(0) & "'"
            mCondStr1 = mCondStr1 & " And  L.LinkedSubcode = '" & ReportFrm.FGetCode(0) & "'"
            mCondStr = mCondStr & " AND Date(L.V_Date) Between " & AgL.Chk_Date(ReportFrm.FGetText(1)) & " And " & AgL.Chk_Date(ReportFrm.FGetText(2)) & " "
            mCondStr1 = mCondStr1 & " AND Date(L.V_Date) Between " & AgL.Chk_Date(ReportFrm.FGetText(1)) & " And " & AgL.Chk_Date(ReportFrm.FGetText(2)) & " "
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("L.Site_Code", 3), "''", "'")
            mCondStr1 = mCondStr1 & Replace(ReportFrm.GetWhereCondition("L.Site_Code", 3), "''", "'")
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("L.DivCode", 4), "''", "'")
            mCondStr1 = mCondStr1 & Replace(ReportFrm.GetWhereCondition("L.DivCode", 4), "''", "'")



            mQry = "
                    SELECT SubCode, Max(VMain.Customer) as Customer, Max(CASE WHEN VMain.DocDateActualFormat<" & AgL.Chk_Date(ReportFrm.FGetText(1)) & " THEN Null ELSE VMain.SearchCode END) SearchCode,                    
                    Max(CASE WHEN VMain.DocDateActualFormat<" & AgL.Chk_Date(ReportFrm.FGetText(1)) & " THEN Null ELSE VMain.Site END) Site, 
                    (CASE WHEN VMain.DocDateActualFormat<" & AgL.Chk_Date(ReportFrm.FGetText(1)) & " THEN " & AgL.Chk_Date(ReportFrm.FGetText(1)) & " ELSE VMain.DocDate END) DocDate, 
                    Max(CASE WHEN VMain.DocDateActualFormat<" & AgL.Chk_Date(ReportFrm.FGetText(1)) & " THEN Null ELSE VMain.DocType END) DocType, 
                    (CASE WHEN VMain.DocDateActualFormat<" & AgL.Chk_Date(ReportFrm.FGetText(1)) & " THEN 'Opening' ELSE VMain.DocNo END) DocNo,                                        
                    Max(CASE WHEN VMain.DocDateActualFormat<" & AgL.Chk_Date(ReportFrm.FGetText(1)) & " THEN Null ELSE VMain.Brand END) Brand, 
                    Max(CASE WHEN VMain.DocDateActualFormat<" & AgL.Chk_Date(ReportFrm.FGetText(1)) & " THEN Null ELSE VMain.LrNo END) LrNo, 
                    Sum(CASE WHEN VMain.DocDateActualFormat<" & AgL.Chk_Date(ReportFrm.FGetText(1)) & " THEN 0 ELSE VMain.TaxableAmount End) TaxableAmount, 
                    Sum(CASE WHEN VMain.DocDateActualFormat<" & AgL.Chk_Date(ReportFrm.FGetText(1)) & " THEN 0 ELSE VMain.TaxAmount End) TaxAmount, 
                    Sum(CASE WHEN VMain.DocDateActualFormat<" & AgL.Chk_Date(ReportFrm.FGetText(1)) & " THEN 0 ELSE VMain.InvoiceAmount End) InvoiceAmount, 
                    (CASE WHEN VMain.DocDateActualFormat<" & AgL.Chk_Date(ReportFrm.FGetText(1)) & " THEN (Case When Sum(VMain.AmtDr)-Sum(VMain.AmtCr) > 0 Then Sum(VMain.AmtDr)-Sum(VMain.AmtCr) Else 0 End) ELSE Sum(VMain.AmtDr) END) AmtDr, 
                    (CASE WHEN VMain.DocDateActualFormat<" & AgL.Chk_Date(ReportFrm.FGetText(1)) & " THEN (Case When Sum(VMain.AmtCr)-Sum(VMain.AmtDr) > 0 Then Sum(VMain.AmtCr)-Sum(VMain.AmtDr) Else 0 End) ELSE Sum(VMain.AmtCr) END) AmtCr, 
                    0 as Balance,                                            
                    Max(CASE WHEN VMain.DocDateActualFormat<" & AgL.Chk_Date(ReportFrm.FGetText(1)) & " THEN Null ELSE VMain.WStatus END) WaStatus, 
                    Max(CASE WHEN VMain.DocDateActualFormat<" & AgL.Chk_Date(ReportFrm.FGetText(1)) & " THEN Null ELSE VMain.WDocType END) WaDocType, 
                    (CASE WHEN VMain.DocDateActualFormat<" & AgL.Chk_Date(ReportFrm.FGetText(1)) & " THEN 'Opening' ELSE VMain.WDocNo END) WaDocNo,                                        
                    Sum(CASE WHEN VMain.DocDateActualFormat<" & AgL.Chk_Date(ReportFrm.FGetText(1)) & " THEN 0 ELSE VMain.WGrossAmount End) WGrossAmount, 
                    Sum(CASE WHEN VMain.DocDateActualFormat<" & AgL.Chk_Date(ReportFrm.FGetText(1)) & " THEN 0 ELSE VMain.WAdditionalAmount End) WAdditionalAmount, 
                    Sum(CASE WHEN VMain.DocDateActualFormat<" & AgL.Chk_Date(ReportFrm.FGetText(1)) & " THEN 0 ELSE VMain.WInvoiceAmount End) [Wa Invoice Amount], 
                    (CASE WHEN VMain.DocDateActualFormat<" & AgL.Chk_Date(ReportFrm.FGetText(1)) & " THEN (Case When Sum(VMain.WAmtDr)-Sum(VMain.WAmtCr) > 0 Then Sum(VMain.WAmtDr)-Sum(VMain.WAmtCr) Else 0 End) ELSE Sum(VMain.WAmtDr) END) WaAmtDr, 
                    (CASE WHEN VMain.DocDateActualFormat<" & AgL.Chk_Date(ReportFrm.FGetText(1)) & " THEN (Case When Sum(VMain.WAmtCr)-Sum(VMain.WAmtDr) > 0 Then Sum(VMain.WAmtCr)-Sum(VMain.WAmtDr) Else 0 End) ELSE Sum(VMain.WAmtCr) END) WaAmtCr, 
                    Sum(CASE WHEN VMain.DocDateActualFormat<" & AgL.Chk_Date(ReportFrm.FGetText(1)) & " THEN 0 ELSE VMain.WPayment End) WPayment, 
                    Sum(CASE WHEN VMain.DocDateActualFormat<" & AgL.Chk_Date(ReportFrm.FGetText(1)) & " THEN 0 ELSE VMain.WDCNote End) WDebitCreditNote,
                    0 as WaBalance,
                    (CASE WHEN VMain.DocDateActualFormat<" & AgL.Chk_Date(ReportFrm.FGetText(1)) & " THEN (Case When Sum(VMain.TotalDr)-Sum(VMain.TotalCr) > 0 Then Sum(VMain.TotalDr)-Sum(VMain.TotalCr) Else 0 End) ELSE Sum(VMain.TotalDr) END)  TotalDr, 
                    (CASE WHEN VMain.DocDateActualFormat<" & AgL.Chk_Date(ReportFrm.FGetText(1)) & " THEN (Case When Sum(VMain.TotalCr)-Sum(VMain.TotalDr) > 0 Then Sum(VMain.TotalCr)-Sum(VMain.TotalDr) Else 0 End) ELSE Sum(VMain.TotalCr) END)  TotalCr                     
                    FROM
                    (

                        SELECT VTemp.Subcode, Max(VTemp.Customer) as Customer, Max(VTemp.SearchCode) AS SearchCode, Max(VTemp.DocID) AS DocID, 
					                        Max(VTemp.Site) AS Site, 
					                        Max(VTemp.RecID) AS DocNo, 
					                        strftime('%d/%m/%Y',IfNull(Max(VTemp.V_Date),Max(VTemp.WV_Date))) AS DocDate, 
                                            IfNull(Max(VTemp.V_Date),Max(VTemp.WV_Date)) AS DocDateActualFormat, 
					                        Max(VTemp.V_Type) AS DocType,
					                        IfNull(Sum(VTemp.AmtDr),0)+IfNull(Sum(VTemp.WAmtDr),0) AS TotalDr, IfNull(Sum(VTemp.AmtCr),0)+IfNull(Sum(VTemp.WAmtCr),0) AS TotalCr, 
                                            Max(VTemp.Brand) AS Brand, Max(VTemp.LRNo) AS LRNo, Sum(VTemp.TaxableAmount) AS TaxableAmount, 
                                            Sum(VTemp.TaxAmount) AS TaxAmount, Sum(VTemp.InvoiceAmount) AS InvoiceAmount, Sum(VTemp.AmtDr) AS AmtDr, 
                                            Sum(VTemp.AmtCr) AS AmtCr,
                                            Max(VTemp.WStatus) AS WStatus, 
                                            Max(VTemp.WDocID) AS WDocId, 
					                        Max(VTemp.WRecID) AS WDocNo, 					                        
					                        Max(VTemp.WV_Type) AS WDocType,
                                            Sum(VTemp.WGrossAmount) AS WGrossAmount, 
                                            Sum(VTemp.WAdditionAmount) AS WAdditionalAmount, Sum(VTemp.WInvoiceAmount) AS WInvoiceAmount, 
                                            Sum(VTemp.WAmtDr) AS WAmtDr, Sum(VTemp.WAmtCr) AS WAmtCr, Sum(CASE WHEN VTemp.NCat ='RCT' THEN VTemp.WAmtCr ELSE 0 End) AS WPayment, Sum(CASE WHEN VTemp.NCat <> 'RCT' THEN VTemp.WAmtCr ELSE 0 End) AS WDCNote
					
                        FROM
                        (					
                            Select CR.Code, L.LinkedSubcode AS SubCode, L.Subcode as Customer, L.DocID AS SearchCode, L.DocID, L.V_SNo, Site.ShortName AS Site, L.RecId , L.V_Date, L.V_Type, Vt.Ncat, 
                                                (Case When Vt.Ncat = '" & Ncat.Receipt & "' Then L.Chq_No Else S.Brand End) Brand, S.LRNo, S.TaxableAmount, S.TaxAmount, S.InvoiceAmount, L.AmtDr, L.AmtCr,
					                            (Case When S.DocID Is Not Null And S.LEDocID Is Null Then 'Pending' Else Null End) as WStatus, NULL AS WDocID, NULL AS WRecID, NULL AS WV_Date, NULL AS WV_Type, 0 WGrossAmount, 0 WAdditionAmount, 0 WInvoiceAmount, 0 WAmtDr, 0 WAmtCr                    
                                                From Ledger L
                                                Left Join (
                    			                            Select SI.DociD, Max(SI.V_Type) AS V_Type, Max(CASE WHEN I.V_Type ='Item' THEN IG.Description ELSE I.Description End) AS Brand, Max(SI.Taxable_Amount) AS TaxableAmount,
                    			                            Max(SI.Tax1) + Max(SI.Tax2) + Max(SI.Tax3) + Max(SI.Tax4) + Max(SI.Tax5) AS TaxAmount,
                    			                            Max(SI.Net_Amount) AS InvoiceAmount, SIT.LrNo || (Case When SIT.NoOfBales Is Not Null Then ' @ ' ||  Cast(SIT.NoOfBales as nVarchar) Else '' End) as LrNo, LE.DocID as LeDocID
                                                            From SaleInvoice SI
                                                            Left Join SaleInvoiceDetail SIL On SI.DocID = SIL.DocId
                                                            LEFT JOIN SaleInvoiceTransport SIT ON SI.DocID = SIT.DocID                                 
                                                            Left Join Item I On SIL.Item = I.Code
                                                            Left JOIN Item IG ON I.ItemGroup = IG.Code 
                                                            LEFT JOIN Voucher_Type Vt ON SI.V_Type = VT.V_Type 
                                                            Left Join SaleInvoiceGeneratedEntries LE On SI.DocID = LE.DocID
                                                            WHERE Vt.NCat = 'SI' And SI.V_Type='SI' And I.ItemType='TP'
                                                            GROUP BY SI.DocID                                
                                                          ) AS S ON L.DocID = S.DocID 
                                                LEFT JOIN SaleInvoiceGeneratedEntries CR ON L.DocId = CR.DocId
                                                LEFT JOIN SiteMast Site ON L.Site_Code = Site.Code                     
                                                LEFT JOIN voucher_type Vt ON L.V_Type = Vt.V_Type 
                            WHERE Substr(L.V_Type,1,1)<> 'W' And 1=1 " & mCondStr & "

                            UNION All

                            SELECT Cr.Code, L.LinkedSubCode AS SubCode, L.Subcode as Customer, L.DocID AS SearchCode, Null  DocId, L.V_SNo, Site.ShortName AS Site, NULL RecId, NULL V_Date, NULL V_Type, Vt.nCat, 
                                                S.Brand, S.LRNo, 0 TaxableAmount, 0 TaxAmount, 0 InvoiceAmount, 0 AmtDr, 0 AmtCr,
                                                Null as WStatus, L.DocId AS WDocID, L.RecId AS WRecID, L.V_Date AS WV_Date, L.V_Type AS WV_Type, IfNull(S.GrossAmount,0) AS WGrossAmount, IfNull(S.AdditionalAmount,0) AS WAdditionAmount, IfNull(S.InvoiceAmount,0) AS WInvoiceAmount, L.AmtDr AS WAmtDr, L.AmtCr WAmtCr
                                                From Ledger L
                                                Left Join (
                    			                            Select SI.DociD, Max(SI.V_Type) AS V_Type, Max(CASE WHEN I.V_Type ='Item' THEN IG.Description ELSE I.Description End) AS Brand, Sum(CASE WHEN I.ItemType = 'TP' THEN SIL.Amount ELSE 0 End) AS GrossAmount,
                    			                            Sum(CASE WHEN I.ItemType <> 'TP' THEN SIL.Amount ELSE 0 End) AS AdditionalAmount,
                    			                            Max(SI.Net_Amount) AS InvoiceAmount, SIT.LrNo || (Case When SIT.NoOfBales Is Not Null Then ' # ' ||  SIT.NoOfBales Else '' End) as LrNo
                                                            From SaleInvoice SI
                                                            Left Join SaleInvoiceDetail SIL On SI.DocID = SIL.DocId
                                                            LEFT JOIN SaleInvoiceTransport SIT ON SI.DocID = SIT.DocID                                 
                                                            Left Join Item I On SIL.Item = I.Code
                                                            Left JOIN Item IG ON I.ItemGroup = IG.Code 
                                                            LEFT JOIN Voucher_Type Vt ON SI.V_Type = VT.V_Type 
                                                            WHERE Vt.NCat = 'SI' And SI.V_Type='WSI'  And I.ItemType='TP'
                                                            GROUP BY SI.DocID                                
                                                          ) AS S ON L.DocID = S.DocID 
                                                LEFT JOIN SaleInvoiceGeneratedEntries CR ON L.DocId = CR.DocId                               
                                                LEFT JOIN SiteMast Site ON L.Site_Code = Site.Code
                                                LEFT JOIN voucher_type Vt ON L.V_Type = Vt.V_Type                      
                            WHERE Substr(L.V_Type,1,1) = 'W' And 1=1 " & mCondStr1 & " 
                            ) AS VTemp
                            GROUP BY VTemp.Subcode, VTemp.Code, (CASE WHEN VTemp.NCat='SI' Then VTemp.NCat ELSE VTemp.SearchCode END)		 	
                    ) AS VMain
                    GROUP BY VMain.Subcode, CASE WHEN VMain.DocDateActualFormat<" & AgL.Chk_Date(ReportFrm.FGetText(1)) & " THEN Null ELSE VMain.SearchCode END
                    ORDER BY VMain.Subcode,
		            VMain.DocDateActualFormat, VMain.DocType, VMain.DocNo

                   "


            'If ReportFrm.FGetText(0) = "Item Wise Balance" Then
            '    mQry = " Select VMain.DocId As SearchCode, Max(VMain.Division) as Division, Max(Vmain.Site) as Site, Max(VMain.V_Date) As OrderDate, Max(VMain.OrderNo) As OrderNo,
            '        Max(VMain.SaleToPartyName) As Party, Max(Vmain.ItemDesc) as ItemDescription, Max(VMain.NoOfBales) as OrderBales, Max(VMain.Qty) as OrderQty, Max(VMain.Amount) as OrderAmount, Max(Vmain.BalanceBales) as BalanceBales, Max(VMain.BalanceQty) as BalanceQty, Max(VMain.BalanceAmount) as BalanceAmount
            '        From (" & mQry & ") As VMain
            '        GROUP By VMain.DocId, VMain.Sr  "

            '    mQry += "Having Max(VMain.BalanceBales) > 0 "
            '    mQry += "Order By Max(VMain.V_Date_ActualFormat), Cast(Max(Replace(Vmain.ManualRefNo,'-','')) as Integer) "

            'Else
            '    mQry = " Select VMain.DocId As SearchCode, Max(VMain.Division) as Division, Max(Vmain.Site) as Site, Max(VMain.V_Date) As OrderDate, 
            '            Max(VMain.OrderNo) As OrderNo, Max(VMain.SaleToPartyName) As Party, Max(Vmain.ItemDesc) as ItemDescription, 
            '            Max(VMain.NoOfBales) as OrderBales, Max(VMain.BillBales) as InvoiceBales, Max(Vmain.BalanceBales) as BalanceBales, 
            '            Max(VMain.BillAmount) as InvoiceAmount, Max(VMain.OrderStatus) as OrderStatus, Max(VMain.OrderStatus) as OldOrderStatus
            '        From (" & mQry & ") As VMain
            '        GROUP By VMain.DocId, VMain.Sr  
            '        Order By Max(VMain.V_Date_ActualFormat), Cast(Max(Replace(Vmain.ManualRefNo,'-','')) as Integer) "
            'End If



            DsHeader = AgL.FillData(mQry, AgL.GCn)

            If DsHeader.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")



            'ReportFrm.Text = "Master Party Ledger - " + ReportFrm.FGetText(0)
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = "ProcMain"
            mQry = " Select 'Print' As MenuText, 'FGetPrint' As FunctionName"
            Dim DtMenuList As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
            ReportFrm.DTCustomMenus = DtMenuList
            ReportFrm.IsHideZeroColumns = False


            ReportFrm.ProcFillGrid(DsHeader)




            ReportFrm.AllowAutoResizeRows = False
            Dim I As Integer
            Dim mRunningBal As Double
            Dim mRunningBalW As Double
            mRunningBal = 0
            mRunningBalW = 0
            For I = 0 To ReportFrm.DGL1.RowCount - 1
                mRunningBal += AgL.VNull(ReportFrm.DGL1.Item(Col1AmtDr, I).Value) - AgL.VNull(ReportFrm.DGL1.Item(Col1AmtCr, I).Value)
                ReportFrm.DGL1.Item(Col1Balance, I).Value = mRunningBal
                mRunningBalW += AgL.VNull(ReportFrm.DGL1.Item(Col1WaAmtDr, I).Value) - AgL.VNull(ReportFrm.DGL1.Item(Col1WaAmtCr, I).Value)
                ReportFrm.DGL1.Item(Col1WaBalance, I).Value = mRunningBalW
            Next

            ReportFrm.AllowAutoResizeRows = True

        Catch ex As Exception
            MsgBox(ex.Message)
            DsHeader = Nothing
        End Try


    End Sub

    Sub FGetPrint(DGL As AgControls.AgDataGrid)
        FGetPrintCrystal(DGL, ClsMain.PrintFor.DocumentPrint, False)
    End Sub

    Sub FGetPrintCrystal(DGL As AgControls.AgDataGrid, mPrintFor As ClsMain.PrintFor, Optional ByVal IsPrintToPrinter As Boolean = False, Optional BulkCondStr As String = "")

        Try

            Dim I As Integer

            mQry = ""
            For I = 0 To DGL.Rows.Count - 1
                If AgL.XNull(DGL.Item(Col1Subcode, I).Value) <> "" Then
                    If mQry <> "" Then mQry += " Union All "
                    mQry += "Select 
                    " & AgL.Chk_Text(DGL.Item(Col1Subcode, I).Value) & " Subcode, 
                    " & AgL.Chk_Text(DGL.Item(Col1Customer, I).Value) & " Customer, 
                    " & AgL.Chk_Text(DGL.Item(Col1Site, I).Value) & " Site, 
                    '" & ClsMain.FormatDate(DGL.Item(Col1DocDate, I).Value) & "' DocDate, 
                    " & AgL.Chk_Text(DGL.Item(Col1DocType, I).Value) & " DocType, 
                    " & AgL.Chk_Text(DGL.Item(Col1DocNo, I).Value) & " DocNo,                                        
                    " & AgL.Chk_Text(DGL.Item(Col1Brand, I).Value) & " Brand, 
                    " & AgL.Chk_Text(DGL.Item(Col1LRNo, I).Value) & " LRNo, 
                    " & AgL.VNull(DGL.Item(Col1TaxableAmount, I).Value) & " TaxableAmount, 
                    " & AgL.VNull(DGL.Item(Col1TaxAmount, I).Value) & " TaxAmount, 
                    " & AgL.VNull(DGL.Item(Col1InvoiceAmount, I).Value) & " InvoiceAmount, 
                    " & AgL.VNull(DGL.Item(Col1AmtDr, I).Value) & " AmtDr, 
                    " & AgL.VNull(DGL.Item(Col1AmtCr, I).Value) & " AmtCr, 
                    " & AgL.VNull(DGL.Item(Col1Balance, I).Value) & " Balance,                                                                 
                    " & AgL.Chk_Text(DGL.Item(Col1WDocType, I).Value) & " WaDocType, 
                    " & AgL.Chk_Text(IIf(AgL.XNull(DGL.Item(Col1WDocNo, I).Value) = "", DGL.Item(Col1WStatus, I).Value, DGL.Item(Col1WDocNo, I).Value)) & " WaDocNo,                                        
                    " & AgL.VNull(DGL.Item(Col1WGrossAmount, I).Value) & " WGrossAmount, 
                    " & AgL.VNull(DGL.Item(Col1WAdditionalAmount, I).Value) & " WAdditionalAmount, 
                    " & AgL.VNull(DGL.Item(Col1WaInvoiceAmount, I).Value) & " [Wa Invoice Amount], 
                    " & AgL.VNull(DGL.Item(Col1WaAmtDr, I).Value) & " WaAmtDr, 
                    " & AgL.VNull(DGL.Item(Col1WaAmtCr, I).Value) & " WaAmtCr, 
                    " & AgL.VNull(DGL.Item(Col1WPayment, I).Value) & " WPayment, 
                    " & AgL.VNull(DGL.Item(Col1WDCNote, I).Value) & " WDCNote,
                    " & AgL.VNull(DGL.Item(Col1WaBalance, I).Value) & " WaBalance,
                    " & AgL.VNull(DGL.Item(Col1TotalDr, I).Value) & "  TotalDr, 
                    " & AgL.VNull(DGL.Item(Col1TotalCr, I).Value) & " TotalCr                     
                "
                End If
            Next
            mQry = "Select V.*, Sg.Name as PartyName, Cust.Name CustomerName
                   From (" & mQry & ") as V 
                   Left Join viewHelpSubgroup Sg On V.Subcode = Sg.Code 
                   Left Join viewHelpSubgroup Cust On V.Customer = Cust.Code 
                    
                   "


            Dim objRepPrint As Object
            If mPrintFor = ClsMain.PrintFor.EMail Then
                objRepPrint = New AgLibrary.FrmMailComposeWithCrystal(AgL)
            Else
                objRepPrint = New AgLibrary.RepView(AgL)
            End If


            ClsMain.FPrintThisDocument(ReportFrm, objRepPrint, "", mQry, "MasterPartyLedgerAadhat.rpt", "Master Party Ledger", , , , ReportFrm.FGetCode(0), ReportFrm.FGetText(1), IsPrintToPrinter)
        Catch ex As Exception
            MsgBox(ex.Message & "  In FGetPrintCrysal Procedure of ClsMasterPartyLedgerAadhat")
        End Try
    End Sub





    Private Function FGetSettings(FieldName As String, SettingType As String) As String
        Dim mValue As String
        mValue = ClsMain.FGetSettings(FieldName, SettingType, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
        FGetSettings = mValue
    End Function
    Public Sub FProceed()
        Try
        Catch ex As Exception
            AgL.ETrans.Rollback()
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub ReportFrm_BtnProceedPressed() Handles ReportFrm.BtnProceedPressed
        FProceed()
    End Sub
End Class
