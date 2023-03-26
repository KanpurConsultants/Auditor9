Imports System.ComponentModel
Imports System.IO
Imports AgLibrary
Imports AgLibrary.ClsMain
Imports AgLibrary.ClsMain.agConstants
Imports CrystalDecisions.CrystalReports.Engine
Imports Customised.ClsMain
Imports Microsoft.Reporting.WinForms
Public Class ClsPartyLedgerGrid

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
    Private Const Col1ReconciliationDate As String = "Reconciliation Date"

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
    Dim rowNextStep As Integer = 14
    Dim rowInterestUptoDate As Integer = 15


    Dim mPartyNature As String

    Private Structure StructLedger
        Public DocID As String
        Public Sr As String
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
        Public Clg_Date As String
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

            If GRepFormName.ToUpper = "SupplierLedger".ToUpper Then
                mPartyNature = "SUPPLIER"
            ElseIf GRepFormName.ToUpper = "CustomerLedger".ToUpper Then
                mPartyNature = "CUSTOMER"
            End If


            mQry = "Select 'Party Wise Summary - Ageing' as Code, 'Party Wise Summary - Ageing' as Name 
                    Union All
                    Select 'Party Wise Summary' as Code, 'Party Wise Summary' as Name 
                    Union All
                    Select 'Party Wise Detail' as Code, 'Party Wise Detail' as Name 
                    Union All
                    Select 'Party Wise Balance' as Code, 'Party Wise Balance' as Name 
                    Union All
                    Select 'Ledger' as Code, 'Ledger' as Name 
                    Union All
                    Select 'Interest Ledger' as Code, 'Interest Ledger' as Name "
            ReportFrm.CreateHelpGrid("Report Type", "Report Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mQry, "Party Wise Summary - Ageing",,,,, False)

            mQry = "Select 'Party' as Code, 'Party' as Name 
                    Union All
                    Select 'Linked Party' as Code, 'Linked Party' as Name"
            ReportFrm.CreateHelpGrid("Group On", "Group On", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mQry, "Party")

            ReportFrm.CreateHelpGrid("From Date", "From Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", "")
            ReportFrm.CreateHelpGrid("Bills Upto Date", "Bills Upto Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubLoginDate)
            ReportFrm.CreateHelpGrid("Payments Upto Date", "Payments Upto Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubLoginDate)
            mQry = "Select 'All' as Code, 'All' as Name 
                    Union All
                    Select 'After Concur' as Code, 'After Concur' as Name 
                    "
            ReportFrm.CreateHelpGrid("Records Type", "Records Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mQry, "All")

            If GRepFormName.ToUpper = "SupplierLedger".ToUpper Then
                mQry = " Select 'o' As Tick,  Sg.Code, Sg.Name AS Party, Sg.Address, Ag.GroupName FROM viewHelpSubGroup Sg  Left Join AcGroup Ag On Sg.groupCode= ag.GroupCode Where Sg.Nature In ('Supplier','Cash') And Sg.SubgroupType Not In ('Master Customer','Master Supplier') "
                ReportFrm.CreateHelpGrid("Supplier", "Supplier", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mQry, , 450, 825, 300)
            ElseIf GRepFormName.ToUpper = "CustomerLedger".ToUpper Then
                mQry = " Select 'o' As Tick,  Sg.Code, Sg.Name AS Party, Sg.Address, Ag.GroupName FROM viewHelpSubGroup Sg  Left Join AcGroup Ag On Sg.groupCode= ag.GroupCode Where Sg.Nature In ('Customer','Cash') And Sg.SubgroupType Not In ('Master Customer','Master Supplier') "
                ReportFrm.CreateHelpGrid("Customer", "Customer", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mQry, , 450, 825, 300)
            Else
                mQry = " Select 'o' As Tick,  Sg.Code, Sg.Name AS Party, Sg.Address, Ag.GroupName FROM viewHelpSubGroup Sg  Left Join AcGroup Ag On Sg.groupCode= ag.GroupCode Where Sg.Nature In ('Customer','Supplier','Cash') And Sg.SubgroupType Not In ('Master Customer','Master Supplier') "
                ReportFrm.CreateHelpGrid("Party", "Party", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mQry, , 450, 825, 300)
            End If

            If GRepFormName.ToUpper = "SupplierLedger".ToUpper Then
                mQry = " Select 'o' As Tick,  Sg.Code, Sg.Name AS Party, Sg.Address, Ag.GroupName FROM viewHelpSubGroup Sg  Left Join AcGroup Ag On Sg.groupCode= ag.GroupCode Where Sg.Nature In ('Supplier') And Sg.Code In (Select Distinct Parent From SubGroup) Order By Name"
                ReportFrm.CreateHelpGrid("Master Supplier", "Master Supplier", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mQry, , 450, 825, 300)
            ElseIf GRepFormName.ToUpper = "CustomerLedger".ToUpper Then
                mQry = " Select 'o' As Tick,  Sg.Code, Sg.Name AS Party, Sg.Address, Ag.GroupName FROM viewHelpSubGroup Sg  Left Join AcGroup Ag On Sg.groupCode= ag.GroupCode Where Sg.Nature In ('Customer') And Sg.Code In (Select Distinct Parent From SubGroup) Order By Name"
                ReportFrm.CreateHelpGrid("Master Customer", "Master Customer", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mQry, , 450, 825, 300)
            Else
                mQry = " Select 'o' As Tick,  Sg.Code, Sg.Name AS Party, Sg.Address, Ag.GroupName FROM viewHelpSubGroup Sg  Left Join AcGroup Ag On Sg.groupCode= ag.GroupCode Where Sg.Nature In ('Customer','Supplier') And Sg.Code In (Select Distinct Parent From SubGroup) Order By Name"
                ReportFrm.CreateHelpGrid("Master Party", "Master Party", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mQry, , 450, 825, 300)
            End If
            If Not ClsMain.IsScopeOfWorkContains(IndustryType.CommonModules.MasterParty) Then ReportFrm.FilterGrid.Rows(rowMasterParty).Visible = False

            If GRepFormName.ToUpper = "SupplierLedger".ToUpper Then
                mQry = " Select 'o' As Tick,  Sg.Code, Sg.Name AS Party, Sg.Address, Ag.GroupName FROM viewHelpSubGroup Sg  Left Join AcGroup Ag On Sg.groupCode= ag.GroupCode Where Sg.SubgroupType In ('Master Supplier') Order By Name"
                ReportFrm.CreateHelpGrid("Linked Supplier", "Linked Supplier", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mQry, , 450, 825, 300)
            ElseIf GRepFormName.ToUpper = "CustomerLedger".ToUpper Then
                mQry = " Select 'o' As Tick,  Sg.Code, Sg.Name AS Party, Sg.Address, Ag.GroupName FROM viewHelpSubGroup Sg  Left Join AcGroup Ag On Sg.groupCode= ag.GroupCode Where Sg.SubgroupType In ('Master Customer') Order By Name"
                ReportFrm.CreateHelpGrid("Linked Customer", "Linked Customer", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mQry, , 450, 825, 300)
            Else
                mQry = " Select 'o' As Tick,  Sg.Code, Sg.Name AS Party, Sg.Address, Ag.GroupName FROM viewHelpSubGroup Sg  Left Join AcGroup Ag On Sg.groupCode= ag.GroupCode Where Sg.SubgroupType In ('Master Customer','Master Supplier') Order By Name"
                ReportFrm.CreateHelpGrid("Linked Party", "Linked Party", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mQry, , 450, 825, 300)
            End If
            If Not ClsMain.IsScopeOfWorkContains(IndustryType.CommonModules.LinkedParty) Then ReportFrm.FilterGrid.Rows(rowLinkedParty).Visible = False

            mQry = " Select 'o' As Tick,  Sg.Code, Sg.Name AS Agent FROM viewHelpSubgroup Sg Where Sg.SubgroupType = '" & SubgroupType.SalesAgent & "' "
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


            mDefaultValue = ClsMain.FGetSettings(ClsMain.SettingFields.DefaultSiteNameInReportFilters, SettingType.General, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
            If mDefaultValue = "All" Then
                mDefaultValue = "All"
            Else
                mDefaultValue = "[SITECODE]"
            End If
            mQry = "Select 'o' As Tick, Code, Name As [Site] From SiteMast Where  Code In (" & AgL.PubSiteList & ")  "
            ReportFrm.CreateHelpGrid("Site", "Site", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mQry, mDefaultValue)


            mQry = "Select 'Ledger' as Code, 'Ledger' as Name 
                    Union All
                    Select 'Interest Ledger' as Code, 'Interest Ledger' as Name "
            ReportFrm.CreateHelpGrid("Next Step Report", "Next Step Report", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mQry, "Ledger",,,,, False)


            ReportFrm.BtnCustomMenu.Visible = True
            mQry = "Select 'Formatted Print' As MenuText, 'ProcFormattedPrint' As FunctionName"
            Dim DtMenuList As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
            ReportFrm.MnuCustomOption.Items.Clear()
            ReportFrm.DTCustomMenus = DtMenuList
            ReportFrm.FCreateCustomMenus()

            ReportFrm.CreateHelpGrid("Interest Upto Date", "Interest Upto Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubLoginDate)
            'ReportFrm.CreateHelpGrid("Additional Credit Days", "Additional Credit Days", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.StringType, "", "")


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

    Private Function ValidateInput() As Boolean
        ValidateInput = False
        'Dim mAdditionalCreditDays As String
        'mAdditionalCreditDays = ReportFrm.FGetText(rowAdditionalCreditDays)
        'If mAdditionalCreditDays <> "" Then
        '    If Val(mAdditionalCreditDays) <> 0 Then
        '        If mAdditionalCreditDays.Substring(0, 1) = "+" Or mAdditionalCreditDays.Substring(0, 1) = "-" Then
        '            ValidateInput = True
        '        End If
        '    End If
        '    If ValidateInput = False Then
        '        MsgBox("Additional Credit days should be in format of '+20' or '-10'. It is in wrong format. Can not continue ")
        '    End If
        'End If
        ValidateInput = True
    End Function

    Private Function CreateCondStr() As String
        Dim mCondStr As String

        Dim mExcludeLedgerAccountsFromTrial As String = ""
        mExcludeLedgerAccountsFromTrial = ClsMain.FGetSettings(SettingFields.ExcludeLedgerAccountsFromTrial, SettingType.General, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")


        mCondStr = " And Sg.Nature In ('Customer','Supplier') "
        mCondStr = mCondStr & " And IfNull(Sg.Status,'Active') Not In ('" & mExcludeLedgerAccountsFromTrial.Replace("+", "','") & "') "
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
        mCondStr = mCondStr & ReportFrm.GetWhereCondition("PSg.Area", rowArea)
        mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("LG.Site_Code", rowSite), "''", "'")
        mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("LG.DivCode", rowDivision), "''", "'")


        If ReportFrm.FGetText(rowRecordsType) = "After Concur" Then
            mCondStr = mCondStr & " And LG.DocID || LG.V_SNo Not In (SELECT H.PurchaseInvoiceDocId || H.PurchaseInvoiceDocIdSr   FROM Cloth_SupplierSettlementInvoices H
                                                                       UNION ALL 
                                                                       SELECT H.PaymentDocId || H.PaymentDocIdSr   FROM Cloth_SupplierSettlementPayments H
                                                                       ) "
            If ClsMain.FDivisionNameForCustomization(22) = "W SHYAMA SHYAM FABRICS" Or ClsMain.FDivisionNameForCustomization(27) = "W SHYAMA SHYAM VENTURES LLP" Then
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
                If mPartyNature = "SUPPLIER" Then
                    mCondStr = mCondStr & " And Sg.SubgroupType In ('" & SubgroupType.Supplier & "','Master Supplier') "
                End If
            Else
                mCondStr = mCondStr & " And 1=2 "
            End If
        End If



        CreateCondStr = mCondStr
    End Function


    Public Sub ProcFillReport(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try

            Dim mCondStr$ = ""
            Dim mMultiplier As Double


            If ValidateInput() = False Then Exit Sub

            CreateTemporaryTables()




            If mPartyNature.ToUpper = "Customer".ToUpper Then
                RepTitle = "Customer Ledger " & " - " & ReportFrm.FGetText(rowReportType) & " - " & ReportFrm.FGetText(rowGroupOn)
            Else
                RepTitle = "Supplier Ledger " & " - " & ReportFrm.FGetText(rowReportType) & " - " & ReportFrm.FGetText(rowGroupOn)
            End If






            If mFilterGrid IsNot Nothing And mGridRow IsNot Nothing Then
                If mGridRow.DataGridView.Columns.Contains("Search Code") = True Then
                    If mFilterGrid.Item(GFilter, rowReportType).Value = "Party Wise Summary - Ageing" Or mFilterGrid.Item(GFilter, rowReportType).Value = "Party Wise Balance - Ageing" Or mFilterGrid.Item(GFilter, rowReportType).Value = "Party Wise Summary" Or mFilterGrid.Item(GFilter, rowReportType).Value = "Party Wise Detail" Or mFilterGrid.Item(GFilter, rowReportType).Value = "Party Wise Balance" Then
                        If ReportFrm.FGetText(rowNextStep) = "" Then
                            mFilterGrid.Item(GFilter, rowReportType).Value = "Ledger"
                        Else
                            mFilterGrid.Item(GFilter, rowReportType).Value = ReportFrm.FGetText(rowNextStep)
                        End If
                        If ReportFrm.FGetText(rowGroupOn) = "Linked Party" Then
                            mFilterGrid.Item(GFilter, rowLinkedParty).Value = mGridRow.Cells("Party").Value
                            mFilterGrid.Item(GFilterCode, rowLinkedParty).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                        Else
                            mFilterGrid.Item(GFilter, rowParty).Value = mGridRow.Cells("Party").Value
                            mFilterGrid.Item(GFilterCode, rowParty).Value = "'" + mGridRow.Cells("Search Code").Value + "'"
                        End If
                    ElseIf mFilterGrid.Item(GFilter, rowReportType).Value = "Ledger" Or mFilterGrid.Item(GFilter, rowReportType).Value = "Interest Ledger" Then
                        ClsMain.FOpenForm(mGridRow.Cells("Search Code").Value, ReportFrm)
                        ReportFrm.FiterGridCopy_Arr.RemoveAt(ReportFrm.FiterGridCopy_Arr.Count - 1)
                        Exit Sub
                    Else
                        Exit Sub
                    End If
                End If
            End If




            mCondStr = CreateCondStr()


            If ClsMain.FDivisionNameForCustomization(22) = "W SHYAMA SHYAM FABRICS" Or ClsMain.FDivisionNameForCustomization(25) = "SHYAMA SHYAM VENTURES LLP" Or ClsMain.FDivisionNameForCustomization(27) = "W SHYAMA SHYAM VENTURES LLP" Then
                mMultiplier = 0.01
            Else
                mMultiplier = 1.0
            End If




            If ReportFrm.FGetText(rowReportType) = "Party Wise Balance" Then
                FillPartyWiseBalance(mCondStr)

            ElseIf ReportFrm.FGetText(rowReportType) = "Party Wise Summary" Then
                FillPartyWiseSummary(mCondStr)

            ElseIf ReportFrm.FGetText(rowReportType) = "Party Wise Detail" Then
                FillPartyWiseDetail(mCondStr)

            ElseIf ReportFrm.FGetText(rowReportType) = "Party Wise Summary - Ageing" Or ReportFrm.FGetText(rowReportType) = "Party Wise Balance - Ageing" Then

                FillFifoOutstanding(mCondStr)
            Else

                If ReportFrm.FGetText(rowGroupOn) = "Linked Party" Then
                    If (ReportFrm.FGetText(rowParty) = "" Or ReportFrm.FGetText(rowParty).ToString.ToUpper() = "ALL") And (ReportFrm.FGetText(rowLinkedParty) = "" Or ReportFrm.FGetText(rowLinkedParty).ToString.ToUpper() = "ALL") Then
                        MsgBox("Ledger can Not filled for multiple parties")
                        Exit Sub
                    End If
                Else
                    If ReportFrm.FGetText(rowParty) = "" Or ReportFrm.FGetText(rowParty).ToString.ToUpper() = "ALL" Then
                        MsgBox("Ledger can Not filled for multiple parties")
                        Exit Sub
                    End If
                End If



                GetDataReady(mCondStr, ShowDataIn.Grid)

                If ReportFrm.FGetText(rowReportType) = "Ledger" Or ReportFrm.FGetText(rowReportType) = "Interest Ledger" Then
                    Dim sQryPurchaseBrand As String
                    Dim sQrySaleBrand As String

                    DsHeader = AgL.FillData("select * from #TempRecord H ", AgL.GCn)


                    If AgL.PubServerName = "" Then
                        If ClsMain.FDivisionNameForCustomization(20) = "SHYAMA SHYAM FABRICS" Or ClsMain.FDivisionNameForCustomization(22) = "W SHYAMA SHYAM FABRICS" Or ClsMain.FDivisionNameForCustomization(25) = "SHYAMA SHYAM VENTURES LLP" Or ClsMain.FDivisionNameForCustomization(27) = "W SHYAMA SHYAM VENTURES LLP" Then
                            sQryPurchaseBrand = "(Select group_concat(Brand ,',') || ',' From (Select IfNull(sGroup.Description, sItem.Description)  as Brand  from PurchInvoiceDetail sPID  With (NoLock) Left Join PurchInvoiceDetailSku sPIDS On sPID.DocID = sPIDS.DocID Left Join Item sItem On sPID.Item = sItem.Code Left Join Item sGroup On IfNull(sPIDS.ItemGroup,sItem.ItemGroup) = sGroup.Code Where sPID.DocID = PI.DocID And sItem.ItemType  In ('" & ItemTypeCode.TradingProduct & "','Wool') AND IfNull(sGroup.Code,'') not in ('CourierCharge','HandlingCharge') Group By IfNull(sGroup.Description, sItem.Description)))"
                            sQrySaleBrand = "(Select group_concat(Brand ,',') || ',' From (Select IfNull(sGroup.Description, sItem.Description)  as Brand  from SaleInvoiceDetail sSID  With (NoLock) Left Join SaleInvoiceDetailSku sSIDS On sSID.DocID = sSIDS.DocID Left Join Item sItem On sSID.Item = sItem.Code Left Join Item sGroup On IfNull(sSIDS.ItemGroup,sItem.ItemGroup) = sGroup.Code Where sSID.DocID = SI.DocID And sItem.ItemType  In ('" & ItemTypeCode.TradingProduct & "','Wool') AND IfNull(sGroup.Code,'') not in ('CourierCharge','HandlingCharge') Group By IfNull(sGroup.Description, sItem.Description)))"
                        Else
                            sQryPurchaseBrand = "(Select group_concat(Brand ,',') || ',' From (Select sGroup.Description  as Brand  from PurchInvoiceDetailSku sPID  With (NoLock) Left Join Item sGroup On sPID.ItemGroup = sGroup.Code Where sPID.DocID = PI.DocID And sGroup.ItemType  In ('" & ItemTypeCode.TradingProduct & "','Wool') Group By sGroup.Description))"
                            sQrySaleBrand = "(Select group_concat(Brand ,',') || ',' From (Select sGroup.Description  as Brand  from SaleInvoiceDetailSku sSID  With (NoLock) Left Join Item sGroup On sSID.ItemGroup = sGroup.Code Where sSID.DocID = SI.DocID And sGroup.ItemType  In ('" & ItemTypeCode.TradingProduct & "','Wool') Group By sGroup.Description))"
                        End If
                    Else
                        sQryPurchaseBrand = "(Select  IfNull(sGroup.Description, sItem.Description)  +  ','  from PurchInvoiceDetail sPID  With (NoLock) Left Join Item sItem On sPID.Item = sItem.Code Left Join Item sGroup On sItem.ItemGroup = sGroup.Code Where sPID.DocID = PI.DocID Group By IfNull(sGroup.Description, sItem.Description) for xml path(''))"
                        sQrySaleBrand = "(Select  IfNull(sGroup.Description, sItem.Description) + ','  from SaleInvoiceDetail sSID  With (NoLock) Left Join Item sItem On sSID.Item = sItem.Code Left Join Item sGroup On sItem.ItemGroup = sGroup.Code Where sSID.DocID = SI.DocID Group By IfNull(sGroup.Description, sItem.Description)  for xml path(''))"
                    End If


                    mQry = " SELECT H.DocID as SearchCode, H.Sr, (Case When IfNull(Site.ShortName,'') ='' Then '' Else Site.ShortName || '-' End) || (Case When IfNull(H.V_Type,'') ='' Then '' Else H.V_Type || '-' End) || H.RecID as DocNo, H.V_Date as DocDate, 
                        (Case When VT.NCat = '" & Ncat.SaleInvoice & "' Then SIT.LrNo || (Case When SIT.NoOfBales Is Not Null Then ' * ' ||  SIT.NoOfBales Else '' End) When VT.NCat = '" & Ncat.PurchaseInvoice & "' then PIT.LrNo || (Case When PIT.NoOfBales Is Not Null Then ' * ' ||  PIT.NoOfBales Else '' End) Else Null End) as LrNo, 
                        (Case When VT.NCat In ('" & Ncat.SaleInvoice & "','" & Ncat.SaleReturn & "') Then " & sQrySaleBrand & " When VT.NCat In ('" & Ncat.PurchaseInvoice & "', '" & Ncat.PurchaseReturn & "') then " & sQryPurchaseBrand & " Else Null End) as Brand, 
                        (Case When VT.NCat = '" & Ncat.SaleInvoice & "' Then SI.AmsDocNo When VT.NCat = '" & Ncat.PurchaseInvoice & "' then PI.AmsDocNo Else Null End) AmsInvNo, 
                        (Case When VT.NCat = '" & Ncat.SaleInvoice & "' Then SI.AmsDocNetAmount When VT.NCat = '" & Ncat.PurchaseInvoice & "' then PI.AmsDocNetAmount Else 0.00 End) * " & mMultiplier & " AmsInvAmt,                         
                        (Case When VT.NCat = '" & Ncat.SaleInvoice & "' Then SI.Gross_Amount + IfNull(SIL1.TotalDiscount,0) + IfNull(SIL1.TotalAdditionalDiscount,0) - IfNull(SIL1.TotalAddition,0) When VT.NCat = '" & Ncat.PurchaseInvoice & "' then PI.Gross_Amount + IfNull(PIL1.TotalDiscount,0) + IfNull(PIL1.TotalAdditionalDiscount,0) - IfNull(PIL1.TotalAddition,0) Else 0.0 End) * " & mMultiplier & " GoodsValue, 
                        (Case When VT.NCat = '" & Ncat.SaleInvoice & "' Then IfNull(SIL1.TotalDiscount,0) + IfNull(SIL1.TotalAdditionalDiscount,0) - IfNull(SIL1.TotalAddition,0) When VT.NCat = '" & Ncat.PurchaseInvoice & "' then IfNull(PIL1.TotalDiscount,0) + IfNull(PIL1.TotalAdditionalDiscount,0) - IfNull(PIL1.TotalAddition,0) Else 0.0 End) * " & mMultiplier & " Discount, 
                        (Case When VT.NCat = '" & Ncat.SaleInvoice & "' Then SI.Taxable_Amount When VT.NCat = '" & Ncat.PurchaseInvoice & "' then PI.Taxable_Amount Else 0.0 End) * " & mMultiplier & " TaxableAmt, 
                        (Case When VT.NCat = '" & Ncat.SaleInvoice & "' Then SI.Tax1 + SI.Tax2 + SI.Tax3 + SI.Tax4 + SI.Tax5  When VT.NCat = '" & Ncat.PurchaseInvoice & "' then PI.Tax1 + PI.Tax2 + PI.Tax3 + PI.Tax4 + PI.Tax5  Else 0.0 End)  * " & mMultiplier & " TaxAmt,                             
                        (Case When VT.NCat = '" & Ncat.SaleInvoice & "' Then SI.Other_Charge + SI.Other_Charge1 + SI.Other_Charge2 When VT.NCat = '" & Ncat.PurchaseInvoice & "' then PI.Other_Charge + PI.Other_Charge1+ PI.Other_Charge2 Else 0.0 End)  * " & mMultiplier & " PackingFreight,                             
                        (Case When VT.NCat = '" & Ncat.SaleInvoice & "' Then SI.Deduction When VT.NCat = '" & Ncat.PurchaseInvoice & "' then PI.Deduction Else 0.0 End)  * " & mMultiplier & " Deduction,                             
                        (Case When VT.NCat = '" & Ncat.SaleInvoice & "' Then SI.Net_Amount When VT.NCat = '" & Ncat.PurchaseInvoice & "' then PI.Net_Amount When  VT.NCat = '" & Ncat.OpeningBalance & "' Then (Case When Sg.Nature='Customer' Then H.AmtDr Else H.AmtCr End) Else 0.0 End) * " & mMultiplier & " BillAmt,                         
                        (Case When VT.NCat In ('" & Ncat.PurchaseInvoice & "','" & Ncat.PurchaseReturn & "') then PI.Commission + PI.AdditionalCommission Else 0.0 End)  * " & mMultiplier & " Commission,
                        ((Case When VT.NCat = '" & Ncat.PurchaseInvoice & "' then PI.Net_Amount When VT.NCat = '" & Ncat.OpeningBalance & "' Then (Case When Sg.Nature='Customer' Then 0 Else H.AmtCr End) Else 0.0 End) - (Case When VT.NCat  In ('" & Ncat.PurchaseInvoice & "','" & Ncat.PurchaseReturn & "') And SPI.DocId Is Null then PI.Commission + PI.AdditionalCommission Else 0.0 End)) * " & mMultiplier & " as NetPurAmt,
                        H.Adjustment  * " & mMultiplier & " as DebitCredit, 
                        H.GoodsReturn * " & mMultiplier & " as GoodsReturn, 
                        H.Payment  * " & mMultiplier & " as Payment, 
                        ((H.AmtDr-H.AmtCr )  * " & mMultiplier & ")  as Balance, 
                        (Case When IfNull(GenSI.SaletoPartyName,'') <> '' Then IfNull(GenSI.SaletoPartyName,'') || '. ' Else '' End) || (Case When IfNull(ShipParty.Name,'') <> '' then ShipParty.Name || '. ' Else '' End) || IfNull(H.Narration,'') as Narration,
                        H.Clg_Date As ReconciliationDate, SPI.DocID as SettlementDocid
                        From #TempRecord H
                        Left Join Subgroup Sg On H.Subcode = Sg.Subcode
                        Left Join SaleInvoice SI On H.DocID = SI.DocId
                        Left Join (Select SIL.DocID, Sum(SIL.DiscountAmount) as TotalDiscount, 
                                    Sum(SIL.AdditionalDiscountAmount) as TotalAdditionalDiscount, 
                                    Sum(SIL.AdditionAmount) as TotalAddition
                                   From SaleInvoiceDetail SIL
                                   Group By SIL.DocID) as SIL1 On H.DocID = SIL1.DocId  
                        Left Join SaleInvoiceTransport SIT On SI.DocID = SIT.DocID
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
                        Left Join Cloth_SupplierSettlementInvoices SPI On H.DocID = SPI.PurchaseInvoiceDocID And H.Sr = SPI.PurchaseInvoiceDocIDSr "



                    DsHeader = AgL.FillData(mQry, AgL.GCn)


                    If ReportFrm.FGetText(rowReportType) = "Interest Ledger" Then

                        Dim sQryInterestRate As String
                        If AgL.PubServerName = "" Then

                            If ClsMain.FDivisionNameForCustomization(20) = "SHYAMA SHYAM FABRICS" Or ClsMain.FDivisionNameForCustomization(22) = "W SHYAMA SHYAM FABRICS" Or ClsMain.FDivisionNameForCustomization(25) = "SHYAMA SHYAM VENTURES LLP" Or ClsMain.FDivisionNameForCustomization(27) = "W SHYAMA SHYAM VENTURES LLP" Then
                                sQryPurchaseBrand = "(Select group_concat(Brand ,',') || ',' From (Select IfNull(sGroup.Description, sItem.Description)  as Brand  from PurchInvoiceDetail sPID  With (NoLock)  Left Join PurchInvoiceDetailSku sPIDS On sPID.DocID = sPIDS.DocID  Left Join Item sItem On sPID.Item = sItem.Code Left Join Item sGroup On IfNull(sPIDS.ItemGroup,sItem.ItemGroup) = sGroup.Code Where sPID.DocID = PI.DocID And sItem.ItemType  In ('" & ItemTypeCode.TradingProduct & "','Wool') AND IfNull(sGroup.Code,'') not in ('CourierCharge','HandlingCharge','Packing') Group By IfNull(sGroup.Description, sItem.Description)))"
                                sQrySaleBrand = "(Select group_concat(Brand ,',') || ',' From (Select IfNull(sGroup.Description, sItem.Description)  as Brand  from SaleInvoiceDetail sSID  With (NoLock)  Left Join SaleInvoiceDetailSku sSIDS On sSID.DocID = sSIDS.DocID  Left Join Item sItem On sSID.Item = sItem.Code Left Join Item sGroup On IfNull(sSIDS.ItemGroup,sItem.ItemGroup) = sGroup.Code Where sSID.DocID = SI.DocID And sItem.ItemType  In ('" & ItemTypeCode.TradingProduct & "','Wool') AND IfNull(sGroup.Code,'') not in ('CourierCharge','HandlingCharge','Packing') Group By IfNull(sGroup.Description, sItem.Description)))"
                            Else
                                sQryPurchaseBrand = "(Select group_concat(Brand ,',') || ',' From (Select sGroup.Description  as Brand  from PurchInvoiceDetailSku sPID  With (NoLock) Left Join Item sGroup On sPID.ItemGroup = sGroup.Code Where sPID.DocID = PI.DocID And sGroup.ItemType  In ('" & ItemTypeCode.TradingProduct & "','Wool') Group By sGroup.Description))"
                                sQrySaleBrand = "(Select group_concat(Brand ,',') || ',' From (Select sGroup.Description  as Brand  from SaleInvoiceDetailSku sSID  With (NoLock) Left Join Item sGroup On sSID.ItemGroup = sGroup.Code Where sSID.DocID = SI.DocID And sGroup.ItemType  In ('" & ItemTypeCode.TradingProduct & "','Wool') Group By sGroup.Description))"
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
		                                            Where ssg.Subcode=" & IIf(ReportFrm.FGetText(rowGroupOn) = "Linked Party", "H.Subcode", "H.LinkedSubcode") & "
		                                            ) 
                                            And  H.InterestDays > DaysGreaterThan
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



                        mQry = " SELECT (Case When H.V_Date < " & AgL.Chk_Date(mFromDate) & " OR H.DueDate < " & AgL.Chk_Date(mFromDate) & " Then 'Opening' Else Null End) as RecordType,  H.DocID as SearchCode, (Case When IfNull(Site.ShortName,'') ='' Then '' Else Site.ShortName || '-' End) || (Case When IfNull(H.V_Type,'') ='' Then '' Else H.V_Type || '-' End) || H.RecID as DocNo, H.V_Date as DocDate,                         
                        (Case When VT.NCat In ('" & Ncat.SaleInvoice & "', '" & Ncat.SaleReturn & "') Then " & sQrySaleBrand & " When VT.NCat In ('" & Ncat.PurchaseInvoice & "', '" & Ncat.PurchaseReturn & "') then " & sQryPurchaseBrand & " Else Null End) as Brand,                         
                        (Case When VT.NCat = '" & Ncat.SaleInvoice & "' Then SIT.LrNo || (Case When SIT.NoOfBales Is Not Null Then ' * ' ||  SIT.NoOfBales Else '' End) When VT.NCat = '" & Ncat.PurchaseInvoice & "' then PIT.LrNo || (Case When PIT.NoOfBales Is Not Null Then ' * ' ||  PIT.NoOfBales Else '' End) Else Null End) as LrNo,                         
                        (Case When VT.NCat = '" & Ncat.SaleInvoice & "' Then SI.AmsDocNo When VT.NCat = '" & Ncat.PurchaseInvoice & "' then PI.AmsDocNo Else Null End) AmsInvNo, 
                        (Case When VT.NCat = '" & Ncat.SaleInvoice & "' Then SI.AmsDocNetAmount When VT.NCat = '" & Ncat.PurchaseInvoice & "' then PI.AmsDocNetAmount Else 0.00 End) * " & mMultiplier & " AmsInvAmt, 
                        H.AmtDr  * " & mMultiplier & " as AmtDr, 
                        H.AmtCr * " & mMultiplier & " as AmtCr, 
                        H.Balance * " & mMultiplier & " as Balance, 
                        (Case When IfNull(GenSI.SaletoPartyName,'') <> '' Then IfNull(GenSI.SaletoPartyName,'') || '. ' Else '' End) || (Case When IfNull(ShipParty.Name,'') <> '' then ShipParty.Name || '. ' Else '' End) || IfNull(H.Narration,'') as Narration, 
                        H.AdjDocID, H.AdjVAmount * " & mMultiplier & " as Payment, H.AdjDate, H.AdjAmount * " & mMultiplier & " as AdjAmount , 
                        H.InterestDays-IsNull(Ints.LeaverageDays,0) as InterestDays,  
                        H.InterestDays as overDays,
                        " & sQryInterestRate & " as InterestRate,
                        Round(IfNull((H.AdjAmount * abs(H.InterestDays-IsNull(Ints.LeaverageDays,0) ) * " & sQryInterestRate & " / 36500),0)  * " & mMultiplier & ",2) InterestAmount,
                        0.00 as IntBal
                        From #TempInterestRecord H
                        LEFT JOIN SubGroup SG On SG.Subcode =H.SubCode  
                        Left Join Subgroup LSG On H.LinkedSubcode = LSg.Subcode
                        Left Join SaleInvoice SI On H.DocID = SI.DocId
                        Left Join SaleInvoiceTransport SIT On SI.DocID = SIT.DocID
                        Left Join Subgroup ShipParty On SI.ShipToParty = ShipParty.Subcode
                        Left Join PurchInvoice PI On H.DocID = PI.DocId
                        Left Join PurchInvoiceTransport PIT On PI.DocID = PIT.DocID
                        Left Join SaleInvoice GenSI On PI.GenDocId = GenSI.DocId
                        Left Join Voucher_Type Vt on H.V_type = Vt.V_type
                        Left Join Division Div On H.Div_Code = Div.Div_Code 
                        Left Join SiteMast Site On H.Site_Code = Site.Code 
                        Left Join InterestSlab IntS On IfNull(LSg.InterestSlab,Sg.InterestSlab) = Ints.Code "

                        If mFromDate <> "" Then
                            mQry = "Select (Case When V.RecordType='Opening' Then Null Else V.SearchCode End) as SearchCode,
                                    Max(Case When V.RecordType='Opening' Then 'Opening' Else V.DocNo End) as DocNo,       
                                    Max(Case When V.RecordType='Opening' Then " & AgL.Chk_Text(mFromDate) & " Else V.DocDate End) as DocNo,
                                    Max(Case When V.RecordType='Opening' Then Null Else V.Brand End) as Brand,
                                    Max(Case When V.RecordType='Opening' Then Null Else V.LrNo End) as LrNo,
                                    Max(Case When V.RecordType='Opening' Then Null Else V.AmsInvNo End) as AmsInvNo,
                                    Sum(Case When V.RecordType='Opening' Then 0 Else V.AmsInvAmt End) as AmsInvAmt,
                                    (Case When  Sum(Case When V.RecordType='Opening' Then V.AmtDr - V.Payment Else 0 End) > 0 Then Sum(Case When V.RecordType='Opening' Then V.AmtDr - V.Payment Else 0 End) Else Sum(Case When V.RecordType='Opening' Then 0 Else V.AmtDr End) End) as AmtDr,
                                    Sum(V.AmtCr) as AmtCr,
                                    0.0 as Balance,
                                    Max(Case When V.RecordType='Opening' Then Null Else V.Narration End) as Narration,
                                    Max(Case When V.RecordType='Opening' Then Null Else V.AdjDocID End) as AdjDocId,
                                    (Case When  Sum(Case When V.RecordType='Opening' Then V.Payment - V.AmtDr Else 0 End) > 0 Then Sum(Case When V.RecordType='Opening' Then V.Payment - V.AmtDr  Else 0 End) Else Sum(Case When V.RecordType='Opening' Then 0 Else V.Payment End) End) Payment,
                                    Max(Case When V.RecordType='Opening' Then Null Else V.AdjDate End) as AdjDate,
                                    Sum(Case When V.RecordType='Opening' Then 0.00 Else V.AdjAmount End) AdjAmount, 
                                    Max(Case When V.RecordType='Opening' Then Null Else V.InterestDays End) as InterestDays,
                                    Max(Case When V.RecordType='Opening' Then Null Else V.OverDays End) as OverDays,
                                    Max(Case When V.RecordType='Opening' Then Null Else V.InterestRate End) as InterestRate,
                                    Sum(V.InterestAmount) as InterestAmount,
                                    0.0 as IntBal
                                    From (" & mQry & ") as V
                                    Group By (Case When V.RecordType='Opening' Then Null Else V.SearchCode End)
                                    "
                        End If


                        DsHeader = AgL.FillData(mQry, AgL.GCn)

                    End If
                End If
            End If






            If DsHeader.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")
            If mPartyNature.ToUpper = "Customer".ToUpper Then
                ReportFrm.Text = "Customer Ledger " & " - " & ReportFrm.FGetText(rowReportType) & " - " & ReportFrm.FGetText(rowGroupOn)
            Else
                ReportFrm.Text = "Supplier Ledger " & " - " & ReportFrm.FGetText(rowReportType) & " - " & ReportFrm.FGetText(rowGroupOn)
            End If

            'ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = "ProcFillReport"


            ReportFrm.ProcFillGrid(DsHeader)

            'If ReportFrm.FGetText(rowReportType) = "Ledger" Or ReportFrm.FGetText(rowReportType) = "Interest Ledger" Then
            '    Dim I As Integer
            '    Dim mRunningBal As Double
            '    mRunningBal = 0
            '    For I = 0 To ReportFrm.DGL1.RowCount - 1
            '        mRunningBal += Val(ReportFrm.DGL1.Item("Balance", I).Value)
            '        If ReportFrm.FGetText(rowReportType) = "Ledger" Then
            '            mRunningBal += Val(ReportFrm.DGL1.Item("Commission", I).Value)
            '        End If
            '        ReportFrm.DGL1.Item("Balance", I).Value = mRunningBal
            '    Next
            '    ReportFrm.DGL2.Item("Balance", 0).Value = mRunningBal
            'End If

            ReportFrm.DGL1.ReadOnly = True
            For J As Integer = 0 To ReportFrm.DGL1.Columns.Count - 1
                ReportFrm.DGL1.Columns(J).ReadOnly = True
            Next


            If ReportFrm.FGetText(rowReportType) = "Ledger" Then
                Dim I As Integer
                Dim mRunningBal As Double
                mRunningBal = 0
                For I = 0 To ReportFrm.DGL1.RowCount - 1
                    mRunningBal += Val(ReportFrm.DGL1.Item("Balance", I).Value)
                    If AgL.XNull(ReportFrm.DGL1.Item("Settlement Docid", I).Value) = "" Then
                        mRunningBal += Val(ReportFrm.DGL1.Item("Commission", I).Value)
                    End If
                    ReportFrm.DGL1.Item("Balance", I).Value = mRunningBal

                    If ReportFrm.DGL1.Columns.Contains(Col1ReconciliationDate) Then
                        If AgL.XNull(ReportFrm.DGL1.Item(Col1ReconciliationDate, I).Value) <> "" Then
                            ReportFrm.DGL1.Item(Col1ReconciliationDate, I).Value = CDate(ReportFrm.DGL1.Item(Col1ReconciliationDate, I).Value)
                        End If
                    End If
                Next
                ReportFrm.DGL2.Item("Balance", 0).Value = mRunningBal
                ReportFrm.DGL1.Columns("Sr").Visible = False
                ReportFrm.DGL2.Columns("Sr").Visible = False
                If ReportFrm.DGL1.Columns.Contains(Col1ReconciliationDate) Then
                    ReportFrm.DGL1.Columns(Col1ReconciliationDate).Visible = True
                    ReportFrm.DGL1.ReadOnly = False
                    For J As Integer = 0 To ReportFrm.DGL1.Columns.Count - 1
                        ReportFrm.DGL1.Columns(J).ReadOnly = True
                    Next
                    ReportFrm.DGL1.Columns(Col1ReconciliationDate).ReadOnly = False
                End If
            End If


            If ReportFrm.FGetText(rowReportType) = "Interest Ledger" Then
                Dim I As Integer
                Dim mRunningBal As Double
                Dim mRunningIntBal As Double
                Dim mTotalPayment As Double
                mRunningBal = 0
                mRunningIntBal = 0
                mTotalPayment = 0
                For I = 0 To ReportFrm.DGL1.RowCount - 1
                    mRunningBal += Val(ReportFrm.DGL1.Item("Amt Dr", I).Value)
                    ReportFrm.DGL1.Item("Balance", I).Value = mRunningBal
                    mRunningIntBal += Val(ReportFrm.DGL1.Item("Interest Amount", I).Value)
                    ReportFrm.DGL1.Item("Int Bal", I).Value = mRunningIntBal
                    mTotalPayment += Val(ReportFrm.DGL1.Item("Payment", I).Value)
                Next
                ReportFrm.DGL2.Item("Balance", 0).Value = Format(mRunningBal - mTotalPayment, "0.00")
                ReportFrm.DGL2.Item("Int Bal", 0).Value = Format(mRunningIntBal, "0.00")
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
            DsHeader = Nothing
        Finally
            'ReportFrm.DGL2.Visible = False
        End Try
    End Sub

    Private Sub GetDataReadyForFIFOBalance(mCondStr As String)

        Dim mFromDate As String
        If ReportFrm.FGetText(rowReportType) = "Interest Ledger" Or ReportFrm.FGetText(rowNextStep) = "Interest Ledger" Then
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

    Private Function FillFifoOutstandingBackup(mCondstr As String, Optional Purpose As String = "") As DataSet
        Dim mRemainingBalance As Double
        Dim i As Integer, j As Integer
        Dim dtParty As DataTable

        Dim DtMain As DataTable
        Dim BalAmount As Double
        Dim DrCr As String




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

        If Purpose = "" Then

            mQry = "Select H.Subcode as SearchCode, Sg.name || (Case When IfNull(City.CityName,'') <> '' Then ', ' || IfNull(City.CityName,'') else '' End) as Party,                            
                            Sum(CASE WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(CurrentMonth) & ") Then H.BalanceAmount ELSE 0 END ) AS [" & CurrentMonth.ToString("MMM-yy").Replace("-", " ") & "],
                            Sum(CASE WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(OneMonthBack) & ") Then H.BalanceAmount ELSE 0 END ) AS [" & OneMonthBack.ToString("MMM-yy").Replace("-", " ") & "],
                            Sum(CASE WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(TwoMonthBack) & ") Then H.BalanceAmount ELSE 0 END ) AS [" & TwoMonthBack.ToString("MMM-yy").Replace("-", " ") & "],
                            Sum(CASE WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(ThreeMonthBack) & ") Then H.BalanceAmount ELSE 0 END ) AS [" & ThreeMonthBack.ToString("MMM-yy").Replace("-", " ") & "],
                            Sum(CASE WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(FourMonthBack) & ") Then H.BalanceAmount ELSE 0 END ) AS [" & FourMonthBack.ToString("MMM-yy").Replace("-", " ") & "],
                            Sum(CASE WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(FiveMonthBack) & ") Then H.BalanceAmount ELSE 0 END ) AS [" & FiveMonthBack.ToString("MMM-yy").Replace("-", " ") & "],
                            Sum(CASE WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(SixMonthBack) & ") Then H.BalanceAmount ELSE 0 END ) AS [" & SixMonthBack.ToString("MMM-yy").Replace("-", " ") & "],
                            Sum(CASE WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(SevenMonthBack) & ") Then H.BalanceAmount ELSE 0 END ) AS [" & SevenMonthBack.ToString("MMM-yy").Replace("-", " ") & "],
                            Sum(CASE WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(EightMonthBack) & ") Then H.BalanceAmount ELSE 0 END ) AS [" & EightMonthBack.ToString("MMM-yy").Replace("-", " ") & "],
                            Sum(CASE WHEN strftime('%Y%m', H.V_Date) < strftime('%Y%m', " & AgL.Chk_Date(EightMonthBack) & ") Then H.BalanceAmount ELSE 0 END ) AS [Before " & EightMonthBack.ToString("MMM-yy").Replace("-", " ") & "],
                            Sum(H.BalanceAmount) As [Balance],
                            Sum(H.DrCr) As [DrCr]                                                                                                                
                            From #FifoOutstanding H
                            Left Join Subgroup Sg on H.Subcode = Sg.Subcode
                            Left Join City On Sg.CityCode = City.CityCode
                            Group By H.Subcode, Sg.name || (Case When IfNull(City.CityName,'') <> '' Then ', ' || IfNull(City.CityName,'') else '' End)
                            Order By Sg.name || (Case When IfNull(City.CityName,'') <> '' Then ', ' || IfNull(City.CityName,'') else '' End)
                            "

            DsHeader = AgL.FillData(mQry, AgL.GCn)
            'FillFifoOutstanding = DsHeader
        Else

            Dim mMultiplier As Double
            If ClsMain.FDivisionNameForCustomization(22) = "W SHYAMA SHYAM FABRICS" Or ClsMain.FDivisionNameForCustomization(27) = "W SHYAMA SHYAM VENTURES LLP" Then
                mMultiplier = 0.01
            Else
                mMultiplier = 1.0
            End If

            mQry = "Select H.Subcode as SearchCode, Sg.name || (Case When IfNull(City.CityName,'') <> '' Then ', ' || IfNull(City.CityName,'') else '' End) as Party,                            
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
            FillFifoOutstandingBackup = AgL.FillData(mQry, AgL.GCn)
        End If
    End Function

    Private Function FillFifoOutstanding(mCondstr As String, Optional Purpose As String = "") As DataSet
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




        mQry = "Select Sg.Subcode, Max(Sg.Nature) as Nature, Sum(Lg.AmtDr)-Sum(Lg.AmtCr) + IfNull(Sum(PI.Commission + PI.AdditionalCommission),0)  as Balance
                            From Ledger Lg "
        If ReportFrm.FGetText(rowGroupOn) = "Linked Party" Then
            mQry = mQry & " Left Join Subgroup Sg On IfNull(Lg.LinkedSubcode,Lg.Subcode) = Sg.Subcode "
            mQry = mQry & " Left Join Subgroup PSg On IfNull(Lg.LinkedSubcode,Lg.Subcode) = PSg.Subcode "
        Else
            mQry = mQry & " Left Join Subgroup Sg On Lg.Subcode = Sg.Subcode "
            mQry = mQry & " Left Join Subgroup PSg On Sg.Parent = PSg.Subcode "
        End If
        mQry = mQry & " Left Join(Select SILTV.Subcode, Max(SILTV.Agent) As Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode) as LTV On Sg.Subcode = LTV.Subcode
                                    "
        mQry = mQry & " Left Join PurchInvoice PI On LG.DocID = PI.DocId "
        mQry = mQry & " Where 1 = 1  "
        mQry = mQry & mCondstr
        mQry = mQry & " Group By Sg.Subcode"
        If ReportFrm.FGetText(rowReportType) = "Party Wise Balance - Ageing" Then
            mQry = mQry & " Having Sum(Lg.AmtDr)-Sum(Lg.AmtCr) <> 0 "
        End If

        dtParty = AgL.FillData(mQry, AgL.GCn).Tables(0)



        mQry = "Select Lg.DocID, Lg.DivCode, Lg.Site_Code, Lg.V_Type, VT.NCat, Vt.Description as V_TypeDesc, 
                                    IfNull(PI.VendorDocNo,Lg.RecId) as RecId, SG.Subcode, IfNull(Lg.EffectiveDate, Lg.V_Date) As V_Date, Lg.Narration, 
                                    (Case  When IfNull(Trd.Type,'')='Cancelled' OR IfNull(Trr.Type,'')='Cancelled' Then 0 Else LG.AmtDr End) + (Case  When IfNull(Trd.Type,'')='Cancelled' OR IfNull(Trr.Type,'')='Cancelled' Then 0 Else LG.AmtCr End) as Amount,
                                    (Case  When IfNull(Trd.Type,'')='Cancelled' OR IfNull(Trr.Type,'')='Cancelled' Then 0 Else LG.AmtDr End) AmtDr, 
                                    (Case  When IfNull(Trd.Type,'')='Cancelled' OR IfNull(Trr.Type,'')='Cancelled' Then 0 Else LG.AmtCr End) AmtCr 
                                    From Ledger Lg  With (NoLock) 
                                    Left Join PurchInvoice PI On Lg.DocID = PI.DocID "
        If ReportFrm.FGetText(rowGroupOn) = "Linked Party" Then
            mQry = mQry & " Left Join SubGroup SG On SG.Subcode =IfNull(Lg.LinkedSubcode,LG.SubCode) "
            mQry = mQry & " Left Join Subgroup PSg On IfNull(Lg.LinkedSubcode,Lg.Subcode) = PSg.Subcode "
        Else
            mQry = mQry & " Left Join SubGroup SG On SG.Subcode =LG.SubCode "
            mQry = mQry & " Left Join Subgroup PSg On Sg.Parent = PSg.Subcode "
        End If
        mQry = mQry & " Left Join(Select SILTV.Subcode, Max(SILTV.Agent) As Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode) as LTV On Sg.Subcode = LTV.Subcode
                        Left Join Voucher_Type Vt On Lg.V_Type = Vt.V_Type
                        Left Join TransactionReferences Trd With (NoLock) On Lg.DocID = Trd.DocId And Trd.DocIDSr=LG.V_Sno And Lg.V_Date >= '2019-07-01'
                        Left Join TransactionReferences Trr With (NoLock) On Lg.DocID = Trr.ReferenceDocId And Trr.ReferenceSr=Lg.V_Sno And Lg.V_Date >= '2019-07-01'
                        Where 1=1  " & mCondstr & " 
                        Order By IfNull(Lg.EffectiveDate, Lg.V_Date) Desc, Lg.RecId desc"

        dtLedger = AgL.FillData(mQry, AgL.GCn).Tables(0)








        If dtParty.Rows.Count > 0 Then
            For i = 0 To dtParty.Rows.Count - 1
                mQry = ""
                If AgL.XNull(dtParty.Rows(i)("Nature")) = "Customer" Then
                    If AgL.VNull(dtParty.Rows(i)("Balance")) > 0 Then
                        mQry = "Select Lg.DocID, Lg.DivCode, Lg.Site_Code, Lg.V_Type, Vt.Description as V_TypeDesc, 
                                    IfNull(PI.VendorDocNo,Lg.RecId) as RecID, SG.Subcode, IfNull(Lg.EffectiveDate, Lg.V_Date) As V_Date, Lg.Narration, Lg.AmtDr as Amount                                
                                    From Ledger Lg  With (NoLock) 
                                    Left Join PurchInvoice PI On LG.DocID = PI.DocId "
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
                        If ReportFrm.FGetText(rowReportType) = "Party Wise Summary - Ageing" Or Purpose <> "" Then
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
                                                " & Val((AgL.VNull(dtParty.Rows(i)("Balance")))) & ",
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
                                    IfNull(PI.VendorDocNo,Lg.RecId) as RecID, SG.Subcode, IfNull(Lg.EffectiveDate, Lg.V_Date) As V_Date, Lg.Narration, Lg.AmtCr as Amount                                
                                    From Ledger Lg  With (NoLock) 
                                    Left Join PurchInvoice PI On LG.DocID = PI.DocID"
                        If ReportFrm.FGetText(rowGroupOn) = "Linked Party" Then
                            mQry = mQry & " Left Join SubGroup SG On SG.Subcode =IfNull(Lg.LinkedSubcode,LG.SubCode) "
                        Else
                            mQry = mQry & " Left Join SubGroup SG On SG.Subcode =LG.SubCode "
                        End If

                        mQry = mQry & " Left Join(Select SILTV.Subcode, Max(SILTV.Agent) As Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode) as LTV On Sg.Subcode = LTV.Subcode
                                    Left Join Voucher_Type Vt On Lg.V_Type = Vt.V_Type
                                    Where Sg.Subcode = '" & AgL.XNull(dtParty.Rows(i)("Subcode")) & "'  And Lg.AmtCr > 0 " & mCondstr & " 
                                    Order By IfNull(Lg.EffectiveDate, Lg.V_Date) Desc, IfNull(PI.VendorDocNo,Lg.RecId) desc"
                    Else
                        If ReportFrm.FGetText(rowReportType) = "Party Wise Summary - Ageing" Or Purpose <> "" Then
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
                                                " & Val(AgL.VNull(dtParty.Rows(i)("Balance"))) * -1.0 & ",
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
                        drInvoices = dtLedger.Select(" Subcode = '" & AgL.XNull(dtParty.Rows(i)("Subcode")) & "'  And AmtDr > 0  ", " V_Date Desc ")
                    Else
                        drInvoices = dtLedger.Select(" Subcode = '" & AgL.XNull(dtParty.Rows(i)("Subcode")) & "'  And AmtCr > 0  ", " V_Date Desc ")
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

        Dim mBillsUpToDate As Date = CDate(ReportFrm.FGetText(rowBillsUptoDate)).ToString("s")
        Dim CurrentMonth As Date = CDate(mBillsUpToDate)
        Dim OneMonthBack As Date = DateAdd(DateInterval.Month, -1, mBillsUpToDate)
        Dim TwoMonthBack As Date = DateAdd(DateInterval.Month, -2, mBillsUpToDate)
        Dim ThreeMonthBack As Date = DateAdd(DateInterval.Month, -3, mBillsUpToDate)
        Dim FourMonthBack As Date = DateAdd(DateInterval.Month, -4, mBillsUpToDate)
        Dim FiveMonthBack As Date = DateAdd(DateInterval.Month, -5, mBillsUpToDate)
        Dim SixMonthBack As Date = DateAdd(DateInterval.Month, -6, mBillsUpToDate)
        Dim SevenMonthBack As Date = DateAdd(DateInterval.Month, -7, mBillsUpToDate)
        Dim EightMonthBack As Date = DateAdd(DateInterval.Month, -8, mBillsUpToDate)
        Dim NineMonthBack As Date = DateAdd(DateInterval.Month, -9, mBillsUpToDate)

        If Purpose = "" Then

            mQry = "Select H.Subcode as SearchCode, Sg.name || (Case When IfNull(City.CityName,'') <> '' Then ', ' || IfNull(City.CityName,'') else '' End) as Party,                            
                            Sum(CASE WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(CurrentMonth) & ") Then H.BalanceAmount ELSE 0 END ) AS [" & CurrentMonth.ToString("MMM-yy").Replace("-", " ") & "],
                            Sum(CASE WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(OneMonthBack) & ") Then H.BalanceAmount ELSE 0 END ) AS [" & OneMonthBack.ToString("MMM-yy").Replace("-", " ") & "],
                            Sum(CASE WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(TwoMonthBack) & ") Then H.BalanceAmount ELSE 0 END ) AS [" & TwoMonthBack.ToString("MMM-yy").Replace("-", " ") & "],
                            Sum(CASE WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(ThreeMonthBack) & ") Then H.BalanceAmount ELSE 0 END ) AS [" & ThreeMonthBack.ToString("MMM-yy").Replace("-", " ") & "],
                            Sum(CASE WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(FourMonthBack) & ") Then H.BalanceAmount ELSE 0 END ) AS [" & FourMonthBack.ToString("MMM-yy").Replace("-", " ") & "],
                            Sum(CASE WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(FiveMonthBack) & ") Then H.BalanceAmount ELSE 0 END ) AS [" & FiveMonthBack.ToString("MMM-yy").Replace("-", " ") & "],
                            Sum(CASE WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(SixMonthBack) & ") Then H.BalanceAmount ELSE 0 END ) AS [" & SixMonthBack.ToString("MMM-yy").Replace("-", " ") & "],
                            Sum(CASE WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(SevenMonthBack) & ") Then H.BalanceAmount ELSE 0 END ) AS [" & SevenMonthBack.ToString("MMM-yy").Replace("-", " ") & "],
                            Sum(CASE WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(EightMonthBack) & ") Then H.BalanceAmount ELSE 0 END ) AS [" & EightMonthBack.ToString("MMM-yy").Replace("-", " ") & "],
                            Sum(CASE WHEN strftime('%Y%m', H.V_Date) < strftime('%Y%m', " & AgL.Chk_Date(EightMonthBack) & ") Then H.BalanceAmount ELSE 0 END ) AS [Before " & EightMonthBack.ToString("MMM-yy").Replace("-", " ") & "],
                            Sum(CASE WHEN H.V_Date <= " & AgL.Chk_Date(mBillsUpToDate) & " Then H.BalanceAmount ELSE 0 END ) As [Balance],
                            Sum(H.DrCr) As [DrCr]                                                      
                            From #FifoOutstanding H
                            Left Join Subgroup Sg on H.Subcode = Sg.Subcode
                            Left Join City On Sg.CityCode = City.CityCode
                            Left join PurchInvoice PI On H.DocID = PI.DocId
                            Group By H.Subcode, Sg.name || (Case When IfNull(City.CityName,'') <> '' Then ', ' || IfNull(City.CityName,'') else '' End)
                            Order By Sg.name || (Case When IfNull(City.CityName,'') <> '' Then ', ' || IfNull(City.CityName,'') else '' End)
                            "

            DsHeader = AgL.FillData(mQry, AgL.GCn)
            'FillFifoOutstanding = DsHeader
        Else

            Dim mMultiplier As Double
            If ClsMain.FDivisionNameForCustomization(22) = "W SHYAMA SHYAM FABRICS" Or ClsMain.FDivisionNameForCustomization(27) = "W SHYAMA SHYAM VENTURES LLP" Then
                mMultiplier = 0.01
            Else
                mMultiplier = 1.0
            End If

            mQry = "Select H.Subcode as SearchCode, 1 as Sr, Sg.name || (Case When IfNull(City.CityName,'') <> '' Then ', ' || IfNull(City.CityName,'') else '' End) as Party,                            
                            Sum(CASE WHEN H.V_Date <= " & AgL.Chk_Date(mBillsUpToDate) & " Then H.BalanceAmount ELSE 0 END ) * " & mMultiplier & " as BalanceAmount, 

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
        End If
    End Function

    Private Function FillFifoOutstandingOld(mCondstr As String, Optional Purpose As String = "") As DataSet
        Dim mRemainingBalance As Double
        Dim i As Integer, j As Integer
        Dim dtParty As DataTable

        Dim DtMain As DataTable
        Dim BalAmount As Double
        Dim DrCr As String
        Dim dtLedger As DataTable
        Dim drInvoices As DataRow()




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
                        drInvoices = dtLedger.Select(" Subcode = '" & AgL.XNull(dtParty.Rows(i)("Subcode")) & "'  And AmtDr > 0 ")
                    Else
                        drInvoices = dtLedger.Select(" Subcode = '" & AgL.XNull(dtParty.Rows(i)("Subcode")) & "'  And AmtCr > 0 ")
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

        If Purpose = "" Then

            mQry = "Select H.Subcode as SearchCode, Sg.name || (Case When IfNull(City.CityName,'') <> '' Then ', ' || IfNull(City.CityName,'') else '' End) as Party,                            
                            Sum(CASE WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(CurrentMonth) & ") Then H.BalanceAmount ELSE 0 END ) AS [" & CurrentMonth.ToString("MMM-yy").Replace("-", " ") & "],
                            Sum(CASE WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(OneMonthBack) & ") Then H.BalanceAmount ELSE 0 END ) AS [" & OneMonthBack.ToString("MMM-yy").Replace("-", " ") & "],
                            Sum(CASE WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(TwoMonthBack) & ") Then H.BalanceAmount ELSE 0 END ) AS [" & TwoMonthBack.ToString("MMM-yy").Replace("-", " ") & "],
                            Sum(CASE WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(ThreeMonthBack) & ") Then H.BalanceAmount ELSE 0 END ) AS [" & ThreeMonthBack.ToString("MMM-yy").Replace("-", " ") & "],
                            Sum(CASE WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(FourMonthBack) & ") Then H.BalanceAmount ELSE 0 END ) AS [" & FourMonthBack.ToString("MMM-yy").Replace("-", " ") & "],
                            Sum(CASE WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(FiveMonthBack) & ") Then H.BalanceAmount ELSE 0 END ) AS [" & FiveMonthBack.ToString("MMM-yy").Replace("-", " ") & "],
                            Sum(CASE WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(SixMonthBack) & ") Then H.BalanceAmount ELSE 0 END ) AS [" & SixMonthBack.ToString("MMM-yy").Replace("-", " ") & "],
                            Sum(CASE WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(SevenMonthBack) & ") Then H.BalanceAmount ELSE 0 END ) AS [" & SevenMonthBack.ToString("MMM-yy").Replace("-", " ") & "],
                            Sum(CASE WHEN strftime('%Y%m', H.V_Date) = strftime('%Y%m', " & AgL.Chk_Date(EightMonthBack) & ") Then H.BalanceAmount ELSE 0 END ) AS [" & EightMonthBack.ToString("MMM-yy").Replace("-", " ") & "],
                            Sum(CASE WHEN strftime('%Y%m', H.V_Date) < strftime('%Y%m', " & AgL.Chk_Date(EightMonthBack) & ") Then H.BalanceAmount ELSE 0 END ) AS [Before " & EightMonthBack.ToString("MMM-yy").Replace("-", " ") & "],
                            Sum(H.BalanceAmount) As [Balance],
                            Sum(H.DrCr) As [DrCr]                                                                                                                
                            From #FifoOutstanding H
                            Left Join Subgroup Sg on H.Subcode = Sg.Subcode
                            Left Join City On Sg.CityCode = City.CityCode
                            Group By H.Subcode, Sg.name || (Case When IfNull(City.CityName,'') <> '' Then ', ' || IfNull(City.CityName,'') else '' End)
                            Order By Sg.name || (Case When IfNull(City.CityName,'') <> '' Then ', ' || IfNull(City.CityName,'') else '' End)
                            "

            DsHeader = AgL.FillData(mQry, AgL.GCn)
            'FillFifoOutstanding = DsHeader
        Else

            Dim mMultiplier As Double
            If ClsMain.FDivisionNameForCustomization(22) = "W SHYAMA SHYAM FABRICS" Or ClsMain.FDivisionNameForCustomization(27) = "W SHYAMA SHYAM VENTURES LLP" Then
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
            FillFifoOutstandingOld = AgL.FillData(mQry, AgL.GCn)
        End If
    End Function

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
                    Sr nVarchar(10),
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
                    InterestBalance Float,
                    Clg_Date nVarchar(100)
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

    Private Sub FillPartyWiseBalance(mCondstr As String)
        mQry = "Select Sg.subcode as SearchCode, Sg.name || (Case When IfNull(City.CityName,'') <> '' Then ', ' || IfNull(City.CityName,'') else '' End) as Party, sAgent.Name as Agent,
                (Sum(Lg.AmtDr)-Sum(Lg.AmtCr)) as Balance, 
                (Case When Sum(Lg.AmtDr)-Sum(Lg.AmtCr) > 0 Then 'Dr'  When Sum(Lg.AmtDr)-Sum(Lg.AmtCr) < 0 Then 'Cr' Else '' End) as DrCr,
                Sum(Lg.AmtDr)-Sum(Lg.AmtCr) + IfNull(Sum(PI.Commission + PI.AdditionalCommission),0)  as NetBalance 
                From Ledger Lg "
        If ReportFrm.FGetText(rowGroupOn) = "Linked Party" Then
            mQry = mQry & " Left Join Subgroup Sg On IfNull(Lg.LinkedSubcode,Lg.Subcode) = Sg.Subcode "
            mQry = mQry & " Left Join Subgroup PSg On IfNull(Lg.LinkedSubcode,Lg.Subcode) = PSg.Subcode "
        Else
            mQry = mQry & " Left Join Subgroup Sg On Lg.Subcode = Sg.Subcode "
            mQry = mQry & " Left Join Subgroup PSg On Sg.Parent = PSg.Subcode "
        End If
        mQry = mQry & " Left join PurchInvoice PI On LG.DocID = PI.DocID
                        Left Join City On Sg.CityCode = City.CityCode
                        Left Join (Select SILTV.Subcode, Max(SILTV.Agent) as Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode) as LTV On Sg.Subcode = LTV.Subcode
                        Left Join Subgroup sAgent On LTV.Agent = sAgent.Subcode
                        Where 1=1 "
        mQry = mQry & mCondstr
        mQry = mQry & " Group By Sg.Subcode Having Sum(Lg.AmtDr)-Sum(Lg.AmtCr)<>0
                        Order By Sg.Name || (Case When IfNull(City.CityName,'') <> '' Then ', ' || IfNull(City.CityName,'') else '' End) "

        DsHeader = AgL.FillData(mQry, AgL.GCn)
    End Sub

    Private Sub FillPartyWiseSummary(mCondStr As String)
        Dim summaryFromDate As String
        If ReportFrm.FGetText(rowFromDate) <> "" Then
            summaryFromDate = ReportFrm.FGetText(rowFromDate)
        Else
            summaryFromDate = AgL.PubStartDate
        End If
        mQry = "Select Sg.Subcode as SearchCode, Sg.name || (Case When IfNull(City.CityName,'') <> '' Then ', ' || IfNull(City.CityName,'') else '' End) as Party, 
                            Abs(Sum(Case When Date(Lg.V_Date) < " & AgL.Chk_Date(summaryFromDate) & " Then Lg.AmtDr-Lg.AmtCr Else 0 End))    as Opening,
                            (Case When Sum(Case When Date(Lg.V_Date) < " & AgL.Chk_Date(summaryFromDate) & " Then Lg.AmtDr-Lg.AmtCr Else 0 End) >= 0 Then 'Dr' Else 'Cr' End)    as ot,
                            Sum(Case When Date(Lg.V_Date) >= " & AgL.Chk_Date(summaryFromDate) & " Then Lg.AmtDr Else 0 End)    as Debit,
                            Sum(Case When Date(Lg.V_Date) >= " & AgL.Chk_Date(summaryFromDate) & " Then Lg.AmtCr Else 0 End)    as Credit,
                            Abs(Sum(Lg.AmtDr)-Sum(Lg.AmtCr)) as Closing, 
                            (Case When Sum(Lg.AmtDr)-Sum(Lg.AmtCr) >= 0 Then 'Dr' Else 'Cr'  End) as ct,
                            Sum(LG.AmtDr) - Sum(LG.AmtCr) + IfNull(Sum(PI.Commission + PI.AdditionalCommission),0) as NetClosing
                            From Ledger Lg "

        If ReportFrm.FGetText(rowGroupOn) = "Linked Party" Then
            mQry = mQry & " Left Join Subgroup Sg On IfNull(Lg.LinkedSubcode,Lg.Subcode) = Sg.Subcode "
            mQry = mQry & " Left Join Subgroup PSg On IfNull(Lg.LinkedSubcode,Lg.Subcode) = PSg.Subcode "
        Else
            mQry = mQry & " Left Join Subgroup Sg On Lg.Subcode = Sg.Subcode "
            mQry = mQry & " Left Join Subgroup PSg On Sg.Parent = PSg.Subcode "
        End If

        mQry = mQry & "     Left Join PurchInvoice PI On LG.DocID = PI.DocID
                            Left Join City On Sg.CityCode = City.CityCode
                            Left Join (Select SILTV.Subcode, Max(SILTV.Agent) as Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode) as LTV On Sg.Subcode = LTV.Subcode
                            Where 1=1 "
        mQry = mQry & mCondStr
        mQry = mQry & " Group By Sg.Subcode"

        DsHeader = AgL.FillData(mQry, AgL.GCn)

    End Sub


    Private Sub FillPartyWiseDetail(mCondStr As String)
        Dim summaryFromDate As String
        If ReportFrm.FGetText(rowFromDate) <> "" Then
            summaryFromDate = ReportFrm.FGetText(rowFromDate)
        Else
            summaryFromDate = AgL.PubStartDate
        End If
        If mPartyNature.ToUpper = "Customer".ToUpper Then

        End If

        mQry = "Select Sg.Subcode as SearchCode, Sg.name || (Case When IfNull(City.CityName,'') <> '' Then ', ' || IfNull(City.CityName,'') else '' End) as Party, 
                            Abs(Sum(Case When Date(Lg.V_Date) < " & AgL.Chk_Date(summaryFromDate) & " Then Lg.AmtDr-Lg.AmtCr Else 0 End))    as Opening,
                            (Case When Sum(Case When Date(Lg.V_Date) < " & AgL.Chk_Date(summaryFromDate) & " Then Lg.AmtDr-Lg.AmtCr Else 0 End) >= 0 Then 'Dr' Else 'Cr' End)    as ot,
                            Sum(Case When Date(Lg.V_Date) >= " & AgL.Chk_Date(summaryFromDate) & " Then Lg.AmtDr Else 0 End)    as Debit,
                            Sum(Case When Date(Lg.V_Date) >= " & AgL.Chk_Date(summaryFromDate) & " Then Lg.AmtCr Else 0 End)    as Credit,                            
                            Abs(Sum(Lg.AmtDr)-Sum(Lg.AmtCr)) as Closing, 
                            (Case When Sum(Lg.AmtDr)-Sum(Lg.AmtCr) >= 0 Then 'Dr' Else 'Cr'  End) as ct,
                            Sum(LG.AmtDr) - Sum(LG.AmtCr) + IfNull(Sum(PI.Commission + PI.AdditionalCommission),0) as NetClosing,
                            Sum(Case When Date(Lg.V_Date) >= " & AgL.Chk_Date(summaryFromDate) & " And Vt.NCat = '" & Ncat.SaleInvoice & "'  Then Lg.AmtDr Else 0 End)    as SalesDr,
                            Sum(Case When Date(Lg.V_Date) >= " & AgL.Chk_Date(summaryFromDate) & " And Vt.NCat = '" & Ncat.PurchaseInvoice & "'  Then Lg.AmtCr Else 0 End)    as PurchaseCr,
                            Sum(Case When Date(Lg.V_Date) >= " & AgL.Chk_Date(summaryFromDate) & " And Vt.NCat = '" & Ncat.SaleReturn & "' Then Lg.AmtCr Else 0 End)    as SalesReturnCr,                            
                            Sum(Case When Date(Lg.V_Date) >= " & AgL.Chk_Date(summaryFromDate) & " And Vt.NCat = '" & Ncat.PurchaseReturn & "' Then Lg.AmtDr Else 0 End)    as PurchaseReturnDr,                            
                            Sum(Case When Date(Lg.V_Date) >= " & AgL.Chk_Date(summaryFromDate) & " And Vt.NCat = '" & Ncat.Receipt & "' Then Lg.AmtCr Else 0 End)    as ReceiptCr,                            
                            Sum(Case When Date(Lg.V_Date) >= " & AgL.Chk_Date(summaryFromDate) & " And Vt.NCat = '" & Ncat.Payment & "' Then Lg.AmtDr Else 0 End)    as PaymentDr,                            
                            Sum(Case When Date(Lg.V_Date) >= " & AgL.Chk_Date(summaryFromDate) & " And (IfNull(LG.ReferenceDocId,'') Like '%WRS%' or IfNull(LG.ReferenceDocID,'') Like '%WPS%')  Then Lg.AmtCr Else 0 End)    as SettlementCr,                            
                            Sum(Case When Date(Lg.V_Date) >= " & AgL.Chk_Date(summaryFromDate) & " And (IfNull(LG.ReferenceDocId,'') Like '%WRS%' or IfNull(LG.ReferenceDocID,'') Like '%WPS%')  Then Lg.AmtDr Else 0 End)    as SettlementDr,                            
                            Sum(Case When Date(Lg.V_Date) >= " & AgL.Chk_Date(summaryFromDate) & " And Vt.NCat Not In ( '" & Ncat.PurchaseInvoice & "', '" & Ncat.SaleReturn & "', '" & Ncat.Receipt & "') And IfNull(LG.ReferenceDocId,'') Not Like '%WRS%' and IfNull(LG.ReferenceDocId,'') Not Like '%WPS%'  Then Lg.AmtCr Else 0 End)    as OtherCr,                            
                            Sum(Case When Date(Lg.V_Date) >= " & AgL.Chk_Date(summaryFromDate) & " And Vt.NCat Not In ( '" & Ncat.SaleInvoice & "', '" & Ncat.PurchaseReturn & "', '" & Ncat.Payment & "') And IfNull(LG.ReferenceDocId,'') Not Like '%WRS%' and IfNull(LG.ReferenceDocId,'') Not Like '%WPS%'  Then Lg.AmtDr Else 0 End)    as OtherDr                            
                            From Ledger Lg "

        If ReportFrm.FGetText(rowGroupOn) = "Linked Party" Then
            mQry = mQry & " Left Join Subgroup Sg On IfNull(Lg.LinkedSubcode,Lg.Subcode) = Sg.Subcode "
            mQry = mQry & " Left Join Subgroup PSg On IfNull(Lg.LinkedSubcode,Lg.Subcode) = PSg.Subcode "
        Else
            mQry = mQry & " Left Join Subgroup Sg On Lg.Subcode = Sg.Subcode "
            mQry = mQry & " Left Join Subgroup PSg On Sg.Parent = PSg.Subcode "
        End If

        mQry = mQry & "     Left Join PurchInvoice PI On LG.DocID = PI.DocID
                            Left Join City On Sg.CityCode = City.CityCode
                            Left Join (Select SILTV.Subcode, Max(SILTV.Agent) as Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode) as LTV On Sg.Subcode = LTV.Subcode
                            Left Join Voucher_Type Vt on Lg.V_Type = Vt.V_Type
                            Where 1=1 "
        mQry = mQry & mCondStr
        mQry = mQry & " Group By Sg.Subcode"

        DsHeader = AgL.FillData(mQry, AgL.GCn)

    End Sub

    Private Sub GetDataReady(mCondStr As String, ShowDataIn As ShowDataIn)


        Dim mFromDate As String
        If ReportFrm.FGetText(rowReportType) = "Interest Ledger" Or ReportFrm.FGetText(rowNextStep) = "Interest Ledger" Then
            mFromDate = ""
        Else
            mFromDate = ReportFrm.FGetText(rowFromDate)
        End If





        Dim SubCode As String = "", Party As String = "", PCity As String = "", SiteCode As String = "", DivCode As String = ""
        Dim Cr As Double = 0, Adv As Double = 0
        Dim runningDr As Double = 0






        Dim DtLedger As DataTable = Nothing




        If Not (ReportFrm.FGetText(rowReportType) = "Interest Ledger" Or ReportFrm.FGetText(rowNextStep) = "Interest Ledger") Then
            GetDataReadyForFIFOBalance(mCondStr)
        End If


        mQry = " SELECT LG.DocId, Lg.V_SNo, LG.V_Type, VT.NCat, Sg.SubCode, PSG.Subcode as LinkedSubcode, IfNull(PI.VendorDocNo,LG.RecId) as RecID, LG.V_Date, Sg.Subcode, Sg.Nature, 
                        Sg.name || (Case When IfNull(Ct.CityName,'') <> '' Then ', ' || IfNull(Ct.CityName,'') else '' End) as PartyName, CT.CityName as PartyCity, 
                        LG.AmtDr, LG.AmtCr, LG.AmtDr + LG.AmtCr as Amount,
                        LG.Site_Code, LG.DivCode As Div_Code, 
                        (Case When VT.NCat Not In ( '" & Ncat.SaleInvoice & "', '" & Ncat.PurchaseInvoice & "'  ) Then LG.Narration || '. ' Else '' End) || IfNull(LG.Chq_No,'') as Narration,
                        LG.Clg_Date
                        FROM Ledger LG 
                        Left Join PurchInvoice PI On LG.DocID = PI.DocID"
        If ReportFrm.FGetText(rowGroupOn) = "Linked Party" Then
            mQry = mQry & " Left Join SubGroup SG On SG.Subcode = IfNull(LG.LinkedSubcode,LG.SubCode)   "
            'mQry = mQry & " Left Join Subgroup PSg On  PSg.Subcode = IfNull(Lg.LinkedSubcode,Lg.Subcode) "
            mQry = mQry & " Left Join Subgroup PSg On  PSg.Subcode = Lg.Subcode "
        Else
            mQry = mQry & " Left Join SubGroup SG On SG.Subcode =LG.SubCode   "
            mQry = mQry & " Left Join Subgroup PSg On Sg.Parent = PSg.Subcode "
        End If

        mQry = mQry & "Left Join Voucher_Type Vt On Lg.V_Type = Vt.V_Type
                        Left Join (Select SILTV.Subcode, Max(SILTV.Agent) as Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode) as LTV On Sg.Subcode = LTV.Subcode
                        LEFT JOIN City CT On SG.CityCode  =CT.CityCode "
        mQry = mQry & "Where SG.Nature In ('Customer','Supplier') " + mCondStr

        If mFromDate <> "" Then
            mQry = mQry & " And Date(Lg.V_Date) >= " & AgL.Chk_Date(mFromDate) & " "
        End If
        mQry = mQry & " Order By Sg.Subcode, LG.V_Date, LG.V_Type, IfNull(PI.VendorDocNo,LG.RecId)  "


        DtLedger = AgL.FillData(mQry, AgL.GCn).Tables(0)


        Dim sLed As StructLedger
        Dim mRunningBalanace As Double = 0
        For I As Integer = 0 To DtLedger.Rows.Count - 1
            sLed = New StructLedger

            sLed.DocID = AgL.XNull(DtLedger.Rows(I)("DocID"))
            sLed.Sr = AgL.XNull(DtLedger.Rows(I)("V_SNo"))
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
            sLed.Clg_Date = AgL.XNull(DtLedger.Rows(I)("Clg_Date"))


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
                            (DocID, Sr, V_Type, RecId, V_Date, 
                            Site_code, Div_Code, Subcode, LinkedSubcode, 
                            Narration, GoodsReturn, Payment, 
                            Adjustment,Balance, AmtDr, AmtCr, Narration, Clg_Date) 
                            Values (" & AgL.Chk_Text(sLed.DocID) & ", " & AgL.Chk_Text(sLed.Sr) & ", " & AgL.Chk_Text(sLed.V_Type) & ", 
                            " & AgL.Chk_Text(sLed.RecId) & "," & AgL.Chk_Date(sLed.V_Date) & ",
                            " & AgL.Chk_Text(sLed.Site_Code) & ", " & AgL.Chk_Text(sLed.Div_Code) & ",                            
                            " & AgL.Chk_Text(sLed.Subcode) & ", " & AgL.Chk_Text(sLed.LinkedSubcode) & "," & AgL.Chk_Text(sLed.Narration) & ", 
                            " & AgL.VNull(sLed.GoodsReturn) & ", " & AgL.VNull(sLed.Payment) & ",
                            " & AgL.VNull(sLed.Adjustment) & ", " & AgL.VNull(sLed.Balance) & ",
                            " & AgL.VNull(sLed.AmtDr) & ", " & AgL.VNull(sLed.AmtCr) & ", " & AgL.Chk_Text(sLed.Narration) & ", " & AgL.Chk_Date(sLed.Clg_Date) & "
                            )"

            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        Next


        If ReportFrm.FGetText(rowReportType) = "Interest Ledger" Or ReportFrm.FGetText(rowNextStep) = "Interest Ledger" Then
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
                    Sg.Subcode, H.LinkedSubcode, H.GoodsReturn, 
                    H.Adjustment, 
                    H.Payment, 
                    H.Balance, 
                    (Case  When IfNull(Trd.Type,'')='Cancelled' OR IfNull(Trr.Type,'')='Cancelled' Then 0 Else H.AmtDr End) AmtDr, 
                    (Case  When IfNull(Trd.Type,'')='Cancelled' OR IfNull(Trr.Type,'')='Cancelled' Then 0 Else H.AmtCr End) AmtCr,  
                    (Case  When IfNull(Trd.Type,'')='Cancelled' OR IfNull(Trr.Type,'')='Cancelled' Then ' Cheque Bounce - ' || H.AmtDr  || ' '  Else '' End) || IfNull(H.Narration,'')  Narration, H.AdjDocID, H.DueDate, H.AdjDate, 
                    H.AdjAmount, H.InterestDays, H.InterestAmount, H.InterestBalance, 
                    Sg.Nature, (Case  When IfNull(Trd.Type,'')='Cancelled' OR IfNull(Trr.Type,'')='Cancelled' Then 0  When Sg.Nature = 'Customer' Then H.AmtDr Else H.AmtCr End) as Amount,
                    I.LeaverageDays
                    From #TempRecord H 
                    Left Join Subgroup MSg On H.LinkedSubcode = MSg.Subcode
                    Left Join InterestSlab I On Msg.InterestSlab = I.Code
                    Left Join TransactionReferences Trd With (NoLock) On H.DocID = Trd.DocId And Trd.DocIDSr=H.Sr And H.V_Date >= '2019-07-01'
                    Left Join TransactionReferences Trr With (NoLock) On H.DocID = Trr.ReferenceDocId And Trr.ReferenceSr=H.Sr And H.V_Date >= '2019-07-01'
                    "

            mQry = mQry & " Left Join SubGroup SG On SG.Subcode =H.SubCode   "
            mQry = mQry & " Left Join SubGroup PSG On SG.Parent =PSG.SubCode   "

            'If ReportFrm.FGetText(rowGroupOn) = "Linked Party" Then
            '    mQry = mQry & " Left Join SubGroup SG On SG.Subcode = IfNull(H.LinkedSubcode,H.SubCode)  "
            '    mQry = mQry & " Left Join SubGroup PSG On PSG.Subcode = IfNull(H.LinkedSubcode,H.SubCode)  "
            'Else
            '    mQry = mQry & " Left Join SubGroup SG On SG.Subcode =H.SubCode   "
            '    mQry = mQry & " Left Join SubGroup PSG On SG.Parent =PSG.SubCode   "
            'End If

            mQry = mQry & " Where 1=1 And (Case When Sg.Nature = 'Customer' Then H.AmtDr Else H.AmtCr End) > 0
                            Order By Sg.Subcode, H.V_date, RecID "
            DtBills = AgL.FillData(mQry, AgL.GCn).Tables(0)

            mQry = "Select Lg.DocID, Lg.DivCode, Lg.Site_Code, Lg.V_Type, Vt.Description as V_TypeDesc, 
                                    Lg.RecId, SG.Subcode, LG.LinkedSubcode, IfNull(Lg.EffectiveDate, Lg.V_Date) As V_Date, IfNull(LG.Chq_No,'')  as ChqNo, 
                                    (Case  When IfNull(Trd.Type,'')='Cancelled' OR IfNull(Trr.Type,'')='Cancelled' Then 'Cheque Bounce  -  ' Else '' End) || IfNull(Lg.Narration,'') as Narration, 
                                    (Case When IfNull(Trd.Type,'')='Cancelled' OR IfNull(Trr.Type,'')='Cancelled' Then 0  When Sg.Nature='Customer' Then Lg.AmtCr Else LG.AmtDr End) as Amount                                
                                    From Ledger Lg  With (NoLock) "
            If ReportFrm.FGetText(rowGroupOn) = "Linked Party" Then
                mQry = mQry & " Left Join SubGroup SG On SG.Subcode = IfNull(LG.LinkedSubcode,LG.SubCode)  "
                mQry = mQry & " Left Join SubGroup PSG On PSG.Subcode = IfNull(LG.LinkedSubcode,LG.SubCode)  "
            Else
                mQry = mQry & " Left Join SubGroup SG On SG.Subcode =LG.SubCode   "
                mQry = mQry & " Left Join SubGroup PSG On SG.Parent =PSG.SubCode   "
            End If
            'mQry = mQry & " Left Join(Select SILTV.Subcode, Max(SILTV.Agent) As Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode) as LTV On Sg.Subcode = LTV.Subcode
            '                        Left Join Voucher_Type Vt  With (NoLock) On Lg.V_Type = Vt.V_Type
            '                        Left Join TransactionReferences Trd With (NoLock) On Lg.DocID = Trd.DocId And Lg.V_SNo = Trd.DocIDSr And Lg.V_Date >= '2019-07-01'
            '                        Left Join TransactionReferences Trr With (NoLock) On Lg.DocID = Trr.ReferenceDocId And Lg.V_SNo = Trr.ReferenceSr And Lg.V_Date >= '2019-07-01'
            '                        Where (Case When Sg.Nature='Customer' Then Lg.AmtCr Else LG.AmtDr End) > 0  
            '                        And IfNull(Trd.Type,'')<>'Cancelled' And IfNull(Trr.Type,'')<>'Cancelled'
            '                          " & mCondStr & "                               
            '                        Order By IfNull(Lg.EffectiveDate, Lg.V_Date) , Lg.RecId 
            '                      "
            mQry = mQry & " Left Join(Select SILTV.Subcode, Max(SILTV.Agent) As Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode) as LTV On Sg.Subcode = LTV.Subcode
                                    Left Join Voucher_Type Vt  With (NoLock) On Lg.V_Type = Vt.V_Type
                                    Left Join TransactionReferences Trd With (NoLock) On Lg.DocID = Trd.DocId And 1 = Trd.DocIDSr And Lg.V_Date >= '2019-07-01'
                                    Left Join TransactionReferences Trr With (NoLock) On Lg.DocID = Trr.ReferenceDocId And 1 = Trr.ReferenceSr And Lg.V_Date >= '2019-07-01'
                                    Where (Case When Sg.Nature='Customer' Then Lg.AmtCr Else LG.AmtDr End) > 0  
                                    And IfNull(Trd.Type,'')<>'Cancelled' And IfNull(Trr.Type,'')<>'Cancelled'
                                      " & mCondStr & "                               
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
                        If ReportFrm.FGetText(rowGroupOn) = "Linked Party" Then
                            drPayment = DtPayment.Select(" LinkedSubcode = '" & AgL.XNull(DtBills.Rows(I)("Subcode")) & "' ")
                        Else
                            drPayment = DtPayment.Select(" Subcode = '" & AgL.XNull(DtBills.Rows(I)("Subcode")) & "' ")
                        End If

                        mBalBill = DtBills.Rows(I)("Amount")
                    Else
                        If AgL.XNull(DtBills.Rows(I)("Subcode")) <> mLastInsertedSubcode Then 'AgL.XNull(DtBills.Rows(I - 1)("Subcode")) Then
                            If AgL.XNull(DtBills.Rows(I)("Subcode")) = "D100001234" Then
                                Debug.Print("found")
                            End If

                            If drPayment IsNot Nothing Then
                                If J < drPayment.Length Then
                                    While J < drPayment.Length
                                        mAdjAmt = 0 'mBalPmt
                                        mBalBill = 0 'mBalBill - mAdjAmt

                                        sLed = New StructLedger

                                        sLed.Subcode = AgL.XNull(drPayment(J)("Subcode"))
                                        sLed.LinkedSubcode = AgL.XNull(drPayment(J)("LinkedSubcode"))

                                        sLed.AdjDocID = IIf(AgL.XNull(drPayment(J)("V_Type")) = "", "", AgL.XNull(drPayment(J)("V_Type")) & "-") & AgL.XNull(drPayment(J)("RecID"))
                                        sLed.AdjAmount = mAdjAmt
                                        sLed.DueDate = AgL.XNull(drPayment(J)("V_Date"))
                                        sLed.AdjDate = AgL.XNull(drPayment(J)("V_Date"))
                                        sLed.AdjVAmount = drPayment(J)("Amount")
                                        sLed.ChqNo = drPayment(J)("ChqNO")
                                        sLed.Narration = AgL.XNull(drPayment(J)("Narration"))


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
                                        If sLed.Subcode = "D100001234" Then
                                            Debug.Print("found Again")
                                        End If
                                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
                                        J = J + 1
                                    End While
                                    If I < DtBills.Rows.Count Then
                                        mBalBill = AgL.VNull(DtBills.Rows(I)("Amount"))
                                    End If
                                End If
                            End If




                            drPayment = Nothing
                            If ReportFrm.FGetText(rowGroupOn) = "Linked Party" Then
                                drPayment = DtPayment.Select(" LinkedSubcode = '" & AgL.XNull(DtBills.Rows(I)("Subcode")) & "' ")
                            Else
                                drPayment = DtPayment.Select(" Subcode = '" & AgL.XNull(DtBills.Rows(I)("Subcode")) & "' ")
                            End If

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
                            If sLed.Subcode = "D100001234" Then
                                Debug.Print("found")
                            End If

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
                                        sLed.DueDate = AgL.XNull(DtBills.Rows(I)("V_Date"))
                                        sLed.AdjDate = AgL.XNull(drPayment(J)("V_Date"))
                                        sLed.Narration = sLed.Narration & " " & AgL.XNull(drPayment(J)("Narration")) & " "

                                        Dim sQrySaleBrand As String = "", sQryPurchaseBrand As String = ""
                                        If AgL.XNull(drPayment(J)("V_Type")) = "WSR" Then
                                            sQrySaleBrand = "Select group_concat(Brand ,',') || ',' From (Select IfNull(sGroup.Description, sItem.Description)  as Brand  from SaleInvoiceDetail sSID  With (NoLock)  Left Join SaleInvoiceDetailSku sSIDS On sSID.DocID = sSIDS.DocID  Left Join Item sItem On sSID.Item = sItem.Code Left Join Item sGroup On IfNull(sSIDS.ItemGroup,sItem.ItemGroup) = sGroup.Code Where sSID.DocID = '" & AgL.XNull(drPayment(J)("DocID")) & "' And sItem.ItemType  In ('" & ItemTypeCode.TradingProduct & "','Wool') Group By IfNull(sGroup.Description, sItem.Description))"
                                            sQrySaleBrand = AgL.Dman_Execute(sQrySaleBrand, AgL.GCn).ExecuteScalar()
                                        End If
                                        If AgL.XNull(drPayment(J)("V_Type")) = "WPR" Then
                                            sQryPurchaseBrand = "(Select group_concat(Brand ,',') || ',' From (Select IfNull(sGroup.Description, sItem.Description)  as Brand  from PurchInvoiceDetail sPID  With (NoLock)  Left Join PurchInvoiceDetailSku sPIDS On sPID.DocID = sPIDS.DocID  Left Join Item sItem On sPID.Item = sItem.Code Left Join Item sGroup On IfNull(sPIDS.ItemGroup,sItem.ItemGroup) = sGroup.Code Where sPID.DocID = '" & AgL.XNull(drPayment(J)("DocID")) & "' And sItem.ItemType  In ('" & ItemTypeCode.TradingProduct & "','Wool') Group By IfNull(sGroup.Description, sItem.Description)))"
                                            sQryPurchaseBrand = AgL.Dman_Execute(sQryPurchaseBrand, AgL.GCn).ExecuteScalar()
                                        End If
                                        sLed.Narration = sLed.Narration & sQrySaleBrand & sQryPurchaseBrand


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
                                        sLed.DueDate = AgL.XNull(DtBills.Rows(I)("V_Date"))
                                        sLed.AdjDate = AgL.XNull(drPayment(J)("V_Date"))
                                        sLed.AdjVAmount = drPayment(J)("Amount")
                                        sLed.ChqNo = drPayment(J)("ChqNO")
                                        sLed.Narration = sLed.Narration & " " & AgL.XNull(drPayment(J)("Narration")) & " "
                                        Dim sQrySaleBrand As String = "", sQryPurchaseBrand As String = ""
                                        If AgL.XNull(drPayment(J)("V_Type")) = "WSR" Then
                                            sQrySaleBrand = "Select group_concat(Brand ,',') || ',' From (Select IfNull(sGroup.Description, sItem.Description)  as Brand  from SaleInvoiceDetail sSID  With (NoLock)  Left Join SaleInvoiceDetailSku sSIDS On sSID.DocID = sSIDS.DocID  Left Join Item sItem On sSID.Item = sItem.Code Left Join Item sGroup On IfNull(sSIDS.ItemGroup,sItem.ItemGroup) = sGroup.Code Where sSID.DocID = '" & AgL.XNull(drPayment(J)("DocID")) & "' And sItem.ItemType  In ('" & ItemTypeCode.TradingProduct & "','Wool') Group By IfNull(sGroup.Description, sItem.Description))"
                                            sQrySaleBrand = AgL.Dman_Execute(sQrySaleBrand, AgL.GCn).ExecuteScalar()
                                        End If
                                        If AgL.XNull(drPayment(J)("V_Type")) = "WPR" Then
                                            sQryPurchaseBrand = "(Select group_concat(Brand ,',') || ',' From (Select IfNull(sGroup.Description, sItem.Description)  as Brand  from PurchInvoiceDetail sPID  With (NoLock)  Left Join PurchInvoiceDetailSku sPIDS On sPID.DocID = sPIDS.DocID  Left Join Item sItem On sPID.Item = sItem.Code Left Join Item sGroup On IfNull(sPIDS.ItemGroup,sItem.ItemGroup) = sGroup.Code Where sPID.DocID = '" & AgL.XNull(drPayment(J)("DocID")) & "' And sItem.ItemType  In ('" & ItemTypeCode.TradingProduct & "','Wool') Group By IfNull(sGroup.Description, sItem.Description)))"
                                            sQryPurchaseBrand = AgL.Dman_Execute(sQryPurchaseBrand, AgL.GCn).ExecuteScalar()
                                        End If
                                        sLed.Narration = sLed.Narration & sQrySaleBrand & sQryPurchaseBrand



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
                                        sLed.DueDate = AgL.XNull(DtBills.Rows(I)("V_Date"))
                                        sLed.AdjDate = AgL.XNull(drPayment(J)("V_Date"))
                                        sLed.AdjVAmount = drPayment(J)("Amount")
                                        sLed.ChqNo = drPayment(J)("ChqNo")
                                        sLed.Narration = sLed.Narration & " " & AgL.XNull(drPayment(J)("Narration")) & " "


                                        Dim sQrySaleBrand As String = "", sQryPurchaseBrand As String = ""
                                        If AgL.XNull(drPayment(J)("V_Type")) = "WSR" Then
                                            sQrySaleBrand = "Select group_concat(Brand ,',') || ',' From (Select IfNull(sGroup.Description, sItem.Description)  as Brand  from SaleInvoiceDetail sSID  With (NoLock)  Left Join SaleInvoiceDetailSku sSIDS On sSID.DocID = sSIDS.DocID  Left Join Item sItem On sSID.Item = sItem.Code Left Join Item sGroup On IfNull(sSIDS.ItemGroup,sItem.ItemGroup) = sGroup.Code Where sSID.DocID = '" & AgL.XNull(drPayment(J)("DocID")) & "' And sItem.ItemType  In ('" & ItemTypeCode.TradingProduct & "','Wool') Group By IfNull(sGroup.Description, sItem.Description))"
                                            sQrySaleBrand = AgL.Dman_Execute(sQrySaleBrand, AgL.GCn).ExecuteScalar()
                                        End If
                                        If AgL.XNull(drPayment(J)("V_Type")) = "WPR" Then
                                            sQryPurchaseBrand = "(Select group_concat(Brand ,',') || ',' From (Select IfNull(sGroup.Description, sItem.Description)  as Brand  from PurchInvoiceDetail sPID  With (NoLock)  Left Join PurchInvoiceDetailSku sPIDS On sPID.DocID = sPIDS.DocID  Left Join Item sItem On sPID.Item = sItem.Code Left Join Item sGroup On IfNull(sPIDS.ItemGroup,sItem.ItemGroup) = sGroup.Code Where sPID.DocID = '" & AgL.XNull(drPayment(J)("DocID")) & "' And sItem.ItemType  In ('" & ItemTypeCode.TradingProduct & "','Wool') Group By IfNull(sGroup.Description, sItem.Description)))"
                                            sQryPurchaseBrand = AgL.Dman_Execute(sQryPurchaseBrand, AgL.GCn).ExecuteScalar()
                                        End If
                                        sLed.Narration = sLed.Narration & sQrySaleBrand & sQryPurchaseBrand

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
                                    If AgL.XNull(ReportFrm.FGetText(rowInterestUptoDate)) <> "" Then
                                        sLed.AdjDate = AgL.XNull(ReportFrm.FGetText(rowInterestUptoDate))
                                    Else
                                        sLed.AdjDate = AgL.XNull(ReportFrm.FGetText(rowPaymentsUptoDate))
                                    End If

                                    I = I + 1
                                    J = J + 1
                                    If I < DtBills.Rows.Count Then
                                        mBalBill = DtBills.Rows(I)("Amount")
                                    Else
                                        mBalBill = 0
                                    End If

                                    'If ReportFrm.FGetText(rowAdditionalCreditDays) <> "" Then
                                    '    mQry = "Update SaleInvoice Set AdditionalCreditDays = AdditionalCreditDays " & ReportFrm.FGetText(rowAdditionalCreditDays) & " Where DociD = '" & sLed.DocID & "' "
                                    '    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
                                    'End If

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
                            While J < drPayment.Length
                                'If AgL.XNull(DtBills.Rows(I)("Subcode")) <> AgL.XNull(DtBills.Rows(I - 1)("Subcode")) Then
                                mAdjAmt = 0 'mBalPmt
                                mBalBill = 0 'mBalBill - mAdjAmt

                                sLed = New StructLedger

                                sLed.Subcode = AgL.XNull(drPayment(J)("Subcode"))
                                sLed.LinkedSubcode = AgL.XNull(drPayment(J)("LinkedSubcode"))

                                sLed.AdjDocID = IIf(AgL.XNull(drPayment(J)("V_Type")) = "", "", AgL.XNull(drPayment(J)("V_Type")) & "-") & AgL.XNull(drPayment(J)("RecID"))
                                sLed.AdjAmount = mAdjAmt
                                sLed.DueDate = AgL.XNull(drPayment(J)("V_Date"))
                                sLed.AdjDate = AgL.XNull(drPayment(J)("V_Date"))
                                sLed.AdjVAmount = drPayment(J)("Amount")
                                sLed.ChqNo = drPayment(J)("ChqNO")
                                sLed.Narration = AgL.XNull(drPayment(J)("Narration"))


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

                                'End If
                                J = J + 1
                            End While


                            'J = drPayment.Length + 1
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

    Private Sub GetDataReadyForInterestLedger_09Sep20(mCondStr As String, ShowDataIn As ShowDataIn)
        Dim DtPayment As DataTable = Nothing
        Dim drPayment As DataRow()
        Dim sLed As StructLedger
        Dim xI As Integer, xJ As Integer

        If ReportFrm.FGetText(rowReportType) = "Interest Ledger" Or ShowDataIn = ShowDataIn.Crystal Then
            Dim DtBills As DataTable

            mQry = "Select H.DocID, H.V_Type, H.RecId, H.V_Date, H.Site_Code, H.Div_Code, 
                    Sg.Subcode, H.LinkedSubcode, H.GoodsReturn, 
                    H.Adjustment, 
                    H.Payment, 
                    H.Balance, 
                    (Case  When IfNull(Trd.Type,'')='Cancelled' OR IfNull(Trr.Type,'')='Cancelled' Then 0 Else H.AmtDr End) AmtDr, 
                    (Case  When IfNull(Trd.Type,'')='Cancelled' OR IfNull(Trr.Type,'')='Cancelled' Then 0 Else H.AmtCr End) AmtCr,  
                    (Case  When IfNull(Trd.Type,'')='Cancelled' OR IfNull(Trr.Type,'')='Cancelled' Then ' Cheque Bounce - ' || H.AmtDr  || ' '  Else '' End) || IfNull(H.Narration,'')  Narration, H.AdjDocID, H.DueDate, H.AdjDate, 
                    H.AdjAmount, H.InterestDays, H.InterestAmount, H.InterestBalance, 
                    Sg.Nature, (Case  When IfNull(Trd.Type,'')='Cancelled' OR IfNull(Trr.Type,'')='Cancelled' Then 0  When Sg.Nature = 'Customer' Then H.AmtDr Else H.AmtCr End) as Amount,
                    I.LeaverageDays
                    From #TempRecord H 
                    Left Join Subgroup MSg On H.LinkedSubcode = MSg.Subcode
                    Left Join InterestSlab I On Msg.InterestSlab = I.Code
                    Left Join TransactionReferences Trd With (NoLock) On H.DocID = Trd.DocId And Trd.DocIDSr=H.Sr And H.V_Date >= '2019-07-01'
                    Left Join TransactionReferences Trr With (NoLock) On H.DocID = Trr.ReferenceDocId And Trr.ReferenceSr=H.Sr And H.V_Date >= '2019-07-01'
                    "

            mQry = mQry & " Left Join SubGroup SG On SG.Subcode =H.SubCode   "
            mQry = mQry & " Left Join SubGroup PSG On SG.Parent =PSG.SubCode   "

            'If ReportFrm.FGetText(rowGroupOn) = "Linked Party" Then
            '    mQry = mQry & " Left Join SubGroup SG On SG.Subcode = IfNull(H.LinkedSubcode,H.SubCode)  "
            '    mQry = mQry & " Left Join SubGroup PSG On PSG.Subcode = IfNull(H.LinkedSubcode,H.SubCode)  "
            'Else
            '    mQry = mQry & " Left Join SubGroup SG On SG.Subcode =H.SubCode   "
            '    mQry = mQry & " Left Join SubGroup PSG On SG.Parent =PSG.SubCode   "
            'End If

            mQry = mQry & " Where 1=1 And (Case When Sg.Nature = 'Customer' Then H.AmtDr Else H.AmtCr End) > 0
                            Order By Sg.Subcode, H.V_date, RecID "
            DtBills = AgL.FillData(mQry, AgL.GCn).Tables(0)

            mQry = "Select Lg.DocID, Lg.DivCode, Lg.Site_Code, Lg.V_Type, Vt.Description as V_TypeDesc, 
                                    Lg.RecId, SG.Subcode, LG.LinkedSubcode, IfNull(Lg.EffectiveDate, Lg.V_Date) As V_Date, IfNull(LG.Chq_No,'')  as ChqNo, 
                                    (Case  When IfNull(Trd.Type,'')='Cancelled' OR IfNull(Trr.Type,'')='Cancelled' Then 'Cheque Bounce  -  ' Else '' End) || IfNull(Lg.Narration,'') as Narration, 
                                    (Case When IfNull(Trd.Type,'')='Cancelled' OR IfNull(Trr.Type,'')='Cancelled' Then 0  When Sg.Nature='Customer' Then Lg.AmtCr Else LG.AmtDr End) as Amount                                
                                    From Ledger Lg  With (NoLock) "
            If ReportFrm.FGetText(rowGroupOn) = "Linked Party" Then
                mQry = mQry & " Left Join SubGroup SG On SG.Subcode = IfNull(LG.LinkedSubcode,LG.SubCode)  "
                mQry = mQry & " Left Join SubGroup PSG On PSG.Subcode = IfNull(LG.LinkedSubcode,LG.SubCode)  "
            Else
                mQry = mQry & " Left Join SubGroup SG On SG.Subcode =LG.SubCode   "
                mQry = mQry & " Left Join SubGroup PSG On SG.Parent =PSG.SubCode   "
            End If
            'mQry = mQry & " Left Join(Select SILTV.Subcode, Max(SILTV.Agent) As Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode) as LTV On Sg.Subcode = LTV.Subcode
            '                        Left Join Voucher_Type Vt  With (NoLock) On Lg.V_Type = Vt.V_Type
            '                        Left Join TransactionReferences Trd With (NoLock) On Lg.DocID = Trd.DocId And Lg.V_SNo = Trd.DocIDSr And Lg.V_Date >= '2019-07-01'
            '                        Left Join TransactionReferences Trr With (NoLock) On Lg.DocID = Trr.ReferenceDocId And Lg.V_SNo = Trr.ReferenceSr And Lg.V_Date >= '2019-07-01'
            '                        Where (Case When Sg.Nature='Customer' Then Lg.AmtCr Else LG.AmtDr End) > 0  
            '                        And IfNull(Trd.Type,'')<>'Cancelled' And IfNull(Trr.Type,'')<>'Cancelled'
            '                          " & mCondStr & "                               
            '                        Order By IfNull(Lg.EffectiveDate, Lg.V_Date) , Lg.RecId 
            '                      "
            mQry = mQry & " Left Join(Select SILTV.Subcode, Max(SILTV.Agent) As Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode) as LTV On Sg.Subcode = LTV.Subcode
                                    Left Join Voucher_Type Vt  With (NoLock) On Lg.V_Type = Vt.V_Type
                                    Left Join TransactionReferences Trd With (NoLock) On Lg.DocID = Trd.DocId And 1 = Trd.DocIDSr And Lg.V_Date >= '2019-07-01'
                                    Left Join TransactionReferences Trr With (NoLock) On Lg.DocID = Trr.ReferenceDocId And 1 = Trr.ReferenceSr And Lg.V_Date >= '2019-07-01'
                                    Where (Case When Sg.Nature='Customer' Then Lg.AmtCr Else LG.AmtDr End) > 0  
                                    And IfNull(Trd.Type,'')<>'Cancelled' And IfNull(Trr.Type,'')<>'Cancelled'
                                      " & mCondStr & "                               
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
                        If ReportFrm.FGetText(rowGroupOn) = "Linked Party" Then
                            drPayment = DtPayment.Select(" LinkedSubcode = '" & AgL.XNull(DtBills.Rows(I)("Subcode")) & "' ")
                        Else
                            drPayment = DtPayment.Select(" Subcode = '" & AgL.XNull(DtBills.Rows(I)("Subcode")) & "' ")
                        End If

                        mBalBill = DtBills.Rows(I)("Amount")
                    Else
                        If AgL.XNull(DtBills.Rows(I)("Subcode")) <> mLastInsertedSubcode Then 'AgL.XNull(DtBills.Rows(I - 1)("Subcode")) Then
                            If AgL.XNull(DtBills.Rows(I)("Subcode")) = "D100001234" Then
                                Debug.Print("found")
                            End If

                            drPayment = Nothing
                            If ReportFrm.FGetText(rowGroupOn) = "Linked Party" Then
                                drPayment = DtPayment.Select(" LinkedSubcode = '" & AgL.XNull(DtBills.Rows(I)("Subcode")) & "' ")
                            Else
                                drPayment = DtPayment.Select(" Subcode = '" & AgL.XNull(DtBills.Rows(I)("Subcode")) & "' ")
                            End If

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
                            If sLed.Subcode = "D100001234" Then
                                Debug.Print("found")
                            End If

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
                                        sLed.DueDate = AgL.XNull(DtBills.Rows(I)("V_Date"))
                                        sLed.AdjDate = AgL.XNull(drPayment(J)("V_Date"))
                                        sLed.Narration = sLed.Narration & " " & AgL.XNull(drPayment(J)("Narration")) & " "

                                        Dim sQrySaleBrand As String = "", sQryPurchaseBrand As String = ""
                                        If AgL.XNull(drPayment(J)("V_Type")) = "WSR" Then
                                            sQrySaleBrand = "Select group_concat(Brand ,',') || ',' From (Select IfNull(sGroup.Description, sItem.Description)  as Brand  from SaleInvoiceDetail sSID  With (NoLock)  Left Join SaleInvoiceDetailSku sSIDS On sSID.DocID = sSIDS.DocID  Left Join Item sItem On sSID.Item = sItem.Code Left Join Item sGroup On IfNull(sSIDS.ItemGroup,sItem.ItemGroup) = sGroup.Code Where sSID.DocID = '" & AgL.XNull(drPayment(J)("DocID")) & "' And sItem.ItemType  In ('" & ItemTypeCode.TradingProduct & "','Wool') Group By IfNull(sGroup.Description, sItem.Description))"
                                            sQrySaleBrand = AgL.Dman_Execute(sQrySaleBrand, AgL.GCn).ExecuteScalar()
                                        End If
                                        If AgL.XNull(drPayment(J)("V_Type")) = "WPR" Then
                                            sQryPurchaseBrand = "(Select group_concat(Brand ,',') || ',' From (Select IfNull(sGroup.Description, sItem.Description)  as Brand  from PurchInvoiceDetail sPID  With (NoLock)  Left Join PurchInvoiceDetailSku sPIDS On sPID.DocID = sPIDS.DocID  Left Join Item sItem On sPID.Item = sItem.Code Left Join Item sGroup On IfNull(sPIDS.ItemGroup,sItem.ItemGroup) = sGroup.Code Where sPID.DocID = '" & AgL.XNull(drPayment(J)("DocID")) & "' And sItem.ItemType  In ('" & ItemTypeCode.TradingProduct & "','Wool') Group By IfNull(sGroup.Description, sItem.Description)))"
                                            sQryPurchaseBrand = AgL.Dman_Execute(sQryPurchaseBrand, AgL.GCn).ExecuteScalar()
                                        End If
                                        sLed.Narration = sLed.Narration & sQrySaleBrand & sQryPurchaseBrand


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
                                        sLed.DueDate = AgL.XNull(DtBills.Rows(I)("V_Date"))
                                        sLed.AdjDate = AgL.XNull(drPayment(J)("V_Date"))
                                        sLed.AdjVAmount = drPayment(J)("Amount")
                                        sLed.ChqNo = drPayment(J)("ChqNO")
                                        sLed.Narration = sLed.Narration & " " & AgL.XNull(drPayment(J)("Narration")) & " "
                                        Dim sQrySaleBrand As String = "", sQryPurchaseBrand As String = ""
                                        If AgL.XNull(drPayment(J)("V_Type")) = "WSR" Then
                                            sQrySaleBrand = "Select group_concat(Brand ,',') || ',' From (Select IfNull(sGroup.Description, sItem.Description)  as Brand  from SaleInvoiceDetail sSID  With (NoLock)  Left Join SaleInvoiceDetailSku sSIDS On sSID.DocID = sSIDS.DocID  Left Join Item sItem On sSID.Item = sItem.Code Left Join Item sGroup On IfNull(sSIDS.ItemGroup,sItem.ItemGroup) = sGroup.Code Where sSID.DocID = '" & AgL.XNull(drPayment(J)("DocID")) & "' And sItem.ItemType  In ('" & ItemTypeCode.TradingProduct & "','Wool') Group By IfNull(sGroup.Description, sItem.Description))"
                                            sQrySaleBrand = AgL.Dman_Execute(sQrySaleBrand, AgL.GCn).ExecuteScalar()
                                        End If
                                        If AgL.XNull(drPayment(J)("V_Type")) = "WPR" Then
                                            sQryPurchaseBrand = "(Select group_concat(Brand ,',') || ',' From (Select IfNull(sGroup.Description, sItem.Description)  as Brand  from PurchInvoiceDetail sPID  With (NoLock)  Left Join PurchInvoiceDetailSku sPIDS On sPID.DocID = sPIDS.DocID  Left Join Item sItem On sPID.Item = sItem.Code Left Join Item sGroup On IfNull(sPIDS.ItemGroup,sItem.ItemGroup) = sGroup.Code Where sPID.DocID = '" & AgL.XNull(drPayment(J)("DocID")) & "' And sItem.ItemType  In ('" & ItemTypeCode.TradingProduct & "','Wool') Group By IfNull(sGroup.Description, sItem.Description)))"
                                            sQryPurchaseBrand = AgL.Dman_Execute(sQryPurchaseBrand, AgL.GCn).ExecuteScalar()
                                        End If
                                        sLed.Narration = sLed.Narration & sQrySaleBrand & sQryPurchaseBrand



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
                                        sLed.DueDate = AgL.XNull(DtBills.Rows(I)("V_Date"))
                                        sLed.AdjDate = AgL.XNull(drPayment(J)("V_Date"))
                                        sLed.AdjVAmount = drPayment(J)("Amount")
                                        sLed.ChqNo = drPayment(J)("ChqNo")
                                        sLed.Narration = sLed.Narration & " " & AgL.XNull(drPayment(J)("Narration")) & " "


                                        Dim sQrySaleBrand As String = "", sQryPurchaseBrand As String = ""
                                        If AgL.XNull(drPayment(J)("V_Type")) = "WSR" Then
                                            sQrySaleBrand = "Select group_concat(Brand ,',') || ',' From (Select IfNull(sGroup.Description, sItem.Description)  as Brand  from SaleInvoiceDetail sSID  With (NoLock)  Left Join SaleInvoiceDetailSku sSIDS On sSID.DocID = sSIDS.DocID  Left Join Item sItem On sSID.Item = sItem.Code Left Join Item sGroup On IfNull(sSIDS.ItemGroup,sItem.ItemGroup) = sGroup.Code Where sSID.DocID = '" & AgL.XNull(drPayment(J)("DocID")) & "' And sItem.ItemType  In ('" & ItemTypeCode.TradingProduct & "','Wool') Group By IfNull(sGroup.Description, sItem.Description))"
                                            sQrySaleBrand = AgL.Dman_Execute(sQrySaleBrand, AgL.GCn).ExecuteScalar()
                                        End If
                                        If AgL.XNull(drPayment(J)("V_Type")) = "WPR" Then
                                            sQryPurchaseBrand = "(Select group_concat(Brand ,',') || ',' From (Select IfNull(sGroup.Description, sItem.Description)  as Brand  from PurchInvoiceDetail sPID  With (NoLock)  Left Join PurchInvoiceDetailSku sPIDS On sPID.DocID = sPIDS.DocID  Left Join Item sItem On sPID.Item = sItem.Code Left Join Item sGroup On IfNull(sPIDS.ItemGroup,sItem.ItemGroup) = sGroup.Code Where sPID.DocID = '" & AgL.XNull(drPayment(J)("DocID")) & "' And sItem.ItemType  In ('" & ItemTypeCode.TradingProduct & "','Wool') Group By IfNull(sGroup.Description, sItem.Description)))"
                                            sQryPurchaseBrand = AgL.Dman_Execute(sQryPurchaseBrand, AgL.GCn).ExecuteScalar()
                                        End If
                                        sLed.Narration = sLed.Narration & sQrySaleBrand & sQryPurchaseBrand

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
                                    If AgL.XNull(ReportFrm.FGetText(rowInterestUptoDate)) <> "" Then
                                        sLed.AdjDate = AgL.XNull(ReportFrm.FGetText(rowInterestUptoDate))
                                    Else
                                        sLed.AdjDate = AgL.XNull(ReportFrm.FGetText(rowPaymentsUptoDate))
                                    End If

                                    I = I + 1
                                    J = J + 1
                                    If I < DtBills.Rows.Count Then
                                        mBalBill = DtBills.Rows(I)("Amount")
                                    Else
                                        mBalBill = 0
                                    End If

                                    'If ReportFrm.FGetText(rowAdditionalCreditDays) <> "" Then
                                    '    mQry = "Update SaleInvoice Set AdditionalCreditDays = AdditionalCreditDays " & ReportFrm.FGetText(rowAdditionalCreditDays) & " Where DociD = '" & sLed.DocID & "' "
                                    '    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
                                    'End If

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
                            While J < drPayment.Length
                                If AgL.XNull(DtBills.Rows(I)("Subcode")) <> AgL.XNull(DtBills.Rows(I - 1)("Subcode")) Then
                                    mAdjAmt = 0 'mBalPmt
                                    mBalBill = 0 'mBalBill - mAdjAmt

                                    sLed = New StructLedger

                                    sLed.Subcode = AgL.XNull(drPayment(J)("Subcode"))
                                    sLed.LinkedSubcode = AgL.XNull(drPayment(J)("LinkedSubcode"))

                                    sLed.AdjDocID = IIf(AgL.XNull(drPayment(J)("V_Type")) = "", "", AgL.XNull(drPayment(J)("V_Type")) & "-") & AgL.XNull(drPayment(J)("RecID"))
                                    sLed.AdjAmount = mAdjAmt
                                    sLed.DueDate = AgL.XNull(DtBills.Rows(I - 1)("V_Date"))
                                    sLed.AdjDate = AgL.XNull(drPayment(J)("V_Date"))
                                    sLed.AdjVAmount = drPayment(J)("Amount")
                                    sLed.ChqNo = drPayment(J)("ChqNO")
                                    sLed.Narration = AgL.XNull(drPayment(J)("Narration"))


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

                                End If
                                J = J + 1
                            End While


                            'J = drPayment.Length + 1
                            Exit While
                        End If

                        If I > 0 Then
                            If I < DtBills.Rows.Count - 1 Then
                                If AgL.XNull(DtBills.Rows(I)("Subcode")) <> AgL.XNull(DtBills.Rows(I + 1)("Subcode")) Then

                                    While J < drPayment.Length
                                        mAdjAmt = 0 'mBalPmt
                                        mBalBill = 0 'mBalBill - mAdjAmt

                                        sLed = New StructLedger

                                        sLed.Subcode = AgL.XNull(drPayment(J)("Subcode"))
                                        sLed.LinkedSubcode = AgL.XNull(drPayment(J)("LinkedSubcode"))

                                        sLed.AdjDocID = IIf(AgL.XNull(drPayment(J)("V_Type")) = "", "", AgL.XNull(drPayment(J)("V_Type")) & "-") & AgL.XNull(drPayment(J)("RecID"))
                                        sLed.AdjAmount = mAdjAmt
                                        sLed.DueDate = AgL.XNull(DtBills.Rows(I - 1)("V_Date"))
                                        sLed.AdjDate = AgL.XNull(drPayment(J)("V_Date"))
                                        sLed.AdjVAmount = drPayment(J)("Amount")
                                        sLed.ChqNo = drPayment(J)("ChqNO")
                                        sLed.Narration = AgL.XNull(drPayment(J)("Narration"))


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
                                        If sLed.Subcode = "D100001234" Then
                                            Debug.Print("found Again")
                                        End If
                                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
                                        J = J + 1
                                    End While


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

        If ClsMain.FDivisionNameForCustomization(20) = "SHYAMA SHYAM FABRICS" Or ClsMain.FDivisionNameForCustomization(22) = "W SHYAMA SHYAM FABRICS" Or ClsMain.FDivisionNameForCustomization(25) = "SHYAMA SHYAM VENTURES LLP" Or ClsMain.FDivisionNameForCustomization(27) = "W SHYAMA SHYAM VENTURES LLP" Then Exit Sub

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


    Public Sub ProcFormattedPrint(DGL As AgControls.AgDataGrid)
        Try
            'Dim mCondStr$ = ""

            Dim RepName As String
            Dim RepTitle As String

            Dim DsRep As DataSet
            Dim mMultiplier As Double
            Dim sQryPakkaBalance As String
            Dim sQryPakkaPartyCount As String
            Dim sQryLinkedPakaaBalance As String


            Dim sQryPurchaseBrand As String
            Dim sQrySaleBrand As String


            If ValidateInput() = False Then Exit Sub

            CreateTemporaryTables()

            GetDataReady(CreateCondStr, ShowDataIn.Crystal)

            If ClsMain.FDivisionNameForCustomization(22) = "W SHYAMA SHYAM FABRICS" Or ClsMain.FDivisionNameForCustomization(27) = "W SHYAMA SHYAM VENTURES LLP" Then
                mMultiplier = 0.01

                Dim mDbPath As String
                mDbPath = AgL.INIRead(StrPath + "\" + IniName, "CompanyInfo", "ActualDBPath", "")
                Try
                    AgL.Dman_ExecuteNonQry(" attach '" & mDbPath & "' as ODB", AgL.GCn)
                Catch ex As Exception
                End Try
                If ReportFrm.FGetText(rowGroupOn) = "Linked Party" Then
                    sQryPakkaBalance = "(Select IfNull(Sum(AmtDr-AmtCr),0.00) * " & mMultiplier & "  as Balance From ODB.Ledger ODBL Where LinkedSubcode=SG.OmsID )"
                    sQryPakkaPartyCount = "IfNull((Select IfNull(Count(Subcode),0) as PartyCount From ODB.Ledger ODBL Where LinkedSubcode=SG.OmsID Group by Subcode Having Sum(AmtDr-AmtCr)<>0),0)"
                    sQryLinkedPakaaBalance = "Select Sg.Subcode as Subcode, Lsg.Subcode as LinkedSubcode, Max(Sg.Name) as Name, Sum(ODBL.AmtDr-ODBL.AmtCr) * " & mMultiplier & "  as Balance 
                                              From ODB.Ledger ODBL 
                                              Left Join Subgroup Sg On ODBL.Subcode = Sg.OmsID 
                                              Left Join Subgroup LSg On ODBL.LinkedSubcode = LSg.OmsID 
                                              Where 1=1  
                                              Group By Sg.SubCode, Lsg.Subcode Having Sum(AmtDr-AmtCr) <> 0 "
                Else
                    sQryPakkaPartyCount = "(Select 1 as PartyCount)"
                    sQryPakkaBalance = "(Select IfNull(Sum(AmtDr-AmtCr),0.00) * " & mMultiplier & " as Balance From ODB.Ledger ODBL Where Subcode=SG.OmsID )"
                    sQryLinkedPakaaBalance = "Select '' as Subcode, '' as LinkedSubcode, '' as Name, 0.0 as Balance "
                End If

            Else
                If (ClsMain.FDivisionNameForCustomization(25) = "SHYAMA SHYAM VENTURES LLP") Then
                    mMultiplier = 0.01
                Else
                    mMultiplier = 1.0
                End If

                sQryPakkaPartyCount = "(Select 1 as PartyCount)"
                    sQryPakkaBalance = "(Select 0.00 as Balance)"
                    SetAveragePaymentDays()
                    sQryLinkedPakaaBalance = "Select '' as Subcode, '' as LinkedSubcode, '' as Name, 0.0 as Balance "
                End If


                If ReportFrm.FGetText(rowReportType) = "Interest Ledger" Or ReportFrm.FGetText(rowNextStep) = "Interest Ledger" Then
                Dim sQryInterestRate As String
                If AgL.PubServerName = "" Then
                    If ClsMain.FDivisionNameForCustomization(20) = "SHYAMA SHYAM FABRICS" Or ClsMain.FDivisionNameForCustomization(22) = "W SHYAMA SHYAM FABRICS" Or ClsMain.FDivisionNameForCustomization(25) = "SHYAMA SHYAM VENTURES LLP" Or ClsMain.FDivisionNameForCustomization(27) = "W SHYAMA SHYAM VENTURES LLP" Then
                        sQryPurchaseBrand = "(Select group_concat(Brand ,',') || ',' From (Select IfNull(sGroup.Description, sItem.Description)  as Brand  from PurchInvoiceDetail sPID  With (NoLock)  Left Join PurchInvoiceDetailSku sPIDS On sPID.DocID = sPIDS.DocID  Left Join Item sItem On sPID.Item = sItem.Code Left Join Item sGroup On IfNull(sPIDS.ItemGroup,sItem.ItemGroup) = sGroup.Code Where sPID.DocID = PI.DocID And sItem.ItemType  In ('" & ItemTypeCode.TradingProduct & "','Wool')  AND IfNull(sGroup.Code,'') not in ('CourierCharge','HandlingCharge','Packing') Group By IfNull(sGroup.Description, sItem.Description)))"
                        sQrySaleBrand = "(Select group_concat(Brand ,',') || ',' From (Select IfNull(sGroup.Description, sItem.Description)  as Brand  from SaleInvoiceDetail sSID  With (NoLock)  Left Join SaleInvoiceDetailSku sSIDS On sSID.DocID = sSIDS.DocID  Left Join Item sItem On sSID.Item = sItem.Code Left Join Item sGroup On IfNull(sSIDS.ItemGroup,sItem.ItemGroup) = sGroup.Code Where sSID.DocID = SI.DocID And sItem.ItemType  In ('" & ItemTypeCode.TradingProduct & "','Wool')  AND IfNull(sGroup.Code,'') not in ('CourierCharge','HandlingCharge','Packing') Group By IfNull(sGroup.Description, sItem.Description)))"
                    Else
                        sQryPurchaseBrand = "(Select group_concat(Brand ,',') || ',' From (Select sGroup.Description  as Brand  from PurchInvoiceDetailSku sPID  With (NoLock) Left Join Item sGroup On sPID.ItemGroup = sGroup.Code Where sPID.DocID = PI.DocID And sGroup.ItemType In ('" & ItemTypeCode.TradingProduct & "','Wool') Group By sGroup.Description))"
                        sQrySaleBrand = "(Select group_concat(Brand ,',') || ',' From (Select sGroup.Description  as Brand  from SaleInvoiceDetailSku sSID  With (NoLock) Left Join Item sGroup On sSID.ItemGroup = sGroup.Code Where sSID.DocID = SI.DocID And sGroup.ItemType  In ('" & ItemTypeCode.TradingProduct & "','Wool') Group By sGroup.Description))"
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
		                                            Where ssg.Subcode=" & IIf(ReportFrm.FGetText(rowGroupOn) = "Linked Party", "H.Subcode", "H.LinkedSubcode") & "
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

                mQry = "Select * from #TempInterestRecord H "
                DsRep = AgL.FillData(mQry, AgL.GCn)

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
                        " & sQryInterestRate & " as IntRate,
                        Round(IfNull((H.AdjAmount * abs(H.InterestDays-IsNull(Ints.LeaverageDays,0)) * " & sQryInterestRate & " / 36500),0)  * " & mMultiplier & ",2) InterestAmount, Ints.LeaverageDays as InterestLeaverageDays, Ints.Description as InterestSlabDescription, 
                        (Case When IfNull(LSG.CreditLimit,0)>0 Then LSG.CreditLimit Else IfNull(SG.CreditLimit,0) End)  as CreditLimit, " & sQryPakkaBalance & " as PakkaBalance, " & sQryPakkaPartyCount & " as PakkaPartyCount, Sg.AveragePaymentDays, Site.ShortName as SiteShortName
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
                                (Case When  Sum(Case When V.RecordType='Opening' Then V.AmtDr - V.AdjPayment Else 0 End) > 0 Then Sum(Case When V.RecordType='Opening' Then V.AmtDr - V.AdjPayment Else 0 End) Else Sum(Case When V.RecordType='Opening' Then 0 Else V.AmtDr End) End) as AmtDr,
                                Sum(V.AmtCr) as AmtCr, 
                                Max(Case When V.RecordType='Opening' Then '' Else V.Narration End) Narration, 
                                Max(Case When V.RecordType='Opening' Then '' Else V.AdjDocID End) AdjDocID, 
                                Max(Case When V.RecordType='Opening' Then '' Else V.DueDate End) DueDate, 
                                Max(Case When V.RecordType='Opening' Then '' Else V.AdjDate End) AdjDate, 
                                (Case When  Sum(Case When V.RecordType='Opening' Then V.AdjPayment - V.AmtDr Else 0 End) > 0 Then Sum(Case When V.RecordType='Opening' Then V.AdjPayment - V.AmtDr  Else 0 End) Else Sum(Case When V.RecordType='Opening' Then 0 Else V.AdjPayment End) End) AdjPayment,
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
                                IfNull(Max(V.PakkaBalance),0.00) PakkaBalance,
                                0 as PakkaPartyCount,
                                Max(V.AveragePaymentDays) AveragePaymentDays,
                                Null as SiteShortName
                                From (" & mQry & ") as V
                                Group by V.Subcode,(Case When V.RecordType='Opening' Then Null Else V.DocID End)
    
                                "
                End If

                DsRep = AgL.FillData(mQry, AgL.GCn)

                Dim DsAgeing As New DataSet
                Dim DsLinkedPakkaBalance As New DataSet
                DsAgeing = FillFifoOutstanding(CreateCondStr, "Interest Ledger")
                DsLinkedPakkaBalance = AgL.FillData(sQryLinkedPakaaBalance, AgL.GCn)


                RepName = "PartyInterestLedger.rpt"
                RepTitle = "Party Interest Ledger"

                If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records to Print!")
                Dim mCrd As New ReportDocument
                Dim mRepView As New AgLibrary.RepView(AgL)

                AgPL.CreateFieldDefFile1(DsRep, AgL.PubReportPath & "\" & RepName & ".ttx", True)
                AgPL.CreateFieldDefFile1(DsAgeing, AgL.PubReportPath & "\" & RepName & "Ageing" & ".ttx", True)
                AgPL.CreateFieldDefFile1(DsLinkedPakkaBalance, AgL.PubReportPath & "\" & RepName & "LinkedBalance" & ".ttx", True)

                mCrd.Load(AgL.PubReportPath & "\" & RepName)
                mCrd.SetDataSource(DsRep.Tables(0))

                mCrd.OpenSubreport("Ageing").Database.Tables(0).SetDataSource(DsAgeing.Tables(0))
                mCrd.OpenSubreport("LinkedBalance").Database.Tables(0).SetDataSource(DsLinkedPakkaBalance.Tables(0))


                CType(mRepView.Controls("CrvReport"), CrystalDecisions.Windows.Forms.CrystalReportViewer).ReportSource = mCrd
                Formula_Set(mCrd, RepTitle)
                mRepView.Text = ReportFrm.Text
                mRepView.MdiParent = ReportFrm.MdiParent
                mRepView.Show()


            Else

                If AgL.PubServerName = "" Then
                    If ClsMain.FDivisionNameForCustomization(20) = "SHYAMA SHYAM FABRICS" Or ClsMain.FDivisionNameForCustomization(22) = "W SHYAMA SHYAM FABRICS" Or ClsMain.FDivisionNameForCustomization(25) = "SHYAMA SHYAM VENTURES LLP" Or ClsMain.FDivisionNameForCustomization(27) = "W SHYAMA SHYAM VENTURES LLP" Then
                        sQryPurchaseBrand = "(Select group_concat(Brand ,',') || ',' From (Select IfNull(sGroup.Description, sItem.Description)  as Brand  from PurchInvoiceDetail sPID  With (NoLock) Left Join PurchInvoiceDetailSku sPIDS On sPID.DocID = sPIDS.DocID Left Join Item sItem On sPID.Item = sItem.Code Left Join Item sGroup On IfNull(sPIDS.ItemGroup,sItem.ItemGroup) = sGroup.Code Where sPID.DocID = PI.DocID And sItem.ItemType  In ('" & ItemTypeCode.TradingProduct & "','Wool') Group By IfNull(sGroup.Description, sItem.Description)))"
                        sQrySaleBrand = "(Select group_concat(Brand ,',') || ',' From (Select IfNull(sGroup.Description, sItem.Description)  as Brand  from SaleInvoiceDetail sSID  With (NoLock)  Left Join SaleInvoiceDetailSku sSIDS On sSID.DocID = sSIDS.DocID  Left Join Item sItem On sSID.Item = sItem.Code Left Join Item sGroup On IfNull(sSIDS.ItemGroup,sItem.ItemGroup) = sGroup.Code Where sSID.DocID = SI.DocID And sItem.ItemType  In ('" & ItemTypeCode.TradingProduct & "','Wool') Group By IfNull(sGroup.Description, sItem.Description)))"
                    Else
                        sQryPurchaseBrand = "(Select group_concat(Brand ,',') || ',' From (Select sGroup.Description  as Brand  from PurchInvoiceDetailSku sPID  With (NoLock) Left Join Item sGroup On sPID.ItemGroup = sGroup.Code Where sPID.DocID = PI.DocID And sGroup.ItemType  In ('" & ItemTypeCode.TradingProduct & "','Wool') Group By sGroup.Description))"
                        sQrySaleBrand = "(Select group_concat(Brand ,',') || ',' From (Select sGroup.Description  as Brand  from SaleInvoiceDetailSku sSID  With (NoLock) Left Join Item sGroup On sSID.ItemGroup = sGroup.Code Where sSID.DocID = SI.DocID And sGroup.ItemType  In ('" & ItemTypeCode.TradingProduct & "','Wool') Group By sGroup.Description))"
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
                    Sg.name || (Case When IfNull(Ct.CityName,'') <> '' Then ', ' || IfNull(Ct.CityName,'') else '' End) as PartyName, CT.CityName as PartyCity, PSG.Name as LinkedPartyName,
                    (Case When VT.NCat = '" & Ncat.SaleInvoice & "' Then SIT.LrNo || (Case When SIT.NoOfBales Is Not Null Then ' * ' ||  SIT.NoOfBales Else '' End) When VT.NCat = '" & Ncat.PurchaseInvoice & "' then PIT.LrNo || (Case When PIT.NoOfBales Is Not Null Then ' * ' ||  PIT.NoOfBales Else '' End) Else Null End) as LrNo, 
                    (Case When VT.NCat In ('" & Ncat.SaleInvoice & "', '" & Ncat.SaleReturn & "') Then " & sQrySaleBrand & " When VT.NCat In ('" & Ncat.PurchaseInvoice & "','" & Ncat.PurchaseReturn & "') then " & sQryPurchaseBrand & " Else Null End) as Brand, 
                    (Case When VT.NCat = '" & Ncat.SaleInvoice & "' Then SI.AmsDocNo When VT.NCat = '" & Ncat.PurchaseInvoice & "' then PI.AmsDocNo Else Null End) AmsInvNo, 
                    (Case When VT.NCat = '" & Ncat.SaleInvoice & "' Then SI.AmsDocNetAmount When VT.NCat = '" & Ncat.PurchaseInvoice & "' then PI.AmsDocNetAmount Else 0.00 End) * " & mMultiplier & " AmsInvAmt, 
                    (Case When VT.NCat = '" & Ncat.SaleInvoice & "' Then SI.Gross_Amount + IfNull(SIL1.TotalDiscount,0) + IfNull(SIL1.TotalAdditionalDiscount,0) - IfNull(SIL1.TotalAddition,0) When VT.NCat = '" & Ncat.PurchaseInvoice & "' then PI.Gross_Amount + IfNull(PIL1.TotalDiscount,0) + IfNull(PIL1.TotalAdditionalDiscount,0) - IfNull(PIL1.TotalAddition,0) Else 0.0 End) * " & mMultiplier & " GoodsValue, 
                    (Case When VT.NCat = '" & Ncat.SaleInvoice & "' Then IfNull(SIL1.DiscountPer,0) + IfNull(SIL1.AdditionalDiscountPer,0) - IfNull(SIL1.AdditionPer,0) When VT.NCat = '" & Ncat.PurchaseInvoice & "' then IfNull(PIL1.Commission_Per,0) + IfNull(PIL1.AdditionalCommission_Per,0) Else 0.0 End) as DiscountPer, 
                    (Case When VT.NCat = '" & Ncat.SaleInvoice & "' Then IfNull(SIL1.TotalDiscount,0) + IfNull(SIL1.TotalAdditionalDiscount,0) - IfNull(SIL1.TotalAddition,0) When VT.NCat = '" & Ncat.PurchaseInvoice & "' then IfNull(PIL1.TotalDiscount,0) + IfNull(PIL1.TotalAdditionalDiscount,0) - IfNull(PIL1.TotalAddition,0) Else 0.0 End) * " & mMultiplier & " Discount, 
                    (Case When VT.NCat = '" & Ncat.SaleInvoice & "' Then SI.Taxable_Amount When VT.NCat = '" & Ncat.PurchaseInvoice & "' then PI.Taxable_Amount Else 0.0 End) * " & mMultiplier & " TaxableAmt, 
                    (Case When VT.NCat = '" & Ncat.SaleInvoice & "' Then SI.Tax1 + SI.Tax2 + SI.Tax3 + SI.Tax4 + SI.Tax5  When VT.NCat = '" & Ncat.PurchaseInvoice & "' then PI.Tax1 + PI.Tax2 + PI.Tax3 + PI.Tax4 + PI.Tax5 Else 0.0 End) * " & mMultiplier & " TaxAmt,                             
                    (Case When VT.NCat = '" & Ncat.SaleInvoice & "' Then SI.Other_Charge + SI.Other_Charge1 + SI.Other_Charge2 When VT.NCat = '" & Ncat.PurchaseInvoice & "' then  PI.Other_Charge + PI.Other_Charge1 + PI.Other_Charge2 Else 0.0 End) * " & mMultiplier & " OtherChgAmt,                             
                    (Case When VT.NCat = '" & Ncat.SaleInvoice & "' Then SI.Deduction When VT.NCat = '" & Ncat.PurchaseInvoice & "' then PI.Deduction Else 0.0 End)  * " & mMultiplier & " Deduction,                             
                    (Case When VT.NCat = '" & Ncat.SaleInvoice & "' Then SI.Net_Amount When VT.NCat = '" & Ncat.PurchaseInvoice & "' then PI.Net_Amount When VT.NCat = '" & Ncat.OpeningBalance & "' Then (Case When Sg.Nature='Customer' Then H.AmtDr Else H.AmtCr End) Else 0.0 End) * " & mMultiplier & " BillAmt,                         
                    (Case When VT.NCat In ('" & Ncat.PurchaseInvoice & "','" & Ncat.PurchaseReturn & "') then PI.Commission + PI.AdditionalCommission Else 0.0 End)  * " & mMultiplier & " Commission,
                    ((Case When VT.NCat = '" & Ncat.PurchaseInvoice & "' then PI.Net_Amount When VT.NCat = '" & Ncat.OpeningBalance & "' Then (Case When Sg.Nature='Customer' Then 0 Else H.AmtCr End) Else 0.0 End) - (Case When VT.NCat  In ('" & Ncat.PurchaseInvoice & "','" & Ncat.PurchaseReturn & "') And SPI.Docid is null then PI.Commission + PI.AdditionalCommission Else 0.0 End)) * " & mMultiplier & " as NetPurAmt,
                    Div.Div_Name, Site.Name as Site_Name, Ints.LeaverageDays, Sg.AveragePaymentDays, Site.ShortName as SiteShortName, 
                    (Case When VT.NCat In ('" & Ncat.PurchaseInvoice & "','" & Ncat.PurchaseReturn & "') And SPI.DocId is Null then PI.Commission + PI.AdditionalCommission Else 0.0 End)  * " & mMultiplier & " Commission_Unsettled
                    From #TempRecord H  "
                If ReportFrm.FGetText(rowGroupOn) = "Linked Party" Then
                    mQry = mQry & " Left Join SubGroup SG On SG.Subcode =H.SubCode   "
                    'mQry = mQry & " Left Join SubGroup SG On SG.Subcode = IfNull(H.LinkedSubcode,H.SubCode)   "
                Else
                    mQry = mQry & " Left Join SubGroup SG On SG.Subcode =H.SubCode   "
                End If

                mQry = mQry & "
                    LEFT JOIN City CT On SG.CityCode  =CT.CityCode 
                    LEFT JOIN Subgroup PSG On PSG.Subcode = H.LinkedSubcode
                    Left Join SaleInvoice SI On H.DocID = SI.DocId
                    Left Join (Select SIL.DocID, Max(SIL.DiscountPer) as DiscountPer, 
                                Max(SIL.AdditionalDiscountPer) as AdditionalDiscountPer, 
                                Max(SIL.AdditionPer) as AdditionPer, 
                                Sum(SIL.DiscountAmount) as TotalDiscount, 
                                Sum(SIL.AdditionalDiscountAmount) as TotalAdditionalDiscount, 
                                Sum(SIL.AdditionAmount) as TotalAddition
                                From SaleInvoiceDetail SIL
                                Group By SIL.DocID) as SIL1 On H.DocID = SIL1.DocId  
                    Left Join SaleInvoiceTransport SIT On SI.DocID = SIT.DocID
                    Left Join Subgroup ShipParty On SI.ShipToParty = ShipParty.Subcode
                    Left Join PurchInvoice PI On H.DocID = PI.DocId
                    Left Join (Select PIL.DocID, 
                                Max(PIL.Commission_Per) as Commission_Per,
                                Max(PIL.AdditionalCommission_Per) as AdditionalCommission_Per,
                                Sum(PIL.DiscountAmount) as TotalDiscount, 
                                Sum(PIL.AdditionalDiscountAmount) as TotalAdditionalDiscount, 
                                Sum(PIL.AdditionAmount) as TotalAddition
                                From PurchInvoiceDetail PIL
                                Group By PIL.DocID) as PIL1 On H.DocID = PIL1.DocId  
                    Left Join PurchInvoiceTransport PIT On PI.DocID = PIT.DocID
                    Left Join SaleInvoice GenSI On PI.GenDocId = GenSI.DocId
                    Left Join Voucher_Type Vt on H.V_type = Vt.V_type
                    Left Join Division Div On H.Div_Code = Div.Div_Code 
                    Left Join SiteMast Site On H.Site_Code = Site.Code 
                    Left Join InterestSlab IntS On Sg.InterestSlab = Ints.Code 
                    Left Join Cloth_SupplierSettlementInvoices SPI On H.DocID = SPI.PurchaseInvoiceDocID And H.Sr = SPI.PurchaseInvoiceDocIDSr "


                DsRep = AgL.FillData(mQry, AgL.GCn)

                Dim DsLinkedPakkaBalance As New DataSet
                DsLinkedPakkaBalance = AgL.FillData(sQryLinkedPakaaBalance, AgL.GCn)


                If mPartyNature.ToUpper = "Customer".ToUpper Then
                    If FDivisionNameForCustomization(16) = "KAMAKHYA TRADERS" Then
                        RepName = "CustomerLedger_Kamakhya.rpt"
                        RepTitle = "Customer Ledger"
                    Else
                        RepName = "CustomerLedger.rpt"
                        RepTitle = "Customer Ledger"
                    End If
                Else
                    RepName = "SupplierLedger.rpt"
                    RepTitle = "Supplier Ledger"
                End If

                AgPL.CreateFieldDefFile1(DsRep, AgL.PubReportPath & "\" & RepName & ".ttx", True)
                AgPL.CreateFieldDefFile1(DsLinkedPakkaBalance, AgL.PubReportPath & "\" & RepName & "LinkedBalance" & ".ttx", True)



                If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records to Print!")
                Dim mCrd As New ReportDocument
                Dim mRepView As New AgLibrary.RepView(AgL)
                mCrd.Load(AgL.PubReportPath & "\" & RepName)
                mCrd.SetDataSource(DsRep.Tables(0))
                mCrd.OpenSubreport("LinkedBalance").Database.Tables(0).SetDataSource(DsLinkedPakkaBalance.Tables(0))
                CType(mRepView.Controls("CrvReport"), CrystalDecisions.Windows.Forms.CrystalReportViewer).ReportSource = mCrd
                Formula_Set(mCrd, RepTitle)
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



    Public Sub Formula_Set(ByVal mCRD As ReportDocument, Optional ByVal mRepTitle As String = "")
        Dim I As Integer = 0, J As Integer = 0

        'For J = 0 To FGMain.Rows.Count - 1
        '    FGMain.Item(GFieldName, J).Tag = 0
        'Next

        For I = 0 To mCRD.DataDefinition.FormulaFields.Count - 1

            Select Case AgL.UTrim(mCRD.DataDefinition.FormulaFields(I).Name)
                Case AgL.UTrim("comp_name")
                    mCRD.DataDefinition.FormulaFields(I).Text = "'" & AgL.PubDivPrintName & "'"
                Case AgL.UTrim("comp_add")
                    mCRD.DataDefinition.FormulaFields(I).Text = "'" & AgL.PubCompAdd1 & "'"
                Case AgL.UTrim("comp_add1")
                    mCRD.DataDefinition.FormulaFields(I).Text = "'" & AgL.PubCompAdd2 & "'"
                Case AgL.UTrim("comp_Pin")
                    mCRD.DataDefinition.FormulaFields(I).Text = "'" & AgL.PubCompPinCode & "'"
                Case AgL.UTrim("comp_phone")
                    mCRD.DataDefinition.FormulaFields(I).Text = "'" & AgL.PubCompPhone & "'"
                Case AgL.UTrim("comp_city")
                    mCRD.DataDefinition.FormulaFields(I).Text = "'" & AgL.PubCompCity & "'"
                Case AgL.UTrim("Title")
                    mCRD.DataDefinition.FormulaFields(I).Text = "'" & mRepTitle & "'"
                Case AgL.UTrim("Site_Name")
                    mCRD.DataDefinition.FormulaFields(I).Text = "'" & "Branch Name : " & AgL.PubSiteName & " { " & AgL.PubSiteManualCode & " } '"
                Case AgL.UTrim("Division")
                    If AgL.PubDivName IsNot Nothing Then
                        mCRD.DataDefinition.FormulaFields(I).Text = "'" & AgL.PubDivName.ToUpper & " DIVISION" & "'"
                    End If
                Case AgL.UTrim("Tin_No")
                    mCRD.DataDefinition.FormulaFields(I).Text = "'" & "TIN NO : " & AgL.PubCompTIN & "'"
            End Select

        Next


        Dim mCountFormulaStr As Integer = 0
        Dim mFormulaStrName As String = "FormulaStr"

        For J = 0 To ReportFrm.FilterGrid.Rows.Count - 1
            If mCountFormulaStr <= 12 Then
                If ReportFrm.FilterGrid.Item(FrmRepDisplay.GFieldName, J).Tag = 0 And ReportFrm.FilterGrid.Item(FrmRepDisplay.GFilter, J).Value <> "All" And ReportFrm.FilterGrid(FrmRepDisplay.GDisplayOnReport, J).Value = "þ" Then
                    mCountFormulaStr += 1
                    mCRD.DataDefinition.FormulaFields(mFormulaStrName & mCountFormulaStr.ToString).Text = "'" & ReportFrm.FilterGrid.Item(FrmRepDisplay.GFieldName, J).Value & " : " & ReportFrm.FilterGrid.Item(FrmRepDisplay.GFilter, J).Value & "'"
                End If
            End If
        Next
    End Sub
    Private Sub ReportFrm_DGL1EditingControl_Validating(sender As Object, e As CancelEventArgs) Handles ReportFrm.DGL1EditingControl_Validating
        Dim bRowIndex As Integer = 0, bColumnIndex As Integer = 0
        Dim bItemCode As String = ""
        Dim DrTemp As DataRow() = Nothing
        Dim dsTemp As DataSet
        Try
            If ReportFrm.DGL1.CurrentCell Is Nothing Then Exit Sub

            bRowIndex = ReportFrm.DGL1.CurrentCell.RowIndex
            bColumnIndex = ReportFrm.DGL1.CurrentCell.ColumnIndex

            Select Case ReportFrm.DGL1.Columns(bColumnIndex).Name
                Case Col1ReconciliationDate
                    ReportFrm.DGL1.Item(bColumnIndex, bRowIndex).Value = AgL.RetDate(AgL.XNull(ReportFrm.DGL1.Item(bColumnIndex, bRowIndex).Value))

                    mQry = "Update Ledger
                            Set Clg_Date = " & AgL.Chk_Date(ReportFrm.DGL1.Item(bColumnIndex, bRowIndex).Value) & " 
                            Where DocId = '" & ReportFrm.DGL1.Item(Col1SearchCode, bRowIndex).Value & "'
                            And V_SNo ='" & ReportFrm.DGL1.Item("Sr", bRowIndex).Value & "'"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

                    ReportFrm.DGL1.Item(bColumnIndex, bRowIndex).Style.BackColor = Color.Cyan
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class
