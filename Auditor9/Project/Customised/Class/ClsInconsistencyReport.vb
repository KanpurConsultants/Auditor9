Imports System.IO
Imports AgLibrary
Imports AgLibrary.ClsMain
Imports AgLibrary.ClsMain.agConstants
Imports Microsoft.Reporting.WinForms
Public Class ClsInconsistencyReport

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
    Dim rowMobile As Integer = 5

    Dim mReportType_PurchaseAccountAndInputTaxRegisterDiffernece As String = "Purchase Account & Input Tax Register Differnece"
    Dim mReportType_SalesAccountAndOutputTaxRegisterDiffernece As String = "Sales Account & Outupt Tax Register Differnece"
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
    Public Sub Ini_Grid()
        Try
            mQry = "Select '" & mReportType_SalesAccountAndOutputTaxRegisterDiffernece & "' as Code, '" & mReportType_SalesAccountAndOutputTaxRegisterDiffernece & "' as Name 
                    Union All 
                    Select '" & mReportType_PurchaseAccountAndInputTaxRegisterDiffernece & "' as Code, '" & mReportType_PurchaseAccountAndInputTaxRegisterDiffernece & "' as Name 
"
            ReportFrm.CreateHelpGrid("Report Type", "Report Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mQry, "",,, 300)
            ReportFrm.CreateHelpGrid("FromDate", "From Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.PubStartDate)
            ReportFrm.CreateHelpGrid("ToDate", "To Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.PubEndDate)
            ReportFrm.CreateHelpGrid("Site", "Site", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSiteQry, "[SITECODE]")
            ReportFrm.CreateHelpGrid("Division", "Division", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpDivisionQry, "[DIVISIONCODE]")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub ObjRepFormGlobal_ProcessReport() Handles ReportFrm.ProcessReport
        ProcInconsistencyReport()
    End Sub
    Public Sub New(ByVal mReportFrm As FrmRepDisplay)
        ReportFrm = mReportFrm
    End Sub
    Public Sub ProcInconsistencyReport(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try
            Dim bTableName$ = ""
            Dim mTags As String() = Nothing
            Dim mCondStr$ = "", mLedgerCondStr$ = ""
            Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"

            RepTitle = "Cash Customer Report"

            If mFilterGrid IsNot Nothing And mGridRow IsNot Nothing Then
                If mGridRow.DataGridView.Columns.Contains("Search Code") = True Then
                    ClsMain.FOpenForm(mGridRow.Cells("Search Code").Value, ReportFrm)
                    ReportFrm.FiterGridCopy_Arr.RemoveAt(ReportFrm.FiterGridCopy_Arr.Count - 1)
                    Exit Sub
                Else
                    Exit Sub
                End If
            End If

            mCondStr = " And Date(H.V_Date) >= " & AgL.Chk_Date(ReportFrm.FGetText(rowFromDate)) & " 
                         And Date(H.V_Date) <= " & AgL.Chk_Date(ReportFrm.FGetText(rowToDate)) & " "
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("H.Site_Code", rowSite), "''", "'")
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("H.Div_Code", rowDivision), "''", "'")


            mLedgerCondStr = " And Date(L.V_Date) >= " & AgL.Chk_Date(ReportFrm.FGetText(rowFromDate)) & " 
                         And Date(L.V_Date) <= " & AgL.Chk_Date(ReportFrm.FGetText(rowToDate)) & " "
            mLedgerCondStr = mLedgerCondStr & Replace(ReportFrm.GetWhereCondition("L.Site_Code", rowSite), "''", "'")
            mLedgerCondStr = mLedgerCondStr & Replace(ReportFrm.GetWhereCondition("L.DivCode", rowDivision), "''", "'")

            If ReportFrm.FGetText(rowReportType) = "" Then
                MsgBox("Please Select Report Type First...!", MsgBoxStyle.Information)
                Exit Sub
            End If


            If ReportFrm.FGetText(rowReportType) = mReportType_PurchaseAccountAndInputTaxRegisterDiffernece Then
                Dim mQryHeaderPart As String = "Select IfNull(VPurch.DocId, VLedger.DocId) As SearchCode, 
                        IfNull(VPurch.DocNo, VLedger.DocNo) As DocNo, 
                        VLedger.Balance As LedgerBalance, VPurch.Amount As DocAmount,
                        Abs(Round(IsNull(VLedger.Balance,0),2) - Round(IsNull(VPurch.Amount,0),2)) AS Diff
                        FROM ( "

                Dim mLedgerQry As String = " (Select L.DocId, Max(L.V_Type || '-' || L.RecId) As DocNo, IsNull(Sum(L.AmtDr),0) - IsNull(SUm(L.AmtCr),0) AS Balance
	                        FROM Ledger L 
	                        LEFT JOIN Subgroup SG ON L.SubCode = Sg.Subcode
	                        LEFT JOIN AcGroup Ag ON Sg.GroupCode = Ag.GroupCode
	                        WHERE Ag.GroupName = 'Purchase Accounts' " & mLedgerCondStr &
                            " GROUP BY L.DocId) As VLedger "

                Dim mPurchQry As String = "	(SELECT H.DocID, Max(H.V_Type || '-' || H.ManualRefNo) As DocNo, Sum(L.Taxable_Amount) AS Amount
	                        FROM PurchInvoice H 
	                        LEFT JOIN PurchInvoiceDetail L ON H.DocID = L.DocID
                            LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
	                        WHERE Vt.NCat IN ('" & Ncat.PurchaseInvoice & "', '" & Ncat.PurchaseReturn & "') " & mCondStr &
                            " GROUP BY H.DocID

	                        UNION ALL 
	
	                        SELECT H.DocID, Max(H.V_Type || '-' || H.ManualRefNo) As DocNo, Sum(L.Taxable_Amount) AS Amount
	                        FROM LedgerHead H 
	                        LEFT JOIN LedgerHeadDetailCharges L ON H.DocID = L.DocID
                            LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
	                        WHERE Vt.NCat IN ('" & Ncat.DebitNoteSupplier & "', '" & Ncat.CreditNoteSupplier & "', '" & Ncat.ExpenseVoucher & "') " & mCondStr &
                            " GROUP BY H.DocID) As VPurch "

                Dim mQryFooterPart As String = " ON VLedger.DocId = VPurch.DocId
                        WHERE Round(IsNull(VLedger.Balance,0),2) <> Round(IsNull(VPurch.Amount,0),2) "

                mQry = mQryHeaderPart & "(" & mLedgerQry & ")" & "(" & mPurchQry & ")" & mQryFooterPart


                'mQry = "Select IfNull(V2.DocId, V1.DocId) As SearchCode, 
                '        IfNull(V2.DocNo, V1.DocNo) As DocNo, 
                '        V1.Balance As LedgerBalance, V2.Amount As DocAmount,
                '        Abs(Round(IsNull(V1.Balance,0),2) - Round(IsNull(V2.Amount,0),2)) AS Diff
                '        FROM (
                '         SELECT L.DocId, Max(L.V_Type || '-' || L.RecId) As DocNo, IsNull(Sum(L.AmtDr),0) - IsNull(SUm(L.AmtCr),0) AS Balance
                '         FROM Ledger L 
                '         LEFT JOIN Subgroup SG ON L.SubCode = Sg.Subcode
                '         LEFT JOIN AcGroup Ag ON Sg.GroupCode = Ag.GroupCode
                '         WHERE Ag.GroupName = 'Purchase Accounts' " & mLedgerCondStr &
                '            " GROUP BY L.DocId
                '        ) AS V1
                '        FULL OUTER JOIN (
                '         SELECT H.DocID, Max(H.V_Type || '-' || H.ManualRefNo) As DocNo, Sum(L.Taxable_Amount) AS Amount
                '         FROM PurchInvoice H 
                '         LEFT JOIN PurchInvoiceDetail L ON H.DocID = L.DocID
                '            LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                '         WHERE Vt.NCat IN ('" & Ncat.PurchaseInvoice & "', '" & Ncat.PurchaseReturn & "') " & mCondStr &
                '            " GROUP BY H.DocID

                '         UNION ALL 

                '         SELECT H.DocID, Max(H.V_Type || '-' || H.ManualRefNo) As DocNo, Sum(L.Taxable_Amount) AS Amount
                '         FROM LedgerHead H 
                '         LEFT JOIN LedgerHeadDetailCharges L ON H.DocID = L.DocID
                '            LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                '         WHERE Vt.NCat IN ('" & Ncat.DebitNoteSupplier & "', '" & Ncat.CreditNoteSupplier & "', '" & Ncat.ExpenseVoucher & "') " & mCondStr &
                '            " GROUP BY H.DocID
                '        ) AS V2 ON V1.DocId = V2.DocId
                '        WHERE Round(IsNull(V1.Balance,0),2) <> Round(IsNull(V2.Amount,0),2) "
            End If

            If ReportFrm.FGetText(rowReportType) = mReportType_SalesAccountAndOutputTaxRegisterDiffernece Then
                mQry = "SELECT IfNull(V2.DocId, V1.DocId) As SearchCode, 
                        IfNull(V2.DocNo, V1.DocNo) As DocNo, 
                        V1.Balance As LedgerBalance, V2.Amount As DocAmount,
                        Abs(Round(IsNull(V1.Balance,0),2) - Round(IsNull(V2.Amount,0),2)) AS Diff
                        FROM (
	                        SELECT L.DocId, Max(L.V_Type || '-' || L.RecId) As DocNo, IsNull(Sum(L.AmtCr),0) - IsNull(SUm(L.AmtDr),0) AS Balance
	                        FROM Ledger L 
	                        LEFT JOIN Subgroup SG ON L.SubCode = Sg.Subcode
	                        LEFT JOIN AcGroup Ag ON Sg.GroupCode = Ag.GroupCode
	                        WHERE Ag.GroupName = 'Sales Accounts' " & mLedgerCondStr &
                            " GROUP BY L.DocId
                        ) AS V1
                        FULL OUTER JOIN (
	                        SELECT H.DocID, Max(H.V_Type || '-' || H.ManualRefNo) As DocNo, Sum(L.Taxable_Amount) AS Amount
	                        FROM SaleInvoice H 
	                        LEFT JOIN SaleInvoiceDetail L ON H.DocID = L.DocID
                            LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
	                        WHERE Vt.NCat IN ('" & Ncat.SaleInvoice & "', '" & Ncat.SaleReturn & "') " & mCondStr &
                            " GROUP BY H.DocID

	                        UNION ALL 
	
	                        SELECT H.DocID, Max(H.V_Type || '-' || H.ManualRefNo) As DocNo, Sum(L.Taxable_Amount) AS Amount
	                        FROM LedgerHead H 
	                        LEFT JOIN LedgerHeadDetailCharges L ON H.DocID = L.DocID
                            LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
	                        WHERE Vt.NCat IN ('" & Ncat.DebitNoteCustomer & "', '" & Ncat.CreditNoteCustomer & "', '" & Ncat.IncomeVoucher & "') " & mCondStr &
                            " GROUP BY H.DocID
                        ) AS V2 ON V1.DocId = V2.DocId
                        WHERE Round(IsNull(V1.Balance,0),2) <> Round(IsNull(V2.Amount,0),2) "
            End If

            DsHeader = AgL.FillData(mQry, AgL.GCn)

            If DsHeader.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            ReportFrm.Text = "Inconsistency Report"
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = "ProcInconsistencyReport"
            ReportFrm.IsHideZeroColumns = False
            ReportFrm.ProcFillGrid(DsHeader)

        Catch ex As Exception
            MsgBox(ex.Message)
            DsHeader = Nothing
        Finally
            For I As Integer = 0 To ReportFrm.DGL1.Columns.Count - 1
                ReportFrm.DGL2.Columns(I).Visible = ReportFrm.DGL1.Columns(I).Visible
                ReportFrm.DGL2.Columns(I).Width = ReportFrm.DGL1.Columns(I).Width
                ReportFrm.DGL2.Columns(I).DisplayIndex = ReportFrm.DGL1.Columns(I).DisplayIndex
            Next
        End Try
    End Sub
End Class
