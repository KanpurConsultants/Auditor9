Imports System.IO
Imports AgLibrary
Imports AgLibrary.ClsMain
Imports AgLibrary.ClsMain.agConstants
Imports Microsoft.Reporting.WinForms
Public Class ClsKiranaPaymentReport


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
    Dim rowParty As Integer = 2
    Dim rowSite As Integer = 3
    Dim rowDivision As Integer = 4
    Dim rowPartyType As Integer = 5
    Dim rowMoreThanDays As Integer = 6
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
    Dim mHelpSubGroupTypeQry$ = "Select 'o' As Tick, SubGroupType As Code, SubGroupType As [Party Type] From SubGroupType Where SubGroupType In ('" & SubgroupType.Customer & "','" & SubgroupType.Supplier & "') "
    Public Sub Ini_Grid()
        Try
            ReportFrm.CreateHelpGrid("FromDate", "From Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubStartDate)
            ReportFrm.CreateHelpGrid("ToDate", "To Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubEndDate)
            ReportFrm.CreateHelpGrid("Party", "Party", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpPartyQry)
            ReportFrm.CreateHelpGrid("Site", "Site", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSiteQry, "[SITECODE]")
            ReportFrm.CreateHelpGrid("Division", "Division", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpDivisionQry, "[DIVISIONCODE]")
            ReportFrm.CreateHelpGrid("PartyType", "Party Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSubGroupTypeQry, "All", 500, 500, 360)
            ReportFrm.CreateHelpGrid("MoreThanDays", "More Than Days", FrmRepDisplay.FieldFilterDataType.NumericType, FrmRepDisplay.FieldDataType.NumericType, "", "90")
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

            RepTitle = "Customer Settlement Report"

            If mFilterGrid IsNot Nothing And mGridRow IsNot Nothing Then
                If mGridRow.DataGridView.Columns.Contains("Search Code") = True Then
                    ClsMain.FOpenForm(mGridRow.Cells("Search Code").Value, ReportFrm)
                    ReportFrm.FiterGridCopy_Arr.RemoveAt(ReportFrm.FiterGridCopy_Arr.Count - 1)
                    Exit Sub
                Else
                    Exit Sub
                End If
            End If

            mCondStr = "  "
            mCondStr = mCondStr & " AND Date(H.V_Date) Between " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowFromDate)).ToString("s")) & " And " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowToDate)).ToString("s")) & " "
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.Subcode", rowParty)
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("H.Site_Code", rowSite), "''", "'")
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("H.Div_Code", rowDivision), "''", "'")
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("Sg.SubGroupType", rowPartyType), "''", "'")




            mQry = "select H.DocID, H.V_Date,Sg.Name as Broker, LSg.Name as SubParty, L.InvoiceAmount, 
                    Adjs.DiscountPer, Adjs.Discount, Adjs.BrokeragePer, Adjs.Brokerage, Adjs.OtherDeductionPer, Adjs.OtherDeduction, Adjs.InterestPer, Adjs.Interest,
                    L.SettlementInvoiceAmount
                    from LedgerHead H
                    Left Join Cloth_SupplierSettlementInvoices L On H.DocId = L.DocID
                    Left Join (
                            select Ad.DocID, Ad.TSr, 
                            Sum(Case When Heads.Description = 'DISCOUNT' Then Ad.Rate Else 0.00 End) as DiscountPer, 
                            Sum(Case When Heads.Description = 'DISCOUNT' Then Ad.Amount Else 0.00 End) as Discount, 
                            Sum(Case When Heads.Description = 'BROKERAGE' Then Ad.Rate Else 0.00 End) as BrokeragePer, 
                            Sum(Case When Heads.Description = 'BROKERAGE' Then Ad.Amount Else 0.00 End) as Brokerage, 
                            Sum(Case When Heads.Description = 'OTHER DEDUCTION' Then Ad.Rate Else 0.00 End) as OtherDeductionPer, 
                            Sum(Case When Heads.Description = 'OTHER DEDUCTION' Then Ad.Amount Else 0.00 End) as OtherDeduction, 
                            Sum(Case When Heads.Description = 'INTEREST' Then Ad.Rate Else 0.00 End) as InterestPer, 
                            Sum(Case When Heads.Description = 'INTEREST' Then Ad.Amount Else 0.00 End) as Interest 
                            from Cloth_SupplierSettlementInvoicesAdjustment Ad
                            Left Join cloth_SupplierSettlementAdjustmentHead Heads On Ad.AdjustmentHead = Heads.Code
                            group By Ad.DocID, Ad.TSr
                            ) as Adjs On L.DocID = Adjs.DocID And L.Sr = Adjs.Tsr
                    Left Join Voucher_Type Vt On H.V_type = VT.V_Type
                    Left Join SaleInvoice SI On L.PurchaseInvoiceDocID = SI.DocID
                    Left Join Subgroup Sg On H.Subcode = Sg.Subcode
                    Left Join Subgroup LSg On SI.LinkedParty = LSg.Subcode
                    where VT.NCat ='RS' " & mCondStr & "

                "

            DsHeader = AgL.FillData(mQry, AgL.GCn)

            If DsHeader.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            ReportFrm.Text = "Payment Settlement Report"
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = "ProcMain"
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
