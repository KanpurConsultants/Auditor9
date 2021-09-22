Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.SQLite

Public Class FrmAssetGroupMaster
    Private Const GSNo As Byte = 0
    Private Const GAssetID As Byte = 1
    Private Const GAssetName As Byte = 2
    Private Const GStockDate As Byte = 3
    Private Const GValue As Byte = 4
    Private Const GAppreciated As Byte = 5
    Private Const GSoldDate As Byte = 6
    Private Const GSoldAmt As Byte = 7

    Private DTMaster As New DataTable()
    Public BMBMaster As BindingManagerBase
    Private LIEvent As ClsEvents
    Private WithEvents FGMain As New AgControls.AgDataGrid
    Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Topctrl1.FSetParent(Me, StrUPVar, DTUP)
        Topctrl1.SetDisp(True)
    End Sub
    Private Sub FrmAssetGroupMaster_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F2 Or e.KeyCode = Keys.F3 Or e.KeyCode = Keys.F4 Or e.KeyCode = (Keys.F And e.Control) Or e.KeyCode = (Keys.P And e.Control) _
        Or e.KeyCode = (Keys.S And e.Control) Or e.KeyCode = Keys.Escape Or e.KeyCode = Keys.F5 Or e.KeyCode = Keys.F10 _
        Or e.KeyCode = Keys.Home Or e.KeyCode = Keys.PageUp Or e.KeyCode = Keys.PageDown Or e.KeyCode = Keys.End Then
            Topctrl1.TopKey_Down(e)
        End If
    End Sub
    Private Sub FrmAssetGroupMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            LIEvent = New ClsEvents(Me)
            Agl.WinSetting(Me, 572, 930, 0, 0)
            FIniMaster()
            Agl.GridDesign(FGMain)
            IniGrid()
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
        PnlMain.Visible = False
        Controls.Add(FGMain)
        FGMain.Visible = True
        FGMain.BringToFront()
        FGMain.AllowUserToAddRows = False
        AgCl.AddAgTextColumn(FGMain, "SNo", 42, 5, "S.No.", True, True, False)
        AgCl.AddAgTextColumn(FGMain, "AssetID", 80, 5, "Asset ID", True, True, False)
        AgCl.AddAgTextColumn(FGMain, "Asset Name", 200, 5, "Asset Name", True, True, False)
        AgCl.AddAgTextColumn(FGMain, "Stock Date", 90, 5, "Stk Date", True, True, False)
        AgCl.AddAgTextColumn(FGMain, "Value", 110, 15, "Value", True, True, True)
        AgCl.AddAgTextColumn(FGMain, "Appreciated", 110, 15, "Appreciated", True, True, True)
        AgCl.AddAgTextColumn(FGMain, "Sold Date", 90, 5, "Sld Date", True, True, False)
        AgCl.AddAgTextColumn(FGMain, "Sld Value", 110, 15, "Sold Value", True, True, True)

        FGMain.Anchor = (AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Bottom)
        AgL.FSetSNo(FGMain, GSNo)
        FGMain.TabIndex = PnlMain.TabIndex
    End Sub
    Private Sub FFillGrid()
        Dim DTTemp As New DataTable
        Dim I As Integer

        If Not DTMaster.Rows.Count > 0 Then Exit Sub

        FGMain.Rows.Clear()
        DTTemp = cmain.FGetDatTable("Select	AM.DocId,Max(AM.Asset_Manual_Id) As AssetID,Max(AM.Name) As AssetName, " & _
                                "Max(StkDate) As StkDate,Sum(StkValue) As StkValue, " & _
                                "Sum(AprValue) As AprValue,Max(SldDate) As SldDate,Sum(SldValue) As SldValue " & _
                                "From AssetMast AM Left Join ( " & _
                                "Select	Asset,Max(V_Date) As StkDate,Sum(Amount) As StkValue, " & _
                                "0 As AprValue,Null As SldDate,0 As SldValue " & _
                                "From AssetTransaction Where V_Type In ('ASTOP','ASTPR') And " & _
                                "Site_Code='" & agl.PubSiteCode & "' " & _
                                "Group By Asset " & _
                                "Union All " & _
                                "Select	Asset,Null As StkDate,0 As StkValue, " & _
                                "Sum(Amount) As AprValue,Null As SldDate,0 As SldValue " & _
                                "From AssetTransaction Where V_Type In ('ASTAP') And " & _
                                "Site_Code='" & agl.PubSiteCode & "' " & _
                                "Group By Asset " & _
                                "Union All " & _
                                "Select	Asset,Null As StkDate,0 As StkValue, " & _
                                "0 As AprValue,Max(V_Date) As SldDate,Sum(Amount) As SldValue " & _
                                "From AssetTransaction Where V_Type In ('ASTSL') And " & _
                                "Site_Code='" & agl.PubSiteCode & "' " & _
                                "Group By Asset " & _
                                ") Tmp On Tmp.Asset=AM.DocId " & _
                                "Where AM.AssetGroup='" & Agl.Xnull(DTMaster.Rows(BMBMaster.Position).Item("SearchCode")) & "' And " & _
                                "AM.Site_Code='" & agl.PubSiteCode & "' " & _
                                "Group By AM.DocId", Agl.Gcn)
        If DTTemp.Rows.Count > 0 Then
            FGMain.Rows.Add(DTTemp.Rows.Count)
        End If
        For I = 0 To DTTemp.Rows.Count - 1
            FGMain(GSNo, I).Value = Trim(I + 1)
            FGMain(GAssetID, I).Value = Agl.Xnull(DTTemp.Rows(I).Item("AssetID"))
            FGMain(GAssetName, I).Value = Agl.Xnull(DTTemp.Rows(I).Item("AssetName"))
            FGMain(GStockDate, I).Value = Agl.Xnull(DTTemp.Rows(I).Item("StkDate"))
            FGMain(GValue, I).Value = IIf(Agl.VNull(DTTemp.Rows(I).Item("StkValue")) > 0, Format(Agl.VNull(DTTemp.Rows(I).Item("StkValue")), "0.00"), "")
            FGMain(GAppreciated, I).Value = IIf(Agl.VNull(DTTemp.Rows(I).Item("AprValue")) > 0, Format(Agl.VNull(DTTemp.Rows(I).Item("AprValue")), "0.00"), "")
            FGMain(GSoldDate, I).Value = Agl.Xnull(DTTemp.Rows(I).Item("SldDate"))
            FGMain(GSoldAmt, I).Value = IIf(Agl.VNull(DTTemp.Rows(I).Item("SldValue")) > 0, Format(Agl.VNull(DTTemp.Rows(I).Item("SldValue")), "0.00"), "")
        Next
        DTTemp.Dispose()
        DTTemp = Nothing

    End Sub
    Private Sub FIniMaster(Optional ByVal BytDel As Byte = 0, Optional ByVal BytRefresh As Byte = 1)
        Topctrl1.FIniForm(DTMaster, Agl.Gcn, "Select Code As SearchCode,Name From AssetGroupMast Order By Name", True, TxtName, "SearchCode", "Name", BytDel, BytRefresh)
    End Sub
    Private Sub Topctrl1_tbDiscard() Handles Topctrl1.tbDiscard
        FIniMaster(0, 0)
    End Sub
    Public Sub MoveRec()

        Dim DTTemp As New DataTable

        Topctrl1.BlankTextBoxes()

        If DTMaster.Rows.Count > 0 Then
            DTTemp = AgL.FillData("Select AG.Name,AG.ManualCode,AG.AcCode,SG.Name As AcName,AG.Depreciation " &
                    "From AssetGroupMast AG Left Join SubGroup SG On AG.AcCode=SG.SubCode " &
                    "Where AG.Code='" & AgL.XNull(DTMaster.Rows(BMBMaster.Position).Item("SearchCode")) & "'", AgL.GCn).Tables(0)

            If DTTemp.Rows.Count > 0 Then
                TxtName.Text = Agl.Xnull(DTTemp.Rows(0).Item("Name"))
                TxtManualCode.Text = Agl.Xnull(DTTemp.Rows(0).Item("ManualCode"))
                TxtAcName.Text = Agl.Xnull(DTTemp.Rows(0).Item("AcName"))
                TxtAcName.Tag = Agl.Xnull(DTTemp.Rows(0).Item("AcCode"))
                TxtDepreciation.Text = Agl.VNull(DTTemp.Rows(0).Item("Depreciation"))
            End If
        End If

        Topctrl1.FSetDispRec(BMBMaster)
        FFillGrid()

        DTTemp = Nothing
    End Sub
    Public Sub FTxtGotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        '======== Write Your Code Below =============
    End Sub
    Public Sub FTxtKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        '======== Write Your Code Below =============
        Select Case sender.Name
            Case TxtAcName.Name
                If e.KeyCode = Keys.Delete Then
                    sender.Text = "" : sender.Tag = ""
                End If
        End Select
    End Sub
    Public Sub FTxtKeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        '======== Write Your Code Below =============
        Select Case sender.Name
            Case TxtAcName.Name
                FHP_AcName(e, sender)
            Case TxtDepreciation.Name
                CMain.NumPress(TxtDepreciation, e, 3, 2, False)
        End Select
    End Sub
    Private Sub FHP_AcName(ByRef e As System.Windows.Forms.KeyPressEventArgs, ByVal Txt As TextBox)
        Dim DTMain As New DataTable
        Dim FRH As DMHelpGrid.FrmHelpGrid
        Dim StrSendText As String
        Dim StrTempCode As String = ""

        If Topctrl1.Mode <> "Add" Then
            StrTempCode = Agl.Xnull(DTMaster.Rows(BMBMaster.Position).Item("SearchCode"))
        End If
        StrSendText = Cmain.FSendText(Txt, e.KeyChar)
        DTMain = AgL.FillData("Select SG.SubCode,SG.Name,SG.ManualCode As Code,AG.GroupName From SubGroup SG Left Join AcGroup AG On SG.GroupCode=AG.GroupCode Where SG.SiteList Like '%|" & AgL.PubSiteCode & "|%' And AG.GroupNature='A' Order By Name", AgL.GCn).Tables(0)
        FRH = New DMHelpGrid.FrmHelpGrid(New DataView(DTMain), StrSendText, 300, 580, (Top + Txt.Top) + 85, Left + Txt.Left + 3)
        FRH.FFormatColumn(0, , 0, , False)
        FRH.FFormatColumn(1, "Name", 200, DataGridViewContentAlignment.MiddleLeft)
        FRH.FFormatColumn(2, "Code", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH.FFormatColumn(3, "Group", 200, DataGridViewContentAlignment.MiddleLeft)
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
        FGMain.Rows.Clear()
        TxtManualCode.Text = CMain.FGetMaxNo("Select IfNull(Max(Cast(ManualCode as Integer)),0)+1 As Mx From AssetGroupMast Where (Case When ManualCode GLOB '*[0-9]*' Then ManualCode Else 0 End)<>0", AgL.GCn)
        TxtName.Focus()
    End Sub
    Private Sub Topctrl1_tbDel() Handles Topctrl1.tbDel
        Dim BlnTrans As Boolean = False
        Dim GCnCmd As New Object

        Try
            If DTMaster.Rows.Count > 0 Then
                If MsgBox(" Delete Conflict ", MsgBoxStyle.YesNo) = vbYes Then
                    StrDocID = ""
                    StrDocID = AgL.XNull(DTMaster.Rows(BMBMaster.Position).Item("SearchCode"))
                    If Trim(Replace(StrDocID, "0", "")) = "" Then MsgBox(" Invalid " & "DocId.") : Exit Sub

                    BlnTrans = True
                    GCnCmd = AgL.GCn.CreateCommand
                    GCnCmd.Transaction = AgL.GCn.BeginTransaction(IsolationLevel.Serializable)

                    GCnCmd.CommandText = "Delete From AssetGroupMast Where Code='" & (StrDocID) & "'"
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
            TxtName.Focus()
        End If
    End Sub
    Private Sub Topctrl1_tbFind() Handles Topctrl1.tbFind
        If DTMaster.Rows.Count <= 0 Then MsgBox(ClsMain.MsgRecNotFnd) : Exit Sub
        Try
            AgL.PubFindQry = "Select AG.Code As AgCode,AG.Name,AG.ManualCode As Code,SG.Name As AcName,AG.Depreciation " &
                         "From AssetGroupMast AG Left Join SubGroup SG On AG.AcCode=SG.SubCode "
            AgL.PubFindQryOrdBy = "Name"
            'LIPublic.CreateAndSendArr("200,100,200,100")
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
        Dim StrName As String

        Try
            If AgL.RequiredField(TxtName, "Asset Group Name") Then Exit Sub
            If AgL.RequiredField(TxtManualCode, "Asset Group Code") Then Exit Sub

            StrName = CMain.FRemoveSpace(TxtName.Text)
            StrDocID = ""
            If Topctrl1.Mode = "Add" Then
                'StrDocID = agl.PubSiteCode + Trim(CMain.FGetMaxNoWithSiteCode("AssetGroupMast", "Code", Agl.Gcn))
                StrDocID = AgL.GetMaxId("AssetGroupMast", "Code", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 5, True)
            Else
                StrDocID = Agl.Xnull(DTMaster.Rows(BMBMaster.Position).Item("SearchCode"))
            End If
            If Trim(Replace(StrDocID, "0", "")) = "" Then MsgBox(" Invalid " & "DocId.") : Exit Sub
            If CMain.DuplicacyChecking("Select Count(Name) As Cnt From AssetGroupMast Where Name='" & StrName & "' And Code<>'" & (StrDocID) & "'", "Asset Group Name Already Exists.") Then TxtName.Focus() : Exit Sub
            If CMain.DuplicacyChecking("Select Count(ManualCode) As Cnt From AssetGroupMast Where ManualCode='" & TxtManualCode.Text & "' And Code<>'" & (StrDocID) & "'", "Asset Group Code Already Exists.") Then TxtManualCode.Focus() : Exit Sub

            BlnTrans = True
            GCnCmd = AgL.GCn.CreateCommand
            GCnCmd.Transaction = Agl.Gcn.BeginTransaction(IsolationLevel.Serializable)

            If Topctrl1.Mode = "Add" Then
                GCnCmd.CommandText = "Insert Into AssetGroupMast(Code,Name,ManualCode,AcCode, " & _
                                     "Depreciation,PreparedBy," & _
                                     "U_EntDt,U_AE) Values " & _
                                     "('" & (StrDocID) & "'," & AgL.Chk_Text(StrName) & "," & AgL.Chk_Text(TxtManualCode.Text) & "," & AgL.Chk_Text(TxtAcName.Tag) & ", " & _
                                     "" & Val(TxtDepreciation.Text) & ",'" & AgL.PubUserName & "'," & _
                                     "'" & Format(AgL.PubLoginDate, "Short Date") & "','" & Microsoft.VisualBasic.Left(Topctrl1.Mode, 1) & "')"
            Else
                GCnCmd.CommandText = "Update AssetGroupMast Set "
                GCnCmd.CommandText = GCnCmd.CommandText + "Name=" & AgL.Chk_Text(StrName) & ", "
                GCnCmd.CommandText = GCnCmd.CommandText + "ManualCode=" & AgL.Chk_Text(TxtManualCode.Text) & ", "
                GCnCmd.CommandText = GCnCmd.CommandText + "AcCode=" & Agl.Chk_Text(TxtAcName.Tag) & ", "
                GCnCmd.CommandText = GCnCmd.CommandText + "Depreciation=" & Val(TxtDepreciation.Text) & ", "
                GCnCmd.CommandText = GCnCmd.CommandText + "Transfered='N', "
                GCnCmd.CommandText = GCnCmd.CommandText + "PreparedBy='" & Agl.PubUserName & "', "
                GCnCmd.CommandText = GCnCmd.CommandText + "U_EntDt='" & Format(Agl.PubLoginDate, "Short Date") & "', "
                GCnCmd.CommandText = GCnCmd.CommandText + "U_AE='" & Microsoft.VisualBasic.Left(Topctrl1.Mode, 1) & "' "
                GCnCmd.CommandText = GCnCmd.CommandText + "Where Code='" & (StrDocID) & "' "
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
    Private Sub FrmAssetGroupMaster_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        Agl.FPaintForm(Me, e, Topctrl1.Height)
    End Sub
End Class
