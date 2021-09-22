Imports CrystalDecisions.CrystalReports.Engine
Public Class frmCurrencyMast

    Private DTMaster As New DataTable()
    Public BMBMaster As BindingManagerBase
    Private LIEvent As ClsEvents
    Dim FrmFind As New AgLibrary.FrmFind(Agl)

    Private Sub frmCurrencyMast_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

    Private Sub frmCurrencyMast_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
        Topctrl1.FIniForm(DTMaster, Agl.Gcn, "Select Code As SearchCode, Name From Currency", True, txtMajor, "SearchCode", "Name", BytDel, BytRefresh)
    End Sub
    Private Sub Topctrl1_tbDiscard() Handles Topctrl1.tbDiscard
        FIniMaster(0, 0)
    End Sub

    Public Sub MoveRec()
        Dim ADTemp As SqlClient.SqlDataAdapter
        Dim DTTemp As New DataTable

        Topctrl1.BlankTextBoxes()
        If DTMaster.Rows.Count > 0 Then
            ADTemp = New SqlClient.SqlDataAdapter("Select Code, Name, Discription, SmallCurrency From Currency " & _
                          " Where Code='" & Agl.Xnull(DTMaster.Rows(BMBMaster.Position).Item("SearchCode")) & "'", Agl.Gcn)
            ADTemp.Fill(DTTemp)
            If DTTemp.Rows.Count > 0 Then
                txtMajor.Text = Agl.Xnull(DTTemp.Rows(0).Item("Name"))
                txtMinor.Text = Agl.Xnull(DTTemp.Rows(0).Item("SmallCurrency"))
                txtDescription.Text = Agl.Xnull(DTTemp.Rows(0).Item("Discription"))
            End If
        End If
        Topctrl1.FSetDispRec(BMBMaster)
        ADTemp = Nothing
        DTTemp = Nothing
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
        txtMajor.Focus()
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
                    GCnCmd.CommandText = "Delete From Currency Where Code='" & StrDocID & "'"
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
            txtMinor.Focus()
        End If
    End Sub

    Private Sub Topctrl1_tbFind() Handles Topctrl1.tbFind
        If DTMaster.Rows.Count <= 0 Then MsgBox(ClsMain.MsgRecNotFnd) : Exit Sub
        Try
            agl.PubFindQry = "Select Code,Name, SmallCurrency, Discription From Currency"
            agl.PubFindQryOrdBy = "Code"
            'LIPublic.CreateAndSendArr("50,100,200")
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
            agl.PubReportTitle = "Currency Master"

            GCnCmd.CommandText = "SELECT     Code, Name FROM         Currency"
            If Not DTMaster.Rows.Count > 0 Then
                MsgBox(ClsMain.MsgRecNotFnd & " to Print!!!")
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

            strQry = "SELECT     Code, Name, Discription, SmallCurrency FROM         Currency"
            Agl.ADMain = New SqlClient.SqlDataAdapter(strQry, Agl.Gcn)
            Agl.ADMain.Fill(ds)

            ds.WriteXmlSchema(Agl.PubReportPath & "\Currencymaster.Xml")
            rptReg.Load(Agl.PubReportPath & "\Currencymaster.rpt")
            rptReg.SetDataSource(ds)
            FormulaSet(rptReg, Me)
            CMain.FShowReport(rptReg, Me.MdiParent, Me.Name)
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
            If AgL.RequiredField(txtMajor, "Currency") Then Exit Sub
            If AgL.RequiredField(txtMinor, "Lower Currency") Then Exit Sub

            StrName = CMain.FRemoveSpace(txtMajor.Text)
            StrDocID = ""
            If Topctrl1.Mode = "Add" Then
                StrDocID = agl.PubSiteCode + Trim(CMain.FGetMaxNoWithSiteCode("Currency", "Code", Agl.Gcn))
            Else
                StrDocID = Agl.Xnull(DTMaster.Rows(BMBMaster.Position).Item("SearchCode"))
            End If
            If Trim(Replace(StrDocID, "0", "")) = "" Then MsgBox(" Invalid " & " DocId.") : Exit Sub

            If CMain.DuplicacyChecking("Select Count(Name) As Cnt From Currency CC Where CC.Name='" & StrName & "' And CC.Code<>'" & StrDocID & "'", "Currency Already Exists.") Then txtMajor.Focus() : Exit Sub

            BlnTrans = True
            GCnCmd.Connection = Agl.Gcn
            GCnCmd.Transaction = Agl.Gcn.BeginTransaction(IsolationLevel.Serializable)

            If Topctrl1.Mode = "Add" Then
                GCnCmd.CommandText = "Insert Into Currency(Code,Name,Discription,SmallCurrency,U_Name,U_EntDt,U_AE)" & _
                                     " Values('" & StrDocID & "'," & AgL.Chk_Text(StrName) & "," & AgL.Chk_Text(txtDescription.Text) & "," & AgL.Chk_Text(txtMinor.Text) & _
                                     ",'" & AgL.PubUserName & "','" & Format(AgL.PubLoginDate, "Short Date") & "','A')"
            Else
                GCnCmd.CommandText = "Update Currency Set Name=" & AgL.Chk_Text(StrName) & ""
                GCnCmd.CommandText = GCnCmd.CommandText + ",SmallCurrency=" & AgL.Chk_Text(Trim(txtMinor.Text)) & ""
                GCnCmd.CommandText = GCnCmd.CommandText + ",Discription=" & AgL.Chk_Text(Trim(txtDescription.Text)) & ""
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