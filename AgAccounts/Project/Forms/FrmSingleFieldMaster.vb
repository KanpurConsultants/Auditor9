Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.SQLite
Public Class FrmSingleFieldMaster
    Private DTMaster As New DataTable()
    Public BMBMaster As BindingManagerBase
    Private LIEvent As ClsEvents
    Private StrTable As String, StrLabel As String, StrFCode As String, StrFName As String
    Dim BlnHaveSiteList As Boolean = False
    Dim BlnHaveSiteCode As Boolean = False    
    Dim StrReportPath As String
    Dim StrDocId As String = ""
    Dim FrmFindLocal As AgLibrary.frmFind
    Sub New(ByVal FrmMDI As Form, ByVal LIPublicVar As AgLibrary.ClsMain, ByVal StrCaption As String, ByVal StrTableVar As String, ByVal StrLabelVar As String, ByVal StrFCodeVar As String, ByVal StrFNameVar As String, ByVal IntMaxLength As Integer, ByVal StrUPVar As String, ByVal DTUP As DataTable, ByVal BlnHaveSiteListVar As Boolean, ByVal BlnHaveSiteCodeVar As Boolean, Optional ByVal StrReportPathVar As String = "")
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        StrLabel = StrLabelVar
        StrTable = StrTableVar
        StrFCode = StrFCodeVar
        StrFName = StrFNameVar
        BlnHaveSiteList = BlnHaveSiteListVar
        BlnHaveSiteCode = BlnHaveSiteCodeVar
        AgL = LIPublicVar
        StrReportPath = StrReportPathVar
        TxtName.MaxLength = IntMaxLength
        Topctrl1.FSetParent(Me, StrUPVar, DTUP)
        Topctrl1.SetDisp(True)
        Me.Text = StrCaption
    End Sub
    Private Sub FrmSingleFieldMaster_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F2 Or e.KeyCode = Keys.F3 Or e.KeyCode = Keys.F4 Or e.KeyCode = (Keys.F And e.Control) Or e.KeyCode = (Keys.P And e.Control) _
        Or e.KeyCode = (Keys.S And e.Control) Or e.KeyCode = Keys.Escape Or e.KeyCode = Keys.F5 Or e.KeyCode = Keys.F10 _
        Or e.KeyCode = Keys.Home Or e.KeyCode = Keys.PageUp Or e.KeyCode = Keys.PageDown Or e.KeyCode = Keys.End Then
            Topctrl1.TopKey_Down(e)
        End If
    End Sub
    Private Sub FrmSingleFieldMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            LIEvent = New ClsEvents(Me)
            AgL.WinSetting(Me, 258, 891, 0, 0)
            LblName.Text = StrLabel
            FIniMaster()
            MoveRec()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub FIniMaster(Optional ByVal BytDel As Byte = 0, Optional ByVal BytRefresh As Byte = 1)
        If BlnHaveSiteCode Then
            Topctrl1.FIniForm(DTMaster, Agl.GCn, "Select " & StrFCode & " As SearchCode," & StrFName & " As Name From " & StrTable & " where site_code='" & Agl.PubSiteCode & "'", True, TxtName, "SearchCode", "Name", BytDel, BytRefresh)
        Else
            Topctrl1.FIniForm(DTMaster, Agl.GCn, "Select " & StrFCode & " As SearchCode," & StrFName & " As Name From " & StrTable & "", True, TxtName, "SearchCode", "Name", BytDel, BytRefresh)
        End If
    End Sub
    Private Sub Topctrl1_tbDiscard() Handles Topctrl1.tbDiscard
        FIniMaster(0, 0)
    End Sub
    Private Sub Topctrl1_tbSite() Handles Topctrl1.tbSite
        Dim DTTemp As DataTable
        Dim StrCurrentvalue As String = ""

        If BlnHaveSiteList Then
            If DTMaster.Rows.Count > 0 Then
                DTTemp = AgL.FillData("Select SiteList From " & StrTable & " Where " & StrFCode & "='" & AgL.XNull(DTMaster.Rows(BMBMaster.Position).Item("SearchCode")) & "'", AgL.GCn).tables(0)  'CMain.FGetDatTable("Select SiteList From " & StrTable & " Where " & StrFCode & "='" & AgL.XNull(DTMaster.Rows(BMBMaster.Position).Item("SearchCode")) & "'", AgL.GCn)
                If DTTemp.Rows.Count > 0 Then StrCurrentvalue = Agl.XNull(DTTemp.Rows(0).Item("SiteList"))

                'Topctrl1.FManageSite(StrTable, "Update " & StrTable & " Set SiteList='@' Where " & StrFCode & "='" & Agl.XNull(DTMaster.Rows(BMBMaster.Position).Item("SearchCode")) & "' ", StrCurrentvalue, Agl.GCn)
            End If
        End If
    End Sub
    Public Sub MoveRec()

        Dim DTTemp As New DataTable

        Topctrl1.BlankTextBoxes()
        If DTMaster.Rows.Count > 0 Then
            DTTemp = AgL.FillData("Select " & StrFName & " As Name " &
                    "From " & StrTable & "  " &
                    "Where " & StrFCode & "='" & AgL.XNull(DTMaster.Rows(BMBMaster.Position).Item("SearchCode")) & "'", AgL.GCn).Tables(0)

            If DTTemp.Rows.Count > 0 Then
                TxtName.Text = Agl.XNull(DTTemp.Rows(0).Item("Name"))
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
        TxtName.Focus()
    End Sub
    Private Sub Topctrl1_tbDel() Handles Topctrl1.tbDel
        Dim BlnTrans As Boolean = False
        Dim GCnCmd As New Object

        Try
            If DTMaster.Rows.Count > 0 Then
                If MsgBox(ClsMain.MsgDeleteCnf, MsgBoxStyle.YesNo) = vbYes Then
                    StrDocId = ""
                    StrDocId = AgL.XNull(DTMaster.Rows(BMBMaster.Position).Item("SearchCode"))
                    If Trim(Replace(StrDocId, "0", "")) = "" Then MsgBox(ClsMain.MsgInvalid & " DocId.") : Exit Sub

                    BlnTrans = True
                    GCnCmd = AgL.GCn.CreateCommand
                    GCnCmd.Transaction = AgL.GCn.BeginTransaction(IsolationLevel.Serializable)

                    GCnCmd.CommandText = "Delete From " & StrTable & " Where " & StrFCode & "='" & (StrDocId) & "'"
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
                MsgBox(ClsMain.MsgDeleteChk)
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
            If BlnHaveSiteCode Then
                AgL.PubFindQry = "Select " & StrFCode & "," & StrFName & " As Name " &
                             "From " & StrTable & " where site_Code='" & AgL.PubSiteCode & "' "
            Else
                AgL.PubFindQry = "Select " & StrFCode & "," & StrFName & " As Name " &
                            "From " & StrTable & "  "
            End If
            AgL.PubFindQryOrdBy = "Name"
            'LIPublic.CreateAndSendArr("200,200,200")
            '*************** common code start *****************
            Dim Frmbj As AgLibrary.FrmFind = New AgLibrary.FrmFind(AgL.PubFindQry, Me.Text & " Find", AgL)
            Frmbj.ShowDialog()

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

            If Not DTMaster.Rows.Count > 0 Then
                MsgBox(ClsMain.MsgRecNotFnd & " to Print!!!")
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

            strQry = "Select " & StrFCode & "," & StrFName & " As Name " &
                         "From " & StrTable & " Order by " & StrFName

            ds = AgL.FillData(strQry, AgL.GCn)


            ds.WriteXmlSchema(AgL.PubReportPath & "\SingleLine.Xml")
            rptReg.Load(AgL.PubReportPath & "\SingleLine.rpt")
            rptReg.SetDataSource(ds)

            CMain.FormulaSet(rptReg, Me.Text)
            CMain.FShowReport(rptReg, Me.MdiParent, Me.Text)

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

            If AgL.RequiredField(TxtName, StrLabel) Then Exit Sub

            StrName = CMain.FRemoveSpace(TxtName.Text)
            StrDocId = ""
            If Topctrl1.Mode = "Add" Then
                If BlnHaveSiteCode Then
                    'StrDocId = AgL.PubSiteCode + Trim(CMain.FGetMaxNoWithSiteCode(StrTable, StrFCode, AgL.GCn))
                    StrDocId = AgL.GetMaxId(StrTable, StrFCode, AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 4, True)
                Else
                    'StrDocId = AgL.PubSiteCode + Trim(CMain.FGetMaxNoWithSiteCode(StrTable, StrFCode, AgL.GCn))
                    StrDocId = AgL.GetMaxId(StrTable, StrFCode, AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 4, False)
                End If
            Else
                StrDocId = AgL.XNull(DTMaster.Rows(BMBMaster.Position).Item("SearchCode"))
            End If
            If Trim(Replace(StrDocId, "0", "")) = "" Then MsgBox(ClsMain.MsgInvalid & " DocId.") : Exit Sub
            If BlnHaveSiteCode Then
                If CMain.DuplicacyChecking("Select Count(" & StrFName & ") As Cnt From " & StrTable & " Where " & StrFName & "='" & StrName & "' And " & StrFCode & "<>'" & (StrDocId) & "' And Site_Code='" & AgL.PubSiteCode & "'", StrLabel + " Already Exists.") Then TxtName.Focus() : Exit Sub
            Else
                If CMain.DuplicacyChecking("Select Count(" & StrFName & ") As Cnt From " & StrTable & " Where " & StrFName & "='" & StrName & "' And " & StrFCode & "<>'" & (StrDocId) & "'", StrLabel + " Already Exists.") Then TxtName.Focus() : Exit Sub
            End If
            BlnTrans = True
            GCnCmd = AgL.GCn.CreateCommand
            GCnCmd.Transaction = AgL.GCn.BeginTransaction(IsolationLevel.Serializable)

            If Topctrl1.Mode = "Add" Then
                GCnCmd.CommandText = "Insert Into " & StrTable & "(" & StrFCode & "," & StrFName & ", " &
                                     "U_Name," &
                                     "U_EntDt,U_AE " &
                                     "" & IIf(BlnHaveSiteList, ",SiteList", "") & "" &
                                     "" & IIf(BlnHaveSiteCode, ",Site_Code", "") & ") Values " &
                                     "('" & (StrDocId) & "'," & AgL.Chk_Text(StrName) & ", " &
                                     "'" & AgL.PubUserName & "','" & Format(AgL.PubLoginDate, "Short Date") & "'," &
                                     "'" & Microsoft.VisualBasic.Left(Topctrl1.Mode, 1) & "'" & IIf(BlnHaveSiteList, ",'|" & AgL.PubSiteCode & "|'", "") & IIf(BlnHaveSiteCode, ",'" & AgL.PubSiteCode & "'", "") & ")"
            Else
                GCnCmd.CommandText = "Update " & StrTable & " Set "
                GCnCmd.CommandText = GCnCmd.CommandText + "" & StrFName & "=" & AgL.Chk_Text(StrName) & ", "
                GCnCmd.CommandText = GCnCmd.CommandText + "Transfered='N', "
                GCnCmd.CommandText = GCnCmd.CommandText + "U_Name='" & AgL.PubUserName & "', "
                GCnCmd.CommandText = GCnCmd.CommandText + "U_EntDt='" & Format(AgL.PubLoginDate, "Short Date") & "', "
                GCnCmd.CommandText = GCnCmd.CommandText + "U_AE='" & Microsoft.VisualBasic.Left(Topctrl1.Mode, 1) & "' "
                GCnCmd.CommandText = GCnCmd.CommandText + "Where " & StrFCode & "='" & (StrDocId) & "' "
            End If
            GCnCmd.ExecuteNonQuery()

            GCnCmd.Transaction.Commit()
            BlnTrans = False
            If Topctrl1.Mode = "Add" Then
                Topctrl1.LblDocId.Text = StrDocId
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

    Public Function GetMaxId(ByVal mTableName As String, ByVal mPrimaryField As String, ByVal mConn As System.Data.SqlClient.SqlConnection,
                                ByVal PubDivCode As String, ByVal PubSiteCode As String, Optional ByVal mPad_Len As Integer = 0,
                                Optional ByVal IsSiteWise As Boolean = False, Optional ByVal IsDivisionWise As Boolean = False,
                                Optional ByVal mCmd As SqlClient.SqlCommand = Nothing, Optional ByVal mConnectionString As String = "")
        Dim CondStr As String = ""
        Dim GcnRead As New SqlClient.SqlConnection

        If mConnectionString <> "" Then
            GcnRead.ConnectionString = mConnectionString
        Else
            GcnRead.ConnectionString = mConn.ConnectionString + ";pwd=" & AgL.PubDBPasswordSQL & ""
        End If
        GcnRead.Open()

        If mCmd Is Nothing Then
            mCmd = New SqlClient.SqlCommand
            mCmd = GcnRead.CreateCommand
        End If

        If PubSiteCode.Trim.Length = 1 Then
            If IsDivisionWise Then CondStr = " And Left(" & mPrimaryField & ",1) = '" & PubDivCode & "' "
            If IsSiteWise Then CondStr = " And SubString(" & mPrimaryField & ",2,1) = '" & PubSiteCode & "' "
        ElseIf PubSiteCode.Trim.Length = 2 Then
            PubDivCode = ""
            If IsSiteWise Then CondStr = " And SubString(" & mPrimaryField & ",1,2) = '" & PubSiteCode & "' "
        End If

        mCmd = AgL.Dman_Execute("Select Case When IsNull(Max(Convert(BigInt, SubString(" & mPrimaryField & ",3,Len(" & mPrimaryField & ")))) ,0) > 0 Then IsNull(Max(Convert(BigInt, SubString(" & mPrimaryField & ",3,Len(" & mPrimaryField & ")))) ,0) Else 0 End  +1 As MaxId From " & mTableName & " With (NoLock)  " &
                            " Where 1=1 AND ISNUMERIC (SubString(" & mPrimaryField & ",3,Len(" & mPrimaryField & ")))>0 " & CondStr & " ", GcnRead)

        GetMaxId = PubDivCode & PubSiteCode & mCmd.ExecuteScalar().ToString.PadLeft(mPad_Len, "0")

        If mConnectionString <> "" Then
            GcnRead.Dispose()
        End If
    End Function


    Private Sub FrmSingleFieldMaster_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub
End Class
