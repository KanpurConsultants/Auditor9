
Public Class FrmVoucherTypeMaster
    Private DTMaster As New DataTable()
    Public BMBMaster As BindingManagerBase
    Private KEAMainKeyCode As System.Windows.Forms.KeyEventArgs
    Private DTStruct As New DataTable
    Dim mQry As String = "", mSearchCode As String = "", mIsSystemDefine As Boolean

    Private Const NumberingMethod_Automatic As String = "Automatic"
    Private Const NumberingMethod_Manual As String = "Manual"
    Dim mNumberMethod = "" & NumberingMethod_Automatic & "," & NumberingMethod_Manual & ""

    'Const Declared for Dgl1
    Private Const Col_SNo As Byte = 0
    Public WithEvents DGL1 As New AgControls.AgDataGrid
    Private Const Col1Date_From As Byte = 1
    Private Const Col1Date_To As Byte = 2
    Private Const Col1Prefix As Byte = 3
    Private Const Col1Start_Srl_No As Byte = 4

    'Const Declared for Dgl2
    Public WithEvents DGL2 As New AgControls.AgDataGrid
    Private Const Col2GroupCode As Byte = 1
    Private Const Col2Dr As Byte = 2
    Private Const Col2Cr As Byte = 3

    'Const Declared for Dgl3
    Public WithEvents DGL3 As New AgControls.AgDataGrid
    Private Const Col3GroupCode As Byte = 1
    Private Const Col3Dr As Byte = 2
    Private Const Col3Cr As Byte = 3



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
        ''==============================================================================
        ''================< Prefix Details Data Grid >====================================
        ''==============================================================================
        With AgCL
            .AddAgTextColumn(DGL1, "DGL1SNo", 50, 5, "S.No.", True, True, False)
            .AddAgDateColumn(DGL1, "Dgl1FromDate", 100, "From Date", True, False, False, True)
            .AddAgDateColumn(DGL1, "Dgl1ToDate", 100, "To Date", True, False, False, True)
            .AddAgTextColumn(DGL1, "Dgl1Prefix", 100, 5, "Prefix", True, False, False, True)
            .AddAgNumberColumn(DGL1, "Dgl1StartSrNo", 100, 8, 0, False, "Start Sr No.", True, False, True)
        End With
        AgL.AddAgDataGrid(DGL1, Pnl1)
        DGL1.ColumnHeadersHeight = 40


        ''==============================================================================
        ''================< A/C Include Data Grid >====================================
        ''==============================================================================
        With AgCL
            .AddAgTextColumn(DGL2, "DGL2SNo", 50, 5, "S.No.", True, True, False)
            .AddAgTextColumn(DGL2, "DGL2GroupName", 200, 5, "A/c Group", True, False, False)
            .AddAgCheckBoxColumn(DGL2, "DGL2Dr", 50, "Dr.", True, False, True)
            .AddAgCheckBoxColumn(DGL2, "DGL2Cr", 50, "Cr.", True, False, True)
        End With
        AgL.AddAgDataGrid(DGL2, Pnl2)
        DGL2.ColumnHeadersHeight = 40


        ''==============================================================================
        ''================< A/C Exclude Data Grid >====================================
        ''==============================================================================
        With AgCL
            .AddAgTextColumn(DGL3, "DGL3SNo", 50, 5, "S.No.", True, True, False)
            .AddAgTextColumn(DGL3, "DGL3GroupName", 200, 5, "A/c Group", True, False, False)

            .AddAgCheckBoxColumn(DGL3, "DGL3Dr", 50, "Dr.", True, False, True)
            .AddAgCheckBoxColumn(DGL3, "DGL3Cr", 50, "Cr.", True, False, True)

        End With
        AgL.AddAgDataGrid(DGL3, Pnl3)
        DGL3.ColumnHeadersHeight = 40

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
            AgL.WinSetting(Me, 650, 880, 0, 0)
            AgL.GridDesign(DGL1)
            AgL.GridDesign(DGL2)
            AgL.GridDesign(DGL3)
            IniGrid()
            Topctrl1.ChangeAgGridState(DGL1, False)
            Topctrl1.ChangeAgGridState(DGL2, False)
            Topctrl1.ChangeAgGridState(DGL3, False)
            FIniMaster()
            Ini_List()
            DispText()
            MoveRec()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub FIniMaster(Optional ByVal BytDel As Byte = 0, Optional ByVal BytRefresh As Byte = 1)
        Dim bCondStr$ = " Where 1=1 "

        If AgL.PubMoveRecApplicable Then
            If Not AgLibrary.ClsConstant.IsOldFaVoucherEntryActive Then
                bCondStr += " And Vt.NCat <> " & AgL.Chk_Text(AgLibrary.ClsConstant.NCat_OldFaVoucher) & " "
            End If

            If Not AgLibrary.ClsConstant.IsNewFaVoucherEntryActive Then
                bCondStr += " And Vt.NCat <> " & AgL.Chk_Text(AgLibrary.ClsConstant.NCat_Voucher) & " "
            End If

            If Not AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Or Not AgL.StrCmp(AgL.PubUserName, "SA") Then
                bCondStr += " And IsNull(Vt.Affect_FA,0) <> 0 "
            End If

            mQry = "Select Vt.V_Type As SearchCode " & _
                    " From Voucher_Type Vt " & bCondStr
            Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
        End If
    End Sub

    Sub Ini_List()
        mQry = "SELECT Vt.V_Type as Code,Vt.V_Type as Name " & _
                " FROM Voucher_Type Vt " & _
                " Order By Vt.V_Type "
        TxtV_Type.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)

        mQry = " SELECT MnuText AS Code, MnuText, MnuName , MnuModule AS Module " & _
                " FROM User_Permission WHERE UserName ='SA' ORDER BY MnuText"
        TxtMenuName.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)

        mQry = "SELECT DISTINCT Vt.Description as Code, Vt.Description as [Description]" & _
                " FROM Voucher_Type Vt " & _
                " Order By Vt.Description "
        TxtDescription.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)

        mQry = "SELECT DISTINCT Vt.Short_Name as Code,Vt.Short_Name as Name " & _
                " FROM Voucher_Type Vt " & _
                " Order By Vt.Short_Name "
        TxtShort_Name.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)

        mQry = "SELECT DISTINCT vc.Category as Code, vc.Category as Name " & _
                " FROM VoucherCat vc " & _
                " Order By Vc.Category "
        TxtCategory.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)



        AgCL.IniAgHelpList(TxtNumber_Method, mNumberMethod)


        mQry = "SELECT Acg.GroupCode as Code, AcG.GroupName as Name  " & _
                " FROM AcGroup Acg  " & _
                " ORDER BY Acg.GroupName "
        DGL2.AgHelpDataSet(Col2GroupCode, 0, Tc1.Top + Tp2.Top, Tc1.Left + Tp2.Left) = AgL.FillData(mQry, AgL.GCn)
        DGL3.AgHelpDataSet(Col3GroupCode, 0, Tc1.Top + Tp3.Top, Tc1.Left + Tp3.Left) = AgL.FillData(mQry, AgL.GCn)


        mQry = "SELECT vpt.V_Prefix as Code, vpt.V_Prefix as Name " & _
                " FROM Voucher_Prefix_Type vpt " & _
                " Order By Vpt.V_Prefix "
        DGL1.AgHelpDataSet(Col1Prefix, 0, Tc1.Top + Tp1.Top, Tc1.Left + Tp1.Left) = AgL.FillData(mQry, AgL.GCn)
    End Sub

    Private Sub Topctrl1_tbAdd() Handles Topctrl1.tbAdd
        BlankText()
        DispText(True)

        LblSystemDefine.Text = "User Define"

        TxtV_Type.Focus()
    End Sub

    Private Sub Topctrl1_tbDel() Handles Topctrl1.tbDel
        Dim BlnTrans As Boolean = False
        Dim GCnCmd As New SqlClient.SqlCommand
        Dim MastPos As Long
        Dim mTrans As Boolean = False


        Try
            If AgL.PubMoveRecApplicable Then MastPos = BMBMaster.Position

            If mSearchCode <> "" Then
                If Not AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Then
                    If mIsSystemDefine Then MsgBox("Permission Denied!..." & vbCrLf & "Voucher Type is System Defined!...", MsgBoxStyle.Information, "Validation Check") : Exit Sub
                End If

                If MsgBox("Are You Sure To Delete This Record?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, AgLibrary.ClsMain.PubMsgTitleInfo) = vbYes Then
                    AgL.ECmd = AgL.GCn.CreateCommand
                    AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
                    AgL.ECmd.Transaction = AgL.ETrans
                    mTrans = True

                    AgL.Dman_ExecuteNonQry("Delete From Voucher_Exclude Where V_Type='" & mSearchCode & "' ", AgL.GCn, AgL.ECmd)
                    AgL.Dman_ExecuteNonQry("Delete From Voucher_Include Where V_Type='" & mSearchCode & "' ", AgL.GCn, AgL.ECmd)
                    AgL.Dman_ExecuteNonQry("Delete From Voucher_Prefix Where V_Type='" & mSearchCode & "' ", AgL.GCn, AgL.ECmd)
                    AgL.Dman_ExecuteNonQry("Delete From Voucher_Type Where V_Type='" & mSearchCode & "'", AgL.GCn, AgL.ECmd)

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
        DispText(True)
        TxtShort_Name.Focus()
    End Sub

    Private Sub Topctrl1_tbFind() Handles Topctrl1.tbFind
        'If DTMaster.Rows.Count <= 0 Then MsgBox("No Records To Search.", vbInformation, AgLibrary.ClsMain.PubMsgTitleInfo) : Exit Sub
        Try
            Dim bCondStr$ = " Where 1=1 "

            If Not AgLibrary.ClsConstant.IsOldFaVoucherEntryActive Then
                bCondStr += " And Vt.NCat <> " & AgL.Chk_Text(AgLibrary.ClsConstant.NCat_OldFaVoucher) & " "
            End If

            If Not AgLibrary.ClsConstant.IsNewFaVoucherEntryActive Then
                bCondStr += " And Vt.NCat <> " & AgL.Chk_Text(AgLibrary.ClsConstant.NCat_Voucher) & " "
            End If

            If Not AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Then
                bCondStr += " And IsNull(Vt.Affect_FA,0) <> 0 "
            End If

            AgL.PubFindQry = "Select Vt.V_Type As SearchCode,Vt.V_Type, Vt.Description, vt.Short_Name, Vt.NCat As [Nature Code], vt.Category  " & _
                                " From Voucher_Type Vt " & bCondStr

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
        Dim I, J As Integer
        Dim DsTemp As DataSet = Nothing
        Dim bSite_Code As String = ""
        Dim MastPos As Long
        Dim mTrans As Boolean = False

        Dim GcnRead As SqlClient.SqlConnection

        GcnRead = New SqlClient.SqlConnection
        GcnRead.ConnectionString = AgL.Gcn_ConnectionString
        GcnRead.Open()

        Try
            If AgL.PubMoveRecApplicable Then MastPos = BMBMaster.Position


            If Not Data_Validation() Then Exit Sub

            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = True


            If Topctrl1.Mode = "Add" Then

                mQry = "INSERT INTO Voucher_Type (NCat,Category,V_Type,Description,Short_Name," & _
                        " MnuName, MnuText, MnuAttachedInModule, " & _
                        " SystemDefine,DivisionWise,SiteWise,PreparedBy,U_EntDt,U_AE,Number_Method," & _
                        " Saperate_Narr,Common_Narr,Narration, ChqNo,ChqDt,ClgDt,Affect_Fa,IsShowVoucherReference) " & _
                        " VALUES (" & AgL.Chk_Text(TxtNCat.Text) & "," & AgL.Chk_Text(TxtCategory.Text) & "," & _
                        " " & AgL.Chk_Text(TxtV_Type.Text) & "," & AgL.Chk_Text(TxtDescription.Text) & "," & _
                        " " & AgL.Chk_Text(TxtShort_Name.Text) & ", " & _
                        " " & AgL.Chk_Text(TxtModule.Text) & ", " & AgL.Chk_Text(TxtMenuName.Text) & "," & AgL.Chk_Text(TxtModule.Tag) & ", " & _
                        " '" & IIf(mIsSystemDefine, "Y", "N") & "',0," & _
                        " " & IIf(AgL.PubSitewiseV_No, 1, 0) & ",'" & AgL.PubUserName & "', " & _
                        " '" & AgL.PubLoginDate & "','A'," & AgL.Chk_Text(TxtNumber_Method.Text) & "," & _
                        " '" & IIf(AgL.StrCmp(TxtSaperate_Narr.Text, "Yes"), "Y", "N") & "'," & _
                        " '" & IIf(AgL.StrCmp(TxtCommon_Narr.Text, "Yes"), "Y", "N") & "'," & _
                        " " & AgL.Chk_Text(TxtNarration.Text) & " ," & _
                        " '" & IIf(AgL.StrCmp(TxtChqNo.Text, "Yes"), "Y", "N") & "'," & _
                        " '" & IIf(AgL.StrCmp(TxtChqDate.Text, "Yes"), "Y", "N") & "'," & _
                        " '" & IIf(AgL.StrCmp(TxtClgDt.Text, "Yes"), "Y", "N") & "' ," & _
                        " " & IIf(AgL.StrCmp(TxtAffect_FA.Text, "Yes"), 1, 0) & ", " & _
                        " " & IIf(AgL.StrCmp(TxtIsShowVoucherReference.Text, "Yes"), 1, 0) & "  )"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

            Else

                mQry = "UPDATE Voucher_Type  " & _
                        " SET " & _
                        " Description = " & AgL.Chk_Text(TxtDescription.Text) & ", " & _
                        " Short_Name = " & AgL.Chk_Text(TxtShort_Name.Text) & ", " & _
                        " MnuText = " & AgL.Chk_Text(TxtMenuName.Text) & ", " & _
                        " MnuName = " & AgL.Chk_Text(TxtModule.Text) & ", " & _
                        " MnuAttachedInModule = " & AgL.Chk_Text(TxtModule.Tag) & ", " & _
                        " Edit_Date='" & Format(AgL.PubLoginDate, "Short Date") & "', U_AE = 'E'," & _
                        " ModifiedBy = '" & AgL.PubUserName & "'," & _
                        " Number_Method = " & AgL.Chk_Text(TxtNumber_Method.Text) & "," & _
                        " Saperate_Narr = '" & IIf(AgL.StrCmp(TxtSaperate_Narr.Text, "Yes"), "Y", "N") & "', " & _
                        " Common_Narr = '" & IIf(AgL.StrCmp(TxtCommon_Narr.Text, "Yes"), "Y", "N") & "', " & _
                        " Narration= " & AgL.Chk_Text(TxtNarration.Text) & " ," & _
                        " ChqNo = '" & IIf(AgL.StrCmp(TxtChqNo.Text, "Yes"), "Y", "N") & "', " & _
                        " ChqDt = '" & IIf(AgL.StrCmp(TxtChqDate.Text, "Yes"), "Y", "N") & "', " & _
                        " ClgDt = '" & IIf(AgL.StrCmp(TxtClgDt.Text, "Yes"), "Y", "N") & "', " & _
                        " Affect_FA = " & IIf(AgL.StrCmp(TxtAffect_FA.Text, "Yes"), 1, 0) & ", " & _
                        " IsShowVoucherReference = " & IIf(AgL.StrCmp(TxtIsShowVoucherReference.Text, "Yes"), 1, 0) & " " & _
                        " WHERE V_Type='" & mSearchCode & "'"

                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

            End If


            If Topctrl1.Mode = "Edit" Then
                mQry = "DELETE FROM Voucher_Prefix " & _
                        " WHERE V_Type = '" & mSearchCode & "' " & _
                        " AND Site_Code = '" & AgL.PubSiteCode & "' " & _
                        " AND Div_Code = '" & AgL.PubDivCode & "' " & _
                        " AND IsNull(Comp_Code,'" & AgL.PubCompCode & "') = '" & AgL.PubCompCode & "' " & _
                        " AND (" & AgL.ConvertDate(AgL.PubStartDate) & " BETWEEN Date_From AND Date_To " & _
                        " OR " & AgL.ConvertDate(AgL.PubEndDate) & " BETWEEN Date_From AND Date_To )"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                mQry = "Delete From Voucher_Include Where V_Type='" & mSearchCode & "' "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                mQry = "Delete From Voucher_Exclude Where V_Type='" & mSearchCode & "' "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If

            DsTemp = AgL.FillData("SELECT S.Code AS Site_Code FROM SiteMast S With (NoLock) ", GcnRead)

            With DGL1
                If DsTemp.Tables(0).Rows.Count > 0 Then
                    For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                        bSite_Code = DsTemp.Tables(0).Rows(I)("Site_Code")
                        For J = 0 To .Rows.Count - 1
                            If .Item(Col1Prefix, J).Value.ToString <> "" Then
                                mQry = "If Not Exists (Select * from Voucher_Prefix Where V_Type=" & AgL.Chk_Text(TxtV_Type.Text) & " " & _
                                        " And Prefix = " & AgL.Chk_Text(.AgSelectedValue(Col1Prefix, J)) & " " & _
                                        " And Date_From = " & AgL.Chk_Text(.Item(Col1Date_From, J).Value) & " " & _
                                        " And Date_To = " & AgL.Chk_Text(.Item(Col1Date_To, J).Value) & " " & _
                                        " And Site_Code = '" & bSite_Code & "' " & _
                                        " And Div_Code = '" & AgL.PubDivCode & "' " & _
                                        " And IsNull(Comp_Code,'" & AgL.PubCompCode & "') = '" & AgL.PubCompCode & "' " & _
                                        " ) " & _
                                        " INSERT INTO dbo.Voucher_Prefix( V_Type, Date_From, Prefix, Start_Srl_No, Date_To,  " & _
                                        " Site_Code, Div_Code, Comp_Code)  " & _
                                        " VALUES (" & AgL.Chk_Text(TxtV_Type.Text) & ", " & AgL.ConvertDate(.Item(Col1Date_From, J).Value.ToString) & "," & _
                                        " " & AgL.Chk_Text(.AgSelectedValue(Col1Prefix, J)) & "," & Val(.Item(Col1Start_Srl_No, J).Value) & "," & _
                                        " " & AgL.ConvertDate(.Item(Col1Date_To, J).Value.ToString) & ",  " & _
                                        " '" & bSite_Code & "',	'" & AgL.PubDivCode & "', '" & AgL.PubCompCode & "')"
                                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                            End If
                        Next J
                    Next I
                End If
            End With

            With DGL2
                For J = 0 To .Rows.Count - 1
                    If .Item(Col2GroupCode, J).Value <> "" Then
                        mQry = " INSERT INTO Voucher_Include ( V_Type, GroupCode, Dr, Cr, SITE_CODE)  " & _
                                " VALUES (" & AgL.Chk_Text(TxtV_Type.Text) & "," & AgL.Chk_Text(.AgSelectedValue(Col2GroupCode, J)) & "," & _
                                " '" & IIf(.Item(Col2Dr, J).Value, "Y", "N") & "', " & _
                                " '" & IIf(.Item(Col2Cr, J).Value, "Y", "N") & "', '" & AgL.PubSiteCode & "')"
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    End If
                Next J
            End With


            With DGL3
                For J = 0 To .Rows.Count - 1
                    If .Item(Col3GroupCode, J).Value <> "" Then
                        mQry = " INSERT INTO Voucher_Exclude ( V_Type, GroupCode, Dr, Cr, SITE_CODE)  " & _
                                " VALUES (" & AgL.Chk_Text(TxtV_Type.Text) & "," & AgL.Chk_Text(.AgSelectedValue(Col3GroupCode, J)) & "," & _
                                " '" & IIf(.Item(Col3Dr, J).Value, "Y", "N") & "', " & _
                                " '" & IIf(.Item(Col3Cr, J).Value, "Y", "N") & "', '" & AgL.PubSiteCode & "')"
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    End If
                Next J
            End With



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
        Finally
            If DsTemp IsNot Nothing Then DsTemp.Dispose()
        End Try
    End Sub

    Public Sub MoveRec()
        Dim i As Integer
        Dim DsTemp As DataSet = Nothing
        Dim DrTemp As DataRow() = Nothing
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

            If mSearchCode.ToString.Trim <> "" Then
                TxtV_Type.Text = mSearchCode
                mQry = "Select Vt.* " & _
                        " From Voucher_Type Vt " & _
                        " Where Vt.V_Type='" & mSearchCode & "'"
                DsTemp = AgL.FillData(mQry, AgL.GCn)
                With DsTemp.Tables(0)
                    If .Rows.Count > 0 Then
                        TxtV_Type.Text = mSearchCode
                        TxtDescription.Text = AgL.XNull(.Rows(0)("Description"))
                        TxtShort_Name.Text = AgL.XNull(.Rows(0)("Short_Name"))

                        TxtMenuName.Text = AgL.XNull(.Rows(0)("MnuText"))
                        TxtMenuName.Tag = AgL.XNull(.Rows(0)("MnuText"))
                        TxtModule.Text = AgL.XNull(.Rows(0)("MnuName"))
                        TxtModule.Tag = AgL.XNull(.Rows(0)("MnuAttachedInModule"))

                        TxtCategory.AgSelectedValue = AgL.XNull(.Rows(0)("Category"))
                        TxtNCat.Text = AgL.XNull(.Rows(0)("NCat"))


                        TxtNumber_Method.Text = AgL.XNull(.Rows(0)("Number_Method"))

                        TxtSaperate_Narr.Text = IIf(AgL.StrCmp(AgL.XNull(.Rows(0)("Saperate_Narr")), "Y"), "Yes", "No")
                        TxtCommon_Narr.Text = IIf(AgL.StrCmp(AgL.XNull(.Rows(0)("Common_Narr")), "Y"), "Yes", "No")

                        TxtChqNo.Text = IIf(AgL.StrCmp(AgL.XNull(.Rows(0)("ChqNo")), "Y"), "Yes", "No")
                        TxtChqDate.Text = IIf(AgL.StrCmp(AgL.XNull(.Rows(0)("ChqDt")), "Y"), "Yes", "No")
                        TxtClgDt.Text = IIf(AgL.StrCmp(AgL.XNull(.Rows(0)("ClgDt")), "Y"), "Yes", "No")
                        TxtAffect_FA.Text = IIf(AgL.VNull(.Rows(0)("Affect_FA")), "Yes", "No")
                        TxtIsShowVoucherReference.Text = IIf(AgL.VNull(.Rows(0)("IsShowVoucherReference")), "Yes", "No")
                        TxtNarration.Text = AgL.XNull(.Rows(0)("Narration"))

                        LblSystemDefine.Text = IIf(AgL.StrCmp(AgL.XNull(.Rows(0)("SystemDefine")), "Y"), "System Define", "User Define")
                        mIsSystemDefine = AgL.StrCmp(AgL.XNull(.Rows(0)("SystemDefine")), "Y")
                    End If
                End With

                mQry = "SELECT V.* " & _
                        " FROM Voucher_Prefix V " & _
                        " WHERE V.V_Type='" & mSearchCode & "' " & _
                        " AND V.Site_Code = '" & AgL.PubSiteCode & "' " & _
                        " AND IsNull(V.Comp_Code,'" & AgL.PubCompCode & "') = '" & AgL.PubCompCode & "' " & _
                        " And (V.Date_From Between " & AgL.ConvertDate(AgL.PubStartDate) & " And " & AgL.ConvertDate(AgL.PubEndDate) & " Or " & _
                        " V.Date_To Between " & AgL.ConvertDate(AgL.PubStartDate) & " And " & AgL.ConvertDate(AgL.PubEndDate) & ") "

                DsTemp = AgL.FillData(mQry, AgL.GCn)
                With DsTemp.Tables(0)
                    DGL1.RowCount = 1 : DGL1.Rows.Clear()
                    If .Rows.Count > 0 Then
                        For i = 0 To DsTemp.Tables(0).Rows.Count - 1
                            DGL1.Rows.Add()
                            DGL1.Item(Col_SNo, i).Value = DGL1.Rows.Count - 1
                            DGL1.Item(Col1Date_From, i).Value = Format(AgL.XNull(.Rows(i)("Date_From")), AgLibrary.ClsConstant.DateFormat_ShortDate)
                            DGL1.Item(Col1Date_To, i).Value = Format(AgL.XNull(.Rows(i)("Date_To")), AgLibrary.ClsConstant.DateFormat_ShortDate)
                            DGL1.AgSelectedValue(Col1Prefix, i) = AgL.XNull(.Rows(i)("Prefix"))
                            DGL1.Item(Col1Start_Srl_No, i).Value = AgL.VNull(.Rows(i)("Start_Srl_No"))
                        Next i
                    End If
                End With


                mQry = "SELECT Vi.* " & _
                        " FROM Voucher_Include Vi " & _
                        " WHERE Vi.V_Type = '" & mSearchCode & "' " & _
                        " And Vi.Site_Code = '" & AgL.PubSiteCode & "'"
                DsTemp = AgL.FillData(mQry, AgL.GCn)

                With DsTemp.Tables(0)
                    DGL2.RowCount = 1 : DGL2.Rows.Clear()
                    If .Rows.Count > 0 Then
                        For i = 0 To DsTemp.Tables(0).Rows.Count - 1
                            DGL2.Rows.Add()
                            DGL2.Item(Col_SNo, i).Value = DGL2.Rows.Count - 1
                            DGL2.AgSelectedValue(Col2GroupCode, i) = AgL.XNull(.Rows(i)("GroupCode"))
                            DGL2.Item(Col2Dr, i).Value = AgL.StrCmp(AgL.XNull(.Rows(i)("Dr")), "Y")
                            DGL2.Item(Col2Cr, i).Value = AgL.StrCmp(AgL.XNull(.Rows(i)("Cr")), "Y")
                        Next i
                    End If
                End With


                mQry = "SELECT * FROM Voucher_Exclude WHERE V_Type='" & mSearchCode & "' and " & AgL.PubSiteCondition("Site_Code", AgL.PubSiteCode) & " "
                DsTemp = AgL.FillData(mQry, AgL.GCn)

                With DsTemp.Tables(0)
                    DGL3.RowCount = 1 : DGL3.Rows.Clear()
                    If .Rows.Count > 0 Then
                        For i = 0 To DsTemp.Tables(0).Rows.Count - 1
                            DGL3.Rows.Add()
                            DGL3.Item(Col_SNo, i).Value = DGL3.Rows.Count - 1
                            DGL3.AgSelectedValue(Col3GroupCode, i) = AgL.XNull(.Rows(i)("GroupCode"))
                            DGL3.Item(Col3Dr, i).Value = AgL.StrCmp(AgL.XNull(.Rows(i)("Dr")), "Y")
                            DGL3.Item(Col3Cr, i).Value = AgL.StrCmp(AgL.XNull(.Rows(i)("Cr")), "Y")
                        Next i
                    End If
                End With
            Else
                BlankText()
            End If
            If AgL.PubMoveRecApplicable Then Topctrl1.FSetDispRec(BMBMaster)


            If Not (AgL.PubIsUserAdmin Or AgL.StrCmp(AgL.PubUserName, "SA") Or _
                    AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName)) Then

                Topctrl1.tAdd = False
                Topctrl1.tEdit = False
                Topctrl1.tDel = False
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            DsTemp = Nothing
            Topctrl1.tPrn = False
        End Try
    End Sub

    Private Sub BlankText()
        If Topctrl1.Mode <> "Add" Then Topctrl1.BlankTextBoxes(Me)
        mSearchCode = "" : LblSystemDefine.Text = ""

        DGL1.RowCount = 1 : DGL1.Rows.Clear()
        DGL2.RowCount = 1 : DGL2.Rows.Clear()
        DGL3.RowCount = 1 : DGL3.Rows.Clear()

        mIsSystemDefine = False

        TxtNumber_Method.Text = NumberingMethod_Automatic
        TxtChqNo.Text = "No" : TxtChqDate.Text = "No" : TxtClgDt.Text = "No" : TxtIsShowVoucherReference.Text = "No"
        TxtCommon_Narr.Text = "No" : TxtSaperate_Narr.Text = "No" : TxtAffect_FA.Text = "Yes"
    End Sub

    Private Sub DispText(Optional ByVal Enb As Boolean = False)
        'Coding To Enable/Disable Controls
        TxtAffect_FA.Visible = AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName)
        LblAffect_FA.Visible = AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName)

        TxtCategory.Enabled = False : TxtNCat.Enabled = False

        If AgL.StrCmp(Topctrl1.Mode, "Edit") Then
            TxtV_Type.Enabled = False
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

    Private Function Data_Validation() As Boolean
        Dim I As Integer
        Try
            If AgL.RequiredField(TxtV_Type) Then Exit Function
            If AgL.RequiredField(TxtDescription) Then Exit Function
            If AgL.RequiredField(TxtShort_Name) Then Exit Function            
            If AgL.RequiredField(TxtCategory) Then Exit Function
            If AgL.RequiredField(TxtNCat) Then Exit Function


            If AgCL.AgIsBlankGrid(DGL1, Col1Prefix) Then Exit Function
            ' If AgCL.AgIsDuplicate(DGL1, "" & Col1Prefix & "," & Col1Date_From & "") Then Exit Function

            With DGL1
                For I = 0 To .Rows.Count - 1
                    If .Item(Col1Date_From, I).Value Is Nothing Then .Item(Col1Date_From, I).Value = ""
                    If .Item(Col1Date_To, I).Value Is Nothing Then .Item(Col1Date_To, I).Value = ""
                    If .Item(Col1Prefix, I).Value Is Nothing Then .Item(Col1Prefix, I).Value = ""
                    If .Item(Col1Start_Srl_No, I).Value Is Nothing Then .Item(Col1Start_Srl_No, I).Value = ""

                    If .Item(Col1Prefix, I).Value.ToString.Trim <> "" Then
                        If .Item(Col1Date_From, I).Value.ToString.Trim = "" Then
                            MsgBox("To Date Is Requiered At Row No. : " & .Item(Col_SNo, I).Value & "!...")
                            DGL1.CurrentCell = DGL1(Col1Date_To, I) : DGL1.Focus() : Exit Function
                        End If

                        If .Item(Col1Date_To, I).Value.ToString.Trim = "" Then
                            MsgBox("To Date Is Requiered At Row No. : " & .Item(Col_SNo, I).Value & "!...")
                            DGL1.CurrentCell = DGL1(Col1Date_To, I) : DGL1.Focus() : Exit Function
                        End If

                        If CDate(.Item(Col1Date_To, I).Value) <= CDate(.Item(Col1Date_From, I).Value) Then
                            MsgBox("""Date To"" Is Less Than Or Equal To From ""Date From"" At Row No : " & .Item(Col_SNo, I).Value & "")
                            DGL1.CurrentCell = DGL1(Col1Date_To, I) : DGL1.Focus() : Exit Function
                        End If

                        If .Item(Col1Prefix, I).Value.ToString.Trim = "" Then
                            MsgBox("Prefix Is Requiered At Row No. : " & .Item(Col_SNo, I).Value & "!...")
                            DGL1.CurrentCell = DGL1(Col1Prefix, I) : DGL1.Focus() : Exit Function
                        End If
                    End If
                Next
            End With

            If AgCL.AgIsDuplicate(DGL2, "" & Col2GroupCode & "") Then Exit Function
            If AgCL.AgIsDuplicate(DGL3, "" & Col3GroupCode & "") Then Exit Function

            If Topctrl1.Mode = "Add" Then
                AgL.ECmd = AgL.Dman_Execute("Select count(*) From Voucher_Type Where V_Type='" & TxtV_Type.Text & "' ", AgL.GCn)
                If AgL.ECmd.ExecuteScalar() > 0 Then MsgBox("Voucher Type Already Exist!") : TxtV_Type.Focus() : Exit Function

                AgL.ECmd = AgL.Dman_Execute("Select count(*) From Voucher_Type Where Description='" & TxtDescription.Text & "' ", AgL.GCn)
                If AgL.ECmd.ExecuteScalar() > 0 Then MsgBox("Description Already Exist!") : TxtDescription.Focus() : Exit Function

                AgL.ECmd = AgL.Dman_Execute("Select count(*) From Voucher_Type Where Short_Name='" & TxtShort_Name.Text & "' ", AgL.GCn)
                If AgL.ECmd.ExecuteScalar() > 0 Then MsgBox("Short Name Already Exist!") : TxtShort_Name.Focus() : Exit Function

                mSearchCode = TxtV_Type.Text

            Else
                AgL.ECmd = AgL.Dman_Execute("Select count(*) From Voucher_Type Where Description='" & TxtDescription.Text & "' And V_Type <> '" & mSearchCode & "' ", AgL.GCn)
                If AgL.ECmd.ExecuteScalar() > 0 Then MsgBox("Description Already Exist!") : TxtDescription.Focus() : Exit Function

                AgL.ECmd = AgL.Dman_Execute("Select count(*) From Voucher_Type Where Short_Name='" & TxtShort_Name.Text & "'  And V_Type <> '" & mSearchCode & "'", AgL.GCn)
                If AgL.ECmd.ExecuteScalar() > 0 Then MsgBox("Short Name Already Exist!") : TxtShort_Name.Focus() : Exit Function
            End If


            Data_Validation = True
        Catch ex As Exception
            MsgBox(ex.Message)
            Data_Validation = False
        End Try
    End Function

    Private Sub DGL1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGL1.CellEnter
        If Topctrl1.Mode = "Browse" Then Exit Sub
        Dim mRowIndex As Integer, mColumnIndex As Integer

        Try
            mRowIndex = DGL1.CurrentCell.RowIndex
            mColumnIndex = DGL1.CurrentCell.ColumnIndex

            If DGL1.Item(mColumnIndex, mRowIndex).Value Is Nothing Then DGL1.Item(mColumnIndex, mRowIndex).Value = ""
            Select Case DGL1.CurrentCell.ColumnIndex
                Case Col1Prefix
                    'Call IniItemHelp(False, DGL1.AgSelectedValue(Col1BarCode, mRowIndex))
            End Select

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Dgl1_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGL1.CellValueChanged
        Dim mRowIndex As Integer, mColumnIndex As Integer
        If Topctrl1.Mode = "Browse" Then Exit Sub

        Try
            mRowIndex = DGL1.CurrentCell.RowIndex
            mColumnIndex = DGL1.CurrentCell.ColumnIndex

            Select Case DGL1.CurrentCell.ColumnIndex
                'Case <ColumnIndex>
                '<Executable Code>
            End Select
        Catch ex As Exception

        End Try
    End Sub

    Private Sub DGL1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DGL1.KeyDown, DGL2.KeyDown, DGL3.KeyDown
        If Topctrl1.Mode <> "Browse" Then
            If e.Control And e.KeyCode = Keys.D Then
                sender.CurrentRow.Selected = True
            End If
        End If
        If e.Control Or e.Shift Or e.Alt Then Exit Sub
    End Sub

    Private Sub DGL1_EditingControl_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles DGL1.EditingControl_Validating, DGL2.EditingControl_Validating, DGL3.EditingControl_Validating
        If Topctrl1.Mode = "Browse" Then Exit Sub
        Dim mRowIndex As Integer, mColumnIndex As Integer
        Try
            mRowIndex = DGL1.CurrentCell.RowIndex
            mColumnIndex = DGL1.CurrentCell.ColumnIndex

            If DGL1.Item(mColumnIndex, mRowIndex).Value Is Nothing Then DGL1.Item(mColumnIndex, mRowIndex).Value = ""
            Select Case DGL1.CurrentCell.ColumnIndex
                Case Col1Date_From
                    If DGL1.Item(Col1Date_From, mRowIndex).Value.ToString.Trim <> "" Then
                        If DGL1.Item(Col1Prefix, mRowIndex).Value Is Nothing Then DGL1.Item(Col1Prefix, mRowIndex).Value = ""
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub DGL1_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles DGL1.RowsAdded, DGL2.RowsAdded, DGL3.RowsAdded
        sender(Col_SNo, sender.Rows.Count - 1).Value = Trim(sender.Rows.Count)
    End Sub

    Private Sub DGL1_RowsRemoved(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs) Handles DGL1.RowsRemoved, DGL2.RowsRemoved, DGL3.RowsRemoved
        Try
            DTStruct.Rows.Remove(DTStruct.Rows.Item(e.RowIndex))
        Catch ex As Exception
        End Try
        AgL.FSetSNo(sender, Col_SNo)
    End Sub

    Private Sub Control_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles _
        TxtCommon_Narr.Validating, TxtChqDate.Validating, TxtChqNo.Validating, TxtClgDt.Validating, TxtMenuName.Validating

        Dim DrTemp As DataRow() = Nothing
        Try
            Select Case sender.NAME
                Case TxtMenuName.Name
                    If sender.text.ToString.Trim = "" Or sender.AgSelectedValue.Trim = "" Then
                        TxtModule.Text = ""
                        TxtModule.Tag = ""
                    Else
                        If sender.AgHelpDataSet IsNot Nothing Then
                            DrTemp = TxtMenuName.AgHelpDataSet.Tables(0).Select("Code = " & AgL.Chk_Text(sender.AgSelectedValue) & "")
                            TxtModule.Text = AgL.XNull(DrTemp(0)("MnuName"))
                            TxtModule.Tag = AgL.XNull(DrTemp(0)("Module"))
                        End If
                    End If

            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            DrTemp = Nothing
        End Try
    End Sub
End Class