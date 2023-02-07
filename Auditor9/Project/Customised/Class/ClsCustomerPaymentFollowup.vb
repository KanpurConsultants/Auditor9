Imports System.IO
Imports AgLibrary
Imports AgLibrary.ClsMain
Imports AgLibrary.ClsMain.agConstants
Imports CrystalDecisions.CrystalReports.Engine
Imports Microsoft.Reporting.WinForms
Public Class ClsCustomerPaymentFollowup

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
    Dim rowDueUptoDate As Integer = 3
    Dim rowParty As Integer = 4
    Dim rowMasterParty As Integer = 5
    Dim rowLinkedParty As Integer = 6
    Dim rowAgent As Integer = 7
    Dim rowRelationShipExecutive As Integer = 8
    Dim rowCity As Integer = 9
    Dim rowArea As Integer = 10
    Dim rowDivision As Integer = 11
    Dim rowSite As Integer = 12

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
            If ClsMain.FDivisionNameForCustomization(22) = "W SHYAMA SHYAM FABRICS" Or ClsMain.FDivisionNameForCustomization(27) = "W SHYAMA SHYAM VENTURES LLP" Then
                ReportFrm.CreateHelpGrid("Group On", "Group On", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mQry, "Linked Party")
            Else
                ReportFrm.CreateHelpGrid("Group On", "Group On", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mQry, "Party")
            End If

            ReportFrm.FilterGrid.Rows(rowGroupOn).Visible = False

            ReportFrm.CreateHelpGrid("Bills Upto Date", "Bills Upto Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubLoginDate)

            ReportFrm.CreateHelpGrid("Due Upto Date", "Due Upto Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubLoginDate)

            mQry = " Select 'o' As Tick,  Sg.Code, Sg.Name AS Party, Sg.Address, Ag.GroupName FROM viewHelpSubGroup Sg  Left Join AcGroup Ag On Sg.groupCode= ag.GroupCode Where Sg.Nature In ('Customer','Cash') And Sg.SubgroupType Not In ('Master Customer','Master Supplier') "
            ReportFrm.CreateHelpGrid("Customer", "Customer", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mQry, , 450, 825, 300)

            mQry = " Select 'o' As Tick,  Sg.Code, Sg.Name AS Party, Sg.Address, Ag.GroupName FROM viewHelpSubGroup Sg  Left Join AcGroup Ag On Sg.groupCode= ag.GroupCode Where Sg.Nature In ('Customer') And Sg.Code In (Select Distinct Parent From SubGroup) Order By Name"
            ReportFrm.CreateHelpGrid("Master Customer", "Master Customer", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mQry, , 450, 825, 300)
            If Not ClsMain.IsScopeOfWorkContains(IndustryType.CommonModules.MasterParty) Then ReportFrm.FilterGrid.Rows(rowMasterParty).Visible = False

            mQry = " Select 'o' As Tick,  Sg.Code, Sg.Name AS Party, Sg.Address, Ag.GroupName FROM viewHelpSubGroup Sg  Left Join AcGroup Ag On Sg.groupCode= ag.GroupCode Where Sg.SubgroupType In ('Master Customer') Order By Name"
            ReportFrm.CreateHelpGrid("Linked Customer", "Linked Customer", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mQry, , 450, 825, 300)

            mQry = " Select 'o' As Tick,  Sg.Code, Sg.Name AS Agent FROM viewHelpSubgroup Sg Where Sg.SubgroupType = '" & SubgroupType.SalesAgent & "' "
            ReportFrm.CreateHelpGrid("Agent", "Agent", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mQry)

            mQry = " Select 'o' As Tick,  Sg.Code, Sg.Name AS Agent FROM viewHelpSubgroup Sg Where Sg.SubgroupType = '" & SubgroupType.Employee & "' "
            ReportFrm.CreateHelpGrid("Relationship Executive", "Relationship Executive", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mQry)

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
    Private Function CreateCondStr() As String
        Dim mCondStr As String

        mCondStr = " And Sg.Nature In ('Customer','Supplier') "
        mCondStr = mCondStr & " AND Date(LG.V_Date) <= (Case 
                                                            When Sg.Nature='Customer' And Lg.AmtDr>0 Then " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowBillsUptoDate)).ToString("s")) & " 
                                                            When Sg.Nature='Customer' And Lg.AmtCr>0 Then " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowDueUptoDate)).ToString("s")) & " 
                                                            When Sg.Nature<>'Customer' And Lg.AmtDr>0 Then " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowDueUptoDate)).ToString("s")) & " 
                                                            When Sg.Nature<>'Customer' And Lg.AmtCr>0 Then " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowBillsUptoDate)).ToString("s")) & " 
                                                            End) "
        mCondStr = mCondStr & ReportFrm.GetWhereCondition("LG.Subcode", rowParty)
        mCondStr = mCondStr & ReportFrm.GetWhereCondition("Sg.Parent", rowMasterParty)
        mCondStr = mCondStr & ReportFrm.GetWhereCondition("LG.LinkedSubcode", rowLinkedParty)
        mCondStr = mCondStr & ReportFrm.GetWhereCondition("Sg.RelationshipExecutive", rowRelationShipExecutive)
        mCondStr = mCondStr & ReportFrm.GetWhereCondition("LTV.Agent", rowAgent)
        mCondStr = mCondStr & ReportFrm.GetWhereCondition("Sg.CityCode", rowCity)
        mCondStr = mCondStr & ReportFrm.GetWhereCondition("Sg.Area", rowArea)
        mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("LG.Site_Code", rowSite), "''", "'")
        mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("LG.DivCode", rowDivision), "''", "'")


        mCondStr = mCondStr & " And LG.DocID || LG.V_SNo Not In (SELECT H.PurchaseInvoiceDocId || IfNull(H.PurchaseInvoiceDocIdSr,'')   FROM Cloth_SupplierSettlementInvoices H
                                                                       UNION ALL 
                                                                       SELECT H.PaymentDocId || IfNull(H.PaymentDocIdSr,'')   FROM Cloth_SupplierSettlementPayments H
                                                                       ) "
        If ClsMain.FDivisionNameForCustomization(22) = "W SHYAMA SHYAM FABRICS" Or ClsMain.FDivisionNameForCustomization(27) = "W SHYAMA SHYAM VENTURES LLP" Then
            mCondStr = mCondStr & " And LG.DocID Not In ( SELECT H.DocID FROM LedgerHead H LEFT JOIN LedgerHeadDetail L ON H.DocID = L.DocID WHERE H.V_Type In ('WPS','WRS') ) "
        Else
            mCondStr = mCondStr & " And LG.DocID Not In ( SELECT H.DocID FROM LedgerHead H LEFT JOIN LedgerHeadDetail L ON H.DocID = L.DocID WHERE H.V_Type In ('" & Ncat.PaymentSettlement & "','" & Ncat.ReceiptSettlement & "') ) "
        End If






        CreateCondStr = mCondStr
    End Function


    Public Sub ProcFillReport(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try

            Dim mCondStr$ = ""


            CreateTemporaryTables()




            RepTitle = "Customer Ledger " & " - " & ReportFrm.FGetText(rowReportType) & " - " & ReportFrm.FGetText(rowGroupOn)






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







            FillFifoOutstanding(mCondStr)






            If DsHeader.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")
            ReportFrm.Text = "Customer Ledger " & " - " & ReportFrm.FGetText(rowReportType) & " - " & ReportFrm.FGetText(rowGroupOn)

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


    Private Function FillFifoOutstanding(mCondstr As String) As DataSet
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
        mQry = mQry & " Left Join(Select SILTV.Subcode, Max(SILTV.Agent) As Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode) as LTV On Sg.Subcode = LTV.Subcode
                                    Where  Sg.Nature='Customer' "
        mQry = mQry & mCondstr
        mQry = mQry & " Group By Sg.Subcode"
        mQry = mQry & " Having Sum(Lg.AmtDr)-Sum(Lg.AmtCr) <> 0 "


        dtParty = AgL.FillData(mQry, AgL.GCn).Tables(0)


        mQry = "Select Lg.DocID, LG.V_SNo, Lg.DivCode, Lg.Site_Code, Lg.V_Type, Vt.Description as V_TypeDesc, 
                                    Lg.RecId, SG.Subcode, Ifnull(LG.LinkedSubcode, LG.SubCode) as LinkedSubcode, IfNull(Lg.EffectiveDate, Lg.V_Date) As V_Date, Lg.Narration, Lg.AmtDr as Amount                                
                                    From Ledger Lg  With (NoLock) "
        If ReportFrm.FGetText(rowGroupOn) = "Linked Party" Then
            mQry = mQry & " Left Join SubGroup SG On SG.Subcode =IfNull(Lg.LinkedSubcode,LG.SubCode) "
        Else
            mQry = mQry & " Left Join SubGroup SG On SG.Subcode =LG.SubCode "
        End If

        mQry = mQry & " Left Join(Select SILTV.Subcode, Max(SILTV.Agent) As Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode) as LTV On Sg.Subcode = LTV.Subcode
                                    Left Join Voucher_Type Vt On Lg.V_Type = Vt.V_Type
                                    Where Lg.AmtDr > 0 " & mCondstr & " 
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
                        mQry = ""
                    End If
                End If


                BalAmount = 0 : DrCr = ""
                mRemainingBalance = Math.Abs(AgL.VNull(dtParty.Rows(i)("Balance")))
                If mQry <> "" Then
                    'DtMain = AgL.FillData(mQry, AgL.GCn).Tables(0)
                    drowCr = dtCr.Select("Subcode = '" & AgL.XNull(dtParty.Rows(i)("Subcode")) & "'", " V_Date Desc ")
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
                                                (DocID, V_SNo, V_Type, RecID, V_Date, 
                                                Site_Code, Div_Code, SubCode,LinkedSubcode, BillAmount,
                                                BalanceAmount, DrCr, Narration)    
                                                Values(" & AgL.Chk_Text(AgL.XNull(drowCr(j)("DocID"))) & ",
                                                " & AgL.Chk_Text(AgL.XNull(drowCr(j)("V_SNo"))) & ",
                                                " & AgL.Chk_Text(AgL.XNull(drowCr(j)("V_Type"))) & ",
                                                " & AgL.Chk_Text(AgL.XNull(drowCr(j)("RecID"))) & ",
                                                " & AgL.Chk_Date(AgL.XNull(drowCr(j)("V_Date"))) & ",                                            
                                                " & AgL.Chk_Text(AgL.XNull(drowCr(j)("Site_Code"))) & ",
                                                " & AgL.Chk_Text(AgL.XNull(drowCr(j)("DivCode"))) & ",
                                                " & AgL.Chk_Text(AgL.XNull(drowCr(j)("Subcode"))) & ",
                                                " & AgL.Chk_Text(AgL.XNull(drowCr(j)("LinkedSubcode"))) & ",
                                                " & AgL.Chk_Text(AgL.XNull(drowCr(j)("Amount"))) & ",
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
        mQry = "Select  H.DocID as SearchCode,H.V_Type || '-' || H.RecID as EntryNo, strftime('%d-%m-%Y',H.V_Date) as EntryDate, 
                S.Name as Site, D.Div_Name as Division, Sg.Subcode as PartyCode,
                Sg.Name || (Case When Sgc.CityName Is Null Then '' Else ',' || Sgc.CityName End) as PartyName, bSg.Name as MasterPartyName,  
                H.BillAmount, H.BalanceAmount,  
                strftime('%d-%m-%Y',Date(H.V_Date,'+' || IfNull(IfNull(Ints.LeaverageDays,P.CreditDays),0) || ' Day'))  as DueDate,  
                PI.VendorDocNo Remarks, Sg.Phone, Sg.Mobile, Agent.Name as AgentName, H.LinkedSubcode
                from #FifoOutstanding H
                Left Join SiteMast S On H.Site_Code = S.Code
                Left Join Division D On H.Div_Code = D.Div_Code
                Left Join Subgroup Sg On H.Subcode = Sg.Subcode 
                Left Join City SgC On Sg.CityCode = Sgc.CityCode
                Left Join Subgroup bsg on Sg.Parent = bsg.Subcode
                Left Join Subgroup P On Sg.Parent = P.Subcode 
                Left Join PurchInvoice PI On H.DocID = PI.DocId
                Left Join InterestSlab IntS On IfNull(BSg.InterestSlab,Sg.InterestSlab) = Ints.Code
                Left Join(Select SILTV.Subcode, Max(SILTV.Agent) As Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode) as LTV On Sg.Subcode = LTV.Subcode
                Left Join Subgroup Agent on LTV.Agent = Agent.Subcode
                Left Join LedgerPaymentFollowup Fup on H.DocID = Fup.DocID and H.V_SNo = Fup.V_SNo
                Where Date(H.V_Date,'+' || IfNull(P.CreditDays,0) || ' Day') <= " & AgL.Chk_Date(ReportFrm.FGetText(rowDueUptoDate)) & " 
                And IfNull(Fup.NextFollowupDate, Date(H.V_Date,'+' || IfNull(P.CreditDays,0) || ' Day')) <= " & AgL.Chk_Date(ReportFrm.FGetText(rowDueUptoDate)) & " 
                And (Fup.UnableToConnectDate is Null Or (strftime('%s','now','localtime') - strftime('%s',Fup.UnableToConnectDate)) / 60 > 180 )
                And H.BalanceAmount>0
                Order By Date(H.V_Date,'+' || IfNull(IfNull(Ints.LeaverageDays,P.CreditDays),0) || ' Day') "
        DsHeader = AgL.FillData(mQry, AgL.GCn)
    End Function

    Private Sub CreateTemporaryTables()
        Try
            mQry = "Drop Table #FifoOutstanding"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        Catch ex As Exception
        End Try


        mQry = " CREATE Temporary TABLE #FifoOutstanding 
                    (DocId  nvarchar(21), 
                    V_Sno Int,
                    V_type  nvarchar(20),
                    RecId  nvarchar(50), 
                    V_Date  DateTime,
                    Site_Code  nvarchar(2), 
                    Div_Code nVarchar(1),                         
                    Subcode nvarchar(10),
                    LinkedSubcode nVarchar(10),
                    BillAmount Float, 
                    BalanceAmount Float,
                    DrCr nVarchar(10),                  
                    Narration  varchar(2000)
                    ); "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

    End Sub

    Private Sub ReportFrm_Dgl1KeyDown(sender As Object, e As KeyEventArgs) Handles ReportFrm.Dgl1KeyDown
        If e.KeyCode = Keys.F2 Then
            ShowFrmCustomerPaymenFollowup()
        End If
    End Sub

    Private Sub ShowFrmCustomerPaymenFollowup()

        Try
            Dim mPartyCode As String
            Dim mObj As FrmCustomerPaymentFollowup
            If ReportFrm.DGL1.CurrentCell Is Nothing Then Exit Sub

            mPartyCode = AgL.XNull(ReportFrm.DGL1.Item("Party Code", ReportFrm.DGL1.CurrentCell.RowIndex).Value)
            If ReportFrm.DGL1.Rows(ReportFrm.DGL1.CurrentCell.RowIndex).Tag IsNot Nothing Then
                mObj = ReportFrm.DGL1.Rows(ReportFrm.DGL1.CurrentCell.RowIndex).Tag
                mObj.EntryMode = "Edit"
            Else
                mObj = New FrmCustomerPaymentFollowup
                mObj.StartPosition = FormStartPosition.CenterScreen
                'mObj.Top = 100
                mObj.EntryMode = "Add"
                Dim mMainCondStr As New FrmCustomerPaymentFollowup.structCondStr
                mMainCondStr.GroupOn = ReportFrm.FGetText(rowGroupOn)
                mMainCondStr.BillsUptoDate = ReportFrm.FGetText(rowBillsUptoDate)
                mMainCondStr.DueUptoDate = ReportFrm.FGetText(rowDueUptoDate)
                mMainCondStr.PartyCodes = AgL.XNull(ReportFrm.DGL1.Item("Party Code", ReportFrm.DGL1.CurrentCell.RowIndex).Value)
                'mMainCondStr.LinkedPartyCodes = AgL.XNull(ReportFrm.DGL1.Item("Linked Subcode", ReportFrm.DGL1.CurrentCell.RowIndex).Value)

                mMainCondStr.Site_Code = ReportFrm.FGetCode(rowSite)
                mMainCondStr.Div_Code = ReportFrm.FGetCode(rowDivision)
                mObj.MainCondStr = mMainCondStr
                mObj.IniGrid()
                mObj.FMoverec(AgL.XNull(ReportFrm.DGL1.Item("Party Code", ReportFrm.DGL1.CurrentCell.RowIndex).Value))
            End If


            mObj.ShowDialog()
            If mObj.mOkButtonPressed Then
                Dim i As Integer
                Dim mRow As Integer = ReportFrm.DGL1.CurrentCell.RowIndex
                Dim mCol As Integer = ReportFrm.DGL1.CurrentCell.ColumnIndex
                ReportFrm.DGL1.CurrentCell = Nothing
                For i = 0 To ReportFrm.DGL1.Rows.Count - 1
                    If AgL.XNull(ReportFrm.DGL1.Item("Party Code", i).Value) = mPartyCode Then
                        ReportFrm.DGL1.Rows(i).Visible = False
                    End If
                Next
                ReportFrm.DGL1.CurrentCell = ReportFrm.DGL1.FirstDisplayedCell ' ReportFrm.DGL1(mRow, mCol)
                ReportFrm.DGL1.Focus()
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class
