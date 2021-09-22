Imports System.IO
Imports AgLibrary
Imports AgLibrary.ClsMain
Imports AgLibrary.ClsMain.agConstants
Imports Customised.ClsMain
Imports Microsoft.Reporting.WinForms

Public Class ClsFinancialDisplay_New

    Dim StrArr1() As String = Nothing, StrArr2() As String = Nothing, StrArr3() As String = Nothing, StrArr4() As String = Nothing, StrArr5() As String = Nothing

    Dim mGRepFormName As String = ""
    Dim mQry As String = ""
    Dim RepTitle As String = ""

    Dim DsReport As DataSet = New DataSet
    Dim DTReport As DataTable = New DataTable
    Dim IntLevel As Int16 = 0

    Dim WithEvents ReportFrm As FrmRepDisplay
    Public Const GFieldName As Byte = 1
    Public Const GFilter As Byte = 2
    Public Const GFilterCode As Byte = 4

    Private Const CnsProfitAndLoss As String = "PRLS"

    Dim mShowReportType As String = ""
    Public Enum FormattingOn
        OnInit = 0
        OnFilter = 1
    End Enum

    Public Class ReportType
        Public Const BalanceSheet As String = "Balance Sheet"
        Public Const ProfitAndLoss As String = "Profit And Loss"
        Public Const TrialBalance As String = "Trial Balance"
        Public Const DetailTrialBalance As String = "Detail Trial Balance"
        Public Const GroupBalance As String = "Group Balance"
        Public Const Ledger As String = "Ledger"
        Public Const Ledger_MonthWise As String = "Ledger Month Wise"
        Public Const Ledger_VoucherTypeWise As String = "Ledger Voucher Type Wise"
        Public Const BankBook As String = "Bank Book"
        Public Const CashBook As String = "Cash Book"
        Public Const CustomerBook As String = "Customer Book"
        Public Const SupplierBook As String = "Supplier Book"
        Public Const GroupLedger_VoucherTypeWise As String = "Group Ledger Voucher Type Wise"
    End Class
    Public Class AddColumn
        Public Const LinkedSubCode As String = "Linked Account"
        Public Const AccountGroup As String = "Account Group"
        Public Const AccountType As String = "Account Type"
    End Class

    Public Const GRCode As String = "SearchCode"
    Public Const DocId As String = "SearchCode"
    Public Const Month As String = "Month"
    Public Const VoucherTypeDesc As String = "Voucher Type"
    Public Const GRName As String = "Name"
    Public Const LGRCode As String = "Linked Account Code"
    Public Const LGRName As String = "Linked Account"
    Public Const LGroupName As String = "Account Group"
    Public Const LAccountType As String = "Account Type"
    Public Const Division As String = "Division"
    Public Const Site As String = "Site"
    Public Const VNo As String = "VNo"
    Public Const VType As String = "Type"
    Public Const VDate As String = "VDate"
    Public Const Narration As String = "Narration"
    Public Const Opening As String = "Opening"
    Public Const Debit As String = "Debit"
    Public Const GRCodeCredit As String = "Grcodecredit"
    Public Const GRNameCredit As String = "Grnamecredit"
    Public Const Credit As String = "Credit"
    Public Const Closing As String = "Closing"
    Public Const Balance As String = "Balance"
    'Public Const GR_SG As String = "GR_SG"
    Public Const DR_CR_CL As String = "DR_CR_CL"
    Public Const DR_CR_OP As String = "DR_CR_OP"
    Public Const AcGroupCode As String = "Ac Group Code"
    Public Const IsAccountGroup As String = "Is Account Group"
    Public Const IsAccountGroupCredit As String = "Is Account Group Credit"

    Dim rowReportType As Integer = 0
    Dim rowFromDate As Integer = 1
    Dim rowToDate As Integer = 2
    Dim rowSite As Integer = 3
    Dim rowDivision As Integer = 4
    Dim rowCostCenter As Integer = 5
    Dim rowShowZeroBalance As Integer = 6
    Dim rowShowMonthWiseLedger As Integer = 7
    Dim rowShowVoucherTypeWiseLedger As Integer = 8
    Dim rowShowVoucherTypeWiseGroupLedger As Integer = 9
    Dim rowAddColumn As Integer = 10
    Dim rowOtherFilter As Integer = 11
    Dim rowIncludeOpening As Integer = 12
    Dim rowShowContraAcInLedger As Integer = 13
    Dim rowLinkedAccountCode As Integer = 14
    Dim rowLedgerAccount As Integer = 15
    Dim rowShowWithHierarchy As Integer = 16
    Dim rowSubgroupNature As Integer = 17
    Dim rowV_Type As Integer = 18
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
    Dim mHelpCostCenterQry$ = "Select 'o' As Tick, Code, Name  FROM CostCenterMast "
    Dim mHelpYesNoQry$ = " Select 'Yes' As Code, 'Yes' AS [Value] Union All Select 'No' As Code, 'No' AS [Value] "
    Dim mHelpSubGroupQry$ = "Select 'o' As Tick, Sg.Code, Sg.Name FROM ViewHelpSubgroup Sg "
    Dim mHelpSubGroupSingleSelectionQry$ = "Select Sg.Code, Sg.Name FROM ViewHelpSubgroup Sg "
    Dim mHelpAcGroupSingleSelectionQry$ = "SELECT Ag.GroupCode AS Code, Ag.GroupName AS Name FROM AcGroup Ag "
    Public Sub Ini_Grid()
        Try
            Dim mQry$ = " Select '" & ReportType.DetailTrialBalance & "' As Code, '" & ReportType.DetailTrialBalance & "' AS [Value] 
                                Union All 
                                Select '" & ReportType.TrialBalance & "' As Code, '" & ReportType.TrialBalance & "' AS [Value] 
                                Union All       
                                Select '" & ReportType.BalanceSheet & "' As Code, '" & ReportType.BalanceSheet & "' AS [Value] 
                                Union All       
                                Select '" & ReportType.ProfitAndLoss & "' As Code, '" & ReportType.ProfitAndLoss & "' AS [Value] "
            ReportFrm.CreateHelpGrid("Report Type", "Report Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mQry, mShowReportType)
            ReportFrm.FilterGrid.Rows(rowReportType).Visible = False

            ReportFrm.CreateHelpGrid("FromDate", "From Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubStartDate)
            ReportFrm.CreateHelpGrid("ToDate", "To Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubEndDate)
            ReportFrm.CreateHelpGrid("Site", "Site", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSiteQry, "[SITECODE]")
            ReportFrm.CreateHelpGrid("Division", "Division", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpDivisionQry, "[DIVISIONCODE]")
            ReportFrm.CreateHelpGrid("CostCenter", "Cost Center", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpCostCenterQry)
            ReportFrm.CreateHelpGrid("ShowZeroBalance", "Show Zero Balance", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mHelpYesNoQry, "Yes")
            ReportFrm.CreateHelpGrid("ShowMonthWiseLedger", "Show Month Wise Ledger", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mHelpYesNoQry, "No")
            ReportFrm.CreateHelpGrid("ShowVoucherTypeWiseLedger", "Show Voucher Type Wise Ledger", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mHelpYesNoQry, "No")
            ReportFrm.CreateHelpGrid("ShowVoucherTypeWiseGroupLedger", "Show Voucher Type Wise Group Ledger", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mHelpYesNoQry, "No")

            mQry = " Select 'o' As Tick, '" & AddColumn.LinkedSubCode & "' As Code, '" & AddColumn.LinkedSubCode & "' AS [Value] "
            mQry += " UNION ALL "
            mQry += " Select 'o' As Tick, '" & AddColumn.AccountGroup & "' As Code, '" & AddColumn.AccountGroup & "' AS [Value] "
            mQry += " UNION ALL "
            mQry += " Select 'o' As Tick, '" & AddColumn.AccountType & "' As Code, '" & AddColumn.AccountType & "' AS [Value] "

            ReportFrm.CreateHelpGrid("AddColumn", "Add Column", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mQry)
            ReportFrm.CreateHelpGrid("OtherFilter", "OtherFilter", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, "")
            ReportFrm.FilterGrid.Rows(rowOtherFilter).Visible = False
            ReportFrm.CreateHelpGrid("IncludeOpening", "Include Opening", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mHelpYesNoQry, "Yes")
            ReportFrm.CreateHelpGrid("ShowContraAcInLedger", "Show Contra A/c In Ledger", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mHelpYesNoQry, "No")
            ReportFrm.CreateHelpGrid("LinkedAccountCode", "Linked Account Code", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.StringType, "", "")
            ReportFrm.FilterGrid.Rows(rowLinkedAccountCode).Visible = False
            ReportFrm.CreateHelpGrid("LedgerAccount", "Ledger Account", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mHelpSubGroupSingleSelectionQry, "",,, 300)
            ReportFrm.CreateHelpGrid("ShowWithHierarchy", "Show With Hierarchy", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mHelpYesNoQry, "No")
            ReportFrm.CreateHelpGrid("SubgroupNature", "Subgroup Nature", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.StringType, "", "")
            ReportFrm.FilterGrid.Rows(rowSubgroupNature).Visible = False
            ReportFrm.CreateHelpGrid("V_Type", "V_Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.StringType, "", "")
            ReportFrm.FilterGrid.Rows(rowV_Type).Visible = False

            FManagerRowVisibility()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub FManagerRowVisibility()
        For I As Integer = 0 To ReportFrm.FilterGrid.Rows.Count - 1
            ReportFrm.FilterGrid.Rows(I).Visible = True
        Next

        ReportFrm.FilterGrid.Rows(rowReportType).Visible = False
        ReportFrm.FilterGrid.Rows(rowOtherFilter).Visible = False
        ReportFrm.FilterGrid.Rows(rowLinkedAccountCode).Visible = False

        Select Case ReportFrm.FGetText(rowReportType)
            Case ReportType.DetailTrialBalance
                ReportFrm.FilterGrid.Rows(rowLedgerAccount).Visible = False
                ReportFrm.FilterGrid.Rows(rowShowWithHierarchy).Visible = False
            Case ReportType.TrialBalance
                ReportFrm.FilterGrid.Rows(rowLedgerAccount).Visible = False
            Case ReportType.BalanceSheet
                ReportFrm.FilterGrid.Rows(rowLedgerAccount).Visible = False
                ReportFrm.FilterGrid.Rows(rowShowWithHierarchy).Visible = True
            Case ReportType.ProfitAndLoss
                ReportFrm.FilterGrid.Rows(rowAddColumn).Visible = False
                ReportFrm.FilterGrid.Rows(rowLedgerAccount).Visible = False
                ReportFrm.FilterGrid.Rows(rowShowWithHierarchy).Visible = True
            Case ReportType.GroupBalance
                ReportFrm.FilterGrid.Rows(rowShowWithHierarchy).Visible = False
            Case ReportType.Ledger, ReportType.Ledger_MonthWise, ReportType.Ledger_VoucherTypeWise
                ReportFrm.FilterGrid.Rows(rowShowWithHierarchy).Visible = False
                ReportFrm.FilterGrid.Rows(rowAddColumn).Visible = False
                ReportFrm.FilterGrid.Rows(rowShowZeroBalance).Visible = False
        End Select
    End Sub
    Private Sub ObjRepFormGlobal_ProcessReport() Handles ReportFrm.ProcessReport
        ProcFinancialDisplay()
    End Sub

    Public Sub New(ByVal mReportFrm As FrmRepDisplay)
        ReportFrm = mReportFrm
    End Sub
    Public Sub ProcFinancialDisplay(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try
            RepTitle = "Financial Display"

            Dim mSummaryOptionOn As Integer = 0
            If ReportFrm.FGetText(rowShowMonthWiseLedger) = "Yes" Then mSummaryOptionOn += 1
            If ReportFrm.FGetText(rowShowVoucherTypeWiseLedger) = "Yes" Then mSummaryOptionOn += 1
            If ReportFrm.FGetText(rowShowVoucherTypeWiseGroupLedger) = "Yes" Then mSummaryOptionOn += 1

            If mSummaryOptionOn > 1 Then
                MsgBox("Only one summary option can be yes at one time. Please choose one option.", MsgBoxStyle.Information)
                Exit Sub
            End If

            If mFilterGrid IsNot Nothing And mGridRow IsNot Nothing Then
                If mGridRow.DataGridView.Columns.Contains("Search Code") = True Then
                    If mFilterGrid.Item(GFilter, rowReportType).Value = ReportType.DetailTrialBalance Then
                        If mFilterGrid.Item(GFilter, rowShowMonthWiseLedger).Value = "Yes" Then
                            mFilterGrid.Item(GFilter, rowReportType).Value = ReportType.Ledger_MonthWise
                        ElseIf mFilterGrid.Item(GFilter, rowShowVoucherTypeWiseLedger).Value = "Yes" Then
                            mFilterGrid.Item(GFilter, rowReportType).Value = ReportType.Ledger_VoucherTypeWise
                        Else
                            mFilterGrid.Item(GFilter, rowReportType).Value = ReportType.Ledger
                        End If
                        mFilterGrid.Item(GFieldName, rowOtherFilter).Value = "Ledger Account"
                        mFilterGrid.Item(GFilter, rowOtherFilter).Value = mGridRow.Cells("Name").Value
                        mFilterGrid.Item(GFilterCode, rowOtherFilter).Value = mGridRow.Cells("Search Code").Value
                        If ReportFrm.DGL1.Columns.Contains("Linked Account Code") = True Then
                            mFilterGrid.Item(GFilter, rowLinkedAccountCode).Value = mGridRow.Cells("Linked Account").Value
                            mFilterGrid.Item(GFilterCode, rowLinkedAccountCode).Value = mGridRow.Cells("Linked Account Code").Value
                        End If
                    ElseIf mFilterGrid.Item(GFilter, rowReportType).Value = ReportType.TrialBalance Then
                        If Not AgL.StrCmp(AgL.XNull(mGridRow.Cells(IsAccountGroup).Value), "Yes") Then
                            mFilterGrid.Item(GFilter, rowReportType).Value = ReportType.Ledger
                            mFilterGrid.Item(GFieldName, rowOtherFilter).Value = "Ledger Account"
                            mFilterGrid.Item(GFilter, rowOtherFilter).Value = mGridRow.Cells("Name").Value
                            mFilterGrid.Item(GFilterCode, rowOtherFilter).Value = mGridRow.Cells("Search Code").Value
                        Else
                            If AgL.StrCmp(ReportFrm.FGetText(rowShowVoucherTypeWiseGroupLedger), "Yes") Then
                                mFilterGrid.Item(GFilter, rowReportType).Value = ReportType.GroupLedger_VoucherTypeWise
                                mFilterGrid.Item(GFieldName, rowOtherFilter).Value = "Ledger Account Group"
                                mFilterGrid.Item(GFilter, rowOtherFilter).Value = mGridRow.Cells("Name").Value
                                mFilterGrid.Item(GFilterCode, rowOtherFilter).Value = mGridRow.Cells("Search Code").Value
                            Else
                                mFilterGrid.Item(GFilter, rowReportType).Value = ReportType.GroupBalance
                                mFilterGrid.Item(GFieldName, rowOtherFilter).Value = "Ledger Account Group"
                                mFilterGrid.Item(GFilter, rowOtherFilter).Value = mGridRow.Cells("Name").Value
                                mFilterGrid.Item(GFilterCode, rowOtherFilter).Value = mGridRow.Cells("Search Code").Value
                            End If
                        End If
                    ElseIf mFilterGrid.Item(GFilter, rowReportType).Value = ReportType.ProfitAndLoss Then
                        If ReportFrm.DGL1.Columns(ReportFrm.DGL1.CurrentCell.ColumnIndex).Name = GRName Or
                                ReportFrm.DGL1.Columns(ReportFrm.DGL1.CurrentCell.ColumnIndex).Name = Debit And
                                AgL.XNull(mGridRow.Cells(GRName).Value) <> "Net Profit" Then
                            mFilterGrid.Item(GFilter, rowReportType).Value = ReportType.GroupBalance
                            mFilterGrid.Item(GFieldName, rowOtherFilter).Value = "Ledger Account Group"
                            mFilterGrid.Item(GFilter, rowOtherFilter).Value = mGridRow.Cells(GRName).Value
                            mFilterGrid.Item(GFilterCode, rowOtherFilter).Value = mGridRow.Cells("Search Code").Value
                        ElseIf ReportFrm.DGL1.Columns(ReportFrm.DGL1.CurrentCell.ColumnIndex).Name = GRNameCredit Or
                                    ReportFrm.DGL1.Columns(ReportFrm.DGL1.CurrentCell.ColumnIndex).Name = Credit And
                                    AgL.XNull(mGridRow.Cells(GRNameCredit).Value) <> "Net Loss" Then
                            mFilterGrid.Item(GFilter, rowReportType).Value = ReportType.GroupBalance
                            mFilterGrid.Item(GFieldName, rowOtherFilter).Value = "Ledger Account Group"
                            mFilterGrid.Item(GFilter, rowOtherFilter).Value = mGridRow.Cells(GRNameCredit).Value
                            mFilterGrid.Item(GFilterCode, rowOtherFilter).Value = mGridRow.Cells("Grcodecredit").Value
                        End If
                    ElseIf mFilterGrid.Item(GFilter, rowReportType).Value = ReportType.BalanceSheet Then
                        If AgL.XNull(mGridRow.Cells(GRName).Value) = "Net Profit" Or
                                AgL.XNull(mGridRow.Cells(GRNameCredit).Value) = "Net Loss" Then
                            mFilterGrid.Item(GFilter, rowReportType).Value = ReportType.ProfitAndLoss
                        Else
                            mFilterGrid.Item(GFilter, rowReportType).Value = ReportType.GroupBalance
                            mFilterGrid.Item(GFieldName, rowOtherFilter).Value = "Ledger Account Group"
                            If ReportFrm.DGL1.Columns(ReportFrm.DGL1.CurrentCell.ColumnIndex).Name = GRName Or
                                 ReportFrm.DGL1.Columns(ReportFrm.DGL1.CurrentCell.ColumnIndex).Name = Debit Then
                                mFilterGrid.Item(GFilter, rowOtherFilter).Value = mGridRow.Cells(GRName).Value
                                mFilterGrid.Item(GFilterCode, rowOtherFilter).Value = mGridRow.Cells("Search Code").Value
                            ElseIf ReportFrm.DGL1.Columns(ReportFrm.DGL1.CurrentCell.ColumnIndex).Name = GRNameCredit Or
                            ReportFrm.DGL1.Columns(ReportFrm.DGL1.CurrentCell.ColumnIndex).Name = Credit Then
                                mFilterGrid.Item(GFilter, rowOtherFilter).Value = mGridRow.Cells(GRNameCredit).Value
                                mFilterGrid.Item(GFilterCode, rowOtherFilter).Value = mGridRow.Cells("Grcodecredit").Value
                            End If
                        End If
                    ElseIf mFilterGrid.Item(GFilter, rowReportType).Value = ReportType.GroupBalance Or mFilterGrid.Item(GFilter, rowReportType).Value = ReportType.BankBook Or mFilterGrid.Item(GFilter, rowReportType).Value = ReportType.CashBook Or mFilterGrid.Item(GFilter, rowReportType).Value = ReportType.CustomerBook Or mFilterGrid.Item(GFilter, rowReportType).Value = ReportType.SupplierBook Then
                        If AgL.StrCmp(AgL.XNull(mGridRow.Cells(IsAccountGroup).Value), "Yes") Then
                            mFilterGrid.Item(GFilter, rowReportType).Value = ReportType.GroupBalance
                            mFilterGrid.Item(GFieldName, rowOtherFilter).Value = "Ledger Account Group"
                            mFilterGrid.Item(GFilter, rowOtherFilter).Value = mGridRow.Cells("Name").Value
                            mFilterGrid.Item(GFilterCode, rowOtherFilter).Value = mGridRow.Cells("Search Code").Value
                        Else
                            If mFilterGrid.Item(GFilter, rowShowMonthWiseLedger).Value = "Yes" Then
                                mFilterGrid.Item(GFilter, rowReportType).Value = ReportType.Ledger_MonthWise
                            ElseIf mFilterGrid.Item(GFilter, rowShowVoucherTypeWiseLedger).Value = "Yes" Then
                                mFilterGrid.Item(GFilter, rowReportType).Value = ReportType.Ledger_VoucherTypeWise
                            Else
                                mFilterGrid.Item(GFilter, rowReportType).Value = ReportType.Ledger
                            End If
                            mFilterGrid.Item(GFilter, rowOtherFilter).Value = mGridRow.Cells("Name").Value
                            mFilterGrid.Item(GFilterCode, rowOtherFilter).Value = mGridRow.Cells("Search Code").Value
                        End If
                    ElseIf mFilterGrid.Item(GFilter, rowReportType).Value = ReportType.Ledger_MonthWise Then
                        mFilterGrid.Item(GFilter, rowReportType).Value = ReportType.Ledger
                        mFilterGrid.Item(GFieldName, rowOtherFilter).Value = "Ledger Account"
                        mFilterGrid.Item(GFilter, rowFromDate).Value = AgL.RetMonthStartDate(CDate(mGridRow.Cells("Month").Value))
                        mFilterGrid.Item(GFilter, rowToDate).Value = AgL.RetMonthEndDate(CDate(mGridRow.Cells("Month").Value))
                        mFilterGrid.Item(GFilter, rowOtherFilter).Value = mGridRow.Cells("Name").Value
                        mFilterGrid.Item(GFilterCode, rowOtherFilter).Value = mGridRow.Cells("Search Code").Value
                    ElseIf mFilterGrid.Item(GFilter, rowReportType).Value = ReportType.Ledger_VoucherTypeWise Then
                        mFilterGrid.Item(GFilter, rowReportType).Value = ReportType.Ledger
                        mFilterGrid.Item(GFieldName, rowOtherFilter).Value = "Ledger Account"
                        mFilterGrid.Item(GFilterCode, rowV_Type).Value = "'" & mGridRow.Cells("Type").Value & "'"
                        mFilterGrid.Item(GFilter, rowOtherFilter).Value = mGridRow.Cells("Name").Value
                        mFilterGrid.Item(GFilterCode, rowOtherFilter).Value = mGridRow.Cells("Search Code").Value
                    ElseIf mFilterGrid.Item(GFilter, rowReportType).Value = ReportType.GroupLedger_VoucherTypeWise Then
                        mFilterGrid.Item(GFilter, rowReportType).Value = ReportType.Ledger
                        mFilterGrid.Item(GFieldName, rowOtherFilter).Value = "Ledger Account Group"
                        mFilterGrid.Item(GFilterCode, rowV_Type).Value = "'" & mGridRow.Cells("Type").Value & "'"
                        mFilterGrid.Item(GFilter, rowOtherFilter).Value = mGridRow.Cells("Name").Value
                        mFilterGrid.Item(GFilterCode, rowOtherFilter).Value = mGridRow.Cells("Search Code").Value
                    ElseIf mFilterGrid.Item(GFilter, rowReportType).Value = ReportType.Ledger Then
                        ClsMain.FOpenForm(mGridRow.Cells("Search Code").Value, ReportFrm)
                        Exit Sub
                    End If
                Else
                    Exit Sub
                End If
            End If

            If ReportFrm.FGetText(rowReportType) = ReportType.BalanceSheet Or
                ReportFrm.FGetText(rowReportType) = ReportType.ProfitAndLoss Then
                IntLevel = 1
            Else
                IntLevel = 0
            End If

            Select Case ReportFrm.FGetText(rowReportType)
                Case ReportType.DetailTrialBalance
                    ReportFrm.DGL1.AllowUserToOrderColumns = True
                    ReportFrm.DGL1.AllowUserToResizeColumns = True
                    ReportFrm.ReportSubTitle = ""
                    FDTrailBalance_Disp()
                Case ReportType.TrialBalance
                    ReportFrm.DGL1.AllowUserToOrderColumns = True
                    ReportFrm.DGL1.AllowUserToResizeColumns = True
                    ReportFrm.ReportSubTitle = ""
                    FTrailBalance_Disp()
                Case ReportType.BalanceSheet
                    ReportFrm.DGL1.AllowUserToOrderColumns = False
                    ReportFrm.DGL1.AllowUserToResizeColumns = False
                    ReportFrm.ReportSubTitle = ""
                    FBalanceSheet_Disp()
                Case ReportType.ProfitAndLoss
                    ReportFrm.DGL1.AllowUserToOrderColumns = False
                    ReportFrm.DGL1.AllowUserToResizeColumns = False
                    ReportFrm.ReportSubTitle = ""
                    FProfitAndLoss_Disp()
                Case ReportType.GroupBalance
                    ReportFrm.DGL1.AllowUserToOrderColumns = True
                    ReportFrm.DGL1.AllowUserToResizeColumns = True
                    ReportFrm.ReportSubTitle = "Account Group : " & ReportFrm.FilterGrid.Item(GFilter, rowOtherFilter).Value
                    FDisplay_Level_Group()
                Case ReportType.BankBook
                    ReportFrm.FilterGrid.Item(GFilterCode, rowSubgroupNature).Value = "Bank"
                    ReportFrm.FilterGrid.Item(GFilter, rowSubgroupNature).Value = "Bank"
                    ReportFrm.DGL1.AllowUserToOrderColumns = False
                    ReportFrm.DGL1.AllowUserToResizeColumns = False
                    ReportFrm.ReportSubTitle = ""
                    FDisplay_Level_Group()
                Case ReportType.CashBook
                    ReportFrm.FilterGrid.Item(GFilterCode, rowSubgroupNature).Value = "Cash"
                    ReportFrm.FilterGrid.Item(GFilter, rowSubgroupNature).Value = "Cash"
                    ReportFrm.DGL1.AllowUserToOrderColumns = False
                    ReportFrm.DGL1.AllowUserToResizeColumns = False
                    ReportFrm.ReportSubTitle = ""
                    FDisplay_Level_Group()
                Case ReportType.CustomerBook
                    ReportFrm.FilterGrid.Item(GFilterCode, rowSubgroupNature).Value = "Customer"
                    ReportFrm.FilterGrid.Item(GFilter, rowSubgroupNature).Value = "Customer"
                    ReportFrm.DGL1.AllowUserToOrderColumns = False
                    ReportFrm.DGL1.AllowUserToResizeColumns = False
                    ReportFrm.ReportSubTitle = ""
                    FDisplay_Level_Group()

                Case ReportType.SupplierBook
                    ReportFrm.FilterGrid.Item(GFilterCode, rowSubgroupNature).Value = "Supplier"
                    ReportFrm.FilterGrid.Item(GFilter, rowSubgroupNature).Value = "Supplier"
                    ReportFrm.DGL1.AllowUserToOrderColumns = False
                    ReportFrm.DGL1.AllowUserToResizeColumns = False
                    ReportFrm.ReportSubTitle = ""
                    FDisplay_Level_Group()

                Case ReportType.Ledger, ReportType.Ledger_MonthWise, ReportType.Ledger_VoucherTypeWise, ReportType.GroupLedger_VoucherTypeWise
                    If ReportFrm.FGetText(rowLedgerAccount) <> "" Then
                        ReportFrm.FilterGrid.Item(GFilterCode, rowOtherFilter).Value = ReportFrm.FGetCode(rowLedgerAccount)
                        ReportFrm.FilterGrid.Item(GFilter, rowOtherFilter).Value = ReportFrm.FGetText(rowLedgerAccount)
                    End If
                    ReportFrm.DGL1.AllowUserToResizeColumns = False
                    ReportFrm.DGL1.AllowUserToOrderColumns = False
                    ReportFrm.ReportSubTitle = "Ledger : " & ReportFrm.FilterGrid.Item(GFilter, rowOtherFilter).Value

                    Dim bStrForType As String = ""
                    If AgL.StrCmp(ReportFrm.FGetText(rowShowVoucherTypeWiseGroupLedger), "Yes") Then
                        bStrForType = "Account Group"
                    Else
                        bStrForType = "Account"
                    End If
                    FDisplay_SubGroup(ReportFrm.FilterGrid.Item(GFilterCode, rowOtherFilter).Value, ReportFrm.FilterGrid.Item(GFilter, rowOtherFilter).Value, bStrForType)
            End Select
            ReportFrm.DGL1.DefaultCellStyle.WrapMode = DataGridViewTriState.True
            ReportFrm.DGL1.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells)
            FManagerRowVisibility()

            If AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Then
                ReportFrm.DGL1.AllowUserToResizeColumns = True
                ReportFrm.DGL1.AllowUserToOrderColumns = True
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            DTReport = Nothing
        Finally
            For I As Integer = 0 To ReportFrm.DGL1.Columns.Count - 1
                ReportFrm.DGL2.Columns(I).Visible = ReportFrm.DGL1.Columns(I).Visible
                ReportFrm.DGL2.Columns(I).Width = ReportFrm.DGL1.Columns(I).Width
                ReportFrm.DGL2.Columns(I).DisplayIndex = ReportFrm.DGL1.Columns(I).DisplayIndex
            Next
        End Try
    End Sub
    Private Sub FDTrailBalance_Disp()
        Dim DTTemp As DataTable
        Dim StrSQLQuery As String = ""
        Dim StrCondition1 As String = "", StrConditionOP As String = ""
        Dim StrConditionZeroBal As String = ""
        Dim StrConditionAcGroup As String = ""
        Dim DblDebit_Total As Double, DblCredit_Total As Double
        Dim DblCLDR As Double, DblCLCR As Double, OpeningTotal As Double, ClosingTotal As Double
        Dim mCondStr$ = ""
        Dim DtStockValue As DataTable = Nothing
        Dim I As Integer
        Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"
        Dim mExcludeLedgerAccountsFromTrial As String = ""
        mExcludeLedgerAccountsFromTrial = ClsMain.FGetSettings(SettingFields.ExcludeLedgerAccountsFromTrial, SettingType.General, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")

        FGetStockValuesInDataTable(DtStockValue, ReportFrm.FGetText(rowFromDate))

        FCreateDataTable(ReportType.DetailTrialBalance)

        If UCase(ReportFrm.FGetText(rowShowZeroBalance)) = "No" Then StrConditionZeroBal = "Having (IfNull(Sum(Tmp.OPBal),0)+IfNull(Sum(Tmp.AmtDr),0)-IfNull(Sum(Tmp.AmtCr),0)) <> 0 "
        StrConditionOP = " Where Date(LG.V_Date) < " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowFromDate)).ToString("s")) & " "
        StrConditionOP += Replace(ReportFrm.GetWhereCondition("LG.Site_Code", rowSite), "''", "'")
        StrConditionOP += Replace(ReportFrm.GetWhereCondition("LG.DivCode", rowDivision), "''", "'")
        StrConditionOP += " And Date(LG.V_Date) >= (Case When Ag.GroupNature in ('R','E') 
                                    Then " & AgL.Chk_Date(CDate(AgL.PubStartDate).ToString("s")) & " 
                                    Else '1900/01/01' End) "
        If mExcludeLedgerAccountsFromTrial <> "" Then
            StrConditionOP += " And IfNull(Sg.Status,'Active') Not In ('" & mExcludeLedgerAccountsFromTrial.Replace("+", "','") & "') "
        End If


        StrCondition1 = " Where ( Date(LG.V_Date) Between " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowFromDate)).ToString("s")) & " 
                                And " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowToDate)).ToString("s")) & " ) "
        StrCondition1 += Replace(ReportFrm.GetWhereCondition("LG.Site_Code", rowSite), "''", "'")
        StrCondition1 += Replace(ReportFrm.GetWhereCondition("LG.DivCode", rowDivision), "''", "'")
        If mExcludeLedgerAccountsFromTrial <> "" Then
            StrCondition1 += " And IfNull(Sg.Status,'Active') Not In ('" & mExcludeLedgerAccountsFromTrial.Replace("+", "','") & "') "
        End If


        StrConditionAcGroup += ReportFrm.GetWhereCondition("Sg.GroupCode", rowOtherFilter)



        '========== For Detail Section =======

        StrSQLQuery = "Select SubCode, "
        StrSQLQuery += IIf(ReportFrm.FGetText(rowAddColumn).ToString.Contains(AddColumn.LinkedSubCode), "LSCode, ", "")
        StrSQLQuery += IIf(ReportFrm.FGetText(rowAddColumn).ToString.Contains(AddColumn.AccountGroup), "GroupCode, ", "")
        StrSQLQuery += IIf(ReportFrm.FGetText(rowAddColumn).ToString.Contains(AddColumn.AccountType), "SubGroupType, ", "")
        StrSQLQuery += "Max(SName) As SName, "
        StrSQLQuery += IIf(ReportFrm.FGetText(rowAddColumn).ToString.Contains(AddColumn.LinkedSubCode), "Max(LSName) As LSName, ", "")
        StrSQLQuery += IIf(ReportFrm.FGetText(rowAddColumn).ToString.Contains(AddColumn.AccountGroup), "Max(GroupName) As GroupName, ", "")
        StrSQLQuery += "IfNull(Sum(OPBal),0.00) As OPBal, "
        StrSQLQuery += "IfNull(Sum(AmtDr),0.00) As AmtDr, "
        StrSQLQuery += "IfNull(Sum(AmtCr),0.00) As AmtCr "
        StrSQLQuery += "From ( "

        If ReportFrm.FGetText(rowIncludeOpening) = "Yes" Then
            StrSQLQuery += "Select IfNull(SG.Code,'') As SubCode, "
            StrSQLQuery += IIf(ReportFrm.FGetText(rowAddColumn).ToString.Contains(AddColumn.LinkedSubCode), "IfNull(LG.LinkedSubcode,'') As LSCode, ", "")
            StrSQLQuery += IIf(ReportFrm.FGetText(rowAddColumn).ToString.Contains(AddColumn.AccountGroup), "IfNull(Ag.GroupCode,'') As GroupCode, ", "")
            StrSQLQuery += IIf(ReportFrm.FGetText(rowAddColumn).ToString.Contains(AddColumn.AccountType), "IfNull(SG.SubGroupType,'') As SubGroupType, ", "")
            StrSQLQuery += "(IfNull(Max(SG.Name),'') || ' - ' || IfNull(Max(CT.CityName),'')) As SName, "
            StrSQLQuery += IIf(ReportFrm.FGetText(rowAddColumn).ToString.Contains(AddColumn.LinkedSubCode), "(IfNull(Max(LSG.Name),'')) As LSName, ", "")
            StrSQLQuery += IIf(ReportFrm.FGetText(rowAddColumn).ToString.Contains(AddColumn.AccountGroup), "(IfNull(Max(Ag.GroupName),'')) As GroupName, ", "")

            StrSQLQuery += "Case When Max(Ag.GroupName) = 'Opening Stock' Then "
            If AgL.PubServerName = "" Then
                StrSQLQuery += " Case When Max(julianday(" & AgL.Chk_Date(AgL.PubStartDate) & ") - julianday(LG.V_Date)) = 1 "
            Else
                StrSQLQuery += " Case When Max(DateDiff(DAY," & AgL.Chk_Date(AgL.PubStartDate) & ",LG.V_Date)) = 1 "
            End If
            StrSQLQuery += " Then (IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LG.AmtCr),0)) Else 0 End  "
            StrSQLQuery += " Else (IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LG.AmtCr),0)) End As OPBal, "
            'StrSQLQuery += "(IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LG.AmtCr),0)) As OPBal, "
            StrSQLQuery += "0.00 As AmtDr, "
            StrSQLQuery += "0.00 As AmtCr "
            StrSQLQuery += "From Ledger LG "
            StrSQLQuery += "Left Join ViewHelpSubgroup SG On LG.SubCode=SG.Code  "
            StrSQLQuery += IIf(ReportFrm.FGetText(rowAddColumn).ToString.Contains(AddColumn.LinkedSubCode), "Left Join ViewHelpSubgroup LSG On LG.LinkedSubcode=LSG.Code  ", "")
            StrSQLQuery += "Left Join AcGroup Ag On Ag.GroupCode=SG.GroupCode "
            StrSQLQuery += "Left Join City CT On CT.CityCode=SG.CityCode "
            StrSQLQuery += StrConditionOP & StrConditionAcGroup
            StrSQLQuery += "Group By IfNull(SG.Code,'') "
            StrSQLQuery += IIf(ReportFrm.FGetText(rowAddColumn).ToString.Contains(AddColumn.LinkedSubCode), ",IfNull(LG.LinkedSubcode,'') ", "")
            StrSQLQuery += IIf(ReportFrm.FGetText(rowAddColumn).ToString.Contains(AddColumn.AccountGroup), ",IfNull(Ag.GroupCode,'') ", "")
            StrSQLQuery += IIf(ReportFrm.FGetText(rowAddColumn).ToString.Contains(AddColumn.AccountType), ",IfNull(Sg.SubGroupType,'') ", "")
            StrSQLQuery += "Having (IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LG.AmtCr),0)) <> 0 "
            StrSQLQuery += "Union All "
        End If

        StrSQLQuery += "Select	IfNull(SG.Code,'') As SubCode, "
        StrSQLQuery += IIf(ReportFrm.FGetText(rowAddColumn).ToString.Contains(AddColumn.LinkedSubCode), "IfNull(LG.LinkedSubcode,'') As LSCode, ", "")
        StrSQLQuery += IIf(ReportFrm.FGetText(rowAddColumn).ToString.Contains(AddColumn.AccountGroup), "IfNull(Ag.GroupCode,'') As GroupCode, ", "")
        StrSQLQuery += IIf(ReportFrm.FGetText(rowAddColumn).ToString.Contains(AddColumn.AccountType), "IfNull(Sg.SubGroupType,'') As SubGroupType, ", "")
        StrSQLQuery += "IfNull(Max(SG.Name),'') As SName, "
        StrSQLQuery += IIf(ReportFrm.FGetText(rowAddColumn).ToString.Contains(AddColumn.LinkedSubCode), "(IfNull(Max(LSG.Name),'')) As LSName, ", "")
        StrSQLQuery += IIf(ReportFrm.FGetText(rowAddColumn).ToString.Contains(AddColumn.AccountGroup), "(IfNull(Max(Ag.GroupName),'')) As GroupName, ", "")
        StrSQLQuery += "0.00 As OPBal, "

        'StrSQLQuery += "(Case When (IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LG.AmtCr),0))>0 Then  "
        'StrSQLQuery += "(IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LG.AmtCr),0)) Else 0.00 End)*1.0 As AmtDr, "
        'StrSQLQuery += "(Case When (IfNull(Sum(LG.AmtCr),0)-IfNull(Sum(LG.AmtDr),0))>0 Then "
        'StrSQLQuery += "(IfNull(Sum(LG.AmtCr),0)-IfNull(Sum(LG.AmtDr),0)) Else 0.00 End)*1.0 As AmtCr "

        'Changed ON 10/May/2019 By Akash because Detail Trial Balance should show Totals Of Debit And Credit Balances
        StrSQLQuery += "IfNull(Sum(LG.AmtDr),0)*1.0 As AmtDr, "
        StrSQLQuery += "IfNull(Sum(LG.AmtCr),0)*1.0 As AmtCr "
        StrSQLQuery += "From Ledger LG "
        StrSQLQuery += "Left Join ViewHelpSubgroup SG On LG.SubCode=SG.Code "
        StrSQLQuery += "Left Join City CT On CT.CityCode=SG.CityCode "
        StrSQLQuery += IIf(ReportFrm.FGetText(rowAddColumn).ToString.Contains(AddColumn.LinkedSubCode), "Left Join ViewHelpSubgroup LSG On LG.LinkedSubcode=LSG.Code  ", "")
        StrSQLQuery += "Left Join AcGroup Ag On Ag.GroupCode=SG.GroupCode "
        StrSQLQuery += StrCondition1 & StrConditionAcGroup
        StrSQLQuery += "Group By IfNull(SG.Code,'')"
        StrSQLQuery += IIf(ReportFrm.FGetText(rowAddColumn).ToString.Contains(AddColumn.LinkedSubCode), ",IfNull(LG.LinkedSubcode,'')", "")
        StrSQLQuery += IIf(ReportFrm.FGetText(rowAddColumn).ToString.Contains(AddColumn.AccountGroup), ",IfNull(Ag.GroupCode,'')", "")
        StrSQLQuery += IIf(ReportFrm.FGetText(rowAddColumn).ToString.Contains(AddColumn.AccountType), ",IfNull(Sg.SubGroupType,'')", "")
        StrSQLQuery += ") As Tmp "
        StrSQLQuery += "Group By SubCode "
        StrSQLQuery += IIf(ReportFrm.FGetText(rowAddColumn).ToString.Contains(AddColumn.LinkedSubCode), ",LSCode ", "")
        StrSQLQuery += IIf(ReportFrm.FGetText(rowAddColumn).ToString.Contains(AddColumn.AccountGroup), ",GroupCode ", "")
        StrSQLQuery += IIf(ReportFrm.FGetText(rowAddColumn).ToString.Contains(AddColumn.AccountType), ",SubGroupType ", "")
        StrSQLQuery += StrConditionZeroBal
        StrSQLQuery += "Order By IfNull(Max(SName),'')"
        StrSQLQuery += IIf(ReportFrm.FGetText(rowAddColumn).ToString.Contains(AddColumn.LinkedSubCode), ",IfNull(Max(LSName),'')  ", "")

        DTTemp = AgL.FillData(StrSQLQuery, AgL.GCn).Tables(0)


        DblDebit_Total = 0
        DblCredit_Total = 0
        For I = 0 To DTTemp.Rows.Count - 1
            DblCLCR = 0
            DblCLDR = 0
            DTReport.Rows.Add()
            'DTReport.Rows(I)(GR_SG) = "S"
            DTReport.Rows(I)(GRCode) = AgL.XNull(DTTemp.Rows(I).Item("SubCode"))
            DTReport.Rows(I)(GRName) = AgL.XNull(DTTemp.Rows(I).Item("SName"))

            If DTTemp.Columns.Contains("LSName") Then
                DTReport.Rows(I)(LGRCode) = AgL.XNull(DTTemp.Rows(I).Item("LSCode"))
                DTReport.Rows(I)(LGRName) = AgL.XNull(DTTemp.Rows(I).Item("LSName"))
            End If
            If DTTemp.Columns.Contains("GroupName") Then
                DTReport.Rows(I)(LGroupName) = AgL.XNull(DTTemp.Rows(I).Item("GroupName"))
            End If
            If DTTemp.Columns.Contains("SubGroupType") Then
                DTReport.Rows(I)(LAccountType) = AgL.XNull(DTTemp.Rows(I).Item("SubGroupType"))
            End If

            DTReport.Rows(I)(Opening) = IIf(AgL.VNull(DTTemp.Rows(I).Item("OPBal")) <> 0, Format(Math.Abs(AgL.VNull(DTTemp.Rows(I).Item("OPBal"))), "0.00"), System.DBNull.Value)
            DTReport.Rows(I)(DR_CR_OP) = IIf(AgL.VNull(DTTemp.Rows(I).Item("OPBal")) > 0, "Dr", "Cr")
            If AgL.VNull(DTTemp.Rows(I).Item("OPBal")) = 0 Then DTReport.Rows(I)(DR_CR_OP) = ""
            If AgL.VNull(DTTemp.Rows(I).Item("OPBal")) > 0 Then DblCLDR = Math.Abs(AgL.VNull(DTTemp.Rows(I).Item("OPBal"))) Else DblCLCR = Math.Abs(AgL.VNull(DTTemp.Rows(I).Item("OPBal")))

            OpeningTotal += AgL.VNull(DTTemp.Rows(I).Item("OPBal"))

            DTReport.Rows(I)(Debit) = IIf(AgL.VNull(DTTemp.Rows(I).Item("AmtDr")) > 0, Format(AgL.VNull(DTTemp.Rows(I).Item("AmtDr")), "0.00"), System.DBNull.Value)
            DTReport.Rows(I)(Credit) = IIf(AgL.VNull(DTTemp.Rows(I).Item("AmtCr")) > 0, Format(AgL.VNull(DTTemp.Rows(I).Item("AmtCr")), "0.00"), System.DBNull.Value)
            DblDebit_Total = DblDebit_Total + Format(AgL.VNull(DTTemp.Rows(I).Item("AmtDr")), "0.00")
            DblCredit_Total = DblCredit_Total + Format(AgL.VNull(DTTemp.Rows(I).Item("AmtCr")), "0.00")
            DblCLDR += Val(AgL.VNull(DTReport.Rows(I)(Debit)))
            DblCLCR += Val(AgL.VNull(DTReport.Rows(I)(Credit)))
            DTReport.Rows(I)(Closing) = IIf((DblCLDR - DblCLCR) <> 0, Format(Math.Abs(DblCLDR - DblCLCR), "0.00"), System.DBNull.Value)
            DTReport.Rows(I)(DR_CR_CL) = IIf((DblCLDR - DblCLCR) > 0, "Dr", "Cr")
            If (DblCLDR - DblCLCR) = 0 Then DTReport.Rows(I)(DR_CR_CL) = ""

            ClosingTotal += (DblCLDR - DblCLCR)
        Next

        'For J As Integer = 0 To DtStockValue.Rows.Count - 1
        '    If AgL.VNull(DtStockValue.Rows(J)("OpeningStockValue")) <> 0 Then
        '        DTReport.Rows.Add()
        '        DTReport.Rows(I)(IsAccountGroup) = "Yes"
        '        DTReport.Rows(I)(GRName) = "Opening Stock" + IIf(AgL.XNull(DtStockValue.Rows(J)("Remark")) <> "", " " + AgL.XNull(DtStockValue.Rows(J)("Remark")), "")
        '        DTReport.Rows(I)(Debit) = DtStockValue.Rows(J)("OpeningStockValue")
        '        DblDebit_Total = DblDebit_Total + Format(AgL.VNull(DTReport.Rows(I)(Debit)), "0.00")
        '        DblCredit_Total = DblCredit_Total + Format(AgL.VNull(DTReport.Rows(I)(Credit)), "0.00")
        '        I = I + 1
        '    End If
        'Next


        For J As Integer = 0 To DtStockValue.Rows.Count - 1
            If AgL.VNull(DtStockValue.Rows(J)("OpeningStockValue")) <> 0 Then
                DTReport.Rows.Add()
                DTReport.Rows(I)(GRName) = "Opening Stock" + IIf(AgL.XNull(DtStockValue.Rows(J)("Remark")) <> "", " " + AgL.XNull(DtStockValue.Rows(J)("Remark")), "")
                DTReport.Rows(I)(Debit) = DtStockValue.Rows(J)("OpeningStockValue")

                DblDebit_Total = DblDebit_Total + Format(AgL.VNull(DTReport.Rows(I)(Debit)), "0.00")
                DblCredit_Total = DblCredit_Total + Format(AgL.VNull(DTReport.Rows(I)(Credit)), "0.00")
                DblCLDR += Val(AgL.VNull(DTReport.Rows(I)(Debit)))
                DblCLCR += Val(AgL.VNull(DTReport.Rows(I)(Credit)))
                DTReport.Rows(I)(Closing) = IIf((DblCLDR - DblCLCR) <> 0, Format(Math.Abs(DblCLDR - DblCLCR), "0.00"), System.DBNull.Value)
                DTReport.Rows(I)(DR_CR_CL) = IIf((DblCLDR - DblCLCR) > 0, "Dr", "Cr")
                If (DblCLDR - DblCLCR) = 0 Then DTReport.Rows(I)(DR_CR_CL) = ""

                ClosingTotal += (DblCLDR - DblCLCR)
                I = I + 1
            End If
        Next



        DTReport.Rows.Add()

        If AgL.XNull(ReportFrm.FGetCode(rowOtherFilter)) = "" Then
            If (DblDebit_Total - DblCredit_Total + OpeningTotal) > 0 Then
                DTReport.Rows(I)(GRName) = "Difference In Trial Balance"
                DTReport.Rows(I)(Credit) = Format(Math.Abs(DblDebit_Total - DblCredit_Total + OpeningTotal), "0.00")
                DblCredit_Total = DblCredit_Total + Format((DblDebit_Total - DblCredit_Total), "0.00")

            ElseIf (DblDebit_Total - DblCredit_Total + OpeningTotal) < 0 Then
                DTReport.Rows(I)(GRName) = "Difference In Trial Balance"
                DTReport.Rows(I)(Debit) = Format(Math.Abs(DblDebit_Total - DblCredit_Total + OpeningTotal), "0.00")
                DblDebit_Total = DblDebit_Total + Format((DblCredit_Total - DblDebit_Total), "0.00")
            End If
        End If




        If DTReport.Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

        'ReportFrm.Text = "Financial Display - " + ReportFrm.FGetText(rowReportType)
        ReportFrm.Text = ReportFrm.FGetText(rowReportType)
        ReportFrm.ClsRep = Me
        ReportFrm.ReportProcName = "ProcFinancialDisplay"
        'ReportFrm.IsManualAggregate = False

        DsReport = New DataSet()
        DsReport.Tables.Add(DTReport)

        ReportFrm.ProcFillGrid(DsReport)

        FormatDetailTrialBalance(FormattingOn.OnInit)

        ReportFrm.DGL2.Item(Opening, 0).Value = Format(Math.Abs(OpeningTotal), "0.00")
        If OpeningTotal > 0 Then
            ReportFrm.DGL2.Item(DR_CR_OP, 0).Value = "Dr"
        ElseIf OpeningTotal < 0 Then
            ReportFrm.DGL2.Item(DR_CR_OP, 0).Value = "Cr"
        End If

        ReportFrm.DGL2.Item(Closing, 0).Value = Format(Math.Abs(ClosingTotal), "0.00")
        If ClosingTotal > 0 Then
            ReportFrm.DGL2.Item(DR_CR_CL, 0).Value = "Dr"
        ElseIf OpeningTotal < 0 Then
            ReportFrm.DGL2.Item(DR_CR_CL, 0).Value = "Cr"
        End If

        If ReportFrm.DGL1.Columns.Contains(LGRCode) Then
            ReportFrm.DGL1.Columns(LGRCode).Visible = False
        End If

        If ReportFrm.DGL1.Columns(GRName).Visible = False Then
            ReportFrm.DGL1.Columns(GRName).Visible = True
            ReportFrm.DGL2.Columns(ReportFrm.DGL1.Columns(GRName).Index).Visible = True
        End If
        If ReportFrm.DGL1.Columns(Opening).Visible = False Then
            ReportFrm.DGL1.Columns(Opening).Visible = True
            ReportFrm.DGL2.Columns(ReportFrm.DGL1.Columns(Opening).Index).Visible = True
        End If
        If ReportFrm.DGL1.Columns(DR_CR_OP).Visible = False Then
            ReportFrm.DGL1.Columns(DR_CR_OP).Visible = True
            ReportFrm.DGL2.Columns(ReportFrm.DGL1.Columns(DR_CR_OP).Index).Visible = True
        End If
        If ReportFrm.DGL1.Columns(Debit).Visible = False Then
            ReportFrm.DGL1.Columns(Debit).Visible = True
            ReportFrm.DGL2.Columns(ReportFrm.DGL1.Columns(Debit).Index).Visible = True
        End If
        If ReportFrm.DGL1.Columns(Credit).Visible = False Then
            ReportFrm.DGL1.Columns(Credit).Visible = True
            ReportFrm.DGL2.Columns(ReportFrm.DGL1.Columns(Credit).Index).Visible = True
        End If
        If ReportFrm.DGL1.Columns(Closing).Visible = False Then
            ReportFrm.DGL1.Columns(Closing).Visible = True
            ReportFrm.DGL2.Columns(ReportFrm.DGL1.Columns(Closing).Index).Visible = True
        End If
        If ReportFrm.DGL1.Columns(DR_CR_CL).Visible = False Then
            ReportFrm.DGL1.Columns(DR_CR_CL).Visible = True
            ReportFrm.DGL2.Columns(ReportFrm.DGL1.Columns(DR_CR_CL).Index).Visible = True
        End If


        ReportFrm.DGL1.ColumnHeadersHeight = 40

        'If ReportFrm.DGL1.Columns.Contains(LGRName) Then
        '    ReportFrm.DGL1.Columns(GRName).Width = 300
        '    ReportFrm.DGL1.Columns(LGRName).Width = 300
        'Else
        ReportFrm.DGL1.Columns(GRName).Width = 380
        'End If
        ReportFrm.DGL1.Columns(Opening).Width = 140
        ReportFrm.DGL1.Columns(DR_CR_OP).Width = 30
        ReportFrm.DGL1.Columns(Debit).Width = 140
        ReportFrm.DGL1.Columns(Credit).Width = 140
        ReportFrm.DGL1.Columns(Closing).Width = 140
        ReportFrm.DGL1.Columns(DR_CR_CL).Width = 30

        ReportFrm.DGL1.Columns(DR_CR_OP).HeaderText = ""
        ReportFrm.DGL1.Columns(DR_CR_CL).HeaderText = ""
    End Sub
    Private Sub FTrailBalance_Disp()
        Dim StrCondition1 As String = ""
        Dim StrSQLQuery As String = ""
        Dim DTTemp As DataTable
        Dim DblDebit_Total As Double, DblCredit_Total As Double
        Dim StrConditionZeroBal As String = ""
        Dim I As Integer

        Dim DtStockValue As DataTable = Nothing
        Dim mExcludeLedgerAccountsFromTrial As String = ""
        Try
            Dim DtSubDetail As DataTable
            If UCase(ReportFrm.FGetText(rowShowWithHierarchy)) = "Yes" Then
                DtSubDetail = FRetDataDisplay_Level_Group().Copy
                DTReport.Rows.Clear()
            End If

            mExcludeLedgerAccountsFromTrial = ClsMain.FGetSettings(SettingFields.ExcludeLedgerAccountsFromTrial, SettingType.General, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
            FGetStockValuesInDataTable(DtStockValue, ReportFrm.FGetText(rowFromDate))

            FCreateDataTable(ReportType.TrialBalance)

            If UCase(ReportFrm.FGetText(rowCostCenter)) = "N" Then StrConditionZeroBal = "Having (Round(IfNull(Sum(LG.AmtDr),0),2)-Round(IfNull(Sum(LG.AmtCr),0),2)) <> 0 "
            StrCondition1 = " Where Date(LG.V_Date) <= " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowToDate)).ToString("s")) & "  "
            StrCondition1 += " And Date(LG.V_Date) >= (Case When Ag.GroupNature in ('R','E') 
                                Then " & AgL.Chk_Date(CDate(AgL.PubStartDate).ToString("s")) & " 
                                Else '1900/01/01' End) "
            If ReportFrm.FGetText(rowIncludeOpening) = "No" Then
                StrCondition1 += " And Date(LG.V_Date) >= " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowFromDate)).ToString("s")) & " "
            End If
            StrCondition1 += Replace(ReportFrm.GetWhereCondition("LG.Site_Code", rowSite), "''", "'")
            StrCondition1 += Replace(ReportFrm.GetWhereCondition("LG.DivCode", rowDivision), "''", "'")

            If mExcludeLedgerAccountsFromTrial <> "" Then
                StrCondition1 += " And IfNull(Sg.Status,'Active') Not In ('" & mExcludeLedgerAccountsFromTrial.Replace("+", "','") & "') "
            End If

            '========== For Detail Section =======

            StrSQLQuery = "Select 'Yes' As IsAccountGroup,	(Case IfNull(AG1.GroupCode,'') When '' Then IfNull(AG.GroupCode,'') "
            StrSQLQuery = StrSQLQuery + "Else IfNull(AG1.GroupCode,'') End)  As GroupCode, "
            StrSQLQuery = StrSQLQuery + "Max(Case IfNull(AG1.GroupName,'') When '' Then IfNull(AG.GroupName,'') "
            StrSQLQuery = StrSQLQuery + "Else IfNull(AG1.GroupName,'') End)  As GName, "

            StrSQLQuery = StrSQLQuery + "(Case When (IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LG.AmtCr),0))>0 Then  "
            StrSQLQuery = StrSQLQuery + "CASE WHEN Max(Ag.GroupName) <> 'Opening Stock' THEN  (IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LG.AmtCr),0)) "
            StrSQLQuery = StrSQLQuery + "When Max(Ag.GroupName) = 'Opening Stock' And "
            If AgL.PubServerName = "" Then
                StrSQLQuery += "Max(julianday(" & AgL.Chk_Date(AgL.PubStartDate) & ") - julianday(LG.V_Date)) = 1 Then "
            Else
                StrSQLQuery += "Max(DateDiff(DAY," & AgL.Chk_Date(AgL.PubStartDate) & ",LG.V_Date)) = 1 Then "
            End If
            StrSQLQuery = StrSQLQuery + "(IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LG.AmtCr),0)) Else 0.00 End Else 0.00 End) As AmtDr, "

            StrSQLQuery = StrSQLQuery + "(Case When (IfNull(Sum(LG.AmtCr),0)-IfNull(Sum(LG.AmtDr),0))>0 Then "
            StrSQLQuery = StrSQLQuery + "CASE WHEN Max(Ag.GroupName) <> 'Opening Stock' THEN  (IfNull(Sum(LG.AmtCr),0)-IfNull(Sum(LG.AmtDr),0)) "
            StrSQLQuery = StrSQLQuery + "When Max(Ag.GroupName) = 'Opening Stock' And "
            If AgL.PubServerName = "" Then
                StrSQLQuery += "Max(julianday(" & AgL.Chk_Date(AgL.PubStartDate) & ") - julianday(LG.V_Date)) = 1 Then "
            Else
                StrSQLQuery += "Max(DateDiff(DAY," & AgL.Chk_Date(AgL.PubStartDate) & ",LG.V_Date)) = 1 Then "
            End If
            StrSQLQuery = StrSQLQuery + "(IfNull(Sum(LG.AmtCr),0)-IfNull(Sum(LG.AmtDr),0)) Else 0.00 End Else 0.00 End) As AmtCr "


            'StrSQLQuery = StrSQLQuery + "(Case When (IfNull(Sum(LG.AmtDr),0.0)-IfNull(Sum(LG.AmtCr),0.0))>0 Then  "
            'StrSQLQuery = StrSQLQuery + "(IfNull(Sum(LG.AmtDr) ,0.0)-IfNull(Sum(LG.AmtCr),0.0)) Else 0.00 End)*1.0 As AmtDr, "
            'StrSQLQuery = StrSQLQuery + "(Case When (IfNull(Sum(LG.AmtCr),0)-IfNull(Sum(LG.AmtDr),0))>0 Then "
            'StrSQLQuery = StrSQLQuery + "(IfNull(Sum(LG.AmtCr),0)-IfNull(Sum(LG.AmtDr),0)) Else 0.00 End)*1.0 As AmtCr "

            StrSQLQuery = StrSQLQuery + "From Ledger LG Left Join SubGroup SG On LG.SubCode=SG.SubCode  Left Join "
            StrSQLQuery = StrSQLQuery + "AcGroup AG On AG.GroupCode=SG.GroupCode Left Join "
            StrSQLQuery = StrSQLQuery + "AcGroupPath AGP On AGP.GroupCode=AG.GroupCode And AGP.SNo=" & IntLevel & " Left Join "
            StrSQLQuery = StrSQLQuery + "AcGroup AG1 On AG1.GroupCode=AGP.GroupUnder "
            StrSQLQuery = StrSQLQuery + StrCondition1

            StrSQLQuery = StrSQLQuery + "Group By (Case IfNull(AG1.GroupCode,'') When '' Then IfNull(AG.GroupCode,'') "
            StrSQLQuery = StrSQLQuery + "Else IfNull(AG1.GroupCode,'') End) "
            StrSQLQuery = StrSQLQuery + StrConditionZeroBal
            StrSQLQuery = StrSQLQuery + "Order By Max(Case IfNull(AG1.GroupName,'') When '' Then IfNull(AG.GroupName,'') "
            StrSQLQuery = StrSQLQuery + "Else IfNull(AG1.GroupName,'') End) "

            DTTemp = AgL.FillData(StrSQLQuery, AgL.GCn).tables(0)

            DblDebit_Total = 0
            DblCredit_Total = 0
            For I = 0 To DTTemp.Rows.Count - 1
                DTReport.Rows.Add()
                'DTReport.Rows(I)(GR_SG) = "A"
                DTReport.Rows(I)(IsAccountGroup) = AgL.XNull(DTTemp.Rows(I).Item("IsAccountGroup"))
                DTReport.Rows(I)(GRCode) = AgL.XNull(DTTemp.Rows(I).Item("GroupCode"))
                DTReport.Rows(I)(GRName) = AgL.XNull(DTTemp.Rows(I).Item("GName"))
                DTReport.Rows(I)(Debit) = IIf(AgL.VNull(DTTemp.Rows(I).Item("AmtDr")) > 0, Format(AgL.VNull(DTTemp.Rows(I).Item("AmtDr")), "0.00"), System.DBNull.Value)
                DTReport.Rows(I)(Credit) = IIf(AgL.VNull(DTTemp.Rows(I).Item("AmtCr")) > 0, Format(AgL.VNull(DTTemp.Rows(I).Item("AmtCr")), "0.00"), System.DBNull.Value)
                DblDebit_Total = DblDebit_Total + Format(AgL.VNull(DTTemp.Rows(I).Item("AmtDr")), "0.00")
                DblCredit_Total = DblCredit_Total + Format(AgL.VNull(DTTemp.Rows(I).Item("AmtCr")), "0.00")
            Next

            For J As Integer = 0 To DtStockValue.Rows.Count - 1
                If AgL.VNull(DtStockValue.Rows(J)("OpeningStockValue")) <> 0 Then
                    DTReport.Rows.Add()
                    DTReport.Rows(I)(IsAccountGroup) = "Yes"
                    DTReport.Rows(I)(GRName) = "Opening Stock" + IIf(AgL.XNull(DtStockValue.Rows(J)("Remark")) <> "", " " + AgL.XNull(DtStockValue.Rows(J)("Remark")), "")
                    DTReport.Rows(I)(Debit) = DtStockValue.Rows(J)("OpeningStockValue")
                    DblDebit_Total = DblDebit_Total + Format(AgL.VNull(DTReport.Rows(I)(Debit)), "0.00")
                    DblCredit_Total = DblCredit_Total + Format(AgL.VNull(DTReport.Rows(I)(Credit)), "0.00")
                    I = I + 1
                End If
            Next

            'If mOpeningStockValue > 0 Then
            '    DTReport.Rows.Add()
            '    DTReport.Rows(I)(IsAccountGroup) = "Yes"
            '    DTReport.Rows(I)(GRName) = "Opening Stock"
            '    DTReport.Rows(I)(Debit) = mOpeningStockValue
            '    DblDebit_Total = DblDebit_Total + Format(AgL.VNull(DTReport.Rows(I)(Debit)), "0.00")
            '    DblCredit_Total = DblCredit_Total + Format(AgL.VNull(DTReport.Rows(I)(Credit)), "0.00")
            '    I = I + 1
            'End If



            DTReport.Rows.Add()

            If (DblDebit_Total - DblCredit_Total) > 0 Then
                DTReport.Rows(I)(IsAccountGroup) = "Yes"
                DTReport.Rows(I)(GRName) = "Difference In Trial Balance"
                DTReport.Rows(I)(Credit) = Format((DblDebit_Total - DblCredit_Total), "0.00")
                DblCredit_Total = DblCredit_Total + Format((DblDebit_Total - DblCredit_Total), "0.00")
            ElseIf (DblCredit_Total - DblDebit_Total) > 0 Then
                DTReport.Rows(I)(IsAccountGroup) = "Yes"
                DTReport.Rows(I)(GRName) = "Difference In Trial Balance"
                DTReport.Rows(I)(Debit) = Format((DblCredit_Total - DblDebit_Total), "0.00")
                DblDebit_Total = DblDebit_Total + Format((DblCredit_Total - DblDebit_Total), "0.00")
            End If

            If DTReport.Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")


            If UCase(ReportFrm.FGetText(rowShowWithHierarchy)) = "Yes" Then
                Dim DtGroupHeader As DataTable = DTReport.Copy()
                Dim POS As Integer = 0
                For I = 0 To DtGroupHeader.Rows.Count - 1
                    Dim DrGroupDetail As DataRow() = DtSubDetail.Select("[" + AcGroupCode + "] = '" + DtGroupHeader.Rows(I)(GRCode) + "'")
                    For J As Integer = 0 To DrGroupDetail.Length - 1
                        Dim DrNewRow As DataRow = DTReport.NewRow
                        DrNewRow(GRCode) = DrGroupDetail(J)(GRCode)
                        DrNewRow(GRName) = "        " + DrGroupDetail(J)(GRName)
                        DrNewRow(Debit) = DrGroupDetail(J)(Debit)
                        DrNewRow(Credit) = DrGroupDetail(J)(Credit)

                        For M As Integer = 0 To DTReport.Rows.Count - 1
                            If AgL.XNull(DtGroupHeader.Rows(I)(GRCode)) = AgL.XNull(DTReport.Rows(M)(GRCode)) Then
                                POS = M + J + 1
                                Exit For
                            End If
                        Next

                        DTReport.Rows.InsertAt(DrNewRow, POS)
                        DTReport.AcceptChanges()
                    Next
                Next
            End If





            'ReportFrm.Text = "Financial Display - " + ReportFrm.FGetText(rowReportType)
            ReportFrm.Text = ReportFrm.FGetText(rowReportType)
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = "ProcFinancialDisplay"
            ReportFrm.IsHideZeroColumns = False
            ReportFrm.IsManualAggregate = False

            DsReport = New DataSet()
            DsReport.Tables.Add(DTReport)
            ReportFrm.ProcFillGrid(DsReport)

            FormatTrialBalance(FormattingOn.OnInit)

            If UCase(ReportFrm.FGetText(rowShowWithHierarchy)) = "Yes" Then
                For I = 0 To ReportFrm.DGL1.Columns.Count - 1
                    ReportFrm.DGL1.Columns(I).SortMode = DataGridViewColumnSortMode.NotSortable
                Next I
                ReportFrm.IsManualAggregate = True
                ReportFrm.DGL2.Item(ReportFrm.DGL1.Columns(Debit).Index, 0).Value = DblDebit_Total
                ReportFrm.DGL2.Item(ReportFrm.DGL1.Columns(Credit).Index, 0).Value = DblCredit_Total
            End If


            ReportFrm.DGL1.Columns(GRName).Width = 500
            ReportFrm.DGL1.Columns(Debit).Width = 300
            ReportFrm.DGL1.Columns(Credit).Width = 300

            ReportFrm.DGL1.Columns(AcGroupCode).Visible = False
            ReportFrm.DGL1.Columns(IsAccountGroup).Visible = False

            'ReportFrm.DGL1.Columns(Opening).Width = 100
            'ReportFrm.DGL1.Columns(DR_CR_OP).Width = 20
            'ReportFrm.DGL1.Columns(DR_CR_OP).HeaderText = ""
            'ReportFrm.DGL1.Columns(Closing).Width = 100
            'ReportFrm.DGL1.Columns(DR_CR_CL).Width = 20
            'ReportFrm.DGL1.Columns(DR_CR_CL).HeaderText = ""
        Catch ex As Exception
            If Not ex.Message.Contains("Index was out of range") Then
                MsgBox(ex.Message)
            End If
        End Try
    End Sub
    Private Sub FBalanceSheet_Disp()
        Dim StrCondition1 As String = ""
        Dim StrSQLQuery As String = ""
        Dim DTTemp As DataTable
        Dim DblDebit_Total As Double, DblCredit_Total As Double
        Dim I As Integer, J As Integer, DblNet_Profit_Loss As Double = 0
        Dim DtStockValue As DataTable = Nothing

        Try
            FGetStockValuesInDataTable(DtStockValue, ReportFrm.FGetText(rowFromDate))

            'mOpeningStockValue = 0
            'mClosingStockValue = 0

            Dim DtSubDetail As DataTable
            If UCase(ReportFrm.FGetText(rowShowWithHierarchy)) = "Yes" Then
                DtSubDetail = FRetDataDisplay_Level_Group().Copy
                DTReport.Rows.Clear()
            End If

            FCreateDataTable(ReportType.BalanceSheet)

            StrCondition1 = " Where Date(LG.V_Date) <= " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowToDate)).ToString("s")) & ""
            StrCondition1 += Replace(ReportFrm.GetWhereCondition("LG.Site_Code", rowSite), "''", "'")
            StrCondition1 += Replace(ReportFrm.GetWhereCondition("LG.DivCode", rowDivision), "''", "'")


            '========== For Detail Section =======

            StrSQLQuery = "Select	'Yes' As IsAccountGroup, (Case IfNull(AG1.GroupCode,'') When '' Then IfNull(AG.GroupCode,'') "
            StrSQLQuery = StrSQLQuery + "Else IfNull(AG1.GroupCode,'') End)  As GroupCode, "
            StrSQLQuery = StrSQLQuery + "Max(Case IfNull(AG1.GroupName,'') When '' Then IfNull(AG.GroupName,'') "
            StrSQLQuery = StrSQLQuery + "Else IfNull(AG1.GroupName,'') End)  As GName, "
            StrSQLQuery = StrSQLQuery + "(Case When (IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LG.AmtCr),0))>0 Then  "
            StrSQLQuery = StrSQLQuery + "(IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LG.AmtCr),0)) Else 0.00 End) As AmtDr, "
            StrSQLQuery = StrSQLQuery + "(Case When (IfNull(Sum(LG.AmtCr),0)-IfNull(Sum(LG.AmtDr),0))>0 Then "
            StrSQLQuery = StrSQLQuery + "(IfNull(Sum(LG.AmtCr),0)-IfNull(Sum(LG.AmtDr),0)) Else 0.00 End) As AmtCr, "
            StrSQLQuery = StrSQLQuery + "Max(Case IfNull(AG1.GroupCode,'') When '' Then IfNull(AG.ContraGroupName,'') Else IfNull(AG1.ContraGroupName,'') End)  "
            StrSQLQuery = StrSQLQuery + "As ContraGroupName, "
            StrSQLQuery = StrSQLQuery + "Max(Case IfNull(AG1.GroupCode,'') When '' Then IfNull(AG.GroupNature,'') Else IfNull(AG1.GroupNature,'') End)   "
            StrSQLQuery = StrSQLQuery + "As GroupNature "

            StrSQLQuery = StrSQLQuery + "From Ledger LG Left Join SubGroup SG On LG.SubCode=SG.SubCode  Left Join "
            StrSQLQuery = StrSQLQuery + "AcGroup AG On AG.GroupCode=SG.GroupCode Left Join "
            StrSQLQuery = StrSQLQuery + "AcGroupPath AGP On AGP.GroupCode=AG.GroupCode And AGP.SNo=" & IntLevel & " Left Join "
            StrSQLQuery = StrSQLQuery + "AcGroup AG1 On AG1.GroupCode=AGP.GroupUnder "
            StrSQLQuery = StrSQLQuery + StrCondition1
            StrSQLQuery = StrSQLQuery + "And AG.GroupNature In ('A','L') "

            StrSQLQuery = StrSQLQuery + "Group By (Case IfNull(AG1.GroupCode,'') When '' Then IfNull(AG.GroupCode,'') "
            StrSQLQuery = StrSQLQuery + "Else IfNull(AG1.GroupCode,'') End) "
            StrSQLQuery = StrSQLQuery + "Having (IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LG.AmtCr),0)) <> 0 "
            StrSQLQuery = StrSQLQuery + "Order By Max(Case IfNull(AG1.GroupName,'') When '' Then IfNull(AG.GroupName,'') "
            StrSQLQuery = StrSQLQuery + "Else IfNull(AG1.GroupName,'') End) "

            DTTemp = AgL.FillData(StrSQLQuery, AgL.GCn).tables(0)

            If DTTemp.Rows.Count > 0 Then
                DTReport.Rows.Add(DTTemp.Rows.Count + 2)
            End If
            DblDebit_Total = 0
            DblCredit_Total = 0

            'If AgL.VNull(mOpeningStockValue) > 0 Then
            '    J = FFindEmptyRow(DTReport, GRName)
            '    DTReport.Rows(J)(GRName) = "Opening Stock"
            '    DTReport.Rows(J)(Debit) = AgL.VNull(mOpeningStockValue)
            '    DblDebit_Total = DblDebit_Total + AgL.VNull(mOpeningStockValue)
            'End If

            For I = 0 To DTTemp.Rows.Count - 1
                If AgL.VNull(DTTemp.Rows(I).Item("AmtDr")) > 0 Then
                    J = FFindEmptyRow(DTReport, GRNameCredit)
                    DTReport.Rows(J)(IsAccountGroupCredit) = AgL.XNull(DTTemp.Rows(I).Item("IsAccountGroup"))
                    DTReport.Rows(J)(GRCodeCredit) = AgL.XNull(DTTemp.Rows(I).Item("GroupCode"))
                    If UCase(AgL.XNull(DTTemp.Rows(I).Item("GroupNature"))) = "A" Then
                        DTReport.Rows(J)(GRNameCredit) = AgL.XNull(DTTemp.Rows(I).Item("GName"))
                    Else
                        DTReport.Rows(J)(GRNameCredit) = AgL.XNull(DTTemp.Rows(I).Item("ContraGroupName"))
                    End If
                    DTReport.Rows(J)(Credit) = Format(AgL.VNull(DTTemp.Rows(I).Item("AmtDr")), "0.00")
                    DblCredit_Total = DblCredit_Total + Val(DTReport.Rows(J)(Credit))
                ElseIf AgL.VNull(DTTemp.Rows(I).Item("AmtCr")) > 0 Then
                    J = FFindEmptyRow(DTReport, GRName)
                    DTReport.Rows(J)(IsAccountGroup) = AgL.XNull(DTTemp.Rows(I).Item("IsAccountGroup"))
                    DTReport.Rows(J)(GRCode) = AgL.XNull(DTTemp.Rows(I).Item("GroupCode"))
                    If UCase(AgL.XNull(DTTemp.Rows(I).Item("GroupNature"))) = "L" Then
                        DTReport.Rows(J)(GRName) = AgL.XNull(DTTemp.Rows(I).Item("GName"))
                    Else
                        DTReport.Rows(J)(GRName) = AgL.XNull(DTTemp.Rows(I).Item("ContraGroupName"))
                    End If
                    DTReport.Rows(J)(Debit) = Format(AgL.VNull(DTTemp.Rows(I).Item("AmtCr")), "0.00")
                    DblDebit_Total = DblDebit_Total + Val(DTReport.Rows(J)(Debit))
                End If
                'DTReport.Rows(J)(GR_SG) = "A"
            Next
            DTTemp.Clear()
            DTTemp.Dispose()

            For K As Integer = 0 To DtStockValue.Rows.Count - 1
                If AgL.VNull(DtStockValue.Rows(K)("ClosingStockValue")) <> 0 Then
                    J = FFindEmptyRow(DTReport, GRNameCredit)
                    DTReport.Rows(J)(GRNameCredit) = "Closing Stock" + IIf(AgL.XNull(DtStockValue.Rows(K)("Remark")) <> "", " " + AgL.XNull(DtStockValue.Rows(K)("Remark")), "")
                    DTReport.Rows(J)(Credit) = AgL.VNull(DtStockValue.Rows(K)("ClosingStockValue"))
                    DblCredit_Total = DblCredit_Total + AgL.VNull(DtStockValue.Rows(K)("ClosingStockValue"))
                End If
            Next


            'If AgL.VNull(mClosingStockValue) > 0 Then
            '    J = FFindEmptyRow(DTReport, GRNameCredit)
            '    DTReport.Rows(J)(GRNameCredit) = "Closing Stock"
            '    DTReport.Rows(J)(Credit) = AgL.VNull(mClosingStockValue)
            '    DblCredit_Total = DblCredit_Total + AgL.VNull(mClosingStockValue)
            'End If

            DTTemp = FGetTRDDataTable()

            For I = 0 To DTTemp.Rows.Count - 1
                If AgL.VNull(DTTemp.Rows(I).Item("AmtDr")) > 0 Then
                    DblNet_Profit_Loss = DblNet_Profit_Loss - Format(AgL.VNull(DTTemp.Rows(I).Item("AmtDr")), "0.00")
                ElseIf AgL.VNull(DTTemp.Rows(I).Item("AmtCr")) > 0 Then
                    DblNet_Profit_Loss = DblNet_Profit_Loss + Format(AgL.VNull(DTTemp.Rows(I).Item("AmtCr")), "0.00")
                End If
            Next
            DTTemp.Clear()
            DTTemp.Dispose()
            DTTemp = FGetPLDataTable()

            For K As Integer = 0 To DtStockValue.Rows.Count - 1
                If AgL.VNull(DtStockValue.Rows(K)("OpeningStockValue")) > 0 Then DblNet_Profit_Loss = DblNet_Profit_Loss - AgL.VNull(DtStockValue.Rows(K)("OpeningStockValue"))
                If AgL.VNull(DtStockValue.Rows(K)("ClosingStockValue")) > 0 Then DblNet_Profit_Loss = DblNet_Profit_Loss + AgL.VNull(DtStockValue.Rows(K)("ClosingStockValue"))
            Next
            'If AgL.VNull(mOpeningStockValue) > 0 Then DblNet_Profit_Loss = DblNet_Profit_Loss - AgL.VNull(mOpeningStockValue)
            'If AgL.VNull(mClosingStockValue) > 0 Then DblNet_Profit_Loss = DblNet_Profit_Loss + AgL.VNull(mClosingStockValue)

            For I = 0 To DTTemp.Rows.Count - 1
                If AgL.VNull(DTTemp.Rows(I).Item("AmtDr")) > 0 Then
                    DblNet_Profit_Loss = DblNet_Profit_Loss - Format(AgL.VNull(DTTemp.Rows(I).Item("AmtDr")), "0.00")
                ElseIf AgL.VNull(DTTemp.Rows(I).Item("AmtCr")) > 0 Then
                    DblNet_Profit_Loss = DblNet_Profit_Loss + Format(AgL.VNull(DTTemp.Rows(I).Item("AmtCr")), "0.00")
                End If
            Next

            If DblNet_Profit_Loss < 0 Then
                J = FFindEmptyRow(DTReport, GRNameCredit)
                If J < FFindEmptyRow(DTReport, GRName) Then J = FFindEmptyRow(DTReport, GRName)

                DTReport.Rows(J)(GRNameCredit) = "Net Loss"
                DTReport.Rows(J)(Credit) = Format(Math.Abs(DblNet_Profit_Loss), "0.00")
                DblCredit_Total = DblCredit_Total + Format(Math.Abs(DblNet_Profit_Loss), "0.00")
                'DTReport.Rows(J)(GR_SG) = CnsProfitAndLoss
            ElseIf DblNet_Profit_Loss > 0 Then
                J = FFindEmptyRow(DTReport, GRName)
                If J < FFindEmptyRow(DTReport, GRNameCredit) Then J = FFindEmptyRow(DTReport, GRNameCredit)
                DTReport.Rows(J)(GRName) = "Net Profit"
                DTReport.Rows(J)(Debit) = Format(Math.Abs(DblNet_Profit_Loss), "0.00")
                DblDebit_Total = DblDebit_Total + Format(Math.Abs(DblNet_Profit_Loss), "0.00")
                'DTReport.Rows(J)(GR_SG) = CnsProfitAndLoss
            End If

            If (DblDebit_Total - DblCredit_Total) > 0.001 Then
                J = FFindEmptyRow(DTReport, GRNameCredit)
                DTReport.Rows(J)(GRNameCredit) = "Difference In Trial Balance"
                DTReport.Rows(J)(Credit) = Format((DblDebit_Total - DblCredit_Total), "0.00")
                DblCredit_Total = DblCredit_Total + Format((DblDebit_Total - DblCredit_Total), "0.00")
            ElseIf (DblCredit_Total - DblDebit_Total) > 0.001 Then
                J = FFindEmptyRow(DTReport, GRName)
                DTReport.Rows(J)(GRName) = "Difference In Trial Balance"
                DTReport.Rows(J)(Debit) = Format((DblCredit_Total - DblDebit_Total), "0.00")
                DblDebit_Total = DblDebit_Total + Format((DblCredit_Total - DblDebit_Total), "0.00")
            End If

            'DTReport.Rows(DTReport.Rows.Count - 1)(Debit) = Format(DblDebit_Total, "0.00")
            'DTReport.Rows(DTReport.Rows.Count - 1)(Credit) = Format(DblCredit_Total, "0.00")

            If DTReport.Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")


            If UCase(ReportFrm.FGetText(rowShowWithHierarchy)) = "Yes" Then
                Dim DtGroupHeader As DataTable = DTReport.Copy()
                DTReport.Rows.Clear()

                For I = 0 To DtGroupHeader.Rows.Count - 1
                    DTReport.Rows.Add()
                    DTReport.Rows(DTReport.Rows.Count - 1)(GRCode) = DtGroupHeader.Rows(I)(GRCode)
                    DTReport.Rows(DTReport.Rows.Count - 1)(GRName) = DtGroupHeader.Rows(I)(GRName)
                    DTReport.Rows(DTReport.Rows.Count - 1)(Debit) = DtGroupHeader.Rows(I)(Debit)
                    DTReport.Rows(DTReport.Rows.Count - 1)(IsAccountGroup) = DtGroupHeader.Rows(I)(IsAccountGroup)

                    Dim DrGroupDetail As DataRow() = DtSubDetail.Select("[" + AcGroupCode + "] = '" + DtGroupHeader.Rows(I)(GRCode) + "'")
                    For J = 0 To DrGroupDetail.Length - 1
                        DTReport.Rows.Add()
                        DTReport.Rows(DTReport.Rows.Count - 1)(GRCode) = DrGroupDetail(J)(GRCode)
                        DTReport.Rows(DTReport.Rows.Count - 1)(GRName) = "        " + DrGroupDetail(J)(GRName)
                        If AgL.VNull(DrGroupDetail(J)(Debit)) > 0 Then
                            DTReport.Rows(DTReport.Rows.Count - 1)(Debit) = -DrGroupDetail(J)(Debit)
                        ElseIf AgL.VNull(DrGroupDetail(J)(Credit)) > 0 Then
                            DTReport.Rows(DTReport.Rows.Count - 1)(Debit) = DrGroupDetail(J)(Credit)
                        End If
                    Next
                Next

                For I = 0 To DtGroupHeader.Rows.Count - 1
                    Dim bRowIndex As Integer = FindNextRowForBalanceSheerHirerichy()

                    DTReport.Rows(bRowIndex)(GRCodeCredit) = DtGroupHeader.Rows(I)(GRCodeCredit)
                    DTReport.Rows(bRowIndex)(GRNameCredit) = DtGroupHeader.Rows(I)(GRNameCredit)
                    DTReport.Rows(bRowIndex)(Credit) = DtGroupHeader.Rows(I)(Credit)
                    DTReport.Rows(bRowIndex)(IsAccountGroupCredit) = DtGroupHeader.Rows(I)(IsAccountGroupCredit)

                    Dim DrGroupDetail As DataRow() = DtSubDetail.Select("[" + AcGroupCode + "] = '" + DtGroupHeader.Rows(I)(GRCodeCredit) + "'")
                    For J = 0 To DrGroupDetail.Length - 1
                        bRowIndex = FindNextRowForBalanceSheerHirerichy()
                        DTReport.Rows(bRowIndex)(GRCodeCredit) = DrGroupDetail(J)(GRCode)
                        DTReport.Rows(bRowIndex)(GRNameCredit) = "        " + DrGroupDetail(J)(GRName)
                        If AgL.VNull(DrGroupDetail(J)(Credit)) > 0 Then
                            DTReport.Rows(bRowIndex)(Credit) = -AgL.VNull(DrGroupDetail(J)(Credit))
                        ElseIf AgL.VNull(DrGroupDetail(J)(Debit)) > 0 Then
                            DTReport.Rows(bRowIndex)(Credit) = AgL.VNull(DrGroupDetail(J)(Debit))
                        End If
                    Next
                Next

                For I = 0 To DTReport.Rows.Count - 1
                    If AgL.StrCmp(AgL.XNull(DTReport.Rows(I)(GRName)), "Net Profit") Or
                        AgL.StrCmp(AgL.XNull(DTReport.Rows(I)(GRName)), "Net Loss") Then
                        DTReport.Rows.Add()
                        DTReport.Rows(DTReport.Rows.Count - 1)(GRName) = DTReport.Rows(I)(GRName)
                        DTReport.Rows(DTReport.Rows.Count - 1)(Debit) = AgL.VNull(DTReport.Rows(I)(Debit))
                        DTReport.Rows(I)(GRName) = System.DBNull.Value
                        DTReport.Rows(I)(Debit) = System.DBNull.Value
                    End If

                    If AgL.StrCmp(AgL.XNull(DTReport.Rows(I)(GRNameCredit)), "Net Profit") Or
                            AgL.StrCmp(AgL.XNull(DTReport.Rows(I)(GRNameCredit)), "Net Loss") Then
                        DTReport.Rows.Add()
                        DTReport.Rows(DTReport.Rows.Count - 1)(GRNameCredit) = DTReport.Rows(I)(GRNameCredit)
                        DTReport.Rows(DTReport.Rows.Count - 1)(Credit) = AgL.VNull(DTReport.Rows(I)(Credit))
                        DTReport.Rows(I)(GRNameCredit) = System.DBNull.Value
                        DTReport.Rows(I)(Credit) = System.DBNull.Value
                    End If
                Next
            End If


            'If UCase(ReportFrm.FGetText(rowShowWithHierarchy)) = "Yes" Then
            '    Dim DtGroupHeader As DataTable = DTReport.Copy()
            '    Dim POS As Integer = 0
            '    For I = 0 To DtGroupHeader.Rows.Count - 1
            '        Dim DrGroupDetail As DataRow() = DtSubDetail.Select("[" + AcGroupCode + "] = '" + DtGroupHeader.Rows(I)(GRCode) + "'")
            '        For J = 0 To DrGroupDetail.Length - 1
            '            Dim DrNewRow As DataRow = DTReport.NewRow
            '            DrNewRow(GRCode) = DrGroupDetail(J)(GRCode)
            '            DrNewRow(GRName) = "        " + DrGroupDetail(J)(GRName)
            '            If AgL.VNull(DrGroupDetail(J)(Debit)) > 0 Then
            '                DrNewRow(Debit) = DrGroupDetail(J)(Debit)
            '            ElseIf AgL.VNull(DrGroupDetail(J)(Credit)) > 0 Then
            '                DrNewRow(Debit) = -DrGroupDetail(J)(Credit)
            '            End If
            '            For M As Integer = 0 To DTReport.Rows.Count - 1
            '                If AgL.XNull(DtGroupHeader.Rows(I)(GRCode)) = AgL.XNull(DTReport.Rows(M)(GRCode)) Then
            '                    POS = M + J + 1
            '                    Exit For
            '                End If
            '            Next
            '            DTReport.Rows.InsertAt(DrNewRow, POS)
            '            DTReport.AcceptChanges()
            '        Next
            '    Next







            '    For I = 0 To DtGroupHeader.Rows.Count - 1
            '        Dim DrGroupDetail As DataRow() = DtSubDetail.Select("[" + AcGroupCode + "] = '" + DtGroupHeader.Rows(I)(GRCodeCredit) + "'")
            '        For J = 0 To DrGroupDetail.Length - 1

            '            Dim bAcGroupCodeRowIndex As Integer = 0
            '            For K As Integer = 0 To DTReport.Rows.Count - 1
            '                If AgL.XNull(DTReport.Rows(K)(GRCodeCredit)) = AgL.XNull(DtGroupHeader.Rows(I)(GRCodeCredit)) Then
            '                    bAcGroupCodeRowIndex = K
            '                    Exit For
            '                End If
            '            Next


            '            Dim DrNewRow As DataRow
            '            Dim bIsNewRow As Boolean = False
            '            For K As Integer = bAcGroupCodeRowIndex To DTReport.Rows.Count - 1
            '                If AgL.XNull(DTReport.Rows(K)(GRCodeCredit)) = "" Then
            '                    DrNewRow = DTReport.Rows(K)
            '                    Exit For
            '                End If
            '            Next

            '            If DrNewRow Is Nothing Then
            '                DrNewRow = DTReport.NewRow
            '                bIsNewRow = True
            '            End If

            '            DrNewRow(GRCodeCredit) = DrGroupDetail(J)(GRCode)
            '            DrNewRow(GRNameCredit) = "        " + DrGroupDetail(J)(GRName)
            '            If AgL.VNull(DrGroupDetail(J)(Debit)) > 0 Then
            '                DrNewRow(Credit) = DrGroupDetail(J)(Debit)
            '            ElseIf AgL.VNull(DrGroupDetail(J)(Credit)) > 0 Then
            '                DrNewRow(Credit) = -DrGroupDetail(J)(Credit)
            '            End If

            '            For M As Integer = 0 To DTReport.Rows.Count - 1
            '                If AgL.XNull(DtGroupHeader.Rows(I)(GRCodeCredit)) = AgL.XNull(DTReport.Rows(M)(GRCodeCredit)) Then
            '                    POS = M + J + 1
            '                    Exit For
            '                End If
            '            Next
            '            If bIsNewRow = True Then
            '                DTReport.Rows.InsertAt(DrNewRow, POS)
            '                DTReport.AcceptChanges()
            '            End If
            '        Next
            '    Next
            'End If



            'ReportFrm.Text = "Financial Display - " + ReportFrm.FGetText(rowReportType)
            ReportFrm.Text = ReportFrm.FGetText(rowReportType)
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = "ProcFinancialDisplay"
            ReportFrm.IsHideZeroColumns = False
            ReportFrm.IsManualAggregate = False

            DsReport = New DataSet()
            DsReport.Tables.Add(DTReport)
            ReportFrm.ProcFillGrid(DsReport)

            ReportFrm.DGL1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None

            For I = 0 To ReportFrm.DGL1.Columns.Count - 1
                ReportFrm.DGL1.Columns(I).SortMode = DataGridViewColumnSortMode.NotSortable
            Next I

            If UCase(ReportFrm.FGetText(rowShowWithHierarchy)) = "Yes" Then
                For I = 0 To ReportFrm.DGL1.Columns.Count - 1
                    ReportFrm.DGL1.Columns(I).SortMode = DataGridViewColumnSortMode.NotSortable
                Next I
                ReportFrm.IsManualAggregate = True
                ReportFrm.DGL2.Item(ReportFrm.DGL1.Columns(Debit).Index, 0).Value = DblDebit_Total
                ReportFrm.DGL2.Item(ReportFrm.DGL1.Columns(Credit).Index, 0).Value = DblCredit_Total
            End If

            ReportFrm.DGL1.Columns(GRCodeCredit).Visible = False
            ReportFrm.DGL2.Columns(ReportFrm.DGL1.Columns(GRCodeCredit).Index).Visible = False

            ReportFrm.DGL1.Columns(IsAccountGroup).Visible = False
            ReportFrm.DGL1.Columns(IsAccountGroupCredit).Visible = False

            ReportFrm.DGL1.Columns(GRName).HeaderText = "Liabilities"
            ReportFrm.DGL1.Columns(GRNameCredit).HeaderText = "Assets"
            ReportFrm.DGL1.Columns(Debit).HeaderText = "Amount"
            ReportFrm.DGL1.Columns(Credit).HeaderText = "Amount"

            ReportFrm.DGL1.Columns(GRName).Width = 440
            ReportFrm.DGL1.Columns(GRNameCredit).Width = 440
            ReportFrm.DGL1.Columns(Debit).Width = 150
            ReportFrm.DGL1.Columns(Credit).Width = 150
        Catch ex As Exception
            If Not ex.Message.Contains("Index was out of range") Then
                MsgBox(ex.Message)
            End If
        End Try
    End Sub
    Private Function FFindEmptyRow(DTReport As DataTable, ByVal ColumnName As String, Optional ByVal IntFindFrom As Integer = 0) As Integer
        Dim I As Integer, BlnFlag As Boolean

        BlnFlag = True
        For I = IntFindFrom To DTReport.Rows.Count - 1
            If AgL.XNull(DTReport.Rows(I)(ColumnName)) = "" Then
                BlnFlag = False
                Exit For
            End If
        Next

        If BlnFlag Then
            DTReport.Rows.Add(1)
            I = DTReport.Rows.Count - 1
        End If
        FFindEmptyRow = I
    End Function
    Private Function FGetTRDDataTable() As DataTable
        Dim StrCondition1 As String = ""
        Dim StrSQLQuery As String = ""
        Dim DTTemp As DataTable

        Try
            StrCondition1 = " Where Date(LG.V_Date) <= " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowToDate)).ToString("s")) & " "
            StrCondition1 += Replace(ReportFrm.GetWhereCondition("LG.Site_Code", rowSite), "''", "'")
            StrCondition1 += Replace(ReportFrm.GetWhereCondition("LG.DivCode", rowDivision), "''", "'")


            StrCondition1 += " And Date(LG.V_Date) >= (Case When Ag.GroupNature in ('R','E') Then " & AgL.Chk_Date(CDate(AgL.PubStartDate).ToString("s")) & " Else '1900/01/01' End) "

            '========== For Detail Section =======
            StrSQLQuery = "Select	'Yes' As IsAccountGroup, (Case IfNull(AG1.GroupCode,'') When '' Then IfNull(AG.GroupCode,'') "
            StrSQLQuery = StrSQLQuery + "Else IfNull(AG1.GroupCode,'') End)  As GroupCode, "
            StrSQLQuery = StrSQLQuery + "Max(Case IfNull(AG1.GroupName,'') When '' Then IfNull(AG.GroupName,'') "
            StrSQLQuery = StrSQLQuery + "Else IfNull(AG1.GroupName,'') End)  As GName, "

            StrSQLQuery = StrSQLQuery + "(Case When (IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LG.AmtCr),0))>0 Then  "
            StrSQLQuery = StrSQLQuery + "CASE WHEN Max(Ag.GroupName) <> 'Opening Stock' THEN  (IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LG.AmtCr),0)) "
            StrSQLQuery = StrSQLQuery + "When Max(Ag.GroupName) = 'Opening Stock' And "
            If AgL.PubServerName = "" Then
                StrSQLQuery += "Max(julianday(" & AgL.Chk_Date(AgL.PubStartDate) & ") - julianday(LG.V_Date)) = 1 Then "
            Else
                StrSQLQuery += "Max(DateDiff(DAY," & AgL.Chk_Date(AgL.PubStartDate) & ",LG.V_Date)) = 1 Then "
            End If
            StrSQLQuery = StrSQLQuery + "(IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LG.AmtCr),0)) Else 0.00 End Else 0.00 End) As AmtDr, "

            StrSQLQuery = StrSQLQuery + "(Case When (IfNull(Sum(LG.AmtCr),0)-IfNull(Sum(LG.AmtDr),0))>0 Then "
            StrSQLQuery = StrSQLQuery + "CASE WHEN Max(Ag.GroupName) <> 'Opening Stock' THEN  (IfNull(Sum(LG.AmtCr),0)-IfNull(Sum(LG.AmtDr),0)) "
            StrSQLQuery = StrSQLQuery + "When Max(Ag.GroupName) = 'Opening Stock' And "
            If AgL.PubServerName = "" Then
                StrSQLQuery += "Max(julianday(" & AgL.Chk_Date(AgL.PubStartDate) & ") - julianday(LG.V_Date)) = 1 Then "
            Else
                StrSQLQuery += "Max(DateDiff(DAY," & AgL.Chk_Date(AgL.PubStartDate) & ",LG.V_Date)) = 1 Then "
            End If
            StrSQLQuery = StrSQLQuery + "(IfNull(Sum(LG.AmtCr),0)-IfNull(Sum(LG.AmtDr),0)) Else 0.00 End Else 0.00 End) As AmtCr, "

            StrSQLQuery = StrSQLQuery + "Max(AG.ContraGroupName) As ContraGroupName,Max(AG.GroupNature) As GroupNature "
            StrSQLQuery = StrSQLQuery + "From Ledger LG Left Join SubGroup SG On LG.SubCode=SG.SubCode  Left Join "
            StrSQLQuery = StrSQLQuery + "AcGroup AG On AG.GroupCode=SG.GroupCode Left Join "
            StrSQLQuery = StrSQLQuery + "AcGroupPath AGP On AGP.GroupCode=AG.GroupCode And AGP.SNo=" & IntLevel & " Left Join "
            StrSQLQuery = StrSQLQuery + "AcGroup AG1 On AG1.GroupCode=AGP.GroupUnder "
            StrSQLQuery = StrSQLQuery + StrCondition1
            StrSQLQuery = StrSQLQuery + "And AG.GroupNature In ('R','E','O') "

            '=================== For Only PL Data =====================
            StrSQLQuery = StrSQLQuery + "And (AG.Nature In ('Direct','Purchase','Sales') Or "
            StrSQLQuery = StrSQLQuery + "AG1.Nature In ('Direct','Purchase','Sales')) "
            '==========================================================

            StrSQLQuery = StrSQLQuery + "Group By (Case IfNull(AG1.GroupCode,'') When '' Then IfNull(AG.GroupCode,'') "
            StrSQLQuery = StrSQLQuery + "Else IfNull(AG1.GroupCode,'') End) "
            StrSQLQuery = StrSQLQuery + "Having (IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LG.AmtCr),0)) <> 0 "
            StrSQLQuery = StrSQLQuery + "Order By Max(Case IfNull(AG1.GroupName,'') When '' Then IfNull(AG.GroupName,'') "
            StrSQLQuery = StrSQLQuery + "Else IfNull(AG1.GroupName,'') End) "

            DTTemp = AgL.FillData(StrSQLQuery, AgL.GCn).tables(0)
        Catch ex As Exception
            DTTemp = Nothing
        End Try
        Return DTTemp
    End Function
    Private Function FGetPLDataTable() As DataTable
        Dim StrCondition1 As String = ""
        Dim StrSQLQuery As String = ""
        Dim DTTemp As DataTable

        Try
            StrCondition1 = " Where Date(LG.V_Date) <= " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowToDate)).ToString("s")) & " "
            StrCondition1 += Replace(ReportFrm.GetWhereCondition("LG.Site_Code", rowSite), "''", "'")
            StrCondition1 += Replace(ReportFrm.GetWhereCondition("LG.DivCode", rowDivision), "''", "'")

            StrCondition1 += " And Date(LG.V_Date) >= (Case When Ag.GroupNature in ('R','E') Then " & AgL.Chk_Date(CDate(AgL.PubStartDate).ToString("s")) & " Else '1900/01/01' End) "
            '========== For Detail Section =======
            StrSQLQuery = "Select	'Yes' As IsAccountGroup, (Case IfNull(AG1.GroupCode,'') When '' Then IfNull(AG.GroupCode,'') "
            StrSQLQuery = StrSQLQuery + "Else IfNull(AG1.GroupCode,'') End)  As GroupCode, "
            StrSQLQuery = StrSQLQuery + "Max(Case IfNull(AG1.GroupName,'') When '' Then IfNull(AG.GroupName,'') "
            StrSQLQuery = StrSQLQuery + "Else IfNull(AG1.GroupName,'') End)  As GName, "
            StrSQLQuery = StrSQLQuery + "(Case When (IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LG.AmtCr),0))>0 Then  "
            StrSQLQuery = StrSQLQuery + "(IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LG.AmtCr),0)) Else 0.00 End) As AmtDr, "
            StrSQLQuery = StrSQLQuery + "(Case When (IfNull(Sum(LG.AmtCr),0)-IfNull(Sum(LG.AmtDr),0))>0 Then "
            StrSQLQuery = StrSQLQuery + "(IfNull(Sum(LG.AmtCr),0)-IfNull(Sum(LG.AmtDr),0)) Else 0.00 End) As AmtCr, "
            StrSQLQuery = StrSQLQuery + "Max(AG.ContraGroupName) As ContraGroupName,Max(AG.GroupNature) As GroupNature "
            StrSQLQuery = StrSQLQuery + "From Ledger LG Left Join SubGroup SG On LG.SubCode=SG.SubCode  Left Join "
            StrSQLQuery = StrSQLQuery + "AcGroup AG On AG.GroupCode=SG.GroupCode Left Join "
            StrSQLQuery = StrSQLQuery + "AcGroupPath AGP On AGP.GroupCode=AG.GroupCode And AGP.SNo=" & IntLevel & " Left Join "
            StrSQLQuery = StrSQLQuery + "AcGroup AG1 On AG1.GroupCode=AGP.GroupUnder "
            StrSQLQuery = StrSQLQuery + StrCondition1
            StrSQLQuery = StrSQLQuery + "And AG.GroupNature In ('R','E') "

            '=================== For Only PL Data =====================
            StrSQLQuery = StrSQLQuery + "And (AG.Nature Not In ('Direct','Purchase','Sales') Or "
            StrSQLQuery = StrSQLQuery + "AG1.Nature Not In ('Direct','Purchase','Sales')) "
            '==========================================================

            StrSQLQuery = StrSQLQuery + "Group By (Case IfNull(AG1.GroupCode,'') When '' Then IfNull(AG.GroupCode,'') "
            StrSQLQuery = StrSQLQuery + "Else IfNull(AG1.GroupCode,'') End) "
            StrSQLQuery = StrSQLQuery + "Having (IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LG.AmtCr),0)) <> 0 "
            StrSQLQuery = StrSQLQuery + "Order By Max(Case IfNull(AG1.GroupName,'') When '' Then IfNull(AG.GroupName,'') "
            StrSQLQuery = StrSQLQuery + "Else IfNull(AG1.GroupName,'') End) "

            'DTTemp = cmain.FGetDatTable(StrSQLQuery, AgL.GCn)
            DTTemp = AgL.FillData(StrSQLQuery, AgL.GCn).Tables(0)
        Catch ex As Exception
            DTTemp = Nothing
        End Try

        Return DTTemp
    End Function
    Private Sub FProfitAndLoss_Disp()
        Dim DTTemp As DataTable
        Dim DblDebit_Total As Double, DblCredit_Total As Double, DblGrossProfit As Double, DblNetProfit As Double
        Dim I As Integer, J As Integer, IntFindRowFrom As Integer
        Dim DtStockValue As DataTable = Nothing

        Try
            FGetStockValuesInDataTable(DtStockValue, ReportFrm.FGetText(rowFromDate))

            Dim DtSubDetail As DataTable
            If UCase(ReportFrm.FGetText(rowShowWithHierarchy)) = "Yes" Then
                DtSubDetail = FRetDataDisplay_Level_Group().Copy
                DTReport.Rows.Clear()
            End If

            FCreateDataTable(ReportType.ProfitAndLoss)





            '========= For Trading A/c ===========
            DTTemp = FGetTRDDataTable()

            DblDebit_Total = 0
            DblCredit_Total = 0


            For K As Integer = 0 To DtStockValue.Rows.Count - 1
                If AgL.VNull(DtStockValue.Rows(K)("OpeningStockValue")) <> 0 Then
                    J = FFindEmptyRow(DTReport, GRName)
                    DTReport.Rows(J)(GRName) = "Opening Stock" + IIf(AgL.XNull(DtStockValue.Rows(K)("Remark")) <> "", " " + AgL.XNull(DtStockValue.Rows(K)("Remark")), "")
                    DTReport.Rows(J)(Debit) = AgL.VNull(DtStockValue.Rows(K)("OpeningStockValue"))
                    DblDebit_Total = DblDebit_Total + AgL.VNull(DtStockValue.Rows(K)("OpeningStockValue"))
                End If
            Next

            For I = 0 To DTTemp.Rows.Count - 1
                If AgL.VNull(DTTemp.Rows(I).Item("AmtCr")) > 0 Then
                    J = FFindEmptyRow(DTReport, GRNameCredit)
                    DTReport.Rows(J)(IsAccountGroupCredit) = AgL.XNull(DTTemp.Rows(I).Item("IsAccountGroup"))
                    DTReport.Rows(J)(GRCodeCredit) = AgL.XNull(DTTemp.Rows(I).Item("GroupCode"))
                    If UCase(AgL.XNull(DTTemp.Rows(I).Item("GroupNature"))) = "R" Then
                        DTReport.Rows(J)(GRNameCredit) = AgL.XNull(DTTemp.Rows(I).Item("GName"))
                    Else
                        DTReport.Rows(J)(GRNameCredit) = AgL.XNull(DTTemp.Rows(I).Item("ContraGroupName"))
                    End If
                    DTReport.Rows(J)(Credit) = Format(AgL.VNull(DTTemp.Rows(I).Item("AmtCr")), "0.00")
                    DblCredit_Total = DblCredit_Total + Format(AgL.VNull(DTTemp.Rows(I).Item("AmtCr")), "0.00")
                ElseIf AgL.VNull(DTTemp.Rows(I).Item("AmtDr")) > 0 Then
                    J = FFindEmptyRow(DTReport, GRName)
                    DTReport.Rows(J)(IsAccountGroup) = AgL.XNull(DTTemp.Rows(I).Item("IsAccountGroup"))
                    DTReport.Rows(J)(GRCode) = AgL.XNull(DTTemp.Rows(I).Item("GroupCode"))
                    If UCase(AgL.XNull(DTTemp.Rows(I).Item("GroupNature"))) = "E" Then
                        DTReport.Rows(J)(GRName) = AgL.XNull(DTTemp.Rows(I).Item("GName"))
                    Else
                        DTReport.Rows(J)(GRName) = AgL.XNull(DTTemp.Rows(I).Item("ContraGroupName"))
                    End If
                    DTReport.Rows(J)(Debit) = Format(AgL.VNull(DTTemp.Rows(I).Item("AmtDr")), "0.00")
                    DblDebit_Total = DblDebit_Total + Format(AgL.VNull(DTTemp.Rows(I).Item("AmtDr")), "0.00")
                End If
                'DTReport.Rows(J)(GR_SG) = "A"
            Next

            For K As Integer = 0 To DtStockValue.Rows.Count - 1
                If AgL.VNull(DtStockValue.Rows(K)("ClosingStockValue")) <> 0 Then
                    J = FFindEmptyRow(DTReport, GRNameCredit)
                    DTReport.Rows(J)(GRNameCredit) = "Closing Stock" + IIf(AgL.XNull(DtStockValue.Rows(K)("Remark")) <> "", " " + AgL.XNull(DtStockValue.Rows(K)("Remark")), "")
                    DTReport.Rows(J)(Credit) = AgL.VNull(DtStockValue.Rows(K)("ClosingStockValue"))
                    DblCredit_Total = DblCredit_Total + AgL.VNull(DtStockValue.Rows(K)("ClosingStockValue"))
                End If
            Next



            DblGrossProfit = (DblDebit_Total - DblCredit_Total)
            If (DblDebit_Total - DblCredit_Total) > 0 Then
                J = FFindEmptyRow(DTReport, GRNameCredit)
                DTReport.Rows(J)(GRNameCredit) = "Gross Loss"
                DTReport.Rows(J)(Credit) = Format((DblDebit_Total - DblCredit_Total), "0.00")
                DblCredit_Total = DblCredit_Total + Format((DblDebit_Total - DblCredit_Total), "0.00")
                DblDebit_Total = DblCredit_Total
            ElseIf (DblCredit_Total - DblDebit_Total) > 0 Then
                J = FFindEmptyRow(DTReport, GRName)
                DTReport.Rows(J)(GRName) = "Gross Profit"
                DTReport.Rows(J)(Debit) = Format((DblCredit_Total - DblDebit_Total), "0.00")
                DblDebit_Total = DblDebit_Total + Format((DblCredit_Total - DblDebit_Total), "0.00")
                DblCredit_Total = DblDebit_Total
            End If

            If DblDebit_Total > 0 Then
                DTReport.Rows.Add()
                DTReport.Rows.Add()
                DTReport.Rows(DTReport.Rows.Count - 1)(Debit) = Format(DblDebit_Total, "0.00")
                DTReport.Rows(DTReport.Rows.Count - 1)(Credit) = Format(DblCredit_Total, "0.00")
                DTReport.Rows.Add()
            End If
            '==========================================


            '============ For P/L A/c =================
            IntFindRowFrom = DTReport.Rows.Count
            If DblGrossProfit > 0 Then
                J = FFindEmptyRow(DTReport, GRName, IntFindRowFrom)
                DTReport.Rows(J)(GRName) = "Gross Loss"
                DTReport.Rows(J)(Debit) = Format(Math.Abs(DblGrossProfit), "0.00")
            ElseIf DblGrossProfit < 0 Then
                J = FFindEmptyRow(DTReport, GRNameCredit, IntFindRowFrom)
                DTReport.Rows(J)(GRNameCredit) = "Gross Profit"
                DTReport.Rows(J)(Credit) = Format(Math.Abs(DblGrossProfit), "0.00")
            End If
            DTTemp = FGetPLDataTable()

            DblDebit_Total = 0
            DblCredit_Total = 0
            For I = 0 To DTTemp.Rows.Count - 1
                If AgL.VNull(DTTemp.Rows(I).Item("AmtCr")) > 0 Then
                    J = FFindEmptyRow(DTReport, GRNameCredit, IntFindRowFrom)
                    DTReport.Rows(J)(IsAccountGroupCredit) = AgL.XNull(DTTemp.Rows(I).Item("IsAccountGroup"))
                    DTReport.Rows(J)(GRCodeCredit) = AgL.XNull(DTTemp.Rows(I).Item("GroupCode"))
                    If UCase(AgL.XNull(DTTemp.Rows(I).Item("GroupNature"))) = "R" Then
                        DTReport.Rows(J)(GRNameCredit) = AgL.XNull(DTTemp.Rows(I).Item("GName"))
                    Else
                        DTReport.Rows(J)(GRNameCredit) = AgL.XNull(DTTemp.Rows(I).Item("ContraGroupName"))
                    End If
                    DTReport.Rows(J)(Credit) = Format(AgL.VNull(DTTemp.Rows(I).Item("AmtCr")), "0.00")
                    DblCredit_Total = DblCredit_Total + Format(AgL.VNull(DTTemp.Rows(I).Item("AmtCr")), "0.00")
                ElseIf AgL.VNull(DTTemp.Rows(I).Item("AmtDr")) > 0 Then
                    J = FFindEmptyRow(DTReport, GRName, IntFindRowFrom)
                    DTReport.Rows(J)(IsAccountGroup) = AgL.XNull(DTTemp.Rows(I).Item("IsAccountGroup"))
                    DTReport.Rows(J)(GRCode) = AgL.XNull(DTTemp.Rows(I).Item("GroupCode"))
                    If UCase(AgL.XNull(DTTemp.Rows(I).Item("GroupNature"))) = "E" Then
                        DTReport.Rows(J)(GRName) = AgL.XNull(DTTemp.Rows(I).Item("GName"))
                    Else
                        DTReport.Rows(J)(GRName) = AgL.XNull(DTTemp.Rows(I).Item("ContraGroupName"))
                    End If
                    DTReport.Rows(J)(Debit) = Format(AgL.VNull(DTTemp.Rows(I).Item("AmtDr")), "0.00")
                    DblDebit_Total = DblDebit_Total + Format(AgL.VNull(DTTemp.Rows(I).Item("AmtDr")), "0.00")
                End If
                'DTReport.Rows(J)(GR_SG) = "A"
            Next

            DblNetProfit = DblGrossProfit + (DblDebit_Total - DblCredit_Total)
            If DblNetProfit > 0 Then
                J = FFindEmptyRow(DTReport, GRNameCredit, IntFindRowFrom)
                DTReport.Rows(J)(GRNameCredit) = "Net Loss"
                DTReport.Rows(J)(Credit) = Format(Math.Abs(DblNetProfit), "0.00")
                DblCredit_Total = DblCredit_Total + Format(Math.Abs(DblNetProfit), "0.00")
                DblDebit_Total = DblCredit_Total
            ElseIf DblNetProfit < 0 Then
                J = FFindEmptyRow(DTReport, GRName, IntFindRowFrom)
                DTReport.Rows(J)(GRName) = "Net Profit"
                DTReport.Rows(J)(Debit) = Format(Math.Abs(DblNetProfit), "0.00")
                DblDebit_Total = DblDebit_Total + Format(Math.Abs(DblNetProfit), "0.00")
                DblCredit_Total = DblDebit_Total
            End If



            'DTReport.Rows.Add()
            'DTReport.Rows.Add()
            'DTReport.Rows(DTReport.Rows.Count - 1)(Debit) = Format(DblDebit_Total, "0.00")
            'DTReport.Rows(DTReport.Rows.Count - 1)(Credit) = Format(DblCredit_Total, "0.00")
            'DTReport.Rows.Add()

            If DTReport.Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")


            If UCase(ReportFrm.FGetText(rowShowWithHierarchy)) = "Yes" Then

                Dim bRowIndex_Total_InOldDataTable As Integer = 0

                Dim DtGroupHeader As DataTable = DTReport.Copy()
                DTReport.Rows.Clear()

                For I = 0 To DtGroupHeader.Rows.Count - 1
                    If AgL.StrCmp(AgL.XNull(DtGroupHeader.Rows(I)(GRName)), "") And
                            AgL.VNull(DtGroupHeader.Rows(I)(Debit)) <> 0 Then
                        bRowIndex_Total_InOldDataTable = I
                        Exit For
                    End If

                    DTReport.Rows.Add()
                    DTReport.Rows(DTReport.Rows.Count - 1)(GRCode) = DtGroupHeader.Rows(I)(GRCode)
                    DTReport.Rows(DTReport.Rows.Count - 1)(GRName) = DtGroupHeader.Rows(I)(GRName)
                    DTReport.Rows(DTReport.Rows.Count - 1)(Debit) = DtGroupHeader.Rows(I)(Debit)
                    DTReport.Rows(DTReport.Rows.Count - 1)(IsAccountGroup) = DtGroupHeader.Rows(I)(IsAccountGroup)

                    Dim DrGroupDetail As DataRow() = DtSubDetail.Select("[" + AcGroupCode + "] = '" + DtGroupHeader.Rows(I)(GRCode) + "'")
                    For J = 0 To DrGroupDetail.Length - 1
                        DTReport.Rows.Add()
                        DTReport.Rows(DTReport.Rows.Count - 1)(GRCode) = DrGroupDetail(J)(GRCode)
                        DTReport.Rows(DTReport.Rows.Count - 1)(GRName) = "        " + DrGroupDetail(J)(GRName)
                        If AgL.VNull(DrGroupDetail(J)(Debit)) > 0 Then
                            DTReport.Rows(DTReport.Rows.Count - 1)(Debit) = DrGroupDetail(J)(Debit)
                        ElseIf AgL.VNull(DrGroupDetail(J)(Credit)) > 0 Then
                            DTReport.Rows(DTReport.Rows.Count - 1)(Debit) = -DrGroupDetail(J)(Credit)
                        End If
                    Next
                Next

                For I = 0 To DtGroupHeader.Rows.Count - 1
                    If AgL.StrCmp(AgL.XNull(DtGroupHeader.Rows(I)(GRNameCredit)), "") And
                            AgL.VNull(DtGroupHeader.Rows(I)(Credit)) <> 0 Then
                        Exit For
                    End If

                    Dim bRowIndex As Integer = FindNextRowForBalanceSheerHirerichy()

                    DTReport.Rows(bRowIndex)(GRCodeCredit) = DtGroupHeader.Rows(I)(GRCodeCredit)
                    DTReport.Rows(bRowIndex)(GRNameCredit) = DtGroupHeader.Rows(I)(GRNameCredit)
                    DTReport.Rows(bRowIndex)(Credit) = DtGroupHeader.Rows(I)(Credit)
                    DTReport.Rows(bRowIndex)(IsAccountGroupCredit) = DtGroupHeader.Rows(I)(IsAccountGroupCredit)

                    Dim DrGroupDetail As DataRow() = DtSubDetail.Select("[" + AcGroupCode + "] = '" + DtGroupHeader.Rows(I)(GRCodeCredit) + "'")
                    For J = 0 To DrGroupDetail.Length - 1
                        bRowIndex = FindNextRowForBalanceSheerHirerichy()
                        DTReport.Rows(bRowIndex)(GRCodeCredit) = DrGroupDetail(J)(GRCode)
                        DTReport.Rows(bRowIndex)(GRNameCredit) = "        " + DrGroupDetail(J)(GRName)
                        If AgL.VNull(DrGroupDetail(J)(Credit)) > 0 Then
                            DTReport.Rows(bRowIndex)(Credit) = AgL.VNull(DrGroupDetail(J)(Credit))
                        ElseIf AgL.VNull(DrGroupDetail(J)(Debit)) > 0 Then
                            DTReport.Rows(bRowIndex)(Credit) = AgL.VNull(DrGroupDetail(J)(Debit))
                        End If
                    Next
                Next

                Dim bRowIndex_Total_InNewDataTable As Integer = 0
                For I = bRowIndex_Total_InOldDataTable To DtGroupHeader.Rows.Count - 1
                    DTReport.Rows.Add()
                    DTReport.Rows(DTReport.Rows.Count - 1)(GRCode) = DtGroupHeader.Rows(I)(GRCode)
                    DTReport.Rows(DTReport.Rows.Count - 1)(GRName) = DtGroupHeader.Rows(I)(GRName)
                    DTReport.Rows(DTReport.Rows.Count - 1)(Debit) = DtGroupHeader.Rows(I)(Debit)
                    DTReport.Rows(DTReport.Rows.Count - 1)(IsAccountGroup) = DtGroupHeader.Rows(I)(IsAccountGroup)

                    If AgL.StrCmp(AgL.XNull(DtGroupHeader.Rows(I)(GRName)), "") And
                            AgL.VNull(DtGroupHeader.Rows(I)(Debit)) <> 0 Then
                        bRowIndex_Total_InNewDataTable = DTReport.Rows.Count - 1
                    End If

                    Dim DrGroupDetail As DataRow() = DtSubDetail.Select("[" + AcGroupCode + "] = '" + DtGroupHeader.Rows(I)(GRCode) + "'")
                    For J = 0 To DrGroupDetail.Length - 1
                        DTReport.Rows.Add()
                        DTReport.Rows(DTReport.Rows.Count - 1)(GRCode) = DrGroupDetail(J)(GRCode)
                        DTReport.Rows(DTReport.Rows.Count - 1)(GRName) = "        " + DrGroupDetail(J)(GRName)
                        If AgL.VNull(DrGroupDetail(J)(Debit)) > 0 Then
                            DTReport.Rows(DTReport.Rows.Count - 1)(Debit) = DrGroupDetail(J)(Debit)
                        ElseIf AgL.VNull(DrGroupDetail(J)(Credit)) > 0 Then
                            DTReport.Rows(DTReport.Rows.Count - 1)(Debit) = -DrGroupDetail(J)(Credit)
                        End If
                    Next
                Next

                For I = bRowIndex_Total_InOldDataTable To DtGroupHeader.Rows.Count - 1
                    Dim bRowIndex As Integer = FindNextRowForBalanceSheerHirerichy()

                    If AgL.StrCmp(AgL.XNull(DtGroupHeader.Rows(I)(GRNameCredit)), "") And
                            AgL.VNull(DtGroupHeader.Rows(I)(Credit)) <> 0 Then
                        bRowIndex = bRowIndex_Total_InNewDataTable
                    End If

                    If bRowIndex = bRowIndex_Total_InNewDataTable + 1 Then
                        bRowIndex = bRowIndex + 1
                    End If


                    DTReport.Rows(bRowIndex)(GRCodeCredit) = DtGroupHeader.Rows(I)(GRCodeCredit)
                    DTReport.Rows(bRowIndex)(GRNameCredit) = DtGroupHeader.Rows(I)(GRNameCredit)
                    DTReport.Rows(bRowIndex)(Credit) = DtGroupHeader.Rows(I)(Credit)
                    DTReport.Rows(bRowIndex)(IsAccountGroupCredit) = DtGroupHeader.Rows(I)(IsAccountGroupCredit)

                    Dim DrGroupDetail As DataRow() = DtSubDetail.Select("[" + AcGroupCode + "] = '" + DtGroupHeader.Rows(I)(GRCodeCredit) + "'")
                    For J = 0 To DrGroupDetail.Length - 1
                        bRowIndex = FindNextRowForBalanceSheerHirerichy()
                        DTReport.Rows(bRowIndex)(GRCodeCredit) = DrGroupDetail(J)(GRCode)
                        DTReport.Rows(bRowIndex)(GRNameCredit) = "        " + DrGroupDetail(J)(GRName)
                        If AgL.VNull(DrGroupDetail(J)(Credit)) > 0 Then
                            DTReport.Rows(bRowIndex)(Credit) = AgL.VNull(DrGroupDetail(J)(Credit))
                        ElseIf AgL.VNull(DrGroupDetail(J)(Debit)) > 0 Then
                            DTReport.Rows(bRowIndex)(Credit) = AgL.VNull(DrGroupDetail(J)(Debit))
                        End If
                    Next
                Next


                'For I = 0 To DTReport.Rows.Count - 1
                '    If AgL.StrCmp(AgL.XNull(DTReport.Rows(I)(GRName)), "Net Profit") Or
                '        AgL.StrCmp(AgL.XNull(DTReport.Rows(I)(GRName)), "Net Loss") Then
                '        DTReport.Rows.Add()
                '        DTReport.Rows(DTReport.Rows.Count - 1)(GRName) = DTReport.Rows(I)(GRName)
                '        DTReport.Rows(DTReport.Rows.Count - 1)(Debit) = AgL.VNull(DTReport.Rows(I)(Debit))
                '        DTReport.Rows(I)(GRName) = System.DBNull.Value
                '        DTReport.Rows(I)(Debit) = System.DBNull.Value
                '    End If

                '    If AgL.StrCmp(AgL.XNull(DTReport.Rows(I)(GRNameCredit)), "Net Profit") Or
                '            AgL.StrCmp(AgL.XNull(DTReport.Rows(I)(GRNameCredit)), "Net Loss") Then
                '        DTReport.Rows.Add()
                '        DTReport.Rows(DTReport.Rows.Count - 1)(GRNameCredit) = DTReport.Rows(I)(GRNameCredit)
                '        DTReport.Rows(DTReport.Rows.Count - 1)(Credit) = AgL.VNull(DTReport.Rows(I)(Credit))
                '        DTReport.Rows(I)(GRNameCredit) = System.DBNull.Value
                '        DTReport.Rows(I)(Credit) = System.DBNull.Value
                '    End If

                '    If AgL.StrCmp(AgL.XNull(DTReport.Rows(I)(GRNameCredit)), "") And
                '            AgL.VNull(DTReport.Rows(I)(Credit)) <> 0 Then
                '        For J = 0 To DTReport.Rows.Count - 1
                '            If AgL.StrCmp(AgL.XNull(DTReport.Rows(J)(GRName)), "") And
                '                AgL.VNull(DTReport.Rows(J)(Debit)) <> 0 Then
                '                DTReport.Rows(J)(Credit) = AgL.VNull(DTReport.Rows(I)(Credit))
                '                DTReport.Rows(I)(Credit) = System.DBNull.Value
                '                Exit For
                '            End If
                '        Next
                '    End If
                'Next
            End If


            'ReportFrm.Text = "Financial Display - " + ReportFrm.FGetText(rowReportType)
            ReportFrm.Text = ReportFrm.FGetText(rowReportType)
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = "ProcFinancialDisplay"
            ReportFrm.IsHideZeroColumns = False
            ReportFrm.IsManualAggregate = True

            DsReport = New DataSet()
            DsReport.Tables.Add(DTReport)
            ReportFrm.ProcFillGrid(DsReport)

            ReportFrm.DGL1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None

            For I = 0 To ReportFrm.DGL1.Columns.Count - 1
                ReportFrm.DGL1.Columns(I).SortMode = DataGridViewColumnSortMode.NotSortable
            Next I

            ReportFrm.DGL2.Item(ReportFrm.DGL1.Columns(Debit).Index, 0).Value = Format(DblDebit_Total, "0.00")
            ReportFrm.DGL2.Item(ReportFrm.DGL1.Columns(Credit).Index, 0).Value = Format(DblDebit_Total, "0.00")

            ReportFrm.DGL1.Columns(IsAccountGroup).Visible = False
            ReportFrm.DGL1.Columns(IsAccountGroupCredit).Visible = False


            'ReportFrm.DGL2.Visible = False
            ReportFrm.DGL1.Columns(GRCodeCredit).Visible = False
            ReportFrm.DGL2.Columns(ReportFrm.DGL1.Columns(GRCodeCredit).Index).Visible = False

            ReportFrm.DGL1.Columns(GRName).HeaderText = "Particulars"
            ReportFrm.DGL1.Columns(GRNameCredit).HeaderText = "Particulars"
            ReportFrm.DGL1.Columns(Debit).HeaderText = "Amount"
            ReportFrm.DGL1.Columns(Credit).HeaderText = "Amount"

            ReportFrm.DGL1.Columns(GRName).Width = 440
            ReportFrm.DGL1.Columns(GRNameCredit).Width = 440
            ReportFrm.DGL1.Columns(Debit).Width = 150
            ReportFrm.DGL1.Columns(Credit).Width = 150
        Catch ex As Exception
            If Not ex.Message.Contains("Index was out of range") Then
                MsgBox(ex.Message)
            End If
        End Try
    End Sub
    Private Sub FDisplay_SubGroup(ByVal StrForCode As String, ByVal StrForName As String, StrForType As String)
        Dim StrCondition1 As String = "", StrConditionOP As String = ""
        Dim StrSQLQuery As String = ""
        Dim DTTemp As DataTable
        Dim DblDebit_Total As Double, DblCredit_Total As Double, DblOpening As Double
        Dim I As Integer, J As Integer
        Dim Color_Main As Color, Color_A As Color, Color_B As Color
        Dim mExcludeLedgerAccountsFromTrial As String = ""


        Try
            mExcludeLedgerAccountsFromTrial = ClsMain.FGetSettings(SettingFields.ExcludeLedgerAccountsFromTrial, SettingType.General, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
            StrConditionOP = " Where LG.V_Date < " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowFromDate)).ToString("s")) & " "
            StrConditionOP += Replace(ReportFrm.GetWhereCondition("LG.Site_Code", rowSite), "''", "'")
            StrConditionOP += Replace(ReportFrm.GetWhereCondition("LG.DivCode", rowDivision), "''", "'")
            StrConditionOP += " And Date(LG.V_Date) >= (Case When Ag.GroupNature in ('R','E') 
                            Then " & AgL.Chk_Date(CDate(AgL.PubStartDate).ToString("s")) & " Else '1900/01/01' End)  "

            If ReportFrm.FGetText(rowAddColumn).ToString.Contains(AddColumn.LinkedSubCode) Then
                'If AgL.XNull(ReportFrm.FGetCode(rowLinkedAccountCode)) <> "" Then
                StrConditionOP += " And IfNull(Lg.LinkedSubCode,'') = '" & AgL.XNull(ReportFrm.FGetCode(rowLinkedAccountCode)) & "'"
                'End If
            End If

            If mExcludeLedgerAccountsFromTrial <> "" Then
                StrConditionOP += " And IfNull(Sg.Status,'Active') Not In ('" & mExcludeLedgerAccountsFromTrial.Replace("+", "','") & "') "
            End If


            StrCondition1 = " Where ( Date(LG.V_Date) Between  " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowFromDate)).ToString("s")) & "
                                And " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowToDate)).ToString("s")) & ") "
            StrCondition1 += Replace(ReportFrm.GetWhereCondition("LG.Site_Code", rowSite), "''", "'")
            StrCondition1 += Replace(ReportFrm.GetWhereCondition("LG.DivCode", rowDivision), "''", "'")
            StrCondition1 += ReportFrm.GetWhereCondition("LG.V_Type", rowV_Type)

            If ReportFrm.FGetText(rowAddColumn).ToString.Contains(AddColumn.LinkedSubCode) Then
                'If AgL.XNull(ReportFrm.FGetCode(rowLinkedAccountCode)) <> "" Then
                StrCondition1 += " And IfNull(Lg.LinkedSubCode,'') = '" & AgL.XNull(ReportFrm.FGetCode(rowLinkedAccountCode)) & "'"
                'End If
            End If

            If mExcludeLedgerAccountsFromTrial <> "" Then
                StrCondition1 += " And IfNull(Sg.Status,'Active') Not In ('" & mExcludeLedgerAccountsFromTrial.Replace("+", "','") & "') "
            End If


            '========== For Detail Section =======
            If ReportFrm.FGetText(rowIncludeOpening) = "Yes" Then
                StrSQLQuery = "Select	Null As DocId, Null as Division, Null as Site,'Opening' As Narration, Max(LG.SubCode) As SubCode, Max(Sg.Name) As Name, Max(Ag.GroupCode) As GroupCode, Max(Ag.GroupName) As GroupName, "
                StrSQLQuery = StrSQLQuery + "(Case When (IfNull(Sum(AmtDr),0)-IfNull(Sum(LG.AmtCr),0))>0 Then "
                StrSQLQuery = StrSQLQuery + "(IfNull(Sum(AmtDr),0)-IfNull(Sum(LG.AmtCr),0)) Else 0.00 End) As AmtDr, "
                StrSQLQuery = StrSQLQuery + "(Case When (IfNull(Sum(AmtCr),0)-IfNull(Sum(LG.AmtDr),0))>0 Then "
                StrSQLQuery = StrSQLQuery + "(IfNull(Sum(AmtCr),0)-IfNull(Sum(LG.AmtDr),0)) Else 0.00 End) As AmtCr, "
                StrSQLQuery = StrSQLQuery + "Null As V_No,Null As V_Type,Null As V_Date,0 As SNo,'' As ContraText, "
                StrSQLQuery = StrSQLQuery + "Null As SerialNo "
                StrSQLQuery = StrSQLQuery + "From Ledger LG "
                StrSQLQuery = StrSQLQuery + "Left Join Subgroup SG On Sg.SubCode = LG.SubCode "
                StrSQLQuery = StrSQLQuery + "Left Join AcGroup AG On Ag.GroupCode = Sg.GroupCode "
                StrSQLQuery = StrSQLQuery + "Left Join Division Div On Lg.DivCode = div.Div_Code "
                StrSQLQuery = StrSQLQuery + "Left Join SiteMast Site On Lg.Site_Code = Site.Code "
                StrSQLQuery = StrSQLQuery + StrConditionOP
                'StrSQLQuery = StrSQLQuery + "And LG.SubCode='" & StrForCode & "' " + IIf(AgL.PubServerName = "", " Group By '1', Lg.Site_Code, Lg.DivCode ", "")

                If StrForType = "Account Group" Then
                    StrSQLQuery = StrSQLQuery + "And SG.GroupCode='" & StrForCode & "' " + IIf(AgL.PubServerName = "", " Group By '1'", "")
                Else
                    StrSQLQuery = StrSQLQuery + "And LG.SubCode='" & StrForCode & "' " + IIf(AgL.PubServerName = "", " Group By '1'", "")
                End If

                StrSQLQuery = StrSQLQuery + "Having (IfNull(Sum(AmtDr),0)-IfNull(Sum(LG.AmtCr),0))<>0 "
                StrSQLQuery = StrSQLQuery + "Union All "
            End If

            StrSQLQuery = StrSQLQuery + "Select	LG.DocId, Div.ShortName as Division, Site.ShortName as Site,"
            StrSQLQuery = StrSQLQuery + "LG.Narration || Case When LG.Chq_No Is Not Null Then ', Cheque No.' || LG.Chq_No Else '' End || Case When LG.Chq_Date Is Not Null Then ', Cheque Date ' || Cast(LG.Chq_Date As nvarchar) Else '' End As Narration , "
            StrSQLQuery = StrSQLQuery + "LG.SubCode As SubCode,Sg.Name, Ag.GroupCode, Ag.GroupName As GroupName, LG.AmtDr,LG.AmtCr,LG.RecID As V_No, "
            StrSQLQuery = StrSQLQuery + "LG.V_Type,LG.V_Date,1 As SNo, ContraText,Lg.RecID as SerialNo "
            StrSQLQuery = StrSQLQuery + "From Ledger LG "
            StrSQLQuery = StrSQLQuery + "Left Join Subgroup SG On Sg.SubCode = LG.SubCode "
            StrSQLQuery = StrSQLQuery + "Left Join AcGroup AG On Ag.GroupCode = Sg.GroupCode "
            StrSQLQuery = StrSQLQuery + "Left Join Voucher_Type VT On LG.V_Type=VT.V_Type "
            StrSQLQuery = StrSQLQuery + "Left Join Division Div On Lg.DivCode = div.Div_Code "
            StrSQLQuery = StrSQLQuery + "Left Join SiteMast Site On Lg.Site_Code = Site.Code "
            StrSQLQuery = StrSQLQuery + StrCondition1
            If StrForType = "Account Group" Then
                StrSQLQuery = StrSQLQuery + "And SG.GroupCode='" & StrForCode & "' "
            Else
                StrSQLQuery = StrSQLQuery + "And LG.SubCode='" & StrForCode & "' "
            End If

            If ReportFrm.FGetText(rowReportType) = ReportType.Ledger_MonthWise Then
                FCreateDataTable(ReportType.Ledger_MonthWise)

                mQry = " Select Case When VLedger.V_Date Is Not Null Then " & IIf(AgL.PubServerName = "", "strftime('%m-%Y',VLedger.V_Date)", "Substring(Convert(NVARCHAR, VLedger.V_Date,103),4,7)") & " Else 'Opening' End As Month, 
                    Sum(VLedger.AmtDr) As AmtDr, Sum(VLedger.AmtCr) As AmtCr, Max(VLedger.SNo) As SNo, Max(VLedger.SubCode) As SubCode, Max(VLedger.Name) As Name
                    From (" & StrSQLQuery & ") As VLedger
                    GROUP BY Case When VLedger.V_Date Is Not Null Then " & IIf(AgL.PubServerName = "", "strftime('%m-%Y',VLedger.V_Date)", "Substring(Convert(NVARCHAR, VLedger.V_Date,103),4,7)") & " Else 'Opening' End 
                    Order By SNo, " &
                    IIf(AgL.PubServerName = "", "strftime('%Y',VLedger.V_Date), strftime('%m',VLedger.V_Date)",
                        "Year(Max(VLedger.V_Date)), Month(MAx(VLedger.V_Date))") & " "
                DTTemp = AgL.FillData(mQry, AgL.GCn).tables(0)

                For I = 0 To DTTemp.Rows.Count - 1
                    DTReport.Rows.Add()
                    J = DTReport.Rows.Count - 1

                    DTReport.Rows(J)(GRCode) = AgL.XNull(DTTemp.Rows(I).Item("SubCode"))
                    DTReport.Rows(J)(GRName) = AgL.XNull(DTTemp.Rows(I).Item("Name"))
                    DTReport.Rows(J)(Month) = AgL.XNull(DTTemp.Rows(I).Item("Month"))
                    If AgL.VNull(DTTemp.Rows(I).Item("SNo")) <> 0 Then
                        DTReport.Rows(J)(Debit) = IIf(AgL.VNull(DTTemp.Rows(I).Item("AmtDr")) > 0, Format(AgL.VNull(DTTemp.Rows(I).Item("AmtDr")), "0.00"), System.DBNull.Value)
                        DTReport.Rows(J)(Credit) = IIf(AgL.VNull(DTTemp.Rows(I).Item("AmtCr")) > 0, Format(AgL.VNull(DTTemp.Rows(I).Item("AmtCr")), "0.00"), System.DBNull.Value)
                        DblDebit_Total = DblDebit_Total + Format(AgL.VNull(DTTemp.Rows(I).Item("AmtDr")), "0.00")
                        DblCredit_Total = DblCredit_Total + Format(AgL.VNull(DTTemp.Rows(I).Item("AmtCr")), "0.00")
                    Else
                        DblOpening = IIf(AgL.VNull(DTTemp.Rows(I).Item("AmtDr")) > 0, Val(Format(AgL.VNull(DTTemp.Rows(I).Item("AmtDr")), "0.00")), 0 - Val(Format(AgL.VNull(DTTemp.Rows(I).Item("AmtCr")), "0.00")))
                    End If

                    DTReport.Rows(J)(Closing) = IIf((DblOpening + DblDebit_Total - DblCredit_Total) <> 0, Format(Math.Abs(DblOpening + DblDebit_Total - DblCredit_Total), "0.00"), System.DBNull.Value)

                    DTReport.Rows(J)(DR_CR_CL) = IIf((DblOpening + DblDebit_Total - DblCredit_Total) > 0, "Dr", "Cr")
                    If (DblOpening + DblDebit_Total - DblCredit_Total) = 0 Then DTReport.Rows(J)(DR_CR_CL) = ""
                Next

                If DTReport.Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

                'ReportFrm.Text = "Financial Display - " + ReportFrm.FGetText(rowReportType)
                ReportFrm.Text = ReportFrm.FGetText(rowReportType)
                ReportFrm.ClsRep = Me
                ReportFrm.ReportProcName = "ProcFinancialDisplay"
                ReportFrm.IsHideZeroColumns = False

                DsReport = New DataSet()
                DsReport.Tables.Add(DTReport)
                ReportFrm.ProcFillGrid(DsReport)

                For I = 0 To ReportFrm.DGL1.Columns.Count - 1
                    ReportFrm.DGL1.Columns(I).SortMode = DataGridViewColumnSortMode.NotSortable
                    ReportFrm.DGL2.Item(I, 0).Value = ""
                Next I

                ReportFrm.DGL2.Item(ReportFrm.DGL1.Columns(Debit).Index, 0).Value = Format(DblDebit_Total, "0.00")
                ReportFrm.DGL2.Item(ReportFrm.DGL1.Columns(Credit).Index, 0).Value = Format(DblCredit_Total, "0.00")
                ReportFrm.DGL2.Item(ReportFrm.DGL1.Columns(Closing).Index, 0).Value = IIf((DblOpening + DblDebit_Total - DblCredit_Total) <> 0, Format(Math.Abs(DblOpening + DblDebit_Total - DblCredit_Total), "0.00"), "")
                ReportFrm.DGL2.Item(ReportFrm.DGL1.Columns(DR_CR_CL).Index, 0).Value = IIf((DblOpening + DblDebit_Total - DblCredit_Total) > 0, "Dr", "Cr")

                ReportFrm.DGL1.Columns(Month).Width = 250
                ReportFrm.DGL1.Columns(Debit).Width = 250
                ReportFrm.DGL1.Columns(Credit).Width = 250
                ReportFrm.DGL1.Columns(Closing).Width = 250
                ReportFrm.DGL1.Columns(DR_CR_CL).Width = 30
                ReportFrm.DGL1.Columns(DR_CR_CL).HeaderText = ""
                ReportFrm.DGL1.Columns(GRName).Visible = False
            ElseIf ReportFrm.FGetText(rowReportType) = ReportType.Ledger_VoucherTypeWise Or ReportFrm.FGetText(rowReportType) = ReportType.GroupLedger_VoucherTypeWise Then
                FCreateDataTable(ReportType.Ledger_VoucherTypeWise)

                mQry = " Select IfNull(Max(Vt.Description),'Opening') As Type, 
                    Sum(VLedger.AmtDr) As AmtDr, Sum(VLedger.AmtCr) As AmtCr, Max(VLedger.SNo) As SNo, "

                If ReportFrm.FGetText(rowReportType) = ReportType.GroupLedger_VoucherTypeWise Then
                    mQry += " Max(VLedger.GroupCode) As SubCode, Max(VLedger.GroupName) As Name, "
                Else
                    mQry += " Max(VLedger.SubCode) As SubCode, Max(VLedger.Name) As Name, "
                End If
                mQry += " VLedger.V_Type
                    From (" & StrSQLQuery & ") As VLedger
                    LEFT JOIN Voucher_Type Vt On VLedger.V_Type = Vt.V_Type
                    GROUP BY VLedger.V_Type
                    Order By Max(Vt.Description) "
                DTTemp = AgL.FillData(mQry, AgL.GCn).tables(0)

                For I = 0 To DTTemp.Rows.Count - 1
                    DTReport.Rows.Add()
                    J = DTReport.Rows.Count - 1

                    DTReport.Rows(J)(GRCode) = AgL.XNull(DTTemp.Rows(I).Item("SubCode"))
                    DTReport.Rows(J)(GRName) = AgL.XNull(DTTemp.Rows(I).Item("Name"))
                    DTReport.Rows(J)(VType) = AgL.XNull(DTTemp.Rows(I).Item("V_Type"))
                    DTReport.Rows(J)(VoucherTypeDesc) = AgL.XNull(DTTemp.Rows(I).Item("Type"))
                    If AgL.VNull(DTTemp.Rows(I).Item("SNo")) <> 0 Then
                        DTReport.Rows(J)(Debit) = IIf(AgL.VNull(DTTemp.Rows(I).Item("AmtDr")) > 0, Format(AgL.VNull(DTTemp.Rows(I).Item("AmtDr")), "0.00"), System.DBNull.Value)
                        DTReport.Rows(J)(Credit) = IIf(AgL.VNull(DTTemp.Rows(I).Item("AmtCr")) > 0, Format(AgL.VNull(DTTemp.Rows(I).Item("AmtCr")), "0.00"), System.DBNull.Value)
                        DblDebit_Total = DblDebit_Total + Format(AgL.VNull(DTTemp.Rows(I).Item("AmtDr")), "0.00")
                        DblCredit_Total = DblCredit_Total + Format(AgL.VNull(DTTemp.Rows(I).Item("AmtCr")), "0.00")
                    Else
                        DblOpening = IIf(AgL.VNull(DTTemp.Rows(I).Item("AmtDr")) > 0, Val(Format(AgL.VNull(DTTemp.Rows(I).Item("AmtDr")), "0.00")), 0 - Val(Format(AgL.VNull(DTTemp.Rows(I).Item("AmtCr")), "0.00")))
                    End If

                    DTReport.Rows(J)(Closing) = IIf((DblOpening + DblDebit_Total - DblCredit_Total) <> 0, Format(Math.Abs(DblOpening + DblDebit_Total - DblCredit_Total), "0.00"), System.DBNull.Value)

                    DTReport.Rows(J)(DR_CR_CL) = IIf((DblOpening + DblDebit_Total - DblCredit_Total) > 0, "Dr", "Cr")
                    If (DblOpening + DblDebit_Total - DblCredit_Total) = 0 Then DTReport.Rows(J)(DR_CR_CL) = ""
                Next

                If DTReport.Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

                'ReportFrm.Text = "Financial Display - " + ReportFrm.FGetText(rowReportType)
                ReportFrm.Text = ReportFrm.FGetText(rowReportType)
                ReportFrm.ClsRep = Me
                ReportFrm.ReportProcName = "ProcFinancialDisplay"
                ReportFrm.IsHideZeroColumns = False

                DsReport = New DataSet()
                DsReport.Tables.Add(DTReport)
                ReportFrm.ProcFillGrid(DsReport)

                For I = 0 To ReportFrm.DGL1.Columns.Count - 1
                    ReportFrm.DGL1.Columns(I).SortMode = DataGridViewColumnSortMode.NotSortable
                    ReportFrm.DGL2.Item(I, 0).Value = ""
                Next I

                ReportFrm.DGL2.Item(ReportFrm.DGL1.Columns(Debit).Index, 0).Value = Format(DblDebit_Total, "0.00")
                ReportFrm.DGL2.Item(ReportFrm.DGL1.Columns(Credit).Index, 0).Value = Format(DblCredit_Total, "0.00")
                ReportFrm.DGL2.Item(ReportFrm.DGL1.Columns(Closing).Index, 0).Value = IIf((DblOpening + DblDebit_Total - DblCredit_Total) <> 0, Format(Math.Abs(DblOpening + DblDebit_Total - DblCredit_Total), "0.00"), "")
                ReportFrm.DGL2.Item(ReportFrm.DGL1.Columns(DR_CR_CL).Index, 0).Value = IIf((DblOpening + DblDebit_Total - DblCredit_Total) > 0, "Dr", "Cr")

                ReportFrm.DGL1.Columns(VoucherTypeDesc).Width = 250
                ReportFrm.DGL1.Columns(Debit).Width = 250
                ReportFrm.DGL1.Columns(Credit).Width = 250
                ReportFrm.DGL1.Columns(Closing).Width = 250
                ReportFrm.DGL1.Columns(DR_CR_CL).Width = 30
                ReportFrm.DGL1.Columns(DR_CR_CL).HeaderText = ""
                ReportFrm.DGL1.Columns(GRName).Visible = False
                ReportFrm.DGL1.Columns(VType).Visible = False
            Else
                StrSQLQuery = StrSQLQuery + "Order By SNo,V_Date,SerialNo,V_No"
                DTTemp = AgL.FillData(StrSQLQuery, AgL.GCn).tables(0)

                FCreateDataTable(ReportType.Ledger)

                Color_A = Color.Linen
                Color_B = Color.Cornsilk

                DblDebit_Total = 0
                DblCredit_Total = 0
                For I = 0 To DTTemp.Rows.Count - 1

                    DTReport.Rows.Add()
                    J = DTReport.Rows.Count - 1

                    If Color_Main = Color_B Then
                        Color_Main = Color_A
                    Else
                        Color_Main = Color_B
                    End If

                    'DTReport.Rows(J)(GR_SG) = "T"
                    DTReport.Rows(J)(DocId) = AgL.XNull(DTTemp.Rows(I).Item("DocId"))
                    If AgL.PubDivisionCount > 1 Then
                        DTReport.Rows(J)(Division) = AgL.XNull(DTTemp.Rows(I).Item("Division"))
                    End If
                    If AgL.PubSiteCount > 1 Then
                        DTReport.Rows(J)(Site) = AgL.XNull(DTTemp.Rows(I).Item("Site"))
                    End If
                    DTReport.Rows(J)(VNo) = AgL.XNull(DTTemp.Rows(I).Item("V_No"))
                    DTReport.Rows(J)(VType) = AgL.XNull(DTTemp.Rows(I).Item("V_Type"))
                    DTReport.Rows(J)(VDate) = Format(AgL.XNull(DTTemp.Rows(I).Item("V_Date")), "Short Date")
                    DTReport.Rows(J)(Narration) = AgL.XNull(DTTemp.Rows(I).Item("Narration"))


                    If AgL.VNull(DTTemp.Rows(I).Item("SNo")) <> 0 Then
                        DTReport.Rows(J)(Debit) = IIf(AgL.VNull(DTTemp.Rows(I).Item("AmtDr")) > 0, Format(AgL.VNull(DTTemp.Rows(I).Item("AmtDr")), "0.00"), System.DBNull.Value)
                        DTReport.Rows(J)(Credit) = IIf(AgL.VNull(DTTemp.Rows(I).Item("AmtCr")) > 0, Format(AgL.VNull(DTTemp.Rows(I).Item("AmtCr")), "0.00"), System.DBNull.Value)
                        DblDebit_Total = DblDebit_Total + Format(AgL.VNull(DTTemp.Rows(I).Item("AmtDr")), "0.00")
                        DblCredit_Total = DblCredit_Total + Format(AgL.VNull(DTTemp.Rows(I).Item("AmtCr")), "0.00")
                    Else
                        DblOpening = IIf(AgL.VNull(DTTemp.Rows(I).Item("AmtDr")) > 0, Val(Format(AgL.VNull(DTTemp.Rows(I).Item("AmtDr")), "0.00")), 0 - Val(Format(AgL.VNull(DTTemp.Rows(I).Item("AmtCr")), "0.00")))
                    End If

                    DTReport.Rows(J)(Closing) = IIf((DblOpening + DblDebit_Total - DblCredit_Total) <> 0, Format(Math.Abs(DblOpening + DblDebit_Total - DblCredit_Total), "0.00"), System.DBNull.Value)

                    DTReport.Rows(J)(DR_CR_CL) = IIf((DblOpening + DblDebit_Total - DblCredit_Total) > 0, "Dr", "Cr")
                    If (DblOpening + DblDebit_Total - DblCredit_Total) = 0 Then DTReport.Rows(J)(DR_CR_CL) = ""

                    If ReportFrm.FGetText(rowShowContraAcInLedger) = "Yes" Then
                        If Trim(AgL.XNull(DTTemp.Rows(I).Item("ContraText"))) <> "" Then
                            DTReport.Rows.Add()
                            J = DTReport.Rows.Count - 1
                            DTReport.Rows(J)(Narration) = AgL.XNull(DTTemp.Rows(I).Item("ContraText"))
                        End If
                    End If
                Next

                'DTReport.Rows.Add()
                'DTReport.Rows.Add()
                'J = DTReport.Rows.Count - 1

                'DTReport.Rows(J)(Narration) = "Total"
                'DTReport.Rows(J)(Debit) = Format(DblDebit_Total, "0.00")
                'DTReport.Rows(J)(Credit) = Format(DblCredit_Total, "0.00")
                'DTReport.Rows(J)(Closing) = IIf((DblOpening + DblDebit_Total - DblCredit_Total) <> 0, Format(Math.Abs(DblOpening + DblDebit_Total - DblCredit_Total), "0.00"), "")
                'DTReport.Rows(J)(DR_CR_CL) = IIf((DblOpening + DblDebit_Total - DblCredit_Total) > 0, "Dr", "Cr")

                If DTReport.Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

                'ReportFrm.Text = "Financial Display - " + ReportFrm.FGetText(rowReportType)
                ReportFrm.Text = ReportFrm.FGetText(rowReportType)
                ReportFrm.ClsRep = Me
                ReportFrm.ReportProcName = "ProcFinancialDisplay"
                ReportFrm.IsHideZeroColumns = False



                DsReport = New DataSet()
                DsReport.Tables.Add(DTReport)


                ReportFrm.ProcFillGrid(DsReport)

                For I = 0 To ReportFrm.DGL1.Columns.Count - 1
                    ReportFrm.DGL1.Columns(I).SortMode = DataGridViewColumnSortMode.NotSortable
                    ReportFrm.DGL2.Item(I, 0).Value = ""
                Next I

                ReportFrm.DGL2.Item(ReportFrm.DGL1.Columns(Debit).Index, 0).Value = Format(DblDebit_Total, "0.00")
                ReportFrm.DGL2.Item(ReportFrm.DGL1.Columns(Credit).Index, 0).Value = Format(DblCredit_Total, "0.00")
                ReportFrm.DGL2.Item(ReportFrm.DGL1.Columns(Closing).Index, 0).Value = IIf((DblOpening + DblDebit_Total - DblCredit_Total) <> 0, Format(Math.Abs(DblOpening + DblDebit_Total - DblCredit_Total), "0.00"), "")
                ReportFrm.DGL2.Item(ReportFrm.DGL1.Columns(DR_CR_CL).Index, 0).Value = IIf((DblOpening + DblDebit_Total - DblCredit_Total) > 0, "Dr", "Cr")

                'If ReportFrm.FGetText(rowShowContraAcInLedger) = "Yes" Then
                '    ReportFrm.DGL1.DefaultCellStyle.Font = New Font("Courier New", 9, FontStyle.Italic)
                'End If

                'ReportFrm.DGL1.Columns(Narration).Width = 400
                'ReportFrm.DGL1.Columns(Debit).Width = 150
                'ReportFrm.DGL1.Columns(Credit).Width = 150
                'ReportFrm.DGL1.Columns(Closing).Width = 150
                'ReportFrm.DGL1.Columns(DR_CR_CL).Width = 30

                ReportFrm.DGL1.Columns(DR_CR_CL).HeaderText = ""
                ReportFrm.DGL1.Columns(Narration).Width = 265
                If ReportFrm.DGL1.Columns.Contains(Site) And Not ReportFrm.DGL1.Columns.Contains(Division) Then
                    ReportFrm.DGL1.Columns(Site).Width = 47
                End If
                If ReportFrm.DGL1.Columns.Contains(Division) And Not ReportFrm.DGL1.Columns.Contains(Site) Then
                    ReportFrm.DGL1.Columns(Division).Width = 47
                End If
                If ReportFrm.DGL1.Columns.Contains(Site) And ReportFrm.DGL1.Columns.Contains(Division) Then
                    ReportFrm.DGL1.Columns(Site).Width = 23
                    ReportFrm.DGL1.Columns(Division).Width = 24
                End If
                If Not ReportFrm.DGL1.Columns.Contains(Site) And Not ReportFrm.DGL1.Columns.Contains(Division) Then
                    ReportFrm.DGL1.Columns(Narration).Width = ReportFrm.DGL1.Columns(Narration).Width + 47
                End If
                ReportFrm.DGL1.Columns("No").Width = 61
                ReportFrm.DGL1.Columns("Type").Width = 49
                ReportFrm.DGL1.Columns("Date").Width = 112
                ReportFrm.DGL1.Columns(Debit).Width = 112
                ReportFrm.DGL1.Columns(Credit).Width = 112
                ReportFrm.DGL1.Columns(Closing).Width = 112
                ReportFrm.DGL1.Columns(DR_CR_CL).Width = 25


            End If


        Catch ex As Exception
            If Not ex.Message.Contains("Index was out Of range") Then
                MsgBox(ex.Message)
            End If
            ReportFrm.FProcessEscapeButton(False)
        End Try
    End Sub
    Private Sub FOpenForm(DocId As String)
        Dim FrmObjMDI As Object
        Dim FrmObj As Object
        Dim DtVType As DataTable
        Dim StrModuleName As String = ""
        Dim StrMnuName As String = ""
        Dim StrMnuText As String = ""

        Try
            DtVType = AgL.FillData("Select V_Type,MnuName,MnuText,MnuAttachedInModule From Voucher_Type Where IfNull(MnuName,'')<>'' And V_Type = '" & AgL.DeCodeDocID(DocId, AgLibrary.ClsMain.DocIdPart.VoucherType) & "' Order By V_Type", AgL.GCn).tables(0)
            If DtVType.Rows.Count > 0 Then
                StrModuleName = AgL.XNull(DtVType.Rows(0)("MnuAttachedInModule"))
                StrMnuName = AgL.XNull(DtVType.Rows(0)("MnuName"))
                StrMnuText = AgL.XNull(DtVType.Rows(0)("MnuText"))

                FrmObjMDI = ReportFrm.MdiParent
                FrmObj = FrmObjMDI.FOpenForm(StrModuleName, StrMnuName, StrMnuText)
                FrmObj.MdiParent = ReportFrm.MdiParent
                FrmObj.Show()
                FrmObj.FindMove(DocId)
                FrmObj = Nothing
            Else
                MsgBox("Define Details For This Voucher Type.")
            End If
            ReportFrm.FiterGridCopy_Arr.RemoveAt(ReportFrm.FiterGridCopy_Arr.Count - 1)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    'Private Sub FDisplay_Level_Group()
    '    Dim StrCondition1 As String = ""
    '    Dim StrSQLQuery As String = ""
    '    Dim DTTemp As DataTable
    '    Dim DblDebit_Total As Double, DblCredit_Total As Double
    '    Dim StrConditionZeroBal As String = ""
    '    Dim I As Integer

    '    Try
    '        FCreateDataTable(ReportType.GroupBalance)

    '        If UCase(ReportFrm.FGetText(rowShowZeroBalance)) = "N" Then StrConditionZeroBal = "Having (Round(IfNull(Sum(LG.AmtDr),0),2)-Round(IfNull(Sum(LG.AmtCr),0),2)) <> 0 "
    '        StrCondition1 = " Where Date(LG.V_Date) <= " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowToDate)).ToString("s")) & " "
    '        StrCondition1 += Replace(ReportFrm.GetWhereCondition("LG.Site_Code", rowSite), "''", "'")
    '        StrCondition1 += Replace(ReportFrm.GetWhereCondition("LG.DivCode", rowDivision), "''", "'")
    '        StrCondition1 += " And Date(LG.V_Date) >= (Case When Ag.GroupNature In ('R','E') 
    '                        Then " & AgL.Chk_Date(CDate(AgL.PubStartDate).ToString("s")) & " Else '1900/01/01' End) "
    '        If ReportFrm.FGetText(rowIncludeOpening) = "No" Then
    '            StrCondition1 += " And Date(LG.V_Date) >= " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowFromDate)).ToString("s")) & " "
    '        End If


    '        '========== For Detail Section =======
    '        StrSQLQuery = "Select	(Case When IfNull(AG1.GroupCode,'')<>'' Then 'A+' || IfNull(AG1.GroupCode,'') "
    '        StrSQLQuery += "When IfNull(AG.GroupUnder,'')='" & ReportFrm.FGetCode(rowOtherFilter) & "' Then 'A+' || IfNull(AG.GroupCode,'') "
    '        StrSQLQuery += "Else 'S+' || IfNull(SG.SubCode,'')  End)  As GroupCode, "
    '        StrSQLQuery += "Max(Case When IfNull(AG1.GroupCode,'')<>'' Then IfNull(AG1.GroupName,'') "
    '        StrSQLQuery += "When IfNull(AG.GroupUnder,'')='" & ReportFrm.FGetCode(rowOtherFilter) & "' Then IfNull(AG.GroupName,'') "
    '        StrSQLQuery += "Else IfNull(SG.Name,'') || ' - ' || IfNull(CT.CityName,'') End)  As GName, "
    '        StrSQLQuery += "(Case When (IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LG.AmtCr),0))>0 Then  "
    '        StrSQLQuery += "(IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LG.AmtCr),0)) Else 0.00 End) As AmtDr, "
    '        StrSQLQuery += "(Case When (IfNull(Sum(LG.AmtCr),0)-IfNull(Sum(LG.AmtDr),0))>0 Then "
    '        StrSQLQuery += "(IfNull(Sum(LG.AmtCr),0)-IfNull(Sum(LG.AmtDr),0)) Else 0.00 End) As AmtCr "
    '        StrSQLQuery += "From Ledger LG Left Join SubGroup SG On LG.SubCode=SG.SubCode  Left Join "
    '        StrSQLQuery += "City CT On CT.CityCode=SG.CityCode Left Join "
    '        StrSQLQuery += "AcGroup AG On AG.GroupCode=SG.GroupCode Left Join "
    '        StrSQLQuery += "AcGroupPath AGP On AGP.GroupCode=AG.GroupCode And AGP.SNo=" & IntLevel & " Left Join "
    '        StrSQLQuery += "AcGroup AG1 On AG1.GroupCode=AGP.GroupUnder "
    '        StrSQLQuery += StrCondition1
    '        StrSQLQuery += "And (AG.GroupCode In "
    '        StrSQLQuery += "(Select GroupCode From AcGroupPath AGP Where AGP.GroupUnder='" & ReportFrm.FGetCode(rowOtherFilter) & "') "
    '        StrSQLQuery += "Or AG.GroupCode='" & ReportFrm.FGetCode(rowOtherFilter) & "') "

    '        StrSQLQuery += "Group By (Case When IfNull(AG1.GroupCode,'')<>'' Then 'A+' || IfNull(AG1.GroupCode,'') "
    '        StrSQLQuery += "When IfNull(AG.GroupUnder,'')='" & ReportFrm.FGetCode(rowOtherFilter) & "' Then 'A+' || IfNull(AG.GroupCode,'') "
    '        StrSQLQuery += "Else 'S+' || IfNull(SG.SubCode,'')  End) "

    '        StrSQLQuery += StrConditionZeroBal

    '        StrSQLQuery += "Order By Max(Case When IfNull(AG1.GroupCode,'')<>'' Then IfNull(AG1.GroupName,'') "
    '        StrSQLQuery += "When IfNull(AG.GroupUnder,'')='" & ReportFrm.FGetCode(rowOtherFilter) & "' Then IfNull(AG.GroupName,'') "
    '        StrSQLQuery += "Else IfNull(SG.Name,'') || ' - ' || IfNull(CT.CityName,'') End) "

    '        DTTemp = AgL.FillData(StrSQLQuery, AgL.GCn).tables(0)


    '        DblDebit_Total = 0
    '        DblCredit_Total = 0
    '        For I = 0 To DTTemp.Rows.Count - 1
    '            DTReport.Rows.Add()
    '            DTReport.Rows(I)(GRCode) = Mid(AgL.XNull(DTTemp.Rows(I).Item("GroupCode")), 3, Len(AgL.XNull(DTTemp.Rows(I).Item("GroupCode"))))
    '            DTReport.Rows(I)(GRName) = AgL.XNull(DTTemp.Rows(I).Item("GName"))
    '            DTReport.Rows(I)(Debit) = IIf(AgL.VNull(DTTemp.Rows(I).Item("AmtDr")) > 0, Format(AgL.VNull(DTTemp.Rows(I).Item("AmtDr")), "0.00"), DBNull.Value)
    '            DTReport.Rows(I)(Credit) = IIf(AgL.VNull(DTTemp.Rows(I).Item("AmtCr")) > 0, Format(AgL.VNull(DTTemp.Rows(I).Item("AmtCr")), "0.00"), DBNull.Value)
    '            DblDebit_Total = DblDebit_Total + Format(AgL.VNull(DTTemp.Rows(I).Item("AmtDr")), "0.00")
    '            DblCredit_Total = DblCredit_Total + Format(AgL.VNull(DTTemp.Rows(I).Item("AmtCr")), "0.00")
    '        Next

    '        ReportFrm.Text = "Financial Display - " + ReportFrm.FGetText(rowReportType)
    '        ReportFrm.ClsRep = Me
    '        ReportFrm.ReportProcName = "ProcFinancialDisplay"
    '        ReportFrm.IsManualAggregate = False

    '        DsReport = New DataSet()
    '        DsReport.Tables.Add(DTReport)
    '        ReportFrm.ProcFillGrid(DsReport)

    '        ReportFrm.DGL1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None

    '        ReportFrm.DGL1.Columns(IsAccountGroup).Visible = False

    '        ReportFrm.DGL1.Columns(GRName).Width = 500
    '        'ReportFrm.DGL1.Columns(Opening).Width = 100
    '        'ReportFrm.DGL1.Columns(DR_CR_OP).Width = 20
    '        'ReportFrm.DGL1.Columns(DR_CR_OP).HeaderText = ""
    '        ReportFrm.DGL1.Columns(Debit).Width = 300
    '        ReportFrm.DGL1.Columns(Credit).Width = 300
    '        'ReportFrm.DGL1.Columns(Closing).Width = 100
    '        'ReportFrm.DGL1.Columns(DR_CR_CL).Width = 20
    '        'ReportFrm.DGL1.Columns(DR_CR_CL).HeaderText = ""
    '    Catch ex As Exception
    '        If Not ex.Message.Contains("Index was out of range") Then
    '            MsgBox(ex.Message)
    '        End If
    '    End Try
    'End Sub
    Private Sub FCreateDataTable(ByVal DspType As String)
        DTReport = New DataTable
        DTReport.Rows.Clear()
        DTReport.Columns.Clear()

        If DspType = ReportType.Ledger Then
            'DTReport.Columns.Add(GRCode)
            DTReport.Columns.Add(DocId)
            'DTReport.Columns.Add(GRName)
            If AgL.PubDivisionCount > 1 Then
                DTReport.Columns.Add(Division)
                'DTReport.Columns.Add(LGRCode)
            End If
            If AgL.PubSiteCount > 1 Then
                DTReport.Columns.Add(Site)
            End If
            DTReport.Columns.Add(VNo)
            DTReport.Columns.Add(VType)
            DTReport.Columns.Add(VDate)
            DTReport.Columns.Add(Narration)
            'DTReport.Columns.Add(Opening)
            DTReport.Columns.Add(Debit)
            DTReport.Columns(Debit).DataType = GetType(Double)
            'DTReport.Columns.Add(GRCodeCredit)
            'DTReport.Columns.Add(GRNameCredit)
            DTReport.Columns.Add(Credit)
            DTReport.Columns(Credit).DataType = GetType(Double)
            'DTReport.Columns.Add(Balance)
            'DTReport.Columns(Balance).DataType = GetType(Double)
            'DTReport.Columns.Add(GR_SG)
            DTReport.Columns.Add(Closing)
            DTReport.Columns(Closing).DataType = GetType(Double)
            DTReport.Columns.Add(DR_CR_CL)
        ElseIf DspType = ReportType.Ledger_MonthWise Then
            DTReport.Columns.Add(Month)
            DTReport.Columns.Add(GRCode)
            'DTReport.Columns.Add(DocId)
            DTReport.Columns.Add(GRName)
            'DTReport.Columns.Add(VNo)
            'DTReport.Columns.Add(VType)
            'DTReport.Columns.Add(VDate)
            'DTReport.Columns.Add(Narration)
            'DTReport.Columns.Add(Opening)
            DTReport.Columns.Add(Debit)
            DTReport.Columns(Debit).DataType = GetType(Double)
            'DTReport.Columns.Add(GRCodeCredit)
            'DTReport.Columns.Add(GRNameCredit)
            DTReport.Columns.Add(Credit)
            DTReport.Columns(Credit).DataType = GetType(Double)
            'DTReport.Columns.Add(Balance)
            'DTReport.Columns(Balance).DataType = GetType(Double)
            'DTReport.Columns.Add(GR_SG)
            DTReport.Columns.Add(Closing)
            DTReport.Columns(Closing).DataType = GetType(Double)
            DTReport.Columns.Add(DR_CR_CL)
        ElseIf DspType = ReportType.Ledger_VoucherTypeWise Then
            DTReport.Columns.Add(VType)
            DTReport.Columns.Add(VoucherTypeDesc)
            DTReport.Columns.Add(GRCode)
            'DTReport.Columns.Add(DocId)
            DTReport.Columns.Add(GRName)
            'DTReport.Columns.Add(VNo)
            'DTReport.Columns.Add(VType)
            'DTReport.Columns.Add(VDate)
            'DTReport.Columns.Add(Narration)
            'DTReport.Columns.Add(Opening)
            DTReport.Columns.Add(Debit)
            DTReport.Columns(Debit).DataType = GetType(Double)
            'DTReport.Columns.Add(GRCodeCredit)
            'DTReport.Columns.Add(GRNameCredit)
            DTReport.Columns.Add(Credit)
            DTReport.Columns(Credit).DataType = GetType(Double)
            'DTReport.Columns.Add(Balance)
            'DTReport.Columns(Balance).DataType = GetType(Double)
            'DTReport.Columns.Add(GR_SG)
            DTReport.Columns.Add(Closing)
            DTReport.Columns(Closing).DataType = GetType(Double)
            DTReport.Columns.Add(DR_CR_CL)
        ElseIf DspType = ReportType.GroupBalance Or DspType = ReportType.TrialBalance Then
            DTReport.Columns.Add(GRCode)
            'DTReport.Columns.Add(DocId)
            DTReport.Columns.Add(GRName)
            'DTReport.Columns.Add(VNo)
            'DTReport.Columns.Add(VType)
            'DTReport.Columns.Add(VDate)
            'DTReport.Columns.Add(Narration)
            'DTReport.Columns.Add(Opening)
            DTReport.Columns.Add(Debit)
            DTReport.Columns(Debit).DataType = GetType(Double)
            'DTReport.Columns.Add(GRCodeCredit)
            'DTReport.Columns.Add(GRNameCredit)
            DTReport.Columns.Add(Credit)
            DTReport.Columns(Credit).DataType = GetType(Double)
            'DTReport.Columns.Add(Closing)
            'DTReport.Columns.Add(GR_SG)
            DTReport.Columns.Add(AcGroupCode)
            DTReport.Columns.Add(IsAccountGroup)
        ElseIf DspType = ReportType.DetailTrialBalance Then
            DTReport.Columns.Add(GRCode)
            'DTReport.Columns.Add(DocId)
            DTReport.Columns.Add(GRName)
            If ReportFrm.FGetText(rowAddColumn).ToString.Contains(AddColumn.LinkedSubCode) Then
                DTReport.Columns.Add(LGRName)
                DTReport.Columns.Add(LGRCode)
            End If
            If ReportFrm.FGetText(rowAddColumn).ToString.Contains(AddColumn.AccountGroup) Then
                DTReport.Columns.Add(LGroupName)
            End If
            If ReportFrm.FGetText(rowAddColumn).ToString.Contains(AddColumn.AccountType) Then
                DTReport.Columns.Add(LAccountType)
            End If
            'DTReport.Columns.Add(VNo)
            'DTReport.Columns.Add(VType)
            'DTReport.Columns.Add(VDate)
            'DTReport.Columns.Add(Narration)
            DTReport.Columns.Add(Opening)
            DTReport.Columns(Opening).DataType = GetType(Double)
            DTReport.Columns.Add(DR_CR_OP)
            DTReport.Columns.Add(Debit)
            DTReport.Columns(Debit).DataType = GetType(Double)
            'DTReport.Columns.Add(GRCodeCredit)
            'DTReport.Columns.Add(GRNameCredit)
            DTReport.Columns.Add(Credit)
            DTReport.Columns(Credit).DataType = GetType(Double)
            DTReport.Columns.Add(Closing)
            DTReport.Columns(Closing).DataType = GetType(Double)
            DTReport.Columns.Add(DR_CR_CL)
            'DTReport.Columns.Add(GR_SG)
        ElseIf DspType = ReportType.BalanceSheet Then
            DTReport.Columns.Add(GRCode)
            'DTReport.Columns.Add(DocId)
            DTReport.Columns.Add(GRName)
            'DTReport.Columns.Add(VNo)
            'DTReport.Columns.Add(VType)
            'DTReport.Columns.Add(VDate)
            'DTReport.Columns.Add(Narration)
            'DTReport.Columns.Add(Opening)
            DTReport.Columns.Add(Debit)
            DTReport.Columns(Debit).DataType = GetType(Double)
            DTReport.Columns.Add(GRCodeCredit)
            DTReport.Columns.Add(GRNameCredit)
            DTReport.Columns.Add(Credit)
            DTReport.Columns(Credit).DataType = GetType(Double)
            DTReport.Columns.Add(IsAccountGroup)
            DTReport.Columns.Add(IsAccountGroupCredit)
            'DTReport.Columns.Add(Closing)
            'DTReport.Columns.Add(GR_SG)
        ElseIf DspType = ReportType.ProfitAndLoss Then
            DTReport.Columns.Add(GRCode)
            'DTReport.Columns.Add(DocId)
            DTReport.Columns.Add(GRName)
            'DTReport.Columns.Add(VNo)
            'DTReport.Columns.Add(VType)
            'DTReport.Columns.Add(VDate)
            'DTReport.Columns.Add(Narration)
            'DTReport.Columns.Add(Opening)
            DTReport.Columns.Add(Debit)
            DTReport.Columns(Debit).DataType = GetType(Double)
            DTReport.Columns.Add(GRCodeCredit)
            DTReport.Columns.Add(GRNameCredit)
            DTReport.Columns.Add(Credit)
            DTReport.Columns(Credit).DataType = GetType(Double)
            DTReport.Columns.Add(IsAccountGroup)
            DTReport.Columns.Add(IsAccountGroupCredit)
            'DTReport.Columns.Add(Closing)
            'DTReport.Columns.Add(GR_SG)
        End If
    End Sub
    Private Sub ObjRepFormGlobal_FilterApplied() Handles ReportFrm.FilterApplied
        Select Case ReportFrm.FGetText(rowReportType)
            Case ReportType.DetailTrialBalance
                FormatDetailTrialBalance(FormattingOn.OnFilter)
            Case ReportType.TrialBalance
                FormatTrialBalance(FormattingOn.OnFilter)
            Case ReportType.BalanceSheet
                FormatBalanceSheet(FormattingOn.OnFilter)
            Case ReportType.ProfitAndLoss
                FormatProfitAndLoss(FormattingOn.OnFilter)
        End Select


    End Sub
    Private Sub FormatDetailTrialBalance(bFormatOn As FormattingOn)
        Dim OpeningTotal As Double, ClosingTotal As Double
        If bFormatOn = FormattingOn.OnFilter Then
            If ReportFrm.DGL2.ColumnCount = ReportFrm.DGL1.ColumnCount Then
                For I As Integer = 0 To ReportFrm.DGL1.Rows.Count - 1
                    'OpeningTotal += IIf(ReportFrm.DGL1.Item(DR_CR_OP, I).Value = "Cr", (-AgL.VNull(ReportFrm.DGL1.Item(Opening, I).Value)), AgL.VNull(ReportFrm.DGL1.Item(Opening, I).Value))
                    'ClosingTotal += IIf(ReportFrm.DGL1.Item(DR_CR_CL, I).Value = "Cr", (-AgL.VNull(ReportFrm.DGL1.Item(Closing, I).Value)), AgL.VNull(ReportFrm.DGL1.Item(Closing, I).Value))

                    If ReportFrm.DGL1.Rows(I).Visible = True Then
                        If AgL.XNull(ReportFrm.DGL1.Item(Opening, I).Value) <> "" Then
                            OpeningTotal += IIf(ReportFrm.DGL1.Item(DR_CR_OP, I).Value = "Cr", (-Val(AgL.XNull(ReportFrm.DGL1.Item(Opening, I).Value))), Val(AgL.XNull(ReportFrm.DGL1.Item(Opening, I).Value)))
                        End If
                        If AgL.XNull(ReportFrm.DGL1.Item(Closing, I).Value) <> "" Then
                            ClosingTotal += IIf(ReportFrm.DGL1.Item(DR_CR_CL, I).Value = "Cr", (-Val(AgL.XNull(ReportFrm.DGL1.Item(Closing, I).Value))), Val(AgL.XNull(ReportFrm.DGL1.Item(Closing, I).Value)))
                        End If
                    End If
                Next
                ReportFrm.DGL2.Item(ReportFrm.DGL1.Columns(Opening).Index, 0).Value = Format(Math.Abs(OpeningTotal), "0.00")
                ReportFrm.DGL2.Item(ReportFrm.DGL1.Columns(Closing).Index, 0).Value = Format(Math.Abs(ClosingTotal), "0.00")

                If OpeningTotal > 0 Then
                    ReportFrm.DGL2.Item(ReportFrm.DGL1.Columns(DR_CR_OP).Index, 0).Value = "Dr"
                ElseIf OpeningTotal < 0 Then
                    ReportFrm.DGL2.Item(ReportFrm.DGL1.Columns(DR_CR_OP).Index, 0).Value = "Cr"
                End If

                If ClosingTotal > 0 Then
                    ReportFrm.DGL2.Item(ReportFrm.DGL1.Columns(DR_CR_CL).Index, 0).Value = "Dr"
                ElseIf OpeningTotal < 0 Then
                    ReportFrm.DGL2.Item(ReportFrm.DGL1.Columns(DR_CR_CL).Index, 0).Value = "Cr"
                End If
            End If
        End If
    End Sub
    Private Sub FormatTrialBalance(bFormatOn As FormattingOn)
        For I As Integer = 0 To ReportFrm.DGL1.Rows.Count - 1
            If AgL.StrCmp(AgL.XNull(ReportFrm.DGL1.Item(IsAccountGroup, I).Value), "Yes") Then
                For J As Integer = 0 To ReportFrm.DGL1.Columns.Count - 1
                    ReportFrm.DGL1.Item(J, I).Style.Font = New Font("Verdana", 9, FontStyle.Bold)
                Next
            End If
        Next
    End Sub
    Private Sub FormatBalanceSheet(bFormatOn As FormattingOn)
        For I As Integer = 0 To ReportFrm.DGL1.Rows.Count - 1
            If AgL.StrCmp(AgL.XNull(ReportFrm.DGL1.Item(IsAccountGroup, I).Value), "Yes") Then
                'ReportFrm.DGL1.Rows(I).DefaultCellStyle.Font = New Font("Verdana", 9, FontStyle.Bold)
                ReportFrm.DGL1.Item(GRName, I).Style.Font = New Font("Verdana", 9, FontStyle.Bold)
                ReportFrm.DGL1.Item(Debit, I).Style.Font = New Font("Verdana", 9, FontStyle.Bold)
            End If

            If AgL.StrCmp(AgL.XNull(ReportFrm.DGL1.Item(IsAccountGroupCredit, I).Value), "Yes") Then
                ReportFrm.DGL1.Item(GRNameCredit, I).Style.Font = New Font("Verdana", 9, FontStyle.Bold)
                ReportFrm.DGL1.Item(Credit, I).Style.Font = New Font("Verdana", 9, FontStyle.Bold)
            End If

            If AgL.StrCmp(AgL.XNull(ReportFrm.DGL1.Item(GRNameCredit, I).Value), "Difference In Trial Balance") Or
                    AgL.StrCmp(AgL.XNull(ReportFrm.DGL1.Item(GRNameCredit, I).Value), "Net Profit") Or
                    AgL.StrCmp(AgL.XNull(ReportFrm.DGL1.Item(GRNameCredit, I).Value), "Net Loss") Or
                    AgL.StrCmp(AgL.XNull(ReportFrm.DGL1.Item(GRNameCredit, I).Value), "Opening Stock") Or
                    AgL.StrCmp(AgL.XNull(ReportFrm.DGL1.Item(GRNameCredit, I).Value), "Closing Stock") Then
                ReportFrm.DGL1.Item(GRNameCredit, I).Style.Font = New Font("Verdana", 9, FontStyle.Bold)
                ReportFrm.DGL1.Item(Credit, I).Style.Font = New Font("Verdana", 9, FontStyle.Bold)
            End If

            If AgL.StrCmp(AgL.XNull(ReportFrm.DGL1.Item(GRName, I).Value), "Difference In Trial Balance") Or
                    AgL.StrCmp(AgL.XNull(ReportFrm.DGL1.Item(GRName, I).Value), "Net Profit") Or
                    AgL.StrCmp(AgL.XNull(ReportFrm.DGL1.Item(GRName, I).Value), "Net Loss") Or
                    AgL.StrCmp(AgL.XNull(ReportFrm.DGL1.Item(GRName, I).Value), "Opening Stock") Or
                    AgL.StrCmp(AgL.XNull(ReportFrm.DGL1.Item(GRName, I).Value), "Closing Stock") Then
                ReportFrm.DGL1.Item(GRName, I).Style.Font = New Font("Verdana", 9, FontStyle.Bold)
                ReportFrm.DGL1.Item(Debit, I).Style.Font = New Font("Verdana", 9, FontStyle.Bold)
            End If

            If AgL.XNull(ReportFrm.DGL1.Item(GRNameCredit, I).Value).ToString.StartsWith("Opening Stock") Or
                    AgL.XNull(ReportFrm.DGL1.Item(GRNameCredit, I).Value).ToString.StartsWith("Closing Stock") Then
                ReportFrm.DGL1.Item(GRNameCredit, I).Style.Font = New Font("Verdana", 9, FontStyle.Bold)
                ReportFrm.DGL1.Item(Credit, I).Style.Font = New Font("Verdana", 9, FontStyle.Bold)
            End If

            If AgL.XNull(ReportFrm.DGL1.Item(GRName, I).Value).ToString.StartsWith("Opening Stock") Or
                    AgL.XNull(ReportFrm.DGL1.Item(GRName, I).Value).ToString.StartsWith("Closing Stock") Then
                ReportFrm.DGL1.Item(GRName, I).Style.Font = New Font("Verdana", 9, FontStyle.Bold)
                ReportFrm.DGL1.Item(Debit, I).Style.Font = New Font("Verdana", 9, FontStyle.Bold)
            End If
        Next
    End Sub
    Private Sub FormatProfitAndLoss(bFormatOn As FormattingOn)
        'For I As Integer = 0 To ReportFrm.DGL1.Rows.Count - 1
        '    ReportFrm.DGL1.Rows(I).DefaultCellStyle.Font = New Font("Verdana", 9, FontStyle.Bold)
        '    If AgL.XNull(ReportFrm.DGL1.Item(GRName, I).Value) = "Gross Profit" Then
        '        ReportFrm.DGL1.Rows(I + 2).DefaultCellStyle.BackColor = Color.LightGray
        '    End If
        'Next

        For I As Integer = 0 To ReportFrm.DGL1.Rows.Count - 1
            If AgL.StrCmp(AgL.XNull(ReportFrm.DGL1.Item(IsAccountGroup, I).Value), "Yes") Then
                'ReportFrm.DGL1.Rows(I).DefaultCellStyle.Font = New Font("Verdana", 9, FontStyle.Bold)
                ReportFrm.DGL1.Item(GRName, I).Style.Font = New Font("Verdana", 9, FontStyle.Bold)
                ReportFrm.DGL1.Item(Debit, I).Style.Font = New Font("Verdana", 9, FontStyle.Bold)
            End If

            If AgL.StrCmp(AgL.XNull(ReportFrm.DGL1.Item(IsAccountGroupCredit, I).Value), "Yes") Then
                ReportFrm.DGL1.Item(GRNameCredit, I).Style.Font = New Font("Verdana", 9, FontStyle.Bold)
                ReportFrm.DGL1.Item(Credit, I).Style.Font = New Font("Verdana", 9, FontStyle.Bold)
            End If

            If AgL.StrCmp(AgL.XNull(ReportFrm.DGL1.Item(GRNameCredit, I).Value), "Net Profit") Or
                    AgL.StrCmp(AgL.XNull(ReportFrm.DGL1.Item(GRNameCredit, I).Value), "Net Loss") Or
                    AgL.StrCmp(AgL.XNull(ReportFrm.DGL1.Item(GRNameCredit, I).Value), "Gross Profit") Or
                    AgL.StrCmp(AgL.XNull(ReportFrm.DGL1.Item(GRNameCredit, I).Value), "Gross Loss") Or
                    AgL.StrCmp(AgL.XNull(ReportFrm.DGL1.Item(GRNameCredit, I).Value), "Opening Stock") Or
                    AgL.StrCmp(AgL.XNull(ReportFrm.DGL1.Item(GRNameCredit, I).Value), "Closing Stock") Or
                    AgL.StrCmp(AgL.XNull(ReportFrm.DGL1.Item(GRNameCredit, I).Value), "") Then
                ReportFrm.DGL1.Item(GRNameCredit, I).Style.Font = New Font("Verdana", 9, FontStyle.Bold)
                ReportFrm.DGL1.Item(Credit, I).Style.Font = New Font("Verdana", 9, FontStyle.Bold)
            End If

            If AgL.StrCmp(AgL.XNull(ReportFrm.DGL1.Item(GRName, I).Value), "Net Profit") Or
                    AgL.StrCmp(AgL.XNull(ReportFrm.DGL1.Item(GRName, I).Value), "Net Loss") Or
                    AgL.StrCmp(AgL.XNull(ReportFrm.DGL1.Item(GRName, I).Value), "Gross Profit") Or
                    AgL.StrCmp(AgL.XNull(ReportFrm.DGL1.Item(GRName, I).Value), "Gross Loss") Or
                    AgL.StrCmp(AgL.XNull(ReportFrm.DGL1.Item(GRName, I).Value), "Opening Stock") Or
                    AgL.StrCmp(AgL.XNull(ReportFrm.DGL1.Item(GRName, I).Value), "Closing Stock") Or
                    AgL.StrCmp(AgL.XNull(ReportFrm.DGL1.Item(GRName, I).Value), "") Then
                ReportFrm.DGL1.Item(GRName, I).Style.Font = New Font("Verdana", 9, FontStyle.Bold)
                ReportFrm.DGL1.Item(Debit, I).Style.Font = New Font("Verdana", 9, FontStyle.Bold)
            End If

            If AgL.XNull(ReportFrm.DGL1.Item(GRNameCredit, I).Value).ToString.StartsWith("Opening Stock") Or
                    AgL.XNull(ReportFrm.DGL1.Item(GRNameCredit, I).Value).ToString.StartsWith("Closing Stock") Then
                ReportFrm.DGL1.Item(GRNameCredit, I).Style.Font = New Font("Verdana", 9, FontStyle.Bold)
                ReportFrm.DGL1.Item(Credit, I).Style.Font = New Font("Verdana", 9, FontStyle.Bold)
            End If

            If AgL.XNull(ReportFrm.DGL1.Item(GRName, I).Value).ToString.StartsWith("Opening Stock") Or
                    AgL.XNull(ReportFrm.DGL1.Item(GRName, I).Value).ToString.StartsWith("Closing Stock") Then
                ReportFrm.DGL1.Item(GRName, I).Style.Font = New Font("Verdana", 9, FontStyle.Bold)
                ReportFrm.DGL1.Item(Debit, I).Style.Font = New Font("Verdana", 9, FontStyle.Bold)
            End If
        Next
    End Sub
    Private Sub ReportFrm_FormatFilterDisplayGrid() Handles ReportFrm.FormatFilterDisplayGrid
        Dim bMainFilterColumnIndex As Integer = 0
        If ReportFrm.FilterGridDisplay.Columns.Count > 0 Then
            For I As Integer = 0 To ReportFrm.FilterGridDisplay.Columns.Count - 1
                If AgL.XNull(ReportFrm.FilterGridDisplay.Item(I, 0).Value).ToString.Contains("Ledger Account :") Or
                    AgL.XNull(ReportFrm.FilterGridDisplay.Item(I, 0).Value).ToString.Contains("Ledger Account Group :") Then
                    bMainFilterColumnIndex = I
                    Exit For
                End If
            Next
            ReportFrm.FilterGridDisplay.Columns(bMainFilterColumnIndex).DisplayIndex = 0
        End If
    End Sub
    Private Sub ReportFrm_Shown(sender As Object, e As EventArgs) Handles ReportFrm.Shown
        If ReportFrm.FGetText(rowReportType) <> "Ledger" Then
            ProcFinancialDisplay()
        End If
    End Sub
    Private Function FRetDataDisplay_Level_Group(Optional GroupCode As String = "") As DataTable
        Dim StrCondition1 As String = ""
        Dim StrSQLQuery As String = ""
        Dim DTTemp As DataTable
        Dim DblDebit_Total As Double, DblCredit_Total As Double
        Dim StrConditionZeroBal As String = ""
        Dim I As Integer
        Dim mExcludeLedgerAccountsFromTrial As String = ""

        FCreateDataTable(ReportType.GroupBalance)


        mExcludeLedgerAccountsFromTrial = ClsMain.FGetSettings(SettingFields.ExcludeLedgerAccountsFromTrial, SettingType.General, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
        If UCase(ReportFrm.FGetText(rowShowZeroBalance)) = "NO" Then StrConditionZeroBal = "Having (Round(IfNull(Sum(LG.AmtDr),0),2)-Round(IfNull(Sum(LG.AmtCr),0),2)) <> 0 "
        StrCondition1 = " Where Date(LG.V_Date) <= " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowToDate)).ToString("s")) & " "
        StrCondition1 += Replace(ReportFrm.GetWhereCondition("LG.Site_Code", rowSite), "''", "'")
        StrCondition1 += Replace(ReportFrm.GetWhereCondition("LG.DivCode", rowDivision), "''", "'")
        StrCondition1 += " And Date(LG.V_Date) >= (Case When Ag.GroupNature In ('R','E') 
                            Then " & AgL.Chk_Date(CDate(AgL.PubStartDate).ToString("s")) & " Else '1900/01/01' End) "
        If ReportFrm.FGetText(rowIncludeOpening) = "No" Then
            StrCondition1 += " And Date(LG.V_Date) >= " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowFromDate)).ToString("s")) & " "
        End If
        If AgL.XNull(ReportFrm.FGetText(rowSubgroupNature)) <> "" Then
            StrCondition1 += " And SG.Nature = " & AgL.Chk_Text(AgL.XNull(ReportFrm.FGetText(rowSubgroupNature))) & " "
        End If

        If mExcludeLedgerAccountsFromTrial <> "" Then
            StrCondition1 += " And IfNull(Sg.Status,'Active') Not In ('" & mExcludeLedgerAccountsFromTrial.Replace("+", "','") & "') "
        End If

        '========== For Detail Section =======
        StrSQLQuery = "Select Max(AG.GroupCode) As AcGroupCode,	(Case When IfNull(AG1.GroupCode,'')<>'' Then 'A+' || IfNull(AG1.GroupCode,'') "
        If GroupCode <> "" Then
            StrSQLQuery += "When IfNull(AG.GroupUnder,'')='" & GroupCode & "' Then 'A+' || IfNull(AG.GroupCode,'') "
        End If
        StrSQLQuery += "Else 'S+' || IfNull(SG.SubCode,'')  End)  As GroupCode, "

        StrSQLQuery += "Max(Case When IfNull(AG1.GroupCode,'')<>'' Then 'Yes' "
        If GroupCode <> "" Then
            StrSQLQuery += "When IfNull(AG.GroupUnder,'')='" & GroupCode & "' Then 'Yes' "
        End If
        StrSQLQuery += "Else 'No'  End)  As IsAccountGroup, "

        StrSQLQuery += "Max(Case When IfNull(AG1.GroupCode,'')<>'' Then IfNull(AG1.GroupName,'') "
        If GroupCode <> "" Then
            StrSQLQuery += "When IfNull(AG.GroupUnder,'')='" & GroupCode & "' Then IfNull(AG.GroupName,'') "
        End If
        StrSQLQuery += "Else IfNull(SG.Name,'') || ' - ' || IfNull(CT.CityName,'') End)  As GName, "
        StrSQLQuery += "(Case When (IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LG.AmtCr),0))>0 Then  "
        StrSQLQuery += "(IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LG.AmtCr),0)) Else 0.00 End) As AmtDr, "
        StrSQLQuery += "(Case When (IfNull(Sum(LG.AmtCr),0)-IfNull(Sum(LG.AmtDr),0))>0 Then "
        StrSQLQuery += "(IfNull(Sum(LG.AmtCr),0)-IfNull(Sum(LG.AmtDr),0)) Else 0.00 End) As AmtCr "
        StrSQLQuery += "From Ledger LG Left Join SubGroup SG On LG.SubCode=SG.SubCode  Left Join "
        StrSQLQuery += "City CT On CT.CityCode=SG.CityCode Left Join "
        StrSQLQuery += "AcGroup AG On AG.GroupCode=SG.GroupCode Left Join "
        StrSQLQuery += "AcGroupPath AGP On AGP.GroupCode=AG.GroupCode And AGP.SNo=" & IntLevel & " Left Join "
        StrSQLQuery += "AcGroup AG1 On AG1.GroupCode=AGP.GroupUnder "
        StrSQLQuery += StrCondition1

        If GroupCode <> "" Then
            StrSQLQuery += "And (AG.GroupCode In "
            StrSQLQuery += "(Select GroupCode From AcGroupPath AGP Where AGP.GroupUnder='" & GroupCode & "') "
            StrSQLQuery += "Or AG.GroupCode='" & GroupCode & "') "
        End If


        StrSQLQuery += "Group By (Case When IfNull(AG1.GroupCode,'')<>'' Then 'A+' || IfNull(AG1.GroupCode,'') "
        If GroupCode <> "" Then
            StrSQLQuery += "When IfNull(AG.GroupUnder,'')='" & GroupCode & "' Then 'A+' || IfNull(AG.GroupCode,'') "
        End If
        StrSQLQuery += "Else 'S+' || IfNull(SG.SubCode,'')  End) "

        StrSQLQuery += StrConditionZeroBal

        StrSQLQuery += "Order By Max(Case When IfNull(AG1.GroupCode,'')<>'' Then IfNull(AG1.GroupName,'') "
        If GroupCode <> "" Then
            StrSQLQuery += "When IfNull(AG.GroupUnder,'')='" & GroupCode & "' Then IfNull(AG.GroupName,'') "
        End If
        StrSQLQuery += "Else IfNull(SG.Name,'') || ' - ' || IfNull(CT.CityName,'') End) "

        DTTemp = AgL.FillData(StrSQLQuery, AgL.GCn).tables(0)


        DblDebit_Total = 0
        DblCredit_Total = 0
        For I = 0 To DTTemp.Rows.Count - 1
            DTReport.Rows.Add()
            DTReport.Rows(I)(AcGroupCode) = AgL.XNull(DTTemp.Rows(I).Item("AcGroupCode"))
            DTReport.Rows(I)(GRCode) = Mid(AgL.XNull(DTTemp.Rows(I).Item("GroupCode")), 3, Len(AgL.XNull(DTTemp.Rows(I).Item("GroupCode"))))
            DTReport.Rows(I)(GRName) = AgL.XNull(DTTemp.Rows(I).Item("GName"))
            DTReport.Rows(I)(Debit) = IIf(AgL.VNull(DTTemp.Rows(I).Item("AmtDr")) > 0, Format(AgL.VNull(DTTemp.Rows(I).Item("AmtDr")), "0.00"), DBNull.Value)
            DTReport.Rows(I)(Credit) = IIf(AgL.VNull(DTTemp.Rows(I).Item("AmtCr")) > 0, Format(AgL.VNull(DTTemp.Rows(I).Item("AmtCr")), "0.00"), DBNull.Value)
            DTReport.Rows(I)(IsAccountGroup) = AgL.XNull(DTTemp.Rows(I).Item("IsAccountGroup"))
            DblDebit_Total = DblDebit_Total + Format(AgL.VNull(DTTemp.Rows(I).Item("AmtDr")), "0.00")
            DblCredit_Total = DblCredit_Total + Format(AgL.VNull(DTTemp.Rows(I).Item("AmtCr")), "0.00")
        Next

        FRetDataDisplay_Level_Group = DTReport
    End Function
    Private Sub FDisplay_Level_Group()
        Try
            FRetDataDisplay_Level_Group(ReportFrm.FGetCode(rowOtherFilter))

            'ReportFrm.Text = "Financial Display - " + ReportFrm.FGetText(rowReportType)
            ReportFrm.Text = ReportFrm.FGetText(rowReportType)
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = "ProcFinancialDisplay"
            ReportFrm.IsManualAggregate = False

            DsReport = New DataSet()
            DsReport.Tables.Add(DTReport)
            ReportFrm.ProcFillGrid(DsReport)

            FormatTrialBalance(FormattingOn.OnInit)

            ReportFrm.DGL1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None

            ReportFrm.DGL1.Columns(IsAccountGroup).Visible = False



            ReportFrm.DGL1.Columns(AcGroupCode).Visible = False
            ReportFrm.DGL1.Columns(GRName).Width = 500
            'ReportFrm.DGL1.Columns(Opening).Width = 100
            'ReportFrm.DGL1.Columns(DR_CR_OP).Width = 20
            'ReportFrm.DGL1.Columns(DR_CR_OP).HeaderText = ""
            ReportFrm.DGL1.Columns(Debit).Width = 200
            ReportFrm.DGL1.Columns(Credit).Width = 200
            'ReportFrm.DGL1.Columns(Closing).Width = 100
            'ReportFrm.DGL1.Columns(DR_CR_CL).Width = 20
            'ReportFrm.DGL1.Columns(DR_CR_CL).HeaderText = ""
        Catch ex As Exception
            If Not ex.Message.Contains("Index was out of range") Then
                MsgBox(ex.Message)
            End If
        End Try
    End Sub
    Private Sub FGetStockValues(ByRef mOpeningStockValue As Double, ByRef mClosingStockValue As Double, mFromDate As String)
        mQry = " SELECT H.OpeningStockValue, H.ClosingStockValue  
                FROM DivisionCompanySetting H
                LEFT JOIN Company C ON H.Comp_Code = C.Comp_Code
                WHERE " & AgL.Chk_Date(CDate(mFromDate)) & " BETWEEN Date(C.Start_Dt) AND Date(C.End_Dt) 
                And H.Div_Code = '" & AgL.PubDivCode & "' "

        'WHERE " & AgL.Chk_Date(CDate(mFromDate)) & " BETWEEN C.Start_Dt AND C.End_Dt 

        Dim DtTemp As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
        If DtTemp.Rows.Count > 0 Then
            mOpeningStockValue = AgL.VNull(DtTemp.Rows(0)("OpeningStockValue"))
            mClosingStockValue = AgL.VNull(DtTemp.Rows(0)("ClosingStockValue"))
        End If
    End Sub
    Private Function FindNextRowForBalanceSheerHirerichy() As Integer
        'Dim bRowIndex As Integer = -1
        Dim bRowIndex As Integer = 0

        For K As Integer = DTReport.Rows.Count - 1 To 0 Step -1
            If AgL.VNull(DTReport.Rows(K)(Credit)) <> 0 Then
                bRowIndex = K + 1
                Exit For
            End If
        Next

        If bRowIndex > DTReport.Rows.Count - 1 Then
            DTReport.Rows.Add()
            bRowIndex = DTReport.Rows.Count - 1
        End If



        'For K As Integer = 0 To DTReport.Rows.Count - 1
        '    If AgL.XNull(DTReport.Rows(K)(GRCodeCredit)) = "" And
        '        AgL.VNull(DTReport.Rows(K)(Credit)) = 0 Then
        '        bRowIndex = K
        '        Exit For
        '    End If
        'Next

        'If bRowIndex = -1 Then
        '    DTReport.Rows.Add()
        '    bRowIndex = DTReport.Rows.Count - 1
        'End If

        FindNextRowForBalanceSheerHirerichy = bRowIndex
    End Function

    'Private Function FindNextRowForProfitAndLossHirerichy(DtSource As DataTable) As Integer
    '    Dim bRowIndex As Integer = -1
    '    For I As Integer = 0 To DTReport.Rows.Count - 1
    '        If AgL.XNull(DTReport.Rows(I)(GRCodeCredit)) = "" And
    '            AgL.VNull(DTReport.Rows(I)(Credit)) = 0 Then
    '            bRowIndex = I
    '            Exit For
    '        End If
    '    Next

    '    If bRowIndex = -1 Then
    '        DTReport.Rows.Add()
    '        bRowIndex = DTReport.Rows.Count - 1
    '    End If

    '    FindNextRowForProfitAndLossHirerichy = bRowIndex
    'End Function

    Private Sub FGetStockValuesInDataTable(ByRef mDtStockValue As DataTable, mFromDate As String)
        mQry = " SELECT H.*
                FROM DivisionCompanySetting H
                LEFT JOIN Company C ON H.Comp_Code = C.Comp_Code
                WHERE " & AgL.Chk_Date(CDate(mFromDate)) & " BETWEEN Date(C.Start_Dt) AND Date(C.End_Dt) 
                And H.Div_Code = '" & AgL.PubDivCode & "' "

        'WHERE " & AgL.Chk_Date(CDate(mFromDate)) & " BETWEEN C.Start_Dt AND C.End_Dt 
        mDtStockValue = AgL.FillData(mQry, AgL.GCn).Tables(0)
    End Sub
End Class
