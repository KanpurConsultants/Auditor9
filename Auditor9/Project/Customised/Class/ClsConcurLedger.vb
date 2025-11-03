Imports System.Data.SqlClient
Imports System.Data.SQLite
Imports System.IO
Imports AgLibrary.ClsMain
Imports AgLibrary.ClsMain.agConstants

Public Class ClsConcurLedger

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


    Private Const CustomerLedger As String = "ChuktiLedger"



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


    Public Sub Ini_Grid()
        Try
            Dim mQry As String
            Dim I As Integer = 0
            Select Case GRepFormName
                Case CustomerLedger
                    mQry = "Select 'Format 1' as Code, 'Format 1' as Name 
                            Union All Select 'Format 2' as Code, 'Format 2' as Name 
                            Union All Select 'Without Interest Portrait' as Code, 'Without Interest Portrait' as Name "
                    ReportFrm.CreateHelpGrid("Report Format", "Report Format", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.SingleSelection, mQry, "Format 1")
                    ReportFrm.CreateHelpGrid("As On Date", "As On Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.PubLoginDate)
                    ReportFrm.CreateHelpGrid("Grace Days", "Grace Days", AgLibrary.FrmReportLayout.FieldFilterDataType.NumericType, AgLibrary.FrmReportLayout.FieldDataType.NumericType, "", AgL.VNull(AgL.PubDtEnviro.Rows(0)("Default_DebtorsCreditDays")))
                    ReportFrm.CreateHelpGrid("Party", "Party", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpSubgroupQry, , 450, 825, 300)
                    mQry = "Select 'After Chukti' as Code, 'After Chukti' as Name 
                            Union All Select 'Financial Year' as Code, 'Financial Year' as Name 
                            Union All Select 'Financial Year Opening' as Code, 'Financial Year Opening' as Name 
                            Union All Select 'Complete' as Code, 'Complete' as Name"
                    ReportFrm.CreateHelpGrid("Records Type", "Records Type", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.SingleSelection, mQry, "After Chukti")
                    ReportFrm.CreateHelpGrid("Agent", "Agent", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpAgentQry)
                    ReportFrm.CreateHelpGrid("City", "City", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpCityQry)
                    ReportFrm.CreateHelpGrid("Area", "Area", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpAreaQry)
                    ReportFrm.CreateHelpGrid("Division", "Division", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpDivisionQry, "[DIVISIONCODE]")
                    ReportFrm.CreateHelpGrid("Site", "Site", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpSiteQry, "[SITECODE]")
                    ReportFrm.CreateHelpGrid("Interest Rate", "Interest Rate", AgLibrary.FrmReportLayout.FieldFilterDataType.NumericType, AgLibrary.FrmReportLayout.FieldDataType.NumericType, "", AgL.VNull(AgL.PubDtEnviro.Rows(0)("Default_DebtorsInterestRate")))
                    ReportFrm.CreateHelpGrid("Account Group", "Account Group", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpAcGroupQry)

            End Select
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
                ProcConcurLedger()

        End Select
    End Sub


    Public Sub New(ByVal mReportFrm As AgLibrary.FrmReportLayout)
        ReportFrm = mReportFrm
    End Sub

    Public Function FunConcurLedger(Conn As Object) As DataSet
        Dim mCondStr$ = ""
        Dim mCondStrOp$ = ""
        Dim NoofDays As Integer = 0
        Dim DtSubcode As DataTable
        Dim iSubcode As Integer
        Dim DtDivision As DataTable
        Dim iDivision As Integer
        Dim DtDr As DataTable
        Dim DtCr As DataTable
        Dim DtTemp As DataTable
        Dim DrRecordCount As Integer
        Dim CrRecordCount As Integer
        Dim LoopLimit As Integer
        Dim I As Integer, J As Integer
        Dim iDr As Integer
        Dim iCr As Integer
        Dim DrSr As Integer
        Dim CrSr As Integer
        Dim ConcurSr As Integer = -1
        Dim FirstConcurSr As Integer = -1
        Dim mSubcode As String
        Dim mDivision As String
        Dim mLastChuktiAmount As Double
        Dim mTotalDr As Double
        Dim mTotalCr As Double
        Dim DtSubcodeBalances As DataTable


        Try

            If AgL.XNull(ReportFrm.FGetText(0)).ToString.ToUpper = "Format 2".ToUpper Then
                If ClsMain.FDivisionNameForCustomization(4) = "X DEVI" Then
                    RepName = "ConcurLedgerLandscape_Devi" : RepTitle = "Chukti Ledger"
                ElseIf ClsMain.FDivisionNameForCustomization(6) = "SADHVI" Then
                    RepName = "ConcurLedgerLandscape_Sadhvi" : RepTitle = "Chukti Ledger"
                Else
                    RepName = "ConcurLedgerLandscape" : RepTitle = "Chukti Ledger"
                End If
            ElseIf AgL.XNull(ReportFrm.FGetText(0)).ToString.ToUpper = "Without Interest Portrait".ToUpper Then
                RepName = "ConcurLedgerWithoutInterest" : RepTitle = "Chukti Ledger"
            Else
                RepName = "ConcurLedger" : RepTitle = "Chukti Ledger"

            End If

            If Val(ReportFrm.FGetText(2)) <> 0 Then
                NoofDays = Val(ReportFrm.FGetText(2))
            Else
                MsgBox("Please Enter Valid No. Of Days.") : FunConcurLedger = Nothing : Exit Function
            End If




            Try
                mQry = "Drop Table #TempTblDr "
                AgL.Dman_ExecuteNonQry(mQry, Conn)
            Catch ex As Exception
            End Try



            mQry = "Create Temporary Table #TempTblDr 
                    (
                        DrDocID nVarchar(21),
                        DrDivision nVarchar(1),
                        DrSubcode nVarchar(10),
                        DrSr Integer,
                        DrDate DateTime,
                        DrDocNo nVarchar(21)  Collate NoCase,
                        DrAmount Float Default 0,
                        DrTaxableAmount Float Default 0,
                        DrTaxAmount Float Default 0,
                        DrDays Integer,
                        DrInterest Float,
                        DrCumAmount Float,
                        DrTotal Float,
                        DrNarration nVarchar(4000)
                    )
                    "

            AgL.Dman_ExecuteNonQry(mQry, Conn)


            Try
                mQry = "Drop Table #TempTblCr "
                AgL.Dman_ExecuteNonQry(mQry, Conn)
            Catch ex As Exception
            End Try


            mQry = "Create Temporary Table #TempTblCr 
                    (
                        CrDocID nVarchar(21),
                        CrDivision nVarchar(1),
                        CrSubcode nVarchar(10),
                        CrSr Integer,
                        CrDate DateTime,
                        CrDocNo nVarchar(21)  Collate NoCase,
                        CrAmount Float Default 0,
                        CrTaxableAmount Float Default 0,
                        CrTaxAmount Float Default 0,
                        CrDays Integer,
                        CrInterest Float,
                        CrCumAmount Float,
                        CrTotal Float,
                        CrNarration nVarchar(4000)                        
                    )
                    "

            AgL.Dman_ExecuteNonQry(mQry, Conn)


            Try
                mQry = "Drop Table #TempTblDrCr "
                AgL.Dman_ExecuteNonQry(mQry, Conn)
            Catch ex As Exception
            End Try


            mQry = "Create Temporary Table #TempTblDrCr 
                    (
                        DrDocID nVarchar(21),
                        DrDivision nVarchar(1),
                        DrSubcode nVarchar(10),
                        DrSr Integer,
                        DrDate DateTime,
                        DrDocNo nVarchar(21)  Collate NoCase,
                        DrAmount Float Default 0,
                        DrTaxableAmount Float Default 0,
                        DrTaxAmount Float Default 0,
                        DrDays Integer,
                        DrInterest Float,
                        DrCumAmount Float,
                        DrBalAmount Float,
                        DrTotal Float,
                        DrNarration nVarchar(4000),
                        CrDocID nVarchar(21),
                        CrDivision nVarchar(1),
                        CrSubcode nVarchar(10),
                        CrSr Integer,
                        CrDate DateTime,
                        CrDocNo nVarchar(21)  Collate NoCase,
                        CrAmount Float Default 0,
                        CrTaxableAmount Float Default 0,
                        CrTaxAmount Float Default 0,
                        CrDays Integer,
                        CrInterest Float,
                        CrCumAmount Float,
                        CrBalAmount Float,
                        CrTotal Float,
                        CrNarration nVarchar(4000)
                    )
                    "
            AgL.Dman_ExecuteNonQry(mQry, Conn)



            If AgL.XNull(ReportFrm.FGetText(8)) = "All" Then
                mQry = "Select D.Div_Code as Code, D.Div_Name As [Division] From Division D With (Nolock) Where Div_Code In (" & AgL.PubDivisionList & ") "
                DtDivision = AgL.FillData(mQry, Conn).Tables(0)
            Else
                mQry = "Select D.Div_Code as Code, D.Div_Name As [Division] From Division D Where 1=1 "
                mQry = mQry & Replace(ReportFrm.GetWhereCondition("D.Div_Code", 8), "''", "'")
                DtDivision = AgL.FillData(mQry, Conn).Tables(0)
            End If




            mQry = "Select Sg.Subcode, Max(Sg.Nature) as Nature 
                    From subgroup sg 
                    Left Join Area A On Sg.Area = A.Code
                    Left Join City C On Sg.CityCode = C.CityCode
                    Left Join SubgroupSiteDivisionDetail L On L.Subcode = Sg.Subcode
                    Where 1=1 And Sg.Subcode Is Not Null "

            mCondStr = mCondStr & ReportFrm.GetWhereCondition("Sg.SubCode", 3)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Agent", 5)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("Sg.CityCode", 6)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("Sg.Area", 7)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("Sg.GroupCode", 11)
            mQry = mQry + mCondStr + " Group By Sg.Subcode "

            DtSubcode = AgL.FillData(mQry, Conn).Tables(0)

            For iSubcode = 0 To DtSubcode.Rows.Count - 1

                ClsMain.GetAveragePaymentDays(AgL.XNull(DtSubcode.Rows(iSubcode)("Subcode")), True)
                Debug.Print(iSubcode.ToString + " / " + DtSubcode.Rows.Count.ToString)
                For iDivision = 0 To DtDivision.Rows.Count - 1

                    mDivision = AgL.XNull(DtDivision.Rows(iDivision)("Code"))
                    mSubcode = AgL.XNull(DtSubcode.Rows(iSubcode)("Subcode"))
                    iDr = 0 : iCr = 0 : DrSr = 0 : CrSr = 0 : mTotalDr = 0 : mTotalCr = 0

                    mCondStr = "" : mCondStrOp = ""
                    If ReportFrm.FGetText(4) = "Financial Year Opening" Then
                        mCondStr = " And Date(L.V_Date) >= " & AgL.Chk_Date(CDate(AgL.PubStartDate).ToString("s")) & " "
                    End If
                    mCondStr = mCondStr & " And Date(L.V_Date) <= " & AgL.Chk_Date(CDate(ReportFrm.FGetText(1)).ToString("s")) & " "
                    mCondStrOp = mCondStrOp & " And Date(L.V_Date) < " & AgL.Chk_Date(CDate(AgL.PubStartDate).ToString("s")) & " "
                    mCondStr = mCondStr & " And L.Subcode = " & AgL.Chk_Text(mSubcode) & " "
                    mCondStrOp = mCondStrOp & " And L.Subcode = " & AgL.Chk_Text(mSubcode) & " "
                    mCondStr = mCondStr & " And L.DivCode = " & AgL.Chk_Text(mDivision) & " "
                    mCondStrOp = mCondStrOp & " And L.DivCode = " & AgL.Chk_Text(mDivision) & " "
                    mCondStr = mCondStr & " And L.Site_Code = " & AgL.Chk_Text(AgL.PubSiteCode) & " "
                    mCondStrOp = mCondStrOp & " And L.Site_Code = " & AgL.Chk_Text(AgL.PubSiteCode) & " "

                    If ReportFrm.FGetText(4) = "After Chukti" Then
                        mCondStr = mCondStr & " And L.DocId || Cast(L.V_SNo As NVARCHAR) Not In (
                                Select IfNull(PaymentDocId ||  Cast(PaymentDocIdSr As NVARCHAR),'') 
                                From Cloth_SupplierSettlementPayments) "

                        mCondStr = mCondStr & " And L.DocId || Cast(L.V_SNo As NVARCHAR) Not In (
                                Select IfNull(PurchaseInvoiceDocId ||  Cast(PurchaseInvoiceDocIdSr As NVARCHAR),'')  
                                From Cloth_SupplierSettlementInvoices) "


                        'mCondStr = mCondStr & " And L.DocId Not In (
                        '        Select IfNull(PaymentDocId,'') 
                        '        From Cloth_SupplierSettlementPayments) "

                        'mCondStr = mCondStr & " And L.DocId Not In (
                        '        Select IfNull(PurchaseInvoiceDocId,'')  
                        '        From Cloth_SupplierSettlementInvoices) "

                        mCondStr = mCondStr & " And L.DocId Not In (
                                Select H.DocId
                                From LedgerHead H 
                                LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                                Where Vt.NCat In ('" & Ncat.PaymentSettlement & "','" & Ncat.ReceiptSettlement & "')) "
                    End If

                    '//For Cheque Cancellation Working But not okay for old data
                    'mQry = "select L.DocId, L.V_Date, L.DivCode || L.site_Code || '-' || L.V_Type || '-' || L.RecId as DocNo, (Case When IfNull(Trd.Type,'')='Cancelled' OR IfNull(Trr.Type,'')='Cancelled' Then 0 Else L.AmtDr End) as AmtDr, 
                    '(Case When IfNull(L.Chq_No,'') <>'' Then 'Chq : ' || IfNull(L.Chq_No,'') Else '' End) || 
                    '(Case When IfNull(LH.PartyDocNo,'') <>'' Then 'Inv : ' || IfNull(LH.PartyDocNo,'') Else '' End) ||
                    '(Case When IfNull(Trd.Type,'')='Cancelled' OR IfNull(Trr.Type,'')='Cancelled' Then ' Cancelled Amt.' || Cast(L.AmtDr as NVarchar) Else '' End)
                    'as DrNarration,
                    'INV.Taxable_Amount, INV.Tax1+INV.Tax2+INV.Tax3+INV.Tax4+INV.Tax5 as Tax_Amount
                    'from ledger L With (NoLock)
                    'Left Join LedgerHead LH With (NoLock) on L.DocID = LH.DocID
                    'Left Join SaleInvoice INV With (NoLock) On L.DocID = INV.DocID
                    'Left Join TransactionReferences Trd With (NoLock) On L.DocID = Trd.DocId And L.TSr = IfNull(Trd.DocIDSr, L.TSr) 
                    'Left Join TransactionReferences Trr With (NoLock) On L.DocID = Trr.ReferenceDocId And L.TSr = IfNull(Trr.ReferenceSr, L.TSr)
                    'where L.AmtDr>0  " & mCondStr & " Order By L.V_Date, Cast(Replace(L.RecId,'-','') as Integer) "

                    mQry = "Select Sum(AmtDr) as AmtDr, Sum(AmtCr) as AmtCr From Ledger L With (NoLock) Where 1=1 " & mCondStr
                    DtSubcodeBalances = AgL.FillData(mQry, Conn).Tables(0)
                    If DtSubcodeBalances.Rows.Count > 0 Then
                        mTotalDr = AgL.VNull(DtSubcodeBalances.Rows(0)("AmtDr"))
                        mTotalCr = AgL.VNull(DtSubcodeBalances.Rows(0)("AmtCr"))
                    Else
                        mTotalDr = 0
                        mTotalCr = 0
                    End If



                    mQry = ""
                    If ReportFrm.FGetText(4) = "Financial Year Opening" Then
                        mQry = "select 'Opening' DocId, " & AgL.Chk_Date(DateAdd(DateInterval.Day, -1, CDate(AgL.PubStartDate))) & " as V_Date, 'Opening' as DocNo, Sum(L.AmtDr-L.AmtCr) as AmtDr, 
                        Null as DrNarration, 0 as Taxable_Amount, 0 as Tax_Amount
                        from Ledger L With (NoLock)
                        Left Join LedgerHead LH With (NoLock) on L.DocID = LH.DocID
                        Left Join SaleInvoice INV With (NoLock) On L.DocID = INV.DocID
                        Left Join RateType Rt On Inv.RateType = Rt.Code
                        where 1=1 " & mCondStrOp & " Group By L.Subcode Having Sum(L.AmtDr-L.AmtCr) > 0 "

                        mQry = mQry & " Union All "
                    End If


                    Dim mLQry As String

                    If ClsMain.FDivisionNameForCustomization(6) = "SADHVI" Then
                        mLQry = " from 
                                (
                                SELECT L.DocId, L.Site_Code, L.DivCode, L.V_SNo, L.V_No, L.V_Type, L.V_Date, L.SubCode, L.TSr, L.AmtDr, L.RecId, L.EffectiveDate, L.Chq_No, L.Narration     
					            FROM ledger L With (NoLock) WHERE L.V_Type <> 'DNS'
					            UNION ALL 
					            SELECT L.DocId, Max(L.Site_Code) Site_Code, Max(L.DivCode) AS DivCode, Max(L.V_SNo) AS V_SNo, Max(L.V_No) AS V_No, Max(L.V_Type) AS V_Type, Max(L.V_Date) AS V_Date, Max(L.SubCode) AS  SubCode, Max(L.TSr) AS  TSr,
					            Sum(L.AmtDr) AS AmtDr, Max(L.RecId) AS RecId, Max(L.EffectiveDate) AS EffectiveDate, Max(L.Chq_No) AS Chq_No, Max(L.Narration) AS Narration  
					            FROM ledger L With (NoLock) WHERE L.V_Type = 'DNS'
					            GROUP BY L.DocId, L.SubCode   
					            ) L  "
                    Else
                        mLQry = "from ledger L With (NoLock) "
                    End If


                    If ClsMain.FDivisionNameForCustomization(6) = "SADHVI" Then
                        mQry = mQry & "select L.DocId, IfNull(L.EffectiveDate,L.V_Date) as V_Date, L.V_Type || '-' || L.RecId as DocNo, 
                                 L.AmtDr as AmtDr, 
                                (Case When IfNull(L.Chq_No,'') <>'' Then 'Chq : ' || IfNull(L.Chq_No,'') Else '' End) || 
                                (Case When IfNull(LH.PartyDocNo,'') <>'' Then 'Inv : ' || IfNull(LH.PartyDocNo,'') Else '' End) ||
                                (Case When IfNull(Inv.RateType,'') <>'' Then 'RT : ' || IfNull(RT.Description,'') Else '' End) ||
                                 IfNull(L.Narration,'')||
                                (Case When PI.V_Type ='PR' AND PI.Tags IS NOT NULL Then ' '+PI.Tags Else '' End)||
                                (Case When INV.V_Type IN ('SI','SR') AND INV.Remarks IS NOT NULL Then ' '||Substr(INV.Remarks,0,15) Else '' End)||
                                (Case When PI.V_Type IN ('PI','PR') AND PI.Remarks IS NOT NULL Then ' '||Substr(PI.Remarks,0,15) Else '' End)
                                as DrNarration,
                                INV.Taxable_Amount, INV.Tax1+INV.Tax2+INV.Tax3+INV.Tax4+INV.Tax5 as Tax_Amount
                                " & mLQry & "
                                Left Join LedgerHead LH With (NoLock) on L.DocID = LH.DocID
                                Left Join SaleInvoice INV With (NoLock) On L.DocID = INV.DocID
                                Left Join PurchInvoice PI With (NoLock) On L.DocID = PI.DocID
                                Left Join RateType Rt On Inv.RateType = Rt.Code
                                where L.AmtDr>0  " & mCondStr & "  "
                    Else
                        mQry = mQry & "select L.DocId, IfNull(L.EffectiveDate,L.V_Date) as V_Date, L.V_Type || '-' || L.RecId as DocNo, 
                            (Case When IfNull(Trd.Type,'')='Cancelled' OR IfNull(Trr.Type,'')='Cancelled' Then 0 Else L.AmtDr End) as AmtDr, 
                            (Case When IfNull(L.Chq_No,'') <>'' Then 'Chq : ' || IfNull(L.Chq_No,'') Else '' End) || 
                            (Case When IfNull(LH.PartyDocNo,'') <>'' Then 'Inv : ' || IfNull(LH.PartyDocNo,'') Else '' End) ||
                            (Case When IfNull(Inv.RateType,'') <>'' Then 'RT : ' || IfNull(RT.Description,'') Else '' End) ||
                            (Case When IfNull(Trd.Type,'')='Cancelled' OR IfNull(Trr.Type,'')='Cancelled' Then ' Cancelled Amt.' || Cast(L.AmtDr as NVarchar) Else '' End) || IfNull(L.Narration,'')||
                            (Case When PI.V_Type ='PR' AND PI.Tags IS NOT NULL Then ' '+PI.Tags Else '' End)||
                            (Case When INV.V_Type IN ('SI','SR') AND INV.Remarks IS NOT NULL Then ' '||Substr(INV.Remarks,0,15) Else '' End)||
                            (Case When PI.V_Type IN ('PI','PR') AND PI.Remarks IS NOT NULL Then ' '||Substr(PI.Remarks,0,15) Else '' End)
                            as DrNarration,
                            INV.Taxable_Amount, INV.Tax1+INV.Tax2+INV.Tax3+INV.Tax4+INV.Tax5 as Tax_Amount
                            " & mLQry & "
                            Left Join LedgerHead LH With (NoLock) on L.DocID = LH.DocID
                            Left Join SaleInvoice INV With (NoLock) On L.DocID = INV.DocID
                            Left Join PurchInvoice PI With (NoLock) On L.DocID = PI.DocID
                            Left Join RateType Rt On Inv.RateType = Rt.Code
                            Left Join TransactionReferences Trd With (NoLock) On L.DocID = Trd.DocId And L.V_SNo = Trd.DocIDSr And L.V_Date >= '2019-07-01'
                            Left Join TransactionReferences Trr With (NoLock) On L.DocID = Trr.ReferenceDocId And L.TSr = Trr.ReferenceSr And L.V_Date >= '2019-07-01'
                            where L.AmtDr>0  " & mCondStr & "  "
                    End If


                    If ReportFrm.FGetText(4) = "Financial Year Opening" Then
                        mQry = mQry & " Order By V_Date,  DocNo "
                    Else
                        If AgL.PubServerName = "" Then
                            mQry = mQry & " Order By IfNull(L.EffectiveDate,L.V_Date), Try_Parse(Replace(L.RecId,'-','') as Integer) "
                        Else
                            mQry = mQry & " Order By IfNull(L.EffectiveDate,L.V_Date), Cast((Case When IsNumeric(Replace(L.RecId,'-',''))=1 Then Replace(L.RecId,'-','') Else Null End) as Integer) "
                        End If
                    End If



                    DtDr = AgL.FillData(mQry, Conn).Tables(0)
                    DrRecordCount = DtDr.Rows.Count

                    'mQry = "
                    'select L.DocId, L.V_Date, L.DivCode || L.site_Code || '-' || L.V_Type || '-' || L.RecId as DocNo, (Case When IfNull(Trd.Type,'')='Cancelled' OR IfNull(Trr.Type,'')='Cancelled' Then 0 Else L.AmtCr End) as AmtCr,
                    '(Case When IfNull(L.Chq_No,'') <>'' Then 'Chq : ' || IfNull(L.Chq_No,'') Else '' End) || 
                    '(Case When IfNull(LH.PartyDocNo,'') <>'' Then 'Inv : ' || IfNull(LH.PartyDocNo,'') Else '' End) ||
                    '(Case When IfNull(Trd.Type,'')='Cancelled' OR IfNull(Trr.Type,'')='Cancelled' Then ' Cancelled Amt.' || Cast(L.AmtCr as NVarchar) Else '' End)
                    'as CrNarration,
                    'INV.Taxable_Amount, INV.Tax1+INV.Tax2+INV.Tax3+INV.Tax4+INV.Tax5 as Tax_Amount
                    'from ledger L  With (NoLock)
                    'Left Join LedgerHead LH  With (NoLock) On L.DocID = LH.DocID
                    'Left Join PurchInvoice INV With (NoLock) On L.DocID = INV.DocID
                    'Left Join TransactionReferences Trd With (NoLock) On L.DocID = Trd.DocId And L.TSr = IfNull(Trd.DocIDSr, L.TSr)
                    'Left Join TransactionReferences Trr With (NoLock) On L.DocID = Trr.ReferenceDocId And L.TSr = IfNull(Trr.ReferenceSr, L.TSr)
                    'where L.AmtCr>0  " & mCondStr & " Order By L.V_Date,  Cast(Replace(L.RecId,'-','') as Integer) "



                    mQry = ""
                    If ReportFrm.FGetText(4) = "Financial Year Opening" Then
                        mQry = "select 'Opening' DocId, " & AgL.Chk_Date(DateAdd(DateInterval.Day, -1, CDate(AgL.PubStartDate))) & " as V_Date, 'Opening' as DocNo, Sum(L.AmtCr-L.AmtDr) as AmtCr, 
                        Null as CrNarration, 0 as Taxable_Amount, 0 as Tax_Amount
                        from Ledger L With (NoLock)
                        Left Join LedgerHead LH With (NoLock) on L.DocID = LH.DocID
                        where 1=1 " & mCondStrOp & " Group By L.Subcode Having Sum(L.AmtCr-L.AmtDr) > 0 "

                        mQry = mQry & " Union All "
                    End If


                    mQry = mQry & "
                    select L.DocId, IfNull(L.EffectiveDate,L.V_Date) as V_Date, L.V_Type || '-' || L.RecId as DocNo, 
                    (Case When IfNull(Trd.Type,'')='Cancelled' OR IfNull(Trr.Type,'')='Cancelled' Then 0 Else L.AmtCr End) as AmtCr,
                    (Case When IfNull(L.Chq_No,'') <>'' Then 'Chq : ' || IfNull(L.Chq_No,'') Else '' End) || 
                    (Case When IfNull(LH.PartyDocNo,'') <>'' Then 'Inv : ' || IfNull(LH.PartyDocNo,'') Else '' End) ||                    
                    (Case When VT.NCAT Not In ('" & Ncat.SaleInvoice & "', '" & Ncat.PurchaseInvoice & "') And '" & ClsMain.FDivisionNameForCustomization(6) & "' = 'SADHVI' Then IfNull(L.Narration,'') Else '' End) ||
                    (Case When IfNull(Trd.Type,'')='Cancelled' OR IfNull(Trr.Type,'')='Cancelled' Then ' Cancelled Amt.' || Cast(L.AmtCr as NVarchar)  Else '' End) ||
                    (Case When IfNull(LTrim(Substr(L.ReferenceDocID,4,5)),'VR') <>'VR' Then ' Ref : ' || IfNull(LTrim(Substr(L.ReferenceDocID,4,5)),'') Else '' End)||
                    (Case When INV.V_Type IN ('PI','PR') AND INV.Remarks IS NOT NULL Then ' '+Substr(INV.Remarks,0,15) Else '' End)||
                    (Case When SI.V_Type IN ('SI','SR') AND SI.Remarks IS NOT NULL Then ' '+Substr(SI.Remarks,0,15) Else '' End)
                    as CrNarration,
                    INV.Taxable_Amount, INV.Tax1+INV.Tax2+INV.Tax3+INV.Tax4+INV.Tax5 as Tax_Amount
                    from ledger L  With (NoLock)
                    Left Join LedgerHead LH  With (NoLock) On L.DocID = LH.DocID
                    Left Join PurchInvoice INV With (NoLock) On L.DocID = INV.DocID
                    Left Join SaleInvoice SI With (NoLock) On L.DocID = SI.DocID
                    Left Join TransactionReferences Trd With (NoLock) On L.DocID = Trd.DocId And L.V_SNo = Trd.DocIDSr And L.V_Date >= '2019-07-01'
                    Left Join TransactionReferences Trr With (NoLock) On L.DocID = Trr.ReferenceDocId And L.TSr = Trr.ReferenceSr And L.V_Date >= '2019-07-01'
                    Left Join Voucher_Type Vt On L.V_Type = VT.V_Type
                    where L.AmtCr>0  " & mCondStr & "  "

                    If ReportFrm.FGetText(4) = "Financial Year Opening" Then
                        mQry = mQry & " Order By V_Date,  DocNo "
                    Else
                        If AgL.PubServerName = "" Then
                            mQry = mQry & " Order By IfNull(L.EffectiveDate,L.V_Date),  Try_Parse(Replace(L.RecId,'-','') as Integer) "
                        Else
                            'mQry = mQry & " Order By IfNull(L.EffectiveDate,L.V_Date),  Try_Parse(Replace(L.RecId,'-','') as Integer) "
                            mQry = mQry & " Order By IfNull(L.EffectiveDate,L.V_Date), Cast((Case When IsNumeric(Replace(L.RecId,'-',''))=1 Then Replace(L.RecId,'-','') Else Null End) as BigInt) "
                        End If
                    End If

                    DtCr = AgL.FillData(mQry, Conn).Tables(0)
                    CrRecordCount = DtCr.Rows.Count


                    Dim mRunningTotalDr As Double
                    Dim mRunningTotalCr As Double
                    Dim mDays As Integer
                    Dim mInterest As Double
                    mRunningTotalDr = 0 : mRunningTotalCr = 0 : mLastChuktiAmount = 0 : mDays = 0 : mInterest = 0 : ConcurSr = -1 : FirstConcurSr = -1

                    LoopLimit = DrRecordCount + CrRecordCount ' IIf(DrRecordCount >= CrRecordCount, DrRecordCount, CrRecordCount)
                    For I = 0 To LoopLimit

                        If DrRecordCount > iDr Then
                            If iDr = 0 Or iCr >= CrRecordCount Or mRunningTotalDr <= mRunningTotalCr Then
                                If AgL.XNull(DtSubcode.Rows(iSubcode)("Nature")).ToString.ToUpper = "CUSTOMER" Then
                                    mDays = DateDiff(DateInterval.Day, DateAdd(DateInterval.Day, Val(ReportFrm.FGetText(2)), CDate(DtDr.Rows(iDr)("V_Date"))), CDate(ReportFrm.FGetText(1)))
                                Else
                                    mDays = DateDiff(DateInterval.Day, AgL.XNull(DtDr.Rows(iDr)("V_Date")), CDate(ReportFrm.FGetText(1)))
                                End If
                                If mDays < 0 Then mDays = 0
                                'mInterest = Math.Round(Val(DtDr.Rows(iDr)("AmtDr")) * AgL.VNull(AgL.PubDtEnviro.Rows(0)("Default_DebtorsInterestRate")) * (mDays / 36500), 2)
                                mInterest = Math.Round(Val(DtDr.Rows(iDr)("AmtDr")) * Val(ReportFrm.FGetText(10)) * (mDays / 36500), 2)
                                mRunningTotalDr += AgL.VNull(DtDr.Rows(iDr)("AmtDr"))
                                mQry = "Insert Into #TempTblDr (DrDocID,DrDivision,DrSubcode,DrSr,DrDate,DrDocNo,DrAmount,DrTaxableAmount,DrTaxAmount,DrDays,DrInterest,DrNarration, DrCumAmount, DrTotal)
                                        Values(" & AgL.Chk_Text(AgL.XNull(DtDr.Rows(iDr)("DocID"))) & "," & AgL.Chk_Text(mDivision) & ", " & AgL.Chk_Text(mSubcode) & ", " & DrSr & ", " & AgL.Chk_Date(AgL.XNull(DtDr.Rows(iDr)("V_Date"))) & ", " & AgL.Chk_Text(AgL.XNull(DtDr.Rows(iDr)("DocNo"))) & "," & AgL.VNull(DtDr.Rows(iDr)("AmtDr")) & "," & AgL.VNull(DtDr.Rows(iDr)("Taxable_Amount")) & "," & AgL.VNull(DtDr.Rows(iDr)("Tax_Amount")) & ", " & mDays & ", " & mInterest & "," & AgL.Chk_Text(AgL.XNull(DtDr.Rows(iDr)("DrNarration"))) & ", " & Val(mRunningTotalDr) & ", " & Val(mTotalDr) & ")
                                       "
                                AgL.Dman_ExecuteNonQry(mQry, Conn)
                                iDr += 1
                                DrSr += 1
                            End If
                        End If


                        If Math.Round(mRunningTotalDr, 2) = Math.Round(mRunningTotalCr, 2) And mLastChuktiAmount <> mRunningTotalDr And mRunningTotalDr > 0 Then
                            J = 0
                            mLastChuktiAmount = mRunningTotalDr
                            If DrSr > CrSr Then
                                For J = CrSr To DrSr - 1
                                    mQry = "Insert Into #TempTblCr(CrDivision,CrSubcode, CrSr, CrCumAmount, CrTotal) Values(" & AgL.Chk_Text(mDivision) & ", " & AgL.Chk_Text(mSubcode) & ", " & J & ", " & Val(mRunningTotalCr) & ", " & Val(mTotalCr) & ")"
                                    AgL.Dman_ExecuteNonQry(mQry, Conn)
                                Next
                            ElseIf CrSr > DrSr Then
                                For J = DrSr To CrSr - 1
                                    mQry = "Insert Into #TempTblDr(DrDivision,DrSubcode,DrSr, DrCumAmount, DrTotal) Values(" & AgL.Chk_Text(mDivision) & ", " & AgL.Chk_Text(mSubcode) & "," & J & ", " & Val(mRunningTotalDr) & ", " & Val(mTotalDr) & ")"
                                    AgL.Dman_ExecuteNonQry(mQry, Conn)
                                Next
                            End If



                            If iDr <= DrRecordCount Or iCr <= CrRecordCount Then
                                If J = 0 Then J = DrSr + 1
                                mQry = "Insert Into #TempTblDr(DrDivision,DrSubcode,DrSr, DrCumAmount, DrTotal) Values(" & AgL.Chk_Text(mDivision) & ", " & AgL.Chk_Text(mSubcode) & ", " & J & ", " & Val(mRunningTotalDr) & ", " & Val(mTotalDr) & ")"
                                AgL.Dman_ExecuteNonQry(mQry, Conn)


                                mQry = "Insert Into #TempTblCr(CrDivision,CrSubcode, CrSr, CrCumAmount, CrTotal) Values(" & AgL.Chk_Text(mDivision) & ", " & AgL.Chk_Text(mSubcode) & ", " & J & ", " & Val(mRunningTotalCr) & ", " & Val(mTotalCr) & ")"
                                AgL.Dman_ExecuteNonQry(mQry, Conn)

                                ConcurSr = J
                                'If CDate(AgL.XNull(DtDr.Rows(iDr)("V_Date"))) < CDate(DateAdd(DateInterval.Year, -1, CDate(AgL.PubStartDate))) Then
                                If CDate(AgL.XNull(DtDr.Rows(iDr - 1)("V_Date"))) < CDate(AgL.PubStartDate) Then
                                    FirstConcurSr = ConcurSr
                                End If

                                DrSr = J + 1
                                CrSr = J + 1

                                LoopLimit += 1
                            End If
                        End If



                        If CrRecordCount > iCr Then
                            If iCr = 0 Or iDr >= DrRecordCount Or mRunningTotalDr > mRunningTotalCr Then
                                'mDays = DateDiff(DateInterval.Day, DateAdd(DateInterval.Day, Val(ReportFrm.FGetText(1)), AgL.XNull(DtCr.Rows(iCr)("V_Date"))), CDate(ReportFrm.FGetText(0)))
                                If AgL.XNull(DtSubcode.Rows(iSubcode)("Nature")).ToString.ToUpper = "CUSTOMER" Then
                                    mDays = DateDiff(DateInterval.Day, AgL.XNull(DtCr.Rows(iCr)("V_Date")), CDate(ReportFrm.FGetText(1)))
                                Else
                                    mDays = DateDiff(DateInterval.Day, DateAdd(DateInterval.Day, Val(ReportFrm.FGetText(2)), CDate(DtCr.Rows(iCr)("V_Date"))), CDate(ReportFrm.FGetText(1)))
                                End If
                                'mInterest = Math.Round(Val(DtCr.Rows(iCr)("AmtCr")) * AgL.VNull(AgL.PubDtEnviro.Rows(0)("Default_DebtorsInterestRate")) * (mDays / 36500), 2)
                                mInterest = Math.Round(Val(DtCr.Rows(iCr)("AmtCr")) * Val(ReportFrm.FGetText(10)) * (mDays / 36500), 2)
                                mRunningTotalCr += AgL.VNull(DtCr.Rows(iCr)("AmtCr"))
                                mQry = "Insert Into #TempTblCr (CrDocId,CrDivision, CrSubcode, CrSr,CrDate,CrDocNo,CrAmount,CrTaxableAmount,CrTaxAmount,CrDays,CrInterest,CrNarration,CrCumAmount,CrTotal)
                                        Values(" & AgL.Chk_Text(AgL.XNull(DtCr.Rows(iCr)("DocID"))) & "," & AgL.Chk_Text(mDivision) & ", " & AgL.Chk_Text(mSubcode) & ", " & CrSr & ", " & AgL.Chk_Date(AgL.XNull(DtCr.Rows(iCr)("V_Date"))) & ", " & AgL.Chk_Text(AgL.XNull(DtCr.Rows(iCr)("DocNo"))) & "," & AgL.VNull(DtCr.Rows(iCr)("AmtCr")) & "," & AgL.VNull(DtCr.Rows(iCr)("Taxable_Amount")) & "," & AgL.VNull(DtCr.Rows(iCr)("Tax_Amount")) & ", " & mDays & ", " & mInterest & "," & AgL.Chk_Text(AgL.XNull(DtCr.Rows(iCr)("CrNarration"))) & ", " & Val(mRunningTotalCr) & ", " & Val(mTotalCr) & ")
                                       "
                                AgL.Dman_ExecuteNonQry(mQry, Conn)

                                iCr += 1
                                CrSr += 1
                            End If
                        End If

                        'Try
                        '    Debug.Print("Dr : " + AgL.XNull(DtDr.Rows(iDr - 1)("V_Date")) + "   Amt : " + Val(DtDr.Rows(iDr - 1)("AmtDr")).ToString() + "   RunningTotal : " + mRunningTotalDr.ToString)
                        'Catch ex As Exception
                        'End Try
                        'Try
                        '    Debug.Print("Cr : " + AgL.XNull(DtCr.Rows(iCr - 1)("V_Date")) + "   Amt : " + Val(DtCr.Rows(iCr - 1)("AmtCr")).ToString() + "   RunningTotal : " + mRunningTotalCr.ToString)
                        'Catch ex As Exception
                        'End Try


                        If Math.Round(mRunningTotalDr, 2) = Math.Round(mRunningTotalCr, 2) And mLastChuktiAmount <> mRunningTotalDr And mRunningTotalDr > 0 Then
                            J = 0
                            mLastChuktiAmount = mRunningTotalDr
                            If DrSr > CrSr Then
                                For J = CrSr To DrSr - 1
                                    mQry = "Insert Into #TempTblCr(CrDivision,CrSubcode, CrSr, CrCumAmount, CrTotal) Values(" & AgL.Chk_Text(mDivision) & ", " & AgL.Chk_Text(mSubcode) & ", " & J & ", " & Val(mRunningTotalCr) & ", " & Val(mTotalCr) & ")"
                                    AgL.Dman_ExecuteNonQry(mQry, Conn)
                                Next
                            ElseIf CrSr > DrSr Then
                                For J = DrSr To CrSr - 1
                                    mQry = "Insert Into #TempTblDr(DrDivision,DrSubcode,DrSr, DrCumAmount, DrTotal) Values(" & AgL.Chk_Text(mDivision) & ", " & AgL.Chk_Text(mSubcode) & "," & J & ", " & Val(mRunningTotalDr) & ", " & Val(mTotalDr) & ")"
                                    AgL.Dman_ExecuteNonQry(mQry, Conn)
                                Next
                            End If



                            If iDr <= DrRecordCount Or iCr <= CrRecordCount Then
                                If J = 0 Then J = DrSr + 1
                                mQry = "Insert Into #TempTblDr(DrDivision,DrSubcode,DrSr, DrCumAmount, DrTotal) Values(" & AgL.Chk_Text(mDivision) & ", " & AgL.Chk_Text(mSubcode) & ", " & J & ", " & Val(mRunningTotalDr) & ", " & Val(mTotalDr) & ")"
                                AgL.Dman_ExecuteNonQry(mQry, Conn)


                                mQry = "Insert Into #TempTblCr(CrDivision,CrSubcode, CrSr, CrCumAmount, CrTotal) Values(" & AgL.Chk_Text(mDivision) & ", " & AgL.Chk_Text(mSubcode) & ", " & J & ", " & Val(mRunningTotalCr) & ", " & Val(mTotalCr) & ")"
                                AgL.Dman_ExecuteNonQry(mQry, Conn)

                                ConcurSr = J
                                'If CDate(AgL.XNull(DtCr.Rows(iCr)("V_Date"))) < CDate(DateAdd(DateInterval.Year, -1, CDate(AgL.PubStartDate))) Then
                                If CDate(AgL.XNull(DtCr.Rows(iCr - 1)("V_Date"))) < CDate(AgL.PubStartDate) Then
                                    FirstConcurSr = ConcurSr
                                End If


                                DrSr = J + 1
                                CrSr = J + 1

                                LoopLimit += 1
                            End If
                        End If
                    Next


                    If DrSr > CrSr Then
                        For J = CrSr To DrSr - 1
                            mQry = "Insert Into #TempTblCr(CrDivision,CrSubcode, CrSr, CrCumAmount, CrTotal) Values(" & AgL.Chk_Text(mDivision) & ", " & AgL.Chk_Text(mSubcode) & ", " & J & ", " & Val(mRunningTotalCr) & ", " & Val(mTotalCr) & ")"
                            AgL.Dman_ExecuteNonQry(mQry, Conn)
                        Next
                    ElseIf CrSr > DrSr Then
                        For J = DrSr To CrSr - 1
                            mQry = "Insert Into #TempTblDr(DrDivision,DrSubcode, DrSr, DrCumAmount, DrTotal) Values(" & AgL.Chk_Text(mDivision) & ", " & AgL.Chk_Text(mSubcode) & ", " & J & ", " & Val(mRunningTotalDr) & ", " & Val(mTotalDr) & ")"
                            AgL.Dman_ExecuteNonQry(mQry, Conn)
                        Next
                    End If


                    mQry = "Insert Into #TempTblDrCr (DrDocID,DrDivision,DrSubcode, DrSr, DrDate, DrDocNo, DrAmount, DrTaxableAmount, DrTaxAmount, DrDays, DrInterest, DrNarration,DrCumAmount,DrTotal,
                        CrDocId,CrDivision,CrSubcode, CrSr, CrDate, CrDocNo, CrAmount, CrTaxableAmount, CrTaxAmount, CrDays, CrInterest, CrNarration,CrCumAmount, CrTotal, DrBalAmount, CrBalAmount) 
                    Select Dr.DrDocID, Dr.DrDivision, Dr.DrSubcode, Dr.DrSr, Dr.DrDate, Dr.DrDocNo, Dr.DrAmount, Dr.DrTaxableAmount, Dr.DrTaxAmount, Dr.DrDays, Dr.DrInterest, Dr.DrNarration, Dr.DrCumAmount, Dr.DrTotal, 
                        Cr.CrDocID, Cr.CrDivision,Cr.CrSubcode, Cr.CrSr, Cr.CrDate, Cr.CrDocNo, Cr.CrAmount, Cr.CrTaxableAmount, Cr.CrTaxAmount, Cr.CrDays, Cr.CrInterest, Cr.CrNarration, Cr.CrCumAmount, Cr.CrTotal,                    
                    (Case When Dr.DrAmount-((Case When (Cr.CrTotal-(Dr.DrCumAmount-Dr.DrAmount))<0 Then 0 Else Cr.CrTotal-(Dr.DrCumAmount-Dr.DrAmount) End))<0 Then 0 Else Dr.DrAmount-((Case When (Cr.CrTotal-(Dr.DrCumAmount-Dr.DrAmount))<0 Then 0 Else Cr.CrTotal-(Dr.DrCumAmount-Dr.DrAmount) End)) End) as DrBalAmount,
                    (Case When Cr.CrAmount-((Case When (Dr.DrTotal-(Cr.CrCumAmount-Cr.CrAmount))<0 Then 0 Else Dr.DrTotal-(Cr.CrCumAmount-Cr.CrAmount) End))<0 Then 0 Else Cr.CrAmount-((Case When (Dr.DrTotal-(Cr.CrCumAmount-Cr.CrAmount))<0 Then 0 Else Dr.DrTotal-(Cr.CrCumAmount-Cr.CrAmount) End)) End) as CrBalAmount
                    From #TempTblDr Dr, #TempTblCr Cr Where Dr.DrDivision = Cr.CrDivision And  Dr.DrSubcode = Cr.CrSubcode And Dr.DrSr = Cr.CrSr "

                    If ReportFrm.FGetText(4) = "After Chukti" Then
                        mQry = mQry & " And Dr.DrSr > " & ConcurSr & ""
                    ElseIf ReportFrm.FGetText(4) = "Financial Year" Then
                        mQry = mQry & " And Dr.DrSr > " & FirstConcurSr & ""
                    End If

                    mQry = mQry & " Order By Dr.DrSr "

                    AgL.Dman_ExecuteNonQry(mQry, Conn)

                    mQry = "Delete From #TempTblDr"
                    AgL.Dman_ExecuteNonQry(mQry, Conn)

                    mQry = "Delete From #TempTblCr"
                    AgL.Dman_ExecuteNonQry(mQry, Conn)

                Next iDivision
            Next iSubcode






            mQry = "Select D.Name as DivisionName, SG.CreditLimit, Sg.Name as PartyName, Sg.Address, Sg.Mobile, Agent.Name as AgentName, 
                    SRep.Name as SalesRepresentativeName, Area.Description as AreaName, H.*, SL.AdditionPer, SL.AdditionAmount, Gr.GrReturnAmt, Gr.GrSaleAmt, Gr.ReturnPer, CASE WHEN sg1.SubgroupType ='Customer' THEN Sg1.AveragePaymentDays ELSE 0 END AveragePaymentDays,"
            If AgL.PubServerName <> "" Then
                mQry = mQry & "Substring(Convert(NVARCHAR, H.DrDate,103),4,7) As [DrMonth], Substring(Convert(NVARCHAR, H.CrDate,103),4,7) As [CrMonth]  "
            Else
                mQry = mQry & "strftime('%m-%Y',H.DrDate) As [DrMonth], strftime('%m-%Y',H.CrDate) As [CrMonth]  "
            End If

            mQry = mQry & "from #TempTblDrCr H 
                    Left Join viewHelpSubgroup Sg on H.DrSubcode COLLATE DATABASE_DEFAULT = Sg.Code COLLATE DATABASE_DEFAULT
                    Left Join subgroup sg1 on sg.code= Sg1.Subcode
                    Left Join viewHelpSubgroup D On D.Code COLLATE DATABASE_DEFAULT = H.DrDivision COLLATE DATABASE_DEFAULT
                    Left Join (
                                select subcode, Max(Agent) as Agent, Max(SalesRepresentative) as SalesRepresentative
                                From SubgroupSiteDivisionDetail
                                Group By Subcode
                              ) as LTV On LTV.Subcode = Sg.Code
                    Left Join viewHelpSubgroup Agent  On LTV.Agent COLLATE DATABASE_DEFAULT = Agent.Code COLLATE DATABASE_DEFAULT
                    Left Join viewHelpSubgroup SRep  On  LTV.SalesRepresentative COLLATE DATABASE_DEFAULT = SRep.Code COLLATE DATABASE_DEFAULT
                    Left Join Area On Sg1.Area = Area.Code
                    Left Join (
                                SELECT L.SubCode, L.DivCode, Sum(L.AmtCr) AS GrReturnAmt, (CASE WHEN Sum(L.AmtDr) = 0 THEN Sum(L.AmtCr) ELSE Sum(L.AmtDr) END) AS GrSaleAmt,  Round((Sum(L.AmtCr) /  (CASE WHEN Sum(L.AmtDr) = 0 THEN Sum(L.AmtCr) ELSE Sum(L.AmtDr) END))*100,2) 	AS ReturnPer
                                FROM Ledger L With (NoLock)
                                LEFT JOIN Voucher_Type VT With (NoLock) ON L.V_Type = vt.V_type
                                LEFT JOIN subgroup Sg  With (NoLock) ON L.SubCode = Sg.Subcode 
                                WHERE VT.NCat IN ('SI','SR') OR VT.V_Type ='OB' AND Sg.Nature ='Customer'
                                GROUP BY L.SubCode, L.DivCode  
                                HAVING Sum(L.AmtCr) > 0
                                UNION ALL 
                                SELECT L.SubCode, L.DivCode, Sum(L.AmtDr) AS GrReturnAmt, (CASE WHEN Sum(L.AmtCr) = 0 THEN Sum(L.AmtDr) ELSE Sum(L.AmtCr) END) AS GrSaleAmt,  Round((Sum(L.AmtDr) /  (CASE WHEN Sum(L.AmtCr) = 0 THEN Sum(L.AmtDr) ELSE Sum(L.AmtCr) END))*100,2) 	AS ReturnPer
                                FROM Ledger L With (NoLock)
                                LEFT JOIN Voucher_Type VT With (NoLock) ON L.V_Type = vt.V_type
                                LEFT JOIN subgroup Sg  With (NoLock) ON L.SubCode = Sg.Subcode 
                                WHERE VT.NCat IN ('PI','PR') OR VT.V_Type ='OB' AND Sg.Nature ='Supplier'
                                GROUP BY L.SubCode, L.DivCode  
                                HAVING Sum(L.AmtDr) > 0
                              ) as Gr On Gr.Subcode  COLLATE DATABASE_DEFAULT = H.DrSubcode  COLLATE DATABASE_DEFAULT And Gr.DivCode  COLLATE DATABASE_DEFAULT = H.DrDivision  COLLATE DATABASE_DEFAULT
                    Left Join (
                                SELECT DocID, Max(AdditionPer) AS AdditionPer, Sum(AdditionAmount) AS AdditionAmount  
                                FROM SaleInvoiceDetail GROUP BY DocID 
                              ) as SL On H.DrDocID  COLLATE DATABASE_DEFAULT = SL.DocId  COLLATE DATABASE_DEFAULT 
                    Order By H.DrSubcode, H.DrSr"
            DsRep = AgL.FillData(mQry, Conn)

            FunConcurLedger = DsRep
        Catch ex As Exception
            FunConcurLedger = Nothing
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Function

    Private Sub ProcConcurLedger()
        Dim DsRep As DataSet = FunConcurLedger(AgL.GCn)

        If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records to Print!")
        ReportFrm.DefaultMobileNo = AgL.XNull(DsRep.Tables(0).Rows(0)("Mobile"))
        'ReportFrm.DefaultMobileNo = "8299399688"
        ReportFrm.PrintReport(DsRep, RepName, RepTitle)
    End Sub

End Class
