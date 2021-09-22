Imports System.Data.SqlClient
Imports System.Data.SQLite
Imports System.IO
Imports AgLibrary.ClsMain
Imports AgLibrary.ClsMain.agConstants
Imports CrystalDecisions.CrystalReports.Engine

Public Class ClsKiranaCustomerLedger



    Dim StrArr1() As String = Nothing, StrArr2() As String = Nothing, StrArr3() As String = Nothing, StrArr4() As String = Nothing, StrArr5() As String = Nothing

    Dim mGRepFormName As String = ""
    Dim ErrorLog As String = ""

    'Dim WithEvents ReportFrm As Aglibrary.FrmReportLayout
    Dim WithEvents ReportFrm As AgLibrary.FrmReportLayout

    Public Property GRepFormName() As String
        Get
            GRepFormName = mGRepFormName
        End Get
        Set(ByVal value As String)
            mGRepFormName = value
        End Set
    End Property


    Private Const CustomerLedger As String = "CustomerLedger"



    Dim mHelpAcGroupQry$ = "Select 'o' As Tick, GroupCode as Code, GroupName as Name From AcGroup Order By GroupName "
    Dim mHelpAreaQry$ = "Select 'o' As Tick, Code, Description From Area "
    Dim mHelpCityQry$ = "Select 'o' As Tick, CityCode, CityName From City "
    Dim mHelpStateQry$ = "Select 'o' As Tick, State_Code, State_Desc From State "
    Dim mHelpSiteQry$ = "Select 'o' As Tick, Code, Name As [Site] From SiteMast Where  Code In (" & AgL.PubSiteList & ")  "
    Dim mHelpDivisionQry$ = "Select 'o' As Tick, Div_Code as Code, Div_Name As [Division] From Division "
    Dim mHelpVendorQry$ = " Select 'o' As Tick,  H.SubCode As Code, Sg.DispName AS Vendor FROM Vendor H LEFT JOIN SubGroup Sg ON H.SubCode = Sg.SubCode "
    Dim mHelpPartyQry$ = " Select 'o' As Tick,  Sg.Code, Sg.Name AS Party, Sg.Address, Ag.GroupName FROM viewHelpSubGroup Sg  Left Join AcGroup Ag On Sg.groupCode= ag.GroupCode Where Sg.Nature In ('Customer','Supplier','Cash') Order By Name "
    Dim mHelpSubgroupQry$ = " Select 'o' As Tick,  Sg.Code, Sg.Name AS Party, Sg.Address, Ag.GroupName FROM viewHelpSubGroup Sg  Left Join AcGroup Ag On Sg.groupCode= ag.GroupCode Order By Name "
    Dim mHelpPartySingleQry$ = " Select Sg.Code, Sg.Name AS Party FROM viewHelpSubGroup Sg Where Sg.Nature In ('Customer','Supplier','Cash') "
    Dim mHelpAgentQry$ = " Select 'o' As Tick,  Sg.Code, Sg.Name AS Agent FROM viewHelpSubgroup Sg Where Sg.SubgroupType = '" & SubgroupType.SalesAgent & "' "
    Dim mHelpYesNo$ = " Select 'Yes' As Code, 'Yes' AS [Value] Union All Select 'No' As Code, 'No' AS [Value] "
    Dim mHelpResponsiblePersonQry$ = " Select 'o' As Tick, Sg.Code, Sg.Name AS Agent FROM viewHelpSubgroup Sg Left Join HRM_Employee E On Sg.Code = E.Subcode LEFT JOIN HRM_Designation D ON E.designation = D.Code Where Sg.SubgroupType = '" & SubgroupType.Employee & "' AND D.Code <>'SREP' "
    Dim mHelpSalesAgentQry$ = " Select 'o' As Tick, Sg.Code, Sg.Name AS [Responsible Person] FROM viewHelpSubgroup Sg Where Sg.SubgroupType = '" & SubgroupType.SalesAgent & "' "
    Dim mHelpTagQry$ = "Select 'o' As Tick, H.Code, H.Description   FROM Tag H "

    Dim DsRep As DataSet = Nothing, DsRep1 As DataSet = Nothing, DsRep2 As DataSet = Nothing
    Dim mQry$ = "", RepName$ = "", RepTitle$ = "", OrderByStr$ = ""

    Dim StrMonth$ = ""
    Dim StrQuarter$ = ""
    Dim StrFinancialYear$ = ""
    Dim StrTaxPeriod$ = ""

    Dim rowReportType As Integer = 0
    Dim rowGroupOn As Integer = 1
    Dim rowFromDate As Integer = 2
    Dim rowBillsUptoDate As Integer = 3
    Dim rowPaymentsUptoDate As Integer = 4
    Dim rowRecordsType As Integer = 5
    Dim rowParty As Integer = 6
    Dim rowMasterParty As Integer = 7
    Dim rowLinkedParty As Integer = 8
    Dim rowAgent As Integer = 9
    Dim rowCity As Integer = 10
    Dim rowArea As Integer = 11
    Dim rowDivision As Integer = 12
    Dim rowSite As Integer = 13


    Dim mPartyNature As String


    Private Structure StructLedger
        Public DocID As String
        Public V_Type As String
        Public V_Date As String
        Public RecId As String
        Public Div_Code As String
        Public Site_Code As String
        Public Subcode As String
        Public LinkedSubcode As String
        Public GoodsReturn As Double
        Public Payment As Double
        Public ChqNo As String
        Public Adjustment As Double
        Public Balance As Double
        Public Narration As String
        Public AmtDr As Double
        Public AmtCr As Double
        Public AdjDocID As String
        Public DueDate As String
        Public AdjDate As String
        Public AdjAmount As Double
        Public AdjVAmount As Double
        Public InterestDays As Double
        Public InterestAmount As Double
        Public InterestBalance As Double
    End Structure

    Private Sub CreateTemporaryTables()
        Try
            mQry = "Drop Table #TempRecord"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        Catch ex As Exception
        End Try

        Try
            mQry = "Drop Table TempInterestRecord"
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
                    Subcode nvarchar(10),
                    LinkedSubcode nvarchar(10),
                    GoodsReturn Float,
                    Adjustment Float, 
                    Payment Float,
                    ChqNo  varchar(1000),
                    Balance Float,  
                    AmtDr Float,
                    AmtCr Float,                       
                    Narration  varchar(2000),
                    AdjDocID nVarchar(21),
                    DueDate DateTime,
                    AdjDate DateTime,
                    AdjAmount Float,
                    AdjVAmount Float,
                    InterestDays Float,
                    InterestAmount Float,
                    InterestBalance Float
                ); "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)




        mQry = mQry.Replace("#TempRecord", "#TempInterestRecord")
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

    End Sub


    Private Function CreateCondStr() As String
        Dim mCondStr As String

        mCondStr = " And Sg.Nature In ('Customer','Supplier') "
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
            If ClsMain.FDivisionNameForCustomization(22) = "W SHYAMA SHYAM FABRICS" Then
                mCondStr = mCondStr & " And LG.DocID Not In ( SELECT H.DocID FROM LedgerHead H LEFT JOIN LedgerHeadDetail L ON H.DocID = L.DocID WHERE H.V_Type In ('WPS','WRS') ) "
            Else
                mCondStr = mCondStr & " And LG.DocID Not In ( SELECT H.DocID FROM LedgerHead H LEFT JOIN LedgerHeadDetail L ON H.DocID = L.DocID WHERE H.V_Type In ('" & Ncat.PaymentSettlement & "','" & Ncat.ReceiptSettlement & "') ) "
            End If
        End If



        If ReportFrm.FGetText(rowParty) = "" Or ReportFrm.FGetText(rowParty).ToString.ToUpper = "ALL" Then
            If mPartyNature = "" Then
                mQry = "SELECT 'CUSTOMER' AS Code, 'CUSTOMER' AS Name
                        UNION ALL
                        SELECT 'SUPPLIER' AS Code, 'SUPPLIER' AS Name"
                Dim DtDivision As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

                Dim FRH_Single_Division As DMHelpGrid.FrmHelpGrid
                FRH_Single_Division = New DMHelpGrid.FrmHelpGrid(New DataView(DtDivision), "", 350, 300, 150, 320, False)
                FRH_Single_Division.FFormatColumn(0, , 0, , False)
                FRH_Single_Division.FFormatColumn(1, "NATURE", 100, DataGridViewContentAlignment.MiddleLeft)
                FRH_Single_Division.StartPosition = FormStartPosition.Manual
                FRH_Single_Division.ShowDialog()

                If FRH_Single_Division.BytBtnValue = 0 Then
                    mPartyNature = AgL.XNull(FRH_Single_Division.DRReturn("Code"))
                Else
                    mPartyNature = ""
                End If
            End If
            If mPartyNature <> "" Then
                mCondStr = mCondStr & " And Sg.Nature = '" & mPartyNature & "'"
            Else
                mCondStr = mCondStr & " And 1=2 "
            End If
        End If



        CreateCondStr = mCondStr
    End Function


    Enum ShowDataIn
        Grid = 1
        Crystal = 2
    End Enum
    Private Sub GetDataReady(mCondStr As String, ShowDataIn As ShowDataIn)


        Dim mFromDate As String
        If ReportFrm.FGetText(rowReportType) = "Interest Ledger" Then
            mFromDate = ""
        Else
            mFromDate = ReportFrm.FGetText(rowFromDate)
        End If





        Dim SubCode As String = "", Party As String = "", PCity As String = "", SiteCode As String = "", DivCode As String = ""
        Dim Cr As Double = 0, Adv As Double = 0
        Dim runningDr As Double = 0






        Dim DtLedger As DataTable = Nothing





        GetDataReadyForFIFOBalance(mCondStr)


        mQry = " SELECT LG.DocId, LG.V_Type, VT.NCat, Sg.SubCode, LG.LinkedSubcode, LG.RecId, LG.V_Date, Sg.Subcode, Sg.Nature, 
                        Sg.name || (Case When IfNull(Ct.CityName,'') <> '' Then ', ' || IfNull(Ct.CityName,'') else '' End) as PartyName, CT.CityName as PartyCity, 
                        LG.AmtDr, LG.AmtCr, LG.AmtDr + LG.AmtCr as Amount,
                        LG.Site_Code, LG.DivCode As Div_Code, 
                        (Case When VT.NCat Not In ( '" & Ncat.SaleInvoice & "', '" & Ncat.PurchaseInvoice & "'  ) Then LG.Narration || '. ' Else '' End) || IfNull(LG.Chq_No,'') as Narration
                        FROM Ledger LG "
        If ReportFrm.FGetText(rowGroupOn) = "Linked Party" Then
            mQry = mQry & " Left Join SubGroup SG On SG.Subcode = IfNull(LG.LinkedSubcode,LG.SubCode)   "
        Else
            mQry = mQry & " Left Join SubGroup SG On SG.Subcode =LG.SubCode   "
        End If

        mQry = mQry & "Left Join Voucher_Type Vt On Lg.V_Type = Vt.V_Type
                        Left Join (Select SILTV.Subcode, Max(SILTV.Agent) as Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode) as LTV On Sg.Subcode = LTV.Subcode
                        LEFT JOIN City CT On SG.CityCode  =CT.CityCode "
        mQry = mQry & "Where SG.Nature In ('Customer','Supplier') " + mCondStr

        If mFromDate <> "" Then
            mQry = mQry & " And Date(Lg.V_Date) >= " & AgL.Chk_Date(mFromDate) & " "
        End If
        mQry = mQry & " Order By Sg.Subcode, LG.V_Date, LG.V_Type, LG.RecID  "


        DtLedger = AgL.FillData(mQry, AgL.GCn).Tables(0)


        Dim sLed As StructLedger
        Dim mRunningBalanace As Double = 0
        For I As Integer = 0 To DtLedger.Rows.Count - 1
            sLed = New StructLedger

            sLed.DocID = AgL.XNull(DtLedger.Rows(I)("DocID"))
            sLed.V_Type = AgL.XNull(DtLedger.Rows(I)("V_Type"))
            sLed.V_Date = AgL.XNull(DtLedger.Rows(I)("V_Date"))
            sLed.RecId = AgL.XNull(DtLedger.Rows(I)("RecId"))
            sLed.Div_Code = AgL.XNull(DtLedger.Rows(I)("Div_Code"))
            sLed.Site_Code = AgL.XNull(DtLedger.Rows(I)("Site_Code"))
            sLed.Subcode = AgL.XNull(DtLedger.Rows(I)("Subcode"))
            sLed.LinkedSubcode = AgL.XNull(DtLedger.Rows(I)("LinkedSubcode"))
            sLed.AmtDr = AgL.VNull(DtLedger.Rows(I)("AmtDr"))
            sLed.AmtCr = AgL.VNull(DtLedger.Rows(I)("AmtCr"))
            mRunningBalanace = mRunningBalanace + (sLed.AmtDr - sLed.AmtCr)
            sLed.Balance = mRunningBalanace
            sLed.Narration = AgL.XNull(DtLedger.Rows(I)("Narration")).ToString.Replace("W Opening Balance", "")


            Select Case AgL.XNull(DtLedger.Rows(I)("NCat"))
                Case Ncat.SaleInvoice, Ncat.PurchaseInvoice
                Case Ncat.PurchaseReturn
                    sLed.GoodsReturn = AgL.VNull(DtLedger.Rows(I)("Amount"))
                Case Ncat.SaleReturn
                    sLed.GoodsReturn = AgL.VNull(DtLedger.Rows(I)("Amount"))
                Case Ncat.Receipt, Ncat.ReceiptSettlement
                    If AgL.XNull(DtLedger.Rows(I)("Nature")) = "Customer" Then
                        sLed.Payment = AgL.VNull(DtLedger.Rows(I)("Amount"))
                    Else
                        sLed.Adjustment = AgL.VNull(DtLedger.Rows(I)("Amount"))
                    End If

                Case Ncat.Payment, Ncat.PaymentSettlement
                    If AgL.XNull(DtLedger.Rows(I)("Nature")) = "Supplier" Then
                        sLed.Payment = AgL.VNull(DtLedger.Rows(I)("Amount"))
                    Else
                        sLed.Adjustment = AgL.VNull(DtLedger.Rows(I)("Amount"))
                    End If
                Case Ncat.OpeningBalance
                    If AgL.XNull(DtLedger.Rows(I)("Nature")) = "Customer" Then
                        If AgL.VNull(DtLedger.Rows(I)("AmtCr")) > 0 Then
                            sLed.Payment = AgL.VNull(DtLedger.Rows(I)("AmtCr"))
                        End If
                    ElseIf AgL.XNull(DtLedger.Rows(I)("Nature")) = "Supplier" Then
                        If AgL.VNull(DtLedger.Rows(I)("AmtDr")) > 0 Then
                            sLed.Payment = AgL.VNull(DtLedger.Rows(I)("AmtDr"))
                        End If
                    End If
                Case Else
                    sLed.Adjustment = AgL.VNull(DtLedger.Rows(I)("AmtDr")) - AgL.VNull(DtLedger.Rows(I)("AmtCr"))
            End Select







            mQry = "Insert Into #TempRecord 
                            (DocID, V_Type, RecId, V_Date, 
                            Site_code, Div_Code, Subcode, LinkedSubcode, 
                            Narration, GoodsReturn, Payment, 
                            Adjustment,Balance, AmtDr, AmtCr, Narration) 
                            Values (" & AgL.Chk_Text(sLed.DocID) & ", " & AgL.Chk_Text(sLed.V_Type) & ", 
                            " & AgL.Chk_Text(sLed.RecId) & "," & AgL.Chk_Date(sLed.V_Date) & ",
                            " & AgL.Chk_Text(sLed.Site_Code) & ", " & AgL.Chk_Text(sLed.Div_Code) & ",                            
                            " & AgL.Chk_Text(sLed.Subcode) & ", " & AgL.Chk_Text(sLed.LinkedSubcode) & "," & AgL.Chk_Text(sLed.Narration) & ", 
                            " & AgL.VNull(sLed.GoodsReturn) & ", " & AgL.VNull(sLed.Payment) & ",
                            " & AgL.VNull(sLed.Adjustment) & ", " & AgL.VNull(sLed.Balance) & ",
                            " & AgL.VNull(sLed.AmtDr) & ", " & AgL.VNull(sLed.AmtCr) & ", " & AgL.Chk_Text(sLed.Narration) & "
                            )"

            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        Next


        If ReportFrm.FGetText(rowReportType) = "Interest Ledger" Then
            GetDataReadyForInterestLedger(mCondStr, ShowDataIn)
        End If
    End Sub

    Private Sub GetDataReadyForInterestLedger(mCondStr As String, ShowDataIn As ShowDataIn)
        Dim DtPayment As DataTable = Nothing
        Dim drPayment As DataRow()
        Dim sLed As StructLedger
        Dim xI As Integer, xJ As Integer

        If ReportFrm.FGetText(rowReportType) = "Interest Ledger" Or ShowDataIn = ShowDataIn.Crystal Then
            Dim DtBills As DataTable

            mQry = "Select H.DocID, H.V_Type, H.RecId, H.V_Date, H.Site_Code, H.Div_Code, 
                    Sg.Subcode, H.LinkedSubcode, H.GoodsReturn, H.Adjustment, H.Payment, H.Balance, 
                    H.AmtDr, H.AmtCr, H.Narration, H.AdjDocID, H.DueDate, H.AdjDate, 
                    H.AdjAmount, H.InterestDays, H.InterestAmount, H.InterestBalance, 
                    Sg.Nature, (Case When Sg.Nature = 'Customer' Then H.AmtDr Else H.AmtCr End) as Amount,
                    I.LeaverageDays
                    From #TempRecord H 
                    Left Join Subgroup MSg On H.LinkedSubcode = MSg.Subcode
                    Left Join InterestSlab I On Msg.InterestSlab = I.Code
                    "
            If ReportFrm.FGetText(rowGroupOn) = "Linked Party" Then
                mQry = mQry & " Left Join SubGroup SG On SG.Subcode = IfNull(H.LinkedSubcode,H.SubCode)  "
            Else
                mQry = mQry & " Left Join SubGroup SG On SG.Subcode =H.SubCode   "
            End If

            mQry = mQry & " Where 1=1 And (Case When Sg.Nature = 'Customer' Then H.AmtDr Else H.AmtCr End) > 0
                            Order By Sg.Subcode, H.V_date, RecID "
            DtBills = AgL.FillData(mQry, AgL.GCn).Tables(0)

            mQry = "Select Lg.DocID, Lg.DivCode, Lg.Site_Code, Lg.V_Type, Vt.Description as V_TypeDesc, 
                                    Lg.RecId, SG.Subcode, IfNull(Lg.EffectiveDate, Lg.V_Date) As V_Date, IfNull(LG.Chq_No,'')  as ChqNo, IfNull(Lg.Narration,'') as Narration, (Case When Sg.Nature='Customer' Then Lg.AmtCr Else LG.AmtDr End) as Amount                                
                                    From Ledger Lg  With (NoLock) "
            If ReportFrm.FGetText(rowGroupOn) = "Linked Party" Then
                mQry = mQry & " Left Join SubGroup SG On SG.Subcode = IfNull(LG.LinkedSubcode,LG.SubCode)  "
            Else
                mQry = mQry & " Left Join SubGroup SG On SG.Subcode =LG.SubCode   "
            End If
            mQry = mQry & " Left Join(Select SILTV.Subcode, Max(SILTV.Agent) As Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode) as LTV On Sg.Subcode = LTV.Subcode
                                    Left Join Voucher_Type Vt  With (NoLock) On Lg.V_Type = Vt.V_Type
                                    Where (Case When Sg.Nature='Customer' Then Lg.AmtCr Else LG.AmtDr End) > 0  " & mCondStr & "                               
                                    Order By IfNull(Lg.EffectiveDate, Lg.V_Date) , Lg.RecId 
                                  "

            DtPayment = AgL.FillData(mQry, AgL.GCn).Tables(0)


            Dim I As Integer
            Dim J As Integer
            Dim mBalPmt As Double
            Dim mBalBill As Double
            Dim mAdjAmt As Double
            Dim mLastInsertedSubcode As String = ""
            'Dim sLed As StructLedger
            drPayment = Nothing
            While I < DtBills.Rows.Count
                If I < DtBills.Rows.Count Then
                    If I = 0 Then
                        drPayment = Nothing
                        drPayment = DtPayment.Select(" Subcode = '" & AgL.XNull(DtBills.Rows(I)("Subcode")) & "' ")
                        mBalBill = DtBills.Rows(I)("Amount")
                    Else
                        If AgL.XNull(DtBills.Rows(I)("Subcode")) <> mLastInsertedSubcode Then 'AgL.XNull(DtBills.Rows(I - 1)("Subcode")) Then
                            drPayment = Nothing
                            drPayment = DtPayment.Select(" Subcode = '" & AgL.XNull(DtBills.Rows(I)("Subcode")) & "' ")
                            J = 0
                        End If
                    End If

                    If drPayment.Length > 0 Then
                        If J < drPayment.Length Then
                            mBalPmt = drPayment(J)("Amount")
                        Else
                            mBalPmt = 0
                        End If
                    End If


                    While J <= drPayment.Length Or drPayment.Length = 0 Or I < DtBills.Rows.Count
                        If I < DtBills.Rows.Count Then
                            sLed = New StructLedger

                            sLed.Subcode = AgL.XNull(DtBills.Rows(I)("Subcode"))
                            sLed.LinkedSubcode = AgL.XNull(DtBills.Rows(I)("LinkedSubcode"))

                            If (I = 0 And J = xJ) Or I <> xI Then
                                sLed.DocID = AgL.XNull(DtBills.Rows(I)("DocID"))
                                sLed.V_Type = AgL.XNull(DtBills.Rows(I)("V_Type"))
                                sLed.V_Date = AgL.XNull(DtBills.Rows(I)("V_Date"))
                                sLed.RecId = AgL.XNull(DtBills.Rows(I)("RecId"))
                                sLed.Div_Code = AgL.XNull(DtBills.Rows(I)("Div_Code"))
                                sLed.Site_Code = AgL.XNull(DtBills.Rows(I)("Site_Code"))
                                sLed.AmtDr = AgL.XNull(DtBills.Rows(I)("AmtDr"))
                                sLed.AmtCr = AgL.XNull(DtBills.Rows(I)("AmtCr"))
                                'mRunningBalanace = mRunningBalanace + (sLed.AmtDr - sLed.AmtCr)
                                sLed.Balance = AgL.VNull(DtBills.Rows(I)("Balance")) 'mRunningBalanace
                                sLed.GoodsReturn = AgL.VNull(DtBills.Rows(I)("GoodsReturn"))
                                sLed.Payment = AgL.VNull(DtBills.Rows(I)("Payment"))
                                sLed.Adjustment = AgL.VNull(DtBills.Rows(I)("Adjustment"))
                                sLed.Narration = AgL.XNull(DtBills.Rows(I)("Narration"))
                            End If


                            xJ = J : xI = I
                            If (AgL.XNull(DtBills.Rows(I)("Nature")) = "Customer" And AgL.VNull(DtBills.Rows(I)("AmtDr")) > 0) Or (AgL.XNull(DtBills.Rows(I)("Nature")) <> "Customer" And AgL.VNull(DtBills.Rows(I)("AmtCr")) > 0) Then


                                If drPayment.Length > 0 And J < drPayment.Length Then


                                    If mBalBill < mBalPmt Then
                                        mAdjAmt = mBalBill
                                        mBalPmt = mBalPmt - mAdjAmt

                                        sLed.AdjDocID = IIf(AgL.XNull(drPayment(J)("V_Type")) = "", "", AgL.XNull(drPayment(J)("V_Type")) & "-") & AgL.XNull(drPayment(J)("RecID"))
                                        sLed.AdjAmount = mAdjAmt
                                        sLed.DueDate = DtBills.Rows(I)("V_Date")
                                        sLed.AdjDate = drPayment(J)("V_Date")

                                        I = I + 1
                                        If I < DtBills.Rows.Count Then
                                            mBalBill = DtBills.Rows(I)("Amount")
                                        Else
                                            mBalBill = 0
                                        End If
                                    ElseIf mBalPmt < mBalBill Then
                                        mAdjAmt = mBalPmt
                                        mBalBill = mBalBill - mAdjAmt

                                        sLed.AdjDocID = IIf(AgL.XNull(drPayment(J)("V_Type")) = "", "", AgL.XNull(drPayment(J)("V_Type")) & "-") & AgL.XNull(drPayment(J)("RecID"))
                                        sLed.AdjAmount = mAdjAmt
                                        sLed.DueDate = DtBills.Rows(I)("V_Date")
                                        sLed.AdjDate = drPayment(J)("V_Date")
                                        sLed.AdjVAmount = drPayment(J)("Amount")
                                        sLed.ChqNo = drPayment(J)("ChqNO")

                                        J = J + 1
                                        If J < drPayment.Length Then
                                            mBalPmt = drPayment(J)("Amount")
                                        Else
                                            mBalPmt = 0
                                        End If
                                    Else
                                        mAdjAmt = mBalBill
                                        mBalBill = 0
                                        mBalPmt = 0


                                        sLed.AdjDocID = IIf(AgL.XNull(drPayment(J)("V_Type")) = "", "", AgL.XNull(drPayment(J)("V_Type")) & "-") & AgL.XNull(drPayment(J)("RecID"))
                                        sLed.AdjAmount = mAdjAmt
                                        sLed.DueDate = DtBills.Rows(I)("V_Date")
                                        sLed.AdjDate = drPayment(J)("V_Date")
                                        sLed.AdjVAmount = drPayment(J)("Amount")
                                        sLed.ChqNo = drPayment(J)("ChqNo")

                                        I = I + 1
                                        J = J + 1
                                        If I < DtBills.Rows.Count Then
                                            mBalBill = DtBills.Rows(I)("Amount")
                                        Else
                                            mBalBill = 0
                                        End If

                                        If J < drPayment.Length Then
                                            mBalPmt = drPayment(J)("Amount")
                                        Else
                                            mBalPmt = 0
                                        End If
                                    End If
                                Else
                                    sLed.DocID = AgL.XNull(DtBills.Rows(I)("DocID"))
                                    sLed.Subcode = AgL.XNull(DtBills.Rows(I)("Subcode"))
                                    sLed.LinkedSubcode = AgL.XNull(DtBills.Rows(I)("LinkedSubcode"))
                                    sLed.AdjDocID = "Balance"
                                    sLed.AdjAmount = mBalBill
                                    sLed.DueDate = AgL.XNull(DtBills.Rows(I)("V_Date"))
                                    sLed.AdjDate = AgL.PubLoginDate
                                    I = I + 1
                                    J = J + 1
                                    If I < DtBills.Rows.Count Then
                                        mBalBill = DtBills.Rows(I)("Amount")
                                    Else
                                        mBalBill = 0
                                    End If


                                End If
                                sLed.InterestDays = DateDiff(DateInterval.Day, CDate(sLed.DueDate), CDate(sLed.AdjDate))
                                'sLed.InterestAmount = Math.Round(sLed.AdjAmount * 12 * sLed.InterestDays / 36500)
                                'mRunningBalanace = mRunningBalanace + sLed.InterestAmount
                                'sLed.InterestBalance = mRunningInterestBalance
                            Else
                                I = I + 1
                                If I < DtBills.Rows.Count Then
                                    mBalBill = DtBills.Rows(I)("Amount")
                                Else
                                    mBalBill = 0
                                End If
                            End If
                            mQry = "Insert Into #TempInterestRecord 
                            (DocID, V_Type, RecId, V_Date, 
                            Site_code, Div_Code, Subcode, LinkedSubcode, 
                            Narration, GoodsReturn, Payment, ChqNo, 
                            Adjustment,Balance, AmtDr, AmtCr, 
                            AdjDocID, DueDate, AdjDate, AdjAmount, AdjVAmount, 
                            InterestDays, InterestAmount, InterestBalance) 
                            Values (" & AgL.Chk_Text(sLed.DocID) & ", " & AgL.Chk_Text(sLed.V_Type) & ", 
                            " & AgL.Chk_Text(sLed.RecId) & "," & AgL.Chk_Date(sLed.V_Date) & ",
                            " & AgL.Chk_Text(sLed.Site_Code) & ", " & AgL.Chk_Text(sLed.Div_Code) & ",                            
                            " & AgL.Chk_Text(sLed.Subcode) & ", " & AgL.Chk_Text(sLed.LinkedSubcode) & "," & AgL.Chk_Text(sLed.Narration) & ", 
                            " & AgL.VNull(sLed.GoodsReturn) & ", " & AgL.VNull(sLed.Payment) & "," & AgL.Chk_Text(sLed.ChqNo) & ",
                            " & AgL.VNull(sLed.Adjustment) & ", " & AgL.VNull(sLed.Balance) & ",
                            " & AgL.VNull(sLed.AmtDr) & ", " & AgL.VNull(sLed.AmtCr) & ",
                            " & AgL.Chk_Text(sLed.AdjDocID) & ", " & AgL.Chk_Date(sLed.DueDate) & ",
                            " & AgL.Chk_Date(sLed.AdjDate) & ", " & AgL.VNull(sLed.AdjAmount) & "," & AgL.VNull(sLed.AdjVAmount) & ",
                            " & AgL.VNull(sLed.InterestDays) & ", " & AgL.VNull(sLed.InterestAmount) & ",
                            " & AgL.VNull(sLed.InterestBalance) & "
                            )"
                            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
                            mLastInsertedSubcode = sLed.Subcode
                        Else
                            J = drPayment.Length + 1
                            Exit While
                        End If

                        If I > 0 Then
                            If I < DtBills.Rows.Count Then
                                If AgL.XNull(DtBills.Rows(I)("Subcode")) <> AgL.XNull(DtBills.Rows(I - 1)("Subcode")) Then
                                    Exit While
                                End If
                            End If
                        End If

                    End While
                    If J > drPayment.Length Then
                        If I > 0 Then
                            If I < DtBills.Rows.Count Then
                                If AgL.XNull(DtBills.Rows(I)("Subcode")) = AgL.XNull(DtBills.Rows(I - 1)("Subcode")) Then
                                    I = I + 1
                                End If
                            End If
                        End If
                    End If
                End If
            End While
        End If

    End Sub



    Public Sub SetAveragePaymentDays()
        Dim mCondStr As String
        Dim dtParty As DataTable
        Dim I As Integer

        mCondStr = CreateCondStr()

        If ClsMain.FDivisionNameForCustomization(20) = "SHYAMA SHYAM FABRICS" Or ClsMain.FDivisionNameForCustomization(22) = "W SHYAMA SHYAM FABRICS" Then Exit Sub

        mQry = "Select Sg.Subcode From Ledger Lg "
        If ReportFrm.FGetText(rowGroupOn) = "Linked Party" Then
            mQry = mQry & " Left Join Subgroup Sg On IfNull(Lg.LinkedSubcode,Lg.Subcode) = Sg.Subcode "
        Else
            mQry = mQry & " Left Join Subgroup Sg On Lg.Subcode = Sg.Subcode "
        End If
        mQry = mQry & " Left Join(Select SILTV.Subcode, Max(SILTV.Agent) As Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode) as LTV On Sg.Subcode = LTV.Subcode
                            Where 1 = 1 "
        mQry = mQry & mCondStr
        mQry = mQry & " Group By Sg.Subcode"

        dtParty = AgL.FillData(mQry, AgL.GCn).Tables(0)


        For I = 0 To dtParty.Rows.Count - 1
            ClsMain.GetAveragePaymentDays(dtParty.Rows(I)("Subcode"), True)
        Next

    End Sub


    Public Sub ProcFormattedPrint()
        Try
            'Dim mCondStr$ = ""

            Dim RepName As String
            Dim RepTitle As String

            Dim DsRep As DataSet
            Dim mMultiplier As Double
            Dim sQryPakkaBalance As String


            Dim sQryPurchaseBrand As String
            Dim sQrySaleBrand As String

            CreateTemporaryTables()

            GetDataReady(CreateCondStr, ShowDataIn.Crystal)

            If ClsMain.FDivisionNameForCustomization(22) = "W SHYAMA SHYAM FABRICS" Then
                mMultiplier = 0.01

                Dim mDbPath As String
                mDbPath = AgL.INIRead(StrPath + "\" + IniName, "CompanyInfo", "ActualDBPath", "")
                Try
                    AgL.Dman_ExecuteNonQry(" attach '" & mDbPath & "' as ODB", AgL.GCn)
                Catch ex As Exception
                End Try
                If ReportFrm.FGetText(rowGroupOn) = "Linked Party" Then
                    sQryPakkaBalance = "(Select Sum(AmtDr-AmtCr) * " & mMultiplier & "  as Balance From ODB.Ledger ODBL Where LinkedSubcode=LSG.OmsID )"
                Else
                    sQryPakkaBalance = "(Select Sum(AmtDr-AmtCr) * " & mMultiplier & " as Balance From ODB.Ledger ODBL Where Subcode=SG.OmsID )"
                End If
            Else
                mMultiplier = 1.0
                sQryPakkaBalance = "(Select 0 as Balance)"
                SetAveragePaymentDays()
            End If


            If ReportFrm.FGetText(rowReportType) = "Interest Ledger" Then
                Dim sQryInterestRate As String
                If AgL.PubServerName = "" Then
                    If ClsMain.FDivisionNameForCustomization(20) = "SHYAMA SHYAM FABRICS" Or ClsMain.FDivisionNameForCustomization(22) = "W SHYAMA SHYAM FABRICS" Then
                        sQryPurchaseBrand = "(Select group_concat(Brand ,',') || ',' From (Select IfNull(sGroup.Description, sItem.Description)  as Brand  from PurchInvoiceDetail sPID  With (NoLock) Left Join Item sItem On sPID.Item = sItem.Code Left Join Item sGroup On sItem.ItemGroup = sGroup.Code Where sPID.DocID = PI.DocID And sItem.ItemType = '" & ItemTypeCode.TradingProduct & "' Group By IfNull(sGroup.Description, sItem.Description)))"
                        sQrySaleBrand = "(Select group_concat(Brand ,',') || ',' From (Select IfNull(sGroup.Description, sItem.Description)  as Brand  from SaleInvoiceDetail sSID  With (NoLock) Left Join Item sItem On sSID.Item = sItem.Code Left Join Item sGroup On sItem.ItemGroup = sGroup.Code Where sSID.DocID = SI.DocID And sItem.ItemType = '" & ItemTypeCode.TradingProduct & "' Group By IfNull(sGroup.Description, sItem.Description)))"
                    Else
                        sQryPurchaseBrand = "(Select group_concat(Brand ,',') || ',' From (Select sGroup.Description  as Brand  from PurchInvoiceDetailSku sPID  With (NoLock) Left Join Item sGroup On sPID.ItemGroup = sGroup.Code Where sPID.DocID = PI.DocID And sGroup.ItemType = '" & ItemTypeCode.TradingProduct & "' Group By sGroup.Description))"
                        sQrySaleBrand = "(Select group_concat(Brand ,',') || ',' From (Select sGroup.Description  as Brand  from SaleInvoiceDetailSku sSID  With (NoLock) Left Join Item sGroup On sSID.ItemGroup = sGroup.Code Where sSID.DocID = SI.DocID And sGroup.ItemType = '" & ItemTypeCode.TradingProduct & "' Group By sGroup.Description))"
                    End If
                    sQryInterestRate = "(Select InterestRate 
                                            From InterestSlabDetail 
                                            Where Code = (Select 
                                            IfNull((Select 
		                                            InterestSlab 
		                                            from ItemGroupPerson 
		                                            Where Person = H.LinkedSubcode 
		                                            And InterestSlab Is Not Null 
		                                            And ItemCategory = (
							                                            Select Max(IfNull(I.ItemCategory, I.Code)) 
							                                            From SaleInvoiceDetail SID 
							                                            Left Join Item I On SId.Item = I.Code 
							                                            Where DocId = H.DocId
							                                            And I.V_Type In ('ITEM','IC')
							                                            )) 
						                                               ,ssg.InterestSlab) 
		                                            From Subgroup ssg 
		                                            Where ssg.Subcode=H.LinkedSubcode
		                                            ) 
                                            And H.InterestDays > DaysGreaterThan
                                            Order By DaysGreaterThan Desc limit 1)"
                    If ClsMain.FDivisionNameForCustomization(22) <> "W SHYAMA SHYAM FABRICS" Then
                        sQryInterestRate = sQryInterestRate.Replace("H.LinkedSubcode", "H.Subcode")
                    End If
                Else
                    sQryPurchaseBrand = "(Select  IfNull(sGroup.Description, sItem.Description)  +  ','  from PurchInvoiceDetail sPID  With (NoLock) Left Join Item sItem On sPID.Item = sItem.Code Left Join Item sGroup On sItem.ItemGroup = sGroup.Code Where sPID.DocID = PI.DocID Group By IfNull(sGroup.Description, sItem.Description) for xml path(''))"
                    sQrySaleBrand = "(Select  IfNull(sGroup.Description, sItem.Description) + ','  from SaleInvoiceDetail sSID  With (NoLock) Left Join Item sItem On sSID.Item = sItem.Code Left Join Item sGroup On sItem.ItemGroup = sGroup.Code Where sSID.DocID = SI.DocID Group By IfNull(sGroup.Description, sItem.Description)  for xml path(''))"
                    'sQryInterestRate = "(Select InterestRate From InterestSlabDetail Where Code = (Select IfNull(Select InterestSlab from ItemGroupPerson Where Subcode = '" & sLed.LinkedSubcode & "' And InterestSlab Is Not Null And ItemCategory = (Select Max(IfNull(I.ItemCategory, I.Code)) From SaleInvoiceDetail SID Left Join Item I On SId.Item = I.Code Where DocId = SI.DocID And I.V_Type In ('" & ItemV_Type.Item & "','" & ItemV_Type.ItemCategory & "')),ssg.InterestSlab) From Subgroup ssg Where ssg.Subcode=H.LinkedSubcode) And DaysGreaterThan > H.InterestDays Order By DaysGreaterThan Desc Limit 1)"
                End If

                Dim mFromDate As String
                mFromDate = ReportFrm.FGetText(rowFromDate)


                mQry = " SELECT (Case When H.V_Date < " & AgL.Chk_Date(mFromDate) & " OR H.DueDate < " & AgL.Chk_Date(mFromDate) & " Then 'Opening' Else Null End) as RecordType,   H.DocID, H.V_Type, H.RecID, H.V_Date, H.Site_Code, H.Div_Code, H.Subcode, H.LinkedSubcode,
                        H.GoodsReturn * " & mMultiplier & " as GoodsReturn, 
                        H.Adjustment * " & mMultiplier & " as Adjustment, 
                        H.Payment * " & mMultiplier & "  as Payment, 
                        H.Balance * " & mMultiplier & "  as Balance, 
                        H.AmtDr * " & mMultiplier & "  as AmtDr, 
                        H.AmtCr * " & mMultiplier & "  As AmtCr, 
                        IfNull(H.Narration,'') || (Case When IfNull(H.Narration,'')<>'' And H.ChqNo<>'' Then Char(10) || H.ChqNo When H.ChqNo<>'' Then H.ChqNo Else '' End) as Narration, H.AdjDocID, H.DueDate, H.AdjDate, 
                        H.AdjVAmount * " & mMultiplier & " as AdjPayment,
                        H.ChqNo as ChqNo,
                        H.AdjAmount * " & mMultiplier & "  as AdjAmount, 
                        H.InterestBalance * " & mMultiplier & "  as InterestBalance,
                        SaleParty.Name as SalePartyName,
                        Sg.name || (Case When IfNull(Ct.CityName,'') <> '' Then ', ' || IfNull(Ct.CityName,'') else '' End) as PartyName, CT.CityName as PartyCity, 
                        LSg.name || (Case When IfNull(LCt.CityName,'') <> '' Then ', ' || IfNull(LCt.CityName,'') else '' End) as LinkedPartyName, LCT.CityName as LinkedPartyCity, 
                        (Case When VT.NCat = '" & Ncat.SaleInvoice & "' Then SIT.LrNo || (Case When SIT.NoOfBales Is Not Null Then ' * ' ||  SIT.NoOfBales Else '' End) When VT.NCat = '" & Ncat.PurchaseInvoice & "' then PIT.LrNo || (Case When PIT.NoOfBales Is Not Null Then ' * ' ||  PIT.NoOfBales Else '' End) Else Null End) as LrNo, 
                        (Case When VT.NCat in ('" & Ncat.SaleInvoice & "','" & Ncat.SaleReturn & "') Then " & sQrySaleBrand & " When VT.NCat In ('" & Ncat.PurchaseInvoice & "','" & Ncat.PurchaseReturn & "') then " & sQryPurchaseBrand & " Else Null End) as Brand,                                                 
                        (Case When VT.NCat = '" & Ncat.SaleInvoice & "' Then SI.AmsDocNo When VT.NCat = '" & Ncat.PurchaseInvoice & "' then PI.AmsDocNo Else Null End) AmsInvNo, 
                        (Case When VT.NCat = '" & Ncat.SaleInvoice & "' Then IfNull(SI.AmsDocNetAmount,'0.00') When VT.NCat = '" & Ncat.PurchaseInvoice & "' then IfNull(PI.AmsDocNetAmount,'0.00') Else 0.00 End) * " & mMultiplier & "  AmsInvAmt, 
                        (Case When VT.NCat = '" & Ncat.SaleInvoice & "' Then SI.Taxable_Amount When VT.NCat = '" & Ncat.PurchaseInvoice & "' then PI.Taxable_Amount Else Null End) * " & mMultiplier & "  TaxableAmt, 
                        (Case When VT.NCat = '" & Ncat.SaleInvoice & "' Then SI.Tax1 + SI.Tax2 + SI.Tax3 + SI.Tax4 + SI.Tax5 When VT.NCat = '" & Ncat.PurchaseInvoice & "' then PI.Tax1 + PI.Tax2 + PI.Tax3 + PI.Tax4 + PI.Tax5 Else Null End) * " & mMultiplier & "  TaxAmt,                             
                        (Case When VT.NCat = '" & Ncat.SaleInvoice & "' Then SI.Other_Charge + SI.Other_Charge1 + SI.Other_Charge2 When VT.NCat = '" & Ncat.PurchaseInvoice & "' then PI.Other_Charge + PI.Other_Charge1 + PI.Other_Charge2 Else Null End) * " & mMultiplier & "  OtherChgAmt,                             
                        (Case When VT.NCat = '" & Ncat.SaleInvoice & "' Then SI.Net_Amount When VT.NCat = '" & Ncat.PurchaseInvoice & "' then PI.Net_Amount Else Null End) * " & mMultiplier & "  BillAmt,                         
                        Div.Div_Name, Site.Name as Site_Name, 
                        H.InterestDays-IsNull(Ints.LeaverageDays,0) as InterestDays, 
                        (Case When IfNull(GenSI.SaletoPartyName,'') <> '' Then IfNull(GenSI.SaletoPartyName,'') || '. ' Else '' End) || (Case When IfNull(ShipParty.Name,'') <> '' then ShipParty.Name || '. ' Else '' End) || IfNull(H.Narration,'') as Narration,                                                
                        Round(IfNull((H.AdjAmount * abs(H.InterestDays-IsNull(Ints.LeaverageDays,0)) * " & sQryInterestRate & " / 36500),0)  * " & mMultiplier & ",2) InterestAmount, Ints.LeaverageDays as InterestLeaverageDays, Ints.Description as InterestSlabDescription, 
                        (Case When IfNull(LSG.CreditLimit,0)>0 Then LSG.CreditLimit Else IfNull(SG.CreditLimit,0) End)  as CreditLimit, " & sQryPakkaBalance & " as PakkaBalance, Sg.AveragePaymentDays
                        From #TempInterestRecord H 
                        Left Join SubGroup SG On SG.Subcode =H.SubCode
                        Left Join Subgroup LSG On H.LinkedSubcode = LSg.Subcode
                        Left JOIN City CT On SG.CityCode  =CT.CityCode                         
                        Left JOIN City LCT On LSG.CityCode  = LCT.CityCode                         
                        Left Join SaleInvoice SI On H.DocID = SI.DocId
                        Left Join SaleInvoiceTransport SIT On SI.DocID = SIT.DocID
                        Left Join Subgroup SaleParty On SI.SaleToParty = SaleParty.Subcode
                        Left Join Subgroup ShipParty On SI.ShipToParty = ShipParty.Subcode
                        Left Join PurchInvoice PI On H.DocID = PI.DocId
                        Left Join PurchInvoiceTransport PIT On PI.DocID = PIT.DocID
                        Left Join SaleInvoice GenSI On PI.GenDocId = GenSI.DocId
                        Left Join Voucher_Type Vt on H.V_type = Vt.V_type
                        Left Join Division Div On H.Div_Code = Div.Div_Code 
                        Left Join SiteMast Site On H.Site_Code = Site.Code 
                        Left Join InterestSlab IntS On IfNull(LSg.InterestSlab,Sg.InterestSlab) = Ints.Code "

                If mFromDate <> "" Then
                    mQry = "
                            Select   (Case When V.RecordType='Opening' Then Null Else V.DocID End) DocID, 
                                Max(Case When V.RecordType='Opening' Then '' Else V.V_Type End) V_Type, 
                                Max(Case When V.RecordType='Opening' Then 'Opening' Else V.RecID End) RecID, 
                                Max(Case When V.RecordType='Opening' Then Null Else V.V_Date End) V_Date, 
                                Max(Case When V.RecordType='Opening' Then '' Else V.Site_Code End) Site_Code, 
                                Max(Case When V.RecordType='Opening' Then '' Else V.Div_Code End) Div_Code, 
                                Max(V.Subcode) as Subcode, 
                                Max(V.LinkedSubcode) LinkedSubcode,
                                Sum(V.GoodsReturn) as GoodsReturn,
                                Sum(V.Adjustment) Adjustment, 
                                Sum(V.Payment) Payment,
                                0.00 As Balance, 
                                Sum(V.AmtDr) as AmtDr,
                                Sum(V.AmtCr) as AmtCr, 
                                Max(Case When V.RecordType='Opening' Then '' Else V.Narration End) Narration, 
                                Max(Case When V.RecordType='Opening' Then '' Else V.AdjDocID End) AdjDocID, 
                                Max(Case When V.RecordType='Opening' Then '' Else V.DueDate End) DueDate, 
                                Max(Case When V.RecordType='Opening' Then '' Else V.AdjDate End) AdjDate, 
                                Sum(Case When V.RecordType='Opening' Then 0.00 Else V.AdjPayment End) AdjPayment,
                                Max(Case When V.RecordType='Opening' Then '' Else V.ChqNo End)  ChqNo,
                                Sum(Case When V.RecordType='Opening' Then 0.00 Else V.AdjAmount End) AdjAmount, 
                                0.00 InterestBalance,
                                Max(Case When V.RecordType='Opening' Then '' Else V.SalePartyName End) As SalePartyName,
                                Max(V.PartyName) As  PartyName, 
                                Max(Case When V.RecordType='Opening' Then '' Else V.PartyCity End) As  PartyCity, 
                                Max(Case When V.RecordType='Opening' Then '' Else V.LinkedPartyName End) As  LinkedPartyName, 
                                Max(Case When V.RecordType='Opening' Then '' Else V.LinkedPartyCity End) As  LinkedPartyCity, 
                                Max(Case When V.RecordType='Opening' Then '' Else V.LrNo End) As  LrNo, 
                                Max(Case When V.RecordType='Opening' Then '' Else V.Brand End) As Brand,                                                 
                                Max(Case When V.RecordType='Opening' Then '' Else V.AmsInvNo End) As AmsInvNo, 
                                Sum(Case When V.RecordType='Opening' Then 0.00 Else V.AmsInvAmt End) As  AmsInvAmt, 
                                Sum(V.TaxableAmt) TaxableAmt, 
                                Sum(V.TaxAmt) as TaxAmt,                             
                                Sum(V.OtherChgAmt) OtherChgAmt,                             
                                Sum(V.BillAmt) BillAmt,                         
                                Max(Case When V.RecordType='Opening' Then '' Else V.Div_Name End) Div_Name, 
                                Max(Case When V.RecordType='Opening' Then '' Else V.Site_Name End) Site_Name, 
                                Max(Case When V.RecordType='Opening' Then 0 Else V.InterestDays End) InterestDays, 
                                Max(Case When V.RecordType='Opening' Then '' Else V.Narration End)  as Narration,                                                
                                Sum(V.InterestAmount) InterestAmount, 
                                Max(Case When V.RecordType='Opening' Then 0 Else V.InterestLeaverageDays End)  InterestLeaverageDays, 
                                Max(Case When V.RecordType='Opening' Then '' Else V.InterestSlabDescription End)  As InterestSlabDescription, 
                                Max(V.CreditLimit)  as CreditLimit, 
                                Max(V.PakkaBalance) PakkaBalance,
                                Max(V.AveragePaymentDays) AveragePaymentDays
                                From (" & mQry & ") as V
                                Group by V.Subcode,(Case When V.RecordType='Opening' Then Null Else V.DocID End)
    
                                "
                End If

                DsRep = AgL.FillData(mQry, AgL.GCn)

                Dim DsAgeing As New DataSet

                DsAgeing = FillFifoOutstanding(CreateCondStr)
                RepName = "PartyInterestLedger.rpt"
                RepTitle = "Party Interest Ledger"

                If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records to Print!")
                Dim mCrd As New ReportDocument
                Dim mRepView As New AgLibrary.RepView(AgL)

                AgPL.CreateFieldDefFile1(DsRep, AgL.PubReportPath & "\" & RepName & ".ttx", True)
                AgPL.CreateFieldDefFile1(DsAgeing, AgL.PubReportPath & "\" & RepName & "Ageing" & ".ttx", True)

                mCrd.Load(AgL.PubReportPath & "\" & RepName)
                mCrd.SetDataSource(DsRep.Tables(0))

                mCrd.OpenSubreport("Ageing").Database.Tables(0).SetDataSource(DsAgeing.Tables(0))


                CType(mRepView.Controls("CrvReport"), CrystalDecisions.Windows.Forms.CrystalReportViewer).ReportSource = mCrd
                ClsMain.Formula_Set(mCrd, ReportFrm, RepTitle)
                mRepView.Text = ReportFrm.Text
                mRepView.MdiParent = ReportFrm.MdiParent
                mRepView.Show()


            Else

                If AgL.PubServerName = "" Then
                    If ClsMain.FDivisionNameForCustomization(20) = "SHYAMA SHYAM FABRICS" Or ClsMain.FDivisionNameForCustomization(22) = "W SHYAMA SHYAM FABRICS" Then
                        sQryPurchaseBrand = "(Select group_concat(Brand ,',') || ',' From (Select IfNull(sGroup.Description, sItem.Description)  as Brand  from PurchInvoiceDetail sPID  With (NoLock) Left Join Item sItem On sPID.Item = sItem.Code Left Join Item sGroup On sItem.ItemGroup = sGroup.Code Where sPID.DocID = PI.DocID And sItem.ItemType = '" & ItemTypeCode.TradingProduct & "' Group By IfNull(sGroup.Description, sItem.Description)))"
                        sQrySaleBrand = "(Select group_concat(Brand ,',') || ',' From (Select IfNull(sGroup.Description, sItem.Description)  as Brand  from SaleInvoiceDetail sSID  With (NoLock) Left Join Item sItem On sSID.Item = sItem.Code Left Join Item sGroup On sItem.ItemGroup = sGroup.Code Where sSID.DocID = SI.DocID And sItem.ItemType = '" & ItemTypeCode.TradingProduct & "' Group By IfNull(sGroup.Description, sItem.Description)))"
                    Else
                        sQryPurchaseBrand = "(Select group_concat(Brand ,',') || ',' From (Select sGroup.Description  as Brand  from PurchInvoiceDetailSku sPID  With (NoLock) Left Join Item sGroup On sPID.ItemGroup = sGroup.Code Where sPID.DocID = PI.DocID And sGroup.ItemType = '" & ItemTypeCode.TradingProduct & "' Group By sGroup.Description))"
                        sQrySaleBrand = "(Select group_concat(Brand ,',') || ',' From (Select sGroup.Description  as Brand  from SaleInvoiceDetailSku sSID  With (NoLock) Left Join Item sGroup On sSID.ItemGroup = sGroup.Code Where sSID.DocID = SI.DocID And sGroup.ItemType = '" & ItemTypeCode.TradingProduct & "' Group By sGroup.Description))"
                    End If
                Else
                    sQryPurchaseBrand = "(Select  IfNull(sGroup.Description, sItem.Description)  +  ','  from PurchInvoiceDetail sPID  With (NoLock) Left Join Item sItem On sPID.Item = sItem.Code Left Join Item sGroup On sItem.ItemGroup = sGroup.Code Where sPID.DocID = PI.DocID Group By IfNull(sGroup.Description, sItem.Description) for xml path(''))"
                    sQrySaleBrand = "(Select  IfNull(sGroup.Description, sItem.Description) + ','  from SaleInvoiceDetail sSID  With (NoLock) Left Join Item sItem On sSID.Item = sItem.Code Left Join Item sGroup On sItem.ItemGroup = sGroup.Code Where sSID.DocID = SI.DocID Group By IfNull(sGroup.Description, sItem.Description)  for xml path(''))"
                End If

                mQry = " SELECT H.DocID, H.V_Type, H.RecID, H.V_Date, H.Site_Code, H.Div_Code, H.Subcode, H.LinkedSubcode,
                        H.GoodsReturn * " & mMultiplier & " as GoodsReturn, 
                        H.Adjustment * " & mMultiplier & " as Adjustment, 
                        H.Payment * " & mMultiplier & "  as Payment, 
                        H.Balance * " & mMultiplier & "  as Balance, 
                        H.AmtDr * " & mMultiplier & "  as AmtDr, 
                        H.AmtCr * " & mMultiplier & "  As AmtCr, 
                        (Case When IfNull(GenSI.SaletoPartyName,'') <> '' Then IfNull(GenSI.SaletoPartyName,'') || '. ' Else '' End) || (Case When IfNull(ShipParty.Name,'') <> '' then ShipParty.Name || '. ' Else '' End) || IfNull(H.Narration,'') as Narration,
                        H.AdjDocID, H.DueDate, H.AdjDate, 
                        H.AdjAmount * " & mMultiplier & "  as AdjAmount, 
                        H.InterestBalance * " & mMultiplier & "  as InterestBalance,
                    Sg.name || (Case When IfNull(Ct.CityName,'') <> '' Then ', ' || IfNull(Ct.CityName,'') else '' End) as PartyName, CT.CityName as PartyCity, 
                    LSg.Name as LinkedPartyName,
                    (Case When VT.NCat = '" & Ncat.SaleInvoice & "' Then SIT.LrNo || (Case When SIT.NoOfBales Is Not Null Then ' * ' ||  SIT.NoOfBales Else '' End) When VT.NCat = '" & Ncat.PurchaseInvoice & "' then PIT.LrNo || (Case When PIT.NoOfBales Is Not Null Then ' * ' ||  PIT.NoOfBales Else '' End) Else Null End) as LrNo, 
                    (Case When VT.NCat In ('" & Ncat.SaleInvoice & "', '" & Ncat.SaleReturn & "') Then " & sQrySaleBrand & " When VT.NCat In ('" & Ncat.PurchaseInvoice & "','" & Ncat.PurchaseReturn & "') then " & sQryPurchaseBrand & " Else Null End) as Brand, 
                    (Case When VT.NCat = '" & Ncat.SaleInvoice & "' Then SI.AmsDocNo When VT.NCat = '" & Ncat.PurchaseInvoice & "' then PI.AmsDocNo Else Null End) AmsInvNo, 
                    (Case When VT.NCat = '" & Ncat.SaleInvoice & "' Then SI.AmsDocNetAmount When VT.NCat = '" & Ncat.PurchaseInvoice & "' then PI.AmsDocNetAmount Else 0.00 End) * " & mMultiplier & " AmsInvAmt, 
                    (Case When VT.NCat = '" & Ncat.SaleInvoice & "' Then SI.Gross_Amount + IfNull(SIL1.TotalDiscount,0) + IfNull(SIL1.TotalAdditionalDiscount,0) - IfNull(SIL1.TotalAddition,0) When VT.NCat = '" & Ncat.PurchaseInvoice & "' then PI.Gross_Amount + IfNull(PIL1.TotalDiscount,0) + IfNull(PIL1.TotalAdditionalDiscount,0) - IfNull(PIL1.TotalAddition,0) Else 0.0 End) * " & mMultiplier & " GoodsValue, 
                    (Case When VT.NCat = '" & Ncat.SaleInvoice & "' Then IfNull(SIL1.TotalDiscount,0) + IfNull(SIL1.TotalAdditionalDiscount,0) - IfNull(SIL1.TotalAddition,0) When VT.NCat = '" & Ncat.PurchaseInvoice & "' then IfNull(PIL1.TotalDiscount,0) + IfNull(PIL1.TotalAdditionalDiscount,0) - IfNull(PIL1.TotalAddition,0) Else 0.0 End) * " & mMultiplier & " Discount, 
                    (Case When VT.NCat = '" & Ncat.SaleInvoice & "' Then SI.Taxable_Amount When VT.NCat = '" & Ncat.PurchaseInvoice & "' then PI.Taxable_Amount Else 0.0 End) * " & mMultiplier & " TaxableAmt, 
                    (Case When VT.NCat = '" & Ncat.SaleInvoice & "' Then SI.Tax1 + SI.Tax2 + SI.Tax3 + SI.Tax4 + SI.Tax5  When VT.NCat = '" & Ncat.PurchaseInvoice & "' then PI.Tax1 + PI.Tax2 + PI.Tax3 + PI.Tax4 + PI.Tax5 Else 0.0 End) * " & mMultiplier & " TaxAmt,                             
                    (Case When VT.NCat = '" & Ncat.SaleInvoice & "' Then SI.Other_Charge + SI.Other_Charge1 + SI.Other_Charge2 When VT.NCat = '" & Ncat.PurchaseInvoice & "' then  PI.Other_Charge + PI.Other_Charge1 + PI.Other_Charge2 Else 0.0 End) * " & mMultiplier & " OtherChgAmt,                             
                    (Case When VT.NCat = '" & Ncat.SaleInvoice & "' Then SI.Deduction When VT.NCat = '" & Ncat.PurchaseInvoice & "' then PI.Deduction Else 0.0 End)  * " & mMultiplier & " Deduction,                             
                    (Case When VT.NCat = '" & Ncat.SaleInvoice & "' Then SI.Net_Amount When VT.NCat = '" & Ncat.PurchaseInvoice & "' then PI.Net_Amount When VT.NCat = '" & Ncat.OpeningBalance & "' Then (Case When Sg.Nature='Customer' Then H.AmtDr Else H.AmtCr End) Else 0.0 End) * " & mMultiplier & " BillAmt,                         
                    (Case When VT.NCat In ('" & Ncat.PurchaseInvoice & "','" & Ncat.PurchaseReturn & "') then PI.Commission + PI.AdditionalCommission Else 0.0 End)  * " & mMultiplier & " Commission,
                    ((Case When VT.NCat = '" & Ncat.PurchaseInvoice & "' then PI.Net_Amount When VT.NCat = '" & Ncat.OpeningBalance & "' Then (Case When Sg.Nature='Customer' Then 0 Else H.AmtCr End) Else 0.0 End) - (Case When VT.NCat = '" & Ncat.PurchaseInvoice & "' then PI.Commission + PI.AdditionalCommission Else 0.0 End)) * " & mMultiplier & " as NetPurAmt,
                    Div.Div_Name, Site.Name as Site_Name, Ints.LeaverageDays, Sg.AveragePaymentDays
                    From #TempRecord H  "
                If ReportFrm.FGetText(rowGroupOn) = "Linked Party" Then
                    mQry = mQry & " Left Join SubGroup SG On SG.Subcode = IfNull(H.LinkedSubcode,H.SubCode)   "
                Else
                    mQry = mQry & " Left Join SubGroup SG On SG.Subcode =H.SubCode   "
                End If

                mQry = mQry & "
                    LEFT JOIN City CT On SG.CityCode  =CT.CityCode 
                    Left Join SaleInvoice SI On H.DocID = SI.DocId
                    Left Join (Select SIL.DocID, Sum(SIL.DiscountAmount) as TotalDiscount, 
                                Sum(SIL.AdditionalDiscountAmount) as TotalAdditionalDiscount, 
                                Sum(SIL.AdditionAmount) as TotalAddition
                                From SaleInvoiceDetail SIL
                                Group By SIL.DocID) as SIL1 On H.DocID = SIL1.DocId  
                    Left Join SaleInvoiceTransport SIT On SI.DocID = SIT.DocID
                    Left Join Subgroup LSG On H.LinkedSubcode = LSG.Subcode
                    Left Join Subgroup ShipParty On SI.ShipToParty = ShipParty.Subcode
                    Left Join PurchInvoice PI On H.DocID = PI.DocId
                    Left Join (Select PIL.DocID, Sum(PIL.DiscountAmount) as TotalDiscount, 
                                Sum(PIL.AdditionalDiscountAmount) as TotalAdditionalDiscount, 
                                Sum(PIL.AdditionAmount) as TotalAddition
                                From PurchInvoiceDetail PIL
                                Group By PIL.DocID) as PIL1 On H.DocID = PIL1.DocId  
                    Left Join PurchInvoiceTransport PIT On PI.DocID = PIT.DocID
                    Left Join SaleInvoice GenSI On PI.GenDocId = GenSI.DocId
                    Left Join Voucher_Type Vt on H.V_type = Vt.V_type
                    Left Join Division Div On H.Div_Code = Div.Div_Code 
                    Left Join SiteMast Site On H.Site_Code = Site.Code 
                    Left Join InterestSlab IntS On Sg.InterestSlab = Ints.Code "


                DsRep = AgL.FillData(mQry, AgL.GCn)


                mQry = "Select L.DocId, L.Item, I.Description as ItemDescription, L.Qty, L.DealQty, L.Unit, L.Rate, L.Amount, L.AdditionAmount
                        From SaleInvoice H
                        Left Join SaleInvoiceDetail L On H.DocID = L.DocId
                        Left Join Subgroup Sg On H.SaleToParty = Sg.Subcode
                        Left Join Item I On L.Item  = I.Code
                        Where 1=1 "
                mQry = mQry & ReportFrm.GetWhereCondition("Sg.Subcode", rowParty)
                mQry = mQry & ReportFrm.GetWhereCondition("Sg.Parent", rowMasterParty)
                mQry = mQry & ReportFrm.GetWhereCondition("Sg.CityCode", rowCity)
                mQry = mQry & ReportFrm.GetWhereCondition("Sg.Area", rowArea)
                Dim DsTrnDetail As DataSet = AgL.FillData(mQry, AgL.GCn)

                If mPartyNature.ToUpper = "Customer".ToUpper Then
                    RepName = "CustomerLedgerKirana.rpt"
                    RepTitle = "Customer Ledger"
                Else
                    RepName = "SupplierLedgerKirana.rpt"
                    RepTitle = "Supplier Ledger"
                End If

                AgPL.CreateFieldDefFile1(DsRep, AgL.PubReportPath & "\" & RepName & ".ttx", True)
                AgPL.CreateFieldDefFile1(DsTrnDetail, AgL.PubReportPath & "\" & RepName & "TrnDetail" & ".ttx", True)


                If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records to Print!")
                Dim mCrd As New ReportDocument
                Dim mRepView As New AgLibrary.RepView(AgL)
                mCrd.Load(AgL.PubReportPath & "\" & RepName)
                mCrd.SetDataSource(DsRep.Tables(0))
                mCrd.OpenSubreport("TrnDetail").Database.Tables(0).SetDataSource(DsTrnDetail.Tables(0))

                CType(mRepView.Controls("CrvReport"), CrystalDecisions.Windows.Forms.CrystalReportViewer).ReportSource = mCrd
                ClsMain.Formula_Set(mCrd, ReportFrm, RepTitle)
                mRepView.Text = ReportFrm.Text
                mRepView.MdiParent = ReportFrm.MdiParent
                mRepView.Show()

            End If





            'Dim objReportLayout As New Aglibrary.FrmReportLayout("", "", "", "")
            'objReportLayout.PrintReport(DsRep, RepName, RepTitle)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub



    Public Sub Ini_Grid()

        Dim mDefaultValue As String = ""
        Try

            Dim mQry As String
            Dim I As Integer = 0

            If GRepFormName.ToUpper = "SupplierLedger".ToUpper Then
                mPartyNature = "SUPPLIER"
            ElseIf GRepFormName.ToUpper = "CustomerLedger".ToUpper Then
                mPartyNature = "CUSTOMER"
            End If


            mQry = "Select 'Ledger' as Code, 'Ledger' as Name 
                    Union All
                    Select 'Interest Ledger' as Code, 'Interest Ledger' as Name "
            ReportFrm.CreateHelpGrid("Report Type", "Report Type", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.SingleSelection, mQry, "Party Wise Summary - Ageing",,,,, False)

            mQry = "Select 'Party' as Code, 'Party' as Name 
                    Union All
                    Select 'Linked Party' as Code, 'Linked Party' as Name"
            ReportFrm.CreateHelpGrid("Group On", "Group On", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.SingleSelection, mQry, "Party")

            ReportFrm.CreateHelpGrid("From Date", "From Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", "")
            ReportFrm.CreateHelpGrid("Bills Upto Date", "Bills Upto Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.PubLoginDate)
            ReportFrm.CreateHelpGrid("Payments Upto Date", "Payments Upto Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.PubLoginDate)
            mQry = "Select 'All' as Code, 'All' as Name 
                    Union All
                    Select 'After Concur' as Code, 'After Concur' as Name 
                    "
            ReportFrm.CreateHelpGrid("Records Type", "Records Type", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.SingleSelection, mQry, "All")
            ReportFrm.FGMain.Rows(rowRecordsType).Visible = False

            If GRepFormName.ToUpper = "SupplierLedger".ToUpper Then
                mQry = " Select 'o' As Tick,  Sg.Code, Sg.Name AS Party, Sg.Address, Ag.GroupName FROM viewHelpSubGroup Sg  Left Join AcGroup Ag On Sg.groupCode= ag.GroupCode Where Sg.Nature In ('Supplier','Cash') And Sg.SubgroupType Not In ('Master Customer','Master Supplier') "
                ReportFrm.CreateHelpGrid("Supplier", "Supplier", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mQry, , 450, 825, 300)
            ElseIf GRepFormName.ToUpper = "CustomerLedger".ToUpper Then
                mQry = " Select 'o' As Tick,  Sg.Code, Sg.Name AS Party, Sg.Address, Ag.GroupName FROM viewHelpSubGroup Sg  Left Join AcGroup Ag On Sg.groupCode= ag.GroupCode Where Sg.Nature In ('Customer','Cash') And Sg.SubgroupType Not In ('Master Customer','Master Supplier') "
                ReportFrm.CreateHelpGrid("Customer", "Customer", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mQry, , 450, 825, 300)
            Else
                mQry = " Select 'o' As Tick,  Sg.Code, Sg.Name AS Party, Sg.Address, Ag.GroupName FROM viewHelpSubGroup Sg  Left Join AcGroup Ag On Sg.groupCode= ag.GroupCode Where Sg.Nature In ('Customer','Supplier','Cash') And Sg.SubgroupType Not In ('Master Customer','Master Supplier') "
                ReportFrm.CreateHelpGrid("Party", "Party", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mQry, , 450, 825, 300)
            End If

            If GRepFormName.ToUpper = "SupplierLedger".ToUpper Then
                mQry = " Select 'o' As Tick,  Sg.Code, Sg.Name AS Party, Sg.Address, Ag.GroupName FROM viewHelpSubGroup Sg  Left Join AcGroup Ag On Sg.groupCode= ag.GroupCode Where Sg.Nature In ('Supplier') And Sg.Code In (Select Distinct Parent From SubGroup) Order By Name"
                ReportFrm.CreateHelpGrid("Master Supplier", "Master Supplier", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mQry, , 450, 825, 300)
            ElseIf GRepFormName.ToUpper = "CustomerLedger".ToUpper Then
                mQry = " Select 'o' As Tick,  Sg.Code, Sg.Name AS Party, Sg.Address, Ag.GroupName FROM viewHelpSubGroup Sg  Left Join AcGroup Ag On Sg.groupCode= ag.GroupCode Where Sg.Nature In ('Customer') And Sg.Code In (Select Distinct Parent From SubGroup) Order By Name"
                ReportFrm.CreateHelpGrid("Master Customer", "Master Customer", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mQry, , 450, 825, 300)
            Else
                mQry = " Select 'o' As Tick,  Sg.Code, Sg.Name AS Party, Sg.Address, Ag.GroupName FROM viewHelpSubGroup Sg  Left Join AcGroup Ag On Sg.groupCode= ag.GroupCode Where Sg.Nature In ('Customer','Supplier') And Sg.Code In (Select Distinct Parent From SubGroup) Order By Name"
                ReportFrm.CreateHelpGrid("Master Party", "Master Party", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mQry, , 450, 825, 300)
            End If
            If Not ClsMain.IsScopeOfWorkContains(IndustryType.CommonModules.MasterParty) Then ReportFrm.FGMain.Rows(rowMasterParty).Visible = False

            If GRepFormName.ToUpper = "SupplierLedger".ToUpper Then
                mQry = " Select 'o' As Tick,  Sg.Code, Sg.Name AS Party, Sg.Address, Ag.GroupName FROM viewHelpSubGroup Sg  Left Join AcGroup Ag On Sg.groupCode= ag.GroupCode Where Sg.SubgroupType In ('Master Supplier') Order By Name"
                ReportFrm.CreateHelpGrid("Linked Supplier", "Linked Supplier", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mQry, , 450, 825, 300)
            ElseIf GRepFormName.ToUpper = "CustomerLedger".ToUpper Then
                mQry = " Select 'o' As Tick,  Sg.Code, Sg.Name AS Party, Sg.Address, Ag.GroupName FROM viewHelpSubGroup Sg  Left Join AcGroup Ag On Sg.groupCode= ag.GroupCode Where Sg.SubgroupType In ('Master Customer') Order By Name"
                ReportFrm.CreateHelpGrid("Linked Customer", "Linked Customer", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mQry, , 450, 825, 300)
            Else
                mQry = " Select 'o' As Tick,  Sg.Code, Sg.Name AS Party, Sg.Address, Ag.GroupName FROM viewHelpSubGroup Sg  Left Join AcGroup Ag On Sg.groupCode= ag.GroupCode Where Sg.SubgroupType In ('Master Customer','Master Supplier') Order By Name"
                ReportFrm.CreateHelpGrid("Linked Party", "Linked Party", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mQry, , 450, 825, 300)
            End If
            If Not ClsMain.IsScopeOfWorkContains(IndustryType.CommonModules.LinkedParty) Then ReportFrm.FGMain.Rows(rowLinkedParty).Visible = False

            mQry = " Select 'o' As Tick,  Sg.Code, Sg.Name AS Agent FROM viewHelpSubgroup Sg Where Sg.SubgroupType = '" & SubgroupType.SalesAgent & "' "
            ReportFrm.CreateHelpGrid("Agent", "Agent", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mQry)

            mQry = "Select 'o' As Tick, CityCode, CityName From City "
            ReportFrm.CreateHelpGrid("City", "City", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mQry)

            mQry = "Select 'o' As Tick, Code, Description From Area "
            ReportFrm.CreateHelpGrid("Area", "Area", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mQry)

            mDefaultValue = ClsMain.FGetSettings(ClsMain.SettingFields.DefaultDivisionNameInReportFilters, SettingType.General, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
            If mDefaultValue = "All" Then
                mDefaultValue = "All"
            Else
                mDefaultValue = "[DIVISIONCODE]"
            End If
            mQry = "Select 'o' As Tick, Div_Code as Code, Div_Name As [Division] From Division "
            ReportFrm.CreateHelpGrid("Division", "Division", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mQry, mDefaultValue)


            mDefaultValue = ClsMain.FGetSettings(ClsMain.SettingFields.DefaultSiteNameInReportFilters, SettingType.General, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
            If mDefaultValue = "All" Then
                mDefaultValue = "All"
            Else
                mDefaultValue = "[SITECODE]"
            End If
            mQry = "Select 'o' As Tick, Code, Name As [Site] From SiteMast Where  Code In (" & AgL.PubSiteList & ")  "
            ReportFrm.CreateHelpGrid("Site", "Site", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mQry, mDefaultValue)


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub


    Private Function FGetVoucher_TypeQry(ByVal TableName As String) As String
        FGetVoucher_TypeQry = " SELECT Distinct 'o' As Tick, H.V_Type AS Code, Vt.Description AS [Voucher Type] " &
                                " FROM " & TableName & " H  " &
                                " LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type "
    End Function

    Private Sub ObjRepFormGlobal_ProcessReport() Handles ReportFrm.ProcessReport
        Select Case mGRepFormName
            Case CustomerLedger
                ProcFormattedPrint()
        End Select
    End Sub


    Public Sub New(ByVal mReportFrm As AgLibrary.FrmReportLayout)
        ReportFrm = mReportFrm
    End Sub








    Private Function FillFifoOutstanding(mCondstr As String) As DataSet
        Dim mRemainingBalance As Double
        Dim i As Integer, j As Integer
        Dim dtParty As DataTable

        Dim DtMain As DataTable
        Dim BalAmount As Double
        Dim DrCr As String
        Dim dtLedger As DataTable
        Dim dtPayments As DataTable
        Dim drInvoices As DataRow()
        Dim drPayments As DataRow()




        mQry = "Select Sg.Subcode, Max(Sg.Nature) as Nature, Sum(Lg.AmtDr)-Sum(Lg.AmtCr) as Balance
                            From Ledger Lg "
        If ReportFrm.FGetText(rowGroupOn) = "Linked Party" Then
            mQry = mQry & " Left Join Subgroup Sg On IfNull(Lg.LinkedSubcode,Lg.Subcode) = Sg.Subcode "
        Else
            mQry = mQry & " Left Join Subgroup Sg On Lg.Subcode = Sg.Subcode "
        End If
        mQry = mQry & " Left Join(Select SILTV.Subcode, Max(SILTV.Agent) As Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode) as LTV On Sg.Subcode = LTV.Subcode
                                    Where 1 = 1 "
        mQry = mQry & mCondstr
        mQry = mQry & " Group By Sg.Subcode"
        If ReportFrm.FGetText(rowReportType) = "Party Wise Balance - Ageing" Then
            mQry = mQry & " Having Sum(Lg.AmtDr)-Sum(Lg.AmtCr) <> 0 "
        End If

        dtParty = AgL.FillData(mQry, AgL.GCn).Tables(0)



        mQry = "Select Lg.DocID, Lg.DivCode, Lg.Site_Code, Lg.V_Type, Vt.Description as V_TypeDesc, 
                                    Lg.RecId, SG.Subcode, IfNull(Lg.EffectiveDate, Lg.V_Date) As V_Date, Lg.Narration, Lg.AmtDr + Lg.AmtCr as Amount,
                                    LG.AmtDr , LG.AmtCr                                
                                    From Ledger Lg  With (NoLock) "
        If ReportFrm.FGetText(rowGroupOn) = "Linked Party" Then
            mQry = mQry & " Left Join SubGroup SG On SG.Subcode =IfNull(Lg.LinkedSubcode,LG.SubCode) "
        Else
            mQry = mQry & " Left Join SubGroup SG On SG.Subcode =LG.SubCode "
        End If

        mQry = mQry & " Left Join(Select SILTV.Subcode, Max(SILTV.Agent) As Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode) as LTV On Sg.Subcode = LTV.Subcode
                                    Left Join Voucher_Type Vt On Lg.V_Type = Vt.V_Type
                                    Where 1=1 " & mCondstr & " 
                                    Order By IfNull(Lg.EffectiveDate, Lg.V_Date) Desc, Lg.RecId desc"

        dtLedger = AgL.FillData(mQry, AgL.GCn).Tables(0)








        If dtParty.Rows.Count > 0 Then
            For i = 0 To dtParty.Rows.Count - 1
                mQry = ""
                If AgL.XNull(dtParty.Rows(i)("Nature")) = "Customer" Then
                    If AgL.VNull(dtParty.Rows(i)("Balance")) > 0 Then
                        mQry = "Select Lg.DocID, Lg.DivCode, Lg.Site_Code, Lg.V_Type, Vt.Description as V_TypeDesc, 
                                    Lg.RecId, SG.Subcode, IfNull(Lg.EffectiveDate, Lg.V_Date) As V_Date, Lg.Narration, Lg.AmtDr as Amount                                
                                    From Ledger Lg  With (NoLock) "
                        If ReportFrm.FGetText(rowGroupOn) = "Linked Party" Then
                            mQry = mQry & " Left Join SubGroup SG On SG.Subcode =IfNull(LG.LinkedSubcode,LG.SubCode)  "
                        Else
                            mQry = mQry & " Left Join SubGroup SG On SG.Subcode =LG.SubCode  "
                        End If
                        mQry = mQry & " Left Join(Select SILTV.Subcode, Max(SILTV.Agent) As Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode) as LTV On Sg.Subcode = LTV.Subcode
                                    Left Join Voucher_Type Vt  With (NoLock) On Lg.V_Type = Vt.V_Type
                                    Where Sg.Subcode = '" & AgL.XNull(dtParty.Rows(i)("Subcode")) & "'  
                                    And Lg.AmtDr > 0  " & mCondstr & "                               
                                    Order By IfNull(Lg.EffectiveDate, Lg.V_Date) Desc, Lg.RecId desc"
                    Else
                        'If ReportFrm.FGetText(rowReportType) = "Party Wise Summary - Ageing" Then
                        mQry = "Insert Into #FifoOutstanding
                                                (DocID, V_Type, RecID, V_Date, 
                                                Site_Code, Div_Code, SubCode, BillAmount,
                                                BalanceAmount, DrCr, Narration)    
                                                Values(Null,
                                                Null,
                                                Null,
                                                Null,
                                                Null,
                                                Null,
                                                " & AgL.Chk_Text(AgL.XNull(dtParty.Rows(i)("Subcode"))) & ",
                                                0,
                                                " & Val(Math.Abs(AgL.VNull(dtParty.Rows(i)("Balance")))) & ",
                                                'Cr',
                                                Null
                                                )
                                                "
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
                        mQry = ""
                        'End If
                    End If
                Else
                    If AgL.VNull(dtParty.Rows(i)("Balance")) < 0 Then
                        mQry = "Select Lg.DocID, Lg.DivCode, Lg.Site_Code, Lg.V_Type, Vt.Description as V_TypeDesc, 
                                    Lg.RecId, SG.Subcode, IfNull(Lg.EffectiveDate, Lg.V_Date) As V_Date, Lg.Narration, Lg.AmtCr as Amount                                
                                    From Ledger Lg  With (NoLock) "
                        If ReportFrm.FGetText(rowGroupOn) = "Linked Party" Then
                            mQry = mQry & " Left Join SubGroup SG On SG.Subcode =IfNull(Lg.LinkedSubcode,LG.SubCode) "
                        Else
                            mQry = mQry & " Left Join SubGroup SG On SG.Subcode =LG.SubCode "
                        End If

                        mQry = mQry & " Left Join(Select SILTV.Subcode, Max(SILTV.Agent) As Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode) as LTV On Sg.Subcode = LTV.Subcode
                                    Left Join Voucher_Type Vt On Lg.V_Type = Vt.V_Type
                                    Where Sg.Subcode = '" & AgL.XNull(dtParty.Rows(i)("Subcode")) & "'  And Lg.AmtCr > 0 " & mCondstr & " 
                                    Order By IfNull(Lg.EffectiveDate, Lg.V_Date) Desc, Lg.RecId desc"
                    Else
                        If ReportFrm.FGetText(rowReportType) = "Party Wise Summary - Ageing" Then
                            mQry = "Insert Into #FifoOutstanding
                                                (DocID, V_Type, RecID, V_Date, 
                                                Site_Code, Div_Code, SubCode, BillAmount,
                                                BalanceAmount, DrCr, Narration)    
                                                Values(Null,
                                                Null,
                                                Null,
                                                Null,
                                                Null,
                                                Null,
                                                " & AgL.Chk_Text(AgL.XNull(dtParty.Rows(i)("Subcode"))) & ",
                                                0,
                                                " & Val(AgL.VNull(dtParty.Rows(i)("Balance"))) & ",
                                                'Dr',
                                                Null
                                                )
                                                "
                            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
                            mQry = ""
                        End If
                    End If
                End If


                BalAmount = 0 : DrCr = ""
                mRemainingBalance = Math.Abs(AgL.VNull(dtParty.Rows(i)("Balance")))
                If mQry <> "" Then
                    If AgL.XNull(dtParty.Rows(i)("Nature")) = "Customer" Then
                        drInvoices = dtLedger.Select(" Subcode = '" & AgL.XNull(dtParty.Rows(i)("Subcode")) & "'  And AmtDr > 0 ", " V_Date Desc ")
                    Else
                        drInvoices = dtLedger.Select(" Subcode = '" & AgL.XNull(dtParty.Rows(i)("Subcode")) & "'  And AmtCr > 0 ", " V_Date Desc ")
                    End If
                    'DtMain = AgL.FillData(mQry, AgL.GCn).Tables(0)
                    If drInvoices.Length > 0 Then
                        For j = 0 To drInvoices.Length - 1

                            If mRemainingBalance > 0 Then

                                If mRemainingBalance > AgL.VNull(drInvoices(j)("Amount")) Then
                                    BalAmount = Format(AgL.VNull(drInvoices(j)("Amount")), "0.00")
                                    mRemainingBalance = mRemainingBalance - AgL.VNull(drInvoices(j)("Amount"))
                                Else
                                    BalAmount = Format(mRemainingBalance, "0.00")
                                    mRemainingBalance = mRemainingBalance - mRemainingBalance
                                End If
                                DrCr = IIf(AgL.VNull(dtParty.Rows(i)("Balance")) > 0, "Dr", "Cr")


                                mQry = "Insert Into #FifoOutstanding
                                                (DocID, V_Type, RecID, V_Date, 
                                                Site_Code, Div_Code, SubCode, BillAmount,
                                                BalanceAmount, DrCr, Narration)    
                                                Values(" & AgL.Chk_Text(AgL.XNull(drInvoices(j)("DocID"))) & ",
                                                " & AgL.Chk_Text(AgL.XNull(drInvoices(j)("V_Type"))) & ",
                                                " & AgL.Chk_Text(AgL.XNull(drInvoices(j)("RecID"))) & ",
                                                " & AgL.Chk_Date(AgL.XNull(drInvoices(j)("V_Date"))) & ",                                            
                                                " & AgL.Chk_Text(AgL.XNull(drInvoices(j)("Site_Code"))) & ",
                                                " & AgL.Chk_Text(AgL.XNull(drInvoices(j)("DivCode"))) & ",
                                                " & AgL.Chk_Text(AgL.XNull(drInvoices(j)("Subcode"))) & ",
                                                " & AgL.Chk_Text(AgL.XNull(drInvoices(j)("Amount"))) & ",
                                                " & BalAmount & ",
                                                " & AgL.Chk_Text(DrCr) & ",
                                                " & AgL.Chk_Text(AgL.XNull(drInvoices(j)("Narration"))) & "
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
                            Null Site_Code, Null Div_Code, Null As LrNo, H.Subcode, Sg.name || (Case When IfNull(City.CityName,'') <> '' Then ', ' || IfNull(City.CityName,'') else '' End) as PartyName,
                    City.CityName, Null As Narration, 0 As TaxableAmount, 0 As TaxAmount,
                            0 as Addition, 0 as BillAmount, 0 as GoodsReturn, 0 as Payment, Sum(Case When H.DrCr='Dr' Then H.BalanceAmount Else -H.BalanceAmount End) as Adjustment, 
                            0 as Balance,Sum(Case When H.DrCr='Dr' Then H.BalanceAmount Else 0 End) as AmtDr, Sum(Case When H.DrCr='Cr' Then H.BalanceAmount Else 0 End) as AmtCr
                            From #FifoOutstanding H
                            Left Join Subgroup Sg on H.Subcode = Sg.Subcode
                            Left Join City On Sg.CityCode = City.CityCode
                            Group By H.Subcode, strftime('%m-%Y', H.V_Date)
                            Order By strftime('%Y', H.V_Date), strftime('%m', H.V_Date)
                            "
        DtMain = AgL.FillData(mQry, AgL.GCn).Tables(0)

        Dim CurrentMonth As Date = CDate(AgL.PubLoginDate)
        Dim OneMonthBack As Date = DateAdd(DateInterval.Month, -1, CDate(AgL.PubLoginDate))
        Dim TwoMonthBack As Date = DateAdd(DateInterval.Month, -2, CDate(AgL.PubLoginDate))
        Dim ThreeMonthBack As Date = DateAdd(DateInterval.Month, -3, CDate(AgL.PubLoginDate))
        Dim FourMonthBack As Date = DateAdd(DateInterval.Month, -4, CDate(AgL.PubLoginDate))
        Dim FiveMonthBack As Date = DateAdd(DateInterval.Month, -5, CDate(AgL.PubLoginDate))
        Dim SixMonthBack As Date = DateAdd(DateInterval.Month, -6, CDate(AgL.PubLoginDate))
        Dim SevenMonthBack As Date = DateAdd(DateInterval.Month, -7, CDate(AgL.PubLoginDate))
        Dim EightMonthBack As Date = DateAdd(DateInterval.Month, -8, CDate(AgL.PubLoginDate))
        Dim NineMonthBack As Date = DateAdd(DateInterval.Month, -9, CDate(AgL.PubLoginDate))


        Dim mMultiplier As Double
        If ClsMain.FDivisionNameForCustomization(22) = "W SHYAMA SHYAM FABRICS" Then
            mMultiplier = 0.01
        Else
            mMultiplier = 1.0
        End If

        mQry = "Select H.Subcode as SearchCode, 1 as Sr, Sg.name || (Case When IfNull(City.CityName,'') <> '' Then ', ' || IfNull(City.CityName,'') else '' End) as Party,                            
                            Sum(H.BalanceAmount) * " & mMultiplier & " as BalanceAmount, 

                            (CASE WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(CurrentMonth) & ") Then " & AgL.Chk_Date(CurrentMonth) & "
                                WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(OneMonthBack) & ") Then " & AgL.Chk_Date(OneMonthBack) & "
                                WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(TwoMonthBack) & ") Then " & AgL.Chk_Date(TwoMonthBack) & "
                                WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(ThreeMonthBack) & ") Then " & AgL.Chk_Date(ThreeMonthBack) & "
                                WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(FourMonthBack) & ") Then " & AgL.Chk_Date(FourMonthBack) & "
                                WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(FiveMonthBack) & ") Then " & AgL.Chk_Date(FiveMonthBack) & "
                                WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(SixMonthBack) & ") Then " & AgL.Chk_Date(SixMonthBack) & "
                                WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(SevenMonthBack) & ") Then " & AgL.Chk_Date(SevenMonthBack) & "
                                WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(EightMonthBack) & ") Then " & AgL.Chk_Date(EightMonthBack) & "
                                WHEN strftime('%Y%m', H.V_Date) < strftime('%Y%m', " & AgL.Chk_Date(EightMonthBack) & ") Then " & AgL.Chk_Date(NineMonthBack) & "
                             Else Null End) as BalanceMonth   
                            From #FifoOutstanding H
                            Left Join Subgroup Sg on H.Subcode = Sg.Subcode
                            Left Join City On Sg.CityCode = City.CityCode
                            Group By H.Subcode, Sg.name || (Case When IfNull(City.CityName,'') <> '' Then ', ' || IfNull(City.CityName,'') else '' End),
                            (CASE WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(CurrentMonth) & ") Then " & AgL.Chk_Date(CurrentMonth) & "
                                WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(OneMonthBack) & ") Then " & AgL.Chk_Date(OneMonthBack) & "
                                WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(TwoMonthBack) & ") Then " & AgL.Chk_Date(TwoMonthBack) & "
                                WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(ThreeMonthBack) & ") Then " & AgL.Chk_Date(ThreeMonthBack) & "
                                WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(FourMonthBack) & ") Then " & AgL.Chk_Date(FourMonthBack) & "
                                WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(FiveMonthBack) & ") Then " & AgL.Chk_Date(FiveMonthBack) & "
                                WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(SixMonthBack) & ") Then " & AgL.Chk_Date(SixMonthBack) & "
                                WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(SevenMonthBack) & ") Then " & AgL.Chk_Date(SevenMonthBack) & "
                                WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(EightMonthBack) & ") Then " & AgL.Chk_Date(EightMonthBack) & "
                                WHEN strftime('%Y%m', H.V_Date) < strftime('%Y%m', " & AgL.Chk_Date(EightMonthBack) & ") Then " & AgL.Chk_Date(NineMonthBack) & "
                             Else Null End)
                            Order By Sg.name || (Case When IfNull(City.CityName,'') <> '' Then ', ' || IfNull(City.CityName,'') else '' End), H.V_Date
                    "
        FillFifoOutstanding = AgL.FillData(mQry, AgL.GCn)
    End Function



    Private Sub GetDataReadyForFIFOBalance(mCondStr As String)

        Dim mFromDate As String
        If ReportFrm.FGetText(rowReportType) = "Interest Ledger" Then
            mFromDate = ""
        Else
            mFromDate = ReportFrm.FGetText(rowFromDate)
        End If


        If mFromDate <> "" Then


            Dim mRemainingBalance As Double
            Dim i As Integer, j As Integer
            Dim dtParty As DataTable

            Dim DtMain As DataTable
            Dim BalAmount As Double
            Dim DrCr As String



            mQry = "Select Sg.Subcode , Max(Sg.Nature) as Nature, Sum(Lg.AmtDr)-Sum(Lg.AmtCr) as Balance
                            From Ledger Lg "
            If ReportFrm.FGetText(rowGroupOn) = "Linked Party" Then
                mQry = mQry & " Left Join Subgroup Sg On IfNull(Lg.LinkedSubcode,Lg.Subcode) = Sg.Subcode "
            Else
                mQry = mQry & " Left Join Subgroup Sg On Lg.Subcode = Sg.Subcode "
            End If
            mQry = mQry & " Left Join(Select SILTV.Subcode, Max(SILTV.Agent) As Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode) as LTV On Sg.Subcode = LTV.Subcode
                            Where 1 = 1 "
            mQry = mQry & mCondStr & " And Date(Lg.V_Date) < " & AgL.Chk_Date(mFromDate) & " "
            mQry = mQry & " Group By Sg.Subcode"

            dtParty = AgL.FillData(mQry, AgL.GCn).Tables(0)

            If dtParty.Rows.Count > 0 Then
                For i = 0 To dtParty.Rows.Count - 1
                    mQry = ""
                    If AgL.XNull(dtParty.Rows(i)("Nature")) = "Customer" Then
                        If AgL.VNull(dtParty.Rows(i)("Balance")) > 0 Then
                            mQry = "Select Lg.DocID, Lg.DivCode, Lg.Site_Code, Lg.V_Type, Vt.Description as V_TypeDesc, 
                                    Lg.RecId, SG.Subcode, IfNull(Lg.EffectiveDate, Lg.V_Date) As V_Date, Lg.Narration, Lg.AmtDr as Amount                                
                                    From Ledger Lg  With (NoLock) "
                            If ReportFrm.FGetText(rowGroupOn) = "Linked Party" Then
                                mQry = mQry & " Left Join SubGroup SG On SG.Subcode = IfNull(LG.LinkedSubcode,LG.SubCode)   "
                            Else
                                mQry = mQry & " Left Join SubGroup SG On SG.Subcode =LG.SubCode   "
                            End If
                            mQry = mQry & " Left Join(Select SILTV.Subcode, Max(SILTV.Agent) As Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode) as LTV On Sg.Subcode = LTV.Subcode
                                    Left Join Voucher_Type Vt  With (NoLock) On Lg.V_Type = Vt.V_Type
                                    Where Date(Lg.V_Date) < " & AgL.Chk_Date(mFromDate) & " And Sg.Subcode = '" & AgL.XNull(dtParty.Rows(i)("Subcode")) & "'  And Lg.AmtDr > 0  " & mCondStr & "                               
                                    Order By IfNull(Lg.EffectiveDate, Lg.V_Date) Desc, Lg.RecId desc"
                        End If
                    Else
                        If AgL.VNull(dtParty.Rows(i)("Balance")) < 0 Then
                            mQry = "Select Lg.DocID, Lg.DivCode, Lg.Site_Code, Lg.V_Type, Vt.Description as V_TypeDesc, 
                                    Lg.RecId, LG.Subcode, IfNull(Lg.EffectiveDate, Lg.V_Date) As V_Date, Lg.Narration, Lg.AmtCr as Amount                                
                                    From Ledger Lg  With (NoLock) "
                            If ReportFrm.FGetText(rowGroupOn) = "Linked Party" Then
                                mQry = mQry & " Left Join SubGroup SG On SG.Subcode = IfNull(LG.LinkedSubcode,LG.SubCode)   "
                            Else
                                mQry = mQry & " Left Join SubGroup SG On SG.Subcode =LG.SubCode   "
                            End If
                            mQry = mQry & " Left Join(Select SILTV.Subcode, Max(SILTV.Agent) As Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode) as LTV On Sg.Subcode = LTV.Subcode
                                    Left Join Voucher_Type Vt On Lg.V_Type = Vt.V_Type
                                    Where  Date(Lg.V_Date) < " & AgL.Chk_Date(mFromDate) & " And Lg.Subcode = '" & AgL.XNull(dtParty.Rows(i)("Subcode")) & "'  And Lg.AmtCr > 0 " & mCondStr & " 
                                    Order By IfNull(Lg.EffectiveDate, Lg.V_Date) Desc, Lg.RecId desc"
                        End If
                    End If


                    BalAmount = 0 : DrCr = ""
                    mRemainingBalance = Math.Abs(AgL.VNull(dtParty.Rows(i)("Balance")))
                    If mQry <> "" Then
                        DtMain = AgL.FillData(mQry, AgL.GCn).Tables(0)
                        If DtMain.Rows.Count > 0 Then
                            For j = 0 To DtMain.Rows.Count - 1

                                If mRemainingBalance > 0 Then

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
                                                Site_Code, Div_Code, SubCode, BillAmount,
                                                BalanceAmount, DrCr, Narration)    
                                                Values(" & AgL.Chk_Text(AgL.XNull(DtMain.Rows(j)("DocID"))) & ",
                                                " & AgL.Chk_Text(AgL.XNull(DtMain.Rows(j)("V_Type"))) & ",
                                                " & AgL.Chk_Text(AgL.XNull(DtMain.Rows(j)("RecID"))) & ",
                                                " & AgL.Chk_Date(AgL.XNull(DtMain.Rows(j)("V_Date"))) & ",                                            
                                                " & AgL.Chk_Text(AgL.XNull(DtMain.Rows(j)("Site_Code"))) & ",
                                                " & AgL.Chk_Text(AgL.XNull(DtMain.Rows(j)("DivCode"))) & ",
                                                " & AgL.Chk_Text(AgL.XNull(DtMain.Rows(j)("Subcode"))) & ",
                                                " & AgL.VNull(AgL.XNull(DtMain.Rows(j)("Amount"))) & ",
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
                            Null Site_Code, Null Div_Code, Null As LrNo, H.Subcode, Sg.name || (Case When IfNull(City.CityName,'') <> '' Then ', ' || IfNull(City.CityName,'') else '' End) as PartyName,
                    City.CityName, Null As Narration, 0 As TaxableAmount, 0 As TaxAmount,
                            0 as Addition, 0 as BillAmount, 0 as GoodsReturn, 0 as Payment, Sum(Case When H.DrCr='Dr' Then H.BalanceAmount Else -H.BalanceAmount End) as Adjustment, 
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
                            Site_code, Div_Code, SubCode, Narration, GoodsReturn, Payment,
                            Adjustment, Balance, AmtDr, AmtCr) 
                            Select Null As DocID, Null As V_Type, strftime('%m-%Y', H.V_Date) as RecID, Null as V_Date, 
                            Null Site_Code, Null Div_Code, H.Subcode, Null As Narration,  0 as GoodsReturn, 0 as Payment, Sum(Case When H.DrCr='Dr' Then H.BalanceAmount Else -H.BalanceAmount End) as Adjustment, 
                            0 as Balance, Sum(Case When H.DrCr='Dr' Then H.BalanceAmount Else 0 End) as AmtDr, Sum(Case When H.DrCr='Cr' Then H.BalanceAmount Else 0 End) as AmtCr
                            From #FifoOutstanding H
                            Left Join Subgroup Sg on H.Subcode = Sg.Subcode
                            Left Join City On Sg.CityCode = City.CityCode
                            Group By H.Subcode, strftime('%m-%Y', H.V_Date)
                            Order By strftime('%Y', H.V_Date), strftime('%m', H.V_Date)
                           "

            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
            mQry = "Select * from #TempRecord "
            DtMain = AgL.FillData(mQry, AgL.GCn).Tables(0)
        End If

    End Sub




End Class
