Imports CrystalDecisions.CrystalReports.Engine
Imports System.Text
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
    Private Sub FrmReportLayout_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AgL.WinSetting(Me, IntFrmHeight, IntFrmWidth, 0, 0)
        Agl.PubSiteListCharIndex = " CharIndex('" & AgL.PubSiteCode & "', IsNull(SiteList,'')) > 0 "
    End Sub
    Private Sub FrmReportLayout_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        Agl.FPaintForm(Me, e, 0)
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
            FGMain(GFilter, e.RowIndex).Value = Agl.RetDate(FGMain(GFilter, e.RowIndex).Value)
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
        DTTable.WriteXmlSchema(Agl.PubReportPath & "\" & StrReportName & ".xml")
        RptMain.Load(Agl.PubReportPath & "\" & StrReportName & ".rpt")
        RptMain.SetDataSource(DTTable)
    End Sub
    Private Sub FLoadSubReport(ByVal StrSubReportName As String, ByVal DTTable As DataTable)
        DTTable.WriteXmlSchema(Agl.PubReportPath & "\" & StrSubReportName & ".xml")
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
        FSetValue(0, "Date", FGDataType.DT_Date, FilterCodeType.DTNone, Agl.PubLoginDate)
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
        FRH_Multiple(5) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(cmain.FGetDatTable( _
                          "Select 'o' As Tick,SG.SubCode,SG.Name,Sg.ManualCode From SubGroup SG Order By SG.Name", _
                          Agl.Gcn)), "", 600, 660, , , False)
        FRH_Multiple(5).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(5).FFormatColumn(1, , 0, , False)
        FRH_Multiple(5).FFormatColumn(2, "Name", 440, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple(5).FFormatColumn(3, "Code", 100, DataGridViewContentAlignment.MiddleLeft)

        'For Godown (From Database) Single Selection
        FSetValue(6, "Godown DB Single Sel.", FGDataType.DT_Selection_Single, FilterCodeType.DTString)
        FRH_Single(6) = New DMHelpGrid.FrmHelpGrid(New DataView(cmain.FGetDatTable( _
                        "Select GM.GodCode,GM.GodName From GodownMast GM Order By GM.GodName", _
                        Agl.Gcn)), "", 300, 300, , , False)
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
                    FIni_Bank_CashBook("'CPV','CRV'")
                Case UCase("BankBook")
                    FIni_Bank_CashBook("'PMT','RCT'")
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
                    FAgeing()
                Case UCase("BillWsOSAgeing")
                    FBillWsOSAgeing("AmtDr", "AmtCr", "Sundry Debtors")
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
                    FAccountGrpWsOSAgeing()
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


        StrSQL = "Select 'o' As Tick,SG.SubCode,SG.Name,SG.ManualCode,IsNull(CT.CityName,'') AS CityName,AG.GroupName From SubGroup  SG Left Join "
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

        StrSQL = " Declare @TmpTable Table(Code nvarchar(1),name nvarchar(15)) "
        StrSQL += " insert into @TmpTable Values('S','Summary')"
        StrSQL += " insert into @TmpTable Values('D','Detail')"
        StrSQL += " Select Code,Name From @TmpTable Order By Name "
        FSetValue(6, "Report On Choice", FGDataType.DT_Selection_Single, FilterCodeType.DTNone, "Detail", True)
        FRH_Single(6) = New DMHelpGrid.FrmHelpGrid(New DataView(CMain.FGetDatTable(StrSQL, AgL.GCn)), "", 200, 220, , , False)
        FRH_Single(6).FFormatColumn(0, , 0, , False)
        FRH_Single(6).FFormatColumn(1, "Name", 140, DataGridViewContentAlignment.MiddleLeft)


        FSetValue(7, "Site Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, AgL.PubSiteName & "|'" & AgL.PubSiteCode & "'")
        FRH_Multiple(7) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable( _
                          "Select 'o' As Tick,Sm.Code,Sm.Name From Sitemast Sm where code in (" & AgL.PubSiteList & ")   Order By Sm.Name", _
                          AgL.GCn)), "", 300, 360, , , False, AgL.PubSiteCode)
        FRH_Multiple(7).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(7).FFormatColumn(1, , 0, , False)
        FRH_Multiple(7).FFormatColumn(2, "Name", 240, DataGridViewContentAlignment.MiddleLeft)
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
        FRH_Multiple(3) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable( _
                          "Select 'o' As Tick,Sm.Code,Sm.Name From Sitemast Sm where code in (" & AgL.PubSiteList & ")   Order By Sm.Name", _
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


        StrCondition = " And (L.V_Date Between " & AgL.ConvertDate(FGMain(GFilter, 0).Value.ToString) & " And " & AgL.ConvertDate(FGMain(GFilter, 1).Value.ToString) & " ) "


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
        StrSQLQuery += "WHERE isnull(L.TDSCategory,'')<>'' AND isnull(L.tdsdesc,'')<>'' "
        StrSQLQuery += "AND L.System_Generated ='Y' "
        StrSQLQuery += StrCondition & " GROUP BY TC.Name "

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


        StrCondition1 = " Where (LG.V_Date <= " & AgL.ConvertDate(FGMain(GFilter, 0).Value.ToString) & " And IsNull(LG.AmtDr,0)>0) And AG.Nature='Customer'  "
        StrCondition2 = " Where (LG.V_Date <= " & AgL.ConvertDate(FGMain(GFilter, 0).Value.ToString) & ") And IsNull(LG.AmtCr,0)>0 And IsNull(LG.AmtCr,0)-ISNULL(T.AMOUNT,0)<>0 And AG.Nature='Customer'  "

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

        STRDATE = AgL.ConvertDate(FGMain(GFilter, 0).Value.ToString)


        StrSQLQuery = "Select LG.Docid,Max(LG.V_Date) AS V_Date,Max(LG.V_Type) AS V_Type,Convert(Varchar,Max(LG.V_No)) AS Recid,Max(SG.Name) As Party,Max(SG.SubCode) As PartySCode,Isnull(Max(C.CityName),'')  As CityName,Max(AG.GroupName) As AGGroup,Max(AG.GroupCode) As AGCode,Max(SG.DueDays) AS CrDays,"
        StrSQLQuery = StrSQLQuery + "Sum(LG.AmtDr) As TotAmtDr,"
        StrSQLQuery = StrSQLQuery + "isnull(Sum(LG.AmtDr),0)-IsNull(Sum(LA.Amount),0) as Balance,"
        StrSQLQuery = StrSQLQuery + "CASE WHEN DATEdiff(day,Max(LG.V_date)," & STRDATE & ")<=Max(SG.DueDays) THEN isnull(Sum(LG.AmtDr),0)-IsNull(Sum(LA.Amount),0) END AS UnDueAmt,"
        StrSQLQuery = StrSQLQuery + "CASE WHEN DATEdiff(day,Max(LG.V_date)," & STRDATE & ")>Max(SG.DueDays) THEN isnull(Sum(LG.AmtDr),0)-IsNull(Sum(LA.Amount),0) END AS DueAmt,"
        StrSQLQuery = StrSQLQuery + "MAx(St.name) As SiteName,"
        StrSQLQuery = StrSQLQuery + "CASE WHEN DATEdiff(day,Max(LG.V_date)," & STRDATE & ")>=0 AND DATEdiff(day,Max(LG.V_date)," & STRDATE & ")<=" & Ist & " THEN isnull(Sum(LG.AmtDr),0)-IsNull(Sum(LA.Amount),0) END AS Ist,"
        StrSQLQuery = StrSQLQuery + "CASE WHEN DATEdiff(day,Max(LG.V_date)," & STRDATE & ")>" & Ist & " AND DATEdiff(day,Max(LG.V_date)," & STRDATE & ")<=" & IInd & " THEN isnull(Sum(LG.AmtDr),0)-IsNull(Sum(LA.Amount),0) END AS IInd,"
        StrSQLQuery = StrSQLQuery + "CASE WHEN DATEdiff(day,Max(LG.V_date)," & STRDATE & ")>" & IInd & " AND DATEdiff(day,Max(LG.V_date)," & STRDATE & ")<=" & IIIrd & " THEN isnull(Sum(LG.AmtDr),0)-IsNull(Sum(LA.Amount),0) END AS IIIrd,"
        StrSQLQuery = StrSQLQuery + "CASE WHEN DATEdiff(day,Max(LG.V_date)," & STRDATE & ")>" & IIIrd & "  THEN isnull(Sum(LG.AmtDr),0)-IsNull(Sum(LA.Amount),0) END AS IV,0 As UnAdjust," & Ist & " AS IstSlabe,  "
        StrSQLQuery = StrSQLQuery + "" & IInd & " IIndSlab," & IIIrd & " IIIrdSlab,'" & STROpt & "' AS Opt  "
        StrSQLQuery = StrSQLQuery + "From Ledger LG "
        StrSQLQuery = StrSQLQuery + "Left Join SubGroup SG On LG.Subcode=SG.SubCode Left Join "
        StrSQLQuery = StrSQLQuery + "City C on SG.CityCode=C.CityCode Left Join "
        StrSQLQuery = StrSQLQuery + "Country CT on SG.CountryCode=CT.Code LEFT JOIN "
        StrSQLQuery = StrSQLQuery + "AcGroup AG ON SG.GroupCode =AG.GroupCode  "
        StrSQLQuery = StrSQLQuery + "Left Join LedgerAdj LA On LG.DocId=LA.Adj_DocID And LG.V_SNo=LA.Adj_V_SNo "
        StrSQLQuery = StrSQLQuery + "LEFT JOIN SiteMast ST ON LG.Site_Code =St.code  "
        StrSQLQuery = StrSQLQuery + "LEFT JOIN ZoneMast ZM ON ZM.Code =SG.Zone "
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
        StrSQLQuery = StrSQLQuery + "LEFT JOIN ZoneMast ZM ON ZM.Code =SG.Zone  "
        StrSQLQuery = StrSQLQuery + "LEFT JOIN SiteMast ST ON LG.Site_Code =St.code   "
        StrSQLQuery = StrSQLQuery + "LEFT JOIN (SELECT LA.Vr_Docid AS Docid,LA.Vr_V_SNo AS S_No,SUM(AMOUNT) AS AMOUNT FROM LedgerAdj LA GROUP BY LA.Vr_DocId,LA.Vr_V_SNo) T ON T.DOCID=LG.DOCID AND T.S_NO=LG.V_SNO  "
        StrSQLQuery = StrSQLQuery + StrCondition2
        StrSQLQuery = StrSQLQuery + "ORDER BY AGGroup,Party,V_Date,Recid  "


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
        FRH_Multiple(2) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable("Select 'o' As Tick,SG.SubCode,SG.Name,SG.ManualCode,SD.Name As Distributor From SubGroup SG Left Join SubGroup SD On SG.Distributor=SD.SubCode Where SG.Nature In ('Customer') Order By SG.Name", AgL.GCn)), "", 600, 820, , , False)
        FRH_Multiple(2).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(2).FFormatColumn(1, , 0, , False)
        FRH_Multiple(2).FFormatColumn(2, "Name", 300, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple(2).FFormatColumn(3, "Code", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple(2).FFormatColumn(4, "Distributor", 300, DataGridViewContentAlignment.MiddleLeft)


        FSetValue(3, "Site Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, AgL.PubSiteName & "|'" & AgL.PubSiteCode & "'", False)
        FRH_Multiple(3) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable( _
                          "Select 'o' As Tick,Sm.Code,Sm.Name From Sitemast Sm where code in (" & AgL.PubSiteList & ")   Order By Sm.Name", _
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

        StrCndBill = " And LG.V_Date <= " & AgL.ConvertDate(FGMain(GFilter, 1).Value.ToString) & " "
        StrCndPmt = " And LG.V_Date < " & AgL.ConvertDate(FGMain(GFilter, 0).Value.ToString) & " "
        StrCndPmt1 = " And (LG.V_Date Between " & AgL.ConvertDate(FGMain(GFilter, 0).Value.ToString) & " And " & AgL.ConvertDate(FGMain(GFilter, 1).Value.ToString) & ") "

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

        StrSQLQuery = "Select	MT.PName,MT.SubCode,"
        StrSQLQuery += "IsNull(RTrim(LTrim(MT.Adj_DocId)),'') +'|'+ IsNull(RTrim(LTrim(MT.Adj_V_SNo)),'') As AdjDocId, "
        StrSQLQuery += "MT.V_Type, Convert(Varchar,MT.V_No) as V_No, "
        StrSQLQuery += "MT.V_Date,MT.AmtDr,MT.DueDays,LGAT.Vr_DocId, LGAT.Vr_Type, LGAT.Vr_RecId, "
        StrSQLQuery += "LGAT.Vr_V_Date, IsNull(LGAT.Amount,0) As Amount, "
        StrSQLQuery += "" & AgL.ConvertDate(FGMain(GFilter, 0).Value.ToString) & " As FromDate , "
        StrSQLQuery += "" & AgL.ConvertDate(FGMain(GFilter, 1).Value.ToString) & " As UpToDate , "
        StrSQLQuery += "" & Val(FGMain(GFilter, 4).Value.ToString) & " As InterestRate "
        StrSQLQuery += "From ( "
        StrSQLQuery += "Select	Max(Adj_DocId) As Adj_DocId,Max(Adj_V_SNo) As Adj_V_SNo, "
        StrSQLQuery += "Max(V_Type) As V_Type,Max(RecId) As RecId,Max(V_Date) As V_Date, "
        StrSQLQuery += "(IsNull(Sum(AmtDr),0)-IsNull(Sum(AmtCr),0)) As AmtDr,Max(DueDays) As DueDays, "
        StrSQLQuery += "Max(PName) As PName,Max(SubCode) As SubCode "
        StrSQLQuery += "From "
        StrSQLQuery += "( "
        StrSQLQuery += "Select	LG.DocId As Adj_DocId,LG.V_SNo As Adj_V_SNo,LG.V_Type,Convert(Varchar,LG.V_No) as V_No,LG.V_Date, "
        StrSQLQuery += "LG.AmtDr,Null As AmtCr,SG.DueDays,SG.Name As PName,SG.SubCode  "
        StrSQLQuery += "From Ledger LG  "
        StrSQLQuery += "Left Join SubGroup SG On LG.SubCode=SG.SubCode "
        StrSQLQuery += "Where SG.Nature In ('Customer') And IsNull(LG.AmtDr,0)<>0 "
        StrSQLQuery += StrCndBill
        StrSQLQuery += "Union All "
        StrSQLQuery += "Select	LGA.Adj_DocId,LGA.Adj_V_SNo,Null As V_Type,Null As RecId,Null As V_Date,Null As AmtDr, "
        StrSQLQuery += "LGA.Amount As AmtCr,0 As DueDays,Null As PName,Null As SubCode "
        StrSQLQuery += "From LedgerAdj LGA "
        StrSQLQuery += "Left Join Ledger LG On LGA.Vr_DocId=LG.DocId "
        StrSQLQuery += "Left Join SubGroup SG On LG.SubCode=SG.SubCode "
        StrSQLQuery += "Where SG.Nature In ('Customer') And IsNull(LG.AmtCr,0)<>0 "
        StrSQLQuery += StrCndPmt
        StrSQLQuery += ") As Tmp "
        StrSQLQuery += "Group By Adj_DocId,Adj_V_SNo "
        StrSQLQuery += "Having (IsNull(Sum(AmtDr),0)-IsNull(Sum(AmtCr),0))>0 "
        StrSQLQuery += StrCndParty
        StrSQLQuery += ") As MT "
        StrSQLQuery += "Left Join "
        StrSQLQuery += "( "
        StrSQLQuery += "Select	LGA.Adj_DocId,LGA.Adj_V_SNo,LGA.Vr_DocId,LG.V_Type As Vr_Type, "
        StrSQLQuery += "LG.V_No As Vr_RecId,LG.V_Date As Vr_V_Date,LGA.Amount "
        StrSQLQuery += "From LedgerAdj LGA "
        StrSQLQuery += "Left Join Ledger LG On LGA.Vr_DocId=LG.DocId "
        StrSQLQuery += "Left Join SubGroup SG On LG.SubCode=SG.SubCode "
        StrSQLQuery += "Where SG.Nature In ('Customer') And IsNull(LG.AmtCr,0)<>0 "
        StrSQLQuery += StrCndPmt1
        StrSQLQuery += ") As LGAT On LGAT.Adj_DocId=MT.Adj_DocId And LGAT.Adj_V_SNo=MT.Adj_V_SNo "
        StrSQLQuery += "Order By	MT.V_Date,MT.V_Type,MT.V_No,"
        StrSQLQuery += "IsNull(RTrim(LTrim(MT.Adj_DocId)),'') +'|'+ IsNull(RTrim(LTrim(MT.Adj_V_SNo)),''), "
        StrSQLQuery += "LGAT.Vr_V_Date,LGAT.Vr_RecId "

        DTTemp = CMain.FGetDatTable(StrSQLQuery, AgL.GCn)

        If Not DTTemp.Rows.Count > 0 Then MsgBox("No Records Found to Print.") : Exit Sub

        FLoadMainReport("InterestCalForDebtors", DTTemp)
        CMain.FormulaSet(RptMain, Me.Text, FGMain)
        CMain.FShowReport(RptMain, Me.MdiParent, Me.Text)
    End Sub
    Private Sub FINI_BillWiseAdj()
        Dim StrSql As String
        FSetValue(0, "As On Date", FGDataType.DT_Date, FilterCodeType.DTNone, AgL.PubLoginDate)
        FSetValue(1, "Account Group", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, "All")
        FRH_Multiple(1) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable("Select 'o' As Tick,AG.GroupCode,AG.GroupName From AcGroup  AG " & _
                          "Order By AG.GroupName", AgL.GCn)), "", 600, 520, , , False)
        FRH_Multiple(1).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(1).FFormatColumn(1, , 0, , False)
        FRH_Multiple(1).FFormatColumn(2, "Name", 400, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(2, "Account Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, "All", False)
        FRH_Multiple(2) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable( _
                          "Select 'o' As Tick,SG.SubCode,SG.Name,Sg.ManualCode,IsNull(CT.CityName,''),AG.GroupName, " & _
                          "IsNull(SG.Zone,'') From SubGroup SG Left Join " & _
                          "AcGroup AG On AG.GroupCode=SG.GroupCode Left Join " & _
                          "City CT On SG.CityCode=CT.CityCode Left Join " & _
                          "ZoneMast ZM On ZM.Code=SG.Zone Where " & AgL.PubSiteListCharIndex & " " & _
                          "Order By SG.Name", _
                          AgL.GCn)), "", 600, 920, , , False)
        FRH_Multiple(2).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(2).FFormatColumn(1, , 0, , False)
        FRH_Multiple(2).FFormatColumn(2, "Name", 300, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple(2).FFormatColumn(3, "Code", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple(2).FFormatColumn(4, "City", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple(2).FFormatColumn(5, "Group Name", 200, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple(2).FFormatColumn(6, "Zone", 100, DataGridViewContentAlignment.MiddleLeft)

        StrSql = " Declare @TmpTable Table(Code nvarchar(1),name nvarchar(15)) "
        StrSql += " insert into @TmpTable Values('C','Credit')"
        StrSql += " insert into @TmpTable Values('D','Debit')"
        StrSql += " Select Code,Name From @TmpTable Order By Name "
        FSetValue(3, "Report For", FGDataType.DT_Selection_Single, FilterCodeType.DTNone, "Debit", False)
        FRH_Single(3) = New DMHelpGrid.FrmHelpGrid(New DataView(CMain.FGetDatTable(StrSql, AgL.GCn)), "", 200, 220, , , False)
        FRH_Single(3).FFormatColumn(0, , 0, , False)
        FRH_Single(3).FFormatColumn(1, "Name", 140, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(4, "Site Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, AgL.PubSiteName & "|'" & AgL.PubSiteCode & "'")
        FRH_Multiple(4) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable( _
                          "Select 'o' As Tick,Sm.Code,Sm.Name From Sitemast Sm where code in (" & AgL.PubSiteList & ")   Order By Sm.Name", _
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

        StrCondition1 = " Where LG.V_Date < = " & AgL.ConvertDate(FGMain(GFilter, 0).Value.ToString) & "  "
        If Trim(FGMain(GFilterCode, 1).Value) <> "" Then StrCondition1 = StrCondition1 & " And (IsNull(SG.GroupCode,'') In (Select IsNull(AGP.GroupCode,'') From AcGroupPath AGP Where AGP.GroupUnder In (" & FGMain(GFilterCode, 1).Value & ")) Or SG.GroupCode In (" & FGMain(GFilterCode, 1).Value & ")) "
        If Trim(FGMain(GFilterCode, 2).Value) <> "" Then StrCondition1 = StrCondition1 & " And SG.SubCode In (" & FGMain(GFilterCode, 2).Value & ")"

        If UCase(Trim(FGMain(GFilterCode, 3).Value)) = "C" Then
            DrCr = UCase(Trim(FGMain(GFilterCode, 3).Value))
        Else
            DrCr = "D"
        End If

        If DrCr = "D" Then StrAmt1 = "IsNull(LG.AmtDr,0)"
        If DrCr = "C" Then StrAmt1 = "IsNull(LG.AmtCr,0)"

        If DrCr = "D" Then StrAmt2 = "IsNull(LG.AmtCr,0)"
        If DrCr = "C" Then StrAmt2 = "IsNull(LG.AmtDr,0)"

        If Trim(FGMain(GFilterCode, 4).Value) <> "" Then
            StrCondition1 = StrCondition1 & " And  LG.Site_Code IN (" & FGMain(GFilterCode, 4).Value & ") "
        Else
            StrCondition1 = StrCondition1 & " And  LG.Site_Code IN (" & AgL.PubSiteList & ") "
        End If

        StrSQLQuery = "SELECT DocId,Vr_DocId,VSno,VNo,AdjVNo,VDate AS VDate,AdjDate AS AdjDate, "
        StrSQLQuery += "VType,AdjVType,PName As PName,Narration AS Narr,AdjNarr AS AdjNarr,'" & DrCr & "' As DRCR, "
        StrSQLQuery += "Amt1 AS Amt1,Amt2 AS Amt2,AdjAmt AS AdjAmt,CityName AS CityName,SiteName AS SiteName "
        StrSQLQuery += "FROM ( "
        StrSQLQuery += "Select LG.DocId,LG.V_SNo AS VSno,Convert(Varchar,LG.V_No) as VNo,Convert(Varchar,LG1.V_No) As AdjVNo, "
        StrSQLQuery += "LG.V_Date as VDate,LG1.V_Date  AS AdjDate, SG.Name As PName,"
        StrSQLQuery += "LG.Narration as Narration,LG1.Narration  AS AdjNarr," & StrAmt1 & " As Amt1,0 As Amt2, "
        StrSQLQuery += "IsNull(LA1.Amount,0) AS AdjAmt,C.CityName As CityName,(St.name) As SiteName, "
        StrSQLQuery += "LA1.Vr_DocId,LG.V_Type AS VType ,LG1.V_Type AS AdjVType "
        StrSQLQuery += "From  Ledger_Temp LG Left Join SubGroup SG On LG.Subcode=SG.SubCode Left Join "
        StrSQLQuery += "City C on SG.CityCode=C.CityCode Left Join LedgerAdj_Temp LA1 On LG.DocId=LA1.Adj_DocId And LG.V_SNo=LA1.Adj_V_SNo "
        StrSQLQuery += "LEFT JOIN Ledger LG1 ON LG1.DocId =LA1.Vr_DocId And LG1.V_SNo=LA1.Vr_V_SNo "
        StrSQLQuery += "LEFT JOIN AcGroup AG ON SG.GroupCode =AG.GroupCode  "
        StrSQLQuery += "Left Join SiteMast ST ON LG.Site_Code=St.code "
        StrSQLQuery += StrCondition1 & " And " & StrAmt1 & " > 0 And IsNull(LA1.Amount, 0) <> " & StrAmt1 & " "
        StrSQLQuery += "Union All "
        StrSQLQuery += "Select LG.DocId,LG.V_SNo AS VSno,NULL as VNo,Convert(Varchar,LG.V_No) As AdjVNo,LG.V_Date as VDate, "
        StrSQLQuery += "LG.V_Date AS AdjDate,SG.Name As PName,LG.Narration as Narration,LG.Narration AS AdjNarr, "
        StrSQLQuery += "0 As Amt1," & StrAmt2 & "-ISNULL(T.AMOUNT,0) as Amt2,0 AS AdjAmt, "
        StrSQLQuery += "C.CityName As CityName,ST.name As sitename, "
        StrSQLQuery += "LG.DocId AS Vr_DocId,LG.V_Type AS VType,LG.V_Type AS AdjVType  "
        StrSQLQuery += "From Ledger_Temp LG Left Join SubGroup SG On SG.SubCode=LG.SubCode "
        StrSQLQuery += "Left Join City C on SG.CityCode=C.CityCode "
        StrSQLQuery += "LEFT JOIN (SELECT LA.Vr_Docid AS Docid,LA.Vr_V_SNo AS S_No,SUM(AMOUNT) AS AMOUNT FROM LedgerAdj LA GROUP BY LA.Vr_DocId,LA.Vr_V_SNo) T ON T.DOCID=LG.DOCID AND T.S_NO=LG.V_SNO  "
        StrSQLQuery += "LEFT JOIN AcGroup AG ON SG.GroupCode =AG.GroupCode  "
        StrSQLQuery += "Left Join SiteMast ST ON LG.Site_Code=St.code "
        StrSQLQuery += StrCondition1 & " And " & StrAmt2 & " > 0 And " & StrAmt2 & "-ISNULL(T.AMOUNT,0)<>0 "
        StrSQLQuery += ") As Tmp "
        StrSQLQuery += "Order By VDate,AdjDate,DocId,Vr_DocId "

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
        FRH_Multiple(5) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable("Select 'o' As Tick,Code,Description as Name,ManualCode  FROM Item   " & _
                          "Order By Description", AgL.GCn)), "", 600, 580, , , False)
        '"Where  " & Agl.PubSiteListCharIndex & "" & _

        FRH_Multiple(5).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(5).FFormatColumn(1, , 0, , False)
        FRH_Multiple(5).FFormatColumn(2, "Name", 360, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple(5).FFormatColumn(3, "Code", 100, DataGridViewContentAlignment.MiddleLeft)

        StrSQL = "Declare @TmpTable Table(Code NVarChar(1),Name NVarChar(15)) "
        StrSQL += "Insert Into @TmpTable Values('S','Summary') "
        StrSQL += "Insert Into @TmpTable Values('D','Detail') "
        StrSQL += "Select Code,Name From @TmpTable Order By Name "
        FSetValue(6, "Detail / Summary ", FGDataType.DT_Selection_Single, FilterCodeType.DTNone, "Detail", False)
        FRH_Single(6) = New DMHelpGrid.FrmHelpGrid(New DataView(CMain.FGetDatTable(StrSQL, AgL.GCn)), "", 200, 180, , , False)
        FRH_Single(6).FFormatColumn(0, , 0, , False)
        FRH_Single(6).FFormatColumn(1, "Name", 100, DataGridViewContentAlignment.MiddleLeft)

        StrSQL = "Declare @TmpTable Table(Code NVarChar(2),Name NVarChar(20)) "
        StrSQL += "Insert Into @TmpTable Values('WA','Weightage Average') "
        StrSQL += "Insert Into @TmpTable Values('FF','FIFO') "
        StrSQL += "Select Code,Name From @TmpTable Order By Name "
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

        StrCondition = " Where (ST.V_Date Between " & AgL.ConvertDate(FGMain(GFilter, 0).Value.ToString) & " And " & AgL.ConvertDate(FGMain(GFilter, 1).Value.ToString) & ") "
        StrConditionOP = " Where ST.V_Date < " & AgL.ConvertDate(FGMain(GFilter, 0).Value.ToString) & " "

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
        StrSQL += "(IsNull(Sum(ST.Qty_Rec),0) - IsNull(Sum(ST.Qty_Iss),0)) As OPQty, "
        StrSQL += "(IsNull(Sum((Case When IsNull(ST.Qty_Rec,0)<> 0 Then " & StrValueField & " Else 0 End)),0) -  "
        StrSQL += "IsNull(Sum((Case	When IsNull(ST.Qty_Iss,0) <> 0 Then " & StrValueField & " Else 0 End)),0)) As OPValue, "
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
        StrSQL += "Select Convert(Varchar,ST.V_No) as V_No,ST.DocId,ST.V_Type As V_Type,ST.V_Date,ST.Item, "
        StrSQL += "IsNull(IM.Description,'') As ItemName,IsNull(IM.Unit,'') As Unit, "
        StrSQL += "0 As OpQty,0 As OPValue, "
        StrSQL += "IsNull(ST.Qty_Rec,0) As RQty, "
        StrSQL += "(Case When IsNull(ST.Qty_Rec,0)<> 0 Then IsNull(" & StrValueField & ",0) Else 0 End) As RVal, "
        StrSQL += "IsNull(ST.Qty_Iss,0) As IQty, "
        StrSQL += "(Case When  IsNull(ST.Qty_Iss,0) <> 0 Then IsNull(" & StrValueField & ",0) Else 0 End) As IVal, "
        StrSQL += "1 As SNo,IsNull(VT.SerialNo,0) As SerialNo "
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
        FRH_Multiple(2) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable("Select  'o'  As Tick,max(ST.Consignor) As Code, S.Name,max(S.ManualCode) as ManualCode FROM STaxTrn ST " & _
                          " LEFT JOIN SubGroup S ON S.SubCode=ST.Consignor   " & _
                          "Where " & AgL.PubSiteListCharIndex & "" & _
                          "group by S.Name  Order By S.Name", AgL.GCn)), "", 600, 660, , , False)
        FRH_Multiple(2).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(2).FFormatColumn(1, , 0, , False)
        FRH_Multiple(2).FFormatColumn(2, "Name", 440, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple(2).FFormatColumn(3, "Code", 100, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(3, "Consignee Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, "All")
        FRH_Multiple(3) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable("Select   'o'  As Tick,max(ST.Consignee) As Code, S.Name,max(S.ManualCode) as ManualCode  FROM STaxTrn ST " & _
                          " LEFT JOIN SubGroup S ON S.SubCode=ST.Consignee   " & _
                          "Where " & AgL.PubSiteListCharIndex & "" & _
                          "group by S.Name  Order By S.Name", AgL.GCn)), "", 600, 660, , , False)
        FRH_Multiple(3).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(3).FFormatColumn(1, , 0, , False)
        FRH_Multiple(3).FFormatColumn(2, "Name", 440, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple(3).FFormatColumn(3, "Code", 100, DataGridViewContentAlignment.MiddleLeft)

        StrSQL = " Declare @TmpTable Table(Code nvarchar(1),name nvarchar(15)) "
        StrSQL += " insert into @TmpTable Values('G','G.T.A.')"
        StrSQL += " insert into @TmpTable Values('N','NON G.T.A.')"
        StrSQL += " Select Code,Name From @TmpTable Order By Name "
        FSetValue(4, "Report On Choice", FGDataType.DT_Selection_Single, FilterCodeType.DTNone, "G.T.A.", False)
        FRH_Single(4) = New DMHelpGrid.FrmHelpGrid(New DataView(CMain.FGetDatTable(StrSQL, AgL.GCn)), "", 200, 220, , , False)
        FRH_Single(4).FFormatColumn(0, , 0, , False)
        FRH_Single(4).FFormatColumn(1, "Name", 140, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(5, "Site Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, AgL.PubSiteName & "|'" & AgL.PubSiteCode & "'")
        FRH_Multiple(5) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable( _
                          "Select 'o' As Tick,Sm.Code,Sm.Name From Sitemast Sm where code in (" & AgL.PubSiteList & ")   Order By Sm.Name", _
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

        StrCondition = " Where (St.V_Date Between " & AgL.ConvertDate(FGMain(GFilter, 0).Value.ToString) & " And " & AgL.ConvertDate(FGMain(GFilter, 1).Value.ToString) & ") "
        StrConditionOp = " Where St.V_Date <  " & AgL.ConvertDate(FGMain(GFilter, 0).Value.ToString) & " "
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

            StrSQLQuery = "SELECT  " & AgL.ConvertDate(FGMain(GFilter, 0).Value.ToString) & " as V_Date,'' AS Consignor,'' AS Consignee,'' as VehicleNo, "
            StrSQLQuery += "'Opening' as  Description,''  AS FrPlace,'' AS ToPlace,'' as ConsignorBill,'' as ConsigneeBill,  "
            StrSQLQuery += "max(ST.EntryType) as EntryType,'' as Remark,datename(MM," & AgL.ConvertDate(FGMain(GFilter, 1).Value.ToString) & ")  As Month, "
            StrSQLQuery += "isnull(sum(case when ST.V_Type<>'STAXR' then ST.GAmount else (0 - ST.GAmount) end),0) as Gamount, "
            StrSQLQuery += "isnull(sum(case when ST.V_Type<>'STAXR' then ST.Exempted else (0 - ST.Exempted) end),0 ) as Exempted, "
            StrSQLQuery += "isnull(sum(case when ST.V_Type<>'STAXR' then ST.TaxableAmt else (0 - ST.TaxableAmt) end),0) TaxableAmt, "
            StrSQLQuery += "isnull(sum(case when ST.V_Type<>'STAXR' then ST.ServiceTaxAmt else (0 - ST.ServiceTaxAmt) end),0) ServiceTaxAmt, "
            StrSQLQuery += "isnull(sum(case when ST.V_Type<>'STAXR' then ST.ECessAmt else (0 - ST.ECessAmt) end ),0) ECessAmt, "
            StrSQLQuery += "isnull(sum(case when ST.V_Type<>'STAXR' then ST.SHCessAmt else (0 - ST.SHCessAmt) end),0) as SHCessAmt,max(ST.V_Type) As  V_Type, "
            StrSQLQuery += "Null As PtyBillNo,Null As PtyBillDt "
            StrSQLQuery += "FROM STaxTrn ST " + StrConditionOp
            StrSQLQuery += "and ST.EntryType='G'     "
            StrSQLQuery += "Union All "
            StrSQLQuery += "SELECT ST.V_Date,S.Name AS Consignor,S1.Name AS Consignee,ST.VehicleNo, "
            StrSQLQuery += "ST.Description,C.CityName AS FrPlace,C1.CityName AS ToPlace,ST.ConsignorBill,ST.ConsigneeBill,  "
            StrSQLQuery += "ST.EntryType,ST.Remark,datename(MM," & AgL.ConvertDate(FGMain(GFilter, 1).Value.ToString) & ") As Month, "
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

            DTTemp = CMain.FGetDatTable(StrSQLQuery, AgL.GCn)
            If Not DTTemp.Rows.Count > 0 Then MsgBox("No Records Found to Print.") : Exit Sub
            FLoadMainReport("GTAREGISTER", DTTemp)
            CMain.FormulaSet(RptMain, "G.T.A. Register", FGMain)
            CMain.FShowReport(RptMain, Me.MdiParent, Me.Text)
        Else

            StrSQLQuery = "SELECT " & AgL.ConvertDate(FGMain(GFilter, 0).Value.ToString) & " as V_Date,'' AS Consignor,'' as STNo,'Opening' as Description,'' as ConsignorBill,'' as Remark,"
            StrSQLQuery += "isnull(sum(case when ST.V_Type<>'STXNR' then ST.ServiceTaxAmt else (0 - ST.ServiceTaxAmt) end),0) as ServiceTaxAmt, "
            StrSQLQuery += "isnull(sum(case when ST.V_Type<>'STXNR' then ST.ECessAmt else (0 - ST.ECessAmt) end ),0) as ECessAmt, "
            StrSQLQuery += "isnull(sum(case when ST.V_Type<>'STXNR' then ST.SHCessAmt else (0 - ST.SHCessAmt) end),0) as SHCessAmt, "
            StrSQLQuery += "isnull(sum((Case When IsNull(ST.VrRefDocId,'')<>'' Then 0 Else (case when ST.V_Type<>'STXNR' then ST.NetAmount else (0 - ST.NetAmount) end) End)),0) as NetAmount, "
            StrSQLQuery += "max(ST.EntryType)as EntryType ,'' As V_Type, "
            StrSQLQuery += "Null As PtyBillNo,Null As PtyBillDt,Null As Chq_No,Null As Chq_Date,Null As Narration,Null As PmtDate "
            StrSQLQuery += " FROM STaxTrn ST " + StrConditionOp
            StrSQLQuery += " and ST.EntryType='N' "
            StrSQLQuery += " Union All "
            StrSQLQuery += " SELECT ST.V_Date,S.Name AS Consignor,S.STNo,ST.Description,ST.ConsignorBill,ST.Remark,"
            StrSQLQuery += " case when ST.V_Type<>'STXNR' then ST.ServiceTaxAmt else (0 - ST.ServiceTaxAmt) end as ServiceTaxAmt, "
            StrSQLQuery += " case when ST.V_Type<>'STXNR' then ST.ECessAmt else (0 - ST.ECessAmt) end as ECessAmt, "
            StrSQLQuery += " case when ST.V_Type<>'STXNR' then ST.SHCessAmt else (0 - ST.SHCessAmt) end as SHCessAmt, "
            StrSQLQuery += " (Case When IsNull(ST.VrRefDocId,'')<>'' Then 0 Else (case when ST.V_Type<>'STXNR' then ST.NetAmount else (0 - ST.NetAmount) end) End) as NetAmount, "
            StrSQLQuery += " ST.EntryType,ST.V_Type, "
            StrSQLQuery += " ST.PtyBillNo,ST.PtyBillDt,L.Chq_No,L.Chq_Date,L.Narration,L.V_Date As PmtDate  "
            StrSQLQuery += " FROM STaxTrn ST"
            StrSQLQuery += " LEFT JOIN SubGroup S ON S.SubCode=ST.Consignor"
            StrSQLQuery += " LEFT JOIN SubGroup S1 ON S1.SubCode=ST.Consignee  "
            StrSQLQuery += " LEFT JOIN Ledger L ON L.DocId=ST.VrRefDocId AND L.V_SNo=ST.VrRef_Sno " + StrCondition
            StrSQLQuery += " and ST.EntryType='N' "

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
        FRH_Multiple(2) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable( _
                          "Select 'o' As Tick,LG.Code,LG.Name From LedgerGroup LG Order By LG.Name", _
                          AgL.GCn)), "", 600, 560, , , False)
        FRH_Multiple(2).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(2).FFormatColumn(1, , 0, , False)
        FRH_Multiple(2).FFormatColumn(2, "Ledger Name", 440, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(3, "Site Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, AgL.PubSiteName & "|'" & AgL.PubSiteCode & "'", False)
        FRH_Multiple(3) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable( _
                          "Select 'o' As Tick,Sm.Code,Sm.Name From Sitemast Sm where code in (" & AgL.PubSiteList & ")   Order By Sm.Name", _
                          AgL.GCn)), "", 300, 360, , , False, AgL.PubSiteCode)
        FRH_Multiple(3).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(3).FFormatColumn(1, , 0, , False)
        FRH_Multiple(3).FFormatColumn(2, "Name", 240, DataGridViewContentAlignment.MiddleLeft)

        StrSQL = "Declare @TmpTable Table(Code nvarchar(4),name nvarchar(20))insert into @TmpTable Values('Y','Yes')insert into @TmpTable Values('N','No')Select Code,Name From @TmpTable Order By Name"

        FSetValue(4, "Index Needed", FGDataType.DT_Selection_Single, FilterCodeType.DTNone, "No", False)
        FRH_Single(4) = New DMHelpGrid.FrmHelpGrid(New DataView(CMain.FGetDatTable(StrSQL, AgL.GCn)), "", 150, 180, , , False)
        FRH_Single(4).FFormatColumn(0, , 0, , False)
        FRH_Single(4).FFormatColumn(1, "Name", 100, DataGridViewContentAlignment.MiddleLeft)

        StrSQL = "Declare @TmpTable Table(Code nvarchar(4),name nvarchar(20))insert into @TmpTable Values('N','No') insert into @TmpTable Values ('Y','Yes') Select Code,Name From @TmpTable Order By Name"

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
        FRH_Multiple(2) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable( _
                          "Select 'o' As Tick,AG.GroupCode,AG.GroupName,AG1.GroupName From AcGroup AG LEFT JOIN AcGroup AG1 ON AG1.GroupCode = AG.GroupUnder Order By AG.GroupName", _
                          AgL.GCn)), "", 600, 560, , , False)
        FRH_Multiple(2).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(2).FFormatColumn(1, , 0, , False)
        FRH_Multiple(2).FFormatColumn(2, "Group Name", 220, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple(2).FFormatColumn(3, "Group Under", 220, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(3, "Site Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, AgL.PubSiteName & "|'" & AgL.PubSiteCode & "'", False)
        FRH_Multiple(3) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable( _
                          "Select 'o' As Tick,Sm.Code,Sm.Name From Sitemast Sm where code in (" & AgL.PubSiteList & ")   Order By Sm.Name", _
                          AgL.GCn)), "", 300, 360, , , False, AgL.PubSiteCode)
        FRH_Multiple(3).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(3).FFormatColumn(1, , 0, , False)
        FRH_Multiple(3).FFormatColumn(2, "Name", 240, DataGridViewContentAlignment.MiddleLeft)

        StrSQL = "Declare @TmpTable Table(Code nvarchar(4),name nvarchar(20))insert into @TmpTable Values('N','No') insert into @TmpTable Values ('Y','Yes') Select Code,Name From @TmpTable Order By Name"

        FSetValue(4, "Contra A/C Needed", FGDataType.DT_Selection_Single, FilterCodeType.DTNone, "No", False)
        FRH_Single(4) = New DMHelpGrid.FrmHelpGrid(New DataView(CMain.FGetDatTable(StrSQL, AgL.GCn)), "", 150, 180, , , False)
        FRH_Single(4).FFormatColumn(0, , 0, , False)
        FRH_Single(4).FFormatColumn(1, "Name", 100, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(5, "Voucher Type", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, "All", False)
        FRH_Multiple(5) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable( _
                          "Select 'o' As Tick, VT.V_Type AS Code,VT.V_Type ,VT.Description   FROM Voucher_Type VT WHERE VT.V_Type IN (SELECT V_Type FROM  Ledger Where  Site_code in (" & AgL.PubSiteList & "))   Order By VT.Description ", _
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

        StrCondition1 = " Where (LG.V_Date Between " & AgL.ConvertDate(FGMain(GFilter, 0).Value.ToString) & " And " & AgL.ConvertDate(FGMain(GFilter, 1).Value.ToString) & ") "
        StrConditionOP = " Where LG.V_Date < " & AgL.ConvertDate(FGMain(GFilter, 0).Value.ToString) & " "

        StrConditionsite = ""
        If Trim(FGMain(GFilterCode, 2).Value) <> "" Then StrCondition1 = StrCondition1 & " And LGG.Code In (" & FGMain(GFilterCode, 2).Value & ")"
        If Trim(FGMain(GFilterCode, 2).Value) <> "" Then StrConditionOP = StrConditionOP & " And LGG.Code In (" & FGMain(GFilterCode, 2).Value & ")"

        If Trim(FGMain(GFilterCode, 3).Value) <> "" Then
            StrConditionsite = " And LG.Site_Code In (" & FGMain(GFilterCode, 3).Value & ") "
        Else
            StrConditionsite = " And LG.Site_Code In  (" & AgL.PubSiteList & ") "
        End If

        '========== For Detail Section =======
        StrSQLQuery = "Select LG.V_Type,Convert(Varchar,LG.V_No) As V_No,LG.V_Date,LG.V_Prefix,SG.Name As PName,LG.SubCode,LG.Narration, "
        StrSQLQuery = StrSQLQuery + "LG.AmtDr,LG.AmtCr,1 As SNo,SM.Name As Division,LG.ContraText As ContraName,LG.Chq_No,LG.Chq_Date,"
        StrSQLQuery = StrSQLQuery + "IsNull(LG.Site_Code,'') As Site_Code,IsNull(LGG.Name,'') As LedgerGr,LGG.Code As Code "
        StrSQLQuery = StrSQLQuery + "From LedgerGroup LGG Left Join SubGroup SG On LGG.Code = SG.LedgerGroup "
        StrSQLQuery = StrSQLQuery + "Left Join Ledger LG ON LG.SubCode = SG.SubCode "
        StrSQLQuery = StrSQLQuery + "Left Join Sitemast SM On LG.Site_Code = SM.Code "

        StrSQLQuery = StrSQLQuery + StrCondition1 + StrConditionsite + "AND IsNull(LGG.Code,'')<>'' "
        StrSQLQuery = StrSQLQuery + "Union All "

        '======= For Opening Balance =========
        StrSQLQuery = StrSQLQuery + "Select	Null As V_Type,Null As V_No,Null As V_Date,Null As V_Prefix, "
        StrSQLQuery = StrSQLQuery + "Max(SG.Name) As PName,Max(LG.SubCode) As SubCode,'OPENING BALANCE' As Narration, "
        StrSQLQuery = StrSQLQuery + "(Case When (IsNull(Sum(LG.AmtDr),0)-IsNull(Sum(LG.AmtCr),0))>0 Then  "
        StrSQLQuery = StrSQLQuery + "(IsNull(Sum(LG.AmtDr),0)-IsNull(Sum(LG.AmtCr),0)) Else 0 End) As AmtDr, "
        StrSQLQuery = StrSQLQuery + "(Case When (IsNull(Sum(LG.AmtCr),0)-IsNull(Sum(LG.AmtDr),0))>0 Then "
        StrSQLQuery = StrSQLQuery + "(IsNull(Sum(LG.AmtCr),0)-IsNull(Sum(LG.AmtDr),0)) Else 0 End) As AmtCr,"
        StrSQLQuery = StrSQLQuery + "0 As SNo,max(SM.name) As Division,Null As ContraName,Null As Chq_No,Null As Chq_Date,"
        StrSQLQuery = StrSQLQuery + "Null As Site_Code,Max(IsNull(LGG.Name,'')) As LedgerGr,Max(LGG.Code) As Code "
        StrSQLQuery = StrSQLQuery + "From LedgerGroup LGG Left Join SubGroup SG On LGG.Code = SG.LedgerGroup "
        StrSQLQuery = StrSQLQuery + "Left Join Ledger LG ON LG.SubCode = SG.SubCode "
        StrSQLQuery = StrSQLQuery + "Left Join Sitemast SM On LG.Site_Code = SM.Code "

        StrSQLQuery = StrSQLQuery + StrConditionOP + StrConditionsite + "AND IsNull(LGG.Code,'')<>'' "

        StrSQLQuery = StrSQLQuery + "Group By LGG.Code "
        StrSQLQuery = StrSQLQuery + "Order By LedgerGr,V_Date,V_Type,V_No,SNo "

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

        StrCondition1 = " Where (LG.V_Date Between " & AgL.ConvertDate(FGMain(GFilter, 0).Value.ToString) & " And " & AgL.ConvertDate(FGMain(GFilter, 1).Value.ToString) & ") "
        StrConditionOP = " Where LG.V_Date < " & AgL.ConvertDate(FGMain(GFilter, 0).Value.ToString) & " "

        StrConditionsite = ""
        If Trim(FGMain(GFilterCode, 2).Value) <> "" Then StrCondition1 = StrCondition1 & " And (IsNull(SG.GroupCode,'') In (Select IsNull(AGP.GroupCode,'') From AcGroupPath AGP Where AGP.GroupUnder In (" & FGMain(GFilterCode, 2).Value & ")) Or SG.GroupCode In (" & FGMain(GFilterCode, 2).Value & ")) "
        If Trim(FGMain(GFilterCode, 2).Value) <> "" Then StrConditionOP = StrConditionOP & " And (IsNull(SG.GroupCode,'') In (Select IsNull(AGP.GroupCode,'') From AcGroupPath AGP Where AGP.GroupUnder In (" & FGMain(GFilterCode, 2).Value & ")) Or SG.GroupCode In (" & FGMain(GFilterCode, 2).Value & ")) "
        If Trim(FGMain(GFilterCode, 5).Value) <> "" Then StrCondition1 = StrCondition1 & " And LG.V_Type In (" & FGMain(GFilterCode, 5).Value & ")"

        If Trim(FGMain(GFilterCode, 3).Value) <> "" Then
            StrConditionsite = " and LG.site_Code In (" & FGMain(GFilterCode, 3).Value & ") "
        Else
            StrConditionsite = " and LG.site_Code In  (" & AgL.PubSiteList & ") "
        End If


        '========== For Detail Section =======
        StrSQLQuery = "Select LG.V_Type,Convert(Varchar,LG.V_No) As V_No,LG.V_Date,LG.V_Prefix,SG.Name  As PName,LG.SubCode,LG.Narration, "
        StrSQLQuery = StrSQLQuery + "LG.AmtDr,LG.AmtCr,1 As SNo,SM.Name As Division,LG.ContraText As ContraName,LG.Chq_No,LG.Chq_Date,"
        StrSQLQuery = StrSQLQuery + "IsNull(LG.Site_Code,'') As Site_Code,AG.GroupName As AccGrName,AG.GroupCode AS GroupCode "
        StrSQLQuery = StrSQLQuery + "From Ledger LG Left Join SubGroup SG On LG.Subcode = SG.SubCode "
        StrSQLQuery = StrSQLQuery + "Left Join AcGroup AG ON AG.GroupCode = SG.GroupCode "
        StrSQLQuery = StrSQLQuery + "Left Join Sitemast SM On LG.Site_Code = SM.Code "

        StrSQLQuery = StrSQLQuery + StrCondition1 + StrConditionsite
        StrSQLQuery = StrSQLQuery + "Union All "

        '======= For Opening Balance =========
        StrSQLQuery = StrSQLQuery + "Select	Null As V_Type,Null As V_No,Null As V_Date,Null As V_Prefix, "
        StrSQLQuery = StrSQLQuery + "Max(SG.Name)As PName,Max(LG.SubCode) As SubCode,'OPENING BALANCE' As Narration, "
        StrSQLQuery = StrSQLQuery + "(Case When (IsNull(Sum(LG.AmtDr),0)-IsNull(Sum(LG.AmtCr),0))>0 Then  "
        StrSQLQuery = StrSQLQuery + "(IsNull(Sum(LG.AmtDr),0)-IsNull(Sum(LG.AmtCr),0)) Else 0 End) As AmtDr, "
        StrSQLQuery = StrSQLQuery + "(Case When (IsNull(Sum(LG.AmtCr),0)-IsNull(Sum(LG.AmtDr),0))>0 Then "
        StrSQLQuery = StrSQLQuery + "(IsNull(Sum(LG.AmtCr),0)-IsNull(Sum(LG.AmtDr),0)) Else 0 End) As AmtCr,"
        StrSQLQuery = StrSQLQuery + "0 As SNo,Max(SM.name) As Division,Null As ContraName,Null As Chq_No,Null As Chq_Date,"
        StrSQLQuery = StrSQLQuery + "Null As Site_Code,Max(AG.GroupName) As AccGrName,Max(AG.GroupCode) AS GroupCode  "
        StrSQLQuery = StrSQLQuery + "From Ledger LG Left Join SubGroup SG On LG.Subcode = SG.SubCode "
        StrSQLQuery = StrSQLQuery + "Left Join AcGroup AG ON AG.GroupCode = SG.GroupCode "
        StrSQLQuery = StrSQLQuery + "Left Join Sitemast SM On LG.Site_Code = SM.Code "

        StrSQLQuery = StrSQLQuery + StrConditionOP + StrConditionsite

        StrSQLQuery = StrSQLQuery + "Group By AG.GroupCode "
        StrSQLQuery = StrSQLQuery + "Order By AccGrName,V_Date,V_Type,V_No,SNo "

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
        FRH_Multiple(2) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable( _
                         "Select Distinct  'o'  As Tick,S.GroupCode As Code,AG.GroupName AS Name From SubGroup S LEFT JOIN AcGroup AG ON AG.GroupCode=S.GroupCode Order By Name", _
                         AgL.GCn)), "", 400, 430, , , False)
        FRH_Multiple(2).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(2).FFormatColumn(1, , 0, , False)
        FRH_Multiple(2).FFormatColumn(2, "Name", 300, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(3, "Site Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, AgL.PubSiteName & "|'" & AgL.PubSiteCode & "'", False)
        FRH_Multiple(3) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable( _
                          "Select 'o' As Tick,Sm.Code,Sm.Name From Sitemast Sm where code in (" & AgL.PubSiteList & ")   Order By Sm.Name", _
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

        StrCondition1 = " Where (LG.V_Date Between " & AgL.ConvertDate(FGMain(GFilter, 0).Value.ToString) & " And " & AgL.ConvertDate(FGMain(GFilter, 1).Value.ToString) & ") "
        StrConditionsite = ""

        If Trim(FGMain(GFilterCode, 2).Value) <> "" Then StrCondition1 = StrCondition1 & " And (IsNull(SG.GroupCode,'') In (Select IsNull(AGP.GroupCode,'') From AcGroupPath AGP Where AGP.GroupUnder In (" & FGMain(GFilterCode, 2).Value & ")) Or SG.GroupCode In (" & FGMain(GFilterCode, 2).Value & ")) "

        If Trim(FGMain(GFilterCode, 3).Value) <> "" Then
            StrConditionsite = " and LG.site_Code In (" & FGMain(GFilterCode, 3).Value & ") "
        Else
            StrConditionsite = " and LG.site_Code In  (" & AgL.PubSiteList & ") "
        End If

        StrSQLQuery = "Select	LG.V_Type,Convert(Varchar,LG.V_No) As V_No,LG.V_Date,LG.V_Prefix,SG.Name  As PName,LG.SubCode,LG.Narration, "
        StrSQLQuery = StrSQLQuery + "LG.AmtCr,1 As SNo,LG.Chq_No,LG.Chq_Date,"
        StrSQLQuery = StrSQLQuery + "IsNull(C.CityName,'') as PCity,IsNull(LG.Site_Code,'') As Site_Code,AG.GroupName "
        StrSQLQuery = StrSQLQuery + "From Ledger LG Left Join SubGroup SG On LG.Subcode=SG.SubCode "
        StrSQLQuery = StrSQLQuery + "LEFT JOIN AcGroup AG ON AG.GroupCode=SG.GroupCode "
        StrSQLQuery = StrSQLQuery + "Left Join Sitemast SM On LG.Site_Code=SM.Code "
        StrSQLQuery = StrSQLQuery + "Left Join City C On C.CityCode=SG.CityCode "

        StrSQLQuery = StrSQLQuery + StrCondition1 + StrConditionsite + " And LG.V_Type IN ('RCT','CRV') AND LG.AmtCr>0 "
        StrSQLQuery = StrSQLQuery + "Order By V_Date,V_No,PName,SNo "


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
        FRH_Multiple(2) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable( _
                         "Select Distinct  'o'  As Tick,S.GroupCode As Code,AG.GroupName AS Name From SubGroup S LEFT JOIN AcGroup AG ON AG.GroupCode=S.GroupCode Order By Name", _
                         AgL.GCn)), "", 400, 430, , , False)
        FRH_Multiple(2).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(2).FFormatColumn(1, , 0, , False)
        FRH_Multiple(2).FFormatColumn(2, "Name", 300, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(3, "Site Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, AgL.PubSiteName & "|'" & AgL.PubSiteCode & "'", False)
        FRH_Multiple(3) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable( _
                          "Select 'o' As Tick,Sm.Code,Sm.Name From Sitemast Sm where code in (" & AgL.PubSiteList & ")   Order By Sm.Name", _
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

        StrCondition1 = " Where (LG.V_Date Between " & AgL.ConvertDate(FGMain(GFilter, 0).Value.ToString) & " And " & AgL.ConvertDate(FGMain(GFilter, 1).Value.ToString) & ") "
        StrConditionsite = ""

        If Trim(FGMain(GFilterCode, 2).Value) <> "" Then StrCondition1 = StrCondition1 & " And (IsNull(SG.GroupCode,'') In (Select IsNull(AGP.GroupCode,'') From AcGroupPath AGP Where AGP.GroupUnder In (" & FGMain(GFilterCode, 2).Value & ")) Or SG.GroupCode In (" & FGMain(GFilterCode, 2).Value & ")) "
        If Trim(FGMain(GFilterCode, 3).Value) <> "" Then
            StrConditionsite = " and LG.site_Code In (" & FGMain(GFilterCode, 3).Value & ") "
        Else
            StrConditionsite = " and LG.site_Code In  (" & AgL.PubSiteList & ") "
        End If

        StrSQLQuery = "Select	LG.V_Type,Convert(Varchar,LG.V_No) As V_No,LG.V_Date,LG.V_Prefix,SG.Name  As PName,LG.SubCode,LG.Narration, "
        StrSQLQuery = StrSQLQuery + "LG.AmtDr,1 As SNo,LG.Chq_No,LG.Chq_Date,"
        StrSQLQuery = StrSQLQuery + "IsNull(C.CityName,'') as PCity,IsNull(LG.Site_Code,'') As Site_Code,AG.GroupName "
        StrSQLQuery = StrSQLQuery + "From Ledger LG Left Join SubGroup SG On LG.Subcode=SG.SubCode "
        StrSQLQuery = StrSQLQuery + "LEFT JOIN AcGroup AG ON AG.GroupCode=SG.GroupCode "
        StrSQLQuery = StrSQLQuery + "Left Join Sitemast SM On LG.Site_Code=SM.Code "
        StrSQLQuery = StrSQLQuery + "Left Join City C On C.CityCode=SG.CityCode "

        StrSQLQuery = StrSQLQuery + StrCondition1 + StrConditionsite + " And LG.V_Type IN ('PMT','CPV') AND LG.AmtDr>0 "
        StrSQLQuery = StrSQLQuery + "Order By V_Date,V_No,PName,SNo "


        DTTemp = CMain.FGetDatTable(StrSQLQuery, AgL.GCn)

        If Not DTTemp.Rows.Count > 0 Then MsgBox("No Records Found to Print.") : Exit Sub

        FLoadMainReport("DailyExpenseReg", DTTemp)
        CMain.FormulaSet(RptMain, Me.Text, FGMain)
        CMain.FShowReport(RptMain, Me.MdiParent, Me.Text)
    End Sub
    Private Sub FINI_FIFOWsOS_DR()
        FSetValue(0, "As On Date", FGDataType.DT_Date, FilterCodeType.DTNone, AgL.PubLoginDate)
        FSetValue(1, "Account Group", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, "All")
        FRH_Multiple(1) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable("Select 'o' As Tick,AG.GroupCode,AG.GroupName From AcGroup  AG " & _
                          "Order By AG.GroupName", AgL.GCn)), "", 600, 520, , , False)
        FRH_Multiple(1).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(1).FFormatColumn(1, , 0, , False)
        FRH_Multiple(1).FFormatColumn(2, "Name", 400, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(2, "Account Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, "All", False)
        FRH_Multiple(2) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable( _
                          "Select 'o' As Tick,SG.SubCode,SG.Name,Sg.ManualCode,IsNull(CT.CityName,''),AG.GroupName, " & _
                          "IsNull(SG.Zone,'') From SubGroup SG Left Join " & _
                          "AcGroup AG On AG.GroupCode=SG.GroupCode Left Join " & _
                          "City CT On SG.CityCode=CT.CityCode Left Join " & _
                          "ZoneMast ZM On ZM.Code=SG.Zone Where " & AgL.PubSiteListCharIndex & " " & _
                          "Order By SG.Name", _
                          AgL.GCn)), "", 600, 920, , , False)
        FRH_Multiple(2).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(2).FFormatColumn(1, , 0, , False)
        FRH_Multiple(2).FFormatColumn(2, "Name", 300, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple(2).FFormatColumn(3, "Code", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple(2).FFormatColumn(4, "City", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple(2).FFormatColumn(5, "Group Name", 200, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple(2).FFormatColumn(6, "Zone", 100, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(3, "Interval", FGDataType.DT_Numeric, FilterCodeType.DTNumeric, 180, False)

        FSetValue(4, "Site Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, AgL.PubSiteName & "|'" & AgL.PubSiteCode & "'")
        FRH_Multiple(4) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable( _
                          "Select 'o' As Tick,Sm.Code,Sm.Name From Sitemast Sm where code in (" & AgL.PubSiteList & ")   Order By Sm.Name", _
                          AgL.GCn)), "", 300, 360, , , False, AgL.PubSiteCode)
        FRH_Multiple(4).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(4).FFormatColumn(1, , 0, , False)
        FRH_Multiple(4).FFormatColumn(2, "Name", 240, DataGridViewContentAlignment.MiddleLeft)
    End Sub
    Private Sub FINI_FIFOWsOS_Cr()
        FSetValue(0, "As On Date", FGDataType.DT_Date, FilterCodeType.DTNone, AgL.PubLoginDate)
        FSetValue(1, "Account Group", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, "All")
        FRH_Multiple(1) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable("Select 'o' As Tick,AG.GroupCode,AG.GroupName From AcGroup  AG " & _
                          "Order By AG.GroupName", AgL.GCn)), "", 600, 520, , , False)
        FRH_Multiple(1).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(1).FFormatColumn(1, , 0, , False)
        FRH_Multiple(1).FFormatColumn(2, "Name", 400, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(2, "Account Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, "All", False)
        FRH_Multiple(2) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable( _
                          "Select 'o' As Tick,SG.SubCode,SG.Name,Sg.ManualCode,IsNull(CT.CityName,''),AG.GroupName, " & _
                          "IsNull(SG.Zone,'') From SubGroup SG Left Join " & _
                          "AcGroup AG On AG.GroupCode=SG.GroupCode Left Join " & _
                          "City CT On SG.CityCode=CT.CityCode Left Join " & _
                          "ZoneMast ZM On ZM.Code=SG.Zone Where " & AgL.PubSiteListCharIndex & " " & _
                          "Order By SG.Name", _
                          AgL.GCn)), "", 600, 920, , , False)
        FRH_Multiple(2).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(2).FFormatColumn(1, , 0, , False)
        FRH_Multiple(2).FFormatColumn(2, "Name", 300, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple(2).FFormatColumn(3, "Code", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple(2).FFormatColumn(4, "City", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple(2).FFormatColumn(5, "Group Name", 200, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple(2).FFormatColumn(6, "Zone", 100, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(3, "Interval", FGDataType.DT_Numeric, FilterCodeType.DTNumeric, 180, False)

        FSetValue(4, "Site Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, AgL.PubSiteName & "|'" & AgL.PubSiteCode & "'")
        FRH_Multiple(4) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable( _
                          "Select 'o' As Tick,Sm.Code,Sm.Name From Sitemast Sm where code in (" & AgL.PubSiteList & ")   Order By Sm.Name", _
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

        If Not FIsValid(0) Then Exit Sub

        STRDATE = AgL.ConvertDate(FGMain(GFilter, 0).Value.ToString)

        StrCondition1 = " Where LG.V_Date < = " & AgL.ConvertDate(FGMain(GFilter, 0).Value.ToString) & "  "
        StrCondDt = " Where LG.V_Date < = " & AgL.ConvertDate(FGMain(GFilter, 0).Value.ToString) & "  "

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
    Private Sub FFIFOWsOS_Cr()
        Dim StrCondition1 As String, StrCondDt As String
        Dim StrSql As String, STRDATE As String
        Dim DTTemp As DataTable
        Dim StrCnd As String = ""
        Dim D1 As Integer
        If Not FIsValid(0) Then Exit Sub
        If Not FIsValid(1) Then Exit Sub

        STRDATE = AgL.ConvertDate(FGMain(GFilter, 0).Value.ToString)

        StrCondition1 = " Where LG.V_Date < = " & AgL.ConvertDate(FGMain(GFilter, 0).Value.ToString) & "  "
        StrCondDt = " Where LG.V_Date < = " & AgL.ConvertDate(FGMain(GFilter, 0).Value.ToString) & "  "

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
        FRH_Multiple(2) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable( _
                          "Select 'o' As Tick,Sm.Code,Sm.Name From Sitemast Sm where code in (" & AgL.PubSiteList & ")   Order By Sm.Name", _
                          AgL.GCn)), "", 300, 360, , , False, AgL.PubSiteCode)
        FRH_Multiple(2).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(2).FFormatColumn(1, , 0, , False)
        FRH_Multiple(2).FFormatColumn(2, "Name", 240, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(3, "Account Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, "All", True)
        FRH_Multiple(3) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable( _
                          "Select 'o' As Tick,SG.SubCode,SG.Name From SubGroup SG  Where SG.SiteList Like '%|" & AgL.PubSiteCode & "|%' Order by SG.Name", _
                          AgL.GCn)), "", 300, 370, , , False)
        FRH_Multiple(3).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(3).FFormatColumn(1, "", 0, , False)
        FRH_Multiple(3).FFormatColumn(2, "Name", 250, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(4, "Voucher Type", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, "All", True)
        FRH_Multiple(4) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable( _
                          "Select 'o' As Tick,VT.V_Type,VT.Description From Voucher_Type VT  Where VT.V_Type in ( Select DISTINCT V_Type from Ledger) Order by VT.Description", _
                          AgL.GCn)), "", 300, 320, , , False)
        FRH_Multiple(4).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(4).FFormatColumn(1, "", 0, , False)
        FRH_Multiple(4).FFormatColumn(2, "Voucher Type", 200, DataGridViewContentAlignment.MiddleLeft)

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

        StrConditionMain = " Where (V_Date Between " & AgL.ConvertDate(FGMain(GFilter, 0).Value.ToString) & " And " & AgL.ConvertDate(FGMain(GFilter, 1).Value.ToString) & ") "
        StrCondition1 = " Where (LG.V_Date Between " & AgL.ConvertDate(FGMain(GFilter, 0).Value.ToString) & " And " & AgL.ConvertDate(FGMain(GFilter, 1).Value.ToString) & ") "
        StrConditionOP = " Where LG.V_Date < " & AgL.ConvertDate(FGMain(GFilter, 0).Value.ToString) & " "
        StrConditionSite = ""
        If Trim(FGMain(GFilterCode, 3).Value) <> "" Then StrCondition1 = StrCondition1 & " And LG.SubCode In (" & FGMain(GFilterCode, 3).Value & ")"
        If Trim(FGMain(GFilterCode, 3).Value) <> "" Then StrConditionOP = StrConditionOP & " And LG.SubCode In (" & FGMain(GFilterCode, 3).Value & ")"

        If Trim(FGMain(GFilterCode, 2).Value) <> "" Then
            StrConditionSite = " And LG.Site_Code In (" & FGMain(GFilterCode, 2).Value & ") "
        Else
            StrConditionSite = " And LG.Site_Code In  (" & AgL.PubSiteList & ") "
        End If

        If Trim(FGMain(GFilterCode, 4).Value) <> "" Then StrCondition1 = StrCondition1 & " And LG.V_Type In (" & FGMain(GFilterCode, 4).Value & ")"
        If Trim(FGMain(GFilterCode, 4).Value) <> "" Then StrConditionOP = StrConditionOP & " And LG.V_Type In (" & FGMain(GFilterCode, 4).Value & ")"


        '========== Head Query Date Wise Grouping ====================================
        StrSQLQuery = "Select V_Date,IsNull(Sum(AmtDr),0) As AmtDr,IsNull(Sum(AmtCr),0) As AmtCr, "
        StrSQLQuery += "IsNull(Sum(OPBal),0) As OPBal "
        StrSQLQuery += "From ( "
        '========== For Detail Section =======
        StrSQLQuery += "Select	LG.V_Date, "
        StrSQLQuery += "IsNull(LG.AmtDr,0) As AmtDr ,IsNull(LG.AmtCr,0) As AmtCr,0 As OPBal  "
        StrSQLQuery += "From Ledger LG "
        StrSQLQuery += StrCondition1 + StrConditionSite
        StrSQLQuery += "Union All "

        '======= For Opening Balance =========
        StrSQLQuery += "Select " & AgL.ConvertDate(FGMain(GFilter, 0).Value.ToString) & " As V_Date, "
        StrSQLQuery += "0 As AmtDr,0 As AmtCr, "
        StrSQLQuery += "IsNull(Sum(LG.AmtDr),0)-IsNull(Sum(LG.AmtCr),0) As OPBal "
        StrSQLQuery += "From Ledger LG "
        StrSQLQuery += StrConditionOP + StrConditionSite
        StrSQLQuery += "Group By LG.V_Date "
        StrSQLQuery += " ) As Tmp "
        StrSQLQuery += StrConditionMain
        StrSQLQuery += "Group By V_Date "
        StrSQLQuery += "Order By V_Date "


        DTTemp = CMain.FGetDatTable(StrSQLQuery, AgL.GCn)

        If Not DTTemp.Rows.Count > 0 Then MsgBox("No Records Found to Print.") : Exit Sub

        FLoadMainReport("DailyTransactionSummary", DTTemp)


        CMain.FormulaSet(RptMain, "Daily Transaction Summary", FGMain)
        CMain.FShowReport(RptMain, Me.MdiParent, Me.Text)
    End Sub
    Private Sub FINI_MonthlyLedgerSummaryFull()

        FSetValue(0, "From Date", FGDataType.DT_Date, FilterCodeType.DTNone, AgL.PubStartDate)
        FSetValue(1, "To Date", FGDataType.DT_Date, FilterCodeType.DTNone, AgL.PubLoginDate)

        FSetValue(2, "Account Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, "All", False)
        FRH_Multiple(2) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable( _
                          "Select 'o' As Tick,SG.SubCode,SG.Name,Sg.ManualCode,IsNull(CT.CityName,'') From SubGroup SG Left Join City CT On SG.CityCode=CT.CityCode where " & AgL.PubSiteListCharIndex & " Order By SG.Name", _
                          AgL.GCn)), "", 600, 760, , , False)
        FRH_Multiple(2).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(2).FFormatColumn(1, , 0, , False)
        FRH_Multiple(2).FFormatColumn(2, "Name", 440, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple(2).FFormatColumn(3, "Code", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple(2).FFormatColumn(4, "City", 100, DataGridViewContentAlignment.MiddleLeft)


        FSetValue(3, "Site Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, AgL.PubSiteName & "|'" & AgL.PubSiteCode & "'", False)
        FRH_Multiple(3) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable( _
                          "Select 'o' As Tick,Sm.Code,Sm.Name From Sitemast Sm where code in (" & AgL.PubSiteList & ")   Order By Sm.Name", _
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

        StrCondition1 = " Where (LG.V_Date Between " & AgL.ConvertDate(FGMain(GFilter, 0).Value.ToString) & " And " & AgL.ConvertDate(FGMain(GFilter, 1).Value.ToString) & ") "
        StrConditionOP = " Where LG.V_Date < " & AgL.ConvertDate(FGMain(GFilter, 0).Value.ToString) & " "
        StrConditionsite = ""
        If Trim(FGMain(GFilterCode, 2).Value) <> "" Then StrCondition1 = StrCondition1 & " And LG.SubCode In (" & FGMain(GFilterCode, 2).Value & ")"
        If Trim(FGMain(GFilterCode, 2).Value) <> "" Then StrConditionOP = StrConditionOP & " And LG.SubCode In (" & FGMain(GFilterCode, 2).Value & ")"

        If Trim(FGMain(GFilterCode, 3).Value) <> "" Then
            StrConditionsite = " and LG.site_Code In (" & FGMain(GFilterCode, 3).Value & ") "
        Else
            StrConditionsite = " and LG.site_Code In  (" & AgL.PubSiteList & ") "
        End If
        StrSQLQuery = "Select Max(SName) As SName,IsNull(Sum(AmtDr),0) As AmtDr, IsNull(Sum(AmtCr),0) As AmtCr, "
        StrSQLQuery += "Max(Month) AS Month,Max(Narration) AS Narration,ID  "
        StrSQLQuery += "From "
        '======= For Opening Balance =========
        StrSQLQuery += "( Select IsNull(SG.SubCode,'') As SubCode, Max(IsNull(SG.Name,'')) As SName, "
        StrSQLQuery += "(Case When (IsNull(Sum(LG.AmtDr),0)-IsNull(Sum(LG.AmtCr),0))>0 Then  "
        StrSQLQuery += "(IsNull(Sum(LG.AmtDr),0)-IsNull(Sum(LG.AmtCr),0)) Else 0 End) As AmtDr, "
        StrSQLQuery += "(Case When (IsNull(Sum(LG.AmtCr),0)-IsNull(Sum(LG.AmtDr),0))>0 Then "
        StrSQLQuery += "(IsNull(Sum(LG.AmtCr),0)-IsNull(Sum(LG.AmtDr),0)) Else 0 End) As AmtCr, '' AS Month,'OPENING BALANCE' As Narration,0 AS ID,0 as MON,0 as yr  "
        StrSQLQuery += "From Ledger LG  "
        StrSQLQuery += "Left Join SubGroup SG On LG.SubCode=SG.SubCode "
        StrSQLQuery += StrConditionOP + StrConditionsite
        StrSQLQuery += "Group By IsNull(SG.SubCode,'') "
        '======= For Detail =========
        StrSQLQuery += "Union All "
        StrSQLQuery += "Select	IsNull(SG.SubCode,'') As SubCode, Max(IsNull(SG.Name,'')) As SName, "
        StrSQLQuery += "IsNull(Sum(LG.AmtDr),0) As AmtDr, "
        StrSQLQuery += "IsNull(Sum(LG.AmtCr),0) As AmtCr, "
        StrSQLQuery += "Max(datename(Month ,LG.V_date) +' ' + (datename(year ,LG.V_date))) AS Month,'' As Narration,1 AS ID,  "
        StrSQLQuery += "max(month(LG.V_date)) AS MON,max(year(LG.V_date)) AS yr "
        StrSQLQuery += "From Ledger  "
        StrSQLQuery += "LG Left Join SubGroup SG On LG.SubCode=SG.SubCode "
        StrSQLQuery += StrCondition1 + StrConditionsite
        StrSQLQuery += "Group By IsNull(SG.SubCode,''),(datename(Month ,LG.V_date) +' ' + (datename(year ,LG.V_date))) "
        StrSQLQuery += " ) As Tmp "
        StrSQLQuery += "Group By SubCode,ID,MON having isnull(SubCode,'')<>''  "
        StrSQLQuery += "Order By Max(SName),ID,MAX(Yr),MON "

        DTTemp = CMain.FGetDatTable(StrSQLQuery, AgL.GCn)

        If Not DTTemp.Rows.Count > 0 Then MsgBox("No Records Found to Print.") : Exit Sub
        FLoadMainReport("MonthlyLedgerSummaryFull", DTTemp)
        CMain.FormulaSet(RptMain, Me.Text, FGMain)
        CMain.FShowReport(RptMain, Me.MdiParent, Me.Text)
    End Sub
    Private Sub FINI_TrialDetailDrCr()
        FSetValue(0, "From Date", FGDataType.DT_Date, FilterCodeType.DTNone, AgL.PubStartDate)
        FSetValue(1, "To Date", FGDataType.DT_Date, FilterCodeType.DTNone, AgL.PubLoginDate)

        FSetValue(2, "A/C Group", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, "All")
        FRH_Multiple(2) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable( _
                         "Select Distinct  'o'  As Tick,S.GroupCode As Code,AG.GroupName AS Name From SubGroup S LEFT JOIN AcGroup AG ON AG.GroupCode=S.GroupCode Order By Name", _
                         AgL.GCn)), "", 400, 430, , , False)
        FRH_Multiple(2).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(2).FFormatColumn(1, , 0, , False)
        FRH_Multiple(2).FFormatColumn(2, "Name", 300, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(3, "Account Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, "All", False)
        FRH_Multiple(3) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable( _
                          "Select 'o' As Tick,SG.SubCode,SG.Name,Sg.ManualCode,IsNull(CT.CityName,'') From SubGroup SG Left Join City CT On SG.CityCode=CT.CityCode where " & AgL.PubSiteListCharIndex & " Order By SG.Name", _
                          AgL.GCn)), "", 600, 760, , , False)
        FRH_Multiple(3).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(3).FFormatColumn(1, , 0, , False)
        FRH_Multiple(3).FFormatColumn(2, "Name", 440, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple(3).FFormatColumn(3, "Code", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple(3).FFormatColumn(4, "City", 100, DataGridViewContentAlignment.MiddleLeft)


        FSetValue(4, "Site Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, AgL.PubSiteName & "|'" & AgL.PubSiteCode & "'", False)
        FRH_Multiple(4) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable( _
                          "Select 'o' As Tick,Sm.Code,Sm.Name From Sitemast Sm where code in (" & AgL.PubSiteList & ")   Order By Sm.Name", _
                          AgL.GCn)), "", 300, 360, , , False, AgL.PubSiteCode)
        FRH_Multiple(4).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(4).FFormatColumn(1, , 0, , False)
        FRH_Multiple(4).FFormatColumn(2, "Name", 240, DataGridViewContentAlignment.MiddleLeft)
    End Sub
    Private Sub FTrialDetailDrCr()
        Dim StrCondition1 As String, StrConditionOP As String, StrConditionsite As String
        Dim DTTemp As DataTable

        If Not FIsValid(0) Then Exit Sub
        If Not FIsValid(1) Then Exit Sub

        StrCondition1 = " Where (LG.V_Date Between " & AgL.ConvertDate(FGMain(GFilter, 0).Value.ToString) & " And " & AgL.ConvertDate(FGMain(GFilter, 1).Value.ToString) & ") "
        StrConditionOP = " Where LG.V_Date < " & AgL.ConvertDate(FGMain(GFilter, 0).Value.ToString) & " "
        StrConditionsite = ""

        If Trim(FGMain(GFilterCode, 2).Value) <> "" Then StrCondition1 = StrCondition1 & " And (IsNull(SG.GroupCode,'') In (Select IsNull(AGP.GroupCode,'') From AcGroupPath AGP Where AGP.GroupUnder In (" & FGMain(GFilterCode, 2).Value & ")) Or SG.GroupCode In (" & FGMain(GFilterCode, 2).Value & ")) "
        If Trim(FGMain(GFilterCode, 2).Value) <> "" Then StrConditionOP = StrConditionOP & " And (IsNull(SG.GroupCode,'') In (Select IsNull(AGP.GroupCode,'') From AcGroupPath AGP Where AGP.GroupUnder In (" & FGMain(GFilterCode, 2).Value & ")) Or SG.GroupCode In (" & FGMain(GFilterCode, 2).Value & ")) "

        If Trim(FGMain(GFilterCode, 3).Value) <> "" Then StrCondition1 = StrCondition1 & " And LG.SubCode In (" & FGMain(GFilterCode, 3).Value & ")"
        If Trim(FGMain(GFilterCode, 3).Value) <> "" Then StrConditionOP = StrConditionOP & " And LG.SubCode In (" & FGMain(GFilterCode, 3).Value & ")"

        If Trim(FGMain(GFilterCode, 4).Value) <> "" Then
            StrConditionsite = " and LG.site_Code In (" & FGMain(GFilterCode, 4).Value & ") "
        Else
            StrConditionsite = " and LG.site_Code In  (" & AgL.PubSiteList & ") "
        End If

        StrSQLQuery = "Select Max(GroupName) AS GroupName,Max(SName) As SName, IsNull(Sum(OPBalDr),0) As OPBalDr, "
        StrSQLQuery += "IsNull(Sum(OPBalCr),0) As OPBalCr,IsNull(Sum(AmtDr),0) As AmtDr, IsNull(Sum(AmtCr),0) As AmtCr "
        StrSQLQuery += "From "
        StrSQLQuery += "( Select Max(IsNull(AG.GroupName,'')) AS GroupName,IsNull(AG.GroupCode,'') As GroupCode, "
        StrSQLQuery += "IsNull(SG.SubCode,'') As SubCode, Max(IsNull(SG.Name,'')) As SName, "
        StrSQLQuery += "(Case When (IsNull(Sum(LG.AmtDr),0)-IsNull(Sum(LG.AmtCr),0))>0 Then  "
        StrSQLQuery += "(IsNull(Sum(LG.AmtDr),0)-IsNull(Sum(LG.AmtCr),0)) Else 0 End) As OPBalDr, "
        StrSQLQuery += "(Case When (IsNull(Sum(LG.AmtCr),0)-IsNull(Sum(LG.AmtDr),0))>0 Then "
        StrSQLQuery += "(IsNull(Sum(LG.AmtCr),0)-IsNull(Sum(LG.AmtDr),0)) Else 0 End) As OPBalCr,0 As AmtDr,0 As AmtCr "
        StrSQLQuery += "From Ledger LG Left Join SubGroup SG On LG.SubCode=SG.SubCode "
        StrSQLQuery += "LEFT JOIN AcGroup AG ON AG.GroupCode=SG.GroupCode "
        StrSQLQuery += StrConditionOP + StrConditionsite
        StrSQLQuery += "Group By IsNull(AG.GroupCode,''),IsNull(SG.SubCode,'') "
        StrSQLQuery += "Having(IsNull(Sum(LG.AmtDr), 0) - IsNull(Sum(LG.AmtCr), 0)) <> 0 "
        StrSQLQuery += "Union All "
        StrSQLQuery += "Select	Max(IsNull(AG.GroupName,'')) AS GroupName,IsNull(AG.GroupCode,'') As GroupCode, "
        StrSQLQuery += "IsNull(SG.SubCode,'') As SubCode, "
        StrSQLQuery += "Max(IsNull(SG.Name,'')) As SName, 0 As OPBalDr,0 As OPBalCr, "
        StrSQLQuery += "IsNull(Sum(LG.AmtDr),0) As AmtDr,  "
        StrSQLQuery += "IsNull(Sum(LG.AmtCr),0) As AmtCr "
        StrSQLQuery += "From Ledger LG Left Join SubGroup SG On LG.SubCode=SG.SubCode "
        StrSQLQuery += "LEFT JOIN AcGroup AG ON AG.GroupCode=SG.GroupCode "
        StrSQLQuery += StrCondition1 + StrConditionsite
        StrSQLQuery += "Group By IsNull(AG.GroupCode,''),IsNull(SG.SubCode,'') "
        StrSQLQuery += "Having (IsNull(Sum(LG.AmtDr),0)-IsNull(Sum(LG.AmtCr),0)) <> 0 ) As Tmp "
        StrSQLQuery += "Group By GroupCode,SubCode "
        StrSQLQuery += "Order By Max(GroupName),Max(SName) "

        DTTemp = CMain.FGetDatTable(StrSQLQuery, AgL.GCn)

        If Not DTTemp.Rows.Count > 0 Then MsgBox("No Records Found to Print.") : Exit Sub

        FLoadMainReport("TrailDetailDrCr", DTTemp)
        CMain.FormulaSet(RptMain, Me.Text, FGMain)
        CMain.FShowReport(RptMain, Me.MdiParent, Me.Text)
    End Sub
    Private Sub FINI_MonthlyLedgerSummary()
        Dim DTTemp As DataTable
        Dim StrReportForCode As String = ""

        DTTemp = CMain.FGetDatTable("SELECT GroupCode FROM AcGroup WHERE GroupName='" & StrReportFor & "'", AgL.GCn)
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
        FRH_Multiple(1) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable( _
                         "Select Distinct  'o'  As Tick,S.GroupCode As Code,AG.GroupName AS Name From SubGroup S LEFT JOIN AcGroup AG ON AG.GroupCode=S.GroupCode Order By Name", _
                         AgL.GCn)), "", 400, 430, , , False)
        FRH_Multiple(1).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(1).FFormatColumn(1, , 0, , False)
        FRH_Multiple(1).FFormatColumn(2, "Name", 300, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(2, "Site Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, AgL.PubSiteName & "|'" & AgL.PubSiteCode & "'")
        FRH_Multiple(2) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable( _
                          "Select 'o' As Tick,Sm.Code,Sm.Name From Sitemast Sm where code in (" & AgL.PubSiteList & ")   Order By Sm.Name", _
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

        If Trim(FGMain(GFilterCode, 0).Value) = "F" Then
            TempField = "0 As Sel "
            DblFirstYear = Year(AgL.PubStartDate)
            DblSecondYear = Year(AgL.PubStartDate)
            StrCondition1 += "Where Month(LG.V_Date) In (4,5,6,7,8,9) And Year(LG.V_Date) In (" & DblFirstYear & ") "
        Else
            TempField = "1 As Sel "
            DblFirstYear = Year(AgL.PubStartDate)
            DblSecondYear = Year(AgL.PubEndDate)
            StrCondition1 += "Where Month(LG.V_Date) In (10,11,12,1,2,3) And Year(LG.V_Date) In (" & DblFirstYear & "," & DblSecondYear & ") "
        End If
        If Trim(FGMain(GFilterCode, 2).Value) <> "" Then
            StrCondition1 = StrCondition1 & " And LG.Site_Code IN (" & FGMain(GFilterCode, 2).Value & ") "
        Else
            StrCondition1 = StrCondition1 & " And LG.Site_Code IN (" & AgL.PubSiteList & ") "
        End If

        If Trim(FGMain(GFilterCode, 1).Value) <> "" Then StrCondition1 = StrCondition1 & " And (IsNull(SG.GroupCode,'') In (Select IsNull(AGP.GroupCode,'') From AcGroupPath AGP Where AGP.GroupUnder In (" & FGMain(GFilterCode, 1).Value & ")) Or SG.GroupCode In (" & FGMain(GFilterCode, 1).Value & ")) "

        StrSQLQuery = "Select GroupName,PName,SubCode,"
        StrSQLQuery += "(Case When (IsNull(DR_1,0)-IsNull(CR_1,0))>0 Then (IsNull(DR_1,0)-IsNull(CR_1,0)) Else 0 End) As DR_1, "
        StrSQLQuery += "(Case When (IsNull(CR_1,0)-IsNull(DR_1,0))>0 Then (IsNull(CR_1,0)-IsNull(DR_1,0)) Else 0 End) As CR_1, "
        StrSQLQuery += "(Case When (IsNull(DR_2,0)-IsNull(CR_2,0))>0 Then (IsNull(DR_2,0)-IsNull(CR_2,0)) Else 0 End) As DR_2, "
        StrSQLQuery += "(Case When (IsNull(CR_2,0)-IsNull(DR_2,0))>0 Then (IsNull(CR_2,0)-IsNull(DR_2,0)) Else 0 End) As CR_2, "
        StrSQLQuery += "(Case When (IsNull(DR_3,0)-IsNull(CR_3,0))>0 Then (IsNull(DR_3,0)-IsNull(CR_3,0)) Else 0 End) As DR_3, "
        StrSQLQuery += "(Case When (IsNull(CR_3,0)-IsNull(DR_3,0))>0 Then (IsNull(CR_3,0)-IsNull(DR_3,0)) Else 0 End) As CR_3, "
        StrSQLQuery += "(Case When (IsNull(DR_4,0)-IsNull(CR_4,0))>0 Then (IsNull(DR_4,0)-IsNull(CR_4,0)) Else 0 End) As DR_4, "
        StrSQLQuery += "(Case When (IsNull(CR_4,0)-IsNull(DR_4,0))>0 Then (IsNull(CR_4,0)-IsNull(DR_4,0)) Else 0 End) As CR_4, "
        StrSQLQuery += "(Case When (IsNull(DR_5,0)-IsNull(CR_5,0))>0 Then (IsNull(DR_5,0)-IsNull(CR_5,0)) Else 0 End) As DR_5, "
        StrSQLQuery += "(Case When (IsNull(CR_5,0)-IsNull(DR_5,0))>0 Then (IsNull(CR_5,0)-IsNull(DR_5,0)) Else 0 End) As CR_5, "
        StrSQLQuery += "(Case When (IsNull(DR_6,0)-IsNull(CR_6,0))>0 Then (IsNull(DR_6,0)-IsNull(CR_6,0)) Else 0 End) As DR_6, "
        StrSQLQuery += "(Case When (IsNull(CR_6,0)-IsNull(DR_6,0))>0 Then (IsNull(CR_6,0)-IsNull(DR_6,0)) Else 0 End) As CR_6, "
        StrSQLQuery += "Sel "
        StrSQLQuery += "From ( "
        StrSQLQuery += "Select	AG.GroupName,Max(SG.Name) As PName,LG.SubCode, "
        StrSQLQuery += "Sum(Case When (Month(LG.V_Date)=4 Or Month(LG.V_Date)=10)And Year(LG.V_Date)=" & DblFirstYear & " Then LG.AmtDr Else 0 End) As DR_1, "
        StrSQLQuery += "Sum(Case When (Month(LG.V_Date)=4 Or Month(LG.V_Date)=10)And Year(LG.V_Date)=" & DblFirstYear & " Then LG.AmtCr Else 0 End) As CR_1, "
        StrSQLQuery += "Sum(Case When (Month(LG.V_Date)=5 Or Month(LG.V_Date)=11)And Year(LG.V_Date)=" & DblFirstYear & " Then LG.AmtDr Else 0 End) As DR_2, "
        StrSQLQuery += "Sum(Case When (Month(LG.V_Date)=5 Or Month(LG.V_Date)=11)And Year(LG.V_Date)=" & DblFirstYear & " Then LG.AmtCr Else 0 End) As CR_2, "
        StrSQLQuery += "Sum(Case When (Month(LG.V_Date)=6 Or Month(LG.V_Date)=12)And Year(LG.V_Date)=" & DblFirstYear & " Then LG.AmtDr Else 0 End) As DR_3, "
        StrSQLQuery += "Sum(Case When (Month(LG.V_Date)=6 Or Month(LG.V_Date)=12)And Year(LG.V_Date)=" & DblFirstYear & " Then LG.AmtCr Else 0 End) As CR_3, "
        StrSQLQuery += "Sum(Case When (Month(LG.V_Date)=7 Or Month(LG.V_Date)=1)And Year(LG.V_Date)=" & DblSecondYear & " Then LG.AmtDr Else 0 End) As DR_4, "
        StrSQLQuery += "Sum(Case When (Month(LG.V_Date)=7 Or Month(LG.V_Date)=1)And Year(LG.V_Date)=" & DblSecondYear & " Then LG.AmtCr Else 0 End) As CR_4, "
        StrSQLQuery += "Sum(Case When (Month(LG.V_Date)=8 Or Month(LG.V_Date)=2)And Year(LG.V_Date)=" & DblSecondYear & " Then LG.AmtDr Else 0 End) As DR_5, "
        StrSQLQuery += "Sum(Case When (Month(LG.V_Date)=8 Or Month(LG.V_Date)=2)And Year(LG.V_Date)=" & DblSecondYear & " Then LG.AmtCr Else 0 End) As CR_5, "
        StrSQLQuery += "Sum(Case When (Month(LG.V_Date)=9 Or Month(LG.V_Date)=3)And Year(LG.V_Date)=" & DblSecondYear & " Then LG.AmtDr Else 0 End) As DR_6, "
        StrSQLQuery += "Sum(Case When (Month(LG.V_Date)=9 Or Month(LG.V_Date)=3)And Year(LG.V_Date)=" & DblSecondYear & " Then LG.AmtCr Else 0 End) As CR_6, "
        StrSQLQuery += TempField
        StrSQLQuery += "From Ledger LG Left Join "
        StrSQLQuery += "SubGroup SG ON LG.SubCode=SG.SubCode Left Join "
        StrSQLQuery += "AcGroup AG ON AG.GroupCode=SG.GroupCode "
        StrSQLQuery += StrCondition1
        StrSQLQuery += "Group By AG.GroupName,LG.SubCode "
        StrSQLQuery += ") As Tmp "
        StrSQLQuery += "Order By GroupName,PName "

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
        FRH_Multiple(5) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable( _
                          "Select 'o' As Tick,SG.SubCode,SG.Name,Sg.ManualCode From SubGroup SG Where " & AgL.PubSiteListCharIndex & " Order By SG.Name", _
                          AgL.GCn)), "", 600, 660, , , False)
        FRH_Multiple(5).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(5).FFormatColumn(1, , 0, , False)
        FRH_Multiple(5).FFormatColumn(2, "Name", 440, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple(5).FFormatColumn(3, "Code", 100, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(6, "Site Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, AgL.PubSiteName & "|'" & AgL.PubSiteCode & "'", False)
        FRH_Multiple(6) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable( _
                          "Select 'o' As Tick,Sm.Code,Sm.Name From Sitemast Sm where code in (" & AgL.PubSiteList & ")   Order By Sm.Name", _
                          AgL.GCn)), "", 300, 360, , , False, AgL.PubSiteCode)
        FRH_Multiple(6).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(6).FFormatColumn(1, , 0, , False)
        FRH_Multiple(6).FFormatColumn(2, "Name", 240, DataGridViewContentAlignment.MiddleLeft)
    End Sub
    Private Sub FInterestLedger()
        Dim StrCondition As String, StrField As String
        Dim DTTemp As DataTable

        If Not FIsValid(0) Then Exit Sub
        If Not FIsValid(1) Then Exit Sub
        If Not FIsValid(2) Then Exit Sub
        If Not FIsValid(3) Then Exit Sub
        If Not FIsValid(3) Then Exit Sub
        StrField = ""
        StrCondition = " Where (L.V_Date Between " & AgL.ConvertDate(FGMain(GFilter, 0).Value.ToString) & " And " & AgL.ConvertDate(FGMain(GFilter, 1).Value.ToString) & ") "

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

        StrSQLQuery = "SELECT Max(SG.Name) AS Party,Max(L.V_Date) AS VDate,max(L.V_type) as V_type, sum(amtdr) AS DRAmt,"
        StrSQLQuery += "sum(amtcr) AS CRAmt, sum(amtdr)-sum(amtcr) AS Bal" + StrField
        StrSQLQuery += " FROM Ledger L"
        StrSQLQuery += " LEFT JOIN SubGroup SG ON SG.SubCode=L.SubCode "
        StrSQLQuery += StrCondition
        StrSQLQuery += " GROUP BY L.SubCode,L.V_Date,L.V_No ORDER BY L.SubCode"


        DTTemp = CMain.FGetDatTable(StrSQLQuery, AgL.GCn)

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
        FRH_Multiple(2) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable("Select 'o' As Tick,SG.SubCode,SG.Name,SG.ManualCode From SubGroup  SG " & _
                          "Where Nature='Expenses' and " & AgL.PubSiteListCharIndex & "" & _
                          "Order By SG.Name", AgL.GCn)), "", 600, 660, , , False)
        FRH_Multiple(2).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(2).FFormatColumn(1, , 0, , False)
        FRH_Multiple(2).FFormatColumn(2, "Name", 440, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple(2).FFormatColumn(3, "Code", 100, DataGridViewContentAlignment.MiddleLeft)

        Strsql = " Declare @TmpTable Table(Code nvarchar(4),name nvarchar(20))insert into @TmpTable Values('Yes','Yes')insert into @TmpTable Values('No','No')Select Code,Name From @TmpTable Order By Name"

        FSetValue(3, "With Opening", FGDataType.DT_Selection_Single, FilterCodeType.DTNone, "Yes")
        FRH_Single(3) = New DMHelpGrid.FrmHelpGrid(New DataView(CMain.FGetDatTable(Strsql, AgL.GCn)), "", 150, 180, , , False)
        FRH_Single(3).FFormatColumn(0, , 0, , False)
        FRH_Single(3).FFormatColumn(1, "Name", 100, DataGridViewContentAlignment.MiddleLeft)


        FSetValue(4, "Site Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, AgL.PubSiteName & "|'" & AgL.PubSiteCode & "'")
        FRH_Multiple(4) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable( _
                          "Select 'o' As Tick,Sm.Code,Sm.Name From Sitemast Sm where code in (" & AgL.PubSiteList & ")   Order By Sm.Name", _
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
        FRH_Multiple(2) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable("Select 'o' As Tick,SG.SubCode,SG.Name,SG.ManualCode,IsNull(CT.CityName,'') From SubGroup SG Left Join City CT On SG.CityCode=CT.CityCode  " & _
                          "Where  " & AgL.PubSiteListCharIndex & "" & _
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

        Strsql = " Declare @TmpTable Table(Code nvarchar(4),name nvarchar(20))insert into @TmpTable Values('Yes','Yes')insert into @TmpTable Values('No','No')Select Code,Name From @TmpTable Order By Name"
        FSetValue(4, "Seperate Page", FGDataType.DT_Selection_Single, FilterCodeType.DTNone, "Yes", False)
        FRH_Single(4) = New DMHelpGrid.FrmHelpGrid(New DataView(CMain.FGetDatTable(Strsql, AgL.GCn)), "", 150, 180, , , False)
        FRH_Single(4).FFormatColumn(0, , 0, , False)
        FRH_Single(4).FFormatColumn(1, "Name", 100, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(5, "TDS Deduct From", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, "All", False)
        FRH_Multiple(5) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable("Select 'o' As Tick,SG.SubCode,SG.Name,SG.ManualCode,IsNull(CT.CityName,'') From SubGroup SG  Left Join Ledger LG On SG.SubCode=LG.TDSDeductFrom  Left Join City CT On SG.CityCode=CT.CityCode  " & _
                          "Where  " & AgL.PubSiteListCharIndex & "" & _
                          "ANd ISNULL(LG.TDSDeductFrom,'') <> ''  Order By SG.Name", AgL.GCn)), "", 600, 760, , , False)
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
        FRH_Multiple(7) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable( _
                          "Select 'o' As Tick,Sm.Code,Sm.Name From Sitemast Sm where code in (" & AgL.PubSiteList & ")   Order By Sm.Name", _
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

        Strsql = " Declare @TmpTable Table(Code nvarchar(4),name nvarchar(20))insert into @TmpTable Values('Yes','Yes')insert into @TmpTable Values('No','No')Select Code,Name From @TmpTable Order By Name"
        FSetValue(3, "Seperate Page", FGDataType.DT_Selection_Single, FilterCodeType.DTNone, "Yes", False)
        FRH_Single(3) = New DMHelpGrid.FrmHelpGrid(New DataView(CMain.FGetDatTable(Strsql, AgL.GCn)), "", 150, 180, , , False)
        FRH_Single(3).FFormatColumn(0, , 0, , False)
        FRH_Single(3).FFormatColumn(1, "Name", 100, DataGridViewContentAlignment.MiddleLeft)

        Strsql = " Declare @TmpTable Table(Code nvarchar(4),name nvarchar(20))insert into @TmpTable Values('Yes','Yes')insert into @TmpTable Values('No','No')Select Code,Name From @TmpTable Order By Name"
        FSetValue(4, "Party Wise", FGDataType.DT_Selection_Single, FilterCodeType.DTNone, "Yes", False)
        FRH_Single(4) = New DMHelpGrid.FrmHelpGrid(New DataView(CMain.FGetDatTable(Strsql, AgL.GCn)), "", 150, 180, , , False)
        FRH_Single(4).FFormatColumn(0, , 0, , False)
        FRH_Single(4).FFormatColumn(1, "Name", 100, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(5, "With Narration", FGDataType.DT_Selection_Single, FilterCodeType.DTNone, "No", True)
        FRH_Single(5) = New DMHelpGrid.FrmHelpGrid(New DataView(CMain.FGetDatTable(Strsql, AgL.GCn)), "", 150, 180, , , False)
        FRH_Single(5).FFormatColumn(0, , 0, , False)
        FRH_Single(5).FFormatColumn(1, "Name", 100, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(6, "Site Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, AgL.PubSiteName & "|'" & AgL.PubSiteCode & "'", False)
        FRH_Multiple(6) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable( _
                          "Select 'o' As Tick,Sm.Code,Sm.Name From Sitemast Sm where code in (" & AgL.PubSiteList & ")   Order By Sm.Name", _
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
            StrCondition = " And (L.V_Date <=" & AgL.ConvertDate(FGMain(GFilter, 1).Value.ToString) & " ) "
        Else
            StrCondition = " And (L.V_Date Between " & AgL.ConvertDate(FGMain(GFilter, 0).Value.ToString) & " And " & AgL.ConvertDate(FGMain(GFilter, 1).Value.ToString) & " ) "
        End If

        If Trim(FGMain(GFilterCode, 2).Value) <> "" Then StrCondition = StrCondition & " And L.SubCode In (" & FGMain(GFilterCode, 2).Value & ")"

        If Trim(FGMain(GFilterCode, 4).Value) <> "" Then
            StrCondition += " And  L.Site_Code IN (" & FGMain(GFilterCode, 4).Value & ") "
        Else
            StrCondition += " And  L.Site_Code IN (" & AgL.PubSiteList & ") "
        End If


        StrSQLQuery = "SELECT max(SG.SubCode) AS SubCode,SG.Name,"
        StrSQLQuery += "sum(L.AmtDr)-sum (L.AmtCr) AS DrBal,"
        StrSQLQuery += "Max(isnull(SG.FBTOnPer,0)) AS FBTOnPer,"
        StrSQLQuery += "Max(isnull(SG.FBTOnPer,0))*(sum(L.AmtDr)-sum (L.AmtCr))/100 AS FBTOn,"
        StrSQLQuery += "Max(isnull(SG.FBTPer,0)) AS FBTPer,"
        StrSQLQuery += "(Max(isnull(SG.FBTOnPer,0))*(sum(L.AmtDr)-sum (L.AmtCr))/100)*Max(isnull(SG.FBTPer,0))/100 AS FBT "
        StrSQLQuery += "FROM Ledger L "
        StrSQLQuery += "LEFT JOIN SubGroup SG ON SG.SubCode=L.SubCode "
        StrSQLQuery += "WHERE SG.Nature='Expenses' "
        StrSQLQuery += StrCondition
        StrSQLQuery += "AND isnull(SG.FBTOnPer,0)>0 "
        StrSQLQuery += "AND isnull(SG.FBTPer,0)>0 "
        StrSQLQuery += "GROUP BY SG.Name "
        StrSQLQuery += "HAVING(sum(L.AmtDr) - sum(L.AmtCr) > 0)"


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


        StrCondition = " And (L.V_Date Between " & AgL.ConvertDate(FGMain(GFilter, 0).Value.ToString) & " And " & AgL.ConvertDate(FGMain(GFilter, 1).Value.ToString) & " ) "


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


        StrSQLQuery = "SELECT IsNull(SG.Name,'') + ' ' + IsNull(C.CityName,'') AS Party,Convert(Varchar,L.V_No) As V_No,L.V_Type as VType,L.V_Date,L.Narration,"
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
        StrSQLQuery += "LEFT JOIN SubGroup SG3 ON SG3.SubCode=(SELECT TDSDeductFrom FROM Ledger WHERE DocId=L.Docid AND isnull(TDSDeductFrom,'')<>'' AND Subcode=L.ContraSub)  "
        StrSQLQuery += "LEFT JOIN City C ON C.CityCode=SG.CityCode "
        StrSQLQuery += "WHERE isnull(L.TDSCategory,'')<>'' AND isnull(L.tdsdesc,'')<>'' "
        StrSQLQuery += "AND L.System_Generated ='Y' "
        StrSQLQuery += StrCondition

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


        StrCondition = " And (L.V_Date Between " & AgL.ConvertDate(FGMain(GFilter, 0).Value.ToString) & " And " & AgL.ConvertDate(FGMain(GFilter, 1).Value.ToString) & " ) "


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

        StrSQLQuery = "SELECT SG.Name  AS Party,Convert(Varchar,L.V_No) As V_No,L.V_Type as VType,L.V_Date,L.Narration,"
        StrSQLQuery += "TC.Name AS TSDCat,TCD.Name AS Description,L.TdsOnAmt,L.TdsPer,L.Amtcr AS TdsAmt,"
        StrSQLQuery += "SG2.Name AS PostingAc,IsNull(C.CityName,'') As CityName,'" & FGMain(GFilter, 4).Value & "' As PWise  "
        StrSQLQuery += TempField
        StrSQLQuery += ",'" & strNarr & "'  AS NarYN "
        StrSQLQuery += "FROM Ledger L "
        StrSQLQuery += "LEFT JOIN SubGroup SG ON SG.SubCode =L.ContraSub "
        StrSQLQuery += "LEFT JOIN TDSCat TC ON TC.Code=L.TDSCategory "
        StrSQLQuery += "LEFT JOIN TDSCat_Description  TCD ON TCD.Code=L.TdsDesc "
        StrSQLQuery += "LEFT JOIN TdsCat_Det TD ON TD.TdsDesc =TCD.Code AND TD.Code=TC.Code "
        StrSQLQuery += "LEFT JOIN SubGroup SG2 ON SG2.SubCode=TD.AcCode "
        StrSQLQuery += "LEFT JOIN City C ON C.CityCode=SG.CityCode "
        StrSQLQuery += "WHERE isnull(L.TDSCategory,'')<>'' AND isnull(L.tdsdesc,'')<>'' "
        StrSQLQuery += "AND L.System_Generated ='Y' "
        StrSQLQuery += StrCondition & " Order By L.V_Date "


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
        FRH_Multiple(0) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable( _
                          "Select 'o' As Tick,'jan' As code,'Jan' As Name union all Select 'o' As Tick,'Feb' As code,'Feb' As Name union all Select 'o' As Tick,'Mar' As code,'Mar' As Name union all Select 'o' As Tick,'Apr' As code,'Apr' As Name union all Select 'o' As Tick,'May' As code,'May' As Name union all Select 'o' As Tick,'Jun' As code,'Jun' As Name union all Select 'o' As Tick,'July' As code,'July' As Name union all Select 'o' As Tick,'Aug' As code,'Aug' As Name union all Select 'o' As Tick,'Sep' As code,'Sep' As Name union all Select 'o' As Tick,'Oct' As code,'Oct' As Name union all Select 'o' As Tick,'Nov' As code,'Nov' As Name union all Select 'o' As Tick,'Dec' As code,'Dec' As Name  ", _
                          AgL.GCn)), "", 400, 250, , , False, AgL.PubSiteCode)
        FRH_Multiple(0).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(0).FFormatColumn(1, , 0, , False)
        FRH_Multiple(0).FFormatColumn(2, "Name", 130, DataGridViewContentAlignment.MiddleLeft)

        FRH_Multiple(1) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable( _
                         "Select 'o' As Tick,SG.SubCode,SG.Name,Sg.ManualCode From SubGroup SG Where SG.GroupNature='E' Order By SG.Name", _
                         AgL.GCn)), "", 400, 525, , , False)
        FRH_Multiple(1).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(1).FFormatColumn(1, , 0, , False)
        FRH_Multiple(1).FFormatColumn(2, "Name", 300, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(2, "Site Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, AgL.PubSiteName & "|'" & AgL.PubSiteCode & "'")
        FRH_Multiple(2) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable( _
                          "Select 'o' As Tick,Sm.Code,Sm.Name From Sitemast Sm where code in (" & AgL.PubSiteList & ")   Order By Sm.Name", _
                          AgL.GCn)), "", 300, 360, , , False, AgL.PubSiteCode)
        FRH_Multiple(2).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(2).FFormatColumn(1, , 0, , False)
        FRH_Multiple(2).FFormatColumn(2, "Name", 240, DataGridViewContentAlignment.MiddleLeft)

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
        FRH_Multiple(1) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable("Select 'o' As Tick,Ag.GroupCode,Ag.GroupName FROM AcGroup  AG   " & _
                          "Where AG.GroupCode In (SELECT AGP.GroupCode FROM AcGroupPath AGP WHERE AGP.GroupUnder='" & StrReportForCode & "') Or AG.GroupCode='" & StrReportForCode & "' " & _
                          "Order By AG.GroupName ", AgL.GCn)), "", 600, 460, , , False)
        FRH_Multiple(1).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(1).FFormatColumn(1, , 0, , False)
        FRH_Multiple(1).FFormatColumn(2, "Name", 340, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(2, "Account Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, "All")
        FRH_Multiple(2) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable("Select 'o' As Tick,SG.SubCode,SG.Name,SG.ManualCode,AG.GroupName From SubGroup  SG Left Join  " & _
                          "AcGroup AG On AG.GroupCode=SG.GroupCode " & _
                          "Where SG.GroupCode In (SELECT AGP.GroupCode FROM AcGroupPath AGP WHERE " & _
                          "AGP.GroupUnder='" & StrReportForCode & "') Or SG.GroupCode='" & StrReportForCode & "' and " & AgL.PubSiteListCharIndex & "" & _
                          "Order By SG.Name", AgL.GCn)), "", 600, 860, , , False)
        FRH_Multiple(2).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(2).FFormatColumn(1, , 0, , False)
        FRH_Multiple(2).FFormatColumn(2, "Name", 440, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple(2).FFormatColumn(3, "Code", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple(2).FFormatColumn(4, "Group Name", 200, DataGridViewContentAlignment.MiddleLeft)


        FSetValue(3, "Zone Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, "All")
        FRH_Multiple(3) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable( _
                          "Select 'o' As Tick,Zm.Code,Zm.Name From ZoneMast Zm  Order By Zm.Name", _
                          AgL.GCn)), "", 300, 360, , , False)
        FRH_Multiple(3).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(3).FFormatColumn(1, , 0, , False)
        FRH_Multiple(3).FFormatColumn(2, "Name", 240, DataGridViewContentAlignment.MiddleLeft)




        StrSQL = " Declare @TmpTable Table(Code nvarchar(1),name nvarchar(15)) "
        StrSQL += " insert into @TmpTable Values('S','Summary') "
        StrSQL += " insert into @TmpTable Values('D','Details') "
        StrSQL += " Select Code,Name From @TmpTable Order By Name "
        FSetValue(4, "Report On Choice", FGDataType.DT_Selection_Single, FilterCodeType.DTNone, "Details", False)
        FRH_Single(4) = New DMHelpGrid.FrmHelpGrid(New DataView(CMain.FGetDatTable(StrSQL, AgL.GCn)), "", 200, 220, , , False)
        FRH_Single(4).FFormatColumn(0, , 0, , False)
        FRH_Single(4).FFormatColumn(1, "Name", 140, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(5, "Site Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, AgL.PubSiteName & "|'" & AgL.PubSiteCode & "'")
        FRH_Multiple(5) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable( _
                          "Select 'o' As Tick,Sm.Code,Sm.Name From Sitemast Sm where code in (" & AgL.PubSiteList & ")   Order By Sm.Name", _
                          AgL.GCn)), "", 300, 360, , , False, AgL.PubSiteCode)
        FRH_Multiple(5).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(5).FFormatColumn(1, , 0, , False)
        FRH_Multiple(5).FFormatColumn(2, "Name", 240, DataGridViewContentAlignment.MiddleLeft)
    End Sub
    Private Sub FIni_journal()
        FSetValue(0, "From Date", FGDataType.DT_Date, FilterCodeType.DTNone, AgL.PubStartDate)
        FSetValue(1, "To Date", FGDataType.DT_Date, FilterCodeType.DTNone, AgL.PubLoginDate)

        FSetValue(2, "Voucher Type", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, "All")
        FRH_Multiple(2) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable( _
                          "select DISTINCT 'o' As Tick ,V_TYPE AS Code, RTRIM(LTRIM(Description))+' Book' AS Name from voucher_type Where category='JV'", _
                          AgL.GCn)), "", 600, 550, , , False)
        FRH_Multiple(2).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(2).FFormatColumn(1, , 0, , False)
        FRH_Multiple(2).FFormatColumn(2, "Name", 440, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(3, "Site Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, AgL.PubSiteName & "|'" & AgL.PubSiteCode & "'")
        FRH_Multiple(3) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable( _
                          "Select 'o' As Tick,Sm.Code,Sm.Name From Sitemast Sm where code in (" & AgL.PubSiteList & ")   Order By Sm.Name", _
                          AgL.GCn)), "", 300, 360, , , False, AgL.PubSiteCode)
        FRH_Multiple(3).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(3).FFormatColumn(1, , 0, , False)
        FRH_Multiple(3).FFormatColumn(2, "Name", 240, DataGridViewContentAlignment.MiddleLeft)
    End Sub
    Private Sub FINI_DayBook()
        FSetValue(0, "From Date", FGDataType.DT_Date, FilterCodeType.DTNone, AgL.PubStartDate)
        FSetValue(1, "To Date", FGDataType.DT_Date, FilterCodeType.DTNone, AgL.PubLoginDate)

        FSetValue(2, "Voucher Type", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, "All")
        FRH_Multiple(2) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable( _
                          "select DISTINCT 'o' As Tick ,V_TYPE AS Code, RTRIM(LTRIM(Description))+' Book' AS Name,Category from voucher_type order by Name ", _
                          AgL.GCn)), "", 600, 600, , , False)
        FRH_Multiple(2).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(2).FFormatColumn(1, , 0, , False)
        FRH_Multiple(2).FFormatColumn(2, "Name", 300, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple(2).FFormatColumn(3, "Category", 160, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(3, "Site Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, AgL.PubSiteName & "|'" & AgL.PubSiteCode & "'", False)
        FRH_Multiple(3) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable( _
                          "Select 'o' As Tick,Sm.Code,Sm.Name From Sitemast Sm where code in (" & AgL.PubSiteList & ")   Order By Sm.Name", _
                          AgL.GCn)), "", 300, 360, , , False, AgL.PubSiteCode)
        FRH_Multiple(3).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(3).FFormatColumn(1, , 0, , False)
        FRH_Multiple(3).FFormatColumn(2, "Name", 240, DataGridViewContentAlignment.MiddleLeft)
    End Sub
    Private Sub FIni_Annexure()
        FSetValue(0, "Up To Date", FGDataType.DT_Date, FilterCodeType.DTNone, AgL.PubLoginDate)
        FSetValue(1, "Account Group ", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, "All")
        FRH_Multiple(1) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable( _
                          "Select 'o' As Tick,AG.groupcode,AG.GroupName as Name, " & _
                          "(Case When GroupNature='L' Then 'Liabilities' " & _
                          "When GroupNature='A' Then 'Assets' " & _
                          "When GroupNature='E' Then 'Revenue' " & _
                          "When GroupNature='R' Then 'Expenses' End) MainGroup " & _
                          "From acgroup AG Order By AG.GroupName", _
                          AgL.GCn)), "", 600, 760, , , False)
        FRH_Multiple(1).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(1).FFormatColumn(1, , 0, , False)
        FRH_Multiple(1).FFormatColumn(2, "Name", 440, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple(1).FFormatColumn(3, "Main Group", 100, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(2, "Site Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, AgL.PubSiteName & "|'" & AgL.PubSiteCode & "'")
        FRH_Multiple(2) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable( _
                          "Select 'o' As Tick,Sm.Code,Sm.Name From Sitemast Sm where code in (" & AgL.PubSiteList & ")   Order By Sm.Name", _
                          AgL.GCn)), "", 300, 360, , , False, AgL.PubSiteCode)
        FRH_Multiple(2).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(2).FFormatColumn(1, , 0, , False)
        FRH_Multiple(2).FFormatColumn(2, "Name", 240, DataGridViewContentAlignment.MiddleLeft)
    End Sub
    Private Sub FIni_FixedAssetRegister()
        Dim DTTemp As DataTable

        FSetValue(0, "As ON Date", FGDataType.DT_Date, FilterCodeType.DTNone, AgL.PubLoginDate)

        FSetValue(1, "Group Name ", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, "All")
        FRH_Multiple(1) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable( _
                          "SELECT 'o' As Tick,Code,Name FROM AssetGroupMast Order By Name", _
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
        FRH_Multiple(2) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable( _
                          "Select 'o' As Tick,AG.GroupCode,AG.GroupName From AcGroup AG Order By AG.GroupName", _
                          AgL.GCn)), "", 600, 560, , , False)
        FRH_Multiple(2).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(2).FFormatColumn(1, , 0, , False)
        FRH_Multiple(2).FFormatColumn(2, "Group Name", 440, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(3, "Account Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, "All", False)
        FRH_Multiple(3) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable( _
                          "Select 'o' As Tick,SG.SubCode,SG.Name,Sg.ManualCode,IsNull(CT.CityName,''),AG.GroupName " & _
                          "From SubGroup SG Left Join " & _
                          "AcGroup AG On AG.GroupCode=SG.GroupCode Left Join " & _
                          "City CT On SG.CityCode=CT.CityCode where " & AgL.PubSiteListCharIndex & " " & _
                          "Order By SG.Name", _
                          AgL.GCn)), "", 600, 960, , , False)
        FRH_Multiple(3).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(3).FFormatColumn(1, , 0, , False)
        FRH_Multiple(3).FFormatColumn(2, "Name", 440, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple(3).FFormatColumn(3, "Code", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple(3).FFormatColumn(4, "City", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple(3).FFormatColumn(5, "Group Name", 200, DataGridViewContentAlignment.MiddleLeft)


        FSetValue(4, "Voucher Type", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, "All", False)
        FRH_Multiple(4) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable( _
                          "Select 'o' As Tick, VT.V_Type AS Code,VT.V_Type ,VT.Description   FROM Voucher_Type VT WHERE VT.V_Type IN (SELECT V_Type FROM  Ledger Where  Site_code in (" & AgL.PubSiteList & "))   Order By VT.Description ", _
                          AgL.GCn)), "", 300, 460, , , False, AgL.PubSiteCode)
        FRH_Multiple(4).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(4).FFormatColumn(1, , 0, , False)
        FRH_Multiple(4).FFormatColumn(2, "Type", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple(4).FFormatColumn(3, "Name", 240, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(5, "Site Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, AgL.PubSiteName & "|'" & AgL.PubSiteCode & "'", False)
        FRH_Multiple(5) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable( _
                          "Select 'o' As Tick,Sm.Code,Sm.Name From Sitemast Sm where code in (" & AgL.PubSiteList & ")   Order By Sm.Name", _
                          AgL.GCn)), "", 300, 360, , , False, AgL.PubSiteCode)
        FRH_Multiple(5).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(5).FFormatColumn(1, , 0, , False)
        FRH_Multiple(5).FFormatColumn(2, "Name", 240, DataGridViewContentAlignment.MiddleLeft)

        StrSQL = "Declare @TmpTable Table(Code nvarchar(4),name nvarchar(20))insert into @TmpTable Values('Y','Yes')insert into @TmpTable Values('N','No')Select Code,Name From @TmpTable Order By Name"

        FSetValue(6, "Index Needed", FGDataType.DT_Selection_Single, FilterCodeType.DTNone, "No", False)
        FRH_Single(6) = New DMHelpGrid.FrmHelpGrid(New DataView(CMain.FGetDatTable(StrSQL, AgL.GCn)), "", 150, 180, , , False)
        FRH_Single(6).FFormatColumn(0, , 0, , False)
        FRH_Single(6).FFormatColumn(1, "Name", 100, DataGridViewContentAlignment.MiddleLeft)

        StrSQL = "Declare @TmpTable Table(Code nvarchar(4),name nvarchar(20))insert into @TmpTable Values('N','No') insert into @TmpTable Values ('Y','Yes') Select Code,Name From @TmpTable Order By Name"

        FSetValue(7, "Contra A/C Needed", FGDataType.DT_Selection_Single, FilterCodeType.DTNone, "No", False)
        FRH_Single(7) = New DMHelpGrid.FrmHelpGrid(New DataView(CMain.FGetDatTable(StrSQL, AgL.GCn)), "", 150, 180, , , False)
        FRH_Single(7).FFormatColumn(0, , 0, , False)
        FRH_Single(7).FFormatColumn(1, "Name", 100, DataGridViewContentAlignment.MiddleLeft)
    End Sub

    Private Sub FIni_Bank_CashBook(ByVal StrTypeIn As String)
        Dim StrSQL As String

        FSetValue(0, "From Date", FGDataType.DT_Date, FilterCodeType.DTNone, AgL.PubStartDate, False)
        FSetValue(1, "To Date", FGDataType.DT_Date, FilterCodeType.DTNone, AgL.PubLoginDate, False)

        FSetValue(2, "Site Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, AgL.PubSiteName & "|'" & AgL.PubSiteCode & "'")
        FRH_Multiple(2) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable( _
                          "Select 'o' As Tick,Sm.Code,Sm.Name From Sitemast Sm where code in (" & AgL.PubSiteList & ")   Order By Sm.Name", _
                          AgL.GCn)), "", 300, 360, , , False, AgL.PubSiteCode)
        FRH_Multiple(2).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(2).FFormatColumn(1, , 0, , False)
        FRH_Multiple(2).FFormatColumn(2, "Name", 240, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(3, "Account", FGDataType.DT_Selection_Single, FilterCodeType.DTString, , True)
        FRH_Single(3) = New DMHelpGrid.FrmHelpGrid(New DataView(CMain.FGetDatTable( _
                          "Select SG.SubCode,SG.Name From SubGroup SG  Where SG.SiteList Like '%|" & AgL.PubSiteCode & "|%' And " & _
                          "SG.Nature In (Select Nature From AcFilteration Where V_Type In (" & StrTypeIn & ")) Order by SG.Name", _
                          AgL.GCn)), "", 300, 360, , , False)
        FRH_Single(3).FFormatColumn(0, "", 0, , False)
        FRH_Single(3).FFormatColumn(1, "Name", 250, DataGridViewContentAlignment.MiddleLeft)

        StrSQL = "Declare @TmpTable Table(Code nvarchar(4),name nvarchar(20))insert into @TmpTable Values('Y','Yes')insert into @TmpTable Values('N','No')Select Code,Name From @TmpTable Order By Name"
        FSetValue(4, "Page Wise", FGDataType.DT_Selection_Single, FilterCodeType.DTNone, "No", False)
        FRH_Single(4) = New DMHelpGrid.FrmHelpGrid(New DataView(CMain.FGetDatTable(StrSQL, AgL.GCn)), "", 150, 180, , , False)
        FRH_Single(4).FFormatColumn(0, , 0, , False)
        FRH_Single(4).FFormatColumn(1, "Name", 100, DataGridViewContentAlignment.MiddleLeft)

        StrSQL = "Declare @TmpTable Table(Code nvarchar(4),name nvarchar(20))insert into @TmpTable Values('Y','Yes')insert into @TmpTable Values('N','No')Select Code,Name From @TmpTable Order By Name"

        FSetValue(5, "With Narration", FGDataType.DT_Selection_Single, FilterCodeType.DTNone, "Yes|Y", False)
        FRH_Single(5) = New DMHelpGrid.FrmHelpGrid(New DataView(CMain.FGetDatTable(StrSQL, AgL.GCn)), "", 150, 180, , , False)
        FRH_Single(5).FFormatColumn(0, , 0, , False)
        FRH_Single(5).FFormatColumn(1, "Name", 100, DataGridViewContentAlignment.MiddleLeft)

        StrSQL = "Declare @TmpTable Table(Code NVarChar(4),Name NVarChar(20)) Insert Into @TmpTable Values('S','Single') Insert Into @TmpTable Values('D','Double') Insert Into @TmpTable Values('J','Journal') Select Code,Name From @TmpTable Order By Name"

        FSetValue(6, "Report Type", FGDataType.DT_Selection_Single, FilterCodeType.DTNone, "Single", False)
        FRH_Single(6) = New DMHelpGrid.FrmHelpGrid(New DataView(CMain.FGetDatTable(StrSQL, AgL.GCn)), "", 150, 180, , , False)
        FRH_Single(6).FFormatColumn(0, , 0, , False)
        FRH_Single(6).FFormatColumn(1, "Name", 100, DataGridViewContentAlignment.MiddleLeft)
    End Sub
    Private Sub FIni_TrialGroup()
        FSetValue(0, "As On Date", FGDataType.DT_Date, FilterCodeType.DTNone, AgL.PubLoginDate)
        FSetValue(1, "Site Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, AgL.PubSiteName & "|'" & AgL.PubSiteCode & "'")
        FRH_Multiple(1) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable( _
                          "Select 'o' As Tick,Sm.Code,Sm.Name From Sitemast Sm where code in (" & AgL.PubSiteList & ")   Order By Sm.Name", _
                          AgL.GCn)), "", 300, 360, , , False, AgL.PubSiteCode)
        FRH_Multiple(1).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(1).FFormatColumn(1, , 0, , False)
        FRH_Multiple(1).FFormatColumn(2, "Name", 240, DataGridViewContentAlignment.MiddleLeft)
    End Sub
    Private Sub FIni_TrialDetail()
        Dim StrSQL As String

        FSetValue(0, "As On Date", FGDataType.DT_Date, FilterCodeType.DTNone, AgL.PubLoginDate)
        FSetValue(1, "Site Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, AgL.PubSiteName & "|'" & AgL.PubSiteCode & "'")
        FRH_Multiple(1) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable( _
                          "Select 'o' As Tick,Sm.Code,Sm.Name From Sitemast Sm where code in (" & AgL.PubSiteList & ")   Order By Sm.Name", _
                          AgL.GCn)), "", 300, 360, , , False, AgL.PubSiteCode)
        FRH_Multiple(1).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(1).FFormatColumn(1, , 0, , False)
        FRH_Multiple(1).FFormatColumn(2, "Name", 240, DataGridViewContentAlignment.MiddleLeft)

        StrSQL = "Declare @TmpTable Table(Code NVarChar(1),Name NVarChar(15)) Insert Into @TmpTable Values('A','Alphabatical') Insert Into @TmpTable Values('M','Manual') Select Code,Name From @TmpTable Order By Name "

        FSetValue(2, "Positioning", FGDataType.DT_Selection_Single, FilterCodeType.DTNone, "Alphabatical", True)
        FRH_Single(2) = New DMHelpGrid.FrmHelpGrid(New DataView(CMain.FGetDatTable(StrSQL, AgL.GCn)), "", 150, 180, , , False)
        FRH_Single(2).FFormatColumn(0, , 0, , False)
        FRH_Single(2).FFormatColumn(1, "Name", 100, DataGridViewContentAlignment.MiddleLeft)

        StrSQL = "Declare @TmpTable Table(Code NVarChar(1),Name NVarChar(15)) Insert Into @TmpTable Values('Y','Yes') Insert Into @TmpTable Values('N','No') Insert Into @TmpTable Values('A','All') Select Code,Name From @TmpTable Order By Name "

        FSetValue(3, "Show Zero Value", FGDataType.DT_Selection_Single, FilterCodeType.DTNone, "All", True)
        FRH_Single(3) = New DMHelpGrid.FrmHelpGrid(New DataView(CMain.FGetDatTable(StrSQL, AgL.GCn)), "", 150, 180, , , False)
        FRH_Single(3).FFormatColumn(0, , 0, , False)
        FRH_Single(3).FFormatColumn(1, "Name", 100, DataGridViewContentAlignment.MiddleLeft)
    End Sub
    Private Sub FINI_Ageing()
        Dim StrSQL As String
        FSetValue(0, "Up To Date", FGDataType.DT_Date, FilterCodeType.DTNone, AgL.PubLoginDate)
        FSetValue(1, "Account Type ", FGDataType.DT_Selection_Single, FilterCodeType.DTString, , True)
        FRH_Single(1) = New DMHelpGrid.FrmHelpGrid(New DataView(CMain.FGetDatTable( _
                          "Select ag.nature as code,AG.nature as Name From acgroup AG group by ag.nature having ag.nature in('Customer','Supplier') Order By AG.Nature", _
                          AgL.GCn)), "", 250, 325, , , False)
        FRH_Single(1).FFormatColumn(0, "", 0, , False)
        FRH_Single(1).FFormatColumn(1, "Name", 250, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(2, "Site Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, AgL.PubSiteName & "|'" & AgL.PubSiteCode & "'")
        FRH_Multiple(2) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable( _
                          "Select 'o' As Tick,Sm.Code,Sm.Name From Sitemast Sm where code in (" & AgL.PubSiteList & ")   Order By Sm.Name", _
                          AgL.GCn)), "", 300, 360, , , False)
        FRH_Multiple(2).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(2).FFormatColumn(1, , 0, , False)
        FRH_Multiple(2).FFormatColumn(2, "Name", 240, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(3, "I Interval", FGDataType.DT_Numeric, FilterCodeType.DTNumeric, 5, False)
        FSetValue(4, "II Interval", FGDataType.DT_Numeric, FilterCodeType.DTNumeric, 10, False)
        FSetValue(5, "III Interval", FGDataType.DT_Numeric, FilterCodeType.DTNumeric, 15, False)
        FSetValue(6, "IV Interval", FGDataType.DT_Numeric, FilterCodeType.DTNumeric, 20, False)
        FSetValue(7, "V Interval", FGDataType.DT_Numeric, FilterCodeType.DTNumeric, 25, False)
        FSetValue(8, "VI Interval", FGDataType.DT_Numeric, FilterCodeType.DTNumeric, 30, False)

        StrSQL = " Declare @TmpTable Table(Code NVarChar(2),Name NVarChar(20)) Insert Into @TmpTable Values('A','All') Insert Into @TmpTable Values('HB','Having Balance') Select Code,Name From @TmpTable Order By Name "

        FSetValue(9, "Show Records", FGDataType.DT_Selection_Single, FilterCodeType.DTNone, "All")
        FRH_Single(9) = New DMHelpGrid.FrmHelpGrid(New DataView(CMain.FGetDatTable(StrSQL, AgL.GCn)), "", 150, 280, , , False)
        FRH_Single(9).FFormatColumn(0, , 0, , False)
        FRH_Single(9).FFormatColumn(1, "Name", 200, DataGridViewContentAlignment.MiddleLeft)

        StrSQL = " Declare @TmpTable Table(Code NVarChar(2),Name NVarChar(20)) Insert Into @TmpTable Values('AG','Account Group Wise') Insert Into @TmpTable Values('AC','Account Name Wise') Select Code,Name From @TmpTable Order By Name "

        FSetValue(10, "Report On Choice", FGDataType.DT_Selection_Single, FilterCodeType.DTNone, "Account Group Wise")
        FRH_Single(10) = New DMHelpGrid.FrmHelpGrid(New DataView(CMain.FGetDatTable(StrSQL, AgL.GCn)), "", 150, 300, , , False)
        FRH_Single(10).FFormatColumn(0, , 0, , False)
        FRH_Single(10).FFormatColumn(1, "Name", 220, DataGridViewContentAlignment.MiddleLeft)

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
        FRH_Multiple(1) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable("Select 'o' As Tick,Ag.GroupCode,Ag.GroupName FROM AcGroup  AG   " & _
                          "Where AG.GroupUnder In (SELECT AGP.GroupCode FROM AcGroupPath AGP WHERE AGP.GroupUnder='" & StrReportForCode & "') Or AG.GroupUnder='" & StrReportForCode & "' " & _
                          "Order By AG.GroupName ", AgL.GCn)), "", 600, 460, , , False)
        FRH_Multiple(1).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(1).FFormatColumn(1, , 0, , False)
        FRH_Multiple(1).FFormatColumn(2, "Name", 340, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(2, "Account Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, "All")
        FRH_Multiple(2) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable("Select 'o' As Tick,SG.SubCode,SG.Name,SG.ManualCode,IsNull(CT.CityName,''),AG.GroupName From SubGroup  SG Left Join  " & _
                          "AcGroup AG On AG.GroupCode=SG.GroupCode " & _
                          "Left Join City CT On SG.CityCode=CT.CityCode " & _
                          "Where SG.GroupCode In (SELECT AGP.GroupCode FROM AcGroupPath AGP WHERE  " & _
                          "AGP.GroupUnder='" & StrReportForCode & "') Or SG.GroupCode='" & StrReportForCode & "' and " & AgL.PubSiteListCharIndex & "" & _
                          "Order By SG.Name", AgL.GCn)), "", 600, 960, , , False)
        FRH_Multiple(2).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(2).FFormatColumn(1, , 0, , False)
        FRH_Multiple(2).FFormatColumn(2, "Name", 440, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple(2).FFormatColumn(3, "Code", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple(2).FFormatColumn(4, "City", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple(2).FFormatColumn(5, "Group Name", 200, DataGridViewContentAlignment.MiddleLeft)

        FSetValue(3, "Interval", FGDataType.DT_Numeric, FilterCodeType.DTNumeric, 180, False)

        FSetValue(4, "Site Name", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, AgL.PubSiteName & "|'" & AgL.PubSiteCode & "'")
        FRH_Multiple(4) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable( _
                          "Select 'o' As Tick,Sm.Code,Sm.Name From Sitemast Sm where code in (" & AgL.PubSiteList & ")   Order By Sm.Name", _
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
        STRDATE = AgL.ConvertDate(FGMain(GFilter, 0).Value.ToString)
        StrCondition1 = " Where (LG.V_Date <= " & AgL.ConvertDate(FGMain(GFilter, 0).Value.ToString) & " And IsNull(LG." & StrAmt1 & ",0)>0) And (SG.GroupCode In (SELECT AGP.GroupCode FROM AcGroupPath AGP WHERE AGP.GroupUnder='" & StrCnd & "') Or SG.GroupCode='" & StrCnd & "') "
        StrCondition2 = " Where (LG.V_Date <= " & AgL.ConvertDate(FGMain(GFilter, 0).Value.ToString) & ") And IsNull(LG." & StrAmt2 & ",0)>0 And IsNull(LG." & StrAmt2 & ",0)-ISNULL(T.AMOUNT,0)<>0 And (SG.GroupCode In (SELECT AGP.GroupCode FROM AcGroupPath AGP WHERE AGP.GroupUnder='" & StrCnd & "') Or SG.GroupCode='" & StrCnd & "') "

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
        StrSQLQuery = StrSQLQuery + "Max(SG.Add1)As Add1,Max(SG.Add2)As Add2,Max(C.CityName)As CityName,Max(CT.Name) as Country,MAx(St.name) As SiteName,max(Ag.GroupName) as AcGroupName, "
        StrSQLQuery = StrSQLQuery + "(CASE WHEN DateDiff(Day,Max(LG.V_Date), " & STRDATE & "  )>= 0 AND  DateDiff(Day,Max(LG.V_Date)," & STRDATE & " )<=" & D1 & " THEN  Max(LG.AmtDr)-IsNull(Sum(LA.Amount),0) ELSE 0 end) AS AmtDay1, "
        StrSQLQuery = StrSQLQuery + "(CASE WHEN DateDiff(Day,Max(LG.V_Date)," & STRDATE & " )>" & D1 & " THEN  Max(LG.AmtDr)-IsNull(Sum(LA.Amount),0) ELSE 0 end) AS AmtDay2," & D1 & " As Days  "
        StrSQLQuery = StrSQLQuery + "From Ledger LG Left Join SubGroup SG On LG.Subcode=SG.SubCode Left Join "
        StrSQLQuery = StrSQLQuery + "City C on SG.CityCode=C.CityCode Left Join Country CT on SG.CountryCode=CT.Code LEFT JOIN AcGroup AG ON SG.GroupCode =AG.GroupCode  "
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

        StrCondition1 = " Where (LG.V_Date Between " & AgL.ConvertDate(FGMain(GFilter, 0).Value.ToString) & " And " & AgL.ConvertDate(FGMain(GFilter, 1).Value.ToString) & ") "
        StrConditionOP = " Where LG.V_Date < " & AgL.ConvertDate(FGMain(GFilter, 0).Value.ToString) & " "

        If Trim(FGMain(GFilterCode, 2).Value) <> "" Then StrCondition1 = StrCondition1 & " And (IsNull(SG.GroupCode,'') In (Select IsNull(AGP.GroupCode,'') From AcGroupPath AGP Where AGP.GroupUnder In (" & FGMain(GFilterCode, 2).Value & ")) Or SG.GroupCode In (" & FGMain(GFilterCode, 2).Value & ")) "
        If Trim(FGMain(GFilterCode, 2).Value) <> "" Then StrConditionOP = StrConditionOP & " And (IsNull(SG.GroupCode,'') In (Select IsNull(AGP.GroupCode,'') From AcGroupPath AGP Where AGP.GroupUnder In (" & FGMain(GFilterCode, 2).Value & ")) Or SG.GroupCode In (" & FGMain(GFilterCode, 2).Value & ")) "
        If Trim(FGMain(GFilterCode, 3).Value) <> "" Then StrCondition1 = StrCondition1 & " And LG.SubCode In (" & FGMain(GFilterCode, 3).Value & ")"
        If Trim(FGMain(GFilterCode, 3).Value) <> "" Then StrConditionOP = StrConditionOP & " And LG.SubCode In (" & FGMain(GFilterCode, 3).Value & ")"
        If Trim(FGMain(GFilterCode, 4).Value) <> "" Then StrCondition1 = StrCondition1 & " And LG.V_Type In (" & FGMain(GFilterCode, 4).Value & ")"
        If Trim(FGMain(GFilterCode, 4).Value) <> "" Then StrConditionOP = StrConditionOP & " And LG.V_Type In (" & FGMain(GFilterCode, 4).Value & ")"

        StrConditionsite = ""
        If Trim(FGMain(GFilterCode, 5).Value) <> "" Then
            StrConditionsite = " and LG.site_Code In (" & FGMain(GFilterCode, 5).Value & ") "
        Else
            StrConditionsite = " and LG.site_Code In  (" & AgL.PubSiteList & ") "
        End If

        '========== For Detail Section =======
        StrSQLQuery = "Select	LG.V_Type,Convert(Varchar,LG.V_No) As V_No,LG.V_Date,LG.V_Prefix,SG.Name  As PName,LG.SubCode,LG.Narration, "
        StrSQLQuery = StrSQLQuery + "LG.AmtDr,LG.AmtCr,1 As SNo,SM.Name as Division,LG.ContraText As ContraName,LG.Chq_No,LG.Chq_Date,"
        StrSQLQuery = StrSQLQuery + "isnull(C.CityName,'') as PCity,IsNull(LG.Site_Code,'') As Site_Code "
        StrSQLQuery = StrSQLQuery + "From Ledger LG Left Join SubGroup SG On LG.Subcode=SG.SubCode "
        StrSQLQuery = StrSQLQuery + "Left Join Sitemast SM On LG.Site_Code=SM.Code "
        StrSQLQuery = StrSQLQuery + "Left Join City C On C.CityCode=SG.CityCode "

        StrSQLQuery = StrSQLQuery + StrCondition1 + StrConditionsite
        StrSQLQuery = StrSQLQuery + "Union All "

        '======= For Opening Balance =========
        StrSQLQuery = StrSQLQuery + "Select	Null As V_Type,Null As V_No,Null As V_Date,Null As V_Prefix, "
        StrSQLQuery = StrSQLQuery + "max(SG.Name)   As PName,LG.SubCode,'OPENING BALANCE' As Narration, "
        StrSQLQuery = StrSQLQuery + "(Case When (IsNull(Sum(LG.AmtDr),0)-IsNull(Sum(LG.AmtCr),0))>0 Then  "
        StrSQLQuery = StrSQLQuery + "(IsNull(Sum(LG.AmtDr),0)-IsNull(Sum(LG.AmtCr),0)) Else 0 End) As AmtDr, "
        StrSQLQuery = StrSQLQuery + "(Case When (IsNull(Sum(LG.AmtCr),0)-IsNull(Sum(LG.AmtDr),0))>0 Then "
        StrSQLQuery = StrSQLQuery + "(IsNull(Sum(LG.AmtCr),0)-IsNull(Sum(LG.AmtDr),0)) Else 0 End) As AmtCr,"
        StrSQLQuery = StrSQLQuery + "0 As SNo,max(SM.name) as Division,Null As ContraName,Null As Chq_No,Null As Chq_Date,"
        StrSQLQuery = StrSQLQuery + "isnull(max(C.CityName),'') as PCity,Null As Site_Code "
        StrSQLQuery = StrSQLQuery + "From Ledger LG Left Join SubGroup SG On LG.Subcode=SG.SubCode "
        StrSQLQuery = StrSQLQuery + "Left Join Sitemast SM On LG.Site_Code=SM.Code "
        StrSQLQuery = StrSQLQuery + "Left Join City C On C.CityCode=SG.CityCode "
        StrSQLQuery = StrSQLQuery + StrConditionOP + StrConditionsite

        StrSQLQuery = StrSQLQuery + "Group By LG.SubCode "
        StrSQLQuery = StrSQLQuery + "Order By PName,V_Date,V_Type,V_No,SNo "

        DTTemp = CMain.FGetDatTable(StrSQLQuery, AgL.GCn)

        If Not DTTemp.Rows.Count > 0 Then MsgBox("No Records Found to Print.") : Exit Sub

        FLoadMainReport("Ledger", DTTemp)

        For I = 0 To RptMain.DataDefinition.FormulaFields.Count - 1
            Select Case (UCase(RptMain.DataDefinition.FormulaFields.Item(I).Name))
                Case UCase("FrmIndexNeeded")
                    RptMain.DataDefinition.FormulaFields.Item(I).Text = "'" & IIf(Trim(FGMain(GFilterCode, 6).Value) = "", "N", Trim(FGMain(GFilterCode, 6).Value)) & "'"
                Case UCase("Contraneeded")
                    RptMain.DataDefinition.FormulaFields.Item(I).Text = "'" & Trim(FGMain(GFilterCode, 7).Value) & "'"
            End Select
        Next

        CMain.FormulaSet(RptMain, Me.Text, FGMain)
        CMain.FShowReport(RptMain, Me.MdiParent, Me.Text)
    End Sub
    Private Sub FJournal()
        Dim StrCondition1 As String
        Dim DTTemp As DataTable

        If Not FIsValid(0) Then Exit Sub

        StrCondition1 = " Where LG.V_Date Between " & AgL.ConvertDate(FGMain(GFilter, 0).Value.ToString) & " and  " & AgL.ConvertDate(FGMain(GFilter, 1).Value.ToString) & " And VType.Category='JV' "
        If Trim(FGMain(GFilterCode, 2).Value) <> "" Then StrCondition1 = StrCondition1 & " And LG.V_type In (" & FGMain(GFilterCode, 2).Value & ")"

        If Trim(FGMain(GFilterCode, 3).Value) <> "" Then
            StrCondition1 = StrCondition1 & "  And LG.Site_Code IN (" & FGMain(GFilterCode, 3).Value & ") "
        Else
            StrCondition1 = StrCondition1 & " And LG.Site_Code IN (" & AgL.PubSiteList & ") "
        End If

        StrSQLQuery = "Select LG.V_date,LG.Amtcr,LG.AmtDr,LG.V_type,Convert(Varchar,LG.V_No) As V_no,LG.V_prefix as V_add,LG.Chq_No, "
        StrSQLQuery = StrSQLQuery + "LG.Chq_Date,LG.Narration As narr,LG.V_Sno,LedgerM.Narration As mnarration,LG.Docid,SG.Name As Name,St.name As SiteName ,LG.Site_Code "
        StrSQLQuery = StrSQLQuery + "FROM Ledger LG LEFT  JOIN  LedgerM ON LG.DocId = dbo.LedgerM.DocId "
        StrSQLQuery = StrSQLQuery + "Left Join Subgroup SG On SG.Subcode=LG.Subcode "
        StrSQLQuery = StrSQLQuery + "Left join Voucher_type VType on Vtype.V_Type=LG.V_Type "
        StrSQLQuery = StrSQLQuery + "LEFT JOIN SiteMast ST ON LG.Site_Code =St.code "
        StrSQLQuery = StrSQLQuery + StrCondition1
        StrSQLQuery = StrSQLQuery + "Order By LG.V_DATE,LG.V_TYPE,LG.V_No,LG.V_SNO"

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

        StrCondition1 = " Where LG.V_Date Between " & AgL.ConvertDate(FGMain(GFilter, 0).Value.ToString) & " and  " & AgL.ConvertDate(FGMain(GFilter, 1).Value.ToString) & " "

        If Trim(FGMain(GFilterCode, 2).Value) <> "" Then StrCondition1 = StrCondition1 & " And LG.V_type In (" & FGMain(GFilterCode, 2).Value & ")"

        If Trim(FGMain(GFilterCode, 3).Value) <> "" Then
            StrCondition1 = StrCondition1 & " And  LG.Site_Code  IN (" & FGMain(GFilterCode, 3).Value & ") "
        Else
            StrCondition1 = StrCondition1 & " And  LG.Site_Code  IN (" & AgL.PubSiteList & ") "
        End If

        StrSQLQuery = "Select LG.V_date,LG.Amtcr,LG.AmtDr,LG.V_type,Convert(Varchar,LG.V_No) As V_no,LG.V_prefix as V_add,LG.Chq_No, "
        StrSQLQuery = StrSQLQuery + "LG.Chq_Date,LG.Narration As narr,LG.V_Sno,LedgerM.Narration As mnarration,LG.Docid,SG.Name As Name,St.name As SiteName,LG.Site_Code "
        StrSQLQuery = StrSQLQuery + "FROM Ledger LG LEFT  JOIN  LedgerM ON LG.DocId = dbo.LedgerM.DocId "
        StrSQLQuery = StrSQLQuery + " Left Join Subgroup SG On SG.Subcode=LG.Subcode "
        StrSQLQuery = StrSQLQuery + "Left join Voucher_type VType on Vtype.V_Type=LG.V_Type "
        StrSQLQuery = StrSQLQuery + "LEFT JOIN SiteMast ST ON LG.Site_Code =St.code"
        StrSQLQuery = StrSQLQuery + StrCondition1
        StrSQLQuery = StrSQLQuery + "Order By LG.V_DATE,LG.V_TYPE,LG.V_No,LG.V_SNO"

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

        StrCondition1 = " Where LG.V_Date <= " & AgL.ConvertDate(FGMain(GFilter, 0).Value.ToString) & " "
        If Trim(FGMain(GFilterCode, 1).Value) <> "" Then
            StrCondition1 += " And LG.Site_Code In (" & FGMain(GFilterCode, 1).Value & ") "
        Else
            StrCondition1 += " And LG.Site_Code In  (" & AgL.PubSiteList & ") "
        End If

        '========== For Detail Section =======
        StrSQLQuery = "Select	(Case IsNull(AG1.GroupCode,'') When '' Then IsNull(AG.GroupCode,'') "
        StrSQLQuery = StrSQLQuery + "Else IsNull(AG1.GroupCode,'') End)  As GroupCode, "
        StrSQLQuery = StrSQLQuery + "Max(Case IsNull(AG1.GroupName,'') When '' Then IsNull(AG.GroupName,'') "
        StrSQLQuery = StrSQLQuery + "Else IsNull(AG1.GroupName,'') End)  As GName, "
        StrSQLQuery = StrSQLQuery + "(Case When (IsNull(Sum(LG.AmtDr),0)-IsNull(Sum(LG.AmtCr),0))>0 Then  "
        StrSQLQuery = StrSQLQuery + "(IsNull(Sum(LG.AmtDr),0)-IsNull(Sum(LG.AmtCr),0)) Else 0 End) As AmtDr, "
        StrSQLQuery = StrSQLQuery + "(Case When (IsNull(Sum(LG.AmtCr),0)-IsNull(Sum(LG.AmtDr),0))>0 Then "
        StrSQLQuery = StrSQLQuery + "(IsNull(Sum(LG.AmtCr),0)-IsNull(Sum(LG.AmtDr),0)) Else 0 End) As AmtCr "
        StrSQLQuery = StrSQLQuery + "From Ledger LG Left Join SubGroup SG On LG.SubCode=SG.SubCode  Left Join "
        StrSQLQuery = StrSQLQuery + "AcGroup AG On AG.GroupCode=SG.GroupCode Left Join "
        StrSQLQuery = StrSQLQuery + "AcGroupPath AGP On AGP.GroupCode=AG.GroupCode And AGP.SNo=1 Left Join "
        StrSQLQuery = StrSQLQuery + "AcGroup AG1 On AG1.GroupCode=AGP.GroupUnder "
        StrSQLQuery = StrSQLQuery + StrCondition1

        StrSQLQuery = StrSQLQuery + "Group By (Case IsNull(AG1.GroupCode,'') When '' Then IsNull(AG.GroupCode,'') "
        StrSQLQuery = StrSQLQuery + "Else IsNull(AG1.GroupCode,'') End) "
        StrSQLQuery = StrSQLQuery + "Having (IsNull(Sum(LG.AmtDr),0)-IsNull(Sum(LG.AmtCr),0)) <> 0 "
        StrSQLQuery = StrSQLQuery + "Order By Max(Case IsNull(AG1.GroupName,'') When '' Then IsNull(AG.GroupName,'') "
        StrSQLQuery = StrSQLQuery + "Else IsNull(AG1.GroupName,'') End) "


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

        If UCase(Trim(FGMain(GFilterCode, 2).Value)) = "M" Then
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

        StrCondition1 = " And LG.V_Date <= " & AgL.ConvertDate(FGMain(GFilter, 0).Value.ToString) & " "
        If Trim(FGMain(GFilterCode, 1).Value) <> "" Then
            StrCondition1 += " And LG.Site_Code In (" & FGMain(GFilterCode, 1).Value & ") "
        Else
            StrCondition1 += " And LG.Site_Code In  (" & AgL.PubSiteList & ") "
        End If

        If FGMain(GFilterCode, 3).Value = "N" Then
            StrConditionZeroBal = "Having (IsNull(Sum(Tbl.AmtDr),0)-IsNull(Sum(Tbl.AmtCr),0)) <> 0  "
        ElseIf FGMain(GFilterCode, 3).Value = "Y" Then
            StrConditionZeroBal = "Having (IsNull(Sum(Tbl.AmtDr),0)-IsNull(Sum(Tbl.AmtCr),0)) = 0  "
        Else
            StrConditionZeroBal = ""
        End If

        '================================= Upper Select Query =====================================
        StrSQLQuery = "Select	Space(IsNull(Max(Level),1)-1) + IsNull(Max(GroupName),'') As GroupName,GroupCode, "
        StrSQLQuery += "Space(IsNull(Max(Level),1)) + IsNull(Max(AcName),'') As AcName,AcCode, "
        StrSQLQuery += "(Case When IsNull(Sum(AmtDr),0)-IsNull(Sum(AmtCr),0)>0 Then "
        StrSQLQuery += "IsNull(Sum(AmtDr),0)-IsNull(Sum(AmtCr),0) Else 0 End) As AmtDr, "
        StrSQLQuery += "(Case When IsNull(Sum(AmtCr),0)-IsNull(Sum(AmtDr),0)>0 Then "
        StrSQLQuery += "IsNull(Sum(AmtCr),0)-IsNull(Sum(AmtDr),0) Else 0 End) As AmtCr, "
        StrSQLQuery += "Max(V_SNo) As V_SNo,Max(Level) As Level "
        StrSQLQuery += "From ("
        '==========================================================================================
        '==========================================================================================

        '===================== Main Ledger Fetching For Expandable Groups =========================
        StrSQLQuery += "Select 	AG.GroupName,AG.GroupCode, "
        StrSQLQuery += "(SG.Name + ' - ' + IsNull(CT.CityName,'')) As AcName,LG.SubCode AcCode,"
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
        StrSQLQuery += "Where IsNull(AGP.GroupCode,'')<>'' "
        StrSQLQuery += ") As Tmp "
        StrSQLQuery += "Group By GroupCode,MainGroup"
        StrSQLQuery += ") POS On POS.GroupCode=SG.GroupCode Left Join "
        StrSQLQuery += "AcGroup AG On POS.MainGroup=AG.GroupCode "
        StrSQLQuery += "Where  IsNull(POS.GroupCode,'')<>'' " & StrCondition1
        '==========================================================================================
        '==========================================================================================

        '================================= Lower Select Query =====================================
        StrSQLQuery += ") As Tbl "
        StrSQLQuery += "Group By GroupCode,AcCode "
        StrSQLQuery += StrConditionZeroBal
        StrSQLQuery += "Order By V_SNo,Max(GroupName),Max(AcName) "

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

        StrCondition1 = " Where LG.V_Date <= " & AgL.ConvertDate(FGMain(GFilter, 0).Value.ToString) & " "
        If Trim(FGMain(GFilterCode, 1).Value) <> "" Then
            StrCondition1 += " And LG.Site_Code In (" & FGMain(GFilterCode, 1).Value & ") "
        Else
            StrCondition1 += " And LG.Site_Code In  (" & AgL.PubSiteList & ") "
        End If

        If FGMain(GFilterCode, 3).Value = "N" Then
            StrConditionZeroBal = "Having (IsNull(Sum(LG.AmtDr),0)-IsNull(Sum(LG.AmtCr),0)) <> 0  "
        ElseIf FGMain(GFilterCode, 3).Value = "Y" Then
            StrConditionZeroBal = "Having (IsNull(Sum(LG.AmtDr),0)-IsNull(Sum(LG.AmtCr),0)) = 0  "
        Else
            StrConditionZeroBal = ""
        End If

        StrSQLQuery = "Select  IsNull((Select Max(AG1.GroupName) "
        StrSQLQuery += "From AcGroupPath AGP Left Join "
        StrSQLQuery += "AcGroup AG1 On AG1.GroupCode=AGP.GroupUnder And AGP.SNo=" & 1 & " "
        StrSQLQuery += "Where AGP.GroupCode=Max(SG.GroupCode)), "
        StrSQLQuery += "(Case When (Select IsNull(Max(SNo),0) From AcGroupPath AGP1 "
        StrSQLQuery += "Where AGP1.GroupCode=Max(SG.GroupCode))= " & 0 & " "
        StrSQLQuery += "Then Max(AG.GroupName) Else '' End)) As " & StrFieldName + Trim(1) & " , "

        DTTemp = CMain.FGetDatTable("Select IsNull(Max(SNo),0) From AcGroupPath", AgL.GCn)
        If DTTemp.Rows(0).Item(0) > (IntMaxHirarchy - 1) Then MsgBox("There Can Be Only " & IntMaxHirarchy - 1 & " A/c Group Levels. Levels Are Exceding.") : Exit Sub
        For I = 2 To DTTemp.Rows(0).Item(0) + 1
            StrFieldPrefix += StrSpace
            StrSQLQuery += "IsNull((Select '" & StrFieldPrefix & "' + Max(AG1.GroupName) "
            StrSQLQuery += "From AcGroupPath AGP Left Join "
            StrSQLQuery += "AcGroup AG1 On AG1.GroupCode=AGP.GroupUnder And AGP.SNo=" & I & " "
            StrSQLQuery += "Where AGP.GroupCode=Max(SG.GroupCode)), "
            StrSQLQuery += "(Case When (Select IsNull(Max(SNo),0) From AcGroupPath AGP1 "
            StrSQLQuery += "Where AGP1.GroupCode=Max(SG.GroupCode))= " & I - 1 & " "
            StrSQLQuery += "Then '" & StrFieldPrefix & "' + Max(AG.GroupName) Else '' End)) As "
            StrSQLQuery += StrFieldName + Trim(I) & " , "
        Next

        For I = DTTemp.Rows(0).Item(0) + 2 To IntMaxHirarchy
            StrSQLQuery += "' ' As " & StrFieldName + Trim(I) & " , "
        Next

        StrSQLQuery += "(SG.Name + ' - ' + IsNull(CT.CityName,'')) As Name, "
        StrSQLQuery += "(Case When IsNull(Sum(LG.AmtDr),0)-IsNull(Sum(LG.AmtCr),0)>0 Then "
        StrSQLQuery += "IsNull(Sum(LG.AmtDr),0)-IsNull(Sum(LG.AmtCr),0) Else 0 End) As AmtDr, "
        StrSQLQuery += "(Case When IsNull(Sum(LG.AmtCr),0)-IsNull(Sum(LG.AmtDr),0)>0 Then "
        StrSQLQuery += "IsNull(Sum(LG.AmtCr),0)-IsNull(Sum(LG.AmtDr),0) Else 0 End) As AmtCr "
        StrSQLQuery += "From "
        StrSQLQuery += "Ledger LG Left Join "
        StrSQLQuery += "SubGroup SG On LG.SubCode=SG.SubCode Left Join "
        StrSQLQuery += "City CT On CT.CityCode=SG.CityCode Left Join "
        StrSQLQuery += "AcGroup AG On AG.GroupCode=SG.GroupCode "
        StrSQLQuery += StrCondition1
        StrSQLQuery += "Group By SG.Name + ' - ' + IsNull(CT.CityName,'') "
        StrSQLQuery += StrConditionZeroBal
        StrSQLQuery += "Order By Max(SG.Name + ' - ' + IsNull(CT.CityName,''))"


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

        StrCondition1 = "Where LG.V_Date<=" & AgL.ConvertDate(FGMain(GFilter, 0).Value.ToString) & " "

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

        StrSQLQuery = "Select  IsNull((Select Max(AG1.GroupName) "
        StrSQLQuery += "From AcGroupPath AGP Left Join "
        StrSQLQuery += "AcGroup AG1 On AG1.GroupCode=AGP.GroupUnder And AGP.SNo=" & 1 & " "
        StrSQLQuery += "Where AGP.GroupCode=Max(SG.GroupCode)), "
        StrSQLQuery += "(Case When (Select IsNull(Max(SNo),0) From AcGroupPath AGP1 "
        StrSQLQuery += "Where AGP1.GroupCode=Max(SG.GroupCode))= " & 0 & " "
        StrSQLQuery += "Then Max(AG.GroupName) Else '' End)) As " & StrFieldName + Trim(1) & " , "

        DTTemp = CMain.FGetDatTable("Select IsNull(Max(SNo),0) From AcGroupPath", AgL.GCn)
        If DTTemp.Rows(0).Item(0) > (IntMaxHirarchy - 1) Then MsgBox("There Can Be Only " & IntMaxHirarchy - 1 & " A/c Group Levels. Levels Are Exceding.") : Exit Sub
        For I = 2 To DTTemp.Rows(0).Item(0) + 1
            StrFieldPrefix += StrSpace
            StrSQLQuery += "IsNull((Select '" & StrFieldPrefix & "' + Max(AG1.GroupName) "
            StrSQLQuery += "From AcGroupPath AGP Left Join "
            StrSQLQuery += "AcGroup AG1 On AG1.GroupCode=AGP.GroupUnder And AGP.SNo=" & I & " "
            StrSQLQuery += "Where AGP.GroupCode=Max(SG.GroupCode)), "
            StrSQLQuery += "(Case When (Select IsNull(Max(SNo),0) From AcGroupPath AGP1 "
            StrSQLQuery += "Where AGP1.GroupCode=Max(SG.GroupCode))= " & I - 1 & " "
            StrSQLQuery += "Then '" & StrFieldPrefix & "' + Max(AG.GroupName) Else '' End)) As "
            StrSQLQuery += StrFieldName + Trim(I) & " , "
        Next

        For I = DTTemp.Rows(0).Item(0) + 2 To IntMaxHirarchy
            StrSQLQuery += "' ' As " & StrFieldName + Trim(I) & " , "
        Next

        StrSQLQuery += "SG.Name, "
        StrSQLQuery += "(Case When IsNull(Sum(LG.AmtDr),0)-IsNull(Sum(LG.AmtCr),0)>0 Then "
        StrSQLQuery += "IsNull(Sum(LG.AmtDr),0)-IsNull(Sum(LG.AmtCr),0) Else 0 End) As AmtDr, "
        StrSQLQuery += "(Case When IsNull(Sum(LG.AmtCr),0)-IsNull(Sum(LG.AmtDr),0)>0 Then "
        StrSQLQuery += "IsNull(Sum(LG.AmtCr),0)-IsNull(Sum(LG.AmtDr),0) Else 0 End) As AmtCr "
        StrSQLQuery += "From "
        StrSQLQuery += "Ledger LG Left Join "
        StrSQLQuery += "SubGroup SG On LG.SubCode=SG.SubCode Left Join "
        StrSQLQuery += "AcGroup AG On AG.GroupCode=SG.GroupCode "
        StrSQLQuery += StrCondition1
        StrSQLQuery += "Group By SG.Name "
        StrSQLQuery += "Having (IsNull(Sum(LG.AmtDr),0)-IsNull(Sum(LG.AmtCr),0)) <> 0 "

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

        StrCondition = " Where (LG.V_Date Between " & AgL.ConvertDate(FGMain(GFilter, 0).Value.ToString) & " And " & AgL.ConvertDate(FGMain(GFilter, 1).Value.ToString) & ") "
        StrConditionSubQuery = " Where (LGS.V_Date Between " & AgL.ConvertDate(FGMain(GFilter, 0).Value.ToString) & " And " & AgL.ConvertDate(FGMain(GFilter, 1).Value.ToString) & ") "
        '        StrConditionDayOP = " Where (LG.V_Date Between DateAdd(dd,1," & Agl.ConvertDate(FGMain(GFilter, 0).Value.ToString) & ") And " & Agl.ConvertDate(FGMain(GFilter, 1).Value.ToString) & ") "
        StrConditionDayOP = " Where (LG.V_Date Between " & AgL.ConvertDate(FGMain(GFilter, 0).Value.ToString) & " And " & AgL.ConvertDate(FGMain(GFilter, 1).Value.ToString) & ") "
        StrConditionOP = " Where LG.V_Date < " & AgL.ConvertDate(FGMain(GFilter, 0).Value.ToString) & " "

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
        StrSQLQuery += "Select	V_Date,(IsNull(Sum(LG.AmtDr),0)-IsNull(Sum(LG.AmtCr),0)) As DayDR, "
        StrSQLQuery += "Null As DayDr_OPN,0 As Opening "
        StrSQLQuery += "From Ledger LG "
        StrSQLQuery += StrConditionDayOP
        StrSQLQuery += "Group By V_Date  "
        StrSQLQuery += "Union All "
        StrSQLQuery += "Select	" & AgL.ConvertDate(FGMain(GFilter, 0).Value.ToString) & " As V_Date,Null As DayDR, "
        StrSQLQuery += "(IsNull(Sum(LG.AmtDr),0)-IsNull(Sum(LG.AmtCr),0)) As DayDR_OPN,0 As Opening "
        StrSQLQuery += "From Ledger LG "
        StrSQLQuery += StrConditionOP
        StrSQLQuery += ") As Tmp "
        StrSQLQuery += "Group By Tmp.V_Date   "
        StrSQLQuery += "ORDER BY V_Date   "

        StrSQLQuery += "Update	@TmpTable Set "
        StrSQLQuery += "@RNTDr = @RNTDr + IsNull(DayDR,0) + IsNull(DayDr_OPN,0), "
        StrSQLQuery += "Opening = @RNTDr - IsNull(DayDR,0)	"

        StrSQLQuery += "Select	LG.DocId,LG.SubCode,Convert(Varchar,LG.RecID) as RecID,LG.V_Date,LG.V_Type,LG.Site_Code,LG.AmtDr,LG.AmtCr, "
        StrSQLQuery += "LG.V_SNo,SG.Name As AcName,LG.Chq_Date,LG.Chq_No,1 As SNo, "
        StrSQLQuery += "Null As Main_AmtDr,Null As Main_AmtCr,LG.Narration,IsNull(VT.SerialNo,0) As SerialNo "
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
        StrSQLQuery += "Where(IsNull(TT.Opening, 0) <> 0) "
        StrSQLQuery += "Union All "

        StrSQLQuery += "Select LGS.DocId,Null As SubCode,Convert(Varchar,LGS.RecID) as RecID,LGS.V_Date,LGS.V_Type, "
        StrSQLQuery += "Null As Site_Code,0 As AmtDr,0 As AmtCr,LGS.V_SNo, "
        StrSQLQuery += "Null As AcName,Null As Chq_Date,Null As Chq_No,2 As SNo, "
        StrSQLQuery += "LGS.AmtDr As Main_AmtDr,LGS.AmtCr As Main_AmtCr,'' As Narration,IsNull(VT.SerialNo,0) As SerialNo "
        StrSQLQuery += "From Ledger LGS "
        StrSQLQuery += "Left Join Voucher_Type VT On VT.V_Type=LGS.V_Type "
        StrSQLQuery += StrConditionSubQuery
        'StrSQLQuery += "Order By LG.V_Date,LG.V_Type,LG.V_No,LG.DocId,SNo "
        StrSQLQuery += "Order By LG.V_Date,SerialNo,RecID,LG.DocId,SNo "

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
        StrCondition1 = " Where (L.V_Date Between " & AgL.ConvertDate(FGMain(GFilter, 0).Value.ToString) & " And " & AgL.ConvertDate(FGMain(GFilter, 1).Value.ToString) & ") "
        If Trim(FGMain(GFilterCode, 2).Value) <> "" Then
            StrCondition1 = StrCondition1 & " And  L.Site_Code IN (" & FGMain(GFilterCode, 2).Value & ") "
            StrConditionOP = " And  L.Site_Code IN (" & FGMain(GFilterCode, 2).Value & ") "
        Else
            StrCondition1 = StrCondition1 & " And  L.Site_Code IN (" & AgL.PubSiteList & ") "
            StrConditionOP = " And  L.Site_Code IN (" & AgL.PubSiteList & ") "
        End If
        If Trim(FGMain(GFilterCode, 4).Value) <> "" Then
            Pagewise = Trim(FGMain(GFilterCode, 4).Value)
        End If
        If Trim(FGMain(GFilterCode, 5).Value) <> "" Then
            Withnarration = Trim(FGMain(GFilterCode, 5).Value)
        End If
        SQL = "Select (IsNull(Sum(AmtCr),0)-IsNull(Sum(AmtDr),0)) As OP From Ledger L "
        SQL = SQL + "Left Join SubGroup SG On L.SubCode=SG.SubCode Where SG.Nature='Cash' "
        SQL = SQL + "And V_Date<" & AgL.ConvertDate(FGMain(GFilter, 0).Value.ToString) & " "
        SQL = SQL + "And " & " L.subcode IN ('" & FGMain(GFilterCode, 3).Value & "') " & StrConditionOP

        DTTemp = CMain.FGetDatTable(SQL, AgL.GCn)
        If DTTemp.Rows.Count > 0 Then DblOpening = AgL.VNull(DTTemp.Rows(0).Item("OP"))
        SQL = "DECLARE @tmptb TABLE(code datetime) "
        SQL += "DECLARE @tempfromdt AS DATETIME "
        SQL += "DECLARE @temptodt AS DATETIME "
        SQL += " SET @tempfromdt=" & AgL.ConvertDate(FGMain(GFilter, 0).Value.ToString)
        SQL += " SET @temptodt=" & AgL.ConvertDate(FGMain(GFilter, 1).Value.ToString)
        SQL += " WHILE @tempfromdt<=@temptodt "
        SQL += " BEGIN "
        SQL += " INSERT INTO @tmptb VALUES (@tempfromdt) "
        SQL += " SET @tempfromdt=@tempfromdt+1 End "
        SQL += "Select isnull(DocID,'') As DocId,Convert(Varchar,isnull(V_No,'')) As V_no,isnull(T.Code,'') As V_date,isnull(Particular,'') As Particular,isnull(AmtDr,0) As AmtDr,isnull(AmtCr,0) As AmtCr,isnull(V_Type,'') As V_Type,isnull(NCat,'') As NCat,isnull(Nature,'') As nature,isnull(Narration,'') as Narration "
        SQL = SQL + " From @tmptb T left join "
        SQL = SQL + " (Select L.DocID,Convert(Varchar,L.RecID) As V_No,L.V_Date ,SG.[Name] As Particular,L.AmtDr , L.AmtCr,L.V_Type ,VT.NCat,SG.Nature,Isnull(L.Narration,'') as Narration "
        SQL = SQL + " From Ledger L "
        SQL = SQL + " Left Join SubGroup SG On L.SubCode=SG.SubCode "
        SQL = SQL + " Left Join Voucher_Type VT On VT.V_Type=L.V_Type "
        SQL = SQL + " Where L.subcode<>'" & FGMain(GFilterCode, 3).Value & "' "
        SQL = SQL + " And (IsNull(L.TDSCategory,'')='' Or (IsNull(L.TDSCategory,'')<>'' And IsNull(L.System_Generated,'N')='N'))"
        SQL = SQL + " And L.DocID In ( "
        SQL = SQL + " Select L.DocID From Ledger L "
        SQL = SQL + " Left Join SubGroup SG On L.SubCode=SG.SubCode "
        SQL = SQL + " Left Join Voucher_Type VT On VT.V_Type=L.V_Type "
        SQL = SQL + StrCondition1 & " And VT.Category IN('RCT','PMT') And SG.Nature='Cash'"
        SQL = SQL + " And L.subcode IN ('" & FGMain(GFilterCode, 3).Value & "'))"
        SQL = SQL + " Union All "
        SQL = SQL + "Select L.DocID,Convert(Varchar,L.RecID) As V_No,L.V_Date ,SG.[Name] As Particular,L.AmtCr As AmtCr,L.AmtDr As AmtDr,L.V_Type ,VT.NCat,SG.Nature,Isnull(L.Narration,'') as Narration "
        SQL = SQL + "From Ledger L "
        SQL = SQL + "Left Join SubGroup SG On L.ContraSub=SG.SubCode "
        SQL = SQL + "Left Join Voucher_Type VT On VT.V_Type=L.V_Type "
        SQL = SQL + StrCondition1 & " And VT.Category NOT IN('RCT','PMT') "
        SQL = SQL + " And L.subcode IN ('" & FGMain(GFilterCode, 3).Value & "')"
        SQL = SQL + ") Tab on tab.v_date=t.code Order By t.code,DocId"

        DTTemp = New DataTable("Tab")

        DTTemp = CMain.FGetDatTable(SQL, AgL.GCn)


        FCashBookDouble(DTTemp, DblOpening, Pagewise, Withnarration)
    End Sub
    Private Sub FCashBookDouble(ByVal DTTemp As DataTable, ByVal DblOpening As Double, _
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
                    FCAddOpening(IIf(DblOpening < 0, 0, Math.Abs(DblOpening)), IIf(DblOpening < 0, Math.Abs(DblOpening), 0), DT_CSHBook, _
                                                   CrPos, DrPos, AgL.XNull(.Item("V_Date")))
                End If
                FCAddRow(AgL.VNull(.Item("AmtDr")), AgL.VNull(.Item("AmtCr")), DT_CSHBook, AgL.XNull(.Item("V_No")), _
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
    Private Sub FCAddOpening(ByVal DblDr As Double, ByVal DblCr As Double, ByRef DTCashBook As DataTable, _
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
    Private Sub FCAddRow(ByVal DblDr As Double, ByVal DblCr As Double, ByRef DTCashBook As DataTable, _
    ByVal StrVNo As String, ByVal StrVType As String, ByVal StrParticular As String, ByVal StrVDate As Date, _
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

    Private Sub FAddOpening(ByVal DblDr As Double, ByVal DblCr As Double, ByRef DTCashBook As DataTable, _
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
    Private Sub FAddRow(ByVal DblDr As Double, ByVal DblCr As Double, ByRef DTCashBook As DataTable, _
    ByVal StrVNo As String, ByVal StrVType As String, ByVal StrParticular As String, ByVal StrVDate As Date, _
    ByRef IntCrPos As Integer, ByRef IntDrPos As Integer, ByRef DblOpening As Double, ByVal StrChqNo As String, _
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
        StrCondition1 = " Where (LG.V_Date Between " & AgL.ConvertDate(FGMain(GFilter, 0).Value.ToString) & " And " & AgL.ConvertDate(FGMain(GFilter, 1).Value.ToString) & ") "

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

        SQL = "Select  (IsNull(Sum(AmtCr),0)-IsNull(Sum(AmtDr),0)) As OP,Max(V_Date) As V_Date From Ledger LG "
        SQL = SQL + "Left Join SubGroup SG On LG.SubCode=SG.SubCode  "
        SQL = SQL + "Where  V_Date<" & AgL.ConvertDate(FGMain(GFilter, 0).Value.ToString) & " "
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
        SQL = "Select LG.DocId,LG.AmtDr,LG.AmtCr,LG.V_Date,(Convert(Varchar,LG.V_No)+'-'+LG.Site_Code) As RecId,LG.V_Type, "
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
                    FAddOpening(IIf(DblOpening > 0, DblOpening, 0), IIf(DblOpening > 0, 0, Math.Abs(DblOpening)), DT_CSHBook, _
                                CrPos, DrPos, AgL.XNull(.Item("VDate")))
                End If
                FAddRow(AgL.VNull(.Item("AmtDr")), AgL.VNull(.Item("AmtCr")), DT_CSHBook, AgL.XNull(.Item("VNo")), _
                        AgL.XNull(.Item("Type")), AgL.XNull(.Item("Particular")), AgL.XNull(.Item("VDate")), CrPos, DrPos, DblOpening, _
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
        StrCondition1 = " Where (LG.V_Date Between " & AgL.ConvertDate(FGMain(GFilter, 0).Value.ToString) & " And " & AgL.ConvertDate(FGMain(GFilter, 1).Value.ToString) & ") "
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

        SQL = "Select  (IsNull(Sum(AmtCr),0)-IsNull(Sum(AmtDr),0)) As OP,Max(V_Date) As V_Date From Ledger LG "
        SQL = SQL + "Left Join SubGroup SG On LG.SubCode=SG.SubCode  "
        SQL = SQL + "Where  V_Date<" & AgL.ConvertDate(FGMain(GFilter, 0).Value.ToString) & " "
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

            FAddRowBankCash(DTbankBook, "", StrVType, StrVParticular, StrVDate, "", "", "", DblAmtDr, DblAmtCr, DblOpening, StrWithnarration)
        End If

        SQL = "Select LG.DocId,LG.AmtDr,LG.AmtCr,LG.V_Date,(Convert(Varchar,LG.RecID)+'-'+LG.Site_Code) As RecId,LG.V_Type, "
        SQL += "LG.Chq_No,LG.Chq_Date,LG.SubCode,LG.ContraSub,LG.Narration, "
        SQL += "SG.Name As PName,SGC.Name As CName,IsNull(VT.SerialNo,0) As SerialNo "
        SQL += "From Ledger LG "
        SQL += "Left Join SubGroup SG On LG.SubCode=SG.SubCode "
        SQL += "Left Join SubGroup SGC On LG.ContraSub=SGC.SubCode "
        SQL += "Left Join Voucher_Type VT On VT.V_Type=LG.V_Type "
        SQL += StrCondition1

        SQL += "And (IsNull(LG.TDSCategory,'')='' Or IsNull(LG.System_Generated,'N')<>'Y') "

        SQL += "And DocId In "
        SQL += "(Select DocId From Ledger LG Where LG.SubCode='" & StrMainSubCode & "') "
        'SQL += "Order By LG.V_Date,LG.V_No,LG.DocId,LG.AmtDr "
        SQL += "Order By LG.V_Date,SerialNo,LG.RecID,LG.DocId,LG.AmtDr "

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

        FLoadMainReport("BankBookSingle", DTbankBook)
        CMain.FormulaSet(RptMain, Me.Text, FGMain)
        CMain.FShowReport(RptMain, Me.MdiParent, Me.Text)
    End Sub
    Private Sub FAddRowBankCash(ByVal DTTable As DataTable, _
     ByVal StrVNo As String, ByVal StrVType As String, ByVal StrParticular As String, ByVal StrVDate As Date, _
     ByVal StrChqNo As String, ByVal StrChqDt As String, ByVal StrNarration As String, _
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
        STRDATE = AgL.ConvertDate(FGMain(GFilter, 0).Value.ToString)
        StrCondition1 = " LG.V_Date <= " & AgL.ConvertDate(FGMain(GFilter, 0).Value.ToString) & " "
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
        StrSQLQuery = StrSQLQuery + " max(Sg.Name) As PName,Max(SM.Name) As Division,0 As AmtPR_Contra,Max(AG.GroupName) as GroupName, Isnull(Max(SG.CreditLimit),0) as CreditLimit ,Isnull(Max(SG.DueDays),0) as DueDays,Isnull(Max(CT.CityName),'') AS CityName    From Ledger As Lg "
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
        StrSQLQuery = StrSQLQuery + " max(Sg.Name) As PName,Max(SM.Name) As Division,0 As AmtPR_Contra,Max(AG.GroupName) as GroupName,Isnull(Max(SG.CreditLimit),0) as CreditLimit ,Isnull(Max(SG.DueDays),0) as DueDays,Isnull(Max(CT.CityName),'') AS CityName    From Ledger As Lg "
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
        StrSQLQuery = StrSQLQuery + " max(Sg.Name) As PName,max(sm.name) As Division,0 As AmtPR_Contra,Max(AG.GroupName) as GroupName,Isnull(Max(SG.CreditLimit),0) as CreditLimit ,Isnull(Max(SG.DueDays),0) as DueDays,Isnull(Max(CT.CityName),'') AS CityName   From Ledger As Lg "
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
        StrSQLQuery = StrSQLQuery + " max(Sg.Name) As PName,max(sm.name) As Division,0 As AmtPR_Contra,Max(AG.GroupName) as GroupName,Isnull(Max(SG.CreditLimit),0) as CreditLimit ,Isnull(Max(SG.DueDays),0) as DueDays ,Isnull(Max(CT.CityName),'') AS CityName   From Ledger As Lg "
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
        StrSQLQuery = StrSQLQuery + " max(Sg.Name) As PName,max(sm.name) As Division,0 As AmtPR_Contra,Max(AG.GroupName) as GroupName,Isnull(Max(SG.CreditLimit),0) as CreditLimit ,Isnull(Max(SG.DueDays),0) as DueDays,Isnull(Max(CT.CityName),'') AS CityName   From Ledger As Lg "
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
        StrSQLQuery = StrSQLQuery + " max(Sg.Name) As PName,max(sm.name) As Division,0 As AmtPR_Contra ,Max(AG.GroupName) as GroupName,Isnull(Max(SG.CreditLimit),0) as CreditLimit ,Isnull(Max(SG.DueDays),0) as DueDays,Isnull(Max(CT.CityName),'') AS CityName   From Ledger As Lg "
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
        StrSQLQuery = StrSQLQuery + " max(Sg.Name) As PName,max(sm.name) As Division,0 As AmtPR_Contra,Max(AG.GroupName) as GroupName,Isnull(Max(SG.CreditLimit),0) as CreditLimit ,Isnull(Max(SG.DueDays),0) as DueDays,Isnull(Max(CT.CityName),'') AS CityName   From Ledger As Lg "
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
        StrSQLQuery = StrSQLQuery + " Isnull(Max(SG.CreditLimit),0) as CreditLimit ,Isnull(Max(SG.DueDays),0) as DueDays,Isnull(Max(CT.CityName),'') AS CityName   From Ledger As Lg "
        StrSQLQuery = StrSQLQuery + " Left Join SubGroup As SG On Lg.SubCode=Sg.SubCode"
        StrSQLQuery = StrSQLQuery + " Left Join AcGroup As AG On ag.GroupCode=Sg.GroupCode "
        StrSQLQuery = StrSQLQuery + " Left Join Sitemast As SM On SM.Code=Lg.site_Code "
        StrSQLQuery = StrSQLQuery + " LEFT JOIN City CT ON CT.CityCode =SG.CityCode "
        StrSQLQuery = StrSQLQuery + " Where "
        StrSQLQuery = StrSQLQuery + StrCondition1 + Strconditionsite
        StrSQLQuery = StrSQLQuery + " And Lg.V_Type<>'F_AO'   "
        StrSQLQuery = StrSQLQuery + StrConditionGrpOn

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
        StrCondition1 = " Where (LG.V_Date <= " & AgL.ConvertDate(FGMain(GFilter, 0).Value.ToString) & " And IsNull(LG." & StrAmt1 & ",0)>0) And (SG.GroupCode In (SELECT AGP.GroupCode FROM AcGroupPath AGP WHERE AGP.GroupUnder='" & StrCnd & "') Or SG.GroupCode='" & StrCnd & "') "
        StrCondition2 = " Where (LG.V_Date <= " & AgL.ConvertDate(FGMain(GFilter, 0).Value.ToString) & ") And IsNull(LG." & StrAmt2 & ",0)>0 And IsNull(LG." & StrAmt2 & ",0)-ISNULL(T.AMOUNT,0)<>0 And (SG.GroupCode In (SELECT AGP.GroupCode FROM AcGroupPath AGP WHERE AGP.GroupUnder='" & StrCnd & "') Or SG.GroupCode='" & StrCnd & "') "

        If Trim(FGMain(GFilterCode, 1).Value) <> "" Then StrCondition1 = StrCondition1 & " And (IsNull(SG.GroupCode,'') In (Select IsNull(AGP.GroupCode,'') From AcGroupPath AGP Where AGP.GroupUnder In (" & FGMain(GFilterCode, 1).Value & ")) Or SG.GroupCode In (" & FGMain(GFilterCode, 1).Value & ")) "
        If Trim(FGMain(GFilterCode, 1).Value) <> "" Then StrCondition2 = StrCondition2 & " And (IsNull(SG.GroupCode,'') In (Select IsNull(AGP.GroupCode,'') From AcGroupPath AGP Where AGP.GroupUnder In (" & FGMain(GFilterCode, 1).Value & ")) Or SG.GroupCode In (" & FGMain(GFilterCode, 1).Value & ")) "

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


        StrSQLQuery = "Select LG.DocId,LG.V_SNo,Convert(Varchar,Max(LG.V_No)) as VNo,Max(LG.V_Type) as VType,Max(LG.V_Date) as VDate,Max(SG.Name) As PName,"
        StrSQLQuery = StrSQLQuery + "Max(LG.SubCode) as SubCode,Max(LG.Narration) as Narration,Max(LG." & StrAmt1 & ") as Amt1,0 As Amt2,IsNull(Sum(LA.Amount),0) as Amt, "
        StrSQLQuery = StrSQLQuery + "Max(SG.Add1)As Add1,Max(SG.Add2)As Add2,Max(C.CityName)As CityName,Max(CT.Name) as Country,MAx(St.name) As SiteName,max(Ag.GroupName) as AcGroupName,'" + Trim(FGMain(GFilterCode, 4).Value) + "' as RepChoice  "
        StrSQLQuery = StrSQLQuery + "From Ledger LG Left Join SubGroup SG On LG.Subcode=SG.SubCode Left Join "
        StrSQLQuery = StrSQLQuery + "City C on SG.CityCode=C.CityCode Left Join Country CT on SG.CountryCode=CT.Code LEFT JOIN AcGroup AG ON SG.GroupCode =AG.GroupCode  "
        StrSQLQuery = StrSQLQuery + "Left Join LedgerAdj LA On LG.DocId=LA.Adj_DocID  And LG.V_SNo=LA.Adj_V_SNo "
        StrSQLQuery = StrSQLQuery + "LEFT JOIN SiteMast ST ON LG.Site_Code =St.code  "
        StrSQLQuery = StrSQLQuery + "LEFT JOIN ZoneMast ZM ON ZM.Code =SG.Zone "
        StrSQLQuery = StrSQLQuery + StrCondition1
        StrSQLQuery = StrSQLQuery + "Group By LG.DocId,LG.V_SNo "
        StrSQLQuery = StrSQLQuery + "HAVING(IsNull(Sum(LA.Amount), 0) <> Max(LG." & StrAmt1 & "))"
        StrSQLQuery = StrSQLQuery + "Union All "
        StrSQLQuery = StrSQLQuery + "Select	LG.DocId,LG.V_SNo,Convert(Varchar,LG.V_No) As V_No,LG.V_Type,LG.V_Date,SG.Name As PName,LG.SubCode, "
        StrSQLQuery = StrSQLQuery + "LG.Narration,0 As Amt1,ISNULL(LG." & StrAmt2 & ",0)-ISNULL(T.AMOUNT,0) as Amt2,0 As Amount,Null As Add1,Null As Add2,"
        StrSQLQuery = StrSQLQuery + "Null As CityName,Null As Country,ST.name As sitename,isnull(Ag.GroupName,'') as AcGroupName,'" + Trim(FGMain(GFilterCode, 4).Value) + "' as RepChoice  "
        StrSQLQuery = StrSQLQuery + "From Ledger LG Left Join SubGroup SG On SG.SubCode=LG.SubCode LEFT JOIN AcGroup AG ON SG.GroupCode =AG.GroupCode LEFT JOIN ZoneMast ZM ON ZM.Code =SG.Zone  LEFT JOIN SiteMast ST ON LG.Site_Code =St.code   "
        StrSQLQuery = StrSQLQuery + "LEFT JOIN (SELECT LA.Vr_Docid AS Docid,LA.Vr_V_SNo AS S_No,SUM(AMOUNT) AS AMOUNT FROM LedgerAdj LA GROUP BY LA.Vr_DocId,LA.Vr_V_SNo) T ON T.DOCID=LG.DOCID AND T.S_NO=LG.V_SNO  "
        StrSQLQuery = StrSQLQuery + StrCondition2
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
        FRH_Multiple(2) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable( _
                          "Select 'o' As Tick,Sm.Code,Sm.Name From Sitemast Sm where code in (" & AgL.PubSiteList & ")   Order By Sm.Name", _
                          AgL.GCn)), "", 300, 360, , , False, AgL.PubSiteCode)
        FRH_Multiple(2).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(2).FFormatColumn(1, , 0, , False)
        FRH_Multiple(2).FFormatColumn(2, "Name", 240, DataGridViewContentAlignment.MiddleLeft)
    End Sub


    Private Sub FCash_Fund_Flow(ByVal IntType As Integer)
        Dim StrCondition1 As String, StrConditionsite As String, reptype As String
        Dim DTTemp As DataTable

        If Not FIsValid(0) Then Exit Sub
        If Not FIsValid(1) Then Exit Sub

        StrCondition1 = " And (Ledger.V_Date Between " & AgL.ConvertDate(FGMain(GFilter, 0).Value.ToString) & " And " & AgL.ConvertDate(FGMain(GFilter, 1).Value.ToString) & ") "

        StrConditionsite = ""
        'If Trim(FGMain(GFilterCode, 2).Value) <> "" Then StrConditionsite = " And Ledger.site_Code In (" & FGMain(GFilterCode, 2).Value & ") "
        If Trim(FGMain(GFilterCode, 2).Value) <> "" Then
            StrConditionsite = StrConditionsite & " And  Ledger.Site_Code IN (" & FGMain(GFilterCode, 2).Value & ") "
        Else
            StrConditionsite = StrConditionsite & " And  Ledger.Site_Code IN (" & AgL.PubSiteList & ") "
        End If

        If IntType = 1 Then reptype = "Cash" Else reptype = "Bank"

        StrSQLQuery = "SELECT s.*,a.* from( "
        ''1 sources of funds part (s Table
        StrSQLQuery = StrSQLQuery + "SELECT row_number() OVER (ORDER BY id) AS sno,id,groupname,sourceamt FROM ("
        ''1.1 cash bal selection (temp table
        StrSQLQuery = StrSQLQuery + "SELECT 1 AS id, '' AS type,'Cash In hand' AS groupname, (IsNull(sum(amtcr),0)-IsNull(Sum(amtdr),0)) AS sourceamt FROM Ledger "
        StrSQLQuery = StrSQLQuery + "WHERE SubCode IN (SELECT subcode FROM SubGroup WHERE Nature IN ('" & reptype & "')) "
        StrSQLQuery = StrSQLQuery + StrConditionsite
        StrSQLQuery = StrSQLQuery + StrCondition1
        StrSQLQuery = StrSQLQuery + "UNION ALL "
        ''1.2 groups for sources of funds
        StrSQLQuery = StrSQLQuery + "SELECT 2 AS id,'Sourcesoffunds' AS type,max(acgroup.GroupName ) AS groupname,"
        StrSQLQuery = StrSQLQuery + "IsNull(sum(amtcr),0) AS sourceamt FROM Ledger LEFT JOIN SubGroup ON Ledger.SubCode =subgroup.SubCode "
        StrSQLQuery = StrSQLQuery + "LEFT JOIN AcGroup ON AcGroup.GroupCode =SubGroup.GroupCode "
        StrSQLQuery = StrSQLQuery + "WHERE DocId IN "
        StrSQLQuery = StrSQLQuery + "(SELECT DISTINCT docid FROM Ledger WHERE SubCode IN ("
        StrSQLQuery = StrSQLQuery + "SELECT subcode FROM SubGroup WHERE Nature IN ('" & reptype & "'))) "
        StrSQLQuery = StrSQLQuery + "AND ledger.SubCode NOT IN (SELECT subcode FROM SubGroup WHERE Nature IN ('" & reptype & "')) "
        StrSQLQuery = StrSQLQuery + StrConditionsite
        StrSQLQuery = StrSQLQuery + StrCondition1
        StrSQLQuery = StrSQLQuery + "AND isnull(ledger.Amtcr,0)>0 "
        StrSQLQuery = StrSQLQuery + "GROUP BY acgroup.GroupCode "
        ''1.3 just to getmax no of rows here to support left join
        StrSQLQuery = StrSQLQuery + "UNION ALL "
        StrSQLQuery = StrSQLQuery + "SELECT 2 AS id,'NA'AS type,'',0 FROM acgroup GROUP BY groupcode ) AS temp "
        StrSQLQuery = StrSQLQuery + ") s "
        StrSQLQuery = StrSQLQuery + " Left Join"
        ''2 application of funds (a Table
        StrSQLQuery = StrSQLQuery + "(SELECT row_number() OVER (ORDER BY id2) AS sno2,groupname2,appamt FROM( "
        ''2.1 selecting cash balance( Temp4 table
        StrSQLQuery = StrSQLQuery + "SELECT 1 AS id2, '' AS type,'Cash In hand' AS groupname2, (IsNull(sum(amtdr),0)-IsNull(Sum(amtcr),0)) AS appamt FROM Ledger "
        StrSQLQuery = StrSQLQuery + "WHERE SubCode IN (SELECT subcode FROM SubGroup WHERE Nature IN ('" & reptype & "')) "
        StrSQLQuery = StrSQLQuery + StrConditionsite
        StrSQLQuery = StrSQLQuery + StrCondition1
        StrSQLQuery = StrSQLQuery + "UNION all "
        ''2.2 groups for application of funds
        StrSQLQuery = StrSQLQuery + "SELECT 2 AS id2,'Applicationoffunds' AS type,max(AcGroup.GroupName) AS groupname2,"
        StrSQLQuery = StrSQLQuery + "IsNull(sum(amtdr),0) AS appamt "
        StrSQLQuery = StrSQLQuery + "FROM Ledger LEFT JOIN SubGroup ON Ledger.SubCode =subgroup.SubCode "
        StrSQLQuery = StrSQLQuery + "LEFT JOIN AcGroup ON AcGroup.GroupCode =SubGroup.GroupCode   "
        StrSQLQuery = StrSQLQuery + "WHERE DocId IN "
        StrSQLQuery = StrSQLQuery + "(SELECT DISTINCT docid FROM Ledger WHERE SubCode IN ("
        StrSQLQuery = StrSQLQuery + "SELECT subcode FROM SubGroup WHERE Nature IN ('" & reptype & "'))) "
        StrSQLQuery = StrSQLQuery + "AND ledger.SubCode NOT IN (SELECT subcode FROM SubGroup WHERE Nature IN ('" & reptype & "')) "
        StrSQLQuery = StrSQLQuery + StrConditionsite
        StrSQLQuery = StrSQLQuery + StrCondition1
        StrSQLQuery = StrSQLQuery + "AND isnull(ledger.Amtdr,0)>0 "
        StrSQLQuery = StrSQLQuery + "GROUP BY AcGroup.GroupCode) AS temp4 "
        StrSQLQuery = StrSQLQuery + ") a ON s.sno=a.sno2 "

        DTTemp = CMain.FGetDatTable(StrSQLQuery, AgL.GCn)

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


        StrSQLQuery = "SELECT CASE WHEN (Sum(Amtdr)-Sum(Amtcr))> 0 THEN Sum(Amtdr)-Sum(Amtcr) ELSE 0 end  AS bal ,Max(SG.name) AS Party, "
        StrSQLQuery = StrSQLQuery + "LEFT(convert(CHAR,max(lg.V_Date),7),3) AS month "
        StrSQLQuery = StrSQLQuery + "FROM Ledger lg LEFT JOIN subgroup sg ON lg.SubCode =sg.SubCode  "
        StrSQLQuery = StrSQLQuery + StrCondition1
        StrSQLQuery = StrSQLQuery + "GROUP BY sg.SubCode, LEFT(convert(CHAR,(lg.V_Date),7),3)" + StrCondition2 + "Order By LEFT(convert(CHAR,max(lg.V_Date),7),3) "

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

        StrCondition2 = "WHERE AR.V_Date Between  '" & AgL.PubStartDate & "' And  '" & AgL.PubEndDate & "' "
        StrCondition3 = "And AT.V_Date Between  '" & AgL.PubStartDate & "' And  '" & AgL.PubEndDate & "' "

        If Trim(FGMain(GFilterCode, 1).Value) <> "" Then
            StrCondition2 = StrCondition2 & " And AGM.Code IN (" & FGMain(GFilterCode, 1).Value & ") "
        End If

        StrSQLQuery = "SELECT DISTINCT AGM.Name AS Group_Name,AM.Name AS Asset_Description,AGM.Depreciation AS Depreciation,"
        StrSQLQuery = StrSQLQuery + "(SELECT Distinct AMOUNT FROM AssetTransaction AT WHERE AM.Docid=AT.Asset AND AT.V_TYPE IN ('ASTOP') " & StrCondition3 & ") AS OPEING,"
        If StrLast6Month <> "" Then
            StrSQLQuery = StrSQLQuery + "(SELECT SUM(AMOUNT) FROM AssetTransaction AT WHERE AM.Docid=AT.Asset AND AT.V_TYPE IN ('ASTPR','ASTAP') And AT.V_Date Between " & StrLast6Month & ") AS Last6Month,"
            StrSQLQuery = StrSQLQuery + "(SELECT Distinct AMOUNT FROM AssetTransaction AT WHERE AM.Docid=AT.Asset AND AT.V_TYPE IN ('ASTPR') And AT.V_Date Between " & StrLast6Month & ") AS PurchaseVal,"
            StrSQLQuery = StrSQLQuery + "(SELECT Distinct DATEDIFF(DD,V_DATE,'" & FGMain(GFilter, 0).Value & "') FROM AssetTransaction AT WHERE AM.Docid=AT.Asset AND AT.V_TYPE IN ('ASTPR') And AT.V_Date Between " & StrLast6Month & ") AS DepLast6Days,"
        Else
            StrSQLQuery = StrSQLQuery + "0  AS Last6Month,"
            StrSQLQuery = StrSQLQuery + "0  AS PurchaseVal,"
            StrSQLQuery = StrSQLQuery + "0 AS DepLast6Days,"
        End If
        StrSQLQuery = StrSQLQuery + "(SELECT SUM(AMOUNT) FROM AssetTransaction AT WHERE AM.Docid=AT.Asset AND AT.V_TYPE IN ('ASTPR','ASTAP') And AT.V_Date Between " & StrIST6Month & ") AS Ist6Month,"
        StrSQLQuery = StrSQLQuery + "(SELECT Distinct DATEDIFF(DD,'" & AgL.PubStartDate & "','" & FGMain(GFilter, 0).Value & "') FROM AssetTransaction AT) AS DepIst6Days,"
        StrSQLQuery = StrSQLQuery + "(SELECT Distinct AMOUNT FROM AssetTransaction AT WHERE AM.Docid=AT.Asset AND AT.V_TYPE IN ('ASTSL') " & StrCondition3 & ") AS SALEVal "
        StrSQLQuery = StrSQLQuery + "FROM AssetMast AM "
        StrSQLQuery = StrSQLQuery + "INNER JOIN AssetTransaction AR ON AM.Docid=AR.Asset "
        StrSQLQuery = StrSQLQuery + "INNER JOIN Voucher_Type VT ON VT.V_Type=AR.V_Type "
        StrSQLQuery = StrSQLQuery + "INNER JOIN AssetGroupMast AGM ON AGM.Code=AM.AssetGroup " + StrCondition2

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

        mTotalVatPayble = AgL.VNull(AgL.Dman_Execute("SELECT Sum(L.AmtDr) - Sum(L.AmtCr)  AS Balance " & _
                " FROM Ledger L " & _
                " LEFT JOIN SubGroup Sg ON L.SubCode = Sg.SubCode " & _
                " LEFT JOIN AcGroup G ON Sg.GroupCode = G.GroupCode " & _
                " WHERE G.GroupName = 'Vat'  " & _
                " And L.V_Date Between '" & DateValue(FGMain(GFilter, 0).Value) & "' And '" & DateValue(FGMain(GFilter, 1).Value) & "' ", AgL.GCn).ExecuteScalar)


        mQry = "SELECT L.SubCode, Max(Sg.Name) As AcName, Max(Sg.ManualCode) As ManualCode, " & _
                " Sum(L.AmtDr) - Sum(L.AmtCr)  AS Balance " & _
                " FROM Ledger L " & _
                " LEFT JOIN SubGroup Sg ON L.SubCode = Sg.SubCode " & _
                " LEFT JOIN AcGroup G ON Sg.GroupCode = G.GroupCode " & _
                " WHERE G.GroupName = 'Vat' " & _
                " And L.V_Date Between '" & DateValue(FGMain(GFilter, 0).Value) & "' And '" & DateValue(FGMain(GFilter, 1).Value) & "' " & _
                " GROUP BY L.SubCode "
        mQry += " UNION ALL "
        mQry += " Select Sg.SubCode, Sg.Name As AcName, Sg.ManualCode As ManualCode, " & _
                " " & -mTotalVatPayble & " As Balance " & _
                " From SubGroup Sg " & _
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
End Class


