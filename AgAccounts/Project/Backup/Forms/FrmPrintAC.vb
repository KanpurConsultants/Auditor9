Imports CrystalDecisions.CrystalReports.Engine
Public Class FrmPrintAC
    Public StrLocalDocId As String
    Public StrLocalVType As String
    Public StrPartyCode As String
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
    Private Sub FrmPrintAC_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LIEvent = New ClsEvents(Me)
        TxtAcGroup.Text = "All"
        TxtNature.Text = "All"
        TxtDistName.Text = "All"
        TxtPartyName.Text = "All"
        TxtCity.Text = "All"
        TxtSortBy.Text = "Party"
        TxtSortBy.Tag = "P"
    End Sub
    Private Sub FrmPrintOS_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        Agl.FPaintForm(Me, e, 0)
    End Sub
    Sub New(ByVal StrLocalDocIdVar As String, ByVal StrLocalVTypeVar As String, ByVal FrmParent_Var As Form)
        InitializeComponent()
        FrmParent = FrmParent_Var
        ' Add any initialization after the InitializeComponent() call.
        StrLocalDocId = StrLocalDocIdVar
        StrLocalVType = StrLocalVTypeVar
    End Sub
    Private Sub FDocPrint()
     
        Dim rptReg As New ReportDocument
        Dim GCnCmd As New SqlClient.SqlCommand
        Dim ds As New DataSet
        Dim StrQry As String, StrCondition As String = "", StrSortBy As String = ""

        Try
            Me.Cursor = Cursors.WaitCursor
            AgL.PubReportTitle = "Account Master"

            If TxtAcGroup.Tag <> "" Then StrCondition += " And SG.GroupCode In (" & TxtAcGroup.Tag & ") "
            If TxtNature.Tag <> "" Then StrCondition += " And SG.Nature In (" & TxtNature.Tag & ") "
            If TxtPartyName.Tag <> "" Then StrCondition += " And SG.SubCode In (" & TxtPartyName.Tag & ") "
            If TxtDistName.Tag <> "" Then StrCondition += " And SG.Distributor In (" & TxtDistName.Tag & ") "
            If TxtCity.Tag <> "" Then StrCondition += " And SG.CityCode In (" & TxtCity.Tag & ") "

            If TxtSortBy.Tag = "G" Then StrSortBy = "AG.GroupName "
            If TxtSortBy.Tag = "P" Then StrSortBy = "SG.Name "
            If TxtSortBy.Tag = "D" Then StrSortBy = "SG1.Name "
            If TxtSortBy.Tag = "N" Then StrSortBy = "SG.Nature "
            If TxtSortBy.Tag = "C" Then StrSortBy = "CityName "

            StrQry = "SELECT  SG.Name, SG.ManualCode,AG.GroupName, SG.Nature,  "
            StrQry += "IsNull(SG.ContactPerson,'') As ConPerson,IsNull(SG.Add1,'')+' '+IsNull(SG.Add2,'') AS Address, "
            StrQry += "IsNull(CT.CityName,'') AS CityName,IsNull(C.Name,'') AS Country,IsNull(CCM.Name,'') As CCName, "
            StrQry += "IsNull(SG.PIN,'') AS PinNo,IsNull(SG.Phone,'') AS Phone,IsNull(SG.Mobile,'') AS Mobile,  "
            StrQry += "IsNull(SG.FAX,'') AS Fax,IsNull(SG.TINNo,'') AS TinNo,IsNull(SG.CSTNo,'') AS CSTNo, "
            StrQry += "IsNull(SG.STNo,'') AS STNo,IsNull(SG.EMail,'') AS Email,IsNull(SG.PAN,'') AS PAN, "
            StrQry += "IsNull(SG.DuplicateTIN,'') AS DuplicateTIN,SG.Location,SG.CreditLimit,SG.DueDays, "
            StrQry += "IsNull(TC.Name,'') As TCName,SG.IECCode,SG.ECCCode,SG.Excise,SG.Range,SG.Division, "
            StrQry += "SG.FBTOnPer,SG.FBTPer,LG.Name As LGName,SG.Remark, "
            StrQry += "ZM.Name As ZoneName,SG1.Name As DistName,SG.PolicyNo,SG.PartyCat,SG.PartyType "
            StrQry += "FROM SubGroup SG "
            StrQry += "LEFT JOIN City CT ON SG.CityCode = CT.CityCode "
            StrQry += "LEFT JOIN Country C ON SG.CountryCode = C.Code "
            StrQry += "LEFT JOIN AcGroup AG ON AG.GroupCode=SG.GroupCode "
            StrQry += "Left Join CostCenterMast CCM  On CCM.Code=SG.CostCenter "
            StrQry += "Left Join TDSCat TC On TC.Code=SG.TDS_Catg "
            StrQry += "Left Join LedgerGroup LG On LG.Code=SG.LedgerGroup "
            StrQry += "Left Join ZoneMast ZM On ZM.Code=SG.Zone "
            StrQry += "Left Join SubGroup SG1 On SG1.SubCode=SG.Distributor "
            StrQry += "Where 1=1 " + StrCondition + " "
            StrQry += "Order By " + StrSortBy + " "

            Agl.ADMain = New SqlClient.SqlDataAdapter(StrQry, Agl.Gcn)
            Agl.ADMain.Fill(ds)

            ds.WriteXmlSchema(Agl.PubReportPath & "\AccountMaster.Xml")
            rptReg.Load(Agl.PubReportPath & "\AccountMaster.rpt")
            rptReg.SetDataSource(ds)
            FormulaSet(rptReg, Me)
            CMain.FShowReport(rptReg, Me.MdiParent, Me.Name)
            Me.Cursor = Cursors.Default
        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try
        Me.Cursor = Cursors.Default
    End Sub
    Private Sub FHP_AcGroup(ByRef e As System.Windows.Forms.KeyPressEventArgs, ByVal Txt As TextBox)
        Dim DTMain As New DataTable
        Dim FRH As DMHelpGrid.FrmHelpGrid_Multi
        Dim StrSQL As String
        Dim StrSendText As String, StrPrvText As String

        StrPrvText = Txt.Text
        StrSendText = Cmain.FSendText(Txt, e.KeyChar)

        StrSQL = "Select 'o' As Tick,GroupCode As Code,GroupName as Name,Nature From AcGroup Order By GroupName"

        DTMain = cmain.FGetDatTable(StrSQL, Agl.Gcn)
        FRH = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(DTMain), StrSendText, 500, 470, (Top + Txt.Top) + 85, Left + Txt.Left + 3, False)

        FRH.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleLeft)
        FRH.FFormatColumn(1, "Code", 0, , False)
        FRH.FFormatColumn(2, "Name", 250, DataGridViewContentAlignment.MiddleLeft)
        FRH.FFormatColumn(3, "Nature", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH.StartPosition = FormStartPosition.CenterScreen
        FRH.ShowDialog()

        Txt.Text = StrPrvText

        If FRH.BytBtnValue = 0 Then
            Txt.Text = FRH.FFetchData(2, "", "", ",")
            Txt.Tag = FRH.FFetchData(1, "'", "'", ",", True)
        End If
        FRH = Nothing
        e.KeyChar = ""
    End Sub
    Private Sub FHP_Nature(ByRef e As System.Windows.Forms.KeyPressEventArgs, ByVal Txt As TextBox)
        Dim DTMain As New DataTable
        Dim FRH As DMHelpGrid.FrmHelpGrid_Multi
        Dim StrSQL As String
        Dim StrSendText As String, StrPrvText As String

        StrPrvText = Txt.Text
        StrSendText = Cmain.FSendText(Txt, e.KeyChar)

        StrSQL = "Select Distinct 'o' As Tick,Nature As Code,Nature As Name From AcGroup Order By Name"

        DTMain = cmain.FGetDatTable(StrSQL, Agl.Gcn)
        FRH = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(DTMain), StrSendText, 380, 250, (Top + Txt.Top) + 85, Left + Txt.Left + 3, False)

        FRH.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleLeft)
        FRH.FFormatColumn(1, "Code", 0, , False)
        FRH.FFormatColumn(2, "Name", 120, DataGridViewContentAlignment.MiddleLeft)
        FRH.StartPosition = FormStartPosition.CenterScreen
        FRH.ShowDialog()

        Txt.Text = StrPrvText

        If FRH.BytBtnValue = 0 Then
            Txt.Text = FRH.FFetchData(2, "", "", ",")
            Txt.Tag = FRH.FFetchData(1, "'", "'", ",", True)
        End If
        FRH = Nothing
        e.KeyChar = ""
    End Sub
    Private Sub FHP_City(ByRef e As System.Windows.Forms.KeyPressEventArgs, ByVal Txt As TextBox)
        Dim DTMain As New DataTable
        Dim FRH As DMHelpGrid.FrmHelpGrid_Multi
        Dim StrSQL As String
        Dim StrSendText As String, StrPrvText As String

        StrPrvText = Txt.Text
        StrSendText = Cmain.FSendText(Txt, e.KeyChar)

        StrSQL = "Select 'o' As Tick,CityCode As Code,CityName as City From City Order By City"

        DTMain = cmain.FGetDatTable(StrSQL, Agl.Gcn)
        FRH = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(DTMain), StrSendText, 300, 270, (Top + Txt.Top) + 85, Left + Txt.Left + 3, False)

        FRH.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleLeft)
        FRH.FFormatColumn(1, "Code", 0, , False)
        FRH.FFormatColumn(2, "Name", 150, DataGridViewContentAlignment.MiddleLeft)
        FRH.StartPosition = FormStartPosition.CenterScreen
        FRH.ShowDialog()

        Txt.Text = StrPrvText

        If FRH.BytBtnValue = 0 Then
            Txt.Text = FRH.FFetchData(2, "", "", ",")
            Txt.Tag = FRH.FFetchData(1, "'", "'", ",", True)
        End If
        FRH = Nothing
        e.KeyChar = ""
    End Sub
    Private Sub FHP_Party(ByRef e As System.Windows.Forms.KeyPressEventArgs, ByVal Txt As TextBox)
        Dim DTMain As New DataTable
        Dim FRH As DMHelpGrid.FrmHelpGrid_Multi
        Dim StrSQL As String
        Dim StrSendText As String, StrPrvText As String

        StrPrvText = Txt.Text
        StrSendText = Cmain.FSendText(Txt, e.KeyChar)

        StrSQL = "Select 'o' As Tick,SG.SubCode As Code,SG.Name,SG.ManualCode,IsNull(CT.CityName,''),AG.GroupName "
        StrSQL += "From SubGroup SG Left Join City CT On CT.CityCode=SG.CityCode "
        StrSQL += "Left Join AcGroup AG ON AG.GroupCode=SG.GroupCode "
        StrSQL += "Where SG.Nature In ('Customer') And SG.SiteList Like '%|" & agl.PubSiteCode & "|%' Order By SG.Name "

        DTMain = cmain.FGetDatTable(StrSQL, Agl.Gcn)
        FRH = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(DTMain), StrSendText, 500, 770, (Top + Txt.Top) + 85, Left + Txt.Left + 3, False)

        FRH.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleLeft)
        FRH.FFormatColumn(1, "Code", 0, , False)
        FRH.FFormatColumn(2, "Name", 250, DataGridViewContentAlignment.MiddleLeft)
        FRH.FFormatColumn(3, "Code", 70, DataGridViewContentAlignment.MiddleLeft)
        FRH.FFormatColumn(4, "City", 130, DataGridViewContentAlignment.MiddleLeft)
        FRH.FFormatColumn(5, "Group Name", 200, DataGridViewContentAlignment.MiddleLeft)
        FRH.StartPosition = FormStartPosition.CenterScreen
        FRH.ShowDialog()

        Txt.Text = StrPrvText

        If FRH.BytBtnValue = 0 Then
            Txt.Text = FRH.FFetchData(2, "", "", ",")
            Txt.Tag = FRH.FFetchData(1, "'", "'", ",", True)
        End If
        FRH = Nothing
        e.KeyChar = ""
    End Sub
    Private Sub FHP_Distributor(ByRef e As System.Windows.Forms.KeyPressEventArgs, ByVal Txt As TextBox)
        Dim DTMain As New DataTable
        Dim FRH As DMHelpGrid.FrmHelpGrid_Multi
        Dim StrSQL As String
        Dim StrSendText As String, StrPrvText As String

        StrPrvText = Txt.Text
        StrSendText = Cmain.FSendText(Txt, e.KeyChar)

        StrSQL = "Select 'o' As Tick,SG.SubCode,SG.Name,IsNull(CT.CityName,''),SG.ManualCode From SubGroup SG "
        StrSQL += "Left Join City CT On CT.CityCode=SG.CityCode "
        StrSQL += "Where SG.SiteList Like '%|" & agl.PubSiteCode & "|%' And SG.Nature In ('Agent','Customer','Supplier') Order by SG.Name"

        DTMain = cmain.FGetDatTable(StrSQL, Agl.Gcn)
        FRH = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(DTMain), StrSendText, 380, 520, (Top + Txt.Top) + 85, Left + Txt.Left + 3, False)

        FRH.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter)
        FRH.FFormatColumn(1, "Code", 0, , False)
        FRH.FFormatColumn(2, "Name", 200, DataGridViewContentAlignment.MiddleLeft)
        FRH.FFormatColumn(3, "City", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH.StartPosition = FormStartPosition.CenterScreen
        FRH.ShowDialog()

        Txt.Text = StrPrvText

        If FRH.BytBtnValue = 0 Then
            Txt.Text = FRH.FFetchData(2, "", "", ",")
            Txt.Tag = FRH.FFetchData(1, "'", "'", ",", True)
        End If
        FRH = Nothing
        e.KeyChar = ""
    End Sub
    Private Sub FHP_SortBy(ByRef e As System.Windows.Forms.KeyPressEventArgs, ByVal Txt As TextBox)
        Dim DTMain As New DataTable
        Dim FRH As DMHelpGrid.FrmHelpGrid
        Dim StrSendText As String
        Dim StrSQL As String

        StrSendText = Cmain.FSendText(Txt, e.KeyChar)
        StrSQL = "Declare @TmpTable Table(Code NVarChar(1),Name NVarChar(30)) "
        StrSQL += "Insert Into @TmpTable Values('P','Party') "
        StrSQL += "Insert Into @TmpTable Values('G','Ac Group') "
        StrSQL += "Insert Into @TmpTable Values('N','Nature') "
        StrSQL += "Insert Into @TmpTable Values('D','Distributor') "
        StrSQL += "Insert Into @TmpTable Values('C','City') "

        StrSQL += "Select Code,Name From @TmpTable Order By Name "

        DTMain = cmain.FGetDatTable(StrSQL, Agl.Gcn)
        FRH = New DMHelpGrid.FrmHelpGrid(New DataView(DTMain), StrSendText, 200, 200, (Top + Txt.Top) + 85, Left + Txt.Left + 3)
        FRH.FFormatColumn(0, , 0, , False)
        FRH.FFormatColumn(1, "Name", 100, DataGridViewContentAlignment.MiddleLeft)
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
    Public Sub FTxtKeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        '======== Write Your Code Below =============	
        Select Case sender.Name
            Case TxtAcGroup.Name
                FHP_AcGroup(e, sender)
            Case TxtPartyName.Name
                FHP_Party(e, sender)
            Case TxtDistName.Name
                FHP_Distributor(e, sender)
            Case TxtNature.Name
                FHP_Nature(e, sender)
            Case TxtCity.Name
                FHP_City(e, sender)
            Case TxtSortBy.Name
                FHP_SortBy(e, sender)
        End Select
    End Sub
    Public Sub FTxtKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        '======== Write Your Code Below =============	
        Select Case sender.Name
            Case TxtPartyName.Name, TxtAcGroup.Name, TxtNature.Name, TxtDistName.Name, TxtCity.Name, TxtSortBy.Name
                If e.KeyCode = Keys.Delete Then
                    sender.Text = "" : sender.Tag = ""
                End If
        End Select
    End Sub
    Public Sub FTxtGotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        '======== Write Your Code Below =============	
    End Sub
End Class