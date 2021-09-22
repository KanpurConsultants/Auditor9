Imports System.Data.SQLite
Public Class FrmChequeMast
    Private Const GSNo As Byte = 0
    Private Const GChequeNo As Byte = 1
    Private Const GDate As Byte = 2
    Private Const GStatus As Byte = 3

    Private DTMaster As New DataTable()
    Public BMBMaster As BindingManagerBase
    Public WithEvents FGMain As New AgControls.AgDataGrid
    Private LIEvent As ClsEvents
    Public SVTMain As ClsStructure.VoucherType
    Private DTStruct As New DataTable
    Private Sub FrmGodownTransfer_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F2 Or e.KeyCode = Keys.F3 Or e.KeyCode = Keys.F4 Or e.KeyCode = (Keys.F And e.Control) Or e.KeyCode = (Keys.P And e.Control) _
        Or e.KeyCode = (Keys.S And e.Control) Or e.KeyCode = Keys.Escape Or e.KeyCode = Keys.F5 Or e.KeyCode = Keys.F10 _
        Or e.KeyCode = Keys.Home Or e.KeyCode = Keys.PageUp Or e.KeyCode = Keys.PageDown Or e.KeyCode = Keys.End Then
            Topctrl1.TopKey_Down(e)
        End If
    End Sub
    Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable)
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        'Add any initialization after the InitializeComponent() call.
        Topctrl1.FSetParent(Me, StrUPVar, DTUP)
        Topctrl1.SetDisp(True)
    End Sub
    Private Sub FrmSaleOrder_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            LIEvent = New ClsEvents(Me)
            AgL.WinSetting(Me, 585, 891, 0, 0)
            AgL.GridDesign(FGMain)
            FIniMaster()
            IniGrid()
            MoveRec()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub FIniMaster(Optional ByVal BytDel As Byte = 0, Optional ByVal BytRefresh As Byte = 1)
        Topctrl1.FIniForm(DTMaster, AgL.GCn, "Select (CM.BCode+'_'+CM.ChequeNo) As SearchCode From ChequeMast CM ", , , , , BytDel, BytRefresh)
    End Sub
    Private Sub Topctrl1_tbDiscard() Handles Topctrl1.tbDiscard
        FIniMaster(0, 0)
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
        AgCl.AddAgTextColumn(FGMain, "SNo", 42, 5, "S.No.", True, True, False)
        AgCl.AddAgTextColumn(FGMain, "Cheque No.", 150, 5, "Cheque No.", True, True, False)
        AgCl.AddAgTextColumn(FGMain, "Date", 120, 0, "Rec. Date", True, True, False)
        AgCl.AddAgTextColumn(FGMain, "Status", 200, 100, "Status", True, True, False)

        FGMain.AllowUserToAddRows = False
        FGMain.Anchor = (AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Bottom)
        AgL.FSetSNo(FGMain, GSNo)
        FGMain.TabIndex = PnlMain.TabIndex
    End Sub
    Public Sub MoveRec()

        Dim DTTemp As New DataTable

        BtnChequeToEdit.Enabled = False
        FGMain.Rows.Clear()
        LblTotalForm.Text = ""
        Topctrl1.BlankTextBoxes()
        If DTMaster.Rows.Count > 0 Then

            DTTemp = AgL.FillData("SELECT CM.V_Date,CM.BCode,SG.Name AS BankName, CM.ChequeNo,CM.Status  FROM ChequeMast  CM  " &
                    "LEFT JOIN SubGroup SG ON SG.SubCode =CM.BCode  " &
                    "Where CM.BCode+'_'+CM.ChequeNo='" & AgL.XNull(DTMaster.Rows(BMBMaster.Position).Item("SearchCode")) & "'", AgL.GCn).Tables(0)


            If DTTemp.Rows.Count > 0 Then
                TxtV_Date.Text = AgL.XNull(DTTemp.Rows(0).Item("V_Date"))
                TxtBankName.Tag = AgL.XNull(DTTemp.Rows(0).Item("BCode"))
                TxtBankName.Text = AgL.XNull(DTTemp.Rows(0).Item("BankName"))
                TxtChequeSrNo.Text = AgL.XNull(DTTemp.Rows(0).Item("ChequeNo"))
                TxtChequeStatus.Text = Trim(AgL.XNull(DTTemp.Rows(0).Item("Status")))
                TxtChequeStatus.Tag = Trim(AgL.XNull(DTTemp.Rows(0).Item("Status")))

            End If
        End If
        Topctrl1.FSetDispRec(BMBMaster)

        DTTemp = Nothing
    End Sub
    Private Sub FHP_ChequeStatus(ByRef e As System.Windows.Forms.KeyPressEventArgs, ByVal Txt As TextBox)
        Dim DTMain As New DataTable
        Dim FRH As DMHelpGrid.FrmHelpGrid
        Dim StrSendText As String, StrPrvText As String
        Dim StrSQL As String

        StrPrvText = Txt.Text
        StrSendText = CMain.FSendText(Txt, e.KeyChar)
        StrSQL = "Declare @TmpTable Table (Code NVarChar(20),Name NVarChar(20)) "
        StrSQL += "Insert Into @TmpTable Values('Stock','Stock') "
        StrSQL += "Insert Into @TmpTable Values('Damage','Damage') "
        StrSQL += "Insert Into @TmpTable Values('Return','Return') "
        StrSQL += "Insert Into @TmpTable Values('Issue','Issue') "
        StrSQL += "Select Code,Name From @TmpTable Order By Name "

        DTMain = CMain.FGetDatTable(StrSQL, AgL.GCn)

        FRH = New DMHelpGrid.FrmHelpGrid(New DataView(DTMain), StrSendText, 200, 220, (Top + Txt.Top) + 85, Left + Txt.Left + 3)
        FRH.FFormatColumn(0, , 0, , False)
        FRH.FFormatColumn(1, "Status", 140, DataGridViewContentAlignment.MiddleLeft)

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


    Public Sub FTxtGotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        '======== Write Your Code Below =============	
    End Sub
    Public Sub FTxtKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        '======== Write Your Code Below =============	
        Select Case sender.Name
            Case TxtBankName.Name, TxtChequeStatus.Name
                If e.KeyCode = Keys.Delete Then
                    sender.Text = "" : sender.Tag = ""
                End If
        End Select
    End Sub
    Public Sub FTxtKeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        '======== Write Your Code Below =============	
        Select Case sender.Name
            Case TxtBankName.Name
                FHP_Type(e, sender)
            Case TxtChequeStatus.Name
                FHP_ChequeStatus(e, sender)
            Case TxtChequeNofrom.Name, TxtChequeNoTo.Name
                CMain.NumPress(sender, e, 10, 0, False)
        End Select
    End Sub
    Private Sub FHP_Type(ByRef e As System.Windows.Forms.KeyPressEventArgs, ByVal Txt As TextBox)
        Dim DTMain As New DataTable
        Dim FRH As DMHelpGrid.FrmHelpGrid
        Dim StrSendText As String
        StrSendText = CMain.FSendText(Txt, e.KeyChar)
        DTMain = AgL.FillData("SELECT SubCode,Name FROM SubGroup WHERE Nature ='BANK'  And SiteList Like '%|" & AgL.PubSiteCode & "|%' Order By  Name", AgL.GCn).Tables(0)

        FRH = New DMHelpGrid.FrmHelpGrid(New DataView(DTMain), StrSendText, 200, 280, (Top + Txt.Top) + 85, Left + Txt.Left + 3)
        FRH.FFormatColumn(0, , 0, , False)
        FRH.FFormatColumn(1, "Name", 200, DataGridViewContentAlignment.MiddleLeft)
        FRH.ShowDialog()

        If FRH.BytBtnValue = 0 Then
            If Not FRH.DRReturn.Equals(Nothing) Then
                Txt.Text = FRH.DRReturn.Item(1)
                Txt.Tag = FRH.DRReturn.Item(0)
                TxtChequeSrNo.Text = ""
            End If
        End If
        FRH = Nothing
        e.KeyChar = ""
    End Sub

    Private Sub Topctrl1_tbAdd() Handles Topctrl1.tbAdd
        TxtV_Date.Text = AgL.PubLoginDate
        LblTotalForm.Text = ""
        TxtBankName.Focus()
        TxtBankName.Enabled = True
        TxtV_Date.Enabled = True
        TxtChequeNoTo.Enabled = True
        TxtChequeSrNo.Enabled = True
        TxtChequeSrNo.Enabled = True
        TxtChequeStatus.Enabled = False
        TxtChequeStatus.Text = "Stock"

    End Sub
    Private Sub Topctrl1_tbDel() Handles Topctrl1.tbDel
        Dim BlnTrans As Boolean = False
        Dim GCnCmd As New Object
        Dim StrChequeNo() As String
        Dim I As Integer
        Try
            If DTMaster.Rows.Count > 0 Then
                StrDocID = ""
                StrDocID = AgL.XNull(DTMaster.Rows(BMBMaster.Position).Item("SearchCode"))
                If Trim(Replace(StrDocID, "0", "")) = "" Then MsgBox(" Invalid " + " Docid.") : Exit Sub
                StrChequeNo = Split(FHPGD_Show_Multiple(TxtBankName.Tag), ",")

                If StrChequeNo(0) <> "" Then
                    If MsgBox("Are You Sure? You Want To Delete Selected Records.", MsgBoxStyle.YesNo) = vbYes Then
                        BlnTrans = True
                        GCnCmd = AgL.GCn.CreateCommand
                        GCnCmd.Transaction = AgL.GCn.BeginTransaction(IsolationLevel.Serializable)
                        For I = 0 To StrChequeNo.Length - 1
                            GCnCmd.CommandText = "Select Count(*) As Cnt From Ledger_Temp LM Left Join Voucher_Type VT on VT.V_Type=LM.V_Type Where VT.Category='PMT' and  IfNull(LM.ContraSub,'')= '" & TxtBankName.Tag & "'  And IfNull(LM.Chq_No,'')=" & StrChequeNo(I) & " "
                            If AgL.VNull(GCnCmd.ExecuteScalar()) > 0 Then
                                MsgBox("Cheque No. " & StrChequeNo(I) & " Is Issued.You Can Not Delete It.")
                            Else
                                GCnCmd.CommandText = "Delete From ChequeMast where BCode='" & Trim(TxtBankName.Tag) & "' And ChequeNo=" & StrChequeNo(I) & " "
                                GCnCmd.ExecuteNonQuery()
                            End If
                        Next
                        GCnCmd.Transaction.Commit()
                        BlnTrans = False
                        FIniMaster(1)
                        MoveRec()
                    End If
                End If
            End If
        Catch Ex As Exception
            If BlnTrans = True Then GCnCmd.Transaction.Rollback()
            MsgBox(Ex.Message)
        End Try
    End Sub
    Private Function FHPGD_ChequeToEdit(ByVal StrBankCode As String) As String
        Dim FRH_Multiple As DMHelpGrid.FrmHelpGrid_Multi
        Dim StrSendText As String
        Dim StrRtn As String = ""

        StrSendText = ""
        FRH_Multiple = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable(
                      "Select 'o' As Tick,CM.ChequeNo As Code,CM.ChequeNo,CM.V_Date,CM.Status From ChequeMast CM  Where CM.BCode='" & StrBankCode & "' Order By ChequeNo",
                      AgL.GCn)), "", 300, 460, , , False)
        FRH_Multiple.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple.FFormatColumn(1, , 0, , False)
        FRH_Multiple.FFormatColumn(2, "Cheque No.", 140, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(3, "Date", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(4, "Status", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.StartPosition = FormStartPosition.CenterScreen
        FRH_Multiple.ShowDialog()

        If FRH_Multiple.BytBtnValue = 0 Then
            StrRtn = FRH_Multiple.FFetchData(1, "'", "'", ",", True)
        End If
        FHPGD_ChequeToEdit = StrRtn
        FRH_Multiple = Nothing
    End Function
    Private Function FHPGD_Show_Multiple(ByVal StrBankCode As String) As String
        Dim FRH_Multiple As DMHelpGrid.FrmHelpGrid_Multi
        Dim StrSendText As String
        Dim StrRtn As String = ""

        StrSendText = ""
        FRH_Multiple = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CMain.FGetDatTable(
                      "Select 'o' As Tick,CM.ChequeNo As Code,CM.ChequeNo,CM.V_Date,CM.Status From ChequeMast CM  Where CM.BCode='" & StrBankCode & "' Order By ChequeNo",
                      AgL.GCn)), "", 300, 460, , , False)
        FRH_Multiple.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple.FFormatColumn(1, , 0, , False)
        FRH_Multiple.FFormatColumn(2, "Cheque No.", 140, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(3, "Date", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(4, "Status", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.StartPosition = FormStartPosition.CenterScreen
        FRH_Multiple.ShowDialog()

        If FRH_Multiple.BytBtnValue = 0 Then
            StrRtn = FRH_Multiple.FFetchData(1, "'", "'", ",", True)
        End If
        FHPGD_Show_Multiple = StrRtn
        FRH_Multiple = Nothing
    End Function
    Private Sub Topctrl1_tbEdit() Handles Topctrl1.tbEdit
        If DTMaster.Rows.Count > 0 Then
            BtnChequeToEdit.Enabled = True
            TxtBankName.Enabled = False
            TxtChequeNoTo.Enabled = False
            TxtChequeNofrom.Enabled = False
            TxtChequeSrNo.Enabled = False
            TxtChequeStatus.Focus()
        End If
    End Sub
    Private Sub Topctrl1_tbExit() Handles Topctrl1.tbExit
        Me.Close()
    End Sub
    Private Sub Topctrl1_tbFind() Handles Topctrl1.tbFind
        If DTMaster.Rows.Count <= 0 Then MsgBox(ClsMain.MsgRecNotFnd + " To Search.") : Exit Sub
        Try
            AgL.PubFindQry = "SELECT  CM.BCode+'_'+CM.ChequeNo as Code, Cm.V_Date,SG.Name AS BankName,CM.ChequeNo,CM.Status  FROM ChequeMast  CM " &
                                 " LEFT JOIN SubGroup SG ON SG.SubCode =CM.BCode  "


            AgL.PubFindQryOrdBy = "[ChequeNo]"
            'LIPublic.CreateAndSendArr("100,200,100,100")
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
        Dim FrmObj As Form
        FrmObj = New FrmReportLayout("FormReceive", "Form Report", 3)
        FrmObj.MdiParent = Me.MdiParent
        FrmObj.Show()
    End Sub
    Private Sub Topctrl1_tbSave() Handles Topctrl1.tbSave
        Dim GCnCmd As New Object
        Dim J As Integer
        Dim StrChequeNo As String
        Dim BlnTrans As Boolean = False
        Dim IntSNo As Integer

        Try
            If AgL.RequiredField(TxtBankName, "Bank Name") Then Exit Sub
            If AgL.RequiredField(TxtV_Date, "Date") Then Exit Sub
            If AgL.RequiredField(TxtChequeSrNo, "Sr No") Then Exit Sub
            If AgL.RequiredField(TxtChequeStatus, "Status") Then Exit Sub

            If Topctrl1.Mode = "Add" Then
                If Not Val(TxtChequeNofrom.Text) > 0 Then MsgBox("Please Enter Valid Cheque No.") : Exit Sub
                If Not Val(TxtChequeNoTo.Text) > 0 Then MsgBox("Please Enter Valid Cheque To No.") : Exit Sub
            Else
                If Not FGMain.Rows.Count > 0 Then MsgBox("Please Select Cheque From Cheque To Edit.") : Exit Sub
            End If

            StrDocID = ""
            IntSNo = Val(TxtChequeNofrom.Text)
            For J = 1 To (Val(TxtChequeNoTo.Text) - Val(TxtChequeNofrom.Text)) + 1
                StrChequeNo = Trim(TxtChequeSrNo.Text) & IntSNo
                If CMain.DuplicacyChecking("Select Count(BCode+ChequeNo) As Cnt From ChequeMast Where BCode='" & Trim(TxtBankName.Tag) & "' And ChequeNo='" & StrChequeNo & "' ", "Cheque No. Already Exists.") Then TxtChequeSrNo.Focus() : Exit Sub
                IntSNo = IntSNo + 1
            Next
            BlnTrans = True
            GCnCmd = AgL.GCn.CreateCommand
            GCnCmd.Transaction = AgL.GCn.BeginTransaction(IsolationLevel.Serializable)

            If Topctrl1.Mode = "Add" Then
                IntSNo = Val(TxtChequeNofrom.Text)
                For J = 1 To (Val(TxtChequeNoTo.Text) - Val(TxtChequeNofrom.Text)) + 1
                    StrChequeNo = Trim(TxtChequeSrNo.Text) & IntSNo
                    StrDocID = TxtBankName.Tag & "_" & StrChequeNo
                    GCnCmd.CommandText = "Insert Into ChequeMast(BCode,ChequeNo,V_Date,Status,U_Name,U_EntDt,U_AE)  " &
                                        " Values(" & AgL.Chk_Text(TxtBankName.Tag) & ",'" & StrChequeNo & "'," & AgL.Chk_Text(CDate(TxtV_Date.Text).ToString("s")) & ",'Stock', '" & AgL.PubUserName & "','" & Format(AgL.PubLoginDate, "Short Date") & "','" & Microsoft.VisualBasic.Left(Topctrl1.Mode, 1) & "' )"
                    GCnCmd.ExecuteNonQuery()
                    IntSNo = IntSNo + 1
                Next
            Else

                StrDocID = TxtBankName.Tag & "_" & TxtChequeSrNo.Text
                For J = 0 To FGMain.Rows.Count - 1
                    GCnCmd.CommandText = "Update ChequeMast Set "
                    GCnCmd.CommandText = GCnCmd.CommandText + "V_Date=" & AgL.Chk_Text(CDate(TxtV_Date.Text).ToString("s")) & ", "
                    GCnCmd.CommandText = GCnCmd.CommandText + "status='" & Trim(TxtChequeStatus.Text) & "', "
                    GCnCmd.CommandText = GCnCmd.CommandText + "Transfered='N', "
                    GCnCmd.CommandText = GCnCmd.CommandText + "U_Name='" & AgL.PubUserName & "', "
                    GCnCmd.CommandText = GCnCmd.CommandText + "U_EntDt='" & Format(AgL.PubLoginDate, "Short Date") & "', "
                    GCnCmd.CommandText = GCnCmd.CommandText + "U_AE='" & Microsoft.VisualBasic.Left(Topctrl1.Mode, 1) & "' "
                    GCnCmd.CommandText = GCnCmd.CommandText + "Where BCode='" & TxtBankName.Tag & "' And ChequeNo='" & FGMain(GChequeNo, J).Value & "' "
                    GCnCmd.ExecuteNonQuery()
                Next
            End If

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
    Private Sub FrmSaleOrder_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub
    Private Sub TxtOrdDate_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtV_Date.Validated, TxtChequeNofrom.Validated, TxtChequeNoTo.Validated
        Select Case sender.name
            Case TxtV_Date.Name
                sender.Text = AgL.RetDate(sender.Text)
            Case TxtChequeNofrom.Name, TxtChequeNoTo.Name
                LblTotalForm.Text = "Total " & Trim((Val(TxtChequeNoTo.Text) + 1) - Val(TxtChequeNofrom.Text))
        End Select
    End Sub

    Private Sub BtnChequeToEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnChequeToEdit.Click
        Dim StrChequeNo As String, StrSQL As String
        Dim DTTemp As DataTable, I As Integer
        TxtChequeStatus.Enabled = True
        FGMain.Rows.Clear()
        StrChequeNo = FHPGD_ChequeToEdit(TxtBankName.Tag)
        If StrChequeNo = "" Then Exit Sub
        StrSQL = " SELECT CM.V_Date,CM.ChequeNo,CM.Status  FROM ChequeMast  CM "
        StrSQL += "Where CM.ChequeNo In (" & StrChequeNo & ") Order By CM.ChequeNo"
        DTTemp = CMain.FGetDatTable(StrSQL, AgL.GCn)
        For I = 0 To DTTemp.Rows.Count - 1
            FGMain.Rows.Add()
            FGMain(GSNo, I).Value = Trim(I + 1)
            FGMain(GChequeNo, I).Value = AgL.XNull(DTTemp.Rows(I).Item("ChequeNo"))
            FGMain(GDate, I).Value = AgL.XNull(DTTemp.Rows(I).Item("V_Date"))
            FGMain(GStatus, I).Value = AgL.XNull(DTTemp.Rows(I).Item("Status"))
        Next
        DTTemp.Dispose()

    End Sub

    Private Sub Label14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label14.Click


    End Sub
End Class