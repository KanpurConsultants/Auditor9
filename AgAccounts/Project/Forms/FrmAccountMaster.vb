Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.SQLite
Imports AgLibrary.ClsMain.agConstants

Public Class FrmAccountMaster
    Private DTMaster As New DataTable
    Public BMBMaster As BindingManagerBase
    Private LIEvent As ClsEvents
    Public _StructObj As New ClsStructure.StuctAcDetails
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
                FrmObjMDI.FAccountOtherDetail(AgL.XNull(DTMaster.Rows(BMBMaster.Position).Item("SearchCode")), False)
            ElseIf Topctrl1.Mode = "Edit" Then
                FrmObjMDI.FAccountOtherDetail(AgL.XNull(DTMaster.Rows(BMBMaster.Position).Item("SearchCode")), True)
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
            LblSalesTaxNo.Text = "GST No."
        Catch ex As Exception




            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub FIniMaster(Optional ByVal BytDel As Byte = 0, Optional ByVal BytRefresh As Byte = 1)
        Topctrl1.FIniForm(DTMaster, AgL.GCn, "Select Sg.SubCode As SearchCode,Sg.Name From SubGroup Sg Left Join AcGroup Ag on Sg.GroupCode = Ag.GroupCode Where Ag.GroupNature in ('E','R','A','L') Order By Sg.Name", True, cmbPartyName, "SearchCode", "Name", BytDel, BytRefresh)
    End Sub
    Private Sub Topctrl1_tbSite() Handles Topctrl1.tbSite
        Dim DTTemp As DataTable
        Dim StrCurrentvalue As String = ""
        If DTMaster.Rows.Count > 0 Then
            DTTemp = AgL.FillData("Select SiteList From Subgroup Where SubCode='" & AgL.XNull(DTMaster.Rows(BMBMaster.Position).Item("SearchCode")) & "'", AgL.GCn).Tables(0) '  cmain.FGetDatTable("Select SiteList From Subgroup Where SubCode='" & Agl.Xnull(DTMaster.Rows(BMBMaster.Position).Item("SearchCode")) & "'", Agl.Gcn)
            If DTTemp.Rows.Count > 0 Then StrCurrentvalue = AgL.XNull(DTTemp.Rows(0).Item("SiteList"))

            'Topctrl1.FManageSite("Subgroup", "Update Subgroup Set SiteList='@' Where SubCode='" & Agl.Xnull(DTMaster.Rows(BMBMaster.Position).Item("SearchCode")) & "' ", StrCurrentvalue, Agl.Gcn)
        End If
    End Sub
    Private Sub Topctrl1_tbDiscard() Handles Topctrl1.tbDiscard
        FIniMaster(0, 0)
    End Sub
    Public Sub MoveRec()

        Dim DTTemp As New DataTable
        Dim StrSQL As String = ""

        Topctrl1.BlankTextBoxes()
        If DTMaster.Rows.Count > 0 Then

            StrSQL = "Select SG.SubCode, Sg.Name as CustomerName, "
            StrSQL += "SG.GroupCode,AG.GroupName, Sg.GroupNature, SG.ManualCode, SG.Nature,SG.Address Add1,  "
            StrSQL += "Sg.CityCode,C.cityName, SG.PIN, SG.Phone, "
            StrSQL += "SG.Mobile,SG.ContactPerson,CCM.Name As CCName,SG.CostCenter,"
            StrSQL += "SG.EMail,SG.CreditLimit,SG.CreditDays as DueDays, "
            StrSQL += "SG.SubgroupType PartyType, "
            StrSQL += "SG.TDS_Catg,TC.Name As TCName, SG.PostingGroupSalesTaxItem, SG.HSN "
            StrSQL += "From SubGroup SG "
            StrSQL += "Left Join AcGroup AG On AG.GroupCode=SG.GroupCode "
            StrSQL += "Left Join City C On C.CityCode=SG.CityCode "
            StrSQL += "Left Join CostCenterMast CCM  On CCM.Code=SG.CostCenter "
            StrSQL += "Left Join TDSCat TC On TC.Code=SG.TDS_Catg "
            StrSQL += "Where SG.SubCode='" & AgL.XNull(DTMaster.Rows(BMBMaster.Position).Item("SearchCode") & "' ")

            DTTemp = AgL.FillData(StrSQL, AgL.GCn).Tables(0)


            If DTTemp.Rows.Count > 0 Then
                With DTTemp.Rows(0)

                    cmbPartyName.Text = .Item("CustomerName")
                    txtManualCode.Text = AgL.XNull(.Item("ManualCode"))
                    txtAcGroup.Tag = AgL.XNull(.Item("GroupCode"))
                    txtAcGroup.Text = AgL.XNull(.Item("GroupName"))
                    txtNature.Text = AgL.XNull(.Item("Nature"))
                    TxtCostCenter.Text = AgL.XNull(.Item("CCName"))
                    TxtCostCenter.Tag = AgL.XNull(.Item("CostCenter"))

                    TxtSalesTaxPostingGroup.Text = AgL.XNull(.Item("PostingGroupSalesTaxItem"))
                    TxtSalesTaxPostingGroup.Tag = AgL.XNull(.Item("PostingGroupSalesTaxItem"))
                    TxtHsn.Text = AgL.XNull(.Item("Hsn"))
                    TxtSalesTaxNo.Text = AgL.XNull(AgL.Dman_Execute("Select RegistrationNo From SubGroupRegistration 
                                    Where SubCode = '" & DTMaster.Rows(BMBMaster.Position).Item("SearchCode") & "' 
                                    And RegistrationType = '" & AgLibrary.ClsMain.agConstants.SubgroupRegistrationType.SalesTaxNo & "'", AgL.GCn).ExecuteScalar())




                    _StructObj.Address1 = AgL.XNull(.Item("Add1"))
                    _StructObj.PIN = AgL.XNull(.Item("Pin"))
                    _StructObj.PhoneNo = AgL.XNull(.Item("Phone"))
                    _StructObj.Mobile = AgL.XNull(.Item("Mobile"))
                    _StructObj.CityCode = AgL.XNull(.Item("Citycode"))
                    _StructObj.CityName = AgL.XNull(.Item("Cityname"))

                    _StructObj.ContactPerson = AgL.XNull(.Item("ContactPerson"))

                    _StructObj.EMail = AgL.XNull(.Item("EMail"))
                    _StructObj.PartyType = AgL.XNull(.Item("PartyType"))
                    _StructObj.CreditLimit = AgL.VNull(.Item("CreditLimit"))
                    _StructObj.DueDays = AgL.VNull(.Item("DueDays"))
                    _StructObj.TDSName = AgL.XNull(.Item("TCName"))
                    _StructObj.TDSCode = AgL.XNull(.Item("TDS_Catg"))

                End With

            End If
        End If
        Topctrl1.FSetDispRec(BMBMaster)

        DTTemp = Nothing
    End Sub

    Sub IniList()
        Dim mQry$
        mQry = "Select GroupCode,GroupName as [Group],Nature,GroupNature From AcGroup where GroupNature In ('E','R','A','L') Order by GroupName"
        txtAcGroup.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)
        mQry = "Select Code as Code,Name As Name From CostCenterMast Order by Name"
        TxtCostCenter.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)

        mQry = "SELECT Description as  Code, Description AS PostingGroupSalesTaxItem FROM PostingGroupSalesTaxItem "
        TxtSalesTaxPostingGroup.AgHelpDataSet() = AgL.FillData(mQry, AgL.GCn)


    End Sub


    Private Sub Topctrl1_tbAdd() Handles Topctrl1.tbAdd
        _StructObj = Nothing
        _StructObj = New ClsStructure.StuctAcDetails
        txtManualCode.Text = CMain.FGetMaxNo("Select IfNull(Max(Cast(ManualCode as Integer)),0)+1 As Mx From SubGroup Where (Case When ManualCode GLOB '*[0-9]*' Then ManualCode Else 0 End)<>0 ", AgL.GCn)
        _StructObj.Location = "W"

        BtnAccountDetail.Enabled = True
        txtNature.Enabled = False
        cmbPartyName.Focus()
    End Sub

    Private Sub Topctrl1_tbDel() Handles Topctrl1.tbDel
        Dim BlnTrans As Boolean = False
        Dim GCnCmd As New Object

        Try
            If DTMaster.Rows.Count > 0 Then
                If MsgBox(" Delete Conflict ", MsgBoxStyle.YesNo) = vbYes Then
                    StrDocID = ""
                    StrDocID = AgL.XNull(DTMaster.Rows(BMBMaster.Position).Item("SearchCode"))
                    If Trim(StrDocID) = "" Then MsgBox(" Invalid " + " DocId")

                    BlnTrans = True
                    GCnCmd = AgL.GCn.CreateCommand
                    GCnCmd.Transaction = AgL.GCn.BeginTransaction(IsolationLevel.Serializable)
                    GCnCmd.CommandText = "Delete From SubgroupRegistration Where SubCode='" & StrDocID & "'"
                    GCnCmd.ExecuteNonQuery()
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
            AgL.PubFindQry = "Select SG.SubCode,SG.[Name],SG.ManualCode,AG.GroupName As GroupUnder, " &
                         "Ag.GroupNature,IfNull(CCM.Name,'') As CCenter " &
                         "From Subgroup SG " &
                         "Left Join City C On C.CityCode=SG.CityCode  " &
                         "Left Join AcGroup AG On AG.GroupCode=SG.GroupCode  " &
                         "Left Join CostCenterMast CCM On CCM.Code=SG.CostCenter  "

            AgL.PubFindQryOrdBy = "[Name]"
            'LIPublic.CreateAndSendArr("150,100,150,150,180,100,100,80,100,80,100")

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
        Dim GCnCmd As New Object
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
                'StrDocID = AgL.PubSiteCode + Trim(CMain.FGetMaxNoWithSiteCode("SubGroup", "SubCode", AgL.GCn))
                StrDocID = AgL.GetMaxId("SubGroup", "Subcode", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 8, True)
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
            GCnCmd = AgL.GCn.CreateCommand
            GCnCmd.Transaction = AgL.GCn.BeginTransaction(IsolationLevel.Serializable)

            If Topctrl1.Mode = "Add" Then
                GCnCmd.CommandText = "Insert Into SubGroup(SubCode, Div_Code,SiteList,[Name], DispName,GroupCode,GroupNature,ManualCode," &
                        "Nature,Address,CityCode,PIN,Phone,Mobile,ContactPerson," &
                        "EntryBy,EntryDate,CostCenter," &
                        "EMail,CreditLimit,CreditDays," &
                        "TDS_Catg,Site_Code, PostingGroupSalesTaxItem, Hsn) " &
                        "Values ('" & StrDocID & "', '" & AgL.PubDivCode & "','" & StrSiteList & "'," & AgL.Chk_Text(StrName) & "," & AgL.Chk_Text(StrName) & "," & AgL.Chk_Text(txtAcGroup.Tag) & ",'" & GNature & "','" &
                        txtManualCode.Text & "','" & txtNature.Text & "','" & _StructObj.Address1 & "'," & AgL.Chk_Text(_StructObj.CityCode) & ",'" &
                        "" & _StructObj.PIN & "','" & _StructObj.PhoneNo & "','" & _StructObj.Mobile & "','" &
                       _StructObj.ContactPerson & "','" &
                        AgL.PubUserName & "','" & Format(Date.Now, "Short Date") & "'," & AgL.Chk_Text(TxtCostCenter.Tag) & ", " &
                        "" & AgL.Chk_Text(_StructObj.EMail) & ", " &
                        "" & AgL.Chk_Text(_StructObj.CreditLimit) & "," & AgL.Chk_Text(_StructObj.DueDays) & "," &
                        "" & AgL.Chk_Text(_StructObj.TDSCode) & "," &
                        "'" & AgL.PubSiteCode & "'," & AgL.Chk_Text(TxtSalesTaxPostingGroup.AgSelectedValue) & ",
                        " & AgL.Chk_Text(TxtHsn.Text) & " )"
            Else
                GCnCmd.CommandText = "update SubGroup Set [Name]=" & AgL.Chk_Text(StrName) & ", DispName = " & AgL.Chk_Text(StrName) & ",GroupCode=" & AgL.Chk_Text(txtAcGroup.Tag) & ",GroupNature='" & GNature & "',ManualCode='" & txtManualCode.Text & "'," &
                        "Nature='" & txtNature.Text & "',Address='" & _StructObj.Address1 & "',CityCode=" & AgL.Chk_Text(_StructObj.CityCode) & ",PIN='" & _StructObj.PIN & "',Phone='" & _StructObj.PhoneNo & "',Mobile='" & _StructObj.Mobile & "',ContactPerson='" & _StructObj.ContactPerson & "'," &
                        "CostCenter=" & AgL.Chk_Text(TxtCostCenter.Tag) & ", " &
                        "MoveToLog='" & AgL.PubUserName & "',MoveToLogDate='" & Format(Date.Now, "Short Date") & "'," &
                        "EMail=" & AgL.Chk_Text(_StructObj.EMail) & ", " &
                        "CreditLimit=" & (_StructObj.CreditLimit) & ", " &
                        "CreditDays=" & (_StructObj.DueDays) & ", " &
                        "SiteList=" & AgL.Chk_Text(StrSiteList) & ", " &
                        "PostingGroupSalesTaxItem=" & AgL.Chk_Text(TxtSalesTaxPostingGroup.AgSelectedValue) & ", " &
                        "Hsn=" & AgL.Chk_Text(TxtHsn.Text) & ", " &
                        "TDS_Catg=" & AgL.Chk_Text(_StructObj.TDSCode) & " " &
                        "Where SubCode='" & StrDocID & "'"
            End If

            GCnCmd.ExecuteNonQuery()

            Dim mQry As String = ""
            If TxtSalesTaxNo.Text <> "" Then
                If AgL.Dman_Execute("Select Count(*) From SubgroupRegistration Where SubCode = '" & StrDocID & "' 
                        And RegistrationType = '" & SubgroupRegistrationType.SalesTaxNo & "'", AgL.GcnRead).ExecuteScalar = 0 Then
                    Dim mRegSr As Integer = AgL.Dman_Execute("Select IfNull(Max(Sr),0) + 1 From SubgroupRegistration Where SubCode = '" & StrDocID & "'", AgL.GCn).ExecuteScalar
                    GCnCmd.CommandText = "Insert Into SubgroupRegistration(Subcode, Sr, RegistrationType, RegistrationNo)
                        Values ('" & StrDocID & "', " & mRegSr & ", '" & SubgroupRegistrationType.SalesTaxNo & "', 
                        " & AgL.Chk_Text(TxtSalesTaxNo.Text) & ") "
                    GCnCmd.ExecuteNonQuery()
                Else
                    GCnCmd.CommandText = " UPDATE SubgroupRegistration Set RegistrationNo = " & AgL.Chk_Text(TxtSalesTaxNo.Text) & "
                        Where SubCode = '" & StrDocID & "' 
                        And RegistrationType = '" & SubgroupRegistrationType.SalesTaxNo & "'"
                    GCnCmd.ExecuteNonQuery()
                End If
            Else
                If AgL.Dman_Execute("Select Count(*) From SubgroupRegistration Where SubCode = '" & StrDocID & "' 
                        And RegistrationType = '" & SubgroupRegistrationType.SalesTaxNo & "'", AgL.GcnRead).ExecuteScalar <> 0 Then
                    GCnCmd.CommandText = "Delete From SubgroupRegistration Where SubCode = '" & StrDocID & "' 
                            And RegistrationType = '" & SubgroupRegistrationType.SalesTaxNo & "'"
                    GCnCmd.ExecuteNonQuery()
                End If
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




