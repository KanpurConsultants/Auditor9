Imports CrystalDecisions.CrystalReports.Engine
Public Class frmCountryMast
    Private DTMaster As New DataTable()
    Public BMBMaster As BindingManagerBase
    Private LIEvent As ClsEvents
    Dim FrmFind As New AgLibrary.FrmFind(Agl)

    Private Sub frmCountryMast_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

    Private Sub frmCountryMast_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
        Topctrl1.FIniForm(DTMaster, Agl.Gcn, "Select Code As SearchCode, Name From Country", True, txtCountry, "SearchCode", "Name", BytDel, BytRefresh)
    End Sub
    Private Sub Topctrl1_tbDiscard() Handles Topctrl1.tbDiscard
        FIniMaster(0, 0)
    End Sub

    Public Sub MoveRec()
        Dim ADTemp As SqlClient.SqlDataAdapter
        Dim DTTemp As New DataTable

        Topctrl1.BlankTextBoxes()
        If DTMaster.Rows.Count > 0 Then
            ADTemp = New SqlClient.SqlDataAdapter("Select CT.Code, CT.Name,CT.CurrencyCode,CR.Name As Currency " & _
                          "From Country CT Left Join Currency CR on CR.Code=CT.CurrencyCode " & _
                          "Where CT.Code='" & Agl.Xnull(DTMaster.Rows(BMBMaster.Position).Item("SearchCode")) & "'", Agl.Gcn)
            ADTemp.Fill(DTTemp)
            If DTTemp.Rows.Count > 0 Then
                txtCountry.Text = Agl.Xnull(DTTemp.Rows(0).Item("Name"))
                txtMajor.Text = Agl.Xnull(DTTemp.Rows(0).Item("Currency"))
                txtMajor.Tag = Agl.Xnull(DTTemp.Rows(0).Item("CurrencyCode"))
            End If
        End If
        Topctrl1.FSetDispRec(BMBMaster)
        ADTemp = Nothing
        DTTemp = Nothing
    End Sub

    Public Sub FTxtKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        '======== Write Your Code Below =============
        Select Case sender.Name
            Case txtMajor.Name
                If e.KeyCode = Keys.Delete Then
                    sender.Text = "" : sender.Tag = ""
                End If
        End Select
    End Sub

    Public Sub FTxtKeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        '======== Write Your Code Below =============
        Select Case sender.Name
            Case txtMajor.Name
                FHP_CountryName(e, sender)
        End Select

    End Sub
    Public Sub FTxtGotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        '======== Write Your Code Below =============
    End Sub
    Private Sub FHP_CountryName(ByRef e As System.Windows.Forms.KeyPressEventArgs, ByVal Txt As TextBox)
        Dim DTMain As New DataTable
        Dim FRH As DMHelpGrid.FrmHelpGrid
        Dim StrSendText As String

        StrSendText = Cmain.FSendText(Txt, e.KeyChar)
        Agl.ADMain = New SqlClient.SqlDataAdapter("Select Code, Name from Currency ", Agl.Gcn)
        Agl.ADMain.Fill(DTMain)
        FRH = New DMHelpGrid.FrmHelpGrid(New DataView(DTMain), StrSendText, 200, 280, (Top + Txt.Top) + 85, Left + Txt.Left + 3)
        FRH.FFormatColumn(0, , 0, , False)
        FRH.FFormatColumn(1, "Name", 200, DataGridViewContentAlignment.MiddleLeft)
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
        txtCountry.Focus()
    End Sub

    Private Sub Topctrl1_tbDel() Handles Topctrl1.tbDel
        Dim BlnTrans As Boolean = False
        Dim GCnCmd As New SqlClient.SqlCommand

        Try
            If DTMaster.Rows.Count > 0 Then
                If MsgBox(" Delete Conflict ", MsgBoxStyle.YesNo) = vbYes Then
                    StrDocID = ""
                    StrDocID = Agl.Xnull(DTMaster.Rows(BMBMaster.Position).Item("SearchCode"))
                    If Trim(StrDocID) = "" Then MsgBox(" Invalid " & " DocId.")
                    BlnTrans = True
                    GCnCmd.Connection = Agl.Gcn
                    GCnCmd.Transaction = Agl.Gcn.BeginTransaction(IsolationLevel.Serializable)
                    GCnCmd.CommandText = "Delete From Country Where Code='" & StrDocID & "'"
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
            txtCountry.Focus()
        End If
    End Sub

    Private Sub Topctrl1_tbFind() Handles Topctrl1.tbFind
        If DTMaster.Rows.Count <= 0 Then MsgBox(ClsMain.MsgRecNotFnd) : Exit Sub
        Try
            agl.PubFindQry = "Select CT.Code,CT.Name,CR.Name Currency From Country CT Left Join Currency CR On CR.Code=CT.CurrencyCode "
            agl.PubFindQryOrdBy = "Code"
            'LIPublic.CreateAndSendArr("200,100")
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

    Private Sub Topctrl1_tbPrn() Handles Topctrl1.tbPrn
        Dim rptReg As New ReportDocument
        Dim GCnCmd As New SqlClient.SqlCommand
        Dim ds As New DataSet
        Dim ds1 As New DataSet
        Dim strQry As String

        Try
            Me.Cursor = Cursors.WaitCursor
            agl.PubReportTitle = "Country Master"

            GCnCmd.CommandText = "SELECT     Code, Name FROM         Country"
            If Not DTMaster.Rows.Count > 0 Then
                MsgBox(ClsMain.MsgRecNotFnd & " to Print!!!")
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

            strQry = "SELECT CT.Code,CT.Name,CY.Name as Currency FROM Country CT Left join Currency CY on CY.Code=CT.CurrencyCode"
            Agl.ADMain = New SqlClient.SqlDataAdapter(strQry, Agl.Gcn)
            Agl.ADMain.Fill(ds)

            ds.WriteXmlSchema(Agl.PubReportPath & "\Countrymaster.Xml")
            rptReg.Load(Agl.PubReportPath & "\Countrymaster.rpt")
            rptReg.SetDataSource(ds)
            FormulaSet(rptReg, Me)
            CMain.FShowReport(rptReg, Me.MdiParent, agl.PubReportTitle)
            Me.Cursor = Cursors.Default
        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try

        Me.Cursor = Cursors.Default
    End Sub

    Private Sub Topctrl1_tbSave() Handles Topctrl1.tbSave
        Dim BlnTrans As Boolean = False
        Dim GCnCmd As New SqlClient.SqlCommand
        Dim StrName As String

        Try
            If AgL.RequiredField(txtCountry, "Country") Then Exit Sub
            If AgL.RequiredField(txtMajor, "Currency") Then Exit Sub

            StrName = CMain.FRemoveSpace(txtCountry.Text)
            StrDocID = ""
            If Topctrl1.Mode = "Add" Then
                StrDocID = agl.PubSiteCode + Trim(CMain.FGetMaxNoWithSiteCode("Country", "Code", Agl.Gcn))
            Else
                StrDocID = Agl.Xnull(DTMaster.Rows(BMBMaster.Position).Item("SearchCode"))
            End If
            If Trim(Replace(StrDocID, "0", "")) = "" Then MsgBox(" Invalid " & " DocId.") : Exit Sub

            If CMain.DuplicacyChecking("Select Count(Name) As Cnt From Country CC Where CC.Name='" & StrName & "' And CC.Code<>'" & StrDocID & "'", "Country Already Exists.") Then txtCountry.Focus() : Exit Sub

            BlnTrans = True
            GCnCmd.Connection = Agl.Gcn
            GCnCmd.Transaction = Agl.Gcn.BeginTransaction(IsolationLevel.Serializable)

            If Topctrl1.Mode = "Add" Then
                GCnCmd.CommandText = "Insert Into Country(Code,Name,CurrencyCode,U_Name,U_EntDt,U_AE)" & _
                                     " Values('" & StrDocID & "'," & AgL.Chk_Text(StrName) & "," & AgL.Chk_Text(txtMajor.Tag) & _
                                     ",'" & AgL.PubUserName & "','" & Format(AgL.PubLoginDate, "Short Date") & "','A')"
            Else
                GCnCmd.CommandText = "Update Country Set Name=" & AgL.Chk_Text(StrName) & ""
                GCnCmd.CommandText = GCnCmd.CommandText + ",CurrencyCode=" & AgL.Chk_Text(Trim(txtMajor.Tag)) & ""
                GCnCmd.CommandText = GCnCmd.CommandText + ",Transfered='N'"
                GCnCmd.CommandText = GCnCmd.CommandText + ",U_Name='" & Agl.PubUserName & "'"
                GCnCmd.CommandText = GCnCmd.CommandText + ",U_EntDt='" & Format(Agl.PubLoginDate, "Short Date") & "'"
                GCnCmd.CommandText = GCnCmd.CommandText + ",U_AE='E' "
                GCnCmd.CommandText = GCnCmd.CommandText + " Where Code='" & StrDocID & "'"
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
    Private Sub frmCurrencyMast_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        Agl.FPaintForm(Me, e, Topctrl1.Height)
    End Sub
End Class