Imports System.IO
Imports AgLibrary
Imports AgLibrary.ClsMain
Imports AgLibrary.ClsMain.agConstants
Imports Microsoft.Reporting.WinForms
Public Class ClsPaymentAndReceiptReport

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
    Dim rowGroupOn As Integer = 1
    Dim rowFromDate As Integer = 2
    Dim rowToDate As Integer = 3
    Dim rowParty As Integer = 4
    Dim rowCashBank As Integer = 5
    Dim rowVoucherType As Integer = 6
    Dim rowCity As Integer = 7
    Dim rowState As Integer = 8
    Dim rowSite As Integer = 9
    Dim rowDivision As Integer = 10
    Dim rowAgent As Integer = 11
    Dim rowArea As Integer = 12
    Dim rowAccountNature As Integer = 13

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
    Dim mHelpAccountNatureQry$ = "Select Distinct 'o' As Tick, Sg.Nature as Code, Sg.Nature as Description  FROM Subgroup Sg "
    Public Sub Ini_Grid()
        Try
            mQry = "Select 'Voucher Wise Detail' as Code, 'Voucher Wise Detail' as Name                             
                    Union All Select 'Summary' as Code, 'Summary' as Name "
            ReportFrm.CreateHelpGrid("Report Type", "Report Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mQry, "Voucher Wise Detail")
            mQry = "SELECT 'o' As Tick, 'Month' As Code, 'Month' As Name 
                    UNION ALL 
                    SELECT 'o' As Tick, 'V_Date' As Code, 'Date' As Name 
                    UNION ALL 
                    SELECT 'o' As Tick, 'PartyCode' As Code, 'Party' As Name
                    UNION ALL 
                    SELECT 'o' As Tick, 'AccountNature' As Code, 'Account Nature' As Name"
            ReportFrm.CreateHelpGrid("GroupOn", "Group On", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mQry, "")
            ReportFrm.CreateHelpGrid("FromDate", "From Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubStartDate)
            ReportFrm.CreateHelpGrid("ToDate", "To Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubEndDate)
            ReportFrm.CreateHelpGrid("Party", "Party", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpPartyQry)
            mQry = "SELECT 'Cash' As Code, 'Cash' As Name 
                    UNION ALL 
                    SELECT 'Bank' As Code, 'Bank' As Name 
                    UNION ALL 
                    SELECT 'Both' As Code, 'Both' As Name"
            ReportFrm.CreateHelpGrid("CashBank", "Cash / Bank", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mQry, "Both")
            ReportFrm.CreateHelpGrid("VoucherType", "Voucher Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, ClsMain.FGetVoucher_TypeQry("LedgerHead", EntryNCat))
            ReportFrm.CreateHelpGrid("City", "City", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpCityQry)
            ReportFrm.CreateHelpGrid("State", "State", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpStateQry)
            ReportFrm.CreateHelpGrid("Site", "Site", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSiteQry, "[SITECODE]")
            ReportFrm.CreateHelpGrid("Division", "Division", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpDivisionQry, "[DIVISIONCODE]")

            mQry = " Select 'o' As Tick,  Sg.Code, Sg.Name AS Agent FROM viewHelpSubgroup Sg Where Sg.SubgroupType = '" & SubgroupType.SalesAgent & "' "
            ReportFrm.CreateHelpGrid("Agent", "Agent", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mQry)

            mQry = "Select 'o' As Tick, Code, Description From Area "
            ReportFrm.CreateHelpGrid("Area", "Area", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mQry)
            ReportFrm.CreateHelpGrid("Account Nature", "Account Nature", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpAccountNatureQry)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub ObjRepFormGlobal_ProcessReport() Handles ReportFrm.ProcessReport
        ProcPaymentAndReceiptReport()
    End Sub
    Public Sub New(ByVal mReportFrm As FrmRepDisplay, ByVal strNCat As String)
        ReportFrm = mReportFrm
        EntryNCat = strNCat
    End Sub
    Public Sub ProcPaymentAndReceiptReport(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try
            Dim mCondStr$ = ""
            Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"
            Dim mMainQry As String = ""


            If EntryNCat.Contains("Payment") Then
                RepTitle = "Payment Report - " + ReportFrm.FGetText(rowReportType)                
            ElseIf EntryNCat.Contains("Receipt") Then
                RepTitle = "Money Receipt Report - " + ReportFrm.FGetText(rowReportType)
            End If

            Dim bGroupOn As String = ""
            If ReportFrm.FGetCode(rowGroupOn) <> "" Then
                bGroupOn = ReportFrm.FGetCode(rowGroupOn).ToString.Replace("'", "")
            Else
                bGroupOn = ""
            End If

            Dim bNcat As String = Replace(EntryNCat, ",", "','")
            mCondStr = "   And Party.Nature Not In ('Cash','Bank') "
            mCondStr = mCondStr & " AND Date(H.V_Date) Between " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowFromDate)).ToString("s")) & " And " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowToDate)).ToString("s")) & " "
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("Party.code", rowParty)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.V_Type", rowVoucherType)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("City.CityCode", rowCity)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("City.State", rowState)
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("H.Site_Code", rowSite), "''", "'")
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("H.Div_Code", rowDivision), "''", "'")
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("Agent.Code", rowAgent), "''", "'")
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("IfNull(LinkedParty.Area,Party.Area)", rowArea), "''", "'")
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("Party.Nature", rowAccountNature)

            If ReportFrm.FGetText(rowCashBank) <> "Both" Then
                mCondStr = mCondStr & " And CashBank.Nature = '" & ReportFrm.FGetText(rowCashBank) & "'"
            Else
                mCondStr = mCondStr & " And CashBank.Nature In ('Cash','Bank')"
            End If

            mMainQry = " SELECT H.DocID, strftime('%d/%m/%Y', H.V_Date) As V_Date, H.V_Date As V_Date_ActualFormat,
                    L.Subcode as PartyCode, Party.Name As PartyName, Party.Nature as AccountNature, LinkedParty.Name as LinkedPartyName, CashBank.Name As PaymentAccount,
                    LTV.Agent As AgentCode, Agent.Name As AgentName , 
                    City.CityCode, City.CityName, State.Code As StateCode, State.Description As StateName,
                    H.Div_Code || H.Site_Code || '-' || H.V_Type || '-' || H.ManualRefNo as VoucherNo, H.ManualRefNo RecId, L.Amount as Amount, 
                    (Case  When IfNull(Trd.Type,'')='Cancelled' OR IfNull(Trr.Type,'')='Cancelled' Then 0 Else L.Amount End) as NetReceipt, "
            'If ClsMain.FDivisionNameForCustomization(20) = "SHYAMA SHYAM FABRICS" Or ClsMain.FDivisionNameForCustomization(22) = "W SHYAMA SHYAM FABRICS" Or ClsMain.FDivisionNameForCustomization(25) = "SHYAMA SHYAM VENTURES LLP" Or ClsMain.FDivisionNameForCustomization(27) = "W SHYAMA SHYAM VENTURES LLP" Then
            '    mMainQry = mMainQry & "(Case  When IfNull(Trd.Type,'')='Cancelled' OR IfNull(Trr.Type,'')='Cancelled' Then 0 Else L.Amount End) as Amount, "
            'Else
            '    mMainQry = mMainQry & "L.Amount as Amount, "
            'End If
            mMainQry = mMainQry & "L.ChqRefNo as ChqRefNo,strftime('%d/%m/%Y', L.ChqRefDate) As ChqRefDate , L.Remarks Narration, H.Remarks, 
                    " & IIf(AgL.PubServerName = "", "strftime('%m-%Y',H.V_Date)  ", "Substring(Convert(NVARCHAR, H.V_Date,103),4,7)") & " As Month
                    FROM LedgerHeadDetail L                     
                    Left Join LedgerHead H On L.DocID = H.DocID
                    Left Join viewHelpSubgroup CashBank On H.Subcode = CashBank.Code 
                    Left Join viewHelpSubgroup Party On L.Subcode = Party.Code 
                    Left Join viewHelpSubgroup LinkedParty On L.LinkedSubcode = LinkedParty.Code 
                    Left Join (Select SILTV.Subcode, Max(SILTV.Agent) as Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode) as LTV On Party.code = LTV.Subcode
                    Left Join viewHelpSubGroup Agent On LTV.Agent = Agent.Code 
                    Left Join City On Party.CityCode = City.CityCode 
                    Left Join State On City.State = State.Code
                    LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type "
            'If ClsMain.FDivisionNameForCustomization(20) = "SHYAMA SHYAM FABRICS" Or ClsMain.FDivisionNameForCustomization(22) = "W SHYAMA SHYAM FABRICS" Or ClsMain.FDivisionNameForCustomization(25) = "SHYAMA SHYAM VENTURES LLP" Or ClsMain.FDivisionNameForCustomization(27) = "W SHYAMA SHYAM VENTURES LLP" Then
            mMainQry = mMainQry & " Left Join TransactionReferences Trd With (NoLock) On H.DocID = Trd.DocId And Trd.DocIDSr=1 and IfNull(Trd.Type,'')='Cancelled' And H.V_Date >= '2019-07-01'
                    Left Join TransactionReferences Trr With (NoLock) On H.DocID = Trr.ReferenceDocId And Trr.ReferenceSr=1 And IfNull(Trr.Type,'')='Cancelled' And H.V_Date >= '2019-07-01' "
            'End If

            mMainQry = mMainQry & " Where 1=1 And VT.NCat In ('" & bNcat & "')  " & mCondStr

            mMainQry = mMainQry & " Union All SELECT H.DocID, strftime('%d/%m/%Y', H.V_Date) As V_Date, H.V_Date As V_Date_ActualFormat,
                    L.Subcode as PartyCode, Party.Name As PartyName, Party.Nature as AccountNature, LinkedParty.Name as LinkedPartyName, CashBank.Name As PaymentAccount,
                    LTV.Agent As AgentCode, Agent.Name As AgentName , 
                    City.CityCode, City.CityName, State.Code As StateCode, State.Description As StateName,
                    H.Div_Code || H.Site_Code || '-' || H.V_Type || '-' || H.ManualRefNo as VoucherNo, H.ManualRefNo RecId, 
                    L.Amount as Amount, L.Amount as NetReceipt, L.ChqRefNo as ChqRefNo,strftime('%d/%m/%Y', L.ChqRefDate) As ChqRefDate, L.Remarks Narration, H.Remarks, 
                    " & IIf(AgL.PubServerName = "", "strftime('%m-%Y',H.V_Date)  ", "Substring(Convert(NVARCHAR, H.V_Date,103),4,7)") & " As Month
                    FROM LedgerHeadDetail L                     
                    Left Join LedgerHead H On L.DocID = H.DocID
                    Left Join viewHelpSubgroup CashBank On L.Subcode = CashBank.Code 
                    Left Join viewHelpSubgroup Party On H.Subcode = Party.Code 
                    Left Join viewHelpSubgroup LinkedParty On L.LinkedSubcode = LinkedParty.Code 
                    Left Join (Select SILTV.Subcode, Max(SILTV.Agent) as Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode) as LTV On Party.code = LTV.Subcode
                    Left Join viewHelpSubGroup Agent On LTV.Agent = Agent.Code 
                    Left Join City On Party.CityCode = City.CityCode 
                    Left Join State On City.State = State.Code
                    LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type Where 1=1 And VT.NCat In ('" & bNcat & "')  " & mCondStr

            mMainQry = mMainQry & " Union All SELECT H.DocID, strftime('%d/%m/%Y', H.V_Date) As V_Date, H.V_Date As V_Date_ActualFormat,
                    L.Subcode as PartyCode, Party.Name As PartyName, Party.Nature as AccountNature, LinkedParty.Name as LinkedPartyName, CashBank.Name As PaymentAccount,
                    LTV.Agent As AgentCode, Agent.Name As AgentName , 
                    City.CityCode, City.CityName, State.Code As StateCode, State.Description As StateName,
                    H.Div_Code || H.Site_Code || '-' || H.V_Type || '-' || H.RecID as VoucherNo, H.RecId, 
                    L.AmtDr+L.AmtCr as Amount, L.AmtDr+L.AmtCr NetReceipt, L.Chq_No as ChqRefNo,strftime('%d/%m/%Y', L.Chq_Date) As ChqRefDate , L.Narration, H.Narration Remarks, 
                    " & IIf(AgL.PubServerName = "", "strftime('%m-%Y',H.V_Date)  ", "Substring(Convert(NVARCHAR, H.V_Date,103),4,7)") & " As Month
                    FROM Ledger L                     
                    Left Join LedgerM H On L.DocID = H.DocID
                    Left Join viewHelpSubgroup CashBank On H.Subcode = CashBank.Code 
                    Left Join viewHelpSubgroup Party On L.Subcode = Party.Code 
                    Left Join viewHelpSubgroup LinkedParty On L.LinkedSubcode = LinkedParty.Code 
                    Left Join (Select SILTV.Subcode, Max(SILTV.Agent) as Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode) as LTV On Party.code = LTV.Subcode
                    Left Join viewHelpSubGroup Agent On LTV.Agent = Agent.Code 
                    Left Join City On Party.CityCode = City.CityCode 
                    Left Join State On City.State = State.Code
                    Left Join (Select DocID 
                               From LedgerHead sLH 
                               Left Join Voucher_Type sVT On sLH.V_Type = sVT.V_Type 
                               Where NCat In ('" & Ncat.Receipt & "','" & Ncat.Payment & "', '" & Ncat.VisitReceipt & "')) as RH On L.ReferenceDocID = RH.DocID
                    LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type  Where RH.DocId is Null And (VT.NCat In ('" & bNcat & "') or VT.Category In ('" & bNcat & "'))  " & mCondStr


            If ReportFrm.FGetText(rowReportType) = "Voucher Wise Detail" Then
                mQry = " Select VMain.DocId As SearchCode, VMain.V_Date As VoucherDate, VMain.VoucherNo as VoucherNo,
                    VMain.PartyName As Party, VMain.LinkedPartyName as LinkedParty, 
                    VMain.PaymentAccount, VMain.Amount as Amount, VMain.NetReceipt, VMain.ChqRefNo, VMain.ChqRefDate, VMain.Narration as Narration, VMain.Remarks as Remarks
                    From (" & mMainQry & ") As VMain
                    Order By Vmain.V_Date_ActualFormat, Vmain.VoucherNo  "
            ElseIf ReportFrm.FGetText(rowReportType) = "Summary" Then
                mQry = " Select Max(VMain.DocId) As SearchCode
                    " & IIf(bGroupOn.Contains("Month"), ", " & IIf(AgL.PubServerName = "", "Max(strftime('%m-%Y',VMain.V_Date_ActualFormat))", "Substring(Convert(NVARCHAR, VMain.V_Date_ActualFormat,103),4,7)") & " as Month", "") & " 
                    " & IIf(bGroupOn.Contains("Date"), ", Max(VMain.V_Date) as Date", "") & " 
                    " & IIf(bGroupOn.Contains("DivisionCode"), ", DivisionCode, Max(VMain.DivisionName) as Division", "") & " 
                    " & IIf(bGroupOn.Contains("PartyCode"), ", PartyCode, Max(VMain.PartyName) as Party", "") & " 
                    " & IIf(bGroupOn.Contains("AccountNature"), ", Max(Sg.Nature) as AccountNature", "") & " 
                    ,Sum(VMain.Amount) as Amount
                    ,Sum(VMain.NetReceipt) as NetReceipt
                    From (" & mMainQry & ") As VMain
                    Left Join Subgroup Sg On VMain.PartyCode = Sg.Subcode
                    GROUP By " & bGroupOn & ""

                Dim mOrderBy As String = ""
                mOrderBy += IIf(bGroupOn.Contains("Month"), "Month,", "")
                mOrderBy += IIf(bGroupOn.Contains("Date"), "V_Date,", "")
                mOrderBy += IIf(bGroupOn.Contains("DivisionCode"), "Division,", "")
                mOrderBy += IIf(bGroupOn.Contains("PartyCode"), "Party,", "")
                mOrderBy += IIf(bGroupOn.Contains("AccountNature"), "Max(Sg.Nature),", "")
                mQry = mQry + " Order By " + mOrderBy.Substring(0, mOrderBy.Length - 1)
            End If
            DsHeader = AgL.FillData(mQry, AgL.GCn)

            If DsHeader.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            If EntryNCat.Contains("Payment") Then
                ReportFrm.Text = "Payment Report - " + ReportFrm.FGetText(rowReportType)
            ElseIf EntryNCat.Contains("Receipt") Then
                ReportFrm.Text = "Money Receipt Report - " + ReportFrm.FGetText(rowReportType)
            End If
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = "ProcPaymentAndReceiptReport"

            ReportFrm.ProcFillGrid(DsHeader)

            If ReportFrm.DGL1.Columns.Contains("Division Code") Then ReportFrm.DGL1.Columns("Division Code").Visible = False
            If ReportFrm.DGL1.Columns.Contains("Party Code") Then ReportFrm.DGL1.Columns("Party Code").Visible = False
        Catch ex As Exception
            MsgBox(ex.Message)
            DsHeader = Nothing
        End Try
    End Sub
End Class
