Public Class FrmVoucherTypePrintSettings
    Inherits AgTemplate.TempMaster
    Friend WithEvents LblEntryTypr As System.Windows.Forms.Label
    Friend WithEvents TxtVoucherType As AgControls.AgTextBox
    Friend WithEvents LblEntryTypeReq As System.Windows.Forms.Label
    Friend WithEvents LblTermCondition As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TxtReportName As AgControls.AgTextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TxtReportHeading As AgControls.AgTextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Protected WithEvents TxtQuery As AgControls.AgTextBox
    Dim mQry$

    Public Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Topctrl1.FSetParent(Me, StrUPVar, DTUP)
        Topctrl1.SetDisp(True)
    End Sub

    Private Sub FrmVoucher_Type_Print_SettingsMaster_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        If AgL.RequiredField(TxtVoucherType, LblEntryTypr.Text) Then passed = False : Exit Sub
        If AgL.RequiredField(TxtQuery, LblTermCondition.Text) Then passed = False : Exit Sub

        If Topctrl1.Mode = "Add" Then
            mQry = "Select count(*) From Voucher_Type_Print_Settings Where V_Type='" & TxtVoucherType.AgSelectedValue & "' And Div_Code = '" & AgL.PubDivCode & "' And Site_Code ='" & AgL.PubSiteCode & "' "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then passed = False : MsgBox("Entry Type Already Exists")
        Else
            mQry = "Select count(*) From Voucher_Type_Print_Settings Where V_Type='" & TxtVoucherType.AgSelectedValue & "' And Div_Code = '" & AgL.PubDivCode & "' And Site_Code ='" & AgL.PubSiteCode & "' And Code<>'" & mInternalCode & "' "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then passed = False : MsgBox("Entry Type Already Exists")
        End If
    End Sub


    Private Sub FrmVoucher_Type_Print_SettingsMaster_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mConStr$ = ""
        mConStr = "WHERE 1=1 " & AgL.RetDivisionCondition(AgL, "H.Div_Code") & " And H.Site_Code = '" & AgL.PubSiteCode & "' AND Isnull(H.IsDeleted,0) = 0"

        AgL.PubFindQry = " SELECT H.Code AS SearchCode, H.Query , Vt.Description As Voucher_Type, H.Report_Name, H.Report_Heading,  H.EntryBy AS [Entry By], H.EntryDate AS [Entry Date], H.EntryType AS [Entry Type], " & _
                        " H.ApproveBy AS [Approve By], H.ApproveDate AS [Approve Date], H.Status  " & _
                        " FROM  Voucher_Type_Print_Settings H " & _
                        " LEFT JOIN Voucher_Type Vt ON Vt.V_Type = H.V_Type " & mConStr & _
                        " And IsNull(H.IsDeleted,0)=0 "

        AgL.PubFindQryOrdBy = "[Voucher Type]"
    End Sub

    Private Sub FrmVoucher_Type_Print_SettingsMaster_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "Voucher_Type_Print_Settings"
        LogTableName = "Voucher_Type_Print_Settings_LOG"
    End Sub


    Private Sub FrmVoucher_Type_Print_SettingsMaster_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As System.Data.SqlClient.SqlConnection, ByVal Cmd As System.Data.SqlClient.SqlCommand) Handles Me.BaseEvent_Save_InTrans
        mQry = " Update Voucher_Type_Print_Settings " & _
                "   SET  " & _
                "	V_Type = " & AgL.Chk_Text(TxtVoucherType.AgSelectedValue) & " ," & _
                "	Site_Code = " & AgL.Chk_Text(AgL.PubSiteCode) & " ," & _
                "	Query = " & AgL.Chk_Text(TxtQuery.Text) & ", " & _
                "	Report_Name = " & AgL.Chk_Text(TxtReportName.Text) & ", " & _
                "	Report_Heading = " & AgL.Chk_Text(TxtReportHeading.Text) & " " & _
                "   Where Code = '" & SearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
    End Sub

    Private Sub FrmVoucher_Type_Print_SettingsMaster_BaseFunction_DispText() Handles Me.BaseFunction_DispText
        If AgL.StrCmp(Topctrl1.Mode, "Edit") Then TxtVoucherType.Enabled = False
    End Sub

    Private Sub FrmVoucher_Type_Print_SettingsMaster_BaseFunction_FIniList() Handles Me.BaseFunction_FIniList
        mQry = " SELECT V_Type AS Code,Description AS [Entry Type], V_Type AS [Voucher Type] " & _
               " FROM Voucher_Type   " & _
               " Where IsNull(Description,'') <> '' " & _
            " Order By Description"
        TxtVoucherType.AgHelpDataSet() = AgL.FillData(mQry, AgL.GCn)
    End Sub

    Private Sub FrmVoucher_Type_Print_SettingsMaster_BaseFunction_FIniMast(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMast
        Dim mConStr$ = ""
        mConStr = "WHERE 1=1 And Div_Code = '" & AgL.PubDivCode & "' And Site_Code = '" & AgL.PubSiteCode & "'  AND Isnull(IsDeleted,0) = 0 "
        mQry = "Select Code As SearchCode " & _
            " From Voucher_Type_Print_Settings " & mConStr & _
            " And IsNull(IsDeleted,0)=0 Order By V_Type "

        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmVoucher_Type_Print_SettingsMaster_BaseFunction_FIniMastLog(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMastLog
        Dim mConStr$ = ""
        mConStr = "WHERE 1=1 " & AgL.RetDivisionCondition(AgL, "Div_Code") & ""
        mQry = "Select UID As SearchCode " & _
               " From Voucher_Type_Print_Settings_log " & mConStr & _
               " And EntryStatus='" & LogStatus.LogOpen & "' Order By Description"

        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmVoucher_Type_Print_SettingsMaster_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim DsTemp As DataSet


        mQry = " Select T.*  " & _
            " From Voucher_Type_Print_Settings T " & _
            " Where T.Code='" & SearchCode & "'"
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                mInternalCode = AgL.XNull(.Rows(0)("Code"))
                TxtVoucherType.AgSelectedValue = AgL.XNull(.Rows(0)("V_Type"))
                TxtQuery.Text = AgL.XNull(.Rows(0)("Query"))
                TxtReportName.Text = AgL.XNull(.Rows(0)("Report_Name"))
                TxtReportHeading.Text = AgL.XNull(.Rows(0)("Report_Heading"))
            End If
        End With
        Topctrl1.tPrn = False
    End Sub

    Private Sub FrmVoucher_Type_Print_SettingsMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        AgL.WinSetting(Me, 400, 870, 0, 0)
    End Sub

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub

    Private Sub Topctrl1_tbAdd() Handles Topctrl1.tbAdd
        TxtVoucherType.Focus()
    End Sub

    Private Sub Topctrl1_tbEdit() Handles Topctrl1.tbEdit
        TxtQuery.Focus()
    End Sub

    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label
        Me.LblTermCondition = New System.Windows.Forms.Label
        Me.LblEntryTypeReq = New System.Windows.Forms.Label
        Me.TxtVoucherType = New AgControls.AgTextBox
        Me.LblEntryTypr = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.TxtReportName = New AgControls.AgTextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.TxtReportHeading = New AgControls.AgTextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.TxtQuery = New AgControls.AgTextBox
        Me.GrpUP.SuspendLayout()
        Me.GBoxEntryType.SuspendLayout()
        Me.GBoxMoveToLog.SuspendLayout()
        Me.GBoxApprove.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GBoxDivision.SuspendLayout()
        CType(Me.DTMaster, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Topctrl1
        '
        Me.Topctrl1.Size = New System.Drawing.Size(854, 41)
        Me.Topctrl1.TabIndex = 4
        '
        'GroupBox1
        '
        Me.GroupBox1.Location = New System.Drawing.Point(0, 370)
        Me.GroupBox1.Size = New System.Drawing.Size(856, 4)
        '
        'GrpUP
        '
        Me.GrpUP.Location = New System.Drawing.Point(4, 377)
        '
        'TxtEntryBy
        '
        Me.TxtEntryBy.Tag = ""
        Me.TxtEntryBy.Text = ""
        '
        'GBoxEntryType
        '
        Me.GBoxEntryType.Location = New System.Drawing.Point(148, 377)
        Me.GBoxEntryType.Size = New System.Drawing.Size(121, 44)
        '
        'TxtEntryType
        '
        Me.TxtEntryType.Size = New System.Drawing.Size(115, 18)
        Me.TxtEntryType.Tag = ""
        '
        'GBoxMoveToLog
        '
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(580, 377)
        Me.GBoxMoveToLog.Size = New System.Drawing.Size(121, 44)
        '
        'TxtMoveToLog
        '
        Me.TxtMoveToLog.Size = New System.Drawing.Size(89, 18)
        Me.TxtMoveToLog.Tag = ""
        '
        'GBoxApprove
        '
        Me.GBoxApprove.Location = New System.Drawing.Point(436, 377)
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
        Me.GroupBox2.Location = New System.Drawing.Point(724, 377)
        Me.GroupBox2.Size = New System.Drawing.Size(121, 44)
        '
        'GBoxDivision
        '
        Me.GBoxDivision.Location = New System.Drawing.Point(292, 377)
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
        Me.TxtStatus.Location = New System.Drawing.Point(3, 23)
        Me.TxtStatus.Size = New System.Drawing.Size(115, 18)
        Me.TxtStatus.Tag = ""
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(109, 138)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(10, 7)
        Me.Label1.TabIndex = 674
        Me.Label1.Text = "Ä"
        '
        'LblTermCondition
        '
        Me.LblTermCondition.AutoSize = True
        Me.LblTermCondition.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTermCondition.Location = New System.Drawing.Point(4, 133)
        Me.LblTermCondition.Name = "LblTermCondition"
        Me.LblTermCondition.Size = New System.Drawing.Size(107, 16)
        Me.LblTermCondition.TabIndex = 672
        Me.LblTermCondition.Text = "Term && Condition"
        '
        'LblEntryTypeReq
        '
        Me.LblEntryTypeReq.AutoSize = True
        Me.LblEntryTypeReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblEntryTypeReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblEntryTypeReq.Location = New System.Drawing.Point(299, 61)
        Me.LblEntryTypeReq.Name = "LblEntryTypeReq"
        Me.LblEntryTypeReq.Size = New System.Drawing.Size(10, 7)
        Me.LblEntryTypeReq.TabIndex = 677
        Me.LblEntryTypeReq.Text = "Ä"
        '
        'TxtVoucherType
        '
        Me.TxtVoucherType.AgAllowUserToEnableMasterHelp = False
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
        Me.TxtVoucherType.Location = New System.Drawing.Point(315, 54)
        Me.TxtVoucherType.MaxLength = 0
        Me.TxtVoucherType.Name = "TxtVoucherType"
        Me.TxtVoucherType.Size = New System.Drawing.Size(345, 18)
        Me.TxtVoucherType.TabIndex = 0
        '
        'LblEntryTypr
        '
        Me.LblEntryTypr.AutoSize = True
        Me.LblEntryTypr.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblEntryTypr.Location = New System.Drawing.Point(194, 54)
        Me.LblEntryTypr.Name = "LblEntryTypr"
        Me.LblEntryTypr.Size = New System.Drawing.Size(70, 16)
        Me.LblEntryTypr.TabIndex = 675
        Me.LblEntryTypr.Text = "Entry Type"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(299, 81)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(10, 7)
        Me.Label2.TabIndex = 680
        Me.Label2.Text = "Ä"
        '
        'TxtReportName
        '
        Me.TxtReportName.AgAllowUserToEnableMasterHelp = False
        Me.TxtReportName.AgMandatory = True
        Me.TxtReportName.AgMasterHelp = False
        Me.TxtReportName.AgNumberLeftPlaces = 0
        Me.TxtReportName.AgNumberNegetiveAllow = False
        Me.TxtReportName.AgNumberRightPlaces = 0
        Me.TxtReportName.AgPickFromLastValue = False
        Me.TxtReportName.AgRowFilter = ""
        Me.TxtReportName.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtReportName.AgSelectedValue = Nothing
        Me.TxtReportName.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtReportName.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtReportName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtReportName.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtReportName.Location = New System.Drawing.Point(315, 74)
        Me.TxtReportName.MaxLength = 100
        Me.TxtReportName.Name = "TxtReportName"
        Me.TxtReportName.Size = New System.Drawing.Size(345, 18)
        Me.TxtReportName.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(194, 74)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(84, 16)
        Me.Label3.TabIndex = 679
        Me.Label3.Text = "Report Name"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(299, 101)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(10, 7)
        Me.Label4.TabIndex = 683
        Me.Label4.Text = "Ä"
        '
        'TxtReportHeading
        '
        Me.TxtReportHeading.AgAllowUserToEnableMasterHelp = False
        Me.TxtReportHeading.AgMandatory = True
        Me.TxtReportHeading.AgMasterHelp = False
        Me.TxtReportHeading.AgNumberLeftPlaces = 0
        Me.TxtReportHeading.AgNumberNegetiveAllow = False
        Me.TxtReportHeading.AgNumberRightPlaces = 0
        Me.TxtReportHeading.AgPickFromLastValue = False
        Me.TxtReportHeading.AgRowFilter = ""
        Me.TxtReportHeading.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtReportHeading.AgSelectedValue = Nothing
        Me.TxtReportHeading.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtReportHeading.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtReportHeading.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtReportHeading.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtReportHeading.Location = New System.Drawing.Point(315, 94)
        Me.TxtReportHeading.MaxLength = 100
        Me.TxtReportHeading.Name = "TxtReportHeading"
        Me.TxtReportHeading.Size = New System.Drawing.Size(345, 18)
        Me.TxtReportHeading.TabIndex = 2
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(194, 94)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(97, 16)
        Me.Label5.TabIndex = 682
        Me.Label5.Text = "Report Heading"
        '
        'GroupBox3
        '
        Me.GroupBox3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox3.Location = New System.Drawing.Point(-2, 124)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(872, 6)
        Me.GroupBox3.TabIndex = 684
        Me.GroupBox3.TabStop = False
        '
        'TxtQuery
        '
        Me.TxtQuery.AgAllowUserToEnableMasterHelp = False
        Me.TxtQuery.AgMandatory = False
        Me.TxtQuery.AgMasterHelp = True
        Me.TxtQuery.AgNumberLeftPlaces = 8
        Me.TxtQuery.AgNumberNegetiveAllow = False
        Me.TxtQuery.AgNumberRightPlaces = 2
        Me.TxtQuery.AgPickFromLastValue = False
        Me.TxtQuery.AgRowFilter = ""
        Me.TxtQuery.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtQuery.AgSelectedValue = Nothing
        Me.TxtQuery.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtQuery.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtQuery.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtQuery.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtQuery.Location = New System.Drawing.Point(7, 152)
        Me.TxtQuery.MaxLength = 0
        Me.TxtQuery.Multiline = True
        Me.TxtQuery.Name = "TxtQuery"
        Me.TxtQuery.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TxtQuery.Size = New System.Drawing.Size(838, 212)
        Me.TxtQuery.TabIndex = 3
        '
        'FrmVoucherTypePrintSettings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(854, 421)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TxtReportHeading)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TxtReportName)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TxtQuery)
        Me.Controls.Add(Me.LblEntryTypeReq)
        Me.Controls.Add(Me.TxtVoucherType)
        Me.Controls.Add(Me.LblEntryTypr)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.LblTermCondition)
        Me.Name = "FrmVoucherTypePrintSettings"
        Me.Text = "Term Conditions"
        Me.Controls.SetChildIndex(Me.LblTermCondition, 0)
        Me.Controls.SetChildIndex(Me.Label1, 0)
        Me.Controls.SetChildIndex(Me.LblEntryTypr, 0)
        Me.Controls.SetChildIndex(Me.TxtVoucherType, 0)
        Me.Controls.SetChildIndex(Me.LblEntryTypeReq, 0)
        Me.Controls.SetChildIndex(Me.TxtQuery, 0)
        Me.Controls.SetChildIndex(Me.Label3, 0)
        Me.Controls.SetChildIndex(Me.TxtReportName, 0)
        Me.Controls.SetChildIndex(Me.Label2, 0)
        Me.Controls.SetChildIndex(Me.Label5, 0)
        Me.Controls.SetChildIndex(Me.TxtReportHeading, 0)
        Me.Controls.SetChildIndex(Me.Label4, 0)
        Me.Controls.SetChildIndex(Me.GroupBox3, 0)
        Me.Controls.SetChildIndex(Me.Topctrl1, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.GrpUP, 0)
        Me.Controls.SetChildIndex(Me.GBoxEntryType, 0)
        Me.Controls.SetChildIndex(Me.GBoxApprove, 0)
        Me.Controls.SetChildIndex(Me.GBoxMoveToLog, 0)
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.GBoxDivision, 0)
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
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Private Sub TxtVoucherType_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtVoucherType.Validating
        Dim DrTemp As DataRow() = Nothing
        Select Case sender.name
            Case TxtVoucherType.Name
                'If sender.text.ToString.Trim <> "" Then
                '    If sender.AgHelpDataSet IsNot Nothing Then
                '        DrTemp = sender.AgHelpDataSet.Tables(0).Select("Code = " & AgL.Chk_Text(sender.AgSelectedValue) & "")
                '        TxtDescription.Text = AgL.XNull(DrTemp(0)("Description"))
                '    End If
                'Else
                '    TxtDescription.Text = ""
                'End If
        End Select
    End Sub
End Class
