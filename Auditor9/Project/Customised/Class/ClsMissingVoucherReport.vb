Imports System.IO
Imports AgLibrary
Imports AgLibrary.ClsMain
Imports AgLibrary.ClsMain.agConstants
Imports Microsoft.Reporting.WinForms
Public Class ClsMissingVoucherReport

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

    Dim rowFromDate As Integer = 0
    Dim rowToDate As Integer = 1
    Dim rowSite As Integer = 2
    Dim rowDivision As Integer = 3
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
            ReportFrm.CreateHelpGrid("FromDate", "From Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.RetMonthStartDate(AgL.PubStartDate))
            ReportFrm.CreateHelpGrid("ToDate", "To Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.RetMonthEndDate(AgL.PubEndDate))
            ReportFrm.CreateHelpGrid("Site", "Site", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSiteQry, "[SITECODE]")
            ReportFrm.CreateHelpGrid("Division", "Division", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpDivisionQry, "[DIVISIONCODE]")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub ObjRepFormGlobal_ProcessReport() Handles ReportFrm.ProcessReport
        ProcMissingVoucherReport()
    End Sub
    Public Sub New(ByVal mReportFrm As FrmRepDisplay)
        ReportFrm = mReportFrm
    End Sub
    Public Sub ProcMissingVoucherReport(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try
            Dim bTableName$ = ""
            Dim mTags As String() = Nothing
            Dim mCondStr$ = ""
            Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"

            RepTitle = "Missing Voucher Report"

            If mFilterGrid IsNot Nothing And mGridRow IsNot Nothing Then
                If mGridRow.DataGridView.Columns.Contains("Search Code") = True Then
                    ClsMain.FOpenForm(mGridRow.Cells("Search Code").Value, ReportFrm)
                    ReportFrm.FiterGridCopy_Arr.RemoveAt(ReportFrm.FiterGridCopy_Arr.Count - 1)
                    Exit Sub
                Else
                    Exit Sub
                End If
            End If

            mQry = " SELECT Count(*) FROM (	SELECT DISTINCT Comp_Code
	                FROM (
		                SELECT C.Comp_Code 
		                FROM Company C
		                WHERE " & AgL.Chk_Date(ReportFrm.FGetText(rowFromDate)) & " BETWEEN C.Start_Dt AND C.End_Dt
		                UNION ALL 
		                SELECT C.Comp_Code
		                FROM Company C
		                WHERE " & AgL.Chk_Date(ReportFrm.FGetText(rowToDate)) & " BETWEEN C.Start_Dt AND C.End_Dt
	                ) AS V1) AS VMain "
            If AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar()) > 1 Then
                MsgBox("Please select one financial year date.", MsgBoxStyle.Information)
                Exit Sub
            End If

            Dim DsSale As DataSet
            Dim DsSaleLedger As DataSet
            Dim DsPurchase As DataSet
            Dim DsLedgerHead As DataSet

            DsSale = FGetDataFromTables("SaleInvoice")
            DsSaleLedger = FGetDataFromLedgerTables("SaleInvoice")
            DsPurchase = FGetDataFromTables("PurchInvoice")
            DsLedgerHead = FGetDataFromTables("LedgerHead")

            DsSale.Merge(DsSaleLedger)
            DsSale.Merge(DsPurchase)
            DsSale.Merge(DsLedgerHead)

            DsHeader = DsSale

            If DsHeader.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            ReportFrm.Text = "Missing Voucher List"
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = "ProcMissingVoucherReport"
            ReportFrm.IsHideZeroColumns = False
            ReportFrm.ProcFillGrid(DsHeader)

            ReportFrm.DGL1.Columns("Type").Visible = False
            ReportFrm.DGL1.Columns("Site Code").Visible = False
            ReportFrm.DGL1.Columns("Div Code").Visible = False
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
    Private Function FGetDataFromTables(bTableName As String) As DataSet
        Dim DsTemp As DataSet
        Dim mCondStr As String

        mCondStr = " Where 1=1 "
        mCondStr = mCondStr & " And Date(H.V_Date) >= " & AgL.Chk_Date(ReportFrm.FGetText(rowFromDate)) & " 
                         And Date(H.V_Date) <= " & AgL.Chk_Date(ReportFrm.FGetText(rowToDate)) & " "
        mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("H.Site_Code", rowSite), "''", "'")
        mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("H.Div_Code", rowDivision), "''", "'")

        If bTableName = "LedgerHead" Then
            mCondStr = mCondStr & " And Vt.NCat Not In ('" & Ncat.PurchaseInvoice & "','" & Ncat.PurchaseReturn & "')"
        End If

        mQry = " SELECT H.V_Type, H.Div_Code, H.Site_Code,
                    Max(Vt.Description) As VoucherType, Max(Sm.Name) As Site, 
                    Max(D.Div_Name) As Division,
                    Min(CAST(H.ManualRefNo AS Int)) As StartDocNo,
                    Max(CAST(H.ManualRefNo AS Int)) As EndDocNo,
                    '' As MissingVoucherList
                    FROM " & bTableName & " H 
                    LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type 
                    LEFT JOIN SiteMast Sm On H.Site_Code = Sm.Code 
                    LEFT JOIN Division D On H.Div_Code = D.Div_Code " & mCondStr &
                " GROUP BY H.V_Type, H.Div_Code, H.Site_Code "
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        For I As Integer = 0 To DsTemp.Tables(0).Rows.Count - 1
            mQry = "SELECT Vt.Description, CAST(H.ManualRefNo AS Int) As ManualRefNo
                        FROM " & bTableName & " H 
                        LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type
                        WHERE Date(H.V_Date) >= " & AgL.Chk_Date(ReportFrm.FGetText(rowFromDate)) & " 
                        And Date(H.V_Date) <= " & AgL.Chk_Date(ReportFrm.FGetText(rowToDate)) & "
                        AND H.V_Type = '" & DsTemp.Tables(0).Rows(I)("V_Type") & "' 
                        AND H.Site_Code = '" & DsTemp.Tables(0).Rows(I)("Site_Code") & "' 
                        AND H.Div_Code = '" & DsTemp.Tables(0).Rows(I)("Div_Code") & "' 
                        ORDER BY CAST(H.ManualRefNo AS Int) "
            Dim DtData As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

            Dim mList As String = ""
            For J As Integer = 0 To DtData.Rows.Count - 1
                If J > 0 Then
                    If AgL.VNull(DtData.Rows(J)("ManualRefNo")) - AgL.VNull(DtData.Rows(J - 1)("ManualRefNo")) > 1 Then
                        mList += Convert.ToString(AgL.VNull(DtData.Rows(J)("ManualRefNo")) - 1) + ", "
                    End If
                End If
            Next
            DsTemp.Tables(0).Rows(I)("MissingVoucherList") = mList
        Next

        FGetDataFromTables = DsTemp
    End Function

    Private Function FGetDataFromLedgerTables(bTableName As String) As DataSet
        Dim DsTemp As DataSet
        Dim mCondStr As String

        mCondStr = " Where 1=1 "
        mCondStr = mCondStr & " And Date(H.V_Date) >= " & AgL.Chk_Date(ReportFrm.FGetText(rowFromDate)) & " 
                         And Date(H.V_Date) <= " & AgL.Chk_Date(ReportFrm.FGetText(rowToDate)) & " "
        mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("H.Site_Code", rowSite), "''", "'")
        mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("H.Div_Code", rowDivision), "''", "'")

        If bTableName = "LedgerHead" Then
            mCondStr = mCondStr & " And Vt.NCat Not In ('" & Ncat.PurchaseInvoice & "','" & Ncat.PurchaseReturn & "')"
        End If

        mQry = " SELECT H.V_Type, H.Div_Code, H.Site_Code,
                    Max(Vt.Description) As VoucherType, Max(Sm.Name) As Site, 
                    Max(D.Div_Name) As Division,
                    Min(CAST(H.ManualRefNo AS Int)) As StartDocNo,
                    Max(CAST(H.ManualRefNo AS Int)) As EndDocNo,
                    '' As MissingVoucherList
                    FROM " & bTableName & " H 
                    LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type 
                    LEFT JOIN SiteMast Sm On H.Site_Code = Sm.Code 
                    LEFT JOIN Division D On H.Div_Code = D.Div_Code " & mCondStr &
                " GROUP BY H.V_Type, H.Div_Code, H.Site_Code "
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        For I As Integer = 0 To DsTemp.Tables(0).Rows.Count - 1
            mQry = "SELECT Max(Vt.Description) +' Ledger' AS Description, CAST(Max(H.RecId) AS Int) As ManualRefNo
                        FROM Ledger H 
                        LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type
                        WHERE Date(H.V_Date) >= " & AgL.Chk_Date(ReportFrm.FGetText(rowFromDate)) & " 
                        And Date(H.V_Date) <= " & AgL.Chk_Date(ReportFrm.FGetText(rowToDate)) & "
                        AND H.V_Type = '" & DsTemp.Tables(0).Rows(I)("V_Type") & "' 
                        AND H.Site_Code = '" & DsTemp.Tables(0).Rows(I)("Site_Code") & "' 
                        AND H.DivCode = '" & DsTemp.Tables(0).Rows(I)("Div_Code") & "' 
                        GROUP BY H.DocId
                        ORDER BY CAST(Max(H.RecId) AS Int) "
            Dim DtData As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

            Dim mList As String = ""
            For J As Integer = 0 To DtData.Rows.Count - 1
                If J > 0 Then
                    If AgL.VNull(DtData.Rows(J)("ManualRefNo")) - AgL.VNull(DtData.Rows(J - 1)("ManualRefNo")) > 1 Then
                        mList += Convert.ToString(AgL.VNull(DtData.Rows(J)("ManualRefNo")) - 1) + ", "
                    End If
                End If
            Next
            DsTemp.Tables(0).Rows(I)("MissingVoucherList") = mList
        Next

        FGetDataFromLedgerTables = DsTemp
    End Function
End Class
