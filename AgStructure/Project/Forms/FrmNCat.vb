
Public Class FrmNCat
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
        mQry = "Select VoucherCat.NCatDescription As SearchCode " & _
        " From VoucherCat "
        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub


    Sub Ini_List()
        mQry = "Select NCat  As Code, Category As Name From VoucherCat " & _
            "  Order By Category"
        TxtCategory.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)

        mQry = "Select NCat  As Code, NCatDescription As Name From VoucherCat " & _
            "  Order By NCatDescription"
        TxtNCatDescription.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)

    End Sub


    Private Sub Topctrl1_tbAdd() Handles Topctrl1.tbAdd
        BlankText()
        DispText()
        TxtCategory.Focus()
    End Sub
    Private Sub Topctrl1_tbDel() Handles Topctrl1.tbDel
        'Dim BlnTrans As Boolean = False
        'Dim GCnCmd As New Object
        'Dim MastPos As Long
        'Dim mTrans As Boolean = False


        'Try
        '    MastPos = BMBMaster.Position


        '    If DTMaster.Rows.Count > 0 Then
        '        If MsgBox("Are You Sure To Delete This Record?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, AgLibrary.ClsMain.PubMsgTitleInfo) = vbYes Then


        '            AgL.ECmd = AgL.GCn.CreateCommand
        '            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
        '            AgL.ECmd.Transaction = AgL.ETrans
        '            mTrans = True

        '            AgL.Dman_ExecuteNonQry("Delete From VoucherCat Where NCat='" & mSearchCode & "'", AgL.GCn, AgL.ECmd)

        '            Call AgL.LogTableEntry(mSearchCode, Me.Text, "D", AgL.PubMachineName, AgL.PubUserName, AgL.PubLoginDate, AgL.GCn, AgL.ECmd)

        '            AgL.SynchroniseSiteOnLineData(AgL, AgL.GCn, AgL.Gcn_ConnectionString, AgL.GcnSite_ConnectionString, AgL.ECmd)
        '            AgL.ETrans.Commit()
        '            mTrans = False


        '            FIniMaster(1)
        '            Topctrl1_tbRef()
        '            MoveRec()
        '        End If
        '    End If
        'Catch Ex As Exception
        '    If mTrans = True Then AgL.ETrans.Rollback()
        '    MsgBox(Ex.Message, MsgBoxStyle.Information, AgLibrary.ClsMain.PubMsgTitleInfo)
        'End Try
    End Sub

    Private Sub Topctrl1_tbDiscard() Handles Topctrl1.tbDiscard
        FIniMaster(0, 0)
        Topctrl1.Focus()
    End Sub

    Private Sub Topctrl1_tbEdit() Handles Topctrl1.tbEdit
        DispText()
        TxtCategory.Focus()
    End Sub

    Private Sub Topctrl1_tbFind() Handles Topctrl1.tbFind
        If DTMaster.Rows.Count <= 0 Then MsgBox("No Records To Search.", vbInformation, AgLibrary.ClsMain.PubMsgTitleInfo) : Exit Sub
        Try


            AgL.PubFindQry = "Select  VoucherCat.NCatDescription As SearchCode, VoucherCat.NCat,  VoucherCat.Category As [Category],  VoucherCat.NCatDescription As [NCatDescription]  From  VoucherCat "


            AgL.PubFindQryOrdBy = "[Category]"


            '*************** common code start *****************
            AgL.PubObjFrmFind = New AgLibrary.FrmFind(AgL.PubFindQry, "Find", AgL)
            AgL.PubObjFrmFind.ShowDialog()
            AgL.PubObjFrmFind = Nothing
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
    End Sub

    Private Sub Topctrl1_tbSave() Handles Topctrl1.tbSave
        Dim MastPos As Long
        Dim mTrans As Boolean = False
        Try
            MastPos = BMBMaster.Position

            If AgCL.AgCheckMandatory(Me) = False Then Exit Sub


            If Topctrl1.Mode = "Add" Then
                AgL.ECmd = AgL.Dman_Execute("Select count(*) From VoucherCat Where Category='" & TxtCategory.Text & "' ", AgL.GCn)
                If AgL.ECmd.ExecuteScalar() > 0 Then MsgBox("Category Already Exist!") : TxtCategory.Focus() : Exit Sub
                AgL.ECmd = AgL.Dman_Execute("Select count(*) From VoucherCat Where NCatDescription='" & TxtNCatDescription.Text & "' ", AgL.GCn)
                If AgL.ECmd.ExecuteScalar() > 0 Then MsgBox("NCatDescription Already Exist!") : TxtNCatDescription.Focus() : Exit Sub

                mSearchCode = TxtCategory.Text
                'mSearchCode = AgL.GetMaxId("VoucherCat", "Code", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 6, True, True, , AgL.Gcn_ConnectionString)
            Else
                AgL.ECmd = AgL.Dman_Execute("Select count(*) From VoucherCat Where Category='" & TxtCategory.Text & "' And NCatDescription<>'" & mSearchCode & "' ", AgL.GCn)
                If AgL.ECmd.ExecuteScalar() > 0 Then MsgBox("Category Already Exist!") : TxtCategory.Focus() : Exit Sub
                AgL.ECmd = AgL.Dman_Execute("Select count(*) From VoucherCat Where NCatDescription='" & TxtNCatDescription.Text & "' And NCatDescription<>'" & mSearchCode & "' ", AgL.GCn)
                If AgL.ECmd.ExecuteScalar() > 0 Then MsgBox("NCatDescription Already Exist!") : TxtNCatDescription.Focus() : Exit Sub
            End If



            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = True


            If Topctrl1.Mode = "Add" Then
                mQry = "Insert Into VoucherCat " & _
                       "(NCat, Category, NCatDescription, HeaderTable, " & _
                       "LineTable, LogHeaderTable, LogLineTable, Structure, " & _
                       "Site_Code) " & _
                       "Values " & _
                       "('" & mSearchCode & "', " & AgL.Chk_Text(TxtCategory.Text) & ", " & AgL.Chk_Text(TxtNCatDescription.Text) & ", " & AgL.Chk_Text(TxtHeaderTable.Tag) & ", " & _
                       "" & AgL.Chk_Text(TxtLineTable.Tag) & ", " & AgL.Chk_Text(TxtHeaderLogTable.Tag) & ", " & AgL.Chk_Text(TxtLineLogTable.Tag) & ", " & AgL.Chk_Text(TxtStructure.Tag) & ", " & _
                       "'" & AgL.PubSiteCode & "') "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            Else
                mQry = "Update VoucherCat Set " & _
                       "Category = " & AgL.Chk_Text(TxtCategory.Text) & ", " & _
                       "NCatDescription = " & AgL.Chk_Text(TxtNCatDescription.Text) & ", " & _
                       "HeaderTable = " & AgL.Chk_Text(TxtHeaderTable.Tag) & ", " & _
                       "LineTable = " & AgL.Chk_Text(TxtLineTable.Tag) & ", " & _
                       "LogHeaderTable = " & AgL.Chk_Text(TxtHeaderLogTable.Tag) & ", " & _
                       "LogLineTable = " & AgL.Chk_Text(TxtLineLogTable.Tag) & ", " & _
                       "Structure = " & AgL.Chk_Text(TxtStructure.Tag) & " " & _
                       "Where NCatDescription='" & mSearchCode & "' "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If


            Call AgL.LogTableEntry(mSearchCode, Me.Text, AgL.MidStr(Topctrl1.Mode, 0, 1), AgL.PubMachineName, AgL.PubUserName, AgL.PubLoginDate, AgL.GCn, AgL.ECmd)


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

    Public Sub MoveRec()
        Dim DsTemp As DataSet = Nothing
        Dim MastPos As Long
        Try
            FClear()
            BlankText()
            If DTMaster.Rows.Count > 0 Then
                MastPos = BMBMaster.Position
                mSearchCode = DTMaster.Rows(MastPos)("SearchCode")

                mQry = " Select H.*, OH.Name as HeaderTableName, OL.Name as LineTableName, OLH.Name as LogHeaderTableName, OLL.Name as LogLineTableName, S.Description as StructureDesc " & _
                    " From (Select * from VoucherCat Where NCatDescription='" & mSearchCode & "') H " & _
                    " Left Join Sys.Objects OH On H.HeaderTable = OH.Object_ID " & _
                    " Left Join Sys.Objects OL On H.LineTable = OL.Object_ID " & _
                    " Left Join Sys.Objects OLH On H.LogHeaderTable = OLH.Object_ID " & _
                    " Left Join Sys.Objects OLL On H.LogLineTable = OLL.Object_ID " & _
                    " Left Join Structure S On H.Structure = S.Code "
                DsTemp = AgL.FillData(mQry, AgL.GCn)

                With DsTemp.Tables(0)
                    If .Rows.Count > 0 Then
                        TxtCategory.AgSelectedValue = AgL.XNull(.Rows(0)("NCat"))
                        TxtNCatDescription.AgSelectedValue = AgL.XNull(.Rows(0)("NCat"))

                        TxtHeaderTable.Text = AgL.XNull(.Rows(0)("HeaderTableName"))
                        TxtHeaderTable.Tag = AgL.XNull(.Rows(0)("HeaderTable"))
                        TxtLineTable.Text = AgL.XNull(.Rows(0)("LineTableName"))
                        TxtLineTable.Tag = AgL.XNull(.Rows(0)("LineTable"))

                        TxtHeaderLogTable.Text = AgL.XNull(.Rows(0)("LogHeaderTableName"))
                        TxtHeaderLogTable.Tag = AgL.XNull(.Rows(0)("LogHeaderTable"))
                        TxtLineLogTable.Text = AgL.XNull(.Rows(0)("LogLineTableName"))
                        TxtLineLogTable.Tag = AgL.XNull(.Rows(0)("LogLineTable"))

                        TxtStructure.Text = AgL.XNull(.Rows(0)("StructureDesc"))
                        TxtStructure.Tag = AgL.XNull(.Rows(0)("Structure"))
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
        If Topctrl1.Mode <> "Add" Then Topctrl1.BlankTextBoxes()
        mSearchCode = ""
    End Sub
    Private Sub DispText(Optional ByVal Enb As Boolean = False)
        'Coding To Enable/Disable Controls
        If Topctrl1.Mode <> "Add" Then
            TxtCategory.Enabled = False
        End If
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


    Private Sub TxtStructure_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtStructure.KeyDown, TxtHeaderLogTable.KeyDown, TxtHeaderTable.KeyDown, TxtLineLogTable.KeyDown, TxtLineTable.KeyDown
        Select Case sender.name
            Case TxtHeaderTable.Name, TxtLineTable.Name, TxtHeaderLogTable.Name, TxtLineLogTable.Name
                If e.KeyCode <> Keys.Enter Then
                    If CType(sender, AgControls.AgTextBox).AgHelpDataSet Is Nothing Then
                        mQry = "SELECT O.object_id, O.name  FROM sys.objects O WHERE type ='U'"
                        CType(sender, AgControls.AgTextBox).AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)
                    End If
                End If

            Case TxtStructure.Name
                If e.KeyCode <> Keys.Enter Then
                    If CType(sender, AgControls.AgTextBox).AgHelpDataSet Is Nothing Then
                        mQry = "SELECT H.Code, H.Description  FROM Structure H"
                        TxtStructure.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)
                    End If
                End If
        End Select
    End Sub
End Class
