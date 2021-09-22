Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.SQLite
Public Class TempMaster
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
    Public Event BaseEvent_Approve_InTrans(ByVal SearchCode As String, ByVal Conn As Object, ByVal Cmd As Object)
    Public Event BaseEvent_Approve_PostTrans(ByVal SearchCode As String)

    Public Event BaseEvent_ApproveDeletion_PreTrans(ByVal SearchCode As String)
    Public Event BaseEvent_ApproveDeletion_InTrans(ByVal SearchCode As String, ByVal Conn As Object, ByVal Cmd As Object)
    Public Event BaseEvent_ApproveDeletion_PostTrans(ByVal SearchCode As String)

    Public Event BaseEvent_Topctrl_tbAdd()
    Public Event BaseEvent_Topctrl_tbEdit(ByRef Passed As Boolean)
    Public Event BaseEvent_Topctrl_tbDel(ByRef Passed As Boolean)
    Public Event BaseEvent_Topctrl_tbPrn(ByVal SearchCode As String)
    Public Event BaseEvent_Topctrl_tbRef()
    Public Event BaseEvent_Topctrl_tbMore()

    Public DTMaster As New DataTable()
    Public BMBMaster As BindingManagerBase
    Private KEAMainKeyCode As System.Windows.Forms.KeyEventArgs

    Dim mQry As String = ""
    Public mSearchCode As String = "", mInternalCode As String = ""

    Dim mFrmType As EntryPointType = EntryPointType.Main
    Dim mMainTableName As String
    Dim mLogTableName As String = ""
    Dim mMainLineTableCSV As String = ""
    Dim mLogLineTableCSV As String
    Dim mLineTableSearchKeyCSV As String
    Dim ArrMainLineTable As String()
    Dim ArrLogLineTable As String()
    Dim ArrLineTableSearchKey(0) As String
    Dim mPrimaryField As String = "Code"
    Protected mLogSystem As Boolean = False
    Dim mEntryPointIniMode As ClsMain.EntryPointIniMode = ClsMain.EntryPointIniMode.Browse

    Protected mFrmObjBeforeModification As TempMaster
    Protected mLogText As String = ""
    Dim ControlKeyPressed As Boolean

    Public Enum EntryPointType
        Main
        Log
    End Enum

    Public Class LogStatus
        Public Const LogOpen As String = "Open"
        Public Const LogDiscard As String = "Discard"
        Public Const LogApproved As String = "Approved"
    End Class


    Public Property LineTableSearchKeyCsv() As String
        Get
            Return mLineTableSearchKeyCSV
        End Get
        Set(ByVal value As String)
            mLineTableSearchKeyCSV = value

            ArrLineTableSearchKey = Split(mLineTableSearchKeyCSV, ",")
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


    Public Property LogTableName1() As String
        Get
            Return mLogTableName
        End Get
        Set(ByVal value As String)
            mLogTableName = value
        End Set
    End Property

    Public Property EntryPointIniMode() As ClsMain.EntryPointIniMode
        Get
            Return mEntryPointIniMode
        End Get
        Set(ByVal value As ClsMain.EntryPointIniMode)
            mEntryPointIniMode = value
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

    Public Property LogSystem1() As Boolean
        Get
            Return mLogSystem
        End Get
        Set(ByVal value As Boolean)
            mLogSystem = value
        End Set
    End Property

    Public Property PrimaryField() As String
        Get
            Return mPrimaryField
        End Get
        Set(ByVal value As String)
            mPrimaryField = value
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
        ControlKeyPressed = e.Control
        If TypeOf (Me.ActiveControl) Is TextBox Then
            If CType(Me.ActiveControl, TextBox).Multiline Then
                If e.Control = False Then
                    If e.KeyCode = Keys.Return Then SendKeys.Send("{Tab}")
                End If
            Else
                If e.KeyCode = Keys.Return Then SendKeys.Send("{Tab}")
            End If
        ElseIf Me.ActiveControl IsNot Nothing Then
            If Me.ActiveControl.Name <> Topctrl1.Name And
                Not (TypeOf (Me.ActiveControl) Is AgControls.AgDataGrid) Then
                If e.KeyCode = Keys.Return Then SendKeys.Send("{Tab}")
            End If
        End If
    End Sub

    Sub KeyPress_Form(ByVal Sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        If e.KeyChar = Chr(Keys.Escape) Then Exit Sub
        If Me.ActiveControl Is Nothing Then Exit Sub
        AgL.CheckQuote(e)

        If e.KeyChar = Chr(Keys.Enter) And TypeOf (Me.ActiveControl) Is TextBox Then
            If CType(Me.ActiveControl, TextBox).Multiline Then
                If Not ControlKeyPressed Then
                    If CType((Me.ActiveControl), TextBox).Multiline = True Then e.KeyChar = ""
                End If
            Else
                If CType((Me.ActiveControl), TextBox).Multiline = True Then e.KeyChar = ""
            End If
        End If

        ControlKeyPressed = False
    End Sub

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            '----------------------------------------------------------
            '-----This Event will Contain TableName Property Assignment
            '----------------------------------------------------------
            RaiseEvent BaseEvent_Form_PreLoad()
            '----------------------------------------------------------
            'Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
            If mEntryPointIniMode = ClsMain.EntryPointIniMode.Insertion Then
                CreateHelpDataSets()
                Ini_List()
                Topctrl1.FButtonClick(0)
            Else
                CreateHelpDataSets()
                IniGrid()
                FIniMaster()
                Ini_List()
                DispText()
                MoveRec()
                Me.Left = 0
                Me.Top = 0
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Public Sub FIniMaster(Optional ByVal BytDel As Byte = 0, Optional ByVal BytRefresh As Byte = 1)
        If mEntryPointIniMode <> ClsMain.EntryPointIniMode.Insertion Then
            If FrmType = EntryPointType.Main Then
                '---------------------------------------
                'Condition when Entry point Type is Main
                '---------------------------------------
                RaiseEvent BaseFunction_FIniMast(BytDel, BytRefresh)
            Else
                '---------------------------------------
                'Condition when Entry point Type is LOG
                '---------------------------------------
                RaiseEvent BaseFunction_FIniMastLog(BytDel, BytRefresh)
            End If
        End If
    End Sub

    Sub Ini_List()
        Try
            If AgL Is Nothing Then Exit Sub

            mQry = "Select Div_Code, Div_Name From Division Order By Div_Name"
            TxtDivision.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)

            mQry = "Select '" & ClsMain.EntryStatus.Active & "' As Code, '" & ClsMain.EntryStatus.Active & "' As Description " &
                    " Union All Select '" & ClsMain.EntryStatus.Inactive & "' As Code, '" & ClsMain.EntryStatus.Inactive & "' As Description "
            TxtStatus.AgHelpDataSet(0, GroupBox2.Top - 150, GroupBox2.Left) = AgL.FillData(mQry, AgL.GCn)

            RaiseEvent BaseFunction_FIniList()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Topctrl1_tbAdd() Handles Topctrl1.tbAdd
        TxtDivision.AgSelectedValue = AgL.PubDivCode
        BlankText()
        TxtStatus.Text = ClsMain.EntryStatus.Active
        DispText(True)
        RaiseEvent BaseEvent_Topctrl_tbAdd()
    End Sub

    Public Overridable Sub Topctrl1_tbDel() Handles Topctrl1.tbDel
        Dim BlnTrans As Boolean = False
        Dim GCnCmd As New Object
        Dim MastPos As Long
        Dim mTrans As Boolean = False
        Dim InstancePassed As Boolean = True
        Dim I As Integer = 0

        Try
            MastPos = BMBMaster.Position




            RaiseEvent BaseEvent_Topctrl_tbDel(InstancePassed)
            If Not InstancePassed Then Exit Sub

            If DTMaster.Rows.Count > 0 Then
                If MsgBox("Are You Sure To Delete This Record?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, AgLibrary.ClsMain.PubMsgTitleInfo) = vbYes Then


                    AgL.ECmd = AgL.GCn.CreateCommand
                    AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
                    AgL.ECmd.Transaction = AgL.ETrans
                    mTrans = True



                    ClsMain.FCreateLogForDelete(Me, mLogText)
                    Call AgL.LogTableEntry(mSearchCode, Me.Text, "D", AgL.PubMachineName, AgL.PubUserName, AgL.GetDateTime(AgL.GcnRead), AgL.GCn, AgL.ECmd,,,,,,,, mLogText)


                    TxtEntryType.Text = "Delete"
                    RaiseEvent BaseEvent_ApproveDeletion_PreTrans(mSearchCode)
                    ProcApporve(AgL.GCn, AgL.ECmd)
                    RaiseEvent BaseEvent_ApproveDeletion_PostTrans(mSearchCode)


                    AgL.ETrans.Commit()
                    mTrans = False

                    FIniMaster(1)
                    Topctrl1_tbRef()
                    MoveRec()
                End If
            End If
        Catch Ex As Exception
            If mTrans = True Then AgL.ETrans.Rollback()
            MsgBox(Ex.Message, MsgBoxStyle.Information, AgLibrary.ClsMain.PubMsgTitleInfo)
        End Try
    End Sub

    Private Sub Topctrl1_tbDiscard() Handles Topctrl1.tbDiscard
        FIniMaster(0, 0)
        Try
            Topctrl1.Focus()
        Catch ex As Exception
        End Try
    End Sub


    Private Sub Topctrl1_tbEdit() Handles Topctrl1.tbEdit
        Dim mInstancePassed As Boolean = True

        RaiseEvent BaseEvent_Topctrl_tbEdit(mInstancePassed)
        If Not mInstancePassed Then
            Topctrl1.FButtonClick(14, True)
            Exit Sub
        End If

        'If AgL.StrCmp(TxtDivision.AgSelectedValue, AgL.PubDivCode) Or TxtDivision.Text = "" Then
        DispText(True)
        'Else
        '    Topctrl1.FButtonClick(14, True)
        '    MsgBox("Different Division Record. Can't Modify!", MsgBoxStyle.OkOnly, "Validation") : Exit Sub
        'End If

        'FCreateObjectOfForm()
        mFrmObjBeforeModification = New TempMaster()
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

    Public Sub Topctrl1_tbRef() Handles Topctrl1.tbRef
        CreateHelpDataSets()
        Ini_List()
        RaiseEvent BaseEvent_Topctrl_tbRef()
    End Sub

    Private Sub Topctrl1_tbPrn() Handles Topctrl1.tbPrn
        RaiseEvent BaseEvent_Topctrl_tbPrn(mSearchCode)
    End Sub

    Public Function Data_Validation() As Boolean
        Try
            Dim ChildDataPassed As Boolean = True

            If AgCL.AgCheckMandatory(Me) = False Then Data_Validation = False : Exit Function
            If Topctrl1.Mode = "Add" Then
                If MainTableName <> "" Then
                    mSearchCode = AgL.GetMaxId(MainTableName, mPrimaryField, AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 4, True, True, , AgL.Gcn_ConnectionString)
                    mInternalCode = mSearchCode
                End If
            End If
            RaiseEvent BaseEvent_Data_Validation(ChildDataPassed)
            If ChildDataPassed Then
                Data_Validation = True
            Else
                Data_Validation = False
            End If
        Catch ex As Exception
            Data_Validation = False
            MsgBox(ex.Message, MsgBoxStyle.OkOnly, "Data Validation")
        End Try
    End Function



    Private Sub Topctrl1_tbSave() Handles Topctrl1.tbSave
        'FCreateLog()
        ProcSave()
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

                If MainTableName <> "" Then
                    mQry = "Select EntryBy, EntryDate, ApproveBy, MoveToLog, MoveToLogDate, Status, Div_Code " &
                        " From " & MainTableName & "    Where " & mPrimaryField & "='" & mSearchCode & "'"
                    DsTemp = AgL.FillData(mQry, AgL.GCn)
                    With DsTemp.Tables(0)
                        '---------------------------------------------------
                        'Common code for all entry and approval management
                        '---------------------------------------------------
                        TxtStatus.Text = AgL.XNull(.Rows(0)("Status"))
                        TxtEntryBy.Text = AgL.XNull(.Rows(0)("EntryBy"))

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

                        TxtApproveBy.Text = AgL.XNull(.Rows(0)("ApproveBy"))
                        TxtMoveToLog.Text = AgL.XNull(.Rows(0)("MoveToLog"))

                        TxtDivision.AgSelectedValue = AgL.XNull(.Rows(0)("Div_Code"))
                        CmdApprove.Enabled = CBool(TxtApproveBy.Text.ToString = "" And GBoxApprove.Enabled)

                        'If AgL.StrCmp(AgL.PubUserName, "SA") Then
                        '    CmdMoveToLog.Enabled = True
                        'Else
                        '    CmdMoveToLog.Enabled = CBool(TxtMoveToLog.Text.ToString = "" And GBoxMoveToLog.Enabled)
                        'End If

                        If AgL.StrCmp(TxtStatus.Text, "Active") Then
                            CmdStatus.Image = My.Resources.Lock
                        Else
                            CmdStatus.Image = My.Resources.UnLock
                        End If
                        '---------------------------------------------------
                    End With
                End If
                RaiseEvent BaseFunction_MoveRec(mSearchCode)
            Else
                BlankText()
            End If

            'Disable Permission if it is different division record
            'If Not AgL.StrCmp(TxtDivision.AgSelectedValue, AgL.PubDivCode) Then Topctrl1.tEdit = False



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

            CmdApprove.Visible = False
            CmdDiscard.Visible = False
            'CmdStatus.Visible = False
            GBoxApprove.Text = "Approved By"
        Else
            CmdApprove.Visible = True
            CmdDiscard.Visible = True
            GBoxApprove.Text = "Approve/Discard"
            CmdStatus.Visible = False
        End If

        If Not mLogSystem Then
            GBoxApprove.Visible = False
            'GBoxMoveToLog.Visible = False
            GBoxEntryType.Left = 240
            GBoxDivision.Left = 470
            GroupBox2.Left = 700
        End If

        RaiseEvent BaseFunction_DispText()
    End Sub

    Function RetMain2LogTableColStr(ByVal MainTableName As String, ByVal LogTableName As String) As String
        'Dim mQry$
        'mQry = "DECLARE @ColStr VARCHAR(Max) " & _
        '"SET @ColStr='' " & _
        '"SELECT @ColStr=@ColStr + '" & MainTableName & ".' + C.COLUMN_NAME + ' = " & LogTableName & ".' + C.COLUMN_NAME  + ',' " & _
        '"FROM INFORMATION_SCHEMA.COLUMNS C  " & _
        '"WHERE C.TABLE_NAME ='" & LogTableName & "' " & _
        '"AND C.COLUMN_NAME NOT IN ('UID', 'EntryBy', 'EntryDate', 'ApproveBy', 'ApproveDate', 'EntryType', 'EntryStatus', 'MoveToLog', 'MoveToLogDate','RowID') " & _
        '"IF LEN(@ColStr)>0 SET @ColStr=substring (@ColStr,1,len(@ColStr)-1) " & _
        '" SELECT @ColStr "
        'RetMain2LogTableColStr = AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar


        Dim mQry$
        Dim DtTemp As DataTable
        Dim I As Integer
        Dim mStr As String = ""
        mQry = "PRAGMA table_info(" + LogTableName + ");"
        DtTemp = AgL.FillData(mQry, AgL.GcnRead).Tables(0)

        For I = 0 To DtTemp.Rows.Count - 1
            Select Case UCase(DtTemp.Rows(I)("name").ToString())
                Case "UID".ToUpper, "EntryBy".ToUpper, "EntryDate".ToUpper, "ApproveBy".ToUpper, "ApproveDate".ToUpper, "EntryType".ToUpper, "EntryStatus".ToUpper, "MoveToLog".ToUpper, "MoveToLogDate".ToUpper, "RowID".ToUpper
                Case Else
                    MsgBox(mStr)
                    mStr += IIf(mStr = "", "", ",") + MainTableName + "." + DtTemp.Rows(I)("name").ToString() + " = " + LogTableName + "." + DtTemp.Rows(I)("name").ToString()
            End Select

        Next

        RetMain2LogTableColStr = mStr

    End Function


    Function RetLog2MainTableColStr(ByVal MainTableName As String, ByVal LogTableName As String) As String

        'mQry = "DECLARE @ColStr VARCHAR(Max) " & _
        '"SET @ColStr='' " & _
        '"SELECT @ColStr=@ColStr + '" & LogTableName & ".' + C.COLUMN_NAME + ' = " & MainTableName & ".' + C.COLUMN_NAME  + ',' " & _
        '"FROM INFORMATION_SCHEMA.COLUMNS C  " & _
        '"WHERE C.TABLE_NAME ='" & MainTableName & "' " & _
        '"AND C.COLUMN_NAME NOT IN ('UID', 'EntryBy', 'EntryDate', 'ApproveBy', 'ApproveDate', 'EntryType', 'EntryStatus', 'MoveToLog', 'MoveToLogDate', 'IsDeleted','RowID') " & _
        '"IF LEN(@ColStr)>0 SET @ColStr=substring (@ColStr,1,len(@ColStr)-1) " & _
        '" SELECT @ColStr "
        'RetLog2MainTableColStr = AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar

        Dim mQry$
        Dim DtTemp As DataTable
        Dim I As Integer
        Dim mStr As String = ""
        mQry = "PRAGMA table_info(" + MainTableName + ");"
        DtTemp = AgL.FillData(mQry, AgL.GcnRead).Tables(0)

        For I = 0 To DtTemp.Rows.Count - 1
            Select Case UCase(DtTemp.Rows(I)("name").ToString())
                Case "UID".ToUpper, "EntryBy".ToUpper, "EntryDate".ToUpper, "ApproveBy".ToUpper, "ApproveDate".ToUpper, "EntryType".ToUpper, "EntryStatus".ToUpper, "MoveToLog".ToUpper, "MoveToLogDate".ToUpper, "IsDeleted".ToUpper, "RowID".ToUpper
                Case Else
                    MsgBox(mStr)
                    mStr += IIf(mStr = "", "", ",") + LogTableName + "." + DtTemp.Rows(I)("name").ToString() + " = " + MainTableName + "." + DtTemp.Rows(I)("name").ToString()

            End Select

        Next
        RetLog2MainTableColStr = mStr

    End Function

    Function RetColStr(ByVal TableName As String) As String
        Dim mQry$
        mQry = "DECLARE @ColStr VARCHAR(Max) " &
        "SET @ColStr='' " &
        "SELECT @ColStr=@ColStr +  C.COLUMN_NAME  + ',' " &
        "FROM INFORMATION_SCHEMA.COLUMNS C  " &
        "WHERE C.TABLE_NAME ='" & TableName & "' " &
        "AND C.COLUMN_NAME NOT IN ('UID','IsDeleted','RowID') " &
        "IF LEN(@ColStr)>0 SET @ColStr=substring (@ColStr,1,len(@ColStr)-1) " &
        " SELECT @ColStr "
        RetColStr = AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar
    End Function

    Public Sub ProcApporve(ByVal mConn As Object, ByVal mCmd As Object)
        Dim DtTemp As DataTable = Nothing
        Dim I As Integer

        If Not AgL.StrCmp(TxtEntryType.Text, "Delete") Then
            RaiseEvent BaseEvent_Approve_InTrans(mSearchCode, AgL.GCn, AgL.ECmd)
        Else




            RaiseEvent BaseEvent_ApproveDeletion_InTrans(mSearchCode, AgL.GCn, AgL.ECmd)

            '--------------------------------------------------------------
            'Line Records will be always deleted
            'exceptionally it is referentially integrated with any other table
            '--------------------------------------------------------------

            If ArrMainLineTable IsNot Nothing Then
                For I = 0 To UBound(ArrMainLineTable)
                    If ArrMainLineTable(I) <> "" Then
                        If UBound(ArrLineTableSearchKey) = 0 Then
                            mQry = "Delete from " & ArrMainLineTable(I) & " Where " & mPrimaryField & " ='" & mInternalCode & "'"
                        Else
                            mQry = "Delete from " & ArrMainLineTable(I) & " Where " & ArrLineTableSearchKey(I) & " ='" & mInternalCode & "'"
                        End If
                        AgL.Dman_ExecuteNonQry(mQry, mConn, mCmd)
                    End If
                Next
            End If
            '--------------------------------------------------------------

            If MainTableName <> "" Then
                mQry = "Delete from " & MainTableName & " Where " & mPrimaryField & " ='" & mInternalCode & "'"
                AgL.Dman_ExecuteNonQry(mQry, mConn, mCmd)
            End If
        End If
    End Sub


    Private Sub CmdMoveToLog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim mTrans As Boolean
        Dim mGuid$
        Dim I As Integer
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


            '----------------------------------------------
            'Update that entry is transferred to main table
            '----------------------------------------------
            mQry = "Update " & MainTableName & " Set MoveToLog = " & AgL.Chk_Text(AgL.PubUserName) & ", MoveToLogDate=" & AgL.Chk_Text(AgL.GetDateTime(AgL.GcnRead)) & " Where " & mPrimaryField & " = '" & mSearchCode & "' "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            '----------------------------------------------

            TxtMoveToLog.Text = AgL.PubUserName



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






    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdStatus.Click
        If FrmType = EntryPointType.Main Then
            If mSearchCode <> "" Then
                If MsgBox("Sure to change status of selected record?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    TxtEntryBy.Text = AgL.PubUserName
                    TxtEntryType.Text = "STATUS"
                    mQry = "Update " & MainTableName & " " &
                            " Set " &
                            " Status = " & AgL.Chk_Text(IIf(TxtStatus.Text = "", ClsMain.EntryStatus.Active, TxtStatus.Text)) & ", " &
                            " MoveToLog = " & AgL.Chk_Text(TxtEntryBy.Text) & ", " &
                            " MoveToLogDate = " & AgL.Chk_Text(AgL.GetDateTime(AgL.GcnRead)) & " " &
                            " Where " & mPrimaryField & " = '" & mSearchCode & "' "

                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

                End If
            End If
        Else
            MsgBox("Status Can be changed on Log Entry Only.")
        End If
    End Sub

    Public Overridable Sub ProcSave()
        Dim MastPos As Long
        Dim mTrans As String = ""

        Try
            If mEntryPointIniMode <> ClsMain.EntryPointIniMode.Insertion Then
                MastPos = BMBMaster.Position
            End If

            '---------------------------------------------------
            'Any type of validation like Required field, Duplicate Check etc.
            'are to be write in Data_Validation function.
            '----------------------------------------------------
            If Data_Validation() = False Then Exit Sub
            '----------------------------------------------------

            RaiseEvent BaseEvent_Save_PreTrans(mSearchCode)


            RaiseEvent BaseEvent_Approve_PreTrans(mSearchCode)

            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = "Begin"



            If Topctrl1.Mode = "Add" Then
                mQry = "INSERT INTO " & MainTableName & " (" & mPrimaryField & ", EntryBy, EntryDate,   Div_Code, Status) " &
                        "VALUES (" & AgL.Chk_Text(mInternalCode) & ", " & AgL.Chk_Text(AgL.PubUserName) & ", " & AgL.Chk_Date(DateTime.Now.ToString("s")) & ", " & AgL.Chk_Text(TxtDivision.AgSelectedValue) & ", " & AgL.Chk_Text(TxtStatus.Text) & ")"

                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            Else

                mQry = "Update " & MainTableName & " Set MoveToLog = " & AgL.Chk_Text(AgL.PubUserName) & ", MoveToLogDate = " & AgL.Chk_Date(DateTime.Now.ToString("u")) & ",  Div_Code = " & AgL.Chk_Text(TxtDivision.AgSelectedValue) & " " &
                           " Where " & mPrimaryField & " = " & AgL.Chk_Text(mInternalCode) & "  "

                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If

            RaiseEvent BaseEvent_Save_InTrans(mSearchCode, AgL.GCn, AgL.ECmd)
            '--------------------------------------------------------------
            'Create a log entry of each activity like add, edit delete print
            '--------------------------------------------------------------
            If Topctrl1.Mode <> "Add" Then
                mLogText = ""
                ClsMain.FCreateLogForEdit(mFrmObjBeforeModification, Me, mLogText)
            End If
            Call AgL.LogTableEntry(mSearchCode, Me.Text, AgL.MidStr(Topctrl1.Mode, 0, 1), AgL.PubMachineName, AgL.PubUserName, AgL.GetDateTime(AgL.GcnRead), AgL.GCn, AgL.ECmd,,,,,,,, mLogText)
            '--------------------------------------------------------------






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

            'If Not LogSystem Then
            '    RaiseEvent BaseEvent_Approve_PostTrans(mSearchCode)
            'End If

            If EntryPointIniMode = ClsMain.EntryPointIniMode.Insertion Then
                Me.Close()
                Exit Sub
            End If


            FIniMaster(0, 1)
            Topctrl1_tbRef()

            If Topctrl1.Mode = "Add" Then
                '--------------------------------------------------------
                'Set newly feeded record as current record
                'go to add mode once again
                '--------------------------------------------------------
                Topctrl1.LblDocId.Text = mSearchCode
                Topctrl1.FButtonClick(0)
                '--------------------------------------------------------
                Exit Sub
            Else
                Topctrl1.SetDisp(True)
                'MoveRec()
                FindMove(mSearchCode)
            End If
        Catch ex As Exception
            If mTrans = "Begin" Then
                AgL.ETrans.Rollback()
            ElseIf mTrans = "Commit" Then
                Topctrl1.FButtonClick(14, True)
            End If
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub CreateHelpDataSets()
        RaiseEvent BaseFunction_CreateHelpDataSet()
    End Sub

    Public Sub FindMove(ByVal SearchCode As String)
        Try
            If SearchCode <> "" Then
                AgL.PubSearchRow = SearchCode
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
End Class
