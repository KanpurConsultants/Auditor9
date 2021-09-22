Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.SQLite
Public Class FrmCommonMaster1
    Private DTMaster As New DataTable()
    Public BMBMaster As BindingManagerBase
    Private KEAMainKeyCode As System.Windows.Forms.KeyEventArgs
    Private DTStruct As New DataTable
    Dim mQry As String = "", mSearchCode As String = ""
    Dim mMasterType As String = "NA"
    Dim mModuleType As String = "NA"
    Dim Agl As AgLibrary.ClsMain

    Private Const mTableCommonMaster1 As String = "CommonMaster1"
    Private Const mTableCommonMaster1Alias As String = "CommonMaster1Alias"

    Public Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable, ByVal AgLibVar As AgLibrary.ClsMain)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Agl = AgLibVar
        Topctrl1.FSetParent(Me, StrUPVar, DTUP)
        Topctrl1.SetDisp(True)
    End Sub
    Public Property MasterType() As String
        Get
            Return mMasterType
        End Get
        Set(ByVal value As String)
            mMasterType = value
        End Set
    End Property

    Public Property ModuleType() As String
        Get
            Return mModuleType
        End Get
        Set(ByVal value As String)
            mModuleType = value
        End Set
    End Property


    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AGL.FPaintForm(Me, e, Topctrl1.Height)
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
        AGL.CheckQuote(e)
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            AGL.WinSetting(Me, 300, 880, 0, 0)
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
        mQry = "Select CommonMaster1.Code As SearchCode " & _
        " From CommonMaster1 Where  ModuleType = '" & mModuleType & "' and MasterType='" & mMasterType & "' "
        Topctrl1.FIniForm(DTMaster, AGL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub


    Sub Ini_List()
        mQry = "Select Code  As Code, Description As Name From CommonMaster1 Where ModuleType = '" & mModuleType & "' and MasterType='" & mMasterType & "' " & _
            "  Order By Description"
        TxtDescription.AgHelpDataSet = Agl.FillData(mQry, Agl.GCn)

    End Sub


    Private Sub Topctrl1_tbAdd() Handles Topctrl1.tbAdd
        BlankText()
        DispText(True)
        TxtDescription.Focus()
    End Sub
    Private Sub Topctrl1_tbDel() Handles Topctrl1.tbDel
        Dim BlnTrans As Boolean = False
        Dim GCnCmd As New SQLiteCommand
        Dim MastPos As Long
        Dim mTrans As Boolean = False


        Try
            MastPos = BMBMaster.Position


            If DTMaster.Rows.Count > 0 Then
                If MsgBox("Are You Sure To Delete This Record?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, AgLibrary.ClsMain.PubMsgTitleInfo) = vbYes Then


                    AgL.ECmd = AGL.GCn.CreateCommand
                    AgL.ETrans = AGL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
                    AgL.ECmd.Transaction = AgL.ETrans
                    mTrans = True

                    Agl.Dman_ExecuteNonQry("Delete From CommonMaster1 Where Code='" & mSearchCode & "'", Agl.GCn, Agl.ECmd)
                    Agl.Dman_ExecuteNonQry("Delete From CommonMaster1Alias Where Code='" & mSearchCode & "'", Agl.GCn, Agl.ECmd)

                    Call AgL.LogTableEntry(mSearchCode, Me.Text, "D", AgL.PubMachineName, AgL.PubUserName, AgL.PubLoginDate, AgL.GCn, AgL.ECmd)

                    AgL.SynchroniseSiteOnLineData(AgL, AgL.GCn, AgL.Gcn_ConnectionString, AgL.GcnSite_ConnectionString, AgL.ECmd)
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
        Topctrl1.Focus()
    End Sub


    Private Sub Topctrl1_tbEdit() Handles Topctrl1.tbEdit
        DispText(True)
        TxtDescription.Focus()
    End Sub


    Private Sub Topctrl1_tbFind() Handles Topctrl1.tbFind
        If DTMaster.Rows.Count <= 0 Then MsgBox("No Records To Search.", vbInformation, AgLibrary.ClsMain.PubMsgTitleInfo) : Exit Sub
        Try


            Agl.PubFindQry = "Select  CommonMaster1.Code As SearchCode,  CommonMaster1.Description As [Description]  From  CommonMaster1 Where ModuleType = '" & mModuleType & "' and MasterType='" & mMasterType & "'  "


            Agl.PubFindQryOrdBy = "[Description]"


            '*************** common code start *****************
            AGL.PubObjFrmFind = New AgLibrary.FrmFindOld(Agl)
            AGL.PubObjFrmFind.ShowDialog()
            AGL.PubObjFrmFind = Nothing
            If AgL.PubSearchRow <> "" Then
                AgL.PubDRFound = DTMaster.Rows.Find(AgL.PubSearchRow)
                BMBMaster.Position = DTMaster.Rows.IndexOf(AgL.PubDRFound)
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
    Private Sub Topctrl1_tbPrn() Handles Topctrl1.tbPrn
        Dim ds As New DataSet
        Dim strQry As String = ""
        Try
            Me.Cursor = Cursors.WaitCursor
            AgL.PubReportTitle = "Customer Category 1 Master"
            If Not DTMaster.Rows.Count > 0 Then
                MsgBox("No Records Found to Print!!!", vbInformation, "Information")
                Me.Cursor = Cursors.Default
                Exit Sub
            End If


            strQry = "Select  CommonMaster1.Code As SearchCode,   CommonMaster1.Description As [Description]  From  CommonMaster1 Where  ModuleType = '" & mModuleType & "' and MasterType='" & mMasterType & "' "
            ds = Agl.FillData(strQry, Agl.GCn)

            Dim mPrnHnd As New PrintHandler(Agl)
            mPrnHnd.DataSetToPrint = ds
            mPrnHnd.LineThreshold = ds.Tables(0).Rows.Count - 1
            mPrnHnd.NumberOfColumns = ds.Tables(0).Columns.Count - 1
            mPrnHnd.ReportTitle = "Customer Category 1 Master"
            mPrnHnd.TableIndex = 0
            mPrnHnd.PageSetupDialog(True)
            mPrnHnd.PrintPreview()
            Call AgL.LogTableEntry(mSearchCode, Me.Text, "P", AgL.PubMachineName, AgL.PubUserName, AgL.PubLoginDate, AgL.GCn, AgL.ECmd)
        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try
        Me.Cursor = Cursors.Default
    End Sub
    Private Sub Topctrl1_tbSave() Handles Topctrl1.tbSave
        Dim MastPos As Long
        Dim mTrans As Boolean = False
        Try
            MastPos = BMBMaster.Position

            If AgCL.AgCheckMandatory(Me) = False Then Exit Sub


            If Topctrl1.Mode = "Add" Then
                Agl.ECmd = Agl.Dman_Execute("Select count(*) From CommonMaster1 Where Description='" & TxtDescription.Text & "' And ModuleType = '" & mModuleType & "' And MasterType = '" & mMasterType & "'  ", Agl.GCn)
                If AgL.ECmd.ExecuteScalar() > 0 Then MsgBox("Description Already Exist!") : TxtDescription.Focus() : Exit Sub


                mSearchCode = Agl.GetMaxId("CommonMaster1", "Code", Agl.GCn, Agl.PubDivCode, Agl.PubSiteCode, 8, True, True, , Agl.Gcn_ConnectionString)
            Else
                Agl.ECmd = Agl.Dman_Execute("Select count(*) From CommonMaster1 Where Description='" & TxtDescription.Text & "'  And ModuleType = '" & mModuleType & "' And MasterType = '" & mMasterType & "'  And Code<>'" & mSearchCode & "' ", Agl.GCn)
                If AgL.ECmd.ExecuteScalar() > 0 Then MsgBox("Description Already Exist!") : TxtDescription.Focus() : Exit Sub

            End If



            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = True


            If Topctrl1.Mode = "Add" Then
                InsertCommonMaster1(mTableCommonMaster1)
                InsertCommonMaster1(mTableCommonMaster1Alias)
            Else
                UpdateCommonMaster1(mTableCommonMaster1)
                UpdateCommonMaster1(mTableCommonMaster1Alias)
            End If




            Call AgL.LogTableEntry(mSearchCode, Me.Text, AgL.MidStr(Topctrl1.Mode, 0, 1), AgL.PubMachineName, AgL.PubUserName, AgL.PubLoginDate, AgL.GCn, AgL.ECmd)


            AgL.SynchroniseSiteOnLineData(AgL, AgL.GCn, AgL.Gcn_ConnectionString, AgL.GcnSite_ConnectionString, AgL.ECmd)
            AgL.ETrans.Commit()
            mTrans = False
            FIniMaster(0, 1)
            Topctrl1_tbRef()
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

    Public Sub InsertCommonMaster1(ByVal bTableName As String)
        mQry = "Insert Into " & bTableName & " (Code, Description, ManualCode, ModuleType,MasterType, Div_Code, Site_Code, U_EntDt, PreparedBy, U_AE) Values('" & mSearchCode & "',  " & Agl.Chk_Text(TxtDescription.Text) & ", '" & mSearchCode & "', '" & mModuleType & "','" & mMasterType & "','" & Agl.PubDivCode & "', '" & Agl.PubSiteCode & "', '" & Format(Agl.PubLoginDate, "Short Date") & "', '" & Agl.PubUserName & "', 'A') "
        Agl.Dman_ExecuteNonQry(mQry, Agl.GCn, Agl.ECmd)
    End Sub
    Public Sub UpdateCommonMaster1(ByVal bTableName As String)
        mQry = "Update " & bTableName & " Set  Description = " & Agl.Chk_Text(TxtDescription.Text) & ", Edit_Date='" & Format(Agl.PubLoginDate, "Short Date") & "', ModifiedBy = '" & Agl.PubUserName & "', U_AE = 'E' Where Code='" & mSearchCode & "' "
        Agl.Dman_ExecuteNonQry(mQry, Agl.GCn, Agl.ECmd)
    End Sub
    Public Sub MoveRec()
        Dim DsTemp As DataSet = Nothing
        Dim MastPos As Long
        Try
            FClear()
            BlankText()
            If DTMaster.Rows.Count > 0 Then
                MastPos = BMBMaster.Position
                mSearchCode = DTMaster.Rows(MastPos)("SearchCode")
                mQry = "Select CommonMaster1.* " & _
                    " From CommonMaster1 Where Code='" & mSearchCode & "'"
                DsTemp = AgL.FillData(mQry, AGL.GCn)
                With DsTemp.Tables(0)
                    If .Rows.Count > 0 Then
                        TxtDescription.AgSelectedValue = AgL.XNull(.Rows(0)("Code"))
                    End If
                End With
            Else
                BlankText()
            End If
            Topctrl1.FSetDispRec(BMBMaster)
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
    End Sub
    Private Sub FClear()
        DTStruct.Clear()
    End Sub
    Private Sub FAddRowStructure()
        Dim DRStruct As DataRow
        Try
            DRStruct = DTStruct.NewRow
            DTStruct.Rows.Add(DRStruct)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Control_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles _
        TxtDescription.Validating
        Try
            Select Case sender.NAME
                Case TxtDescription.Name
                    '<Executable Code>
            End Select

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

End Class
