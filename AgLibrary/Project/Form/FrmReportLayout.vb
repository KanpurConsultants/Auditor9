Imports CrystalDecisions.CrystalReports.Engine
Imports System.Text
Public Class FrmReportLayout
    Public Agl As AgLibrary.ClsMain

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
    Public Const GFieldCode As Byte = 0
    Public Const GFieldName As Byte = 1
    Public Const GFilter As Byte = 2
    Public Const GButton As Byte = 3
    Public Const GFilterCode As Byte = 4
    Public Const GFilterCodeDataType As Byte = 5
    Public Const GDataType As Byte = 6
    Public Const GDisplayOnReport As Byte = 7
    Public Const GHelpQuery As Byte = 8
    Public Const GHGHeight As Byte = 9
    Public Const GHGWidth As Byte = 10
    Public Const GHGColWidth As Byte = 11
    Public Const GHGColAlignment As Byte = 12
    '=======================================
    '===== For FGMain Columns In Grid ======
    '================= End =================
    '=======================================

    Private StrReportFor As String
    Private StrModule As String
    Private IntFrmWidth As Integer
    Private IntFrmHeight As Integer
    Private StrReportPath As String
    Private IntNoOfSubReport As String

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

    Sub New(objAgl As AgLibrary.ClsMain, ByVal StrModuleVar As String, ByVal StrReportForVar As String, ByVal StrFormCaption As String, ByVal StrReportPathVar As String, Optional ByVal IntFrmWidthVar As Integer = 554,
    Optional ByVal IntFrmHeightVar As Integer = 498, Optional ByVal IntFieldWidth As Integer = 143, Optional ByVal IntFilterWidth As Integer = 300)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Agl = objAgl
        IntFrmHeight = IntFrmHeightVar
        IntFrmWidth = IntFrmWidthVar
        StrModule = StrModuleVar
        StrReportPath = StrReportPathVar
        StrReportFor = Trim(UCase(StrReportForVar))
        Me.Text = StrFormCaption
        Agl.GridDesign(FGMain)
        GlobalIniGrid(IntFieldWidth, IntFilterWidth)
        If Agl.VNull(Agl.Dman_Execute("SELECT Count(*) FROM Report_Module_Menu Where MnuModule='" & StrModule & "' And MnuName='" & StrReportFor & "' ", Agl.GCn).ExecuteScalar) > 0 Then
            IniGrid()
        End If

    End Sub
    'This Procedure Is For Designing Grid Globaly Used In Every Report
    Private Sub GlobalIniGrid(ByVal IntFieldWidth As Integer, ByVal IntFilterWidth As Integer)
        Agl.AddTextColumn(FGMain, "FieldCode", 0, 0, "FieldCode", False, True, False)
        Agl.AddTextColumn(FGMain, "FieldName", IntFieldWidth, 0, "Field", True, True, False)
        Agl.AddTextColumn(FGMain, "Filter", IntFilterWidth, 0, "Filter", True, False, False)
        FGMain.Columns.Add("Button", "")
        FGMain.Columns(GButton).Width = 27
        Agl.AddTextColumn(FGMain, "FilterCode", 0, 0, "FilterCode", False, True, False)
        Agl.AddTextColumn(FGMain, "FilterCodeDataType", 0, 0, "FilterCodeDataType", False, True, False)
        Agl.AddTextColumn(FGMain, "DataType", 0, 0, "DataType", False, True, False)
        Agl.AddTextColumn(FGMain, "", 25, 0, "", True, True, False)
        Agl.AddTextColumn(FGMain, "HelpQuery", 0, 0, "HelpQuery", False, True, False)
        Agl.AddTextColumn(FGMain, "HGHeight", 0, 0, "HGHeight", False, True, False)
        Agl.AddTextColumn(FGMain, "HGWidth", 0, 0, "HGWidth", False, True, False)
        Agl.AddTextColumn(FGMain, "ColWidth", 0, 0, "ColWidth", False, True, False)
        Agl.AddTextColumn(FGMain, "ColAlignment", 0, 0, "ColAlignment", False, True, False)
        FGMain.Anchor = (AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Bottom)
        FGMain.AllowUserToAddRows = False
        FGMain.BackgroundColor = Color.White
        FGMain.Columns(GFieldName).DefaultCellStyle.BackColor = Color.FromArgb(230, 230, 250)

        FGMain.DefaultCellStyle.Font = New Font("Arial", 9, FontStyle.Regular)
        FGMain.ColumnHeadersDefaultCellStyle.Font = New Font("Arial", 9, FontStyle.Regular)
        FGMain.Columns(GFieldName).DefaultCellStyle.Font = New Font("Arial", 9, FontStyle.Regular)
        FGMain.Columns(GDisplayOnReport).DefaultCellStyle.Font = New Font("wingdings", 12, FontStyle.Regular)
        FGMain.Columns(GDisplayOnReport).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        FGMain.Columns(GDisplayOnReport).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
    End Sub
    Private Sub FHPGD_Show_Single(ByRef e As System.Windows.Forms.KeyEventArgs)
        Dim StrSendText As String

        If Not Agl.FGrdDisableKeys(e) Then Exit Sub
        StrSendText = Agl.FSendText(FGMain, Chr(e.KeyCode))

        FRH_Single(FGMain.CurrentCell.RowIndex).StartPosition = FormStartPosition.CenterScreen
        FRH_Single(FGMain.CurrentCell.RowIndex).ShowDialog()

        If FRH_Single(FGMain.CurrentCell.RowIndex).BytBtnValue = 0 Then
            If Not FRH_Single(FGMain.CurrentCell.RowIndex).DRReturn.Equals(Nothing) Then
                FGMain(GFilter, FGMain.CurrentCell.RowIndex).Value = FRH_Single(FGMain.CurrentCell.RowIndex).DRReturn.Item(1)
                FGMain(GFilterCode, FGMain.CurrentCell.RowIndex).Value = "'" + FRH_Single(FGMain.CurrentCell.RowIndex).DRReturn.Item(0) + "'"
            End If
        End If
    End Sub
    Private Sub FHPGD_Show_Multiple(ByRef e As System.Windows.Forms.KeyEventArgs)
        Dim StrSendText As String
        Dim StrPrefix As String = "", StrSufix As String = "", StrSeprator As String = ""

        If Not Agl.FGrdDisableKeys(e) Then Exit Sub
        StrSendText = Agl.FSendText(FGMain, Chr(e.KeyCode))

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
    Private Sub FSetValue(ByVal IntRow As Int16, ByVal StrFieldCode As String, ByVal StrFieldName As String,
    ByVal BytDataType As FGDataType, ByVal FCTType As FilterCodeType, ByVal strHelpQuery As String, Optional ByVal StrDefaultValue As String = "",
    Optional ByVal BlnDisplayOnReport As Boolean = True, Optional ByVal intHgHeight As Integer = 400, Optional ByVal intHgWidth As Integer = 400, Optional ByVal intColWidth As Integer = 200, Optional ByVal ColAlignment As DataGridViewContentAlignment = DataGridViewContentAlignment.NotSet)

        Dim BtnCell As DataGridViewButtonCell
        Dim StrArray() As String

        FGMain(GFieldCode, IntRow).Value = StrFieldCode
        FGMain(GFieldName, IntRow).Value = StrFieldName
        FGMain(GDataType, IntRow).Value = BytDataType
        FGMain(GFilterCodeDataType, IntRow).Value = FCTType
        FGMain(GHelpQuery, IntRow).Value = strHelpQuery
        FGMain(GHGHeight, IntRow).Value = intHgHeight
        FGMain(GHGWidth, IntRow).Value = intHgWidth
        FGMain(GHGColWidth, IntRow).Value = intColWidth
        FGMain(GHGColAlignment, IntRow).Value = ColAlignment

        If StrDefaultValue <> "" Then
            StrArray = Split(StrDefaultValue, "|")
            Select Case UCase(Trim(StrArray(0)))
                Case "[LOGINDATE]"
                    FGMain(GFilter, IntRow).Value = Agl.PubLoginDate
                Case "[STARTDATE]"
                    FGMain(GFilter, IntRow).Value = Agl.PubStartDate
                Case "[ENDDATE]"
                    FGMain(GFilter, IntRow).Value = Agl.PubEndDate
                Case Else
                    FGMain(GFilter, IntRow).Value = StrArray(0)
            End Select
            If UBound(StrArray) > 0 Then
                FGMain(GFilterCode, IntRow).Value = StrArray(1)
            End If

            '======== For Both Name, Code Pick From Variables ===========
            Select Case UCase(Trim(StrArray(0)))
                Case "[SITECODE]"
                    FGMain(GFilter, IntRow).Value = Agl.PubSiteName
                    FGMain(GFilterCode, IntRow).Value = Agl.PubSiteCode
                Case "[DIVISIONCODE]"
                    FGMain(GFilter, IntRow).Value = Agl.PubDivName
                    FGMain(GFilterCode, IntRow).Value = Agl.PubDivCode
            End Select

            If FCTType = FilterCodeType.DTString Then
                If FGMain(GFilterCode, IntRow).Value <> "" Then
                    If BytDataType = FGDataType.DT_Selection_Multiple Then FGMain(GFilterCode, IntRow).Value = "''" & FGMain(GFilterCode, IntRow).Value & "''"
                    If BytDataType = FGDataType.DT_Selection_Single Then FGMain(GFilterCode, IntRow).Value = "'" & FGMain(GFilterCode, IntRow).Value & "'"
                End If
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
        Agl.WinSetting(Me, IntFrmHeight, IntFrmWidth, 0, 0)
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
                    If FRH_Multiple(e.RowIndex) Is Nothing Then
                        FRH_Multiple(e.RowIndex) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(Agl.FillData(FRefineSQLQuery(FGMain.Item(GHelpQuery, e.RowIndex).Value), Agl.GCn).Tables(0)), "", Val(FGMain.Item(GHGHeight, e.RowIndex).Value), Val(FGMain.Item(GHGWidth, e.RowIndex).Value), , , False, Replace(FGMain(GFilterCode, e.RowIndex).Value, "'", ""))
                        FRH_Multiple(e.RowIndex).FFormatColumn(0, "Select", 50, DataGridViewContentAlignment.MiddleCenter)
                        FRH_Multiple(e.RowIndex).FFormatColumn(1, , 0, , False)
                        FRH_Multiple(e.RowIndex).FFormatColumn(2, FGMain(GFieldName, e.RowIndex).Value, Val(FGMain(GHGColWidth, e.RowIndex).Value), FGMain.Item(GHGColAlignment, e.RowIndex).Value)
                    End If
                    FHPGD_Show_Multiple(New System.Windows.Forms.KeyEventArgs(Keys.A))
                ElseIf FGMain(GDataType, e.RowIndex).Value = FGDataType.DT_Selection_Single Then
                    'If FRH_Single(e.RowIndex) Is Nothing Then
                    FRH_Single(e.RowIndex) = New DMHelpGrid.FrmHelpGrid(New DataView(Agl.FillData(FRefineSQLQuery(FGMain.Item(GHelpQuery, e.RowIndex).Value), Agl.GCn).Tables(0)), "", Val(FGMain.Item(GHGHeight, e.RowIndex).Value), Val(FGMain.Item(GHGWidth, e.RowIndex).Value), , , False)
                    FRH_Single(e.RowIndex).FFormatColumn(0, , 0, , False)
                    FRH_Single(e.RowIndex).FFormatColumn(1, FGMain(GFieldName, e.RowIndex).Value, Val(FGMain(GHGColWidth, e.RowIndex).Value), FGMain.Item(GHGColAlignment, e.RowIndex).Value)
                    'End If
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
        If TypeOf e.Control Is System.Windows.Forms.TextBox Then
            RemoveHandler DirectCast(e.Control, System.Windows.Forms.TextBox).KeyPress, AddressOf FGrdNumPress
            AddHandler DirectCast(e.Control, System.Windows.Forms.TextBox).KeyPress, AddressOf FGrdNumPress
        End If
    End Sub
    Private Sub FGrdNumPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Select Case FGMain.CurrentCell.ColumnIndex
            Case GFilter
                If FGMain(GDataType, FGMain.CurrentCell.RowIndex).Value = FGDataType.DT_Float Then
                    Agl.NumPress(sender, e, 10, 4, False)
                ElseIf FGMain(GDataType, FGMain.CurrentCell.RowIndex).Value = FGDataType.DT_Numeric Then
                    Agl.NumPress(sender, e, 10, 0, False)
                End If
        End Select
    End Sub
    Private Sub FGMain_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FGMain.KeyDown
        If e.Control Or e.Shift Or e.Alt Then Exit Sub
        Dim mRow As Integer = FGMain.CurrentCell.RowIndex
        Try
            Select Case FGMain.CurrentCell.ColumnIndex
                Case GFilter
                    Select Case Val(FGMain(GDataType, FGMain.CurrentCell.RowIndex).Value)
                        Case FGDataType.DT_Selection_Single
                            'If FRH_Single(mRow) Is Nothing Then
                            FRH_Single(mRow) = New DMHelpGrid.FrmHelpGrid(New DataView(Agl.FillData(FRefineSQLQuery(FGMain.Item(GHelpQuery, mRow).Value), Agl.GCn).Tables(0)), "", Val(FGMain.Item(GHGHeight, mRow).Value), Val(FGMain.Item(GHGWidth, mRow).Value), , , False)
                                FRH_Single(mRow).FFormatColumn(0, , 0, , False)
                                FRH_Single(mRow).FFormatColumn(1, FGMain(GFieldName, mRow).Value, Val(FGMain(GHGColWidth, mRow).Value), FGMain.Item(GHGColAlignment, mRow).Value)
                            'End If

                            FHPGD_Show_Single(e)
                        Case FGDataType.DT_Selection_Multiple
                            If FRH_Multiple(mRow) Is Nothing Then
                                FRH_Multiple(mRow) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(Agl.FillData(FRefineSQLQuery(FGMain.Item(GHelpQuery, mRow).Value), Agl.GCn).Tables(0)), "", Val(FGMain.Item(GHGHeight, mRow).Value), Val(FGMain.Item(GHGWidth, mRow).Value), , , False, Replace(FGMain(GFilterCode, mRow).Value, "'", ""))
                                FRH_Multiple(mRow).FFormatColumn(0, "Select", 50, DataGridViewContentAlignment.MiddleCenter)
                                FRH_Multiple(mRow).FFormatColumn(1, , 0, , False)
                                FRH_Multiple(mRow).FFormatColumn(2, FGMain(GFieldName, mRow).Value, Val(FGMain(GHGColWidth, mRow).Value), FGMain.Item(GHGColAlignment, mRow).Value)
                            End If

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
            MsgBox("System Exception : " & vbCrLf & Ex.Message, MsgBoxStyle.Exclamation)
        End Try
    End Sub
    Private Sub BtnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnClose.Click
        Me.Close()
    End Sub
    Private Function FIsValid(ByVal IntRow As Integer, Optional ByVal StrMsg As String = "Invalid Data") As Boolean
        Dim BlnRtn As Boolean = True

        If FGMain(GFilter, IntRow).Value = "" Then
            MsgBox(FGMain(GFieldName, IntRow).Value + " : " + vbCrLf + StrMsg, MsgBoxStyle.Information)
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
        RptMain.Subreports(UCase(StrSubReportName & ".rpt")).SetDataSource(DTTable)
    End Sub
