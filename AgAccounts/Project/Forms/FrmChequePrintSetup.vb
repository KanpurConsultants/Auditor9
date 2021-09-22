Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.SQLite
Public Class FrmChequePrintSetup
    Private DTMaster As New DataTable()
    Public BMBMaster As BindingManagerBase
    Private LIEvent As ClsEvents
    Private Sub FrmChequePrintSetup_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F2 Or e.KeyCode = Keys.F3 Or e.KeyCode = Keys.F4 Or e.KeyCode = (Keys.F And e.Control) Or e.KeyCode = (Keys.P And e.Control) _
                Or e.KeyCode = (Keys.S And e.Control) Or e.KeyCode = Keys.Escape Or e.KeyCode = Keys.F5 Or e.KeyCode = Keys.F10 _
                Or e.KeyCode = Keys.Home Or e.KeyCode = Keys.PageUp Or e.KeyCode = Keys.PageDown Or e.KeyCode = Keys.End Then
            Topctrl1.TopKey_Down(e)
        End If
    End Sub
    Public Sub FSearchRecord(ByVal StrKeyField As String)
        Try
            If StrKeyField <> "" Then
                CMain.DRFound = DTMaster.Rows.Find(StrKeyField)
                BMBMaster.Position = DTMaster.Rows.IndexOf(CMain.DRFound)
                MoveRec()
            End If
        Catch ex As Exception
        End Try
    End Sub
    Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Topctrl1.FSetParent(Me, StrUPVar, DTUP)
        Topctrl1.SetDisp(True)
    End Sub
    Private Sub FrmChequePrintSetup_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            LIEvent = New ClsEvents(Me)
            Agl.WinSetting(Me, 385, 880, 0, 0)
            FIniMaster()
            MoveRec()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub FIniMaster(Optional ByVal BytDel As Byte = 0, Optional ByVal BytRefresh As Byte = 1)
        Topctrl1.FIniForm(DTMaster, Agl.Gcn, "Select BankCode As SearchCode From ChequePrintSetup ", , , , , BytDel, BytRefresh)
    End Sub
    Private Sub Topctrl1_tbDiscard() Handles Topctrl1.tbDiscard
        FIniMaster(0, 0)
    End Sub
    Public Sub MoveRec()
        Dim DTTemp As New DataTable

        FManageDisplay(True)
        Topctrl1.BlankTextBoxes()
        If DTMaster.Rows.Count > 0 Then
            DTTemp = cmain.FGetDatTable("Select CPS.*, " & _
                                "SG.SubCode,SG.Name As BankName " & _
                                "From ChequePrintSetup CPS " & _
                                "Left Join SubGroup SG On SG.SubCode=CPS.BankCode " & _
                                "Where CPS.BankCode='" & Agl.Xnull(DTMaster.Rows(BMBMaster.Position).Item("SearchCode")) & "'", Agl.Gcn)
            If DTTemp.Rows.Count > 0 Then
                TxtAmount_Left.Text = Agl.VNull(DTTemp.Rows(0).Item("Amount_Left"))
                TxtAmount_Top.Text = Agl.VNull(DTTemp.Rows(0).Item("Amount_Top"))
                TxtAmountInWords_Left.Text = Agl.VNull(DTTemp.Rows(0).Item("AmountInWords_Left"))
                TxtAmountInWords_Top.Text = Agl.VNull(DTTemp.Rows(0).Item("AmountInWords_Top"))
                TxtAuthorizedSg.Text = Agl.Xnull(DTTemp.Rows(0).Item("AuthorizedBy"))
                TxtAuthorizedSg_Left.Text = Agl.VNull(DTTemp.Rows(0).Item("Authorized_Left"))
                TxtAuthorizedSg_Top.Text = Agl.VNull(DTTemp.Rows(0).Item("Authorized_Top"))

                TxtBankName.Text = Agl.Xnull(DTTemp.Rows(0).Item("BankName"))
                TxtBankName.Tag = Agl.Xnull(DTTemp.Rows(0).Item("BankCode"))

                TxtChqDt_Left.Text = Agl.VNull(DTTemp.Rows(0).Item("ChequeDate_Left"))
                TxtChqDt_Top.Text = Agl.VNull(DTTemp.Rows(0).Item("ChequeDate_Top"))
                TxtCompanyName.Text = Agl.Xnull(DTTemp.Rows(0).Item("CompanyName"))
                TxtCompanyName_Left.Text = Agl.VNull(DTTemp.Rows(0).Item("CompanyName_Left"))
                TxtCompanyName_Top.Text = Agl.VNull(DTTemp.Rows(0).Item("CompanyName_Top"))
                TxtFavourOf_Left.Text = Agl.VNull(DTTemp.Rows(0).Item("FavourOf_Left"))
                TxtFavourOf_Top.Text = Agl.VNull(DTTemp.Rows(0).Item("FavourOf_Top"))
                TxtPaperSizeName.Text = Agl.Xnull(DTTemp.Rows(0).Item("PaperSizeName"))
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
            Case TxtBankName.Name
                If e.KeyCode = Keys.Delete Then
                    sender.Text = "" : sender.Tag = ""
                End If
        End Select
    End Sub
    Public Sub FTxtKeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        '======== Write Your Code Below =============
        Select Case sender.Name
            Case TxtBankName.Name
                FHP_BankCode(e, sender)
            Case TxtAmount_Left.Name, TxtAmount_Top.Name, TxtAmountInWords_Left.Name, TxtAmountInWords_Top.Name, _
                 TxtAuthorizedSg_Left.Name, TxtAuthorizedSg_Top.Name, _
                 TxtChqDt_Left.Name, TxtChqDt_Top.Name, TxtCompanyName_Left.Name, TxtCompanyName_Top.Name, _
                 TxtFavourOf_Left.Name, TxtFavourOf_Top.Name

                CMain.NumPress(sender, e, 10, 2, False)
        End Select
    End Sub
    Private Sub FManageDisplay(ByVal BlnEnb As Boolean)
        '================
    End Sub
    Private Sub Topctrl1_tbAdd() Handles Topctrl1.tbAdd
        FManageDisplay(True)
        TxtBankName.Focus()
    End Sub
    Private Sub FHP_BankCode(ByRef e As System.Windows.Forms.KeyPressEventArgs, ByVal Txt As TextBox)
        Dim DTMain As New DataTable
        Dim FRH As DMHelpGrid.FrmHelpGrid
        Dim StrSendText As String, StrPrvText As String
        StrPrvText = Txt.Text
        StrSendText = Cmain.FSendText(Txt, e.KeyChar)
        DTMain = AgL.FillData("Select SG.SubCode,SG.Name,SG.ManualCode From SubGroup SG Where SG.Nature In ('Bank') Order by SG.Name", AgL.GCn).Tables(0)

        FRH = New DMHelpGrid.FrmHelpGrid(New DataView(DTMain), StrSendText, 300, 380, (Top + Txt.Top) + 85, Left + Txt.Left + 3)
        FRH.FFormatColumn(0, , 0, , False)
        FRH.FFormatColumn(1, "Name", 200, DataGridViewContentAlignment.MiddleLeft)
        FRH.FFormatColumn(2, "Code", 100, DataGridViewContentAlignment.MiddleLeft)

        FRH.StartPosition = FormStartPosition.CenterScreen
        FRH.ShowDialog()
        Txt.Text = StrPrvText
        If FRH.BytBtnValue = 0 Then
            If Not FRH.DRReturn.Equals(Nothing) Then
                Txt.Text = FRH.DRReturn.Item(1)
                Txt.Tag = FRH.DRReturn.Item(0)
            End If
        End If
        FRH = Nothing
        e.KeyChar = ""
    End Sub
    Private Sub Topctrl1_tbDel() Handles Topctrl1.tbDel
        Dim BlnTrans As Boolean = False
        Dim GCnCmd As New Object

        Try
            If DTMaster.Rows.Count > 0 Then
                If MsgBox(" Delete Conflict ", MsgBoxStyle.YesNo) = vbYes Then
                    StrDocID = ""
                    StrDocID = AgL.XNull(DTMaster.Rows(BMBMaster.Position).Item("SearchCode"))
                    If Trim(StrDocID) = "" Then MsgBox(" Invalid " + " DocID.")
                    BlnTrans = True
                    GCnCmd = AgL.GCn.CreateCommand

                    GCnCmd.Transaction = AgL.GCn.BeginTransaction(IsolationLevel.Serializable)
                    GCnCmd.CommandText = "Delete From ChequePrintSetup Where BankCode='" & StrDocID & "'"

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
            FManageDisplay(True)
            TxtBankName.Focus()
        End If
    End Sub
    Private Sub Topctrl1_tbFind() Handles Topctrl1.tbFind
        If DTMaster.Rows.Count <= 0 Then MsgBox(ClsMain.MsgRecNotFnd + " To Search.") : Exit Sub
        Try
            AgL.PubFindQry = "Select CPS.BankCode,SG.Name As [Bank Name] " &
                          "From ChequePrintSetup CPS  " &
                          "Left Join SubGroup SG On CPS.BankCode=SG.SubCode  "
            AgL.PubFindQryOrdBy = "[Bank Name]"
            'LIPublic.CreateAndSendArr("400")
            '*************** common code start *****************
            Dim Frmbj As AgLibrary.FrmFind = New AgLibrary.FrmFind(AgL.PubFindQry, Me.Text & " Find", AgL)
            Frmbj.ShowDialog()
            AgL.PubSearchRow = Frmbj.DGL1.Item(0, Frmbj.DGL1.CurrentRow.Index).Value.ToString

            FSearchRecord(AgL.PubSearchRow)
            '*************** common code end  *****************
        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try
    End Sub
    Private Sub Topctrl1_tbSave() Handles Topctrl1.tbSave
        Dim BlnTrans As Boolean = False
        Dim GCnCmd As New Object

        Try
            If AgL.RequiredField(TxtBankName, "Bank Name") Then Exit Sub

            BlnTrans = True
            GCnCmd = AgL.GCn.CreateCommand
            GCnCmd.Transaction = Agl.Gcn.BeginTransaction(IsolationLevel.Serializable)

            GCnCmd.CommandText = "Delete From ChequePrintSetup Where BankCode='" & TxtBankName.Tag & "'"
            GCnCmd.ExecuteNonQuery()

            GCnCmd.CommandText = "Insert Into ChequePrintSetup(BankCode,Amount_Left,Amount_Top,AmountInWords_Left, "
            GCnCmd.CommandText += "AmountInWords_Top,AuthorizedBy,Authorized_Left,Authorized_Top,"
            GCnCmd.CommandText += "ChequeDate_Left,ChequeDate_Top,CompanyName,CompanyName_Left,"
            GCnCmd.CommandText += "CompanyName_Top,FavourOf_Left,FavourOf_Top,PaperSizeName, "
            GCnCmd.CommandText += "Site_Code,PreparedBy,U_EntDt,U_AE) Values"
            GCnCmd.CommandText += "('" & TxtBankName.Tag & "',"
            GCnCmd.CommandText += "" & Val(TxtAmount_Left.Text) & ","
            GCnCmd.CommandText += "" & Val(TxtAmount_Top.Text) & ","
            GCnCmd.CommandText += "" & Val(TxtAmountInWords_Left.Text) & ","
            GCnCmd.CommandText += "" & Val(TxtAmountInWords_Top.Text) & ","
            GCnCmd.CommandText += "" & AgL.Chk_Text(TxtAuthorizedSg.Text) & ","
            GCnCmd.CommandText += "" & Val(TxtAuthorizedSg_Left.Text) & ","
            GCnCmd.CommandText += "" & Val(TxtAuthorizedSg_Top.Text) & ","
            GCnCmd.CommandText += "" & Val(TxtChqDt_Left.Text) & ","
            GCnCmd.CommandText += "" & Val(TxtChqDt_Top.Text) & ","
            GCnCmd.CommandText += "" & AgL.Chk_Text(TxtCompanyName.Text) & ","
            GCnCmd.CommandText += "" & Val(TxtCompanyName_Left.Text) & ","
            GCnCmd.CommandText += "" & Val(TxtCompanyName_Top.Text) & ","
            GCnCmd.CommandText += "" & Val(TxtFavourOf_Left.Text) & ","
            GCnCmd.CommandText += "" & Val(TxtFavourOf_Top.Text) & ","
            GCnCmd.CommandText += "" & AgL.Chk_Text(TxtPaperSizeName.Text) & ","
            GCnCmd.CommandText += "'" & agl.PubSiteCode & "','" & Agl.PubUserName & "','" & Agl.PubLoginDate & "' , "
            GCnCmd.CommandText += "'" & Microsoft.VisualBasic.Left(Topctrl1.Mode, 1) & "')"

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
    Private Sub FrmChequePrintSetup_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        Agl.FPaintForm(Me, e, Topctrl1.Height)
    End Sub
    Private Sub BtnCopy_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnCopy.Click
        Dim DTMain As New DataTable
        Dim StrSql As String = ""
        Dim FRH As DMHelpGrid.FrmHelpGrid
        If UCase(Topctrl1.Mode) <> "ADD" Then Exit Sub
        StrSql = "Select SG.SubCode,SG.Name,"
        StrSql += "Amount_Left,Amount_Top,Amount_left,"
        StrSql += "AmountInWords_left, AmountInWords_Top,"
        StrSql += "AuthorizedBy,Authorized_Left,Authorized_Top,"
        StrSql += "ChequeDate_Left,ChequeDate_Top,"
        StrSql += "CompanyName,CompanyName_Left,CompanyName_Top,"
        StrSql += "Favourof_Left, Favourof_Top, PaperSizeName "
        StrSql += "From ChequePrintSetup CPS "
        StrSql += "Left Join SubGroup SG On SG.SubCode=CPS.BankCode "
        StrSql += "Where CPS.Site_Code ='" & agl.PubSiteCode & "'"
        DTMain = cmain.FGetDatTable(StrSql, Agl.Gcn)
        FRH = New DMHelpGrid.FrmHelpGrid(New DataView(DTMain), "", 300, 380)
        FRH.FFormatColumn(0, , 0, , False)
        FRH.FFormatColumn(1, "Name", 300, DataGridViewContentAlignment.MiddleLeft)
        FRH.FFormatColumn(2, , 0, , False)
        FRH.FFormatColumn(3, , 0, , False)
        FRH.FFormatColumn(4, , 0, , False)
        FRH.FFormatColumn(5, , 0, , False)
        FRH.FFormatColumn(6, , 0, , False)
        FRH.FFormatColumn(7, , 0, , False)
        FRH.FFormatColumn(8, , 0, , False)
        FRH.FFormatColumn(9, , 0, , False)
        FRH.FFormatColumn(10, , 0, , False)
        FRH.FFormatColumn(11, , 0, , False)
        FRH.FFormatColumn(12, , 0, , False)
        FRH.FFormatColumn(13, , 0, , False)
        FRH.FFormatColumn(14, , 0, , False)
        FRH.FFormatColumn(15, , 0, , False)
        FRH.FFormatColumn(16, , 0, , False)
        FRH.FFormatColumn(17, , 0, , False)


        FRH.StartPosition = FormStartPosition.CenterScreen
        FRH.ShowDialog()
        If FRH.BytBtnValue = 0 Then
            If Not FRH.DRReturn.Equals(Nothing) Then
                TxtAmount_Left.Text = Agl.VNull(DTMain.Rows(0).Item("Amount_Left"))
                TxtAmount_Top.Text = Agl.VNull(DTMain.Rows(0).Item("Amount_Top"))
                TxtAmountInWords_Left.Text = Agl.VNull(DTMain.Rows(0).Item("AmountInWords_Left"))
                TxtAmountInWords_Top.Text = Agl.VNull(DTMain.Rows(0).Item("AmountInWords_Top"))
                TxtAuthorizedSg.Text = Agl.Xnull(DTMain.Rows(0).Item("AuthorizedBy"))
                TxtAuthorizedSg_Left.Text = Agl.VNull(DTMain.Rows(0).Item("Authorized_Left"))
                TxtAuthorizedSg_Top.Text = Agl.VNull(DTMain.Rows(0).Item("Authorized_Top"))


                TxtChqDt_Left.Text = Agl.VNull(DTMain.Rows(0).Item("ChequeDate_Left"))
                TxtChqDt_Top.Text = Agl.VNull(DTMain.Rows(0).Item("ChequeDate_Top"))
                TxtCompanyName.Text = Agl.Xnull(DTMain.Rows(0).Item("CompanyName"))
                TxtCompanyName_Left.Text = Agl.VNull(DTMain.Rows(0).Item("CompanyName_Left"))
                TxtCompanyName_Top.Text = Agl.VNull(DTMain.Rows(0).Item("CompanyName_Top"))
                TxtFavourOf_Left.Text = Agl.VNull(DTMain.Rows(0).Item("FavourOf_Left"))
                TxtFavourOf_Top.Text = Agl.VNull(DTMain.Rows(0).Item("FavourOf_Top"))
                TxtPaperSizeName.Text = Agl.Xnull(DTMain.Rows(0).Item("PaperSizeName"))
            End If
        End If
    End Sub
End Class