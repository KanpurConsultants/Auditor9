Imports System.IO
Imports AgLibrary
Imports AgLibrary.ClsMain
Imports AgLibrary.ClsMain.agConstants
Imports Microsoft.Reporting.WinForms
Public Class ClsPurchaseAgentCommissionOnPayment

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
    Dim rowCommissionPer As Integer = 3
    Dim rowParty As Integer = 4
    Dim rowVoucherType As Integer = 5
    Dim rowAgent As Integer = 6
    Dim rowCity As Integer = 7
    Dim rowState As Integer = 8
    Dim rowSite As Integer = 9
    Dim rowDivision As Integer = 10
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
            mQry = "Select 'Payment Wise Detail' as Code, 'Payment Wise Detail' as Name 
                            Union All Select 'Party Wise Summary' as Code, 'Party Wise Summary' as Name 
                            Union All Select 'Agent Wise Summary' as Code, 'Agent Wise Summary' as Name "
            ReportFrm.CreateHelpGrid("Report Type", "Report Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mQry, "Agent Wise Summary")
            ReportFrm.CreateHelpGrid("FromDate", "From Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubStartDate)
            ReportFrm.CreateHelpGrid("ToDate", "To Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubEndDate)
            ReportFrm.CreateHelpGrid("Commission %", "Commission %", FrmRepDisplay.FieldFilterDataType.NumericType, FrmRepDisplay.FieldDataType.FloatType, "", "1")
            ReportFrm.CreateHelpGrid("Party", "Party", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpPartyQry)
            ReportFrm.CreateHelpGrid("VoucherType", "Voucher Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, ClsMain.FGetVoucher_TypeQry("PurchInvoice"))
            ReportFrm.CreateHelpGrid("Agent", "Agent", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpPurchaseAgentQry)
            ReportFrm.CreateHelpGrid("City", "City", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpCityQry)
            ReportFrm.CreateHelpGrid("State", "State", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpStateQry)
            ReportFrm.CreateHelpGrid("Site", "Site", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSiteQry, "[SITECODE]")
            ReportFrm.CreateHelpGrid("Division", "Division", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpDivisionQry, "[DIVISIONCODE]")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub ObjRepFormGlobal_ProcessReport() Handles ReportFrm.ProcessReport
        ProcPurchaseAgentCommissionReport()
    End Sub
    Public Sub New(ByVal mReportFrm As FrmRepDisplay)
        ReportFrm = mReportFrm
    End Sub
    Public Sub ProcPurchaseAgentCommissionReport(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try
            Dim mCondStr$ = ""
            Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"
            Dim mCommissionPer As Double


            RepTitle = "Purchase Agent Commission Report"

            If mFilterGrid IsNot Nothing And mGridRow IsNot Nothing Then
                If mGridRow.DataGridView.Columns.Contains("Search Code") = True Then
                    If mFilterGrid.Item(GFilter, rowReportType).Value = "Agent Wise Summary" Then
                        mFilterGrid.Item(GFilter, rowReportType).Value = "Party Wise Summary"
                        mFilterGrid.Item(GFilter, rowAgent).Value = mGridRow.Cells("Agent").Value
                        mFilterGrid.Item(GFilterCode, rowAgent).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                    ElseIf mFilterGrid.Item(GFilter, rowReportType).Value = "Party Wise Summary" Then
                        mFilterGrid.Item(GFilter, rowReportType).Value = "Payment Wise Detail"
                        mFilterGrid.Item(GFilter, rowParty).Value = mGridRow.Cells("Party").Value
                        mFilterGrid.Item(GFilterCode, rowParty).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                    ElseIf mFilterGrid.Item(GFilter, rowReportType).Value = "Payment Wise Detail" Then
                        ClsMain.FOpenForm(mGridRow.Cells("Search Code").Value, ReportFrm)
                        ReportFrm.FiterGridCopy_Arr.RemoveAt(ReportFrm.FiterGridCopy_Arr.Count - 1)
                        Exit Sub
                    Else
                        Exit Sub
                    End If
                Else
                    Exit Sub
                End If
            End If

            mCondStr = "  "
            mCondStr = mCondStr & "  "
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Subcode", rowParty)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.V_Type", rowVoucherType)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("LTV.Agent", rowAgent)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("City.CityCode", rowCity)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("City.State", rowState)
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("L.Site_Code", rowSite), "''", "'")
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("L.DivCode", rowDivision), "''", "'")

            mCommissionPer = Val(ReportFrm.FGetText(rowCommissionPer))

            mQry = " SELECT L.DocID, strftime('%d/%m/%Y', L.V_Date) As V_Date, L.V_Date As V_Date_ActualFormat,
                    L.Subcode as Party, Party.Name As PartyName, LTV.Agent As AgentCode, Agent.Name As AgentName , 
                    City.CityCode, City.CityName, State.Code As StateCode, State.Description As StateName,
                    L.DivCode || L.Site_Code || '-' || L.V_Type || '-' || L.RecId as VoucherNo, L.RecId, 
                    L.AmtDr as Amount, 0.00 AS JvAmount, " & mCommissionPer & " as CommissionPer, L.AmtDr*" & mCommissionPer & "/100 as Commission
                    FROM Ledger L                     
                    Left Join viewHelpSubgroup Party On L.Subcode = Party.Code 
                    Left Join (Select SILTV.Subcode, Max(SILTV.Agent) as Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode) as LTV On Party.code = LTV.Subcode
                    Left Join viewHelpSubGroup Agent On LTV.Agent = Agent.Code 
                    Left Join City On Party.CityCode = City.CityCode 
                    Left Join State On City.State = State.Code
                    LEFT JOIN Voucher_Type Vt On L.V_Type = Vt.V_Type 
                    Where VT.Category='PMT' And L.AmtDr > 0 
                    AND Date(L.V_Date) Between " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowFromDate)).ToString("s")) & " And " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowToDate)).ToString("s")) & "
                    " & mCondStr
            mQry = mQry & " Union All "
            mQry = mQry & " SELECT L.DocID, strftime('%d/%m/%Y', L.V_Date) As V_Date, L.V_Date As V_Date_ActualFormat,
                    L.Subcode as Party, Party.Name As PartyName, LTV.Agent As AgentCode, Agent.Name As AgentName , 
                    City.CityCode, City.CityName, State.Code As StateCode, State.Description As StateName,
                    L.DivCode || L.Site_Code || '-' || L.V_Type || '-' || L.RecId as VoucherNo, L.RecId, 
                    0 as Amount, L.AmtCr as JVAmount, " & mCommissionPer & " as CommissionPer, -1.0*(L.AmtCr*" & mCommissionPer & "/100) as Commission
                    FROM Ledger L                     
                    Left Join viewHelpSubgroup Party On L.Subcode = Party.Code 
                    Left Join (Select SILTV.Subcode, Max(SILTV.Agent) as Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode) as LTV On Party.code = LTV.Subcode
                    Left Join viewHelpSubGroup Agent On LTV.Agent = Agent.Code 
                    Left Join City On Party.CityCode = City.CityCode 
                    Left Join State On City.State = State.Code
                    LEFT JOIN Voucher_Type Vt On L.V_Type = Vt.V_Type 
                    Where VT.Category='JV' And L.AmtCr > 0 
                    AND Date(L.V_Date) Between " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowFromDate)).ToString("s")) & " And " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowToDate)).ToString("s")) & "
                    " & mCondStr



            If ReportFrm.FGetText(rowReportType) = "Payment Wise Detail" Then
                mQry = " Select VMain.DocId As SearchCode, Max(VMain.V_Date) As VoucherDate, Max(VMain.VoucherNo) as VoucherNo,
                    Max(VMain.PartyName) As Party, Max(VMain.AgentName) As [Agent], Sum(VMain.Amount) as Amount, Sum(Vmain.JvAmount) as JvAmount, Max(VMain.CommissionPer) as CommissionPer, Sum(VMain.Commission) as CommissionAmt
                    From (" & mQry & ") As VMain
                    GROUP By VMain.DocId 
                    Order By VoucherDate, VoucherNo  "
            ElseIf ReportFrm.FGetText(rowReportType) = "Agent Wise Summary" Then
                mQry = " Select VMain.AgentCode As SearchCode, Max(VMain.AgentName) As [Agent], 
                    Sum(VMain.Amount) As [Amount], Sum(Vmain.JvAmount) as JvAmount, Max(VMain.CommissionPer) as [CommissionPer], Sum(VMain.Commission) As [CommissionAmt]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.AgentCode 
                    Order By [Agent]"
            ElseIf ReportFrm.FGetText(rowReportType) = "Party Wise Summary" Then
                mQry = " Select VMain.Party As SearchCode, Max(VMain.PartyName) as [Party], Max(VMain.AgentName) As [Agent], 
                    Sum(VMain.Amount) As [Amount], Sum(Vmain.JvAmount) as JvAmount, Max(VMain.CommissionPer) as [CommissionPer], Sum(VMain.Commission) As [CommissionAmt]
                    From (" & mQry & ") As VMain
                    GROUP By VMain.Party
                    Order By [Party]"
            End If




            DsHeader = AgL.FillData(mQry, AgL.GCn)

            If DsHeader.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            ReportFrm.Text = "Purchase Agent Commission On Payment - " + ReportFrm.FGetText(rowReportType)
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = "ProcPurchaseAgentCommissionReport"

            ReportFrm.ProcFillGrid(DsHeader)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsHeader = Nothing
        End Try
    End Sub
End Class
