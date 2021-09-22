Imports CrystalDecisions.CrystalReports.Engine
Public Class FrmAccountMaster
    Private DTMaster As New DataTable
    Public BMBMaster As BindingManagerBase
    Private LIEvent As ClsEvents
    Public _StructObj As New ClsStructure.StuctAcDetails
    Dim FrmFind As New AgLibrary.frmFind(AgL)
    Dim GNature As String = ""
    Private Sub BtnAccountDetail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnAccountDetail.Click, BtnOtherDetails.Click
        Dim mfrm As FrmAccountDetails
        Select Case sender.name
            Case BtnAccountDetail.Name
                If Topctrl1.Mode = "Browse" Then
                    mfrm = New FrmAccountDetails(Me, False)
                Else
                    mfrm = New FrmAccountDetails(Me, True)
                End If
                mfrm.ShowDialog()
                mfrm = Nothing
            Case BtnOtherDetails.Name
                FOpenOtherDetail()
        End Select
    End Sub
    Private Sub FOpenOtherDetail()
        Dim FrmObjMDI As Object
        Try
            FrmObjMDI = Me.MdiParent
            If Topctrl1.Mode = "Browse" Then
                FrmObjMDI.FAccountOtherDetail(Agl.Xnull(DTMaster.Rows(BMBMaster.Position).Item("SearchCode")), False)
            ElseIf Topctrl1.Mode = "Edit" Then
                FrmObjMDI.FAccountOtherDetail(Agl.Xnull(DTMaster.Rows(BMBMaster.Position).Item("SearchCode")), True)
            ElseIf Topctrl1.Mode = "Add" Then
                MsgBox("You Cann Not Use This Feature In Add Mode.")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub FrmAccountMaster_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F2 Or e.KeyCode = Keys.F3 Or e.KeyCode = Keys.F4 Or e.KeyCode = (Keys.F And e.Control) Or e.KeyCode = (Keys.P And e.Control) _
        Or e.KeyCode = (Keys.S And e.Control) Or e.KeyCode = Keys.Escape Or e.KeyCode = Keys.F5 Or e.KeyCode = Keys.F10 Or e.KeyCode = Keys.F11 _
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

    Private Sub FrmAccountMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            LIEvent = New ClsEvents(Me)
            AgL.WinSetting(Me, 379, 891, 0, 0)
            IniList()
            FIniMaster()
            MoveRec()
        Catch ex As Exception




            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub FIniMaster(Optional ByVal BytDel As Byte = 0, Optional ByVal BytRefresh As Byte = 1)
        Topctrl1.FIniForm(DTMaster, Agl.Gcn, "Select SubCode As SearchCode,Name From SubGroup Order By Name", True, cmbPartyName, "SearchCode", "Name", BytDel, BytRefresh)
    End Sub
    Private Sub Topctrl1_tbSite() Handles Topctrl1.tbSite
        Dim DTTemp As DataTable
        Dim StrCurrentvalue As String = ""
        If DTMaster.Rows.Count > 0 Then
            DTTemp = cmain.FGetDatTable("Select SiteList From Subgroup Where SubCode='" & Agl.Xnull(DTMaster.Rows(BMBMaster.Position).Item("SearchCode")) & "'", Agl.Gcn)
            If DTTemp.Rows.Count > 0 Then StrCurrentvalue = Agl.Xnull(DTTemp.Rows(0).Item("SiteList"))

            Topctrl1.FManageSite("Subgroup", "Update Subgroup Set SiteList='@' Where SubCode='" & Agl.Xnull(DTMaster.Rows(BMBMaster.Position).Item("SearchCode")) & "' ", StrCurrentvalue, Agl.Gcn)
        End If
    End Sub
    Private Sub Topctrl1_tbDiscard() Handles Topctrl1.tbDiscard
        FIniMaster(0, 0)
    End Sub
    Public Sub MoveRec()
        Dim ADTemp As SqlClient.SqlDataAdapter
        Dim DTTemp As New DataTable
        Dim StrSQL As String = ""

        Topctrl1.BlankTextBoxes()
        If DTMaster.Rows.Count > 0 Then

            StrSQL = "Select SG.SubCode, Sg.Name as CustomerName,SG.STNo,SG.IECCode,SG.DuplicateTIN, "
            StrSQL += "SG.GroupCode,AG.GroupName, Sg.GroupNature, SG.ManualCode, SG.Nature,SG.Add1,SG.Add2,  "
            StrSQL += "Sg.CityCode,C.cityName, SG.CountryCode,Ctry.Name as CountryName, SG.PIN, SG.Phone, "
            StrSQL += "SG.Mobile,SG.Fax,SG.ContactPerson,CCM.Name As CCName,SG.CostCenter,"
            StrSQL += "SG.remark,SG.PAN,SG.CSTNo,SG.EMail,SG.TINNo,SG.Location,SG.CreditLimit,SG.DueDays, "
            StrSQL += "SG.PartyCat,SG.PartyType,SG.ECCCode,SG.Excise,SG.Range,SG.Division,SG.FBTOnPer,SG.FBTPer, "
            StrSQL += "SG.TDS_Catg,TC.Name As TCName,SG.LedgerGroup,LG.Name As LGName, "
            StrSQL += "SG.Zone,ZM.Name As ZMName,SG.Distributor,SG1.Name As DName,SG.PolicyNo "
            StrSQL += "From SubGroup SG "
            StrSQL += "Left Join AcGroup AG On AG.GroupCode=SG.GroupCode "
            StrSQL += "Left Join City C On C.CityCode=SG.CityCode "
            StrSQL += "Left Join Country Ctry On Ctry.Code=SG.CountryCode "
            StrSQL += "Left Join CostCenterMast CCM  On CCM.Code=SG.CostCenter "
            StrSQL += "Left Join TDSCat TC On TC.Code=SG.TDS_Catg "
            StrSQL += "Left Join LedgerGroup LG On LG.Code=SG.LedgerGroup "
            StrSQL += "Left Join ZoneMast ZM On ZM.Code=SG.Zone "
            StrSQL += "Left Join SubGroup SG1 On SG1.SubCode=SG.Distributor "
            StrSQL += "Where SG.SubCode='" & Agl.Xnull(DTMaster.Rows(BMBMaster.Position).Item("SearchCode") & "' ")

            ADTemp = New SqlClient.SqlDataAdapter(StrSQL, Agl.Gcn)
            ADTemp.Fill(DTTemp)

            If DTTemp.Rows.Count > 0 Then
                With DTTemp.Rows(0)

                    cmbPartyName.Text = .Item("CustomerName")
                    txtManualCode.Text = Agl.Xnull(.Item("ManualCode"))
                    txtAcGroup.Tag = Agl.Xnull(.Item("GroupCode"))
                    txtAcGroup.Text = Agl.Xnull(.Item("GroupName"))
                    txtNature.Text = Agl.Xnull(.Item("Nature"))
                    TxtCostCenter.Text = Agl.Xnull(.Item("CCName"))
                    TxtCostCenter.Tag = Agl.Xnull(.Item("CostCenter"))

                    _StructObj.Address1 = Agl.Xnull(.Item("Add1"))
                    _StructObj.Address2 = Agl.Xnull(.Item("Add2"))
                    _StructObj.PIN = Agl.Xnull(.Item("Pin"))
                    _StructObj.PhoneNo = Agl.Xnull(.Item("Phone"))
                    _StructObj.Remark = Agl.Xnull(.Item("Remark"))
                    _StructObj.Mobile = Agl.Xnull(.Item("Mobile"))
                    _StructObj.CityCode = Agl.Xnull(.Item("Citycode"))
                    _StructObj.CityName = Agl.Xnull(.Item("Cityname"))
                    _StructObj.CountryCode = Agl.Xnull(.Item("CountryCode"))
                    _StructObj.CountryName = Agl.Xnull(.Item("CountryName"))
                    _StructObj.Fax = Agl.Xnull(.Item("Fax"))
                    _StructObj.ContactPerson = Agl.Xnull(.Item("ContactPerson"))

                    _StructObj.PAN = Agl.Xnull(.Item("PAN"))
                    _StructObj.CST = Agl.Xnull(.Item("CSTNo"))
                    _StructObj.ST = Agl.Xnull(.Item("STNo"))
                    _StructObj.EMail = Agl.Xnull(.Item("EMail"))
                    _StructObj.TIN = Agl.Xnull(.Item("TINNo"))
                    _StructObj.DuplicateTIN = Agl.Xnull(.Item("DuplicateTIN"))
                    _StructObj.Location = Agl.Xnull(.Item("Location"))
                    _StructObj.PartyCategory = Agl.Xnull(.Item("PartyCat"))
                    _StructObj.PartyType = Agl.Xnull(.Item("PartyType"))
                    _StructObj.CreditLimit = Agl.VNull(.Item("CreditLimit"))
                    _StructObj.DueDays = Agl.VNull(.Item("DueDays"))
                    _StructObj.ECCCode = Agl.Xnull(.Item("ECCCode"))
                    _StructObj.ExciseNo = Agl.Xnull(.Item("Excise"))
                    _StructObj.Range = Agl.Xnull(.Item("Range"))
                    _StructObj.Division = Agl.Xnull(.Item("Division"))
                    _StructObj.IECCode = Agl.Xnull(.Item("IECCode"))
                    _StructObj.FBTOnPer = AgL.VNull(.Item("FBTOnPer"))
                    _StructObj.FBTPer = AgL.VNull(.Item("FBTPer"))
                    _StructObj.TDSName = Agl.Xnull(.Item("TCName"))
                    _StructObj.TDSCode = Agl.Xnull(.Item("TDS_Catg"))

                    _StructObj.PolicyNo = Agl.Xnull(.Item("PolicyNo"))
                    _StructObj.ZoneName = Agl.Xnull(.Item("ZMName"))
                    _StructObj.ZoneCode = Agl.Xnull(.Item("Zone"))
                    _StructObj.DistributorName = Agl.Xnull(.Item("DName"))
                    _StructObj.DistributorCode = Agl.Xnull(.Item("Distributor"))
                    _StructObj.LedgerGroupName = Agl.Xnull(.Item("LGName"))
                    _StructObj.LedgerGroupCode = Agl.Xnull(.Item("LedgerGroup"))
                End With

            End If
        End If
        Topctrl1.FSetDispRec(BMBMaster)
        ADTemp = Nothing
        DTTemp = Nothing
    End Sub

    Sub IniList()
        Dim mQry$
        mQry = "Select GroupCode,GroupName as [Group],Nature,GroupNature From AcGroup Order by GroupName"
        txtAcGroup.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)
        mQry = "Select Code as Code,Name As Name From CostCenterMast Order by Name"
        TxtCostCenter.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)

    End Sub


    Private Sub Topctrl1_tbAdd() Handles Topctrl1.tbAdd
        _StructObj = Nothing
        _StructObj = New ClsStructure.StuctAcDetails
        txtManualCode.Text = CMain.FGetMaxNo("Select IsNull(Max(Convert(bigint,ManualCode)),0)+1 As Mx From SubGroup Where isnumeric(ManualCode)<>0 ", AgL.GCn)
        _StructObj.Location = "W"

        BtnAccountDetail.Enabled = True
        txtNature.Enabled = False
        cmbPartyName.Focus()
    End Sub

    Private Sub Topctrl1_tbDel() Handles Topctrl1.tbDel
        Dim BlnTrans As Boolean = False
        Dim GCnCmd As New SqlClient.SqlCommand

        Try
            If DTMaster.Rows.Count > 0 Then
                If MsgBox(" Delete Conflict ", MsgBoxStyle.YesNo) = vbYes Then
                    StrDocID = ""
                    StrDocID = AgL.XNull(DTMaster.Rows(BMBMaster.Position).Item("SearchCode"))
                    If Trim(StrDocID) = "" Then MsgBox(" Invalid " + " DocId")

                    BlnTrans = True
                    GCnCmd.Connection = AgL.GCn
                    GCnCmd.Transaction = AgL.GCn.BeginTransaction(IsolationLevel.Serializable)
                    GCnCmd.CommandText = "Delete From Subgroup Where SubCode='" & StrDocID & "'"
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
            txtNature.Enabled = False
            BtnAccountDetail.Enabled = True
            cmbPartyName.Focus()
        End If
    End Sub

    Private Sub Topctrl1_tbFind() Handles Topctrl1.tbFind
        If DTMaster.Rows.Count <= 0 Then MsgBox(ClsMain.MsgRecNotFnd + " To Find") : Exit Sub

        Try
            AgL.PubFindQry = "Select SG.SubCode,SG.[Name],SG.ManualCode,AG.GroupName As GroupUnder,IsNull(SG1.Name,'') As Distributor, " & _
                         "(SG.Add1+''+SG.Add2) as Address,IsNull(CityName,'') as City,IsNull(Ctry.Name,'') as Country, " & _
                         "ZN.Name AS Zone,SG.Nature,IsNull(CCM.Name,'') As CCenter,SG.TINNo " & _
                         "From Subgroup SG " & _
                         "Left Join City C On C.CityCode=SG.CityCode  " & _
                         "Left Join Country Ctry On Ctry.Code=SG.CountryCode " & _
                         "Left Join ZoneMast ZN On ZN.Code=SG.Zone " & _
                         "Left Join AcGroup AG On AG.GroupCode=SG.GroupCode  " & _
                         "Left Join CostCenterMast CCM On CCM.Code=SG.CostCenter  " & _
                         "Left Join SubGroup SG1 On SG1.SubCode=SG.Distributor "

            AgL.PubFindQryOrdBy = "[Name]"
            'LIPublic.CreateAndSendArr("150,100,150,150,180,100,100,80,100,80,100")

            '*************** common code start *****************
            FrmFind.ShowDialog()
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
        'Dim FrmObj_Show As FrmPrintAC
        'If DTMaster.Rows.Count > 0 Then
        '    FrmObj_Show = New FrmPrintAC(Agl.Xnull(DTMaster.Rows(BMBMaster.Position).Item("SearchCode")), "", Me)
        '    FrmObj_Show.MdiParent = Me.MdiParent
        '    FrmObj_Show.Show()
        'End If
        'FrmObj_Show = Nothing
    End Sub
    Private Sub Topctrl1_tbSave() Handles Topctrl1.tbSave
        Dim BlnTrans As Boolean = False
        Dim GCnCmd As New SqlClient.SqlCommand
        Dim StrName As String, StrSiteList As String = ""

        Try
            If AgL.RequiredField(cmbPartyName, "Party Name") Then Exit Sub
            If AgL.RequiredField(txtManualCode, "Manual Code") Then Exit Sub
            If AgL.RequiredField(txtAcGroup, "Group Name") Then Exit Sub
            'txtNature.Text = "Customer"
            If AgL.RequiredField(txtNature, "Nature") Then Exit Sub

            StrName = CMain.FRemoveSpace(cmbPartyName.Text)
            StrDocID = ""
            If Topctrl1.Mode = "Add" Then
                StrSiteList = CMain.FGetAllSiteList()
                StrDocID = AgL.PubSiteCode + Trim(CMain.FGetMaxNoWithSiteCode("SubGroup", "SubCode", AgL.GCn))
            Else
                If StrSiteList = "" Then StrSiteList = CMain.FGetAllSiteList()
                StrDocID = AgL.XNull(DTMaster.Rows(BMBMaster.Position).Item("SearchCode"))
            End If
            If Trim(Replace(StrDocID, 0, "")) = "" Then MsgBox(" Invalid " + " DocId") : Exit Sub

            If CMain.DuplicacyChecking("Select Count(*) From SubGroup where [Name] ='" & StrName & "' and SubCode<>'" & StrDocID & "'", "Duplicate Party Name Not Allowed !!!") = True Then cmbPartyName.Focus() : Exit Sub
            If CMain.DuplicacyChecking("Select Count(*) From SubGroup where ManualCode ='" & txtManualCode.Text & "' and SubCode<>'" & StrDocID & "'", "Duplicate Manual Code Not Allowed !!!") = True Then txtManualCode.Focus() : Exit Sub
            If Trim(_StructObj.TIN) <> "" And UCase(Trim(_StructObj.DuplicateTIN)) <> "Y" Then
                If CMain.DuplicacyChecking("Select Count(*) From SubGroup where TinNo =" & AgL.Chk_Text(_StructObj.TIN) & " and SubCode<>'" & StrDocID & "'", "Duplicate TIN Not Allowed !!!") = True Then Exit Sub
            End If

            BlnTrans = True
            GCnCmd.Connection = AgL.GCn
            GCnCmd.Transaction = AgL.GCn.BeginTransaction(IsolationLevel.Serializable)

            If Topctrl1.Mode = "Add" Then
                GCnCmd.CommandText = "Insert Into SubGroup(SubCode, Div_Code,SiteList,[Name], DispName,GroupCode,GroupNature,ManualCode," & _
                        "Nature,Add1,Add2,CityCode,CountryCode,PIN,Phone,Mobile,FAX,ContactPerson," & _
                        "Remark,DuplicateTIN,PolicyNo," & _
                        "U_Name,U_EntDt,U_AE,CostCenter," & _
                        "PAN,CSTNo,EMail,TINNo,Location,CreditLimit,DueDays," & _
                        "PartyCat,PartyType,ECCCode,Excise," & _
                        "Range,Division,FBTOnPer,FBTPer,STNo,IECCode,TDS_Catg,Zone,LedgerGroup,Distributor,Site_Code) " & _
                        "Values ('" & StrDocID & "', '" & AgL.PubDivCode & "','" & StrSiteList & "'," & AgL.Chk_Text(StrName) & "," & AgL.Chk_Text(StrName) & "," & AgL.Chk_Text(txtAcGroup.Tag) & ",'" & GNature & "','" & _
                        txtManualCode.Text & "','" & txtNature.Text & "','" & _StructObj.Address1 & "','" & _StructObj.Address2 & "'," & AgL.Chk_Text(_StructObj.CityCode) & "," & AgL.Chk_Text(_StructObj.CountryCode) & ",'" & _
                        "" & _StructObj.PIN & "','" & _StructObj.PhoneNo & "','" & _StructObj.Mobile & "','" & _StructObj.Fax & "','" & _
                       _StructObj.ContactPerson & "','" & _StructObj.Remark & "','" & _StructObj.DuplicateTIN & "','" & _StructObj.PolicyNo & "','" & _
                        AgL.PubUserName & "','" & Format(Date.Now, "Short Date") & "','" & Microsoft.VisualBasic.Left(Topctrl1.Mode, 1) & "'," & AgL.Chk_Text(TxtCostCenter.Tag) & ", " & _
                        "" & AgL.Chk_Text(_StructObj.PAN) & "," & AgL.Chk_Text(_StructObj.CST) & "," & AgL.Chk_Text(_StructObj.EMail) & ", " & _
                        "" & AgL.Chk_Text(_StructObj.TIN) & "," & AgL.Chk_Text(_StructObj.Location) & ", " & _
                        "" & AgL.Chk_Text(_StructObj.CreditLimit) & "," & AgL.Chk_Text(_StructObj.DueDays) & "," & _
                        "" & AgL.Chk_Text(_StructObj.PartyCategory) & "," & AgL.Chk_Text(_StructObj.PartyType) & ", " & _
                        "" & AgL.Chk_Text(_StructObj.ECCCode) & "," & AgL.Chk_Text(_StructObj.ExciseNo) & "," & _
                        "" & AgL.Chk_Text(_StructObj.Range) & "," & AgL.Chk_Text(_StructObj.Division) & "," & _
                        "" & Val(_StructObj.FBTOnPer) & "," & Val(_StructObj.FBTPer) & "," & AgL.Chk_Text(_StructObj.ST) & "," & AgL.Chk_Text(_StructObj.IECCode) & "," & AgL.Chk_Text(_StructObj.TDSCode) & "," & _
                        "" & AgL.Chk_Text(_StructObj.ZoneCode) & "," & AgL.Chk_Text(_StructObj.LedgerGroupCode) & "," & AgL.Chk_Text(_StructObj.DistributorCode) & ",'" & AgL.PubSiteCode & "' )"
            Else
                GCnCmd.CommandText = "update SubGroup Set [Name]=" & AgL.Chk_Text(StrName) & ", DispName = " & AgL.Chk_Text(StrName) & ",GroupCode=" & AgL.Chk_Text(txtAcGroup.Tag) & ",GroupNature='" & GNature & "',ManualCode='" & txtManualCode.Text & "'," & _
                        "Nature='" & txtNature.Text & "',Add1='" & _StructObj.Address1 & "',Add2='" & _StructObj.Address2 & "',CityCode=" & AgL.Chk_Text(_StructObj.CityCode) & ",CountryCode=" & AgL.Chk_Text(_StructObj.CountryCode) & ",PIN='" & _StructObj.PIN & "',Phone='" & _StructObj.PhoneNo & "',Mobile='" & _StructObj.Mobile & "',FAX='" & _StructObj.Fax & "',ContactPerson='" & _StructObj.ContactPerson & "'," & _
                        "Remark=" & AgL.Chk_Text(_StructObj.Remark) & ",CostCenter=" & AgL.Chk_Text(TxtCostCenter.Tag) & ", " & _
                        "U_Name='" & AgL.PubUserName & "',U_EntDt='" & Format(Date.Now, "Short Date") & "',U_AE='" & Microsoft.VisualBasic.Left(Topctrl1.Mode, 1) & "'," & _
                        "PAN=" & AgL.Chk_Text(_StructObj.PAN) & ", " & _
                        "CSTNo=" & AgL.Chk_Text(_StructObj.CST) & ", " & _
                        "STNo=" & AgL.Chk_Text(_StructObj.ST) & ", " & _
                        "PolicyNo=" & AgL.Chk_Text(_StructObj.PolicyNo) & ", " & _
                        "EMail=" & AgL.Chk_Text(_StructObj.EMail) & ", " & _
                        "TINNo=" & AgL.Chk_Text(_StructObj.TIN) & ", " & _
                        "Location=" & AgL.Chk_Text(_StructObj.Location) & ", " & _
                        "PartyCat=" & AgL.Chk_Text(_StructObj.PartyCategory) & ", " & _
                        "PartyType=" & AgL.Chk_Text(_StructObj.PartyType) & ", " & _
                        "ECCCode=" & AgL.Chk_Text(_StructObj.ECCCode) & ", " & _
                        "Excise=" & AgL.Chk_Text(_StructObj.ExciseNo) & ", " & _
                        "Range=" & AgL.Chk_Text(_StructObj.Range) & ", " & _
                        "Division=" & AgL.Chk_Text(_StructObj.Division) & ", " & _
                        "IECCode=" & AgL.Chk_Text(_StructObj.IECCode) & ", " & _
                        "CreditLimit=" & (_StructObj.CreditLimit) & ", " & _
                        "DueDays=" & (_StructObj.DueDays) & ", " & _
                        "FBTOnPer=" & Val(_StructObj.FBTOnPer) & "," & _
                        "FBTPer=" & Val(_StructObj.FBTPer) & ", " & _
                        "Zone=" & AgL.Chk_Text(_StructObj.ZoneCode) & "," & _
                        "LedgerGroup=" & AgL.Chk_Text(_StructObj.LedgerGroupCode) & "," & _
                        "Distributor=" & AgL.Chk_Text(_StructObj.DistributorCode) & "," & _
                        "Transfered='N', " & _
                        "SiteList=" & AgL.Chk_Text(StrSiteList) & ", " & _
                        "TDS_Catg=" & AgL.Chk_Text(_StructObj.TDSCode) & " " & _
                        "Where SubCode='" & StrDocID & "'"
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
    Private Sub FrmAccountMaster_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub

    Public Sub FTxtKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        '======== Write Your Code Below =============
        Select Case sender.Name
            Case sender.name
                If e.KeyCode = Keys.Delete Then
                    sender.Text = "" : sender.Tag = ""

                End If
        End Select
    End Sub
    Public Sub FTxtKeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        '======== Write Your Code Below =============
        Select Case sender.Name
        End Select
    End Sub

    Private Sub txtAcGroup_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtAcGroup.Validating

    End Sub

    Private Sub txtNature_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtNature.Validating, txtAcGroup.Validating
        Dim DrTemp As DataRow() = Nothing
        Select Case sender.name
            Case txtAcGroup.Name
                If txtAcGroup.Text <> "" Then
                    DrTemp = sender.AgHelpDataSet.Tables(0).Select("GroupCode = " & AgL.Chk_Text(sender.AgSelectedValue) & "")
                    txtNature.Text = AgL.XNull(DrTemp(0)("Nature"))
                End If
        End Select
    End Sub
End Class




