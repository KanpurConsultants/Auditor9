Imports CrystalDecisions.CrystalReports.Engine
Public Class FrmAssetTransaction
    Private DTMaster As New DataTable()
    Public BMBMaster As BindingManagerBase
    Private LIEvent As ClsEvents
    Dim FrmFind As New AgLibrary.FrmFind(Agl)
    Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Topctrl1.FSetParent(Me, StrUPVar, DTUP)
        Topctrl1.SetDisp(True)
    End Sub
    Private Sub FrmAssetTransaction_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F2 Or e.KeyCode = Keys.F3 Or e.KeyCode = Keys.F4 Or e.KeyCode = (Keys.F And e.Control) Or e.KeyCode = (Keys.P And e.Control) _
        Or e.KeyCode = (Keys.S And e.Control) Or e.KeyCode = Keys.Escape Or e.KeyCode = Keys.F5 Or e.KeyCode = Keys.F10 _
        Or e.KeyCode = Keys.Home Or e.KeyCode = Keys.PageUp Or e.KeyCode = Keys.PageDown Or e.KeyCode = Keys.End Then
            Topctrl1.TopKey_Down(e)
        End If
    End Sub
    Private Sub FrmAssetTransaction_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            LIEvent = New ClsEvents(Me)
            Agl.WinSetting(Me, 510, 891, 0, 0)
            FIniMaster()
            IniList()
            MoveRec()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub FIniMaster(Optional ByVal BytDel As Byte = 0, Optional ByVal BytRefresh As Byte = 1)
        Topctrl1.FIniForm(DTMaster, AgL.GCn, "Select DocId As SearchCode,Asset From AssetTransaction Where Site_Code='" & AgL.PubSiteCode & "' And V_Prefix='" & AgL.PubCompVPrefix & "' Order By Recid", , , , , BytDel, BytRefresh)
    End Sub
    Private Sub Topctrl1_tbDiscard() Handles Topctrl1.tbDiscard
        FIniMaster(0, 0)
    End Sub
    Public Sub MoveRec()
        Dim ADTemp As SqlClient.SqlDataAdapter
        Dim DTTemp As New DataTable

        FManageDisplay(True)
        Topctrl1.BlankTextBoxes()
        If DTMaster.Rows.Count > 0 Then
            ADTemp = New SqlClient.SqlDataAdapter("Select AT.V_No,AT.V_No, AT.V_Prefix,AT.V_Date,AT.V_Type,AT.Remark, " & _
                    "AT.Asset,AT.Amount,AM.Name As AMName,AM.AssetGroup,AG.Name As GroupName,VT.Description, " & _
                    "AT.PreparedBy,AT.ModifiedBy " & _
                    "From AssetTransaction AT Left Join " & _
                    "AssetMast AM On AT.Asset=AM.DocId Left Join " & _
                    "AssetGroupMast AG On AM.AssetGroup=AG.Code Left Join " & _
                    "Voucher_Type VT On AT.V_Type=VT.V_Type " & _
                    "Where AT.DocId='" & AgL.XNull(DTMaster.Rows(BMBMaster.Position).Item("SearchCode")) & "'", AgL.GCn)
            ADTemp.Fill(DTTemp)
            If DTTemp.Rows.Count > 0 Then
                TxtID.Text = AgL.XNull(DTTemp.Rows(0).Item("V_No"))
                TxtRecid.Text = AgL.XNull(DTTemp.Rows(0).Item("RecID"))
                TxtID.Tag = AgL.XNull(DTTemp.Rows(0).Item("V_Prefix"))
                TxtDate.Text = AgL.XNull(DTTemp.Rows(0).Item("V_Date"))
                TxtType.Text = AgL.XNull(DTTemp.Rows(0).Item("Description"))
                TxtType.Tag = AgL.XNull(DTTemp.Rows(0).Item("V_Type"))
                TxtAssetGroup.Text = AgL.XNull(DTTemp.Rows(0).Item("GroupName"))
                TxtAssetGroup.Tag = AgL.XNull(DTTemp.Rows(0).Item("AssetGroup"))
                TxtAssetName.Text = AgL.XNull(DTTemp.Rows(0).Item("AMName"))
                TxtAssetName.Tag = AgL.XNull(DTTemp.Rows(0).Item("Asset"))
                TxtValue.Text = Agl.VNull(DTTemp.Rows(0).Item("Amount"))
                TxtRemark.Text = AgL.XNull(DTTemp.Rows(0).Item("Remark"))
                TxtPrepared.Text = AgL.XNull(DTTemp.Rows(0).Item("PreparedBy"))
                TxtModified.Text = AgL.XNull(DTTemp.Rows(0).Item("ModifiedBy"))
            End If
        End If

        Topctrl1.FSetDispRec(BMBMaster)
        ADTemp = Nothing
        DTTemp = Nothing
    End Sub
    Private Sub FManageDisplay(ByVal BlnEnb As Boolean)
        TxtID.Enabled = False
        TxtRecid.Enabled = False
        TxtPrepared.Enabled = False
        TxtModified.Enabled = False
    End Sub


    Public Sub FTxtKeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        '======== Write Your Code Below =============
        Select Case sender.Name
            Case TxtValue.Name
                CMain.NumPress(TxtValue, e, 10, 2, False)
        End Select
    End Sub

    Sub IniList()
        Dim mQry$
        mQry = "Select Code,Name,ManualCode As Code From AssetGroupMast Order By Name"
        TxtAssetGroup.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)

        mQry = "Select V_Type,Description From Voucher_Type Where NCat='FA' And Category='ASTTR' Order By Description"
        TxtType.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)
    End Sub

    Private Sub Topctrl1_tbAdd() Handles Topctrl1.tbAdd
        FManageDisplay(False)
        TxtDate.Text = AgL.PubLoginDate
        TxtPrepared.Text = AgL.PubUserName
        TxtType.Focus()
    End Sub
    Private Sub Topctrl1_tbDel() Handles Topctrl1.tbDel
        Dim BlnTrans As Boolean = False
        Dim GCnCmd As New SqlClient.SqlCommand
        Dim DTTemp As New DataTable

        Try
            If DTMaster.Rows.Count > 0 Then
                If MsgBox(" Delete Conflict ", MsgBoxStyle.YesNo) = vbYes Then
                    If Trim(UCase(TxtType.Tag)) = "ASTOP" Or Trim(UCase(TxtType.Tag)) = "ASTPR" Then
                        DTTemp = cmain.FGetDatTable("Select Count(*) As Cnt From AssetTransaction Where Asset='" & AgL.XNull(DTMaster.Rows(BMBMaster.Position).Item("Asset")) & "' And V_Type In ('ASTSL','ASTAP')", AgL.GCn)
                    ElseIf Trim(UCase(TxtType.Tag)) = "ASTAP" Then
                        DTTemp = cmain.FGetDatTable("Select Count(*) As Cnt From AssetTransaction Where Asset='" & AgL.XNull(DTMaster.Rows(BMBMaster.Position).Item("Asset")) & "' And V_Type In ('ASTSL')", AgL.GCn)
                    Else
                        DTTemp = cmain.FGetDatTable("Select 0 As Cnt", AgL.GCn)
                    End If
                    If DTTemp.Rows(0).Item("Cnt") > 0 Then MsgBox("Corresponding Records Exist") : Exit Sub

                    StrDocID = ""
                    StrDocID = AgL.XNull(DTMaster.Rows(BMBMaster.Position).Item("SearchCode"))
                    If Trim(Replace(StrDocID, "0", "")) = "" Then MsgBox(" Invalid " & "DocId.") : Exit Sub

                    BlnTrans = True
                    GCnCmd.Connection = AgL.GCn
                    GCnCmd.Transaction = AgL.GCn.BeginTransaction(IsolationLevel.Serializable)

                    GCnCmd.CommandText = "Delete From AssetTransaction Where DocId='" & (StrDocID) & "'"
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
        Dim DTTemp As New DataTable

        If DTMaster.Rows.Count > 0 Then
            If Trim(UCase(TxtType.Tag)) = "ASTOP" Or Trim(UCase(TxtType.Tag)) = "ASTPR" Then
                DTTemp = cmain.FGetDatTable("Select Count(*) As Cnt From AssetTransaction Where Asset='" & AgL.XNull(DTMaster.Rows(BMBMaster.Position).Item("Asset")) & "' And V_Type In ('ASTSL','ASTAP')", AgL.GCn)
            ElseIf Trim(UCase(TxtType.Tag)) = "ASTAP" Then
                DTTemp = cmain.FGetDatTable("Select Count(*) As Cnt From AssetTransaction Where Asset='" & AgL.XNull(DTMaster.Rows(BMBMaster.Position).Item("Asset")) & "' And V_Type In ('ASTSL')", AgL.GCn)
            Else
                DTTemp = cmain.FGetDatTable("Select 0 As Cnt", AgL.GCn)
            End If
            If DTTemp.Rows(0).Item("Cnt") > 0 Then MsgBox(ClsMain.MsgEditChk) : Topctrl1.FButtonClick(99) : Exit Sub

            TxtType.Enabled = False
            FManageDisplay(False)
            TxtValue.Focus()
        End If
    End Sub
    Private Sub Topctrl1_tbFind() Handles Topctrl1.tbFind
        If DTMaster.Rows.Count <= 0 Then MsgBox(ClsMain.MsgRecNotFnd) : Exit Sub
        Try
            agl.PubFindQry = "Select AT.DocId,VT.Description,AT.V_No As TransactionId,convert(nvarchar(12),AT.V_Date,103) As VDate, " & _
                    "AG.Name As AssetGroup,AM.Name As Asset " & _
                    "From AssetTransaction AT Left Join " & _
                    "AssetMast AM On AT.Asset=AM.DocId Left Join " & _
                    "AssetGroupMast AG On AM.AssetGroup=AG.Code Left Join " & _
                    "Voucher_Type VT On AT.V_Type=VT.V_Type Where AT.Site_Code='" & AgL.PubSiteCode & "' And AT.V_Prefix='" & AgL.PubCompVPrefix & "' "

            agl.PubFindQryOrdBy = "TransactionId"
            'LIPublic.CreateAndSendArr("150,100,100,200,200")
            '*************** common code start *****************
            FrmFind.ShowDialog()
            If agl.PubSearchRow <> "" Then
                CMain.DRFound = DTMaster.Rows.Find(agl.PubSearchRow)
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
        Dim GCnCmd As New SqlClient.SqlCommand
        Dim StrStatus As String = ""

        Try
            If AgL.RequiredField(TxtType, "Type") Then Exit Sub
            If AgL.RequiredField(TxtDate, "Date") Then Exit Sub
            If AgL.RequiredField(TxtAssetGroup, "Asset Group") Then Exit Sub
            If AgL.RequiredField(TxtAssetName, "Asset Id") Then Exit Sub
            If Not Val(TxtValue.Text) > 0 Then MsgBox("Amount Should Be Greater Than Zero.") : Exit Sub

            StrDocID = ""
            If Topctrl1.Mode = "Add" Then
                TxtID.Text = ""
                TxtRecid.Text = ""
                StrDocID = CMain.FGetDoId(TxtID, TxtType.Tag, "AssetTransaction", "V_No", TxtDate.Text)
                TxtRecid.Text = CMain.FGetMaxNo("Select IsNull(Max(Convert(Bigint,AT.V_No)),0)+1 As Mx From AssetTransaction AT Where IsNumeric(AT.V_No)<>0 And V_Prefix='" & TxtID.Tag & "' And Site_Code='" & agl.PubSiteCode & "' And V_Type='" & TxtType.Tag & "' ", Agl.Gcn)
            Else
                StrDocID = Agl.Xnull(DTMaster.Rows(BMBMaster.Position).Item("SearchCode"))
            End If
            If Trim(Replace(StrDocID, "0", "")) = "" Then MsgBox(" Invalid " & "DocId.") : Exit Sub

            BlnTrans = True
            GCnCmd.Connection = Agl.Gcn
            GCnCmd.Transaction = Agl.Gcn.BeginTransaction(IsolationLevel.Serializable)

            If Topctrl1.Mode = "Add" Then
                GCnCmd.CommandText = "Insert Into AssetTransaction(DocId,V_Type,V_No,Recid,V_Prefix,V_Date, " & _
                                     "Asset,Amount,Remark, " & _
                                     "PreparedBy,ModifiedBy,Site_Code," & _
                                     "U_EntDt,U_AE) Values " & _
                                     "('" & (StrDocID) & "','" & TxtType.Tag & "'," & AgL.Chk_Text(TxtID.Text) & "," & AgL.Chk_Text(TxtRecid.Text) & "," & AgL.Chk_Text(TxtID.Tag) & "," & AgL.ConvertDate(TxtDate.Text) & ", " & _
                                     "" & AgL.Chk_Text(TxtAssetName.Tag) & "," & Val(TxtValue.Text) & "," & AgL.Chk_Text(TxtRemark.Text) & ", " & _
                                     "'" & AgL.PubUserName & "','" & AgL.PubUserName & "','" & AgL.PubSiteCode & "'," & _
                                     "'" & Format(AgL.PubLoginDate, "Short Date") & "','" & Microsoft.VisualBasic.Left(Topctrl1.Mode, 1) & "')"
            Else
                GCnCmd.CommandText = "Update AssetTransaction Set "
                GCnCmd.CommandText = GCnCmd.CommandText + "V_Date=" & AgL.ConvertDate(TxtDate.Text) & ", "
                GCnCmd.CommandText = GCnCmd.CommandText + "Asset=" & Agl.Chk_Text(TxtAssetName.Tag) & ", "
                GCnCmd.CommandText = GCnCmd.CommandText + "Amount=" & Val(TxtValue.Text) & ", "
                GCnCmd.CommandText = GCnCmd.CommandText + "Remark=" & AgL.Chk_Text(TxtRemark.Text) & ", "
                GCnCmd.CommandText = GCnCmd.CommandText + "Transfered='N', "
                GCnCmd.CommandText = GCnCmd.CommandText + "ModifiedBy='" & Agl.PubUserName & "', "
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
    Private Sub FrmAssetTransaction_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        Agl.FPaintForm(Me, e, Topctrl1.Height)
    End Sub

    Private Sub TxtDate_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtDate.Validated
        Select Case sender.Name
            Case TxtDate.Name
                TxtDate.Text = AgL.RetDate(TxtDate.Text)
        End Select
    End Sub

    Private Sub TxtAssetGroup_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtAssetGroup.Validating
        Select Case sender.Name
            Case TxtAssetGroup.Name
                TxtAssetName.Text = ""
                TxtAssetName.Tag = ""
            Case TxtType.Name
                If sender.text <> "" Then
                    TxtID.Text = ""
                    TxtRecid.Text = ""
                    StrDocID = CMain.FGetDoId(TxtID, sender.Tag, "AssetTransaction", "V_No", TxtDate.Text)
                    TxtRecid.Text = CMain.FGetMaxNo("Select IsNull(Max(Convert(Bigint,AT.V_No)),0)+1 As Mx From AssetTransaction AT Where IsNumeric(AT.V_No)<>0 And V_Prefix='" & TxtID.Tag & "' And Site_Code='" & AgL.PubSiteCode & "' And V_Type='" & sender.Tag & "' ", AgL.GCn)
                End If
        End Select
    End Sub

    Private Sub TxtAssetName_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtAssetName.Enter
        Dim mQry$
        Select Case sender.name
            Case TxtAssetName.Name
                If Trim(UCase(TxtType.Tag)) = "ASTSL" Or Trim(UCase(TxtType.Tag)) = "ASTAP" Then
                    mQry = "Select	DocId,Name,Asset_Manual_ID As Code " & _
                                "From AssetMast Where AssetGroup='" & TxtAssetGroup.Tag & "' And " & _
                                "Site_Code='" & AgL.PubSiteCode & "' And " & _
                                "DocId In (Select Asset From AssetTransaction  " & _
                                "Where V_Type In ('ASTOP','ASTPR') And Site_Code='" & AgL.PubSiteCode & "' " & _
                                "Group By Asset) And " & _
                                "DocId Not In (Select Asset From AssetTransaction  " & _
                                "Where V_Type In ('ASTSL') And Site_Code='" & AgL.PubSiteCode & "' " & _
                                "Group By Asset) " & _
                                "Order By Name "
                Else
                    mQry = "Select	DocId,Name,Asset_Manual_ID As Code " & _
                                "From AssetMast Where AssetGroup='" & TxtAssetGroup.Tag & "' And " & _
                                "Site_Code='" & AgL.PubSiteCode & "' And " & _
                                "DocId Not In (Select Asset From AssetTransaction " & _
                                "Where V_Type In ('ASTOP','ASTPR','ASTSL','ASTAP') And Site_Code='" & AgL.PubSiteCode & "' " & _
                                "Group By Asset) " & _
                                "Order By Name"
                End If
                TxtAssetName.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)

        End Select
    End Sub
End Class
