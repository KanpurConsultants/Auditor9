Imports System.IO
Imports AgLibrary
Imports AgLibrary.ClsMain
Imports AgLibrary.ClsMain.agConstants
Imports Microsoft.Reporting.WinForms
Public Class ClsWorkOrderJobStatusReport

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
    Dim rowStatusType As Integer = 2
    Dim rowSite As Integer = 3
    Dim rowDivision As Integer = 4

    Private Const PendingForWorkDispatch As String = "Pending For Work Dispatch"
    Private Const PendingForJobOrder As String = "Pending For Job Order"
    Private Const PendingForJobReceive As String = "Pending For Job Receive"
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
            ReportFrm.CreateHelpGrid("FromDate", "From Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.RetMonthStartDate(AgL.PubLoginDate))
            ReportFrm.CreateHelpGrid("ToDate", "To Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.RetMonthEndDate(AgL.PubLoginDate))

            mQry = "Select '" & PendingForJobOrder & "' as Code, '" & PendingForJobOrder & "' as Name 
                    Union All 
                    Select '" & PendingForJobReceive & "' as Code, '" & PendingForJobReceive & "' as Name 
                    Union All 
                    Select '" & PendingForWorkDispatch & "' as Code, '" & PendingForWorkDispatch & "' as Name 
                    Union All 
                    Select 'All' as Code, 'All' as Name "
            ReportFrm.CreateHelpGrid("Status", "Status", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mQry, PendingForWorkDispatch,,, 300)
            ReportFrm.CreateHelpGrid("Site", "Site", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSiteQry, "[SITECODE]")
            ReportFrm.CreateHelpGrid("Division", "Division", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpDivisionQry, "[DIVISIONCODE]")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub ObjRepFormGlobal_ProcessReport() Handles ReportFrm.ProcessReport
        ProcWorkOrderJobStatus()
    End Sub
    Public Sub New(ByVal mReportFrm As FrmRepDisplay)
        ReportFrm = mReportFrm
    End Sub
    Public Sub ProcWorkOrderJobStatus(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try
            Dim bTableName$ = ""
            Dim mTags As String() = Nothing
            Dim mCondStr$ = ""
            Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"

            RepTitle = "Work Order Job Status"

            If mFilterGrid IsNot Nothing And mGridRow IsNot Nothing Then
                If mGridRow.DataGridView.Columns.Contains("Search Code") = True Then
                    ClsMain.FOpenForm(mGridRow.Cells("Search Code").Value, ReportFrm)
                    ReportFrm.FiterGridCopy_Arr.RemoveAt(ReportFrm.FiterGridCopy_Arr.Count - 1)
                    Exit Sub
                Else
                    Exit Sub
                End If
            End If

            mCondStr = " Where 1=1 "
            mCondStr = mCondStr & " And Date(H.V_Date) >= " & AgL.Chk_Date(ReportFrm.FGetText(rowFromDate)) & " 
                         And Date(H.V_Date) <= " & AgL.Chk_Date(ReportFrm.FGetText(rowToDate)) & " "
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("H.Site_Code", rowSite), "''", "'")
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("H.Div_Code", rowDivision), "''", "'")

            Dim mPendingCondition As String = ""
            If ReportFrm.FGetText(rowStatusType) = PendingForJobOrder Then
                mPendingCondition = " Having Sum(V1.JobOrderQty) = 0 "
            ElseIf ReportFrm.FGetText(rowStatusType) = PendingForJobReceive Then
                mPendingCondition = " Having Sum(V1.JobInvoiceQty) = 0 "
            ElseIf ReportFrm.FGetText(rowStatusType) = PendingForWorkDispatch Then
                mPendingCondition = " Having Sum(V1.WorkInvoiceQty) = 0 "
            End If

            mQry = "SELECT Max(H.V_Type + '-' + H.ManualRefNo) AS WorkOrderNo, Max(H.V_Date) AS WorkOrderDate, 
                    Max(H.SaleToPartyName) As Party, 
                    Max(H.SaleToPartyMobile) As Phone, 
                    Max(Ic.Description) AS ItemCategory, Max(Ig.Description) AS ItemGroup, 
                    Max(I.Description) AS Item, Sum(V1.WorkOrderQty) AS OrderQty, 
                    Sum(V1.JobOrderQty) AS JobOrderQty, Sum(V1.JobInvoiceQty) AS JobReceiveQty, Sum(V1.WorkInvoiceQty) AS DispatchQty   
                    FROM (
	                    SELECT L.DocID, L.Sr, L.Qty AS WorkOrderQty, 0 AS JobOrderQty, 0 AS JobInvoiceQty, 0 AS WorkInvoiceQty   
	                    FROM SaleInvoice H 
	                    LEFT JOIN SaleInvoiceDetail L ON H.DocID = L.DocID
	                    LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type
	                    WHERE Vt.NCat = '" & Ncat.WorkOrder & "'
	
	                    UNION ALL 
	
	                    SELECT L.SaleInvoice AS DocId, L.SaleInvoiceSr AS Sr, 0 AS WorkOrderQty, L.Qty  AS JobOrderQty, 0 AS JobInvoiceQty, 0 AS WorkInvoiceQty   
	                    FROM PurchInvoice H 
	                    LEFT JOIN PurchInvoiceDetail L ON H.DocID = L.DocID
	                    LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type
	                    WHERE Vt.NCat = '" & Ncat.JobOrder & "'
	
	                    UNION ALL 
	
	                    SELECT Pid.SaleInvoice AS DocId, Pid.SaleInvoiceSr AS Sr, 0 AS WorkOrderQty, 0 AS JobOrderQty, L.Qty AS JobInvoiceQty, 0 AS WorkInvoiceQty   
	                    FROM PurchInvoice H 
	                    LEFT JOIN PurchInvoiceDetail L ON H.DocID = L.DocID
	                    LEFT JOIN PurchInvoiceDetail Pid ON L.PurchInvoice = Pid.DocID AND L.PurchInvoiceSr = Pid.Sr
	                    LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type
	                    WHERE Vt.NCat = '" & Ncat.JobInvoice & "'
	
	                    UNION ALL 
	
	                    SELECT L.SaleInvoice AS DocID, L.SaleInvoiceSr AS Sr, 0 AS WorkOrderQty, 0 AS JobOrderQty, 0 AS JobInvoiceQty, L.Qty AS WorkInvoiceQty   
	                    FROM SaleInvoice H 
	                    LEFT JOIN SaleInvoiceDetail L ON H.DocID = L.DocID
	                    LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type
	                    WHERE Vt.NCat = '" & Ncat.WorkInvoice & "'
                    ) AS V1
                    LEFT JOIN SaleInvoiceDetail L ON V1.DocId = L.DocID AND V1.Sr = L.Sr
                    LEFT JOIN SaleInvoice H ON L.DocID = H.DocID
                    LEFT JOIN Item I ON L.Item = I.Code 
                    LEFT JOIN Item Ig On I.ItemGroup = Ig.Code
                    LEFT JOIN Item Ic On IsNull(I.ItemCategory,I.Code) = Ic.Code " & mCondStr &
                    " GROUP BY V1.DocID, V1.Sr " & mPendingCondition &
                    " ORDER BY WorkOrderDate, WorkOrderNo "
            DsHeader = AgL.FillData(mQry, AgL.GCn)

            If DsHeader.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            ReportFrm.Text = "Work Order Job Status"
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = "ProcWorkOrderJobStatus"
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
