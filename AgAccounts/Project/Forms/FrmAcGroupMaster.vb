Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.SQLite

Public Class FrmAcGroupMaster
    Private DTMaster As New DataTable()
    Public BMBMaster As BindingManagerBase
    Private LIEvent As ClsEvents
    Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Topctrl1.FSetParent(Me, StrUPVar, DTUP)
        Topctrl1.SetDisp(True)
    End Sub
    Private Sub FrmGodownTransfer_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F2 Or e.KeyCode = Keys.F3 Or e.KeyCode = Keys.F4 Or e.KeyCode = (Keys.F And e.Control) Or e.KeyCode = (Keys.P And e.Control) _
        Or e.KeyCode = (Keys.S And e.Control) Or e.KeyCode = Keys.Escape Or e.KeyCode = Keys.F5 Or e.KeyCode = Keys.F10 _
        Or e.KeyCode = Keys.Home Or e.KeyCode = Keys.PageUp Or e.KeyCode = Keys.PageDown Or e.KeyCode = Keys.End Then
            Topctrl1.TopKey_Down(e)
        End If
    End Sub
    Private Sub FrmAcGroupMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            LIEvent = New ClsEvents(Me)
            Agl.WinSetting(Me, 338, 891, 0, 0)
            FIniMaster()
            MoveRec()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub FIniMaster(Optional ByVal BytDel As Byte = 0, Optional ByVal BytRefresh As Byte = 1)
        Topctrl1.FIniForm(DTMaster, Agl.Gcn, "Select GroupCode As SearchCode,GroupName As Name,SysGroup From AcGroup Order By GroupName", True, TxtName, "SearchCode", "Name", BytDel, BytRefresh)
    End Sub
    Private Sub Topctrl1_tbDiscard() Handles Topctrl1.tbDiscard
        FIniMaster(0, 0)
    End Sub
    Public Sub MoveRec()
        Dim DTTemp As New DataTable

        Topctrl1.BlankTextBoxes()
        LblSysGroup.Text = ""
        If DTMaster.Rows.Count > 0 Then
            DTTemp = AgL.FillData("Select AG.SysGroup,AG.GroupName As Name,AG.GroupUnder,AG.Nature,AG1.GroupName As GrUnderName,AG.GroupNature,AG.ContraGroupName " &
                    "From AcGroup AG Left Join AcGroup AG1 On AG.GroupUnder=AG1.GroupCode " &
                    "Where AG.GroupCode='" & AgL.XNull(DTMaster.Rows(BMBMaster.Position).Item("SearchCode")) & "'", AgL.GCn).Tables(0)

            If DTTemp.Rows.Count > 0 Then
                TxtName.Text = Agl.Xnull(DTTemp.Rows(0).Item("Name"))
                TxtContraGroupName.Text = Agl.Xnull(DTTemp.Rows(0).Item("ContraGroupName"))
                txtGroupUnder.Text = Agl.Xnull(DTTemp.Rows(0).Item("GrUnderName"))
                txtGroupUnder.Tag = Agl.Xnull(DTTemp.Rows(0).Item("GroupUnder"))
                txtNature.Text = Agl.Xnull(DTTemp.Rows(0).Item("Nature"))
                txtNature.Tag = Agl.Xnull(DTTemp.Rows(0).Item("Nature"))
                TxtMainGroup.Tag = Agl.Xnull(DTTemp.Rows(0).Item("GroupNature"))
                TxtMainGroup.Text = IIf(UCase(Agl.Xnull(DTTemp.Rows(0).Item("GroupNature"))) = "A", "Assets", _
                                    IIf(UCase(Agl.Xnull(DTTemp.Rows(0).Item("GroupNature"))) = "L", "Liabilities", _
                                    IIf(UCase(Agl.Xnull(DTTemp.Rows(0).Item("GroupNature"))) = "E", "Expenses", _
                                    IIf(UCase(Agl.Xnull(DTTemp.Rows(0).Item("GroupNature"))) = "R", "Revenue", ""))))
                LblSysGroup.Text = IIf(UCase(Agl.Xnull(DTTemp.Rows(0).Item("SysGroup"))) = "N", "User Define", "System Define")
            End If
        End If

        Topctrl1.FSetDispRec(BMBMaster)

        DTTemp = Nothing
    End Sub
    Public Sub FTxtGotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        '======== Write Your Code Below =============
    End Sub
    Public Sub FTxtKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        '======== Write Your Code Below =============
        Select Case sender.Name
            Case txtGroupUnder.Name, txtNature.Name, TxtMainGroup.Name
                If e.KeyCode = Keys.Delete Then
                    sender.Text = "" : sender.Tag = ""
                    If sender.Name = txtGroupUnder.Name Then TxtMainGroup.Enabled = True
                End If
        End Select
    End Sub
    Public Sub FTxtKeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        '======== Write Your Code Below =============
        Select Case sender.Name
            Case txtGroupUnder.Name
                FHP_GroupUnder(e, sender)
            Case TxtMainGroup.Name
                FHP_MainNature(e, sender)
            Case txtNature.Name
                FHP_Nature(e, sender)
        End Select
    End Sub

    Private Sub FHP_GroupUnder(ByRef e As System.Windows.Forms.KeyPressEventArgs, ByVal Txt As TextBox)
        Dim DTMain As New DataTable
        Dim FRH As DMHelpGrid.FrmHelpGrid
        Dim StrSendText As String
        Dim StrTempCode As String = ""

        If Topctrl1.Mode <> "Add" Then
            StrTempCode = AgL.XNull(DTMaster.Rows(BMBMaster.Position).Item("SearchCode"))
        End If
        StrSendText = CMain.FSendText(Txt, e.KeyChar)
        DTMain = AgL.FillData("Select GroupCode,GroupName,(Case GroupNature When 'A' Then 'Assets' When 'L' Then 'Liabilities' When 'E' Then 'Expenses' When 'R' Then 'Revenue' End) As GroupNature,Nature From AcGroup Where GroupCode<>'" & StrTempCode & "' And GroupCode Not In (Select GroupCode From AcGroupPath Where GroupUnder='" & StrTempCode & "') Order by GroupName", AgL.GCn).Tables(0)

        FRH = New DMHelpGrid.FrmHelpGrid(New DataView(DTMain), StrSendText, 300, 480, (Top + Txt.Top) + 85, Left + Txt.Left + 3)
        FRH.FFormatColumn(0, , 0, , False)
        FRH.FFormatColumn(1, "A/c Group", 200, DataGridViewContentAlignment.MiddleLeft)
        FRH.FFormatColumn(2, "Main Group", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH.FFormatColumn(3, "Nature", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH.ShowDialog()

        If FRH.BytBtnValue = 0 Then
            If Not FRH.DRReturn.Equals(Nothing) Then
                Txt.Text = FRH.DRReturn.Item(1)
                Txt.Tag = FRH.DRReturn.Item(0)
                TxtMainGroup.Text = FRH.DRReturn.Item(2)
                TxtMainGroup.Tag = Mid(FRH.DRReturn.Item(2), 1, 1)
                txtNature.Text = FRH.DRReturn.Item(3)
                txtNature.Tag = FRH.DRReturn.Item(3)
                TxtMainGroup.Enabled = False
            End If
        End If
        FRH = Nothing
        e.KeyChar = ""
    End Sub
    Private Sub FHP_MainNature(ByRef e As System.Windows.Forms.KeyPressEventArgs, ByVal Txt As TextBox)
        Dim DTMain As New DataTable

        Dim FRH As DMHelpGrid.FrmHelpGrid
        Dim StrSendText As String
        Dim StrSQL As String

        StrSQL = "Declare @TmpTable Table (Code Varchar(1),Name Varchar(15)) "
        StrSQL += "Insert Into @TmpTable Values('L','Liabilities') "
        StrSQL += "Insert Into @TmpTable Values('A','Assets') "
        StrSQL += "Insert Into @TmpTable Values('R','Expenses') "
        StrSQL += "Insert Into @TmpTable Values('E','Revenue') "
        StrSQL += "Select Code,Name From @TmpTable Order By Name"
        StrSendText = Cmain.FSendText(Txt, e.KeyChar)
        DTMain = AgL.FillData(StrSQL, AgL.GCn).Tables(0)

        FRH = New DMHelpGrid.FrmHelpGrid(New DataView(DTMain), StrSendText, 300, 200, (Top + Txt.Top) + 85, Left + Txt.Left + 3)
        FRH.FFormatColumn(0, , 0, , False)
        FRH.FFormatColumn(1, "Main Group", 125, DataGridViewContentAlignment.MiddleLeft)
        FRH.ShowDialog()

        If FRH.BytBtnValue = 0 Then
            If Not FRH.DRReturn.Equals(Nothing) Then
                Txt.Text = FRH.DRReturn.Item(1)
                Txt.Tag = FRH.DRReturn.Item(0)
            End If
        End If
        FRH = Nothing
        e.KeyChar = ""
    End Sub
    Private Sub FHP_Nature(ByRef e As System.Windows.Forms.KeyPressEventArgs, ByVal Txt As TextBox)
        Dim DTMain As New DataTable

        Dim FRH As DMHelpGrid.FrmHelpGrid
        Dim StrSendText As String

        StrSendText = Cmain.FSendText(Txt, e.KeyChar)
        DTMain = AgL.FillData("Select Nature,Nature From Nature Order By Nature", AgL.GCn.ConnectionString).Tables(0)
        FRH = New DMHelpGrid.FrmHelpGrid(New DataView(DTMain), StrSendText, 300, 200, (Top + Txt.Top) + 85, Left + Txt.Left + 3)
        FRH.FFormatColumn(0, , 0, , False)
        FRH.FFormatColumn(1, "Nature", 125, DataGridViewContentAlignment.MiddleLeft)
        FRH.ShowDialog()

        If FRH.BytBtnValue = 0 Then
            If Not FRH.DRReturn.Equals(Nothing) Then
                Txt.Text = FRH.DRReturn.Item(1)
                Txt.Tag = FRH.DRReturn.Item(0)
            End If
        End If
        FRH = Nothing
        e.KeyChar = ""
    End Sub
    Private Sub Topctrl1_tbAdd() Handles Topctrl1.tbAdd
        TxtMainGroup.Enabled = False
        LblSysGroup.Text = "User Define"
        TxtMainGroup.Enabled = True
        TxtName.Focus()
    End Sub
    Private Sub Topctrl1_tbDel() Handles Topctrl1.tbDel
        Dim BlnTrans As Boolean = False
        Dim GCnCmd As New Object
        Dim DTTemp As DataTable

        Try
            If DTMaster.Rows.Count > 0 Then
                If UCase(Agl.Xnull(DTMaster.Rows(BMBMaster.Position).Item("SysGroup"))) = "Y" Then
                    MsgBox("System Defined")
                    Topctrl1.FButtonClick(99)
                    Exit Sub
                End If

                DTTemp = cmain.FGetDatTable("Select GroupCode From AcGroupPath Where GroupUnder='" & Agl.Xnull(DTMaster.Rows(BMBMaster.Position).Item("SearchCode")) & "' Order By SNo", Agl.Gcn)
                If DTTemp.Rows.Count > 0 Then
                    MsgBox("Corresponding Records Exist")
                    Topctrl1.FButtonClick(99)
                    Exit Sub
                End If

                If MsgBox(" Delete Conflict ", MsgBoxStyle.YesNo) = vbYes Then

                    StrDocID = ""
                    StrDocID = AgL.XNull(DTMaster.Rows(BMBMaster.Position).Item("SearchCode"))
                    If Trim(Replace(StrDocID, "0", "")) = "" Then MsgBox(" Invalid " & "DocId.") : Exit Sub

                    BlnTrans = True
                    GCnCmd = AgL.GCn.CreateCommand
                    GCnCmd.Transaction = Agl.Gcn.BeginTransaction(IsolationLevel.Serializable)

                    GCnCmd.CommandText = "Delete From AcGroupPath Where GroupCode='" & (StrDocID) & "'"
                    GCnCmd.ExecuteNonQuery()

                    GCnCmd.CommandText = "Delete From AcGroup Where GroupCode='" & (StrDocID) & "'"
                    GCnCmd.ExecuteNonQuery()

                    GCnCmd.Transaction.Commit()
                    BlnTrans = False
                    FIniMaster(1)
                    MoveRec()
                End If
            End If
        Catch Ex As Exception
            If BlnTrans = True Then GCnCmd.Transaction.Rollback()
            If Err.Number = 5 Then    'foreign key - there exists related record in primary key table
                MsgBox("Corresponding Records Exist")
            Else
                MsgBox(Ex.Message)
            End If
        End Try
    End Sub

    Private Sub Topctrl1_tbEdit() Handles Topctrl1.tbEdit
        TxtMainGroup.Enabled = False
        If DTMaster.Rows.Count > 0 Then
            If UCase(Agl.Xnull(DTMaster.Rows(BMBMaster.Position).Item("SysGroup"))) = "Y" Then
                TxtName.Enabled = False
                txtGroupUnder.Enabled = False
                TxtContraGroupName.Focus()
            Else
                If Trim(txtGroupUnder.Text) = "" Then TxtMainGroup.Enabled = True
                TxtName.Focus()
            End If
        End If
    End Sub
    Private Sub Topctrl1_tbFind() Handles Topctrl1.tbFind
        If DTMaster.Rows.Count <= 0 Then MsgBox(ClsMain.MsgRecNotFnd) : Exit Sub
        Try
            agl.PubFindQry = "Select AG.GroupCode,AG.GroupName As Name,AG.ContraGroupName,AG1.GroupName As GroupUnder,(Case AG.GroupNature When 'A' Then 'Assets' When 'L' Then 'Liabilities' When 'E' Then 'Expenses' When 'R' Then 'Revenue' End) As MainGroup,AG.Nature,AG.SysGroup " & _
                         "From AcGroup AG Left Join AcGroup AG1 On AG.GroupUnder=AG1.GroupCode "
            agl.PubFindQryOrdBy = "Name"
            'LIPublic.CreateAndSendArr("200,200,100,100,100,80")
            '*************** common code start *****************


            Dim Frmbj As AgLibrary.FrmFind = New AgLibrary.FrmFind(AgL.PubFindQry, Me.Text & " Find", AgL)
            Frmbj.ShowDialog()
            AgL.PubSearchRow = Frmbj.DGL1.Item(0, Frmbj.DGL1.CurrentRow.Index).Value.ToString
            If agl.PubSearchRow <> "" Then
                CMain.DRFound = DTMaster.Rows.Find(AgL.PubSearchRow)

                BMBMaster.Position = DTMaster.Rows.IndexOf(CMain.DRFound)
                MoveRec()
            End If
            '*************** common code end  *****************
        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try
    End Sub

    Private Sub Topctrl1_tbSave() Handles Topctrl1.tbSave
        Dim BlnTrans As Boolean = False
        Dim GCnCmd As New Object
        Dim I As Integer, J As Integer, DTTemp As DataTable, DTTemp1 As DataTable, DTTemp2 As DataTable
        Dim IntSNo As Integer
        Dim StrName As String

        Try
            If AgL.RequiredField(TxtName, "A/c Group Name") Then Exit Sub
            If AgL.RequiredField(TxtContraGroupName, "Contra Group Name") Then Exit Sub
            If AgL.RequiredField(txtNature, "Nature") Then Exit Sub

            StrName = CMain.FRemoveSpace(TxtName.Text)
            StrDocID = ""
            If Topctrl1.Mode = "Add" Then
                'StrDocID = AgL.PubSiteCode + Trim(CMain.FGetMaxNoWithSiteCode("AcGroup", "GroupCode", AgL.GCn))
                StrDocID = AgL.GetMaxId("AcGroup", "GroupCode", AgL.GCn, "", "", 4, True)
            Else
                StrDocID = Agl.Xnull(DTMaster.Rows(BMBMaster.Position).Item("SearchCode"))
            End If
            If Trim(Replace(StrDocID, "0", "")) = "" Then MsgBox(" Invalid " & "DocId.") : Exit Sub
            If CMain.DuplicacyChecking("Select Count(GroupName) As Cnt From AcGroup Where GroupName='" & StrName & "' And GroupCode<>'" & (StrDocID) & "'", "A/c Group Name Already Exists.") Then TxtName.Focus() : Exit Sub

            '============= Fetching Group Path Of Particular Group Under For Later Use ==========
            DTTemp = cmain.FGetDatTable("Select GroupCode,SNo,GroupUnder From AcGroupPath Where GroupCode='" & txtGroupUnder.Tag & "' Order By SNo", Agl.Gcn)
            '==============================================================================

            BlnTrans = True
            GCnCmd = AgL.GCn.CreateCommand
            GCnCmd.Transaction = Agl.Gcn.BeginTransaction(IsolationLevel.Serializable)

            GCnCmd.CommandText = "Delete From AcGroupPath Where GroupCode='" & (StrDocID) & "'"
            GCnCmd.ExecuteNonQuery()

            If Topctrl1.Mode = "Add" Then
                GCnCmd.CommandText = "Insert Into AcGroup(GroupCode,GroupName,GroupUnder, " &
                                     "Nature,U_Name,SysGroup," &
                                     "U_EntDt,U_AE,GroupNature,ContraGroupName) Values " &
                                     "('" & (StrDocID) & "','" & AgL.Chk_Quot(StrName) & "'," & AgL.Chk_Text(txtGroupUnder.Tag) & ", " &
                                     "" & AgL.Chk_Text(txtNature.Tag) & ",'" & AgL.PubUserName & "','N'," &
                                     "" & AgL.Chk_Date(AgL.PubLoginDate) & ",'" & Microsoft.VisualBasic.Left(Topctrl1.Mode, 1) & "'," & AgL.Chk_Text(TxtMainGroup.Tag) & "," & AgL.Chk_Text(TxtContraGroupName.Text) & ")"
            Else
                GCnCmd.CommandText = "Update AcGroup Set "
                GCnCmd.CommandText = GCnCmd.CommandText + "GroupName=" & AgL.Chk_Text(StrName) & ", "
                GCnCmd.CommandText = GCnCmd.CommandText + "ContraGroupName=" & AgL.Chk_Text(TxtContraGroupName.Text) & ", "
                GCnCmd.CommandText = GCnCmd.CommandText + "GroupUnder=" & AgL.Chk_Text(txtGroupUnder.Tag) & ", "
                GCnCmd.CommandText = GCnCmd.CommandText + "GroupNature=" & AgL.Chk_Text(TxtMainGroup.Tag) & ", "
                GCnCmd.CommandText = GCnCmd.CommandText + "Nature=" & AgL.Chk_Text(txtNature.Tag) & ", "
                GCnCmd.CommandText = GCnCmd.CommandText + "Transfered='N', "
                GCnCmd.CommandText = GCnCmd.CommandText + "U_Name='" & AgL.PubUserName & "', "
                GCnCmd.CommandText = GCnCmd.CommandText + "U_EntDt=" & AgL.Chk_Date(AgL.PubLoginDate) & ", "
                GCnCmd.CommandText = GCnCmd.CommandText + "U_AE='" & Microsoft.VisualBasic.Left(Topctrl1.Mode, 1) & "' "

                GCnCmd.CommandText = GCnCmd.CommandText + "Where GroupCode='" & (StrDocID) & "' "
            End If
            GCnCmd.ExecuteNonQuery()

            IntSNo = 0
            If Trim(txtGroupUnder.Text) <> "" Then
                '======== Inserting Path For Particular Group Under Which Is Already Fetched Above ====
                For I = 0 To DTTemp.Rows.Count - 1
                    IntSNo = IntSNo + 1
                    GCnCmd.CommandText = "Insert Into AcGroupPath(GroupCode,SNo,GroupUnder) " & _
                                                 "Values " & _
                                                 "('" & (StrDocID) & "'," & IntSNo & ",'" & AgL.XNull(DTTemp.Rows(I).Item("GroupUnder")) & "') "
                    GCnCmd.ExecuteNonQuery()

                Next

                '======== Inserting Path For Particular Group Under ====
                IntSNo = IntSNo + 1
                GCnCmd.CommandText = "Insert Into AcGroupPath(GroupCode,SNo,GroupUnder) " & _
                                                 "Values " & _
                                                 "('" & (StrDocID) & "'," & IntSNo & ",'" & AgL.Chk_Quot(txtGroupUnder.Tag) & "') "
                GCnCmd.ExecuteNonQuery()
            End If

            '==========================================================================
            '========== Updating Nature In SubGroup Same As Group Nature ==============
            '==========================================================================
            GCnCmd.CommandText = "Update SubGroup Set Nature='" & txtNature.Text & "' Where GroupCode='" & StrDocID & "' "
            GCnCmd.ExecuteNonQuery()
            '==========================================================================

            GCnCmd.Transaction.Commit()
            BlnTrans = False

            '============================================
            '========= Updating Child Records Also ======
            '================ Begin =====================
            '============================================
            If Trim(txtGroupUnder.Text) <> "" Then
                '============= Fetching Group Path Of Particular Group For Later Use ==========
                DTTemp = cmain.FGetDatTable("Select GroupCode,SNo,GroupUnder From AcGroupPath Where GroupCode='" & StrDocID & "' Order By SNo", Agl.Gcn)
                '==============================================================================

                '============= Fetching All Child Groups Of Particular Record =================
                DTTemp1 = cmain.FGetDatTable("Select GroupCode,SNo,GroupUnder From AcGroupPath Where GroupUnder='" & StrDocID & "' Order By SNo", Agl.Gcn)
                '==============================================================================

                For J = 0 To DTTemp1.Rows.Count - 1
                    '============= Fetching Group Path Whose Level Is Greater Than Or Equal To ========
                    '============= Particular Group For Later Use =====================================
                    DTTemp2 = cmain.FGetDatTable("Select GroupCode,SNo,GroupUnder From AcGroupPath Where GroupCode='" & Agl.Xnull(DTTemp1.Rows(J).Item("GroupCode")) & "' And SNo>=" & Agl.VNull(DTTemp1.Rows(J).Item("SNo")) & " Order By SNo", Agl.Gcn)
                    '==================================================================================

                    BlnTrans = True
                    GCnCmd = AgL.GCn.CreateCommand
                    GCnCmd.Transaction = Agl.Gcn.BeginTransaction(IsolationLevel.Serializable)

                    '============= Deleting Particular Group Path =====================================
                    GCnCmd.CommandText = "Delete From AcGroupPath Where GroupCode='" & Agl.Xnull(DTTemp1.Rows(J).Item("GroupCode")) & "'"
                    GCnCmd.ExecuteNonQuery()

                    '======= Updating Group Path Of Child Record =======
                    IntSNo = 0
                    For I = 0 To DTTemp.Rows.Count - 1
                        IntSNo = IntSNo + 1
                        GCnCmd.CommandText = "Insert Into AcGroupPath(GroupCode,SNo,GroupUnder) " & _
                                                     "Values " & _
                                                     "('" & Agl.Xnull(DTTemp1.Rows(J).Item("GroupCode")) & "'," & IntSNo & ",'" & Agl.Xnull(DTTemp.Rows(I).Item("GroupUnder")) & "') "
                        GCnCmd.ExecuteNonQuery()
                    Next
                    For I = 0 To DTTemp2.Rows.Count - 1
                        IntSNo = IntSNo + 1
                        GCnCmd.CommandText = "Insert Into AcGroupPath(GroupCode,SNo,GroupUnder) " & _
                                                     "Values " & _
                                                     "('" & Agl.Xnull(DTTemp1.Rows(J).Item("GroupCode")) & "'," & IntSNo & ",'" & Agl.Xnull(DTTemp2.Rows(I).Item("GroupUnder")) & "') "
                        GCnCmd.ExecuteNonQuery()
                    Next
                    '====================================================

                    '============= Updating Child Record ================
                    GCnCmd.CommandText = "Update AcGroup Set GroupNature='" & TxtMainGroup.Tag & "' " & _
                                         "Where GroupCode='" & Agl.Xnull(DTTemp1.Rows(J).Item("GroupCode")) & "' "
                    GCnCmd.ExecuteNonQuery()
                    '====================================================

                    GCnCmd.Transaction.Commit()
                    BlnTrans = False
                Next
            End If
            '============================================
            '========= Updating Child Records Also ======
            '================ End =====================
            '============================================

            If Topctrl1.Mode = "Add" Then
                Topctrl1.LblDocId.Text = StrDocID
                Topctrl1.FButtonClick(0)
                Exit Sub
            Else
                Topctrl1.SetDisp(True)
                MoveRec()
            End If

        Catch Ex As Exception
            If BlnTrans = True Then GCnCmd.Transaction.Rollback()
            MsgBox(Ex.Message)
        End Try
    End Sub
    Private Sub FrmAcGroupMaster_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub
    Private Sub Topctrl1_tbPrn() Handles Topctrl1.tbPrn
        Dim rptReg As New ReportDocument
        Dim GCnCmd As New Object
        Dim ds As New DataSet
        Dim ds1 As New DataSet
        Dim strQry As String

        Try
            Me.Cursor = Cursors.WaitCursor
            AgL.PubReportTitle = "Account Group Master"

            If Not DTMaster.Rows.Count > 0 Then
                MsgBox(ClsMain.MsgRecNotFnd & " to Print!!!")
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

            strQry = "Select AG.GroupName As Name,AG.Nature,"
            strQry = strQry + "AG1.GroupName As GrUnderName,"
            strQry = strQry + "CASE AG.GroupNature WHEN 'L'"
            strQry = strQry + "THEN 'Liabilities' "
            strQry = strQry + "WHEN 'A'"
            strQry = strQry + "THEN 'Assets'"
            strQry = strQry + "WHEN 'R'"
            strQry = strQry + "THEN 'Revenue'"
            strQry = strQry + "WHEN 'E'"
            strQry = strQry + "THEN 'Expenses'"
            strQry = strQry + "END AS GrpNat,"
            strQry = strQry + "CASE AG.SysGroup WHEN 'Y' "
            strQry = strQry + "THEN 'System Define'"
            strQry = strQry + "ELSE"
            strQry = strQry + "'User Define'"
            strQry = strQry + "END  AS Type,"
            strQry = strQry + "AG.ContraGroupName From AcGroup AG "
            strQry = strQry + "Left Join AcGroup AG1 On AG.GroupUnder=AG1.GroupCode Order By AG.GroupName"

            ds = AgL.FillData(strQry, AgL.GCn)


            ds.WriteXmlSchema(AgL.PubReportPath & "\AcGrpMast.Xml")
            rptReg.Load(AgL.PubReportPath & "\AcGrpMast.rpt")
            rptReg.SetDataSource(ds)
            FormulaSet(rptReg, Me)
            CMain.FShowReport(rptReg, Me.MdiParent, Me.Name)
            Me.Cursor = Cursors.Default
        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try
        Me.Cursor = Cursors.Default
    End Sub
    Private Sub BtnPositioning_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPositioning.Click
        Dim FrmObj As FrmAcGroupPositioning

        FrmObj = New FrmAcGroupPositioning()
        FrmObj.MdiParent = Me.MdiParent
        FrmObj.Show()
    End Sub
End Class
