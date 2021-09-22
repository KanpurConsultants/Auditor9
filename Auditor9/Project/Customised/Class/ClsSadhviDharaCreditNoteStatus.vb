Imports System.IO
Imports AgLibrary
Imports AgLibrary.ClsMain
Imports AgLibrary.ClsMain.agConstants
Imports Microsoft.Reporting.WinForms
Public Class ClsDharaCreditNoteStatusReport

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
    Dim rowParty As Integer = 5
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
            ReportFrm.CreateHelpGrid("Party", "Party", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpPartyQry,, 600, 650, 300)
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
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try
            Dim bTableName$ = ""
            Dim mTags As String() = Nothing
            Dim mCondStr$ = ""
            Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"

            RepTitle = "Sale Invoice Dhara Status"

            If mFilterGrid IsNot Nothing And mGridRow IsNot Nothing Then
                If mGridRow.DataGridView.Columns.Contains("Search Code") = True Then
                    If mFilterGrid.Item(GFilter, rowReportType).Value = "Summary" Then
                        mFilterGrid.Item(GFilter, rowReportType).Value = "Detail"
                        mFilterGrid.Item(GFilter, rowParty).Value = mGridRow.Cells("Sale Man").Value
                        mFilterGrid.Item(GFilterCode, rowParty).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                    Else
                        ClsMain.FOpenForm(mGridRow.Cells("Search Code").Value, ReportFrm)
                        ReportFrm.FiterGridCopy_Arr.RemoveAt(ReportFrm.FiterGridCopy_Arr.Count - 1)
                        Exit Sub
                    End If
                Else
                    Exit Sub
                End If
            End If

            mCondStr = " And Date(H.V_Date) >= " & AgL.Chk_Date(ReportFrm.FGetText(rowFromDate)) & " 
                         And Date(H.V_Date) <= " & AgL.Chk_Date(ReportFrm.FGetText(rowToDate)) & " "
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("H.Site_Code", rowSite), "''", "'")
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("H.Div_Code", rowDivision), "''", "'")
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.SaleToParty", rowParty)



            mQry = "SELECT H.DocId, strftime('%d/%m/%Y', H.V_Date) As V_Date, H.V_Date As V_Date_ActualFormat, 
                    H.ManualRefNo, L.NetAmount
                    FROM SaleInvoice H
                    LEFT JOIN (SELECT L1.DocID, Sum(L1.Net_Amount) AS NetAmount, Sum(L1.AdditionAmount) AS AdditionAmt
		                       FROM SaleInvoiceDetail L1 GROUP BY L1.DocID) AS L ON H.DocID  = L.DocId		   
                    LEFT JOIN (SELECT L1.SpecificationDocID, Sum(LC.Net_Amount) AS NetAmount 
		                       FROM LedgerHeadDetail L1 
		                       LEFT JOIN LedgerHeadDetailCharges LC ON L1.DocID = LC.DocID AND L1.Sr = LC.Sr 
		                       GROUP BY L1.SpecificationDocID) AS LHD ON H.DocID  = LHD.SpecificationDocId
                    WHERE SID.AdditionAmt > 0 "

            mQry = "SELECT H.DocId, strftime('%d/%m/%Y', H.V_Date) As V_Date, H.V_Date As V_Date_ActualFormat, 
                    H.ManualRefNo, L.SalesRepresentative, Sg.Name AS SaleMan, 
                    Ic.Description As ItemCategory, Ig.Description As ItemGroup,
                    L.Net_Amount As SaleAmount,
                    L.Net_Amount * (CASE WHEN IsNull(Ig.SalesRepresentativeCommissionPer,0) <> 0 
				                    THEN IsNull(Ig.SalesRepresentativeCommissionPer,0)
				                    ELSE IsNull(Ic.SalesRepresentativeCommissionPer,0) END)/100 AS Commission
                    FROM SaleInvoice H 
                    LEFT JOIN SaleInvoiceDetail L ON H.DocID = L.DocID
                    LEFT JOIN Subgroup Sg ON L.SalesRepresentative = Sg.Subcode
                    LEFT JOIN Item I ON L.Item = I.Code
                    LEFT JOIN Item IG ON I.ItemGroup = IG.Code
                    LEFT JOIN Item Ic ON I.ItemCategory = Ic.Code
                    WHERE 1=1 " & mCondStr

            'WHERE L.SalesRepresentative IS NOT NULL " & mCondStr

            If ReportFrm.FGetText(rowReportType) = "Detail" Then
                mQry = " Select VMain.DocId As SearchCode, VMain.ManualRefNo As InvoiceNo, 
                    VMain.V_Date As InvoiceDate, VMain.ItemCategory, VMain.ItemGroup, 
                    VMain.SaleAmount, VMain.Commission
                    From (" & mQry & ") As VMain
                    Order By VMain.V_Date_ActualFormat, Cast(Replace(Vmain.ManualRefNo,'-','') as Integer) "
            ElseIf ReportFrm.FGetText(rowReportType) = "Summary" Then
                mQry = " Select VMain.SalesRepresentative As SearchCode, Max(VMain.SaleMan) As SaleMan, 
                    Sum(VMain.SaleAmount) As SaleAmount, Sum(VMain.Commission) AS Commission
                    From (" & mQry & ") As VMain
                    GROUP By VMain.SalesRepresentative 
                    Order By Max(VMain.V_Date_ActualFormat), Cast(Max(Replace(Vmain.ManualRefNo,'-','')) as Integer) "
            End If


            'mQry = "SELECT L.SalesRepresentative, Max(Sg.Name) AS SaleMan, 
            '        Sum(L.Amount) As SaleAmount,
            '        Sum(L.Amount * (CASE WHEN IsNull(Ig.SalesRepresentativeCommissionPer,0) <> 0 
            '            THEN IsNull(Ig.SalesRepresentativeCommissionPer,0)
            '            ELSE IsNull(Ic.SalesRepresentativeCommissionPer,0) END)/100) AS Commission
            '        FROM SaleInvoice H 
            '        LEFT JOIN SaleInvoiceDetail L ON H.DocID = L.DocID
            '        LEFT JOIN Subgroup Sg ON L.SalesRepresentative = Sg.Subcode
            '        LEFT JOIN Item I ON L.Item = I.Code
            '        LEFT JOIN ItemGroup IG ON I.ItemGroup = IG.Code
            '        LEFT JOIN ItemCategory Ic ON I.ItemCategory = Ic.Code
            '        WHERE L.SalesRepresentative IS NOT NULL " & mCondStr &
            '        " GROUP BY L.SalesRepresentative "
            DsHeader = AgL.FillData(mQry, AgL.GCn)

            If DsHeader.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            ReportFrm.Text = "Sales Man Commission Report"
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = "ProcSalesManCommissionReport"
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
