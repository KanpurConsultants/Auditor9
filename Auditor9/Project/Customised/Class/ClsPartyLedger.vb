Imports AgLibrary.ClsMain.agConstants

Public Class ClsPartyLedger

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
    Dim mHelpPartyQry$ = " Select 'o' As Tick,  Sg.Code, Sg.Name AS Party, Sg.Address, Ag.GroupName FROM viewHelpSubGroup Sg  Left Join AcGroup Ag On Sg.groupCode= ag.GroupCode Where Sg.Nature In ('Customer','Supplier','Cash') And Sg.SubgroupType Not In ('Master Customer','Master Supplier') "

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
    Private Const rowFromDate As Integer = 1
    Private Const rowBillsUptoDate As Integer = 2
    Private Const rowPaymentsUptoDate As Integer = 3
    Private Const rowRecordsType As Integer = 4
    Private Const rowParty As Integer = 5
    Private Const rowMasterParty As Integer = 6
    Private Const rowLinkedParty As Integer = 7
    Private Const rowAgent As Integer = 8
    Private Const rowCity As Integer = 9
    Private Const rowArea As Integer = 10
    Private Const rowDivision As Integer = 11
    Private Const rowSite As Integer = 12


    Public Sub Ini_Grid()
        Try
            Dim mQry As String
            Dim I As Integer = 0

            mQry = "Select 'Format 1' as Code, 'Format 1' as Name "
            ReportFrm.CreateHelpGrid("Report Type", "Report Type", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.SingleSelection, mQry, "Format 1")
            ReportFrm.CreateHelpGrid("From Date", "From Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", "")
            ReportFrm.CreateHelpGrid("Bills Upto Date", "Bills Upto Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.PubLoginDate)
            ReportFrm.CreateHelpGrid("Payments Upto Date", "Payments Upto Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.PubLoginDate)
            mQry = "Select 'All' as Code, 'All' as Name 
                    Union All
                    Select 'After Concur' as Code, 'After Concur' as Name 
                    "
            ReportFrm.CreateHelpGrid("Records Type", "Records Type", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.SingleSelection, mQry, "All")
            ReportFrm.CreateHelpGrid("Party", "Party", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpPartyQry, , 450, 825, 300)

            mQry = " Select 'o' As Tick,  Sg.Code, Sg.Name AS Party, Sg.Address, Ag.GroupName FROM viewHelpSubGroup Sg  Left Join AcGroup Ag On Sg.groupCode= ag.GroupCode Where Sg.Nature In ('Customer', 'Supplier') And Sg.Code In (Select Distinct Parent From SubGroup) Order By Name"
            ReportFrm.CreateHelpGrid("Master Party", "Master Party", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mQry, , 450, 825, 300)
            If Not ClsMain.IsScopeOfWorkContains(IndustryType.CommonModules.MasterParty) Then ReportFrm.FGMain.Rows(rowMasterParty).Visible = False

            mQry = " Select 'o' As Tick,  Sg.Code, Sg.Name AS Party, Sg.Address, Ag.GroupName FROM viewHelpSubGroup Sg  Left Join AcGroup Ag On Sg.groupCode= ag.GroupCode Where Sg.SubgroupType In ('Master Customer','Master Supplier') Order By Name"
            ReportFrm.CreateHelpGrid("Linked Party", "Linked Party", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mQry, , 450, 825, 300)
            If Not ClsMain.IsScopeOfWorkContains(IndustryType.CommonModules.LinkedParty) Then ReportFrm.FGMain.Rows(rowLinkedParty).Visible = False

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


    Private Structure StructLedger
        Public DocID As String
        Public V_Type As String
        Public V_Date As String
        Public RecId As String
        Public Div_Code As String
        Public Site_Code As String
        Public LRNo As String
        Public Subcode As String
        Public PartyName As String
        Public PartyCity As String
        Public TaxableAmount As Double
        Public TaxAmount As Double
        Public Addition As Double
        Public BillAmount As Double
        Public GoodsReturn As Double
        Public Payment As Double
        Public Adjustment As Double
        Public Balance As Double
        Public Narration As String
        Public AmtDr As Double
        Public AmtCr As Double
    End Structure

    Structure OutstandingBill
        Public DocNo As String
        Public DocDate As Date
        Public Narration As String
        Public DocAmount As Double
        Public BalAmount As Double
        Public DrCr As String
    End Structure
    Public Sub ProcDebtorsOutstaningReport()
        Try
            Dim mCondStr$ = ""


            Dim DsRep As DataSet




            mCondStr = "  "
            mCondStr = mCondStr & " AND Date(LG.V_Date) <= (Case 
                                                            When Sg.Nature='Customer' And Lg.AmtDr>0 Then " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowBillsUptoDate)).ToString("s")) & " 
                                                            When Sg.Nature='Customer' And Lg.AmtCr>0 Then " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowPaymentsUptoDate)).ToString("s")) & " 
                                                            When Sg.Nature<>'Customer' And Lg.AmtDr>0 Then " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowPaymentsUptoDate)).ToString("s")) & " 
                                                            When Sg.Nature<>'Customer' And Lg.AmtCr>0 Then " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowBillsUptoDate)).ToString("s")) & " 
                                                            End) "
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("LG.Subcode", rowParty)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("Sg.Parent", rowMasterParty)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("LG.LinkedSubcode", rowLinkedParty)
            'mCondStr = mCondStr & ReportFrm.GetWhereCondition("Sg.GroupCode", rowa)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("LTV.Agent", rowAgent)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("Sg.CityCode", rowCity)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("Sg.Area", rowArea)
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("LG.Site_Code", rowSite), "''", "'")
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("LG.DivCode", rowDivision), "''", "'")



            If ReportFrm.FGetText(rowRecordsType) = "After Concur" Then
                mCondStr = mCondStr & " And LG.DocID || LG.V_SNo Not In (SELECT H.PurchaseInvoiceDocId || H.PurchaseInvoiceDocIdSr   FROM Cloth_SupplierSettlementInvoices H
                                                                       UNION ALL 
                                                                       SELECT H.PaymentDocId || H.PaymentDocIdSr   FROM Cloth_SupplierSettlementPayments H
                                                                       ) "
                mCondStr = mCondStr & " And LG.DocID Not In ( SELECT H.DocID FROM LedgerHead H LEFT JOIN LedgerHeadDetail L ON H.DocID = L.DocID WHERE H.V_Type In ('" & Ncat.PaymentSettlement & "','" & Ncat.ReceiptSettlement & "') ) "
            End If





            Try
                mQry = "Drop Table #TempRecord"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
            Catch ex As Exception
            End Try

            mQry = " CREATE Temporary TABLE #TempRecord 
                    (DocId  nvarchar(21), 
                    V_type  nvarchar(20),
                    RecId  nvarchar(50), 
                    V_Date  DateTime,
                    Site_Code  nvarchar(2), 
                    Div_Code nVarchar(1),                         
                    LRNo nVarchar(50),                         
                    Subcode nvarchar(10),
                    PartyName nvarchar(255),
                    PartyCity  nvarchar(255),
                    TaxableAmount FLOAT, 
                    TaxAmount FLOAT, 
                    Addition FLOAT, 
                    BillAmount Float, 
                    SaleReturn Float,
                    Adjustment Float, 
                    Payment Float,
                    Balance Float,  
                    AmtDr Float,
                    AmtCr Float,                       
                    Narration  varchar(2000)
                        ); "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)


            Try
                mQry = "Drop Table #FifoOutstanding"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
            Catch ex As Exception
            End Try


            mQry = " CREATE Temporary TABLE #FifoOutstanding 
                    (DocId  nvarchar(21), 
                    V_type  nvarchar(20),
                    RecId  nvarchar(50), 
                    V_Date  DateTime,
                    Site_Code  nvarchar(2), 
                    Div_Code nVarchar(1),                         
                    Subcode nvarchar(10),
                    BillAmount Float, 
                    BalanceAmount Float,
                    DrCr nVarchar(10),                  
                    Narration  varchar(2000)
                    ); "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)


            Dim SubCode As String = "", Party As String = "", PCity As String = "", SiteCode As String = "", DivCode As String = ""
                Dim Cr As Double = 0, Adv As Double = 0
                Dim runningDr As Double = 0

            Dim DtLedger As DataTable = Nothing



            If ReportFrm.FGetText(rowFromDate) <> "" Then


                'Dim mBalance As Double
                Dim mRemainingBalance As Double
                Dim i As Integer, j As Integer
                'Dim OutstandingBills As New List(Of OutstandingBill)
                'Dim objOutstandingBill As OutstandingBill
                Dim dtParty As DataTable
                Dim condStrParty As String = ""
                Dim DtMain As DataTable
                Dim BalAmount As Double
                Dim DrCr As String


                condStrParty = condStrParty & ReportFrm.GetWhereCondition("LG.Subcode", rowParty)
                condStrParty = condStrParty & ReportFrm.GetWhereCondition("Sg.Parent", rowMasterParty)
                'condStrParty = condStrParty & ReportFrm.GetWhereCondition("Sg.GroupCode", 5)
                condStrParty = condStrParty & ReportFrm.GetWhereCondition("LTV.Agent", rowAgent)
                condStrParty = condStrParty & ReportFrm.GetWhereCondition("Sg.CityCode", rowCity)
                condStrParty = condStrParty & ReportFrm.GetWhereCondition("Sg.Area", rowArea)

                mQry = "Select Lg.Subcode, Max(Sg.Nature) as Nature, Sum(Lg.AmtDr)-Sum(Lg.AmtCr) as Balance
                        From Ledger Lg
                        Left Join Subgroup Sg On Lg.Subcode = Sg.Subcode
                        Left Join (Select SILTV.Subcode, Max(SILTV.Agent) as Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode) as LTV On Sg.Subcode = LTV.Subcode
                        Where 1=1 "
                mQry = mQry & mCondStr & " and Date(Lg.V_Date) < " & AgL.Chk_Date(ReportFrm.FGetText(rowFromDate)) & " "
                mQry = mQry & " Group By Lg.Subcode"

                dtParty = AgL.FillData(mQry, AgL.GCn).Tables(0)

                If dtParty.Rows.Count > 0 Then
                    For i = 0 To dtParty.Rows.Count - 1
                        mQry = ""
                        If AgL.XNull(dtParty.Rows(i)("Nature")) = "Customer" Then
                            If AgL.VNull(dtParty.Rows(i)("Balance")) > 0 Then
                                mQry = "Select Lg.DocID, Lg.DivCode, Lg.Site_Code, Lg.V_Type, Vt.Description as V_TypeDesc, 
                                Lg.RecId, LG.Subcode, IfNull(Lg.EffectiveDate,Lg.V_Date) as V_Date, Lg.Narration, Lg.AmtDr as Amount                                
                                From Ledger Lg  With (NoLock)
                                LEFT JOIN SubGroup SG On SG.Subcode =LG.SubCode  
                                Left Join (Select SILTV.Subcode, Max(SILTV.Agent) as Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode) as LTV On Sg.Subcode = LTV.Subcode
                                Left Join Voucher_Type Vt  With (NoLock) On Lg.V_Type = Vt.V_Type
                                Where Date(Lg.V_Date) < " & AgL.Chk_Date(ReportFrm.FGetText(rowFromDate)) & " And Lg.Subcode = '" & AgL.XNull(dtParty.Rows(i)("Subcode")) & "'  And Lg.AmtDr > 0  " & mCondStr & "                               
                                Order By IfNull(Lg.EffectiveDate,Lg.V_Date) Desc, Lg.RecId desc"
                            End If
                        Else
                            If AgL.VNull(dtParty.Rows(i)("Balance")) < 0 Then
                                mQry = "Select Lg.DocID, Lg.DivCode, Lg.Site_Code, Lg.V_Type, Vt.Description as V_TypeDesc, 
                                Lg.RecId, LG.Subcode, IfNull(Lg.EffectiveDate,Lg.V_Date) as V_Date, Lg.Narration, Lg.AmtCr as Amount                                
                                From Ledger Lg  With (NoLock)
                                LEFT JOIN SubGroup SG On SG.Subcode =LG.SubCode  
                                Left Join (Select SILTV.Subcode, Max(SILTV.Agent) as Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode) as LTV On Sg.Subcode = LTV.Subcode
                                Left Join Voucher_Type Vt On Lg.V_Type = Vt.V_Type
                                Where  Date(Lg.V_Date) < " & AgL.Chk_Date(ReportFrm.FGetText(rowFromDate)) & " And Lg.Subcode = '" & AgL.XNull(dtParty.Rows(i)("Subcode")) & "'  And Lg.AmtCr > 0 " & mCondStr & " 
                                Order By IfNull(Lg.EffectiveDate,Lg.V_Date) Desc, Lg.RecId desc"
                            End If
                        End If


                        BalAmount = 0 : DrCr = ""
                        mRemainingBalance = Math.Abs(AgL.VNull(dtParty.Rows(i)("Balance")))
                        If mQry <> "" Then
                            DtMain = AgL.FillData(mQry, AgL.GCn).Tables(0)
                            If DtMain.Rows.Count > 0 Then
                                For j = 0 To DtMain.Rows.Count - 1

                                    If mRemainingBalance > 0 Then

                                        'objOutstandingBill = New OutstandingBill
                                        'objOutstandingBill.DocNo = AgL.XNull(DtMain.Rows(j)("DocNo"))
                                        'objOutstandingBill.DocDate = AgL.XNull(DtMain.Rows(j)("V_Date"))
                                        'objOutstandingBill.Narration = IIf(AgL.XNull(DtMain.Rows(j)("Narration")) = "", AgL.XNull(DtMain.Rows(j)("V_TypeDesc")), AgL.XNull(DtMain.Rows(j)("Narration")))
                                        'objOutstandingBill.DocAmount = AgL.VNull(DtMain.Rows(j)("Amount"))
                                        'If mRemainingBalance > AgL.VNull(DtMain.Rows(j)("Amount")) Then
                                        '    objOutstandingBill.BalAmount = Format(AgL.VNull(DtMain.Rows(j)("Amount")), "0.00")
                                        '    mRemainingBalance = mRemainingBalance - AgL.VNull(DtMain.Rows(j)("Amount"))
                                        'Else
                                        '    objOutstandingBill.BalAmount = Format(mRemainingBalance, "0.00")
                                        '    mRemainingBalance = mRemainingBalance - mRemainingBalance
                                        'End If
                                        'objOutstandingBill.DrCr = IIf(AgL.VNull(dtParty.Rows(i)("Balance")) > 0, "Dr", "Cr")

                                        'OutstandingBills.Add(objOutstandingBill)



                                        If mRemainingBalance > AgL.VNull(DtMain.Rows(j)("Amount")) Then
                                            BalAmount = Format(AgL.VNull(DtMain.Rows(j)("Amount")), "0.00")
                                            mRemainingBalance = mRemainingBalance - AgL.VNull(DtMain.Rows(j)("Amount"))
                                        Else
                                            BalAmount = Format(mRemainingBalance, "0.00")
                                            mRemainingBalance = mRemainingBalance - mRemainingBalance
                                        End If
                                        DrCr = IIf(AgL.VNull(dtParty.Rows(i)("Balance")) > 0, "Dr", "Cr")


                                        mQry = "Insert Into #FifoOutstanding
                                            (DocID, V_Type, RecID, V_Date, 
                                            Site_Code, Div_Code, Subcode, BillAmount, 
                                            BalanceAmount, DrCr, Narration)    
                                            Values(" & AgL.Chk_Text(AgL.XNull(DtMain.Rows(j)("DocID"))) & ",
                                            " & AgL.Chk_Text(AgL.XNull(DtMain.Rows(j)("V_Type"))) & ",
                                            " & AgL.Chk_Text(AgL.XNull(DtMain.Rows(j)("RecID"))) & ",
                                            " & AgL.Chk_Date(AgL.XNull(DtMain.Rows(j)("V_Date"))) & ",                                            
                                            " & AgL.Chk_Text(AgL.XNull(DtMain.Rows(j)("Site_Code"))) & ",
                                            " & AgL.Chk_Text(AgL.XNull(DtMain.Rows(j)("DivCode"))) & ",
                                            " & AgL.Chk_Text(AgL.XNull(DtMain.Rows(j)("Subcode"))) & ",
                                            " & AgL.Chk_Text(AgL.XNull(DtMain.Rows(j)("Amount"))) & ",
                                            " & BalAmount & ",
                                            " & AgL.Chk_Text(DrCr) & ",
                                            " & AgL.Chk_Text(AgL.XNull(DtMain.Rows(j)("Narration"))) & "
                                            )
                                            "

                                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
                                    End If
                                Next
                            End If
                        End If
                    Next
                End If
                mQry = "Select * from #FifoOutstanding"
                DtMain = AgL.FillData(mQry, AgL.GCn).Tables(0)

                mQry = "Select Null as DocID, Null as V_Type, strftime('%m-%Y', H.V_Date) as RecID, Null as V_Date, 
                        Null Site_Code, Null Div_Code, Null as LrNo, H.Subcode, Sg.name || (Case When IfNull(City.CityName,'') <> '' Then ', ' || IfNull(City.CityName,'') else '' End) as PartyName,
                        City.CityName, Null as Narration, 0 as TaxableAmount, 0 as TaxAmount,
                        0 as Addition, 0 as BillAmount, 0 as SaleReturn, 0 as Payment, Sum(Case When H.DrCr='Dr' Then H.BalanceAmount Else -H.BalanceAmount End) as Adjustment, 
                        0 as Balance,Sum(Case When H.DrCr='Dr' Then H.BalanceAmount Else 0 End) as AmtDr, Sum(Case When H.DrCr='Cr' Then H.BalanceAmount Else 0 End) as AmtCr
                        From #FifoOutstanding H
                        Left Join Subgroup Sg on H.Subcode = Sg.Subcode
                        Left Join City On Sg.CityCode = City.CityCode
                        Group By H.Subcode, strftime('%m-%Y', H.V_Date)
                        Order By strftime('%Y', H.V_Date), strftime('%m', H.V_Date)
                        "
                DtMain = AgL.FillData(mQry, AgL.GCn).Tables(0)

                mQry = "Insert Into #TempRecord 
                        (DocID, V_Type, RecId, V_Date, 
                        Site_code, Div_Code, LrNo, Subcode, PartyName, 
                        PartyCity, Narration, TaxableAmount, TaxAmount, 
                        Addition, BillAmount, SaleReturn, Payment, 
                        Adjustment,Balance, AmtDr, AmtCr) 
                        Select Null as DocID, Null as V_Type, strftime('%m-%Y', H.V_Date) as RecID, Null as V_Date, 
                        Null Site_Code, Null Div_Code, Null as LrNo, H.Subcode, Sg.name || (Case When IfNull(City.CityName,'') <> '' Then ', ' || IfNull(City.CityName,'') else '' End) as PartyName,
                        City.CityName, Null as Narration, 0 as TaxableAmount, 0 as TaxAmount,
                        0 as Addition, 0 as BillAmount, 0 as SaleReturn, 0 as Payment, Sum(Case When H.DrCr='Dr' Then H.BalanceAmount Else -H.BalanceAmount End) as Adjustment, 
                        0 as Balance, Sum(Case When H.DrCr='Dr' Then H.BalanceAmount Else 0 End) as AmtDr, Sum(Case When H.DrCr='Cr' Then H.BalanceAmount Else 0 End) as AmtCr
                        From #FifoOutstanding H
                        Left Join Subgroup Sg on H.Subcode = Sg.Subcode
                        Left Join City On Sg.CityCode = City.CityCode
                        Group By H.Subcode, strftime('%m-%Y', H.V_Date)
                        Order By strftime('%Y', H.V_Date), strftime('%m', H.V_Date)
                       "

                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
            End If



            mQry = " SELECT LG.DocId, LG.V_Type, VT.NCat, LG.SubCode, LG.RecId, LG.V_Date, Lg.Subcode, 
                    Sg.name || (Case When IfNull(Ct.CityName,'') <> '' Then ', ' || IfNull(Ct.CityName,'') else '' End) as PartyName, CT.CityName as PartyCity, LG.AmtDr, LG.AmtCr, LG.AmtDr + LG.AmtCr as Amount,
                    SIT.LrNo as SLrNo, SI.Taxable_Amount as STaxableAmount, SI.Tax1 + SI.Tax2 + SI.Tax3 + SI.Tax4 + SI.Tax5 as STaxAmount, 
                    SI.Other_Charge - SI.Deduction as SAddition, SI.Net_Amount as SBillAmount, 
                    PIT.LrNo as PLrNo, PI.Taxable_Amount as PTaxableAmount, PI.Tax1 + PI.Tax2 + PI.Tax3 + PI.Tax4 + PI.Tax5 as PTaxAmount, 
                    PI.Other_Charge - PI.Deduction as PAddition, PI.Net_Amount as PBillAmount,  
                    LG.Site_Code, LG.DivCode As Div_Code, 
                    (Case When IfNull(GenSI.SaletoPartyName,'') <> '' Then IfNull(GenSI.SaletoPartyName,'') || '. ' Else '' End) || LG.Narration as Narration
                    FROM Ledger LG 
                    LEFT JOIN SubGroup SG On SG.Subcode =LG.SubCode  
                    Left Join SaleInvoice SI On LG.DocID = SI.DocId
                    Left Join SaleInvoiceTransport SIT On SI.DocID = SIT.DocID
                    Left Join PurchInvoice PI On LG.DocID = PI.DocId
                    Left Join PurchInvoiceTransport PIT On PI.DocID = PIT.DocID
                    Left Join SaleInvoice GenSI On PI.GenDocId = GenSI.DocId
                    Left Join Voucher_Type Vt On Lg.V_Type = Vt.V_Type
                    Left Join (Select SILTV.Subcode, Max(SILTV.Agent) as Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode) as LTV On Sg.Subcode = LTV.Subcode
                    LEFT JOIN City CT On SG.CityCode  =CT.CityCode "
            mQry = mQry & "Where SG.Nature In ('Customer','Supplier') " + mCondStr

            If ReportFrm.FGetText(rowFromDate) <> "" Then
                mQry = mQry & " And Date(Lg.V_Date) >= " & AgL.Chk_Date(ReportFrm.FGetText(rowFromDate)) & " "
            End If
            mQry = mQry & " Order By LG.V_Date, LG.V_Type, LG.RecID  "


            DtLedger = AgL.FillData(mQry, AgL.GCn).Tables(0)

            Dim sLed As StructLedger

            For I As Integer = 0 To DtLedger.Rows.Count - 1
                sLed = New StructLedger

                sLed.DocID = AgL.XNull(DtLedger.Rows(I)("DocID"))
                sLed.V_Type = AgL.XNull(DtLedger.Rows(I)("V_Type"))
                sLed.V_Date = AgL.XNull(DtLedger.Rows(I)("V_Date"))
                sLed.RecId = AgL.XNull(DtLedger.Rows(I)("RecId"))
                sLed.Div_Code = AgL.XNull(DtLedger.Rows(I)("Div_Code"))
                sLed.Site_Code = AgL.XNull(DtLedger.Rows(I)("Site_Code"))
                sLed.Subcode = AgL.XNull(DtLedger.Rows(I)("Subcode"))
                sLed.PartyName = AgL.XNull(DtLedger.Rows(I)("PartyName"))
                sLed.PartyCity = AgL.XNull(DtLedger.Rows(I)("PartyCity"))
                sLed.AmtDr = AgL.XNull(DtLedger.Rows(I)("AmtDr"))
                sLed.AmtCr = AgL.XNull(DtLedger.Rows(I)("AmtCr"))


                Select Case AgL.XNull(DtLedger.Rows(I)("NCat"))
                    Case Ncat.SaleInvoice
                        sLed.LRNo = AgL.XNull(DtLedger.Rows(I)("SLRNo"))
                        sLed.TaxableAmount = AgL.VNull(DtLedger.Rows(I)("STaxableAmount"))
                        sLed.TaxAmount = AgL.VNull(DtLedger.Rows(I)("STaxAmount"))
                        sLed.Addition = AgL.VNull(DtLedger.Rows(I)("SAddition"))
                        sLed.BillAmount = AgL.VNull(DtLedger.Rows(I)("SBillAmount"))
                    Case Ncat.PurchaseInvoice
                        sLed.LRNo = AgL.XNull(DtLedger.Rows(I)("PLRNo"))
                        sLed.TaxableAmount = AgL.VNull(DtLedger.Rows(I)("PTaxableAmount"))
                        sLed.TaxAmount = AgL.VNull(DtLedger.Rows(I)("PTaxAmount"))
                        sLed.Addition = AgL.VNull(DtLedger.Rows(I)("PAddition"))
                        sLed.BillAmount = AgL.VNull(DtLedger.Rows(I)("PBillAmount"))
                    Case Ncat.PurchaseReturn, Ncat.SaleReturn
                        sLed.GoodsReturn = AgL.VNull(DtLedger.Rows(I)("Amount"))
                    Case Ncat.Payment, Ncat.Receipt, Ncat.PaymentSettlement, Ncat.ReceiptSettlement
                        sLed.Payment = AgL.VNull(DtLedger.Rows(I)("Amount"))
                    Case Else
                        sLed.Adjustment = AgL.VNull(DtLedger.Rows(I)("AmtDr")) - AgL.VNull(DtLedger.Rows(I)("AmtCr"))
                End Select

                sLed.Narration = AgL.XNull(DtLedger.Rows(I)("Narration"))



                mQry = "Insert Into #TempRecord 
                        (DocID, V_Type, RecId, V_Date, 
                        Site_code, Div_Code, LrNo, Subcode, PartyName, 
                        PartyCity, Narration, TaxableAmount, TaxAmount, 
                        Addition, BillAmount, SaleReturn, Payment, 
                        Adjustment,Balance, AmtDr, AmtCr) 
                        Values (" & AgL.Chk_Text(sLed.DocID) & ", " & AgL.Chk_Text(sLed.V_Type) & ", 
                        " & AgL.Chk_Text(sLed.RecId) & "," & AgL.Chk_Date(sLed.V_Date) & ",
                        " & AgL.Chk_Text(sLed.Site_Code) & ", " & AgL.Chk_Text(sLed.Div_Code) & "," & AgL.Chk_Text(sLed.LRNo) & ",
                        " & AgL.Chk_Text(sLed.Subcode) & ", " & AgL.Chk_Text(sLed.PartyName) & ", 
                        " & AgL.Chk_Text(sLed.PartyCity) & ", " & AgL.Chk_Text(sLed.Narration) & ", 
                        " & AgL.VNull(sLed.TaxableAmount) & ", " & AgL.VNull(sLed.TaxAmount) & ",
                        " & AgL.VNull(sLed.Addition) & ", " & AgL.VNull(sLed.BillAmount) & ", 
                        " & AgL.VNull(sLed.GoodsReturn) & ", " & AgL.VNull(sLed.Payment) & ",
                        " & AgL.VNull(sLed.Adjustment) & ", " & AgL.VNull(sLed.Balance) & ",
                        " & AgL.VNull(sLed.AmtDr) & ", " & AgL.VNull(sLed.AmtCr) & "
                        )"

                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
            Next

            mQry = " SELECT H.*, Div.Div_Name, Site.Name as Site_Name 
                       From #TempRecord H
                       Left Join Division Div On H.Div_Code = Div.Div_Code 
                       Left Join SiteMast Site On H.Site_Code = Site.Code "


            RepName = "PartyLedger" : RepTitle = "Party Ledger"

            DsRep = AgL.FillData(mQry, AgL.GCn)

            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records to Print!")

            ReportFrm.PrintReport(DsRep, RepName, RepTitle)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


End Class
