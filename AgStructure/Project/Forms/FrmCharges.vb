Imports System.Data.SQLite
Public Class FrmCharges
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
        mQry = "Select Charges.Code As SearchCode " & _
        " From Charges "
        Topctrl1.FIniForm(DTMaster, AGL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub


    Sub Ini_List()
        mQry = "Select Code  As Code, ManualCode As Name From Charges " & _
            "  Order By ManualCode"
        TxtManualCode.AgHelpDataSet = Agl.FillData(mQry, Agl.Gcn)

        mQry = "Select Code  As Code, Description As Name From Charges " & _
            "  Order By Description"
        TxtDescription.AgHelpDataSet = Agl.FillData(mQry, Agl.Gcn)

    End Sub


    Private Sub Topctrl1_tbAdd() Handles Topctrl1.tbAdd
        BlankText()
        DispText()
        TxtManualCode.Focus()
    End Sub
    Private Sub Topctrl1_tbDel() Handles Topctrl1.tbDel
        Dim BlnTrans As Boolean = False
        Dim GCnCmd As New Object
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

                    AgL.Dman_ExecuteNonQry("Delete From Charges Where Code='" & mSearchCode & "'", AgL.GCn, AgL.ECmd)

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
        DispText()
        TxtManualCode.Focus()
    End Sub


    Private Sub Topctrl1_tbFind() Handles Topctrl1.tbFind
        If DTMaster.Rows.Count <= 0 Then MsgBox("No Records To Search.", vbInformation, AgLibrary.ClsMain.PubMsgTitleInfo) : Exit Sub
        Try


            AgL.PubFindQry = "Select  Charges.Code As SearchCode,  Charges.ManualCode As [Manual Code],  Charges.Description As [Description]  From  Charges "


            AGL.PubFindQryOrdBy = "[Manual Code]"


            '*************** common code start *****************
            AgL.PubObjFrmFind = New AgLibrary.FrmFind(AgL.PubFindQry, "Find", AgL)
            AgL.PubObjFrmFind.ShowDialog()
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
    End Sub
    Private Sub Topctrl1_tbSave() Handles Topctrl1.tbSave
        Dim MastPos As Long
        Dim mTrans As Boolean = False
        Try
            MastPos = BMBMaster.Position

            If AgCL.AgCheckMandatory(Me) = False Then Exit Sub


            If Topctrl1.Mode = "Add" Then
                AgL.ECmd = AgL.Dman_Execute("Select count(*) From Charges Where ManualCode='" & TxtManualCode.Text & "' ", AgL.GCn)
                If AgL.ECmd.ExecuteScalar() > 0 Then MsgBox("Manual Code Already Exist!") : TxtManualCode.Focus() : Exit Sub
                AgL.ECmd = AgL.Dman_Execute("Select count(*) From Charges Where Description='" & TxtDescription.Text & "' ", AgL.GCn)
                If AgL.ECmd.ExecuteScalar() > 0 Then MsgBox("Description Already Exist!") : TxtDescription.Focus() : Exit Sub

                AgL.ECmd = AgL.Dman_Execute("Select count(*) From Charges Where FieldName='" & TxtFieldName.Text & "' ", AgL.GCn)
                If AgL.ECmd.ExecuteScalar() > 0 Then MsgBox("Database Field Name Already Exist!") : TxtFieldName.Focus() : Exit Sub

                mSearchCode = TxtManualCode.Text
                'mSearchCode = AgL.GetMaxId("Charges", "Code", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 6, True, True, , AgL.Gcn_ConnectionString)
            Else
                AgL.ECmd = AgL.Dman_Execute("Select count(*) From Charges Where ManualCode='" & TxtManualCode.Text & "' And Code<>'" & mSearchCode & "' ", AgL.GCn)
                If AgL.ECmd.ExecuteScalar() > 0 Then MsgBox("Manual Code Already Exist!") : TxtManualCode.Focus() : Exit Sub
                AgL.ECmd = AgL.Dman_Execute("Select count(*) From Charges Where Description='" & TxtDescription.Text & "' And Code<>'" & mSearchCode & "' ", AgL.GCn)
                If AgL.ECmd.ExecuteScalar() > 0 Then MsgBox("Description Already Exist!") : TxtDescription.Focus() : Exit Sub
                AgL.ECmd = AgL.Dman_Execute("Select count(*) From Charges Where FieldName='" & TxtFieldName.Text & "' And Code<>'" & mSearchCode & "' ", AgL.GCn)
                If AgL.ECmd.ExecuteScalar() > 0 Then MsgBox("Database Field Name Already Exist!") : TxtFieldName.Focus() : Exit Sub

            End If



            AgL.ECmd = AGL.GCn.CreateCommand
            AgL.ETrans = AGL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = True


            If Topctrl1.Mode = "Add" Then
                mQry = "Insert Into Charges (Code, ManualCode, Description, FieldName, Div_Code, Site_Code, U_EntDt, PreparedBy, U_AE) Values('" & mSearchCode & "', " & AgL.Chk_Text(TxtManualCode.Text) & ", " & AgL.Chk_Text(TxtDescription.Text) & ", " & AgL.Chk_Text(TxtFieldName.Text) & ", '" & AgL.PubDivCode & "', '" & AgL.PubSiteCode & "', '" & Format(AgL.PubLoginDate, "Short Date") & "', '" & AgL.PubUserName & "', 'A') "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            Else
                mQry = "Update Charges Set ManualCode = " & AgL.Chk_Text(TxtManualCode.Text) & ", Description = " & AgL.Chk_Text(TxtDescription.Text) & ", FieldName = " & AgL.Chk_Text(TxtFieldName.Text) & ", Edit_Date='" & Format(AgL.PubLoginDate, "Short Date") & "', ModifiedBy = '" & AgL.PubUserName & "', U_AE = 'E' Where Code='" & mSearchCode & "' "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If


            Call AgL.LogTableEntry(mSearchCode, Me.Text, AgL.MidStr(Topctrl1.Mode, 0, 1), AgL.PubMachineName, AGL.PubUserName, AGL.PubLoginDate, AGL.GCn, AgL.ECmd)


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
                mQry = "Select Charges.* " & _
                    " From Charges Where Code='" & mSearchCode & "'"
                DsTemp = AgL.FillData(mQry, AGL.GCn)
                With DsTemp.Tables(0)
                    If .Rows.Count > 0 Then
                        TxtManualCode.AgSelectedValue = AGL.XNull(.Rows(0)("Code"))
                        TxtDescription.AgSelectedValue = AgL.XNull(.Rows(0)("Code"))
                        TxtFieldName.Text = AgL.XNull(.Rows(0)("FieldName"))
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
            TxtManualCode.Enabled = False
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

End Class
