Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.SQLite
Public Class FrmBudget
    Private DTMaster As New DataTable()
    Public BMBMaster As BindingManagerBase
    Private LIEvent As ClsEvents
    Private WithEvents FGMain As New AgControls.AgDataGrid
    Private Const GSNo As Byte = 0
    Private Const GCostCenter As Byte = 1
    Private Const GCostCenterCode As Byte = 2
    Private Const GAmount As Byte = 3
    Private StrV_Type As String = "BDGT"
    Private Sub FrmBudget_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

    Private Sub FrmBudget_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            LIEvent = New ClsEvents(Me)
            Agl.WinSetting(Me, 554, 891, 0, 0)
            Agl.GridDesign(FGMain)
            IniGrid()
            FIniMaster()
            MoveRec()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
    Private Sub FManageDisplay(ByVal BlnEnb As Boolean)
        FGMain.Columns(GAmount).ReadOnly = BlnEnb

        TxtPrepared.Enabled = False
        TxtModified.Enabled = False
        TxtBudgetID.Enabled = False
        TxtRecid.Enabled = False
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
        AgCl.AddAgTextColumn(FGMain, "Cost Center", 230, 35, "Cost Center", True, True, False)
        AgCl.AddAgTextColumn(FGMain, "CostCenterCode", 0, 0, "Cost Center Code", False, False, False)
        AgCl.AddAgTextColumn(FGMain, "Amount", 120, 13, "Amount", True, False, True)
        FGMain.Anchor = (AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Bottom)
        Agl.FSetSNo(FGMain, GSNo)
        FGMain.TabIndex = PnlMain.TabIndex
    End Sub
    Private Sub FIniMaster(Optional ByVal BytDel As Byte = 0, Optional ByVal BytRefresh As Byte = 1)
        Topctrl1.FIniForm(DTMaster, Agl.Gcn, "Select DocId As SearchCode,Name From Budget Where Site_Code='" & agl.PubSiteCode & "' And V_Prefix='" & Agl.PubCompVPrefix & "' Order By Name", True, TxtDescription, "SearchCode", "Name", BytDel, BytRefresh)
    End Sub
    Private Sub Topctrl1_tbDiscard() Handles Topctrl1.tbDiscard
        FIniMaster(0, 0)
    End Sub
    Public Sub MoveRec()
        Dim DTTemp As New DataTable
        Dim I As Integer
        Dim StrSQL As String

        FManageDisplay(True)
        FGMain.Rows.Clear()
        Topctrl1.BlankTextBoxes()
        If DTMaster.Rows.Count > 0 Then
            StrSQL = ""
            StrSQL = "Select V_No,V_Prefix,Recid,V_Date,Name,DateFrom,DateTo,PreparedBy,ModifiedBy "
            StrSQL = StrSQL + "From Budget "
            StrSQL = StrSQL + "Where DocId='" & Agl.Xnull(DTMaster.Rows(BMBMaster.Position).Item("SearchCode")) & "'"
            DTTemp = cmain.FGetDatTable(StrSQL, Agl.Gcn)
            If DTTemp.Rows.Count > 0 Then
                TxtBudgetID.Text = Agl.Xnull(DTTemp.Rows(0).Item("V_No"))
                TxtRecid.Text = Agl.Xnull(DTTemp.Rows(0).Item("Recid"))
                TxtBudgetID.Tag = Agl.Xnull(DTTemp.Rows(0).Item("V_Prefix"))
                TxtDate.Text = Agl.Xnull(DTTemp.Rows(0).Item("V_Date"))
                TxtDescription.Text = Agl.Xnull(DTTemp.Rows(0).Item("Name"))
                TxtFrom.Text = Agl.Xnull(DTTemp.Rows(0).Item("DateFrom"))
                TxtTo.Text = Agl.Xnull(DTTemp.Rows(0).Item("DateTo"))
                TxtPrepared.Text = Agl.Xnull(DTTemp.Rows(0).Item("PreparedBy"))
                TxtModified.Text = Agl.Xnull(DTTemp.Rows(0).Item("ModifiedBy"))

                DTTemp.Clear()
                StrSQL = ""
                StrSQL = "Select  CCM.Name,BMD.CostCenter,BMD.Amount "
                StrSQL = StrSQL + "From BudgetDet BMD Left Join "
                StrSQL = StrSQL + "CostCenterMast CCM On CCM.Code=BMD.CostCenter "
                StrSQL = StrSQL + "Where BMD.DocId='" & Agl.Xnull(DTMaster.Rows(BMBMaster.Position).Item("SearchCode")) & "'"
                DTTemp = cmain.FGetDatTable(StrSQL, Agl.Gcn)
                If DTTemp.Rows.Count > 0 Then
                    FGMain.Rows.Add(DTTemp.Rows.Count)
                End If
                For I = 0 To DTTemp.Rows.Count - 1
                    FGMain.Item(GSNo, I).Value = I + 1
                    FGMain.Item(GCostCenter, I).Value = Agl.Xnull(DTTemp.Rows(I).Item("Name"))
                    FGMain.Item(GCostCenterCode, I).Value = Agl.Xnull(DTTemp.Rows(I).Item("CostCenter"))
                    FGMain.Item(GAmount, I).Value = Format(Agl.VNull(DTTemp.Rows(I).Item("Amount")), "0.00")
                Next
                DTTemp.Dispose()
                DTTemp = Nothing
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
        FManageDisplay(False)
        TxtDescription.Focus()
        FGMain.Rows.Clear()
        TxtPrepared.Text = Agl.PubUserName
        TxtDate.Text = Agl.PubLoginDate
        cmain.FGetDoId(TxtBudgetID, StrV_Type, "Budget", "V_No", TxtDate.Text)
        TxtRecid.Text = CMain.FGetMaxNo("Select IfNull(Max(cast(B.V_No as Integer)),0)+1 As Mx From Budget B Where (Case When B.V_No GLOB '*[0-9]*' Then B.V_No Else 0 End)<>0 And B.V_Prefix='" & TxtBudgetID.Tag & "' And Site_Code='" & AgL.PubSiteCode & "' And V_Type='" & StrV_Type & "' ", AgL.GCn)
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
                    GCnCmd.CommandText = "Delete From BudgetDet Where DocId='" & StrDocID & "'"
                    GCnCmd.ExecuteNonQuery()
                    GCnCmd.CommandText = "Delete From Budget Where DocId='" & StrDocID & "'"
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
            FManageDisplay(False)
            TxtDescription.Enabled = True
            TxtFrom.Focus()
            FGMain.CurrentCell = FGMain.Item(GCostCenter, 0)
            FGMain.CurrentCell.Selected = True
        End If
    End Sub
    Private Sub Topctrl1_tbFind() Handles Topctrl1.tbFind
        If DTMaster.Rows.Count <= 0 Then MsgBox(ClsMain.MsgRecNotFnd) : Exit Sub
        Try
            AgL.PubFindQry = "Select DocId, Name From Budget Where Site_Code='" & AgL.PubSiteCode & "' And V_Prefix='" & AgL.PubCompVPrefix & "' "
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
            If AgL.RequiredField(TxtDate, "Date") Then Exit Sub
            If AgL.RequiredField(TxtDescription, "Name") Then Exit Sub
            If AgL.RequiredField(TxtFrom, "From") Then Exit Sub
            If AgL.RequiredField(TxtTo, "To") Then Exit Sub
            If CDate(TxtFrom.Text) > CDate(TxtTo.Text) Then MsgBox("To Date Should Be Greater Than From Date.") : Exit Sub

            StrName = CMain.FRemoveSpace(TxtDescription.Text)
            StrDocID = ""
            If Topctrl1.Mode = "Add" Then
                TxtBudgetID.Text = ""
                StrDocID = CMain.FGetDoId(TxtBudgetID, StrV_Type, "Budget", "V_No", TxtDate.Text)
                TxtRecid.Text = CMain.FGetMaxNo("Select IfNull(Max(Cast(B.V_No as Integer)),0)+1 As Mx From Budget B Where (Case When B.V_No GLOB '*[0-9]*' Then B.V_No Else 0 End)<>0 And B.V_Prefix='" & TxtBudgetID.Tag & "' And Site_Code='" & AgL.PubSiteCode & "' And V_Type='" & StrV_Type & "' ", AgL.GCn)
            Else
                StrDocID = Agl.Xnull(DTMaster.Rows(BMBMaster.Position).Item("SearchCode"))
            End If
            If Trim(Replace(StrDocID, "0", "")) = "" Then MsgBox(" Invalid " & "DocId.") : Exit Sub
            If CMain.DuplicacyChecking("Select Count(Name) As Cnt From Budget BM Where BM.Name='" & StrName & "' And BM.DocId<>'" & (StrDocID) & "' And BM.Site_Code='" & agl.PubSiteCode & "' ", "Description Already Exists.") Then TxtDescription.Focus() : Exit Sub
            If Not FCkhGrid(StrDocID) Then Exit Sub

            BlnTrans = True
            GCnCmd = AgL.GCn.CreateCommand
            GCnCmd.Transaction = Agl.Gcn.BeginTransaction(IsolationLevel.Serializable)
            GCnCmd.CommandText = "Delete From BudgetDet where DocId='" & StrDocID & "'"
            GCnCmd.ExecuteNonQuery()
            If Topctrl1.Mode = "Add" Then
                GCnCmd.CommandText = "Insert Into Budget(DocId,V_No,V_Prefix,Recid,V_Date, " &
                                     "V_Type,Name,Site_Code," &
                                     "PreparedBy,ModifiedBy,U_EntDt,U_AE,DateFrom,DateTo)" &
                                     " Values('" & StrDocID & "','" & TxtBudgetID.Text & "','" & TxtBudgetID.Tag & "','" & TxtRecid.Text & "','" & TxtDate.Text & "', " &
                                     "" & AgL.Chk_Text(StrV_Type) & "," & AgL.Chk_Text(StrName) & ",'" & AgL.PubSiteCode & "' ," &
                                     "'" & AgL.PubUserName & "','" & AgL.PubUserName & "','" & Format(AgL.PubLoginDate, "Short Date") & "','A'," & AgL.Chk_Text(CDate(TxtFrom.Text).ToString("s")) & "," & AgL.Chk_Text(CDate(TxtTo.Text).ToString("s")) & ")"

            Else
                GCnCmd.CommandText = "Update Budget Set Name=" & Trim(AgL.Chk_Text(StrName)) & ""
                GCnCmd.CommandText = GCnCmd.CommandText + ",V_Date=" & AgL.Chk_Text(CDate(TxtDate.Text).ToString("s")) & " "
                GCnCmd.CommandText = GCnCmd.CommandText + ",DateFrom=" & AgL.Chk_Text(CDate(TxtFrom.Text).ToString("s")) & " "
                GCnCmd.CommandText = GCnCmd.CommandText + ",DateTo=" & AgL.Chk_Text(CDate(TxtTo.Text).ToString("s")) & " "
                GCnCmd.CommandText = GCnCmd.CommandText + ",Transfered='N'"
                GCnCmd.CommandText = GCnCmd.CommandText + ",ModifiedBy='" & Agl.PubUserName & "' "
                GCnCmd.CommandText = GCnCmd.CommandText + ",U_EntDt='" & Format(Agl.PubLoginDate, "Short Date") & "'"
                GCnCmd.CommandText = GCnCmd.CommandText + ",U_AE='E' "
                GCnCmd.CommandText = GCnCmd.CommandText + "Where DocId='" & StrDocID & "'"
            End If
            GCnCmd.ExecuteNonQuery()

            For I = 0 To FGMain.RowCount - 1
                If Not FGMain.Item(GCostCenter, I).Value = "" Then
                    GCnCmd.CommandText = "Insert Into BudgetDet(DocId,SNo,CostCenter,Amount,Site_Code) " & _
                                         " Values('" & StrDocID & "'," & Val(FGMain.Item(GSNo, I).Value) & " , " & _
                                          "" & Agl.Chk_Text(FGMain.Item(GCostCenterCode, I).Value) & "," & Val(FGMain.Item(GAmount, I).Value) & ",'" & agl.PubSiteCode & "')"
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
                Case GCostCenter
                    FHPGD_CostCenter(e)
            End Select
        Catch Ex As NullReferenceException
        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try
    End Sub
    Private Sub FGMain_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles FGMain.RowsAdded
        FGMain(GSNo, FGMain.Rows.Count - 1).Value = Trim(FGMain.Rows.Count)
    End Sub
    Private Sub FGMain_RowsRemoved(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs) Handles FGMain.RowsRemoved
        Agl.FSetSNo(FGMain, GSNo)
    End Sub
    Private Sub FrmBudget_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        Agl.FPaintForm(Me, e, Topctrl1.Height)
    End Sub
    Private Function FCkhGrid(ByVal StrDocId As String) As Boolean
        Dim I As Integer, J As Integer
        Dim BlnRtn As Boolean

        BlnRtn = True
        For I = 0 To FGMain.Rows.Count - 1
            If Trim(FGMain(GCostCenter, I).Value) <> "" Then
                For J = I + 1 To FGMain.Rows.Count - 1
                    If Trim(UCase(FGMain(GCostCenter, I).Value)) = Trim(UCase(FGMain(GCostCenter, J).Value)) Then
                        MsgBox(ClsMain.MsgDuplicate & "Cost Center.")
                        FGMain(GCostCenter, J).Selected = True
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
    Private Sub FHPGD_CostCenter(ByRef e As System.Windows.Forms.KeyEventArgs)
        Dim DTMain As New DataTable
        Dim FRH As DMHelpGrid.FrmHelpGrid
        Dim StrSendText As String

        If Not CMain.FGrdDisableKeys(e) Then Exit Sub
        StrSendText = Cmain.FSendText(FGMain, Chr(e.KeyCode))
        DTMain = AgL.FillData("Select Code,Name From CostCenterMast Order by Name", AgL.GCn).Tables(0)

        FRH = New DMHelpGrid.FrmHelpGrid(New DataView(DTMain), StrSendText, 300, 280)
        FRH.FFormatColumn(0, , , , False)
        FRH.FFormatColumn(1, "Name", 200, DataGridViewContentAlignment.MiddleLeft)
        FRH.StartPosition = FormStartPosition.CenterScreen
        FRH.ShowDialog()

        If FRH.BytBtnValue = 0 Then
            If Not FRH.DRReturn.Equals(Nothing) Then
                FGMain(GCostCenterCode, FGMain.CurrentCell.RowIndex).Value = FRH.DRReturn.Item(0)
                FGMain(GCostCenter, FGMain.CurrentCell.RowIndex).Value = FRH.DRReturn.Item(1)
            End If
        End If
        FRH = Nothing
    End Sub
    Private Sub GrdNumPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Select Case FGMain.CurrentCell.ColumnIndex
            Case GAmount
                CMain.NumPress(sender, e, 10, 2, False)
        End Select
    End Sub
    Private Sub TxtFrom_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtFrom.Validated, TxtTo.Validated, TxtDate.Validated
        Select Case sender.Name
            Case TxtFrom.Name, TxtTo.Name, TxtDate.Name
                sender.Text = Agl.RetDate(sender.Text)
        End Select
    End Sub
End Class
