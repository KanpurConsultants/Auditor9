Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.SQLite
Public Class frmTdsCatMaster
    Private DTMaster As New DataTable()
    Public BMBMaster As BindingManagerBase
    Private LIEvent As ClsEvents
    Private WithEvents FGMain As New AgControls.AgDataGrid
    Private Const colSlNo As Byte = 0
    Private Const coldesc As Byte = 1
    Private Const coldesccode As Byte = 2
    Private Const colacname As Byte = 3
    Private Const colper As Byte = 4
    Private Const ColAcCode As Byte = 5
    Private Const ColFormula As Byte = 6
    Private Sub frmTdsCatMaster_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
    Private Sub frmTdsCatMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            LIEvent = New ClsEvents(Me)
            Agl.WinSetting(Me, 488, 891, 0, 0)
            Agl.GridDesign(FGMain)
            IniGrid()
            FIniMaster()
            MoveRec()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub IniGrid()
        FGMain.Height = PnlMain.Height
        FGMain.Width = PnlMain.Width
        FGMain.Top = PnlMain.Top
        FGMain.Left = PnlMain.Left
        Controls.Add(FGMain)
        FGMain.Visible = True
        FGMain.BringToFront()
        AgCl.AddAgTextColumn(FGMain, "SrNo", 50, 5, "S.No.", True, True, False)
        AgCl.AddAgTextColumn(FGMain, "TDSDesc", 220, 35, "Description", True, True, False)
        AgCl.AddAgTextColumn(FGMain, "TDSDescCode", 50, 0, "Code", True, False, False)
        AgCl.AddAgTextColumn(FGMain, "accode", 250, 0, "Posting A/c", True, True, False)
        AgCl.AddAgTextColumn(FGMain, "Percentage", 60, 6, "%", True, False, False)
        AgCl.AddAgTextColumn(FGMain, "AcCode", 0, 6, "AcCode", False, True, False)
        AgCl.AddAgTextColumn(FGMain, "Calculate On", 150, 0, "Calculate On", True, False, False)
        FGMain.Anchor = (AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Bottom)
        Agl.FSetSNo(FGMain, colSlNo)
        FGMain.TabIndex = PnlMain.TabIndex
        FGMain.Font = New Font("Arial", 11.25)
    End Sub
    Private Sub FIniMaster(Optional ByVal BytDel As Byte = 0, Optional ByVal BytRefresh As Byte = 1)
        Topctrl1.FIniForm(DTMaster, Agl.Gcn, "Select Code As SearchCode,Name From tdscat", True, txttdscat, "SearchCode", "Name", BytDel, BytRefresh)
    End Sub
    Private Sub Topctrl1_tbDiscard() Handles Topctrl1.tbDiscard
        FIniMaster(0, 0)
    End Sub
    Public Sub MoveRec()

        Dim DTTemp As New DataTable
        Dim I As Integer
        Dim sql1 As String
        FGMain.Rows.Clear()
        Topctrl1.BlankTextBoxes()
        If DTMaster.Rows.Count > 0 Then
            sql1 = "SELECT  NAME AS NAME FROM  TDSCat Where Code='" & Agl.Xnull(DTMaster.Rows(BMBMaster.Position).Item("SearchCode")) & "'"
            DTTemp = AgL.FillData(sql1, AgL.GCn).Tables(0)

            If DTTemp.Rows.Count > 0 Then
                txttdscat.Text = Agl.Xnull(DTTemp.Rows(0).Item("Name"))

                DTTemp.Clear()
                sql1 = ""
                sql1 = "Select  TD.FormulaString,TD.CODE As Code,T.Code,TD.SrNo,SG.Name As AcName,TD.Percentage,TD.TDSDesc,TCD.Name As Desc_Name,TD.AcCode "
                sql1 = sql1 + "From  TDSCat_Det As TD Left Join "
                sql1 = sql1 + "SubGroup As SG On SG.SubCode=TD.AcCode Left Join "
                sql1 = sql1 + "TDSCat As T On T.Code=TD.Code Left Join "
                sql1 = sql1 + "TDSCat_Description AS TCD ON TCD.Code =TD.TDSDesc "
                sql1 = sql1 + "Where T.Code='" & Agl.Xnull(DTMaster.Rows(BMBMaster.Position).Item("SearchCode")) & "'"
                DTTemp = AgL.FillData(sql1, AgL.GCn).Tables(0)

                If DTTemp.Rows.Count > 0 Then
                    FGMain.Rows.Add(DTTemp.Rows.Count)
                End If
                For I = 0 To DTTemp.Rows.Count - 1
                    FGMain.Item(colSlNo, I).Value = I + 1
                    FGMain.Item(coldesc, I).Value = Agl.Xnull(DTTemp.Rows(I).Item("Desc_Name"))
                    FGMain.Item(coldesccode, I).Value = Agl.Xnull(DTTemp.Rows(I).Item("TDSDesc"))
                    FGMain.Item(ColAcCode, I).Value = Agl.Xnull(DTTemp.Rows(I).Item("AcCode"))
                    FGMain.Item(colacname, I).Value = Agl.Xnull(DTTemp.Rows(I).Item("AcName"))
                    FGMain.Item(colper, I).Value = Agl.Xnull(DTTemp.Rows(I).Item("Percentage"))
                    FGMain.Item(ColFormula, I).Value = Agl.Xnull(DTTemp.Rows(I).Item("FormulaString"))
                Next

            End If
        End If
        Topctrl1.FSetDispRec(BMBMaster)


    End Sub
    Public Sub FTxtGotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        '======== Write Your Code Below =============
    End Sub
    Public Sub FTxtKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        '======== Write Your Code Below =============
    End Sub
    Public Sub FTxtKeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        '======== Write Your Code Below =============
    End Sub
    Private Sub Topctrl1_tbAdd() Handles Topctrl1.tbAdd
        txttdscat.Focus()
        FGMain.Rows.Clear()
    End Sub

    Private Sub Topctrl1_tbDel() Handles Topctrl1.tbDel
        Dim BlnTrans As Boolean = False
        Dim GCnCmd As New Object

        Try
            If DTMaster.Rows.Count > 0 Then
                If MsgBox(" Delete Conflict ", MsgBoxStyle.YesNo) = vbYes Then
                    StrDocID = ""
                    StrDocID = AgL.XNull(DTMaster.Rows(BMBMaster.Position).Item("SearchCode"))
                    If Trim(StrDocID) = "" Then MsgBox(" Invalid " & "DocId.")

                    BlnTrans = True
                    GCnCmd = AgL.GCn.CreateCommand
                    GCnCmd.Transaction = AgL.GCn.BeginTransaction(IsolationLevel.Serializable)
                    GCnCmd.CommandText = "Delete From tdscat Where code='" & StrDocID & "'"
                    GCnCmd.ExecuteNonQuery()
                    GCnCmd.CommandText = "Delete From TDSCat_Det Where Code='" & StrDocID & "'"
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
            txttdscat.Enabled = True
            FGMain.Focus()
            FGMain.CurrentCell = FGMain.Item(coldesc, 0)
            FGMain.CurrentCell.Selected = True
        End If
    End Sub

    Private Sub Topctrl1_tbFind() Handles Topctrl1.tbFind
        If DTMaster.Rows.Count <= 0 Then MsgBox(ClsMain.MsgRecNotFnd) : Exit Sub
        Try
            AgL.PubFindQry = "Select Code, Name From tdscat"
            AgL.PubFindQryOrdBy = "Name"
            'LIPublic.CreateAndSendArr("300,100")
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
    Private Sub Topctrl1_tbSave() Handles Topctrl1.tbSave
        Dim BlnTrans As Boolean = False
        Dim GCnCmd As New Object
        Dim I As Integer
        Dim StrName As String

        Try
            If AgL.RequiredField(txttdscat, "Name") Then Exit Sub
            If Not FCkhGrid() Then Exit Sub

            StrName = CMain.FRemoveSpace(txttdscat.Text)
            StrDocID = ""
            If Topctrl1.Mode = "Add" Then
                'StrDocID = agl.PubSiteCode + Trim(CMain.FGetMaxNoWithSiteCode("tdscat", "Code", Agl.Gcn))
                StrDocID = AgL.GetMaxId("tdsCat", "Code", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 5)
            Else
                StrDocID = Agl.Xnull(DTMaster.Rows(BMBMaster.Position).Item("SearchCode"))
            End If
            If Trim(Replace(StrDocID, "0", "")) = "" Then MsgBox(" Invalid " & "DocId.") : Exit Sub
            If CMain.DuplicacyChecking("Select Count(Name) As Cnt From TDSCAT TD Where TD.Name='" & StrName & "' And TD.Code<>'" & (StrDocID) & "'", "Item Code Already Exists.") Then txttdscat.Focus() : Exit Sub

            BlnTrans = True
            GCnCmd = AgL.GCn.CreateCommand
            GCnCmd.Transaction = Agl.Gcn.BeginTransaction(IsolationLevel.Serializable)
            GCnCmd.CommandText = "Delete From tdscat_DET where Code='" & StrDocID & "'"
            GCnCmd.ExecuteNonQuery()
            If Topctrl1.Mode = "Add" Then
                GCnCmd.CommandText = "Insert Into tdscat(code,name," & _
                                     "U_Name,U_EntDt,U_AE)" & _
                                     " Values('" & StrDocID & "'," & AgL.Chk_Text(StrName) & "," & _
                                     " '" & AgL.PubUserName & "','" & Format(AgL.PubLoginDate, "Short Date") & "','A')"

                GCnCmd.ExecuteNonQuery()
            Else
                GCnCmd.CommandText = "Update tdscat Set Name=" & Trim(AgL.Chk_Text(StrName)) & ""
                GCnCmd.CommandText = GCnCmd.CommandText + ",Transfered='N' "
                GCnCmd.CommandText = GCnCmd.CommandText + ",U_Name='" & Agl.PubUserName & "' "
                GCnCmd.CommandText = GCnCmd.CommandText + ",U_EntDt='" & Format(Agl.PubLoginDate, "Short Date") & "'"
                GCnCmd.CommandText = GCnCmd.CommandText + ",U_AE='E' "
                GCnCmd.CommandText = GCnCmd.CommandText + "Where code='" & StrDocID & "'"
                GCnCmd.ExecuteNonQuery()
            End If
            For I = 0 To FGMain.RowCount - 1
                If Not FGMain.Item(coldesc, I).Value = "" Then
                    GCnCmd.CommandText = "Insert Into TDSCat_Det(code,SrNo,TDSDesc,AcCode,Percentage,U_Name,U_EntDt,U_AE,FormulaString) " & _
                                         " Values('" & StrDocID & "',' " & FGMain.Item(colSlNo, I).Value & " ', " & _
                                          " " & AgL.Chk_Text(FGMain.Item(coldesccode, I).Value) & ",'" & FGMain.Item(ColAcCode, I).Value & "','" & FGMain.Item(colper, I).Value & "', " & _
                                          " '" & AgL.PubUserName & "'," & _
                                          "'" & Format(AgL.PubLoginDate, "short date") & "','A'," & Trim(AgL.Chk_Text(FGMain.Item(ColFormula, I).Value)) & ")"
                    GCnCmd.ExecuteNonQuery()
                End If
            Next

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



    Private Sub FGMain_CellBeginEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles FGMain.CellBeginEdit
        If UCase(Topctrl1.Mode) = "BROWSE" Then e.Cancel = True
    End Sub

    Private Sub FGMain_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles FGMain.EditingControlShowing
        If TypeOf e.Control Is AgControls.AgTextBox Then

            RemoveHandler DirectCast(e.Control, AgControls.AgTextBox).KeyPress, AddressOf _
                                GrdNumPress
            AddHandler DirectCast(e.Control, AgControls.AgTextBox).KeyPress, AddressOf _
                                GrdNumPress
        End If
    End Sub



    Private Sub FGMain_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FGMain.KeyDown
        If UCase(Topctrl1.Mode) = "BROWSE" Then Exit Sub
        If e.Control And e.KeyCode = Keys.D Then
            FGMain.CurrentRow.Selected = True
        End If
        If e.Control Or e.Shift Or e.Alt Then Exit Sub
        Try
            Select Case FGMain.CurrentCell.ColumnIndex
                Case colacname
                    FHPGD_ACCOUNT(e)
                Case coldesc
                    FHPGD_Description(e)
            End Select
        Catch Ex As NullReferenceException
        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try
    End Sub

    Private Sub FGMain_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles FGMain.RowsAdded
        FGMain(colSlNo, FGMain.Rows.Count - 1).Value = Trim(FGMain.Rows.Count)
    End Sub

    Private Sub FGMain_RowsRemoved(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs) Handles FGMain.RowsRemoved
        Agl.FSetSNo(FGMain, colSlNo)
    End Sub

    Private Sub chkActive_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If Topctrl1.Mode = "Browse" Then Exit Sub
    End Sub

    Private Sub frmAgentMast_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        Agl.FPaintForm(Me, e, Topctrl1.Height)
    End Sub
    Private Function FCkhGrid() As Boolean
        Dim I As Integer, J As Integer
        Dim BlnRtn As Boolean

        BlnRtn = True
        For I = 0 To FGMain.Rows.Count - 1
            If Trim(FGMain(coldesc, I).Value) <> "" Then

                If Trim(Agl.Xnull(FGMain(colacname, I).Value)) = "" Then
                    MsgBox("Please Define in Enviro" & "Posting A/c.")
                    FGMain(colacname, I).Selected = True
                    BlnRtn = False
                    FGMain.Focus()
                    Exit For
                End If

                For J = I + 1 To FGMain.Rows.Count - 1
                    If Trim(UCase(FGMain(coldesc, I).Value)) = Trim(UCase(FGMain(coldesc, J).Value)) Then
                        MsgBox(ClsMain.MsgDuplicate & "Description.")
                        FGMain(coldesc, J).Selected = True
                        BlnRtn = False
                        FGMain.Focus()
                        Exit For
                    End If
                Next
            End If
            If Not BlnRtn Then
                Exit For
            End If
        Next
        FCkhGrid = BlnRtn
    End Function

    Private Sub FHPGD_ACCOUNT(ByRef e As System.Windows.Forms.KeyEventArgs)
        Dim DTMain As New DataTable
        Dim FRH As DMHelpGrid.FrmHelpGrid
        Dim StrSendText As String

        If Not CMain.FGrdDisableKeys(e) Then Exit Sub
        StrSendText = Cmain.FSendText(FGMain, Chr(e.KeyCode))
        DTMain = AgL.FillData("Select SubCode,Name,ManualCode From Subgroup Where SiteList Like '%|" & AgL.PubSiteCode & "|%' Order by Name", AgL.GCn).Tables(0)

        FRH = New DMHelpGrid.FrmHelpGrid(New DataView(DTMain), StrSendText, 300, 380)
        FRH.FFormatColumn(0, , , , False)
        FRH.FFormatColumn(1, "Name", 200, DataGridViewContentAlignment.MiddleLeft)
        FRH.FFormatColumn(2, "Code", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH.StartPosition = FormStartPosition.CenterScreen
        FRH.ShowDialog()

        If FRH.BytBtnValue = 0 Then
            If Not FRH.DRReturn.Equals(Nothing) Then
                FGMain(ColAcCode, FGMain.CurrentCell.RowIndex).Value = FRH.DRReturn.Item(0)
                FGMain(colacname, FGMain.CurrentCell.RowIndex).Value = FRH.DRReturn.Item(1)
            End If
        End If
        FRH = Nothing
    End Sub
    Private Sub FHPGD_Description(ByRef e As System.Windows.Forms.KeyEventArgs)
        Dim DTMain As New DataTable
        Dim FRH As DMHelpGrid.FrmHelpGrid
        Dim StrSendText As String

        If Not CMain.FGrdDisableKeys(e) Then Exit Sub
        StrSendText = Cmain.FSendText(FGMain, Chr(e.KeyCode))
        DTMain = AgL.FillData("Select Code,Name From TDSCat_Description Order by Name", AgL.GCn).Tables(0)

        FRH = New DMHelpGrid.FrmHelpGrid(New DataView(DTMain), StrSendText, 300, 280)
        FRH.FFormatColumn(0, , , , False)
        FRH.FFormatColumn(1, "Name", 200, DataGridViewContentAlignment.MiddleLeft)
        FRH.StartPosition = FormStartPosition.CenterScreen
        FRH.ShowDialog()

        If FRH.BytBtnValue = 0 Then
            If Not FRH.DRReturn.Equals(Nothing) Then
                FGMain(coldesccode, FGMain.CurrentCell.RowIndex).Value = FRH.DRReturn.Item(0)
                FGMain(coldesc, FGMain.CurrentCell.RowIndex).Value = FRH.DRReturn.Item(1)
            End If
        End If
        FRH = Nothing
    End Sub
    Private Sub GrdNumPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Select Case FGMain.CurrentCell.ColumnIndex
            Case colper
                CMain.NumPress(sender, e, 3, 3, False)
        End Select
    End Sub

    Private Function SetActiveYN() As Boolean
        Dim mBool As Boolean
        If Topctrl1.Mode = "Browse" Then
            mBool = False
        Else
            mBool = True
        End If
        Return mBool
    End Function
End Class
