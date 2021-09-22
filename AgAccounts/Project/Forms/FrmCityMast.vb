Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.SQLite
Public Class FrmCityMast
    Private DTMaster As New DataTable()
    Public BMBMaster As BindingManagerBase
    Private LIEvent As ClsEvents
    Private Sub FrmCityMast_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F2 Or e.KeyCode = Keys.F3 Or e.KeyCode = Keys.F4 Or e.KeyCode = (Keys.F And e.Control) Or e.KeyCode = (Keys.P And e.Control) _
               Or e.KeyCode = (Keys.S And e.Control) Or e.KeyCode = Keys.Escape Or e.KeyCode = Keys.F5 Or e.KeyCode = Keys.F10 _
               Or e.KeyCode = Keys.Home Or e.KeyCode = Keys.PageUp Or e.KeyCode = Keys.PageDown Or e.KeyCode = Keys.End Then
            Topctrl1.TopKey_Down(e)
        End If
    End Sub
    Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Topctrl1.FSetParent(Me, StrUPVar, DTUP)
        Topctrl1.SetDisp(True)
    End Sub
    Private Sub FrmCityMast_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            LIEvent = New ClsEvents(Me)
            Agl.WinSetting(Me, 258, 891, 0, 0)
            FIniMaster()
            MoveRec()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FIniMaster(Optional ByVal BytDel As Byte = 0, Optional ByVal BytRefresh As Byte = 1)
        Topctrl1.FIniForm(DTMaster, AgL.GCn, "Select CityCode As SearchCode, CityName as Name From City", True, txtCityName, "SearchCode", "Name", BytDel, BytRefresh)

        'AgL.IniMasterHelpList(AgL.GCn, txtState, CMain.FGetDatTable("Select State As StateCode,State From City Order By State", AgL.GCn), "StateCode", "State")
    End Sub
    Private Sub Topctrl1_tbDiscard() Handles Topctrl1.tbDiscard
        FIniMaster(0, 0)
    End Sub
    Public Sub MoveRec()

        Dim DTTemp As New DataTable

        Topctrl1.BlankTextBoxes()
        If DTMaster.Rows.Count > 0 Then
            DTTemp = AgL.FillData("Select CityCode, CityName, State, STDCode From City " &
                          " Where CityCode='" & AgL.XNull(DTMaster.Rows(BMBMaster.Position).Item("SearchCode")) & "'", AgL.GCn).Tables(0)

            If DTTemp.Rows.Count > 0 Then
                txtCityName.Text = Agl.Xnull(DTTemp.Rows(0).Item("CityName"))
                txtState.Text = Agl.Xnull(DTTemp.Rows(0).Item("State"))
                txtSTDCode.Text = Agl.Xnull(DTTemp.Rows(0).Item("STDCode"))
            End If
        End If
        Topctrl1.FSetDispRec(BMBMaster)

        DTTemp = Nothing
    End Sub

    Public Sub FTxtKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        '======== Write Your Code Below =============
    End Sub

    Public Sub FTxtKeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        '======== Write Your Code Below =============
    End Sub
    Public Sub FTxtGotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        '======== Write Your Code Below =============
    End Sub

    Private Sub Topctrl1_tbAdd() Handles Topctrl1.tbAdd
        txtCityName.Focus()
    End Sub

    Private Sub Topctrl1_tbDel() Handles Topctrl1.tbDel
        Dim BlnTrans As Boolean = False
        Dim GCnCmd As New Object

        Try
            If DTMaster.Rows.Count > 0 Then
                If MsgBox(" Delete Conflict ", MsgBoxStyle.YesNo) = vbYes Then
                    StrDocID = ""
                    StrDocID = AgL.XNull(DTMaster.Rows(BMBMaster.Position).Item("SearchCode"))
                    If Trim(StrDocID) = "" Then MsgBox(" Invalid " & " DocId.")
                    BlnTrans = True
                    GCnCmd = AgL.GCn.CreateCommand
                    GCnCmd.Transaction = AgL.GCn.BeginTransaction(IsolationLevel.Serializable)
                    GCnCmd.CommandText = "Delete From City Where CityCode='" & StrDocID & "'"
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
        If DTMaster.Rows.Count > 0 Then
            txtCityName.Focus()
        End If
    End Sub

    Private Sub Topctrl1_tbFind() Handles Topctrl1.tbFind
        If DTMaster.Rows.Count <= 0 Then MsgBox(ClsMain.MsgRecNotFnd) : Exit Sub
        Try
            AgL.PubFindQry = "Select CityCode,CityName,State,STDCode From City "
            AgL.PubFindQryOrdBy = "CityName"
            'LIPublic.CreateAndSendArr("200,150,100")
            '*************** common code start *****************
            Dim Frmbj As AgLibrary.FrmFind = New AgLibrary.FrmFind(AgL.PubFindQry, Me.Text & " Find", AgL)
            Frmbj.ShowDialog()
            AgL.PubSearchRow = Frmbj.DGL1.Item(0, Frmbj.DGL1.CurrentRow.Index).Value.ToString

            If AgL.PubSearchRow <> "" Then
                CMain.DRFound = DTMaster.Rows.Find(AgL.PubSearchRow)
                BMBMaster.Position = DTMaster.Rows.IndexOf(CMain.DRFound)
                MoveRec()
            End If
            '*************** common code end  *****************
        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try
    End Sub

    Private Sub Topctrl1_tbPrn() Handles Topctrl1.tbPrn
        Dim rptReg As New ReportDocument
        Dim GCnCmd As New Object
        Dim ds As New DataSet
        Dim ds1 As New DataSet
        Dim strQry As String

        Try
            Me.Cursor = Cursors.WaitCursor
            AgL.PubReportTitle = "City Master"

            GCnCmd.CommandText = "SELECT    CityCode, CityName FROM         City"
            If Not DTMaster.Rows.Count > 0 Then
                MsgBox(ClsMain.MsgRecNotFnd & " to Print!!!")
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

            strQry = "SELECT     CityCode, CityName, State,STDCode FROM         City Order By CityName"
            ds = AgL.FillData(strQry, AgL.GCn)


            ds.WriteXmlSchema(AgL.PubReportPath & "\Citymaster.Xml")
            rptReg.Load(AgL.PubReportPath & "\Citymaster.rpt")
            rptReg.SetDataSource(ds)
            FormulaSet(rptReg, Me)
            CMain.FShowReport(rptReg, Me.MdiParent, AgL.PubReportTitle)
            Me.Cursor = Cursors.Default
        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try

        Me.Cursor = Cursors.Default
    End Sub

    Private Sub Topctrl1_tbSave() Handles Topctrl1.tbSave
        Dim BlnTrans As Boolean = False
        Dim GCnCmd As New Object
        Dim StrName As String

        Try
            If AgL.RequiredField(txtCityName, "City Name") Then Exit Sub

            StrName = CMain.FRemoveSpace(txtCityName.Text)
            StrDocID = ""
            If Topctrl1.Mode = "Add" Then
                StrDocID = AgL.GetMaxId("City", "CityCode", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 5)
                'StrDocID = AgL.PubSiteCode + Trim(CMain.FGetMaxNoWithSiteCode("City", "CityCode", AgL.GCn))
            Else
                StrDocID = Agl.Xnull(DTMaster.Rows(BMBMaster.Position).Item("SearchCode"))
            End If
            If Trim(Replace(StrDocID, "0", "")) = "" Then MsgBox(" Invalid " & " DocId.") : Exit Sub

            If CMain.DuplicacyChecking("Select Count(CityName) As Cnt From City CT Where CT.CityName='" & StrName & "' And CT.CityCode<>'" & StrDocID & "'", "City Already Exists.") Then txtCityName.Focus() : Exit Sub

            BlnTrans = True
            GCnCmd = AgL.GCn.CreateCommand
            GCnCmd.Transaction = Agl.Gcn.BeginTransaction(IsolationLevel.Serializable)

            If Topctrl1.Mode = "Add" Then
                GCnCmd.CommandText = "Insert Into City(CityCode ,CityName,State,STDCode,U_Name,U_EntDt,U_AE)" & _
                                     " Values('" & StrDocID & "'," & AgL.Chk_Text(StrName) & "," & AgL.Chk_Text(txtState.Text) & "," & AgL.Chk_Text(txtSTDCode.Text) & _
                                     ",'" & AgL.PubUserName & "','" & Format(AgL.PubLoginDate, "Short Date") & "','A')"
            Else
                GCnCmd.CommandText = "Update City Set CityName=" & AgL.Chk_Text(StrName) & ""
                GCnCmd.CommandText = GCnCmd.CommandText + ",State=" & AgL.Chk_Text(Trim(txtState.Text)) & ""
                GCnCmd.CommandText = GCnCmd.CommandText + ",STDCode=" & AgL.Chk_Text(Trim(txtSTDCode.Text)) & ""
                GCnCmd.CommandText = GCnCmd.CommandText + ",Transfered='N' "
                GCnCmd.CommandText = GCnCmd.CommandText + ",U_Name='" & Agl.PubUserName & "'"
                GCnCmd.CommandText = GCnCmd.CommandText + ",U_EntDt='" & Format(Agl.PubLoginDate, "Short Date") & "'"
                GCnCmd.CommandText = GCnCmd.CommandText + ",U_AE='E' "
                GCnCmd.CommandText = GCnCmd.CommandText + " Where CityCode='" & StrDocID & "'"
            End If
            GCnCmd.ExecuteNonQuery()

            GCnCmd.Transaction.Commit()
            BlnTrans = False
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
    Private Sub FrmCityMast_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        Agl.FPaintForm(Me, e, Topctrl1.Height)
    End Sub
End Class