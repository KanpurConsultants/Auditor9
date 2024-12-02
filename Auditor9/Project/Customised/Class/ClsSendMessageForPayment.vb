Imports System.IO
Imports AgLibrary
Imports AgLibrary.ClsMain
Imports AgLibrary.ClsMain.agConstants
Imports Microsoft.Reporting.WinForms
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports System.Net
Imports System.Windows.Forms
Imports Customised.ClsMain
Public Class ClsSendMessageForPayment

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

    Const mCalculation_FIFO As String = "FIFO"
    Const mCalculation_Adjustment As String = "Adjustment"



    Dim mShowReportType As String = ""
    Dim mReportDefaultText$ = ""

    Dim DsHeader As DataSet = Nothing

    Dim rowCalculation As Integer = 0
    Dim rowAsOnDate As Integer = 1
    Dim rowLeaverageDays As Integer = 2


    Dim mDocId As String = ""

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
    Dim mHelpAcGroupCustomerQry$ = "Select 'o' As Tick, GroupCode, GroupName From AcGroup Where Nature='Customer' "
    Public Shared mHelpAreaQry$ = "Select 'o' As Tick, Code, Description From Area "
    Dim mHelpCityQry$ = "Select 'o' As Tick, CityCode, CityName From City "
    Dim mHelpStateQry$ = "Select 'o' As Tick, Code, Description From State "
    Dim mHelpItemQry$ = "Select 'o' As Tick, Code, Description As [Item] From Item Where V_Type = '" & ItemV_Type.Item & "'"
    Dim mHelpPurchaseAgentQry$ = " Select 'o' As Tick, Sg.Code, Sg.Name AS Agent FROM viewHelpSubgroup Sg Where Sg.SubgroupType = '" & SubgroupType.PurchaseAgent & "' "
    Dim mHelpItemGroupQry$ = "Select 'o' As Tick, Code, Description As [Item Group] From ItemGroup "
    Dim mHelpItemCategoryQry$ = "Select 'o' As Tick, Code, Description As [Item Category] From ItemCategory "
    Dim mHelpItemTypeQry$ = "Select 'o' As Tick, Code, Name FROM ItemType "
    Dim mHelpLocationQry$ = " Select 'o' As Tick,  Sg.Code, Sg.Name AS Party FROM viewHelpSubGroup Sg Where Sg.Nature In ('Supplier','Stock') "
    Dim mHelpTagQry$ = "Select Distinct 'o' As Tick, H.Tags as Code, H.Tags as Description  FROM PurchInvoiceDetail H "
    Private Function FGetSettings(FieldName As String, SettingType As String) As String
        Dim mValue As String
        mValue = ClsMain.FGetSettings(FieldName, SettingType, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
        FGetSettings = mValue
    End Function
    Public Sub Ini_Grid()
        Try
            mQry = "Select '" & mCalculation_FIFO & "' as Code, '" & mCalculation_FIFO & "' as Name 
                    Union All 
                    Select '" & mCalculation_Adjustment & "' as Code, '" & mCalculation_Adjustment & "' as Name"
            ReportFrm.CreateHelpGrid("Calculation", "Calculation", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mQry, mCalculation_FIFO,,, 300)

            ReportFrm.CreateHelpGrid("AsOnDate", "As On Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.PubLoginDate)
            ReportFrm.CreateHelpGrid("LeaverageDays", "Leaverage Days", FrmRepDisplay.FieldFilterDataType.NumericType, FrmRepDisplay.FieldDataType.FloatType, "", "90")
            ReportFrm.CreateHelpGrid("Party", "Party", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpPartyQry)
            ReportFrm.CreateHelpGrid("AccountGroup", "Account Group", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpAcGroupCustomerQry)
            ReportFrm.CreateHelpGrid("City", "City", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpCityQry)
            ReportFrm.CreateHelpGrid("State", "State", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpStateQry)
            ReportFrm.CreateHelpGrid("Area", "Area", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpAreaQry)
            ReportFrm.CreateHelpGrid("Site", "Site", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSiteQry, "[SITECODE]")
            ReportFrm.CreateHelpGrid("Division", "Division", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpDivisionQry, "[DIVISIONCODE]")
            'ReportFrm.CreateHelpGrid("Site", "Site", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSiteQry, "[SITECODE]")
            'ReportFrm.CreateHelpGrid("Division", "Division", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpDivisionQry, "[DIVISIONCODE]")
            'ReportFrm.CreateHelpGrid("Mobile", "Mobile", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.StringType, "", "")
            'If mDocId <> "" Then ReportFrm.FilterGrid.Rows(rowReportType).Visible = False
            'ReportFrm.FilterGrid.Rows(rowMobile).Visible = False
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub
    Private Sub ObjRepFormGlobal_ProcessReport() Handles ReportFrm.ProcessReport
        ProcFillOutstaning()
    End Sub
    Public Sub New(ByVal mReportFrm As FrmRepDisplay, Optional bDocId As String = "")
        mDocId = bDocId
        ReportFrm = mReportFrm
    End Sub
    Public Sub ProcFillOutstaning(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try
            Dim mCondStr$ = ""
            Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"
            Dim mLeavergeDays As Double
            Dim strSql As String
            Dim strDate As String

            Dim mPendingBillCount As Integer

            RepTitle = "Outstanding Report"

            If mFilterGrid IsNot Nothing And mGridRow IsNot Nothing Then
                If mGridRow.DataGridView.Columns.Contains("Search Code") = True Then
                    If mFilterGrid.Item(GFilter, 0).Value = "Party Wise Summary" Then
                        Dim mSearchCodes As String()
                        mSearchCodes = mGridRow.Cells("Search Code").Value.ToString.Split("^")

                        mFilterGrid.Item(GFilter, 4).Value = mGridRow.Cells("Party").Value
                        mFilterGrid.Item(GFilterCode, 4).Value = "'" + mSearchCodes(0) + "'" '"'" + mGridRow.Cells("Search Code").Value + "'"

                        mFilterGrid.Item(GFilter, 11).Value = mGridRow.Cells("Division").Value
                        mFilterGrid.Item(GFilterCode, 11).Value = "'" + mSearchCodes(1) + "'" '"'" + mGridRow.Cells("Search Code").Value + "'"

                        mFilterGrid.Item(GFilter, 0).Value = "Invoice Wise Detail"
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = "Invoice Wise Detail" Then
                        ClsMain.FOpenForm(mGridRow.Cells("Search Code").Value, ReportFrm)
                        ReportFrm.FiterGridCopy_Arr.RemoveAt(ReportFrm.FiterGridCopy_Arr.Count - 1)
                        Exit Sub
                    End If
                Else
                    Exit Sub
                End If
            End If


            strDate = AgL.Chk_Text(CDate(ReportFrm.FGetText(1)).ToString("s"))

            mCondStr = "  "
            mCondStr = mCondStr & " AND Date(LG.V_Date) <= " & AgL.Chk_Date(CDate(ReportFrm.FGetText(1)).ToString("s")) & " "
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("LG.Subcode", 3)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("SG.GroupCode", 4)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("CT.CityCode", 5)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("Ct.State", 6)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("SG.Area", 7)
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("LG.Site_Code", 8), "''", "'")
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("LG.DivCode", 9), "''", "'")

            'mCondStr = mCondStr & " AND SG.Name LIKE 'a%' "

            mLeavergeDays = Val(ReportFrm.FGetText(2))



            If ReportFrm.FGetText(0) = "FIFO" Then
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
                    LEFT JOIN SubGroup SG On SG.SubCode =LG.SubCode  
                    Left Join (Select SILTV.Subcode, Max(SILTV.Agent) as Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode) as LTV On Sg.SubCode = LTV.Subcode
                    LEFT JOIN City CT On SG.CityCode  =CT.CityCode Where 1=1 " + mCondStr + " And SG.Nature ='Customer'
                    GROUP BY LG.SubCode, LG.DivCode 
                    Having IfNull(sum(AmtCr),0) - IfNull(sum(AmtDr),0) < 0 "
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
                            FROM Ledger LG LEFT JOIN SubGroup SG On  SG.SubCode=LG.SubCode 
                            Left Join (Select SILTV.Subcode, Max(SILTV.Agent) as Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode) as LTV On Sg.SubCode = LTV.Subcode
                            LEFT JOIN City CT On Ct.CityCode =Sg.CityCode  
                            Left Join Voucher_Type Vt On Lg.V_Type = Vt.V_Type
                            Where Date(LG.V_Date) <= " & AgL.Chk_Date(CDate(ReportFrm.FGetText(1)).ToString("s")) & " 
                            And IfNull(Lg.AmtDr, 0) <> 0 And LG.SubCode = '" & SubCode & "' And LG.DivCode='" & DivCode & "'  "
                    If AgL.PubServerName = "" Then
                        mQry = mQry & " Order By Lg.V_Date, Try_Parse(Replace(LG.RecId,'-','') as Integer) "
                    Else
                        mQry = mQry & " Order By Lg.V_Date, Cast((Case When IsNumeric(Replace(LG.RecId,'-',''))=1 Then Replace(LG.RecId,'-','') Else Null End) as BigInt) "
                    End If


                    curr_TempAdjust = AgL.FillData(mQry, AgL.GCn).Tables(0)

                    CrAmt = Cr
                    mPendingBillCount = 0
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
                            mQry = " INSERT INTO  #TempRecord (DocId, RecId, V_Date, subcode,
                                     PartyName,BillAmt,PendingAmt, cummAmt, 
                                     Status, Site_Code, Div_Code, PartyCity,
                                     Narration ,V_type)
                                    VALUES ('" & DocId & "','" & RecId & "'," & AgL.Chk_Date(V_Date) & ",'" & Supplier & "',
                                    '" & Replace(PartyName, "'", "`") & "', " & Math.Round(DrAmt, 2) & ", " & Math.Round(DrAmt, 2) - Math.Round(CrAmt, 2) & ", " & runningDr & ", 
                                    '" & Status & "', '" & Site & "', '" & Division & "' , '" & City & "', 
                                    '" & Narr & "', '" & VType & "')  "
                            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
                            mPendingBillCount += 1

                            CrAmt = 0
                            Status = ""
                        End If
                    Next

                    Dim NextYearDate As String
                    NextYearDate = DateAdd(DateInterval.Day, 1, CDate(AgL.PubLoginDate))

                    If mPendingBillCount > 0 Then
                        mQry = " INSERT INTO  #TempRecord (DocId, RecId, V_Date, subcode,
                                     PartyName,BillAmt,PendingAmt, cummAmt, 
                                     Status, Site_Code, Div_Code, PartyCity,
                                     Narration ,V_type)
                            VALUES ('','Total'," & AgL.Chk_Date(NextYearDate) & ", '" & SubCode & "', 
                            '" & Replace(PartyName, "'", "`") & "', 0, 0, 0,
                            '', '" & SiteCode & "', '" & DivCode & "', '" & PCity & "',
                            '','') "
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
                    End If
                Next



                Dim mDays1 As Double
                Dim mDays2 As Double
                Dim mDays3 As Double
                Dim mDays4 As Double
                Dim mDays5 As Double
                Dim mDays6 As Double

                mDays1 = mLeavergeDays
                mDays2 = mDays1 + mLeavergeDays
                mDays3 = mDays2 + mLeavergeDays
                mDays4 = mDays3 + mLeavergeDays
                mDays5 = mDays4 + mLeavergeDays
                mDays6 = mDays5 + mLeavergeDays

                strSql = " SELECT *, "
                strSql += " (CASE WHEN DaysDiff>= 0 AND  DaysDiff<=" & mLeavergeDays & " THEN  PendingAmt Else 0 end ) AS AmtDay1, "
                strSql += " (CASE WHEN DaysDiff>" & mLeavergeDays & " THEN  PendingAmt ELSE 0 end) AS AmtDay2, "
                strSql += " (CASE WHEN DaysDiff<=" & mDays1 & " THEN  PendingAmt ELSE 0 end) AS AmtDay0, "
                strSql += " (CASE WHEN DaysDiff>" & mDays1 & " And DaysDiff<=" & mDays2 & " THEN  PendingAmt ELSE 0 end) AS AmtDay30, "
                strSql += " (CASE WHEN DaysDiff>" & mDays2 & " And DaysDiff<=" & mDays3 & " THEN  PendingAmt ELSE 0 end) AS AmtDay60, "
                strSql += " (CASE WHEN DaysDiff>" & mDays3 & " And DaysDiff<=" & mDays4 & " THEN  PendingAmt ELSE 0 end) AS AmtDay90, "
                strSql += " (CASE WHEN DaysDiff>" & mDays4 & " And DaysDiff<=" & mDays5 & " THEN  PendingAmt ELSE 0 end) AS AmtDay120, "
                strSql += " (CASE WHEN DaysDiff>" & mDays5 & " And DaysDiff<=" & mDays6 & " THEN  PendingAmt ELSE 0 end) AS AmtDay150, "
                strSql += " (CASE WHEN DaysDiff>" & mDays6 & " THEN  PendingAmt ELSE 0 end) AS AmtDay180 "
                strSql += " FROM ( "
                strSql += " SELECT DocId, RecId, V_Date As V_Date,subcode, PartyName,BillAmt,PendingAmt,Status,Site_Code, Div_Code,PartyCity,Narration,V_type,"
                If AgL.PubServerName = "" Then
                    strSql += "  julianday(" & strDate & ")  - julianday(V_Date)  As DaysDiff, "
                Else
                    strSql += " DateDiff(Day,V_Date, " & strDate & ") As DaysDiff, "
                End If

                strSql += " " & mLeavergeDays & " As Days "
                strSql += " FROM #TempRecord where (IfNull(Round(PendingAmt,2),0)<>0  Or RecId='Total')"
                strSql += " ) As VMain "




                mQry = strSql

                Dim dtTemp As DataTable
                dtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)



                mQry = " Select " & IIf(mDocId <> "", "'þ'", "'o'") & " As Tick, '' As Exception, VMain.Subcode || '^' || VMain.Div_Code  As SearchCode, Max(VMain.PartyName) As [Party], Max(VMain.PartyCity) as City, 
                        IfNull(Max(Party.Mobile),'') || (Case  When IfNull(Max(Party.Phone),'')='' Then '' Else ', ' || IfNull(Max(Party.Phone),'')  End)  as ContactNo, 
                        Max(VPartyGST.SalesTaxNo) as GstNo, Max(Division.ManualCode) as Division, Max(Agent.Name) as AgentName,
                        sum(VMain.PendingAmt) as [Amount], Sum(VMain.AmtDay2) As [Amount GE " & mLeavergeDays.ToString & " Days],
                        Max(Cast(VMain.DaysDiff as Int)) As FirstBillAge 
                        From (" & mQry & ") As VMain
                        Left Join Subgroup Division On VMain.Div_Code  COLLATE DATABASE_DEFAULT = Division.Subcode  COLLATE DATABASE_DEFAULT
                        Left Join Subgroup Party On VMain.Subcode  COLLATE DATABASE_DEFAULT = Party.SubCode  COLLATE DATABASE_DEFAULT
                        Left Join (Select SILTV.Subcode, SILTV.Div_Code, Max(SILTV.Agent) as Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode, SILTV.Div_Code) as LTV On Party.Subcode  COLLATE DATABASE_DEFAULT = LTV.Subcode  COLLATE DATABASE_DEFAULT And VMain.Div_Code COLLATE DATABASE_DEFAULT = LTV.Div_Code  COLLATE DATABASE_DEFAULT                    
                        Left Join viewHelpSubgroup Agent On LTV.Agent  COLLATE DATABASE_DEFAULT = Agent.Code  COLLATE DATABASE_DEFAULT
                        LEFT JOIN (Select Subcode, RegistrationNo As SalesTaxNo
                            From SubgroupRegistration 
                            Where RegistrationType = 'Sales Tax No') As VPartyGST On VMain.Subcode COLLATE DATABASE_DEFAULT = VPartyGST.SubCode COLLATE DATABASE_DEFAULT
                        GROUP By VMain.Subcode, VMain.Div_Code
                        Having Sum(VMain.AmtDay2)<>0
                        Order By Max(VMain.PartyName) "

                DsHeader = AgL.FillData(mQry, AgL.GCn)

                If DsHeader.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

                mQry = "Select 'Send Whatsapp Message' As MenuText, 'SendWhatsappMessage' As FunctionName "
                mQry += "UNION ALL "
                mQry += "Select 'Send Whatsapp PDF' As MenuText, 'SendWhatsappPDF' As FunctionName "
                Dim DtMenuList As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

                ReportFrm.Text = "Send Message For Payment"
                ReportFrm.ClsRep = Me
                ReportFrm.ReportProcName = "ProcDebtorsOutstaningReport"
                ReportFrm.DTCustomMenus = DtMenuList
                ReportFrm.ProcFillGrid(DsHeader)

            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            DsHeader = Nothing
        End Try
    End Sub


    Public Sub SendWhatsappMessage(DGL As AgControls.AgDataGrid)
        Dim mSearchCode As String = ""
        'Dim strIrn As String, strAckNo As String, strAckDate As String, strQrCodeImage As String
        'Dim Result As String, url As String, strdata As String
        Dim IsSuccess As Boolean
        Dim I As Integer = 0
        Dim mMobileNo As String = ""
        Dim mMessage As String = ""

        Try
            'For I = 0 To DGL.Rows.Count - 1
            '    If DGL.Item("Search Code", I).Value IsNot Nothing And DGL.Item("Search Code", I).Value <> "" Then
            '        If AgL.XNull(DGL.Item("Irn", I).Value) IsNot Nothing And AgL.XNull(DGL.Item("Irn", I).Value) <> "" Then
            '            MsgBox("IRN Generated Already For Invoice No." & DGL.Item("Invoice No", I).Value & ". Can't Generate Again.", MsgBoxStyle.Information)
            '            Exit Sub
            '        End If
            '    End If
            'Next

            For I = 0 To DGL.Rows.Count - 1
                If DGL.Item("Search Code", I).Value IsNot Nothing And DGL.Item("Search Code", I).Value <> "" And DGL.Item("Tick", I).Value = "þ" Then
                    mSearchCode = DGL.Item("Search Code", I).Value
                    mMobileNo = DGL.Item("Contact No", I).Value
                    mMessage = "Dear " + DGL.Item("Party", I).Value + ", Your Rs. " + DGL.Item("Amount", I).Value.ToString() + " Due. Please Do Payment"

                    IsSuccess = FSendWhatsappMessage(mMobileNo, mMessage, "Message", "")

                End If
            Next
            mMessage = "Message Send Successfully."
            MsgBox(mMessage, MsgBoxStyle.Information)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Public Sub SendWhatsappPDF(DGL As AgControls.AgDataGrid)
        Dim mSearchCode As String = ""
        'Dim strIrn As String, strAckNo As String, strAckDate As String, strQrCodeImage As String
        'Dim Result As String, url As String, strdata As String
        Dim IsSuccess As Boolean
        Dim I As Integer = 0
        Dim mMobileNo As String = ""
        Dim mMessage As String = ""

        Try
            'For I = 0 To DGL.Rows.Count - 1
            '    If DGL.Item("Search Code", I).Value IsNot Nothing And DGL.Item("Search Code", I).Value <> "" Then
            '        If AgL.XNull(DGL.Item("Irn", I).Value) IsNot Nothing And AgL.XNull(DGL.Item("Irn", I).Value) <> "" Then
            '            MsgBox("IRN Generated Already For Invoice No." & DGL.Item("Invoice No", I).Value & ". Can't Generate Again.", MsgBoxStyle.Information)
            '            Exit Sub
            '        End If
            '    End If
            'Next

            For I = 0 To DGL.Rows.Count - 1
                If DGL.Item("Search Code", I).Value IsNot Nothing And DGL.Item("Search Code", I).Value <> "" And DGL.Item("Tick", I).Value = "þ" Then
                    mSearchCode = DGL.Item("Search Code", I).Value
                    mMobileNo = DGL.Item("Contact No", I).Value
                    mMessage = "Dear " + DGL.Item("Party", I).Value + ", Your Rs. " + DGL.Item("Amount", I).Value.ToString() + " Due. Please Do Payment"

                    IsSuccess = FSendWhatsappMessage(mMobileNo, mMessage, "PDF", "")

                    'strdata = FGetJsonForIrn(mSearchCode)

                    'url = "http://testapi.taxprogsp.co.in/eicore/dec/v1.03/Invoice?&aspid=" & mAspUserId & "&password=" & mAspPassword & "&Gstin=" & mGstin & "&user_name=" & mUserName & "&&AuthToken=" & AuthToken() & "&QrCodeSize=250"

                    'mAuthToken = AuthToken()
                    'url = mWhatsappMessageURL.Replace("<AspUserId>", mAspUserId).
                    '            Replace("<AspPassword>", mAspPassword).
                    '            Replace("<Gstin>", mGstin).
                    '            Replace("<EInvioceUserName>", mUserName).
                    '            Replace("<EInviocePassword>", mPassword).
                    '            Replace("<AuthToken>", mAuthToken)

                    'Result = WebRequestbody(url, strdata)

                    'Dim p As Object = JSON.parse(Result)


                    'If p.Item("Status") = "0" Then
                    'If p.Item("ErrorDetails")(1).Item("ErrorCode") <> "0" Then
                    '    mMessage += p.Item("ErrorDetails")(1).Item("ErrorCode") & " : " & p.Item("ErrorDetails")(1).Item("ErrorMessage") & ". Error In Invoice No." & mInvoiceNo & vbCrLf
                    '    Continue For
                    'End If
                    'End If

                    'Dim sOutputJson As Object = p.Item("Data")
                    'p = JSON.parse(sOutputJson)

                    'strIrn = p.Item("Irn")
                    'strAckNo = p.Item("AckNo")
                    'strAckDate = p.Item("AckDt")
                    'strQrCodeImage = p.Item("QrCodeImage")

                    'Dim DestinationPath As String = PubAttachmentPath + mSearchCode + "\"
                    'If Not Directory.Exists(DestinationPath) Then
                    '    Directory.CreateDirectory(DestinationPath)
                    'End If

                    'Dim mByte() As Byte = Convert.FromBase64String(strQrCodeImage)
                    'System.IO.File.WriteAllBytes(DestinationPath + "EInvoiceQRCode.png", mByte)

                    'If strIrn = "" Then
                    '    mMessage = " IRN not generated for Invoice No." & mInvoiceNo & vbCrLf
                    '    Exit Sub
                    'Else
                    '    mQry = " UPDATE SaleInvoice Set EInvoiceIRN = " & AgL.Chk_Text(strIrn) & ",
                    '        EInvoiceACKNo = " & AgL.Chk_Text(strAckNo) & ",
                    '        EInvoiceACKDate = " & AgL.Chk_Date(strAckDate) & "
                    '        Where DocId = '" & mSearchCode & "'"
                    '    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                    '    mQry = " UPDATE LedgerHead Set EInvoiceIRN = " & AgL.Chk_Text(strIrn) & ",
                    '        EInvoiceACKNo = " & AgL.Chk_Text(strAckNo) & ",
                    '        EInvoiceACKDate = " & AgL.Chk_Date(strAckDate) & "
                    '        Where DocId = '" & mSearchCode & "'"
                    '    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                    '    mQry = "Insert Into TransactionReferences (DocID, ReferenceDocID, Type, Remark, IsEditingAllowed, IsDeletingAllowed) 
                    '        Values (" & AgL.Chk_Text(mSearchCode) & ", " & AgL.Chk_Text(mSearchCode) & ", 'E Invoice',
                    '        'E-Invoice is created. To make changes in this invoice yoou have to first cancel E-invoice.', 0, 0) "
                    '    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                    '    mMessage += " E-Invoice Generated Successfully For Invoice No." & mInvoiceNo & vbCrLf
                    '    ReportFrm.DGL1.DataSource = Nothing
                    'End If
                End If
            Next

            mMessage = " E-Invoice Generated Successfully."

            'MsgBox(mMessage, MsgBoxStyle.Information)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub



End Class
