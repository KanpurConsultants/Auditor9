Imports AgLibrary.ClsMain.agConstants

Public Class ClsDebtorOutstandingReport

    Dim StrArr1() As String = Nothing, StrArr2() As String = Nothing, StrArr3() As String = Nothing, StrArr4() As String = Nothing, StrArr5() As String = Nothing

    Dim mGRepFormName As String = ""
    Dim ErrorLog As String = ""

    Dim WithEvents ReportFrm As AgLibrary.FrmReportLayout

    Public Property GRepFormName() As String
        Get
            GRepFormName = mGRepFormName
        End Get
        Set(ByVal value As String)
            mGRepFormName = value
        End Set
    End Property

    Dim mHelpAreaQry$ = "Select 'o' As Tick, Code, Description From Area "
    Dim mHelpCityQry$ = "Select 'o' As Tick, CityCode, CityName From City "
    Dim mHelpStateQry$ = "Select 'o' As Tick, State_Code, State_Desc From State "
    Dim mHelpSiteQry$ = "Select 'o' As Tick, Code, Name As [Site] From SiteMast Where  Code In (" & AgL.PubSiteList & ")  "
    'Dim mHelpDivisionQry$ = "Select 'o' As Tick, Div_Code as Code, Div_Name As [Division] From Division Where Div_Code In (" & AgL.PubDivisionList & ") "
    Dim mHelpDivisionQry$ = "Select 'o' As Tick, Div_Code as Code, Div_Name As [Division] From Division "
    Dim mHelpPartyQry$ = " Select 'o' As Tick,  Sg.Code, Sg.Name AS Party, Sg.Address, Ag.GroupName FROM viewHelpSubGroup Sg  Left Join AcGroup Ag On Sg.groupCode= ag.GroupCode Where Sg.Nature In ('Customer','Supplier','Cash') "
    Dim mHelpAgentQry$ = " Select 'o' As Tick,  Sg.Code, Sg.Name AS Agent FROM viewHelpSubgroup Sg Where Sg.SubgroupType = '" & SubgroupType.SalesAgent & "' "
    Dim mHelpSalesAgentQry$ = " Select 'o' As Tick, Sg.Code, Sg.Name AS [Responsible Person] FROM viewHelpSubgroup Sg Where Sg.SubgroupType = '" & SubgroupType.SalesAgent & "' "
    Dim mHelpTagQry$ = "Select 'o' As Tick, H.Code, H.Description   FROM Tag H "


    Dim DsRep As DataSet = Nothing, DsRep1 As DataSet = Nothing, DsRep2 As DataSet = Nothing
    Dim mQry$ = "", RepName$ = "", RepTitle$ = "", OrderByStr$ = ""



    Dim StrMonth$ = ""
    Dim StrQuarter$ = ""
    Dim StrFinancialYear$ = ""
    Dim StrTaxPeriod$ = ""



    Private Const rowReportType As Integer = 0
    Private Const rowCalculation As Integer = 1
    Private Const rowAsOnDate As Integer = 2
    Private Const rowGraceDays As Integer = 3
    Private Const rowParty As Integer = 4
    Private Const rowAgent As Integer = 5
    Private Const rowCity As Integer = 6
    Private Const rowArea As Integer = 7
    Private Const rowDivision As Integer = 8
    Private Const rowSite As Integer = 9


    Public Sub Ini_Grid()
        Try
            Dim mQry As String
            Dim I As Integer = 0

            mQry = "Select 'Party Wise Summary' as Code, 'Party Wise Summary' as Name 
                            Union All Select 'Invoice Wise Detail' as Code, 'Invoice Wise Detail' as Name "
            ReportFrm.CreateHelpGrid("Report Type", "Report Type", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.SingleSelection, mQry, "Party Wise Summary")

            mQry = "Select 'FIFO' as Code, 'FIFO' as Name 
                            Union All Select 'Adjustment' as Code, 'Adjustment' as Name "
            ReportFrm.CreateHelpGrid("Calculation", "Calculation", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.SingleSelection, mQry, "FIFO")

            ReportFrm.CreateHelpGrid("As On Date", "As On Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.PubLoginDate)
            ReportFrm.CreateHelpGrid("Grace Days", "Grace Days", AgLibrary.FrmReportLayout.FieldFilterDataType.NumericType, AgLibrary.FrmReportLayout.FieldDataType.NumericType, "", 30)
            ReportFrm.CreateHelpGrid("Party", "Party", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpPartyQry, , 450, 825, 300)
            ReportFrm.CreateHelpGrid("Agent", "Agent", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpAgentQry)
            ReportFrm.CreateHelpGrid("City", "City", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpCityQry)
            ReportFrm.CreateHelpGrid("Area", "Area", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpAreaQry)
            ReportFrm.CreateHelpGrid("Division", "Division", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpDivisionQry, "[DIVISIONCODE]")
            ReportFrm.CreateHelpGrid("Site", "Site", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpSiteQry, "[SITECODE]")

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ObjRepFormGlobal_ProcessReport() Handles ReportFrm.ProcessReport
        ProcDebtorsOutstaningReport()
    End Sub

    Public Sub New(ByVal mReportFrm As AgLibrary.FrmReportLayout)
        ReportFrm = mReportFrm
    End Sub

    Public Sub ProcDebtorsOutstaningReport()
        Try
            Dim mCondStr$ = ""
            Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"
            Dim mLeavergeDays As Double
            Dim strSql As String
            Dim strDate As String
            Dim DsRep As DataSet






            strDate = AgL.Chk_Text(CDate(ReportFrm.FGetText(rowAsOnDate)).ToString("s"))

            mCondStr = "  "
            mCondStr = mCondStr & " AND Date(LG.V_Date) <= " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowAsOnDate)).ToString("s")) & " "
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("LG.Subcode", rowParty)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("Party.GroupCode", 5)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("LTV.Agent", rowAgent)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("CT.CityCode", rowCity)
            'mCondStr = mCondStr & ReportFrm.GetWhereCondition("Ct.State", rows)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("Party.Area", rowArea)
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("LG.Site_Code", rowSite), "''", "'")
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("LG.DivCode", rowDivision), "''", "'")

            mLeavergeDays = Val(ReportFrm.FGetText(rowGraceDays))



            If ReportFrm.FGetText(rowCalculation) = "FIFO" Then

                Try
                    mQry = "Drop Table #TempRecord"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
                Catch ex As Exception
                End Try

                mQry = " CREATE Temporary TABLE #TempRecord (DocId  nvarchar(21),RecId  nvarchar(50),V_Date  DateTime,subcode nvarchar(30),"
                mQry += " PartyName nvarchar(500),BillAmt FLOAT,PendingAmt FLOAT, cummAmt Float,Status  nvarchar(20), Site_Code  nvarchar(2), Div_Code nVarchar(1),
                          PartyCity  nvarchar(200),Narration  varchar(2000),V_type  nvarchar(20) );	"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

                Dim SubCode As String = "", Party As String = "", PCity As String = "", SiteCode As String = "", DivCode As String = ""
                Dim Cr As Double = 0, Adv As Double = 0
                Dim runningDr As Double = 0

                Dim CurrTempPayment As DataTable = Nothing

                mQry = " SELECT LG.SubCode,max(Sg.name) as PartyName,max(CT.CityName) as PCity,IfNull(sum(AmtCr),0) AS AmtCr,
                    Case When IfNull(sum(AmtCr),0)> IfNull(sum(AmtDr),0) Then (IfNull(sum(AmtCr),0) - IfNull(sum(AmtDr),0)) Else  0   End As Advance ,
                    Max(LG.Site_Code) As SiteCode, LG.DivCode  
                    FROM Ledger LG 
                    LEFT JOIN viewHelpSubGroup SG On SG.Code =LG.SubCode  
                    Left Join (Select SILTV.Subcode, Max(SILTV.Agent) as Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode) as LTV On Sg.Code = LTV.Subcode
                    LEFT JOIN City CT On SG.CityCode  =CT.CityCode Where 1=1 " + mCondStr + " And SG.Nature ='Customer'
                    GROUP BY LG.SubCode, LG.DivCode "
                CurrTempPayment = AgL.FillData(mQry, AgL.GCn).Tables(0)

                For I As Integer = 0 To CurrTempPayment.Rows.Count - 1
                    SubCode = AgL.XNull(CurrTempPayment.Rows(I)("SubCode"))
                    Party = AgL.XNull(CurrTempPayment.Rows(I)("PartyName"))
                    PCity = AgL.XNull(CurrTempPayment.Rows(I)("PCity"))
                    Cr = AgL.XNull(CurrTempPayment.Rows(I)("AmtCr"))
                    Adv = AgL.XNull(CurrTempPayment.Rows(I)("Advance"))
                    SiteCode = AgL.XNull(CurrTempPayment.Rows(I)("SiteCode"))
                    DivCode = AgL.XNull(CurrTempPayment.Rows(I)("DivCode"))

                    Dim CrAmt As Double = 0, tempval As Double = 0, DrAmt As Double = 0
                    Dim DocId As String = "", RecId As String = "", Supplier As String = "", PartyName As String = "", Site As String = "", Division As String = "", City As String = "", Narr As String = "", VType As String = ""
                    Dim V_Date As String = ""

                    tempval = 0

                    Dim curr_TempAdjust As DataTable = Nothing

                    mQry = " SELECT  IfNull(LG.DocId,'') AS DocId, LG.V_Type,'" & IIf(AgL.PubPrintDivisionShortNameOnDocumentsYn, AgL.PubDivShortName, "") & IIf(AgL.PubPrintSiteShortNameOnDocumentsYn, AgL.PubSiteShortName, "") & "' || (Case When VT.Short_Name Is Not Null Then VT.Short_Name Else '' End) ||  LG.RecId As RecId,LG.V_date AS V_date,IfNull(LG.SubCode,'') AS Subcode,
                IfNull(SG.Name,'') AS PartyName, IfNull(Lg.AmtDr,0) AS AmtDr,IfNull(Lg.Site_Code,0) AS Site_Code, LG.DivCode ,IfNull(Ct.CityName,'') as City,IfNull(Lg.Narration,'') as Narr,IfNull(Lg.V_type,'') as V_type  
                FROM Ledger LG LEFT JOIN viewHelpSubGroup SG On  SG.Code=LG.SubCode 
                Left Join (Select SILTV.Subcode, Max(SILTV.Agent) as Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode) as LTV On Sg.Code = LTV.Subcode
                LEFT JOIN City CT On Ct.CityCode =Sg.CityCode  
                Left Join Voucher_Type Vt On Lg.V_Type = Vt.V_Type
                Where Date(LG.V_Date) <= " & AgL.Chk_Date(CDate(ReportFrm.FGetText(2)).ToString("s")) & " 
                And IfNull(Lg.AmtDr, 0) <> 0 And LG.SubCode = '" & SubCode & "' And LG.DivCode='" & DivCode & "'  "
                    If AgL.PubServerName = "" Then
                        mQry = mQry & " Order By Lg.V_Date, Try_Parse(Replace(LG.RecId,'-','') as Integer) "
                    Else
                        mQry = mQry & " Order By Lg.V_Date, Cast((Case When IsNumeric(Replace(LG.RecId,'-',''))=1 Then Replace(LG.RecId,'-','') Else Null End) as BigInt) "
                    End If


                    curr_TempAdjust = AgL.FillData(mQry, AgL.GCn).Tables(0)

                    CrAmt = Cr

                    For J As Integer = 0 To curr_TempAdjust.Rows.Count - 1
                        DocId = AgL.XNull(curr_TempAdjust.Rows(J)("DocId"))
                        RecId = AgL.XNull(curr_TempAdjust.Rows(J)("RecId"))
                        V_Date = curr_TempAdjust.Rows(J)("V_Date")
                        Supplier = AgL.XNull(curr_TempAdjust.Rows(J)("Subcode"))
                        PartyName = AgL.XNull(curr_TempAdjust.Rows(J)("PartyName"))
                        DrAmt = AgL.XNull(curr_TempAdjust.Rows(J)("AmtDr"))
                        Site = AgL.XNull(curr_TempAdjust.Rows(J)("Site_Code"))
                        Division = AgL.XNull(curr_TempAdjust.Rows(J)("DivCode"))
                        City = AgL.XNull(curr_TempAdjust.Rows(J)("City"))
                        Narr = AgL.XNull(curr_TempAdjust.Rows(J)("Narr"))
                        VType = AgL.XNull(curr_TempAdjust.Rows(J)("V_type"))

                        If Math.Round(DrAmt, 2) < Math.Round(CrAmt, 2) Then
                            CrAmt = Math.Round(CrAmt, 2) - Math.Round(DrAmt, 2)
                        Else
                            Dim Status As String = ""
                            If Math.Round(DrAmt, 2) <> Math.Round(DrAmt, 2) - Math.Round(CrAmt, 2) Then Status = "A"
                            runningDr = runningDr + Math.Round(DrAmt, 2) - Math.Round(CrAmt, 2)
                            mQry = " INSERT INTO  #TempRecord 
                                VALUES ('" & DocId & "','" & RecId & "'," & AgL.Chk_Date(V_Date) & ",'" & Supplier & "','" & Replace(PartyName, "'", "`") & "',
                                " & Math.Round(DrAmt, 2) & ", " & Math.Round(DrAmt, 2) - Math.Round(CrAmt, 2) & ", " & runningDr & ", '" & Status & "', '" & Site & "', '" & Division & "' , '" & City & "', 
                                '" & Narr & "', '" & VType & "')  "
                            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

                            CrAmt = 0
                            Status = ""
                        End If
                    Next

                    'If Adv <> 0 Then
                    '    mQry = " INSERT INTO  #TempRecord 
                    '        VALUES ('','','01/feb/1980', '" & SubCode & "', '" & Replace(Party, "'", "`") & "', 0, " & -Adv & ",'Adv',
                    '        '" & SiteCode & "', '" & DivCode & "', '" & PCity & "','Advance Payment ','') "
                    '    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
                    'End If
                Next



                strSql = " SELECT *, "
                strSql += " (CASE WHEN DaysDiff>= 0 AND  DaysDiff<=" & mLeavergeDays & " THEN  PendingAmt Else 0 end ) AS AmtDay1, "
                strSql += " (CASE WHEN DaysDiff>" & mLeavergeDays & " THEN  PendingAmt ELSE 0 end) AS AmtDay2 "
                strSql += " FROM ( "
                strSql += " SELECT DocId, RecId, V_Date As V_Date,subcode, PartyName,BillAmt,PendingAmt,Status,Site_Code, Div_Code,PartyCity,Narration,V_type,"
                If AgL.PubServerName = "" Then
                    strSql += "  julianday(" & strDate & ")  - julianday(V_Date)  As DaysDiff, "
                Else
                    strSql += " DateDiff(Day,V_Date, " & strDate & ") As DaysDiff, "
                End If

                strSql += " " & mLeavergeDays & " As Days "
                strSql += " FROM #TempRecord where IfNull(Round(PendingAmt,2),0)<>0  "
                strSql += " ) As VMain "




                mQry = strSql

                Dim dtTemp As DataTable
                dtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)


                If ReportFrm.FGetText(0) = "Invoice Wise Detail" Then
                    mQry = " Select VMain.DocId As SearchCode, VMain.Subcode as Subcode, strftime('%d/%m/%Y',VMain.V_Date) As VoucherDate, VMain.RecID as VoucherNo,
                        VMain.PartyName As Party, VMain.PartyCity as City, Cast(VMain.DaysDiff as Int) as [Age], VMain.BillAmt, VMain.AmtDay2 as  [DueAmount], 1 as Balance, '.' as DrCr
                        From (" & mQry & ") As VMain                                            
                        Where VMain.AmtDay2<>0
                        Order By VMain.PartyName, VMain.V_Date, VMain.RecID  "

                    RepName = "DebtorOutstandingReport_BillDetail" : RepTitle = "Debtor Outstanding Report"
                ElseIf ReportFrm.FGetText(0) = "Party Wise Summary" Then
                    mQry = " Select VMain.Subcode || '^' || VMain.Div_Code  As SearchCode, Max(VMain.PartyName) As [Party], Max(VMain.PartyCity) as City, 
                        IfNull(Max(Party.Mobile),'') || (Case  When IfNull(Max(Party.Phone),'')='' Then '' Else ', ' || IfNull(Max(Party.Phone),'')  End)  as ContactNo, 
                        Max(Division.ManualCode) as Division, Max(Agent.Name) as AgentName,
                        sum(VMain.PendingAmt) as [PendingAmount], Sum(VMain.AmtDay2) As DueAmount
                        From (" & mQry & ") As VMain
                        Left Join Subgroup Division On VMain.Div_Code  COLLATE DATABASE_DEFAULT = Division.Subcode  COLLATE DATABASE_DEFAULT
                        Left Join Subgroup Party On VMain.Subcode  COLLATE DATABASE_DEFAULT = Party.SubCode  COLLATE DATABASE_DEFAULT
                        Left Join (Select SILTV.Subcode, SILTV.Div_Code, Max(SILTV.Agent) as Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode, SILTV.Div_Code) as LTV On Party.Subcode  COLLATE DATABASE_DEFAULT = LTV.Subcode  COLLATE DATABASE_DEFAULT And VMain.Div_Code COLLATE DATABASE_DEFAULT = LTV.Div_Code  COLLATE DATABASE_DEFAULT                    
                        Left Join viewHelpSubgroup Agent On LTV.Agent  COLLATE DATABASE_DEFAULT = Agent.Code  COLLATE DATABASE_DEFAULT
                        GROUP By VMain.Subcode, VMain.Div_Code
                        Having Sum(VMain.AmtDay2)<>0
                        Order By [Party]"
                    RepName = "DebtorOutstandingReport_PartySummary" : RepTitle = "Debtor Outstanding Report"
                End If



                DsRep = AgL.FillData(mQry, AgL.GCn)

                If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records to Print!")
                ReportFrm.PrintReport(DsRep, RepName, RepTitle)




            ElseIf ReportFrm.FGetText(1) = "Adjustment" Then
                mQry = "
                        Select LG.DocID, Lg.Site_Code, LG.DivCode as Div_Code, D.ManualCode as Division, LG.Subcode, LG.V_Date, LG.RecID, 
                        Sg.Name as PartyName, Ct.CityName, Sg.Mobile, Sg.Phone, Agent.Name as Agent, LG.AmtDr+LG.AmtCr as TransAmt, IfNull(Adj.AdjAmt,0) as AdjAmt, 
                        (Case When Lg.AmtDr > 0 Then LG.AmtDr-IfNull(Adj.AdjAmt,0) Else 0 End) AmtDr, 
                        (Case When Lg.AmtCr > 0 Then LG.AmtCr-IfNull(Adj.AdjAmt,0) Else 0 End) as AmtCr "
                If AgL.PubServerName = "" Then
                    mQry += ",  julianday(" & strDate & ")  - julianday(Lg.V_Date)  As DaysDiff "
                Else
                    mQry += ", DateDiff(Day,LG.V_Date, " & strDate & ") As DaysDiff "
                End If
                mQry = mQry + "From ledger LG 
                        Left Join (Select Adj_DocID as DocID, Adj_V_Sno as V_SNo, 
                                    abs(Sum(Amount)) as AdjAmt 
                                    From LedgerAdj LA  
                                    Left Join Ledger L1   On L1.DocId = LA.Vr_DocID And L1.V_SNo = LA.Vr_V_Sno
                                    Group By Adj_DocID, Adj_V_Sno
                                    Union All 
                                    Select Vr_DocID as DocID, Vr_V_Sno as V_SNo, 
                                    abs(Sum(Amount)) as AdjAmt 
                                    From LedgerAdj LA  
                                    Left Join Ledger L1   On L1.DocId = LA.Vr_DocID And L1.V_SNo = LA.Vr_V_Sno
                                    Group By Vr_DocID, Vr_V_Sno                    
                                    ) as Adj On LG.DocID = Adj.DocID And LG.V_Sno = Adj.V_Sno                
                        LEFT JOIN SubGroup SG On SG.SubCode =LG.SubCode  
                        Left Join (Select SILTV.Subcode, Max(SILTV.Agent) as Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode) as LTV On Sg.SubCode = LTV.Subcode
                        Left Join viewHelpSubgroup Agent On LTV.Agent = Agent.Code
                        LEFT JOIN City CT On SG.CityCode  =CT.CityCode 
                        Left Join SiteMast Site On LG.Site_Code = Site.Code
                        Left Join Subgroup D On LG.DivCode = D.SubCode
                        Where (LG.AmtDr+LG.AmtCr)  - IfNull(Adj.AdjAmt,0) >0                         
                        And SG.Nature ='Customer'                            
                    " & mCondStr



                If ReportFrm.FGetText(0) = "Invoice Wise Detail" Then


                    mQry = " Select VMain.DocId As SearchCode, VMain.Subcode as Subcode, Vmain.Division, strftime('%d/%m/%Y',VMain.V_Date) As VoucherDate, VMain.RecID as VoucherNo,
                        VMain.PartyName As Party, VMain.PartyCity as City, Cast(VMain.DaysDiff as Int) as [Age], VMain.BillAmt, VMain.AmtDay2 as  [Amount], 1 as Balance, '.' as DrCr"

                    mQry = " Select VMain.DocId As SearchCode, Vmain.Subcode, Vmain.Division, strftime('%d/%m/%Y',VMain.V_Date) As VoucherDate, VMain.RecID as VoucherNo,
                        VMain.PartyName As Party, VMain.CityName as City, VMain.DaysDiff as [Age], Vmain.TransAmt as BillAmt, Vmain.AdjAmt, Vmain.AmtDr, Vmain.AmtCr, 1 as Balance, '.' as DrCr
                        From (" & mQry & ") As VMain       
                        Where (Vmain.DaysDiff > " & mLeavergeDays & " Or Vmain.AmtCr>0)            
                        Order By VMain.PartyName, VMain.V_Date, VMain.RecID "


                    DsRep = AgL.FillData(mQry, AgL.GCn)


                ElseIf ReportFrm.FGetText(0) = "Party Wise Summary" Then
                    mQry = " Select VMain.Subcode || '^' || VMain.Div_Code  As SearchCode, Max(VMain.PartyName) As [Party], Max(VMain.CityName) as City, 
                        IfNull(Max(VMain.Mobile),'') || (Case  When IfNull(Max(VMain.Phone),'')='' Then '' Else ', ' || IfNull(Max(VMain.Phone),'')  End)  as ContactNo, 
                        Max(VMain.Division) as Division, Max(Vmain.Agent) as AgentName,
                        sum(VMain.AmtDr - VMain.AmtCr) as [Amount], Sum(Case When VMain.DaysDiff > " & mLeavergeDays & " Or VMain.AmtCr > 0 Then VMain.AmtDr - VMain.AmtCr  Else 0 End) As AmtDay2
                        From (" & mQry & ") As VMain
                        GROUP By VMain.Subcode, VMain.Div_Code
                        Having Sum(Case When VMain.DaysDiff > " & mLeavergeDays & " Or VMain.AmtCr > 0 Then VMain.AmtDr - VMain.AmtCr Else 0 End) > 0
                        Order By [Party]"

                    DsRep = AgL.FillData(mQry, AgL.GCn)
                End If


                If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records to Print!")
                ReportFrm.PrintReport(DsRep, RepName, RepTitle)

            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


End Class
