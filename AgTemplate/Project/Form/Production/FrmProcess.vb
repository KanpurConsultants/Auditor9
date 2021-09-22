Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.SQLite
Public Class FrmProcess
    Private DTMaster As New DataTable()
    Public BMBMaster As BindingManagerBase
    Private KEAMainKeyCode As System.Windows.Forms.KeyEventArgs
    Private DTStruct As New DataTable
    Dim mQry As String = "", mSearchCode As String = ""

    Public Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Topctrl1.FSetParent(Me, StrUPVar, DTUP)
        Topctrl1.SetDisp(True)
    End Sub

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub

    Private Sub Form_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        DTMaster = Nothing
    End Sub

    Private Sub IniGrid()
    End Sub

    Private Sub KeyDown_Form(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F2 Or e.KeyCode = Keys.F3 Or e.KeyCode = Keys.F4 Or e.KeyCode = (Keys.F And e.Control) Or e.KeyCode = (Keys.P And e.Control) _
        Or e.KeyCode = (Keys.S And e.Control) Or e.KeyCode = Keys.Escape Or e.KeyCode = Keys.F5 Or e.KeyCode = Keys.F10 _
        Or e.KeyCode = Keys.Home Or e.KeyCode = Keys.PageUp Or e.KeyCode = Keys.PageDown Or e.KeyCode = Keys.End Then
            Topctrl1.TopKey_Down(e)
        End If
        If Me.ActiveControl IsNot Nothing Then
            If Me.ActiveControl.Name <> Topctrl1.Name And _
                Not (TypeOf (Me.ActiveControl) Is AgControls.AgDataGrid) Then
                If e.KeyCode = Keys.Return Then SendKeys.Send("{Tab}")
            End If
        End If
    End Sub

    Sub KeyPress_Form(ByVal Sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        If e.KeyChar = Chr(Keys.Escape) Then Exit Sub
        If Me.ActiveControl Is Nothing Then Exit Sub
        AgL.CheckQuote(e)
    End Sub

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            AgL.WinSetting(Me, 465, 880, 0, 0)
            IniGrid()
            FIniMaster()
            Ini_List()
            DispText()
            MoveRec()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FIniMaster(Optional ByVal BytDel As Byte = 0, Optional ByVal BytRefresh As Byte = 1)
        If AgL.PubMoveRecApplicable Then
            mQry = "Select NCat As SearchCode " & _
                    " From Process "
            Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
        End If
    End Sub

    Sub Ini_List()
        Try
            'mQry = "Select NCat As Code, NCat As ManualCode " & _
            '        " From VoucherCat " & _
            '        " Order By NCat"
            'TxtManualCode.AgHelpDataSet() = AgL.FillData(mQry, AgL.GCn)

            'mQry = "Select Vc.NCat As Code, Vc.NCatDescription As Description " & _
            '        " From VoucherCat Vc " & _
            '        " Order By Vc.NCat"
            'TxtDescription.AgHelpDataSet() = AgL.FillData(mQry, AgL.GCn)

            mQry = "Select P.Code, P.Description As ProcessGroup  " & _
                    " From ProcessGroup P "
            TxtProcessGroup.AgHelpDataSet() = AgL.FillData(mQry, AgL.GCn)

            mQry = "SELECT S.SubCode AS Code, S.Name FROM SubGroup S "
            TxtSubCode.AgHelpDataSet() = AgL.FillData(mQry, AgL.GCn)

            mQry = " SELECT H.Code, H.Description AS QcGroup FROM QcGroup H "
            TxtQCGroup.AgHelpDataSet() = AgL.FillData(mQry, AgL.GCn)

            mQry = " SELECT H.Code, H.Description AS Structure FROM Structure H "
            TxtStructure.AgHelpDataSet() = AgL.FillData(mQry, AgL.GCn)

            mQry = " SELECT H.NCat AS Code, L.NCatDescription AS Process " & _
                    " FROM Process H " & _
                    " LEFT JOIN VoucherCat L ON H.NCat = L.NCat "
            TxtPrevProcess.AgHelpDataSet() = AgL.FillData(mQry, AgL.GCn)

            mQry = " Select '" & ClsMain.JobType.Inside & "' As Code, '" & ClsMain.JobType.Inside & "' As JobType   " & _
                    " UNION ALL " & _
                    " Select '" & ClsMain.JobType.Outside & "' As Code, '" & ClsMain.JobType.Outside & "' As JobType   "
            TxtInsideOutside.AgHelpDataSet() = AgL.FillData(mQry, AgL.GCn)

            mQry = "SELECT '" & ClsMain.JobOrderFor.ProductionOrder & "' As Code, '" & ClsMain.JobOrderFor.ProductionOrder & "' As JobOrderFor " & _
                " UNION ALL " & _
                " SELECT '" & ClsMain.JobOrderFor.Stock & "'  As Code, '" & ClsMain.JobOrderFor.Stock & "'  As JobOrderFor  "
            TxtDefaultJobOrderFor.AgHelpDataSet() = AgL.FillData(mQry, AgL.GCn)

            TxtDefaultBillingType.AgHelpDataSet() = AgL.FillData(ClsMain.HelpQueries.BillingType, AgL.GCn)
            TxtJobOn.AgHelpDataSet() = AgL.FillData(ClsMain.HelpQueries.BillingType, AgL.GCn)

            mQry = " SELECT H.NCat AS Code, H.NCat AS Description, H.NCatDescription " & _
                    " FROM VoucherCat H "
            TxtManualCode.AgHelpDataSet() = AgL.FillData(mQry, AgL.GCn)

            mQry = " SELECT H.NCatDescription AS Code, H.NCatDescription AS Description " & _
                    " FROM VoucherCat H "
            TxtDescription.AgHelpDataSet() = AgL.FillData(mQry, AgL.GCn)

            mQry = " SELECT H.NCat AS Code, H.NCat AS Description, H.NCatDescription " & _
                    " FROM VoucherCat H " & _
                    " WHERE H.NCat NOT IN (SELECT P.NCat FROM Process P) "
            TxtProcessIssueNCat.AgHelpDataSet() = AgL.FillData(mQry, AgL.GCn)
            TxtProcessReceiveNCat.AgHelpDataSet() = TxtProcessIssueNCat.AgHelpDataSet
            TxtProcessReturnNCat.AgHelpDataSet() = TxtProcessIssueNCat.AgHelpDataSet
            TxtProcessCancelNcat.AgHelpDataSet() = TxtProcessIssueNCat.AgHelpDataSet
            TxtProcessInvoiceNCat.AgHelpDataSet() = TxtProcessIssueNCat.AgHelpDataSet

            mQry = " SELECT H.NCatDescription AS Code, H.NCatDescription AS Description " & _
                    " FROM VoucherCat H " & _
                    " WHERE H.NCat NOT IN (SELECT P.NCat FROM Process P)"
            TxtProcessIssueNCatDescription.AgHelpDataSet() = AgL.FillData(mQry, AgL.GCn)
            TxtProcessReceiveNCatDescription.AgHelpDataSet() = TxtProcessIssueNCatDescription.AgHelpDataSet
            TxtProcessReturnNCatDescription.AgHelpDataSet() = TxtProcessIssueNCatDescription.AgHelpDataSet
            TxtProcessCancelDesc.AgHelpDataSet() = TxtProcessIssueNCatDescription.AgHelpDataSet
            TxtProcessInvoiceNCatDescription.AgHelpDataSet() = TxtProcessIssueNCatDescription.AgHelpDataSet
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Topctrl1_tbAdd() Handles Topctrl1.tbAdd
        BlankText()
        DispText()
        TxtManualCode.Focus()
    End Sub

    Private Sub Topctrl1_tbDel() Handles Topctrl1.tbDel
        Dim BlnTrans As Boolean = False
        Dim GCnCmd As New SqliteCommand
        Dim MastPos As Long
        Dim mTrans As Boolean = False

        Try
            If AgL.PubMoveRecApplicable Then MastPos = BMBMaster.Position

            If mSearchCode <> "" Then
                If MsgBox("Are You Sure To Delete This Record?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, AgLibrary.ClsMain.PubMsgTitleInfo) = vbYes Then
                    AgL.ECmd = AgL.GCn.CreateCommand
                    AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
                    AgL.ECmd.Transaction = AgL.ETrans
                    mTrans = True

                    AgL.Dman_ExecuteNonQry("Delete From Voucher_Prefix Where V_Type ='" & mSearchCode & "'", AgL.GCn, AgL.ECmd)
                    AgL.Dman_ExecuteNonQry("Delete From Voucher_Type Where NCat ='" & mSearchCode & "'", AgL.GCn, AgL.ECmd)
                    AgL.Dman_ExecuteNonQry("Delete From Process Where NCat ='" & mSearchCode & "'", AgL.GCn, AgL.ECmd)
                    AgL.Dman_ExecuteNonQry("Delete From VoucherCat Where NCat ='" & mSearchCode & "'", AgL.GCn, AgL.ECmd)

                    Call AgL.LogTableEntry(mSearchCode, Me.Text, "D", AgL.PubMachineName, AgL.PubUserName, AgL.PubLoginDate, AgL.GCn, AgL.ECmd)

                    AgL.SynchroniseSiteOnLineData(AgL, AgL.GCn, AgL.Gcn_ConnectionString, AgL.GcnSite_ConnectionString, AgL.ECmd)
                    AgL.ETrans.Commit()
                    mTrans = False

                    If AgL.PubMoveRecApplicable Then
                        FIniMaster(1)
                        Topctrl1_tbRef()
                    Else
                        AgL.PubSearchRow = ""
                    End If
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
        Topctrl1.Focus()
    End Sub

    Private Sub Topctrl1_tbEdit() Handles Topctrl1.tbEdit
        DispText()
        TxtManualCode.Focus()
    End Sub

    Private Sub Topctrl1_tbFind() Handles Topctrl1.tbFind
        'If DTMaster.Rows.Count <= 0 Then MsgBox("No Records To Search.", vbInformation, AgLibrary.ClsMain.PubMsgTitleInfo) : Exit Sub
        Try
            AgL.PubFindQry = "Select P.NCat As SearchCode, P.NCat As ManualCode, " & _
                            " Vc.NCatDescription As [Description],P.ProcessGroup " & _
                            " From Process P " & _
                            " Left Join VoucherCat Vc On P.NCat = Vc.NCat "
            AgL.PubFindQryOrdBy = "[SearchCode]"

            '*************** common code start *****************
            AgL.PubObjFrmFind = New AgLibrary.frmFind(AgL)
            AgL.PubObjFrmFind.ShowDialog()
            AgL.PubObjFrmFind = Nothing
            If AgL.PubSearchRow <> "" Then
                If AgL.PubMoveRecApplicable Then
                    AgL.PubDRFound = DTMaster.Rows.Find(AgL.PubSearchRow)
                    BMBMaster.Position = DTMaster.Rows.IndexOf(AgL.PubDRFound)
                End If
                MoveRec()
            End If
            '*************** common code end  *****************
        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try
    End Sub

    Private Sub Topctrl1_tbRef() Handles Topctrl1.tbRef
        Ini_List()
    End Sub

    Private Sub Topctrl1_tbSave() Handles Topctrl1.tbSave
        Dim MastPos As Long
        Dim mTrans As Boolean = False
        Try
            If AgL.PubMoveRecApplicable Then MastPos = BMBMaster.Position
            If AgL.RequiredField(TxtManualCode, LblManualCode.Text) Then Exit Sub
            If AgL.RequiredField(TxtDescription, LblDescription.Text) Then Exit Sub
            If AgL.RequiredField(TxtProcessGroup, LblProcessGroup.Text) Then Exit Sub
            If AgL.RequiredField(TxtInsideOutside, LblInsideOutside.Text) Then Exit Sub
            If AgL.RequiredField(TxtDefaultJobOrderFor, LblDefaultJobOrderFor.Text) Then Exit Sub
            If AgL.RequiredField(TxtDefaultBillingType, LblDefaultBillingType.Text) Then Exit Sub
            If AgL.RequiredField(TxtJobOn, LblJobOn.Text) Then Exit Sub

            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = True


            If Topctrl1.Mode = "Add" Then
                If TxtManualCode.Text <> "" And TxtManualCode.AgSelectedValue = "" Then Call ProcCreateNewNCat(TxtManualCode.Text, TxtDescription.Text)
                If TxtProcessIssueNCat.Text <> "" And TxtProcessIssueNCat.AgSelectedValue = "" Then Call ProcCreateNewNCat(TxtProcessIssueNCat.Text, TxtProcessIssueNCatDescription.Text)
                If TxtProcessReceiveNCat.Text <> "" And TxtProcessReceiveNCat.AgSelectedValue = "" Then Call ProcCreateNewNCat(TxtProcessReceiveNCat.Text, TxtProcessReceiveNCatDescription.Text)
                If TxtProcessReceiveNCat.Text <> "" And TxtProcessReturnNCat.AgSelectedValue = "" Then Call ProcCreateNewNCat(TxtProcessReturnNCat.Text, TxtProcessReturnNCatDescription.Text)
                If TxtProcessCancelNcat.Text <> "" And TxtProcessCancelNcat.AgSelectedValue = "" Then Call ProcCreateNewNCat(TxtProcessCancelNcat.Text, TxtProcessCancelDesc.Text)
                If TxtProcessInvoiceNCat.Text <> "" And TxtProcessInvoiceNCat.AgSelectedValue = "" Then Call ProcCreateNewNCat(TxtProcessInvoiceNCat.Text, TxtProcessInvoiceNCatDescription.Text)

                mQry = "Insert Into Process(NCat,ProcessGroup,SubCode,Div_Code, QcGroup, " & _
                        " InsideOutside, DefaultJobOrderFor, DefaultBillingType, PrevProcess, " & _
                        " ProcessIssueNCat, ProcessReceiveNCat, ProcessReturnNCat, " & _
                        " ProcessCancelNCat, ProcessInvoiceNCat, JobOn) " & _
                        " Values(" & AgL.Chk_Text(TxtManualCode.Text) & ", " & AgL.Chk_Text(TxtProcessGroup.AgSelectedValue) & ", " & _
                        " " & AgL.Chk_Text(TxtSubCode.AgSelectedValue) & ", " & AgL.Chk_Text(AgL.PubDivCode) & ", " & _
                        " " & AgL.Chk_Text(TxtQCGroup.AgSelectedValue) & ", " & _
                        " " & AgL.Chk_Text(TxtInsideOutside.Text) & ", " & _
                        " " & AgL.Chk_Text(TxtDefaultJobOrderFor.Text) & ", " & _
                        " " & AgL.Chk_Text(TxtDefaultBillingType.Text) & ", " & _
                        " " & AgL.Chk_Text(TxtPrevProcess.AgSelectedValue) & ", " & _
                        " " & AgL.Chk_Text(TxtProcessIssueNCat.Text) & ", " & _
                        " " & AgL.Chk_Text(TxtProcessReceiveNCat.Text) & ", " & _
                        " " & AgL.Chk_Text(TxtProcessReturnNCat.Text) & ", " & _
                        " " & AgL.Chk_Text(TxtProcessCancelNcat.Text) & ", " & _
                        " " & AgL.Chk_Text(TxtProcessInvoiceNCat.Text) & ", " & _
                        " " & AgL.Chk_Text(TxtJobOn.Text) & " " & _
                        " ) "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            Else
                mQry = "UPDATE Process SET " & _
                        " NCat = " & AgL.Chk_Text(TxtManualCode.Text) & ",	" & _
                        " ProcessGroup = " & AgL.Chk_Text(TxtProcessGroup.AgSelectedValue) & ", " & _
                        " QcGroup = " & AgL.Chk_Text(TxtQCGroup.AgSelectedValue) & ", " & _
                        " SubCode = " & AgL.Chk_Text(TxtSubCode.AgSelectedValue) & ", " & _
                        " InsideOutside = " & AgL.Chk_Text(TxtInsideOutside.Text) & ", " & _
                        " PrevProcess = " & AgL.Chk_Text(TxtPrevProcess.AgSelectedValue) & ", " & _
                        " ProcessIssueNCat = " & AgL.Chk_Text(TxtProcessIssueNCat.Text) & ", " & _
                        " ProcessReceiveNCat = " & AgL.Chk_Text(TxtProcessReceiveNCat.Text) & ", " & _
                        " ProcessCancelNCat = " & AgL.Chk_Text(TxtProcessCancelNcat.Text) & ", " & _
                        " ProcessReturnNCat = " & AgL.Chk_Text(TxtProcessReturnNCat.Text) & ", " & _
                        " ProcessInvoiceNCat = " & AgL.Chk_Text(TxtProcessInvoiceNCat.Text) & ", " & _
                        " DefaultJobOrderFor = " & AgL.Chk_Text(TxtDefaultJobOrderFor.Text) & ", " & _
                        " DefaultBillingType = " & AgL.Chk_Text(TxtDefaultBillingType.Text) & ", " & _
                        " JobOn = " & AgL.Chk_Text(TxtJobOn.Text) & " " & _
                        " Where NCat ='" & mSearchCode & "' "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                mQry = "UPDATE VoucherCat " & _
                        " SET NCat = " & AgL.Chk_Text(TxtManualCode.Text) & ",  " & _
                        " Category = " & AgL.Chk_Text(TxtManualCode.Text) & ",  " & _
                        " NCatDescription = " & AgL.Chk_Text(TxtDescription.Text) & "  " & _
                        " WHERE NCat = '" & mSearchCode & "' "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                mQry = "UPDATE Voucher_Type " & _
                        " SET " & _
                        " NCat = " & AgL.Chk_Text(TxtManualCode.Text) & ", " & _
                        " Category = " & AgL.Chk_Text(TxtManualCode.Text) & ", " & _
                        " V_Type = " & AgL.Chk_Text(TxtManualCode.Text) & ", " & _
                        " Description = " & AgL.Chk_Text(TxtDescription.Text) & ", " & _
                        " Short_Name = " & AgL.Chk_Text(TxtManualCode.Text) & " " & _
                        " WHERE NCat = '" & mSearchCode & "' "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If

            mQry = "UPDATE VoucherCat SET " & _
                    " Structure = " & AgL.Chk_Text(TxtStructure.AgSelectedValue) & " " & _
                    " Where NCat In ('" & mSearchCode & "','" & TxtProcessReceiveNCat.Text & "') "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

            Call AgL.LogTableEntry(mSearchCode, Me.Text, AgL.MidStr(Topctrl1.Mode, 0, 1), AgL.PubMachineName, AgL.PubUserName, AgL.PubLoginDate, AgL.GCn, AgL.ECmd)

            AgL.SynchroniseSiteOnLineData(AgL, AgL.GCn, AgL.Gcn_ConnectionString, AgL.GcnSite_ConnectionString, AgL.ECmd)
            AgL.ETrans.Commit()
            mTrans = False
            If AgL.PubMoveRecApplicable Then
                FIniMaster(0, 1)
                Topctrl1_tbRef()
            End If
            If Topctrl1.Mode = "Add" Then
                Topctrl1.LblDocId.Text = mSearchCode
                Topctrl1.FButtonClick(0)
                Exit Sub
            Else
                Topctrl1.SetDisp(True)
                MoveRec()
            End If
        Catch ex As Exception
            If mTrans = True Then AgL.ETrans.Rollback()
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub MoveRec()
        Dim DsTemp As DataSet = Nothing
        Dim MastPos As Long
        Try
            FClear()
            BlankText()

            If AgL.PubMoveRecApplicable Then
                MastPos = BMBMaster.Position
                mSearchCode = DTMaster.Rows(MastPos)("SearchCode")
            Else
                mSearchCode = AgL.PubSearchRow
            End If

            If mSearchCode <> "" Then
                mQry = "Select H.*, L.NCatDescription As ProcessDescription, L.Structure, " & _
                        " H.InsideOutside, H.DefaultJobOrderFor, H.DefaultBillingType, " & _
                        " H.PrevProcess, H.ProcessIssueNCat, H.ProcessReceiveNCat, " & _
                        " H.ProcessReturnNCat, H.ProcessCancelNCat, H.ProcessInvoiceNCat, " & _
                        " Vc1.NCatDescription As ProcessIssueNCatDesc, " & _
                        " Vc2.NCatDescription As ProcessReceiveNCatDesc, " & _
                        " Vc3.NCatDescription As ProcessReturnNCatDesc, " & _
                        " Vc4.NCatDescription As ProcessCancelNCatDesc, " & _
                        " Vc5.NCatDescription As ProcessInvoiceNCatDesc " & _
                        " From Process H " & _
                        " LEFT JOIN VoucherCat L On H.NCat = L.NCat " & _
                        " LEFT JOIN VoucherCat Vc1 On H.ProcessIssueNCat = Vc1.NCat " & _
                        " LEFT JOIN VoucherCat Vc2 On H.ProcessReceiveNCat = Vc2.NCat " & _
                        " LEFT JOIN VoucherCat Vc3 On H.ProcessReturnNCat = Vc3.NCat " & _
                        " LEFT JOIN VoucherCat Vc4 On H.ProcessCancelNCat = Vc4.NCat " & _
                        " LEFT JOIN VoucherCat Vc5 On H.ProcessInvoiceNCat = Vc5.NCat " & _
                        " Where H.NCat = '" & mSearchCode & "'"
                DsTemp = AgL.FillData(mQry, AgL.GCn)
                With DsTemp.Tables(0)
                    If .Rows.Count > 0 Then
                        TxtManualCode.Text = AgL.XNull(.Rows(0)("NCat"))
                        TxtDescription.Text = AgL.XNull(.Rows(0)("ProcessDescription"))
                        TxtProcessGroup.AgSelectedValue = AgL.XNull(.Rows(0)("ProcessGroup"))
                        TxtSubCode.AgSelectedValue = AgL.XNull(.Rows(0)("SubCode"))
                        TxtQCGroup.AgSelectedValue = AgL.XNull(.Rows(0)("QCGroup"))
                        TxtStructure.AgSelectedValue = AgL.XNull(.Rows(0)("Structure"))
                        TxtInsideOutside.Text = AgL.XNull(.Rows(0)("InsideOutside"))
                        TxtDefaultJobOrderFor.Text = AgL.XNull(.Rows(0)("DefaultJobOrderFor"))
                        TxtDefaultBillingType.Text = AgL.XNull(.Rows(0)("DefaultBillingType"))
                        TxtJobOn.Text = AgL.XNull(.Rows(0)("JobOn"))
                        TxtPrevProcess.AgSelectedValue = AgL.XNull(.Rows(0)("PrevProcess"))
                        TxtProcessIssueNCat.Text = AgL.XNull(.Rows(0)("ProcessIssueNCat"))
                        TxtProcessIssueNCatDescription.Text = AgL.XNull(.Rows(0)("ProcessIssueNCatDesc"))
                        TxtProcessReceiveNCat.Text = AgL.XNull(.Rows(0)("ProcessReceiveNCat"))
                        TxtProcessReceiveNCatDescription.Text = AgL.XNull(.Rows(0)("ProcessReceiveNCatDesc"))
                        TxtProcessReturnNCat.Text = AgL.XNull(.Rows(0)("ProcessReturnNCat"))
                        TxtProcessReturnNCatDescription.Text = AgL.XNull(.Rows(0)("ProcessReturnNCatDesc"))
                        TxtProcessCancelNcat.Text = AgL.XNull(.Rows(0)("ProcessCancelNCat"))
                        TxtProcessCancelDesc.Text = AgL.XNull(.Rows(0)("ProcessCancelNCatDesc"))
                        TxtProcessInvoiceNCat.Text = AgL.XNull(.Rows(0)("ProcessInvoiceNCat"))
                        TxtProcessInvoiceNCatDescription.Text = AgL.XNull(.Rows(0)("ProcessInvoiceNCatDesc"))
                    End If
                End With
            Else
                BlankText()
            End If
            If AgL.PubMoveRecApplicable Then Topctrl1.FSetDispRec(BMBMaster)
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            DsTemp = Nothing
        End Try
    End Sub

    Private Sub BlankText()
        If Topctrl1.Mode <> "Add" Then Topctrl1.BlankTextBoxes(Me)
        mSearchCode = ""
    End Sub

    Private Sub DispText(Optional ByVal Enb As Boolean = False)
        'Coding To Enable/Disable Controls
        If AgL.StrCmp(Topctrl1.Mode, "Edit") Then
            TxtProcessIssueNCat.AgMasterHelp = False
            TxtProcessReceiveNCat.AgMasterHelp = False
            TxtProcessReturnNCat.AgMasterHelp = False
            TxtProcessCancelNcat.AgMasterHelp = False
            TxtProcessInvoiceNCat.AgMasterHelp = False
        Else
            TxtProcessIssueNCat.AgMasterHelp = True
            TxtProcessReceiveNCat.AgMasterHelp = True
            TxtProcessReturnNCat.AgMasterHelp = True
            TxtProcessCancelNcat.AgMasterHelp = True
            TxtProcessInvoiceNCat.AgMasterHelp = True
        End If
    End Sub

    Private Sub FClear()
        DTStruct.Clear()
    End Sub

    Private Sub ProcCreateNewNCat(ByVal NCat As String, ByVal NCatDescription As String)
        AgL.CreateNCat(AgL.GcnRead, NCat, NCat, NCatDescription, AgL.PubSiteCode)
        AgL.CreateVType(AgL.GcnRead, NCat, NCat, NCat, NCatDescription, NCat, AgL.PubUserName, AgL.PubLoginDate, AgL.PubStartDate, AgL.PubEndDate, AgL.PubSiteCode, AgL.PubDivCode, False, AgL.PubSitewiseV_No)
    End Sub

    Private Sub TxtProcessIssueNCat_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtManualCode.Validating, TxtProcessIssueNCat.Validating, TxtProcessReceiveNCat.Validating, TxtProcessCancelNcat.Validating, TxtProcessInvoiceNCat.Validating
        Dim DrTemp As DataRow() = Nothing
        Try
            Select Case sender.Name
                Case TxtManualCode.Name
                    If sender.text <> "" Then
                        DrTemp = sender.AgHelpDataSet.Tables(0).Select("Code = '" & sender.AgSelectedValue & "' ")
                        If DrTemp.Length > 0 Then
                            TxtDescription.Text = AgL.XNull(DrTemp(0)("NCatDescription"))
                            TxtDescription.Enabled = False
                        Else
                            'TxtDescription.Text = ""
                            TxtDescription.Enabled = True
                        End If
                    End If

                Case TxtProcessIssueNCat.Name
                    If sender.text <> "" Then
                        DrTemp = sender.AgHelpDataSet.Tables(0).Select("Code = '" & sender.AgSelectedValue & "' ")
                        If DrTemp.Length > 0 Then
                            TxtProcessIssueNCatDescription.Text = AgL.XNull(DrTemp(0)("NCatDescription"))
                            TxtProcessIssueNCatDescription.Enabled = False
                        Else
                            TxtProcessIssueNCatDescription.Text = ""
                            TxtProcessIssueNCatDescription.Enabled = True
                        End If
                    End If

                Case TxtProcessReceiveNCat.Name
                    If sender.text <> "" Then
                        DrTemp = sender.AgHelpDataSet.Tables(0).Select("Code = '" & sender.AgSelectedValue & "' ")
                        If DrTemp.Length > 0 Then
                            TxtProcessReceiveNCatDescription.Text = AgL.XNull(DrTemp(0)("NCatDescription"))
                            TxtProcessReceiveNCatDescription.Enabled = False
                        Else
                            TxtProcessReceiveNCatDescription.Text = ""
                            TxtProcessReceiveNCatDescription.Enabled = True
                        End If
                    End If

                Case TxtProcessCancelNcat.Name
                    If sender.text <> "" Then
                        DrTemp = sender.AgHelpDataSet.Tables(0).Select("Code = '" & sender.AgSelectedValue & "' ")
                        If DrTemp.Length > 0 Then
                            TxtProcessCancelDesc.Text = AgL.XNull(DrTemp(0)("NCatDescription"))
                            TxtProcessCancelDesc.Enabled = False
                        Else
                            TxtProcessCancelDesc.Text = ""
                            TxtProcessCancelDesc.Enabled = True
                        End If
                    End If

                Case TxtProcessInvoiceNCat.Name
                    If sender.text <> "" Then
                        DrTemp = sender.AgHelpDataSet.Tables(0).Select("Code = '" & sender.AgSelectedValue & "' ")
                        If DrTemp.Length > 0 Then
                            TxtProcessInvoiceNCatDescription.Text = AgL.XNull(DrTemp(0)("NCatDescription"))
                            TxtProcessInvoiceNCatDescription.Enabled = False
                        Else
                            TxtProcessInvoiceNCatDescription.Text = ""
                            TxtProcessInvoiceNCatDescription.Enabled = True
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class
