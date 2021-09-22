Module MdlFunction
    'Public Sub CMain.FShowReport(ByVal RptReg As CrystalDecisions.CrystalReports.Engine.ReportDocument, ByVal FrmMDI As Form, ByVal StrReportCaption As String, Optional ByVal BlnDirectPrint As Boolean = False)
    '    Dim NRepView As New RepView
    '    If BlnDirectPrint Then
    '        RptReg.PrintToPrinter(1, True, 1, 1)
    '    Else
    '        NRepView.RepObj = RptReg
    '        NRepView.MdiParent = FrmMDI
    '        NRepView.Text = StrReportCaption
    '        NRepView.Show()
    '    End If
    'End Sub
    Public DHSSMain As ClsStructure.DHSettings_Stock
    Public DHSMain As ClsStructure.DHSettings


    Public Function FOpenIni(ByVal StrIniPath As String, ByVal StrUserName As String, ByVal StrPassword As String) As Boolean
        'Dim StrGetPassword As String
        Dim OLECmd As New OleDb.OleDbCommand
        Dim BlnRtn As Boolean = False
        Dim ECmd As SqlClient.SqlCommand

        Try
            AgL = New AgLibrary.ClsMain

            AgL.PubDBUserSQL = "Sa"
            AgL.PubDBPasswordSQL = ""
            AgL.PubServerName = AgL.INIRead(StrIniPath, "Server", "Name", "NULL")
            AgL.PubReportPath = AgL.INIRead(StrIniPath, "Reports", "Path", "NULL")
            AgL.PubCompanyDBName = AgL.INIRead(StrIniPath, "CompanyInfo", "Path", "NULL")
            AgL.PubChkPasswordSQL = AgL.INIRead(StrIniPath, "Security", "PasswordSQL", "NULL")
            AgL.PubChkPasswordAccess = AgL.INIRead(StrIniPath, "Security", "PasswordAccess", "NULL")

            AgL.PubReportPath_CommonData = AgL.INIRead(StrIniPath, "Reports", "CommonData", "NULL")
            AgL.PubReportPath_Utility = AgL.INIRead(StrIniPath, "Reports", "Utility", "NULL")

            AgL.PubReportPath = My.Application.Info.DirectoryPath & "\Reports"

            AgIniVar = New AgLibrary.ClsIniVariables(AgL)

            AgIniVar.FOpenIni(StrUserName, StrPassword)

            'AgL.GCnComp = New OleDb.OleDbConnection()
            'AgL.ECompConn = New SqlClient.SqlConnection()
            'AgL.GcnMain = New SqlClient.SqlConnection()
            'AgL.GcnMainAdo = New ADODB.Connection
            'AgL.GcnMainAdo.CursorLocation = ADODB.CursorLocationEnum.adUseClient
            'AgL.GcnMainAdo.IsolationLevel = ADODB.IsolationLevelEnum.adXactChaos

            'If UCase(Trim(AgL.PubChkPasswordSQL)) = "Y" Then
            '    AgL.GcnComp_ConnectionString = "Provider=SQLOLEDB;User ID='" & AgL.PubDBUserSQL & "';password=" & AgL.PubDBPasswordSQL & ";Data Source=" & AgL.PubServerName & ";Database=" & AgL.PubCompanyDBName & ""
            '    AgL.GCnComp.ConnectionString = AgL.GcnComp_ConnectionString


            '    AgL.ECompConn_ConnectionString = "Persist Security Info=False;User ID='" & AgL.PubDBUserSQL & "';pwd=" & AgL.PubDBPasswordSQL & ";Initial Catalog=" & AgL.PubCompanyDBName & ";Data Source=" & AgL.PubServerName
            '    AgL.ECompConn.ConnectionString = AgL.ECompConn_ConnectionString

            '    AgL.GcnMain_ConnectionString = "Persist Security Info=False;User ID='" & AgL.PubDBUserSQL & "';pwd=" & AgL.PubDBPasswordSQL & ";Initial Catalog=" & AgL.PubCompanyDBName & ";Data Source=" & AgL.PubServerName
            '    AgL.GcnMain.ConnectionString = AgL.GcnMain_ConnectionString

            '    AgL.GcnMainAdo_ConnectionString = "Provider=SQLOLEDB.1;Persist Security Info=False;User ID=" & AgL.PubDBUserSQL & "; Password=" & AgL.PubDBPasswordSQL & ";Initial Catalog=" & AgL.PubCompanyDBName & " ;Data Source=" & AgL.PubServerName
            '    AgL.GcnMainAdo.ConnectionString = AgL.GcnMainAdo_ConnectionString
            'Else
            '    AgL.GcnComp_ConnectionString = "Provider=SQLOLEDB;User ID='" & AgL.PubDBUserSQL & "';password=;Data Source=" & AgL.PubServerName & ";Database=" & AgL.PubCompanyDBName & ""
            '    AgL.GCnComp.ConnectionString = AgL.GcnComp_ConnectionString

            '    AgL.ECompConn_ConnectionString = "Persist Security Info=False;User ID='" & AgL.PubDBUserSQL & "';pwd=;Initial Catalog=" & AgL.PubCompanyDBName & ";Data Source=" & AgL.PubServerName
            '    AgL.ECompConn.ConnectionString = AgL.ECompConn_ConnectionString

            '    AgL.GcnMain_ConnectionString = "Persist Security Info=False;User ID='" & AgL.PubDBUserSQL & "';pwd=;Initial Catalog=" & AgL.PubCompanyDBName & ";Data Source=" & AgL.PubServerName
            '    AgL.GcnMain.ConnectionString = AgL.GcnMain_ConnectionString

            '    AgL.GcnMainAdo_ConnectionString = "Provider=SQLOLEDB.1;Persist Security Info=False;User ID=" & AgL.PubDBUserSQL & "; Initial Catalog=" & AgL.PubCompanyDBName & " ;Data Source=" & AgL.PubServerName
            '    AgL.GcnMainAdo.ConnectionString = AgL.GcnMainAdo_ConnectionString
            'End If
            'AgL.GCnComp.Open()
            'AgL.ECompConn.Open()
            'AgL.GcnMain.Open()
            'AgL.GcnMainAdo.Open()

            'ECmd = AgL.Dman_Execute("Select PassWD From UserMast Where User_Name='" & StrUserName & "'", AgL.ECompConn)
            'StrGetPassword = ECmd.ExecuteScalar

            'If UCase(StrPassword) = UCase(AgL.DCODIFY(StrGetPassword)) And (Not StrGetPassword Is Nothing) Then
            '    AgL.PubUserName = StrUserName
            '    BlnRtn = True
            'Else
            '    MsgBox("Access Denied.Please Check User Name/ Password.", MsgBoxStyle.Information, AgLibrary.ClsMain.PubMsgTitleInfo)
            '    BlnRtn = False
            'End If
            OLECmd = Nothing
        Catch Ex As Exception
            BlnRtn = False
            MsgBox(Ex.Message, MsgBoxStyle.Information, AgLibrary.ClsMain.PubMsgTitleInfo)
        Finally
            ECmd = Nothing

        End Try
        FOpenIni = BlnRtn
    End Function

    Public Sub FormulaSet(ByVal Rpt As Object, ByVal frm As Form)
        Dim i As Int16

        For i = 0 To Rpt.DataDefinition.FormulaFields.Count - 1
            Select Case CStr(UCase(Rpt.DataDefinition.FormulaFields.Item(i).Name))
                Case "COMPANYNAME"
                    Rpt.DataDefinition.FormulaFields.Item(i).Text = "'" & AgL.PubCompName & "'"
                Case "COMPANYADDRESS"
                    Rpt.DataDefinition.FormulaFields.Item(i).Text = "'" & AgL.PubCompAdd1 & ", " & AgL.PubCompAdd2 & "'"
                Case "COMPANYCITY"
                    Rpt.DataDefinition.FormulaFields.Item(i).Text = "'" & AgL.PubCompCity & " - " & AgL.PubCompPinCode & " '"
                Case "TITLE"
                    Rpt.DataDefinition.FormulaFields.Item(i).Text = "'" & AgL.PubReportTitle & "'"
            End Select
        Next
    End Sub
    Public Function FGetLedgerBalance(ByVal StrSubCode As String) As Double
        Dim DblRtn As Double
        Dim DTTemp As DataTable

        Try
            DTTemp = CMain.FGetDatTable("Select (IsNull(Sum(LG.AmtDr),0)-IsNull(Sum(LG.AmtCr),0)) As Balance From Ledger LG Where LG.SubCode='" & StrSubCode & "' And LG.Site_Code='" & AgL.PubSiteCode & "' ", AgL.GCn)
            DblRtn = DTTemp.Rows(0).Item("Balance")
            DTTemp.Dispose()
        Catch ex As Exception
            DblRtn = 0
        End Try
        DTTemp = Nothing
        FGetLedgerBalance = DblRtn
    End Function
    Public Function FGetSinleValue(ByVal StrSQL As String, ByVal SQLCon As SqlClient.SqlConnection) As Double
        Dim GCnCmd1 As New SqlClient.SqlCommand
        Dim DblRtnValue As Double
        Try
            GCnCmd1.Connection = SQLCon
            GCnCmd1.CommandText = StrSQL
            DblRtnValue = GCnCmd1.ExecuteScalar
        Catch ex As Exception
            DblRtnValue = 0
        End Try
        Return (DblRtnValue)
    End Function

    Public Sub FPrintGlobal(ByVal StrLocalDocId As String, ByVal StrLocalV_Type As String, ByVal StrReportTitle As String, ByVal FrmObj As Form, ByVal StrPrefix As String, Optional ByVal BlnDirectPrint As Boolean = False)
        Dim rptReg As New CrystalDecisions.CrystalReports.Engine.ReportDocument
        Dim DTTemp As New DataTable
        Dim StrReportName As String = ""
        Dim StrProcName As String = ""

        Try
            If StrLocalDocId = "" Then MsgBox(ClsMain.MsgRecNotFnd + " To Print.") : Exit Sub
            StrPrefix = Trim(StrPrefix)
            DTTemp = CMain.FGetDatTable("SELECT ReportName,ProcedureName FROM Voucher_Type WHERE v_type='" & StrLocalV_Type & "' ", AgL.GCn)
            If DTTemp.Rows.Count > 0 Then
                StrReportName = Trim(AgL.XNull(DTTemp.Rows(0).Item("ReportName")))
                StrProcName = Trim(AgL.XNull(DTTemp.Rows(0).Item("ProcedureName")))
            End If
            DTTemp.Dispose()
            If StrReportName = "" Or StrProcName = "" Then MsgBox("Please Define [RPT] And [Procedure] Name. ") : Exit Sub

            AgL.PubReportTitle = StrReportTitle
            DTTemp = CMain.FGetDatTable("Exec " & StrProcName & "  '''" & StrLocalDocId & "'''", AgL.GCn)
            DTTemp.WriteXmlSchema(AgL.PubReportPath & "\" & StrReportName & ".Xml")
            rptReg.Load(AgL.PubReportPath & "\" & StrReportName & ".rpt")

            rptReg.SetDataSource(DTTemp)
            FormulaSet(rptReg, FrmObj)
            CMain.FShowReport(rptReg, FrmObj.MdiParent, StrReportTitle, BlnDirectPrint)
        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try
    End Sub

End Module