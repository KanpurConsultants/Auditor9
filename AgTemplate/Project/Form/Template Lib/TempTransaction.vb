Imports System.Data.SQLite
Imports AgLibrary.ClsMain.agConstants
Imports CrystalDecisions.CrystalReports.Engine
Public Class TempTransaction

    Public Event BaseFunction_MoveRec(ByVal SearchCode As String)
    Public Event BaseFunction_IniGrid()
    Public Event BaseFunction_FIniMast(ByVal BytDel As Byte, ByVal BytRefresh As Byte)
    Public Event BaseFunction_FIniMastLog(ByVal BytDel As Byte, ByVal BytRefresh As Byte)
    Public Event BaseFunction_FIniList()
    Public Event BaseFunction_CreateHelpDataSet()
    Public Event BaseEvent_Data_Validation(ByRef passed As Boolean)

    Public Event BaseFunction_Calculation()
    Public Event BaseFunction_BlankText()
    Public Event BaseFunction_DispText()

    Public Event BaseEvent_FindMain()
    Public Event BaseEvent_FindLog()
    Public Event BaseEvent_Form_PreLoad()
    Public Event BaseEvent_Save_PreTrans(ByVal SearchCode As String)
    Public Event BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As Object, ByVal Cmd As Object)
    Public Event BaseEvent_Save_PostTrans(ByVal SearchCode As String)
    Public Event BaseEvent_Approve_PreTrans(ByVal SearchCode As String)
    Public Event BaseEvent_ApproveDeletion_PreTrans(ByVal SearchCode As String)
    Public Event BaseEvent_Approve_InTrans(ByVal SearchCode As String, ByVal Conn As Object, ByVal Cmd As Object)
    Public Event BaseEvent_ApproveDeletion_InTrans(ByVal SearchCode As String, ByVal Conn As Object, ByVal Cmd As Object)
    Public Event BaseEvent_Approve_PostTrans(ByVal SearchCode As String)
    Public Event BaseEvent_ApproveDeletion_PostTrans(ByVal SearchCode As String)
    Public Event BaseEvent_Discard_InTrans(ByVal SearchCode As String, ByVal Conn As Object, ByVal Cmd As Object)
    Public Event BaseEvent_Topctrl_tbAdd()
    Public Event BaseEvent_Topctrl_tbEdit(ByRef Passed As Boolean)
    Public Event BaseEvent_Topctrl_tbDel(ByRef Passed As Boolean)
    Public Event BaseEvent_Topctrl_tbPrn(ByVal SearchCode As String)
    Public Event BaseEvent_Topctrl_tbRef()
    Public Event BaseEvent_Topctrl_tbMore()


    Public DTMaster As New DataTable()
    Public BMBMaster As BindingManagerBase
    Private KEAMainKeyCode As System.Windows.Forms.KeyEventArgs
    Private DTStruct As New DataTable
    Public IsApplyVTypePermission As Boolean

    Dim mFlagSaveAllowed As Boolean = False
    Dim mInUseToken As String = ""

    Dim mQry As String = ""
    Public mSearchCode As String = "", mInternalCode As String = ""


    Dim ClsRep As ClsReportProcedures


    Dim mTmV_Type$ = "", mTmV_Prefix$ = "", mTmV_Date$ = "", mTmV_NCat$ = ""             'Variables Holds Value During Add Mode


    Dim mNCAT As String
    Dim mIsFutureDateTransactionAllowed As Boolean = False
    Dim mFrmType As EntryPointType = EntryPointType.Main
    Dim mMainTableName As String
    Dim mLogTableName As String
    Dim mMainLineTableCSV As String
    Dim mLogLineTableCSV As String
    Dim ArrMainLineTable As String()
    Dim ArrLogLineTable As String()
    Protected mLogSystem As Boolean = False
    Dim mRestrictFinancialYearRecord As Boolean = True

    Protected mFrmObjBeforeModification As Form
    Protected mLogText As String = ""
    Public DrVoucherTypeDateLock As DataRow() = Nothing
    Public DrVoucherTypeTimePlan As DataRow() = Nothing
    Public DrFinancialYearDateLock As DataRow() = Nothing
    Public DtV_TypeSettings As DataTable
    Dim VoucherCategory As String
    Dim mOpenDocId As String = ""
    Public mCustomUI As String = ""


    Public Enum EntryPointType
        Main
        Log
    End Enum

    Public Class LogStatus
        Public Const LogOpen As String = "Open"
        Public Const LogDiscard As String = "Discard"
        Public Const LogApproved As String = "Approved"
    End Class

    Public Property EntryNCat() As String
        Get
            Return Replace(Replace(mNCAT, " ", ""), ",", "','")
        End Get
        Set(ByVal value As String)
            mNCAT = value
        End Set
    End Property

    Public Property RestrictFinancialYearRecord() As Boolean
        Get
            Return mRestrictFinancialYearRecord
        End Get
        Set(ByVal value As Boolean)
            mRestrictFinancialYearRecord = value
        End Set
    End Property

    Public Property MainLineTableCsv() As String
        Get
            Return mMainLineTableCSV
        End Get
        Set(ByVal value As String)
            mMainLineTableCSV = value

            ArrMainLineTable = Split(mMainLineTableCSV, ",")
        End Set
    End Property

    Public Property LogLineTableCsv() As String
        Get
            Return mLogLineTableCSV
        End Get
        Set(ByVal value As String)
            mLogLineTableCSV = value

            ArrLogLineTable = Split(mLogLineTableCSV, ",")
        End Set
    End Property

    Public Property MainTableName() As String
        Get
            Return mMainTableName
        End Get
        Set(ByVal value As String)
            mMainTableName = value
        End Set
    End Property

    Public Property LogTableName() As String
        Get
            Return mLogTableName
        End Get
        Set(ByVal value As String)
            mLogTableName = value
        End Set
    End Property

    Public Property FrmType() As EntryPointType
        Get
            Return mFrmType
        End Get
        Set(ByVal value As EntryPointType)
            mFrmType = value
        End Set
    End Property

    Public Property LogSystem() As Boolean
        Get
            Return mLogSystem
        End Get
        Set(ByVal value As Boolean)
            mLogSystem = value
        End Set
    End Property
    Public Property OpenDocId() As String
        Get
            Return mOpenDocId
        End Get
        Set(ByVal value As String)
            mOpenDocId = value
        End Set
    End Property

    Private Sub Form_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        Dim Obj As Object
        Dim I As Integer
        DTMaster = Nothing
        For Each Obj In Me.Controls
            If TypeOf Obj Is AgControls.AgTextBox Then
                If CType(Obj, AgControls.AgTextBox).AgHelpDataSet IsNot Nothing Then CType(Obj, AgControls.AgTextBox).AgHelpDataSet.Dispose()
            ElseIf TypeOf Obj Is AgControls.AgDataGrid Then
                For I = 0 To CType(Obj, AgControls.AgDataGrid).Columns.Count - 1
                    If CType(Obj, AgControls.AgDataGrid).AgHelpDataSet(I) IsNot Nothing Then CType(Obj, AgControls.AgDataGrid).AgHelpDataSet(I).Dispose()
                Next
            End If
        Next


        If mInUseToken <> "" Then
            mQry = "Update " & MainTableName & " Set InUseBy=Null, inUseToken=Null Where DocID = " & AgL.Chk_Text(mInternalCode) & " And InUseToken=" & AgL.Chk_Text(mInUseToken) & "  "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
            mInUseToken = ""
        End If
    End Sub

    Public Sub IniGrid()
        RaiseEvent BaseFunction_IniGrid()
    End Sub

    Private Sub KeyDown_Form(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F2 Or e.KeyCode = Keys.F3 Or e.KeyCode = Keys.F4 Or e.KeyCode = (Keys.F And e.Control) Or e.KeyCode = (Keys.P And e.Control) _
        Or e.KeyCode = (Keys.S And e.Control) Or e.KeyCode = Keys.Escape Or e.KeyCode = Keys.F5 Or e.KeyCode = Keys.F10 _
        Or e.KeyCode = Keys.Home Or e.KeyCode = Keys.PageUp Or e.KeyCode = Keys.PageDown Or e.KeyCode = Keys.End Then
            Topctrl1.TopKey_Down(e)
        End If

        If Me.ActiveControl IsNot Nothing Then
            If TypeOf (Me.ActiveControl) Is TextBox Then
                If Not CType(Me.ActiveControl, TextBox).Multiline Then
                    If e.KeyCode = Keys.Return Then SendKeys.Send("{Tab}")
                End If
            ElseIf Me.ActiveControl.Name <> Topctrl1.Name And
                Not (TypeOf (Me.ActiveControl) Is AgControls.AgDataGrid) Then
                If e.KeyCode = Keys.Return Then SendKeys.Send("{Tab}")
            End If

            'If e.KeyCode = Keys.Insert Then OpenLinkForm(Me.ActiveControl)
        End If
    End Sub

    Sub KeyPress_Form(ByVal Sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        If e.KeyChar = Chr(Keys.Escape) Then Exit Sub
        If Me.ActiveControl Is Nothing Then Exit Sub
        AgL.CheckQuote(e)
    End Sub

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            '----------------------------------------------------------
            '-----This Event will Contain TableName Property Assignment
            '----------------------------------------------------------
            RaiseEvent BaseEvent_Form_PreLoad()
            '----------------------------------------------------------
            Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
            IsApplyVTypePermission = AgTemplate.ClsMain.FIsApplyVTypePermission(AgL.PubUserName, EntryNCat)
            CreateHelpDataSets()
            IniGrid()
            FIniMaster()
            Ini_List()
            DispText()
            MoveRec()
            Me.Left = 0
            Me.Top = 0
            'AgL.WinSetting(Me, 660, 992, 0, 0)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Public Sub FIniMaster(Optional ByVal BytDel As Byte = 0, Optional ByVal BytRefresh As Byte = 1)

        If FrmType = EntryPointType.Main Then
            '---------------------------------------
            'Condition when Entry point Type is Main
            '---------------------------------------
            RaiseEvent BaseFunction_FIniMast(BytDel, BytRefresh)

            If mOpenDocId <> "" Then
                If DTMaster.Select("SearchCode='" & OpenDocId & "'").Length = 0 Then
                    mQry = "Select DocID As SearchCode 
                        From " & mMainTableName & " H  With (NoLock)
                        Where 1 = 1 And DocId = '" & mOpenDocId & "'  Order By V_Date , V_No  "
                    mQry = AgL.GetBackendBasedQuery(mQry)
                    Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
                End If
            End If
        Else
            '---------------------------------------
            'Condition when Entry point Type is LOG
            '---------------------------------------
            RaiseEvent BaseFunction_FIniMastLog(BytDel, BytRefresh)
        End If

    End Sub

    Sub Ini_List()
        Try
            If AgL Is Nothing Then Exit Sub

            mQry = ""
            If IsApplyVTypePermission Then mQry = " And V_Type In (Select V_Type From User_VType_Permission VP Where VP.UserName = '" & AgL.PubUserName & "' And VP.Div_Code = '" & AgL.PubDivCode & "' And VP.Site_Code = '" & AgL.PubSiteCode & "') "
            mQry = "Select V_Type as Code, Description, NCat, Category, IfNull(Status,'" & ClsMain.EntryStatus.Active & "') As Status, IsFutureDateTransactionAllowed " &
                   "From Voucher_Type " &
                   "Where NCat In ('" & EntryNCat & "') " & mQry
            mQry += " And IsNull(Status,'Active') <> 'InActive'"
            mQry += " And (CharIndex('" & AgL.PubSiteCode & "','+' || SiteList) > 0 Or SiteList Is Null) "
            mQry += " And (CharIndex('" & AgL.PubDivCode & "','+' || DivisionList) > 0 Or DivisionList Is Null) "
            mQry += " And IfNull(CustomUI,'') = '" & mCustomUI & "'"

            mQry += " And IsNull(Status,'Active') <> 'InActive'"
            TxtV_Type.AgHelpDataSet(2, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)

            mQry = "Select Div_Code, Div_Name From Division Order By Div_Name"
            TxtDivision.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)

            mQry = "Select Code, ManualCode, Name From SiteMast Order By ManualCode"
            TxtSite_Code.AgHelpDataSet(1, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)

            mQry = "Select '" & ClsMain.EntryStatus.Active & "' As Code, '" & ClsMain.EntryStatus.Active & "' As Description " &
                    " Union All Select '" & ClsMain.EntryStatus.Cancelled & "' As Code, '" & ClsMain.EntryStatus.Cancelled & "' As Description "
            TxtStatus.AgHelpDataSet(0, GroupBox2.Top - 150, GroupBox2.Left) = AgL.FillData(mQry, AgL.GCn)


            RaiseEvent BaseFunction_FIniList()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    'Private Sub Topctrl1_tbAdd() Handles Topctrl1.tbAdd
    '    BlankText()
    '    DispText(True)
    '    TxtDivision.AgSelectedValue = AgL.PubDivCode
    '    TxtStatus.Text = ClsMain.EntryStatus.Active
    '    TxtV_Date.Text = AgL.PubLoginDate
    '    TxtV_Date.Focus()
    'End Sub


    Private Sub Topctrl1_tbDel() Handles Topctrl1.tbDel
        Dim BlnTrans As Boolean = False
        Dim GCnCmd As New Object
        Dim MastPos As Long
        Dim mTrans As Boolean = False
        Dim InstancePassed As Boolean = True
        Dim StrVPrefixStatus As String
        Dim dtTemp As DataTable

        Try
            MastPos = BMBMaster.Position


            If Not AgL.StrCmp(TxtDivision.AgSelectedValue, AgL.PubDivCode) Then
                MsgBox("Different Division Record. Can't Modify!", MsgBoxStyle.OkOnly, "Validation") : Exit Sub
            End If

            If DrVoucherTypeDateLock IsNot Nothing Then
                If DrVoucherTypeDateLock.Length > 0 Then
                    If AgL.XNull(DrVoucherTypeDateLock(0)("LockTillDate")) <> "" Then
                        If CDate(TxtV_Date.Text) <= CDate((AgL.XNull(DrVoucherTypeDateLock(0)("LockTillDate")))) Then
                            MsgBox("Entries are locked till date " & Format(CDate(AgL.XNull(DrVoucherTypeDateLock(0)("LockTillDate"))), "dd/MMM/yyyy"), MsgBoxStyle.Information)
                            Topctrl1.FButtonClick(14, True)
                            Exit Sub
                        End If
                    End If
                End If
            End If

            If DrFinancialYearDateLock IsNot Nothing Then
                If DrFinancialYearDateLock.Length > 0 Then
                    If CType(AgL.VNull(DrFinancialYearDateLock(0)("IsLocked")), Boolean) = True Then
                        If AgL.XNull(DrFinancialYearDateLock(0)("End_Dt")) <> "" Then
                            If CDate(TxtV_Date.Text) <= CDate((AgL.XNull(DrFinancialYearDateLock(0)("End_Dt")))) Then
                                MsgBox("Financial year " & AgL.XNull(DrFinancialYearDateLock(0)("cyear")) & " is locked.", MsgBoxStyle.Information)
                                Topctrl1.FButtonClick(14, True)
                                Exit Sub
                            End If
                        End If
                    End If
                End If
            End If

            If DrVoucherTypeTimePlan IsNot Nothing Then
                If Not AgL.StrCmp(AgL.PubUserName, "Sa") And Not AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Then
                    If DrVoucherTypeTimePlan.Length > 0 Then
                        If AgL.VNull(DrVoucherTypeTimePlan(0)("DayLimitDelete")) <> 0 Then
                            If DateDiff(DateInterval.Day, CDate(TxtV_Date.Text), CDate(AgL.PubLoginDate)) > AgL.VNull(DrVoucherTypeTimePlan(0)("DayLimitDelete")) - 1 Then
                                If FIsAllowedByTimePlan(AgL.PubUserName, mSearchCode, EntryAction.Delete) = False Then
                                    If AgL.PubUserName.ToUpper = AgLibrary.ClsConstant.PubSuperUserName _
                                   Or AgL.PubUserName.ToUpper = "SA" Then
                                        If MsgBox("Deletion is locked for date " & TxtV_Date.Text + ".Do you want to proceed ? ", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                                            Exit Sub
                                        End If
                                    Else
                                        If MsgBox("Deletion is locked for date " & TxtV_Date.Text + ".Do you want to request for permission ? ", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                                            FRequestForPermission(EntryAction.Delete)
                                        End If
                                        Exit Sub
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            End If

            Dim mTempStr As String = ""
            mQry = "Select IfNull(InUseBy,'') as InUseBy From " & MainTableName & " Where DocID = " & AgL.Chk_Text(mInternalCode) & "  "
            dtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If dtTemp.Rows.Count > 0 Then
                mTempStr = AgL.XNull(dtTemp.Rows(0)("InUseBy"))
                If mTempStr <> "" Then
                    MsgBox("Record is already is use by user " & mTempStr.ToUpper & ", You can not delete it for now.")
                    Exit Sub
                Else
                    mInUseToken = Guid.NewGuid().ToString
                    mQry = "Update " & MainTableName & " Set InUseBy=" & AgL.Chk_Text(AgL.PubUserName) & ", InUseToken=" & AgL.Chk_Text(mInUseToken) & " Where DocID = " & AgL.Chk_Text(mInternalCode) & "  "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
                End If
            Else
                MsgBox("Record can not be found, May be just deleted by any user")
                Exit Sub
            End If


            If AgL.PubDivCode <> TxtDivision.Tag Then
                MsgBox("Cant't Delete Other Division Record...!", MsgBoxStyle.Information)
                Topctrl1.FButtonClick(14, True)
                Exit Sub
            End If

            If AgL.PubSiteCode <> TxtSite_Code.Tag Then
                MsgBox("Cant't Delete Other Site Record...!", MsgBoxStyle.Information)
                Topctrl1.FButtonClick(14, True)
                Exit Sub
            End If


            RaiseEvent BaseEvent_Topctrl_tbDel(InstancePassed)
            If Not InstancePassed Then Exit Sub


            If TxtApproveBy.Text <> "" Then
                If TxtApproveBy.Text.ToUpper <> AgL.PubUserName.ToUpper Then
                    MsgBox("Deletion of approved record is not allowed." & vbCrLf & "Please contact to " & TxtApproveBy.Text)
                Else
                    MsgBox("Deletion of approved record is not allowed." & vbCrLf & "Please unlock it first ")
                End If

                Exit Sub
            End If



            If DTMaster.Rows.Count > 0 Then
                If MsgBox("Are You Sure To Delete This Record?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, AgLibrary.ClsMain.PubMsgTitleInfo) = vbYes Then


                    AgL.ECmd = AgL.GCn.CreateCommand
                    AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
                    AgL.ECmd.Transaction = AgL.ETrans
                    mTrans = True

                    If LogSystem Then
                        AgL.Dman_ExecuteNonQry("Update " & LogTableName & " Set EntryBy=" & AgL.Chk_Text(AgL.PubUserName) & ", EntryDate = " & AgL.Chk_Text(AgL.GetDateTime(AgL.GcnRead)) & " Where UID='" & mSearchCode & "'", AgL.GCn, AgL.ECmd)
                    End If


                    ClsMain.FCreateLogForDelete(Me, mLogText)
                    Call AgL.LogTableEntry(mSearchCode, Me.Text, "D", AgL.PubMachineName, AgL.PubUserName, AgL.GetDateTime(AgL.GcnRead), AgL.GCn, AgL.ECmd,, TxtV_Date.Text,,,, TxtSite_Code.Tag, TxtDivision.Tag, mLogText, TxtV_Type.Tag, TxtReferenceNo.Text)

                    If Not LogSystem Then
                        TxtEntryType.Text = "Delete"
                        FMoveToLog(AgL.GCn, AgL.ECmd, "Delete")
                        RaiseEvent BaseEvent_ApproveDeletion_PreTrans(mSearchCode)
                        ProcApporve(AgL.GCn, AgL.ECmd)
                        RaiseEvent BaseEvent_ApproveDeletion_PostTrans(mSearchCode)
                    End If

                    AgL.ETrans.Commit()
                    mTrans = False

                    FIniMaster(1)
                    Topctrl1_tbRef()
                    MoveRec()
                Else
                    If mInUseToken <> "" Then
                        mQry = "Update " & MainTableName & " Set InUseBy=Null, inUseToken=Null Where DocID = " & AgL.Chk_Text(mInternalCode) & " And InUseToken=" & AgL.Chk_Text(mInUseToken) & "  "
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
                        mInUseToken = ""
                    End If
                End If
            End If
        Catch Ex As Exception
            If mTrans = True Then AgL.ETrans.Rollback()
            MsgBox(Ex.Message, MsgBoxStyle.Information, AgLibrary.ClsMain.PubMsgTitleInfo)
        End Try
    End Sub

    Private Sub Topctrl1_tbDiscard() Handles Topctrl1.tbDiscard
        FIniMaster(0, 0)
        mFlagSaveAllowed = False
        Topctrl1.Focus()
        If Topctrl1.Mode.ToUpper = "EDIT" Then
            If mInUseToken <> "" Then
                mQry = "Update " & MainTableName & " Set InUseBy=Null, inUseToken=Null Where DocID = " & AgL.Chk_Text(mInternalCode) & " And InUseToken=" & AgL.Chk_Text(mInUseToken) & "  "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
                mInUseToken = ""
            End If
        End If
    End Sub


    Private Sub Topctrl1_tbEdit() Handles Topctrl1.tbEdit
        Dim InstancePassed As Boolean = True
        Dim StrVPrefixStatus As String
        Dim mTempStr As String = ""
        Dim dtTemp As DataTable

        If DrVoucherTypeDateLock IsNot Nothing Then
            If DrVoucherTypeDateLock.Length > 0 Then
                If AgL.XNull(DrVoucherTypeDateLock(0)("LockTillDate")) <> "" Then
                    If CDate(TxtV_Date.Text) <= CDate((AgL.XNull(DrVoucherTypeDateLock(0)("LockTillDate")))) Then
                        MsgBox("Entries are locked till date " & Format(CDate(AgL.XNull(DrVoucherTypeDateLock(0)("LockTillDate"))), "dd/MMM/yyyy"))
                        Topctrl1.FButtonClick(14, True)
                        Exit Sub
                    End If
                End If
            End If
        End If

        If DrFinancialYearDateLock IsNot Nothing Then
            If DrFinancialYearDateLock.Length > 0 Then
                If CType(AgL.VNull(DrFinancialYearDateLock(0)("IsLocked")), Boolean) = True Then
                    If AgL.XNull(DrFinancialYearDateLock(0)("End_Dt")) <> "" Then
                        If CDate(TxtV_Date.Text) <= CDate((AgL.XNull(DrFinancialYearDateLock(0)("End_Dt")))) Then
                            MsgBox("Financial year " & AgL.XNull(DrFinancialYearDateLock(0)("cyear")) & " is locked.", MsgBoxStyle.Information)
                            Topctrl1.FButtonClick(14, True)
                            Exit Sub
                        End If
                    End If
                End If
            End If
        End If


        If DrVoucherTypeTimePlan IsNot Nothing Then
            If Not AgL.StrCmp(AgL.PubUserName, "Sa") And Not AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Then
                If DrVoucherTypeTimePlan.Length > 0 Then
                    If AgL.VNull(DrVoucherTypeTimePlan(0)("DayLimitEdit")) <> 0 Then
                        If DateDiff(DateInterval.Day, CDate(TxtV_Date.Text), CDate(AgL.PubLoginDate)) > AgL.VNull(DrVoucherTypeTimePlan(0)("DayLimitEdit")) - 1 Then
                            If FIsAllowedByTimePlan(AgL.PubUserName, mSearchCode, EntryAction.Edit) = False Then
                                If MsgBox("Editing is locked for date " & TxtV_Date.Text + ".Do you want to request for permission ? ", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                                    FRequestForPermission(EntryAction.Edit)
                                End If
                                Topctrl1.FButtonClick(14, True)
                                Exit Sub
                            End If
                        End If
                    End If
                End If
            End If
        End If

        mQry = "Select IfNull(InUseBy,'') as InUseBy From " & MainTableName & " Where DocID = " & AgL.Chk_Text(mInternalCode) & "  "
        dtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
        If dtTemp.Rows.Count > 0 Then
            mTempStr = AgL.XNull(dtTemp.Rows(0)("InUseBy"))
            If mTempStr <> "" Then
                MsgBox("Record is already is use by user " & mTempStr.ToUpper & ", You can not edit it for now.")
                Topctrl1.FButtonClick(14, True)
                Exit Sub
            Else
                mInUseToken = Guid.NewGuid().ToString
                mQry = "Update " & MainTableName & " Set InUseBy=" & AgL.Chk_Text(AgL.PubUserName) & ", InUseToken=" & AgL.Chk_Text(mInUseToken) & " Where DocID = " & AgL.Chk_Text(mInternalCode) & "  "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
            End If
        Else
            MsgBox("Record can not be found, May be just deleted by any user")
            Exit Sub
        End If


        If AgL.PubDivCode <> TxtDivision.Tag Then
            MsgBox("Cant't Edit Other Division Record...!", MsgBoxStyle.Information)
            Topctrl1.FButtonClick(14, True)
            Exit Sub
        End If

        If AgL.PubSiteCode <> TxtSite_Code.Tag Then
            MsgBox("Cant't Edit Other Site Record...!", MsgBoxStyle.Information)
            Topctrl1.FButtonClick(14, True)
            Exit Sub
        End If






        RaiseEvent BaseEvent_Topctrl_tbEdit(InstancePassed)
        If Not InstancePassed Then
            Topctrl1.FButtonClick(14, True)
            Exit Sub
        End If

        If TxtApproveBy.Text <> "" Then
            If TxtApproveBy.Text.ToUpper <> AgL.PubUserName.ToUpper Then
                MsgBox("Editing of approved record is not allowed." & vbCrLf & "Please contact to " & TxtApproveBy.Text)
            Else
                MsgBox("Editing of approved record is not allowed." & vbCrLf & "Please unlock it first ")
            End If

            Topctrl1.FButtonClick(14, True)
            Exit Sub
        End If



        If AgL.StrCmp(TxtDivision.AgSelectedValue, AgL.PubDivCode) Then
            DispText(True)
            TxtV_Date.Focus()

        Else
            Topctrl1.FButtonClick(14, True)
            MsgBox("Different Division Record. Can't Modify!", MsgBoxStyle.OkOnly, "Validation") : Exit Sub
        End If

        If AgL.XNull(TxtV_Type.Tag) <> "" Then
            If FGetVoucher_Type_ManualRefType(TxtV_Type.Tag) = "Day Wise" Then
                TxtReferenceNo.Enabled = False
                TxtV_Date.Enabled = False
            End If
        End If

        mFlagSaveAllowed = True
        mFrmObjBeforeModification = New Form()
        ClsMain.FCreateObjectOfForm(mFrmObjBeforeModification, Me)
    End Sub


    Private Sub Topctrl1_tbFind() Handles Topctrl1.tbFind
        If DTMaster.Rows.Count <= 0 Then MsgBox("No Records To Search.", vbInformation, AgLibrary.ClsMain.PubMsgTitleInfo) : Exit Sub
        Try
            If FrmType = EntryPointType.Main Then
                '---------------------------------------
                'Condition when Entry point Type is Main
                '---------------------------------------
                RaiseEvent BaseEvent_FindMain()
            Else
                '---------------------------------------
                'Condition when Entry point Type is LOG
                '---------------------------------------
                RaiseEvent BaseEvent_FindLog()
            End If



            Dim Frmbj As AgLibrary.FrmFind = New AgLibrary.FrmFind(AgL.PubFindQry, Me.Text & " Find", AgL)
            Frmbj.ShowDialog()
            If Frmbj.IsFrmCancelled = True Then
                AgL.PubSearchRow = ""
            Else
                If Frmbj.DGL1.CurrentRow IsNot Nothing Then
                    AgL.PubSearchRow = Frmbj.DGL1.Item(0, Frmbj.DGL1.CurrentRow.Index).Value.ToString
                End If
            End If
            If AgL.PubSearchRow <> "" Then
                AgL.PubDRFound = DTMaster.Rows.Find(AgL.PubSearchRow)
                BMBMaster.Position = DTMaster.Rows.IndexOf(AgL.PubDRFound)
                MoveRec()
            End If


            ''*************** common code start *****************
            'AgL.PubObjFrmFind = New AgLibrary.frmFind(AgL)
            'AgL.PubObjFrmFind.ShowDialog()
            'AgL.PubObjFrmFind = Nothing
            'If AgL.PubSearchRow <> "" Then
            '    AgL.PubDRFound = DTMaster.Rows.Find(AgL.PubSearchRow)
            '    BMBMaster.Position = DTMaster.Rows.IndexOf(AgL.PubDRFound)
            '    MoveRec()
            'End If
            ''*************** common code end  *****************
        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try
    End Sub

    Private Sub Topctrl1_tbRef() Handles Topctrl1.tbRef
        CreateHelpDataSets()
        Ini_List()
        RaiseEvent BaseEvent_Topctrl_tbRef()
    End Sub




    Private Sub Topctrl1_tbPrn() Handles Topctrl1.tbPrn
        Dim mTempStr As String = ""
        Dim dtTemp As DataTable


        If DrVoucherTypeTimePlan IsNot Nothing Then
            If Not AgL.StrCmp(AgL.PubUserName, "Sa") And Not AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Then
                If DrVoucherTypeTimePlan.Length > 0 Then
                    If AgL.VNull(DrVoucherTypeTimePlan(0)("DayLimitPrint")) <> 0 Then
                        If DateDiff(DateInterval.Day, CDate(TxtV_Date.Text), CDate(AgL.PubLoginDate)) > AgL.VNull(DrVoucherTypeTimePlan(0)("DayLimitPrint")) - 1 Then
                            If FIsAllowedByTimePlan(AgL.PubUserName, mSearchCode, EntryAction.Print) = False Then
                                If MsgBox("Printing is locked for date " & TxtV_Date.Text + ".Do you want to request for permission ? ", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                                    FRequestForPermission(EntryAction.Print)
                                End If
                                Exit Sub
                            End If
                        End If
                    End If
                End If
            End If
        End If

        mQry = "Select IfNull(InUseBy,'') as InUseBy From " & MainTableName & " Where DocID = " & AgL.Chk_Text(mInternalCode) & "  "
        dtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
        If dtTemp.Rows.Count > 0 Then
            mTempStr = AgL.XNull(dtTemp.Rows(0)("InUseBy"))
            If mTempStr <> "" Then
                MsgBox("Record is already is use by user " & mTempStr.ToUpper & ", You can not print it for now.")
                Topctrl1.FButtonClick(14, True)
                Exit Sub
            End If
        Else
            MsgBox("Record can not be found, May be just deleted by any user")
            Exit Sub
        End If



        'Dim StrVPrefixStatus As String

        'StrVPrefixStatus = AgL.Dman_Execute("SELECT IfNull(Status_Print,'" & AgTemplate.ClsMain.EntryStatus.Active & "'), Date_From , Date_To , Prefix  FROM Voucher_Prefix WHERE Date_From <= '" & TxtV_Date.Text & "' AND Date_To >= '" & TxtV_Date.Text & "'", AgL.GCn).ExecuteScalar
        'If StrVPrefixStatus <> AgTemplate.ClsMain.EntryStatus.Active Then
        '    MsgBox("Entry is " & StrVPrefixStatus & " for Date " & TxtV_Date.Text)
        '    Topctrl1.FButtonClick(14, True)
        '    Exit Sub
        'End If

        RaiseEvent BaseEvent_Topctrl_tbPrn(mSearchCode)
    End Sub


    Private Sub Topctrl1_tbSave() Handles Topctrl1.tbSave
        Dim MastPos As Long
        Dim mTrans As String = ""

        Try
            MastPos = BMBMaster.Position

            If Not mFlagSaveAllowed Then Exit Sub

            '---------------------------------------------------
            'Any type of validation like Required field, Duplicate Check etc.
            'are to be write in Data_Validation function.
            '----------------------------------------------------
            If Data_Validation() = False Then Exit Sub
            '----------------------------------------------------

            RaiseEvent BaseEvent_Save_PreTrans(mSearchCode)

            If Not LogSystem Then
                RaiseEvent BaseEvent_Approve_PreTrans(mSearchCode)
            End If

            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = "Begin"


            If Topctrl1.Mode = "Edit" Then
                If FrmType = EntryPointType.Main Then
                    FMoveToLog(AgL.GCn, AgL.ECmd, "Edit")
                End If
            End If

            If Topctrl1.Mode = "Add" Then
                If FrmType = EntryPointType.Main Then
                    mQry = "INSERT INTO " & MainTableName & " (DocId, Div_Code, Site_Code, V_Date, V_Type, V_Prefix, V_No, EntryBy, EntryDate, Status) " &
                            "VALUES (" & AgL.Chk_Text(mInternalCode) & ", '" & TxtDivision.AgSelectedValue & "',  " & AgL.Chk_Text(TxtSite_Code.AgSelectedValue) & "," & AgL.Chk_Text(CDate(TxtV_Date.Text).ToString("s")) & ", " & AgL.Chk_Text(TxtV_Type.AgSelectedValue) & ", " & AgL.Chk_Text(LblPrefix.Text) & ",  " & Val(TxtV_No.Text) & "," & AgL.Chk_Text(AgL.PubUserName) & ", " & AgL.Chk_Text(CDate(AgL.GetDateTime(AgL.GcnRead)).ToString("s")) & ",  " & AgL.Chk_Text(TxtStatus.Text) & " )"
                Else
                    mQry = "INSERT INTO " & LogTableName & " (UID, DocId, Div_Code, Site_Code, V_Date, V_Type, V_Prefix, V_No, EntryBy, EntryDate,  Status) " &
                            "VALUES (" & AgL.Chk_Text(mSearchCode) & ", " & AgL.Chk_Text(mInternalCode) & ", '" & TxtDivision.AgSelectedValue & "',  " & AgL.Chk_Text(TxtSite_Code.AgSelectedValue) & "," & AgL.Chk_Text(CDate(TxtV_Date.Text).ToString("s")) & ", " & AgL.Chk_Text(TxtV_Type.AgSelectedValue) & ", " & AgL.Chk_Text(LblPrefix.Text) & ",  " & Val(TxtV_No.Text) & "," & AgL.Chk_Text(AgL.PubUserName) & ", " & AgL.Chk_Text(CDate(AgL.GetDateTime(AgL.GcnRead)).ToString("s")) & ",  " & AgL.Chk_Text(TxtStatus.Text) & " )"
                End If

                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            Else
                If FrmType = EntryPointType.Main Then
                    mQry = "Update " & MainTableName & " Set V_Date=" & AgL.Chk_Text(CDate(TxtV_Date.Text).ToString("s")) & ", MoveToLog = " & AgL.Chk_Text(AgL.PubUserName) & ", MoveToLogDate = " & AgL.Chk_Text(CDate(AgL.GetDateTime(AgL.GcnRead)).ToString("s")) & " " &
                           " Where DocID = " & AgL.Chk_Text(mInternalCode) & "  "
                Else
                    mQry = "Update " & LogTableName & " Set V_Date=" & AgL.Chk_Text(CDate(TxtV_Date.Text).ToString("s")) & ", MoveToLog = " & AgL.Chk_Text(AgL.PubUserName) & ", MoveToLogDate = " & AgL.Chk_Text(CDate(AgL.GetDateTime(AgL.GcnRead)).ToString("s")) & " " &
                           " Where UID = " & AgL.Chk_Text(mSearchCode) & "  "
                End If
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If

            RaiseEvent BaseEvent_Save_InTrans(mSearchCode, AgL.GCn, AgL.ECmd)


            If Topctrl1.Mode = "Add" Then
                ClsMain.UpdateVoucherCounter(mInternalCode, CDate(TxtV_Date.Text), AgL.GCn, AgL.ECmd, AgL.PubDivCode, TxtSite_Code.AgSelectedValue)
            End If

            '--------------------------------------------------------------
            'Create a log entry of each activity like add, edit delete print
            '--------------------------------------------------------------
            If Topctrl1.Mode <> "Add" Then
                mLogText = ""
                ClsMain.FCreateLogForEdit(mFrmObjBeforeModification, Me, mLogText)
            End If
            Call AgL.LogTableEntry(mSearchCode, Me.Text, AgL.MidStr(Topctrl1.Mode, 0, 1), AgL.PubMachineName, AgL.PubUserName, AgL.GetDateTime(AgL.GcnRead), AgL.GCn, AgL.ECmd, , TxtV_Date.Text,,,, TxtSite_Code.Tag, TxtDivision.Tag, mLogText, TxtV_Type.Tag, TxtReferenceNo.Text)
            '--------------------------------------------------------------

            If Topctrl1.Mode.ToUpper = "EDIT" Then
                If mInUseToken <> "" Then
                    mQry = "Update " & MainTableName & " Set InUseBy=Null, inUseToken=Null Where DocID = " & AgL.Chk_Text(mInternalCode) & " And InUseToken=" & AgL.Chk_Text(mInUseToken) & "  "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                End If
            End If


            ''--------------------------------------------------------------
            ''If not using Log System then approve record automatic on each save
            ''--------------------------------------------------------------
            'If Not LogSystem Then
            '    Call ProcApporve(AgL.GCn, AgL.ECmd)
            'End If
            ''--------------------------------------------------------------

            AgL.ETrans.Commit()
            mTrans = "Commit"

            RaiseEvent BaseEvent_Save_PostTrans(mSearchCode)

            If Not LogSystem Then
                RaiseEvent BaseEvent_Approve_PostTrans(mSearchCode)
            End If

            FIniMaster(0, 1)
            Topctrl1_tbRef()

            If Topctrl1.Mode = "Add" Then
                '--------------------------------------------------------
                'Set newly feeded record as current record
                'go to add mode once again
                '--------------------------------------------------------

                Topctrl1.LblDocId.Text = mSearchCode

                mTmV_Type = TxtV_Type.AgSelectedValue : mTmV_Prefix = LblPrefix.Text : mTmV_Date = TxtV_Date.Text : mTmV_NCat = LblV_Type.Tag

                Topctrl1.FButtonClick(0)

                Exit Sub
            Else
                mTmV_Type = "" : mTmV_Prefix = "" : mTmV_Date = "" : mTmV_NCat = ""

                Topctrl1.SetDisp(True)
                If AgL.PubMoveRecApplicable Then MoveRec()
            End If
        Catch ex As Exception
            If mTrans = "Begin" Then
                AgL.ETrans.Rollback()
            ElseIf mTrans = "Commit" Then
                Topctrl1.FButtonClick(14, True)
            End If
            MsgBox(ex.Message)
        Finally
        End Try
    End Sub
    Public Shared Sub UpdateVoucherCounter(ByVal mDocId As String, ByVal mDate As Date, ByVal mConn As SQLiteConnection,
                                    ByVal mCmd As SQLiteCommand, ByVal mDiv_Code As String, ByVal mSite_Code As String,
                                    Optional ByVal mDelete_Record As Boolean = False, Optional ByVal mComp_Code As String = "")
        Dim RdTemp As SQLiteDataReader = Nothing
        Dim mVno As Long
        Dim mVType As String
        Dim mVPrefix As String
        Dim CondStr As String
        Dim mQry As String

        Try
            mVno = Val(AgL.DeCodeDocID(mDocId, AgLibrary.ClsMain.DocIdPart.VoucherNo))
            mVType = AgL.DeCodeDocID(mDocId, AgLibrary.ClsMain.DocIdPart.VoucherType)
            mVPrefix = AgL.DeCodeDocID(mDocId, AgLibrary.ClsMain.DocIdPart.VoucherPrefix)

            AgL.ECmd.CommandText = "Select SiteWise,DivisionWise From Voucher_Type Where V_Type='" & mVType & "'"
            RdTemp = AgL.ECmd.ExecuteReader

            CondStr = " Where V_Type='" & Trim(mVType) & "' and Prefix='" & Trim(mVPrefix) & "' " &
                      "And Date_From<=" & AgL.Chk_Date(Format(CDate(mDate), "dd/MMM/yyyy")) & " And Date_To>=" & AgL.Chk_Date(Format(CDate(mDate), "dd/MMM/yyyy")) & "  And Start_Srl_No " & IIf(mDelete_Record, "=", "<") & " " & mVno & " "

            If RdTemp.Read Then
                If Math.Abs(AgL.VNull(RdTemp.Item("SiteWise"))) = 1 Then CondStr = CondStr & " And Site_Code='" & mSite_Code & "' "
                If Math.Abs(AgL.VNull(RdTemp.Item("DivisionWise"))) = 1 Then CondStr = CondStr & " And Div_Code='" & mDiv_Code & "' "
            End If
            If RdTemp IsNot Nothing Then RdTemp.Close()

            If mComp_Code.Trim <> "" Then CondStr += " And IfNull(Comp_Code,'" & AgL.PubCompCode & "') = '" & mComp_Code & "' "

            mQry = "Update Voucher_Prefix Set Start_Srl_No=" & IIf(mDelete_Record, mVno - 1, mVno) & CondStr
            AgL.Dman_ExecuteNonQry(mQry, mConn, mCmd)
        Catch Ex As Exception
            If RdTemp IsNot Nothing Then RdTemp.Close()
            MsgBox(Ex.Message)
        Finally
            If RdTemp IsNot Nothing Then RdTemp.Close()
        End Try
    End Sub

    Public Sub MoveRec()
        Dim DsTemp As DataSet = Nothing
        Dim MastPos As Long
        Try
            If AgL Is Nothing Then Exit Sub
            BlankText()
            If DTMaster.Rows.Count > 0 Then
                MastPos = BMBMaster.Position

                mSearchCode = DTMaster.Rows(MastPos)("SearchCode").ToString




                mQry = "Select H.DocID, H.Div_Code, H.Site_Code, H.V_Type, H.V_Prefix, H.V_No, H.V_Date, H.EntryBy, H.EntryDate,  H.ApproveBy, H.MoveToLog, H.MoveToLogDate, H.Status, Vt.NCat, Vt.Description As Voucher_TypeDesc " &
                " From " & MainTableName & " H Left Join Voucher_Type Vt On H.V_Type = VT.V_Type Where H.DocId='" & mSearchCode & "'"
                DsTemp = AgL.FillData(mQry, AgL.GCn)
                With DsTemp.Tables(0)
                    '---------------------------------------------------
                    'Common code for all entry and approval management
                    '---------------------------------------------------
                    mInternalCode = AgL.XNull(.Rows(0)("DocID"))
                    TxtDocId.Text = AgL.XNull(.Rows(0)("DocID"))
                    TxtSite_Code.AgSelectedValue = AgL.XNull(.Rows(0)("Site_Code"))
                    TxtDivision.AgSelectedValue = AgL.XNull(.Rows(0)("Div_Code"))
                    TxtV_Type.Tag = AgL.XNull(.Rows(0)("V_Type"))
                    TxtV_Type.Text = AgL.XNull(.Rows(0)("Voucher_TypeDesc"))
                    LblV_Type.Tag = AgL.XNull(.Rows(0)("NCat"))
                    Validating_VType(TxtV_Type)
                    TxtV_Type.AgLastValueTag = TxtV_Type.Tag
                    TxtV_Type.AgLastValueText = TxtV_Type.Text
                    LblPrefix.Text = AgL.XNull(.Rows(0)("V_Prefix"))
                    TxtV_No.Text = AgL.VNull(.Rows(0)("V_No"))
                    TxtV_Date.Text = Format(CDate(AgL.XNull(.Rows(0)("V_Date"))), "dd/MMM/yyyy")
                    CmdStatus.Tag = AgL.XNull(.Rows(0)("Status"))
                    TxtStatus.Text = AgL.XNull(.Rows(0)("Status"))
                    TxtEntryBy.Text = AgL.XNull(.Rows(0)("EntryBy"))
                    TxtApproveBy.Text = AgL.XNull(.Rows(0)("ApproveBy"))
                    TxtMoveToLog.Text = AgL.XNull(.Rows(0)("MoveToLog"))
                    CmdApprove.Enabled = CBool(TxtApproveBy.Text.ToString = "" And GBoxApprove.Enabled)
                    'CmdMoveToLog.Enabled = CBool(TxtMoveToLog.Text.ToString = "" And GBoxMoveToLog.Enabled)

                    If AgL.XNull(.Rows(0)("EntryDate")) <> "" Then
                        ToolTip1.SetToolTip(GrpUP, Format(CDate(AgL.XNull(.Rows(0)("EntryDate"))), "dd/MMM/yyyy"))
                    Else
                        ToolTip1.SetToolTip(GrpUP, "")
                    End If
                    If AgL.XNull(.Rows(0)("MoveToLogDate")) <> "" Then
                        ToolTip1.SetToolTip(GBoxMoveToLog, Format(CDate(AgL.XNull(.Rows(0)("MoveToLogDate"))), "dd/MMM/yyyy"))
                    Else
                        ToolTip1.SetToolTip(GBoxMoveToLog, "")
                    End If


                    If FrmType = EntryPointType.Main Then
                        If Not LogSystem Then
                            If TxtApproveBy.Text.ToString <> "" Then
                                CmdApprove.Visible = False
                                If AgL.PubUserName.ToUpper = AgLibrary.ClsConstant.PubSuperUserName _
                                   Or AgL.PubUserName.ToUpper = "SA" _
                                   Or AgL.PubUserName.ToUpper = TxtApproveBy.Text.ToUpper Then
                                    CmdDiscard.Visible = True
                                Else
                                    CmdDiscard.Visible = False
                                End If
                            Else
                                CmdApprove.Visible = True
                                CmdDiscard.Visible = False
                            End If
                        End If
                    End If

                    If AgL.StrCmp(TxtStatus.Text, "Active") Then
                        CmdStatus.Image = My.Resources.Lock
                    Else
                        CmdStatus.Image = My.Resources.UnLock
                    End If
                    '---------------------------------------------------
                End With

                RaiseEvent BaseFunction_MoveRec(mSearchCode)
            Else
                BlankText()
            End If
            Topctrl1.FSetDispRec(BMBMaster)
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            DsTemp = Nothing
            TxtStatus.Enabled = True
        End Try
    End Sub

    Private Sub BlankText()
        If Topctrl1.Mode <> "Add" Then Topctrl1.BlankTextBoxes(Me)
        mSearchCode = "" : mInternalCode = ""
        RaiseEvent BaseFunction_BlankText()
    End Sub

    Private Sub DispText(Optional ByVal Enb As Boolean = False)
        'Coding To Enable/Disable Controls
        If FrmType = EntryPointType.Main Then
            If LogSystem Then
                Topctrl1.tAdd = False
                Topctrl1.tEdit = False
                Topctrl1.tDel = False
            End If
            'CmdApprove.Visible = False
            CmdDiscard.Visible = False
            'GBoxApprove.Text = "Approved By"
        Else
            'CmdMoveToLog.Visible = False
            CmdStatus.Visible = False
        End If


        If Not mLogSystem Then
            GBoxApprove.Visible = True
            'GBoxMoveToLog.Visible = False
            'GBoxEntryType.Left = 240
            'GBoxDivision.Left = 470
        End If


        TxtSite_Code.Enabled = False
        TxtV_No.Enabled = False

        If Topctrl1.Mode <> "Add" Then
            TxtV_Type.Enabled = False
        End If

        RaiseEvent BaseFunction_DispText()
    End Sub

    Function RetMain2LogTableColStr(ByVal MainTableName As String, ByVal LogTableName As String) As String
        Dim mQry$
        mQry = "DECLARE @ColStr VARCHAR(Max) " &
        "SET @ColStr='' " &
        "SELECT @ColStr=@ColStr + '" & MainTableName & ".' + C.COLUMN_NAME + ' = " & LogTableName & ".' + C.COLUMN_NAME  + ',' " &
        "FROM INFORMATION_SCHEMA.COLUMNS C  " &
        "WHERE C.TABLE_NAME ='" & LogTableName & "' " &
        "AND C.COLUMN_NAME NOT IN ('UID', 'DocID', 'V_Type', 'V_Prefix', 'V_No', 'Div_Code', 'Site_Code', 'EntryBy', 'EntryDate', 'ApproveBy', 'ApproveDate', 'EntryType', 'EntryStatus', 'MoveToLog', 'MoveToLogDate', 'RowID') " &
        "IF LEN(@ColStr)>0 SET @ColStr=substring (@ColStr,1,len(@ColStr)-1) " &
        " SELECT @ColStr "
        RetMain2LogTableColStr = AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar
    End Function


    Function RetLog2MainTableColStr(ByVal MainTableName As String, ByVal LogTableName As String) As String
        Dim mQry$
        mQry = "DECLARE @ColStr VARCHAR(Max) " &
        "SET @ColStr='' " &
        "SELECT @ColStr=@ColStr + '" & LogTableName & ".' + C.COLUMN_NAME + ' = " & MainTableName & ".' + C.COLUMN_NAME  + ',' " &
        "FROM INFORMATION_SCHEMA.COLUMNS C  " &
        "WHERE C.TABLE_NAME ='" & MainTableName & "' " &
        "AND C.COLUMN_NAME NOT IN ('UID','DocID', 'EntryBy', 'EntryDate', 'ApproveBy', 'ApproveDate', 'EntryType', 'EntryStatus', 'MoveToLog', 'MoveToLogDate', 'IsDeleted', 'RowId') " &
        "IF LEN(@ColStr)>0 SET @ColStr=substring (@ColStr,1,len(@ColStr)-1) " &
        " SELECT @ColStr "
        RetLog2MainTableColStr = AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar
    End Function

    Function RetColStr(ByVal TableName As String) As String
        Dim mQry$
        mQry = "DECLARE @ColStr VARCHAR(Max) " &
        "SET @ColStr='' " &
        "SELECT @ColStr=@ColStr +  C.COLUMN_NAME  + ',' " &
        "FROM INFORMATION_SCHEMA.COLUMNS C  " &
        "WHERE C.TABLE_NAME ='" & TableName & "' " &
        "AND C.COLUMN_NAME NOT IN ('UID', 'IsDeleted', 'RowID') " &
        "IF LEN(@ColStr)>0 SET @ColStr=substring (@ColStr,1,len(@ColStr)-1) " &
        " SELECT @ColStr "
        RetColStr = AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar
    End Function

    Private Sub CmdApprove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdApprove.Click
        Dim mTrans As Boolean
        Dim I As Integer



        If TxtEntryBy.Text = "" Then
            MsgBox("No Action is done on this record. Can't Approve!", MsgBoxStyle.OkOnly, "Approve")
            Exit Sub
        End If


        Try


            If FrmType = EntryPointType.Main Then
                '========================================================
                '====If approve button is pressed in main form, 
                '====just update approved by user name in Main Table
                '========================================================
                mQry = "UPDATE " & MainTableName & " " &
                "   SET  " &
                "" & MainTableName & ".ApproveBy =  " & AgL.Chk_Text(AgL.PubUserName) & ", " &
                "" & MainTableName & ".ApproveDate =  " & AgL.Chk_Text(AgL.GetDateTime(AgL.GcnRead)) & " " &
                "Where " & MainTableName & ".DocID = '" & mSearchCode & "'"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                TxtApproveBy.Text = AgL.PubUserName
                CmdApprove.Visible = False
                CmdDiscard.Visible = True
                Call AgL.LogTableEntry(mSearchCode, Me.Text, "L", AgL.PubMachineName, AgL.PubUserName, AgL.PubLoginDate, AgL.GCn, AgL.ECmd)
            Else
                If TxtEntryType.Text <> "Delete" Then
                    If Data_Validation() = False Then Exit Sub
                End If


                If Not AgL.StrCmp(TxtEntryType.Text, "Delete") Then
                    RaiseEvent BaseEvent_Approve_PreTrans(mSearchCode)
                Else
                    RaiseEvent BaseEvent_ApproveDeletion_PreTrans(mSearchCode)
                End If


                AgL.ECmd = AgL.GCn.CreateCommand
                AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
                AgL.ECmd.Transaction = AgL.ETrans
                mTrans = True


                If Not AgL.StrCmp(TxtEntryType.Text, "Delete") Then

                    '----------------------------------------------------------
                    'Find this record in main table if found then
                    'update old record other wise insert new record
                    '----------------------------------------------------------
                    mQry = " Select Count(*) from " & MainTableName & " Where DocID ='" & mInternalCode & "' "
                    If AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar = 0 Then
                        mQry = "INSERT INTO " & MainTableName & " (UID, DocId, Div_Code, Site_Code, V_Date, V_Type, V_Prefix, V_No, EntryBy, EntryDate,  MoveToLog, MoveToLogDate, ApproveBy, ApproveDate) " &
                               "Select UID, DocId, Div_Code, Site_Code, V_Date, V_Type, V_Prefix, V_No, EntryBy, EntryDate,  Null, Null, " & AgL.Chk_Text(AgL.PubUserName) & ", " & AgL.Chk_Text(AgL.GetDateTime(AgL.GcnRead)) & " From " & LogTableName & "  Where UID = '" & mSearchCode & "' "

                    Else
                        mQry = "UPDATE " & MainTableName & " " &
                        "   SET  " &
                        "" & MainTableName & ".EntryBy =  " & LogTableName & ".entryby, " &
                        "" & MainTableName & ".EntryDate =  " & LogTableName & ".entrydate, " &
                        "" & MainTableName & ".ApproveBy =  " & AgL.Chk_Text(AgL.PubUserName) & ", " &
                        "" & MainTableName & ".ApproveDate =  " & AgL.Chk_Text(AgL.GetDateTime(AgL.GcnRead)) & ", " &
                        "" & MainTableName & ".MoveToLog =  NULL, " &
                        "" & MainTableName & ".MoveToLogDate =  NULL " &
                        "From " & LogTableName & " " &
                        "Where " & MainTableName & ".DocID = " & LogTableName & ".DocId " &
                        "And " & LogTableName & ".UID = '" & mSearchCode & "'"

                    End If
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)



                    mQry = "UPDATE " & MainTableName & " " &
                    "   SET  " & RetMain2LogTableColStr(MainTableName, LogTableName) &
                    " From " & LogTableName & " " &
                    "Where " & MainTableName & ".DocId = " & LogTableName & ".DocID " &
                    "And " & LogTableName & ".UID = '" & mSearchCode & "'"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)




                    '--------------------------------------------------------------
                    'Line Records will be always deleted and insert from Log Table
                    'exceptionally it is referentially integrated with any other table
                    '--------------------------------------------------------------
                    If ArrMainLineTable IsNot Nothing Then
                        For I = 0 To UBound(ArrMainLineTable)
                            If ArrMainLineTable(I) <> "" Then
                                mQry = "Delete from " & ArrMainLineTable(I) & " Where DocID ='" & mInternalCode & "'"
                                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                                mQry = "Insert Into " & ArrMainLineTable(I) & " (" & RetColStr(ArrMainLineTable(I)) & ") " &
                                     "SELECT " & RetColStr(ArrMainLineTable(I)) & " " &
                                     "FROM " & ArrLogLineTable(I) & "   Where UID = '" & mSearchCode & "' "
                                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                            End If
                        Next
                    End If
                    '--------------------------------------------------------------


                    RaiseEvent BaseEvent_Approve_InTrans(mSearchCode, AgL.GCn, AgL.ECmd)
                Else
                    'mQry = "Update " & MainTableName & " Set IsDeleted=1, ApproveBy = " & AgL.Chk_Text(AgL.PubUserName) & ", ApproveDate=" & AgL.Chk_Text(AgL.GetDateTime(AgL.GcnRead)) & ", EntryStatus = " & AgL.Chk_Text(LogStatus.LogApproved) & "  where DocID = '" & mInternalCode & "'"
                    'AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)


                    'Code by akash on date 25-Apr-2012------------------------

                    RaiseEvent BaseEvent_ApproveDeletion_InTrans(mSearchCode, AgL.GCn, AgL.ECmd)

                    If ArrMainLineTable IsNot Nothing Then
                        For I = 0 To UBound(ArrMainLineTable)
                            If ArrMainLineTable(I) <> "" Then
                                mQry = "Delete from " & ArrMainLineTable(I) & " Where DocID ='" & mInternalCode & "'"
                                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                            End If
                        Next
                    End If

                    mQry = "Delete from " & MainTableName & " Where DocID ='" & mInternalCode & "'"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)


                    'End Code BY Akash---------------------------------------------
                End If






                '----------------------------------------------
                'Update that entry is transferred to main table
                '----------------------------------------------
                If LogSystem Then
                    mQry = "Update " & LogTableName & " Set ApproveBy = " & AgL.Chk_Text(AgL.PubUserName) & ", ApproveDate=" & AgL.Chk_Text(AgL.GetDateTime(AgL.GcnRead)) & ", EntryStatus = " & AgL.Chk_Text(LogStatus.LogApproved) & " Where UID = '" & mSearchCode & "' "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    Call AgL.LogTableEntry(mSearchCode, Me.Text, "L", AgL.PubMachineName, AgL.PubUserName, AgL.PubLoginDate, AgL.GCn, AgL.ECmd)
                End If
                '----------------------------------------------



                '----------------------------------------------
                AgL.ETrans.Commit()
                mTrans = False


                If Not AgL.StrCmp(TxtEntryType.Text, "Delete") Then
                    RaiseEvent BaseEvent_Approve_PostTrans(mSearchCode)
                Else
                    RaiseEvent BaseEvent_ApproveDeletion_PostTrans(mSearchCode)
                End If

                FIniMaster()
                MoveRec()
            End If
        Catch ex As Exception
            If mTrans Then AgL.ETrans.Rollback()
            MsgBox(ex.Message, MsgBoxStyle.OkOnly, "Approval")
        End Try
    End Sub

    Sub ProcApporve(ByVal mConn As Object, ByVal mCmd As Object)
        Dim DtTemp As DataTable = Nothing
        Dim I As Integer


        If Not AgL.StrCmp(TxtEntryType.Text, "Delete") Then


            '----------------------------------------------------------
            'Find this record in main table if found then
            'update old record other wise insert new record
            '----------------------------------------------------------
            mQry = " Select Count(*) from " & MainTableName & "  Where DocID ='" & mInternalCode & "' "
            If AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar = 0 Then
                mQry = "INSERT INTO " & MainTableName & " (UID, DocId, Div_Code, Site_Code, V_Date, V_Type, V_Prefix, V_No, EntryBy, EntryDate,  MoveToLog, MoveToLogDate, ApproveBy, ApproveDate) " &
                       "Select UID, DocId, Div_Code, Site_Code, V_Date, V_Type, V_Prefix, V_No, EntryBy, EntryDate,  Null, Null, " & AgL.Chk_Text(AgL.PubUserName) & ", " & AgL.Chk_Text(AgL.GetDateTime(AgL.GcnRead)) & " From " & LogTableName & " Where UID = '" & mSearchCode & "' "

            Else
                mQry = "UPDATE " & MainTableName & " " &
                "   SET  " &
                "" & MainTableName & ".EntryBy =  " & LogTableName & ".entryby, " &
                "" & MainTableName & ".EntryDate =  " & LogTableName & ".entrydate, " &
                "" & MainTableName & ".ApproveBy =  " & AgL.Chk_Text(AgL.PubUserName) & ", " &
                "" & MainTableName & ".ApproveDate =  " & AgL.Chk_Text(AgL.GetDateTime(AgL.GcnRead)) & ", " &
                "" & MainTableName & ".MoveToLog =  NULL, " &
                "" & MainTableName & ".MoveToLogDate =  NULL " &
                "From " & LogTableName & " " &
                "Where " & MainTableName & ".DocID = " & LogTableName & ".DocId " &
                "And " & LogTableName & ".UID = '" & mSearchCode & "'"

            End If
            AgL.Dman_ExecuteNonQry(mQry, mConn, mCmd)



            mQry = "UPDATE " & MainTableName & " " &
            "   SET  " & RetMain2LogTableColStr(MainTableName, LogTableName) &
            " From " & LogTableName & " " &
            "Where " & MainTableName & ".DocId = " & LogTableName & ".DocID " &
            "And " & LogTableName & ".UID = '" & mSearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, mConn, mCmd)




            '--------------------------------------------------------------
            'Line Records will be always deleted and insert from Log Table
            'exceptionally it is referentially integrated with any other table
            '--------------------------------------------------------------
            If ArrMainLineTable IsNot Nothing Then
                For I = 0 To UBound(ArrMainLineTable)
                    If ArrMainLineTable(I) <> "" Then
                        mQry = "Delete from " & ArrMainLineTable(I) & " Where DocID ='" & mInternalCode & "'"
                        AgL.Dman_ExecuteNonQry(mQry, mConn, mCmd)
                        mQry = "Insert Into " & ArrMainLineTable(I) & " (" & RetColStr(ArrMainLineTable(I)) & ") " &
                             "SELECT " & RetColStr(ArrMainLineTable(I)) & " " &
                             "FROM " & ArrLogLineTable(I) & " Where UID = '" & mSearchCode & "' "
                        AgL.Dman_ExecuteNonQry(mQry, mConn, mCmd)
                    End If
                Next
            End If
            '--------------------------------------------------------------




            '----------------------------------------------
            'Update that entry is transferred to main table
            '----------------------------------------------
            If LogSystem Then
                mQry = "Update " & LogTableName & " Set ApproveBy = " & AgL.Chk_Text(AgL.PubUserName) & ", ApproveDate=" & AgL.Chk_Text(AgL.GetDateTime(AgL.GcnRead)) & ", EntryStatus = " & AgL.Chk_Text(LogStatus.LogApproved) & " Where UID = '" & mSearchCode & "' "
                AgL.Dman_ExecuteNonQry(mQry, mConn, mCmd)
            End If
            '----------------------------------------------

            RaiseEvent BaseEvent_Approve_InTrans(mSearchCode, mConn, mCmd)

            mQry = "Update " & LogTableName & " Set ApproveBy = " & AgL.Chk_Text(AgL.PubUserName) & ", ApproveDate=" & AgL.Chk_Text(AgL.GetDateTime(AgL.GcnRead)) & ", EntryStatus = " & AgL.Chk_Text(LogStatus.LogApproved) & " Where UID = '" & mSearchCode & "' "
            AgL.Dman_ExecuteNonQry(mQry, mConn, mCmd)

        Else
            If LogSystem Then
                'mQry = "Update " & MainTableName & " Set IsDeleted=1, ApproveBy = " & AgL.Chk_Text(AgL.PubUserName) & ", ApproveDate=" & AgL.Chk_Text(AgL.GetDateTime(AgL.GcnRead)) & ", EntryStatus = " & AgL.Chk_Text(LogStatus.LogApproved) & "  where DocID = '" & mInternalCode & "'"
                'AgL.Dman_ExecuteNonQry(mQry, mConn, mCmd)

                RaiseEvent BaseEvent_ApproveDeletion_InTrans(mSearchCode, mConn, mCmd)



                If ArrMainLineTable IsNot Nothing Then
                    For I = 0 To UBound(ArrMainLineTable)
                        If ArrMainLineTable(I) <> "" Then
                            mQry = "Delete from " & ArrMainLineTable(I) & " Where DocID ='" & mInternalCode & "'"
                            AgL.Dman_ExecuteNonQry(mQry, mConn, mCmd)
                        End If
                    Next
                End If

                mQry = "Delete from " & MainTableName & " Where DocID ='" & mInternalCode & "'"
                AgL.Dman_ExecuteNonQry(mQry, mConn, mCmd)


                mQry = "Update " & LogTableName & " Set ApproveBy = " & AgL.Chk_Text(AgL.PubUserName) & ", ApproveDate=" & AgL.Chk_Text(AgL.GetDateTime(AgL.GcnRead)) & ", EntryStatus = " & AgL.Chk_Text(LogStatus.LogApproved) & " Where UID = '" & mSearchCode & "' "
                AgL.Dman_ExecuteNonQry(mQry, mConn, mCmd)


            Else
                '--------------------------------------------------------------
                'Line Records will be always deleted
                'exceptionally it is referentially integrated with any other table
                '--------------------------------------------------------------

                RaiseEvent BaseEvent_ApproveDeletion_InTrans(mSearchCode, mConn, mCmd)


                If ArrMainLineTable IsNot Nothing Then
                    For I = 0 To UBound(ArrMainLineTable)
                        If ArrMainLineTable(I) <> "" Then
                            mQry = "Delete from " & ArrMainLineTable(I) & " Where DocID ='" & mInternalCode & "'"
                            AgL.Dman_ExecuteNonQry(mQry, mConn, mCmd)
                        End If
                    Next
                End If


                mQry = "Delete from " & MainTableName & " Where DocID ='" & mInternalCode & "'"
                AgL.Dman_ExecuteNonQry(mQry, mConn, mCmd)

            End If
        End If
    End Sub


    Public Sub FMoveToLog(ByVal Conn As Object, ByVal Cmd As Object, Optional ByVal mEntryType As String = "")
        'Dim mGuid$
        'Dim I As Integer

        ''----------------------------------------------------------
        ''Create new GUID. Insert a new record in log table with OPEN status            
        ''----------------------------------------------------------
        'If LogTableName Is Nothing Then LogTableName = ""
        'If LogTableName = "" Then Exit Sub
        'mGuid = AgL.GetGUID(AgL.GcnRead).ToString


        'If mEntryType <> "" Then
        '    mQry = "INSERT INTO " & LogTableName & " (UID, DocId, EntryBy, EntryDate, EntryType, EntryStatus, MoveToLog, MoveToLogDate) " & _
        '           "Select '" & mGuid & "', " & AgL.Chk_Text(mSearchCode) & ", NULL, NULL, " & AgL.Chk_Text(mEntryType) & ", EntryStatus, " & AgL.Chk_Text(AgL.PubUserName) & ", " & AgL.Chk_Text(AgL.GetDateTime(AgL.GcnRead)) & " From " & MainTableName & " Where DocID = '" & mSearchCode & "' "
        '    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        'Else
        '    mQry = "INSERT INTO " & LogTableName & " (UID, DocId, EntryBy, EntryDate, EntryType, EntryStatus, MoveToLog, MoveToLogDate) " & _
        '           "Select '" & mGuid & "', " & AgL.Chk_Text(mSearchCode) & ", NULL, NULL, " & AgL.Chk_Text(mEntryType) & ", " & AgL.Chk_Text(LogStatus.LogOpen) & ", " & AgL.Chk_Text(AgL.PubUserName) & ", " & AgL.Chk_Text(AgL.GetDateTime(AgL.GcnRead)) & " From " & MainTableName & " Where DocID = '" & mSearchCode & "' "
        '    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        'End If



        'mQry = "Update " & LogTableName & " " & _
        '       "Set  " & RetLog2MainTableColStr(MainTableName, LogTableName) & _
        '       " From " & MainTableName & "  " & _
        '       " Where " & LogTableName & ".UID = '" & mGuid & "' And " & LogTableName & ".DocID = " & MainTableName & ".DocId "
        'AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)


        'If ArrMainLineTable IsNot Nothing Then
        '    For I = 0 To UBound(ArrMainLineTable)
        '        If ArrMainLineTable(I) <> "" Then
        '            mQry = "Insert Into " & ArrLogLineTable(I) & " (UID, " & RetColStr(ArrLogLineTable(I)) & ") " & _
        '                 "SELECT '" & mGuid & "', " & RetColStr(ArrLogLineTable(I)) & " " & _
        '                 "FROM " & ArrMainLineTable(I) & " Where DocID = '" & mSearchCode & "' "
        '            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        '        End If
        '    Next
        'End If


        '--------------------------------------------------------------

    End Sub

    Private Sub CmdMoveToLog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim mTrans As Boolean
        '--------------------------------------------------------------
        '*****  This section will work only if it is a Main form  ******
        '--------------------------------------------------------------
        If FrmType = EntryPointType.Log Then Exit Sub
        '--------------------------------------------------------------



        Try
            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = True




            FMoveToLog(AgL.GCn, AgL.ECmd)

            '----------------------------------------------
            'Update that entry is transferred to main table
            '----------------------------------------------
            mQry = "Update " & MainTableName & " Set MoveToLog = " & AgL.Chk_Text(AgL.PubUserName) & ", MoveToLogDate=" & AgL.Chk_Text(AgL.GetDateTime(AgL.GcnRead)) & " Where DocId = '" & mSearchCode & "' "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            '----------------------------------------------


            TxtMoveToLog.Text = AgL.PubUserName
            'CmdMoveToLog.Enabled = False


            '----------------------------------------------------------

            AgL.ETrans.Commit()
            mTrans = False

        Catch ex As Exception
            If mTrans Then AgL.ETrans.Rollback()
            MsgBox(ex.Message, MsgBoxStyle.OkOnly, "Approval")
        End Try
    End Sub


    Sub Calculation()
        RaiseEvent BaseFunction_Calculation()
    End Sub


    Private Sub PicDiscardBy_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdDiscard.Click
        Dim mTrans As Boolean
        Dim strUnlockReason As String = ""

        '--------------------------------------------------------------
        '*****  This section will work only if it is a log form  ******
        '--------------------------------------------------------------


        Try
            If FrmType = EntryPointType.Main Then
                strUnlockReason = InputBox("Why you want to unlock this record?", "Unlock")
                If strUnlockReason = "" Then Exit Sub
            End If

            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = True





            If FrmType = EntryPointType.Main Then
                '========================================================
                '====If discard button is pressed in main form, 
                '====Step 1 : Only that user who has approved the record or SA can unlock record
                '====Step 2 : Just make blank approved by user field in main table
                '====Step 3 : insert a record to LogTable 
                '========================================================

                mQry = "UPDATE " & MainTableName & " " &
                "   SET  " &
                "" & MainTableName & ".ApproveBy =  Null, " &
                "" & MainTableName & ".ApproveDate =  Null " &
                "Where " & MainTableName & ".DocID = '" & mSearchCode & "'"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                TxtApproveBy.Text = ""
                CmdApprove.Visible = True
                CmdApprove.Enabled = True
                CmdDiscard.Visible = False
                Call AgL.LogTableEntry(mSearchCode, Me.Text, "U", AgL.PubMachineName, AgL.PubUserName, AgL.PubLoginDate, AgL.GCn, AgL.ECmd, strUnlockReason)
            Else
                '----------------------------------------------
                'Update that entry is transferred to main table
                '----------------------------------------------

                mQry = "Update " & LogTableName & " Set EntryStatus = " & AgL.Chk_Text(LogStatus.LogDiscard) & " Where UID = '" & mSearchCode & "' "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)


                mQry = "Update " & MainTableName & " Set EntryStatus = " & AgL.Chk_Text(LogStatus.LogDiscard) & ",MoveToLog = NULL, MoveToLogDate=NULL Where DocID = '" & mInternalCode & "' "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                '----------------------------------------------
            End If
            RaiseEvent BaseEvent_Discard_InTrans(mSearchCode, AgL.GCn, AgL.ECmd)

            AgL.ETrans.Commit()
            mTrans = False


            If FrmType = EntryPointType.Log Then
                FIniMaster()
                MoveRec()
            End If

        Catch ex As Exception
            If mTrans Then AgL.ETrans.Rollback()
            MsgBox(ex.Message, MsgBoxStyle.OkOnly, "Discard")
        End Try

    End Sub

    'Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdStatus.Click
    '    If FrmType = EntryPointType.Log Then
    '        If mSearchCode <> "" Then
    '            If MsgBox("Sure to change status of selected record?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
    '                TxtEntryBy.Text = AgL.PubUserName
    '                TxtEntryType.Text = "STATUS"
    '                If AgL.StrCmp(TxtStatus.Text, "Inactive") Then
    '                    TxtStatus.Text = "Active"
    '                Else
    '                    TxtStatus.Text = "Inactive"
    '                End If
    '                mQry = "Update " & LogTableName & " Set Status = " & AgL.Chk_Text(TxtStatus.Text) & ", EntryBy = " & AgL.Chk_Text(TxtEntryBy.Text) & ", EntryDate = " & AgL.Chk_Text(AgL.GetDateTime(AgL.GcnRead)) & ", EntryType = " & AgL.Chk_Text(TxtEntryType.Text) & " Where UID = '" & mSearchCode & "' "
    '                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
    '            End If
    '        End If
    '    Else
    '        MsgBox("Status Can be changed on Log Entry Only.")
    '    End If
    'End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdStatus.Click
        If FrmType = EntryPointType.Main Then
            If mSearchCode <> "" Then
                If MsgBox("Sure to change status of selected record?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    TxtEntryBy.Text = AgL.PubUserName
                    TxtEntryType.Text = "STATUS"

                    mQry = "Update " & MainTableName & " " &
                            " Set " &
                            " Status = " & AgL.Chk_Text(IIf(TxtStatus.Text = "", ClsMain.EntryStatus.Active, TxtStatus.Text)) & ", " &
                            " EntryBy = " & AgL.Chk_Text(TxtEntryBy.Text) & ", " &
                            " EntryDate = " & AgL.Chk_Text(AgL.GetDateTime(AgL.GcnRead)) & ", " &
                            " Where DocID = '" & mSearchCode & "' "

                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

                    '--------------------------------------------------------------
                    'Create a log entry of status change
                    '--------------------------------------------------------------
                    Call AgL.LogTableEntry(mSearchCode, Me.Text, "S", AgL.PubMachineName, AgL.PubUserName, AgL.PubLoginDate, AgL.GCn, AgL.ECmd, "Old Status : " & CmdStatus.Tag & "  New Status : " & TxtStatus.Text)
                    '--------------------------------------------------------------

                End If
            End If
        Else
            MsgBox("Status Can be changed on Log Entry Only.")
        End If
    End Sub

    Private Sub Control_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles _
        TxtDocId.Validating, TxtSite_Code.Validating, TxtV_Type.Validating, TxtV_Date.Validating, TxtV_No.Validating

        Dim DtTemp As DataTable = Nothing
        Dim DrTemp As DataRow() = Nothing
        Try
            Select Case sender.NAME
                Case TxtV_Type.Name

                    Validating_VType(sender)
                Case TxtV_Date.Name
                    If TxtV_Date.Text.Trim = "" Then TxtV_Date.Text = Format(CDate(AgL.PubLastTransactionDate), "dd/MMM/yyyy")
                    If LblV_Type.Tag <> Ncat.OpeningBalance Then
                        TxtV_Date.Text = AgL.RetDateFinYear(TxtV_Date.Text)
                    End If
                    If TxtV_Date.Text <> "" Then
                        If AgL.StrCmp(Topctrl1.Mode, "Add") Then

                            If DrVoucherTypeDateLock IsNot Nothing Then
                                If DrVoucherTypeDateLock.Length > 0 Then
                                    If AgL.XNull(DrVoucherTypeDateLock(0)("LockTillDate")) <> "" Then
                                        If CDate(TxtV_Date.Text) <= CDate((AgL.XNull(DrVoucherTypeDateLock(0)("LockTillDate")))) Then
                                            MsgBox("Entries are locked till date " & Format(CDate(AgL.XNull(DrVoucherTypeDateLock(0)("LockTillDate"))), "dd/MMM/yyyy"))
                                            Topctrl1.FButtonClick(14, True)
                                            Exit Sub
                                        End If
                                    End If
                                End If
                            End If

                            If DrFinancialYearDateLock IsNot Nothing Then
                                If DrFinancialYearDateLock.Length > 0 Then
                                    If CType(AgL.VNull(DrFinancialYearDateLock(0)("IsLocked")), Boolean) = True Then
                                        If AgL.XNull(DrFinancialYearDateLock(0)("End_Dt")) <> "" Then
                                            If CDate(TxtV_Date.Text) <= CDate((AgL.XNull(DrFinancialYearDateLock(0)("End_Dt")))) Then
                                                MsgBox("Financial year " & AgL.XNull(DrFinancialYearDateLock(0)("cyear")) & " is locked.", MsgBoxStyle.Information)
                                                Topctrl1.FButtonClick(14, True)
                                                Exit Sub
                                            End If
                                        End If
                                    End If
                                End If
                            End If


                            If DrVoucherTypeTimePlan IsNot Nothing Then
                                If Not AgL.StrCmp(AgL.PubUserName, "Sa") And Not AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Then
                                    If DrVoucherTypeTimePlan.Length > 0 Then
                                        If AgL.VNull(DrVoucherTypeTimePlan(0)("DayLimitAdd")) <> 0 Then
                                            If DateDiff(DateInterval.Day, CDate(TxtV_Date.Text), CDate(AgL.PubLoginDate)) > AgL.VNull(DrVoucherTypeTimePlan(0)("DayLimitAdd")) - 1 Then
                                                If FIsAllowedByTimePlan(AgL.PubUserName, mSearchCode, EntryAction.Add) = False Then
                                                    If MsgBox("Adding is locked for date " & TxtV_Date.Text + ".Do you want to request for permission ?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                                                        FRequestForPermission(EntryAction.Add)
                                                    End If
                                                    Topctrl1.FButtonClick(14, True)
                                                    Exit Sub
                                                End If
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        End If

                        If AgL.XNull(TxtV_Type.Tag) <> "" Then
                            If FGetVoucher_Type_ManualRefType(TxtV_Type.Tag) = "Day Wise" Then
                                TxtReferenceNo.Text = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", MainTableName, TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, AgTemplate.ClsMain.ManualRefType.Max)
                            End If
                        End If
                    End If
                    If FDivisionNameForCustomization(12) = "MAA KI KRIPA" Then
                        AgL.PubLastTransactionDate = TxtV_Date.Text
                    Else
                        AgL.PubLastTransactionDate = AgL.PubLoginDate
                    End If

            End Select

            'Call Calculation()

            If Topctrl1.Mode = "Add" And TxtV_Type.AgSelectedValue.Trim <> "" And TxtV_Date.Text.Trim <> "" And TxtSite_Code.Text.Trim <> "" Then
                'mInternalCode = AgL.GetDocId(TxtV_Type.AgSelectedValue, CStr(TxtV_No.Text), CDate(TxtV_Date.Text), AgL.GCn, AgL.PubDivCode, TxtSite_Code.AgSelectedValue)
                mInternalCode = AgL.CreateDocId(AgL, MainTableName, TxtV_Type.AgSelectedValue, CStr(TxtV_No.Text), CDate(TxtV_Date.Text), AgL.GCn, AgL.PubDivCode, TxtSite_Code.AgSelectedValue)
                IncrementVoucherCounterIfExist(mInternalCode)
                TxtDocId.Text = mInternalCode
                TxtV_No.Text = Val(AgL.DeCodeDocID(mInternalCode, AgLibrary.ClsMain.DocIdPart.VoucherNo))
                LblPrefix.Text = AgL.DeCodeDocID(mInternalCode, AgLibrary.ClsMain.DocIdPart.VoucherPrefix)
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            If DtTemp IsNot Nothing Then DtTemp.Dispose()
        End Try
    End Sub

    Public Sub Validating_VType(ByVal Sender As Object)
        Dim DrTemp As DataRow() = Nothing
        Dim dtTemp As DataTable

        If Sender.text.ToString.Trim = "" Or Sender.AgSelectedValue.Trim = "" Then
            LblV_Type.Tag = ""
        Else
            If Sender.AgHelpDataSet IsNot Nothing Then
                mQry = "Select V_Type as Code, Description, NCat, Category, IfNull(Status,'" & ClsMain.EntryStatus.Active & "') As Status, IsFutureDateTransactionAllowed " &
                   "From Voucher_Type " &
                   "Where V_Type='" & Sender.tag & "'"

                dtTemp = AgL.FillData(mQry, AgL.GcnRead).Tables(0)

                'DrTemp = Sender.AgHelpDataSet.Tables(0).Select("Code = " & AgL.Chk_Text(Sender.AgSelectedValue) & "")
                'LblV_Type.Tag = AgL.XNull(DrTemp(0)("NCat"))
                'VoucherCategory = AgL.XNull(DrTemp(0)("Category"))
                'mIsFutureDateTransactionAllowed = CType(AgL.VNull(DrTemp(0)("IsFutureDateTransactionAllowed")), Boolean)
                LblV_Type.Tag = AgL.XNull(dtTemp.Rows(0)("NCat"))
                VoucherCategory = AgL.XNull(dtTemp.Rows(0)("Category"))
                mIsFutureDateTransactionAllowed = CType(AgL.VNull(dtTemp.Rows(0)("IsFutureDateTransactionAllowed")), Boolean)
                DrVoucherTypeDateLock = AgL.PubDtVoucherTypeDateLock.Select("Category = " & AgL.Chk_Text(VoucherCategory) & "")
                DrVoucherTypeTimePlan = AgL.PubDtVoucherTypeTimePlan.Select("Category = " & AgL.Chk_Text(VoucherCategory) & "")
                DrFinancialYearDateLock = AgL.PubDtFinancialYearDateLock.Select("Comp_Code = " & AgL.Chk_Text(AgL.PubCompCode) & "
                            And Site_Code = " & AgL.Chk_Text(AgL.PubSiteCode) & "
                            And Div_Code = " & AgL.Chk_Text(AgL.PubDivCode) & "")


                If AgL.StrCmp(Topctrl1.Mode, "Add") And AgL.XNull(dtTemp.Rows(0)("Status")) = "InActive" Then
                    MsgBox("Voucher Type is not active", MsgBoxStyle.Exclamation)
                    Topctrl1.FButtonClick(14, True)
                    Exit Sub
                End If

            End If
        End If

        TxtV_Type.AgLastValueTag = TxtV_Type.Tag
        TxtV_Type.AgLastValueText = TxtV_Type.Text

        If AgL.XNull(TxtV_Type.Tag) <> "" Then
            If FGetVoucher_Type_ManualRefType(TxtV_Type.Tag) = "Day Wise" Then
                TxtReferenceNo.Enabled = False
            End If
        End If
    End Sub

    Private Sub Topctrl1_Load(sender As Object, e As EventArgs) Handles Topctrl1.Load

    End Sub

    Private Function Data_Validation() As Boolean
        Dim I As Integer = 0, J As Integer = 0
        Dim bStudentCode$ = ""
        Try

            If AgL.PubKillerDate <> "" Then
                If CDate(TxtV_Date.Text) > CDate(AgL.PubKillerDate) Then
                    MsgBox("Software validity expired. Can not add new record.")
                    Exit Function
                End If
            End If

            If DrVoucherTypeDateLock IsNot Nothing Then
                If DrVoucherTypeDateLock.Length > 0 Then
                    If AgL.XNull(DrVoucherTypeDateLock(0)("LockTillDate")) <> "" Then
                        If CDate(TxtV_Date.Text) <= CDate((AgL.XNull(DrVoucherTypeDateLock(0)("LockTillDate")))) Then
                            MsgBox("Entries are locked till date " & Format(CDate(AgL.XNull(DrVoucherTypeDateLock(0)("LockTillDate"))), "dd/MMM/yyyy"), MsgBoxStyle.Information)
                            Exit Function
                        End If
                    End If
                End If
            End If

            If DrFinancialYearDateLock IsNot Nothing Then
                If DrFinancialYearDateLock.Length > 0 Then
                    If CType(AgL.VNull(DrFinancialYearDateLock(0)("IsLocked")), Boolean) = True Then
                        If AgL.XNull(DrFinancialYearDateLock(0)("End_Dt")) <> "" Then
                            If CDate(TxtV_Date.Text) <= CDate((AgL.XNull(DrFinancialYearDateLock(0)("End_Dt")))) Then
                                MsgBox("Financial year " & AgL.XNull(DrFinancialYearDateLock(0)("cyear")) & " is locked.", MsgBoxStyle.Information)
                                Exit Function
                            End If
                        End If
                    End If
                End If
            End If


            Dim ChildDataPassed As Boolean = True

            Call Calculation()

            If AgL.RequiredField(TxtSite_Code) Then Exit Function
            If AgL.RequiredField(TxtV_Type, LblV_Type.Text) Then Exit Function
            If AgL.RequiredField(TxtV_Date, LblV_Date.Text) Then Exit Function
            If mRestrictFinancialYearRecord And LblV_Type.Tag <> Ncat.OpeningBalance Then If Not AgL.IsValidDate(TxtV_Date, AgL.PubStartDate, AgL.PubEndDate) Then Exit Function
            If CDate(TxtV_Date.Text) > CDate(AgL.PubLoginDate) And CDate(TxtV_Date.Text) > CDate(AgL.PubStartDate) Then
                If mIsFutureDateTransactionAllowed = False Then
                    MsgBox("Future date transaction is not allowed.")
                    TxtV_Date.Focus()
                    Exit Function
                End If
            End If

            If Not AgCL.AgCheckMandatory(Me) Then Exit Function


            If Topctrl1.Mode = "Add" Then
                If LogSystem Then
                    mSearchCode = AgL.GetGUID(AgL.GCn).ToString
                    'mInternalCode = AgL.GetDocId(TxtV_Type.AgSelectedValue, CStr(TxtV_No.Text), CDate(TxtV_Date.Text), AgL.GCn, AgL.PubDivCode, TxtSite_Code.AgSelectedValue)
                    mInternalCode = AgL.CreateDocId(AgL, MainTableName, TxtV_Type.AgSelectedValue, CStr(TxtV_No.Text), CDate(TxtV_Date.Text), AgL.GCn, AgL.PubDivCode, TxtSite_Code.AgSelectedValue)
                    IncrementVoucherCounterIfExist(mInternalCode)
                Else
                    'mSearchCode = AgL.GetDocId(TxtV_Type.AgSelectedValue, CStr(TxtV_No.Text), CDate(TxtV_Date.Text), AgL.GCn, AgL.PubDivCode, TxtSite_Code.AgSelectedValue)
                    mSearchCode = AgL.CreateDocId(AgL, MainTableName, TxtV_Type.AgSelectedValue, CStr(TxtV_No.Text), CDate(TxtV_Date.Text), AgL.GCn, AgL.PubDivCode, TxtSite_Code.AgSelectedValue)
                    IncrementVoucherCounterIfExist(mSearchCode)
                    mInternalCode = mSearchCode
                End If
                TxtV_No.Text = Val(AgL.DeCodeDocID(mInternalCode, AgLibrary.ClsMain.DocIdPart.VoucherNo))
                LblPrefix.Text = AgL.DeCodeDocID(mInternalCode, AgLibrary.ClsMain.DocIdPart.VoucherPrefix)

                If mInternalCode <> TxtDocId.Text Then
                    'MsgBox("DocId : " & TxtDocId.Text & " Already Exist New DocId Alloted : " & mInternalCode & "")
                    TxtDocId.Text = mInternalCode
                End If

                If TxtV_Date.AgLastValueText <> TxtV_Date.Text Then
                    If FGetVoucher_Type_ManualRefType(TxtV_Type.Tag) = "Day Wise" Then
                        TxtReferenceNo.Text = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", MainTableName, TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, AgTemplate.ClsMain.ManualRefType.Max)
                    End If
                End If
            End If

            If Topctrl1.Mode = "Add" Then
                If FrmType = EntryPointType.Log Then
                    mQry = "Select count(*) From " & LogTableName & " Where DocID='" & mInternalCode & "'  "
                    If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Entry No. Already Exists in Log File")
                Else
                    mQry = "Select count(*) From " & MainTableName & " Where DocID='" & mInternalCode & "'  "
                    If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Entry No. Already Exists")
                End If
            End If

            RaiseEvent BaseEvent_Data_Validation(ChildDataPassed)
            If ChildDataPassed Then
                Data_Validation = True
            Else
                Data_Validation = False
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " In Data_Validation function of AgTemplate ")
            Data_Validation = False
        End Try
    End Function

    Public Overridable Sub Topctrl_tbAdd() Handles Topctrl1.tbAdd
        If AgL.PubKillerDate <> "" Then
            If DateTime.Now > CDate(AgL.PubKillerDate) Then
                MsgBox("Software validity expired. Can not add new record.")
                Topctrl1.FButtonClick(14, True)
                Exit Sub
            End If
        End If



        Dim StrVPrefixStatus As String
        BlankText()
        DispText(True)
        TxtSite_Code.AgSelectedValue = AgL.PubSiteCode
        TxtDivision.AgSelectedValue = AgL.PubDivCode
        TxtStatus.Text = ClsMain.EntryStatus.Active
        If TxtV_Type.AgHelpDataSet.Tables(0).Rows.Count = 1 Then
            TxtV_Type.AgSelectedValue = TxtV_Type.AgHelpDataSet.Tables(0).Rows(0)("Code")
            LblV_Type.Tag = AgL.XNull(TxtV_Type.AgHelpDataSet.Tables(0).Rows(0)("NCat"))
            VoucherCategory = AgL.XNull(TxtV_Type.AgHelpDataSet.Tables(0).Rows(0)("Category"))
            TxtV_Type.Enabled = False
            DrVoucherTypeDateLock = AgL.PubDtVoucherTypeDateLock.Select("Category = " & AgL.Chk_Text(VoucherCategory) & "")
            DrVoucherTypeTimePlan = AgL.PubDtVoucherTypeTimePlan.Select("Category = " & AgL.Chk_Text(VoucherCategory) & "")
            DrFinancialYearDateLock = AgL.PubDtFinancialYearDateLock.Select("Comp_Code = " & AgL.Chk_Text(AgL.PubCompCode) & "
                            And Site_Code = " & AgL.Chk_Text(AgL.PubSiteCode) & "
                            And Div_Code = " & AgL.Chk_Text(AgL.PubDivCode) & "")
            TxtV_Date.Focus()
        Else
            TxtV_Type.Enabled = True
            TxtV_Type.Tag = IIf(TxtV_Type.AgLastValueTag Is Nothing, "", TxtV_Type.AgLastValueTag)
            TxtV_Type.Text = IIf(TxtV_Type.AgLastValueText Is Nothing, "", TxtV_Type.AgLastValueText)
            TxtV_Type.Focus()
        End If

        If CDate(AgL.PubLoginDate) > CDate(AgL.PubEndDate) Then
            TxtV_Date.Text = AgL.PubEndDate
        Else
            TxtV_Date.Text = Format(CDate(AgL.PubLastTransactionDate), "dd/MMM/yyyy")
        End If


        If TxtV_Date.Text <> "" Then
            If DrVoucherTypeDateLock IsNot Nothing Then
                If DrVoucherTypeDateLock.Length > 0 Then
                    If AgL.XNull(DrVoucherTypeDateLock(0)("LockTillDate")) <> "" Then
                        If CDate(TxtV_Date.Text) <= CDate((AgL.XNull(DrVoucherTypeDateLock(0)("LockTillDate")))) Then
                            MsgBox("Entries are locked till date " & Format(CDate(AgL.XNull(DrVoucherTypeDateLock(0)("LockTillDate"))), "dd/MMM/yyyy"))
                            Topctrl1.FButtonClick(14, True)
                            Exit Sub
                        End If
                    End If
                End If
            End If
        End If




        If TxtV_Date.Text <> "" Then
            If DrVoucherTypeTimePlan IsNot Nothing Then
                If Not AgL.StrCmp(AgL.PubUserName, "Sa") And Not AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Then
                    If DrVoucherTypeTimePlan.Length > 0 Then
                        If AgL.VNull(DrVoucherTypeTimePlan(0)("DayLimitAdd")) <> 0 Then
                            If DateDiff(DateInterval.Day, CDate(TxtV_Date.Text), CDate(AgL.PubLoginDate)) > AgL.VNull(DrVoucherTypeTimePlan(0)("DayLimitAdd")) - 1 Then
                                If FIsAllowedByTimePlan(AgL.PubUserName, mSearchCode, EntryAction.Add) = False Then
                                    If MsgBox("Adding is locked for date " & TxtV_Date.Text + ".Do you want to request for permission ?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                                        FRequestForPermission(EntryAction.Add)
                                    End If
                                    Topctrl1.FButtonClick(14, True)
                                    Exit Sub
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        End If


        'StrVPrefixStatus = AgL.Dman_Execute("SELECT IfNull(Status_Add,'" & AgTemplate.ClsMain.EntryStatus.Active & "'), Date_From , Date_To , Prefix  FROM Voucher_Prefix WHERE Date_From <= '" & TxtV_Date.Text & "' AND Date_To >= '" & TxtV_Date.Text & "' And V_Type = '" & TxtV_Type.Tag & "' And Site_Code = '" & AgL.PubSiteCode & "'", AgL.GCn).ExecuteScalar
        'If StrVPrefixStatus <> AgTemplate.ClsMain.EntryStatus.Active Then
        '    If AgL.PubUserName.ToUpper = AgLibrary.ClsConstant.PubSuperUserName Or AgL.PubUserName.ToUpper = "SA" Then
        '        If MsgBox("Entry is " & StrVPrefixStatus & " for Date " & TxtV_Date.Text & ", Do you want to continue?", MsgBoxStyle.DefaultButton2 + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
        '            Topctrl1.FButtonClick(14, True)
        '            Exit Sub
        '        End If
        '    Else
        '        MsgBox("Entry is " & StrVPrefixStatus & " for Date " & TxtV_Date.Text)
        '        Topctrl1.FButtonClick(14, True)
        '        Exit Sub
        '    End If
        'End If



        If Topctrl1.Mode = "Add" And TxtV_Type.Tag.Trim <> "" And TxtV_Date.Text.Trim <> "" And TxtSite_Code.Text.Trim <> "" Then
            'mInternalCode = AgL.GetDocId(TxtV_Type.Tag, CStr(TxtV_No.Text), CDate(TxtV_Date.Text), AgL.GCn, AgL.PubDivCode, TxtSite_Code.Tag)
            mInternalCode = AgL.CreateDocId(AgL, MainTableName, TxtV_Type.Tag, CStr(TxtV_No.Text), CDate(TxtV_Date.Text), AgL.GCn, AgL.PubDivCode, TxtSite_Code.Tag)
            IncrementVoucherCounterIfExist(mInternalCode)
            TxtDocId.Text = mInternalCode
            TxtV_No.Text = Val(AgL.DeCodeDocID(mInternalCode, AgLibrary.ClsMain.DocIdPart.VoucherNo))
            LblPrefix.Text = AgL.DeCodeDocID(mInternalCode, AgLibrary.ClsMain.DocIdPart.VoucherPrefix)
        End If

        If AgL.XNull(TxtV_Type.Tag) <> "" Then
            If FGetVoucher_Type_ManualRefType(TxtV_Type.Tag) = "Day Wise" Then
                TxtReferenceNo.Enabled = False
            End If
        End If

        mFlagSaveAllowed = True
        RaiseEvent BaseEvent_Topctrl_tbAdd()
    End Sub



    Private Sub CreateHelpDataSets()
        RaiseEvent BaseFunction_CreateHelpDataSet()
    End Sub

    Public Sub FindMove(ByVal bDocId As String)
        Try
            If bDocId <> "" Then
                AgL.PubSearchRow = bDocId
                If AgL.PubMoveRecApplicable Then
                    AgL.PubDRFound = DTMaster.Rows.Find(AgL.PubSearchRow)
                    BMBMaster.Position = DTMaster.Rows.IndexOf(AgL.PubDRFound)
                End If
                Call MoveRec()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Topctrl1_tbSite() Handles Topctrl1.tbSite
        RaiseEvent BaseEvent_Topctrl_tbMore()
    End Sub

    Private Function FIsAllowedByTimePlan(UserName As String, SearchCode As String, Action As String)
        mQry = "SELECT Count(*)
                FROM PermissionRequest H
                WHERE H.EntryBy = '" & UserName & "' 
                And (H.DocId = '" & SearchCode & "' OR H.V_Date BETWEEN H.FromDate AND H.ToDate)
                And H.ExpiryDate <= " & AgL.Chk_Date(AgL.PubLoginDate) & "
                And H.Action = '" & Action & "'
                And H.ApproveBy Is Not Null "
        If AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar()) = 0 Then
            FIsAllowedByTimePlan = False
        Else
            FIsAllowedByTimePlan = True
        End If
    End Function
    Protected Sub FRequestForPermission(bAction As String)
        Dim FrmObj As New FrmPermissionRequest()
        FrmObj.V_Type = TxtV_Type.Tag
        FrmObj.V_Date = TxtV_Date.Text
        FrmObj.Div_Code = TxtDivision.Tag
        FrmObj.Site_Code = TxtSite_Code.Tag
        FrmObj.Action = bAction
        If bAction <> EntryAction.Add Then
            FrmObj.SearchCode = mSearchCode
            FrmObj.ManualRefNo = TxtReferenceNo.Text
        End If
        FrmObj.StartPosition = FormStartPosition.CenterParent
        FrmObj.ShowDialog()
    End Sub
    Public Sub FRefreshMovRec()
        FIniMaster()
        Ini_List()
        MoveRec()
    End Sub

    Private Function FGetVoucher_Type_ManualRefType(V_Type As String) As String
        Dim ManualRefType_VType As String = AgL.XNull(AgL.Dman_Execute("Select ManualRefType 
                            From Voucher_Type With (NoLock) 
                            Where V_Type = '" & V_Type & "'", AgL.GcnRead).ExecuteScalar())
        FGetVoucher_Type_ManualRefType = ManualRefType_VType
    End Function
    Private Sub IncrementVoucherCounterIfExist(ByRef bDocId As String)
        If AgL.VNull(AgL.Dman_Execute("Select Count(*) From " & MainTableName & " Where DocID='" & mInternalCode & "'  ", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar) > 0 Then
            mQry = "Select 'UPDATE Voucher_Prefix Set Start_Srl_No = ' || CAST(Max(H.V_No) AS NVARCHAR) || ' Where V_Type = ' || '''' || H.V_Type || ''''
                || ' And ' || ''''  || 
                " & IIf(AgL.PubServerName <> "", "Replace(CONVERT(VARCHAR(11), Max(H.V_Date), 106),' ','/')", "DATE(H.V_Date)") & "  
                || '''' || '  BETWEEN DATE(Date_From) AND DATE(Date_To) ' AS Qry,
                H.V_Type, H.V_Prefix, Max(H.V_No) As V_No  , C.cyear
                From " & MainTableName & " H
                LEFT JOIN Company C ON DATE(H.V_Date) BETWEEN DATE(C.Start_Dt) AND DATE(C.End_Dt)
                WHERE H.V_Type In ('" & TxtV_Type.Tag & "')
                GROUP BY H.V_Type, H.V_Prefix, C.cyear "
            Dim DtTemp As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
            For I As Integer = 0 To DtTemp.Rows.Count - 1
                If AgL.XNull(DtTemp.Rows(I)("Qry")) <> "" Then
                    AgL.Dman_ExecuteNonQry(AgL.XNull(DtTemp.Rows(I)("Qry")), AgL.GCn)
                End If
            Next
            'bDocId = AgL.GetDocId(TxtV_Type.Tag, CStr(TxtV_No.Text), CDate(TxtV_Date.Text), AgL.GCn, AgL.PubDivCode, TxtSite_Code.AgSelectedValue)
            bDocId = AgL.CreateDocId(AgL, MainTableName, TxtV_Type.Tag, CStr(TxtV_No.Text), CDate(TxtV_Date.Text), AgL.GCn, AgL.PubDivCode, TxtSite_Code.AgSelectedValue)
        End If
    End Sub
    Protected Sub FShowLedgerPosting()
        mQry = "SELECT SG.Name As AccountName, CSg.Name AS Contra, L.AmtDr, L.AmtCr
                FROM Ledger L 
                LEFT JOIN Subgroup SG ON L.SubCode = Sg.Subcode
                LEFT JOIN Subgroup CSg ON L.ContraSub = CSg.Subcode
                WHERE L.DocId = '" & mSearchCode & "' "
        Dim Frmbj As AgLibrary.FrmFind = New AgLibrary.FrmFind(mQry, "Ledger Posting For This Entry.", AgL)
        Frmbj.ShowDialog()
    End Sub
End Class