Imports System.Data.SQLite
Public Class FrmVoucherTypeSettingsPrint
    Inherits AgTemplate.TempMaster
    Public Const Yes As String = "Yes"
    Public Const No As String = "No"
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Protected Const ColSNo As String = "S.No."
    Protected Const Col1SubReportName As String = "Sub Report Name"
    Protected Const Col1SubReportQuery As String = "Sub Report Query"
    Protected Const Col1SubReportQueryExecute As String = "Execute"

    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents TxtSiteCode As AgControls.AgTextBox
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents LblEntryTypeReq As System.Windows.Forms.Label
    Friend WithEvents TxtVoucherType As AgControls.AgTextBox
    Friend WithEvents LblEntryType As System.Windows.Forms.Label
    Protected WithEvents BtnCopyToAllDiv As System.Windows.Forms.Button
    Protected WithEvents BtnCopyToAllSite As System.Windows.Forms.Button
    Protected WithEvents BtnExcuteQuery As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents TC1 As System.Windows.Forms.TabControl
    Friend WithEvents TP1 As System.Windows.Forms.TabPage
    Friend WithEvents TP2 As System.Windows.Forms.TabPage
    Friend WithEvents TxtQuery As AgControls.AgTextBox
    Friend WithEvents Label59 As System.Windows.Forms.Label
    Friend WithEvents TxtReport_Heading As AgControls.AgTextBox
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents TxtReport_Name As AgControls.AgTextBox
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents TxtReport_Format As AgControls.AgTextBox
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents TxtReport_HeadingUnapproved As AgControls.AgTextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents TxtSubReport_QueryList As AgControls.AgTextBox
    Friend WithEvents TxtSubReport_NameList As AgControls.AgTextBox
    Dim mQry$

    Public Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Topctrl1.FSetParent(Me, StrUPVar, DTUP)
        Topctrl1.SetDisp(True)
    End Sub

    Private Sub FrmVoucher_Type_Print_SettingsMaster_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        If AgL.RequiredField(TxtVoucherType, LblEntryType.Text) Then passed = False : Exit Sub
        TxtSubReport_NameList.Text = ""
        TxtSubReport_QueryList.Text = ""

        Dim I As Integer
        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1SubReportName, I).Value <> "" Then
                If TxtSubReport_NameList.Text = "" Then
                    TxtSubReport_NameList.Text = Dgl1.Item(Col1SubReportName, I).Value
                Else
                    TxtSubReport_NameList.Text = TxtSubReport_NameList.Text & "|" & Dgl1.Item(Col1SubReportName, I).Value
                End If

                If Dgl1.Item(Col1SubReportQuery, I).Value = "" Then
                    MsgBox(Col1SubReportQuery & " Is Blank At Row No " & Dgl1.Item(ColSNo, I).Value & "")
                    Dgl1.CurrentCell = Dgl1.Item(Col1SubReportQuery, I) : Dgl1.Focus()
                    TxtSubReport_NameList.Text = ""
                    TxtSubReport_QueryList.Text = ""
                    passed = False
                    Exit Sub
                End If

                If TxtSubReport_QueryList.Text = "" Then
                    TxtSubReport_QueryList.Text = Dgl1.Item(Col1SubReportQuery, I).Value
                Else
                    TxtSubReport_QueryList.Text = TxtSubReport_QueryList.Text & "|" & Dgl1.Item(Col1SubReportQuery, I).Value
                End If
            End If
        Next

        If Topctrl1.Mode = "Add" Then
            mQry = "Select count(*) From Voucher_Type_Settings Where V_Type='" & TxtVoucherType.AgSelectedValue & "' And Div_Code = '" & AgL.PubDivCode & "' And Site_Code ='" & AgL.PubSiteCode & "' "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then passed = False : MsgBox("Entry Type Already Exists")
        Else
            mQry = "Select count(*) From Voucher_Type_Settings Where V_Type='" & TxtVoucherType.AgSelectedValue & "' And Div_Code = '" & AgL.PubDivCode & "' And Site_Code ='" & AgL.PubSiteCode & "' And Code <> '" & mInternalCode & "' "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then passed = False : MsgBox("Entry Type Already Exists")
        End If
    End Sub

    Private Sub FrmVoucher_Type_Print_SettingsMaster_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mConStr$ = ""
        mConStr = " WHERE 1=1 AND H.Div_Code = '" & AgL.PubDivCode & "' And H.Site_Code = '" & AgL.PubSiteCode & "' AND IfNull(H.IsDeleted,0) = 0"
        AgL.PubFindQry = " SELECT H.Code, H.V_Type, Vt.Description AS [V Type], SM.Name AS SiteName, D.Div_Name " &
                        " FROM Voucher_Type_Settings H  " &
                        " LEFT JOIN Voucher_Type Vt ON Vt.V_Type = H.V_Type  " &
                        " LEFT JOIN SiteMast SM ON SM.Code = H.Site_Code  " &
                        " LEFT JOIN Division D ON D.Div_Code = H.Div_Code " &
                        " " & mConStr & " "
        AgL.PubFindQryOrdBy = "[V Type]"
    End Sub

    Private Sub FrmVoucher_Type_Print_SettingsMaster_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "Voucher_Type_Settings"
        LogTableName = "Voucher_Type_Settings_Log"
    End Sub

    Private Sub FrmVoucher_Type_Print_SettingsMaster_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As SQLiteConnection, ByVal Cmd As SQLiteCommand) Handles Me.BaseEvent_Save_InTrans
        mQry = " UPDATE Voucher_Type_Settings " &
                " SET V_Type = " & AgL.Chk_Text(TxtVoucherType.Tag) & " , " &
                " EntryBy = '" & AgL.PubUserName & "', " &
                " EntryDate = '" & AgL.PubLoginDate & "', " &
                " ApproveBy =  '" & AgL.PubUserName & "', " &
                " ApproveDate = '" & AgL.PubLoginDate & "', " &
                " Site_Code = '" & TxtSiteCode.Tag & "', " &
                " Div_Code = '" & TxtDivision.Tag & "', " &
                " Query = '" & TxtQuery.Text & "', " &
                " Report_Name = '" & TxtReport_Name.Text & "', " &
                " Report_Format = '" & TxtReport_Format.Text & "', " &
                " Report_HeadingUnapproved = '" & TxtReport_HeadingUnapproved.Text & "', " &
                " SubReport_NameList = '" & TxtSubReport_NameList.Text & "', " &
                " SubReport_QueryList = '" & TxtSubReport_QueryList.Text & "', " &
                " Report_Heading = '" & TxtReport_Heading.Text & "' " &
                " Where Code = '" & SearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
    End Sub

    Private Sub FrmVoucherTypeSettingsPrint_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        Dgl1.Rows.Clear()
    End Sub

    Private Sub FrmVoucher_Type_Print_SettingsMaster_BaseFunction_DispText() Handles Me.BaseFunction_DispText
        If AgL.StrCmp(Topctrl1.Mode, "Edit") Then
            TxtVoucherType.Enabled = False
        End If
        TxtSiteCode.Enabled = False
    End Sub

    Private Sub FrmVoucher_Type_Print_SettingsMaster_BaseFunction_FIniMast(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMast
        Dim mConStr$ = ""
        mConStr = "WHERE 1=1 And Div_Code = '" & AgL.PubDivCode & "' And Site_Code = '" & AgL.PubSiteCode & "'  AND IfNull(IsDeleted,0) = 0 "
        mQry = "Select Code As SearchCode " &
            " From Voucher_Type_Settings " & mConStr &
            " Order By V_Type "
        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmVoucher_Type_Print_SettingsMaster_BaseFunction_FIniMastLog(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMastLog
        Dim mConStr$ = ""
        mConStr = "WHERE 1=1 " & AgL.RetDivisionCondition(AgL, "Div_Code") & ""
        mQry = "Select UID As SearchCode " &
               " From Voucher_Type_Settings_log " & mConStr &
               " And EntryStatus='" & LogStatus.LogOpen & "' Order By Description"

        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmVoucherTypeSettingsPrint_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl1, Col1SubReportName, 300, 0, Col1SubReportName, True, False)
            .AddAgTextColumn(Dgl1, Col1SubReportQuery, 2000, 0, Col1SubReportQuery, True, False)
            .AddAgButtonColumn(Dgl1, Col1SubReportQueryExecute, 60, Col1SubReportQueryExecute, True)
        End With
        AgL.AddAgDataGrid(Dgl1, Panel1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 35
        Dgl1.ColumnHeadersDefaultCellStyle.Font = New Font(New FontFamily("Courier New"), 10)
        Dgl1.DefaultCellStyle.Font = New Font(New FontFamily("Courier New"), 10)
        Dgl1.EnableHeadersVisualStyles = False

    End Sub

    Private Sub FrmVoucher_Type_Print_SettingsMaster_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim DsTemp As DataSet
        mQry = " SELECT H.* , Vt.Description AS VtDesc, SM.Name AS SiteName  " &
            " FROM Voucher_Type_Settings H " &
            " LEFT JOIN Voucher_Type Vt ON Vt.V_Type = H.V_Type  " &
            " LEFT JOIN SiteMast SM ON SM.Code = H.Site_Code " &
            " Where H.Code='" & SearchCode & "'"
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                mInternalCode = AgL.XNull(.Rows(0)("Code"))
                TxtVoucherType.Tag = AgL.XNull(.Rows(0)("V_Type"))
                TxtVoucherType.Text = AgL.XNull(.Rows(0)("VtDesc"))
                TxtSiteCode.Tag = AgL.XNull(.Rows(0)("Site_Code"))
                TxtSiteCode.Text = AgL.XNull(.Rows(0)("SiteName"))
                TxtQuery.Text = AgL.XNull(.Rows(0)("Query"))
                TxtReport_Name.Text = AgL.XNull(.Rows(0)("Report_Name"))
                TxtReport_Heading.Text = AgL.XNull(.Rows(0)("Report_Heading"))
                TxtReport_Format.Text = AgL.XNull(.Rows(0)("Report_Format"))
                TxtSubReport_QueryList.Text = AgL.XNull(.Rows(0)("SubReport_QueryList"))
                TxtSubReport_NameList.Text = AgL.XNull(.Rows(0)("SubReport_NameList"))
                TxtReport_HeadingUnapproved.Text = AgL.XNull(.Rows(0)("Report_HeadingUnapproved"))
                MoveRec_Grid()
            End If
        End With
        Topctrl1.tPrn = False
    End Sub
    Private Sub MoveRec_Grid()
        Dgl1.RowCount = 1
        Dgl1.Rows.Clear()
        Dim I As Integer
        Dim strArr() As String
        Dim strArr1() As String
        If TxtSubReport_NameList.Text <> "" Then
            strArr = Split(TxtSubReport_NameList.Text, "|")
            strArr1 = Split(TxtSubReport_QueryList.Text, "|")
            For I = 0 To strArr.Length - 1
                Dgl1.Rows.Add()
                Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1
                Dgl1.Item(Col1SubReportName, I).Value = strArr(I).ToString
                Dgl1.Item(Col1SubReportQuery, I).Value = strArr1(I).ToString
            Next
        End If
    End Sub
    Private Sub FrmVoucher_Type_Print_SettingsMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        AgL.WinSetting(Me, 660, 992, 0, 0)
        AgL.GridDesign(Dgl1)
    End Sub

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub

    Private Sub Topctrl1_tbAdd() Handles Topctrl1.tbAdd
        TxtVoucherType.Focus()
        TxtSiteCode.Tag = AgL.PubSiteCode
        TxtSiteCode.Text = AgL.Dman_Execute("SELECT Name FROM SiteMast Where Code = '" & TxtSiteCode.Tag & "'", AgL.GcnRead).ExecuteScalar
    End Sub

    Private Sub Topctrl1_tbEdit() Handles Topctrl1.tbEdit
        'TxtIsMandatory_SubCode.Focus()
    End Sub

    Private Sub InitializeComponent()
        Me.Label26 = New System.Windows.Forms.Label
        Me.TxtSiteCode = New AgControls.AgTextBox
        Me.Label27 = New System.Windows.Forms.Label
        Me.LblEntryTypeReq = New System.Windows.Forms.Label
        Me.TxtVoucherType = New AgControls.AgTextBox
        Me.LblEntryType = New System.Windows.Forms.Label
        Me.BtnCopyToAllDiv = New System.Windows.Forms.Button
        Me.BtnCopyToAllSite = New System.Windows.Forms.Button
        Me.BtnExcuteQuery = New System.Windows.Forms.Button
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.TC1 = New System.Windows.Forms.TabControl
        Me.TP1 = New System.Windows.Forms.TabPage
        Me.TxtSubReport_QueryList = New AgControls.AgTextBox
        Me.TxtSubReport_NameList = New AgControls.AgTextBox
        Me.TxtQuery = New AgControls.AgTextBox
        Me.Label59 = New System.Windows.Forms.Label
        Me.TxtReport_Heading = New AgControls.AgTextBox
        Me.Label37 = New System.Windows.Forms.Label
        Me.TxtReport_Name = New AgControls.AgTextBox
        Me.Label33 = New System.Windows.Forms.Label
        Me.TxtReport_Format = New AgControls.AgTextBox
        Me.Label31 = New System.Windows.Forms.Label
        Me.TxtReport_HeadingUnapproved = New AgControls.AgTextBox
        Me.Label16 = New System.Windows.Forms.Label
        Me.TP2 = New System.Windows.Forms.TabPage
        Me.GrpUP.SuspendLayout()
        Me.GBoxEntryType.SuspendLayout()
        Me.GBoxMoveToLog.SuspendLayout()
        Me.GBoxApprove.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GBoxDivision.SuspendLayout()
        CType(Me.DTMaster, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TC1.SuspendLayout()
        Me.TP1.SuspendLayout()
        Me.TP2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Topctrl1
        '
        Me.Topctrl1.Size = New System.Drawing.Size(986, 41)
        '
        'GroupBox1
        '
        Me.GroupBox1.Location = New System.Drawing.Point(0, 573)
        Me.GroupBox1.Size = New System.Drawing.Size(988, 10)
        '
        'GrpUP
        '
        Me.GrpUP.Location = New System.Drawing.Point(20, 583)
        '
        'TxtEntryBy
        '
        Me.TxtEntryBy.Tag = ""
        Me.TxtEntryBy.Text = ""
        '
        'GBoxEntryType
        '
        Me.GBoxEntryType.Location = New System.Drawing.Point(287, 583)
        Me.GBoxEntryType.Size = New System.Drawing.Size(121, 44)
        '
        'TxtEntryType
        '
        Me.TxtEntryType.Size = New System.Drawing.Size(115, 18)
        Me.TxtEntryType.Tag = ""
        '
        'GBoxMoveToLog
        '
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(580, 588)
        Me.GBoxMoveToLog.Size = New System.Drawing.Size(121, 44)
        '
        'TxtMoveToLog
        '
        Me.TxtMoveToLog.Size = New System.Drawing.Size(89, 18)
        Me.TxtMoveToLog.Tag = ""
        '
        'GBoxApprove
        '
        Me.GBoxApprove.Location = New System.Drawing.Point(436, 588)
        Me.GBoxApprove.Size = New System.Drawing.Size(121, 44)
        Me.GBoxApprove.Text = "Approved By"
        '
        'TxtApproveBy
        '
        Me.TxtApproveBy.Location = New System.Drawing.Point(3, 23)
        Me.TxtApproveBy.Size = New System.Drawing.Size(115, 18)
        Me.TxtApproveBy.Tag = ""
        '
        'CmdDiscard
        '
        Me.CmdDiscard.Location = New System.Drawing.Point(92, 18)
        '
        'GroupBox2
        '
        Me.GroupBox2.Location = New System.Drawing.Point(822, 583)
        Me.GroupBox2.Size = New System.Drawing.Size(121, 44)
        '
        'GBoxDivision
        '
        Me.GBoxDivision.Location = New System.Drawing.Point(557, 583)
        Me.GBoxDivision.Size = New System.Drawing.Size(121, 44)
        '
        'TxtDivision
        '
        Me.TxtDivision.AgSelectedValue = ""
        Me.TxtDivision.Size = New System.Drawing.Size(115, 18)
        Me.TxtDivision.Tag = ""
        '
        'TxtStatus
        '
        Me.TxtStatus.AgSelectedValue = ""
        Me.TxtStatus.Size = New System.Drawing.Size(89, 18)
        Me.TxtStatus.Tag = ""
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label26.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label26.Location = New System.Drawing.Point(491, 67)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(10, 7)
        Me.Label26.TabIndex = 730
        Me.Label26.Text = "Ä"
        '
        'TxtSiteCode
        '
        Me.TxtSiteCode.AgAllowUserToEnableMasterHelp = False
        Me.TxtSiteCode.AgLastValueTag = Nothing
        Me.TxtSiteCode.AgLastValueText = Nothing
        Me.TxtSiteCode.AgMandatory = True
        Me.TxtSiteCode.AgMasterHelp = False
        Me.TxtSiteCode.AgNumberLeftPlaces = 0
        Me.TxtSiteCode.AgNumberNegetiveAllow = False
        Me.TxtSiteCode.AgNumberRightPlaces = 0
        Me.TxtSiteCode.AgPickFromLastValue = False
        Me.TxtSiteCode.AgRowFilter = ""
        Me.TxtSiteCode.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtSiteCode.AgSelectedValue = Nothing
        Me.TxtSiteCode.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtSiteCode.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtSiteCode.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtSiteCode.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSiteCode.Location = New System.Drawing.Point(507, 62)
        Me.TxtSiteCode.MaxLength = 0
        Me.TxtSiteCode.Name = "TxtSiteCode"
        Me.TxtSiteCode.Size = New System.Drawing.Size(288, 18)
        Me.TxtSiteCode.TabIndex = 2
        Me.TxtSiteCode.Text = "TxtSiteCode"
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.Location = New System.Drawing.Point(393, 62)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(76, 16)
        Me.Label27.TabIndex = 729
        Me.Label27.Text = "Site/Branch"
        '
        'LblEntryTypeReq
        '
        Me.LblEntryTypeReq.AutoSize = True
        Me.LblEntryTypeReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblEntryTypeReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblEntryTypeReq.Location = New System.Drawing.Point(138, 67)
        Me.LblEntryTypeReq.Name = "LblEntryTypeReq"
        Me.LblEntryTypeReq.Size = New System.Drawing.Size(10, 7)
        Me.LblEntryTypeReq.TabIndex = 788
        Me.LblEntryTypeReq.Text = "Ä"
        '
        'TxtVoucherType
        '
        Me.TxtVoucherType.AgAllowUserToEnableMasterHelp = False
        Me.TxtVoucherType.AgLastValueTag = Nothing
        Me.TxtVoucherType.AgLastValueText = Nothing
        Me.TxtVoucherType.AgMandatory = True
        Me.TxtVoucherType.AgMasterHelp = False
        Me.TxtVoucherType.AgNumberLeftPlaces = 0
        Me.TxtVoucherType.AgNumberNegetiveAllow = False
        Me.TxtVoucherType.AgNumberRightPlaces = 0
        Me.TxtVoucherType.AgPickFromLastValue = False
        Me.TxtVoucherType.AgRowFilter = ""
        Me.TxtVoucherType.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtVoucherType.AgSelectedValue = Nothing
        Me.TxtVoucherType.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtVoucherType.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtVoucherType.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtVoucherType.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtVoucherType.Location = New System.Drawing.Point(154, 62)
        Me.TxtVoucherType.MaxLength = 0
        Me.TxtVoucherType.Name = "TxtVoucherType"
        Me.TxtVoucherType.Size = New System.Drawing.Size(180, 18)
        Me.TxtVoucherType.TabIndex = 1
        Me.TxtVoucherType.Text = "TxtVoucherType"
        '
        'LblEntryType
        '
        Me.LblEntryType.AutoSize = True
        Me.LblEntryType.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblEntryType.Location = New System.Drawing.Point(11, 62)
        Me.LblEntryType.Name = "LblEntryType"
        Me.LblEntryType.Size = New System.Drawing.Size(70, 16)
        Me.LblEntryType.TabIndex = 787
        Me.LblEntryType.Text = "Entry Type"
        '
        'BtnCopyToAllDiv
        '
        Me.BtnCopyToAllDiv.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnCopyToAllDiv.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnCopyToAllDiv.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnCopyToAllDiv.Location = New System.Drawing.Point(834, 69)
        Me.BtnCopyToAllDiv.Margin = New System.Windows.Forms.Padding(0)
        Me.BtnCopyToAllDiv.Name = "BtnCopyToAllDiv"
        Me.BtnCopyToAllDiv.Size = New System.Drawing.Size(148, 25)
        Me.BtnCopyToAllDiv.TabIndex = 789
        Me.BtnCopyToAllDiv.TabStop = False
        Me.BtnCopyToAllDiv.Text = "Copy To All Division"
        Me.BtnCopyToAllDiv.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnCopyToAllDiv.UseVisualStyleBackColor = True
        '
        'BtnCopyToAllSite
        '
        Me.BtnCopyToAllSite.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnCopyToAllSite.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnCopyToAllSite.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnCopyToAllSite.Location = New System.Drawing.Point(834, 43)
        Me.BtnCopyToAllSite.Margin = New System.Windows.Forms.Padding(0)
        Me.BtnCopyToAllSite.Name = "BtnCopyToAllSite"
        Me.BtnCopyToAllSite.Size = New System.Drawing.Size(148, 25)
        Me.BtnCopyToAllSite.TabIndex = 790
        Me.BtnCopyToAllSite.TabStop = False
        Me.BtnCopyToAllSite.Text = "Copy To All Site"
        Me.BtnCopyToAllSite.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnCopyToAllSite.UseVisualStyleBackColor = True
        '
        'BtnExcuteQuery
        '
        Me.BtnExcuteQuery.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnExcuteQuery.Font = New System.Drawing.Font("Lucida Console", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnExcuteQuery.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnExcuteQuery.Location = New System.Drawing.Point(111, 49)
        Me.BtnExcuteQuery.Margin = New System.Windows.Forms.Padding(0)
        Me.BtnExcuteQuery.Name = "BtnExcuteQuery"
        Me.BtnExcuteQuery.Size = New System.Drawing.Size(74, 19)
        Me.BtnExcuteQuery.TabIndex = 922
        Me.BtnExcuteQuery.TabStop = False
        Me.BtnExcuteQuery.Text = "Execute"
        Me.BtnExcuteQuery.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnExcuteQuery.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Location = New System.Drawing.Point(9, 8)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(960, 448)
        Me.Panel1.TabIndex = 923
        '
        'TC1
        '
        Me.TC1.Controls.Add(Me.TP1)
        Me.TC1.Controls.Add(Me.TP2)
        Me.TC1.Location = New System.Drawing.Point(5, 83)
        Me.TC1.Name = "TC1"
        Me.TC1.SelectedIndex = 0
        Me.TC1.Size = New System.Drawing.Size(986, 490)
        Me.TC1.TabIndex = 3
        '
        'TP1
        '
        Me.TP1.BackColor = System.Drawing.Color.Gainsboro
        Me.TP1.Controls.Add(Me.TxtSubReport_QueryList)
        Me.TP1.Controls.Add(Me.TxtSubReport_NameList)
        Me.TP1.Controls.Add(Me.TxtQuery)
        Me.TP1.Controls.Add(Me.BtnExcuteQuery)
        Me.TP1.Controls.Add(Me.Label59)
        Me.TP1.Controls.Add(Me.TxtReport_Heading)
        Me.TP1.Controls.Add(Me.Label37)
        Me.TP1.Controls.Add(Me.TxtReport_Name)
        Me.TP1.Controls.Add(Me.Label33)
        Me.TP1.Controls.Add(Me.TxtReport_Format)
        Me.TP1.Controls.Add(Me.Label31)
        Me.TP1.Controls.Add(Me.TxtReport_HeadingUnapproved)
        Me.TP1.Controls.Add(Me.Label16)
        Me.TP1.Location = New System.Drawing.Point(4, 22)
        Me.TP1.Name = "TP1"
        Me.TP1.Padding = New System.Windows.Forms.Padding(3)
        Me.TP1.Size = New System.Drawing.Size(978, 464)
        Me.TP1.TabIndex = 0
        Me.TP1.Text = "Common"
        '
        'TxtSubReport_QueryList
        '
        Me.TxtSubReport_QueryList.AgAllowUserToEnableMasterHelp = False
        Me.TxtSubReport_QueryList.AgLastValueTag = Nothing
        Me.TxtSubReport_QueryList.AgLastValueText = Nothing
        Me.TxtSubReport_QueryList.AgMandatory = False
        Me.TxtSubReport_QueryList.AgMasterHelp = False
        Me.TxtSubReport_QueryList.AgNumberLeftPlaces = 0
        Me.TxtSubReport_QueryList.AgNumberNegetiveAllow = False
        Me.TxtSubReport_QueryList.AgNumberRightPlaces = 0
        Me.TxtSubReport_QueryList.AgPickFromLastValue = False
        Me.TxtSubReport_QueryList.AgRowFilter = ""
        Me.TxtSubReport_QueryList.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtSubReport_QueryList.AgSelectedValue = Nothing
        Me.TxtSubReport_QueryList.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtSubReport_QueryList.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtSubReport_QueryList.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtSubReport_QueryList.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSubReport_QueryList.Location = New System.Drawing.Point(676, 50)
        Me.TxtSubReport_QueryList.MaxLength = 0
        Me.TxtSubReport_QueryList.Name = "TxtSubReport_QueryList"
        Me.TxtSubReport_QueryList.Size = New System.Drawing.Size(187, 18)
        Me.TxtSubReport_QueryList.TabIndex = 934
        Me.TxtSubReport_QueryList.Text = "TxtSubReport_QryList"
        Me.TxtSubReport_QueryList.Visible = False
        '
        'TxtSubReport_NameList
        '
        Me.TxtSubReport_NameList.AgAllowUserToEnableMasterHelp = False
        Me.TxtSubReport_NameList.AgLastValueTag = Nothing
        Me.TxtSubReport_NameList.AgLastValueText = Nothing
        Me.TxtSubReport_NameList.AgMandatory = False
        Me.TxtSubReport_NameList.AgMasterHelp = False
        Me.TxtSubReport_NameList.AgNumberLeftPlaces = 0
        Me.TxtSubReport_NameList.AgNumberNegetiveAllow = False
        Me.TxtSubReport_NameList.AgNumberRightPlaces = 0
        Me.TxtSubReport_NameList.AgPickFromLastValue = False
        Me.TxtSubReport_NameList.AgRowFilter = ""
        Me.TxtSubReport_NameList.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtSubReport_NameList.AgSelectedValue = Nothing
        Me.TxtSubReport_NameList.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtSubReport_NameList.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtSubReport_NameList.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtSubReport_NameList.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSubReport_NameList.Location = New System.Drawing.Point(387, 51)
        Me.TxtSubReport_NameList.MaxLength = 0
        Me.TxtSubReport_NameList.Name = "TxtSubReport_NameList"
        Me.TxtSubReport_NameList.Size = New System.Drawing.Size(99, 18)
        Me.TxtSubReport_NameList.TabIndex = 933
        Me.TxtSubReport_NameList.Text = "TxtSubReport_List"
        Me.TxtSubReport_NameList.Visible = False
        '
        'TxtQuery
        '
        Me.TxtQuery.AgAllowUserToEnableMasterHelp = False
        Me.TxtQuery.AgLastValueTag = Nothing
        Me.TxtQuery.AgLastValueText = Nothing
        Me.TxtQuery.AgMandatory = False
        Me.TxtQuery.AgMasterHelp = False
        Me.TxtQuery.AgNumberLeftPlaces = 0
        Me.TxtQuery.AgNumberNegetiveAllow = False
        Me.TxtQuery.AgNumberRightPlaces = 0
        Me.TxtQuery.AgPickFromLastValue = False
        Me.TxtQuery.AgRowFilter = ""
        Me.TxtQuery.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtQuery.AgSelectedValue = Nothing
        Me.TxtQuery.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtQuery.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtQuery.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtQuery.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtQuery.Location = New System.Drawing.Point(10, 74)
        Me.TxtQuery.MaxLength = 0
        Me.TxtQuery.Multiline = True
        Me.TxtQuery.Name = "TxtQuery"
        Me.TxtQuery.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.TxtQuery.Size = New System.Drawing.Size(959, 384)
        Me.TxtQuery.TabIndex = 4
        Me.TxtQuery.Text = "TxtQuery"
        '
        'Label59
        '
        Me.Label59.AutoSize = True
        Me.Label59.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label59.Location = New System.Drawing.Point(10, 48)
        Me.Label59.Name = "Label59"
        Me.Label59.Size = New System.Drawing.Size(43, 16)
        Me.Label59.TabIndex = 932
        Me.Label59.Text = "Query"
        '
        'TxtReport_Heading
        '
        Me.TxtReport_Heading.AgAllowUserToEnableMasterHelp = False
        Me.TxtReport_Heading.AgLastValueTag = Nothing
        Me.TxtReport_Heading.AgLastValueText = Nothing
        Me.TxtReport_Heading.AgMandatory = False
        Me.TxtReport_Heading.AgMasterHelp = False
        Me.TxtReport_Heading.AgNumberLeftPlaces = 0
        Me.TxtReport_Heading.AgNumberNegetiveAllow = False
        Me.TxtReport_Heading.AgNumberRightPlaces = 0
        Me.TxtReport_Heading.AgPickFromLastValue = False
        Me.TxtReport_Heading.AgRowFilter = ""
        Me.TxtReport_Heading.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtReport_Heading.AgSelectedValue = Nothing
        Me.TxtReport_Heading.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtReport_Heading.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtReport_Heading.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtReport_Heading.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtReport_Heading.Location = New System.Drawing.Point(676, 10)
        Me.TxtReport_Heading.MaxLength = 0
        Me.TxtReport_Heading.Name = "TxtReport_Heading"
        Me.TxtReport_Heading.Size = New System.Drawing.Size(293, 18)
        Me.TxtReport_Heading.TabIndex = 1
        Me.TxtReport_Heading.Text = "TxtReport_Heading"
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label37.Location = New System.Drawing.Point(501, 10)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(97, 16)
        Me.Label37.TabIndex = 931
        Me.Label37.Text = "Report Heading"
        '
        'TxtReport_Name
        '
        Me.TxtReport_Name.AgAllowUserToEnableMasterHelp = False
        Me.TxtReport_Name.AgLastValueTag = Nothing
        Me.TxtReport_Name.AgLastValueText = "TxtReport_Name"
        Me.TxtReport_Name.AgMandatory = False
        Me.TxtReport_Name.AgMasterHelp = False
        Me.TxtReport_Name.AgNumberLeftPlaces = 0
        Me.TxtReport_Name.AgNumberNegetiveAllow = False
        Me.TxtReport_Name.AgNumberRightPlaces = 0
        Me.TxtReport_Name.AgPickFromLastValue = False
        Me.TxtReport_Name.AgRowFilter = ""
        Me.TxtReport_Name.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtReport_Name.AgSelectedValue = Nothing
        Me.TxtReport_Name.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtReport_Name.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtReport_Name.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtReport_Name.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtReport_Name.Location = New System.Drawing.Point(112, 10)
        Me.TxtReport_Name.MaxLength = 0
        Me.TxtReport_Name.Name = "TxtReport_Name"
        Me.TxtReport_Name.Size = New System.Drawing.Size(374, 18)
        Me.TxtReport_Name.TabIndex = 0
        Me.TxtReport_Name.Text = "TxtReport_Name"
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.Location = New System.Drawing.Point(10, 10)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(84, 16)
        Me.Label33.TabIndex = 930
        Me.Label33.Text = "Report Name"
        '
        'TxtReport_Format
        '
        Me.TxtReport_Format.AgAllowUserToEnableMasterHelp = False
        Me.TxtReport_Format.AgLastValueTag = Nothing
        Me.TxtReport_Format.AgLastValueText = Nothing
        Me.TxtReport_Format.AgMandatory = False
        Me.TxtReport_Format.AgMasterHelp = False
        Me.TxtReport_Format.AgNumberLeftPlaces = 0
        Me.TxtReport_Format.AgNumberNegetiveAllow = False
        Me.TxtReport_Format.AgNumberRightPlaces = 0
        Me.TxtReport_Format.AgPickFromLastValue = False
        Me.TxtReport_Format.AgRowFilter = ""
        Me.TxtReport_Format.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtReport_Format.AgSelectedValue = Nothing
        Me.TxtReport_Format.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtReport_Format.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtReport_Format.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtReport_Format.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtReport_Format.Location = New System.Drawing.Point(112, 29)
        Me.TxtReport_Format.MaxLength = 0
        Me.TxtReport_Format.Name = "TxtReport_Format"
        Me.TxtReport_Format.Size = New System.Drawing.Size(374, 18)
        Me.TxtReport_Format.TabIndex = 2
        Me.TxtReport_Format.Text = "TxtReport_Format"
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.Location = New System.Drawing.Point(10, 29)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(91, 16)
        Me.Label31.TabIndex = 928
        Me.Label31.Text = "Report Format"
        '
        'TxtReport_HeadingUnapproved
        '
        Me.TxtReport_HeadingUnapproved.AgAllowUserToEnableMasterHelp = False
        Me.TxtReport_HeadingUnapproved.AgLastValueTag = Nothing
        Me.TxtReport_HeadingUnapproved.AgLastValueText = "TxtReport_HeadingUnapproved"
        Me.TxtReport_HeadingUnapproved.AgMandatory = False
        Me.TxtReport_HeadingUnapproved.AgMasterHelp = False
        Me.TxtReport_HeadingUnapproved.AgNumberLeftPlaces = 0
        Me.TxtReport_HeadingUnapproved.AgNumberNegetiveAllow = False
        Me.TxtReport_HeadingUnapproved.AgNumberRightPlaces = 0
        Me.TxtReport_HeadingUnapproved.AgPickFromLastValue = False
        Me.TxtReport_HeadingUnapproved.AgRowFilter = ""
        Me.TxtReport_HeadingUnapproved.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtReport_HeadingUnapproved.AgSelectedValue = Nothing
        Me.TxtReport_HeadingUnapproved.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtReport_HeadingUnapproved.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtReport_HeadingUnapproved.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtReport_HeadingUnapproved.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtReport_HeadingUnapproved.Location = New System.Drawing.Point(676, 29)
        Me.TxtReport_HeadingUnapproved.MaxLength = 0
        Me.TxtReport_HeadingUnapproved.Name = "TxtReport_HeadingUnapproved"
        Me.TxtReport_HeadingUnapproved.Size = New System.Drawing.Size(293, 18)
        Me.TxtReport_HeadingUnapproved.TabIndex = 3
        Me.TxtReport_HeadingUnapproved.Text = "TxtReport_HeadingUnapproved"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(501, 29)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(168, 16)
        Me.Label16.TabIndex = 927
        Me.Label16.Text = "Report Heading Unapproved"
        '
        'TP2
        '
        Me.TP2.BackColor = System.Drawing.Color.Gainsboro
        Me.TP2.Controls.Add(Me.Panel1)
        Me.TP2.Location = New System.Drawing.Point(4, 22)
        Me.TP2.Name = "TP2"
        Me.TP2.Padding = New System.Windows.Forms.Padding(3)
        Me.TP2.Size = New System.Drawing.Size(978, 464)
        Me.TP2.TabIndex = 1
        Me.TP2.Text = "Sub Reports Detail"
        '
        'FrmVoucherTypeSettingsPrint
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(986, 632)
        Me.Controls.Add(Me.BtnCopyToAllSite)
        Me.Controls.Add(Me.BtnCopyToAllDiv)
        Me.Controls.Add(Me.TC1)
        Me.Controls.Add(Me.TxtSiteCode)
        Me.Controls.Add(Me.Label27)
        Me.Controls.Add(Me.Label26)
        Me.Controls.Add(Me.LblEntryType)
        Me.Controls.Add(Me.TxtVoucherType)
        Me.Controls.Add(Me.LblEntryTypeReq)
        Me.Name = "FrmVoucherTypeSettingsPrint"
        Me.Text = "Voucher Type Settings"
        Me.Controls.SetChildIndex(Me.LblEntryTypeReq, 0)
        Me.Controls.SetChildIndex(Me.TxtVoucherType, 0)
        Me.Controls.SetChildIndex(Me.LblEntryType, 0)
        Me.Controls.SetChildIndex(Me.Label26, 0)
        Me.Controls.SetChildIndex(Me.Label27, 0)
        Me.Controls.SetChildIndex(Me.TxtSiteCode, 0)
        Me.Controls.SetChildIndex(Me.Topctrl1, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.GrpUP, 0)
        Me.Controls.SetChildIndex(Me.GBoxEntryType, 0)
        Me.Controls.SetChildIndex(Me.GBoxApprove, 0)
        Me.Controls.SetChildIndex(Me.GBoxMoveToLog, 0)
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.GBoxDivision, 0)
        Me.Controls.SetChildIndex(Me.TC1, 0)
        Me.Controls.SetChildIndex(Me.BtnCopyToAllDiv, 0)
        Me.Controls.SetChildIndex(Me.BtnCopyToAllSite, 0)
        Me.GrpUP.ResumeLayout(False)
        Me.GrpUP.PerformLayout()
        Me.GBoxEntryType.ResumeLayout(False)
        Me.GBoxEntryType.PerformLayout()
        Me.GBoxMoveToLog.ResumeLayout(False)
        Me.GBoxMoveToLog.PerformLayout()
        Me.GBoxApprove.ResumeLayout(False)
        Me.GBoxApprove.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GBoxDivision.ResumeLayout(False)
        Me.GBoxDivision.PerformLayout()
        CType(Me.DTMaster, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TC1.ResumeLayout(False)
        Me.TP1.ResumeLayout(False)
        Me.TP1.PerformLayout()
        Me.TP2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Private Sub Txt_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtVoucherType.KeyDown, TxtSiteCode.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then Exit Sub
            If e.KeyCode = Keys.Delete Then sender.Tag = "" : sender.Text = "" : Exit Sub

            Select Case sender.Name
                Case TxtVoucherType.Name
                    If TxtVoucherType.AgHelpDataSet Is Nothing Then
                        mQry = " SELECT V_Type AS Code,Description AS [Entry Type], V_Type AS [Voucher Type] " &
                                " FROM Voucher_Type   " &
                                " Where IfNull(Description,'') <> '' " &
                                " Order By Description "
                        TxtVoucherType.AgHelpDataSet() = AgL.FillData(mQry, AgL.GCn)
                    End If

                Case TxtReport_Heading.Name
                    If TxtReport_Heading.AgHelpDataSet Is Nothing Then
                        mQry = " Select Code, Description FROM Godown " &
                                " Order By Description "
                        TxtReport_Heading.AgHelpDataSet() = AgL.FillData(mQry, AgL.GCn)
                    End If

                Case TxtSiteCode.Name
                    If TxtSiteCode.AgHelpDataSet Is Nothing Then
                        mQry = " SELECT H.Code, H.Name FROM SiteMast H " &
                                " Order By H.Name "
                        TxtSiteCode.AgHelpDataSet() = AgL.FillData(mQry, AgL.GCn)
                    End If

            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub BtnCopyToAllSite_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCopyToAllSite.Click
        If Topctrl1.Mode <> "Browse" Then Exit Sub
        If MsgBox("Are You Sure To Copy this for All Sites?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton1, AgLibrary.ClsMain.PubMsgTitleInfo) = vbYes Then
            ProcCopyToAllSite()
            MsgBox("Process is completed !")
        End If
    End Sub

    Private Sub ProcCopyToAllSite()
        Dim DsTemp As DataSet
        Dim mTrans As String = ""
        Dim I As Integer
        mQry = "SELECT Code FROM SiteMast WHERE Code <> '" & AgL.PubSiteCode & "'"
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        AgL.ECmd = AgL.GCn.CreateCommand
        AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
        AgL.ECmd.Transaction = AgL.ETrans
        mTrans = "Begin"

        Try
            With DsTemp.Tables(0)
                If .Rows.Count > 0 Then
                    For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                        mQry = " INSERT INTO Voucher_Type_Settings_Log " &
                                " SELECT * FROM Voucher_Type_Settings WHERE V_Type = '" & TxtVoucherType.Tag & "' AND Site_Code = '" & AgL.XNull(.Rows(I)("Code")) & "' AND Div_Code = '" & AgL.PubDivCode & "' "
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                        mQry = "Select count(*) From Voucher_Type_Settings  Where V_Type='" & TxtVoucherType.AgSelectedValue & "' And Div_Code = '" & AgL.PubDivCode & "' And Site_Code = '" & AgL.XNull(.Rows(I)("Code")) & "' "
                        If AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar <= 0 Then
                            mQry = " INSERT INTO Voucher_Type_Settings (Code, V_Type , EntryBy , EntryDate, ApproveBy ,ApproveDate , Site_Code, Div_Code ) " &
                                    " Values (" & AgL.Chk_Text(GetCode(AgL.XNull(.Rows(I)("Code")), AgL.PubDivCode)) & ", " & AgL.Chk_Text(TxtVoucherType.Tag) & ", " & AgL.Chk_Text(AgL.PubUserName) & ", " & AgL.Chk_Text(AgL.GetDateTime(AgL.GcnRead)) & ", " & AgL.Chk_Text(AgL.PubUserName) & ", " & AgL.Chk_Text(AgL.GetDateTime(AgL.GcnRead)) & ",'" & AgL.XNull(.Rows(I)("Code")) & "', " & AgL.Chk_Text(AgL.PubDivCode) & " ) "
                            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                        End If

                        mQry = "  Update Voucher_Type_Settings  " &
                                " SET Query = V1.query, " &
                                " Report_Name = V1.report_name, " &
                                " Report_Heading = V1.report_heading, " &
                                " Report_Format = V1.report_format, " &
                                " Report_HeadingUnapproved = V1.Report_HeadingUnapproved, " &
                                " SubReport_QueryList = V1.subreport_querylist, " &
                                " SubReport_NameList = V1.subreport_namelist " &
                                " FROM " &
                                " ( " &
                                " SELECT *    " &
                                " From Voucher_Type_Settings   " &
                                " Where V_TYpe = " & AgL.Chk_Text(TxtVoucherType.Tag) & " AND Div_Code = " & AgL.Chk_Text(AgL.PubDivCode) & " AND Site_Code =" & AgL.Chk_Text(AgL.PubSiteCode) & "  " &
                                " ) V1 WHERE V1.V_TYpe = Voucher_Type_Settings.V_Type  " &
                                " AND V1.Div_Code = Voucher_Type_Settings.Div_Code  " &
                                " AND voucher_type_settings.Site_Code =  '" & AgL.XNull(.Rows(I)("Code")) & "' "
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    Next
                End If
            End With

            AgL.ETrans.Commit()
            mTrans = "Commit"
        Catch ex As Exception
            If mTrans = "Begin" Then
                AgL.ETrans.Rollback()
            ElseIf mTrans = "Commit" Then
                Topctrl1.FButtonClick(14, True)
            End If
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ProcCopyToAllDivision()
        Dim DsTemp As DataSet
        Dim mTrans As String = ""
        Dim I As Integer
        mQry = "SELECT Div_Code AS Code FROM Division WHERE Div_Code <> '" & AgL.PubDivCode & "'"
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        AgL.ECmd = AgL.GCn.CreateCommand
        AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
        AgL.ECmd.Transaction = AgL.ETrans
        mTrans = "Begin"

        Try
            With DsTemp.Tables(0)
                If .Rows.Count > 0 Then
                    For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                        mQry = " INSERT INTO Voucher_Type_Settings_Log " &
                                " SELECT * FROM Voucher_Type_Settings WHERE V_Type = '" & TxtVoucherType.Tag & "' AND Div_Code = '" & AgL.XNull(.Rows(I)("Code")) & "' AND Site_Code = '" & AgL.PubSiteCode & "' "
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                        mQry = "Select count(*) From Voucher_Type_Settings  Where V_Type='" & TxtVoucherType.AgSelectedValue & "' And Site_Code = '" & AgL.PubSiteCode & "' And Div_Code = '" & AgL.XNull(.Rows(I)("Code")) & "' "
                        If AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar <= 0 Then
                            mQry = " INSERT INTO Voucher_Type_Settings (Code, V_Type , EntryBy , EntryDate, ApproveBy ,ApproveDate, Div_Code, Site_Code ) " &
                                    " Values (" & AgL.Chk_Text(GetCode(AgL.PubSiteCode, AgL.XNull(.Rows(I)("Code")))) & ", " & AgL.Chk_Text(TxtVoucherType.Tag) & ", " & AgL.Chk_Text(AgL.PubUserName) & ", " & AgL.Chk_Text(AgL.GetDateTime(AgL.GcnRead)) & ", " & AgL.Chk_Text(AgL.PubUserName) & ", " & AgL.Chk_Text(AgL.GetDateTime(AgL.GcnRead)) & ",'" & AgL.XNull(.Rows(I)("Code")) & "', " & AgL.Chk_Text(AgL.PubSiteCode) & " ) "
                            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                        End If

                        mQry = "  Update Voucher_Type_Settings  " &
                                " SET Query = V1.query, " &
                                " Report_Name = V1.report_name, " &
                                " Report_Heading = V1.report_heading, " &
                                " Report_Format = V1.report_format, " &
                                " Report_HeadingUnapproved = V1.Report_HeadingUnapproved, " &
                                " SubReport_QueryList = V1.subreport_querylist, " &
                                " SubReport_NameList = V1.subreport_namelist " &
                                " FROM " &
                                " ( " &
                                " SELECT *    " &
                                " From Voucher_Type_Settings   " &
                                " Where V_TYpe = " & AgL.Chk_Text(TxtVoucherType.Tag) & " AND Site_Code = " & AgL.Chk_Text(AgL.PubSiteCode) & " AND Div_Code = " & AgL.Chk_Text(AgL.PubDivCode) & "  " &
                                " ) V1 WHERE V1.V_TYpe = Voucher_Type_Settings.V_Type  " &
                                " AND V1.Site_Code = Voucher_Type_Settings.Site_Code  " &
                                " AND voucher_type_settings.Div_Code =  '" & AgL.XNull(.Rows(I)("Code")) & "' "
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    Next
                End If
            End With

            AgL.ETrans.Commit()
            mTrans = "Commit"
        Catch ex As Exception
            If mTrans = "Begin" Then
                AgL.ETrans.Rollback()
            ElseIf mTrans = "Commit" Then
                Topctrl1.FButtonClick(14, True)
            End If
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function GetCode(ByVal bSiteCode As String, ByVal bDivCode As String) As String
        GetCode = AgL.GetMaxId("Voucher_Type_Settings", "Code", AgL.GCn, bDivCode, bSiteCode, 4, True, True, , AgL.Gcn_ConnectionString)
    End Function

    Private Sub BtnCopyToAllDiv_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCopyToAllDiv.Click
        If Topctrl1.Mode <> "Browse" Then Exit Sub
        If MsgBox("Are You Sure To Copy this for All Division?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton1, AgLibrary.ClsMain.PubMsgTitleInfo) = vbYes Then
            ProcCopyToAllDivision()
            MsgBox("Process is completed !")
        End If
    End Sub

    Private Sub BtnExcuteQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnExcuteQuery.Click
        ExcuteQry(TxtQuery.Text)
    End Sub

    Private Sub ExcuteQry(ByVal StrQry As String)
        If StrQry = "" Then MsgBox("Query is Blank !") : Exit Sub

        Dim TempStr As String = ""
        Dim TempSearchCode As String = ""
        TempStr = StrQry
        If TempStr.Contains("``<SEARCHCODE>``") = True Then
            TempSearchCode = InputBox("SearchCode", "Fill SearchCode")
            TempStr = TempStr.Replace("``<SEARCHCODE>``", AgL.Chk_Text(TempSearchCode))
        End If

        Try
            Dim DtTemp As DataTable = Nothing
            mQry = TempStr
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
            Dim FrmObj = New FrmQueryResult(DtTemp)
            FrmObj.Show()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Dgl1_CellContentClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellContentClick
        Dim bRowIndex As Integer = 0
        bRowIndex = Dgl1.CurrentCell.RowIndex
        Select Case Dgl1.Columns(e.ColumnIndex).Name
            Case Col1SubReportQueryExecute
                ExcuteQry(Dgl1.Item(Col1SubReportQuery, bRowIndex).Value)
        End Select
    End Sub
    Private Sub Dgl1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.KeyDown
        If e.Control And e.KeyCode = Keys.D Then
            sender.CurrentRow.Selected = True
        End If
        If e.Control Or e.Shift Or e.Alt Then Exit Sub
    End Sub

    Private Sub Dgl1_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles Dgl1.RowsAdded
        sender(ColSNo, sender.Rows.Count - 1).Value = Trim(sender.Rows.Count)
    End Sub
End Class
