Imports System.IO
Imports AgLibrary
Imports AgLibrary.ClsMain
Imports AgLibrary.ClsMain.agConstants
Imports CrystalDecisions.CrystalReports.Engine
Imports Microsoft.Reporting.WinForms
Public Class ClsSupplierOutstanding_ShyamaShyam

    Enum ShowDataIn
        Grid = 1
        Crystal = 2
    End Enum

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

    Public Const Col1SearchCode As String = "Search Code"

    Dim rowReportType As Integer = 0
    Dim rowGroupOn As Integer = 1
    Dim rowBillsUptoDate As Integer = 2
    Dim rowPaymentsUptoDate As Integer = 3
    Dim rowParty As Integer = 4
    Dim rowMasterParty As Integer = 5
    Dim rowLinkedParty As Integer = 6
    Dim rowAgent As Integer = 7
    Dim rowCity As Integer = 8
    Dim rowArea As Integer = 9
    Dim rowDivision As Integer = 10
    Dim rowSite As Integer = 11

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

    Structure OutstandingBill
        Public DocNo As String
        Public DocDate As Date
        Public Narration As String
        Public DocAmount As Double
        Public BalAmount As Double
        Public DrCr As String
    End Structure

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

    Public Sub Ini_Grid()
        Dim mDefaultValue As String = ""
        Try



            Dim mQry As String
            Dim I As Integer = 0



            mQry = " Select 'Detail' as Code, 'Detail' as Name "
            ReportFrm.CreateHelpGrid("Report Type", "Report Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mQry, "Detail",,,,, False)

            mQry = "Select 'Party' as Code, 'Party' as Name 
                    Union All
                    Select 'Linked Party' as Code, 'Linked Party' as Name"
            ReportFrm.CreateHelpGrid("Group On", "Group On", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mQry, "Party")
            ReportFrm.FilterGrid.Rows(rowGroupOn).Visible = False

            ReportFrm.CreateHelpGrid("Bills Upto Date", "Bills Upto Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubLoginDate)

            ReportFrm.CreateHelpGrid("Payments Upto Date", "Payments Upto Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubLoginDate)
            ReportFrm.FilterGrid.Rows(rowPaymentsUptoDate).Visible = False

            mQry = " Select 'o' As Tick,  Sg.Code, Sg.Name AS Party, Sg.Address, Ag.GroupName FROM viewHelpSubGroup Sg  Left Join AcGroup Ag On Sg.groupCode= ag.GroupCode Where Sg.Nature In ('Supplier','Cash') And Sg.SubgroupType Not In ('Master Customer','Master Supplier') "
            ReportFrm.CreateHelpGrid("Supplier", "Supplier", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mQry, , 450, 825, 300)

            mQry = " Select 'o' As Tick,  Sg.Code, Sg.Name AS Party, Sg.Address, Ag.GroupName FROM viewHelpSubGroup Sg  Left Join AcGroup Ag On Sg.groupCode= ag.GroupCode Where Sg.Nature In ('Supplier') And Sg.Code In (Select Distinct Parent From SubGroup) Order By Name"
            ReportFrm.CreateHelpGrid("Master Supplier", "Master Supplier", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mQry, , 450, 825, 300)
            If Not ClsMain.IsScopeOfWorkContains(IndustryType.CommonModules.MasterParty) Then ReportFrm.FilterGrid.Rows(rowMasterParty).Visible = False

            mQry = " Select 'o' As Tick,  Sg.Code, Sg.Name AS Party, Sg.Address, Ag.GroupName FROM viewHelpSubGroup Sg  Left Join AcGroup Ag On Sg.groupCode= ag.GroupCode Where Sg.SubgroupType In ('Master Supplier') Order By Name"
            ReportFrm.CreateHelpGrid("Linked Supplier", "Linked Supplier", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mQry, , 450, 825, 300)

            mQry = " Select 'o' As Tick,  Sg.Code, Sg.Name AS Agent FROM viewHelpSubgroup Sg Where Sg.SubgroupType = '" & SubgroupType.PurchaseAgent & "' "
            ReportFrm.CreateHelpGrid("Agent", "Agent", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mQry)

            mQry = "Select 'o' As Tick, CityCode, CityName From City "
            ReportFrm.CreateHelpGrid("City", "City", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mQry)

            mQry = "Select 'o' As Tick, Code, Description From Area "
            ReportFrm.CreateHelpGrid("Area", "Area", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mQry)

            mDefaultValue = ClsMain.FGetSettings(ClsMain.SettingFields.DefaultDivisionNameInReportFilters, SettingType.General, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
            If mDefaultValue = "All" Then
                mDefaultValue = "All"
            Else
                mDefaultValue = "[DIVISIONCODE]"
            End If
            mQry = "Select 'o' As Tick, Div_Code as Code, Div_Name As [Division] From Division "
            ReportFrm.CreateHelpGrid("Division", "Division", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mQry, mDefaultValue)
            If AgL.PubDivisionCount = 1 Then ReportFrm.FilterGrid.Rows(rowDivision).Visible = False

            mDefaultValue = ClsMain.FGetSettings(ClsMain.SettingFields.DefaultSiteNameInReportFilters, SettingType.General, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
            If mDefaultValue = "All" Then
                mDefaultValue = "All"
            Else
                mDefaultValue = "[SITECODE]"
            End If
            mQry = "Select 'o' As Tick, Code, Name As [Site] From SiteMast Where  Code In (" & AgL.PubSiteList & ")  "
            ReportFrm.CreateHelpGrid("Site", "Site", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mQry, mDefaultValue)



            'ReportFrm.BtnCustomMenu.Visible = True
            'mQry = "Select 'Formatted Print' As MenuText, 'ProcFormattedPrint' As FunctionName"
            'Dim DtMenuList As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
            'ReportFrm.MnuCustomOption.Items.Clear()
            'ReportFrm.DTCustomMenus = DtMenuList
            'ReportFrm.FCreateCustomMenus()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub ObjRepFormGlobal_ProcessReport() Handles ReportFrm.ProcessReport
        ProcFillReport()
    End Sub
    Public Sub New(ByVal mReportFrm As FrmRepDisplay)
        ReportFrm = mReportFrm
        ReportFrm.ClsRep = Me
    End Sub
    Private Function CreateCondStrX() As String
        Dim mCondStr As String

        mCondStr = " And Sg.Nature In ('Customer','Supplier') "
        mCondStr = mCondStr & " AND Date(LG.V_Date) <= (Case 
                                                            When Sg.Nature='Customer' And Lg.AmtDr>0 Then " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowBillsUptoDate)).ToString("s")) & " 
                                                            When Sg.Nature='Customer' And Lg.AmtCr>0 Then " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowPaymentsUptoDate)).ToString("s")) & " 
                                                            When Sg.Nature<>'Customer' And Lg.AmtDr>0 Then " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowPaymentsUptoDate)).ToString("s")) & " 
                                                            When Sg.Nature<>'Customer' And Lg.AmtCr>0 Then " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowBillsUptoDate)).ToString("s")) & " 
                                                            End) "
        mCondStr = mCondStr & " And Date(LG.V_Date) <= " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowBillsUptoDate)).ToString("s")) & "  "

        mCondStr = mCondStr & ReportFrm.GetWhereCondition("LG.Subcode", rowParty)
        mCondStr = mCondStr & ReportFrm.GetWhereCondition("Sg.Parent", rowMasterParty)
        mCondStr = mCondStr & ReportFrm.GetWhereCondition("LG.LinkedSubcode", rowLinkedParty)
        'mCondStr = mCondStr & ReportFrm.GetWhereCondition("Sg.GroupCode", rowa)
        mCondStr = mCondStr & ReportFrm.GetWhereCondition("LTV.Agent", rowAgent)
        mCondStr = mCondStr & ReportFrm.GetWhereCondition("Sg.CityCode", rowCity)
        mCondStr = mCondStr & ReportFrm.GetWhereCondition("Sg.Area", rowArea)
        mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("LG.Site_Code", rowSite), "''", "'")
        mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("LG.DivCode", rowDivision), "''", "'")


        mCondStr = mCondStr & " And LG.DocID || LG.V_SNo Not In (SELECT H.PurchaseInvoiceDocId || H.PurchaseInvoiceDocIdSr   FROM Cloth_SupplierSettlementInvoices H
                                                                       UNION ALL 
                                                                       SELECT H.PaymentDocId || H.PaymentDocIdSr   FROM Cloth_SupplierSettlementPayments H
                                                                       ) "
        If ClsMain.FDivisionNameForCustomization(22) = "W SHYAMA SHYAM FABRICS" Or ClsMain.FDivisionNameForCustomization(27) = "W SHYAMA SHYAM VENTURES LLP" Then
            mCondStr = mCondStr & " And LG.DocID Not In ( SELECT H.DocID FROM LedgerHead H LEFT JOIN LedgerHeadDetail L ON H.DocID = L.DocID WHERE H.V_Type In ('WPS','WRS') ) "
        Else
            mCondStr = mCondStr & " And LG.DocID Not In ( SELECT H.DocID FROM LedgerHead H LEFT JOIN LedgerHeadDetail L ON H.DocID = L.DocID WHERE H.V_Type In ('" & Ncat.PaymentSettlement & "','" & Ncat.ReceiptSettlement & "') ) "
        End If






        CreateCondStrX = mCondStr
    End Function

    Private Function CreateCondStr() As String
        Dim mCondStr As String

        mCondStr = " And Sg.Nature In ('Customer','Supplier') "
        mCondStr = mCondStr & " And Date(LG.V_Date) <= " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowBillsUptoDate)).ToString("s")) & "  "

        mCondStr = mCondStr & ReportFrm.GetWhereCondition("SG.Subcode", rowParty)
        mCondStr = mCondStr & ReportFrm.GetWhereCondition("Sg.Parent", rowMasterParty)
        mCondStr = mCondStr & ReportFrm.GetWhereCondition("LG.BillToParty", rowLinkedParty)
        mCondStr = mCondStr & ReportFrm.GetWhereCondition("LTV.Agent", rowAgent)
        mCondStr = mCondStr & ReportFrm.GetWhereCondition("Sg.CityCode", rowCity)
        mCondStr = mCondStr & ReportFrm.GetWhereCondition("Sg.Area", rowArea)
        mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("LG.Site_Code", rowSite), "''", "'")



        'mCondStr = mCondStr & " And LG.DocID || LG.V_SNo Not In (SELECT H.PurchaseInvoiceDocId || H.PurchaseInvoiceDocIdSr   FROM Cloth_SupplierSettlementInvoices H
        '                                                               UNION ALL 
        '                                                               SELECT H.PaymentDocId || H.PaymentDocIdSr   FROM Cloth_SupplierSettlementPayments H
        '                                                               ) "
        'If ClsMain.FDivisionNameForCustomization(22) = "W SHYAMA SHYAM FABRICS" Or ClsMain.FDivisionNameForCustomization(27) = "W SHYAMA SHYAM VENTURES LLP" Then
        '    mCondStr = mCondStr & " And LG.DocID Not In ( SELECT H.DocID FROM LedgerHead H LEFT JOIN LedgerHeadDetail L ON H.DocID = L.DocID WHERE H.V_Type In ('WPS','WRS') ) "
        'Else
        '    mCondStr = mCondStr & " And LG.DocID Not In ( SELECT H.DocID FROM LedgerHead H LEFT JOIN LedgerHeadDetail L ON H.DocID = L.DocID WHERE H.V_Type In ('" & Ncat.PaymentSettlement & "','" & Ncat.ReceiptSettlement & "') ) "
        'End If






        CreateCondStr = mCondStr
    End Function


    Public Sub ProcFillReport(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try

            Dim mCondStr$ = ""


            CreateTemporaryTables()




            RepTitle = "Supplier Ledger " & " - " & ReportFrm.FGetText(rowReportType) & " - " & ReportFrm.FGetText(rowGroupOn)






            If mFilterGrid IsNot Nothing And mGridRow IsNot Nothing Then
                If mGridRow.DataGridView.Columns.Contains("Search Code") = True Then
                    If mFilterGrid.Item(GFilter, rowReportType).Value = "Ledger" Or mFilterGrid.Item(GFilter, rowReportType).Value = "Interest Ledger" Then
                    Else
                        ClsMain.FOpenForm(mGridRow.Cells("Search Code").Value, ReportFrm)
                        ReportFrm.FiterGridCopy_Arr.RemoveAt(ReportFrm.FiterGridCopy_Arr.Count - 1)
                        Exit Sub
                    End If
                End If
            End If




            mCondStr = CreateCondStr()



            'FillFifoOutstanding(mCondStr)
            FillPendingBills(mCondStr)



            If DsHeader.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")
            ReportFrm.Text = "Supplier Ledger " & " - " & ReportFrm.FGetText(rowReportType) & " - " & ReportFrm.FGetText(rowGroupOn)

            'ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = "ProcFillReport"

            ReportFrm.ProcFillGrid(DsHeader)
            ReportFrm.DGL1.MultiSelect = False

        Catch ex As Exception
            MsgBox(ex.Message)
            DsHeader = Nothing
        Finally
            'ReportFrm.DGL2.Visible = False
        End Try
    End Sub

    'Private Sub GetDataReadyForFIFOBalance(mCondStr As String)

    '    Dim mFromDate As String
    '    If ReportFrm.FGetText(rowReportType) = "Interest Ledger" Then
    '        mFromDate = ""
    '    Else
    '        mFromDate = ReportFrm.FGetText(rowFromDate)
    '    End If


    '    If mFromDate <> "" Then


    '        Dim mRemainingBalance As Double
    '        Dim i As Integer, j As Integer
    '        Dim dtParty As DataTable

    '        Dim DtMain As DataTable
    '        Dim BalAmount As Double
    '        Dim DrCr As String



    '        mQry = "Select Sg.Subcode , Max(Sg.Nature) as Nature, Sum(Lg.AmtDr)-Sum(Lg.AmtCr) as Balance
    '                        From Ledger Lg "
    '        If ReportFrm.FGetText(rowGroupOn) = "Linked Party" Then
    '            mQry = mQry & " Left Join Subgroup Sg On IfNull(Lg.LinkedSubcode,Lg.Subcode) = Sg.Subcode "
    '        Else
    '            mQry = mQry & " Left Join Subgroup Sg On Lg.Subcode = Sg.Subcode "
    '        End If
    '        mQry = mQry & " Left Join(Select SILTV.Subcode, Max(SILTV.Agent) As Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode) as LTV On Sg.Subcode = LTV.Subcode
    '                        Where 1 = 1 "
    '        mQry = mQry & mCondStr & " And Date(Lg.V_Date) < " & AgL.Chk_Date(mFromDate) & " "
    '        mQry = mQry & " Group By Sg.Subcode"

    '        dtParty = AgL.FillData(mQry, AgL.GCn).Tables(0)

    '        If dtParty.Rows.Count > 0 Then
    '            For i = 0 To dtParty.Rows.Count - 1
    '                mQry = ""
    '                If AgL.XNull(dtParty.Rows(i)("Nature")) = "Customer" Then
    '                    If AgL.VNull(dtParty.Rows(i)("Balance")) > 0 Then
    '                        mQry = "Select Lg.DocID, Lg.DivCode, Lg.Site_Code, Lg.V_Type, Vt.Description as V_TypeDesc, 
    '                                Lg.RecId, SG.Subcode, IfNull(Lg.EffectiveDate, Lg.V_Date) As V_Date, Lg.Narration, Lg.AmtDr as Amount                                
    '                                From Ledger Lg  With (NoLock) "
    '                        If ReportFrm.FGetText(rowGroupOn) = "Linked Party" Then
    '                            mQry = mQry & " Left Join SubGroup SG On SG.Subcode = IfNull(LG.LinkedSubcode,LG.SubCode)   "
    '                        Else
    '                            mQry = mQry & " Left Join SubGroup SG On SG.Subcode =LG.SubCode   "
    '                        End If
    '                        mQry = mQry & " Left Join(Select SILTV.Subcode, Max(SILTV.Agent) As Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode) as LTV On Sg.Subcode = LTV.Subcode
    '                                Left Join Voucher_Type Vt  With (NoLock) On Lg.V_Type = Vt.V_Type
    '                                Where Date(Lg.V_Date) < " & AgL.Chk_Date(mFromDate) & " And Sg.Subcode = '" & AgL.XNull(dtParty.Rows(i)("Subcode")) & "'  And Lg.AmtDr > 0  " & mCondStr & "                               
    '                                Order By IfNull(Lg.EffectiveDate, Lg.V_Date) Desc, Lg.RecId desc"
    '                    End If
    '                Else
    '                    If AgL.VNull(dtParty.Rows(i)("Balance")) < 0 Then
    '                        mQry = "Select Lg.DocID, Lg.DivCode, Lg.Site_Code, Lg.V_Type, Vt.Description as V_TypeDesc, 
    '                                Lg.RecId, LG.Subcode, IfNull(Lg.EffectiveDate, Lg.V_Date) As V_Date, Lg.Narration, Lg.AmtCr as Amount                                
    '                                From Ledger Lg  With (NoLock) "
    '                        If ReportFrm.FGetText(rowGroupOn) = "Linked Party" Then
    '                            mQry = mQry & " Left Join SubGroup SG On SG.Subcode = IfNull(LG.LinkedSubcode,LG.SubCode)   "
    '                        Else
    '                            mQry = mQry & " Left Join SubGroup SG On SG.Subcode =LG.SubCode   "
    '                        End If
    '                        mQry = mQry & " Left Join(Select SILTV.Subcode, Max(SILTV.Agent) As Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode) as LTV On Sg.Subcode = LTV.Subcode
    '                                Left Join Voucher_Type Vt On Lg.V_Type = Vt.V_Type
    '                                Where  Date(Lg.V_Date) < " & AgL.Chk_Date(mFromDate) & " And Lg.Subcode = '" & AgL.XNull(dtParty.Rows(i)("Subcode")) & "'  And Lg.AmtCr > 0 " & mCondStr & " 
    '                                Order By IfNull(Lg.EffectiveDate, Lg.V_Date) Desc, Lg.RecId desc"
    '                    End If
    '                End If


    '                BalAmount = 0 : DrCr = ""
    '                mRemainingBalance = Math.Abs(AgL.VNull(dtParty.Rows(i)("Balance")))
    '                If mQry <> "" Then
    '                    DtMain = AgL.FillData(mQry, AgL.GCn).Tables(0)
    '                    If DtMain.Rows.Count > 0 Then
    '                        For j = 0 To DtMain.Rows.Count - 1

    '                            If mRemainingBalance > 0 Then

    '                                If mRemainingBalance > AgL.VNull(DtMain.Rows(j)("Amount")) Then
    '                                    BalAmount = Format(AgL.VNull(DtMain.Rows(j)("Amount")), "0.00")
    '                                    mRemainingBalance = mRemainingBalance - AgL.VNull(DtMain.Rows(j)("Amount"))
    '                                Else
    '                                    BalAmount = Format(mRemainingBalance, "0.00")
    '                                    mRemainingBalance = mRemainingBalance - mRemainingBalance
    '                                End If
    '                                DrCr = IIf(AgL.VNull(dtParty.Rows(i)("Balance")) > 0, "Dr", "Cr")


    '                                mQry = "Insert Into #FifoOutstanding
    '                                            (DocID, V_Type, RecID, V_Date, 
    '                                            Site_Code, Div_Code, SubCode, BillAmount,
    '                                            BalanceAmount, DrCr, Narration)    
    '                                            Values(" & AgL.Chk_Text(AgL.XNull(DtMain.Rows(j)("DocID"))) & ",
    '                                            " & AgL.Chk_Text(AgL.XNull(DtMain.Rows(j)("V_Type"))) & ",
    '                                            " & AgL.Chk_Text(AgL.XNull(DtMain.Rows(j)("RecID"))) & ",
    '                                            " & AgL.Chk_Date(AgL.XNull(DtMain.Rows(j)("V_Date"))) & ",                                            
    '                                            " & AgL.Chk_Text(AgL.XNull(DtMain.Rows(j)("Site_Code"))) & ",
    '                                            " & AgL.Chk_Text(AgL.XNull(DtMain.Rows(j)("DivCode"))) & ",
    '                                            " & AgL.Chk_Text(AgL.XNull(DtMain.Rows(j)("Subcode"))) & ",
    '                                            " & AgL.VNull(AgL.XNull(DtMain.Rows(j)("Amount"))) & ",
    '                                            " & BalAmount & ",
    '                                            " & AgL.Chk_Text(DrCr) & ",
    '                                            " & AgL.Chk_Text(AgL.XNull(DtMain.Rows(j)("Narration"))) & "
    '                                            )
    '                                            "

    '                                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
    '                            End If
    '                        Next
    '                    End If
    '                End If
    '            Next
    '        End If
    '        mQry = "Select * from #FifoOutstanding"
    '        DtMain = AgL.FillData(mQry, AgL.GCn).Tables(0)

    '        mQry = "Select Null as DocID, Null as V_Type, strftime('%m-%Y', H.V_Date) as RecID, Null as V_Date, 
    '                        Null Site_Code, Null Div_Code, Null As LrNo, H.Subcode, Sg.name || (Case When IfNull(City.CityName,'') <> '' Then ', ' || IfNull(City.CityName,'') else '' End) as PartyName,
    '                City.CityName, Null As Narration, 0 As TaxableAmount, 0 As TaxAmount,
    '                        0 as Addition, 0 as BillAmount, 0 as GoodsReturn, 0 as Payment, Sum(Case When H.DrCr='Dr' Then H.BalanceAmount Else -H.BalanceAmount End) as Adjustment, 
    '                        0 as Balance,Sum(Case When H.DrCr='Dr' Then H.BalanceAmount Else 0 End) as AmtDr, Sum(Case When H.DrCr='Cr' Then H.BalanceAmount Else 0 End) as AmtCr
    '                        From #FifoOutstanding H
    '                        Left Join Subgroup Sg on H.Subcode = Sg.Subcode
    '                        Left Join City On Sg.CityCode = City.CityCode
    '                        Group By H.Subcode, strftime('%m-%Y', H.V_Date)
    '                        Order By strftime('%Y', H.V_Date), strftime('%m', H.V_Date)
    '                        "
    '        DtMain = AgL.FillData(mQry, AgL.GCn).Tables(0)

    '        mQry = "Insert Into #TempRecord 
    '                        (DocID, V_Type, RecId, V_Date, 
    '                        Site_code, Div_Code, SubCode, Narration, GoodsReturn, Payment,
    '                        Adjustment, Balance, AmtDr, AmtCr) 
    '                        Select Null As DocID, Null As V_Type, strftime('%m-%Y', H.V_Date) as RecID, Null as V_Date, 
    '                        Null Site_Code, Null Div_Code, H.Subcode, Null As Narration,  0 as GoodsReturn, 0 as Payment, Sum(Case When H.DrCr='Dr' Then H.BalanceAmount Else -H.BalanceAmount End) as Adjustment, 
    '                        0 as Balance, Sum(Case When H.DrCr='Dr' Then H.BalanceAmount Else 0 End) as AmtDr, Sum(Case When H.DrCr='Cr' Then H.BalanceAmount Else 0 End) as AmtCr
    '                        From #FifoOutstanding H
    '                        Left Join Subgroup Sg on H.Subcode = Sg.Subcode
    '                        Left Join City On Sg.CityCode = City.CityCode
    '                        Group By H.Subcode, strftime('%m-%Y', H.V_Date)
    '                        Order By strftime('%Y', H.V_Date), strftime('%m', H.V_Date)
    '                       "

    '        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
    '        mQry = "Select * from #TempRecord "
    '        DtMain = AgL.FillData(mQry, AgL.GCn).Tables(0)
    '    End If

    'End Sub

    Private Function FillPendingBills(mCondstr As String, Optional Purpose As String = "") As DataSet
        Dim mRemainingBalance As Double
        Dim i As Integer, j As Integer
        Dim dtParty As DataTable

        'Dim DtMain As DataTable
        Dim BalAmount As Double
        Dim DrCr As String
        Dim dtCr As DataTable
        Dim drowCr As DataRow()




        mQry = "Select Sg.Subcode, Max(Sg.Nature) as Nature, Sum(Lg.AmtDr)-Sum(Lg.AmtCr) as Balance
                            From Ledger Lg "
        If ReportFrm.FGetText(rowGroupOn) = "Linked Party" Then
            mQry = mQry & " Left Join Subgroup Sg On IfNull(Lg.LinkedSubcode,Lg.Subcode) = Sg.Subcode "
        Else
            mQry = mQry & " Left Join Subgroup Sg On Lg.Subcode = Sg.Subcode "
        End If
        mQry = mQry & " Left Join Subgroup LS On Lg.LinkedSubcode = Ls.Subcode "
        mQry = mQry & " Left Join(Select SILTV.Subcode, Max(SILTV.Agent) As Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode) as LTV On Sg.Subcode = LTV.Subcode
                                    Where  Sg.Nature='Supplier' "
        mQry = mQry & mCondstr & Replace(ReportFrm.GetWhereCondition("LG.DivCode", rowDivision), "''", "'")
        mQry = mQry & " Group By Sg.Subcode"
        mQry = mQry & " Having Sum(Lg.AmtDr)-Sum(Lg.AmtCr) <> 0 "

        dtParty = AgL.FillData(mQry, AgL.GCn).Tables(0)


        mQry = "Select Lg.DocID, Lg.Div_Code as DivCode, Lg.Site_Code, Lg.V_Type, Vt.Description as V_TypeDesc, 
                                    Lg.ManualRefNo as RecId, SG.Subcode, Lg.V_Date As V_Date, 
                                    Lg.Remarks as Narration, Lg.Net_Amount as BillAmount, 
                                    IfNull(PR.NetAmount,0) as PurchaseReturn, Lg.Net_Amount-IfNull(Abs(PR.NetAmount),0) as Amount                                
                                    From PurchInvoice Lg  With (NoLock) "
        If ReportFrm.FGetText(rowGroupOn) = "Linked Party" Then
            mQry = mQry & " Left Join SubGroup SG On SG.Subcode =IfNull(Lg.BillToParty,LG.Vendor) "
        Else
            mQry = mQry & " Left Join SubGroup SG On SG.Subcode =LG.Vendor "
        End If

        mQry = mQry & " Left Join (                                    Select ReferenceDocId, Sum(NetAmount) as NetAmount From (

                                    Select PID.ReferenceDocID, PID.Net_Amount as NetAmount
                                           From PurchInvoiceDetail PID
                                           Left Join PurchInvoice PI On PID.DocID = PI.DocId
                                           Where PI.V_Type = 'PR'
                                           And PI.DocID Not in (Select PurchaseInvoiceDocID from Cloth_SupplierSettlementInvoices Union all Select PaymentDocId from Cloth_SupplierSettlementPayments)

                                    union all

                                    select DCND.SpecificationDocId as ReferenceDocId, DCNDC.Net_Amount as NetAmount
                                    From LedgerHead DCN
                                    Left Join LedgerHeadDetail DCND On DCN.DocID = DCND.DocID
                                    Left Join LedgerHeadDetailCharges DCNDC On DCND.DocId = DCNDC.DocID And DCND.Sr = DCNDC.Sr
                                    where dcn.V_type In ('CNS','DNS') And IfNull(DCND.SpecificationDocId ,'') <> ''
                                    And DCN.DocID Not in (Select PurchaseInvoiceDocID from Cloth_SupplierSettlementInvoices Union all Select PaymentDocId from Cloth_SupplierSettlementPayments)
                                    ) as X group by X.ReferenceDocID
                                   ) as PR On LG.DocID = PR.ReferenceDocID 

                        Left Join(Select SILTV.Subcode, Max(SILTV.Agent) As Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode) as LTV On Sg.Subcode = LTV.Subcode
                                    Left Join Voucher_Type Vt On Lg.V_Type = Vt.V_Type
                                    Where Sg.SubgroupType = 'Supplier' And Lg.Net_Amount > 0   And 1 = (Case When VT.Ncat<>'" & Ncat.JournalVoucher & "' Then 1 When VT.NCat = '" & Ncat.JournalVoucher & "' And Sg.SubgroupType Not In ('Supplier') Then 1 Else 0  End) 
                                    And Lg.DocID Not in (Select PurchaseInvoiceDocID from Cloth_SupplierSettlementInvoices)
                                    " & mCondstr & Replace(ReportFrm.GetWhereCondition("LG.Div_Code", rowDivision), "''", "'") & "                                     
                                    "



        mQry = mQry & " Union all Select Lg.DocID, Lg.DivCode as DivCode, Lg.Site_Code, Lg.V_Type, Vt.Description as V_TypeDesc, 
                                    Lg.RecId as RecId, SG.Subcode, Lg.V_Date As V_Date, 
                                    Lg.Narration as Narration, Lg.AmtCr as BillAmount, 
                                    0 as PurchaseReturn, Lg.AmtCr as Amount                                
                                    From Ledger Lg  With (NoLock) "
        If ReportFrm.FGetText(rowGroupOn) = "Linked Party" Then
            mQry = mQry & " Left Join SubGroup SG On SG.Subcode =IfNull(Lg.BillToParty,LG.Vendor) "
        Else
            mQry = mQry & " Left Join SubGroup SG On SG.Subcode =LG.Subcode "
        End If

        mQry = mQry & " Left Join (
                                    Select ReferenceDocId, Sum(NetAmount) as NetAmount From (

                                    Select PID.ReferenceDocID, PID.Net_Amount as NetAmount
                                           From PurchInvoiceDetail PID
                                           Left Join PurchInvoice PI On PID.DocID = PI.DocId
                                           Where PI.V_Type = 'PR'
                                           And PI.DocID Not in (Select PurchaseInvoiceDocID from Cloth_SupplierSettlementInvoices Union all Select PaymentDocId from Cloth_SupplierSettlementPayments)

                                    union all

                                    select DCND.SpecificationDocId as ReferenceDocId, DCNDC.Net_Amount as NetAmount
                                    From LedgerHead DCN
                                    Left Join LedgerHeadDetail DCND On DCN.DocID = DCND.DocID
                                    Left Join LedgerHeadDetailCharges DCNDC On DCND.DocId = DCNDC.DocID And DCND.Sr = DCNDC.Sr
                                    where dcn.V_type In ('CNS','DNS') And IfNull(DCND.SpecificationDocId ,'') <> ''
                                    And DCN.DocID Not in (Select PurchaseInvoiceDocID from Cloth_SupplierSettlementInvoices Union all Select PaymentDocId from Cloth_SupplierSettlementPayments)
                                    ) as X group by X.ReferenceDocID) as PR On LG.DocID = PR.ReferenceDocID 

                        Left Join(Select SILTV.Subcode, Max(SILTV.Agent) As Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode) as LTV On Sg.Subcode = LTV.Subcode
                                    Left Join Voucher_Type Vt On Lg.V_Type = Vt.V_Type
                                    Where Sg.SubgroupType <> 'Supplier' And Lg.AmtCr > 0   
                                    And Lg.DocID Not in (Select PurchaseInvoiceDocID from Cloth_SupplierSettlementInvoices)
                                    " & mCondstr & Replace(ReportFrm.GetWhereCondition("LG.DivCode", rowDivision), "''", "'") & "                                     
                                    "

        dtCr = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mCondstr = mCondstr & Replace(ReportFrm.GetWhereCondition("LG.Div_Code", rowDivision), "''", "'")
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
                                    And Lg.AmtDr > 0  " & mCondstr & Replace(ReportFrm.GetWhereCondition("LG.Div_Code", rowDivision), "''", "'") & "                               
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
                                                " & Val(Math.Abs(AgL.VNull(dtParty.Rows(i)("Balance")))) & ",
                                                'Cr',
                                                Null
                                                )
                                                "
                            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
                            mQry = ""
                        End If
                    End If
                Else
                    If AgL.VNull(dtParty.Rows(i)("Balance")) < 0 Then
                        mQry = "Select Lg.DocID, Lg.DivCode, Lg.Site_Code, Lg.V_Type, Vt.Description as V_TypeDesc, 
                                    Lg.RecId, SG.Subcode, Lg.V_Date As V_Date, Lg.Narration, Lg.AmtCr as BillAmount, 
                                    IfNull(PR.NetAmount,0) as PurchaseReturn,
                                    Lg.AmtCr - IfNull(PR.NetAmount,0)   as Amount                                
                                    From Ledger Lg  With (NoLock) "
                        If ReportFrm.FGetText(rowGroupOn) = "Linked Party" Then
                            mQry = mQry & " Left Join SubGroup SG On SG.Subcode =IfNull(Lg.LinkedSubcode,LG.SubCode) "
                        Else
                            mQry = mQry & " Left Join SubGroup SG On SG.Subcode =LG.SubCode "
                        End If

                        mQry = mQry & "
                                    Left Join (Select PID.DocID, Sum(PID.Net_Amount) as NetAmount
                                               From PurchInvoiceDetail PID
                                               Left Join PurchInvoice PI On PID.DocID = PI.DocId
                                               Where PI.V_Type = 'PR' And PI.DocId Not In (SELECT PaymentDocID FROM Cloth_SupplierSettlementPayments)
                                               Group By PID.DocID) as PR On LG.DocID = PR.DocID 
                                    Left Join(Select SILTV.Subcode, Max(SILTV.Agent) As Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode) as LTV On Sg.Subcode = LTV.Subcode
                                    Left Join Voucher_Type Vt On Lg.V_Type = Vt.V_Type
                                    Where Sg.Subcode = '" & AgL.XNull(dtParty.Rows(i)("Subcode")) & "'  And Lg.AmtCr > 0 " & mCondstr & " 
                                    Order By IfNull(Lg.EffectiveDate, Lg.V_Date) Desc, Lg.RecId desc"
                    Else
                        mQry = ""
                    End If
                End If


                BalAmount = 0 : DrCr = ""
                mRemainingBalance = Math.Abs(AgL.VNull(dtParty.Rows(i)("Balance")))
                If mQry <> "" Then
                    'DtMain = AgL.FillData(mQry, AgL.GCn).Tables(0)
                    drowCr = dtCr.Select("Subcode = '" & AgL.XNull(dtParty.Rows(i)("Subcode")) & "'", " V_Date, RecID ")
                    If drowCr.Length > 0 Then
                        For j = 0 To drowCr.Length - 1

                            If mRemainingBalance > 0 Then

                                If mRemainingBalance > AgL.VNull(drowCr(j)("Amount")) Then
                                    BalAmount = Format(AgL.VNull(drowCr(j)("Amount")), "0.00")
                                    mRemainingBalance = mRemainingBalance - AgL.VNull(drowCr(j)("Amount"))
                                Else
                                    BalAmount = Format(mRemainingBalance, "0.00")
                                    mRemainingBalance = mRemainingBalance - mRemainingBalance
                                End If
                                DrCr = IIf(AgL.VNull(dtParty.Rows(i)("Balance")) > 0, "Dr", "Cr")


                                mQry = "Insert Into #FifoOutstanding
                                                (DocID, V_Type, RecID, V_Date, 
                                                Site_Code, Div_Code, SubCode, BillAmount,PurchaseReturn,
                                                BalanceAmount, DrCr, Narration)    
                                                Values(" & AgL.Chk_Text(AgL.XNull(drowCr(j)("DocID"))) & ",
                                                " & AgL.Chk_Text(AgL.XNull(drowCr(j)("V_Type"))) & ",
                                                " & AgL.Chk_Text(AgL.XNull(drowCr(j)("RecID"))) & ",
                                                " & AgL.Chk_Date(AgL.XNull(drowCr(j)("V_Date"))) & ",                                            
                                                " & AgL.Chk_Text(AgL.XNull(drowCr(j)("Site_Code"))) & ",
                                                " & AgL.Chk_Text(AgL.XNull(drowCr(j)("DivCode"))) & ",
                                                " & AgL.Chk_Text(AgL.XNull(drowCr(j)("Subcode"))) & ",
                                                " & AgL.Chk_Text(AgL.XNull(drowCr(j)("BillAmount"))) & ",
                                                " & AgL.Chk_Text(AgL.XNull(drowCr(j)("PurchaseReturn"))) & ",
                                                " & BalAmount & ",
                                                " & AgL.Chk_Text(DrCr) & ",
                                                " & AgL.Chk_Text(AgL.XNull(drowCr(j)("Narration"))) & "
                                                )
                                                "

                                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
                            End If
                        Next
                    End If
                End If
            Next
        End If



        mQry = "Select 1 as Sr,  'þ' As Tick, '' as SearchCode,'' as EntryNo, Null as ActEntryDate, null as EntryDate, 
                '' as Site, '' MasterPartyName,  0.00 BillAmount, 0.00 PurchaseReturn, null  as DueDate, 
                '' BankName,  '' PartyName, 'XXXX' BankAccount, 'XXXX' Ifsc, 0.00 BalanceAmount, 
                '' Remarks, Null as ActDueDate "

        mQry += "Union All Select 2 as Sr,  'o' As Tick, H.DocID as SearchCode,H.V_Type || '-' || H.RecID as EntryNo, H.V_Date as ActEntryDate, strftime('%d-%m-%Y',H.V_Date) as EntryDate, 
                S.Name as Site, bSg.Name as MasterPartyName,  H.BillAmount, H.PurchaseReturn, strftime('%d-%m-%Y',Date(H.V_Date,'+' || IfNull(P.CreditDays,0) || ' Day'))  as DueDate, BA.BankName,  Sg.DispName as PartyName, BA.BankAccount, BA.BankIfsc as Ifsc, H.BalanceAmount, PI.VendorDocNo Remarks, Date(H.V_Date,'+' || IfNull(P.CreditDays,0) || ' Day') as ActDueDate
                from #FifoOutstanding H
                Left Join SiteMast S On H.Site_Code = S.Code
                Left Join Subgroup Sg On H.Subcode = Sg.Subcode 
                Left Join SubgroupBankAccount BA On Sg.Subcode = BA.Subcode and BA.Sr=0
                Left Join viewHelpSubgroup bsg on Sg.Parent = bsg.code
                Left Join Subgroup P On Sg.Parent = P.Subcode 
                Left Join PurchInvoice PI On H.DocID = PI.DocId
                Where Date(H.V_Date,'+' || IfNull(P.CreditDays,0) || ' Day') <= " & AgL.Chk_Date(AgL.PubLoginDate) & "
                And H.BalanceAmount>0
                Order By ActDueDate "
        'Where Date(H.V_Date,'+' || IfNull(P.CreditDays,0) || ' Day') <= " & AgL.Chk_Date(AgL.PubLoginDate) & " 
        mQry = " Select  Tick, SearchCode,EntryNo, EntryDate, 
                Site, MasterPartyName,  BillAmount, PurchaseReturn, DueDate, 
                BankName,  PartyName, BankAccount, Ifsc, BalanceAmount, 
                Remarks 
                From (" & mQry & ") as X Order By X.ActEntryDate, X.ActDueDate "

        DsHeader = AgL.FillData(mQry, AgL.GCn)

        ''''Date(H.V_Date,'+' || IfNull(P.CreditDays,0) || ' Day') <= " & AgL.Chk_Date(AgL.PubLoginDate) & "

        'mQry = "Select Null as DocID, Null as V_Type, strftime('%m-%Y', H.V_Date) as RecID, Null as V_Date, 
        '                    Null Site_Code, Null Div_Code, Null As LrNo, H.Subcode, Sg.name || (Case When IfNull(City.CityName,'') <> '' Then ', ' || IfNull(City.CityName,'') else '' End) as PartyName,
        '            City.CityName, Null As Narration, 0 As TaxableAmount, 0 As TaxAmount,
        '                    0 as Addition, 0 as BillAmount, 0 as GoodsReturn, 0 as Payment, Sum(Case When H.DrCr='Dr' Then H.BalanceAmount Else -H.BalanceAmount End) as Adjustment, 
        '                    0 as Balance,Sum(Case When H.DrCr='Dr' Then H.BalanceAmount Else 0 End) as AmtDr, Sum(Case When H.DrCr='Cr' Then H.BalanceAmount Else 0 End) as AmtCr
        '                    From #FifoOutstanding H
        '                    Left Join Subgroup Sg on H.Subcode = Sg.Subcode
        '                    Left Join City On Sg.CityCode = City.CityCode
        '                    Group By H.Subcode, strftime('%m-%Y', H.V_Date)
        '                    Order By strftime('%Y', H.V_Date), strftime('%m', H.V_Date)
        '                    "
        'DsHeader = AgL.FillData(mQry, AgL.GCn)

        'Dim CurrentMonth As Date = CDate(AgL.PubLoginDate)
        'Dim OneMonthBack As Date = DateAdd(DateInterval.Month, -1, CDate(AgL.PubLoginDate))
        'Dim TwoMonthBack As Date = DateAdd(DateInterval.Month, -2, CDate(AgL.PubLoginDate))
        'Dim ThreeMonthBack As Date = DateAdd(DateInterval.Month, -3, CDate(AgL.PubLoginDate))
        'Dim FourMonthBack As Date = DateAdd(DateInterval.Month, -4, CDate(AgL.PubLoginDate))
        'Dim FiveMonthBack As Date = DateAdd(DateInterval.Month, -5, CDate(AgL.PubLoginDate))
        'Dim SixMonthBack As Date = DateAdd(DateInterval.Month, -6, CDate(AgL.PubLoginDate))
        'Dim SevenMonthBack As Date = DateAdd(DateInterval.Month, -7, CDate(AgL.PubLoginDate))
        'Dim EightMonthBack As Date = DateAdd(DateInterval.Month, -8, CDate(AgL.PubLoginDate))
        'Dim NineMonthBack As Date = DateAdd(DateInterval.Month, -9, CDate(AgL.PubLoginDate))

        'If Purpose = "" Then

        '    mQry = "Select H.Subcode as SearchCode, Sg.name || (Case When IfNull(City.CityName,'') <> '' Then ', ' || IfNull(City.CityName,'') else '' End) as Party,                            
        '                    Sum(CASE WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(CurrentMonth) & ") Then H.BalanceAmount ELSE 0 END ) AS [" & CurrentMonth.ToString("MMM-yy").Replace("-", " ") & "],
        '                    Sum(CASE WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(OneMonthBack) & ") Then H.BalanceAmount ELSE 0 END ) AS [" & OneMonthBack.ToString("MMM-yy").Replace("-", " ") & "],
        '                    Sum(CASE WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(TwoMonthBack) & ") Then H.BalanceAmount ELSE 0 END ) AS [" & TwoMonthBack.ToString("MMM-yy").Replace("-", " ") & "],
        '                    Sum(CASE WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(ThreeMonthBack) & ") Then H.BalanceAmount ELSE 0 END ) AS [" & ThreeMonthBack.ToString("MMM-yy").Replace("-", " ") & "],
        '                    Sum(CASE WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(FourMonthBack) & ") Then H.BalanceAmount ELSE 0 END ) AS [" & FourMonthBack.ToString("MMM-yy").Replace("-", " ") & "],
        '                    Sum(CASE WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(FiveMonthBack) & ") Then H.BalanceAmount ELSE 0 END ) AS [" & FiveMonthBack.ToString("MMM-yy").Replace("-", " ") & "],
        '                    Sum(CASE WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(SixMonthBack) & ") Then H.BalanceAmount ELSE 0 END ) AS [" & SixMonthBack.ToString("MMM-yy").Replace("-", " ") & "],
        '                    Sum(CASE WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(SevenMonthBack) & ") Then H.BalanceAmount ELSE 0 END ) AS [" & SevenMonthBack.ToString("MMM-yy").Replace("-", " ") & "],
        '                    Sum(CASE WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(EightMonthBack) & ") Then H.BalanceAmount ELSE 0 END ) AS [" & EightMonthBack.ToString("MMM-yy").Replace("-", " ") & "],
        '                    Sum(CASE WHEN strftime('%Y%m', H.V_Date) < strftime('%Y%m', " & AgL.Chk_Date(EightMonthBack) & ") Then H.BalanceAmount ELSE 0 END ) AS [Before " & EightMonthBack.ToString("MMM-yy").Replace("-", " ") & "],
        '                    Sum(H.BalanceAmount) As [Balance],
        '                    Sum(H.DrCr) As [DrCr]                                                                                                                
        '                    From #FifoOutstanding H
        '                    Left Join Subgroup Sg on H.Subcode = Sg.Subcode
        '                    Left Join City On Sg.CityCode = City.CityCode
        '                    Group By H.Subcode, Sg.name || (Case When IfNull(City.CityName,'') <> '' Then ', ' || IfNull(City.CityName,'') else '' End)
        '                    Order By Sg.name || (Case When IfNull(City.CityName,'') <> '' Then ', ' || IfNull(City.CityName,'') else '' End)
        '                    "

        '    DsHeader = AgL.FillData(mQry, AgL.GCn)
        '    'FillFifoOutstanding = DsHeader
        'Else

        '    Dim mMultiplier As Double
        '    If ClsMain.FDivisionNameForCustomization(22) = "W SHYAMA SHYAM FABRICS" Or ClsMain.FDivisionNameForCustomization(27) = "W SHYAMA SHYAM VENTURES LLP" Then
        '        mMultiplier = 0.01
        '    Else
        '        mMultiplier = 1.0
        '    End If

        '    mQry = "Select H.Subcode as SearchCode, Sg.name || (Case When IfNull(City.CityName,'') <> '' Then ', ' || IfNull(City.CityName,'') else '' End) as Party,                            
        '                    Sum(H.BalanceAmount) * " & mMultiplier & " as BalanceAmount, 

        '                    (CASE WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(CurrentMonth) & ") Then " & AgL.Chk_Date(CurrentMonth) & "
        '                        WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(OneMonthBack) & ") Then " & AgL.Chk_Date(OneMonthBack) & "
        '                        WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(TwoMonthBack) & ") Then " & AgL.Chk_Date(TwoMonthBack) & "
        '                        WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(ThreeMonthBack) & ") Then " & AgL.Chk_Date(ThreeMonthBack) & "
        '                        WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(FourMonthBack) & ") Then " & AgL.Chk_Date(FourMonthBack) & "
        '                        WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(FiveMonthBack) & ") Then " & AgL.Chk_Date(FiveMonthBack) & "
        '                        WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(SixMonthBack) & ") Then " & AgL.Chk_Date(SixMonthBack) & "
        '                        WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(SevenMonthBack) & ") Then " & AgL.Chk_Date(SevenMonthBack) & "
        '                        WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(EightMonthBack) & ") Then " & AgL.Chk_Date(EightMonthBack) & "
        '                        WHEN strftime('%Y%m', H.V_Date) < strftime('%Y%m', " & AgL.Chk_Date(EightMonthBack) & ") Then " & AgL.Chk_Date(NineMonthBack) & "
        '                     Else Null End) as BalanceMonth   
        '                    From #FifoOutstanding H
        '                    Left Join Subgroup Sg on H.Subcode = Sg.Subcode
        '                    Left Join City On Sg.CityCode = City.CityCode
        '                    Group By H.Subcode, Sg.name || (Case When IfNull(City.CityName,'') <> '' Then ', ' || IfNull(City.CityName,'') else '' End),
        '                    (CASE WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(CurrentMonth) & ") Then " & AgL.Chk_Date(CurrentMonth) & "
        '                        WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(OneMonthBack) & ") Then " & AgL.Chk_Date(OneMonthBack) & "
        '                        WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(TwoMonthBack) & ") Then " & AgL.Chk_Date(TwoMonthBack) & "
        '                        WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(ThreeMonthBack) & ") Then " & AgL.Chk_Date(ThreeMonthBack) & "
        '                        WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(FourMonthBack) & ") Then " & AgL.Chk_Date(FourMonthBack) & "
        '                        WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(FiveMonthBack) & ") Then " & AgL.Chk_Date(FiveMonthBack) & "
        '                        WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(SixMonthBack) & ") Then " & AgL.Chk_Date(SixMonthBack) & "
        '                        WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(SevenMonthBack) & ") Then " & AgL.Chk_Date(SevenMonthBack) & "
        '                        WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(EightMonthBack) & ") Then " & AgL.Chk_Date(EightMonthBack) & "
        '                        WHEN strftime('%Y%m', H.V_Date) < strftime('%Y%m', " & AgL.Chk_Date(EightMonthBack) & ") Then " & AgL.Chk_Date(NineMonthBack) & "
        '                     Else Null End)
        '                    Order By Sg.name || (Case When IfNull(City.CityName,'') <> '' Then ', ' || IfNull(City.CityName,'') else '' End), H.V_Date
        '            "
        '    FillFifoOutstanding = AgL.FillData(mQry, AgL.GCn)
        'End If
    End Function


    Private Function FillFifoOutstanding(mCondstr As String, Optional Purpose As String = "") As DataSet
        Dim mRemainingBalance As Double
        Dim i As Integer, j As Integer
        Dim dtParty As DataTable

        'Dim DtMain As DataTable
        Dim BalAmount As Double
        Dim DrCr As String
        Dim dtCr As DataTable
        Dim drowCr As DataRow()




        mQry = "Select Sg.Subcode, Max(Sg.Nature) as Nature, Sum(Lg.AmtDr)-Sum(Lg.AmtCr) as Balance
                            From Ledger Lg "
        If ReportFrm.FGetText(rowGroupOn) = "Linked Party" Then
            mQry = mQry & " Left Join Subgroup Sg On IfNull(Lg.LinkedSubcode,Lg.Subcode) = Sg.Subcode "
        Else
            mQry = mQry & " Left Join Subgroup Sg On Lg.Subcode = Sg.Subcode "
        End If
        mQry = mQry & " Left Join Subgroup LS On Lg.LinkedSubcode = Ls.Subcode "
        mQry = mQry & " Left Join(Select SILTV.Subcode, Max(SILTV.Agent) As Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode) as LTV On Sg.Subcode = LTV.Subcode
                                    Where  Sg.Nature='Supplier' "
        mQry = mQry & mCondstr
        mQry = mQry & " AND  Date(LG.V_Date)  <= (Case 
                                                    When Sg.Nature='Customer' And Lg.AmtDr>0 Then " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowBillsUptoDate)).ToString("s")) & " 
                                                    When Sg.Nature='Customer' And Lg.AmtCr>0 Then " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowPaymentsUptoDate)).ToString("s")) & " 
                                                    When Sg.Nature<>'Customer' And Lg.AmtDr>0 Then " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowPaymentsUptoDate)).ToString("s")) & " 
                                                    When Sg.Nature<>'Customer' And Lg.AmtCr>0 Then " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowBillsUptoDate)).ToString("s")) & " 
                                                    End) 
                        And LG.V_Type <> 'PR' "
        mQry = mQry & " Group By Sg.Subcode"
        mQry = mQry & " Having Sum(Lg.AmtDr)-Sum(Lg.AmtCr) <> 0 "




        dtParty = AgL.FillData(mQry, AgL.GCn).Tables(0)


        mQry = "Select Lg.DocID, Lg.DivCode, Lg.Site_Code, Lg.V_Type, Vt.Description as V_TypeDesc, 
                                    Lg.RecId, SG.Subcode, Lg.V_Date As V_Date, Lg.Narration, Lg.AmtCr as BillAmount, 
                                    IfNull(PR.NetAmount,0) as PurchaseReturn, Lg.AmtCr-IfNull(Abs(PR.NetAmount),0) as Amount                                
                                    From Ledger Lg  With (NoLock) "
        If ReportFrm.FGetText(rowGroupOn) = "Linked Party" Then
            mQry = mQry & " Left Join SubGroup SG On SG.Subcode =IfNull(Lg.LinkedSubcode,LG.SubCode) "
        Else
            mQry = mQry & " Left Join SubGroup SG On SG.Subcode =LG.SubCode "
        End If

        mQry = mQry & " Left Join (Select PID.ReferenceDocID, Sum(PID.Net_Amount) as NetAmount
                                               From PurchInvoiceDetail PID
                                               Left Join PurchInvoice PI On PID.DocID = PI.DocId
                                               Where PI.V_Type = 'PR'
                                               Group By PID.ReferenceDocID) as PR On LG.DocID = PR.ReferenceDocID 

                        Left Join(Select SILTV.Subcode, Max(SILTV.Agent) As Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode) as LTV On Sg.Subcode = LTV.Subcode
                                    Left Join Voucher_Type Vt On Lg.V_Type = Vt.V_Type
                                    Where Lg.AmtCr > 0   And 1 = (Case When VT.Ncat<>'" & Ncat.JournalVoucher & "' Then 1 When VT.NCat = '" & Ncat.JournalVoucher & "' And Sg.SubgroupType Not In ('Supplier') Then 1 Else 0  End) " & mCondstr & " 
                                    Order By Sg.Subcode, IfNull(Lg.EffectiveDate, Lg.V_Date) Desc, Lg.RecId desc"
        dtCr = AgL.FillData(mQry, AgL.GCn).Tables(0)


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
                                                " & Val(Math.Abs(AgL.VNull(dtParty.Rows(i)("Balance")))) & ",
                                                'Cr',
                                                Null
                                                )
                                                "
                            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
                            mQry = ""
                        End If
                    End If
                Else
                    If AgL.VNull(dtParty.Rows(i)("Balance")) < 0 Then
                        mQry = "Select Lg.DocID, Lg.DivCode, Lg.Site_Code, Lg.V_Type, Vt.Description as V_TypeDesc, 
                                    Lg.RecId, SG.Subcode, Lg.V_Date As V_Date, Lg.Narration, Lg.AmtCr as BillAmount, 
                                    IfNull(PR.NetAmount,0) as PurchaseReturn,
                                    Lg.AmtCr - IfNull(PR.NetAmount,0)   as Amount                                
                                    From Ledger Lg  With (NoLock) "
                        If ReportFrm.FGetText(rowGroupOn) = "Linked Party" Then
                            mQry = mQry & " Left Join SubGroup SG On SG.Subcode =IfNull(Lg.LinkedSubcode,LG.SubCode) "
                        Else
                            mQry = mQry & " Left Join SubGroup SG On SG.Subcode =LG.SubCode "
                        End If

                        mQry = mQry & "
                                    Left Join (Select PID.DocID, Sum(PID.Net_Amount) as NetAmount
                                               From PurchInvoiceDetail PID
                                               Left Join PurchInvoice PI On PID.DocID = PI.DocId
                                               Where PI.V_Type = 'PR' And PI.DocId Not In (SELECT PaymentDocID FROM Cloth_SupplierSettlementPayments)
                                               Group By PID.DocID) as PR On LG.DocID = PR.DocID 
                                    Left Join(Select SILTV.Subcode, Max(SILTV.Agent) As Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode) as LTV On Sg.Subcode = LTV.Subcode
                                    Left Join Voucher_Type Vt On Lg.V_Type = Vt.V_Type
                                    Where Sg.Subcode = '" & AgL.XNull(dtParty.Rows(i)("Subcode")) & "'  And Lg.AmtCr > 0 " & mCondstr & " 
                                    Order By IfNull(Lg.EffectiveDate, Lg.V_Date) Desc, Lg.RecId desc"
                    Else
                        mQry = ""
                    End If
                End If


                BalAmount = 0 : DrCr = ""
                mRemainingBalance = Math.Abs(AgL.VNull(dtParty.Rows(i)("Balance")))
                If mQry <> "" Then
                    'DtMain = AgL.FillData(mQry, AgL.GCn).Tables(0)
                    drowCr = dtCr.Select("Subcode = '" & AgL.XNull(dtParty.Rows(i)("Subcode")) & "'", " V_Date Desc, RecID ")
                    If drowCr.Length > 0 Then
                        For j = 0 To drowCr.Length - 1

                            If mRemainingBalance > 0 Then

                                If mRemainingBalance > AgL.VNull(drowCr(j)("Amount")) Then
                                    BalAmount = Format(AgL.VNull(drowCr(j)("Amount")), "0.00")
                                    mRemainingBalance = mRemainingBalance - AgL.VNull(drowCr(j)("Amount"))
                                Else
                                    BalAmount = Format(mRemainingBalance, "0.00")
                                    mRemainingBalance = mRemainingBalance - mRemainingBalance
                                End If
                                DrCr = IIf(AgL.VNull(dtParty.Rows(i)("Balance")) > 0, "Dr", "Cr")


                                mQry = "Insert Into #FifoOutstanding
                                                (DocID, V_Type, RecID, V_Date, 
                                                Site_Code, Div_Code, SubCode, BillAmount,PurchaseReturn,
                                                BalanceAmount, DrCr, Narration)    
                                                Values(" & AgL.Chk_Text(AgL.XNull(drowCr(j)("DocID"))) & ",
                                                " & AgL.Chk_Text(AgL.XNull(drowCr(j)("V_Type"))) & ",
                                                " & AgL.Chk_Text(AgL.XNull(drowCr(j)("RecID"))) & ",
                                                " & AgL.Chk_Date(AgL.XNull(drowCr(j)("V_Date"))) & ",                                            
                                                " & AgL.Chk_Text(AgL.XNull(drowCr(j)("Site_Code"))) & ",
                                                " & AgL.Chk_Text(AgL.XNull(drowCr(j)("DivCode"))) & ",
                                                " & AgL.Chk_Text(AgL.XNull(drowCr(j)("Subcode"))) & ",
                                                " & AgL.Chk_Text(AgL.XNull(drowCr(j)("BillAmount"))) & ",
                                                " & AgL.Chk_Text(AgL.XNull(drowCr(j)("PurchaseReturn"))) & ",
                                                " & BalAmount & ",
                                                " & AgL.Chk_Text(DrCr) & ",
                                                " & AgL.Chk_Text(AgL.XNull(drowCr(j)("Narration"))) & "
                                                )
                                                "

                                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
                            End If
                        Next
                    End If
                End If
            Next
        End If



        mQry = "Select 1 as Sr,  'þ' As Tick, '' as SearchCode,'' as EntryNo, Null as ActEntryDate, null as EntryDate, 
                '' as Site, '' MasterPartyName,  0.00 BillAmount, 0.00 PurchaseReturn, null  as DueDate, 
                '' BankName,  '' PartyName, 'XXXX' BankAccount, 'XXXX' Ifsc, 0.00 BalanceAmount, 
                '' Remarks, Null as ActDueDate "

        mQry += "Union All Select 2 as Sr,  'o' As Tick, H.DocID as SearchCode,H.V_Type || '-' || H.RecID as EntryNo, H.V_Date as ActEntryDate, strftime('%d-%m-%Y',H.V_Date) as EntryDate, 
                S.Name as Site, bSg.Name as MasterPartyName,  H.BillAmount, H.PurchaseReturn, strftime('%d-%m-%Y',Date(H.V_Date,'+' || IfNull(P.CreditDays,0) || ' Day'))  as DueDate, BA.BankName,  Sg.DispName as PartyName, BA.BankAccount, BA.BankIfsc as Ifsc, H.BalanceAmount, PI.VendorDocNo Remarks, Date(H.V_Date,'+' || IfNull(P.CreditDays,0) || ' Day') as ActDueDate
                from #FifoOutstanding H
                Left Join SiteMast S On H.Site_Code = S.Code
                Left Join Subgroup Sg On H.Subcode = Sg.Subcode 
                Left Join SubgroupBankAccount BA On Sg.Subcode = BA.Subcode and BA.Sr=0
                Left Join viewHelpSubgroup bsg on Sg.Parent = bsg.code
                Left Join Subgroup P On Sg.Parent = P.Subcode 
                Left Join PurchInvoice PI On H.DocID = PI.DocId
                Where Date(H.V_Date,'+' || IfNull(P.CreditDays,0) || ' Day') <= " & AgL.Chk_Date(AgL.PubLoginDate) & "
                And H.BalanceAmount>0
                Order By ActDueDate "
        'Where Date(H.V_Date,'+' || IfNull(P.CreditDays,0) || ' Day') <= " & AgL.Chk_Date(AgL.PubLoginDate) & " 
        mQry = " Select  Tick, SearchCode,EntryNo, EntryDate, 
                Site, MasterPartyName,  BillAmount, PurchaseReturn, DueDate, 
                BankName,  PartyName, BankAccount, Ifsc, BalanceAmount, 
                Remarks 
                From (" & mQry & ") as X Order By X.ActEntryDate, X.ActDueDate "

        DsHeader = AgL.FillData(mQry, AgL.GCn)

        ''''Date(H.V_Date,'+' || IfNull(P.CreditDays,0) || ' Day') <= " & AgL.Chk_Date(AgL.PubLoginDate) & "

        'mQry = "Select Null as DocID, Null as V_Type, strftime('%m-%Y', H.V_Date) as RecID, Null as V_Date, 
        '                    Null Site_Code, Null Div_Code, Null As LrNo, H.Subcode, Sg.name || (Case When IfNull(City.CityName,'') <> '' Then ', ' || IfNull(City.CityName,'') else '' End) as PartyName,
        '            City.CityName, Null As Narration, 0 As TaxableAmount, 0 As TaxAmount,
        '                    0 as Addition, 0 as BillAmount, 0 as GoodsReturn, 0 as Payment, Sum(Case When H.DrCr='Dr' Then H.BalanceAmount Else -H.BalanceAmount End) as Adjustment, 
        '                    0 as Balance,Sum(Case When H.DrCr='Dr' Then H.BalanceAmount Else 0 End) as AmtDr, Sum(Case When H.DrCr='Cr' Then H.BalanceAmount Else 0 End) as AmtCr
        '                    From #FifoOutstanding H
        '                    Left Join Subgroup Sg on H.Subcode = Sg.Subcode
        '                    Left Join City On Sg.CityCode = City.CityCode
        '                    Group By H.Subcode, strftime('%m-%Y', H.V_Date)
        '                    Order By strftime('%Y', H.V_Date), strftime('%m', H.V_Date)
        '                    "
        'DsHeader = AgL.FillData(mQry, AgL.GCn)

        'Dim CurrentMonth As Date = CDate(AgL.PubLoginDate)
        'Dim OneMonthBack As Date = DateAdd(DateInterval.Month, -1, CDate(AgL.PubLoginDate))
        'Dim TwoMonthBack As Date = DateAdd(DateInterval.Month, -2, CDate(AgL.PubLoginDate))
        'Dim ThreeMonthBack As Date = DateAdd(DateInterval.Month, -3, CDate(AgL.PubLoginDate))
        'Dim FourMonthBack As Date = DateAdd(DateInterval.Month, -4, CDate(AgL.PubLoginDate))
        'Dim FiveMonthBack As Date = DateAdd(DateInterval.Month, -5, CDate(AgL.PubLoginDate))
        'Dim SixMonthBack As Date = DateAdd(DateInterval.Month, -6, CDate(AgL.PubLoginDate))
        'Dim SevenMonthBack As Date = DateAdd(DateInterval.Month, -7, CDate(AgL.PubLoginDate))
        'Dim EightMonthBack As Date = DateAdd(DateInterval.Month, -8, CDate(AgL.PubLoginDate))
        'Dim NineMonthBack As Date = DateAdd(DateInterval.Month, -9, CDate(AgL.PubLoginDate))

        'If Purpose = "" Then

        '    mQry = "Select H.Subcode as SearchCode, Sg.name || (Case When IfNull(City.CityName,'') <> '' Then ', ' || IfNull(City.CityName,'') else '' End) as Party,                            
        '                    Sum(CASE WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(CurrentMonth) & ") Then H.BalanceAmount ELSE 0 END ) AS [" & CurrentMonth.ToString("MMM-yy").Replace("-", " ") & "],
        '                    Sum(CASE WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(OneMonthBack) & ") Then H.BalanceAmount ELSE 0 END ) AS [" & OneMonthBack.ToString("MMM-yy").Replace("-", " ") & "],
        '                    Sum(CASE WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(TwoMonthBack) & ") Then H.BalanceAmount ELSE 0 END ) AS [" & TwoMonthBack.ToString("MMM-yy").Replace("-", " ") & "],
        '                    Sum(CASE WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(ThreeMonthBack) & ") Then H.BalanceAmount ELSE 0 END ) AS [" & ThreeMonthBack.ToString("MMM-yy").Replace("-", " ") & "],
        '                    Sum(CASE WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(FourMonthBack) & ") Then H.BalanceAmount ELSE 0 END ) AS [" & FourMonthBack.ToString("MMM-yy").Replace("-", " ") & "],
        '                    Sum(CASE WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(FiveMonthBack) & ") Then H.BalanceAmount ELSE 0 END ) AS [" & FiveMonthBack.ToString("MMM-yy").Replace("-", " ") & "],
        '                    Sum(CASE WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(SixMonthBack) & ") Then H.BalanceAmount ELSE 0 END ) AS [" & SixMonthBack.ToString("MMM-yy").Replace("-", " ") & "],
        '                    Sum(CASE WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(SevenMonthBack) & ") Then H.BalanceAmount ELSE 0 END ) AS [" & SevenMonthBack.ToString("MMM-yy").Replace("-", " ") & "],
        '                    Sum(CASE WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(EightMonthBack) & ") Then H.BalanceAmount ELSE 0 END ) AS [" & EightMonthBack.ToString("MMM-yy").Replace("-", " ") & "],
        '                    Sum(CASE WHEN strftime('%Y%m', H.V_Date) < strftime('%Y%m', " & AgL.Chk_Date(EightMonthBack) & ") Then H.BalanceAmount ELSE 0 END ) AS [Before " & EightMonthBack.ToString("MMM-yy").Replace("-", " ") & "],
        '                    Sum(H.BalanceAmount) As [Balance],
        '                    Sum(H.DrCr) As [DrCr]                                                                                                                
        '                    From #FifoOutstanding H
        '                    Left Join Subgroup Sg on H.Subcode = Sg.Subcode
        '                    Left Join City On Sg.CityCode = City.CityCode
        '                    Group By H.Subcode, Sg.name || (Case When IfNull(City.CityName,'') <> '' Then ', ' || IfNull(City.CityName,'') else '' End)
        '                    Order By Sg.name || (Case When IfNull(City.CityName,'') <> '' Then ', ' || IfNull(City.CityName,'') else '' End)
        '                    "

        '    DsHeader = AgL.FillData(mQry, AgL.GCn)
        '    'FillFifoOutstanding = DsHeader
        'Else

        '    Dim mMultiplier As Double
        '    If ClsMain.FDivisionNameForCustomization(22) = "W SHYAMA SHYAM FABRICS" Or ClsMain.FDivisionNameForCustomization(27) = "W SHYAMA SHYAM VENTURES LLP" Then
        '        mMultiplier = 0.01
        '    Else
        '        mMultiplier = 1.0
        '    End If

        '    mQry = "Select H.Subcode as SearchCode, Sg.name || (Case When IfNull(City.CityName,'') <> '' Then ', ' || IfNull(City.CityName,'') else '' End) as Party,                            
        '                    Sum(H.BalanceAmount) * " & mMultiplier & " as BalanceAmount, 

        '                    (CASE WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(CurrentMonth) & ") Then " & AgL.Chk_Date(CurrentMonth) & "
        '                        WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(OneMonthBack) & ") Then " & AgL.Chk_Date(OneMonthBack) & "
        '                        WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(TwoMonthBack) & ") Then " & AgL.Chk_Date(TwoMonthBack) & "
        '                        WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(ThreeMonthBack) & ") Then " & AgL.Chk_Date(ThreeMonthBack) & "
        '                        WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(FourMonthBack) & ") Then " & AgL.Chk_Date(FourMonthBack) & "
        '                        WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(FiveMonthBack) & ") Then " & AgL.Chk_Date(FiveMonthBack) & "
        '                        WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(SixMonthBack) & ") Then " & AgL.Chk_Date(SixMonthBack) & "
        '                        WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(SevenMonthBack) & ") Then " & AgL.Chk_Date(SevenMonthBack) & "
        '                        WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(EightMonthBack) & ") Then " & AgL.Chk_Date(EightMonthBack) & "
        '                        WHEN strftime('%Y%m', H.V_Date) < strftime('%Y%m', " & AgL.Chk_Date(EightMonthBack) & ") Then " & AgL.Chk_Date(NineMonthBack) & "
        '                     Else Null End) as BalanceMonth   
        '                    From #FifoOutstanding H
        '                    Left Join Subgroup Sg on H.Subcode = Sg.Subcode
        '                    Left Join City On Sg.CityCode = City.CityCode
        '                    Group By H.Subcode, Sg.name || (Case When IfNull(City.CityName,'') <> '' Then ', ' || IfNull(City.CityName,'') else '' End),
        '                    (CASE WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(CurrentMonth) & ") Then " & AgL.Chk_Date(CurrentMonth) & "
        '                        WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(OneMonthBack) & ") Then " & AgL.Chk_Date(OneMonthBack) & "
        '                        WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(TwoMonthBack) & ") Then " & AgL.Chk_Date(TwoMonthBack) & "
        '                        WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(ThreeMonthBack) & ") Then " & AgL.Chk_Date(ThreeMonthBack) & "
        '                        WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(FourMonthBack) & ") Then " & AgL.Chk_Date(FourMonthBack) & "
        '                        WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(FiveMonthBack) & ") Then " & AgL.Chk_Date(FiveMonthBack) & "
        '                        WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(SixMonthBack) & ") Then " & AgL.Chk_Date(SixMonthBack) & "
        '                        WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(SevenMonthBack) & ") Then " & AgL.Chk_Date(SevenMonthBack) & "
        '                        WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(EightMonthBack) & ") Then " & AgL.Chk_Date(EightMonthBack) & "
        '                        WHEN strftime('%Y%m', H.V_Date) < strftime('%Y%m', " & AgL.Chk_Date(EightMonthBack) & ") Then " & AgL.Chk_Date(NineMonthBack) & "
        '                     Else Null End)
        '                    Order By Sg.name || (Case When IfNull(City.CityName,'') <> '' Then ', ' || IfNull(City.CityName,'') else '' End), H.V_Date
        '            "
        '    FillFifoOutstanding = AgL.FillData(mQry, AgL.GCn)
        'End If
    End Function

    Private Sub CreateTemporaryTables()
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
                    PurchaseReturn Float, 
                    BalanceAmount Float,
                    DrCr nVarchar(10),                  
                    Narration  varchar(2000)
                    ); "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

    End Sub

    Private Sub ReportFrm_DGL1CheckedColumnValueChanged(Sender As Object, columnIndex As Integer) Handles ReportFrm.DGL1CheckedColumnValueChanged

        If (AgL.VNull(ReportFrm.DGL1.Item("Bill Amount", Sender.CurrentCell.RowIndex).Value) + AgL.VNull(ReportFrm.DGL1.Item("Purchase Return", Sender.CurrentCell.RowIndex).Value)) <> AgL.VNull(ReportFrm.DGL1.Item("Balance Amount", Sender.CurrentCell.RowIndex).Value) Then
            If MsgBox("Records Bill amount not matching with Balance Amount. Can not select this record", vbYesNo) = vbNo Then
                If ReportFrm.DGL1.Item("Tick", Sender.currentcell.RowIndex).Value = "þ" Then
                    ReportFrm.DGL1.Item("Tick", Sender.currentcell.RowIndex).Value = "o"
                Else
                    ReportFrm.DGL1.Item("Tick", Sender.currentcell.RowIndex).Value = "þ"
                End If

                Exit Sub
            End If
        End If
            Dim I As Integer
            Dim mSelectedBal As Double
            mSelectedBal = 0
            For I = 0 To ReportFrm.DGL1.RowCount - 1
                If ReportFrm.DGL1.Item("Search Code", I).Value = "" Then
                    ReportFrm.DGL1.Item("Tick", I).Value = "þ"
                End If
                If ReportFrm.DGL1.Item("Tick", I).Value = "þ" Then
                    If ReportFrm.DGL1.Columns.Contains("Balance Amount") Then
                        mSelectedBal = Math.Round(mSelectedBal + AgL.VNull(ReportFrm.DGL1.Item("Balance Amount", I).Value), 2)
                    End If
                End If
            Next
            If ReportFrm.DGL2.Columns.Contains("Party Name") Then
                ReportFrm.DGL2.Item("Party Name", 0).Value = "Selected Bal.: " & mSelectedBal
            End If

        'If Sender(columnIndex, Sender.CurrentCell.RowIndex).Value = "þ" Then
        '    ReportFrm.DGL2.Item("Search Code", 0).Value = Math.Round(AgL.VNull(ReportFrm.DGL2.Item("Search Code", 0).Value) + AgL.VNull(ReportFrm.DGL1.Item("Balance Amount", Sender.CurrentCell.RowIndex).Value), 2)
        'Else
        '    ReportFrm.DGL2.Item("Search Code", 0).Value = Math.Round(AgL.VNull(ReportFrm.DGL2.Item("Search Code", 0).Value) - AgL.VNull(ReportFrm.DGL1.Item("Balance Amount", Sender.CurrentCell.RowIndex).Value), 2)
        'End If
        'ReportFrm.DGL2.Item("Party Name", 0).Value = "Selected Balance : " & ReportFrm.DGL2.Item("Search Code", 0).Value        
    End Sub

    Private Sub ReportFrm_FilterApplied() Handles ReportFrm.FilterApplied
        Dim I As Integer
        Dim mSelectedBal As Double
        mSelectedBal = 0
        For I = 0 To ReportFrm.DGL1.RowCount - 1
            If ReportFrm.DGL1.Item("Tick", I).Value = "þ" Then
                If ReportFrm.DGL1.Columns.Contains("Balance Amount") Then
                    mSelectedBal = Math.Round(mSelectedBal + AgL.VNull(ReportFrm.DGL1.Item("Balance Amount", I).Value), 2)
                End If
            End If
        Next
        If ReportFrm.DGL2.Columns.Contains("Party Name") Then
            ReportFrm.DGL2.Item("Party Name", 0).Value = "Selected Bal.: " & mSelectedBal
        End If
    End Sub
End Class
