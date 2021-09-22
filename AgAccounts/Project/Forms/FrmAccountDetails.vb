Public Class FrmAccountDetails
    Private LIEvent As ClsEvents
    Dim FrmCMMain As FrmAccountMaster

    Sub New(ByVal FrmCMVar As FrmAccountMaster, ByVal mSetMode As Boolean)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        FrmCMMain = FrmCMVar
        btnOK.Enabled = mSetMode
        SetEnableDisable(mSetMode)
        With FrmCMMain._StructObj
            txtAddress1.Text = .Address1
            txtAddress2.Text = .Address2
            txtPhoneNo.Text = .PhoneNo
            txtMobNo.Text = .Mobile
            txtFaxNo.Text = .Fax
            TxtRemarks.Text = .Remark
            txtCountry.Text = .CountryName
            txtCountry.Tag = .CountryCode
            txtCity.Text = .CityName
            txtCity.Tag = .CityCode
            txtContactPerson.Text = .ContactPerson
            txtPIN.Text = .PIN

            TxtPAN.Text = .PAN
            TxtCST.Text = .CST
            TxtSTNo.Text = .ST
            TxtEMail.Text = .EMail

            TxtDuplicateTIN.Text = IIf(UCase(.DuplicateTIN) = "Y", "Yes", "No")
            TxtDuplicateTIN.Tag = .DuplicateTIN
            TxtTIN.Text = .TIN

            TxtLocation.Text = IIf(UCase(.Location) = "W", "With In State", _
                               IIf(UCase(.Location) = "O", "Out Of State", _
                               IIf(UCase(.Location) = "C", "Out Of Country", _
                               IIf(UCase(.Location) = "N", "Not Applicable", ""))))

            TxtLocation.Tag = .Location

            If mSetMode = True And TxtLocation.Tag = "" Then
                TxtLocation.Text = "With In State"
                .Location = "W"
                TxtLocation.Tag = "W"
            End If

            TxtPartyCat.Text = IIf(UCase(.PartyCategory) = "D", "With In District", _
                               IIf(UCase(.PartyCategory) = "O", "Out Of District", ""))
            TxtPartyCat.Tag = .PartyCategory

            TxtPartyType.Text = IIf(UCase(.PartyType) = "R", "Registered", _
                               IIf(UCase(.PartyType) = "U", "Unregistered", ""))
            TxtPartyType.Tag = .PartyType
            TxtCreditLimit.Text = .CreditLimit
            TxtDueDays.Text = .DueDays
            TxtExciseNo.Text = .ExciseNo
            TxtECCCode.Text = .ECCCode
            TxtRange.Text = .Range
            TxtDivision.Text = .Division
            TxtIEC.Text = .IECCode
            TxtFBTOnPer.Text = .FBTOnPer
            TxtFBTPer.Text = .FBTPer
            TxtTDSCategory.Text = .TDSName
            TxtTDSCategory.Tag = .TDSCode
            TxtPolicyNo.Text = .PolicyNo

            TxtZone.Text = .ZoneName
            TxtZone.Tag = .ZoneCode
            TxtDistributor.Text = .DistributorName
            TxtDistributor.Tag = .DistributorCode
            TxtLedgerGroup.Text = .LedgerGroupName
            TxtLedgerGroup.Tag = .LedgerGroupCode
        End With
        ' Add any initialization after the InitializeComponent() call.
    End Sub
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
    Private Sub FrmAccountDetails_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LIEvent = New ClsEvents(Me)
        IniList()
        Me.BackColor = CMain.ClrPubBackColorForm
    End Sub

    Public Sub FTxtKeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        '======== Write Your Code Below =============
        Select Case sender.Name
            Case TxtCreditLimit.Name
                CMain.NumPress(sender, e, 10, 2, False)
            Case TxtFBTOnPer.Name, TxtFBTPer.Name
                CMain.NumPress(sender, e, 3, 2, False)
            Case TxtDueDays.Name
                CMain.NumPress(sender, e, 5, 0, False)
        End Select
    End Sub
    Private Sub BtnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        If Len(Trim(TxtTIN.Text)) <> 11 And Trim(TxtTIN.Text) <> "" Then MsgBox("TIN Should Be Of 11(Eleven) Characters.") : TxtTIN.Focus() : Exit Sub
        If Len(Trim(TxtPAN.Text)) <> 10 And Trim(TxtPAN.Text) <> "" Then MsgBox("PAN Should Be Of 10(Ten) Characters.") : TxtPAN.Focus() : Exit Sub
        If AgL.RequiredField(TxtLocation, "Location") Then Exit Sub

        With FrmCMMain._StructObj
            .Address1 = CMain.FRemoveSpace(txtAddress1.Text)
            .Address2 = CMain.FRemoveSpace(txtAddress2.Text)
            .PIN = txtPIN.Text
            .PhoneNo = txtPhoneNo.Text
            .Remark = TxtRemarks.Text
            .Mobile = txtMobNo.Text
            .CityCode = txtCity.Tag
            .CityName = txtCity.Text
            .CountryName = txtCountry.Text
            .CountryCode = AgL.XNull(txtCountry.Tag)
            .Fax = txtFaxNo.Text
            .ContactPerson = CMain.FRemoveSpace(txtContactPerson.Text)

            .PAN = TxtPAN.Text
            .CST = TxtCST.Text
            .ST = TxtSTNo.Text
            .EMail = TxtEMail.Text
            .DuplicateTIN = TxtDuplicateTIN.Tag
            .TIN = TxtTIN.Text
            .Location = TxtLocation.Tag
            .PartyCategory = TxtPartyCat.Tag
            .PartyType = TxtPartyType.Tag
            .CreditLimit = TxtCreditLimit.Text
            .DueDays = TxtDueDays.Text
            .ECCCode = TxtECCCode.Text
            .ExciseNo = TxtExciseNo.Text
            .Range = TxtRange.Text
            .Division = TxtDivision.Text
            .IECCode = TxtIEC.Text
            .FBTOnPer = Val(TxtFBTOnPer.Text)
            .FBTPer = Val(TxtFBTPer.Text)
            .TDSCode = TxtTDSCategory.Tag
            .TDSName = TxtTDSCategory.Text
            .PolicyNo = TxtPolicyNo.Text

            .ZoneName = TxtZone.Text
            .ZoneCode = TxtZone.Tag
            .DistributorName = TxtDistributor.Text
            .DistributorCode = TxtDistributor.Tag
            .LedgerGroupName = TxtLedgerGroup.Text
            .LedgerGroupCode = TxtLedgerGroup.Tag
        End With
        Me.Close()
    End Sub
    Private Sub FrmAccountDetails_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, 0)
    End Sub
    Private Sub SetEnableDisable(ByVal mSet As Boolean)
        Dim Control As Object = Nothing
        For Each Control In Me.Controls
            If TypeOf Control Is TextBox Then
                Control.enabled = mSet
                Control.BackColor = Color.White
            End If
        Next
    End Sub

    Sub IniList()
        Dim mQry$

        mQry = "Select CityCode as Code,CityName From City Order by CityName"
        txtCity.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)

        mQry = "Select Code,[Name] From Country Order by [Name]"
        txtCountry.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)

        mQry = "Select Code,[Name] From TdsCat Order by [Name]"
        TxtTDSCategory.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)

        mQry = "Select SG.SubCode,SG.Name,IfNull(CT.CityName,''),SG.ManualCode From SubGroup SG Left Join City CT On CT.CityCode=SG.CityCode Where SG.SiteList Like '%|" & AgL.PubSiteCode & "|%' And SG.Nature In ('Agent','Customer','Supplier') Order by SG.Name"
        TxtDistributor.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)

        mQry = "Select Code,[Name] From LedgerGroup Order by [Name]"
        TxtLedgerGroup.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)

        mQry = "select 'W' as code, 'With In State' as Name
                Union select 'O' as code, 'Out of State' as Name
                Union select 'C' as code, 'Out of Country' as Name
                Union select 'N' as code, 'Not Applicable' as Name
                Order by Name"
        TxtLocation.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)

        mQry = "select 'D' as code, 'With In District' as Name
                Union select 'O' as code, 'Out of District' as Name
                Order by Name "
        TxtPartyCat.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)



        mQry = "select 'R' as code, 'Registered' as Name
                Union select 'U' as code, 'Unregistered' as Name
                Order by Name "

        TxtPartyType.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)



        mQry = "select 'Y' as code, 'Yes' as Name
                Union select 'N' as code, 'No' as Name
                Order by Name "
        TxtDuplicateTIN.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)


        mQry = "Select Code,[Name] From ZoneMast Order by [Name]"
        TxtZone.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)
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

    Public Sub FTxtGotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        '======== Write Your Code Below =============
    End Sub

End Class