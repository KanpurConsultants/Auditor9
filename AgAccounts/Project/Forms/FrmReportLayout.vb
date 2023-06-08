Imports CrystalDecisions.CrystalReports.Engine
Imports System.Text
Imports System.Data.SQLite

Public Class FrmReportLayout
#Region "General Variable Declaration Don't Change It."
    '********************************* By VineetJ 8************************************
    '============= This Region Contains General Variable Declaration ==================
    '============= It Is Recommended Not To Change/ Remove This Section ===============
    '============= Until Unless You Have Proper Knowledge Of ==========================
    '============= What Is Going Through In The code ==================================
    '**********************************************************************************
    Private Enum FilterCodeType
        DTNone = 0
        DTNumeric = 1
        DTString = 2
    End Enum
    '=======================================
    '======== For DataType In Grid =========
    '================ Start ================
    '=======================================
    Private Enum FGDataType
        DT_Date = 0
        DT_Numeric = 1
        DT_Float = 2
        DT_String = 3
        DT_None = 4
        DT_Selection_Single = 5
        DT_Selection_Multiple = 6
    End Enum
    '=======================================
    '======== For DataType In Grid =========
    '================ End ================
    '=======================================

    '=======================================
    '===== For FGMain Columns In Grid ======
    '================ Start ================
    '=======================================
    Private Const GField As Byte = 0
    Private Const GFilter As Byte = 1
    Private Const GButton As Byte = 2
    Private Const GFilterCode As Byte = 3
    Private Const GFilterCodeDataType As Byte = 4
    Private Const GDataType As Byte = 5
    Private Const GDisplayOnReport As Byte = 6
    '=======================================
    '===== For FGMain Columns In Grid ======
    '================= End =================
    '=======================================

    Private StrReportFor As String
    Private IntFrmWidth As Integer
    Private IntFrmHeight As Integer
    Dim FRH_Single() As DMHelpGrid.FrmHelpGrid
    Dim FRH_Multiple() As DMHelpGrid.FrmHelpGrid_Multi
    Dim RptMain As ReportDocument
    Dim StrSQLQuery As String
#End Region
#Region "General Functions/Procedures Declaration Don't Change It."
    '********************************* By VineetJ *************************************
    '============= This Region Contains General Functions/Procedures Declaration ======
    '============= It Is Recommended Not To Change/ Remove This Section ===============
    '============= Until Unless You Have Proper Knowledge Of ==========================
    '============= What Is Going Through In The code ==================================
    '**********************************************************************************

    Sub New(ByVal StrReportForVar As String, ByVal StrFormCaption As String, ByVal IntRowsNeededVar As Int16, Optional ByVal IntFrmWidthVar As Integer = 554, _
    Optional ByVal IntFrmHeightVar As Integer = 498, Optional ByVal IntFieldWidth As Integer = 143, Optional ByVal IntFilterWidth As Integer = 300)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        IntFrmHeight = IntFrmHeightVar
        IntFrmWidth = IntFrmWidthVar
        StrReportFor = Trim(UCase(StrReportForVar))
        Me.Text = StrFormCaption
        Agl.GridDesign(FGMain)
        GlobalIniGrid(IntFieldWidth, IntFilterWidth)
        FGMain.Rows.Add(IntRowsNeededVar)
        ReDim FRH_Single(IntRowsNeededVar)
        ReDim FRH_Multiple(IntRowsNeededVar)
        IniGrid()
    End Sub
    'This Procedure Is For Designing Grid Globaly Used In Every Report
    Private Sub GlobalIniGrid(ByVal IntFieldWidth As Integer, ByVal IntFilterWidth As Integer)
        AgCl.AddAgTextColumn(FGMain, "Field", IntFieldWidth, 0, "Field", True, True, False)
        AgCl.AddAgTextColumn(FGMain, "Filter", IntFilterWidth, 0, "Filter", True, False, False)
        FGMain.Columns.Add("Button", "")
        FGMain.Columns(GButton).Width = 27
        FGMain.Columns(GButton).ReadOnly = True
        AgCl.AddAgTextColumn(FGMain, "FilterCode", 0, 0, "FilterCode", False, True, False)
        AgCl.AddAgTextColumn(FGMain, "FilterCodeDataType", 0, 0, "FilterCodeDataType", False, True, False)
        AgCl.AddAgTextColumn(FGMain, "DataType", 0, 0, "DataType", False, True, False)
        AgCl.AddAgTextColumn(FGMain, "", 25, 0, "", True, True, False)
        FGMain.Anchor = (AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Bottom)
        FGMain.AllowUserToAddRows = False
        FGMain.BackgroundColor = Color.White
        FGMain.Columns(GField).DefaultCellStyle.BackColor = Color.FromArgb(230, 230, 250)

        FGMain.DefaultCellStyle.Font = New Font("Arial", 9, FontStyle.Regular)
        FGMain.ColumnHeadersDefaultCellStyle.Font = New Font("Arial", 9, FontStyle.Regular)
        FGMain.Columns(GField).DefaultCellStyle.Font = New Font("Arial", 9, FontStyle.Regular)
        FGMain.Columns(GDisplayOnReport).DefaultCellStyle.Font = New Font("wingdings", 12, FontStyle.Regular)
        FGMain.Columns(GDisplayOnReport).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        FGMain.Columns(GDisplayOnReport).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
    End Sub
    Private Sub FHPGD_Show_Single(ByRef e As System.Windows.Forms.KeyEventArgs)
        Dim StrSendText As String

        If Not CMain.FGrdDisableKeys(e) Then Exit Sub
        StrSendText = Cmain.FSendText(FGMain, Chr(e.KeyCode))

        FRH_Single(FGMain.CurrentCell.RowIndex).StartPosition = FormStartPosition.CenterScreen
        FRH_Single(FGMain.CurrentCell.RowIndex).ShowDialog()

        If FRH_Single(FGMain.CurrentCell.RowIndex).BytBtnValue = 0 Then
            If Not FRH_Single(FGMain.CurrentCell.RowIndex).DRReturn.Equals(Nothing) Then
                FGMain(GFilter, FGMain.CurrentCell.RowIndex).Value = FRH_Single(FGMain.CurrentCell.RowIndex).DRReturn.Item(1)
                FGMain(GFilterCode, FGMain.CurrentCell.RowIndex).Value = FRH_Single(FGMain.CurrentCell.RowIndex).DRReturn.Item(0)
            End If
        End If
    End Sub
    Private Sub FHPGD_Show_Multiple(ByRef e As System.Windows.Forms.KeyEventArgs)
        Dim StrSendText As String
        Dim StrPrefix As String = "", StrSufix As String = "", StrSeprator As String = ""

        If Not CMain.FGrdDisableKeys(e) Then Exit Sub
        StrSendText = Cmain.FSendText(FGMain, Chr(e.KeyCode))

        If Val(FGMain(GFilterCodeDataType, FGMain.CurrentCell.RowIndex).Value) = FilterCodeType.DTString Then
            StrPrefix = "'"
            StrSufix = "'"
            StrSeprator = ","
        ElseIf Val(FGMain(GFilterCodeDataType, FGMain.CurrentCell.RowIndex).Value) = FilterCodeType.DTNumeric Then
            StrPrefix = ""
            StrSufix = ""
            StrSeprator = ","
        End If

        FRH_Multiple(FGMain.CurrentCell.RowIndex).StartPosition = FormStartPosition.CenterScreen
        FRH_Multiple(FGMain.CurrentCell.RowIndex).ShowDialog()

        If FRH_Multiple(FGMain.CurrentCell.RowIndex).BytBtnValue = 0 Then
            FGMain(GFilter, FGMain.CurrentCell.RowIndex).Value = FRH_Multiple(FGMain.CurrentCell.RowIndex).FFetchData(2, "", "", ",")
            FGMain(GFilterCode, FGMain.CurrentCell.RowIndex).Value = FRH_Multiple(FGMain.CurrentCell.RowIndex).FFetchData(1, StrPrefix, StrSufix, StrSeprator, True)
        End If
    End Sub
    Private Sub FSetValue(ByVal IntRow As Int16, ByVal StrField As String, _
    ByVal BytDataType As FGDataType, ByVal FCTType As FilterCodeType, Optional ByVal StrDefaultValue As String = "", _
    Optional ByVal BlnDisplayOnReport As Boolean = True)

        Dim BtnCell As DataGridViewButtonCell
        Dim StrArray() As String

        FGMain(GField, IntRow).Value = StrField
        FGMain(GDataType, IntRow).Value = BytDataType
        FGMain(GFilterCodeDataType, IntRow).Value = FCTType
        If StrDefaultValue <> "" Then
            StrArray = Split(StrDefaultValue, "|")
            FGMain(GFilter, IntRow).Value = StrArray(0)
            If UBound(StrArray) > 0 Then
                FGMain(GFilterCode, IntRow).Value = StrArray(1)
            End If
        End If

        If BytDataType = FGDataType.DT_Selection_Multiple Or BytDataType = FGDataType.DT_Selection_Single Then
            BtnCell = New DataGridViewButtonCell
            BtnCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
            BtnCell.Style.SelectionBackColor = Color.WhiteSmoke
            BtnCell.Style.BackColor = Color.WhiteSmoke
            BtnCell.Style.ForeColor = Color.BlueViolet
            BtnCell.Style.Font = New Font("Webdings", 9, FontStyle.Regular)
            BtnCell.Value = "6"
            BtnCell.FlatStyle = FlatStyle.Popup
            FGMain(GButton, IntRow) = BtnCell
        End If
        If BlnDisplayOnReport Then
            FGMain(GDisplayOnReport, IntRow).Value = "þ"
        Else
            FGMain(GDisplayOnReport, IntRow).Value = "o"
        End If
    End Sub
    Private Sub FManageTick()
        If FGMain.CurrentCell.RowIndex < 0 Then Exit Sub
        If FGMain.CurrentCell.ColumnIndex <> GDisplayOnReport Then Exit Sub

        If FGMain(GDisplayOnReport, FGMain.CurrentCell.RowIndex).Value = "þ" Then
            FGMain(GDisplayOnReport, FGMain.CurrentCell.RowIndex).Value = "o"
        Else
            FGMain(GDisplayOnReport, FGMain.CurrentCell.RowIndex).Value = "þ"
        End If
    End Sub
    Private Sub FrmReportLayout_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        AgL.WinSetting(Me, IntFrmHeight, IntFrmWidth, 0, 0)
        AgL.PubSiteListCharIndex = " CharIndex('" & AgL.PubSiteCode & "', IfNull(SiteList,'')) > 0 "
        FGMain.AgSkipReadOnlyColumns = True
    End Sub
    Private Sub FrmReportLayout_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles MyBase.Paint
        AgL.FPaintForm(Me, e, 0)
    End Sub
    Private Sub FGMain_CellBeginEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles FGMain.CellBeginEdit
        Select Case Val(FGMain(GDataType, e.RowIndex).Value)
            Case FGDataType.DT_None, FGDataType.DT_Selection_Single, FGDataType.DT_Selection_Multiple
                e.Cancel = True
        End Select
    End Sub

    Private Sub FGMain_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles FGMain.CellClick
        FManageTick()
    End Sub
    Private Sub FGMain_CellContentClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles FGMain.CellContentClick
        Select Case FGMain.CurrentCell.ColumnIndex
            Case GButton
                FGMain(GFilter, FGMain.CurrentCell.RowIndex).Selected = True
                If FGMain(GDataType, e.RowIndex).Value = FGDataType.DT_Selection_Multiple Then
                    FHPGD_Show_Multiple(New System.Windows.Forms.KeyEventArgs(Keys.A))
                ElseIf FGMain(GDataType, e.RowIndex).Value = FGDataType.DT_Selection_Single Then
                    FHPGD_Show_Single(New System.Windows.Forms.KeyEventArgs(Keys.A))
                End If
        End Select
    End Sub
    Private Sub FGMain_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles FGMain.CellEndEdit
        If FGMain(GDataType, e.RowIndex).Value = FGDataType.DT_Date Then
            FGMain(GFilter, e.RowIndex).Value = AgL.RetDate(FGMain(GFilter, e.RowIndex).Value)
        End If
    End Sub
    Private Sub FGMain_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles FGMain.EditingControlShowing
        If TypeOf e.Control Is AgControls.AgTextBox Then
            RemoveHandler DirectCast(e.Control, AgControls.AgTextBox).KeyPress, AddressOf FGrdNumPress
            AddHandler DirectCast(e.Control, AgControls.AgTextBox).KeyPress, AddressOf FGrdNumPress
        End If
    End Sub
    Private Sub FGrdNumPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Select Case FGMain.CurrentCell.ColumnIndex
            Case GFilter
                If FGMain(GDataType, FGMain.CurrentCell.RowIndex).Value = FGDataType.DT_Float Then
                    CMain.NumPress(sender, e, 10, 4, False)
                ElseIf FGMain(GDataType, FGMain.CurrentCell.RowIndex).Value = FGDataType.DT_Numeric Then
                    CMain.NumPress(sender, e, 10, 0, False)
                End If
        End Select
    End Sub
    Private Sub FGMain_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FGMain.KeyDown
        If e.Control Or e.Shift Or e.Alt Then Exit Sub
        Try
            Select Case FGMain.CurrentCell.ColumnIndex
                Case GFilter
                    Select Case Val(FGMain(GDataType, FGMain.CurrentCell.RowIndex).Value)
                        Case FGDataType.DT_Selection_Single
                            FHPGD_Show_Single(e)
                        Case FGDataType.DT_Selection_Multiple
                            FHPGD_Show_Multiple(e)
                    End Select
                Case GDisplayOnReport
                    If e.KeyCode = Keys.Space Then
                        FManageTick()
                    End If
            End Select

            If FGMain.Rows.Count - 1 = FGMain.CurrentCell.RowIndex Then
                If e.KeyCode = Keys.Enter Then
                    BtnPrint.Focus()
                End If
            End If
        Catch Ex As NullReferenceException
        Catch Ex As Exception
            MsgBox("System Exception : " & vbCrLf & Ex.Message)
        End Try
    End Sub
    Private Sub BtnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnClose.Click
        Me.Close()
    End Sub
    Private Function FIsValid(ByVal IntRow As Integer, Optional ByVal StrMsg As String = "Invalid Data") As Boolean
        Dim BlnRtn As Boolean = True

        If FGMain(GFilter, IntRow).Value = "" Then
            MsgBox(FGMain(GField, IntRow).Value + " : " + vbCrLf + StrMsg)
            FGMain(GFilter, IntRow).Selected = True
            FGMain.Focus()
            BlnRtn = False
        End If
        Return BlnRtn
    End Function
    Private Sub FLoadMainReport(ByVal StrReportName As String, ByVal DTTable As DataTable)
        RptMain = New ReportDocument
        DTTable.WriteXmlSchema(AgL.PubReportPath & "\" & StrReportName & ".xml")
        RptMain.Load(AgL.PubReportPath & "\" & StrReportName & ".rpt")
        RptMain.SetDataSource(DTTable)
    End Sub
    Private Sub FLoadSubReport(ByVal StrSubReportName As String, ByVal DTTable As DataTable)
        DTTable.WriteXmlSchema(AgL.PubReportPath & "\" & StrSubReportName & ".xml")
        RptMain.Subreports(StrSubReportName).SetDataSource(DTTable)
    End Sub
#End Region
#Region "FIni_Templete For Programmer Help See It."
    '********************************* By VineetJ *************************************
    '============= This Procedure Is For Help It Holds All The Possible ===============
    '============= Combination This Report Tool Can Work On.See It ====================
    '**********************************************************************************
    Private Sub FIni_Templete()
        'For Date Type Field
        FSetValue(0, "Date", FGDataType.DT_Date, FilterCodeType.DTNone, AgL.PubLoginDate)
        'For Numeric Type Field
        FSetValue(1, "Numeric", FGDataType.DT_Numeric, FilterCodeType.DTNone)
        'For Float Type Field
        FSetValue(2, "Float", FGDataType.DT_Float, FilterCodeType.DTNone)
        'For String Type Field
        FSetValue(3, "String", FGDataType.DT_String, FilterCodeType.DTNone)
        'For None Type Field (User Cannot Change Any Thing In This Type)
        FSetValue(4, "None", FGDataType.DT_None, FilterCodeType.DTNone, "Default")

        'For Party Multiple Selection From DataBase
        FSetValue(5, "Party Name Mutil Sel.", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, "All")
        FRH_Multiple(5) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable(
                          "Select 'o' As Tick,SG.SubCode,SG.Name,Sg.ManualCode From SubGroup SG Order By SG.Name",
                          AgL.GCn)), "", 600, 660, , , False)
        FRH_Multiple(5).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(5).FFormatColumn(1, , 0, , False)
        FRH_Multiple(5).FFormatColumn(2, "Name", 440, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple(5).FFormatColumn(3, "Code", 100, DataGridViewContentAlignment.MiddleLeft)

        'For Godown (From Database) Single Selection
        FSetValue(6, "Godown DB Single Sel.", FGDataType.DT_Selection_Single, FilterCodeType.DTString)
        FRH_Single(6) = New DMHelpGrid.FrmHelpGrid(New DataView(CMain.FGetDatTable(
                        "Select GM.GodCode,GM.GodName From GodownMast GM Order By GM.GodName",
                        AgL.GCn)), "", 300, 300, , , False)
        FRH_Single(6).FFormatColumn(0, , 0, , False)
        FRH_Single(6).FFormatColumn(1, "Name", 200, DataGridViewContentAlignment.MiddleLeft)

        'For Item (From Temporary Table) Single Selection 
        Dim DTTemp As New DataTable
        DTTemp.Columns.Add("Code", System.Type.GetType("System.String"))
        DTTemp.Columns.Add("Name", System.Type.GetType("System.String"))

        DTTemp.Rows.Add(New Object() {"Detail", "Detail"})
        DTTemp.Rows.Add(New Object() {"Summary", "Summary"})

        FSetValue(7, "Report Type Tmp Single Sel.", FGDataType.DT_Selection_Single, FilterCodeType.DTNone, "Detail")
        FRH_Single(7) = New DMHelpGrid.FrmHelpGrid(New DataView(DTTemp), "", 220, 200, , , False)
        FRH_Single(7).FFormatColumn(0, , 0, , False)
        FRH_Single(7).FFormatColumn(1, "Name", 100, DataGridViewContentAlignment.MiddleLeft)
    End Sub
#End Region
    '************************** By VineetJ *************************
    '============ Programmers May Add There Code Below ============= 
    '***************************************************************
#Region "Programmers Can Declare There Variables Here."

#End Region

    Private Sub IniGrid()
        Try
            Select Case StrReportFor
                Case UCase("DailyTransBook")
                    FIni_DailyTransBook()
                Case UCase("MonthlyLedgerSummaryFull")
                    FINI_MonthlyLedgerSummaryFull()
                Case UCase("TrialDetailDrCr")
                    FINI_TrialDetailDrCr()
                Case UCase("MonthlyLedgerSummary")
                    FINI_MonthlyLedgerSummary()
                Case UCase("InterestLedger")
                    FINI_InterestLedger()
                Case UCase("FBTReport")
                    FINI_FBTReport()
                Case UCase("PartyWiseTDSReport")
                    FINI_PartyWiseTDSReport()
                Case UCase("TDSCategoryWiseReport")
                    FINI_TDSCategoryWiseReport()
                Case UCase("FixedAssetRegister")
                    FIni_FixedAssetRegister()
                Case UCase("Ledger")
                    FIni_Ledger()
                Case UCase("TrialGroup")
                    FIni_TrialGroup()
                Case UCase("TrialDetail")
                    FIni_TrialDetail()
                Case UCase("CashBook")
                    FIni_Bank_CashBook("'CP','CR'")
                Case UCase("BankBook")
                    FIni_Bank_CashBook("'BP','BR'")
                Case UCase("Annexure")
                    FIni_Annexure()
                Case UCase("Journal")
                    FIni_journal()
                Case UCase("Daybook")
                    FINI_DayBook()
                Case UCase("Ageing")
                    FINI_Ageing()
                Case UCase("BillWsOSAgeing")
                    FINI_BillWsOSAgeing("Sundry Debtors")
                Case UCase("BillWsOS_Dr")
                    FINI_BillWsOS("Sundry Debtors")
                Case UCase("BillWsOS_Cr")
                    FINI_BillWsOS("Sundry Creditors")
                Case UCase("CashFlow"), UCase("FundFlow")
                    FINI_CASH_FundFlow()
                Case UCase("MonthlyExpenses")
                    FINI_MonthlyExpenses()
                Case UCase("FIFOWsOS_Dr")
                    FINI_FIFOWsOS_DR()
                Case UCase("FIFOWsOS_Cr")
                    FINI_FIFOWsOS_Cr()
                Case UCase("Stock_Valuation")
                    FINI_StockValuation()
                Case UCase("DailyExpenseRegister")
                    FIni_DailyExpenseReg()
                Case UCase("DailyCollectionRegister")
                    FIni_DailyCollection()
                Case UCase("LedgerGrMergeLedger")
                    FIni_LedgerGrMergeLedger()
                Case UCase("AccountGrMergeLedger")
                    FIni_AccountGrMergeLedger()
                Case UCase("GTAReg")
                    FINI_GTAReg()
                Case UCase("BillWiseAdj")
                    FINI_BillWiseAdj()
                Case UCase("TDSTaxChallan")
                    FINI_TDSTaxChallan()
                Case UCase("AccountGrpWsOSAgeing")
                    FINI_AccountGrpWsOSAgeing("AccountGrpWsOSAgeing")
                Case UCase("IntCalForDebtors")
                    FINI_IntCalForDebtors()
                Case UCase("SalesTaxClubbing")
                    FINI_IntSalesTaxClubbing()
            End Select

            If FGMain.Rows.Count > 0 Then
                FGMain(GFilter, 0).Selected = True
            End If
            IniGrid_SetDefaultValue()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub BtnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnPrint.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Select Case StrReportFor
                Case UCase("DailyTransBook")
                    FDailyTransBook()
                Case UCase("MonthlyLedgerSummaryFull")
                    FMonthlyLedgerSummaryFull()
                Case UCase("TrialDetailDrCr")
                    FTrialDetailDrCr()
                Case UCase("MonthlyLedgerSummary")
                    FMonthlyLedgerSummary()
                Case UCase("InterestLedger")
                    FInterestLedger()
                Case UCase("FBTReport")
                    FFBTReport()
                Case UCase("PartyWiseTDSReport")
                    FPartyWiseTDSReport()
                Case UCase("TDSCategoryWiseReport")
                    FTDSCategoryWiseReport()
                Case UCase("FixedAssetRegister")
                    FFixedAssetRegister()
                Case UCase("Ledger")
                    FLedger()
                Case UCase("TrialGroup")
                    FTrialGroup()
                Case UCase("TrialDetail")
                    FTrialDetail()
                Case UCase("CashBook")
                    If Trim(FGMain(GFilterCode, 6).Value) = "D" Then
                        FCashBook()
                    ElseIf Trim(FGMain(GFilterCode, 6).Value) = "J" Then
                        FCashBank_JournalBook()
                    Else
                        FBank_CashBookSingle()
                    End If
                Case UCase("BankBook")
                    If Trim(FGMain(GFilterCode, 6).Value) = "D" Then
                        FBankBook()
                    ElseIf Trim(FGMain(GFilterCode, 6).Value) = "J" Then
                        FCashBank_JournalBook()
                    Else
                        FBank_CashBookSingle()
                    End If
                Case UCase("Annexure")
                    FAnnexure()
                Case UCase("DayBook")
                    FDayBook()
                Case UCase("Journal")
                    FJournal()
                Case UCase("Ageing")
                    If AgL.PubServerName = "" Then
                        FAgeing()
                    Else
                        FAgeingSqlServer()
                    End If
                Case UCase("BillWsOSAgeing")
                    If AgL.PubServerName = "" Then
                        FBillWsOSAgeing("AmtDr", "AmtCr", "Sundry Debtors")
                    Else
                        FBillWsOSAgeingSqlServer("AmtDr", "AmtCr", "Sundry Debtors")
                    End If
                Case UCase("BillWsOS_Dr")
                    FBillWsOS("AmtDr", "AmtCr", "Sundry Debtors")
                Case UCase("BillWsOS_Cr")
                    FBillWsOS("AmtCr", "AmtDr", "Sundry Creditors")
                Case UCase("CashFlow")
                    FCash_Fund_Flow(1)
                Case UCase("FundFlow")
                    FCash_Fund_Flow(2)
                Case UCase("MonthlyExpenses")
                    FMonthlyExpenses()
                Case UCase("FIFOWsOS_Dr")
                    FFIFOWsOS_Dr()
                    'If AgL.PubServerName = "" Then
                    '    FFIFOWsOS_Dr()
                    'Else
                    '    FFIFOWsOS_DrSqlServer()
                    'End If
                Case UCase("FIFOWsOS_Cr")
                    FFIFOWsOS_Cr()
                Case UCase("Stock_Valuation")
                    FStockValuation()
                Case UCase("DailyExpenseRegister")
                    FDailyExpenseReg()
                Case UCase("DailyCollectionRegister")
                    FDailyCollectionReg()
                Case UCase("LedgerGrMergeLedger")
                    FLedgerGrMergeLedger()
                Case UCase("AccountGrMergeLedger")
                    FAccountGrMergeLedger()
                Case UCase("GTAReg")
                    FGTAReg()
                Case UCase("BillWiseAdj")
                    FBillWiseAdj()
                Case UCase("TDSTaxChallan")
                    FTDSTaxChallan()
                Case UCase("AccountGrpWsOSAgeing")
                    If AgL.PubServerName = "" Then
                        FAccountGrpWsOSAgeing()
                    Else
                        FAccountGrpWsOSAgeingSqlServer()
                    End If

                Case UCase("IntCalForDebtors")
                    FIntCalForDebtors()
                Case UCase("SalesTaxClubbing")
                    FSalesTaxClubbing()
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Me.Cursor = Cursors.Arrow
    End Sub
    Private Sub FINI_AccountGrpWsOSAgeing(ByVal StrReportFor As String)
        Dim StrSQL As String = ""

        FSetValue(0, "As On Date", FGDataType.DT_Date, FilterCodeType.DTNone, AgL.PubLoginDate)

        StrSQL = "Select 'o' As Tick,Ag.GroupCode,Ag.GroupName FROM AcGroup  AG "
        StrSQL += "Where AG.Nature='Customer' "
        StrSQL += "Order By AG.GroupName "
        FSetValue(1, "Account Group", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, "All")
        FRH_Multiple(1) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable(StrSQL, AgL.GCn)), "", 600, 460, , , False)
        FRH_Multiple(1).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(1).FFormatColumn(1, , 0, , False)
        FRH_Multiple(1).FFormatColumn(2, "Name", 340, DataGridViewContentAlignment.MiddleLeft)


        StrSQL = "Select 'o' As Tick,SG.SubCode,SG.Name,SG.ManualCode,IfNull(CT.CityName,'') AS CityName,AG.GroupName From SubGroup  SG Left Join "
        StrSQL += "AcGroup AG On AG.GroupCode=SG.GroupCode "
        StrSQL += "Left Join City CT On SG.CityCode=CT.CityCode "
        StrSQL += "Where AG.Nature='Customer'  Order By SG.Name"
        FSetValue(2, "Account Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, "All")
        FRH_Multiple(2) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable(StrSQL, AgL.GCn)), "", 600, 860, , , False)
        FRH_Multiple(2).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(2).FFormatColumn(1, , 0, , False)
        FRH_Multiple(2).FFormatColumn(2, "Name", 340, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple(2).FFormatColumn(3, "Code", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple(2).FFormatColumn(4, "City", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple(2).FFormatColumn(5, "Group Name", 200, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(3, "Ist Slabe", FGDataType.DT_Numeric, FilterCodeType.DTNumeric, 30, True)
        FSetValue(4, "IInd Slabe", FGDataType.DT_Numeric, FilterCodeType.DTNumeric, 60, True)
        FSetValue(5, "IIIrd Slabe", FGDataType.DT_Numeric, FilterCodeType.DTNumeric, 90, True)

        StrSQL = "Select 'D' as Code, 'Detail' as Name Union All Select 'S' as Code, 'Summary' as Name "
        FSetValue(6, "Report On Choice", FGDataType.DT_Selection_Single, FilterCodeType.DTNone, "Detail", True)
        FRH_Single(6) = New DMHelpGrid.FrmHelpGrid(New DataView(CMain.FGetDatTable(StrSQL, AgL.GCn)), "", 200, 220, , , False)
        FRH_Single(6).FFormatColumn(0, , 0, , False)
        FRH_Single(6).FFormatColumn(1, "Name", 140, DataGridViewContentAlignment.MiddleLeft)


        FSetValue(7, "Site Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, AgL.PubSiteName & "|'" & AgL.PubSiteCode & "'")
        FRH_Multiple(7) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable(
                          "Select 'o' As Tick,Sm.Code,Sm.Name From Sitemast Sm where code in (" & AgL.PubSiteList & ")   Order By Sm.Name",
                          AgL.GCn)), "", 300, 360, , , False, AgL.PubSiteCode)
        FRH_Multiple(7).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(7).FFormatColumn(1, , 0, , False)
        FRH_Multiple(7).FFormatColumn(2, "Name", 240, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(8, "Division", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, AgL.PubDivName & "|'" & AgL.PubDivCode & "'")
        FRH_Multiple(8) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable(
                          "Select 'o' As Tick,H.Div_Code Code,H.Div_Name Name From Division H where Div_code in (" & AgL.PubDivisionList & ")   Order By H.Div_Name",
                          AgL.GCn)), "", 300, 360, , , False, AgL.PubSiteCode)
        FRH_Multiple(8).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(8).FFormatColumn(1, , 0, , False)
        FRH_Multiple(8).FFormatColumn(2, "Name", 240, DataGridViewContentAlignment.MiddleLeft)
    End Sub
    Private Sub FINI_TDSTaxChallan()
        Dim DTTemp As DataTable
        Dim StrReportForCode As String = ""

        DTTemp = CMain.FGetDatTable("SELECT GroupCode FROM AcGroup WHERE GroupName='" & StrReportFor & "'", AgL.GCn)
        If DTTemp.Rows.Count > 0 Then StrReportForCode = AgL.XNull(DTTemp.Rows(0).Item("GroupCode")) : DTTemp.Rows.Clear()
        DTTemp.Dispose()

        FSetValue(0, "From Date", FGDataType.DT_Date, FilterCodeType.DTNone, AgL.PubStartDate)
        FSetValue(1, "To Date", FGDataType.DT_Date, FilterCodeType.DTNone, AgL.PubLoginDate)

        FSetValue(2, "Category Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, "All", False)
        FRH_Multiple(2) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable("Select 'o' As Tick,Code,Name From TdsCat Order By Name", AgL.GCn)), "", 600, 660, , , False)
        FRH_Multiple(2).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(2).FFormatColumn(1, , 0, , False)
        FRH_Multiple(2).FFormatColumn(2, "Category", 440, DataGridViewContentAlignment.MiddleLeft)


        FSetValue(3, "Site Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, AgL.PubSiteName & "|'" & AgL.PubSiteCode & "'", False)
        FRH_Multiple(3) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable(
                          "Select 'o' As Tick,Sm.Code,Sm.Name From Sitemast Sm where code in (" & AgL.PubSiteList & ")   Order By Sm.Name",
                          AgL.GCn)), "", 300, 360, , , False, AgL.PubSiteCode)
        FRH_Multiple(3).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(3).FFormatColumn(1, , 0, , False)
        FRH_Multiple(3).FFormatColumn(2, "Name", 240, DataGridViewContentAlignment.MiddleLeft)
    End Sub
    Private Sub FTDSTaxChallan()
        Dim StrCondition As String
        Dim DTTemp As DataTable
        Dim StrCnd As String = ""
        If Not FIsValid(0) Then Exit Sub
        If Not FIsValid(1) Then Exit Sub


        StrCondition = " And ( Date(L.V_Date) Between " & AgL.Chk_Date(CDate(FGMain(GFilter, 0).Value.ToString).ToString("s")) & " And " & AgL.Chk_Date(CDate(FGMain(GFilter, 1).Value.ToString).ToString("s")) & " ) "


        If Trim(FGMain(GFilterCode, 2).Value) <> "" Then StrCondition = StrCondition & " And TC.Code In (" & FGMain(GFilterCode, 2).Value & ")"

        If Trim(FGMain(GFilterCode, 3).Value) <> "" Then
            StrCondition += " And  L.Site_Code IN (" & FGMain(GFilterCode, 3).Value & ") "
        Else
            StrCondition += " And  L.Site_Code IN (" & AgL.PubSiteList & ") "
        End If

        StrSQLQuery = "SELECT TC.Name AS TSDCat,Sum(L.TdsOnAmt) AS TdsOnAmt,Sum(L.AmtCr) AS TdsAmt "
        StrSQLQuery += "FROM Ledger L "
        StrSQLQuery += "LEFT JOIN SubGroup SG ON SG.SubCode =L.ContraSub "
        StrSQLQuery += "LEFT JOIN TDSCat TC ON TC.Code=L.TDSCategory "
        StrSQLQuery += "WHERE IfNull(L.TDSCategory,'')<>'' AND IfNull(L.tdsdesc,'')<>'' "
        StrSQLQuery += "AND L.System_Generated ='Y' "
        StrSQLQuery += StrCondition & " GROUP BY TC.Name "

        StrSQLQuery = AgL.GetBackendBasedQuery(StrSQLQuery)
        DTTemp = CMain.FGetDatTable(StrSQLQuery, AgL.GCn)

        If Not DTTemp.Rows.Count > 0 Then MsgBox("No Records Found to Print.") : Exit Sub

        FLoadMainReport("TDSTaxChallan", DTTemp)
        CMain.FormulaSet(RptMain, Me.Text, FGMain)
        CMain.FShowReport(RptMain, Me.MdiParent, Me.Text)
    End Sub
    Private Sub FAccountGrpWsOSAgeing()
        Dim StrCondition1 As String
        Dim StrCondition2 As String
        Dim DTTemp As DataTable
        Dim STRDATE As String = ""
        Dim STROpt As String = ""
        Dim Ist As Integer
        Dim IInd As Integer
        Dim IIIrd As Integer
        Dim StrCnd As String = ""


        StrCondition1 = " Where (LG.V_Date <= " & AgL.Chk_Date(CDate(FGMain(GFilter, 0).Value.ToString).ToString("s")) & " And IfNull(LG.AmtDr,0)>0) And AG.Nature='Customer'  "
        StrCondition2 = " Where (LG.V_Date <= " & AgL.Chk_Date(CDate(FGMain(GFilter, 0).Value.ToString).ToString("s")) & ") And IfNull(LG.AmtCr,0)>0 And IfNull(LG.AmtCr,0)-IfNull(T.AMOUNT,0)<>0 And AG.Nature='Customer'  "

        If Trim(FGMain(GFilterCode, 1).Value) <> "" Then StrCondition1 = StrCondition1 & " And (IfNull(SG.GroupCode,'') In (Select IfNull(AGP.GroupCode,'') From AcGroupPath AGP Where AGP.GroupUnder In (" & FGMain(GFilterCode, 1).Value & ")) Or SG.GroupCode In (" & FGMain(GFilterCode, 1).Value & ")) "
        If Trim(FGMain(GFilterCode, 1).Value) <> "" Then StrCondition2 = StrCondition2 & " And (IfNull(SG.GroupCode,'') In (Select IfNull(AGP.GroupCode,'') From AcGroupPath AGP Where AGP.GroupUnder In (" & FGMain(GFilterCode, 1).Value & ")) Or SG.GroupCode In (" & FGMain(GFilterCode, 1).Value & ")) "

        If Trim(FGMain(GFilterCode, 2).Value) <> "" Then StrCondition1 = StrCondition1 & " And LG.SubCode In (" & FGMain(GFilterCode, 2).Value & ")"
        If Trim(FGMain(GFilterCode, 2).Value) <> "" Then StrCondition2 = StrCondition2 & " And LG.SubCode In (" & FGMain(GFilterCode, 2).Value & ")"

        Ist = Val((FGMain(GFilter, 3).Value.ToString))
        IInd = Val((FGMain(GFilter, 4).Value.ToString))
        IIIrd = Val((FGMain(GFilter, 5).Value.ToString))

        If Trim(FGMain(GFilterCode, 6).Value) = "S" Then
            STROpt = "S"
        Else
            STROpt = "D"
        End If

        If Trim(FGMain(GFilterCode, 7).Value) <> "" Then
            StrCondition1 = StrCondition1 & " And  LG.Site_Code IN (" & FGMain(GFilterCode, 7).Value & ") "
            StrCondition2 = StrCondition2 & " And  LG.Site_Code IN (" & FGMain(GFilterCode, 7).Value & ") "
        Else
            StrCondition1 = StrCondition1 & " And  LG.Site_Code IN (" & AgL.PubSiteList & ") "
            StrCondition2 = StrCondition2 & " And  LG.Site_Code IN (" & FGMain(GFilterCode, 7).Value & ") "
        End If

        If Trim(FGMain(GFilterCode, 8).Value) <> "" Then
            StrCondition1 = StrCondition1 & " And  LG.DivCode IN (" & FGMain(GFilterCode, 8).Value & ") "
            StrCondition2 = StrCondition2 & " And  LG.DivCode IN (" & FGMain(GFilterCode, 8).Value & ") "
        Else
            StrCondition1 = StrCondition1 & " And  LG.DivCode IN (" & AgL.PubDivisionList & ") "
            StrCondition2 = StrCondition2 & " And  LG.DivCode IN (" & AgL.PubDivisionList & ") "
        End If


        STRDATE = AgL.Chk_Date(CDate(FGMain(GFilter, 0).Value.ToString).ToString("s"))


        StrSQLQuery = "Select LG.Docid,LG.V_Date AS V_Date,Max(LG.V_Type) AS V_Type, LG.DivCode || LG.Site_Code || '-' || LG.V_Type || '-' || LG.RecId As Recid,Max(SG.Name) As Party,Max(SG.SubCode) As PartySCode,IfNull(Max(C.CityName),'')  As CityName,Max(AG.GroupName) As AGGroup,Max(AG.GroupCode) As AGCode,Max(SG.CreditDays) AS CrDays,"
        StrSQLQuery = StrSQLQuery + "Sum(LG.AmtDr) As TotAmtDr,"
        StrSQLQuery = StrSQLQuery + "IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LA.Amount),0) as Balance,"
        StrSQLQuery = StrSQLQuery + "CASE WHEN julianday(" & STRDATE & ")  - julianday(Max(LG.V_Date))<=Max(SG.CreditDays) THEN IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LA.Amount),0) END AS UnDueAmt,"
        StrSQLQuery = StrSQLQuery + "CASE WHEN julianday(" & STRDATE & ")  - julianday(Max(LG.V_Date))>Max(SG.CreditDays) THEN IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LA.Amount),0) END AS DueAmt,"
        StrSQLQuery = StrSQLQuery + "MAx(St.name) As SiteName,"
        StrSQLQuery = StrSQLQuery + "CASE WHEN julianday(" & STRDATE & ")  - julianday(Max(LG.V_Date))>=0 AND julianday(" & STRDATE & ")  - julianday(Max(LG.V_Date))<=" & Ist & " THEN IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LA.Amount),0) END AS Ist,"
        StrSQLQuery = StrSQLQuery + "CASE WHEN julianday(" & STRDATE & ")  - julianday(Max(LG.V_Date))>" & Ist & " AND julianday(" & STRDATE & ")  - julianday(Max(LG.V_Date))<=" & IInd & " THEN IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LA.Amount),0) END AS IInd,"
        StrSQLQuery = StrSQLQuery + "CASE WHEN julianday(" & STRDATE & ")  - julianday(Max(LG.V_Date))>" & IInd & " AND julianday(" & STRDATE & ")  - julianday(Max(LG.V_Date))<=" & IIIrd & " THEN IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LA.Amount),0) END AS IIIrd,"
        StrSQLQuery = StrSQLQuery + "CASE WHEN julianday(" & STRDATE & ")  - julianday(Max(LG.V_Date))>" & IIIrd & "  THEN IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LA.Amount),0) END AS IV,0 As UnAdjust," & Ist & " AS IstSlabe,  "
        StrSQLQuery = StrSQLQuery + "" & IInd & " IIndSlab," & IIIrd & " IIIrdSlab,'" & STROpt & "' AS Opt  "
        StrSQLQuery = StrSQLQuery + "From Ledger LG "
        StrSQLQuery = StrSQLQuery + "Left Join SubGroup SG On LG.Subcode=SG.SubCode Left Join "
        StrSQLQuery = StrSQLQuery + "City C on SG.CityCode=C.CityCode Left Join "
        StrSQLQuery = StrSQLQuery + "AcGroup AG ON SG.GroupCode =AG.GroupCode  "
        StrSQLQuery = StrSQLQuery + "Left Join LedgerAdj LA On LG.DocId=LA.Adj_DocID And LG.V_SNo=LA.Adj_V_SNo "
        StrSQLQuery = StrSQLQuery + "LEFT JOIN SiteMast ST ON LG.Site_Code =St.code  "
        StrSQLQuery = StrSQLQuery + "LEFT JOIN Area ZM ON ZM.Code =SG.Area "
        StrSQLQuery = StrSQLQuery + StrCondition1
        StrSQLQuery = StrSQLQuery + "Group By LG.DocId,LG.V_SNo "
        StrSQLQuery = StrSQLQuery + "HAVING(IfNull(Sum(LA.Amount), 0) <> Max(LG.AmtDr))"

        StrSQLQuery = StrSQLQuery + " Union All "

        StrSQLQuery = StrSQLQuery + " SELECT LG.Docid,LG.V_Date AS V_Date,LG.V_Type,LG.DivCode || LG.Site_Code || '-' || LG.V_Type || '-' || LG.RecId As Recid,SG.Name As Party,"
        StrSQLQuery = StrSQLQuery + " SG.SubCode As PartySCode,IfNull(C.CityName,'') As CityName,AG.GroupName As AGGroup,AG.GroupCode As AGCode,0 AS CrDays,  "
        StrSQLQuery = StrSQLQuery + " 0 As TotAmtDr,0 As Balance,0 AS UnDueAmt,0 AS  DueAmt,St.name As SiteName, 0 AS Ist,0 AS IInd,"
        StrSQLQuery = StrSQLQuery + " 0 AS IIIrd,0 AS IV,IfNull(LG.AmtCr,0)-IfNull(T.AMOUNT,0) As UnAdjust," & Ist & " AS IstSlabe, " & IInd & " IIndSlab," & IIIrd & " IIIrdSlab,"
        StrSQLQuery = StrSQLQuery + " '" & STROpt & "'  AS Opt   "
        StrSQLQuery = StrSQLQuery + "From Ledger LG "
        StrSQLQuery = StrSQLQuery + "LEFT JOIN SubGroup SG On SG.SubCode=LG.SubCode "
        StrSQLQuery = StrSQLQuery + "LEFT JOIN City C on SG.CityCode=C.CityCode "
        StrSQLQuery = StrSQLQuery + "LEFT JOIN AcGroup AG ON SG.GroupCode =AG.GroupCode "
        StrSQLQuery = StrSQLQuery + "LEFT JOIN Area ZM ON ZM.Code =SG.Area  "
        StrSQLQuery = StrSQLQuery + "LEFT JOIN SiteMast ST ON LG.Site_Code =St.code   "
        StrSQLQuery = StrSQLQuery + "LEFT JOIN (SELECT LA.Vr_Docid AS Docid,LA.Vr_V_SNo AS S_No,SUM(AMOUNT) AS AMOUNT FROM LedgerAdj LA GROUP BY LA.Vr_DocId,LA.Vr_V_SNo) T ON T.DOCID=LG.DOCID AND T.S_NO=LG.V_SNO  "
        StrSQLQuery = StrSQLQuery + StrCondition2
        StrSQLQuery = StrSQLQuery + "ORDER BY AGGroup,Party,V_Date,Recid  "

        StrSQLQuery = AgL.GetBackendBasedQuery(StrSQLQuery)
        DTTemp = CMain.FGetDatTable(StrSQLQuery, AgL.GCn)
        If Not DTTemp.Rows.Count > 0 Then MsgBox("No Records Found to Print.") : Exit Sub

        FLoadMainReport("DealerWsOSAgeingSummary", DTTemp)

        CMain.FormulaSet(RptMain, Me.Text, FGMain)
        CMain.FShowReport(RptMain, Me.MdiParent, Me.Text)
    End Sub

    Private Sub FAccountGrpWsOSAgeingSqlServer()
        Dim StrCondition1 As String
        Dim StrCondition2 As String
        Dim DTTemp As DataTable
        Dim STRDATE As String = ""
        Dim STROpt As String = ""
        Dim Ist As Integer
        Dim IInd As Integer
        Dim IIIrd As Integer
        Dim StrCnd As String = ""


        StrCondition1 = " Where (LG.V_Date <= " & AgL.Chk_Date(FGMain(GFilter, 0).Value.ToString) & " And IsNull(LG.AmtDr,0)>0) And AG.Nature='Customer'  "
        StrCondition2 = " Where (LG.V_Date <= " & AgL.Chk_Date(FGMain(GFilter, 0).Value.ToString) & ") And IsNull(LG.AmtCr,0)>0 And IsNull(LG.AmtCr,0)-ISNULL(T.AMOUNT,0)<>0 And AG.Nature='Customer'  "

        If Trim(FGMain(GFilterCode, 1).Value) <> "" Then StrCondition1 = StrCondition1 & " And (IsNull(SG.GroupCode,'') In (Select IsNull(AGP.GroupCode,'') From AcGroupPath AGP Where AGP.GroupUnder In (" & FGMain(GFilterCode, 1).Value & ")) Or SG.GroupCode In (" & FGMain(GFilterCode, 1).Value & ")) "
        If Trim(FGMain(GFilterCode, 1).Value) <> "" Then StrCondition2 = StrCondition2 & " And (IsNull(SG.GroupCode,'') In (Select IsNull(AGP.GroupCode,'') From AcGroupPath AGP Where AGP.GroupUnder In (" & FGMain(GFilterCode, 1).Value & ")) Or SG.GroupCode In (" & FGMain(GFilterCode, 1).Value & ")) "

        If Trim(FGMain(GFilterCode, 2).Value) <> "" Then StrCondition1 = StrCondition1 & " And LG.SubCode In (" & FGMain(GFilterCode, 2).Value & ")"
        If Trim(FGMain(GFilterCode, 2).Value) <> "" Then StrCondition2 = StrCondition2 & " And LG.SubCode In (" & FGMain(GFilterCode, 2).Value & ")"

        Ist = Val((FGMain(GFilter, 3).Value.ToString))
        IInd = Val((FGMain(GFilter, 4).Value.ToString))
        IIIrd = Val((FGMain(GFilter, 5).Value.ToString))

        If Trim(FGMain(GFilterCode, 6).Value) = "S" Then
            STROpt = "S"
        Else
            STROpt = "D"
        End If

        If Trim(FGMain(GFilterCode, 7).Value) <> "" Then
            StrCondition1 = StrCondition1 & " And  LG.Site_Code IN (" & FGMain(GFilterCode, 7).Value & ") "
            StrCondition2 = StrCondition2 & " And  LG.Site_Code IN (" & FGMain(GFilterCode, 7).Value & ") "
        Else
            StrCondition1 = StrCondition1 & " And  LG.Site_Code IN (" & AgL.PubSiteList & ") "
            StrCondition2 = StrCondition2 & " And  LG.Site_Code IN (" & FGMain(GFilterCode, 7).Value & ") "
        End If

        If Trim(FGMain(GFilterCode, 8).Value) <> "" Then
            StrCondition1 = StrCondition1 & " And  LG.DivCode IN (" & FGMain(GFilterCode, 8).Value & ") "
            StrCondition2 = StrCondition2 & " And  LG.DivCode IN (" & FGMain(GFilterCode, 8).Value & ") "
        Else
            StrCondition1 = StrCondition1 & " And  LG.DivCode IN (" & AgL.PubDivisionList & ") "
            StrCondition2 = StrCondition2 & " And  LG.DivCode IN (" & AgL.PubDivisionList & ") "
        End If

        STRDATE = AgL.Chk_Date(FGMain(GFilter, 0).Value.ToString)


        StrSQLQuery = "Select LG.Docid,Max(LG.V_Date) AS V_Date,Max(LG.V_Type) AS V_Type,Convert(Varchar,Max(LG.V_No)) AS Recid,Max(SG.Name) As Party,Max(SG.SubCode) As PartySCode,Isnull(Max(C.CityName),'')  As CityName,Max(AG.GroupName) As AGGroup,Max(AG.GroupCode) As AGCode,Max(Sg.CreditDays) AS CrDays,"
        StrSQLQuery = StrSQLQuery + "Sum(LG.AmtDr) As TotAmtDr,"
        StrSQLQuery = StrSQLQuery + "isnull(Sum(LG.AmtDr),0)-IsNull(Sum(LA.Amount),0) as Balance,"
        StrSQLQuery = StrSQLQuery + "CASE WHEN DATEdiff(day,Max(LG.V_date)," & STRDATE & ")<=Max(Sg.CreditDays) THEN isnull(Sum(LG.AmtDr),0)-IsNull(Sum(LA.Amount),0) END AS UnDueAmt,"
        StrSQLQuery = StrSQLQuery + "CASE WHEN DATEdiff(day,Max(LG.V_date)," & STRDATE & ")>Max(Sg.CreditDays) THEN isnull(Sum(LG.AmtDr),0)-IsNull(Sum(LA.Amount),0) END AS DueAmt,"
        StrSQLQuery = StrSQLQuery + "MAx(St.name) As SiteName,"
        StrSQLQuery = StrSQLQuery + "CASE WHEN DATEdiff(day,Max(LG.V_date)," & STRDATE & ")>=0 AND DATEdiff(day,Max(LG.V_date)," & STRDATE & ")<=" & Ist & " THEN isnull(Sum(LG.AmtDr),0)-IsNull(Sum(LA.Amount),0) END AS Ist,"
        StrSQLQuery = StrSQLQuery + "CASE WHEN DATEdiff(day,Max(LG.V_date)," & STRDATE & ")>" & Ist & " AND DATEdiff(day,Max(LG.V_date)," & STRDATE & ")<=" & IInd & " THEN isnull(Sum(LG.AmtDr),0)-IsNull(Sum(LA.Amount),0) END AS IInd,"
        StrSQLQuery = StrSQLQuery + "CASE WHEN DATEdiff(day,Max(LG.V_date)," & STRDATE & ")>" & IInd & " AND DATEdiff(day,Max(LG.V_date)," & STRDATE & ")<=" & IIIrd & " THEN isnull(Sum(LG.AmtDr),0)-IsNull(Sum(LA.Amount),0) END AS IIIrd,"
        StrSQLQuery = StrSQLQuery + "CASE WHEN DATEdiff(day,Max(LG.V_date)," & STRDATE & ")>" & IIIrd & "  THEN isnull(Sum(LG.AmtDr),0)-IsNull(Sum(LA.Amount),0) END AS IV,0 As UnAdjust," & Ist & " AS IstSlabe,  "
        StrSQLQuery = StrSQLQuery + "" & IInd & " IIndSlab," & IIIrd & " IIIrdSlab,'" & STROpt & "' AS Opt  "
        StrSQLQuery = StrSQLQuery + "From Ledger LG "
        StrSQLQuery = StrSQLQuery + "Left Join SubGroup SG On LG.Subcode=SG.SubCode Left Join "
        StrSQLQuery = StrSQLQuery + "City C on SG.CityCode=C.CityCode Left Join "
        StrSQLQuery = StrSQLQuery + "AcGroup AG ON SG.GroupCode =AG.GroupCode  "
        StrSQLQuery = StrSQLQuery + "Left Join LedgerAdj LA On LG.DocId=LA.Adj_DocID And LG.V_SNo=LA.Adj_V_SNo "
        StrSQLQuery = StrSQLQuery + "LEFT JOIN SiteMast ST ON LG.Site_Code =St.code  "
        StrSQLQuery = StrSQLQuery + StrCondition1
        StrSQLQuery = StrSQLQuery + "Group By LG.DocId,LG.V_SNo "
        StrSQLQuery = StrSQLQuery + "HAVING(IsNull(Sum(LA.Amount), 0) <> Max(LG.AmtDr))"

        StrSQLQuery = StrSQLQuery + " Union All "

        StrSQLQuery = StrSQLQuery + " SELECT LG.Docid,LG.V_Date AS V_Date,LG.V_Type,Convert(Varchar,LG.V_No) AS Recid,SG.Name As Party,"
        StrSQLQuery = StrSQLQuery + " SG.SubCode As PartySCode,Isnull(C.CityName,'') As CityName,AG.GroupName As AGGroup,AG.GroupCode As AGCode,0 AS CrDays,  "
        StrSQLQuery = StrSQLQuery + " 0 As TotAmtDr,0 As Balance,0 AS UnDueAmt,0 AS  DueAmt,St.name As SiteName, 0 AS Ist,0 AS IInd,"
        StrSQLQuery = StrSQLQuery + " 0 AS IIIrd,0 AS IV,ISNULL(LG.AmtCr,0)-ISNULL(T.AMOUNT,0) As UnAdjust," & Ist & " AS IstSlabe, " & IInd & " IIndSlab," & IIIrd & " IIIrdSlab,"
        StrSQLQuery = StrSQLQuery + " '" & STROpt & "'  AS Opt   "
        StrSQLQuery = StrSQLQuery + "From Ledger LG "
        StrSQLQuery = StrSQLQuery + "LEFT JOIN SubGroup SG On SG.SubCode=LG.SubCode "
        StrSQLQuery = StrSQLQuery + "LEFT JOIN City C on SG.CityCode=C.CityCode "
        StrSQLQuery = StrSQLQuery + "LEFT JOIN AcGroup AG ON SG.GroupCode =AG.GroupCode "
        StrSQLQuery = StrSQLQuery + "LEFT JOIN SiteMast ST ON LG.Site_Code =St.code   "
        StrSQLQuery = StrSQLQuery + "LEFT JOIN (SELECT LA.Vr_Docid AS Docid,LA.Vr_V_SNo AS S_No,SUM(AMOUNT) AS AMOUNT FROM LedgerAdj LA GROUP BY LA.Vr_DocId,LA.Vr_V_SNo) T ON T.DOCID=LG.DOCID AND T.S_NO=LG.V_SNO  "
        StrSQLQuery = StrSQLQuery + StrCondition2
        StrSQLQuery = StrSQLQuery + "ORDER BY AGGroup,Party,V_Date,Recid  "

        StrSQLQuery = AgL.GetBackendBasedQuery(StrSQLQuery)
        DTTemp = CMain.FGetDatTable(StrSQLQuery, AgL.GCn)
        If Not DTTemp.Rows.Count > 0 Then MsgBox("No Records Found to Print.") : Exit Sub

        FLoadMainReport("DealerWsOSAgeingSummary", DTTemp)

        CMain.FormulaSet(RptMain, Me.Text, FGMain)
        CMain.FShowReport(RptMain, Me.MdiParent, Me.Text)
    End Sub


    Private Sub FINI_IntCalForDebtors()
        FSetValue(0, "From Date", FGDataType.DT_Date, FilterCodeType.DTNone, AgL.PubStartDate)
        FSetValue(1, "To Date", FGDataType.DT_Date, FilterCodeType.DTNone, AgL.PubLoginDate)

        FSetValue(2, "Party Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, "All", False)
        FRH_Multiple(2) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable("Select 'o' As Tick,SG.SubCode,SG.Name,SG.ManualCode From SubGroup SG  Where SG.Nature In ('Customer') Order By SG.Name", AgL.GCn)), "", 600, 820, , , False)
        FRH_Multiple(2).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(2).FFormatColumn(1, , 0, , False)
        FRH_Multiple(2).FFormatColumn(2, "Name", 300, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple(2).FFormatColumn(3, "Code", 100, DataGridViewContentAlignment.MiddleLeft)
        'FRH_Multiple(2).FFormatColumn(4, "Distributor", 300, DataGridViewContentAlignment.MiddleLeft)


        FSetValue(3, "Site Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, AgL.PubSiteName & "|'" & AgL.PubSiteCode & "'", False)
        FRH_Multiple(3) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable(
                          "Select 'o' As Tick,Sm.Code,Sm.Name From Sitemast Sm where code in (" & AgL.PubSiteList & ")   Order By Sm.Name",
                          AgL.GCn)), "", 300, 360, , , False, AgL.PubSiteCode)
        FRH_Multiple(3).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(3).FFormatColumn(1, , 0, , False)
        FRH_Multiple(3).FFormatColumn(2, "Name", 240, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(4, "Interest Rate", FGDataType.DT_Float, FilterCodeType.DTNumeric, , True)
    End Sub
    Private Sub FIntCalForDebtors()
        Dim StrCndBill As String, StrCndPmt As String
        Dim StrCndParty As String, StrCndPmt1 As String
        Dim DTTemp As DataTable

        If Not FIsValid(0) Then Exit Sub
        If Not FIsValid(1) Then Exit Sub
        If Not FIsValid(4) Then Exit Sub

        StrCndBill = " And Date(LG.V_Date) <= " & AgL.Chk_Date(CDate(FGMain(GFilter, 1).Value.ToString).ToString("s")) & " "
        StrCndPmt = " And Date(LG.V_Date) < " & AgL.Chk_Date(CDate(FGMain(GFilter, 0).Value.ToString).ToString("s")) & " "
        StrCndPmt1 = " And ( Date(LG.V_Date) Between " & AgL.Chk_Date(CDate(FGMain(GFilter, 0).Value.ToString).ToString("s")) & " And " & AgL.Chk_Date(CDate(FGMain(GFilter, 1).Value.ToString).ToString("s")) & ") "

        StrCndParty = ""
        If Trim(FGMain(GFilterCode, 2).Value) <> "" Then StrCndParty = " And Max(Tmp.SubCode) In (" & FGMain(GFilterCode, 2).Value & ") "

        If Trim(FGMain(GFilterCode, 3).Value) <> "" Then
            StrCndBill += " And  LG.Site_Code IN (" & FGMain(GFilterCode, 3).Value & ") "
            StrCndPmt += " And  LG.Site_Code IN (" & FGMain(GFilterCode, 3).Value & ") "
            StrCndPmt1 += " And  LG.Site_Code IN (" & FGMain(GFilterCode, 3).Value & ") "
        Else
            StrCndBill += " And  LG.Site_Code IN (" & AgL.PubSiteList & ") "
            StrCndPmt += " And  LG.Site_Code IN (" & AgL.PubSiteList & ") "
            StrCndPmt1 += " And  LG.Site_Code IN (" & AgL.PubSiteList & ") "
        End If

        Dim mQry As String

        mQry = " CREATE Temporary TABLE #TempRecord (Adj_DocId nvarchar(100),Adj_V_SNo INT,V_Type nvarchar(100),RecId nvarchar(100),
                V_Date datetime,AmtDr float, AmtCr float, DueDays Int, PName nvarchar(200), SubCode nvarchar(200))	"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

        mQry = " INSERT INTO #TempRecord 
                Select	LG.DocId As Adj_DocId,LG.V_SNo As Adj_V_SNo,LG.V_Type,LG.RecId,LG.V_Date As V_Date, 
                LG.AmtDr, Null As AmtCr, SG.CreditDays DueDays, SG.Name As PName, SG.SubCode  
                From Ledger LG  
                Left Join SubGroup SG On LG.SubCode=SG.SubCode 
                Where SG.Nature In ('Customer') And IfNull(LG.AmtDr,0)<>0 " + StrCndBill +
                " Union All 
                Select	LGA.Adj_DocId,LGA.Adj_V_SNo,Null As V_Type,Null As RecId, Null As V_Date,Null As AmtDr, 
                LGA.Amount As AmtCr, 0 As DueDays, Null As PName, Null As SubCode 
                From LedgerAdj LGA 
                Left Join Ledger LG On LGA.Vr_DocId=LG.DocId 
                Left Join SubGroup SG On LG.SubCode=SG.SubCode 
                Where SG.Nature In ('Customer') And IfNull(LG.AmtCr,0)<>0 " + StrCndPmt
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)


        StrSQLQuery = "Select	MT.PName,MT.SubCode,"
        StrSQLQuery += "IfNull(RTrim(LTrim(MT.Adj_DocId)),'') || '|' || IfNull(RTrim(LTrim(MT.Adj_V_SNo)),'') As AdjDocId, "
        StrSQLQuery += "MT.V_Type, MT.RecId as V_No, "
        StrSQLQuery += "MT.V_Date,MT.AmtDr,MT.DueDays,LGAT.Vr_DocId, LGAT.Vr_Type, LGAT.Vr_RecId, "
        StrSQLQuery += "LGAT.Vr_V_Date, IfNull(LGAT.Amount,0) As Amount, "
        StrSQLQuery += "" & AgL.Chk_Text(FGMain(GFilter, 0).Value.ToString) & " As FromDate , "
        StrSQLQuery += "" & AgL.Chk_Text(FGMain(GFilter, 1).Value.ToString) & " As UpToDate , "
        StrSQLQuery += "" & Val(FGMain(GFilter, 4).Value.ToString) & " As InterestRate "
        StrSQLQuery += "From ( "
        StrSQLQuery += "Select	Max(Adj_DocId) As Adj_DocId,Max(Adj_V_SNo) As Adj_V_SNo, "
        StrSQLQuery += "Max(V_Type) As V_Type,Max(RecId) As RecId,Max(V_Date) As V_Date, "
        StrSQLQuery += "(IfNull(Sum(AmtDr),0)-IfNull(Sum(AmtCr),0)) As AmtDr,Max(DueDays) As DueDays, "
        StrSQLQuery += "Max(PName) As PName,Max(SubCode) As SubCode "
        StrSQLQuery += "From "
        StrSQLQuery += "( "
        StrSQLQuery += " Select * From #TempRecord "
        StrSQLQuery += ") As Tmp "
        StrSQLQuery += "Group By Adj_DocId,Adj_V_SNo "
        StrSQLQuery += "Having (IfNull(Sum(AmtDr),0)-IfNull(Sum(AmtCr),0))>0 "
        StrSQLQuery += StrCndParty
        StrSQLQuery += ") As MT "
        StrSQLQuery += "Left Join "
        StrSQLQuery += "( "
        StrSQLQuery += "Select	LGA.Adj_DocId,LGA.Adj_V_SNo,LGA.Vr_DocId,LG.V_Type As Vr_Type, "
        StrSQLQuery += "LG.V_No As Vr_RecId,LG.V_Date As Vr_V_Date,LGA.Amount "
        StrSQLQuery += "From LedgerAdj LGA "
        StrSQLQuery += "Left Join Ledger LG On LGA.Vr_DocId=LG.DocId "
        StrSQLQuery += "Left Join SubGroup SG On LG.SubCode=SG.SubCode "
        StrSQLQuery += "Where SG.Nature In ('Customer') And IfNull(LG.AmtCr,0)<>0 "
        StrSQLQuery += StrCndPmt1
        StrSQLQuery += ") As LGAT On LGAT.Adj_DocId=MT.Adj_DocId And LGAT.Adj_V_SNo=MT.Adj_V_SNo "
        StrSQLQuery += "Order By	MT.V_Date,MT.V_Type,MT.RecId,"
        StrSQLQuery += "IfNull(RTrim(LTrim(MT.Adj_DocId)),'') || '|' || IfNull(RTrim(LTrim(MT.Adj_V_SNo)),''), "
        StrSQLQuery += "LGAT.Vr_V_Date,LGAT.Vr_RecId "

        StrSQLQuery = AgL.GetBackendBasedQuery(StrSQLQuery)
        DTTemp = CMain.FGetDatTable(StrSQLQuery, AgL.GCn)

        mQry = "Drop Table #TempRecord"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

        If Not DTTemp.Rows.Count > 0 Then MsgBox("No Records Found to Print.") : Exit Sub

        FLoadMainReport("InterestCalForDebtors", DTTemp)
        CMain.FormulaSet(RptMain, Me.Text, FGMain)
        CMain.FShowReport(RptMain, Me.MdiParent, Me.Text)
    End Sub
    Private Sub FINI_BillWiseAdj()
        Dim StrSql As String
        FSetValue(0, "As On Date", FGDataType.DT_Date, FilterCodeType.DTNone, AgL.PubLoginDate)
        FSetValue(1, "Account Group", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, "All")
        FRH_Multiple(1) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable("Select 'o' As Tick,AG.GroupCode,AG.GroupName From AcGroup  AG " &
                          "Order By AG.GroupName", AgL.GCn)), "", 600, 520, , , False)
        FRH_Multiple(1).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(1).FFormatColumn(1, , 0, , False)
        FRH_Multiple(1).FFormatColumn(2, "Name", 400, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(2, "Account Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, "All", False)
        FRH_Multiple(2) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable(
                          "Select 'o' As Tick,SG.SubCode,SG.Name,Sg.ManualCode,IfNull(CT.CityName,''),AG.GroupName, " &
                          "IfNull(SG.Area,'') From SubGroup SG Left Join " &
                          "AcGroup AG On AG.GroupCode=SG.GroupCode Left Join " &
                          "City CT On SG.CityCode=CT.CityCode Left Join " &
                          "Area ZM On ZM.Code=SG.Area Where  (SG.SiteList Like '%|" & AgL.PubSiteCode & "|%' Or SG.SiteList Is Null)  " &
                          "Order By SG.Name",
                          AgL.GCn)), "", 600, 920, , , False)
        FRH_Multiple(2).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(2).FFormatColumn(1, , 0, , False)
        FRH_Multiple(2).FFormatColumn(2, "Name", 300, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple(2).FFormatColumn(3, "Code", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple(2).FFormatColumn(4, "City", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple(2).FFormatColumn(5, "Group Name", 200, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple(2).FFormatColumn(6, "Area", 100, DataGridViewContentAlignment.MiddleLeft)


        StrSql = "Select 'C' as Code, 'Credit' as Name Union All Select 'D' as Code, 'Debit' as Name "
        FSetValue(3, "Report For", FGDataType.DT_Selection_Single, FilterCodeType.DTNone, "Debit", False)
        FRH_Single(3) = New DMHelpGrid.FrmHelpGrid(New DataView(CMain.FGetDatTable(StrSql, AgL.GCn)), "", 200, 220, , , False)
        FRH_Single(3).FFormatColumn(0, , 0, , False)
        FRH_Single(3).FFormatColumn(1, "Name", 140, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(4, "Site Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, AgL.PubSiteName & "|'" & AgL.PubSiteCode & "'")
        FRH_Multiple(4) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable(
                          "Select 'o' As Tick,Sm.Code,Sm.Name From Sitemast Sm where code in (" & AgL.PubSiteList & ")   Order By Sm.Name",
                          AgL.GCn)), "", 300, 360, , , False, AgL.PubSiteCode)
        FRH_Multiple(4).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(4).FFormatColumn(1, , 0, , False)
        FRH_Multiple(4).FFormatColumn(2, "Name", 240, DataGridViewContentAlignment.MiddleLeft)
    End Sub
    Private Sub FBillWiseAdj()
        Dim StrCondition1 As String
        Dim DTTemp As DataTable
        Dim DrCr As String = ""
        Dim StrAmt1 As String = ""
        Dim StrAmt2 As String = ""

        If Not FIsValid(0) Then Exit Sub

        StrCondition1 = " Where LG.V_Date <= " & AgL.Chk_Date(CDate(FGMain(GFilter, 0).Value.ToString).ToString("s")) & "  "
        If Trim(FGMain(GFilterCode, 1).Value) <> "" Then StrCondition1 = StrCondition1 & " And (IfNull(SG.GroupCode,'') In (Select IfNull(AGP.GroupCode,'') From AcGroupPath AGP Where AGP.GroupUnder In (" & FGMain(GFilterCode, 1).Value & ")) Or SG.GroupCode In (" & FGMain(GFilterCode, 1).Value & ")) "
        If Trim(FGMain(GFilterCode, 2).Value) <> "" Then StrCondition1 = StrCondition1 & " And SG.SubCode In (" & FGMain(GFilterCode, 2).Value & ")"

        If UCase(Trim(FGMain(GFilterCode, 3).Value)) = "C" Then
            DrCr = UCase(Trim(FGMain(GFilterCode, 3).Value))
        Else
            DrCr = "D"
        End If

        If DrCr = "D" Then StrAmt1 = "IfNull(LG.AmtDr,0)"
        If DrCr = "C" Then StrAmt1 = "IfNull(LG.AmtCr,0)"

        If DrCr = "D" Then StrAmt2 = "IfNull(LG.AmtCr,0)"
        If DrCr = "C" Then StrAmt2 = "IfNull(LG.AmtDr,0)"

        If Trim(FGMain(GFilterCode, 4).Value) <> "" Then
            StrCondition1 = StrCondition1 & " And  LG.Site_Code IN (" & FGMain(GFilterCode, 4).Value & ") "
        Else
            StrCondition1 = StrCondition1 & " And  LG.Site_Code IN (" & AgL.PubSiteList & ") "
        End If

        StrSQLQuery = "SELECT DocId,Vr_DocId,VSno,VNo,AdjVNo,VDate AS VDate,AdjDate AS AdjDate, "
        StrSQLQuery += "VType,AdjVType,PName As PName,Narration AS Narr,AdjNarr AS AdjNarr,'" & DrCr & "' As DRCR, "
        StrSQLQuery += "Amt1 AS Amt1,Amt2 AS Amt2,AdjAmt AS AdjAmt,CityName AS CityName,SiteName AS SiteName "
        StrSQLQuery += "FROM ( "
        StrSQLQuery += "Select LG.DocId,LG.V_SNo AS VSno,LG.DivCode || LG.Site_Code || '-' || LG.V_Type || '-' || LG.RecId As VNo,Cast(LG1.V_No as Varchar) As AdjVNo, "
        StrSQLQuery += "LG.V_Date as VDate,LG1.V_Date  AS AdjDate, SG.Name As PName,"
        StrSQLQuery += "LG.Narration as Narration,LG1.Narration  AS AdjNarr," & StrAmt1 & " As Amt1,0 As Amt2, "
        StrSQLQuery += "IfNull(LA1.Amount,0) AS AdjAmt,C.CityName As CityName,(St.name) As SiteName, "
        StrSQLQuery += "LA1.Vr_DocId,LG.V_Type AS VType ,LG1.V_Type AS AdjVType "
        StrSQLQuery += "From  Ledger LG Left Join SubGroup SG On LG.Subcode=SG.SubCode Left Join "
        StrSQLQuery += "City C on SG.CityCode=C.CityCode Left Join LedgerAdj LA1 On LG.DocId=LA1.Adj_DocId And LG.V_SNo=LA1.Adj_V_SNo "
        StrSQLQuery += "LEFT JOIN Ledger LG1 ON LG1.DocId =LA1.Vr_DocId And LG1.V_SNo=LA1.Vr_V_SNo "
        StrSQLQuery += "LEFT JOIN AcGroup AG ON SG.GroupCode =AG.GroupCode  "
        StrSQLQuery += "Left Join SiteMast ST ON LG.Site_Code=St.code "
        StrSQLQuery += StrCondition1 & " And " & StrAmt1 & " > 0 And IfNull(LA1.Amount, 0) <> " & StrAmt1 & " "
        StrSQLQuery += "Union All "
        StrSQLQuery += "Select LG.DocId,LG.V_SNo AS VSno,NULL as VNo,LG.DivCode || LG.Site_Code || '-' || LG.V_Type || '-' || LG.RecId As AdjVNo,LG.V_Date as VDate, "
        StrSQLQuery += "LG.V_Date AS AdjDate,SG.Name As PName,LG.Narration as Narration,LG.Narration AS AdjNarr, "
        StrSQLQuery += "0 As Amt1," & StrAmt2 & "-IfNull(T.AMOUNT,0) as Amt2,0 AS AdjAmt, "
        StrSQLQuery += "C.CityName As CityName,ST.name As sitename, "
        StrSQLQuery += "LG.DocId AS Vr_DocId,LG.V_Type AS VType,LG.V_Type AS AdjVType  "
        StrSQLQuery += "From Ledger LG Left Join SubGroup SG On SG.SubCode=LG.SubCode "
        StrSQLQuery += "Left Join City C on SG.CityCode=C.CityCode "
        StrSQLQuery += "LEFT JOIN (SELECT LA.Vr_Docid AS Docid,LA.Vr_V_SNo AS S_No,SUM(AMOUNT) AS AMOUNT FROM LedgerAdj LA GROUP BY LA.Vr_DocId,LA.Vr_V_SNo) T ON T.DOCID=LG.DOCID AND T.S_NO=LG.V_SNO  "
        StrSQLQuery += "LEFT JOIN AcGroup AG ON SG.GroupCode =AG.GroupCode  "
        StrSQLQuery += "Left Join SiteMast ST ON LG.Site_Code=St.code "
        StrSQLQuery += StrCondition1 & " And " & StrAmt2 & " > 0 And " & StrAmt2 & "-IfNull(T.AMOUNT,0)<>0 "
        StrSQLQuery += ") As Tmp "
        StrSQLQuery += "Order By VDate,AdjDate,DocId,Vr_DocId "

        StrSQLQuery = AgL.GetBackendBasedQuery(StrSQLQuery)
        DTTemp = CMain.FGetDatTable(StrSQLQuery, AgL.GCn)
        If Not DTTemp.Rows.Count > 0 Then MsgBox("No Records Found to Print.") : Exit Sub
        FLoadMainReport("MnuBillWiseAdjReport", DTTemp)

        CMain.FormulaSet(RptMain, Me.Text, FGMain)
        CMain.FShowReport(RptMain, Me.MdiParent, Me.Text)
    End Sub
    Private Sub FINI_StockValuation()
        Dim StrSQL As String

        FSetValue(0, "From Date", FGDataType.DT_Date, FilterCodeType.DTNone, AgL.PubStartDate, True)
        FSetValue(1, "To Date", FGDataType.DT_Date, FilterCodeType.DTNone, AgL.PubLoginDate, True)

        FSetValue(2, "Item Type", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, "All")
        FRH_Multiple(2) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable("SELECT 'o' As Tick, Code,Name FROM ItemType   Order By Name ", AgL.GCn)), "", 400, 320, , , False)
        FRH_Multiple(2).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(2).FFormatColumn(1, , 0, , False)
        FRH_Multiple(2).FFormatColumn(2, "Name", 200, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(3, "Item Category ", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, "All")
        FRH_Multiple(3) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable("SELECT 'o' As Tick,Code,Description,ItemType FROM ItemCategory  Order By Description ", AgL.GCn)), "", 400, 420, , , False)
        FRH_Multiple(3).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(3).FFormatColumn(1, , 0, , False)
        FRH_Multiple(3).FFormatColumn(2, "Name", 200, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple(3).FFormatColumn(3, "Type", 100, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(4, "Item Group ", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, "All")
        FRH_Multiple(4) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable("SELECT 'o' As Tick,IG.Code,IG.Description,IC.Description FROM ItemGroup IG LEFT JOIN ItemCategory IC ON IC.Code =IG.ItemCategory Order By IG.Description ", AgL.GCn)), "", 600, 520, , , False)
        FRH_Multiple(4).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(4).FFormatColumn(1, , 0, , False)
        FRH_Multiple(4).FFormatColumn(2, "Item Group", 200, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple(4).FFormatColumn(3, "Item Category", 200, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(5, "Item Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, "All", False)
        FRH_Multiple(5) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable("Select 'o' As Tick,Code,Description as Name,ManualCode  FROM Item   " &
                          "Order By Description", AgL.GCn)), "", 600, 580, , , False)
        '"Where  " & Agl.PubSiteListCharIndex & "" & _

        FRH_Multiple(5).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(5).FFormatColumn(1, , 0, , False)
        FRH_Multiple(5).FFormatColumn(2, "Name", 360, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple(5).FFormatColumn(3, "Code", 100, DataGridViewContentAlignment.MiddleLeft)

        StrSQL = "Select 'D' as Code, 'Detail' as Name Union All Select 'S' as Code, 'Summary' as Name "
        FSetValue(6, "Detail / Summary ", FGDataType.DT_Selection_Single, FilterCodeType.DTNone, "Detail", False)
        FRH_Single(6) = New DMHelpGrid.FrmHelpGrid(New DataView(CMain.FGetDatTable(StrSQL, AgL.GCn)), "", 200, 180, , , False)
        FRH_Single(6).FFormatColumn(0, , 0, , False)
        FRH_Single(6).FFormatColumn(1, "Name", 100, DataGridViewContentAlignment.MiddleLeft)

        StrSQL = "Select 'WA' as Code, 'Weightage Average' as Name Union All Select 'FF' as Code, 'FIFO' as Name "
        FSetValue(7, "Method", FGDataType.DT_Selection_Single, FilterCodeType.DTNone, "FIFO", True)
        FRH_Single(7) = New DMHelpGrid.FrmHelpGrid(New DataView(CMain.FGetDatTable(StrSQL, AgL.GCn)), "", 200, 280, , , False)
        FRH_Single(7).FFormatColumn(0, , 0, , False)
        FRH_Single(7).FFormatColumn(1, "Name", 200, DataGridViewContentAlignment.MiddleLeft)
    End Sub
    Private Sub FStockValuation()
        Dim StrCondition As String
        Dim StrConditionOP As String
        Dim DTTemp As DataTable
        Dim StrSQL As String
        Dim StrValueField As String

        If Not FIsValid(0) Then Exit Sub
        If Not FIsValid(1) Then Exit Sub
        If Not FIsValid(2) Then Exit Sub
        If Not FIsValid(3) Then Exit Sub
        If Not FIsValid(4) Then Exit Sub
        If Not FIsValid(5) Then Exit Sub
        If Not FIsValid(6) Then Exit Sub
        If Not FIsValid(7) Then Exit Sub

        StrCondition = " Where ( Date(ST.V_Date) Between " & AgL.Chk_Date(CDate(FGMain(GFilter, 0).Value.ToString).ToString("s")) & " And " & AgL.Chk_Date(CDate(FGMain(GFilter, 1).Value.ToString).ToString("s")) & ") "
        StrConditionOP = " Where Date(ST.V_Date) < " & AgL.Chk_Date(CDate(FGMain(GFilter, 0).Value.ToString).ToString("s")) & " "

        If Trim(FGMain(GFilterCode, 2).Value) <> "" Then StrCondition += " And IM.ItemType In (" & FGMain(GFilterCode, 2).Value & ") "
        If Trim(FGMain(GFilterCode, 2).Value) <> "" Then StrConditionOP += " And IM.ItemType In (" & FGMain(GFilterCode, 2).Value & ") "

        If Trim(FGMain(GFilterCode, 3).Value) <> "" Then StrCondition += " And IG.CatCode In (" & FGMain(GFilterCode, 3).Value & ") "
        If Trim(FGMain(GFilterCode, 3).Value) <> "" Then StrConditionOP += " And IG.CatCode In (" & FGMain(GFilterCode, 3).Value & ") "

        If Trim(FGMain(GFilterCode, 4).Value) <> "" Then StrCondition += " And IG.Code In (" & FGMain(GFilterCode, 4).Value & ") "
        If Trim(FGMain(GFilterCode, 4).Value) <> "" Then StrConditionOP += " And IG.Code In (" & FGMain(GFilterCode, 4).Value & ") "

        If Trim(FGMain(GFilterCode, 5).Value) <> "" Then StrCondition += " And ST.Item In (" & FGMain(GFilterCode, 5).Value & ") "
        If Trim(FGMain(GFilterCode, 5).Value) <> "" Then StrConditionOP += " And ST.Item In (" & FGMain(GFilterCode, 5).Value & ") "

        If UCase(Trim(FGMain(GFilterCode, 7).Value)) = "WA" Then
            StrValueField = "ST.AverageValue"
        Else
            StrValueField = "ST.FifoValue"
        End If

        StrSQL = "Select	'OPENING' As RecId,Null As DocId,Null As V_Type,Null As V_Date,ST.Item,"
        StrSQL += "Max(IM.Description) As ItemName,Max(IM.Unit) As Unit, "
        StrSQL += "(IfNull(Sum(ST.Qty_Rec),0) - IfNull(Sum(ST.Qty_Iss),0)) As OPQty, "
        StrSQL += "(IfNull(Sum((Case When IfNull(ST.Qty_Rec,0)<> 0 Then " & StrValueField & " Else 0 End)),0) -  "
        StrSQL += "IfNull(Sum((Case	When IfNull(ST.Qty_Iss,0) <> 0 Then " & StrValueField & " Else 0 End)),0)) As OPValue, "
        StrSQL += "0 As RQty,0 As RValue,0 As IQty,0 As IValue, "
        StrSQL += "0 As SNo,0 As SerialNo "
        StrSQL += "From Stock ST "
        StrSQL += "Left Join Item IM On ST.Item=IM.Code "
        StrSQL += StrConditionOP
        StrSQL += "Group By Item "
        '=========================================================
        '================= For Transaction Stock =================
        '=========================================================
        StrSQL += "Union All "
        StrSQL += "Select ST.Div_Code || ST.Site_Code || '-' || ST.V_Type || '-' || ST.RecId As V_No,ST.DocId,ST.V_Type As V_Type,ST.V_Date,ST.Item, "
        StrSQL += "IfNull(IM.Description,'') As ItemName,IfNull(IM.Unit,'') As Unit, "
        StrSQL += "0 As OpQty,0 As OPValue, "
        StrSQL += "IfNull(ST.Qty_Rec,0) As RQty, "
        StrSQL += "(Case When IfNull(ST.Qty_Rec,0)<> 0 Then IfNull(" & StrValueField & ",0) Else 0 End) As RVal, "
        StrSQL += "IfNull(ST.Qty_Iss,0) As IQty, "
        StrSQL += "(Case When  IfNull(ST.Qty_Iss,0) <> 0 Then IfNull(" & StrValueField & ",0) Else 0 End) As IVal, "
        StrSQL += "1 As SNo,IfNull(VT.SerialNo,0) As SerialNo "
        StrSQL += "From Stock ST "
        StrSQL += "Left Join Item IM On ST.Item=IM.Code "
        StrSQL += "Left Join Voucher_Type VT On VT.V_Type=ST.V_Type "
        StrSQL += StrCondition
        StrSQL += "Order By Item,V_Date,SNo,SerialNo,RecId "
        DTTemp = CMain.FGetDatTable(StrSQL, AgL.GCn)

        If Not DTTemp.Rows.Count > 0 Then MsgBox("No Records Found to Print.") : Exit Sub

        If Trim(FGMain(GFilterCode, 6).Value) <> "S" Then
            FLoadMainReport("STOCKWITHITEMVALUEDETAIL", DTTemp)
            CMain.FormulaSet(RptMain, "Stock Valuation (Detail)", FGMain)
        Else
            FLoadMainReport("STOCKWITHITEMVALUE", DTTemp)
            CMain.FormulaSet(RptMain, "Stock Valuation (Summary)", FGMain)
        End If
        CMain.FShowReport(RptMain, Me.MdiParent, Me.Text)
    End Sub
    Private Sub FINI_GTAReg()
        Dim StrSQL As String
        FSetValue(0, "From Date", FGDataType.DT_Date, FilterCodeType.DTNone, AgL.PubStartDate)
        FSetValue(1, "To Date", FGDataType.DT_Date, FilterCodeType.DTNone, AgL.PubLoginDate)


        FSetValue(2, "Consignor Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, "All")
        FRH_Multiple(2) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable("Select  'o'  As Tick,max(ST.Consignor) As Code, S.Name,max(S.ManualCode) as ManualCode FROM STaxTrn ST " &
                          " LEFT JOIN SubGroup S ON S.SubCode=ST.Consignor   " &
                          "Where  (SG.SiteList Like '%|" & AgL.PubSiteCode & "|%' Or SG.SiteList Is Null) " &
                          "group by S.Name  Order By S.Name", AgL.GCn)), "", 600, 660, , , False)
        FRH_Multiple(2).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(2).FFormatColumn(1, , 0, , False)
        FRH_Multiple(2).FFormatColumn(2, "Name", 440, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple(2).FFormatColumn(3, "Code", 100, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(3, "Consignee Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, "All")
        FRH_Multiple(3) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable("Select   'o'  As Tick,max(ST.Consignee) As Code, S.Name,max(S.ManualCode) as ManualCode  FROM STaxTrn ST " &
                          " LEFT JOIN SubGroup S ON S.SubCode=ST.Consignee   " &
                          "Where  (SG.SiteList Like '%|" & AgL.PubSiteCode & "|%' Or SG.SiteList Is Null) " &
                          "group by S.Name  Order By S.Name", AgL.GCn)), "", 600, 660, , , False)
        FRH_Multiple(3).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(3).FFormatColumn(1, , 0, , False)
        FRH_Multiple(3).FFormatColumn(2, "Name", 440, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple(3).FFormatColumn(3, "Code", 100, DataGridViewContentAlignment.MiddleLeft)

        StrSQL = "Select 'G' as Code, 'G.T.A.' as Name Union All Select 'N' as Code, 'NON G.T.A.' as Name "
        FSetValue(4, "Report On Choice", FGDataType.DT_Selection_Single, FilterCodeType.DTNone, "G.T.A.", False)
        FRH_Single(4) = New DMHelpGrid.FrmHelpGrid(New DataView(CMain.FGetDatTable(StrSQL, AgL.GCn)), "", 200, 220, , , False)
        FRH_Single(4).FFormatColumn(0, , 0, , False)
        FRH_Single(4).FFormatColumn(1, "Name", 140, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(5, "Site Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, AgL.PubSiteName & "|'" & AgL.PubSiteCode & "'")
        FRH_Multiple(5) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable(
                          "Select 'o' As Tick,Sm.Code,Sm.Name From Sitemast Sm where code in (" & AgL.PubSiteList & ")   Order By Sm.Name",
                          AgL.GCn)), "", 300, 360, , , False, AgL.PubSiteCode)
        FRH_Multiple(5).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(5).FFormatColumn(1, , 0, , False)
        FRH_Multiple(5).FFormatColumn(2, "Name", 240, DataGridViewContentAlignment.MiddleLeft)
    End Sub
    Private Sub FGTAReg()
        Dim StrCondition As String
        Dim StrConditionOp As String
        Dim DTTemp As DataTable
        If Not FIsValid(0) Then Exit Sub
        If Not FIsValid(1) Then Exit Sub
        If Not FIsValid(2) Then Exit Sub
        If Not FIsValid(3) Then Exit Sub
        If Not FIsValid(4) Then Exit Sub
        If Not FIsValid(5) Then Exit Sub

        StrCondition = " Where ( Date(St.V_Date) Between " & AgL.Chk_Date(CDate(FGMain(GFilter, 0).Value.ToString).ToString("s")) & " And " & AgL.Chk_Date(CDate(FGMain(GFilter, 1).Value.ToString).ToString("s")) & ") "
        StrConditionOp = " Where Date(St.V_Date) <  " & AgL.Chk_Date(CDate(FGMain(GFilter, 0).Value.ToString).ToString("s")) & " "
        If Trim(FGMain(GFilterCode, 2).Value) <> "" Then StrCondition = StrCondition & " And ST.Consignor In (" & FGMain(GFilterCode, 2).Value & ")"
        If Trim(FGMain(GFilterCode, 2).Value) <> "" Then StrConditionOp = StrConditionOp & " And ST.Consignor In (" & FGMain(GFilterCode, 2).Value & ")"

        If Trim(FGMain(GFilterCode, 3).Value) <> "" Then StrCondition = StrCondition & " And ST.Consignee In (" & FGMain(GFilterCode, 3).Value & ")"
        If Trim(FGMain(GFilterCode, 3).Value) <> "" Then StrConditionOp = StrConditionOp & " And ST.Consignee In (" & FGMain(GFilterCode, 3).Value & ")"

        If Trim(FGMain(GFilterCode, 5).Value) <> "" Then
            StrCondition = StrCondition & " And  St.Site_Code IN (" & FGMain(GFilterCode, 5).Value & ") "
            StrConditionOp = StrConditionOp & " And  St.Site_Code IN (" & FGMain(GFilterCode, 5).Value & ") "
        Else
            StrCondition = StrCondition & " And  St.Site_Code IN (" & AgL.PubSiteList & ") "
            StrConditionOp = StrConditionOp & " And  St.Site_Code IN (" & AgL.PubSiteList & ") "
        End If

        If UCase(Trim(FGMain(GFilterCode, 4).Value)) <> "N" Then

            StrSQLQuery = "SELECT  " & AgL.Chk_Date(CDate(FGMain(GFilter, 0).Value.ToString).ToString("s")) & " as V_Date,'' AS Consignor,'' AS Consignee,'' as VehicleNo, "
            StrSQLQuery += "'Opening' as  Description,''  AS FrPlace,'' AS ToPlace,'' as ConsignorBill,'' as ConsigneeBill,  "
            StrSQLQuery += "max(ST.EntryType) as EntryType,'' as Remark,datename(MM," & AgL.Chk_Date(CDate(FGMain(GFilter, 1).Value.ToString).ToString("s")) & ")  As Month, "
            StrSQLQuery += "IfNull(sum(case when ST.V_Type<>'STAXR' then ST.GAmount else (0 - ST.GAmount) end),0) as Gamount, "
            StrSQLQuery += "IfNull(sum(case when ST.V_Type<>'STAXR' then ST.Exempted else (0 - ST.Exempted) end),0 ) as Exempted, "
            StrSQLQuery += "IfNull(sum(case when ST.V_Type<>'STAXR' then ST.TaxableAmt else (0 - ST.TaxableAmt) end),0) TaxableAmt, "
            StrSQLQuery += "IfNull(sum(case when ST.V_Type<>'STAXR' then ST.ServiceTaxAmt else (0 - ST.ServiceTaxAmt) end),0) ServiceTaxAmt, "
            StrSQLQuery += "IfNull(sum(case when ST.V_Type<>'STAXR' then ST.ECessAmt else (0 - ST.ECessAmt) end ),0) ECessAmt, "
            StrSQLQuery += "IfNull(sum(case when ST.V_Type<>'STAXR' then ST.SHCessAmt else (0 - ST.SHCessAmt) end),0) as SHCessAmt,max(ST.V_Type) As  V_Type, "
            StrSQLQuery += "Null As PtyBillNo,Null As PtyBillDt "
            StrSQLQuery += "FROM STaxTrn ST " + StrConditionOp
            StrSQLQuery += "and ST.EntryType='G'     "
            StrSQLQuery += "Union All "
            StrSQLQuery += "SELECT ST.V_Date,S.Name AS Consignor,S1.Name AS Consignee,ST.VehicleNo, "
            StrSQLQuery += "ST.Description,C.CityName AS FrPlace,C1.CityName AS ToPlace,ST.ConsignorBill,ST.ConsigneeBill,  "
            StrSQLQuery += "ST.EntryType,ST.Remark,datename(MM," & AgL.Chk_Date(CDate(FGMain(GFilter, 1).Value.ToString).ToString("s")) & ") As Month, "
            StrSQLQuery += "case when ST.V_Type<>'STAXR' then ST.GAmount else (0 - ST.GAmount) end as Gamount, "
            StrSQLQuery += "case when ST.V_Type<>'STAXR' then ST.Exempted else (0 - ST.Exempted) end as Exempted, "
            StrSQLQuery += "case when ST.V_Type<>'STAXR' then ST.TaxableAmt else (0 - ST.TaxableAmt) end TaxableAmt, "
            StrSQLQuery += "case when ST.V_Type<>'STAXR' then ST.ServiceTaxAmt else (0 - ST.ServiceTaxAmt) end ServiceTaxAmt, "
            StrSQLQuery += "case when ST.V_Type<>'STAXR' then ST.ECessAmt else (0 - ST.ECessAmt) end ECessAmt, "
            StrSQLQuery += "case when ST.V_Type<>'STAXR' then ST.SHCessAmt else (0 - ST.SHCessAmt) end as SHCessAmt,ST.V_Type, "
            StrSQLQuery += "ST.PtyBillNo,ST.PtyBillDt "
            StrSQLQuery += "FROM STaxTrn ST "
            StrSQLQuery += "LEFT JOIN SubGroup S ON S.SubCode=ST.Consignor "
            StrSQLQuery += "LEFT JOIN SubGroup S1 ON S1.SubCode=ST.Consignee "
            StrSQLQuery += "LEFT JOIN City C ON C.CityCode=ST.FromPlace "
            StrSQLQuery += "LEFT JOIN City C1 ON C1.CityCode=ST.ToPlace " + StrCondition
            StrSQLQuery += "and ST.EntryType='G'  "

            StrSQLQuery = AgL.GetBackendBasedQuery(StrSQLQuery)
            DTTemp = CMain.FGetDatTable(StrSQLQuery, AgL.GCn)
            If Not DTTemp.Rows.Count > 0 Then MsgBox("No Records Found to Print.") : Exit Sub
            FLoadMainReport("GTAREGISTER", DTTemp)
            CMain.FormulaSet(RptMain, "G.T.A. Register", FGMain)
            CMain.FShowReport(RptMain, Me.MdiParent, Me.Text)
        Else

            StrSQLQuery = "SELECT " & AgL.Chk_Date(CDate(FGMain(GFilter, 0).Value.ToString).ToString("s")) & " as V_Date,'' AS Consignor,'' as STNo,'Opening' as Description,'' as ConsignorBill,'' as Remark,"
            StrSQLQuery += "IfNull(sum(case when ST.V_Type<>'STXNR' then ST.ServiceTaxAmt else (0 - ST.ServiceTaxAmt) end),0) as ServiceTaxAmt, "
            StrSQLQuery += "IfNull(sum(case when ST.V_Type<>'STXNR' then ST.ECessAmt else (0 - ST.ECessAmt) end ),0) as ECessAmt, "
            StrSQLQuery += "IfNull(sum(case when ST.V_Type<>'STXNR' then ST.SHCessAmt else (0 - ST.SHCessAmt) end),0) as SHCessAmt, "
            StrSQLQuery += "IfNull(sum((Case When IfNull(ST.VrRefDocId,'')<>'' Then 0 Else (case when ST.V_Type<>'STXNR' then ST.NetAmount else (0 - ST.NetAmount) end) End)),0) as NetAmount, "
            StrSQLQuery += "max(ST.EntryType)as EntryType ,'' As V_Type, "
            StrSQLQuery += "Null As PtyBillNo,Null As PtyBillDt,Null As Chq_No,Null As Chq_Date,Null As Narration,Null As PmtDate "
            StrSQLQuery += " FROM STaxTrn ST " + StrConditionOp
            StrSQLQuery += " and ST.EntryType='N' "
            StrSQLQuery += " Union All "
            StrSQLQuery += " SELECT ST.V_Date,S.Name AS Consignor,S.STNo,ST.Description,ST.ConsignorBill,ST.Remark,"
            StrSQLQuery += " case when ST.V_Type<>'STXNR' then ST.ServiceTaxAmt else (0 - ST.ServiceTaxAmt) end as ServiceTaxAmt, "
            StrSQLQuery += " case when ST.V_Type<>'STXNR' then ST.ECessAmt else (0 - ST.ECessAmt) end as ECessAmt, "
            StrSQLQuery += " case when ST.V_Type<>'STXNR' then ST.SHCessAmt else (0 - ST.SHCessAmt) end as SHCessAmt, "
            StrSQLQuery += " (Case When IfNull(ST.VrRefDocId,'')<>'' Then 0 Else (case when ST.V_Type<>'STXNR' then ST.NetAmount else (0 - ST.NetAmount) end) End) as NetAmount, "
            StrSQLQuery += " ST.EntryType,ST.V_Type, "
            StrSQLQuery += " ST.PtyBillNo,ST.PtyBillDt,L.Chq_No,L.Chq_Date,L.Narration,L.V_Date As PmtDate  "
            StrSQLQuery += " FROM STaxTrn ST"
            StrSQLQuery += " LEFT JOIN SubGroup S ON S.SubCode=ST.Consignor"
            StrSQLQuery += " LEFT JOIN SubGroup S1 ON S1.SubCode=ST.Consignee  "
            StrSQLQuery += " LEFT JOIN Ledger L ON L.DocId=ST.VrRefDocId AND L.V_SNo=ST.VrRef_Sno " + StrCondition
            StrSQLQuery += " and ST.EntryType='N' "

            StrSQLQuery = AgL.GetBackendBasedQuery(StrSQLQuery)
            DTTemp = CMain.FGetDatTable(StrSQLQuery, AgL.GCn)
            If Not DTTemp.Rows.Count > 0 Then MsgBox("No Records Found to Print.") : Exit Sub
            FLoadMainReport("NONGTAREGISTER", DTTemp)

            CMain.FormulaSet(RptMain, "NON G.T.A. Register", FGMain)
            CMain.FShowReport(RptMain, Me.MdiParent, Me.Text)
        End If
    End Sub
    Private Sub FIni_LedgerGrMergeLedger()
        Dim StrSQL As String

        FSetValue(0, "From Date", FGDataType.DT_Date, FilterCodeType.DTNone, AgL.PubStartDate)
        FSetValue(1, "To Date", FGDataType.DT_Date, FilterCodeType.DTNone, AgL.PubLoginDate)

        FSetValue(2, "Ledger Group", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, "All", False)
        FRH_Multiple(2) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable(
                          "Select 'o' As Tick,LG.Code,LG.Name From LedgerGroup LG Order By LG.Name",
                          AgL.GCn)), "", 600, 560, , , False)
        FRH_Multiple(2).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(2).FFormatColumn(1, , 0, , False)
        FRH_Multiple(2).FFormatColumn(2, "Ledger Name", 440, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(3, "Site Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, AgL.PubSiteName & "|'" & AgL.PubSiteCode & "'", False)
        FRH_Multiple(3) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable(
                          "Select 'o' As Tick,Sm.Code,Sm.Name From Sitemast Sm where code in (" & AgL.PubSiteList & ")   Order By Sm.Name",
                          AgL.GCn)), "", 300, 360, , , False, AgL.PubSiteCode)
        FRH_Multiple(3).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(3).FFormatColumn(1, , 0, , False)
        FRH_Multiple(3).FFormatColumn(2, "Name", 240, DataGridViewContentAlignment.MiddleLeft)

        StrSQL = "Select 'Y' as Code, 'Yes' as Name Union All Select 'N' as Code, 'No' as Name"

        FSetValue(4, "Index Needed", FGDataType.DT_Selection_Single, FilterCodeType.DTNone, "No", False)
        FRH_Single(4) = New DMHelpGrid.FrmHelpGrid(New DataView(CMain.FGetDatTable(StrSQL, AgL.GCn)), "", 150, 180, , , False)
        FRH_Single(4).FFormatColumn(0, , 0, , False)
        FRH_Single(4).FFormatColumn(1, "Name", 100, DataGridViewContentAlignment.MiddleLeft)

        StrSQL = "Select 'Y' as Code, 'Yes' as Name Union All Select 'N' as Code, 'No' as Name"

        FSetValue(5, "Contra A/C Needed", FGDataType.DT_Selection_Single, FilterCodeType.DTNone, "No", False)
        FRH_Single(5) = New DMHelpGrid.FrmHelpGrid(New DataView(CMain.FGetDatTable(StrSQL, AgL.GCn)), "", 150, 180, , , False)
        FRH_Single(5).FFormatColumn(0, , 0, , False)
        FRH_Single(5).FFormatColumn(1, "Name", 100, DataGridViewContentAlignment.MiddleLeft)
    End Sub
    Private Sub FIni_AccountGrMergeLedger()
        Dim StrSQL As String

        FSetValue(0, "From Date", FGDataType.DT_Date, FilterCodeType.DTNone, AgL.PubStartDate)
        FSetValue(1, "To Date", FGDataType.DT_Date, FilterCodeType.DTNone, AgL.PubLoginDate)

        FSetValue(2, "Account Group", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, "All", True)
        FRH_Multiple(2) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable(
                          "Select 'o' As Tick,AG.GroupCode,AG.GroupName,AG1.GroupName From AcGroup AG LEFT JOIN AcGroup AG1 ON AG1.GroupCode = AG.GroupUnder Order By AG.GroupName",
                          AgL.GCn)), "", 600, 560, , , False)
        FRH_Multiple(2).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(2).FFormatColumn(1, , 0, , False)
        FRH_Multiple(2).FFormatColumn(2, "Group Name", 220, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple(2).FFormatColumn(3, "Group Under", 220, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(3, "Site Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, AgL.PubSiteName & "|'" & AgL.PubSiteCode & "'", False)
        FRH_Multiple(3) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable(
                          "Select 'o' As Tick,Sm.Code,Sm.Name From Sitemast Sm where code in (" & AgL.PubSiteList & ")   Order By Sm.Name",
                          AgL.GCn)), "", 300, 360, , , False, AgL.PubSiteCode)
        FRH_Multiple(3).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(3).FFormatColumn(1, , 0, , False)
        FRH_Multiple(3).FFormatColumn(2, "Name", 240, DataGridViewContentAlignment.MiddleLeft)

        StrSQL = "Select 'Y' as Code, 'Yes' as Name Union All Select 'N' as Code, 'No' as Name"

        FSetValue(4, "Contra A/C Needed", FGDataType.DT_Selection_Single, FilterCodeType.DTNone, "No", False)
        FRH_Single(4) = New DMHelpGrid.FrmHelpGrid(New DataView(CMain.FGetDatTable(StrSQL, AgL.GCn)), "", 150, 180, , , False)
        FRH_Single(4).FFormatColumn(0, , 0, , False)
        FRH_Single(4).FFormatColumn(1, "Name", 100, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(5, "Voucher Type", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, "All", False)
        FRH_Multiple(5) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable(
                          "Select 'o' As Tick, VT.V_Type AS Code,VT.V_Type ,VT.Description   FROM Voucher_Type VT WHERE VT.V_Type IN (SELECT V_Type FROM  Ledger Where  Site_code in (" & AgL.PubSiteList & "))   Order By VT.Description ",
                          AgL.GCn)), "", 300, 460, , , False, AgL.PubSiteCode)
        FRH_Multiple(5).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(5).FFormatColumn(1, , 0, , False)
        FRH_Multiple(5).FFormatColumn(2, "Type", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple(5).FFormatColumn(3, "Name", 240, DataGridViewContentAlignment.MiddleLeft)
    End Sub
    Private Sub FLedgerGrMergeLedger()
        Dim StrCondition1 As String, StrConditionOP As String, StrConditionsite As String
        Dim DTTemp As DataTable
        Dim I As Integer

        If Not FIsValid(0) Then Exit Sub
        If Not FIsValid(1) Then Exit Sub

        StrCondition1 = " Where ( Date(LG.V_Date) Between " & AgL.Chk_Date(CDate(FGMain(GFilter, 0).Value.ToString)).ToString("s") & " And " & AgL.Chk_Date(CDate(FGMain(GFilter, 1).Value.ToString).ToString("s")) & ") "
        StrConditionOP = " Where Date(LG.V_Date) < " & AgL.Chk_Date(CDate(FGMain(GFilter, 0).Value.ToString).ToString("s")) & " "

        StrConditionsite = ""
        If Trim(FGMain(GFilterCode, 2).Value) <> "" Then StrCondition1 = StrCondition1 & " And LGG.Code In (" & FGMain(GFilterCode, 2).Value & ")"
        If Trim(FGMain(GFilterCode, 2).Value) <> "" Then StrConditionOP = StrConditionOP & " And LGG.Code In (" & FGMain(GFilterCode, 2).Value & ")"

        If Trim(FGMain(GFilterCode, 3).Value) <> "" Then
            StrConditionsite += " And LG.Site_Code In (" & FGMain(GFilterCode, 3).Value & ") "
        Else
            StrConditionsite += " And LG.Site_Code In  (" & AgL.PubSiteList & ") "
        End If

        '========== For Detail Section =======
        StrSQLQuery = "Select LG.V_Type,LG.DivCode || LG.Site_Code || '-' || LG.V_Type || '-' || LG.RecId As V_No,LG.V_Date,LG.V_Prefix,SG.Name As PName,LG.SubCode,LG.Narration, "
        StrSQLQuery = StrSQLQuery + "LG.AmtDr,LG.AmtCr,1 As SNo,SM.Name As Division,LG.ContraText As ContraName,LG.Chq_No,LG.Chq_Date,"
        StrSQLQuery = StrSQLQuery + "IfNull(LG.Site_Code,'') As Site_Code,IfNull(LGG.Name,'') As LedgerGr,LGG.Code As Code "
        StrSQLQuery = StrSQLQuery + "From LedgerGroup LGG Left Join SubGroup SG On LGG.Code = SG.LedgerGroup "
        StrSQLQuery = StrSQLQuery + "Left Join Ledger LG ON LG.SubCode = SG.SubCode "
        StrSQLQuery = StrSQLQuery + "Left Join Sitemast SM On LG.Site_Code = SM.Code "

        StrSQLQuery = StrSQLQuery + StrCondition1 + StrConditionsite + "AND IfNull(LGG.Code,'')<>'' "
        StrSQLQuery = StrSQLQuery + "Union All "

        '======= For Opening Balance =========
        StrSQLQuery = StrSQLQuery + "Select	Null As V_Type,Null As V_No,Null As V_Date,Null As V_Prefix, "
        StrSQLQuery = StrSQLQuery + "Max(SG.Name) As PName,Max(LG.SubCode) As SubCode,'OPENING BALANCE' As Narration, "
        StrSQLQuery = StrSQLQuery + "(Case When (IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LG.AmtCr),0))>0 Then  "
        StrSQLQuery = StrSQLQuery + "(IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LG.AmtCr),0)) Else 0 End) As AmtDr, "
        StrSQLQuery = StrSQLQuery + "(Case When (IfNull(Sum(LG.AmtCr),0)-IfNull(Sum(LG.AmtDr),0))>0 Then "
        StrSQLQuery = StrSQLQuery + "(IfNull(Sum(LG.AmtCr),0)-IfNull(Sum(LG.AmtDr),0)) Else 0 End) As AmtCr,"
        StrSQLQuery = StrSQLQuery + "0 As SNo,max(SM.name) As Division,Null As ContraName,Null As Chq_No,Null As Chq_Date,"
        StrSQLQuery = StrSQLQuery + "Null As Site_Code,Max(IfNull(LGG.Name,'')) As LedgerGr,Max(LGG.Code) As Code "
        StrSQLQuery = StrSQLQuery + "From LedgerGroup LGG Left Join SubGroup SG On LGG.Code = SG.LedgerGroup "
        StrSQLQuery = StrSQLQuery + "Left Join Ledger LG ON LG.SubCode = SG.SubCode "
        StrSQLQuery = StrSQLQuery + "Left Join Sitemast SM On LG.Site_Code = SM.Code "

        StrSQLQuery = StrSQLQuery + StrConditionOP + StrConditionsite + "AND IfNull(LGG.Code,'')<>'' "

        StrSQLQuery = StrSQLQuery + "Group By LGG.Code "
        StrSQLQuery = StrSQLQuery + "Order By LedgerGr,V_Date,V_Type,V_No,SNo "

        StrSQLQuery = AgL.GetBackendBasedQuery(StrSQLQuery)
        DTTemp = CMain.FGetDatTable(StrSQLQuery, AgL.GCn)

        If Not DTTemp.Rows.Count > 0 Then MsgBox("No Records Found to Print.") : Exit Sub

        FLoadMainReport("MergeLedger", DTTemp)
        For I = 0 To RptMain.DataDefinition.FormulaFields.Count - 1
            Select Case (UCase(RptMain.DataDefinition.FormulaFields.Item(I).Name))
                Case UCase("FrmIndexNeeded")
                    RptMain.DataDefinition.FormulaFields.Item(I).Text = "'" & IIf(Trim(FGMain(GFilterCode, 4).Value) = "", "N", Trim(FGMain(GFilterCode, 4).Value)) & "'"
                Case UCase("Contraneeded")
                    RptMain.DataDefinition.FormulaFields.Item(I).Text = "'" & Trim(FGMain(GFilterCode, 5).Value) & "'"
            End Select
        Next

        CMain.FormulaSet(RptMain, Me.Text, FGMain)
        CMain.FShowReport(RptMain, Me.MdiParent, Me.Text)
    End Sub
    Private Sub FAccountGrMergeLedger()
        Dim StrCondition1 As String, StrConditionOP As String, StrConditionsite As String
        Dim DTTemp As DataTable
        Dim I As Integer

        If Not FIsValid(0) Then Exit Sub
        If Not FIsValid(1) Then Exit Sub

        StrCondition1 = " Where ( Date(LG.V_Date) Between " & AgL.Chk_Date(CDate(FGMain(GFilter, 0).Value.ToString).ToString("s")) & " And " & AgL.Chk_Date(CDate(FGMain(GFilter, 1).Value.ToString).ToString("s")) & ") "
        StrConditionOP = " Where Date(LG.V_Date) < " & AgL.Chk_Date(CDate(FGMain(GFilter, 0).Value.ToString).ToString("s")) & " "

        StrConditionsite = ""
        If Trim(FGMain(GFilterCode, 2).Value) <> "" Then StrCondition1 = StrCondition1 & " And (IfNull(SG.GroupCode,'') In (Select IfNull(AGP.GroupCode,'') From AcGroupPath AGP Where AGP.GroupUnder In (" & FGMain(GFilterCode, 2).Value & ")) Or SG.GroupCode In (" & FGMain(GFilterCode, 2).Value & ")) "
        If Trim(FGMain(GFilterCode, 2).Value) <> "" Then StrConditionOP = StrConditionOP & " And (IfNull(SG.GroupCode,'') In (Select IfNull(AGP.GroupCode,'') From AcGroupPath AGP Where AGP.GroupUnder In (" & FGMain(GFilterCode, 2).Value & ")) Or SG.GroupCode In (" & FGMain(GFilterCode, 2).Value & ")) "
        If Trim(FGMain(GFilterCode, 5).Value) <> "" Then StrCondition1 = StrCondition1 & " And LG.V_Type In (" & FGMain(GFilterCode, 5).Value & ")"

        If Trim(FGMain(GFilterCode, 3).Value) <> "" Then
            StrConditionsite += " and LG.site_Code In (" & FGMain(GFilterCode, 3).Value & ") "
        Else
            StrConditionsite += " and LG.site_Code In  (" & AgL.PubSiteList & ") "
        End If


        '========== For Detail Section =======
        StrSQLQuery = "Select LG.V_Type,LG.DivCode || LG.Site_Code || '-' || LG.V_Type || '-' || LG.RecId As V_No,LG.V_Date,LG.V_Prefix,SG.Name  As PName,LG.SubCode,LG.Narration, "
        StrSQLQuery = StrSQLQuery + "LG.AmtDr,LG.AmtCr,1 As SNo,SM.Name As Division,LG.ContraText As ContraName,LG.Chq_No,LG.Chq_Date,"
        StrSQLQuery = StrSQLQuery + "IfNull(LG.Site_Code,'') As Site_Code,AG.GroupName As AccGrName,AG.GroupCode AS GroupCode "
        StrSQLQuery = StrSQLQuery + "From Ledger LG Left Join SubGroup SG On LG.Subcode = SG.SubCode "
        StrSQLQuery = StrSQLQuery + "Left Join AcGroup AG ON AG.GroupCode = SG.GroupCode "
        StrSQLQuery = StrSQLQuery + "Left Join Sitemast SM On LG.Site_Code = SM.Code "

        StrSQLQuery = StrSQLQuery + StrCondition1 + StrConditionsite
        StrSQLQuery = StrSQLQuery + "Union All "

        '======= For Opening Balance =========
        StrSQLQuery = StrSQLQuery + "Select	Null As V_Type,Null As V_No,Null As V_Date,Null As V_Prefix, "
        StrSQLQuery = StrSQLQuery + "Max(SG.Name)As PName,Max(LG.SubCode) As SubCode,'OPENING BALANCE' As Narration, "
        StrSQLQuery = StrSQLQuery + "(Case When (IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LG.AmtCr),0))>0 Then  "
        StrSQLQuery = StrSQLQuery + "(IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LG.AmtCr),0)) Else 0 End) As AmtDr, "
        StrSQLQuery = StrSQLQuery + "(Case When (IfNull(Sum(LG.AmtCr),0)-IfNull(Sum(LG.AmtDr),0))>0 Then "
        StrSQLQuery = StrSQLQuery + "(IfNull(Sum(LG.AmtCr),0)-IfNull(Sum(LG.AmtDr),0)) Else 0 End) As AmtCr,"
        StrSQLQuery = StrSQLQuery + "0 As SNo,Max(SM.name) As Division,Null As ContraName,Null As Chq_No,Null As Chq_Date,"
        StrSQLQuery = StrSQLQuery + "Null As Site_Code,Max(AG.GroupName) As AccGrName,Max(AG.GroupCode) AS GroupCode  "
        StrSQLQuery = StrSQLQuery + "From Ledger LG Left Join SubGroup SG On LG.Subcode = SG.SubCode "
        StrSQLQuery = StrSQLQuery + "Left Join AcGroup AG ON AG.GroupCode = SG.GroupCode "
        StrSQLQuery = StrSQLQuery + "Left Join Sitemast SM On LG.Site_Code = SM.Code "

        StrSQLQuery = StrSQLQuery + StrConditionOP + StrConditionsite

        StrSQLQuery = StrSQLQuery + "Group By AG.GroupCode "
        StrSQLQuery = StrSQLQuery + "Order By AccGrName,V_Date,V_Type,V_No,SNo "

        StrSQLQuery = AgL.GetBackendBasedQuery(StrSQLQuery)
        DTTemp = CMain.FGetDatTable(StrSQLQuery, AgL.GCn)

        If Not DTTemp.Rows.Count > 0 Then MsgBox("No Records Found to Print.") : Exit Sub

        FLoadMainReport("AccountGrMergeLedger", DTTemp)
        For I = 0 To RptMain.DataDefinition.FormulaFields.Count - 1
            Select Case (UCase(RptMain.DataDefinition.FormulaFields.Item(I).Name))
                Case UCase("Contraneeded")
                    RptMain.DataDefinition.FormulaFields.Item(I).Text = "'" & Trim(FGMain(GFilterCode, 4).Value) & "'"
            End Select
        Next

        CMain.FormulaSet(RptMain, Me.Text, FGMain)
        CMain.FShowReport(RptMain, Me.MdiParent, Me.Text)
    End Sub
    Private Sub FIni_DailyCollection()
        FSetValue(0, "From Date", FGDataType.DT_Date, FilterCodeType.DTNone, AgL.PubStartDate)
        FSetValue(1, "To Date", FGDataType.DT_Date, FilterCodeType.DTNone, AgL.PubLoginDate)

        FSetValue(2, "A/C Group", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, "All")
        FRH_Multiple(2) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable(
                         "Select Distinct  'o'  As Tick,S.GroupCode As Code,AG.GroupName AS Name From SubGroup S LEFT JOIN AcGroup AG ON AG.GroupCode=S.GroupCode Order By Name",
                         AgL.GCn)), "", 400, 430, , , False)
        FRH_Multiple(2).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(2).FFormatColumn(1, , 0, , False)
        FRH_Multiple(2).FFormatColumn(2, "Name", 300, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(3, "Site Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, AgL.PubSiteName & "|'" & AgL.PubSiteCode & "'", False)
        FRH_Multiple(3) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable(
                          "Select 'o' As Tick,Sm.Code,Sm.Name From Sitemast Sm where code in (" & AgL.PubSiteList & ")   Order By Sm.Name",
                          AgL.GCn)), "", 300, 360, , , False, AgL.PubSiteCode)
        FRH_Multiple(3).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(3).FFormatColumn(1, , 0, , False)
        FRH_Multiple(3).FFormatColumn(2, "Name", 240, DataGridViewContentAlignment.MiddleLeft)
    End Sub
    Private Sub FDailyCollectionReg()
        Dim StrCondition1 As String, StrConditionsite As String
        Dim DTTemp As DataTable

        If Not FIsValid(0) Then Exit Sub
        If Not FIsValid(1) Then Exit Sub

        StrCondition1 = " Where ( Date(LG.V_Date) Between " & AgL.Chk_Date(CDate(FGMain(GFilter, 0).Value.ToString).ToString("s")) & " And " & AgL.Chk_Date(CDate(FGMain(GFilter, 1).Value.ToString).ToString("s")) & ") "
        StrConditionsite = ""

        If Trim(FGMain(GFilterCode, 2).Value) <> "" Then StrCondition1 = StrCondition1 & " And (IfNull(SG.GroupCode,'') In (Select IfNull(AGP.GroupCode,'') From AcGroupPath AGP Where AGP.GroupUnder In (" & FGMain(GFilterCode, 2).Value & ")) Or SG.GroupCode In (" & FGMain(GFilterCode, 2).Value & ")) "

        If Trim(FGMain(GFilterCode, 3).Value) <> "" Then
            StrConditionsite += " and LG.site_Code In (" & FGMain(GFilterCode, 3).Value & ") "
        Else
            StrConditionsite += " and LG.site_Code In  (" & AgL.PubSiteList & ") "
        End If

        StrSQLQuery = "Select	LG.V_Type,LG.DivCode || LG.Site_Code || '-' || LG.V_Type || '-' || LG.RecId As V_No,LG.V_Date,LG.V_Prefix,SG.Name  As PName,LG.SubCode,LG.Narration, "
        StrSQLQuery = StrSQLQuery + "LG.AmtCr,1 As SNo,LG.Chq_No,LG.Chq_Date,"
        StrSQLQuery = StrSQLQuery + "IfNull(C.CityName,'') as PCity,IfNull(LG.Site_Code,'') As Site_Code,AG.GroupName "
        StrSQLQuery = StrSQLQuery + "From Ledger LG Left Join SubGroup SG On LG.Subcode=SG.SubCode "
        StrSQLQuery = StrSQLQuery + "LEFT JOIN AcGroup AG ON AG.GroupCode=SG.GroupCode "
        StrSQLQuery = StrSQLQuery + "Left Join Sitemast SM On LG.Site_Code=SM.Code "
        StrSQLQuery = StrSQLQuery + "Left Join City C On C.CityCode=SG.CityCode "

        StrSQLQuery = StrSQLQuery + StrCondition1 + StrConditionsite + " And LG.V_Type IN ('CR','BR') AND LG.AmtCr>0 "
        StrSQLQuery = StrSQLQuery + "Order By V_Date,V_No,PName,SNo "

        StrSQLQuery = AgL.GetBackendBasedQuery(StrSQLQuery)
        DTTemp = CMain.FGetDatTable(StrSQLQuery, AgL.GCn)

        If Not DTTemp.Rows.Count > 0 Then MsgBox("No Records Found to Print.") : Exit Sub

        FLoadMainReport("DailyCollection", DTTemp)
        CMain.FormulaSet(RptMain, Me.Text, FGMain)
        CMain.FShowReport(RptMain, Me.MdiParent, Me.Text)
    End Sub
    Private Sub FIni_DailyExpenseReg()
        FSetValue(0, "From Date", FGDataType.DT_Date, FilterCodeType.DTNone, AgL.PubStartDate)
        FSetValue(1, "To Date", FGDataType.DT_Date, FilterCodeType.DTNone, AgL.PubLoginDate)

        FSetValue(2, "A/C Group", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, "All")
        FRH_Multiple(2) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable(
                         "Select Distinct  'o'  As Tick,S.GroupCode As Code,AG.GroupName AS Name From SubGroup S LEFT JOIN AcGroup AG ON AG.GroupCode=S.GroupCode Order By Name",
                         AgL.GCn)), "", 400, 430, , , False)
        FRH_Multiple(2).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(2).FFormatColumn(1, , 0, , False)
        FRH_Multiple(2).FFormatColumn(2, "Name", 300, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(3, "Site Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, AgL.PubSiteName & "|'" & AgL.PubSiteCode & "'", False)
        FRH_Multiple(3) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable(
                          "Select 'o' As Tick,Sm.Code,Sm.Name From Sitemast Sm where code in (" & AgL.PubSiteList & ")   Order By Sm.Name",
                          AgL.GCn)), "", 300, 360, , , False, AgL.PubSiteCode)
        FRH_Multiple(3).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(3).FFormatColumn(1, , 0, , False)
        FRH_Multiple(3).FFormatColumn(2, "Name", 240, DataGridViewContentAlignment.MiddleLeft)
    End Sub
    Private Sub FDailyExpenseReg()
        Dim StrCondition1 As String, StrConditionsite As String
        Dim DTTemp As DataTable

        If Not FIsValid(0) Then Exit Sub
        If Not FIsValid(1) Then Exit Sub

        StrCondition1 = " Where ( Date(LG.V_Date) Between " & AgL.Chk_Date(CDate(FGMain(GFilter, 0).Value.ToString).ToString("s")) & " And " & AgL.Chk_Date(CDate(FGMain(GFilter, 1).Value.ToString).ToString("s")) & ") "
        StrConditionsite = ""

        If Trim(FGMain(GFilterCode, 2).Value) <> "" Then StrCondition1 = StrCondition1 & " And (IfNull(SG.GroupCode,'') In (Select IfNull(AGP.GroupCode,'') From AcGroupPath AGP Where AGP.GroupUnder In (" & FGMain(GFilterCode, 2).Value & ")) Or SG.GroupCode In (" & FGMain(GFilterCode, 2).Value & ")) "
        If Trim(FGMain(GFilterCode, 3).Value) <> "" Then
            StrConditionsite += " and LG.site_Code In (" & FGMain(GFilterCode, 3).Value & ") "
        Else
            StrConditionsite += " and LG.site_Code In  (" & AgL.PubSiteList & ") "
        End If

        StrSQLQuery = "Select	LG.V_Type,LG.DivCode || LG.Site_Code || '-' || LG.V_Type || '-' || LG.RecId As V_No,LG.V_Date,LG.V_Prefix,SG.Name  As PName,LG.SubCode,LG.Narration, "
        StrSQLQuery = StrSQLQuery + "LG.AmtDr,1 As SNo,LG.Chq_No,LG.Chq_Date,"
        StrSQLQuery = StrSQLQuery + "IfNull(C.CityName,'') as PCity,IfNull(LG.Site_Code,'') As Site_Code,AG.GroupName "
        StrSQLQuery = StrSQLQuery + "From Ledger LG Left Join SubGroup SG On LG.Subcode=SG.SubCode "
        StrSQLQuery = StrSQLQuery + "LEFT JOIN AcGroup AG ON AG.GroupCode=SG.GroupCode "
        StrSQLQuery = StrSQLQuery + "Left Join Sitemast SM On LG.Site_Code=SM.Code "
        StrSQLQuery = StrSQLQuery + "Left Join City C On C.CityCode=SG.CityCode "

        StrSQLQuery = StrSQLQuery + StrCondition1 + StrConditionsite + " And LG.V_Type IN ('CP','BP') AND LG.AmtDr>0 "
        StrSQLQuery = StrSQLQuery + "Order By V_Date,V_No,PName,SNo "

        StrSQLQuery = AgL.GetBackendBasedQuery(StrSQLQuery)
        DTTemp = CMain.FGetDatTable(StrSQLQuery, AgL.GCn)

        If Not DTTemp.Rows.Count > 0 Then MsgBox("No Records Found to Print.") : Exit Sub

        FLoadMainReport("DailyExpenseReg", DTTemp)
        CMain.FormulaSet(RptMain, Me.Text, FGMain)
        CMain.FShowReport(RptMain, Me.MdiParent, Me.Text)
    End Sub
    Private Sub FINI_FIFOWsOS_DR()
        FSetValue(0, "As On Date", FGDataType.DT_Date, FilterCodeType.DTNone, AgL.PubLoginDate)
        FSetValue(1, "Account Group", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, "All")
        FRH_Multiple(1) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable("Select 'o' As Tick,AG.GroupCode,AG.GroupName From AcGroup  AG " &
                          "Order By AG.GroupName", AgL.GCn)), "", 600, 520, , , False)
        FRH_Multiple(1).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(1).FFormatColumn(1, , 0, , False)
        FRH_Multiple(1).FFormatColumn(2, "Name", 400, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(2, "Account Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, "All", False)
        FRH_Multiple(2) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable(
                          "Select 'o' As Tick,SG.SubCode,SG.Name,Sg.ManualCode,IfNull(CT.CityName,''),AG.GroupName, " &
                          "IfNull(SG.Area,'') From SubGroup SG Left Join " &
                          "AcGroup AG On AG.GroupCode=SG.GroupCode Left Join " &
                          "City CT On SG.CityCode=CT.CityCode Left Join " &
                          "Area ZM On ZM.Code=SG.Area Where  (SG.SiteList Like '%|" & AgL.PubSiteCode & "|%' Or SG.SiteList Is Null)  " &
                          "Order By SG.Name",
                          AgL.GCn)), "", 600, 920, , , False)
        FRH_Multiple(2).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(2).FFormatColumn(1, , 0, , False)
        FRH_Multiple(2).FFormatColumn(2, "Name", 300, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple(2).FFormatColumn(3, "Code", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple(2).FFormatColumn(4, "City", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple(2).FFormatColumn(5, "Group Name", 200, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple(2).FFormatColumn(6, "Area", 100, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(3, "Interval", FGDataType.DT_Numeric, FilterCodeType.DTNumeric, 180, False)

        FSetValue(4, "Site", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, AgL.PubSiteName & "|'" & AgL.PubSiteCode & "'")
        FRH_Multiple(4) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable(
                          "Select 'o' As Tick,H.Code,H.Name From Sitemast H where code in (" & AgL.PubSiteList & ")   Order By H.Name",
                          AgL.GCn)), "", 300, 360, , , False, AgL.PubSiteCode)
        FRH_Multiple(4).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(4).FFormatColumn(1, , 0, , False)
        FRH_Multiple(4).FFormatColumn(2, "Name", 240, DataGridViewContentAlignment.MiddleLeft)


        FSetValue(5, "Division", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, AgL.PubDivName & "|'" & AgL.PubDivCode & "'")
        FRH_Multiple(5) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable(
                          "Select 'o' As Tick,H.Div_Code Code,H.Div_Name Name From Division H where Div_code in (" & AgL.PubDivisionList & ")   Order By H.Div_Name",
                          AgL.GCn)), "", 300, 360, , , False, AgL.PubSiteCode)
        FRH_Multiple(5).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(5).FFormatColumn(1, , 0, , False)
        FRH_Multiple(5).FFormatColumn(2, "Name", 240, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(6, "City", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, "All")
        FRH_Multiple(6) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable(
                          "Select 'o' As Tick,H.CityCode Code,H.CityName Name From City H  Order By H.CityName",
                          AgL.GCn)), "", 300, 360, , , False, AgL.PubSiteCode)
        FRH_Multiple(6).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(6).FFormatColumn(1, , 0, , False)
        FRH_Multiple(6).FFormatColumn(2, "Name", 240, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(7, "Area", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, "All")
        FRH_Multiple(7) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable(
                          "Select 'o' As Tick,H.Code,H.Description Name From Area H Order By H.Description",
                          AgL.GCn)), "", 300, 360, , , False, AgL.PubSiteCode)
        FRH_Multiple(7).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(7).FFormatColumn(1, , 0, , False)
        FRH_Multiple(7).FFormatColumn(2, "Name", 240, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(8, "Agent", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, "All")
        FRH_Multiple(8) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable(
                          "Select 'o' As Tick,H.Code,H.Name From viewHelpSubgroup H Where H.SubgroupType ='" & AgLibrary.ClsMain.agConstants.SubgroupType.SalesAgent & "' Order By H.Name",
                          AgL.GCn)), "", 300, 360, , , False, AgL.PubSiteCode)
        FRH_Multiple(8).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(8).FFormatColumn(1, , 0, , False)
        FRH_Multiple(8).FFormatColumn(2, "Name", 240, DataGridViewContentAlignment.MiddleLeft)

    End Sub
    Private Sub FINI_FIFOWsOS_Cr()
        FSetValue(0, "As On Date", FGDataType.DT_Date, FilterCodeType.DTNone, AgL.PubLoginDate)
        FSetValue(1, "Account Group", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, "All")
        FRH_Multiple(1) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable("Select 'o' As Tick,AG.GroupCode,AG.GroupName From AcGroup  AG " &
                          "Order By AG.GroupName", AgL.GCn)), "", 600, 520, , , False)
        FRH_Multiple(1).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(1).FFormatColumn(1, , 0, , False)
        FRH_Multiple(1).FFormatColumn(2, "Name", 400, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(2, "Account Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, "All", False)
        FRH_Multiple(2) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable(
                          "Select 'o' As Tick,SG.SubCode,SG.Name,Sg.ManualCode,IfNull(CT.CityName,''),AG.GroupName, " &
                          "IfNull(SG.Area,'') From SubGroup SG Left Join " &
                          "AcGroup AG On AG.GroupCode=SG.GroupCode Left Join " &
                          "City CT On SG.CityCode=CT.CityCode Left Join " &
                          "Area ZM On ZM.Code=SG.Area Where  (SG.SiteList Like '%|" & AgL.PubSiteCode & "|%' Or SG.SiteList Is Null)  " &
                          "Order By SG.Name",
                          AgL.GCn)), "", 600, 920, , , False)
        FRH_Multiple(2).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(2).FFormatColumn(1, , 0, , False)
        FRH_Multiple(2).FFormatColumn(2, "Name", 300, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple(2).FFormatColumn(3, "Code", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple(2).FFormatColumn(4, "City", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple(2).FFormatColumn(5, "Group Name", 200, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple(2).FFormatColumn(6, "Area", 100, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(3, "Interval", FGDataType.DT_Numeric, FilterCodeType.DTNumeric, 180, False)

        FSetValue(4, "Site Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, AgL.PubSiteName & "|'" & AgL.PubSiteCode & "'")
        FRH_Multiple(4) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable(
                          "Select 'o' As Tick,Sm.Code,Sm.Name From Sitemast Sm where code in (" & AgL.PubSiteList & ")   Order By Sm.Name",
                          AgL.GCn)), "", 300, 360, , , False, AgL.PubSiteCode)
        FRH_Multiple(4).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(4).FFormatColumn(1, , 0, , False)
        FRH_Multiple(4).FFormatColumn(2, "Name", 240, DataGridViewContentAlignment.MiddleLeft)
    End Sub
    Private Sub FFIFOWsOS_Dr()
        Dim StrCondition1 As String, StrCondDt As String
        Dim DTTemp As DataTable
        Dim StrSql As String, STRDATE As String
        Dim D1 As Integer
        Dim mQry As String = ""

        If Not FIsValid(0) Then Exit Sub

        STRDATE = AgL.Chk_Date(CDate(FGMain(GFilter, 0).Value.ToString).ToString("s"))

        StrCondition1 = " Where LG.V_Date <= " & AgL.Chk_Date(CDate(FGMain(GFilter, 0).Value.ToString).ToString("s")) & "  "
        StrCondDt = " Where LG.V_Date <= " & AgL.Chk_Date(CDate(FGMain(GFilter, 0).Value.ToString).ToString("s")) & "  "

        If Trim(FGMain(GFilterCode, 1).Value) <> "" Then StrCondition1 = StrCondition1 & " And (IfNull(SG.GroupCode,'') In (Select IfNull(AGP.GroupCode,'') From AcGroupPath AGP Where AGP.GroupUnder In (" & FGMain(GFilterCode, 1).Value & ")) Or SG.GroupCode In (" & FGMain(GFilterCode, 1).Value & ")) "
        If Trim(FGMain(GFilterCode, 2).Value) <> "" Then StrCondition1 = StrCondition1 & " And LG.SubCode In (" & FGMain(GFilterCode, 2).Value & ")"

        D1 = Val((FGMain(GFilter, 3).Value.ToString))

        If Trim(FGMain(GFilterCode, 4).Value) <> "" Then
            StrCondition1 = StrCondition1 & " And  LG.Site_Code IN (" & FGMain(GFilterCode, 4).Value & ") "
            StrCondDt = StrCondDt & " And  LG.Site_Code IN (" & FGMain(GFilterCode, 4).Value & ") "
        Else
            StrCondition1 = StrCondition1 & " And  LG.Site_Code IN (" & AgL.PubSiteList & ") "
            StrCondDt = StrCondDt & " And  LG.Site_Code IN (" & AgL.PubSiteList & ") "
        End If

        If Trim(FGMain(GFilterCode, 5).Value) <> "" Then
            StrCondition1 = StrCondition1 & " And  LG.DivCode IN (" & FGMain(GFilterCode, 5).Value & ") "
            StrCondDt = StrCondDt & " And  LG.DivCode IN (" & FGMain(GFilterCode, 5).Value & ") "
        Else
            StrCondition1 = StrCondition1 & " And  LG.DivCode IN (" & AgL.PubSiteList & ") "
            StrCondDt = StrCondDt & " And  LG.DivCode IN (" & AgL.PubSiteList & ") "
        End If

        If Trim(FGMain(GFilterCode, 6).Value) <> "" Then StrCondition1 = StrCondition1 & " And CT.CityCode In (" & FGMain(GFilterCode, 6).Value & ")"
        If Trim(FGMain(GFilterCode, 6).Value) <> "" Then StrCondDt = StrCondDt & " And CT.CityCode In (" & FGMain(GFilterCode, 6).Value & ")"
        If Trim(FGMain(GFilterCode, 7).Value) <> "" Then StrCondition1 = StrCondition1 & " And Sg.Area In (" & FGMain(GFilterCode, 7).Value & ")"
        If Trim(FGMain(GFilterCode, 7).Value) <> "" Then StrCondDt = StrCondDt & " And Sg.Area In (" & FGMain(GFilterCode, 7).Value & ")"
        If Trim(FGMain(GFilterCode, 8).Value) <> "" Then StrCondition1 = StrCondition1 & " And LTV.Agent In (" & FGMain(GFilterCode, 8).Value & ")"
        If Trim(FGMain(GFilterCode, 8).Value) <> "" Then StrCondDt = StrCondDt & " And LTV.Agent In (" & FGMain(GFilterCode, 8).Value & ")"


        Try
            mQry = "Drop Table #TempRecord"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        Catch ex As Exception
        End Try

        mQry = " CREATE Temporary TABLE #TempRecord (DocId  nvarchar(21),RecId  nvarchar(50),V_Date  nvarchar(30),subcode nvarchar(30),"
        mQry += " PartyName nvarchar(500),BillAmt FLOAT,PendingAmt FLOAT,Status  nvarchar(20),Site_Code  nvarchar(20),
        PartyCity  nvarchar(200),Narration  varchar(2000),V_type  nvarchar(20) );	"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

        Dim SubCode As String = "", Party As String = "", PCity As String = "", SiteCode As String = ""
        Dim Cr As Double = 0, Adv As Double = 0

        Dim CurrTempPayment As DataTable = Nothing

        mQry = " SELECT LG.SubCode,max(Sg.name) as PartyName,max(CT.CityName) as PCity,IfNull(sum(AmtCr),0) AS AmtCr,
                Case When IfNull(sum(AmtCr),0)> IfNull(sum(AmtDr),0) Then (IfNull(sum(AmtCr),0) - IfNull(sum(AmtDr),0)) Else  0   End As Advance ,
                Max(LG.Site_Code) As SiteCode 
                FROM Ledger LG 
                LEFT JOIN SubGroup SG On SG.SubCode =LG.SubCode  
                Left Join (Select SILTV.Subcode, Max(SILTV.Agent) as Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode) as LTV On Sg.SubCode = LTV.Subcode
                LEFT JOIN City CT On SG.CityCode  =CT.CityCode " + StrCondition1 + " And SG.Nature ='Customer'
                GROUP BY LG.SubCode "
        CurrTempPayment = AgL.FillData(mQry, AgL.GCn).Tables(0)

        For I As Integer = 0 To CurrTempPayment.Rows.Count - 1
            SubCode = AgL.XNull(CurrTempPayment.Rows(I)("SubCode"))
            Party = AgL.XNull(CurrTempPayment.Rows(I)("PartyName"))
            PCity = AgL.XNull(CurrTempPayment.Rows(I)("PCity"))
            Cr = AgL.XNull(CurrTempPayment.Rows(I)("AmtCr"))
            Adv = AgL.XNull(CurrTempPayment.Rows(I)("Advance"))
            SiteCode = AgL.XNull(CurrTempPayment.Rows(I)("SiteCode"))

            Dim CrAmt As Double = 0, tempval As Double = 0, DrAmt As Double = 0
            Dim DocId As String = "", RecId As String = "", Supplier As String = "", PartyName As String = "", Site As String = "", City As String = "", Narr As String = "", VType As String = ""
            Dim V_Date As String = ""

            tempval = 0

            Dim curr_TempAdjust As DataTable = Nothing

            mQry = " SELECT  IfNull(LG.DocId,'') AS DocId, LG.V_Type,LG.DivCode || LG.Site_Code || '-' || LG.V_Type || '-' || LG.RecId As RecId,LG.V_date AS V_date,IfNull(LG.SubCode,'') AS Subcode,
            IfNull(SG.Name,'') AS PartyName, IfNull(Lg.AmtDr,0) AS AmtDr,IfNull(Lg.Site_Code,0) AS Site_Code ,IfNull(Ct.CityName,'') as City,IfNull(Lg.Narration,'') as Narr,IfNull(Lg.V_type,'') as V_type  
            FROM Ledger LG LEFT JOIN SubGroup SG On  SG.SubCode=LG.SubCode 
            Left Join (Select SILTV.Subcode, Max(SILTV.Agent) as Agent From SubgroupSiteDivisionDetail SILTV  Group By SILTV.Subcode) as LTV On Sg.SubCode = LTV.Subcode
            LEFT JOIN City CT On Ct.CityCode =Sg.CityCode  " + StrCondDt +
            " And IfNull(Lg.AmtDr, 0) <> 0 And LG.SubCode = '" & SubCode & "'   order by Lg.V_Date "
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
                City = AgL.XNull(curr_TempAdjust.Rows(J)("City"))
                Narr = AgL.XNull(curr_TempAdjust.Rows(J)("Narr"))
                VType = AgL.XNull(curr_TempAdjust.Rows(J)("V_type"))

                If DrAmt < CrAmt Then
                    CrAmt = CrAmt - DrAmt
                Else
                    Dim Status As String = ""
                    If DrAmt <> DrAmt - CrAmt Then Status = "A"
                    mQry = " INSERT INTO  #TempRecord 
                            VALUES ('" & DocId & "','" & RecId & "'," & AgL.Chk_Date(V_Date) & ",'" & Supplier & "','" & PartyName & "',
                            " & DrAmt & ", " & DrAmt - CrAmt & ", '" & Status & "', '" & Site & "', '" & City & "', 
                            '" & Narr & "', '" & VType & "')  "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

                    CrAmt = 0
                    Status = ""
                End If
            Next

            If Adv <> 0 Then
                mQry = " INSERT INTO  #TempRecord 
                        VALUES ('','','01/feb/1980', '" & SubCode & "', '" & Party & "', 0, " & Adv & ",'Adv',
                        '" & SiteCode & "', '" & PCity & "','Advance Payment ','') "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
            End If
        Next

        'StrSql = " SELECT *, "
        'StrSql += " (CASE WHEN DateDiff(Day,V_Date," & STRDATE & "  )>= 0 AND  DateDiff(Day,V_Date," & STRDATE & " )<=" & D1 & " THEN  PendingAmt Else 0 end ) AS AmtDay1, "
        'StrSql += " (CASE WHEN DateDiff(Day,V_Date," & STRDATE & " )>" & D1 & " THEN  PendingAmt ELSE 0 end) AS AmtDay2, " & D1 & " As Days "
        'StrSql += " FROM TempRecord where IfNull(PendingAmt,0)<>0  "

        'StrSql = " SELECT DocId, RecId, V_Date As V_Date,subcode, PartyName,BillAmt,PendingAmt,Status,Site_Code,PartyCity,Narration,V_type,"
        'StrSql += " (CASE WHEN julianday(" & STRDATE & ")  - julianday(" & FGetDateQry("V_Date") & ") >= 0 AND  julianday(" & STRDATE & ") - julianday(V_Date) <=" & D1 & " THEN  PendingAmt Else 0 end ) AS AmtDay1, "
        'StrSql += " (CASE WHEN julianday(" & STRDATE & ")  - julianday(" & FGetDateQry("V_Date") & ") >" & D1 & " THEN  PendingAmt ELSE 0 end) AS AmtDay2, " & D1 & " As Days "
        'StrSql += " FROM TempRecord where IfNull(PendingAmt,0)<>0  "

        StrSql = " SELECT *, "
        StrSql += " (CASE WHEN DaysDiff>= 0 AND  DaysDiff<=" & D1 & " THEN  PendingAmt Else 0 end ) AS AmtDay1, "
        StrSql += " (CASE WHEN DaysDiff>" & D1 & " THEN  PendingAmt ELSE 0 end) AS AmtDay2, " & D1 & " As Days "
        StrSql += " FROM ( "
        StrSql += " SELECT DocId, RecId, V_Date As V_Date,subcode, PartyName,BillAmt,PendingAmt,Status,Site_Code,PartyCity,Narration,V_type,"
        If AgL.PubServerName = "" Then
            StrSql += "  julianday(" & STRDATE & ")  - julianday(V_Date) As DaysDiff, "
        Else
            StrSql += " DateDiff(Day,V_Date, " & STRDATE & ") As DaysDiff, "
        End If

        StrSql += " " & D1 & " As Days "
        StrSql += " FROM #TempRecord where IfNull(Round(PendingAmt,2),0)<>0  "
        StrSql += " ) As VMain "




        'StrSql = " DECLARE @TempRecord TABLE (DocId  nvarchar(20),RecId  nvarchar(20),V_Date  nvarchar(30),subcode nvarchar(30),"
        'StrSql += " PartyName nvarchar(500),BillAmt FLOAT,PendingAmt FLOAT,Status  nvarchar(20),Site_Code  nvarchar(20),PartyCity  nvarchar(200),Narration  varchar(max),V_type  nvarchar(20) );	"
        'StrSql += " DECLARE @SubCode VARCHAR(100);DECLARE @Party VARCHAR(200);DECLARE @PCity VARCHAR(200);"
        'StrSql += " DECLARE @Cr float;DECLARE @Adv float;DECLARE @SiteCode VARCHAR(100)"
        'StrSql += " DECLARE CurrTempPayment CURSOR FOR  SELECT LG.SubCode,max(Sg.name) as PartyName,max(CT.CityName) as PCity,IfNull(sum(AmtCr),0) AS AmtCr,"
        'StrSql += " CASE WHEN IfNull(sum(AmtCr),0)> IfNull(sum(AmtDr),0) THEN (IfNull(sum(AmtCr),0) - IfNull(sum(AmtDr),0)) ELSE  0   END AS Advance ,"
        'StrSql += "  Max(LG.Site_Code) as SiteCode "
        'StrSql += " FROM Ledger LG LEFT JOIN SubGroup SG ON SG.SubCode =LG.SubCode  "
        'StrSql += " LEFT JOIN City CT ON SG.CityCode  =CT.CityCode "
        'StrSql += StrCondition1 + " and SG.Nature='Customer'"
        'StrSql += " GROUP BY LG.SubCode "
        'StrSql += " OPEN CurrTempPayment; "
        'StrSql += " FETCH next FROM CurrTempPayment INTO @SubCode,@Party,@PCity,@Cr,@Adv,@SiteCode ;"
        'StrSql += " WHILE @@FETCH_STATUS =0 "
        'StrSql += " BEGIN  DECLARE @CrAmt float; DECLARE @tempval float; "
        'StrSql += " DECLARE @DocId nvarchar(20);DECLARE @RecId nvarchar(20);"
        'StrSql += " DECLARE @V_date nvarchar(20);DECLARE @Supplier nvarchar(20);DECLARE @PartyName nvarchar(300);DECLARE @DrAmt float;"
        'StrSql += " DECLARE @Site nvarchar(30);DECLARE @City nvarchar(100);DECLARE @Narr varchar(max);DECLARE @VType nvarchar(1000);"
        'StrSql += " SET @tempval=0;  "
        'StrSql += " DECLARE curr_TempAdjust CURSOR FOR SELECT  IfNull(LG.DocId,'') AS DocId,Cast(IfNull(LG.V_No,'') as Varchar) AS RecId,IfNull(LG.V_date,'') AS V_date,IfNull(LG.SubCode,'') AS Subcode,"
        'StrSql += " IfNull(SG.Name,'') AS PartyName, IfNull(Lg.AmtDr,0) AS AmtDr,IfNull(Lg.Site_Code,0) AS Site_Code ,IfNull(Ct.CityName,'') as City,IfNull(Lg.Narration,'') as Narr,IfNull(Lg.V_type,'') as V_type  "
        'StrSql += " FROM Ledger LG LEFT JOIN SubGroup SG ON  SG.SubCode=LG.SubCode LEFT JOIN City CT ON Ct.CityCode =Sg.CityCode  "
        'StrSql += StrCondDt + " and IfNull(Lg.AmtDr,0) <>0  AND LG.SubCode = @SubCode   order by Lg.V_Date ; "
        'StrSql += " SET @CrAmt=@Cr  OPEN curr_TempAdjust; "
        'StrSql += " FETCH next FROM curr_TempAdjust INTO @DocId,@RecId,@V_date,@Supplier,@PartyName,@DrAmt,@Site,@City,@Narr,@VType;"
        'StrSql += " WHILE @@FETCH_STATUS =0 BEGIN if   @DrAmt< @CrAmt Begin "
        'StrSql += " SET @CrAmt=@CrAmt-@DrAmt End Else BEGIN  DECLARE @Status nvarchar(20);"
        'StrSql += " IF  @DrAmt<> @DrAmt -@CrAmt SET  @Status='A'"
        'StrSql += " INSERT INTO  @TempRecord VALUES (@DocId,@RecId,@V_date,@Supplier,@PartyName,@DrAmt,@DrAmt -@CrAmt,@Status,@Site,@City,@Narr,@VType);  "
        'StrSql += " Set  @CrAmt = 0 SET @Status='' End"
        'StrSql += " FETCH next FROM curr_TempAdjust INTO @DocId,@RecId,@V_date,@Supplier,@PartyName,@DrAmt,@Site,@City,@Narr,@VType;  End"
        'StrSql += " CLOSE curr_TempAdjust; DEALLOCATE curr_TempAdjust;"
        'StrSql += " IF   @Adv<>0  INSERT INTO  @TempRecord VALUES ('','','01/feb/1980', @SubCode,@Party,0,@Adv,'Adv',@SiteCode,@PCity,'Advance Payment ','');  "
        'StrSql += " FETCH next FROM CurrTempPayment INTO @SubCode,@Party,@PCity,@Cr,@Adv,@SiteCode ; End"
        'StrSql += " CLOSE CurrTempPayment;DEALLOCATE CurrTempPayment;	"
        'StrSql += " SELECT *, "
        'StrSql += " (CASE WHEN DateDiff(Day,V_Date," & STRDATE & "  )>= 0 AND  DateDiff(Day,V_Date," & STRDATE & " )<=" & D1 & " THEN  PendingAmt Else 0 end ) AS AmtDay1, "
        'StrSql += " (CASE WHEN DateDiff(Day,V_Date," & STRDATE & " )>" & D1 & " THEN  PendingAmt ELSE 0 end) AS AmtDay2, " & D1 & " As Days "
        'StrSql += " FROM @TempRecord where IfNull(PendingAmt,0)<>0  "

        DTTemp = CMain.FGetDatTable(StrSql, AgL.GCn)



        If Not DTTemp.Rows.Count > 0 Then MsgBox("No Records Found to Print.") : Exit Sub
        FLoadMainReport("Outstanding_FIFO_Dr", DTTemp)
        CMain.FormulaSet(RptMain, Me.Text, FGMain)
        CMain.FShowReport(RptMain, Me.MdiParent, Me.Text)
    End Sub
    Private Sub FFIFOWsOS_Cr()
        Dim StrCondition1 As String, StrCondDt As String
        Dim StrSql As String, STRDATE As String
        Dim DTTemp As DataTable
        Dim StrCnd As String = ""
        Dim mQry As String = ""
        Dim D1 As Integer
        If Not FIsValid(0) Then Exit Sub
        If Not FIsValid(1) Then Exit Sub

        STRDATE = AgL.Chk_Date(CDate(FGMain(GFilter, 0).Value.ToString).ToString("s"))

        StrCondition1 = " Where LG.V_Date <= " & AgL.Chk_Date(CDate(FGMain(GFilter, 0).Value.ToString).ToString("s")) & "  "
        StrCondDt = " Where LG.V_Date <= " & AgL.Chk_Date(CDate(FGMain(GFilter, 0).Value.ToString).ToString("s")) & "  "

        If Trim(FGMain(GFilterCode, 1).Value) <> "" Then StrCondition1 = StrCondition1 & " And (IfNull(SG.GroupCode,'') In (Select IfNull(AGP.GroupCode,'') From AcGroupPath AGP Where AGP.GroupUnder In (" & FGMain(GFilterCode, 1).Value & ")) Or SG.GroupCode In (" & FGMain(GFilterCode, 1).Value & ")) "
        If Trim(FGMain(GFilterCode, 2).Value) <> "" Then StrCondition1 = StrCondition1 & " And LG.SubCode In (" & FGMain(GFilterCode, 2).Value & ")"

        D1 = Val((FGMain(GFilter, 3).Value.ToString))

        If Trim(FGMain(GFilterCode, 4).Value) <> "" Then
            StrCondition1 = StrCondition1 & " And  LG.Site_Code IN (" & FGMain(GFilterCode, 4).Value & ") "
        Else
            StrCondition1 = StrCondition1 & " And  LG.Site_Code IN (" & AgL.PubSiteList & ") "
        End If

        StrCondition1 = StrCondition1 & " And  LG.DivCode = '" & AgL.PubDivCode & "' "

        Try
            mQry = "Drop Table #TempRecord"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        Catch ex As Exception
        End Try

        mQry = " CREATE Temporary TABLE #TempRecord (DocId  nvarchar(21),RecId  nvarchar(20),V_Date  nvarchar(30),subcode nvarchar(30),"
        mQry += " PartyName nvarchar(500),BillAmt FLOAT,PendingAmt FLOAT,Status  nvarchar(20),Site_Code  nvarchar(20),PartyCity  nvarchar(200),Narration  varchar(2000),V_type  nvarchar(20));	"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

        Dim SubCode As String = "", Party As String = "", PCity As String = "", SiteCode As String = ""
        Dim Dr As Double = 0, Adv As Double = 0

        Dim CurrTempPayment As DataTable = Nothing

        mQry = " Select LG.SubCode, max(Sg.name) As PartyName, max(CT.CityName) As PCity, IfNull(sum(AmtDr), 0) As AmtDr,
        Case When IfNull(sum(AmtDr),0)> IfNull(sum(AmtCr),0) Then (IfNull(sum(AmtDr),0) - IfNull(sum(AmtCr),0)) Else  0   End As Advance, Max(LG.Site_Code) As SiteCode 
        From Ledger LG LEFT Join SubGroup SG On SG.SubCode =LG.SubCode  
        Left Join City CT On SG.CityCode  =CT.CityCode " + StrCondition1 + " And SG.Nature ='Supplier'
        Group BY LG.SubCode "
        CurrTempPayment = AgL.FillData(mQry, AgL.GCn).Tables(0)

        For I As Integer = 0 To CurrTempPayment.Rows.Count - 1
            SubCode = AgL.XNull(CurrTempPayment.Rows(I)("SubCode"))
            Party = AgL.XNull(CurrTempPayment.Rows(I)("PartyName"))
            PCity = AgL.XNull(CurrTempPayment.Rows(I)("PCity"))
            Dr = AgL.XNull(CurrTempPayment.Rows(I)("AmtDr"))
            Adv = AgL.XNull(CurrTempPayment.Rows(I)("Advance"))
            SiteCode = AgL.XNull(CurrTempPayment.Rows(I)("SiteCode"))

            Dim DrAmt As Double = 0, tempval As Double = 0, CrAmt As Double = 0
            Dim DocId As String = "", RecId As String = "", Supplier As String = "", PartyName As String = "", Site As String = "", City As String = "", Narr As String = "", VType As String = ""
            Dim V_Date As DateTime

            tempval = 0

            Dim curr_TempAdjust As DataTable = Nothing

            mQry = " SELECT  IfNull(LG.DocId,'') AS DocId,LG.DivCode || LG.Site_Code || '-' || LG.V_Type || '-' || LG.RecId As RecId,IfNull(LG.V_date,'') AS V_date,IfNull(LG.SubCode,'') AS Subcode,
            IfNull(SG.Name,'') AS PartyName, IfNull(Lg.AmtCr,0) AS AmtCr,IfNull(Lg.Site_Code,0) AS Site_Code ,IfNull(Ct.CityName,'') as City,IfNull(Lg.Narration,'') as Narr,IfNull(Lg.V_type,'') as V_type  
            FROM Ledger LG LEFT JOIN SubGroup SG On  SG.SubCode=LG.SubCode 
            LEFT JOIN City CT On Ct.CityCode =Sg.CityCode  " + StrCondDt + " And IfNull(Lg.AmtCr, 0) <> 0 
            And LG.SubCode = '" & SubCode & "'     order by Lg.V_Date  "
            curr_TempAdjust = AgL.FillData(mQry, AgL.GCn).Tables(0)

            DrAmt = Dr

            For J As Integer = 0 To curr_TempAdjust.Rows.Count - 1
                DocId = AgL.XNull(curr_TempAdjust.Rows(J)("DocId"))
                RecId = AgL.XNull(curr_TempAdjust.Rows(J)("RecId"))
                V_Date = CDate(AgL.XNull(curr_TempAdjust.Rows(J)("V_Date")))
                Supplier = AgL.XNull(curr_TempAdjust.Rows(J)("Subcode"))
                PartyName = AgL.XNull(curr_TempAdjust.Rows(J)("PartyName"))
                CrAmt = AgL.XNull(curr_TempAdjust.Rows(J)("AmtCr"))
                Site = AgL.XNull(curr_TempAdjust.Rows(J)("Site_Code"))
                City = AgL.XNull(curr_TempAdjust.Rows(J)("City"))
                Narr = AgL.XNull(curr_TempAdjust.Rows(J)("Narr"))
                VType = AgL.XNull(curr_TempAdjust.Rows(J)("V_type"))

                If CrAmt < DrAmt Then
                    DrAmt = DrAmt - CrAmt
                Else
                    Dim Status As String = ""
                    If CrAmt <> CrAmt - DrAmt Then Status = "A"
                    mQry = " INSERT INTO  #TempRecord 
                            VALUES ('" & DocId & "','" & RecId & "','" & CDate(V_Date) & "','" & Supplier & "','" & PartyName & "',
                            " & CrAmt & ", " & CrAmt - DrAmt & ",
                            '" & Status & "', '" & Site & "', '" & City & "', '" & Narr & "', '" & VType & "')  "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

                    DrAmt = 0
                    Status = ""
                End If
            Next

            If Adv <> 0 Then
                mQry = " INSERT INTO  #TempRecord VALUES ('','','01/feb/1980', '" & SubCode & "',
                    '" & Party & "',0,'" & Adv & "','Adv','" & SiteCode & "','" & PCity & "','Advance Payment ','') "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
            End If
        Next



        'StrSql = " SELECT DocId,RecId,V_Date As V_Date,subcode, PartyName ,BillAmt ,PendingAmt ,Status  ,Site_Code  ,PartyCity  ,Narration  ,V_type,"
        'StrSql += "(CASE WHEN julianday(" & STRDATE & ")  - julianday(V_Date) >= 0 AND  julianday(" & STRDATE & ") - julianday(V_Date) <=" & D1 & " THEN  PendingAmt Else 0 end ) AS AmtDay1, "
        'StrSql += "(CASE WHEN julianday(" & STRDATE & ")  - julianday(V_Date) >" & D1 & " THEN  PendingAmt ELSE 0 end) AS AmtDay2, " & D1 & " As Days "
        'StrSql += "FROM TempRecord where IfNull(PendingAmt,0)<>0  "


        StrSql = " SELECT *,"
        StrSql += "(CASE WHEN DaysDiff >= 0 AND  DaysDiff <=" & D1 & " THEN  PendingAmt Else 0 end ) AS AmtDay1, "
        StrSql += "(CASE WHEN DaysDiff >" & D1 & " THEN  PendingAmt ELSE 0 end) AS AmtDay2, " & D1 & " As Days "
        StrSql += " FROM ( "
        StrSql += " SELECT DocId,RecId,V_Date As V_Date,subcode, PartyName ,BillAmt ,PendingAmt ,Status  ,Site_Code  ,PartyCity  ,Narration  ,V_type,"
        If AgL.PubServerName = "" Then
            StrSql += "julianday(" & STRDATE & ")  - julianday(" & FGetDateQry("V_Date") & ") As DaysDiff, "
        Else
            StrSql += " DateDiff(Day,V_Date, " & STRDATE & ") As DaysDiff, "
        End If

        StrSql += "" & D1 & " As Days "
        StrSql += "FROM #TempRecord where IfNull(Round(PendingAmt,2),0)<>0  "
        StrSql += " ) As VMain "



        'StrSql = " DECLARE @TempRecord TABLE (DocId  nvarchar(20),RecId  nvarchar(20),V_Date  nvarchar(30),subcode nvarchar(30),"
        'StrSql += " PartyName nvarchar(500),BillAmt FLOAT,PendingAmt FLOAT,Status  nvarchar(20),Site_Code  nvarchar(20),PartyCity  nvarchar(200),Narration  varchar(max),V_type  nvarchar(20));	"
        'StrSql += " DECLARE @SubCode VARCHAR(100);DECLARE @Party VARCHAR(200);DECLARE @PCity VARCHAR(200);"
        'StrSql += " DECLARE @Dr float;DECLARE @Adv float;DECLARE @SiteCode VARCHAR(100)"
        'StrSql += " DECLARE CurrTempPayment CURSOR FOR  SELECT LG.SubCode,max(Sg.name) as PartyName,max(CT.CityName) as PCity,IfNull(sum(AmtDr),0) AS AmtDr,"
        'StrSql += " CASE WHEN IfNull(sum(AmtDr),0)> IfNull(sum(AmtCr),0) THEN (IfNull(sum(AmtDr),0) - IfNull(sum(AmtCr),0)) ELSE  0   END AS Advance ,Max(LG.Site_Code) as SiteCode "
        'StrSql += " FROM Ledger LG LEFT JOIN SubGroup SG ON SG.SubCode =LG.SubCode  "
        'StrSql += " LEFT JOIN City CT ON SG.CityCode  =CT.CityCode "
        'StrSql += StrCondition1 + " and SG.Nature='Supplier'"
        'StrSql += " GROUP BY LG.SubCode "
        'StrSql += " OPEN CurrTempPayment; "
        'StrSql += " FETCH next FROM CurrTempPayment INTO @SubCode,@Party,@PCity,@Dr,@Adv,@SiteCode ;"
        'StrSql += " WHILE @@FETCH_STATUS =0 "
        'StrSql += " BEGIN  DECLARE @DrAmt float; DECLARE @tempval float; "
        'StrSql += " DECLARE @DocId nvarchar(20);DECLARE @RecId nvarchar(20);"
        'StrSql += " DECLARE @V_date nvarchar(20);DECLARE @Supplier nvarchar(20);DECLARE @PartyName nvarchar(300);DECLARE @CrAmt float;"
        'StrSql += " DECLARE @Site nvarchar(30);DECLARE @City nvarchar(100);DECLARE @Narr varchar(max);DECLARE @VType nvarchar(1000);"
        'StrSql += " SET @tempval=0;  "
        'StrSql += " DECLARE curr_TempAdjust CURSOR FOR SELECT  IfNull(LG.DocId,'') AS DocId,Cast(IfNull(LG.V_No,'') as Varchar) AS RecId,IfNull(LG.V_date,'') AS V_date,IfNull(LG.SubCode,'') AS Subcode,"
        'StrSql += " IfNull(SG.Name,'') AS PartyName, IfNull(Lg.AmtCr,0) AS AmtCr,IfNull(Lg.Site_Code,0) AS Site_Code ,IfNull(Ct.CityName,'') as City,IfNull(Lg.Narration,'') as Narr,IfNull(Lg.V_type,'') as V_type  "
        'StrSql += " FROM Ledger LG LEFT JOIN SubGroup SG ON  SG.SubCode=LG.SubCode LEFT JOIN City CT ON Ct.CityCode =Sg.CityCode  "
        'StrSql += StrCondDt + " and IfNull(Lg.AmtCr,0) <>0  AND LG.SubCode = @SubCode   order by Lg.V_Date ; "
        'StrSql += " SET @DrAmt=@Dr  OPEN curr_TempAdjust; "
        'StrSql += " FETCH next FROM curr_TempAdjust INTO @DocId,@RecId,@V_date,@Supplier,@PartyName,@CrAmt,@Site,@City,@Narr,@VType;"
        'StrSql += " WHILE @@FETCH_STATUS =0 BEGIN if   @CrAmt< @DrAmt Begin "
        'StrSql += " SET @DrAmt=@DrAmt-@CrAmt End Else BEGIN  DECLARE @Status nvarchar(20);"
        'StrSql += " IF  @CrAmt<> @CrAmt -@DrAmt SET  @Status='A'"
        'StrSql += " INSERT INTO  @TempRecord VALUES (@DocId,@RecId,@V_date,@Supplier,@PartyName,@CrAmt,@CrAmt -@DrAmt,@Status,@Site,@City,@Narr,@VType);  "
        'StrSql += " Set  @DrAmt = 0 SET @Status='' End"
        'StrSql += " FETCH next FROM curr_TempAdjust INTO @DocId,@RecId,@V_date,@Supplier,@PartyName,@CrAmt,@Site,@City,@Narr,@VType;  End"
        'StrSql += " CLOSE curr_TempAdjust; DEALLOCATE curr_TempAdjust;"
        'StrSql += " IF   @Adv<>0  INSERT INTO  @TempRecord VALUES ('','','01/feb/1980', @SubCode,@Party,0,@Adv,'Adv',@SiteCode,@PCity,'Advance Payment ','');  "
        'StrSql += " FETCH next FROM CurrTempPayment INTO @SubCode,@Party,@PCity,@Dr,@Adv,@SiteCode ; End "
        'StrSql += " CLOSE CurrTempPayment;DEALLOCATE CurrTempPayment;	"
        'StrSql += " SELECT *,"
        'StrSql += "(CASE WHEN DateDiff(Day,V_Date," & STRDATE & "  )>= 0 AND  DateDiff(Day,V_Date," & STRDATE & " )<=" & D1 & " THEN  PendingAmt Else 0 end ) AS AmtDay1, "
        'StrSql += "(CASE WHEN DateDiff(Day,V_Date," & STRDATE & " )>" & D1 & " THEN  PendingAmt ELSE 0 end) AS AmtDay2, " & D1 & " As Days "
        'StrSql += "FROM @TempRecord where IfNull(PendingAmt,0)<>0  "

        DTTemp = CMain.FGetDatTable(StrSql, AgL.GCn)



        If Not DTTemp.Rows.Count > 0 Then MsgBox("No Records Found to Print.") : Exit Sub
        FLoadMainReport("Outstanding_FIFO_Cr", DTTemp)
        CMain.FormulaSet(RptMain, Me.Text, FGMain)
        CMain.FShowReport(RptMain, Me.MdiParent, Me.Text)
    End Sub


    Private Sub FFIFOWsOS_DrSqlServer()
        Dim StrCondition1 As String, StrCondDt As String
        Dim DTTemp As DataTable
        Dim StrSql As String, STRDATE As String
        Dim D1 As Integer

        If Not FIsValid(0) Then Exit Sub

        STRDATE = AgL.Chk_Date(FGMain(GFilter, 0).Value.ToString)

        StrCondition1 = " Where LG.V_Date < = " & AgL.Chk_Date(FGMain(GFilter, 0).Value.ToString) & "  "
        StrCondDt = " Where LG.V_Date < = " & AgL.Chk_Date(FGMain(GFilter, 0).Value.ToString) & "  "

        If Trim(FGMain(GFilterCode, 1).Value) <> "" Then StrCondition1 = StrCondition1 & " And (IsNull(SG.GroupCode,'') In (Select IsNull(AGP.GroupCode,'') From AcGroupPath AGP Where AGP.GroupUnder In (" & FGMain(GFilterCode, 1).Value & ")) Or SG.GroupCode In (" & FGMain(GFilterCode, 1).Value & ")) "
        If Trim(FGMain(GFilterCode, 2).Value) <> "" Then StrCondition1 = StrCondition1 & " And LG.SubCode In (" & FGMain(GFilterCode, 2).Value & ")"

        D1 = Val((FGMain(GFilter, 3).Value.ToString))

        If Trim(FGMain(GFilterCode, 4).Value) <> "" Then
            StrCondition1 = StrCondition1 & " And  LG.Site_Code IN (" & FGMain(GFilterCode, 4).Value & ") "
        Else
            StrCondition1 = StrCondition1 & " And  LG.Site_Code IN (" & AgL.PubSiteList & ") "
        End If

        StrSql = " DECLARE @TempRecord TABLE (DocId  nvarchar(20),RecId  nvarchar(20),V_Date  nvarchar(30),subcode nvarchar(30),"
        StrSql += " PartyName nvarchar(500),BillAmt FLOAT,PendingAmt FLOAT,Status  nvarchar(20),Site_Code  nvarchar(20),PartyCity  nvarchar(200),Narration  varchar(max),V_type  nvarchar(20) );	"
        StrSql += " DECLARE @SubCode VARCHAR(100);DECLARE @Party VARCHAR(200);DECLARE @PCity VARCHAR(200);"
        StrSql += " DECLARE @Cr float;DECLARE @Adv float;DECLARE @SiteCode VARCHAR(100)"
        StrSql += " DECLARE CurrTempPayment CURSOR FOR  SELECT LG.SubCode,max(Sg.name) as PartyName,max(CT.CityName) as PCity,isnull(sum(AmtCr),0) AS AmtCr,"
        StrSql += " CASE WHEN isnull(sum(AmtCr),0)> isnull(sum(AmtDr),0) THEN (isnull(sum(AmtCr),0) - isnull(sum(AmtDr),0)) ELSE  0   END AS Advance ,"
        StrSql += "  Max(LG.Site_Code) as SiteCode "
        StrSql += " FROM Ledger LG LEFT JOIN SubGroup SG ON SG.SubCode =LG.SubCode  "
        StrSql += " LEFT JOIN City CT ON SG.CityCode  =CT.CityCode "
        StrSql += StrCondition1 + " and SG.Nature='Customer'"
        StrSql += " GROUP BY LG.SubCode "
        StrSql += " OPEN CurrTempPayment; "
        StrSql += " FETCH next FROM CurrTempPayment INTO @SubCode,@Party,@PCity,@Cr,@Adv,@SiteCode ;"
        StrSql += " WHILE @@FETCH_STATUS =0 "
        StrSql += " BEGIN  DECLARE @CrAmt float; DECLARE @tempval float; "
        StrSql += " DECLARE @DocId nvarchar(20);DECLARE @RecId nvarchar(20);"
        StrSql += " DECLARE @V_date nvarchar(20);DECLARE @Supplier nvarchar(20);DECLARE @PartyName nvarchar(300);DECLARE @DrAmt float;"
        StrSql += " DECLARE @Site nvarchar(30);DECLARE @City nvarchar(100);DECLARE @Narr varchar(max);DECLARE @VType nvarchar(1000);"
        StrSql += " SET @tempval=0;  "
        StrSql += " DECLARE curr_TempAdjust CURSOR FOR SELECT  isnull(LG.DocId,'') AS DocId,Convert(Varchar,isnull(LG.V_No,'')) AS RecId,isnull(LG.V_date,'') AS V_date,isnull(LG.SubCode,'') AS Subcode,"
        StrSql += " isnull(SG.Name,'') AS PartyName, isnull(Lg.AmtDr,0) AS AmtDr,isnull(Lg.Site_Code,0) AS Site_Code ,isnull(Ct.CityName,'') as City,isnull(Lg.Narration,'') as Narr,isnull(Lg.V_type,'') as V_type  "
        StrSql += " FROM Ledger LG LEFT JOIN SubGroup SG ON  SG.SubCode=LG.SubCode LEFT JOIN City CT ON Ct.CityCode =Sg.CityCode  "
        StrSql += StrCondDt + " and isnull(Lg.AmtDr,0) <>0  AND LG.SubCode = @SubCode   order by Lg.V_Date ; "
        StrSql += " SET @CrAmt=@Cr  OPEN curr_TempAdjust; "
        StrSql += " FETCH next FROM curr_TempAdjust INTO @DocId,@RecId,@V_date,@Supplier,@PartyName,@DrAmt,@Site,@City,@Narr,@VType;"
        StrSql += " WHILE @@FETCH_STATUS =0 BEGIN if   @DrAmt< @CrAmt Begin "
        StrSql += " SET @CrAmt=@CrAmt-@DrAmt End Else BEGIN  DECLARE @Status nvarchar(20);"
        StrSql += " IF  @DrAmt<> @DrAmt -@CrAmt SET  @Status='A'"
        StrSql += " INSERT INTO  @TempRecord VALUES (@DocId,@RecId,@V_date,@Supplier,@PartyName,@DrAmt,@DrAmt -@CrAmt,@Status,@Site,@City,@Narr,@VType);  "
        StrSql += " Set  @CrAmt = 0 SET @Status='' End"
        StrSql += " FETCH next FROM curr_TempAdjust INTO @DocId,@RecId,@V_date,@Supplier,@PartyName,@DrAmt,@Site,@City,@Narr,@VType;  End"
        StrSql += " CLOSE curr_TempAdjust; DEALLOCATE curr_TempAdjust;"
        StrSql += " IF   @Adv<>0  INSERT INTO  @TempRecord VALUES ('','','01/feb/1980', @SubCode,@Party,0,@Adv,'Adv',@SiteCode,@PCity,'Advance Payment ','');  "
        StrSql += " FETCH next FROM CurrTempPayment INTO @SubCode,@Party,@PCity,@Cr,@Adv,@SiteCode ; End"
        StrSql += " CLOSE CurrTempPayment;DEALLOCATE CurrTempPayment;	"
        StrSql += " SELECT *, "
        StrSql += " (CASE WHEN DateDiff(Day,V_Date," & STRDATE & "  )>= 0 AND  DateDiff(Day,V_Date," & STRDATE & " )<=" & D1 & " THEN  PendingAmt Else 0 end ) AS AmtDay1, "
        StrSql += " (CASE WHEN DateDiff(Day,V_Date," & STRDATE & " )>" & D1 & " THEN  PendingAmt ELSE 0 end) AS AmtDay2, " & D1 & " As Days "
        StrSql += " FROM @TempRecord where isnull(PendingAmt,0)<>0  "

        DTTemp = CMain.FGetDatTable(StrSql, AgL.GCn)
        If Not DTTemp.Rows.Count > 0 Then MsgBox("No Records Found to Print.") : Exit Sub
        FLoadMainReport("Outstanding_FIFO_Dr", DTTemp)
        CMain.FormulaSet(RptMain, Me.Text, FGMain)
        CMain.FShowReport(RptMain, Me.MdiParent, Me.Text)
    End Sub
    Private Sub FFIFOWsOS_CrSqlServer()
        Dim StrCondition1 As String, StrCondDt As String
        Dim StrSql As String, STRDATE As String
        Dim DTTemp As DataTable
        Dim StrCnd As String = ""
        Dim D1 As Integer
        If Not FIsValid(0) Then Exit Sub
        If Not FIsValid(1) Then Exit Sub

        STRDATE = AgL.Chk_Date(FGMain(GFilter, 0).Value.ToString)

        StrCondition1 = " Where LG.V_Date < = " & AgL.Chk_Date(FGMain(GFilter, 0).Value.ToString) & "  "
        StrCondDt = " Where LG.V_Date < = " & AgL.Chk_Date(FGMain(GFilter, 0).Value.ToString) & "  "

        If Trim(FGMain(GFilterCode, 1).Value) <> "" Then StrCondition1 = StrCondition1 & " And (IsNull(SG.GroupCode,'') In (Select IsNull(AGP.GroupCode,'') From AcGroupPath AGP Where AGP.GroupUnder In (" & FGMain(GFilterCode, 1).Value & ")) Or SG.GroupCode In (" & FGMain(GFilterCode, 1).Value & ")) "
        If Trim(FGMain(GFilterCode, 2).Value) <> "" Then StrCondition1 = StrCondition1 & " And LG.SubCode In (" & FGMain(GFilterCode, 2).Value & ")"

        D1 = Val((FGMain(GFilter, 3).Value.ToString))

        If Trim(FGMain(GFilterCode, 4).Value) <> "" Then
            StrCondition1 = StrCondition1 & " And  LG.Site_Code IN (" & FGMain(GFilterCode, 4).Value & ") "
        Else
            StrCondition1 = StrCondition1 & " And  LG.Site_Code IN (" & AgL.PubSiteList & ") "
        End If

        StrSql = " DECLARE @TempRecord TABLE (DocId  nvarchar(20),RecId  nvarchar(20),V_Date  nvarchar(30),subcode nvarchar(30),"
        StrSql += " PartyName nvarchar(500),BillAmt FLOAT,PendingAmt FLOAT,Status  nvarchar(20),Site_Code  nvarchar(20),PartyCity  nvarchar(200),Narration  varchar(max),V_type  nvarchar(20));	"
        StrSql += " DECLARE @SubCode VARCHAR(100);DECLARE @Party VARCHAR(200);DECLARE @PCity VARCHAR(200);"
        StrSql += " DECLARE @Dr float;DECLARE @Adv float;DECLARE @SiteCode VARCHAR(100)"
        StrSql += " DECLARE CurrTempPayment CURSOR FOR  SELECT LG.SubCode,max(Sg.name) as PartyName,max(CT.CityName) as PCity,isnull(sum(AmtDr),0) AS AmtDr,"
        StrSql += " CASE WHEN isnull(sum(AmtDr),0)> isnull(sum(AmtCr),0) THEN (isnull(sum(AmtDr),0) - isnull(sum(AmtCr),0)) ELSE  0   END AS Advance ,Max(LG.Site_Code) as SiteCode "
        StrSql += " FROM Ledger LG LEFT JOIN SubGroup SG ON SG.SubCode =LG.SubCode  "
        StrSql += " LEFT JOIN City CT ON SG.CityCode  =CT.CityCode "
        StrSql += StrCondition1 + " and SG.Nature='Supplier'"
        StrSql += " GROUP BY LG.SubCode "
        StrSql += " OPEN CurrTempPayment; "
        StrSql += " FETCH next FROM CurrTempPayment INTO @SubCode,@Party,@PCity,@Dr,@Adv,@SiteCode ;"
        StrSql += " WHILE @@FETCH_STATUS =0 "
        StrSql += " BEGIN  DECLARE @DrAmt float; DECLARE @tempval float; "
        StrSql += " DECLARE @DocId nvarchar(20);DECLARE @RecId nvarchar(20);"
        StrSql += " DECLARE @V_date nvarchar(20);DECLARE @Supplier nvarchar(20);DECLARE @PartyName nvarchar(300);DECLARE @CrAmt float;"
        StrSql += " DECLARE @Site nvarchar(30);DECLARE @City nvarchar(100);DECLARE @Narr varchar(max);DECLARE @VType nvarchar(1000);"
        StrSql += " SET @tempval=0;  "
        StrSql += " DECLARE curr_TempAdjust CURSOR FOR SELECT  isnull(LG.DocId,'') AS DocId,Convert(Varchar,isnull(LG.V_No,'')) AS RecId,isnull(LG.V_date,'') AS V_date,isnull(LG.SubCode,'') AS Subcode,"
        StrSql += " isnull(SG.Name,'') AS PartyName, isnull(Lg.AmtCr,0) AS AmtCr,isnull(Lg.Site_Code,0) AS Site_Code ,isnull(Ct.CityName,'') as City,isnull(Lg.Narration,'') as Narr,isnull(Lg.V_type,'') as V_type  "
        StrSql += " FROM Ledger LG LEFT JOIN SubGroup SG ON  SG.SubCode=LG.SubCode LEFT JOIN City CT ON Ct.CityCode =Sg.CityCode  "
        StrSql += StrCondDt + " and isnull(Lg.AmtCr,0) <>0  AND LG.SubCode = @SubCode   order by Lg.V_Date ; "
        StrSql += " SET @DrAmt=@Dr  OPEN curr_TempAdjust; "
        StrSql += " FETCH next FROM curr_TempAdjust INTO @DocId,@RecId,@V_date,@Supplier,@PartyName,@CrAmt,@Site,@City,@Narr,@VType;"
        StrSql += " WHILE @@FETCH_STATUS =0 BEGIN if   @CrAmt< @DrAmt Begin "
        StrSql += " SET @DrAmt=@DrAmt-@CrAmt End Else BEGIN  DECLARE @Status nvarchar(20);"
        StrSql += " IF  @CrAmt<> @CrAmt -@DrAmt SET  @Status='A'"
        StrSql += " INSERT INTO  @TempRecord VALUES (@DocId,@RecId,@V_date,@Supplier,@PartyName,@CrAmt,@CrAmt -@DrAmt,@Status,@Site,@City,@Narr,@VType);  "
        StrSql += " Set  @DrAmt = 0 SET @Status='' End"
        StrSql += " FETCH next FROM curr_TempAdjust INTO @DocId,@RecId,@V_date,@Supplier,@PartyName,@CrAmt,@Site,@City,@Narr,@VType;  End"
        StrSql += " CLOSE curr_TempAdjust; DEALLOCATE curr_TempAdjust;"
        StrSql += " IF   @Adv<>0  INSERT INTO  @TempRecord VALUES ('','','01/feb/1980', @SubCode,@Party,0,@Adv,'Adv',@SiteCode,@PCity,'Advance Payment ','');  "
        StrSql += " FETCH next FROM CurrTempPayment INTO @SubCode,@Party,@PCity,@Dr,@Adv,@SiteCode ; End "
        StrSql += " CLOSE CurrTempPayment;DEALLOCATE CurrTempPayment;	"
        StrSql += " SELECT *,"
        StrSql += "(CASE WHEN DateDiff(Day,V_Date," & STRDATE & "  )>= 0 AND  DateDiff(Day,V_Date," & STRDATE & " )<=" & D1 & " THEN  PendingAmt Else 0 end ) AS AmtDay1, "
        StrSql += "(CASE WHEN DateDiff(Day,V_Date," & STRDATE & " )>" & D1 & " THEN  PendingAmt ELSE 0 end) AS AmtDay2, " & D1 & " As Days "
        StrSql += "FROM @TempRecord where isnull(PendingAmt,0)<>0  "

        DTTemp = CMain.FGetDatTable(StrSql, AgL.GCn)
        If Not DTTemp.Rows.Count > 0 Then MsgBox("No Records Found to Print.") : Exit Sub
        FLoadMainReport("Outstanding_FIFO_Cr", DTTemp)
        CMain.FormulaSet(RptMain, Me.Text, FGMain)
        CMain.FShowReport(RptMain, Me.MdiParent, Me.Text)
    End Sub
    Private Sub FIni_DailyTransBook()

        FSetValue(0, "From Date", FGDataType.DT_Date, FilterCodeType.DTNone, AgL.PubStartDate, False)
        FSetValue(1, "To Date", FGDataType.DT_Date, FilterCodeType.DTNone, AgL.PubLoginDate, False)

        FSetValue(2, "Site Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, AgL.PubSiteName & "|'" & AgL.PubSiteCode & "'")
        FRH_Multiple(2) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable(
                          "Select 'o' As Tick,Sm.Code,Sm.Name From Sitemast Sm where code in (" & AgL.PubSiteList & ")   Order By Sm.Name",
                          AgL.GCn)), "", 300, 360, , , False, AgL.PubSiteCode)
        FRH_Multiple(2).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(2).FFormatColumn(1, , 0, , False)
        FRH_Multiple(2).FFormatColumn(2, "Name", 240, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(3, "Division Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, AgL.PubDivName & "|'" & AgL.PubDivCode & "'")
        FRH_Multiple(3) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable(
                          "Select 'o' As Tick,Sm.Div_Code Code, Sg.DispName Name From Division Sm 
                           Left Join Subgroup Sg On SM.Subcode = Sg.Subcode 
                           where Sm.Div_code in (" & AgL.PubDivisionList & ")   
                          Order By Sg.DispName",
                          AgL.GCn)), "", 300, 360, , , False, AgL.PubSiteCode)
        FRH_Multiple(3).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(3).FFormatColumn(1, , 0, , False)
        FRH_Multiple(3).FFormatColumn(2, "Name", 240, DataGridViewContentAlignment.MiddleLeft)


        FSetValue(4, "Account Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, "All", True)
        FRH_Multiple(4) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable(
                          "Select 'o' As Tick,SG.SubCode,SG.Name From SubGroup SG  Where (SG.SiteList Like '%|" & AgL.PubSiteCode & "|%' Or SG.SiteList Is Null) Order by SG.Name",
                          AgL.GCn)), "", 300, 370, , , False)
        FRH_Multiple(4).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(4).FFormatColumn(1, "", 0, , False)
        FRH_Multiple(4).FFormatColumn(2, "Name", 250, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(5, "Voucher Type", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, "All", True)
        FRH_Multiple(5) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable(
                          "Select 'o' As Tick,VT.V_Type,VT.Description From Voucher_Type VT  Where VT.V_Type in ( Select DISTINCT V_Type from Ledger) Order by VT.Description",
                          AgL.GCn)), "", 300, 320, , , False)
        FRH_Multiple(5).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(5).FFormatColumn(1, "", 0, , False)
        FRH_Multiple(5).FFormatColumn(2, "Voucher Type", 200, DataGridViewContentAlignment.MiddleLeft)

    End Sub
    Private Sub FDailyTransBook()
        Dim StrCondition1 As String, StrConditionOP As String, StrConditionSite As String
        Dim StrConditionMain As String
        Dim DTTemp As DataTable

        If Not FIsValid(0) Then Exit Sub
        If Not FIsValid(1) Then Exit Sub
        If Not FIsValid(2) Then Exit Sub
        If Not FIsValid(3) Then Exit Sub
        If Not FIsValid(4) Then Exit Sub

        StrConditionMain = " Where ( Date(V_Date) Between " & AgL.Chk_Date(CDate(FGMain(GFilter, 0).Value.ToString).ToString("s")) & " And " & AgL.Chk_Date(CDate(FGMain(GFilter, 1).Value.ToString).ToString("s")) & ") "
        StrCondition1 = " Where ( Date(LG.V_Date) Between " & AgL.Chk_Date(CDate(FGMain(GFilter, 0).Value.ToString).ToString("s")) & " And " & AgL.Chk_Date(CDate(FGMain(GFilter, 1).Value.ToString).ToString("s")) & ") "
        StrConditionOP = " Where Date(LG.V_Date) < " & AgL.Chk_Date(CDate(FGMain(GFilter, 0).Value.ToString).ToString("s")) & " "
        StrConditionSite = ""
        If Trim(FGMain(GFilterCode, 2).Value) <> "" Then
            StrConditionSite += " And LG.Site_Code In (" & FGMain(GFilterCode, 2).Value & ") "
        Else
            StrConditionSite += " And LG.Site_Code In  (" & AgL.PubSiteList & ") "
        End If

        If Trim(FGMain(GFilterCode, 3).Value) <> "" Then
            StrConditionSite += " And LG.DivCode In (" & FGMain(GFilterCode, 3).Value & ") "
        Else
            StrConditionSite += " And LG.DivCode In  (" & AgL.PubDivisionList & ") "
        End If


        If Trim(FGMain(GFilterCode, 4).Value) <> "" Then StrCondition1 = StrCondition1 & " And LG.SubCode In (" & FGMain(GFilterCode, 4).Value & ")"
        If Trim(FGMain(GFilterCode, 4).Value) <> "" Then StrConditionOP = StrConditionOP & " And LG.SubCode In (" & FGMain(GFilterCode, 4).Value & ")"


        If Trim(FGMain(GFilterCode, 5).Value) <> "" Then StrCondition1 = StrCondition1 & " And LG.V_Type In (" & FGMain(GFilterCode, 5).Value & ")"
        If Trim(FGMain(GFilterCode, 5).Value) <> "" Then StrConditionOP = StrConditionOP & " And LG.V_Type In (" & FGMain(GFilterCode, 5).Value & ")"



        Dim mQry As String = " CREATE Temporary TABLE #TempRecord (V_Date DateTime, AmtDr Float, AmtCr  Float,  OPBal Float)"

        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

        mQry = "INSERT INTO #TempRecord 
                Select " & FGetDateQry(AgL.Chk_Text(FGMain(GFilter, 0).Value)) & " As V_Date,
                0 As AmtDr,0 As AmtCr,
                IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LG.AmtCr),0) As OPBal
                From Ledger LG " + StrConditionOP + StrConditionSite +
                " Group By LG.V_Date "
        mQry = "INSERT INTO #TempRecord 
                Select " & AgL.Chk_Date(FGMain(GFilter, 0).Value) & " As V_Date,
                0 As AmtDr,0 As AmtCr,
                IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LG.AmtCr),0) As OPBal
                From Ledger LG " + StrConditionOP + StrConditionSite +
                " Group By LG.V_Date "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)


        '========== Head Query Date Wise Grouping ====================================
        StrSQLQuery = "Select V_Date,IfNull(Sum(AmtDr),0) As AmtDr,IfNull(Sum(AmtCr),0) As AmtCr, "
        StrSQLQuery += "IfNull(Sum(OPBal),0) As OPBal "
        StrSQLQuery += "From ( "
        '========== For Detail Section =======
        StrSQLQuery += "Select	LG.V_Date, "
        StrSQLQuery += "IfNull(LG.AmtDr,0) As AmtDr ,IfNull(LG.AmtCr,0) As AmtCr,0 As OPBal  "
        StrSQLQuery += "From Ledger LG "
        StrSQLQuery += StrCondition1 + StrConditionSite
        StrSQLQuery += "Union All "

        '======= For Opening Balance =========
        'StrSQLQuery += "Select " & AgL.Chk_Text(CDate(FGMain(GFilter, 0).Value.ToString).ToString("s")) & " As V_Date, "
        'StrSQLQuery += "0 As AmtDr,0 As AmtCr, "
        'StrSQLQuery += "IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LG.AmtCr),0) As OPBal "
        'StrSQLQuery += "From Ledger LG "
        'StrSQLQuery += StrConditionOP + StrConditionSite
        'StrSQLQuery += "Group By LG.V_Date "
        StrSQLQuery += " Select V_Date, AmtDr, AmtCr,  OPBal From #TempRecord "
        StrSQLQuery += " ) As Tmp "
        StrSQLQuery += StrConditionMain
        StrSQLQuery += "Group By V_Date "
        StrSQLQuery += "Order By V_Date "

        StrSQLQuery = AgL.GetBackendBasedQuery(StrSQLQuery)
        DTTemp = CMain.FGetDatTable(StrSQLQuery, AgL.GCn)

        mQry = "Drop Table #TempRecord"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

        If Not DTTemp.Rows.Count > 0 Then MsgBox("No Records Found To Print.") : Exit Sub

        FLoadMainReport("DailyTransactionSummary", DTTemp)


        CMain.FormulaSet(RptMain, "Daily Transaction Summary", FGMain)
        CMain.FShowReport(RptMain, Me.MdiParent, Me.Text)
    End Sub
    Private Sub FINI_MonthlyLedgerSummaryFull()

        FSetValue(0, "From Date", FGDataType.DT_Date, FilterCodeType.DTNone, AgL.PubStartDate)
        FSetValue(1, "To Date", FGDataType.DT_Date, FilterCodeType.DTNone, AgL.PubLoginDate)

        FSetValue(2, "Account Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, "All", False)
        FRH_Multiple(2) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable(
                          "Select 'o' As Tick,SG.SubCode,SG.Name,Sg.ManualCode,IfNull(CT.CityName,'') From SubGroup SG Left Join City CT On SG.CityCode=CT.CityCode where  (SG.SiteList Like '%|" & AgL.PubSiteCode & "|%' Or SG.SiteList Is Null)  Order By SG.Name",
                          AgL.GCn)), "", 600, 760, , , False)
        FRH_Multiple(2).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(2).FFormatColumn(1, , 0, , False)
        FRH_Multiple(2).FFormatColumn(2, "Name", 440, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple(2).FFormatColumn(3, "Code", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple(2).FFormatColumn(4, "City", 100, DataGridViewContentAlignment.MiddleLeft)


        FSetValue(3, "Site Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, AgL.PubSiteName & "|'" & AgL.PubSiteCode & "'", False)
        FRH_Multiple(3) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable(
                          "Select 'o' As Tick,Sm.Code,Sm.Name From Sitemast Sm where code in (" & AgL.PubSiteList & ")   Order By Sm.Name",
                          AgL.GCn)), "", 300, 360, , , False, AgL.PubSiteCode)
        FRH_Multiple(3).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(3).FFormatColumn(1, , 0, , False)
        FRH_Multiple(3).FFormatColumn(2, "Name", 240, DataGridViewContentAlignment.MiddleLeft)
    End Sub
    Private Sub FMonthlyLedgerSummaryFull()
        Dim StrCondition1 As String, StrConditionOP As String, StrConditionsite As String
        Dim DTTemp As DataTable
        Dim DblFirstYear As Double, DblSecondYear As Double

        If Not FIsValid(0) Then Exit Sub
        If Not FIsValid(1) Then Exit Sub

        DblFirstYear = Year(AgL.PubStartDate)
        DblSecondYear = Year(AgL.PubEndDate)

        StrCondition1 = " Where ( Date(LG.V_Date) Between " & AgL.Chk_Date(CDate(FGMain(GFilter, 0).Value.ToString).ToString("s")) & " And " & AgL.Chk_Date(CDate(FGMain(GFilter, 1).Value.ToString).ToString("s")) & ") "
        StrConditionOP = " Where Date(LG.V_Date) < " & AgL.Chk_Date(CDate(FGMain(GFilter, 0).Value.ToString).ToString("s")) & " "
        StrConditionsite = ""
        If Trim(FGMain(GFilterCode, 2).Value) <> "" Then StrCondition1 = StrCondition1 & " And LG.SubCode In (" & FGMain(GFilterCode, 2).Value & ")"
        If Trim(FGMain(GFilterCode, 2).Value) <> "" Then StrConditionOP = StrConditionOP & " And LG.SubCode In (" & FGMain(GFilterCode, 2).Value & ")"

        If Trim(FGMain(GFilterCode, 3).Value) <> "" Then
            StrConditionsite += " and LG.site_Code In (" & FGMain(GFilterCode, 3).Value & ") "
        Else
            StrConditionsite += " and LG.site_Code In  (" & AgL.PubSiteList & ") "
        End If
        StrSQLQuery = "Select Max(SName) As SName,IfNull(Sum(AmtDr),0) As AmtDr, IfNull(Sum(AmtCr),0) As AmtCr, "
        StrSQLQuery += "Max(Month) As Month,Max(Narration) As Narration,ID  "
        StrSQLQuery += "From "
        '======= For Opening Balance =========
        StrSQLQuery += "( Select IfNull(SG.SubCode,'') As SubCode, Max(IfNull(SG.Name,'')) As SName, "
        StrSQLQuery += "(Case When (IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LG.AmtCr),0))>0 Then  "
        StrSQLQuery += "(IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LG.AmtCr),0)) Else 0 End) As AmtDr, "
        StrSQLQuery += "(Case When (IfNull(Sum(LG.AmtCr),0)-IfNull(Sum(LG.AmtDr),0))>0 Then "
        StrSQLQuery += "(IfNull(Sum(LG.AmtCr),0)-IfNull(Sum(LG.AmtDr),0)) Else 0 End) As AmtCr, '' AS Month,'OPENING BALANCE' As Narration,0 AS ID,'' as MON,0 as yr  "
        StrSQLQuery += "From Ledger LG  "
        StrSQLQuery += "Left Join SubGroup SG On LG.SubCode=SG.SubCode "
        StrSQLQuery += StrConditionOP + StrConditionsite
        StrSQLQuery += "Group By IfNull(SG.SubCode,'') "
        '======= For Detail =========
        StrSQLQuery += "Union All "
        StrSQLQuery += "Select	IfNull(SG.SubCode,'') As SubCode, Max(IfNull(SG.Name,'')) As SName, "
        StrSQLQuery += "IfNull(Sum(LG.AmtDr),0) As AmtDr, "
        StrSQLQuery += "IfNull(Sum(LG.AmtCr),0) As AmtCr, "
        StrSQLQuery += "Max(" & FGetMonthNameQry("lg.V_Date") & "  "
        StrSQLQuery += " || ' ' || Cast((strftime('%Y', LG.V_date)) as Varchar)) AS Month,'' As Narration,1 AS ID,  "
        'StrSQLQuery += "Max(strftime('%m', LG.V_date) +' ' || (strftime('%Y', LG.V_date))) AS Month,'' As Narration,1 AS ID,  "
        StrSQLQuery += "max(" & FGetMonthNameQry("lg.V_Date") & ") AS MON,max(strftime('%Y', LG.V_date)) AS yr "
        'StrSQLQuery += "max(strftime('%m', LG.V_date)) AS MON,max(strftime('%Y', LG.V_date)) AS yr "
        StrSQLQuery += "From Ledger  "
        StrSQLQuery += "LG Left Join SubGroup SG On LG.SubCode=SG.SubCode "
        StrSQLQuery += StrCondition1 + StrConditionsite
        StrSQLQuery += "Group By IfNull(SG.SubCode,''),(strftime('%m', LG.V_date)  || ' ' || (strftime('%Y', LG.V_date))) "
        StrSQLQuery += " ) As Tmp "
        StrSQLQuery += "Group By SubCode,ID,MON having IfNull(SubCode,'')<>''  "
        StrSQLQuery += "Order By Max(SName),ID,MAX(Yr),MON "

        StrSQLQuery = AgL.GetBackendBasedQuery(StrSQLQuery)

        DTTemp = CMain.FGetDatTable(StrSQLQuery, AgL.GCn)

        If Not DTTemp.Rows.Count > 0 Then MsgBox("No Records Found to Print.") : Exit Sub
        FLoadMainReport("MonthlyLedgerSummaryFull", DTTemp)
        CMain.FormulaSet(RptMain, Me.Text, FGMain)
        CMain.FShowReport(RptMain, Me.MdiParent, Me.Text)
    End Sub
    Private Sub FINI_TrialDetailDrCr()
        FSetValue(0, "From Date", FGDataType.DT_Date, FilterCodeType.DTNone, AgL.PubStartDate)
        FSetValue(1, "To Date", FGDataType.DT_Date, FilterCodeType.DTNone, AgL.PubEndDate)

        FSetValue(2, "A/C Group", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, "All")
        FRH_Multiple(2) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable(
                         "Select Distinct  'o'  As Tick,S.GroupCode As Code,AG.GroupName AS Name From SubGroup S LEFT JOIN AcGroup AG ON AG.GroupCode=S.GroupCode Order By Name",
                         AgL.GCn)), "", 400, 430, , , False)
        FRH_Multiple(2).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, False)
        FRH_Multiple(2).FFormatColumn(1, , 0, , False)
        FRH_Multiple(2).FFormatColumn(2, "Name", 300, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(3, "Account Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, "All", False)
        FRH_Multiple(3) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable(
                          "Select 'o' As Tick,SG.SubCode,SG.Name,Sg.ManualCode,IfNull(CT.CityName,'') From SubGroup SG Left Join City CT On SG.CityCode=CT.CityCode where  (SG.SiteList Like '%|" & AgL.PubSiteCode & "|%' Or SG.SiteList Is Null)  Order By SG.Name",
                          AgL.GCn)), "", 600, 760, , , False)
        FRH_Multiple(3).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(3).FFormatColumn(1, , 0, , False)
        FRH_Multiple(3).FFormatColumn(2, "Name", 440, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple(3).FFormatColumn(3, "Code", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple(3).FFormatColumn(4, "City", 100, DataGridViewContentAlignment.MiddleLeft)


        FSetValue(4, "Site Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, AgL.PubSiteName & "|'" & AgL.PubSiteCode & "'", False)
        FRH_Multiple(4) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable(
                          "Select 'o' As Tick,Sm.Code,Sm.Name From Sitemast Sm where code in (" & AgL.PubSiteList & ")   Order By Sm.Name",
                          AgL.GCn)), "", 300, 360, , , False, AgL.PubSiteCode)
        FRH_Multiple(4).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(4).FFormatColumn(1, , 0, , False)
        FRH_Multiple(4).FFormatColumn(2, "Name", 240, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(5, "Division Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, AgL.PubDivName & "|'" & AgL.PubDivCode & "'")
        FRH_Multiple(5) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable(
                          "Select 'o' As Tick,Sm.Div_Code Code, Sg.DispName Name From Division Sm 
                           Left Join Subgroup Sg On SM.Subcode = Sg.Subcode 
                           where Sm.Div_code in (" & AgL.PubDivisionList & ")   
                          Order By Sg.DispName",
                          AgL.GCn)), "", 300, 360, , , False, AgL.PubSiteCode)
        FRH_Multiple(5).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(5).FFormatColumn(1, , 0, , False)
        FRH_Multiple(5).FFormatColumn(2, "Name", 240, DataGridViewContentAlignment.MiddleLeft)

        Dim StrSQL As String = ""
        StrSQL = "Select 'Y' as Code, 'Yes' as Name Union All Select 'N' as Code, 'No' as Name"
        FSetValue(6, "Show Zero Balance", FGDataType.DT_Selection_Single, FilterCodeType.DTNone, "Yes", False)
        FRH_Single(6) = New DMHelpGrid.FrmHelpGrid(New DataView(CMain.FGetDatTable(StrSQL, AgL.GCn)), "", 150, 180, , , False)
        FRH_Single(6).FFormatColumn(0, , 0, , False)
        FRH_Single(6).FFormatColumn(1, "Name", 100, DataGridViewContentAlignment.MiddleLeft)
    End Sub
    Private Sub FTrialDetailDrCr()
        Dim StrCondition1 As String, StrConditionOP As String, StrConditionsite As String
        Dim DTTemp As DataTable
        Dim DtStockValue As DataTable = Nothing

        If Not FIsValid(0) Then Exit Sub
        If Not FIsValid(1) Then Exit Sub

        FGetStockValuesInDataTable(DtStockValue, FGMain(GFilter, 0).Value)

        StrCondition1 = " Where ( Date(LG.V_Date) Between " & AgL.Chk_Date(CDate(FGMain(GFilter, 0).Value.ToString).ToString("s")) & " And " & AgL.Chk_Date(CDate(FGMain(GFilter, 1).Value.ToString).ToString("s")) & ") "
        'StrConditionOP = " Where Date(LG.V_Date) < " & AgL.Chk_Date(CDate(FGMain(GFilter, 0).Value.ToString).ToString("s")) & " "
        StrConditionOP = " Where Date(LG.V_Date) < (Case When Ag.GroupNature in ('R','E') Then '1900/Jan/01' Else " & AgL.Chk_Date(CDate(AgL.PubStartDate).ToString("s")) & " End) "
        StrConditionsite = ""

        If Trim(FGMain(GFilterCode, 2).Value) <> "" Then StrCondition1 = StrCondition1 & " And (IfNull(SG.GroupCode,'') In (Select IfNull(AGP.GroupCode,'') From AcGroupPath AGP Where AGP.GroupUnder In (" & FGMain(GFilterCode, 2).Value & ")) Or SG.GroupCode In (" & FGMain(GFilterCode, 2).Value & ")) "
        If Trim(FGMain(GFilterCode, 2).Value) <> "" Then StrConditionOP = StrConditionOP & " And (IfNull(SG.GroupCode,'') In (Select IfNull(AGP.GroupCode,'') From AcGroupPath AGP Where AGP.GroupUnder In (" & FGMain(GFilterCode, 2).Value & ")) Or SG.GroupCode In (" & FGMain(GFilterCode, 2).Value & ")) "

        If Trim(FGMain(GFilterCode, 3).Value) <> "" Then StrCondition1 = StrCondition1 & " And LG.SubCode In (" & FGMain(GFilterCode, 3).Value & ")"
        If Trim(FGMain(GFilterCode, 3).Value) <> "" Then StrConditionOP = StrConditionOP & " And LG.SubCode In (" & FGMain(GFilterCode, 3).Value & ")"

        If Trim(FGMain(GFilterCode, 4).Value) <> "" Then
            StrConditionsite += " and LG.site_Code In (" & FGMain(GFilterCode, 4).Value & ") "
        Else
            StrConditionsite += " and LG.site_Code In  (" & AgL.PubSiteList & ") "
        End If

        If Trim(FGMain(GFilterCode, 5).Value) <> "" Then
            StrConditionsite += " and LG.DivCode In (" & FGMain(GFilterCode, 5).Value & ") "
        Else
            StrConditionsite += " and LG.DivCode In  (" & AgL.PubDivisionList & ") "
        End If

        StrSQLQuery = "Select Max(GroupName) AS GroupName,Max(SName) As SName, IfNull(Sum(OPBalDr),0)*1.00 As OPBalDr, "
        StrSQLQuery += "IfNull(Sum(OPBalCr),0)*1.00 As OPBalCr,IfNull(Sum(AmtDr),0)*1.00 As AmtDr, IfNull(Sum(AmtCr),0)*1.00 As AmtCr "
        StrSQLQuery += "From "
        StrSQLQuery += "( Select Max(IfNull(AG.GroupName,'')) AS GroupName,IfNull(AG.GroupCode,'') As GroupCode, "
        StrSQLQuery += "IfNull(SG.SubCode,'') As SubCode, Max(IfNull(SG.Name,'')) As SName, "
        StrSQLQuery += "(Case When (IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LG.AmtCr),0))>0  Then  "
        StrSQLQuery += "(IfNull(Sum(Case When AG.GroupNature In ('A','L') Or ( Date(LG.V_Date)>=" & AgL.Chk_Text(AgL.PubStartDate) & " And Ag.GroupName <> 'Opening Stock') Then LG.AmtDr-LG.AmtCr "
        StrSQLQuery += "When Ag.GroupName = 'Opening Stock' And "
        If AgL.PubServerName = "" Then
            StrSQLQuery += "julianday(" & AgL.Chk_Date(AgL.PubStartDate) & ") - julianday(LG.V_Date) = 1 "
        Else
            StrSQLQuery += "DateDiff(DAY," & AgL.Chk_Date(AgL.PubStartDate) & ",LG.V_Date) = 1 "
        End If
        StrSQLQuery += "Then  LG.AmtDr-LG.AmtCr "
        StrSQLQuery += "Else 0 End),0)) Else 0 End) As OPBalDr, "
        StrSQLQuery += "(Case When (IfNull(Sum(LG.AmtCr),0)-IfNull(Sum(LG .AmtDr),0))>0  Then "
        StrSQLQuery += "(IfNull(Sum(Case When AG.GroupNature In ('A','L') Or ( Date(LG.V_Date)>=" & AgL.Chk_Text(AgL.PubStartDate) & " And Ag.GroupName <> 'Opening Stock') Then LG.AmtCr-LG.AmtDr "
        StrSQLQuery += "When Ag.GroupName = 'Opening Stock' And "
        If AgL.PubServerName = "" Then
            StrSQLQuery += "julianday(" & AgL.Chk_Date(AgL.PubStartDate) & ") - julianday(LG.V_Date) = 1 "
        Else
            StrSQLQuery += "DateDiff(DAY," & AgL.Chk_Date(AgL.PubStartDate) & ",LG.V_Date) = 1 "
        End If
        StrSQLQuery += "Then  LG.AmtCr-LG.AmtDr "
        StrSQLQuery += "Else 0 End),0)) Else 0 End) As OPBalCr,"
        StrSQLQuery += "0 As AmtDr,0 As AmtCr "
        StrSQLQuery += "From Ledger LG Left Join SubGroup SG On LG.SubCode=SG.SubCode "
        StrSQLQuery += "LEFT JOIN AcGroup AG On AG.GroupCode=SG.GroupCode "
        StrSQLQuery += StrConditionOP + StrConditionsite
        StrSQLQuery += "Group By IfNull(AG.GroupCode,''),IfNull(SG.SubCode,'') "
        StrSQLQuery += "Having(IfNull(Sum(LG.AmtDr), 0) - IfNull(Sum(LG.AmtCr), 0)) <> 0 "
        StrSQLQuery += "Union All "
        StrSQLQuery += "Select	Max(IfNull(AG.GroupName,'')) AS GroupName,IfNull(AG.GroupCode,'') As GroupCode, "
        StrSQLQuery += "IfNull(SG.SubCode,'') As SubCode, "
        StrSQLQuery += "Max(IfNull(SG.Name,'')) As SName, 0 As OPBalDr,0 As OPBalCr, "
        StrSQLQuery += "IfNull(Sum(LG.AmtDr),0) As AmtDr,  "
        StrSQLQuery += "IfNull(Sum(LG.AmtCr),0) As AmtCr "
        StrSQLQuery += "From Ledger LG Left Join SubGroup SG On LG.SubCode=SG.SubCode "
        StrSQLQuery += "LEFT JOIN AcGroup AG ON AG.GroupCode=SG.GroupCode "
        StrSQLQuery += StrCondition1 + StrConditionsite
        StrSQLQuery += "Group By IfNull(AG.GroupCode,''),IfNull(SG.SubCode,'') "
        If ClsMain.FDivisionNameForCustomization(12) = "SHRI PARWATI" Then
            StrSQLQuery += "Having (IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LG.AmtCr),0)) <> 0 "
        End If

        For J As Integer = 0 To DtStockValue.Rows.Count - 1
            If AgL.VNull(DtStockValue.Rows(J)("OpeningStockValue")) <> 0 Then
                StrSQLQuery += "Union All "
                StrSQLQuery += "Select	'Opening Stock' As GroupName, 'OSTOCK' AS GroupCode, "
                StrSQLQuery += "'OSTOCK' || '" & IIf(AgL.XNull(DtStockValue.Rows(J)("Remark")) <> "", " " + AgL.XNull(DtStockValue.Rows(J)("Remark")), "") & "' As SubCode, "
                StrSQLQuery += "'Opening Stock' || '" & IIf(AgL.XNull(DtStockValue.Rows(J)("Remark")) <> "", " " + AgL.XNull(DtStockValue.Rows(J)("Remark")), "") & "' As SName, "
                StrSQLQuery += AgL.VNull(DtStockValue.Rows(J)("OpeningStockValue")) & " As OPBalDr,"
                StrSQLQuery += " 0 As OPBalCr, "
                StrSQLQuery += " 0 As AmtDr,  "
                StrSQLQuery += " 0 As AmtCr "
            End If
        Next

        'If mOpeningStockValue > 0 Then
        '    StrSQLQuery += "Union All "
        '    StrSQLQuery += "Select	'Opening Stock' As GroupName, 'OSTOCK' AS GroupCode, "
        '    StrSQLQuery += "'OSTOCK' As SubCode, "
        '    StrSQLQuery += "'Opening Stock' As SName, "
        '    StrSQLQuery += mOpeningStockValue & " As OPBalDr,"
        '    StrSQLQuery += " 0 As OPBalCr, "
        '    StrSQLQuery += " 0 As AmtDr,  "
        '    StrSQLQuery += " 0 As AmtCr "
        'End If

        StrSQLQuery += ") As Tmp "
        StrSQLQuery += "Group By GroupCode,SubCode "


        If Trim(FGMain(GFilter, 6).Value) = "No" Then
            StrSQLQuery += " Having Round(IfNull(Sum(OPBalDr),0) + IfNull(Sum(AmtDr),0)
             - IfNull(Sum(OPBalCr), 0) - IfNull(Sum(AmtCr),0),2) <> 0 "
        End If

        StrSQLQuery += "Order By Max(GroupName),Max(SName) "

        StrSQLQuery = AgL.GetBackendBasedQuery(StrSQLQuery)
        DTTemp = CMain.FGetDatTable(StrSQLQuery, AgL.GCn)

        If Not DTTemp.Rows.Count > 0 Then MsgBox("No Records Found To Print.") : Exit Sub

        FLoadMainReport("TrailDetailDrCr", DTTemp)
        CMain.FormulaSet(RptMain, Me.Text, FGMain)
        CMain.FShowReport(RptMain, Me.MdiParent, Me.Text)
    End Sub
    Private Sub FINI_MonthlyLedgerSummary()
        Dim DTTemp As DataTable
        Dim StrReportForCode As String = ""

        DTTemp = CMain.FGetDatTable("Select GroupCode FROM AcGroup WHERE GroupName='" & StrReportFor & "'", AgL.GCn)
        If DTTemp.Rows.Count > 0 Then StrReportForCode = AgL.XNull(DTTemp.Rows(0).Item("GroupCode")) : DTTemp.Rows.Clear()
        DTTemp.Dispose()
        FSetValue(0, "Month", FGDataType.DT_Selection_Single, FilterCodeType.DTNone, "Last Six Month")
        DTTemp = New DataTable
        DTTemp.Columns.Add("Code", System.Type.GetType("System.String"))
        DTTemp.Columns.Add("Name", System.Type.GetType("System.String"))
        DTTemp.Rows.Add(New Object() {"F", "First Six Month"})
        DTTemp.Rows.Add(New Object() {"L", "Last Six Month"})

        FRH_Single(0) = New DMHelpGrid.FrmHelpGrid(New DataView(DTTemp), "", 150, 200, , , False)
        FRH_Single(0).FFormatColumn(0, , 0, , False)
        FRH_Single(0).FFormatColumn(1, "Name", 130, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(1, "A/C Group", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, "All")
        FRH_Multiple(1) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable(
                         "Select Distinct  'o'  As Tick,S.GroupCode As Code,AG.GroupName AS Name From SubGroup S LEFT JOIN AcGroup AG ON AG.GroupCode=S.GroupCode Order By Name",
                         AgL.GCn)), "", 400, 430, , , False)
        FRH_Multiple(1).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(1).FFormatColumn(1, , 0, , False)
        FRH_Multiple(1).FFormatColumn(2, "Name", 300, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(2, "Site Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, AgL.PubSiteName & "|'" & AgL.PubSiteCode & "'")
        FRH_Multiple(2) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable(
                          "Select 'o' As Tick,Sm.Code,Sm.Name From Sitemast Sm where code in (" & AgL.PubSiteList & ")   Order By Sm.Name",
                          AgL.GCn)), "", 300, 360, , , False, AgL.PubSiteCode)
        FRH_Multiple(2).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(2).FFormatColumn(1, , 0, , False)
        FRH_Multiple(2).FFormatColumn(2, "Name", 240, DataGridViewContentAlignment.MiddleLeft)

    End Sub
    Private Sub FMonthlyLedgerSummary()
        Dim StrCondition1 As String = ""
        Dim TempField As String
        Dim DTTemp As DataTable
        Dim DblFirstYear As Double, DblSecondYear As Double

        If Not FIsValid(0) Then Exit Sub


        If AgL.PubServerName = "" Then

            If Trim(FGMain(GFilterCode, 0).Value) = "F" Then
                TempField = "0 As Sel "
                DblFirstYear = Year(AgL.PubStartDate)
                DblSecondYear = Year(AgL.PubStartDate)
                StrCondition1 += "Where Cast(strftime('%m', LG.V_date) as Int) In (4,5,6,7,8,9) And Cast(strftime('%Y', LG.V_date) as Int) In (" & DblFirstYear & ") "
            Else
                TempField = "1 As Sel "
                DblFirstYear = Year(AgL.PubStartDate)
                DblSecondYear = Year(AgL.PubEndDate)
                StrCondition1 += "Where Cast(strftime('%m', LG.V_date) as Int) In (10,11,12,1,2,3) And Cast(strftime('%Y', LG.V_date) as Int) In (" & DblFirstYear & "," & DblSecondYear & ") "
            End If
            If Trim(FGMain(GFilterCode, 2).Value) <> "" Then
                StrCondition1 = StrCondition1 & " And LG.Site_Code IN (" & FGMain(GFilterCode, 2).Value & ") "
            Else
                StrCondition1 = StrCondition1 & " And LG.Site_Code IN (" & AgL.PubSiteList & ") "
            End If

            If Trim(FGMain(GFilterCode, 1).Value) <> "" Then StrCondition1 = StrCondition1 & " And (IfNull(SG.GroupCode,'') In (Select IfNull(AGP.GroupCode,'') From AcGroupPath AGP Where AGP.GroupUnder In (" & FGMain(GFilterCode, 1).Value & ")) Or SG.GroupCode In (" & FGMain(GFilterCode, 1).Value & ")) "

            StrSQLQuery = "Select GroupName,PName,SubCode,"
            StrSQLQuery += "(Case When (IfNull(DR_1,0)-IfNull(CR_1,0))>0 Then (IfNull(DR_1,0)-IfNull(CR_1,0)) Else 0 End) As DR_1, "
            StrSQLQuery += "(Case When (IfNull(CR_1,0)-IfNull(DR_1,0))>0 Then (IfNull(CR_1,0)-IfNull(DR_1,0)) Else 0 End) As CR_1, "
            StrSQLQuery += "(Case When (IfNull(DR_2,0)-IfNull(CR_2,0))>0 Then (IfNull(DR_2,0)-IfNull(CR_2,0)) Else 0 End) As DR_2, "
            StrSQLQuery += "(Case When (IfNull(CR_2,0)-IfNull(DR_2,0))>0 Then (IfNull(CR_2,0)-IfNull(DR_2,0)) Else 0 End) As CR_2, "
            StrSQLQuery += "(Case When (IfNull(DR_3,0)-IfNull(CR_3,0))>0 Then (IfNull(DR_3,0)-IfNull(CR_3,0)) Else 0 End) As DR_3, "
            StrSQLQuery += "(Case When (IfNull(CR_3,0)-IfNull(DR_3,0))>0 Then (IfNull(CR_3,0)-IfNull(DR_3,0)) Else 0 End) As CR_3, "
            StrSQLQuery += "(Case When (IfNull(DR_4,0)-IfNull(CR_4,0))>0 Then (IfNull(DR_4,0)-IfNull(CR_4,0)) Else 0 End) As DR_4, "
            StrSQLQuery += "(Case When (IfNull(CR_4,0)-IfNull(DR_4,0))>0 Then (IfNull(CR_4,0)-IfNull(DR_4,0)) Else 0 End) As CR_4, "
            StrSQLQuery += "(Case When (IfNull(DR_5,0)-IfNull(CR_5,0))>0 Then (IfNull(DR_5,0)-IfNull(CR_5,0)) Else 0 End) As DR_5, "
            StrSQLQuery += "(Case When (IfNull(CR_5,0)-IfNull(DR_5,0))>0 Then (IfNull(CR_5,0)-IfNull(DR_5,0)) Else 0 End) As CR_5, "
            StrSQLQuery += "(Case When (IfNull(DR_6,0)-IfNull(CR_6,0))>0 Then (IfNull(DR_6,0)-IfNull(CR_6,0)) Else 0 End) As DR_6, "
            StrSQLQuery += "(Case When (IfNull(CR_6,0)-IfNull(DR_6,0))>0 Then (IfNull(CR_6,0)-IfNull(DR_6,0)) Else 0 End) As CR_6, "
            StrSQLQuery += "Sel "
            StrSQLQuery += "From ( "
            StrSQLQuery += "Select	AG.GroupName,Max(SG.Name) As PName,LG.SubCode, "
            StrSQLQuery += "Sum(Case When (Cast(strftime('%m', LG.V_date) as Int)=4 Or Cast(strftime('%m', LG.V_date) as Int)=10) And Cast(strftime('%Y', LG.V_date) as Int)=" & DblFirstYear & " Then LG.AmtDr Else 0 End) As DR_1, "
            StrSQLQuery += "Sum(Case When (Cast(strftime('%m', LG.V_date) as Int)=4 Or Cast(strftime('%m', LG.V_date) as Int)=10) And Cast(strftime('%Y', LG.V_date) as Int)=" & DblFirstYear & " Then LG.AmtCr Else 0 End) As CR_1, "
            StrSQLQuery += "Sum(Case When (Cast(strftime('%m', LG.V_date) as Int)=5 Or Cast(strftime('%m', LG.V_date) as Int)=11) And Cast(strftime('%Y', LG.V_date) as Int)=" & DblFirstYear & " Then LG.AmtDr Else 0 End) As DR_2, "
            StrSQLQuery += "Sum(Case When (Cast(strftime('%m', LG.V_date) as Int)=5 Or Cast(strftime('%m', LG.V_date) as Int)=11) And Cast(strftime('%Y', LG.V_date) as Int)=" & DblFirstYear & " Then LG.AmtCr Else 0 End) As CR_2, "
            StrSQLQuery += "Sum(Case When (Cast(strftime('%m', LG.V_date) as Int)=6 Or Cast(strftime('%m', LG.V_date) as Int)=12) And Cast(strftime('%Y', LG.V_date) as Int)=" & DblFirstYear & " Then LG.AmtDr Else 0 End) As DR_3, "
            StrSQLQuery += "Sum(Case When (Cast(strftime('%m', LG.V_date) as Int)=6 Or Cast(strftime('%m', LG.V_date) as Int)=12) And Cast(strftime('%Y', LG.V_date) as Int)=" & DblFirstYear & " Then LG.AmtCr Else 0 End) As CR_3, "
            StrSQLQuery += "Sum(Case When (Cast(strftime('%m', LG.V_date) as Int)=7 Or Cast(strftime('%m', LG.V_date) as Int)=1) And Cast(strftime('%Y', LG.V_date) as Int)=" & DblSecondYear & " Then LG.AmtDr Else 0 End) As DR_4, "
            StrSQLQuery += "Sum(Case When (Cast(strftime('%m', LG.V_date) as Int)=7 Or Cast(strftime('%m', LG.V_date) as Int)=1) And Cast(strftime('%Y', LG.V_date) as Int)=" & DblSecondYear & " Then LG.AmtCr Else 0 End) As CR_4, "
            StrSQLQuery += "Sum(Case When (Cast(strftime('%m', LG.V_date) as Int)=8 Or Cast(strftime('%m', LG.V_date) as Int)=2) And Cast(strftime('%Y', LG.V_date) as Int)=" & DblSecondYear & " Then LG.AmtDr Else 0 End) As DR_5, "
            StrSQLQuery += "Sum(Case When (Cast(strftime('%m', LG.V_date) as Int)=8 Or Cast(strftime('%m', LG.V_date) as Int)=2) And Cast(strftime('%Y', LG.V_date) as Int)=" & DblSecondYear & " Then LG.AmtCr Else 0 End) As CR_5, "
            StrSQLQuery += "Sum(Case When (Cast(strftime('%m', LG.V_date) as Int)=9 Or Cast(strftime('%m', LG.V_date) as Int)=3) And Cast(strftime('%Y', LG.V_date) as Int)=" & DblSecondYear & " Then LG.AmtDr Else 0 End) As DR_6, "
            StrSQLQuery += "Sum(Case When (Cast(strftime('%m', LG.V_date) as Int)=9 Or Cast(strftime('%m', LG.V_date) as Int)=3) And Cast(strftime('%Y', LG.V_date) as Int)=" & DblSecondYear & " Then LG.AmtCr Else 0 End) As CR_6, "
            StrSQLQuery += TempField
            StrSQLQuery += "From Ledger LG Left Join "
            StrSQLQuery += "SubGroup SG ON LG.SubCode=SG.SubCode Left Join "
            StrSQLQuery += "AcGroup AG ON AG.GroupCode=SG.GroupCode "
            StrSQLQuery += StrCondition1
            StrSQLQuery += "Group By AG.GroupName,LG.SubCode "
            StrSQLQuery += ") As Tmp "
            StrSQLQuery += "Order By GroupName,PName "
        Else
            If Trim(FGMain(GFilterCode, 0).Value) = "F" Then
                TempField = "0 As Sel "
                DblFirstYear = Year(AgL.PubStartDate)
                DblSecondYear = Year(AgL.PubStartDate)
                StrCondition1 += "Where Cast(strftime('%m', LG.V_date) as Int) In (4,5,6,7,8,9) And Cast(strftime('%Y', LG.V_date) as Int) In (" & DblFirstYear & ") "
            Else
                TempField = "1 As Sel "
                DblFirstYear = Year(AgL.PubStartDate)
                DblSecondYear = Year(AgL.PubEndDate)
                StrCondition1 += "Where Cast(strftime('%m', LG.V_date) as Int) In (10,11,12,1,2,3) And Cast(strftime('%Y', LG.V_date) as Int) In (" & DblFirstYear & "," & DblSecondYear & ") "
            End If
            If Trim(FGMain(GFilterCode, 2).Value) <> "" Then
                StrCondition1 = StrCondition1 & " And LG.Site_Code IN (" & FGMain(GFilterCode, 2).Value & ") "
            Else
                StrCondition1 = StrCondition1 & " And LG.Site_Code IN (" & AgL.PubSiteList & ") "
            End If

            If Trim(FGMain(GFilterCode, 1).Value) <> "" Then StrCondition1 = StrCondition1 & " And (IfNull(SG.GroupCode,'') In (Select IfNull(AGP.GroupCode,'') From AcGroupPath AGP Where AGP.GroupUnder In (" & FGMain(GFilterCode, 1).Value & ")) Or SG.GroupCode In (" & FGMain(GFilterCode, 1).Value & ")) "

            StrSQLQuery = "Select GroupName,PName,SubCode,"
            StrSQLQuery += "(Case When (IfNull(DR_1,0)-IfNull(CR_1,0))>0 Then (IfNull(DR_1,0)-IfNull(CR_1,0)) Else 0 End) As DR_1, "
            StrSQLQuery += "(Case When (IfNull(CR_1,0)-IfNull(DR_1,0))>0 Then (IfNull(CR_1,0)-IfNull(DR_1,0)) Else 0 End) As CR_1, "
            StrSQLQuery += "(Case When (IfNull(DR_2,0)-IfNull(CR_2,0))>0 Then (IfNull(DR_2,0)-IfNull(CR_2,0)) Else 0 End) As DR_2, "
            StrSQLQuery += "(Case When (IfNull(CR_2,0)-IfNull(DR_2,0))>0 Then (IfNull(CR_2,0)-IfNull(DR_2,0)) Else 0 End) As CR_2, "
            StrSQLQuery += "(Case When (IfNull(DR_3,0)-IfNull(CR_3,0))>0 Then (IfNull(DR_3,0)-IfNull(CR_3,0)) Else 0 End) As DR_3, "
            StrSQLQuery += "(Case When (IfNull(CR_3,0)-IfNull(DR_3,0))>0 Then (IfNull(CR_3,0)-IfNull(DR_3,0)) Else 0 End) As CR_3, "
            StrSQLQuery += "(Case When (IfNull(DR_4,0)-IfNull(CR_4,0))>0 Then (IfNull(DR_4,0)-IfNull(CR_4,0)) Else 0 End) As DR_4, "
            StrSQLQuery += "(Case When (IfNull(CR_4,0)-IfNull(DR_4,0))>0 Then (IfNull(CR_4,0)-IfNull(DR_4,0)) Else 0 End) As CR_4, "
            StrSQLQuery += "(Case When (IfNull(DR_5,0)-IfNull(CR_5,0))>0 Then (IfNull(DR_5,0)-IfNull(CR_5,0)) Else 0 End) As DR_5, "
            StrSQLQuery += "(Case When (IfNull(CR_5,0)-IfNull(DR_5,0))>0 Then (IfNull(CR_5,0)-IfNull(DR_5,0)) Else 0 End) As CR_5, "
            StrSQLQuery += "(Case When (IfNull(DR_6,0)-IfNull(CR_6,0))>0 Then (IfNull(DR_6,0)-IfNull(CR_6,0)) Else 0 End) As DR_6, "
            StrSQLQuery += "(Case When (IfNull(CR_6,0)-IfNull(DR_6,0))>0 Then (IfNull(CR_6,0)-IfNull(DR_6,0)) Else 0 End) As CR_6, "
            StrSQLQuery += "Sel "
            StrSQLQuery += "From ( "
            StrSQLQuery += "Select	AG.GroupName,Max(SG.Name) As PName,LG.SubCode, "
            StrSQLQuery += "Sum(Case When (Cast(strftime('%m', LG.V_date) as Int)=4 Or Cast(strftime('%m', LG.V_date) as Int)=10) And Cast(strftime('%Y', LG.V_date) as Int)=" & DblFirstYear & " Then LG.AmtDr Else 0 End) As DR_1, "
            StrSQLQuery += "Sum(Case When (Cast(strftime('%m', LG.V_date) as Int)=4 Or Cast(strftime('%m', LG.V_date) as Int)=10) And Cast(strftime('%Y', LG.V_date) as Int)=" & DblFirstYear & " Then LG.AmtCr Else 0 End) As CR_1, "
            StrSQLQuery += "Sum(Case When (Cast(strftime('%m', LG.V_date) as Int)=5 Or Cast(strftime('%m', LG.V_date) as Int)=11) And Cast(strftime('%Y', LG.V_date) as Int)=" & DblFirstYear & " Then LG.AmtDr Else 0 End) As DR_2, "
            StrSQLQuery += "Sum(Case When (Cast(strftime('%m', LG.V_date) as Int)=5 Or Cast(strftime('%m', LG.V_date) as Int)=11) And Cast(strftime('%Y', LG.V_date) as Int)=" & DblFirstYear & " Then LG.AmtCr Else 0 End) As CR_2, "
            StrSQLQuery += "Sum(Case When (Cast(strftime('%m', LG.V_date) as Int)=6 Or Cast(strftime('%m', LG.V_date) as Int)=12) And Cast(strftime('%Y', LG.V_date) as Int)=" & DblFirstYear & " Then LG.AmtDr Else 0 End) As DR_3, "
            StrSQLQuery += "Sum(Case When (Cast(strftime('%m', LG.V_date) as Int)=6 Or Cast(strftime('%m', LG.V_date) as Int)=12) And Cast(strftime('%Y', LG.V_date) as Int)=" & DblFirstYear & " Then LG.AmtCr Else 0 End) As CR_3, "
            StrSQLQuery += "Sum(Case When (Cast(strftime('%m', LG.V_date) as Int)=7 Or Cast(strftime('%m', LG.V_date) as Int)=1) And Cast(strftime('%Y', LG.V_date) as Int)=" & DblSecondYear & " Then LG.AmtDr Else 0 End) As DR_4, "
            StrSQLQuery += "Sum(Case When (Cast(strftime('%m', LG.V_date) as Int)=7 Or Cast(strftime('%m', LG.V_date) as Int)=1) And Cast(strftime('%Y', LG.V_date) as Int)=" & DblSecondYear & " Then LG.AmtCr Else 0 End) As CR_4, "
            StrSQLQuery += "Sum(Case When (Cast(strftime('%m', LG.V_date) as Int)=8 Or Cast(strftime('%m', LG.V_date) as Int)=2) And Cast(strftime('%Y', LG.V_date) as Int)=" & DblSecondYear & " Then LG.AmtDr Else 0 End) As DR_5, "
            StrSQLQuery += "Sum(Case When (Cast(strftime('%m', LG.V_date) as Int)=8 Or Cast(strftime('%m', LG.V_date) as Int)=2) And Cast(strftime('%Y', LG.V_date) as Int)=" & DblSecondYear & " Then LG.AmtCr Else 0 End) As CR_5, "
            StrSQLQuery += "Sum(Case When (Cast(strftime('%m', LG.V_date) as Int)=9 Or Cast(strftime('%m', LG.V_date) as Int)=3) And Cast(strftime('%Y', LG.V_date) as Int)=" & DblSecondYear & " Then LG.AmtDr Else 0 End) As DR_6, "
            StrSQLQuery += "Sum(Case When (Cast(strftime('%m', LG.V_date) as Int)=9 Or Cast(strftime('%m', LG.V_date) as Int)=3) And Cast(strftime('%Y', LG.V_date) as Int)=" & DblSecondYear & " Then LG.AmtCr Else 0 End) As CR_6, "
            StrSQLQuery += TempField
            StrSQLQuery += "From Ledger LG Left Join "
            StrSQLQuery += "SubGroup SG ON LG.SubCode=SG.SubCode Left Join "
            StrSQLQuery += "AcGroup AG ON AG.GroupCode=SG.GroupCode "
            StrSQLQuery += StrCondition1
            StrSQLQuery += "Group By AG.GroupName,LG.SubCode "
            StrSQLQuery += ") As Tmp "
            StrSQLQuery += "Order By GroupName,PName "

        End If
        StrSQLQuery = AgL.GetBackendBasedQuery(StrSQLQuery)
        DTTemp = CMain.FGetDatTable(StrSQLQuery, AgL.GCn)

        If Not DTTemp.Rows.Count > 0 Then MsgBox("No Records Found to Print.") : Exit Sub
        FLoadMainReport("MonthlyLedgerSummary", DTTemp)
        CMain.FormulaSet(RptMain, Me.Text, FGMain)
        CMain.FShowReport(RptMain, Me.MdiParent, Me.Text)
    End Sub
    Private Sub FINI_InterestLedger()
        Dim DTTemp As DataTable
        Dim StrReportForCode As String = ""

        DTTemp = CMain.FGetDatTable("SELECT GroupCode FROM AcGroup WHERE GroupName='" & StrReportFor & "'", AgL.GCn)
        If DTTemp.Rows.Count > 0 Then StrReportForCode = AgL.XNull(DTTemp.Rows(0).Item("GroupCode")) : DTTemp.Rows.Clear()
        DTTemp.Dispose()

        FSetValue(0, "From Date", FGDataType.DT_Date, FilterCodeType.DTNone, AgL.PubStartDate)
        FSetValue(1, "To Date", FGDataType.DT_Date, FilterCodeType.DTNone, AgL.PubLoginDate)
        FSetValue(2, "Interest Rate (Dr.)", FGDataType.DT_Numeric, FilterCodeType.DTNone, "1")
        FSetValue(3, "Interest Rate (Cr)", FGDataType.DT_Numeric, FilterCodeType.DTNone, "1")
        FSetValue(4, "Days", FGDataType.DT_Numeric, FilterCodeType.DTNone, "365")



        FSetValue(5, "Account Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, "All")
        FRH_Multiple(5) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable("Select 'o' As Tick,SG.SubCode,SG.Name,IfNull(CT.CityName,''),AG.GroupName From SubGroup  SG Left Join  " &
                          "AcGroup AG On AG.GroupCode=SG.GroupCode " &
                          "Left Join City CT On SG.CityCode=CT.CityCode " &
                          "Where  (SG.SiteList Like '%|" & AgL.PubSiteCode & "|%' Or SG.SiteList Is Null) " &
                          "Order By SG.Name", AgL.GCn)), "", 600, 960, , , False)
        FRH_Multiple(5).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(5).FFormatColumn(1, , 0, , False)
        FRH_Multiple(5).FFormatColumn(2, "Name", 440, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple(5).FFormatColumn(3, "City", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple(5).FFormatColumn(4, "Group Name", 200, DataGridViewContentAlignment.MiddleLeft)


        'FSetValue(5, "Account Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, "All")
        'FRH_Multiple(5) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable(
        '                  "Select 'o' As Tick,SG.SubCode,SG.Name,Sg.ManualCode From SubGroup SG Where  (SG.SiteList Like '%|" & AgL.PubSiteCode & "|%' Or SG.SiteList Is Null)  Order By SG.Name",
        '                  AgL.GCn)), "", 600, 660, , , False)
        'FRH_Multiple(5).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        'FRH_Multiple(5).FFormatColumn(1, , 0, , False)
        'FRH_Multiple(5).FFormatColumn(2, "Name", 440, DataGridViewContentAlignment.MiddleLeft)
        'FRH_Multiple(5).FFormatColumn(3, "Code", 100, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(6, "Site Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, AgL.PubSiteName & "|'" & AgL.PubSiteCode & "'", False)
        FRH_Multiple(6) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable(
                          "Select 'o' As Tick,Sm.Code,Sm.Name From Sitemast Sm where code in (" & AgL.PubSiteList & ")   Order By Sm.Name",
                          AgL.GCn)), "", 300, 360, , , False, AgL.PubSiteCode)
        FRH_Multiple(6).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(6).FFormatColumn(1, , 0, , False)
        FRH_Multiple(6).FFormatColumn(2, "Name", 240, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(7, "Division", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, AgL.PubDivName & "|'" & AgL.PubDivCode & "'")
        FRH_Multiple(7) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable(
                          "Select 'o' As Tick,H.Div_Code Code,H.Div_Name Name From Division H where Div_code in (" & AgL.PubDivisionList & ")   Order By H.Div_Name",
                          AgL.GCn)), "", 300, 360, , , False, AgL.PubSiteCode)
        FRH_Multiple(7).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(7).FFormatColumn(1, , 0, , False)
        FRH_Multiple(7).FFormatColumn(2, "Name", 240, DataGridViewContentAlignment.MiddleLeft)
    End Sub
    Private Sub FInterestLedger()
        Dim StrCondition As String, StrField As String
        Dim strConditionOp As String, StrSQLQueryOp As String
        Dim DTTemp As DataTable

        If Not FIsValid(0) Then Exit Sub
        If Not FIsValid(1) Then Exit Sub
        If Not FIsValid(2) Then Exit Sub
        If Not FIsValid(3) Then Exit Sub
        If Not FIsValid(3) Then Exit Sub


        strConditionOp = " Where Date(L.V_Date) < " & AgL.Chk_Date(CDate(FGMain(GFilter, 0).Value.ToString).ToString("s")) & "  "
        StrField = "," & Val(FGMain(GFilter, 2).Value) & " as IntrateDr"
        StrField += "," & Val(FGMain(GFilter, 3).Value) & " as IntrateCr"
        StrField += "," & Val(FGMain(GFilter, 4).Value) & " as Days"
        StrField += ",'" & Trim(FGMain(GFilter, 1).Value) & "' as ToDate"
        If Trim(FGMain(GFilterCode, 5).Value) <> "" Then strConditionOp += " And SG.Subcode In (" & FGMain(GFilterCode, 5).Value & ")"

        If Trim(FGMain(GFilterCode, 6).Value) <> "" Then
            strConditionOp += " and L.site_Code In (" & FGMain(GFilterCode, 6).Value & ") "
        Else
            strConditionOp += " and L.site_Code In  (" & AgL.PubSiteList & ") "
        End If

        If Trim(FGMain(GFilterCode, 7).Value) <> "" Then
            strConditionOp += " and L.DivCode In (" & FGMain(GFilterCode, 7).Value & ") "
        Else
            strConditionOp += " and L.DivCode In  (" & AgL.PubDivisionList & ") "
        End If

        StrSQLQueryOp = "SELECT Max(SG.Name) AS Party, Date(" & AgL.Chk_Date(CDate(FGMain(GFilter, 0).Value.ToString).ToString("s")) & ") AS VDate,' OP' as V_type, 0 AS DRAmt,"
        StrSQLQueryOp += "0 AS CRAmt, sum(amtdr)-sum(amtcr) AS Bal" + StrField
        StrSQLQueryOp += " FROM Ledger L"
        StrSQLQueryOp += " LEFT JOIN SubGroup SG ON SG.SubCode=L.SubCode "
        StrSQLQueryOp += strConditionOp
        StrSQLQueryOp += " GROUP BY L.SubCode"



        StrField = ""
        StrCondition = " Where ( Date(L.V_Date) Between " & AgL.Chk_Date(CDate(FGMain(GFilter, 0).Value.ToString).ToString("s")) & " And " & AgL.Chk_Date(CDate(FGMain(GFilter, 1).Value.ToString).ToString("s")) & ") "

        StrField = "," & Val(FGMain(GFilter, 2).Value) & " as IntrateDr"
        StrField += "," & Val(FGMain(GFilter, 3).Value) & " as IntrateCr"
        StrField += "," & Val(FGMain(GFilter, 4).Value) & " as Days"
        StrField += ",'" & Trim(FGMain(GFilter, 1).Value) & "' as ToDate"
        If Trim(FGMain(GFilterCode, 5).Value) <> "" Then StrCondition += " And SG.Subcode In (" & FGMain(GFilterCode, 5).Value & ")"

        If Trim(FGMain(GFilterCode, 6).Value) <> "" Then
            StrCondition += " and L.site_Code In (" & FGMain(GFilterCode, 6).Value & ") "
        Else
            StrCondition += " and L.site_Code In  (" & AgL.PubSiteList & ") "
        End If

        If Trim(FGMain(GFilterCode, 7).Value) <> "" Then
            StrCondition += " and L.DivCode In (" & FGMain(GFilterCode, 7).Value & ") "
        Else
            StrCondition += " and L.DivCode In  (" & AgL.PubDivisionList & ") "
        End If

        StrSQLQuery = "SELECT Max(SG.Name) AS Party,(L.V_Date) AS VDate,max(L.V_type) as V_type, sum(amtdr) AS DRAmt,"
        StrSQLQuery += "sum(amtcr) AS CRAmt, sum(amtdr)-sum(amtcr) AS Bal" + StrField
        StrSQLQuery += " FROM Ledger L"
        StrSQLQuery += " LEFT JOIN SubGroup SG ON SG.SubCode=L.SubCode "
        StrSQLQuery += StrCondition
        StrSQLQuery += " GROUP BY L.SubCode,L.V_Date,L.V_No ORDER BY Party"

        StrSQLQuery = StrSQLQueryOp & " Union All " & StrSQLQuery

        StrSQLQuery = AgL.GetBackendBasedQuery(StrSQLQuery)
        DTTemp = CMain.FGetDatTable(StrSQLQuery, AgL.GCn)

        Dim clonedDT As DataTable = DTTemp.Clone()
        clonedDT.Columns("VDate").DataType = GetType(Date)
        For Each row As DataRow In DTTemp.Rows
            clonedDT.ImportRow(row)
        Next
        DTTemp = clonedDT


        If Not DTTemp.Rows.Count > 0 Then MsgBox("No Records Found to Print.") : Exit Sub

        FLoadMainReport("InterestLedger", DTTemp)
        CMain.FormulaSet(RptMain, Me.Text, FGMain)
        CMain.FShowReport(RptMain, Me.MdiParent, Me.Text)
    End Sub
    Private Sub FINI_FBTReport()
        Dim DTTemp As DataTable
        Dim StrReportForCode As String = ""
        Dim Strsql As String
        DTTemp = CMain.FGetDatTable("SELECT GroupCode FROM AcGroup WHERE GroupName='" & StrReportFor & "'", AgL.GCn)
        If DTTemp.Rows.Count > 0 Then StrReportForCode = AgL.XNull(DTTemp.Rows(0).Item("GroupCode")) : DTTemp.Rows.Clear()
        DTTemp.Dispose()

        FSetValue(0, "From Date", FGDataType.DT_Date, FilterCodeType.DTNone, AgL.PubStartDate)
        FSetValue(1, "To Date", FGDataType.DT_Date, FilterCodeType.DTNone, AgL.PubLoginDate)
        FSetValue(2, "Account Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, "All")
        FRH_Multiple(2) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable("Select 'o' As Tick,SG.SubCode,SG.Name,SG.ManualCode From SubGroup  SG " &
                          "Where Nature='Expenses' and  (SG.SiteList Like '%|" & AgL.PubSiteCode & "|%' Or SG.SiteList Is Null) " &
                          "Order By SG.Name", AgL.GCn)), "", 600, 660, , , False)
        FRH_Multiple(2).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(2).FFormatColumn(1, , 0, , False)
        FRH_Multiple(2).FFormatColumn(2, "Name", 440, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple(2).FFormatColumn(3, "Code", 100, DataGridViewContentAlignment.MiddleLeft)

        Strsql = "Select 'Y' as Code, 'Yes' as Name Union All Select 'N' as Code, 'No' as Name"

        FSetValue(3, "With Opening", FGDataType.DT_Selection_Single, FilterCodeType.DTNone, "Yes")
        FRH_Single(3) = New DMHelpGrid.FrmHelpGrid(New DataView(CMain.FGetDatTable(Strsql, AgL.GCn)), "", 150, 180, , , False)
        FRH_Single(3).FFormatColumn(0, , 0, , False)
        FRH_Single(3).FFormatColumn(1, "Name", 100, DataGridViewContentAlignment.MiddleLeft)


        FSetValue(4, "Site Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, AgL.PubSiteName & "|'" & AgL.PubSiteCode & "'")
        FRH_Multiple(4) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable(
                          "Select 'o' As Tick,Sm.Code,Sm.Name From Sitemast Sm where code in (" & AgL.PubSiteList & ")   Order By Sm.Name",
                          AgL.GCn)), "", 300, 360, , , False, AgL.PubSiteCode)
        FRH_Multiple(4).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(4).FFormatColumn(1, , 0, , False)
        FRH_Multiple(4).FFormatColumn(2, "Name", 240, DataGridViewContentAlignment.MiddleLeft)
    End Sub
    Private Sub FINI_PartyWiseTDSReport()
        Dim DTTemp As DataTable
        Dim StrReportForCode As String = ""
        Dim Strsql As String

        DTTemp = CMain.FGetDatTable("SELECT GroupCode FROM AcGroup WHERE GroupName='" & StrReportFor & "'", AgL.GCn)
        If DTTemp.Rows.Count > 0 Then StrReportForCode = AgL.XNull(DTTemp.Rows(0).Item("GroupCode")) : DTTemp.Rows.Clear()
        DTTemp.Dispose()

        FSetValue(0, "From Date", FGDataType.DT_Date, FilterCodeType.DTNone, AgL.PubStartDate)
        FSetValue(1, "To Date", FGDataType.DT_Date, FilterCodeType.DTNone, AgL.PubLoginDate)
        FSetValue(2, "Account Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, "All", False)
        FRH_Multiple(2) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable("Select 'o' As Tick,SG.SubCode,SG.Name,SG.ManualCode,IfNull(CT.CityName,'') From SubGroup SG Left Join City CT On SG.CityCode=CT.CityCode  " &
                          "Where   (SG.SiteList Like '%|" & AgL.PubSiteCode & "|%' Or SG.SiteList Is Null) " &
                          "Order By SG.Name", AgL.GCn)), "", 600, 760, , , False)
        FRH_Multiple(2).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(2).FFormatColumn(1, , 0, , False)
        FRH_Multiple(2).FFormatColumn(2, "Name", 440, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple(2).FFormatColumn(3, "Code", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple(2).FFormatColumn(4, "City", 100, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(3, "Category Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, "All", False)
        FRH_Multiple(3) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable("Select 'o' As Tick,Code,Name From TdsCat Order By Name", AgL.GCn)), "", 600, 660, , , False)
        FRH_Multiple(3).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(3).FFormatColumn(1, , 0, , False)
        FRH_Multiple(3).FFormatColumn(2, "Category", 440, DataGridViewContentAlignment.MiddleLeft)

        Strsql = "Select 'Yes' as Code, 'Yes' as Name Union All Select 'No' as Code, 'No' as Name"
        FSetValue(4, "Seperate Page", FGDataType.DT_Selection_Single, FilterCodeType.DTNone, "Yes", False)
        FRH_Single(4) = New DMHelpGrid.FrmHelpGrid(New DataView(CMain.FGetDatTable(Strsql, AgL.GCn)), "", 150, 180, , , False)
        FRH_Single(4).FFormatColumn(0, , 0, , False)
        FRH_Single(4).FFormatColumn(1, "Name", 100, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(5, "TDS Deduct From", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, "All", False)
        FRH_Multiple(5) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable("Select 'o' As Tick,SG.SubCode,SG.Name,SG.ManualCode,IfNull(CT.CityName,'') From SubGroup SG  Left Join Ledger LG On SG.SubCode=LG.TDSDeductFrom  Left Join City CT On SG.CityCode=CT.CityCode  " &
                          "Where   (SG.SiteList Like '%|" & AgL.PubSiteCode & "|%' Or SG.SiteList Is Null) " &
                          "ANd IfNull(LG.TDSDeductFrom,'') <> ''  Order By SG.Name", AgL.GCn)), "", 600, 760, , , False)
        FRH_Multiple(5).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(5).FFormatColumn(1, , 0, , False)
        FRH_Multiple(5).FFormatColumn(2, "Name", 440, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple(5).FFormatColumn(3, "Code", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple(5).FFormatColumn(4, "City", 100, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(6, "With Narration", FGDataType.DT_Selection_Single, FilterCodeType.DTNone, "No", True)
        FRH_Single(6) = New DMHelpGrid.FrmHelpGrid(New DataView(CMain.FGetDatTable(Strsql, AgL.GCn)), "", 150, 180, , , False)
        FRH_Single(6).FFormatColumn(0, , 0, , False)
        FRH_Single(6).FFormatColumn(1, "Name", 100, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(7, "Site Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, AgL.PubSiteName & "|'" & AgL.PubSiteCode & "'", False)
        FRH_Multiple(7) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable(
                          "Select 'o' As Tick,Sm.Code,Sm.Name From Sitemast Sm where code in (" & AgL.PubSiteList & ")   Order By Sm.Name",
                          AgL.GCn)), "", 300, 360, , , False, AgL.PubSiteCode)
        FRH_Multiple(7).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(7).FFormatColumn(1, , 0, , False)
        FRH_Multiple(7).FFormatColumn(2, "Name", 240, DataGridViewContentAlignment.MiddleLeft)
    End Sub


    Private Sub FINI_TDSCategoryWiseReport()
        Dim DTTemp As DataTable
        Dim StrReportForCode As String = ""
        Dim Strsql As String
        DTTemp = CMain.FGetDatTable("SELECT GroupCode FROM AcGroup WHERE GroupName='" & StrReportFor & "'", AgL.GCn)
        If DTTemp.Rows.Count > 0 Then StrReportForCode = AgL.XNull(DTTemp.Rows(0).Item("GroupCode")) : DTTemp.Rows.Clear()
        DTTemp.Dispose()

        FSetValue(0, "From Date", FGDataType.DT_Date, FilterCodeType.DTNone, AgL.PubStartDate)
        FSetValue(1, "To Date", FGDataType.DT_Date, FilterCodeType.DTNone, AgL.PubLoginDate)
        FSetValue(2, "Category Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, "All", False)
        FRH_Multiple(2) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable("Select 'o' As Tick,Code,Name From TdsCat Order By Name", AgL.GCn)), "", 600, 660, , , False)
        FRH_Multiple(2).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(2).FFormatColumn(1, , 0, , False)
        FRH_Multiple(2).FFormatColumn(2, "Category", 440, DataGridViewContentAlignment.MiddleLeft)

        Strsql = "Select 'Yes' as Code, 'Yes' as Name Union All Select 'No' as Code, 'No' as Name"
        FSetValue(3, "Seperate Page", FGDataType.DT_Selection_Single, FilterCodeType.DTNone, "Yes", False)
        FRH_Single(3) = New DMHelpGrid.FrmHelpGrid(New DataView(CMain.FGetDatTable(Strsql, AgL.GCn)), "", 150, 180, , , False)
        FRH_Single(3).FFormatColumn(0, , 0, , False)
        FRH_Single(3).FFormatColumn(1, "Name", 100, DataGridViewContentAlignment.MiddleLeft)

        Strsql = "Select 'Yes' as Code, 'Yes' as Name Union All Select 'No' as Code, 'No' as Name"
        FSetValue(4, "Party Wise", FGDataType.DT_Selection_Single, FilterCodeType.DTNone, "Yes", False)
        FRH_Single(4) = New DMHelpGrid.FrmHelpGrid(New DataView(CMain.FGetDatTable(Strsql, AgL.GCn)), "", 150, 180, , , False)
        FRH_Single(4).FFormatColumn(0, , 0, , False)
        FRH_Single(4).FFormatColumn(1, "Name", 100, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(5, "With Narration", FGDataType.DT_Selection_Single, FilterCodeType.DTNone, "No", True)
        FRH_Single(5) = New DMHelpGrid.FrmHelpGrid(New DataView(CMain.FGetDatTable(Strsql, AgL.GCn)), "", 150, 180, , , False)
        FRH_Single(5).FFormatColumn(0, , 0, , False)
        FRH_Single(5).FFormatColumn(1, "Name", 100, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(6, "Site Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, AgL.PubSiteName & "|'" & AgL.PubSiteCode & "'", False)
        FRH_Multiple(6) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable(
                          "Select 'o' As Tick,Sm.Code,Sm.Name From Sitemast Sm where code in (" & AgL.PubSiteList & ")   Order By Sm.Name",
                          AgL.GCn)), "", 300, 360, , , False, AgL.PubSiteCode)
        FRH_Multiple(6).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(6).FFormatColumn(1, , 0, , False)
        FRH_Multiple(6).FFormatColumn(2, "Name", 240, DataGridViewContentAlignment.MiddleLeft)
    End Sub
    Private Sub FFBTReport()
        Dim StrCondition As String
        Dim DTTemp As DataTable
        Dim StrCnd As String = ""

        If Not FIsValid(0) Then Exit Sub
        If Not FIsValid(1) Then Exit Sub

        If Trim(FGMain(GFilter, 3).Value) = "Yes" Then
            StrCondition = " And (L.V_Date <=" & AgL.Chk_Date(CDate(FGMain(GFilter, 1).Value.ToString).ToString("s")) & " ) "
        Else
            StrCondition = " And ( Date(L.V_Date) Between " & AgL.Chk_Date(CDate(FGMain(GFilter, 0).Value.ToString).ToString("s")) & " And " & AgL.Chk_Date(CDate(FGMain(GFilter, 1).Value.ToString).ToString("s")) & " ) "
        End If

        If Trim(FGMain(GFilterCode, 2).Value) <> "" Then StrCondition = StrCondition & " And L.SubCode In (" & FGMain(GFilterCode, 2).Value & ")"

        If Trim(FGMain(GFilterCode, 4).Value) <> "" Then
            StrCondition += " And  L.Site_Code IN (" & FGMain(GFilterCode, 4).Value & ") "
        Else
            StrCondition += " And  L.Site_Code IN (" & AgL.PubSiteList & ") "
        End If


        StrSQLQuery = "SELECT max(SG.SubCode) AS SubCode,SG.Name,"
        StrSQLQuery += "sum(L.AmtDr)-sum (L.AmtCr) AS DrBal,"
        StrSQLQuery += "Max(IfNull(SG.FBTOnPer,0)) AS FBTOnPer,"
        StrSQLQuery += "Max(IfNull(SG.FBTOnPer,0))*(sum(L.AmtDr)-sum (L.AmtCr))/100 AS FBTOn,"
        StrSQLQuery += "Max(IfNull(SG.FBTPer,0)) AS FBTPer,"
        StrSQLQuery += "(Max(IfNull(SG.FBTOnPer,0))*(sum(L.AmtDr)-sum (L.AmtCr))/100)*Max(IfNull(SG.FBTPer,0))/100 AS FBT "
        StrSQLQuery += "FROM Ledger L "
        StrSQLQuery += "LEFT JOIN SubGroup SG ON SG.SubCode=L.SubCode "
        StrSQLQuery += "WHERE SG.Nature='Expenses' "
        StrSQLQuery += StrCondition
        StrSQLQuery += "AND IfNull(SG.FBTOnPer,0)>0 "
        StrSQLQuery += "AND IfNull(SG.FBTPer,0)>0 "
        StrSQLQuery += "GROUP BY SG.Name "
        StrSQLQuery += "HAVING(sum(L.AmtDr) - sum(L.AmtCr) > 0)"

        StrSQLQuery = AgL.GetBackendBasedQuery(StrSQLQuery)
        DTTemp = CMain.FGetDatTable(StrSQLQuery, AgL.GCn)

        If Not DTTemp.Rows.Count > 0 Then MsgBox("No Records Found to Print.") : Exit Sub

        FLoadMainReport("FBTReport", DTTemp)
        CMain.FormulaSet(RptMain, Me.Text, FGMain)
        CMain.FShowReport(RptMain, Me.MdiParent, Me.Text)
    End Sub
    Private Sub FPartyWiseTDSReport()
        Dim StrCondition As String
        Dim DTTemp As DataTable
        Dim StrCnd As String = ""
        Dim TempField As String
        Dim strNarr As String = ""
        If Not FIsValid(0) Then Exit Sub
        If Not FIsValid(1) Then Exit Sub


        StrCondition = " And ( Date(L.V_Date) Between " & AgL.Chk_Date(CDate(FGMain(GFilter, 0).Value.ToString).ToString("s")) & " And " & AgL.Chk_Date(CDate(FGMain(GFilter, 1).Value.ToString).ToString("s")) & " ) "


        If Trim(FGMain(GFilterCode, 2).Value) <> "" Then StrCondition = StrCondition & " And SG.SubCode In (" & FGMain(GFilterCode, 2).Value & ")"
        If Trim(FGMain(GFilterCode, 3).Value) <> "" Then StrCondition = StrCondition & " And TC.Code In (" & FGMain(GFilterCode, 3).Value & ")"
        If Trim(FGMain(GFilterCode, 5).Value) <> "" Then StrCondition = StrCondition & " And SG3.SubCode  In (" & FGMain(GFilterCode, 5).Value & ")"
        If Trim(FGMain(GFilter, 4).Value) = "Yes" Then TempField = ",1 as PB " Else TempField = ",0 as PB "

        If Trim(FGMain(GFilterCode, 6).Value) = "Yes" Then
            strNarr = "Y"
        Else
            strNarr = "N"
        End If


        If Trim(FGMain(GFilterCode, 7).Value) <> "" Then
            StrCondition += " And  L.Site_Code IN (" & FGMain(GFilterCode, 7).Value & ") "
        Else
            StrCondition += " And  L.Site_Code IN (" & AgL.PubSiteList & ") "
        End If


        StrSQLQuery = "SELECT IfNull(SG.Name,'') || ' ' || IfNull(C.CityName,'') AS Party,Cast(L.V_No as Varchar) As V_No,L.V_Type as VType,L.V_Date,L.Narration,"
        StrSQLQuery += "TC.Name AS TSDCat,TCD.Name AS Description,L.TdsOnAmt,"
        StrSQLQuery += "L.TdsPer,L.Amtcr AS TdsAmt,sg2.Name AS PostingAc"
        StrSQLQuery += TempField
        StrSQLQuery += ",'" & strNarr & "'  AS NarYN,SG3.Name AS TDSDeductFrom "
        StrSQLQuery += "FROM Ledger L "
        StrSQLQuery += "LEFT JOIN SubGroup SG ON SG.SubCode =L.ContraSub "
        StrSQLQuery += "LEFT JOIN TDSCat TC ON TC.Code=L.TDSCategory "
        StrSQLQuery += "LEFT JOIN TDSCat_Description  TCD ON TCD.Code=L.TdsDesc "
        StrSQLQuery += "LEFT JOIN TdsCat_Det TD ON TD.TdsDesc =TCD.Code AND TD.Code=TC.Code "
        StrSQLQuery += "LEFT JOIN SubGroup SG2 ON SG2.SubCode=TD.AcCode "
        StrSQLQuery += "LEFT JOIN SubGroup SG3 ON SG3.SubCode=(SELECT TDSDeductFrom FROM Ledger WHERE DocId=L.Docid AND IfNull(TDSDeductFrom,'')<>'' AND Subcode=L.ContraSub)  "
        StrSQLQuery += "LEFT JOIN City C ON C.CityCode=SG.CityCode "
        StrSQLQuery += "WHERE IfNull(L.TDSCategory,'')<>'' AND IfNull(L.tdsdesc,'')<>'' "
        StrSQLQuery += "AND L.System_Generated ='Y' "
        StrSQLQuery += StrCondition

        StrSQLQuery = AgL.GetBackendBasedQuery(StrSQLQuery)
        DTTemp = CMain.FGetDatTable(StrSQLQuery, AgL.GCn)

        If Not DTTemp.Rows.Count > 0 Then MsgBox("No Records Found to Print.") : Exit Sub

        FLoadMainReport("PartyWiseTDSReport", DTTemp)
        CMain.FormulaSet(RptMain, Me.Text, FGMain)
        CMain.FShowReport(RptMain, Me.MdiParent, Me.Text)
    End Sub
    Private Sub FTDSCategoryWiseReport()
        Dim StrCondition As String
        Dim DTTemp As DataTable
        Dim StrCnd As String = ""
        Dim TempField As String
        Dim strNarr As String = ""
        If Not FIsValid(0) Then Exit Sub
        If Not FIsValid(1) Then Exit Sub


        StrCondition = " And ( Date(L.V_Date) Between " & AgL.Chk_Date(CDate(FGMain(GFilter, 0).Value.ToString).ToString("s")) & " And " & AgL.Chk_Date(CDate(FGMain(GFilter, 1).Value.ToString).ToString("s")) & " ) "


        If Trim(FGMain(GFilterCode, 2).Value) <> "" Then StrCondition = StrCondition & " And TC.Code In (" & FGMain(GFilterCode, 2).Value & ")"
        If Trim(FGMain(GFilter, 3).Value) = "Yes" Then TempField = ",1 as PB " Else TempField = ",0 as PB "

        If Trim(FGMain(GFilterCode, 5).Value) = "Yes" Then
            strNarr = "Y"
        Else
            strNarr = "N"
        End If

        If Trim(FGMain(GFilterCode, 6).Value) <> "" Then
            StrCondition += " And  L.Site_Code IN (" & FGMain(GFilterCode, 6).Value & ") "
        Else
            StrCondition += " And  L.Site_Code IN (" & AgL.PubSiteList & ") "
        End If

        StrSQLQuery = "SELECT SG.Name  AS Party,Cast(L.V_No as Varchar) As V_No,L.V_Type as VType,L.V_Date,L.Narration,"
        StrSQLQuery += "TC.Name AS TSDCat,TCD.Name AS Description,L.TdsOnAmt,L.TdsPer,L.Amtcr AS TdsAmt,"
        StrSQLQuery += "SG2.Name AS PostingAc,IfNull(C.CityName,'') As CityName,'" & FGMain(GFilter, 4).Value & "' As PWise  "
        StrSQLQuery += TempField
        StrSQLQuery += ",'" & strNarr & "'  AS NarYN "
        StrSQLQuery += "FROM Ledger L "
        StrSQLQuery += "LEFT JOIN SubGroup SG ON SG.SubCode =L.ContraSub "
        StrSQLQuery += "LEFT JOIN TDSCat TC ON TC.Code=L.TDSCategory "
        StrSQLQuery += "LEFT JOIN TDSCat_Description  TCD ON TCD.Code=L.TdsDesc "
        StrSQLQuery += "LEFT JOIN TdsCat_Det TD ON TD.TdsDesc =TCD.Code AND TD.Code=TC.Code "
        StrSQLQuery += "LEFT JOIN SubGroup SG2 ON SG2.SubCode=TD.AcCode "
        StrSQLQuery += "LEFT JOIN City C ON C.CityCode=SG.CityCode "
        StrSQLQuery += "WHERE IfNull(L.TDSCategory,'')<>'' AND IfNull(L.tdsdesc,'')<>'' "
        StrSQLQuery += "AND L.System_Generated ='Y' "
        StrSQLQuery += StrCondition & " Order By L.V_Date "

        StrSQLQuery = AgL.GetBackendBasedQuery(StrSQLQuery)
        DTTemp = CMain.FGetDatTable(StrSQLQuery, AgL.GCn)

        If Not DTTemp.Rows.Count > 0 Then MsgBox("No Records Found to Print.") : Exit Sub

        FLoadMainReport("TDSCategoryWiseReport", DTTemp)
        CMain.FormulaSet(RptMain, Me.Text, FGMain)
        CMain.FShowReport(RptMain, Me.MdiParent, Me.Text)
    End Sub
    Private Sub FINI_MonthlyExpenses()
        Dim DTTemp As DataTable
        Dim StrReportForCode As String = ""

        DTTemp = CMain.FGetDatTable("SELECT GroupCode FROM AcGroup WHERE GroupName='" & StrReportFor & "'", AgL.GCn)
        If DTTemp.Rows.Count > 0 Then StrReportForCode = AgL.XNull(DTTemp.Rows(0).Item("GroupCode")) : DTTemp.Rows.Clear()
        DTTemp.Dispose()
        FSetValue(0, "Month", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, "All")
        FSetValue(1, "Expenses", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, "All")
        FRH_Multiple(0) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable(
                          "Select 'o' As Tick,'jan' As code,'Jan' As Name union all Select 'o' As Tick,'Feb' As code,'Feb' As Name union all Select 'o' As Tick,'Mar' As code,'Mar' As Name union all Select 'o' As Tick,'Apr' As code,'Apr' As Name union all Select 'o' As Tick,'May' As code,'May' As Name union all Select 'o' As Tick,'Jun' As code,'Jun' As Name union all Select 'o' As Tick,'July' As code,'July' As Name union all Select 'o' As Tick,'Aug' As code,'Aug' As Name union all Select 'o' As Tick,'Sep' As code,'Sep' As Name union all Select 'o' As Tick,'Oct' As code,'Oct' As Name union all Select 'o' As Tick,'Nov' As code,'Nov' As Name union all Select 'o' As Tick,'Dec' As code,'Dec' As Name  ",
                          AgL.GCn)), "", 400, 250, , , False, AgL.PubSiteCode)
        FRH_Multiple(0).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(0).FFormatColumn(1, , 0, , False)
        FRH_Multiple(0).FFormatColumn(2, "Name", 130, DataGridViewContentAlignment.MiddleLeft)

        FRH_Multiple(1) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable(
                         "Select 'o' As Tick,SG.SubCode,SG.Name,Sg.ManualCode From SubGroup SG Where SG.GroupNature='E' Order By SG.Name",
                         AgL.GCn)), "", 400, 525, , , False)
        FRH_Multiple(1).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(1).FFormatColumn(1, , 0, , False)
        FRH_Multiple(1).FFormatColumn(2, "Name", 300, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(2, "Site Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, AgL.PubSiteName & "|'" & AgL.PubSiteCode & "'")
        FRH_Multiple(2) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable(
                          "Select 'o' As Tick,Sm.Code,Sm.Name From Sitemast Sm where code in (" & AgL.PubSiteList & ")   Order By Sm.Name",
                          AgL.GCn)), "", 300, 360, , , False, AgL.PubSiteCode)
        FRH_Multiple(2).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(2).FFormatColumn(1, , 0, , False)
        FRH_Multiple(2).FFormatColumn(2, "Name", 240, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(3, "Division Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, AgL.PubDivName & "|'" & AgL.PubDivCode & "'")
        FRH_Multiple(3) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable(
                          "Select 'o' As Tick,Sm.Div_Code Code, Sg.DispName Name From Division Sm 
                           Left Join Subgroup Sg On SM.Subcode = Sg.Subcode 
                           where Sm.Div_code in (" & AgL.PubDivisionList & ")   
                          Order By Sg.DispName",
                          AgL.GCn)), "", 300, 360, , , False, AgL.PubSiteCode)
        FRH_Multiple(3).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(3).FFormatColumn(1, , 0, , False)
        FRH_Multiple(3).FFormatColumn(2, "Name", 240, DataGridViewContentAlignment.MiddleLeft)

    End Sub
    Private Sub FINI_BillWsOS(ByVal StrReportFor As String)
        Dim DTTemp As DataTable
        Dim StrReportForCode As String = ""
        Dim StrSQL As String = ""

        DTTemp = CMain.FGetDatTable("SELECT GroupCode FROM AcGroup WHERE GroupName='" & StrReportFor & "'", AgL.GCn)
        If DTTemp.Rows.Count > 0 Then StrReportForCode = AgL.XNull(DTTemp.Rows(0).Item("GroupCode")) : DTTemp.Rows.Clear()
        DTTemp.Dispose()

        FSetValue(0, "As On Date", FGDataType.DT_Date, FilterCodeType.DTNone, AgL.PubLoginDate)
        FSetValue(1, "Account Group", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, "All")
        FRH_Multiple(1) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable("Select 'o' As Tick,Ag.GroupCode,Ag.GroupName FROM AcGroup  AG   " &
                          "Where AG.GroupCode In (SELECT AGP.GroupCode FROM AcGroupPath AGP WHERE AGP.GroupUnder='" & StrReportForCode & "') Or AG.GroupCode='" & StrReportForCode & "' " &
                          "Order By AG.GroupName ", AgL.GCn)), "", 600, 460, , , False)
        FRH_Multiple(1).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(1).FFormatColumn(1, , 0, , False)
        FRH_Multiple(1).FFormatColumn(2, "Name", 340, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(2, "Account Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, "All")
        FRH_Multiple(2) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable("Select 'o' As Tick,SG.SubCode,SG.Name,SG.ManualCode,AG.GroupName From SubGroup  SG Left Join  " &
                          "AcGroup AG On AG.GroupCode=SG.GroupCode " &
                          "Where SG.GroupCode In (SELECT AGP.GroupCode FROM AcGroupPath AGP WHERE " &
                          "AGP.GroupUnder='" & StrReportForCode & "') Or SG.GroupCode='" & StrReportForCode & "' and  (SG.SiteList Like '%|" & AgL.PubSiteCode & "|%' Or SG.SiteList Is Null) " &
                          "Order By SG.Name", AgL.GCn)), "", 600, 860, , , False)
        FRH_Multiple(2).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(2).FFormatColumn(1, , 0, , False)
        FRH_Multiple(2).FFormatColumn(2, "Name", 440, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple(2).FFormatColumn(3, "Code", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple(2).FFormatColumn(4, "Group Name", 200, DataGridViewContentAlignment.MiddleLeft)


        FSetValue(3, "Area Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, "All")
        FRH_Multiple(3) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable(
                          "Select 'o' As Tick,Zm.Code,Zm.Description as Name From Area Zm  Order By Zm.Description",
                          AgL.GCn)), "", 300, 360, , , False)
        FRH_Multiple(3).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(3).FFormatColumn(1, , 0, , False)
        FRH_Multiple(3).FFormatColumn(2, "Name", 240, DataGridViewContentAlignment.MiddleLeft)




        StrSQL = "Select 'D' as Code, 'Detail' as Name Union All Select 'S' as Code, 'Summary' as Name "
        FSetValue(4, "Report On Choice", FGDataType.DT_Selection_Single, FilterCodeType.DTNone, "Details", False)
        FRH_Single(4) = New DMHelpGrid.FrmHelpGrid(New DataView(CMain.FGetDatTable(StrSQL, AgL.GCn)), "", 200, 220, , , False)
        FRH_Single(4).FFormatColumn(0, , 0, , False)
        FRH_Single(4).FFormatColumn(1, "Name", 140, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(5, "Site Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, AgL.PubSiteName & "|'" & AgL.PubSiteCode & "'")
        FRH_Multiple(5) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable(
                          "Select 'o' As Tick,Sm.Code,Sm.Name From Sitemast Sm where code in (" & AgL.PubSiteList & ")   Order By Sm.Name",
                          AgL.GCn)), "", 300, 360, , , False, AgL.PubSiteCode)
        FRH_Multiple(5).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(5).FFormatColumn(1, , 0, , False)
        FRH_Multiple(5).FFormatColumn(2, "Name", 240, DataGridViewContentAlignment.MiddleLeft)
    End Sub
    Private Sub FIni_journal()
        FSetValue(0, "From Date", FGDataType.DT_Date, FilterCodeType.DTNone, AgL.PubStartDate)
        FSetValue(1, "To Date", FGDataType.DT_Date, FilterCodeType.DTNone, AgL.PubLoginDate)

        FSetValue(2, "Voucher Type", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, "All")
        FRH_Multiple(2) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable(
                          "select DISTINCT 'o' As Tick ,V_TYPE AS Code, RTRIM(LTRIM(Description)) || ' Book' AS Name from voucher_type Where category='JV'",
                          AgL.GCn)), "", 600, 550, , , False)
        FRH_Multiple(2).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(2).FFormatColumn(1, , 0, , False)
        FRH_Multiple(2).FFormatColumn(2, "Name", 440, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(3, "Site Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, AgL.PubSiteName & "|'" & AgL.PubSiteCode & "'")
        FRH_Multiple(3) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable(
                          "Select 'o' As Tick,Sm.Code,Sm.Name From Sitemast Sm where code in (" & AgL.PubSiteList & ")   Order By Sm.Name",
                          AgL.GCn)), "", 300, 360, , , False, AgL.PubSiteCode)
        FRH_Multiple(3).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(3).FFormatColumn(1, , 0, , False)
        FRH_Multiple(3).FFormatColumn(2, "Name", 240, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(4, "Division Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, AgL.PubDivName & "|'" & AgL.PubDivCode & "'")
        FRH_Multiple(4) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable(
                          "Select 'o' As Tick,Sm.Div_Code Code, Sg.DispName Name From Division Sm 
                           Left Join Subgroup Sg On SM.Subcode = Sg.Subcode 
                           where Sm.Div_code in (" & AgL.PubDivisionList & ")   
                          Order By Sg.DispName",
                          AgL.GCn)), "", 300, 360, , , False, AgL.PubSiteCode)
        FRH_Multiple(4).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(4).FFormatColumn(1, , 0, , False)
        FRH_Multiple(4).FFormatColumn(2, "Name", 240, DataGridViewContentAlignment.MiddleLeft)

    End Sub
    Private Sub FINI_DayBook()
        FSetValue(0, "From Date", FGDataType.DT_Date, FilterCodeType.DTNone, AgL.PubStartDate)
        FSetValue(1, "To Date", FGDataType.DT_Date, FilterCodeType.DTNone, AgL.PubLoginDate)

        FSetValue(2, "Voucher Type", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, "All")
        FRH_Multiple(2) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable(
                          "select DISTINCT 'o' As Tick ,V_TYPE AS Code, RTRIM(LTRIM(Description)) || ' Book' AS Name,Category from voucher_type order by Name ",
                          AgL.GCn)), "", 600, 600, , , False)
        FRH_Multiple(2).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(2).FFormatColumn(1, , 0, , False)
        FRH_Multiple(2).FFormatColumn(2, "Name", 300, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple(2).FFormatColumn(3, "Category", 160, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(3, "Site Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, AgL.PubSiteName & "|'" & AgL.PubSiteCode & "'", False)
        FRH_Multiple(3) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable(
                          "Select 'o' As Tick,Sm.Code,Sm.Name From Sitemast Sm where code in (" & AgL.PubSiteList & ")   Order By Sm.Name",
                          AgL.GCn)), "", 300, 360, , , False, AgL.PubSiteCode)
        FRH_Multiple(3).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(3).FFormatColumn(1, , 0, , False)
        FRH_Multiple(3).FFormatColumn(2, "Name", 240, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(4, "Division Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, AgL.PubDivName & "|'" & AgL.PubDivCode & "'")
        FRH_Multiple(4) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable(
                          "Select 'o' As Tick,Sm.Div_Code Code, Sg.DispName Name From Division Sm 
                           Left Join Subgroup Sg On SM.Subcode = Sg.Subcode 
                           where Sm.Div_code in (" & AgL.PubDivisionList & ")   
                          Order By Sg.DispName",
                          AgL.GCn)), "", 300, 360, , , False, AgL.PubSiteCode)
        FRH_Multiple(4).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(4).FFormatColumn(1, , 0, , False)
        FRH_Multiple(4).FFormatColumn(2, "Name", 240, DataGridViewContentAlignment.MiddleLeft)

    End Sub
    Private Sub FIni_Annexure()
        FSetValue(0, "Up To Date", FGDataType.DT_Date, FilterCodeType.DTNone, AgL.PubLoginDate)
        FSetValue(1, "Account Group ", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, "All")
        FRH_Multiple(1) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable(
                          "Select 'o' As Tick,AG.groupcode,AG.GroupName as Name, " &
                          "(Case When GroupNature='L' Then 'Liabilities' " &
                          "When GroupNature='A' Then 'Assets' " &
                          "When GroupNature='E' Then 'Revenue' " &
                          "When GroupNature='R' Then 'Expenses' End) MainGroup " &
                          "From acgroup AG Order By AG.GroupName",
                          AgL.GCn)), "", 600, 760, , , False)
        FRH_Multiple(1).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(1).FFormatColumn(1, , 0, , False)
        FRH_Multiple(1).FFormatColumn(2, "Name", 440, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple(1).FFormatColumn(3, "Main Group", 100, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(2, "Site Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, AgL.PubSiteName & "|'" & AgL.PubSiteCode & "'")
        FRH_Multiple(2) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable(
                          "Select 'o' As Tick,Sm.Code,Sm.Name From Sitemast Sm where code in (" & AgL.PubSiteList & ")   Order By Sm.Name",
                          AgL.GCn)), "", 300, 360, , , False, AgL.PubSiteCode)
        FRH_Multiple(2).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(2).FFormatColumn(1, , 0, , False)
        FRH_Multiple(2).FFormatColumn(2, "Name", 240, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(3, "Division Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, AgL.PubDivName & "|'" & AgL.PubDivCode & "'")
        FRH_Multiple(3) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable(
                          "Select 'o' As Tick,Sm.Div_Code Code, Sg.DispName Name From Division Sm 
                           Left Join Subgroup Sg On SM.Subcode = Sg.Subcode 
                           where Sm.Div_code in (" & AgL.PubDivisionList & ")   
                          Order By Sg.DispName",
                          AgL.GCn)), "", 300, 360, , , False, AgL.PubSiteCode)
        FRH_Multiple(3).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(3).FFormatColumn(1, , 0, , False)
        FRH_Multiple(3).FFormatColumn(2, "Name", 240, DataGridViewContentAlignment.MiddleLeft)

    End Sub
    Private Sub FIni_FixedAssetRegister()
        Dim DTTemp As DataTable

        FSetValue(0, "As ON Date", FGDataType.DT_Date, FilterCodeType.DTNone, AgL.PubLoginDate)

        FSetValue(1, "Group Name ", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, "All")
        FRH_Multiple(1) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable(
                          "SELECT 'o' As Tick,Code,Name FROM AssetGroupMast Order By Name",
                          AgL.GCn)), "", 300, 320, , , False)
        FRH_Multiple(1).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(1).FFormatColumn(1, , 0, , False)
        FRH_Multiple(1).FFormatColumn(2, "Name", 200, DataGridViewContentAlignment.MiddleLeft)


        FSetValue(2, "Report Type", FGDataType.DT_Selection_Single, FilterCodeType.DTNone, "Summary")
        DTTemp = New DataTable
        DTTemp.Columns.Add("Code", System.Type.GetType("System.String"))
        DTTemp.Columns.Add("Name", System.Type.GetType("System.String"))
        DTTemp.Rows.Add(New Object() {"Summary", "Summary"})
        DTTemp.Rows.Add(New Object() {"Detail", "Detail"})

        FRH_Single(2) = New DMHelpGrid.FrmHelpGrid(New DataView(DTTemp), "", 150, 200, , , False)
        FRH_Single(2).FFormatColumn(0, , 0, , False)
        FRH_Single(2).FFormatColumn(1, "Name", 130, DataGridViewContentAlignment.MiddleLeft)
    End Sub
    Private Sub FIni_Ledger()
        Dim StrSQL As String

        FSetValue(0, "From Date", FGDataType.DT_Date, FilterCodeType.DTNone, AgL.PubStartDate)
        FSetValue(1, "To Date", FGDataType.DT_Date, FilterCodeType.DTNone, AgL.PubLoginDate)

        FSetValue(2, "Account Group", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, "All", False)
        FRH_Multiple(2) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable(
                          "Select 'o' As Tick,AG.GroupCode,AG.GroupName From AcGroup AG Order By AG.GroupName",
                          AgL.GCn)), "", 600, 560, , , False)
        FRH_Multiple(2).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(2).FFormatColumn(1, , 0, , False)
        FRH_Multiple(2).FFormatColumn(2, "Group Name", 440, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(3, "Account Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, "All", False)
        FRH_Multiple(3) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable(
                          "Select 'o' As Tick,SG.SubCode,SG.Name,Sg.ManualCode,IfNull(CT.CityName,''),AG.GroupName, Sg.SubgroupType " &
                          "From SubGroup SG Left Join " &
                          "AcGroup AG On AG.GroupCode=SG.GroupCode Left Join " &
                          "City CT On SG.CityCode=CT.CityCode where  (SG.SiteList Like '%|" & AgL.PubSiteCode & "|%' Or SG.SiteList Is Null)  " &
                          "Order By SG.Name",
                          AgL.GCn)), "", 600, 960, , , False)
        FRH_Multiple(3).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(3).FFormatColumn(1, , 0, , False)
        FRH_Multiple(3).FFormatColumn(2, "Name", 350, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple(3).FFormatColumn(3, "Code", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple(3).FFormatColumn(4, "City", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple(3).FFormatColumn(5, "Group Name", 130, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple(3).FFormatColumn(6, "A/C Type", 130, DataGridViewContentAlignment.MiddleLeft)


        FSetValue(4, "Voucher Type", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, "All", False)
        FRH_Multiple(4) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable(
                          "Select 'o' As Tick, VT.V_Type AS Code,VT.V_Type ,VT.Description   FROM Voucher_Type VT WHERE VT.V_Type IN (SELECT V_Type FROM  Ledger Where  Site_code in (" & AgL.PubSiteList & "))   Order By VT.Description ",
                          AgL.GCn)), "", 300, 460, , , False, AgL.PubSiteCode)
        FRH_Multiple(4).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(4).FFormatColumn(1, , 0, , False)
        FRH_Multiple(4).FFormatColumn(2, "Type", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple(4).FFormatColumn(3, "Name", 240, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(5, "Site Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, AgL.PubSiteName & "|'" & AgL.PubSiteCode & "'", False)
        FRH_Multiple(5) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable(
                          "Select 'o' As Tick,Sm.Code,Sm.Name From Sitemast Sm where code in (" & AgL.PubSiteList & ")   Order By Sm.Name",
                          AgL.GCn)), "", 300, 360, , , False, AgL.PubSiteCode)
        FRH_Multiple(5).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(5).FFormatColumn(1, , 0, , False)
        FRH_Multiple(5).FFormatColumn(2, "Name", 240, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(6, "Division Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, AgL.PubDivName & "|'" & AgL.PubDivCode & "'")
        FRH_Multiple(6) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable(
                          "Select 'o' As Tick,Sm.Div_Code Code, Sg.DispName Name From Division Sm 
                           Left Join Subgroup Sg On SM.Subcode = Sg.Subcode 
                           where Sm.Div_code in (" & AgL.PubDivisionList & ")   
                          Order By Sg.DispName",
                          AgL.GCn)), "", 300, 360, , , False, AgL.PubSiteCode)
        FRH_Multiple(6).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(6).FFormatColumn(1, , 0, , False)
        FRH_Multiple(6).FFormatColumn(2, "Name", 240, DataGridViewContentAlignment.MiddleLeft)



        StrSQL = "Select 'Y' as Code, 'Yes' as Name Union All Select 'N' as Code, 'No' as Name"

        FSetValue(7, "Index Needed", FGDataType.DT_Selection_Single, FilterCodeType.DTNone, "No", False)
        FRH_Single(7) = New DMHelpGrid.FrmHelpGrid(New DataView(CMain.FGetDatTable(StrSQL, AgL.GCn)), "", 150, 180, , , False)
        FRH_Single(7).FFormatColumn(0, , 0, , False)
        FRH_Single(7).FFormatColumn(1, "Name", 100, DataGridViewContentAlignment.MiddleLeft)

        StrSQL = "Select 'Y' as Code, 'Yes' as Name Union All Select 'N' as Code, 'No' as Name"

        FSetValue(8, "Contra A/C Needed", FGDataType.DT_Selection_Single, FilterCodeType.DTNone, "No", False)
        FRH_Single(8) = New DMHelpGrid.FrmHelpGrid(New DataView(CMain.FGetDatTable(StrSQL, AgL.GCn)), "", 150, 180, , , False)
        FRH_Single(8).FFormatColumn(0, , 0, , False)
        FRH_Single(8).FFormatColumn(1, "Name", 100, DataGridViewContentAlignment.MiddleLeft)

        StrSQL = "Select 'Y' as Code, 'Yes' as Name Union All Select 'N' as Code, 'No' as Name"
        FSetValue(9, "Show Voucher No", FGDataType.DT_Selection_Single, FilterCodeType.DTNone, "Yes", False)
        FRH_Single(9) = New DMHelpGrid.FrmHelpGrid(New DataView(CMain.FGetDatTable(StrSQL, AgL.GCn)), "", 150, 180, , , False)
        FRH_Single(9).FFormatColumn(0, , 0, , False)
        FRH_Single(9).FFormatColumn(1, "Name", 100, DataGridViewContentAlignment.MiddleLeft)
    End Sub

    Private Sub FIni_Bank_CashBook(ByVal StrTypeIn As String)
        Dim StrSQL As String

        FSetValue(0, "From Date", FGDataType.DT_Date, FilterCodeType.DTNone, AgL.PubStartDate, False)
        FSetValue(1, "To Date", FGDataType.DT_Date, FilterCodeType.DTNone, AgL.PubLoginDate, False)

        FSetValue(2, "Site Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, AgL.PubSiteName & "|'" & AgL.PubSiteCode & "'")
        FRH_Multiple(2) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable(
                          "Select 'o' As Tick,Sm.Code,Sm.Name From Sitemast Sm where code in (" & AgL.PubSiteList & ")   Order By Sm.Name",
                          AgL.GCn)), "", 300, 360, , , False, AgL.PubSiteCode)
        FRH_Multiple(2).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(2).FFormatColumn(1, , 0, , False)
        FRH_Multiple(2).FFormatColumn(2, "Name", 240, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(3, "Division Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, AgL.PubDivName & "|'" & AgL.PubDivCode & "'")
        FRH_Multiple(3) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable(
                          "Select 'o' As Tick,Sm.Div_Code Code, Sg.DispName Name From Division Sm 
                           Left Join Subgroup Sg On SM.Subcode = Sg.Subcode 
                           where Sm.Div_code in (" & AgL.PubDivisionList & ")   
                          Order By Sg.DispName",
                          AgL.GCn)), "", 300, 360, , , False, AgL.PubSiteCode)
        FRH_Multiple(3).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(3).FFormatColumn(1, , 0, , False)
        FRH_Multiple(3).FFormatColumn(2, "Name", 240, DataGridViewContentAlignment.MiddleLeft)



        FSetValue(4, "Account", FGDataType.DT_Selection_Single, FilterCodeType.DTString, , True)
        FRH_Single(4) = New DMHelpGrid.FrmHelpGrid(New DataView(CMain.FGetDatTable(
                          "Select SG.SubCode,SG.Name From SubGroup SG  Where (SG.SiteList Like '%|" & AgL.PubSiteCode & "|%' Or SG.SiteList Is Null) And " &
                          "SG.Nature In (Select Nature From AcFilteration Where V_Type In (" & StrTypeIn & ")) Order by SG.Name",
                          AgL.GCn)), "", 300, 360, , , False)
        FRH_Single(4).FFormatColumn(0, "", 0, , False)
        FRH_Single(4).FFormatColumn(1, "Name", 250, DataGridViewContentAlignment.MiddleLeft)

        StrSQL = "Select 'Y' as Code, 'Yes' as Name Union All Select 'N' as Code, 'No' as Name"
        FSetValue(5, "Page Wise", FGDataType.DT_Selection_Single, FilterCodeType.DTNone, "No", False)
        FRH_Single(5) = New DMHelpGrid.FrmHelpGrid(New DataView(CMain.FGetDatTable(StrSQL, AgL.GCn)), "", 150, 180, , , False)
        FRH_Single(5).FFormatColumn(0, , 0, , False)
        FRH_Single(5).FFormatColumn(1, "Name", 100, DataGridViewContentAlignment.MiddleLeft)

        StrSQL = "Select 'Y' as Code, 'Yes' as Name Union All Select 'N' as Code, 'No' as Name"

        FSetValue(6, "With Narration", FGDataType.DT_Selection_Single, FilterCodeType.DTNone, "Yes|Y", False)
        FRH_Single(6) = New DMHelpGrid.FrmHelpGrid(New DataView(CMain.FGetDatTable(StrSQL, AgL.GCn)), "", 150, 180, , , False)
        FRH_Single(6).FFormatColumn(0, , 0, , False)
        FRH_Single(6).FFormatColumn(1, "Name", 100, DataGridViewContentAlignment.MiddleLeft)

        StrSQL = "Select 'S' as Code, 'Single' as Name Union All Select 'D' as Code, 'Double' as Name Union All Select 'J' as Code, 'Journal' as Name "
        FSetValue(7, "Report Type", FGDataType.DT_Selection_Single, FilterCodeType.DTNone, "Single", False)
        FRH_Single(7) = New DMHelpGrid.FrmHelpGrid(New DataView(CMain.FGetDatTable(StrSQL, AgL.GCn)), "", 150, 180, , , False)
        FRH_Single(7).FFormatColumn(0, , 0, , False)
        FRH_Single(7).FFormatColumn(1, "Name", 100, DataGridViewContentAlignment.MiddleLeft)

        StrSQL = "Select 'Y' as Code, 'Yes' as Name Union All Select 'N' as Code, 'No' as Name"
        FSetValue(8, "Show Voucher No", FGDataType.DT_Selection_Single, FilterCodeType.DTNone, "Yes", False)
        FRH_Single(8) = New DMHelpGrid.FrmHelpGrid(New DataView(CMain.FGetDatTable(StrSQL, AgL.GCn)), "", 150, 180, , , False)
        FRH_Single(8).FFormatColumn(0, , 0, , False)
        FRH_Single(8).FFormatColumn(1, "Name", 100, DataGridViewContentAlignment.MiddleLeft)

        StrSQL = "Select 'Day Wise Summary' as Code, 'Day Wise Summary' as Name 
                    Union All 
                    Select 'Detail' as Code, 'Detail' as Name "
        FSetValue(9, "Fromat", FGDataType.DT_Selection_Single, FilterCodeType.DTNone, "Detail", False)
        FRH_Single(9) = New DMHelpGrid.FrmHelpGrid(New DataView(CMain.FGetDatTable(StrSQL, AgL.GCn)), "", 150, 180, , , False)
        FRH_Single(9).FFormatColumn(0, , 0, , False)
        FRH_Single(9).FFormatColumn(1, "Name", 100, DataGridViewContentAlignment.MiddleLeft)
    End Sub
    Private Sub FIni_TrialGroup()
        FSetValue(0, "As On Date", FGDataType.DT_Date, FilterCodeType.DTNone, AgL.PubLoginDate)

        FSetValue(1, "Site Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, AgL.PubSiteName & "|'" & AgL.PubSiteCode & "'")
        FRH_Multiple(1) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable(
                          "Select 'o' As Tick,Sm.Code,Sm.Name From Sitemast Sm where code in (" & AgL.PubSiteList & ")   Order By Sm.Name",
                          AgL.GCn)), "", 300, 360, , , False, AgL.PubSiteCode)
        FRH_Multiple(1).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(1).FFormatColumn(1, , 0, , False)
        FRH_Multiple(1).FFormatColumn(2, "Name", 240, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(2, "Division Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, AgL.PubDivName & "|'" & AgL.PubDivCode & "'")
        FRH_Multiple(2) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable(
                          "Select 'o' As Tick,Sm.Div_Code Code, Sg.DispName Name From Division Sm 
                           Left Join Subgroup Sg On SM.Subcode = Sg.Subcode 
                           where Sm.Div_code in (" & AgL.PubDivisionList & ")   
                          Order By Sg.DispName",
                          AgL.GCn)), "", 300, 360, , , False, AgL.PubSiteCode)
        FRH_Multiple(2).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(2).FFormatColumn(1, , 0, , False)
        FRH_Multiple(2).FFormatColumn(2, "Name", 240, DataGridViewContentAlignment.MiddleLeft)

    End Sub
    Private Sub FIni_TrialDetail()
        Dim StrSQL As String

        FSetValue(0, "As On Date", FGDataType.DT_Date, FilterCodeType.DTNone, AgL.PubLoginDate)

        FSetValue(1, "Site Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, AgL.PubSiteName & "|'" & AgL.PubSiteCode & "'")
        FRH_Multiple(1) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable(
                          "Select 'o' As Tick,Sm.Code,Sm.Name From Sitemast Sm where code in (" & AgL.PubSiteList & ")   Order By Sm.Name",
                          AgL.GCn)), "", 300, 360, , , False, AgL.PubSiteCode)
        FRH_Multiple(1).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(1).FFormatColumn(1, , 0, , False)
        FRH_Multiple(1).FFormatColumn(2, "Name", 240, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(2, "Division Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, AgL.PubDivName & "|'" & AgL.PubDivCode & "'")
        FRH_Multiple(2) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable(
                          "Select 'o' As Tick,Sm.Div_Code Code, Sg.DispName Name From Division Sm 
                           Left Join Subgroup Sg On SM.Subcode = Sg.Subcode 
                           where Sm.Div_code in (" & AgL.PubDivisionList & ")   
                          Order By Sg.DispName",
                          AgL.GCn)), "", 300, 360, , , False, AgL.PubSiteCode)
        FRH_Multiple(2).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(2).FFormatColumn(1, , 0, , False)
        FRH_Multiple(2).FFormatColumn(2, "Name", 240, DataGridViewContentAlignment.MiddleLeft)

        'StrSQL = "Select 'A' as Code, 'Alphabatical' as Name Union All Select 'M' as Code, 'Manual' as Name "
        StrSQL = "Select 'A' as Code, 'Alphabatical' as Name "
        FSetValue(3, "Positioning", FGDataType.DT_Selection_Single, FilterCodeType.DTNone, "Alphabatical", True)
        FRH_Single(3) = New DMHelpGrid.FrmHelpGrid(New DataView(CMain.FGetDatTable(StrSQL, AgL.GCn)), "", 150, 180, , , False)
        FRH_Single(3).FFormatColumn(0, , 0, , False)
        FRH_Single(3).FFormatColumn(1, "Name", 100, DataGridViewContentAlignment.MiddleLeft)

        StrSQL = "Select 'Y' as Code, 'Yes' as Name Union All Select 'N' as Code, 'No' as Name"

        FSetValue(4, "Show Zero Value", FGDataType.DT_Selection_Single, FilterCodeType.DTNone, "All", True)
        FRH_Single(4) = New DMHelpGrid.FrmHelpGrid(New DataView(CMain.FGetDatTable(StrSQL, AgL.GCn)), "", 150, 180, , , False)
        FRH_Single(4).FFormatColumn(0, , 0, , False)
        FRH_Single(4).FFormatColumn(1, "Name", 100, DataGridViewContentAlignment.MiddleLeft)
    End Sub
    Private Sub FINI_Ageing()
        Dim StrSQL As String
        FSetValue(0, "Up To Date", FGDataType.DT_Date, FilterCodeType.DTNone, AgL.PubLoginDate)
        FSetValue(1, "Account Type ", FGDataType.DT_Selection_Single, FilterCodeType.DTString, "Customer", True)
        FRH_Single(1) = New DMHelpGrid.FrmHelpGrid(New DataView(CMain.FGetDatTable(
                          "Select ag.nature as code,AG.nature as Name From acgroup AG group by ag.nature having ag.nature in('Customer','Supplier') Order By AG.Nature",
                          AgL.GCn)), "", 250, 325, , , False)
        FRH_Single(1).FFormatColumn(0, "", 0, , False)
        FRH_Single(1).FFormatColumn(1, "Name", 250, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(2, "Site Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, AgL.PubSiteName & "|'" & AgL.PubSiteCode & "'")
        FRH_Multiple(2) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable(
                          "Select 'o' As Tick,Sm.Code,Sm.Name From Sitemast Sm where code in (" & AgL.PubSiteList & ")   Order By Sm.Name",
                          AgL.GCn)), "", 300, 360, , , False)
        FRH_Multiple(2).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(2).FFormatColumn(1, , 0, , False)
        FRH_Multiple(2).FFormatColumn(2, "Name", 240, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(3, "I Interval", FGDataType.DT_Numeric, FilterCodeType.DTNumeric, 30, False)
        FSetValue(4, "II Interval", FGDataType.DT_Numeric, FilterCodeType.DTNumeric, 60, False)
        FSetValue(5, "III Interval", FGDataType.DT_Numeric, FilterCodeType.DTNumeric, 90, False)
        FSetValue(6, "IV Interval", FGDataType.DT_Numeric, FilterCodeType.DTNumeric, 120, False)
        FSetValue(7, "V Interval", FGDataType.DT_Numeric, FilterCodeType.DTNumeric, 150, False)
        FSetValue(8, "VI Interval", FGDataType.DT_Numeric, FilterCodeType.DTNumeric, 180, False)

        StrSQL = "Select 'A' as Code, 'All' as Name Union All Select 'HB' as Code, 'Having Balance' as Name "
        FSetValue(9, "Show Records", FGDataType.DT_Selection_Single, FilterCodeType.DTNone, "All")
        FRH_Single(9) = New DMHelpGrid.FrmHelpGrid(New DataView(CMain.FGetDatTable(StrSQL, AgL.GCn)), "", 150, 280, , , False)
        FRH_Single(9).FFormatColumn(0, , 0, , False)
        FRH_Single(9).FFormatColumn(1, "Name", 200, DataGridViewContentAlignment.MiddleLeft)

        StrSQL = "Select 'AG' as Code, 'Account Group Wise' as Name Union All Select 'AC' as Code, 'Account Name Wise' as Name "
        FSetValue(10, "Report On Choice", FGDataType.DT_Selection_Single, FilterCodeType.DTNone, "Account Name Wise|AC")
        FRH_Single(10) = New DMHelpGrid.FrmHelpGrid(New DataView(CMain.FGetDatTable(StrSQL, AgL.GCn)), "", 150, 300, , , False)
        FRH_Single(10).FFormatColumn(0, , 0, , False)
        FRH_Single(10).FFormatColumn(1, "Name", 220, DataGridViewContentAlignment.MiddleLeft)


        FSetValue(11, "Account Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, "All")
        FRH_Multiple(11) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable("Select 'o' As Tick,SG.SubCode,SG.Name,IfNull(CT.CityName,''),AG.GroupName From SubGroup  SG Left Join  " &
                          "AcGroup AG On AG.GroupCode=SG.GroupCode " &
                          "Left Join City CT On SG.CityCode=CT.CityCode " &
                          "Where Sg.Nature In ('Customer','Supplier') and  (SG.SiteList Like '%|" & AgL.PubSiteCode & "|%' Or SG.SiteList Is Null) " &
                          "Order By SG.Name", AgL.GCn)), "", 600, 960, , , False)
        FRH_Multiple(11).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(11).FFormatColumn(1, , 0, , False)
        FRH_Multiple(11).FFormatColumn(2, "Name", 440, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple(11).FFormatColumn(3, "City", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple(11).FFormatColumn(4, "Group Name", 200, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(12, "Division", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, AgL.PubDivName & "|'" & AgL.PubDivCode & "'")
        FRH_Multiple(12) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable(
                          "Select 'o' As Tick,H.Div_Code Code,H.Div_Name Name From Division H where Div_code in (" & AgL.PubDivisionList & ")   Order By H.Div_Name",
                          AgL.GCn)), "", 300, 360, , , False, AgL.PubSiteCode)
        FRH_Multiple(12).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(12).FFormatColumn(1, , 0, , False)
        FRH_Multiple(12).FFormatColumn(2, "Name", 240, DataGridViewContentAlignment.MiddleLeft)

        StrSQL = "Select 'Format-1' as Code, 'Format-1' as Name Union All Select 'Format-2' as Code, 'Format-2' as Name "
        FSetValue(13, "Format", FGDataType.DT_Selection_Single, FilterCodeType.DTNone, "Format-2")
        FRH_Single(13) = New DMHelpGrid.FrmHelpGrid(New DataView(CMain.FGetDatTable(StrSQL, AgL.GCn)), "", 150, 300, , , False)
        FRH_Single(13).FFormatColumn(0, , 0, , False)
        FRH_Single(13).FFormatColumn(1, "Name", 220, DataGridViewContentAlignment.MiddleLeft)

        StrSQL = "Select 'o' As Tick,Ag.GroupCode,Ag.GroupName FROM AcGroup  AG "
        StrSQL += "Order By AG.GroupName "
        FSetValue(14, "Account Group", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, "All")
        FRH_Multiple(14) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable(StrSQL, AgL.GCn)), "", 600, 460, , , False)
        FRH_Multiple(14).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(14).FFormatColumn(1, , 0, , False)
        FRH_Multiple(14).FFormatColumn(2, "Name", 340, DataGridViewContentAlignment.MiddleLeft)
    End Sub
    Private Sub FINI_BillWsOSAgeing(ByVal StrReportFor As String)
        Dim DTTemp As DataTable
        Dim StrReportForCode As String = ""
        Dim StrSQL As String = ""

        DTTemp = CMain.FGetDatTable("SELECT GroupCode FROM AcGroup WHERE GroupName='" & StrReportFor & "'", AgL.GCn)
        If DTTemp.Rows.Count > 0 Then StrReportForCode = AgL.XNull(DTTemp.Rows(0).Item("GroupCode")) : DTTemp.Rows.Clear()
        DTTemp.Dispose()

        FSetValue(0, "As On Date", FGDataType.DT_Date, FilterCodeType.DTNone, AgL.PubLoginDate)
        FSetValue(1, "Account Group", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, "All")
        FRH_Multiple(1) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable("Select 'o' As Tick,Ag.GroupCode,Ag.GroupName FROM AcGroup  AG   " &
                          "Where AG.GroupUnder In (SELECT AGP.GroupCode FROM AcGroupPath AGP WHERE AGP.GroupUnder='" & StrReportForCode & "') Or AG.GroupUnder='" & StrReportForCode & "' " &
                          "Order By AG.GroupName ", AgL.GCn)), "", 600, 460, , , False)
        FRH_Multiple(1).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(1).FFormatColumn(1, , 0, , False)
        FRH_Multiple(1).FFormatColumn(2, "Name", 340, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(2, "Account Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, "All")
        FRH_Multiple(2) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable("Select 'o' As Tick,SG.SubCode,SG.Name,SG.ManualCode,IfNull(CT.CityName,''),AG.GroupName From SubGroup  SG Left Join  " &
                          "AcGroup AG On AG.GroupCode=SG.GroupCode " &
                          "Left Join City CT On SG.CityCode=CT.CityCode " &
                          "Where SG.GroupCode In (SELECT AGP.GroupCode FROM AcGroupPath AGP WHERE  " &
                          "AGP.GroupUnder='" & StrReportForCode & "') Or SG.GroupCode='" & StrReportForCode & "' and  (SG.SiteList Like '%|" & AgL.PubSiteCode & "|%' Or SG.SiteList Is Null) " &
                          "Order By SG.Name", AgL.GCn)), "", 600, 960, , , False)
        FRH_Multiple(2).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(2).FFormatColumn(1, , 0, , False)
        FRH_Multiple(2).FFormatColumn(2, "Name", 440, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple(2).FFormatColumn(3, "Code", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple(2).FFormatColumn(4, "City", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple(2).FFormatColumn(5, "Group Name", 200, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(3, "Interval", FGDataType.DT_Numeric, FilterCodeType.DTNumeric, 180, False)

        FSetValue(4, "Site Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, AgL.PubSiteName & "|'" & AgL.PubSiteCode & "'")
        FRH_Multiple(4) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable(
                          "Select 'o' As Tick,Sm.Code,Sm.Name From Sitemast Sm where code in (" & AgL.PubSiteList & ")   Order By Sm.Name",
                          AgL.GCn)), "", 300, 360, , , False, AgL.PubSiteCode)
        FRH_Multiple(4).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(4).FFormatColumn(1, , 0, , False)
        FRH_Multiple(4).FFormatColumn(2, "Name", 240, DataGridViewContentAlignment.MiddleLeft)
    End Sub
    Private Sub FBillWsOSAgeing(ByVal StrAmt1 As String, ByVal StrAmt2 As String, ByVal StrReportFor As String)
        Dim StrCondition1 As String, StrCondition2, STRDATE As String
        Dim DTTemp As DataTable
        Dim StrCnd As String = ""
        Dim D1 As Integer

        If Not FIsValid(0) Then Exit Sub
        DTTemp = CMain.FGetDatTable("SELECT GroupCode FROM AcGroup WHERE GroupName='" & StrReportFor & "'", AgL.GCn)

        If DTTemp.Rows.Count > 0 Then StrCnd = AgL.XNull(DTTemp.Rows(0).Item("GroupCode")) : DTTemp.Rows.Clear()
        STRDATE = AgL.Chk_Date(CDate(FGMain(GFilter, 0).Value.ToString).ToString("s"))
        StrCondition1 = " Where (LG.V_Date <= " & AgL.Chk_Date(CDate(FGMain(GFilter, 0).Value.ToString).ToString("s")) & " And IfNull(LG." & StrAmt1 & ",0)>0) And (SG.GroupCode In (SELECT AGP.GroupCode FROM AcGroupPath AGP WHERE AGP.GroupUnder='" & StrCnd & "') Or SG.GroupCode='" & StrCnd & "') "
        StrCondition2 = " Where (LG.V_Date <= " & AgL.Chk_Date(CDate(FGMain(GFilter, 0).Value.ToString).ToString("s")) & ") And IfNull(LG." & StrAmt2 & ",0)>0 And IfNull(LG." & StrAmt2 & ",0)-IfNull(T.AMOUNT,0)<>0 And (SG.GroupCode In (SELECT AGP.GroupCode FROM AcGroupPath AGP WHERE AGP.GroupUnder='" & StrCnd & "') Or SG.GroupCode='" & StrCnd & "') "

        If Trim(FGMain(GFilterCode, 1).Value) <> "" Then StrCondition1 = StrCondition1 & " And (IfNull(SG.GroupCode,'') In (Select IfNull(AGP.GroupCode,'') From AcGroupPath AGP Where AGP.GroupUnder In (" & FGMain(GFilterCode, 1).Value & ")) Or SG.GroupCode In (" & FGMain(GFilterCode, 1).Value & ")) "
        If Trim(FGMain(GFilterCode, 1).Value) <> "" Then StrCondition2 = StrCondition2 & " And (IfNull(SG.GroupCode,'') In (Select IfNull(AGP.GroupCode,'') From AcGroupPath AGP Where AGP.GroupUnder In (" & FGMain(GFilterCode, 1).Value & ")) Or SG.GroupCode In (" & FGMain(GFilterCode, 1).Value & ")) "

        If Trim(FGMain(GFilterCode, 2).Value) <> "" Then StrCondition1 = StrCondition1 & " And LG.SubCode In (" & FGMain(GFilterCode, 2).Value & ")"
        If Trim(FGMain(GFilterCode, 2).Value) <> "" Then StrCondition2 = StrCondition2 & " And LG.SubCode In (" & FGMain(GFilterCode, 2).Value & ")"

        If Trim(FGMain(GFilterCode, 4).Value) <> "" Then
            StrCondition1 = StrCondition1 & " And  LG.Site_Code IN (" & FGMain(GFilterCode, 4).Value & ") "
            StrCondition2 = StrCondition2 & " And  LG.Site_Code IN (" & FGMain(GFilterCode, 4).Value & ") "
        Else
            StrCondition1 = StrCondition1 & " And  LG.Site_Code IN (" & AgL.PubSiteList & ") "
            StrCondition2 = StrCondition2 & " And  LG.Site_Code IN (" & AgL.PubSiteList & ") "
        End If
        D1 = Val((FGMain(GFilter, 3).Value.ToString))

        StrSQLQuery = "Select LG.DocId,LG.V_SNo,Cast(Max(LG.V_No) as Varchar) as VNo,Max(LG.V_Type) as VType,LG.V_Date as VDate,Max(SG.Name) As PName,"
        StrSQLQuery = StrSQLQuery + "Max(LG.SubCode) as SubCode,Max(LG.Narration) as Narration,Max(LG." & StrAmt1 & ") as Amt1,0 As Amt2,IfNull(Sum(LA.Amount),0) as Amt, "
        StrSQLQuery = StrSQLQuery + "Max(SG.Address)As Add1,Null As Add2,Max(C.CityName)As CityName,'India' as Country,MAx(St.name) As SiteName,max(Ag.GroupName) as AcGroupName, "
        StrSQLQuery = StrSQLQuery + "(CASE WHEN julianday(" & STRDATE & ")  - julianday(Max(LG.V_Date))>= 0 AND  julianday(" & STRDATE & ")  - julianday(Max(LG.V_Date))<=" & D1 & " THEN  Max(LG.AmtDr)-IfNull(Sum(LA.Amount),0) ELSE 0 end) AS AmtDay1, "
        StrSQLQuery = StrSQLQuery + "(CASE WHEN julianday(" & STRDATE & ")  - julianday(Max(LG.V_Date))>" & D1 & " THEN  Max(LG.AmtDr)-IfNull(Sum(LA.Amount),0) ELSE 0 end) AS AmtDay2," & D1 & " As Days  "
        StrSQLQuery = StrSQLQuery + "From Ledger LG Left Join SubGroup SG On LG.Subcode=SG.SubCode Left Join "
        StrSQLQuery = StrSQLQuery + "City C on SG.CityCode=C.CityCode LEFT JOIN AcGroup AG ON SG.GroupCode =AG.GroupCode  "
        StrSQLQuery = StrSQLQuery + "Left Join LedgerAdj LA On LG.DocId=LA.Adj_DocID  And LG.V_SNo=LA.Adj_V_SNo "
        StrSQLQuery = StrSQLQuery + "LEFT JOIN SiteMast ST ON LG.Site_Code =St.code  "
        StrSQLQuery = StrSQLQuery + StrCondition1
        StrSQLQuery = StrSQLQuery + "Group By LG.DocId,LG.V_SNo "
        StrSQLQuery = StrSQLQuery + "HAVING(IfNull(Sum(LA.Amount), 0) <> Max(LG." & StrAmt1 & "))"
        StrSQLQuery = StrSQLQuery + "Union All "
        StrSQLQuery = StrSQLQuery + "Select	LG.DocId,LG.V_SNo,LG.DivCode || LG.Site_Code || '-' || LG.V_Type || '-' || LG.RecId As V_No,LG.V_Type,LG.V_Date,SG.Name As PName,LG.SubCode, "
        StrSQLQuery = StrSQLQuery + "LG.Narration,0 As Amt1,IfNull(LG." & StrAmt2 & ",0)-IfNull(T.AMOUNT,0) as Amt2,0 As Amount,Null As Add1,Null As Add2,"
        StrSQLQuery = StrSQLQuery + "Null As CityName,Null As Country,ST.name As sitename,IfNull(Ag.GroupName,'') as AcGroupName,0 AS AmtDay1,0 AS AmtDay2,0 As Days "
        StrSQLQuery = StrSQLQuery + "From Ledger LG Left Join SubGroup SG On SG.SubCode=LG.SubCode LEFT JOIN AcGroup AG ON SG.GroupCode =AG.GroupCode  LEFT JOIN SiteMast ST ON LG.Site_Code =St.code   "
        StrSQLQuery = StrSQLQuery + "LEFT JOIN (SELECT LA.Vr_Docid AS Docid,LA.Vr_V_SNo AS S_No,SUM(AMOUNT) AS AMOUNT FROM LedgerAdj LA GROUP BY LA.Vr_DocId,LA.Vr_V_SNo) T ON T.DOCID=LG.DOCID AND T.S_NO=LG.V_SNO  "
        StrSQLQuery = StrSQLQuery + StrCondition2

        StrSQLQuery = AgL.GetBackendBasedQuery(StrSQLQuery)
        DTTemp = CMain.FGetDatTable(StrSQLQuery, AgL.GCn)
        If Not DTTemp.Rows.Count > 0 Then MsgBox("No Records Found to Print.") : Exit Sub
        FLoadMainReport("BillwiseOutstandingAgeing", DTTemp)

        CMain.FormulaSet(RptMain, Me.Text, FGMain)
        CMain.FShowReport(RptMain, Me.MdiParent, Me.Text)
    End Sub

    Private Sub FBillWsOSAgeingSqlServer(ByVal StrAmt1 As String, ByVal StrAmt2 As String, ByVal StrReportFor As String)
        Dim StrCondition1 As String, StrCondition2, STRDATE As String
        Dim DTTemp As DataTable
        Dim StrCnd As String = ""
        Dim D1 As Integer

        If Not FIsValid(0) Then Exit Sub
        DTTemp = CMain.FGetDatTable("SELECT GroupCode FROM AcGroup WHERE GroupName='" & StrReportFor & "'", AgL.GCn)

        If DTTemp.Rows.Count > 0 Then StrCnd = AgL.XNull(DTTemp.Rows(0).Item("GroupCode")) : DTTemp.Rows.Clear()
        STRDATE = AgL.Chk_Date(FGMain(GFilter, 0).Value.ToString)
        StrCondition1 = " Where (LG.V_Date <= " & AgL.Chk_Date(FGMain(GFilter, 0).Value.ToString) & " And IsNull(LG." & StrAmt1 & ",0)>0) And (SG.GroupCode In (SELECT AGP.GroupCode FROM AcGroupPath AGP WHERE AGP.GroupUnder='" & StrCnd & "') Or SG.GroupCode='" & StrCnd & "') "
        StrCondition2 = " Where (LG.V_Date <= " & AgL.Chk_Date(FGMain(GFilter, 0).Value.ToString) & ") And IsNull(LG." & StrAmt2 & ",0)>0 And IsNull(LG." & StrAmt2 & ",0)-ISNULL(T.AMOUNT,0)<>0 And (SG.GroupCode In (SELECT AGP.GroupCode FROM AcGroupPath AGP WHERE AGP.GroupUnder='" & StrCnd & "') Or SG.GroupCode='" & StrCnd & "') "

        If Trim(FGMain(GFilterCode, 1).Value) <> "" Then StrCondition1 = StrCondition1 & " And (IsNull(SG.GroupCode,'') In (Select IsNull(AGP.GroupCode,'') From AcGroupPath AGP Where AGP.GroupUnder In (" & FGMain(GFilterCode, 1).Value & ")) Or SG.GroupCode In (" & FGMain(GFilterCode, 1).Value & ")) "
        If Trim(FGMain(GFilterCode, 1).Value) <> "" Then StrCondition2 = StrCondition2 & " And (IsNull(SG.GroupCode,'') In (Select IsNull(AGP.GroupCode,'') From AcGroupPath AGP Where AGP.GroupUnder In (" & FGMain(GFilterCode, 1).Value & ")) Or SG.GroupCode In (" & FGMain(GFilterCode, 1).Value & ")) "

        If Trim(FGMain(GFilterCode, 2).Value) <> "" Then StrCondition1 = StrCondition1 & " And LG.SubCode In (" & FGMain(GFilterCode, 2).Value & ")"
        If Trim(FGMain(GFilterCode, 2).Value) <> "" Then StrCondition2 = StrCondition2 & " And LG.SubCode In (" & FGMain(GFilterCode, 2).Value & ")"

        If Trim(FGMain(GFilterCode, 4).Value) <> "" Then
            StrCondition1 = StrCondition1 & " And  LG.Site_Code IN (" & FGMain(GFilterCode, 4).Value & ") "
            StrCondition2 = StrCondition2 & " And  LG.Site_Code IN (" & FGMain(GFilterCode, 4).Value & ") "
        Else
            StrCondition1 = StrCondition1 & " And  LG.Site_Code IN (" & AgL.PubSiteList & ") "
            StrCondition2 = StrCondition2 & " And  LG.Site_Code IN (" & AgL.PubSiteList & ") "
        End If
        D1 = Val((FGMain(GFilter, 3).Value.ToString))

        StrSQLQuery = "Select LG.DocId,LG.V_SNo,Convert(Varchar,Max(LG.V_No)) as VNo,Max(LG.V_Type) as VType,Max(LG.V_Date) as VDate,Max(SG.Name) As PName,"
        StrSQLQuery = StrSQLQuery + "Max(LG.SubCode) as SubCode,Max(LG.Narration) as Narration,Max(LG." & StrAmt1 & ") as Amt1,0 As Amt2,IsNull(Sum(LA.Amount),0) as Amt, "
        StrSQLQuery = StrSQLQuery + "Max(SG.Address)As Add1,'' As Add2,Max(C.CityName)As CityName,'India' as Country,MAx(St.name) As SiteName,max(Ag.GroupName) as AcGroupName, "
        StrSQLQuery = StrSQLQuery + "(CASE WHEN DateDiff(Day,Max(LG.V_Date), " & STRDATE & "  )>= 0 AND  DateDiff(Day,Max(LG.V_Date)," & STRDATE & " )<=" & D1 & " THEN  Max(LG.AmtDr)-IsNull(Sum(LA.Amount),0) ELSE 0 end) AS AmtDay1, "
        StrSQLQuery = StrSQLQuery + "(CASE WHEN DateDiff(Day,Max(LG.V_Date)," & STRDATE & " )>" & D1 & " THEN  Max(LG.AmtDr)-IsNull(Sum(LA.Amount),0) ELSE 0 end) AS AmtDay2," & D1 & " As Days  "
        StrSQLQuery = StrSQLQuery + "From Ledger LG Left Join SubGroup SG On LG.Subcode=SG.SubCode Left Join "
        StrSQLQuery = StrSQLQuery + "City C on SG.CityCode=C.CityCode LEFT JOIN AcGroup AG ON SG.GroupCode =AG.GroupCode  "
        StrSQLQuery = StrSQLQuery + "Left Join LedgerAdj LA On LG.DocId=LA.Adj_DocID  And LG.V_SNo=LA.Adj_V_SNo "
        StrSQLQuery = StrSQLQuery + "LEFT JOIN SiteMast ST ON LG.Site_Code =St.code  "
        StrSQLQuery = StrSQLQuery + StrCondition1
        StrSQLQuery = StrSQLQuery + "Group By LG.DocId,LG.V_SNo "
        StrSQLQuery = StrSQLQuery + "HAVING(IsNull(Sum(LA.Amount), 0) <> Max(LG." & StrAmt1 & "))"
        StrSQLQuery = StrSQLQuery + "Union All "
        StrSQLQuery = StrSQLQuery + "Select	LG.DocId,LG.V_SNo,Convert(Varchar,LG.V_No) As V_No,LG.V_Type,LG.V_Date,SG.Name As PName,LG.SubCode, "
        StrSQLQuery = StrSQLQuery + "LG.Narration,0 As Amt1,ISNULL(LG." & StrAmt2 & ",0)-ISNULL(T.AMOUNT,0) as Amt2,0 As Amount,Null As Add1,Null As Add2,"
        StrSQLQuery = StrSQLQuery + "Null As CityName,Null As Country,ST.name As sitename,isnull(Ag.GroupName,'') as AcGroupName,0 AS AmtDay1,0 AS AmtDay2,0 As Days "
        StrSQLQuery = StrSQLQuery + "From Ledger LG Left Join SubGroup SG On SG.SubCode=LG.SubCode LEFT JOIN AcGroup AG ON SG.GroupCode =AG.GroupCode  LEFT JOIN SiteMast ST ON LG.Site_Code =St.code   "
        StrSQLQuery = StrSQLQuery + "LEFT JOIN (SELECT LA.Vr_Docid AS Docid,LA.Vr_V_SNo AS S_No,SUM(AMOUNT) AS AMOUNT FROM LedgerAdj LA GROUP BY LA.Vr_DocId,LA.Vr_V_SNo) T ON T.DOCID=LG.DOCID AND T.S_NO=LG.V_SNO  "
        StrSQLQuery = StrSQLQuery + StrCondition2

        StrSQLQuery = AgL.GetBackendBasedQuery(StrSQLQuery)
        DTTemp = CMain.FGetDatTable(StrSQLQuery, AgL.GCn)
        If Not DTTemp.Rows.Count > 0 Then MsgBox("No Records Found to Print.") : Exit Sub
        FLoadMainReport("BillwiseOutstandingAgeing", DTTemp)

        CMain.FormulaSet(RptMain, Me.Text, FGMain)
        CMain.FShowReport(RptMain, Me.MdiParent, Me.Text)
    End Sub

    Private Sub FLedger()
        Dim StrCondition1 As String, StrConditionOP As String, StrConditionsite As String
        Dim DTTemp As DataTable
        Dim I As Integer

        If Not FIsValid(0) Then Exit Sub
        If Not FIsValid(1) Then Exit Sub

        StrCondition1 = " Where ( Date(LG.V_Date) Between " & AgL.Chk_Date(CDate(FGMain(GFilter, 0).Value.ToString).ToString("s")) & " And " & AgL.Chk_Date(CDate(FGMain(GFilter, 1).Value.ToString).ToString("s")) & ") "
        StrConditionOP = " Where Sg.GroupNature In ('A','L') And Date(LG.V_Date) < " & AgL.Chk_Date(CDate(FGMain(GFilter, 0).Value.ToString).ToString("s")) & " "

        If Trim(FGMain(GFilterCode, 2).Value) <> "" Then StrCondition1 = StrCondition1 & " And (IfNull(SG.GroupCode,'') In (Select IfNull(AGP.GroupCode,'') From AcGroupPath AGP Where AGP.GroupUnder In (" & FGMain(GFilterCode, 2).Value & ")) Or SG.GroupCode In (" & FGMain(GFilterCode, 2).Value & ")) "
        If Trim(FGMain(GFilterCode, 2).Value) <> "" Then StrConditionOP = StrConditionOP & " And (IfNull(SG.GroupCode,'') In (Select IfNull(AGP.GroupCode,'') From AcGroupPath AGP Where AGP.GroupUnder In (" & FGMain(GFilterCode, 2).Value & ")) Or SG.GroupCode In (" & FGMain(GFilterCode, 2).Value & ")) "
        If Trim(FGMain(GFilterCode, 3).Value) <> "" Then StrCondition1 = StrCondition1 & " And LG.SubCode In (" & FGMain(GFilterCode, 3).Value & ")"
        If Trim(FGMain(GFilterCode, 3).Value) <> "" Then StrConditionOP = StrConditionOP & " And LG.SubCode In (" & FGMain(GFilterCode, 3).Value & ")"
        If Trim(FGMain(GFilterCode, 4).Value) <> "" Then StrCondition1 = StrCondition1 & " And LG.V_Type In (" & FGMain(GFilterCode, 4).Value & ")"
        If Trim(FGMain(GFilterCode, 4).Value) <> "" Then StrConditionOP = StrConditionOP & " And LG.V_Type In (" & FGMain(GFilterCode, 4).Value & ")"

        StrConditionsite = ""
        If Trim(FGMain(GFilterCode, 5).Value) <> "" Then
            StrConditionsite += " and LG.site_Code In (" & FGMain(GFilterCode, 5).Value & ") "
        Else
            StrConditionsite += " and LG.site_Code In  (" & AgL.PubSiteList & ") "
        End If

        If Trim(FGMain(GFilterCode, 6).Value) <> "" Then
            StrConditionsite += " and LG.DivCode In (" & FGMain(GFilterCode, 6).Value & ") "
        Else
            StrConditionsite += " and LG.DivCode In  (" & AgL.PubSiteList & ") "
        End If


        '========== For Detail Section =======
        StrSQLQuery = "Select	LG.V_Type, LG.DivCode || LG.Site_Code || '-' || LG.V_Type " & IIf(FGMain(GFilter, 9).Value = "No", "", "|| '-' || LG.RecId") & " As V_No,"
        StrSQLQuery = StrSQLQuery + "LG.V_Date,LG.V_Prefix,SG.Name  As PName,LG.SubCode, "
        'StrSQLQuery = StrSQLQuery + "LG.Narration || Case When LG.Chq_No Is Not Null Then 'Cheque No.' || LG.Chq_No Else '' End || Case When LG.Chq_No Is Not Null Then 'Cheque Date ' || Cast(LG.Chq_Date As nvarchar) Else '' End As Narration , "
        StrSQLQuery = StrSQLQuery + "LG.Narration, "
        StrSQLQuery = StrSQLQuery + "LG.AmtDr,LG.AmtCr,1 As SNo,SM.Name As Division,LG.ContraText As ContraName,LG.Chq_No,LG.Chq_Date,"
        StrSQLQuery = StrSQLQuery + "IfNull(C.CityName,'') as PCity,IfNull(LG.Site_Code,'') As Site_Code "
        StrSQLQuery = StrSQLQuery + "From Ledger LG Left Join SubGroup SG On LG.Subcode=SG.SubCode "
        StrSQLQuery = StrSQLQuery + "Left Join Sitemast SM On LG.Site_Code=SM.Code "
        StrSQLQuery = StrSQLQuery + "Left Join City C On C.CityCode=SG.CityCode "

        StrSQLQuery = StrSQLQuery + StrCondition1 + StrConditionsite
        StrSQLQuery = StrSQLQuery + "Union All "

        '======= For Opening Balance =========
        StrSQLQuery = StrSQLQuery + "Select	Null As V_Type,Null As V_No,Null As V_Date,Null As V_Prefix, "
        StrSQLQuery = StrSQLQuery + "max(SG.Name)   As PName,LG.SubCode,'OPENING BALANCE' As Narration, "
        StrSQLQuery = StrSQLQuery + "(Case When (IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LG.AmtCr),0))>0 Then  "
        StrSQLQuery = StrSQLQuery + "(IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LG.AmtCr),0)) Else 0 End) As AmtDr, "
        StrSQLQuery = StrSQLQuery + "(Case When (IfNull(Sum(LG.AmtCr),0)-IfNull(Sum(LG.AmtDr),0))>0 Then "
        StrSQLQuery = StrSQLQuery + "(IfNull(Sum(LG.AmtCr),0)-IfNull(Sum(LG.AmtDr),0)) Else 0 End) As AmtCr,"
        StrSQLQuery = StrSQLQuery + "0 As SNo,max(SM.name) as Division,Null As ContraName,Null As Chq_No,Null As Chq_Date,"
        StrSQLQuery = StrSQLQuery + "IfNull(max(C.CityName),'') as PCity,Null As Site_Code "
        StrSQLQuery = StrSQLQuery + "From Ledger LG Left Join SubGroup SG On LG.Subcode=SG.SubCode "
        StrSQLQuery = StrSQLQuery + "Left Join Sitemast SM On LG.Site_Code=SM.Code "
        StrSQLQuery = StrSQLQuery + "Left Join City C On C.CityCode=SG.CityCode "
        StrSQLQuery = StrSQLQuery + StrConditionOP + StrConditionsite

        StrSQLQuery = StrSQLQuery + "Group By LG.SubCode "
        StrSQLQuery = StrSQLQuery + "Order By PName,V_Date,V_Type,V_No,SNo "

        StrSQLQuery = AgL.GetBackendBasedQuery(StrSQLQuery)
        DTTemp = CMain.FGetDatTable(StrSQLQuery, AgL.GCn)

        If Not DTTemp.Rows.Count > 0 Then MsgBox("No Records Found to Print.") : Exit Sub

        FLoadMainReport("Ledger", DTTemp)

        For I = 0 To RptMain.DataDefinition.FormulaFields.Count - 1
            Select Case (UCase(RptMain.DataDefinition.FormulaFields.Item(I).Name))
                Case UCase("FrmIndexNeeded")
                    RptMain.DataDefinition.FormulaFields.Item(I).Text = "'" & IIf(Trim(FGMain(GFilterCode, 7).Value) = "", "N", Trim(FGMain(GFilterCode, 7).Value)) & "'"
                Case UCase("Contraneeded")
                    RptMain.DataDefinition.FormulaFields.Item(I).Text = "'" & Trim(FGMain(GFilterCode, 8).Value) & "'"
            End Select
        Next

        CMain.FormulaSet(RptMain, Me.Text, FGMain)
        CMain.FShowReport(RptMain, Me.MdiParent, Me.Text)
    End Sub
    Private Sub FJournal()
        Dim StrCondition1 As String
        Dim DTTemp As DataTable

        If Not FIsValid(0) Then Exit Sub

        StrCondition1 = " Where Date(LG.V_Date) Between " & AgL.Chk_Date(CDate(FGMain(GFilter, 0).Value.ToString).ToString("s")) & " and  " & AgL.Chk_Date(CDate(FGMain(GFilter, 1).Value.ToString).ToString("s")) & " And VType.Category='JV' "
        If Trim(FGMain(GFilterCode, 2).Value) <> "" Then StrCondition1 = StrCondition1 & " And LG.V_type In (" & FGMain(GFilterCode, 2).Value & ")"

        If Trim(FGMain(GFilterCode, 3).Value) <> "" Then
            StrCondition1 = StrCondition1 & "  And LG.Site_Code IN (" & FGMain(GFilterCode, 3).Value & ") "
        Else
            StrCondition1 = StrCondition1 & " And LG.Site_Code IN (" & AgL.PubSiteList & ") "
        End If

        If Trim(FGMain(GFilterCode, 4).Value) <> "" Then
            StrCondition1 = StrCondition1 & "  And LG.DivCode IN (" & FGMain(GFilterCode, 4).Value & ") "
        Else
            StrCondition1 = StrCondition1 & " And LG.DivCode IN (" & AgL.PubSiteList & ") "
        End If


        StrSQLQuery = "Select LG.V_date,LG.Amtcr,LG.AmtDr,LG.V_type,LG.DivCode || LG.Site_Code || '-' || LG.V_Type || '-'  || LG.RecId As V_no,LG.V_prefix as V_add,LG.Chq_No, "
        StrSQLQuery = StrSQLQuery + "LG.Chq_Date,LG.Narration As narr,LG.V_Sno,LedgerM.Narration As mnarration,LG.Docid,SG.Name As Name,St.name As SiteName ,LG.Site_Code "
        StrSQLQuery = StrSQLQuery + "FROM Ledger LG LEFT  JOIN  LedgerM ON LG.DocId = LedgerM.DocId "
        StrSQLQuery = StrSQLQuery + "Left Join Subgroup SG On SG.Subcode=LG.Subcode "
        StrSQLQuery = StrSQLQuery + "Left join Voucher_type VType on Vtype.V_Type=LG.V_Type "
        StrSQLQuery = StrSQLQuery + "LEFT JOIN SiteMast ST ON LG.Site_Code =St.code "
        StrSQLQuery = StrSQLQuery + StrCondition1
        StrSQLQuery = StrSQLQuery + "Order By LG.V_DATE,LG.V_TYPE, LG.RecId,LG.V_No,LG.V_SNO"

        StrSQLQuery = AgL.GetBackendBasedQuery(StrSQLQuery)
        DTTemp = CMain.FGetDatTable(StrSQLQuery, AgL.GCn)

        If Not DTTemp.Rows.Count > 0 Then MsgBox("No Records Found to Print.") : Exit Sub

        FLoadMainReport("Journal", DTTemp)
        CMain.FormulaSet(RptMain, Me.Text, FGMain)
        CMain.FShowReport(RptMain, Me.MdiParent, Me.Text)
    End Sub
    Private Sub FDayBook()
        Dim StrCondition1 As String
        Dim DTTemp As DataTable

        If Not FIsValid(0) Then Exit Sub

        StrCondition1 = " Where Date(LG.V_Date) Between " & AgL.Chk_Date(CDate(FGMain(GFilter, 0).Value.ToString).ToString("s")) & " and  " & AgL.Chk_Date(CDate(FGMain(GFilter, 1).Value.ToString).ToString("s")) & " "

        If Trim(FGMain(GFilterCode, 2).Value) <> "" Then StrCondition1 = StrCondition1 & " And LG.V_type In (" & FGMain(GFilterCode, 2).Value & ")"

        If Trim(FGMain(GFilterCode, 3).Value) <> "" Then
            StrCondition1 = StrCondition1 & " And  LG.Site_Code  IN (" & FGMain(GFilterCode, 3).Value & ") "
        Else
            StrCondition1 = StrCondition1 & " And  LG.Site_Code  IN (" & AgL.PubSiteList & ") "
        End If

        If Trim(FGMain(GFilterCode, 4).Value) <> "" Then
            StrCondition1 = StrCondition1 & " And  LG.DivCode  IN (" & FGMain(GFilterCode, 4).Value & ") "
        Else
            StrCondition1 = StrCondition1 & " And  LG.DivCode  IN (" & AgL.PubDivisionList & ") "
        End If

        StrSQLQuery = "Select LG.V_date,LG.Amtcr,LG.AmtDr,LG.V_type,LG.DivCode || LG.Site_Code || '-' || LG.V_Type || '-' || LG.RecId As V_no,LG.V_prefix as V_add,LG.Chq_No, "
        StrSQLQuery = StrSQLQuery + "LG.Chq_Date,LG.Narration As narr,LG.V_Sno,LedgerM.Narration As mnarration,LG.Docid,SG.Name As Name,St.name As SiteName,LG.Site_Code "
        StrSQLQuery = StrSQLQuery + "FROM Ledger LG LEFT  JOIN  LedgerM ON LG.DocId = LedgerM.DocId "
        StrSQLQuery = StrSQLQuery + " Left Join Subgroup SG On SG.Subcode=LG.Subcode "
        StrSQLQuery = StrSQLQuery + "Left join Voucher_type VType on Vtype.V_Type=LG.V_Type "
        StrSQLQuery = StrSQLQuery + "LEFT JOIN SiteMast ST ON LG.Site_Code =St.code"
        StrSQLQuery = StrSQLQuery + StrCondition1
        StrSQLQuery = StrSQLQuery + "Order By LG.V_DATE,LG.V_TYPE, LG.RecId, LG.V_No, LG.V_SNO"

        StrSQLQuery = AgL.GetBackendBasedQuery(StrSQLQuery)
        DTTemp = CMain.FGetDatTable(StrSQLQuery, AgL.GCn)

        If Not DTTemp.Rows.Count > 0 Then MsgBox("No Records Found to Print.") : Exit Sub
        FLoadMainReport("Journal", DTTemp)

        CMain.FormulaSet(RptMain, Me.Text, FGMain)
        CMain.FShowReport(RptMain, Me.MdiParent, Me.Text)
    End Sub
    Private Sub FTrialGroup()
        Dim StrCondition1 As String
        Dim DTTemp As DataTable

        If Not FIsValid(0) Then Exit Sub

        StrCondition1 = " Where LG.V_Date <= " & AgL.Chk_Date(CDate(FGMain(GFilter, 0).Value.ToString).ToString("s")) & " "
        If Trim(FGMain(GFilterCode, 1).Value) <> "" Then
            StrCondition1 += " And LG.Site_Code In (" & FGMain(GFilterCode, 1).Value & ") "
        Else
            StrCondition1 += " And LG.Site_Code In  (" & AgL.PubSiteList & ") "
        End If


        If Trim(FGMain(GFilterCode, 2).Value) <> "" Then
            StrCondition1 += " And LG.DivCode In (" & FGMain(GFilterCode, 2).Value & ") "
        Else
            StrCondition1 += " And LG.DivCode In  (" & AgL.PubSiteList & ") "
        End If

        '========== For Detail Section =======
        StrSQLQuery = "Select	(Case IfNull(AG1.GroupCode,'') When '' Then IfNull(AG.GroupCode,'') "
        StrSQLQuery = StrSQLQuery + "Else IfNull(AG1.GroupCode,'') End)  As GroupCode, "
        StrSQLQuery = StrSQLQuery + "Max(Case IfNull(AG1.GroupName,'') When '' Then IfNull(AG.GroupName,'') "
        StrSQLQuery = StrSQLQuery + "Else IfNull(AG1.GroupName,'') End)  As GName, "
        StrSQLQuery = StrSQLQuery + "(Case When (IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LG.AmtCr),0))>0 Then  "
        StrSQLQuery = StrSQLQuery + "(IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LG.AmtCr),0)) Else 0 End) As AmtDr, "
        StrSQLQuery = StrSQLQuery + "(Case When (IfNull(Sum(LG.AmtCr),0)-IfNull(Sum(LG.AmtDr),0))>0 Then "
        StrSQLQuery = StrSQLQuery + "(IfNull(Sum(LG.AmtCr),0)-IfNull(Sum(LG.AmtDr),0)) Else 0 End) As AmtCr "
        StrSQLQuery = StrSQLQuery + "From Ledger LG Left Join SubGroup SG On LG.SubCode=SG.SubCode  Left Join "
        StrSQLQuery = StrSQLQuery + "AcGroup AG On AG.GroupCode=SG.GroupCode Left Join "
        StrSQLQuery = StrSQLQuery + "AcGroupPath AGP On AGP.GroupCode=AG.GroupCode And AGP.SNo=1 Left Join "
        StrSQLQuery = StrSQLQuery + "AcGroup AG1 On AG1.GroupCode=AGP.GroupUnder "
        StrSQLQuery = StrSQLQuery + StrCondition1

        StrSQLQuery = StrSQLQuery + "Group By (Case IfNull(AG1.GroupCode,'') When '' Then IfNull(AG.GroupCode,'') "
        StrSQLQuery = StrSQLQuery + "Else IfNull(AG1.GroupCode,'') End) "
        StrSQLQuery = StrSQLQuery + "Having (IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LG.AmtCr),0)) <> 0 "
        StrSQLQuery = StrSQLQuery + "Order By Max(Case IfNull(AG1.GroupName,'') When '' Then IfNull(AG.GroupName,'') "
        StrSQLQuery = StrSQLQuery + "Else IfNull(AG1.GroupName,'') End) "

        StrSQLQuery = AgL.GetBackendBasedQuery(StrSQLQuery)
        DTTemp = CMain.FGetDatTable(StrSQLQuery, AgL.GCn)
        If Not DTTemp.Rows.Count > 0 Then MsgBox("No Records Found to Print.") : Exit Sub
        FLoadMainReport("TrialGroup", DTTemp)
        CMain.FormulaSet(RptMain, Me.Text, FGMain)
        CMain.FShowReport(RptMain, Me.MdiParent, Me.Text)
    End Sub
    Private Sub FTrialDetail()
        Dim FrmObj As FrmAcGroupPositioning
        If Not FIsValid(0) Then Exit Sub
        If Not FIsValid(2) Then Exit Sub

        If UCase(Trim(FGMain(GFilterCode, 3).Value)) = "M" Then
            If MsgBox("Do You Want To Set Account Group Positioning?") = MsgBoxResult.Yes Then
                FrmObj = New FrmAcGroupPositioning()
                FrmObj.MdiParent = Me.MdiParent
                FrmObj.Show()
            Else
                FTrailDetail_Manual()
            End If
        Else
            FTrailDetail_Alphabatical()
        End If
    End Sub
    Private Sub FTrailDetail_Manual()
        Dim StrCondition1 As String
        Dim StrConditionZeroBal As String = ""
        Dim DTTemp As DataTable

        StrCondition1 = " And LG.V_Date <= " & AgL.Chk_Date(CDate(FGMain(GFilter, 0).Value.ToString).ToString("s")) & " "
        If Trim(FGMain(GFilterCode, 1).Value) <> "" Then
            StrCondition1 += " And LG.Site_Code In (" & FGMain(GFilterCode, 1).Value & ") "
        Else
            StrCondition1 += " And LG.Site_Code In  (" & AgL.PubSiteList & ") "
        End If

        If Trim(FGMain(GFilterCode, 2).Value) <> "" Then
            StrCondition1 += " And LG.DivCode In (" & FGMain(GFilterCode, 2).Value & ") "
        Else
            StrCondition1 += " And LG.DivCode In  (" & AgL.PubDivisionList & ") "
        End If


        If FGMain(GFilterCode, 4).Value = "N" Then
            StrConditionZeroBal = "Having (IfNull(Sum(Tbl.AmtDr),0)-IfNull(Sum(Tbl.AmtCr),0)) <> 0  "
        ElseIf FGMain(GFilterCode, 4).Value = "Y" Then
            StrConditionZeroBal = "Having (IfNull(Sum(Tbl.AmtDr),0)-IfNull(Sum(Tbl.AmtCr),0)) = 0  "
        Else
            StrConditionZeroBal = ""
        End If

        '================================= Upper Select Query =====================================
        StrSQLQuery = "Select	Space(IfNull(Max(Level),1)-1) + IfNull(Max(GroupName),'') As GroupName,GroupCode, "
        StrSQLQuery += "Space(IfNull(Max(Level),1)) + IfNull(Max(AcName),'') As AcName,AcCode, "
        StrSQLQuery += "(Case When IfNull(Sum(AmtDr),0)-IfNull(Sum(AmtCr),0)>0 Then "
        StrSQLQuery += "IfNull(Sum(AmtDr),0)-IfNull(Sum(AmtCr),0) Else 0 End) As AmtDr, "
        StrSQLQuery += "(Case When IfNull(Sum(AmtCr),0)-IfNull(Sum(AmtDr),0)>0 Then "
        StrSQLQuery += "IfNull(Sum(AmtCr),0)-IfNull(Sum(AmtDr),0) Else 0 End) As AmtCr, "
        StrSQLQuery += "Max(V_SNo) As V_SNo,Max(Level) As Level "
        StrSQLQuery += "From ("
        '==========================================================================================
        '==========================================================================================

        '===================== Main Ledger Fetching For Expandable Groups =========================
        StrSQLQuery += "Select 	AG.GroupName,AG.GroupCode, "
        StrSQLQuery += "(SG.Name || ' - ' || IfNull(CT.CityName,'')) As AcName,LG.SubCode AcCode,"
        StrSQLQuery += "LG.AmtDr,LG.AmtCr,AGP.V_SNo,AGP.Level "
        StrSQLQuery += "From Ledger LG Left Join "
        StrSQLQuery += "SubGroup SG On LG.SubCode=SG.SubCode Left Join "
        StrSQLQuery += "AcGroup AG On SG.GroupCode=AG.GroupCode Left Join "
        StrSQLQuery += "City CT On CT.CityCode=SG.CityCode Left Join "
        StrSQLQuery += "AcGroupPositioning AGP On AG.GroupCode=AGP.GroupCode And AGP.ExpandGroup='Y' "
        StrSQLQuery += "Where AGP.ExpandGroup='Y' " & StrCondition1
        StrSQLQuery += "Union All "
        '==========================================================================================
        '==========================================================================================

        '=================== Main Ledger Fetching For Non Expandable Groups =======================
        StrSQLQuery += "Select	AG.GroupName,AG.GroupCode,"
        StrSQLQuery += "AG.GroupName As AcName , "
        StrSQLQuery += "AG.GroupCode As AcCode,LG.AmtDr,LG.AmtCr,POS.V_SNo,POS.Level "
        StrSQLQuery += "From Ledger LG Left Join "
        StrSQLQuery += "SubGroup SG On LG.SubCode=SG.SubCode Left Join "
        StrSQLQuery += "(Select	GroupCode,MainGroup,Max(V_SNo) As V_SNo,Max(Level) As Level,"
        StrSQLQuery += "Max(ExpandGroup) As ExpandGroup "
        StrSQLQuery += "From "
        StrSQLQuery += "( "
        StrSQLQuery += "Select	AGP.GroupCode,AGP.GroupCode As MainGroup,AGP.V_SNo,AGP.Level,AGP.ExpandGroup "
        StrSQLQuery += "From AcGroupPositioning AGP "
        StrSQLQuery += "Where AGP.ExpandGroup='N' "
        StrSQLQuery += "Union All "
        StrSQLQuery += "Select	AP.GroupCode,AGP.GroupCode As MainGroup,AGP.V_SNo,AGP.Level,AGP.ExpandGroup "
        StrSQLQuery += "From AcGroupPath AP Left Join "
        StrSQLQuery += "AcGroupPositioning AGP On AP.GroupUnder=AGP.GroupCode And AGP.ExpandGroup='N' "
        StrSQLQuery += "Where IfNull(AGP.GroupCode,'')<>'' "
        StrSQLQuery += ") As Tmp "
        StrSQLQuery += "Group By GroupCode,MainGroup"
        StrSQLQuery += ") POS On POS.GroupCode=SG.GroupCode Left Join "
        StrSQLQuery += "AcGroup AG On POS.MainGroup=AG.GroupCode "
        StrSQLQuery += "Where  IfNull(POS.GroupCode,'')<>'' " & StrCondition1
        '==========================================================================================
        '==========================================================================================

        '================================= Lower Select Query =====================================
        StrSQLQuery += ") As Tbl "
        StrSQLQuery += "Group By GroupCode,AcCode "
        StrSQLQuery += StrConditionZeroBal
        StrSQLQuery += "Order By V_SNo,Max(GroupName),Max(AcName) "

        StrSQLQuery = AgL.GetBackendBasedQuery(StrSQLQuery)
        DTTemp = CMain.FGetDatTable(StrSQLQuery, AgL.GCn)
        If Not DTTemp.Rows.Count > 0 Then MsgBox("No Records Found to Print.") : Exit Sub
        FLoadMainReport("TrialDetailManual", DTTemp)
        CMain.FormulaSet(RptMain, Me.Text, FGMain)
        CMain.FShowReport(RptMain, Me.MdiParent, Me.Text)
    End Sub
    Private Sub FTrailDetail_Alphabatical()
        Dim StrCondition1 As String
        Dim StrConditionZeroBal As String = ""
        Dim DTTemp As DataTable
        Dim I As Int16
        Dim StrFieldName As String = "GroupName", StrSpace As String = "   ", StrFieldPrefix As String = ""
        Dim IntMaxHirarchy As Int16 = 10

        StrCondition1 = " Where LG.V_Date <= " & AgL.Chk_Date(CDate(FGMain(GFilter, 0).Value.ToString).ToString("s")) & " "
        If Trim(FGMain(GFilterCode, 1).Value) <> "" Then
            StrCondition1 += " And LG.Site_Code In (" & FGMain(GFilterCode, 1).Value & ") "
        Else
            StrCondition1 += " And LG.Site_Code In  (" & AgL.PubSiteList & ") "
        End If

        If Trim(FGMain(GFilterCode, 2).Value) <> "" Then
            StrCondition1 += " And LG.DivCode In (" & FGMain(GFilterCode, 2).Value & ") "
        Else
            StrCondition1 += " And LG.DivCode In  (" & AgL.PubDivisionList & ") "
        End If


        If FGMain(GFilterCode, 3).Value = "N" Then
            StrConditionZeroBal = "Having (IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LG.AmtCr),0)) <> 0  "
        ElseIf FGMain(GFilterCode, 3).Value = "Y" Then
            StrConditionZeroBal = "Having (IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LG.AmtCr),0)) = 0  "
        Else
            StrConditionZeroBal = ""
        End If

        StrSQLQuery = "Select  IfNull((Select Max(AG1.GroupName) "
        StrSQLQuery += "From AcGroupPath AGP Left Join "
        StrSQLQuery += "AcGroup AG1 On AG1.GroupCode=AGP.GroupUnder And AGP.SNo=" & 1 & " "
        StrSQLQuery += "Where AGP.GroupCode=Max(SG.GroupCode)), "
        StrSQLQuery += "(Case When (Select IfNull(Max(SNo),0) From AcGroupPath AGP1 "
        StrSQLQuery += "Where AGP1.GroupCode=Max(SG.GroupCode))= " & 0 & " "
        StrSQLQuery += "Then Max(AG.GroupName) Else '' End)) As " & StrFieldName + Trim(1) & " , "

        DTTemp = CMain.FGetDatTable("Select IfNull(Max(SNo),0) From AcGroupPath", AgL.GCn)
        If DTTemp.Rows(0).Item(0) > (IntMaxHirarchy - 1) Then MsgBox("There Can Be Only " & IntMaxHirarchy - 1 & " A/c Group Levels. Levels Are Exceding.") : Exit Sub
        For I = 2 To DTTemp.Rows(0).Item(0) + 1
            StrFieldPrefix += StrSpace
            StrSQLQuery += "IfNull((Select '" & StrFieldPrefix & "' || Max(AG1.GroupName) "
            StrSQLQuery += "From AcGroupPath AGP Left Join "
            StrSQLQuery += "AcGroup AG1 On AG1.GroupCode=AGP.GroupUnder And AGP.SNo=" & I & " "
            StrSQLQuery += "Where AGP.GroupCode=Max(SG.GroupCode)), "
            StrSQLQuery += "(Case When (Select IfNull(Max(SNo),0) From AcGroupPath AGP1 "
            StrSQLQuery += "Where AGP1.GroupCode=Max(SG.GroupCode))= " & I - 1 & " "
            StrSQLQuery += "Then '" & StrFieldPrefix & "' || Max(AG.GroupName) Else '' End)) As "
            StrSQLQuery += StrFieldName + Trim(I) & " , "
        Next

        For I = DTTemp.Rows(0).Item(0) + 2 To IntMaxHirarchy
            StrSQLQuery += "' ' As " & StrFieldName + Trim(I) & " , "
        Next

        StrSQLQuery += "(SG.Name || ' - ' || IfNull(CT.CityName,'')) As Name, "
        StrSQLQuery += "(Case When IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LG.AmtCr),0)>0 Then "
        StrSQLQuery += "IfNull(Sum(Case When AG.GroupNature In ('A','L') Or Date(LG.V_Date)>=" & AgL.Chk_Text(AgL.PubStartDate) & " Then LG.AmtDr-LG.AmtCr Else 0 End),0) Else 0 End) As AmtDr, "
        StrSQLQuery += "(Case When IfNull(Sum(LG.AmtCr),0)-IfNull(Sum(LG.AmtDr),0)>0 Then "
        StrSQLQuery += "IfNull(Sum(Case When AG.GroupNature In ('A','L') Or Date(LG.V_Date)>=" & AgL.Chk_Text(AgL.PubStartDate) & " Then LG.AmtCr-LG.AmtDr Else 0 End),0) Else 0 End) As AmtCr "
        StrSQLQuery += "From "
        StrSQLQuery += "Ledger LG Left Join "
        StrSQLQuery += "SubGroup SG On LG.SubCode=SG.SubCode Left Join "
        StrSQLQuery += "City CT On CT.CityCode=SG.CityCode Left Join "
        StrSQLQuery += "AcGroup AG On AG.GroupCode=SG.GroupCode "
        StrSQLQuery += StrCondition1
        StrSQLQuery += "Group By SG.Name || ' - ' || IfNull(CT.CityName,'') "
        StrSQLQuery += StrConditionZeroBal
        StrSQLQuery += "Order By Max(SG.Name || ' - ' || IfNull(CT.CityName,''))"

        StrSQLQuery = AgL.GetBackendBasedQuery(StrSQLQuery)
        DTTemp = CMain.FGetDatTable(StrSQLQuery, AgL.GCn)
        If Not DTTemp.Rows.Count > 0 Then MsgBox("No Records Found to Print.") : Exit Sub
        FLoadMainReport("TrialDetail", DTTemp)
        CMain.FormulaSet(RptMain, Me.Text, FGMain)
        CMain.FShowReport(RptMain, Me.MdiParent, Me.Text)
    End Sub
    Private Sub FAnnexure()
        Dim StrCondition1 As String
        Dim DTTemp As DataTable
        Dim I As Int16
        Dim StrFieldName As String = "GroupName", StrSpace As String = "   ", StrFieldPrefix As String = ""
        Dim IntMaxHirarchy As Int16 = 10

        If Not FIsValid(0) Then Exit Sub
        If Not FIsValid(1) Then Exit Sub

        StrCondition1 = "Where LG.V_Date<=" & AgL.Chk_Date(CDate(FGMain(GFilter, 0).Value.ToString).ToString("s")) & " "

        If Trim(FGMain(GFilterCode, 1).Value) <> "" Then
            StrCondition1 += "And (SG.GroupCode In (" & FGMain(GFilterCode, 1).Value & ")  "
            StrCondition1 += "Or SG.GroupCode In (Select AGP2.GroupCode From AcGroupPath AGP2 "
            StrCondition1 += "Where AGP2.GroupUnder In (" & FGMain(GFilterCode, 1).Value & ")))  "
        End If

        If Trim(FGMain(GFilterCode, 2).Value) <> "" Then
            StrCondition1 += " And LG.Site_Code In (" & FGMain(GFilterCode, 2).Value & ") "
        Else
            StrCondition1 += " And LG.Site_Code In  (" & AgL.PubSiteList & ") "
        End If

        If Trim(FGMain(GFilterCode, 3).Value) <> "" Then
            StrCondition1 += " And LG.DivCode In (" & FGMain(GFilterCode, 3).Value & ") "
        Else
            StrCondition1 += " And LG.DivCode In  (" & AgL.PubDivisionList & ") "
        End If

        StrSQLQuery = "Select  IfNull((Select Max(AG1.GroupName) "
        StrSQLQuery += "From AcGroupPath AGP Left Join "
        StrSQLQuery += "AcGroup AG1 On AG1.GroupCode=AGP.GroupUnder And AGP.SNo=" & 1 & " "
        StrSQLQuery += "Where AGP.GroupCode=Max(SG.GroupCode)), "
        StrSQLQuery += "(Case When (Select IfNull(Max(SNo),0) From AcGroupPath AGP1 "
        StrSQLQuery += "Where AGP1.GroupCode=Max(SG.GroupCode))= " & 0 & " "
        StrSQLQuery += "Then Max(AG.GroupName) Else '' End)) As " & StrFieldName + Trim(1) & " , "

        DTTemp = CMain.FGetDatTable("Select IfNull(Max(SNo),0) From AcGroupPath", AgL.GCn)
        If DTTemp.Rows(0).Item(0) > (IntMaxHirarchy - 1) Then MsgBox("There Can Be Only " & IntMaxHirarchy - 1 & " A/c Group Levels. Levels Are Exceding.") : Exit Sub
        For I = 2 To DTTemp.Rows(0).Item(0) + 1
            StrFieldPrefix += StrSpace
            StrSQLQuery += "IfNull((Select '" & StrFieldPrefix & "' || Max(AG1.GroupName) "
            StrSQLQuery += "From AcGroupPath AGP Left Join "
            StrSQLQuery += "AcGroup AG1 On AG1.GroupCode=AGP.GroupUnder And AGP.SNo=" & I & " "
            StrSQLQuery += "Where AGP.GroupCode=Max(SG.GroupCode)), "
            StrSQLQuery += "(Case When (Select IfNull(Max(SNo),0) From AcGroupPath AGP1 "
            StrSQLQuery += "Where AGP1.GroupCode=Max(SG.GroupCode))= " & I - 1 & " "
            StrSQLQuery += "Then '" & StrFieldPrefix & "' || Max(AG.GroupName) Else '' End)) As "
            StrSQLQuery += StrFieldName + Trim(I) & " , "
        Next

        For I = DTTemp.Rows(0).Item(0) + 2 To IntMaxHirarchy
            StrSQLQuery += "' ' As " & StrFieldName + Trim(I) & " , "
        Next

        StrSQLQuery += "SG.Name, "
        StrSQLQuery += "(Case When IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LG.AmtCr),0)>0 Then "
        StrSQLQuery += "IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LG.AmtCr),0) Else 0 End) As AmtDr, "
        StrSQLQuery += "(Case When IfNull(Sum(LG.AmtCr),0)-IfNull(Sum(LG.AmtDr),0)>0 Then "
        StrSQLQuery += "IfNull(Sum(LG.AmtCr),0)-IfNull(Sum(LG.AmtDr),0) Else 0 End) As AmtCr "
        StrSQLQuery += "From "
        StrSQLQuery += "Ledger LG Left Join "
        StrSQLQuery += "SubGroup SG On LG.SubCode=SG.SubCode Left Join "
        StrSQLQuery += "AcGroup AG On AG.GroupCode=SG.GroupCode "
        StrSQLQuery += StrCondition1
        StrSQLQuery += "Group By SG.Name "
        StrSQLQuery += "Having (IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LG.AmtCr),0)) <> 0 "

        StrSQLQuery = AgL.GetBackendBasedQuery(StrSQLQuery)
        DTTemp = CMain.FGetDatTable(StrSQLQuery, AgL.GCn)
        If Not DTTemp.Rows.Count > 0 Then MsgBox("No Records Found to Print.") : Exit Sub

        FLoadMainReport("Annexure", DTTemp)
        CMain.FormulaSet(RptMain, Me.Text, FGMain)
        CMain.FShowReport(RptMain, Me.MdiParent, Me.Text)
    End Sub
    Private Sub FCashBank_JournalBook()
        Dim DTTemp As DataTable
        Dim StrCondition As String, StrConditionSubQuery As String
        Dim StrConditionDayOP As String, StrConditionOP As String

        If Not FIsValid(0) Then Exit Sub
        If Not FIsValid(1) Then Exit Sub
        If Not FIsValid(2) Then Exit Sub
        If Not FIsValid(3) Then Exit Sub

        StrCondition = " Where ( Date(LG.V_Date) Between " & AgL.Chk_Date(CDate(FGMain(GFilter, 0).Value.ToString).ToString("s")) & " And " & AgL.Chk_Date(CDate(FGMain(GFilter, 1).Value.ToString).ToString("s")) & ") "
        StrConditionSubQuery = " Where ( Date(LGS.V_Date) Between " & AgL.Chk_Date(CDate(FGMain(GFilter, 0).Value.ToString).ToString("s")) & " And " & AgL.Chk_Date(CDate(FGMain(GFilter, 1).Value.ToString).ToString("s")) & ") "
        StrConditionDayOP = " Where ( Date(LG.V_Date) Between " & AgL.Chk_Date(CDate(FGMain(GFilter, 0).Value.ToString).ToString("s")) & " And " & AgL.Chk_Date(CDate(FGMain(GFilter, 1).Value.ToString).ToString("s")) & ") "
        StrConditionOP = " Where Date(LG.V_Date) < " & AgL.Chk_Date(CDate(FGMain(GFilter, 0).Value.ToString).ToString("s")) & " "

        If Trim(FGMain(GFilterCode, 3).Value) <> "" Then StrCondition += " And LG.Subcode <>'" & FGMain(GFilterCode, 3).Value & "' "
        If Trim(FGMain(GFilterCode, 3).Value) <> "" Then StrConditionSubQuery += " And LGS.Subcode ='" & FGMain(GFilterCode, 3).Value & "' "
        If Trim(FGMain(GFilterCode, 3).Value) <> "" Then StrConditionDayOP += " And LG.Subcode ='" & FGMain(GFilterCode, 3).Value & "' "
        If Trim(FGMain(GFilterCode, 3).Value) <> "" Then StrConditionOP += " And LG.Subcode ='" & FGMain(GFilterCode, 3).Value & "' "

        If Trim(FGMain(GFilterCode, 3).Value) <> "" Then StrConditionSubQuery += " And LGS.Site_Code In (" & FGMain(GFilterCode, 2).Value & ") "
        If Trim(FGMain(GFilterCode, 3).Value) <> "" Then StrConditionDayOP += " And LG.Site_Code In (" & FGMain(GFilterCode, 2).Value & ") "
        If Trim(FGMain(GFilterCode, 3).Value) <> "" Then StrConditionOP += " And LG.Site_Code In (" & FGMain(GFilterCode, 2).Value & ") "

        'If Trim(FGMain(GFilterCode, 5).Value) <> "" Then
        '    StrWithnarration = Trim(FGMain(GFilterCode, 5).Value)
        'End If
        '===========================
        StrSQLQuery = "Declare @TmpTable Table (V_Date SmallDateTime,DayDR Float,DayDR_OPN Float,Opening Float) "
        StrSQLQuery += "Declare @RNTDr Float  "
        StrSQLQuery += "Set @RNTDr=0 "

        StrSQLQuery += "Insert Into @TmpTable  "

        StrSQLQuery += "Select	V_Date,Max(DayDR) As DayDR, Max(DayDr_OPN) As DayDr_OPN,0 As Opening "
        StrSQLQuery += "From ("
        StrSQLQuery += "Select	V_Date,(IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LG.AmtCr),0)) As DayDR, "
        StrSQLQuery += "Null As DayDr_OPN,0 As Opening "
        StrSQLQuery += "From Ledger LG "
        StrSQLQuery += StrConditionDayOP
        StrSQLQuery += "Group By V_Date  "
        StrSQLQuery += "Union All "
        StrSQLQuery += "Select	" & AgL.Chk_Date(CDate(FGMain(GFilter, 0).Value.ToString).ToString("s")) & " As V_Date,Null As DayDR, "
        StrSQLQuery += "(IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LG.AmtCr),0)) As DayDR_OPN,0 As Opening "
        StrSQLQuery += "From Ledger LG "
        StrSQLQuery += StrConditionOP
        StrSQLQuery += ") As Tmp "
        StrSQLQuery += "Group By Tmp.V_Date   "
        StrSQLQuery += "ORDER BY V_Date   "

        StrSQLQuery += "Update	@TmpTable Set "
        StrSQLQuery += "@RNTDr = @RNTDr + IfNull(DayDR,0) + IfNull(DayDr_OPN,0), "
        StrSQLQuery += "Opening = @RNTDr - IfNull(DayDR,0)	"

        StrSQLQuery += "Select	LG.DocId,LG.SubCode,Cast(LG.RecID as Varchar) as RecID,LG.V_Date,LG.V_Type,LG.Site_Code,LG.AmtDr,LG.AmtCr, "
        StrSQLQuery += "LG.V_SNo,SG.Name As AcName,LG.Chq_Date,LG.Chq_No,1 As SNo, "
        StrSQLQuery += "Null As Main_AmtDr,Null As Main_AmtCr,LG.Narration,IfNull(VT.SerialNo,0) As SerialNo "
        StrSQLQuery += "From Ledger LG "
        StrSQLQuery += "Left Join SubGroup SG On LG.SubCode=SG.SubCode "
        StrSQLQuery += "Left Join Voucher_Type VT On VT.V_Type=LG.V_Type "
        StrSQLQuery += StrCondition
        StrSQLQuery += "And LG.DocId In "
        StrSQLQuery += "(Select DocId From Ledger LGS " & StrConditionSubQuery & " Group By DocId) "
        StrSQLQuery += "Union All "

        StrSQLQuery += "Select	Null As DocId,Null As SubCode,Null As RecID,TT.V_Date,Null As V_Type, "
        StrSQLQuery += "Null As Site_Code, "
        StrSQLQuery += "(Case When TT.Opening < 0 Then Abs(TT.Opening) Else 0 End) As AmtDr, "
        StrSQLQuery += "(Case When TT.Opening > 0 Then Abs(TT.Opening) Else 0 End) As AmtCr, "
        StrSQLQuery += "Null As V_SNo, "
        StrSQLQuery += "'O P E N I N G  B A L A N C E' As AcName,Null As Chq_Date,Null As Chq_No,0 As SNo, "
        StrSQLQuery += "Null As Main_AmtDr,Null As Main_AmtCr,'' As Narration,0 As SerialNo "
        StrSQLQuery += "From @TmpTable TT "
        StrSQLQuery += "Where(IfNull(TT.Opening, 0) <> 0) "
        StrSQLQuery += "Union All "

        StrSQLQuery += "Select LGS.DocId,Null As SubCode,Cast(LGS.RecID as Varchar) as RecID,LGS.V_Date,LGS.V_Type, "
        StrSQLQuery += "Null As Site_Code,0 As AmtDr,0 As AmtCr,LGS.V_SNo, "
        StrSQLQuery += "Null As AcName,Null As Chq_Date,Null As Chq_No,2 As SNo, "
        StrSQLQuery += "LGS.AmtDr As Main_AmtDr,LGS.AmtCr As Main_AmtCr,'' As Narration,IfNull(VT.SerialNo,0) As SerialNo "
        StrSQLQuery += "From Ledger LGS "
        StrSQLQuery += "Left Join Voucher_Type VT On VT.V_Type=LGS.V_Type "
        StrSQLQuery += StrConditionSubQuery
        'StrSQLQuery += "Order By LG.V_Date,LG.V_Type,LG.V_No,LG.DocId,SNo "
        StrSQLQuery += "Order By LG.V_Date,SerialNo,RecID,LG.DocId,SNo "

        StrSQLQuery = AgL.GetBackendBasedQuery(StrSQLQuery)
        DTTemp = CMain.FGetDatTable(StrSQLQuery, AgL.GCn)
        If Not DTTemp.Rows.Count > 0 Then MsgBox("No Records Found to Print.") : Exit Sub

        FLoadMainReport("CashBank_JournalBook", DTTemp)
        CMain.FormulaSet(RptMain, Me.Text, FGMain)
        CMain.FShowReport(RptMain, Me.MdiParent, Me.Text)
    End Sub
    Private Sub FCashBook()
        Dim StrCondition1 As String
        Dim StrConditionOP As String
        Dim DTTemp As DataTable
        Dim DblOpening As Double = 0
        Dim SQL As String
        Dim Pagewise As String
        Dim Withnarration As String

        If Not FIsValid(0) Then Exit Sub
        If Not FIsValid(1) Then Exit Sub
        If Not FIsValid(3) Then Exit Sub
        Pagewise = "N"
        Withnarration = "N"
        StrCondition1 = " Where ( Date(L.V_Date) Between " & AgL.Chk_Date(CDate(FGMain(GFilter, 0).Value.ToString).ToString("s")) & " And " & AgL.Chk_Date(CDate(FGMain(GFilter, 1).Value.ToString).ToString("s")) & ") "
        If Trim(FGMain(GFilterCode, 2).Value) <> "" Then
            StrCondition1 = StrCondition1 & " And  L.Site_Code IN (" & FGMain(GFilterCode, 2).Value & ") "
            StrConditionOP = " And  L.Site_Code IN (" & FGMain(GFilterCode, 2).Value & ") "
        Else
            StrCondition1 = StrCondition1 & " And  L.Site_Code IN (" & AgL.PubSiteList & ") "
            StrConditionOP = " And  L.Site_Code IN (" & AgL.PubSiteList & ") "
        End If

        If Trim(FGMain(GFilterCode, 3).Value) <> "" Then
            StrCondition1 = StrCondition1 & " And  L.DivCode IN (" & FGMain(GFilterCode, 3).Value & ") "
            StrConditionOP = " And  L.DivCode IN (" & FGMain(GFilterCode, 3).Value & ") "
        Else
            StrCondition1 = StrCondition1 & " And  L.DivCode IN (" & AgL.PubDivisionList & ") "
            StrConditionOP = " And  L.DivCode IN (" & AgL.PubDivisionList & ") "
        End If

        If Trim(FGMain(GFilterCode, 5).Value) <> "" Then
            Pagewise = Trim(FGMain(GFilterCode, 5).Value)
        End If
        If Trim(FGMain(GFilterCode, 6).Value) <> "" Then
            Withnarration = Trim(FGMain(GFilterCode, 6).Value)
        End If
        SQL = "Select (IfNull(Sum(AmtCr),0)-IfNull(Sum(AmtDr),0)) As OP From Ledger L "
        SQL = SQL + "Left Join SubGroup SG On L.SubCode=SG.SubCode Where SG.Nature='Cash' "
        SQL = SQL + "And V_Date<" & AgL.Chk_Date(CDate(FGMain(GFilter, 0).Value.ToString).ToString("s")) & " "
        SQL = SQL + "And " & " L.subcode IN ('" & FGMain(GFilterCode, 4).Value & "') " & StrConditionOP

        DTTemp = CMain.FGetDatTable(SQL, AgL.GCn)
        If DTTemp.Rows.Count > 0 Then DblOpening = AgL.VNull(DTTemp.Rows(0).Item("OP"))
        SQL = "DECLARE @tmptb TABLE(code datetime) "
        SQL += "DECLARE @tempfromdt AS DATETIME "
        SQL += "DECLARE @temptodt AS DATETIME "
        SQL += " SET @tempfromdt=" & AgL.Chk_Date(CDate(FGMain(GFilter, 0).Value.ToString).ToString("s"))
        SQL += " SET @temptodt=" & AgL.Chk_Date(CDate(FGMain(GFilter, 1).Value.ToString).ToString("s"))
        SQL += " WHILE @tempfromdt<=@temptodt "
        SQL += " BEGIN "
        SQL += " INSERT INTO @tmptb VALUES (@tempfromdt) "
        SQL += " SET @tempfromdt=@tempfromdt+1 End "
        SQL += "Select IfNull(DocID,'') As DocId,Cast(IfNull(V_No,'') as Varchar) As V_no,IfNull(T.Code,'') As V_date,IfNull(Particular,'') As Particular,IfNull(AmtDr,0) As AmtDr,IfNull(AmtCr,0) As AmtCr,IfNull(V_Type,'') As V_Type,IfNull(NCat,'') As NCat,IfNull(Nature,'') As nature,IfNull(Narration,'') as Narration "
        SQL = SQL + " From @tmptb T left join "
        SQL = SQL + " (Select L.DocID,Cast(L.RecID as Varchar) As V_No,L.V_Date ,SG.[Name] As Particular,L.AmtDr , L.AmtCr,L.V_Type ,VT.NCat,SG.Nature,IfNull(L.Narration,'') as Narration "
        SQL = SQL + " From Ledger L "
        SQL = SQL + " Left Join SubGroup SG On L.SubCode=SG.SubCode "
        SQL = SQL + " Left Join Voucher_Type VT On VT.V_Type=L.V_Type "
        SQL = SQL + " Where L.subcode<>'" & FGMain(GFilterCode, 4).Value & "' "
        SQL = SQL + " And (IfNull(L.TDSCategory,'')='' Or (IfNull(L.TDSCategory,'')<>'' And IfNull(L.System_Generated,'N')='N'))"
        SQL = SQL + " And L.DocID In ( "
        SQL = SQL + " Select L.DocID From Ledger L "
        SQL = SQL + " Left Join SubGroup SG On L.SubCode=SG.SubCode "
        SQL = SQL + " Left Join Voucher_Type VT On VT.V_Type=L.V_Type "
        SQL = SQL + StrCondition1 & " And VT.Category IN('RCT','PMT') And SG.Nature='Cash'"
        SQL = SQL + " And L.subcode IN ('" & FGMain(GFilterCode, 4).Value & "'))"
        SQL = SQL + " Union All "
        SQL = SQL + "Select L.DocID,Cast(L.RecID as Varchar) As V_No,L.V_Date ,SG.[Name] As Particular,L.AmtCr As AmtCr,L.AmtDr As AmtDr,L.V_Type ,VT.NCat,SG.Nature,IfNull(L.Narration,'') as Narration "
        SQL = SQL + "From Ledger L "
        SQL = SQL + "Left Join SubGroup SG On L.ContraSub=SG.SubCode "
        SQL = SQL + "Left Join Voucher_Type VT On VT.V_Type=L.V_Type "
        SQL = SQL + StrCondition1 & " And VT.Category NOT IN('RCT','PMT') "
        SQL = SQL + " And L.subcode IN ('" & FGMain(GFilterCode, 4).Value & "')"
        SQL = SQL + ") Tab on tab.v_date=t.code Order By t.code,DocId"

        DTTemp = New DataTable("Tab")

        DTTemp = CMain.FGetDatTable(SQL, AgL.GCn)


        FCashBookDouble(DTTemp, DblOpening, Pagewise, Withnarration)
    End Sub
    Private Sub FCashBookDouble(ByVal DTTemp As DataTable, ByVal DblOpening As Double,
    ByVal Pagewise As String, ByVal Withnarration As String)
        Dim CrPos As Integer = 0
        Dim DrPos As Integer = 0
        Dim StrVDate As String = ""
        Dim DT_CSHBook As DataTable

        DT_CSHBook = New DataTable("CashBook")
        With DT_CSHBook.Columns
            .Add("CVDate", System.Type.GetType("System.DateTime"))
            .Add("CVNo", System.Type.GetType("System.String"))
            .Add("CType", System.Type.GetType("System.String"))
            .Add("CParticular", System.Type.GetType("System.String"))
            .Add("AmtCr", System.Type.GetType("System.Double"))
            .Add("DVNo", System.Type.GetType("System.String"))
            .Add("DType", System.Type.GetType("System.String"))
            .Add("DParticular", System.Type.GetType("System.String"))
            .Add("AmtDr", System.Type.GetType("System.Double"))
            .Add("PageWise", System.Type.GetType("System.String"))
            .Add("WithNarration", System.Type.GetType("System.String"))
            .Add("CNarr", System.Type.GetType("System.String"))
            .Add("DNarr", System.Type.GetType("System.String"))
        End With
        For mCnt As Integer = 0 To DTTemp.Rows.Count - 1
            With DTTemp.Rows(mCnt)
                If StrVDate <> AgL.XNull(.Item("V_Date")) Then
                    If DrPos > CrPos Then CrPos = DrPos Else DrPos = CrPos
                    FCAddOpening(IIf(DblOpening < 0, 0, Math.Abs(DblOpening)), IIf(DblOpening < 0, Math.Abs(DblOpening), 0), DT_CSHBook,
                                                   CrPos, DrPos, AgL.XNull(.Item("V_Date")))
                End If
                FCAddRow(AgL.VNull(.Item("AmtDr")), AgL.VNull(.Item("AmtCr")), DT_CSHBook, AgL.XNull(.Item("V_No")),
                        AgL.XNull(.Item("V_Type")), AgL.XNull(.Item("Particular")), AgL.XNull(.Item("V_Date")), CrPos, DrPos, DblOpening, AgL.XNull(.Item("Narration")))
                DT_CSHBook.Rows(DT_CSHBook.Rows.Count - 1).Item("PageWise") = Pagewise 'IIf(FGMain(GFilterCode, 3).Value = Nothing, "N", FGMain(GFilterCode, 3).Value.ToString)
                DT_CSHBook.Rows(DT_CSHBook.Rows.Count - 1).Item("WithNarration") = Withnarration
                StrVDate = AgL.XNull(.Item("V_Date"))
            End With
        Next
        If Not DTTemp.Rows.Count > 0 Then MsgBox("No Records Found to Print.") : Exit Sub
        FLoadMainReport("CashBook", DT_CSHBook)
        CMain.FormulaSet(RptMain, Me.Text, FGMain)
        CMain.FShowReport(RptMain, Me.MdiParent, Me.Text)
    End Sub
    Private Sub FCAddOpening(ByVal DblDr As Double, ByVal DblCr As Double, ByRef DTCashBook As DataTable,
           ByRef IntCrPos As Integer, ByRef IntDrPos As Integer, ByVal StrVDate As Date)

        If DblCr <> 0 Then
            If IntCrPos >= IntDrPos Then
                Dim DRRow As DataRow = DTCashBook.NewRow
                DRRow("CVDate") = StrVDate
                DRRow("CParticular") = "Opening Balance"
                DRRow("AmtCr") = DblCr
                DTCashBook.Rows.Add(DRRow)
            Else
                DTCashBook.Rows(IntCrPos).Item("CParticular") = "Opening Balance"
                DTCashBook.Rows(IntCrPos).Item("AmtCr") = DblCr
            End If
            IntCrPos = IntCrPos + 1
        ElseIf DblDr <> 0 Then
            If IntDrPos >= IntCrPos Then
                Dim DRRow As DataRow = DTCashBook.NewRow
                DRRow("CVDate") = StrVDate
                DRRow("DParticular") = "Opening Balance"
                DRRow("AmtDr") = DblDr
                DTCashBook.Rows.Add(DRRow)
            Else
                DTCashBook.Rows(IntCrPos).Item("DParticular") = "Opening Balance"
                DTCashBook.Rows(IntCrPos).Item("AmtDr") = DblDr
            End If
            IntDrPos = IntDrPos + 1
        Else
            If IntCrPos >= IntDrPos Then
                Dim DRRow As DataRow = DTCashBook.NewRow
                DRRow("CVDate") = StrVDate
                DRRow("CParticular") = "Opening Balance"
                DRRow("AmtCr") = 0
                DTCashBook.Rows.Add(DRRow)
            Else
                DTCashBook.Rows(IntCrPos).Item("CParticular") = "Opening Balance"
                DTCashBook.Rows(IntCrPos).Item("AmtCr") = 0
            End If
            IntCrPos = IntCrPos + 1
        End If
    End Sub
    Private Sub FCAddRow(ByVal DblDr As Double, ByVal DblCr As Double, ByRef DTCashBook As DataTable,
    ByVal StrVNo As String, ByVal StrVType As String, ByVal StrParticular As String, ByVal StrVDate As Date,
    ByRef IntCrPos As Integer, ByRef IntDrPos As Integer, ByRef DblOpening As Double, ByRef StrNarration As String)
        If DblCr > 0 Then
            If IntCrPos >= IntDrPos Then
                Dim DRRow As DataRow = DTCashBook.NewRow
                DRRow("CVDate") = StrVDate
                DRRow("CVNo") = StrVNo
                DRRow("CType") = StrVType
                DRRow("CParticular") = StrParticular
                DRRow("AmtCr") = DblCr
                DRRow("CNarr") = StrNarration
                DTCashBook.Rows.Add(DRRow)
            Else
                DTCashBook.Rows(IntCrPos).Item("CVNo") = StrVNo
                DTCashBook.Rows(IntCrPos).Item("CType") = StrVType
                DTCashBook.Rows(IntCrPos).Item("CParticular") = StrParticular
                DTCashBook.Rows(IntCrPos).Item("AmtCr") = DblCr
                DTCashBook.Rows(IntCrPos).Item("CNarr") = StrNarration
            End If
            DblOpening = DblOpening - DblCr
            IntCrPos = IntCrPos + 1
        ElseIf DblDr > 0 Then
            If IntDrPos >= IntCrPos Then
                Dim DRRow As DataRow = DTCashBook.NewRow
                DRRow("CVDate") = StrVDate
                DRRow("DVNo") = StrVNo
                DRRow("DType") = StrVType
                DRRow("DParticular") = StrParticular
                DRRow("AmtDr") = DblDr
                DRRow("DNarr") = StrNarration
                DTCashBook.Rows.Add(DRRow)
            Else
                DTCashBook.Rows(IntDrPos).Item("DVNo") = StrVNo
                DTCashBook.Rows(IntDrPos).Item("DType") = StrVType
                DTCashBook.Rows(IntDrPos).Item("DParticular") = StrParticular
                DTCashBook.Rows(IntDrPos).Item("AmtDr") = DblDr
                DTCashBook.Rows(IntDrPos).Item("DNarr") = StrNarration
            End If
            DblOpening = DblOpening + DblDr
            IntDrPos = IntDrPos + 1
        Else
            If IntCrPos >= IntDrPos Then
                Dim DRRow As DataRow = DTCashBook.NewRow
                DRRow("CVDate") = StrVDate
                DRRow("CVNo") = StrVNo
                DRRow("CType") = StrVType
                DRRow("CParticular") = StrParticular
                DRRow("AmtCr") = DblCr
                DTCashBook.Rows.Add(DRRow)
            Else
                DTCashBook.Rows(IntCrPos).Item("CVNo") = StrVNo
                DTCashBook.Rows(IntCrPos).Item("CType") = StrVType
                DTCashBook.Rows(IntCrPos).Item("CParticular") = StrParticular
                DTCashBook.Rows(IntCrPos).Item("AmtCr") = DblCr
            End If
            DblOpening = DblOpening - DblCr
            IntCrPos = IntCrPos + 1
        End If
    End Sub

    Private Sub FAddOpening(ByVal DblDr As Double, ByVal DblCr As Double, ByRef DTCashBook As DataTable,
             ByRef IntCrPos As Integer, ByRef IntDrPos As Integer, ByVal StrVDate As Date)
        If DblCr > 0 Then
            If IntCrPos >= IntDrPos Then
                Dim DRRow As DataRow = DTCashBook.NewRow
                DRRow("CVDate") = StrVDate
                DRRow("CParticular") = "Opening Balance"
                DRRow("AmtCr") = DblCr
                DTCashBook.Rows.Add(DRRow)
            Else
                DTCashBook.Rows(IntCrPos).Item("CParticular") = "Opening Balance"
                DTCashBook.Rows(IntCrPos).Item("AmtCr") = DblCr
            End If
            IntCrPos = IntCrPos + 1
        ElseIf DblDr > 0 Then
            If IntDrPos >= IntCrPos Then
                Dim DRRow As DataRow = DTCashBook.NewRow
                DRRow("CVDate") = StrVDate
                DRRow("DParticular") = "Opening Balance"
                DRRow("AmtDr") = DblDr
                DTCashBook.Rows.Add(DRRow)
            Else
                DTCashBook.Rows(IntCrPos).Item("DParticular") = "Opening Balance"
                DTCashBook.Rows(IntCrPos).Item("AmtDr") = DblDr
            End If
            IntDrPos = IntDrPos + 1
        Else
            If IntCrPos >= IntDrPos Then
                Dim DRRow As DataRow = DTCashBook.NewRow
                DRRow("CVDate") = StrVDate
                DRRow("CParticular") = "Opening Balance"
                DRRow("AmtCr") = DblDr + DblCr
                DTCashBook.Rows.Add(DRRow)
            Else
                DTCashBook.Rows(IntCrPos).Item("CParticular") = "Opening Balance"
                DTCashBook.Rows(IntCrPos).Item("AmtCr") = DblDr + DblCr
            End If
            IntCrPos = IntCrPos + 1
        End If
    End Sub
    Private Sub FAddRow(ByVal DblDr As Double, ByVal DblCr As Double, ByRef DTCashBook As DataTable,
    ByVal StrVNo As String, ByVal StrVType As String, ByVal StrParticular As String, ByVal StrVDate As Date,
    ByRef IntCrPos As Integer, ByRef IntDrPos As Integer, ByRef DblOpening As Double, ByVal StrChqNo As String,
    ByVal StrChqDt As String, ByVal StrNarration As String)
        If DblCr > 0 Then
            If IntCrPos >= IntDrPos Then
                Dim DRRow As DataRow = DTCashBook.NewRow
                DRRow("CVDate") = StrVDate
                DRRow("CVNo") = StrVNo
                DRRow("CType") = StrVType
                DRRow("CParticular") = StrParticular
                DRRow("AmtCr") = DblCr
                DRRow("CChqNo") = StrChqNo
                DRRow("CChqDt") = StrChqDt
                DRRow("NarrationCr") = StrNarration
                DTCashBook.Rows.Add(DRRow)
            Else
                DTCashBook.Rows(IntCrPos).Item("CVNo") = StrVNo
                DTCashBook.Rows(IntCrPos).Item("CType") = StrVType
                DTCashBook.Rows(IntCrPos).Item("CParticular") = StrParticular
                DTCashBook.Rows(IntCrPos).Item("AmtCr") = DblCr
                DTCashBook.Rows(IntCrPos).Item("CChqNo") = StrChqNo
                DTCashBook.Rows(IntCrPos).Item("CChqDt") = StrChqDt
                DTCashBook.Rows(IntCrPos).Item("NarrationCr") = StrNarration
            End If
            DblOpening = DblOpening - DblCr
            IntCrPos = IntCrPos + 1
        ElseIf DblDr > 0 Then
            If IntDrPos >= IntCrPos Then
                Dim DRRow As DataRow = DTCashBook.NewRow
                DRRow("CVDate") = StrVDate
                DRRow("DVNo") = StrVNo
                DRRow("DType") = StrVType
                DRRow("DParticular") = StrParticular
                DRRow("AmtDr") = DblDr
                DRRow("DChqNo") = StrChqNo
                DRRow("DChqDt") = StrChqDt
                DRRow("NarrationDr") = StrNarration
                DTCashBook.Rows.Add(DRRow)
            Else
                DTCashBook.Rows(IntDrPos).Item("DVNo") = StrVNo
                DTCashBook.Rows(IntDrPos).Item("DType") = StrVType
                DTCashBook.Rows(IntDrPos).Item("DParticular") = StrParticular
                DTCashBook.Rows(IntDrPos).Item("AmtDr") = DblDr
                DTCashBook.Rows(IntDrPos).Item("DChqNo") = StrChqNo
                DTCashBook.Rows(IntDrPos).Item("DChqDt") = StrChqDt
                DTCashBook.Rows(IntDrPos).Item("NarrationDr") = StrNarration
            End If
            DblOpening = DblOpening + DblDr
            IntDrPos = IntDrPos + 1
        Else
            If IntCrPos >= IntDrPos Then
                Dim DRRow As DataRow = DTCashBook.NewRow
                DRRow("CVDate") = StrVDate
                DRRow("CVNo") = StrVNo
                DRRow("CType") = StrVType
                DRRow("CParticular") = StrParticular
                DRRow("AmtCr") = DblCr
                DRRow("CChqNo") = StrChqNo
                DRRow("CChqDt") = StrChqDt
                DRRow("NarrationCr") = StrNarration
                DTCashBook.Rows.Add(DRRow)
            Else
                DTCashBook.Rows(IntCrPos).Item("CVNo") = StrVNo
                DTCashBook.Rows(IntCrPos).Item("CType") = StrVType
                DTCashBook.Rows(IntCrPos).Item("CParticular") = StrParticular
                DTCashBook.Rows(IntCrPos).Item("AmtCr") = DblCr
                DTCashBook.Rows(IntCrPos).Item("CChqNo") = StrChqNo
                DTCashBook.Rows(IntCrPos).Item("CChqDt") = StrChqDt
                DTCashBook.Rows(IntCrPos).Item("NarrationCr") = StrNarration
            End If
            DblOpening = DblOpening - DblCr
            IntCrPos = IntCrPos + 1
        End If
    End Sub
    Private Sub FBankBook()
        Dim StrConditionOP As String
        Dim StrCondition1 As String
        Dim DTTemp As DataTable
        Dim I As Integer, J As Integer, IntPosition As Integer
        Dim StrDebitAc As String, StrCreditAc As String
        Dim BlnDebit As Boolean, BlnCredit As Boolean
        Dim StrMainSubCode As String
        Dim StrPrvDocId As String
        Dim SQL As String
        Dim DblOpening As Double = 0
        Dim StrChkFieldFor As String, StrChkDataFor As String
        Dim StrVDate As String
        Dim StrVNo As String
        Dim StrVType As String
        Dim StrVParticular As String
        Dim DblAmtDr As Double
        Dim DblAmtCr As Double
        Dim StrChqNo As String
        Dim StrChqDt As String
        Dim StrNarration As String
        Dim StrWithnarration As String
        Dim DTbankBook As DataTable

        If Not FIsValid(0) Then Exit Sub
        If Not FIsValid(1) Then Exit Sub
        If Not FIsValid(3) Then Exit Sub

        StrWithnarration = "N"
        DTbankBook = New DataTable("BankBook")
        With DTbankBook.Columns
            .Add("VDate", System.Type.GetType("System.DateTime"))
            .Add("VNo", System.Type.GetType("System.String"))
            .Add("Type", System.Type.GetType("System.String"))
            .Add("Particular", System.Type.GetType("System.String"))
            .Add("AmtCr", System.Type.GetType("System.Double"))
            .Add("AmtDr", System.Type.GetType("System.Double"))
            .Add("ChqNo", System.Type.GetType("System.String"))
            .Add("ChqDt", System.Type.GetType("System.String"))
            .Add("Narration", System.Type.GetType("System.String"))
            .Add("OP", System.Type.GetType("System.Double"))
            .Add("PageWise", System.Type.GetType("System.String"))
            .Add("WithNarration", System.Type.GetType("System.String"))
        End With
        StrMainSubCode = UCase(Trim(FGMain(GFilterCode, 3).Value))
        StrCondition1 = " Where ( Date(LG.V_Date) Between " & AgL.Chk_Date(CDate(FGMain(GFilter, 0).Value.ToString).ToString("s")) & " And " & AgL.Chk_Date(CDate(FGMain(GFilter, 1).Value.ToString).ToString("s")) & ") "

        If Trim(FGMain(GFilterCode, 2).Value) <> "" Then
            StrCondition1 = StrCondition1 & " And LG.Site_Code  IN (" & FGMain(GFilterCode, 2).Value & ") "
            StrConditionOP = " And LG.Site_Code  IN (" & FGMain(GFilterCode, 2).Value & ") "
        Else
            StrCondition1 = StrCondition1 & " And LG.Site_Code  IN (" & AgL.PubSiteList & ") "
            StrConditionOP = " And LG.Site_Code  IN (" & AgL.PubSiteList & ") "
        End If

        If Trim(FGMain(GFilterCode, 5).Value) <> "" Then
            StrWithnarration = Trim(FGMain(GFilterCode, 5).Value)
        End If

        SQL = "Select  (IfNull(Sum(AmtCr),0)-IfNull(Sum(AmtDr),0)) As OP,Max(V_Date) As V_Date From Ledger LG "
        SQL = SQL + "Left Join SubGroup SG On LG.SubCode=SG.SubCode  "
        SQL = SQL + "Where  V_Date<" & AgL.Chk_Date(CDate(FGMain(GFilter, 0).Value.ToString).ToString("s")) & " "
        SQL = SQL + "And " & "(LG.subcode IN ('" & FGMain(GFilterCode, 3).Value & "')) " & StrConditionOP


        DTTemp = CMain.FGetDatTable(SQL, AgL.GCn)
        If DTTemp.Rows.Count > 0 Then DblOpening = AgL.VNull(DTTemp.Rows(0).Item("OP"))


        If DblOpening <> 0 Then
            StrVDate = FGMain(GFilter, 0).Value
            StrVParticular = "Opening Balance"
            StrVType = "OPBAL"

            If DblOpening < 0 Then
                DblAmtCr = Math.Abs(DblOpening)
            Else
                DblAmtDr = Math.Abs(DblOpening)
            End If
        End If
        SQL = "Select LG.DocId,LG.AmtDr,LG.AmtCr,LG.V_Date,(Cast(LG.V_No as Varchar) || '-' || LG.Site_Code) As RecId,LG.V_Type, "
        SQL += "LG.Chq_No,LG.Chq_Date,LG.SubCode,LG.ContraSub,LG.Narration, "
        SQL += "SG.Name As PName,SGC.Name As CName "
        SQL += "From Ledger LG "
        SQL += "Left Join SubGroup SG On LG.SubCode=SG.SubCode "
        SQL += "Left Join SubGroup SGC On LG.ContraSub=SGC.SubCode "
        SQL += StrCondition1
        SQL += "And DocId In "
        SQL += "(Select DocId From Ledger LG Where LG.SubCode='" & StrMainSubCode & "') "
        SQL += "Order By LG.V_Date,LG.V_No,LG.DocId,LG.AmtDr "

        DTTemp = CMain.FGetDatTable(SQL, AgL.GCn)
        If DTTemp.Rows.Count > 0 Then StrPrvDocId = AgL.XNull(DTTemp.Rows(I).Item("DocId")) Else StrPrvDocId = ""
        IntPosition = 0
        StrDebitAc = ""
        StrCreditAc = ""
        StrVDate = "" : StrVNo = "" : StrVType = "" : StrVParticular = "" : DblAmtDr = 0 : DblAmtCr = 0
        StrChqNo = "" : StrChqDt = "" : StrNarration = ""
        BlnDebit = False : BlnCredit = False

        For I = 0 To DTTemp.Rows.Count - 1
            If StrPrvDocId <> AgL.XNull(DTTemp.Rows(I).Item("DocId")) Then
LblForLastRecord:
                For J = IntPosition To IIf((DTTemp.Rows.Count - 1) = I, I, I - 1)
                    StrVDate = AgL.XNull(DTTemp.Rows(J).Item("V_Date"))
                    StrVNo = AgL.XNull(DTTemp.Rows(J).Item("RecId"))
                    StrVType = AgL.XNull(DTTemp.Rows(J).Item("V_Type"))

                    'Conditions
                    If StrDebitAc = "" And StrCreditAc = "" Then
                        'Case 5 & 3
                        If StrMainSubCode = Trim(UCase(AgL.XNull(DTTemp.Rows(J).Item("SubCode")))) Then
                            If Trim(UCase(AgL.XNull(DTTemp.Rows(J).Item("ContraSub")))) <> "" Then StrVParticular = AgL.XNull(DTTemp.Rows(J).Item("CName")) Else StrVParticular = ""
                            DblAmtCr = AgL.VNull(DTTemp.Rows(J).Item("AmtDr"))
                            DblAmtDr = AgL.VNull(DTTemp.Rows(J).Item("AmtCr"))
                            StrChqNo = AgL.XNull(DTTemp.Rows(J).Item("Chq_No"))
                            StrChqDt = AgL.XNull(DTTemp.Rows(J).Item("Chq_Date"))
                            StrNarration = AgL.XNull(DTTemp.Rows(J).Item("Narration"))
                            FAddRowBankCash(DTbankBook, StrVNo, StrVType, StrVParticular, StrVDate, StrChqNo, StrChqDt, StrNarration, DblAmtDr, DblAmtCr, 0, StrWithnarration)
                        End If
                    ElseIf (StrDebitAc <> "" Or StrCreditAc <> "") And (UCase(Trim(StrDebitAc)) = StrMainSubCode Or UCase(Trim(StrCreditAc)) = StrMainSubCode) Then
                        'Case 1 & 4
                        StrChkFieldFor = ""
                        If StrDebitAc <> "" And UCase(Trim(StrDebitAc)) = StrMainSubCode Then StrChkFieldFor = "AmtCr"
                        If StrCreditAc <> "" And UCase(Trim(StrCreditAc)) = StrMainSubCode Then StrChkFieldFor = "AmtDr"

                        If AgL.VNull(DTTemp.Rows(J).Item(StrChkFieldFor)) > 0 Then
                            If StrMainSubCode <> Trim(UCase(AgL.XNull(DTTemp.Rows(J).Item("SubCode")))) Then
                                StrVParticular = AgL.XNull(DTTemp.Rows(J).Item("PName"))
                                DblAmtCr = AgL.VNull(DTTemp.Rows(J).Item("AmtCr"))
                                DblAmtDr = AgL.VNull(DTTemp.Rows(J).Item("AmtDr"))
                                StrChqNo = AgL.XNull(DTTemp.Rows(J).Item("Chq_No"))
                                StrChqDt = AgL.XNull(DTTemp.Rows(J).Item("Chq_Date"))
                                StrNarration = AgL.XNull(DTTemp.Rows(J).Item("Narration"))
                                FAddRowBankCash(DTbankBook, StrVNo, StrVType, StrVParticular, StrVDate, StrChqNo, StrChqDt, StrNarration, DblAmtDr, DblAmtCr, 0, StrWithnarration)
                            End If
                        End If
                    ElseIf StrDebitAc <> "" Or StrCreditAc <> "" Then
                        'Case 2
                        StrChkFieldFor = ""
                        StrChkDataFor = ""
                        If StrDebitAc <> "" Then StrChkFieldFor = "AmtCr" : StrChkDataFor = Trim(UCase(StrDebitAc))
                        If StrCreditAc <> "" Then StrChkFieldFor = "AmtDr" : StrChkDataFor = Trim(UCase(StrCreditAc))

                        If StrMainSubCode = Trim(UCase(AgL.XNull(DTTemp.Rows(J).Item("SubCode")))) Then
                            StrVParticular = AgL.XNull(DTTemp.Rows(J).Item("CName"))
                            DblAmtCr = AgL.VNull(DTTemp.Rows(J).Item("AmtDr"))
                            DblAmtDr = AgL.VNull(DTTemp.Rows(J).Item("AmtCr"))
                            StrChqNo = AgL.XNull(DTTemp.Rows(J).Item("Chq_No"))
                            StrChqDt = AgL.XNull(DTTemp.Rows(J).Item("Chq_Date"))
                            StrNarration = AgL.XNull(DTTemp.Rows(J).Item("Narration"))

                            FAddRowBankCash(DTbankBook, StrVNo, StrVType, StrVParticular, StrVDate, StrChqNo, StrChqDt, StrNarration, DblAmtDr, DblAmtCr, 0, StrWithnarration)
                        End If
                    End If
                Next
                IntPosition = I
                StrDebitAc = ""
                StrCreditAc = ""
                StrVDate = "" : StrVNo = "" : StrVType = "" : StrVParticular = "" : DblAmtDr = 0 : DblAmtCr = 0
                StrChqNo = "" : StrChqDt = "" : StrNarration = ""
                BlnDebit = False : BlnCredit = False
                If (DTTemp.Rows.Count - 1) = I Then Exit For
            End If
            If AgL.VNull(DTTemp.Rows(I).Item("AmtDr")) > 0 Then
                If Not BlnDebit Then
                    If Trim(StrDebitAc) = "" Then StrDebitAc = AgL.XNull(DTTemp.Rows(I).Item("SubCode")) Else StrDebitAc = "" : BlnDebit = True
                End If
            End If
            If AgL.VNull(DTTemp.Rows(I).Item("AmtCr")) > 0 Then
                If Not BlnCredit Then
                    If Trim(StrCreditAc) = "" Then StrCreditAc = AgL.XNull(DTTemp.Rows(I).Item("SubCode")) Else StrCreditAc = "" : BlnCredit = True
                End If
            End If
            StrPrvDocId = AgL.XNull(DTTemp.Rows(I).Item("DocId"))
            If (DTTemp.Rows.Count - 1) = I Then GoTo LblForLastRecord
        Next
        If DblOpening = 0 Then
            If Not DTTemp.Rows.Count > 0 Then MsgBox("No Records Found to Print.") : Exit Sub
        End If
        FBankBookDouble(DTbankBook, DblOpening)
    End Sub
    Private Sub FBankBookDouble(ByVal DTTemp As DataTable, ByVal DblOpening As Double)
        Dim CrPos As Integer = 0
        Dim DrPos As Integer = 0
        Dim StrVDate As String = ""
        Dim DT_CSHBook As DataTable
        DT_CSHBook = New DataTable("BankBook")
        With DT_CSHBook.Columns
            .Add("CVDate", System.Type.GetType("System.DateTime"))
            .Add("CVNo", System.Type.GetType("System.String"))
            .Add("CType", System.Type.GetType("System.String"))
            .Add("CParticular", System.Type.GetType("System.String"))
            .Add("AmtCr", System.Type.GetType("System.Double"))
            .Add("CChqNo", System.Type.GetType("System.String"))
            .Add("CChqDt", System.Type.GetType("System.String"))
            .Add("NarrationCr", System.Type.GetType("System.String"))
            .Add("DVNo", System.Type.GetType("System.String"))
            .Add("DType", System.Type.GetType("System.String"))
            .Add("DParticular", System.Type.GetType("System.String"))
            .Add("AmtDr", System.Type.GetType("System.Double"))
            .Add("DChqNo", System.Type.GetType("System.String"))
            .Add("DChqDt", System.Type.GetType("System.String"))
            .Add("NarrationDr", System.Type.GetType("System.String"))
            .Add("OP", System.Type.GetType("System.Double"))
            .Add("PageWise", System.Type.GetType("System.String"))
        End With
        For mCnt As Integer = 0 To DTTemp.Rows.Count - 1
            With DTTemp.Rows(mCnt)
                If StrVDate <> AgL.XNull(.Item("VDate")) Then
                    If DrPos > CrPos Then CrPos = DrPos Else DrPos = CrPos
                    FAddOpening(IIf(DblOpening > 0, DblOpening, 0), IIf(DblOpening > 0, 0, Math.Abs(DblOpening)), DT_CSHBook,
                                CrPos, DrPos, AgL.XNull(.Item("VDate")))
                End If
                FAddRow(AgL.VNull(.Item("AmtDr")), AgL.VNull(.Item("AmtCr")), DT_CSHBook, AgL.XNull(.Item("VNo")),
                        AgL.XNull(.Item("Type")), AgL.XNull(.Item("Particular")), AgL.XNull(.Item("VDate")), CrPos, DrPos, DblOpening,
                        AgL.XNull(.Item("ChqNo")), AgL.XNull(.Item("ChqDt")), AgL.XNull(.Item("Narration")))
                DT_CSHBook.Rows(DT_CSHBook.Rows.Count - 1).Item("PageWise") = IIf(Trim(FGMain(GFilterCode, 3).Value) = "", "N", Trim(FGMain(GFilterCode, 4).Value))
                StrVDate = AgL.XNull(.Item("VDate"))
            End With
        Next
        If Not DTTemp.Rows.Count > 0 Then MsgBox("No Records Found to Print.") : Exit Sub
        FLoadMainReport("BankBook", DT_CSHBook)
        CMain.FormulaSet(RptMain, Me.Text, FGMain)
        CMain.FShowReport(RptMain, Me.MdiParent, Me.Text)
    End Sub
    Private Sub FBank_CashBookSingle()
        Dim StrConditionOP As String
        Dim StrCondition1 As String
        Dim DTTemp As DataTable
        Dim I As Integer, J As Integer, IntPosition As Integer
        Dim StrDebitAc As String, StrCreditAc As String
        Dim BlnDebit As Boolean, BlnCredit As Boolean
        Dim StrMainSubCode As String
        Dim StrPrvDocId As String
        Dim SQL As String
        Dim DblOpening As Double = 0
        Dim StrChkFieldFor As String, StrChkDataFor As String
        Dim StrVDate As String
        Dim StrVNo As String
        Dim StrVType As String
        Dim StrNCat As String
        Dim StrVParticular As String
        Dim DblAmtDr As Double
        Dim DblAmtCr As Double
        Dim StrChqNo As String
        Dim StrChqDt As String
        Dim StrNarration As String
        Dim StrWithnarration As String
        Dim DTbankBook As DataTable

        If Not FIsValid(0) Then Exit Sub
        If Not FIsValid(1) Then Exit Sub
        If Not FIsValid(3) Then Exit Sub
        If Not FIsValid(4) Then Exit Sub

        StrWithnarration = "N"
        DTbankBook = New DataTable("BankBook")
        With DTbankBook.Columns
            .Add("VDate", System.Type.GetType("System.DateTime"))
            .Add("VNo", System.Type.GetType("System.String"))
            .Add("Type", System.Type.GetType("System.String"))
            .Add("Particular", System.Type.GetType("System.String"))
            .Add("AmtCr", System.Type.GetType("System.Double"))
            .Add("AmtDr", System.Type.GetType("System.Double"))
            .Add("ChqNo", System.Type.GetType("System.String"))
            .Add("ChqDt", System.Type.GetType("System.String"))
            .Add("Narration", System.Type.GetType("System.String"))
            .Add("OP", System.Type.GetType("System.Double"))
            .Add("PageWise", System.Type.GetType("System.String"))
            .Add("WithNarration", System.Type.GetType("System.String"))
        End With

        StrMainSubCode = UCase(Trim(FGMain(GFilterCode, 4).Value))
        StrCondition1 = " Where ( Date(LG.V_Date) Between " & AgL.Chk_Date(CDate(FGMain(GFilter, 0).Value.ToString).ToString("s")) & " And " & AgL.Chk_Date(CDate(FGMain(GFilter, 1).Value.ToString).ToString("s")) & ") "
        If Trim(FGMain(GFilterCode, 2).Value) <> "" Then
            StrCondition1 = StrCondition1 & " And LG.Site_Code  IN (" & FGMain(GFilterCode, 2).Value & ") "
            StrConditionOP = " And LG.Site_Code  IN (" & FGMain(GFilterCode, 2).Value & ") "
        Else
            StrCondition1 = StrCondition1 & " And LG.Site_Code  IN (" & AgL.PubSiteList & ") "
            StrConditionOP = " And LG.Site_Code  IN (" & AgL.PubSiteList & ") "
        End If

        If Trim(FGMain(GFilterCode, 3).Value) <> "" Then
            StrCondition1 = StrCondition1 & " And LG.DivCode  IN (" & FGMain(GFilterCode, 3).Value & ") "
            StrConditionOP = StrConditionOP & " And LG.DivCode  IN (" & FGMain(GFilterCode, 3).Value & ") "
        Else
            StrCondition1 = StrCondition1 & " And LG.DivCode  IN (" & AgL.PubDivisionList & ") "
            StrConditionOP = StrConditionOP & " And LG.DivCode  IN (" & AgL.PubDivisionList & ") "
        End If

        If Trim(FGMain(GFilterCode, 6).Value) <> "" Then
            StrWithnarration = Trim(FGMain(GFilterCode, 6).Value)
        End If

        SQL = "Select  (IfNull(Sum(AmtCr),0)-IfNull(Sum(AmtDr),0)) As OP,Max(V_Date) As V_Date From Ledger LG "
        SQL = SQL + "Left Join SubGroup SG On LG.SubCode=SG.SubCode  "
        SQL = SQL + "Where  V_Date<" & AgL.Chk_Date(CDate(FGMain(GFilter, 0).Value.ToString).ToString("s")) & " "
        SQL = SQL + "And " & "(LG.subcode IN ('" & FGMain(GFilterCode, 4).Value & "')) " & StrConditionOP


        DTTemp = CMain.FGetDatTable(SQL, AgL.GCn)
        If DTTemp.Rows.Count > 0 Then DblOpening = AgL.VNull(DTTemp.Rows(0).Item("OP"))


        If DblOpening <> 0 Then
            StrVDate = FGMain(GFilter, 0).Value
            StrVParticular = "Opening Balance"
            StrVType = "OPBAL"

            If DblOpening < 0 Then
                DblAmtCr = Math.Abs(DblOpening)
            Else
                DblAmtDr = Math.Abs(DblOpening)
            End If

            FAddRowBankCash(DTbankBook, "", StrVType, StrVParticular, StrVDate, "", "", "", DblAmtDr, DblAmtCr, DblOpening, StrWithnarration)
        End If

        'ABCD

        If FGMain(GFilter, 9).Value = "Day Wise Summary" Then
            SQL = "Select LG.V_Date As DocId,0 As AmtDr,Sum(LG.AmtDr) As AmtCr,LG.V_Date, "
            SQL += "LG.V_Date  As RecId,LG.V_Date As V_Type, "
            SQL += "Max(LG.Chq_No) As Chq_No,Max(LG.Chq_Date) As Chq_Date,Null As SubCode,Max(LG.ContraSub) As ContraSub,Max(LG.Narration) As Narration, "
            SQL += "Max(SG.Name) As PName,Max(SGC.Name) As CName,Max(IfNull(VT.SerialNo,0)) As SerialNo "
            SQL += "From Ledger LG "
            SQL += "Left Join SubGroup SG On LG.SubCode=SG.SubCode "
            SQL += "Left Join SubGroup SGC On LG.ContraSub=SGC.SubCode "
            SQL += "Left Join Voucher_Type VT On VT.V_Type=LG.V_Type "
            SQL += StrCondition1

            SQL += "And (IfNull(LG.TDSCategory,'')='' Or IfNull(LG.System_Generated,'N')<>'Y') "

            SQL += "And DocId In "
            SQL += "(Select DocId From Ledger LG Where LG.SubCode='" & StrMainSubCode & "') "
            SQL += "And VT.NCat = 'SI'  "
            SQL += "And LG.SubCode =  '" & StrMainSubCode & "' "
            SQL += "Group By LG.V_Date  "
            SQL += "Order By LG.V_Date "
            Dim DtDateWiseSummary As DataTable = AgL.FillData(SQL, AgL.GCn).Tables(0)
            For I = 0 To DtDateWiseSummary.Rows.Count - 1
                FAddRowBankCash(DTbankBook, "Sale Invoice",
                                AgL.XNull(DtDateWiseSummary.Rows(I)("V_Type")),
                                "Sale Invoice",
                                AgL.XNull(DtDateWiseSummary.Rows(I)("V_Date")),
                                "", "", "",
                                AgL.VNull(DtDateWiseSummary.Rows(I)("AmtDr")),
                                AgL.VNull(DtDateWiseSummary.Rows(I)("AmtCr")), 0, StrWithnarration)
            Next
        End If


        SQL = "Select LG.DocId,LG.AmtDr,LG.AmtCr,LG.V_Date, "
        SQL += "LG.DivCode || LG.Site_Code || '-' || LG.V_Type " & IIf(FGMain(GFilter, 8).Value = "No", "", "|| '-' || LG.RecId") & "  As RecId,LG.V_Type, "
        SQL += "VT.NCat,LG.Chq_No,LG.Chq_Date,LG.SubCode,LG.ContraSub,LG.Narration, "
        SQL += "SG.Name As PName,SGC.Name As CName,IfNull(VT.SerialNo,0) As SerialNo "
        SQL += "From Ledger LG "
        SQL += "Left Join SubGroup SG On LG.SubCode=SG.SubCode "
        SQL += "Left Join SubGroup SGC On LG.ContraSub=SGC.SubCode "
        SQL += "Left Join Voucher_Type VT On VT.V_Type=LG.V_Type "
        SQL += StrCondition1

        SQL += "And (IfNull(LG.TDSCategory,'')='' Or IfNull(LG.System_Generated,'N')<>'Y') "

        SQL += "And DocId In "
        SQL += "(Select DocId From Ledger LG Where LG.SubCode='" & StrMainSubCode & "') "
        'SQL += "Order By LG.V_Date,LG.V_No,LG.DocId,LG.AmtDr "
        If FGMain(GFilter, 9).Value = "Day Wise Summary" Then
            SQL += "And VT.NCat <> 'SI'  "
        End If


        SQL += "Order By LG.V_Date,SerialNo,LG.RecID,LG.DocId,LG.AmtDr "


        DTTemp = CMain.FGetDatTable(SQL, AgL.GCn)
        If DTTemp.Rows.Count > 0 Then StrPrvDocId = AgL.XNull(DTTemp.Rows(I).Item("DocId")) Else StrPrvDocId = ""
        IntPosition = 0
        StrDebitAc = ""
        StrCreditAc = ""
        StrVDate = "" : StrVNo = "" : StrVType = "" : StrNCat = "" : StrVParticular = "" : DblAmtDr = 0 : DblAmtCr = 0
        StrChqNo = "" : StrChqDt = "" : StrNarration = ""
        BlnDebit = False : BlnCredit = False

        For I = 0 To DTTemp.Rows.Count - 1
            If StrPrvDocId <> AgL.XNull(DTTemp.Rows(I).Item("DocId")) Then
LblForLastRecord:
                For J = IntPosition To IIf((DTTemp.Rows.Count - 1) = I, I, I - 1)
                    StrVDate = AgL.XNull(DTTemp.Rows(J).Item("V_Date"))
                    StrVNo = AgL.XNull(DTTemp.Rows(J).Item("RecId"))
                    StrVType = AgL.XNull(DTTemp.Rows(J).Item("V_Type"))
                    StrNCat = AgL.XNull(DTTemp.Rows(J).Item("NCAT"))

                    'Conditions
                    If StrDebitAc = "" And StrCreditAc = "" Then
                        'Case 5 & 3
                        If StrMainSubCode = Trim(UCase(AgL.XNull(DTTemp.Rows(J).Item("SubCode")))) Then
                            If Trim(UCase(AgL.XNull(DTTemp.Rows(J).Item("ContraSub")))) <> "" Then StrVParticular = AgL.XNull(DTTemp.Rows(J).Item("CName")) Else StrVParticular = ""
                            DblAmtCr = AgL.VNull(DTTemp.Rows(J).Item("AmtDr"))
                            DblAmtDr = AgL.VNull(DTTemp.Rows(J).Item("AmtCr"))
                            StrChqNo = AgL.XNull(DTTemp.Rows(J).Item("Chq_No"))
                            StrChqDt = AgL.XNull(DTTemp.Rows(J).Item("Chq_Date"))
                            StrNarration = AgL.XNull(DTTemp.Rows(J).Item("Narration"))
                            FAddRowBankCash(DTbankBook, StrVNo, StrVType, StrVParticular, StrVDate, StrChqNo, StrChqDt, StrNarration, DblAmtDr, DblAmtCr, 0, StrWithnarration)
                        End If
                    ElseIf (StrDebitAc <> "" Or StrCreditAc <> "") And (UCase(Trim(StrDebitAc)) = StrMainSubCode Or UCase(Trim(StrCreditAc)) = StrMainSubCode) Then
                        'Case 1 & 4
                        StrChkFieldFor = ""
                        If StrDebitAc <> "" And UCase(Trim(StrDebitAc)) = StrMainSubCode Then StrChkFieldFor = "AmtCr"
                        If StrCreditAc <> "" And UCase(Trim(StrCreditAc)) = StrMainSubCode Then StrChkFieldFor = "AmtDr"

                        If AgL.VNull(DTTemp.Rows(J).Item(StrChkFieldFor)) > 0 Or StrNCat = "OB" Then
                            If StrMainSubCode <> Trim(UCase(AgL.XNull(DTTemp.Rows(J).Item("SubCode")))) Or StrNCat = "OB" Then
                                StrVParticular = AgL.XNull(DTTemp.Rows(J).Item("PName"))
                                If StrNCat = "OB" Then
                                    DblAmtCr = AgL.VNull(DTTemp.Rows(J).Item("AmtDr"))
                                    DblAmtDr = AgL.VNull(DTTemp.Rows(J).Item("AmtCr"))
                                Else
                                    DblAmtCr = AgL.VNull(DTTemp.Rows(J).Item("AmtCr"))
                                    DblAmtDr = AgL.VNull(DTTemp.Rows(J).Item("AmtDr"))
                                End If
                                StrChqNo = AgL.XNull(DTTemp.Rows(J).Item("Chq_No"))
                                StrChqDt = AgL.XNull(DTTemp.Rows(J).Item("Chq_Date"))
                                StrNarration = AgL.XNull(DTTemp.Rows(J).Item("Narration"))
                                FAddRowBankCash(DTbankBook, StrVNo, StrVType, StrVParticular, StrVDate, StrChqNo, StrChqDt, StrNarration, DblAmtDr, DblAmtCr, 0, StrWithnarration)
                            End If
                        End If
                    ElseIf StrDebitAc <> "" Or StrCreditAc <> "" Then
                        'Case 2
                        StrChkFieldFor = ""
                        StrChkDataFor = ""

                        If StrDebitAc <> "" Then StrChkFieldFor = "AmtCr" : StrChkDataFor = Trim(UCase(StrDebitAc))
                        If StrCreditAc <> "" Then StrChkFieldFor = "AmtDr" : StrChkDataFor = Trim(UCase(StrCreditAc))

                        If StrMainSubCode = Trim(UCase(AgL.XNull(DTTemp.Rows(J).Item("SubCode")))) Then
                            StrVParticular = AgL.XNull(DTTemp.Rows(J).Item("CName"))
                            DblAmtCr = AgL.VNull(DTTemp.Rows(J).Item("AmtDr"))
                            DblAmtDr = AgL.VNull(DTTemp.Rows(J).Item("AmtCr"))
                            StrChqNo = AgL.XNull(DTTemp.Rows(J).Item("Chq_No"))
                            StrChqDt = AgL.XNull(DTTemp.Rows(J).Item("Chq_Date"))
                            StrNarration = AgL.XNull(DTTemp.Rows(J).Item("Narration"))

                            FAddRowBankCash(DTbankBook, StrVNo, StrVType, StrVParticular, StrVDate, StrChqNo, StrChqDt, StrNarration, DblAmtDr, DblAmtCr, 0, StrWithnarration)
                        End If
                    End If
                Next
                IntPosition = I
                StrDebitAc = ""
                StrCreditAc = ""
                StrVDate = "" : StrVNo = "" : StrVType = "" : StrVParticular = "" : DblAmtDr = 0 : DblAmtCr = 0
                StrChqNo = "" : StrChqDt = "" : StrNarration = ""
                BlnDebit = False : BlnCredit = False
                If (DTTemp.Rows.Count - 1) = I Then Exit For
            End If

            If AgL.VNull(DTTemp.Rows(I).Item("AmtDr")) > 0 Then
                If Not BlnDebit Then
                    If Trim(StrDebitAc) = "" Then StrDebitAc = AgL.XNull(DTTemp.Rows(I).Item("SubCode")) Else StrDebitAc = "" : BlnDebit = True
                End If
            End If

            If AgL.VNull(DTTemp.Rows(I).Item("AmtCr")) > 0 Then
                If Not BlnCredit Then
                    If Trim(StrCreditAc) = "" Then StrCreditAc = AgL.XNull(DTTemp.Rows(I).Item("SubCode")) Else StrCreditAc = "" : BlnCredit = True
                End If
            End If
            StrPrvDocId = AgL.XNull(DTTemp.Rows(I).Item("DocId"))
            If (DTTemp.Rows.Count - 1) = I Then GoTo LblForLastRecord
        Next

        If DblOpening = 0 Then
            If Not DTTemp.Rows.Count > 0 Then MsgBox("No Records Found to Print.") : Exit Sub
        End If

        If FGMain(GFilter, 9).Value = "Day Wise Summary" Then
            DTbankBook.DefaultView.Sort = "VDate ASC"
            DTbankBook = DTbankBook.DefaultView.ToTable
        End If

        FLoadMainReport("BankBookSingle", DTbankBook)
        CMain.FormulaSet(RptMain, Me.Text, FGMain)
        CMain.FShowReport(RptMain, Me.MdiParent, Me.Text)
    End Sub
    Private Sub FAddRowBankCash(ByVal DTTable As DataTable,
     ByVal StrVNo As String, ByVal StrVType As String, ByVal StrParticular As String, ByVal StrVDate As Date,
     ByVal StrChqNo As String, ByVal StrChqDt As String, ByVal StrNarration As String,
     ByVal DblAmtDr As Double, ByVal DblAmtCr As Double, ByVal DblOpening As Double, ByVal StrWithnarration As String)

        Dim DRRow As DataRow = DTTable.NewRow
        DRRow("VDate") = StrVDate
        DRRow("VNo") = StrVNo
        DRRow("Type") = StrVType
        DRRow("Particular") = StrParticular
        DRRow("AmtCr") = DblAmtCr
        DRRow("AmtDr") = DblAmtDr
        DRRow("OP") = DblOpening
        DRRow("ChqNo") = StrChqNo
        DRRow("ChqDt") = StrChqDt
        DRRow("Narration") = StrNarration
        DRRow("WithNarration") = StrWithnarration
        DTTable.Rows.Add(DRRow)
    End Sub
    Private Sub FAgeing()
        Dim D1, D2, D3, D4, D5, D6 As Integer
        Dim StrCondition1 As String, Strconditionsite As String, StrConditionGrpOn As String, StrChoice As String
        Dim STRDATE, StrAmtCr, StrAmtDr, Reptitle As String
        Dim Repdebit, Repcredit, RepDays As String
        Dim DTTemp As DataTable

        If Not FIsValid(0) Then Exit Sub
        If Not FIsValid(1) Then Exit Sub
        If Not FIsValid(2) Then Exit Sub
        If Not FIsValid(3) Then Exit Sub
        If Not FIsValid(4) Then Exit Sub
        If Not FIsValid(5) Then Exit Sub
        If Not FIsValid(6) Then Exit Sub
        If Not FIsValid(7) Then Exit Sub
        If Not FIsValid(8) Then Exit Sub
        If Not FIsValid(9) Then Exit Sub

        If Val((FGMain(GFilter, 3).Value.ToString)) > Val((FGMain(GFilter, 4).Value.ToString)) Then MsgBox("II Interval Must Be Greater Than I Interval ") : Exit Sub
        If Val((FGMain(GFilter, 4).Value.ToString)) > Val((FGMain(GFilter, 5).Value.ToString)) Then MsgBox("III Interval Must Be Greater Than II Interval ") : Exit Sub
        If Val((FGMain(GFilter, 5).Value.ToString)) > Val((FGMain(GFilter, 6).Value.ToString)) Then MsgBox("IV Interval Must Be Greater Than III Interval ") : Exit Sub
        If Val((FGMain(GFilter, 6).Value.ToString)) > Val((FGMain(GFilter, 7).Value.ToString)) Then MsgBox("V Interval Must Be Greater Than IV Interval ") : Exit Sub
        If Val((FGMain(GFilter, 7).Value.ToString)) > Val((FGMain(GFilter, 8).Value.ToString)) Then MsgBox("VI Interval Must Be Greater Than V Interval ") : Exit Sub
        Strconditionsite = ""
        STRDATE = AgL.Chk_Date(CDate(FGMain(GFilter, 0).Value.ToString).ToString("s"))
        StrCondition1 = " LG.V_Date <= " & AgL.Chk_Date(CDate(FGMain(GFilter, 0).Value.ToString).ToString("s")) & " "
        If Trim(FGMain(GFilterCode, 1).Value) <> "" Then StrCondition1 = StrCondition1 & " And ag.nature In ('" & FGMain(GFilterCode, 1).Value & "')"
        If Trim(FGMain(GFilterCode, 11).Value) <> "" Then StrCondition1 = StrCondition1 & " And Sg.Subcode In (" & FGMain(GFilterCode, 11).Value & ")"
        If Trim(FGMain(GFilterCode, 2).Value) <> "" Then
            Strconditionsite = Strconditionsite & "  And LG.site_Code In(" & FGMain(GFilterCode, 2).Value & ") "
        Else
            Strconditionsite = Strconditionsite & " And LG.site_Code In(" & AgL.PubSiteList & ") "
        End If

        If Trim(FGMain(GFilterCode, 12).Value) <> "" Then
            Strconditionsite = Strconditionsite & "  And LG.DivCode In(" & FGMain(GFilterCode, 12).Value & ") "
        Else
            Strconditionsite = Strconditionsite & " And LG.DivCode In(" & AgL.PubDivisionList & ") "
        End If

        If Trim(FGMain(GFilterCode, 14).Value) <> "" Then StrCondition1 = StrCondition1 & " And (IfNull(SG.GroupCode,'') In (Select IfNull(AGP.GroupCode,'') From AcGroupPath AGP Where AGP.GroupUnder In (" & FGMain(GFilterCode, 14).Value & ")) Or SG.GroupCode In (" & FGMain(GFilterCode, 14).Value & ")) "


        If FGMain(GFilterCode, 1).Value = "Customer" Then
            StrAmtDr = "AmtDr"
            StrAmtCr = "AmtCr"
            Reptitle = "Ageing Analysis of Debtors"
            Repdebit = "Total Debit"
            Repcredit = "Total Credit"
            RepDays = "Amount Debited From Days"
        Else
            StrAmtDr = "AmtCr"
            StrAmtCr = "AmtDr"
            Reptitle = "Ageing Analysis of Creditors"
            Repdebit = "Total Credit"
            Repcredit = "Total Debit"
            RepDays = "Amount Credited From Days"
        End If

        If FGMain(GFilterCode, 10).Value <> "AC" Then
            StrConditionGrpOn = " Group By AG.GroupName "
            StrChoice = "AG"
        Else
            StrConditionGrpOn = " Group By SG.Name "
            StrChoice = "AC"
        End If

        D1 = Val((FGMain(GFilter, 3).Value.ToString))
        D2 = Val((FGMain(GFilter, 4).Value.ToString))
        D3 = Val((FGMain(GFilter, 5).Value.ToString))
        D4 = Val((FGMain(GFilter, 6).Value.ToString))
        D5 = Val((FGMain(GFilter, 7).Value.ToString))
        D6 = Val((FGMain(GFilter, 8).Value.ToString))

        ''*********** For trans Purpose **************''
        StrSQLQuery = "Select  IfNull(Sum(" & StrAmtDr & "),0) As Amt1,0 As Amt2,0 As Amt3,0 As Amt4,0 As Amt5,0 As Amt6,0 As Amt7,0 As AmtPR, "
        StrSQLQuery = StrSQLQuery + " max(Sg.Name) As PName,Max(SM.Name) As Division,0 As AmtPR_Contra,Max(AG.GroupName) as GroupName, IfNull(Max(SG.CreditLimit),0) as CreditLimit ,IfNull(Max(Sg.CreditDays),0) as DueDays,IfNull(Max(CT.CityName),'') AS CityName    From Ledger As Lg "
        StrSQLQuery = StrSQLQuery + " Left Join SubGroup As SG On Lg.SubCode=Sg.SubCode "
        StrSQLQuery = StrSQLQuery + " Left Join AcGroup As AG On ag.GroupCode=Sg.GroupCode "
        StrSQLQuery = StrSQLQuery + " Left Join Sitemast As SM On SM.Code=Lg.site_Code "
        StrSQLQuery = StrSQLQuery + " LEFT JOIN City CT ON CT.CityCode =SG.CityCode "
        StrSQLQuery = StrSQLQuery + " Where julianday(" & STRDATE & ")  - julianday(V_Date)>=0 And "
        StrSQLQuery = StrSQLQuery + " julianday(" & STRDATE & ")  - julianday(V_Date)<= " & D1 & "  And "
        StrSQLQuery = StrSQLQuery + StrCondition1 + Strconditionsite
        StrSQLQuery = StrSQLQuery + "And Lg.V_Type<>'F_AO'   "
        StrSQLQuery = StrSQLQuery + StrConditionGrpOn

        StrSQLQuery = StrSQLQuery + " Union All "

        StrSQLQuery = StrSQLQuery + " Select  0 As Amt1,IfNull(Sum(" & StrAmtDr & "),0) As Amt2,0 As Amt3,0 As Amt4,0 As Amt5,0 As Amt6,0 As Amt7,0 As AmtPR, "
        StrSQLQuery = StrSQLQuery + " max(Sg.Name) As PName,Max(SM.Name) As Division,0 As AmtPR_Contra,Max(AG.GroupName) as GroupName,IfNull(Max(SG.CreditLimit),0) as CreditLimit ,IfNull(Max(Sg.CreditDays),0) as DueDays,IfNull(Max(CT.CityName),'') AS CityName    From Ledger As Lg "
        StrSQLQuery = StrSQLQuery + " Left Join SubGroup As SG On Lg.SubCode=Sg.SubCode "
        StrSQLQuery = StrSQLQuery + " Left Join AcGroup As AG On ag.GroupCode=Sg.GroupCode "
        StrSQLQuery = StrSQLQuery + " Left Join Sitemast As SM On SM.Code=Lg.site_Code "
        StrSQLQuery = StrSQLQuery + " LEFT JOIN City CT ON CT.CityCode =SG.CityCode "
        StrSQLQuery = StrSQLQuery + " Where julianday(" & STRDATE & ")  - julianday(V_Date)>" & D1 & " And "
        StrSQLQuery = StrSQLQuery + " julianday(" & STRDATE & ")  - julianday(V_Date)<=" & D2 & "  And "
        StrSQLQuery = StrSQLQuery + StrCondition1 + Strconditionsite
        StrSQLQuery = StrSQLQuery + " And Lg.V_Type<>'F_AO'   "
        StrSQLQuery = StrSQLQuery + StrConditionGrpOn

        StrSQLQuery = StrSQLQuery + " Union All "

        StrSQLQuery = StrSQLQuery + " Select  0 As Amt1,0 As Amt2,IfNull(Sum(" & StrAmtDr & "),0) As Amt3,0 As Amt4,0 As Amt5,0 As Amt6,0 As Amt7,0 As AmtPR,  "
        StrSQLQuery = StrSQLQuery + " max(Sg.Name) As PName,max(sm.name) As Division,0 As AmtPR_Contra,Max(AG.GroupName) as GroupName,IfNull(Max(SG.CreditLimit),0) as CreditLimit ,IfNull(Max(Sg.CreditDays),0) as DueDays,IfNull(Max(CT.CityName),'') AS CityName   From Ledger As Lg "
        StrSQLQuery = StrSQLQuery + " Left Join SubGroup As SG On Lg.SubCode=Sg.SubCode"
        StrSQLQuery = StrSQLQuery + " Left Join AcGroup As AG On ag.GroupCode=Sg.GroupCode "
        StrSQLQuery = StrSQLQuery + " Left Join Sitemast As SM On SM.Code=Lg.site_Code "
        StrSQLQuery = StrSQLQuery + " LEFT JOIN City CT ON CT.CityCode =SG.CityCode "
        StrSQLQuery = StrSQLQuery + " Where julianday(" & STRDATE & ")  - julianday(V_Date)>" & D2 & " And "
        StrSQLQuery = StrSQLQuery + " julianday(" & STRDATE & ")  - julianday(V_Date)<=" & D3 & "  And "
        StrSQLQuery = StrSQLQuery + StrCondition1 + Strconditionsite
        StrSQLQuery = StrSQLQuery + " And Lg.V_Type<>'F_AO'   "
        StrSQLQuery = StrSQLQuery + StrConditionGrpOn

        StrSQLQuery = StrSQLQuery + " Union All "

        StrSQLQuery = StrSQLQuery + " Select  0 As Amt1,0 As Amt2,0 As Amt3,IfNull(Sum(" & StrAmtDr & "),0) As Amt4,0 As Amt5,0 As Amt6,0 As Amt7,0 As AmtPR,"
        StrSQLQuery = StrSQLQuery + " max(Sg.Name) As PName,max(sm.name) As Division,0 As AmtPR_Contra,Max(AG.GroupName) as GroupName,IfNull(Max(SG.CreditLimit),0) as CreditLimit ,IfNull(Max(Sg.CreditDays),0) as DueDays ,IfNull(Max(CT.CityName),'') AS CityName   From Ledger As Lg "
        StrSQLQuery = StrSQLQuery + " Left Join SubGroup As SG On Lg.SubCode=Sg.SubCode"
        StrSQLQuery = StrSQLQuery + " Left Join AcGroup As AG On ag.GroupCode=Sg.GroupCode "
        StrSQLQuery = StrSQLQuery + " Left Join Sitemast As SM On SM.Code=Lg.site_Code "
        StrSQLQuery = StrSQLQuery + " LEFT JOIN City CT ON CT.CityCode =SG.CityCode "
        StrSQLQuery = StrSQLQuery + " Where julianday(" & STRDATE & ")  - julianday(V_Date)>" & D3 & " And "
        StrSQLQuery = StrSQLQuery + " julianday(" & STRDATE & ")  - julianday(V_Date)<=" & D4 & "  And "
        StrSQLQuery = StrSQLQuery + StrCondition1 + Strconditionsite
        StrSQLQuery = StrSQLQuery + " And Lg.V_Type<>'F_AO'   "
        StrSQLQuery = StrSQLQuery + StrConditionGrpOn

        StrSQLQuery = StrSQLQuery + " Union All "

        StrSQLQuery = StrSQLQuery + "Select  0 As Amt1,0 As Amt2,0 As Amt3,0 As Amt4,IfNull(Sum(" & StrAmtDr & "),0) As Amt5,0 As Amt6,0 As Amt7,0 As AmtPR,"
        StrSQLQuery = StrSQLQuery + " max(Sg.Name) As PName,max(sm.name) As Division,0 As AmtPR_Contra,Max(AG.GroupName) as GroupName,IfNull(Max(SG.CreditLimit),0) as CreditLimit ,IfNull(Max(Sg.CreditDays),0) as DueDays,IfNull(Max(CT.CityName),'') AS CityName   From Ledger As Lg "
        StrSQLQuery = StrSQLQuery + " Left Join SubGroup As SG On Lg.SubCode=Sg.SubCode"
        StrSQLQuery = StrSQLQuery + " Left Join AcGroup As AG On ag.GroupCode=Sg.GroupCode "
        StrSQLQuery = StrSQLQuery + " Left Join Sitemast As SM On SM.Code=Lg.site_Code "
        StrSQLQuery = StrSQLQuery + " LEFT JOIN City CT ON CT.CityCode =SG.CityCode "
        StrSQLQuery = StrSQLQuery + " Where julianday(" & STRDATE & ")  - julianday(V_Date)>" & D4 & " And "
        StrSQLQuery = StrSQLQuery + " julianday(" & STRDATE & ")  - julianday(V_Date)<=" & D5 & "  And "
        StrSQLQuery = StrSQLQuery + StrCondition1 + Strconditionsite
        StrSQLQuery = StrSQLQuery + " And Lg.V_Type<>'F_AO'   "
        StrSQLQuery = StrSQLQuery + StrConditionGrpOn

        StrSQLQuery = StrSQLQuery + " Union All "


        StrSQLQuery = StrSQLQuery + " Select  0 As Amt1,0 As Amt2,0 As Amt3,0 As Amt4,0 As Amt5,IfNull(Sum(" & StrAmtDr & "),0) As Amt6,0 As Amt7,0 As AmtPR,"
        StrSQLQuery = StrSQLQuery + " max(Sg.Name) As PName,max(sm.name) As Division,0 As AmtPR_Contra ,Max(AG.GroupName) as GroupName,IfNull(Max(SG.CreditLimit),0) as CreditLimit ,IfNull(Max(Sg.CreditDays),0) as DueDays,IfNull(Max(CT.CityName),'') AS CityName   From Ledger As Lg "
        StrSQLQuery = StrSQLQuery + " Left Join SubGroup As SG On Lg.SubCode=Sg.SubCode"
        StrSQLQuery = StrSQLQuery + " Left Join AcGroup As AG On ag.GroupCode=Sg.GroupCode "
        StrSQLQuery = StrSQLQuery + " Left Join Sitemast As SM On SM.Code=Lg.site_Code "
        StrSQLQuery = StrSQLQuery + " LEFT JOIN City CT ON CT.CityCode =SG.CityCode "
        StrSQLQuery = StrSQLQuery + " Where julianday(" & STRDATE & ")  - julianday(V_Date)>" & D5 & " And "
        StrSQLQuery = StrSQLQuery + " julianday(" & STRDATE & ")  - julianday(V_Date)<=" & D6 & "  And "
        StrSQLQuery = StrSQLQuery + StrCondition1 + Strconditionsite
        StrSQLQuery = StrSQLQuery + " And Lg.V_Type<>'F_AO'   "
        StrSQLQuery = StrSQLQuery + StrConditionGrpOn

        StrSQLQuery = StrSQLQuery + " Union All "

        StrSQLQuery = StrSQLQuery + "Select  0 As Amt1,0 As Amt2,0 As Amt3,0 As Amt4,0 As Amt5,0 As Amt6,IfNull(Sum(" & StrAmtDr & "),0) As Amt7,0 As AmtPR,  "
        StrSQLQuery = StrSQLQuery + " max(Sg.Name) As PName,max(sm.name) As Division,0 As AmtPR_Contra,Max(AG.GroupName) as GroupName,IfNull(Max(SG.CreditLimit),0) as CreditLimit ,IfNull(Max(Sg.CreditDays),0) as DueDays,IfNull(Max(CT.CityName),'') AS CityName   From Ledger As Lg "
        StrSQLQuery = StrSQLQuery + " Left Join SubGroup As SG On Lg.SubCode=Sg.SubCode"
        StrSQLQuery = StrSQLQuery + " Left Join AcGroup As AG On ag.GroupCode=Sg.GroupCode "
        StrSQLQuery = StrSQLQuery + " Left Join Sitemast As SM On SM.Code=Lg.site_Code "
        StrSQLQuery = StrSQLQuery + " LEFT JOIN City CT ON CT.CityCode =SG.CityCode "
        StrSQLQuery = StrSQLQuery + " Where julianday(" & STRDATE & ")  - julianday(V_Date)>" & D6 & " And "
        StrSQLQuery = StrSQLQuery + StrCondition1 + Strconditionsite
        StrSQLQuery = StrSQLQuery + " And Lg.V_Type<>'F_AO'   "
        StrSQLQuery = StrSQLQuery + StrConditionGrpOn

        StrSQLQuery = StrSQLQuery + " Union All "

        StrSQLQuery = StrSQLQuery + " Select  0 As Amt1,0 As Amt2,0 As Amt3,0"
        StrSQLQuery = StrSQLQuery + " As Amt4,0 As Amt5,0 As Amt6,0 As Amt7,IfNull(Sum(" & StrAmtCr & " ),0) As AmtPR,  "
        StrSQLQuery = StrSQLQuery + " max(Sg.Name) As PName,max(sm.name) As Division,IfNull(Sum(" & StrAmtDr & " ),0) As AmtPR_Contra,Max(AG.GroupName) as GroupName, "
        StrSQLQuery = StrSQLQuery + " IfNull(Max(SG.CreditLimit),0) as CreditLimit ,IfNull(Max(Sg.CreditDays),0) as DueDays,IfNull(Max(CT.CityName),'') AS CityName   From Ledger As Lg "
        StrSQLQuery = StrSQLQuery + " Left Join SubGroup As SG On Lg.SubCode=Sg.SubCode"
        StrSQLQuery = StrSQLQuery + " Left Join AcGroup As AG On ag.GroupCode=Sg.GroupCode "
        StrSQLQuery = StrSQLQuery + " Left Join Sitemast As SM On SM.Code=Lg.site_Code "
        StrSQLQuery = StrSQLQuery + " LEFT JOIN City CT ON CT.CityCode =SG.CityCode "
        StrSQLQuery = StrSQLQuery + " Where "
        StrSQLQuery = StrSQLQuery + StrCondition1 + Strconditionsite
        StrSQLQuery = StrSQLQuery + " And Lg.V_Type<>'F_AO'   "
        StrSQLQuery = StrSQLQuery + StrConditionGrpOn


        Dim cstr1 As String = ""
        cstr1 = StrSQLQuery.ToString()

        StrSQLQuery = AgL.GetBackendBasedQuery(StrSQLQuery)
        DTTemp = CMain.FGetDatTable(StrSQLQuery, AgL.GCn)

        If Not DTTemp.Rows.Count > 0 Then MsgBox("No Records Found to Print.") : Exit Sub


        If Trim(FGMain(GFilter, 13).Value) = "Format-2" Then
            FLoadMainReport("FaAgeingFormat2", DTTemp)
        Else
            FLoadMainReport("FaAgeing", DTTemp)
        End If
        '=======================================================================
        '==================== For Display Days in Reports ======================
        '=======================================================================
        Dim i As Integer
        For i = 0 To RptMain.DataDefinition.FormulaFields.Count - 1
            Select Case CStr(UCase(RptMain.DataDefinition.FormulaFields.Item(i).Name))
                Case "D1"
                    RptMain.DataDefinition.FormulaFields.Item(i).Text = " " & D1 & ""
                Case "D2"
                    RptMain.DataDefinition.FormulaFields.Item(i).Text = " " & D2 & ""
                Case "D3"
                    RptMain.DataDefinition.FormulaFields.Item(i).Text = " " & D3 & ""
                Case "D4"
                    RptMain.DataDefinition.FormulaFields.Item(i).Text = " " & D4 & ""
                Case "D5"
                    RptMain.DataDefinition.FormulaFields.Item(i).Text = " " & D5 & ""
                Case "D6"
                    RptMain.DataDefinition.FormulaFields.Item(i).Text = " " & D6 & ""
                Case "TITLE2"
                    RptMain.DataDefinition.FormulaFields.Item(i).Text = "' " & Reptitle & " '"
                Case "REPCREDIT"
                    RptMain.DataDefinition.FormulaFields.Item(i).Text = "' " & Repcredit & " '"
                Case "REPDEBIT"
                    RptMain.DataDefinition.FormulaFields.Item(i).Text = "' " & Repdebit & " '"
                Case "REPDAYS"
                    RptMain.DataDefinition.FormulaFields.Item(i).Text = "' " & RepDays & " '"
                Case "FRMSHOWRECORDS"
                    RptMain.DataDefinition.FormulaFields.Item(i).Text = "'" & Trim(FGMain(GFilterCode, 9).Value) & "'"
                Case "FRMCHOICE"
                    RptMain.DataDefinition.FormulaFields.Item(i).Text = "'" & StrChoice & "'"

            End Select
        Next
        '=======================================================================
        '========================= End Display Days ============================
        '=======================================================================
        CMain.FormulaSet(RptMain, Me.Text, FGMain)
        CMain.FShowReport(RptMain, Me.MdiParent, Me.Text)
    End Sub

    Private Sub FAgeingSqlServer()
        Dim D1, D2, D3, D4, D5, D6 As Integer
        Dim StrCondition1 As String, Strconditionsite As String, StrConditionGrpOn As String, StrChoice As String
        Dim STRDATE, StrAmtCr, StrAmtDr, Reptitle As String
        Dim Repdebit, Repcredit, RepDays As String
        Dim DTTemp As DataTable

        If Not FIsValid(0) Then Exit Sub
        If Not FIsValid(1) Then Exit Sub
        If Not FIsValid(2) Then Exit Sub
        If Not FIsValid(3) Then Exit Sub
        If Not FIsValid(4) Then Exit Sub
        If Not FIsValid(5) Then Exit Sub
        If Not FIsValid(6) Then Exit Sub
        If Not FIsValid(7) Then Exit Sub
        If Not FIsValid(8) Then Exit Sub
        If Not FIsValid(9) Then Exit Sub

        If Val((FGMain(GFilter, 3).Value.ToString)) > Val((FGMain(GFilter, 4).Value.ToString)) Then MsgBox("II Interval Must Be Greater Than I Interval ") : Exit Sub
        If Val((FGMain(GFilter, 4).Value.ToString)) > Val((FGMain(GFilter, 5).Value.ToString)) Then MsgBox("III Interval Must Be Greater Than II Interval ") : Exit Sub
        If Val((FGMain(GFilter, 5).Value.ToString)) > Val((FGMain(GFilter, 6).Value.ToString)) Then MsgBox("IV Interval Must Be Greater Than III Interval ") : Exit Sub
        If Val((FGMain(GFilter, 6).Value.ToString)) > Val((FGMain(GFilter, 7).Value.ToString)) Then MsgBox("V Interval Must Be Greater Than IV Interval ") : Exit Sub
        If Val((FGMain(GFilter, 7).Value.ToString)) > Val((FGMain(GFilter, 8).Value.ToString)) Then MsgBox("VI Interval Must Be Greater Than V Interval ") : Exit Sub
        Strconditionsite = ""
        STRDATE = AgL.Chk_Date(FGMain(GFilter, 0).Value.ToString)
        StrCondition1 = " LG.V_Date <= " & AgL.Chk_Date(FGMain(GFilter, 0).Value.ToString) & " "
        If Trim(FGMain(GFilterCode, 1).Value) <> "" Then StrCondition1 = StrCondition1 & " And ag.nature In ('" & FGMain(GFilterCode, 1).Value & "')"
        If Trim(FGMain(GFilterCode, 2).Value) <> "" Then
            Strconditionsite = Strconditionsite & "  And LG.site_Code In(" & FGMain(GFilterCode, 2).Value & ") "
        Else
            Strconditionsite = Strconditionsite & " And LG.site_Code In(" & AgL.PubSiteList & ") "
        End If

        If FGMain(GFilterCode, 1).Value = "Customer" Then
            StrAmtDr = "AmtDr"
            StrAmtCr = "AmtCr"
            Reptitle = "Ageing Analysis of Debtors"
            Repdebit = "Total Debit"
            Repcredit = "Total Credit"
            RepDays = "Amount Debited From Days"
        Else
            StrAmtDr = "AmtCr"
            StrAmtCr = "AmtDr"
            Reptitle = "Ageing Analysis of Creditors"
            Repdebit = "Total Credit"
            Repcredit = "Total Debit"
            RepDays = "Amount Credited From Days"
        End If

        If FGMain(GFilterCode, 10).Value <> "AC" Then
            StrConditionGrpOn = " Group By AG.GroupName "
            StrChoice = "AG"
        Else
            StrConditionGrpOn = " Group By SG.Name "
            StrChoice = "AC"
        End If

        D1 = Val((FGMain(GFilter, 3).Value.ToString))
        D2 = Val((FGMain(GFilter, 4).Value.ToString))
        D3 = Val((FGMain(GFilter, 5).Value.ToString))
        D4 = Val((FGMain(GFilter, 6).Value.ToString))
        D5 = Val((FGMain(GFilter, 7).Value.ToString))
        D6 = Val((FGMain(GFilter, 8).Value.ToString))

        ''*********** For trans Purpose **************''
        StrSQLQuery = "Select  IsNull(Sum(" & StrAmtDr & "),0) As Amt1,0 As Amt2,0 As Amt3,0 As Amt4,0 As Amt5,0 As Amt6,0 As Amt7,0 As AmtPR, "
        StrSQLQuery = StrSQLQuery + " max(Sg.Name) As PName,Max(SM.Name) As Division,0 As AmtPR_Contra,Max(AG.GroupName) as GroupName, Isnull(Max(SG.CreditLimit),0) as CreditLimit ,Isnull(Max(Sg.CreditDays),0) as DueDays,Isnull(Max(CT.CityName),'') AS CityName    From Ledger As Lg "
        StrSQLQuery = StrSQLQuery + " Left Join SubGroup As SG On Lg.SubCode=Sg.SubCode "
        StrSQLQuery = StrSQLQuery + " Left Join AcGroup As AG On ag.GroupCode=Sg.GroupCode "
        StrSQLQuery = StrSQLQuery + " Left Join Sitemast As SM On SM.Code=Lg.site_Code "
        StrSQLQuery = StrSQLQuery + " LEFT JOIN City CT ON CT.CityCode =SG.CityCode "
        StrSQLQuery = StrSQLQuery + " Where DateDiff(Day,V_Date,  " & STRDATE & " )>=0 And "
        StrSQLQuery = StrSQLQuery + " DateDiff(Day,V_Date, " & STRDATE & " )<= " & D1 & "  And "
        StrSQLQuery = StrSQLQuery + StrCondition1 + Strconditionsite
        StrSQLQuery = StrSQLQuery + "And Lg.V_Type<>'F_AO'   "
        StrSQLQuery = StrSQLQuery + StrConditionGrpOn

        StrSQLQuery = StrSQLQuery + " Union All "

        StrSQLQuery = StrSQLQuery + " Select  0 As Amt1,IsNull(Sum(" & StrAmtDr & "),0) As Amt2,0 As Amt3,0 As Amt4,0 As Amt5,0 As Amt6,0 As Amt7,0 As AmtPR, "
        StrSQLQuery = StrSQLQuery + " max(Sg.Name) As PName,Max(SM.Name) As Division,0 As AmtPR_Contra,Max(AG.GroupName) as GroupName,Isnull(Max(SG.CreditLimit),0) as CreditLimit ,Isnull(Max(Sg.CreditDays),0) as DueDays,Isnull(Max(CT.CityName),'') AS CityName    From Ledger As Lg "
        StrSQLQuery = StrSQLQuery + " Left Join SubGroup As SG On Lg.SubCode=Sg.SubCode "
        StrSQLQuery = StrSQLQuery + " Left Join AcGroup As AG On ag.GroupCode=Sg.GroupCode "
        StrSQLQuery = StrSQLQuery + " Left Join Sitemast As SM On SM.Code=Lg.site_Code "
        StrSQLQuery = StrSQLQuery + " LEFT JOIN City CT ON CT.CityCode =SG.CityCode "
        StrSQLQuery = StrSQLQuery + " Where DateDiff(Day,V_Date," & STRDATE & " )>" & D1 & " And "
        StrSQLQuery = StrSQLQuery + " DateDiff(Day,V_Date," & STRDATE & " )<=" & D2 & "  And "
        StrSQLQuery = StrSQLQuery + StrCondition1 + Strconditionsite
        StrSQLQuery = StrSQLQuery + " And Lg.V_Type<>'F_AO'   "
        StrSQLQuery = StrSQLQuery + StrConditionGrpOn

        StrSQLQuery = StrSQLQuery + " Union All "

        StrSQLQuery = StrSQLQuery + " Select  0 As Amt1,0 As Amt2,IsNull(Sum(" & StrAmtDr & "),0) As Amt3,0 As Amt4,0 As Amt5,0 As Amt6,0 As Amt7,0 As AmtPR,  "
        StrSQLQuery = StrSQLQuery + " max(Sg.Name) As PName,max(sm.name) As Division,0 As AmtPR_Contra,Max(AG.GroupName) as GroupName,Isnull(Max(SG.CreditLimit),0) as CreditLimit ,Isnull(Max(Sg.CreditDays),0) as DueDays,Isnull(Max(CT.CityName),'') AS CityName   From Ledger As Lg "
        StrSQLQuery = StrSQLQuery + " Left Join SubGroup As SG On Lg.SubCode=Sg.SubCode"
        StrSQLQuery = StrSQLQuery + " Left Join AcGroup As AG On ag.GroupCode=Sg.GroupCode "
        StrSQLQuery = StrSQLQuery + " Left Join Sitemast As SM On SM.Code=Lg.site_Code "
        StrSQLQuery = StrSQLQuery + " LEFT JOIN City CT ON CT.CityCode =SG.CityCode "
        StrSQLQuery = StrSQLQuery + " Where DateDiff(Day,V_Date," & STRDATE & " )>" & D2 & " And "
        StrSQLQuery = StrSQLQuery + " DateDiff(Day,V_Date," & STRDATE & " )<=" & D3 & "  And "
        StrSQLQuery = StrSQLQuery + StrCondition1 + Strconditionsite
        StrSQLQuery = StrSQLQuery + " And Lg.V_Type<>'F_AO'   "
        StrSQLQuery = StrSQLQuery + StrConditionGrpOn

        StrSQLQuery = StrSQLQuery + " Union All "

        StrSQLQuery = StrSQLQuery + " Select  0 As Amt1,0 As Amt2,0 As Amt3,IsNull(Sum(" & StrAmtDr & "),0) As Amt4,0 As Amt5,0 As Amt6,0 As Amt7,0 As AmtPR,"
        StrSQLQuery = StrSQLQuery + " max(Sg.Name) As PName,max(sm.name) As Division,0 As AmtPR_Contra,Max(AG.GroupName) as GroupName,Isnull(Max(SG.CreditLimit),0) as CreditLimit ,Isnull(Max(Sg.CreditDays),0) as DueDays ,Isnull(Max(CT.CityName),'') AS CityName   From Ledger As Lg "
        StrSQLQuery = StrSQLQuery + " Left Join SubGroup As SG On Lg.SubCode=Sg.SubCode"
        StrSQLQuery = StrSQLQuery + " Left Join AcGroup As AG On ag.GroupCode=Sg.GroupCode "
        StrSQLQuery = StrSQLQuery + " Left Join Sitemast As SM On SM.Code=Lg.site_Code "
        StrSQLQuery = StrSQLQuery + " LEFT JOIN City CT ON CT.CityCode =SG.CityCode "
        StrSQLQuery = StrSQLQuery + " Where DateDiff(Day,V_Date," & STRDATE & " )>" & D3 & " And "
        StrSQLQuery = StrSQLQuery + " DateDiff(Day,V_Date," & STRDATE & " )<=" & D4 & "  And "
        StrSQLQuery = StrSQLQuery + StrCondition1 + Strconditionsite
        StrSQLQuery = StrSQLQuery + " And Lg.V_Type<>'F_AO'   "
        StrSQLQuery = StrSQLQuery + StrConditionGrpOn

        StrSQLQuery = StrSQLQuery + " Union All "

        StrSQLQuery = StrSQLQuery + "Select  0 As Amt1,0 As Amt2,0 As Amt3,0 As Amt4,IsNull(Sum(" & StrAmtDr & "),0) As Amt5,0 As Amt6,0 As Amt7,0 As AmtPR,"
        StrSQLQuery = StrSQLQuery + " max(Sg.Name) As PName,max(sm.name) As Division,0 As AmtPR_Contra,Max(AG.GroupName) as GroupName,Isnull(Max(SG.CreditLimit),0) as CreditLimit ,Isnull(Max(Sg.CreditDays),0) as DueDays,Isnull(Max(CT.CityName),'') AS CityName   From Ledger As Lg "
        StrSQLQuery = StrSQLQuery + " Left Join SubGroup As SG On Lg.SubCode=Sg.SubCode"
        StrSQLQuery = StrSQLQuery + " Left Join AcGroup As AG On ag.GroupCode=Sg.GroupCode "
        StrSQLQuery = StrSQLQuery + " Left Join Sitemast As SM On SM.Code=Lg.site_Code "
        StrSQLQuery = StrSQLQuery + " LEFT JOIN City CT ON CT.CityCode =SG.CityCode "
        StrSQLQuery = StrSQLQuery + " Where DateDiff(Day,V_Date," & STRDATE & " )>" & D4 & " And "
        StrSQLQuery = StrSQLQuery + " DateDiff(Day,V_Date," & STRDATE & " )<=" & D5 & "  And "
        StrSQLQuery = StrSQLQuery + StrCondition1 + Strconditionsite
        StrSQLQuery = StrSQLQuery + " And Lg.V_Type<>'F_AO'   "
        StrSQLQuery = StrSQLQuery + StrConditionGrpOn

        StrSQLQuery = StrSQLQuery + " Union All "


        StrSQLQuery = StrSQLQuery + " Select  0 As Amt1,0 As Amt2,0 As Amt3,0 As Amt4,0 As Amt5,IsNull(Sum(" & StrAmtDr & "),0) As Amt6,0 As Amt7,0 As AmtPR,"
        StrSQLQuery = StrSQLQuery + " max(Sg.Name) As PName,max(sm.name) As Division,0 As AmtPR_Contra ,Max(AG.GroupName) as GroupName,Isnull(Max(SG.CreditLimit),0) as CreditLimit ,Isnull(Max(Sg.CreditDays),0) as DueDays,Isnull(Max(CT.CityName),'') AS CityName   From Ledger As Lg "
        StrSQLQuery = StrSQLQuery + " Left Join SubGroup As SG On Lg.SubCode=Sg.SubCode"
        StrSQLQuery = StrSQLQuery + " Left Join AcGroup As AG On ag.GroupCode=Sg.GroupCode "
        StrSQLQuery = StrSQLQuery + " Left Join Sitemast As SM On SM.Code=Lg.site_Code "
        StrSQLQuery = StrSQLQuery + " LEFT JOIN City CT ON CT.CityCode =SG.CityCode "
        StrSQLQuery = StrSQLQuery + " Where DateDiff(Day,V_Date," & STRDATE & " )>" & D5 & " And "
        StrSQLQuery = StrSQLQuery + " DateDiff(Day,V_Date," & STRDATE & " )<=" & D6 & "  And "
        StrSQLQuery = StrSQLQuery + StrCondition1 + Strconditionsite
        StrSQLQuery = StrSQLQuery + " And Lg.V_Type<>'F_AO'   "
        StrSQLQuery = StrSQLQuery + StrConditionGrpOn

        StrSQLQuery = StrSQLQuery + " Union All "

        StrSQLQuery = StrSQLQuery + "Select  0 As Amt1,0 As Amt2,0 As Amt3,0 As Amt4,0 As Amt5,0 As Amt6,IsNull(Sum(" & StrAmtDr & "),0) As Amt7,0 As AmtPR,  "
        StrSQLQuery = StrSQLQuery + " max(Sg.Name) As PName,max(sm.name) As Division,0 As AmtPR_Contra,Max(AG.GroupName) as GroupName,Isnull(Max(SG.CreditLimit),0) as CreditLimit ,Isnull(Max(Sg.CreditDays),0) as DueDays,Isnull(Max(CT.CityName),'') AS CityName   From Ledger As Lg "
        StrSQLQuery = StrSQLQuery + " Left Join SubGroup As SG On Lg.SubCode=Sg.SubCode"
        StrSQLQuery = StrSQLQuery + " Left Join AcGroup As AG On ag.GroupCode=Sg.GroupCode "
        StrSQLQuery = StrSQLQuery + " Left Join Sitemast As SM On SM.Code=Lg.site_Code "
        StrSQLQuery = StrSQLQuery + " LEFT JOIN City CT ON CT.CityCode =SG.CityCode "
        StrSQLQuery = StrSQLQuery + " Where DateDiff(Day,V_Date," & STRDATE & " )>" & D6 & " And "
        StrSQLQuery = StrSQLQuery + StrCondition1 + Strconditionsite
        StrSQLQuery = StrSQLQuery + " And Lg.V_Type<>'F_AO'   "
        StrSQLQuery = StrSQLQuery + StrConditionGrpOn

        StrSQLQuery = StrSQLQuery + " Union All "

        StrSQLQuery = StrSQLQuery + " Select  0 As Amt1,0 As Amt2,0 As Amt3,0"
        StrSQLQuery = StrSQLQuery + "As Amt4,0 As Amt5,0 As Amt6,0 As Amt7,IsNull(Sum(" & StrAmtCr & " ),0) As AmtPR,  "
        StrSQLQuery = StrSQLQuery + " max(Sg.Name) As PName,max(sm.name) As Division,IsNull(Sum(" & StrAmtDr & " ),0) As AmtPR_Contra,Max(AG.GroupName) as GroupName, "
        StrSQLQuery = StrSQLQuery + " Isnull(Max(SG.CreditLimit),0) as CreditLimit ,Isnull(Max(Sg.CreditDays),0) as DueDays,Isnull(Max(CT.CityName),'') AS CityName   From Ledger As Lg "
        StrSQLQuery = StrSQLQuery + " Left Join SubGroup As SG On Lg.SubCode=Sg.SubCode"
        StrSQLQuery = StrSQLQuery + " Left Join AcGroup As AG On ag.GroupCode=Sg.GroupCode "
        StrSQLQuery = StrSQLQuery + " Left Join Sitemast As SM On SM.Code=Lg.site_Code "
        StrSQLQuery = StrSQLQuery + " LEFT JOIN City CT ON CT.CityCode =SG.CityCode "
        StrSQLQuery = StrSQLQuery + " Where "
        StrSQLQuery = StrSQLQuery + StrCondition1 + Strconditionsite
        StrSQLQuery = StrSQLQuery + " And Lg.V_Type<>'F_AO'   "
        StrSQLQuery = StrSQLQuery + StrConditionGrpOn

        StrSQLQuery = AgL.GetBackendBasedQuery(StrSQLQuery)
        DTTemp = CMain.FGetDatTable(StrSQLQuery, AgL.GCn)

        If Not DTTemp.Rows.Count > 0 Then MsgBox("No Records Found to Print.") : Exit Sub

        FLoadMainReport("FaAgeing", DTTemp)
        '=======================================================================
        '==================== For Display Days in Reports ======================
        '=======================================================================
        Dim i As Integer
        For i = 0 To RptMain.DataDefinition.FormulaFields.Count - 1
            Select Case CStr(UCase(RptMain.DataDefinition.FormulaFields.Item(i).Name))
                Case "D1"
                    RptMain.DataDefinition.FormulaFields.Item(i).Text = " " & D1 & ""
                Case "D2"
                    RptMain.DataDefinition.FormulaFields.Item(i).Text = " " & D2 & ""
                Case "D3"
                    RptMain.DataDefinition.FormulaFields.Item(i).Text = " " & D3 & ""
                Case "D4"
                    RptMain.DataDefinition.FormulaFields.Item(i).Text = " " & D4 & ""
                Case "D5"
                    RptMain.DataDefinition.FormulaFields.Item(i).Text = " " & D5 & ""
                Case "D6"
                    RptMain.DataDefinition.FormulaFields.Item(i).Text = " " & D6 & ""
                Case "TITLE2"
                    RptMain.DataDefinition.FormulaFields.Item(i).Text = "' " & Reptitle & " '"
                Case "REPCREDIT"
                    RptMain.DataDefinition.FormulaFields.Item(i).Text = "' " & Repcredit & " '"
                Case "REPDEBIT"
                    RptMain.DataDefinition.FormulaFields.Item(i).Text = "' " & Repdebit & " '"
                Case "REPDAYS"
                    RptMain.DataDefinition.FormulaFields.Item(i).Text = "' " & RepDays & " '"
                Case "FRMSHOWRECORDS"
                    RptMain.DataDefinition.FormulaFields.Item(i).Text = "'" & Trim(FGMain(GFilterCode, 9).Value) & "'"
                Case "FRMCHOICE"
                    RptMain.DataDefinition.FormulaFields.Item(i).Text = "'" & StrChoice & "'"

            End Select
        Next
        '=======================================================================
        '========================= End Display Days ============================
        '=======================================================================
        CMain.FormulaSet(RptMain, Me.Text, FGMain)
        CMain.FShowReport(RptMain, Me.MdiParent, Me.Text)
    End Sub

    Private Sub FBillWsOS(ByVal StrAmt1 As String, ByVal StrAmt2 As String, ByVal StrReportFor As String)

        Dim StrCondition1 As String, StrCondition2 As String
        Dim DTTemp As DataTable
        Dim StrCnd As String = ""

        If Not FIsValid(0) Then Exit Sub
        DTTemp = CMain.FGetDatTable("SELECT GroupCode FROM AcGroup WHERE GroupName='" & StrReportFor & "'", AgL.GCn)

        If DTTemp.Rows.Count > 0 Then StrCnd = AgL.XNull(DTTemp.Rows(0).Item("GroupCode")) : DTTemp.Rows.Clear()
        StrCondition1 = " Where (LG.V_Date <= " & AgL.Chk_Date(CDate(FGMain(GFilter, 0).Value.ToString).ToString("s")) & " And IfNull(LG." & StrAmt1 & ",0)>0) And (SG.GroupCode In (SELECT AGP.GroupCode FROM AcGroupPath AGP WHERE AGP.GroupUnder='" & StrCnd & "') Or SG.GroupCode='" & StrCnd & "') "
        StrCondition2 = " Where (LG.V_Date <= " & AgL.Chk_Date(CDate(FGMain(GFilter, 0).Value.ToString).ToString("s")) & ") And IfNull(LG." & StrAmt2 & ",0)>0 And IfNull(LG." & StrAmt2 & ",0)-IfNull(T.AMOUNT,0)<>0 And (SG.GroupCode In (SELECT AGP.GroupCode FROM AcGroupPath AGP WHERE AGP.GroupUnder='" & StrCnd & "') Or SG.GroupCode='" & StrCnd & "') "

        If Trim(FGMain(GFilterCode, 1).Value) <> "" Then StrCondition1 = StrCondition1 & " And (IfNull(SG.GroupCode,'') In (Select IfNull(AGP.GroupCode,'') From AcGroupPath AGP Where AGP.GroupUnder In (" & FGMain(GFilterCode, 1).Value & ")) Or SG.GroupCode In (" & FGMain(GFilterCode, 1).Value & ")) "
        If Trim(FGMain(GFilterCode, 1).Value) <> "" Then StrCondition2 = StrCondition2 & " And (IfNull(SG.GroupCode,'') In (Select IfNull(AGP.GroupCode,'') From AcGroupPath AGP Where AGP.GroupUnder In (" & FGMain(GFilterCode, 1).Value & ")) Or SG.GroupCode In (" & FGMain(GFilterCode, 1).Value & ")) "

        If Trim(FGMain(GFilterCode, 2).Value) <> "" Then StrCondition1 = StrCondition1 & " And LG.SubCode In (" & FGMain(GFilterCode, 2).Value & ")"
        If Trim(FGMain(GFilterCode, 2).Value) <> "" Then StrCondition2 = StrCondition2 & " And LG.SubCode In (" & FGMain(GFilterCode, 2).Value & ")"

        If Trim(FGMain(GFilterCode, 3).Value) <> "" Then StrCondition1 = StrCondition1 & "  AND ZM.Code In (" & FGMain(GFilterCode, 3).Value & ")"
        If Trim(FGMain(GFilterCode, 3).Value) <> "" Then StrCondition2 = StrCondition2 & " AND ZM.Code In (" & FGMain(GFilterCode, 3).Value & ")"

        If Trim(FGMain(GFilterCode, 5).Value) <> "" Then
            StrCondition1 = StrCondition1 & " And  LG.Site_Code IN (" & FGMain(GFilterCode, 5).Value & ") "
            StrCondition2 = StrCondition2 & " And  LG.Site_Code IN (" & FGMain(GFilterCode, 5).Value & ") "
        Else
            StrCondition1 = StrCondition1 & " And  LG.Site_Code IN (" & AgL.PubSiteList & ") "
            StrCondition2 = StrCondition2 & " And  LG.Site_Code IN (" & AgL.PubSiteList & ") "
        End If


        StrSQLQuery = "Select LG.DocId,LG.V_SNo, LG.V_Type,LG.DivCode || LG.Site_Code || '-' || LG.V_Type || '-' || LG.RecId As VNo,Max(LG.V_Type) as VType,Max(LG.V_Date) as VDate,Max(SG.Name) As PName,"
        StrSQLQuery = StrSQLQuery + "Max(LG.SubCode) as SubCode,Max(LG.Narration) as Narration,Max(LG." & StrAmt1 & ") as Amt1,0 As Amt2,IfNull(Sum(LA.Amount),0) as Amt, "
        StrSQLQuery = StrSQLQuery + "Max(SG.Address)As Add1,'' As Add2,Max(C.CityName)As CityName,'India' as Country,MAx(St.name) As SiteName,max(Ag.GroupName) as AcGroupName,'" + Trim(FGMain(GFilterCode, 4).Value) + "' as RepChoice  "
        StrSQLQuery = StrSQLQuery + "From Ledger LG Left Join SubGroup SG On LG.Subcode=SG.SubCode Left Join "
        'StrSQLQuery = StrSQLQuery + "City C on SG.CityCode=C.CityCode Left Join Country CT on SG.CountryCode=CT.Code LEFT JOIN AcGroup AG ON SG.GroupCode =AG.GroupCode  "
        StrSQLQuery = StrSQLQuery + "City C on SG.CityCode=C.CityCode LEFT JOIN AcGroup AG ON SG.GroupCode =AG.GroupCode  "
        StrSQLQuery = StrSQLQuery + "Left Join LedgerAdj LA On LG.DocId=LA.Adj_DocID  And LG.V_SNo=LA.Adj_V_SNo "
        StrSQLQuery = StrSQLQuery + "LEFT JOIN SiteMast ST ON LG.Site_Code =St.code  "
        StrSQLQuery = StrSQLQuery + "LEFT JOIN Area ZM ON ZM.Code =SG.Area "
        StrSQLQuery = StrSQLQuery + StrCondition1
        StrSQLQuery = StrSQLQuery + "Group By LG.DocId, LG.V_Type,LG.V_SNo,LG.DivCode || LG.Site_Code || '-' || LG.V_Type || '-' || LG.RecId "
        StrSQLQuery = StrSQLQuery + "HAVING(IfNull(Sum(LA.Amount), 0) <> Max(LG." & StrAmt1 & "))"
        StrSQLQuery = StrSQLQuery + "Union All "
        StrSQLQuery = StrSQLQuery + "Select	LG.DocId,LG.V_SNo,LG.V_Type,LG.DivCode || LG.Site_Code || '-' || LG.V_Type || '-' || LG.RecId As V_No,LG.V_Type,LG.V_Date,SG.Name As PName,LG.SubCode, "
        StrSQLQuery = StrSQLQuery + "LG.Narration,0 As Amt1,IfNull(LG." & StrAmt2 & ",0)-IfNull(T.AMOUNT,0) as Amt2,0 As Amount,Null As Add1,Null As Add2,"
        StrSQLQuery = StrSQLQuery + "Null As CityName,Null As Country,ST.name As sitename,IfNull(Ag.GroupName,'') as AcGroupName,'" + Trim(FGMain(GFilterCode, 4).Value) + "' as RepChoice  "
        StrSQLQuery = StrSQLQuery + "From Ledger LG Left Join SubGroup SG On SG.SubCode=LG.SubCode LEFT JOIN AcGroup AG ON SG.GroupCode =AG.GroupCode LEFT JOIN Area ZM ON ZM.Code =SG.Area  LEFT JOIN SiteMast ST ON LG.Site_Code =St.code   "
        StrSQLQuery = StrSQLQuery + "LEFT JOIN (SELECT LA.Vr_Docid AS Docid,LA.Vr_V_SNo AS S_No,SUM(AMOUNT) AS AMOUNT FROM LedgerAdj LA GROUP BY LA.Vr_DocId,LA.Vr_V_SNo) T ON T.DOCID=LG.DOCID AND T.S_NO=LG.V_SNO  "
        StrSQLQuery = StrSQLQuery + StrCondition2

        StrSQLQuery = AgL.GetBackendBasedQuery(StrSQLQuery)
        DTTemp = CMain.FGetDatTable(StrSQLQuery, AgL.GCn)
        If Not DTTemp.Rows.Count > 0 Then MsgBox("No Records Found to Print.") : Exit Sub
        FLoadMainReport("BillwiseOutstanding", DTTemp)

        CMain.FormulaSet(RptMain, Me.Text, FGMain)
        CMain.FShowReport(RptMain, Me.MdiParent, Me.Text)
    End Sub
    Private Sub FINI_CASH_FundFlow()
        FSetValue(0, "From Date", FGDataType.DT_Date, FilterCodeType.DTNone, AgL.PubStartDate)
        FSetValue(1, "To Date", FGDataType.DT_Date, FilterCodeType.DTNone, AgL.PubLoginDate)

        FSetValue(2, "Site Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, AgL.PubSiteName & "|'" & AgL.PubSiteCode & "'")
        FRH_Multiple(2) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable(
                          "Select 'o' As Tick,Sm.Code,Sm.Name From Sitemast Sm where code in (" & AgL.PubSiteList & ")   Order By Sm.Name",
                          AgL.GCn)), "", 300, 360, , , False, AgL.PubSiteCode)
        FRH_Multiple(2).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(2).FFormatColumn(1, , 0, , False)
        FRH_Multiple(2).FFormatColumn(2, "Name", 240, DataGridViewContentAlignment.MiddleLeft)
    End Sub


    Private Sub FCash_Fund_Flow(ByVal IntType As Integer)
        Dim StrCondition1 As String, StrConditionsite As String, reptype As String
        Dim DTTemp As DataTable
        Dim mQry As String = ""

        If Not FIsValid(0) Then Exit Sub
        If Not FIsValid(1) Then Exit Sub

        StrCondition1 = " And ( Date(Ledger.V_Date) Between " & AgL.Chk_Date(CDate(FGMain(GFilter, 0).Value.ToString).ToString("s")) & " And " & AgL.Chk_Date(CDate(FGMain(GFilter, 1).Value.ToString).ToString("s")) & ") "

        StrConditionsite = ""
        'If Trim(FGMain(GFilterCode, 2).Value) <> "" Then StrConditionsite = " And Ledger.site_Code In (" & FGMain(GFilterCode, 2).Value & ") "
        If Trim(FGMain(GFilterCode, 2).Value) <> "" Then
            StrConditionsite = StrConditionsite & " And  Ledger.Site_Code IN (" & FGMain(GFilterCode, 2).Value & ") "
        Else
            StrConditionsite = StrConditionsite & " And  Ledger.Site_Code IN (" & AgL.PubSiteList & ") "
        End If

        If IntType = 1 Then reptype = "Cash" Else reptype = "Bank"





        StrSQLQuery = "SELECT 1 AS id, '' AS type,'Cash In hand' AS groupname, (IfNull(sum(amtcr),0)-IfNull(Sum(amtdr),0)) AS sourceamt FROM Ledger "
        StrSQLQuery = StrSQLQuery + "WHERE SubCode IN (SELECT subcode FROM SubGroup WHERE Nature IN ('" & reptype & "')) "
        StrSQLQuery = StrSQLQuery + StrConditionsite
        StrSQLQuery = StrSQLQuery + StrCondition1
        StrSQLQuery = StrSQLQuery + "UNION ALL "
        ''1.2 groups for sources of funds
        StrSQLQuery = StrSQLQuery + "SELECT 2 AS id,'Sourcesoffunds' AS type,max(acgroup.GroupName ) AS groupname,"
        StrSQLQuery = StrSQLQuery + "IfNull(sum(amtcr),0) AS sourceamt FROM Ledger LEFT JOIN SubGroup ON Ledger.SubCode =subgroup.SubCode "
        StrSQLQuery = StrSQLQuery + "LEFT JOIN AcGroup ON AcGroup.GroupCode =SubGroup.GroupCode "
        StrSQLQuery = StrSQLQuery + "WHERE DocId IN "
        StrSQLQuery = StrSQLQuery + "(SELECT DISTINCT docid FROM Ledger WHERE SubCode IN ("
        StrSQLQuery = StrSQLQuery + "SELECT subcode FROM SubGroup WHERE Nature IN ('" & reptype & "'))) "
        StrSQLQuery = StrSQLQuery + "AND ledger.SubCode NOT IN (SELECT subcode FROM SubGroup WHERE Nature IN ('" & reptype & "')) "
        StrSQLQuery = StrSQLQuery + StrConditionsite
        StrSQLQuery = StrSQLQuery + StrCondition1
        StrSQLQuery = StrSQLQuery + "AND IfNull(ledger.Amtcr,0)>0 "
        StrSQLQuery = StrSQLQuery + "GROUP BY acgroup.GroupCode "
        ''1.3 just to getmax no of rows here to support left join
        StrSQLQuery = StrSQLQuery + "UNION ALL "
        StrSQLQuery = StrSQLQuery + "SELECT 2 AS id,'NA'AS type,'',0 FROM acgroup GROUP BY groupcode "

        Dim DtTemp1 As DataTable = AgL.FillData(StrSQLQuery, AgL.GCn).Tables(0)

        mQry = " CREATE Temporary TABLE #TempRecord1 (sno INT,id INT,groupname nvarchar(100),sourceamt  FLOAT);	"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

        For I As Integer = 0 To DtTemp1.Rows.Count - 1
            mQry = " INSERT INTO #TempRecord1 Values(" & I & ", " & DtTemp1.Rows(I)("id") & ",
                    '" & DtTemp1.Rows(I)("groupname") & "'," & DtTemp1.Rows(I)("sourceamt") & ")"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        Next


        StrSQLQuery = "Select 1 As id2, '' AS type,'Cash In hand' AS groupname2, (IfNull(sum(amtdr),0)-IfNull(Sum(amtcr),0)) AS appamt FROM Ledger "
        StrSQLQuery = StrSQLQuery + "WHERE SubCode IN (SELECT subcode FROM SubGroup WHERE Nature IN ('" & reptype & "')) "
        StrSQLQuery = StrSQLQuery + StrConditionsite
        StrSQLQuery = StrSQLQuery + StrCondition1
        StrSQLQuery = StrSQLQuery + "UNION all "
        ''2.2 groups for application of funds
        StrSQLQuery = StrSQLQuery + "SELECT 2 AS id2,'Applicationoffunds' AS type,max(AcGroup.GroupName) AS groupname2,"
        StrSQLQuery = StrSQLQuery + "IfNull(sum(amtdr),0) AS appamt "
        StrSQLQuery = StrSQLQuery + "FROM Ledger LEFT JOIN SubGroup ON Ledger.SubCode =subgroup.SubCode "
        StrSQLQuery = StrSQLQuery + "LEFT JOIN AcGroup ON AcGroup.GroupCode =SubGroup.GroupCode   "
        StrSQLQuery = StrSQLQuery + "WHERE DocId IN "
        StrSQLQuery = StrSQLQuery + "(SELECT DISTINCT docid FROM Ledger WHERE SubCode IN ("
        StrSQLQuery = StrSQLQuery + "SELECT subcode FROM SubGroup WHERE Nature IN ('" & reptype & "'))) "
        StrSQLQuery = StrSQLQuery + "AND ledger.SubCode NOT IN (SELECT subcode FROM SubGroup WHERE Nature IN ('" & reptype & "')) "
        StrSQLQuery = StrSQLQuery + StrConditionsite
        StrSQLQuery = StrSQLQuery + StrCondition1
        StrSQLQuery = StrSQLQuery + "AND IfNull(ledger.Amtdr,0)>0 "
        StrSQLQuery = StrSQLQuery + "GROUP BY AcGroup.GroupCode "

        Dim DtTemp2 As DataTable = AgL.FillData(StrSQLQuery, AgL.GCn).Tables(0)

        mQry = " CREATE Temporary TABLE #TempRecord2 (sno2 INT,groupname2 nvarchar(100),appamt  FLOAT);	"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

        For I As Integer = 0 To DtTemp2.Rows.Count - 1
            mQry = " INSERT INTO #TempRecord2 Values(" & I & ", 
                    '" & DtTemp2.Rows(I)("groupname2") & "'," & DtTemp2.Rows(I)("appamt") & ")"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        Next

        StrSQLQuery = "SELECT s.*,a.* from #TempRecord1 s "
        StrSQLQuery = StrSQLQuery + " Left Join"
        StrSQLQuery = StrSQLQuery + " #TempRecord2 a ON s.sno=a.sno2 "



        'StrSQLQuery = "SELECT s.*,a.* from( "
        ''1 sources of funds part (s Table
        'StrSQLQuery = StrSQLQuery + "SELECT row_number() OVER (ORDER BY id) AS sno,id,groupname,sourceamt FROM ("
        ''1.1 cash bal selection (temp table
        'StrSQLQuery = StrSQLQuery + "SELECT 1 AS id, '' AS type,'Cash In hand' AS groupname, (IfNull(sum(amtcr),0)-IfNull(Sum(amtdr),0)) AS sourceamt FROM Ledger "
        'StrSQLQuery = StrSQLQuery + "WHERE SubCode IN (SELECT subcode FROM SubGroup WHERE Nature IN ('" & reptype & "')) "
        'StrSQLQuery = StrSQLQuery + StrConditionsite
        'StrSQLQuery = StrSQLQuery + StrCondition1
        'StrSQLQuery = StrSQLQuery + "UNION ALL "
        ''1.2 groups for sources of funds
        'StrSQLQuery = StrSQLQuery + "SELECT 2 AS id,'Sourcesoffunds' AS type,max(acgroup.GroupName ) AS groupname,"
        'StrSQLQuery = StrSQLQuery + "IfNull(sum(amtcr),0) AS sourceamt FROM Ledger LEFT JOIN SubGroup ON Ledger.SubCode =subgroup.SubCode "
        'StrSQLQuery = StrSQLQuery + "LEFT JOIN AcGroup ON AcGroup.GroupCode =SubGroup.GroupCode "
        'StrSQLQuery = StrSQLQuery + "WHERE DocId IN "
        'StrSQLQuery = StrSQLQuery + "(SELECT DISTINCT docid FROM Ledger WHERE SubCode IN ("
        'StrSQLQuery = StrSQLQuery + "SELECT subcode FROM SubGroup WHERE Nature IN ('" & reptype & "'))) "
        'StrSQLQuery = StrSQLQuery + "AND ledger.SubCode NOT IN (SELECT subcode FROM SubGroup WHERE Nature IN ('" & reptype & "')) "
        'StrSQLQuery = StrSQLQuery + StrConditionsite
        'StrSQLQuery = StrSQLQuery + StrCondition1
        'StrSQLQuery = StrSQLQuery + "AND IfNull(ledger.Amtcr,0)>0 "
        'StrSQLQuery = StrSQLQuery + "GROUP BY acgroup.GroupCode "
        ''1.3 just to getmax no of rows here to support left join
        'StrSQLQuery = StrSQLQuery + "UNION ALL "
        'StrSQLQuery = StrSQLQuery + "SELECT 2 AS id,'NA'AS type,'',0 FROM acgroup GROUP BY groupcode ) AS temp "
        'StrSQLQuery = StrSQLQuery + ") s "
        'StrSQLQuery = StrSQLQuery + " Left Join"
        ''2 application of funds (a Table
        'StrSQLQuery = StrSQLQuery + "(SELECT row_number() OVER (ORDER BY id2) AS sno2,groupname2,appamt FROM( "
        ''2.1 selecting cash balance( Temp4 table
        'StrSQLQuery = StrSQLQuery + "SELECT 1 AS id2, '' AS type,'Cash In hand' AS groupname2, (IfNull(sum(amtdr),0)-IfNull(Sum(amtcr),0)) AS appamt FROM Ledger "
        'StrSQLQuery = StrSQLQuery + "WHERE SubCode IN (SELECT subcode FROM SubGroup WHERE Nature IN ('" & reptype & "')) "
        'StrSQLQuery = StrSQLQuery + StrConditionsite
        'StrSQLQuery = StrSQLQuery + StrCondition1
        'StrSQLQuery = StrSQLQuery + "UNION all "
        ''2.2 groups for application of funds
        'StrSQLQuery = StrSQLQuery + "SELECT 2 AS id2,'Applicationoffunds' AS type,max(AcGroup.GroupName) AS groupname2,"
        'StrSQLQuery = StrSQLQuery + "IfNull(sum(amtdr),0) AS appamt "
        'StrSQLQuery = StrSQLQuery + "FROM Ledger LEFT JOIN SubGroup ON Ledger.SubCode =subgroup.SubCode "
        'StrSQLQuery = StrSQLQuery + "LEFT JOIN AcGroup ON AcGroup.GroupCode =SubGroup.GroupCode   "
        'StrSQLQuery = StrSQLQuery + "WHERE DocId IN "
        'StrSQLQuery = StrSQLQuery + "(SELECT DISTINCT docid FROM Ledger WHERE SubCode IN ("
        'StrSQLQuery = StrSQLQuery + "SELECT subcode FROM SubGroup WHERE Nature IN ('" & reptype & "'))) "
        'StrSQLQuery = StrSQLQuery + "AND ledger.SubCode NOT IN (SELECT subcode FROM SubGroup WHERE Nature IN ('" & reptype & "')) "
        'StrSQLQuery = StrSQLQuery + StrConditionsite
        'StrSQLQuery = StrSQLQuery + StrCondition1
        'StrSQLQuery = StrSQLQuery + "AND IfNull(ledger.Amtdr,0)>0 "
        'StrSQLQuery = StrSQLQuery + "GROUP BY AcGroup.GroupCode) AS temp4 "
        'StrSQLQuery = StrSQLQuery + ") a ON s.sno=a.sno2 "

        StrSQLQuery = AgL.GetBackendBasedQuery(StrSQLQuery)
        DTTemp = CMain.FGetDatTable(StrSQLQuery, AgL.GCn)

        mQry = "Drop Table #TempRecord1"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        mQry = "Drop Table #TempRecord2"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

        If Not DTTemp.Rows.Count > 0 Then MsgBox("No Records Found to Print.") : Exit Sub

        FLoadMainReport("Cash_fundflow", DTTemp)
        CMain.FormulaSet(RptMain, Me.Text, FGMain)
        CMain.FShowReport(RptMain, Me.MdiParent, Me.Text)
    End Sub

    Private Sub FMonthlyExpenses()
        Dim StrCondition1 As String
        Dim StrCondition2 As String
        Dim DTTemp As DataTable

        If Not FIsValid(0) Then Exit Sub
        StrCondition2 = ""
        StrCondition1 = " Where SG.GroupNature ='E' "
        If Trim(FGMain(GFilterCode, 0).Value) <> "" Then StrCondition2 = StrCondition2 & " HAVING  LEFT(convert(CHAR,max(lg.V_Date),7),3) In (" & FGMain(GFilterCode, 0).Value & ")"

        If Trim(FGMain(GFilterCode, 1).Value) <> "" Then StrCondition1 = StrCondition1 & " And SG.subcode In (" & FGMain(GFilterCode, 1).Value & ")"

        If Trim(FGMain(GFilterCode, 2).Value) <> "" Then
            StrCondition1 = StrCondition1 & " And  LG.Site_Code IN (" & FGMain(GFilterCode, 2).Value & ") "
        Else
            StrCondition1 = StrCondition1 & " And  LG.Site_Code IN (" & AgL.PubSiteList & ") "
        End If

        If Trim(FGMain(GFilterCode, 3).Value) <> "" Then
            StrCondition1 = StrCondition1 & " And  LG.DivCode IN (" & FGMain(GFilterCode, 3).Value & ") "
        Else
            StrCondition1 = StrCondition1 & " And  LG.DivCode IN (" & AgL.PubDivisionList & ") "
        End If


        StrSQLQuery = "SELECT CASE WHEN (Sum(Amtdr)-Sum(Amtcr))> 0 THEN Sum(Amtdr)-Sum(Amtcr) ELSE 0 end  AS bal ,Max(SG.name) AS Party, "
        'StrSQLQuery = StrSQLQuery + "LEFT(convert(CHAR,max(lg.V_Date),7),3) AS month "
        StrSQLQuery = StrSQLQuery + FGetMonthNameQry("lg.V_Date") + " AS month "
        StrSQLQuery = StrSQLQuery + "FROM Ledger lg LEFT JOIN subgroup sg ON lg.SubCode =sg.SubCode  "
        StrSQLQuery = StrSQLQuery + StrCondition1
        StrSQLQuery = StrSQLQuery + "GROUP BY sg.SubCode, " + FGetMonthNameQry("lg.V_Date") + StrCondition2
        StrSQLQuery = StrSQLQuery + "Order By  " + FGetMonthNameQry("lg.V_Date")

        StrSQLQuery = AgL.GetBackendBasedQuery(StrSQLQuery)
        DTTemp = CMain.FGetDatTable(StrSQLQuery, AgL.GCn)

        If Not DTTemp.Rows.Count > 0 Then MsgBox("No Records Found to Print.") : Exit Sub
        FLoadMainReport("MonthlyExpenses", DTTemp)
        CMain.FormulaSet(RptMain, Me.Text, FGMain)
        CMain.FShowReport(RptMain, Me.MdiParent, Me.Text)
    End Sub
    Private Sub FFixedAssetRegister()
        Dim StrIST6Month As String = ""
        Dim StrLast6Month As String = ""
        Dim StrCondition2 As String = ""
        Dim StrCondition3 As String = ""

        Dim DTTemp As DataTable
        If Not FIsValid(0) Then Exit Sub
        If Not FIsValid(1) Then Exit Sub
        If Not FIsValid(2) Then Exit Sub

        If DateValue(FGMain(GFilter, 0).Value) < DateValue(AgL.PubStartDate) Or DateValue(FGMain(GFilter, 0).Value) > DateValue(AgL.PubEndDate) Then
            MsgBox("As On Date Is Not In Financial Date")
            Exit Sub
        End If

        'Date Setting For Ist 6 Month        
        StrIST6Month = "'" & AgL.PubStartDate & "'"

        If FGMain(GFilter, 0).Value >= AgL.PubStartDate And FGMain(GFilter, 0).Value <= Microsoft.VisualBasic.DateAdd(DateInterval.Day, +182, CDate(AgL.PubStartDate)) Then
            StrIST6Month = StrIST6Month & " And " & "'" & FGMain(GFilter, 0).Value & "'"
        Else
            StrIST6Month = StrIST6Month & " And " & "'" & Microsoft.VisualBasic.DateAdd(DateInterval.Day, +182, CDate(AgL.PubStartDate)) & "'"
        End If

        'Date Setting For Last 6 Month    
        If FGMain(GFilter, 0).Value >= Microsoft.VisualBasic.DateAdd(DateInterval.Day, +183, CDate(AgL.PubStartDate)) And FGMain(GFilter, 0).Value <= AgL.PubEndDate Then
            StrLast6Month = "'" & Microsoft.VisualBasic.DateAdd(DateInterval.Day, +183, CDate(AgL.PubStartDate)) & "'"
            StrLast6Month = StrLast6Month & " And " & "'" & FGMain(GFilter, 0).Value & "'"
        End If

        StrCondition2 = "WHERE Date(AR.V_Date) Between  " & AgL.Chk_Date(CDate(AgL.PubStartDate).ToString("s")) & " And  " & AgL.Chk_Date(CDate(AgL.PubEndDate).ToString("s")) & " "
        StrCondition3 = "And Date(AT.V_Date) Between  " & AgL.Chk_Date(CDate(AgL.PubStartDate).ToString("s")) & " And  " & AgL.Chk_Date(CDate(AgL.PubEndDate).ToString("s")) & " "

        If Trim(FGMain(GFilterCode, 1).Value) <> "" Then
            StrCondition2 = StrCondition2 & " And AGM.Code IN (" & FGMain(GFilterCode, 1).Value & ") "
        End If

        StrSQLQuery = "SELECT DISTINCT AGM.Name AS Group_Name,AM.Name AS Asset_Description,AGM.Depreciation AS Depreciation,"
        StrSQLQuery = StrSQLQuery + "(SELECT Distinct AMOUNT FROM AssetTransaction AT WHERE AM.Docid=AT.Asset AND AT.V_TYPE IN ('ASTOP') " & StrCondition3 & ") AS OPEING,"
        If StrLast6Month <> "" Then
            StrSQLQuery = StrSQLQuery + "(SELECT SUM(AMOUNT) FROM AssetTransaction AT WHERE AM.Docid=AT.Asset AND AT.V_TYPE IN ('ASTPR','ASTAP') And Date(AT.V_Date) Between " & StrLast6Month & ") AS Last6Month,"
            StrSQLQuery = StrSQLQuery + "(SELECT Distinct AMOUNT FROM AssetTransaction AT WHERE AM.Docid=AT.Asset AND AT.V_TYPE IN ('ASTPR') And Date(AT.V_Date Between " & StrLast6Month & ") AS PurchaseVal,"
            StrSQLQuery = StrSQLQuery + "(SELECT Distinct DATEDIFF(DD,V_DATE,'" & FGMain(GFilter, 0).Value & "') FROM AssetTransaction AT WHERE AM.Docid=AT.Asset AND AT.V_TYPE IN ('ASTPR') And Date(AT.V_Date) Between " & StrLast6Month & ") AS DepLast6Days,"
        Else
            StrSQLQuery = StrSQLQuery + "0  AS Last6Month,"
            StrSQLQuery = StrSQLQuery + "0  AS PurchaseVal,"
            StrSQLQuery = StrSQLQuery + "0 AS DepLast6Days,"
        End If
        StrSQLQuery = StrSQLQuery + "(SELECT SUM(AMOUNT) FROM AssetTransaction AT WHERE AM.Docid=AT.Asset AND AT.V_TYPE IN ('ASTPR','ASTAP') And Date(AT.V_Date) Between " & StrIST6Month & ") AS Ist6Month,"
        StrSQLQuery = StrSQLQuery + "(SELECT Distinct DATEDIFF(DD,'" & AgL.PubStartDate & "','" & FGMain(GFilter, 0).Value & "') FROM AssetTransaction AT) AS DepIst6Days,"
        StrSQLQuery = StrSQLQuery + "(SELECT Distinct AMOUNT FROM AssetTransaction AT WHERE AM.Docid=AT.Asset AND AT.V_TYPE IN ('ASTSL') " & StrCondition3 & ") AS SALEVal "
        StrSQLQuery = StrSQLQuery + "FROM AssetMast AM "
        StrSQLQuery = StrSQLQuery + "INNER JOIN AssetTransaction AR ON AM.Docid=AR.Asset "
        StrSQLQuery = StrSQLQuery + "INNER JOIN Voucher_Type VT ON VT.V_Type=AR.V_Type "
        StrSQLQuery = StrSQLQuery + "INNER JOIN AssetGroupMast AGM ON AGM.Code=AM.AssetGroup " + StrCondition2

        StrSQLQuery = AgL.GetBackendBasedQuery(StrSQLQuery)
        DTTemp = CMain.FGetDatTable(StrSQLQuery, AgL.GCn)
        If Not DTTemp.Rows.Count > 0 Then MsgBox(ClsMain.MsgRecNotFnd) : Exit Sub
        FLoadMainReport("FixedAssetRegister", DTTemp)
        CMain.FormulaSet(RptMain, Me.Text, FGMain)

        If Trim(FGMain(GFilterCode, 2).Value) = "Detail" Then
            RptMain.DataDefinition.FormulaFields("RepType").Text = "'D'"
        Else
            RptMain.DataDefinition.FormulaFields("RepType").Text = "'S'"
        End If
        CMain.FShowReport(RptMain, Me.MdiParent, Me.Text)
    End Sub

    Private Sub FINI_IntSalesTaxClubbing()
        FSetValue(0, "From Date", FGDataType.DT_Date, FilterCodeType.DTNone, AgL.PubStartDate)
        FSetValue(1, "To Date", FGDataType.DT_Date, FilterCodeType.DTNone, AgL.PubLoginDate)
        BtnPrint.Text = "Ok"
    End Sub

    Private Sub FSalesTaxClubbing()
        Dim I As Integer = 0
        Dim FrmObj As FrmVoucherEntry
        Dim CFOpen As New ClsFunction
        Dim Mdi As New MDIMain
        Dim DtTemp As DataTable = Nothing
        Dim mQry$ = ""
        Dim mTotalVatPayble As Double = 0

        FrmObj = CFOpen.FOpen(Mdi.MnuVoucherEntry.Name, Mdi.MnuVoucherEntry.Text)
        FrmObj.MdiParent = Me.MdiParent
        FrmObj.Show()
        FrmObj.Topctrl1.FButtonClick(0)
        'FrmObj.FManageScreen("JV")
        FrmObj.TxtType.Focus()
        'FrmObj.TxtType.AgSelectedValue = "JV"
        'FrmObj.TxtType.AgSelectedValue = AgL.XNull(AgL.Dman_Execute("Select From Voucher_Type Vt Where Vt.V_Type = '" & FrmObj.TxtType.AgSelectedValue & "'", AgL.GcnRead).ExecuteScalar)



        mQry = " Select Category, Description, V_Type From Voucher_Type Vt Where Vt.V_Type = 'JV' "
        DtTemp = CMain.FGetDatTable(mQry, AgL.GCn)
        If DtTemp.Rows.Count > 0 Then
            FrmObj.FManageScreen(AgL.XNull(DtTemp.Rows(0).Item("Category")))
            FrmObj.TxtType.Text = AgL.XNull(DtTemp.Rows(0).Item("Description"))
            FrmObj.TxtType.Tag = AgL.XNull(DtTemp.Rows(0).Item("V_Type"))
            FrmObj.TxtVDate.Text = AgL.PubLoginDate

            FrmObj.TxtVDate.Focus()
            FrmObj.FGMain.Focus()
            DtTemp.Clear()
        End If

        mTotalVatPayble = AgL.VNull(AgL.Dman_Execute("SELECT Sum(L.AmtDr) - Sum(L.AmtCr)  AS Balance " &
                " FROM Ledger L " &
                " LEFT JOIN SubGroup Sg ON L.SubCode = Sg.SubCode " &
                " LEFT JOIN AcGroup G ON Sg.GroupCode = G.GroupCode " &
                " WHERE G.GroupName = 'Vat'  " &
                " And Date(L.V_Date) Between " & AgL.Chk_Date(CDate(FGMain(GFilter, 0).Value).ToString("s")) & " And " & AgL.Chk_Date(CDate(FGMain(GFilter, 1).Value).ToString("s")) & " ", AgL.GCn).ExecuteScalar)


        mQry = "SELECT L.SubCode, Max(Sg.Name) As AcName, Max(Sg.ManualCode) As ManualCode, " &
                " Sum(L.AmtDr) - Sum(L.AmtCr)  AS Balance " &
                " FROM Ledger L " &
                " LEFT JOIN SubGroup Sg ON L.SubCode = Sg.SubCode " &
                " LEFT JOIN AcGroup G ON Sg.GroupCode = G.GroupCode " &
                " WHERE G.GroupName = 'Vat' " &
                " And Date(L.V_Date) Between " & AgL.Chk_Date(CDate(FGMain(GFilter, 0).Value).ToString("s")) & " And " & AgL.Chk_Date(CDate(FGMain(GFilter, 1).Value).ToString("s")) & " " &
                " GROUP BY L.SubCode "
        mQry += " UNION ALL "
        mQry += " Select Sg.SubCode, Sg.Name As AcName, Sg.ManualCode As ManualCode, " &
                " " & -mTotalVatPayble & " As Balance " &
                " From SubGroup Sg " &
                " Where Sg.DispName = 'Vat Payable' "
        DtTemp = CMain.FGetDatTable(mQry, AgL.GCn)
        If DtTemp.Rows.Count > 0 Then
            FrmObj.FGMain.Rows.Add(DtTemp.Rows.Count)
        End If

        For I = 0 To DtTemp.Rows.Count - 1
            FrmObj.FUpdateRowStructure(New ClsStructure.VoucherType, I)
            FrmObj.FGMain(FrmVoucherEntry.GSNo, I).Value = Trim(I + 1)
            FrmObj.FGMain(FrmVoucherEntry.GAcCode, I).Value = AgL.XNull(DtTemp.Rows(I).Item("SubCode"))
            FrmObj.FGMain(FrmVoucherEntry.GAcName, I).Value = AgL.XNull(DtTemp.Rows(I).Item("AcName"))
            FrmObj.FGMain(FrmVoucherEntry.GAcManaulCode, I).Value = AgL.XNull(DtTemp.Rows(I).Item("ManualCode"))

            FrmObj.FGMain(FrmVoucherEntry.GDebit, I).Value = IIf(AgL.VNull(DtTemp.Rows(I).Item("Balance")) < 0, Format(Math.Abs(AgL.VNull(DtTemp.Rows(I).Item("Balance"))), "0.00"), "")
            FrmObj.FGMain(FrmVoucherEntry.GCredit, I).Value = IIf(AgL.VNull(DtTemp.Rows(I).Item("Balance")) > 0, Format(Math.Abs(AgL.VNull(DtTemp.Rows(I).Item("Balance"))), "0.00"), "")
        Next
        FrmObj.FUpdateRowStructure(New ClsStructure.VoucherType, FrmObj.FGMain.Rows.Count - 1)
        FrmObj.FCalculate()
    End Sub

    Private Sub BtnPrint_Resize(sender As Object, e As EventArgs) Handles BtnPrint.Resize

    End Sub

    Private Function FGetMonthNameQry(FieldName As String) As String
        If AgL.PubServerName = "" Then
            Return " SubStr(case strftime('%m', " & FieldName & ") when '01' then 'January' when '02' then 'Febuary' when '03' then 'March' when '04' then 'April' when '05' then 'May' when '06' then 'June' when '07' then 'July' when '08' then 'August' when '09' then 'September' when '10' then 'October' when '11' then 'November' when '12' then 'December' else '' end,1,3) "
        Else
            Return " SubStr(case Format(Month(" & FieldName & "),'00') when '01' then 'January' when '02' then 'Febuary' when '03' then 'March' when '04' then 'April' when '05' then 'May' when '06' then 'June' when '07' then 'July' when '08' then 'August' when '09' then 'September' when '10' then 'October' when '11' then 'November' when '12' then 'December' else '' end,1,3) "
        End If
    End Function
    Private Function FGetDateQry(FieldName As String) As String
        Return " DATETIME(substr(" & FieldName & ",8,4) || '-' ||
                Case When substr(" & FieldName & ",4,3) = 'Jan' Then '01'
                     When substr(" & FieldName & ",4,3) = 'Feb' Then '02'
                     When substr(" & FieldName & ",4,3) = 'Mar' Then '03'
                     When substr(" & FieldName & ",4,3) = 'Apr' Then '04'
                     When substr(" & FieldName & ",4,3) = 'May' Then '05'
                     When substr(" & FieldName & ",4,3) = 'Jun' Then '06'
                     When substr(" & FieldName & ",4,3) = 'Jul' Then '07'
                     When substr(" & FieldName & ",4,3) = 'Aug' Then '08'
                     When substr(" & FieldName & ",4,3) = 'Sep' Then '09'
                     When substr(" & FieldName & ",4,3) = 'Oct' Then '10'
                     When substr(" & FieldName & ",4,3) = 'Nov' Then '11'
                     When substr(" & FieldName & ",4,3) = 'Dec' Then '12'
                Else Null End || '-' || substr(" & FieldName & ",1,2)) "
    End Function
    Private Sub FGetStockValues(ByRef mOpeningStockValue As Double, ByRef mClosingStockValue As Double, mFromDate As String)
        Dim mQry As String = ""
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
    Private Sub FGetStockValuesInDataTable(ByRef mDtStockValue As DataTable, mFromDate As String)
        Dim mQry As String = " SELECT H.*
                FROM DivisionCompanySetting H
                LEFT JOIN Company C ON H.Comp_Code = C.Comp_Code
                WHERE " & AgL.Chk_Date(CDate(mFromDate)) & " BETWEEN Date(C.Start_Dt) AND Date(C.End_Dt) 
                And H.Div_Code = '" & AgL.PubDivCode & "' "
        'WHERE " & AgL.Chk_Date(CDate(mFromDate)) & " BETWEEN C.Start_Dt AND C.End_Dt 
        mDtStockValue = AgL.FillData(mQry, AgL.GCn).Tables(0)
    End Sub
    Private Sub FGMain_MouseClick(sender As Object, e As MouseEventArgs) Handles FGMain.MouseClick
        If e.Button = MouseButtons.Right Then
            Dim currentMouseOverRow As Integer
            currentMouseOverRow = FGMain.HitTest(e.X, e.Y).RowIndex

            If currentMouseOverRow >= 0 Then
                mnuSave.Text = "Save " + FGMain.Item("Field", currentMouseOverRow).Value
                mnuSave.Tag = currentMouseOverRow
            End If

            MnuOptions.Show(FGMain, New Point(e.X, e.Y))
        End If
    End Sub
    Private Sub mnuSave_Click(sender As Object, e As EventArgs) Handles mnuSave.Click
        'MsgBox("Data : " & FGMain.Item("Filter", CInt(sender.Tag)).Value + " / Code : " & FGMain.Item("FilterCode", CInt(sender.Tag)).Value)
        Dim mQry As String
        Dim mFilter As String
        mQry = "Delete From ReportFilterDefaultValues Where MenuText = '" & Me.Text & "' And User_Name ='" & AgL.PubUserName & "' And Head = '" & FGMain.Item("Field", CInt(sender.Tag)).Value & "' "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

        mFilter = FGMain.Item(GFilter, CInt(sender.Tag)).Value
        If FGMain(GDataType, CInt(sender.Tag)).Value = FGDataType.DT_Date Then
            If FGMain.Item(GFilter, CInt(sender.Tag)).Value = AgL.RetDate(AgL.PubLoginDate) Then
                mFilter = "Today"
            ElseIf FGMain.Item(GFilter, CInt(sender.Tag)).Value = AgL.RetDate(DateAdd(DateInterval.Day, -1, CDate(AgL.PubLoginDate))) Then
                mFilter = "Yesterday"
            ElseIf FGMain.Item(GFilter, CInt(sender.Tag)).Value = AgL.RetMonthStartDate(AgL.PubLoginDate) Then
                mFilter = "Month Start Date"
            ElseIf FGMain.Item(GFilter, CInt(sender.Tag)).Value = AgL.RetMonthEndDate(AgL.PubLoginDate) Then
                mFilter = "Month End Date"
            ElseIf FGMain.Item(GFilter, CInt(sender.Tag)).Value = AgL.RetMonthStartDate(DateAdd(DateInterval.Month, -1, CDate(AgL.PubLoginDate))) Then
                mFilter = "Last Month Start Date"
            ElseIf FGMain.Item(GFilter, CInt(sender.Tag)).Value = AgL.RetMonthEndDate(DateAdd(DateInterval.Month, -1, CDate(AgL.PubLoginDate))) Then
                mFilter = "Last Month End Date"
            ElseIf FGMain.Item(GFilter, CInt(sender.Tag)).Value = AgL.PubStartDate Then
                mFilter = "Year Start Date"
            ElseIf FGMain.Item(GFilter, CInt(sender.Tag)).Value = AgL.PubEndDate Then
                mFilter = "Year End Date"
            End If
        End If
        mQry = "Insert Into ReportFilterDefaultValues (MenuText, User_Name, Head, Value, ValueCode, EntryBy, EntryDate) 
               Values('" & Me.Text & "', '" & AgL.PubUserName & "', '" & FGMain.Item("Field", CInt(sender.Tag)).Value & "', '" & mFilter & "', '" & Replace(FGMain.Item("FilterCode", CInt(sender.Tag)).Value, "'", "`") & "', '" & AgL.PubUserName & "', " & AgL.Chk_Date(AgL.PubLoginDate) & ") "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
    End Sub
    Private Sub IniGrid_SetDefaultValue()
        Dim DTTemp1 As New DataTable
        Dim I As Int16
        Dim IntHeight As Int16 = Nothing, IntWidth As Int16 = Nothing
        Dim dtDefualtData As DataTable

        Try
            For I = 0 To FGMain.Rows.Count - 1
                dtDefualtData = AgL.FillData("Select * From ReportFilterDefaultValues 
                    Where MenuText='" & Me.Text & "' 
                    And User_Name = '" & AgL.PubUserName & "' 
                    And Head = '" & AgL.XNull(FGMain.Item("Field", I).Value) & "' ", AgL.GCn).Tables(0)
                If dtDefualtData.Rows.Count > 0 Then
                    FGMain.Item(GFilter, I).Value = AgL.XNull(dtDefualtData.Rows(0)("Value"))
                    FGMain.Item(GFilterCode, I).Value = Replace(AgL.XNull(dtDefualtData.Rows(0)("ValueCode")), "`", "'")


                    If AgL.XNull(FGMain.Item(GFilterCodeDataType, I).Value) = FGDataType.DT_Date Then
                        If FGMain.Item("Filter", I).Value = "Today" Then
                            FGMain.Item("Filter", I).Value = AgL.RetDate(AgL.PubLoginDate)
                        ElseIf FGMain.Item("Filter", I).Value = "Yesterday" Then
                            FGMain.Item("Filter", I).Value = AgL.RetDate(DateAdd(DateInterval.Day, -1, CDate(AgL.PubLoginDate)))
                        ElseIf FGMain.Item("Filter", I).Value = "Month Start Date" Then
                            FGMain.Item("Filter", I).Value = AgL.RetMonthStartDate(AgL.PubLoginDate)
                        ElseIf FGMain.Item("Filter", I).Value = "Month End Date" Then
                            FGMain.Item("Filter", I).Value = AgL.RetMonthEndDate(AgL.PubLoginDate)
                        ElseIf FGMain.Item("Filter", I).Value = "Last Month Start Date" Then
                            FGMain.Item("Filter", I).Value = AgL.RetMonthStartDate(DateAdd(DateInterval.Month, -1, CDate(AgL.PubLoginDate)))
                        ElseIf FGMain.Item("Filter", I).Value = "Last Month End Date" Then
                            FGMain.Item("Filter", I).Value = AgL.RetMonthEndDate(DateAdd(DateInterval.Month, -1, CDate(AgL.PubLoginDate)))
                        ElseIf FGMain.Item("Filter", I).Value = "Year Start Date" Then
                            FGMain.Item("Filter", I).Value = AgL.PubStartDate
                        ElseIf FGMain.Item("Filter", I).Value = "Year End Date" Then
                            FGMain.Item("Filter", I).Value = AgL.PubEndDate
                        End If
                    End If
                End If
            Next
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
        End Try
    End Sub
End Class


