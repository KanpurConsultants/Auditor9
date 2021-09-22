Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.SQLite
Public Class FrmAssetMaster
    Private DTMaster As New DataTable()
    Public BMBMaster As BindingManagerBase
    Private LIEvent As ClsEvents
    Private StrV_Type As String = "ASSET"
    Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Topctrl1.FSetParent(Me, StrUPVar, DTUP)
        Topctrl1.SetDisp(True)
    End Sub
    Private Sub FrmAssetMaster_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F2 Or e.KeyCode = Keys.F3 Or e.KeyCode = Keys.F4 Or e.KeyCode = (Keys.F And e.Control) Or e.KeyCode = (Keys.P And e.Control) _
        Or e.KeyCode = (Keys.S And e.Control) Or e.KeyCode = Keys.Escape Or e.KeyCode = Keys.F5 Or e.KeyCode = Keys.F10 _
        Or e.KeyCode = Keys.Home Or e.KeyCode = Keys.PageUp Or e.KeyCode = Keys.PageDown Or e.KeyCode = Keys.End Then
            Topctrl1.TopKey_Down(e)
        End If
    End Sub
    Private Sub FrmAssetMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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
        Topctrl1.FIniForm(DTMaster, Agl.Gcn, "Select DocId As SearchCode,Name From AssetMast Where Site_Code='" & agl.PubSiteCode & "' Order By Name", True, TxtName, "SearchCode", "Name", BytDel, BytRefresh)
    End Sub
    Private Sub Topctrl1_tbDiscard() Handles Topctrl1.tbDiscard
        FIniMaster(0, 0)
    End Sub
    Public Sub MoveRec()

        Dim DTTemp As New DataTable

        Topctrl1.BlankTextBoxes()
        If DTMaster.Rows.Count > 0 Then
            DTTemp = AgL.FillData("Select AM.Asset_SYS_ID,AM.V_Prefix,AM.Name,AM.Asset_Manual_ID,AM.AssetGroup,AG.Name As AssetName " &
                    "From AssetMast AM Left Join AssetGroupMast AG On AM.AssetGroup=AG.Code " &
                    "Where AM.DocId='" & AgL.XNull(DTMaster.Rows(BMBMaster.Position).Item("SearchCode")) & "'", AgL.GCn).Tables(0)
            If DTTemp.Rows.Count > 0 Then
                TxtID.Text = Agl.Xnull(DTTemp.Rows(0).Item("Asset_SYS_ID"))
                TxtID.Tag = Agl.Xnull(DTTemp.Rows(0).Item("V_Prefix"))
                TxtName.Text = Agl.Xnull(DTTemp.Rows(0).Item("Name"))
                TxtManualCode.Text = Agl.Xnull(DTTemp.Rows(0).Item("Asset_Manual_ID"))
                TxtAssetGroup.Text = Agl.Xnull(DTTemp.Rows(0).Item("AssetName"))
                TxtAssetGroup.Tag = Agl.Xnull(DTTemp.Rows(0).Item("AssetGroup"))
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
            Case TxtAssetGroup.Name
                If e.KeyCode = Keys.Delete Then
                    sender.Text = "" : sender.Tag = ""
                End If
        End Select
    End Sub
    Public Sub FTxtKeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        '======== Write Your Code Below =============
        Select Case sender.Name
            Case TxtAssetGroup.Name
                FHP_AssetGroup(e, sender)
        End Select
    End Sub
    Private Sub FHP_AssetGroup(ByRef e As System.Windows.Forms.KeyPressEventArgs, ByVal Txt As TextBox)
        Dim DTMain As New DataTable
        Dim FRH As DMHelpGrid.FrmHelpGrid
        Dim StrSendText As String
        Dim StrTempCode As String = ""

        If Topctrl1.Mode <> "Add" Then
            StrTempCode = Agl.Xnull(DTMaster.Rows(BMBMaster.Position).Item("SearchCode"))
        End If
        StrSendText = Cmain.FSendText(Txt, e.KeyChar)
        DTMain = AgL.FillData("Select Code,Name,ManualCode As Code From AssetGroupMast Order By Name", AgL.GCn).Tables(0)

        FRH = New DMHelpGrid.FrmHelpGrid(New DataView(DTMain), StrSendText, 300, 380, (Top + Txt.Top) + 85, Left + Txt.Left + 3)
        FRH.FFormatColumn(0, , 0, , False)
        FRH.FFormatColumn(1, "Name", 200, DataGridViewContentAlignment.MiddleLeft)
        FRH.FFormatColumn(2, "Code", 100, DataGridViewContentAlignment.MiddleLeft)
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
        TxtManualCode.Text = CMain.FGetMaxNo("Select IfNull(Max(Cast(Asset_Manual_ID as Integer)),0)+1 As Mx From AssetMast Where (Case When Asset_Manual_ID GLOB '*[0-9]*' Then Asset_Manual_ID Else 0 End) <>0 And Site_Code='" & AgL.PubSiteCode & "'", AgL.GCn)
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

                    GCnCmd.CommandText = "Delete From AssetMast Where DocId='" & (StrDocID) & "'"
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
            AgL.PubFindQry = "Select AM.DocId,AM.Name,AM.Asset_Manual_ID As AssetId,AG.Name As AssetName " &
                         "From AssetMast AM Left Join AssetGroupMast AG On AM.AssetGroup=AG.Code Where AM.Site_Code='" & AgL.PubSiteCode & "' "
            AgL.PubFindQryOrdBy = "Name"
            'LIPublic.CreateAndSendArr("200,100,200")
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
            If AgL.RequiredField(TxtName, "Asset Name") Then Exit Sub
            If AgL.RequiredField(TxtManualCode, "Asset Id") Then Exit Sub
            If AgL.RequiredField(TxtAssetGroup, "Asset Group") Then Exit Sub

            StrName = CMain.FRemoveSpace(TxtName.Text)
            StrDocID = ""
            If Topctrl1.Mode = "Add" Then
                TxtID.Text = ""
                StrDocID = CMain.FGetDoId(TxtID, StrV_Type, "AssetMast", "Asset_SYS_ID", AgL.PubLoginDate)
            Else
                StrDocID = Agl.Xnull(DTMaster.Rows(BMBMaster.Position).Item("SearchCode"))
            End If
            If Trim(Replace(StrDocID, "0", "")) = "" Then MsgBox(" Invalid " & "DocId.") : Exit Sub
            If CMain.DuplicacyChecking("Select Count(Name) As Cnt From AssetMast Where Name='" & StrName & "' And DocId<>'" & (StrDocID) & "' And Site_Code='" & AgL.PubSiteCode & "'", "Asset Name Already Exists.") Then TxtName.Focus() : Exit Sub
            If CMain.DuplicacyChecking("Select Count(Asset_Manual_ID) As Cnt From AssetMast Where Asset_Manual_ID='" & TxtManualCode.Text & "' And DocId<>'" & (StrDocID) & "' And Site_Code='" & AgL.PubSiteCode & "'", "Asset ID Already Exists.") Then TxtManualCode.Focus() : Exit Sub

            BlnTrans = True
            GCnCmd = AgL.GCn.CreateCommand
            GCnCmd.Transaction = Agl.Gcn.BeginTransaction(IsolationLevel.Serializable)

            If Topctrl1.Mode = "Add" Then
                GCnCmd.CommandText = "Insert Into AssetMast(DocId,V_Type,Asset_SYS_ID,V_Prefix, " & _
                                     "Name,Asset_Manual_ID,AssetGroup, " & _
                                     "PreparedBy,Site_Code," & _
                                     "U_EntDt,U_AE) Values " & _
                                     "('" & (StrDocID) & "','" & StrV_Type & "'," & AgL.Chk_Text(TxtID.Text) & "," & AgL.Chk_Text(TxtID.Tag) & ", " & _
                                     "" & AgL.Chk_Text(StrName) & "," & AgL.Chk_Text(TxtManualCode.Text) & "," & AgL.Chk_Text(TxtAssetGroup.Tag) & ", " & _
                                     "'" & AgL.PubUserName & "','" & AgL.PubSiteCode & "'," & _
                                     "'" & Format(AgL.PubLoginDate, "Short Date") & "','" & Microsoft.VisualBasic.Left(Topctrl1.Mode, 1) & "')"
            Else
                GCnCmd.CommandText = "Update AssetMast Set "
                GCnCmd.CommandText = GCnCmd.CommandText + "Name=" & AgL.Chk_Text(StrName) & ", "
                GCnCmd.CommandText = GCnCmd.CommandText + "Asset_Manual_ID=" & AgL.Chk_Text(TxtManualCode.Text) & ", "
                GCnCmd.CommandText = GCnCmd.CommandText + "AssetGroup=" & Agl.Chk_Text(TxtAssetGroup.Tag) & ", "
                GCnCmd.CommandText = GCnCmd.CommandText + "Transfered='N', "
                GCnCmd.CommandText = GCnCmd.CommandText + "PreparedBy='" & Agl.PubUserName & "', "
                GCnCmd.CommandText = GCnCmd.CommandText + "U_EntDt='" & Format(Agl.PubLoginDate, "Short Date") & "', "
                GCnCmd.CommandText = GCnCmd.CommandText + "U_AE='" & Microsoft.VisualBasic.Left(Topctrl1.Mode, 1) & "' "
                GCnCmd.CommandText = GCnCmd.CommandText + "Where DocId='" & (StrDocID) & "' "
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
    Private Sub FrmAssetMaster_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        Agl.FPaintForm(Me, e, Topctrl1.Height)
    End Sub
End Class
