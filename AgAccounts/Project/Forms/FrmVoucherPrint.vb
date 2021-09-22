Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.SQLite
Public Class FrmVoucherPrint
    Public StrLocalDocId As String
    Public StrLocalVType As String
    Public StrPartyCode As String
    Public StrLocalVtypeName As String
    Public DtVdate As Date
    Public StrVNo As String
    Private FrmParent As Form
    Private DTMaster As New DataTable
    Private LIEvent As ClsEvents
    Private Sub BtnOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles BtnCancel.Click, _
         BtnPrint.Click
        Select Case sender.name
            Case BtnPrint.Name
                FDocPrint()
            Case BtnCancel.Name
                Me.Close()
        End Select
    End Sub
    Private Sub FrmVoucherPrint_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LIEvent = New ClsEvents(Me)
        IniList()
        ' txtFromdate.Text = "All"
        'txtVoucherType.Text = "All"
        'TxtFromVrNo.Text = "All"

    End Sub
    Private Sub VoucherPrint_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        Agl.FPaintForm(Me, e, 0)
    End Sub
    Sub New(ByVal StrLocalDocIdVar As String, ByVal StrLocalVTypeVar As String, ByVal StrLocalVtypeName As String, ByVal Dtvdate As Date, ByVal StrVno As String, ByVal FrmParent_Var As Form)
        InitializeComponent()
        FrmParent = FrmParent_Var
        ' Add any initialization after the InitializeComponent() call.
        StrLocalDocId = StrLocalDocIdVar
        StrLocalVType = StrLocalVTypeVar
        txtVoucherType.Text = StrLocalVtypeName
        txtVoucherType.Tag = StrLocalVTypeVar
        txtFromdate.Text = Dtvdate
        txtToDate.Text = Dtvdate
        Txtvrno.Text = StrVno
    End Sub
    Private Sub FDocPrint()
        Dim rptReg As New ReportDocument
        Dim GCnCmd As New Object
        Dim ds As New DataSet
        Dim StrQry As String = "", StrCondition As String = ""
        Dim DTtemp As New DataTable
        Dim StrReportName As String = ""

        If AgL.RequiredField(txtFromdate, "From Date") Then Exit Sub
        If AgL.RequiredField(txtToDate, "To Date") Then Exit Sub
        If AgL.RequiredField(txtVoucherType, "Voucher Type") Then Exit Sub
        If AgL.RequiredField(Txtvrno, "Voucher No") Then Exit Sub

        StrCondition = " Where 1=1 "

        If txtFromdate.Text <> "'" Then StrCondition = StrCondition & " And LM.V_Date >=" & AgL.Chk_Date(CDate(txtFromdate.Text).ToString("s")) & ""
        If txtToDate.Text <> "'" Then StrCondition = StrCondition & " and LM.V_Date <=" & AgL.Chk_Date(CDate(txtToDate.Text).ToString("s")) & ""
        If txtVoucherType.Tag <> "" Then StrCondition = StrCondition & " And LG.V_type ='" & txtVoucherType.Tag & "'"

        If Txtvrno.Tag <> "" Then StrCondition = StrCondition & " And LG.docid in(" & Txtvrno.Tag & ")"
        If Txtvrno.Tag = "" And Txtvrno.Text <> "All" Then StrCondition = StrCondition & " And LG.docid in('" & StrLocalDocId & "')"
        Try
            Me.Cursor = Cursors.WaitCursor
            AgL.PubReportTitle = txtVoucherType.Text

            DTtemp = cmain.FGetDatTable("Select ReportName From Voucher_Type Where V_Type='" & txtVoucherType.Tag & "'", Agl.Gcn)

            If DTtemp.Rows.Count > 0 Then
                StrReportName = Trim(Agl.Xnull(DTtemp.Rows(0).Item("ReportName")))
            End If

            If StrReportName = "" Then MsgBox("Please Define [RPT]  Name. ") : Exit Sub

            'StrQry = "Select LM.DocId,LM.V_Type,LM.V_Prefix,LM.Site_Code,LM.RecId As V_No," & _
            '        " LM.V_Date,LM.SubCode,  SG.Name As AcName, LM.Narration,LM.U_Name,LM.PreparedBy, " & _
            '        " VT.Description,VT.Category,LG.SubCode,SG1.Name As AcName1," & _
            '        " SG1.ManualCode,LG.V_SNo, LG.AmtDr,LG.AmtCr,LG.Narration, " & _
            '        " IfNull(LG.Chq_No,'''') as Chq_No, LG.Chq_Date, LG.TDSOnAmt, " & _
            '        " LG.TDSCategory,SM.Name as SiteName,IfNull(LM.PostedBy,'''') as PostedBy," & _
            '        " VT.Category as Categ, 1 as RecId ," & _
            '        " IfNull(sg1.add1,'') as add1," & _
            '        " IfNull(sg1.add2,'') as add2,IfNull(CT.cityname,'') as Cityname " & _
            '        " From LedgerM_Temp LM " & _
            '        " LEFT JOIN Ledger_Temp LG  ON LG.DocID=LM.DocId " & _
            '        " Left Join SubGroup SG On LM.SubCode=SG.SubCode " & _
            '        " Left Join SubGroup SG1 On LG.SubCode=SG1.SubCode " & _
            '        " Left Join Voucher_Type VT On VT.V_Type=LM.V_Type " & _
            '        " Left join city CT on CT.citycode=SG1.citycode  " & _
            '        " Left Join SiteMast SM On SM.Code=LM.Site_Code" + StrCondition + ""

            StrQry = "Select LM.DocId,LM.V_Type,LM.V_Prefix,LM.Site_Code,LM.RecId As V_No," &
                    " LM.V_Date,LM.SubCode,  SG.Name As AcName, LM.Narration,LM.U_Name,LM.PreparedBy, " &
                    " VT.Description,VT.Category,LG.SubCode,SG1.Name As AcName1," &
                    " SG1.ManualCode,LG.V_SNo, LG.AmtDr,LG.AmtCr,LG.Narration, " &
                    " IfNull(LG.Chq_No,'''') as Chq_No, LG.Chq_Date, LG.TDSOnAmt, " &
                    " LG.TDSCategory,SM.Name as SiteName,IfNull(LM.PostedBy,'''') as PostedBy," &
                    " VT.Category as Categ, 1 as RecId ," &
                    " IfNull(sg1.address,'') as add1," &
                    " '' as add2,IfNull(CT.cityname,'') as Cityname " &
                    " From LedgerM LM " &
                    " LEFT JOIN Ledger LG  ON LG.DocID=LM.DocId " &
                    " Left Join SubGroup SG On LM.SubCode=SG.SubCode " &
                    " Left Join SubGroup SG1 On LG.SubCode=SG1.SubCode " &
                    " Left Join Voucher_Type VT On VT.V_Type=LM.V_Type " &
                    " Left join city CT on CT.citycode=SG1.citycode  " &
                    " Left Join SiteMast SM On SM.Code=LM.Site_Code" + StrCondition + ""

            DTtemp = CMain.FGetDatTable(StrQry, AgL.GCn)
            If DTtemp.Rows.Count > 0 Then
                If AgL.XNull(DTtemp.Rows(0).Item("Categ")) <> "JV" Then

                    If AgL.XNull(DTtemp.Rows(0).Item("Categ")) <> "PMT" Then
                        StrQry = StrQry + "And IfNull(AmtCr,0)>0"
                    ElseIf AgL.XNull(DTtemp.Rows(0).Item("Categ")) <> "RCT" Then
                        StrQry = StrQry + "And IfNull(AmtDr,0)>0"
                    End If
                End If
            End If
            StrQry = StrQry + " order by LM.V_date,LM.Recid"

            ds = AgL.FillData(StrQry, AgL.GCn)


            ds.WriteXmlSchema(Agl.PubReportPath & "\" & StrReportName & ".Xml")
            rptReg.Load(Agl.PubReportPath & "\" & StrReportName & ".rpt")

            rptReg.SetDataSource(ds)
            FormulaSet(rptReg, Me)
            CMain.FShowReport(rptReg, Me.MdiParent, Me.Name)
            Me.Cursor = Cursors.Default
        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try
        Me.Cursor = Cursors.Default
    End Sub

    Sub IniList()
        Dim mQry$
        mQry = "Select VT.V_Type,VT.Description,VT.DefaultAc,SG.Name As DAName "
        mQry += "From Voucher_Type VT "
        mQry += "Left Join SubGroup SG On VT.DefaultAc=SG.SubCode "
        mQry += "Where VT.NCat='FA' And VT.Category In ('PMT','RCT','JV')"
        mQry += "Order By VT.Description "
        txtVoucherType.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)


        mQry = "SELECT 'o' As Tick, LG.Docid,LG.Recid FROM LedgerM LG "
        mQry += "Where LG.Site_Code= '" & AgL.PubSiteCode & "' and LG.V_Type='" & txtVoucherType.Tag & "' and LG.V_date>='" & txtFromdate.Text & "' and LG.V_date<='" & txtToDate.Text & "' Order by LG.V_date"
        Txtvrno.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)
    End Sub


    Private Sub txtFromdate_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFromdate.Validated, txtToDate.Validated
        Select Case sender.name
            Case txtFromdate.Name, txtToDate.Name
                sender.Text = Agl.RetDate(sender.Text)
        End Select
    End Sub


End Class