Imports System.IO
Imports AgLibrary
Imports AgLibrary.ClsMain
Imports AgLibrary.ClsMain.agConstants
Imports Microsoft.Reporting.WinForms

Public Class ClsFinancialDisplay

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
    'Public Enum DisplayType
    '    BalanceSheet = 0
    '    ProfitAndLoss = 1
    '    TrailBalance = 2
    '    DTrailBalance = 3
    '    GroupBalance = 4
    '    Ledger = 5
    'End Enum

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
    End Class
    Public Class AddColumn
        Public Const LinkedSubCode As String = "Linked Account"
        Public Const AccountGroup As String = "Account Group"
        Public Const AccountType As String = "Account Type"
    End Class

    Public Const GRCode As String = "SearchCode"
    Public Const DocId As String = "SearchCode"
    Public Const Month As String = "Month"
    Public Const GRName As String = "Name"
    Public Const LGRCode As String = "Linked Account Code"
    Public Const LGRName As String = "Linked Account"
    Public Const LGroupName As String = "Account Group"
    Public Const LAccountType As String = "Account Type"
    Public Const Division As String = "Division"
    Public Const Site As String = "Site"
    Public Const VNo As String = "VNo"
    Public Const VType As String = "VType"
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
            ReportFrm.FilterGrid.Rows(0).Visible = False

            ReportFrm.CreateHelpGrid("FromDate", "From Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubStartDate)
            ReportFrm.CreateHelpGrid("ToDate", "To Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubEndDate)
            ReportFrm.CreateHelpGrid("Site", "Site", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSiteQry, "[SITECODE]")
            ReportFrm.CreateHelpGrid("Division", "Division", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpDivisionQry, "[DIVISIONCODE]")
            ReportFrm.CreateHelpGrid("CostCenter", "Cost Center", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpCostCenterQry)
            ReportFrm.CreateHelpGrid("ShowZeroBalance", "Show Zero Balance", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mHelpYesNoQry, "Yes")
            ReportFrm.CreateHelpGrid("ShowMonthWiseLedger", "Show Month Wise Ledger", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mHelpYesNoQry, "No")

            mQry = " Select 'o' As Tick, '" & AddColumn.LinkedSubCode & "' As Code, '" & AddColumn.LinkedSubCode & "' AS [Value] "
            mQry += " UNION ALL "
            mQry += " Select 'o' As Tick, '" & AddColumn.AccountGroup & "' As Code, '" & AddColumn.AccountGroup & "' AS [Value] "
            mQry += " UNION ALL "
            mQry += " Select 'o' As Tick, '" & AddColumn.AccountType & "' As Code, '" & AddColumn.AccountType & "' AS [Value] "

            ReportFrm.CreateHelpGrid("AddColumn", "Add Column", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mQry)
            ReportFrm.CreateHelpGrid("OtherFilter", "OtherFilter", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, "")
            ReportFrm.FilterGrid.Rows(9).Visible = False
            ReportFrm.CreateHelpGrid("IncludeOpening", "Include Opening", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mHelpYesNoQry, "Yes")
            ReportFrm.CreateHelpGrid("ShowContraAcInLedger", "Show Contra A/c In Ledger", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mHelpYesNoQry, "No")
            ReportFrm.CreateHelpGrid("ClosingStock", "Closing Stock", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.FloatType, "", "0")
            ReportFrm.CreateHelpGrid("LinkedAccountCode", "Linked Account Code", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.StringType, "", "")
            ReportFrm.FilterGrid.Rows(13).Visible = False
            ReportFrm.CreateHelpGrid("LedgerAccount", "Ledger Account", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mHelpSubGroupSingleSelectionQry, "",,, 300)


            Select Case ReportFrm.FGetText(0)
                Case ReportType.DetailTrialBalance
                    ReportFrm.FilterGrid.Rows(12).Visible = False
                    ReportFrm.FilterGrid.Rows(14).Visible = False
                Case ReportType.TrialBalance
                    ReportFrm.FilterGrid.Rows(12).Visible = False
                    ReportFrm.FilterGrid.Rows(14).Visible = False
                Case ReportType.BalanceSheet
                    ReportFrm.FilterGrid.Rows(12).Visible = False
                    ReportFrm.FilterGrid.Rows(14).Visible = False
                Case ReportType.ProfitAndLoss
                    ReportFrm.FilterGrid.Rows(8).Visible = False
                    ReportFrm.FilterGrid.Rows(14).Visible = False
                Case ReportType.GroupBalance
                    ReportFrm.FilterGrid.Rows(12).Visible = False
                Case ReportType.Ledger, ReportType.Ledger
                    ReportFrm.FilterGrid.Rows(12).Visible = False
                Case ReportType.Ledger, ReportType.Ledger_MonthWise
                    ReportFrm.FilterGrid.Rows(12).Visible = False
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
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

            If mFilterGrid IsNot Nothing And mGridRow IsNot Nothing Then
                If mGridRow.DataGridView.Columns.Contains("Search Code") = True Then
                    If mFilterGrid.Item(GFilter, 0).Value = ReportType.DetailTrialBalance Then
                        If mFilterGrid.Item(GFilter, 7).Value = "Yes" Then
                            mFilterGrid.Item(GFilter, 0).Value = ReportType.Ledger_MonthWise
                        Else
                            mFilterGrid.Item(GFilter, 0).Value = ReportType.Ledger
                        End If
                        mFilterGrid.Item(GFieldName, 9).Value = "Ledger Account"
                        mFilterGrid.Item(GFilter, 9).Value = mGridRow.Cells("Name").Value
                        mFilterGrid.Item(GFilterCode, 9).Value = mGridRow.Cells("Search Code").Value
                        If ReportFrm.DGL1.Columns.Contains("Linked Account Code") = True Then
                            mFilterGrid.Item(GFilter, 13).Value = mGridRow.Cells("Linked Account").Value
                            mFilterGrid.Item(GFilterCode, 13).Value = mGridRow.Cells("Linked Account Code").Value
                        End If
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = ReportType.TrialBalance Then
                        mFilterGrid.Item(GFilter, 0).Value = ReportType.GroupBalance
                        mFilterGrid.Item(GFieldName, 9).Value = "Ledger Account Group"
                        mFilterGrid.Item(GFilter, 9).Value = mGridRow.Cells("Name").Value
                        mFilterGrid.Item(GFilterCode, 9).Value = mGridRow.Cells("Search Code").Value
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = ReportType.ProfitAndLoss Then
                        mFilterGrid.Item(GFilter, 0).Value = ReportType.GroupBalance
                        mFilterGrid.Item(GFieldName, 9).Value = "Ledger Account Group"
                        If ReportFrm.DGL1.Columns(ReportFrm.DGL1.CurrentCell.ColumnIndex).Name = GRName Or
                            ReportFrm.DGL1.Columns(ReportFrm.DGL1.CurrentCell.ColumnIndex).Name = Debit Then
                            mFilterGrid.Item(GFilter, 9).Value = mGridRow.Cells(GRName).Value
                            mFilterGrid.Item(GFilterCode, 9).Value = mGridRow.Cells("Search Code").Value
                        ElseIf ReportFrm.DGL1.Columns(ReportFrm.DGL1.CurrentCell.ColumnIndex).Name = GRNameCredit Or
                                ReportFrm.DGL1.Columns(ReportFrm.DGL1.CurrentCell.ColumnIndex).Name = Credit Then
                            mFilterGrid.Item(GFilter, 9).Value = mGridRow.Cells(GRNameCredit).Value
                            mFilterGrid.Item(GFilterCode, 9).Value = mGridRow.Cells("Grcodecredit").Value
                        End If
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = ReportType.BalanceSheet Then
                        If mGridRow.Cells(GRName).Value = "Net Profit" Or
                                mGridRow.Cells(GRName).Value = "Net Loss" Then
                            mFilterGrid.Item(GFilter, 0).Value = ReportType.ProfitAndLoss
                        Else
                            mFilterGrid.Item(GFilter, 0).Value = ReportType.GroupBalance
                            mFilterGrid.Item(GFieldName, 9).Value = "Ledger Account Group"
                            If ReportFrm.DGL1.Columns(ReportFrm.DGL1.CurrentCell.ColumnIndex).Name = GRName Or
                                 ReportFrm.DGL1.Columns(ReportFrm.DGL1.CurrentCell.ColumnIndex).Name = Debit Then
                                mFilterGrid.Item(GFilter, 9).Value = mGridRow.Cells(GRName).Value
                                mFilterGrid.Item(GFilterCode, 9).Value = mGridRow.Cells("Search Code").Value
                            ElseIf ReportFrm.DGL1.Columns(ReportFrm.DGL1.CurrentCell.ColumnIndex).Name = GRNameCredit Or
                            ReportFrm.DGL1.Columns(ReportFrm.DGL1.CurrentCell.ColumnIndex).Name = Credit Then
                                mFilterGrid.Item(GFilter, 9).Value = mGridRow.Cells(GRNameCredit).Value
                                mFilterGrid.Item(GFilterCode, 9).Value = mGridRow.Cells("Grcodecredit").Value
                            End If
                        End If
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = ReportType.GroupBalance Then
                        If mFilterGrid.Item(GFilter, 7).Value = "Yes" Then
                            mFilterGrid.Item(GFilter, 0).Value = ReportType.Ledger_MonthWise
                        Else
                            mFilterGrid.Item(GFilter, 0).Value = ReportType.Ledger
                        End If
                        mFilterGrid.Item(GFilter, 9).Value = mGridRow.Cells("Name").Value
                        mFilterGrid.Item(GFilterCode, 9).Value = mGridRow.Cells("Search Code").Value
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = ReportType.Ledger_MonthWise Then
                        mFilterGrid.Item(GFilter, 0).Value = ReportType.Ledger
                        mFilterGrid.Item(GFieldName, 9).Value = "Ledger Account"
                        mFilterGrid.Item(GFilter, 1).Value = AgL.RetMonthStartDate(CDate(mGridRow.Cells("Month").Value))
                        mFilterGrid.Item(GFilter, 2).Value = AgL.RetMonthEndDate(CDate(mGridRow.Cells("Month").Value))
                        mFilterGrid.Item(GFilter, 9).Value = mGridRow.Cells("Name").Value
                        mFilterGrid.Item(GFilterCode, 9).Value = mGridRow.Cells("Search Code").Value
                    ElseIf mFilterGrid.Item(GFilter, 0).Value = ReportType.Ledger Then
                        ClsMain.FOpenForm(mGridRow.Cells("Search Code").Value, ReportFrm)
                        Exit Sub
                    End If
                Else
                    Exit Sub
                End If
            End If

            Select Case ReportFrm.FGetText(0)
                Case ReportType.DetailTrialBalance
                    FDTrailBalance_Disp()
                Case ReportType.TrialBalance
                    FTrailBalance_Disp()
                Case ReportType.BalanceSheet
                    FBalanceSheet_Disp()
                Case ReportType.ProfitAndLoss
                    FProfitAndLoss_Disp()
                Case ReportType.GroupBalance
                    FDisplay_Level_Group()
                Case ReportType.Ledger, ReportType.Ledger_MonthWise
                    If ReportFrm.FGetText(14) <> "" Then
                        ReportFrm.FilterGrid.Item(GFilterCode, 9).Value = ReportFrm.FGetCode(14)
                        ReportFrm.FilterGrid.Item(GFilter, 9).Value = ReportFrm.FGetText(14)
                    End If
                    FDisplay_SubGroup(ReportFrm.FilterGrid.Item(GFilterCode, 9).Value, ReportFrm.FilterGrid.Item(GFilter, 9).Value)
            End Select
            ReportFrm.DGL1.DefaultCellStyle.WrapMode = DataGridViewTriState.True
            ReportFrm.DGL1.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells)
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
        Dim DblCLDR As Double, DblCLCR As Double, OpeningTotal As Double
        Dim mCondStr$ = ""
        Dim I As Integer
        Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"
        FCreateDataTable(ReportType.DetailTrialBalance)

        If UCase(ReportFrm.FGetText(5)) = "No" Then StrConditionZeroBal = "Having (IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LG.AmtCr),0)) <> 0 "
        StrConditionOP = " Where LG.V_Date < " & AgL.Chk_Date(CDate(ReportFrm.FGetText(1)).ToString("s")) & " "
        StrConditionOP += Replace(ReportFrm.GetWhereCondition("LG.Site_Code", 3), "''", "'")
        StrConditionOP += Replace(ReportFrm.GetWhereCondition("LG.DivCode", 4), "''", "'")
        StrConditionOP += " And Date(LG.V_Date) >= (Case When Ag.GroupNature in ('R','E') 
                                    Then " & AgL.Chk_Date(CDate(AgL.PubStartDate).ToString("s")) & " 
                                    Else '1900/01/01' End) "

        StrCondition1 = " Where ( Date(LG.V_Date) Between " & AgL.Chk_Date(CDate(ReportFrm.FGetText(1)).ToString("s")) & " 
                                And " & AgL.Chk_Date(CDate(ReportFrm.FGetText(2)).ToString("s")) & " ) "
        StrCondition1 += Replace(ReportFrm.GetWhereCondition("LG.Site_Code", 3), "''", "'")
        StrCondition1 += Replace(ReportFrm.GetWhereCondition("LG.DivCode", 4), "''", "'")

        StrConditionAcGroup += ReportFrm.GetWhereCondition("Sg.GroupCode", 9)

        '========== For Detail Section =======

        StrSQLQuery = "Select SubCode, "
        StrSQLQuery += IIf(ReportFrm.FGetText(8).ToString.Contains(AddColumn.LinkedSubCode), "LSCode, ", "")
        StrSQLQuery += IIf(ReportFrm.FGetText(8).ToString.Contains(AddColumn.AccountGroup), "GroupCode, ", "")
        StrSQLQuery += IIf(ReportFrm.FGetText(8).ToString.Contains(AddColumn.AccountType), "SubGroupType, ", "")
        StrSQLQuery += "Max(SName) As SName, "
        StrSQLQuery += IIf(ReportFrm.FGetText(8).ToString.Contains(AddColumn.LinkedSubCode), "Max(LSName) As LSName, ", "")
        StrSQLQuery += IIf(ReportFrm.FGetText(8).ToString.Contains(AddColumn.AccountGroup), "Max(GroupName) As GroupName, ", "")
        StrSQLQuery += "IfNull(Sum(OPBal),0) As OPBal, "
        StrSQLQuery += "IfNull(Sum(AmtDr),0) As AmtDr, "
        StrSQLQuery += "IfNull(Sum(AmtCr),0) As AmtCr "
        StrSQLQuery += "From ( "

        If ReportFrm.FGetText(10) = "Yes" Then
            StrSQLQuery += "Select IfNull(SG.Code,'') As SubCode, "
            StrSQLQuery += IIf(ReportFrm.FGetText(8).ToString.Contains(AddColumn.LinkedSubCode), "IfNull(LG.LinkedSubcode,'') As LSCode, ", "")
            StrSQLQuery += IIf(ReportFrm.FGetText(8).ToString.Contains(AddColumn.AccountGroup), "IfNull(Ag.GroupCode,'') As GroupCode, ", "")
            StrSQLQuery += IIf(ReportFrm.FGetText(8).ToString.Contains(AddColumn.AccountType), "IfNull(SG.SubGroupType,'') As SubGroupType, ", "")
            StrSQLQuery += "(IfNull(Max(SG.Name),'') || ' - ' || IfNull(Max(CT.CityName),'')) As SName, "
            StrSQLQuery += IIf(ReportFrm.FGetText(8).ToString.Contains(AddColumn.LinkedSubCode), "(IfNull(Max(LSG.Name),'')) As LSName, ", "")
            StrSQLQuery += IIf(ReportFrm.FGetText(8).ToString.Contains(AddColumn.AccountGroup), "(IfNull(Max(Ag.GroupName),'')) As GroupName, ", "")
            StrSQLQuery += "(IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LG.AmtCr),0)) As OPBal, "
            StrSQLQuery += "0 As AmtDr, "
            StrSQLQuery += "0 As AmtCr "
            StrSQLQuery += "From Ledger LG "
            StrSQLQuery += "Left Join ViewHelpSubgroup SG On LG.SubCode=SG.Code  "
            StrSQLQuery += IIf(ReportFrm.FGetText(8).ToString.Contains(AddColumn.LinkedSubCode), "Left Join ViewHelpSubgroup LSG On LG.LinkedSubcode=LSG.Code  ", "")
            StrSQLQuery += "Left Join AcGroup Ag On Ag.GroupCode=SG.GroupCode "
            StrSQLQuery += "Left Join City CT On CT.CityCode=SG.CityCode "
            StrSQLQuery += StrConditionOP & StrConditionAcGroup
            StrSQLQuery += "Group By IfNull(SG.Code,'') "
            StrSQLQuery += IIf(ReportFrm.FGetText(8).ToString.Contains(AddColumn.LinkedSubCode), ",IfNull(LG.LinkedSubcode,'') ", "")
            StrSQLQuery += IIf(ReportFrm.FGetText(8).ToString.Contains(AddColumn.AccountGroup), ",IfNull(Ag.GroupCode,'') ", "")
            StrSQLQuery += IIf(ReportFrm.FGetText(8).ToString.Contains(AddColumn.AccountType), ",IfNull(Sg.SubGroupType,'') ", "")
            StrSQLQuery += "Having (IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LG.AmtCr),0)) <> 0 "
            StrSQLQuery += "Union All "
        End If

        StrSQLQuery += "Select	IfNull(SG.Code,'') As SubCode, "
        StrSQLQuery += IIf(ReportFrm.FGetText(8).ToString.Contains(AddColumn.LinkedSubCode), "IfNull(LG.LinkedSubcode,'') As LSCode, ", "")
        StrSQLQuery += IIf(ReportFrm.FGetText(8).ToString.Contains(AddColumn.AccountGroup), "IfNull(Ag.GroupCode,'') As GroupCode, ", "")
        StrSQLQuery += IIf(ReportFrm.FGetText(8).ToString.Contains(AddColumn.AccountType), "IfNull(Sg.SubGroupType,'') As SubGroupType, ", "")
        StrSQLQuery += "IfNull(Max(SG.Name),'') As SName, "
        StrSQLQuery += IIf(ReportFrm.FGetText(8).ToString.Contains(AddColumn.LinkedSubCode), "(IfNull(Max(LSG.Name),'')) As LSName, ", "")
        StrSQLQuery += IIf(ReportFrm.FGetText(8).ToString.Contains(AddColumn.AccountGroup), "(IfNull(Max(Ag.GroupName),'')) As GroupName, ", "")
        StrSQLQuery += "0 As OPBal, "

        'StrSQLQuery += "(Case When (IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LG.AmtCr),0))>0 Then  "
        'StrSQLQuery += "(IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LG.AmtCr),0)) Else 0 End)*1.0 As AmtDr, "
        'StrSQLQuery += "(Case When (IfNull(Sum(LG.AmtCr),0)-IfNull(Sum(LG.AmtDr),0))>0 Then "
        'StrSQLQuery += "(IfNull(Sum(LG.AmtCr),0)-IfNull(Sum(LG.AmtDr),0)) Else 0 End)*1.0 As AmtCr "

        'Changed ON 10/May/2019 By Akash because Detail Trial Balance should show Totals Of Debit And Credit Balances
        StrSQLQuery += "IfNull(Sum(LG.AmtDr),0)*1.0 As AmtDr, "
        StrSQLQuery += "IfNull(Sum(LG.AmtCr),0)*1.0 As AmtCr "
        StrSQLQuery += "From Ledger LG "
        StrSQLQuery += "Left Join ViewHelpSubgroup SG On LG.SubCode=SG.Code "
        StrSQLQuery += "Left Join City CT On CT.CityCode=SG.CityCode "
        StrSQLQuery += IIf(ReportFrm.FGetText(8).ToString.Contains(AddColumn.LinkedSubCode), "Left Join ViewHelpSubgroup LSG On LG.LinkedSubcode=LSG.Code  ", "")
        StrSQLQuery += "Left Join AcGroup Ag On Ag.GroupCode=SG.GroupCode "
        StrSQLQuery += StrCondition1 & StrConditionAcGroup
        StrSQLQuery += "Group By IfNull(SG.Code,'')"
        StrSQLQuery += IIf(ReportFrm.FGetText(8).ToString.Contains(AddColumn.LinkedSubCode), ",IfNull(LG.LinkedSubcode,'')", "")
        StrSQLQuery += IIf(ReportFrm.FGetText(8).ToString.Contains(AddColumn.AccountGroup), ",IfNull(Ag.GroupCode,'')", "")
        StrSQLQuery += IIf(ReportFrm.FGetText(8).ToString.Contains(AddColumn.AccountType), ",IfNull(Sg.SubGroupType,'')", "")
        StrSQLQuery += StrConditionZeroBal
        StrSQLQuery += ") As Tmp "
        StrSQLQuery += "Group By SubCode "
        StrSQLQuery += IIf(ReportFrm.FGetText(8).ToString.Contains(AddColumn.LinkedSubCode), ",LSCode ", "")
        StrSQLQuery += IIf(ReportFrm.FGetText(8).ToString.Contains(AddColumn.AccountGroup), ",GroupCode ", "")
        StrSQLQuery += IIf(ReportFrm.FGetText(8).ToString.Contains(AddColumn.AccountType), ",SubGroupType ", "")
        StrSQLQuery += "Order By IfNull(Max(SName),'')"
        StrSQLQuery += IIf(ReportFrm.FGetText(8).ToString.Contains(AddColumn.LinkedSubCode), ",IfNull(Max(LSName),'')  ", "")

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

        Next

        DTReport.Rows.Add()

        If AgL.XNull(ReportFrm.FGetCode(9)) = "" Then
            If (DblDebit_Total - DblCredit_Total) > 0 Then
                DTReport.Rows(I)(GRName) = "Difference In Trial Balance"
                DTReport.Rows(I)(Credit) = Format(Math.Abs(DblDebit_Total - DblCredit_Total), "0.00")
                DblCredit_Total = DblCredit_Total + Format((DblDebit_Total - DblCredit_Total), "0.00")

            ElseIf (DblDebit_Total - DblCredit_Total + OpeningTotal) < 0 Then
                DTReport.Rows(I)(GRName) = "Difference In Trial Balance"
                DTReport.Rows(I)(Debit) = Format(Math.Abs(DblDebit_Total - DblCredit_Total), "0.00")
                DblDebit_Total = DblDebit_Total + Format((DblCredit_Total - DblDebit_Total), "0.00")
            End If
        End If


        If DTReport.Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

        ReportFrm.Text = "Financial Display - " + ReportFrm.FGetText(0)
        ReportFrm.ClsRep = Me
        ReportFrm.ReportProcName = "ProcFinancialDisplay"
        ReportFrm.IsManualAggregate = False

        DsReport = New DataSet()
        DsReport.Tables.Add(DTReport)
        ReportFrm.ProcFillGrid(DsReport)

        FormatDetailTrialBalance(FormattingOn.OnInit)

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
        '    ReportFrm.DGL1.Columns(GRName).Width = 400
        'End If
        'ReportFrm.DGL1.Columns(Opening).Width = 150
        'ReportFrm.DGL1.Columns(DR_CR_OP).Width = 30
        'ReportFrm.DGL1.Columns(Debit).Width = 150
        'ReportFrm.DGL1.Columns(Credit).Width = 150
        'ReportFrm.DGL1.Columns(Closing).Width = 150
        'ReportFrm.DGL1.Columns(DR_CR_CL).Width = 30

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
        Try
            FCreateDataTable(ReportType.TrialBalance)

            If UCase(ReportFrm.FGetText(5)) = "N" Then StrConditionZeroBal = "Having (Round(IfNull(Sum(LG.AmtDr),0),2)-Round(IfNull(Sum(LG.AmtCr),0),2)) <> 0 "
            StrCondition1 = " Where Date(LG.V_Date) <= " & AgL.Chk_Date(CDate(ReportFrm.FGetText(2)).ToString("s")) & "  "
            StrCondition1 += " And Date(LG.V_Date) >= (Case When Ag.GroupNature in ('R','E') 
                                Then " & AgL.Chk_Date(CDate(AgL.PubStartDate).ToString("s")) & " 
                                Else '1900/01/01' End) "
            If ReportFrm.FGetText(10) = "No" Then
                StrCondition1 += " And Date(LG.V_Date) >= " & AgL.Chk_Date(CDate(ReportFrm.FGetText(1)).ToString("s")) & " "
            End If
            StrCondition1 += Replace(ReportFrm.GetWhereCondition("LG.Site_Code", 3), "''", "'")
            StrCondition1 += Replace(ReportFrm.GetWhereCondition("LG.DivCode", 4), "''", "'")

            '========== For Detail Section =======

            StrSQLQuery = "Select	(Case IfNull(AG1.GroupCode,'') When '' Then IfNull(AG.GroupCode,'') "
            StrSQLQuery = StrSQLQuery + "Else IfNull(AG1.GroupCode,'') End)  As GroupCode, "
            StrSQLQuery = StrSQLQuery + "Max(Case IfNull(AG1.GroupName,'') When '' Then IfNull(AG.GroupName,'') "
            StrSQLQuery = StrSQLQuery + "Else IfNull(AG1.GroupName,'') End)  As GName, "
            StrSQLQuery = StrSQLQuery + "(Case When (IfNull(Sum(LG.AmtDr),0.0)-IfNull(Sum(LG.AmtCr),0.0))>0 Then  "
            StrSQLQuery = StrSQLQuery + "(IfNull(Sum(LG.AmtDr) ,0.0)-IfNull(Sum(LG.AmtCr),0.0)) Else 0 End)*1.0 As AmtDr, "
            StrSQLQuery = StrSQLQuery + "(Case When (IfNull(Sum(LG.AmtCr),0)-IfNull(Sum(LG.AmtDr),0))>0 Then "
            StrSQLQuery = StrSQLQuery + "(IfNull(Sum(LG.AmtCr),0)-IfNull(Sum(LG.AmtDr),0)) Else 0 End)*1.0 As AmtCr "
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
                DTReport.Rows(I)(GRCode) = AgL.XNull(DTTemp.Rows(I).Item("GroupCode"))
                DTReport.Rows(I)(GRName) = AgL.XNull(DTTemp.Rows(I).Item("GName"))
                DTReport.Rows(I)(Debit) = IIf(AgL.VNull(DTTemp.Rows(I).Item("AmtDr")) > 0, Format(AgL.VNull(DTTemp.Rows(I).Item("AmtDr")), "0.00"), System.DBNull.Value)
                DTReport.Rows(I)(Credit) = IIf(AgL.VNull(DTTemp.Rows(I).Item("AmtCr")) > 0, Format(AgL.VNull(DTTemp.Rows(I).Item("AmtCr")), "0.00"), System.DBNull.Value)
                DblDebit_Total = DblDebit_Total + Format(AgL.VNull(DTTemp.Rows(I).Item("AmtDr")), "0.00")
                DblCredit_Total = DblCredit_Total + Format(AgL.VNull(DTTemp.Rows(I).Item("AmtCr")), "0.00")
            Next

            DTReport.Rows.Add()

            If (DblDebit_Total - DblCredit_Total) > 0 Then
                DTReport.Rows(I)(GRName) = "Difference In Trial Balance"
                DTReport.Rows(I)(Credit) = Format((DblDebit_Total - DblCredit_Total), "0.00")
                DblCredit_Total = DblCredit_Total + Format((DblDebit_Total - DblCredit_Total), "0.00")
            ElseIf (DblCredit_Total - DblDebit_Total) > 0 Then
                DTReport.Rows(I)(GRName) = "Difference In Trial Balance"
                DTReport.Rows(I)(Debit) = Format((DblCredit_Total - DblDebit_Total), "0.00")
                DblDebit_Total = DblDebit_Total + Format((DblCredit_Total - DblDebit_Total), "0.00")
            End If

            If DTReport.Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            ReportFrm.Text = "Financial Display - " + ReportFrm.FGetText(0)
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = "ProcFinancialDisplay"
            ReportFrm.IsHideZeroColumns = False
            ReportFrm.IsManualAggregate = False

            DsReport = New DataSet()
            DsReport.Tables.Add(DTReport)
            ReportFrm.ProcFillGrid(DsReport)

            FormatTrialBalance(FormattingOn.OnInit)


            ReportFrm.DGL1.Columns(GRName).Width = 500
            ReportFrm.DGL1.Columns(Debit).Width = 300
            ReportFrm.DGL1.Columns(Credit).Width = 300


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

        Try
            FCreateDataTable(ReportType.BalanceSheet)

            StrCondition1 = " Where Date(LG.V_Date) <= " & AgL.Chk_Date(CDate(ReportFrm.FGetText(2)).ToString("s")) & ""
            StrCondition1 += Replace(ReportFrm.GetWhereCondition("LG.Site_Code", 3), "''", "'")
            StrCondition1 += Replace(ReportFrm.GetWhereCondition("LG.DivCode", 4), "''", "'")


            '========== For Detail Section =======

            StrSQLQuery = "Select	(Case IfNull(AG1.GroupCode,'') When '' Then IfNull(AG.GroupCode,'') "
            StrSQLQuery = StrSQLQuery + "Else IfNull(AG1.GroupCode,'') End)  As GroupCode, "
            StrSQLQuery = StrSQLQuery + "Max(Case IfNull(AG1.GroupName,'') When '' Then IfNull(AG.GroupName,'') "
            StrSQLQuery = StrSQLQuery + "Else IfNull(AG1.GroupName,'') End)  As GName, "
            StrSQLQuery = StrSQLQuery + "(Case When (IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LG.AmtCr),0))>0 Then  "
            StrSQLQuery = StrSQLQuery + "(IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LG.AmtCr),0)) Else 0 End) As AmtDr, "
            StrSQLQuery = StrSQLQuery + "(Case When (IfNull(Sum(LG.AmtCr),0)-IfNull(Sum(LG.AmtDr),0))>0 Then "
            StrSQLQuery = StrSQLQuery + "(IfNull(Sum(LG.AmtCr),0)-IfNull(Sum(LG.AmtDr),0)) Else 0 End) As AmtCr, "
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
            For I = 0 To DTTemp.Rows.Count - 1
                If AgL.VNull(DTTemp.Rows(I).Item("AmtDr")) > 0 Then
                    J = FFindEmptyRow(DTReport, GRNameCredit)
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

            If AgL.VNull(ReportFrm.FGetText(12)) > 0 Then
                J = FFindEmptyRow(DTReport, GRNameCredit)
                DTReport.Rows(J)(GRNameCredit) = "Closing Stock"
                DTReport.Rows(J)(Credit) = AgL.VNull(ReportFrm.FGetText(12))
                DblCredit_Total = DblCredit_Total + AgL.VNull(ReportFrm.FGetText(12))
                'DTReport.Rows(J)(GRNameCredit, J).Style.Font = New Font("Arial", 9, FontStyle.Regular)
                'DTReport.Rows(J)(GCredit, J).Style.Font = New Font("Arial", 9, FontStyle.Regular)
            End If

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

            'If DHSMain.DblClosingStock > 0 Then DblNet_Profit_Loss = DblNet_Profit_Loss + DHSMain.DblClosingStock

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

            ReportFrm.Text = "Financial Display - " + ReportFrm.FGetText(0)
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

            ReportFrm.DGL1.Columns(GRCodeCredit).Visible = False
            ReportFrm.DGL2.Columns(ReportFrm.DGL1.Columns(GRCodeCredit).Index).Visible = False

            ReportFrm.DGL1.Columns(GRName).HeaderText = "Liabilities"
            ReportFrm.DGL1.Columns(GRNameCredit).HeaderText = "Assets"
            ReportFrm.DGL1.Columns(Debit).HeaderText = "Amount"
            ReportFrm.DGL1.Columns(Credit).HeaderText = "Amount"

            ReportFrm.DGL1.Columns(GRName).Width = 450
            ReportFrm.DGL1.Columns(GRNameCredit).Width = 450
            ReportFrm.DGL1.Columns(Debit).Width = 200
            ReportFrm.DGL1.Columns(Credit).Width = 200
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
            StrCondition1 = " Where Date(LG.V_Date) <= " & AgL.Chk_Date(CDate(ReportFrm.FGetText(2)).ToString("s")) & " "
            StrCondition1 += Replace(ReportFrm.GetWhereCondition("LG.Site_Code", 3), "''", "'")
            StrCondition1 += Replace(ReportFrm.GetWhereCondition("LG.DivCode", 4), "''", "'")


            StrCondition1 += " And Date(LG.V_Date) >= (Case When Ag.GroupNature in ('R','E') Then " & AgL.Chk_Date(CDate(AgL.PubStartDate).ToString("s")) & " Else '1900/01/01' End) "

            '========== For Detail Section =======
            StrSQLQuery = "Select	(Case IfNull(AG1.GroupCode,'') When '' Then IfNull(AG.GroupCode,'') "
            StrSQLQuery = StrSQLQuery + "Else IfNull(AG1.GroupCode,'') End)  As GroupCode, "
            StrSQLQuery = StrSQLQuery + "Max(Case IfNull(AG1.GroupName,'') When '' Then IfNull(AG.GroupName,'') "
            StrSQLQuery = StrSQLQuery + "Else IfNull(AG1.GroupName,'') End)  As GName, "
            StrSQLQuery = StrSQLQuery + "(Case When (IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LG.AmtCr),0))>0 Then  "
            StrSQLQuery = StrSQLQuery + "(IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LG.AmtCr),0)) Else 0 End) As AmtDr, "
            StrSQLQuery = StrSQLQuery + "(Case When (IfNull(Sum(LG.AmtCr),0)-IfNull(Sum(LG.AmtDr),0))>0 Then "
            StrSQLQuery = StrSQLQuery + "(IfNull(Sum(LG.AmtCr),0)-IfNull(Sum(LG.AmtDr),0)) Else 0 End) As AmtCr, "
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
            StrCondition1 = " Where Date(LG.V_Date) <= " & AgL.Chk_Date(CDate(ReportFrm.FGetText(2)).ToString("s")) & " "
            StrCondition1 += Replace(ReportFrm.GetWhereCondition("LG.Site_Code", 3), "''", "'")
            StrCondition1 += Replace(ReportFrm.GetWhereCondition("LG.DivCode", 4), "''", "'")

            StrCondition1 += " And Date(LG.V_Date) >= (Case When Ag.GroupNature in ('R','E') Then " & AgL.Chk_Date(CDate(AgL.PubStartDate).ToString("s")) & " Else '1900/01/01' End) "
            '========== For Detail Section =======
            StrSQLQuery = "Select	(Case IfNull(AG1.GroupCode,'') When '' Then IfNull(AG.GroupCode,'') "
            StrSQLQuery = StrSQLQuery + "Else IfNull(AG1.GroupCode,'') End)  As GroupCode, "
            StrSQLQuery = StrSQLQuery + "Max(Case IfNull(AG1.GroupName,'') When '' Then IfNull(AG.GroupName,'') "
            StrSQLQuery = StrSQLQuery + "Else IfNull(AG1.GroupName,'') End)  As GName, "
            StrSQLQuery = StrSQLQuery + "(Case When (IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LG.AmtCr),0))>0 Then  "
            StrSQLQuery = StrSQLQuery + "(IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LG.AmtCr),0)) Else 0 End) As AmtDr, "
            StrSQLQuery = StrSQLQuery + "(Case When (IfNull(Sum(LG.AmtCr),0)-IfNull(Sum(LG.AmtDr),0))>0 Then "
            StrSQLQuery = StrSQLQuery + "(IfNull(Sum(LG.AmtCr),0)-IfNull(Sum(LG.AmtDr),0)) Else 0 End) As AmtCr, "
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

        Try
            FCreateDataTable(ReportType.ProfitAndLoss)



            '========= For Trading A/c ===========
            DTTemp = FGetTRDDataTable()

            DblDebit_Total = 0
            DblCredit_Total = 0
            For I = 0 To DTTemp.Rows.Count - 1
                If AgL.VNull(DTTemp.Rows(I).Item("AmtCr")) > 0 Then
                    J = FFindEmptyRow(DTReport, GRNameCredit)
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

            'If DHSMain.DblClosingStock > 0 Then
            '    J = FFindEmptyRow(DTReport, GRNameCredit)
            '    DTReport.Rows(J)(GRNameCredit) = "Closing Stock"
            '    DTReport.Rows(J)(Credit) = DHSMain.DblClosingStock
            '    DblCredit_Total = DblCredit_Total + DHSMain.DblClosingStock
            '    DTReport.Rows(J)(GRNameCredit, J).Style.Font = New Font("Arial", 9, FontStyle.Regular)
            '    DTReport.Rows(J)(Credit, J).Style.Font = New Font("Arial", 9, FontStyle.Regular)
            'End If

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

            ReportFrm.Text = "Financial Display - " + ReportFrm.FGetText(0)
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

            'ReportFrm.DGL2.Visible = False
            ReportFrm.DGL1.Columns(GRCodeCredit).Visible = False
            ReportFrm.DGL2.Columns(ReportFrm.DGL1.Columns(GRCodeCredit).Index).Visible = False

            ReportFrm.DGL1.Columns(GRName).HeaderText = "Particulars"
            ReportFrm.DGL1.Columns(GRNameCredit).HeaderText = "Particulars"
            ReportFrm.DGL1.Columns(Debit).HeaderText = "Amount"
            ReportFrm.DGL1.Columns(Credit).HeaderText = "Amount"

            ReportFrm.DGL1.Columns(GRName).Width = 450
            ReportFrm.DGL1.Columns(GRNameCredit).Width = 450
            ReportFrm.DGL1.Columns(Debit).Width = 200
            ReportFrm.DGL1.Columns(Credit).Width = 200
        Catch ex As Exception
            If Not ex.Message.Contains("Index was out of range") Then
                MsgBox(ex.Message)
            End If
        End Try
    End Sub
    Private Sub FDisplay_SubGroup(ByVal StrForCode As String, ByVal StrForName As String)
        Dim StrCondition1 As String = "", StrConditionOP As String = ""
        Dim StrSQLQuery As String = ""
        Dim DTTemp As DataTable
        Dim DblDebit_Total As Double, DblCredit_Total As Double, DblOpening As Double
        Dim I As Integer, J As Integer
        Dim Color_Main As Color, Color_A As Color, Color_B As Color

        Try
            StrConditionOP = " Where LG.V_Date < " & AgL.Chk_Date(CDate(ReportFrm.FGetText(1)).ToString("s")) & " "
            StrConditionOP += Replace(ReportFrm.GetWhereCondition("LG.Site_Code", 3), "''", "'")
            StrConditionOP += Replace(ReportFrm.GetWhereCondition("LG.DivCode", 4), "''", "'")
            StrConditionOP += " And Date(LG.V_Date) >= (Case When Ag.GroupNature in ('R','E') 
                            Then " & AgL.Chk_Date(CDate(AgL.PubStartDate).ToString("s")) & " Else '1900/01/01' End)  "

            If ReportFrm.FGetText(8).ToString.Contains(AddColumn.LinkedSubCode) Then
                'If AgL.XNull(ReportFrm.FGetCode(13)) <> "" Then
                StrConditionOP += " And IfNull(Lg.LinkedSubCode,'') = '" & AgL.XNull(ReportFrm.FGetCode(13)) & "'"
                'End If
            End If

            StrCondition1 = " Where ( Date(LG.V_Date) Between  " & AgL.Chk_Date(CDate(ReportFrm.FGetText(1)).ToString("s")) & "
                                And " & AgL.Chk_Date(CDate(ReportFrm.FGetText(2)).ToString("s")) & ") "
            StrCondition1 += Replace(ReportFrm.GetWhereCondition("LG.Site_Code", 3), "''", "'")
            StrCondition1 += Replace(ReportFrm.GetWhereCondition("LG.DivCode", 4), "''", "'")

            If ReportFrm.FGetText(8).ToString.Contains(AddColumn.LinkedSubCode) Then
                'If AgL.XNull(ReportFrm.FGetCode(13)) <> "" Then
                StrCondition1 += " And IfNull(Lg.LinkedSubCode,'') = '" & AgL.XNull(ReportFrm.FGetCode(13)) & "'"
                'End If
            End If



            '========== For Detail Section =======
            If ReportFrm.FGetText(10) = "Yes" Then
                StrSQLQuery = "Select	Null As DocId, Null as Division, Null as Site,'Opening' As Narration, Max(LG.SubCode) As SubCode, Max(Sg.Name) As Name,"
                StrSQLQuery = StrSQLQuery + "(Case When (IfNull(Sum(AmtDr),0)-IfNull(Sum(LG.AmtCr),0))>0 Then "
                StrSQLQuery = StrSQLQuery + "(IfNull(Sum(AmtDr),0)-IfNull(Sum(LG.AmtCr),0)) Else 0 End) As AmtDr, "
                StrSQLQuery = StrSQLQuery + "(Case When (IfNull(Sum(AmtCr),0)-IfNull(Sum(LG.AmtDr),0))>0 Then "
                StrSQLQuery = StrSQLQuery + "(IfNull(Sum(AmtCr),0)-IfNull(Sum(LG.AmtDr),0)) Else 0 End) As AmtCr, "
                StrSQLQuery = StrSQLQuery + "Null As V_No,Null As V_Type,Null As V_Date,0 As SNo,'' As ContraText, "
                StrSQLQuery = StrSQLQuery + "Null As SerialNo "
                StrSQLQuery = StrSQLQuery + "From Ledger LG "
                StrSQLQuery = StrSQLQuery + "Left Join Subgroup SG On Sg.SubCode = LG.SubCode "
                StrSQLQuery = StrSQLQuery + "Left Join AcGroup AG On Ag.GroupCode = Sg.GroupCode "
                StrSQLQuery = StrSQLQuery + "Left Join Division Div On Lg.DivCode = div.Div_Code "
                StrSQLQuery = StrSQLQuery + "Left Join SiteMast Site On Lg.Site_Code = Site.Code "
                StrSQLQuery = StrSQLQuery + StrConditionOP
                'StrSQLQuery = StrSQLQuery + "And LG.SubCode='" & StrForCode & "' " + IIf(AgL.PubServerName = "", " Group By '1', Lg.Site_Code, Lg.DivCode ", "")
                StrSQLQuery = StrSQLQuery + "And LG.SubCode='" & StrForCode & "' " + IIf(AgL.PubServerName = "", " Group By '1'", "")
                StrSQLQuery = StrSQLQuery + "Having (IfNull(Sum(AmtDr),0)-IfNull(Sum(LG.AmtCr),0))<>0 "
                StrSQLQuery = StrSQLQuery + "Union All "
            End If

            StrSQLQuery = StrSQLQuery + "Select	LG.DocId, Div.ShortName as Division, Site.ShortName as Site,LG.Narration,LG.SubCode As SubCode,Sg.Name, LG.AmtDr,LG.AmtCr,LG.RecID As V_No,"
            StrSQLQuery = StrSQLQuery + "LG.V_Type,LG.V_Date,1 As SNo, ContraText,Lg.RecID as SerialNo "
            StrSQLQuery = StrSQLQuery + "From Ledger LG "
            StrSQLQuery = StrSQLQuery + "Left Join Subgroup SG On Sg.SubCode = LG.SubCode "
            StrSQLQuery = StrSQLQuery + "Left Join Voucher_Type VT On LG.V_Type=VT.V_Type "
            StrSQLQuery = StrSQLQuery + "Left Join Division Div On Lg.DivCode = div.Div_Code "
            StrSQLQuery = StrSQLQuery + "Left Join SiteMast Site On Lg.Site_Code = Site.Code "
            StrSQLQuery = StrSQLQuery + StrCondition1
            StrSQLQuery = StrSQLQuery + "And LG.SubCode='" & StrForCode & "' "

            If ReportFrm.FGetText(0) = ReportType.Ledger_MonthWise Then
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

                ReportFrm.Text = "Financial Display - " + ReportFrm.FGetText(0)
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

                    If ReportFrm.FGetText(11) = "Yes" Then
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

                ReportFrm.Text = "Financial Display - " + ReportFrm.FGetText(0)
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

                'If ReportFrm.FGetText(11) = "Yes" Then
                '    ReportFrm.DGL1.DefaultCellStyle.Font = New Font("Courier New", 9, FontStyle.Italic)
                'End If

                ReportFrm.DGL1.Columns(Narration).Width = 400
                ReportFrm.DGL1.Columns(Debit).Width = 150
                ReportFrm.DGL1.Columns(Credit).Width = 150
                ReportFrm.DGL1.Columns(Closing).Width = 150
                ReportFrm.DGL1.Columns(DR_CR_CL).Width = 30
                ReportFrm.DGL1.Columns(DR_CR_CL).HeaderText = ""
            End If


        Catch ex As Exception
            If Not ex.Message.Contains("Index was out Of range") Then
                MsgBox(ex.Message)
            End If
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
    Private Sub FDisplay_Level_Group()
        Dim StrCondition1 As String = ""
        Dim StrSQLQuery As String = ""
        Dim DTTemp As DataTable
        Dim DblDebit_Total As Double, DblCredit_Total As Double
        Dim StrConditionZeroBal As String = ""
        Dim I As Integer

        Try
            FCreateDataTable(ReportType.GroupBalance)

            If UCase(ReportFrm.FGetText(6)) = "N" Then StrConditionZeroBal = "Having (Round(IfNull(Sum(LG.AmtDr),0),2)-Round(IfNull(Sum(LG.AmtCr),0),2)) <> 0 "
            StrCondition1 = " Where Date(LG.V_Date) <= " & AgL.Chk_Date(CDate(ReportFrm.FGetText(2)).ToString("s")) & " "
            StrCondition1 += Replace(ReportFrm.GetWhereCondition("LG.Site_Code", 3), "''", "'")
            StrCondition1 += Replace(ReportFrm.GetWhereCondition("LG.DivCode", 4), "''", "'")
            StrCondition1 += " And Date(LG.V_Date) >= (Case When Ag.GroupNature In ('R','E') 
                            Then " & AgL.Chk_Date(CDate(AgL.PubStartDate).ToString("s")) & " Else '1900/01/01' End) "
            If ReportFrm.FGetText(10) = "No" Then
                StrCondition1 += " And Date(LG.V_Date) >= " & AgL.Chk_Date(CDate(ReportFrm.FGetText(1)).ToString("s")) & " "
            End If


            '========== For Detail Section =======
            StrSQLQuery = "Select	(Case When IfNull(AG1.GroupCode,'')<>'' Then 'A+' || IfNull(AG1.GroupCode,'') "
            StrSQLQuery += "When IfNull(AG.GroupUnder,'')='" & ReportFrm.FGetCode(9) & "' Then 'A+' || IfNull(AG.GroupCode,'') "
            StrSQLQuery += "Else 'S+' || IfNull(SG.SubCode,'')  End)  As GroupCode, "
            StrSQLQuery += "Max(Case When IfNull(AG1.GroupCode,'')<>'' Then IfNull(AG1.GroupName,'') "
            StrSQLQuery += "When IfNull(AG.GroupUnder,'')='" & ReportFrm.FGetCode(9) & "' Then IfNull(AG.GroupName,'') "
            StrSQLQuery += "Else IfNull(SG.Name,'') || ' - ' || IfNull(CT.CityName,'') End)  As GName, "
            StrSQLQuery += "(Case When (IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LG.AmtCr),0))>0 Then  "
            StrSQLQuery += "(IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LG.AmtCr),0)) Else 0 End) As AmtDr, "
            StrSQLQuery += "(Case When (IfNull(Sum(LG.AmtCr),0)-IfNull(Sum(LG.AmtDr),0))>0 Then "
            StrSQLQuery += "(IfNull(Sum(LG.AmtCr),0)-IfNull(Sum(LG.AmtDr),0)) Else 0 End) As AmtCr "
            StrSQLQuery += "From Ledger LG Left Join SubGroup SG On LG.SubCode=SG.SubCode  Left Join "
            StrSQLQuery += "City CT On CT.CityCode=SG.CityCode Left Join "
            StrSQLQuery += "AcGroup AG On AG.GroupCode=SG.GroupCode Left Join "
            StrSQLQuery += "AcGroupPath AGP On AGP.GroupCode=AG.GroupCode And AGP.SNo=" & IntLevel & " Left Join "
            StrSQLQuery += "AcGroup AG1 On AG1.GroupCode=AGP.GroupUnder "
            StrSQLQuery += StrCondition1
            StrSQLQuery += "And (AG.GroupCode In "
            StrSQLQuery += "(Select GroupCode From AcGroupPath AGP Where AGP.GroupUnder='" & ReportFrm.FGetCode(9) & "') "
            StrSQLQuery += "Or AG.GroupCode='" & ReportFrm.FGetCode(9) & "') "

            StrSQLQuery += "Group By (Case When IfNull(AG1.GroupCode,'')<>'' Then 'A+' || IfNull(AG1.GroupCode,'') "
            StrSQLQuery += "When IfNull(AG.GroupUnder,'')='" & ReportFrm.FGetCode(9) & "' Then 'A+' || IfNull(AG.GroupCode,'') "
            StrSQLQuery += "Else 'S+' || IfNull(SG.SubCode,'')  End) "

            StrSQLQuery += StrConditionZeroBal

            StrSQLQuery += "Order By Max(Case When IfNull(AG1.GroupCode,'')<>'' Then IfNull(AG1.GroupName,'') "
            StrSQLQuery += "When IfNull(AG.GroupUnder,'')='" & ReportFrm.FGetCode(9) & "' Then IfNull(AG.GroupName,'') "
            StrSQLQuery += "Else IfNull(SG.Name,'') || ' - ' || IfNull(CT.CityName,'') End) "

            DTTemp = AgL.FillData(StrSQLQuery, AgL.GCn).tables(0)


            DblDebit_Total = 0
            DblCredit_Total = 0
            For I = 0 To DTTemp.Rows.Count - 1
                DTReport.Rows.Add()
                DTReport.Rows(I)(GRCode) = Mid(AgL.XNull(DTTemp.Rows(I).Item("GroupCode")), 3, Len(AgL.XNull(DTTemp.Rows(I).Item("GroupCode"))))
                DTReport.Rows(I)(GRName) = AgL.XNull(DTTemp.Rows(I).Item("GName"))
                DTReport.Rows(I)(Debit) = IIf(AgL.VNull(DTTemp.Rows(I).Item("AmtDr")) > 0, Format(AgL.VNull(DTTemp.Rows(I).Item("AmtDr")), "0.00"), DBNull.Value)
                DTReport.Rows(I)(Credit) = IIf(AgL.VNull(DTTemp.Rows(I).Item("AmtCr")) > 0, Format(AgL.VNull(DTTemp.Rows(I).Item("AmtCr")), "0.00"), DBNull.Value)
                DblDebit_Total = DblDebit_Total + Format(AgL.VNull(DTTemp.Rows(I).Item("AmtDr")), "0.00")
                DblCredit_Total = DblCredit_Total + Format(AgL.VNull(DTTemp.Rows(I).Item("AmtCr")), "0.00")
            Next

            ReportFrm.Text = "Financial Display - " + ReportFrm.FGetText(0)
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = "ProcFinancialDisplay"
            ReportFrm.IsManualAggregate = False

            DsReport = New DataSet()
            DsReport.Tables.Add(DTReport)
            ReportFrm.ProcFillGrid(DsReport)

            ReportFrm.DGL1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None

            ReportFrm.DGL1.Columns(GRName).Width = 500
            'ReportFrm.DGL1.Columns(Opening).Width = 100
            'ReportFrm.DGL1.Columns(DR_CR_OP).Width = 20
            'ReportFrm.DGL1.Columns(DR_CR_OP).HeaderText = ""
            ReportFrm.DGL1.Columns(Debit).Width = 300
            ReportFrm.DGL1.Columns(Credit).Width = 300
            'ReportFrm.DGL1.Columns(Closing).Width = 100
            'ReportFrm.DGL1.Columns(DR_CR_CL).Width = 20
            'ReportFrm.DGL1.Columns(DR_CR_CL).HeaderText = ""
        Catch ex As Exception
            If Not ex.Message.Contains("Index was out of range") Then
                MsgBox(ex.Message)
            End If
        End Try
    End Sub
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
                DTReport.Columns.Add(LGRCode)
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
            ElseIf DspType = ReportType.DetailTrialBalance Then
                DTReport.Columns.Add(GRCode)
                'DTReport.Columns.Add(DocId)
                DTReport.Columns.Add(GRName)
            If ReportFrm.FGetText(8).ToString.Contains(AddColumn.LinkedSubCode) Then
                DTReport.Columns.Add(LGRName)
                DTReport.Columns.Add(LGRCode)
            End If
            If ReportFrm.FGetText(8).ToString.Contains(AddColumn.AccountGroup) Then
                DTReport.Columns.Add(LGroupName)
            End If
            If ReportFrm.FGetText(8).ToString.Contains(AddColumn.AccountType) Then
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
            'DTReport.Columns.Add(Closing)
            'DTReport.Columns.Add(GR_SG)
        End If
    End Sub
    Private Sub ObjRepFormGlobal_FilterApplied() Handles ReportFrm.FilterApplied
        Select Case ReportFrm.FGetText(0)
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
        If ReportFrm.DGL2.ColumnCount = ReportFrm.DGL1.ColumnCount Then
            ReportFrm.DGL2.Item(ReportFrm.DGL1.Columns(Opening).Index, 0).Value = ""
            ReportFrm.DGL2.Item(ReportFrm.DGL1.Columns(Closing).Index, 0).Value = ""
        End If
    End Sub
    Private Sub FormatTrialBalance(bFormatOn As FormattingOn)
        For I As Integer = 0 To ReportFrm.DGL1.Rows.Count - 1
            ReportFrm.DGL1.Rows(I).DefaultCellStyle.Font = New Font("Verdana", 9, FontStyle.Bold)
        Next
    End Sub
    Private Sub FormatBalanceSheet(bFormatOn As FormattingOn)
        For I As Integer = 0 To ReportFrm.DGL1.Rows.Count - 1
            ReportFrm.DGL1.Rows(I).DefaultCellStyle.Font = New Font("Verdana", 9, FontStyle.Bold)
        Next
    End Sub
    Private Sub FormatProfitAndLoss(bFormatOn As FormattingOn)
        For I As Integer = 0 To ReportFrm.DGL1.Rows.Count - 1
            ReportFrm.DGL1.Rows(I).DefaultCellStyle.Font = New Font("Verdana", 9, FontStyle.Bold)
            If AgL.XNull(ReportFrm.DGL1.Item(GRName, I).Value) = "Gross Profit" Then
                ReportFrm.DGL1.Rows(I + 2).DefaultCellStyle.BackColor = Color.LightGray
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
        If ReportFrm.FGetText(0) <> "Ledger" Then
            ProcFinancialDisplay()
        End If
    End Sub
End Class