#End Region
#Region "FIni_Templete For Programmer Help See It."
    '********************************* By VineetJ *************************************
    '============= This Procedure Is For Help It Holds All The Possible ===============
    '============= Combination This Report Tool Can Work On.See It ====================
    '**********************************************************************************
    Private Sub FIni_Templete()
        'For Date Type Field
        FSetValue(0, "Date", "Date", FGDataType.DT_Date, FilterCodeType.DTNone, "", Agl.PubLoginDate)
        'For Numeric Type Field
        FSetValue(1, "Numeric", "Numeric", FGDataType.DT_Numeric, FilterCodeType.DTNone, "")
        'For Float Type Field
        FSetValue(2, "Float", "Float", FGDataType.DT_Float, FilterCodeType.DTNone, "")
        'For String Type Field
        FSetValue(3, "String", "String", FGDataType.DT_String, FilterCodeType.DTNone, "")
        'For None Type Field (User Cannot Change Any Thing In This Type)
        FSetValue(4, "None", "None", FGDataType.DT_None, FilterCodeType.DTNone, "Default")

        'For Party Multiple Selection From DataBase
        FSetValue(5, "Party Name Mutil Sel.", "Party Name Mutil Sel.", FGDataType.DT_Selection_Multiple, FilterCodeType.DTString, "All")
        FRH_Multiple(5) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(Agl.FillData(
                          "Select 'o' As Tick,SG.SubCode,SG.Name,Sg.ManualCode From SubGroup SG Order By SG.Name",
                          Agl.GCn).Tables(0)), "", 600, 660, , , False)
        FRH_Multiple(5).FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple(5).FFormatColumn(1, , 0, , False)
        FRH_Multiple(5).FFormatColumn(2, "Name", 440, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple(5).FFormatColumn(3, "Code", 100, DataGridViewContentAlignment.MiddleLeft)

        'For Godown (From Database) Single Selection
        FSetValue(6, "Godown DB Single Sel.", "Godown DB Single Sel.", FGDataType.DT_Selection_Single, FilterCodeType.DTString, "")
        FRH_Single(6) = New DMHelpGrid.FrmHelpGrid(New DataView(Agl.FillData(
                        "Select GM.GodCode,GM.GodName From GodownMast GM Order By GM.GodName",
                        Agl.GCn).Tables(0)), "", 300, 300, , , False)
        FRH_Single(6).FFormatColumn(0, , 0, , False)
        FRH_Single(6).FFormatColumn(1, "Name", 200, DataGridViewContentAlignment.MiddleLeft)

        'For Item (From Temporary Table) Single Selection 
        Dim DTTemp As New DataTable
        DTTemp.Columns.Add("Code", System.Type.GetType("System.String"))
        DTTemp.Columns.Add("Name", System.Type.GetType("System.String"))

        DTTemp.Rows.Add(New Object() {"Detail", "Detail"})
        DTTemp.Rows.Add(New Object() {"Summary", "Summary"})

        FSetValue(7, "Report Type Tmp Single Sel.", "Report Type Tmp Single Sel.", FGDataType.DT_Selection_Single, FilterCodeType.DTNone, "Detail")
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

    Public Event ProcessReport()

    Public Enum FieldDataType
        DateType = 0
        NumericType = 1
        FloatType = 2
        StringType = 3
        None = 4
        SingleSelection = 5
        MultiSelection = 6
    End Enum

    Public Enum FieldFilterDataType
        None = 0
        NumericType = 1
        StringType = 2
    End Enum


    Private Sub IniGrid()
        Dim DTTemp As DataTable
        Dim DTTemp1 As New DataTable
        Dim I As Int16, J As Int16
        Dim BlnDisplayOnReport As Boolean
        Dim IntHeight As Int16 = Nothing, IntWidth As Int16 = Nothing
        Dim BlnDisplay As Boolean
        Dim dtDefualtData As DataTable

        Try
            IntNoOfSubReport = 0
            DTTemp = Agl.FillData("Select NoOfSubReport From Report_Module_Menu Where MnuModule='" & StrModule & "' And MnuName='" & StrReportFor & "' ", Agl.GCn).Tables(0)
            If DTTemp.Rows.Count > 0 Then
                IntNoOfSubReport = Agl.VNull(DTTemp.Rows(I).Item("NoOfSubReport"))
            End If

            DTTemp.Dispose()
            DTTemp = Agl.FillData("Select * From Report_Initialize Where MnuModule='" & StrModule & "' And MnuName='" & StrReportFor & "'  Order By RowIndex", Agl.GCn).Tables(0)
            If DTTemp.Rows.Count > 0 Then
                FGMain.Rows.Add(DTTemp.Rows.Count)
                ReDim FRH_Single(DTTemp.Rows.Count)
                ReDim FRH_Multiple(DTTemp.Rows.Count)

                For I = 0 To DTTemp.Rows.Count - 1
                    If UCase(Agl.XNull(DTTemp.Rows(I).Item("DisplayOnReport"))) = "Y" Then
                        BlnDisplayOnReport = True
                    Else
                        BlnDisplayOnReport = False
                    End If
                    FSetValue(I, Agl.XNull(DTTemp.Rows(I).Item("FieldCode")), Agl.XNull(DTTemp.Rows(I).Item("FieldName")), Agl.XNull(DTTemp.Rows(I).Item("FieldDataType")),
                    Agl.XNull(DTTemp.Rows(I).Item("FieldFilterDataType")), Agl.XNull(DTTemp.Rows(I).Item("DMHGQuery")), Agl.XNull(DTTemp.Rows(I).Item("DefaultValue")), BlnDisplayOnReport)

                    dtDefualtData = Agl.FillData("Select * From ReportFilterDefaultValues Where MenuText='" & Me.Text & "' And User_Name = '" & Agl.PubUserName & "' And Head = '" & Agl.XNull(DTTemp.Rows(I).Item("FieldName")) & "' ", Agl.GCn).Tables(0)
                    If dtDefualtData.Rows.Count > 0 Then
                        FGMain.Item("Filter", I).Value = Agl.XNull(dtDefualtData.Rows(0)("Value"))
                        FGMain.Item("FilterCode", I).Value = Replace(Agl.XNull(dtDefualtData.Rows(0)("ValueCode")), "`", "'")


                        If Agl.XNull(DTTemp.Rows(I).Item("FieldDataType")) = FGDataType.DT_Date Then
                            If FGMain.Item("Filter", I).Value = "Today" Then
                                FGMain.Item("Filter", I).Value = Agl.RetDate(Agl.PubLoginDate)
                            ElseIf FGMain.Item("Filter", I).Value = "Yesterday" Then
                                FGMain.Item("Filter", I).Value = Agl.RetDate(DateAdd(DateInterval.Day, -1, CDate(Agl.PubLoginDate)))
                            ElseIf FGMain.Item("Filter", I).Value = "Month Start Date" Then
                                FGMain.Item("Filter", I).Value = Agl.RetMonthStartDate(Agl.PubLoginDate)
                            ElseIf FGMain.Item("Filter", I).Value = "Month End Date" Then
                                FGMain.Item("Filter", I).Value = Agl.RetMonthEndDate(Agl.PubLoginDate)
                            ElseIf FGMain.Item("Filter", I).Value = "Last Month Start Date" Then
                                FGMain.Item("Filter", I).Value = Agl.RetMonthStartDate(DateAdd(DateInterval.Month, -1, CDate(Agl.PubLoginDate)))
                            ElseIf FGMain.Item("Filter", I).Value = "Last Month End Date" Then
                                FGMain.Item("Filter", I).Value = Agl.RetMonthEndDate(DateAdd(DateInterval.Month, -1, CDate(Agl.PubLoginDate)))
                            ElseIf FGMain.Item("Filter", I).Value = "Year Start Date" Then
                                FGMain.Item("Filter", I).Value = Agl.PubStartDate
                            ElseIf FGMain.Item("Filter", I).Value = "Year End Date" Then
                                FGMain.Item("Filter", I).Value = Agl.PubEndDate
                            End If
                        End If
                    End If


                    If Agl.XNull(DTTemp.Rows(I).Item("FieldDataType")) = FGDataType.DT_Selection_Multiple Or Agl.XNull(DTTemp.Rows(I).Item("FieldDataType")) = FGDataType.DT_Selection_Single Then
                        DTTemp1.Dispose()
                        DTTemp1 = Agl.FillData("Select * From Report_HGColFormat Where MnuModule='" & StrModule & "' And MnuName='" & StrReportFor & "' And FieldCode='" & FGMain(GFieldCode, I).Value & "' Order By ColIndex", Agl.GCn).Tables(0)

                        If Not DTTemp.Rows(I).Item("DMHGHeight").GetType.ToString = "System.DBNull" Then
                            IntHeight = DTTemp.Rows(I).Item("DMHGHeight")
                        End If
                        If Not DTTemp.Rows(I).Item("DMHGWidth").GetType.ToString = "System.DBNull" Then
                            IntWidth = DTTemp.Rows(I).Item("DMHGWidth")
                        End If

                        If Agl.XNull(DTTemp.Rows(I).Item("FieldDataType")) = FGDataType.DT_Selection_Single Then
                            FRH_Single(I) = New DMHelpGrid.FrmHelpGrid(New DataView(Agl.FillData(
                                         FRefineSQLQuery(Agl.XNull(DTTemp.Rows(I).Item("DMHGQuery"))),
                                         Agl.GCn).Tables(0)), "", IntHeight, IntWidth, , , False)
                            For J = 0 To DTTemp1.Rows.Count - 1
                                If UCase(Agl.XNull(DTTemp1.Rows(J).Item("ColDisplay"))) = "Y" Then
                                    BlnDisplay = True
                                Else
                                    BlnDisplay = False
                                End If
                                FRH_Single(I).FFormatColumn(Agl.VNull(DTTemp1.Rows(J).Item("ColIndex")), Agl.XNull(DTTemp1.Rows(J).Item("HeaderText")), Agl.VNull(DTTemp1.Rows(J).Item("ColWidth")), Agl.VNull(DTTemp1.Rows(J).Item("ColAlignment")), BlnDisplay)
                            Next
                        ElseIf Agl.XNull(DTTemp.Rows(I).Item("FieldDataType")) = FGDataType.DT_Selection_Multiple Then
                            FRH_Multiple(I) = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(Agl.FillData(
                                        FRefineSQLQuery(Agl.XNull(DTTemp.Rows(I).Item("DMHGQuery"))),
                                        Agl.GCn).Tables(0)), "", IntHeight, IntWidth, , , False, Replace(FGMain(GFilterCode, I).Value, "'", ""))

                            For J = 0 To DTTemp1.Rows.Count - 1
                                If UCase(Agl.XNull(DTTemp1.Rows(J).Item("ColDisplay"))) = "Y" Then
                                    BlnDisplay = True
                                Else
                                    BlnDisplay = False
                                End If
                                FRH_Multiple(I).FFormatColumn(Agl.VNull(DTTemp1.Rows(J).Item("ColIndex")), Agl.XNull(DTTemp1.Rows(J).Item("HeaderText")), Agl.VNull(DTTemp1.Rows(J).Item("ColWidth")), Agl.VNull(DTTemp1.Rows(J).Item("ColAlignment")), BlnDisplay)
                            Next
                        End If
                    End If
                Next
                FGMain(GFilter, 0).Selected = True
            End If
            DTTemp.Dispose()
            DTTemp = Nothing
            DTTemp1.Dispose()
            DTTemp1 = Nothing
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
        End Try
    End Sub
    Private Function FRefineSQLQuery(ByVal StrSQL As String) As String
        StrSQL = Replace(StrSQL, "[SITECODE]", Agl.PubSiteCode)
        Return StrSQL
    End Function
    Private Sub BtnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnPrint.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            RaiseEvent ProcessReport()
            If Agl.VNull(Agl.Dman_Execute("SELECT Count(*) FROM Report_Module_Menu Where MnuModule='" & StrModule & "' And MnuName='" & StrReportFor & "' ", Agl.GCn).ExecuteScalar) > 0 Then
                FReportPrint()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
        End Try
        Me.Cursor = Cursors.Arrow
    End Sub
    Private Sub FReportPrint()
        Dim mQry$ = ""
        Dim I As Integer, J As Integer
        Dim DTTemp As DataTable
        Dim StrValue As Object
        Dim StrParameter As String = ""
        Agpl = New AgLibrary.ClsPrinting(Agl)

        For I = 0 To FGMain.Rows.Count - 1
            If Not FIsValid(I) Then Exit Sub
            StrValue = ""
            Select Case FGMain(GFilterCodeDataType, I).Value
                Case FilterCodeType.DTString
                    If FGMain(GDataType, I).Value = FGDataType.DT_Selection_Multiple Then
                        If Trim(FGMain(GFilterCode, I).Value) <> "" Then
                            StrValue = "'" & Replace(FGMain(GFilterCode, I).Value, "'", "''") & "'"
                        Else
                            StrValue = "''"
                        End If
                    ElseIf FGMain(GDataType, I).Value = FGDataType.DT_Selection_Single Then
                        StrValue = "'''" & FGMain(GFilterCode, I).Value & "'''"
                    Else
                        StrValue = "'''" & FGMain(GFilter, I).Value & "'''"
                    End If
                Case FilterCodeType.DTNumeric, FilterCodeType.DTNone
                    If FGMain(GDataType, I).Value = FGDataType.DT_Selection_Multiple Then
                        If Trim(FGMain(GFilterCode, I).Value) <> "" Then
                            StrValue = "'" & FGMain(GFilterCode, I).Value & "'"
                        End If
                    ElseIf FGMain(GDataType, I).Value = FGDataType.DT_Selection_Single Then
                        StrValue = "'" & FGMain(GFilterCode, I).Value & "'"
                    Else
                        StrValue = "'" & FGMain(GFilter, I).Value & "'"
                    End If
            End Select

            Try
                If StrParameter = "" Then
                    StrParameter = StrValue
                Else
                    StrParameter = StrParameter & "," & StrValue
                End If
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Information)
            End Try
        Next

        Dim mRepName As String = ""
        Dim mRepTitle As String = ""
        Dim mRepProcedureName As String = ""
        Dim DtReportDetail As DataTable = Nothing

        mQry = " Select Replace(Report_File_Sql,'`','''') As Report_File_Sql , " &
                " Replace(Report_Title_Sql,'`','''') As Report_Title_Sql , " &
                " Replace(Report_ProcedureName_Sql,'`','''') As Report_ProcedureName_Sql " &
                " From Report_Module_Menu " &
                " Where MnuName = '" & StrReportFor & "'"
        DtReportDetail = Agl.FillData(mQry, Agl.GCn).Tables(0)

        mRepName = Agl.XNull(Agl.Dman_Execute(Agl.XNull(DtReportDetail.Rows(0)("Report_File_Sql")), Agl.GCn).ExecuteScalar)
        mRepTitle = Agl.XNull(Agl.Dman_Execute(Agl.XNull(DtReportDetail.Rows(0)("Report_Title_Sql")), Agl.GCn).ExecuteScalar)
        mRepProcedureName = Agl.XNull(Agl.Dman_Execute(Agl.XNull(DtReportDetail.Rows(0)("Report_ProcedureName_Sql")), Agl.GCn).ExecuteScalar)



        DTTemp = Agl.FillData("Exec " & mRepProcedureName & " " & StrParameter, Agl.GCn).Tables(0)
        If Not DTTemp.Rows.Count > 0 Then MsgBox("No Records Found to Print.", vbInformation) : Exit Sub

        FLoadMainReport(mRepName, DTTemp)
        For I = 1 To IntNoOfSubReport
            DTTemp.Dispose()
            DTTemp = Agl.FillData("Exec " & mRepProcedureName & Trim(I) & " " & StrParameter, Agl.GCn).Tables(0)
            FLoadSubReport(mRepName & Trim(I), DTTemp)
        Next
        'FormulaSet(RptMain, Me.Text, FGMain, GFieldName, GFilter, GDisplayOnReport)
        'Agpl.Formula_Set(RptMain, mRepTitle)
        Formula_Set(RptMain, mRepTitle)


        '======================================================
        '=========== For Transfering Filter To Report =========
        'For I = 0 To RptMain.DataDefinition.FormulaFields.Count - 1
        '    For J = 0 To FGMain.Rows.Count - 1
        '        If UCase(Trim(RptMain.DataDefinition.FormulaFields.Item(I).Name)) = UCase(Trim(FGMain(GFieldCode, J).Value)) Then
        '            RptMain.DataDefinition.FormulaFields.Item(I).Text = "'" & Trim(FGMain(GFilter, J).Value) & "'"
        '        End If
        '    Next
        'Next

        'For I = 0 To RptMain.DataDefinition.FormulaFields.Count - 1
        '    If RptMain.DataDefinition.FormulaFields(I).Text = "" And FGMain.Item(GFieldName, J).Tag = 0 And FGMain.Item(GFilter, J).Value <> "All" And FGMain(GDisplayOnReport, J).Value = "þ" Then
        '        RptMain.DataDefinition.FormulaFields(I).Text = "'" & FGMain.Item(GFieldName, J).Value & " : " & FGMain.Item(GFilter, J).Value & "'"
        '        FGMain.Item(GFieldName, J).Tag = 1
        '    End If
        'Next

        '======================================================

        FShowReport(RptMain, Me.MdiParent, Me.Text)
    End Sub

    Public Sub FShowReport(ByVal RpdReg As CrystalDecisions.CrystalReports.Engine.ReportDocument,
    ByVal FrmMDI As Form, ByVal StrReportCaption As String, Optional ByVal BlnDirectPrint As Boolean = False,
    Optional ByVal StrPaperSizeName As String = "", Optional ByVal StrLandScape As String = "")

        Dim PDPrint As System.Drawing.Printing.PrintDocument
        Dim PRDGMain As PrintDialog = Nothing
        Dim I As Integer
        Dim IntRawKind As Integer
        Dim NRepView As RepView

        If Trim(StrPaperSizeName) <> "" Then
            PDPrint = New System.Drawing.Printing.PrintDocument()
            For I = 0 To PDPrint.PrinterSettings.PaperSizes.Count - 1
                If UCase(Trim(PDPrint.PrinterSettings.PaperSizes(I).PaperName)) = UCase(Trim(StrPaperSizeName)) Then
                    IntRawKind = CInt(PDPrint.PrinterSettings.PaperSizes(I).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(PDPrint.PrinterSettings.PaperSizes(I)))
                    RpdReg.PrintOptions.PaperSize = IntRawKind
                    RpdReg.PrintOptions.PaperOrientation = IIf(Trim(UCase(StrLandScape)) = "Y", CrystalDecisions.Shared.PaperOrientation.Landscape, CrystalDecisions.Shared.PaperOrientation.Portrait)

                    If Not BlnDirectPrint Then
                        PRDGMain = New PrintDialog
                        PRDGMain.PrinterSettings.PrinterName = PDPrint.PrinterSettings.PrinterName
                        PRDGMain.PrinterSettings.DefaultPageSettings.PaperSize = PDPrint.PrinterSettings.PaperSizes(I)
                        PRDGMain.PrinterSettings.DefaultPageSettings.Landscape = IIf(Trim(UCase(StrLandScape)) = "Y", True, False)
                    End If
                    Exit For
                End If
            Next
        End If

        If BlnDirectPrint Then
            RpdReg.PrintToPrinter(1, True, 1, 1)
        Else
            If PRDGMain Is Nothing Then PRDGMain = New PrintDialog
            'NRepView = New RepView(PRDGMain)
            NRepView = New AgLibrary.RepView(Agl)
            NRepView.RepObj = RpdReg
            NRepView.MdiParent = FrmMDI
            NRepView.Text = StrReportCaption
            NRepView.Show()
        End If
    End Sub


    Public Sub CreateHelpGrid(ByVal FieldCode As String, ByVal FieldName As String,
                               ByVal FieldFilterDataType As FrmReportLayout.FieldFilterDataType,
                               ByVal FieldDataType As FrmReportLayout.FieldDataType,
                               ByVal DMHGQuery As String,
                               Optional ByVal DefaultValue As String = "All",
                               Optional ByVal DMHGHeight As Integer = 400,
                               Optional ByVal DMHGWidth As Integer = 400,
                               Optional ByVal ColWidth As Integer = 200,
                               Optional ByVal ColAlignment As DataGridViewContentAlignment = DataGridViewContentAlignment.NotSet,
                               Optional ByVal DisplayOnReport As Boolean = True)

        Try
            ReDim Preserve FRH_Single(FGMain.Rows.Count)
            ReDim Preserve FRH_Multiple(FGMain.Rows.Count)
            Dim dtDefualtData As DataTable
            Dim mRow As Integer
            FGMain.Rows.Add()
            FSetValue(FGMain.Rows.Count - 1, FieldCode, FieldName, FieldDataType, FieldFilterDataType, DMHGQuery, DefaultValue, DisplayOnReport, DMHGHeight, DMHGWidth, ColWidth, ColAlignment)

            dtDefualtData = Agl.FillData("Select * From ReportFilterDefaultValues Where MenuText='" & Me.Text & "' And User_Name = '" & Agl.PubUserName & "' And Head = '" & FGMain.Item("FieldName", FGMain.Rows.Count - 1).Value & "' ", Agl.GCn).Tables(0)
            If dtDefualtData.Rows.Count > 0 Then
                FGMain.Item("Filter", FGMain.Rows.Count - 1).Value = Agl.XNull(dtDefualtData.Rows(0)("Value"))
                FGMain.Item("FilterCode", FGMain.Rows.Count - 1).Value = Replace(Agl.XNull(dtDefualtData.Rows(0)("ValueCode")), "`", "'")

                mRow = FGMain.Rows.Count - 1
                If FieldDataType = FGDataType.DT_Date Then
                    If FGMain.Item("Filter", mRow).Value = "Today" Then
                        FGMain.Item("Filter", mRow).Value = Agl.RetDate(Agl.PubLoginDate)
                    ElseIf FGMain.Item("Filter", mRow).Value = "Yesterday" Then
                        FGMain.Item("Filter", mRow).Value = Agl.RetDate(DateAdd(DateInterval.Day, -1, CDate(Agl.PubLoginDate)))
                    ElseIf FGMain.Item("Filter", mRow).Value = "Month Start Date" Then
                        FGMain.Item("Filter", mRow).Value = Agl.RetMonthStartDate(Agl.PubLoginDate)
                    ElseIf FGMain.Item("Filter", mRow).Value = "Month End Date" Then
                        FGMain.Item("Filter", mRow).Value = Agl.RetMonthEndDate(Agl.PubLoginDate)
                    ElseIf FGMain.Item("Filter", mRow).Value = "Last Month Start Date" Then
                        FGMain.Item("Filter", mRow).Value = Agl.RetMonthStartDate(DateAdd(DateInterval.Month, -1, CDate(Agl.PubLoginDate)))
                    ElseIf FGMain.Item("Filter", mRow).Value = "Last Month End Date" Then
                        FGMain.Item("Filter", mRow).Value = Agl.RetMonthEndDate(DateAdd(DateInterval.Month, -1, CDate(Agl.PubLoginDate)))
                    ElseIf FGMain.Item("Filter", mRow).Value = "Year Start Date" Then
                        FGMain.Item("Filter", mRow).Value = Agl.PubStartDate
                    ElseIf FGMain.Item("Filter", mRow).Value = "Year End Date" Then
                        FGMain.Item("Filter", mRow).Value = Agl.PubEndDate
                    End If
                End If

            End If


            FGMain(GFilter, 0).Selected = True
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
        End Try
    End Sub

    Public Function GetWhereCondition(ByVal FieldName As String, ByVal Index As Integer) As String
        If FGMain(GFilterCode, Index).Value <> "" Then
            GetWhereCondition = " And " & FieldName & " In (" & FGMain(GFilterCode, Index).Value & ")"
        Else
            GetWhereCondition = ""
        End If
    End Function

    Public Function FGetText(ByVal Index As Integer) As Object
        FGetText = FGMain.Item(GFilter, Index).Value
    End Function

    Public Function FGetCode(ByVal Index As Integer) As Object
        FGetCode = FGMain.Item(GFilterCode, Index).Value
    End Function


    'Public Sub PrintReport(ByVal DtTemp As DataTable, ByVal RepName As String, ByVal RepTitle As String)
    '    Dim I As Integer = 0, J As Integer = 0
    '    FLoadMainReport(RepName, DtTemp)
    '    FormulaSet(RptMain, Me.Text, FGMain, GFieldName, GFilter, GDisplayOnReport)


    '    '======================================================
    '    '=========== For Transfering Filter To Report =========
    '    For I = 0 To RptMain.DataDefinition.FormulaFields.Count - 1
    '        For J = 0 To FGMain.Rows.Count - 1
    '            If UCase(Trim(RptMain.DataDefinition.FormulaFields.Item(I).Name)) = UCase(Trim(FGMain(GFieldCode, J).Value)) Then
    '                RptMain.DataDefinition.FormulaFields.Item(I).Text = "'" & Trim(FGMain(GFilter, J).Value) & "'"
    '            End If
    '        Next
    '    Next
    '    '======================================================

    '    FShowReport(RptMain, Me.MdiParent, Me.Text)
    'End Sub

    Public Sub PrintReport(ByVal DsRep As DataSet, ByVal RepName As String, ByVal RepTitle As String,
                           Optional ByVal ReportPath As String = "",
                           Optional ByVal SubRepQryArr As String() = Nothing)
        Dim mRepNameCustomize As String = ""
        Dim mCrd As New ReportDocument
        Dim ReportView As New AgLibrary.RepView(Agl)
        Dim FrmObj As Form = Nothing
        Dim I As Integer = 0
        Dim mPrnHnd As New AgLibrary.PrintHandler(Agl)
        Dim DsSubRep As DataSet = Nothing
        Agpl = New AgLibrary.ClsPrinting(Agl)

        Try
            Me.Cursor = Cursors.WaitCursor

            If ReportPath.Trim = "" Then ReportPath = Agl.PubReportPath

            Agpl.CreateFieldDefFile1(DsRep, ReportPath & "\" & RepName & ".ttx", True)
            ''''''''''IF CUSTOMER NEED SOME CHANGE IN FORMAT OF A REPORT'''''''''''
            ''''''''''CUTOMIZE REPORT CAN BE CREATED WITHOUT CHANGE IN CODE''''''''
            ''''''''''WITH ADDING 6 CHAR OF COMPANY NAME AND 4 CHAR OF CITY NAME'''
            ''''''''''WITHOUT SPACES IN EXISTING REPORT NAME''''''''''''''''''''''''''''''''''''''
            RepName = Agpl.GetRepNameCustomize(RepName, ReportPath)
            '''''''''''''''''''''''''''''''''''''''''''''''''''''

            mCrd.Load(ReportPath & "\" & RepName & ".rpt")

            If SubRepQryArr IsNot Nothing Then
                For I = 0 To SubRepQryArr.Length - 1
                    DsSubRep = Agl.FillData(SubRepQryArr(I), Agl.GCn)
                    Agpl.CreateFieldDefFile1(DsSubRep, ReportPath & "\" & RepName & I.ToString & ".ttx", True)
                    mCrd.OpenSubreport("SUBREP" & I.ToString).Database.Tables(0).SetDataSource(DsSubRep.Tables(0))
                Next
            End If

            mCrd.SetDataSource(DsRep.Tables(0))
            Agpl.ReportCommonInformation(Agl, mCrd, ReportPath)

            'If SubRep1 = True Then mCrd.OpenSubreport("SUBREP1").Database.Tables(0).SetDataSource(DsRep1.Tables(0))
            'If SubRep2 = True Then mCrd.OpenSubreport("SUBREP2").Database.Tables(0).SetDataSource(DsRep2.Tables(0))

            CType(ReportView.Controls("CrvReport"), CrystalDecisions.Windows.Forms.CrystalReportViewer).ReportSource = mCrd
            Formula_Set(mCrd, RepTitle)
            Agpl.Show_Report(ReportView, "* " & RepTitle & " *", Me.MdiParent)



            Call Agl.LogTableEntry("Report", Me.Text, "P", Agl.PubMachineName, Agl.PubUserName, Agl.PubLoginDate, Agl.GCn)
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Public Sub Formula_Set(ByVal mCRD As ReportDocument, Optional ByVal mRepTitle As String = "")
        Dim I As Integer = 0, J As Integer = 0

        For J = 0 To FGMain.Rows.Count - 1
            FGMain.Item(GFieldName, J).Tag = 0
        Next

        For I = 0 To mCRD.DataDefinition.FormulaFields.Count - 1

            Select Case Agl.UTrim(mCRD.DataDefinition.FormulaFields(I).Name)
                Case Agl.UTrim("comp_name")
                    mCRD.DataDefinition.FormulaFields(I).Text = "'" & Agl.PubCompName & "'"
                Case Agl.UTrim("comp_add")
                    mCRD.DataDefinition.FormulaFields(I).Text = "'" & Agl.PubCompAdd1 & "'"
                Case Agl.UTrim("comp_add1")
                    mCRD.DataDefinition.FormulaFields(I).Text = "'" & Agl.PubCompAdd2 & "'"
                Case Agl.UTrim("comp_Pin")
                    mCRD.DataDefinition.FormulaFields(I).Text = "'" & Agl.PubCompPinCode & "'"
                Case Agl.UTrim("comp_phone")
                    mCRD.DataDefinition.FormulaFields(I).Text = "'" & Agl.PubCompPhone & "'"
                Case Agl.UTrim("comp_city")
                    mCRD.DataDefinition.FormulaFields(I).Text = "'" & Agl.PubCompCity & "'"
                Case Agl.UTrim("Title")
                    mCRD.DataDefinition.FormulaFields(I).Text = "'" & mRepTitle & "'"
                Case Agl.UTrim("Site_Name")
                    mCRD.DataDefinition.FormulaFields(I).Text = "'" & "Branch Name : " & Agl.PubSiteName & " { " & Agl.PubSiteManualCode & " } '"
                Case Agl.UTrim("Division")
                    If Agl.PubDivName IsNot Nothing Then
                        mCRD.DataDefinition.FormulaFields(I).Text = "'" & Agl.PubDivName.ToUpper & " DIVISION" & "'"
                    End If
                Case Agl.UTrim("Tin_No")
                    mCRD.DataDefinition.FormulaFields(I).Text = "'" & "TIN NO : " & Agl.PubCompTIN & "'"
            End Select

        Next



        For I = 0 To mCRD.DataDefinition.FormulaFields.Count - 1
            For J = 0 To FGMain.Rows.Count - 1
                Select Case Agl.UTrim(mCRD.DataDefinition.FormulaFields(I).Name)
                    Case Agl.UTrim("comp_name")
                        mCRD.DataDefinition.FormulaFields(I).Text = "'" & Agl.PubCompName & "'"
                    Case Agl.UTrim("comp_add")
                        mCRD.DataDefinition.FormulaFields(I).Text = "'" & Agl.PubCompAdd1 & "'"
                    Case Agl.UTrim("comp_add1")
                        mCRD.DataDefinition.FormulaFields(I).Text = "'" & Agl.PubCompAdd2 & "'"
                    Case Agl.UTrim("comp_Pin")
                        mCRD.DataDefinition.FormulaFields(I).Text = "'" & Agl.PubCompPinCode & "'"
                    Case Agl.UTrim("comp_phone")
                        mCRD.DataDefinition.FormulaFields(I).Text = "'" & Agl.PubCompPhone & "'"
                    Case Agl.UTrim("comp_city")
                        mCRD.DataDefinition.FormulaFields(I).Text = "'" & Agl.PubCompCity & "'"
                    Case Agl.UTrim("Title")
                        mCRD.DataDefinition.FormulaFields(I).Text = "'" & mRepTitle & "'"
                    Case Agl.UTrim("Site_Name")
                        mCRD.DataDefinition.FormulaFields(I).Text = "'" & "Branch Name : " & Agl.PubSiteName & " { " & Agl.PubSiteManualCode & " } '"
                    Case Agl.UTrim("Division")
                        If Agl.PubDivName IsNot Nothing Then
                            mCRD.DataDefinition.FormulaFields(I).Text = "'" & Agl.PubDivName.ToUpper & " DIVISION" & "'"
                        End If
                    Case Agl.UTrim("Tin_No")
                        mCRD.DataDefinition.FormulaFields(I).Text = "'" & "TIN NO : " & Agl.PubCompTIN & "'"
                    Case Else
                        If mCRD.DataDefinition.FormulaFields(I).Text = "" And FGMain.Item(GFieldName, J).Tag = 0 And FGMain.Item(GFilter, J).Value <> "All" And FGMain(GDisplayOnReport, J).Value = "þ" Then
                            mCRD.DataDefinition.FormulaFields(I).Text = "'" & FGMain.Item(GFieldName, J).Value & " : " & FGMain.Item(GFilter, J).Value & "'"
                            FGMain.Item(GFieldName, J).Tag = 1
                        End If
                End Select
            Next
        Next
    End Sub

    Private Sub FGMain_MouseClick(sender As Object, e As MouseEventArgs) Handles FGMain.MouseClick
        If e.Button = MouseButtons.Right Then
            Dim currentMouseOverRow As Integer
            currentMouseOverRow = FGMain.HitTest(e.X, e.Y).RowIndex

            If currentMouseOverRow >= 0 Then
                mnuSave.Text = "Save " + FGMain.Item("FieldName", currentMouseOverRow).Value
                mnuSave.Tag = currentMouseOverRow
            End If

            MnuOptions.Show(FGMain, New Point(e.X, e.Y))
        End If
    End Sub

    Private Sub mnuSave_Click(sender As Object, e As EventArgs) Handles mnuSave.Click
        'MsgBox("Data : " & FGMain.Item("Filter", CInt(sender.Tag)).Value + " / Code : " & FGMain.Item("FilterCode", CInt(sender.Tag)).Value)
        Dim mQry As String
        Dim mFilter As String
        mQry = "Delete From ReportFilterDefaultValues Where MenuText = '" & Me.Text & "' And User_Name ='" & Agl.PubUserName & "' And Head = '" & FGMain.Item("FieldName", CInt(sender.Tag)).Value & "' "
        Agl.Dman_ExecuteNonQry(mQry, Agl.GCn)

        mFilter = FGMain.Item(GFilter, CInt(sender.Tag)).Value
        If FGMain(GDataType, CInt(sender.Tag)).Value = FGDataType.DT_Date Then
            If FGMain.Item(GFilter, CInt(sender.Tag)).Value = Agl.RetDate(Agl.PubLoginDate) Then
                mFilter = "Today"
            ElseIf FGMain.Item(GFilter, CInt(sender.Tag)).Value = Agl.RetDate(DateAdd(DateInterval.Day, -1, CDate(Agl.PubLoginDate))) Then
                mFilter = "Yesterday"
            ElseIf FGMain.Item(GFilter, CInt(sender.Tag)).Value = Agl.RetMonthStartDate(Agl.PubLoginDate) Then
                mFilter = "Month Start Date"
            ElseIf FGMain.Item(GFilter, CInt(sender.Tag)).Value = Agl.RetMonthEndDate(Agl.PubLoginDate) Then
                mFilter = "Month End Date"
            ElseIf FGMain.Item(GFilter, CInt(sender.Tag)).Value = Agl.RetMonthStartDate(DateAdd(DateInterval.Month, -1, CDate(Agl.PubLoginDate))) Then
                mFilter = "Last Month Start Date"
            ElseIf FGMain.Item(GFilter, CInt(sender.Tag)).Value = Agl.RetMonthEndDate(DateAdd(DateInterval.Month, -1, CDate(Agl.PubLoginDate))) Then
                mFilter = "Last Month End Date"
            ElseIf FGMain.Item(GFilter, CInt(sender.Tag)).Value = Agl.PubStartDate Then
                mFilter = "Year Start Date"
            ElseIf FGMain.Item(GFilter, CInt(sender.Tag)).Value = Agl.PubEndDate Then
                mFilter = "Year End Date"
            End If
        End If
        mQry = "Insert Into ReportFilterDefaultValues (MenuText, User_Name, Head, Value, ValueCode, EntryBy, EntryDate) 
               Values('" & Me.Text & "', '" & Agl.PubUserName & "', '" & FGMain.Item("FieldName", CInt(sender.Tag)).Value & "', '" & mFilter & "', '" & Replace(FGMain.Item("FilterCode", CInt(sender.Tag)).Value, "'", "`") & "', '" & Agl.PubUserName & "', " & Agl.Chk_Date(Agl.PubLoginDate) & ") "
        Agl.Dman_ExecuteNonQry(mQry, Agl.GCn)
    End Sub
End Class
