Imports System.IO
Imports AgLibrary
Imports AgLibrary.ClsMain
Imports AgLibrary.ClsMain.agConstants
Imports Microsoft.Reporting.WinForms
Public Class ClsSadhviBranchPaymentStatus

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
            mQry = "Select 'Summary' as Code, 'Summary' as Name 
                    Union All Select 'Detail' as Code, 'Detail' as Name "
            ReportFrm.CreateHelpGrid("Report Type", "Report Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mQry, "Summary",,, 300)
            ReportFrm.CreateHelpGrid("FromDate", "From Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.RetMonthStartDate(AgL.PubLoginDate))
            ReportFrm.CreateHelpGrid("ToDate", "To Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.RetMonthEndDate(AgL.PubLoginDate))
            ReportFrm.CreateHelpGrid("Site", "Site", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSiteQry, "[SITECODE]")
            ReportFrm.CreateHelpGrid("Division", "Division", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpDivisionQry, "[DIVISIONCODE]")
            ReportFrm.CreateHelpGrid("Mobile", "Mobile", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.StringType, "", "")
            ReportFrm.FilterGrid.Rows(rowReportType).Visible = False
            ReportFrm.FilterGrid.Rows(rowMobile).Visible = False
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub ObjRepFormGlobal_ProcessReport() Handles ReportFrm.ProcessReport
        ProcBranchPaymentStatus()
    End Sub
    Public Sub New(ByVal mReportFrm As FrmRepDisplay)
        ReportFrm = mReportFrm
    End Sub
    Public Sub ProcBranchPaymentStatus(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try
            Dim bTableName$ = ""
            Dim mTags As String() = Nothing
            Dim mCondStr$ = ""
            Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"

            RepTitle = "Cash Customer Report"

            If mFilterGrid IsNot Nothing And mGridRow IsNot Nothing Then
                If mGridRow.DataGridView.Columns.Contains("Search Code") = True Then
                    If mFilterGrid.Item(GFilter, rowReportType).Value = "Summary" Then
                        mFilterGrid.Item(GFilter, rowReportType).Value = "Detail"
                        mFilterGrid.Item(GFilter, rowMobile).Value = mGridRow.Cells("Contact No").Value
                    Else
                        ClsMain.FOpenForm(mGridRow.Cells("Search Code").Value, ReportFrm)
                        ReportFrm.FiterGridCopy_Arr.RemoveAt(ReportFrm.FiterGridCopy_Arr.Count - 1)
                        Exit Sub
                    End If
                Else
                    Exit Sub
                End If
            End If

            Dim mHoParties As String = ""
            Dim Site As String = ReportFrm.FGetCode(rowSite)

            If Site = "''2''" Then
                mHoParties = "'D100004830','D100006102'"
            ElseIf Site = "''4''" Then
                mHoParties = "'D100028540','D100028541'"
            ElseIf Site = "''5''" Then
                mHoParties = "'D100025715','D100025716'"
            End If



            mCondStr = " And Date(H.V_Date) >= " & AgL.Chk_Date(ReportFrm.FGetText(rowFromDate)) & " 
                         And Date(H.V_Date) <= " & AgL.Chk_Date(ReportFrm.FGetText(rowToDate)) & " "
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("H.Site_Code", rowSite), "''", "'")
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("H.Div_Code", rowDivision), "''", "'")
            If ReportFrm.FGetText(rowMobile) <> "" Then
                mCondStr = mCondStr & " And H.SaleToPartyMobile = '" & ReportFrm.FGetText(rowMobile) & "'"
            End If

            Dim bCashBalanceQry As String = "SELECT 'Cash Balance' AS Type, 
                    Sum(CASE WHEN L.V_Date < " & AgL.Chk_Date(ReportFrm.FGetText(rowFromDate)) & " THEN IsNull(L.AmtDr,0) - IsNull(L.AmtCr,0) ELSE 0 END) AS Opening,
                    Sum(CASE WHEN L.V_Date >= " & AgL.Chk_Date(ReportFrm.FGetText(rowFromDate)) & " AND L.V_Date <= " & AgL.Chk_Date(ReportFrm.FGetText(rowToDate)) & " THEN IsNull(L.AmtDr,0) - IsNull(L.AmtCr,0) ELSE 0 END) AS PeriodAmount,
                    Sum(CASE WHEN L.V_Date <= " & AgL.Chk_Date(ReportFrm.FGetText(rowToDate)) & " THEN IsNull(L.AmtDr,0) - IsNull(L.AmtCr,0) ELSE 0 END) AS Closing
                    FROM Ledger L 
                    LEFT JOIN Subgroup Sg ON L.SubCode = Sg.Subcode " &
                    " Where 1=1 " & Replace(ReportFrm.GetWhereCondition("L.Site_Code", rowSite), "''", "'") &
                    Replace(ReportFrm.GetWhereCondition("L.DivCode", rowDivision), "''", "'") &
                    " And Sg.Nature = 'Cash'"

            Dim bCashPaymentToHO As String = "SELECT 'Cash Payment To HO' AS Type, 
                    Sum(CASE WHEN L.V_Date < " & AgL.Chk_Date(ReportFrm.FGetText(rowFromDate)) & " THEN IsNull(L.AmtDr,0) ELSE 0 END) AS Opening,
                    Sum(CASE WHEN L.V_Date >= " & AgL.Chk_Date(ReportFrm.FGetText(rowFromDate)) & " AND L.V_Date <= " & AgL.Chk_Date(ReportFrm.FGetText(rowToDate)) & " THEN IsNull(L.AmtDr,0) ELSE 0 END) AS PeriodAmount,
                    Sum(CASE WHEN L.V_Date <= " & AgL.Chk_Date(ReportFrm.FGetText(rowToDate)) & " THEN IsNull(L.AmtDr,0) ELSE 0 END) AS Closing
                    FROM Ledger L 
                    LEFT JOIN LedgerHead H ON L.DocId = H.DocID
                    LEFT JOIN PurchInvoice Pi ON L.DocId = Pi.DocID
                    LEFT JOIN SaleInvoice Si ON L.DocId = Si.DocID
                    LEFT JOIN Subgroup Sg ON IsNull(IsNull(H.SubCode,Pi.BillToParty),Si.BillToParty) = Sg.Subcode " &
                    " Where 1=1 " & Replace(ReportFrm.GetWhereCondition("L.Site_Code", rowSite), "''", "'") &
                    Replace(ReportFrm.GetWhereCondition("L.DivCode", rowDivision), "''", "'") &
                    " AND IsNull(L.AmtDr,0) > 0
                    AND Sg.Nature = 'Cash'
                    AND L.SubCode IN (" & mHoParties & ") "

            Dim bBankBalance As String = " Select 'Bank Balance' AS Type, 
                    Sum(CASE WHEN L.V_Date < " & AgL.Chk_Date(ReportFrm.FGetText(rowFromDate)) & " THEN IsNull(L.AmtDr,0) - IsNull(L.AmtCr,0) ELSE 0 END) AS Opening,
                    Sum(CASE WHEN L.V_Date >= " & AgL.Chk_Date(ReportFrm.FGetText(rowFromDate)) & " AND L.V_Date <= " & AgL.Chk_Date(ReportFrm.FGetText(rowToDate)) & " THEN IsNull(L.AmtDr,0) - IsNull(L.AmtCr,0) ELSE 0 END) AS PeriodAmount,
                    Sum(CASE WHEN L.V_Date <= " & AgL.Chk_Date(ReportFrm.FGetText(rowToDate)) & " THEN IsNull(L.AmtDr,0) - IsNull(L.AmtCr,0) ELSE 0 END) AS Closing
                    FROM Ledger L 
                    LEFT JOIN Subgroup Sg ON L.SubCode = Sg.Subcode " &
                    " Where 1=1 " & Replace(ReportFrm.GetWhereCondition("L.Site_Code", rowSite), "''", "'") &
                    Replace(ReportFrm.GetWhereCondition("L.DivCode", rowDivision), "''", "'") &
                    " And Sg.Nature = 'Bank' "

            'Dim bBankPayment As String = " Select 'Bank Payment' AS Type, 
            '        Sum(CASE WHEN L.V_Date < " & AgL.Chk_Date(ReportFrm.FGetText(rowFromDate)) & " THEN IsNull(L.AmtCr,0) ELSE 0 END) AS Opening,
            '        Sum(CASE WHEN L.V_Date >= " & AgL.Chk_Date(ReportFrm.FGetText(rowFromDate)) & " AND L.V_Date <= " & AgL.Chk_Date(ReportFrm.FGetText(rowToDate)) & " THEN IsNull(L.AmtCr,0) ELSE 0 END) AS PeriodAmount,
            '        Sum(CASE WHEN L.V_Date <= " & AgL.Chk_Date(ReportFrm.FGetText(rowToDate)) & " THEN IsNull(L.AmtCr,0) ELSE 0 END) AS Closing
            '        FROM Ledger L 
            '        LEFT JOIN Subgroup Sg ON L.SubCode = Sg.Subcode " &
            '        " Where 1=1 " & Replace(ReportFrm.GetWhereCondition("L.Site_Code", rowSite), "''", "'") &
            '        Replace(ReportFrm.GetWhereCondition("L.DivCode", rowDivision), "''", "'") &
            '        " And IsNull(L.AmtCr,0) > 0
            '        AND Sg.Nature = 'Bank' "

            Dim bBankPayment As String = " Select 'Bank Payment' AS Type, 
                    Sum(CASE WHEN L.V_Date < " & AgL.Chk_Date(ReportFrm.FGetText(rowFromDate)) & " THEN CASE WHEN L.V_Type='JV'  THEN -IsNull(L.AmtCr,0) ELSE  IsNull(L.AmtCr,0) END ELSE 0 END) AS Opening,
                    Sum(CASE WHEN L.V_Date >= " & AgL.Chk_Date(ReportFrm.FGetText(rowFromDate)) & " AND L.V_Date <= " & AgL.Chk_Date(ReportFrm.FGetText(rowToDate)) & " THEN CASE WHEN L.V_Type='JV'  THEN -IsNull(L.AmtCr,0) ELSE  IsNull(L.AmtCr,0) END ELSE 0 END) AS PeriodAmount,
                    Sum(CASE WHEN L.V_Date <= " & AgL.Chk_Date(ReportFrm.FGetText(rowToDate)) & " THEN CASE WHEN L.V_Type='JV'  THEN -IsNull(L.AmtCr,0) ELSE  IsNull(L.AmtCr,0) END ELSE 0 END) AS Closing
                    FROM Ledger L 
                    LEFT JOIN Subgroup Sg ON L.SubCode = Sg.Subcode " &
                    " Where 1=1 " & Replace(ReportFrm.GetWhereCondition("L.Site_Code", rowSite), "''", "'") &
                    Replace(ReportFrm.GetWhereCondition("L.DivCode", rowDivision), "''", "'") &
                    " And IsNull(L.AmtCr,0) > 0
                    AND Sg.Nature = 'Bank' "


            Dim bDepositToHOBankAccount As String = " Select 'Deposit To HO Bank Account', 
                    Sum(CASE WHEN L.V_Date < " & AgL.Chk_Date(ReportFrm.FGetText(rowFromDate)) & " THEN IsNull(L.AmtCr,0) ELSE 0 END) AS Opening,
                    Sum(CASE WHEN L.V_Date >= " & AgL.Chk_Date(ReportFrm.FGetText(rowFromDate)) & " AND L.V_Date <= " & AgL.Chk_Date(ReportFrm.FGetText(rowToDate)) & " THEN IsNull(L.AmtCr,0) ELSE 0 END) AS PeriodAmount,
                    Sum(CASE WHEN L.V_Date <= " & AgL.Chk_Date(ReportFrm.FGetText(rowToDate)) & " THEN IsNull(L.AmtCr,0) ELSE 0 END) AS Closing
                    FROM Ledger L
                    LEFT JOIN Subgroup Sg ON L.SubCode = Sg.Subcode
                    LEFT JOIN AcGroup Ag ON Sg.GroupCode = Ag.GroupCode " &
                    " Where 1=1 " & Replace(ReportFrm.GetWhereCondition("L.Site_Code", rowSite), "''", "'") &
                    Replace(ReportFrm.GetWhereCondition("L.DivCode", rowDivision), "''", "'") &
                    " And Ag.GroupName = 'Sundry Debtors'
                    AND L.V_Type = 'JV' "

            Dim bDebtorsOutstanding As String = " Select 'Debtors Outstanding', 
                    Sum(CASE WHEN L.V_Date < " & AgL.Chk_Date(ReportFrm.FGetText(rowFromDate)) & " THEN IsNull(L.AmtDr,0) - IsNull(L.AmtCr,0) ELSE 0 END) AS Opening,
                    Sum(CASE WHEN L.V_Date >= " & AgL.Chk_Date(ReportFrm.FGetText(rowFromDate)) & " AND L.V_Date <= " & AgL.Chk_Date(ReportFrm.FGetText(rowToDate)) & " THEN IsNull(L.AmtDr,0) - IsNull(L.AmtCr,0) ELSE 0 END) AS PeriodAmount,
                    Sum(CASE WHEN L.V_Date <= " & AgL.Chk_Date(ReportFrm.FGetText(rowToDate)) & " THEN IsNull(L.AmtDr,0) - IsNull(L.AmtCr,0) ELSE 0 END) AS Closing
                    FROM Ledger L
                    LEFT JOIN Subgroup Sg ON L.SubCode = Sg.Subcode
                    LEFT JOIN AcGroup Ag ON Sg.GroupCode = Ag.GroupCode " &
                    " Where 1=1 " & Replace(ReportFrm.GetWhereCondition("L.Site_Code", rowSite), "''", "'") &
                    Replace(ReportFrm.GetWhereCondition("L.DivCode", rowDivision), "''", "'") &
                    " And Ag.GroupName = 'Sundry Debtors' "

            Dim bTotalSale As String = " Select 'Total Sale', 
                    -Round(Sum(CASE WHEN H.V_Date < " & AgL.Chk_Date(ReportFrm.FGetText(rowFromDate)) & " THEN IsNull(L.Net_Amount,0) ELSE 0 END),0) AS Opening,
                    -Round(Sum(CASE WHEN H.V_Date >= " & AgL.Chk_Date(ReportFrm.FGetText(rowFromDate)) & " AND H.V_Date <= " & AgL.Chk_Date(ReportFrm.FGetText(rowToDate)) & " THEN IsNull(L.Net_Amount,0) ELSE 0 END),0) AS PeriodAmount,
                    -Round(Sum(CASE WHEN H.V_Date <= " & AgL.Chk_Date(ReportFrm.FGetText(rowToDate)) & " THEN IsNull(L.Net_Amount,0) ELSE 0 END),0) AS Closing
                    FROM SaleInvoice H 
                    LEFT JOIN SaleInvoiceDetail L ON H.DocID = L.DocID " &
                    " Where 1=1 " & Replace(ReportFrm.GetWhereCondition("H.Site_Code", rowSite), "''", "'") &
                    Replace(ReportFrm.GetWhereCondition("H.Div_Code", rowDivision), "''", "'")


            mQry = bCashBalanceQry & " UNION ALL " &
                   bCashPaymentToHO & " UNION ALL " &
                   bBankBalance & " UNION ALL " &
                   bBankPayment & " UNION ALL " &
                   bDepositToHOBankAccount & " UNION ALL " &
                   bDebtorsOutstanding & " UNION ALL " &
                   bTotalSale

            DsHeader = AgL.FillData(mQry, AgL.GCn)

            If DsHeader.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            ReportFrm.Text = "Branch Payment Status"
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = "ProcBranchPaymentStatus"
            ReportFrm.IsHideZeroColumns = False
            ReportFrm.ProcFillGrid(DsHeader)

            ReportFrm.DGL1.Columns("Type").Width = 300
            ReportFrm.DGL1.Columns("Opening").Width = 200
            ReportFrm.DGL1.Columns("Period Amount").Width = 200
            ReportFrm.DGL1.Columns("Closing").Width = 200

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
