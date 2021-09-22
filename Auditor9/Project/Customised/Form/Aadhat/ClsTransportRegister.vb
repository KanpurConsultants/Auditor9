Imports System.IO
Imports AgLibrary
Imports AgLibrary.ClsMain
Imports AgLibrary.ClsMain.agConstants
Imports Microsoft.Reporting.WinForms
Public Class ClsTransporterRegister

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
    Dim ReportType As String = ""

    Dim DsHeader As DataSet = Nothing

    Dim rowReportType As Integer = 0
    Dim rowFromDate As Integer = 1
    Dim rowToDate As Integer = 2
    Dim rowTransporter As Integer = 3
    Dim rowCity As Integer = 4
    Dim rowSite As Integer = 5
    Dim rowDivision As Integer = 6
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
    Public Shared mHelpTransporterQry$ = " Select 'o' As Tick, Sg.Code, Sg.Name AS Agent FROM viewHelpSubgroup Sg Where Sg.SubgroupType = '" & SubgroupType.Transporter & "' "
    Public Sub Ini_Grid()
        Try
            mQry = "Select 'Format-1' as Code, 'Format-1' as Name 
                    Union All Select 'Format-2' as Code, 'Format-2' as Name "
            ReportFrm.CreateHelpGrid("Report Type", "Report Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mQry, ReportType)
            ReportFrm.CreateHelpGrid("FromDate", "From Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubStartDate)
            ReportFrm.CreateHelpGrid("ToDate", "To Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubEndDate)
            ReportFrm.CreateHelpGrid("Transporter", "Transporter", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpTransporterQry)
            ReportFrm.CreateHelpGrid("City", "City", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpCityQry)
            ReportFrm.CreateHelpGrid("Site", "Site", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSiteQry, "[SITECODE]")
            ReportFrm.CreateHelpGrid("Division", "Division", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpDivisionQry, "[DIVISIONCODE]")
            ReportFrm.FilterGrid.Rows(rowReportType).Visible = False
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub ObjRepFormGlobal_ProcessReport() Handles ReportFrm.ProcessReport
        ProcTransporterRegister()
    End Sub
    Public Sub New(ByVal mReportFrm As FrmRepDisplay, ByVal mReportType As String)
        ReportFrm = mReportFrm
        ReportType = mReportType
    End Sub
    Public Sub ProcTransporterRegister(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try
            Dim mCondStr$ = ""
            Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"

            If mFilterGrid IsNot Nothing And mGridRow IsNot Nothing Then
                If mGridRow.DataGridView.Columns.Contains("Search Code") = True Then
                    ClsMain.FOpenForm(mGridRow.Cells("Search Code").Value, ReportFrm)
                    ReportFrm.FiterGridCopy_Arr.RemoveAt(ReportFrm.FiterGridCopy_Arr.Count - 1)
                    Exit Sub
                Else
                    Exit Sub
                End If
            End If


            mCondStr = " Where 1=1"
            mCondStr = mCondStr & " And Vt.NCat = '" & Ncat.SaleInvoice & "'"
            mCondStr = mCondStr & " AND Date(H.V_Date) Between " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowFromDate)).ToString("s")) & " And " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowToDate)).ToString("s")) & " "
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("SIT.Transporter", rowTransporter)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("Sg.CityCode", rowCity)
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("H.Site_Code", rowSite), "''", "'")
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("H.Div_Code", rowDivision), "''", "'")


            If ReportFrm.FGetText(rowReportType) = "Format-1" Then
                mQry = " SELECT H.DocId As SearchCode, H.V_Date AS BillDate, H.ManualRefNo AS BillNo, Sit.LrNo AS BiltyNo, Sit.NoOfBales AS Nag, Sit.Freight,
                        H.SaleToPartyName As PartyName, Sg.Name AS Supplier, Sit.Destination As Station
                        FROM SaleInvoiceTransport Sit
                        LEFT JOIN SaleInvoice H ON Sit.DocID = H.DocID
                        LEFT JOIN (
	                        SELECT L.DocID, Max(L.Item) As Item
	                        FROM SaleInvoiceDetail L 
	                        GROUP BY L.DocID
                        ) AS VLine ON H.DocId = VLine.DocId
                        LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                        LEFT JOIN Item I ON VLine.Item = I.Code 
                        LEFT JOIN ItemGroup Ig On I.ItemGroup = Ig.Code
                        LEFT JOIN ViewHelpSubgroup Sg ON Ig.DefaultSupplier = Sg.Code " & mCondStr
            ElseIf ReportFrm.FGetText(rowReportType) = "Format-2" Then
                mQry = "SELECT H.DocId As SearchCode, H.V_Date AS BillDate, H.ManualRefNo AS BillNo, H.SaleToPartyName As PartyName, ShipTo.Name as ShipToParty, 
                        I.Description AS Product, Sit.LrNo AS BiltyNo, Sit.NoOfBales AS Nag, H.Net_Amount As Amount, VLine.LRemarks as Remarks
                        FROM SaleInvoiceTransport Sit
                        LEFT JOIN SaleInvoice H ON Sit.DocID = H.DocID
                        LEFT JOIN (
                            SELECT L.DocID, Max(L.Item) As Item, Max(L.Remark) as LRemarks
                            FROM SaleInvoiceDetail L 
                            GROUP BY L.DocID
                        ) AS VLine ON H.DocId = VLine.DocId
                        LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                        LEFT JOIN Item I ON VLine.Item = I.Code 
                        LEFT JOIN ItemGroup Ig On I.ItemGroup = Ig.Code
                        LEFT JOIN ViewHelpSubgroup Sg ON Ig.DefaultSupplier = Sg.Code 
                        LEFT JOIN ViewHelpSubgroup ShipTo ON H.ShipToParty = ShipTo.Code " & mCondStr & "
                        Order By H.V_Date, H.ManualRefNo "
            End If
            DsHeader = AgL.FillData(mQry, AgL.GCn)

            If DsHeader.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            ReportFrm.Text = "Transporter Register"
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = "ProcTransporterRegister"
            ReportFrm.ProcFillGrid(DsHeader)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsHeader = Nothing
        End Try
    End Sub
End Class
